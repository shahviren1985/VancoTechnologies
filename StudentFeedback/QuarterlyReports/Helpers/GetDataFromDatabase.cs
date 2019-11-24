using QuarterlyReports.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace QuarterlyReports
{
    public class GetDataFromDatabase
    {
        OdbcConnection obcon;
        OdbcDataAdapter odbAdp;
        OdbcCommand odbCommand;
        string getIdQuery = "Select @@Identity";
        int ID;

        public int InsertCommittee(Committee committee)
        {
            string insertCommitteeQuery = "insert into committee(userId,name,incharge) values(" + committee.UserId + ",'" + committee.Name + "','" + committee.Incharge + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertCommitteeQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                odbCommand.CommandText = getIdQuery;
                ID = (int)Convert.ToInt32(odbCommand.ExecuteScalar().ToString());
                obcon.Close();
                return ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertEvent(Event eventData)
        {
            string insertEventQuery = "insert into event(commiteeId,title,eventType,eventDate,venue,objective,numOfParticipants,sponsors,outcome)" +
            "values(" + eventData.CommiteeId + ",'" + eventData.Title + "','" + eventData.EventType + "','" + eventData.EventDate + "','" + eventData.Venue + "','" + eventData.Objective + "'," + eventData.NumOfParticipants + ",'" + eventData.Sponsors + "','" + eventData.Outcome + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertEventQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertDepartmental(Departmental departmental)
        {
            string insertDepartmenalQuery = "insert into departmental(userId,deptName) values(" + departmental.UserId + ",'" + departmental.DeptName + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertDepartmenalQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                odbCommand.CommandText = getIdQuery;
                ID = (int)Convert.ToInt32(odbCommand.ExecuteScalar().ToString());
                obcon.Close();
                return ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertGuestLectureTalk(GuestLectureTalk lecture)
        {
            string insertGuestLectureQuery = "INSERT INTO guestlecturetalk (deptId,title,objective,resourcePerson," +
            "designationAndOrganization,numOfStudent,sponsors,dateOfLecture) VALUES " +
            "(" + lecture.DeptId + ",'" + lecture.Title + "','" + lecture.Objective + "','" + lecture.ResourcePerson + "','" + lecture.DesignationAndOrganization + "'," +
            "" + lecture.NumOfStudent + ",'" + lecture.Sponsors + "','" + lecture.DateOfLecture + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertGuestLectureQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool InsertVisits(Visit visit)
        {
            string insertVisitQuery = "INSERT INTO visits (deptId,nameOfOrganization,place,objective,numOfStudent,sponsoringAgency," +
                "dateOfVisit) VALUES (" + visit.DeptId + ",'" + visit.NameOfOrganization + "','" + visit.Place + "'," +
                "'" + visit.Objective + "'," + visit.NumOfStudent + ",'" + visit.SponsoringAgency + "','" + visit.DateOfVisit + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertVisitQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool InsertSeminar(Seminar seminar)
        {
            string insertSeminar = "INSERT INTO seminars (deptId,title,objective,resourcePerson,designationOfRecourcePerson," +
                "numOfStudent,sponsors,dateOfSeminar) VALUES (" + seminar.DeptId + ",'" + seminar.Title + "','" + seminar.Objective + "'," +
                    "'" + seminar.ResourcePerson + "','" + seminar.DesignationOfRecourcePerson + "'," + seminar.NumOfStudent + "," +
                    "'" + seminar.Sponsors + "','" + seminar.DateOfSeminar + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertSeminar, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertActivities(Activity activity)
        {
            string insertActivity = "INSERT INTO activities (deptId,name,objective,targetGroup,levelOfAcitivity," +
            "numOfStudent,dateOfActivity) VALUES (" + activity.DeptId + ",'" + activity.Name + "','" + activity.Objective + "'," +
            "'" + activity.TargetGroup + "','" + activity.LevelOfAcitivity + "'," + activity.NumOfStudent + ",'" + activity.DateOfActivity + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertActivity, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertCollaborations(Collaboration collaboration)
        {
            string insertCollaborationsQuery = "INSERT INTO collaborations (deptId,bodiesOrDept,cType,objective,place," +
                "cLevel,numOfBeneficiaries,dateOfCollaborations) VALUES (" + collaboration.DeptId + ",'" + collaboration.BodiesOrDept + "'," +
                "'" + collaboration.Type + "','" + collaboration.Objective + "','" + collaboration.Place + "','" + collaboration.Level + "'," +
                "'" + collaboration.NumOfBeneficiaries + "','" + collaboration.DateOfCollaborations + "');";

            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertCollaborationsQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertInHouseCollaborations(InHouseCollaboration collaboration)
        {
            string insertCollaborationsQuery = "INSERT INTO inhousecollaborations (deptId,bodiesOrDept,inHouseType,objective,place," +
                "ihcLevel,numOfBeneficiaries,dateOfCollaborations) VALUES (" + collaboration.DeptId + ",'" + collaboration.BodiesOrDept + "'," +
                "'" + collaboration.Type + "','" + collaboration.Objective + "','" + collaboration.Place + "','" + collaboration.Level + "'," +
                "'" + collaboration.NumOfBeneficiaries + "','" + collaboration.DateOfCollaborations + "');";

            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertCollaborationsQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertDepartmentalSeminar(DepartmentalSeminar departmentalSeminar)
        {
            string insertCollaborationsQuery = "INSERT INTO departmentalseminar (deptId,title,dpsLevel,numOfParticipants," +
                "numOfPapersPresented,numOfExpertsInvited,grantReceived,fundingAgency,publication,dspDate) " +
                "VALUES(" + departmentalSeminar.DeptId + ",'" + departmentalSeminar.Title + "','" + departmentalSeminar.Level + "'," +
            "'" + departmentalSeminar.NumOfParticipants + "','" + departmentalSeminar.NumOfPapersPresented + "','" + departmentalSeminar.NumOfExpertsInvited + "'," +
            "'" + departmentalSeminar.GrantReceived + "','" + departmentalSeminar.FundingAgency + "','" + departmentalSeminar.Publication + "'," +
            "'" + departmentalSeminar.Date + "');";

            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertCollaborationsQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertPeakPerformers(PerfomerLevel perfomerLevel)
        {
            string insertDepartmenalQuery = "insert into pickperformors(deptId,eventId,userId,deptName,eventName,perfomerLevel)" +
                "values(" + perfomerLevel.DeptId + "," + perfomerLevel.EventId + "," + perfomerLevel.UserId + ",'" + perfomerLevel.DeptName + "','" + perfomerLevel.Event + "','" + perfomerLevel.PeakPerformerLevel + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertDepartmenalQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                odbCommand.CommandText = getIdQuery;
                ID = (int)Convert.ToInt32(odbCommand.ExecuteScalar().ToString());
                obcon.Close();
                return ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertPerformerActivities(PerformerActivity performerActivity)
        {
            //if(!string.IsNullOrEmpty(performerActivity.NameOfWinner))
            //    performerActivity.NameOfWinner = performerActivity.NameOfWinner.Replace("'", "''");
            string insertPerformerActivityQuery = "INSERT INTO perfomeractivities (performerid,activityType,activityName,sponsors," +
                "dateOfActivity,nameOfWinner,classOfWinner,prize,nameOfParticipants,classOfParticipants,numOfParticipants) " +
                "VALUES(" + performerActivity.PerformerId + ",'" + performerActivity.ActivityType + "','" + performerActivity.ActivityName + "'," +
            "'" + performerActivity.Sponsors + "','" + performerActivity.Date + "','" + performerActivity.NameOfWinner + "'," +
            "'" + performerActivity.ClassOfWinner + "','" + performerActivity.Prize + "','" + performerActivity.NameOfParticipants + "'," +
            "'" + performerActivity.ClassOfParticipants + "','" + performerActivity.NumOfParticipants + "');";

            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertPerformerActivityQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertIndividual(Individual individual)
        {
            string insertIndividual = "insert into individual(userId,facultyName,designation,deptName)" +
                "values(" + individual.UserId + ",'" + individual.FacultyName + "','" + individual.Designation + "','" + individual.DeptName + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertIndividual, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                odbCommand.CommandText = getIdQuery;
                ID = (int)Convert.ToInt32(odbCommand.ExecuteScalar().ToString());
                obcon.Close();
                return ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertArticlesInJournals(ArticlesInJournal articlesInJournal)
        {
            string insertJournal = "INSERT INTO articlesinjournals (individualId,paperTitle,paperType,paperNumber," +
                "aijLevel,impactFactor,place,publisher,aijdate) " +
                "VALUES(" + articlesInJournal.IndividualId + ",'" + articlesInJournal.PaperTitle + "','" + articlesInJournal.PaperType + "'," +
            "'" + articlesInJournal.PaperNumber + "','" + articlesInJournal.Level + "','" + articlesInJournal.ImpactFactor + "'," +
            "'" + articlesInJournal.Place + "','" + articlesInJournal.Publisher + "','" + articlesInJournal.Date + "');";

            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertJournal, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertProcInConferenceSeminar(ProcInConferenceSeminar procInConferenceSeminar)
        {
            string insertPicConfQuery = "INSERT INTO procinconferenceseminar (individualId,paperTitle,seminarTitle,organizingBody," +
                "picsLevel,iNumber,place,publisher,picsDate) " +
                "VALUES(" + procInConferenceSeminar.IndividualId + ",'" + procInConferenceSeminar.PaperTitle + "','" + procInConferenceSeminar.SeminarTitle + "'," +
            "'" + procInConferenceSeminar.OrganizingBody + "','" + procInConferenceSeminar.Level + "','" + procInConferenceSeminar.INumber + "'," +
            "'" + procInConferenceSeminar.Place + "','" + procInConferenceSeminar.Publisher + "','" + procInConferenceSeminar.Date + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertPicConfQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertChapterInBooks(ChapterInBook chapterInBook)
        {
            string insertPicConfQuery = "INSERT INTO chapterinbooks (individualId,ChapterTitle,bookName,iNumber," +
                "ciblevel,place,publisher,cibdate) " +
                "VALUES(" + chapterInBook.IndividualId + ",'" + chapterInBook.ChapterTitle + "','" + chapterInBook.BookName + "'," +
            "'" + chapterInBook.INumber + "','" + chapterInBook.Level + "','" + chapterInBook.Place + "'," +
            "'" + chapterInBook.Publisher + "','" + chapterInBook.Date + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertPicConfQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertBookEditedWritten(BookEditedWritten bookEditedWritten)
        {
            string insertBookEditedWrittenQuery = "INSERT INTO bookeditedwritten (individualId,nameOfBook,iNumber,bewlevel," +
                "place,publisher,bewdate) " +
                "VALUES(" + bookEditedWritten.IndividualId + ",'" + bookEditedWritten.NameOfBook + "','" + bookEditedWritten.INumber + "'," +
            "'" + bookEditedWritten.Level + "','" + bookEditedWritten.Place + "','" + bookEditedWritten.Publisher + "'," +
            "'" + bookEditedWritten.Date + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertBookEditedWrittenQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertArticle(Article article)
        {
            string insertArticle = "INSERT INTO articles (individualId,articleTitle,newsPaperMagazineTitle,natureOfArticle," +
                "aLevel,place,publisher,aDate) " +
                "VALUES(" + article.IndividualId + ",'" + article.ArticleTitle + "','" + article.NewsPaperMagazineTitle + "'," +
            "'" + article.NatureOfArticle + "','" + article.Level + "','" + article.Place + "'," +
            "'" + article.Publisher + "','" + article.Date + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertArticle, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertPaperPresentations(PaperPresentation paperPresentation)
        {
            string insertpaperPresentation = "INSERT INTO paperpresentations (individualId,paperTitle,seminarTitle,sponsors," +
                "pplevel,ppdate) " +
                "VALUES(" + paperPresentation.IndividualId + ",'" + paperPresentation.PaperTitle + "','" + paperPresentation.SeminarTitle + "'," +
            "'" + paperPresentation.Sponsors + "','" + paperPresentation.Level + "','" + paperPresentation.Date + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertpaperPresentation, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertTrainingProgram(TrainingProgram trainingProgram)
        {
            string inserttrainingProgramQuery = "INSERT INTO trainingprogram (individualId,title,tplevel,attendedOrOrganised," +
                "organizingBody,sponsors) " +
                "VALUES(" + trainingProgram.IndividualId + ",'" + trainingProgram.Title + "','" + trainingProgram.Level + "'," +
            "'" + trainingProgram.AttendedOrOrganised + "','" + trainingProgram.OrganizingBody + "','" + trainingProgram.Sponsors + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(inserttrainingProgramQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertIndividualSeminar(IndividualSeminar seminar)
        {
            string insertSeminarQuery = "INSERT INTO individualseminars (individualId,seminarTitle,slevel,attendedOrOrganised," +
                "organizingBody,sponsors) " +
                "VALUES(" + seminar.IndividualId + ",'" + seminar.SeminarTitle + "','" + seminar.Level + "'," +
            "'" + seminar.AttendedOrOrganised + "','" + seminar.OrganizingBody + "','" + seminar.Sponsors + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertSeminarQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertFacultyDevelopementProgram(FacultyDevelopementProgram facultyDevelopementProgram)
        {
            string insertSeminarQuery = "INSERT INTO facultydevelopementprogram (individualId,programTitle,outcomeOfTraining,organizingBody," +
                "period,appriciation) " +
                "VALUES(" + facultyDevelopementProgram.IndividualId + ",'" + facultyDevelopementProgram.ProgramTitle + "','" + facultyDevelopementProgram.OutcomeOfTraining + "'," +
            "'" + facultyDevelopementProgram.OrganizingBody + "','" + facultyDevelopementProgram.Period + "','" + facultyDevelopementProgram.Appriciation + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertSeminarQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertHonoursAndAwards(HonoursAndAward honoursAndAward)
        {
            string insertHonoursAndAwardsQuery = "INSERT INTO honoursandawards (individualId,memberType,natureOfContribution,organizingBody," +
                "certificates,period) " +
                "VALUES(" + honoursAndAward.IndividualId + ",'" + honoursAndAward.MemberType + "','" + honoursAndAward.NatureOfContribution + "'," +
            "'" + honoursAndAward.OrganizingBody + "','" + honoursAndAward.Certificates + "','" + honoursAndAward.Period + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertHonoursAndAwardsQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertPostGraduateTraining(PostGraduateTraining postGraduateTraining)
        {
            string insertPostGraduateTrainingQuery = "INSERT INTO postgraduatetraining (individualId,title,nameOfCourse,organizingBody," +
                "targetGroup,period) " +
                "VALUES(" + postGraduateTraining.IndividualId + ",'" + postGraduateTraining.Title + "','" + postGraduateTraining.NameOfCourse + "'," +
            "'" + postGraduateTraining.OrganizingBody + "','" + postGraduateTraining.TargetGroup + "','" + postGraduateTraining.Period + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertPostGraduateTrainingQuery, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetUser(User user)
        {
            string getUserQuery = "SELECT * FROM tbluser where lastName='" + user.LastName + "' and mobileNumber='" + user.MobileNumber + "' ";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(getUserQuery, obcon);
            try
            {
                obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
                DataSet dsItems = new DataSet();
                odbAdp = new OdbcDataAdapter(getUserQuery, obcon); // need to add examtype
                odbAdp.Fill(dsItems);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    User userDetails = new User();
                    foreach (DataRow row in dsItems.Tables[0].Rows)
                    {
                        userDetails.UserId = int.Parse(row["userId"].ToString());
                        userDetails.FirstName = row["firsName"].ToString();
                        userDetails.LastName = row["lastName"].ToString();
                        userDetails.MobileNumber = row["mobileNumber"].ToString();
                        userDetails.UserType = row["userType"].ToString();
                        userDetails.CollegeId = row["collegeId"].ToString();
                    }
                    return userDetails;
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertUserDeatils(User user)
        {
            string insertUserQuery = "INSERT INTO tbluser(`firsName`, `lastname`, `mobilenumber`,`userType`,`collegeId`) VALUES ('" + user.FirstName + "', '" + user.LastName + "', '" + user.MobileNumber + "'," +
                "'" + user.UserType + "','" + user.CollegeId + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertUserQuery, obcon);
            try
            {
                obcon.Open();
                int isSuccess= odbCommand.ExecuteNonQuery();
                obcon.Close();
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateUserDeatils(User user)
        {
            string updateUserQuery = "Update tbluser SET firsName ='" + user.FirstName + "',lastname = '" + user.LastName + "',mobilenumber = '" + user.MobileNumber + "'," +
            "userType = '" + user.UserType + "',collegeId = '" + user.CollegeId + "'" +
            "WHERE userid = '" + user.UserId + "';";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(updateUserQuery, obcon);
        
            try
            {
                obcon.Open();
                int isSuccess = odbCommand.ExecuteNonQuery();
                obcon.Close();
                return isSuccess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ExportUsers(string collageCode)
        {
            string getUserQuery = "SELECT * FROM tbluser where collegeId='" + collageCode + "'";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(getUserQuery, obcon);
            DataTable table = null;
            DataSet dsItems = new DataSet();
            string csvString = string.Empty;
            try
            {
                odbAdp = new OdbcDataAdapter(getUserQuery, obcon); // need to add examtype
                odbAdp.Fill(dsItems);
                if (dsItems.Tables != null && dsItems.Tables.Count > 0)
                    table = dsItems.Tables[0];
                if (table != null && table.Rows.Count > 0)
                {
                    csvString = ExportDataTableToCSV(table);
                }
                return csvString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int InsertNaacRequirements(NaacRequirements naacRequirements)
        {
            string insertNaacRequirements = "INSERT INTO naacrequirements(userid,modifieddate) VALUES(" + naacRequirements.UserId + ",'" + naacRequirements.ModifiedDate.ToString("yyyy-MM-ddTHH:mm:ss") + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertNaacRequirements, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                odbCommand.CommandText = getIdQuery;
                ID = (int)Convert.ToInt32(odbCommand.ExecuteScalar().ToString());
                obcon.Close();
                return ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertEditedBookDetail(EditedBookDetail editedBookDetail)
        {
            string insertEditedBookDetail = "INSERT INTO editedbookdetails(naacrequirementsid,teachername,papertitle," +
                "booktitle,authorname,conferencetitle,publishername,publishertype,isbnorissnnumber,affiliatinginstitutes," +
            "publicationyear) VALUES ('" + editedBookDetail.NaacRequirementsId + "','" + editedBookDetail.TeacherName + "','" + editedBookDetail.PaperTitle + "'," +
"'" + editedBookDetail.BookTitle + "','" + editedBookDetail.AuthorName + "','" + editedBookDetail.ConferenceTitle + "','" + editedBookDetail.PublisherName + "'," +
"'" + editedBookDetail.PublisherType + "','" + editedBookDetail.IsbnOrIssnNumber + "','" + editedBookDetail.AffiliatingInstitutes + "','" + editedBookDetail.PublicationYear + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertEditedBookDetail, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertMouDetail(MouDetail mouDetail)
        {
            string insertMouDetails = "INSERT INTO moudetails(naacrequirementsid,organisationmousigned,institutename,yearofsign," +
"duration,listofactivities,numberofstudents,numberofteachers) VALUES ('" + mouDetail.NaacRequirementsId + "','" + mouDetail.OrganisationMOUSigned + "'," +
"'" + mouDetail.InstituteName + "','" + mouDetail.YearOfSign + "','" + mouDetail.Duration + "','" + mouDetail.ListOfActivities + "','" + mouDetail.NumberOfStudents + "','" + mouDetail.NumberOfTeachers + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertMouDetails, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertActivityDetail(ActivityDetail activityDetail)
        {
            string insertActivityDetail = "INSERT INTO activitydetails(naacrequirementsid,activityname,schemename,activityyear," +
                "numberofteacher,numberofstudents) VALUES ('" + activityDetail.NaacRequirementsId + "','" + activityDetail.ActivityName + "','" + activityDetail.SchemeName + "'," +
"'" + activityDetail.ActivityYear + "','" + activityDetail.NumberOfTeacher + "','" + activityDetail.NumberOfStudents + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertActivityDetail, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertExtensionOutReachProgramDetail(ExtensionOutReachProgramDetail extensionOutReachProgramDetail)
        {
            string insertExtensionOutReachProgramDetail = "INSERT INTO extensionoutreachprogramdetails(naacrequirementsid,extensionname,extensioncount," +
                "collaboratingagencyname,agencytype,contactnumber,address,activityyear) VALUES ('" + extensionOutReachProgramDetail.NaacRequirementsId + "'," +
                "'" + extensionOutReachProgramDetail.ExtensionName + "','" + extensionOutReachProgramDetail.ExtensionCount + "'," +
"'" + extensionOutReachProgramDetail.CollaboratingAgencyName + "','" + extensionOutReachProgramDetail.AgencyType + "','" + extensionOutReachProgramDetail.ContactNumber + "'," +
"'" + extensionOutReachProgramDetail.Address + "','" + extensionOutReachProgramDetail.ActivityYear + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertExtensionOutReachProgramDetail, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertLinkageDetail(LinkageDetail linkageDetail)
        {
            string insertLinkageDetail = "INSERT INTO linkagedetails(naacrequirementsid,linkagetitle,organizationname," +
"details,commencementyear,durationfrom,durationto,natureoflinkage) VALUES ('" + linkageDetail.NaacRequirementsId + "'," +
"'" + linkageDetail.LinkageTitle + "','" + linkageDetail.OrganizationName + "','" + linkageDetail.Details + "'," +
"'" + linkageDetail.CommencementYear + "','" + linkageDetail.DurationFrom + "','" + linkageDetail.DurationTo + "','" + linkageDetail.NatureOfLinkage + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertLinkageDetail, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertDevelopmentSchemeDetail(DevelopmentSchemeDetail developmentSchemeDetail)
        {
            string insertDevelopmentSchemeDetail = "INSERT INTO developmentschemedetails(naacrequirementsid,schemeoption," +
"schemecount,schemename,implementationyear,numberofstudents,agencyname,contactnumber,agencyaddress) VALUES ('" + developmentSchemeDetail.NaacRequirementsId + "'," +
"'" + developmentSchemeDetail.SchemeOption + "','" + developmentSchemeDetail.SchemeCount + "','" + developmentSchemeDetail.SchemeName + "'," +
"'" + developmentSchemeDetail.ImplementationYear + "','" + developmentSchemeDetail.NumberOfStudents + "','" + developmentSchemeDetail.AgencyName + "'," +
"'" + developmentSchemeDetail.ContactNumber + "','" + developmentSchemeDetail.AgencyAddress + "');";
            obcon = new OdbcConnection(ConfigurationManager.AppSettings.Get("mySqlConn").ToString());
            odbCommand = new OdbcCommand(insertDevelopmentSchemeDetail, obcon);
            try
            {
                obcon.Open();
                odbCommand.ExecuteNonQuery();
                obcon.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ExportDataTableToCSV(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append(dt.Columns[i].ColumnName + ',');
            }
            sb.Append(Environment.NewLine);

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    sb.Append(dt.Rows[j][k].ToString().Replace(",", " ") + ',');
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }

    }
}