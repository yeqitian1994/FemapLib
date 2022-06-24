using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FemapLib
{
    public class NeuFileHeader100:BaseNeuData
    {
        //line1
        public string title="<NULL>";
        //line2
        public string version;

        public override void UpdateRawTexts()
        {
            rawTexts = new List<string>();
            rawTexts.Add(title);
            rawTexts.Add(version);
        }
    }
}
