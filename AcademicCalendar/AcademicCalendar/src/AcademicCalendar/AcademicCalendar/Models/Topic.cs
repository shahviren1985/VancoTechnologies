using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicCalendar.Models
{
    [Table("topics")]
    public class Topic
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("lecture_no")]
        public int LectureNo { get; set; }

        [MaxLength(255)]
        [Column("name")]
        public string Name { get; set; }

        [Column("faculty_subject_id")]
        public int FacultySubjectId { get; set; }

        public virtual FacultySubject FacultySubject { get; set; }
    }
}
