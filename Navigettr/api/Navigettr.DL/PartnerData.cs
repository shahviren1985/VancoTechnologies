using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigettr.Data
{
    public class PartnerData
    {
        NavigettrEntities Objdb = new NavigettrEntities();

        public int SaveUpdatePartner(int UserID, string LoginType, string UserName, string Password, int RoleId, string UStatus, string EmailAddress, string MobileNumber, int PartnerID, string PartnerName, string PartnerLogoPath, DateTime ExpiryDate, string PStatus)
        {
            try
            {
                ObjectParameter StatusResult = new ObjectParameter("StatusResult", typeof(string));
                ObjectParameter StatusResultUserId = new ObjectParameter("StatusResultUserId", typeof(string));

                int retval = 0;
                retval = Objdb.SP_PartnerDetails_InsertUpdate(UserID, LoginType, UserName, Password, RoleId, UStatus, EmailAddress, MobileNumber, PartnerID, PartnerName, PartnerLogoPath, ExpiryDate, PStatus, StatusResult, StatusResultUserId);
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

        public List<int> SaveUpdateRates(List<PartnerRate> rates)
        {
            try
            {
                ObjectParameter StatusResult = new ObjectParameter("StatusResult", typeof(string));
                List<int> ids = new List<int>();

                foreach (PartnerRate rate in rates)
                {
                    int retval = 0;
                    retval = Objdb.SP_PartnerRates_InsertUpdate(rate.Id, rate.PartnerId, rate.FromRate, rate.ToRate, rate.Guaranteed, rate.Indicative, rate.Status, rate.RateType, StatusResult);
                    ids.Add(rate.Id);
                }

                return ids;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveUpdateCharges(PartnerCharge charge)
        {
            try
            {
                ObjectParameter StatusResult = new ObjectParameter("StatusResult", typeof(string));

                int retval = 0;
                retval = Objdb.SP_PartnerCharges_InsertUpdate(charge.Id, charge.PartnerId, charge.Frequency, charge.Category, charge.Charges, charge.IsDefault, charge.Status, StatusResult);

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

        public List<SP_FetchPartnerDetails_Result> FetchPartner(string status, string Partnername, int Page, int PageData)
        {
            try
            {
                return Objdb.SP_FetchPartnerDetails(status, Partnername, Page, PageData).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SP_FetchPartnerRates_Result1> FetchPartnerRates(int partnerId)
        {
            try
            {
                return Objdb.SP_FetchPartnerRates(partnerId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SP_FetchPartnerChargesByPartnerID_Result> FetchPartnerCharges(int partnerId)
        {
            try
            {
                return Objdb.SP_FetchPartnerChargesByPartnerID(partnerId).ToList();
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
                return Objdb.SP_FetchSystemParam().ToList();
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
                return Objdb.SP_FetchPartnerSettings(partnerId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SaveGlobalConfig(List<SystemParam> configs)
        {
            foreach (SystemParam config in configs)
            {
                ObjectParameter StatusResult = new ObjectParameter("StatusResult", typeof(string));
                config.Id = Objdb.SP_SystemParam_InsertUpdate(config.Id, config.PartnerId, config.Key, config.DisplayValue, config.Value, config.ParentId, config.Status, StatusResult);
            }

            return true;
        }

        public int SavePartnerSettings(PartnerSettings settings)
        {
            ObjectParameter StatusResult = new ObjectParameter("StatusResult", typeof(string));
            return Objdb.SP_PartnerSettings(settings.Id, settings.BrandName, settings.BrandLogoPath, settings.RedirectLink, settings.EmailAddress, StatusResult);
        }


        public ServiceProviders SearchServiceProviders(int userId, double amount, string city, string country, string zipCode, int radius, float latitude, float longitude, string fromCurrency, string toCurrency, string orderByColumn, string orderDirection, int page, int pageData)
        {
            string connString = System.Configuration.ConfigurationSettings.AppSettings["conn"];
            List<SP_SearchServiceProviders_Result> providers = new List<SP_SearchServiceProviders_Result>();

            int totalCount = 0;
            int offerCount = 0;
            bool redirectLink = false;

            SqlParameter total = new SqlParameter("TotalCount", DbType.Int16);
            SqlParameter offerParam = new SqlParameter("Offers", DbType.Int16);
            SqlParameter redirect = new SqlParameter("RedirectLink", DbType.Boolean);

            total.Direction = ParameterDirection.Output;
            offerParam.Direction = ParameterDirection.Output;
            redirect.Direction = ParameterDirection.Output;

            using (SqlConnection con = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_FetchServiceProviders", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add("@RADIUS", SqlDbType.Int).Value = radius;
                    cmd.Parameters.Add("@LAT", SqlDbType.Float).Value = latitude;
                    cmd.Parameters.Add("@LONG", SqlDbType.Float).Value = longitude;
                    cmd.Parameters.Add("@FromCurrency", SqlDbType.NVarChar).Value = fromCurrency;
                    cmd.Parameters.Add("@ToCurrency", SqlDbType.NVarChar).Value = toCurrency;
                    cmd.Parameters.Add("@OrderByColumn", SqlDbType.NVarChar).Value = orderByColumn;
                    cmd.Parameters.Add("@OrderDirection", SqlDbType.NVarChar).Value = orderDirection;
                    cmd.Parameters.Add("@Page", SqlDbType.NVarChar).Value = page;
                    cmd.Parameters.Add("@Pagedata", SqlDbType.NVarChar).Value = pageData;
                    //cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Value = 0;
                    //cmd.Parameters.Add("@Offers", SqlDbType.Int).Value = 0;
                    //cmd.Parameters.Add("@RedirectLink", SqlDbType.Bit).Value = 0;

                    cmd.Parameters.Add(total);
                    cmd.Parameters.Add(offerParam);
                    cmd.Parameters.Add(redirect);

                    con.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        SP_SearchServiceProviders_Result provider = new SP_SearchServiceProviders_Result();
                        provider.ID = int.Parse(reader["ID"].ToString());
                        provider.PartnerId = int.Parse(reader["PartnerId"].ToString());
                        provider.PartnerName = reader["PartnerName"].ToString();
                        provider.PartnerLogoPath = reader["PartnerLogoPath"].ToString();
                        provider.RedirectLink = reader["RedirectLink"].ToString();

                        provider.AddressLine1 = reader["AddressLine1"].ToString();
                        provider.AddressLine2 = reader["AddressLine2"].ToString();
                        provider.City = reader["City"].ToString();
                        provider.State = reader["State"].ToString();
                        provider.ZipCode = reader["ZipCode"].ToString();

                        provider.Latitude = float.Parse(reader["Latitude"].ToString());
                        provider.Longitude = float.Parse(reader["Longitude"].ToString());
                        provider.MobileNumber = reader["MobileNumber"].ToString();
                        provider.Distance = float.Parse(reader["Distance"].ToString());
                        provider.Guaranteed = float.Parse(reader["Guaranteed"].ToString());
                        provider.Indicative = float.Parse(reader["Indicative"].ToString());
                        providers.Add(provider);
                    }

                    reader.Close();

                    ObjectParameter statusResult = new ObjectParameter("StatusResult", "");

                    if (total.Value != null)
                    {
                        totalCount = int.Parse(total.Value.ToString());
                        offerCount = int.Parse(offerParam.Value.ToString());
                        redirectLink = redirect.Value.ToString() == "0" ? false : true;
                    }

                    Objdb.SP_UserSearchTracker_Insert(userId, city, country, zipCode, latitude, longitude, amount, fromCurrency, toCurrency, DateTime.UtcNow, totalCount, statusResult);
                }

                if (providers.Count > 0)
                {
                    using (SqlCommand cmd = new SqlCommand("SP_FetchServiceProvidersWorkLocations", con))
                    {
                        List<SP_FetchLocationWorkTimeDetails_Result> worktimes = new List<SP_FetchLocationWorkTimeDetails_Result>();
                        string locationId = string.Join(",", providers.Select(p => p.ID));
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@LocationId", SqlDbType.VarChar).Value = locationId;
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            SP_FetchLocationWorkTimeDetails_Result worktime = new SP_FetchLocationWorkTimeDetails_Result();
                            worktime.ID = int.Parse(reader["ID"].ToString());
                            worktime.LocationId = int.Parse(reader["LocationId"].ToString());
                            worktime.WorkDay = reader["WorkDay"].ToString();
                            worktime.WorkStartTime = reader["WorkStartTime"].ToString();
                            worktime.WorkEndTime = reader["WorkEndTime"].ToString();
                            worktimes.Add(worktime);

                            var location = providers.Find(p => p.ID == worktime.LocationId);
                            if (location != null)
                            {
                                if (location.WorkTime == null)
                                    location.WorkTime = new List<SP_FetchLocationWorkTimeDetails_Result>();

                                location.WorkTime.Add(worktime);
                            }
                        }

                        reader.Close();
                    }
                }

                // return offers only if user has logged in
                if (userId > 0 && providers.Count > 0)
                {
                    using (SqlCommand cmd = new SqlCommand("SP_FetchServiceProvidersOffers", con))
                    {
                        string partnerId = string.Join(",", providers.Select(p => p.PartnerId).Distinct().Take(offerCount));
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@PartnerId", SqlDbType.VarChar).Value = partnerId;
                        var reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            OfferDetails offer = new OfferDetails();
                            offer.Id = int.Parse(reader["ID"].ToString());
                            offer.PartnerId = int.Parse(reader["PartnerId"].ToString());
                            offer.OfferName = reader["OfferName"].ToString();
                            offer.OfferType = reader["OfferType"].ToString();
                            offer.OfferText = reader["OfferText"].ToString();
                            offer.OfferImagePath = reader["OfferImage"].ToString();

                            var partner = providers.Find(p => p.PartnerId == offer.PartnerId);
                            if (partner != null)
                            {
                                partner.Offer = offer;
                            }
                        }
                    }
                }
            }

            ServiceProviders serviceProvider = new ServiceProviders();
            serviceProvider.Providers = providers;
            serviceProvider.TotalCount = totalCount;
            return serviceProvider;
        }

        public int UserLocationTracker(int userId, int partnerId, int locationId, DateTime reachedAt)
        {
            ObjectParameter StatusResult = new ObjectParameter("StatusResult", typeof(string));
            return Objdb.SP_UserLocationTracker_Insert(userId, partnerId, locationId, reachedAt, StatusResult);
        }

        public int UserQRCodeTracker(int userId, string locationId, int partnerId, float amount, string fromCurrency, string toCurrnecy, DateTime scannedOn)
        {
            ObjectParameter statusResult = new ObjectParameter("StatusResult", typeof(string));
            return Objdb.SP_UserQRCodeTracker_Insert(userId, locationId, partnerId, amount, fromCurrency, toCurrnecy, scannedOn, statusResult);
        }
    }
}
