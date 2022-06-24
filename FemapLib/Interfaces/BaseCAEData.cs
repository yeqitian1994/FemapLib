using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FemapLib;

namespace FemapLib
{
    public class BaseCAEReader:FileOper
    {
        public BaseCAEReader(string filePath):base(filePath)
        {

        }
        public List<BaseCAEDataBlock> dataBlocks;

        public virtual List<BaseCAEDataBlock> GetCAEDataBlocks()
        {
            return dataBlocks;
        }
        public T GetDataBlock<T>(TreeNode node) where T:BaseCAEDataBlock
        {
            if (node != null && node.Parent == null)
            {
                return dataBlocks.Find(q => q.assocNode == node) as T;
            }
            else
            {
                return null;
            }
        }

    }

    public class BaseCAEDataBlock
    {
        public List<BaseCAEData> datas;
        public List<string> dataTexts;
        public string dataBlockType;
        public TreeNode assocNode;
        public BaseCAEDataBlock()
        {
            datas = new List<BaseCAEData>();
            dataTexts = new List<string>();
        }
        public virtual List<string> ToRawTexts()
        {
            return new List<string>();
        }
        public T GetData<T>(TreeNode node)where T:BaseCAEData
        {
            if (node != null)
            {
                return datas.Find(q => q.assocNode == node ) as T;
            }
            else
            {
                return null;
            }
        }
        public T GetData<T>(string id)where T:BaseCAEData
        {
            return datas.Find(q => q.caeDataID == id) as T;
        }

    }
    public class BaseCAEData
    {
        public string caeDataID;
        public string caeDataType;
        public TreeNode assocNode;
        public List<string> rawTexts;
        public BaseCAEDataBlock owningBlock;
        /// <summary>
        /// 临时和其他接口数据关联的ID
        /// </summary>
        public string tempAssocDataID;
        public BaseCAEData()
        {
            rawTexts = new List<string>();
        }

        public virtual List<string> ToTranslateTexts()
        {
            return new List<string>();
        }
        public virtual void UpdateRawTexts()
        {

        }
        public virtual string ToNeuConnectStr(string split)
        {
            return "";
        }
        public void CleanTexts()
        {
            for (int i = 0; i < rawTexts.Count; i++)
            {
                if (rawTexts[i].EndsWith(","))
                {
                    rawTexts[i] = rawTexts[i].Substring(0, rawTexts[i].Length - 1);
                }
            }
        }



    }
}
