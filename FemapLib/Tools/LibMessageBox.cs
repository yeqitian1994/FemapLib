using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using femap;

namespace FemapLib
{
    public class LibMessageBox
    {
        public static string normalTitle = "消息";
        public static void InfoNoFemap()
        {
            MessageBox.Show("未找到正在运行的Femap实例。", normalTitle);
        }

        public static bool QueBox(string message)
        {
            DialogResult dr = MessageBox.Show(message, normalTitle, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            return dr == DialogResult.OK;
        }

        public static bool QueSaveFile(model feModel)
        {
            if(feModel!=null)
            {
               return QueBox("需要保存当前文件，是否继续？");
            }
            return false;
        }
    }
}
