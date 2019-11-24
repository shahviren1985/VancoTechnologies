using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using ITM.Courses.DAOBase;
using MySql.Data.MySqlClient;

namespace ITM.Courses.DAO
{
    public class CourseFilesDAO
    {
        #region DB Queries
        private string Q_AddCourseFile = "insert into coursefiles(name,displayname,caption,contenttype,contentsize,thumbnail,attachlink,userId,courseId) values('{0}','{1}','{2}','{3}',{4},'{5}','{6}',{7},{8})";
        private string Q_UpdateCourseFile = "update coursefiles set name='{1}',displayname='{2}',caption='{3}',contenttype='{4}',contentsize={5},thumbnail='{6}',attachlink='{7}',userId={8},courseId={9} where id={0}";
        private string Q_GetAllCourseFiles = "select * from coursefiles";

        private string Q_GetCourseFilesByCourse = "select * from coursefiles where courseId = {0}";
        private string Q_GetCourseFilesByUser = "select * from coursefiles where userId = {0}";
        private string Q_GetCourseFileById = "select * from coursefiles where Id = {0}";
        #endregion

        #region Insert function
        public void AddCourseFiles(string name, string displayName, string caption, string contentType, int contentSize, string thumbnail, string attachLink, int userId, int courseId, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_AddCourseFile, name, displayName, caption, contentType, contentSize, thumbnail, attachLink, userId, courseId);
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

        #region Select functions
        public List<CourseFiles> GetAllCourseFiles(string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetAllCourseFiles);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<CourseFiles> courseFiles = new List<CourseFiles>();

                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CourseFiles courseFile = new CourseFiles();
                                courseFile.Id = Convert.ToInt32(reader["id"]);
                                courseFile.Name = reader["name"].ToString();
                                courseFile.DisplayName = reader["displayname"].ToString();
                                courseFile.Caption = reader["caption"].ToString();
                                courseFile.ContentType = reader["contenttype"].ToString();
                                courseFile.ContentSize = Convert.ToInt32(reader["contentsize"]);
                                courseFile.Thumbnail = reader["thumbnail"].ToString();
                                courseFile.AttachLink = reader["attachlink"].ToString();
                                courseFile.UserId = Convert.ToInt32(reader["userId"]);
                                courseFile.CourseId = Convert.ToInt32(reader["courseId"]);
                                courseFiles.Add(courseFile);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return courseFiles;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CourseFiles> GetCourseFilesByCourse(int courseId, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetCourseFilesByCourse, courseId);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<CourseFiles> courseFiles = new List<CourseFiles>();

                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CourseFiles courseFile = new CourseFiles();

                                courseFile.Id = Convert.ToInt32(reader["id"]);
                                courseFile.Name = reader["name"].ToString();
                                courseFile.DisplayName = reader["displayname"].ToString();
                                courseFile.Caption = reader["caption"].ToString();
                                courseFile.ContentType = reader["contenttype"].ToString();
                                courseFile.ContentSize = Convert.ToInt32(reader["contentsize"]);
                                courseFile.Thumbnail = reader["thumbnail"].ToString();
                                courseFile.AttachLink = reader["attachlink"].ToString();
                                courseFile.UserId = Convert.ToInt32(reader["userId"]);
                                courseFile.CourseId = Convert.ToInt32(reader["courseId"]);

                                courseFiles.Add(courseFile);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return courseFiles;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CourseFiles> GetCourseFilesByUser(int userId, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetCourseFilesByUser, userId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<CourseFiles> courseFiles = new List<CourseFiles>();

                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CourseFiles courseFile = new CourseFiles();

                                courseFile.Id = Convert.ToInt32(reader["id"]);
                                courseFile.Name = reader["name"].ToString();
                                courseFile.DisplayName = reader["displayname"].ToString();
                                courseFile.Caption = reader["caption"].ToString();
                                courseFile.ContentType = reader["contenttype"].ToString();
                                courseFile.ContentSize = Convert.ToInt32(reader["contentsize"]);
                                courseFile.Thumbnail = reader["thumbnail"].ToString();
                                courseFile.AttachLink = reader["attachlink"].ToString();
                                courseFile.UserId = Convert.ToInt32(reader["userId"]);
                                courseFile.CourseId = Convert.ToInt32(reader["courseId"]);

                                courseFiles.Add(courseFile);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return courseFiles;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CourseFiles GetCourseFileById(int id, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetCourseFileById, id);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    CourseFiles courseFile = new CourseFiles();

                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                courseFile.Id = Convert.ToInt32(reader["id"]);
                                courseFile.Name = reader["name"].ToString();
                                courseFile.DisplayName = reader["displayname"].ToString();
                                courseFile.Caption = reader["caption"].ToString();
                                courseFile.ContentType = reader["contenttype"].ToString();
                                courseFile.ContentSize = Convert.ToInt32(reader["contentsize"]);
                                courseFile.Thumbnail = reader["thumbnail"].ToString();
                                courseFile.AttachLink = reader["attachlink"].ToString();
                                courseFile.UserId = Convert.ToInt32(reader["userId"]);
                                courseFile.CourseId = Convert.ToInt32(reader["courseId"]);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return courseFile;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update function
        public void UpdateCourseFile(int id, string name, string displayName, string caption, string contentType, int contentSize, string thumbnail, string attachLink, int userId, int courseId, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_UpdateCourseFile, id, name, displayName, caption, contentType, contentSize, thumbnail, attachLink, userId, courseId);
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
    }
}
