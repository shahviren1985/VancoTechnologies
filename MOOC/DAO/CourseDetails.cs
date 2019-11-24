using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class CourseDetail
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string EstimateTime { get; set; }
        public DateTime DateCreated { get; set; }
        public int StaffId { get; set; }
        public string OrignalCourseName { get; set; }
        public int MigratedCourseId { get; set; }
        public bool IsPublic { get; set; }
    }
}
