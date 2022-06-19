using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using femap;

namespace FemapLib
{
    public class LibSurface
    {
        /// <summary>
        /// 获得所有Surface对象
        /// </summary>
        /// <param name="feModel">Femap Model对象</param>
        /// <returns>Surface List</returns>
        public static IList<int> GetAllSurfaceIDs(model feModel)
        {
            List <int> surfaceIDs = new List<int>();
            if (feModel == null)feModel = LibApp.GetDefaultFemapModel();
            if (feModel != null)
            {
                Surface surface= feModel.feSurface;
                surface.Next();
                while (LibApp.IsIDValid(surface.ID))
                {
                    surfaceIDs.Add(surface.ID);
                    surface.Next();
                }
            }
            return surfaceIDs;
        }

        public static FeVector3d GetSurfaceNormalAtCenter(int surfaceID,model feModel)
        {
            FeVector3d vec =new FeVector3d();
            if(feModel!=null)
            {
                Surface surface= feModel.feSurface;
                zReturnCode rc= surface.Get(surfaceID);
                if(rc==zReturnCode.FE_OK)
                {
                    surface.normal(0.5, 0.5, out object normal);
                    double[] normalV= normal as double[];
                    vec.X = normalV[0];
                    vec.Y = normalV[1];
                    vec.Z = normalV[2];
                }
            }
            return vec;
        }
    }
}
