namespace AdmissionForm.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Project Configuration used to initialize all configuration setting
    /// </summary>
    public class ProjectConfiguration
    {
        #region Variable

        /// <summary>
        /// Gets English Culture Code
        /// </summary>
        public static string EnglishCultureCode
        {
            get
            {
                return "en-GB";
            }
        }

        #endregion

        #region Configuration Settings


        /// <summary>
        /// Gets the Configuration From Email Address
        /// </summary>
        public static string FormExpireDate
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FormExpireDate"].String();
            }
        }

        public static double RemeberMeLoginTime
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["RemeberMeLoginTime"].ToDouble();
            }

        }


        /// <summary>
        /// Gets the Configuration BitCoinCurrentPriceAPI
        /// </summary>
        public static string BitCoinCurrentPriceAPI
        {
            get
            {
                return ConfigurationManager.AppSettings["BitCoinCurrentPriceAPI"].String();
            }
        }

        /// <summary>
        /// Gets the Configuration BitCoinTransactionStatusAPI
        /// </summary>
        public static string BitCoinTransactionStatusAPI
        {
            get
            {
                return ConfigurationManager.AppSettings["BitCoinTransactionStatusAPI"].String();
            }
        }

        /// <summary>
        /// Gets the Configuration BitCoinGetBlockCountAPI
        /// </summary>
        public static string BitCoinGetBlockCountAPI
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["BitCoinGetBlockCountAPI"].String();
            }
        }

        /// <summary>
        /// Gets the Configuration CurrencyLayerAPI
        /// </summary>
        public static string CurrencyLayerAPI
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["CurrencyLayerAPI"].String();
            }
        }


        public static string WalletAPIURL
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["WalletAPIURL"].String();
            }

        }


        public static string BitcoinPUBKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["BitcoinPUBKey"].String();
            }

        }

        public static string BitcoinAPIPassword
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["BitcoinAPIPassword"].String();
            }

        }

        public static string BitcoinAPIPassword2
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["BitcoinAPIPassword2"].String();
            }

        }

        public static string SendPaymentAPIURL
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["SendPaymentAPIURL"].String();
            }

        }

        public static string MerchatAPIAccountURL
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["MerchatAPIAccountURL"].String();
            }

        }

        public static string BitcoinID
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["BitcoinID"].String();
            }

        }

        public static string BitcoinAPIKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["BitcoinAPIKey"].String();
            }

        }
       
        public static string BitcoinAddressCalbackURL
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["BitcoinAddressCalbackURL"].String();
            }

        }

        public static string OtherWebsiteURL
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["OtherWebsiteURL"].String();
            }

        }



        /// <summary>
        /// Gets Return Culture
        /// </summary>
        public static string Culture
        {
            get
            {
                string strCutureCode = EnglishCultureCode;
                try
                {
                    if (!Convert.ToString(ConfigurationManager.AppSettings["Culture"]).ToLower().IsEmpty())
                    {
                        strCutureCode = ConfigurationManager.AppSettings["Culture"].String();
                    }
                }
                catch (Exception)
                {
                }

                return strCutureCode;
            }
        }

        //public static string PaymentAmount
        //{
        //    get
        //    {
        //        return System.Configuration.ConfigurationManager.AppSettings["PaymentAmount"].String();
        //    }

        //}


        #endregion

        #region Format
        /// <summary>
        /// Gets the Date Format
        /// </summary>
        public static string DateFormatDisplay
        {
            get
            {
                return "dd MMM, yyyy";
            }
        }

        /// <summary>
        /// Gets the Date Format
        /// </summary>
        public static string DateFormatString
        {
            get
            {
                return "dd-MMM-yyyy";
            }
        }


        /// <summary>
        /// Gets the Date Format
        /// </summary>
        public static string DateFormat
        {
            get
            {
                return "yyyy-MM-dd";
            }
        }

        /// <summary>
        /// Gets the Date Format
        /// </summary>
        public static string DatePickerDateFormat
        {
            get
            {
                return "yyyy-MM-dd";
            }
        }

        /// <summary>
        /// Gets the Date Format
        /// </summary>
        public static string DateTimeFormat
        {
            get
            {
                //return "MM/dd/yyyy HH:mm";
                return "dd/MM/yyyy HH:mm";
            }
        }

        /// <summary>
        /// Gets the Date Format
        /// </summary>
        public static string DateTimeClientFormat
        {
            get
            {
                return "dd/MMM/yyyy hh:mm tt";
            }
        }

        /// <summary>
        /// Gets the Date Format
        /// </summary>
        public static string CurrencyFormate
        {
            get
            {
                return "{0:0.##}";
            }
        }

        /// <summary>
        /// Gets the Decimal Place
        /// </summary>
        public static int DecimalPlace
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// Gets the number Format
        /// </summary>
        public static string NumberFormatWithSpace
        {
            get
            {
                return "# ### ### ### ##0.00";
            }
        }

        /// <summary>
        /// Gets the number Format
        /// </summary>
        public static string BitCoinFormat
        {
            get
            {
                return "##0.00######";
            }
        }

        /// <summary>
        /// Gets the Minimum Date
        /// </summary>
        public static string MinDate
        {
            get
            {
                return "1/1/1753 12:00:00 AM";
            }
        }

        /// <summary>
        /// Gets the Time Format
        /// </summary>
        public static string TimeFormat
        {
            get
            {
                return "HH:mm";
            }
        }

        /// <summary>
        /// Gets a page value 
        /// </summary>
        public static int PageSize
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("PageSize").ToInteger();
            }
        }

        #endregion

        #region System Path

        /// <summary>
        /// Gets the Root Path of the Project
        /// </summary>
        public static string ApplicationRootPath
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    string rootPath = HttpContext.Current.Server.MapPath("~");
                    if (rootPath.EndsWith("\\", StringComparison.CurrentCulture))
                    {
                        return rootPath;
                    }
                    else
                    {
                        return rootPath + "\\";
                    }
                }
                else
                {
                    return AppDomain.CurrentDomain.BaseDirectory;
                }
            }
        }

        /// <summary>
        /// Gets HostName
        /// </summary>
        public static string HostName
        {
            get { return HttpContext.Current.Request.Url.Host; }
        }

        /// <summary>
        /// Gets Secure User Base
        /// </summary>
        public static string SecureUrlBase
        {
            get
            {
                return "https://" + UrlSuffix;
            }
        }

        /// <summary>
        /// Gets Url Base
        /// </summary>
        public static string UrlBase
        {
            get
            {
                return "http://" + UrlSuffix;
            }
        }

        /// <summary>
        /// Gets Site Url Base
        /// </summary>
        public static string SiteUrlBase
        {
            get
            {
                if (HttpContext.Current.Request.IsSecureConnection)
                {
                    return SecureUrlBase;
                }
                else
                {
                    return UrlBase;
                }
            }
        }

        /// <summary>
        /// Gets Site Url Base
        /// </summary>
        public static string EmailRedirect
        {
            get
            {
                return SiteUrlBase + "email/redirect";
            }
        }

        /// <summary>
        /// Gets Site Url Base
        /// </summary>
        public static string GetIPAddress
        {
            get
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
        }

        /// <summary>
        /// Gets Secure User
        /// </summary>
        public static string SecureUrl
        {
            get
            {
                return "https://" + HostName;
            }
        }

        /// <summary>
        /// Gets Url
        /// </summary>
        public static string Url
        {
            get
            {
                return "http://" + HostName;
            }
        }

        /// <summary>
        /// Gets Site Url
        /// </summary>
        public static string SiteUrl
        {
            get
            {
                if (HttpContext.Current.Request.IsSecureConnection)
                {
                    return SecureUrl;
                }
                else
                {
                    return Url;
                }
            }
        }

        /// <summary>
        /// Gets Email Template Path
        /// </summary>
        public static string PhotoPath
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Server.MapPath("~/data/Photos/");
                else
                    return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data/Photos/");
            }
        }

        public static string SignaturePath
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Server.MapPath("~/data/Signature/");
                else
                    return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data/Signature/");
            }
        }

        public static string ParentSignaturePath
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Server.MapPath("~/data/ParentSignature/");
                else
                    return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data/ParentSignature/");
            }
        }


        public static string PDFPath
        {
            get
            {
                if (HttpContext.Current != null)
                    return HttpContext.Current.Server.MapPath("~/data/PDF/");
                else
                    return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data/PDF/");
            }
        } 


        /// <summary>
        /// Gets current Url 
        /// </summary>
        public static string CurrentUrl
        {
            get
            {
                return HttpContext.Current.Request.Url.ToString();
            }
        }

        /// <summary>
        /// Gets Url Suffix
        /// </summary>
        private static string UrlSuffix
        {
            get
            {
                string port = !string.IsNullOrEmpty(HttpContext.Current.Request.Url.Port.String()) && HttpContext.Current.Request.Url.Port != 80 ? ":" + HttpContext.Current.Request.Url.Port : "";
                if (HttpContext.Current.Request.ApplicationPath == "/")
                {
                    return HttpContext.Current.Request.Url.Host + port + HttpContext.Current.Request.ApplicationPath;
                }
                else
                {
                    return HttpContext.Current.Request.Url.Host + port + HttpContext.Current.Request.ApplicationPath + "/";
                }
            }
        } 

        #endregion

        #region Methods

        /// <summary>
        /// Add Exception For SCC Web
        /// </summary>
        /// <param name="e">Exception Value</param>
        /// <param name="functionName">Function Name</param>
        /// <returns>Return exception message</returns>
        public static string ExceptionMessage(Exception e, string functionName)
        {
            string exceptionMessage = string.Empty;
            try
            {
                exceptionMessage += Environment.NewLine + string.Format("----------------------------- {0} ------------------------------", DateTime.UtcNow.ToString(DateTimeFormat));
                exceptionMessage += Environment.NewLine + string.Format("Function Name : {0}", functionName);
                exceptionMessage += Environment.NewLine + string.Format("Exception Message : {0}", e.Message);

                if (e.InnerException != null)
                {
                    exceptionMessage += Environment.NewLine + string.Format("Exception Inner Exception  : {0}", e.InnerException.ToString());
                }

                exceptionMessage += Environment.NewLine + string.Format("Exception Source : {0}", e.Source);
                exceptionMessage += Environment.NewLine + string.Format("Exception Stack Trace : {0}", e.StackTrace);
                exceptionMessage += Environment.NewLine + string.Format("--------------------------------------------------------------");
            }
            catch (Exception)
            {
            }

            return exceptionMessage;
        }

        #endregion

    }
}
