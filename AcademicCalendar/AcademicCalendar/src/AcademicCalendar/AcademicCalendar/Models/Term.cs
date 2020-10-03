using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicCalendar.Models
{
    [Table("terms")]
    public class Term
    {
        public Term()
        {
            Subjects = new List<Subject>();
        }

        [Column("id")]
        public int Id { get; set; }

        [MaxLength(255)]
        [Column("name")]
        public string Name { get; set; }

        [Column("year_id")]
        public int YearId { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }

        public Year Year { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Term)
            {
                return (obj as Term).Id == Id;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
