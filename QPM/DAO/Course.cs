using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.DAO
{
    public class Course
    {
        //this is for course
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string UniversityCourseName { get; set; }
        public String Medium { get; set; }
        public string CourseHistory { get; set; }

        //this is for subcourse
        public int SubCourseId { get; set; }
        public string SubCourseName { get; set; }
        public string SubCourseHistory { get; set; }

        //this is for subject
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string ShortForm { get; set; }
        public bool IsActive { get; set; }
        public string History { get; set; }

        //this is for paper
        public int PaperId { get; set; }
        public string PaperName { get; set; }
        

        

    }
}
