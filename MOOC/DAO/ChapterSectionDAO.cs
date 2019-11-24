using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using ITM.Courses.Utilities;
using ITM.Courses.DAOBase;
using ITM.Courses.LogManager;
using System.Data;
using MySql.Data.MySqlClient;

namespace ITM.Courses.DAO
{
    public class ChapterSectionDAO
    {
        Logger logger = new Logger();

        #region Database Queries
        private string Add_ChapterSection = "Insert into chaptersection(ChapterId, Title, PageName,DateCreated, Description,Link,Time,IsDB, PageContent, OrignalName) values({0},'{1}','{2}','{3}','{4}','{5}','{6}',{7},'{8}','{1}')";
        private string Get_All_ChapterSection = "Select * from chaptersection";
        private string Get_ChapterSectionByChapterId = "Select * from chaptersection where ChapterId = @chapterid";
        private string Get_SectionByChapterIdAndSectionId = "Select * from chaptersection where ChapterId = {0} and Id = {1}";
        private string Update_ChapterSection = "update chaptersection set Title='{0}', PageName='{1}' where id={2}";
        private string DeleteDynamicSection = "Delete from chaptersection where Id = {0}";
        private string Q_GetChapterEstimateTime = "SELECT sum(Time) as EstimateTime FROM chaptersection where ChapterId= {0} group by ChapterId";
        private string Q_UpdateSectionPageContent = "update chaptersection set PageContent='{0}', Time='{1}', Description='{2}'  where id={3}";
        private string Q_GetChapterSectionById = "Select * from chaptersection where Id = {0}";
        private string Q_GetTotalSectionCountByCourse = "SELECT count(*) as Count  FROM chaptersection where chapterid in (select id from chapterdetails where courseid = {0})";
        private string Q_GetEmptySecitonCountByCourse = "SELECT count(*) as Count  FROM chaptersection where chapterid in (select id from chapterdetails where courseid = {0}) and isDB = true and (pagecontent= '' or pagecontent= ' ')";
        //dynamic course
        private string Q_GetAllSectionsByCourse = "SELECT * FROM chaptersection cs inner join chapterdetails cd on cs.chapterid = cd.id where cd.courseid = @courseid";
        #endregion

        #region Insert Function //
        public int AddChapterSection(int chapterId, string sectionName, string sectionFileName, string description, string link, int time, bool IsDB, string pageContent, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Add_ChapterSection, chapterId, ParameterFormater.FormatParameter(sectionName), ParameterFormater.FormatParameter(sectionFileName),
                                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), description, link, time, IsDB, pageContent);
                    Database db = new Database();
                    int lastInsetedId = db.Insert(cmdText, logPath, con);
                    con.Close();
                    return lastInsetedId;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Select Funcation for dynamic-course
        public List<ChapterSection> GetAllSectionsByCourse(int courseId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterSectionDAO", "GetChapterSectionsByChapterId", "Selecting Chapter Details list from database", logPath);
                string result = string.Format(Q_GetAllSectionsByCourse, courseId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();

                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    MySqlCommand cmd = new MySqlCommand(Q_GetAllSectionsByCourse, con);
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.AddWithValue("courseid", courseId);
                    adapter.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    //DataSet ds = db.SelectDataSet(result, logPath, con);
                    List<ChapterSection> chapterSections = new List<ChapterSection>();

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            ChapterSection chapterSection = new ChapterSection();
                            chapterSection.Id = Convert.ToInt32(row["Id"]);
                            chapterSection.ChapterId = Convert.ToInt32(row["ChapterId"]);
                            chapterSection.Title = ParameterFormater.UnescapeXML(row["Title"].ToString());
                            chapterSection.PageName = ParameterFormater.UnescapeXML(row["PageName"].ToString());
                            chapterSection.Description = ParameterFormater.UnescapeXML(row["Description"].ToString());
                            chapterSection.Link = ParameterFormater.UnescapeXML(row["Link"].ToString());
                            chapterSection.Time = Convert.ToInt32(row["Time"]);
                            chapterSection.DateCreated = Convert.ToDateTime(row["DateCreated"].ToString());
                            chapterSection.IsDB = Convert.ToBoolean(row["IsDB"]);
                            chapterSection.PageContent = ParameterFormater.UnescapeXML(row["PageContent"].ToString());
                            // migration properties
                            chapterSection.OrignalName = ParameterFormater.UnescapeXML(row["OrignalName"].ToString());
                            chapterSection.MigratedSectionId = Convert.ToInt32(row["MigratedSectionId"]);
                            chapterSections.Add(chapterSection);
                        }
                    }

