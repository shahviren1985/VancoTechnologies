using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicCalendar.Models
{
    [Table("years")]
    public class Year
    {
        public Year()
        {
            Terms = new List<Term>();
        }

        [Column("id")]
        public int Id { get; set; }

        [MaxLength(255)]
        [Column("code")]
        public string Code { get; set; }

        public virtual ICollection<Term> Terms { get; set; }

        public override string ToString()
        {
            return Code;
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is Year)
            {
                return (obj as Year).Id == Id;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
