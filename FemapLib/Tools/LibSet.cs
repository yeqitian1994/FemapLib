using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using femap;

namespace FemapLib
{
    public class LibSet
    {
        public static List<int> GetIDsFromSet(Set feSet) 
        {
            List<int> ids = new List<int>();
            if (feSet != null)
            {
                feSet.GetArray(out int num, out object idArrayObj);
                if(num>0)
                {
                    foreach (var id in idArrayObj as int[])
                    {
                        ids.Add(id);
                    }
                }
            }
            return ids;
        }

        public static Set SelectEntities(model feModel, zDataType dataType)
        {
            Set set = null;
            if (feModel != null)
            {
                set = feModel.feSet;
                set.Select(dataType, true, "请选择" + dataType.ToString() + "。");
            }
            return set;
        }
        /// <summary>
        /// 选择单个实体
        /// </summary>
        /// <param name="feModel">Femap Model对象</param>
        /// <param name="dataType">选择对象类型</param>
        /// <param id="dataType">选择对象的ID</param>
        /// <returns>是否选中</returns>
        public static bool SelectEntity(model feModel,zDataType dataType,out int id)
        {
            Set set = null;
            id = -1;
            if (feModel != null)
            {
                set = feModel.feSet;
                set.Select(dataType, true, "请选择" + dataType.ToString() + "。");
                set.GetArray(out int numID,out object idArrayObj);
                if(numID>0)
                {
                    id = (idArrayObj as int[])[0];
                    return true;
                }
            }
            return false;
        }

        public static Set GetNodesAtSurface(int surfaceID, model feModel)
        {
            Set set = feModel.feSet;
            set.AddRule(surfaceID, zGroupDefinitionType.FGD_NODE_ATSURFACE);
            return set;
        }

        public List<int> GetEntityIDsFromSet(Set set)
        {
            set.GetArray(out int numID,out object idArrayObj);
            int[] idArray = idArrayObj as int[];
            return new List<int>(idArray);
        }

        public static Set CreateSet(int[] itemIDs, model feModel)
        {
            Set set = null;
            if (feModel!=null)
            {
                set = feModel.feSet;
                foreach (var item in itemIDs)
                {
                    set.Add(item);
                }
            }
            return set;
            
        }
    }
}
