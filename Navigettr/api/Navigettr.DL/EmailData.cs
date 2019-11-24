using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Navigettr.Data
{
    public class EmailData
    {
        public string ToEmailAdress { get; set; }
        public string FromEmailAddress { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
        public string FromPassword { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string TemplateFilePath { get; set; }
        public string LoginLink { get; set; }
        public bool EnableSSL { get; set; }
        public string ToPassword { get; set; }

        public static bool SendEmail(EmailData email)
        {
            try
            {
                var fromAddress = new MailAddress(email.FromEmailAddress, email.FromName);
                var toAddress = new MailAddress(email.ToEmailAdress, email.ToName);

                var smtp = new SmtpClient
                {
                    Host = email.HostName,
                    Port = email.Port,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    //UseDefaultCredentials = true
                    Credentials = new NetworkCredential(fromAddress.Address, email.FromPassword)
                };

                var message = new MailMessage();
                message.From = new MailAddress(email.FromEmailAddress, email.FromName);
                message.To.Add(toAddress);
                message.Subject = email.Subject;
                message.Body = email.Body;
                message.IsBodyHtml = true;
                smtp.Send(message);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
    }

    public class PartnerSettings
    {
        public int Id { get; set; }
        public string BrandName { get; set; }
        public string EmailAddress { get; set; }
        public string RedirectLink { get; set; }
        public string BrandLogoPath { get; set; }
    }

    public class SearchParams
    {
        public int UserId { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string OrderByColumn { get; set; }
        public string OrderByDirection { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string ServiceType { get; set; }
        public int SearchRadius { get; set; }
        public ServiceParam ServiceParams { get; set; }

    }

    public class ServiceParam
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public int Amount { get; set; }
        public string Operation { get; set; }
        public string Mode { get; set; }
    }

    public class ServiceProviders
    {
        public int TotalCount { get; set; }
        public List<SP_SearchServiceProviders_Result> Providers { get; set; }
    }

    public class SP_SearchServiceProviders_Result
    {
        public int ID { get; set; }
        public int PartnerId { get; set; }
        public string PartnerName { get; set; }
        public string PartnerLogoPath { get; set; }
        public string RedirectLink { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string MobileNumber { get; set; }
        public float Distance { get; set; }
        public float Guaranteed { get; set; }
        public float Indicative { get; set; }

        public List<SP_FetchLocationWorkTimeDetails_Result> WorkTime { get; set; }
        public OfferDetails Offer { get; set; }
    }

    public class OfferDetails
    {
        public int Id { get; set; }
        public int PartnerId { get; set; }
        public string OfferName { get; set; }
        public string OfferText { get; set; }
        public string OfferType { get; set; }
        public string OfferImagePath { get; set; }
    }
}

