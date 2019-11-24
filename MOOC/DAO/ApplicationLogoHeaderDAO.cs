using ITM.Courses.DAOBase;
using ITM.Courses.Utilities;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;

namespace ITM.Courses.DAO
{
    public class ApplicationLogoHeaderDAO
    {
        public string Q_AddLogoHeaderDetails = "Insert into applicationlogoheader(collegeName, logoimage,logoimagepath,logotext,DateCreated) values('{0}','{1}','{2}','{3}','{4}')";
        public string Q_GetLogoHeaderbyCollege = "select * from applicationlogoheader where collegeName='{0}'";
        public string Q_UpdateLogoHeader = "Update applicationlogoheader set logoimage='{0}',logoimagepath='{1}',logotext ='{2}' where id={3}";

        public void AddLogoHeaderDetails(string college, string logoImage, string logoImagePath, string logoText, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_AddLogoHeaderDetails, college, logoImage, logoImagePath,
                   ParameterFormater.FormatParameter(logoText), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff"));
                Database db = new Database();

                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    db.Insert(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateLogoHeader(int id, string logoImage, string logoImagePath, string logoText, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_UpdateLogoHeader, logoImage, logoImagePath, ParameterFormater.FormatParameter(logoText), id);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    db.Update(cmdText, logPath, con);
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ApplicationLogoHeader GetLogoHeaderbyCollege(string collegeName, string cnxnString, string logPath)
        {
            try
            {
                string cmdText = string.Format(Q_GetLogoHeaderbyCollege, collegeName);
                Database db = new Database();
                using (MySqlConnection con = new MySqlConnection(cnxnString))
                {
                    con.Open();
                    using (DbDataReader reader = db.Select(cmdText, logPath, con))
                    {
                        if (reader.HasRows)
                        {
                            ApplicationLogoHeader appLogoHeader = new ApplicationLogoHeader();

                            while (reader.Read())
                            {
                                appLogoHeader.Id = Convert.ToInt32(reader["id"]);
                                appLogoHeader.CollegeName = reader["collegeName"].ToString();
                                appLogoHeader.LogoImageName = reader["logoimage"].ToString();
                                appLogoHeader.LogoImagePath = reader["logoimagepath"].ToString();
                                appLogoHeader.LogoText = ParameterFormater.UnescapeXML(reader["logotext"].ToString());
                            }

                            if (!reader.IsClosed)
                            {
                                reader.Close();
                            }

                            return appLogoHeader;
                        }
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }
    }
}
