using System.ComponentModel;

namespace AcademicCalendar.Helpers
{
    public class TopicHelper
    {
        [DisplayName("Lecture No")]
        public int LectureNo { get; set; }

        [DisplayName("Topic")]
        public string Name { get; set; }

        public int FacultyId { get; set; }

        [DisplayName("Faculty")]
        public string FacultyName { get; set; }
    }
}
