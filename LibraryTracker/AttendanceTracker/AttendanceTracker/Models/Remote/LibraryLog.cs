using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTracker.Models.Remote
{
    [Table("library_log")]
    public class LibraryLog
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("id_in_attendance_tracker")]
        public int IdInAttendanceTracker { get; set; }

        [Column("id_card_number")]
        public string IdCardNumber { get; set; }

        [Column("crn")]
        public string CRN { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("in_time")]
        public DateTime InTime { get; set; }

        [Column("out_time")]
        public DateTime? OutTime { get; set; }
    }
}
