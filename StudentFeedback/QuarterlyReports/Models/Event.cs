using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Models
{
    public class Event
    {
        [JsonProperty("commiteeId")]
        public int CommiteeId { get; set; }

        [JsonProperty("eventId")]
        public int EventId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("eventType")]
        public string EventType { get; set; }

        [JsonProperty("eventDate")]
        public DateTime EventDate { get; set; }

        [JsonProperty("venue")]
        public string Venue { get; set; }

        [JsonProperty("objective")]
        public string Objective { get; set; }

        [JsonProperty("numOfParticipants")]
        public int NumOfParticipants { get; set; }

        [JsonProperty("sponsors")]
        public string Sponsors { get; set; }

        [JsonProperty("outcome")]
        public string Outcome { get; set; }
    }


}