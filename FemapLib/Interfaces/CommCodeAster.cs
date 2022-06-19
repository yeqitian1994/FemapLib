using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace FemapLib
{
    public class CommCodeAster:FileOper
    {
        public CommCodeAster(string filePath):base(filePath)
        {

        }
        public void Initialize()
        {
            mFileData.Add("DEBUT()");
        }
        /// <summary>
        /// 导入网格
        /// </summary>
        /// <param name="meshName">导入的网格组名称</param>
        public void Write_LIRE_MAILLAGE(string meshName)
        {
            mFileData.Add(meshName + "= LIRE_MAILLAGE(UNITE=20)");
        }
        /// <summary>
        /// 修改现有网格
        /// </summary>
        /// <param name="meshName">操作的网格组名称</param>
        /// <param name="reorientateGroupName">重定位网格边缘的组名称</param>
        public void Write_MODI_MAILLAGE(string meshName,string reorientateGroupName)
        {
            mFileData.Add(meshName + "=MODI_MAILLAGE(reuse=" + meshName + ",MAILLAGE=" + meshName + ",ORIE_PEAU=_F(GROUP_MA_PEAU=('"+ reorientateGroupName + "', )))");
        }
        /// <summary>
        /// 定义被模拟的物理现象(机械的、热的或声学的)和有限元素的类型。
        /// </summary>
        /// <param name="meshName"></param>
        public void Write_AFFE_MODELE(string meshName)
        {

        }
    }
}
