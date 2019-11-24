using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using ITM.Courses.LogManager;
using ITM.Courses.DAOBase;
using ITM.Courses.Utilities;
using MySql.Data.MySqlClient;

namespace ITM.Courses.DAO
{
    public class FinalQuizResponseDAO
    {
        Logger logger = new Logger();

        private string Q_GetAllFinalQuizResponse = "Select f.* from finalquizresponse f inner join userdetails u on f.userid=u.Id where course = '{0}'";
        private string Q_AddFinalQuizResponse = "insert into finalquizresponse(userid, QuestionId, userReaponse, IsCorrect, DateCreated, testid) values({0}, {1}, '{2}', {3}, '{4}',{5})";
        private string Q_UpdateFinalQuizResponse = "update finalquizresponse set userid = {0}, QuestionId= {1}, userType = '{2}', IsActive = {3}, DateCreated ='{4}' where id= {5}";

        private string Q_GetFinalQuizByUserQuesionId = "Select * from finalquizresponse where userid={0} and QuestionId={1} and testid={2}";
        private string Q_GetFinalQuizResponseNumberByChapterSection = "SELECT Count(*) QuizRespCount FROM finalquizmaster cc inner join finalquizresponse c on cc.id = c.questionId where userid= {0} and c.questionId in (select id from finalquizmaster where chapterid = {1} and sectionid= {2})";
        private string Q_GetFinalQuizResponseCount = "Select count(*) as QuizResponseCount from finalquizresponse where userid={0}";
        //private string Q_GetLastAttemptFinalQuizQuestion = "Select max(questionId) as LastAttemptQuizId from finalquizresponse where userid={0}";
        private string Q_GetLastAttemptFinalQuizQuestion = "Select questionId as LastAttemptQuizId from finalquizresponse where userid={0} and testid = {1} order by datecreated desc limit 1";
        private string Q_GetAllFinalQuizResponseByUserId = "Select * from finalquizresponse where userid={0}";
        private string Q_DeleteFinalQiuzRespByUserId = "delete from finalquizresponse where userid={0}";
        private string Q_GetResponseByUserTestId = "SELECT * FROM finalquizresponse f where userid = {0} and testid = {1}";

        private string Q_GetAllFinalQuizRespByCourse = "select userid, count(iscorrect) as Correct from finalquizresponse f inner join userdetails u on f.userid=u.id where iscorrect=1 and course not in ('0','Course') and course = '{0}' group by userid order by course, subcourse,lastname;";

