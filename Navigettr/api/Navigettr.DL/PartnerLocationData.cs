using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigettr.Data
{
    public class PartnerLocationData
    {
        NavigettrEntities Objdb = new NavigettrEntities();

        public int SaveUpdatePartnerLocation(int PartnerLocationID, int PartnerID, string AddressLine1, string AddressLine2, string City, string State, string Country, string ZipCode,
                                              float Latitude, float Longitude, string MobileNumber, DateTime DateActivated, DateTime DateExpiry, string Status)
        {
            try
            {
                ObjectParameter StatusResult = new ObjectParameter("StatusResult", typeof(string));
                int retval = 0;
                retval = Objdb.SP_PartnerLocationDetails_InsertUpdate(PartnerLocationID, PartnerID, AddressLine1, AddressLine2, City, State, Country, ZipCode, Latitude,
                                                                       Longitude, MobileNumber, DateActivated, DateExpiry, Status, StatusResult);
                // return retval;

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

        public List<SP_FetchPartnerLocationDetails_Result> FetchPartnerLocation(int PartnerID, string Status, string ZipCode, string City, int Page, int PageData, ObjectParameter TotalCount)
        {
            try
            {
                return Objdb.SP_FetchPartnerLocationDetails(PartnerID, Status, ZipCode, City, Page, PageData, TotalCount).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int SaveUpdatePartnerLocationWorkTime(int ID, int PartnerID, int LocationId, string WorkDay, string WorkStartTime, string WorkEndTime, string Status)
        {
            try
            {
                ObjectParameter StatusResult = new ObjectParameter("StatusResult", typeof(string));
                int retval = 0;
                retval = Objdb.SP_PartnerLocationWorkTime_InsertUpdate(ID, PartnerID, LocationId, WorkDay, WorkStartTime, WorkEndTime, Status, StatusResult);



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

        public List<SP_FetchLocationWorkTimeDetails_Result> GetPartnerLocationWorkTimes(int partnerId)
        {
            return Objdb.SP_FetchLocationWorkTimeDetails(partnerId).ToList();
        }

        public int? GetPartnerLocationCount(int partnerId)
        {
            return Objdb.SP_FetchPartnerLocationDetailsCount(partnerId).First().TotalCount;
        }

        public List<SP_FetchUnProcessedLocations_Result> FetchUnProcessedLocation()
        {
            try
            {
                return Objdb.SP_FetchUnProcessedLocations().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
