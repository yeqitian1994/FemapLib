using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using femap;

namespace FemapLib
{
    public class LibPoint
    {
        /// <summary>
        /// 获得所有Point ID
        /// </summary>
        /// <param name="feModel">Femap Model对象</param>
        /// <returns>Point ID List</returns>
        public static IList<int> GetAllPointIDs(model feModel)
        {
            List<int> pointIDs = new List<int>();
            if (feModel == null) feModel = LibApp.GetDefaultFemapModel();
            if (feModel != null)
            {
                Point point = feModel.fePoint;
                point.Next();
                while (LibApp.IsIDValid(point.ID))
                {
                    pointIDs.Add(point.ID);
                    point.Next();
                }
            }
            return pointIDs;
        }
    }
}
