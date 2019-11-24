using ITM.Courses.DAOBase;
using ITM.Courses.LogManager;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

namespace ITM.Courses.DAO
{
    public class UserTimeTrackerDAO
    {
        Logger logger = new Logger();
        private string Select_Time_Taken_By_All_User = "Select * from usertimetracker";
        private string Add_Time_Used_By_User = "INSERT INTO usertimetracker(userId, chapterId, sectionId, timespent) VALUES ({0}, {1}, {2}, {3})";
        private string Update_Time_Used_By_User = " UPDATE usertimetracker set timespent = {0} WHERE Id = {1}";
        private string Get_UserTimeByUserChaperSectionId = "Select * from usertimetracker where userId = {0} and chapterId = {1} and sectionId = {2}";

        private string Q_GetTimeTakenByUserChapterId = "select sum(timespent) as UserTimeTaken from usertimetracker where userid ={0} and chapterid= {1} group by chapterid";

        #region Select function
        public List<UserTimeTracker> GetTimeTakenByAllUser(string cnxnString, string logPath)
        {
            try
            {
                logger.Debug("UserTimeTrackerDAO", "GetTimeTakenByAllUser", "Selecting Time Taken by User list from database", logPath);
                string result = Select_Time_Taken_By_All_User;
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserTimeTracker> userlist = new List<UserTimeTracker>();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UserTimeTracker user = new UserTimeTracker();
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user.UserId = Convert.ToInt32(reader["userId"]);
                                user.ChapterId = Convert.ToInt32(reader["chapterId"]);
                                user.TimeSpent = Convert.ToInt32(reader["timespent"]);
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
                logger.Error("UserTimeTrackerDAO", "GetTimeTakenByAllUser", " Error occurred while Getting Time Taken By User Details list", ex, logPath);
                throw ex;
            }
        }

        // Get UserTracker object By UserId, Chapterid and section id
        public UserTimeTracker GetUserTimeTakerByUserChapterSectionId(int userId, int chapterId, int sectionId, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Get_UserTimeByUserChaperSectionId, userId, chapterId, sectionId);

                Database db = new Database();
                UserTimeTracker userTracker = new UserTimeTracker();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                userTracker.Id = Convert.ToInt32(reader["Id"]);
                                userTracker.UserId = Convert.ToInt32(reader["userId"]);
                                userTracker.ChapterId = Convert.ToInt32(reader["chapterId"]);
                                userTracker.SectionId = Convert.ToInt32(reader["sectionId"]);
                                userTracker.TimeSpent = Convert.ToInt32(reader["timespent"]);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return userTracker;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal GetTimeTakenByUserInHours(int userId, int chapterId, string cnxnString, string logPath)
        {
            int timeInSeconds = 0;

            try
            {
                string cmdText = string.Format(Q_GetTimeTakenByUserChapterId, userId, chapterId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                timeInSeconds = Convert.ToInt32(reader["UserTimeTaken"]);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }

                            if (timeInSeconds > 0)
                            {
                                decimal hours = (timeInSeconds / 60) / 60;
                                hours = Math.Round((decimal.Parse(timeInSeconds.ToString()) / decimal.Parse(60.ToString())) / 60, 2);
                                return hours;
                            }
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return timeInSeconds;
        }

        public decimal GetTimeTakenByUserInMinuts(int userId, int chapterId, string cnxnString, string logPath)
        {
            int timeInSeconds = 0;

            try
            {
                string cmdText = string.Format(Q_GetTimeTakenByUserChapterId, userId, chapterId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                timeInSeconds = Convert.ToInt32(reader["UserTimeTaken"]);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }

                            if (timeInSeconds > 0)
                            {
                                decimal hours = (timeInSeconds) / 60;
                                hours = Math.Round((decimal.Parse(timeInSeconds.ToString()) / decimal.Parse(60.ToString())), 2);
                                return hours;
                            }
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return timeInSeconds;
        }

        #endregion

        #region Insert Statement
        public void AddTimingDetails(int userId, int chapterId, int sectionId, int timeSpent, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string result = string.Format(Add_Time_Used_By_User, userId, chapterId, sectionId, timeSpent);
                    Database db = new Database();
                    db.Insert(result, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserTimeTrackerDAO", "InsertTimingDetails", " Error occurred while saving timing Details", ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Update Statement
        public void UpdateUserTimeTracker(int id, int timeSpent, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string result = string.Format(Update_Time_Used_By_User, timeSpent, id);
                    Database db = new Database();
                    db.Update(result, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserTimeTrackerDAO", "UpdateTimingDetails", " Error occurred while updating timing details", ex, logPath);
                throw ex;
            }
        }
        #endregion
    }
}
