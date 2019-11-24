using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class StudentCourseMapper
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public bool IsEnrolled { get; set; }
        public DateTime MappedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
