using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.ConfigurationsManager
{
    public class AutoSendReportConfig
    {
        public bool IsSendCourseCompletedReport { get; set; }
        public bool IsSendChapterWiseProgressReport { get; set; }
        public bool IsSendStepWiseProgressReport { get; set; }
        public bool IsSendTypingStatsReport { get; set; }
        public bool IsSendNotStartedCourseReport { get; set; }
        public bool IsSendNotStartedTypingReport { get; set; }
        public bool IsSendCompletedCourseFinalQuizTypingReport { get; set; }
    }
}