                    con.Close();
                    return chapterSections;
                }

            }
            catch (Exception ex)
            {
                logger.Error("ChapterSectionDAO", "GetChapterSectionsByChapterId", ex.Message, ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Select Function
        public List<ChapterSection> GetAllChapterSections(string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterSectionDAO", "GetAllChapterSections", "Selecting Chapter Details list from database", logPath);
                string result = Get_All_ChapterSection;
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<ChapterSection> chapterSections = new List<ChapterSection>();

                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {

                            while (reader.Read())
                            {
                                ChapterSection chapterSection = new ChapterSection();
                                chapterSection.Id = Convert.ToInt32(reader["Id"]);
                                chapterSection.ChapterId = Convert.ToInt32(reader["ChapterId"]);
                                chapterSection.Title = ParameterFormater.UnescapeXML(reader["Title"].ToString());
                                chapterSection.PageName = ParameterFormater.UnescapeXML(reader["PageName"].ToString());
                                chapterSection.Description = ParameterFormater.UnescapeXML(reader["Description"].ToString());
                                chapterSection.Link = ParameterFormater.UnescapeXML(reader["Link"].ToString());
                                chapterSection.Time = Convert.ToInt32(reader["Time"]);
                                chapterSection.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                chapterSection.IsDB = Convert.ToBoolean(reader["IsDB"]);
                                chapterSection.PageContent = ParameterFormater.UnescapeXML(reader["PageContent"].ToString());
                                // migration properties
                                chapterSection.OrignalName = ParameterFormater.UnescapeXML(reader["OrignalName"].ToString());
                                chapterSection.MigratedSectionId = Convert.ToInt32(reader["MigratedSectionId"]);

                                chapterSections.Add(chapterSection);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return chapterSections;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterSectionDAO", "GetAllChapterSections", ex.Message, ex, logPath);
                throw ex;
            }
        }

        public List<ChapterSection> GetChapterSectionsByChapterId(int chapterId, string cnxnString, string logPath)
        {
            Database db = new Database();

            try
            {
                //logger.Debug("ChapterSectionDAO", "GetChapterSectionsByChapterId", "Selecting Chapter Details list from database", logPath);
                string result = string.Format(Get_ChapterSectionByChapterId, chapterId);

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<ChapterSection> chapterSections = new List<ChapterSection>();
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    DataSet ds = new DataSet();

                    MySqlCommand cmd = new MySqlCommand(Get_ChapterSectionByChapterId, con);
                    cmd.CommandTimeout = 300;
                    cmd.Parameters.AddWithValue("chapterid", chapterId);
                    adapter.SelectCommand = cmd;
                    adapter.Fill(ds);
                    
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            ChapterSection chapterSection = new ChapterSection();
                            chapterSection.Id = Convert.ToInt32(row["Id"]);
                            chapterSection.ChapterId = Convert.ToInt32(row["ChapterId"]);
                            chapterSection.Title = ParameterFormater.UnescapeXML(row["Title"].ToString());
                            chapterSection.PageName = ParameterFormater.UnescapeXML(row["PageName"].ToString());
                            chapterSection.Description = ParameterFormater.UnescapeXML(row["Description"].ToString());
                            chapterSection.Link = ParameterFormater.UnescapeXML(row["Link"].ToString());
                            chapterSection.Time = Convert.ToInt32(row["Time"]);
                            chapterSection.DateCreated = Convert.ToDateTime(row["DateCreated"].ToString());
                            chapterSection.IsDB = Convert.ToBoolean(row["IsDB"]);
                            chapterSection.PageContent = ParameterFormater.UnescapeXML(row["PageContent"].ToString());
                            // migration properties
                            chapterSection.OrignalName = ParameterFormater.UnescapeXML(row["OrignalName"].ToString());
                            chapterSection.MigratedSectionId = Convert.ToInt32(row["MigratedSectionId"]);
                            chapterSections.Add(chapterSection);
                        }

                        //logger.Debug("ChapterSectionDAO", "GetChapterSectionsByChapterId", " returning Chapter Details list  from database", logPath);
                    }

                    con.Close();
                    return chapterSections;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterSectionDAO", "GetChapterSectionsByChapterId", ex.Message, ex, logPath);
                throw ex;
            }
        }

        public ChapterSection GetSectionsByChapterIdAndSectionId(int chapterId, int sectionId, string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ChapterSectionDAO", "GetSectionsByChapterIdAndSectionId", "Selecting section details from database", logPath);
                string result = string.Format(Get_SectionByChapterIdAndSectionId, chapterId, sectionId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    ChapterSection chapterSections = new ChapterSection();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ChapterSection chapterSection = new ChapterSection();
                                chapterSection.Id = Convert.ToInt32(reader["Id"]);
                                chapterSection.ChapterId = Convert.ToInt32(reader["ChapterId"]);
                                chapterSection.Title = ParameterFormater.UnescapeXML(reader["Title"].ToString());
                                chapterSection.PageName = ParameterFormater.UnescapeXML(reader["PageName"].ToString());
                                chapterSection.Description = ParameterFormater.UnescapeXML(reader["Description"].ToString());
                                chapterSection.Link = ParameterFormater.UnescapeXML(reader["Link"].ToString());
                                chapterSection.Time = Convert.ToInt32(reader["Time"]);
                                chapterSection.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                chapterSection.IsDB = Convert.ToBoolean(reader["IsDB"]);
                                chapterSection.PageContent = ParameterFormater.UnescapeXML(reader["PageContent"].ToString());
                                // migration properties
                                chapterSection.OrignalName = ParameterFormater.UnescapeXML(reader["OrignalName"].ToString());
                                chapterSection.MigratedSectionId = Convert.ToInt32(reader["MigratedSectionId"]);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return chapterSections;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterSectionDAO", "GetSectionsByChapterIdAndSectionId", ex.Message, ex, logPath);
                throw ex;
            }
        }

        public ChapterSection GetChapterSectionById(int sectionId, string cnxnString, string logPath)
        {
            try
            {

                string result = string.Format(Q_GetChapterSectionById, sectionId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    ChapterSection chapterSection = new ChapterSection();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                chapterSection.Id = Convert.ToInt32(reader["Id"]);
                                chapterSection.ChapterId = Convert.ToInt32(reader["ChapterId"]);
                                chapterSection.Title = ParameterFormater.UnescapeXML(reader["Title"].ToString());
                                chapterSection.PageName = ParameterFormater.UnescapeXML(reader["PageName"].ToString());
                                chapterSection.Description = ParameterFormater.UnescapeXML(reader["Description"].ToString());
                                chapterSection.Link = ParameterFormater.UnescapeXML(reader["Link"].ToString());
                                chapterSection.Time = Convert.ToInt32(reader["Time"]);
                                chapterSection.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                chapterSection.IsDB = Convert.ToBoolean(reader["IsDB"]);
                                //chapterSection.PageContent = ParameterFormater.UnescapeXML(reader["PageContent"].ToString());
                                chapterSection.PageContent = reader["PageContent"].ToString();
                                // migration properties
                                chapterSection.OrignalName = ParameterFormater.UnescapeXML(reader["OrignalName"].ToString());
                                chapterSection.MigratedSectionId = Convert.ToInt32(reader["MigratedSectionId"]);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }

                            //logger.Debug("ChapterSectionDAO", "GetSectionsByChapterIdAndSectionId", " returning section details  from database", logPath);
                        }

                        con.Close();
                        return chapterSection;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("ChapterSectionDAO", "GetSectionsByChapterIdAndSectionId", ex.Message, ex, logPath);
                throw ex;
            }
        }

        public decimal GetChapterEstimateTimeInHours(int chapterId, string cnxnString, string logPath)
        {
            int timeInSeconds = 0;

            try
            {
                string cmdText = string.Format(Q_GetChapterEstimateTime, chapterId);
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
                                timeInSeconds = Convert.ToInt32(reader["EstimateTime"]);
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

                            return timeInSeconds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return timeInSeconds;
        }

        public decimal GetChapterEstimateTimeInMinuts(int chapterId, string cnxnString, string logPath)
        {
            int timeInSeconds = 0;

            try
            {
                string cmdText = string.Format(Q_GetChapterEstimateTime, chapterId);
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
                                timeInSeconds = Convert.ToInt32(reader["EstimateTime"]);
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

        public int GetTotalSectionCountByCourse(int courseId, string cnxnString, string logPath)
        {
            int sectionCount = 0;

            try
            {
                string cmdText = string.Format(Q_GetTotalSectionCountByCourse, courseId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    DataSet ds = db.SelectDataSet(cmdText, logPath, con);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            sectionCount = Convert.ToInt32(row["Count"]);
                        }
                    }

                    con.Close();
                    return sectionCount;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetEmptySecitonCountByCourse(int courseId, string cnxnString, string logPath)
        {
            int emptySectionCount = 0;

            try
            {
                string cmdText = string.Format(Q_GetEmptySecitonCountByCourse, courseId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    DataSet ds = db.SelectDataSet(cmdText, logPath, con);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            emptySectionCount = Convert.ToInt32(row["Count"]);
                        }
                    }

                    con.Close();
                    return emptySectionCount;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update Function
        public void UpdateChapterSection(int id, string sectionName, string sectionFileName, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Update_ChapterSection, ParameterFormater.FormatParameter(sectionName), sectionFileName, id);
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

        public void UpdateSectionPageContent(int sectionId, string pageContent, int time, string pageColor, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    Database db = new Database();
                    string cmdText = string.Format(Q_UpdateSectionPageContent, ParameterFormater.FormatParameter(pageContent), time, pageColor, sectionId);//description use as page color
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
        public void DeleteSectionRecord(int secId, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(DeleteDynamicSection, secId);
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

        #region Migration Functionalyties
        #region DB Queries
        private string Q_AddMigChapterSection = "Insert into chaptersection(ChapterId, Title, PageName,DateCreated, Description,Link,Time,IsDB, PageContent, OrignalName,MigratedSectionId) values({0},'{1}','{2}','{3}','{4}','{5}','{6}',{7},'{8}','{9}',{10})";
        #endregion

        #region Insert Function
        public int AddMigChapterSection(int chapterId, string sectionName, string sectionFileName, string description, string link, int time, bool IsDB, string pageContent, string orignalName, int migrateSectionId, string cnxnString, string logPath)
        {
            try
            {
                int lastInsetedId = 0;
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_AddMigChapterSection, chapterId, ParameterFormater.FormatParameter(sectionName), ParameterFormater.FormatParameter(sectionFileName),
                                        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), description, link, time, IsDB, ParameterFormater.FormatParameter(pageContent), ParameterFormater.FormatParameter(orignalName), migrateSectionId);
                    Database db = new Database();
                    lastInsetedId = db.Insert(cmdText, logPath, con);
                    con.Close();
                }
                return lastInsetedId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion
    }
}
