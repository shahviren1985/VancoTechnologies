using ITM.Courses.DAOBase;
using ITM.Courses.LogManager;
using ITM.Courses.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace ITM.Courses.DAO
{
    public class ChapterDetailsDAO
    {
        Logger logger = new Logger();
        private string Select_All_Chapter_Details = "Select * from chapterdetails";
        private string Select_Chapter_By_Id = "Select cpd.id, coursename, Title, language, contentversion from chapterdetails cpd inner join coursedetails cd on cpd.courseid=cd.id";
        private string Select_Chapter_Details_By_Chapter_Id = "Select * from chapterdetails where id = {0}";
        private string Add_Chapters = "insert into chapterdetails(CourseId,Language,ContentVersion,PageName,Title,DateCreated, Link,Time,IsDB,OrignalName) values({0},'{1}','{2}','{3}','{4}','{5}','{6}',{7},{8},'{4}')";
        private string Select_Latest_Content_Version = "select * from contentversion";
        private string Update_Chapters = "update chapterdetails set CourseId = {0}, Language= '{1}', ContentVersion = '{2}', PageName = '{3}', Title='{4}', DateCreated='{5}' where id={6}";
        private string SelectMaxChapterId = "select max(id) as ChapterId from chapterdetails";
        private string Q_GetChapterByTwoChapterIds = "SELECT * FROM chapterdetails c where id between {0} and {1}";
        private string Select_chater_by_course_id = "SELECT * FROM chapterdetails c where Courseid = {0}";

        private string Q_DeleteChapterById = "delete from chapterdetails where id={0}";
        //dynamic-course
        private string Q_GetAllChaptersByCourse = "Select * from chapterdetails where courseid={0}";
        private string Q_Update_ChaptersName = "update chapterdetails set title = '{0}' where id = {1}";
        private string Q_GetChaptersByIds = "Select * from chapterdetails where id in ({0})";
        private string Q_GetChaptersBySecitonId = "select * from chapterdetails where id =(select chapterid from chaptersection where id= {0})";

        #region Select Function
        public List<ChapterDetails> GetAllChapterDetails(string cnxnString, string logPath)
        {
            try
            {
                string result = Select_All_Chapter_Details;
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<ChapterDetails> userlist = new List<ChapterDetails>();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChapterDetails user = new ChapterDetails();
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user.CourseId = Convert.ToInt32(reader["CourseId"]);

                                user.Language = ParameterFormater.UnescapeXML(reader["Language"].ToString());
                                user.ContentVersion = ParameterFormater.UnescapeXML(reader["ContentVersion"].ToString());
                                user.PageName = ParameterFormater.UnescapeXML(reader["PageName"].ToString());
                                user.Title = ParameterFormater.UnescapeXML(reader["Title"].ToString());

                                user.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                user.Link = reader["Link"].ToString();
                                user.Time = Convert.ToInt32(reader["Time"]);
                                user.IsDB = Convert.ToBoolean(reader["IsDB"]);

                                user.MigratedChapterId = Convert.ToInt32(reader["MigratedChapterId"]);
                                user.OrignalName = reader["OrignalName"].ToString();

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
                logger.Error("ChapterDetailsDAO", "GetAllChapterDetails", " Error occurred while Getting Chapter Details list", ex, logPath);
                throw ex;
            }
        }

        public List<ChapterDetails> GetChaptersByIds(string chapterIds, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterDetailsDAO", "GetAllChapterDetails", "Selecting Chapter Details list from database", logPath);
                string result = string.Format(Q_GetChaptersByIds, chapterIds);

                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<ChapterDetails> userlist = new List<ChapterDetails>();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ChapterDetails user = new ChapterDetails();
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user.CourseId = Convert.ToInt32(reader["CourseId"]);
                                user.Language = ParameterFormater.UnescapeXML(reader["Language"].ToString());
                                user.ContentVersion = ParameterFormater.UnescapeXML(reader["ContentVersion"].ToString());
                                user.PageName = ParameterFormater.UnescapeXML(reader["PageName"].ToString());
                                user.Title = ParameterFormater.UnescapeXML(reader["Title"].ToString());
                                user.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                user.Link = reader["Link"].ToString();
                                user.Time = Convert.ToInt32(reader["Time"]);
                                user.IsDB = Convert.ToBoolean(reader["IsDB"]);
                                // properties for migration
                                user.MigratedChapterId = Convert.ToInt32(reader["MigratedChapterId"]);
                                user.OrignalName = reader["OrignalName"].ToString();
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
                logger.Error("ChapterDetailsDAO", "GetAllChapterDetails", " Error occurred while Getting Chapter Details list", ex, logPath);
                throw ex;
            }
        }

        public ChapterDetails GetChapterBySecitonId(int sectionId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterDetailsDAO", "GetAllChapterDetails", "Selecting Chapter Details list from database", logPath);
                string result = string.Format(Q_GetChaptersBySecitonId, sectionId);

                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    ChapterDetails user = new ChapterDetails();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user.CourseId = Convert.ToInt32(reader["CourseId"]);
                                user.Language = ParameterFormater.UnescapeXML(reader["Language"].ToString());
                                user.ContentVersion = ParameterFormater.UnescapeXML(reader["ContentVersion"].ToString());
                                user.PageName = ParameterFormater.UnescapeXML(reader["PageName"].ToString());
                                user.Title = ParameterFormater.UnescapeXML(reader["Title"].ToString());
                                user.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                user.Link = reader["Link"].ToString();
                                user.Time = Convert.ToInt32(reader["Time"]);
                                user.IsDB = Convert.ToBoolean(reader["IsDB"]);
                                user.MigratedChapterId = Convert.ToInt32(reader["MigratedChapterId"]);
                                user.OrignalName = reader["OrignalName"].ToString();
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }
                    con.Close();
                    return user;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterDetailsDAO", "GetAllChapterDetails", " Error occurred while Getting Chapter Details list", ex, logPath);
                throw ex;
            }
        }

        public List<ChapterDetails> GetAllChaptersByCourse(int courseId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterDetailsDAO", "GetAllChapterDetails", "Selecting Chapter Details list from database", logPath);
                string result = string.Format(Q_GetAllChaptersByCourse, courseId);

                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    DataSet ds = db.SelectDataSet(result, logPath, con);
                    List<ChapterDetails> userlist = new List<ChapterDetails>();
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            ChapterDetails user = new ChapterDetails();
                            user.Id = Convert.ToInt32(row["Id"]);
                            user.CourseId = Convert.ToInt32(row["CourseId"]);
                            user.Language = ParameterFormater.UnescapeXML(row["Language"].ToString());
                            user.ContentVersion = ParameterFormater.UnescapeXML(row["ContentVersion"].ToString());
                            user.PageName = ParameterFormater.UnescapeXML(row["PageName"].ToString());
                            user.Title = ParameterFormater.UnescapeXML(row["Title"].ToString());
                            user.DateCreated = Convert.ToDateTime(row["DateCreated"].ToString());
                            user.Link = row["Link"].ToString();
                            user.Time = Convert.ToInt32(row["Time"]);
                            user.IsDB = Convert.ToBoolean(row["IsDB"]);
                            // properties for migration
                            user.MigratedChapterId = Convert.ToInt32(row["MigratedChapterId"]);
                            user.OrignalName = row["OrignalName"].ToString();
                            userlist.Add(user);
                        }
                    }

                    con.Close();
                    return userlist;
                }

            }
            catch (Exception ex)
            {
                logger.Error("ChapterDetailsDAO", "GetAllChapterDetails", " Error occurred while Getting Chapter Details list", ex, logPath);
                throw ex;
            }
        }

        // select function for selecting chapter details by id
        public List<ChapterDetails> GetChapterDetailsById(string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterDetailsDAO", "GetChapterDetailsById", "Selecting Chapter Details list by Id from database", logPath);
                string result = Select_Chapter_By_Id;
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<ChapterDetails> userlist = new List<ChapterDetails>();
                    DbDataReader reader = db.Select(result, logPath, con);
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            ChapterDetails user = new ChapterDetails();
                            user.Id = Convert.ToInt32(reader["Id"]);
                            user.CourseName = reader["coursename"].ToString();
                            user.Language = ParameterFormater.UnescapeXML(reader["Language"].ToString());
                            user.ContentVersion = ParameterFormater.UnescapeXML(reader["ContentVersion"].ToString());
                            //user.PageName = ParameterFormater.UnescapeXML(reader["PageName"].ToString());
                            user.Title = ParameterFormater.UnescapeXML(reader["Title"].ToString());
                            //user.FileName = reader["FileName"].ToString();
                            // user.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                            userlist.Add(user);
                        }

                        if (!reader.IsClosed)
                        {
                            reader.Close();
                        }
                    }

                    con.Close();
                    return userlist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterDetailsDAO", "GetChapterDetailsById", " Error occurred while Getting Chapter Details list by id", ex, logPath);
                throw ex;
            }
        }

        //select function for selecting chapter's on the basis of given id. 
        public ChapterDetails GetChapterDetails(int id, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterDetailsDAO", "GetChapterDetails", "Selecting Chapter Details list by Id from database", logPath);
                string result = string.Format(Select_Chapter_Details_By_Chapter_Id, id);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();

                    DataSet ds = db.SelectDataSet(result, logPath, con);
                    ChapterDetails user = new ChapterDetails();

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            user.Id = Convert.ToInt32(row["Id"]);
                            user.CourseId = Convert.ToInt32(row["CourseId"]);
                            user.Language = ParameterFormater.UnescapeXML(row["Language"].ToString());
                            user.ContentVersion = ParameterFormater.UnescapeXML(row["ContentVersion"].ToString());
                            user.PageName = ParameterFormater.UnescapeXML(row["PageName"].ToString());
                            user.Title = ParameterFormater.UnescapeXML(row["Title"].ToString());
                            user.DateCreated = Convert.ToDateTime(row["DateCreated"].ToString());
                            user.Link = row["Link"].ToString();
                            user.Time = Convert.ToInt32(row["Time"]);
                            user.IsDB = Convert.ToBoolean(row["IsDB"]);
                            // properties for migration
                            user.MigratedChapterId = Convert.ToInt32(row["MigratedChapterId"]);
                            user.OrignalName = row["OrignalName"].ToString();
                        }
                    }

                    con.Close();
                    return user;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterDetailsDAO", "GetChapterDetails", " Error occurred while Getting Chapter Details list by id", ex, logPath);
                throw ex;
            }
        }

        // Select Function for selecting latest content version.
        public string GetLatestContectVersion(string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterDetailsDAO", "GetLatestContectVersion", "Selecting latest content version from database", logPath);
                string result = Select_Latest_Content_Version;
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string contentVersion = string.Empty;
                    DbDataReader reader = db.Select(result, logPath, con);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            contentVersion = reader["version"].ToString();
                        }
                        if (!reader.IsClosed)
                        {
                            reader.Close();
                        }
                    }

                    con.Close();
                    return contentVersion;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterDetailsDAO", "GetLatestContectVersion", " Error occurred while selecting latest content version", ex, logPath);
                throw ex;
            }
        }

        public int GetLastInsertedChapterId(string cnxnString, string logPath)
        {
            int chapterId = 1;

            try
            {
                string cmdText = SelectMaxChapterId;
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    DbDataReader reader = db.Select(cmdText, logPath, con);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            chapterId = Convert.ToInt32(reader["ChapterId"]);
                        }
                        if (!reader.IsClosed)
                        {
                            reader.Close();
                        }
                    }

                    con.Close();
                    return chapterId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChapterDetails> GetChaptersBetweenTwoIds(int startChapterId, int endChapterId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterDetailsDAO", "GetAllChapterDetails", "Selecting Chapter Details list from database", logPath);
                string result = string.Format(Q_GetChapterByTwoChapterIds, startChapterId, endChapterId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<ChapterDetails> userlist = new List<ChapterDetails>();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChapterDetails user = new ChapterDetails();
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user.CourseId = Convert.ToInt32(reader["CourseId"]);
                                user.Language = ParameterFormater.UnescapeXML(reader["Language"].ToString());
                                user.ContentVersion = ParameterFormater.UnescapeXML(reader["ContentVersion"].ToString());
                                user.PageName = ParameterFormater.UnescapeXML(reader["PageName"].ToString());
                                user.Title = ParameterFormater.UnescapeXML(reader["Title"].ToString());
                                user.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                user.Link = reader["Link"].ToString();
                                user.Time = Convert.ToInt32(reader["Time"]);
                                user.IsDB = Convert.ToBoolean(reader["IsDB"]);
                                // properties for migration
                                user.MigratedChapterId = Convert.ToInt32(reader["MigratedChapterId"]);
                                user.OrignalName = reader["OrignalName"].ToString();
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
                logger.Error("ChapterDetailsDAO", "GetAllChapterDetails", " Error occurred while Getting Chapter Details list", ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Insert Function
        public int AddChapters(int courseId, string language, string contentVersion, string fileName, string chapterName, DateTime dateCreated, string link, int time, bool isDB, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();

                    string cmdText = string.Format(Add_Chapters, courseId, ParameterFormater.FormatParameter(language), ParameterFormater.FormatParameter(contentVersion), ParameterFormater.FormatParameter(fileName),
                        ParameterFormater.FormatParameter(chapterName), dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"), link, time, isDB);

                    Database db = new Database();

                    int lastInsetedId = db.Insert(cmdText, logPath, con);

                    con.Close();
                    return lastInsetedId;
                }

            }
            catch (Exception ex)
            {
                logger.Error("ChapterDetailsDAO", "AddChapters", ex.Message, ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Update Function
        public void UpdateChapters(int id, int courseId, string language, string contentVersion, string fileName, string chapterName, DateTime dateCreated, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Update_Chapters, courseId, ParameterFormater.FormatParameter(language), ParameterFormater.FormatParameter(contentVersion), ParameterFormater.FormatParameter(fileName),
                                        ParameterFormater.FormatParameter(chapterName), dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"), id);

                    Database db = new Database();
                    db.Update(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterDetailsDAO", "UpdateChapters", "Error Occured while updating Chapter Details", ex, logPath);
                throw ex;
            }
        }

        public void Update_ChaptersTitle(int id, string title, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_Update_ChaptersName, ParameterFormater.FormatParameter(title), id);
                    Database db = new Database();
                    db.Update(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete Function
        public void DeleteChapter(int chapterId, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    Database db = new Database();
                    string cmdText = string.Format(Q_DeleteChapterById, chapterId);

                    con.Open();
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

        #region Migration Functionalyties
        #region DB Queries
        private string Q_AddMigChapters = "insert into chapterdetails(CourseId,Language,ContentVersion,PageName,Title,DateCreated, Link,Time,IsDB,OrignalName,MigratedChapterId) values({0},'{1}','{2}','{3}','{4}','{5}','{6}',{7},{8},'{9}',{10})";
        #endregion

        #region Insert Function
        public int AddMigChapters(int courseId, string language, string contentVersion, string fileName, string chapterName, DateTime dateCreated, string link, int time, bool isDB, string orignalName, int migrateChapterId, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_AddMigChapters, courseId, ParameterFormater.FormatParameter(language), ParameterFormater.FormatParameter(contentVersion), ParameterFormater.FormatParameter(fileName),
                        ParameterFormater.FormatParameter(chapterName), dateCreated.ToString("yyyy/MM/dd HH:mm:ss.fff"), link, time, isDB, ParameterFormater.FormatParameter(orignalName), migrateChapterId);
                    Database db = new Database();
                    int lastInsetedId = db.Insert(cmdText, logPath, con);
                    con.Close();
                    return lastInsetedId;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterDetailsDAO", "AddChapters", ex.Message, ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Select function
        #endregion
        #endregion
    }
}
