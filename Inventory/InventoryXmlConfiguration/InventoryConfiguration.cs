using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Configuration;
using System.IO;
using System.Xml;

namespace ITM.InventoryXmlConfiguration
{
    public class InventoryConfiguration
    {
        public string file;

        public XmlDocument OpenFile(string configFilePath)
        {
            try
            {
                if (string.IsNullOrEmpty(configFilePath))
                {
                    file = ConfigurationManager.AppSettings["BASE_PATH"] + "/Release/Default/Configuration.xml";
                }
                else
                {
                    file = configFilePath;
                }

                XmlDocument doc;
                
                if (!File.Exists(file))
                {
                    throw new Exception("Configuration file does not exist. Please add XML Configuration file in Release folder.");
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

        public LoggerDetails GetLoggerDetails(string configFilePath)
        {
            try
            {
                LoggerDetails logDetails = new LoggerDetails();

                XmlDocument doc = OpenFile(configFilePath);

                XmlNode newlogger = doc.SelectSingleNode("Configuration/Logger");

                if (newlogger != null)
                {
                    if (newlogger.SelectSingleNode("LogLevel") != null)
                    {
                        logDetails.LogLevel = newlogger.SelectSingleNode("LogLevel").InnerText;
                    }

                    if (newlogger.SelectSingleNode("LogPath") != null)
                    {
                        logDetails.LogFilePath = newlogger.SelectSingleNode("LogPath").InnerText;
                    }

                    if (newlogger.SelectSingleNode("LogFileName") != null)
                    {
                        logDetails.LogFileName = newlogger.SelectSingleNode("LogFileName").InnerText;
                    }

                    if (newlogger.SelectSingleNode("LogFileSize") != null)
                    {
                        logDetails.LogFileSizeMB = newlogger.SelectSingleNode("LogFileSize").InnerText;
                    }

                    if (newlogger.SelectSingleNode("LogType") != null)
                    {
                        logDetails.LogType = newlogger.SelectSingleNode("LogType").InnerText;
                    }

                    if (newlogger.SelectSingleNode("LogDBConnectionString") != null)
                    {
                        logDetails.LogDBConnectionString = newlogger.SelectSingleNode("LogDBConnectionString").InnerText;
                    }

                    return logDetails;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("11583");
            }
            return null;
        }

        public EmailDetails GetEmailSettings(string configFilePath)
        {
            try
            {
                EmailDetails emailDetails = new EmailDetails();

                XmlDocument doc = OpenFile(configFilePath);

                XmlNode newEmail = doc.SelectSingleNode("Configuration/EmailSettings");

                if (newEmail != null)
                {
                    if (newEmail.SelectSingleNode("EmailAddress") != null)
                    {
                        emailDetails.EmailAddress = newEmail.SelectSingleNode("EmailAddress").InnerText;
                    }

                    if (newEmail.SelectSingleNode("UserName") != null)
                    {
                        emailDetails.UserName = newEmail.SelectSingleNode("UserName").InnerText;
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
                throw new Exception("11584");
            }
            return null;
        }       
    }   
}
