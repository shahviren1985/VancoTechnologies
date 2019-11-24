using Newtonsoft.Json;
using RoshniAppWebApi.Helpers;
using RoshniAppWebApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Http;

namespace RoshniAppWebApi.Controllers
{
    public class ProductInquiry
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string PersonName { get; set; }
        public string MobileNumber { get; set; }

        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }

    }
    public class ProductList
    {
        public string lastModified { get; set; }
        public List<products> products { get; set; }
    }
    public class products
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string designer { get; set; }
        public string provider { get; set; }
        public string stoneType { get; set; }
        public string colorFilter { get; set; }
        public string productFilter { get; set; }
        public string mainImage { get; set; }
        public List<string> images { get; set; }
        public string availableQuantity { get; set; }
        public string price { get; set; }
        public List<string> availableInCity { get; set; }
        public string deliveryInDays { get; set; }
        public string courierCharges { get; set; }
    }
    public class ProductController : ApiController
    {
        DatabaseOperations op;
        public ProductController()
        {
            op = new DatabaseOperations();
        }

        [HttpGet]
        public HttpResponseMessage test()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "It Works");
        }

        [HttpGet]
        public HttpResponseMessage GetProducts()
        {
            string filePath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/products.json");
            string allText = System.IO.File.ReadAllText(filePath);
            ProductList response = JsonConvert.DeserializeObject<ProductList>(allText);
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        public HttpResponseMessage SaveInquiry(ProductInquiry products)
        {
            MailMessage message = new MailMessage();

            try
            {
                message.From = new MailAddress(products.EmailAddress);

                message.To.Add(new MailAddress(ConfigurationManager.AppSettings["DummyMessageEmail"]));
                message.IsBodyHtml = true;

                message.Subject = "Product Inquiry - " + products.ProductName + "(" + products.ProductId + ")";
                message.Body = "Person Name - " + products.PersonName + "<br/>";
                message.Body += "Mobile - " + products.MobileNumber + "<br/>";
                message.Body += "Address - " + products.Address + "<br/>";
                message.Body += "State - " + products.State + "<br/>";
                message.Body += "Pin Code - " + products.PinCode + "<br/>";
                message.Body += "Email Address - " + products.EmailAddress + "<br/>";
                message.Body += "Product Name - " + products.ProductName + "<br/>";
                message.Body += "Product Id - " + products.ProductId + "<br/>";

                SmtpClient client = new SmtpClient();
                client.Host = ConfigurationManager.AppSettings["EmailHost"];
                client.UseDefaultCredentials = bool.Parse(ConfigurationManager.AppSettings["UseDefaultCredentials"]);
                client.Send(message);
                return Request.CreateResponse(HttpStatusCode.OK, "Thank you for your inquiry, One of our executives will reach out to you.");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "An error occurred while sending your enquiry. " + ex.Message + ". " + ex.StackTrace);
            }

        }

    }
}
