using SVT.Business.Model;
using SVT.DataContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Web.Http;
using System.Web;

namespace SVT.Business.Services
{
    /// <summary>
    /// This class is used to Define Database object for Save, Search, Delete
    /// </summary>
    /// <CreatedBy>Kaushik Patel</CreatedBy>
    /// <CreatedDate>06-Aug-2015</CreatedDate>
    public sealed class ServiceContext : DBContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ServiceContext class with database name.
        /// </summary>
        /// <param name="databaseName">database Name value</param>
        /// <param name="isFromWindowService">Is from window service</param>
        public ServiceContext()
        {
            this.PagingInformation = new Pagination() { PageSize = DefaultPageSize, PagerSize = DefaultPagerSize };
            this.CheckForDuplicate = false;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceContext class for checking duplicate value for one column
        /// </summary>
        /// <param name="col1Name">column Name</param>
        public ServiceContext(string col1Name)
        {
            this.PagingInformation = new Pagination() { PageSize = DefaultPageSize, PagerSize = DefaultPagerSize };
            this.CheckForDuplicate = true;
            this.Col1Name = col1Name;
            this.CombinationCheckRequired = false;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceContext class for checking duplicate value for two column with combination
        /// </summary>
        /// <param name="col1Name">first column name</param>
        /// <param name="col2Name">second column name</param>
        public ServiceContext(string col1Name, string col2Name)
        {
            this.PagingInformation = new Pagination() { PageSize = DefaultPageSize, PagerSize = DefaultPagerSize };

            this.CheckForDuplicate = true;
            this.Col1Name = col1Name;
            this.Col2Name = col2Name;
            this.CombinationCheckRequired = true;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceContext class for checking duplicate value for two column with combination
        /// </summary>
        /// <param name="col1Name">first column name</param>
        /// <param name="col2Name">second column name</param>
        /// <param name="col3Name">third column name</param>
        public ServiceContext(string col1Name, string col2Name, string col3Name)
        {
            this.CheckForDuplicate = true;
            this.Col1Name = col1Name;
            this.Col2Name = col2Name;
            this.Col3Name = col3Name;
            this.CombinationCheckRequired = true;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceContext class for checking duplicate value for two column
        /// </summary>
        /// <param name="col1Name">first column name</param>
        /// <param name="col2Name">second column name</param>
        /// <param name="combinationCheckRequired">Combination Check Required</param>
        public ServiceContext(string col1Name, string col2Name, bool combinationCheckRequired)
        {
            this.PagingInformation = new Pagination() { PageSize = DefaultPageSize, PagerSize = DefaultPagerSize };

            this.CheckForDuplicate = true;
            this.Col1Name = col1Name;
            this.Col2Name = col2Name;
            this.CombinationCheckRequired = combinationCheckRequired;
        }

        /// <summary>
        /// Initializes a new instance of the ServiceContext class for checking duplicate value for two column
        /// </summary>
        /// <param name="col1Name">first column name</param>
        /// <param name="col2Name">second column name</param>
        /// <param name="col3Name">second column name</param>
        /// <param name="combinationCheckRequired">Combination Check Required</param>
        public ServiceContext(string col1Name, string col2Name, string col3Name, bool combinationCheckRequired)
        {
            this.PagingInformation = new Pagination() { PageSize = DefaultPageSize, PagerSize = DefaultPagerSize };

            this.CheckForDuplicate = true;
            this.Col1Name = col1Name;
            this.Col2Name = col2Name;
            this.Col3Name = col3Name;
            this.CombinationCheckRequired = combinationCheckRequired;
        }

        #endregion

        #region Custom Methods

        public IList<StudentDetail> GetStudentList(bool isSVT, string category)
        {
            System.Collections.ObjectModel.Collection<DBParameters> parameters = new System.Collections.ObjectModel.Collection<DBParameters>();
            parameters.Add(new DBParameters() { Name = "Category", Value = category, DBType = DbType.String });
            parameters.Add(new DBParameters() { Name = "IsSVTStudent ", Value = isSVT, DBType = DbType.Boolean });
            return this.ExecuteProcedure<StudentDetail>("UspGetStudentList", parameters);
        }

        public IList<StudentDetail> GetStudentPreviewList(bool isSVT)
        {
            System.Collections.ObjectModel.Collection<DBParameters> parameters = new System.Collections.ObjectModel.Collection<DBParameters>();
            parameters.Add(new DBParameters() { Name = "IsSVTStudent ", Value = isSVT, DBType = DbType.Boolean });
            return this.ExecuteProcedure<StudentDetail>("UspGetStudentListPreview", parameters);
        }

        public IList<StudentDetail> GetStudentListForSummary()
        {
            System.Collections.ObjectModel.Collection<DBParameters> parameters = new System.Collections.ObjectModel.Collection<DBParameters>();
            return this.ExecuteProcedure<StudentDetail>("UspGetStudentListForSummary", parameters);
        }

        public object UpdatePreferenceByCourse(int ids, string courseAdmitted, string round)
        {
            /*Add Parameters*/
            System.Collections.ObjectModel.Collection<DBParameters> parameters = new System.Collections.ObjectModel.Collection<DBParameters>();
            parameters.Add(new DBParameters() { Name = "Id", Value = ids, DBType = DbType.Int32 });
            parameters.Add(new DBParameters() { Name = "CourseAdmitted", Value = courseAdmitted, DBType = DbType.String });
            parameters.Add(new DBParameters() { Name = "Round ", Value = round, DBType = DbType.String });
            return this.ExecuteProcedure("UpdatePreferenceByCourse", ExecuteType.ExecuteScalar, parameters);
        }

        #region SVT Marit List Reports
        public DataTable GenerateReports(string ReportType, bool IsSVT, bool IsOpen, int Round)
        {
            string conn = ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(conn))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("GenerateMaritReport", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 60;
                sqlCommand.Parameters.AddWithValue("@ReportType", ReportType);
                sqlCommand.Parameters.AddWithValue("@IsSVT", IsSVT);
                sqlCommand.Parameters.AddWithValue("@IsOpen", IsOpen);
                sqlCommand.Parameters.AddWithValue("@Round", Round);

                DataTable dt = new DataTable();
                dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dt);
                return dt;

            }

        }
        #endregion

        #region SVT General Reports
        public DataTable GenerateGeneralReport()
        {
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(conn))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GenerateGeneralReport", sqlConnection);

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 60;
                DataTable dt = new DataTable();
                dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dt);
                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string cp1 = Convert.ToString(row["CoursePreference1"]);
                        row["CoursePreference1"] = cp1 == null ? "" : GetSpecilizationSortName(cp1.ToUpper());

                        string cp2 = Convert.ToString(row["CoursePreference2"]);
                        row["CoursePreference2"] = cp2 == null ? "" : GetSpecilizationSortName(cp2.ToUpper());

                        string cp3 = Convert.ToString(row["CoursePreference3"]);
                        row["CoursePreference3"] = cp3 == null ? "" : GetSpecilizationSortName(cp3.ToUpper());

                        string cp4 = Convert.ToString(row["CoursePreference4"]);
                        row["CoursePreference4"] = cp4 == null ? "" : GetSpecilizationSortName(cp4.ToUpper());

                        string cp5 = Convert.ToString(row["CoursePreference5"]);
                        row["CoursePreference5"] = cp5 == null ? "" : GetSpecilizationSortName(cp5.ToUpper());

                        string cp6 = Convert.ToString(row["CoursePreference6"]);
                        row["CoursePreference6"] = cp6 == null ? "" : GetSpecilizationSortName(cp6.ToUpper());

                        string cp7 = Convert.ToString(row["CoursePreference7"]);
                        row["CoursePreference7"] = cp1 == null ? "" : GetSpecilizationSortName(cp7.ToUpper());
                    }
                }
                return dt;
            }
        }

        public DataTable GenerateGeneralReport2(bool IsSVT, string category)
        {
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(conn))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GenerateGeneralReportWithParameter", sqlConnection);
                sqlCommand.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "Category",
                    Value = category,
                    DbType = DbType.String
                });
                sqlCommand.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "IsSVT",
                    Value = IsSVT,
                    DbType = DbType.Boolean
                });
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 60;
                DataTable dt = new DataTable();
                dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dt);
                if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string cp1 = Convert.ToString(row["CP1"]);
                        row["CP1"] = cp1 == null ? "" : GetSpecilizationSortName(cp1.ToUpper());

                        string cp2 = Convert.ToString(row["CP2"]);
                        row["CP2"] = cp2 == null ? "" : GetSpecilizationSortName(cp2.ToUpper());

                        string cp3 = Convert.ToString(row["CP3"]);
                        row["CP3"] = cp3 == null ? "" : GetSpecilizationSortName(cp3.ToUpper());

                        string cp4 = Convert.ToString(row["CP4"]);
                        row["CP4"] = cp4 == null ? "" : GetSpecilizationSortName(cp4.ToUpper());

                        string cp5 = Convert.ToString(row["CP5"]);
                        row["CP5"] = cp5 == null ? "" : GetSpecilizationSortName(cp5.ToUpper());

                        string cp6 = Convert.ToString(row["CP6"]);
                        row["CP6"] = cp6 == null ? "" : GetSpecilizationSortName(cp6.ToUpper());

                        string cp7 = Convert.ToString(row["CP7"]);
                        row["CP7"] = cp1 == null ? "" : GetSpecilizationSortName(cp7.ToUpper());
                    }
                }
                return dt;
            }
        }

        #endregion

        #region GetStudentWithPagination
        public JArrayObject GetStudentWithPagination(string firstName = "", string lastName = "", DateTime? dob = null, int formId = 0, int pageNo = 0, int pageSize = 0)
        {
            bool isWhereQuery = false;
            string dateValue = null;
            if (pageNo == 0 || pageSize == 0)
            {
                pageSize = 10000;
                pageNo = 1;
            }
            if (!string.IsNullOrEmpty(firstName) || !string.IsNullOrEmpty(lastName) || dob != null || formId > 0)
                isWhereQuery = true;
            if (dob != null)
                dateValue = dob.Value.ToString("yyyy-MM-dd");

            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
            string query = GetStudentWithPaginationQuery(isWhereQuery, firstName, lastName, dateValue, formId, pageNo, pageSize);
            /* using (SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(conn))
             {
                 sqlConnection.Open();
                 SqlCommand sqlCommand = new SqlCommand("StudentWithPagination", sqlConnection);
                 sqlCommand.CommandType = CommandType.StoredProcedure;
                 sqlCommand.CommandTimeout = 60;
                 sqlCommand.Parameters.AddWithValue("@FirstName", firstName);
                 sqlCommand.Parameters.AddWithValue("@PageNo", pageNumber);
                 sqlCommand.Parameters.AddWithValue("@PageSize", pageSize);
                 DataTable dt1 = new DataTable();
                 DataTable dt2 = new DataTable();

                 dt1.Locale = System.Globalization.CultureInfo.InvariantCulture;
                 dt2.Locale = System.Globalization.CultureInfo.InvariantCulture;
                 SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                 //sqlAdapter.Fill(dt);
                 using (DataSet ds = new DataSet())
                 {
                     sqlAdapter.Fill(ds);
                     dt1 = ds.Tables[0];
                     dt2 = ds.Tables[1];
                 }
                 JArrayObject obj = new JArrayObject();
                 obj.Data = ToJson(dt1);
                 obj.Count = int.Parse(dt2.Rows[0][0].ToString());
                 obj.IsSuccess = true;
                 obj.SuccessMessage = "Success";
                 return obj;
             }*/
            using (SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(conn))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandTimeout = 60;
                DataTable dt = new DataTable();
                dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                dt1.Locale = System.Globalization.CultureInfo.InvariantCulture;
                dt2.Locale = System.Globalization.CultureInfo.InvariantCulture;
                //sqlAdapter.Fill(dt);
                using (DataSet ds = new DataSet())
                {
                    sqlAdapter.Fill(ds);
                    dt1 = ds.Tables[0];
                    dt2 = ds.Tables[1];
                }
                JArrayObject obj = new JArrayObject();
                obj.Data = ToJson(dt1);
                obj.Count = int.Parse(dt2.Rows[0][0].ToString());
                obj.IsSuccess = true;
                obj.SuccessMessage = "Success";
                return obj;
            }


        }

        public JArray ToJson(System.Data.DataTable source)
        {
            JArray result = new JArray();
            JObject row;
            foreach (System.Data.DataRow dr in source.Rows)
            {
                row = new JObject();
                foreach (System.Data.DataColumn col in source.Columns)
                {
                    row.Add(col.ColumnName.Trim(), JToken.FromObject(dr[col]));
                }
                result.Add(row);
            }
            return result;
        }
        #endregion

        public int UpdateFinalAdmitted()
        {
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(conn))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("UpdateFinalAdmitted", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 60;
                return sqlCommand.ExecuteNonQuery();
            }
        }

        public bool GenerateRollNumber()
        {
            try
            {
                string conn = System.Configuration.ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
                int rollGap = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["RollNumberGap"]);
                int start = 1;
                List<Division> dItems = new List<Division>();
                string dFilepath = HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/Division.json"));
                using (StreamReader r = new StreamReader(dFilepath))
                {
                    string json = r.ReadToEnd();
                    dItems = JsonConvert.DeserializeObject<List<Division>>(json);
                    r.Close();
                }
                foreach (Division d in dItems)
                {
                    foreach (string dcourse in d.Specialisations)
                    {
                        using (SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(conn))
                        {
                            sqlConnection.Open();
                            SqlCommand sqlCommand = new SqlCommand("AssignRollNumber", sqlConnection);
                            sqlCommand.CommandType = CommandType.StoredProcedure;
                            sqlCommand.CommandTimeout = 60;
                            sqlCommand.Parameters.AddWithValue("@Course", dcourse);
                            DataTable dt = new DataTable();
                            dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                            SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                            sqlAdapter.Fill(dt);
                            if (dt != null && dt.Rows != null && dt.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    UpdateStudentRollNumer(row["id"].ToString(), start);
                                    start = start + 1;
                                }
                                start = start + 10;
                            }
                        }

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateStudentRollNumer(string Id, int value)
        {
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;
            string sql = null;
            connetionString = System.Configuration.ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
            sql = string.Format("Update StudentDetails set RollNumber={0} where Id='{1}'", value, Id);
            string key = string.Empty;
            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return true;
        }

        public DataTable GetStudentByRollNumber(string course)
        {
            string conn = ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(conn))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("GetStudentByRollnumber", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 60;
                sqlCommand.Parameters.AddWithValue("@Course", course);
                DataTable dt = new DataTable();
                dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetStudentIdCardReport(string Specialization)
        {
            string conn = ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(conn))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GetStudentIdCardReport", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 60;
                sqlCommand.Parameters.AddWithValue("@Specialization", Specialization);

                List<Department> items = new List<Department>();
                string filepath = HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/Department.json"));
                using (StreamReader r = new StreamReader(filepath))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<Department>>(json);
                    r.Close();
                }

                List<Division> dItems = new List<Division>();
                string dFilepath = HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/Division.json"));
                using (StreamReader r = new StreamReader(dFilepath))
                {
                    string json = r.ReadToEnd();
                    dItems = JsonConvert.DeserializeObject<List<Division>>(json);
                    r.Close();
                }


                DataTable dt = new DataTable();
                dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dt);
                string FolderPath = System.Web.HttpContext.Current.Server.MapPath(String.Format("~/data"));
                foreach (DataRow row in dt.Rows)
                {
                    string FinalAdminted = row["FinalAdmitted"].ToString();
                    var deptname = items.FirstOrDefault(x => x.Specialisations.Contains(FinalAdminted.ToUpper())).DepartmentName;
                    row["Department"] = deptname;
                    var divname = dItems.FirstOrDefault(x => x.Specialisations.Contains(FinalAdminted.ToUpper())).DivisionName;
                    row["Division"] = divname;

                    string photo = Convert.ToString(row["Photo"]);
                    if (!string.IsNullOrEmpty(photo) && photo != "-")
                    {
                        row["Photo"] = Path.GetFileNameWithoutExtension(photo); //FolderPath + "\\Photos\\" + row["Photo"];
                    }
                    string signature = Convert.ToString(row["Signature"]);
                    if (!string.IsNullOrEmpty(signature) && signature != "-")
                    {
                        row["Signature"] = Path.GetFileNameWithoutExtension(signature); //FolderPath + "\\Signature\\" + row["Signature"];
                    }
                }
                dt.Columns.Remove("FinalAdmitted");
                return dt;
            }
        }

        public DataTable GetGeneralElectiveReport(string Specialization)
        {
            string conn = ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(conn))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GetGeneralElectiveReport", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 60;
                sqlCommand.Parameters.AddWithValue("@Specialization", Specialization);

                List<Department> items = new List<Department>();
                string filepath = HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/Department.json"));
                using (StreamReader r = new StreamReader(filepath))
                {
                    string json = r.ReadToEnd();
                    items = JsonConvert.DeserializeObject<List<Department>>(json);
                    r.Close();
                }

                List<Division> dItems = new List<Division>();
                string dFilepath = HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/Division.json"));
                using (StreamReader r = new StreamReader(dFilepath))
                {
                    string json = r.ReadToEnd();
                    dItems = JsonConvert.DeserializeObject<List<Division>>(json);
                    r.Close();
                }


                DataTable dt = new DataTable();
                DataTable ndt = new DataTable();
                ndt.Columns.Add("Full Name");
                ndt.Columns.Add("Department");
                ndt.Columns.Add("Division");
                ndt.Columns.Add("Date Of Birth");
                ndt.Columns.Add("BloodGroup");
                ndt.Columns.Add("Correspondence Address");
                ndt.Columns.Add("Mobile Number");
                ndt.Columns.Add("Photo");
                ndt.Columns.Add("Signature");
                ndt.Columns.Add("FinalAdmitted");
                ndt.Columns.Add("General Elective 1");
                ndt.Columns.Add("General Elective 2");

                dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                ndt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dt);
                string FolderPath = System.Web.HttpContext.Current.Server.MapPath(String.Format("~/data"));
                foreach (DataRow row in dt.Rows)
                {
                    DataRow r = ndt.NewRow();
                    r["Full Name"] = row["Full Name"];
                    r["FinalAdmitted"] = row["FinalAdmitted"];
                    r["Date Of Birth"] = row["Date Of Birth"];
                    r["BloodGroup"] = row["BloodGroup"];
                    r["Correspondence Address"] = row["Correspondence Address"];
                    r["Mobile Number"] = row["Mobile Number"];

                    string FinalAdminted = row["FinalAdmitted"].ToString();
                    var deptname = items.FirstOrDefault(x => x.Specialisations.Contains(FinalAdminted.ToUpper())).DepartmentName;
                    r["Department"] = deptname;
                    var divname = dItems.FirstOrDefault(x => x.Specialisations.Contains(FinalAdminted.ToUpper())).DivisionName;
                    r["Division"] = divname;

                    string photo = Convert.ToString(row["Photo"]);
                    if (!string.IsNullOrEmpty(photo) && photo != "-")
                    {
                        r["Photo"] = Path.GetFileNameWithoutExtension(photo); //FolderPath + "\\Photos\\" + row["Photo"];
                    }
                    string signature = Convert.ToString(row["Signature"]);
                    if (!string.IsNullOrEmpty(signature) && signature != "-")
                    {
                        r["Signature"] = Path.GetFileNameWithoutExtension(signature); //FolderPath + "\\Signature\\" + row["Signature"];
                    }
                    if (row["FinalAdmitted"].Equals(row["CoursePreference1"]))
                    {
                        r["General Elective 1"] = row["Preference1GE1"];
                        r["General Elective 2"] = row["Preference1GE2"];
                    }
                    else if (row["FinalAdmitted"].Equals(row["CoursePreference2"]))
                    {
                        r["General Elective 1"] = row["Preference2GE1"];
                        r["General Elective 2"] = row["Preference2GE2"];
                    }
                    else if (row["FinalAdmitted"].Equals(row["CoursePreference3"]))
                    {
                        r["General Elective 1"] = row["Preference3GE1"];
                        r["General Elective 2"] = row["Preference3GE2"];
                    }
                    else if (row["FinalAdmitted"].Equals(row["CoursePreference4"]))
                    {
                        r["General Elective 1"] = row["Preference4GE1"];
                        r["General Elective 2"] = row["Preference4GE2"];
                    }
                    else if (row["FinalAdmitted"].Equals(row["CoursePreference5"]))
                    {
                        r["General Elective 1"] = row["Preference5GE1"];
                        r["General Elective 2"] = row["Preference5GE2"];
                    }
                    else if (row["FinalAdmitted"].Equals(row["CoursePreference6"]))
                    {
                        r["General Elective 1"] = row["Preference6GE1"];
                        r["General Elective 2"] = row["Preference6GE2"];
                    }
                    else if (row["FinalAdmitted"].Equals(row["CoursePreference7"]))
                    {
                        r["General Elective 1"] = row["Preference7GE1"];
                        r["General Elective 2"] = row["Preference7GE2"];
                    }
                    ndt.Rows.Add(r);
                }
                return ndt;
            }
        }


        public string GetSpecilizationSortName(string name)
        {
            switch (name)
            {
                case "HOSPITALITY & TOURISM MANAGEMENT":
                    return "HTM";
                case "INTERIOR DESIGN & RESOURCE MANAGEMENT":
                    return "IDRM";
                case "FOOD, NUTRITION AND DIETETICS":
                    return "FND";
                case "DEVELOPMENTAL COUNSELLING":
                    return "DC";
                case "EARLY CHILDHOOD CARE & EDUCATION":
                    return "ECCE";
                case "MASS COMMUNICATION & EXTENSION":
                    return "MCE";
                case "TEXTILES & APPAREL DESIGNING":
                    return "TAD";
                default:
                    return "";
            }
        }

        public string GetStudentWithPaginationQuery(bool isVarQuery, string firstName, string lastName, string dob, int formNumber, int pageNo, int pageSize)
        {
            string countQuery = " Select COUNT(distinct(aadharnumber)) as Count from StudentDetails where (aadharnumber is not null and Rtrim(Ltrim(aadharnumber)) <> '') ";

            string sql = "select *,ROW_NUMBER() OVER(ORDER BY LastModified DESC) AS 'Serial Number' " +
" from ( SELECT DISTINCT(aadharnumber),ROW_NUMBER() OVER(PARTITION BY aadharnumber ORDER BY LastModified DESC) AS Row_Number, " +
" Id as Id,LastModified, ISNULL((LastName+' '+FirstName+' '+FatherName+' '+MotherName),'')AS 'Student Full Name'," +
" LastName as 'Last Name',FirstName as 'First Name',FatherName as 'Father Name',MotherName as 'Mother Name'," +
" ISNULL((CAST(Dob AS varchar)) ,'') AS 'Date Of Birth'," +
" CAST(Id AS varchar)+'_'+FirstName+'_'+LastName+'_'+format(DateRegistered,'ddmmyyyy_hhmmss')+'.pdf' as PDFPath," +
" DateRegistered as DateRegistered,ISNULL((MKCLFormNumber),'') as 'MKCLFormNumber',ISNULL((PermanentAddress),'') AS Address," +
" ISNULL((MobileNumber),'')AS 'Telephone Number',ISNULL((Email),'')AS Email," +
" (CASE WHEN ISNULL((IsSVTStudent),'') <> 0 THEN 'Yes' ELSE 'No' END) AS 'SVT Student'," +
" ISNULL((Category),'') AS Category,ISNULL((IsScience),'')AS 'Science Student',ISNULL((Caste),'')AS Caste," +
" ISNULL((Percentage),0)AS Percentage,ISNULL((CoursePreference1),'')AS CoursePreference1," +
" ISNULL((CoursePreference2),'')AS CoursePreference2,ISNULL((CoursePreference3),'')AS CoursePreference3," +
" ISNULL((CoursePreference4),'')AS CoursePreference4,ISNULL((CoursePreference5),'')AS CoursePreference5," +
" ISNULL((CoursePreference6),'')AS CoursePreference6,ISNULL((CoursePreference7),'')AS CoursePreference7," +
" ISNULL((Preference1GE1),'')AS Preference1GE1,ISNULL((Preference1GE2),'')AS Preference1GE2," +
" ISNULL((Preference2GE1),'')AS Preference2GE1,ISNULL((Preference2GE2),'')AS Preference2GE2," +
" ISNULL((Preference3GE1),'')AS Preference3GE1,ISNULL((Preference3GE2),'')AS Preference3GE2," +
" ISNULL((Preference4GE1),'')AS Preference4GE1,ISNULL((Preference4GE2),'')AS Preference4GE2," +
" ISNULL((Preference5GE1),'')AS Preference5GE1,ISNULL((Preference5GE2),'')AS Preference5GE2," +
" ISNULL((Preference6GE1),'')AS Preference6GE1,ISNULL((Preference6GE2),'')AS Preference6GE2," +
" ISNULL((Preference7GE1),'')AS Preference7GE1,ISNULL((Preference7GE2),'')AS Preference7GE2," +
" (CASE WHEN ISNULL((IsDuplicate),0) <> 0 THEN 'Yes' ELSE 'No' END) AS 'Duplicate Student'," +
" (CASE WHEN ISNULL((IsAdmitted),0) <> 0 THEN 'Yes' ELSE 'No' END) AS 'Admitted Student'," +
" (CASE WHEN ISNULL((IsCancelled),0) <> 0 THEN 'Yes' ELSE 'No' END) AS 'Cancelled Student'," +
" (CASE WHEN ISNULL((IsFormSubmitted),0) <> 1 THEN 'No' ELSE 'Yes' END) AS 'Form Submitted'," +
" (CASE WHEN ISNULL((IsFeesPaid),0) <> 1 THEN 'No' ELSE 'Yes' END) AS 'Fees Paid'" +
" FROM StudentDetails where (aadharnumber is not null and Rtrim(Ltrim(aadharnumber)) <> '') and ";
            if (isVarQuery)
            {
                string whereQuery = " ";
                if (!string.IsNullOrEmpty(firstName))
                {
                    whereQuery = string.Format(whereQuery + " FirstName LIKE '%{0}%' and ", firstName);
                }
                if (!string.IsNullOrEmpty(lastName))
                {
                    whereQuery = string.Format(whereQuery + " LastName LIKE '%{0}%' and ", lastName);
                }
                if (!string.IsNullOrEmpty(dob))
                {
                    whereQuery = string.Format(whereQuery + " CAST(Dob AS varchar) LIKE '%{0}%' and ", dob);
                }
                if (formNumber > 0)
                {
                    whereQuery = string.Format(whereQuery + " Id={0} and ", formNumber);
                }
                string removed = "and";
                int index = whereQuery.LastIndexOf(removed);
                whereQuery = (index < 0) ? whereQuery : whereQuery.Remove(index, removed.Length);
                sql = sql + whereQuery;
                countQuery = countQuery + " and " + whereQuery;
            }
            else
            {
                string removed = "and";
                int index = sql.LastIndexOf(removed);
                sql = (index < 0) ? sql : sql.Remove(index, removed.Length);
            }
            sql = sql + string.Format("  ) test where test.Row_Number=1 ORDER By test.LastModified DESC OFFSET ({0} * ({1} - 1)) ROWS FETCH NEXT {2} ROWS ONLY; ", pageSize, pageNo, pageSize);
            sql = sql + countQuery;

            return sql;
        }
        #endregion

        public DataTable GetOperationalReport(string sp, DateTime date, DateTime endDate)
        {
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(conn))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(sp, sqlConnection);
                string datesrt = date.ToString("yyyy/MM/dd");
                string dateEnd = endDate.ToString("yyyy/MM/dd");
                sqlCommand.Parameters.AddWithValue("@Reportdate", datesrt);

                if (sp == "GetFormSubmittedReportByDate" || sp == "GetFessPaidReportByDate")
                {
                    sqlCommand.Parameters.AddWithValue("@Enddate", dateEnd);
                }

                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 60;
                DataTable dt = new DataTable();
                dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetHostelApplicationReport()
        {
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(conn))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GetHostelApplications", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 60;
                DataTable dt = new DataTable();
                dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dt);
                return dt;
            }
        }

        #region SVT Enrollment Reports
        public DataTable GenerateEnrollmentReport()
        {
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(conn))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand("GetEnrollmentReport", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 60;
                DataTable dt = new DataTable();
                dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetMasterDataForExam()
        {
            string conn = ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(conn))
            {
                sqlConnection.Open();
                string query = " select RollNumber,FirstName,LastName,FatherName,MotherName, " +
                " FinalAdmitted,CurrentAddress,MobileNumber,Photo,SignaturePath, " +
                " CoursePreference1,Preference1GE1,Preference1GE2, " +
                " CoursePreference2,Preference2GE1,Preference2GE2, " +
                " CoursePreference3,Preference3GE1,Preference3GE2, " +
                " CoursePreference4,Preference4GE1,Preference4GE2, " +
                " CoursePreference5,Preference5GE1,Preference5GE2, " +
                " CoursePreference6,Preference6GE1,Preference6GE2, " +
                " CoursePreference7,Preference7GE1,Preference7GE2 " +
                " from StudentDetails where RollNumber>0  order by RollNumber";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;

                DataTable dt = new DataTable();
                DataTable ndt = new DataTable();
                ndt.Columns.Add("RollNumber");
                ndt.Columns.Add("FirstName");
                ndt.Columns.Add("LastName");
                ndt.Columns.Add("FatherName");
                ndt.Columns.Add("MotherName");
                ndt.Columns.Add("Course");
                ndt.Columns.Add("Specialisation");
                ndt.Columns.Add("PRN");
                ndt.Columns.Add("Address");
                ndt.Columns.Add("PhoneNumber");
                ndt.Columns.Add("PhotoPath");
                ndt.Columns.Add("SignaturePath");
                ndt.Columns.Add("GeneralElective1");
                ndt.Columns.Add("GeneralElective2");

                dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                ndt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dt);
                string FolderPath = System.Web.HttpContext.Current.Server.MapPath(String.Format("~/data"));
                foreach (DataRow row in dt.Rows)
                {
                    DataRow r = ndt.NewRow();
                    r["RollNumber"] = row["RollNumber"];
                    r["FirstName"] = row["FirstName"];
                    r["LastName"] = row["LastName"];
                    r["FatherName"] = row["FatherName"];
                    r["MotherName"] = row["MotherName"];
                    r["Course"] = "BSc - Regular";
                    r["Specialisation"] = GetSpecilizationSortName(row["FinalAdmitted"].ToString().ToUpper());
                    r["PRN"] = "";
                    string add = Convert.ToString(row["CurrentAddress"]);
                    r["Address"] = (add == null) ? "" : add.Replace(",", " ").Replace("\n", String.Empty)
                        .Replace("\r", String.Empty).Replace("\t", String.Empty);
                    r["PhoneNumber"] = row["MobileNumber"];
                    string photo = Convert.ToString(row["Photo"]);
                    if (!string.IsNullOrEmpty(photo) && photo != "-")
                    {
                        r["PhotoPath"] = photo; //FolderPath + "\\Photos\\" + row["Photo"];
                    }
                    string signature = Convert.ToString(row["SignaturePath"]);
                    if (!string.IsNullOrEmpty(signature) && signature != "-")
                    {
                        r["SignaturePath"] = signature; //FolderPath + "\\Signature\\" + row["Signature"];
                    }
                    if (row["FinalAdmitted"].Equals(row["CoursePreference1"]))
                    {
                        r["GeneralElective1"] = row["Preference1GE1"];
                        r["GeneralElective2"] = row["Preference1GE2"];
                    }
                    else if (row["FinalAdmitted"].Equals(row["CoursePreference2"]))
                    {
                        r["GeneralElective1"] = row["Preference2GE1"];
                        r["GeneralElective2"] = row["Preference2GE2"];
                    }
                    else if (row["FinalAdmitted"].Equals(row["CoursePreference3"]))
                    {
                        r["GeneralElective1"] = row["Preference3GE1"];
                        r["GeneralElective2"] = row["Preference3GE2"];
                    }
                    else if (row["FinalAdmitted"].Equals(row["CoursePreference4"]))
                    {
                        r["GeneralElective1"] = row["Preference4GE1"];
                        r["GeneralElective2"] = row["Preference4GE2"];
                    }
                    else if (row["FinalAdmitted"].Equals(row["CoursePreference5"]))
                    {
                        r["GeneralElective1"] = row["Preference5GE1"];
                        r["GeneralElective2"] = row["Preference5GE2"];
                    }
                    else if (row["FinalAdmitted"].Equals(row["CoursePreference6"]))
                    {
                        r["GeneralElective1"] = row["Preference6GE1"];
                        r["GeneralElective2"] = row["Preference6GE2"];
                    }
                    else if (row["FinalAdmitted"].Equals(row["CoursePreference7"]))
                    {
                        r["GeneralElective1"] = row["Preference7GE1"];
                        r["GeneralElective2"] = row["Preference7GE2"];
                    }
                    ndt.Rows.Add(r);
                }
                return ndt;
            }
        }

        public DataTable GetStudentRegisterFormExcelData()
        {
            string conn = ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
            using (SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(conn))
            {
                sqlConnection.Open();
                string query = "  select '' as 'Register No.',LastName+' '+FirstName+' '+FatherName+' '+MotherName as 'NAME OF THE STUDENT'," +
"CurrentAddress as 'FULL ADRESS',Caste+' '+Religion as 'CAST & RELIGION'," +
"PlaceofBirth as 'BIRTH PLACE',Dob as 'BIRTH DATE',MaritalStatus as 'SPECIALIZATION  NAME MARRIED OR UNMARRIED'," +
"DateRegistered as 'DATE OF ADMISSION',LastSchoolAttend as 'LAST SCHOOL OR COLLEGE ATTENDED '," +
"FinalAdmitted as 'CLASS IN WHICH ADMITTED','' as 'L.C.NO','' as 'AGGREATE POINTS','' as 'TOTAL GRADE POINTS','' as 'REMARKS'" +
" from StudentDetails where RollNumber>0 order by RollNumber;";
                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.CommandType = CommandType.Text;

                DataTable dt = new DataTable();
                dt.Locale = System.Globalization.CultureInfo.InvariantCulture;
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                sqlAdapter.Fill(dt);
                return dt;
            }
        }

        #endregion
    }

}
