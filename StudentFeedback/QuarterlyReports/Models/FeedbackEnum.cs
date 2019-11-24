using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace QuarterlyReports.Models
{
    public class FeedbackEnum
    {
    }

    public enum UserTypeEnum
    {
        [Description("Teacher")]
        Teacher = 1,
        [Description("Employer")]
        Employee = 2,
        [Description("Parent")]
        Parent = 3,
        [Description("Alumini")]
        Alumini = 4
    }

    public enum FeedbakFormEnum
    {
        [Description("Very strongly disagree with the statement")]
        A1 = 1,
        [Description("Strongly disagree with the statement")]
        A2 = 2,
        [Description("Disagree with the statement")]
        A3 = 3,
        [Description("Neither agree nor disagree with the statement")]
        A4 = 4,
        [Description("Agree with the statement")]
        A5 = 5,
        [Description("Strongly agree with the statement")]
        A6 = 6,
        [Description("Very strongly agree with the statement")]
        A7 = 7
    }

    public enum CommonFeedbakFormEnum
    {
        [Description("Excellent")]
        A1 = 1,
        [Description("Very Good")]
        A2 = 2,
        [Description("Good")]
        A3 = 3,
        [Description("Average")]
        A4 = 4,
        [Description("Need to Improve")]
        A5 = 5
    }

    public static class CommonFeedbakFormEnumExtensions
    {
        public static string ToDescriptionString(this CommonFeedbakFormEnum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }

    public static class FeedbakFormEnumExtensions
    {
        public static string ToDescriptionString(this FeedbakFormEnum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}