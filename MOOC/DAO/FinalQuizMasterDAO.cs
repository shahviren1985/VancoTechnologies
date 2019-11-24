using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using ITM.Courses.DAOBase;
using ITM.Courses.Utilities;
using ITM.Courses.LogManager;
using MySql.Data.MySqlClient;

namespace ITM.Courses.DAO
{
    public class FinalQuizMasterDAO
    {
        Logger logger = new Logger();
        private string Q_AddFinalQuiz = "insert into finalquizmaster(courseId, chapterId, sectionId, groupNo,complexity,questionText,IsQuestionOptionPresent,QuestionOption,AnswerOption,ContentVersion,DateCreated,createdby,MigratedFinalQuizId)" +
                                        " values({0},{1},{2},{3},{4},'{5}',{6},'{7}','{8}','{9}','{10}','{11}',{12})";
        private string Q_GetAllFinalQuiz = "Select * from finalquizmaster";
        private string Q_GetFinalQuizByQuizId = "Select * from finalquizmaster where id = {0}";
        private string Q_GetFinalQuizCount = "Select count(*) as QuizCount from finalquizmaster";
        private string Q_UpdateFinalQuiz = "Update finalquizmaster set courseId = {0}, chapterId = {1}, sectionId = {2}, groupNo = {3}, complexity = {4}, questionText = '{5}', isQuestionOptionPresent = {6}, questionOption = '{7}', answerOption = '{8}', contentVersion = '{9}', DateCreated = '{10}', createdBy = '{11}' where id = {12}";
        private string Q_GetQuizbyTest = "Select * from finalquizmaster where chapterid in ({0})";
        private string Q_DeleteFinalQuiz = "delete from finalquizmaster where id={0}";
        private string Q_GetUsersResponseByTest = "Select fq.*, fr.* from finalquizmaster fq inner join finalquizresponse fr on fq.id = fr.questionid where chapterid in ({0}) and userid = {1} and testid={2}";
        private string Q_GetAllQuizsByCourse = "Select * from finalquizmaster where courseId={0}";

