using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITM.LogManager;
using ITM.Utilities;
using ITM.DAOBase;
using System.Data.Common;

namespace ITM.DAO
{
    public class UserLogin
    {
        Logger logger = new Logger();

        #region Properties
        public int CollegeId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CollegeName { get; set; }
        public string LastLogin { get; set; }
        public string ConnectionString { get; set; }
        public string ReleaseFilePath { get; set; }
        public string PDFFolderPath { get; set; }
        public bool IsAuthenticated { get; set; }
        public string LogFilePath { get; set; }
        public string RoleType { get; set; }
        public string ApplicationType { get; set; }
        public string RedirectUrl { get; set; }
        #endregion

        public UserLogin UserAuthentication(string userName, string password, string logPath)
        {
            string Q_SelectUserbyUserNamePassword = "select CollegeId, UserName, Password, CollegeName, LastLogin, ConnectionString, ReleaseFilePath, PDFFolderPath,LogFilePath, RoleType, ApplicationType, RedirectUrl, count(*) as count from logindetails where UserName='{0}' and Password='{1}'";

            UserLogin user = new UserLogin();

            try
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ToString();

                logger.Debug("UserDetailsDAO", "IsAuthenticated", " Authenticating User", logPath);

                string cmdText = string.Format(Q_SelectUserbyUserNamePassword, ParameterFormater.FormatParameter(userName), ParameterFormater.FormatParameter(password));

                Database db = new Database();

                DbDataReader reader = db.Select(cmdText, connectionString, logPath);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int count = int.Parse(reader["count"].ToString());
                        if (count == 0)
                        {
                            logger.Debug("UserDetailsDAO", "IsAuthenticated", " User Authentication failed ", logPath);
                            //return false;
                        }
                        else
                        {
                            logger.Debug("UserDetailsDAO", "IsAuthenticated", " User  Authenticated ", logPath);
                            user.CollegeId = int.Parse(reader["CollegeId"].ToString());
                            user.CollegeName = reader["CollegeName"].ToString();
                            user.UserName = reader["UserName"].ToString();
                            user.Password = reader["Password"].ToString();
                            user.LastLogin = reader["LastLogin"].ToString();
                            user.ConnectionString = reader["ConnectionString"].ToString();
                            user.PDFFolderPath = reader["PDFFolderPath"].ToString();
                            user.ReleaseFilePath = reader["ReleaseFilePath"].ToString();
                            user.LogFilePath = reader["LogFilePath"].ToString();

                            user.RoleType = reader["RoleType"].ToString();
                            user.ApplicationType = reader["ApplicationType"].ToString();
                            user.RedirectUrl = reader["RedirectUrl"].ToString();
                            user.IsAuthenticated = true;
                        }
                    }

                    if (!reader.IsClosed)
                    {
                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("UserDetailsDAO", "IsAuthenticated", " Error occurred while Authenticating User", ex, logPath);
                throw ex;
            }

            return user;
        }
    }
}
