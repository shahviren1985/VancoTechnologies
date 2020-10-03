using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicCalendar.Models
{
    [Table("faculty_subject")]
    public class FacultySubject
    {
        public FacultySubject()
        {
            Topics = new List<Topic>();
        }

        [Column("id")]
        public int Id { get; set; }

        [Column("faculty_id")]
        public int FacultyId { get; set; }

        [Column("subject_id")]
        public int SubjectId { get; set; }

        public Faculty Faculty { get; set; }

        public Subject Subject { get; set; }

        public virtual ICollection<Topic> Topics { get; set; }
    }
}
