using Navigettr.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HtmlAgilityPack;
using System.Web;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;
using Navigettr.Data;

namespace Navigettr.Services.Controllers
{
    public class RolesController : ApiController
    {
        private static readonly RoleRepository _repository = new RoleRepository();
        private static readonly IPartnerRepository _partnerRepository = new PartnerRepository();

        public class Role
        {
            public Int32 RoleId { get; set; }
            public string RoleName { get; set; }

        }

        [HttpPost]
        [HttpOptions]
        [Route("api/GetRoles")]

        public HttpResponseMessage GetRoles([FromBody]Role jsondata)
        {
            try
            {
                var data = _repository.GetRoles(jsondata.RoleId);

                if (data.Count > 0 && data != null)
                {

                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No Data found ");
                }


            }
            catch (Exception ex)
            {


                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "");
            }

        }


        [HttpGet]
        [HttpOptions]
        [Route("api/ScrapRates")]
        public void GetRates()
        {
            GetICICIRates();
            GetYesBankRates();
            GetSBIRates();
            GetHDFCRates();
            GetKotakRates();
            GetBOBRates();
        }

        [HttpGet]
        [HttpOptions]
        [Route("api/GetHDFCBranches")]
        public void GetHDFCBranches()
        {
            string url = "https://www.hdfcbank.com/branch-atm-locator?city_name=Bengaluru&locality_name=Bengaluru&radius=100&state_name=Karnataka&page=1";
            using (var client = new WebClient())
            {
                client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.33 Safari/537.36");
                string html = client.DownloadString(url);
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
            }
        }

        [HttpGet]
        [HttpOptions]
        [Route("api/GetICICIRates")]
        public void GetICICIRates()
        {
            using (var client = new WebClient())
            {
                /*client.Headers[HttpRequestHeader.ContentType] = "application/json";
                string html = client.UploadString("https://www.yesbank.in/LocateUsService/service/getBranch", "{\"location\":\"Mumbai\",\"Lat\":\"18\",\"Long\":\"72\", \"radius\": 100}");*/
                string html = client.DownloadString("https://instantforex.icicibank.com/instantforex/forms/MicroCardRateView.aspx");
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("/html[1]/body[1]/form[1]/table[1]/tr[2]/td[1]/div[1]/table[1]/tr[1]/td[1]/table[1]/tbody[1]/tr[1]/td[1]/div[2]/table[1]");

                foreach (var node in nodes)
                {
                    HtmlNodeCollection rows = node.SelectNodes("tr");

                    foreach (HtmlNode row in rows)
                    {
                        HtmlNodeCollection cells = row.SelectNodes("td");

                        if (cells == null)
                            continue;
                        string fromCurrency = cells[0].InnerText.Substring(cells[0].InnerText.IndexOf("(") + 1, 3);
                        string toCurrency = "INR";

                        string moneyToINR = cells[1].InnerText;
                        string foreignToINR = cells[3].InnerText;
                        string travelCardToINR = cells[4].InnerText;

                        string moneyFromINR = cells[6].InnerText;
                        string foreignFromINR = cells[7].InnerText;
                        string travelCardFromINR = cells[9].InnerText;

                        // insert into DB
                        int iciciPartnerId = 1160;

                        List<SP_FetchPartnerRates_Result1> partnerRateList = _partnerRepository.FetchPartnerRates(iciciPartnerId);
                        PartnerRate pr1 = new PartnerRate();
                        PartnerRate pr2 = new PartnerRate();

                        #region Money Transfer
                        if (partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "MoneyTransfer") != null)
                            pr1.Id = partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "MoneyTransfer").Id;

                        pr1.FromRate = fromCurrency;
                        pr1.ToRate = toCurrency;

                        if (moneyToINR == "-")
                            continue;

                        pr1.Guaranteed = double.Parse(moneyToINR);
                        pr1.Indicative = double.Parse(moneyToINR);
                        pr1.RateType = "MoneyTransfer";//"Forex"; // "TravelCard"
                        pr1.Status = "Active";
                        pr1.PartnerId = iciciPartnerId;

                        if (partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency && p.RateType.Trim() == "MoneyTransfer") != null)
                            pr2.Id = partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency && p.RateType.Trim() == "MoneyTransfer").Id;

