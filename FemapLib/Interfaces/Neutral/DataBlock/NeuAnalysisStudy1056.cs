using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FemapLib
{
    public class NeuAnalysisStudy1056 : BaseNeuData
    {
        //line1
        public string ID;
        //line2
        public string title;
        //line3
        public string analysisProgram;
        public string analysisType;
        //line4
        public string fileTime;
        public string anaylsisSet;
        //line5..n
        public List<string> studyNotes;
        public NeuAnalysisStudy1056()
        {
            caeDataType = "Analysis Study";
            blockTypeID = "1056";
        }
        public override void UpdateRawTexts()
        {
            rawTexts = new List<string>();
            rawTexts.Add(ID);
            rawTexts.Add(title);
            rawTexts.Add(analysisProgram+spli+analysisType);
            rawTexts.Add(fileTime + spli + anaylsisSet);
            rawTexts.AddRange(studyNotes);
        }
    }
}
