using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVT.Business.Model
{
    public class AadharClass : BaseClass
    {
        public bool IsAadharExist { get; set; }
        public bool IsFormSubmitted { get; set; }
    }

    public class BaseClass
    {
        [JsonProperty("isSuccess", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsSuccess { get; set; }

        [JsonProperty("errorMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorMessage { get; set; }

        [JsonProperty("SuccessMessage", NullValueHandling = NullValueHandling.Ignore)]
        public string SuccessMessage { get; set; }

    }

    public class UserDetails : BaseClass
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

    }

    public class FileClass : BaseClass
    {
        [JsonProperty("file")]
        public string File { get; set; }

        [JsonProperty("type")]
        public string FileType { get; set; }
    }
}
