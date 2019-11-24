using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoshniAppWebApi.Models
{
    public class UserDetails
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }

        [JsonProperty("isValidated")]
        public bool IsValidated { get; set; }

        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty("userType")]
        public string UserType { get; set; }

        [JsonProperty("emergency1")]
        public string Emergency1 { get; set; }

        [JsonProperty("emergency2")]
        public string Emergency2 { get; set; }

        [JsonProperty("emergency3")]
        public string Emergency3 { get; set; }

        [JsonProperty("emergency4")]
        public string Emergency4 { get; set; }

        [JsonProperty("emergency5")]
        public string Emergency5 { get; set; }

    }
}