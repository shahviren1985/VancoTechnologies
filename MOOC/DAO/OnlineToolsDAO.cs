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
    public class OnlineToolsDAO
    {
        Logger logger = new Logger();

        #region DB Scripts
        private string Q_AddOnlineTools = "insert into onlinetools(RelatedCourseId,Title,Description,LogoImageUrl,LogoImageName,ToolUrl,ToolDisplayDate,IsActive) values({0},'{1}','{2}','{3}','{4}','{5}','{6}',{7})";
        private string Q_GetAllOnlineToolsByCourseId = "select * from onlinetools where RelatedCourseId={0}";
        private string Q_UpdateOnlineTools = "update onlinetools set RelatedCourseId={0},Title='{1}',Description='{2}',LogoImageUrl='{3}',LogoImageName='{4}',ToolUrl='{5}',ToolDisplayDate='{6}',IsActive={7} where id={8}";
        private string Q_DeleteOnlineTools = "delete from onlinetools where id={0}";
        private string Q_GetOnlineToolsById = "select * from onlinetools where Id={0}";
        #endregion

        #region Insert Function
        public void AddOnlineTools(int relatedCourseId, string title, string description, string logoImgURL, string logoImgName, string toolURL, DateTime toolDisplayDate, bool isActive, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_AddOnlineTools, relatedCourseId, ParameterFormater.FormatParameter(title), ParameterFormater.FormatParameter(description), logoImgURL, logoImgName, toolURL, toolDisplayDate.ToString("yyyy/MM/dd HH:mm:ss.ff"), isActive);
                    Database db = new Database();
                    db.Insert(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("OnlineTools", "AddOnlineTools", "Error occurred while adding online tools", ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Select Function
        public List<OnlineTools> GetAllOnlineToolsByCourseId(int courseId, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetAllOnlineToolsByCourseId, courseId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();

                    DataSet ds = db.SelectDataSet(cmdText, logPath, con);
                    List<OnlineTools> onlineTools = new List<OnlineTools>();

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            OnlineTools onlineTool = new OnlineTools();

                            onlineTool.Id = Convert.ToInt32(row["Id"]);
                            onlineTool.RelatedCourseId = Convert.ToInt32(row["RelatedCourseId"]);
                            onlineTool.Title = ParameterFormater.UnescapeXML(row["Title"].ToString());
                            onlineTool.Description = ParameterFormater.UnescapeXML(row["Description"].ToString());
                            onlineTool.LogoImageURL = row["LogoImageUrl"].ToString();
                            onlineTool.LogoImageName = row["LogoImageName"].ToString();
                            onlineTool.ToolURL = row["ToolUrl"].ToString();
                            onlineTool.ToolDisplayDate = Convert.ToDateTime(row["ToolDisplayDate"]);
                            onlineTool.ToolDisplayDateNormal = onlineTool.ToolDisplayDate.ToString("dd-MMM-yyyy");
                            onlineTool.IsActive = Convert.ToBoolean(row["IsActive"]);
                            onlineTool.CreateDate = Convert.ToDateTime(row["CreatedDate"]);

                            onlineTools.Add(onlineTool);
                        }
                    }

                    con.Close();
                    return onlineTools;
                }
            }
            catch (Exception ex)
            {
                logger.Error("OnlineTools", "GetAllOnlineToolsByCourseId", "Error occurred while getting all online tools by courseId", ex, logPath);
                throw ex;
            }
        }

        public OnlineTools GetOnlineToolsById(int Id, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetOnlineToolsById, Id);
                Database db = new Database();
                OnlineTools onlineTool = new OnlineTools();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                onlineTool.Id = Convert.ToInt32(reader["Id"]);
                                onlineTool.RelatedCourseId = Convert.ToInt32(reader["RelatedCourseId"]);
                                onlineTool.Title = ParameterFormater.UnescapeXML(reader["Title"].ToString());
                                onlineTool.Description = ParameterFormater.UnescapeXML(reader["Description"].ToString());
                                onlineTool.LogoImageURL = reader["LogoImageUrl"].ToString();
                                onlineTool.LogoImageName = reader["LogoImageName"].ToString();
                                onlineTool.ToolURL = reader["ToolUrl"].ToString();
                                onlineTool.ToolDisplayDate = Convert.ToDateTime(reader["ToolDisplayDate"]);
                                onlineTool.ToolDisplayDateNormal = onlineTool.ToolDisplayDate.ToString("dd-MMM-yyyy");
                                onlineTool.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                onlineTool.CreateDate = Convert.ToDateTime(reader["CreatedDate"]);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return onlineTool;
                }
            }
            catch (Exception ex)
            {
                logger.Error("OnlineTools", "GetOnlineToolsById", "Error occurred while getting all online tools by Id", ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Update Function
        public void UpdateOnlineTools(int id, int relatedCourseId, string title, string description, string logoImgURL, string logoImgName, string toolURL, DateTime toolDisplayDate, bool isActive, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_UpdateOnlineTools, relatedCourseId, ParameterFormater.FormatParameter(title), ParameterFormater.FormatParameter(description), logoImgURL, logoImgName, toolURL, toolDisplayDate.ToString("yyyy/MM/dd HH:mm:ss.ff"), isActive, id);
                    Database db = new Database();
                    db.Update(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("OnlineTools", "UpdateOnlineTools", "Error occurred while updating online tools", ex, logPath);
                throw ex;
            }
        }
        #endregion

        #region Delete Function
        public void DeleteOnlineTools(int id, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_DeleteOnlineTools, id);
                    Database db = new Database();
                    db.Delete(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Error("OnlineTools", "DeleteOnlineTools", "Error occurred while deleting online tools", ex, logPath);
                throw ex;
            }
        }
        #endregion
    }
}
