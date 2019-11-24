using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoshniAppWebApi.Models
{
    public class CounselorList
    {

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }
    }

    public class NGOList
    {

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }
        [JsonProperty("Latitude")]
        public string Latitude { get; set; }
        [JsonProperty("Longitude")]
        public string Longitude { get; set; }

        [JsonProperty("Contact")]
        public string Contact { get; set; }

        [JsonProperty("Website")]
        public string Website { get; set; }

        [JsonProperty("TimingFrom")]
        public string TimingFrom { get; set; }

        [JsonProperty("TimingTo")]
        public string TimingTo { get; set; }

        [JsonProperty("DaysFrom")]
        public string DaysFrom { get; set; }

        [JsonProperty("DaysTo")]
        public string DaysTo { get; set; }

        [JsonProperty("CounselorList")]
        public List<CounselorList> CounselorList { get; set; }
    }
}