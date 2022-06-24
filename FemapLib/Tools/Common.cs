using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FemapLib
{
    public class Common
    {
        public static string ToString(List<string> texts,char split=',')
        {
            if(texts==null)
            {
                return "";
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < texts.Count; i++)
            {
                sb.Append(texts[i]);
                if (i < texts.Count - 1)
                {
                    sb.Append(split);
                }
            }
            return sb.ToString();
        }
    }
}
