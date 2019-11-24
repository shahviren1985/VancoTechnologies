using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Models
{
    public class ExitForm
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("formId")]
        public int FormId { get; set; }

        [JsonProperty("a1")]
        public string A1 { get; set; }

        [JsonProperty("a2")]
        public A2 A2 { get; set; }

        [JsonProperty("a3")]
        public A3 A3 { get; set; }

        [JsonProperty("a4")]
        public string A4 { get; set; }

        [JsonProperty("a5")]
        public string A5 { get; set; }

        [JsonProperty("a6")]
        public List<A6> A6 { get; set; }

        [JsonProperty("a7")]
        public A7 A7 { get; set; }

        [JsonProperty("a8")]
        public string A8 { get; set; }

        [JsonProperty("a9")]
        public List<A9> A9 { get; set; }

        [JsonProperty("dateCreated")]
        public DateTime DateCreated { get; set; }
        [JsonIgnore]
        public string IPAddress { get; set; }
        [JsonIgnore]
        public string FwdIPAddresses { get; set; }
    }

    public class A2
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("mostValuable")]
        public string MostValuable { get; set; }

        [JsonProperty("valuable")]
        public string Valuable { get; set; }

        [JsonProperty("lessValuable")]
        public string LessValuable { get; set; }
    }

    public class A3
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("postiveEffect")]
        public string PostiveEffect { get; set; }

        [JsonProperty("negativeEffect")]
        public string NegativeEffect { get; set; }
    }

    public class A6
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("deptName")]
        public string DeptName { get; set; }

        [JsonProperty("positiveComment")]
        public string PositiveComment { get; set; }

        [JsonProperty("negativeComment")]
        public string NegativeComment { get; set; }
    }

    public class A7
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("isParticipated")]
        public string IsParticipated { get; set; }

        [JsonProperty("eventName")]
        public string EventName { get; set; }
    }

    public class A9
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("deptName")]
        public string DeptName { get; set; }

        [JsonProperty("a1")]
        public int A1 { get; set; }

        [JsonProperty("a2")]
        public int A2 { get; set; }

        [JsonProperty("a3")]
        public int A3 { get; set; }

        [JsonProperty("a4")]
        public int A4 { get; set; }

        [JsonProperty("a5")]
        public int A5 { get; set; }
    }

    


}