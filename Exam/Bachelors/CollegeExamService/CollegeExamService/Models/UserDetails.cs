using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeExamService.Models
{
    public class UserList : BaseClass
    {
        [JsonProperty("users")]
        public List<UserDetails> Users { get; set; }
    }

    public class Paper
    {

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("sections")]
        public int Sections { get; set; }
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

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("paper")]
        public List<Paper> Paper { get; set; }

        public static UserDetails FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            UserDetails response = new UserDetails();
            if (string.IsNullOrEmpty(values[0]))
                response.UserId = 0;
            else
                response.UserId = int.Parse(values[0]);
            response.UserName = values[1];
            response.Password = values[2];
            response.MobileNumber = values[3];
            response.FirstName = values[4];
            response.LastName = values[5];
            string value6 = values[6];
            if (string.IsNullOrEmpty(value6))
                response.Role = "Customer";
            else
                response.Role = value6;
            return response;
        }

    }

    public class User
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

        [JsonProperty("role")]
        public string Role { get; set; }

    }

}