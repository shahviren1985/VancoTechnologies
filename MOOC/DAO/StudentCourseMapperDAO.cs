using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using ITM.Courses.DAOBase;
using ITM.Courses.LogManager;
using ITM.Courses.Utilities;
using System.Data;
using MySql.Data.MySqlClient;

namespace ITM.Courses.DAO
{
    public class StudentCourseMapperDAO
    {
        Logger logger = new Logger();

        #region DB-Queries
        private string Q_AddStudentCourseMapper = "insert into studentcoursemapper(userId,courseId,IsEnrolled,mappedOn) values({0},{1},{2},'{3}')";
        private string Q_GetAllStudentCourseMapperRecords = "select * from studentcoursemapper";
        private string Q_GetStudentCourseMapperRecordsByCourseUserId = "select * from studentcoursemapper where userid={0} and courseId={1}";
        private string Q_UpdateStudentCourseMapper = "update studentcoursemapper set userId={0},courseId={1},IsEnrolled={2},mappedOn='{3}', updatedOn='{4}'";
        private string Q_GetMappedCourseByStudent = "select * from coursedetails cd inner join studentcoursemapper sc on cd.id = sc.courseid where sc.userid = {0}";

        private string Q_GetStudentsByTest = "SELECT * FROM userdetails u where id in (select userid from studentcoursemapper where courseid = (select courseid from coursetests where id= {0})) and course = '{1}' and isactive = true and isenabled = true";

        private string Q_GetStudentCourseMapperByCourseId = "SELECT * FROM studentcoursemapper s where courseid={0}";
        #endregion

        #region Insert functions
        public void AddStudentCourseMapper(int userId, int courseId, bool isEnrolled, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_AddStudentCourseMapper, userId, courseId, isEnrolled, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
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
        public List<StudentCourseMapper> GetAllStudentCourseMapperRecords(string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetAllStudentCourseMapperRecords);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<StudentCourseMapper> oMappers = new List<StudentCourseMapper>();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                StudentCourseMapper oMapper = new StudentCourseMapper();
                                oMapper.Id = Convert.ToInt32(reader["Id"]);
                                oMapper.UserId = Convert.ToInt32(reader["userId"]);
                                oMapper.CourseId = Convert.ToInt32(reader["courseId"]);
                                oMapper.IsEnrolled = Convert.ToBoolean(reader["IsEnrolled"]);
                                oMapper.MappedOn = Convert.ToDateTime(reader["mappedOn"]);
                                oMapper.UpdatedOn = Convert.ToDateTime(reader["updatedOn"]);

                                oMappers.Add(oMapper);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return oMappers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StudentCourseMapper> GetStudentCourseMapperByCourseId(int courseId, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetStudentCourseMapperByCourseId, courseId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<StudentCourseMapper> oMappers = new List<StudentCourseMapper>();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                StudentCourseMapper oMapper = new StudentCourseMapper();
                                oMapper.Id = Convert.ToInt32(reader["Id"]);
                                oMapper.UserId = Convert.ToInt32(reader["userId"]);
                                oMapper.CourseId = Convert.ToInt32(reader["courseId"]);
                                oMapper.IsEnrolled = Convert.ToBoolean(reader["IsEnrolled"]);
                                oMapper.MappedOn = Convert.ToDateTime(reader["mappedOn"]);
                                oMapper.UpdatedOn = Convert.ToDateTime(reader["updatedOn"]);

                                oMappers.Add(oMapper);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return oMappers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public StudentCourseMapper GetStudentCourseMapperRecordsByCourseUserId(int userId, int courseId, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetStudentCourseMapperRecordsByCourseUserId, userId, courseId);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    StudentCourseMapper oMapper = new StudentCourseMapper();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                oMapper.Id = Convert.ToInt32(reader["Id"]);
                                oMapper.UserId = Convert.ToInt32(reader["userId"]);
                                oMapper.CourseId = Convert.ToInt32(reader["courseId"]);
                                oMapper.IsEnrolled = Convert.ToBoolean(reader["IsEnrolled"]);
                                oMapper.MappedOn = Convert.ToDateTime(reader["mappedOn"]);
                                oMapper.UpdatedOn = Convert.ToDateTime(reader["updatedOn"]);
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }
                        }
                    }

                    con.Close();
                    return oMapper;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CourseDetail> GetMappedCourseByStudent(int userid, string cnxnString, string logPath)
        {
            Database db = new Database();
            try
            {
                string sCmdText = string.Format(Q_GetMappedCourseByStudent, userid);

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    DataSet ds = db.SelectDataSet(sCmdText, logPath, con);
                    List<CourseDetail> courselist = new List<CourseDetail>();

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            CourseDetail course = new CourseDetail();
                            course.Id = Convert.ToInt32(row["Id"]);
                            course.CourseName = row["CourseName"].ToString();
                            courselist.Add(course);
                        }
                    }

                    con.Close();
                    return courselist;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserDetails> GetStudentsByTest(int testId, string course, string cnxnString, string logPath)
        {
            try
            {
                string result = string.Format(Q_GetStudentsByTest, testId, course);
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    List<UserDetails> userlist = new List<UserDetails>();

                    using (DbDataReader reader = db.Select(result, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UserDetails user = new UserDetails();
                                user.Id = Convert.ToInt32(reader["Id"]);
                                user.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                                user.LastLogin = Convert.ToDateTime(reader["LastLogin"].ToString());
                                user.FirstName = ParameterFormater.UnescapeXML(reader["FirstName"].ToString());
                                user.LastName = ParameterFormater.UnescapeXML(reader["LastName"].ToString());
                                user.FatherName = ParameterFormater.UnescapeXML(reader["FatherName"].ToString());
                                user.MotherName = ParameterFormater.UnescapeXML(reader["MotherName"].ToString());
                                user.EmailAddress = reader["EmailAddress"].ToString();
                                user.UserType = reader["userType"].ToString();
                                user.UserName = reader["username"].ToString();
                                user.Password = reader["Password"].ToString();
                                user.MobileNo = reader["MobileNo"].ToString();
                                user.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                user.IsEnable = Convert.ToBoolean(reader["IsEnabled"]);
                                user.IsCompleted = Convert.ToBoolean(reader["IsCompleted"]);
                                user.IsCertified = Convert.ToBoolean(reader["IsCertified"]);
                                user.VersionRegister = reader["VersionRegistered"].ToString();
                                user.MobileNo = reader["MobileNo"].ToString();
                                user.RollNumber = reader["RollNumber"].ToString();
                                user.IsNewUser = Convert.ToBoolean(reader["IsNewUser"]);
                                user.Course = ParameterFormater.UnescapeXML(reader["Course"].ToString());
                                user.SubCourse = ParameterFormater.UnescapeXML(reader["SubCourse"].ToString());
                                user.BatchYear = Convert.ToInt32(reader["BatchYear"]);
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
                throw ex;
            }
        }
        #endregion

        #region Update functions
        public void UpdateStudentCourseMapper(int userId, int courseId, bool isEnrolled, string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_UpdateStudentCourseMapper, userId, courseId, isEnrolled, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"));
                    Database db = new Database();
                    db.Update(cmdText, logPath, con);
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
