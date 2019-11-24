using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class ChapterQuizMaster
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int ChapterId { get; set; }
        public int SectionId { get; set; }
        public string QuestionText { get; set; }
        public bool IsQuestionOptionPresent { get; set; }
        public string QuestionOption { get; set; }
        public string AnswerOption { get; set; }
        public string ContentVersion { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }        
        public List<QuestionOptions> QuestionOptionList { get; set; }
        public List<AnswerOptions> AnswerOptionList { get; set; }

        // added by anup for reterving given response of user on prvious click.
        public bool IsAnsGiven { get; set; }
        public bool IsCorrect { get; set; }
        public string AnswerText { get; set; }

        // addede by anup for reterving question by courseId
        public string ChapterName { get; set; }
        public string SectionName { get; set; }

        // added by vasim for multi select(multi true) answer on 27-jan-2014.
        public bool IsMultiTrueAnswer { get; set; }

        public int MigratedChapterQuizId { get; set; }
    }
}
