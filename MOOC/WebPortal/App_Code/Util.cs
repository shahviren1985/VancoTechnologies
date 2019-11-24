using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net.Mail;
using ITM.Courses.LogManager;
using System.Net;
using System.Security.Cryptography;
using System.Collections;
using System.Text;
using System.IO;
using System.Net.Mime;

/// <summary>
/// Summary description for Util
/// </summary>
public class Util
{
    public static string BASE_URL = ConfigurationManager.AppSettings["BASE_URL"];
    public static bool IsAdminLoggedIn;

    public Util()
    {
        BASE_URL = ConfigurationManager.AppSettings["BASE_URL"];
    }

    public Util(Uri uri)
    {
        //string[] hostArray = uri.Host.Split(new char[] { '.' });

        //string baseUrl = ConfigurationManager.AppSettings["BASE_URL"];

        //string[] baseUrlArray = baseUrl.Split(new char[] { '/' });

        //string newUrl = null;

        //newUrl = uri.Scheme + "://" + uri.Host + ":" + uri.Port + "/" + baseUrlArray[3];
        //if (!string.IsNullOrEmpty(baseUrlArray[3]))
        //{
        //    newUrl += "/";
        //}

        //BASE_URL = newUrl;

        BASE_URL = ConfigurationManager.AppSettings["BASE_URL"];
    }

    public string GetAutoGeneratePassword(int length)
    {
        var charArray = new List<Char>(Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (Char)i).ToArray());

        string password = string.Empty;

        Random rd = new Random();

        for (int i = 0; i < length; i++)
        {
            int index = rd.Next(0, 25);
            password = password + charArray[index];
        }

