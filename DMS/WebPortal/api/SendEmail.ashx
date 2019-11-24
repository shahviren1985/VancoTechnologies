<%@ WebHandler Language="C#" Class="SendEmail" %>

using AA.DAO;
using AA.LogManager;
using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Web.SessionState;

public class SendEmail : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        JavaScriptSerializer jss = new JavaScriptSerializer();
        try
        {
            string logPath = context.Session["LogFilePath"].ToString();
            string cnxnString = context.Session["ConnectionString"].ToString();
            string userName = context.Session["UserName"].ToString();

            string college = context.Session["College"].ToString();

            string configFilePath = context.Server.MapPath(context.Session["ReleaseFilePath"].ToString());

            string data = HttpUtility.UrlDecode(new System.IO.StreamReader(context.Request.InputStream).ReadToEnd());
            string documentId = string.Empty;
            string to = string.Empty;
            bool attachDocument = false;
            string subject = string.Empty;
            string body = string.Empty;
            List<string> toEmailList = new List<string>();
            List<string> attachPaths = new List<string>();
            EmailSentDAO dao = new EmailSentDAO();
            DocumentDetailsDAO docDao = new DocumentDetailsDAO();

            XmlConfiguration config = new XmlConfiguration();
            EmailConfig emailConfig = config.GetEmailConfig(logPath, configFilePath);

            foreach (string item in context.Request.Form)
            {
                switch (item)
                {
                    case "DocumentId":
                        documentId = context.Request.Params[item];
                        break;
                    case "To":
                        to = context.Request.Params[item];
                        break;
                    case "Message":
                        body = context.Request.Params[item];
                        break;
                    case "Subject":
                        subject = context.Request.Params[item];
                        break;
                    case "AttachDocument":
                        attachDocument = bool.Parse(context.Request.Params[item]);
                        break;
                }
            }

            string attachPath = string.Empty;

            if (!string.IsNullOrEmpty(to))
            {
                string[] toList = to.Split(',');
                foreach (string toItem in toList)
                {
                    toEmailList.Add(toItem);
                }
            }

            DocumentDetails doc = docDao.GetDocumentDetails(int.Parse(documentId), cnxnString, logPath);

            attachPaths.Add(context.Server.MapPath("~/Directories/" + college + "/outward/" + doc.UniqueName + ".pdf"));

            if (attachDocument && !string.IsNullOrEmpty(doc.ScanPath))
            {
                attachPaths.Add(context.Server.MapPath("~/docs/" + college + "/" + doc.Author + "/" + doc.ScanPath));
            }

            emailConfig.Subject = subject;
            emailConfig.Message = body.Replace("\n", "<br/>");
            emailConfig.ToAddress = toEmailList;

            bool isMailSent = new MailManager().SendEmail(emailConfig, attachDocument, attachPaths, logPath);
            
            if (isMailSent)
            {
                dao.AddEmailSentDetails(HttpUtility.UrlEncode(emailConfig.FromAddress), HttpUtility.UrlEncode(to), HttpUtility.UrlEncode(subject), HttpUtility.UrlEncode(body), attachPath, cnxnString, logPath);
                context.Response.Write(jss.Serialize("true"));
            }
            else
            {
                context.Response.Write(jss.Serialize("false"));
            }
        }
        catch (Exception ex)
        {
            context.Response.Write(jss.Serialize("false"));
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}