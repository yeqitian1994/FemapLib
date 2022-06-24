using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using femap;

namespace FemapLib
{
    public class LibMessage
    {
        public static void WriteLine(zReturnCode returnCode, zMessageColor messColor = zMessageColor.FCM_NORMAL, model feModel = null)
        {
            WriteLine(returnCode.ToString(), messColor, feModel);
        }
        public static void WriteLine(double message, zMessageColor messColor = zMessageColor.FCM_NORMAL, model feModel = null)
        {
            WriteLine(message.ToString(), messColor, feModel);
        }
        public static void WriteLine(int message, zMessageColor messColor = zMessageColor.FCM_NORMAL, model feModel = null)
        {
            WriteLine(message.ToString(), messColor, feModel);
        }
        /// <summary>
        /// 输出信息框信息
        /// </summary>
        /// <param name="message">信息</param>
        /// <param name="messColor">颜色，默认Normal</param>
        /// <param name="feModel">femap model</param>
        public static void WriteLine(string message, zMessageColor messColor=zMessageColor.FCM_NORMAL,model feModel =null)
        {
            if(feModel==null)
            {
                feModel = LibApp.GetFemapModel();
            }
            if(feModel!=null)
            {
                feModel.feAppMessage(messColor, message);
            }
        }
        public static void WriteGeometryInfos(bool writeIDs=true, model feModel = null)
        {
            WriteSolidInfos(writeIDs, feModel);
            WriteSurfaceInfos(writeIDs, feModel);
            WriteCurveInfos(writeIDs, feModel);
            WritePointInfos(writeIDs, feModel);
        }
        /// <summary>
        /// 输出实体信息
        /// </summary>
        /// <param name="writeIDs"></param>
        /// <param name="feModel"></param>
        public static void WriteSolidInfos(bool writeIDs = true, model feModel = null)
        {
            if (feModel == null)feModel = LibApp.GetFemapModel();
            if (feModel != null)
            {
                IList<int> solidIDs = LibSoild.GetAllSolidIDs(feModel);
                WriteLine("Solids Count:" + solidIDs.Count);
                if (writeIDs)
                {
                    for (int i = 0; i < solidIDs.Count; i++)
                    {
                        WriteLine("Solid " + (i + 1) + " ID:" + solidIDs[i]);
                    }
                }
            }
        }
        /// <summary>
        /// 输出表面信息
        /// </summary>
        /// <param name="writeIDs"></param>
        /// <param name="feModel"></param>
        public static void WriteSurfaceInfos(bool writeIDs = true, model feModel = null)
        {
            if (feModel == null) feModel = LibApp.GetFemapModel();
            if (feModel != null)
            {
                IList<int> surfaceIDs = LibSurface.GetAllSurfaceIDs(feModel);
                WriteLine("Surface Count:" + surfaceIDs.Count);
                if (writeIDs)
                {
                    for (int i = 0; i < surfaceIDs.Count; i++)
                    {
                        WriteLine("Surface " + (i + 1) + " ID:" + surfaceIDs[i]);
                    }
                }
            }
        }
        /// <summary>
        /// 输出曲线信息
        /// </summary>
        /// <param name="writeIDs"></param>
        /// <param name="feModel"></param>
        public static void WriteCurveInfos(bool writeIDs = true, model feModel = null)
        {
            if (feModel == null) feModel = LibApp.GetFemapModel();
            if (feModel != null)
            {
                IList<int> curveIDs =LibCurve.GetAllCurveIDs(feModel);
                WriteLine("Curve Count:" + curveIDs.Count);
                if (writeIDs)
                {
                    for (int i = 0; i < curveIDs.Count; i++)
                    {
                        WriteLine("Curve " + (i + 1) + " ID:" + curveIDs[i]);
                    }
                }
            }
        }
        /// <summary>
        /// 输出点坐标信息
        /// </summary>
        /// <param name="writeDetails">输出其他细节</param>
        /// <param name="feModel">femap model</param>
        public static void WritePointInfos(bool writeDetails = true, model feModel = null)
        {
            if (feModel == null) feModel = LibApp.GetFemapModel();
            if (feModel != null)
            {
                IList<int> pointIDs =LibPoint.GetAllPointIDs(feModel);
                WriteLine("Point Count:" + pointIDs.Count);
                if (writeDetails)
                {
                    for (int i = 0; i < pointIDs.Count; i++)
                    {
                        WriteLine("Point " + (i + 1) + " ID:" + pointIDs[i]);
                    }
                }
            }
        }
        /// <summary>
        /// 报错："未能获取到ID号为："+id+"的\""+type+"\"类型对象。"
        /// </summary>
        /// <param name="id">id号</param>
        /// <param name="type">对象类型</param>
        /// <param name="additionalMess">额外信息</param>
        /// <param name="messColor">信息颜色</param>
        /// <param name="feModel">femap model对象</param>
        public static void WriteFailedToGetEntity(int id,string type,string additionalMess="", zMessageColor messColor = zMessageColor.FCM_NORMAL, model feModel = null)
        {
            WriteLine("未能获取到ID号为："+id+"的\""+type+"\"类型对象。"+ additionalMess, messColor, feModel);
        }
    }
}
