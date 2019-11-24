using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoshniAppWebApi.Models
{
    public class SOSLogs
    {
        [JsonProperty("sosLogsid")]
        public int SosLogsid { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("sentToMobileNumber")]
        public string SentToMobileNumber { get; set; }

        [JsonProperty("sosMessage")]
        public string SosMessage { get; set; }

        [JsonProperty("sentAt")]
        public DateTime SentAt { get; set; }
    }
}