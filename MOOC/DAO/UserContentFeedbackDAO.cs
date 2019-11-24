using ITM.Courses.DAOBase;
using ITM.Courses.LogManager;
using ITM.Courses.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;


namespace ITM.Courses.DAO
{
    public class UserContentFeedbackDAO
    {
        Logger logger = new Logger();
        private string Select_All_Users = "Select * from usercontentfeedback";

        private string INSERT_USER_FEEDBACK = "INSERT INTO usercontentfeedback (userId,chapterDetailsId,Feedback,DateCreated) VALUES ('{0}','{1}','{2}','{3}')";

        private string Update_UserContentFeedback = "update usercontentfeedback set userId = {0}, chapterDetailsId = {1}, Feedback='{2}', DateCreated ='{3}' where id='{4}'";


        #region Select Function
        public List<UserContentFeedBack> GetAllUsersFeedback(string cnxnString, string logPath)
        {
            try
            {
                logger.Debug("UserContentFeedbackDAO", "GetAllUsersFeedback", "Selecting Users Feedback Details list from database", logPath);
                string result = Select_All_Users;
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserContentFeedBack> userlist = new List<UserContentFeedBack>();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UserContentFeedBack user = new UserContentFeedBack();
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user.UserId = Convert.ToInt32(reader["userId"]);
                                user.ChapterDetailsId = Convert.ToInt32(reader["chapterDetailsId"]);
                                user.Feedback = reader["Feedback"].ToString();
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
                logger.Error("UserContentFeedbackDAO", "GetAllUsersFeedback", " Error occurred while Users Feedback list", ex, logPath);
                throw ex;
            } 
        }

        #endregion

        #region Insert Statement
        public void InsertUserFeedback(int userId, int chapterDetailId, string feedBack, DateTime dateCreated, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string result = string.Format(INSERT_USER_FEEDBACK, userId, chapterDetailId, ParameterFormater.FormatParameter(feedBack), dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                    Database db = new Database();
                    db.Insert(result, logPath, con);
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                logger.Error("UserContentFeedbackDAO", "InsertUserFeedback", " Error occurred while user feeback details", ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Update Function
        public void UpdateUserContentFeedback(int id, int userId, int chapterDetailsId, string feedBack, DateTime dateCreated, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Update_UserContentFeedback, userId, chapterDetailsId, ParameterFormater.FormatParameter(feedBack), dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"), id);
                    Database db = new Database();
                    db.Update(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserContentFeedbackDAO", "UpdateUserContentFeedback", "Error Occured while updating UserContentFeedback", ex, logPath);
                throw ex;
            }
        }
        #endregion

    }
}
