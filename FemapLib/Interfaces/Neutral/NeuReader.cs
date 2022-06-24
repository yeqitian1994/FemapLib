using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace FemapLib
{
    public class NeuReader: BaseCAEReader
    {
        public const string blockBoundary = "   -1";

        public List<NeuDataBlock> NeuDataBlock
        {
            get
            {
                List<NeuDataBlock> neuDataBlocks = new List<NeuDataBlock>();
                foreach (var dataBlock in dataBlocks)
                {
                    neuDataBlocks.Add(dataBlock as NeuDataBlock);
                }
                return neuDataBlocks;
            }
        }

        public NeuReader(string filePathI):base(filePathI)
        {
            dataBlocks = new List<BaseCAEDataBlock>();
        }
        public string filePath;



        public void ReadAllDataBlocks()
        {
            Read();
            dataBlocks = new List<BaseCAEDataBlock>();
            int curIndex = 0;
            while(curIndex>=0&&curIndex<lineNum)
            {
                if(GetDataBlockStartAndEnd(curIndex,out int startindex,out int endIndex))
                {
                    NeuDataBlock newBlock = new NeuDataBlock();
                    newBlock.id = lines[startindex + 1];
                    int dataLineNums = endIndex - startindex - 2;
                    if(dataLineNums>0)
                    {
                        newBlock.dataTexts = lines.GetRange(startindex + 2, dataLineNums);
                    }
                    dataBlocks.Add(newBlock);
                    //elements
                    if(newBlock.CleanID=="403")
                    {
                        for (int i = 0; i < newBlock.dataTexts.Count ; i += NeuNode403.elementTextRows)
                        {
                            List<string> dataTexts = newBlock.dataTexts.GetRange(i, 1);
                            NeuNode403 newNode = new NeuNode403(newBlock, dataTexts);
                            newBlock.datas.Add(newNode);
                        }
                    }
                    else if(newBlock.CleanID=="404")
                    {
                        for (int i = 0; i < newBlock.dataTexts.Count && newBlock.dataTexts.Count-i>=9; i+= NeuElement404.elementTextRows)
                        {
                            List<string> dataTexts = newBlock.dataTexts.GetRange(i, NeuElement404.elementTextRows);
                            NeuElement404 newElement = new NeuElement404(newBlock, dataTexts);
                            newBlock.datas.Add(newElement);
                        }
                    }
                    curIndex = endIndex + 1;
                }
                else
                {
                    break;
                }
            }
        }
        public bool GetDataBlockStartAndEnd(int startSearchIndex,out int startIndex,out int endIndex)
        {
            startIndex = -1;
            endIndex = -1;
            for (int i = startSearchIndex; i < lineNum; i++)
            {
                if (lines[i] == blockBoundary)
                {
                    startIndex = i;
                    break;
                }
            }
            if(startIndex>=0)
            {
                for (int i = startIndex+1; i < lineNum; i++)
                {
                    if(lines[i]== blockBoundary)
                    {
                        endIndex = i;
                        break;
                    }
                }
            }
            return startIndex >= 0 && endIndex > startIndex;
        }
        public void UpdateType(NeuIDTypeReader idTypeReader)
        {
            if(idTypeReader!=null)
            {
                foreach (var dataBlock in NeuDataBlock)
                {
                    if(!string.IsNullOrEmpty(dataBlock.id))
                    {
                        NeuIDType idType= idTypeReader.GetNeuIDType(dataBlock.id.Trim());
                        if(idType!=null)
                        {
                            dataBlock.dataBlockType = idType.type;
                        }
                        else
                        {
                            dataBlock.dataBlockType = "<Missing Type> Id:" + dataBlock.id;
                        }
                    }
                }
            }
        }
        public void SaveAs(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            StreamWriter WriterFileInfo = new StreamWriter(filePath, false, Encoding.Default);
            foreach (var dataBlock in dataBlocks)
            {
                List<string> lines = dataBlock.ToRawTexts();
                foreach (string line in lines)
                {
                    WriterFileInfo.WriteLine(line);
                }
            }
            WriterFileInfo.Close();
        }
        public NeuDataBlock GetNeuDataBlock(string blockID)
        {
            if (NeuDataBlock != null&&!string.IsNullOrEmpty(blockID))
            {
                return NeuDataBlock.Find(q => q.CleanID == blockID.ToString());
            }
            else
            {
                return null;
            }
        }
        public Dictionary<string, string> verticeNodeDic;
        public void UpdateNodesAndElements(MeshReader meshReader,ProgressBar progBar=null)
        {
            MeshDataBlock verticeBlock = meshReader.GetMeshDataBlockByType(MeshDataBlockType.Vertices);
            MeshDataBlock teraBlock = meshReader.GetMeshDataBlockByType(MeshDataBlockType.Tetrahedra);
            MeshDataBlock pyramidBlock = meshReader.GetMeshDataBlockByType(MeshDataBlockType.Pyramids);
            MeshDataBlock prismBlock= meshReader.GetMeshDataBlockByType(MeshDataBlockType.Prisms);
            int totalNum = teraBlock.datas.Count+pyramidBlock.datas.Count+ prismBlock.datas.Count;
            int curNum = 0;
            verticeNodeDic = new Dictionary<string, string>();
            //Nodes
            NeuDataBlock nodeDataBlock = NeuDataBlock.Find(q => q.CleanID == "403");
            if(nodeDataBlock!=null)
            {
                nodeDataBlock.dataTexts = new List<string>();
                nodeDataBlock.datas = new List<BaseCAEData>();
                int id = 2451;
                
                if(verticeBlock!=null)
                {
                    foreach (MVertice vertice in verticeBlock.datas)
                    {
                        string nodeText = id.ToString() + ",0,0,1,46,0,0,0,0,0,0," + vertice.ToNeuConnectStr(",") + ",0,0";
                        NeuNode403 newNode = new NeuNode403(nodeDataBlock, new List<string>() { nodeText });
                        newNode.caeDataID = id.ToString();
                        newNode.tempAssocDataID = vertice.caeDataID;
                        nodeDataBlock.dataTexts.Add(nodeText);
                        nodeDataBlock.datas.Add(newNode);
                        verticeNodeDic.Add(vertice.caeDataID, newNode.caeDataID);
                        //curNum++;
                        id++;
                        //if (progBar!=null)
                        //{
                        //    progBar.Value = (int)((double)curNum / (double)totalNum * progBar.Maximum);
                        //}
                       
                    }
                }
            }
            //Elements
            NeuDataBlock eleDataBlock = NeuDataBlock.Find(q => q.CleanID == "404");
            if (eleDataBlock != null)
            {
                eleDataBlock.dataTexts = new List<string>();
                eleDataBlock.datas = new List<BaseCAEData>();
                int id = 5000;
                if(teraBlock!=null)
                {
                    foreach (MTerahedra terah in teraBlock.datas)
                    {
                        List<string> texts = new List<string>();
                        texts.Add(id.ToString() + ",124,1,26,6,1,0,0,1,0,0,0,0,0,0");
                        texts.Add("0,0,0,0,0,0,0,0,0,0,0,0");
                        //Node ID
                        string node1ID = verticeNodeDic[terah.vert1ID];
                        string node2ID = verticeNodeDic[terah.vert2ID];
                        string node3ID = verticeNodeDic[terah.vert3ID];
                        string node4ID = verticeNodeDic[terah.vert4ID];
                        if (node1ID != null && node2ID != null && node3ID != null && node4ID != null)
                        {
                            texts.Add(node1ID + "," + node2ID + "," + node3ID + ",0," + node4ID + ",0,0,0,0,0");
                            texts.AddRange(new string[]{"0,0,0,0,0,0,0,0,0,0",
                                "0.,0.,0.,0,0,0,0,0,0",
                                "0.,0.,0.",
                                "0.,0.,0.",
                                "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
                                "0" });
                            //此处为效率考虑，不添加Block的RawTexts
                            eleDataBlock.datas.Add(new NeuElement404(eleDataBlock, texts, false, false));
                            curNum++;
                            id++;
                            if (progBar != null)
                            {
                                progBar.Value = (int)((double)curNum / (double)totalNum * progBar.Maximum);
                            }
                        }
                    }
                    foreach (MPyramid pyramid in pyramidBlock.datas)
                    {
                        List<string> texts = new List<string>();
                        texts.Add(id.ToString() + ",124,1,26,14,1,0,0,1,0,0,0,0,0,0");
                        texts.Add("0,0,0,0,0,0,0,0,0,0,0,0");
                        //Node ID
                        string node1ID = verticeNodeDic[pyramid.vert1ID];
                        string node2ID = verticeNodeDic[pyramid.vert2ID];
                        string node3ID = verticeNodeDic[pyramid.vert3ID];
                        string node4ID = verticeNodeDic[pyramid.vert4ID];
                        string node5ID = verticeNodeDic[pyramid.vert5ID];
                        if (node1ID != null && node2ID != null && node3ID != null && node4ID != null)
                        {
                            texts.Add(node1ID + "," + node2ID + "," + node3ID + "," + node4ID + "," + node5ID + ",0,0,0,0,0");
                            texts.AddRange(new string[]{"0,0,0,0,0,0,0,0,0,0",
                                "0.,0.,0.,0,0,0,0,0,0",
                                "0.,0.,0.",
                                "0.,0.,0.",
                                "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
                                "0" });
                            //此处为效率考虑，不添加Block的RawTexts
                            eleDataBlock.datas.Add(new NeuElement404(eleDataBlock, texts, false, false));
                            curNum++;
                            id++;
                            if (progBar != null)
                            {
                                progBar.Value = (int)((double)curNum / (double)totalNum * progBar.Maximum);
                            }
                        }
                    }
                    foreach (MPrism prism in prismBlock.datas)
                    {
                        List<string> texts = new List<string>();
                        texts.Add(id.ToString() + ",124,1,26,7,1,0,0,1,0,0,0,0,0,0");
                        texts.Add("0,0,0,0,0,0,0,0,0,0,0,0");
                        //Node ID
                        string node1ID = verticeNodeDic[prism.vert1ID];
                        string node2ID = verticeNodeDic[prism.vert2ID];
                        string node3ID = verticeNodeDic[prism.vert3ID];
                        string node4ID = verticeNodeDic[prism.vert4ID];
                        string node5ID = verticeNodeDic[prism.vert5ID];
                        string node6ID = verticeNodeDic[prism.vert6ID];
                        if (node1ID != null && node2ID != null && node3ID != null && node4ID != null)
                        {
                            texts.Add(node1ID + "," + node2ID + "," + node3ID + ",0," + node4ID + "," + node5ID +","+ node6ID+ ",0,0,0,0,0");
                            texts.AddRange(new string[]{"0,0,0,0,0,0,0,0,0,0",
                                "0.,0.,0.,0,0,0,0,0,0",
                                "0.,0.,0.",
                                "0.,0.,0.",
                                "0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0",
                                "0" });
                            //此处为效率考虑，不添加Block的RawTexts
                            eleDataBlock.datas.Add(new NeuElement404(eleDataBlock, texts, false, false));
                            curNum++;
                            id++;
                            if (progBar != null)
                            {
                                progBar.Value = (int)((double)curNum / (double)totalNum * progBar.Maximum);
                            }
                        }
                    }
                }
            }
        }

        public NeuNode403 GetNodeByCoordinate(string x,string y,string z)
        {
            NeuDataBlock nodeBlock=GetNeuDataBlock("403");
            if(nodeBlock!=null)
            {
                foreach (NeuNode403 node in nodeBlock.datas)
                {
                    if(node.x==x&&node.y==y&&node.z==z)
                    {
                        return node;
                    }
                }
            }
            return null;
        }

        public NeuNode403 GetNodeByCoordinate(MVertice vertice)
        {
            return GetNodeByCoordinate(vertice.x, vertice.y, vertice.z);
        }

        public NeuNode403 GetNodeByTempAssocDataID(string assocID)
        {
            NeuDataBlock nodeBlock = GetNeuDataBlock("403");
            if (nodeBlock != null)
            {
                return nodeBlock.datas.Find(q => q.tempAssocDataID == assocID) as NeuNode403;
            }
            return null;
        }

        public void ClearNodesAndElementDatas()
        {
            //Nodes
            NeuDataBlock nodeDataBlock = NeuDataBlock.Find(q => q.CleanID == "403");
            if(nodeDataBlock!=null)
            {
                nodeDataBlock.datas.Clear();
                nodeDataBlock.dataTexts.Clear();
            }
            //Elements
            NeuDataBlock eleDataBlock = NeuDataBlock.Find(q => q.CleanID == "404");
            if (eleDataBlock != null)
            {
                eleDataBlock.datas.Clear();
                eleDataBlock.dataTexts.Clear();
            }
        }

    }

}
