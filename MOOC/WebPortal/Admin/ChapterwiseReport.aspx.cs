using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using System.Web.Script.Serialization;


public partial class Admin_ChapterwiseReport : PageBase
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

        if (!IsPostBack)
        {
            ChapterDetailsDAO chapterDetails = new ChapterDetailsDAO();
            UserDetailsDAO objUserDetailsDAO = new UserDetailsDAO();
            List<UserDetails> userList = objUserDetailsDAO.GetUserByUserTypeAndOtherStatus(true, cnxnString, logPath);
            List<ChapterDetails> chapterDetailsList = chapterDetails.GetAllChapterDetails(cnxnString,logPath);
            int i = 0;
            if (userList == null)
            {
                errorSummary.InnerText = "Sorry! no any student completed course yet.";
                errorSummary.Visible = true;
                return;
            }

            foreach(ChapterDetails chapter in chapterDetailsList)
            {
                i++;
                List<ChapterwisePercentialList> chapterwiseChart = new PreparAdminChapterwiseChart().getData(chapter.Id, userList, cnxnString, logPath);
               // hfChapterwisePer.Value = jss.Serialize(chapterwiseChart);
               // HiddenField hf = (HiddenField)FindControl("hfChapter" + i);
                HiddenField hf = (HiddenField)hfContainer.FindControl("hfChapter" + i);
                hf.Value = jss.Serialize(chapterwiseChart);
            }
        }

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "startup", "<script type='text/javascript'>PopulateChapterwiseUserPercentage();</script>", false);
    }
}