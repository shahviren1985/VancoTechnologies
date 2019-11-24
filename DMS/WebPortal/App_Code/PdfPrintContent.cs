using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PdfPrintContent
/// </summary>
public class PdfPrintContent
{
    public string Content { get; set; }
    public string RefNumber { get; set; }

    public string Date { get; set; }

    public string Subject { get; set; }
    public string Address { get; set; }

    public string GetReferenceNumber(string serialNumber, string logPath, string configPath)
    {
        string initials = string.Empty;
        string month = string.Empty;

        try
        {
            new XmlConfiguration().GetRefNumberConfig(logPath, configPath, out initials, out month);
            string acadYear = string.Empty;

            // If month is jan to march - current year - 1, current year
            // If month is apr to dec - current year, current year  +1
            if (DateTime.Now.Month <= 2)
            {
                acadYear = (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year;
            }
            else if (DateTime.Now.Month >= 3)
            {
                acadYear = DateTime.Now.Year + "-" + (DateTime.Now.Year + 1);
            }

            return initials + "/" + acadYear + "/" + serialNumber;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}