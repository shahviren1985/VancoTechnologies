using Navigettr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigettr.Core
{
    public class UserRepository : IUserRepository
    {
        UserData objData = new UserData();

        public List<SP_UserLogIn_Result> UserLogIn(string UserName, string Password)
        {
            try
            {
                return objData.UserLogIn(UserName, Password).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<SP_forgotPassword_Result1> ForgotPassword(string mobileNumber)
        {
            try
            {
                return objData.ForgotPassword(mobileNumber).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ChangePassword(int Userid, string oldPassword, string newPassword)
        {

            int retval = objData.ChangePassword(Userid, oldPassword, newPassword);
            return retval;
        }

        public int RegisterUser(int userId, string firstName, string lastName, string password, string mobileNumber, string countryCode)
        {
            return objData.AddUser(userId, firstName, lastName, password, mobileNumber, countryCode);
        }
    }
}
