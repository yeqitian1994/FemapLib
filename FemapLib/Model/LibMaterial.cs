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
        /// <summary>
        /// 获取所有材料的id号
        /// </summary>
        /// <param name="feModel"></param>
        /// <returns></returns>
        public static List<int> GetAllMaterialIDs(model feModel)
        {
            List<int> matIDs = new List<int>();
            if (feModel == null) feModel = LibApp.GetFemapModel();
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
        /// <summary>
        /// 尝试根据标题获得材料
        /// </summary>
        /// <param name="title">材料标题</param>
        /// <param name="id">材料id</param>
        /// <param name="feModel">femap model</param>
        /// <returns>是否成功</returns>
        public static bool TryToGetMaterial(string title,out int id, model feModel = null)
        {
            id = -1;
            if (feModel == null) feModel = LibApp.GetFemapModel();
            if (feModel != null)
            {
                Matl matl = feModel.feMatl;
                matl.Next();
                while (LibApp.IsIDValid(matl.ID))
                {
                    if(matl.title==title)
                    {
                        id = matl.ID;
                        return true;
                    }
                    matl.Next();
                }
            }
            return false;
        }

        public static int CreateMaterialFormLib(int idInLib, zMaterialType matType= zMaterialType.FMT_ISOTROPIC, zLibraryFile libFile= zLibraryFile.FLIB_SYSTEM, model feModel=null)
        {
            int newMatID = -1;
            if (feModel == null) feModel = LibApp.GetFemapModel();
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
