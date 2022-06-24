using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FemapLib
{
    public class NeuIDTypeDescReader : ConfigFileOper
    {
        const string startBorder = "NEU_ID_TYPE_DESCRIPTION_START_";
        const string endBorder = "NEU_ID_TYPE_DESCRIPTION_END_";

        public NeuIDTypeDescReader(string filePath):base(filePath)
        {
        }

        public List<FieldInfo> GetFiledInfos(string id)
        {
            List<FieldInfo> fields = new List<FieldInfo>();
            SConfigBlock block= GetConfigBlock(startBorder + id, endBorder + id);
            if(block.lines.Count>0)
            {
                List<List<string>> fieldTexts=block.GetSplitedInfos('|');
                foreach (List<string> fieldText in fieldTexts)
                {
                    FieldInfo newField = new FieldInfo();
                    newField.field = fieldText[0];
                    if (fieldText.Count > 1)
                    {
                        newField.description = fieldText[1];
                    }
                    if (fieldText.Count > 2)
                    {
                        newField.size = fieldText[2];
                    }
                    fields.Add(newField);
                }
            }
            return fields;
        }

        public void Update(ref NeuIDTypeReader typeReader)
        {
            if(typeReader!=null)
            {
                for (int i = 0; i < typeReader.idTypes.Count; i++)
                {
                    NeuIDType idType = typeReader.idTypes[i];
                    string id = idType.id;
                    SConfigBlock block = GetConfigBlock(startBorder + id, endBorder + id);
                    idType.fields = new List<FieldInfo>();
                    if (block.lines.Count > 0)
                    {
                        List<List<string>> fieldTexts = block.GetSplitedInfos('|');
                        foreach (List<string> fieldText in fieldTexts)
                        {
                            FieldInfo newField = new FieldInfo();
                            newField.field = fieldText[0];
                            if (fieldText.Count > 1)
                            {
                                newField.description = fieldText[1];
                            }
                            if (fieldText.Count > 2)
                            {
                                newField.size = fieldText[2];
                            }
                            idType.fields.Add(newField);
                        }
                    }
                }
            }
        }

    }

}
