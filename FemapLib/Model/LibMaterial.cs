using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using femap;

namespace FemapLib
{
    public class LibMaterial
    {
        public static List<int> GetAllMaterialIDs(model feModel)
        {
            List<int> matIDs = new List<int>();
            if (feModel == null) feModel = LibApp.GetDefaultFemapModel();
            if (feModel != null)
            {
                Matl matl = feModel.feMatl;
                matl.Next();
                while (LibApp.IsIDValid(matl.ID))
                {
                    matIDs.Add(matl.ID);
                    matl.Next();
                }
            }
            return matIDs;
        }

        public static bool HasMaterial(string name,out int id, model feModel = null)
        {
            id = -1;
            if (feModel == null) feModel = LibApp.GetDefaultFemapModel();
            if (feModel != null)
            {
                Matl matl = feModel.feMatl;
                matl.Next();
                while (LibApp.IsIDValid(matl.ID))
                {
                    if(matl.title==name)
                    {
                        id = matl.ID;
                        return true;
                    }
                    matl.Next();
                }
            }
            return false;
        }

        public static int CreateMaterialFormLib(int idInLib,model feModel=null)
        {
            int newMatID = -1;
            if (feModel == null) feModel = LibApp.GetDefaultFemapModel();
            if (feModel != null)
            {
                Matl matl = feModel.feMatl;
                newMatID = matl.NextEmptyID();
                zReturnCode re = matl.GetLibraryOfType(idInLib, zMaterialType.FMT_ISOTROPIC, zLibraryFile.FLIB_SYSTEM);
                re = matl.Put(newMatID);//保存材料
            }
            return newMatID;
        }

    }
}
