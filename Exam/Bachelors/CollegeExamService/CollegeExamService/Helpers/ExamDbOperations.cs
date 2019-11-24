using CollegeExamService.Controllers;
using CollegeExamService.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using static CollegeExamService.Controllers.AttendanceController;

namespace CollegeExamService.Helpers
{
    public class ExamDbOperations
    {
        OdbcConnection obcon;
        OdbcDataAdapter odbAdp;
        OdbcCommand odbCommand;
        string getIdQuery = "Select @@Identity";
        int ID;
        string connection = ConfigurationManager.AppSettings.Get("DbConn").ToString();

        public OdbcConnection CreateConnection()
        {
            return new OdbcConnection(connection);
        }

        public int ExcuteQuery(string query)
        {
            try
            {
                using (obcon = CreateConnection())
                using (odbCommand = new OdbcCommand(query, obcon))
                {
                    if (obcon.State == ConnectionState.Closed)
                    {
                        obcon.Open();
                    }
                    odbCommand.ExecuteNonQuery();
                    odbCommand.CommandText = getIdQuery;
                    ID = (int)Convert.ToInt32(odbCommand.ExecuteScalar().ToString());
                    return ID;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (obcon.State == ConnectionState.Open)
                {
                    obcon.Close();
                }
            }
        }

        public int ExcuteUpdateQuery(string query)
        {
            try
            {
                using (obcon = CreateConnection())
                using (odbCommand = new OdbcCommand(query, obcon))
                {
                    if (obcon.State == ConnectionState.Closed)
                    {
                        obcon.Open();
                    }
                    odbCommand.ExecuteNonQuery();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (obcon.State == ConnectionState.Open)
                {
                    obcon.Close();
                }
            }
        }

        public DataSet GetQuery(string query)
        {
            try
            {
                using (obcon = CreateConnection())
                using (odbCommand = new OdbcCommand(query, obcon))
                {
                    if (obcon.State == ConnectionState.Closed)
                    {
                        obcon.Open();
                    }
                    DataSet dsItems = new DataSet();
                    odbAdp = new OdbcDataAdapter(query, obcon); // need to add examtype
                    odbAdp.Fill(dsItems);
                    return dsItems;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (obcon.State == ConnectionState.Open)
                {
                    obcon.Close();
                }
            }
        }

        public int InsertStudentDetails(StudentDetailsDataModel sd)
        {
            string iq = "INSERT INTO studentdetails(admissionyear,collegeregistrationnumber,prn,lastname," +
"firstname,fathername,mothername,stream,course,specialisation) VALUES ('" + sd.AdmissionYear + "','" + sd.CollegeRegistrationNumber + "'," +
"'" + sd.Prn + "','" + sd.LastName + "','" + sd.FirstName + "','" + sd.FatherName + "','" + sd.MotherName + "','" + sd.Stream + "'," +
"'" + sd.Course + "','" + sd.Specialisation + "');";

            return ExcuteQuery(iq);
        }

        public int UpdateStudentDetails(StudentDetailsDataModel sd)
        {
            string iq = "Update studentdetails set admissionyear='" + sd.AdmissionYear + "',collegeregistrationnumber='" + sd.CollegeRegistrationNumber + "'," +
                " prn='" + sd.Prn + "',lastname='" + sd.LastName + "',firstname='" + sd.FirstName + "',fathername='" + sd.FatherName + "'," +
                " mothername='" + sd.MotherName + "',stream='" + sd.Stream + "',course='" + sd.Course + "',specialisation='" + sd.Specialisation + "'" +
                " where id=" + sd.Id + "";

            return ExcuteQuery(iq);
        }

        public int SaveExamPaper(ExamPaperDataModel paper)
        {
            string query = "INSERT INTO papermaster (papercode,papertitle,examyear,exammonth,semester,course,specialisation,examtype,uploadedby,uploadeddate,paper1,paper2,paper3) values ('" +
               paper.PaperCode + "','" + paper.PaperTitle + "','" + paper.ExamYear + "','" + paper.ExamMonth + "','" + paper.Semester + "','" + paper.Course + "','" + paper.Specialisation +
               "','" + paper.ExamType + "','" + paper.UploadedBy + "','" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "','" + paper.Paper1 + "','" + paper.Paper2 + "','" + paper.Paper3 + "')";
            return ExcuteQuery(query);
        }

        public void UpdateExamPaper(int paperId, string userName)
        {
            string query = "update papermaster set isdeleted=true, datedeleted='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "' WHERE id=" + paperId;
            ExcuteQuery(query);
        }

        public void GenerateRandomPaper(int paperId)
        {
            Random rand = new Random();
            int seconds = DateTime.Now.Second % 3;
            int random = seconds == 0 ? 3 : seconds;
            string columnName = "paper" + random;
            string query = "update papermaster set randomselectedpaper=" + columnName + ", randomgenerationdate='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "' WHERE id=" + paperId;
            ExcuteQuery(query);
        }

        public List<ExamPaperDataModel> GetExamPapers(string examYear, string examMonth, string semester, string userName)
        {
            List<ExamPaperDataModel> papers = new List<ExamPaperDataModel>();
            string query = "SELECT id,specialisation,papercode,papertitle,paper1,paper2,paper3,randomselectedpaper,uploadedby,uploadeddate FROM papermaster WHERE examyear=" + examYear + " and exammonth='" + examMonth + "' and semester='" + semester + "' and isdeleted=false order by papertitle asc";

            if (!string.IsNullOrEmpty(userName))
            {
                query = "SELECT id,specialisation,papercode,papertitle,paper1,paper2,paper3,randomselectedpaper,uploadedby,uploadeddate FROM papermaster WHERE examyear=" + examYear + " and exammonth='" + examMonth + "' and semester='" + semester + "' and uploadedby = '" + userName + "' and isdeleted=false order by papertitle asc";
            }

            obcon = new OdbcConnection(connection);
            DataSet dsItems = new DataSet();
            odbAdp = new OdbcDataAdapter(query, obcon); // need to add examtype
            odbAdp.Fill(dsItems);
            if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsItems.Tables[0].Rows)
                {
                    ExamPaperDataModel paper = new ExamPaperDataModel();
                    paper.Id = int.Parse(row["id"].ToString());
                    paper.PaperCode = row["papercode"].ToString();
                    paper.PaperTitle = row["papertitle"].ToString();
                    paper.Specialisation = row["specialisation"].ToString();
                    paper.Paper1 = row["paper1"].ToString();
                    paper.Paper2 = row["paper2"].ToString();
                    paper.Paper3 = row["paper3"].ToString();
                    paper.UploadedBy = row["uploadedby"].ToString();
                    paper.UploadedDate = DateTime.Parse(row["uploadeddate"].ToString());
                    paper.RandomSelectedPaper = row["randomselectedpaper"].ToString();
                    papers.Add(paper);
                }

                return papers;
            }

            return null;
        }

        public int InsertStudentExamDetails(StudentExamDetail sd)
        {
            string iq = "INSERT INTO studentexamdetails(studentid,examname,examtype,semester,yearOfExam,monthOfExam) VALUES ('" + sd.StudentId + "','" + sd.ExamName + "'," +
            "'" + sd.ExamType + "','" + sd.Semester + "'," + sd.yearOfExam + ",'" + sd.monthOfExam + "');";

            return ExcuteQuery(iq);
        }
        public int InsertStudentTranscript(string firstN, string Lname, string PNR, string AdYear)
        {
            string iq = "INSERT INTO transcript(FirstName, LastName, PNR, AdmissionYear, ReqRecvdOn, RequestStatus) VALUES";
            iq += "('" + firstN + "','" + Lname + "','" + PNR + "','" + AdYear + "', NOW(), 0)";
            return ExcuteQuery(iq);
        }

        public int CompleteTranscriptData(int TrnascriptId, int status)
        {
            string iq = string.Format("update transcript set RequestStatus={0} where RequestId={1}", status, TrnascriptId);
            return ExcuteQuery(iq);
        }

        public string ExecuteScalerValue(string query)
        {
            obcon = new OdbcConnection(connection);
            odbCommand = new OdbcCommand();
            odbCommand.Connection = obcon;
            if (obcon.State == ConnectionState.Closed)
            {
                obcon.Open();
            }
            odbCommand.CommandText = query;
            string Response = Convert.ToString(odbCommand.ExecuteScalar());
            if (obcon.State == ConnectionState.Open)
            {
                obcon.Close();
            }
            return Response;
        }

        public string GetInsertQuery(MarksdetailDataModel sd)
        {
            return "(" + sd.StudentId + ",'" + sd.Semester + "','" + sd.SeatNumber + "','" + sd.Credit + "'," + sd.IsGrade + ",'" + sd.PaperCode + "'," +
"'" + sd.PaperTitle + "','" + sd.PaperType + "'," + sd.IsElective + ",'" + sd.InternalPassingMarks + "','" + sd.Internalmarksobtained + "'," +
"'" + sd.ExternalPassingMarks + "','" + sd.InternalTotalMarks + "','" + sd.ExternalSection1Marks + "','" + sd.ExternalSection2Marks + "','" + sd.ExternalTotalMarks + "'," +
"'" + sd.PracticalMarksObtained + "','" + sd.PracticalMaxMarks + "','" + sd.GraceMarks + "','" + sd.PaperResult + "','" + sd.Gp + "','" + sd.Grade + "'," +
"'" + sd.Attempt + "','" + sd.Remarks + "'," + sd.PrintedCommon4 + "," + sd.PrintedCommon6 + ",'" + sd.ExternalMaxMarks + "'," + sd.RetryCount + "," + sd.Year + "),";
        }

        public int InsertMarksdetails(MarksdetailDataModel sd)
        {
            string iq = "INSERT INTO marksdetails(studentid,semester,seatnumber,credit,isgrade,papercode,papertitle,papertype,iselective," +
"internalpassingmarks,internalmarksobtained,externalpassingmarks,internaltotalmarks,externalsection1marks,externalsection2marks," +
"externaltotalmarks,practicalmarksobtained,practicalmaxmarks,gracemarks,paperresult,gp,grade,attempt,remarks,printedcommon4,printedcommon6,externalmaxmarks,RetryCount,year)" +
"VALUES ('" + sd.StudentId + "','" + sd.Semester + "','" + sd.SeatNumber + "','" + sd.Credit + "'," + sd.IsGrade + ",'" + sd.PaperCode + "'," +
"'" + sd.PaperTitle + "','" + sd.PaperType + "'," + sd.IsElective + ",'" + sd.InternalPassingMarks + "','" + sd.Internalmarksobtained + "'," +
"'" + sd.ExternalPassingMarks + "','" + sd.InternalTotalMarks + "','" + sd.ExternalSection1Marks + "','" + sd.ExternalSection2Marks + "','" + sd.ExternalTotalMarks + "'," +
"'" + sd.PracticalMarksObtained + "','" + sd.PracticalMaxMarks + "','" + sd.GraceMarks + "','" + sd.PaperResult + "','" + sd.Gp + "','" + sd.Grade + "'," +
"'" + sd.Attempt + "','" + sd.Remarks + "'," + sd.PrintedCommon4 + "," + sd.PrintedCommon6 + ",'" + sd.ExternalMaxMarks + "'," + sd.RetryCount + "," + sd.Year + ");";

            return ExcuteQuery(iq);
        }

        public string GetUpdateQuery(MarksdetailDataModel sd)
        {
            return "UPDATE marksdetails SET credit = '" + sd.Credit + "',isgrade = " + sd.IsGrade + ",papercode = '" + sd.PaperCode + "'," +
"papertitle ='" + sd.PaperTitle + "',papertype ='" + sd.PaperType + "',iselective = " + sd.IsElective + ",internalpassingmarks = '" + sd.InternalPassingMarks + "'," +
"internalmarksobtained = '" + sd.Internalmarksobtained + "',externalpassingmarks ='" + sd.ExternalPassingMarks + "',internaltotalmarks = '" + sd.InternalTotalMarks + "'," +
"externalsection1marks = '" + sd.ExternalSection1Marks + "',externalsection2marks ='" + sd.ExternalSection2Marks + "',externaltotalmarks = '" + sd.ExternalTotalMarks + "'," +
"practicalmarksobtained = '" + sd.PracticalMarksObtained + "',practicalmaxmarks = '" + sd.PracticalMaxMarks + "',gracemarks = '" + sd.GraceMarks + "'," +
"paperresult = '" + sd.PaperResult + "',gp = '" + sd.Gp + "',grade ='" + sd.Grade + "',attempt = '" + sd.Attempt + "',remarks = '" + sd.Remarks + "'," +
"printedcommon4 = " + sd.PrintedCommon4 + ",printedcommon6 =" + sd.PrintedCommon6 + ",externalmaxmarks='" + sd.ExternalMaxMarks + "',RetryCount=(RetryCount+1)  WHERE id = " + sd.Id + ";";
        }

        public int UpdateMarksdetails(MarksdetailDataModel sd)
        {
            string iq = "UPDATE marksdetails SET credit = '" + sd.Credit + "',isgrade = " + sd.IsGrade + ",papercode = '" + sd.PaperCode + "'," +
"papertitle ='" + sd.PaperTitle + "',papertype ='" + sd.PaperType + "',iselective = " + sd.IsElective + ",internalpassingmarks = '" + sd.InternalPassingMarks + "'," +
"internalmarksobtained = '" + sd.Internalmarksobtained + "',externalpassingmarks ='" + sd.ExternalPassingMarks + "',internaltotalmarks = '" + sd.InternalTotalMarks + "'," +
"externalsection1marks = '" + sd.ExternalSection1Marks + "',externalsection2marks ='" + sd.ExternalSection2Marks + "',externaltotalmarks = '" + sd.ExternalTotalMarks + "'," +
"practicalmarksobtained = '" + sd.PracticalMarksObtained + "',practicalmaxmarks = '" + sd.PracticalMaxMarks + "',gracemarks = '" + sd.GraceMarks + "'," +
"paperresult = '" + sd.PaperResult + "',gp = '" + sd.Gp + "',grade ='" + sd.Grade + "',attempt = '" + sd.Attempt + "',remarks = '" + sd.Remarks + "'," +
"printedcommon4 = " + sd.PrintedCommon4 + ",printedcommon6 =" + sd.PrintedCommon6 + ",externalmaxmarks='" + sd.ExternalMaxMarks + "',RetryCount=(RetryCount+1)  WHERE id = " + sd.Id + ";";

            return ExcuteQuery(iq);
        }

        public int InsertAtktMarksDetails(AtktMarksDetail sd)
        {
            string iq = "INSERT INTO atktmarksdetails(studentid,examid,seatnumber,credit,isgrade,papercode,papertitle,papertype,iselective," +
"internalpassingmarks,internalmarksobtained,externalpassingmarks,internaltotalmarks,externalsection1marks,externalsection2marks," +
"externaltotalmarks,practicalmarksobtained,practicalmaxmarks,gracemarks,paperresult,gp,grade,attempt,remarks,externalmaxmarks, status)" +
"VALUES ('" + sd.StudentId + "','" + sd.ExamId + "','" + sd.SeatNumber + "','" + sd.Credit + "'," + sd.IsGrade + ",'" + sd.PaperCode + "'," +
"'" + sd.PaperTitle + "','" + sd.PaperType + "'," + sd.IsElective + ",'" + sd.InternalPassingMarks + "','" + sd.Internalmarksobtained + "'," +
"'" + sd.ExternalPassingMarks + "','" + sd.InternalTotalMarks + "','" + sd.ExternalSection1Marks + "','" + sd.ExternalSection2Marks + "','" + sd.ExternalTotalMarks + "'," +
"'" + sd.PracticalMarksObtained + "','" + sd.PracticalMaxMarks + "','" + sd.GraceMarks + "','" + sd.PaperResult + "','" + sd.Gp + "','" + sd.Grade + "'," +
"'" + sd.Attempt + "','" + sd.Remarks + "','" + sd.ExternalMaxMarks + "'," + sd.status + ");";

            return ExcuteQuery(iq);
        }

        public int UpdateATKTMarksdetails(AtktMarksDetail sd)
        {
            string iq = "UPDATE atktmarksdetails SET credit = '" + sd.Credit + "',isgrade = " + sd.IsGrade + ",papercode = '" + sd.PaperCode + "'," +
"papertitle ='" + sd.PaperTitle + "',papertype ='" + sd.PaperType + "',iselective = " + sd.IsElective + ",internalpassingmarks = '" + sd.InternalPassingMarks + "'," +
"internalmarksobtained = '" + sd.Internalmarksobtained + "',externalpassingmarks ='" + sd.ExternalPassingMarks + "',internaltotalmarks = '" + sd.InternalTotalMarks + "'," +
"externalsection1marks = '" + sd.ExternalSection1Marks + "',externalsection2marks ='" + sd.ExternalSection2Marks + "',externaltotalmarks = '" + sd.ExternalTotalMarks + "'," +
"practicalmarksobtained = '" + sd.PracticalMarksObtained + "',practicalmaxmarks = '" + sd.PracticalMaxMarks + "',gracemarks = '" + sd.GraceMarks + "'," +
"paperresult = '" + sd.PaperResult + "',gp = '" + sd.Gp + "',grade ='" + sd.Grade + "',attempt = '" + sd.Attempt + "',remarks = '" + sd.Remarks + "'," +
"externalmaxmarks='" + sd.ExternalMaxMarks + "', status=" + sd.status + " WHERE id = " + sd.Id + ";";

            return ExcuteQuery(iq);
        }

        public List<StudentDetails> GetStudents()
        {
            try
            {
                List<StudentDetails> students = new List<StudentDetails>();
                obcon = new OdbcConnection(connection);
                odbCommand = new OdbcCommand();
                odbCommand.Connection = obcon;
                obcon.Open();
                odbCommand.CommandText = "select id,collegeregistrationnumber from studentdetails";
                OdbcDataReader reader = odbCommand.ExecuteReader();

                while (reader.Read())
                {
                    StudentDetails student = new StudentDetails();
                    student.Id = reader["id"].ToString();
                    student.Crn = reader["collegeregistrationnumber"].ToString();
                    students.Add(student);
                }

                obcon.Close();
                return students;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CheckIfStudentDetailsExist(string collegeRegistrationNumber)
        {
            try
            {
                string query = string.Format("select id from studentdetails where collegeregistrationnumber='{0}'", collegeRegistrationNumber);
                obcon = new OdbcConnection(connection);
                odbCommand = new OdbcCommand();
                odbCommand.Connection = obcon;
                obcon.Open();
                odbCommand.CommandText = query;
                string strID = Convert.ToString(odbCommand.ExecuteScalar());
                if (!string.IsNullOrEmpty(strID))
                {
                    ID = (int)Convert.ToInt32(strID);
                }
                else
                {
                    ID = 0;
                }
                obcon.Close();
                return ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CheckIfExamDetailsExist(string examName, string examType, string sem, int year, string month)
        {
            try
            {
                string query = string.Format("select id from studentexamdetails where examname='{0}' and examtype='{1}'" +
                "and semester={2} AND yearOfExam={3} AND monthOfExam='{4}'", examName, examType, sem, year, month);
                obcon = new OdbcConnection(connection);
                odbCommand = new OdbcCommand();
                odbCommand.Connection = obcon;
                obcon.Open();
                odbCommand.CommandText = query;
                string strID = Convert.ToString(odbCommand.ExecuteScalar());
                if (!string.IsNullOrEmpty(strID))
                {
                    ID = (int)Convert.ToInt32(strID);
                }
                else
                {
                    ID = 0;
                }
                obcon.Close();
                return ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetRetryCountForSubjectOfStudent(int studentId, string paperCode, string paperType)
        {
            string query = string.Format("SELECT COUNT(*) FROM atktmarksdetails WHERE studentid={0} AND papercode='{1}' and paperType='{2}'", studentId, paperCode, paperType);
            obcon = new OdbcConnection(connection);
            odbCommand = new OdbcCommand();
            odbCommand.Connection = obcon;
            obcon.Open();
            odbCommand.CommandText = query;
            string strID = Convert.ToString(odbCommand.ExecuteScalar());
            if (!string.IsNullOrEmpty(strID))
            {
                ID = (int)Convert.ToInt32(strID);
            }
            else
            {
                ID = 0;
            }
            obcon.Close();
            return ID;
        }
        public int UpdateSubjectStatusFromATKT(int studentId, string paperCode, string paperType)
        {
            string query = string.Format("UPDATE atktmarksdetails SET status=1 WHERE studentid={0} AND papercode='{1} AND paperType={2}'", studentId, paperCode, paperType);
            return ExcuteQuery(query);
        }

        public List<PaperDetail> GetPapers(string studentId, string semester)
        {
            try
            {
                List<PaperDetail> papers = new List<PaperDetail>();
                string query = string.Format(" select id,papercode,paperType from marksdetails where studentid='{0}' and semester='{1}'", studentId, semester);
                obcon = new OdbcConnection(connection);
                odbCommand = new OdbcCommand();
                odbCommand.Connection = obcon;
                obcon.Open();
                odbCommand.CommandText = query;
                OdbcDataReader reader = odbCommand.ExecuteReader();
                while (reader.Read())
                {
                    PaperDetail paper = new PaperDetail();
                    paper.Id = reader["id"].ToString();
                    paper.paperCode = reader["papercode"].ToString();
                    paper.paperType = reader["paperType"].ToString();
                    papers.Add(paper);
                }

                reader.Close();
                obcon.Close();

                return papers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CheckIfMarksheetDetailsExist(string studentId, string examId, string seatNumber, string paperCode, string paperType)
        {
            try
            {
                string query = string.Format(" select id from marksdetails where studentid='{0}' and examid='{1}' and seatnumber='{2}'" +
                "and papercode='{3}' and paperType='{4}'", studentId, examId, seatNumber, paperCode, paperType);
                obcon = new OdbcConnection(connection);
                odbCommand = new OdbcCommand();
                odbCommand.Connection = obcon;
                obcon.Open();
                odbCommand.CommandText = query;
                string strID = Convert.ToString(odbCommand.ExecuteScalar());
                if (!string.IsNullOrEmpty(strID))
                {
                    ID = (int)Convert.ToInt32(strID);
                }
                else
                {
                    ID = 0;
                }
                obcon.Close();
                return ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CheckIfATKTMarksDetailsExist(string studentId, string examId, string seatNumber, string paperCode, string paperType)
        {
            try
            {
                string query = string.Format(" select id from atktmarksdetails where studentid='{0}' and examid='{1}' and seatnumber='{2}'" +
                "and papercode='{3}' and paperType='{4}'", studentId, examId, seatNumber, paperCode, paperType);
                obcon = new OdbcConnection(connection);
                odbCommand = new OdbcCommand();
                odbCommand.Connection = obcon;
                obcon.Open();
                odbCommand.CommandText = query;
                string strID = Convert.ToString(odbCommand.ExecuteScalar());
                if (!string.IsNullOrEmpty(strID))
                {
                    ID = (int)Convert.ToInt32(strID);
                }
                else
                {
                    ID = 0;
                }
                obcon.Close();
                return ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetSemeter(string studentId, string examId)
        {
            try
            {
                string query = string.Format(" select semester from studentexamdetails where studentid='{0}' and id='{1}'", studentId, examId);
                obcon = new OdbcConnection(connection);
                odbCommand = new OdbcCommand();
                odbCommand.Connection = obcon;
                obcon.Open();
                odbCommand.CommandText = query;
                string strID = Convert.ToString(odbCommand.ExecuteScalar());
                if (!string.IsNullOrEmpty(strID))
                {
                    ID = (int)Convert.ToInt32(strID);
                }
                else
                {
                    ID = 0;
                }
                obcon.Close();
                return ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CommonMarksheetResponse GetStudentDetailsByCollegeRegistrationNumber(string CollegeRegistrationNumber)
        {
            try
            {
                CommonMarksheetResponse cmr = new CommonMarksheetResponse();
                string query = string.Format("select * from studentdetails where collegeregistrationnumber={0}", CollegeRegistrationNumber);
                obcon = new OdbcConnection(connection);
                DataSet dsItems = new DataSet();
                odbAdp = new OdbcDataAdapter(query, obcon); // need to add examtype
                odbAdp.Fill(dsItems);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        cmr.Id = row["id"].ToString();
                        cmr.PRN = row["prn"].ToString();
                        cmr.CollegeRegistrationNumber = row["collegeregistrationnumber"].ToString();
                        cmr.AdmissionYear = row["admissionyear"].ToString();
                        cmr.LastName = row["lastname"].ToString();
                        cmr.FirstName = row["firstname"].ToString();
                        cmr.FatherName = row["fathername"].ToString();
                        cmr.MotherName = row["mothername"].ToString();
                        cmr.StudentFullName = cmr.LastName + " " + cmr.FirstName + " " + cmr.FatherName + " " + cmr.MotherName;
                        cmr.Stream = row["stream"].ToString();
                        cmr.Course = row["course"].ToString();
                        cmr.Specialisation = row["specialisation"].ToString();

                    }

                    return cmr;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateStudentDetails(string lastname, string firstname, string fathername, string mothername, string collegeRegistration, string prn)
        {
            string iq = "Update studentdetails set prn='" + prn + "',lastname='" + lastname + "',firstname='" + firstname + "',fathername='" + fathername + "'," +
                " mothername='" + mothername + "' where collegeregistrationnumber='" + collegeRegistration + "'";

            return ExcuteQuery(iq);
        }

        public bool UpdateCourseFromHonorsToRegular(string studentId)
        {
            string query = "Update studentdetails set course=REPLACE(course,' - Honors','') where id=" + studentId;
            ExcuteQuery(query);
            return true;
        }

        public int UpdateMarkDetails(string rowId, string studentId, string paperCode, string internalMarks, string externalMarks, string practicalMarks, string grade, string attempt)
        {
            #region Check if ATKT record exists
            int atktRowId = 0;
            string query = string.Format("select id from atktmarksdetails where studentid='{0}' and paperCode='{1}'", studentId, paperCode);
            obcon = new OdbcConnection(connection);
            odbCommand = new OdbcCommand();
            odbCommand.Connection = obcon;
            obcon.Open();
            odbCommand.CommandText = query;
            string strID = Convert.ToString(odbCommand.ExecuteScalar());
            atktRowId = !string.IsNullOrEmpty(strID) ? (int)Convert.ToInt32(strID) : 0;

            obcon.Close();
            #endregion

            if (atktRowId > 0)
            {
                query = "Update atktmarksdetails set internalmarksobtained='" + internalMarks + "',externaltotalmarks='" + externalMarks + "',practicalmarksobtained='" + practicalMarks + "',gp='" + grade + "' " +
                      " where studentid=" + studentId + " and papercode='" + paperCode + "'";
                ExcuteQuery(query);

                // Insert the ATKT record in the marksdetails table and delete the record from atkt table
                // This is required in order to make sure that the ATKT record is present in marksdetails table as well
                query = string.Format("insert into marksdetails (studentid, examid, seatnumber, credit,isgrade, papercode, papertitle, papertype,iselective,internalpassingmarks, internalmarksobtained, externalpassingmarks, internaltotalmarks,externalsection1marks, externalsection2marks, externaltotalmarks, practicalmarksobtained,practicalmaxmarks, gracemarks, paperresult, gp, grade, attempt, remarks, externalmaxmarks) select studentid, examid, seatnumber, credit, isgrade, papercode, papertitle, papertype, iselective, internalpassingmarks, internalmarksobtained, externalpassingmarks, internaltotalmarks, externalsection1marks, externalsection2marks, externaltotalmarks, practicalmarksobtained, practicalmaxmarks, gracemarks, paperresult, gp, grade, attempt, remarks, externalmaxmarks FROM atktmarksdetails where id in ({0})", atktRowId);
                ExcuteQuery(query);

                query = string.Format("delete FROM atktmarksdetails where id in ({0})", atktRowId);
                ExcuteQuery(query);
            }

            // if marksdetails table had more than one record - update all the records having same studentid - papercode combination
            query = "Update marksdetails set internalmarksobtained='" + internalMarks + "',externaltotalmarks='" + externalMarks + "',practicalmarksobtained='" + practicalMarks + "',gp='" + grade + "'," +
                " retrycount='" + attempt + "' where studentid=" + studentId + " and papercode='" + paperCode + "'";

            return ExcuteQuery(query);
        }

        public CommonMarksheetResponse GetPaperDetails(int studentid)
        {
            try
            {
                CommonMarksheetResponse cmr = new CommonMarksheetResponse();
                List<PaperModel> papers = new List<PaperModel>();
                string query = string.Format("SELECT id, studentid, examid, seatnumber, credit, isgrade, papercode, papertitle, papertype, iselective, internalpassingmarks, internalmarksobtained, externalpassingmarks, internaltotalmarks, externalsection1marks, externalsection2marks, externaltotalmarks, practicalmarksobtained, practicalmaxmarks, gracemarks, paperresult, gp, grade, attempt, remarks, externalmaxmarks, RetryCount FROM marksdetails m where studentid = {0} union SELECT id, studentid, examid, seatnumber, credit, isgrade, papercode, papertitle, papertype, iselective, internalpassingmarks, internalmarksobtained, externalpassingmarks, internaltotalmarks, externalsection1marks, externalsection2marks, externaltotalmarks, practicalmarksobtained, practicalmaxmarks, gracemarks, paperresult, gp, grade, attempt, remarks, externalmaxmarks, 0 FROM atktmarksdetails m where studentid = {0} order by papercode;", studentid);
                obcon = new OdbcConnection(connection);
                DataSet dsItems = new DataSet();
                odbAdp = new OdbcDataAdapter(query, obcon); // need to add examtype
                odbAdp.Fill(dsItems);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        PaperModel paperDetails = new PaperModel();
                        string sid = row["studentid"].ToString();
                        paperDetails.PaperCode = row["papercode"].ToString();
                        //bool isContains = filteredResponse.PaperDetails.Any(a => a.PaperCode.Equals(paperDetails.PaperCode));
                        //if (!isContains)
                        //    continue;
                        //else
                        //{
                        paperDetails.Id = row["id"].ToString();
                        paperDetails.ExamId = row["examid"].ToString();
                        paperDetails.StudentId = row["studentid"].ToString();
                        //paperDetails.Semester = int.Parse(row["semester"].ToString());//GetSemeter(paperDetails.StudentId, paperDetails.ExamId);
                        paperDetails.PaperTitle = row["papertitle"].ToString();
                        paperDetails.Credit = row["credit"].ToString();
                        paperDetails.PaperType = row["papertype"].ToString();
                        paperDetails.InternalPassingMarks = row["internalpassingmarks"].ToString();
                        paperDetails.InternalTotalMarks = row["internaltotalmarks"].ToString();
                        paperDetails.InternalMarksObtained = row["internalmarksobtained"].ToString();
                        paperDetails.ExternalPassingMarks = row["externalpassingmarks"].ToString();
                        paperDetails.ExternalSection1Marks = row["externalsection1marks"].ToString();
                        paperDetails.ExternalSection2Marks = row["externalsection2marks"].ToString();
                        paperDetails.ExternalTotalMarks = row["externaltotalmarks"].ToString();
                        paperDetails.PracticalMarksObtained = row["practicalmarksobtained"].ToString();
                        paperDetails.PracticalMaxMarks = row["practicalmaxmarks"].ToString();
                        paperDetails.ExternalMaxMarks = row["externalmaxmarks"].ToString();
                        paperDetails.GraceMarks = row["gracemarks"].ToString();
                        paperDetails.PaperResult = row["paperresult"].ToString();
                        paperDetails.GP = row["gp"].ToString();
                        paperDetails.Grade = row["grade"].ToString();
                        paperDetails.Attempt = row["attempt"].ToString();
                        paperDetails.RetryCount = !string.IsNullOrEmpty(row["retrycount"].ToString()) ? int.Parse(row["retrycount"].ToString()) : 1;
                        paperDetails.Remarks = row["remarks"].ToString();
                        //paperDetails.semester = row["semester"].ToString();
                        //paperDetails.monthOfExam = row["monthOfExam"].ToString();
                        //paperDetails.ResultStatus = row["ResultStatus"].ToString();
                        string retryCont = Convert.ToString(row["RetryCount"]);
                        int retry;
                        if (int.TryParse(retryCont, out retry))
                        {
                            paperDetails.RetryCount = retry;
                        }
                        else
                        {
                            paperDetails.RetryCount = 0;
                        }
                        //}
                        papers.Add(paperDetails);
                    }
                    cmr.Papers = papers;
                    return cmr;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CommonMarksheetResponse GetStudentDetails(string studentId)
        {
            try
            {
                CommonMarksheetResponse cmr = new CommonMarksheetResponse();
                string query = string.Format("select * from studentdetails where id={0}", studentId);
                obcon = new OdbcConnection(connection);
                DataSet dsItems = new DataSet();
                odbAdp = new OdbcDataAdapter(query, obcon); // need to add examtype
                odbAdp.Fill(dsItems);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        cmr.Id = row["id"].ToString();
                        cmr.PRN = row["prn"].ToString();
                        cmr.CollegeRegistrationNumber = row["collegeregistrationnumber"].ToString();
                        cmr.AdmissionYear = row["admissionyear"].ToString();
                        cmr.LastName = row["lastname"].ToString();
                        cmr.FirstName = row["firstname"].ToString();
                        cmr.FatherName = row["fathername"].ToString();
                        cmr.MotherName = row["mothername"].ToString();
                        cmr.StudentFullName = cmr.LastName + " " + cmr.FirstName + " " + cmr.FatherName + " " + cmr.MotherName;
                        cmr.Stream = row["stream"].ToString();
                        cmr.Course = row["course"].ToString();
                        cmr.Specialisation = row["specialisation"].ToString();

                    }

                    return cmr;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CommonMarksheetResponse> GetStudentList(string studentIds)
        {
            try
            {
                List<CommonMarksheetResponse> cmrList = new List<CommonMarksheetResponse>();
                string query = string.Format("select * from studentdetails where id in ({0})", studentIds);
                obcon = new OdbcConnection(connection);
                DataSet dsItems = new DataSet();
                odbAdp = new OdbcDataAdapter(query, obcon); // need to add examtype
                odbAdp.Fill(dsItems);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        CommonMarksheetResponse cmr = new CommonMarksheetResponse();
                        cmr.Id = row["id"].ToString();
                        cmr.PRN = row["prn"].ToString();
                        cmr.CollegeRegistrationNumber = row["collegeregistrationnumber"].ToString();
                        cmr.AdmissionYear = row["admissionyear"].ToString();
                        cmr.LastName = row["lastname"].ToString();
                        cmr.FirstName = row["firstname"].ToString();
                        cmr.FatherName = row["fathername"].ToString();
                        cmr.MotherName = row["mothername"].ToString();
                        cmr.StudentFullName = cmr.LastName + " " + cmr.FirstName + " " + cmr.FatherName + " " + cmr.MotherName;
                        cmr.Stream = row["stream"].ToString();
                        cmr.Course = row["course"].ToString();
                        cmr.Specialisation = row["specialisation"].ToString();
                        cmrList.Add(cmr);

                    }

                    return cmrList;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CommonMarksheetResponse> GetCommonMarksheetData(string sem, string examType, string specialisationCode)
        {
            try
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + "/");

                string query = string.Empty;
                string mySemOldValue = string.Empty;
                if (sem.ToLower() == "all")
                {
                    sem = "4";
                    mySemOldValue = "all";
                }
                if (examType.ToLower().Equals("regular"))
                {
                    filePath = filePath + "CommonMarksheetSem" + sem + "Papers.json";
                }
                else
                {
                    filePath = filePath + "CommonMarksheetSem" + sem + "HonorsPapers.json";
                }
                List<CommonMarksheetResponse> listOfCMR = new List<CommonMarksheetResponse>();
                if (!File.Exists(filePath))
                {
                    throw new Exception(string.Format("JSON file not found for semester {0}", sem));
                }
                List<CommonMarksheetSem4Papers> jResponse = JsonConvert.DeserializeObject<List<CommonMarksheetSem4Papers>>(System.IO.File.ReadAllText(filePath));
                CommonMarksheetSem4Papers filteredResponse = jResponse.FirstOrDefault(a => a.SpecialisationCode.Equals(specialisationCode.ToUpper()));


                if (mySemOldValue.ToLower() == "all")
                {
                    string filePath6 = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + "/");

                    if (examType.ToLower().Equals("regular"))
                        filePath6 = filePath6 + "CommonMarksheetSem6Papers.json";
                    else
                        filePath6 = filePath6 + "CommonMarksheetSem6HonorsPapers.json";
                    List<CommonMarksheetSem4Papers> jResponse6 = JsonConvert.DeserializeObject<List<CommonMarksheetSem4Papers>>(System.IO.File.ReadAllText(filePath6));
                    CommonMarksheetSem4Papers filteredResponse6 = jResponse6.FirstOrDefault(a => a.SpecialisationCode.Equals(specialisationCode.ToUpper()));
                    filteredResponse.PaperDetails.AddRange(filteredResponse6.PaperDetails);
                }
                if (mySemOldValue.ToLower() == "all")
                {
                    if (examType.ToLower().Equals("regular"))
                        query = "RegularWithATKTPapers";
                    else
                        query = "HonorsWithATKT";
                }
                else
                {
                    if (examType.ToLower().Equals("regular"))
                        query = "RegularWithoutATKT";
                    else
                        query = "HonorsWithoutATKT";
                }

                obcon = new OdbcConnection(connection);
                DataSet dsItems = new DataSet();
                odbAdp = new OdbcDataAdapter("CALL " + query + "('" + filteredResponse.Specialisation + "')", obcon); // need to add examtype
                //odbAdp.SelectCommand.Parameters.AddWithValue("@specialisation", );
                odbAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
                odbAdp.Fill(dsItems);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    List<PaperModel> paperList = new List<PaperModel>();
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        PaperModel paperDetails = new PaperModel();
                        string sid = row["studentid"].ToString();
                        paperDetails.PaperCode = row["papercode"].ToString();
                        bool isContains = filteredResponse.PaperDetails.Any(a => a.PaperCode.Equals(paperDetails.PaperCode));
                        if (!isContains)
                            continue;
                        else
                        {
                            paperDetails.Id = row["id"].ToString();
                            paperDetails.ExamId = row["examid"].ToString();
                            paperDetails.StudentId = row["studentid"].ToString();
                            paperDetails.Semester = int.Parse(row["semester"].ToString());//GetSemeter(paperDetails.StudentId, paperDetails.ExamId);
                            paperDetails.PaperTitle = row["papertitle"].ToString();
                            paperDetails.Credit = row["credit"].ToString();
                            paperDetails.PaperType = row["papertype"].ToString();
                            paperDetails.InternalPassingMarks = row["internalpassingmarks"].ToString();
                            paperDetails.InternalTotalMarks = row["internaltotalmarks"].ToString();
                            paperDetails.InternalMarksObtained = row["internalmarksobtained"].ToString();
                            paperDetails.ExternalPassingMarks = row["externalpassingmarks"].ToString();
                            paperDetails.ExternalSection1Marks = row["externalsection1marks"].ToString();
                            paperDetails.ExternalSection2Marks = row["externalsection2marks"].ToString();
                            paperDetails.ExternalTotalMarks = row["externaltotalmarks"].ToString();
                            paperDetails.PracticalMarksObtained = row["practicalmarksobtained"].ToString();
                            paperDetails.PracticalMaxMarks = row["practicalmaxmarks"].ToString();
                            paperDetails.ExternalMaxMarks = row["externalmaxmarks"].ToString();
                            paperDetails.GraceMarks = row["gracemarks"].ToString();
                            paperDetails.PaperResult = row["paperresult"].ToString();
                            paperDetails.GP = row["gp"].ToString();
                            paperDetails.Grade = row["grade"].ToString();
                            paperDetails.Attempt = row["attempt"].ToString();
                            paperDetails.RetryCount = !string.IsNullOrEmpty(row["retrycount"].ToString()) ? int.Parse(row["retrycount"].ToString()) : 1;
                            paperDetails.Remarks = row["remarks"].ToString();
                            string examYear = row["yearOfExam"].ToString();
                            int ExamYear = 0;
                            if (int.TryParse(examYear, out ExamYear))
                            {
                                paperDetails.yearOfExam = ExamYear;
                            }
                            else
                            {
                                paperDetails.yearOfExam = 0;
                            }
                            paperDetails.semester = row["semester"].ToString();
                            paperDetails.monthOfExam = row["monthOfExam"].ToString();
                            paperDetails.ResultStatus = row["ResultStatus"].ToString();
                            string retryCont = Convert.ToString(row["RetryCount"]);
                            int retry;
                            if (int.TryParse(retryCont, out retry))
                            {
                                paperDetails.RetryCount = retry;
                            }
                            else
                            {
                                paperDetails.RetryCount = 0;
                            }
                        }
                        paperList.Add(paperDetails);
                    }
                    List<List<PaperModel>> groupedCustomerList = paperList.GroupBy(u => u.StudentId).Select(grp => grp.ToList()).ToList();
                    string ids = "";

                    foreach (List<PaperModel> li in groupedCustomerList)
                    {
                        ids += (li.FirstOrDefault().StudentId) + ",";
                    }

                    ids = ids.Remove(ids.Length - 1);

                    List<CommonMarksheetResponse> cmr = GetStudentList(ids);

                    foreach (List<PaperModel> li in groupedCustomerList)
                    {
                        CommonMarksheetResponse r = cmr.FirstOrDefault(p => p.Id == li.FirstOrDefault().StudentId);
                        if (r == null)
                            continue;
                        else
                        {
                            var lstPapers = li.OrderBy(y => y.PaperCode).OrderBy(x => x.Semester).ToList();
                            List<PaperModel> lm = new List<PaperModel>();

                            Hashtable hTable = new Hashtable();
                            ArrayList duplicateList = new ArrayList();

                            foreach (var ppr in lstPapers)
                            {
                                if (hTable.Contains(ppr.PaperCode))
                                    ppr.IsDuplicate = true;
                                else
                                    hTable.Add(ppr.PaperCode, string.Empty);
                            }
                            //var finalList = lstPapers.Where(x => x.IsDuplicate==true).ToList();
                            //r.Papers = li;
                            r.Papers = lstPapers.Where(x => x.IsDuplicate == false).ToList();

                            if (r.Papers.Find(p => p.Semester == 6) != null && r.Specialisation.ToLower() == filteredResponse.Specialisation.ToLower().Replace("&", "and"))
                            {
                                //r.Papers.Add(new PaperModel
                                //{
                                //    PaperCode = "SVTVI51",
                                //    PaperTitle = "Internship",
                                //    ResultStatus = "Satisfactory",
                                //    Credit = filteredResponse.PaperDetails.FirstOrDefault(p => p.PaperTitle == "Internship").Credits,
                                //    PaperType = "PR"
                                //});
                                listOfCMR.Add(r);
                            }
                        }
                    }



                    return listOfCMR;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConvocationList> GetConvocationRequests(string sem, string examType, string specialisationCode)
        {
            try
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + "/");

                string query = string.Empty;
                string mySemOldValue = string.Empty;
                if (sem.ToLower() == "all")
                {
                    sem = "4";
                    mySemOldValue = "all";
                }
                if (examType.ToLower().Equals("regular"))
                {
                    filePath = filePath + "CommonMarksheetSem" + sem + "Papers.json";
                }
                else
                {
                    filePath = filePath + "CommonMarksheetSem" + sem + "HonorsPapers.json";
                }
                List<CommonMarksheetResponse> listOfCMR = new List<CommonMarksheetResponse>();
                if (!File.Exists(filePath))
                {
                    throw new Exception(string.Format("JSON file not found for semester {0}", sem));
                }
                List<CommonMarksheetSem4Papers> jResponse = JsonConvert.DeserializeObject<List<CommonMarksheetSem4Papers>>(System.IO.File.ReadAllText(filePath));
                CommonMarksheetSem4Papers filteredResponse = jResponse.FirstOrDefault(a => a.SpecialisationCode.Equals(specialisationCode.ToUpper()));


                if (mySemOldValue.ToLower() == "all")
                {
                    string filePath6 = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + "/");

                    if (examType.ToLower().Equals("regular"))
                        filePath6 = filePath6 + "CommonMarksheetSem6Papers.json";
                    else
                        filePath6 = filePath6 + "CommonMarksheetSem6HonorsPapers.json";
                    List<CommonMarksheetSem4Papers> jResponse6 = JsonConvert.DeserializeObject<List<CommonMarksheetSem4Papers>>(System.IO.File.ReadAllText(filePath6));
                    CommonMarksheetSem4Papers filteredResponse6 = jResponse6.FirstOrDefault(a => a.SpecialisationCode.Equals(specialisationCode.ToUpper()));
                    filteredResponse.PaperDetails.AddRange(filteredResponse6.PaperDetails);
                }
                if (mySemOldValue.ToLower() == "all")
                {
                    if (examType.ToLower().Equals("regular"))
                        query = "RegularWithATKTPapers";
                    else
                        query = "HonorsWithATKT";
                }
                else
                {
                    if (examType.ToLower().Equals("regular"))
                        query = "RegularWithoutATKT";
                    else
                        query = "HonorsWithoutATKT";
                }

                obcon = new OdbcConnection(connection);
                DataSet dsItems = new DataSet();
                odbAdp = new OdbcDataAdapter("CALL " + query + "('" + filteredResponse.Specialisation + "')", obcon); // need to add examtype
                //odbAdp.SelectCommand.Parameters.AddWithValue("@specialisation", );
                odbAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
                odbAdp.Fill(dsItems);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    List<PaperModel> paperList = new List<PaperModel>();
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        PaperModel paperDetails = new PaperModel();
                        string sid = row["studentid"].ToString();
                        paperDetails.PaperCode = row["papercode"].ToString();
                        bool isContains = filteredResponse.PaperDetails.Any(a => a.PaperCode != string.Empty && a.PaperCode.Equals(paperDetails.PaperCode));
                        if (!isContains)
                            continue;
                        else
                        {
                            paperDetails.Id = row["id"].ToString();
                            paperDetails.ExamId = row["examid"].ToString();
                            paperDetails.StudentId = row["studentid"].ToString();
                            paperDetails.Semester = int.Parse(row["semester"].ToString());//GetSemeter(paperDetails.StudentId, paperDetails.ExamId);
                            paperDetails.PaperTitle = row["papertitle"].ToString();
                            paperDetails.Credit = row["credit"].ToString();
                            paperDetails.PaperType = row["papertype"].ToString();
                            paperDetails.InternalPassingMarks = row["internalpassingmarks"].ToString();
                            paperDetails.InternalTotalMarks = row["internaltotalmarks"].ToString();
                            paperDetails.InternalMarksObtained = row["internalmarksobtained"].ToString();
                            paperDetails.ExternalPassingMarks = row["externalpassingmarks"].ToString();
                            paperDetails.ExternalSection1Marks = row["externalsection1marks"].ToString();
                            paperDetails.ExternalSection2Marks = row["externalsection2marks"].ToString();
                            paperDetails.ExternalTotalMarks = row["externaltotalmarks"].ToString();
                            paperDetails.PracticalMarksObtained = row["practicalmarksobtained"].ToString();
                            paperDetails.PracticalMaxMarks = row["practicalmaxmarks"].ToString();
                            paperDetails.ExternalMaxMarks = row["externalmaxmarks"].ToString();
                            paperDetails.GraceMarks = row["gracemarks"].ToString();
                            paperDetails.PaperResult = row["paperresult"].ToString();
                            paperDetails.GP = row["gp"].ToString();
                            paperDetails.Grade = row["grade"].ToString();
                            paperDetails.Attempt = row["attempt"].ToString();
                            paperDetails.RetryCount = !string.IsNullOrEmpty(row["retrycount"].ToString()) ? int.Parse(row["retrycount"].ToString()) : 1;
                            paperDetails.Remarks = row["remarks"].ToString();
                            string examYear = row["yearOfExam"].ToString();
                            int ExamYear = 0;
                            if (int.TryParse(examYear, out ExamYear))
                            {
                                paperDetails.yearOfExam = ExamYear;
                            }
                            else
                            {
                                paperDetails.yearOfExam = 0;
                            }
                            paperDetails.semester = row["semester"].ToString();
                            paperDetails.monthOfExam = row["monthOfExam"].ToString();
                            paperDetails.ResultStatus = row["ResultStatus"].ToString();
                            string retryCont = Convert.ToString(row["RetryCount"]);
                            int retry;
                            if (int.TryParse(retryCont, out retry))
                            {
                                paperDetails.RetryCount = retry;
                            }
                            else
                            {
                                paperDetails.RetryCount = 0;
                            }
                        }
                        paperList.Add(paperDetails);
                    }
                    List<List<PaperModel>> groupedCustomerList = paperList.GroupBy(u => u.StudentId).Select(grp => grp.ToList()).ToList();
                    string ids = "";

                    foreach (List<PaperModel> li in groupedCustomerList)
                    {
                        ids += (li.FirstOrDefault().StudentId) + ",";
                    }

                    ids = ids.Remove(ids.Length - 1);

                    List<CommonMarksheetResponse> cmr = GetStudentList(ids);

                    foreach (List<PaperModel> li in groupedCustomerList)
                    {
                        CommonMarksheetResponse r = cmr.FirstOrDefault(p => p.Id == li.FirstOrDefault().StudentId);
                        if (r == null)
                            continue;
                        else
                        {
                            var lstPapers = li.OrderBy(y => y.PaperCode).OrderBy(x => x.Semester).ToList();
                            List<PaperModel> lm = new List<PaperModel>();

                            Hashtable hTable = new Hashtable();
                            ArrayList duplicateList = new ArrayList();

                            foreach (var ppr in lstPapers)
                            {
                                if (hTable.Contains(ppr.PaperCode))
                                    ppr.IsDuplicate = true;
                                else
                                    hTable.Add(ppr.PaperCode, string.Empty);
                            }
                            //var finalList = lstPapers.Where(x => x.IsDuplicate==true).ToList();
                            //r.Papers = li;
                            r.Papers = lstPapers.Where(x => x.IsDuplicate == false).ToList();

                            if (r.Papers.Find(p => p.Semester == 6) != null && r.Specialisation.ToLower() == filteredResponse.Specialisation.ToLower().Replace("&", "and"))
                            {
                                listOfCMR.Add(r);
                            }
                        }
                    }

                    List<ConvocationList> convocationList = new List<ConvocationList>();
                    foreach (CommonMarksheetResponse cmrList in listOfCMR)
                    {
                        var failed = cmrList.Papers.Find(p => p.GP == "F");
                        if (failed == null)
                        {
                            float totalMarks = 0;
                            float totalCredit = 0;
                            float weightedMarks = 0;
                            string grade = string.Empty;

                            foreach (PaperModel paper in cmrList.Papers)
                            {
                                paper.InternalMarksObtained = string.IsNullOrEmpty(paper.InternalMarksObtained) ? "0" : paper.InternalMarksObtained;
                                paper.ExternalTotalMarks = string.IsNullOrEmpty(paper.ExternalTotalMarks) ? "0" : paper.ExternalTotalMarks;
                                paper.PracticalMarksObtained = string.IsNullOrEmpty(paper.PracticalMarksObtained) ? "0" : paper.PracticalMarksObtained;
                                float subjectMarks = float.Parse(paper.InternalMarksObtained) + float.Parse(paper.ExternalTotalMarks) + float.Parse(paper.PracticalMarksObtained);
                                float subjectCredit = float.Parse(paper.Credit);
                                totalMarks += subjectMarks;
                                totalCredit += subjectCredit;
                                weightedMarks += (subjectMarks * subjectCredit);
                            }

                            ConvocationList convo = new ConvocationList();
                            convo.StudentId = int.Parse(cmrList.Id);
                            convo.StudentName = cmrList.LastName + " " + cmrList.FirstName + " " + cmrList.FatherName + " " + cmrList.MotherName;
                            convo.Crn = cmrList.CollegeRegistrationNumber;
                            convo.WeightedPercentage = (weightedMarks / totalCredit);
                            convo.TotalCredits = totalCredit;
                            convo.TotalMarks = totalMarks;
                            convo.Specialisation = cmrList.Specialisation;
                            convo.Grade = GetGPAGrade(convo.WeightedPercentage);
                            convocationList.Add(convo);
                        }
                    }

                    return convocationList;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetGPAGrade(float weightedPercent)
        {
            if (weightedPercent < 40) return "F";
            else if (weightedPercent >= 40 && weightedPercent < 44) return "P";
            else if (weightedPercent >= 45 && weightedPercent < 50) return "C";
            else if (weightedPercent >= 50 && weightedPercent < 55) return "B";
            else if (weightedPercent >= 55 && weightedPercent < 60) return "B+";
            else if (weightedPercent >= 60 && weightedPercent < 70) return "A";
            else if (weightedPercent >= 70 && weightedPercent < 80) return "A+";
            else if (weightedPercent >= 80 && weightedPercent < 90) return "O";
            else if (weightedPercent >= 90 && weightedPercent < 101) return "O+";

            return "";
        }

        public List<ConvocationList> GetConvocationList(string examType, string specialisationCode)
        {
            try
            {
                List<CommonMarksheetResponse> listOfCMR = new List<CommonMarksheetResponse>();
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/");
                string query = string.Empty;
                string mySemOldValue = string.Empty;

                if (examType.ToLower().Equals("regular"))
                {
                    filePath = filePath + "CommonMarksheetSem4Papers.json";
                }
                else
                {
                    filePath = filePath + "CommonMarksheetSem4HonorsPapers.json";
                }

                List<CommonMarksheetSem4Papers> jResponse = JsonConvert.DeserializeObject<List<CommonMarksheetSem4Papers>>(System.IO.File.ReadAllText(filePath));
                CommonMarksheetSem4Papers filteredResponse = jResponse.FirstOrDefault(a => a.SpecialisationCode.Equals(specialisationCode.ToUpper()));


                if (mySemOldValue.ToLower() == "all")
                {
                    string filePath6 = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + "/");

                    if (examType.ToLower().Equals("regular"))
                        filePath6 = filePath6 + "CommonMarksheetSem6Papers.json";
                    else
                        filePath6 = filePath6 + "CommonMarksheetSem6HonorsPapers.json";
                    List<CommonMarksheetSem4Papers> jResponse6 = JsonConvert.DeserializeObject<List<CommonMarksheetSem4Papers>>(System.IO.File.ReadAllText(filePath6));
                    CommonMarksheetSem4Papers filteredResponse6 = jResponse6.FirstOrDefault(a => a.SpecialisationCode.Equals(specialisationCode.ToUpper()));
                    filteredResponse.PaperDetails.AddRange(filteredResponse6.PaperDetails);
                }
                if (mySemOldValue.ToLower() == "all")
                {
                    if (examType.ToLower().Equals("regular"))
                        query = "RegularWithATKTPapers";
                    else
                        query = "HonorsWithATKT";
                }
                else
                {
                    if (examType.ToLower().Equals("regular"))
                        query = "RegularWithoutATKT";
                    else
                        query = "HonorsWithoutATKT";
                }

                obcon = new OdbcConnection(connection);
                DataSet dsItems = new DataSet();
                odbAdp = new OdbcDataAdapter("CALL " + query + "('" + filteredResponse.Specialisation + "')", obcon);

                odbAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
                odbAdp.Fill(dsItems);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    List<PaperModel> paperList = new List<PaperModel>();
                    List<string> failedStudents = new List<string>();
                    List<ConvocationList> convocationList = new List<ConvocationList>();
                    int counter = 0;
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        counter++;
                        PaperModel paperDetails = new PaperModel();
                        string sid = row["studentid"].ToString();
                        paperDetails.PaperCode = row["papercode"].ToString();
                        bool isContains = filteredResponse.PaperDetails.Any(a => a.PaperCode != string.Empty && a.PaperCode.Equals(paperDetails.PaperCode));
                        if (!isContains)
                            continue;
                        else
                        {
                            if (failedStudents.Contains(row["studentid"].ToString()))
                                continue;

                            ConvocationList convocation = convocationList.Find(p => p.StudentId == int.Parse(row["studentid"].ToString()));
                            if (convocation == null)
                            {
                                convocation = new ConvocationList();
                                convocation.StudentId = int.Parse(row["studentid"].ToString());
                                convocation.StudentName = string.Empty;
                            }

                            float credit = float.Parse(row["credit"].ToString());
                            float marks = float.Parse(row["internalmarksobtained"].ToString()) + float.Parse(row["externaltotalmarks"].ToString()) + float.Parse(row["practicalmarksobtained"].ToString());

                            convocation.TotalCredits += credit;
                            convocation.TotalMarks += (marks * credit);
                            convocation.WeightedPercentage = convocation.TotalMarks / convocation.TotalCredits;
                            convocationList.Add(convocation);
                        }

                    }

                    string ids = "";

                    foreach (ConvocationList li in convocationList)
                    {
                        ids += (li.StudentId) + ",";
                    }

                    if (ids.Length > 0)
                    {
                        ids = ids.Remove(ids.Length - 1);
                        List<CommonMarksheetResponse> cmr = GetStudentList(ids);
                        foreach (ConvocationList li in convocationList)
                        {
                            CommonMarksheetResponse r = cmr.FirstOrDefault(p => p.Id == li.StudentId.ToString());
                            li.StudentName = r.LastName + " " + r.FirstName + " " + r.FatherName + " " + r.MotherName;
                        }
                    }

                    return convocationList;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetStudentExamDetailsById(int id)
        {
            string query = string.Format("select * from studentexamdetails where studentid={0}", id);
            obcon = new OdbcConnection(connection);
            DataSet dsItems = new DataSet();
            odbAdp = new OdbcDataAdapter(query, obcon); // need to add examtype
            odbAdp.Fill(dsItems);
            return dsItems.Tables[0];
        }

        public DataTable GetStudentExamHestory(string StudentId)
        {
            string query = string.Format("select MD.* , (Select semester from studentexamdetails where id=MD.examid) as semester from marksdetails MD" +
            " WHERE MD.studentid = {0}", StudentId);
            obcon = new OdbcConnection(connection);
            DataSet dsItems = new DataSet();
            odbAdp = new OdbcDataAdapter(query, obcon);
            odbAdp.Fill(dsItems);
            return dsItems.Tables[0];
        }

        public float GetSemesterPercent(string StudentId, int semester)
        {
            string query = string.Format("select PaperTitle,(internalmarksobtained+externaltotalmarks+practicalmarksobtained) as TotalObtained, (internaltotalmarks+externalmaxmarks+practicalmaxmarks) as TotalMarks from marksdetails where studentid={0} and semester={1} ", StudentId, semester);
            obcon = new OdbcConnection(connection);
            DataSet dsItems = new DataSet();
            odbAdp = new OdbcDataAdapter(query, obcon);
            odbAdp.Fill(dsItems);

            int marksObtained = 0;
            int totalMarks = 0;
            float percent = 0;
            foreach (DataRow row in dsItems.Tables[0].Rows)
            {
                marksObtained += int.Parse(row["TotalObtained"].ToString());
                totalMarks += int.Parse(row["TotalMarks"].ToString());
            }
            if (totalMarks == 0)
                percent = 0;
            else
                percent = float.Parse(marksObtained.ToString()) / float.Parse(totalMarks.ToString()) * 100;
            return percent;
        }

        public DataTable GetSemesterData(string studentId, int semester)
        {
            string query = string.Format("select papercode,papertitle,papertype,credit,gp,attempt,internalmarksobtained,externaltotalmarks,practicalmarksobtained,(internalmarksobtained+externaltotalmarks+practicalmarksobtained) as TotalObtained, (internaltotalmarks+externalmaxmarks+practicalmaxmarks) as TotalMarks from marksdetails where studentid = {0} and semester={1}", studentId, semester);
            obcon = new OdbcConnection(connection);
            DataSet dsItems = new DataSet();
            odbAdp = new OdbcDataAdapter(query, obcon);
            odbAdp.Fill(dsItems);
            return dsItems.Tables[0];
        }

        public List<CommonMarksheetResponse> GetTranscriptDataByStudent(string studentId, string sem, string examType, string specialisationCode)
        {
            try
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + "/");
                string honours = "%-Honors";
                string query = string.Empty;
                if (examType.ToLower().Equals("regular"))
                {
                    filePath = filePath + "CommonMarksheetSem4Papers.json";
                }
                else
                {
                    filePath = filePath + "CommonMarksheetSem4HonorsPapers.json";
                }
                List<CommonMarksheetResponse> listOfCMR = new List<CommonMarksheetResponse>();
                List<CommonMarksheetSem4Papers> jResponse = JsonConvert.DeserializeObject<List<CommonMarksheetSem4Papers>>(System.IO.File.ReadAllText(filePath));
                CommonMarksheetSem4Papers filteredResponse = jResponse.FirstOrDefault(a => a.SpecialisationCode.Equals(specialisationCode) || a.Specialisation.Equals(specialisationCode));

                if (examType.ToLower().Equals("regular"))
                {
                    query = string.Format("select * from marksdetails where printedcommon4={0} and studentid not in " +
                    " (select studentid from atktmarksdetails) and studentid in (select id from studentdetails where" +
                    " specialisation='{1}' and course not LIKE '{2}') and studentid={3}", false, filteredResponse.Specialisation, honours, studentId);
                }
                else
                {
                    query = string.Format("select * from marksdetails where printedcommon4={0} and studentid not in " +
                   " (select studentid from atktmarksdetails) and studentid in (select id from studentdetails where" +
                   " specialisation='{1}' and course LIKE '{2}') and studentid={3}", false, filteredResponse.Specialisation, honours, studentId);
                }

                obcon = new OdbcConnection(connection);
                DataSet dsItems = new DataSet();
                odbAdp = new OdbcDataAdapter(query, obcon); // need to add examtype
                odbAdp.Fill(dsItems);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    List<PaperModel> paperList = new List<PaperModel>();
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        PaperModel paperDetails = new PaperModel();
                        paperDetails.PaperCode = row["papercode"].ToString();
                        bool isContains = filteredResponse.PaperDetails.Any(a => a.PaperCode.Equals(paperDetails.PaperCode));
                        if (!isContains)
                            continue;
                        else
                        {
                            paperDetails.Id = row["id"].ToString();
                            paperDetails.ExamId = row["examid"].ToString();
                            paperDetails.StudentId = row["studentid"].ToString();
                            paperDetails.Semester = GetSemeter(paperDetails.StudentId, paperDetails.ExamId);
                            paperDetails.PaperTitle = row["papertitle"].ToString();
                            paperDetails.Credit = row["credit"].ToString();
                            paperDetails.PaperType = row["papertype"].ToString();
                            paperDetails.InternalPassingMarks = row["internalpassingmarks"].ToString();
                            paperDetails.InternalTotalMarks = row["internaltotalmarks"].ToString();
                            paperDetails.InternalMarksObtained = row["internalmarksobtained"].ToString();
                            paperDetails.ExternalPassingMarks = row["externalpassingmarks"].ToString();
                            paperDetails.ExternalSection1Marks = row["externalsection1marks"].ToString();
                            paperDetails.ExternalSection2Marks = row["externalsection2marks"].ToString();
                            paperDetails.ExternalTotalMarks = row["externaltotalmarks"].ToString();
                            paperDetails.PracticalMarksObtained = row["practicalmarksobtained"].ToString();
                            paperDetails.PracticalMaxMarks = row["practicalmaxmarks"].ToString();
                            paperDetails.ExternalMaxMarks = row["externalmaxmarks"].ToString();
                            paperDetails.GraceMarks = row["gracemarks"].ToString();
                            paperDetails.PaperResult = row["paperresult"].ToString();
                            paperDetails.GP = row["gp"].ToString();
                            paperDetails.Grade = row["grade"].ToString();
                            paperDetails.Attempt = row["attempt"].ToString();
                            paperDetails.Remarks = row["remarks"].ToString();
                        }
                        paperList.Add(paperDetails);
                    }
                    List<List<PaperModel>> groupedCustomerList = paperList.GroupBy(u => u.StudentId).Select(grp => grp.ToList()).ToList();
                    foreach (List<PaperModel> li in groupedCustomerList)
                    {
                        CommonMarksheetResponse r = GetStudentDetails(li.FirstOrDefault().StudentId);
                        if (r == null)
                            continue;
                        else
                        {
                            r.Papers = li;
                            listOfCMR.Add(r);
                        }
                    }

                    return listOfCMR;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //0 all 1 pending 2 completed
        public DataSet GetTranscriptRequest(int status)
        {
            string query = string.Empty;
            if (status == 0)
            {
                query = "select * from transcript";
            }
            else if (status == 1)
            {
                query = "select * from transcript WHERE RequestStatus=0";
            }
            else
            {
                query = "select * from transcript WHERE RequestStatus=1";
            }
            DataSet ds = GetQuery(query);
            ds.Tables[0].TableName = "transcript";
            return ds;
        }

        public string BuildAttendanceInsertQuery(Attendance attendance)
        {
            return "INSERT into attendance (bioempcode,punchin,punchout,ipaddress) values (" + attendance.BioEmpCode + ", '" + attendance.PunchInTime.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + attendance.PunchOutTime.ToString("yyyy-MM-dd HH:mm:ss") + "','" + attendance.IPAddress + "');";
        }

        public bool IsElectiveExist(string crn, string semester, string year)
        {
            string query = string.Format("SELECT id FROM electives WHERE crn='{0}' and semester='{1}' and academicyear={2}", crn, semester, year);

            obcon = new OdbcConnection(connection);
            DataSet dsItems = new DataSet();
            odbAdp = new OdbcDataAdapter(query, obcon);
            odbAdp.Fill(dsItems);

            try
            {
                if (dsItems.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }

        public string GetUpdateElectiveQuery(Electives electives)
        {
            return string.Format("UPDATE electives SET crn='{0}',department='{1}',specialisation='{2}', semester='{3}', elective1='{4}', elective2='{5}', elective3='{6}', elective4='{7}', elective5='{8}', elective6='{9}', elective6='{10}', lastmodified='{11}', academicyear={12}, rollnumber={13}, studentname='{14}' WHERE crn='{0}' and semester='{3}' and academicyear={11}"
                , electives.CollegeRegistartionNumber
                , electives.Department
                , electives.Specialisation
                , electives.Semester
                , electives.Elective1
                , electives.Elective2
                , electives.Elective3
                , electives.Elective4
                , electives.Elective5
                , electives.Elective6
                , electives.Elective7
                , DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss")
                , electives.Year
                , electives.RollNumber
                , electives.StudentName
                );
        }

        public string GetUpdateApprovedElectiveQuery(Electives electives)
        {
            return string.Format("UPDATE electives SET approvedelective1='{0}', approvedelective2='{1}', approvedelective3='{2}', approvedelective4='{3}', approvedelective5='{4}', approvedelective6='{5}',approvedelective7='{6}', approvedby='{7}', approvaldate='{8}' WHERE crn='{9}' and semester='{10}' and academicyear={11}"
                , electives.ApprovedElective1
                , electives.ApprovedElective2
                , electives.ApprovedElective3
                , electives.ApprovedElective4
                , electives.ApprovedElective5
                , electives.ApprovedElective6
                , electives.ApprovedElective7
                , electives.ApprovedBy
                , DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss")
                , electives.CollegeRegistartionNumber
                , electives.Semester
                , electives.Year
                );
        }

        public string GetInsertElectiveQuery(Electives electives)
        {
            return string.Format("INSERT INTO electives (crn,department,specialisation, semester, elective1, elective2, elective3, elective4, elective5, elective6, elective7, lastmodified, academicyear, rollnumber, studentname) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},{13},'{14}');"
                , electives.CollegeRegistartionNumber
                , electives.Department
                , electives.Specialisation
                , electives.Semester
                , electives.Elective1
                , electives.Elective2
                , electives.Elective3
                , electives.Elective4
                , electives.Elective5
                , electives.Elective6
                , electives.Elective7
                , DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm:ss")
                , electives.Year
                , electives.RollNumber
                , electives.StudentName
                );
        }

        public DataTable GetStudentElectives(string specialisation, string semester, string year)
        {
            string query = string.Format("SELECT crn,department,specialisation, semester, elective1, elective2, elective3, elective4, elective5, elective6, elective7, approvedelective1, approvedelective2, approvedelective3, approvedelective4, approvedelective5, approvedelective6, approvedelective7, lastmodified, academicyear, rollnumber, studentname FROM electives WHERE specialisation='{0}' and semester='{1}' and academicyear='{2}'", specialisation, semester, year);
            obcon = new OdbcConnection(connection);
            DataSet dsItems = new DataSet();
            odbAdp = new OdbcDataAdapter(query, obcon);
            odbAdp.Fill(dsItems);
            return dsItems.Tables[0];
        }

        public int InsertElective(string query)
        {
            return ExcuteQuery(query);
        }


        public int InsertAttendanceRecord(string query)
        {
            //string query = "INSERT into attendance (bioempcode,punchin,punchout,ipaddress) " +
            //    "SELECT * FROM (SELECT " + attendance.BioEmpCode + ", '" + attendance.PunchInTime.ToString() + "', '" + attendance.PunchOutTime.ToString() + "','" + attendance.IPAddress + "') AS atten WHERE NOT EXISTS(SELECT id FROM attendance WHERE bioempcode = " + attendance.BioEmpCode + " and punchin='" + attendance.PunchInTime.ToString() + "' and punchout='" + attendance.PunchOutTime.ToString() + "') LIMIT 1;";
            return ExcuteQuery(query);
        }

        public DataTable GetAttendanceSummary(string bioEmpCode, int month, int year)
        {
            string query = string.Format("SELECT punchin,punchout FROM attendance WHERE (YEAR(punchin) = {0} and MONTH(punchin) = {1}) and bioempcode={2} ORDER BY punchin desc", year, month, bioEmpCode);

            obcon = new OdbcConnection(connection);
            DataSet dsItems = new DataSet();
            odbAdp = new OdbcDataAdapter(query, obcon);
            odbAdp.Fill(dsItems);
            return dsItems.Tables[0];
        }
    }
}