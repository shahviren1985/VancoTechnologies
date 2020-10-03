using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicCalendar.Models
{
    [Table("faculties")]
    public class Faculty
    {
        public Faculty()
        {
            FacultySubjects = new List<FacultySubject>();
        }

        [Column("id")]
        public int Id { get; set; }

        [MaxLength(255)]
        [Column("name")]
        public string Name { get; set; }

        public virtual ICollection<FacultySubject> FacultySubjects { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
