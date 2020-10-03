using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTracker.Models.Local
{
    [Table("student_details")]
    public class StudentDetail
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("crn")]
        public string CRN { get; set; }

        [Column("library_card_no")]
        public string LibraryCardNo { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("course_details_year")]
        public string CourseDetailsYear { get; set; }

        [Column("course_details_course_name")]
        public string CourseDetailsCourseName { get; set; }

        [Column("course_name")]
        public string CourseName { get; set; }

        [Column("specialization")]
        public string Specialization { get; set; }

        [Column("roll_number")]
        public int? RollNumber { get; set; }

        [Column("division")]
        public string Division { get; set; }

        [Column("PRN_number")]
        public string PrnNumber { get; set; }

        [Column("photo_path")]
        public string PhotoPath { get; set; }

        [Column("admission_year")]
        public string AdmissionYear { get; set; }
    }
}
