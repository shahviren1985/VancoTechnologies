using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVT.Business.Model
{
    public class Division
    {
        [JsonProperty("division")]
        public string DivisionName { get; set; }

        [JsonProperty("specialisations")]
        public List<string> Specialisations { get; set; }
    }
}
