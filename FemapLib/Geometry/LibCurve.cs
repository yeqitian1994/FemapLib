using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using femap;
namespace FemapLib
{
    public class LibCurve
    {
        /// <summary>
        /// 获得所有Curve ID
        /// </summary>
        /// <param name="feModel">Femap Model对象</param>
        /// <returns>Curve ID List</returns>
        public static IList<int> GetAllCurveIDs(model feModel)
        {
            List<int> curveIDs = new List<int>();
            if (feModel == null) feModel = LibApp.GetFemapModel();
            if (feModel != null)
            {
                Curve curve = feModel.feCurve;
                curve.Next();
                while (LibApp.IsIDValid(curve.ID))
                {
                    curveIDs.Add(curve.ID);
                    curve.Next();
                }
            }
            return curveIDs;
        }
    }
}
