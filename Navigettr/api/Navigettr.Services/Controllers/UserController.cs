using Navigettr.Core;
using Navigettr.Data;
using Navigettr.Services.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Navigettr.Services.Controllers
{
    public class UserController : ApiController
    {
        private static readonly IUserRepository _repository = new UserRepository();
        UserData objData = new UserData();
        NavigettrEntities Objdb = new NavigettrEntities();

        [Route("api/user/register")]
        [HttpPost]
        [HttpOptions]
        public HttpResponseMessage RegisterUser([FromBody]RegisterUserDetails userDetails)
        {
            //userDetails.MobileNumber = userDetails.CountryCode + userDetails.MobileNumber;
            int data = _repository.RegisterUser(userDetails.Id, userDetails.Name, userDetails.Nationality, userDetails.Mpin, userDetails.MobileNumber, userDetails.CountryCode);

            if (data > 0)
            {
                string userMessage = "Successfully registered the user.";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = userMessage, user = userDetails });
            }
            else if (data == -1 || data == -100)
            {
                string userMessage = "Mobile number already registered. Please try registering using different mobile number.";
                return Request.CreateResponse(HttpStatusCode.OK, new { message = userMessage });
            }

            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, new Exception("Unable to register the user."));
        }

        [Route("api/user/login")]
        [HttpPost]
        [HttpOptions]
        public HttpResponseMessage Login([FromBody]UserDetails jsondata)
        {
            try
            {
                string yourJson = "";
                var data = _repository.UserLogIn(jsondata.mobileNumber, jsondata.mPin);

                if (data.Count > 0 && data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    //return Request.CreateResponse(HttpStatusCode.NotFound, "No Data found ");
                    yourJson = "Invalid Mobile number or MPIN";
                    HttpError myCustomError = new HttpError(yourJson);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, myCustomError);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }
        }

        [Route("api/user/changeMPin")]
        [HttpPost]
        [HttpOptions]
        public HttpResponseMessage ChangePassword([FromBody]UserDetails jsondata)
        {
            try
            {
                string yourJson = "";
                var data = _repository.ChangePassword(jsondata.userId, jsondata.oldMPin, jsondata.newMPin);

                if (Convert.ToInt32(data) == 1)
                {
                    yourJson = "Successfully changed the MPIN";
                    return Request.CreateResponse(HttpStatusCode.OK, new { message = yourJson });
                }
                else
                {
                    yourJson = "Either Incorrect userid or old MPIN, unable to change the password";
                    HttpError myCustomError = new HttpError(yourJson);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, myCustomError);
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("api/user/forgotPassword")]
        [HttpPost]
        [HttpOptions]
        public HttpResponseMessage ForgotPassword(string mobileNumber)
        {
            try
            {
                string yourJson = "";
                var data = _repository.ForgotPassword(mobileNumber);

                // Send an email notification in case we have got the user id 
                if (data != null && data.Count > 0 && Convert.ToInt32((data[0] as SP_forgotPassword_Result1).UserId) > 0)
                {
                    string link = string.Format(ConfigurationManager.AppSettings["ChangePasswordLink"], (data[0] as SP_forgotPassword_Result1).UserId);
                    return Request.CreateResponse(HttpStatusCode.OK, new { message = link });
                }
                else
                {
                    yourJson = "Mobile number doesn't exist";
                    HttpError myCustomError = new HttpError(yourJson);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, myCustomError);
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
