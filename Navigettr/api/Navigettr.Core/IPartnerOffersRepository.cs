using Navigettr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigettr.Core
{
    public interface IPartnerOffersRepository
    {
        int SaveUpdatePartnerOffer(int ID, int PartnerID, string OfferName, string OfferType, string OfferText, DateTime OfferStartDate, DateTime OfferEndDate, string Status, DateTime DateActivation, DateTime ExpiryDate);
        List<SP_FetchPartnerOffers_Result> FetchPartnerOffer(int ID, string Status, string Offername, int Page, int PageData);
      

    }
}
