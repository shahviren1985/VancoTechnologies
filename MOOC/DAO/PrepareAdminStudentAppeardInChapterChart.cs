using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    #region Chart data classes for Line Chart (Chapter Appeard by student)
    public class StudentAppeardChart
    {
        public List<string> labels { get; set; }
        public List<StudentAppeardDataSets> datasets { get; set; }
    }

    public class StudentAppeardDataSets
    {
        public string fillColor { get; set; }
        public string strokeColor { get; set; }
        public string pointColor { get; set; }
        public string pointStrokeColor { get; set; }
        public List<int> data { get; set; }
    }
    #endregion

    #region Chart Function class for line chart (Chapter Appeard by student)
    public class PrepareAdminStudentAppeardInChapterChart
    {
        public StudentAppeardChart GetStudentAppeardInChapterChartData(string cnxnString, string logPath)
        {
            try
            {
                List<string> Labels = new List<string>();
                List<StudentAppeardDataSets> chartDataSet = new List<StudentAppeardDataSets>();

                List<int> totalStudents = new List<int>();
                List<int> appeardStudents = new List<int>();

                List<UserDetails> users = new UserDetailsDAO().GetAllUsers(false, cnxnString, logPath);
                List<ChapterDetails> chapters = new ChapterDetailsDAO().GetAllChapterDetails(cnxnString, logPath);

                if (users != null && chapters != null)
                {
                    foreach (ChapterDetails chapter in chapters)
                    {
                        Labels.Add(chapter.ChapterName);
                        int appeardStudent = 0;

                        foreach (UserDetails user in users)
                        {
                            if (IsStudentAppeardInChapter(user.Id, chapter.Id, cnxnString, logPath))
                            {
                                appeardStudent++;
                            }
                        }

                        totalStudents.Add(users.Count);
                        appeardStudents.Add(appeardStudent);
                    }

                    StudentAppeardDataSets dataTotalStudents = new StudentAppeardDataSets();
                    dataTotalStudents.fillColor = "rgba(220,220,220,0.5)";
                    dataTotalStudents.strokeColor = "rgba(220,220,220,1)";
                    dataTotalStudents.pointColor = "rgba(220,220,220,1)";
                    dataTotalStudents.pointStrokeColor = "#fff";
                    dataTotalStudents.data = totalStudents;

                    StudentAppeardDataSets dataAppeardStudents = new StudentAppeardDataSets();
                    dataAppeardStudents.fillColor = "rgba(151,187,205,0.5)";
                    dataAppeardStudents.strokeColor = "rgba(151,187,205,1)";
                    dataAppeardStudents.pointColor = "rgba(151,187,205,1)";
                    dataAppeardStudents.pointStrokeColor = "#fff";
                    dataAppeardStudents.data = appeardStudents;

                    chartDataSet.Add(dataTotalStudents);
                    chartDataSet.Add(dataAppeardStudents);

                    StudentAppeardChart chart = new StudentAppeardChart();
                    chart.labels = Labels;
                    chart.datasets = chartDataSet;
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }

            return null;
        }

        public bool IsStudentAppeardInChapter(int userId, int chapterId, string cnxnString, string logPath)
        {


            return true;
        }
    }
    #endregion
}
