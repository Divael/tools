/*
====================================================================
||Copyright(C) 2008 用于交流学习使用，未经允许不得用于商业用途    ||
||功  能：XML的简单操作                                           ||
||作  者：Aidy Dream                                              ||
||  QQ  ：4468722                                                 ||
||E-mail：aidydream@163.com                                       ||
||版  本：1.0.0.0 未经过任何BUG修改                               ||
====================================================================
 */
//<?xml version="1.0" encoding="utf-8" ?>
//<config>
//  <l1>
//    <id name="编号" type="xs:int">1</id>
//    <name name="姓名" type="xs:string">Aidy Dream</name>
//    <age name="年龄" type="xs:int" minOccurs="0">25</age>
//    <sex name="性别" type ="xs:string">男</sex>
//  </l1>
//  <l1>
//    <id name="编号" type="xs:int">2</id>
//    <name name="姓名" type="xs:string">Joe Yu</name>
//    <age name="年龄" type="xs:int" minOccurs="0">27</age>
//    <sex name="性别" type ="xs:string">男</sex>
//  </l1>
//</config>
using System;
using System.Collections.Generic;
using System.Xml;
//两个构造函数中，一个是XML文件路径，一个文件路径加上节点路径，在重载的函数中有的需要设置节点路径
//方法为：xmlManager.NodePath = "\\子节点\子节点……"
//所有异常全部抛出，需要在程序捕捉
namespace Tools
{
    /// <summary>
    /// <para>　</para>
    /// <para>　常用工具类——XML操作类</para>
    /// <para>　------------------------------------------------</para>
    /// </summary>
    public class XmlHelper
    {
        #region 变量的定义

