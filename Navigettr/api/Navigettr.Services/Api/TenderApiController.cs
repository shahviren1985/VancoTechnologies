using GreenNub.Core;
using GreenNub.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GreenNub.Api
{
    public class TenderApiController : ApiController
    {
        private static readonly ITenderRepository _repository = new TenderRepository();

        #region Insert Update
        public int InsertTender(Int32 TenderID, Int32 PartnerID, string Title, string TenderNoticeNo,
         Int32 LocationID, Int32 CategoryID, string Skill, string Description, Decimal Budget, DateTime Openingdate, string OpeningTime, DateTime closingDate, string ClosingTime, int Status)
        {
            try
            {
                return _repository.InsertTender(TenderID, PartnerID, Title, TenderNoticeNo, LocationID, CategoryID, Skill, Description, Budget, Openingdate, OpeningTime, closingDate, ClosingTime, Status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateTender(Int32 TenderID, Int32 PartnerID, string Title, string TenderNoticeNo,
        Int32 LocationID, Int32 CategoryID, string Skill, string Description, Decimal Budget, DateTime Openingdate, string OpeningTime, DateTime closingDate, string ClosingTime, int Status)
        {
            try
            {
                return _repository.InsertTender(TenderID, PartnerID, Title, TenderNoticeNo, LocationID, CategoryID, Skill, Description, Budget, Openingdate, OpeningTime, closingDate, ClosingTime, Status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public List<usp_GetTender_Result> GetTender(int ID)
        {
            var Roles = _repository.GetTender(ID).ToList();
            return Roles;
        }

        #region delete 
        public int DeleteTenderID(int ID)
        {
            try
            {
                return _repository.DeleteTenderID(ID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<usp_GetTenderbycategoryid_Result> GetTenderbycategoryid(int ID)
        {
            var Roles = _repository.GetTenderbycategoryid(ID).ToList();
            return Roles;
        }
        #endregion
    }
}
