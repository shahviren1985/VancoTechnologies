using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVT.Business.Model
{
    public class Department
    {
        [JsonProperty("department")]
        public string DepartmentName { get; set; }

        [JsonProperty("specialisations")]
        public List<string> Specialisations { get; set; }
    }
}
