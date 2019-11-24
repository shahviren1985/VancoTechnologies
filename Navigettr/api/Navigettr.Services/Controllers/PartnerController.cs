using Navigettr.Core;
using Navigettr.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Navigettr.Services.Controllers
{
    public class PartnerController : ApiController
    {
        private static readonly IPartnerRepository _repository = new PartnerRepository();
        PartnerData objPartner = new PartnerData();

        public class Partner
        {

            #region User Details Variable

            public int userId { get; set; }
            public string loginType { get; set; }
            public string userName { get; set; }
            public string password { get; set; }
            public int roleId { get; set; }
            public string uStatus { get; set; }
            public string emailAddress { get; set; }
            public string mobileNo { get; set; }

            #endregion
            #region Partner Details Variable

            public string name { get; set; }
            public string partnerLogoPath { get; set; }
            public DateTime expiryDate { get; set; }
            public int partnerId { get; set; }
            public int page { get; set; }
            public int pageData { get; set; }
            public string status { get; set; }

            #endregion

        }
        public class Partnersettings
        {
            public int id { get; set; }
            public string brandName { get; set; }
            public string brandLogoPath { get; set; }
            public string redirectLink { get; set; }
            public string emailAddress { get; set; }
        }


        [HttpPost]
        [HttpOptions]
        [Route("api/addPartner")]

        public HttpResponseMessage SaveUpdatePartner([FromBody]Partner jsondata)
        {
            string yourJson = "";

            try
            {
                int data = _repository.SaveUpdatePartner(jsondata.userId, jsondata.loginType, jsondata.userName, jsondata.password, jsondata.roleId, jsondata.uStatus, jsondata.emailAddress, jsondata.mobileNo, jsondata.partnerId, jsondata.name, jsondata.partnerLogoPath, jsondata.expiryDate, jsondata.status);

                if (data > 0)
                {
                    jsondata.partnerId = data;
                    jsondata.userId = data;

                    EmailData email = new EmailData();
                    email.TemplateFilePath = HttpContext.Current.Server.MapPath("~/App_Data/EmailTemplates/AddPartner.html");
                    email.HostName = ConfigurationManager.AppSettings["Host"];
                    email.EnableSSL = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"]);
                    email.Subject = ConfigurationManager.AppSettings["Subject"];
                    email.FromEmailAddress = ConfigurationManager.AppSettings["MailUserName"];
                    email.FromPassword = ConfigurationManager.AppSettings["MailPassword"];
                    email.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                    email.LoginLink = ConfigurationManager.AppSettings["LoginLink"];
                    email.FromName = ConfigurationManager.AppSettings["MailName"];
                    email.ToName = jsondata.name;
                    email.ToPassword = jsondata.password;

                    try
                    {
                        _repository.SendAddPartnerEmail(email);
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, jsondata);
                }
                else if (data == -1)
                {
                    yourJson = "username/email address already exists";
                    HttpError myCustomError = new HttpError(yourJson);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, myCustomError);
                }
                else if (data == -2)
                {
                    yourJson = "Unable to add new partner";
                    HttpError myCustomError = new HttpError(yourJson);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, myCustomError);
                }
                else
                {
                    yourJson = "";
                    HttpError myCustomError = new HttpError(yourJson);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, myCustomError);
                }
            }
            catch (Exception ex)
            {

                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [HttpPost]
        [HttpOptions]
        [Route("api/getPartners")]

        public HttpResponseMessage populatePartners([FromBody]Partner jsondata)
        {
            string yourJson = "";
            try
            {
                var data = _repository.FetchPartner(jsondata.status, jsondata.name, jsondata.page, jsondata.pageData);

                if (data.Count > 0 && data != null)
                {

                    //return Request.CreateResponse(HttpStatusCode.OK, data);
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {

                    yourJson = "No records found";
                    HttpError myCustomError = new HttpError(yourJson);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, myCustomError);


                }


            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }

        }

        [HttpPost]
        [HttpOptions]
        [Route("api/getDefaultCharges")]
        public HttpResponseMessage GetDefaultCharges()
        {
            try
            {
                var data = _repository.FetchPartnerCharges(1);
                if (data.Count > 0 && data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    HttpError myCustomError = new HttpError("No default charges found");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, myCustomError);
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "An error occurred while getting the default charges");
            }
        }

        [HttpPost]
        [HttpOptions]
        [Route("api/getPartnerCharges")]
        public HttpResponseMessage GetPartnerCharges(int PartnerID)
        {
            try
            {
                var data = _repository.FetchPartnerCharges(PartnerID);
                if (data.Count > 0 && data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    HttpError myCustomError = new HttpError("No partner charges found");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, myCustomError);
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "An error occurred while getting the partner charges");
            }
        }

        [HttpPost]
        [HttpOptions]
        [Route("api/getPartnerRates")]
        public HttpResponseMessage GetPartnerRates(int PartnerId)
        {
            try
            {
                var data = _repository.FetchPartnerRates(PartnerId);
                if (data.Count > 0 && data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    HttpError myCustomError = new HttpError("No partner rates found");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, myCustomError);
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "An error occurred while getting the partner rates");
            }
        }

        [HttpPost]
        [HttpOptions]
        [Route("api/getPartnerCurrencies")]
        public HttpResponseMessage GetPartnerCurrencies(int PartnerId)
        {
            List<string> fromList = new List<string> { "AED", "AUD", "CAD", "CHF", "DKK", "EUR", "GBP", "INR", "SEK", "SGD", "USD", "YEN" };
            return Request.CreateResponse(HttpStatusCode.OK, fromList);
        }

        [HttpGet]
        [HttpOptions]
        [Route("api/getAllCurrencies")]
        public HttpResponseMessage GetAllCurrencies()
        {
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/currency.json");
            string allText = System.IO.File.ReadAllText(filePath);
            var currency = JsonConvert.DeserializeObject(allText);
            return Request.CreateResponse(HttpStatusCode.OK, currency);
        }

        [HttpPost]
        [HttpOptions]
        [Route("api/savePartnerRates")]

        public HttpResponseMessage SaveUpdatePartnerRates([FromBody]List<PartnerRate> rates)
        {
            try
            {
                List<int> data = _repository.SaveUpdateRates(rates);
                return Request.CreateResponse(HttpStatusCode.OK, data);

                //else
                //{
                //    HttpError myCustomError = new HttpError("Unable to add the new rates for partner");
                //    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, myCustomError);

                //}
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [HttpOptions]
        [Route("api/savePartnerCharges")]

        public HttpResponseMessage SaveUpdatePartnerCharges([FromBody]List<PartnerCharge> charges)
        {
            try
            {
                List<SP_FetchPartnerChargesByPartnerID_Result> chargeList = new List<SP_FetchPartnerChargesByPartnerID_Result>();
                foreach (PartnerCharge charge in charges)
                {
                    int data = _repository.SaveUpdateCharges(charge);
                    charge.Id = data;

                    SP_FetchPartnerChargesByPartnerID_Result chargeItem = new SP_FetchPartnerChargesByPartnerID_Result();
                    chargeItem.category = charge.Category;
                    chargeItem.charges = charge.Charges;
                    chargeItem.frequency = charge.Frequency;
                    chargeItem.id = charge.Id;
                    chargeItem.isDefault = charge.IsDefault;
                    chargeList.Add(chargeItem);
                }

                return Request.CreateResponse(HttpStatusCode.OK, chargeList);
            }
            catch (Exception ex)
            {
                HttpError myCustomError = new HttpError("Unable to add/update the charges for partner");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, myCustomError);
            }
        }

        [HttpPost]
        [HttpOptions]
        [Route("api/getGlobalConfig")]
        public HttpResponseMessage GetGlobalConfig()
        {
            try
            {
                var data = _repository.FetchGlobalConfig();
                if (data.Count > 0 && data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    HttpError myCustomError = new HttpError("No Global configurations found");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, myCustomError);
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "An error occurred while getting the global configuration");
            }
        }

        [HttpPost]
        [HttpOptions]
        [Route("api/saveGlobalConfig")]
        public HttpResponseMessage SaveGlobalConfig(List<SystemParam> config)
        {
            try
            {
                var saved = _repository.SaveGlobalConfig(config);
                if (saved)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, config);
                }
                else
                {
                    HttpError myCustomError = new HttpError("No Global configurations found");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, myCustomError);
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "An error occurred while saving the global configuration");
            }
        }

        [HttpPost]
        [HttpOptions]
        [Route("api/getPartnerSettings")]
        public HttpResponseMessage GetPartnerSettings(int partnerId)
        {
            try
            {
                var data = _repository.FetchPartnerSettings(partnerId);
                if (data.Count > 0 && data != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    HttpError myCustomError = new HttpError("No partner settings found");
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, myCustomError);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "An error occurred while getting the partner settings");
            }
        }

        [HttpPost]
        [HttpOptions]
        [Route("api/savePartnerSettings")]
        public HttpResponseMessage SavePartnerSettings(int id, string brandName, string brandLogoPath, string redirectLink, string emailAddress)
        {
            try
            {
                PartnerSettings settings = new PartnerSettings { Id = id, BrandName = brandName, BrandLogoPath = brandLogoPath, EmailAddress = emailAddress, RedirectLink = redirectLink };
                _repository.SavePartnerSettings(settings);
                return Request.CreateResponse(HttpStatusCode.OK, settings);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [HttpOptions]
        [Route("api/uploadBrandLogo")]
        public HttpResponseMessage PostFile(int partnerId)
        {
            Partnersettings settings = new Partnersettings();
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                string guid = Guid.NewGuid().ToString() + "_" + partnerId + "_";
                foreach (string file in HttpContext.Current.Request.Files)
                {
                    var postedFile = HttpContext.Current.Request.Files[file];

                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(postedFile.FileName).Replace(" ", "").Replace("(", "").Replace(")", "");
                        string extension = Path.GetExtension(postedFile.FileName);
                        string targetPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Logos/"), guid + fileName + extension);
                        postedFile.SaveAs(targetPath);

                        settings.brandLogoPath = guid + fileName + extension;
                        return Request.CreateResponse(HttpStatusCode.OK, settings);
                    }
                }
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError, settings);
        }

        [Route("api/search")]
        [HttpPost]
        [HttpOptions]
        public HttpResponseMessage Search(SearchParams param)
        {
            ServiceProviders result = _repository.SearchServiceProviders(param.UserId, param.ServiceParams.Amount, param.City, param.Country, param.ZipCode, param.SearchRadius, float.Parse(param.Latitude), float.Parse(param.Longitude), param.ServiceParams.FromCurrency, param.ServiceParams.ToCurrency, param.OrderByColumn, param.OrderByDirection, param.PageNumber, param.PageSize);
            // 1. Get the worktime of each location
            // 5. Get offers for top N results. N is configured in DB - get only if user id not missing
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("api/trackUserVisit")]
        [HttpPost]
        [HttpOptions]
        public HttpResponseMessage TrackUserVisit(int userId, int partnerId, int locationId)
        {
            int response = _repository.UserLocationTracker(userId, partnerId, locationId, DateTime.UtcNow);
            return Request.CreateResponse(HttpStatusCode.OK, new { message = "User visit tracked successfully." });
        }

        [Route("api/scanPartnerQRCode")]
        [HttpPost]
        [HttpOptions]
        public HttpResponseMessage ScanPartnerQRCode(int userId, int partnerId, string locationId, float amount, string fromCurrency, string toCurrency, DateTime dateScanned)
        {
            int response = _repository.UserQRCodeTracker(userId, locationId, partnerId, amount, fromCurrency, toCurrency, dateScanned);
            return Request.CreateResponse(HttpStatusCode.OK, new { message = "QR Code scanned successfully." });
        }

        [Route("api/getRewards")]
        [HttpPost]
        [HttpOptions]
        public HttpResponseMessage GetRewards(int userId)
        {
            var rewards = new List<Rewards> {
                new Rewards { Id = 3, TransactionId ="ABCDEF123", RewardPoints = 50, Comments="You have earned 50 points.", DateEarned = "25 April 2019" },
                new Rewards { Id = 2, TransactionId ="ABCDEG123", RewardPoints = 25, Comments="You have earned 25 points.", DateEarned = "22 April 2019"},
                new Rewards { Id = 1, TransactionId ="ABCDEH123", RewardPoints = 5, Comments="You have earned 5 points.", DateEarned = "20 April 2019" }
            };
            return Request.CreateResponse(HttpStatusCode.OK, rewards);
        }
    }

    public class Rewards
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public int RewardPoints { get; set; }
        public string DateEarned { get; set; }
        public string Comments { get; set; }
    }
}