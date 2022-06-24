using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FemapLib
{
    public enum MeshDataBlockType
    {
        Vertices, Edges, Triangles, Tetrahedra, Pyramids, Prisms
    }
    /// <summary>
    /// 达索3D Pricese Mesh生成的.mesh文件
    /// </summary>
    public class MeshReader : BaseCAEReader
    {
        public string meshVersionFormatted;
        public string dimension;
        public List<MeshDataBlock> MeshDataBlocks
        {
            get
            {
                List<MeshDataBlock> meshDataBlocks = new List<MeshDataBlock>();
                foreach (var dataBlock in dataBlocks)
                {
                    meshDataBlocks.Add(dataBlock as MeshDataBlock);
                }
                return meshDataBlocks;
            }
        }
        public MeshReader(string filePath) : base(filePath)
        {
            dataBlocks = new List<BaseCAEDataBlock>();
        }
        public void ReadAllMeshData()
        {
            Read();
            dataBlocks = new List<BaseCAEDataBlock>();
            meshVersionFormatted = lines.Find(q => q.Contains("MeshVersion"));
            dimension = lines.Find(q => q.Contains("Dimension"));
            //获取Vertices
            int verticesIndex = lines.IndexOf("Vertices");
            MeshDataBlock vertices = ReadVertices(verticesIndex);
            dataBlocks.Add(vertices);
            //获取Edges
            int edgesIndex = lines.IndexOf("Edges");
            MeshDataBlock edge = ReadEdges(edgesIndex);
            dataBlocks.Add(edge);
            //获取Triangles
            int trianglesIndex = lines.IndexOf("Triangles");
            MeshDataBlock triangle = ReadTriangles(trianglesIndex);
            dataBlocks.Add(triangle);
            //获取Triangles
            int TetrahedraIndex = lines.IndexOf("Tetrahedra");
            MeshDataBlock Tetrahedra = ReadTetrahedra(TetrahedraIndex);
            dataBlocks.Add(Tetrahedra);
            //获取Pyramids
            int pyramidIndex = lines.IndexOf(MeshDataBlockType.Pyramids.ToString());
            MeshDataBlock pyramids = ReadPyramids(pyramidIndex);
            dataBlocks.Add(pyramids);
            //获取Prisms
            int prismsIndex = lines.IndexOf(MeshDataBlockType.Prisms.ToString());
            MeshDataBlock prims = ReadPrisms(prismsIndex);
            dataBlocks.Add(prims);
        }
        public MeshDataBlock ReadVertices(int startIndex)
        {
            MeshDataBlock block = new MeshDataBlock();
            if (startIndex >= 0)
            {
                block.blockTitle = "Vertices";
                block.dataBlockType = "Vertices";
                block.dataNum = lines[startIndex + 1];
                int dataIndex = startIndex + 2;
                int count = 1;
                while (!string.IsNullOrEmpty(lines[dataIndex]))
                {
                    block.dataTexts.Add(lines[dataIndex]);
                    string[] verticesCoords = lines[dataIndex].Split(' ');
                    if (verticesCoords.Length >= 4)
                    {
                        MVertice newVertice = new MVertice();
                        newVertice.caeDataID = count.ToString();
                        newVertice.rawTexts.Add(lines[dataIndex]);
                        newVertice.x = verticesCoords[0];
                        newVertice.y = verticesCoords[1];
                        newVertice.z = verticesCoords[2];
                        newVertice.attriID = verticesCoords[3];
                        block.datas.Add(newVertice);
                    }
                    count++;
                    dataIndex++;
                }
            }
            return block;
        }
        public MeshDataBlock ReadEdges(int startIndex)
        {
            MeshDataBlock block = new MeshDataBlock();
            if (startIndex >= 0)
            {
                block.blockTitle = "Edges";
                block.dataBlockType = "Edges";
                block.dataNum = lines[startIndex + 1];
                int dataIndex = startIndex + 2;
                int count = 1;
                while (!string.IsNullOrEmpty(lines[dataIndex]))
                {
                    block.dataTexts.Add(lines[dataIndex]);
                    string[] splitedTexts = lines[dataIndex].Split(' ');
                    if (splitedTexts.Length >= 3)
                    {
                        MEdge data = new MEdge();
                        data.caeDataID = count.ToString();
                        data.rawTexts.Add(lines[dataIndex]);
                        data.vertice1ID = splitedTexts[0];
                        data.vertice2ID = splitedTexts[1];
                        data.attriID = splitedTexts[2];
                        block.datas.Add(data);
                    }
                    count++;
                    dataIndex++;
                }
            }
            return block;
        }
        public MeshDataBlock ReadTriangles(int startIndex)
        {
            MeshDataBlock block = new MeshDataBlock();
            if (startIndex >= 0)
            {
                block.blockTitle = "Triangles";
                block.dataBlockType = "Triangles";
                block.dataNum = lines[startIndex + 1];
                int dataIndex = startIndex + 2;
                int count = 1;
                while (!string.IsNullOrEmpty(lines[dataIndex]))
                {
                    block.dataTexts.Add(lines[dataIndex]);
                    string[] splitedTexts = lines[dataIndex].Split(' ');
                    if (splitedTexts.Length >= 4)
                    {
                        MTriangle data = new MTriangle();
                        data.caeDataID = count.ToString();
                        data.rawTexts.Add(lines[dataIndex]);
                        data.edge1ID = splitedTexts[0];
                        data.edge2ID = splitedTexts[1];
                        data.edge3ID = splitedTexts[2];
                        data.attriID = splitedTexts[3];
                        block.datas.Add(data);
                    }
                    count++;
                    dataIndex++;
                }
            }
            return block;
        }
        public MeshDataBlock ReadTetrahedra(int startIndex)
        {
            MeshDataBlock block = new MeshDataBlock();
            if (startIndex >= 0)
            {
                block.blockTitle = "Tetrahedra";
                block.dataBlockType = "Tetrahedra";
                block.dataNum = lines[startIndex + 1];
                int dataIndex = startIndex + 2;
                int count = 1;
                while (!string.IsNullOrEmpty(lines[dataIndex]))
                {
                    block.dataTexts.Add(lines[dataIndex]);
                    string[] splitedTexts = lines[dataIndex].Split(' ');
                    if (splitedTexts.Length >= 5)
                    {
                        MTerahedra data = new MTerahedra();
                        data.caeDataID = count.ToString();
                        data.rawTexts.Add(lines[dataIndex]);
                        data.vert1ID = splitedTexts[0];
                        data.vert2ID = splitedTexts[1];
                        data.vert3ID = splitedTexts[2];
                        data.vert4ID = splitedTexts[3];
                        data.attriID = splitedTexts[4];
                        block.datas.Add(data);
                    }
                    count++;
                    dataIndex++;
                }
            }
            return block;
        }
        public MeshDataBlock ReadPyramids(int startIndex)
        {
            MeshDataBlock block = new MeshDataBlock();
            if (startIndex >= 0)
            {
                block.blockTitle = MeshDataBlockType.Pyramids.ToString();
                block.dataBlockType = MeshDataBlockType.Pyramids.ToString();
                block.dataNum = lines[startIndex + 1];
                int dataIndex = startIndex + 2;
                int count = 1;
                while (!string.IsNullOrEmpty(lines[dataIndex]))
                {
                    block.dataTexts.Add(lines[dataIndex]);
                    string[] splitedTexts = lines[dataIndex].Split(' ');
                    if (splitedTexts.Length >= 6)
                    {
                        MPyramid data = new MPyramid();
                        data.caeDataID = count.ToString();
                        data.rawTexts.Add(lines[dataIndex]);
                        data.vert1ID = splitedTexts[0];
                        data.vert2ID = splitedTexts[1];
                        data.vert3ID = splitedTexts[2];
                        data.vert4ID = splitedTexts[3];
                        data.vert5ID = splitedTexts[4];
                        data.attriID = splitedTexts[5];
                        block.datas.Add(data);
                    }
                    count++;
                    dataIndex++;
                }
            }
            return block;
        }

        public MeshDataBlock ReadPrisms(int startIndex)
        {
            MeshDataBlock block = new MeshDataBlock();
            if (startIndex >= 0)
            {
                block.blockTitle = MeshDataBlockType.Prisms.ToString();
                block.dataBlockType = MeshDataBlockType.Prisms.ToString();
                block.dataNum = lines[startIndex + 1];
                int dataIndex = startIndex + 2;
                int count = 1;
                while (!string.IsNullOrEmpty(lines[dataIndex]))
                {
                    block.dataTexts.Add(lines[dataIndex]);
                    string[] splitedTexts = lines[dataIndex].Split(' ');
                    if (splitedTexts.Length >= 7)
                    {
                        MPrism data = new MPrism();
                        data.caeDataID = count.ToString();
                        data.rawTexts.Add(lines[dataIndex]);
                        data.vert1ID = splitedTexts[0];
                        data.vert2ID = splitedTexts[1];
                        data.vert3ID = splitedTexts[2];
                        data.vert4ID = splitedTexts[3];
                        data.vert5ID = splitedTexts[4];
                        data.vert6ID = splitedTexts[5];
                        data.attriID = splitedTexts[6];
                        block.datas.Add(data);
                    }
                    count++;
                    dataIndex++;
                }
            }
            return block;
        }
        public MeshDataBlock GetMeshDataBlockByType(MeshDataBlockType blockType)
        {
            return MeshDataBlocks.Find(q => q.dataBlockType == blockType.ToString());
        }
        public MVertice GetVerticeByID(string id)
        {
            MeshDataBlock VerticeBlock = GetMeshDataBlockByType(MeshDataBlockType.Vertices);
            if (VerticeBlock != null)
            {
                return VerticeBlock.GetData<MVertice>(id);
            }
            return null;
        }

    }

    public class MeshDataBlock : BaseCAEDataBlock
    {
        public string blockTitle;
        public string dataNum;
        public override List<string> ToRawTexts()
        {
            List<string> lines = new List<string>();
            lines.Add(blockTitle);
            lines.Add(dataNum);
            lines.AddRange(dataTexts);
            lines.Add("");
            return lines;
        }
    }

    public class MeshData : BaseCAEData
    {
        public string attriID;
    }

    public class MVertice : MeshData
    {
        public string x;
        public string y;
        public string z;
        public MVertice()
        {
            caeDataType = MeshDataBlockType.Vertices.ToString();
        }
        public override List<string> ToTranslateTexts()
        {
            List<string> lines = new List<string>();
            lines.Add("X：" + x);
            lines.Add("Y：" + y);
            lines.Add("Z：" + z);
            lines.Add("the attribute of element：" + attriID);
            return lines;
        }
        public override string ToNeuConnectStr(string split)
        {
            return x.ToString() + split + y.ToString() + split + z.ToString();
        }

    }

    public class MEdge : MeshData
    {
        public string vertice1ID;
        public string vertice2ID;
        public MEdge()
        {
            caeDataType = MeshDataBlockType.Edges.ToString();
        }
        public override List<string> ToTranslateTexts()
        {
            List<string> lines = new List<string>();
            lines.Add("vertice1ID：" + vertice1ID);
            lines.Add("vertice2ID：" + vertice2ID);
            lines.Add("the attribute of element：" + attriID);
            return lines;
        }
    }

    public class MTriangle : MeshData
    {
        public string edge1ID;
        public string edge2ID;
        public string edge3ID;
        public MTriangle()
        {
            caeDataType = MeshDataBlockType.Triangles.ToString();
        }
        public override List<string> ToTranslateTexts()
        {
            List<string> lines = new List<string>();
            lines.Add("edge1ID：" + edge1ID);
            lines.Add("edge2ID：" + edge2ID);
            lines.Add("edge3ID：" + edge3ID);
            lines.Add("the attribute of element：" + attriID);
            return lines;
        }
    }

    public class MTerahedra : MeshData
    {
        public string vert1ID;
        public string vert2ID;
        public string vert3ID;
        public string vert4ID;
        public MTerahedra()
        {
            caeDataType = MeshDataBlockType.Tetrahedra.ToString();
        }
        public override List<string> ToTranslateTexts()
        {
            List<string> lines = new List<string>();
            lines.Add("vertices1ID：" + vert1ID);
            lines.Add("vertices2ID：" + vert2ID);
            lines.Add("vertices3ID：" + vert3ID);
            lines.Add("vertices4ID：" + vert4ID);
            lines.Add("the attribute of element：" + attriID);
            return lines;
        }
        public override string ToNeuConnectStr(string split)
        {
            return vert1ID + split + vert2ID + split + vert3ID + split + vert4ID;
        }

    }

    public class MPyramid : MeshData
    {
        public string vert1ID;
        public string vert2ID;
        public string vert3ID;
        public string vert4ID;
        public string vert5ID;
        public MPyramid()
        {
            caeDataType = MeshDataBlockType.Pyramids.ToString();
        }
        public override List<string> ToTranslateTexts()
        {
            List<string> lines = new List<string>();
            lines.Add("vertices1ID：" + vert1ID);
            lines.Add("vertices2ID：" + vert2ID);
            lines.Add("vertices3ID：" + vert3ID);
            lines.Add("vertices4ID：" + vert4ID);
            lines.Add("vertices5ID：" + vert5ID);
            lines.Add("the attribute of element：" + attriID);
            return lines;
        }
        public override string ToNeuConnectStr(string split)
        {
            return vert1ID + split + vert2ID + split + vert3ID + split + vert4ID + split + vert5ID;
        }
    }

    public class MPrism : MeshData
    {
        public string vert1ID;
        public string vert2ID;
        public string vert3ID;
        public string vert4ID;
        public string vert5ID;
        public string vert6ID;
        public MPrism()
        {
            caeDataType = MeshDataBlockType.Prisms.ToString();
        }
        public override List<string> ToTranslateTexts()
        {
            List<string> lines = new List<string>();
            lines.Add("vertices1ID：" + vert1ID);
            lines.Add("vertices2ID：" + vert2ID);
            lines.Add("vertices3ID：" + vert3ID);
            lines.Add("vertices4ID：" + vert4ID);
            lines.Add("vertices5ID：" + vert5ID);
            lines.Add("vertices6ID：" + vert6ID);
            lines.Add("the attribute of element：" + attriID);
            return lines;
        }
        public override string ToNeuConnectStr(string split)
        {
            return vert1ID + split + vert2ID + split + vert3ID + split + vert4ID + split + vert5ID + split + vert6ID;
        }
    }
}

