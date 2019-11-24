using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    class SummaryReportData
    {
    }

    public class CompletedCoursesCount
    {
        public string Course { get; set; }
        public int NumberOfStudents { get; set; }
    }

    public class NotStartedCourseCount
    {
        public string Course { get; set; }
        public int NumberOfStudents { get; set; }
    }
}
