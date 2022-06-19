using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using femap;

namespace FemapLib
{
    public class LibApp
    {
        public static model GetDefaultFemapModel()
        {
            return Marshal.GetActiveObject("femap.model") as model;
        }

        public static bool IsIDValid(int id)
        {
            return id > 0 && id < 2147483647;
        }



        public static zReturnCode returnCode;


    }
}