        return password;
    }

    /* Chapter Complete */
    public string ChpaterCompletedValue(int chapterId, int chapterCount)
    {
        string val = "Step ";

        if (chapterId <= 7 && chapterId >= 1)
        {
            val += "1, ";
        }
        else if (chapterId <= 10 && chapterId >= 8)
        {
            val += "2, ";
        }
        else if (chapterId <= 14 && chapterId >= 11)
        {
            val += "3, ";
        }

        val += "Chapter " + chapterId + " out of " + chapterCount;

        return val;
    }

    /* send email */
    public void SendMail(string sToAddress, string sFromAddress, string sSubject, string sBody)
    {
        /* get email configuration setting from web.config file */
        sFromAddress = ConfigurationManager.AppSettings["FromAddress"];
        string sUser = ConfigurationManager.AppSettings["User"];
        string sPassword = ConfigurationManager.AppSettings["Password"];
        string sHost = ConfigurationManager.AppSettings["Host"];
        int iPort = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
        bool bUseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["UseDefaultCredentials"]);
        bool bEnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
        /* get CC email addressess form web.config file */
        string cc1 = ConfigurationManager.AppSettings["ToCC_Hrishi"];
        string cc2 = ConfigurationManager.AppSettings["ToCC_Shrikant"];
        string cc3 = ConfigurationManager.AppSettings["ToCC_Rajesh"];
        string cc4 = ConfigurationManager.AppSettings["ToCC_Viren"];

        try
        {
            System.Net.Mail.MailMessage oMessage = new System.Net.Mail.MailMessage();
            /* Add to address */
            oMessage.To.Add(sToAddress);

            /* Add CC addressess */
            if (!string.IsNullOrEmpty(cc1) && !string.IsNullOrEmpty(cc2))
            {
                oMessage.Bcc.Add(cc1);
                oMessage.Bcc.Add(cc2);
            }

            if (!string.IsNullOrEmpty(cc3) && !string.IsNullOrEmpty(cc4))
            {
                oMessage.Bcc.Add(cc3);
                oMessage.Bcc.Add(cc4);
            }

            oMessage.To.Add(sToAddress);
            /* Add from address */
            oMessage.From = new MailAddress(sFromAddress, ConfigurationManager.AppSettings["MailDisplayName"]);
            /* Add subject*/
            oMessage.Subject = sSubject;
            /* Add body */
            oMessage.Body = HttpUtility.UrlDecode(sBody, System.Text.Encoding.Default); ;

            oMessage.IsBodyHtml = true;

            bool type = bool.Parse(ConfigurationManager.AppSettings["IsMailingTypeGMail"]);

            if (type)
            {
                SendGmail(oMessage);
            }
            else
            {
                SendGoDaddy(oMessage);
            }

            /*System.Net.Mail.SmtpClient oSmtpClient = new System.Net.Mail.SmtpClient();
            oSmtpClient.UseDefaultCredentials = bUseDefaultCredentials;
            oSmtpClient.Host = sHost;
            oSmtpClient.Port = iPort;
            /* Set Credentials * /
            oSmtpClient.Credentials = new System.Net.NetworkCredential(sUser, sPassword);
            oSmtpClient.EnableSsl = bEnableSsl;
            oSmtpClient.Send(oMessage);*/
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /* Send mail overload function for add CC recipts*/
    public void SendMail(string sToAddress, string sFromAddress, string sCC, string sSubject, string sBody)
    {
        /* get email configuration setting from web.config file */
        sFromAddress = ConfigurationManager.AppSettings["FromAddress"];
        string sUser = ConfigurationManager.AppSettings["User"];
        string sPassword = ConfigurationManager.AppSettings["Password"];
        string sHost = ConfigurationManager.AppSettings["Host"];
        int iPort = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
        bool bUseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["UseDefaultCredentials"]);
        bool bEnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
        /* get CC email addressess form web.config file */
        string cc1 = sCC;

        try
        {
            System.Net.Mail.MailMessage oMessage = new System.Net.Mail.MailMessage();
            /* Add to address */
            oMessage.To.Add(sToAddress);

            /* Add CC addressess */
            if (!string.IsNullOrEmpty(cc1))
            {
                oMessage.Bcc.Add(cc1);
            }


            oMessage.To.Add(sToAddress);
            /* Add from address */
            oMessage.From = new MailAddress(sFromAddress, ConfigurationManager.AppSettings["MailDisplayName"]);
            /* Add subject*/
            oMessage.Subject = sSubject;
            /* Add body */
            oMessage.Body = HttpUtility.UrlDecode(sBody, System.Text.Encoding.Default); ;

            oMessage.IsBodyHtml = true;

            bool type = bool.Parse(ConfigurationManager.AppSettings["IsMailingTypeGMail"]);

            if (type)
            {
                SendGmail(oMessage);
            }
            else
            {
                SendGoDaddy(oMessage);
            }

            /*System.Net.Mail.SmtpClient oSmtpClient = new System.Net.Mail.SmtpClient();
            oSmtpClient.UseDefaultCredentials = bUseDefaultCredentials;
            oSmtpClient.Host = sHost;
            oSmtpClient.Port = iPort;
            /* Set Credentials * /
            oSmtpClient.Credentials = new System.Net.NetworkCredential(sUser, sPassword);
            oSmtpClient.EnableSsl = bEnableSsl;
            oSmtpClient.Send(oMessage);*/
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /* Send mail overload function for Add attachments , List<string> attachmets*/
    public void SendMail(string sToAddress, string sFromAddress, string sCC, string sSubject, string sBody, List<string> attachments)
    {
        /* get email configuration setting from web.config file */
        sFromAddress = ConfigurationManager.AppSettings["FromAddress"];
        string sUser = ConfigurationManager.AppSettings["User"];
        string sPassword = ConfigurationManager.AppSettings["Password"];
        string sHost = ConfigurationManager.AppSettings["Host"];
        int iPort = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
        bool bUseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["UseDefaultCredentials"]);
        bool bEnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
        /* get CC email addressess form web.config file */
        string cc1 = sCC;

        try
        {
            System.Net.Mail.MailMessage oMessage = new System.Net.Mail.MailMessage();
            /* Add to address */
            oMessage.To.Add(sToAddress);

            /* Add CC addressess */
            if (!string.IsNullOrEmpty(cc1))
            {
                oMessage.Bcc.Add(cc1);
            }


            oMessage.To.Add(sToAddress);
            /* Add from address */
            oMessage.From = new MailAddress(sFromAddress, ConfigurationManager.AppSettings["MailDisplayName"]);
            /* Add subject*/
            oMessage.Subject = sSubject;
            /* Add body */
            oMessage.Body = HttpUtility.UrlDecode(sBody, System.Text.Encoding.Default); ;

            if (attachments != null)
            {
                foreach (string attachment in attachments)
                {
                    if (File.Exists(attachment))
                    {
                        //Attachment file = new Attachment(attachment, MediaTypeNames.Application.Octet); //for refference
                        Attachment file = new Attachment(attachment);
                        oMessage.Attachments.Add(file);
                    }
                }
            }

            oMessage.IsBodyHtml = true;

            bool type = bool.Parse(ConfigurationManager.AppSettings["IsMailingTypeGMail"]);

            if (type)
            {
                SendGmail(oMessage);
            }
            else
            {
                SendGoDaddy(oMessage);
            }
        }
        catch (Exception ex)
        {
            //throw ex;
        }
    }

    /* Send mail overload function for sending to multiple addressess with multiple cc adressess Add attachments , List<string> attachmets*/
    public void SendMail(List<string> lToAddress, string sFromAddress, List<string> lCC, string sSubject, string sBody, List<string> attachments)
    {
        /* get email configuration setting from web.config file */
        sFromAddress = ConfigurationManager.AppSettings["FromAddress"];
        string sUser = ConfigurationManager.AppSettings["User"];
        string sPassword = ConfigurationManager.AppSettings["Password"];
        string sHost = ConfigurationManager.AppSettings["Host"];
        int iPort = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
        bool bUseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["UseDefaultCredentials"]);
        bool bEnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);

        try
        {
            System.Net.Mail.MailMessage oMessage = new System.Net.Mail.MailMessage();
            
            /* Add to address */
            if (lToAddress != null)
            {
                foreach (string to in lToAddress)
                {
                    oMessage.To.Add(to);
                }
            }
            else
            {
                //return if empty list for to address
                return;
            }

            /* Add CC addressess */
            if (lCC != null)
            {
                foreach (string cc in lCC)
                {
                    oMessage.Bcc.Add(cc);
                }
            }

            /* Add from address */
            oMessage.From = new MailAddress(sFromAddress, ConfigurationManager.AppSettings["MailDisplayName"]);
            /* Add subject*/
            oMessage.Subject = sSubject;
            /* Add body */
            oMessage.Body = HttpUtility.UrlDecode(sBody, System.Text.Encoding.Default); ;

            if (attachments != null)
            {
                foreach (string attachment in attachments)
                {
                    if (File.Exists(attachment))
                    {
                        //Attachment file = new Attachment(attachment, MediaTypeNames.Application.Octet); //for refference
                        Attachment file = new Attachment(attachment);
                        oMessage.Attachments.Add(file);
                    }
                }
            }

            oMessage.IsBodyHtml = true;

            bool type = bool.Parse(ConfigurationManager.AppSettings["IsMailingTypeGMail"]);

            if (type)
            {
                SendGmail(oMessage);
            }
            else
            {
                SendGoDaddy(oMessage);
            }
        }
        catch (Exception ex)
        {
            //throw ex;
        }
    }

    public void SendMailForScheduleDemo(string sToAddress, string sFromAddress, string sSubject, string sBody, string mailDisplayName)
    {
        /* get email configuration setting from web.config file */
        sFromAddress = ConfigurationManager.AppSettings["FromAddress"];
        string sUser = ConfigurationManager.AppSettings["User"];
        string sPassword = ConfigurationManager.AppSettings["Password"];
        string sHost = ConfigurationManager.AppSettings["Host"];
        int iPort = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
        bool bUseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["UseDefaultCredentials"]);
        bool bEnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
        /* get CC email addressess form web.config file */
        string cc1 = ConfigurationManager.AppSettings["ToCC_Hrishi"];
        string cc2 = ConfigurationManager.AppSettings["ToCC_Shrikant"];
        string cc3 = ConfigurationManager.AppSettings["ToCC_Rajesh"];
        string cc4 = ConfigurationManager.AppSettings["ToCC_Viren"];

        try
        {
            System.Net.Mail.MailMessage oMessage = new System.Net.Mail.MailMessage();
            /* Add to address */
            oMessage.To.Add(sToAddress);

            /* Add CC addressess 
            if (!string.IsNullOrEmpty(cc1) && !string.IsNullOrEmpty(cc2))
            {
                oMessage.Bcc.Add(cc1);
                oMessage.Bcc.Add(cc2);
            }

            if (!string.IsNullOrEmpty(cc3) && !string.IsNullOrEmpty(cc4))
            {
                oMessage.Bcc.Add(cc3);
                oMessage.Bcc.Add(cc4);
            }

            if (!string.IsNullOrEmpty(cc5))
            {
                oMessage.Bcc.Add(cc5);
            }
            */

            oMessage.To.Add(sToAddress);
            /* Add from address */
            if (string.IsNullOrEmpty(mailDisplayName))
            {
                mailDisplayName = ConfigurationManager.AppSettings["MailDisplayName"];
            }

            oMessage.From = new MailAddress(sFromAddress, mailDisplayName);
            /* Add subject*/
            oMessage.Subject = sSubject;
            /* Add body */
            oMessage.Body = sBody;

            oMessage.IsBodyHtml = true;

            bool type = bool.Parse(ConfigurationManager.AppSettings["IsMailingTypeGMail"]);

            if (type)
            {
                SendGmail(oMessage);
            }
            else
            {
                SendGoDaddy(oMessage);
            }

            /*System.Net.Mail.SmtpClient oSmtpClient = new System.Net.Mail.SmtpClient();
            oSmtpClient.UseDefaultCredentials = bUseDefaultCredentials;
            oSmtpClient.Host = sHost;
            oSmtpClient.Port = iPort;
            /* Set Credentials * /
            oSmtpClient.Credentials = new System.Net.NetworkCredential(sUser, sPassword);
            oSmtpClient.EnableSsl = bEnableSsl;
            oSmtpClient.Send(oMessage);*/
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private static void SendGoDaddy(MailMessage message)
    {
        try
        {
            message.From = new MailAddress(ConfigurationManager.AppSettings["FromAddress"], ConfigurationManager.AppSettings["MailDisplayName"]);

            SmtpClient smtp = new SmtpClient();

            smtp.Host = ConfigurationManager.AppSettings["Host"];
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["User"], ConfigurationManager.AppSettings["Password"]);

            smtp.Send(message);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    private static void SendGmail(MailMessage message)
    {
        try
        {
            string sUser = ConfigurationManager.AppSettings["User"];
            string sPassword = ConfigurationManager.AppSettings["Password"];
            string sHost = ConfigurationManager.AppSettings["Host"];
            int iPort = Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);
            bool bUseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["UseDefaultCredentials"]);
            bool bEnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);


            System.Net.Mail.SmtpClient oSmtpClient = new System.Net.Mail.SmtpClient();
            oSmtpClient.UseDefaultCredentials = bUseDefaultCredentials;
            oSmtpClient.Host = sHost;
            oSmtpClient.Port = iPort;
            /* Set Credentials */
            oSmtpClient.Credentials = new System.Net.NetworkCredential(sUser, sPassword);
            oSmtpClient.EnableSsl = bEnableSsl;
            oSmtpClient.Send(message);



            //int portNumber = int.Parse(ConfigurationManager.AppSettings["Port"]);
            //System.Net.Mail.SmtpClient sc = new SmtpClient(ConfigurationManager.AppSettings["Host"], portNumber);
            //sc.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["User"], ConfigurationManager.AppSettings["Password"]);
            //sc.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
            //sc.UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["UseDefaultCredentials"]);

            //sc.Send(message);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    #region Encryption and Decryptions
    /* Encrypt data  */
    /*(calling fucntion) string outPut = Decrypt(Nornal-Data, "Pas5pr@se", "s@1tValue", "MD5", 5, "@1B2c3D4e5F6g7H8", 256);*/
    public static string Encrypt(string plainText)
    {
        string passPhrase = "Pas5pr@se";
        string saltValue = "s@1tValue";
        string hashAlgorithm = "MD5";
        int passwordIterations = 5;
        string initVector = "@1B2c3D4e5F6g7H8";
        int keySize = 256;

        try
        {
            //create array of bytes
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string cipherText = Convert.ToBase64String(cipherTextBytes);
            return cipherText;
        }
        catch (Exception ex)
        {
            return "";
        }
    }

    /* Decrypt Encrypted data  */
    /*(calling fucntion) string outPut = Decrypt(Encrypted-Data, "Pas5pr@se", "s@1tValue", "MD5", 5, "@1B2c3D4e5F6g7H8", 256);*/
    public static string Decrypt(string cipherText)
    {
        string passPhrase = "Pas5pr@se";
        string saltValue = "s@1tValue";
        string hashAlgorithm = "MD5";
        int passwordIterations = 5;
        string initVector = "@1B2c3D4e5F6g7H8";
        int keySize = 256;

        try
        {
            Hashtable args = new Hashtable();
            args.Add("cipherText", cipherText);
            args.Add("passPhrase", passPhrase);
            args.Add("saltValue", saltValue);
            args.Add("hashAlgorithm", hashAlgorithm);
            args.Add("passwordIterations", passwordIterations);
            args.Add("initVector", initVector);
            args.Add("keySize", keySize);
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

            return plainText;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
}