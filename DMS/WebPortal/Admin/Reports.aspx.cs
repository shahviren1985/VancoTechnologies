using AA.DAO;
using AA.ExcelManager;
using AA.LogManager;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.Script.Serialization;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Util;

public partial class Admin_Reports : Page, IRequiresSessionState
{
    private string connString = string.Empty;
    private JavaScriptSerializer json = new JavaScriptSerializer();
    private string loggedInUser = string.Empty;
    private Logger logger = new Logger();
    private string logPath = string.Empty;
    private string role = string.Empty;

    protected void btnGenerate_Click(object s, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(this.txtStartDate.Text) && !string.IsNullOrEmpty(this.txtEndDate.Text))
            {
                string str = "Inward";
                if (this.rbOutward.Checked)
                {
                    str = "Outward";
                }
                if (this.rbReceivedDate.Checked)
                {
                    str = "Received";
                }
                string query = "select * from documents where DateCreated >= '" + txtStartDate.Text + "' and DateCreated <= '" + txtEndDate.Text + "'";
                List<DocumentDetails> list = new DocumentDetailsDAO().GetDocumentDetails(query, this.connString, this.logPath);
                if ((list != null) && (list.Count > 0))
                {
                    DateTime time = DateTime.Parse(this.txtStartDate.Text);
                    DateTime time2 = DateTime.Parse(this.txtEndDate.Text);
                    DataTable objects = new DataTable();
                    objects.Columns.Add("Serial Number");
                    objects.Columns.Add("Document Type");
                    objects.Columns.Add("Document Number");
                    objects.Columns.Add("From");
                    objects.Columns.Add("To");
                    /*if ((str.ToLower() == "inward") || (str == "Received"))
                    {
                        objects.Columns.Add("To");
                    }*/
                    objects.Columns.Add("Subject");
                    objects.Columns.Add("Document Date");
                    objects.Columns.Add("Received Date");
                    objects.Columns.Add("User Tags");
                    objects.Columns.Add("Store Location");
                    int num = 1;
                    foreach (DocumentDetails details in list)
                    {

                        MessageHeader header = this.json.Deserialize<MessageHeader>(details.MessageHeader);
                        if (header != null)
                        {
                            if (header.DocumentType.ToLower() != str.ToLower())
                            {
                                if (header.DocumentType.ToLower() != "Received")
                                    continue;
                            }

                            DateTime now;
                            DateTime time4;
                            DataRow row = objects.NewRow();
                            row["Serial Number"] = details.SerialNumber;
                            row["Document Type"] = header.DocumentType;
                            row["From"] = this.Context.Server.UrlDecode(header.From);
                            row["To"] = (str.ToLower() == "outward") ? this.Context.Server.UrlDecode(details.Address) : header.To;
                            /*if (header.DocumentType.ToLower() == "inward")
                            {
                                
                            }*/
                            row["Subject"] = this.Context.Server.UrlDecode(header.Subject);
                            row["User Tags"] = this.Context.Server.UrlDecode(details.TaggedUsers);
                            if (details.StoreRoomLocation == "---Room---,---Cupboard---,---Shelf---,---File---")
                            {
                                row["Store Location"] = "-";
                            }
                            else
                            {
                                row["Store Location"] = details.StoreRoomLocation;
                            }
                            if (header.DocumentType.ToLower() == "inward")
                            {
                                now = Utilities.ParseDate(header.InwardDate);
                                row["Document Date"] = header.InwardDate;
                                row["Document Number"] = this.Context.Server.UrlDecode(header.InwardNumber);
                            }
                            else if (header.DocumentType.ToLower() == "outward")
                            {
                                now = Utilities.ParseDate(header.OutwardDate);
                                row["Document Date"] = header.OutwardDate;
                                row["Document Number"] = header.OutwardNumber;
                            }
                            else
                            {
                                now = DateTime.Now;
                            }
                            if (header.IsReceived == "true")
                            {
                                time4 = Utilities.ParseDate(header.ReceivedDate);
                                row["Received Date"] = header.ReceivedDate;
                            }
                            else
                            {
                                time4 = DateTime.Now;
                                row["Received Date"] = "-";
                            }
                            if (header.DocumentType.ToLower() == str.ToLower())
                            {
                                if ((now >= time) && (now <= time2))
                                {
                                    objects.Rows.Add(row);
                                    num++;
                                }
                            }
                            else if (((str == "Received") && (header.IsReceived == "true")) && ((time4 >= time) && (time4 <= time2)))
                            {
                                objects.Rows.Add(row);
                                num++;
                            }
                        }
                    }
                    if (objects.Rows.Count > 0)
                    {
                        string str3 = str.ToLower() + "-from-" + this.txtStartDate.Text + "-to-" + this.txtEndDate.Text + "-" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx";
                        string path = base.Server.MapPath(this.Session["ExcelReportURL"].ToString());
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        path = Path.Combine(path, str3);
                        new GenerateExcelReports().Create(path, objects, str + "_Sheet", this.logPath);
                        this.outputLink.InnerHtml = "<a href=\"" + ConfigurationManager.AppSettings["BASE_URL"] + this.Session["ExcelReportURL"].ToString().Replace("~", "") + str3 + "\">Click here</a> to download excel report.";
                    }
                    else
                    {
                        this.outputLink.InnerText = "Documents not found!";
                    }
                }
            }
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    protected void btnGenerateInward_Click(object s, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(this.txtStartDate.Text) && !string.IsNullOrEmpty(this.txtEndDate.Text))
            {
                string str = "Inward";
                if (this.rbOutward.Checked)
                {
                    str = "Outward";
                }
                if (this.rbReceivedDate.Checked)
                {
                    str = "Received";
                }
                string query = "select * from documents where DateCreated >= DATE_SUB(curdate(), INTERVAL 3 YEAR)";
                List<DocumentDetails> list = new DocumentDetailsDAO().GetDocumentDetails(query, this.connString, this.logPath);
                if ((list != null) && (list.Count > 0))
                {
                    DateTime time = DateTime.Parse(this.txtStartDate.Text);
                    DateTime time2 = DateTime.Parse(this.txtEndDate.Text);
                    DataTable objects = new DataTable();
                    objects.Columns.Add("Serial Number");
                    objects.Columns.Add("Document Type");
                    objects.Columns.Add("Document Number");
                    objects.Columns.Add("From");
                    objects.Columns.Add("To");
                    /*if ((str.ToLower() == "inward") || (str == "Received"))
                    {
                        objects.Columns.Add("To");
                    }*/
                    objects.Columns.Add("Subject");
                    objects.Columns.Add("Document Date");
                    objects.Columns.Add("Received Date");
                    objects.Columns.Add("User Tags");
                    objects.Columns.Add("Store Location");
                    int num = 1;
                    foreach (DocumentDetails details in list)
                    {

                        MessageHeader header = this.json.Deserialize<MessageHeader>(details.MessageHeader);
                        if (header != null)
                        {
                            if (header.DocumentType.ToLower() != str.ToLower())
                            {
                                if (header.DocumentType.ToLower() != "Received")
                                    continue;
                            }

                            DateTime now;
                            DateTime time4;
                            DataRow row = objects.NewRow();
                            row["Serial Number"] = details.SerialNumber;
                            row["Document Type"] = header.DocumentType;
                            row["From"] = this.Context.Server.UrlDecode(header.From);
                            row["To"] = (str.ToLower() == "outward") ? this.Context.Server.UrlDecode(details.Address) : header.To;
                            /*if (header.DocumentType.ToLower() == "inward")
                            {
                                
                            }*/
                            row["Subject"] = this.Context.Server.UrlDecode(header.Subject);
                            row["User Tags"] = this.Context.Server.UrlDecode(details.TaggedUsers);
                            if (details.StoreRoomLocation == "---Room---,---Cupboard---,---Shelf---,---File---")
                            {
                                row["Store Location"] = "-";
                            }
                            else
                            {
                                row["Store Location"] = details.StoreRoomLocation;
                            }
                            if (header.DocumentType.ToLower() == "inward")
                            {
                                now = Utilities.ParseDate(header.InwardDate);

                                if (now < DateTime.Parse(txtStartDate.Text) || now > DateTime.Parse(txtEndDate.Text))
                                {
                                    continue;
                                }

                                row["Document Date"] = header.InwardDate;
                                row["Document Number"] = this.Context.Server.UrlDecode(header.InwardNumber);
                            }
                            else if (header.DocumentType.ToLower() == "outward")
                            {
                                now = Utilities.ParseDate(header.OutwardDate);
                                row["Document Date"] = header.OutwardDate;
                                row["Document Number"] = header.OutwardNumber;
                            }
                            else
                            {
                                now = DateTime.Now;
                            }
                            if (header.IsReceived == "true")
                            {
                                time4 = Utilities.ParseDate(header.ReceivedDate);
                                row["Received Date"] = header.ReceivedDate;
                            }
                            else
                            {
                                time4 = DateTime.Now;
                                row["Received Date"] = "-";
                            }
                            if (header.DocumentType.ToLower() == str.ToLower())
                            {
                                if ((now >= time) && (now <= time2))
                                {
                                    objects.Rows.Add(row);
                                    num++;
                                }
                            }
                            else if (((str == "Received") && (header.IsReceived == "true")) && ((time4 >= time) && (time4 <= time2)))
                            {
                                objects.Rows.Add(row);
                                num++;
                            }
                        }
                    }
                    if (objects.Rows.Count > 0)
                    {
                        string str3 = str.ToLower() + "-from-" + this.txtStartDate.Text + "-to-" + this.txtEndDate.Text + "-" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".xlsx";
                        string path = base.Server.MapPath(this.Session["ExcelReportURL"].ToString());
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        path = Path.Combine(path, str3);
                        new GenerateExcelReports().Create(path, objects, str + "_Sheet", this.logPath);
                        this.outputLink.InnerHtml = "<a href=\"" + ConfigurationManager.AppSettings["BASE_URL"] + this.Session["ExcelReportURL"].ToString().Replace("~", "") + str3 + "\">Click here</a> to download excel report.";
                    }
                    else
                    {
                        this.outputLink.InnerText = "Documents not found!";
                    }
                }
            }
        }
        catch (Exception exception)
        {
            throw exception;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["UserName"] == null)
        {
            base.Response.Redirect("~/Login.apsx", false);
        }
        else
        {
            this.role = this.Session["RoleType"].ToString();
            this.loggedInUser = this.Session["UserName"].ToString();
            this.connString = this.Session["ConnectionString"].ToString();
            this.logPath = this.Session["LogFilePath"].ToString();
            new DocumentDetailsDAO().GetDocumentsHistory(0x48e, this.connString, this.logPath);
        }
    }
}
