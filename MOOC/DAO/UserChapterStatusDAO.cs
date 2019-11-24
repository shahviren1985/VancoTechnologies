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
    public class UserChapterStatusDAO
    {
        Logger logger = new Logger();
        private string Select_All_Users_Chapter_Status = "Select * from userchapterstatus";
        private string INSERT_USER_CHAPTER_STATUS = "INSERT INTO userchapterstatus (userId,chapterId,sectionId,contentVersion,DateCreated) VALUES ({0},{1},{2},'{3}','{4}')";
        private string Update_UserChapterStatus = "update userchapterstatus set userId = {0}, chapterId = {1}, sectionId={2}, contentVersion='{3}', DateCreated ='{4}' where id='{5}'";
        private string Get_UserChapterStatusByUserChaperSectionId = "Select * from userchapterstatus where userId = {0} and chapterId = {1} and sectionId = {2}";
        //private string Get_UserMaxChapterByUserId = "SELECT max(chapterid) as chapterId  FROM userchapterstatus where userid = {0}";
        private string Get_UserMaxChapterByUserId = "SELECT chapterId FROM userchapterstatus u where userid = {0} and chapterId in (select id from chapterdetails where courseid = {1}) order by datecreated desc limit 1";
        private string Get_UserLastChapterId = "SELECT chapterId FROM userchapterstatus u where userid = {0} order by datecreated desc limit 1";
        //private string Get_UserMaxSectionIdByUserIdChapterId = "SELECT max(sectionid) as sectionId  FROM userchapterstatus where userid = {0} and chapterid={1}";
        private string Get_UserMaxSectionIdByUserIdChapterId = "SELECT sectionid FROM userchapterstatus u where userid = {0} and chapterid= {1} order by datecreated desc limit 1";

        private string Get_ChapterStatusByCourseUser = "SELECT u.* FROM userchapterstatus u inner join chapterdetails c on u.chapterid = c.id where c.courseid= @courseid and u.userid = @userid";

        #region Select Function
        public List<UserChapterStatus> GetAllUsersChapterStatus(string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("UserChapterStatusDAO", "GetAllUsersChapterStatus", "Selecting User Chapter Status Details list from database", logPath);
                string result = Select_All_Users_Chapter_Status;
                Database db = new Database();
                List<UserChapterStatus> userlist = new List<UserChapterStatus>();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UserChapterStatus user = new UserChapterStatus();
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user.UserId = Convert.ToInt32(reader["userId"]);
                                user.ChapterId = Convert.ToInt32(reader["chapterId"]);
                                user.SectionId = Convert.ToInt32(reader["sectionId"]);
                                user.ContentVersion = reader["contentVersion"].ToString();
                                user.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
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
                logger.Error("UserChapterStatusDAO", "GetAllUsersChapterStatus", " Error occurred while Getting User Chapter Status list", ex, logPath);
                throw ex;
            }
        }

        public List<UserChapterStatus> GetChapterStatusByCourseUserId(int courseId, int userId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("UserChapterStatusDAO", "GetChapterStatusByCourseUserId", "Selecting User Chapter Status Details list from database", logPath);

                //string result = string.Format(Get_ChapterStatusByCourseUser, courseId, userId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();

                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    DataSet ds = new DataSet();

                    MySqlCommand cmd = new MySqlCommand(Get_ChapterStatusByCourseUser, con);
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.AddWithValue("courseid", courseId);
                    cmd.Parameters.AddWithValue("userid", userId);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);

                    List<UserChapterStatus> userlist = new List<UserChapterStatus>();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            UserChapterStatus user = new UserChapterStatus();
                            user.Id = Convert.ToInt32(row["Id"]);
                            user.UserId = Convert.ToInt32(row["userId"]);
                            user.ChapterId = Convert.ToInt32(row["chapterId"]);
                            user.SectionId = Convert.ToInt32(row["sectionId"]);
                            user.ContentVersion = row["contentVersion"].ToString();
                            user.DateCreated = Convert.ToDateTime(row["DateCreated"].ToString());
                            userlist.Add(user);
                        }
                    }

                    con.Close();
                    return userlist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserChapterStatusDAO", "GetChapterStatusByCourseUserId", " Error occurred while Getting User Chapter Status list", ex, logPath);
                throw ex;
            }
        }

        // Get UserTracker object By UserId, Chapterid and section id
        public UserChapterStatus GetUserChapterSectionByUserChapterSectionId(int userId, int chapterId, int sectionId, string cnxnString, string logPath)
        {
            Database db = new Database();

            try
            {
                string cmdText = string.Format(Get_UserChapterStatusByUserChaperSectionId, userId, chapterId, sectionId);
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    DataSet ds = db.SelectDataSet(cmdText, logPath, con);
                    UserChapterStatus userChapterStatus = null;
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            userChapterStatus = new UserChapterStatus();
                            userChapterStatus.Id = Convert.ToInt32(row["Id"]);
                            userChapterStatus.UserId = Convert.ToInt32(row["userId"]);
                            userChapterStatus.ChapterId = Convert.ToInt32(row["chapterId"]);
                            userChapterStatus.SectionId = Convert.ToInt32(row["sectionId"]);
                            userChapterStatus.ContentVersion = row["contentVersion"].ToString();
                            userChapterStatus.DateCreated = Convert.ToDateTime(row["DateCreated"]);
                        }
                    }

                    con.Close();
                    return userChapterStatus;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        public int GetUserMaxChapterSectionIdByUserId(int userId, int courseId, out int sectionId, string cnxnString, string logPath)
        {
            sectionId = 0;
            int chapterId = 0;

            try
            {
                string cmdText;//= string.Format(Get_UserMaxChapterByUserId, userId, courseId);
                if (courseId == 0)
                {
                    cmdText = string.Format(Get_UserLastChapterId, userId);
                }
                else
                {
                    cmdText = string.Format(Get_UserMaxChapterByUserId, userId, courseId);
                }

                //string cmdText = string.Format(Get_UserMaxChapterByUserId, userId, courseId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    DataSet ds = db.SelectDataSet(cmdText, logPath, con);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            if (!string.IsNullOrEmpty(row["chapterId"].ToString()))
                            {
                                chapterId = Convert.ToInt32(row["chapterId"]);
                                //sectionId = Convert.ToInt32(reader["sectionId"]);
                            }
                        }

                        cmdText = string.Format(Get_UserMaxSectionIdByUserIdChapterId, userId, chapterId);

                        ds = db.SelectDataSet(cmdText, logPath, con);

                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                if (!string.IsNullOrEmpty(row["sectionId"].ToString()))
                                {
                                    //chapterId = Convert.ToInt32(reader["chapterId"]);
                                    sectionId = Convert.ToInt32(row["sectionId"]);
                                }
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

            return chapterId;
        }
        #endregion

        #region Insert Statement
        public void AddUserChapterStatus(int userId, int chapterId, int sectionId, string contactVersion, DateTime dateCreated, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string result = string.Format(INSERT_USER_CHAPTER_STATUS, userId, chapterId, sectionId, ParameterFormater.FormatParameter(contactVersion), dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                    Database db = new Database();
                    db.Insert(result, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserChapterStatusDAO", "InsertUserChapterStatus", " Error occurred while saving user chapter status details", ex, logPath);
                throw ex;
            }
            finally
            {
                /* killing sleeping connections */
                Database.KillConnections(cnxnString, logPath);
            }
        }
        #endregion

        #region Update Function
        public void UpdateUserChapterStatus(int id, int userId, int chapterId, int sectionId, string contactVersion, DateTime dateCreated, string cnxnString, string logPath)
        {
            try
            {
                logger.Debug("UserChapterStatusDAO", "UpdateUserChapterStatus", "Preparing Database Query", logPath);
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Update_UserChapterStatus, userId, chapterId, sectionId, ParameterFormater.FormatParameter(contactVersion), dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"), id);
                    Database db = new Database();
                    db.Update(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserChapterStatusDAO", "UpdateUserChapterStatus", "Error Occured while updating UserChapterStatus", ex, logPath);
                throw ex;
            }
        }
        #endregion
    }
}
