using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using femap;

namespace FemapLib
{
    public class LibNode
    {
        public static Node GetNode(int nodeID,model feModel)
        {
            Node node = feModel.feNode;
            zReturnCode rc = node.Get(nodeID);
            if (rc == zReturnCode.FE_OK)
            {
                return node;
            }
            else
            {
                return null;
            }
        }
        public static void SetNodeCoordinate(int nodeID,double[] coordinate,model feModel)
        {
            if(feModel!=null&& coordinate!=null&& coordinate.Length>=3)
            {
                Node node = GetNode(nodeID, feModel);
                if(node!=null)
                {
                    node.x = coordinate[0];
                    node.y = coordinate[1];
                    node.z = coordinate[2];
                }
            }
        }
    }
}
