using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ITM.Courses.DAO;
using System.Web.Script.Serialization;
using ITM.Courses.LogManager;
using ITM.Courses.DAOBase;
using System.Data;

public partial class Dashboard : PageBase//System.Web.UI.Page
{
    private JavaScriptSerializer jss = new JavaScriptSerializer();
    Logger logger = new Logger();

    string cnxnString;
    string logPath;

    public string value = "10";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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

            Util util = new Util(Request.Url);

            bool isNewUser = Convert.ToBoolean(Session["IsNewUser"]);

            if (isNewUser)
            {
                Response.Redirect(Util.BASE_URL + "ChangePassword.aspx?ref=ps", false);
                return;
            }

            bool isStatusTrackerRequired = Convert.ToBoolean(Session["IsStatusTrackerRequired"]);

            if (isStatusTrackerRequired)
            {
                //PopulatingStepStatus();
                //divExamState.Style["display"] = "block";
            }
            else
            {
                divExamState.Style["display"] = "none";
            }

            string userName = Session["UserName"].ToString();
            if (userName.ToLower() == "principal" || userName.ToLower() == "sejal" || userName.ToLower() == "sunita" || userName.ToLower() == "shama" || userName.ToLower() == "pranaya")
            {
                Video.Visible = false;
            }
            
        }
        catch (Exception ex)
        {
            logger.Error("Dashboard.aspx", "PageLoad", "Error occurred while loading dashboard page", ex, logPath);
            throw;
        }
    }

    public void PopulatingStepStatus()
    {
        try
        {
            ChapterDetailsDAO chapterDAO = new ChapterDetailsDAO();
            // for step one
            int totalCounter = 0, userCounter = 0;

            List<ChapterDetails> chapters = chapterDAO.GetChaptersBetweenTwoIds(1, 7, cnxnString, logPath);
            /* killing sleeping connections */
            //Database.KillConnections(cnxnString, logPath);

            foreach (ChapterDetails chp in chapters)
            {
                List<ChapterSection> sections = new ChapterSectionDAO().GetChapterSectionsByChapterId(chp.Id, cnxnString, logPath);
                /* killing sleeping connections */
                //Database.KillConnections(cnxnString, logPath);

                if (sections != null)
                {
                    foreach (ChapterSection sec in sections)
                    {
                        totalCounter++;
                        UserChapterStatus userChapterStatus = new UserChapterStatusDAO().GetUserChapterSectionByUserChapterSectionId(int.Parse(Session["UserId"].ToString()), chp.Id, sec.Id, cnxnString, logPath);
                        /* killing sleeping connections */
                        //Database.KillConnections(cnxnString, logPath);

                        if (userChapterStatus != null)
                        {
                            userCounter++;
                        }
                    }
                }
            }

            if (userCounter == 0)
            {
                //Not Started
                //step1.Style["background-color"] = "lightgreen";
            }
            else if (userCounter > 0 && userCounter < totalCounter)
            {
                //Running
                step1.Style["background-color"] = "dodgerblue";
            }
            else if (userCounter == totalCounter)
            {
                //Completed
                step1.Style["background-color"] = "lightgreen";
            }

            // step tow
            totalCounter = 0;
            userCounter = 0;
            chapters = null;

            chapters = chapterDAO.GetChaptersBetweenTwoIds(8, 10, cnxnString, logPath);
            foreach (ChapterDetails chp in chapters)
            {
                List<ChapterSection> sections = new ChapterSectionDAO().GetChapterSectionsByChapterId(chp.Id, cnxnString, logPath);
                foreach (ChapterSection sec in sections)
                {
                    totalCounter++;
                    UserChapterStatus userChapterStatus = new UserChapterStatusDAO().GetUserChapterSectionByUserChapterSectionId(int.Parse(Session["UserId"].ToString()), chp.Id, sec.Id, cnxnString, logPath);
                    /* killing sleeping connections */
                    //Database.KillConnections(cnxnString, logPath);

                    if (userChapterStatus != null)
                    {
                        userCounter++;
                    }
                }
            }

            if (userCounter == 0)
            {
                //Not Started
                //step1.Style["background-color"] = "lightgreen";
            }
            else if (userCounter > 0 && userCounter < totalCounter)
            {
                //Running
                step2.Style["background-color"] = "dodgerblue";
            }
            else if (userCounter == totalCounter)
            {
                //Completed
                step2.Style["background-color"] = "lightgreen";
            }

            // step three
            totalCounter = 0;
            userCounter = 0;
            chapters = null;

            chapters = chapterDAO.GetChaptersBetweenTwoIds(11, 14, cnxnString, logPath);
            foreach (ChapterDetails chp in chapters)
            {
                List<ChapterSection> sections = new ChapterSectionDAO().GetChapterSectionsByChapterId(chp.Id, cnxnString, logPath);
                foreach (ChapterSection sec in sections)
                {
                    totalCounter++;
                    UserChapterStatus userChapterStatus = new UserChapterStatusDAO().GetUserChapterSectionByUserChapterSectionId(int.Parse(Session["UserId"].ToString()), chp.Id, sec.Id, cnxnString, logPath);
                    /* killing sleeping connections */
                    //Database.KillConnections(cnxnString, logPath);

                    if (userChapterStatus != null)
                    {
                        userCounter++;
                    }
                }
            }

            if (userCounter == 0)
            {
                //Not Started
                //step1.Style["background-color"] = "lightgreen";
            }
            else if (userCounter > 0 && userCounter < totalCounter)
            {
                //Running
                step3.Style["background-color"] = "dodgerblue";
            }
            else if (userCounter == totalCounter)
            {
                //Completed
                step3.Style["background-color"] = "lightgreen";
            }
        }
        catch (Exception ex)
        {
            logger.Error("Dashboard.aspx", "PopulatingStepStatus", "Error occurred while populating step status", ex, logPath);
            throw ex;
        }
    }
}