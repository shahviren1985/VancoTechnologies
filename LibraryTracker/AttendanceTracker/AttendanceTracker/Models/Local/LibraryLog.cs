using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTracker.Models.Local
{
    [Table("library_log")]
    public class LibraryLog
    {
        [DisplayName("Id")]
        [Column("id")]
        public int Id { get; set; }

        [Column("id_card_number")]
        public string IdCardNumber { get; set; }

        [DisplayName("CRN")]
        [Column("crn")]
        public string CRN { get; set; }

        [DisplayName("Date")]
        [Column("date")]
        public DateTime Date { get; set; }

        [DisplayName("In Time")]
        [Column("in_time")]
        public DateTime InTime { get; set; }

        [DisplayName("Out Time")]
        [Column("out_time")]
        public DateTime? OutTime { get; set; }

        [Column("pushed")]
        public bool Pushed { get; set; }
    }
}
