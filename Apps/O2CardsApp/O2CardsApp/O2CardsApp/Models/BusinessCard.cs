using System;
using System.Collections.Generic;
using System.Text;

namespace O2CardsApp.Models
{
    public class BusinessCard
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string MobileNumber { get; set; }
        public string Designation { get; set; }
        public string CompanyName { get; set; }
        public string Website { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string YoutubeLink { get; set; }
        public string InstagramLink { get; set; }
        public string LinkedInLink { get; set; }
        public string PinterestLink { get; set; }
        public string LogoImage { get; set; }
        public List<string> Tags { get; set; }
        public string MarketingAttachment { get; set; }
        public string PromotionalVideo { get; set; }
        public string PresentationLink { get; set; }
        public string OtherAttachment { get; set; }
        public string TemplateName { get; set; }
    }
}
