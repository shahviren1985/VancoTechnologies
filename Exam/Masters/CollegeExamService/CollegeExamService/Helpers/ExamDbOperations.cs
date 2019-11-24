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
            int random = rand.Next(1, 3) ;
            string columnName = "paper" + random;
            string query = "update papermaster set randomselectedpaper="+ columnName + ", randomgenerationdate='" + DateTime.Now.ToString("yyyy-MM-dd H:mm:ss") + "' WHERE id=" + paperId;
            ExcuteQuery(query);
        }

        public List<ExamPaperDataModel> GetExamPapers(string examYear, string examMonth, string semester, string userName)
        {
            List<ExamPaperDataModel> papers = new List<ExamPaperDataModel>();
            string query = "SELECT id,specialisation,papercode,papertitle,paper1,paper2,paper3,randomselectedpaper,uploadedby,uploadeddate FROM papermaster WHERE examyear=" + examYear + " and exammonth='" + examMonth + "' and semester='" + semester + "' and isdeleted=false group by specialisation order by papertitle asc";

            if (!string.IsNullOrEmpty(userName))
            {
                query = "SELECT id,specialisation,papercode,papertitle,paper1,paper2,paper3,randomselectedpaper,uploadedby,uploadeddate FROM papermaster WHERE examyear=" + examYear + " and exammonth='" + examMonth + "' and semester='" + semester + "' and uploadedby = '" + userName + "' and isdeleted=false group by specialisation order by papertitle asc";
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

        public int InsertMarksdetails(MarksdetailDataModel sd)
        {
            string iq = "INSERT INTO marksdetails(studentid,examid,seatnumber,credit,isgrade,papercode,papertitle,papertype,iselective," +
"internalpassingmarks,internalmarksobtained,externalpassingmarks,internaltotalmarks,externalsection1marks,externalsection2marks," +
"externaltotalmarks,practicalmarksobtained,practicalmaxmarks,gracemarks,paperresult,gp,grade,attempt,remarks,printedcommon4,printedcommon6,externalmaxmarks,RetryCount)" +
"VALUES ('" + sd.StudentId + "','" + sd.ExamId + "','" + sd.SeatNumber + "','" + sd.Credit + "'," + sd.IsGrade + ",'" + sd.PaperCode + "'," +
"'" + sd.PaperTitle + "','" + sd.PaperType + "'," + sd.IsElective + ",'" + sd.InternalPassingMarks + "','" + sd.Internalmarksobtained + "'," +
"'" + sd.ExternalPassingMarks + "','" + sd.InternalTotalMarks + "','" + sd.ExternalSection1Marks + "','" + sd.ExternalSection2Marks + "','" + sd.ExternalTotalMarks + "'," +
"'" + sd.PracticalMarksObtained + "','" + sd.PracticalMaxMarks + "','" + sd.GraceMarks + "','" + sd.PaperResult + "','" + sd.Gp + "','" + sd.Grade + "'," +
"'" + sd.Attempt + "','" + sd.Remarks + "'," + sd.PrintedCommon4 + "," + sd.PrintedCommon6 + ",'" + sd.ExternalMaxMarks + "'," + sd.RetryCount + ");";

            return ExcuteQuery(iq);
        }

        public int UpdateMarksdetails(MarksdetailDataModel sd)
        {
            string iq = "UPDATE marksdetails SET credit = '" + sd.Credit + "',isgrade = " + sd.IsGrade + ",papercode = '" + sd.PaperCode + "'," +
"papertitle ='" + sd.PaperTitle + "',papertype ='" + sd.PaperType + "',iselective = " + sd.IsElective + ",internalpassingmarks = '" + sd.InternalPassingMarks + "'," +
"internalmarksobtained = '" + sd.Internalmarksobtained + "',externalpassingmarks ='" + sd.ExternalPassingMarks + "',internaltotalmarks = '" + sd.InternalTotalMarks + "'," +
"externalsection1marks = '" + sd.ExternalSection1Marks + "',externalsection2marks ='" + sd.ExternalSection2Marks + "',externaltotalmarks = '" + sd.ExternalTotalMarks + "'," +
"practicalmarksobtained = '" + sd.PracticalMarksObtained + "',practicalmaxmarks = '" + sd.PracticalMaxMarks + "',gracemarks = '" + sd.GraceMarks + "'," +
"paperresult = '" + sd.PaperResult + "',gp = '" + sd.Gp + "',grade ='" + sd.Grade + "',attempt = '" + sd.Attempt + "',remarks = '" + sd.Remarks + "'," +
"printedcommon4 = " + sd.PrintedCommon4 + ",printedcommon6 =" + sd.PrintedCommon6 + ",externalmaxmarks='" + sd.ExternalMaxMarks + "',RetryCount=" + sd.RetryCount + " WHERE id = " + sd.Id + ";";

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
                string honours = "%-Honours";
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

                            if (r.Papers.Find(p => p.Semester == 4) != null && r.Specialisation.ToLower() == filteredResponse.Specialisation.ToLower().Replace("&", "&"))
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

        public List<CommonMarksheetResponse> GetTranscriptDataByStudent(string studentId, string sem, string examType, string specialisationCode)
        {
            try
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/" + "/");
                string honours = "%-Honours";
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

    }
}