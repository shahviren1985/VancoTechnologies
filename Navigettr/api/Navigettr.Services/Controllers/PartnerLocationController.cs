using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Navigettr.Core;
using Navigettr.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Xml.Linq;

namespace Navigettr.Services.Controllers
{
    public class PartnerLocationController : ApiController
    {
        private static readonly IPartnerLocationRepository _repository = new PartnerLocationRepository();
        PartnerLocationData objPartner = new PartnerLocationData();

        public class PartnerLocation
        {
            public int partnerLocationId { get; set; }
            public int partnerId { get; set; }
            public string addressLine1 { get; set; }
            public string addressLine2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string zipCode { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }
            public string mobileNumber { get; set; }
            public DateTime dateActivated { get; set; }
            public DateTime dateExpiry { get; set; }
            public int page { get; set; }
            public int pageData { get; set; }
            public string status { get; set; }
            public List<WorkList> partnerLocationWorkList { get; set; }

        }

        public class WorkList
        {
            public int id { get; set; }
            public string day { get; set; }
            public string startTime { get; set; }
            public string closeTime { get; set; }
            public string status { get; set; }

        }
        [HttpPost]
        [HttpOptions]
        [Route("api/addPartnerLocation")]
        public HttpResponseMessage SaveUpdatePartnerLocation([FromBody]PartnerLocation jsondata)
        {
            try
            {
                string yourJson = "";
                int data = _repository.SaveUpdatePartnerLocation(jsondata.partnerLocationId, jsondata.partnerId, jsondata.addressLine1, jsondata.addressLine2,
                                 jsondata.city, jsondata.state, jsondata.country, jsondata.zipCode, jsondata.latitude, jsondata.longitude, jsondata.mobileNumber,
                                 jsondata.dateActivated, jsondata.dateExpiry, jsondata.status);

                if (data > 0)
                {

                    jsondata.partnerLocationId = data;

                    if (jsondata.partnerLocationWorkList != null && jsondata.partnerLocationWorkList.Count > 0)
                    {
                        for (int i = 0; i < jsondata.partnerLocationWorkList.Count; i++)
                        {
                            int Workdata = _repository.SaveUpdatePartnerLocationWorkTime(jsondata.partnerLocationWorkList[i].id, jsondata.partnerId, jsondata.partnerLocationId, jsondata.partnerLocationWorkList[i].day, jsondata.partnerLocationWorkList[i].startTime, jsondata.partnerLocationWorkList[i].closeTime, jsondata.partnerLocationWorkList[i].status);
                            jsondata.partnerLocationWorkList[i].id = Workdata;
                        }
                        return Request.CreateResponse(HttpStatusCode.OK, jsondata);
                    }
                    else
                    {
                        yourJson = "";
                        HttpError myCustomError = new HttpError(yourJson);
                        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, myCustomError);
                    }
                }
                else
                {
                    yourJson = "Unable to add location";
                    HttpError myCustomError = new HttpError(yourJson);
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, myCustomError);

                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [HttpOptions]
        [Route("api/importPartnerLocation")]
        public HttpResponseMessage SaveUpdatePartnerLocation(int partnerId)
        {
            string filePath = SaveFile();
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(filePath, false))
            {
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();

                var sheets = workbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();
                var relationshipId = sheets.First().Id.Value;
                worksheetPart = (WorksheetPart)workbookPart.GetPartById(relationshipId);
                var workSheet = worksheetPart.Worksheet;
                var sheetData = workSheet.GetFirstChild<SheetData>();
                var rows = sheetData.Descendants<Row>().ToList();

                for (int i = 2; i < rows.Count; i++)
                {
                    PartnerLocationDetail location = new PartnerLocationDetail();
                    location.PartnerId = partnerId;
                    if (location.PartnerId < 1)
                    {
                        location.PartnerId = int.Parse(GetCellValue(spreadsheetDocument, GetCell(workSheet, "A", i)));
                    }

                    location.AddressLine1 = GetCellValue(spreadsheetDocument, GetCell(workSheet, "C", i));
                    location.AddressLine2 = string.Empty;
                    location.City = GetCellValue(spreadsheetDocument, GetCell(workSheet, "D", i));
                    location.State = GetCellValue(spreadsheetDocument, GetCell(workSheet, "F", i));
                    location.Country = GetCellValue(spreadsheetDocument, GetCell(workSheet, "G", i));
                    location.MobileNumber = GetCellValue(spreadsheetDocument, GetCell(workSheet, "H", i));
                    location.DateCreated = DateTime.FromOADate(double.Parse((GetCellValue(spreadsheetDocument, GetCell(workSheet, "I", i)))));
                    location.DateActivated = DateTime.FromOADate(double.Parse(GetCellValue(spreadsheetDocument, GetCell(workSheet, "J", i))));
                    location.DateExpiry = DateTime.FromOADate(double.Parse(GetCellValue(spreadsheetDocument, GetCell(workSheet, "K", i))));
                    location.Status = GetCellValue(spreadsheetDocument, GetCell(workSheet, "L", i));

                    // Need to add logic for IFSC code. update the address if IFSC code exists
                    location.ID = _repository.SaveUpdatePartnerLocation(0, location.PartnerId, location.AddressLine1, location.AddressLine2,
                                 location.City, location.State, location.Country, location.ZipCode, 0, 0, location.MobileNumber,
                                 location.DateActivated, location.DateExpiry, location.Status);

                    // Insert entry into PartnerLocationWorkTime
                    PartnerLocationWorkTime workTime = new PartnerLocationWorkTime();
                    workTime.LocationId = location.ID;
                    workTime.Status = "Active";
                    workTime.WorkDay = "Monday";
                    workTime.WorkStartTime = DateTime.FromOADate(double.Parse(GetCellValue(spreadsheetDocument, GetCell(workSheet, "M", i)))).TimeOfDay.ToString(@"hh\:mm\:ss");
                    workTime.WorkEndTime = DateTime.FromOADate(double.Parse(GetCellValue(spreadsheetDocument, GetCell(workSheet, "N", i)))).TimeOfDay.ToString(@"hh\:mm\:ss");
                    workTime.PartnerId = location.PartnerId;

                    _repository.SaveUpdatePartnerLocationWorkTime(0, workTime.PartnerId.GetValueOrDefault(), workTime.LocationId.GetValueOrDefault(), workTime.WorkDay, workTime.WorkStartTime, workTime.WorkEndTime, workTime.Status);

                    workTime = new PartnerLocationWorkTime();
                    workTime.LocationId = location.ID;
                    workTime.Status = "Active";
                    workTime.WorkDay = "Tuesday";
                    workTime.WorkStartTime = DateTime.FromOADate(double.Parse(GetCellValue(spreadsheetDocument, GetCell(workSheet, "O", i)))).TimeOfDay.ToString(@"hh\:mm\:ss");
                    workTime.WorkEndTime = DateTime.FromOADate(double.Parse(GetCellValue(spreadsheetDocument, GetCell(workSheet, "P", i)))).TimeOfDay.ToString(@"hh\:mm\:ss");
                    workTime.PartnerId = location.PartnerId;

                    _repository.SaveUpdatePartnerLocationWorkTime(0, workTime.PartnerId.GetValueOrDefault(), workTime.LocationId.GetValueOrDefault(), workTime.WorkDay, workTime.WorkStartTime, workTime.WorkEndTime, workTime.Status);

                    workTime = new PartnerLocationWorkTime();
                    workTime.LocationId = location.ID;
                    workTime.Status = "Active";
                    workTime.WorkDay = "Wednesday";
                    workTime.WorkStartTime = DateTime.FromOADate(double.Parse(GetCellValue(spreadsheetDocument, GetCell(workSheet, "Q", i)))).TimeOfDay.ToString(@"hh\:mm\:ss");
                    workTime.WorkEndTime = DateTime.FromOADate(double.Parse(GetCellValue(spreadsheetDocument, GetCell(workSheet, "R", i)))).TimeOfDay.ToString(@"hh\:mm\:ss");
                    workTime.PartnerId = location.PartnerId;

                    _repository.SaveUpdatePartnerLocationWorkTime(0, workTime.PartnerId.GetValueOrDefault(), workTime.LocationId.GetValueOrDefault(), workTime.WorkDay, workTime.WorkStartTime, workTime.WorkEndTime, workTime.Status);

                    workTime = new PartnerLocationWorkTime();
                    workTime.LocationId = location.ID;
                    workTime.Status = "Active";
                    workTime.WorkDay = "Thursday";
                    workTime.WorkStartTime = DateTime.FromOADate(double.Parse(GetCellValue(spreadsheetDocument, GetCell(workSheet, "S", i)))).TimeOfDay.ToString(@"hh\:mm\:ss");
                    workTime.WorkEndTime = DateTime.FromOADate(double.Parse(GetCellValue(spreadsheetDocument, GetCell(workSheet, "T", i)))).TimeOfDay.ToString(@"hh\:mm\:ss");
                    workTime.PartnerId = location.PartnerId;

                    _repository.SaveUpdatePartnerLocationWorkTime(0, workTime.PartnerId.GetValueOrDefault(), workTime.LocationId.GetValueOrDefault(), workTime.WorkDay, workTime.WorkStartTime, workTime.WorkEndTime, workTime.Status);

                    workTime = new PartnerLocationWorkTime();
                    workTime.LocationId = location.ID;
                    workTime.Status = "Active";
                    workTime.WorkDay = "Friday";
                    workTime.WorkStartTime = DateTime.FromOADate(double.Parse(GetCellValue(spreadsheetDocument, GetCell(workSheet, "U", i)))).TimeOfDay.ToString(@"hh\:mm\:ss");
                    workTime.WorkEndTime = DateTime.FromOADate(double.Parse(GetCellValue(spreadsheetDocument, GetCell(workSheet, "V", i)))).TimeOfDay.ToString(@"hh\:mm\:ss");
                    workTime.PartnerId = location.PartnerId;

                    _repository.SaveUpdatePartnerLocationWorkTime(0, workTime.PartnerId.GetValueOrDefault(), workTime.LocationId.GetValueOrDefault(), workTime.WorkDay, workTime.WorkStartTime, workTime.WorkEndTime, workTime.Status);

                    workTime = new PartnerLocationWorkTime();
                    workTime.LocationId = location.ID;
                    workTime.Status = "Active";
                    workTime.WorkDay = "Saturday";
                    workTime.WorkStartTime = DateTime.FromOADate(double.Parse(GetCellValue(spreadsheetDocument, GetCell(workSheet, "W", i)))).TimeOfDay.ToString(@"hh\:mm\:ss");
                    workTime.WorkEndTime = DateTime.FromOADate(double.Parse(GetCellValue(spreadsheetDocument, GetCell(workSheet, "X", i)))).TimeOfDay.ToString(@"hh\:mm\:ss");
                    workTime.PartnerId = location.PartnerId;

                    _repository.SaveUpdatePartnerLocationWorkTime(0, workTime.PartnerId.GetValueOrDefault(), workTime.LocationId.GetValueOrDefault(), workTime.WorkDay, workTime.WorkStartTime, workTime.WorkEndTime, workTime.Status);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Imported partner locations successfully");
        }

        [HttpPost]
        [HttpOptions]
        [Route("api/getPartnerLocations")]
        public HttpResponseMessage populatePartnersLocation(string PartnerId, [FromBody]PartnerLocation jsondata)
        {
            try
            {
                ObjectParameter totalCount = new ObjectParameter("totalCount", 0);
                var data = _repository.FetchPartnerLocation(Convert.ToInt32(PartnerId), jsondata.status, jsondata.zipCode, jsondata.city, jsondata.page, jsondata.pageData, totalCount);

                if (data.Count > 0 && data != null)
                {
                    var locations = _repository.GetPartnerLocationWorkTimes(Convert.ToInt32(PartnerId));

                    foreach (SP_FetchPartnerLocationDetails_Result d in data)
                    {
                        var partnerLoc = d as SP_FetchPartnerLocationDetails_Result;
                        partnerLoc.PartnerLocationWorkTimes = locations.FindAll(l => l.LocationId == d.id);
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, data);
                }
                else
                {
                    string yourJson = "No locations found for given search criteria";
                    HttpError myCustomError = new HttpError(yourJson);
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, myCustomError);
                }


            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.InnerException.Message);

            }

        }

