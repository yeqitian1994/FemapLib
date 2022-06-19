
using System;
using System.Xml;
using System.Web;

namespace FemapLib
{
    /// <summary>
    /// XML文件的操作类
    /// </summary>
    public class XMLOper
    {
        /// <summary>
        /// xml文件所在路径类型
        /// </summary>
        public enum enumXmlPathType
        {
            /// <summary>
            /// 绝对路径
            /// </summary>
            AbsolutePath,
            /// <summary>
            /// 虚拟路径
            /// </summary>
            VirtualPath
        }
        private string xmlFilePath;//文件路径
        private enumXmlPathType xmlFilePathType;//定义一个枚举变量
        private XmlDocument xmlDoc = new XmlDocument();
        /// <summary>
        /// 文件路径
        /// </summary>
        /// <remarks>文件路径</remarks>
        public string XmlFilePath
        {
            get
            {
                return this.xmlFilePath;
            }
            set
            {
                xmlFilePath = value;
            }
        }
        /// <summary>
        /// 文件路径类型
        /// </summary>
        public enumXmlPathType XmlFilePathTyp
        {
            set
            {
                xmlFilePathType = value;
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tempXmlFilePath">文件路径</param>
        public XMLOper(string tempXmlFilePath)
        {
            this.xmlFilePathType = enumXmlPathType.VirtualPath;//虚拟路径
            this.xmlFilePath = tempXmlFilePath;//xml文件路径
            GetXmlDocument();//获取XmlDocument实体类
            //xmlDoc.Load( xmlFilePath ) ;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tempXmlFilePath">文件路径</param>
        /// <param name="tempXmlFilePathType">文件类型</param>
        public XMLOper(string tempXmlFilePath, enumXmlPathType tempXmlFilePathType)
        {
            this.xmlFilePathType = tempXmlFilePathType;
            this.xmlFilePath = tempXmlFilePath;
            GetXmlDocument();//获取XmlDocument实体类
        }
        /// <summary>
        /// 获取XmlDocument实体类
        /// </summary>    
        /// <returns>返回指定的XML描述文件的一个xmldocument实例</returns>
        private XmlDocument GetXmlDocument()
        {
            XmlDocument doc = null;//5@1@a@s@p@x
            if (this.xmlFilePathType == enumXmlPathType.AbsolutePath)
            {
                doc = GetXmlDocumentFromFile(xmlFilePath);
            }
            else if (this.xmlFilePathType == enumXmlPathType.VirtualPath)
            {
                doc = GetXmlDocumentFromFile(HttpContext.Current.Server.MapPath(xmlFilePath));
            }
            return doc;
        }
        /// <summary>
        /// 获取XmlDocument实体类
        /// </summary>
        /// <param name="tempXmlFilePath">文件路径</param>
        /// <returns>返回指定的XML描述文件的一个xmldocument实例</returns>
        private XmlDocument GetXmlDocumentFromFile(string tempXmlFilePath)
        {
            string xmlFileFullPath = tempXmlFilePath;
            xmlDoc.Load(xmlFileFullPath);
            //定义事件处理
            xmlDoc.NodeChanged += new XmlNodeChangedEventHandler(this.nodeUpdateEvent);
            xmlDoc.NodeInserted += new XmlNodeChangedEventHandler(this.nodeInsertEvent);
            xmlDoc.NodeRemoved += new XmlNodeChangedEventHandler(this.nodeDeleteEvent);
            return xmlDoc;
        }
        /// <summary>
        /// 功能:
        /// 获取所有指定名称的节点(XmlNodeList)
        /// </summary>
        /// <param name="strNode">节点名称</param>
        /// <returns>返回指定名称的节点</returns>
        public XmlNodeList GetXmlNodeList(string strNode)
        {
            XmlNodeList strReturn = null;
            try
            {
                //根据指定路径获取节点
                XmlNodeList xmlNode = xmlDoc.SelectNodes(strNode);
                if (!(xmlNode == null))//5#1#a#s#p#x
                {
                    strReturn = xmlNode;
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
            return strReturn;
        }
        /// <summary>
        /// 功能:
        /// 读取指定节点的指定属性值(Value)
        /// </summary>
        /// <param name="strNode">节点名称</param>
        /// <param name="strAttribute">此节点属性</param>
        /// <returns>返回指定节点的指定属性值</returns>
        public string GetXmlNodeAttributeValue(string strNode, string strAttribute)
        {
            string strReturn = "";
            try
            {
                //根据指定路径获取节点
                XmlNode xmlNode = xmlDoc.SelectSingleNode(strNode);
                if (!(xmlNode == null))
                {
                    strReturn = xmlNode.Attributes.GetNamedItem(strAttribute).Value;
                    /**/
                    ////获取节点的属性，并循环取出需要的属性值
                    //XmlAttributeCollection xmlAttr = xmlNode.Attributes;
                    //for (int i = 0; i < xmlAttr.Count; i++)
                    //{
                    //    if (xmlAttr.Item(i).Name == strAttribute)
                    //    {
                    //        strReturn = xmlAttr.Item(i).Value;
                    //        break;
                    //    }
                    //}
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
            return strReturn;
        }
        /// <summary>
        /// 功能:
        /// 读取指定节点的值(InnerText)
        /// </summary>
        /// <param name="strNode">节点名称</param>
        /// <returns>返回指定节点的值</returns>
        public string GetXmlNodeValue(string strNode)
        {
            string strReturn = String.Empty;
            try
            {
                //根据路径获取节点
                XmlNode xmlNode = xmlDoc.SelectSingleNode(strNode);
                if (!(xmlNode == null))
                    strReturn = xmlNode.InnerText;
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
            return strReturn;
        }
        /// <summary>
        /// 功能:
        /// 设置节点值(InnerText)
        /// </summary>
        /// <param name="xmlNodePath">节点名称</param>
        /// <param name="xmlNodeValue">节点值</param>
        public void SetXmlNodeValue(string xmlNodePath, string xmlNodeValue)
        {
            try
            {
                //可以批量为符合条件的节点进行付值
                XmlNodeList xmlNode = this.xmlDoc.SelectNodes(xmlNodePath);
                if (!(xmlNode == null))
                {
                    foreach (XmlNode xn in xmlNode)
                    {
                        xn.InnerText = xmlNodeValue;
                    }
                }
                /**/
                /**/
                /**/
                /*
         * 根据指定路径获取节点
        XmlNode xmlNode = xmlDoc.SelectSingleNode(xmlNodePath) ;            
        //设置节点值
        if (!(xmlNode==null))
            xmlNode.InnerText = xmlNodeValue ;*/
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        /// <summary>
        /// 功能:
        /// 设置节点的属性值
        /// </summary>
        /// <param name="xmlNodePath">节点名称</param>
        /// <param name="xmlNodeAttribute">属性名称</param>
        /// <param name="xmlNodeAttributeValue">属性值</param>
        public void SetXmlNodeAttributeValue(string xmlNodePath, string xmlNodeAttribute, string xmlNodeAttributeValue)
        {
            try
            {
                //可以批量为符合条件的节点的属性付值
                XmlNodeList xmlNode = this.xmlDoc.SelectNodes(xmlNodePath);
                if (!(xmlNode == null))
                {
                    foreach (XmlNode xn in xmlNode)
                    {
                        XmlAttributeCollection xmlAttr = xn.Attributes;
                        for (int i = 0; i < xmlAttr.Count; i++)
                        {
                            if (xmlAttr.Item(i).Name == xmlNodeAttribute)
                            {
                                xmlAttr.Item(i).Value = xmlNodeAttributeValue;
                                break;
                            }
                        }
                    }
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        /// <summary>
        /// 获取XML文件的根元素
        /// </summary>
        /// <returns>返回XML文件的根元素</returns>
        public XmlNode GetXmlRoot()
        {
            return xmlDoc.DocumentElement;
        }
        /// <summary>
        /// 在根节点下添加父节点
        /// </summary>
        /// <param name="parentNode">父节点名称</param>
        public void AddParentNode(string parentNode)
        {
            try
            {
                XmlNode root = GetXmlRoot();
                XmlNode parentXmlNode = xmlDoc.CreateElement(parentNode);
                root.AppendChild(parentXmlNode);
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        /// <summary>
        /// 向一个已经存在的父节点中插入一个子节点,并返回子节点.
        /// </summary>
        /// <param name="parentNodePath">父节点</param>
        /// <param name="childnodename">子节点名称</param>
        /// <returns>返回子节点</returns>
        public XmlNode AddChildNode(string parentNodePath, string childnodename)
        {
            XmlNode childXmlNode = null;
            try
            {
                XmlNode parentXmlNode = xmlDoc.SelectSingleNode(parentNodePath);
                if (!((parentXmlNode) == null))//如果此节点存在
                {
                    childXmlNode = xmlDoc.CreateElement(childnodename);
                    parentXmlNode.AppendChild(childXmlNode);
                }
                else
                {//如果不存在就放父节点添加
                    this.GetXmlRoot().AppendChild(childXmlNode);
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
            return childXmlNode;
        }
        /// <summary>
        /// 添加子结点
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="childNodeName">子结点名称</param>
        /// <returns>添加的子结点</returns>
        public XmlNode AddChildNode(XmlNode parentNode,string childNodeName)
        {
            XmlNode childXmlNode=null;
            if (parentNode != null)//如果此节点存在
            {
                childXmlNode = xmlDoc.CreateElement(childNodeName);
                parentNode.AppendChild(childXmlNode);
            }
            else
            {//如果不存在就放父节点添加
                this.GetXmlRoot().AppendChild(childXmlNode);
            }
            return childXmlNode;
        }
        /// <summary>
        /// 向一个已经存在的父节点中插入一个子节点,并添加一个属性
        /// </summary>
        /// <param name="parentNodePath">父节点</param>
        /// <param name="childnodename">子节点名称</param>
        /// <param name="NodeAttribute">属性名称</param>
        /// <param name="NodeAttributeValue">属性值</param>
        public void AddChildNode(string parentNodePath, string childnodename, string NodeAttribute, string NodeAttributeValue)
        {
            try
            {
                XmlNode parentXmlNode = xmlDoc.SelectSingleNode(parentNodePath);
                XmlNode childXmlNode = null;
                if (!((parentXmlNode) == null))//如果此节点存在
                {
                    childXmlNode = xmlDoc.CreateElement(childnodename);
                    //添加属性
                    XmlAttribute nodeAttribute = this.xmlDoc.CreateAttribute(NodeAttribute);
                    nodeAttribute.Value = NodeAttributeValue;
                    childXmlNode.Attributes.Append(nodeAttribute);
                    parentXmlNode.AppendChild(childXmlNode);
                }
                else
                {//如果不存在就放父节点添加
                    this.GetXmlRoot().AppendChild(childXmlNode);
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        /// <summary>
        /// 向一个节点添加属性,值为空
        /// </summary>
        /// <param name="NodePath">节点路径</param>
        /// <param name="NodeAttribute">属性名</param>
        public void AddAttribute(string NodePath, string NodeAttribute)
        {
            privateAddAttribute(NodePath, NodeAttribute, "");
        }
        /// <summary>
        /// 向一个节点添加属性,并赋值***
        /// </summary>
        /// <param name="childXmlNode">子节点</param>
        /// <param name="NodeAttribute">属性名称</param>
        /// <param name="NodeAttributeValue">属性值</param>
        public void AddAttribute(XmlNode childXmlNode, string NodeAttribute, string NodeAttributeValue)
        {
            XmlAttribute nodeAttribute = this.xmlDoc.CreateAttribute(NodeAttribute);
            nodeAttribute.Value = NodeAttributeValue;
            childXmlNode.Attributes.Append(nodeAttribute);
        }
        /// <summary>
        /// 向一个节点添加属性
        /// </summary>
        /// <param name="NodePath">节点路径</param>
        /// <param name="NodeAttribute">属性名</param>
        /// <param name="NodeAttributeValue">属性值</param>
        private void privateAddAttribute(string NodePath, string NodeAttribute, string NodeAttributeValue)
        {
            try
            {
                XmlNode nodePath = xmlDoc.SelectSingleNode(NodePath);
                if (!(nodePath == null))
                {
                    XmlAttribute nodeAttribute = this.xmlDoc.CreateAttribute(NodeAttribute);
                    nodeAttribute.Value = NodeAttributeValue;
                    nodePath.Attributes.Append(nodeAttribute);
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        /// <summary>
        /// 向一个节点添加属性,并赋值
        /// </summary>
        /// <param name="NodePath">节点路径</param>
        /// <param name="NodeAttribute">属性名</param>
        /// <param name="NodeAttributeValue">属性值</param>
        public void AddAttribute(string NodePath, string NodeAttribute, string NodeAttributeValue)
        {
            privateAddAttribute(NodePath, NodeAttribute, NodeAttributeValue);
        }
        /// <summary>
        /// 删除节点的一个属性
        /// </summary>
        /// <param name="NodePath">节点所在的xpath表达式</param>
        /// <param name="NodeAttribute">属性名</param>
        public void DeleteAttribute(string NodePath, string NodeAttribute)
        {
            XmlNodeList nodePath = this.xmlDoc.SelectNodes(NodePath);
            if (!(nodePath == null))
            {
                foreach (XmlNode tempxn in nodePath)
                {
                    XmlAttributeCollection xmlAttr = tempxn.Attributes;
                    for (int i = 0; i < xmlAttr.Count; i++)
                    {
                        if (xmlAttr.Item(i).Name == NodeAttribute)
                        {
                            tempxn.Attributes.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 删除节点的一个属性,当其属性值等于给定的值时
        /// </summary>
        /// <param name="NodePath">节点所在的xpath表达式</param>
        /// <param name="NodeAttribute">属性名</param>
        /// <param name="NodeAttributeValue">属性值</param>
        public void DeleteAttribute(string NodePath, string NodeAttribute, string NodeAttributeValue)
        {
            XmlNodeList nodePath = this.xmlDoc.SelectNodes(NodePath);
            if (!(nodePath == null))
            {
                foreach (XmlNode tempxn in nodePath)
                {
                    XmlAttributeCollection xmlAttr = tempxn.Attributes;
                    for (int i = 0; i < xmlAttr.Count; i++)
                    {
                        if (xmlAttr.Item(i).Name == NodeAttribute && xmlAttr.Item(i).Value == NodeAttributeValue)
                        {
                            tempxn.Attributes.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="tempXmlNode">节点名称</param>
        public void DeleteXmlNode(string tempXmlNode)
        {
            XmlNodeList nodePath = this.xmlDoc.SelectNodes(tempXmlNode);
            if (!(nodePath == null))
            {
                foreach (XmlNode xn in nodePath)
                {
                    xn.ParentNode.RemoveChild(xn);
                }
            }
        }
        /// <summary>
        /// 节点插入事件
        /// </summary>
        /// <param name="src">对象</param>
        /// <param name="args">事件</param>
        private void nodeInsertEvent(Object src, XmlNodeChangedEventArgs args)
        {
            //保存设置
            SaveXmlDocument();
        }
        /// <summary>
        /// 节点删除事件
        /// </summary>
        /// <param name="src">对象</param>
        /// <param name="args">事件</param>
        private void nodeDeleteEvent(Object src, XmlNodeChangedEventArgs args)
        {
            //保存设置
            SaveXmlDocument();
        }
        /// <summary>
        /// 节点更新事件
        /// </summary>
        /// <param name="src">对象</param>
        /// <param name="args">事件</param>
        private void nodeUpdateEvent(Object src, XmlNodeChangedEventArgs args)
        {
            //保存设置
            SaveXmlDocument();
        }
        /// <summary>
        /// 功能: 
        /// 保存XML文件
        /// </summary>
        public void SaveXmlDocument()
        {
            try
            {
                //保存设置的结果
                if (this.xmlFilePathType == enumXmlPathType.AbsolutePath)
                {
                    Savexml(xmlFilePath);
                }
                else if (this.xmlFilePathType == enumXmlPathType.VirtualPath)
                {
                    Savexml(HttpContext.Current.Server.MapPath(xmlFilePath));
                }
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        /// <summary>
        /// 功能: 
        /// 保存XML文件
        /// </summary>
        /// <param name="tempXMLFilePath">XML文件路径</param>
        public void SaveXmlDocument(string tempXMLFilePath)
        {
            try
            {
                //保存设置的结果
                Savexml(tempXMLFilePath);
            }
            catch (XmlException xmle)
            {
                throw xmle;
            }
        }
        /// <summary>
        /// 保存XML文件
        /// </summary>
        /// <param name="filepath">XML文件路径</param>
        private void Savexml(string filepath)
        {
            xmlDoc.Save(filepath);
        }
    }
}
