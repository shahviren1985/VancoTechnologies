using Newtonsoft.Json;
using QuarterlyReports.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using MySql.Data.MySqlClient;

namespace QuarterlyReports
{
    public class DatabaseOperations
    {
        MySqlConnection con;
        //OdbcDataAdapter odbAdp;
        MySqlCommand cmd = new MySqlCommand();
        //OdbcCommand odbCommand, cmd;
        string getIdQuery = "Select @@Identity";
        //int ID;
        string connection = ConfigurationManager.AppSettings.Get("dbConn").ToString();
        //object obj = new object();

        public MySqlConnection GetConnection()
        {
            con = new MySqlConnection(connection);
            con.Open();
            cmd.Connection = con;
            return con;
        }

        public int ExcuteQuery(string query)
        {
            try
            {
                using (con = GetConnection())
                {
                    using (cmd = new MySqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = getIdQuery;
                        return (int)Convert.ToInt32(cmd.ExecuteScalar().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExcuteUpdateQuery(string query)
        {
            try
            {
                using (con = GetConnection())
                {
                    using (cmd = new MySqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetQuery(string query)
        {
            try
            {
                using (con = GetConnection())
                {
                    using (cmd = new MySqlCommand(query, con))
                    {
                        DataSet dsItems = new DataSet();
                        MySqlDataAdapter odbAdp = new MySqlDataAdapter(query, con);
                        odbAdp.Fill(dsItems);
                        return dsItems;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertTeacherFeedback(TeacherFeedback feedback)
        {
            string insertInsertTeacherFeedbackQuery = "INSERT INTO teacherfeedback (userid,teachercode,subjectcode," +
                                            "collegecode,a1,a2,a3,a4,a5,a6,a7,a8,a9,a10,a11,a12,a13,a14,a15,a16,a17,a18,a19,a20,a21," +
                                            "a22,a23,a24,a25,ipaddress,fwdipaddress) VALUES ('" + feedback.UserId + "','" + feedback.TeacherCode + "','" + feedback.SubjectCode + "'," +
                                            "'" + feedback.CollegeCode + "','" + feedback.A1 + "','" + feedback.A2 + "','" + feedback.A3 + "','" + feedback.A4 + "','" + feedback.A5 + "','" + feedback.A6 + "','" + feedback.A7 + "'," +
                                            "'" + feedback.A8 + "','" + feedback.A9 + "','" + feedback.A10 + "','" + feedback.A11 + "','" + feedback.A12 + "','" + feedback.A13 + "','" + feedback.A14 + "','" + feedback.A15 + "','" + feedback.A16 + "'," +
                                            "'" + feedback.A17 + "','" + feedback.A18 + "','" + feedback.A19 + "','" + feedback.A20 + "','" + feedback.A_21 + "','" + feedback.A_22 + "','" + feedback.A23 + "','" + feedback.A24 + "','" + feedback.A25 + "','" + feedback.IPAddress + "','" + feedback.FwdIPAddresses + "');";

            using (con = GetConnection())
            {
                cmd = new MySqlCommand(insertInsertTeacherFeedbackQuery, con);
                cmd.ExecuteNonQuery();
                cmd.CommandText = getIdQuery;
                return (int)Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
        }

        public int InsertExitForm(ExitForm form)
        {
            try
            {
                string insertExitFormQuery = "INSERT INTO exitform(userid,a1,a4,a5,a8,ipaddress,fwdipaddress) VALUES ('" + form.UserId + "','" + form.A1 + "','" + form.A4 + "','" + form.A5 + "','" + form.A8 + "','" + form.IPAddress + "','" + form.FwdIPAddresses + "');";
                int formId = ExcuteQuery(insertExitFormQuery);
                if (formId <= 0)
                {
                    return -1;
                }
                else
                {
                    if (form.A2 != null && (form.A2.MostValuable != null || form.A2.Valuable != null || form.A2.LessValuable != null))
                    {
                        string insertAnswer2Querty = "INSERT INTO a2(formid,mostvaluable,valuable,lessvaluable) VALUES('" + formId + "','" + form.A2.MostValuable + "','" + form.A2.Valuable + "','" + form.A2.LessValuable + "')";
                        int a2Id = ExcuteQuery(insertAnswer2Querty);
                        if (a2Id <= 0)
                            return 0;
                    }
                    if (form.A3 != null && (form.A3.PostiveEffect != null || form.A3.NegativeEffect != null))
                    {
                        string insertAnswer3Querty = "INSERT INTO a3(formid,postiveeffect,negativeeffect) VALUES('" + formId + "','" + form.A3.PostiveEffect + "','" + form.A3.NegativeEffect + "')";
                        int a3Id = ExcuteQuery(insertAnswer3Querty);
                        if (a3Id <= 0)
                            return 0;
                    }
                    if (form.A6 != null && form.A6.Count > 0)
                    {
                        foreach (A6 a6 in form.A6)
                        {
                            string insertAnswer6Querty = "INSERT INTO a6(formid,deptname,positivecomment,negativecomment) VALUES('" + formId + "','" + a6.DeptName + "','" + a6.PositiveComment + "','" + a6.NegativeComment + "')";
                            int a6Id = ExcuteQuery(insertAnswer6Querty);
                            if (a6Id <= 0)
                                return 0;
                        }
                    }
                    if (form.A7 != null && (form.A7.EventName != null || form.A7.IsParticipated != null))
                    {
                        string insertAnswer7Querty = "INSERT INTO a7(formid,isparticipated,eventname) VALUES('" + formId + "','" + form.A7.IsParticipated + "','" + form.A7.EventName + "')";
                        int a7Id = ExcuteQuery(insertAnswer7Querty);
                        if (a7Id <= 0)
                            return 0;
                    }
                    if (form.A9 != null && form.A9.Count > 0)
                    {
                        foreach (A9 a9 in form.A9)
                        {
                            string insertAnswer9Querty = "INSERT INTO a9(formid,deptname,a1,a2,a3,a4,a5) VALUES('" + formId + "','" + a9.DeptName + "','" + a9.A1 + "','" + a9.A2 + "','" + a9.A3 + "','" + a9.A4 + "','" + a9.A5 + "')";
                            int a9Id = ExcuteQuery(insertAnswer9Querty);
                            if (a9Id <= 0)
                                return 0;
                        }
                    }
                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<string, Dictionary<string, string>> GetSummary(string collegeCode, string userType, List<string> departmentIds)
        {
            try
            {
                Dictionary<string, Dictionary<string, string>> summary = new Dictionary<string, Dictionary<string, string>>();
                for (int i = 0; i < departmentIds.Count; i++)
                {
                    string summaryQuery = string.Format("SELECT a3,count(a3) FROM commonfeedback WHERE collegecode={0} and usertype='{1}' and a2={2} and a3 like '%-%' group by a3", collegeCode, userType, (i + 1).ToString());

                    if (userType.ToLower() == "student")
                    {
                        summaryQuery = string.Format("SELECT a20,count(a2) FROM commonfeedback WHERE collegecode={0} and usertype='{1}' and a2={2} and a20 like '%-%' group by a20", collegeCode, userType, (i + 1).ToString());
                    }

                    Dictionary<string, string> yearWise = new Dictionary<string, string>();

                    using (con = new MySqlConnection(connection))
                    {
                        cmd = new MySqlCommand(summaryQuery, con);
                        MySqlDataReader reader = cmd.ExecuteReader();

                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                yearWise.Add(reader[0].ToString(), reader[1].ToString());
                            }
                        }
                    }

                    summary.Add(departmentIds[i], yearWise);
                }

                return summary;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LoginResponse GetUserDetails(User user)
        {
            string getUserQuery = "SELECT * FROM tbluser where lastName='" + user.LastName + "' and mobileNumber='" + user.MobileNumber + "' ";
            try
            {
                DataSet dsItems = new DataSet();
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    LoginResponse userDetails = new LoginResponse();
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        userDetails.UserId = row["userid"].ToString();
                        userDetails.FirstName = row["firstname"].ToString();
                        userDetails.LastName = row["lastname"].ToString();
                        userDetails.MobileNumber = row["mobilenumber"].ToString();
                        userDetails.CollegeCode = row["collegecode"].ToString();
                        userDetails.Course = row["course"].ToString();
                        userDetails.SubCourse = row["subcourse"].ToString();
                        userDetails.IsActive = row["isactive"].ToString();
                        userDetails.IsFinalYearStudent = row["isfinalyearstudent"].ToString();
                        userDetails.CurrentSemester = row["currentsemester"].ToString();
                        userDetails.RoleType = row["roletype"].ToString();
                        string createdDate = row["datecreated"].ToString();
                        if (!string.IsNullOrEmpty(createdDate))
                            userDetails.DateCreated = Convert.ToDateTime(createdDate);
                    }
                    string getFeedbackQuery = "SELECT teachercode,subjectcode FROM teacherfeedback where userid='" + userDetails.UserId + "'";
                    dsItems = GetQuery(getFeedbackQuery);
                    if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                    {
                        userDetails.FeedbackStatus = new List<FeedbackStatus>();
                        foreach (DataRow row in dsItems.Tables[0].Rows)
                        {
                            FeedbackStatus s = new FeedbackStatus();
                            s.TeacherCode = row["teachercode"].ToString();
                            s.SubjectCode = row["subjectcode"].ToString();
                            userDetails.FeedbackStatus.Add(s);
                        }
                    }
                    string getExitQuery = "SELECT formid FROM exitform where userid='" + userDetails.UserId + "'";
                    dsItems = GetQuery(getExitQuery);
                    if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                    {
                        userDetails.IsExistFormSubmitted = "true";
                    }
                    else
                    {
                        userDetails.IsExistFormSubmitted = "false";
                    }
                    return userDetails;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LoginResponse GetUserDetails(string userId)
        {
            string getUserQuery = "SELECT * FROM tbluser where userid='" + userId + "'";
            try
            {
                DataSet dsItems = new DataSet();
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    LoginResponse userDetails = new LoginResponse();
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        userDetails.UserId = row["userid"].ToString();
                        userDetails.FirstName = row["firstname"].ToString();
                        userDetails.LastName = row["lastname"].ToString();
                        userDetails.MobileNumber = row["mobilenumber"].ToString();
                        userDetails.CollegeCode = row["collegecode"].ToString();
                        userDetails.Course = row["course"].ToString();
                        userDetails.SubCourse = row["subcourse"].ToString();
                        userDetails.IsActive = row["isactive"].ToString();
                        userDetails.IsFinalYearStudent = row["isfinalyearstudent"].ToString();
                        userDetails.CurrentSemester = row["currentsemester"].ToString();
                        userDetails.RoleType = row["roletype"].ToString();
                        string createdDate = row["datecreated"].ToString();
                        if (!string.IsNullOrEmpty(createdDate))
                            userDetails.DateCreated = Convert.ToDateTime(createdDate);
                    }

                    return userDetails;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LoginResponse> GetUserDetailsByCollegeCode(string collegeCode)
        {
            string getUserQuery = "SELECT * FROM tbluser where collegecode='" + collegeCode + "' and isactive='yes'";
            try
            {
                DataSet dsItems = new DataSet();
                dsItems = GetQuery(getUserQuery);
                List<LoginResponse> listOfLoginResponse = new List<LoginResponse>();
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        LoginResponse userDetails = new LoginResponse();
                        userDetails.UserId = row["userid"].ToString();
                        userDetails.FirstName = row["firstname"].ToString();
                        userDetails.LastName = row["lastname"].ToString();
                        //userDetails.MobileNumber = row["mobilenumber"].ToString();
                        userDetails.CollegeCode = row["collegecode"].ToString();
                        userDetails.Course = row["course"].ToString();
                        userDetails.SubCourse = row["subcourse"].ToString();
                        //userDetails.IsActive = row["isactive"].ToString();
                        //userDetails.IsFinalYearStudent = row["isfinalyearstudent"].ToString();
                        //userDetails.CurrentSemester = row["currentsemester"].ToString();
                        //userDetails.RoleType = row["roletype"].ToString();
                        //string createdDate = row["datecreated"].ToString();
                        //if (!string.IsNullOrEmpty(createdDate))
                        //   userDetails.DateCreated = Convert.ToDateTime(createdDate);
                        listOfLoginResponse.Add(userDetails);

                    }
                    //string getFeedbackQuery = "SELECT teacherCode FROM teacherfeedback where userid='" + userDetails.UserId + "'";
                    //dsItems = GetQuery(getFeedbackQuery);
                    //if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                    //{
                    //    userDetails.FeedbackStatus = new List<string>();
                    //    foreach (DataRow row in dsItems.Tables[0].Rows)
                    //    {
                    //        userDetails.FeedbackStatus.Add(row["teacherCode"].ToString());
                    //    }
                    //}
                }
                else
                    return null;

                return listOfLoginResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ExportFeedbackFormDetails(string collageCode, DateTime fromDate, DateTime toDate)
        {
            string getUserQuery = "SELECT * FROM teacherfeedback where collegecode='" + collageCode + "' and createdDate>='" + fromDate.ToString("yyyy-MM-ddTHH:mm:ss") + "' and createdDate<='" + toDate.ToString("yyyy-MM-ddTHH:mm:ss") + "' order by teachercode,subjectcode ";
            DataTable table = null;
            DataSet dsItems = new DataSet();
            string csvString = string.Empty;
            try
            {
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables != null && dsItems.Tables.Count > 0)
                    table = dsItems.Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                    DataTable dt = new DataTable("FeedbackList");
                    dt.Columns.Add("Serial Number");
                    dt.Columns.Add("Student Name");
                    dt.Columns.Add("Course");
                    dt.Columns.Add("SubCourse");
                    dt.Columns.Add("Teacher Name");
                    dt.Columns.Add("Teacher Code");
                    dt.Columns.Add("Subject Code");
                    dt.Columns.Add("A1");
                    dt.Columns.Add("A2");
                    dt.Columns.Add("A3");
                    dt.Columns.Add("A4");
                    dt.Columns.Add("A5");
                    dt.Columns.Add("A6");
                    dt.Columns.Add("A7");
                    dt.Columns.Add("A8");
                    dt.Columns.Add("A9");
                    dt.Columns.Add("A10");
                    dt.Columns.Add("A11");
                    dt.Columns.Add("A12");
                    dt.Columns.Add("A13");
                    dt.Columns.Add("A14");
                    dt.Columns.Add("A15");
                    dt.Columns.Add("A16");
                    dt.Columns.Add("A17");
                    dt.Columns.Add("A18");
                    dt.Columns.Add("A19");
                    dt.Columns.Add("A20");
                    dt.Columns.Add("Quality1");
                    dt.Columns.Add("Quality2");
                    dt.Columns.Add("Change1");
                    dt.Columns.Add("Change2");

                    string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/UserData/teachers_" + collageCode + ".json");
                    string allText = System.IO.File.ReadAllText(filePath);
                    List<TeacherDetails> teacherDetails = JsonConvert.DeserializeObject<List<TeacherDetails>>(allText);


                    int counter = 0;
                    string prevTCode = string.Empty, prevSCode = string.Empty;
                    foreach (DataRow row in table.Rows)
                    {


                        counter++;
                        LoginResponse userDetails = GetUserDetails(row["userId"].ToString());
                        if (userDetails == null)
                            return csvString;

                        DataRow r = dt.NewRow();
                        r["Serial Number"] = counter.ToString();
                        r["Student Name"] = userDetails.FirstName + " " + userDetails.LastName;
                        r["Course"] = userDetails.Course;
                        r["SubCourse"] = userDetails.SubCourse;
                        string teacherCode = Convert.ToString(row["teacherCode"]);
                        TeacherDetails t = teacherDetails.FirstOrDefault(a => a.TeacherCode.Equals(teacherCode));
                        if (t != null)
                        {
                            r["Teacher Name"] = t.TeacherName;
                        }
                        r["Teacher Code"] = teacherCode;
                        r["Subject Code"] = row["subjectCode"];
                        string subjectCode = Convert.ToString(row["subjectCode"]);

                        if (!string.IsNullOrEmpty(prevTCode) && !string.IsNullOrEmpty(prevSCode))
                        {
                            if (!(prevTCode.Equals(teacherCode) && prevSCode.Equals(subjectCode)))
                            {
                                dt.Rows.Add();
                            }
                        }

                        string a1Value = Convert.ToString(row["a1"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a1"]))).ToDescriptionString();
                        r["A1"] = a1Value;
                        string a2Value = Convert.ToString(row["a2"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a2"]))).ToDescriptionString();
                        r["A2"] = a2Value;
                        string a3Value = Convert.ToString(row["a3"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a3"]))).ToDescriptionString();
                        r["A3"] = a3Value;
                        string a4Value = Convert.ToString(row["a4"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a4"]))).ToDescriptionString();
                        r["A4"] = a4Value;
                        string a5Value = Convert.ToString(row["a5"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a5"]))).ToDescriptionString();
                        r["A5"] = a5Value;
                        string a6Value = Convert.ToString(row["a6"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a6"]))).ToDescriptionString();
                        r["A6"] = a6Value;
                        string a7Value = Convert.ToString(row["a7"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a7"]))).ToDescriptionString();
                        r["A7"] = a7Value;
                        string a8Value = Convert.ToString(row["a8"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a8"]))).ToDescriptionString();
                        r["A8"] = a8Value;
                        string a9Value = Convert.ToString(row["a9"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a9"]))).ToDescriptionString();
                        r["A9"] = a9Value;
                        string a10Value = Convert.ToString(row["a10"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a10"]))).ToDescriptionString();
                        r["A10"] = a10Value;
                        string a11Value = Convert.ToString(row["a11"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a11"]))).ToDescriptionString();
                        r["A11"] = a11Value;
                        string a12Value = Convert.ToString(row["a12"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a12"]))).ToDescriptionString();
                        r["A12"] = a12Value;
                        string a13Value = Convert.ToString(row["a13"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a13"]))).ToDescriptionString();
                        r["A13"] = a13Value;
                        string a14Value = Convert.ToString(row["a14"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a14"]))).ToDescriptionString();
                        r["A14"] = a14Value;
                        string a15Value = Convert.ToString(row["a15"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a15"]))).ToDescriptionString();
                        r["A15"] = a15Value;
                        string a16Value = Convert.ToString(row["a16"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a16"]))).ToDescriptionString();
                        r["A16"] = a16Value;
                        string a17Value = Convert.ToString(row["a17"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a17"]))).ToDescriptionString();
                        r["A17"] = a17Value;
                        string a18Value = Convert.ToString(row["a18"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a18"]))).ToDescriptionString();
                        r["A18"] = a18Value;
                        string a19Value = Convert.ToString(row["a19"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a19"]))).ToDescriptionString();
                        r["A19"] = a19Value;
                        string a20Value = Convert.ToString(row["a20"]);//((FeedbakFormEnum)int.Parse(Convert.ToString(row["a20"]))).ToDescriptionString();
                        r["A20"] = a20Value;
                        string a21Value = Convert.ToString(row["a21"]);
                        if (!string.IsNullOrEmpty(a21Value))
                        {
                            string[] a21 = a21Value.Split('|');
                            if (a21.Count() > 1)
                            {
                                r["Quality1"] = string.IsNullOrEmpty(a21[0]) ? "" : a21[0].Replace("~~~", "'");
                                r["Quality2"] = string.IsNullOrEmpty(a21[1]) ? "" : a21[1].Replace("~~~", "'");
                            }
                        }
                        string a22Value = Convert.ToString(row["a22"]);
                        if (!string.IsNullOrEmpty(a22Value))
                        {
                            string[] a22 = a22Value.Split('|');
                            if (a22.Count() > 1)
                            {
                                r["Change1"] = string.IsNullOrEmpty(a22[0]) ? "" : a22[0].Replace("~~~", "'");
                                r["Change2"] = string.IsNullOrEmpty(a22[1]) ? "" : a22[1].Replace("~~~", "'");
                            }
                        }
                        dt.Rows.Add(r);
                        prevTCode = teacherCode;
                        prevSCode = subjectCode;
                    }
                    csvString = ExportDataTableToCSV(dt);
                }
                return csvString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<A2> GetA2(int formId)
        {
            string getUserQuery = "SELECT * FROM a2 where formid=" + formId;
            DataTable table = null;
            DataSet dsItems = new DataSet();
            string csvString = string.Empty;
            try
            {
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables != null && dsItems.Tables.Count > 0)
                    table = dsItems.Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                    List<A2> a2List = new List<A2>();
                    foreach (DataRow row in table.Rows)
                    {
                        A2 a2 = new A2();
                        a2.Id = (int)row["id"];
                        a2.LessValuable = (string)row["lessvaluable"];
                        a2.Valuable = (string)row["valuable"];
                        a2.MostValuable = (string)row["mostvaluable"];
                        a2List.Add(a2);
                    }
                    return a2List;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<A3> GetA3(int formId)
        {
            string getUserQuery = "SELECT * FROM a3 where formid=" + formId;
            DataTable table = null;
            DataSet dsItems = new DataSet();
            string csvString = string.Empty;
            try
            {
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables != null && dsItems.Tables.Count > 0)
                    table = dsItems.Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                    List<A3> a3List = new List<A3>();
                    foreach (DataRow row in table.Rows)
                    {
                        A3 a3 = new A3();
                        a3.Id = (int)row["id"];
                        a3.PostiveEffect = (string)row["postiveeffect"];
                        a3.NegativeEffect = (string)row["negativeeffect"];
                        a3List.Add(a3);
                    }
                    return a3List;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<A6> GetA6(int formId)
        {
            string getUserQuery = "SELECT * FROM a6 where formid=" + formId;
            DataTable table = null;
            DataSet dsItems = new DataSet();
            string csvString = string.Empty;
            try
            {
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables != null && dsItems.Tables.Count > 0)
                    table = dsItems.Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                    List<A6> a6List = new List<A6>();
                    foreach (DataRow row in table.Rows)
                    {
                        A6 a6 = new A6();
                        a6.Id = (int)row["id"];
                        a6.DeptName = (string)row["deptname"];
                        a6.PositiveComment = (string)row["positivecomment"];
                        a6.NegativeComment = (string)row["negativecomment"];
                        a6List.Add(a6);
                    }
                    return a6List;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<A7> GetA7(int formId)
        {
            string getUserQuery = "SELECT * FROM a7 where formid=" + formId;
            DataTable table = null;
            DataSet dsItems = new DataSet();
            string csvString = string.Empty;
            try
            {
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables != null && dsItems.Tables.Count > 0)
                    table = dsItems.Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                    List<A7> a7List = new List<A7>();
                    foreach (DataRow row in table.Rows)
                    {
                        A7 a7 = new A7();
                        a7.Id = (int)row["id"];
                        a7.IsParticipated = (string)row["isparticipated"];
                        a7.EventName = (string)row["eventname"];
                        a7List.Add(a7);
                    }
                    return a7List;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<A9> GetA9(int formId)
        {
            string getUserQuery = "SELECT * FROM a9 where formid=" + formId;
            DataTable table = null;
            DataSet dsItems = new DataSet();
            string csvString = string.Empty;
            try
            {
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables != null && dsItems.Tables.Count > 0)
                    table = dsItems.Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                    List<A9> a9List = new List<A9>();
                    foreach (DataRow row in table.Rows)
                    {
                        A9 a9 = new A9();
                        a9.Id = (int)row["id"];
                        a9.DeptName = (string)row["deptname"];
                        a9.A1 = (int)row["A1"];
                        a9.A2 = (int)row["A2"];
                        a9.A3 = (int)row["A3"];
                        a9.A4 = (int)row["A4"];
                        a9.A5 = (int)row["A5"];
                        a9List.Add(a9);
                    }
                    return a9List;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ExportExitFormDetails(string collageCode, DateTime fromDate, DateTime toDate)
        {
            List<LoginResponse> userList = GetUserDetailsByCollegeCode(collageCode);
            if (userList == null || userList.Count == 0)
                return null;

            List<string> listOfIds = new List<string>();
            foreach (LoginResponse response in userList)
            {
                listOfIds.Add(response.UserId);
            }
            string ids = string.Join("','", listOfIds.Select(i => i.Replace("'", "''")).ToArray());
            string getUserQuery = "SELECT * FROM exitform where userId in ('" + ids + "')  and datecreated>='" + fromDate.ToString("yyyy-MM-dd HH:mm:ss") + "' and datecreated<='" + toDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            DataTable table = null;
            DataSet dsItems = new DataSet();
            string csvString = string.Empty;
            try
            {
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables != null && dsItems.Tables.Count > 0)
                    table = dsItems.Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                    DataTable dt = new DataTable("ExitFormList");
                    dt.Columns.Add("Serial Number");
                    dt.Columns.Add("Student Name");
                    dt.Columns.Add("Graduation Plan");
                    dt.Columns.Add("Most Valuable");
                    dt.Columns.Add("Valuable");
                    dt.Columns.Add("Less Valuable");
                    dt.Columns.Add("Postive Effect");
                    dt.Columns.Add("Negative Effect");
                    dt.Columns.Add("Keep In Touch");
                    dt.Columns.Add("Experience");
                    dt.Columns.Add("Department1");
                    dt.Columns.Add("Positive Comment1");
                    dt.Columns.Add("Negative Comment1");
                    dt.Columns.Add("Department2");
                    dt.Columns.Add("Positive Comment2");
                    dt.Columns.Add("Negative Comment2");
                    dt.Columns.Add("Event Participated");
                    dt.Columns.Add("Event Name");
                    dt.Columns.Add("Contribution");
                    dt.Columns.Add("Canteen_Quality of food");
                    dt.Columns.Add("Canteen_Price bf food");
                    dt.Columns.Add("Canteen_Variety of food");
                    dt.Columns.Add("Canteen_Service");
                    dt.Columns.Add("Canteen_Hygiene");
                    dt.Columns.Add("Library_Book availability");
                    dt.Columns.Add("Library_Staff helpfullness in getting book");
                    dt.Columns.Add("Library_Adequate Space/Seating");
                    dt.Columns.Add("Library_Library Timing");
                    dt.Columns.Add("Library_Frequency of your library visits");
                    dt.Columns.Add("Office_Co-operation");
                    dt.Columns.Add("Office_Information");
                    dt.Columns.Add("Office_Timelines");
                    dt.Columns.Add("Office_Politeness");
                    dt.Columns.Add("Gym/FitnessCenter_Timing");
                    dt.Columns.Add("Gym/FitnessCenter_Environment");
                    dt.Columns.Add("Gym/FitnessCenter_Equipment");
                    dt.Columns.Add("Gym/FitnessCenter_Traning");
                    dt.Columns.Add("Sport_Facilities");
                    dt.Columns.Add("Sport_Equipment");
                    dt.Columns.Add("Sport_Traning");
                    dt.Columns.Add("HealhServices_Regularity of health checkup");
                    dt.Columns.Add("HealhServices_Availibity of emegency medical facilities");
                    dt.Columns.Add("HealhServices_Quality of the Psychological counselling service provided");

                    int counter = 0;
                    foreach (DataRow row in table.Rows)
                    {
                        counter++;
                        DataRow r = dt.NewRow();
                        int formId = (int)row["formid"];

                        r["Serial Number"] = counter.ToString();
                        string userId = Convert.ToString(row["userId"]);
                        LoginResponse userDetails = userList.FirstOrDefault(a => a.UserId.Equals(userId));
                        r["Student Name"] = userDetails.FirstName + " " + userDetails.LastName;
                        r["Graduation Plan"] = row["a1"];
                        List<A2> a2List = GetA2(formId);
                        if (a2List != null && a2List.Count > 0)
                        {
                            r["Valuable"] = a2List[0].Valuable;
                            r["Less Valuable"] = a2List[0].LessValuable;
                            r["Most Valuable"] = a2List[0].MostValuable;
                        }
                        List<A3> a3List = GetA3(formId);
                        if (a3List != null && a3List.Count > 0)
                        {
                            r["Postive Effect"] = a3List[0].PostiveEffect;
                            r["Negative Effect"] = a3List[0].NegativeEffect;
                        }
                        r["Keep In Touch"] = row["a4"];
                        r["Experience"] = row["a5"];
                        List<A6> a6List = GetA6(formId);
                        if (a6List != null && a6List.Count > 0)
                        {
                            r["Department1"] = a6List[0].DeptName;
                            r["Positive Comment1"] = a6List[0].PositiveComment;
                            r["Negative Comment1"] = a6List[0].NegativeComment;
                            if (a6List.Count > 1)
                            {
                                r["Department2"] = a6List[1].DeptName;
                                r["Positive Comment2"] = a6List[1].PositiveComment;
                                r["Negative Comment2"] = a6List[1].NegativeComment;
                            }
                        }
                        List<A7> a7List = GetA7(formId);
                        if (a7List != null && a7List.Count > 0)
                        {
                            r["Event Participated"] = a7List[0].IsParticipated;
                            r["Event Name"] = a7List[0].EventName;

                        }
                        r["Contribution"] = row["a8"];

                        List<A9> a9List = GetA9(formId);
                        if (a9List != null && a9List.Count > 0)
                        {
                            foreach (A9 a9 in a9List)
                            {
                                if (a9.DeptName.ToLower().Equals("canteen"))
                                {
                                    r["Canteen_Quality of food"] = a9.A1;
                                    r["Canteen_Price bf food"] = a9.A2;
                                    r["Canteen_Variety of food"] = a9.A3;
                                    r["Canteen_Service"] = a9.A4;
                                    r["Canteen_Hygiene"] = a9.A5;
                                }
                                else if (a9.DeptName.ToLower().Equals("library"))
                                {
                                    r["Library_Book availability"] = a9.A1;
                                    r["Library_Staff helpfullness in getting book"] = a9.A2;
                                    r["Library_Adequate Space/Seating"] = a9.A3;
                                    r["Library_Library Timing"] = a9.A4;
                                    r["Library_Frequency of your library visits"] = a9.A5;
                                }
                                else if (a9.DeptName.ToLower().Equals("office"))
                                {
                                    r["Office_Co-operation"] = a9.A1;
                                    r["Office_Information"] = a9.A2;
                                    r["Office_Timelines"] = a9.A3;
                                    r["Office_Politeness"] = a9.A4;
                                }
                                else if (a9.DeptName.ToLower().Equals("gym/fitness center"))
                                {
                                    r["Gym/FitnessCenter_Timing"] = a9.A1;
                                    r["Gym/FitnessCenter_Environment"] = a9.A2;
                                    r["Gym/FitnessCenter_Equipment"] = a9.A3;
                                    r["Gym/FitnessCenter_Traning"] = a9.A4;
                                }
                                else if (a9.DeptName.ToLower().Equals("sport"))
                                {
                                    r["Sport_Facilities"] = a9.A1;
                                    r["Sport_Equipment"] = a9.A2;
                                    r["Sport_Traning"] = a9.A3;
                                }
                                else if (a9.DeptName.ToLower().Equals("health services"))
                                {
                                    r["HealhServices_Regularity of health checkup"] = a9.A1;
                                    r["HealhServices_Availibity of emegency medical facilities"] = a9.A2;
                                    r["HealhServices_Quality of the Psychological counselling service provided"] = a9.A3;
                                }
                            }
                        }
                        dt.Rows.Add(r);
                    }
                    csvString = ExportDataTableToCSV(dt);
                }
                return csvString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ExportDataTableToCSV(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append(dt.Columns[i].ColumnName + ',');
            }
            sb.Append(Environment.NewLine);

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    sb.Append(dt.Rows[j][k].ToString().Replace("\n", "").Replace(",", " ") + ',');
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

        public int InsertUserDeatils(LoginResponse user)
        {
            string insertUserQuery = "INSERT INTO tbluser(`firstname`, `lastname`, `mobilenumber`, `collegecode`, `course`, `subcourse`," +
                "`isactive`, `isfinalyearstudent`, `currentsemester`, `roletype`,`acedemicyear`,`submissionid`) VALUES ('" + user.FirstName + "', '" + user.LastName + "', '" + user.MobileNumber + "'," +
                "'" + user.CollegeCode + "', '" + user.Course + "','" + user.SubCourse + "', '" + user.IsActive + "', '" + user.IsFinalYearStudent + "', '" + user.CurrentSemester + "', " +
                "'" + user.RoleType + "', '" + user.AcedemicYear + "', '" + user.SubmissionId + "');";

            using (con = GetConnection())
            {
                cmd.CommandText = insertUserQuery;
                cmd.ExecuteNonQuery();
                cmd.CommandText = getIdQuery;
                return (int)Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }
            //return ExcuteQuery(insertUserQuery);
        }

        public int UpdateUserDeatils(LoginResponse user)
        {
            string updateUserQuery = "Update tbluser SET firstname ='" + user.FirstName + "',lastname = '" + user.LastName + "',mobilenumber = '" + user.MobileNumber + "'," +
            "collegecode = '" + user.CollegeCode + "',course = '" + user.Course + "',subcourse = '" + user.SubCourse + "',isactive = '" + user.IsActive + "'," +
            "isfinalyearstudent = '" + user.IsFinalYearStudent + "',currentsemester = '" + user.CurrentSemester + "',roletype = '" + user.RoleType + "'" +
            "WHERE userid = '" + user.UserId + "';";
            return ExcuteUpdateQuery(updateUserQuery);
        }

        public string ExportUsers(string collageCode)
        {
            string getUserQuery = "SELECT * FROM tbluser where collegecode='" + collageCode + "'";
            DataTable table = null;
            DataSet dsItems = new DataSet();
            string csvString = string.Empty;
            try
            {
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables != null && dsItems.Tables.Count > 0)
                    table = dsItems.Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                    csvString = ExportDataTableToCSV(table);
                }
                return csvString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ExportStudenTeacherFeedback(string collageCode)
        {
            string getUserQuery = "SELECT * FROM tbluser where collegecode='" + collageCode + "'";
            try
            {

                string csvString = string.Empty;
                DataTable dt = new DataTable("ExportUser");
                dt.Columns.Add("User Id");
                dt.Columns.Add("First Name");
                dt.Columns.Add("Last Name");
                dt.Columns.Add("Course");
                dt.Columns.Add("Sub Course");

                DataSet dsItems = new DataSet();
                DataSet dsItems2 = new DataSet();
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    //DataRow r=null;
                    LoginResponse userDetails = new LoginResponse();
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        DataRow r = dt.NewRow();
                        r["User Id"] = row["userid"].ToString();
                        r["First Name"] = row["firstname"].ToString();
                        r["Last Name"] = row["lastname"].ToString();
                        //userDetails.MobileNumber = row["mobilenumber"].ToString();
                        //userDetails.CollegeCode = row["collegecode"].ToString();
                        r["Course"] = row["course"].ToString();
                        r["Sub Course"] = row["subcourse"].ToString();
                        //userDetails.IsActive = row["isactive"].ToString();
                        //userDetails.IsFinalYearStudent = row["isfinalyearstudent"].ToString();
                        //userDetails.CurrentSemester = row["currentsemester"].ToString();
                        //userDetails.RoleType = row["roletype"].ToString();
                        //string createdDate = row["datecreated"].ToString();
                        //if (!string.IsNullOrEmpty(createdDate))
                        //    userDetails.DateCreated = Convert.ToDateTime(createdDate);
                        string getFeedbackQuery = "SELECT teacherCode FROM teacherfeedback where userid='" + row["userid"] + "'";
                        dsItems2 = GetQuery(getFeedbackQuery);
                        if (dsItems2.Tables[0].Rows != null && dsItems2.Tables[0].Rows.Count > 0)
                        {
                            int counter = 1;
                            //userDetails.FeedbackStatus = new List<string>();
                            foreach (DataRow row2 in dsItems2.Tables[0].Rows)
                            {
                                string clmCode = "Code" + counter;
                                if (!dt.Columns.Contains(clmCode))
                                    dt.Columns.Add(clmCode, typeof(String));

                                r[clmCode] = row2["teacherCode"].ToString();
                                //userDetails.FeedbackStatus.Add(row["teacherCode"].ToString());
                                counter++;
                            }
                        }
                        dt.Rows.Add(r);
                    }
                    csvString = ExportDataTableToCSV(dt);
                    return csvString;
                    //return userDetails;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable ConvertToDataTable<T>(IList<T> data)
        {

            PropertyDescriptorCollection properties =

            TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)

                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {

                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)

                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                table.Rows.Add(row);

            }

            return table;

        }

        public string ExportNotInFeedbackUser(string Subject, int count)
        {
            string getUserQuery = "";
            if (string.IsNullOrEmpty(Subject))
            {
                //getUserQuery = "select userid,firstname,lastname,course,subcourse from FeedbackDB1.tbluser where userid in(SELECT userid FROM FeedbackDB1.teacherfeedback GROUP BY userid having COUNT(userid) <= "+count+")";
                getUserQuery = "select userid,firstname,lastname,course,subcourse from tbluser where userid not in (select userid from teacherfeedback group by userid having count(subjectcode)>" + count + ")";
            }
            else
            {
                getUserQuery = "select userid,firstname,lastname,course,subcourse from tbluser where Course='" + Subject + "' and userid not in (select userid from teacherfeedback group by userid having count(subjectcode)>" + count + ")";
            }
            try
            {
                string csvString = string.Empty;
                DataTable dt = new DataTable("ExportUser");
                dt.Columns.Add("User Id");
                dt.Columns.Add("First Name");
                dt.Columns.Add("Last Name");
                dt.Columns.Add("Course");
                dt.Columns.Add("Sub Course");
                DataSet dsItems = new DataSet();
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        DataRow r = dt.NewRow();
                        r["User Id"] = row["userid"].ToString();
                        r["First Name"] = row["firstname"].ToString();
                        r["Last Name"] = row["lastname"].ToString();
                        r["Course"] = row["course"].ToString();
                        r["Sub Course"] = row["subcourse"].ToString();
                        dt.Rows.Add(r);
                    }
                    csvString = ExportDataTableToCSV(dt);
                    return csvString;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable[] AcademicAdministratorReportCard(string adminName, string collageCode, string academicYear)
        {
            string getUserQuery = "select * from academicfeedback where a1='" + adminName + "' and collegecode='" + collageCode + "' and a32='" + academicYear + "'";
            try
            {
                DataTable enumDt = new DataTable();
                enumDt.Columns.Add("RATING");
                enumDt.Columns.Add("ENUMDESCRIPTION");

                foreach (CommonFeedbakFormEnum val in Enum.GetValues(typeof(CommonFeedbakFormEnum)))
                {
                    DataRow r = enumDt.NewRow();
                    r["RATING"] = (int)val;
                    r["ENUMDESCRIPTION"] = val.ToDescriptionString();

                    enumDt.Rows.Add(r);
                }

                string csvString = string.Empty;
                DataTable dt = new DataTable("AcademicAdministratorReportCard");
                dt.Columns.Add("SR NO.");
                dt.Columns.Add("THE ADIMINISTRATOR");
                dt.Columns.Add("AVERAGE");
                dt.Columns.Add("PERCENTAGE");
                DataSet dsItems = new DataSet();
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + collageCode + "_AcademicAdministrator_Questions.json");
                    string allText = System.IO.File.ReadAllText(filePath);
                    List<FeedbackQuestions> response = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);

                    List<string> tLeadershipAns = new List<string>() { "a2", "a3", "a4", "a5", "a6" };
                    List<string> tAdministratorAns = new List<string>() { "a7", "a8", "a9", "a10", "a11", "a12", "a13", "a14", "a15", "a16", "a17", "a18" };
                    List<string> tCommunicationAns = new List<string>() { "a19", "a20", "a21", "a22", "a23", "a24", "a25" };
                    List<string> tOverall = new List<string> { "a26" };

                    double sumLeadershipAns = 0;
                    double sumAdministratorAns = 0;
                    double sumCommunicationAns = 0;
                    double sumOverall = 0;

                    double total = 0;
                    var ienumlist = dsItems.Tables[0].AsEnumerable();
                    for (int i = 2; i <= response.Count; i++)
                    {
                        string columnname = "a" + i;
                        DataRow r = dt.NewRow();
                        r["SR NO."] = i;
                        r["THE ADIMINISTRATOR"] = response.FirstOrDefault(a => a.Id.Equals(columnname)).Question.Replace("The teacher", "");
                        double sum = 0;
                        double counter = 0;
                        var dRows = ienumlist.Select(s => s.Field<string>(columnname));//.Select("a" + i);
                        foreach (string dr in dRows)
                        {
                            int sValue = 0;
                            string strValue = dr.Trim().Substring(0, 1);

                            switch (strValue)
                            {
                                case "E":
                                    strValue = "5";
                                    break;
                                case "V":
                                    strValue = "4";
                                    break;
                                case "G":
                                    strValue = "3";
                                    break;
                                case "A":
                                    strValue = "2";
                                    break;
                                case "N":
                                    strValue = "1";
                                    break;
                            }

                            bool isParse = int.TryParse(strValue, out sValue);
                            if (isParse)
                            {
                                counter++;
                                sum += sValue;

                            }
                        }
                        if (counter > 0)
                        {
                            double avg = Math.Round(sum / counter, 2);
                            total += avg;
                            r["AVERAGE"] = avg;
                            if (tLeadershipAns.Contains(columnname))
                            {
                                sumLeadershipAns += avg;
                            }
                            else if (tAdministratorAns.Contains(columnname))
                            {
                                sumAdministratorAns += avg;
                            }
                            else if (tCommunicationAns.Contains(columnname))
                            {
                                sumCommunicationAns += avg;
                            }
                            else if (tOverall.Contains(columnname))
                            {
                                sumOverall += avg;
                            }
                        }
                        else
                        {
                            r["AVERAGE"] = 0;
                        }
                        dt.Rows.Add(r);
                    }

                    ReportCard rc = new ReportCard();
                    rc.TSMaxScore = tLeadershipAns.Count * 5;
                    rc.SKMaxScore = tAdministratorAns.Count * 5;
                    rc.PIMaxScore = tCommunicationAns.Count * 5;
                    rc.OIMaxScore = tOverall.Count * 5;
                    rc.GrandMaxScore = 5;


                    DataRow r1 = dt.NewRow();
                    dt.Rows.Add(r1);
                    DataRow r2 = dt.NewRow();
                    r2["SR NO."] = "";
                    r2["THE ADIMINISTRATOR"] = "GRAND MEAN";
                    double grandMean = Math.Round(total / 25, 2); // Math.Round(dt.AsEnumerable().Average(r => Convert.ToDouble(r.Field<string>("AVERAGE"))), 2);
                    rc.GrandMean = grandMean;
                    rc.GrandPercentage = Math.Round((grandMean * 100) / 5);
                    r2["AVERAGE"] = grandMean;
                    dt.Rows.Add(r2);

                    enumDt = AddBlanlkRowsToDatatable(2, enumDt);
                    dt = AddBlanlkRowsToDatatable(4, dt);

                    DataRow r3 = dt.NewRow();
                    r3["SR NO."] = "";
                    r3["THE ADIMINISTRATOR"] = "Leadership style";

                    r3["AVERAGE"] = sumLeadershipAns;
                    rc.TSScoreObtain = sumLeadershipAns;
                    double per3 = Math.Round((sumLeadershipAns * 100) / (tLeadershipAns.Count * 5));
                    r3["PERCENTAGE"] = per3;
                    rc.TSPerformance = per3;
                    dt.Rows.Add(r3);

                    DataRow r4 = dt.NewRow();
                    r4["SR NO."] = "";
                    r4["THE ADIMINISTRATOR"] = "Administration";
                    r4["AVERAGE"] = sumAdministratorAns;
                    rc.SKScoreObtain = sumAdministratorAns;
                    double per4 = Math.Round((sumAdministratorAns * 100) / (tAdministratorAns.Count * 5));
                    r4["PERCENTAGE"] = per4;
                    rc.SKPerformance = per4;
                    dt.Rows.Add(r4);

                    DataRow r5 = dt.NewRow();
                    r5["SR NO."] = "";
                    r5["THE ADIMINISTRATOR"] = "Communication";
                    r5["AVERAGE"] = sumCommunicationAns;
                    rc.PIScoreObtain = sumCommunicationAns;
                    double per5 = Math.Round((sumCommunicationAns * 100) / (tCommunicationAns.Count * 5));
                    r5["PERCENTAGE"] = per5;
                    rc.PIPerformance = per5;
                    dt.Rows.Add(r5);

                }
                return new DataTable[] { enumDt, dt };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetMDShahFeedabck()
        {
            int currentyear = 2017;
            int nextyear = 18;
            StringBuilder str = new StringBuilder();
            for (int j = 0; j < 3; j++)
            {
                string getUserQuery = "select * from commonfeedback where usertype='Student' and collegecode='105' and feedbackid>3254 and a3='" + currentyear + "-" + nextyear + "';";

                DataSet dsItems = new DataSet();
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    double sum = 0;
                    double counter = 0;
                    var ienumlist = dsItems.Tables[0].AsEnumerable();
                    int totalRows = 0;
                    for (int i = 5; i < 13; i++)
                    {
                        if (i == 12)
                            i = 14;

                        string columnname = "a" + i;
                        var dRows = ienumlist.Select(s => s.Field<string>(columnname));
                        totalRows = dRows.Count();
                        int one = 0;
                        int two = 0;
                        int three = 0;
                        int four = 0;
                        int five = 0;

                        foreach (string dr in dRows)
                        {
                            int sValue = 0;
                            string strValue = string.IsNullOrEmpty(dr) ? "" : dr.Trim().Substring(0, 1);
                            bool isParse = int.TryParse(strValue, out sValue);
                            if (isParse)
                            {
                                counter++;

                                switch (strValue)
                                {
                                    case "1":
                                        sValue = 5;
                                        one++;
                                        break;
                                    case "2":
                                        sValue = 4;
                                        two++;
                                        break;
                                    case "3":
                                        sValue = 3;
                                        three++;
                                        break;
                                    case "4":
                                        sValue = 2;
                                        four++;
                                        break;
                                    case "5":
                                        sValue = 1;
                                        five++;
                                        break;
                                }

                                sum += sValue;
                            }
                        }

                        double avg = Math.Round(sum / counter, 2);
                        str.AppendLine(currentyear + "=" + nextyear + " => a" + i + ": Excellent:" + (one * 100 /totalRows) + ", Very Good:" + (two * 100 / totalRows) + ", Good:" + (three * 100 / totalRows) + ", Average:" + (four * 100 / totalRows) + ", Poor: "+ (five * 100 / totalRows));
                        //str.AppendLine(currentyear + "=" + nextyear + " => a" + i + "=" + avg);
                    }
                }

                currentyear++;
                nextyear++;
            }

            string finalo = str.ToString();
        }

        public ReportCard AcademicAdministratorReportCard2(string adminName, string collageCode, string academicYear)
        {
            ReportCard rc = null;
            string getUserQuery = "select * from academicfeedback where a1='" + adminName + "' and collegecode='" + collageCode + "' and a32='" + academicYear + "'";
            try
            {
                DataTable enumDt = new DataTable();
                enumDt.Columns.Add("RATING");
                enumDt.Columns.Add("ENUMDESCRIPTION");
                foreach (FeedbakFormEnum val in Enum.GetValues(typeof(FeedbakFormEnum)))
                {
                    DataRow r = enumDt.NewRow();
                    r["RATING"] = (int)val;
                    r["ENUMDESCRIPTION"] = val.ToDescriptionString(); ;
                    enumDt.Rows.Add(r);
                }

                string csvString = string.Empty;
                DataTable dt = new DataTable("AdminReportCard");
                dt.Columns.Add("SR NO.");
                dt.Columns.Add("THE ADMINISTRATOR");
                dt.Columns.Add("AVERAGE");
                dt.Columns.Add("PERCENTAGE");
                DataSet dsItems = new DataSet();
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + collageCode + "_AcademicAdministrator_Questions.json");
                    string allText = System.IO.File.ReadAllText(filePath);
                    List<FeedbackQuestions> response = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);

                    List<string> tLeadershipAns = new List<string>() { "a2", "a3", "a4", "a5", "a6" };
                    List<string> tAdministratorAns = new List<string>() { "a7", "a8", "a9", "a10", "a11", "a12", "a13", "a14", "a15", "a16", "a17", "a18" };
                    List<string> tCommunicationAns = new List<string>() { "a19", "a20", "a21", "a22", "a23", "a24", "a25" };
                    List<string> tOverall = new List<string> { "a26" };

                    double sumLeadershipAns = 0;
                    double sumAdministratorAns = 0;
                    double sumCommunicationAns = 0;

                    var ienumlist = dsItems.Tables[0].AsEnumerable();
                    for (int i = 2; i <= response.Count; i++)
                    {
                        string columnname = "a" + i;
                        DataRow r = dt.NewRow();
                        r["SR NO."] = i;
                        r["THE ADMINISTRATOR"] = response.FirstOrDefault(a => a.Id.Equals(columnname)).Question.Replace("The teacher", "");
                        double sum = 0;
                        double counter = 0;
                        var dRows = ienumlist.Select(s => s.Field<string>(columnname));//.Select("a" + i);
                        foreach (string dr in dRows)
                        {
                            int sValue = 0;
                            string strValue = string.IsNullOrEmpty(dr) ? "" : dr.Trim().Substring(0, 1);

                            switch (strValue)
                            {
                                case "E":
                                    strValue = "5";
                                    break;
                                case "V":
                                    strValue = "4";
                                    break;
                                case "G":
                                    strValue = "3";
                                    break;
                                case "A":
                                    strValue = "2";
                                    break;
                                case "N":
                                    strValue = "1";
                                    break;
                            }

                            bool isParse = int.TryParse(strValue, out sValue);
                            if (isParse)
                            {
                                counter++;
                                sum += sValue;
                            }
                        }
                        if (counter > 0)
                        {
                            double avg = Math.Round(sum / counter, 2);
                            r["AVERAGE"] = avg;
                            if (tLeadershipAns.Contains(columnname))
                            {
                                sumLeadershipAns += avg;
                            }
                            else if (tAdministratorAns.Contains(columnname))
                            {
                                sumAdministratorAns += avg;
                            }
                            else if (tCommunicationAns.Contains(columnname))
                            {
                                sumCommunicationAns += avg;
                            }
                        }
                        else
                        {
                            r["AVERAGE"] = 0;
                        }
                        dt.Rows.Add(r);
                    }

                    rc = new ReportCard();
                    rc.TSMaxScore = tLeadershipAns.Count * 5;
                    rc.SKMaxScore = tAdministratorAns.Count * 5;
                    rc.PIMaxScore = tCommunicationAns.Count * 5;
                    rc.GrandMaxScore = 5;

                    DataRow r1 = dt.NewRow();
                    dt.Rows.Add(r1);
                    DataRow r2 = dt.NewRow();
                    r2["SR NO."] = "";
                    r2["THE ADMINISTRATOR"] = "GRAND MEAN";
                    double grandMean = Math.Round(dt.AsEnumerable().Average(r => Convert.ToDouble(r.Field<string>("AVERAGE"))), 2);
                    rc.GrandMean = grandMean;
                    rc.GrandPercentage = Math.Round((grandMean * 100) / 5);
                    r2["AVERAGE"] = grandMean;
                    dt.Rows.Add(r2);

                    enumDt = AddBlanlkRowsToDatatable(2, enumDt);
                    dt = AddBlanlkRowsToDatatable(4, dt);

                    DataRow r3 = dt.NewRow();
                    r3["SR NO."] = "";
                    r3["THE ADMINISTRATOR"] = "Leadership style";

                    r3["AVERAGE"] = sumLeadershipAns;
                    rc.TSScoreObtain = sumLeadershipAns;
                    double per3 = Math.Round((sumLeadershipAns * 100) / (tLeadershipAns.Count * 5));
                    r3["PERCENTAGE"] = per3;
                    rc.TSPerformance = per3;
                    dt.Rows.Add(r3);

                    DataRow r4 = dt.NewRow();
                    r4["SR NO."] = "";
                    r4["THE ADMINISTRATOR"] = "Administrator";
                    r4["AVERAGE"] = sumAdministratorAns;
                    rc.SKScoreObtain = sumAdministratorAns;
                    double per4 = Math.Round((sumAdministratorAns * 100) / (tAdministratorAns.Count * 5));
                    r4["PERCENTAGE"] = per4;
                    rc.SKPerformance = per4;
                    dt.Rows.Add(r4);

                    DataRow r5 = dt.NewRow();
                    r5["SR NO."] = "";
                    r5["THE ADMINISTRATOR"] = "Communication";
                    r5["AVERAGE"] = sumCommunicationAns;
                    rc.PIScoreObtain = sumCommunicationAns;
                    double per5 = Math.Round((sumCommunicationAns * 100) / (tCommunicationAns.Count * 5));
                    r5["PERCENTAGE"] = per5;
                    rc.PIPerformance = per5;
                    dt.Rows.Add(r5);

                }
                return rc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable[] TeacherReportCard(string teacherCode, string collageCode, string CurrentSemester, string year = null)
        {
            string getUserQuery = "select * from teacherfeedback where teachercode='" + teacherCode + "' " +
            " and collegecode='" + collageCode + "' and a24='" + year + "' and userid in (select userid from tbluser where currentsemester='" + CurrentSemester + "')";

            if (string.IsNullOrEmpty(year) || year == "undefined")
            {
                getUserQuery = "select * from teacherfeedback where teachercode='" + teacherCode + "' " +
            " and collegecode='" + collageCode + "' and userid in (select userid from tbluser where currentsemester='" + CurrentSemester + "')";
            }

            try
            {
                DataTable enumDt = new DataTable();
                enumDt.Columns.Add("RATING");
                enumDt.Columns.Add("ENUMDESCRIPTION");

                if (collageCode == "101" || collageCode == "103")
                {
                    foreach (FeedbakFormEnum val in Enum.GetValues(typeof(FeedbakFormEnum)))
                    {
                        DataRow r = enumDt.NewRow();
                        r["RATING"] = (int)val;
                        r["ENUMDESCRIPTION"] = val.ToDescriptionString();
                        enumDt.Rows.Add(r);
                    }
                }
                else
                {
                    foreach (CommonFeedbakFormEnum val in Enum.GetValues(typeof(CommonFeedbakFormEnum)))
                    {
                        DataRow r = enumDt.NewRow();
                        r["RATING"] = (int)val;
                        r["ENUMDESCRIPTION"] = val.ToDescriptionString();

                        enumDt.Rows.Add(r);
                    }
                }


                string csvString = string.Empty;
                DataTable dt = new DataTable("TeacherReportCard");
                dt.Columns.Add("SR NO.");
                dt.Columns.Add("THE TEACHER");
                dt.Columns.Add("AVERAGE");
                dt.Columns.Add("PERCENTAGE");
                DataSet dsItems = new DataSet();
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + collageCode + "_FeedbackQuestions.json");
                    string allText = System.IO.File.ReadAllText(filePath);
                    List<FeedbackQuestions> response = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);

                    List<string> tStyleAns = new List<string>() { "a1", "a2", "a4", "a5", "a10", "a11", "a13", "a16" };
                    List<string> tSubKnowAns = new List<string>() { "a3", "a6", "a7" };
                    List<string> tPersonalInterAns = new List<string>() { "a8", "a9", "a12", "a14", "a15", "a17", "a18", "a19", "a20" };

                    double sumStyleAns = 0;
                    double sumSubKnowAns = 0;
                    double sumPersonalInterAns = 0;
                    double total = 0;
                    var ienumlist = dsItems.Tables[0].AsEnumerable();
                    for (int i = 1; i <= response.Count; i++)
                    {
                        if (response[i - 1].Type != "radio")
                            continue;

                        string columnname = "a" + i;
                        DataRow r = dt.NewRow();
                        r["SR NO."] = i;
                        r["THE TEACHER"] = response.FirstOrDefault(a => a.Id.Equals(columnname)).Question.Replace("The teacher", "");
                        double sum = 0;
                        double counter = 0;
                        var dRows = ienumlist.Select(s => s.Field<string>(columnname));//.Select("a" + i);
                        foreach (string dr in dRows)
                        {
                            int sValue = 0;
                            if (string.IsNullOrEmpty(dr))
                                continue;

                            string strValue = dr.Substring(0, 1);

                            switch (strValue)
                            {
                                case "E":
                                    strValue = "5";
                                    break;
                                case "V":
                                    strValue = "4";
                                    break;
                                case "G":
                                    strValue = "3";
                                    break;
                                case "A":
                                    strValue = "2";
                                    break;
                                case "N":
                                    strValue = "1";
                                    break;
                            }

                            bool isParse = int.TryParse(strValue, out sValue);
                            if (isParse)
                            {
                                counter++;
                                sum += sValue;

                            }
                        }
                        if (counter > 0)
                        {
                            double avg = Math.Round(sum / counter, 2);
                            total += avg;
                            r["AVERAGE"] = avg;
                            if (tStyleAns.Contains(columnname))
                            {
                                sumStyleAns += avg;
                            }
                            else if (tSubKnowAns.Contains(columnname))
                            {
                                sumSubKnowAns += avg;
                            }
                            else if (tPersonalInterAns.Contains(columnname))
                            {
                                sumPersonalInterAns += avg;
                            }
                        }
                        else
                        {
                            r["AVERAGE"] = 0;
                        }
                        dt.Rows.Add(r);
                    }

                    ReportCard rc = new ReportCard();
                    rc.TSMaxScore = tStyleAns.Count * 7;
                    rc.SKMaxScore = tSubKnowAns.Count * 7;
                    rc.PIMaxScore = tPersonalInterAns.Count * 7;
                    rc.GrandMaxScore = 7;


                    DataRow r1 = dt.NewRow();
                    dt.Rows.Add(r1);
                    DataRow r2 = dt.NewRow();
                    r2["SR NO."] = "";
                    r2["THE TEACHER"] = "GRAND MEAN";
                    double grandMean = Math.Round(total / 20, 2); // Math.Round(dt.AsEnumerable().Average(r => Convert.ToDouble(r.Field<string>("AVERAGE"))), 2);
                    rc.GrandMean = grandMean;
                    rc.GrandPercentage = Math.Round((grandMean * 100) / 7);
                    r2["AVERAGE"] = grandMean;
                    dt.Rows.Add(r2);

                    enumDt = AddBlanlkRowsToDatatable(2, enumDt);
                    dt = AddBlanlkRowsToDatatable(4, dt);

                    DataRow r3 = dt.NewRow();
                    r3["SR NO."] = "";
                    r3["THE TEACHER"] = "Teaching style";

                    r3["AVERAGE"] = sumStyleAns;
                    rc.TSScoreObtain = sumStyleAns;
                    double per3 = Math.Round((sumStyleAns * 100) / (tStyleAns.Count * 7));
                    r3["PERCENTAGE"] = per3;
                    rc.TSPerformance = per3;
                    dt.Rows.Add(r3);

                    DataRow r4 = dt.NewRow();
                    r4["SR NO."] = "";
                    r4["THE TEACHER"] = "Subject Know";
                    r4["AVERAGE"] = sumSubKnowAns;
                    rc.SKScoreObtain = sumSubKnowAns;
                    double per4 = Math.Round((sumSubKnowAns * 100) / (tSubKnowAns.Count * 7));
                    r4["PERCENTAGE"] = per4;
                    rc.SKPerformance = per4;
                    dt.Rows.Add(r4);

                    DataRow r5 = dt.NewRow();
                    r5["SR NO."] = "";
                    r5["THE TEACHER"] = "Personal Interaction";
                    r5["AVERAGE"] = sumPersonalInterAns;
                    rc.PIScoreObtain = sumPersonalInterAns;
                    double per5 = Math.Round((sumPersonalInterAns * 100) / (tPersonalInterAns.Count * 7));
                    r5["PERCENTAGE"] = per5;
                    rc.PIPerformance = per5;
                    dt.Rows.Add(r5);

                }
                return new DataTable[] { enumDt, dt };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable[] TeacherReportCardMDShah(string teacherCode, string collageCode, string CurrentSemester, string year = null)
        {
            string getUserQuery = "select * from teacherfeedback where teachercode='" + teacherCode + "' " +
            " and collegecode='" + collageCode + "' and a24='" + year + "' and userid in (select userid from tbluser where currentsemester='" + CurrentSemester + "')";

            if (string.IsNullOrEmpty(year))
            {
                getUserQuery = "select * from teacherfeedback where teachercode='" + teacherCode + "' " +
            " and collegecode='" + collageCode + "' and userid in (select userid from tbluser where currentsemester='" + CurrentSemester + "')";
            }

            try
            {
                DataTable enumDt = new DataTable();
                enumDt.Columns.Add("RATING");
                enumDt.Columns.Add("ENUMDESCRIPTION");

                if (collageCode == "101")
                {
                    foreach (FeedbakFormEnum val in Enum.GetValues(typeof(FeedbakFormEnum)))
                    {
                        DataRow r = enumDt.NewRow();
                        r["RATING"] = (int)val;
                        r["ENUMDESCRIPTION"] = val.ToDescriptionString();
                        enumDt.Rows.Add(r);
                    }
                }
                else
                {
                    foreach (CommonFeedbakFormEnum val in Enum.GetValues(typeof(CommonFeedbakFormEnum)))
                    {
                        DataRow r = enumDt.NewRow();
                        r["RATING"] = (int)val;
                        r["ENUMDESCRIPTION"] = val.ToDescriptionString();

                        enumDt.Rows.Add(r);
                    }
                }


                string csvString = string.Empty;
                DataTable dt = new DataTable("TeacherReportCard");
                dt.Columns.Add("SR NO.");
                dt.Columns.Add("THE TEACHER");
                dt.Columns.Add("AVERAGE");
                dt.Columns.Add("PERCENTAGE");
                DataSet dsItems = new DataSet();
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + collageCode + "_FeedbackQuestions.json");
                    string allText = System.IO.File.ReadAllText(filePath);
                    List<FeedbackQuestions> response = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);

                    List<string> tEthicsAns = new List<string>() { "a1", "a2", "a3" };
                    List<string> tStylesAns = new List<string>() { "a4", "a6", "a7", "a8", "a9", "a13", "a14", "a15" };
                    List<string> tStudentOrientedAns = new List<string>() { "a5", "a10", "a11", "a12" };
                    List<string> tOverall = new List<string>() { "a16" };

                    double sumEthicsAns = 0;
                    double sumStylesAns = 0;
                    double sumStudentOrientedAns = 0;
                    double sumOverall = 0;

                    double total = 0;
                    var ienumlist = dsItems.Tables[0].AsEnumerable();
                    for (int i = 1; i <= response.Count; i++)
                    {
                        string columnname = "a" + i;
                        DataRow r = dt.NewRow();
                        r["SR NO."] = i;
                        r["THE TEACHER"] = response.FirstOrDefault(a => a.Id.Equals(columnname)).Question.Replace("The teacher", "");
                        double sum = 0;
                        double counter = 0;
                        var dRows = ienumlist.Select(s => s.Field<string>(columnname));//.Select("a" + i);
                        foreach (string dr in dRows)
                        {
                            int sValue = 0;
                            string strValue = dr.Substring(0, 1);

                            switch (strValue)
                            {
                                case "E":
                                    strValue = "5";
                                    break;
                                case "V":
                                    strValue = "4";
                                    break;
                                case "G":
                                    strValue = "3";
                                    break;
                                case "A":
                                    strValue = "2";
                                    break;
                                case "N":
                                    strValue = "1";
                                    break;
                            }

                            bool isParse = int.TryParse(strValue, out sValue);
                            if (isParse)
                            {
                                counter++;
                                sum += sValue;

                            }
                        }
                        if (counter > 0)
                        {
                            double avg = Math.Round(sum / counter, 2);
                            total += avg;
                            r["AVERAGE"] = avg;
                            if (tEthicsAns.Contains(columnname))
                            {
                                sumEthicsAns += avg;
                            }
                            else if (tStylesAns.Contains(columnname))
                            {
                                sumStylesAns += avg;
                            }
                            else if (tStudentOrientedAns.Contains(columnname))
                            {
                                sumStudentOrientedAns += avg;
                            }
                            else if (tOverall.Contains(columnname))
                            {
                                sumOverall += avg;
                            }
                        }
                        else
                        {
                            r["AVERAGE"] = 0;
                        }
                        dt.Rows.Add(r);
                    }

                    ReportCard rc = new ReportCard();
                    rc.TSMaxScore = tEthicsAns.Count * 5;
                    rc.SKMaxScore = tStylesAns.Count * 5;
                    rc.PIMaxScore = tStudentOrientedAns.Count * 5;
                    rc.OIMaxScore = tOverall.Count * 5;
                    rc.GrandMaxScore = 5;


                    DataRow r1 = dt.NewRow();
                    dt.Rows.Add(r1);
                    DataRow r2 = dt.NewRow();
                    r2["SR NO."] = "";
                    r2["THE TEACHER"] = "GRAND MEAN";
                    double grandMean = Math.Round(total / 16, 2); // Math.Round(dt.AsEnumerable().Average(r => Convert.ToDouble(r.Field<string>("AVERAGE"))), 2);
                    rc.GrandMean = grandMean;
                    rc.GrandPercentage = Math.Round((grandMean * 100) / 5);
                    r2["AVERAGE"] = grandMean;
                    dt.Rows.Add(r2);

                    enumDt = AddBlanlkRowsToDatatable(2, enumDt);
                    dt = AddBlanlkRowsToDatatable(4, dt);

                    DataRow r3 = dt.NewRow();
                    r3["SR NO."] = "";
                    r3["THE TEACHER"] = "Teachers Ethics";

                    r3["AVERAGE"] = sumEthicsAns;
                    rc.TSScoreObtain = sumEthicsAns;
                    double per3 = Math.Round((sumEthicsAns * 100) / (tEthicsAns.Count * 5));
                    r3["PERCENTAGE"] = per3;
                    rc.TSPerformance = per3;
                    dt.Rows.Add(r3);

                    DataRow r4 = dt.NewRow();
                    r4["SR NO."] = "";
                    r4["THE TEACHER"] = "Teaching Style";
                    r4["AVERAGE"] = sumStylesAns;
                    rc.SKScoreObtain = sumStylesAns;
                    double per4 = Math.Round((sumStylesAns * 100) / (tStylesAns.Count * 5));
                    r4["PERCENTAGE"] = per4;
                    rc.SKPerformance = per4;
                    dt.Rows.Add(r4);

                    DataRow r5 = dt.NewRow();
                    r5["SR NO."] = "";
                    r5["THE TEACHER"] = "Student - Oriented approach";
                    r5["AVERAGE"] = sumStudentOrientedAns;
                    rc.PIScoreObtain = sumStudentOrientedAns;
                    double per5 = Math.Round((sumStudentOrientedAns * 100) / (tStudentOrientedAns.Count * 5));
                    r5["PERCENTAGE"] = per5;
                    rc.PIPerformance = per5;
                    dt.Rows.Add(r5);

                    DataRow r6 = dt.NewRow();
                    r6["SR NO."] = "";
                    r6["THE TEACHER"] = "Overall Impression";
                    r6["AVERAGE"] = sumOverall;
                    rc.PIScoreObtain = sumOverall;
                    double per6 = Math.Round((sumOverall * 100) / (tOverall.Count * 5));
                    r6["PERCENTAGE"] = per6;
                    rc.OIPerformance = per6;
                    dt.Rows.Add(r6);

                }
                return new DataTable[] { enumDt, dt };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ReportCard TeacherReportCard2MDShah(string teacherCode, string collageCode, string CurrentSemester, string year = null)
        {
            ReportCard rc = null;
            string getUserQuery = "select * from teacherfeedback where teachercode='" + teacherCode + "' " +
            " and collegecode='" + collageCode + "' and a24='" + year + "' and userid in (select userid from tbluser where currentsemester='" + CurrentSemester + "')";

            if (string.IsNullOrEmpty(year))
                getUserQuery = "select * from teacherfeedback where teachercode='" + teacherCode + "' " +
            " and collegecode='" + collageCode + "' and userid in (select userid from tbluser where currentsemester='" + CurrentSemester + "')";

            try
            {
                DataTable enumDt = new DataTable();
                enumDt.Columns.Add("RATING");
                enumDt.Columns.Add("ENUMDESCRIPTION");
                foreach (FeedbakFormEnum val in Enum.GetValues(typeof(FeedbakFormEnum)))
                {
                    DataRow r = enumDt.NewRow();
                    r["RATING"] = (int)val;
                    r["ENUMDESCRIPTION"] = val.ToDescriptionString(); ;
                    enumDt.Rows.Add(r);
                }

                string csvString = string.Empty;
                DataTable dt = new DataTable("TeacherReportCard");
                dt.Columns.Add("SR NO.");
                dt.Columns.Add("THE TEACHER");
                dt.Columns.Add("AVERAGE");
                dt.Columns.Add("PERCENTAGE");
                DataSet dsItems = new DataSet();
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + collageCode + "_FeedbackQuestions.json");
                    string allText = System.IO.File.ReadAllText(filePath);
                    List<FeedbackQuestions> response = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);

                    List<string> tEthicsAns = new List<string>() { "a1", "a2", "a3" };
                    List<string> tStylesAns = new List<string>() { "a4", "a6", "a7", "a8", "a9", "a13", "a14", "a15" };
                    List<string> tStudentOrientedAns = new List<string>() { "a5", "a10", "a11", "a12" };
                    List<string> tOverall = new List<string>() { "a16" };

                    double sumEthicsAns = 0;
                    double sumStylesAns = 0;
                    double sumStudentOrientedAns = 0;
                    double sumOverallAns = 0;

                    var ienumlist = dsItems.Tables[0].AsEnumerable();
                    for (int i = 1; i <= response.Count; i++)
                    {
                        string columnname = "a" + i;
                        DataRow r = dt.NewRow();
                        r["SR NO."] = i;
                        r["THE TEACHER"] = response.FirstOrDefault(a => a.Id.Equals(columnname)).Question.Replace("The teacher", "");
                        double sum = 0;
                        double counter = 0;
                        var dRows = ienumlist.Select(s => s.Field<string>(columnname));//.Select("a" + i);
                        foreach (string dr in dRows)
                        {
                            int sValue = 0;
                            string strValue = string.IsNullOrEmpty(dr) ? "" : dr.Trim().Substring(0, 1);

                            switch (strValue)
                            {
                                case "E":
                                    strValue = "5";
                                    break;
                                case "V":
                                    strValue = "4";
                                    break;
                                case "G":
                                    strValue = "3";
                                    break;
                                case "A":
                                    strValue = "2";
                                    break;
                                case "N":
                                    strValue = "1";
                                    break;
                            }

                            bool isParse = int.TryParse(strValue, out sValue);
                            if (isParse)
                            {
                                counter++;
                                sum += sValue;
                            }
                        }
                        if (counter > 0)
                        {
                            double avg = Math.Round(sum / counter, 2);
                            r["AVERAGE"] = avg;
                            if (tEthicsAns.Contains(columnname))
                            {
                                sumEthicsAns += avg;
                            }
                            else if (tStylesAns.Contains(columnname))
                            {
                                sumStylesAns += avg;
                            }
                            else if (tStudentOrientedAns.Contains(columnname))
                            {
                                sumStudentOrientedAns += avg;
                            }
                            else if (tOverall.Contains(columnname))
                            {
                                sumOverallAns += avg;
                            }
                        }
                        else
                        {
                            r["AVERAGE"] = 0;
                        }
                        dt.Rows.Add(r);
                    }

                    rc = new ReportCard();
                    rc.TSMaxScore = tEthicsAns.Count * 5;
                    rc.SKMaxScore = tStylesAns.Count * 5;
                    rc.PIMaxScore = tStudentOrientedAns.Count * 5;
                    rc.OIMaxScore = tOverall.Count * 5;
                    rc.GrandMaxScore = 5;

                    DataRow r1 = dt.NewRow();
                    dt.Rows.Add(r1);
                    DataRow r2 = dt.NewRow();
                    r2["SR NO."] = "";
                    r2["THE TEACHER"] = "GRAND MEAN";
                    double grandMean = Math.Round(dt.AsEnumerable().Sum(r => Convert.ToDouble(r.Field<string>("AVERAGE"))), 2);
                    grandMean = grandMean / (dt.Rows.Count - 1);
                    rc.GrandMean = grandMean;
                    rc.GrandPercentage = Math.Round((grandMean * 100) / 5);
                    r2["AVERAGE"] = grandMean;
                    dt.Rows.Add(r2);

                    enumDt = AddBlanlkRowsToDatatable(2, enumDt);
                    dt = AddBlanlkRowsToDatatable(4, dt);

                    DataRow r3 = dt.NewRow();
                    r3["SR NO."] = "";
                    r3["THE TEACHER"] = "Teachers Ethics";

                    r3["AVERAGE"] = sumEthicsAns;
                    rc.TSScoreObtain = sumEthicsAns;
                    double per3 = Math.Round((sumEthicsAns * 100) / (tEthicsAns.Count * 5));
                    r3["PERCENTAGE"] = per3;
                    rc.TSPerformance = per3;
                    dt.Rows.Add(r3);

                    DataRow r4 = dt.NewRow();
                    r4["SR NO."] = "";
                    r4["THE TEACHER"] = "Teaching Style";
                    r4["AVERAGE"] = sumStylesAns;
                    rc.SKScoreObtain = sumStylesAns;
                    double per4 = Math.Round((sumStylesAns * 100) / (tStylesAns.Count * 5));
                    r4["PERCENTAGE"] = per4;
                    rc.SKPerformance = per4;
                    dt.Rows.Add(r4);

                    DataRow r5 = dt.NewRow();
                    r5["SR NO."] = "";
                    r5["THE TEACHER"] = "Student - Oriented approach";
                    r5["AVERAGE"] = sumStudentOrientedAns;
                    rc.PIScoreObtain = sumStudentOrientedAns;
                    double per5 = Math.Round((sumStudentOrientedAns * 100) / (tStudentOrientedAns.Count * 5));
                    r5["PERCENTAGE"] = per5;
                    rc.PIPerformance = per5;
                    dt.Rows.Add(r5);

                    DataRow r6 = dt.NewRow();
                    r6["SR NO."] = "";
                    r6["THE TEACHER"] = "Overall Impression";
                    r6["AVERAGE"] = sumOverallAns;
                    rc.OIScoreObtain = sumOverallAns;
                    double per6 = Math.Round((sumOverallAns * 100) / (tOverall.Count * 5));
                    r6["PERCENTAGE"] = per6;
                    rc.OIPerformance = per6;
                    dt.Rows.Add(r6);

                }
                return rc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ReportCard TeacherReportCard2(string teacherCode, string collageCode, string CurrentSemester, string year = null)
        {
            ReportCard rc = null;
            string getUserQuery = "select * from teacherfeedback where teachercode='" + teacherCode + "' " +
            " and collegecode='" + collageCode + "' and a24='" + year + "' and userid in (select userid from tbluser where currentsemester='" + CurrentSemester + "')";

            if (string.IsNullOrEmpty(year))
                getUserQuery = "select * from teacherfeedback where teachercode='" + teacherCode + "' " +
            " and collegecode='" + collageCode + "' and userid in (select userid from tbluser where currentsemester='" + CurrentSemester + "')";

            try
            {
                DataTable enumDt = new DataTable();
                enumDt.Columns.Add("RATING");
                enumDt.Columns.Add("ENUMDESCRIPTION");
                foreach (FeedbakFormEnum val in Enum.GetValues(typeof(FeedbakFormEnum)))
                {
                    DataRow r = enumDt.NewRow();
                    r["RATING"] = (int)val;
                    r["ENUMDESCRIPTION"] = val.ToDescriptionString(); ;
                    enumDt.Rows.Add(r);
                }

                string csvString = string.Empty;
                DataTable dt = new DataTable("TeacherReportCard");
                dt.Columns.Add("SR NO.");
                dt.Columns.Add("THE TEACHER");
                dt.Columns.Add("AVERAGE");
                dt.Columns.Add("PERCENTAGE");
                DataSet dsItems = new DataSet();
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + collageCode + "_FeedbackQuestions.json");
                    string allText = System.IO.File.ReadAllText(filePath);
                    List<FeedbackQuestions> response = JsonConvert.DeserializeObject<List<FeedbackQuestions>>(allText);

                    List<string> tStyleAns = new List<string>() { "a1", "a2", "a4", "a5", "a10", "a11", "a13", "a16" };
                    List<string> tSubKnowAns = new List<string>() { "a3", "a6", "a7" };
                    List<string> tPersonalInterAns = new List<string>() { "a8", "a9", "a12", "a14", "a15", "a17", "a18", "a19", "a20" };

                    double sumStyleAns = 0;
                    double sumSubKnowAns = 0;
                    double sumPersonalInterAns = 0;

                    var ienumlist = dsItems.Tables[0].AsEnumerable();
                    for (int i = 1; i <= response.Count; i++)
                    {
                        string columnname = "a" + i;
                        DataRow r = dt.NewRow();
                        r["SR NO."] = i;
                        r["THE TEACHER"] = response.FirstOrDefault(a => a.Id.Equals(columnname)).Question.Replace("The teacher", "");
                        double sum = 0;
                        double counter = 0;
                        var dRows = ienumlist.Select(s => s.Field<string>(columnname));//.Select("a" + i);
                        foreach (string dr in dRows)
                        {
                            int sValue = 0;
                            string strValue = string.IsNullOrEmpty(dr) ? "" : dr.Substring(0, 1);

                            switch (strValue)
                            {
                                case "E":
                                    strValue = "5";
                                    break;
                                case "V":
                                    strValue = "4";
                                    break;
                                case "G":
                                    strValue = "3";
                                    break;
                                case "A":
                                    strValue = "2";
                                    break;
                                case "N":
                                    strValue = "1";
                                    break;
                            }

                            bool isParse = int.TryParse(strValue, out sValue);
                            if (isParse)
                            {
                                counter++;
                                sum += sValue;
                            }
                        }
                        if (counter > 0)
                        {
                            double avg = Math.Round(sum / counter, 2);
                            r["AVERAGE"] = avg;
                            if (tStyleAns.Contains(columnname))
                            {
                                sumStyleAns += avg;
                            }
                            else if (tSubKnowAns.Contains(columnname))
                            {
                                sumSubKnowAns += avg;
                            }
                            else if (tPersonalInterAns.Contains(columnname))
                            {
                                sumPersonalInterAns += avg;
                            }
                        }
                        else
                        {
                            r["AVERAGE"] = 0;
                        }
                        dt.Rows.Add(r);
                    }

                    rc = new ReportCard();
                    int factor = 7;
                    if (collageCode != "101" && collageCode != "103")
                    {
                        factor = 5;
                    }

                    rc.TSMaxScore = tStyleAns.Count * factor;
                    rc.SKMaxScore = tSubKnowAns.Count * factor;
                    rc.PIMaxScore = tPersonalInterAns.Count * factor;
                    rc.GrandMaxScore = factor;

                    DataRow r1 = dt.NewRow();
                    dt.Rows.Add(r1);
                    DataRow r2 = dt.NewRow();
                    r2["SR NO."] = "";
                    r2["THE TEACHER"] = "GRAND MEAN";
                    double grandMean = Math.Round(dt.AsEnumerable().Average(r => Convert.ToDouble(r.Field<string>("AVERAGE"))), 2);
                    rc.GrandMean = grandMean;
                    rc.GrandPercentage = Math.Round((grandMean * 100) / factor);
                    r2["AVERAGE"] = grandMean;
                    dt.Rows.Add(r2);

                    enumDt = AddBlanlkRowsToDatatable(2, enumDt);
                    dt = AddBlanlkRowsToDatatable(4, dt);

                    DataRow r3 = dt.NewRow();
                    r3["SR NO."] = "";
                    r3["THE TEACHER"] = "Teaching style";

                    r3["AVERAGE"] = sumStyleAns;
                    rc.TSScoreObtain = sumStyleAns;
                    double per3 = Math.Round((sumStyleAns * 100) / (tStyleAns.Count * factor));
                    r3["PERCENTAGE"] = per3;
                    rc.TSPerformance = per3;
                    dt.Rows.Add(r3);

                    DataRow r4 = dt.NewRow();
                    r4["SR NO."] = "";
                    r4["THE TEACHER"] = "Subject Know";
                    r4["AVERAGE"] = sumSubKnowAns;
                    rc.SKScoreObtain = sumSubKnowAns;
                    double per4 = Math.Round((sumSubKnowAns * 100) / (tSubKnowAns.Count * factor));
                    r4["PERCENTAGE"] = per4;
                    rc.SKPerformance = per4;
                    dt.Rows.Add(r4);

                    DataRow r5 = dt.NewRow();
                    r5["SR NO."] = "";
                    r5["THE TEACHER"] = "Personal Interaction";
                    r5["AVERAGE"] = sumPersonalInterAns;
                    rc.PIScoreObtain = sumPersonalInterAns;
                    double per5 = Math.Round((sumPersonalInterAns * 100) / (tPersonalInterAns.Count * factor));
                    r5["PERCENTAGE"] = per5;
                    rc.PIPerformance = per5;
                    dt.Rows.Add(r5);

                }
                return rc;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        DataTable AddBlanlkRowsToDatatable(int noOfRow, DataTable dt)
        {
            for (int i = 0; i < noOfRow; i++)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr);
            }
            return dt;
        }

        public string ExportNotInFeedbackFinalYearUser(string Subject)
        {
            string getUserQuery = "";
            if (string.IsNullOrEmpty(Subject))
            {
                getUserQuery = "select userid,firstname,lastname,course,subcourse,isfinalyearstudent from tbluser where userid not in(SELECT userid FROM ) and isfinalyearstudent in('true','1')";
            }
            else
            {
                getUserQuery = "select userid,firstname,lastname,course,subcourse,isfinalyearstudent from tbluser where userid not in(SELECT userid FROM teacherfeedback) and isfinalyearstudent in('true','1') and course = '" + Subject + "'";
            }
            try
            {
                string csvString = string.Empty;
                DataTable dt = new DataTable("ExportUser");
                dt.Columns.Add("User Id");
                dt.Columns.Add("First Name");
                dt.Columns.Add("Last Name");
                dt.Columns.Add("Course");
                dt.Columns.Add("Sub Course");
                DataSet dsItems = new DataSet();
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        DataRow r = dt.NewRow();
                        r["User Id"] = row["userid"].ToString();
                        r["First Name"] = row["firstname"].ToString();
                        r["Last Name"] = row["lastname"].ToString();
                        r["Course"] = row["course"].ToString();
                        r["Sub Course"] = row["subcourse"].ToString();
                        dt.Rows.Add(r);
                    }
                    csvString = ExportDataTableToCSV(dt);
                    return csvString;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetMissingStudentFeedbackData(string collageCode)
        {
            string getUserQuery = "";
            string csvString = string.Empty;
            DataTable dt = new DataTable("ExportUser");
            dt.Columns.Add("User Id");
            dt.Columns.Add("Last Name");
            dt.Columns.Add("First Name");
            dt.Columns.Add("Course");
            dt.Columns.Add("Sub Course");
            dt.Columns.Add("Teacher Code");
            dt.Columns.Add("Teacher Name");
            dt.Columns.Add("Subject Code");
            dt.Columns.Add("Subject Name");

            getUserQuery = "select UserId,LastName,FirstName,course,subcourse from tbluser order by course";
            try
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/UserData/teachers_" + collageCode + ".json");
                string allText = System.IO.File.ReadAllText(filePath);
                List<TeacherDetails> teacherDetails = JsonConvert.DeserializeObject<List<TeacherDetails>>(allText);


                DataSet dsItems = new DataSet();
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        DataSet dsItems2 = new DataSet();
                        List<TeacherDetails> td = teacherDetails.Where(a => a.Course == Convert.ToString(row["course"]) && a.SubCourse == Convert.ToString(row["subcourse"])).ToList();

                        string getFeedbackQuery = "SELECT teacherCode,subjectcode FROM teacherfeedback where userid='" + row["userid"] + "'";
                        dsItems2 = GetQuery(getFeedbackQuery);
                        if (dsItems2.Tables != null && dsItems2.Tables.Count > 0 && dsItems2.Tables[0].Rows != null && dsItems2.Tables[0].Rows.Count > 0)
                        {
                            foreach (TeacherDetails t in td)
                            {
                                DataRow[] dRow = dsItems2.Tables[0].Select("teacherCode = '" + t.TeacherCode + "' AND subjectcode ='" + t.SubjectCode + "'");
                                if (dRow == null || dRow.Length == 0)
                                {
                                    DataRow r = dt.NewRow();
                                    r["User Id"] = row["userid"].ToString();
                                    r["First Name"] = row["firstname"].ToString();
                                    r["Last Name"] = row["lastname"].ToString();
                                    r["Course"] = row["course"].ToString();
                                    r["Sub Course"] = row["subcourse"].ToString();
                                    r["Teacher Code"] = t.TeacherCode;
                                    r["Teacher Name"] = t.TeacherName;
                                    r["Subject Code"] = t.SubjectCode;
                                    r["Subject Name"] = t.SubjectName;
                                    dt.Rows.Add(r);
                                }

                            }
                        }
                        else
                        {
                            foreach (TeacherDetails t in td)
                            {
                                DataRow r = dt.NewRow();
                                r["User Id"] = row["userid"].ToString();
                                r["First Name"] = row["firstname"].ToString();
                                r["Last Name"] = row["lastname"].ToString();
                                r["Course"] = row["course"].ToString();
                                r["Sub Course"] = row["subcourse"].ToString();
                                r["Teacher Code"] = t.TeacherCode;
                                r["Teacher Name"] = t.TeacherName;
                                r["Subject Code"] = t.SubjectCode;
                                r["Subject Name"] = t.SubjectName;
                                dt.Rows.Add(r);
                            }
                        }
                    }
                    csvString = ExportDataTableToCSV(dt);
                    return csvString;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<User> GetMissingStudentFeedbackData(string collageCode, string course, string sem)
        {
            List<User> users = new List<User>();
            string getUserQuery = "";
            string csvString = string.Empty;
            getUserQuery = "select * from tbluser where collegecode='" + collageCode + "' and course='" + course + "' and currentsemester='" + sem + "' " +
           " and userid not in (select userid from teacherfeedback where collegecode='" + collageCode + "' group by userid);";
            try
            {

                DataSet dsItems = new DataSet();
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        User userDetails = new User();
                        userDetails.UserId = int.Parse(row["userid"].ToString());
                        userDetails.FirstName = row["firstname"].ToString();
                        userDetails.LastName = row["lastname"].ToString();
                        userDetails.MobileNumber = row["mobilenumber"].ToString();
                        userDetails.CollegeCode = row["collegecode"].ToString();
                        userDetails.Course = row["course"].ToString();
                        userDetails.SubCourse = row["subcourse"].ToString();
                        userDetails.IsActive = row["isactive"].ToString();
                        userDetails.IsFinalYearStudent = row["isfinalyearstudent"].ToString();
                        userDetails.CurrentSemester = row["currentsemester"].ToString();
                        users.Add(userDetails);
                    }
                    return users;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<User> GetMissingFeedbackTeacherWise(string collageCode, string course, string sem)
        {
            List<User> users = new List<User>();
            string getUserQuery = "";
            string csvString = string.Empty;
            getUserQuery = "select * from tbluser where collegecode='" + collageCode + "' and course='" + course + "' and currentsemester='" + sem + "' " +
                      " and userid in (select userid from teacherfeedback where collegecode='" + collageCode + "' group by userid);";

            try
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/UserData/teachers_" + collageCode + ".json");
                string allText = System.IO.File.ReadAllText(filePath);
                List<TeacherDetails> teacherDetails = JsonConvert.DeserializeObject<List<TeacherDetails>>(allText);

                DataSet dsItems = new DataSet();
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        User userDetails = new User();
                        List<TeacherDetails> td = teacherDetails.Where(a => a.Course == Convert.ToString(row["course"]) && a.SubCourse == Convert.ToString(row["subcourse"]) && a.Semester.ToLower() == Convert.ToString(row["currentsemester"]).ToLower()).ToList();
                        DataSet dsItems2 = new DataSet();

                        string getFeedbackQuery = "SELECT teacherCode,subjectcode FROM teacherfeedback where userid='" + row["userid"] + "'";
                        dsItems2 = GetQuery(getFeedbackQuery);
                        if (dsItems2.Tables != null && dsItems2.Tables.Count > 0 && dsItems2.Tables[0].Rows != null && dsItems2.Tables[0].Rows.Count > 0)
                        {
                            foreach (TeacherDetails t in td)
                            {
                                DataRow[] dRow = dsItems2.Tables[0].Select("teacherCode = '" + t.TeacherCode + "' AND subjectcode ='" + t.SubjectCode + "'");
                                if (dRow == null || dRow.Length == 0)
                                {
                                    userDetails.TeacherCodes.Add(t.TeacherCode);
                                    userDetails.TeacherNames.Add(t.TeacherName);
                                    userDetails.SubjectCodes.Add(t.SubjectCode);
                                    userDetails.SubjectNames.Add(t.SubjectName);
                                }
                            }
                        }
                        if (userDetails.TeacherCodes.Count > 0)
                        {
                            userDetails.UserId = int.Parse(row["userid"].ToString());
                            userDetails.FirstName = row["firstname"].ToString();
                            userDetails.LastName = row["lastname"].ToString();
                            userDetails.MobileNumber = row["mobilenumber"].ToString();
                            userDetails.CollegeCode = row["collegecode"].ToString();
                            userDetails.Course = row["course"].ToString();
                            userDetails.SubCourse = row["subcourse"].ToString();
                            userDetails.IsActive = row["isactive"].ToString();
                            userDetails.IsFinalYearStudent = row["isfinalyearstudent"].ToString();
                            userDetails.CurrentSemester = row["currentsemester"].ToString();
                            users.Add(userDetails);
                        }
                    }
                    return users;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet GetStaffFeedback(int CollegeCode, string ReportType, string DepartmentName = null, string academicYear = null, string subjectName = null)
        {
            string getDepartmentalQuery = "SELECT * FROM commonfeedback where usertype='" + ReportType + "' and collegecode=" + CollegeCode;

            if (!string.IsNullOrEmpty(DepartmentName) && ReportType.ToLower() == "curriculumevaluation")
            {
                getDepartmentalQuery = "SELECT * FROM commonfeedback where usertype = '" + ReportType + "' and collegecode=" + CollegeCode + " and a1='" + DepartmentName + "' and a2='" + subjectName + "' and a14='" + academicYear + "'";
            }
            else if (!string.IsNullOrEmpty(DepartmentName) && ReportType.ToLower() != "peeralldepartment")
            {
                getDepartmentalQuery = "SELECT * FROM commonfeedback where usertype = '" + ReportType + "' and collegecode=" + CollegeCode + " and a16='" + DepartmentName + "'";
            }
            else if (ReportType.ToLower() == "peeralldepartment")
            {
                getDepartmentalQuery = "SELECT * FROM commonfeedback where usertype in ('PeerOwnDepartment','PeerOtherDepartment','PeerAnyDepartment') and collegecode=" + CollegeCode;
            }

            DataTable ndt = new DataTable();
            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();
                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    ndt = dsItems.Tables[0];
                }
                return dsItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetFeedbackResponseByUserAndCollege(int CollegeCode, string ReportType, string tableName, string adminName, string academicYear)
        {
            string getDepartmentalQuery = "SELECT * FROM " + tableName + " where usertype='" + ReportType + "' and collegecode=" + CollegeCode + " and a32='" + academicYear + "'";
            if (!string.IsNullOrEmpty(adminName))
            {
                getDepartmentalQuery = "SELECT * FROM " + tableName + " where usertype='" + ReportType + "' and collegecode=" + CollegeCode + " and a1='" + adminName + "' and a32='" + academicYear + "'";
            }

            DataTable ndt = new DataTable();
            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();
                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    ndt = dsItems.Tables[0];
                }
                return dsItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet GetTeacherFeedbackByTeacher(string teacherCode, string CurrentSemester, int CollegeCode, string year = null)
        {
            string getDepartmentalQuery = "select ts.currentsemester,tf.* from teacherfeedback as tf INNER JOIN tbluser as ts ON tf.userid=ts.userid where tf.a24='" + year + "' and tf.teachercode='" + teacherCode + "' and ts.currentsemester='" + CurrentSemester + "' and tf.collegecode=" + CollegeCode;

            if (string.IsNullOrEmpty(year))
                getDepartmentalQuery = "select ts.currentsemester,tf.* from teacherfeedback as tf INNER JOIN tbluser as ts ON tf.userid=ts.userid where tf.teachercode='" + teacherCode + "' and ts.currentsemester='" + CurrentSemester + "' and tf.collegecode=" + CollegeCode;

            DataTable ndt = new DataTable();
            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();
                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    ndt = dsItems.Tables[0];
                }
                return dsItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ClsActivities GetClsActivityData(int formId)
        {
            string getUserQuery = "SELECT * FROM clsactivities where clsaluminiid=" + formId;
            DataTable table = null;
            DataSet dsItems = new DataSet();
            string csvString = string.Empty;
            ClsActivities a2 = new ClsActivities();
            try
            {
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables != null && dsItems.Tables.Count > 0)
                    table = dsItems.Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                    DataRow row = table.Rows[0];
                    a2.ClsActivitiesId = (int)row["clsactivitiesid"];
                    a2.A1 = Convert.ToString(row["a1"]);
                    a2.A2 = Convert.ToString(row["a2"]);
                    a2.A3 = Convert.ToString(row["a3"]);
                    a2.A4 = Convert.ToString(row["a4"]);
                    a2.A5 = Convert.ToString(row["a5"]);
                    a2.A6 = Convert.ToString(row["a6"]);
                    a2.A7 = Convert.ToString(row["a7"]);
                    a2.A8 = Convert.ToString(row["a8"]);
                    a2.A9 = Convert.ToString(row["a9"]);
                    a2.A10 = Convert.ToString(row["a10"]);
                    a2.A11 = Convert.ToString(row["a11"]);
                    a2.A12 = Convert.ToString(row["a12"]);
                    a2.A13 = Convert.ToString(row["a13"]);
                    a2.A14 = Convert.ToString(row["a14"]);
                    a2.A15 = Convert.ToString(row["a15"]);
                    a2.A16 = Convert.ToString(row["a16"]);
                    a2.A17 = Convert.ToString(row["a17"]);
                    a2.A18 = Convert.ToString(row["a18"]);
                    a2.A19 = Convert.ToString(row["a19"]);
                    a2.A20 = Convert.ToString(row["a20"]);
                    a2.A21 = Convert.ToString(row["a21"]);
                    a2.A22 = Convert.ToString(row["a22"]);
                    a2.A23 = Convert.ToString(row["a23"]);
                    a2.A24 = Convert.ToString(row["a24"]);
                    a2.A25 = Convert.ToString(row["a25"]);
                }
                return a2;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<ClsProfessional> GetClsProfessional(int formId)
        {
            string getUserQuery = "SELECT * FROM clsprofessional where clsaluminiid=" + formId;
            DataTable table = null;
            DataSet dsItems = new DataSet();
            string csvString = string.Empty;
            try
            {
                dsItems = GetQuery(getUserQuery);
                if (dsItems.Tables != null && dsItems.Tables.Count > 0)
                    table = dsItems.Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                    List<ClsProfessional> a2List = new List<ClsProfessional>();
                    foreach (DataRow row in table.Rows)
                    {
                        ClsProfessional a2 = new ClsProfessional();
                        a2.ClsProfessionalId = (int)row["clsprofessionalid"];
                        a2.A1 = (string)row["a1"];
                        a2.A2 = (string)row["a2"];
                        a2.A3 = (string)row["a3"];
                        a2.A4 = (string)row["a4"];
                        a2.A5 = (string)row["a5"];
                        a2.A6 = (string)row["a6"];
                        a2.A7 = (string)row["a7"];
                        a2.A8 = (string)row["a8"];
                        a2.A9 = (string)row["a9"];
                        a2List.Add(a2);
                    }
                    return a2List;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        public DataSet GetclsActivityData(int CollegeCode)
        {
            string getDepartmentalQuery = "select cal.userid,cal.collegecode,ca.* from  clsactivities as ca INNER JOIN clsalumini as cal ON ca.clsaluminiid=cal.clsaluminiid where cal.collegecode=" + CollegeCode;
            DataTable ndt = new DataTable();
            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();
                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    ndt = dsItems.Tables[0];
                }
                return dsItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetExitFormReport(int CollegeCode, string TabType)
        {
            string getDepartmentalQuery = GetSqlQuery(TabType, CollegeCode);
            DataTable ndt = new DataTable();
            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();
                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    ndt = dsItems.Tables[0];
                }
                return dsItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetSqlQuery(string ReportType, int collegeCode)
        {
            if (ReportType.ToLower() == "a2tab")
            {
                return "SELECT *,tbluser.collegecode FROM a2 INNER JOIN exitform ON a2.formid = exitform.formid INNER JOIN tbluser ON exitform.userid = tbluser.userid where collegecode=" + collegeCode;
            }
            else if (ReportType.ToLower() == "a3tab")
            {
                return "SELECT *,tbluser.collegecode FROM a9 INNER JOIN exitform ON a9.formid = exitform.formid INNER JOIN tbluser ON exitform.userid = tbluser.userid where collegecode=" + collegeCode;
            }
            else if (ReportType.ToLower() == "exitformtab")
            {
                return "select *,tu.collegecode from exitform as ef INNER JOIN tbluser tu on ef.userid=tu.userid where collegecode=" + collegeCode;
            }
            else
            {
                return string.Empty;
            }
        }

        public DataSet GetclsAlumniData(int CollegeCode)
        {
            string getDepartmentalQuery = "select * from clsalumini where collegecode=" + CollegeCode;
            DataTable ndt = new DataTable();
            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();
                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    ndt = dsItems.Tables[0];
                }
                return dsItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet GetclsRatingData(int CollegeCode)
        {
            string getDepartmentalQuery = "select cal.userid,cal.collegecode,ca.* from  clsrating as ca INNER JOIN clsalumini as cal ON ca.clsaluminiid=cal.clsaluminiid where cal.collegecode=" + CollegeCode;
            DataTable ndt = new DataTable();
            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();
                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    ndt = dsItems.Tables[0];
                }
                return dsItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetSubmissionId(string submissionId)
        {
            try
            {
                int id = 0;
                string query = string.Format("select userid from tbluser where submissionid='{0}'", submissionId);

                using (con = GetConnection())
                {
                    cmd.CommandText = query;
                    string strID = Convert.ToString(cmd.ExecuteScalar());
                    if (!string.IsNullOrEmpty(strID))
                    {
                        id = (int)Convert.ToInt32(strID);
                    }
                }

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetUserIdByUserType(string collegeCode, string userType)
        {
            try
            {
                string query = string.Format("select userid from tbluser where collegecode='{0}' and  roletype='{1}'", collegeCode, userType);
                int id = 0;

                using (con = GetConnection())
                {
                    cmd.CommandText = query;
                    string strID = Convert.ToString(cmd.ExecuteScalar());
                    if (!string.IsNullOrEmpty(strID))
                    {
                        id = (int)Convert.ToInt32(strID);
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetPeerViewPeopleList(string collegeCode)
        {
            string query = string.Format("SELECT distinct a23 FROM commonfeedback c where usertype in ('PeerOwnDepartment','PeerOtherDepartment','PeerAnyDepartment') and collegecode={0}", collegeCode);
            DataTable table = new DataTable();
            List<string> people = new List<string>();
            try
            {
                DataSet dsItems = new DataSet();
                dsItems = GetQuery(query);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    table = dsItems.Tables[0];
                    foreach (DataRow row in table.Rows)
                    {
                        people.Add(row["a23"].ToString());
                    }
                }

                return people;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetPeerViewPeopleList(string collegeCode, string department)
        {
            string query = string.Format("SELECT distinct a2 FROM commonfeedback where collegecode='{0}' and a1 like '{1}%' and usertype = 'CurriculumEvaluation';", collegeCode, department);
            DataTable table = new DataTable();
            List<string> subjects = new List<string>();
            try
            {
                DataSet dsItems = new DataSet();
                dsItems = GetQuery(query);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    table = dsItems.Tables[0];
                    foreach (DataRow row in table.Rows)
                    {
                        subjects.Add(row["a2"].ToString());
                    }
                }

                return subjects;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PeerReview GetPeerReviewOwnDepartmentList(string collegeCode, string personCode)
        {

            string exQuestionResponseQuery = "SELECT count(a{0}) FROM commonfeedback where usertype = '{1}' and a{0}='Excellent' and a23='{2}'";
            string vgQuestionResponseQuery = "SELECT count(a{0}) FROM commonfeedback where usertype = '{1}' and a{0}='Very Good' and a23='{2}'";
            string gQuestionResponseQuery = "SELECT count(a{0}) FROM commonfeedback where usertype = '{1}' and a{0}='Good' and a23='{2}'";
            string aQuestionResponseQuery = "SELECT count(a{0}) FROM commonfeedback where usertype = '{1}' and a{0}='Average' and a23='{2}'";
            string nQuestionResponseQuery = "SELECT count(a{0}) FROM commonfeedback where usertype = '{1}' and a{0}='Needs to Improve' and a23='{2}'";

            PeerReview peerReview = new PeerReview();
            peerReview.Questions = new Dictionary<string, PeerReviewAnalysis>();
            int totalQuestions = 0;
            int excellent = 0;
            int veryGood = 0;
            int good = 0;
            int average = 0;
            int needsToImprove = 0;
            // Get the total count
            using (con = GetConnection())
            {

                for (int j = 1; j < 4; j++)
                {
                    string reviewType = j == 1 ? "PeerOwnDepartment" : (j == 2) ? "PeerOtherDepartment" : "PeerAnyDepartment";
                    if (!peerReview.Questions.ContainsKey(reviewType))
                    {
                        peerReview.Questions.Add(reviewType, new PeerReviewAnalysis());
                    }

                    string totalQuery = "SELECT count(a1) Total FROM commonfeedback where collegecode='{0}' and a23='{1}' and usertype = '{2}'";
                    totalQuery = string.Format(totalQuery, collegeCode, personCode, reviewType);
                    cmd.CommandText = totalQuery;
                    string strID = Convert.ToString(cmd.ExecuteScalar());
                    if (!string.IsNullOrEmpty(strID))
                    {
                        totalQuestions = (int)Convert.ToInt32(strID);
                    }

                    for (int i = 1; i < 18; i++)
                    {
                        cmd.CommandText = string.Format(exQuestionResponseQuery, i, reviewType, personCode);
                        string counter = Convert.ToString(cmd.ExecuteScalar());
                        PeerReviewAnalysis analysis = peerReview.Questions[reviewType];
                        if (analysis.Percentage == null)
                        {
                            analysis.Percentage = new Dictionary<string, Analysis>();
                        }

                        if (!analysis.Percentage.ContainsKey("a" + i))
                        {
                            analysis.Percentage.Add("a" + i, new Analysis());
                        }

                        Analysis ana = analysis.Percentage["a" + i];

                        if (!string.IsNullOrEmpty(counter))
                        {
                            excellent = (int)Convert.ToInt32(counter);
                        }

                        ana.Excellent = excellent.ToString();

                        cmd.CommandText = string.Format(vgQuestionResponseQuery, i, reviewType, personCode);
                        counter = Convert.ToString(cmd.ExecuteScalar());
                        if (!string.IsNullOrEmpty(counter))
                        {
                            veryGood = (int)Convert.ToInt32(counter);
                        }

                        ana.VeryGood = veryGood.ToString();

                        cmd.CommandText = string.Format(gQuestionResponseQuery, i, reviewType, personCode);
                        counter = Convert.ToString(cmd.ExecuteScalar());
                        if (!string.IsNullOrEmpty(counter))
                        {
                            good = (int)Convert.ToInt32(counter);
                        }

                        ana.Good = good.ToString();

                        cmd.CommandText = string.Format(aQuestionResponseQuery, i, reviewType, personCode);
                        counter = Convert.ToString(cmd.ExecuteScalar());
                        if (!string.IsNullOrEmpty(counter))
                        {
                            average = (int)Convert.ToInt32(counter);
                        }

                        ana.Average = average.ToString();

                        cmd.CommandText = string.Format(nQuestionResponseQuery, i, reviewType, personCode);
                        counter = Convert.ToString(cmd.ExecuteScalar());
                        if (!string.IsNullOrEmpty(counter))
                        {
                            needsToImprove = (int)Convert.ToInt32(counter);
                        }

                        ana.NeedsToImprove = needsToImprove.ToString();
                        ana.Total = totalQuestions.ToString();
                        ana.TotalPoints = (int.Parse(ana.Excellent) * 5 + int.Parse(ana.VeryGood) * 4 + int.Parse(ana.Good) * 3 + int.Parse(ana.Average) * 2 + int.Parse(ana.NeedsToImprove) * 1).ToString();

                        if (ana.Total != "0")
                        {
                            ana.Grade = ((float.Parse(ana.TotalPoints) / float.Parse(ana.Total)) / 5 * 100).ToString();
                        }
                        else
                        {
                            ana.Grade = "0";
                        }

                    }
                }
            }

            return peerReview;
        }
    }
}