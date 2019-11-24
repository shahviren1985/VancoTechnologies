using ITM.Courses.DAOBase;
using ITM.Courses.LogManager;
using ITM.Courses.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace ITM.Courses.DAO
{
    public class ArchiveFinalQuizResponseDAO
    {
        Logger logger = new Logger();

        private string Q_GetAllFinalQuizResponse = "Select * from archivefinalquizresponse";
        private string Q_AddFinalQuizResponse = "insert into archivefinalquizresponse(userid, QuestionId, userReaponse, IsCorrect, DateCreated) values({0}, {1}, '{2}', {3}, '{4}')";
        private string Q_UpdateFinalQuizResponse = "update archivefinalquizresponse set userid = {0}, QuestionId= {1}, userType = '{2}', IsActive = {3}, DateCreated ='{4}' where id= {5}";

        private string Q_GetFinalQuizByUserQuesionId = "Select * from archivefinalquizresponse where userid={0} and QuestionId={1}";
        private string Q_GetFinalQuizResponseNumberByChapterSection = "SELECT Count(*) QuizRespCount FROM finalquizmaster cc inner join archivefinalquizresponse c on cc.id = c.questionId where userid= {0} and c.questionId in (select id from finalquizmaster where chapterid = {1} and sectionid= {2})";

        #region Select Function
        public List<ArchiveFinalQuizResponse> GetAllFinalQuizResponse(string cnxnString, string logPath)
        {
            try
            {
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(Q_GetAllFinalQuizResponse, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            List<ArchiveFinalQuizResponse> userlist = new List<ArchiveFinalQuizResponse>();
                            while (reader.Read())
                            {
                                ArchiveFinalQuizResponse user = new ArchiveFinalQuizResponse();
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user.UserId = Convert.ToInt32(reader["userid"]);
                                user.QuestionId = Convert.ToInt32(reader["QuestionId"]);
                                user.UserResponse = reader["userReaponse"].ToString();
                                user.IsCorrect = Convert.ToBoolean(reader["IsCorrect"]);
                                user.DateTime = Convert.ToDateTime(reader["DateCreated"].ToString());
                                userlist.Add(user);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }

                            return userlist;
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", " Error occurred while Getting Chapter Quiz Response list", ex, logPath);
                throw new Exception("11377");
            }

            return null;
        }

        public ArchiveFinalQuizResponse GetFinalQuizByUserQuesionId(int userId, int questionId, string cnxnString, string logPath)
        {
            try
            {
                
                string result = string.Format(Q_GetFinalQuizByUserQuesionId, userId, questionId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            ArchiveFinalQuizResponse user = new ArchiveFinalQuizResponse();
                            while (reader.Read())
                            {
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user.UserId = Convert.ToInt32(reader["userid"]);
                                user.QuestionId = Convert.ToInt32(reader["QuestionId"]);
                                user.UserResponse = reader["userReaponse"].ToString();
                                user.IsCorrect = Convert.ToBoolean(reader["IsCorrect"]);
                                user.DateTime = Convert.ToDateTime(reader["DateCreated"].ToString());
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }

                            return user;
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", " Error occurred while Getting Chapter Quiz Response list", ex, logPath);
                throw new Exception("11377");
            }

            return null;
        }

        public int GetFinalQuizResponceCountByChapterSectionId(int userId, int chapterId, int sectionId, string cnxnString, string logPath)
        {
            int quizCount = 0;

            try
            {
                string cmdText = string.Format(Q_GetFinalQuizResponseNumberByChapterSection, userId, chapterId, sectionId);
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
                                quizCount = Convert.ToInt32(reader["QuizRespCount"]);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
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

            return quizCount;
        }

        #endregion

        #region Insert Function
        public void AddFinalQuizResponse(int userId, int questionId, string userResponse, bool isCurrect, DateTime dateCreated, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_AddFinalQuizResponse, userId, questionId, ParameterFormater.FormatParameter(userResponse), isCurrect, dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                Database db = new Database();
                
                using(MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    db.Insert(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterQuizResponseDAO", "AddChaptersQuizResponse", ex.Message, ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Update Function
        public void UpdateFinalQuizResponse(int id, int userId, int questionId, string userType, bool isActive, DateTime dateCreated, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_UpdateFinalQuizResponse, userId, questionId, ParameterFormater.FormatParameter(userType), isActive, dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"), id);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    db.Update(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterQuizResponseDAO", "UpdateChapterQuizResponse", "Error Occured while updating Chapter Details", ex, logPath);
                throw ex;
            }
        }
        #endregion
    }
}
