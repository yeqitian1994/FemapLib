using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FemapLib;
using System.Xml;

namespace FemapLib
{
    public class XMLForan:XMLOper
    {
        public FEMColorsByParts part;
        public XMLForan(string filePath):base(filePath, enumXmlPathType.AbsolutePath)
        {
            part = new FEMColorsByParts();
        }

        public List<base_part> GetBase_Parts()
        {
            List<base_part> parts = new List<base_part>();
            parts.AddRange(part.plate_parts);
            parts.AddRange(part.profile_parts);
            return parts;
        }
        public void ReadForanXML()
        {
            XmlNode root= GetXmlRoot();
            part.plate_parts = new List<plate_part>();
            part.profile_parts = new List<profile_part>();
            foreach (XmlNode node in root.ChildNodes)
            {
                if(node.Name=="plate_part")
                {
                    plate_part newPlatePart = new plate_part();
                    foreach (XmlNode plateChildNode in node.ChildNodes)
                    {
                        switch (plateChildNode.Name)
                        {
                            case "part_oid": newPlatePart.part_oid = plateChildNode.InnerText;  break;
                            case "part_id": newPlatePart.part_id = plateChildNode.InnerText; break;
                            case "block": newPlatePart.block = plateChildNode.InnerText; break;
                            case "ship": newPlatePart.ship = plateChildNode.InnerText; break;
                            case "cog": newPlatePart.cog = GetCog(plateChildNode); break;
                            case "color": newPlatePart.color =GetColor(plateChildNode) ; break;
                            case "attribs": newPlatePart.attribs =GetPlateAttribs(plateChildNode) ; break;
                            default:break;
                        }
                    }
                    part.plate_parts.Add(newPlatePart);
                }
                else if(node.Name=="profile_part")
                {
                    profile_part newProfilePart = new profile_part();
                    foreach (XmlNode profileChildNode in node.ChildNodes)
                    {
                        switch (profileChildNode.Name)
                        {
                            case "part_oid": newProfilePart.part_oid = profileChildNode.InnerText; break;
                            case "part_id": newProfilePart.part_id = profileChildNode.InnerText; break;
                            case "block": newProfilePart.block = profileChildNode.InnerText; break;
                            case "ship": newProfilePart.ship = profileChildNode.InnerText; break;
                            case "cog": newProfilePart.cog = GetCog(profileChildNode); break;
                            case "color": newProfilePart.color = GetColor(profileChildNode); break;
                            case "attribs": newProfilePart.attribs = GetProfileAttribs(profileChildNode); break;
                            default: break;
                        }
                    }
                    part.profile_parts.Add(newProfilePart);
                }
            }
        }
        public static cog GetCog(XmlNode cogNode)
        {
            cog newCog = null;
            if (cogNode!=null)
            {
                newCog = new cog();
                foreach (XmlNode cogChildNode in cogNode.ChildNodes)
                {
                    switch (cogChildNode.Name.ToUpper())
                    {
                        case "X": newCog.x = cogChildNode.InnerText; break;
                        case "Y": newCog.y = cogChildNode.InnerText; break;
                        case "Z": newCog.z = cogChildNode.InnerText; break;
                        default: break;
                    }
                }
            }
            return newCog;
        }
        public static color GetColor(XmlNode colorNode)
        {
            color newColor = null;
            if (colorNode != null)
            {
                newColor = new color();
                foreach (XmlNode colorChildNode in colorNode.ChildNodes)
                {
                    switch (colorChildNode.Name.ToUpper())
                    {
                        case "R": newColor.r = colorChildNode.InnerText; break;
                        case "G": newColor.g = colorChildNode.InnerText; break;
                        case "B": newColor.b = colorChildNode.InnerText; break;
                        default: break;
                    }
                }
            }
            return newColor;
        }
        public static plate_attribs GetPlateAttribs(XmlNode plateAttrNode)
        {
            plate_attribs newAttri = null;
            if (plateAttrNode != null)
            {
                newAttri = new plate_attribs();
                foreach (XmlNode plateAttrChildNode in plateAttrNode.ChildNodes)
                {
                    switch (plateAttrChildNode.Name)
                    {
                        case "material": newAttri.material = plateAttrChildNode.InnerText; break;
                        case "thickness": newAttri.thickness = plateAttrChildNode.InnerText; break;
                        default: break;
                    }
                }
            }
            return newAttri;
        }
        public static profile_attribs GetProfileAttribs(XmlNode profileAttrNode)
        {
            profile_attribs newAttri = null;
            if (profileAttrNode != null)
            {
                newAttri = new profile_attribs();
                foreach (XmlNode profileAttrChildNode in profileAttrNode.ChildNodes)
                {
                    switch (profileAttrChildNode.Name)
                    {
                        case "material": newAttri.material = profileAttrChildNode.InnerText; break;
                        case "scantling": newAttri.scantling = profileAttrChildNode.InnerText; break;
                        case "inertia_x": newAttri.inertia_x = profileAttrChildNode.InnerText; break;
                        case "inertia_y": newAttri.inertia_y = profileAttrChildNode.InnerText; break;
                        case "inertia_z": newAttri.inertia_z = profileAttrChildNode.InnerText; break;
                        case "section_area": newAttri.section_area = profileAttrChildNode.InnerText; break;
                        default: break;
                    }
                }
            }
            return newAttri;
        }
        public List<string> GetAllMaterials()
        {
            List<string> materials = new List<string>();
            foreach (plate_part platePart in part.plate_parts)
            {
                string mat = platePart.attribs.material;
                if (!materials.Contains(mat))
                materials.Add(mat);
            }
            foreach (profile_part profilePart in part.profile_parts)
            {
                string mat = profilePart.attribs.material;
                if (!materials.Contains(mat))
                    materials.Add(mat);
            }
            return materials;
        }

