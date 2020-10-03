using System.Data.Common;
using System.Data.Entity;

namespace AttendanceTracker.Models.Local
{
    public class LocalContext : DbContext
    {
        public DbSet<CourseDetail> CourseDetails { get; set; }

        public DbSet<LibraryLog> LibraryLogs { get; set; }

        public DbSet<StaffDetail> StaffDetails { get; set; }

        public DbSet<StudentDetail> StudentDetails { get; set; }

        public DbSet<Synchronization> Synchronizations { get; set; }

        public LocalContext() : base()
        {

        }

        public LocalContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
