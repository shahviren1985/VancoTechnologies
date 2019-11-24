using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class QuestionOptions
    {
        public string QuestionOption { get; set; }
        public string File { get; set; }
    }

    public class AnswerOptions
    {
        public string AnswerOption { get; set; }
        public bool IsCurrect { get; set; }
    }
}
