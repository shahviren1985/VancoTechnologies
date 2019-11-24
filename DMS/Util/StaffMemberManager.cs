using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Util
{
    public class StaffMemberManager
    {
        public static string GetStaffMembersIdXML(List<string> ids)
        {
            try
            {
                XmlDocument doc = new XmlDocument();

                XmlNode root = doc.CreateNode(XmlNodeType.Element, "StaffMembers", "");

                foreach (string id in ids)
                {
                    XmlNode member = doc.CreateNode(XmlNodeType.Element, "Member", "");
                    XmlAttribute memberId = doc.CreateAttribute("id");
                    memberId.Value = id;
                    member.Attributes.Append(memberId);

                    root.AppendChild(member);
                }

                return root.OuterXml;
            }
            catch (Exception ex)
            {                
                throw;
            }            
        }

        public static List<string> GetStaffIdList(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return null;
            }

            try
            {
                xml = UnescapeFormat.UnescapeXML(xml);

                List<string> ids = new List<string>();
                XmlDocument doc = new XmlDocument();
                
                doc.LoadXml(xml);

                XmlNode root = doc.SelectSingleNode("StaffMembers");
                XmlNodeList members = root.SelectNodes("Member");
                
                foreach (XmlNode member in members)
                {
                    ids.Add(member.Attributes["id"].Value);
                }

                return ids;
            }
            catch (Exception ex)
            {                
                throw;
            }
        }
    }
}
