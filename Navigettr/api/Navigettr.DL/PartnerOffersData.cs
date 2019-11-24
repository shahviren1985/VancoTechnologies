using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigettr.Data
{
    public class PartnerOffersData
    {
        NavigettrEntities Objdb = new NavigettrEntities();

        public int SaveUpdatePartnerOffer(int ID, int PartnerID, string OfferName, string OfferType, string OfferText, DateTime OfferStartDate, DateTime OfferEndDate, string Status, DateTime DateActivation, DateTime ExpiryDate)
        {
            try
            {
                ObjectParameter StatusResult = new ObjectParameter("StatusResult", typeof(string));
               
                int retval = 0;
                retval = Objdb.SP_PartnerOffers_InsertUpdate(ID, PartnerID, OfferName, OfferType, OfferText, OfferStartDate, OfferEndDate, Status, DateActivation, ExpiryDate, StatusResult);
                //return retval;
                
                if (retval > 0)
                {
                    return Convert.ToInt32(StatusResult.Value);
                }
                else
                {
                    return retval;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

     
        public List<SP_FetchPartnerOffers_Result> FetchPartnerOffer(int ID,string Status, string Offername, int Page, int PageData)
        {
            try
            {
                return Objdb.SP_FetchPartnerOffers(ID, Status, Offername, Page, PageData).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     





    }
}
