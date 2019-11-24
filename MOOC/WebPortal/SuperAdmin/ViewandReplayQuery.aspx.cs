using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using System.Web.Script.Serialization;
using ITM.Courses.LogManager;

public partial class SuperAdmin_ViewandReplayQuery : PageBase
{
    Logger logger = new Logger();
    private JavaScriptSerializer jss = new JavaScriptSerializer();
    string logPath;
    string cnxnString;

    int queryId;
    string username;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Count < 2 || string.IsNullOrEmpty(Request.QueryString["id"]) || string.IsNullOrEmpty(Request.QueryString["username"]))
            {
                Response.Redirect(Util.BASE_URL + "SuperAdmin/ContactInquiry.aspx", false);
                return;
            }

            queryId = Convert.ToInt32(Request.QueryString["id"]);
            username = Request.QueryString["username"];

            if (Session["ConnectionString"] == null)
            {
                Response.Redirect(Util.BASE_URL + "Login.aspx", false);
                return;
            }
            else
            {
                cnxnString = Session["ConnectionString"].ToString();
                logPath = Server.MapPath(Session["LogFilePath"].ToString());
            }

            UserLogins userLogins = new UserLoginsDAO().GetUserByUserName(username, logPath);

            if (userLogins != null)
            {
                cnxnString = userLogins.CnxnString;
                logPath = Server.MapPath(userLogins.LogFilePath);

                StudentQueries studentQuery = new StudentQueriesDAO().GetStudentQueryById(queryId, userLogins.CnxnString, Server.MapPath(userLogins.LogFilePath));

                hfUserQuery.Value = jss.Serialize(studentQuery);
                hfQueryId.Value = queryId.ToString();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "startup", "<script type='text/javascript'>PopulateStudentQuery();</script>", false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnSaveSend_Click(object s, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtToAddress.Text) || string.IsNullOrEmpty(txtSubject.Text) || string.IsNullOrEmpty(hfResponseMessage.Value) || string.IsNullOrEmpty(hfQueryId.Value))
        {
            Success.InnerHtml = "All feilds are mandatory!";
            Success.Style["display"] = "block";
            Success.Style["background-color"] = "pink";
            return;
        }
        try
        {
            new StudentQueriesResponseDAO().AddStudentQueryResponse(int.Parse(hfQueryId.Value), int.Parse(Session["UserId"].ToString()), "", cnxnString, logPath);

            Success.InnerHtml = "Responsed sent successfully!";
            Success.Style["display"] = "block";

            UserLogins userLogin = new UserLoginsDAO().GetUserByUserName(username, logPath);
            new StudentQueriesDAO().UpdateQueryStatus(queryId, 2, userLogin.CnxnString, logPath);

            try
            {
                //new Util().SendMail(txtToAddress.Text.Trim(), "", txtSubject.Text, txtMessage.Text);
                new Util().SendMail(txtToAddress.Text.Trim(), "", txtSubject.Text, hfResponseMessage.Value);
            }
            catch (Exception ex)
            {
                logger.Error("SuperAdmin_ViewandReplayQuery", "btnSaveSend_Click", "error occured while sending response mail", ex, logPath);
            }
            
            Response.Redirect(Util.BASE_URL + "SuperAdmin/ContactInquiry.aspx", false);
        }
        catch (Exception)
        {
            throw;
        }        
    }
}