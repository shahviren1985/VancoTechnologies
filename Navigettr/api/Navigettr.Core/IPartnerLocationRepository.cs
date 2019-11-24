using Navigettr.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigettr.Core
{
  public interface IPartnerLocationRepository
    {
        int SaveUpdatePartnerLocation(int PartnerLocationID, int PartnerID, string AddressLine1, string AddressLine2, string City, string State, string Country, string ZipCode,
                                              float Latitude, float Longitude, string MobileNumber, DateTime DateActivated, DateTime DateExpiry,string Status);
        List<SP_FetchPartnerLocationDetails_Result> FetchPartnerLocation(int PartnerID, string Status, string ZipCode, string City, int Page, int PageData, ObjectParameter TotalCount);

        int SaveUpdatePartnerLocationWorkTime(int ID, int PartnerID, int LocationId, string WorkDay, string WorkStartTime, string WorkEndTime, string Status);

        List<SP_FetchLocationWorkTimeDetails_Result> GetPartnerLocationWorkTimes(int partnerId);

        //int GetPartnerLocationCount(int partnerId);
        List<SP_FetchUnProcessedLocations_Result> FetchUnProcessedLocation();
    }
}
