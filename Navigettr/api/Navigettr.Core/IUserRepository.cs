using Navigettr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigettr.Core
{
    public interface IUserRepository
    {
        int ChangePassword(int Userid,string oldPassword, string newPassword);

        List<SP_forgotPassword_Result1> ForgotPassword(string mobileNumber);

        List<SP_UserLogIn_Result> UserLogIn(string UserName, string Password);

        int RegisterUser(int userId, string name, string nationality, string password, string mobileNumber, string countryCode);
    }
}
