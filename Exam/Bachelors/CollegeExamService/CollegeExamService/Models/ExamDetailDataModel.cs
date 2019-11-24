using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeExamService.Models
{
    public class ExamDetailDataModel
    {
        [JsonProperty("studentDetailsList")]
        public List<StudentDetailsDataModel> StudentDetailsList { get; set; }

        [JsonProperty("studentExamDetailList")]
        public List<StudentExamDetail> StudentExamDetailList { get; set; }

        [JsonProperty("marksdetailList")]
        public List<MarksdetailDataModel> MarksdetailList { get; set; }

        [JsonProperty("atktMarksDetailList")]
        public List<AtktMarksDetail> AtktMarksDetailList { get; set; }
    }

    public class StudentDetailsDataModel
    {

        [JsonProperty("id")]
        public object Id { get; set; }

        [JsonProperty("admissionYear")]
        public int AdmissionYear { get; set; }

        [JsonProperty("collegeRegistrationNumber")]
        public string CollegeRegistrationNumber { get; set; }

        [JsonProperty("prn")]
        public string Prn { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("fatherName")]
        public string FatherName { get; set; }

        [JsonProperty("motherName")]
        public string MotherName { get; set; }

        [JsonProperty("stream")]
        public string Stream { get; set; }

        [JsonProperty("course")]
        public string Course { get; set; }

        [JsonProperty("specialisation")]
        public string Specialisation { get; set; }

        [JsonProperty("currentAddress")]
        public string CurrentAddress { get; set; }

        [JsonProperty("emailAddress")]
        public string EmailAddress { get; set; }

        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }

        [JsonProperty("learningDisability")]
        public bool LearningDisability { get; set; }
    }

    public class StudentExamDetail
    {

        [JsonProperty("id")]
        public object Id { get; set; }

        [JsonProperty("studentId")]
        public int StudentId { get; set; }

        [JsonProperty("examName")]
        public string ExamName { get; set; }

        [JsonProperty("examType")]
        public string ExamType { get; set; }

        [JsonProperty("semester")]
        public int Semester { get; set; }

        [JsonProperty("yearOfExam")]
        public int yearOfExam { get; set; }

        [JsonProperty("monthOfExam")]
        public string monthOfExam { get; set; }
    }

    public class MarksdetailDataModel
    {

        [JsonProperty("id")]
        public object Id { get; set; }

        [JsonProperty("studentId")]
        public int StudentId { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("semester")]
        public int Semester { get; set; }

        [JsonProperty("seatNumber")]
        public string SeatNumber { get; set; }

        [JsonProperty("credit")]
        public int Credit { get; set; }

        [JsonProperty("isGrade")]
        public bool IsGrade { get; set; }

        [JsonProperty("paperCode")]
        public string PaperCode { get; set; }

        [JsonProperty("paperTitle")]
        public string PaperTitle { get; set; }

        [JsonProperty("paperType")]
        public string PaperType { get; set; }

        [JsonProperty("isElective")]
        public bool IsElective { get; set; }

        [JsonProperty("internalPassingMarks")]
        public int InternalPassingMarks { get; set; }

        [JsonProperty("internalmarksobtained")]
        public float Internalmarksobtained { get; set; }

        [JsonProperty("externalPassingMarks")]
        public int ExternalPassingMarks { get; set; }

        [JsonProperty("internalTotalMarks")]
        public int InternalTotalMarks { get; set; }

        [JsonProperty("externalSection1Marks")]
        public string ExternalSection1Marks { get; set; }

        [JsonProperty("externalSection2Marks")]
        public string ExternalSection2Marks { get; set; }

        [JsonProperty("externalTotalMarks")]
        public float ExternalTotalMarks { get; set; }

        [JsonProperty("externalMaxMarks")]
        public int ExternalMaxMarks { get; set; }

        [JsonProperty("practicalMarksObtained")]
        public string PracticalMarksObtained { get; set; }

        [JsonProperty("practicalMaxMarks")]
        public int PracticalMaxMarks { get; set; }

        [JsonProperty("graceMarks")]
        public int GraceMarks { get; set; }

        [JsonProperty("paperResult")]
        public string PaperResult { get; set; }

        [JsonProperty("gp")]
        public string Gp { get; set; }

        [JsonProperty("grade")]
        public string Grade { get; set; }

        [JsonProperty("attempt")]
        public string Attempt { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }

        [JsonProperty("printedCommon4")]
        public bool PrintedCommon4 { get; set; }

        [JsonProperty("printedCommon6")]
        public bool PrintedCommon6 { get; set; }
        [JsonProperty("status")]
        public bool status { get; set; }

        [JsonProperty("RetryCount")]
        public int RetryCount { get; set; }
    }

    public class AtktMarksDetail
    {

        [JsonProperty("id")]
        public object Id { get; set; }

        [JsonProperty("studentId")]
        public int StudentId { get; set; }

        [JsonProperty("examId")]
        public int ExamId { get; set; }

        [JsonProperty("seatNumber")]
        public string SeatNumber { get; set; }

        [JsonProperty("credit")]
        public int Credit { get; set; }

        [JsonProperty("isGrade")]
        public bool IsGrade { get; set; }

        [JsonProperty("paperCode")]
        public string PaperCode { get; set; }

        [JsonProperty("paperTitle")]
        public string PaperTitle { get; set; }

        [JsonProperty("paperType")]
        public string PaperType { get; set; }

        [JsonProperty("isElective")]
        public bool IsElective { get; set; }

        [JsonProperty("internalPassingMarks")]
        public int InternalPassingMarks { get; set; }

        [JsonProperty("internalmarksobtained")]
        public float Internalmarksobtained { get; set; }

        [JsonProperty("externalPassingMarks")]
        public int ExternalPassingMarks { get; set; }

        [JsonProperty("internalTotalMarks")]
        public int InternalTotalMarks { get; set; }

        [JsonProperty("externalSection1Marks")]
        public string ExternalSection1Marks { get; set; }

        [JsonProperty("externalSection2Marks")]
        public string ExternalSection2Marks { get; set; }

        [JsonProperty("externalTotalMarks")]
        public float ExternalTotalMarks { get; set; }

        [JsonProperty("practicalMarksObtained")]
        public string PracticalMarksObtained { get; set; }

        [JsonProperty("practicalMaxMarks")]
        public int PracticalMaxMarks { get; set; }

        [JsonProperty("graceMarks")]
        public int GraceMarks { get; set; }

        [JsonProperty("paperResult")]
        public string PaperResult { get; set; }

        [JsonProperty("gp")]
        public string Gp { get; set; }

        [JsonProperty("grade")]
        public string Grade { get; set; }

        [JsonProperty("attempt")]
        public string Attempt { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }

        [JsonProperty("externalMaxMarks")]
        public int ExternalMaxMarks { get; set; }
        [JsonProperty("status")]
        public bool status { get; set; }

    }

    public class StudentATKTDetails
    {
        [JsonProperty("studentDetails")]
        public StudentDetailsDataModel StudentDetails { get; set; }

        [JsonProperty("atktMarksDetailList")]
        public List<AtktMarksDetail> AtktMarksDetailList { get; set; }

        [JsonProperty("semester")]
        public string Semester { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }
    }

    public class ExamPaperDataModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("paperCode")]
        public string  PaperCode { get; set; }

        [JsonProperty("paperTitle")]
        public string PaperTitle { get; set; }

        [JsonProperty("paper1")]
        public string Paper1 { get; set; }

        [JsonProperty("paper2")]
        public string Paper2 { get; set; }

        [JsonProperty("paper3")]
        public string Paper3 { get; set; }

        [JsonProperty("examYear")]
        public string ExamYear { get; set; }

        [JsonProperty("examMonth")]
        public string ExamMonth { get; set; }

        [JsonProperty("semester")]
        public string Semester { get; set; }

        [JsonProperty("course")]
        public string Course { get; set; }

        [JsonProperty("specialisation")]
        public string Specialisation { get; set; }

        [JsonProperty("examType")]
        public string ExamType { get; set; }

        [JsonProperty("uploadedBy")]
        public string UploadedBy { get; set; }

        [JsonProperty("uploadedDate")]
        public DateTime UploadedDate { get; set; }

        [JsonProperty("randomSelectedPaper")]
        public string RandomSelectedPaper { get; set; }

        [JsonProperty("randomGenerationDate")]
        public DateTime RandomGenerationDate { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("dateDeleted")]
        public DateTime DateDeleted { get; set; }
    }
}