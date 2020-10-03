using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTracker.Models.Remote
{
    [Table("course_details")]
    public class CourseDetail
    {
        [Column("id")]
        public int? Id { get; set; }

        [Column("course_name")]
        public string CourseName { get; set; }

        [Column("course_type")]
        public string CourseType { get; set; }

        [Column("short_form")]
        public string ShortForm { get; set; }

        [Column("specialization")]
        public string Specialization { get; set; }

        [Column("status")]
        public string Status { get; set; }

        [Column("year")]
        public string Year { get; set; }
    }
}
