using QuarterlyReports.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Web;

namespace QuarterlyReports
{
    public class ReportDataDBOperations
    {
        OdbcConnection obcon;
        OdbcDataAdapter odbAdp;
        OdbcCommand odbCommand;
        string getIdQuery = "Select @@Identity";
        int ID;

        public OdbcConnection CreateConnection()
        {
            string connection = ConfigurationManager.AppSettings.Get("mySqlConn").ToString();
            return new OdbcConnection(connection);
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

        public DataTable GetCommiteeData(int userId)
        {
            string getComitteeQuery = "SELECT * FROM committee where userId='" + userId + "'";
            DataTable ndt = new DataTable("EventData");
            ndt.Columns.Add("Commitee Name");
            ndt.Columns.Add("Incharge");
            ndt.Columns.Add("Title");
            ndt.Columns.Add("Type of Event");
            ndt.Columns.Add("Event Date");
            ndt.Columns.Add("Venue");
            ndt.Columns.Add("Objective");
            ndt.Columns.Add("No. of participants");
            ndt.Columns.Add("Sponsors/Collaborators");
            ndt.Columns.Add("Outcome");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsEvents = new DataSet();

                dsItems = GetQuery(getComitteeQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Committee> listOfCommittee = dt.ToList<Committee>();
                    foreach (Committee c in listOfCommittee)
                    {
                        string getEventsQuery = "Select * from event where commiteeId='" + c.CommiteeId + "'";
                        dsEvents = GetQuery(getEventsQuery);
                        if (dsEvents.Tables[0].Rows != null && dsEvents.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtEvents = dsEvents.Tables[0];
                            List<Event> listOfEvents = dtEvents.ToList<Event>();

                            foreach (Event e in listOfEvents)
                            {
                                DataRow r = ndt.NewRow();
                                r["Commitee Name"] = c.Name;
                                r["Incharge"] = c.Incharge;
                                r["Title"] = e.Title;
                                r["Type of Event"] = e.EventType;
                                r["Event Date"] = e.EventDate;
                                r["Venue"] = e.Venue;
                                r["Objective"] = e.Objective;
                                r["No. of participants"] = e.NumOfParticipants;
                                r["Sponsors/Collaborators"] = e.Sponsors;
                                r["Outcome"] = e.Outcome;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDepartmentalGuestLecturesTalks(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM departmental where userId='" + userId + "'";
            DataTable ndt = new DataTable("GetDepartmentalGuestLecturesTalks");
            ndt.Columns.Add("Name of the Department");
            ndt.Columns.Add("Title of the Lecture");
            ndt.Columns.Add("Objective of the Activity");
            ndt.Columns.Add("Name of the  Resource Person");
            ndt.Columns.Add("Designation and Organization");
            ndt.Columns.Add("Target group & No. Of Students");
            ndt.Columns.Add("Sponsors,  if any");
            ndt.Columns.Add("Date");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Departmental> listOfDepartmental = dt.ToList<Departmental>();
                    foreach (Departmental c in listOfDepartmental)
                    {
                        string getGLTQuery = "Select * from guestlecturetalk where deptId='" + c.DeptId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<GuestLectureTalk> listOfGTL = dtGTL.ToList<GuestLectureTalk>();

                            foreach (GuestLectureTalk e in listOfGTL)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name of the Department"] = c.DeptName;
                                r["Title of the Lecture"] = e.Title;
                                r["Objective of the Activity"] = e.Objective;
                                r["Name of the  Resource Person"] = e.ResourcePerson;
                                r["Designation and Organization"] = e.DesignationAndOrganization;
                                r["Target group & No. Of Students"] = e.NumOfStudent;
                                r["Sponsors,  if any"] = e.Sponsors;
                                r["Date"] = e.DateOfLecture;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDepartmentalVisits(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM departmental where userId='" + userId + "'";
            DataTable ndt = new DataTable("GetDepartmentalVisits");
            ndt.Columns.Add("Name of the Department");
            ndt.Columns.Add("Name of Industry/ Organization");
            ndt.Columns.Add("Place");
            ndt.Columns.Add("Objectives");
            ndt.Columns.Add("Target  Group & No. Of Students");
            ndt.Columns.Add("Sponsoring Agency, if any");
            ndt.Columns.Add("Sponsors,  if any");
            ndt.Columns.Add("Date");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Departmental> listOfDepartmental = dt.ToList<Departmental>();
                    foreach (Departmental c in listOfDepartmental)
                    {
                        string getGLTQuery = "Select * from visits where deptId='" + c.DeptId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<Visit> listOfGTL = dtGTL.ToList<Visit>();

                            foreach (Visit e in listOfGTL)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name of the Department"]=c.DeptName;
                                r["Name of Industry/ Organization"]=e.NameOfOrganization;
                                r["Place"]=e.Place;
                                r["Objectives"]=e.Objective;
                                r["Target  Group & No. Of Students"]=e.NumOfStudent;
                                r["Sponsoring Agency, if any"]=e.SponsoringAgency;
                                r["Sponsors,  if any"]=e.SponsoringAgency;
                                r["Date"]=e.DateOfVisit;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDepartmentalSeminar(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM departmental where userId='" + userId + "'";
            DataTable ndt = new DataTable("DepartmentalSeminar");
            ndt.Columns.Add("Name of the Department");
            ndt.Columns.Add("Title");
            ndt.Columns.Add("Objectives");
            ndt.Columns.Add("Resource  Person");
            ndt.Columns.Add("Designation of Resource Person");
            ndt.Columns.Add("Sponsors, if any");
            ndt.Columns.Add("Target Group &No. Of Students");
            ndt.Columns.Add("Date");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Departmental> listOfDepartmental = dt.ToList<Departmental>();
                    foreach (Departmental c in listOfDepartmental)
                    {
                        string getGLTQuery = "Select * from seminars where deptId='" + c.DeptId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<Seminar> listOfGTL = dtGTL.ToList<Seminar>();

                            foreach (Seminar e in listOfGTL)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name of the Department"]=c.DeptName;
                                r["Title"]=e.Title;
                                r["Objectives"]=e.Objective;
                                r["Resource  Person"]=e.ResourcePerson;
                                r["Designation of Resource Person"]=e.DesignationOfRecourcePerson;
                                r["Sponsors, if any"]=e.Sponsors;
                                r["Target Group &No. Of Students"]=e.NumOfStudent;
                                r["Date"]=e.DateOfSeminar;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDepartmentalActivities(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM departmental where userId='" + userId + "'";
            DataTable ndt = new DataTable("DepartmentalActivities");
            ndt.Columns.Add("Name of the Department");
            ndt.Columns.Add("Name of the Activity");
            ndt.Columns.Add("Objectives");
            ndt.Columns.Add("Target group");
            ndt.Columns.Add("College Level/ Inter college/university level");
            ndt.Columns.Add("Target Group &No. Of Students");
            ndt.Columns.Add("Date");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Departmental> listOfDepartmental = dt.ToList<Departmental>();
                    foreach (Departmental c in listOfDepartmental)
                    {
                        string getGLTQuery = "Select * from activities where deptId='" + c.DeptId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<Activity> listOfGTL = dtGTL.ToList<Activity>();

                            foreach (Activity e in listOfGTL)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name of the Department"]=c.DeptName;
                                r["Name of the Activity"]=e.Name;
                                r["Objectives"]=e.Objective;
                                r["Target group"]=e.TargetGroup;
                                r["College Level/ Inter college/university level"]=e.LevelOfAcitivity;
                                r["Target Group &No. Of Students"]=e.NumOfStudent;
                                r["Date"]=e.DateOfActivity;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDepartmentalCollaborations(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM departmental where userId='" + userId + "'";
            DataTable ndt = new DataTable("DepartmentalCollaborations");
            ndt.Columns.Add("Name of the Department");
            ndt.Columns.Add("Collaborating Bodies/ Departments");
            ndt.Columns.Add("Type of Activity/Event");
            ndt.Columns.Add("Objective");
            ndt.Columns.Add("Place");
            ndt.Columns.Add("National/state/International/University/industry/NGO/ Research Bodies");
            ndt.Columns.Add("Target Group & No. of  beneficiaries");
            ndt.Columns.Add("Date");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Departmental> listOfDepartmental = dt.ToList<Departmental>();
                    foreach (Departmental c in listOfDepartmental)
                    {
                        string getGLTQuery = "Select * from collaborations where deptId='" + c.DeptId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<Collaboration> listOfGTL = dtGTL.ToList<Collaboration>();
                            foreach (Collaboration e in listOfGTL)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name of the Department"]=c.DeptName;
                                r["Collaborating Bodies/ Departments"]=e.BodiesOrDept;
                                r["Type of Activity/Event"]=e.Type;
                                r["Objective"]=e.Objective;
                                r["Place"]=e.Place;
                                r["National/state/International/University/industry/NGO/ Research Bodies"]=e.Level;
                                r["Target Group & No. of  beneficiaries"]=e.NumOfBeneficiaries;
                                r["Date"]=e.DateOfCollaborations;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDepartmentalInHouseCollaborations(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM departmental where userId='" + userId + "'";
            DataTable ndt = new DataTable("DepartmentalInHouseCollaborations");
            ndt.Columns.Add("Name of the Department");
            ndt.Columns.Add("Collaborating Bodies/Departments");
            ndt.Columns.Add("Type of Activity/Event");
            ndt.Columns.Add("Objective");
            ndt.Columns.Add("Place");
            ndt.Columns.Add("Target Group & No. of  beneficiaries");
            ndt.Columns.Add("Date");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Departmental> listOfDepartmental = dt.ToList<Departmental>();
                    foreach (Departmental c in listOfDepartmental)
                    {
                        string getGLTQuery = "Select * from inhousecollaborations where deptId='" + c.DeptId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<InHouseCollaboration> listOfGTL = dtGTL.ToList<InHouseCollaboration>();
                            foreach (InHouseCollaboration e in listOfGTL)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name of the Department"]=c.DeptName;
                                r["Collaborating Bodies/Departments"]=e.BodiesOrDept;
                                r["Type of Activity/Event"]=e.Type;
                                r["Objective"]=e.Objective;
                                r["Place"]=e.Place;
                                r["Target Group & No. of  beneficiaries"]=e.NumOfBeneficiaries;
                                r["Date"]=e.DateOfCollaborations;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetDepartmentalConferences(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM departmental where userId='" + userId + "'";
            DataTable ndt = new DataTable("DepartmentalConferences");
            ndt.Columns.Add("Name of the Department");
            ndt.Columns.Add("Title of Conference");
            ndt.Columns.Add("International / National/ State");
            ndt.Columns.Add("No. of Participants");
            ndt.Columns.Add("No. of Papers presented");
            ndt.Columns.Add("No. of experts invited");
            ndt.Columns.Add("Grant received");
            ndt.Columns.Add("Funding Agency");
            ndt.Columns.Add("Conference Publications ISSN/ISBN");
            ndt.Columns.Add("Date");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Departmental> listOfDepartmental = dt.ToList<Departmental>();
                    foreach (Departmental c in listOfDepartmental)
                    {
                        string getGLTQuery = "Select * from departmentalseminar where deptId='" + c.DeptId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<DepartmentalSeminar> listOfGTL = dtGTL.ToList<DepartmentalSeminar>();
                            foreach (DepartmentalSeminar e in listOfGTL)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name of the Department"]=c.DeptName;
                                r["Title of Conference"]=e.Title;
                                r["International / National/ State"]=e.Level;
                                r["No. of Participants"]=e.NumOfParticipants;
                                r["No. of Papers presented"]=e.NumOfPapersPresented;
                                r["No. of experts invited"]=e.NumOfExpertsInvited;
                                r["Grant received"]=e.GrantReceived;
                                r["Funding Agency"]=e.FundingAgency;
                                r["Conference Publications ISSN/ISBN"]=e.Publication;
                                r["Date"]=e.Date;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetIndividualArticlesInJournals(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM individual where userId='" + userId + "'";
            DataTable ndt = new DataTable("DepartmentalConferences");
            ndt.Columns.Add("Name Of The Faculty");
            ndt.Columns.Add("Designation");
            ndt.Columns.Add("Name Of The Department");
            ndt.Columns.Add("Title Of The Paper");
            ndt.Columns.Add("Peer Reviewed/Referred/Non Referred");
            ndt.Columns.Add("ISSN/ISBN No Volume, Issue No & PG.No");
            ndt.Columns.Add("State/National Iternational");
            ndt.Columns.Add("Impact Factor & International Data Base");
            ndt.Columns.Add("Place & Publisher /Date");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Individual> listOfItems = dt.ToList<Individual>();
                    foreach (Individual c in listOfItems)
                    {
                        string getGLTQuery = "Select *,aijLevel as level from articlesinjournals where individualId='" + c.IndividualId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<ArticlesInJournal> listOfItems2 = dtGTL.ToList<ArticlesInJournal>();
                            foreach (ArticlesInJournal e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name Of The Faculty"]=c.FacultyName;
                                r["Designation"]=c.Designation;
                                r["Name Of The Department"]=c.DeptName;
                                r["Publications"]=c.Publications;
                                r["Title Of The Paper"]=e.PaperTitle;
                                r["Peer Reviewed/Referred/Non Referred"]=e.PaperType;
                                r["ISSN/ISBN No Volume, Issue No & PG.No"]=e.PaperNumber;
                                r["State/National Iternational"]=e.Level;
                                r["Impact Factor & International Data Base"]=e.ImpactFactor;
                                r["Place & Publisher /Date"]=e.Place + ", " +e.Publisher+ ", "+ e.Date;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetIndividualProcInConferenceSeminar(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM individual where userId='" + userId + "'";
            DataTable ndt = new DataTable("DepartmentalConferences");
            ndt.Columns.Add("Name Of The Faculty");
            ndt.Columns.Add("Designation");
            ndt.Columns.Add("Name Of The Department");
            ndt.Columns.Add("Title Of The Paper");
            ndt.Columns.Add("Title Of Seminar,Conference");
            ndt.Columns.Add("Organizing Body");
            ndt.Columns.Add("State/National/International");
            ndt.Columns.Add("ISSN/ISBN");
            ndt.Columns.Add("Place & Publisher /Date");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Individual> listOfItems = dt.ToList<Individual>();
                    foreach (Individual c in listOfItems)
                    {
                        string getGLTQuery = "Select *,picsLevel as level from procinconferenceseminar where individualId='" + c.IndividualId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<ProcInConferenceSeminar> listOfItems2 = dtGTL.ToList<ProcInConferenceSeminar>();
                            foreach (ProcInConferenceSeminar e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name Of The Faculty"] = c.FacultyName;
                                r["Designation"] = c.Designation;
                                r["Name Of The Department"] = c.DeptName;
                                r["Publications"] = c.Publications;
                                r["Title Of The Paper"]=e.PaperTitle;
                                r["Title Of Seminar,Conference"]=e.SeminarTitle;
                                r["Organizing Body"]=e.OrganizingBody;
                                r["State/National/International"]=e.Level;
                                r["ISSN/ISBN"]=e.INumber;
                                r["Place & Publisher /Date"] = e.Place + ", " + e.Publisher + ", " + e.Date; 
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetIndividualChapterInBooks(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM individual where userId='" + userId + "'";
            DataTable ndt = new DataTable("DepartmentalConferences");
            ndt.Columns.Add("Name Of The Faculty");
            ndt.Columns.Add("Designation");
            ndt.Columns.Add("Name Of The Department");
            ndt.Columns.Add("Title Of The Chapter");
            ndt.Columns.Add("Name Of the book");
            ndt.Columns.Add("ISSN/ISBN No PG.No");
            ndt.Columns.Add("State/National/International");
            ndt.Columns.Add("Place & Publisher /Date");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Individual> listOfItems = dt.ToList<Individual>();
                    foreach (Individual c in listOfItems)
                    {
                        string getGLTQuery = "Select *,ciblevel as level from chapterinbooks where individualId='" + c.IndividualId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<ChapterInBook> listOfItems2 = dtGTL.ToList<ChapterInBook>();
                            foreach (ChapterInBook e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name Of The Faculty"]=c.FacultyName;
                                r["Designation"]=c.Designation;
                                r["Name Of The Department"]=c.DeptName;
                                r["Title Of The Chapter"]=e.ChapterTitle;
                                r["Name Of the book"]=e.BookName;
                                r["ISSN/ISBN No PG.No"]=e.INumber;
                                r["State/National/International"]=e.Level;
                                r["Place & Publisher /Date"] = e.Place + ", " + e.Publisher + ", " + e.Date;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetIndividualBookEditedWritten(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM individual where userId='" + userId + "'";
            DataTable ndt = new DataTable("DepartmentalConferences");
            ndt.Columns.Add("Name Of The Faculty");
            ndt.Columns.Add("Designation");
            ndt.Columns.Add("Name Of The Department");
            ndt.Columns.Add("Name Of the book");
            ndt.Columns.Add("ISSN/ISBN No Volume,Issue No & PG.No");
            ndt.Columns.Add("State/National/International");
            ndt.Columns.Add("Place & Publisher /Date");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Individual> listOfItems = dt.ToList<Individual>();
                    foreach (Individual c in listOfItems)
                    {
                        string getGLTQuery = "Select *,bewlevel as level from bookeditedwritten where individualId='" + c.IndividualId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<BookEditedWritten> listOfItems2 = dtGTL.ToList<BookEditedWritten>();
                            foreach (BookEditedWritten e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name Of The Faculty"] = c.FacultyName;
                                r["Designation"] = c.Designation;
                                r["Name Of The Department"] = c.DeptName;
                                r["Name Of the book"] = e.NameOfBook;
                                r["ISSN/ISBN No Volume,Issue No & PG.No"] = e.INumber;
                                r["State/National/International"] = e.Level;
                                r["Place & Publisher /Date"] = e.Place + ", " + e.Publisher + ", " + e.Date;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetIndividualArticles(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM individual where userId='" + userId + "'";
            DataTable ndt = new DataTable("IndividualArticles");
            ndt.Columns.Add("Name Of The Faculty");
            ndt.Columns.Add("Designation");
            ndt.Columns.Add("Name Of The Department");
            ndt.Columns.Add("Title Of Article");
            ndt.Columns.Add("Title Of The Newspaper, Magazine, etc");
            ndt.Columns.Add("Nature Of The Article");
            ndt.Columns.Add("State/National International");
            ndt.Columns.Add("Place & Publisher /Date");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Individual> listOfItems = dt.ToList<Individual>();
                    foreach (Individual c in listOfItems)
                    {
                        string getGLTQuery = "Select *,aLevel as level from articles where individualId='" + c.IndividualId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<Article> listOfItems2 = dtGTL.ToList<Article>();
                            foreach (Article e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name Of The Faculty"] = c.FacultyName;
                                r["Designation"] = c.Designation;
                                r["Name Of The Department"] = c.DeptName;
                                r["Title Of Article"]=e.ArticleTitle;
                                r["Title Of The Newspaper, Magazine, etc"]=e.NewsPaperMagazineTitle;
                                r["Nature Of The Article"]=e.NatureOfArticle;
                                r["State/National International"]=e.Level;
                                r["Place & Publisher /Date"] = e.Place + ", " + e.Publisher + ", " + e.Date;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetIndividualPaperPresentation(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM individual where userId='" + userId + "'";
            DataTable ndt = new DataTable("IndividualArticles");
            ndt.Columns.Add("Name Of The Faculty");
            ndt.Columns.Add("Designation");
            ndt.Columns.Add("Name Of The Department");
            ndt.Columns.Add("Title Of the Paper");
            ndt.Columns.Add("Title of Seminar / Conference");
            ndt.Columns.Add("Org. Body/ sponsors/ Collaborators");
            ndt.Columns.Add("State/National International");
            ndt.Columns.Add("Date");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Individual> listOfItems = dt.ToList<Individual>();
                    foreach (Individual c in listOfItems)
                    {
                        string getGLTQuery = "Select *,pplevel as level from paperpresentations where individualId='" + c.IndividualId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<PaperPresentation> listOfItems2 = dtGTL.ToList<PaperPresentation>();
                            foreach (PaperPresentation e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name Of The Faculty"] = c.FacultyName;
                                r["Designation"] = c.Designation;
                                r["Name Of The Department"] = c.DeptName;
                                r["Title Of the Paper"]=e.PaperTitle;
                                r["Title of Seminar / Conference"]=e.SeminarTitle;
                                r["Org. Body/ sponsors/ Collaborators"]=e.Sponsors;
                                r["State/National International"]=e.Level;
                                r["Date"]=e.Date;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetIndividualTrainingProgram(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM individual where userId='" + userId + "'";
            DataTable ndt = new DataTable("IndividualArticles");
            ndt.Columns.Add("Name Of The Faculty");
            ndt.Columns.Add("Designation");
            ndt.Columns.Add("Name Of The Department");
            ndt.Columns.Add("Title Of The Training Program");
            ndt.Columns.Add("State/National International");
            ndt.Columns.Add("Attended/ organised");
            ndt.Columns.Add("Org. Body");
            ndt.Columns.Add("Sponsor/ Collaborator");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Individual> listOfItems = dt.ToList<Individual>();
                    foreach (Individual c in listOfItems)
                    {
                        string getGLTQuery = "Select *,tplevel as level from trainingprogram where individualId='" + c.IndividualId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<TrainingProgram> listOfItems2 = dtGTL.ToList<TrainingProgram>();
                            foreach (TrainingProgram e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name Of The Faculty"] = c.FacultyName;
                                r["Designation"] = c.Designation;
                                r["Name Of The Department"] = c.DeptName;
                                r["Title Of The Training Program"]=e.Title;
                                r["State/National International"]=e.Level;
                                r["Attended/ organised"]=e.AttendedOrOrganised;
                                r["Org. Body"]=e.OrganizingBody;
                                r["Sponsor/ Collaborator"]=e.Sponsors;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetIndividualSeminarsConferenceAttended(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM individual where userId='" + userId + "'";
            DataTable ndt = new DataTable("IndividualArticles");
            ndt.Columns.Add("Name Of The Faculty");
            ndt.Columns.Add("Designation");
            ndt.Columns.Add("Name Of The Department");
            ndt.Columns.Add("Title Of The Seminar/ Conference");
            ndt.Columns.Add("State/National International");
            ndt.Columns.Add("Attended/ organised");
            ndt.Columns.Add("Org. Body");
            ndt.Columns.Add("Sponsor/ Collaborator");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Individual> listOfItems = dt.ToList<Individual>();
                    foreach (Individual c in listOfItems)
                    {
                        string getGLTQuery = "Select *,slevel as level from individualseminars where individualId='" + c.IndividualId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<IndividualSeminar> listOfItems2 = dtGTL.ToList<IndividualSeminar>();
                            foreach (IndividualSeminar e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name Of The Faculty"] = c.FacultyName;
                                r["Designation"] = c.Designation;
                                r["Name Of The Department"] = c.DeptName;
                                r["Title Of The Seminar/ Conference"] = e.SeminarTitle;
                                r["State/National International"] = e.Level;
                                r["Attended/ organised"] = e.AttendedOrOrganised;
                                r["Org. Body"] = e.OrganizingBody;
                                r["Sponsor/ Collaborator"] = e.Sponsors;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetIndividualFacultyDevelopementProgram(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM individual where userId='" + userId + "'";
            DataTable ndt = new DataTable("FacultyDevelopementProgram");
            ndt.Columns.Add("Name Of The Faculty");
            ndt.Columns.Add("Designation");
            ndt.Columns.Add("Name Of The Department");
            ndt.Columns.Add("Title Of The Programme");
            ndt.Columns.Add("Outcome of the Training");
            ndt.Columns.Add("Organizing Body");
            ndt.Columns.Add("Period");
            ndt.Columns.Add("Honour/ Award /Grade Recd., If Any");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Individual> listOfItems = dt.ToList<Individual>();
                    foreach (Individual c in listOfItems)
                    {
                        string getGLTQuery = "Select * from facultydevelopementprogram where individualId='" + c.IndividualId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<FacultyDevelopementProgram> listOfItems2 = dtGTL.ToList<FacultyDevelopementProgram>();
                            foreach (FacultyDevelopementProgram e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name Of The Faculty"] = c.FacultyName;
                                r["Designation"] = c.Designation;
                                r["Name Of The Department"] = c.DeptName;
                                r["Title Of The Programme"]=e.ProgramTitle;
                                r["Outcome of the Training"]=e.OutcomeOfTraining;
                                r["Organizing Body"]=e.OrganizingBody;
                                r["Period"]=e.Period;
                                r["Honour/ Award /Grade Recd., If Any"]=e.Appriciation;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetIndividualPostGraduateTraining(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM individual where userId='" + userId + "'";
            DataTable ndt = new DataTable("PostGraduateTraining");
            ndt.Columns.Add("Name Of The Faculty");
            ndt.Columns.Add("Designation");
            ndt.Columns.Add("Name Of The Department");
            ndt.Columns.Add("Title of the Lesson/ Module");
            ndt.Columns.Add("Name of the Course");
            ndt.Columns.Add("Organizing Body");
            ndt.Columns.Add("Target Group");
            ndt.Columns.Add("Period");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Individual> listOfItems = dt.ToList<Individual>();
                    foreach (Individual c in listOfItems)
                    {
                        string getGLTQuery = "Select * from postgraduatetraining where individualId='" + c.IndividualId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<PostGraduateTraining> listOfItems2 = dtGTL.ToList<PostGraduateTraining>();
                            foreach (PostGraduateTraining e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name Of The Faculty"] = c.FacultyName;
                                r["Designation"] = c.Designation;
                                r["Name Of The Department"] = c.DeptName;
                                r["Title of the Lesson/ Module"]=e.Title;
                                r["Name of the Course"]=e.NameOfCourse;
                                r["Organizing Body"]=e.OrganizingBody;
                                r["Target Group"]=e.TargetGroup;
                                r["Period"]=e.Period;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetIndividualHonoursAndAward(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM individual where userId='" + userId + "'";
            DataTable ndt = new DataTable("HonoursAndAward");
            ndt.Columns.Add("Name Of The Faculty");
            ndt.Columns.Add("Designation");
            ndt.Columns.Add("Name Of The Department");
            ndt.Columns.Add("Member of BOS / Aca. Council and  Awards Recd.");
            ndt.Columns.Add("Nature of  Contribution / Services / consultancy etc.");
            ndt.Columns.Add("Organizing Body");
            ndt.Columns.Add("Honours Recd-  Certificates Etc.");
            ndt.Columns.Add("Date/ period");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<Individual> listOfItems = dt.ToList<Individual>();
                    foreach (Individual c in listOfItems)
                    {
                        string getGLTQuery = "Select * from honoursandawards where individualId='" + c.IndividualId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<HonoursAndAward> listOfItems2 = dtGTL.ToList<HonoursAndAward>();
                            foreach (HonoursAndAward e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name Of The Faculty"] = c.FacultyName;
                                r["Designation"] = c.Designation;
                                r["Name Of The Department"] = c.DeptName;
                                r["Member of BOS / Aca. Council and  Awards Recd."]=e.MemberType;
                                r["Nature of  Contribution / Services / consultancy etc."]=e.NatureOfContribution;
                                r["Organizing Body"]=e.OrganizingBody;
                                r["Honours Recd-  Certificates Etc."]=e.Certificates;
                                r["Date/ period"]=e.Period;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPickperformorsWinnersAtUperLevel(int userId)
        {
            string getDepartmentalQuery = "SELECT *,eventName as event  FROM pickperformors where userId='" + userId + "' and perfomerLevel='WinnersAtZonalLevel'";
            DataTable ndt = new DataTable("PerformerActivity");
            ndt.Columns.Add("Name of the Dept/ Committee");
            ndt.Columns.Add("Event");
            ndt.Columns.Add("Activity");
            ndt.Columns.Add("International/ National / Zonal Level");
            ndt.Columns.Add("Organizing Body & Place");
            ndt.Columns.Add("Date");
            ndt.Columns.Add("Name of the Winners & Class");
            ndt.Columns.Add("Prize");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<PerfomerLevel> listOfItems = dt.ToList<PerfomerLevel>();
                    foreach (PerfomerLevel c in listOfItems)
                    {
                        string getGLTQuery = "Select * from perfomeractivities where performerid='" + c.PerformerId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<PerformerActivity> listOfItems2 = dtGTL.ToList<PerformerActivity>();
                            foreach (PerformerActivity e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name of the Dept/ Committee"]=c.DeptName;
                                r["Event"]=c.Event;
                                r["Activity"]=e.ActivityName;
                                r["International/ National / Zonal Level"]=e.ActivityType;
                                r["Organizing Body & Place"]=e.Sponsors;
                                r["Date"]=e.Date;
                                r["Name of the Winners & Class"]=e.NameOfWinner+", "+e.ClassOfWinner;
                                r["Prize"]=e.Prize;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPickperformorsWinnersInterCollegiateLevel(int userId)
        {
            string getDepartmentalQuery = "SELECT *,eventName as event  FROM pickperformors where userId='" + userId + "' and perfomerLevel='WinnersAtRegionalLevel'";
            DataTable ndt = new DataTable("PerformerActivity");
            ndt.Columns.Add("Name of the Dept/ Committee");
            ndt.Columns.Add("Event");
            ndt.Columns.Add("Activity");
            ndt.Columns.Add("State/Regional");
            ndt.Columns.Add("Organizing Body & Place");
            ndt.Columns.Add("Date");
            ndt.Columns.Add("Name of the Winners & Class");
            ndt.Columns.Add("Prize");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<PerfomerLevel> listOfItems = dt.ToList<PerfomerLevel>();
                    foreach (PerfomerLevel c in listOfItems)
                    {
                        string getGLTQuery = "Select * from perfomeractivities where performerid='" + c.PerformerId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<PerformerActivity> listOfItems2 = dtGTL.ToList<PerformerActivity>();
                            foreach (PerformerActivity e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name of the Dept/ Committee"] = c.DeptName;
                                r["Event"] = c.Event;
                                r["Activity"] = e.ActivityName;
                                r["State/Regional"] = e.ActivityType;
                                r["Organizing Body & Place"] = e.Sponsors;
                                r["Date"] = e.Date;
                                r["Name of the Winners & Class"] = e.NameOfWinner + ", " + e.ClassOfWinner;
                                r["Prize"] = e.Prize;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPickperformorsParticipationAtUperLevel(int userId)
        {
            string getDepartmentalQuery = "SELECT *,eventName as event FROM pickperformors where userId='" + userId + "' and perfomerLevel='ParticipationAtZonalLevel'";
            DataTable ndt = new DataTable("PerformerActivity");
            ndt.Columns.Add("Name of the Dept/ Committee");
            ndt.Columns.Add("Event");
            ndt.Columns.Add("Activity");
            ndt.Columns.Add("International/ National / Zonal Level");
            ndt.Columns.Add("Organizing Body & Place");
            ndt.Columns.Add("Date");
            ndt.Columns.Add("No Of Participants");
            ndt.Columns.Add("Name & Class");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<PerfomerLevel> listOfItems = dt.ToList<PerfomerLevel>();
                    foreach (PerfomerLevel c in listOfItems)
                    {
                        string getGLTQuery = "Select * from perfomeractivities where performerid='" + c.PerformerId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<PerformerActivity> listOfItems2 = dtGTL.ToList<PerformerActivity>();
                            foreach (PerformerActivity e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name of the Dept/ Committee"] = c.DeptName;
                                r["Event"] = c.Event;
                                r["Activity"] = e.ActivityName;
                                r["International/ National / Zonal Level"] = e.ActivityType;
                                r["Organizing Body & Place"] = e.Sponsors;
                                r["Date"] = e.Date;
                                r["No Of Participants"] = e.NumOfParticipants;
                                r["Name & Class"] = e.NameOfParticipants + ", " + e.ClassOfParticipants;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPickperformorsParticipationAtIntercollegiate(int userId)
        {
            string getDepartmentalQuery = "SELECT *,eventName as event FROM pickperformors where userId='" + userId + "' and perfomerLevel='ParticipationAtRegionalLevel'";
            DataTable ndt = new DataTable("PerformerActivity");
            ndt.Columns.Add("Name of the Dept/ Committee");
            ndt.Columns.Add("Event");
            ndt.Columns.Add("Activity");
            ndt.Columns.Add("State/Regional");
            ndt.Columns.Add("Organizing Body & Place");
            ndt.Columns.Add("Date");
            ndt.Columns.Add("No Of Participants");
            ndt.Columns.Add("Name & Class");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<PerfomerLevel> listOfItems = dt.ToList<PerfomerLevel>();
                    foreach (PerfomerLevel c in listOfItems)
                    {
                        string getGLTQuery = "Select * from perfomeractivities where performerid='" + c.PerformerId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<PerformerActivity> listOfItems2 = dtGTL.ToList<PerformerActivity>();
                            foreach (PerformerActivity e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name of the Dept/ Committee"] = c.DeptName;
                                r["Event"] = c.Event;
                                r["Activity"] = e.ActivityName;
                                r["State/Regional"] = e.ActivityType;
                                r["Organizing Body & Place"] = e.Sponsors;
                                r["Date"] = e.Date;
                                r["No Of Participants"] = e.NumOfParticipants;
                                r["Name & Class"] = e.NameOfParticipants + ", " + e.ClassOfParticipants;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPickperformorsPrizeWinnersAtCollegeLevel(int userId)
        {
            string getDepartmentalQuery = "SELECT *,eventName as event FROM pickperformors where userId='" + userId + "' and perfomerLevel='WinnersAtCollageLevel'";
            DataTable ndt = new DataTable("PerformerActivity");
            ndt.Columns.Add("Name of the Dept/ Committee");
            ndt.Columns.Add("Event");
            ndt.Columns.Add("Activity");
            ndt.Columns.Add("Sponsors if any ");
            ndt.Columns.Add("Date");
            ndt.Columns.Add("Names of Winners & Class");
            ndt.Columns.Add("Prize");

            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<PerfomerLevel> listOfItems = dt.ToList<PerfomerLevel>();
                    foreach (PerfomerLevel c in listOfItems)
                    {
                        string getGLTQuery = "Select * from perfomeractivities where performerid='" + c.PerformerId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<PerformerActivity> listOfItems2 = dtGTL.ToList<PerformerActivity>();
                            foreach (PerformerActivity e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Name of the Dept/ Committee"] = c.DeptName;
                                r["Event"] = c.Event;
                                r["Activity"] = e.ActivityName;
                                r["Sponsors if any "]=e.Sponsors;
                                r["Date"]=e.Date;
                                r["Names of Winners & Class"]=e.NameOfWinner+", "+e.ClassOfWinner;
                                r["Prize"]=e.Prize;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetNaacRequirementsEditedBookDetails(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM naacrequirements where userId='" + userId+"'";
            DataTable ndt = new DataTable("naacrequirements");
            ndt.Columns.Add("Teacher Name");
            ndt.Columns.Add("Paper Title");
            ndt.Columns.Add("Book Title");
            ndt.Columns.Add("Author Name");
            ndt.Columns.Add("Conference Title");
            ndt.Columns.Add("Publisher Name");
            ndt.Columns.Add("Publisher Type");
            ndt.Columns.Add("ISBN/ISSN Number");
            ndt.Columns.Add("Affiliating Institutes");
            ndt.Columns.Add("Publication Year");
            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<NaacRequirements> listOfItems = dt.ToList<NaacRequirements>();
                    foreach (NaacRequirements c in listOfItems)
                    {
                        string getGLTQuery = "Select * from editedbookdetails where naacrequirementsid='" + c.NaacRequirementsId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<EditedBookDetail> listOfItems2 = dtGTL.ToList<EditedBookDetail>();
                            foreach (EditedBookDetail e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Teacher Name"]=e.TeacherName;
                                r["Paper Title"]=e.PaperTitle;
                                r["Book Title"]=e.BookTitle;
                                r["Author Name"]=e.AuthorName;
                                r["Conference Title"]=e.ConferenceTitle;
                                r["Publisher Name"]=e.PublisherName;
                                r["Publisher Type"]=e.PublisherType;
                                r["ISBN/ISSN Number"]=e.IsbnOrIssnNumber;
                                r["Affiliating Institutes"]=e.AffiliatingInstitutes;
                                r["Publication Year"]=e.PublicationYear;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetNaacRequirementsExtensionOutReachProgramDetails(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM naacrequirements where userId='" + userId + "'";
            DataTable ndt = new DataTable("ExtensionOutReachProgramDetails");
            ndt.Columns.Add("Extension Name");
            ndt.Columns.Add("Extension Count");
            ndt.Columns.Add("Collaborating Agency Name");
            ndt.Columns.Add("Agency Type");
            ndt.Columns.Add("Contact Number");
            ndt.Columns.Add("Address");
            ndt.Columns.Add("Activity Year");
            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<NaacRequirements> listOfItems = dt.ToList<NaacRequirements>();
                    foreach (NaacRequirements c in listOfItems)
                    {
                        string getGLTQuery = "Select * from extensionoutreachprogramdetails where naacrequirementsid='" + c.NaacRequirementsId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<ExtensionOutReachProgramDetail> listOfItems2 = dtGTL.ToList<ExtensionOutReachProgramDetail>();
                            foreach (ExtensionOutReachProgramDetail e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Extension Name"]=e.ExtensionName;
                                r["Extension Count"]=e.ExtensionCount;
                                r["Collaborating Agency Name"]=e.CollaboratingAgencyName;
                                r["Agency Type"]=e.AgencyType;
                                r["Contact Number"]=e.ContactNumber;
                                r["Address"]=e.Address;
                                r["Activity Year"]=e.ActivityYear;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetNaacRequirementsActivityDetails(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM naacrequirements where userId='" + userId + "'";
            DataTable ndt = new DataTable("ActivityDetails");
            ndt.Columns.Add("Activity Name");
            ndt.Columns.Add("Scheme Name");
            ndt.Columns.Add("Activity Year");
            ndt.Columns.Add("Number Of Teacher");
            ndt.Columns.Add("Number Of Students");
            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<NaacRequirements> listOfItems = dt.ToList<NaacRequirements>();
                    foreach (NaacRequirements c in listOfItems)
                    {
                        string getGLTQuery = "Select * from activitydetails where naacrequirementsid='" + c.NaacRequirementsId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<ActivityDetail> listOfItems2 = dtGTL.ToList<ActivityDetail>();
                            foreach (ActivityDetail e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Activity Name"]=e.ActivityName;
                                r["Scheme Name"]=e.SchemeName;
                                r["Activity Year"]=e.ActivityYear;
                                r["Number Of Teacher"]=e.NumberOfTeacher;
                                r["Number Of Students"]=e.NumberOfTeacher;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetNaacRequirementsLinkageDetails(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM naacrequirements where userId='" + userId + "'";
            DataTable ndt = new DataTable("linkagedetails");
            ndt.Columns.Add("Linkage Title");
            ndt.Columns.Add("Organization Name");
            ndt.Columns.Add("Details");
            ndt.Columns.Add("Commencement Year");
            ndt.Columns.Add("Duration From");
            ndt.Columns.Add("Duration To");
            ndt.Columns.Add("Nature Of Linkage");
            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<NaacRequirements> listOfItems = dt.ToList<NaacRequirements>();
                    foreach (NaacRequirements c in listOfItems)
                    {
                        string getGLTQuery = "Select * from linkagedetails where naacrequirementsid='" + c.NaacRequirementsId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<LinkageDetail> listOfItems2 = dtGTL.ToList<LinkageDetail>();
                            foreach (LinkageDetail e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Linkage Title"]=e.LinkageTitle;
                                r["Organization Name"]=e.OrganizationName;
                                r["Details"]=e.Details;
                                r["Commencement Year"]=e.CommencementYear;
                                r["Duration From"]=e.DurationFrom;
                                r["Duration To"]=e.DurationTo;
                                r["Nature Of Linkage"]=e.NatureOfLinkage;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetNaacRequirementsMOUDetails(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM naacrequirements where userId='" + userId + "'";
            DataTable ndt = new DataTable("MOUDetails");
            ndt.Columns.Add("Organisation MOUSigned");
            ndt.Columns.Add("Institute Name");
            ndt.Columns.Add("Year Of Sign");
            ndt.Columns.Add("Duration");
            ndt.Columns.Add("List Of Activities");
            ndt.Columns.Add("Number Of Students");
            ndt.Columns.Add("Number Of Teachers");
            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<NaacRequirements> listOfItems = dt.ToList<NaacRequirements>();
                    foreach (NaacRequirements c in listOfItems)
                    {
                        string getGLTQuery = "Select * from moudetails where naacrequirementsid='" + c.NaacRequirementsId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<MouDetail> listOfItems2 = dtGTL.ToList<MouDetail>();
                            foreach (MouDetail e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Organisation MOUSigned"]=e.OrganisationMOUSigned;
                                r["Institute Name"]=e.InstituteName;
                                r["Year Of Sign"]=e.YearOfSign;
                                r["Duration"]=e.Duration;
                                r["List Of Activities"]=e.ListOfActivities;
                                r["Number Of Students"]=e.NumberOfStudents;
                                r["Number Of Teachers"]=e.NumberOfTeachers;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetNaacRequirementsDevelopmentSchemeDetails(int userId)
        {
            string getDepartmentalQuery = "SELECT * FROM naacrequirements where userId='" + userId + "'";
            DataTable ndt = new DataTable("DevelopmentSchemeDetails");
            ndt.Columns.Add("Scheme Option");
            ndt.Columns.Add("Scheme Count");
            ndt.Columns.Add("Scheme Name");
            ndt.Columns.Add("Implementation Year");
            ndt.Columns.Add("Number Of Students");
            ndt.Columns.Add("Agency Name");
            ndt.Columns.Add("Contact Number");
            ndt.Columns.Add("Agency Address");
            try
            {
                DataSet dsItems = new DataSet();
                DataSet dsGLT = new DataSet();

                dsItems = GetQuery(getDepartmentalQuery);
                if (dsItems.Tables[0].Rows != null && dsItems.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsItems.Tables[0];
                    List<NaacRequirements> listOfItems = dt.ToList<NaacRequirements>();
                    foreach (NaacRequirements c in listOfItems)
                    {
                        string getGLTQuery = "Select * from developmentschemedetails where naacrequirementsid='" + c.NaacRequirementsId + "'";
                        dsGLT = GetQuery(getGLTQuery);
                        if (dsGLT.Tables[0].Rows != null && dsGLT.Tables[0].Rows.Count > 0)
                        {
                            DataTable dtGTL = dsGLT.Tables[0];
                            List<DevelopmentSchemeDetail> listOfItems2 = dtGTL.ToList<DevelopmentSchemeDetail>();
                            foreach (DevelopmentSchemeDetail e in listOfItems2)
                            {
                                DataRow r = ndt.NewRow();
                                r["Scheme Option"]=e.SchemeOption;
                                r["Scheme Count"]=e.SchemeCount;
                                r["Scheme Name"]=e.SchemeName;
                                r["Implementation Year"]=e.ImplementationYear;
                                r["Number Of Students"]=e.NumberOfStudents;
                                r["Agency Name"]=e.AgencyName;
                                r["Contact Number"]=e.ContactNumber;
                                r["Agency Address"]=e.AgencyAddress;
                                ndt.Rows.Add(r);
                            }
                        }
                    }
                }
                return ndt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}