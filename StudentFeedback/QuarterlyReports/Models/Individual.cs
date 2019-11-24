using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Models
{
    public class ArticlesInJournal
    {
        [JsonProperty("individualId")]
        public int IndividualId { get; set; }

        [JsonProperty("paperTitle")]
        public string PaperTitle { get; set; }

        [JsonProperty("paperType")]
        public string PaperType { get; set; }

        [JsonProperty("paperNumber")]
        public string PaperNumber { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("impactFactor")]
        public string ImpactFactor { get; set; }

        [JsonProperty("place")]
        public string Place { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }

    public class ProcInConferenceSeminar
    {
        [JsonProperty("individualId")]
        public int IndividualId { get; set; }

        [JsonProperty("paperTitle")]
        public string PaperTitle { get; set; }

        [JsonProperty("seminarTitle")]
        public string SeminarTitle { get; set; }

        [JsonProperty("organizingBody")]
        public string OrganizingBody { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("iNumber")]
        public string INumber { get; set; }

        [JsonProperty("place")]
        public string Place { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }

    public class ChapterInBook
    {
        [JsonProperty("individualId")]
        public int IndividualId { get; set; }

        [JsonProperty("ChapterTitle")]
        public string ChapterTitle { get; set; }

        [JsonProperty("bookName")]
        public string BookName { get; set; }

        [JsonProperty("iNumber")]
        public string INumber { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("place")]
        public string Place { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }

    public class BookEditedWritten
    {
        [JsonProperty("individualId")]
        public int IndividualId { get; set; }

        [JsonProperty("nameOfBook")]
        public string NameOfBook { get; set; }

        [JsonProperty("iNumber")]
        public string INumber { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("place")]
        public string Place { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }

    public class Article
    {
        [JsonProperty("individualId")]
        public int IndividualId { get; set; }

        [JsonProperty("articleTitle")]
        public string ArticleTitle { get; set; }

        [JsonProperty("newsPaperMagazineTitle")]
        public string NewsPaperMagazineTitle { get; set; }

        [JsonProperty("natureOfArticle")]
        public string NatureOfArticle { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("place")]
        public string Place { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }

    public class PaperPresentation
    {
        [JsonProperty("individualId")]
        public int IndividualId { get; set; }

        [JsonProperty("paperTitle")]
        public string PaperTitle { get; set; }

        [JsonProperty("seminarTitle")]
        public string SeminarTitle { get; set; }

        [JsonProperty("sponsors")]
        public string Sponsors { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }

    public class TrainingProgram
    {
        [JsonProperty("individualId")]
        public int IndividualId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("attendedOrOrganised")]
        public string AttendedOrOrganised { get; set; }

        [JsonProperty("organizingBody")]
        public string OrganizingBody { get; set; }

        [JsonProperty("sponsors")]
        public string Sponsors { get; set; }
    }

    public class IndividualSeminar
    {
        [JsonProperty("individualId")]
        public int IndividualId { get; set; }

        [JsonProperty("seminarTitle")]
        public string SeminarTitle { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("attendedOrOrganised")]
        public string AttendedOrOrganised { get; set; }

        [JsonProperty("organizingBody")]
        public string OrganizingBody { get; set; }

        [JsonProperty("sponsors")]
        public string Sponsors { get; set; }
    }

    public class FacultyDevelopementProgram
    {
        [JsonProperty("individualId")]
        public int IndividualId { get; set; }

        [JsonProperty("programTitle")]
        public string ProgramTitle { get; set; }

        [JsonProperty("outcomeOfTraining")]
        public string OutcomeOfTraining { get; set; }

        [JsonProperty("organizingBody")]
        public string OrganizingBody { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("appriciation")]
        public string Appriciation { get; set; }
    }

    public class PostGraduateTraining
    {
        [JsonProperty("individualId")]
        public int IndividualId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("nameOfCourse")]
        public string NameOfCourse { get; set; }

        [JsonProperty("organizingBody")]
        public string OrganizingBody { get; set; }

        [JsonProperty("targetGroup")]
        public string TargetGroup { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }
    }

    public class HonoursAndAward
    {
        [JsonProperty("individualId")]
        public int IndividualId { get; set; }

        [JsonProperty("memberType")]
        public string MemberType { get; set; }

        [JsonProperty("natureOfContribution")]
        public string NatureOfContribution { get; set; }

        [JsonProperty("organizingBody")]
        public string OrganizingBody { get; set; }

        [JsonProperty("certificates")]
        public string Certificates { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }
    }

    public class Publications
    {

        [JsonProperty("articlesInJournals")]
        public IList<ArticlesInJournal> ArticlesInJournals { get; set; }

        [JsonProperty("procInConferenceSeminar")]
        public IList<ProcInConferenceSeminar> ProcInConferenceSeminar { get; set; }

        [JsonProperty("chapterInBooks")]
        public IList<ChapterInBook> ChapterInBooks { get; set; }

        [JsonProperty("bookEditedWritten")]
        public IList<BookEditedWritten> BookEditedWritten { get; set; }

        [JsonProperty("articles")]
        public IList<Article> Articles { get; set; }

        [JsonProperty("paperPresentations")]
        public IList<PaperPresentation> PaperPresentations { get; set; }

        [JsonProperty("trainingProgram")]
        public IList<TrainingProgram> TrainingProgram { get; set; }

        [JsonProperty("seminars")]
        public IList<IndividualSeminar> Seminars { get; set; }

        [JsonProperty("facultyDevelopementProgram")]
        public IList<FacultyDevelopementProgram> FacultyDevelopementProgram { get; set; }

        [JsonProperty("postGraduateTraining")]
        public IList<PostGraduateTraining> PostGraduateTraining { get; set; }

        [JsonProperty("HonoursAndAwards")]
        public IList<HonoursAndAward> HonoursAndAwards { get; set; }
    }

    public class Individual
    {
        [JsonProperty("individualId")]
        public int IndividualId { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("facultyName")]
        public string FacultyName { get; set; }

        [JsonProperty("designation")]
        public string Designation { get; set; }

        [JsonProperty("deptName")]
        public string DeptName { get; set; }

        [JsonProperty("publications")]
        public Publications Publications { get; set; }
    }


}