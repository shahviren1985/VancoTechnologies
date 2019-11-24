using Navigettr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigettr.Core
{
    public class PartnerOffersRepository: IPartnerOffersRepository
    {
        PartnerOffersData Objdb = new PartnerOffersData();
        public int SaveUpdatePartnerOffer(int ID, int PartnerID, string OfferName, string OfferType, string OfferText, DateTime OfferStartDate, DateTime OfferEndDate, string Status, DateTime DateActivation, DateTime ExpiryDate)
        {
            try
            {
                int retval = 0;
                retval = Objdb.SaveUpdatePartnerOffer(ID, PartnerID, OfferName, OfferType, OfferText, OfferStartDate, OfferEndDate, Status, DateActivation, ExpiryDate);
                return retval;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<SP_FetchPartnerOffers_Result> FetchPartnerOffer(int ID, string Status, string Offername, int Page, int PageData)
        {
            try
            {
                return Objdb.FetchPartnerOffer(ID, Status, Offername, Page, PageData).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
    }
}
