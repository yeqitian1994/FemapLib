using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FemapLib
{
    public class NeuIDTypeReader:ConfigFileOper
    {
        public List<NeuIDType> idTypes;
        public List<NeuEleShapeType> eleShapeTypes;

        const string idTypeStartBorder = "NEU_ID_TYPE_START";
        const string idTypeEndBorder = "NEU_ID_TYPE_END";
        const string shapeTypeStartBorder = "NEU_ELEMENT_SHAPE_START";
        const string shapeTypeEndBorder = "NEU_ELEMENT_SHAPE_END";

        public NeuIDTypeReader(string filePath):base(filePath)
        {
            idTypes = new List<NeuIDType>();
            eleShapeTypes = new List<NeuEleShapeType>();
        }
        public void ReadAllIDTypes()
        {
            Read();
            idTypes = new List<NeuIDType>();
            SConfigBlock idTypeBlock= GetConfigBlock(idTypeStartBorder, idTypeEndBorder);
            List<List<string>> idTypeBlockVals = idTypeBlock.GetSplitedInfos();
            foreach (List<string> typeInfo in idTypeBlockVals)
            {
                if(typeInfo.Count>=4)
                {
                    NeuIDType newType = new NeuIDType();
                    newType.id = typeInfo[0];
                    newType.type = typeInfo[1];
                    newType.canWrite = typeInfo[2];
                    newType.canRead = typeInfo[3];
                    idTypes.Add(newType);
                }
            }
            //Shape Type
            SConfigBlock shapeTypeBlock = GetConfigBlock(shapeTypeStartBorder, shapeTypeEndBorder);
            List<List<string>> shapeTypeBlockVals = shapeTypeBlock.GetSplitedInfos('=');
            foreach (List<string> shapeInfo in shapeTypeBlockVals)
            {
                if (shapeInfo.Count >= 2)
                {
                    NeuEleShapeType newType = new NeuEleShapeType();
                    newType.id = shapeInfo[0];
                    newType.shapeType = shapeInfo[1];
                    eleShapeTypes.Add(newType);
                }
            }
        }
        public NeuIDType GetNeuIDType(string id)
        {
            if(idTypes!=null)
            {
                return idTypes.Find(q => q.id == id);
            }
            else
            {
                return null;
            }
        }
        public string GetEleShapeName(string id)
        {
            if(eleShapeTypes!=null)
            {
                NeuEleShapeType shapeType = eleShapeTypes.Find(q => q.id == id);
                if(shapeType!=null)
                {
                    return shapeType.shapeType;
                }
            }
            return "";
        }

    }
    public class NeuEleShapeType
    {
        public string id;
        public string shapeType;
    }
    public class NeuIDType
    {
        public string id;
        public string type;
        public string canWrite;
        public string canRead;
        public List<FieldInfo> fields;
        public NeuIDType()
        {
            fields = new List<FieldInfo>();
        }
    }
    public class FieldInfo
    {
        public string field;
        public string description;
        public string size;
        public string[] ToStrings()
        {
            return new string[] { field, description, size };
        }
    }
}
