using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace AA.DAO
{
    public class GetXMLUsers
    {

        public static string GetXmlFormat(List<UserDetails> users)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlNode root = doc.CreateNode(XmlNodeType.Element, "Users", string.Empty);
                foreach (UserDetails u in users)
                {
                    XmlNode user = doc.CreateNode(XmlNodeType.Element, "User", string.Empty);
                    user.InnerText = u.Id.ToString();
                    root.AppendChild(user);

                }
                return root.OuterXml;
            }
            catch (Exception ex)
            {
                
                throw;
            }
            return null;
        }
        public static List<UserDetails> GetXMLINTOList(string xmluesrlist)
        {
            try
            {
                if (string.IsNullOrEmpty(xmluesrlist))
                {
                    return null;
                }


                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmluesrlist);
                List<UserDetails> userlist = new List<UserDetails>();
                XmlNode root = doc.SelectSingleNode("Users");

                XmlNodeList user = root.SelectNodes("User");
                foreach (XmlNode node in user)
                {
                    UserDetails tempuser = new UserDetails();
                    tempuser.Id = int.Parse(node.InnerText);
                    userlist.Add(tempuser);

                }

                return userlist;
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
    }
}
