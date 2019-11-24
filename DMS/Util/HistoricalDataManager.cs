using System;
using System.Collections;
using System.Security;
using System.Xml;

namespace Util
{
    public class HistoricalDataManager
    {
        public static string CreateHistory(string userName, Hashtable table)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlNode root = doc.CreateNode(XmlNodeType.Element, "Details", "");
                XmlNode detail = doc.CreateNode(XmlNodeType.Element, "Detail", "");

                //create attributs
                XmlAttribute mode = doc.CreateAttribute("Mode");
                mode.Value = "Create";

                XmlAttribute date = doc.CreateAttribute("Date");
                date.Value = DateTime.Now.ToString();

                XmlAttribute user = doc.CreateAttribute("User");
                user.Value = userName;

                //add attributes to detail node
                detail.Attributes.Append(mode);
                detail.Attributes.Append(date);
                detail.Attributes.Append(user);

                XmlNode orignal = doc.CreateNode(XmlNodeType.Element, "Orignal", "");
                detail.AppendChild(orignal);

                XmlNode newValue = doc.CreateNode(XmlNodeType.Element, "New", "");

                IDictionaryEnumerator num = table.GetEnumerator();

                while (num.MoveNext())
                {

                    XmlNode add = doc.CreateNode(XmlNodeType.Element, "add", "");

                    XmlAttribute key = doc.CreateAttribute("key");
                    key.Value = num.Key.ToString();

                    add.Attributes.Append(key);

                    add.InnerText = num.Value.ToString();

                    newValue.AppendChild(add);
                }

                detail.AppendChild(newValue);
                root.AppendChild(detail);

                //return root.OuterXml;                
                return SecurityElement.Escape(root.OuterXml);
            }
            catch (Exception ex)
            {
                throw new Exception("11626");
            }
        }

        public static string UpdateHistory(string userName, Hashtable oldTable, Hashtable newTable)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlNode root = doc.CreateNode(XmlNodeType.Element, "Details", "");
                XmlNode detail = doc.CreateNode(XmlNodeType.Element, "Detail", "");

                //create attributs
                XmlAttribute mode = doc.CreateAttribute("Mode");
                mode.Value = "Update";

                XmlAttribute date = doc.CreateAttribute("Date");
                date.Value = DateTime.Now.ToString();

                XmlAttribute user = doc.CreateAttribute("User");
                user.Value = userName;

                //add attributes to detail node
                detail.Attributes.Append(mode);
                detail.Attributes.Append(date);
                detail.Attributes.Append(user);

                //create old value node
                XmlNode orignal = doc.CreateNode(XmlNodeType.Element, "Orignal", "");

                IDictionaryEnumerator oldVal = oldTable.GetEnumerator();

                while (oldVal.MoveNext())
                {
                    XmlNode add = doc.CreateNode(XmlNodeType.Element, "add", "");

                    XmlAttribute key = doc.CreateAttribute("key");
                    key.Value = oldVal.Key.ToString();

                    add.Attributes.Append(key);

                    add.InnerText = oldVal.Value.ToString();

                    orignal.AppendChild(add);
                }

                detail.AppendChild(orignal);

                //create new value node
                XmlNode newValue = doc.CreateNode(XmlNodeType.Element, "New", "");

                IDictionaryEnumerator newVal = newTable.GetEnumerator();

                while (newVal.MoveNext())
                {
                    XmlNode add = doc.CreateNode(XmlNodeType.Element, "add", "");

                    XmlAttribute key = doc.CreateAttribute("key");
                    key.Value = newVal.Key.ToString();

                    add.Attributes.Append(key);

                    add.InnerText = newVal.Value == null ? string.Empty : newVal.Value.ToString();

                    newValue.AppendChild(add);
                }

                detail.AppendChild(newValue);
                root.AppendChild(detail);

                //return root.OuterXml;
                return SecurityElement.Escape(root.OuterXml);
            }
            catch (Exception ex)
            {
                throw new Exception("11627");
            }
        }

        public static string AppendHistory(string userName, string orignalXMLChunk, Hashtable table)
        {
            try
            {
                if (string.IsNullOrEmpty(orignalXMLChunk))
                {
                    return orignalXMLChunk;
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(UnescapeFormat.UnescapeXML(orignalXMLChunk));

                XmlNode xnode = doc.SelectSingleNode("/Details");
                XmlNode node = xnode.SelectSingleNode("Detail");

                XmlNode detail = doc.CreateNode(XmlNodeType.Element, "Detail", "");

                //create attributs
                XmlAttribute mode = doc.CreateAttribute("Mode");
                mode.Value = "Append";

                XmlAttribute date = doc.CreateAttribute("Date");
                date.Value = DateTime.Now.ToString();

                XmlAttribute user = doc.CreateAttribute("User");
                user.Value = userName;

                //add attributes to detail node
                detail.Attributes.Append(mode);
                detail.Attributes.Append(date);
                detail.Attributes.Append(user);

                XmlNode orignal = doc.CreateNode(XmlNodeType.Element, "Orignal", "");
                detail.AppendChild(orignal);

                XmlNode newValue = doc.CreateNode(XmlNodeType.Element, "New", "");

                IDictionaryEnumerator num = table.GetEnumerator();

                while (num.MoveNext())
                {
                    XmlNode add = doc.CreateNode(XmlNodeType.Element, "add", "");

                    XmlAttribute key = doc.CreateAttribute("key");
                    key.Value = num.Key.ToString();

                    add.Attributes.Append(key);

                    add.InnerText = num.Value.ToString();

                    newValue.AppendChild(add);
                }

                detail.AppendChild(newValue);
                xnode.InsertBefore(detail, node);

                //return doc.OuterXml;
                return SecurityElement.Escape(doc.OuterXml);
            }
            catch (Exception ex)
            {
                throw new Exception("11628");
            }
        }
    }
}
