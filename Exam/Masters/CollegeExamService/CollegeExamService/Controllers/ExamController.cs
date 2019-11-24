using CollegeExamService.Helpers;
using CollegeExamService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace CollegeExamService.Controllers
{
    public class ExamController : ApiController
    {
        ExamDbOperations db = null;
        public ExamController()
        {
            db = new ExamDbOperations();
        }

        [HttpPost]
        public HttpResponseMessage SaveMarksDetailsInDb([FromBody]List<MarksEntryViewModel> models, [FromUri]int admissionYear,
             [FromUri]string stream, [FromUri]string examName, [FromUri]string examType, [FromUri]int semester, [FromUri]int year, [FromUri]string month)
        {
            HttpResponseMessage result = null;
            try
            {
                int count = 0;
                if (models == null || models.Count == 0)
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });

                foreach (MarksEntryViewModel model in models)
                {
                    if (model == null || model.CollegeRegistrationNumber == null)
                    {
                        count++;
                        continue;
                    }

                    StudentDetailsDataModel sdtModel = new StudentDetailsDataModel();
                    sdtModel.AdmissionYear = admissionYear;
                    sdtModel.CollegeRegistrationNumber = model.CollegeRegistrationNumber;
                    sdtModel.Prn = string.IsNullOrEmpty(model.PRN) ? "" : model.PRN.Replace("'", "");
                    sdtModel.LastName = model.LastName;
                    sdtModel.FirstName = model.FirstName;
                    sdtModel.FatherName = model.FatherName;
                    sdtModel.MotherName = model.MotherName;
                    sdtModel.Stream = stream;
                    sdtModel.Course = model.Course.Replace("S.Y.", "T.Y.");
                    sdtModel.Specialisation = model.Specialisation;
                    int studentId = db.CheckIfStudentDetailsExist(sdtModel.CollegeRegistrationNumber);
                    if (studentId <= 0)
                    {
                        studentId = db.InsertStudentDetails(sdtModel);
                    }
                    else
                    {
                        sdtModel.Id = studentId;
                        db.UpdateStudentDetails(sdtModel);
                    }

                    int examId = db.CheckIfExamDetailsExist(examName, examType, semester.ToString(), year, month);
                    if (examId <= 0)
                    {
                        StudentExamDetail dtModel = new StudentExamDetail();
                        dtModel.StudentId = studentId;
                        dtModel.ExamName = examName;
                        dtModel.ExamType = examType;
                        dtModel.Semester = semester;
                        dtModel.yearOfExam = year;
                        dtModel.monthOfExam = month;

                        examId = db.InsertStudentExamDetails(dtModel);
                    }

                    if (!string.IsNullOrEmpty(model.Code1))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit1) ? 0 : Convert.ToInt16(model.Credit1);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade1) ? false : true;
                        dtModel.PaperCode = model.Code1;
                        dtModel.PaperTitle = model.PaperTitle1;
                        dtModel.PaperType = model.PaperType1;
                        dtModel.IsElective = model.IsElective1;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks1;
                        dtModel.Internalmarksobtained = model.InternalC1;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks1;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C1;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C1;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC1;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks1;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC1;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks1;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks1;
                        dtModel.GraceMarks = string.IsNullOrEmpty(model.Grace1) ? 0 : Convert.ToUInt16(model.Grace1);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade1;
                        dtModel.Attempt = model.Attempt1;
                        dtModel.Remarks = model.Remarks;
                        //Retry total count of exams done for same subject...
                        // VS - Uncomment for ATKT 
                        int Retry = db.GetRetryCountForSubjectOfStudent(studentId, model.Code1, model.PaperType1);
                        dtModel.RetryCount = (Retry + 1);
                        db.UpdateSubjectStatusFromATKT(studentId, dtModel.PaperCode, model.PaperType1);

                        // VS - Uncomment 
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code1, model.PaperType1);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code2))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit2) ? 0 : Convert.ToInt16(model.Credit2);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade2) ? false : true;
                        dtModel.PaperCode = model.Code2;
                        dtModel.PaperTitle = model.PaperTitle2;
                        dtModel.PaperType = model.PaperType2;
                        dtModel.IsElective = model.IsElective2;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks2;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks2;
                        dtModel.Internalmarksobtained = model.InternalC2;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks2;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C2;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C2;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC2;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks2;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC2;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks2;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace2);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade2;
                        dtModel.Attempt = model.Attempt2;
                        dtModel.Remarks = model.Remarks;
                        //Retry total count of exams done for same subject...
                        int Retry = db.GetRetryCountForSubjectOfStudent(studentId, model.Code2, model.PaperType2);
                        dtModel.RetryCount = (Retry + 1);
                        db.UpdateSubjectStatusFromATKT(studentId, dtModel.PaperCode, model.PaperType1);
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code2, model.PaperType2);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code3))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit3) ? 0 : Convert.ToInt16(model.Credit3);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade3) ? false : true;
                        dtModel.PaperCode = model.Code3;
                        dtModel.PaperTitle = model.PaperTitle3;
                        dtModel.PaperType = model.PaperType3;
                        dtModel.IsElective = model.IsElective3;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks3;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks3;
                        dtModel.Internalmarksobtained = model.InternalC3;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks3;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C3;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C3;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC3;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks3;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC3;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks3;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace3);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade3;
                        dtModel.Attempt = model.Attempt3;
                        dtModel.Remarks = model.Remarks;
                        //Retry total count of exams done for same subject...
                        int Retry = db.GetRetryCountForSubjectOfStudent(studentId, model.Code3, model.PaperType3);
                        dtModel.RetryCount = (Retry + 1);
                        db.UpdateSubjectStatusFromATKT(studentId, dtModel.PaperCode, model.PaperType3);
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code3, model.PaperType3);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code4))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit4) ? 0 : Convert.ToInt16(model.Credit4);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade4) ? false : true;
                        dtModel.PaperCode = model.Code4;
                        dtModel.PaperTitle = model.PaperTitle4;
                        dtModel.PaperType = model.PaperType4;
                        dtModel.IsElective = model.IsElective4;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks4;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks4;
                        dtModel.Internalmarksobtained = model.InternalC4;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks4;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C4;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C4;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC4;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks4;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC4;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks4;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace4);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade4;
                        dtModel.Attempt = model.Attempt4;
                        dtModel.Remarks = model.Remarks;
                        //Retry total count of exams done for same subject...
                        int Retry = db.GetRetryCountForSubjectOfStudent(studentId, model.Code4, model.PaperType4);
                        dtModel.RetryCount = (Retry + 1);
                        db.UpdateSubjectStatusFromATKT(studentId, dtModel.PaperCode, model.PaperType4);
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code4, model.PaperType4);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code5))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit5) ? 0 : Convert.ToInt16(model.Credit5);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade5) ? false : true;
                        dtModel.PaperCode = model.Code5;
                        dtModel.PaperTitle = model.PaperTitle5;
                        dtModel.PaperType = model.PaperType5;
                        dtModel.IsElective = model.IsElective5;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks5;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks5;
                        dtModel.Internalmarksobtained = model.InternalC5;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks5;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C5;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C5;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC5;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks5;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC5;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks5;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace5);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade5;
                        dtModel.Attempt = model.Attempt5;
                        dtModel.Remarks = model.Remarks;
                        //Retry total count of exams done for same subject...
                        int Retry = db.GetRetryCountForSubjectOfStudent(studentId, model.Code5, model.PaperType5);
                        dtModel.RetryCount = (Retry + 1);
                        db.UpdateSubjectStatusFromATKT(studentId, dtModel.PaperCode, model.PaperType5);
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code5, model.PaperType5);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code6))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit6) ? 0 : Convert.ToInt16(model.Credit6);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade6) ? false : true;
                        dtModel.PaperCode = model.Code6;
                        dtModel.PaperTitle = model.PaperTitle6;
                        dtModel.PaperType = model.PaperType6;
                        dtModel.IsElective = model.IsElective6;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks6;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks6;
                        dtModel.Internalmarksobtained = model.InternalC6;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks6;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C6;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C6;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC6;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks6;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC6;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks6;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace6);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade6;
                        dtModel.Attempt = model.Attempt6;
                        dtModel.Remarks = model.Remarks;
                        //Retry total count of exams done for same subject...
                        int Retry = db.GetRetryCountForSubjectOfStudent(studentId, model.Code6, model.PaperType6);
                        dtModel.RetryCount = (Retry + 1);
                        db.UpdateSubjectStatusFromATKT(studentId, dtModel.PaperCode, model.PaperType6);
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code6, model.PaperType6);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code7))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit7) ? 0 : Convert.ToInt16(model.Credit7);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade7) ? false : true;
                        dtModel.PaperCode = model.Code7;
                        dtModel.PaperTitle = model.PaperTitle7;
                        dtModel.PaperType = model.PaperType7;
                        dtModel.IsElective = model.IsElective7;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks7;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks7;
                        dtModel.Internalmarksobtained = model.InternalC7;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks7;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C7;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C7;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC7;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks7;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC7;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks7;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace7);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade7;
                        dtModel.Attempt = model.Attempt7;
                        dtModel.Remarks = model.Remarks;
                        //Retry total count of exams done for same subject...
                        int Retry = db.GetRetryCountForSubjectOfStudent(studentId, model.Code7, model.PaperType7);
                        dtModel.RetryCount = (Retry + 1);
                        db.UpdateSubjectStatusFromATKT(studentId, dtModel.PaperCode, model.PaperType7);
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code7, model.PaperType7);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code8))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit8) ? 0 : Convert.ToInt16(model.Credit8);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade8) ? false : true;
                        dtModel.PaperCode = model.Code8;
                        dtModel.PaperTitle = model.PaperTitle8;
                        dtModel.PaperType = model.PaperType8;
                        dtModel.IsElective = model.IsElective8;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks8;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks8;
                        dtModel.Internalmarksobtained = model.InternalC8;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks8;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C8;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C8;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC8;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks8;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC8;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks8;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace8);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade8;
                        dtModel.Attempt = model.Attempt8;
                        dtModel.Remarks = model.Remarks;
                        //Retry total count of exams done for same subject...
                        int Retry = db.GetRetryCountForSubjectOfStudent(studentId, model.Code8, model.PaperType8);
                        dtModel.RetryCount = (Retry + 1);
                        db.UpdateSubjectStatusFromATKT(studentId, dtModel.PaperCode, model.PaperType8);
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code8, model.PaperType8);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code9))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit9) ? 0 : Convert.ToInt16(model.Credit9);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade9) ? false : true;
                        dtModel.PaperCode = model.Code9;
                        dtModel.PaperTitle = model.PaperTitle9;
                        dtModel.PaperType = model.PaperType9;
                        dtModel.IsElective = model.IsElective9;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks9;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks9;
                        dtModel.Internalmarksobtained = model.InternalC9;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks9;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C9;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C9;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC9;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks9;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC9;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks9;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace9);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade9;
                        dtModel.Attempt = model.Attempt9;
                        dtModel.Remarks = model.Remarks;
                        //Retry total count of exams done for same subject...
                        int Retry = db.GetRetryCountForSubjectOfStudent(studentId, model.Code9, model.PaperType9);
                        dtModel.RetryCount = (Retry + 1);
                        db.UpdateSubjectStatusFromATKT(studentId, dtModel.PaperCode, model.PaperType9);
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code9, model.PaperType9);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code10))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit10) ? 0 : Convert.ToInt16(model.Credit10);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade10) ? false : true;
                        dtModel.PaperCode = model.Code10;
                        dtModel.PaperTitle = model.PaperTitle10;
                        dtModel.PaperType = model.PaperType10;
                        dtModel.IsElective = model.IsElective10;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks10;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks10;
                        dtModel.Internalmarksobtained = model.InternalC10;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks10;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C10;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C10;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC10;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks10;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC10;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks10;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace10);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade10;
                        dtModel.Attempt = model.Attempt10;
                        dtModel.Remarks = model.Remarks;
                        //Retry total count of exams done for same subject...
                        int Retry = db.GetRetryCountForSubjectOfStudent(studentId, model.Code10, model.PaperType10);
                        dtModel.RetryCount = (Retry + 1);
                        db.UpdateSubjectStatusFromATKT(studentId, dtModel.PaperCode, model.PaperType10);
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code10, model.PaperType10);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }

                }
                result = Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Saved Successfully" });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public HttpResponseMessage SaveATKTMarksDetailsInDb([FromBody]List<MarksEntryViewModel> models, [FromUri]int admissionYear,
             [FromUri]string stream, [FromUri]string examName, [FromUri]string examType, [FromUri]int semester, [FromUri]int year, [FromUri]string month)
        {
            HttpResponseMessage result = null;
            try
            {
                int count = 0;
                if (models == null || models.Count == 0)
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });

                foreach (MarksEntryViewModel model in models)
                {
                    if (model == null || model.CollegeRegistrationNumber == null)
                    {
                        count++;
                        continue;
                    }

                    int studentId = db.CheckIfStudentDetailsExist(model.CollegeRegistrationNumber);
                    if (studentId <= 0)
                    {
                        StudentDetailsDataModel dtModel = new StudentDetailsDataModel();
                        dtModel.AdmissionYear = admissionYear;
                        dtModel.CollegeRegistrationNumber = model.CollegeRegistrationNumber;
                        dtModel.Prn = string.IsNullOrEmpty(model.PRN) ? "" : model.PRN.Replace("'", "");
                        dtModel.LastName = model.LastName;
                        dtModel.FirstName = model.FirstName;
                        dtModel.FatherName = model.FatherName;
                        dtModel.MotherName = model.MotherName;
                        dtModel.Stream = stream;
                        dtModel.Course = model.Course;
                        dtModel.Specialisation = model.Specialisation;
                        studentId = db.InsertStudentDetails(dtModel);
                    }

                    int examId = db.CheckIfExamDetailsExist(examName, examType, semester.ToString(), year, month);
                    if (examId <= 0)
                    {
                        StudentExamDetail dtModel = new StudentExamDetail();
                        dtModel.StudentId = studentId;
                        dtModel.ExamName = examName;
                        dtModel.ExamType = examType;
                        dtModel.Semester = semester;
                        examId = db.InsertStudentExamDetails(dtModel);
                    }

                    if (!string.IsNullOrEmpty(model.Code1))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit1) ? 0 : Convert.ToInt16(model.Credit1);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade1) ? false : true;
                        dtModel.PaperCode = model.Code1;
                        dtModel.PaperTitle = model.PaperTitle1;
                        dtModel.PaperType = model.PaperType1;
                        dtModel.IsElective = model.IsElective1;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks1;
                        dtModel.Internalmarksobtained = model.InternalC1;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks1;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C1;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C1;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC1;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks1;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC1;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks1;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks1;
                        dtModel.GraceMarks = string.IsNullOrEmpty(model.Grace1) ? 0 : Convert.ToUInt16(model.Grace1);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade1;
                        dtModel.Attempt = model.Attempt1;
                        dtModel.Remarks = model.Remarks;
                        dtModel.status = false;

                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code1, model.PaperType1);
                        //int marksheetId = db.CheckIfATKTMarksDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code1, model.PaperType1);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code2))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit2) ? 0 : Convert.ToInt16(model.Credit2);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade2) ? false : true;
                        dtModel.PaperCode = model.Code2;
                        dtModel.PaperTitle = model.PaperTitle2;
                        dtModel.PaperType = model.PaperType2;
                        dtModel.IsElective = model.IsElective2;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks2;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks2;
                        dtModel.Internalmarksobtained = model.InternalC2;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks2;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C2;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C2;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC2;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks2;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC2;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks2;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace2);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade1;
                        dtModel.Attempt = model.Attempt2;
                        dtModel.Remarks = model.Remarks;
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code2, model.PaperType2);
                        /*if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertAtktMarksDetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateATKTMarksdetails(dtModel);
                        }*/
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code3))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit3) ? 0 : Convert.ToInt16(model.Credit3);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade3) ? false : true;
                        dtModel.PaperCode = model.Code3;
                        dtModel.PaperTitle = model.PaperTitle3;
                        dtModel.PaperType = model.PaperType3;
                        dtModel.IsElective = model.IsElective3;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks3;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks3;
                        dtModel.Internalmarksobtained = model.InternalC3;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks3;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C3;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C3;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC3;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks3;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC3;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks3;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace3);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade3;
                        dtModel.Attempt = model.Attempt3;
                        dtModel.Remarks = model.Remarks;
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code3, model.PaperType3);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code4))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit4) ? 0 : Convert.ToInt16(model.Credit4);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade4) ? false : true;
                        dtModel.PaperCode = model.Code4;
                        dtModel.PaperTitle = model.PaperTitle4;
                        dtModel.PaperType = model.PaperType4;
                        dtModel.IsElective = model.IsElective4;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks4;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks4;
                        dtModel.Internalmarksobtained = model.InternalC4;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks4;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C4;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C4;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC4;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks4;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC4;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks4;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace4);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade4;
                        dtModel.Attempt = model.Attempt4;
                        dtModel.Remarks = model.Remarks;
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code4, model.PaperType4);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code5))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit5) ? 0 : Convert.ToInt16(model.Credit5);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade5) ? false : true;
                        dtModel.PaperCode = model.Code5;
                        dtModel.PaperTitle = model.PaperTitle5;
                        dtModel.PaperType = model.PaperType5;
                        dtModel.IsElective = model.IsElective5;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks5;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks5;
                        dtModel.Internalmarksobtained = model.InternalC5;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks5;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C5;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C5;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC5;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks5;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC5;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks5;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace5);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade5;
                        dtModel.Attempt = model.Attempt5;
                        dtModel.Remarks = model.Remarks;
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code5, model.PaperType5);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code6))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit6) ? 0 : Convert.ToInt16(model.Credit6);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade6) ? false : true;
                        dtModel.PaperCode = model.Code6;
                        dtModel.PaperTitle = model.PaperTitle6;
                        dtModel.PaperType = model.PaperType6;
                        dtModel.IsElective = model.IsElective6;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks6;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks6;
                        dtModel.Internalmarksobtained = model.InternalC6;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks6;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C6;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C6;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC6;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks6;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC6;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks6;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace6);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade5;
                        dtModel.Attempt = model.Attempt6;
                        dtModel.Remarks = model.Remarks;
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code6, model.PaperType6);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code7))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit7) ? 0 : Convert.ToInt16(model.Credit7);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade7) ? false : true;
                        dtModel.PaperCode = model.Code7;
                        dtModel.PaperTitle = model.PaperTitle7;
                        dtModel.PaperType = model.PaperType7;
                        dtModel.IsElective = model.IsElective7;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks7;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks7;
                        dtModel.Internalmarksobtained = model.InternalC7;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks7;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C7;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C7;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC7;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks7;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC7;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks7;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace7);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade7;
                        dtModel.Attempt = model.Attempt7;
                        dtModel.Remarks = model.Remarks;
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code7, model.PaperType7);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code8))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit8) ? 0 : Convert.ToInt16(model.Credit8);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade8) ? false : true;
                        dtModel.PaperCode = model.Code8;
                        dtModel.PaperTitle = model.PaperTitle8;
                        dtModel.PaperType = model.PaperType8;
                        dtModel.IsElective = model.IsElective8;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks8;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks8;
                        dtModel.Internalmarksobtained = model.InternalC8;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks8;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C8;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C8;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC8;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks8;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC8;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks8;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace8);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade8;
                        dtModel.Attempt = model.Attempt8;
                        dtModel.Remarks = model.Remarks;
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code8, model.PaperType8);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code9))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit9) ? 0 : Convert.ToInt16(model.Credit9);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade9) ? false : true;
                        dtModel.PaperCode = model.Code9;
                        dtModel.PaperTitle = model.PaperTitle9;
                        dtModel.PaperType = model.PaperType9;
                        dtModel.IsElective = model.IsElective9;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks9;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks9;
                        dtModel.Internalmarksobtained = model.InternalC9;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks9;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C9;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C9;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC9;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks9;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC9;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks9;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace9);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade9;
                        dtModel.Attempt = model.Attempt9;
                        dtModel.Remarks = model.Remarks;
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code9, model.PaperType9);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }
                    if (!string.IsNullOrEmpty(model.Code10))
                    {
                        MarksdetailDataModel dtModel = new MarksdetailDataModel();
                        dtModel.StudentId = studentId;
                        dtModel.ExamId = examId;
                        dtModel.SeatNumber = model.SeatNumber;
                        dtModel.Credit = string.IsNullOrEmpty(model.Credit10) ? 0 : Convert.ToInt16(model.Credit10);
                        dtModel.IsGrade = string.IsNullOrEmpty(model.Grade10) ? false : true;
                        dtModel.PaperCode = model.Code10;
                        dtModel.PaperTitle = model.PaperTitle10;
                        dtModel.PaperType = model.PaperType10;
                        dtModel.IsElective = model.IsElective10;
                        dtModel.InternalPassingMarks = model.InternalPassingMarks10;
                        dtModel.InternalTotalMarks = model.InternalTotalMarks10;
                        dtModel.Internalmarksobtained = model.InternalC10;
                        dtModel.ExternalPassingMarks = model.ExternalPassingMarks10;
                        dtModel.ExternalSection1Marks = model.ExternalSection1C10;
                        dtModel.ExternalSection2Marks = model.ExternalSection2C10;
                        dtModel.ExternalTotalMarks = model.ExternalTotalC10;
                        dtModel.ExternalMaxMarks = model.ExternalMaxMarks10;
                        dtModel.PracticalMarksObtained = model.PracticalMarksC10;
                        dtModel.PracticalMaxMarks = model.PracticalMaxMarks10;
                        dtModel.GraceMarks = Convert.ToUInt16(model.Grace10);
                        dtModel.PaperResult = model.Result;
                        dtModel.Gp = model.Grade10;
                        dtModel.Attempt = model.Attempt10;
                        dtModel.Remarks = model.Remarks;
                        int marksheetId = db.CheckIfMarksheetDetailsExist(studentId.ToString(), examId.ToString(), model.SeatNumber, model.Code10, model.PaperType10);
                        if (marksheetId <= 0)
                        {
                            marksheetId = db.InsertMarksdetails(dtModel);
                        }
                        else
                        {
                            dtModel.Id = marksheetId;
                            db.UpdateMarksdetails(dtModel);
                        }
                    }

                }
                result = Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Saved Successfully" });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetCommonMarksheetResponse(string examType, string specialisationCode, string sem)
        {
            try
            {
                if (string.IsNullOrEmpty(examType) || string.IsNullOrEmpty(sem) || string.IsNullOrEmpty(specialisationCode))
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });

                if (sem != "4" && sem != "6" && sem.ToLower() != "all")
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "semester not valid" });
                }

                if (specialisationCode == "DCN" || specialisationCode == "RN" || specialisationCode == "PN")
                    sem = "4";

                List<CommonMarksheetResponse> response = db.GetCommonMarksheetData(sem, examType, specialisationCode);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetStudentsTranscript(string CollegeRegistrationNumber, string semester)
        {
            if (string.IsNullOrWhiteSpace(semester))
            {
                semester = "6";
            }
            if (string.IsNullOrWhiteSpace(CollegeRegistrationNumber))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "College Registration Number Cannot be blank");
            }
            CommonMarksheetResponse cm = db.GetStudentDetailsByCollegeRegistrationNumber(CollegeRegistrationNumber);
            if (cm == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "College Registration Number not matching with any student");
            }
            else
            {
                var ExamTable = db.GetStudentExamHestory(cm.Id);
                var ExamDetails = db.GetStudentExamDetailsById(Convert.ToInt32(cm.Id));
                return Request.CreateResponse(HttpStatusCode.OK, new { ExamTable, cm, ExamDetails });
            }
        }

        [HttpGet]
        public HttpResponseMessage GetExamPapers(string examYear, string examMonth, string semester, string userName)
        {
            List<ExamPaperDataModel> papers = db.GetExamPapers(examYear, examMonth, semester, userName);
            return Request.CreateResponse(HttpStatusCode.OK, papers);
        }

        [HttpGet]
        public HttpResponseMessage DeleteExamPaper(int paperId, string userName)
        {
            db.UpdateExamPaper(paperId, userName);
            return Request.CreateResponse(HttpStatusCode.OK, "Paper deleted successfully");
        }

        [HttpGet]
        public HttpResponseMessage GenerateRandomExamPaper(int paperId)
        {
            db.GenerateRandomPaper(paperId);
            return Request.CreateResponse(HttpStatusCode.OK, "Random paper selected successfully, please refresh the page");
        }

        [HttpGet]
        public HttpResponseMessage DownloadPaper(string path)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/" + path);
            byte[] bytes = File.ReadAllBytes(filePath);
            string fileName = path.Substring(path.LastIndexOf("/") + 1);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

            response.Content = new ByteArrayContent(bytes);
            response.Content.Headers.ContentLength = bytes.LongLength;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = fileName;

            //Set the File Content Type.
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(fileName));
            return response;
        }
    }
}
