namespace AA.DAO
{
    using AA.DAOBase;
    using AA.LogManager;
    using AA.Util;
    using System;
    using System.Data.Common;

    public class UserLoginsDAO
    {
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ToString();
        private Logger logger = new Logger();
        public string Q_AddUser = "insert into dmslogindetails(CollegeName,UserName,Password,LastLogin,ConnectionString,PDFFolderPath,ReleaseFilePath,LogFilePath,RoleType,ApplicationType, RedirectUrl,ExcelReportPath,RelativeConnectionString) values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}')";
        public string Q_ChangePassword = "UPDATE dmslogindetails set Password='{0}' WHERE UserName='{1}' and Password='{2}'";
        public string Q_SelectUserbyUserNamePassword = "select CollegeId, UserName, Password, CollegeName, LastLogin, ConnectionString, ReleaseFilePath, PDFFolderPath,LogFilePath, RoleType, ApplicationType, RedirectUrl, ExcelReportPath, count(*) as count from dmslogindetails where UserName='{0}' and Password='{1}'";

        public void AddUserDetails(UserLogins user, string logPath)
        {
            try
            {
                string str = string.Format(this.Q_AddUser, new object[] { user.CollegeName, ParameterFormater.FormatParameter(user.UserName), ParameterFormater.FormatParameter(user.Password), user.LastLogin, user.ConnectionString, user.PDFFolderPath, user.ReleaseFilePath, user.LogFilePath, ParameterFormater.FormatParameter(user.RoleType), ParameterFormater.FormatParameter(user.ApplicationType), user.RedirectUrl, user.ExcelReportURL, user.ConnectionString });
                new Database().Insert(str, this.connectionString, logPath);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void ChangePassword(string userName, string oldPassword, string newPassword, string logPath)
        {
            try
            {
                UserLogins logins = this.IsAuthenticated(userName, oldPassword, logPath);
                if ((logins != null) && logins.IsAuthenticated)
                {
                    string str = string.Format(this.Q_ChangePassword, ParameterFormater.FormatParameter(newPassword), ParameterFormater.FormatParameter(userName), ParameterFormater.FormatParameter(oldPassword));
                    new Database().Update(str, this.connectionString, logPath);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public UserLogins IsAuthenticated(string userName, string password, string logPath)
        {
            UserLogins logins = new UserLogins();
            try
            {
                this.logger.Debug("UserDetailsDAO", "IsAuthenticated", " Authenticating User", logPath);
                string str = string.Format(this.Q_SelectUserbyUserNamePassword, ParameterFormater.FormatParameter(userName), ParameterFormater.FormatParameter(password));
                DbDataReader reader = new Database().Select(str, this.connectionString, logPath);
                if (!reader.HasRows)
                {
                    return logins;
                }
                while (reader.Read())
                {
                    if (int.Parse(reader["count"].ToString()) == 0)
                    {
                        this.logger.Debug("UserDetailsDAO", "IsAuthenticated", " User Authentication failed ", logPath);
                    }
                    else
                    {
                        this.logger.Debug("UserDetailsDAO", "IsAuthenticated", " User  Authenticated ", logPath);
                        logins.CollegeId = int.Parse(reader["CollegeId"].ToString());
                        logins.CollegeName = reader["CollegeName"].ToString();
                        logins.UserName = ParameterFormater.UnescapeXML(reader["UserName"].ToString());
                        logins.Password = ParameterFormater.UnescapeXML(reader["Password"].ToString());
                        logins.LastLogin = reader["LastLogin"].ToString();
                        logins.ConnectionString = reader["ConnectionString"].ToString();

                        #if DEBUG
                            logins.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["con"].ConnectionString;
                        #endif

                        logins.PDFFolderPath = reader["PDFFolderPath"].ToString();
                        logins.ReleaseFilePath = reader["ReleaseFilePath"].ToString();
                        logins.LogFilePath = reader["LogFilePath"].ToString();
                        logins.RoleType = ParameterFormater.UnescapeXML(reader["RoleType"].ToString());
                        logins.ApplicationType = ParameterFormater.UnescapeXML(reader["ApplicationType"].ToString());
                        logins.RedirectUrl = reader["RedirectUrl"].ToString();
                        logins.ExcelReportURL = reader["ExcelReportPath"].ToString();
                        logins.IsAuthenticated = true;
                    }
                }
                if (!reader.IsClosed)
                {
                    reader.Close();
                }
            }
            catch (Exception exception)
            {
                this.logger.Error("UserDetailsDAO", "IsAuthenticated", " Error occurred while Authenticating User", exception, logPath);
                throw exception;
            }
            return logins;
        }
    }
}
