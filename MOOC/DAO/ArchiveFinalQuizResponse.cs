using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITM.Courses.DAO
{
    public class ArchiveFinalQuizResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string UserResponse { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime DateTime { get; set; }
    }
}
