using System.Data.Common;
using System.Data.Entity;

namespace AcademicCalendar.Models
{
    public class CalContext : DbContext
    {
        public DbSet<CommonTimeslot> CommonTimeslots { get; set; }

        public DbSet<Day> Days { get; set; }

        public DbSet<Faculty> Faculties { get; set; }

        public DbSet<FacultySubject> FacultySubjects { get; set; }

        public DbSet<Holiday> Holidays { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Term> Terms { get; set; }

        public DbSet<Timeslot> Timeslots { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Year> Years { get; set; }

        public CalContext() : base()
        {

        }

        public CalContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Day>()
                        .HasMany<CommonTimeslot>(s => s.CommonTimeslots)
                        .WithMany(c => c.Days)
                        .Map(cs =>
                        {
                            cs.MapLeftKey("Day_Id");
                            cs.MapRightKey("Common_Timeslot_Id");
                            cs.ToTable("Day_Common_Timeslot");
                        });
        }
    }
}
