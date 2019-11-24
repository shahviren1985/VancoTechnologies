using AA.LogManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Xml;

/// <summary>
/// Summary description for XmlConfiguration
/// </summary>
public class XmlConfiguration
{

    /*public XmlDocument OpenFile(string logPath, string configurationPath)
    {
        try
        {
            string filePath = string.Empty;

            if (string.IsNullOrEmpty(configurationPath))
            {
                filePath = Path.Combine(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BASE_PATH"] + "/"), "Release/config.xml");
            }
            else
            {
                filePath = configurationPath;
            }


            XmlDocument doc = null;

            //Verify whether a file exists
            if (File.Exists(filePath))
            {
                doc = new XmlDocument();
                doc.Load(filePath);
            }

            return doc;
        }
        catch (Exception ex)
        {
            //logger.Error("ConfigurationXmlParser.cs", "OpenFile", "Error Occured while Open Existing File", ex, logPath);
            throw ex;
        }
    }

    public void GetPdfDocumentContent(ref List<PdfPrintOptions> config, string logPath, string configurationPath)
    {
        try
        {
            config = new List<PdfPrintOptions>();
            XmlDocument doc = OpenFile(logPath, configurationPath);
            XmlNode pdf = doc.SelectSingleNode("Settings/Document/Content");

            if (pdf != null)
            {
                XmlNodeList pdfContent = pdf.SelectNodes("Line");

                foreach (XmlNode line in pdfContent)
                {
                    PdfPrintOptions pdfOptions = new PdfPrintOptions();
                    pdfOptions.Bold = line.Attributes["bold"] != null ? bool.Parse(line.Attributes["bold"].Value) : false;
                    pdfOptions.Display = line.Attributes["show"] != null ? bool.Parse(line.Attributes["show"].Value) : false;
                    pdfOptions.FontSize = line.Attributes["fontsize"] != null ? int.Parse(line.Attributes["fontsize"].Value) : 0;
                    pdfOptions.Id = line.Attributes["id"].Value;
                    pdfOptions.LLX = line.Attributes["llx"] != null ? int.Parse(line.Attributes["llx"].Value) : 0;
                    pdfOptions.LLY = line.Attributes["lly"] != null ? int.Parse(line.Attributes["lly"].Value) : 0;
                    pdfOptions.URX = line.Attributes["urx"] != null ? int.Parse(line.Attributes["urx"].Value) : 0;
                    pdfOptions.URY = line.Attributes["ury"] != null ? int.Parse(line.Attributes["ury"].Value) : 0;
                    pdfOptions.Text = line.InnerText;
                    pdfOptions.LineType = line.Attributes["type"] != null ? (string.IsNullOrEmpty(line.Attributes["type"].Value) ? "text" : line.Attributes["type"].Value) : "text";
                    config.Add(pdfOptions);
                }
            }
        }
        catch (Exception ex)
        {
            //logger.Error("ConfigurationXmlParser.cs", "GetBonafideContent", "Error Occured while getting Bonafide Content", ex, logPath);
            throw ex;
        }
    }

    public void GetRefNumberConfig(string logPath, string configurationPath, out string initials, out string academicYearStartMonth)
    {
        try
        {
            XmlDocument doc = OpenFile(logPath, configurationPath);
            XmlNode initialNode = doc.SelectSingleNode("Settings/OutwardDocument/Initials");
            XmlNode acadYearMonth = doc.SelectSingleNode("Settings/OutwardDocument/AcademicYearStart");

            initials = initialNode != null ? initialNode.InnerText : string.Empty;
            academicYearStartMonth = acadYearMonth != null ? acadYearMonth.InnerText : string.Empty;
            return;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }*/

