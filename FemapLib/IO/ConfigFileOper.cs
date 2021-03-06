using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FemapLib
{
    /// <summary>
    /// 配置文件操作类
    /// </summary>
    public class ConfigFileOper:FileOper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filePathI">源文件全路径</param>
        public ConfigFileOper(string filePathI):base(filePathI)
        {
            
        }
        /// <summary>
        /// 根据起始行和终止行获取配置信息
        /// </summary>
        /// <param name="startLineI">起始行</param>
        /// <param name="endLineI">终止行</param>
        /// <returns>配置块信息</returns>
        public SConfigBlock GetConfigBlock(string startLineI,string endLineI)
        {
            string[] configInfos=base.GetStringsByBorder(startLineI, endLineI, false);
            return new SConfigBlock(startLineI, endLineI,configInfos);
        }
    }
    /// <summary>
    /// 配置信息块，包含了起始行、终止行以及配置信息
    /// </summary>
    public struct SConfigBlock
    {
        private string startLine;
        /// <summary>
        /// 起始行
        /// </summary>
        public string StartLine
        {
            get { return startLine; }
        }
        private string endLine;
        /// <summary>
        /// 终止行
        /// </summary>
        public string EndLine
        {
            get { return endLine; }
        }
        /// <summary>
        /// 配置信息行
        /// </summary>
        public List<string> lines;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="startLineI">起始行</param>
        /// <param name="endLineI">终止行</param>
        /// <param name="linesI">配置信息行</param>
        public SConfigBlock(string startLineI,string endLineI,string[] linesI)
        {
            startLine = startLineI;
            endLine = endLineI;
            lines = new List<string>();
            if(linesI!=null&&linesI.Length>0)
            {
                foreach (string line in linesI)
                {
                    if (string.IsNullOrEmpty(line) == false && line.StartsWith("//") == false)
                    {
                        lines.Add(line);
                    }
                }
            }
        }
        /// <summary>
        /// 获取用分隔符分隔后的配置信息（输入多行文本，每一行文本用固定的分隔符组合信息）
        /// </summary>
        /// <param name="configLines">配置信息行</param>
        /// <param name="splitChar">每行信息之间的分隔符</param>
        /// <returns>分隔后的信息</returns>
        public List<List<string>> GetSplitedInfos(char splitChar=',')
        {
            List<List<string>> lineInfosList = new List<List<string>>();
            for (int i = 0; i < lines.Count; i++)
            {
                string[] strs = lines[i].Split(splitChar);
                List<string> elements = new List<string>();
                foreach (string str in strs)
                {
                    elements.Add(str.Trim());
                }
                lineInfosList.Add(elements);
            }
            return lineInfosList;
        }
    }
}
