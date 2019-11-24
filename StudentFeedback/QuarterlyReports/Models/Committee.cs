using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Models
{
    public class Committee
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("commiteeId")]
        public int CommiteeId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("incharge")]
        public string Incharge { get; set; }

        [JsonProperty("events")]
        public List<Event> Events { get; set; }
    }
}