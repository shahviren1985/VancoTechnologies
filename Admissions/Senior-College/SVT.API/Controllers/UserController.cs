using SVT.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SVT.API.Controllers
{
    public class UserController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Login(UserDetails user)
        {
            if (user == null || string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                return Request.CreateResponse(HttpStatusCode.OK, new UserDetails() { IsSuccess = false, ErrorMessage = "Invalid Data" });
            try
            {
                string userName = System.Web.Configuration.WebConfigurationManager.AppSettings["userName"];
                string password = System.Web.Configuration.WebConfigurationManager.AppSettings["password"];
                if (user.UserName.Equals(userName) && user.Password.Equals(password))
                {
                    UserDetails userDetails = new UserDetails();
                    userDetails.IsSuccess = true;
                    userDetails.SuccessMessage = "Success";
                    userDetails.FirstName = "SVT";
                    userDetails.LastName = "Admin";
                    userDetails.UserId = 1;
                    userDetails.MobileNumber = "1234567890";
                    return Request.CreateResponse(HttpStatusCode.OK, userDetails);

                }
                else
                    return Request.CreateResponse(HttpStatusCode.OK, new UserDetails() { IsSuccess = false, ErrorMessage = "Invalid Credentials" });

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
