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


namespace WebApplication.User
{
    public partial class UploadControl : System.Web.UI.Page
    {

        string connString = string.Empty;
        string mainConnString = string.Empty;
        string logPath = string.Empty;
        string userId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            mainConnString = ConfigurationManager.ConnectionStrings["DMSConnString"].ToString();
            LoadConnString();
            userId = Session["UserName"].ToString();

            logPath = ConfigurationManager.AppSettings["LogPath"].ToString();

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
                    default : 
                     //   connString = ConfigurationManager.ConnectionStrings["ExamConnString"].ToString();
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
                throw;
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


        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string extension = string.Empty;
                if (docFirst.HasFile)
                {
                    extension = System.IO.Path.GetExtension(docFirst.FileName);

                    if (extension != ".doc" && extension != ".docx")
                    {

                        status_edit.InnerText = "Invalid File Type. Only .doc and docx formate allowed.";
                        status_edit.Style["display"] = "block";
                        status_edit.Style["color"] = "red";
                        return ;
                    }                   
                }
                if (docSecond.HasFile)
                {
                    extension = System.IO.Path.GetExtension(docSecond.FileName);

                    if (extension != ".doc" && extension != ".docx")
                    {

                        status_edit.InnerText = "Invalid File Type. Only .doc and docx formate allowed.";
                        status_edit.Style["display"] = "block";
                        status_edit.Style["color"] = "red";
                        return;
                    }
                }


                DocumentDetails dd = new DocumentDetails();
                DocumentDetails ddSec = new DocumentDetails();


                //this is for the first document;
                dd.Date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                dd.Course = ddlCourse.SelectedItem.Text;
                dd.SubCourse = ddlSubCourse.SelectedItem.Text;
                dd.Subject = ddlSubject.SelectedItem.Text;
                dd.Paper = ddlPaper.SelectedItem.Text;
                dd.DocumentFirst = UploadedDocument(docFirst, "First");
                dd.IsDeleteRequested = "0";
                dd.IsUsed = "0";
                dd.UserId = userId;
                DocumentDetailsDAO document = new DocumentDetailsDAO();
                document.InsertDocumentDetails(dd, mainConnString, logPath);


                //this is for seconde document
                ddSec.Date = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                ddSec.Course = ddlCourse.SelectedItem.Text;
                ddSec.SubCourse = ddlSubCourse.SelectedItem.Text;
                ddSec.Subject = ddlSubject.SelectedItem.Text;
                ddSec.Paper = ddlPaper.SelectedItem.Text;
                ddSec.DocumentFirst = UploadedDocument(docSecond, "Second");
                ddSec.IsDeleteRequested = "0";
                ddSec.IsUsed = "0";
                ddSec.UserId = userId;

                if (!string.IsNullOrEmpty(ddSec.DocumentFirst)) 
                {
                    document.InsertDocumentDetails(ddSec, mainConnString, logPath);
                }

