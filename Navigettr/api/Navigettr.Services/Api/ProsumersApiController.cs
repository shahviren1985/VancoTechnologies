
using GreenNub.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GreenNub.Api
{
    public class ProsumersApiController : ApiController
    {
        private static readonly IProsumersRepository _repository = new ProsumersRepository();

        public int InsertProsumers(Int32 ProsumersID, string UniquePartnerId, string CompanyName, string Avatar, string IncorporationDate, string Email, string Password, string ContactNo, string Address1, string Address2, string PostalCode, int StateID, int CityID, bool IsActive, string Website, string IpAddress)
        {
            try
            {
                int retval = 0;
                retval = _repository.InsertProsumers(ProsumersID, UniquePartnerId, CompanyName, Avatar, IncorporationDate, Email, Password, ContactNo, Address1, Address2, PostalCode, StateID, CityID, IsActive, Website, IpAddress);
                return retval;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int ProsumersStatusChange(int ID, bool Status)
        {
            return _repository.ProsumersStatusChange(ID, Status);
        }
    }
}
