using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using femap;

namespace FemapLib
{
    public class LibProperty
    {
        public static bool HasProperty(string title, out int id, model feModel = null)
        {
            id = -1;
            if (feModel == null) feModel = LibApp.GetDefaultFemapModel();
            if (feModel != null)
            {
                Prop prop = feModel.feProp;
                prop.Next();
                while (LibApp.IsIDValid(prop.ID))
                {
                    if (prop.title == title)
                    {
                        id = prop.ID;
                        return true;
                    }
                    prop.Next();
                }
            }
            return false;
        }

        public static int CreatePlateProperty(string title,int matID,double thickness, model feModel = null)
        {
            int newPropID = -1;
            if (feModel == null) feModel = LibApp.GetDefaultFemapModel();
            if (feModel != null)
            {
                Prop prop = feModel.feProp;
                newPropID = prop.NextEmptyID();
                prop.type = zElementType.FET_L_PLATE;
                prop.title = title;
                prop.matlID = matID;
                prop.color = zColor.FCL_BLUE;
                prop.layer = 1;
                prop.pval[0] = thickness;
                prop.Put(newPropID);
            }
            return newPropID;
        }

        public static int CreateBeamProperty(string title, int matID, double thickness, model feModel = null)
        {
            int newPropID = -1;
            if (feModel == null) feModel = LibApp.GetDefaultFemapModel();
            if (feModel != null)
            {
                Prop prop = feModel.feProp;
                newPropID = prop.NextEmptyID();
                prop.type = zElementType.FET_L_PLATE;
                prop.title = title;
                prop.matlID = matID;
                prop.color = zColor.FCL_BLUE;
                prop.layer = 1;
                prop.pval[0] = thickness;
                prop.Put(newPropID);
            }
            return newPropID;
        }
    }
}
