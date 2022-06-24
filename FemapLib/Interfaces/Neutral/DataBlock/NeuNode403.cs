using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FemapLib
{
    public class NeuNode403 : BaseNeuData
    {
        //ID在父类中
        public string define_sys;
        public string output_sys;
        public string layer;
        public string color;
        public List<string> permbc;
        public string x;
        public string y;
        public string z;
        public string node_type;
        public string superelementID;
        //其他数据
        public static int elementTextRows = 1;
        public NeuNode403(NeuDataBlock owningBlockI, List<string> rawTextsI)
        {
            //父对象初始化信息
            elementTextRows =1;
            assocNode = null;
            owningBlock = owningBlockI;
            caeDataType = "Node";
            blockTypeID = "403";
            rawTexts = rawTextsI;
            //处理texts
            CleanTexts();
            if (rawTexts.Count == elementTextRows)
            {
                UpdateRow1Fields(rawTexts[0]);
            }
            else
            {
                throw new Exception(caeDataType+"初始化失败，输入的文本行数量不为"+ elementTextRows + "，不支持当前文本解析。");
            }
        }
        public void UpdateRow1Fields(string text)
        {
            string[] fields = text.Split(',');
            if (fields.Length >= 16)
            {
                caeDataID = fields[0];
                define_sys = fields[1];
                output_sys = fields[2];
                layer = fields[3];
                color = fields[4];
                permbc = new List<string>();
                permbc.Add(fields[5]);
                permbc.Add(fields[6]);
                permbc.Add(fields[7]);
                permbc.Add(fields[8]);
                permbc.Add(fields[9]);
                permbc.Add(fields[10]);
                x = fields[11];
                y = fields[12];
                z = fields[13];
                node_type = fields[14];
                superelementID = fields[15];
            }
        }

        public override List<string> ToTranslateTexts()
        {
            List<string> texts = new List<string>();
            texts.Add("ID：" + caeDataID);
            texts.Add("define_sys：" + define_sys);
            texts.Add("output_sys：" + output_sys);
            texts.Add("layer：" + layer);
            texts.Add("color：" + color);
            texts.Add("permbc：" + Common.ToString(permbc));
            texts.Add("x：" + x);
            texts.Add("y：" + y);
            texts.Add("z：" + z);
            texts.Add("node_type：" + node_type);
            texts.Add("superelementID：" + superelementID);
            return texts;
        }
    }
}
