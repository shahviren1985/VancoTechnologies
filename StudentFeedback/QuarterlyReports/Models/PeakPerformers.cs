using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Models
{
    public class WinnersAtZonalLevelActivity : PerformerActivity
    {
        //[JsonProperty("activityName")]
        //public string ActivityName { get; set; }

        //[JsonProperty("level")]
        //public string Level { get; set; }

        //[JsonProperty("organizingBody")]
        //public string OrganizingBody { get; set; }

        //[JsonProperty("date")]
        //public DateTime Date { get; set; }

        //[JsonProperty("nameOfWinner")]
        //public string NameOfWinner { get; set; }

        //[JsonProperty("classOfWinner")]
        //public string ClassOfWinner { get; set; }

        //[JsonProperty("prize")]
        //public string Prize { get; set; }
    }

    public class WinnersAtZonalLevel : PerfomerLevel
    {

        //[JsonProperty("deptName")]
        //public string DeptName { get; set; }

        //[JsonProperty("event")]
        //public string Event { get; set; }

        [JsonProperty("activities")]
        public IList<WinnersAtZonalLevelActivity> Activities { get; set; }
    }

    public class WinnersAtRegionalLevelActivity : PerformerActivity
    {

        //[JsonProperty("activityName")]
        //public string ActivityName { get; set; }

        //[JsonProperty("level")]
        //public string Level { get; set; }

        //[JsonProperty("organizingBody")]
        //public string OrganizingBody { get; set; }

        //[JsonProperty("date")]
        //public DateTime Date { get; set; }

        //[JsonProperty("nameOfWinner")]
        //public string NameOfWinner { get; set; }

        //[JsonProperty("classOfWinner")]
        //public string ClassOfWinner { get; set; }

        //[JsonProperty("prize")]
        //public string Prize { get; set; }
    }

    public class WinnersAtRegionalLevel : PerfomerLevel
    {

        //[JsonProperty("deptName")]
        //public string DeptName { get; set; }

        //[JsonProperty("event")]
        //public string Event { get; set; }

        [JsonProperty("activities")]
        public IList<WinnersAtRegionalLevelActivity> Activities { get; set; }
    }

    public class ParticipationAtZonalLevelActivity : PerformerActivity
    {

        //[JsonProperty("activityName")]
        //public string ActivityName { get; set; }

        //[JsonProperty("level")]
        //public string Level { get; set; }

        //[JsonProperty("organizingBody")]
        //public string OrganizingBody { get; set; }

        //[JsonProperty("date")]
        //public DateTime Date { get; set; }

        //[JsonProperty("nameOfParticipants")]
        //public string NameOfParticipants { get; set; }

        //[JsonProperty("classOfParticipants")]
        //public string ClassOfParticipants { get; set; }

        //[JsonProperty("numOfParticipants")]
        //public string NumOfParticipants { get; set; }
    }

    public class ParticipationAtZonalLevel : PerfomerLevel
    {

        //[JsonProperty("deptName")]
        //public string DeptName { get; set; }

        //[JsonProperty("event")]
        //public string Event { get; set; }

        [JsonProperty("activities")]
        public IList<ParticipationAtZonalLevelActivity> Activities { get; set; }
    }

    public class ParticipationAtRegionalLevelActivity : PerformerActivity
    {

        [JsonProperty("activityName")]
        public string ActivityName { get; set; }

        [JsonProperty("level")]
        public string Level { get; set; }

        [JsonProperty("organizingBody")]
        public string OrganizingBody { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("nameOfParticipants")]
        public string NameOfParticipants { get; set; }

        [JsonProperty("classOfParticipants")]
        public string ClassOfParticipants { get; set; }

        [JsonProperty("numOfParticipants")]
        public string NumOfParticipants { get; set; }
    }

    public class ParticipationAtRegionalLevel : PerfomerLevel
    {

        [JsonProperty("deptName")]
        public string DeptName { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("activities")]
        public IList<ParticipationAtRegionalLevelActivity> Activities { get; set; }
    }

    public class WinnersAtCollageLevelActivity  : PerformerActivity
    {

        //[JsonProperty("activityName")]
        //public string ActivityName { get; set; }

        //[JsonProperty("sponsors")]
        //public string Sponsors { get; set; }

        //[JsonProperty("date")]
        //public DateTime Date { get; set; }

        //[JsonProperty("nameOfWinner")]
        //public string NameOfWinner { get; set; }

        //[JsonProperty("classOfWinner")]
        //public string ClassOfWinner { get; set; }

        //[JsonProperty("prize")]
        //public string Prize { get; set; }
    }

    public class WinnersAtCollageLevel : PerfomerLevel
    {

        [JsonProperty("activities")]
        public IList<WinnersAtCollageLevelActivity> Activities { get; set; }
    }

    public class PeakPerformers
    {
        [JsonProperty("winnersAtZonalLevel")]
        public WinnersAtZonalLevel WinnersAtZonalLevel { get; set; }

        [JsonProperty("winnersAtRegionalLevel")]
        public WinnersAtRegionalLevel WinnersAtRegionalLevel { get; set; }

        [JsonProperty("participationAtZonalLevel")]
        public ParticipationAtZonalLevel ParticipationAtZonalLevel { get; set; }

        [JsonProperty("participationAtRegionalLevel")]
        public ParticipationAtRegionalLevel ParticipationAtRegionalLevel { get; set; }

        [JsonProperty("winnersAtCollageLevel")]
        public WinnersAtCollageLevel WinnersAtCollageLevel { get; set; }
    }

    public class PerformerActivity 
    {
        [JsonProperty("performerId")]
        public int PerformerId { get; set; }

        [JsonProperty("activityId")]
        public int ActivityId { get; set; }

        [JsonProperty("activityName")]
        public string ActivityName { get; set; }

        [JsonProperty("activityType")]
        public string ActivityType { get; set; }

        [JsonProperty("sponsors")]
        public string Sponsors { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("nameOfWinner")]
        public string NameOfWinner { get; set; }

        [JsonProperty("classOfWinner")]
        public string ClassOfWinner { get; set; }

        [JsonProperty("prize")]
        public string Prize { get; set; }

        [JsonProperty("nameOfParticipants")]
        public string NameOfParticipants { get; set; }

        [JsonProperty("classOfParticipants")]
        public string ClassOfParticipants { get; set; }

        [JsonProperty("numOfParticipants")]
        public string NumOfParticipants { get; set; }
    }

    public class PerfomerLevel 
    {

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("performerId")]
        public int PerformerId { get; set; }

        [JsonProperty("deptId")]
        public int DeptId { get; set; }

        [JsonProperty("eventId")]
        public int EventId { get; set; }

        [JsonProperty("deptName")]
        public string DeptName { get; set; }

        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("peakPerformerLevel")]
        public string PeakPerformerLevel { get; set; }

    }
}