using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using ITM.Courses.ExcelGenerator;
using ITM.Courses.DAO;
using ITM.Courses.LogManager;
using System.Xml;

public partial class Admin_ImportStudents : PageBase
{
    Logger logger = new Logger();
    string logPath;
    string cnxnString;
    string configPath;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["ConnectionString"] == null)
            {
                Response.Redirect("../Login.aspx", false);
                return;
            }
            else
            {
                cnxnString = Session["ConnectionString"].ToString();
                logPath = Server.MapPath(Session["LogFilePath"].ToString());
                configPath = Server.MapPath(Session["ReleaseFilePath"].ToString());
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnUpload_Click(object s, EventArgs e)
    {
        errorSummary.Visible = false;
        try
        {
            if (fuStudents.HasFile)
            {
                string extention = Path.GetExtension(fuStudents.FileName);

                if (extention.ToLower() == ".xlsx" || extention.ToLower() == ".xls")
                {
                    string path = ConfigurationManager.AppSettings["ImportFilePath"];

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    fuStudents.SaveAs(path + fuStudents.FileName);

                    string newFile = "import-students-" + DateTime.Now.Ticks + extention;

                    File.Move(path + fuStudents.FileName, path + newFile);

                    DataTable dt = new GetDataTableFromExcel().GetTableByExcel(path + newFile);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        btnUpload.Visible = false;
                        btnCancle.Visible = false;
                        btnSave.Visible = true;
                        btnCancel2.Visible = true;

                        DataTable dataTable = dt.Clone();

                        foreach (DataRow row in dt.Rows)
                        {
                            string firstName = row["FirstName"].ToString().Trim().Replace("'", "");
                            string lastName = row["LastName"].ToString().Trim().Replace("'", "");

                            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                            {
                                continue;
                            }
                            else
                            {
                                DataRow nRow = dataTable.NewRow();

                                nRow.ItemArray = row.ItemArray;

                                dataTable.Rows.Add(nRow);
                            }
                        }


                        ViewState["table"] = dataTable;

                        gvStudents.DataSource = dataTable;
                        gvStudents.DataBind();
                    }
                    else
                    {
                        btnUpload.Visible = true;
                        btnSave.Visible = true;

                        errorSummary.InnerHtml = "Please valid select excel. which contains following Colums: FirstName, LastName, Email and MobileNo.";
                        errorSummary.Visible = true;
                    }
                }
                else
                {
                    errorSummary.InnerHtml = "Please select excel file with .xlsx or .xls extention";
                    errorSummary.Visible = true;
                }
            }
            else
            {
                errorSummary.InnerHtml = "Please select file";
                errorSummary.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void gvStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvStudents.PageIndex = e.NewPageIndex;
            if (ViewState["table"] == null)
            {
                return;
            }

            DataTable dt = ViewState["table"] as DataTable;
            gvStudents.DataSource = dt;
            gvStudents.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void btnSave_Click(object s, EventArgs e)
    {
        try
        {
            if (ViewState["table"] == null)
            {
                return;
            }

            DataTable dt = ViewState["table"] as DataTable;

            foreach (DataRow row in dt.Rows)
            {
                string userName = string.Empty;
                string password = string.Empty;

                string firstName = row["FirstName"].ToString().Trim().Replace("'", "");
                string lastName = row["LastName"].ToString().Trim().Replace("'", "");
                string email = row["Email"].ToString().Trim().Replace("'", "");
                string mobileNo = row["MobileNo"].ToString().Trim().Replace("'", "");
                string rollNo = row["RollNumber"].ToString().Trim().Replace("'", "");

                string father = row["FatherName"].ToString().Trim().Replace("'", "");
                string mother = row["MotherName"].ToString().Trim().Replace("'", "");
                string course = row["Course"].ToString().Trim().Replace("'", "");
                string subCourse = row["SubCourse"].ToString().Trim().Replace("'", "");

                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                {
                    continue;
                }

                int batchYear = DateTime.Now.Year;

                if (!string.IsNullOrEmpty(row["BatchYear"].ToString()))
                    batchYear = Convert.ToInt32(row["BatchYear"].ToString());

                // creating password
                //if (string.IsNullOrEmpty(rollNo))  this is commented by rajesh for the password generation by mobile number
                /* this is old code commented by rajesh on 2-8-2014
                if (string.IsNullOrEmpty(mobileNo))
                {
                    // To Do Write code to Create password
                    password = new Util().GetAutoGeneratePassword(5);
                }
                else
                {
                    string configPassword = GetPasswordType();
                    if (configPassword == "True")
                    {
                        password = mobileNo;
                    }
                    else {
                        password = "123"; // this is the default password
                    }
                    
                    
                }
                 */
                string configPassword = GetPasswordType();

                if (string.IsNullOrEmpty(mobileNo))
                {
                    password = "123";
                }
                else
                {
                    password = mobileNo;
                }

                string userNameType = ConfigurationManager.AppSettings["UserNameType"];

                if (userNameType == "1")
                {
                    if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && lastName.Trim() != ".")
                    {
                        userName = firstName.ToLower() + "." + lastName.ToLower();
                    }
                    else if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(father) && father.Trim() != ".")
                    {
                        userName = firstName.Trim().ToLower() + "." + father.Trim().ToLower();
                    }
                    else if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(mother) && mother.Trim() != ".")
                    {
                        userName = firstName.Trim().ToLower() + "." + mother.Trim().ToLower();
                    }
                    else
                    {
                        userName = firstName.Trim().ToLower();
                    }
                }
                else if (userNameType == "2")
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        userName = email;
                    }
                    else
                    { continue; }
                }

                userName = userName.Replace(" ", "");

                UserDetailsDAO userDAO = new UserDetailsDAO();
                UserLoginsDAO userLoginDAO = new UserLoginsDAO();

                //UserDetails users = userDAO.GetUserByUserName(userName, cnxnString, logPath);
                UserLogins users = userLoginDAO.GetUserByUserName(userName, logPath);

                if (users == null || string.IsNullOrEmpty(users.UserName))
                {
                    // TO DO insert record
                    userDAO.CreateUser(DateTime.Now, DateTime.Now, firstName, lastName, email, "User", userName, password, true, true, false, false, "Yes", mobileNo,
                        father, mother, rollNo, course, subCourse, batchYear, cnxnString, logPath);

                    string adminConnnectionString = ConfigurationManager.ConnectionStrings["AdminApp_cnxnString"].ConnectionString;

                    // inserting one record in addmin apps database for login purpuse
                    userLoginDAO.AddUserLoginsDetail(Session["CollegeName"].ToString(), userName, password, DateTime.Now, Session["ConnectionString"].ToString(), Session["PDFFolderPath"].ToString(),
                        Session["ReleaseFilePath"].ToString(), Session["LogFilePath"].ToString(), "User", Session["ApplicationType"].ToString(), "~/Dashboard.aspx", true, logPath);
                }
                else
                {
                    if (userNameType != "2")
                    {
                        //int userNameCount = userDAO.GetUserCountByUserName(userName, cnxnString, logPath);
                        int userNameCount = userLoginDAO.GetUserCountByUserName(userName, logPath);
                        userName = userName + userNameCount.ToString();

                        // To DO Insert record
                        userDAO.CreateUser(DateTime.Now, DateTime.Now, firstName, lastName, email, "User", userName, password, true, true, false, false, "Yes", mobileNo,
                            father, mother, rollNo, course, subCourse, batchYear, cnxnString, logPath);

                        // inserting one record in addmin apps database for login purpuse
                        userLoginDAO.AddUserLoginsDetail(Session["CollegeName"].ToString(), userName, password, DateTime.Now, Session["ConnectionString"].ToString(), Session["PDFFolderPath"].ToString(),
                            Session["ReleaseFilePath"].ToString(), Session["LogFilePath"].ToString(), "User", Session["ApplicationType"].ToString(), "~/Dashboard.aspx", true, logPath);
                    }
                }

                // inserting one record in studentcoursemapper table for default foc course
                UserDetails user = userDAO.GetUserByUserName(userName, cnxnString, logPath);
                if (user != null)
                {
                    StudentCourseMapper courseMapper = new StudentCourseMapperDAO().GetStudentCourseMapperRecordsByCourseUserId(user.Id, 1, cnxnString, logPath);
                    if (courseMapper == null)
                    {
                        new StudentCourseMapperDAO().AddStudentCourseMapper(user.Id, 1, true, cnxnString, logPath);
                    }
                }
            }

            if (dt.Rows.Count > 0)
            {
                btnSave.Visible = false;
                btnCancel2.Visible = false;

                btnUpload.Visible = true;
                btnCancle.Visible = true;

                gvStudents.DataSource = null;
                gvStudents.DataBind();

                Success.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string GetPasswordType()
    {
        System.Xml.XmlDocument xdoc = new System.Xml.XmlDocument();
        xdoc.Load(configPath);
        string attrVal = string.Empty;
        XmlNode mobile = xdoc.SelectSingleNode("Configuration/PasswordType");
        if (mobile != null)
        {
            XmlNode value = mobile.SelectSingleNode("IsMobileNumber");
            if (value != null)
            {
                attrVal = value.Attributes["Type"].Value;
            }
        }
        return attrVal;
    }
}