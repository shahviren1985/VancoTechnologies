using Newtonsoft.Json;
using RoshniAppWebApi.Helpers;
using RoshniAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace RoshniAppWebApi.Controllers
{
    public class UserController : ApiController
    {
        DatabaseOperations op;
        public UserController()
        {
            op = new DatabaseOperations();
        }

        [HttpGet]
        public HttpResponseMessage test()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "It Works");
        }

        [HttpPost]
        public HttpResponseMessage RegisterUser(UserDetails model)
        {
            try
            {
                if (model == null)
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });

                if (op.CheckIfMobileNumberExist(model.MobileNumber) > 0)
                {
                    op.UpdateMobileNumber(model.MobileNumber);
                    //return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "User already registered with this mobilenumber" });
                }
                //else
                //{
                    bool isInserted = op.RegisterUser(model) > 0;
                    if (!isInserted)
                        return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });

                    SendOTP(model.MobileNumber);
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Record inserted successfully" });
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateUser(UserDetails model)
        {
            try
            {
                if (model == null)
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });

                bool isInserted = op.UpdateImegencyContact(model) > 0;
                if (!isInserted)
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });
                else
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Record inserted successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool SendOTP(string mobileNumber)
        {
            try
            {

                int userId = op.CheckIfMobileNumberExist(mobileNumber);

                //For generating OTP
                Random r = new Random();
                string OTP = r.Next(1000, 9999).ToString();
                //Need to replace Original SentOTP() 
                string message = string.Format(ConfigurationManager.AppSettings["OTPMessage"], OTP);
                OTPLogs oTPLogs = new OTPLogs();
                oTPLogs.UserId = userId;
                oTPLogs.SentToMobileNumber = mobileNumber;
                oTPLogs.IsValidated = false;
                oTPLogs.OtpMessage = message;
                oTPLogs.OtpCode = OTP;
                oTPLogs.SentAt = DateTime.Now;
                bool isInserted = op.InsertOTPLogs(oTPLogs) > 0;

                if (isInserted)
                    SentOTP(message, mobileNumber);
                /*if (!isInserted)
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });
                else
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "OTP sent successfully" });
                */
                return isInserted;
            }
            catch (Exception ex)
            {
                // Log
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage ValidateOTP(string mobileNumber, string otpCode)
        {
            try
            {
                if (string.IsNullOrEmpty(mobileNumber) || string.IsNullOrEmpty(otpCode))
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });

                //Need to Validate OTP Third Party() 
                if (op.ValidateOTP(mobileNumber, otpCode))
                {
                    bool isInserted = op.updateOTPDetails(mobileNumber, otpCode) > 0;
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "OTP Validated successfully" });
                }
                else
                {
                    bool isInserted = op.updateOTPDetails(mobileNumber, otpCode) > 0;
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage SendSOSMessage(string mobileNumber, string latitude, string longitude)
        {
            try
            {
                if (string.IsNullOrEmpty(mobileNumber))
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });

                UserDetails user = op.GetUserDetails(mobileNumber);
                if (user == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Mobilenumber does not exist" });
                }
                else
                {
                    int userId = op.CheckIfMobileNumberExist(mobileNumber);
                    string sosMessage = string.Format(ConfigurationManager.AppSettings["EmergencyMessage"], user.MobileNumber, latitude, longitude);
                    //string sosMessage = "Hey, My mobile number is " + mobileNumber + ". I need your urgent help. My Location is Latitude = " + latitude + ", Longitude = " + longitude + ".";

                    SOSLogs sosLogs = new SOSLogs();
                    sosLogs.UserId = userId;
                    sosLogs.SosMessage = sosMessage;
                    sosLogs.SentToMobileNumber = string.Empty;
                    sosLogs.SentToMobileNumber += !string.IsNullOrEmpty(user.Emergency1) ? user.Emergency1 + "," : string.Empty;
                    sosLogs.SentToMobileNumber += !string.IsNullOrEmpty(user.Emergency2) ? user.Emergency2 + "," : string.Empty;
                    sosLogs.SentToMobileNumber += !string.IsNullOrEmpty(user.Emergency3) ? user.Emergency3 + "," : string.Empty;
                    sosLogs.SentToMobileNumber += !string.IsNullOrEmpty(user.Emergency4) ? user.Emergency4 + "," : string.Empty;
                    sosLogs.SentToMobileNumber += !string.IsNullOrEmpty(user.Emergency5) ? user.Emergency5 + "," : string.Empty;
                    sosLogs.SentAt = DateTime.Now;
                    op.InsertSOSLogs(sosLogs);
                    //SendSOSMessage(sosLogs.SentToMobileNumber, latitude, longitude);
                    try
                    {
                        SentSOSMessage(sosMessage, sosLogs.SentToMobileNumber);
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, new BaseClass() { IsSuccess = false, SuccessMessage = ex.Message });

                    }
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "SOS sent successfully" });
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage SendMessage(string emailId, string chatMessage)
        {
            try
            {
                if (!bool.Parse(ConfigurationManager.AppSettings["IsChatLive"]))
                {
                    emailId = ConfigurationManager.AppSettings["DummyMessageEmail"];
                }

                if (string.IsNullOrEmpty(emailId))
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });
                else
                {
                    bool isValidAddress = EmailHelper.IsValidEmailAddress(emailId);
                    if (!isValidAddress)
                        return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Invalid Data" });


                    string subject = "Counseling Chat Message";
                    string body = chatMessage;
                    EmailHelper.SendMailMessage(emailId, ConfigurationManager.AppSettings["FromEmail"], null, null, subject, body, null);
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Email Sent Successfully" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetNGOList()
        {
            try
            {
                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/NGOList.Json");
                string allText = System.IO.File.ReadAllText(filePath);
                List<NGOList> response = JsonConvert.DeserializeObject<List<NGOList>>(allText);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool SentOTP(string message, string mobileNumber)
        {
            try
            {
                message = HttpUtility.UrlEncode(message);
                using (var wb = new WebClient())
                {
                    byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                    {"apikey" , ConfigurationManager.AppSettings["SMSKey"]},
                    {"numbers" , mobileNumber},
                    {"message" , message},
                    {"sender" , "ROSHNI"}
                });

                    string result = System.Text.Encoding.UTF8.GetString(response);
                    // Log result
                    File.WriteAllText(Path.Combine(ConfigurationManager.AppSettings["LogPath"], "OTP_" + mobileNumber + "_" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".json"), result);
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log
                return false;
            }
            /*try
            {
                string result = string.Empty;
                String url = "https://api.textlocal.in/send/?apikey=bJoR6cXhY6o-cqh74qelXo9WbTnOGKW7p8NGyvIbwq&numbers=" + mobileNumber + "&message=" + message + "&sender=ROSHNI";
                StreamWriter myWriter = null;
                HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

                objRequest.Method = "POST";
                objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
                objRequest.ContentType = "application/x-www-form-urlencoded";
                try
                {
                    myWriter = new StreamWriter(objRequest.GetRequestStream());
                    myWriter.Write(url);
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    myWriter.Close();
                }

                HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                    // Close and clean up the StreamReader
                    sr.Close();
                }  

                return true;

            }
            catch (Exception ex)
            {
                // Log
                return false;
            }*/
        }

        public bool SentSOSMessage(string message, string emergencyNumbers)
        {
            try
            {
                message = HttpUtility.UrlEncode(message);
                using (var wb = new WebClient())
                {
                    byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                    {"apikey" , ConfigurationManager.AppSettings["SMSKey"]},
                    {"numbers" , emergencyNumbers},
                    {"message" , message},
                    {"sender" , "ROSHNI"}
                });

                    string result = System.Text.Encoding.UTF8.GetString(response);
                    // Log result
                    File.WriteAllText(Path.Combine(ConfigurationManager.AppSettings["LogPath"], "SOS_" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + ".json"), result);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
