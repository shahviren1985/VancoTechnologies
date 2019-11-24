using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Models
{
    public class GuestLectureTalk
    {
        [JsonProperty("deptId")]
        public int DeptId { get; set; }

        [JsonProperty("srNo")]
        public int SrNo { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("objective")]
        public string Objective { get; set; }

        [JsonProperty("resourcePerson")]
        public string ResourcePerson { get; set; }

        [JsonProperty("designationAndOrganization")]
        public string DesignationAndOrganization { get; set; }

        [JsonProperty("numOfStudent")]
        public int NumOfStudent { get; set; }

        [JsonProperty("sponsors")]
        public string Sponsors { get; set; }

        [JsonProperty("dateOfLecture")]
        public DateTime DateOfLecture { get; set; }
    }

    public class Visit
    {
        [JsonProperty("deptId")]
        public int DeptId { get; set; }

        [JsonProperty("srNo")]
        public int SrNo { get; set; }

        [JsonProperty("nameOfOrganization")]
        public string NameOfOrganization { get; set; }

        [JsonProperty("place")]
        public string Place { get; set; }

        [JsonProperty("objective")]
        public string Objective { get; set; }

        [JsonProperty("numOfStudent")]
        public int NumOfStudent { get; set; }

        [JsonProperty("sponsoringAgency")]
        public string SponsoringAgency { get; set; }

        [JsonProperty("dateOfVisit")]
        public DateTime DateOfVisit { get; set; }
    }

    public class Seminar
    {
        [JsonProperty("deptId")]
        public int DeptId { get; set; }

        [JsonProperty("srNo")]
        public int SrNo { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("objective")]
        public string Objective { get; set; }

        [JsonProperty("resourcePerson")]
        public string ResourcePerson { get; set; }

        [JsonProperty("designationOfRecourcePerson")]
        public string DesignationOfRecourcePerson { get; set; }

        [JsonProperty("numOfStudent")]
        public int NumOfStudent { get; set; }

        [JsonProperty("sponsors")]
        public string Sponsors { get; set; }

        [JsonProperty("dateOfSeminar")]
        public DateTime DateOfSeminar { get; set; }
    }

    public class Activity
    {
        [JsonProperty("deptId")]
        public int DeptId { get; set; }

        [JsonProperty("srNo")]
        public int SrNo { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("objective")]
        public string Objective { get; set; }

        [JsonProperty("targetGroup")]
        public string TargetGroup { get; set; }

        [JsonProperty("levelOfAcitivity")]
        public string LevelOfAcitivity { get; set; }

        [JsonProperty("numOfStudent")]
        public int NumOfStudent { get; set; }

        [JsonProperty("dateOfActivity")]
        public DateTime DateOfActivity { get; set; }
    }

    public class Collaboration
    {
        [JsonProperty("deptId")]
        public int DeptId { get; set; }

        [JsonProperty("srNo")]
        public int SrNo { get; set; }

        [JsonProperty("bodiesOrDept")]
        public string BodiesOrDept { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("objective")]
        public string Objective { get; set; }

        [JsonProperty("place")]
        public string Place { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("numOfBeneficiaries")]
        public string NumOfBeneficiaries { get; set; }

        [JsonProperty("dateOfCollaborations")]
        public DateTime DateOfCollaborations { get; set; }
    }

    public class InHouseCollaboration
    {
        [JsonProperty("deptId")]
        public int DeptId { get; set; }

        [JsonProperty("srNo")]
        public int SrNo { get; set; }

        [JsonProperty("bodiesOrDept")]
        public string BodiesOrDept { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("objective")]
        public string Objective { get; set; }

        [JsonProperty("place")]
        public string Place { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("numOfBeneficiaries")]
        public string NumOfBeneficiaries { get; set; }

        [JsonProperty("dateOfCollaborations")]
        public DateTime DateOfCollaborations { get; set; }
    }

    public class DepartmentalSeminar
    {
        [JsonProperty("deptId")]
        public int DeptId { get; set; }

        [JsonProperty("srNo")]
        public int SrNo { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("numOfParticipants")]
        public string NumOfParticipants { get; set; }

        [JsonProperty("numOfPapersPresented")]
        public string NumOfPapersPresented { get; set; }

        [JsonProperty("numOfExpertsInvited")]
        public string NumOfExpertsInvited { get; set; }

        [JsonProperty("grantReceived")]
        public string GrantReceived { get; set; }

        [JsonProperty("fundingAgency")]
        public string FundingAgency { get; set; }

        [JsonProperty("publication")]
        public string Publication { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }

    public class Departmental
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("deptId")]
        public int DeptId { get; set; }

        [JsonProperty("deptName")]
        public string DeptName { get; set; }

        [JsonProperty("guestLectureTalk")]
        public IList<GuestLectureTalk> GuestLectureTalk { get; set; }

        [JsonProperty("visits")]
        public IList<Visit> Visits { get; set; }

        [JsonProperty("seminars")]
        public IList<Seminar> Seminars { get; set; }

        [JsonProperty("activities")]
        public IList<Activity> Activities { get; set; }

        [JsonProperty("collaborations")]
        public IList<Collaboration> Collaborations { get; set; }

        [JsonProperty("inHouseCollaborations")]
        public IList<InHouseCollaboration> InHouseCollaborations { get; set; }

        [JsonProperty("departmentalSeminar")]
        public IList<DepartmentalSeminar> DepartmentalSeminar { get; set; }
    }


}