using Navigettr.Core;
using Navigettr.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;

namespace Navigettr.Services.Controllers
{
    public class UserController : ApiController
    {
        private static readonly IUserRepository _repository = new UserRepository();
        UserData objData = new UserData();
        NavigettrEntities Objdb = new NavigettrEntities();

        public class UserDetails
        {
            public int userId { get; set; }
            public string username { get; set; }
            public string oldPassword { get; set; }
            public string newPassword { get; set; }
            public string email { get; set; }
            public string password { get; set; }
        }

        [Route("api/login")]
        [HttpPost]
        [HttpOptions]
        public HttpResponseMessage Login([FromBody]UserDetails jsondata)
        {
            try
            {
                string yourJson = "";
                var data = _repository.UserLogIn(jsondata.username, jsondata.password);

                if (data.Count > 0 && data != null)
                {

                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    //return Request.CreateResponse(HttpStatusCode.NotFound, "No Data found ");
                    yourJson = "Unable to login at this moment";
                    HttpError myCustomError = new HttpError(yourJson);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, myCustomError);
                }


            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }

        }


        [Route("api/changePassword")]
        [HttpPost]
        [HttpOptions]
        public HttpResponseMessage ChangePassword([FromBody]UserDetails jsondata)
        {
            try
            {
                string yourJson = "";
                var data = _repository.ChangePassword(jsondata.userId, jsondata.oldPassword, jsondata.newPassword);

                if (Convert.ToInt32(data) == 1)
                {
                    yourJson = "Successfully changed the password";
                    return Request.CreateResponse(HttpStatusCode.OK, new { message = yourJson });
                }
                else
                {
                    yourJson = "Either Incorrect userid or old password, unable to change the password";
                    HttpError myCustomError = new HttpError(yourJson);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, myCustomError);
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/forgotPassword")]
        [HttpPost]
        [HttpOptions]
        public HttpResponseMessage ForgotPassword(string emailId)
        {
            try
            {
                string yourJson = "";
                var data = _repository.ForgotPassword(emailId);

                // Send an email notification in case we have got the user id 
                if (data != null && data.Count > 0 && Convert.ToInt32((data[0] as SP_forgotPassword_Result1).UserId) > 0)
                {
                    EmailData email = new EmailData();
                    email.ToEmailAdress = emailId;
                    email.Body = string.Format(ConfigurationManager.AppSettings["ChangePasswordLink"], (data[0] as SP_forgotPassword_Result1).UserId);
                    email.EnableSSL = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);
                    email.FromEmailAddress = ConfigurationManager.AppSettings["MailUserName"];
                    email.FromName = ConfigurationManager.AppSettings["MailName"];
                    email.FromPassword = ConfigurationManager.AppSettings["MailPassword"];
                    email.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                    email.Subject = "Navigettr - Reset your password";
                    email.HostName = ConfigurationManager.AppSettings["Host"];

                    EmailData.SendEmail(email);
                    yourJson = "An email is sent to your registered email address to reset the password";
                    return Request.CreateResponse(HttpStatusCode.OK, new { message = yourJson });
                }
                else
                {
                    yourJson = "Email address doesn't exist";
                    HttpError myCustomError = new HttpError(yourJson);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, myCustomError);
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/register")]
        [HttpPost]
        [HttpOptions]
        public HttpResponseMessage RegisterUser([FromBody]RegisterUserDetails userDetails)
        {
            userDetails.UserName = userDetails.MobileNumber;
            userDetails.MobileNumber = userDetails.CountryCode + userDetails.MobileNumber;
            int data = _repository.RegisterUser(userDetails.Id, userDetails.FirstName, userDetails.LastName, userDetails.Password, userDetails.MobileNumber, userDetails.UserName, userDetails.EmailAddress);

            if (data > 0)
            {
                string userMessage = "Successfully registered the user.";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = userMessage });
            }
            else if (data == -1 || data == -100)
            {
                string userMessage = "Username already exist. Please try registering using different mobile number or try forgot password from website.";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = userMessage });
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new Exception("Unable to register the user."));
        }
    }

    public class RegisterUserDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryCode { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
