using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Models
{
    public class EditedBookDetail
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("naacRequirementsId")]
        public int NaacRequirementsId { get; set; }

        [JsonProperty("teacherName")]
        public string TeacherName { get; set; }

        [JsonProperty("paperTitle")]
        public string PaperTitle { get; set; }

        [JsonProperty("bookTitle")]
        public string BookTitle { get; set; }

        [JsonProperty("authorName")]
        public string AuthorName { get; set; }

        [JsonProperty("conferenceTitle")]
        public string ConferenceTitle { get; set; }

        [JsonProperty("publisherName")]
        public string PublisherName { get; set; }

        [JsonProperty("publisherType")]
        public string PublisherType { get; set; }

        [JsonProperty("isbnOrIssnNumber")]
        public string IsbnOrIssnNumber { get; set; }

        [JsonProperty("affiliatingInstitutes")]
        public string AffiliatingInstitutes { get; set; }

        [JsonProperty("publicationYear")]
        public string PublicationYear { get; set; }
    }

    public class ExtensionOutReachProgramDetail
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("naacRequirementsId")]
        public int NaacRequirementsId { get; set; }


        [JsonProperty("extensionName")]
        public string ExtensionName { get; set; }

        [JsonProperty("extensionCount")]
        public string ExtensionCount { get; set; }

        [JsonProperty("collaboratingAgencyName")]
        public string CollaboratingAgencyName { get; set; }

        [JsonProperty("agencyType")]
        public string AgencyType { get; set; }

        [JsonProperty("contactNumber")]
        public string ContactNumber { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("activityYear")]
        public string ActivityYear { get; set; }
    }

    public class ActivityDetail
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("naacRequirementsId")]
        public int NaacRequirementsId { get; set; }


        [JsonProperty("activityName")]
        public string ActivityName { get; set; }

        [JsonProperty("schemeName")]
        public string SchemeName { get; set; }

        [JsonProperty("activityYear")]
        public string ActivityYear { get; set; }

        [JsonProperty("numberOfTeacher")]
        public string NumberOfTeacher { get; set; }

        [JsonProperty("numberOfStudents")]
        public string NumberOfStudents { get; set; }
    }

    public class LinkageDetail
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("naacRequirementsId")]
        public int NaacRequirementsId { get; set; }


        [JsonProperty("linkageTitle")]
        public string LinkageTitle { get; set; }

        [JsonProperty("organizationName")]
        public string OrganizationName { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("commencementYear")]
        public string CommencementYear { get; set; }

        [JsonProperty("durationFrom")]
        public string DurationFrom { get; set; }

        [JsonProperty("durationTo")]
        public string DurationTo { get; set; }

        [JsonProperty("natureOfLinkage")]
        public string NatureOfLinkage { get; set; }
    }

    public class MouDetail
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("naacRequirementsId")]
        public int NaacRequirementsId { get; set; }


        [JsonProperty("organisationMOUSigned")]
        public string OrganisationMOUSigned { get; set; }

        [JsonProperty("instituteName")]
        public string InstituteName { get; set; }

        [JsonProperty("yearOfSign")]
        public string YearOfSign { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("listOfActivities")]
        public string ListOfActivities { get; set; }

        [JsonProperty("numberOfStudents")]
        public string NumberOfStudents { get; set; }

        [JsonProperty("numberOfTeachers")]
        public string NumberOfTeachers { get; set; }
    }

    public class DevelopmentSchemeDetail
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("naacRequirementsId")]
        public int NaacRequirementsId { get; set; }


        [JsonProperty("schemeOption")]
        public string SchemeOption { get; set; }

        [JsonProperty("schemeCount")]
        public string SchemeCount { get; set; }

        [JsonProperty("schemeName")]
        public string SchemeName { get; set; }

        [JsonProperty("implementationYear")]
        public string ImplementationYear { get; set; }

        [JsonProperty("numberOfStudents")]
        public string NumberOfStudents { get; set; }

        [JsonProperty("agencyName")]
        public string AgencyName { get; set; }

        [JsonProperty("contactNumber")]
        public string ContactNumber { get; set; }

        [JsonProperty("agencyAddress")]
        public string AgencyAddress { get; set; }
    }

    public class NaacRequirements
    {

        [JsonProperty("naacRequirementsId")]
        public int NaacRequirementsId { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("modifiedDate")]
        public DateTime ModifiedDate { get; set; }

        [JsonProperty("editedBookDetails")]
        public List<EditedBookDetail> EditedBookDetails { get; set; }

        [JsonProperty("extensionOutReachProgramDetails")]
        public List<ExtensionOutReachProgramDetail> ExtensionOutReachProgramDetails { get; set; }

        [JsonProperty("activityDetails")]
        public List<ActivityDetail> ActivityDetails { get; set; }

        [JsonProperty("linkageDetails")]
        public List<LinkageDetail> LinkageDetails { get; set; }

        [JsonProperty("mouDetails")]
        public List<MouDetail> MouDetails { get; set; }

        [JsonProperty("developmentSchemeDetails")]
        public List<DevelopmentSchemeDetail> DevelopmentSchemeDetails { get; set; }
    }
}