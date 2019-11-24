using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITM.DAOBase;
using ITM.LogManager;
using ITM.Util;
using System.Data.Common;


namespace ITM.DAO
{
    public class CourseDAO
    {

        #region Query
        public string SELECT_COURSE_NAME = "SELECT Id, CourseName FROM coursedetails";
        public string SELECT_SUBCOURSE_NAME = "SELECT * FROM subcoursedetails s where courseid = '{0}'";
        public string SELECT_SUBJECT_NAME = " SELECT * FROM subjectcoursemapper s inner join subjectmaster sm on sm.id = s.subjectid where s.courseid = '{0}' group by subjectid order by s.subjectid asc;";
        public string SELECT_PAPER_NAME = "SELECT * FROM papermaster p  where p.courseid = '{0}' and p.subjectid = '{1}'  group by papercode;";
        //public string Q_GetMappedSubjects_Courses = "Select Id, SubjectName, ShortForm, Id as SMId from subjectmaster where id in (select distinct subjectid from subjectcoursemapper where courseid={0});";
        #endregion

        #region function

        public List<Course> GetCourseNameForDropDown(string connString, string logPath)
        {
            try
            {
                string result = string.Format(SELECT_COURSE_NAME);
                Database db = new Database();
                DbDataReader reader = db.Select(result, connString, logPath);
                if (reader.HasRows)
                {
                    List<Course> courseList = new List<Course>();
                    while (reader.Read())
                    {
                        Course Name = new Course();
                        Name.CourseId = Convert.ToInt32(reader["Id"]);
                        Name.CourseName = reader["CourseName"].ToString();
                        courseList.Add(Name);
                    }
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                    return courseList;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }



        public List<Course> GetSubCourseNameForDropDown(string courseId, string connString, string logPath)
        {
            try
            {
                string result = string.Format(SELECT_SUBCOURSE_NAME, courseId);
                Database db = new Database();
                DbDataReader reader = db.Select(result, connString, logPath);
                if (reader.HasRows)
                {
                    List<Course> courseList = new List<Course>();
                    while (reader.Read())
                    {
                        Course Name = new Course();
                        Name.SubCourseId = Convert.ToInt32(reader["Id"]);
                        Name.SubCourseName = reader["SubCourseName"].ToString();
                        courseList.Add(Name);
                    }
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                    return courseList;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }


        //public List<Course> GetSubjectListByCourseId(int courseId, int subcourseid, string connString, string logPath)
        public List<Course> GetSubjectListByCourseId(int courseId, string connString, string logPath)
        {
            try
            {
                string result = string.Format(SELECT_SUBJECT_NAME, courseId);
                Database db = new Database();
                DbDataReader reader = db.Select(result, connString, logPath);
                if (reader.HasRows)
                {
                    List<Course> courseList = new List<Course>();
                    while (reader.Read())
                    {
                        Course Name = new Course();
                        Name.SubjectId = Convert.ToInt32(reader["SubjectId"]);
                        Name.SubjectName = reader["SubjectName"].ToString();
                        courseList.Add(Name);
                    }
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                    return courseList;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }


        public List<Course> GetPaperListByCourseId(int courseId, int subjectid, string connString, string logPath)
        {
            try
            {
                string result = string.Format(SELECT_PAPER_NAME, courseId, subjectid);
                Database db = new Database();
                DbDataReader reader = db.Select(result, connString, logPath);
                if (reader.HasRows)
                {
                    List<Course> courseList = new List<Course>();
                    while (reader.Read())
                    {
                        Course Name = new Course();
                        Name.PaperId = Convert.ToInt32(reader["Id"]);
                        Name.PaperName = reader["PaperTitle"].ToString();
                        courseList.Add(Name);
                    }
                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                    return courseList;
                }
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        #endregion




    }
}
