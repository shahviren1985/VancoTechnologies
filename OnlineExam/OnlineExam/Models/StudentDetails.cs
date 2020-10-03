using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineExam.Models
{
    public class StudentDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Membership { get; set; }
        public string Status { get; set; }
        public string ExamId { get; set; }
        public string CertificatePdfLink { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string StartTime { get; set; }
        public string CorrectAnswers { get; set; }
        public string CollegeCode { get; set; }
    }

    public class UserResponse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string ExmaId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ElapsedTime { get; set; }
    }

    public class ExamTiming
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string ExmaId { get; set; }
        public string QuestionId { get; set; }
        public string UserResponse { get; set; }
        public string CorrectResponse { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class ValidateResponseModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int ElapsedTime { get; set; }
    }

    public class QuestionMaster
    {
        public string id { get; set; }
        public string question { get; set; }
        public string type { get; set; }
        public IList<string> optionValues { get; set; }
        public string placeHolder { get; set; }
        public string category { get; set; }
        public string correctAnswer { get; set; }
    }

    public class Questions
    {
        public string id { get; set; }
        public string question { get; set; }
        public string type { get; set; }
        public IList<string> optionValues { get; set; }
        public string placeHolder { get; set; }
        public string category { get; set; }
        public string answer { get; set; }
        public string correctResponse { get; set; }

    }

    public class ExamValidation
    {
        public bool IsExamValid { get; set; }
        public bool IsExamExpired { get; set; }
        public bool IsFinished { get; set; }
        // Time remaining in minutes
        public double TimeRemaining { get; set; }
        public DateTime StartTime { get; set; }
        public int Result { get; set; }

    }

    public class Answer
    {
        public int serial { get; set; }
        public string qid { get; set; }
        public string examid { get; set; }
        public string answer { get; set; }
        public string correctAnswer { get; set; }
        public string status { get; set; }
        public string category { get; set; }
        public string subjectCode { get; set; }
    }
}