        #region Insert Function
        public void AddFinalQuiz(int courseId, int chapterId, int sectionId, int groupNo, int complexity, string questionText, bool isQuizOptionPresent, string questionOption, string answerOption, string contentVersion, string createdBy, int migratedFinalQuizId,string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    
                    string cmdText = string.Format(Q_AddFinalQuiz, courseId, chapterId, sectionId, groupNo, complexity, ParameterFormater.FormatParameter(questionText), isQuizOptionPresent,
                    ParameterFormater.FormatParameter(questionOption), ParameterFormater.FormatParameter(answerOption), ParameterFormater.FormatParameter(contentVersion), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                    ParameterFormater.FormatParameter(createdBy), migratedFinalQuizId);
                    
                    Database db = new Database();
                    db.Insert(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select Functions

        public List<FinalQuizMaster> GetQuizUsersResponseByTest(int testId, int userId, string chapterIds, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetUsersResponseByTest, chapterIds, userId, testId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<FinalQuizMaster> finalQuizs = new List<FinalQuizMaster>();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                FinalQuizMaster finalQuiz = new FinalQuizMaster();
                                finalQuiz.Id = Convert.ToInt32(reader["Id"]);
                                finalQuiz.CourseId = Convert.ToInt32(reader["courseId"]);
                                finalQuiz.ChapterId = Convert.ToInt32(reader["chapterId"]);
                                finalQuiz.SectionId = Convert.ToInt32(reader["sectionId"]);
                                finalQuiz.GroupNo = Convert.ToInt32(reader["groupNo"]);
                                finalQuiz.Complexity = Convert.ToInt32(reader["complexity"]);
                                finalQuiz.QuestionText = ParameterFormater.UnescapeXML(reader["questionText"].ToString());
                                finalQuiz.IsQuestionOptionPresent = Convert.ToBoolean(reader["IsQuestionOptionPresent"]);
                                finalQuiz.QuestionOption = ParameterFormater.UnescapeXML(reader["QuestionOption"].ToString());
                                finalQuiz.AnswerOption = ParameterFormater.UnescapeXML(reader["AnswerOption"].ToString());
                                finalQuiz.ContentVersion = reader["ContentVersion"].ToString();
                                finalQuiz.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                finalQuiz.CreatedBy = reader["createdby"].ToString();
                                //getting user-response
                                finalQuiz.IsAnsGiven = true;
                                finalQuiz.IsCorrect = Convert.ToBoolean(reader["iscorrect"]);
                                finalQuiz.AnswerText = reader["userreaponse"].ToString();
                                // property for migration
                                finalQuiz.MigratedFinalQuizId = Convert.ToInt32(reader["MigratedFinalQuizId"]);

                                finalQuizs.Add(finalQuiz);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return finalQuizs;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }


        public List<FinalQuizMaster> GetAllQuizs(string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetAllFinalQuiz);
                Database db = new Database();
                List<FinalQuizMaster> finalQuizs = new List<FinalQuizMaster>();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();

                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                FinalQuizMaster finalQuiz = new FinalQuizMaster();
                                finalQuiz.Id = Convert.ToInt32(reader["Id"]);
                                finalQuiz.CourseId = Convert.ToInt32(reader["courseId"]);
                                finalQuiz.ChapterId = Convert.ToInt32(reader["chapterId"]);
                                finalQuiz.SectionId = Convert.ToInt32(reader["sectionId"]);
                                finalQuiz.GroupNo = Convert.ToInt32(reader["groupNo"]);
                                finalQuiz.Complexity = Convert.ToInt32(reader["complexity"]);
                                finalQuiz.QuestionText = ParameterFormater.UnescapeXML(reader["questionText"].ToString());
                                finalQuiz.IsQuestionOptionPresent = Convert.ToBoolean(reader["IsQuestionOptionPresent"]);
                                finalQuiz.QuestionOption = ParameterFormater.UnescapeXML(reader["QuestionOption"].ToString());
                                finalQuiz.AnswerOption = ParameterFormater.UnescapeXML(reader["AnswerOption"].ToString());
                                finalQuiz.ContentVersion = reader["ContentVersion"].ToString();
                                finalQuiz.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                finalQuiz.CreatedBy = reader["createdby"].ToString();
                                // property for migration
                                finalQuiz.MigratedFinalQuizId = Convert.ToInt32(reader["MigratedFinalQuizId"]);

                                finalQuizs.Add(finalQuiz);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return finalQuizs;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FinalQuizMaster> GetAllQuizsByCourse(int courseId, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetAllQuizsByCourse, courseId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<FinalQuizMaster> finalQuizs = new List<FinalQuizMaster>();

                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                FinalQuizMaster finalQuiz = new FinalQuizMaster();
                                finalQuiz.Id = Convert.ToInt32(reader["Id"]);
                                finalQuiz.CourseId = Convert.ToInt32(reader["courseId"]);
                                finalQuiz.ChapterId = Convert.ToInt32(reader["chapterId"]);
                                finalQuiz.SectionId = Convert.ToInt32(reader["sectionId"]);
                                finalQuiz.GroupNo = Convert.ToInt32(reader["groupNo"]);
                                finalQuiz.Complexity = Convert.ToInt32(reader["complexity"]);
                                finalQuiz.QuestionText = ParameterFormater.UnescapeXML(reader["questionText"].ToString());
                                finalQuiz.IsQuestionOptionPresent = Convert.ToBoolean(reader["IsQuestionOptionPresent"]);
                                finalQuiz.QuestionOption = ParameterFormater.UnescapeXML(reader["QuestionOption"].ToString());
                                finalQuiz.AnswerOption = ParameterFormater.UnescapeXML(reader["AnswerOption"].ToString());
                                finalQuiz.ContentVersion = reader["ContentVersion"].ToString();
                                finalQuiz.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                finalQuiz.CreatedBy = reader["createdby"].ToString();
                                // property for migration
                                finalQuiz.MigratedFinalQuizId = Convert.ToInt32(reader["MigratedFinalQuizId"]);

                                finalQuizs.Add(finalQuiz);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return finalQuizs;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<FinalQuizMaster> GetQuizbyTest(string chapterIds, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetQuizbyTest, chapterIds);
                Database db = new Database();
                List<FinalQuizMaster> finalQuizs = new List<FinalQuizMaster>();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                FinalQuizMaster finalQuiz = new FinalQuizMaster();
                                finalQuiz.Id = Convert.ToInt32(reader["Id"]);
                                finalQuiz.CourseId = Convert.ToInt32(reader["courseId"]);
                                finalQuiz.ChapterId = Convert.ToInt32(reader["chapterId"]);
                                finalQuiz.SectionId = Convert.ToInt32(reader["sectionId"]);
                                finalQuiz.GroupNo = Convert.ToInt32(reader["groupNo"]);
                                finalQuiz.Complexity = Convert.ToInt32(reader["complexity"]);
                                finalQuiz.QuestionText = ParameterFormater.UnescapeXML(reader["questionText"].ToString());
                                finalQuiz.IsQuestionOptionPresent = Convert.ToBoolean(reader["IsQuestionOptionPresent"]);
                                finalQuiz.QuestionOption = ParameterFormater.UnescapeXML(reader["QuestionOption"].ToString());
                                finalQuiz.AnswerOption = ParameterFormater.UnescapeXML(reader["AnswerOption"].ToString());
                                finalQuiz.ContentVersion = reader["ContentVersion"].ToString();
                                finalQuiz.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                finalQuiz.CreatedBy = reader["createdby"].ToString();
                                // property for migration
                                finalQuiz.MigratedFinalQuizId = Convert.ToInt32(reader["MigratedFinalQuizId"]);

                                finalQuizs.Add(finalQuiz);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return finalQuizs;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FinalQuizMaster GetFinalQuizByQuizId(int id, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetFinalQuizByQuizId, id);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    FinalQuizMaster finalQuiz = new FinalQuizMaster();

                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                finalQuiz.Id = Convert.ToInt32(reader["Id"]);
                                finalQuiz.CourseId = Convert.ToInt32(reader["courseId"]);
                                finalQuiz.ChapterId = Convert.ToInt32(reader["chapterId"]);
                                finalQuiz.SectionId = Convert.ToInt32(reader["sectionId"]);
                                finalQuiz.GroupNo = Convert.ToInt32(reader["groupNo"]);
                                finalQuiz.Complexity = Convert.ToInt32(reader["complexity"]);
                                finalQuiz.QuestionText = ParameterFormater.UnescapeXML(reader["questionText"].ToString());
                                finalQuiz.IsQuestionOptionPresent = Convert.ToBoolean(reader["IsQuestionOptionPresent"].ToString());
                                finalQuiz.QuestionOption = ParameterFormater.UnescapeXML(reader["QuestionOption"].ToString());
                                finalQuiz.AnswerOption = ParameterFormater.UnescapeXML(reader["AnswerOption"].ToString());
                                finalQuiz.ContentVersion = reader["ContentVersion"].ToString();
                                finalQuiz.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                finalQuiz.CreatedBy = reader["createdby"].ToString();

                                // property for migration
                                finalQuiz.MigratedFinalQuizId = Convert.ToInt32(reader["MigratedFinalQuizId"]);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return finalQuiz;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetFinalQuizCount(string cnxnString, string logPath)
        {
            int count = 0;
            try
            {
                string cmdText = string.Format(Q_GetFinalQuizCount);
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
                                if (reader["QuizCount"] != DBNull.Value)
                                    count = Convert.ToInt32(reader["QuizCount"]);
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

        #region Update Function
        public void UpdateFinalQuiz(int courseId, int chapterId, int sectionId, int groupNo, int complexity, string questionText, bool isQuestionOptionPresent, string questionOption, string answerOption, string contentVersion, string createdBy, int queId, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_UpdateFinalQuiz, courseId, chapterId, sectionId, groupNo, complexity, ParameterFormater.FormatParameter(questionText), isQuestionOptionPresent, ParameterFormater.FormatParameter(questionOption), ParameterFormater.FormatParameter(answerOption), ParameterFormater.FormatParameter(contentVersion), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), ParameterFormater.FormatParameter(createdBy), queId);
                    Database db = new Database();
                    db.Update(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("FinalQuizMasterDAO", "UpdateFinalQuiz", "Error Occured while updating question Details", ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Delete Function
        public void DeleteFinalQuiz(int quizId, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_DeleteFinalQuiz, quizId);
                    Database db = new Database();
                    db.Delete(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
