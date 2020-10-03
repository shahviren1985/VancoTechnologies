using ExcelDataExtraction.Model;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExcelDataExtraction.Services
{
    public static class ResultSummarySerializer
    {
        public static ResultSummary SerializeData(Dictionary<string,string> resultData)
        {
            return JsonConvert.DeserializeObject<ResultSummary>(JsonConvert.SerializeObject(resultData));
        }
    }
}
