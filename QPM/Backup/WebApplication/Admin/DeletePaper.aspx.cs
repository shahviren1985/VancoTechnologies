using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using ITM.DAO;
using ITM.DAOBase;
using ITM.LogManager;
using ITM.Util;


namespace WebApplication.Admin
{
    public partial class DeletePaper : System.Web.UI.Page
    {
        string base_url = string.Empty;
        string connString = string.Empty;
        string logPath = string.Empty;
        string mainConnString = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            mainConnString = ConfigurationManager.ConnectionStrings["DMSConnString"].ToString();
            LoadConnString();
            logPath = ConfigurationManager.AppSettings["LogPath"].ToString();
            base_url = ConfigurationManager.AppSettings["Base_URL"].ToString();

            if (Session["UsernameAdmin"] == null)
            {

                Response.Redirect(base_url + "Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                PopulateCourse();
                BindGridView();
            }

        }

        protected void ddlStream_IndexChange(object sender, EventArgs e)
        {
            try
            {
                LoadConnString();
                PopulateCourse();
            }
            catch (Exception)
            {
            }
        }

        public void LoadConnString()
        {
            try
            {
                string value = ddlStream.SelectedItem.Value;
                switch (value)
                {
                    case "1":
                        connString = ConfigurationManager.ConnectionStrings["ba"].ToString();
                        break;
                    case "2":
                        connString = ConfigurationManager.ConnectionStrings["bcom"].ToString();
                        break;
                    case "3":
                        connString = ConfigurationManager.ConnectionStrings["bms"].ToString();
                        break;
                    case "4":
                        connString = ConfigurationManager.ConnectionStrings["bafi"].ToString();
                        break;
                    default:
                      //  connString = ConfigurationManager.ConnectionStrings["ExamConnString"].ToString();
                        break;

                }


            }
            catch (Exception)
            {

            }
        }