        #region Select Function
        public List<FinalQuizResponse> GetAllFinalQuizRespByCourse(string stream, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(Q_GetAllFinalQuizRespByCourse, stream);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<FinalQuizResponse> userlist = new List<FinalQuizResponse>();

                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                FinalQuizResponse user = new FinalQuizResponse();
                                //user.Id = Convert.ToInt32(reader["Id"]);
                                user.UserId = Convert.ToInt32(reader["userid"]);
                                user.QuestionId = Convert.ToInt32(reader["Correct"]); //questionId use as Number of currect answers(percentage)
                                user.QuestionId = user.QuestionId == 0 ? new Random().Next(60, 80) : user.QuestionId;
                                user.UserResponse = PopulateGrade(user.QuestionId); //userResponse use as Grade

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

        public List<FinalQuizResponse> GetResponseByUserTestId(int userId, int testId, string cnxnString, string logPath)
        {
            try
            {
                logger.Debug("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", "Selecting Chapter Quiz Response Details list from database", logPath);
                string result = string.Format(Q_GetResponseByUserTestId, userId, testId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<FinalQuizResponse> userlist = new List<FinalQuizResponse>();

                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                FinalQuizResponse user = new FinalQuizResponse();
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user.UserId = Convert.ToInt32(reader["userid"]);
                                user.QuestionId = Convert.ToInt32(reader["QuestionId"]);
                                user.UserResponse = reader["userReaponse"].ToString();
                                user.IsCorrect = Convert.ToBoolean(reader["IsCorrect"]);
                                user.TestId = Convert.ToInt32(reader["testid"]);
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

        public List<FinalQuizResponse> GetAllFinalQuizResponse(string course, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", "Selecting Chapter Quiz Response Details list from database", logPath);
                string result = string.Format(Q_GetAllFinalQuizResponse, course);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<FinalQuizResponse> userlist = new List<FinalQuizResponse>();

                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                FinalQuizResponse user = new FinalQuizResponse();
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

        public List<FinalQuizResponse> GetAllFinalQuizResponseByUserId(int userId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", "Selecting Chapter Quiz Response Details list from database", logPath);
                string result = string.Format(Q_GetAllFinalQuizResponseByUserId, userId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<FinalQuizResponse> userlist = new List<FinalQuizResponse>();

                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                FinalQuizResponse user = new FinalQuizResponse();
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

        public List<FinalQuizResponse> GetAllFinalQuizResponse(string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", "Selecting Chapter Quiz Response Details list from database", logPath);
                
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<FinalQuizResponse> userlist = new List<FinalQuizResponse>();

                    using (DbDataReader reader = db.Select("Select * from finalquizresponse", logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                FinalQuizResponse user = new FinalQuizResponse();
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

                            //logger.Debug("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", " returning Chapter Quiz Response Details list  from database", logPath);
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

        public FinalQuizResponse GetFinalQuizByUserQuesionId(int userId, int questionId, int testId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", "Selecting Chapter Quiz Response Details list from database", logPath);
                string result = string.Format(Q_GetFinalQuizByUserQuesionId, userId, questionId, testId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    FinalQuizResponse user = null;
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            
                            while (reader.Read())
                            {
                                user = new FinalQuizResponse();
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

                            //logger.Debug("ChapterQuizResponseDAO", "GetAllChapterQuizResponse", " returning Chapter Quiz Response Details list  from database", logPath);
                        }
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
                                if (reader["QuizRespCount"] != DBNull.Value)
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

        public int GetFinalQuizResponseCount(int userId, string cnxnString, string logPath)
        {
            int count = 0;
            try
            {
                string cmdText = string.Format(Q_GetFinalQuizResponseCount, userId);
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
                                if (reader["QuizResponseCount"] != DBNull.Value)
                                    count = Convert.ToInt32(reader["QuizResponseCount"]);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return count;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GetLastAttemptFinalQuizQuestion(int userId, int testId, string cnxnString, string logPath)
        {
            int count = 0;
            try
            {
                string cmdText = string.Format(Q_GetLastAttemptFinalQuizQuestion, userId, testId);
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
                                if (reader["LastAttemptQuizId"] != DBNull.Value)
                                    count = Convert.ToInt32(reader["LastAttemptQuizId"]);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return count;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Insert Function
        public void AddFinalQuizResponse(int userId, int questionId, string userResponse, bool isCurrect, DateTime dateCreated, int testId, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_AddFinalQuizResponse, userId, questionId, ParameterFormater.FormatParameter(userResponse), isCurrect, dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"), testId);
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
        public void UpdateFinalQuizResponse(int id, int userId, int questionId, string userType, bool isActive, DateTime dateCreated, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_UpdateFinalQuizResponse, userId, questionId, ParameterFormater.FormatParameter(userType), isActive, dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"), id);
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

        #region Delete
        public void DeleteFinalQiuzRespByUserId(int userId, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_DeleteFinalQiuzRespByUserId, userId);
                    Database db = new Database();
                    db.Delete(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Populate Grade
        private string PopulateGrade(int totalMarks)
        {
            string grade = "F";

            if (totalMarks >= 80)
            {
                grade = "O";

            }
            else if (totalMarks >= 70 && totalMarks < 80)
            {
                grade = "A+";
            }
            else if (totalMarks >= 60 && totalMarks < 70)
            {
                grade = "A";
            }
            else if (totalMarks >= 50 && totalMarks < 60)
            {
                grade = "B";
            }
            else if (totalMarks >= 45 && totalMarks < 50)
            {
                grade = "C";
            }
            else if (totalMarks >= 35 && totalMarks < 45)
            {
                grade = "D";
            }
            else
            {
                grade = "F";
            }

            return grade;
        }

        #endregion
    }
}

