using System;

namespace AcademicCalendar.Helpers
{
    class TopicData
    {
        public string Subject { get; set; }

        public string Topic { get; set; }

        public string Faculty { get; set; }

        public override string ToString()
        {
            return $"{Subject}{Environment.NewLine}{Topic}{Environment.NewLine}{Faculty}";
        }
    }
}