                status_edit.InnerHtml = "<li>File Uplaoded successfully.</li>";
                status_edit.Style["color"] = "green";
                status_edit.Style["display"] = "block";
                Clear();
                BindGridView();

            }
            catch (Exception)
            {

            }
        }

        public string UploadedDocument(FileUpload file, string document)
        {
            try
            {
                string image = string.Empty;
                string profileImage = string.Empty;
                string extension = string.Empty;
                //extension = photoUpload.na
                if (file.HasFile)
                {
                    extension = System.IO.Path.GetExtension(file.FileName);

                    if (extension != ".doc" && extension != ".docx")
                    {
                        
                         status_edit.InnerText = "Invalid File Type. Only .doc and docx formate allowed.";
                         status_edit.Style["display"] = "block";
                         status_edit.Style["color"] = "red";
                        return string.Empty;
                    }
                    profileImage = RemoveSpecialCharacters(ddlCourse.SelectedItem.Text) + "_" + RemoveSpecialCharacters(ddlSubCourse.SelectedItem.Text) + "_" + RemoveSpecialCharacters(ddlSubject.SelectedItem.Text) + "_" + RemoveSpecialCharacters(ddlPaper.SelectedItem.Text) + "_" + document + "_" + System.IO.Path.GetFileNameWithoutExtension(file.FileName) + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day.ToString() + "_" + System.IO.Path.GetExtension(file.FileName);
                }
                else
                {
                    return string.Empty;
                }

                string path = Server.MapPath(@"~/UploadedFiles/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string photoPath = path + "\\" + profileImage;


                file.SaveAs(photoPath);
                string photoUrl = "/UploadedFiles/" + profileImage;
                return photoUrl;
            }
            catch (Exception)
            {

                throw;
            }
            return string.Empty;
        }

        protected List<DocumentDetails> BindGridView()
        {
            try
            {
                DocumentDetailsDAO ddd = new DocumentDetailsDAO();
                List<DocumentDetails> ddList = ddd.SelectPaperListByCourseUserId(userId, mainConnString, logPath);
                gvPaper.DataSource = ddList;
                gvPaper.DataBind();
                //   ViewState["userlist"] = ddList;
                return ddList;
            }
            catch (Exception)
            {

                throw;
            }
        }



        public string RemoveSpecialCharacters(string parameter)
        {
            if (string.IsNullOrEmpty(parameter)) return parameter;
            string returnString = parameter;

            returnString = returnString.Replace("'", "");
            returnString = returnString.Replace("?", "");
            returnString = returnString.Replace("\"", "");
            returnString = returnString.Replace("/", "");
            returnString = returnString.Replace(".", "");
            returnString = returnString.Replace("|", "");
            returnString = returnString.Replace("~", "");
            returnString = returnString.Replace("!", "");
            returnString = returnString.Replace("#", "");
            returnString = returnString.Replace("@", "");
            returnString = returnString.Replace("$", "");
            returnString = returnString.Replace("%", "");
            returnString = returnString.Replace("^", "");
            returnString = returnString.Replace("&", "");
            returnString = returnString.Replace("*", "");
            returnString = returnString.Replace("(", "");
            returnString = returnString.Replace(")", "");
            returnString = returnString.Replace("_", "");
            returnString = returnString.Replace("+", "");
            returnString = returnString.Replace("{", "");
            returnString = returnString.Replace("}", "");
            returnString = returnString.Replace("[", "");
            returnString = returnString.Replace("]", "");
            returnString = returnString.Replace(";", "");
            returnString = returnString.Replace(":", "");
            returnString = returnString.Replace(",", "");
            returnString = returnString.Replace("<", "");
            returnString = returnString.Replace(">", "");

            return returnString;
        }


        //protected void gvCareerUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        DocumentDetailsDAO commDb = new DocumentDetailsDAO();

        //        if (e.CommandName == "reqDel")
        //        {
        //            string id = e.CommandArgument.ToString();

        //            if (ViewState["userlist"] == null)
        //            {
        //                List<DocumentDetails> students = commDb.SelectPaperListByCourseUserId("1000", connString, logPath);
        //                // List<DocumentDetails> students = ViewState["UserGrid"] as List<DocumentDetails>;

        //                if (students != null)
        //                {
        //                    DocumentDetails student = students.Find(s => s.Id == Convert.ToInt32(id));

        //                    if (student != null)
        //                    {
        //                        // PopulateStudent(student);
        //                        student.IsDeleteRequested = "1";
        //                        commDb.UpdateRequestDocumentDetails(student, connString, logPath);
        //                        //    ViewState["UserGrid"] = student;
        //                        // divEdit.Style["display"] = "block";
        //                    }
        //                }
        //            }
        //            //  Status.InnerText = "User career application deleted successfully.";
        //            //  Status.Style["display"] = "block";
        //            // Status.Style["color"] = "Green";
        //        }
        //        BindGridView();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}


        protected void gvCareerUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblHf = e.Row.FindControl("gvlblDel") as Label;
                LinkButton lbf = e.Row.FindControl("btnStatus") as LinkButton;
               // LinkButton lbs = e.Row.FindControl("btnStatusSec") as LinkButton;

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

        protected void btnDelete_Click(object sender, EventArgs e)
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
                            int id = Convert.ToInt32(((Label)gvrow.FindControl("gvlblId")).Text);
                            DocumentDetails dd = new DocumentDetails();
                            dd.Id = id;
                            dd.IsDeleteRequested = "1";
                            ddd.UpdateRequestDocumentDetails(dd, mainConnString, logPath);
                        }
                    }


                }
                status_edit.InnerHtml = "Updated the status.";
                status_edit.Style["color"] = "green";
                status_edit.Style["display"] = "block";

                BindGridView();
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