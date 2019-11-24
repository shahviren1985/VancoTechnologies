namespace AdmissionForm.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    /// <summary>
    /// Manage the Twitter Open Authentication 
    /// </summary>
   public static class Extensions
    {
        #region Variable and Properties

        /// <summary>
        /// Split String, based on provided character
        /// </summary>
        /// <param name="value">main string</param>
        /// <param name="sap">string to split with</param>
        /// <param name="returnType">return type</param>
        /// <returns>return the array of the split string</returns>
        public static System.Collections.ArrayList Split(this string value, char sap, int returnType)
        {
            System.Collections.ArrayList l = new System.Collections.ArrayList();

            char[] charSeparators = new char[] { sap }; //// [,] or [-] or what ever
            string[] result = value.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);

            switch (returnType)
            {
                case 1: ////int
                    foreach (string r in result)
                    {
                        l.Add(r.ToInteger());
                    }

                    break;

                case 2: ////string
                    foreach (string r in result)
                    {
                        l.Add(r);
                    }

                    break;
                case 3: ////date
                    foreach (string r in result)
                    {
                        l.Add(r.ToDate());
                    }

                    break;
                default:
                    break;
            }

            return l;
        }

        /// <summary>
        /// check for the Is Empty
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>return boolean</returns>
        public static bool IsEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Check for Is null or not
        /// </summary>
        /// <param name="source">source value</param>
        /// <returns>Return boolean</returns>
        public static bool IsNull(this object source)
        {
            return source == null;
        }

        /// <summary>
        /// Match Regex Value
        /// </summary>
        /// <param name="value">Given Value</param>
        /// <param name="pattern">Pattern Value</param>
        /// <returns>Return boolean</returns>
        public static bool Match(this string value, string pattern)
        {
            Regex regex = new Regex(pattern);
            return regex.IsMatch(value);
        }

        /// <summary>
        /// Check Date is between from and to date  or not
        /// </summary>
        /// <param name="date">date value</param>
        /// <param name="fromDate">from Date</param>
        /// <param name="toDate">To Date</param>
        /// <returns>Return Boolean</returns>
        public static bool Between(this DateTime date, DateTime fromDate, DateTime toDate)
        {
            return date.Ticks >= fromDate.Ticks && date.Ticks <= toDate.Ticks;
        }

        /// <summary>
        /// Converts string to enum object
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="value">String value to convert</param>
        /// <returns>Returns enum object</returns>
        public static T ToEnum<T>(this string value)
            where T : struct
        {
            return (T)System.Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Checks Null-able value and return false/Null = No else Yes
        /// </summary>
        /// <param name="value">Null-able value to check</param>
        /// <returns>returns string</returns>
        public static string ToYesNo(this bool? value)
        {
            return value.ToYesNo("No");
        }

        /// <summary>
        /// Checks Null-able value and return false = No, true = Yes, null = passed value 
        /// </summary>
        /// <param name="value">Null-able value to check</param>
        /// <param name="nullableValue">Value to be return in case of null</param>
        /// <returns>returns string</returns>
        public static string ToYesNo(this bool? value, string nullableValue)
        {
            string q;

            if (value == null)
            {
                q = nullableValue;
            }
            else
            {
                q = ((bool)value).ToYesNo();
            }

            return q;
        }

        /// <summary>
        /// checks and return  false = No, true = Yes
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>returns string</returns>
        public static string ToYesNo(this bool value)
        {
            string q;

            if (value)
            {
                q = "Yes";
            }
            else
            {
                q = "No";
            }

            return q;
        }

        /// <summary>
        /// Return Customize Date Format
        /// </summary>
        /// <param name="value">value to check</param>
        /// <param name="dateFormat">date format</param>
        /// <param name="nullvalue">value for null</param>
        /// <returns>return String</returns>
        public static string ToCustomDateFormat(this DateTime? value, string dateFormat, string nullvalue)
        {
            if (value != null && value.HasValue)
            {
                return value.Value.ToCustomDateFormat(dateFormat);
            }
            else
            {
                return nullvalue;
            }
        }

        /// <summary>
        /// Return Customize Date Format
        /// </summary>
        /// <param name="value">value to check</param>
        /// <param name="dateFormat">date format</param>
        /// <returns>return String</returns>
        public static string ToCustomDateFormat(this DateTime? value, string dateFormat)
        {
            return value.ToCustomDateFormat(dateFormat, string.Empty);
        }

        /// <summary>
        /// Return Customize Date Format
        /// </summary>
        /// <param name="value">value to check</param>
        /// <param name="timeFormat">time format</param>
        /// <param name="nullvalue">value for null</param>
        /// <returns>return String</returns>
        public static string ToCustomTimeFormat(this TimeSpan? value, string timeFormat, string nullvalue)
        {
            if (value != null && value.HasValue)
            {
                return value.Value.ToString(timeFormat);
            }
            else
            {
                return nullvalue;
            }
        }

        /// <summary>
        /// Return Customize Time Format
        /// </summary>
        /// <param name="value">value to check</param>
        /// <param name="timeFormat">time format</param>
        /// <returns>return String</returns>
        public static string ToCustomTimeFormat(this TimeSpan? value, string timeFormat)
        {
            return value.ToCustomTimeFormat(timeFormat, string.Empty);
        }

        /// <summary>
        /// Return Customize Date Format
        /// </summary>
        /// <param name="value">value to check</param>
        /// <param name="dateFormat">date format</param>
        /// <returns>return String</returns>
        public static string ToCustomDateFormat(this DateTime value, string dateFormat)
        {
            try
            {
                return value.ToDate(dateFormat);
            }
            catch
            {
                return value.ToDate(ProjectConfiguration.DateTimeFormat);
            }
        }

        /// <summary>
        /// Return Standard Date Format
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>return String</returns>
        public static string ToSystemDate(this DateTime? value)
        {
            string q = string.Empty;
            if (value != null)
            {
                q = value.Value.ToSystemDate();
            }

            return q;
        }

        /// <summary>
        /// Return Standard Date time Format
        /// </summary>
        /// <param name="value">Date Time Value</param>
        /// <returns>Format Date string</returns>
        public static string ToSystemDateTime(this DateTime? value)
        {
            string q = string.Empty;
            if (value.HasValue)
            {
                q = value.Value.ToSystemDateTime();
            }

            return q;
        }

        /// <summary>
        /// Return Standard Date Format
        /// </summary>
        /// <param name="value">Date Time Value</param>
        /// <returns>Format Date string</returns>
        public static string ToSystemDate(this DateTime value)
        {
            return value.ToDate(ProjectConfiguration.DateFormat);
        }

        /// <summary>
        /// Return Standard Date time Format
        /// </summary>
        /// <param name="value">Date Time Value</param>
        /// <returns>Format Date string</returns>
        public static string ToSystemDateTime(this DateTime value)
        {
            return value.ToDate(ProjectConfiguration.DateTimeFormat);
        }

        /// <summary>
        /// Set Max String length
        /// </summary>
        /// <param name="value">current value</param>
        /// <param name="len">argument one</param>
        /// <returns>return format string</returns>
        public static string SetMaxStringlength(this string value, int len)
        {
            if (value.Length > len)
            {
                value = value.Substring(0, len) + "...";
            }

            return value;
        }

        /// <summary>
        /// Set Argument to current string
        /// </summary>
        /// <param name="value">current value</param>
        /// <param name="args">arguments Value</param>
        /// <returns>return format string</returns>
        public static string SetArguments(this string value, params object[] args)
        {
            return string.Format(value, args);
        }

        /// <summary>
        /// Distinct By Extended Method
        /// </summary>
        /// <typeparam name="t">class entity</typeparam>
        /// <param name="list">List Value</param>
        /// <param name="propertySelector">distinct property</param>
        /// <returns>Distinct List Value</returns>
        public static IEnumerable<t> DistinctBy<t>(this IEnumerable<t> list, Func<t, object> propertySelector)
        {
            return list.GroupBy(propertySelector).Select(x => x.First());
        }

        //public static string GetDisplayName(this Enum enumType)
        //{
        //    return EnumHelper.GetName(enumType.GetType(), enumType.ToString());
        //}

        /// <summary>
        /// Return Standard Amount Format
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>return String</returns>
        public static string ToSystemAmount(this decimal? value)
        {
            string q = string.Empty;
            if (value != null)
            {
                q = value.Value.ToSystemAmount();
            }

            return q;
        }

        /// <summary>
        /// Return Standard Date Format
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>return String</returns>
        public static string ToSystemAmount(this decimal value)
        {
            string q = string.Empty;
            if (value != null)
            {
                q = value.ToString(ProjectConfiguration.NumberFormatWithSpace);
            }

            return q;
        }

        /// <summary>
        /// Return Standard Amount Format
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>return String</returns>
        public static string ToBitCoinAmount(this decimal? value)
        {
            string q = string.Empty;
            if (value != null)
            {
                q = value.Value.ToBitCoinAmount();
            }

            return q;
        }

        /// <summary>
        /// Return Standard Date Format
        /// </summary>
        /// <param name="value">value to check</param>
        /// <returns>return String</returns>
        public static string ToBitCoinAmount(this decimal value)
        {
            string q = string.Empty;
            if (value != null)
            {
                q = value.ToString(ProjectConfiguration.BitCoinFormat);
            }

            return q;
        }
        #endregion
    }
}
