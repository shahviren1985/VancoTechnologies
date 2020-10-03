using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExcelDataExtraction.Services
{
    public static class StringConverter
    {
        /// <summary>
        /// String proceeding where \s\s and \n is splitters 
        /// </summary>
        /// <param name="stringToConvert">Initial string</param>
        /// <returns>Key value pairs of student details</returns>
        public static Dictionary<string, string> StringProceed(string stringToConvert)
        {
            stringToConvert = stringToConvert.Trim();
            var keyValuePairs = new Dictionary<string, string>();
            var regexSpace = new Regex(@"\s\s+");
            var regexEnter = new Regex(@"\n+");
            stringToConvert = regexSpace.Replace(stringToConvert, @"_");
            stringToConvert = regexEnter.Replace(stringToConvert, @"_");

            var keyValueArray = stringToConvert.Split('_');

            foreach (var keyValue in keyValueArray)
            {
                keyValue.Trim();
                var keyValuePair = keyValue.Split(':');
                keyValuePairs.Add(keyValuePair[0].Trim(), keyValuePair[1].Trim());
            }
            return keyValuePairs;
        }
        /// <summary>
        /// Method for extracting college info
        /// </summary>
        /// <param name="data"></param>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetCollegeInfo(string data, Dictionary<string, string> keyValuePairs)
        {
            if (data.Contains("College"))
            {
                var collegeIndex = data.IndexOf("College");
                data = data.Substring(collegeIndex);
                var collegeDataArray = data.Split(':');
                keyValuePairs.Add("College Code", collegeDataArray[1].Trim());
                keyValuePairs.Add("College Name", collegeDataArray[2].Trim());
                return keyValuePairs;
            }
            return keyValuePairs;
        }

        public static List<string> ExtractProgramNames(string data)
        {
            data = data.Trim();
            var regex = new Regex(@"\n");
            data = regex.Replace(data, @"_");
            data = data.Replace(") ", ")_");
            var programNamesArray = data.Split('_').ToList();
            programNamesArray.RemoveAt(programNamesArray.Count - 1);
            return programNamesArray;
        }

        public static string GetTotalCredits(string data)
        {
            data = data.Trim();
            if (data.Contains("Total"))
            {
                var collegeIndex = data.IndexOf("Total");
                return data.Substring(collegeIndex);
            }
            else return data;
        }

        public static string SeparateGrandTotal(string data)
        {
            data = data.Trim();
            if (data.Contains("Grand"))
            {
                var finalArray = data.Split(':');
                var stringToReturn = finalArray[1].Trim();

                var arr = stringToReturn.Split('/');

                var str = $"Grand Total Obtained:{arr[0]}";
                str += $"_Grand Total Marks:{arr[1]}";

                return str;
            }
            else return data;
        }
    }
}
