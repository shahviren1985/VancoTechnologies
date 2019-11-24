package com.navigettr.app.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.List;

public class SearchResultForeignModel implements Serializable {

    @SerializedName("TotalCount")
    private int TotalCount;

    @SerializedName("Providers")
    public List<SearchResultForeignModel.Providers> Providers;

    public int getTotalCount() {
        return TotalCount;
    }

    public void setTotalCount(int totalCount) {
        TotalCount = totalCount;
    }

    public List<SearchResultForeignModel.Providers> getProviders() {
        return Providers;
    }

    public void setProviders(List<SearchResultForeignModel.Providers> providers) {
        Providers = providers;
    }

    public static class Providers {

        @SerializedName("ID")
        private int ID;

        @SerializedName("PartnerId")
        private int PartnerId;

        @SerializedName("PartnerName")
        private String PartnerName;

        @SerializedName("PartnerLogoPath")
        private String PartnerLogoPath;

        @SerializedName("RedirectLink")
        private String RedirectLink;

        @SerializedName("AddressLine1")
        private String AddressLine1;

        @SerializedName("AddressLine2")
        private String AddressLine2;

        @SerializedName("City")
        private String City;

        @SerializedName("State")
        private String State;

        @SerializedName("ZipCode")
        private String ZipCode;

        @SerializedName("Latitude")
        private double Latitude;

        @SerializedName("Longitude")
        private double Longitude;

        @SerializedName("MobileNumber")
        private String MobileNumber;

        @SerializedName("Distance")
        private double Distance;

        @SerializedName("Guaranteed")
        private double Guaranteed;

        @SerializedName("Indicative")
        private double Indicative;

        @SerializedName("WorkTime")
        public List<SearchResultForeignModel.Providers.WorkTime> WorkTime;

        @SerializedName("Offer")
        private SearchResultForeignModel.Providers.Offer Offer;

        public int getID() {
            return ID;
        }

        public void setID(int ID) {
            this.ID = ID;
        }

        public int getPartnerId() {
            return PartnerId;
        }

        public void setPartnerId(int partnerId) {
            PartnerId = partnerId;
        }

        public String getPartnerName() {
            return PartnerName;
        }

        public void setPartnerName(String partnerName) {
            PartnerName = partnerName;
        }

        public String getPartnerLogoPath() {
            return PartnerLogoPath;
        }

        public void setPartnerLogoPath(String partnerLogoPath) {
            PartnerLogoPath = partnerLogoPath;
        }

        public String getRedirectLink() {
            return RedirectLink;
        }

        public void setRedirectLink(String redirectLink) {
            RedirectLink = redirectLink;
        }

        public String getAddressLine1() {
            return AddressLine1;
        }

        public void setAddressLine1(String addressLine1) {
            AddressLine1 = addressLine1;
        }

        public String getAddressLine2() {
            return AddressLine2;
        }

        public void setAddressLine2(String addressLine2) {
            AddressLine2 = addressLine2;
        }

        public String getCity() {
            return City;
        }

        public void setCity(String city) {
            City = city;
        }

        public String getState() {
            return State;
        }

        public void setState(String state) {
            State = state;
        }

        public String getZipCode() {
            return ZipCode;
        }

        public void setZipCode(String zipCode) {
            ZipCode = zipCode;
        }

        public double getLatitude() {
            return Latitude;
        }

        public void setLatitude(double latitude) {
            Latitude = latitude;
        }

        public double getLongitude() {
            return Longitude;
        }

        public void setLongitude(double longitude) {
            Longitude = longitude;
        }

        public String getMobileNumber() {
            return MobileNumber;
        }

        public void setMobileNumber(String mobileNumber) {
            MobileNumber = mobileNumber;
        }

        public double getDistance() {
            return Distance;
        }

        public void setDistance(double distance) {
            Distance = distance;
        }

        public double getGuaranteed() {
            return Guaranteed;
        }

        public void setGuaranteed(double guaranteed) {
            Guaranteed = guaranteed;
        }

        public double getIndicative() {
            return Indicative;
        }

        public void setIndicative(double indicative) {
            Indicative = indicative;
        }

        public List<SearchResultForeignModel.Providers.WorkTime> getWorkTime() {
            return WorkTime;
        }

        public void setWorkTime(List<SearchResultForeignModel.Providers.WorkTime> workTime) {
            WorkTime = workTime;
        }

        public SearchResultForeignModel.Providers.Offer getOffer() {
            return Offer;
        }

        public void setOffer(SearchResultForeignModel.Providers.Offer offer) {
            Offer = offer;
        }

        public class WorkTime {

            @SerializedName("ID")
            private int ID;

            @SerializedName("LocationId")
            private int LocationId;

            @SerializedName("MobileNumber")
            private String MobileNumber;

            @SerializedName("WorkDay")
            private String WorkDay;

            @SerializedName("WorkStartTime")
            private String WorkStartTime;

            @SerializedName("WorkEndTime")
            private String WorkEndTime;

            @SerializedName("Status")
            private String Status;

            public int getID() {
                return ID;
            }

            public void setID(int ID) {
                this.ID = ID;
            }

            public int getLocationId() {
                return LocationId;
            }

            public void setLocationId(int locationId) {
                LocationId = locationId;
            }

            public String getMobileNumber() {
                return MobileNumber;
            }

            public void setMobileNumber(String mobileNumber) {
                MobileNumber = mobileNumber;
            }

            public String getWorkDay() {
                return WorkDay;
            }

            public void setWorkDay(String workDay) {
                WorkDay = workDay;
            }

            public String getWorkStartTime() {
                return WorkStartTime;
            }

            public void setWorkStartTime(String workStartTime) {
                WorkStartTime = workStartTime;
            }

            public String getWorkEndTime() {
                return WorkEndTime;
            }

            public void setWorkEndTime(String workEndTime) {
                WorkEndTime = workEndTime;
            }

            public String getStatus() {
                return Status;
            }

            public void setStatus(String status) {
                Status = status;
            }
        }

        public class Offer {

            @SerializedName("Id")
            private int Id;

            @SerializedName("PartnerId")
            private int PartnerId;

            @SerializedName("OfferName")
            private String OfferName;

            @SerializedName("OfferText")
            private String OfferText;

            @SerializedName("OfferType")
            private String OfferType;

            @SerializedName("OfferImagePath")
            private String OfferImagePath;

            public int getId() {
                return Id;
            }

            public void setId(int id) {
                Id = id;
            }

            public int getPartnerId() {
                return PartnerId;
            }

            public void setPartnerId(int partnerId) {
                PartnerId = partnerId;
            }

            public String getOfferName() {
                return OfferName;
            }

            public void setOfferName(String offerName) {
                OfferName = offerName;
            }

            public String getOfferText() {
                return OfferText;
            }

            public void setOfferText(String offerText) {
                OfferText = offerText;
            }

            public String getOfferType() {
                return OfferType;
            }

            public void setOfferType(String offerType) {
                OfferType = offerType;
            }

            public String getOfferImagePath() {
                return OfferImagePath;
            }

            public void setOfferImagePath(String offerImagePath) {
                OfferImagePath = offerImagePath;
            }
        }

    }
}