    public EmailConfig GetEmailConfig(string logPath, string configurationPath)
    {
        EmailConfig config2;
        try
        {
            EmailConfig config = new EmailConfig();
            foreach (XmlNode node in this.OpenFile(logPath, configurationPath).SelectNodes("Settings/Email/add"))
            {
                switch (node.Attributes["key"].Value)
                {
                    case "Host":
                        config.Host = node.Attributes["value"].Value;
                        break;

                    case "Port":
                        config.PortNumber = int.Parse(node.Attributes["value"].Value);
                        break;

                    case "User":
                        config.UserName = node.Attributes["value"].Value;
                        break;

                    case "Password":
                        config.Password = node.Attributes["value"].Value;
                        break;

                    case "MailDisplayName":
                        config.MailDisplayName = node.Attributes["value"].Value;
                        break;

                    case "ToAddress":
                        config.ToAddress = node.Attributes["value"].Value.Split(new char[] { ',' }).ToList<string>();
                        break;

                    case "FromAddress":
                        config.FromAddress = node.Attributes["value"].Value;
                        break;

                    case "Subject":
                        config.Subject = node.Attributes["value"].Value;
                        break;

                    case "EnableSsl":
                        config.EnableSSL = bool.Parse(node.Attributes["value"].Value);
                        break;

                    case "UseDefaultCredentials":
                        config.UseDefaultCredentials = bool.Parse(node.Attributes["value"].Value);
                        break;

                    case "ToCC":
                        config.CC = node.Attributes["value"].Value.Split(new char[] { ',' }).ToList<string>();
                        break;

                    case "ToBCC":
                        config.BCC = node.Attributes["value"].Value.Split(new char[] { ',' }).ToList<string>();
                        break;
                }
            }
            config2 = config;
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return config2;
    }

    public EmailConfig GetEmailSendConfig(string logPath, string configurationPath)
    {
        EmailConfig config2;
        try
        {
            EmailConfig config = new EmailConfig
            {
                IsSendCreateMail = false,
                IsSendUpdateMail = false,
                IsSendCommentMail = false,
                IsSendApproveMail = false
            };
            foreach (XmlNode node in this.OpenFile(logPath, configurationPath).SelectNodes("Settings/Email/add"))
            {
                string str2 = node.Attributes["key"].Value;
                if (str2 != null)
                {
                    if (str2 != "SendCreateEmail")
                    {
                        if (str2 == "SendUpdateEmail")
                        {
                            goto Label_00D1;
                        }
                        if (str2 == "SendCommentEmail")
                        {
                            goto Label_00F3;
                        }
                        if (str2 == "SendApproveEmail")
                        {
                            goto Label_0115;
                        }
                    }
                    else
                    {
                        config.IsSendCreateMail = bool.Parse(node.Attributes["value"].Value);
                    }
                }
                continue;
            Label_00D1:
                config.IsSendUpdateMail = bool.Parse(node.Attributes["value"].Value);
                continue;
            Label_00F3:
                config.IsSendCommentMail = bool.Parse(node.Attributes["value"].Value);
                continue;
            Label_0115:
                config.IsSendApproveMail = bool.Parse(node.Attributes["value"].Value);
            }
            config2 = config;
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return config2;
    }


    public int GetInwardDocumentId(string logPath, string configurationPath)
    {
        int num;
        try
        {
            XmlNode node = this.OpenFile(logPath, configurationPath).SelectSingleNode("Settings/DocumentNumber/add[@key='inward']");
            num = (node != null) ? int.Parse(node.Attributes["value"].Value) : -1;
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return num;
    }

    public int GetPayslipCount(string logPath, string configurationPath)
    {
        int num = 0;
        try
        {
            XmlNode node = this.OpenFile(logPath, configurationPath).SelectSingleNode("Settings/General/FileRecordCount/add[@key='Pay-Slip']");
            num = (node != null) ? int.Parse(node.Attributes["value"].Value) : -1;
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return num;
    }

    public int GetLeaveRecordsCount(string logPath, string configurationPath)
    {
        int num;
        try
        {
            XmlNode node = this.OpenFile(logPath, configurationPath).SelectSingleNode("Settings/General/FileRecordCount/add[@key='Leave-Records']");
            num = (node != null) ? int.Parse(node.Attributes["value"].Value) : -1;
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return num;
    }

    public int GetOutwardDocumentId(string logPath, string configurationPath)
    {
        int num;
        try
        {
            XmlNode node = this.OpenFile(logPath, configurationPath).SelectSingleNode("Settings/DocumentNumber/add[@key='outward']");
            num = (node != null) ? int.Parse(node.Attributes["value"].Value) : -1;
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return num;
    }

    public void GetPdfDocumentContent(ref List<PdfPrintOptions> config, string logPath, string configurationPath)
    {
        try
        {
            config = new List<PdfPrintOptions>();
            XmlNode node = this.OpenFile(logPath, configurationPath).SelectSingleNode("Settings/Document/Content");
            if (node != null)
            {
                foreach (XmlNode node2 in node.SelectNodes("Line"))
                {
                    PdfPrintOptions item = new PdfPrintOptions
                    {
                        Bold = (node2.Attributes["bold"] != null) ? bool.Parse(node2.Attributes["bold"].Value) : false,
                        Display = (node2.Attributes["show"] != null) ? bool.Parse(node2.Attributes["show"].Value) : false,
                        FontSize = (node2.Attributes["fontsize"] != null) ? int.Parse(node2.Attributes["fontsize"].Value) : 0,
                        Id = node2.Attributes["id"].Value,
                        LLX = (node2.Attributes["llx"] != null) ? int.Parse(node2.Attributes["llx"].Value) : 0,
                        LLY = (node2.Attributes["lly"] != null) ? int.Parse(node2.Attributes["lly"].Value) : 0,
                        URX = (node2.Attributes["urx"] != null) ? int.Parse(node2.Attributes["urx"].Value) : 0,
                        URY = (node2.Attributes["ury"] != null) ? int.Parse(node2.Attributes["ury"].Value) : 0,
                        Text = node2.InnerText,
                        LineType = (node2.Attributes["type"] != null) ? (string.IsNullOrEmpty(node2.Attributes["type"].Value) ? "text" : node2.Attributes["type"].Value) : "text",
                        ImagePath = node2.InnerText,
                        Scale = (node2.Attributes["scale"] != null) ? int.Parse(node2.Attributes["scale"].Value) : 0
                    };
                    config.Add(item);
                }
            }
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    public void GetRefNumberConfig(string logPath, string configurationPath, out string initials, out string academicYearStartMonth)
    {
        try
        {
            XmlDocument document = this.OpenFile(logPath, configurationPath);
            XmlNode node = document.SelectSingleNode("Settings/OutwardDocument/Initials");
            XmlNode node2 = document.SelectSingleNode("Settings/OutwardDocument/AcademicYearStart");
            initials = (node != null) ? node.InnerText : string.Empty;
            academicYearStartMonth = (node2 != null) ? node2.InnerText : string.Empty;
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    public XmlDocument OpenFile(string logPath, string configurationPath)
    {
        XmlDocument document2;
        try
        {
            string path = string.Empty;
            if (string.IsNullOrEmpty(configurationPath))
            {
                path = Path.Combine(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BASE_PATH"] + "/"), "Release/config.xml");
            }
            else
            {
                path = configurationPath;
            }
            XmlDocument document = null;
            if (File.Exists(path))
            {
                document = new XmlDocument();
                document.Load(path);
            }
            document2 = document;
        }
        catch (Exception exception)
        {
            throw exception;
        }
        return document2;
    }

    public void SaveDocumentId(string docType, string logPath, string configurationPath)
    {
        try
        {
            string path = string.Empty;
            if (string.IsNullOrEmpty(configurationPath))
            {
                path = Path.Combine(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["BASE_PATH"] + "/"), "Release/config.xml");
            }
            else
            {
                path = configurationPath;
            }
            XmlDocument document = null;
            if (File.Exists(path))
            {
                document = new XmlDocument();
                document.Load(path);
            }
            XmlNode node = document.SelectSingleNode("Settings/DocumentNumber/add[@key='inward']");
            XmlNode node2 = document.SelectSingleNode("Settings/DocumentNumber/add[@key='outward']");
            if ((node != null) && (docType == "inward"))
            {
                int num = int.Parse(node.Attributes["value"].Value);
                node.Attributes["value"].Value = (++num).ToString();
            }
            else if ((node2 != null) && (docType == "outward"))
            {
                int num2 = int.Parse(node2.Attributes["value"].Value);
                node2.Attributes["value"].Value = (++num2).ToString();
            }
            document.Save(path);
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }
}

public class MailManager
{
    private Logger logger;
    public MailManager()
    {
        this.logger = new Logger();
    }

    public bool SendEmail(EmailConfig config, bool hasAttachments, List<string> attachPaths, string logPath)
    {
        try
        {
            MailMessage message = new MailMessage();
            if (config.ToAddress != null)
            {
                foreach (string str in config.ToAddress)
                {
                    if (!string.IsNullOrEmpty(str))
                    {
                        message.To.Add(str);
                    }
                }
            }
            if (config.CC != null)
            {
                foreach (string str2 in config.CC)
                {
                    if (!string.IsNullOrEmpty(str2))
                    {
                        message.CC.Add(str2);
                    }
                }
            }
            if (config.BCC != null)
            {
                foreach (string str3 in config.BCC)
                {
                    if (!string.IsNullOrEmpty(str3))
                    {
                        message.Bcc.Add(str3);
                    }
                }
            }
            message.From = new MailAddress(config.FromAddress, config.MailDisplayName);
            message.Subject = config.Subject;
            message.Body = HttpUtility.UrlDecode(config.Message, Encoding.Default);
            message.IsBodyHtml = true;
            if (hasAttachments)
            {
                foreach (string str4 in attachPaths)
                {
                    message.Attachments.Add(new Attachment(str4));
                }
            }
            new SmtpClient
            {
                UseDefaultCredentials = config.UseDefaultCredentials,
                Host = config.Host,
                Port = config.PortNumber,
                Credentials = new NetworkCredential(config.UserName, config.Password),
                EnableSsl = config.EnableSSL
            }.Send(message);
            return true;
        }
        catch (Exception exception)
        {
            logger.Error("MailManager", "SendEmail", exception.Message, exception, logPath);
            return false;
        }
    }


}

public class EmailConfig
{


    // Properties
    public List<string> BCC { get; set; }
    public List<string> CC { get; set; }
    public bool EnableSSL { get; set; }
    public string FromAddress { get; set; }
    public string Host { get; set; }
    public bool IsHtml { get; set; }
    public bool IsSendApproveMail { get; set; }
    public bool IsSendCommentMail { get; set; }
    public bool IsSendCreateMail { get; set; }
    public bool IsSendUpdateMail { get; set; }
    public string MailDisplayName { get; set; }
    public string Message { get; set; }
    public string Password { get; set; }
    public int PortNumber { get; set; }
    public string Subject { get; set; }
    public List<string> ToAddress { get; set; }
    public bool UseDefaultCredentials { get; set; }
    public string UserName { get; set; }
}

