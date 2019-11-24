using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeExamService.Models
{
    public class BaseClass
    {
        [JsonProperty("isSuccess", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsSuccess { get; set; }

        [JsonProperty("errorMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorMessage { get; set; }

        [JsonProperty("SuccessMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string SuccessMessage { get; set; }

    }
}