using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using System.Web.Script.Serialization;

public partial class StudentPerformanceReport : PageBase
{
    private JavaScriptSerializer jss = new JavaScriptSerializer();

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

        if (Request.QueryString.Count > 0)
        {
            int courseId = Convert.ToInt32(Request.QueryString["courseid"]);

            // getting Chapterwise performance chart data
            ChapterwisePerformanceChart chapterWisePermChart = new PrepareChapterPerformnaceChart().GetChapterwiseChartData(int.Parse(Session["UserId"].ToString()), cnxnString, logPath);
            hfChapterwisePer.Value = jss.Serialize(chapterWisePermChart);

            // getting user spent time
            UserTimeEstimateChart timeEstimateChart = new PrepareTimeEstimateChart().GetUserTimeEstimateChartData(int.Parse(Session["UserId"].ToString()), courseId, cnxnString, logPath);
            hfTimeEstimated.Value = jss.Serialize(timeEstimateChart);

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "startup", "<script type='text/javascript'>PopulateChapterWisePerformance();</script>", false);
        }
        else
        {
            Response.Redirect(Util.BASE_URL + "Login.aspx", false);
        }
    }
}