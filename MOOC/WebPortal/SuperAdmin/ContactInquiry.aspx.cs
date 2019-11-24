using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using ITM.Courses.DAOBase;
using ITM.Courses.ConfigurationsManager;
using ITM.Courses.LogManager;
using ITM.Courses.Utilities;
using System.Configuration;
using System.Web.UI;
using System.Web.Script.Serialization;

public partial class SuperAdmin_ContactInquiry : PageBase
{
    Logger logger = new Logger();
    // JavaScriptSerializer jss = new JavaScriptSerializer();
    string logPath;
    string cnxnString;
    string adminAppCnxnString;
    const int FIRST_ELEMENT = 0;

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (Session["ConnectionString"] == null)
        {
            Response.Redirect("../Login.aspx", false);
            return;
        }
        else
        {
            adminAppCnxnString = ConfigurationManager.ConnectionStrings["AdminApp_cnxnString"].ToString();
            cnxnString = Session["ConnectionString"].ToString();
            logPath = Server.MapPath(Session["LogFilePath"].ToString());
            //configPath = Server.MapPath(Session["ReleaseFilePath"].ToString());
        }

        if (!IsPostBack)
        {
            bindGridView();
        }
    }

    protected void gvStudentsContactDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Close")
            {
                string[] arguments = e.CommandArgument.ToString().Split(new char[] { '-' });
                int queryId = Convert.ToInt32(arguments[0]);
                string userName = (arguments[1]);

                UserLogins userLogin = new UserLoginsDAO().GetUserByUserName(userName, logPath);

                new StudentQueriesDAO().UpdateQueryStatus(queryId, 2, userLogin.CnxnString, logPath);
                bindGridView();
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    public void bindGridView()
    {
        try
        {
            StudentQueriesDAO studentQueyDetails = new StudentQueriesDAO();
            List<StudentQueries> studentQuery = studentQueyDetails.GetAllStudent(cnxnString, logPath, adminAppCnxnString);

            if (studentQuery != null)
            {
                studentQuery = studentQuery.OrderByDescending(ss => ss.DatePosted).ToList();
            }

            gvStudentsContactDetails.DataSource = studentQuery;//studentQueyDetails.GetAllStudent(cnxnString, logPath, adminAppCnxnString);
            gvStudentsContactDetails.DataBind();
            ViewState["QueryList"] = studentQuery;

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvStudentsContactDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label queryStatus = e.Row.FindControl("gvlblStatus") as Label;
                if (queryStatus != null)
                {
                    if (queryStatus.Text.Trim() == "1")
                    {
                        queryStatus.Text = "New";
                    }
                }

                Label query = e.Row.FindControl("gvlblQuery") as Label;
                string wholeQuery = string.Empty;

                if (query != null)
                {
                    wholeQuery = query.Text;
                    string qry = string.Empty;
                    if (query.Text.LastIndexOf("||") >= 0)
                    {
                        qry = query.Text.Substring(0, query.Text.LastIndexOf("||"));
                    }
                    else
                    {
                        qry = query.Text;
                    }

                    if (qry.Length > 100)
                    {
                        query.ToolTip = qry;
                        qry = qry.Substring(0, 100) + "...";
                    }

                    Label id = e.Row.FindControl("gvlblId") as Label;
                    Label userName = e.Row.FindControl("gvlblUserName") as Label;

                    query.Text = qry + " <a href='" + Util.BASE_URL + "SuperAdmin/ViewandReplayQuery.aspx?id=" + id.Text + "&username=" + userName.Text + "'> view & replay</a>";
                }

                if (!string.IsNullOrEmpty(wholeQuery) && wholeQuery.LastIndexOf("||") > 0)
                {
                    string emailContener = wholeQuery.Substring(wholeQuery.LastIndexOf("||"));
                    string[] emails = emailContener.Split(new char[] { ':' });

                    Label lblEmail = e.Row.FindControl("gvlblEamil") as Label;
                    if (lblEmail != null && emails.Length > 1)
                    {
                        lblEmail.Text = emails[1];
                    }
                }

                Label date = e.Row.FindControl("gvlblDatePosted") as Label;
                if (date != null)
                {
                    try
                    {
                        DateTime dt = Convert.ToDateTime(date.Text);
                        date.Text = dt.ToString("dd-MM-yyyy hh:mm tt");
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnCloseAll_Click(object s, EventArgs e)
    {
        try
        {
            if (ViewState["QueryList"] != null)
            {
                List<StudentQueries> studentQuery = (List<StudentQueries>)ViewState["QueryList"];

                if (studentQuery != null)
                {
                    foreach (StudentQueries query in studentQuery)
                    {
                        UserLogins userLogin = new UserLoginsDAO().GetUserByUserName(query.UserName, logPath);

                        new StudentQueriesDAO().UpdateQueryStatus(query.Id, 2, userLogin.CnxnString, logPath);
                    }

                    bindGridView();
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void gvStudentsContactDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            if (ViewState["QueryList"] != null)
            {
                List<StudentQueries> studentQuery = (List<StudentQueries>)ViewState["QueryList"];
                gvStudentsContactDetails.PageIndex = e.NewPageIndex;

                gvStudentsContactDetails.DataSource = studentQuery;
                gvStudentsContactDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}