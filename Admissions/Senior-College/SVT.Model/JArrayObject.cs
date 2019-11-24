using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVT.Business.Model
{
    public class JArrayObject : BaseClass
    {
        [JsonProperty("data")]
        public JArray Data { get; set; }

        [JsonProperty("Count")]
        public int Count { get; set; }

    }
}
