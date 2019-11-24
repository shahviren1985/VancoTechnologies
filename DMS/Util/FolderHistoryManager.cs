using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Security;

namespace Util
{
    public class FolderHistoryManager
    {
        public DateTime Date { get; set; }
        public string Activity { get; set; }
        public string User { get; set; }

        public static string CreateHistory(FolderHistoryManager obj)
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                XmlNode root = doc.CreateNode(XmlNodeType.Element, "History", "");
                XmlNode events = doc.CreateNode(XmlNodeType.Element, "Event", "");

                XmlAttribute date = doc.CreateAttribute("date");
                date.Value = obj.Date.ToString();
                events.Attributes.Append(date);

                XmlAttribute activity = doc.CreateAttribute("activity");
                activity.Value = obj.Activity;
                events.Attributes.Append(activity);

                XmlAttribute user = doc.CreateAttribute("user");
                user.Value = obj.User;
                events.Attributes.Append(user);

                root.AppendChild(events);

                //return root.OuterXml;
                return SecurityElement.Escape(root.OuterXml);
            }
            catch (Exception ex)
            {                
                throw;
            }            
        }

        public static string AppendHistory(string xml, FolderHistoryManager obj)
        {
            try
            {
                if (string.IsNullOrEmpty(xml))
                {
                    return xml;
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(UnescapeFormat.UnescapeXML(xml));

                XmlNode root = doc.SelectSingleNode("History");
                XmlNode oldChild = root.FirstChild;

                XmlNode events = doc.CreateNode(XmlNodeType.Element, "Event", "");

                XmlAttribute date = doc.CreateAttribute("date");
                date.Value = obj.Date.ToString();
                events.Attributes.Append(date);

                XmlAttribute activity = doc.CreateAttribute("activity");
                activity.Value = obj.Activity;
                events.Attributes.Append(activity);

                XmlAttribute user = doc.CreateAttribute("user");
                user.Value = obj.User;
                events.Attributes.Append(user);

                root.InsertBefore(events, oldChild);

                //return root.OuterXml;
                return SecurityElement.Escape(root.OuterXml);
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        public static List<FolderHistoryManager> GetHistoryList(string xml)
        {
            try
            {
                List<FolderHistoryManager> historyList = new List<FolderHistoryManager>();
                
                if (string.IsNullOrEmpty(xml))
                {
                    return historyList;
                }

                XmlDocument doc = new XmlDocument();

                doc.LoadXml(UnescapeFormat.UnescapeXML(xml));

                XmlNode root = doc.SelectSingleNode("History");

                XmlNodeList nodeList = root.SelectNodes("Event");

                foreach (XmlNode node in nodeList)
                {
                    FolderHistoryManager obj = new FolderHistoryManager();
                    obj.Activity = node.Attributes["activity"].Value;
                    obj.Date = Convert.ToDateTime(node.Attributes["date"].Value);
                    obj.User = node.Attributes["user"].Value;

                    historyList.Add(obj);
                }

                return historyList;
            }
            catch (Exception ex)
            {
                return null;//throw;
            }
        }
    }
}
