using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class PrintStudentCertifiacateBackProps
    {
        public string StudentFullName { get; set; }
        public string Cousre { get; set; }
        public string Stream { get; set; }
        public List<ChapterPerformanceDetails> ChaptersPerformances { get; set; }
        public List<TypingPerformanceDetails> TypingPerformances { get; set; }
    }

    public class ChapterPerformanceDetails
    {
        public string ChapterName { get; set; }
        public int CorrectQuizCount { get; set; }
        public int TotalQuizCount { get; set; }
        public decimal ChapterQuizScore { get; set; }
    }

    public class TypingPerformanceDetails
    {
        public int Level { get; set; }
        public int Speed_WPM { get; set; }
        public int Accuracy { get; set; }
    }
}
