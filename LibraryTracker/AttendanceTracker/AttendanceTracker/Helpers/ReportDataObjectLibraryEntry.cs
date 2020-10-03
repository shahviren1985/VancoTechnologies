using System;

namespace AttendanceTracker.Helpers
{
    public class ReportDataObjectLibraryEntry
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string CRN { get; set; }

        public DateTime In { get; set; }

        public DateTime? Out { get; set; }

        public string ToCsvLine()
        {
            return $"{Id}\t{Name}\t{CRN}\t{In}\t{Out}";
        }

        public static string GetCsvHeader()
        {
            return "Id\tName\tCRN\tIn\tOut";
        }
    }
}
