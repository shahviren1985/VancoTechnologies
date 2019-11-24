using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoshniAppWebApi.Models
{
    public class OTPLogs
    {
        [JsonProperty("otpLogSid")]
        public int OtpLogSid { get; set; }
       
        [JsonProperty("userId")]
        public int UserId { get; set; }
       
        [JsonProperty("sentToMobileNumber")]
        public string SentToMobileNumber { get; set; }

        [JsonProperty("otpMessage")]
        public string OtpMessage { get; set; }

        [JsonProperty("otpCode")]
        public string OtpCode { get; set; }

        [JsonProperty("isValidated")]
        public bool IsValidated { get; set; }

        [JsonProperty("sentAt")]
        public DateTime SentAt { get; set; }

        [JsonProperty("validatedAt")]
        public DateTime ValidatedAt { get; set; }

    }
}