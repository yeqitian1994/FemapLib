using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FemapLib
{
    /// <summary>
    /// 文本文件操作类
    /// </summary>
    public class FileOper
    {
        /// <summary>
        /// 每一行文本文件内容集合
        /// </summary>
        public List<string> lines;
        /// <summary>
        /// 文件行数
        /// </summary>
        public int lineNum
        {
            get { if (lines != null) { return lines.Count; } else return 0; }
        }
        /// <summary>
        /// 文件全路径
        /// </summary>
        public string filePath;
        /// <summary>
        /// 构造函数，输入读取的文本文件的全路径
        /// </summary>
        /// <param name="filePathI">文本文件的全路径</param>
        public FileOper(string filePathI)
        {
            filePath = filePathI;
        }
        /// <summary>
        /// 读取文本文件
        /// </summary>
        public virtual void Read()
        {
            if (File.Exists(filePath))
            {
                //采用Unicode编码，支持中文情况
                StreamReader ReadFileInfo = new StreamReader(filePath, System.Text.Encoding.Default);
                string Line;
                lines = new List<string>();
                do
                {
                    Line = ReadFileInfo.ReadLine();
                    if (Line != null)
                    {
                        lines.Add(Line);
                    }

                } while (Line != null);

                ReadFileInfo.Close();
            }
            else
            {
                MessageBox.Show(filePath + "不存在！");
            }
        }
        /// <summary>
        /// 写入文本文件
        /// </summary>
        public void WriteFile()
        {
            StreamWriter WriterFileInfo = new StreamWriter(filePath, false, Encoding.Default);
            for (int n = 0; n < lines.Count; ++n)
            {
                WriterFileInfo.WriteLine(lines[n]);
            }

            WriterFileInfo.Close();

        }
        /// <summary>
        /// 通过边界获取文本
        /// </summary>
        /// <param name="startLineI">边界头</param>
        /// <param name="endLineI">边界尾</param>
        /// <param name="hasBorderI">输出文本是否包含头尾</param>
        /// <returns>返回字符串数组</returns>
        public string[] GetStringsByBorder(string startLineI, string endLineI, bool hasBorderI)
        {
            if (lines.Contains(startLineI) && lines.Contains(endLineI))
            {
                List<string> strReturn = new List<string>();
                int startIndexI = lines.IndexOf(startLineI);
                int endIndexI = -1;
                for (int i = startIndexI + 1; i < lines.Count; i++)
                {
                    if (lines[i] == endLineI)
                    {
                        endIndexI = i;
                        break;
                    }
                }
                if (endIndexI != -1)
                {
                    for (int i = startIndexI + 1; i < endIndexI; i++)
                    {
                        strReturn.Add(lines[i]);
                    }
                    if (hasBorderI)
                    {
                        strReturn.Insert(0, startLineI);
                        strReturn.Add(endLineI);
                    }
                    return strReturn.ToArray();
                }
            }
            return null;
        }
        /// <summary>
        /// 从字符串数组中返回指定的中间字符串数组
        /// </summary>
        /// <param name="oriStringsI">输入的文本文件的内容字符串</param>
        /// <param name="startIndexI">oriStringsI数组的起始索引</param>
        /// <param name="endIndexI">oriStringsI数组的终止索引</param>
        /// <param name="hasBorderI">是否包含起始索引和终止索引所代表的字符串</param>
        /// <returns>返回的中间字符串数组</returns>
        public string[] GetStringsByBorder(string[] oriStringsI, int startIndexI, int endIndexI, bool hasBorderI)
        {
            List<string> strRe = new List<string>();
            if (startIndexI < oriStringsI.Length && endIndexI < oriStringsI.Length)
            {
                for (int i = startIndexI; i <= endIndexI; i++)
                {
                    strRe.Add(oriStringsI[i]);
                }
                if (!hasBorderI)
                {
                    strRe.RemoveAt(0);
                    strRe.RemoveAt(strRe.Count - 1);
                }
            }
            return strRe.ToArray();
        }
    }
}