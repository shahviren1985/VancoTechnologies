using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITM.Courses.LogManager;
using ITM.Courses.DAOBase;
using System.Data.Common;
using ITM.Courses.Utilities;
using System.Data;
using MySql.Data.MySqlClient;

namespace ITM.Courses.DAO
{
    public class UserActivityTrackerDAO
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");


        Logger logger = new Logger();
        private string Select_All_Users_Activity_Tracker = "Select * from useractivitytracker order by datecreated desc";
        private string Select_All_Student_According_To_LastActivityDate = "Select * from userdetails where userType = 'user' and id in (select Distinct userID from useractivitytracker where dateCreated <= '{0}') UNION Select * from userdetails";
        //private string Select_All_Student_According_To_LastActivityDate = "Select * from userdetails where id in (select Distinct userID from useractivitytracker where dateCreated between '{0}' and '{1}')";
        private string Add_UserActivityTracker = "insert into useractivitytracker(userId, Activity, DateCreated) values({0},'{1}', '{2}')";
        private string Update_UserActivityTracker = "update useractivitytracker set userId={0}, Activity = '{1}', DateCreated ='{2}' where id='{3}'";
        private string Q_GetTop5ActivitiesUserId = "SELECT * FROM useractivitytracker where userid = {0}  order by datecreated desc limit 5";

        #region Select Function
        public List<UserActivityTraker> GetAllUsersActivities(string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("UserActivityTrackerDAO", "GetAllUsersActivity", "Selecting User Activity Details list from database", logPath);
                string result = Select_All_Users_Activity_Tracker;
                Database db = new Database();
                List<UserActivityTraker> userlist = new List<UserActivityTraker>();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UserActivityTraker user = new UserActivityTraker();
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user.UserId = Convert.ToInt32(reader["userId"]);
                                user.Activity = reader["Activity"].ToString();
                                user.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());

                                user.DateCreated = new TimeFormats().GetIndianStandardTime(user.DateCreated);

                                userlist.Add(user);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return userlist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserActivityTrackerDAO", "GetAllUsersActivity", " Error occurred while Getting User Activity list", ex, logPath);
                throw new Exception("11377");
            }
        }

        public List<UserActivityTraker> GetTop5ActivitiesUserId(int userId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("UserActivityTrackerDAO", "GetAllUsersActivity", "Selecting User Activity Details list from database", logPath);
                string result = string.Format(Q_GetTop5ActivitiesUserId, userId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserActivityTraker> userlist = new List<UserActivityTraker>();
                    DataSet ds = db.SelectDataSet(result, logPath, con);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            UserActivityTraker user = new UserActivityTraker();
                            user.Id = Convert.ToInt32(row["Id"]);
                            user.UserId = Convert.ToInt32(row["userId"]);
                            user.Activity = ParameterFormater.UnescapeXML(row["Activity"].ToString());
                            user.DateCreated = Convert.ToDateTime(row["DateCreated"].ToString());

                            //DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                            user.DateCreated = new TimeFormats().GetIndianStandardTime(user.DateCreated);

                            userlist.Add(user);
                        }

                        //logger.Debug("UserActivityTrackerDAO", "GetTop5ActivitiesUserId", " returning User Activity Details list  from database", logPath);
                    }

                    con.Close();
                    return userlist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserActivityTrackerDAO", "GetTop5ActivitiesUserId", " Error occurred while Getting User Activity list", ex, logPath);
                throw new Exception("11377");
            } 
        }

        public UserActivityTraker GetUserActivityByDescDate(int userId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("UserActivityTrackerDAO", "GetAllUsersActivity", "Selecting User Activity Details list from database", logPath);
                string result = string.Format(Q_GetTop5ActivitiesUserId, userId);
                Database db = new Database();
                UserActivityTraker user = new UserActivityTraker();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();

                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user.UserId = Convert.ToInt32(reader["userId"]);
                                user.Activity = ParameterFormater.UnescapeXML(reader["Activity"].ToString());
                                user.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                            //logger.Debug("UserActivityTrackerDAO", "GetTop5ActivitiesUserId", " returning User Activity Details list  from database", logPath);
                        }
                    }

                    con.Close();
                    return user;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserActivityTrackerDAO", "GetTop5ActivitiesUserId", " Error occurred while Getting User Activity list", ex, logPath);
                throw new Exception("11377");
            }
        }


        // function to select student who are not done any activity in last few days.
        public List<UserDetails> GetStudentByLastActivity(DateTime date, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("UserActivityTrackerDAO", "GetStudentByLastActivity", "Selecting Student Activity Details list from database", logPath);
                string result = string.Format(Select_All_Student_According_To_LastActivityDate, date.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserDetails> studentList = new List<UserDetails>();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            
                            while (reader.Read())
                            {
                                UserDetails student = new UserDetails();
                                student.Id = Convert.ToInt32(reader["Id"]);
                                student.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                student.LastLogin = Convert.ToDateTime(reader["LastLogin"].ToString());
                                student.FirstName = ParameterFormater.UnescapeXML(reader["FirstName"].ToString());
                                student.LastName = ParameterFormater.UnescapeXML(reader["LastName"].ToString());
                                student.FatherName = ParameterFormater.UnescapeXML(reader["FatherName"].ToString());
                                student.MotherName = ParameterFormater.UnescapeXML(reader["MotherName"].ToString());
                                student.EmailAddress = reader["EmailAddress"].ToString();
                                student.UserType = reader["userType"].ToString();
                                student.UserName = reader["username"].ToString();
                                student.Password = reader["Password"].ToString();
                                student.MobileNo = reader["MobileNo"].ToString();
                                student.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                student.IsEnable = Convert.ToBoolean(reader["IsEnabled"]);
                                student.IsCompleted = Convert.ToBoolean(reader["IsCompleted"]);
                                student.IsCertified = Convert.ToBoolean(reader["IsCertified"]);
                                student.VersionRegister = reader["VersionRegistered"].ToString();
                                student.MobileNo = reader["MobileNo"].ToString();
                                student.RollNumber = reader["RollNumber"].ToString();
                                student.IsNewUser = Convert.ToBoolean(reader["IsNewUser"]);

                                student.DateCreated = new TimeFormats().GetIndianStandardTime(student.DateCreated);

                                studentList.Add(student);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }

                            //logger.Debug("UserActivityTrackerDAO", "GetStudentByLastActivity", " returning Student Activity Details list  from database", logPath);
                        }
                    }

                    con.Close();
                    return studentList;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserActivityTrackerDAO", "GetStudentByLastActivity", " Error occurred while Getting Student Activity list", ex, logPath);
                throw new Exception("11377");
            } 
        }

        #endregion

        #region Insert Function
        public void AddUserActivityTracker(int userId, string activity, DateTime dateCreated, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Add_UserActivityTracker, userId, ParameterFormater.FormatParameter(ParameterFormater.GetSpecialCharsQS(activity)), dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                    Database db = new Database();
                    db.Insert(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserActivityTrackerDAO", "AddUserActivityTracker", ex.Message, ex, logPath);
                throw ex;
            } 
        }
        #endregion

        #region Update Function
        public void UpdateUserActivityTracker(int id, int userId, string activity, DateTime dateCreated, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Update_UserActivityTracker, userId, ParameterFormater.FormatParameter(activity), dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"), id);
                    Database db = new Database();
                    db.Update(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserActivityTrackerDAO", "UpdateUserActivityTracker", "Error Occured while updating Chapter Details", ex, logPath);
                throw ex;
            }
        }
        #endregion
    }
}
