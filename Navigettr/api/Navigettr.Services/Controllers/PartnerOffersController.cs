using Navigettr.Core;
using Navigettr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace Navigettr.Services.Controllers
{
    public class PartnerOffersController : ApiController
    {
        private static readonly IPartnerOffersRepository _repository = new PartnerOffersRepository();
        PartnerOffersData objPartner = new PartnerOffersData();

        public class PartnerOffers
        {
            public int id { get; set; }
            public int partnerId { get; set; }
            public string offerName { get; set; }
            public string offerType { get; set; }
            public string offerText { get; set; }
            public DateTime offerStartDate { get; set; }
            public DateTime offerEndDate { get; set; }
            public string status { get; set; }
            public DateTime dateActivation { get; set; }
            public DateTime dateExpiry { get; set; }
            public int page { get; set; }
            public int pageData { get; set; }
            public int totalCount { get; set; }
        }
        [HttpPost]
        [HttpOptions]
        [Route("api/addPartnerOffer")]
    
        public HttpResponseMessage SaveUpdatePartnerOffer([FromBody]PartnerOffers jsondata)
        {
            string yourJson = "";

            try
            {
                int data = _repository.SaveUpdatePartnerOffer(jsondata.id, jsondata.partnerId, jsondata.offerName, jsondata.offerType, jsondata.offerText, jsondata.offerStartDate, jsondata.offerEndDate, jsondata.status, jsondata.dateActivation,jsondata.dateExpiry);

                if (data > 0)
                {
                    jsondata.id = data;
                    return Request.CreateResponse(HttpStatusCode.OK, jsondata);
                }
                else
                {
                    yourJson = "Unable to add offer";
                    HttpError myCustomError = new HttpError(yourJson);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, myCustomError);
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [HttpOptions]
        [Route("api/getPartnerOffers")]
      
        public HttpResponseMessage getPartnerOffers(string PartnerId,[FromBody]PartnerOffers jsondata)
        {
            try
            {

                var data = _repository.FetchPartnerOffer(Convert.ToInt32(PartnerId), jsondata.status, jsondata.offerName, jsondata.page, jsondata.pageData);
             
                if (data.Count > 0 && data != null)
                {
                    foreach(SP_FetchPartnerOffers_Result offer in data)
                    {
                        offer.dateCreated = DateTime.Parse(offer.dateCreated.ToShortDateString());
                        offer.dateExpiry = DateTime.Parse(offer.dateExpiry.ToShortDateString());
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, data);
                  
                }
                else
                {
                    string yourJson = "This partner doesn't have any offers added.";
                    HttpError myCustomError = new HttpError(yourJson);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, myCustomError);
                }


            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }

        }


    }
}
