using System.ComponentModel;

namespace AttendanceTracker.Helpers
{
    public class ReportDataObjectHoursSpent
    {
        public string CRN { get; set; }

        public string Name { get; set; }

        public string Specialization { get; set; }

        public string Month { get; set; }

        [DisplayName("Hours Spent")]
        public double HoursSpent { get; set; }

        public string ToCsvLine()
        {
            return $"{CRN}\t{Name}\t{Specialization}\t{Month}\t{HoursSpent}";
        }

        public static string GetCsvHeader()
        {
            return "CRN\tName\tSpecialization\tMonth\tHours Spent";
        }
    }
}
