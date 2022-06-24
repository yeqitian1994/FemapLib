using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FemapLib
{
    public class BaseNeuData: BaseCAEData
    {
        /// <summary>
        /// 所属DataBlock
        /// </summary>
        public string blockTypeID;
        public static char spli = ',';
        public string ConvertToNeuConnectStr(List<string> texts)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(texts[0]);
            for (int i = 1; i < texts.Count; i++)
            {
                sb.Append(spli+texts[i]);
            }
            return sb.ToString();
        }


    }
    public class NeuDataBlock : BaseCAEDataBlock
    {
        public string id;
        public string CleanID
        {
            get
            {
                if (!string.IsNullOrEmpty(id))
                {
                    return id.Trim();
                }
                else
                {
                    return "";
                }
            }
        }
        public int ID
        {
            get
            {
                if (int.TryParse(id, out int result))
                {
                    return result;
                }
                else
                {
                    return -1;
                }
            }
        }
        public NeuDataBlock()
        {
            dataTexts = new List<string>();
            datas = new List<BaseCAEData>();
        }
        public NeuDataBlock(BaseNeuData data)
        {
            dataTexts = new List<string>();
            datas = new List<BaseCAEData>();
            datas.Add(data);
        }
        public override List<string> ToRawTexts()
        {
            List<string> lines = new List<string>();
            lines.Add(NeuReader.blockBoundary);
            lines.Add(id);
            foreach (BaseNeuData data in datas)
            {
                lines.AddRange(data.rawTexts);
            }
            lines.Add(NeuReader.blockBoundary);
            return lines;
        }
    }
}
