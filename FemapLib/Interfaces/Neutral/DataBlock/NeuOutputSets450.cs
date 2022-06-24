using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FemapLib
{
    public class NeuOutputSets450 : BaseNeuData
    {
        //line1
        public string ID;
        //line2
        public string title;
        //line3
        public string from_prog;
        public string anal_type;
        public string processType;
        public string integerFormat;
        //line4
        public string value;
        //line5
        public string nlines;
        public List<string> notes;
        //line6
        public string attachID;
        public string locationID;
        public string studyID;
        public string dataType;
        public string setID;
        public string factor;
        //line7
        public string nas_case;
        public string nas_rev;
        //line8
        public string use_oc_orient;
        //line9
        public string uSolidIsotropic;
        public string uSolidAnisotropic;
        public string uSolidHyperelastic;
        //line10
        public string uTria3Stress;
        public string uTria3Strain;
        public string uTria3Force;
        public string uTria6Stress;
        public string uTria6Strain;
        public string uTria6Force;
        //line11
        public string uQuad4Stress;
        public string uQuad4Strain;
        public string uQuad4Force;
        public string uQuad8Stress;
        public string uQuad8Strain;
        public string uQuad8Force;

        public NeuOutputSets450()
        {
            caeDataType = "Output Sets";
            blockTypeID = "450";
            notes = new List<string>();
        }

        public string ToLine3String()
        {
            return from_prog +  spli + anal_type + spli + processType + spli+ integerFormat;
        }
        public List<string> ToLine5Strings()
        {
            List<string> lines = new List<string>();
            lines.Add(nlines);
            lines.AddRange(notes);
            return lines;
        }
        public string ToLine6String()
        {
            return attachID + spli + locationID + spli + studyID;
        }
        public string ToLine7String()
        {
            return dataType + spli + setID + spli + factor;
        }
        public string ToLine8String()
        {
            return nas_case + spli + nas_rev;
        }
        public string ToLine9String()
        {
            return use_oc_orient;
        }
        public string ToLine10String()
        {
            return uSolidIsotropic + spli + uSolidAnisotropic + spli + uSolidHyperelastic;
        }
        public string ToLine11String()
        {
            return uTria3Stress + spli + uTria3Strain + spli + uTria3Force + spli + uTria6Stress + spli + uTria6Strain + spli + uTria6Force;
        }
        public string ToLine12String()
        {
            return uQuad4Stress + spli + uQuad4Strain + spli + uQuad4Force + spli + uQuad8Stress + spli + uQuad8Strain + spli + uQuad8Force;
        }
        public List<string> ToBlockStrings()
        {
            List<string> texts = new List<string>();
            texts.Add(NeuReader.blockBoundary);
            texts.Add(blockTypeID);
            texts.Add(ID);
            texts.Add(title);
            texts.Add(ToLine3String());
            texts.AddRange(ToLine5Strings());
            texts.Add(ToLine6String());
            texts.Add(ToLine7String());
            texts.Add(ToLine8String());
            texts.Add(ToLine9String());
            texts.Add(ToLine10String());
            texts.Add(ToLine11String());
            texts.Add(ToLine12String());
            texts.Add(NeuReader.blockBoundary);
            return texts;
        }

        public void WriteDefaultValues(int idI,string titleI,int studyIDI)
        {
            ID = idI.ToString();
            title = titleI;
            from_prog = "0";
            anal_type = "1";
            processType = "0";
            integerFormat = "0";
            value = "0";
            nlines = "0";
            notes = new List<string>();
            attachID = "0";
            locationID = "0";
            studyID = studyIDI.ToString();
            caeDataType = "0";
            dataType="-1";
            setID = "-1";
            factor = "0";
            nas_case = "0";
            nas_rev = "0";
            use_oc_orient = "0";
            uSolidIsotropic = "0";
            uSolidAnisotropic = "0";
            uSolidHyperelastic = "0";
            uTria3Stress = "0";
            uTria3Strain = "0";
            uTria3Force = "0";
            uTria6Stress = "0";
            uTria6Strain = "0";
            uTria6Force = "0";
            uQuad4Stress = "0";
            uQuad4Strain = "0";
            uQuad4Force = "0";
            uQuad8Stress = "0";
            uQuad8Strain = "0";
            uQuad8Force = "0";
        }
        public override void UpdateRawTexts()
        {
            rawTexts = new List<string>();
            rawTexts.Add(ID);
            rawTexts.Add(title);
            rawTexts.Add(ToLine3String());
            rawTexts.Add(value);
            rawTexts.AddRange(ToLine5Strings());
            rawTexts.Add(ToLine6String());
            rawTexts.Add(ToLine7String());
            rawTexts.Add(ToLine8String());
            rawTexts.Add(ToLine9String());
            rawTexts.Add(ToLine10String());
            rawTexts.Add(ToLine11String());
            rawTexts.Add(ToLine12String());
        }
    }
}