        private XmlDocument _document = new XmlDocument();
        private string _xmlPath;//文件路径
        private string _nodePath;//节点路径        
        /// <summary>
        /// 操作的XML文档路径＝文件路径＋文件全名
        /// </summary>
        public string XmlPath
        {
            get { return _xmlPath; }
            set { _xmlPath = value; }
        }
        /// <summary>
        /// XML文档中的节点路径"//根节点/子节点/子节点……"
        /// </summary>
        public string NodePath
        {
            get { return _nodePath; }
            set { _nodePath = value; }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xmlPath">XML文档路径+xml文件全名</param>
        /// <param name="nodePath"></param>
        public XmlHelper(string xmlPath, string nodePath)
        {
            this._xmlPath = xmlPath;
            this._nodePath = nodePath;
            this._document.Load(xmlPath);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xmlPath">默认程序运行路径和XML文件名</param>
        public XmlHelper(string xmlPath)
        {
            this._xmlPath = xmlPath;
            this._document.Load(xmlPath);
        }
        #endregion

        #region 各种操作方法
        /// <summary>
        /// 读出指定路径XML文档的全部内容
        /// </summary>
        /// <returns>XML文档的全部内容</returns>
        public string Out()
        {
            return this._document.OuterXml;
        }
        #region 获取节点
        /// <summary>
        /// 获取XML中节点的内容
        /// </summary>
        /// <param name="nodeName">节点名称</param>
        /// <returns>List(string)集合</returns>
        public List<string> GetNode(string nodeName)
        {
            try
            {
                List<string> list = new List<string>();
                XmlNodeList nodeList = this._document.GetElementsByTagName(nodeName);
                //string[] li = new string[nodeList.Count];
                foreach (XmlNode node in nodeList)
                {
                    list.Add(node.InnerText);
                }
                return list;
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));

            }
        }
        /// <summary>
        /// 获取XML中节点的内容
        /// </summary>
        /// <param name="i">第i段数据</param>
        /// <param name="j">第j个属性</param>
        /// <param name="path">用户指定节点路径</param>
        /// <returns></returns>
        public string GetNode(int i, int j, string path)
        {
            try
            {
                XmlNodeList nodeList = this._document.SelectNodes(path);

                return nodeList.Item(i - 1).ChildNodes.Item(j - 1).InnerText;
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
        /// <summary>
        /// 获取XML中节点的内容
        /// </summary>
        /// <param name="i">第i段数据</param>
        /// <param name="j">第j个属性</param>
        /// <returns></returns>
        public string GetNode(int i, int j)
        {
            try
            {
                XmlNodeList nodeList = this._document.SelectNodes(_nodePath);

                return nodeList.Item(i - 1).ChildNodes.Item(j - 1).InnerText;
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
        /// <summary>
        /// 获取XML中节点的内容
        /// </summary>
        /// <param name="i">第i段数据</param>
        /// <param name="nodeName">节点名</param>
        /// <param name="nodePath">用户指定路径</param>
        /// <returns></returns>
        public string GetNode(int i, string nodePath, string nodeName)
        {
            try
            {
                XmlNodeList nodeList = this._document.SelectNodes(nodePath);
                for (int j = 0; j <= nodeList.Item(i - 1).ChildNodes.Count; j++)
                {
                    if (nodeList.Item(i - 1).ChildNodes.Item(j).Name == nodeName)
                        return nodeList.Item(i - 1).ChildNodes.Item(j).InnerText;

                }
                return "nofind";
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
        /// <summary>
        /// 获取XML中节点的内容
        /// </summary>
        /// <param name="i">第i段数据</param>
        /// <param name="nodeName">节点名</param>
        /// <returns></returns>
        public string GetNode(int i, string nodeName)
        {
            try
            {
                XmlNodeList nodeList = this._document.SelectNodes(_nodePath);
                for (int j = 0; j <= nodeList.Item(i - 1).ChildNodes.Count; j++)
                {
                    if (nodeList.Item(i - 1).ChildNodes.Item(j).Name == nodeName)
                        return nodeList.Item(i - 1).ChildNodes.Item(j).InnerText;

                }
                return "nofind";
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }

        #endregion

        #region 获取节点数
        /// <summary>
        /// 返回符合指定名称的节点数
        /// </summary>
        /// <param name="nodeName">节点名</param>
        /// <returns>节点数</returns>
        public int Count(string nodeName)
        {
            try
            {
                XmlNodeList nodeList = this._document.GetElementsByTagName(nodeName);
                return nodeList.Count;
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
        /// <summary>
        /// 使用设置的节点路径返回符合指定名称的节点数
        /// </summary>       
        /// <returns>节点数</returns>
        public int Count()
        {

            try
            {

                XmlNodeList nodeList = this._document.SelectNodes(_nodePath);
                return nodeList.Count;
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }

        }
        /// <summary>
        /// 返回指符合指定名称的节点集中第i个集合的子节点数
        /// </summary>
        /// <param name="i"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public int CountChilds(int i, string nodeName)
        {
            try
            {
                XmlNodeList nodeList = this._document.GetElementsByTagName(nodeName);
                if (nodeList.Count > 0)
                {
                    return nodeList.Item(i - 1).ChildNodes.Count;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
        /// <summary>
        /// 返回指符合指定名称的节点的子节点数
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public int CountChilds(string nodeName)
        {
            try
            {
                int counts = 0;
                XmlNodeList nodeList = this._document.GetElementsByTagName(nodeName);
                if (nodeList.Count > 0)
                {
                    for (int i = 0; i < nodeList.Count; i++)
                    {
                        counts += nodeList.Item(i).ChildNodes.Count;
                    }
                    return counts;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
        #endregion

        #region 修改节点值
        /// <summary>
        /// 修改指定节点的值
        /// </summary>
        /// <param name="nodeName">节点名称</param>
        /// <param name="newValue">新值</param>
        /// <returns>返回更改的节点数</returns>
        public int SetNode(string nodeName, string newValue)
        {
            try
            {
                XmlNodeList nodeList = this._document.GetElementsByTagName(nodeName);
                foreach (XmlNode node in nodeList)
                {
                    node.InnerText = newValue;
                }
                this._document.Save(_xmlPath);
                return nodeList.Count;
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
        /// <summary>
        /// 修改指定节点的值
        /// </summary>
        /// <param name="i">第i段数据</param>
        /// <param name="nodePath">节点路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="newValue">新值</param>
        public void SetNode(int i, string nodePath, string nodeName, string newValue)
        {
            try
            {
                XmlNodeList nodeList = this._document.SelectNodes(nodePath);
                if (nodeList.Count > 0 && nodeList.Count <= i)
                {
                    for (int j = 0; j < nodeList.Item(i - 1).ChildNodes.Count; j++)
                    {
                        if (nodeList.Item(i - 1).ChildNodes.Item(j).Name == nodeName)
                        {
                            nodeList.Item(i - 1).ChildNodes.Item(j).InnerText = newValue;
                            this._document.Save(_xmlPath);

                        }
                    }
                }
                else
                {
                    throw (new Exception("并无此子集"));
                }
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }

        /// <summary>
        /// 修改指定节点的值,使用设置的节点路径
        /// </summary>
        /// <param name="i">第i段数据</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="newValue">新值</param>
        public void SetNode(int i, string nodeName, string newValue)
        {
            try
            {
                XmlNodeList nodeList = this._document.SelectNodes(_nodePath);
                if (nodeList.Count > 0 && nodeList.Count <= i)
                {
                    for (int j = 0; j < nodeList.Item(i - 1).ChildNodes.Count; j++)
                    {
                        if (nodeList.Item(i - 1).ChildNodes.Item(j).Name == nodeName)
                        {
                            nodeList.Item(i - 1).ChildNodes.Item(j).InnerText = newValue;
                            this._document.Save(_xmlPath);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }
        }
        #endregion

        #region　插入节点
        /// <summary>
        /// 为指定的节点(集插)入一个子节点
        /// </summary>
        /// <param name="parentName">父节点</param>
        /// <param name="nodeName">子节点名</param>
        /// <param name="nodeVale">子节点值</param>
        public void InsetNode(string parentName, string nodeName, string nodeVale)
        {
            XmlNodeList nodeList = this._document.GetElementsByTagName(parentName);
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlElement newElement = _document.CreateElement(nodeName);
                //newElement.SetAttribute(nodeName,nodeVale);//会插入如<id id = "0012"/>的节点
                nodeList.Item(i).AppendChild(newElement);//<id>002</id>
                newElement.InnerText = nodeVale;
            }
            this._document.Save(this._xmlPath);

        }
        /// <summary>
        /// 为指定的节点(集)插入一个子节点
        /// </summary>
        /// <param name="i"></param>
        /// <param name="parentName">父节点</param>
        /// <param name="nodeName">子节点名</param>
        /// <param name="nodeValue">子节点值</param>
        public void InsetNode(int i, string parentName, string nodeName, string nodeValue)
        {
            try
            {
                XmlNodeList nodeList = this._document.GetElementsByTagName(parentName);
                XmlElement newElement = _document.CreateElement(nodeName);
                //newElement.SetAttribute(nodeName, nodeValue);
                if (nodeList.Count > 0 && nodeList.Count <= i)
                {
                    nodeList.Item(i - 1).AppendChild(newElement);
                    newElement.InnerText = nodeValue;

                    this._document.Save(this._xmlPath);
                }
                else
                {
                    throw (new Exception("无此节点"));
                }
            }
            catch (Exception e)
            {
                throw (new Exception(e.Message));
            }

        }
        /// <summary>
        /// 插入根节点（第二级根节点）
        /// </summary>
        /// <param name="rootName">节点名</param>
        /// <param name="nodeName">子节点名</param>
        /// <param name="nodeValue">子节点值</param>
        public void InsertRootNode(string rootName, string[] nodeName, string[] nodeValue)
        {
            XmlElement root = _document.DocumentElement;
            XmlElement newRoot = _document.CreateElement(rootName);
            root.AppendChild(newRoot);
            for (int i = 0; i < nodeName.Length; i++)
            {
                XmlElement newChild = _document.CreateElement(nodeName[i]);
                newRoot.AppendChild(newChild);
                newChild.InnerText = nodeValue[i];
            }
            this._document.Save(this._xmlPath);
        }


        //public string geT(string name)
        //{
        //    XmlNodeList n = _document.GetElementsByTagName(name);
        //    XmlElement nn = _document.
        //    return nn[0].Attributes.ToString();
        //}
        #endregion

        #region 删除节点
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="parentName"></param>
        /// <param name="noteName"></param>
        public void DeleteNote(string parentName, string noteName)
        {
            XmlNodeList nodeList = _document.GetElementsByTagName(parentName);
            //for (int i = 0; i < nodeList.Count; i++)
            //    if (nodeList.Item(i).ParentNode.Name == parentName)
            //        nodeList.Item(i).ParentNode.RemoveChild(nodeList.Item(i));
            //this._document.Save(this._xmlPath);            
            //for (int i = 0; i < nodeList.Count; i++)
            //    for (int j = 0; j < nodeList.Item(i).ChildNodes.Count; j++)
            //        if (nodeList.Item(i).ChildNodes.Item(j).Name == noteName)
            //            nodeList.Item(i).RemoveChild(nodeList.Item(i).ChildNodes.Item(j));
            //this._document.Save(this._xmlPath);
            foreach (XmlNode node in nodeList)
            {
                foreach (XmlNode nodechild in node.ChildNodes)
                    if (nodechild.Name == noteName)
                    {
                        node.RemoveChild(nodechild);
                    }
            }
            this._document.Save(this._xmlPath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parmentName"></param>
        public void DeleteNote(string parmentName)
        {
            XmlNodeList nodeList = this._document.GetElementsByTagName(parmentName);
            foreach (XmlNode node in nodeList)
                node.RemoveAll();
            this._document.Save(_xmlPath);
        }
        /// <summary>
        /// 删除所有节点
        /// </summary>
        public void DeleteAll()
        {
            XmlElement element = this._document.DocumentElement;
            element.RemoveAll();
            this._document.Save(this._xmlPath);
        }

        #endregion


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="xmlName"></param>
        /// <param name="rootElement"></param>
        /// <returns></returns>
        public Boolean Save(string xmlName, string rootElement)
        {
            try
            {
                string savePath = System.IO.Directory.GetCurrentDirectory() + "\\" + xmlName;
                XmlDocument document = new XmlDocument();
                XmlElement element = document.CreateElement(rootElement);
                document.AppendChild(element);
                document.Save(savePath);
                return true;
            }
            catch (XmlException xe)
            {
                throw (new Exception(xe.Message));
            }
        }
        #endregion
    }
}
