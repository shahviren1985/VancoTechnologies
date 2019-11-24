using QuarterlyReports.Helpers;
using QuarterlyReports.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;

namespace QuarterlyReports.Controllers
{
    public class ReportController : ApiController
    {
        ReportDataDBOperations db;
        public ReportController()
        {
            db = new ReportDataDBOperations();
        }

        [HttpGet]
        public HttpResponseMessage GenerateFeedbackReport(QuarterlyReports.Models.ReporIdEnum reportId, int userId)
        {
            FileStream fileStream = null;
            try
            {
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/UserData/ReportData/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                DataTable dt = null;
                HttpResponseMessage response = new HttpResponseMessage();
                bool isTrue = false;
                switch (reportId)
                {
                    case ReporIdEnum.R1:
                        fileName = "Committee_Event" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetCommiteeData(userId);
                        break;
                    case ReporIdEnum.R2:
                        fileName = "Departmental_Guest_Lectures_Talks" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetDepartmentalGuestLecturesTalks(userId);
                        break;
                    case ReporIdEnum.R3:
                        fileName = "Departmental_Visits" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetDepartmentalVisits(userId);
                        break;
                    case ReporIdEnum.R4:
                        fileName = "Departmental_Seminars" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetDepartmentalSeminar(userId);
                        break;
                    case ReporIdEnum.R5:
                        fileName = "Departmental_Activities" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetDepartmentalActivities(userId);
                        break;
                    case ReporIdEnum.R6:
                        fileName = "Departmental_Collaborations" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetDepartmentalCollaborations(userId);
                        break;
                    case ReporIdEnum.R7:
                        fileName = "Departmental_InHouseCollaborations" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetDepartmentalInHouseCollaborations(userId);
                        break;
                    case ReporIdEnum.R8:
                        fileName = "Departmental_ConferencesSeminars" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetDepartmentalConferences(userId);
                        break;
                    case ReporIdEnum.R9:
                        fileName = "Individual_ArticlesInJournals" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetIndividualArticlesInJournals(userId);
                        break;
                    case ReporIdEnum.R10:
                        fileName = "Individual_ProcInConferenceSeminar" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetIndividualProcInConferenceSeminar(userId);
                        break;
                    case ReporIdEnum.R11:
                        fileName = "Individual_ChapterInBook" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetIndividualChapterInBooks(userId);
                        break;
                    case ReporIdEnum.R12:
                        fileName = "Individual_BookEditedWritten" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetIndividualBookEditedWritten(userId);
                        break;
                    case ReporIdEnum.R13:
                        fileName = "Individual_Articles" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetIndividualArticles(userId);
                        break;
                    case ReporIdEnum.R14:
                        fileName = "Individual_PaperPresentation" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetIndividualPaperPresentation(userId);
                        break;
                    case ReporIdEnum.R15:
                        fileName = "Individual_TrainingProgram" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetIndividualTrainingProgram(userId);
                        break;
                    case ReporIdEnum.R16:
                        fileName = "Individual_SeminarsConferenceAttended" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetIndividualSeminarsConferenceAttended(userId);
                        break;
                    case ReporIdEnum.R17:
                        fileName = "Individual_FacultyDevelopementProgram" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetIndividualFacultyDevelopementProgram(userId);
                        break;
                    case ReporIdEnum.R18:
                        fileName = "Individual_PostGraduateTraining" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetIndividualPostGraduateTraining(userId);
                        break;
                    case ReporIdEnum.R19:
                        fileName = "Individual_HonoursAndAward" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetIndividualHonoursAndAward(userId);
                        break;
                    case ReporIdEnum.R20:
                        fileName = "Pickperformors_WinnersAtUperLevel" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetPickperformorsWinnersAtUperLevel(userId);
                        break;
                    case ReporIdEnum.R21:
                        fileName = "Pickperformors_WinnersInterCollegiateLevel" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetPickperformorsWinnersInterCollegiateLevel(userId);
                        break;
                    case ReporIdEnum.R22:
                        fileName = "Pickperformors_ParticipationAtUperLevel" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetPickperformorsParticipationAtUperLevel(userId);
                        break;
                    case ReporIdEnum.R23:
                        fileName = "Pickperformors_ParticipationAtCollegiateLevel" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetPickperformorsParticipationAtIntercollegiate(userId);
                        break;
                    case ReporIdEnum.R24:
                        fileName = "Pickperformors_PrizeWinnersAtCollegeLevel" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetPickperformorsPrizeWinnersAtCollegeLevel(userId);
                        break;
                    case ReporIdEnum.R25:
                        fileName = "NaacRequirements_EditedBookDetails" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetNaacRequirementsEditedBookDetails(userId);
                        break;
                    case ReporIdEnum.R26:
                        fileName = "NaacRequirements_ExtensionOutReachProgramDetails" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetNaacRequirementsExtensionOutReachProgramDetails(userId);
                        break;
                    case ReporIdEnum.R27:
                        fileName = "NaacRequirements_ActivityDetails" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetNaacRequirementsActivityDetails(userId);
                        break;
                    case ReporIdEnum.R28:
                        fileName = "NaacRequirements_LinkageDetails" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetNaacRequirementsLinkageDetails(userId);
                        break;
                    case ReporIdEnum.R29:
                        fileName = "NaacRequirements_MOUDetails" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetNaacRequirementsMOUDetails(userId);
                        break;
                    case ReporIdEnum.R30:
                        fileName = "NaacRequirements_DevelopmentSchemeDetails" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xslx";
                        fullPath = folderPath + fileName;
                        dt = db.GetNaacRequirementsDevelopmentSchemeDetails(userId);
                        break;
                    default:
                        break;
                }
                if (dt != null)
                {
                    OpenXmlHelper helper = new OpenXmlHelper();
                    isTrue = helper.ExportDataSet(dt, fullPath);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Datatable is empty");
                }
                if (isTrue)
                {
                    fileStream = File.Open(fullPath,FileMode.Open);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StreamContent(fileStream);
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = fileName;
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
                    response.Content.Headers.ContentLength = fileStream.Length;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Issue with the excel file generation");
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /*
        public HttpResponseMessage GetExcelFileTest(DataTable dt)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            //Create the file in Web App Physical Folder
            string fileName = Guid.NewGuid().ToString() + ".xls";
            string filePath = HttpContext.Current.Server.MapPath(String.Format("~/{0}", fileName));

            StringBuilder fileContent = new StringBuilder();
            //Get Data here
            if (dt != null)
            {
                string str = string.Empty;
                foreach (DataColumn dtcol in dt.Columns)
                {
                    fileContent.Append(str + dtcol.ColumnName);
                    str = "\t";
                }
                fileContent.Append("\n");
                foreach (DataRow dr in dt.Rows)
                {
                    str = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        fileContent.Append(str + Convert.ToString(dr[j]));
                        str = "\t";
                    }
                    fileContent.Append("\n");
                }
            }
            // write the data into Excel file

            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.Write(fileContent.ToString());
            }
            //Get the File Stream
            FileStream fileStream = File.OpenRead(filePath);
            //Set response
            response.Content = new StreamContent(fileStream);
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = fileName;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
            response.Content.Headers.ContentLength = fileStream.Length;
            //Delete the file

            //if(File.Exists(filePath))
            //{
            //    File.Delete(filePath);
            //}
            return response;
        }
         */
    }
}
