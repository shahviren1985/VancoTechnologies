using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using System.Data;
using System.Configuration;

public partial class TypingStats : System.Web.UI.Page
{
    string cnxnString;
    string logPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ConnectionString"] == null)
        {
            Response.Redirect("~/Login.aspx", false);
            return;
        }
        else
        {
            cnxnString = Session["ConnectionString"].ToString();
            logPath = Server.MapPath(Session["LogFilePath"].ToString());            
        }

        if (!IsPostBack)
        {
            BindGridView();
        }
    }

    public void BindGridView()
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SR");
            dt.Columns.Add("Level");
            dt.Columns.Add("Status");
            dt.Columns.Add("CompletedDate");

            List<StudentTypingStats> lTypingStats = new StudentTypingStatsDAO().GetTypingStatsByUserId(int.Parse(Session["UserId"].ToString()), false, 0, cnxnString, logPath);

            if (lTypingStats != null)
            {
                int typingLevelCount = string.IsNullOrEmpty(ConfigurationManager.AppSettings["TypingLevel"]) ? 23 : int.Parse(ConfigurationManager.AppSettings["TypingLevel"]);

                for (int i = 1; i <= typingLevelCount; i++)
                {
                    DataRow row = dt.NewRow();

                    row["Level"] = "Level " + i;

                    StudentTypingStats sts = lTypingStats.Find(lt=> lt.Level == i);

                    if (sts != null)
                    {
                        row["Status"] = "Completed";
                        row["CompletedDate"] = sts.DateCreated.ToString("dd-MMM-yyyy");
                    }
                    else
                    {                        
                        row["Status"] = "Pending";
                        row["CompletedDate"] = string.Empty;
                    }
                    
                    dt.Rows.Add(row);
                }
            }

            gvUserDetails.DataSource = dt;
            gvUserDetails.DataBind();

        }
        catch (Exception ex)
        {            
            throw ex;
        }
    }
    

    protected void gvUserDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string status = ((Label)e.Row.FindControl("gvlblStatus")).Text;

                if (!string.IsNullOrEmpty(status))
                {
                    if (status == "Pending")
                    {
                        e.Row.Style["background-color"] = "rgb(234, 18, 18)";
                        e.Row.Style["color"] = "white";
                    }
                }
            }
        }
        catch (Exception ex)
        {            
            throw ex;
        }
    }
}