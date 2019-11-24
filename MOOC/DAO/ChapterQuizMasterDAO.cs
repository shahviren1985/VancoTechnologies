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
    public class ChapterQuizMasterDAO
    {
        Logger logger = new Logger();
        private string Select_Chapter_Quiz_Master = "Select * from chapterquizmaster";
        private string Select_Chapter_Quiz_MasterByChapterSectionId = "Select * from chapterquizmaster where chapterId={0} and sectionId={1}";
        private string Select_Chapter_Quiz_MasterByChapterId = "Select * from chapterquizmaster where chapterId={0}";
        private string Add_ChapterQuizMaster = "insert into chapterquizmaster(courseId, chapterId, sectionId, questionText, IsQuestionOptionPresent, QuestionOption, AnswerOption, ContentVersion, DateCreated, createdby,MigratedChapterQuizId) " +
                                                "values({0}, {1}, {2}, '{3}', {4}, '{5}', '{6}', '{7}', '{8}', '{9}',{10})";
        private string Update_ChapterQuizMaster = "update chapterquizmaster set courseId = {0}, chapterId= {1}, sectionId = {2}, questionText = '{3}', IsQuestionOptionPresent={4}, QuestionOption='{5}', AnswerOption='{6}', ContentVersion='{7}', DateCreated='{8}', createdby='{9}' where id={10}";
        private string Get_QuizNumberByChapterSection = "SELECT count(*) as QuizCount FROM chapterquizmaster where chapterid ={0} and sectionid={1}";

        private string Get_Questions_By_courseId = "SELECT c.id as questionId,c.courseid,c.questionText,cd.title,cs.title as sectitle, MigratedFinalQuizId FROM finalquizmaster c inner join chapterdetails cd on c.chapterid = cd.id inner join chaptersection cs on c.sectionId = cs.id where c.courseId = {0} order by cd.title";

        //private string Get_Chapter_and_section_by_question_and_courseId = "SELECT cd.chaptername,cs.sectionName,c.questionText,c.IsQuestionOptionPresent,c.QuestionOption,c.AnswerOption FROM chapterquizmaster c"
        // + "inner join chapterdetails cd on c.chapterid = cd.id"
        // + "inner join chaptersection cs on c.sectionId = cs.id where c.id = {0} and c.CourseId = {1}";
        private string Select_Question_By_questionId = "Select * from finalquizmaster where id = {0}";

        private string Q_GetChapterQuestionByQuestionText = "Select * from chapterquizmaster where questionText='{0}' and chapterId={1} and sectionId={2}";

        private string Q_GetAllChapterQuizsByCourse = "Select * from chapterquizmaster where courseId={0}";

        #region Select Function

        public List<ChapterQuizMaster> GetAllChapterQuizsByCourse(int courseId, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetAllChapterQuizsByCourse, courseId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();

                    DataSet ds = db.SelectDataSet(cmdText, logPath, con);
                    List<ChapterQuizMaster> chapterQuizs = new List<ChapterQuizMaster>();

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            ChapterQuizMaster chapterQuiz = new ChapterQuizMaster();
                            chapterQuiz.Id = Convert.ToInt32(row["Id"]);
                            chapterQuiz.CourseId = Convert.ToInt32(row["courseId"]);
                            chapterQuiz.ChapterId = Convert.ToInt32(row["chapterId"]);
                            chapterQuiz.SectionId = Convert.ToInt32(row["sectionId"]);

                            chapterQuiz.QuestionText = ParameterFormater.UnescapeXML(row["questionText"].ToString());
                            chapterQuiz.IsQuestionOptionPresent = Convert.ToBoolean(row["IsQuestionOptionPresent"]);
                            chapterQuiz.QuestionOption = ParameterFormater.UnescapeXML(row["QuestionOption"].ToString());
                            chapterQuiz.AnswerOption = ParameterFormater.UnescapeXML(row["AnswerOption"].ToString());
                            chapterQuiz.ContentVersion = row["ContentVersion"].ToString();
                            chapterQuiz.DateCreated = Convert.ToDateTime(row["DateCreated"].ToString());
                            chapterQuiz.CreatedBy = row["createdby"].ToString();
                            // property for migration
                            chapterQuiz.MigratedChapterQuizId = Convert.ToInt32(row["MigratedChapterQuizId"]);

                            chapterQuizs.Add(chapterQuiz);
                        }
                    }

                    con.Close();
                    return chapterQuizs;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ChapterQuizMaster GetChapterQuestionByQuestionText(string questionText, int chapterId, int sectionId, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(Q_GetChapterQuestionByQuestionText, ParameterFormater.FormatParameter(questionText), chapterId, sectionId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    ChapterQuizMaster question = new ChapterQuizMaster();

                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                question.Id = Convert.ToInt32(reader["Id"]);
                                question.CourseId = Convert.ToInt32(reader["courseId"]);
                                question.ChapterId = Convert.ToInt32(reader["chapterId"]);
                                question.SectionId = Convert.ToInt32(reader["sectionId"]);
                                question.QuestionText = ParameterFormater.UnescapeXML(reader["questionText"].ToString());
                                question.IsQuestionOptionPresent = Convert.ToBoolean(reader["IsQuestionOptionPresent"]);
                                question.QuestionOption = ParameterFormater.UnescapeXML(reader["QuestionOption"].ToString());
                                question.AnswerOption = ParameterFormater.UnescapeXML(reader["AnswerOption"].ToString());
                                question.ContentVersion = reader["ContentVersion"].ToString();
                                question.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                question.CreatedBy = ParameterFormater.UnescapeXML(reader["createdby"].ToString());
                                //property for migration
                                question.MigratedChapterQuizId = Convert.ToInt32(reader["MigratedChapterQuizId"]);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return question;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterQuizMasterDAO", "GetQuestionsByCourse", " Error occurred while Getting Questions list", ex, logPath);
                throw new Exception("11377");
            }
        }

        public List<ChapterQuizMaster> GetChapterQuizMasterDetails(string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterQuizMasterDAO", "GetChapterQuizMasterDetails", "Selecting User Details list from database", logPath);
                string result = Select_Chapter_Quiz_Master;
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<ChapterQuizMaster> userlist = new List<ChapterQuizMaster>();

                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChapterQuizMaster user = new ChapterQuizMaster();
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user.CourseId = Convert.ToInt32(reader["courseId"]);
                                user.ChapterId = Convert.ToInt32(reader["chapterId"]);
                                user.SectionId = Convert.ToInt32(reader["sectionId"]);

                                user.QuestionText = ParameterFormater.UnescapeXML(reader["questionText"].ToString());
                                user.IsQuestionOptionPresent = Convert.ToBoolean(reader["IsQuestionOptionPresent"]);
                                user.QuestionOption = ParameterFormater.UnescapeXML(reader["QuestionOption"].ToString());
                                user.AnswerOption = ParameterFormater.UnescapeXML(reader["AnswerOption"].ToString());
                                user.ContentVersion = reader["ContentVersion"].ToString();
                                user.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                user.CreatedBy = ParameterFormater.UnescapeXML(reader["createdby"].ToString());
                                //property for migration
                                user.MigratedChapterQuizId = Convert.ToInt32(reader["MigratedChapterQuizId"]);

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
                logger.Error("ChapterQuizMasterDAO", "GetChapterQuizMasterDetails", " Error occurred while Getting User list", ex, logPath);
                throw new Exception("11377");
            }
        }

        public List<ChapterQuizMaster> GetChapterQuizByChapterSection(int chapterId, int sectionId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterQuizMasterDAO", "GetChapterQuizMasterDetails", "Selecting User Details list from database", logPath);
                string result = string.Format(Select_Chapter_Quiz_MasterByChapterSectionId, chapterId, sectionId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    DataSet ds = db.SelectDataSet(result, logPath, con);
                    List<ChapterQuizMaster> userlist = new List<ChapterQuizMaster>();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            ChapterQuizMaster user = new ChapterQuizMaster();
                            user.Id = Convert.ToInt32(row["Id"]);
                            user.CourseId = Convert.ToInt32(row["courseId"]);
                            user.ChapterId = Convert.ToInt32(row["chapterId"]);
                            user.SectionId = Convert.ToInt32(row["sectionId"]);

                            user.QuestionText = ParameterFormater.UnescapeXML(row["questionText"].ToString());
                            user.IsQuestionOptionPresent = Convert.ToBoolean(row["IsQuestionOptionPresent"]);
                            user.QuestionOption = ParameterFormater.UnescapeXML(row["QuestionOption"].ToString());
                            user.AnswerOption = ParameterFormater.UnescapeXML(row["AnswerOption"].ToString());
                            user.ContentVersion = row["ContentVersion"].ToString();
                            user.DateCreated = Convert.ToDateTime(row["DateCreated"].ToString());
                            user.CreatedBy = ParameterFormater.UnescapeXML(row["createdby"].ToString());
                            //property for migration
                            user.MigratedChapterQuizId = Convert.ToInt32(row["MigratedChapterQuizId"]);

                            userlist.Add(user);
                        }
                    }

                    con.Close();
                    return userlist;
                }

            }
            catch (Exception ex)
            {
                logger.Error("ChapterQuizMasterDAO", "GetChapterQuizMasterDetails", " Error occurred while Getting User list", ex, logPath);
                throw new Exception("11377");
            }
            finally
            {
                Database.KillConnections(cnxnString, logPath);
            }
        }

        public int GetQuizCountByChapterSectionId(int chapterId, int sectionId, string cnxnString, string logPath)
        {
            int quizCount = 0;

            try
            {
                string cmdText = string.Format(Get_QuizNumberByChapterSection, chapterId, sectionId);
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
                                quizCount = Convert.ToInt32(reader["QuizCount"]);
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

        public List<ChapterQuizMaster> GetChapterQuizByChapterId(int chapterId, string cnxnString, string logPath)
        {
            try
            {
                logger.Debug("ChapterQuizMasterDAO", "GetChapterQuizByChapterId", "Selecting Quiz Details list from database by chapter id", logPath);
                string result = string.Format(Select_Chapter_Quiz_MasterByChapterId, chapterId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<ChapterQuizMaster> questionlist = new List<ChapterQuizMaster>();

                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChapterQuizMaster question = new ChapterQuizMaster();
                                question.Id = Convert.ToInt32(reader["Id"]);
                                question.CourseId = Convert.ToInt32(reader["courseId"]);
                                question.ChapterId = Convert.ToInt32(reader["chapterId"]);
                                question.SectionId = Convert.ToInt32(reader["sectionId"]);

                                question.QuestionText = ParameterFormater.UnescapeXML(reader["questionText"].ToString());
                                question.IsQuestionOptionPresent = Convert.ToBoolean(reader["IsQuestionOptionPresent"]);
                                question.QuestionOption = ParameterFormater.UnescapeXML(reader["QuestionOption"].ToString());
                                question.AnswerOption = ParameterFormater.UnescapeXML(reader["AnswerOption"].ToString());
                                question.ContentVersion = reader["ContentVersion"].ToString();
                                question.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                question.CreatedBy = ParameterFormater.UnescapeXML(reader["createdby"].ToString());
                                //property for migration
                                question.MigratedChapterQuizId = Convert.ToInt32(reader["MigratedChapterQuizId"]);

                                questionlist.Add(question);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }

                            //logger.Debug("ChapterQuizMasterDAO", "GetChapterQuizByChapterId", " returning Quiz Details list  from database", logPath);

                        }
                    }

                    con.Close();
                    return questionlist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterQuizMasterDAO", "GetChapterQuizByChapterId", " Error occurred while Getting Quiz list", ex, logPath);
                throw new Exception("11377");
            }
        }

        public List<ChapterQuizMaster> GetQuestionsByCourse(int CourseId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterQuizMasterDAO", "GetQuestionsByCourse", "Selecting Question Details list from database by course id", logPath);
                string result = string.Format(Get_Questions_By_courseId, CourseId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<ChapterQuizMaster> questionlist = new List<ChapterQuizMaster>();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChapterQuizMaster question = new ChapterQuizMaster();
                                question.Id = Convert.ToInt32(reader["questionId"]);
                                question.CourseId = Convert.ToInt32(reader["courseId"]);
                                question.QuestionText = ParameterFormater.UnescapeXML(reader["questionText"].ToString());
                                question.ChapterName = reader["title"].ToString();
                                question.SectionName = reader["sectitle"].ToString();
                                //property for migration
                                question.MigratedChapterQuizId = Convert.ToInt32(reader["MigratedFinalQuizId"]);

                                questionlist.Add(question);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }

                            //logger.Debug("ChapterQuizMasterDAO", "GetQuestionsByCourse", " returning Questions list  from database", logPath);
                        }
                    }

                    con.Close();
                    return questionlist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterQuizMasterDAO", "GetQuestionsByCourse", " Error occurred while Getting Questions list", ex, logPath);
                throw new Exception("11377");
            }
        }

        public ChapterQuizMaster GetQuestionByQuestionId(int questionId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterQuizMasterDAO", "GetQuestionsByCourse", "Selecting Question Details list from database by course id", logPath);
                string result = string.Format(Select_Question_By_questionId, questionId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    ChapterQuizMaster question = new ChapterQuizMaster();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                question.Id = Convert.ToInt32(reader["Id"]);
                                question.CourseId = Convert.ToInt32(reader["courseId"]);
                                question.ChapterId = Convert.ToInt32(reader["chapterId"]);
                                question.SectionId = Convert.ToInt32(reader["sectionId"]);
                                question.QuestionText = ParameterFormater.UnescapeXML(reader["questionText"].ToString());
                                question.IsQuestionOptionPresent = Convert.ToBoolean(reader["IsQuestionOptionPresent"]);
                                question.QuestionOption = ParameterFormater.UnescapeXML(reader["QuestionOption"].ToString());
                                question.AnswerOption = ParameterFormater.UnescapeXML(reader["AnswerOption"].ToString());
                                question.ContentVersion = reader["ContentVersion"].ToString();
                                question.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                question.CreatedBy = ParameterFormater.UnescapeXML(reader["createdby"].ToString());
                                //property for migration
                                question.MigratedChapterQuizId = Convert.ToInt32(reader["MigratedChapterQuizId"]);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return question;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterQuizMasterDAO", "GetQuestionsByCourse", " Error occurred while Getting Questions list", ex, logPath);
                throw new Exception("11377");
            }
        }

        #endregion

        #region Insert Function
        public void AddChaptersQuizMaster(int courseId, int chapterId, int sectionId, string questionText, bool isQuestionOptionPresent, string questionOption,
            string answerOption, string contentVersion, DateTime dateCreated, string createdBy, int migratedQuizId, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Add_ChapterQuizMaster, courseId, chapterId, sectionId, ParameterFormater.FormatParameter(questionText), isQuestionOptionPresent, questionOption,
                                ParameterFormater.FormatParameter(answerOption), ParameterFormater.FormatParameter(contentVersion), dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"), ParameterFormater.FormatParameter(createdBy), migratedQuizId);

                    Database db = new Database();

                    db.Insert(cmdText, logPath, con);
                    con.Close();
                }


            }
            catch (Exception ex)
            {
                logger.Error("ChapterQuizMasterDAO", "AddChaptersQuizMaster", ex.Message, ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Update Function
        public void UpdateChapterQuizMaster(int id, int courseId, int chapterId, int sectionId, string questionText, bool isQuestionOptionPresent, string questionOption, string answerOption, string contentVersion, DateTime dateCreated, string createdBy, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();

                    Database db = new Database();
                    string cmdText = string.Format(Update_ChapterQuizMaster, courseId, chapterId, sectionId, ParameterFormater.FormatParameter(questionText), isQuestionOptionPresent, ParameterFormater.FormatParameter(questionOption),
                                            ParameterFormater.FormatParameter(answerOption), ParameterFormater.FormatParameter(contentVersion), dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"), ParameterFormater.FormatParameter(createdBy), id);
                    db.Update(cmdText, logPath, con);
                    con.Close();
                }
                //logger.Debug("ChapterQuizMasterDAO", "UpdateChapterQuizMaster", "After Updating Chapter Details", logPath);
            }
            catch (Exception ex)
            {
                logger.Error("ChapterQuizMasterDAO", "UpdateChapterQuizMaster", "Error Occured while updating Chapter Details", ex, logPath);
                throw ex;
            }
        }
        #endregion
    }
}
