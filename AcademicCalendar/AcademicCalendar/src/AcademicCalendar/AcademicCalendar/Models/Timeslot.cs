using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicCalendar.Models
{
    [Table("timeslots")]
    public class Timeslot
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("from")]
        public TimeSpan From { get; set; }

        [Column("to")]
        public TimeSpan To { get; set; }

        [Column("day_id")]
        public int DayId { get; set; }

        [Column("subject_id")]
        public int SubjectId { get; set; }

        public virtual Day Day { get; set; }

        public virtual Subject Subject { get; set; }

        public override bool Equals(object obj)
        {
            var t = obj as Timeslot;

            if (t == null)
            {
                return false;
            }

            return From == t.From && To == t.To && DayId == t.DayId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
