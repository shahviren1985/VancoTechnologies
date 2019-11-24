using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using ITM.Courses.LogManager;
using ITM.Courses.DAOBase;
using ITM.Courses.Utilities;
using System.Data;
using MySql.Data.MySqlClient;

namespace ITM.Courses.DAO
{
    public class ChapterQuizResponseDAO
    {
        Logger logger = new Logger();

        private string Select_All_Chapter_Quiz_Response = "Select * from chapterquizresponse";
        private string Add_ChapterQuizResponse = "insert into chapterquizresponse(userid, QuestionId, userReaponse, IsCorrect, DateCreated) values({0}, {1}, '{2}', {3}, '{4}')";
        private string Update_ChapterQuizResponse = "update chapterquizresponse set userid = {0}, QuestionId= {1}, userType = '{2}', IsActive = {3}, DateCreated ='{4}' where id= {5}";

        private string Get_ChapterQuizByUserQuesionId = "Select * from chapterquizresponse where userid={0} and QuestionId={1}";
        private string Get_QuizResponceNumberByChapterSection = "SELECT Count(*) QuizRespCount FROM chapterquizmaster cc inner join chapterquizresponse c on cc.id = c.questionId where userid= {0} and c.questionId in (select id from chapterquizmaster where chapterid = {1} and sectionid= {2})";
        private string Get_ChapterQuizByUserId = "Select * from chapterquizresponse where userid={0}";
        private string Get_DistinctChapterIdsbyUserId = "Select distinct ch.chapterid from chapterquizmaster ch inner join chapterquizresponse cr on ch.id = cr.questionId where userid={0}";


        #region Select Function
        public List<ChapterQuizResponse> GetChapterQuizResponseByUserId(int userId, string cnxnString, string logPath)
        {
            try
            {

                string result = string.Format(Get_ChapterQuizByUserId, userId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<ChapterQuizResponse> userlist = new List<ChapterQuizResponse>();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChapterQuizResponse user = new ChapterQuizResponse();
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
                        }
                    }

                    con.Close();
                    return userlist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", " Error occurred while Getting Chapter Quiz Response list", ex, logPath);
                throw new Exception("11377");
            }
        }

        public List<ChapterQuizResponse> GetAllChapterQuizResponse(string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", "Selecting Chapter Quiz Response Details list from database", logPath);
                string result = Select_All_Chapter_Quiz_Response;
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<ChapterQuizResponse> userlist = new List<ChapterQuizResponse>();

                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChapterQuizResponse user = new ChapterQuizResponse();
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
                        }
                    }

                    con.Close();
                    return userlist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", " Error occurred while Getting Chapter Quiz Response list", ex, logPath);
                throw new Exception("11377");
            }
        }

        public List<ChapterQuizResponse> GetAllChapterQuizResponseByUserCourse(int courseId, int userId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", "Selecting Chapter Quiz Response Details list from database", logPath);
                string result = string.Format("Select * from chapterquizresponse cr inner join chapterquizmaster cq on cr.QuestionId = cq.id where cq.courseid = {0} and cr.userid={1}", courseId, userId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<ChapterQuizResponse> userlist = new List<ChapterQuizResponse>();
                    DataSet ds = db.SelectDataSet(result, logPath, con);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            ChapterQuizResponse user = new ChapterQuizResponse();
                            user.Id = Convert.ToInt32(row["Id"]);
                            user.UserId = Convert.ToInt32(row["userid"]);
                            user.QuestionId = Convert.ToInt32(row["QuestionId"]);
                            user.UserResponse = row["userReaponse"].ToString();
                            user.IsCorrect = Convert.ToBoolean(row["IsCorrect"]);
                            user.DateTime = Convert.ToDateTime(row["DateCreated"].ToString());

                            userlist.Add(user);
                        }

                        //logger.Debug("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", " returning Chapter Quiz Response Details list  from database", logPath);
                    }

                    con.Close();
                    return userlist;
                }

            }
            catch (Exception ex)
            {
                logger.Error("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", " Error occurred while Getting Chapter Quiz Response list", ex, logPath);
                throw new Exception("11377");
            }
        }

        public ChapterQuizResponse GetChapterQuizByUserQuesionId(int userId, int questionId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", "Selecting Chapter Quiz Response Details list from database", logPath);
                string result = string.Format(Get_ChapterQuizByUserQuesionId, userId, questionId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    ChapterQuizResponse user = null;

                    DataSet ds = db.SelectDataSet(result, logPath, con);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            user = new ChapterQuizResponse();
                            user.Id = Convert.ToInt32(row["Id"]);
                            user.UserId = Convert.ToInt32(row["userid"]);
                            user.QuestionId = Convert.ToInt32(row["QuestionId"]);
                            user.UserResponse = row["userReaponse"].ToString();
                            user.IsCorrect = Convert.ToBoolean(row["IsCorrect"]);
                            user.DateTime = Convert.ToDateTime(row["DateCreated"].ToString());
                        }
                        //logger.Debug("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", " returning Chapter Quiz Response Details list  from database", logPath);
                    }

                    con.Close();
                    return user;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", " Error occurred while Getting Chapter Quiz Response list", ex, logPath);
                throw new Exception("11377");
            }
        }

        public int GetQuizResponceCountByChapterSectionId(int userId, int chapterId, int sectionId, string cnxnString, string logPath)
        {
            int quizCount = 0;

            try
            {
                string cmdText = string.Format(Get_QuizResponceNumberByChapterSection, userId, chapterId, sectionId);
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
                throw;
            }

            return quizCount;
        }

        #endregion

        #region Chart Related Functions
        public List<int> GetDistinctChapterIdsbyUserId(int userId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", "Selecting Chapter Quiz Response Details list from database", logPath);
                string result = string.Format(Get_DistinctChapterIdsbyUserId, userId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<int> chapterIds = new List<int>();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                int chapterId = Convert.ToInt32(reader["chapterid"]);
                                chapterIds.Add(chapterId);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }

                        }
                    }

                    con.Close();
                    return chapterIds;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", " Error occurred while Getting Chapter Quiz Response list", ex, logPath);
                throw new Exception("11377");
            }
        }
        #endregion

        #region Insert Function
        public void AddChaptersQuizResponse(int userId, int questionId, string userResponse, bool isCurrect, DateTime dateCreated, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Add_ChapterQuizResponse, userId, questionId, ParameterFormater.FormatParameter(userResponse), isCurrect, dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                    Database db = new Database();
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
        public void UpdateChapterQuizResponse(int id, int userId, int questionId, string userType, bool isActive, DateTime dateCreated, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Update_ChapterQuizResponse, userId, questionId, ParameterFormater.FormatParameter(userType), isActive, dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"), id);
                    Database db = new Database();
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
