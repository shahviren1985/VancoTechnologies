using System;
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
}