        public void ApplyNewMaterial(string oldMat,string newMat)
        {
            foreach (plate_part platePart in part.plate_parts)
            {
                string mat = platePart.attribs.material;
                if (mat == oldMat)
                    platePart.attribs.material = newMat;
            }
            foreach (profile_part profilePart in part.profile_parts)
            {
                string mat = profilePart.attribs.material;
                if (mat == oldMat)
                    profilePart.attribs.material = newMat;
            }
        }

        public base_part GetPartByOid(string oid)
        {
            foreach (plate_part platePart in part.plate_parts)
            {
                if (platePart.part_oid == oid)
                    return platePart;
            }
            foreach (profile_part profilePart in part.profile_parts)
            {
                if (profilePart.part_oid == oid)
                    return profilePart;
            }
            return null;
        }
    }

    public class FEMColorsByParts
    {
        public List<plate_part> plate_parts;
        public List<profile_part> profile_parts;
        public FEMColorsByParts()
        {
            plate_parts = new List<plate_part>();
            profile_parts = new List<profile_part>();
        }
    }

    public class base_part
    {
        public string part_oid;
        public string part_id;
        public string block;
        public string ship;
        public cog cog;
        public color color;
    }

    public class plate_part: base_part
    {
        public plate_attribs attribs;
    }

    public class profile_part: base_part
    {
        public profile_attribs attribs;
    }


    public class cog
    {
        public string x;
        public string y;
        public string z;
        public double X
        {
            get
            {
                if(double.TryParse(x, out double result))
                {
                    return result;
                }
                return -1;
            }
        }
        public double Y
        {
            get
            {
                if (double.TryParse(y, out double result))
                {
                    return result;
                }
                return -1;
            }
        }
        public double Z
        {
            get
            {
                if (double.TryParse(z, out double result))
                {
                    return result;
                }
                return -1;
            }
        }

    }

    public class color
    {
        public string r;
        public string g;
        public string b;
        public int R
        {
            get
            {
                if (int.TryParse(r, out int result))
                {
                    return result;
                }
                return -1;
            }
        }
        public int G
        {
            get
            {
                if (int.TryParse(g, out int result))
                {
                    return result;
                }
                return -1;
            }
        }
        public int B
        {
            get
            {
                if (int.TryParse(b, out int result))
                {
                    return result;
                }
                return -1;
            }
        }
    }

    public class base_attribs
    {
        public string material;
    }

    public class plate_attribs:base_attribs
    {
        public string thickness;
        public double Thickness
        {
            get
            {
                if(double.TryParse(thickness, out double result))
                {
                    return result;
                }
                else
                {
                    return 0;
                }
            }
        }

        public override string ToString()
        {
            string str = "plate_attribs info start:\n";
            str += "material:"+ material+"\n";
            str += "thickness:" + thickness + "\n";
            str += "info end\n";
            return str;
        }
    }
    public class profile_attribs : base_attribs
    {
        public string scantling;
        public string inertia_x;
        public double Inertia_X
        {
            get
            {
                if(double.TryParse(inertia_x, out double result))
                {
                    return result;
                }
                return -1;
            }
        }
        public string inertia_y;
        public double Inertia_Y
        {
            get
            {
                if (double.TryParse(inertia_y, out double result))
                {
                    return result;
                }
                return -1;
            }
        }
        public string inertia_z;
        public double Inertia_Z
        {
            get
            {
                if (double.TryParse(inertia_z, out double result))
                {
                    return result;
                }
                return -1;
            }
        }
        public string section_area;
        public double Section_Area
        {
            get
            {
                if (double.TryParse(section_area, out double result))
                {
                    return result;
                }
                return -1;
            }
        }

        public override string ToString()
        {
            string str = "plate_attribs info start:\n";
            str += "material:" + material + "\n";
            str += "scantling:" + scantling + "\n";
            str += "inertia_x:" + inertia_x + "\n";
            str += "inertia_y:" + inertia_y + "\n";
            str += "inertia_z:" + inertia_z + "\n";
            str += "section_area:" + section_area + "\n";
            str += "info end\n";
            return str;
        }
    }
}
