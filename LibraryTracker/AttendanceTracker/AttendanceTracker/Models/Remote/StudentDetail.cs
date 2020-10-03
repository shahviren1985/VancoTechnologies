using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTracker.Models.Remote
{
    [Table("student_details")]
    public class StudentDetail
    {
        [DisplayName("Roll Number")]
        [Column("roll_number")]
        public int? RollNumber { get; set; }

        [DisplayName("Library Card")]
        [Column("library_card_no")]
        public string LibraryCardNo { get; set; }

        [DisplayName("CRN")]
        [Column("userID")]
        public string UserId { get; set; }

        [Column("id")]
        public int Id { get; set; }

        [DisplayName("First Name")]
        [Column("first_name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Column("last_name")]
        public string LastName { get; set; }

        [Column("course_name")]
        public string CourseName { get; set; }

        [Column("specialization")]
        public string Specialization { get; set; }

        [Column("division")]
        public string Division { get; set; }

        [Column("PRN_number")]
        public string PrnNumber { get; set; }

        [Column("photo_path")]
        public string PhotoPath { get; set; }

        [Column("course_id")]
        public int CourseId { get; set; }

        [Column("admission_year")]
        public string AdmissionYear { get; set; }
    }
}
