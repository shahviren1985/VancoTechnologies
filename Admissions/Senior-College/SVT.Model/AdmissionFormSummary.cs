using Newtonsoft.Json;
using SVT.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVT.Business.Model
{
    public class OpenInternal
    {

        [JsonProperty("totalForms")]
        public int TotalForms { get; set; }

        [JsonProperty("maxPercent")]
        public double MaxPercent { get; set; }

        [JsonProperty("minPercent")]
        public double MinPercent { get; set; }

        [JsonProperty("avgPercent")]
        public double AvgPercent { get; set; }
    }

    public class OpenExternal
    {

        [JsonProperty("totalForms")]
        public int TotalForms { get; set; }

        [JsonProperty("maxPercent")]
        public double MaxPercent { get; set; }

        [JsonProperty("minPercent")]
        public double MinPercent { get; set; }

        [JsonProperty("avgPercent")]
        public double AvgPercent { get; set; }
    }

    public class ReservedInternal
    {

        [JsonProperty("totalForms")]
        public int TotalForms { get; set; }

        [JsonProperty("maxPercent")]
        public double MaxPercent { get; set; }

        [JsonProperty("minPercent")]
        public double MinPercent { get; set; }

        [JsonProperty("avgPercent")]
        public double AvgPercent { get; set; }
    }

    public class ReservedExternal
    {

        [JsonProperty("totalForms")]
        public int TotalForms { get; set; }

        [JsonProperty("maxPercent")]
        public double MaxPercent { get; set; }

        [JsonProperty("minPercent")]
        public double MinPercent { get; set; }

        [JsonProperty("avgPercent")]
        public double AvgPercent { get; set; }
    }

    public class Specialisation
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("openInternal")]
        public  OpenInternal OpenInternal { get; set; }

        [JsonProperty("openExternal")]
        public  OpenExternal OpenExternal { get; set; }

        [JsonProperty("reservedInternal")]
        public  ReservedInternal  ReservedInternal { get; set; }

        [JsonProperty("reservedExternal")]
        public ReservedExternal ReservedExternal { get; set; }
    }

    public class AdmissionFormSummary : BaseClass
    {

        [JsonProperty("totalForms")]
        public int TotalForms { get; set; }

        [JsonProperty("openInternal")]
        public OpenInternal OpenInternal { get; set; }

        [JsonProperty("openExternal")]
        public OpenExternal OpenExternal { get; set; }

        [JsonProperty("reservedInternal")]
        public ReservedInternal ReservedInternal { get; set; }

        [JsonProperty("reservedExternal")]
        public ReservedExternal ReservedExternal { get; set; }

        [JsonProperty("specialisation")]
        public List<Specialisation> Specialisation { get; set; }
    }
}
