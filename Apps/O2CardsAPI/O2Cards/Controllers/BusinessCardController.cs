using O2CardsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Http;

namespace O2CardsAppApi.Controllers
{
    public class BusinessCardController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(string emailAddress)
        {
            return "value";
        }

        public List<string> Post([FromBody]BusinessCard value)
        {
            #region Read Business Card Template
            string text;
            string basePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/Cards/");

            string fileName = value.Name.Replace(" ", "-") + "-" + DateTime.Now.ToString("yyyyMMddhhmmss");
            string dynamicFolder = RandomString(12);
            string htmlFolderName = basePath + dynamicFolder + "/" + fileName + ".html";
            string vCardFolderName = basePath + dynamicFolder + "/" + fileName + ".vcard";

            if (!Directory.Exists(basePath + dynamicFolder))
            {
                Directory.CreateDirectory(basePath + dynamicFolder);
            }

            BusinessCard card = value as BusinessCard;
            if (string.IsNullOrEmpty(value.TemplateName))
            {
                value.TemplateName = "o2card-horizontal.html";
            }
            string path = System.Web.Hosting.HostingEnvironment.MapPath(@"/App_Data/templates/" + value.TemplateName);
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                text = streamReader.ReadToEnd();
            }
            #endregion

            #region Write VCard File
            var vc = new VCard();
            vc.FirstName = value.Name;
            vc.Email = value.EmailAddress;
            vc.Mobile = value.MobileNumber;
            vc.CountryName = value.Country;
            vc.HomePage = value.Website;
            vc.StreetAddress = value.AddressLine1;
            vc.Organization = value.CompanyName;
            vc.JobTitle = value.Designation;
            vc.Zip = value.ZipCode;
            vc.City = value.City;

            string vCardFile = vc.ToString();
            using (StreamWriter outputFile = new StreamWriter(vCardFolderName))
            {
                outputFile.WriteLine(vCardFile);
            }
            #endregion

            #region Write HTML file 
            string template = text.Replace("~~~Name~~~", value.Name);
            template = template.Replace("~~~LogoImage~~~", value.LogoImage);
            template = template.Replace("~~~MarketingAttachment~~~", value.MarketingAttachment);
            template = template.Replace("~~~FacebookLink~~~", value.FacebookLink);
            template = template.Replace("~~~YoutubeLink~~~", value.TwitterLink);
            template = template.Replace("~~~TwitterLink~~~", value.YoutubeLink);
            template = template.Replace("~~~VCard~~~", ConfigurationManager.AppSettings["BaseUrl"] + dynamicFolder + "/" + fileName + ".vcard");

            using (StreamWriter outputFile = new StreamWriter(htmlFolderName))
            {
                outputFile.WriteLine(template);
            }
            #endregion

            return new List<string>
            {
                dynamicFolder,
                ConfigurationManager.AppSettings["BaseUrl"] + dynamicFolder + "/" + fileName + ".html",
                ConfigurationManager.AppSettings["BaseUrl"] + dynamicFolder + "/" + fileName + ".vcard"
            };
        }

        public string RandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }
    }
}