        private static Row GetRow(Worksheet worksheet, int rowIndex)
        {
            Row row = worksheet.GetFirstChild<SheetData>().
            Elements<Row>().FirstOrDefault(r => r.RowIndex == rowIndex);
            if (row == null)
            {
                throw new ArgumentException(String.Format("No row with index {0} found in spreadsheet", rowIndex));
            }
            return row;
        }

        [HttpPost]
        [HttpOptions]
        [Route("api/ProcessAddress")]
        public HttpResponseMessage ProcessAddresses()
        {
            List<SP_FetchUnProcessedLocations_Result> locations = _repository.FetchUnProcessedLocation();

            foreach (SP_FetchUnProcessedLocations_Result location in locations)
            {
                string address = location.AddressLine1 + ", " + location.AddressLine2 + ", " + location.City + ", " + location.State + ", " + location.Country;
                // key =AIzaSyDOgpcjQu4kRkC4ZqE0YAXIvgDEi2P2HFE
                string requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?key=AIzaSyC7Bgi2lHuE5WaMAsVeTZznuLC0ocAAC04&address={0}&sensor=false", Uri.EscapeDataString(address));

                WebRequest request = WebRequest.Create(requestUri);
                WebResponse response = request.GetResponse();
                XDocument xdoc = XDocument.Load(response.GetResponseStream());

                XElement result = xdoc.Element("GeocodeResponse").Element("result");

                if (result == null)
                    continue;
                try
                {
                    List<XElement> addressComponent = result.Elements("address_component").ToList();

                    foreach (XElement com in addressComponent)
                    {
                        if (com.Element("type") != null && com.Element("type").Value == "postal_code")
                        {
                            location.ZipCode = com.Element("long_name").Value;
                            break;
                        }
                    }

                    XElement locationElement = result.Element("geometry").Element("location");
                    XElement lat = locationElement.Element("lat");
                    XElement lng = locationElement.Element("lng");

                    location.Latitude = double.Parse(lat.Value);
                    location.Longitude = double.Parse(lng.Value);
                    //location.ZipCode = postalCode.Value;

                    _repository.SaveUpdatePartnerLocation(location.Id, location.PartnerId, location.AddressLine1, location.AddressLine2, location.City, location.State, location.Country, location.ZipCode, float.Parse(location.Latitude.ToString()), float.Parse(location.Longitude.ToString()), location.MobileNumber, DateTime.Parse(location.ActivatedDate), DateTime.Parse(location.ExpiryDate), location.Status);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, "Update Geo Codes for all the locations");
        }

        [HttpPost]
        [HttpOptions]
        [Route("api/ProcessAddressOpenSource")]
        public async void ProcessOpenSourceAPIAddresses()
        {
            List<SP_FetchUnProcessedLocations_Result> locations = _repository.FetchUnProcessedLocation();
            HttpClient client = new HttpClient();
            foreach (SP_FetchUnProcessedLocations_Result location in locations)
            {
                string address = location.AddressLine1 + ", " + location.AddressLine2 + ", " + location.City + ", " + location.State + ", " + location.Country;
                //string requestUri = string.Format("https://www.mapdevelopers.com/data.php?operation=geocode", Uri.EscapeDataString(address));

                var values = new Dictionary<string, string>{
                                                                { "address", address }
                                                            };

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync("https://www.mapdevelopers.com/data.php?operation=geocode", content);

                var responseString = await response.Content.ReadAsStringAsync();
                if (responseString.Length < 30)
                    continue;

                AddressInfo loc = JsonConvert.DeserializeObject<AddressInfo>(responseString);

                location.Latitude = loc.data.lat;
                location.Longitude = loc.data.lng;

                _repository.SaveUpdatePartnerLocation(location.Id, location.PartnerId, location.AddressLine1, location.AddressLine2, location.City, location.State, location.Country, location.ZipCode, float.Parse(location.Latitude.ToString()), float.Parse(location.Longitude.ToString()), location.MobileNumber, DateTime.Parse(location.ActivatedDate), DateTime.Parse(location.ExpiryDate), location.Status);
                //location.Latitude = 
                //WebRequest request = WebRequest.Create(requestUri);
                //WebResponse response = request.GetResponse();
                //XDocument xdoc = XDocument.Load(response.GetResponseStream());
            }
        }

        private static Cell GetCell(Worksheet worksheet, string columnName, int rowIndex)
        {
            Row row = GetRow(worksheet, rowIndex);

            if (row == null) return null;

            var FirstRow = row.Elements<Cell>().Where(c => string.Compare
            (c.CellReference.Value, columnName +
            rowIndex, true) == 0).FirstOrDefault();

            if (FirstRow == null) return null;

            return FirstRow;
        }

        private string GetCellValue(SpreadsheetDocument document, Cell cell)
        {
            SharedStringTablePart stringTablePart = document.WorkbookPart.SharedStringTablePart;
            string finalValue = string.Empty;
            if (cell == null || cell.CellValue == null)
            {
                return "";
            }
            string value = cell.CellValue.InnerXml;
            if (cell.DataType != null && cell.DataType == CellValues.SharedString)
            {
                finalValue = stringTablePart.SharedStringTable.ChildElements[Int32.Parse(value)].InnerText;
            }
            else
            {
                finalValue = value;
            }

            return finalValue.Trim();

        }

        private string SaveFile()
        {
            var time = DateTime.Parse(DateTime.Now.ToString());

            var clientZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(time, clientZone).ToString("MM/dd/yyyy HH:mm:ss");
            string targetFolder = HttpContext.Current.Server.MapPath("~/uploads");
            string targetPath = string.Empty;

            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                foreach (string file in HttpContext.Current.Request.Files)
                {
                    var postedFile = HttpContext.Current.Request.Files[file];

                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(postedFile.FileName).Replace(" ", "").Replace("(", "").Replace(")", "");
                        string extension = Path.GetExtension(postedFile.FileName);

                        targetPath = Path.Combine(targetFolder, fileName + "_" + utcTime.ToString().Replace(":", "").Replace("/", "").Replace(" ", "-") + extension);
                        postedFile.SaveAs(targetPath);
                    }
                }
            }

            return targetPath;
        }
    }

    public class AddressInfo
    {
        public string response { get; set; }
        public data data { get; set; }
    }

    public class data
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }
}
