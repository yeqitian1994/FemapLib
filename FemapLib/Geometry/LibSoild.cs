using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using femap;

namespace FemapLib
{
    public class LibSoild
    {
        /// <summary>
        /// 获得所有Solid对象
        /// </summary>
        /// <param name="feModel">Femap Model对象</param>
        /// <returns>Solid List</returns>
        public static IList<int> GetAllSolidIDs(model feModel)
        {
            List<int> solidIDs = new List<int>();
            if (feModel == null) feModel = LibApp.GetFemapModel();
            if (feModel != null)
            {
                Solid solid = feModel.feSolid;
                solid.Next();
                while (LibApp.IsIDValid(solid.ID))
                {
                    solidIDs.Add(solid.ID);
                    solid.Next();
                }
            }
            return solidIDs;
        }

        public static int GetSolidByTitle(string name,model feModel)
        {
            if (feModel == null) feModel = LibApp.GetFemapModel();
            if (feModel != null)
            {
                Solid solid = feModel.feSolid;
                solid.Next();
                while (LibApp.IsIDValid(solid.ID))
                {
                    if(solid.title==name)
                    {
                        return solid.ID;
                    }
                    solid.Next();
                }
            }
            return -1;
        }

        public static List<int> GetSurfaceIDsFromSolid(int solidID,model feModel)
        {
            List<int> surfaceIDs = new List<int>();
            if (feModel == null) feModel = LibApp.GetFemapModel();
            if (feModel != null)
            {
                Solid solid = feModel.feSolid;
                zReturnCode rc= solid.Get(solidID);
                if(rc==zReturnCode.FE_OK)
                {
                    solid.Surfaces(zCombinedMode.FCC_OFF, out int numSurf, out object surfaceIDObj);
                    surfaceIDs.AddRange(surfaceIDObj as int[]);
                }
            }
            return surfaceIDs;
        }

        public static List<int> GetCurveIDsFromSolid(int solidID, model feModel)
        {
            List<int> curveIDs = new List<int>();
            if (feModel == null) feModel = LibApp.GetFemapModel();
            if (feModel != null)
            {
                Solid solid = feModel.feSolid;
                zReturnCode rc = solid.Get(solidID);
                if (rc == zReturnCode.FE_OK)
                {
                    solid.Curves(zCombinedMode.FCC_OFF, out int numCurves, out object curveIDObj);
                    curveIDs.AddRange(curveIDObj as int[]);
                }
            }
            return curveIDs;
        }
    }
}
