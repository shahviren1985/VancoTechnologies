using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVT.Business.Model
{
    public class PreviewStudentDetails
    {
        public int Id { get; set; }

        public int SerialNumber { get; set; }

        public string FullName { get; set; }

        public string FatherName { get; set; }

        public string MotherName { get; set; }

        public decimal? Percentage { get; set; }

        public string InternalExternal { get; set; }

        public string Category { get; set; }

        public string Caste { get; set; }

        public string CourseAdmittedRound1 { get; set; }

        public string CourseAdmittedRound2 { get; set; }

        public string PossibleCourseAdmitted { get; set; }

        public string CoursePreference1 { get; set; }

        public string CoursePreference2 { get; set; }

        public string CoursePreference3 { get; set; }

        public string CoursePreference4 { get; set; }

        public string CoursePreference5 { get; set; }

        public string CoursePreference6 { get; set; }

        public string CoursePreference7 { get; set; }

    }

    public class MeritStudentDetails : BaseClass
    {
       public List<PreviewStudentDetails> StudentList { get; set; }
    }

}
