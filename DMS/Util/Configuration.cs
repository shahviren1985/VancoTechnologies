using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Util;
using System.Data;
using System.Web;

namespace AA.ConfigurationsManager
{
    public class Configurations
    {
        public static string file;

        // open existing file or create new file
        public static XmlDocument OpenFile()
        {
            try
            {
                file = HttpContext.Current.Server.MapPath("~/Release/Configuration.xml");

                XmlDocument doc;
                //Verify whether a file is exists or not
                if (!File.Exists(file))
                {
                    doc = new XmlDocument();
                    XmlNode root = doc.CreateNode(XmlNodeType.Element, "Configuration", "");

                    XmlNode logger = doc.CreateNode(XmlNodeType.Element, "Logger", "");
                    XmlNode emailSetting = doc.CreateNode(XmlNodeType.Element, "EmailSettings", "");
                    XmlNode seatNumber = doc.CreateNode(XmlNodeType.Element, "SeatNumber", "");
                    XmlNode semester = doc.CreateNode(XmlNodeType.Element, "Semesters", "");
                    XmlNode pages = doc.CreateNode(XmlNodeType.Element, "Pages", "");
                    root.AppendChild(logger);
                    root.AppendChild(emailSetting);
                    root.AppendChild(seatNumber);
                    root.AppendChild(semester);
                    root.AppendChild(pages);
                    doc.AppendChild(root);
                }
                else
                {
                    doc = new XmlDocument();
                    doc.Load(file);
                }

                return doc;
            }
            catch (Exception ex)
            {
                throw new Exception("11576");
            }
        }

        public static EmailDetails GetEmailSettings()
        {
            try
            {
                EmailDetails emailDetails = new EmailDetails();

                XmlDocument doc = OpenFile();

                XmlNode newEmail = doc.SelectSingleNode("Configuration/EmailSettings");

                if (newEmail != null)
                {
                    if (newEmail.SelectSingleNode("EmailAddress") != null)
                    {
                        emailDetails.EmailAddress = newEmail.SelectSingleNode("EmailAddress").InnerText;
                    }
                    if (newEmail.SelectSingleNode("EmailFromDisplayName") != null)
                    {
                        emailDetails.EmailFromDisplayName = newEmail.SelectSingleNode("EmailFromDisplayName").InnerText;
                    }
                    if (newEmail.SelectSingleNode("NewUserEmailSubject") != null)
                    {
                        emailDetails.NewUserEmailSubject = newEmail.SelectSingleNode("NewUserEmailSubject").InnerText;
                    }
                    if (newEmail.SelectSingleNode("IsMailSend") != null)
                    {
                        emailDetails.IsMailSend = bool.Parse(newEmail.SelectSingleNode("IsMailSend").InnerText);
                    }
                    if (newEmail.SelectSingleNode("EmailUseDefaultCredentials") != null)
                    {
                        emailDetails.EmailUseDefaultCredentials = bool.Parse(newEmail.SelectSingleNode("EmailUseDefaultCredentials").InnerText);
                    }
                    if (newEmail.SelectSingleNode("UserName") != null)
                    {
                        emailDetails.UserName = newEmail.SelectSingleNode("UserName").InnerText;
                    }
                    if (newEmail.SelectSingleNode("EmailHost") != null)
                    {
                        emailDetails.EmailHost = newEmail.SelectSingleNode("EmailHost").InnerText;
                    }
                    if (newEmail.SelectSingleNode("Password") != null)
                    {
                        emailDetails.Password = newEmail.SelectSingleNode("Password").InnerText;
                    }

                    if (newEmail.SelectSingleNode("Port") != null)
                    {
                        emailDetails.Port = newEmail.SelectSingleNode("Port").InnerText;
                    }

                    if (newEmail.SelectSingleNode("Server") != null)
                    {
                        emailDetails.Server = newEmail.SelectSingleNode("Server").InnerText;
                    }

                    if (newEmail.SelectSingleNode("IsSSLEnabled") != null)
                    {
                        emailDetails.IsSSLEnable = bool.Parse(newEmail.SelectSingleNode("IsSSLEnabled").InnerText);
                    }

                    if (newEmail.SelectSingleNode("IsHTMLResponse") != null)
                    {
                        emailDetails.IsHTMLResponse = bool.Parse(newEmail.SelectSingleNode("IsHTMLResponse").InnerText);
                    }

                    return emailDetails;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("");
            }
            return null;
        }
        public static ConnectionString GetDatabaseConnection()
        {
            try
            {
                XmlDocument doc = OpenFile();

                XmlNode connection = doc.SelectSingleNode("Configuration/ConnectionString");

                //  List<ConnectionString> connectionList = new List<ConnectionString>();
                if (connection != null)
                {
                    ConnectionString conn = new ConnectionString();
                    conn.ConnectionStringName = connection.SelectSingleNode("ConnectionName").InnerText;
                    conn.Connectionstring = connection.SelectSingleNode("Connection").InnerText;
                    return conn;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static List<DocumentType> GetDocumnettypes()
        {
            try
            {
                List<DocumentType> documentlist = new List<DocumentType>();
                XmlDocument doc = OpenFile();

                XmlNode Document = doc.SelectSingleNode("Configuration/Documents");



                if (Document != null)
                {
                    XmlNodeList doctype = Document.SelectNodes("DocumentType");
                    foreach (XmlNode node in doctype)
                    {
                        DocumentType item = new DocumentType();
                        item.Documents = node.InnerText;
                        //  item.amount = node.Attributes["amount"].Name;
                        documentlist.Add(item);
                    }
                }

                return documentlist;


            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static bool GetAccessRights()
        {
            try
            {
                bool rights = false;
                List<DocumentType> documentlist = new List<DocumentType>();
                XmlDocument doc = OpenFile();

                XmlNode access = doc.SelectSingleNode("Configuration/Access");



                if (access != null)
                {
                    rights = bool.Parse(access.SelectSingleNode("Accessright").InnerText);

                }

                return rights;


            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public class Encryption
        {
            public string EncryptionTypeName { get; set; }
            public string Salt { get; set; }
            public string HashAlgorithm { get; set; }
            public int PasswordIterations { get; set; }
            public string passworditerations { get; set; }
            public string InitialVector { get; set; }
            public string Password { get; set; }
        }

        public class ConnectionString
        {
            public string Connectionstring { get; set; }
            public string ConnectionStringName { get; set; }
        }
        public class ReminderSetting
        {
            public string ReminderDays { get; set; }
            public bool SendReminderMailOnlyAdmin { get; set; }
            public bool SendReminderMailStudentWithAdmin { get; set; }
        }

        public class BasePath
        {
            public string path { get; set; }
        }

        public class DocumentType
        {
            public String Documents { get; set; }
        }
    }
}
