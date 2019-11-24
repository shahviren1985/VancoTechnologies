namespace AdmissionForm.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Globalization; 

    /// <summary>
    ///  Manage to Convert the Data Type to other Data Type
    /// </summary>
   public static class ConvertTo
    {
        #region Methods/Functions

        /// <summary> 
        /// check for given value is null string 
        /// </summary> 
        /// <param name="readField">object to convert</param> 
        /// <returns>if value=string return string else ""</returns> 
        public static string String(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    return Convert.ToString(readField, CultureInfo.InvariantCulture);
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary> 
        /// check for given value is not double 
        /// </summary> 
        /// <param name="readField">object to convert</param> 
        /// <returns>if value=double return double else 0.0</returns> 
        public static double ToDouble(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (readField.ToString().Trim().Length == 0)
                    {
                        return 0.0;
                    }
                    else
                    {
                        return Convert.ToDouble(readField, CultureInfo.InvariantCulture);
                    }
                }
                else
                {
                    return 0.0;
                }
            }
            else
            {
                return 0.0;
            }
        }

        /// <summary> 
        /// check for given value is not decimal 
        /// </summary> 
        /// <param name="readField">object to convert</param> 
        /// <returns>if value=double return double else 0.0</returns> 
        public static decimal ToDecimal(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (readField.ToString().Trim().Length == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        decimal x;
                        if (decimal.TryParse(readField.ToString(), out x))
                        {
                            x = decimal.Round(x, ProjectConfiguration.DecimalPlace);
                            return x;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary> 
        /// check given value is boolean or null 
        /// </summary> 
        /// <param name="readField">object to convert</param> 
        /// <returns>return true else false</returns> 
        public static bool ToBoolean(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    bool x;
                    if (bool.TryParse(Convert.ToString(readField, CultureInfo.InvariantCulture), out x))
                    {
                        return x;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary> 
        /// check given value is boolean or null 
        /// </summary> 
        /// <param name="readField">object to convert</param> 
        /// <returns>return true else false</returns> 
        public static bool? ToBoolNull(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    bool x;
                    if (bool.TryParse(Convert.ToString(readField, CultureInfo.InvariantCulture), out x))
                    {
                        return x;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary> 
        /// check given value is integer or null 
        /// </summary> 
        /// <param name="readField">object to convert</param> 
        /// <returns>return integer else 0</returns> 
        public static int ToInteger(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (readField.ToString().Trim().Length == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        int toReturn;
                        if (int.TryParse(Convert.ToString(readField, CultureInfo.InvariantCulture), out toReturn))
                        {
                            return toReturn;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary> 
        /// check given value is long or null 
        /// </summary> 
        /// <param name="readField">object to convert</param> 
        /// <returns>return long else 0</returns> 
        public static long ToLong(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (readField.ToString().Trim().Length == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        long toReturn;
                        if (long.TryParse(Convert.ToString(readField, CultureInfo.InvariantCulture), out toReturn))
                        {
                            return toReturn;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary> 
        /// check given value is short or null 
        /// </summary> 
        /// <param name="readField">object to convert</param> 
        /// <returns>return short else 0</returns> 
        public static short ToShort(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (readField.ToString().Trim().Length == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        short toReturn = 0;
                        if (short.TryParse(Convert.ToString(readField, CultureInfo.InvariantCulture), out toReturn))
                        {
                            return toReturn;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        /// <summary> 
        /// check given value of date is date or null 
        /// </summary> 
        /// <param name="readField">date value to check</param> 
        /// <returns>return date if valid format else return nothing</returns> 
        public static DateTime? ToDate(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    DateTime dateReturn;
                    if (DateTime.TryParse(Convert.ToString(readField, CultureInfo.CurrentCulture), out dateReturn))
                    {
                        return Convert.ToDateTime(readField, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return null;
        }

        /// <summary> 
        /// check given value of date is date or null 
        /// </summary> 
        /// <param name="readField">date value to check</param> 
        /// <returns>return date if valid format else return nothing</returns> 
        public static DateTime ToDateNotNull(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    DateTime dateReturn;
                    if (DateTime.TryParse(Convert.ToString(readField, CultureInfo.CurrentCulture), out dateReturn))
                    {
                        return Convert.ToDateTime(readField, CultureInfo.InvariantCulture);
                    }
                    else
                    {
                        return DateTime.MinValue;
                    }
                }
            }

            return DateTime.MinValue;
        }

        /// <summary> 
        /// check given value of date is date or null 
        /// </summary> 
        /// <param name="readField">date value to check</param> 
        /// <returns>return date if valid format else return nothing</returns> 
        public static string ToDateFormat(this object readField)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    DateTime dateReturn;
                    if (DateTime.TryParse(Convert.ToString(readField, CultureInfo.CurrentCulture), out dateReturn))
                    {
                        return Convert.ToDateTime(readField, CultureInfo.InvariantCulture).GetDateTimeFormats('d', CultureInfo.InvariantCulture)[5];
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }

            return string.Empty;
        }

        /// <summary> 
        /// check given value of date is date or null 
        /// </summary> 
        /// <param name="readField">date value to check</param> 
        /// <param name="dateFormat">Date format</param> 
        /// <returns>return date if valid format else return nothing</returns> 
        public static string ToDate(this object readField, string dateFormat)
        {
            if (readField != null)
            {
                if (readField.GetType() != typeof(System.DBNull))
                {
                    if (!string.IsNullOrEmpty(dateFormat))
                    {
                        return Convert.ToDateTime(readField, CultureInfo.CurrentCulture).ToString(dateFormat, CultureInfo.InvariantCulture);
                    }

                    return Convert.ToDateTime(readField, CultureInfo.CurrentCulture).ToString(CultureInfo.CurrentCulture);
                }
            }

            return DateTime.MinValue.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary> 
        /// for save null value in database 
        /// </summary> 
        /// <param name="value">object to convert</param> 
        /// <returns>return DBNull value</returns> 
        public static object ToDBNullValue(this string value)
        {
            if (value == null | string.IsNullOrEmpty(value))
            {
                return System.DBNull.Value;
            }

            return value;
        }

        /// <summary>
        /// To check null value
        /// </summary>
        /// <param name="value">object to check</param>
        /// <returns>if null than returns DBNull.Value else returns object which is passed</returns>
        public static object ToDBNull(this object value)
        {
            if (null != value)
            {
                return value;
            }

            return DBNull.Value;
        }

        /// <summary>
        /// Convert HTML to plain text
        /// </summary>
        /// <param name="source">source as html</param>
        /// <returns>return plain text</returns>
        public static string ToStripHTML(this string source)
        {
            try
            {
                string result = null;
                string strVbCr = "\r";
                string strVbLf = "\n";
                string strVbTab = "\t";

                //// Remove HTML Development formatting
                //// Replace line breaks with space
                //// because browsers inserts space
                result = source.Replace(strVbCr, " ");

                //// Replace line breaks with space
                //// because browsers inserts space
                result = result.Replace(strVbLf, " ");

                //// Remove step-formatting
                result = result.Replace(strVbTab, string.Empty);

                //// Remove repeating spaces because browsers ignore them
                result = System.Text.RegularExpressions.Regex.Replace(result, "( )+", " ");

                //// Remove the header (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*head([^>])*>", "<head>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "(<( )*(/)( )*head( )*>)", "</head>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "(<head>).*(</head>)", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //// remove all scripts (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*script([^>])*>", "<script>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "(<( )*(/)( )*script( )*>)", "</script>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                ////result = System.Text.RegularExpressions.Regex.Replace(result,
                //// @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //// string.Empty,
                //// System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "(<script>).*(</script>)", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //// remove all styles (prepare first by clearing attributes)
                result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*style([^>])*>", "<style>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "(<( )*(/)( )*style( )*>)", "</style>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "(<style>).*(</style>)", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //// insert tabs in spaces of <td> tags
                result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*td([^>])*>", strVbTab, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //// insert line breaks in places of <BR> and <LI> tags
                result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*br( )*>", strVbCr, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*li( )*>", strVbCr, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //// insert line paragraphs (double line breaks) in place
                //// if <P>, <DIV> and <TR> tags
                result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*div([^>])*>", strVbCr + strVbCr, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*tr([^>])*>", strVbCr + strVbCr, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "<( )*p([^>])*>", strVbCr + strVbCr, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //// Remove remaining tags like <a>, links, images,
                //// comments etc - anything that's enclosed inside < >
                result = System.Text.RegularExpressions.Regex.Replace(result, "<[^>]*>", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //// replace special characters:
                result = System.Text.RegularExpressions.Regex.Replace(result, " ", " ", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                result = System.Text.RegularExpressions.Regex.Replace(result, "&bull;", " * ", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "&lsaquo;", "<", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "&rsaquo;", ">", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "&trade;", "(tm)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "&frasl;", "/", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "&lt;", "<", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "&gt;", ">", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "&copy;", "(c)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "&reg;", "(r)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //// Remove all others. More can be added, see
                //// http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = System.Text.RegularExpressions.Regex.Replace(result, "&(.{2,6});", string.Empty, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //// for testing
                ////System.Text.RegularExpressions.Regex.Replace(result,
                //// this.txtRegex.Text,string.Empty,
                //// System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //// make line breaking consistent
                result = result.Replace(strVbLf, strVbCr);

                //// Remove extra line breaks and tabs:
                //// replace over 2 breaks with 2 and over 4 tabs with 4.
                //// Prepare first to remove any whitespaces in between
                //// the escaped characters and remove redundant tabs in between line breaks
                result = System.Text.RegularExpressions.Regex.Replace(result, "(" + strVbCr + ")( )+(" + strVbCr + ")", strVbCr + strVbCr, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "(" + strVbTab + ")( )+(" + strVbTab + ")", strVbTab + strVbTab, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "(" + strVbTab + ")( )+(" + strVbCr + ")", strVbTab + strVbCr, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                result = System.Text.RegularExpressions.Regex.Replace(result, "(" + strVbCr + ")( )+(" + strVbTab + ")", strVbCr + strVbTab, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //// Remove redundant tabs
                result = System.Text.RegularExpressions.Regex.Replace(result, "(" + strVbCr + ")(" + strVbTab + ")+(" + strVbCr + ")", strVbCr + strVbCr, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //// Remove multiple tabs following a line break with just one tab
                result = System.Text.RegularExpressions.Regex.Replace(result, "(" + strVbCr + ")(" + strVbTab + ")+", strVbCr + strVbTab, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                //// Initial replacement target string for line breaks
                string breaks = strVbCr + strVbCr + strVbCr;

                //// Initial replacement target string for tabs
                string tabs = strVbTab + strVbTab + strVbTab + strVbTab + strVbTab;
                for (int index = 0; index <= result.Length - 1; index++)
                {
                    result = result.Replace(breaks, strVbCr + strVbCr);
                    result = result.Replace(tabs, strVbTab + strVbTab + strVbTab + strVbTab);
                    breaks = breaks + strVbCr;
                    tabs = tabs + strVbTab;
                }

                result = result.Replace("'", string.Empty);
                return result;
            }
            catch
            {
                return source;
            }
        }

        #endregion
    }
}
