using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FemapLib
{
    public class NeuElement404: BaseNeuData
    {

        //第1行
        //ID在父类中
        public string color;
        public string propID;
        public string type;
        public string topology;
        public string layer;
        public string orientID;
        public string matl_orflag;
        public string geomID;
        public string formulation;
        public List<string> contactSegment;
        public string formulation2;
        public List<string> MaterialCSys;
        //第2行
        public string formulation3;
        public List<string> keyopt;
        //第3行+第4行
        public List<string> node;
        //第5行
        public List<string> orient;
        public List<string> warpingNode;
        public List<string> connectTYPE;
        public List<string> connectSEG;
        //第6行
        public List<string> offset1;
        //第7行
        public List<string> offset2;
        //第8行 Not Spring type with CBUSH
        public string releaseFormat;
        public List<string> release1;
        public List<string> release2;
        public List<string> nodelist;
        //第8行 Sptring type with CBUSH
        public string SpringTypeReleaseFormat;
        public string springElemLocation;
        public string springLocation;
        public string springPropLocation;
        public string springUseElCid;
        public string springNoOrient;
        public List<string> SpringNodelist;
        //第9行（根据上述部分非0值产生）
        public string nodeID;
        public string faceID;
        public string weight;
        public List<string> dof;
        public string genelList;
        public string Row;
        public string Column;
        public string Value;
        //其他数据
        public bool isSpring;
        public static int elementTextRows = 9;
        public NeuElement404(NeuDataBlock owningBlockI, List<string> rawTextsI,bool cleanText=true,bool analyzeData=true)
        {
            //父对象初始化信息%
            elementTextRows = 9;
            assocNode = null;
            owningBlock = owningBlockI;
            caeDataType = "Element";
            blockTypeID = "404";
            rawTexts = rawTextsI;
            //处理texts
            if(cleanText)
            {
                CleanTexts();
            }
            if(analyzeData)
            {
                if (rawTexts.Count == elementTextRows)
                {
                    UpdateRow1Fields(rawTexts[0]);
                    UpdateRow2Fields(rawTexts[1]);
                    UpdateRow3Fields(rawTexts[2]);
                    UpdateRow4Fields(rawTexts[3]);
                    UpdateRow5Fields(rawTexts[4]);
                    UpdateRow6Fields(rawTexts[5]);
                    UpdateRow7Fields(rawTexts[6]);
                    UpdateRow8Fields(rawTexts[7]);
                }
                else
                {
                    throw new Exception(caeDataType + "初始化失败，输入的文本行数量不为" + elementTextRows + "，不支持当前文本解析。");
                }
            }
        }

        public void UpdateRow1Fields(string text)
        {
            string[] fields = text.Split(',');
            if(fields.Length>=15)
            {
                caeDataID = fields[0];
                color = fields[1];
                propID = fields[2];
                type = fields[3];
                topology = fields[4];
                layer = fields[5];
                int matl_orflagIndex = 6;
                if(fields.Length==16)
                {
                    orientID = fields[6];
                    matl_orflagIndex = 7;
                }
                
                matl_orflag = fields[matl_orflagIndex];
                geomID = fields[matl_orflagIndex+1];
                formulation = fields[matl_orflagIndex+2];
                contactSegment = new List<string>();
                contactSegment.Add(fields[matl_orflagIndex +3]);
                contactSegment.Add(fields[matl_orflagIndex +4]);
                formulation2 = fields[matl_orflagIndex +5];
                MaterialCSys = new List<string>();
                MaterialCSys.Add(fields[matl_orflagIndex +5]);
                MaterialCSys.Add(fields[matl_orflagIndex +6]);
               MaterialCSys.Add(fields[matl_orflagIndex +7]);
            }

        }
        /// <summary>
        /// formulation3 and keyopts[0,11]
        /// </summary>
        /// <param name="text"></param>
        public void UpdateRow2Fields(string text)
        {
            string[] fields = text.Split(',');
            if (fields.Length == 12)
            {
                formulation3 = fields[0];
                keyopt = new List<string>();
                keyopt.Add(fields[1]);
                keyopt.Add(fields[2]);
                keyopt.Add(fields[3]);
                keyopt.Add(fields[4]);
                keyopt.Add(fields[5]);
                keyopt.Add(fields[6]);
                keyopt.Add(fields[7]);
                keyopt.Add(fields[8]);
                keyopt.Add(fields[9]);
                keyopt.Add(fields[10]);
                keyopt.Add(fields[11]);
            }

        }
        /// <summary>
        /// nodes 0~9
        /// </summary>
        /// <param name="text"></param>
        public void UpdateRow3Fields(string text)
        {
            string[] fields = text.Split(',');
            if (fields.Length == 10)
            {
                node = new List<string>(fields);
            }
        }
        /// <summary>
        /// nodes 10~19
        /// </summary>
        /// <param name="text"></param>
        public void UpdateRow4Fields(string text)
        {
            string[] fields = text.Split(',');
            if (fields.Length == 10)
            {
                node.AddRange(fields);
            }
        }

        public void UpdateRow5Fields(string text)
        {
            string[] fields = text.Split(',');
            if (fields.Length == 9)
            {
                orient = new List<string>();
                orient.Add(fields[0]);
                orient.Add(fields[1]);
                orient.Add(fields[2]);
                warpingNode = new List<string>();
                warpingNode.Add(fields[0]);
                warpingNode.Add(fields[1]);
                connectTYPE = new List<string>();
                connectTYPE.Add(fields[0]);
                connectTYPE.Add(fields[1]);
                connectSEG = new List<string>();
                connectSEG.Add(fields[0]);
                connectSEG.Add(fields[1]);
            }
        }

        public void UpdateRow6Fields(string text)
        {
            string[] fields = text.Split(',');
            if (fields.Length ==3)
            {
                offset1 = new List<string>();
                foreach (var field in fields)
                {
                    offset1.Add(field);
                }
            }
        }
        public void UpdateRow7Fields(string text)
        {
            string[] fields = text.Split(',');
            if (fields.Length == 3)
            {
                offset2 = new List<string>(fields);
            }
        }
        public void UpdateRow8Fields(string text)
        {
            string[] fields = text.Split(',');
            isSpring = fields.Length == 10;
            if (fields.Length == 17)
            {
                releaseFormat = fields[0];
                release1 = new List<string>();
                release1.Add(fields[1]);
                release1.Add(fields[2]);
                release1.Add(fields[3]);
                release1.Add(fields[4]);
                release1.Add(fields[5]);
                release1.Add(fields[6]);
                release2 = new List<string>();
                release2.Add(fields[7]);
                release2.Add(fields[8]);
                release2.Add(fields[9]);
                release2.Add(fields[10]);
                release2.Add(fields[11]);
                release2.Add(fields[12]);
                nodelist = new List<string>();
                nodelist.Add(fields[13]);
                nodelist.Add(fields[14]);
                nodelist.Add(fields[15]);
                nodelist.Add(fields[16]);
            }
            else if (fields.Length==10)
            {
                SpringTypeReleaseFormat = fields[0];
                springElemLocation = fields[1];
                springLocation = fields[2];
                springPropLocation = fields[3];
                springUseElCid = fields[4];
                springNoOrient = fields[5];
                SpringNodelist = new List<string>();
                SpringNodelist.Add(fields[6]);
                SpringNodelist.Add(fields[7]);
                SpringNodelist.Add(fields[8]);
                SpringNodelist.Add(fields[9]);
            }
        }
        public override List<string> ToTranslateTexts()
        {
            List<string> texts = new List<string>();
            texts.Add("ID：" + caeDataID);
            texts.Add("color：" + color);
            texts.Add("propID：" + propID);
            texts.Add("type：" + type);
            texts.Add("topology：" + topology);
            texts.Add("layer：" + layer);
            if(!string.IsNullOrEmpty(orientID))
                texts.Add("orientID：" + orientID);
            texts.Add("matl_orflag：" + matl_orflag);
            texts.Add("geomID：" + geomID);
            texts.Add("formulation：" + formulation);
            texts.Add("contact segment：" +Common.ToString(contactSegment));
            texts.Add("formulation2：" + formulation2);
            texts.Add("MaterialCSys：" + Common.ToString(MaterialCSys));
            texts.Add("formulation3：" + formulation3);
            texts.Add("keyopt：" + Common.ToString(keyopt));
            texts.Add("node：" + Common.ToString(node));
            texts.Add("orient：" + Common.ToString(orient));
            texts.Add("warping node：" + Common.ToString(warpingNode));
            texts.Add("connectTYPE：" + Common.ToString(connectTYPE));
            texts.Add("connectSEG：" + Common.ToString(connectSEG));
            texts.Add("offset1：" + Common.ToString(offset1));
            texts.Add("offset2：" + Common.ToString(offset2));
            if(!isSpring)
            {
                texts.Add("releaseFormat：" + releaseFormat);
                texts.Add("release1：" + Common.ToString(release1));
                texts.Add("release2：" + Common.ToString(release2));
                texts.Add("nodelist：" + Common.ToString(nodelist));
            }
            else
            {
                texts.Add("releaseFormat：" + releaseFormat);
                texts.Add("springElemLocation：" + springElemLocation);
                texts.Add("springLocation：" + springLocation);
                texts.Add("springPropLocation：" + springPropLocation);
                texts.Add("springUseElCid：" + springUseElCid);
                texts.Add("springNoOrient：" + springNoOrient);
                texts.Add("SpringNodelist：" + Common.ToString(SpringNodelist));
            }
            if(!string.IsNullOrEmpty(nodeID))
            texts.Add("nodeID：" + nodeID);
            if (!string.IsNullOrEmpty(faceID))
                texts.Add("faceID：" + faceID);
            if (!string.IsNullOrEmpty(weight))
                texts.Add("weight：" + weight);
            if (dof!=null&& dof.Count!=0)
                texts.Add("dof：" + Common.ToString(dof));
            if (!string.IsNullOrEmpty(genelList))
                texts.Add("genelList：" + genelList);
            if (!string.IsNullOrEmpty(Row))
                texts.Add("Row：" + Row);
            if (!string.IsNullOrEmpty(Column))
                texts.Add("Column：" + Column);
            if (!string.IsNullOrEmpty(Value))
                texts.Add("Value (factor)：" + Value);
            return texts;
        }


    }

}
