using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FemapLib
{

    public class NeuOutputDataVectors1051 : BaseNeuData
    {
        //line1
        public string setID;
        public string vecID;
        public string one="1";
        //line2
        public string title;
        //line3
        public string min_val;
        public string max_val;
        public string abs_max;
        //line4
        public List<string> comp1;
        //line5
        public List<string> comp2;
        //line6
        public string doubleSidedContourVectorID;
        //line7
        public string id_min;
        public string id_max;
        /// <summary>
        /// 0=Any, 1=Disp, 2=Accel, 3=Force, 4=Stress, 5=Strain, 6=Temp, others=User)
        /// </summary>
        public string out_type;
        /// <summary>
        /// Either nodal (7) or elemental (8) output
        /// </summary>
        public string ent_type;
        /// <summary>
        /// 0=None, 1=Magnitude, 2=Average, 3=CornerAverage, 4=PrinStressA, 5=PrinStressB, 6=PrinStressC, 7=MaxShear,8=VonMises, 9=ComplexMagnitude
        /// </summary>
        public string compute_type;
        //line8
        public string calc_warn;
        public string comp_dir;
        public string cent_total;
        public string integer_format;
        //line9-1
        public string entityID;
        public string value;
        //line9-2
        public List<NeuOutputVectorValues> vectorValues;
        public NeuOutputDataVectors1051()
        {
            caeDataType = "Output Data Vectors";
            blockTypeID = "1051";
            comp1 = new List<string>();
            comp2 = new List<string>();
            vectorValues = new List<NeuOutputVectorValues>();
        }
        public override void UpdateRawTexts()
        {
            rawTexts = new List<string>();
            rawTexts.Add(setID+spli+vecID+spli+one);
            rawTexts.Add(title);
            rawTexts.Add(min_val + spli + max_val + spli + abs_max);
            rawTexts.Add(ConvertToNeuConnectStr(comp1));
            rawTexts.Add(ConvertToNeuConnectStr(comp2));
            rawTexts.Add(doubleSidedContourVectorID);
            rawTexts.Add(id_min + spli + id_max +spli+ out_type + spli + ent_type +spli+ compute_type);
            rawTexts.Add(calc_warn + spli + comp_dir +spli+ cent_total + spli + integer_format);
            foreach (NeuOutputVectorValues values in vectorValues)
            {
                List<string> valueRawTexts = values.ToRawTexts();
                rawTexts.AddRange(valueRawTexts);
            }
        }
        public void WriteDefaultValues(int studySetID,int vectorID,string titleI)
        {
            setID = studySetID.ToString();
            vecID = vectorID.ToString();
            title = titleI;
            min_val = "0";
            max_val = "0";
            abs_max = "0";
            comp1 = new List<string>() { "2", "3", "4", "0", "0", "0", "0", "0", "0", "0" };
            comp2 = new List<string>() { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
            doubleSidedContourVectorID = "0";
            id_min = "0";
            id_max = "0";
            out_type = "1";
            ent_type = "7";
            compute_type = "0";
            calc_warn = "1";
            comp_dir = "1";
            cent_total = "1";
            integer_format = "0";
        }

        public void AddDisplacementValues(string startID, string endID,List<string> values)
        {
            NeuOutputVectorValues newVectorValue = new NeuOutputVectorValues();
            newVectorValue.start_entityID = startID;
            newVectorValue.end_entityID = endID;
            newVectorValue.values = values;
            vectorValues.Add(newVectorValue);
        }
    }

    public class NeuOutputVectorValues
    {
        public string start_entityID;
        public string end_entityID;
        public List<string> values;
        public NeuOutputVectorValues()
        {
            values=new List<string>();
        }
        public List<string> ToRawTexts()
        {
            List<string> texts = new List<string>();
            string firstLine = start_entityID + BaseNeuData.spli + end_entityID ;
            for (int i = 0; i < 8&&i<values.Count; i++)
            {
                firstLine +=(BaseNeuData.spli + values[i]);
            }
            texts.Add(firstLine);
            int count = 1;
            string valueLine = "";
            for (int i = 8; i < values.Count; i++)
            {
                if (count == 1)
                {
                    valueLine += values[i];
                }
                else
                {
                    valueLine += (BaseNeuData.spli + values[i]);
                }
                if (i==values.Count-1||count == 10)
                {
                    texts.Add(valueLine);
                    valueLine = "";
                    count = 1;
                    continue;
                }
                count++;
            }
            return texts;
        }
    }

}
