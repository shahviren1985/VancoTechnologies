using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTracker.Models.Local
{
    [Table("synchronizations")]
    public class Synchronization
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("synced_at")]
        public DateTime SyncedAt { get; set; }

        [Column("sync_type")]
        public string SyncType { get; set; }
    }
}
