using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using ITM.Courses.DAOBase;
using MySql.Data.MySqlClient;

namespace ITM.Courses.DAO
{
    public class SiteAnalyticsDAO
    {
        private string Q_AddSiteAnalyticsDetails = "insert into siteanalytics(time,ipaddress,browser,language,os,pagename,pagetitle,refferpage,useragent,DateCreated,IsMobile,MobileDivice,Comment,ScreenResulotion) values({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}',{10},'{11}','{12}','{13}')";

        public void AddSiteAnalyticsDetails(int time, string ipAddress, string browser, string language, string os, string pageName, string pageTitle, string refferPage, string userAgent, bool isMobile, string mobileDevice, string comments,string screenResulotion ,string cnxnString, string logPath)
        {
            try
            {
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    string cmdText = string.Format(Q_AddSiteAnalyticsDetails, time, ipAddress, browser, language, os, pageName, pageTitle, refferPage, userAgent, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff"), isMobile, mobileDevice, comments, screenResulotion);
                    Database db = new Database();
                    db.Insert(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
