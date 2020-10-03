using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicCalendar.Models
{
    [Table("days")]
    public class Day
    {
        public Day()
        {
            CommonTimeslots = new List<CommonTimeslot>();
            Timeslots = new List<Timeslot>();
        }

        [Column("id")]
        public int Id { get; set; }

        [MaxLength(255)]
        [Column("name")]
        public string Name { get; set; }

        [Column("order_on_week")]
        public int OrderOnWeek { get; set; }

        public virtual ICollection<CommonTimeslot> CommonTimeslots { get; set; }

        public virtual ICollection<Timeslot> Timeslots { get; set; }

        public override string ToString()
        {
            return Name; ;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj == System.DBNull.Value)
                return false;

            return ((Day)obj).OrderOnWeek == OrderOnWeek;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
