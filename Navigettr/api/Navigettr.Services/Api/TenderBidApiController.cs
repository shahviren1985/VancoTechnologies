using GreenNub.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GreenNub.Api
{
    public class TenderBidApiController : ApiController
    {
        private static readonly ITenderBidRepository _repository = new TenderBidRepository();

        public int InsertTenderBid(Int32 BidID, Int32 PartnerID, Int32 TenderId, Decimal Budget,
             string Description, DateTime BidDate, string AdditionalNotes, Int32 ExpectedDeliveryDays, Int32 Status)
        {
            try
            {
                return _repository.InsertTenderBid(BidID, PartnerID, TenderId, Budget, Description, BidDate, AdditionalNotes, ExpectedDeliveryDays, Status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int TenderBIDStatusChange(int ID, int Status)
        {
            return _repository.TenderBIDStatusChange(ID, Status);
        }
    }
}