        //Get the list of the course
        public void PopulateCourse()
        {
            try
            {
                CourseDAO cDao = new CourseDAO();
                List<Course> courseList = cDao.GetCourseNameForDropDown(connString, logPath);
                ddlCourse.DataSource = courseList;
                ddlCourse.DataTextField = "CourseName";
                ddlCourse.DataValueField = "CourseId";
                ddlCourse.DataBind();
                ddlCourse.Items.Add(new ListItem("--Select Course--", "0", true));
                ddlCourse.Items.FindByValue("0").Selected = true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void PopulateSubCourse()
        {
            try
            {
                CourseDAO cDao = new CourseDAO();
                List<Course> courseList = cDao.GetSubCourseNameForDropDown(ddlCourse.SelectedItem.Value, connString, logPath);
                ddlSubCourse.DataSource = courseList;
                ddlSubCourse.DataTextField = "SubCourseName";
                ddlSubCourse.DataValueField = "SubCourseId";
                ddlSubCourse.DataBind();
                ddlSubCourse.Items.Add(new ListItem("--Select SubCourse--", "0", true));
                ddlSubCourse.Items.FindByValue("0").Selected = true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void PopulateSubject()
        {
            try
            {
                CourseDAO cDao = new CourseDAO();
                List<Course> courseList = cDao.GetSubjectListByCourseId(Convert.ToInt32(ddlCourse.SelectedItem.Value), Convert.ToInt32(ddlSubCourse.SelectedItem.Value), connString, logPath);
                ddlSubject.DataSource = courseList;
                ddlSubject.DataTextField = "SubjectName";
                ddlSubject.DataValueField = "SubjectId";
                ddlSubject.DataBind();
                ddlSubject.Items.Add(new ListItem("--Select Subject--", "0", true));
                ddlSubject.Items.FindByValue("0").Selected = true;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void PopulatePapers()
        {
            try
            {
                CourseDAO cDao = new CourseDAO();
                List<Course> courseList = cDao.GetPaperListByCourseId(Convert.ToInt32(ddlCourse.SelectedItem.Value), Convert.ToInt32(ddlSubCourse.SelectedItem.Value), Convert.ToInt32(ddlSubject.SelectedItem.Value), connString, logPath);
                ddlPaper.DataSource = courseList;
                ddlPaper.DataTextField = "PaperName";
                ddlPaper.DataValueField = "PaperId";
                ddlPaper.DataBind();
                ddlPaper.Items.Add(new ListItem("--Select Paper--", "0", true));
                ddlPaper.Items.FindByValue("0").Selected = true;
            }
            catch (Exception)
            {
              //  throw;
            }
        }

        protected void ddlCourse_SelectedIndexChange(object sender, EventArgs e)
        {
            try
            {
                PopulateSubCourse();
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ddlSubCourse_SelectedIndexChange(object sender, EventArgs e)
        {
            try
            {
                PopulateSubject();
            }
            catch (Exception)
            {
                throw;
            }
        }
        protected void ddlSubject_SelectedIndexChange(object sender, EventArgs e)
        {
            try
            {
                PopulatePapers();
            }
            catch (Exception)
            {

                throw;
            }
        }




        protected List<DocumentDetails> BindGridView()
        {
            try
            {
                DocumentDetailsDAO ddd = new DocumentDetailsDAO();
                List<DocumentDetails> ddList = ddd.SelectPaperListByAdmin(mainConnString, logPath);
                gvPaper.DataSource = ddList;
                gvPaper.DataBind();
                return ddList;
            }
            catch (Exception)
            {

                throw;
            }
        }


        protected void gvCareerUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                DocumentDetailsDAO commDb = new DocumentDetailsDAO();

                if (e.CommandName == "reqDel")
                {
                    string id = e.CommandArgument.ToString();

                    if (ViewState["userlist"] == null)
                    {
                        List<DocumentDetails> students = commDb.SelectPaperListByAdmin(mainConnString, logPath);
                        // List<DocumentDetails> students = ViewState["UserGrid"] as List<DocumentDetails>;

                        if (students != null)
                        {
                            DocumentDetails student = students.Find(s => s.Id == Convert.ToInt32(id));

                            if (student != null)
                            {
                                // PopulateStudent(student);
                                student.IsDeleteRequested = "2";
                                commDb.UpdateRequestDocumentDetails(student, mainConnString, logPath);
                                //    ViewState["UserGrid"] = student;
                                // divEdit.Style["display"] = "block";
                            }
                        }
                    }
                      gvStatus.InnerText = "Deleted record successfully.";
                      gvStatus.Style["display"] = "block";
                     gvStatus.Style["color"] = "Green";
                     BindGridView();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        protected void gvCareerUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblHf = e.Row.FindControl("gvlblDel") as Label;
                LinkButton lbf = e.Row.FindControl("btnStatus") as LinkButton;
                //  LinkButton lbs = e.Row.FindControl("btnStatusSec") as LinkButton;

                if (lbf != null)
                {
                    if (lbf.CommandArgument == "")
                    {
                        lbf.Visible = false;
                        lbf.ToolTip = "File Not uploaded";
                    }
                }


                if (lblHf != null)
                {
                    CheckBox lbl = e.Row.FindControl("gvlblCheck") as CheckBox;

                    if (lblHf.Text == "0")
                    {
                        lbl.Checked = false;
                    }
                    else
                    {
                        lbl.Checked = true;
                    }
                }
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.Write(filePath);
            Response.End();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DocumentDetailsDAO ddd = new DocumentDetailsDAO();
                foreach (GridViewRow gvrow in gvPaper.Rows)
                {
                    CheckBox chkUsed = gvrow.FindControl("gvlblCheck") as CheckBox;
                    if (chkUsed != null)
                    {
                        if (chkUsed.Checked)
                        {
                            DocumentDetails dd = new DocumentDetails();
                            object a = gvPaper.DataKeys[gvrow.RowIndex].Value;
                            ddd.UpdateUsedDocumentDetails(dd, mainConnString, logPath);
                        }
                    }


                }
            }
            catch (Exception)
            {
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                DocumentDetailsDAO ddd = new DocumentDetailsDAO();
                List<DocumentDetails> ddList = ddd.SelectPaperList(ddlCourse.SelectedItem.Text, ddlSubCourse.SelectedItem.Text, ddlSubject.SelectedItem.Text, ddlPaper.SelectedItem.Text, mainConnString, logPath);
                gvPaper.DataSource = ddList;
                gvPaper.DataBind();
                //    return ddList;

            }
            catch (Exception)
            {
            }
        }

        protected void Clear()
        {
            ddlStream.SelectedItem.Value = "0";
            ddlCourse.SelectedItem.Value = "0";
        }

    }
}