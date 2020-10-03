using System.Data.Common;
using System.Data.Entity;

namespace AttendanceTracker.Models.Remote
{
    public class RemoteContext : DbContext
    {
        public DbSet<CourseDetail> CourseDetails { get; set; }

        public DbSet<LibraryLog> LibraryLogs { get; set; }

        public DbSet<StaffDetail> StaffDetails { get; set; }

        public DbSet<StudentDetail> StudentDetails { get; set; }

        public RemoteContext() : base()
        {

        }

        public RemoteContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {

        }
    }
}
