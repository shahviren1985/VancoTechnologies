using Navigettr.Data;
using System;
using System.Collections.Generic;

namespace Navigettr.Core
{
    public interface IPartnerRepository
    {
        int SaveUpdatePartner(int UserID, string LoginType, string UserName, string Password, int RoleId, string UStatus, string EmailAddress, string MobileNumber, int PartnerID, string PartnerName, string PartnerLogoPath, DateTime ExpiryDate, string PStatus);
        List<SP_FetchPartnerDetails_Result> FetchPartner(string status, string Partnername, int Page, int PageData);
        List<SP_FetchPartnerRates_Result1> FetchPartnerRates(int partnerId);
        List<SP_FetchPartnerChargesByPartnerID_Result> FetchPartnerCharges(int partnerId);
        int SaveUpdateCharges(PartnerCharge charge);
        List<int> SaveUpdateRates(List<PartnerRate> rate);
        List<SP_FetchSystemParam_Result> FetchGlobalConfig();
        bool SaveGlobalConfig(List<SystemParam> param);
        List<SP_FetchPartnerSettings_Result> FetchPartnerSettings(int partnerId);
        int SavePartnerSettings(PartnerSettings settings);
        bool SendAddPartnerEmail(EmailData data);
        ServiceProviders SearchServiceProviders(int userId, double amount, string city, string country, string zipCode, int radius, float latitude, float longitude, string fromCurrency, string toCurrency, string orderByColumn, string orderDirection, int page, int pageData);
        ServiceProviders SearchServiceProviderLocations(int userId, int partnerId, double amount, string city, string country, string zipCode, int radius, float latitude, float longitude, string fromCurrency, string toCurrency, string orderByColumn, string orderDirection, int page, int pageData);
        int UserLocationTracker(int userId, int partnerId, int locationId, DateTime reachedAt);
        int UserQRCodeTracker(int userId, string locationId, int partnerId, float amount, string fromCurrency, string toCurrnecy, DateTime scannedOn, float rate, string serviceType, float fees, string feesCurrency);
        //int UserRewardPointsTracker(int userId, string transactionId, string rewardPointsEarned, DateTime earnedDate, string comments);

    }

}
