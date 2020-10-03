using System.Data.Entity;
using MySql.Data.Entity;
using System.Data.Common;
using ExcelDataExtraction.Model;

namespace ExcelDataExtraction
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public sealed class ExcelDataExtractionDbContext : DbContext
    {
        public ExcelDataExtractionDbContext() : base() { }
        public ExcelDataExtractionDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection) { }

        public DbSet<StudentDetails> StudentDetails { get; set; }
        public DbSet<ResultSummary> ResultSummaries { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
