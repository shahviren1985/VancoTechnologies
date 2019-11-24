using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITM.Courses.LogManager;
using ITM.Courses.DAOBase;
using System.Data.Common;
using ITM.Courses.Utilities;
using MySql.Data.MySqlClient;

namespace ITM.Courses.DAO
{
    public class ContentVersionDAO
    {
        Logger logger = new Logger();
        public string Select_All_Conent_Version = "Select * from contentversion";
        private string Add_Conent_Version = "insert into contentversion(version) values('{0}')";
        private string Update_Conent_Version = "update contentversion set version = '{0}' ";

        #region Select function
        public List<ContentVersion> GetAllConentVersion(string cnxnString, string logPath)
        {
            try
            {
                //logger.Debug("ContentVersionDAO", "GetAllConentVersion", "Selecting Content Version list from database", logPath);
                string result = Select_All_Conent_Version;
                Database db = new Database();
                List<ContentVersion> contentlist = new List<ContentVersion>();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ContentVersion content = new ContentVersion();
                                content.Vesion = reader["version"].ToString();
                                contentlist.Add(content);
                            }
                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return contentlist;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ContentVersionDAO", "GetAllConentVersion", " Error occurred while Getting Content Version list", ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Insert Function
        public void AddContentVersion(string version, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Add_Conent_Version, version);
                    Database db = new Database();
                    db.Insert(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("ContentVersionDAO", "AddContentVersion", ex.Message, ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Update Function
        public void UpdateContentVersion(string version, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Update_Conent_Version, version);
                    Database db = new Database();
                    db.Update(cmdText, logPath, con);
                    con.Close();
                }
                
            }
            catch (Exception ex)
            {
                logger.Error("ContentVersionDAO", "UpdateContentVersion", "Error Occured while updating Chapter Details", ex, logPath);
                throw ex;
            }
        }
        #endregion
    }
}
