using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class CourseTests
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Chapters { get; set; }
        public bool IsTimebound { get; set; }
        public int TimeLimit { get; set; }
        public int TotalQuestions { get; set; }
        public bool IsTestActive { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }

        public string DisplayStartDate { get; set; }
        public string DisplayEndDate { get; set; }
    }
}
