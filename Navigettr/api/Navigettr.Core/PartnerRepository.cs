using Navigettr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigettr.Core
{
    public class PartnerRepository : IPartnerRepository
    {
        PartnerData objPartnerData = new PartnerData();
        PartnerLocationData partnerLocationData = new PartnerLocationData();

        public int SaveUpdatePartner(int UserID, string LoginType, string UserName, string Password, int RoleId, string UStatus, string EmailAddress, string MobileNumber, int PartnerID, string PartnerName, string PartnerLogoPath, DateTime ExpiryDate, string PStatus)
        {
            try
            {
                int retval = 0;
                retval = objPartnerData.SaveUpdatePartner(UserID, LoginType, UserName, Password, RoleId, UStatus, EmailAddress, MobileNumber, PartnerID, PartnerName, PartnerLogoPath, ExpiryDate, PStatus);
                return retval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<int> SaveUpdateRates(List<PartnerRate> rates)
        {
            return objPartnerData.SaveUpdateRates(rates);
        }

        public int SaveUpdateCharges(PartnerCharge charge)
        {
            int retval = 0;
            retval = objPartnerData.SaveUpdateCharges(charge);
            return retval;
        }

        public List<SP_FetchPartnerDetails_Result> FetchPartner(string status, string Partnername, int Page, int PageData)
        {
            try
            {
                List<SP_FetchPartnerDetails_Result> partners = objPartnerData.FetchPartner(status, Partnername, Page, PageData).ToList();

                foreach (SP_FetchPartnerDetails_Result partner in partners)
                {
                    partner.locationId = partnerLocationData.GetPartnerLocationCount(partner.id).Value;
                }

                return partners;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SP_FetchPartnerRates_Result1> FetchPartnerRates(int partnerID)
        {
            try
            {
                return objPartnerData.FetchPartnerRates(partnerID).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SP_FetchPartnerChargesByPartnerID_Result> FetchPartnerCharges(int partnerID)
        {
            try
            {
                return objPartnerData.FetchPartnerCharges(partnerID).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<SP_FetchSystemParam_Result> FetchGlobalConfig()
        {
            try
            {
                return objPartnerData.FetchGlobalConfig().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveGlobalConfig(List<SystemParam> parameters)
        {
            try
            {
                return objPartnerData.SaveGlobalConfig(parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SP_FetchPartnerSettings_Result> FetchPartnerSettings(int partnerId)
        {
            try
            {
                return objPartnerData.FetchPartnerSettings(partnerId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SavePartnerSettings(PartnerSettings settings)
        {
            try
            {
                return objPartnerData.SavePartnerSettings(settings);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SendAddPartnerEmail(EmailData data)
        {
            string content = System.IO.File.ReadAllText(data.TemplateFilePath);
            content = string.Format(content, data.LoginLink, data.ToEmailAdress, data.ToPassword);
            data.Body = content;
            return EmailData.SendEmail(data);
        }

        public ServiceProviders SearchServiceProviders(int userId, double amount, string city, string country, string zipCode, int radius, float latitude, float longitude, string fromCurrency, string toCurrency, string orderByColumn, string orderDirection, int page, int pageData)
        {
            try
            {
                return objPartnerData.SearchServiceProviders(userId, amount, city, country, zipCode, radius, latitude, longitude, fromCurrency, toCurrency, orderByColumn, orderDirection, page, pageData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ServiceProviders SearchServiceProviderLocations(int userId, int partnerId, double amount, string city, string country, string zipCode, int radius, float latitude, float longitude, string fromCurrency, string toCurrency, string orderByColumn, string orderDirection, int page, int pageData)
        {
            try
            {
                return objPartnerData.SearchServiceProviderLocations(userId, partnerId, amount, city, country, zipCode, radius, latitude, longitude, fromCurrency, toCurrency, orderByColumn, orderDirection, page, pageData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UserLocationTracker(int userId, int partnerId, int locationId, DateTime reachedAt)
        {
            try
            {
                return objPartnerData.UserLocationTracker(userId, partnerId, locationId, reachedAt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UserQRCodeTracker(int userId, string transactionId, int partnerId, float amount, string fromCurrency, string toCurrnecy, DateTime scannedOn, float rate, string serviceType, float fees, string feesCurrency)
        {
            try
            {
                return objPartnerData.UserQRCodeTracker(userId, transactionId, partnerId, amount, fromCurrency, toCurrnecy, scannedOn, rate, serviceType, fees, feesCurrency);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
