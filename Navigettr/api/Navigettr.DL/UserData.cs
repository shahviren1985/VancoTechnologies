using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigettr.Data
{
    public class UserData
    {
        NavigettrEntities Objdb = new NavigettrEntities();
        public int ChangePassword(int Userid, string oldPassword, string newPassword)
        {
            ObjectParameter StatusResult = new ObjectParameter("StatusResult", typeof(string));
            int retval = Objdb.SP_changePassword(Userid, oldPassword, newPassword, StatusResult);
            return retval;
        }

        public List<SP_forgotPassword_Result1> ForgotPassword(string Email)
        {
            try
            {
                return Objdb.SP_forgotPassword(Email).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SP_UserLogIn_Result> UserLogIn(string UserName, string Password)
        {
            try
            {
                return Objdb.SP_UserLogIn(UserName, Password).ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddUser(int userId, string firstName, string lastName, string password, string mobileNumber, string userName, string emailAddress)
        {
            try
            {
                ObjectParameter statusResult = new ObjectParameter("StatusResult", typeof(string));
                ObjectParameter statusResultUserId = new ObjectParameter("StatusResultUserId", typeof(string));
                return Objdb.SP_UserDetails_InsertUpdate(userId, "User", userName, password, 2, "Active", firstName, lastName, mobileNumber, emailAddress, statusResult, statusResultUserId);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
