using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicCalendar.Models
{
    [Table("holidays")]
    public class Holiday
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [MaxLength(255)]
        [DisplayName("Holiday")]
        [Column("name")]
        public string Name { get; set; }
    }
}
