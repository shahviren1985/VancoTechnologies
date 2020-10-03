using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicCalendar.Models
{
    [Table("common_timeslots")]
    public class CommonTimeslot
    {
        public CommonTimeslot()
        {
            Days = new List<Day>();
        }

        [Column("id")]
        public int Id { get; set; }

        [MaxLength(255)]
        [Column("name")]
        public string Name { get; set; }

        [Column("from")]
        public TimeSpan From { get; set; }

        [Column("to")]
        public TimeSpan To { get; set; }

        public virtual ICollection<Day> Days { get; set; }
    }
}
