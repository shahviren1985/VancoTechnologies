using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Web;

namespace QuarterlyReports
{
    public static class Extensions
    {
       
        #region ConvertToList
        /// <summary>
        /// DataTableToList ---  This function is used to Convert DataTable to List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public static List<T> ToList<T>(this DataTable dtDataCollection) where T : new()
        {
            var objList = new List<T>();

            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name.ToLower(),
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read Datatable column names and types
            var dtlFieldNames = dtDataCollection.Columns.Cast<DataColumn>().
                Select(item => new
                {
                    Name = item.ColumnName.ToLower(),
                    Type = item.DataType
                }).ToList();

            foreach (DataRow dataRow in dtDataCollection.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo propertyInfos = classObj.GetType().GetProperty(dtField.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    var field = objFieldNames.Find(x => x.Name == dtField.Name);

                    if (field != null)
                    {

                        if (propertyInfos.PropertyType == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, convertToDateTime(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToInt(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(long))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToLong(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToDecimal(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(Boolean))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToBool(dataRow[dtField.Name]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (dataRow[dtField.Name].GetType() == typeof(DateTime))
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToDateString(dataRow[dtField.Name]), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToString(dataRow[dtField.Name]), null);
                            }
                        }
                    }
                }
                objList.Add(classObj);
            }
            return objList;
        }


        /// <summary>
        /// ConvertToDateString ---   This function is used to convert object to DateString
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private static string ConvertToDateString(object dtValue)
        {
            if (dtValue == null && dtValue == DBNull.Value)
                return string.Empty;

            //return SpecialDateTime.ConvertDate(Convert.ToDateTime(date));
            return Convert.ToString(dtValue);
        }


        /// <summary>
        /// ConvertToString --- This function is used to convert object to string              
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private static string ConvertToString(object strValue)
        {
            // return Convert.ToString(HelperFunctions.ReturnEmptyIfNull(value));
            string returnValue = string.Empty;
            if (strValue != null && strValue != DBNull.Value)
                returnValue = Convert.ToString(strValue);
            return returnValue;
        }


        /// <summary>
        /// ConvertToInt --- This function is used to convert object to Int            
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private static int ConvertToInt(object iValue)
        {
            //return Convert.ToInt32(HelperFunctions.ReturnZeroIfNull(value));
            int returnValue = 0;
            if (iValue != null && iValue != DBNull.Value)
                returnValue = Convert.ToInt32(iValue);
            return returnValue;
        }


        /// <summary>
        /// ConvertToLong ---This function is used to convert object to Long
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private static long ConvertToLong(object lngValue)
        {
            //return Convert.ToInt64(HelperFunctions.ReturnZeroIfNull(value));
            Int64 returnValue = 0;
            if (lngValue != null && lngValue != DBNull.Value)
                returnValue = Convert.ToInt64(lngValue);
            return returnValue;
        }

        /// <summary>
        /// ConvertToDecimal --- This function is used to convert object to Decimal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private static decimal ConvertToDecimal(object decValue)
        {
            //return Convert.ToDecimal(HelperFunctions.ReturnZeroIfNull(value));
            decimal returnValue = 0;
            if (decValue != null && decValue != DBNull.Value)
                returnValue = Convert.ToDecimal(decValue);
            return returnValue;
        }


        /// <summary>
        /// DateTime --- This function is used to convert object to convertToDateTime
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private static DateTime? convertToDateTime(object dtValue)
        {
            // return Convert.ToDateTime(HelperFunctions.ReturnDateTimeMinIfNull(date));
            DateTime? returnValue = null;
            if (dtValue != null && dtValue != DBNull.Value)
                returnValue = Convert.ToDateTime(dtValue);
            return returnValue;
        }



        /// <summary>
        /// ConvertToBool ---This function is used to convert object to Bool
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private static bool ConvertToBool(object blValue)
        {
            //return Convert.ToDecimal(HelperFunctions.ReturnZeroIfNull(value));
            bool returnValue = false;
            if (blValue != null && blValue != DBNull.Value)
                returnValue = Convert.ToBoolean(blValue);
            return returnValue;
        }
        #endregion ConvertToList
    }
}