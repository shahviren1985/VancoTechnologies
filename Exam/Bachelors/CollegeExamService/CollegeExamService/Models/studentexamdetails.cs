using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeExamService.Models
{
    public class studentexamdetails
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("studentid")]
        public int studentid { get; set; }

        [JsonProperty("examname")]
        public string examname { get; set; }
        [JsonProperty("examtype")]
        public string examtype { get; set; }
        [JsonProperty("semester")]
        public int semester { get; set; }
    }
}