using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AA.LogManager;
using System.Configuration;
using System.Collections;
using System.Net.Mail;
//using AA.Exam.ConfigurationManager;
using AA.DAO;
using AA.ConfigurationsManager;
using Util;

namespace AA.DAO
{
    public class EmailManager
    {
        //send an email to Resion User when Multiple user is created, with user details attachment
        public static void UserCreation_SendEmailToRegionUser(string to, string from, string subject, string attachmentPath)
        {
            try
            {
                EmailDetails emaildet = Configurations.GetEmailSettings();
                Hashtable args = new Hashtable();
                args.Add("To", to);
                args.Add("From", from);
                args.Add("Subject", subject);
                args.Add("AttachmentPath", attachmentPath);

                //Logger.Debug("EmailManager", "UserCreation_SendEmailToRegionUser", " Sending Email to region user", args);
                if (emaildet.IsMailSend)
                {
                    //Logger.Debug("EmailManager", "UserCreation_SendEmailToRegionUser", " isMailSend in webconfig is true");
                    MailMessage mm = new MailMessage();
                    mm.From = new MailAddress(emaildet.EmailAddress, emaildet.EmailFromDisplayName);
                    mm.To.Add(to);
                    mm.CC.Add(emaildet.EmailAddress);

                    mm.Body = "Welcome .";
                    mm.Subject = subject;
                    mm.IsBodyHtml = true;
                    //mm.Attachments.Add(new Attachment(attachmentPath));

                    //create smtpclient object initialize with host and port and credential
                    SmtpClient mySmtpClient = new SmtpClient();

                    mySmtpClient.Port = int.Parse(emaildet.Port);
                    mySmtpClient.UseDefaultCredentials = emaildet.EmailUseDefaultCredentials;
                    mySmtpClient.EnableSsl = emaildet.IsSSLEnable;
                    mySmtpClient.Credentials = new System.Net.NetworkCredential(emaildet.UserName, emaildet.Password);
                    mySmtpClient.Host = emaildet.EmailHost;
                    mySmtpClient.Send(mm);
                    //Logger.Debug("EmailManager", "UserCreation_SendEmailToRegionUser", " Sended Email to region user");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("");
            }
        }

        /// <summary>
        /// this is methos used by senging an email to new user
        /// </summary>        
        public static bool UserCreation_SendEmailToNewUser(string to, string userName, string password)
        {
            try
            {
                EmailDetails emaildet = Configurations.GetEmailSettings();
                Hashtable args = new Hashtable();
                args.Add("To", to);
                args.Add("UserName", userName);
                args.Add("Password", password);
                //Logger.Debug("EmailManager", "UserCreation_SendEmailToNewUser", " Mailing password to user  ", args);
                if (emaildet.IsMailSend)
                {
                    //Logger.Debug("EmailManager", "UserCreation_SendEmailToNewUser", " isMailSend in webconfig is true");
                    string mailSubject = emaildet.NewUserEmailSubject;
                    string body = string.Format("Hello,<br/><br/>You have been invited by Administrator (DMS). <br /><br />Please use following credentials to login into the system:<br /><br/><b>User name - {0}<br/>Password - {1}</b><br/><br/>Login address - {2}<br/><br/>Contact <a href='mailto:'" + emaildet.EmailAddress + "'>administrator</a> in case of any difficulties while login into the website.<br/><br/><br/>Thank You,<br/>", userName, password, System.Configuration.ConfigurationManager.AppSettings["BASE_URL"] + "/Login.aspx");
                    //Logger.Debug("EmailManager", "UserCreation_SendEmailToNewUser", " Calling function SendEmail to send mail and returning the bool value");
                    return SendEmail(to, mailSubject, body, string.Empty);

                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("");
            }

            return true;
        }
        public static bool ForGotPassword_SendEmailToNewUser(string to, string userName, string password)
        {
            try
            {
                EmailDetails emaildet = Configurations.GetEmailSettings();
                Hashtable args = new Hashtable();
                args.Add("To", to);
                args.Add("UserName", userName);
                args.Add("Password", password);
                //Logger.Debug("EmailManager", "UserCreation_SendEmailToNewUser", " Mailing password to user  ", args);
                if (emaildet.IsMailSend)
                {
                    //Logger.Debug("EmailManager", "UserCreation_SendEmailToNewUser", " isMailSend in webconfig is true");
                    string mailSubject = "DMS - Password Changed";
                    string body = string.Format("Hello,<br/><br/>Your password has been changed. <br /><br />Please use following credentials to login into the system:<br /><br/><b>User name - {0}<br/>Password - {1}</b><br/><br/>Login address - {2}<br/><br/>Contact <a href='mailto:'" + emaildet.EmailAddress + "'>administrator</a> in case of any difficulties while login into the website.<br/><br/><br/>Thank You,<br/> DMS <br/><br/>", userName, password, System.Configuration.ConfigurationManager.AppSettings["BASE_URL"] + "/Login.aspx");
                    //Logger.Debug("EmailManager", "UserCreation_SendEmailToNewUser", " Calling function SendEmail to send mail and returning the bool value");
                    return SendEmail(to, mailSubject, body, string.Empty);

                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("");
            }

            return true;
        }
        /// <summary>
        /// this is method used by sending an email to new created user also send a copy to admin user and region user.
        /// </summary> 
        /// 
        public static bool UserCreation_SendEmailToNewUser(UserDetails user)
        {
            try
            {
                EmailDetails emaildet = Configurations.GetEmailSettings();
                Hashtable args = new Hashtable();
                args.Add("To", user.Email);
                args.Add("UserName", user.Id);
                args.Add("Password", user.Password);
                args.Add("Verificationid", user.VerificationId);
                //Logger.Debug("EmailManager", "UserCreation_SendEmailToNewUser", " Mailing New User details to new user and region  ", args);

                if (emaildet.IsMailSend)
                {
                    //Logger.Debug("EmailManager", "UserCreation_SendEmailToRegionUser", " isMailSend in webconfig is true");
                    string mailSubject = "DMS - Verification Email";
                    string body = string.Format("Hello {0},<br/><br/>Thank you for signing up with DMS. . <br /><br />Please use following credentials to login into the system:<br /><br/><b>User name - {1}<br/>Password - {2}</b><br/><br/>  Click on the link to complete the verification process. - {3}<br/><br/>Contact <a href=\"mailto:" + emaildet.EmailAddress + "\">administrator</a> in case of any difficulties while login into the website.<br/><br/><br/>Thank You,<br/> DMS <br/>", user.FirstName, user.Id, user.Password, System.Configuration.ConfigurationManager.AppSettings["BASE_URL"] + "/Verification.aspx?userid=" + user.Id + "&unqueid=" + user.VerificationId);
                    //Logger.Debug("EmailManager", "UserCreation_SendEmailToNewUser", " Calling function SendEmail to send mail and returning the bool value");
                    return SendEmail(user.Email, mailSubject, body, "");
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("");
            }

            return true;
        }
        public static bool Send_TestEmailToSpecificEmail()
        {
            try
            {
                EmailDetails emaildet = Configurations.GetEmailSettings();
                //Logger.Debug("EmailManager", "Send_TestEmailToSpecificEmail", " Mailing test email  ");

                if (emaildet.IsMailSend)
                {
                    //Logger.Debug("EmailManager", "Send_TestEmailToSpecificEmail", " isMailSend in webconfig is true");
                    string mailSubject = "DMS - Test Email";
                    string body = string.Format("Hello Sir/Madam,<br/><br/>Your email setting has been updated successfully. <br />");
                    //Logger.Debug("EmailManager", "Send_TestEmailToSpecificEmail", " Calling function SendEmail to send mail and returning the bool value");
                    return SendEmail(emaildet.EmailAddress, mailSubject, body, "");
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("");
            }

            return true;
        }

        public static bool Send_ReminderEmailToStudent(string to, string bodyMessage)
        {
            try
            {
                EmailDetails emaildet = Configurations.GetEmailSettings();
                //Logger.Debug("EmailManager", "Send_ReminderEmailToStudent", " Mailing reminder email  ");

                if (emaildet.IsMailSend)
                {
                    //Logger.Debug("EmailManager", "Send_ReminderEmailToStudent", " isMailSend in webconfig is true");
                    string mailSubject = "DMS - Reminder Email";
                    string body = bodyMessage;
                    //Logger.Debug("EmailManager", "Send_ReminderEmailToStudent", " Calling function SendEmail to send reminder mail and returning the bool value");
                    if (string.IsNullOrEmpty(to))
                    {
                        return SendEmail(emaildet.EmailAddress, mailSubject, body,"");
                    }
                    else
                    {
                        return SendEmail(to, mailSubject, body, emaildet.EmailAddress);
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("");
            }

            return true;
        }

        public static bool UserCreation_SendEmailToNewUser(string to, string userName, string password, string verificationid)
        {
            try
            {
                EmailDetails emaildet = Configurations.GetEmailSettings();
                Hashtable args = new Hashtable();
                args.Add("To", to);
                args.Add("UserName", userName);
                args.Add("Password", password);
                args.Add("Verificationid", verificationid);
                //Logger.Debug("EmailManager", "UserCreation_SendEmailToNewUser", " Mailing New User details to new user and region  ", args);

                if (emaildet.IsMailSend)
                {
                    //Logger.Debug("EmailManager", "UserCreation_SendEmailToRegionUser", " isMailSend in webconfig is true");
                    string mailSubject = "DMS - Verification Email";
                    string body = string.Format("Hello,<br/><br/>You have been invited by Administrator . <br /><br />Please use following credentials to login into the system:<br /><br/><b>User name - {0}<br/>Password - {1}</b><br/><br/>  Please Click the Following Link  For Veification - {2}<br/><br/>Contact <a href=\"mailto:" + emaildet.EmailAddress + "\">administrator</a> in case of any difficulties while login into the website.<br/><br/><br/>Thank You,<br/>", userName, password, System.Configuration.ConfigurationManager.AppSettings["BASE_URL"] + "/Verification.aspx?userid=" + userName + "&unqueid=" + verificationid);
                    //Logger.Debug("EmailManager", "UserCreation_SendEmailToNewUser", " Calling function SendEmail to send mail and returning the bool value");
                    return SendEmail(to, mailSubject, body, "");
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("");
            }

            return true;
        }
        public static bool Userpasswordchange_SendEmailUser(string to, string userName, string password, string regionUserAddress)
        {
            try
            {
                EmailDetails emaildet = Configurations.GetEmailSettings();
                Hashtable args = new Hashtable();
                args.Add("To", to);
                args.Add("UserName", userName);
                args.Add("Password", password);
                args.Add("RegionUserAddress", regionUserAddress);
                //Logger.Debug("EmailManager", "Userpasswordchange_SendEmailToNewUser", " Mailing  User details to  user and region  ", args);
                if (emaildet.IsMailSend)
                {
                    //Logger.Debug("EmailManager", "Userpasswordchange_SendEmailToNewUser", " isMailSend in webconfig is true");
                    string mailSubject = emaildet.NewUserEmailSubject;
                    string body = string.Format("Hello,<br/><br/>You have been invited by Administrator (DVET). <br /><br />Please use following credentials to login into the system:<br /><br/><b>User name - {0}<br/>Password - {1}</b><br/><br/>Login address - {2}<br/><br/>Contact <a href=\"mailto:" + emaildet.EmailAddress + "\">administrator</a> in case of any difficulties while login into the website.<br/><br/><br/>Thank You,<br/>", userName, password, System.Configuration.ConfigurationManager.AppSettings["BASE_URL"] + "/Login.aspx");
                    //Logger.Debug("EmailManager", "Userpasswordchange_SendEmailToNewUser", " Calling function SendEmail to send mail and returning the bool value");
                    return SendEmail(to, mailSubject, body, regionUserAddress);
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("");
            }

            return true;
        }

        private static bool SendEmail(string to, string subject, string body, string ccAddress)
        {
            try
            {
                EmailDetails emaildet = Configurations.GetEmailSettings();
                Hashtable args = new Hashtable();
                args.Add("To", to);
                args.Add("subject", subject);
                args.Add("body", body);
                args.Add("ccAddress", ccAddress);

                //Logger.Debug("EmailManager", "SendEmail", " Sending mail ", args);

                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(emaildet.EmailAddress, emaildet.EmailFromDisplayName);
                mm.To.Add(to);
                mm.CC.Add(emaildet.EmailAddress);

                if (!string.IsNullOrEmpty(ccAddress))
                {
                    mm.CC.Add(ccAddress);
                }

                mm.Body = body;
                mm.Subject = subject;
                mm.IsBodyHtml = true;

                SmtpClient mySmtpClient = new SmtpClient();
                //smtp.Host = "smtp.gmail.com";
                //smtp.Port = 25;
                //smtp.EnableSsl = true;
                mySmtpClient.Port = 587;
                mySmtpClient.UseDefaultCredentials = emaildet.EmailUseDefaultCredentials;
                mySmtpClient.EnableSsl = emaildet.IsSSLEnable;
                mySmtpClient.Credentials = new System.Net.NetworkCredential(emaildet.UserName, emaildet.Password);
                mySmtpClient.Host = emaildet.EmailHost;
                mySmtpClient.Send(mm);
                //Logger.Debug("EmailManager", "SendEmail", " Sended mail and returning true value ");
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }


        }

        public static bool FileSharing_EmailToUser(string to, string message, string logPath)
        {
            try
            {
                EmailDetails emaildet = Configurations.GetEmailSettings();
                Hashtable args = new Hashtable();
                args.Add("To", to);
                args.Add("message", message);

                //logger.Debug("EmailManager", "Userpasswordchange_SendEmailToNewUser", " Mailing  User details to  user and region  ", args);
                //if (emaildet.IsMailSend)
                //{
                //logger.Debug("EmailManager", "Userpasswordchange_SendEmailToNewUser", " isMailSend in webconfig is true");
                string mailSubject = "File Sharing Details";
                // string body = string.Format("Hello,<br/><br/>You have been invited by Administrator (DVET). <br /><br />Please use following credentials to login into the system:<br /><br/><b>User name - {0}<br/>Password - {1}</b><br/><br/>Login address - {2}<br/><br/>Contact <a href=\"mailto:" + emaildet.EmailAddress + "\">administrator</a> in case of any difficulties while login into the website.<br/><br/><br/>Thank You,<br/>DVET", userName, password, ConfigurationManager.AppSettings["BASE_URL"] + "/Login.aspx");
                string body = string.Format(message);
                //logger.Debug("EmailManager", "Userpasswordchange_SendEmailToNewUser", " Calling function SendEmail to send mail and returning the bool value");
                return SendEmail(to, mailSubject, body, "");
                //}
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("");
            }

            return true;
        }
    }
}