                        pr2.FromRate = toCurrency;
                        pr2.ToRate = fromCurrency;
                        pr2.Guaranteed = double.Parse(moneyFromINR);
                        pr2.Indicative = double.Parse(moneyFromINR);
                        pr2.RateType = "MoneyTransfer";//"Forex"; // "TravelCard"
                        pr2.Status = "Active";
                        pr2.PartnerId = iciciPartnerId;

                        List<PartnerRate> rateList = new List<PartnerRate> { pr1, pr2 };
                        _partnerRepository.SaveUpdateRates(rateList);
                        #endregion

                        #region Forex
                        if (partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency && p.RateType.Trim() == "Forex") != null)
                        {
                            pr1.Id = partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency && p.RateType.Trim() == "Forex").Id;
                            pr2.Id = pr1.Id;

                            pr1.FromRate = toCurrency;
                            pr1.ToRate = fromCurrency;
                            pr1.Guaranteed = double.Parse(foreignFromINR);
                            pr1.Indicative = double.Parse(foreignFromINR);
                            pr1.RateType = "Forex";//"Forex"; // "TravelCard"
                            pr1.Status = "Active";
                            pr1.PartnerId = iciciPartnerId;

                            pr2.RateType = "Forex";
                            pr2.Guaranteed = double.Parse(foreignToINR);
                            pr2.Indicative = double.Parse(foreignToINR);

                            rateList = new List<PartnerRate> { pr1, pr2 };
                            _partnerRepository.SaveUpdateRates(rateList);
                        }
                        #endregion
                        if (partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency && p.RateType.Trim() == "TravelCard") != null)
                        {
                            pr1.Id = partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency && p.RateType.Trim() == "TravelCard").Id;
                            pr2.Id = pr1.Id;

                            pr1.FromRate = toCurrency;
                            pr1.ToRate = fromCurrency;
                            pr1.Guaranteed = double.Parse(travelCardFromINR);
                            pr1.Indicative = double.Parse(travelCardFromINR);
                            pr1.RateType = "TravelCard";//"Forex"; // "TravelCard"
                            pr1.Status = "Active";
                            pr1.PartnerId = iciciPartnerId;

                            pr2.RateType = "TravelCard";
                            pr2.Guaranteed = double.Parse(travelCardToINR);
                            pr2.Indicative = double.Parse(travelCardToINR);

                            rateList = new List<PartnerRate> { pr1, pr2 };
                            _partnerRepository.SaveUpdateRates(rateList);
                        }
                        #region Travel Card

                        #endregion


                    }
                }

            }

        }

        [HttpGet]
        [HttpOptions]
        [Route("api/GetYesBankRates")]
        public void GetYesBankRates()
        {
            try
            {
                WebClient wb = new WebClient();
                wb.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.33 Safari/537.36");
                wb.DownloadFile("https://www.yesbank.in/pdf/forexcardratesenglishpdf", HttpContext.Current.Server.MapPath("~/App_Data/" + "YesBank.pdf"));

                string parsedText = PDFParser.GetYesBankText(HttpContext.Current.Server.MapPath("~/App_Data/" + "YesBank.pdf"));
            }
            catch(Exception ex)
            {

            }
        }

        [HttpGet]
        [HttpOptions]
        [Route("api/GetSBIRates")]
        public void GetSBIRates()
        {
            try
            {
                WebClient wb = new WebClient();
                wb.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.33 Safari/537.36");
                wb.DownloadFile("https://sbi.co.in/webfiles/uploads/files/FOREX_CARD_RATES.pdf", HttpContext.Current.Server.MapPath("~/App_Data/" + "SBI.pdf"));

                string parsedText = PDFParser.GetSBIText(HttpContext.Current.Server.MapPath("~/App_Data/" + "SBI.pdf"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [HttpOptions]
        [Route("api/GetHDFCRates")]
        public void GetHDFCRates()
        {
            WebClient wb = new WebClient();
            wb.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2490.33 Safari/537.36");
            wb.DownloadFile("https://www.hdfcbank.com/assets/pdf/forex_rates/rates.pdf", HttpContext.Current.Server.MapPath("~/App_Data/" + "HDFC.pdf"));

            string parsedText = PDFParser.GetHDFCText(HttpContext.Current.Server.MapPath("~/App_Data/" + "HDFC.pdf"));
        }

        [HttpGet]
        [HttpOptions]
        [Route("api/GetKotakRates")]
        public void GetKotakRates()
        {
            using (var client = new WebClient())
            {
                string html = client.DownloadString("https://www.kotak.com/j1001drup/phpapps/content/siteadmin/get_forex_rates_data2.php");
                html = html.Trim().Replace("document.write(\"", "").Replace("\");", "");
                var doc = new HtmlDocument();
                doc.LoadHtml(html);

                // Buy Rates 
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("/table[1]");
                foreach (var node in nodes)
                {
                    HtmlNodeCollection rows = node.SelectNodes("tr");
                    int counter = 0;
                    foreach (HtmlNode row in rows)
                    {
                        HtmlNodeCollection cells = row.SelectNodes("td");
                        counter++;
                        if (cells == null || cells.Count < 5 || counter < 4)
                            continue;

                        string toCurrency = "INR";
                        int kotakPartnerId = 1162;
                        List<SP_FetchPartnerRates_Result1> partnerRateList = _partnerRepository.FetchPartnerRates(kotakPartnerId);

                        if (counter < 19)
                        {
                            string fromCurrency = cells[0].InnerText;

                            string moneyToINR = cells[1].InnerText;
                            string foreignToINR = cells[4].InnerText;
                            string travelCardToINR = cells[2].InnerText;
                            // insert into DB

                            PartnerRate pr1 = new PartnerRate();
                            PartnerRate pr2 = new PartnerRate();
                            PartnerRate pr3 = new PartnerRate();

                            if (partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "MoneyTransfer") != null)
                                pr1.Id = partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "MoneyTransfer").Id;

                            pr1.FromRate = fromCurrency;
                            pr1.ToRate = toCurrency;

                            if (moneyToINR == "-")
                                continue;

                            pr1.Guaranteed = double.Parse(moneyToINR);
                            pr1.Indicative = double.Parse(moneyToINR);
                            pr1.RateType = "MoneyTransfer";//"Forex"; // "TravelCard"
                            pr1.Status = "Active";
                            pr1.PartnerId = kotakPartnerId;

                            if (partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "Forex") != null)
                                pr2.Id = partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "Forex").Id;

                            pr2.FromRate = fromCurrency;
                            pr2.ToRate = toCurrency;
                            pr2.Guaranteed = double.Parse(foreignToINR);
                            pr2.Indicative = double.Parse(foreignToINR);
                            pr2.RateType = "Forex";//"Forex"; // "TravelCard"
                            pr2.Status = "Active";
                            pr2.PartnerId = kotakPartnerId;

                            if (partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "TravelCard") != null)
                                pr3.Id = partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "TravelCard").Id;

                            pr3.FromRate = fromCurrency;
                            pr3.ToRate = toCurrency;
                            pr3.Guaranteed = double.Parse(foreignToINR);
                            pr3.Indicative = double.Parse(foreignToINR);
                            pr3.RateType = "TravelCard";//"Forex"; // "TravelCard"
                            pr3.Status = "Active";
                            pr3.PartnerId = kotakPartnerId;

                            /*if (partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency) != null)
                                pr2.Id = partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency).Id;

                            pr2.FromRate = toCurrency;
                            pr2.ToRate = fromCurrency;
                            pr2.Guaranteed = double.Parse(moneySell);
                            pr2.Indicative = double.Parse(moneySell);
                            pr2.RateType = "MoneyTransfer";//"Forex"; // "TravelCard"
                            pr2.Status = "Active";
                            pr2.PartnerId = kotakPartnerId;*/

                            List<PartnerRate> rateList = new List<PartnerRate> { pr1, pr2, pr3 };
                            _partnerRepository.SaveUpdateRates(rateList);


                            /*if (partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "Forex") != null)
                            {
                                pr1.Id = partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "Forex").Id;
                                pr2.Id = pr1.Id;

                                pr1.RateType = "Forex";
                                pr1.Guaranteed = double.Parse(forexBuy);
                                pr1.Indicative = double.Parse(forexBuy);

                                pr2.RateType = "Forex";
                                pr2.Guaranteed = double.Parse(forexSell);
                                pr2.Indicative = double.Parse(forexSell);

                                rateList = new List<PartnerRate> { pr1, pr2 };
                                _partnerRepository.SaveUpdateRates(rateList);
                            }*/

                        }
                        else if (counter > 23 && counter < 39)
                        {
                            string fromCurrency = cells[0].InnerText;
                            string moneyFromINR = cells[1].InnerText;
                            string foreignFromINR = cells[4].InnerText;
                            string travelCardFromINR = cells[2].InnerText;
                            // insert into DB
                            PartnerRate pr1 = new PartnerRate();
                            PartnerRate pr2 = new PartnerRate();
                            PartnerRate pr3 = new PartnerRate();

                            if (partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency && p.RateType.Trim() == "MoneyTransfer") != null)
                                pr1.Id = partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "MoneyTransfer").Id;

                            pr1.FromRate = toCurrency;
                            pr1.ToRate = fromCurrency;
                            pr1.Guaranteed = double.Parse(moneyFromINR);
                            pr1.Indicative = double.Parse(moneyFromINR);
                            pr1.RateType = "MoneyTransfer";
                            pr1.Status = "Active";
                            pr1.PartnerId = kotakPartnerId;

                            if (partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency && p.RateType.Trim() == "Forex") != null)
                                pr2.Id = partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "Forex").Id;

                            pr2.FromRate = toCurrency;
                            pr2.ToRate = fromCurrency;
                            pr2.Guaranteed = double.Parse(foreignFromINR);
                            pr2.Indicative = double.Parse(foreignFromINR);
                            pr2.RateType = "Forex";
                            pr2.Status = "Active";
                            pr2.PartnerId = kotakPartnerId;

                            if (partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency && p.RateType.Trim() == "TravelCard") != null)
                                pr3.Id = partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "TravelCard").Id;

                            pr3.FromRate = toCurrency;
                            pr3.ToRate = fromCurrency;
                            pr3.Guaranteed = double.Parse(travelCardFromINR);
                            pr3.Indicative = double.Parse(travelCardFromINR);
                            pr3.RateType = "TravelCard";
                            pr3.Status = "Active";
                            pr3.PartnerId = kotakPartnerId;


                            List<PartnerRate> rateList = new List<PartnerRate> { pr1, pr2, pr3 };
                            _partnerRepository.SaveUpdateRates(rateList);
                        }

                    }
                }


            }

        }

        [HttpGet]
        [HttpOptions]
        [Route("api/GetBOBRates")]
        public void GetBOBRates()
        {
            using (var client = new WebClient())
            {
                /*client.Headers[HttpRequestHeader.ContentType] = "application/json";
                string html = client.UploadString("https://www.yesbank.in/LocateUsService/service/getBranch", "{\"location\":\"Mumbai\",\"Lat\":\"18\",\"Long\":\"72\", \"radius\": 100}");*/
                string html = client.DownloadString("https://www.bankofbaroda.com/forex-card-rates.htm");
                var doc = new HtmlDocument();
                doc.LoadHtml(html);
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("/html[1]/body[1]/form[1]/div[3]/div[1]/div[2]/div[1]/div[1]/div[2]/table[1]/tbody[1]");
                foreach (var node in nodes)
                {
                    HtmlNodeCollection rows = node.SelectNodes("tr");
                    foreach (HtmlNode row in rows)
                    {
                        HtmlNodeCollection cells = row.SelectNodes("td");

                        if (cells == null)
                            continue;

                        string fromCurrency = cells[0].InnerText;
                        string toCurrency = "INR";

                        string moneyBuy = cells[3].InnerText;
                        string forexBuy = cells[4].InnerText;
                        //string travelCardToINR = cells[4].InnerText;

                        string moneySell = cells[1].InnerText;
                        string forexSell = cells[2].InnerText;
                        //string travelCardFromINR = cells[9].InnerText;

                        int bobPartnerId = 1158;

                        List<SP_FetchPartnerRates_Result1> partnerRateList = _partnerRepository.FetchPartnerRates(bobPartnerId);
                        PartnerRate pr1 = new PartnerRate();
                        PartnerRate pr2 = new PartnerRate();
                        if (partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "MoneyTransfer") != null)
                            pr1.Id = partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "MoneyTransfer").Id;

                        pr1.FromRate = fromCurrency;
                        pr1.ToRate = toCurrency;

                        if (moneyBuy == "-")
                            continue;

                        pr1.Guaranteed = double.Parse(moneyBuy);
                        pr1.Indicative = double.Parse(moneyBuy);
                        pr1.RateType = "MoneyTransfer";//"Forex"; // "TravelCard"
                        pr1.Status = "Active";
                        pr1.PartnerId = bobPartnerId;

                        if (partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency) != null)
                            pr2.Id = partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency).Id;

                        pr2.FromRate = toCurrency;
                        pr2.ToRate = fromCurrency;
                        pr2.Guaranteed = double.Parse(moneySell);
                        pr2.Indicative = double.Parse(moneySell);
                        pr2.RateType = "MoneyTransfer";//"Forex"; // "TravelCard"
                        pr2.Status = "Active";
                        pr2.PartnerId = bobPartnerId;

                        List<PartnerRate> rateList = new List<PartnerRate> { pr1, pr2 };
                        _partnerRepository.SaveUpdateRates(rateList);


                        if (partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "Forex") != null)
                        {
                            pr1.Id = partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "Forex").Id;
                            pr2.Id = pr1.Id;

                            pr1.RateType = "Forex";
                            pr1.Guaranteed = double.Parse(forexBuy);
                            pr1.Indicative = double.Parse(forexBuy);

                            pr2.RateType = "Forex";
                            pr2.Guaranteed = double.Parse(forexSell);
                            pr2.Indicative = double.Parse(forexSell);

                            rateList = new List<PartnerRate> { pr1, pr2 };
                            _partnerRepository.SaveUpdateRates(rateList);
                        }
                    }
                }

            }
        }


    }

    public static class PDFParser
    {
        private static readonly IPartnerRepository _partnerRepository = new PartnerRepository();
        /// <summary>
        /// Extracts a text from a PDF file.
        /// </summary>
        /// <param name="filePath">the full path to the pdf file.</param>
        /// <returns>the extracted text</returns>
        public static string GetYesBankText(string filePath)
        {
            var sb = new StringBuilder();
            try
            {
                using (PdfReader reader = new PdfReader(filePath))
                {
                    string prevPage = "";
                    for (int page = 1; page <= reader.NumberOfPages; page++)
                    {
                        ITextExtractionStrategy its = new SimpleTextExtractionStrategy();
                        var s = PdfTextExtractor.GetTextFromPage(reader, page, its);
                        if (prevPage != s) sb.Append(s);
                        prevPage = s;
                    }
                    reader.Close();

                    string[] rates = Regex.Split(prevPage, "\n");
                    int yesBankPartnerId = 1174;
                    List<SP_FetchPartnerRates_Result1> partnerRateList = _partnerRepository.FetchPartnerRates(yesBankPartnerId);
                    foreach (string line in rates)
                    {
                        if (line.Contains("/") && line.Length > 50)
                        {
                            string fromCurrency = line.Substring(line.IndexOf("/") - 3, 3);
                            string toCurrency = "INR";
                            string[] singleRate = Regex.Split(line.Substring(line.IndexOf("/")), " ");
                            string moneyBuy = singleRate[1];
                            string moneySell = singleRate[2];
                            string travelCardBuy = singleRate[7];
                            string travelCardSell = singleRate[8];
                            string forexBuy = singleRate[5];
                            string forexSell = singleRate[6];

                            PartnerRate pr1 = new PartnerRate();
                            PartnerRate pr2 = new PartnerRate();

                            if (partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency) != null)
                                pr1.Id = partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency).Id;

                            pr1.FromRate = fromCurrency;
                            pr1.ToRate = toCurrency;
                            pr1.Guaranteed = double.Parse(moneyBuy);
                            pr1.Indicative = double.Parse(moneyBuy);
                            pr1.RateType = "MoneyTransfer";//"Forex"; // "TravelCard"
                            pr1.Status = "Active";
                            pr1.PartnerId = yesBankPartnerId;

                            if (partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency) != null)
                                pr2.Id = partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency).Id;

                            pr2.FromRate = toCurrency;
                            pr2.ToRate = fromCurrency;
                            pr2.Guaranteed = double.Parse(moneySell);
                            pr2.Indicative = double.Parse(moneySell);
                            pr2.RateType = "MoneyTransfer";//"Forex"; // "TravelCard"
                            pr2.Status = "Active";
                            pr2.PartnerId = yesBankPartnerId;

                            List<PartnerRate> rateList = new List<PartnerRate> { pr1, pr2 };
                            _partnerRepository.SaveUpdateRates(rateList);
                        }

                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return sb.ToString();
        }

        public static string GetHDFCText(string filePath)
        {
            var sb = new StringBuilder();
            try
            {
                using (PdfReader reader = new PdfReader(filePath))
                {
                    string prevPage = "";
                    for (int page = 1; page <= reader.NumberOfPages; page++)
                    {
                        ITextExtractionStrategy its = new SimpleTextExtractionStrategy();
                        var s = PdfTextExtractor.GetTextFromPage(reader, page, its);
                        if (prevPage != s) sb.Append(s);
                        prevPage = s;
                    }
                    reader.Close();

                    string[] rates = Regex.Split(prevPage, "\n");
                    int counter = 0;

                    foreach (string line in rates)
                    {
                        counter++;
                        string newLine = line;
                        if (newLine.Length > 50 && counter < 49)
                        {
                            if (newLine.Contains("Great Britain Pound"))
                                newLine = line.Replace("Great Britain Pound", "");
                            else if (newLine.Contains("Dirham"))
                                newLine = line.Replace("U.A.E. Dirham ", "");
                            else if (newLine.Contains("United States Dollar"))
                                newLine = line.Replace("United States Dollar", "");
                            else if (newLine.Contains("South African Rand"))
                                newLine = line.Replace("South African Rand ", "");

                            var upper = newLine.Split(' ').Where(s => String.Equals(s, s.ToUpper(), StringComparison.Ordinal));

                            string fromCurrency = upper.ElementAt(0);
                            string toCurrency = "INR";
                            //string[] singleRate = Regex.Split(line.Substring(line.IndexOf("/")), " ");
                            string moneyBuy = upper.ElementAt(2);
                            string moneySell = upper.ElementAt(3);
                            string travelCardBuy = upper.ElementAt(8);
                            string travelCardSell = upper.ElementAt(9);
                            string forexBuy = upper.ElementAt(6);
                            string forexSell = upper.ElementAt(7);


                            int hdfcPartnerId = 1159;
                            List<SP_FetchPartnerRates_Result1> partnerRateList = _partnerRepository.FetchPartnerRates(hdfcPartnerId);
                            PartnerRate pr1 = new PartnerRate();
                            PartnerRate pr2 = new PartnerRate();
                            if (partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "MoneyTransfer") != null)
                                pr1.Id = partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "MoneyTransfer").Id;

                            pr1.FromRate = fromCurrency;
                            pr1.ToRate = toCurrency;

                            if (moneyBuy == "-")
                                continue;

                            pr1.Guaranteed = double.Parse(forexSell);
                            pr1.Indicative = double.Parse(forexSell);
                            pr1.RateType = "MoneyTransfer";//"Forex"; // "TravelCard"
                            pr1.Status = "Active";
                            pr1.PartnerId = hdfcPartnerId;

                            if (partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency) != null)
                                pr2.Id = partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency).Id;

                            pr2.FromRate = toCurrency;
                            pr2.ToRate = fromCurrency;
                            pr2.Guaranteed = double.Parse(forexBuy);
                            pr2.Indicative = double.Parse(forexBuy);
                            pr2.RateType = "MoneyTransfer";//"Forex"; // "TravelCard"
                            pr2.Status = "Active";
                            pr2.PartnerId = hdfcPartnerId;

                            List<PartnerRate> rateList = new List<PartnerRate> { pr1, pr2 };
                            _partnerRepository.SaveUpdateRates(rateList);


                            if (partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "TravelCard") != null)
                            {
                                pr1.Id = partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "TravelCard").Id;
                                pr2.Id = pr1.Id;

                                pr1.RateType = "TravelCard";
                                pr1.Guaranteed = double.Parse(travelCardBuy);
                                pr1.Indicative = double.Parse(travelCardBuy);

                                pr2.RateType = "TravelCard";
                                pr2.Guaranteed = double.Parse(travelCardSell);
                                pr2.Indicative = double.Parse(travelCardSell);

                                rateList = new List<PartnerRate> { pr1, pr2 };
                                _partnerRepository.SaveUpdateRates(rateList);
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return sb.ToString();
        }

        public static string GetSBIText(string filePath)
        {
            var sb = new StringBuilder();
            try
            {
                using (PdfReader reader = new PdfReader(filePath))
                {
                    string prevPage = "";
                    for (int page = 1; page <= reader.NumberOfPages; page++)
                    {
                        ITextExtractionStrategy its = new SimpleTextExtractionStrategy();
                        var s = PdfTextExtractor.GetTextFromPage(reader, page, its);
                        if (prevPage != s) sb.Append(s);
                        prevPage = s;
                    }
                    reader.Close();

                    string[] rates = Regex.Split(prevPage, "\n");

                    foreach (string line in rates)
                    {
                        if (line.Contains("/") && line.Length > 50)
                        {
                            try
                            {
                                string fromCurrency = line.Substring(line.IndexOf("/") - 3, 3);
                                string toCurrency = "INR";
                                string[] singleRate = Regex.Split(line.Substring(line.IndexOf("/")), " ");
                                string moneyBuy = singleRate[1];
                                string moneySell = singleRate[2];
                                string travelCardBuy = singleRate[5];
                                string travelCardSell = singleRate[6];
                                string forexBuy = singleRate[3];
                                string forexSell = singleRate[4];

                                int hdfcPartnerId = 1163;
                                List<SP_FetchPartnerRates_Result1> partnerRateList = _partnerRepository.FetchPartnerRates(hdfcPartnerId);
                                PartnerRate pr1 = new PartnerRate();
                                PartnerRate pr2 = new PartnerRate();
                                if (partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "MoneyTransfer") != null)
                                    pr1.Id = partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "MoneyTransfer").Id;

                                pr1.FromRate = fromCurrency;
                                pr1.ToRate = toCurrency;

                                if (moneyBuy == "-")
                                    continue;

                                pr1.Guaranteed = double.Parse(moneyBuy);
                                pr1.Indicative = double.Parse(moneyBuy);
                                pr1.RateType = "MoneyTransfer";//"Forex"; // "TravelCard"
                                pr1.Status = "Active";
                                pr1.PartnerId = hdfcPartnerId;

                                if (partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency) != null)
                                    pr2.Id = partnerRateList.Find(p => p.from == toCurrency && p.to == fromCurrency).Id;

                                pr2.FromRate = toCurrency;
                                pr2.ToRate = fromCurrency;
                                pr2.Guaranteed = double.Parse(moneySell);
                                pr2.Indicative = double.Parse(moneySell);
                                pr2.RateType = "MoneyTransfer";//"Forex"; // "TravelCard"
                                pr2.Status = "Active";
                                pr2.PartnerId = hdfcPartnerId;

                                List<PartnerRate> rateList = new List<PartnerRate> { pr1, pr2 };
                                _partnerRepository.SaveUpdateRates(rateList);


                                if (partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "TravelCard") != null)
                                {
                                    pr1.Id = partnerRateList.Find(p => p.from == fromCurrency && p.to == toCurrency && p.RateType.Trim() == "TravelCard").Id;
                                    pr2.Id = pr1.Id;

                                    pr1.RateType = "TravelCard";
                                    pr1.Guaranteed = double.Parse(travelCardBuy);
                                    pr1.Indicative = double.Parse(travelCardBuy);

                                    pr2.RateType = "TravelCard";
                                    pr2.Guaranteed = double.Parse(travelCardSell);
                                    pr2.Indicative = double.Parse(travelCardSell);

                                    rateList = new List<PartnerRate> { pr1, pr2 };
                                    _partnerRepository.SaveUpdateRates(rateList);
                                }
                            }
                            catch (Exception ex)
                            {
                                // ignore the error 
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return sb.ToString();
        }

        /*public static string GetHTMLText(string sourceFilePath)
        {
            var txt = PDFParser.GetText(sourceFilePath);
            var sb = new StringBuilder();
            foreach (string s in txt.Split('\n'))
            {
                sb.AppendFormat("<p>{0}</p>", s);
            }
            return sb.ToString();
        }*/
    }
}

