using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicCalendar.Models
{
    [Table("subjects")]
    public class Subject
    {
        public Subject()
        {
            FacultySubjects = new List<FacultySubject>();
            Timeslots = new List<Timeslot>();
        }

        [Column("id")]
        public int Id { get; set; }
        
        public string Year
        {
            get
            {
                return Term?.Year?.ToString() ?? string.Empty;
            }
        }

        public Term Term { get; set; }

        [DisplayName("Subject Name")]
        [MaxLength(255)]
        [Column("subject_name")]
        public string SubjectName { get; set; }

        [Column("term_id")]
        public int TermId { get; set; }

        public virtual ICollection<FacultySubject> FacultySubjects { get; set; }

        public virtual ICollection<Timeslot> Timeslots { get; set; }

        public override string ToString()
        {
            return $"{SubjectName}";
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Subject)
                return ((Subject)obj).Id == Id;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
