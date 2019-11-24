using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Diagnostics;
using SVT.Business.Services;
using SVT.Business.Model;
using SVT.Infrastructure;
using System.Text;
using System.Text.RegularExpressions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.parser;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using SVT.API.Classes;
using System.Data.SqlClient;
using System.Data;
using System.Net.Http.Headers;
using System.Reflection;
using System.Globalization;

namespace SVL.API.Controllers
{
    public class FormController : ApiController
    {
        [HttpPost]
        public async Task<HttpResponseMessage> PostFormData()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return Request.CreateResponse(HttpStatusCode.OK, "plesae enter the data in form.");
            }
            var message = string.Empty;
            StudentDetail studentDetail = new StudentDetail();
            if (!GetFormExpireDate())
            {
                string root = HttpContext.Current.Server.MapPath("~/App_Data");
                var provider = new MultipartFormDataStreamProvider(root);

                try
                {
                    await Request.Content.ReadAsMultipartAsync(provider);

                    #region  ID

                    if (!string.IsNullOrEmpty(provider.FormData.GetValues("Id")[0].String()))
                    {
                        studentDetail.Id = provider.FormData.GetValues("Id")[0].ToInteger();
                    }
                    else
                    {
                        studentDetail.DateRegistered = DateTime.Now;
                    }

                    #endregion

                    #region Get Form Data for All the Controls

                    #region Validate MKCLFormNumber and also Assign Value if proper
                    string MKCLFormNumbervalue = provider.FormData["MKCLFormNumber"];
                    if (MKCLFormNumbervalue == null)
                    {
                        studentDetail.MKCLFormNumber = "0";
                    }
                    else
                    {

                        if (string.IsNullOrEmpty(provider.FormData.GetValues("MKCLFormNumber")[0].String()))
                        {
                            message = "Please enter MKCL Form Number.";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                        else
                        {
                            studentDetail.MKCLFormNumber = provider.FormData.GetValues("MKCLFormNumber")[0].String();
                        }
                    }
                    #endregion

                    #region  Validate LastName and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("LastName")[0].String()))
                    {
                        message = "Please enter LastName.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.LastName = provider.FormData.GetValues("LastName")[0].String();
                    }

                    #endregion

                    #region  Validate FirstName and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("FirstName")[0].String()))
                    {
                        message = "Please enter FirstName.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.FirstName = provider.FormData.GetValues("FirstName")[0].String();
                    }

                    #endregion

                    #region  Validate FatherName and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("FatherName")[0].String()))
                    {
                        message = "Please enter FatherName.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.FatherName = provider.FormData.GetValues("FatherName")[0].String();
                    }

                    #endregion

                    #region  Validate MotherName and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("MotherName")[0].String()))
                    {
                        message = "Please enter MotherName.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.MotherName = provider.FormData.GetValues("MotherName")[0].String();
                    }

                    #endregion

                    #region  Validate FatherLastName and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("FatherLastName")[0].String()))
                    {
                        message = "Please enter Father LastName.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.FatherLastName = provider.FormData.GetValues("FatherLastName")[0].String();
                    }

                    #endregion

                    #region  Validate FatherFirstName and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("FatherFirstName")[0].String()))
                    {
                        message = "Please enter Father FirstName.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.FatherFirstName = provider.FormData.GetValues("FatherFirstName")[0].String();
                    }

                    #endregion

                    #region  Validate FatherMiddleName and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("FatherMiddleName")[0].String()))
                    {
                        message = "Please enter Father MiddleName.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.FatherMiddleName = provider.FormData.GetValues("FatherMiddleName")[0].String();
                    }

                    #endregion

                    #region  Validate MotherLastName and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("MotherLastName")[0].String()))
                    {
                        message = "Please enter Mother LastName.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.MotherLastName = provider.FormData.GetValues("MotherLastName")[0].String();
                    }

                    #endregion

                    #region  Validate MotherFirstName and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("MotherFirstName")[0].String()))
                    {
                        message = "Please enter Mother FirstName.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.MotherFirstName = provider.FormData.GetValues("MotherFirstName")[0].String();
                    }

                    #endregion

                    #region  Validate MotherMiddleName and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("MotherMiddleName")[0].String()))
                    {
                        message = "Please enter Mother MiddleName.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.MotherMiddleName = provider.FormData.GetValues("MotherMiddleName")[0].String();
                    }

                    #endregion

                    #region  Validate AadharNumber and also Assign Value if proper
                    string aNumber = provider.FormData.GetValues("AadharNumber")[0].String();
                    if (string.IsNullOrEmpty(aNumber))
                    {
                        message = "Please enter AadharNumber.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        if (aNumber.Trim().Length == 12)
                        {

                            if (!IsAadharSubmitted(aNumber))
                            {
                                studentDetail.AadharNumber = provider.FormData.GetValues("AadharNumber")[0].String();
                            }
                            else
                            {
                                message = "You have already submitted one form with Aadhar # " + aNumber + ". You can't submit more than one form with same aadhar #. Please contact College office administration team for assistance.";
                                return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                            }
                        }
                        else
                        {
                            message = "Please enter 12 digit AadharNumber.";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                    }

                    if (provider.FormData.GetValues("VoterId") == null || provider.FormData.GetValues("VoterId").Length == 0 || string.IsNullOrEmpty(provider.FormData.GetValues("VoterId")[0].ToString()))
                    {
                        // Do nothing
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(provider.FormData.GetValues("VoterNumber")[0].ToString()))
                        {
                            message = "Please enter your voter number";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                    }

                    if (provider.FormData.GetValues("IsHostelRequired") == null || provider.FormData.GetValues("IsHostelRequired").Length == 0 || string.IsNullOrEmpty(provider.FormData.GetValues("IsHostelRequired")[0].ToString()))
                    {
                        // Do nothing
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(provider.FormData.GetValues("HostelReason")[0].ToString()))
                        {
                            message = "Please enter reason for availaing hostel facility.";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                    }

                    if (!string.IsNullOrEmpty(provider.FormData.GetValues("PanNumber")[0].String()))
                    {
                        studentDetail.PanNumber = provider.FormData.GetValues("PanNumber")[0].String().ToUpper();
                    }
                    #endregion

                    #region  Validate BirthDate and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("txtBirthDate")[0].String()))
                    {
                        message = "Please enter Birth Date.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.Dob = Convert.ToDateTime(provider.FormData.GetValues("txtBirthDate")[0].String());
                    }

                    #endregion


                    studentDetail.BloodGroup = provider.FormData.GetValues("BloodGroup")[0].String();

                    #region  Validate PlaceofBirth and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("PlaceofBirth")[0].String()))
                    {
                        message = "Please enter Place of Birth.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.PlaceofBirth = provider.FormData.GetValues("PlaceofBirth")[0].String();
                    }

                    #endregion

                    studentDetail.Nationality = provider.FormData.GetValues("Nationality")[0].String().ToUpper();
                    studentDetail.MaritalStatus = provider.FormData.GetValues("MaritalStatus")[0].String();
                    studentDetail.Religion = provider.FormData.GetValues("Religion")[0].String();
                    studentDetail.Category = provider.FormData.GetValues("Category")[0].String();
                    studentDetail.MotherTongue = provider.FormData.GetValues("MotherTongue")[0].String();

                    #region  Validate CurrentAddress and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("CurrentAddress")[0].String()))
                    {
                        message = "Please enter Current Address.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.CurrentAddress = provider.FormData.GetValues("CurrentAddress")[0].String();
                    }

                    #endregion

                    #region  Validate PermanentAddress and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("PermanentAddress")[0].String()))
                    {
                        message = "Please enter Permanent Address.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.PermanentAddress = provider.FormData.GetValues("PermanentAddress")[0].String();
                    }

                    #endregion

                    #region  Validate EmergencyTel and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("EmergencyTel")[0].String()))
                    {
                        message = "Please enter emergency telephone number.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.EmergencyTel = provider.FormData.GetValues("EmergencyTel")[0].String();
                    }

                    #endregion

                    #region Validate MobileNumber and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("MobileNumber")[0].String()))
                    {
                        message = "Please enter mobile number.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.MobileNumber = provider.FormData.GetValues("MobileNumber")[0].String();
                    }

                    #endregion

                    #region Validate Email and also Assign Value if proper

                    if (!string.IsNullOrEmpty(provider.FormData.GetValues("Email")[0].String()))
                    {
                        string emailRegex = @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$";
                        Regex re = new Regex(emailRegex);
                        if (!re.IsMatch(provider.FormData.GetValues("Email")[0].String()))
                        {
                            message = "Please Enter Correct Email Address";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                        else
                        {
                            studentDetail.Email = provider.FormData.GetValues("Email")[0].String();
                        }
                    }
                    else
                    {
                        message = "Please Enter Email Address";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }

                    #endregion

                    studentDetail.GuardianOccupation = provider.FormData.GetValues("GuardianOccupation")[0].String();
                    studentDetail.GuardianSalary = provider.FormData.GetValues("GuardianSalary")[0].String();
                    studentDetail.RelwithGurdian = provider.FormData.GetValues("RelwithGurdian")[0].String();
                    studentDetail.EmploymentStatus = Convert.ToInt16(provider.FormData.GetValues("EmploymentStatus")[0].String());

                    #region Validate Category and also Assign Value if proper


                    if (provider.FormData.GetValues("Category")[0].String() == "Reserved")
                    {
                        studentDetail.Caste = provider.FormData.GetValues("Caste")[0].String();
                        studentDetail.SubCaste = provider.FormData.GetValues("SubCaste")[0].String();
                    }
                    else
                    {
                        studentDetail.Caste = "";
                    }

                    #endregion

                    studentDetail.AttemptofHSC = provider.FormData.GetValues("AttemptofHSC")[0].String();


                    #region Validate Employee Status and also Assign Value if proper

                    if (provider.FormData.GetValues("HscStream")[0].String().ToLower() == "science")
                    {
                        studentDetail.IsScience = true;
                    }
                    else
                    {
                        studentDetail.IsScience = false;
                    }
                    studentDetail.HscStream = provider.FormData.GetValues("HscStream")[0].String();

                    #endregion

                    #region Validate Bank Details
                    if (string.IsNullOrEmpty(provider.FormData.GetValues("BankName")[0].String()))
                    {
                        message = "Please enter Bank Name.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.BankName = provider.FormData.GetValues("BankName")[0].String();
                    }
                    if (string.IsNullOrEmpty(provider.FormData.GetValues("AccountNumber")[0].String()))
                    {
                        message = "Please enter Account Number.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.AccountNumber = provider.FormData.GetValues("AccountNumber")[0].String();
                    }
                    if (string.IsNullOrEmpty(provider.FormData.GetValues("BranchName")[0].String()))
                    {
                        message = "Please enter Branch Name.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.Branch = provider.FormData.GetValues("BranchName")[0].String();
                    }
                    if (string.IsNullOrEmpty(provider.FormData.GetValues("IFSCCode")[0].String()))
                    {
                        message = "Please enter IFSCCode.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.IFSCCode = provider.FormData.GetValues("IFSCCode")[0].String();
                    }
                    if (string.IsNullOrEmpty(provider.FormData.GetValues("AccountHolderName")[0].String()))
                    {
                        message = "Please enter Account Holder Name.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.AccountHolderName = provider.FormData.GetValues("AccountHolderName")[0].String();
                    }
                    #endregion

                    studentDetail.LastSchoolAttend = provider.FormData.GetValues("LastSchoolAttend")[0].String();
                    studentDetail.NameofExaminationBoard = provider.FormData.GetValues("NameofExaminationBoard")[0].String();
                    studentDetail.MonthLastExamPassed = Convert.ToInt16(provider.FormData.GetValues("MonthLastExamPassed")[0]);
                    studentDetail.YearLastExamPassed = Convert.ToInt16(provider.FormData.GetValues("YearLastExamPassed")[0]);

                    if (provider.FormData.GetValues("drpDwnState")[0].ToString().Trim().ToLower().Equals("please select state"))
                    {
                        message = "Please Select State Of Residence.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.StudentState = Convert.ToString(provider.FormData.GetValues("drpDwnState")[0]);
                    }


                    if (provider.FormData.AllKeys.Contains("chkLearningDisability"))
                    {
                        if (provider.FormData.GetValues("chkLearningDisability")[0].String() == "on")
                        {
                            studentDetail.IsLearningDisability = true;
                        }
                        else
                        {
                            studentDetail.IsLearningDisability = false;
                        }
                    }
                    else
                    {
                        studentDetail.IsLearningDisability = false;
                    }

                    #region Check Disability Number
                    if (studentDetail.IsLearningDisability == true)
                    {
                        if (string.IsNullOrEmpty(provider.FormData.GetValues("DisabilityCardNumber")[0].String()))
                        {
                            message = "Please Enter Disability Number";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                        else if (string.IsNullOrEmpty(provider.FormData.GetValues("DisabilityPercentage")[0].String()))
                        {
                            message = "Please Enter Disability Percentage";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                        else if (string.IsNullOrEmpty(provider.FormData.GetValues("DisabilityType")[0].String()))
                        {
                            message = "Please Enter Disability Type";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                    }
                    #endregion

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("PinCode")[0].String()))
                    {
                        message = "Please Enter PinCode";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }

                    #region Validate IsSVTStudent and also Assign Value if proper

                    if (Convert.ToInt16(provider.FormData.GetValues("Choose")[0].String()) == 1)
                    {
                        studentDetail.IsSVTStudent = true;
                    }
                    else
                    {
                        studentDetail.IsSVTStudent = false;
                    }
                    #endregion

                    #region Validate WishtojoinNCC and also Assign Value if proper

                    if ((provider.FormData.GetValues("JoinNSS")[0].String() == "1"))
                    {
                        studentDetail.WishtojoinNCC = true;
                    }
                    else
                    {
                        studentDetail.WishtojoinNCC = false;
                    }
                    #endregion

                    #region  image file is uploaded or not

                    if (provider.FileData[0] != null)
                    {
                        studentDetail.Photo = "";
                    }
                    else
                    {
                        message = "Please upload the photo.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }

                    if (provider.FileData[1] != null)
                    {
                        studentDetail.SignaturePath = "";
                    }
                    else
                    {
                        message = "Please upload the signature image.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }

                    #endregion

                    #region Check isSubmit

                    if (provider.FormData.GetValues("isSubmit")[0].String() == "1")
                    {
                        studentDetail.IsSubmitted = true;
                    }
                    else
                    {
                        studentDetail.IsSubmitted = false;
                    }


                    #endregion

                    #region subject with checking null

                    //if (string.IsNullOrEmpty(provider.FormData.GetValues("Subject1Name")[0].String()) || string.IsNullOrEmpty(provider.FormData.GetValues("Subject2Name")[0].String()) &&
                    //   string.IsNullOrEmpty(provider.FormData.GetValues("Subject3Name")[0].String()) || string.IsNullOrEmpty(provider.FormData.GetValues("Subject4Name")[0].String()) &&
                    //   string.IsNullOrEmpty(provider.FormData.GetValues("Subject5Name")[0].String()) || string.IsNullOrEmpty(provider.FormData.GetValues("Subject6Name")[0].String()))
                    //{
                    //    message = "Subject Name are mandatory";
                    //    return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    //}
                    //else
                    //{
                    studentDetail.Subject1Name = provider.FormData.GetValues("Subject1Name")[0].String();
                    studentDetail.Subject2Name = provider.FormData.GetValues("Subject2Name")[0].String();
                    studentDetail.Subject3Name = provider.FormData.GetValues("Subject3Name")[0].String();
                    studentDetail.Subject4Name = provider.FormData.GetValues("Subject4Name")[0].String();
                    studentDetail.Subject5Name = provider.FormData.GetValues("Subject5Name")[0].String();
                    studentDetail.Subject6Name = provider.FormData.GetValues("Subject6Name")[0].String();
                    studentDetail.Subject7Name = provider.FormData.GetValues("Subject7Name")[0].String();
                    //}

                    #endregion

                    #region checking subject marks

                    decimal result = 0;

                    //if (string.IsNullOrEmpty(provider.FormData.GetValues("Subject1MarksObtained")[0].String()) || string.IsNullOrEmpty(provider.FormData.GetValues("Subject2MarksObtained")[0].String()) &&
                    //   string.IsNullOrEmpty(provider.FormData.GetValues("Subject3MarksObtained")[0].String()) || string.IsNullOrEmpty(provider.FormData.GetValues("Subject4MarksObtained")[0].String()) &&
                    //   string.IsNullOrEmpty(provider.FormData.GetValues("Subject5MarksObtained")[0].String()) || string.IsNullOrEmpty(provider.FormData.GetValues("Subject6MarksObtained")[0].String()))
                    //{
                    //    message = "Marks are mandatory";
                    //    return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    //}
                    //else
                    //{
                    studentDetail.Subject1MarksObtained = (provider.FormData.GetValues("Subject1MarksObtained")[0].String()).ToDecimal();
                    studentDetail.Subject2MarksObtained = (provider.FormData.GetValues("Subject2MarksObtained")[0].String()).ToDecimal();
                    studentDetail.Subject3MarksObtained = (provider.FormData.GetValues("Subject3MarksObtained")[0].String()).ToDecimal();
                    studentDetail.Subject4MarksObtained = (provider.FormData.GetValues("Subject4MarksObtained")[0].String()).ToDecimal();
                    studentDetail.Subject5MarksObtained = (provider.FormData.GetValues("Subject5MarksObtained")[0].String()).ToDecimal();
                    studentDetail.Subject6MarksObtained = (provider.FormData.GetValues("Subject6MarksObtained")[0].String()).ToDecimal();
                    studentDetail.Subject7MarksObtained = (provider.FormData.GetValues("Subject7MarksObtained")[0].String()).ToDecimal();

                    result = (provider.FormData.GetValues("Subject1MarksObtained")[0].String()).ToDecimal() +
                             (provider.FormData.GetValues("Subject2MarksObtained")[0].String()).ToDecimal() + (provider.FormData.GetValues("Subject3MarksObtained")[0].String()).ToDecimal() +
                             (provider.FormData.GetValues("Subject4MarksObtained")[0].String()).ToDecimal() + (provider.FormData.GetValues("Subject5MarksObtained")[0].String()).ToDecimal() +
                             (provider.FormData.GetValues("Subject6MarksObtained")[0].String()).ToDecimal() + (provider.FormData.GetValues("Subject7MarksObtained")[0].String()).ToDecimal();

                    //}

                    #endregion

                    #region checking toatal of subject

                    decimal resultTotal = 0;

                    //if (string.IsNullOrEmpty(provider.FormData.GetValues("Subject1Total")[0].String()) || string.IsNullOrEmpty(provider.FormData.GetValues("Subject2Total")[0].String()) &&
                    //    string.IsNullOrEmpty(provider.FormData.GetValues("Subject3Total")[0].String()) || string.IsNullOrEmpty(provider.FormData.GetValues("Subject4Total")[0].String()) &&
                    //    string.IsNullOrEmpty(provider.FormData.GetValues("Subject5Total")[0].String()) || string.IsNullOrEmpty(provider.FormData.GetValues("Subject6Total")[0].String()))
                    //{
                    //    message = "Subject total marks are mandatory";
                    //    return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    //}
                    //else
                    //{
                    studentDetail.Subject1Total = (provider.FormData.GetValues("Subject1Total")[0].String()).ToDecimal();
                    studentDetail.Subject2Total = (provider.FormData.GetValues("Subject2Total")[0].String()).ToDecimal();
                    studentDetail.Subject3Total = (provider.FormData.GetValues("Subject3Total")[0].String()).ToDecimal();
                    studentDetail.Subject4Total = (provider.FormData.GetValues("Subject4Total")[0].String()).ToDecimal();
                    studentDetail.Subject5Total = (provider.FormData.GetValues("Subject5Total")[0].String()).ToDecimal();
                    studentDetail.Subject6Total = (provider.FormData.GetValues("Subject6Total")[0].String()).ToDecimal();
                    studentDetail.Subject7Total = (provider.FormData.GetValues("Subject7Total")[0].String()).ToDecimal();

                    resultTotal = (provider.FormData.GetValues("Subject1Total")[0].String()).ToDecimal() + (provider.FormData.GetValues("Subject2Total")[0].String()).ToDecimal() +
                                  (provider.FormData.GetValues("Subject3Total")[0].String()).ToDecimal() + (provider.FormData.GetValues("Subject4Total")[0].String()).ToDecimal() +
                                  (provider.FormData.GetValues("Subject5Total")[0].String()).ToDecimal() + (provider.FormData.GetValues("Subject6Total")[0].String()).ToDecimal() +
                                  (provider.FormData.GetValues("Subject7Total")[0].String()).ToDecimal();
                    //}

                    #endregion

                    #region  set Preference for Speclization

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("Pre1")[0].String()) && string.IsNullOrEmpty(provider.FormData.GetValues("Pre2")[0].String()) &&
                        string.IsNullOrEmpty(provider.FormData.GetValues("Pre3")[0].String()) && string.IsNullOrEmpty(provider.FormData.GetValues("Pre4")[0].String()) &&
                        string.IsNullOrEmpty(provider.FormData.GetValues("Pre5")[0].String()) && string.IsNullOrEmpty(provider.FormData.GetValues("Pre6")[0].String()))
                    {
                        message = "Preference are mandatory";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.CoursePreference1 = provider.FormData.GetValues("Pre1")[0].String();
                        studentDetail.CoursePreference2 = provider.FormData.GetValues("Pre2")[0].String();
                        studentDetail.CoursePreference3 = provider.FormData.GetValues("Pre3")[0].String();
                        studentDetail.CoursePreference4 = provider.FormData.GetValues("Pre4")[0].String();
                        studentDetail.CoursePreference5 = provider.FormData.GetValues("Pre5")[0].String();
                        studentDetail.CoursePreference6 = provider.FormData.GetValues("Pre6")[0].String();
                        if (!string.IsNullOrEmpty(provider.FormData.GetValues("Pre7")[0].String()) && provider.FormData.GetValues("HscStream")[0].String().ToLower() == "science")
                        {
                            studentDetail.CoursePreference7 = provider.FormData.GetValues("Pre7")[0].ToString();
                        }
                        else
                        {
                            studentDetail.CoursePreference7 = "";
                        }
                    }

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("Pre1Elective1")[0].String()) && string.IsNullOrEmpty(provider.FormData.GetValues("Pre1Elective2")[0].String()) &&
                        string.IsNullOrEmpty(provider.FormData.GetValues("Pre2Elective1")[0].String()) && string.IsNullOrEmpty(provider.FormData.GetValues("Pre2Elective2")[0].String()) &&
                        string.IsNullOrEmpty(provider.FormData.GetValues("Pre3Elective1")[0].String()) && string.IsNullOrEmpty(provider.FormData.GetValues("Pre3Elective2")[0].String()) &&
                        string.IsNullOrEmpty(provider.FormData.GetValues("Pre4Elective1")[0].String()) && string.IsNullOrEmpty(provider.FormData.GetValues("Pre4Elective2")[0].String()) &&
                        string.IsNullOrEmpty(provider.FormData.GetValues("Pre5Elective1")[0].String()) && string.IsNullOrEmpty(provider.FormData.GetValues("Pre5Elective2")[0].String()) &&
                        string.IsNullOrEmpty(provider.FormData.GetValues("Pre6Elective1")[0].String()) && string.IsNullOrEmpty(provider.FormData.GetValues("Pre6Elective2")[0].String())
                        )
                    {
                        message = "Elective are mandatory";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        studentDetail.Preference1GE1 = provider.FormData.GetValues("Pre1Elective1")[0].String();
                        studentDetail.Preference1GE2 = provider.FormData.GetValues("Pre1Elective2")[0].String();
                        studentDetail.Preference2GE1 = provider.FormData.GetValues("Pre2Elective1")[0].String();
                        studentDetail.Preference2GE2 = provider.FormData.GetValues("Pre2Elective2")[0].String();
                        studentDetail.Preference3GE1 = provider.FormData.GetValues("Pre3Elective1")[0].String();
                        studentDetail.Preference3GE2 = provider.FormData.GetValues("Pre3Elective2")[0].String();
                        studentDetail.Preference4GE1 = provider.FormData.GetValues("Pre4Elective1")[0].String();
                        studentDetail.Preference4GE2 = provider.FormData.GetValues("Pre4Elective2")[0].String();
                        studentDetail.Preference5GE1 = provider.FormData.GetValues("Pre5Elective1")[0].String();
                        studentDetail.Preference5GE2 = provider.FormData.GetValues("Pre5Elective2")[0].String();
                        studentDetail.Preference6GE1 = provider.FormData.GetValues("Pre6Elective1")[0].String();
                        studentDetail.Preference6GE2 = provider.FormData.GetValues("Pre6Elective2")[0].String();
                        if (!string.IsNullOrEmpty(provider.FormData.GetValues("Pre7Elective1")[0].String()) && !string.IsNullOrEmpty(provider.FormData.GetValues("Pre7Elective2")[0].String())
                            && provider.FormData.GetValues("HscStream")[0].String().ToLower() == "science")
                        {
                            studentDetail.Preference7GE1 = provider.FormData.GetValues("Pre7Elective1")[0].String();
                            studentDetail.Preference7GE2 = provider.FormData.GetValues("Pre7Elective2")[0].String();
                        }
                        else
                        {
                            studentDetail.CoursePreference7 = "";
                        }

                    }

                    #endregion

                    #region set Total Value

                    if (result > 0 && resultTotal > 0)
                    {
                        studentDetail.MarksObtain = result.ToShort();
                        studentDetail.TotalMarks = resultTotal.ToShort();
                        studentDetail.Percentage = (result * 100 / resultTotal);
                    }

                    #endregion

                    #region Set some value

                    studentDetail.IsAdmitted = false;
                    studentDetail.LastModified = DateTime.Now;
                    studentDetail.Gender = 1;

                    #endregion

                    if (!studentDetail.AttemptofHSC.ToLower().Equals("first"))
                    {
                        studentDetail.Percentage = 35;
                    }

                    if (provider.FormData.GetValues("VoterId") == null || provider.FormData.GetValues("VoterId").Length == 0 || string.IsNullOrEmpty(provider.FormData.GetValues("VoterId")[0].ToString()))
                    {
                        studentDetail.VoterId = false;
                    }
                    else
                    {
                        studentDetail.VoterId = provider.FormData.GetValues("VoterId")[0].String() == "on" ? true : false;
                        studentDetail.VoterNumber = provider.FormData.GetValues("VoterNumber")[0].String();
                    }

                    if (provider.FormData.GetValues("IsNRI") == null || provider.FormData.GetValues("IsNRI").Length == 0 || string.IsNullOrEmpty(provider.FormData.GetValues("IsNRI")[0].ToString()))
                    {
                        studentDetail.IsNRI = false;
                    }
                    else
                    {
                        studentDetail.IsNRI = provider.FormData.GetValues("IsNRI")[0].String() == "on" ? true : false;
                    }

                    if (provider.FormData.GetValues("IsHostelRequired") == null || provider.FormData.GetValues("IsHostelRequired").Length == 0 || string.IsNullOrEmpty(provider.FormData.GetValues("IsHostelRequired")[0].ToString()))
                    {
                        studentDetail.IsHostelRequired = false;
                    }
                    else
                    {
                        studentDetail.IsHostelRequired = provider.FormData.GetValues("IsHostelRequired")[0].String() == "on" ? true : false;
                        if (studentDetail.IsHostelRequired == true)
                        {
                            studentDetail.HostelReason = provider.FormData.GetValues("HostelReason")[0].String();
                        }
                    }

                    studentDetail.AboutCollege = provider.FormData.GetValues("AboutCollege")[0].String();
                    studentDetail.DisabilityNumber = provider.FormData.GetValues("DisabilityCardNumber")[0].String();
                    studentDetail.DisabilityPercentage = provider.FormData.GetValues("DisabilityPercentage")[0].String();
                    studentDetail.DisabilityType = provider.FormData.GetValues("DisabilityType") != null ? provider.FormData.GetValues("DisabilityType")[0].String() : "";
                    studentDetail.VoterNumber = provider.FormData.GetValues("VoterNumber")[0].String(); ;
                    studentDetail.ResidenceState = Convert.ToString(provider.FormData.GetValues("drpDwnState")[0]);
                    studentDetail.PinCode = provider.FormData.GetValues("PinCode")[0].String();
                    #endregion
                    #region Save Code

                    using (ServiceContext service = new ServiceContext())
                    {
                        var id = service.Save(studentDetail);
                        if (id > 0)
                        {
                            bool markDuplicate = SetDuplicateRecord(id.ToString(), studentDetail.AadharNumber);


                            var studentData = service.SelectObject<StudentDetail>(id);

                            #region Photo Upload

                            if (provider.FileData[0] != null)
                            {
                                MultipartFileData photo = provider.FileData[0];
                                if (Regex.Replace(photo.Headers.ContentDisposition.FileName.Split('.')[0], @"[^\w\d]", "") != "")
                                {
                                    string fName = photo.Headers.ContentDisposition.FileName;
                                    string[] fPartData = fName.Split('.');

                                    if (fPartData.Length > 1)
                                    {
                                        string newpath = SVT.Infrastructure.ProjectConfiguration.PhotoPath;
                                        string extantion = Regex.Replace(fPartData[fPartData.Length - 1], @"[^\w\d]", "");// Regex.Replace(photo.Headers.ContentDisposition.FileName.Split('.')[1], @"[^\w\d]", "");
                                        string photonewpath = id + "_" + studentData.FirstName + "_" + studentData.LastName + "_Photo_" + Convert.ToDateTime(studentData.DateRegistered).ToString("ddmmyyyy_hhmmss") + "." + extantion;
                                        string photonewtThumbpath = id + "_" + studentData.FirstName + "_" + studentDetail.LastName + "_Photo_Thumb_" + Convert.ToDateTime(studentData.DateRegistered).ToString("ddmmyyyy_hhmmss") + "." + extantion;
                                        FileInfo f1 = new FileInfo(photo.LocalFileName);

                                        if (f1.Exists)
                                        {
                                            if (!Directory.Exists(newpath))
                                            {
                                                Directory.CreateDirectory(newpath);
                                            }
                                            ImageCompress imgCompress = ImageCompress.GetImageCompressObject;
                                            imgCompress.GetImage = new System.Drawing.Bitmap(photo.LocalFileName);
                                            imgCompress.Height = 566;
                                            imgCompress.Width = 566;
                                            imgCompress.Save(photonewpath, newpath);
                                            imgCompress.Save(photonewtThumbpath, newpath);
                                        }
                                        studentData.Photo = photonewpath;
                                    }
                                }
                                else
                                {
                                    studentData.Photo = provider.FormData.GetValues("hdnPhoto")[0].String();
                                }
                            }
                            else
                            {
                                studentData.Photo = provider.FormData.GetValues("hdnPhoto")[0].String();
                            }

                            if (provider.FileData[1] != null)
                            {
                                MultipartFileData signature = provider.FileData[1];
                                if (Regex.Replace(signature.Headers.ContentDisposition.FileName.Split('.')[0], @"[^\w\d]", "") != "")
                                {
                                    string fsName = signature.Headers.ContentDisposition.FileName;
                                    string[] fsPartData = fsName.Split('.');

                                    if (fsPartData.Length > 1)
                                    {
                                        string newpath = SVT.Infrastructure.ProjectConfiguration.SignaturePath;
                                        string extantion = Regex.Replace(fsPartData[fsPartData.Length - 1], @"[^\w\d]", "");// Regex.Replace(signature.Headers.ContentDisposition.FileName.Split('.')[1], @"[^\w\d]", "");
                                        string signaturenewpath = id + "_" + studentData.FirstName + "_" + studentData.LastName + "_Sign_" + Convert.ToDateTime(studentData.DateRegistered).ToString("ddmmyyyy_hhmmss") + "." + extantion;
                                        string signaturenewThumbpath = id + "_" + studentData.FirstName + "_" + studentData.LastName + "_Sign_Thumb_" + Convert.ToDateTime(studentData.DateRegistered).ToString("ddmmyyyy_hhmmss") + "." + extantion;
                                        FileInfo f1 = new FileInfo(signature.LocalFileName);
                                        if (f1.Exists)
                                        {
                                            if (!Directory.Exists(newpath))
                                            {
                                                Directory.CreateDirectory(newpath);
                                            }
                                            ImageCompress imgCompress = ImageCompress.GetImageCompressObject;
                                            imgCompress.GetImage = new System.Drawing.Bitmap(signature.LocalFileName);
                                            imgCompress.Height = 566;
                                            imgCompress.Width = 566;
                                            imgCompress.Save(signaturenewpath, newpath);
                                            imgCompress.Save(signaturenewThumbpath, newpath);

                                        }
                                        studentData.SignaturePath = signaturenewpath;
                                    }
                                }
                                else
                                {
                                    studentData.SignaturePath = provider.FormData.GetValues("hdnSignature")[0].String();
                                }
                            }
                            else
                            {
                                studentData.SignaturePath = provider.FormData.GetValues("hdnSignature")[0].String();
                            }

                            #endregion

                            var repatedId = service.Save(studentData);
                            if (studentDetail.IsSubmitted == false)
                            {
                                var encryptedId = SVT.Infrastructure.EncryptionDecryption.GetEncrypt(id.ToString());
                                return Request.CreateResponse(HttpStatusCode.OK, encryptedId + ":~" + id);
                            }
                            else
                            {
                                DownloadPDF(studentData);
                                string path = "data/PDF/" + id + "_" + studentData.FirstName + "_" + studentData.LastName + "_" + Convert.ToDateTime(studentData.DateRegistered).ToString("ddmmyyyy_hhmmss") + ".pdf";
                                return Request.CreateResponse(HttpStatusCode.OK, path + ":~" + id);
                            }
                        }
                        else
                        {
                            message = "An error occurred in code.";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                    }
                    #endregion
                }
                catch (System.Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message + e.StackTrace);
                }
                finally
                {
                    string[] files = Directory.GetFiles(root);
                    foreach (string file in files)
                    {
                        FileInfo fi = new FileInfo(file);
                        if (fi.LastAccessTime < DateTime.Now.AddDays(-1))
                            fi.Delete();
                    }
                }
            }
            else
            {
                message = "An error occurred in code.";
                return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
            }
        }

        #region Get Seleted Date

        [HttpGet]
        public bool GetFormExpireDate()
        {
            return false;// CheckFormExpireDate();
        }

        public bool CheckFormExpireDate()
        {
            if (Convert.ToDateTime(ProjectConfiguration.FormExpireDate) <= DateTime.UtcNow)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Edit Detail

        [HttpGet]
        public StudentDetail GetEditDetail(string id)
        {
            StudentDetail studentDetail = new StudentDetail();

            if (!GetFormExpireDate())
            {
                using (ServiceContext service = new ServiceContext())
                {
                    if (id != null)
                    {
                        var newid = SVT.Infrastructure.EncryptionDecryption.GetDecrypt(id);
                        studentDetail.Id = newid.ToInteger();
                    }
                    var student = service.Search(studentDetail, 1, null, null).FirstOrDefault();

                    student.StudentPdfURL = "data/PDF/" + student.Id + "_" + student.FirstName + "_" + student.LastName + "_" + Convert.ToDateTime(student.DateRegistered).ToString("ddmmyyyy_hhmmss") + ".pdf";
                    return student;
                }
            }
            return studentDetail;
        }

        #endregion

        #region Get Detail and PDF Part

        [HttpGet]
        public HttpResponseMessage GetStudentDetail(string formNumber, string mkclFormNumber, string lastName, DateTime? dateofBirth)
        {
            if (string.IsNullOrEmpty(lastName) || dateofBirth == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Please pass proper parameter");
            }
            else
            {
                if (string.IsNullOrEmpty(formNumber))
                {
                    if (string.IsNullOrEmpty(mkclFormNumber))
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Please pass proper parameter");
                    }
                }
            }
            StudentDetail searchstudentDetail = new StudentDetail();
            if (!GetFormExpireDate())
            {
                using (ServiceContext service = new ServiceContext())
                {
                    int fNumber = 0;
                    fNumber = formNumber.ToInteger();
                    if (fNumber > 0)
                    {
                        searchstudentDetail.Id = fNumber;
                    }
                    if (mkclFormNumber != null)
                    {
                        searchstudentDetail.MKCLFormNumber = mkclFormNumber;
                    }
                    if (lastName != null)
                    {
                        searchstudentDetail.LastName = lastName;
                    }
                    if (dateofBirth != null)
                    {
                        searchstudentDetail.Dob = dateofBirth;
                    }
                    var student = service.Search(searchstudentDetail, 1, null, null).FirstOrDefault();

                    if (student != null)
                    {
                        StudentDetail selectedvalue = new StudentDetail();
                        selectedvalue.Id = student.Id;
                        selectedvalue.IsSubmitted = student.IsSubmitted;
                        selectedvalue.Photo = "data/Photos/" + student.Photo;
                        selectedvalue.FirstName = student.FirstName;
                        selectedvalue.LastName = student.LastName;
                        selectedvalue.EncryptedId = SVT.Infrastructure.EncryptionDecryption.GetEncrypt(selectedvalue.Id.ToString());
                        selectedvalue.StudentPdfURL = "data/Pdf/" + student.Id + "_" + student.FirstName + "_" + student.LastName + "_" + Convert.ToDateTime(student.DateRegistered).ToString("ddmmyyyy_hhmmss") + ".pdf";

                        selectedvalue.CourseAdmittedRound1 = student.CourseAdmittedRound1;
                        selectedvalue.CourseAdmittedRound2 = student.CourseAdmittedRound2;
                        selectedvalue.CourseAdmittedRound3 = student.CourseAdmittedRound3;
                        selectedvalue.AdmittedRound1 = student.AdmittedRound1;
                        selectedvalue.AdmittedRound2 = student.AdmittedRound2;
                        selectedvalue.AdmittedRound3 = student.AdmittedRound3;
                        return Request.CreateResponse(HttpStatusCode.OK, selectedvalue);
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, student);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, searchstudentDetail);
        }

        [HttpGet]
        public HttpResponseMessage GetReport()
        {
            DownloadMeritListReports();
            return Request.CreateResponse(HttpStatusCode.OK, "Report generated successfully");
        }

        public static void DownloadPDF(StudentDetail studentDetailModel)
        {
            string templateName = studentDetailModel.IsHostelRequired == true ? "~/Templates/PDFHtmlHostel.html" : "~/Templates/PDFHtml.html";
            string HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(templateName));

            if (studentDetailModel != null)
            {
                #region Form Detail

                if (!string.IsNullOrEmpty(studentDetailModel.MKCLFormNumber) && !(studentDetailModel.MKCLFormNumber.Trim().Equals("0")))
                    HTMLContent = HTMLContent.Replace("{{ MKCLno }}", studentDetailModel.MKCLFormNumber);
                else
                    HTMLContent = HTMLContent.Replace("{{ MKCLno }}", "");
                HTMLContent = HTMLContent.Replace("{{ Fno }}", studentDetailModel.Id.ToString());
                HTMLContent = HTMLContent.Replace("{{ Surname }}", studentDetailModel.LastName);
                HTMLContent = HTMLContent.Replace("{{ Firstname }}", studentDetailModel.FirstName);
                HTMLContent = HTMLContent.Replace("{{ Middlename }}", studentDetailModel.FatherName);
                HTMLContent = HTMLContent.Replace("{{ Mothername }}", studentDetailModel.MotherName);

                // update it
                HTMLContent = HTMLContent.Replace("{{ Specialization }}", "");
                HTMLContent = HTMLContent.Replace("{{ Incharge }}", "");

                HTMLContent = HTMLContent.Replace("{{ Date }}", studentDetailModel.DateRegistered.ToString());
                if (studentDetailModel.IsSVTStudent != null && studentDetailModel.IsSVTStudent.Value)
                {
                    HTMLContent = HTMLContent.Replace("{{ IsCol }}", "YES");
                    HTMLContent = HTMLContent.Replace("{{ IsOtherCol }}", " ");
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ IsCol }}", " ");
                    HTMLContent = HTMLContent.Replace("{{ IsOtherCol }}", "YES");
                }

                #endregion

                #region Set Photo Path

                string ImagePath = string.Empty;
                string Image = string.Empty;

                if (studentDetailModel.Photo != null)
                {
                    ImagePath = ProjectConfiguration.PhotoPath + studentDetailModel.Photo;
                    using (System.Drawing.Image image = System.Drawing.Image.FromFile(ImagePath))
                    {
                        using (MemoryStream m = new MemoryStream())
                        {
                            image.Save(m, image.RawFormat);
                            byte[] imageBytes = m.ToArray();
                            Image = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(imageBytes));
                        }
                    }

                    HTMLContent = HTMLContent.Replace("{{ Image }}", ImagePath);
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ Image }}", "");
                }
                #endregion

                #region Set signature

                ImagePath = string.Empty;

                if (studentDetailModel.SignaturePath != null)
                {
                    ImagePath = ProjectConfiguration.SignaturePath + studentDetailModel.SignaturePath;
                    using (System.Drawing.Image image = System.Drawing.Image.FromFile(ImagePath))
                    {
                        using (MemoryStream m = new MemoryStream())
                        {
                            image.Save(m, image.RawFormat);
                            byte[] imageBytes = m.ToArray();
                            Image = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(imageBytes));
                        }
                    }
                    HTMLContent = HTMLContent.Replace("{{ Sign }}", ImagePath);
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ Sign }}", "");
                }
                #endregion

                #region Set preference

                string preference = string.Empty;

                preference += @"<tr><td style='width:95%;'>1." + studentDetailModel.CoursePreference1 + "</td></tr>";
                preference += @"<tr><td style='width:95%;'>2." + studentDetailModel.CoursePreference2 + "</td></tr>";
                preference += @"<tr><td style='width:95%;'>3." + studentDetailModel.CoursePreference3 + "</td></tr>";
                preference += @"<tr><td style='width:95%;'>4." + studentDetailModel.CoursePreference4 + "</td></tr>";
                preference += @"<tr><td style='width:95%;'>5." + studentDetailModel.CoursePreference5 + "</td></tr>";
                preference += @"<tr><td style='width:95%;'>6." + studentDetailModel.CoursePreference6 + "</td></tr>";

                if (studentDetailModel.HscStream.ToLower() == "science")
                {
                    preference += @"<tr><td style='width:95%;'>7." + studentDetailModel.CoursePreference7 + "</td></tr>";
                }

                HTMLContent = HTMLContent.Replace("{{ Preference }}", preference);

                #endregion

                HTMLContent = HTMLContent.Replace("{{ ExamPassMonthYear }}", studentDetailModel.YearLastExamPassed.ToString());
                HTMLContent = HTMLContent.Replace("{{ ExamBoard }}", studentDetailModel.NameofExaminationBoard);
                HTMLContent = HTMLContent.Replace("{{ Stream }}", studentDetailModel.HscStream);
                HTMLContent = HTMLContent.Replace("{{ Attempt }}", studentDetailModel.AttemptofHSC);
                HTMLContent = HTMLContent.Replace("{{ FullName }}", studentDetailModel.LastName + " " + studentDetailModel.FirstName + " " + studentDetailModel.FatherName + " " + studentDetailModel.MotherName);
                HTMLContent = HTMLContent.Replace("{{ PreviousCollegeName }}", studentDetailModel.LastSchoolAttend);
                HTMLContent = HTMLContent.Replace("{{ LastExaminationBoard }}", studentDetailModel.NameofExaminationBoard);

                #region Set Subject Detail

                int sub1Total, sub2Total, sub3Total, sub4Total, sub5Total, sub6Total, sub7Total = 0;
                int.TryParse(studentDetailModel.Subject1Total.ToString().Replace(".000", ""), out sub1Total);
                int.TryParse(studentDetailModel.Subject2Total.ToString().Replace(".000", ""), out sub2Total);
                int.TryParse(studentDetailModel.Subject3Total.ToString().Replace(".000", ""), out sub3Total);
                int.TryParse(studentDetailModel.Subject4Total.ToString().Replace(".000", ""), out sub4Total);
                int.TryParse(studentDetailModel.Subject5Total.ToString().Replace(".000", ""), out sub5Total);
                int.TryParse(studentDetailModel.Subject6Total.ToString().Replace(".000", ""), out sub6Total);
                int.TryParse(studentDetailModel.Subject7Total.ToString().Replace(".000", ""), out sub7Total);

                HTMLContent = HTMLContent.Replace("{{ Sub1 }}", studentDetailModel.Subject1Name.Replace("&", "and"));
                HTMLContent = HTMLContent.Replace("{{ Sub2 }}", studentDetailModel.Subject2Name.Replace("&", "and"));
                HTMLContent = HTMLContent.Replace("{{ Sub3 }}", studentDetailModel.Subject3Name.Replace("&", "and"));
                HTMLContent = HTMLContent.Replace("{{ Sub4 }}", studentDetailModel.Subject4Name.Replace("&", "and"));
                HTMLContent = HTMLContent.Replace("{{ Sub5 }}", studentDetailModel.Subject5Name.Replace("&", "and"));
                HTMLContent = HTMLContent.Replace("{{ Sub6 }}", studentDetailModel.Subject6Name.Replace("&", "and"));
                HTMLContent = HTMLContent.Replace("{{ Sub7 }}", studentDetailModel.Subject7Name.Replace("&", "and"));
                HTMLContent = HTMLContent.Replace("{{ Sub1From }}", sub1Total.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub2From }}", sub2Total.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub3From }}", sub3Total.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub4From }}", sub4Total.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub5From }}", sub5Total.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub6From }}", sub6Total.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub7From }}", sub7Total.ToString());

                int total = 0;

                total = int.Parse(studentDetailModel.Subject1Total.ToString().Replace(".000", "")) +
                        int.Parse(studentDetailModel.Subject2Total.ToString().Replace(".000", "")) +
                        int.Parse(studentDetailModel.Subject3Total.ToString().Replace(".000", "")) +
                        int.Parse(studentDetailModel.Subject4Total.ToString().Replace(".000", "")) +
                        int.Parse(studentDetailModel.Subject5Total.ToString().Replace(".000", "")) +
                        int.Parse(studentDetailModel.Subject6Total.ToString().Replace(".000", "")) +
                        int.Parse(studentDetailModel.Subject7Total.ToString().Replace(".000", ""));

                string t = (total == 0) ? studentDetailModel.TotalMarks.ToString() : total.ToString();
                HTMLContent = HTMLContent.Replace("{{ TotalFrom }}", t);

                int sub1Marks, sub2Marks, sub3Marks, sub4Marks, sub5Marks, sub6Marks, sub7Marks = 0;
                int.TryParse(studentDetailModel.Subject1MarksObtained.ToString().Replace(".000", ""), out sub1Marks);
                int.TryParse(studentDetailModel.Subject2MarksObtained.ToString().Replace(".000", ""), out sub2Marks);
                int.TryParse(studentDetailModel.Subject3MarksObtained.ToString().Replace(".000", ""), out sub3Marks);
                int.TryParse(studentDetailModel.Subject4MarksObtained.ToString().Replace(".000", ""), out sub4Marks);
                int.TryParse(studentDetailModel.Subject5MarksObtained.ToString().Replace(".000", ""), out sub5Marks);
                int.TryParse(studentDetailModel.Subject6MarksObtained.ToString().Replace(".000", ""), out sub6Marks);
                int.TryParse(studentDetailModel.Subject7MarksObtained.ToString().Replace(".000", ""), out sub7Marks);

                HTMLContent = HTMLContent.Replace("{{ TotalPerFrom }}", "100%");
                HTMLContent = HTMLContent.Replace("{{ Sub1Mark }}", sub1Marks.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub2Mark }}", sub2Marks.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub3Mark }}", sub3Marks.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub4Mark }}", sub4Marks.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub5Mark }}", sub5Marks.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub6Mark }}", sub6Marks.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub7Mark }}", sub7Marks.ToString());
                HTMLContent = HTMLContent.Replace("{{ TotalMark }}", studentDetailModel.MarksObtain.ToString());
                HTMLContent = HTMLContent.Replace("{{ TotalPerMark }}", studentDetailModel.Percentage.ToString());

                #endregion

                HTMLContent = HTMLContent.Replace("{{ AcademicHonors }}", "");
                HTMLContent = HTMLContent.Replace("{{ FLName }}", studentDetailModel.FatherLastName);
                HTMLContent = HTMLContent.Replace("{{ FFName }}", studentDetailModel.FatherFirstName);
                HTMLContent = HTMLContent.Replace("{{ FMName }}", studentDetailModel.FatherMiddleName);
                HTMLContent = HTMLContent.Replace("{{ MLName }}", studentDetailModel.MotherLastName);
                HTMLContent = HTMLContent.Replace("{{ MFName }}", studentDetailModel.MotherFirstName);
                HTMLContent = HTMLContent.Replace("{{ MMName }}", studentDetailModel.MotherMiddleName);

                #region Set Category

                HTMLContent = HTMLContent.Replace("{{ Religion }}", studentDetailModel.Religion);
                if (studentDetailModel.Category.ToLower().Equals("open"))
                {
                    HTMLContent = HTMLContent.Replace("{{ Category }}", "Open");
                    HTMLContent = HTMLContent.Replace("{{ Reservation }}", " ");
                    HTMLContent = HTMLContent.Replace("{{ SubCaste }}", " ");
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ Category }}", "Reserved");
                    HTMLContent = HTMLContent.Replace("{{ Caste }}", studentDetailModel.Caste);
                    HTMLContent = HTMLContent.Replace("{{ SubCaste }}", studentDetailModel.SubCaste);
                }
                HTMLContent = HTMLContent.Replace("{{ Caste }}", " ");

                #endregion

                #region Personal Detail

                HTMLContent = HTMLContent.Replace("{{ MaritalStatus }}", studentDetailModel.MaritalStatus);
                HTMLContent = HTMLContent.Replace("{{ BirthPlace }}", studentDetailModel.PlaceofBirth);
                HTMLContent = HTMLContent.Replace("{{ BloodGroup }}", studentDetailModel.BloodGroup);
                HTMLContent = HTMLContent.Replace("{{ CountryName }}", studentDetailModel.Nationality);
                HTMLContent = HTMLContent.Replace("{{ AadharCard }}", studentDetailModel.AadharNumber);
                HTMLContent = HTMLContent.Replace("{{ BirthDate }}", studentDetailModel.Dob.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                HTMLContent = HTMLContent.Replace("{{ PinCode }}", studentDetailModel.PinCode.ToString());
                HTMLContent = HTMLContent.Replace("{{ DisabilityNumber }}", studentDetailModel.DisabilityNumber.ToString());
                HTMLContent = HTMLContent.Replace("{{ State }}", studentDetailModel.ResidenceState.ToString());
                HTMLContent = HTMLContent.Replace("{{ DisabilityType }}", studentDetailModel.DisabilityType.ToString());
                HTMLContent = HTMLContent.Replace("{{ DisabilityPercentage }}", studentDetailModel.DisabilityPercentage.ToString());

                if (studentDetailModel.IsNRI == true)
                {
                    HTMLContent = HTMLContent.Replace("{{ IsNRI }}", "Yes");
                }
                else
                    HTMLContent = HTMLContent.Replace("{{ IsNRI }}", "No");

                if (studentDetailModel.IsHostelRequired == true)
                {
                    HTMLContent = HTMLContent.Replace("{{ IsHostelRequired }}", "Yes");
                }
                else
                    HTMLContent = HTMLContent.Replace("{{ IsHostelRequired }}", "No");

                if (studentDetailModel.VoterId == true)
                {
                    HTMLContent = HTMLContent.Replace("{{ VoterId }}", "Yes");
                    HTMLContent = HTMLContent.Replace("{{ VoterNumber }}", studentDetailModel.VoterNumber);
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ VoterId }}", "No");
                    HTMLContent = HTMLContent.Replace("{{ VoterNumber }}", studentDetailModel.VoterNumber);
                }


                if (studentDetailModel.EmploymentStatus == 1)
                {
                    HTMLContent = HTMLContent.Replace("{{ EmploymentStatus }}", "Employed");
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ EmploymentStatus }}", "Unemployed");
                }

                if (studentDetailModel.IsLearningDisability == true)
                {
                    HTMLContent = HTMLContent.Replace("{{ LearningDisability }}", "Yes");
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ LearningDisability }}", "No");
                }

                HTMLContent = HTMLContent.Replace("{{ Address1 }}", studentDetailModel.CurrentAddress.Replace("(", "").Replace(")", ""));
                HTMLContent = HTMLContent.Replace("{{ Address2 }}", studentDetailModel.PermanentAddress.Replace("(", "").Replace(")", ""));
                HTMLContent = HTMLContent.Replace("{{ Occupation }}", studentDetailModel.GuardianOccupation);
                HTMLContent = HTMLContent.Replace("{{ ETelNum }}", studentDetailModel.EmergencyTel.ToString());
                HTMLContent = HTMLContent.Replace("{{ MNum }}", studentDetailModel.MobileNumber);
                HTMLContent = HTMLContent.Replace("{{ Email }}", studentDetailModel.Email);
                HTMLContent = HTMLContent.Replace("{{ MTongue }}", studentDetailModel.MotherTongue);
                HTMLContent = HTMLContent.Replace("{{ WNCC }}", studentDetailModel.WishtojoinNCC.ToString());
                HTMLContent = HTMLContent.Replace("{{ GIncome }}", studentDetailModel.GuardianSalary);
                HTMLContent = HTMLContent.Replace("{{ GRelationship }}", studentDetailModel.RelwithGurdian);

                #endregion

                HTMLContent = HTMLContent.Replace("{{ AFirst }}", "");
                HTMLContent = HTMLContent.Replace("{{ ASecond }}", "");
                HTMLContent = HTMLContent.Replace("{{ AThird }}", "");
                HTMLContent = HTMLContent.Replace("{{ AFourth }}", "");
                HTMLContent = HTMLContent.Replace("{{ AFifth }}", "");
                HTMLContent = HTMLContent.Replace("{{ ASixth }}", "");
                HTMLContent = HTMLContent.Replace("{{ ASeventh }}", "");
                HTMLContent = HTMLContent.Replace("{{ AEighth }}", "");
                HTMLContent = HTMLContent.Replace("{{ ANinth }}", "");
                HTMLContent = HTMLContent.Replace("{{ ATenth }}", "");

                #region Set Course Preference Value

                HTMLContent = HTMLContent.Replace("{{ CoursePreference1 }}", studentDetailModel.CoursePreference1);
                HTMLContent = HTMLContent.Replace("{{ CoursePreference2 }}", studentDetailModel.CoursePreference2);
                HTMLContent = HTMLContent.Replace("{{ CoursePreference3 }}", studentDetailModel.CoursePreference3);
                HTMLContent = HTMLContent.Replace("{{ CoursePreference4 }}", studentDetailModel.CoursePreference4);
                HTMLContent = HTMLContent.Replace("{{ CoursePreference5 }}", studentDetailModel.CoursePreference5);
                HTMLContent = HTMLContent.Replace("{{ CoursePreference6 }}", studentDetailModel.CoursePreference6);
                HTMLContent = HTMLContent.Replace("{{ CoursePreference7 }}", studentDetailModel.CoursePreference7);

                HTMLContent = HTMLContent.Replace("{{ Elective1 }}", studentDetailModel.Preference1GE1);
                HTMLContent = HTMLContent.Replace("{{ Elective2 }}", studentDetailModel.Preference2GE1);
                HTMLContent = HTMLContent.Replace("{{ Elective3 }}", studentDetailModel.Preference3GE1);
                HTMLContent = HTMLContent.Replace("{{ Elective4 }}", studentDetailModel.Preference4GE1);
                HTMLContent = HTMLContent.Replace("{{ Elective5 }}", studentDetailModel.Preference5GE1);
                HTMLContent = HTMLContent.Replace("{{ Elective6 }}", studentDetailModel.Preference6GE1);
                HTMLContent = HTMLContent.Replace("{{ Elective7 }}", studentDetailModel.Preference7GE1);

                HTMLContent = HTMLContent.Replace("{{ Elective2_1 }}", studentDetailModel.Preference1GE2);
                HTMLContent = HTMLContent.Replace("{{ Elective2_2 }}", studentDetailModel.Preference2GE2);
                HTMLContent = HTMLContent.Replace("{{ Elective2_3 }}", studentDetailModel.Preference3GE2);
                HTMLContent = HTMLContent.Replace("{{ Elective2_4 }}", studentDetailModel.Preference4GE2);
                HTMLContent = HTMLContent.Replace("{{ Elective2_5 }}", studentDetailModel.Preference5GE2);
                HTMLContent = HTMLContent.Replace("{{ Elective2_6 }}", studentDetailModel.Preference6GE2);
                HTMLContent = HTMLContent.Replace("{{ Elective2_7 }}", studentDetailModel.Preference7GE2);


                HTMLContent = HTMLContent.Replace("{{ CoursePreference1 }}", "");
                HTMLContent = HTMLContent.Replace("{{ CoursePreference2 }}", "");
                HTMLContent = HTMLContent.Replace("{{ CoursePreference3 }}", "");
                HTMLContent = HTMLContent.Replace("{{ CoursePreference4 }}", "");
                HTMLContent = HTMLContent.Replace("{{ CoursePreference5 }}", "");
                HTMLContent = HTMLContent.Replace("{{ CoursePreference6 }}", "");
                HTMLContent = HTMLContent.Replace("{{ CoursePreference7 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective1 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective2 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective3 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective4 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective5 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective6 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective7 }}", "");

                HTMLContent = HTMLContent.Replace("{{ Elective2_1 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective2_2 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective2_3 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective2_4 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective2_5 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective2_6 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective2_7 }}", "");
                HTMLContent = HTMLContent.Replace("{{ AboutCollege }}", studentDetailModel.AboutCollege);
                #endregion

                // Populate values in hostel html
                if (studentDetailModel.IsHostelRequired == true)
                {
                    HTMLContent = HTMLContent.Replace("{{ HostelReason }}", studentDetailModel.HostelReason);
                }

            }
            GetPDF(HTMLContent, studentDetailModel.FirstName, studentDetailModel.LastName, studentDetailModel.Id.ToString(), studentDetailModel.DateRegistered);
        }

        public static void GetPDF(string pHTML, string firstName, string lastName, string formNumber, DateTime? registerDate)
        {
            var tagProcessors = (DefaultTagProcessorFactory)Tags.GetHtmlTagProcessorFactory();
            tagProcessors.RemoveProcessor(HTML.Tag.IMG);
            tagProcessors.AddProcessor(HTML.Tag.IMG, new CustomImageTagProcessor());
            byte[] bPDF = null;

            MemoryStream ms = new MemoryStream();
            TextReader txtReader = new StringReader(pHTML);

            Document doc = new Document(PageSize.A4, 25, 25, 25, 25);
            PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms);

            doc.Open();

            CssFilesImpl cssFiles = new CssFilesImpl();
            cssFiles.Add(XMLWorkerHelper.GetInstance().GetDefaultCSS());
            var cssResolver = new StyleAttrCSSResolver(cssFiles);
            cssResolver.AddCss(@"table { font-size:12px; }", "utf-8", true);
            var charset = Encoding.UTF8;
            var hpc = new HtmlPipelineContext(new CssAppliersImpl(new XMLWorkerFontProvider()));
            hpc.SetAcceptUnknown(true).AutoBookmark(true).SetTagFactory(tagProcessors);
            var htmlPipeline = new HtmlPipeline(hpc, new PdfWriterPipeline(doc, oPdfWriter));
            var pipeline = new CssResolverPipeline(cssResolver, htmlPipeline);
            var worker = new XMLWorker(pipeline, true);
            var xmlParser = new XMLParser(true, worker, charset);
            xmlParser.Parse(txtReader);

            doc.Close();

            bPDF = ms.ToArray();
            //string path = ProjectConfiguration.FilePath + formNumber + "_" + firstName + "_" + lastName + "_" + registerDate.ToString() + ".pdf";

            string path = ProjectConfiguration.PDFPath + formNumber + "_" + firstName + "_" + lastName + "_" + Convert.ToDateTime(registerDate).ToString("ddmmyyyy_hhmmss") + ".pdf";
            File.WriteAllBytes(path, bPDF);

            //  return formNumber + "_" + firstName + "_" + lastName + "_" + registerDate + ".pdf";
            // return bPDF;
        }

        public static void GetMeritList(string pHTML, string fileName, string folderName)
        {
            var tagProcessors = (DefaultTagProcessorFactory)Tags.GetHtmlTagProcessorFactory();
            tagProcessors.RemoveProcessor(HTML.Tag.IMG);
            tagProcessors.AddProcessor(HTML.Tag.IMG, new CustomImageTagProcessor());
            byte[] bPDF = null;

            MemoryStream ms = new MemoryStream();
            TextReader txtReader = new StringReader(pHTML);

            Document doc = new Document(PageSize.A4.Rotate(), 25, 25, 25, 25);
            PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, ms);

            doc.Open();

            CssFilesImpl cssFiles = new CssFilesImpl();
            cssFiles.Add(XMLWorkerHelper.GetInstance().GetDefaultCSS());
            var cssResolver = new StyleAttrCSSResolver(cssFiles);
            cssResolver.AddCss(@"table { font-size:12px; }", "utf-8", true);
            var charset = Encoding.UTF8;
            var hpc = new HtmlPipelineContext(new CssAppliersImpl(new XMLWorkerFontProvider()));
            hpc.SetAcceptUnknown(true).AutoBookmark(true).SetTagFactory(tagProcessors);
            var htmlPipeline = new HtmlPipeline(hpc, new PdfWriterPipeline(doc, oPdfWriter));
            var pipeline = new CssResolverPipeline(cssResolver, htmlPipeline);
            var worker = new XMLWorker(pipeline, true);
            var xmlParser = new XMLParser(true, worker, charset);
            xmlParser.Parse(txtReader);
            doc.Close();
            bPDF = ms.ToArray();

            string path = ProjectConfiguration.PDFPath + "/Reports/" + folderName + "/" + fileName + "_" + DateTime.Now.ToString("ddmmyyyy_hhmmss") + ".pdf";
            File.WriteAllBytes(path, bPDF);
        }

        public class CustomImageTagProcessor : iTextSharp.tool.xml.html.Image
        {
            public override IList<IElement> End(IWorkerContext ctx, Tag tag, IList<IElement> currentContent)
            {
                IDictionary<string, string> attributes = tag.Attributes;
                string src;
                if (!attributes.TryGetValue(HTML.Attribute.SRC, out src))
                    return new List<IElement>(1);

                if (string.IsNullOrEmpty(src))
                    return new List<IElement>(1);

                if (src.StartsWith("data:image/", StringComparison.InvariantCultureIgnoreCase))
                {
                    var base64Data = src.Substring(src.IndexOf(",") + 1);
                    var imagedata = Convert.FromBase64String(base64Data);
                    var image = iTextSharp.text.Image.GetInstance(imagedata);

                    var list = new List<IElement>();
                    var htmlPipelineContext = GetHtmlPipelineContext(ctx);
                    list.Add(GetCssAppliers().Apply(new Chunk((iTextSharp.text.Image)GetCssAppliers().Apply(image, tag, htmlPipelineContext), 0, 0, true), tag, htmlPipelineContext));
                    return list;
                }
                else
                {
                    return base.End(ctx, tag, currentContent);
                }
            }
        }


        //If you want to call any custom store procedue.
        //using (SVT.Business.Service.DbProcedureCall service = new SVT.Business.Service.DbProcedureCall())
        //    {
        //        object retValue = service.CallStoreProcedureWithParam(ids,"test");
        //    }
        #endregion

        //#region CutOff Round Logic

        [HttpPost]
        public string MeritListLogic(bool isSVT, string category, string round)
        {
            if (string.IsNullOrEmpty(category) || (category.ToUpper() != "OPEN" && category.ToUpper() != "RESERVED")
                || string.IsNullOrEmpty(round) || (round.ToUpper() != "ROUND1" && round.ToUpper() != "ROUND2" && round.ToUpper() != "ROUND3"))
            {
                return "Please pass proper parameter";
            }

            if (round == "Round1")
            {
                GetRound1PreviewList(isSVT, category, round, true);
                return "Merit List for Round " + round + " generated successfully";
            }

            var roundlist = new List<RoundList>();
            //Get Seats json list item
            var seatsList = ReadSeatsFile();
            var reservedSeats = GetReservationSeat();
            isSVT = true;
            category = "Reserved";
            //Get Round json list item
            roundlist = ReadRoundFile(round);

            if (seatsList != null && seatsList.Count > 1 && roundlist != null && roundlist.Count > 1)
            {
                #region Internal Reserved
                try
                {
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);
                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"
                            bool isAdmittedinCurrentRound = false;
                            string lastAdmittedCourse = string.Empty;
                            bool leaveStudent = false;
                            if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"

                            bool SVTOpenInternal = false;
                            bool SVTReservedInternal = false;
                            bool ExternalOpen = false;
                            bool ExternalReserved = false;
                            //Get Categoy and SVT student wise but value
                            if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                            {
                                SVTOpenInternal = true;
                            }
                            else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                            {
                                ExternalOpen = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                            {
                                SVTReservedInternal = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                            {
                                ExternalReserved = true;
                            }

                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                            {
                                service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                UpdateSeatData(seatsList);
                            }
                            else if (leaveStudent == false)
                            {

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                {
                                    service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                    UpdateSeatData(seatsList);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                    {
                                        service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                        UpdateSeatData(seatsList);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                        {
                                            service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                            UpdateSeatData(seatsList);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                            {
                                                service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                UpdateSeatData(seatsList);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                {
                                                    service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                    UpdateSeatData(seatsList);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                    {
                                                        service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                        UpdateSeatData(seatsList);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        seatsList.Where(w => w.SVTReservedInternal > 0).ToList().ForEach(i => { i.ExternalReserved = i.ExternalReserved + i.SVTReservedInternal; i.SVTReservedInternal = 0; });

                        UpdateSeatData(seatsList);
                        AdjustSeats(reservedSeats, "internal");
                        UpdateReservedQuotaSeats(reservedSeats);
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                #endregion Internal Reserved

                seatsList = ReadSeatsFile();
                reservedSeats = GetReservationSeat();
                isSVT = false;

                #region External Reserved
                try
                {
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);
                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"
                            bool isAdmittedinCurrentRound = false;
                            string lastAdmittedCourse = string.Empty;
                            bool leaveStudent = false;
                            if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"

                            bool SVTOpenInternal = false;
                            bool SVTReservedInternal = false;
                            bool ExternalOpen = false;
                            bool ExternalReserved = false;
                            //Get Categoy and SVT student wise but value
                            if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                            {
                                SVTOpenInternal = true;
                            }
                            else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                            {
                                ExternalOpen = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                            {
                                SVTReservedInternal = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                            {
                                ExternalReserved = true;
                            }

                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                            {
                                service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                UpdateSeatData(seatsList);
                            }
                            else if (leaveStudent == false)
                            {

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                {
                                    service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                    UpdateSeatData(seatsList);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                    {
                                        service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                        UpdateSeatData(seatsList);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                        {
                                            service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                            UpdateSeatData(seatsList);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                            {
                                                service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                UpdateSeatData(seatsList);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                {
                                                    service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                    UpdateSeatData(seatsList);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                    {
                                                        service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                        UpdateSeatData(seatsList);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        seatsList.Where(w => w.ExternalReserved > 0).ToList().ForEach(i => { i.SVTOpenInternal = i.SVTOpenInternal + i.ExternalReserved; i.ExternalReserved = 0; });
                        UpdateSeatData(seatsList);
                        UpdateReservedQuotaSeats(reservedSeats);
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                #endregion External Reserved

                seatsList = ReadSeatsFile();
                reservedSeats = GetReservationSeat();
                isSVT = true;
                category = "Open";

                #region Internal Open
                try
                {
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);
                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"
                            bool isAdmittedinCurrentRound = false;
                            string lastAdmittedCourse = string.Empty;
                            bool leaveStudent = false;
                            if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"

                            bool SVTOpenInternal = false;
                            bool SVTReservedInternal = false;
                            bool ExternalOpen = false;
                            bool ExternalReserved = false;
                            //Get Categoy and SVT student wise but value
                            if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                            {
                                SVTOpenInternal = true;
                            }
                            else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                            {
                                ExternalOpen = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                            {
                                SVTReservedInternal = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                            {
                                ExternalReserved = true;
                            }

                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                            {
                                service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                UpdateSeatData(seatsList);
                            }
                            else if (leaveStudent == false)
                            {

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                {
                                    service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                    UpdateSeatData(seatsList);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                    {
                                        service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                        UpdateSeatData(seatsList);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                        {
                                            service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                            UpdateSeatData(seatsList);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                            {
                                                service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                UpdateSeatData(seatsList);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                {
                                                    service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                    UpdateSeatData(seatsList);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                    {
                                                        service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                        UpdateSeatData(seatsList);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        seatsList.Where(w => w.SVTOpenInternal > 0).ToList().ForEach(i => { i.ExternalOpen = i.SVTOpenInternal + i.ExternalOpen; i.SVTOpenInternal = 0; });
                        UpdateSeatData(seatsList);
                        UpdateReservedQuotaSeats(reservedSeats);
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                #endregion Internal Open

                seatsList = ReadSeatsFile();
                reservedSeats = GetReservationSeat();
                isSVT = false;

                #region External Open
                try
                {
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);
                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"
                            bool isAdmittedinCurrentRound = false;
                            string lastAdmittedCourse = string.Empty;
                            bool leaveStudent = false;
                            if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"

                            bool SVTOpenInternal = false;
                            bool SVTReservedInternal = false;
                            bool ExternalOpen = false;
                            bool ExternalReserved = false;
                            //Get Categoy and SVT student wise but value
                            if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                            {
                                SVTOpenInternal = true;
                            }
                            else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                            {
                                ExternalOpen = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                            {
                                SVTReservedInternal = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                            {
                                ExternalReserved = true;
                            }

                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                            {
                                service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                UpdateSeatData(seatsList);
                            }
                            else if (leaveStudent == false)
                            {

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                {
                                    service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                    UpdateSeatData(seatsList);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                    {
                                        service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                        UpdateSeatData(seatsList);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                        {
                                            service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                            UpdateSeatData(seatsList);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                            {
                                                service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                UpdateSeatData(seatsList);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                {
                                                    service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                    UpdateSeatData(seatsList);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                    {
                                                        service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                        UpdateSeatData(seatsList);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        UpdateSeatData(seatsList);
                        UpdateReservedQuotaSeats(reservedSeats);
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
                #endregion

                return "Merit List for Round " + round + " generated successfully";
            }
            else
            {
                return "Please check json file data";
            }

        }

        // Preview list for round 1 & round 4
        public List<PreviewStudentDetails> GetRound1PreviewList(bool isSVT, string category, string round, bool generate = false)
        {
            var roundlist = new List<RoundList>();
            string returnMessage = string.Empty;
            var seatsList = ReadSeatsFile();
            var reservedSeats = GetReservationSeat();
            List<PreviewStudentDetails> listOfStudents = new List<PreviewStudentDetails>();

            roundlist = ReadRoundFile(round);
            if (seatsList != null && seatsList.Count > 1 && roundlist != null && roundlist.Count > 1)
            {
                int sr = 0;

                isSVT = true;
                category = "OPEN";
                try
                {
                    //Get Student List
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentPreviewList(isSVT);
                        bool SVTOpenInternal = true;
                        bool SVTReservedInternal = false;
                        bool ExternalOpen = false;
                        bool ExternalReserved = false;
                        bool leaveStudent = false;
                        string lastAdmittedCourse = string.Empty;

                        #region Internal Open / Reserved - Round 1
                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"
                            bool isAdmittedinCurrentRound = false;
                            lastAdmittedCourse = string.Empty;
                            leaveStudent = false;
                            var stt = listOfStudents.FirstOrDefault(s => s.Id == student.Id);
                            if (stt != null)
                            {
                                isAdmittedinCurrentRound = true;
                                lastAdmittedCourse = stt.PossibleCourseAdmitted;
                            }
                            else if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion
                            #region Round 1 - Internal Open Category (Reserved Internal included)
                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                            {
                                if (generate)
                                {
                                    service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                    UpdateSeatData(seatsList);
                                }

                                if (listOfStudents.Count > 0)
                                    sr = listOfStudents.Last().SerialNumber;
                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                st.SerialNumber = sr + 1;
                                st.PossibleCourseAdmitted = student.CoursePreference1;
                                listOfStudents.Add(st);

                            }
                            else if (leaveStudent == false && student.IsSVTStudent == true && student.Category.ToUpper() == "OPEN")
                            {
                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                {
                                    if (generate)
                                    {
                                        service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                        UpdateSeatData(seatsList);
                                    }

                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference2;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                    {
                                        if (generate)
                                        {
                                            service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                            UpdateSeatData(seatsList);
                                        }

                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference3;
                                        listOfStudents.Add(st);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                        {
                                            if (generate)
                                            {
                                                service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                                UpdateSeatData(seatsList);
                                            }

                                            if (listOfStudents.Count > 0)
                                                sr = listOfStudents.Last().SerialNumber;
                                            PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                            st.SerialNumber = sr + 1;
                                            st.PossibleCourseAdmitted = student.CoursePreference4;
                                            listOfStudents.Add(st);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                            {
                                                if (generate)
                                                {
                                                    service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                    UpdateSeatData(seatsList);
                                                }

                                                if (listOfStudents.Count > 0)
                                                    sr = listOfStudents.Last().SerialNumber;
                                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                st.SerialNumber = sr + 1;
                                                st.PossibleCourseAdmitted = student.CoursePreference5;
                                                listOfStudents.Add(st);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                {
                                                    if (generate)
                                                    {
                                                        service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                        UpdateSeatData(seatsList);
                                                    }

                                                    if (listOfStudents.Count > 0)
                                                        sr = listOfStudents.Last().SerialNumber;
                                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                    st.SerialNumber = sr + 1;
                                                    st.PossibleCourseAdmitted = student.CoursePreference6;
                                                    listOfStudents.Add(st);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                    {
                                                        if (generate)
                                                        {
                                                            service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                            UpdateSeatData(seatsList);
                                                        }

                                                        if (listOfStudents.Count > 0)
                                                            sr = listOfStudents.Last().SerialNumber;
                                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                        st.SerialNumber = sr + 1;
                                                        st.PossibleCourseAdmitted = student.CoursePreference7;
                                                        listOfStudents.Add(st);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        seatsList.Where(w => w.SVTOpenInternal > 0).ToList().ForEach(i => { i.ExternalOpen = i.ExternalOpen + i.SVTOpenInternal; i.SVTOpenInternal = 0; });
                        #endregion

                        isSVT = false;
                        SVTOpenInternal = false;
                        ExternalOpen = true;
                        leaveStudent = false;
                        lastAdmittedCourse = string.Empty;

                        #region External Open / Reserved - Round 1
                        studenList = service.GetStudentPreviewList(isSVT);

                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"
                            bool isAdmittedinCurrentRound = false;
                            lastAdmittedCourse = string.Empty;
                            leaveStudent = false;

                            var stt = listOfStudents.FirstOrDefault(s => s.Id == student.Id);
                            if (stt != null)
                            {
                                isAdmittedinCurrentRound = true;
                                lastAdmittedCourse = stt.PossibleCourseAdmitted;
                            }
                            else if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion
                            #region Round 1 - External Open Category (Reserved External included)
                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                            {
                                if (generate)
                                {
                                    service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                    UpdateSeatData(seatsList);
                                }

                                if (listOfStudents.Count > 0)
                                    sr = listOfStudents.Last().SerialNumber;
                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                st.SerialNumber = sr + 1;
                                st.PossibleCourseAdmitted = student.CoursePreference1;
                                listOfStudents.Add(st);

                            }
                            else if (leaveStudent == false && student.IsSVTStudent == false && student.Category.ToUpper() == "OPEN")
                            {
                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                {
                                    if (generate)
                                    {
                                        service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                        UpdateSeatData(seatsList);
                                    }

                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference2;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                    {
                                        if (generate)
                                        {
                                            service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                            UpdateSeatData(seatsList);
                                        }

                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference3;
                                        listOfStudents.Add(st);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                        {
                                            if (generate)
                                            {
                                                service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                                UpdateSeatData(seatsList);
                                            }

                                            if (listOfStudents.Count > 0)
                                                sr = listOfStudents.Last().SerialNumber;
                                            PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                            st.SerialNumber = sr + 1;
                                            st.PossibleCourseAdmitted = student.CoursePreference4;
                                            listOfStudents.Add(st);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                            {
                                                if (generate)
                                                {
                                                    service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                    UpdateSeatData(seatsList);
                                                }

                                                if (listOfStudents.Count > 0)
                                                    sr = listOfStudents.Last().SerialNumber;
                                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                st.SerialNumber = sr + 1;
                                                st.PossibleCourseAdmitted = student.CoursePreference5;
                                                listOfStudents.Add(st);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                {
                                                    if (generate)
                                                    {
                                                        service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                        UpdateSeatData(seatsList);
                                                    }
                                                    if (listOfStudents.Count > 0)
                                                        sr = listOfStudents.Last().SerialNumber;
                                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                    st.SerialNumber = sr + 1;
                                                    st.PossibleCourseAdmitted = student.CoursePreference6;
                                                    listOfStudents.Add(st);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                    {
                                                        if (generate)
                                                        {
                                                            service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                            UpdateSeatData(seatsList);
                                                        }

                                                        if (listOfStudents.Count > 0)
                                                            sr = listOfStudents.Last().SerialNumber;
                                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                        st.SerialNumber = sr + 1;
                                                        st.PossibleCourseAdmitted = student.CoursePreference7;
                                                        listOfStudents.Add(st);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        seatsList.Where(w => w.ExternalOpen > 0).ToList().ForEach(i => { i.SVTReservedInternal = i.ExternalOpen + i.SVTReservedInternal; i.ExternalOpen = 0; });
                        #endregion

                        isSVT = true;
                        category = "RESERVED";
                        SVTOpenInternal = false;
                        SVTReservedInternal = true;
                        ExternalOpen = false;
                        leaveStudent = false;
                        lastAdmittedCourse = string.Empty;

                        #region Internal Reserved - Round 1
                        studenList = service.GetStudentList(isSVT, category);
                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"
                            bool isAdmittedinCurrentRound = false;
                            lastAdmittedCourse = string.Empty;
                            leaveStudent = false;
                            var stt = listOfStudents.FirstOrDefault(s => s.Id == student.Id);
                            if (stt != null)
                            {
                                isAdmittedinCurrentRound = true;
                                lastAdmittedCourse = stt.PossibleCourseAdmitted;
                            }
                            else if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"
                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                            {
                                if (generate)
                                {
                                    service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                    UpdateSeatData(seatsList);
                                }

                                if (listOfStudents.Count > 0)
                                    sr = listOfStudents.Last().SerialNumber;

                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                st.SerialNumber = sr + 1;
                                st.PossibleCourseAdmitted = student.CoursePreference1;
                                listOfStudents.Add(st);
                            }
                            else if (leaveStudent == false)
                            {
                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                {
                                    if (generate)
                                    {
                                        service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                        UpdateSeatData(seatsList);
                                    }

                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference2;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                    {
                                        if (generate)
                                        {
                                            service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                            UpdateSeatData(seatsList);
                                        }
                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference3;
                                        listOfStudents.Add(st);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                        {
                                            if (generate)
                                            {
                                                service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                                UpdateSeatData(seatsList);
                                            }

                                            if (listOfStudents.Count > 0)
                                                sr = listOfStudents.Last().SerialNumber;
                                            PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                            st.SerialNumber = sr + 1;
                                            st.PossibleCourseAdmitted = student.CoursePreference4;
                                            listOfStudents.Add(st);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                            {
                                                if (generate)
                                                {
                                                    service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                    UpdateSeatData(seatsList);
                                                }

                                                if (listOfStudents.Count > 0)
                                                    sr = listOfStudents.Last().SerialNumber;
                                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                st.SerialNumber = sr + 1;
                                                st.PossibleCourseAdmitted = student.CoursePreference5;
                                                listOfStudents.Add(st);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                {
                                                    if (generate)
                                                    {
                                                        service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                        UpdateSeatData(seatsList);
                                                    }

                                                    if (listOfStudents.Count > 0)
                                                        sr = listOfStudents.Last().SerialNumber;
                                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                    st.SerialNumber = sr + 1;
                                                    st.PossibleCourseAdmitted = student.CoursePreference6;
                                                    listOfStudents.Add(st);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                    {
                                                        if (generate)
                                                        {
                                                            service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                            UpdateSeatData(seatsList);
                                                        }

                                                        if (listOfStudents.Count > 0)
                                                            sr = listOfStudents.Last().SerialNumber;
                                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                        st.SerialNumber = sr + 1;
                                                        st.PossibleCourseAdmitted = student.CoursePreference7;
                                                        listOfStudents.Add(st);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        returnMessage = string.Empty;
                        seatsList.Where(w => w.SVTReservedInternal > 0).ToList().ForEach(i => { i.ExternalReserved = i.ExternalReserved + i.SVTReservedInternal; i.SVTReservedInternal = 0; });
                        AdjustSeats(reservedSeats, "internal");
                        #endregion

                        isSVT = false;
                        category = "RESERVED";
                        SVTOpenInternal = false;
                        SVTReservedInternal = false;
                        ExternalOpen = false;
                        ExternalReserved = true;
                        leaveStudent = false;
                        lastAdmittedCourse = string.Empty;

                        #region External Reserved - Round 1
                        studenList = service.GetStudentList(isSVT, category);
                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"

                            bool isAdmittedinCurrentRound = false;
                            lastAdmittedCourse = string.Empty;
                            leaveStudent = false;
                            var stt = listOfStudents.FirstOrDefault(s => s.Id == student.Id);
                            if (stt != null)
                            {
                                isAdmittedinCurrentRound = true;
                                lastAdmittedCourse = stt.PossibleCourseAdmitted;
                            }
                            else if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"
                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                            {
                                if (generate)
                                {
                                    service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                    UpdateSeatData(seatsList);
                                }

                                if (listOfStudents.Count > 0)
                                    sr = listOfStudents.Last().SerialNumber;
                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                st.SerialNumber = sr + 1;
                                st.PossibleCourseAdmitted = student.CoursePreference1;
                                listOfStudents.Add(st);
                            }
                            else if (leaveStudent == false)
                            {
                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                {
                                    if (generate)
                                    {
                                        service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                        UpdateSeatData(seatsList);
                                    }

                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference2;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                    {
                                        if (generate)
                                        {
                                            service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                            UpdateSeatData(seatsList);
                                        }

                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference3;
                                        listOfStudents.Add(st);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                        {
                                            if (generate)
                                            {
                                                service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                                UpdateSeatData(seatsList);
                                            }


                                            if (listOfStudents.Count > 0)
                                                sr = listOfStudents.Last().SerialNumber;
                                            PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                            st.SerialNumber = sr + 1;
                                            st.PossibleCourseAdmitted = student.CoursePreference4;
                                            listOfStudents.Add(st);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                            {
                                                if (generate)
                                                {
                                                    service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                    UpdateSeatData(seatsList);
                                                }
                                                if (listOfStudents.Count > 0)
                                                    sr = listOfStudents.Last().SerialNumber;
                                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                st.SerialNumber = sr + 1;
                                                st.PossibleCourseAdmitted = student.CoursePreference5;
                                                listOfStudents.Add(st);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                {
                                                    if (generate)
                                                    {
                                                        service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                        UpdateSeatData(seatsList);
                                                    }

                                                    if (listOfStudents.Count > 0)
                                                        sr = listOfStudents.Last().SerialNumber;
                                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                    st.SerialNumber = sr + 1;
                                                    st.PossibleCourseAdmitted = student.CoursePreference6;
                                                    listOfStudents.Add(st);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                    {
                                                        if (generate)
                                                        {
                                                            service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                            UpdateSeatData(seatsList);
                                                        }
                                                        if (listOfStudents.Count > 0)
                                                            sr = listOfStudents.Last().SerialNumber;
                                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                        st.SerialNumber = sr + 1;
                                                        st.PossibleCourseAdmitted = student.CoursePreference7;
                                                        listOfStudents.Add(st);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        #endregion

                        if (generate)
                        {
                            UpdateSeatData(seatsList);
                            UpdateReservedQuotaSeats(reservedSeats);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return listOfStudents;
        }

        public List<PreviewStudentDetails> GetPreviewList(bool isSVT, string category, string round)
        {
            if (round == "Round1")
            {
                return GetRound1PreviewList(isSVT, category, round);
            }

            var roundlist = new List<RoundList>();
            //Get Seats json list item
            var seatsList = ReadSeatsFile();
            var reservedSeats = GetReservationSeat();
            //Get Round json list item
            roundlist = ReadRoundFile(round);
            if (seatsList != null && seatsList.Count > 1 && roundlist != null && roundlist.Count > 1)
            {
                List<PreviewStudentDetails> listOfStudents = new List<PreviewStudentDetails>();
                int sr = 0;

                #region Internal Reserved
                isSVT = true;
                category = "RESERVED";
                try
                {
                    //Get Student List
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);

                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"

                            bool isAdmittedinCurrentRound = false;
                            string lastAdmittedCourse = string.Empty;
                            bool leaveStudent = false;

                            if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"

                            bool SVTOpenInternal = false;
                            bool SVTReservedInternal = false;
                            bool ExternalOpen = false;
                            bool ExternalReserved = false;
                            //Get Categoy and SVT student wise but value
                            if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                            {
                                SVTOpenInternal = true;
                            }
                            else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                            {
                                ExternalOpen = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                            {
                                SVTReservedInternal = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                            {
                                ExternalReserved = true;
                            }


                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                            {
                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                //UpdateSeatData(seatsList);
                                if (listOfStudents.Count > 0)
                                    sr = listOfStudents.Last().SerialNumber;
                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                st.SerialNumber = sr + 1;
                                st.PossibleCourseAdmitted = student.CoursePreference1;
                                listOfStudents.Add(st);

                            }
                            else if (leaveStudent == false)
                            {

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                {
                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                    //UpdateSeatData(seatsList);
                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference2;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                    {
                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                        //UpdateSeatData(seatsList);
                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference3;
                                        listOfStudents.Add(st);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                        {
                                            //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                            //UpdateSeatData(seatsList);
                                            if (listOfStudents.Count > 0)
                                                sr = listOfStudents.Last().SerialNumber;
                                            PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                            st.SerialNumber = sr + 1;
                                            st.PossibleCourseAdmitted = student.CoursePreference4;
                                            listOfStudents.Add(st);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                            {
                                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                //UpdateSeatData(seatsList);
                                                if (listOfStudents.Count > 0)
                                                    sr = listOfStudents.Last().SerialNumber;
                                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                st.SerialNumber = sr + 1;
                                                st.PossibleCourseAdmitted = student.CoursePreference5;
                                                listOfStudents.Add(st);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                {
                                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                    //UpdateSeatData(seatsList);
                                                    if (listOfStudents.Count > 0)
                                                        sr = listOfStudents.Last().SerialNumber;
                                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                    st.SerialNumber = sr + 1;
                                                    st.PossibleCourseAdmitted = student.CoursePreference6;
                                                    listOfStudents.Add(st);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                    {
                                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                        //UpdateSeatData(seatsList);
                                                        if (listOfStudents.Count > 0)
                                                            sr = listOfStudents.Last().SerialNumber;
                                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                        st.SerialNumber = sr + 1;
                                                        st.PossibleCourseAdmitted = student.CoursePreference7;
                                                        listOfStudents.Add(st);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        string returnMessage = string.Empty;
                        #region "Transfer Seat"

                        if (category.ToUpper() == "OPEN" && isSVT == true)
                        {
                            //Convert to External Open 
                            seatsList.Where(w => w.SVTOpenInternal > 0).ToList().ForEach(i => { i.ExternalOpen = i.ExternalOpen + i.SVTOpenInternal; i.SVTOpenInternal = 0; });
                            returnMessage = "SVT Internal Open category " + round + " allocation completed successfully.";
                        }
                        else if (category.ToUpper() == "OPEN" && isSVT == false)
                        {
                            //Convert to External Open 
                            if (round.ToUpper() == "ROUND1")
                            {
                                seatsList.Where(w => w.ExternalOpen > 0).ToList().ForEach(i => { i.SVTReservedInternal = i.ExternalOpen + i.SVTReservedInternal; i.ExternalOpen = 0; });
                            }

                            returnMessage = "SVT External OPEN category " + round + " allocation completed successfully.";
                        }
                        //Get Categoy and SVT student wise value
                        else if (category.ToUpper() == "RESERVED" && isSVT == true)
                        {
                            //Convert to External Reserved   
                            seatsList.Where(w => w.SVTReservedInternal > 0).ToList().ForEach(i => { i.ExternalReserved = i.ExternalReserved + i.SVTReservedInternal; i.SVTReservedInternal = 0; });
                            AdjustSeats(reservedSeats, "internal");
                            returnMessage = "SVT Internal Reserved category " + round + " allocation completed successfully.";
                        }
                        else
                        {
                            if (round.ToUpper() == "ROUND2" || round.ToUpper() == "ROUND3")
                            {
                                seatsList.Where(w => w.ExternalReserved > 0).ToList().ForEach(i => { i.SVTOpenInternal = i.SVTOpenInternal + i.ExternalReserved; i.ExternalReserved = 0; });
                            }

                            returnMessage = "SVT External Reserved category " + round + " allocation completed successfully.";
                        }
                        //UpdateSeatData(seatsList);

                        #endregion

                        //return Request.CreateResponse(HttpStatusCode.OK, new MeritStudentDetails() { IsSuccess = true, SuccessMessage = "Success", StudentList = listOfStudents });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion

                #region External Reserved
                isSVT = false;
                category = "RESERVED";
                try
                {
                    //Get Student List
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);

                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"

                            bool isAdmittedinCurrentRound = false;
                            string lastAdmittedCourse = string.Empty;
                            bool leaveStudent = false;

                            if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"

                            bool SVTOpenInternal = false;
                            bool SVTReservedInternal = false;
                            bool ExternalOpen = false;
                            bool ExternalReserved = false;
                            //Get Categoy and SVT student wise but value
                            if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                            {
                                SVTOpenInternal = true;
                            }
                            else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                            {
                                ExternalOpen = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                            {
                                SVTReservedInternal = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                            {
                                ExternalReserved = true;
                            }


                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                            {
                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                //UpdateSeatData(seatsList);
                                if (listOfStudents.Count > 0)
                                    sr = listOfStudents.Last().SerialNumber;
                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                st.SerialNumber = sr + 1;
                                st.PossibleCourseAdmitted = student.CoursePreference1;
                                listOfStudents.Add(st);

                            }
                            else if (leaveStudent == false)
                            {

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                {
                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                    //UpdateSeatData(seatsList);
                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference2;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                    {
                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                        //UpdateSeatData(seatsList);
                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference3;
                                        listOfStudents.Add(st);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                        {
                                            //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                            //UpdateSeatData(seatsList);
                                            if (listOfStudents.Count > 0)
                                                sr = listOfStudents.Last().SerialNumber;
                                            PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                            st.SerialNumber = sr + 1;
                                            st.PossibleCourseAdmitted = student.CoursePreference4;
                                            listOfStudents.Add(st);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                            {
                                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                //UpdateSeatData(seatsList);
                                                if (listOfStudents.Count > 0)
                                                    sr = listOfStudents.Last().SerialNumber;
                                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                st.SerialNumber = sr + 1;
                                                st.PossibleCourseAdmitted = student.CoursePreference5;
                                                listOfStudents.Add(st);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                {
                                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                    //UpdateSeatData(seatsList);
                                                    if (listOfStudents.Count > 0)
                                                        sr = listOfStudents.Last().SerialNumber;
                                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                    st.SerialNumber = sr + 1;
                                                    st.PossibleCourseAdmitted = student.CoursePreference6;
                                                    listOfStudents.Add(st);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                    {
                                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                        //UpdateSeatData(seatsList);
                                                        if (listOfStudents.Count > 0)
                                                            sr = listOfStudents.Last().SerialNumber;
                                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                        st.SerialNumber = sr + 1;
                                                        st.PossibleCourseAdmitted = student.CoursePreference7;
                                                        listOfStudents.Add(st);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        string returnMessage = string.Empty;
                        #region "Transfer Seat"

                        if (category.ToUpper() == "OPEN" && isSVT == true)
                        {
                            //Convert to External Open 
                            seatsList.Where(w => w.SVTOpenInternal > 0).ToList().ForEach(i => { i.ExternalOpen = i.ExternalOpen + i.SVTOpenInternal; i.SVTOpenInternal = 0; });

                            returnMessage = "SVT Internal Open category " + round + " allocation completed successfully.";
                        }
                        else if (category.ToUpper() == "OPEN" && isSVT == false)
                        {
                            //Convert to External Open 
                            if (round.ToUpper() == "ROUND1")
                            {
                                seatsList.Where(w => w.ExternalOpen > 0).ToList().ForEach(i => { i.SVTReservedInternal = i.ExternalOpen + i.SVTReservedInternal; i.ExternalOpen = 0; });
                            }
                            returnMessage = "SVT External Reserved category " + round + " allocation completed successfully.";
                        }
                        //Get Categoy and SVT student wise value
                        else if (category.ToUpper() == "RESERVED" && isSVT == true)
                        {
                            //Convert to External Reserved   
                            seatsList.Where(w => w.SVTReservedInternal > 0).ToList().ForEach(i => { i.ExternalReserved = i.ExternalReserved + i.SVTReservedInternal; i.SVTReservedInternal = 0; });

                            returnMessage = "SVT Internal Reserved category " + round + " allocation completed successfully.";
                        }
                        else
                        {
                            if (round.ToUpper() == "ROUND2" || round.ToUpper() == "ROUND3")
                            {
                                seatsList.Where(w => w.ExternalReserved > 0).ToList().ForEach(i => { i.SVTOpenInternal = i.SVTOpenInternal + i.ExternalReserved; i.ExternalReserved = 0; });
                            }
                            returnMessage = "SVT External Reserved category " + round + " allocation completed successfully.";
                        }
                        //UpdateSeatData(seatsList);

                        #endregion
                        //return listOfStudents;
                        //
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion

                #region Internal Open
                isSVT = true;
                category = "OPEN";
                try
                {
                    //Get Student List
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);

                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"

                            bool isAdmittedinCurrentRound = false;
                            string lastAdmittedCourse = string.Empty;
                            bool leaveStudent = false;

                            if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"

                            bool SVTOpenInternal = false;
                            bool SVTReservedInternal = false;
                            bool ExternalOpen = false;
                            bool ExternalReserved = false;
                            //Get Categoy and SVT student wise but value


                            if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                            {
                                SVTOpenInternal = true;
                            }
                            else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                            {
                                ExternalOpen = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                            {
                                SVTReservedInternal = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                            {
                                ExternalReserved = true;
                            }


                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                            {
                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                //UpdateSeatData(seatsList);
                                if (listOfStudents.Count > 0)
                                    sr = listOfStudents.Last().SerialNumber;
                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                st.SerialNumber = sr + 1;
                                st.PossibleCourseAdmitted = student.CoursePreference1;
                                listOfStudents.Add(st);

                            }
                            else if (leaveStudent == false)
                            {

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                {
                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                    //UpdateSeatData(seatsList);
                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference2;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                    {
                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                        //UpdateSeatData(seatsList);
                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference3;
                                        listOfStudents.Add(st);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                        {
                                            //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                            //UpdateSeatData(seatsList);
                                            if (listOfStudents.Count > 0)
                                                sr = listOfStudents.Last().SerialNumber;
                                            PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                            st.SerialNumber = sr + 1;
                                            st.PossibleCourseAdmitted = student.CoursePreference4;
                                            listOfStudents.Add(st);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                            {
                                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                //UpdateSeatData(seatsList);
                                                if (listOfStudents.Count > 0)
                                                    sr = listOfStudents.Last().SerialNumber;
                                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                st.SerialNumber = sr + 1;
                                                st.PossibleCourseAdmitted = student.CoursePreference5;
                                                listOfStudents.Add(st);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                {
                                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                    //UpdateSeatData(seatsList);
                                                    if (listOfStudents.Count > 0)
                                                        sr = listOfStudents.Last().SerialNumber;
                                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                    st.SerialNumber = sr + 1;
                                                    st.PossibleCourseAdmitted = student.CoursePreference6;
                                                    listOfStudents.Add(st);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                    {
                                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                        //UpdateSeatData(seatsList);
                                                        if (listOfStudents.Count > 0)
                                                            sr = listOfStudents.Last().SerialNumber;
                                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                        st.SerialNumber = sr + 1;
                                                        st.PossibleCourseAdmitted = student.CoursePreference7;
                                                        listOfStudents.Add(st);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        string returnMessage = string.Empty;
                        #region "Transfer Seat"

                        if (category.ToUpper() == "OPEN" && isSVT == true)
                        {
                            //Convert to External Open 
                            seatsList.Where(w => w.SVTOpenInternal > 0).ToList().ForEach(i => { i.ExternalOpen = i.ExternalOpen + i.SVTOpenInternal; i.SVTOpenInternal = 0; });

                            returnMessage = "SVT Internal Open category " + round + " allocation completed successfully.";
                        }
                        else if (category.ToUpper() == "OPEN" && isSVT == false)
                        {
                            //Convert to External Open 
                            if (round.ToUpper() == "ROUND1")
                            {
                                seatsList.Where(w => w.ExternalOpen > 0).ToList().ForEach(i => { i.SVTReservedInternal = i.ExternalOpen + i.SVTReservedInternal; i.ExternalOpen = 0; });
                            }
                            returnMessage = "SVT External Reserved category " + round + " allocation completed successfully.";
                        }
                        //Get Categoy and SVT student wise value
                        else if (category.ToUpper() == "RESERVED" && isSVT == true)
                        {
                            //Convert to External Reserved   
                            seatsList.Where(w => w.SVTReservedInternal > 0).ToList().ForEach(i => { i.ExternalReserved = i.ExternalReserved + i.SVTReservedInternal; i.SVTReservedInternal = 0; });

                            returnMessage = "SVT Internal Reserved category " + round + " allocation completed successfully.";
                        }
                        else
                        {
                            if (round.ToUpper() == "ROUND2" || round.ToUpper() == "ROUND3")
                            {
                                seatsList.Where(w => w.ExternalReserved > 0).ToList().ForEach(i => { i.SVTOpenInternal = i.SVTOpenInternal + i.ExternalReserved; i.ExternalReserved = 0; });
                            }
                            returnMessage = "SVT External Reserved category " + round + " allocation completed successfully.";
                        }
                        //UpdateSeatData(seatsList);

                        #endregion

                        //return Request.CreateResponse(HttpStatusCode.OK, new MeritStudentDetails() { IsSuccess = true, SuccessMessage = "Success", StudentList = listOfStudents });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion

                #region External Open
                isSVT = false;
                category = "OPEN";
                try
                {
                    //List<PreviewStudentDetails> listOfStudents = new List<PreviewStudentDetails>();


                    //Get Student List
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);

                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"

                            bool isAdmittedinCurrentRound = false;
                            string lastAdmittedCourse = string.Empty;
                            bool leaveStudent = false;

                            if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"

                            bool SVTOpenInternal = false;
                            bool SVTReservedInternal = false;
                            bool ExternalOpen = false;
                            bool ExternalReserved = false;
                            //Get Categoy and SVT student wise but value
                            if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                            {
                                SVTOpenInternal = true;
                            }
                            else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                            {
                                ExternalOpen = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                            {
                                SVTReservedInternal = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                            {
                                ExternalReserved = true;
                            }


                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                            {
                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                //UpdateSeatData(seatsList);
                                if (listOfStudents.Count > 0)
                                    sr = listOfStudents.Last().SerialNumber;
                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                st.SerialNumber = sr + 1;
                                st.PossibleCourseAdmitted = student.CoursePreference1;
                                listOfStudents.Add(st);

                            }
                            else if (leaveStudent == false)
                            {

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                {
                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                    //UpdateSeatData(seatsList);
                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference2;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                    {
                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                        //UpdateSeatData(seatsList);
                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference3;
                                        listOfStudents.Add(st);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                        {
                                            //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                            //UpdateSeatData(seatsList);
                                            if (listOfStudents.Count > 0)
                                                sr = listOfStudents.Last().SerialNumber;
                                            PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                            st.SerialNumber = sr + 1;
                                            st.PossibleCourseAdmitted = student.CoursePreference4;
                                            listOfStudents.Add(st);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                            {
                                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                //UpdateSeatData(seatsList);
                                                if (listOfStudents.Count > 0)
                                                    sr = listOfStudents.Last().SerialNumber;
                                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                st.SerialNumber = sr + 1;
                                                st.PossibleCourseAdmitted = student.CoursePreference5;
                                                listOfStudents.Add(st);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                {
                                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                    //UpdateSeatData(seatsList);
                                                    if (listOfStudents.Count > 0)
                                                        sr = listOfStudents.Last().SerialNumber;
                                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                    st.SerialNumber = sr + 1;
                                                    st.PossibleCourseAdmitted = student.CoursePreference6;
                                                    listOfStudents.Add(st);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                    {
                                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                        //UpdateSeatData(seatsList);
                                                        if (listOfStudents.Count > 0)
                                                            sr = listOfStudents.Last().SerialNumber;
                                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                        st.SerialNumber = sr + 1;
                                                        st.PossibleCourseAdmitted = student.CoursePreference7;
                                                        listOfStudents.Add(st);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        string returnMessage = string.Empty;
                        #region "Transfer Seat"

                        if (category.ToUpper() == "OPEN" && isSVT == true)
                        {
                            //Convert to External Open 
                            seatsList.Where(w => w.SVTOpenInternal > 0).ToList().ForEach(i => { i.ExternalOpen = i.ExternalOpen + i.SVTOpenInternal; i.SVTOpenInternal = 0; });

                            returnMessage = "SVT Internal Open category " + round + " allocation completed successfully.";
                        }
                        else if (category.ToUpper() == "OPEN" && isSVT == false)
                        {
                            //Convert to External Open 
                            if (round.ToUpper() == "ROUND1")
                            {
                                seatsList.Where(w => w.ExternalOpen > 0).ToList().ForEach(i => { i.SVTReservedInternal = i.ExternalOpen + i.SVTReservedInternal; i.ExternalOpen = 0; });
                            }

                            returnMessage = "SVT External Reserved category " + round + " allocation completed successfully.";
                        }
                        //Get Categoy and SVT student wise value
                        else if (category.ToUpper() == "RESERVED" && isSVT == true)
                        {
                            //Convert to External Reserved   
                            seatsList.Where(w => w.SVTReservedInternal > 0).ToList().ForEach(i => { i.ExternalReserved = i.ExternalReserved + i.SVTReservedInternal; i.SVTReservedInternal = 0; });

                            returnMessage = "SVT Internal Reserved category " + round + " allocation completed successfully.";
                        }
                        else
                        {
                            if (round.ToUpper() == "ROUND2" || round.ToUpper() == "ROUND3")
                            {
                                seatsList.Where(w => w.ExternalReserved > 0).ToList().ForEach(i => { i.SVTOpenInternal = i.SVTOpenInternal + i.ExternalReserved; i.ExternalReserved = 0; });
                            }
                            returnMessage = "SVT External Reserved category " + round + " allocation completed successfully.";
                        }
                        //UpdateSeatData(seatsList);

                        #endregion
                        return listOfStudents;
                        //return Request.CreateResponse(HttpStatusCode.OK, new MeritStudentDetails() { IsSuccess = true, SuccessMessage = "Success", StudentList = listOfStudents });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion
            }

            return null;
        }

        #region Commented Round 1 - Merit List Preview
        /*
         * public List<PreviewStudentDetails> GetPreviewList(bool isSVT, string category, string round)
        {
            if(round == "Round1")
            {
                return GetRound1PreviewList(isSVT, category, round);
            }
            
            var roundlist = new List<RoundList>();
            //Get Seats json list item
            var seatsList = ReadSeatsFile();
            var reservedSeats = GetReservationSeat();
            //Get Round json list item
            roundlist = ReadRoundFile(round);
            if (seatsList != null && seatsList.Count > 1 && roundlist != null && roundlist.Count > 1)
            {
                List<PreviewStudentDetails> listOfStudents = new List<PreviewStudentDetails>();
                int sr = 0;

                #region Internal Open
                isSVT = true;
                category = "OPEN";
                try
                {
                    //Get Student List
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);

                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"

                            bool isAdmittedinCurrentRound = false;
                            string lastAdmittedCourse = string.Empty;
                            bool leaveStudent = false;

                            if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"

                            bool SVTOpenInternal = false;
                            bool SVTReservedInternal = false;
                            bool ExternalOpen = false;
                            bool ExternalReserved = false;
                            //Get Categoy and SVT student wise but value


                            if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                            {
                                SVTOpenInternal = true;
                            }
                            else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                            {
                                ExternalOpen = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                            {
                                SVTReservedInternal = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                            {
                                ExternalReserved = true;
                            }


                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                            {
                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                //UpdateSeatData(seatsList);
                                if (listOfStudents.Count > 0)
                                    sr = listOfStudents.Last().SerialNumber;
                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                st.SerialNumber = sr + 1;
                                st.PossibleCourseAdmitted = student.CoursePreference1;
                                listOfStudents.Add(st);

                            }
                            else if (leaveStudent == false)
                            {

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                {
                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                    //UpdateSeatData(seatsList);
                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference2;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                    {
                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                        //UpdateSeatData(seatsList);
                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference3;
                                        listOfStudents.Add(st);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                        {
                                            //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                            //UpdateSeatData(seatsList);
                                            if (listOfStudents.Count > 0)
                                                sr = listOfStudents.Last().SerialNumber;
                                            PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                            st.SerialNumber = sr + 1;
                                            st.PossibleCourseAdmitted = student.CoursePreference4;
                                            listOfStudents.Add(st);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                            {
                                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                //UpdateSeatData(seatsList);
                                                if (listOfStudents.Count > 0)
                                                    sr = listOfStudents.Last().SerialNumber;
                                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                st.SerialNumber = sr + 1;
                                                st.PossibleCourseAdmitted = student.CoursePreference5;
                                                listOfStudents.Add(st);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                {
                                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                    //UpdateSeatData(seatsList);
                                                    if (listOfStudents.Count > 0)
                                                        sr = listOfStudents.Last().SerialNumber;
                                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                    st.SerialNumber = sr + 1;
                                                    st.PossibleCourseAdmitted = student.CoursePreference6;
                                                    listOfStudents.Add(st);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                    {
                                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                        //UpdateSeatData(seatsList);
                                                        if (listOfStudents.Count > 0)
                                                            sr = listOfStudents.Last().SerialNumber;
                                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                        st.SerialNumber = sr + 1;
                                                        st.PossibleCourseAdmitted = student.CoursePreference7;
                                                        listOfStudents.Add(st);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        string returnMessage = string.Empty;
                        #region "Transfer Seat"

                        if (category.ToUpper() == "OPEN" && isSVT == true)
                        {
                            //Convert to External Open 
                            seatsList.Where(w => w.SVTOpenInternal > 0).ToList().ForEach(i => { i.ExternalOpen = i.ExternalOpen + i.SVTOpenInternal; i.SVTOpenInternal = 0; });

                            returnMessage = "SVT Internal Open category " + round + " allocation completed successfully.";
                        }
                        else if (category.ToUpper() == "OPEN" && isSVT == false)
                        {
                            //Convert to External Open 
                            seatsList.Where(w => w.ExternalOpen > 0).ToList().ForEach(i => { i.SVTReservedInternal = i.ExternalOpen + i.SVTReservedInternal; i.ExternalOpen = 0; });

                            returnMessage = "SVT External Reserved category " + round + " allocation completed successfully.";
                        }
                        //Get Categoy and SVT student wise value
                        else if (category.ToUpper() == "RESERVED" && isSVT == true)
                        {
                            //Convert to External Reserved   
                            seatsList.Where(w => w.SVTReservedInternal > 0).ToList().ForEach(i => { i.ExternalReserved = i.ExternalReserved + i.SVTReservedInternal; i.SVTReservedInternal = 0; });

                            returnMessage = "SVT Internal Reserved category " + round + " allocation completed successfully.";
                        }
                        else
                        {
                            //seatsList.Where(w => w.ExternalReserved > 0).ToList().ForEach(i => { i.SVTOpenInternal = i.SVTOpenInternal + i.ExternalReserved; i.ExternalReserved = 0; });
                            returnMessage = "SVT External Reserved category " + round + " allocation completed successfully.";
                        }
                        //UpdateSeatData(seatsList);

                        #endregion

                        //return Request.CreateResponse(HttpStatusCode.OK, new MeritStudentDetails() { IsSuccess = true, SuccessMessage = "Success", StudentList = listOfStudents });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion

                #region External Open
                isSVT = false;
                category = "OPEN";
                try
                {
                    //List<PreviewStudentDetails> listOfStudents = new List<PreviewStudentDetails>();


                    //Get Student List
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);

                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"

                            bool isAdmittedinCurrentRound = false;
                            string lastAdmittedCourse = string.Empty;
                            bool leaveStudent = false;

                            if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"

                            bool SVTOpenInternal = false;
                            bool SVTReservedInternal = false;
                            bool ExternalOpen = false;
                            bool ExternalReserved = false;
                            //Get Categoy and SVT student wise but value
                            if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                            {
                                SVTOpenInternal = true;
                            }
                            else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                            {
                                ExternalOpen = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                            {
                                SVTReservedInternal = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                            {
                                ExternalReserved = true;
                            }


                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                            {
                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                //UpdateSeatData(seatsList);
                                if (listOfStudents.Count > 0)
                                    sr = listOfStudents.Last().SerialNumber;
                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                st.SerialNumber = sr + 1;
                                st.PossibleCourseAdmitted = student.CoursePreference1;
                                listOfStudents.Add(st);

                            }
                            else if (leaveStudent == false)
                            {

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                {
                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                    //UpdateSeatData(seatsList);
                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference2;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                    {
                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                        //UpdateSeatData(seatsList);
                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference3;
                                        listOfStudents.Add(st);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                        {
                                            //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                            //UpdateSeatData(seatsList);
                                            if (listOfStudents.Count > 0)
                                                sr = listOfStudents.Last().SerialNumber;
                                            PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                            st.SerialNumber = sr + 1;
                                            st.PossibleCourseAdmitted = student.CoursePreference4;
                                            listOfStudents.Add(st);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                            {
                                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                //UpdateSeatData(seatsList);
                                                if (listOfStudents.Count > 0)
                                                    sr = listOfStudents.Last().SerialNumber;
                                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                st.SerialNumber = sr + 1;
                                                st.PossibleCourseAdmitted = student.CoursePreference5;
                                                listOfStudents.Add(st);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                {
                                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                    //UpdateSeatData(seatsList);
                                                    if (listOfStudents.Count > 0)
                                                        sr = listOfStudents.Last().SerialNumber;
                                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                    st.SerialNumber = sr + 1;
                                                    st.PossibleCourseAdmitted = student.CoursePreference6;
                                                    listOfStudents.Add(st);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                    {
                                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                        //UpdateSeatData(seatsList);
                                                        if (listOfStudents.Count > 0)
                                                            sr = listOfStudents.Last().SerialNumber;
                                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                        st.SerialNumber = sr + 1;
                                                        st.PossibleCourseAdmitted = student.CoursePreference7;
                                                        listOfStudents.Add(st);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        string returnMessage = string.Empty;
                        #region "Transfer Seat"

                        if (category.ToUpper() == "OPEN" && isSVT == true)
                        {
                            //Convert to External Open 
                            seatsList.Where(w => w.SVTOpenInternal > 0).ToList().ForEach(i => { i.ExternalOpen = i.ExternalOpen + i.SVTOpenInternal; i.SVTOpenInternal = 0; });

                            returnMessage = "SVT Internal Open category " + round + " allocation completed successfully.";
                        }
                        else if (category.ToUpper() == "OPEN" && isSVT == false)
                        {
                            //Convert to External Open 
                            seatsList.Where(w => w.ExternalOpen > 0).ToList().ForEach(i => { i.SVTReservedInternal = i.ExternalOpen + i.SVTReservedInternal; i.ExternalOpen = 0; });

                            returnMessage = "SVT External Reserved category " + round + " allocation completed successfully.";
                        }
                        //Get Categoy and SVT student wise value
                        else if (category.ToUpper() == "RESERVED" && isSVT == true)
                        {
                            //Convert to External Reserved   
                            seatsList.Where(w => w.SVTReservedInternal > 0).ToList().ForEach(i => { i.ExternalReserved = i.ExternalReserved + i.SVTReservedInternal; i.SVTReservedInternal = 0; });

                            returnMessage = "SVT Internal Reserved category " + round + " allocation completed successfully.";
                        }
                        else
                        {
                            //seatsList.Where(w => w.ExternalReserved > 0).ToList().ForEach(i => { i.SVTOpenInternal = i.SVTOpenInternal + i.ExternalReserved; i.ExternalReserved = 0; });
                            returnMessage = "SVT External Reserved category " + round + " allocation completed successfully.";
                        }
                        //UpdateSeatData(seatsList);

                        #endregion

                        //return Request.CreateResponse(HttpStatusCode.OK, new MeritStudentDetails() { IsSuccess = true, SuccessMessage = "Success", StudentList = listOfStudents });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion

                #region Internal Reserved
                isSVT = true;
                category = "RESERVED";
                try
                {
                    //Get Student List
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);

                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"

                            bool isAdmittedinCurrentRound = false;
                            string lastAdmittedCourse = string.Empty;
                            bool leaveStudent = false;

                            if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"

                            bool SVTOpenInternal = false;
                            bool SVTReservedInternal = false;
                            bool ExternalOpen = false;
                            bool ExternalReserved = false;
                            //Get Categoy and SVT student wise but value
                            if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                            {
                                SVTOpenInternal = true;
                            }
                            else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                            {
                                ExternalOpen = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                            {
                                SVTReservedInternal = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                            {
                                ExternalReserved = true;
                            }


                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                            {
                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                //UpdateSeatData(seatsList);
                                if (listOfStudents.Count > 0)
                                    sr = listOfStudents.Last().SerialNumber;
                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                st.SerialNumber = sr + 1;
                                st.PossibleCourseAdmitted = student.CoursePreference1;
                                listOfStudents.Add(st);

                            }
                            else if (leaveStudent == false)
                            {

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                {
                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                    //UpdateSeatData(seatsList);
                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference2;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                    {
                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                        //UpdateSeatData(seatsList);
                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference3;
                                        listOfStudents.Add(st);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                        {
                                            //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                            //UpdateSeatData(seatsList);
                                            if (listOfStudents.Count > 0)
                                                sr = listOfStudents.Last().SerialNumber;
                                            PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                            st.SerialNumber = sr + 1;
                                            st.PossibleCourseAdmitted = student.CoursePreference4;
                                            listOfStudents.Add(st);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                            {
                                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                //UpdateSeatData(seatsList);
                                                if (listOfStudents.Count > 0)
                                                    sr = listOfStudents.Last().SerialNumber;
                                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                st.SerialNumber = sr + 1;
                                                st.PossibleCourseAdmitted = student.CoursePreference5;
                                                listOfStudents.Add(st);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                {
                                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                    //UpdateSeatData(seatsList);
                                                    if (listOfStudents.Count > 0)
                                                        sr = listOfStudents.Last().SerialNumber;
                                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                    st.SerialNumber = sr + 1;
                                                    st.PossibleCourseAdmitted = student.CoursePreference6;
                                                    listOfStudents.Add(st);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                    {
                                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                        //UpdateSeatData(seatsList);
                                                        if (listOfStudents.Count > 0)
                                                            sr = listOfStudents.Last().SerialNumber;
                                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                        st.SerialNumber = sr + 1;
                                                        st.PossibleCourseAdmitted = student.CoursePreference7;
                                                        listOfStudents.Add(st);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        string returnMessage = string.Empty;
                        #region "Transfer Seat"

                        if (category.ToUpper() == "OPEN" && isSVT == true)
                        {
                            //Convert to External Open 
                            seatsList.Where(w => w.SVTOpenInternal > 0).ToList().ForEach(i => { i.ExternalOpen = i.ExternalOpen + i.SVTOpenInternal; i.SVTOpenInternal = 0; });
                            returnMessage = "SVT Internal Open category " + round + " allocation completed successfully.";
                        }
                        else if (category.ToUpper() == "OPEN" && isSVT == false)
                        {
                            //Convert to External Open 
                            seatsList.Where(w => w.ExternalOpen > 0).ToList().ForEach(i => { i.SVTReservedInternal = i.ExternalOpen + i.SVTReservedInternal; i.ExternalOpen = 0; });

                            returnMessage = "SVT External OPEN category " + round + " allocation completed successfully.";
                        }
                        //Get Categoy and SVT student wise value
                        else if (category.ToUpper() == "RESERVED" && isSVT == true)
                        {
                            //Convert to External Reserved   
                            seatsList.Where(w => w.SVTReservedInternal > 0).ToList().ForEach(i => { i.ExternalReserved = i.ExternalReserved + i.SVTReservedInternal; i.SVTReservedInternal = 0; });
                            AdjustSeats(reservedSeats, "internal");
                            returnMessage = "SVT Internal Reserved category " + round + " allocation completed successfully.";
                        }
                        else
                        {
                            //seatsList.Where(w => w.ExternalReserved > 0).ToList().ForEach(i => { i.SVTOpenInternal = i.SVTOpenInternal + i.ExternalReserved; i.ExternalReserved = 0; });
                            returnMessage = "SVT External Reserved category " + round + " allocation completed successfully.";
                        }
                        //UpdateSeatData(seatsList);

                        #endregion

                        //return Request.CreateResponse(HttpStatusCode.OK, new MeritStudentDetails() { IsSuccess = true, SuccessMessage = "Success", StudentList = listOfStudents });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion

                #region External Reserved
                isSVT = false;
                category = "RESERVED";
                try
                {
                    //Get Student List
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);

                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"

                            bool isAdmittedinCurrentRound = false;
                            string lastAdmittedCourse = string.Empty;
                            bool leaveStudent = false;

                            if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"

                            bool SVTOpenInternal = false;
                            bool SVTReservedInternal = false;
                            bool ExternalOpen = false;
                            bool ExternalReserved = false;
                            //Get Categoy and SVT student wise but value
                            if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                            {
                                SVTOpenInternal = true;
                            }
                            else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                            {
                                ExternalOpen = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                            {
                                SVTReservedInternal = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                            {
                                ExternalReserved = true;
                            }


                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                            {
                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                //UpdateSeatData(seatsList);
                                if (listOfStudents.Count > 0)
                                    sr = listOfStudents.Last().SerialNumber;
                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                st.SerialNumber = sr + 1;
                                st.PossibleCourseAdmitted = student.CoursePreference1;
                                listOfStudents.Add(st);

                            }
                            else if (leaveStudent == false)
                            {

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                {
                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                    //UpdateSeatData(seatsList);
                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference2;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                    {
                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                        //UpdateSeatData(seatsList);
                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference3;
                                        listOfStudents.Add(st);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                        {
                                            //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                            //UpdateSeatData(seatsList);
                                            if (listOfStudents.Count > 0)
                                                sr = listOfStudents.Last().SerialNumber;
                                            PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                            st.SerialNumber = sr + 1;
                                            st.PossibleCourseAdmitted = student.CoursePreference4;
                                            listOfStudents.Add(st);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                            {
                                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                //UpdateSeatData(seatsList);
                                                if (listOfStudents.Count > 0)
                                                    sr = listOfStudents.Last().SerialNumber;
                                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                st.SerialNumber = sr + 1;
                                                st.PossibleCourseAdmitted = student.CoursePreference5;
                                                listOfStudents.Add(st);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                {
                                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                    //UpdateSeatData(seatsList);
                                                    if (listOfStudents.Count > 0)
                                                        sr = listOfStudents.Last().SerialNumber;
                                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                    st.SerialNumber = sr + 1;
                                                    st.PossibleCourseAdmitted = student.CoursePreference6;
                                                    listOfStudents.Add(st);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent, reservedSeats) == true)
                                                    {
                                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                        //UpdateSeatData(seatsList);
                                                        if (listOfStudents.Count > 0)
                                                            sr = listOfStudents.Last().SerialNumber;
                                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                        st.SerialNumber = sr + 1;
                                                        st.PossibleCourseAdmitted = student.CoursePreference7;
                                                        listOfStudents.Add(st);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        string returnMessage = string.Empty;
                        #region "Transfer Seat"

                        if (category.ToUpper() == "OPEN" && isSVT == true)
                        {
                            //Convert to External Open 
                            seatsList.Where(w => w.SVTOpenInternal > 0).ToList().ForEach(i => { i.ExternalOpen = i.ExternalOpen + i.SVTOpenInternal; i.SVTOpenInternal = 0; });

                            returnMessage = "SVT Internal Open category " + round + " allocation completed successfully.";
                        }
                        else if (category.ToUpper() == "OPEN" && isSVT == false)
                        {
                            //Convert to External Open 
                            seatsList.Where(w => w.ExternalOpen > 0).ToList().ForEach(i => { i.SVTReservedInternal = i.ExternalOpen + i.SVTReservedInternal; i.ExternalOpen = 0; });

                            returnMessage = "SVT External Reserved category " + round + " allocation completed successfully.";
                        }
                        //Get Categoy and SVT student wise value
                        else if (category.ToUpper() == "RESERVED" && isSVT == true)
                        {
                            //Convert to External Reserved   
                            seatsList.Where(w => w.SVTReservedInternal > 0).ToList().ForEach(i => { i.ExternalReserved = i.ExternalReserved + i.SVTReservedInternal; i.SVTReservedInternal = 0; });

                            returnMessage = "SVT Internal Reserved category " + round + " allocation completed successfully.";
                        }
                        else
                        {
                            //seatsList.Where(w => w.ExternalReserved > 0).ToList().ForEach(i => { i.SVTOpenInternal = i.SVTOpenInternal + i.ExternalReserved; i.ExternalReserved = 0; });
                            returnMessage = "SVT External Reserved category " + round + " allocation completed successfully.";
                        }
                        //UpdateSeatData(seatsList);

                        #endregion
                        return listOfStudents;
                        //
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion
            }

            return null;
        }
         */
        #endregion

        /// <summary>
        /// This Method is used to get vacant seat after swapping for round 2/3
        /// </summary>
        public int GetReservedVacantSeats(ReservationCategory category, string studentCaste)
        {
            studentCaste = studentCaste.ToLower().Replace("(", "").Replace(")", "").Replace(" ", "");
            int seats = 0;
            switch (studentCaste)
            {
                case "sc":
                    if (category.sc <= 0)
                    {
                        if (category.st > 0)
                            seats = category.st;
                    }
                    else if (category.sc > 0)
                    {
                        seats = category.sc;
                    }
                    break;
                case "st":
                    if (category.st <= 0)
                    {
                        if (category.sc > 0)
                            seats = category.sc;
                    }
                    else if (category.st > 0)
                    {
                        seats = category.st;
                    }
                    break;
                case "vj":
                    if (category.vj <= 0)
                    {
                        if (category.nt1 > 0)
                            seats = category.nt1;
                    }
                    else if (category.vj > 0)
                    {
                        seats = category.vj;
                    }
                    break;
                case "nt1":
                    if (category.nt1 <= 0)
                    {
                        if (category.vj > 0)
                            seats = category.vj;
                    }
                    else if (category.nt1 > 0)
                    {
                        seats = category.nt1;
                    }
                    break;
                case "nt2":
                    if (category.nt2 <= 0)
                    {
                        if (category.nt3 > 0)
                            seats = category.nt3;
                        else if (category.obc > 0)
                            seats = category.obc;
                    }
                    else if (category.nt2 > 0)
                    {
                        seats = category.nt2;
                    }
                    break;
                case "nt3":
                    if (category.nt3 <= 0)
                    {
                        if (category.nt2 > 0)
                            seats = category.nt2;
                        else if (category.obc > 0)
                            seats = category.obc;
                    }
                    else if (category.nt3 > 0)
                    {
                        seats = category.nt3;
                    }
                    break;
                case "obc":
                    if (category.obc <= 0)
                    {
                        if (category.nt2 > 0)
                            seats = category.nt2;
                        else if (category.nt3 > 0)
                            seats = category.nt3;
                    }
                    else if (category.obc > 0)
                    {
                        seats = category.obc;
                    }
                    break;
            }

            return seats;
        }

        /// <summary>
        /// This Method is used to adjust the seats between SC/ST, OBC/NT2/NT3, VJ/NT1
        /// </summary>
        public ReservationCategory CheckAndSubtractCombinedReservedSeats(ReservationCategory category, string studentCaste)
        {
            studentCaste = studentCaste.ToLower().Replace("(", "").Replace(")", "").Replace(" ", "");

            switch (studentCaste)
            {
                case "sc":
                    if (category.sc <= 0)
                    {
                        if (category.st > 0)
                            category.st = category.st - 1;
                    }
                    else if (category.sc > 0)
                    {
                        category.sc = category.sc - 1;
                    }
                    break;
                case "st":
                    if (category.st <= 0)
                    {
                        if (category.sc > 0)
                            category.sc = category.sc - 1;
                    }
                    else if (category.st > 0)
                    {
                        category.st = category.st - 1;
                    }
                    break;
                case "vj":
                    if (category.vj <= 0)
                    {
                        if (category.nt1 > 0)
                            category.nt1 = category.nt1 - 1;
                    }
                    else if (category.vj > 0)
                    {
                        category.vj = category.vj - 1;
                    }
                    break;
                case "nt1":
                    if (category.nt1 <= 0)
                    {
                        if (category.vj > 0)
                            category.vj = category.vj - 1;
                    }
                    else if (category.nt1 > 0)
                    {
                        category.nt1 = category.nt1 - 1;
                    }
                    break;
                case "nt2":
                    if (category.nt2 <= 0)
                    {
                        if (category.nt3 > 0)
                            category.nt3 = category.nt3 - 1;
                        else if (category.obc > 0)
                            category.obc = category.obc - 1;
                    }
                    else if (category.nt2 > 0)
                    {
                        category.nt2 = category.nt2 - 1;
                    }
                    break;
                case "nt3":
                    if (category.nt3 <= 0)
                    {
                        if (category.nt2 > 0)
                            category.nt2 = category.nt2 - 1;
                        else if (category.obc > 0)
                            category.obc = category.obc - 1;
                    }
                    else if (category.nt3 > 0)
                    {
                        category.nt3 = category.nt3 - 1;
                    }
                    break;
                case "obc":
                    if (category.obc <= 0)
                    {
                        if (category.nt2 > 0)
                            category.nt2 = category.nt2 - 1;
                        else if (category.nt3 > 0)
                            category.nt3 = category.nt3 - 1;
                    }
                    else if (category.obc > 0)
                    {
                        category.obc = category.obc - 1;
                    }
                    break;
            }

            return category;
        }

        public void AdjustSeats(List<ReservationSeatList> reservedSeats, string studentType)
        {
            foreach (ReservationSeatList reservedSeat in reservedSeats)
            {
                Type firstType = reservedSeat.GetType();
                foreach (PropertyInfo property in firstType.GetProperties())
                {
                    if (property.CanRead)
                    {
                        var seats = property.GetValue(reservedSeat, null);
                        if (studentType == "internal" && property.Name == "internalCategory")
                        {
                            Type castes = reservedSeat.internalCategory.GetType();
                            foreach (PropertyInfo caste in castes.GetProperties())
                            {
                                switch (caste.Name)
                                {
                                    case "sc":
                                        reservedSeat.externalCategory.sc = reservedSeat.externalCategory.sc + int.Parse(caste.GetValue(reservedSeat.internalCategory).ToString());
                                        reservedSeat.internalCategory.sc = 0;
                                        break;
                                    case "st":
                                        reservedSeat.externalCategory.st = reservedSeat.externalCategory.st + int.Parse(caste.GetValue(reservedSeat.internalCategory).ToString());
                                        reservedSeat.internalCategory.st = 0;
                                        break;
                                    case "vj":
                                        reservedSeat.externalCategory.vj = reservedSeat.externalCategory.vj + int.Parse(caste.GetValue(reservedSeat.internalCategory).ToString());
                                        reservedSeat.internalCategory.vj = 0;
                                        break;
                                    case "nt1":
                                        reservedSeat.externalCategory.nt1 = reservedSeat.externalCategory.nt1 + int.Parse(caste.GetValue(reservedSeat.internalCategory).ToString());
                                        reservedSeat.internalCategory.nt1 = 0;
                                        break;
                                    case "nt2":
                                        reservedSeat.externalCategory.nt2 = reservedSeat.externalCategory.nt2 + int.Parse(caste.GetValue(reservedSeat.internalCategory).ToString());
                                        reservedSeat.internalCategory.nt2 = 0;
                                        break;
                                    case "nt3":
                                        reservedSeat.externalCategory.nt3 = reservedSeat.externalCategory.nt3 + int.Parse(caste.GetValue(reservedSeat.internalCategory).ToString());
                                        reservedSeat.internalCategory.nt3 = 0;
                                        break;
                                    case "obc":
                                        reservedSeat.externalCategory.obc = reservedSeat.externalCategory.obc + int.Parse(caste.GetValue(reservedSeat.internalCategory).ToString());
                                        reservedSeat.internalCategory.obc = 0;
                                        break;
                                    case "sebc":
                                        reservedSeat.externalCategory.sebc = reservedSeat.externalCategory.sebc + int.Parse(caste.GetValue(reservedSeat.internalCategory).ToString());
                                        reservedSeat.internalCategory.sebc = 0;
                                        break;
                                    case "ebc":
                                        reservedSeat.externalCategory.ebc = reservedSeat.externalCategory.ebc + int.Parse(caste.GetValue(reservedSeat.internalCategory).ToString());
                                        reservedSeat.internalCategory.ebc = 0;
                                        break;
                                    case "sbc":
                                        reservedSeat.externalCategory.sbc = reservedSeat.externalCategory.sbc + int.Parse(caste.GetValue(reservedSeat.internalCategory).ToString());
                                        reservedSeat.internalCategory.sbc = 0;
                                        break;
                                }
                            }
                        }
                    }
                }
                /* int scSeats = reservedSeat.internalCategory.sc;
             reservedSeats.FirstOrDefault(s => s.specialisation == "Food, Nutrition and Dietetics").internalCategory.sc = 0;
             int externalscSeats = reservedSeats.FirstOrDefault(s => s.specialisation == "Food, Nutrition and Dietetics").externalCategory.sc;
             reservedSeats.FirstOrDefault(s => s.specialisation == "Food, Nutrition and Dietetics").externalCategory.sc = externalscSeats + scSeats;
             */
            }
        }

        [HttpGet]
        public HttpResponseMessage PreviewMeritList(bool isSVT, string category, string round)
        {
            if (string.IsNullOrEmpty(category) || (category.ToUpper() != "OPEN" && category.ToUpper() != "RESERVED")
                || string.IsNullOrEmpty(round) || (round.ToUpper() != "ROUND1" && round.ToUpper() != "ROUND2" && round.ToUpper() != "ROUND3"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Please pass proper parameter" });
            }

            var previewList = GetPreviewList(isSVT, category, round);
            if (previewList != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new MeritStudentDetails() { IsSuccess = true, SuccessMessage = "Success", StudentList = previewList });
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Please check json file data" });
            }
        }

        public PreviewStudentDetails ConvertToPreviewStudentDetails(StudentDetail details)
        {
            PreviewStudentDetails d = new PreviewStudentDetails();
            d.Id = details.Id;
            d.FullName = details.LastName + " " + details.FirstName;
            d.FatherName = details.FatherName;
            d.MotherName = details.MotherName;
            d.Percentage = details.Percentage;
            d.Category = details.Category;
            d.Caste = details.Caste;
            if (details.IsSVTStudent == true)
                d.InternalExternal = "Internal";
            else
                d.InternalExternal = "External";

            d.CoursePreference1 = details.CoursePreference1;
            d.CoursePreference2 = details.CoursePreference2;
            d.CoursePreference3 = details.CoursePreference3;
            d.CoursePreference4 = details.CoursePreference4;
            d.CoursePreference5 = details.CoursePreference5;
            d.CoursePreference6 = details.CoursePreference6;
            d.CoursePreference7 = details.CoursePreference7;
            if (!string.IsNullOrEmpty(details.CourseAdmittedRound1))
            {
                d.CourseAdmittedRound1 = details.CourseAdmittedRound1;
            }
            if (!string.IsNullOrEmpty(details.CourseAdmittedRound2))
            {
                d.CourseAdmittedRound2 = details.CourseAdmittedRound2;
            }
            return d;
        }


        [HttpGet]
        public HttpResponseMessage UpdateStudentValues(string Id, string operation, bool value)
        {
            if (string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(operation))
            {
                return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Please pass proper parameter" });
            }
            else
            {
                string connetionString = null;
                SqlConnection connection = null;
                SqlCommand command;
                string sql = null;
                int val = value ? 1 : 0;
                string key = string.Empty;
                try
                {

                    connetionString = System.Configuration.ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
                    connection = new SqlConnection(connetionString);
                    if (connection.State == ConnectionState.Open)
                        connection.Close();

                    connection.Open();
                    if (operation.ToLower().Equals("iscancelled") && value == true)
                    {
                        sql = string.Format("Select top 1 AadharNumber from StudentDetails where Id='{0}'", Id);
                        command = new SqlCommand(sql, connection);
                        string stradharcard = Convert.ToString(command.ExecuteScalar());
                        if (!string.IsNullOrEmpty(stradharcard))
                        {
                            sql = string.Format("Update StudentDetails set IsCancelled='{0}' where AadharNumber='{1}'", 1, stradharcard);
                            command = new SqlCommand(sql, connection);
                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        sql = string.Format("Update StudentDetails set {0}='{1}' where Id='{2}'", operation, val, Id);
                        command = new SqlCommand(sql, connection);
                        int count = command.ExecuteNonQuery();
                        if (operation.ToLower().Equals("isformsubmitted") && count > 0)
                        {
                            sql = string.Format("Update StudentDetails set FormSubmittedDate='{0}' where Id='{1}'", DateTime.Now, Id);
                            command = new SqlCommand(sql, connection);
                            command.ExecuteNonQuery();
                        }
                        if (operation.ToLower().Equals("isfeespaid") && count > 0)
                        {
                            sql = string.Format("Update StudentDetails set FeesPaidDate='{0}' where Id='{1}'", DateTime.Now, Id);
                            command = new SqlCommand(sql, connection);
                            command.ExecuteNonQuery();
                        }

                        command.Dispose();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Value updated successfully." });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }

            }
        }

        public bool SetDuplicateRecord(string Id, string aadharNumber)
        {
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;
            string sql = null;
            connetionString = System.Configuration.ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
            sql = string.Format("Update StudentDetails set IsDuplicate='{0}' where AadharNumber='{1}' and Id!={2} ", 1, aadharNumber, Id);
            string key = string.Empty;
            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        [HttpGet]
        public HttpResponseMessage IsAadharCardExist(string aadharNumber)
        {
            if (string.IsNullOrEmpty(aadharNumber))
            {
                return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Please pass proper parameter" });
            }
            else
            {
                string connetionString = null;
                SqlConnection connection;
                SqlCommand command;
                string sql = null;
                connetionString = System.Configuration.ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
                sql = string.Format("select count(*) from StudentDetails where AadharNumber='{0}' and (IsCancelled=0 or IsCancelled IS NULL) ", aadharNumber);
                string key = string.Empty;
                connection = new SqlConnection(connetionString);
                try
                {
                    connection.Open();
                    command = new SqlCommand(sql, connection);
                    int value = Convert.ToInt32(command.ExecuteScalar());
                    int SubmittedCount = 0;
                    if (value > 0)
                    {
                        sql = string.Format("select count(*) from StudentDetails where AadharNumber='{0}' and (IsCancelled=0 or IsCancelled IS NULL) and IsFormSubmitted=1", aadharNumber);
                        command = new SqlCommand(sql, connection);
                        SubmittedCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                    command.Dispose();
                    return Request.CreateResponse(HttpStatusCode.OK, new AadharClass()
                    {
                        IsSuccess = true,
                        SuccessMessage = "Success",
                        IsAadharExist = value > 0,
                        IsFormSubmitted = SubmittedCount > 0
                    });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        public bool IsAadharSubmitted(string aadharNumber)
        {
            string connetionString = null;
            SqlConnection connection;
            SqlCommand command;
            string sql = null;
            connetionString = System.Configuration.ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
            connection = new SqlConnection(connetionString);
            try
            {
                connection.Open();
                int SubmittedCount = 0;
                sql = string.Format("select count(*) from StudentDetails where AadharNumber='{0}' and (IsCancelled=0 or IsCancelled IS NULL) and IsFormSubmitted=1", aadharNumber);
                command = new SqlCommand(sql, connection);
                SubmittedCount = Convert.ToInt32(command.ExecuteScalar());
                return SubmittedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        [HttpGet]
        public HttpResponseMessage UpdateFinalAdmitted()
        {
            using (ServiceContext service = new ServiceContext())
            {
                int isUpdate = service.UpdateFinalAdmitted();
                if (isUpdate > 0)
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Final admitted updated successfully" });
                else
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Fail to update final Admitted" });
            }
        }

        [HttpGet]
        public HttpResponseMessage GenerateRollNumbers()
        {
            using (ServiceContext service = new ServiceContext())
            {
                bool isUpdate = service.GenerateRollNumber();
                if (isUpdate)
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "RollNumbers Generated successfully" });
                else
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Fail to generate rollnumbers" });
            }
        }


        public bool CheckRoundCutoffandAvailbilityByCourse(string coursePreference, List<RoundList> roundList, ref List<SeatsList> seatList, StudentDetail student, bool svtOpenInternal, bool svtReservedInternal, bool externalOpen, bool externalReserved, string round, string lastAdmittedCourse, ref bool leaveStudent)
        {
            if (!string.IsNullOrEmpty(lastAdmittedCourse) && lastAdmittedCourse == coursePreference)
            {
                leaveStudent = true;
                return false;
            }
            var courseCutoffData = roundList.Where(m => m.specialisation == coursePreference).FirstOrDefault();
            var seatData = seatList.Where(m => m.specialisation == coursePreference).FirstOrDefault();

            if (!string.IsNullOrEmpty(coursePreference) && courseCutoffData != null && seatData != null)
            {
                #region SVTOPENINTERNAL
                if (svtOpenInternal && student.Percentage >= courseCutoffData.SVTOpenInternal && seatData.SVTOpenInternal > 0)
                {
                    seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.SVTOpenInternal = i.SVTOpenInternal - 1);

                    #region  Check Round 1 and 2 Preferences
                    if (round.ToUpper() == "ROUND2" && student.AdmittedRound1 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.SVTOpenInternal = i.SVTOpenInternal + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound1).ToList().ForEach(i => i.SVTOpenInternal = i.SVTOpenInternal + 1);
                        }
                    }

                    else if (round.ToUpper() == "ROUND3" && student.AdmittedRound2 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.SVTOpenInternal = i.SVTOpenInternal + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound2).ToList().ForEach(i => i.SVTOpenInternal = i.SVTOpenInternal + 1);
                        }
                    }
                    else if (round.ToUpper() == "ROUND3" && student.AdmittedRound1 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.SVTOpenInternal = i.SVTOpenInternal + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound1).ToList().ForEach(i => i.SVTOpenInternal = i.SVTOpenInternal + 1);
                        }
                    }
                    #endregion

                    return true;
                }
                #endregion SVTOPENINTERNAL

                #region EXTERNALOPEN
                else if (externalOpen && student.Percentage >= courseCutoffData.ExternalOpen && seatData.ExternalOpen > 0)
                {
                    seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.ExternalOpen = i.ExternalOpen - 1);


                    #region  Check Round 1 and 2 Preferences
                    if (round.ToUpper() == "ROUND2" && student.AdmittedRound1 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.ExternalOpen = i.ExternalOpen + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound1).ToList().ForEach(i => i.ExternalOpen = i.ExternalOpen + 1);
                        }
                    }

                    else if (round.ToUpper() == "ROUND3" && student.AdmittedRound2 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.ExternalOpen = i.ExternalOpen + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound2).ToList().ForEach(i => i.ExternalOpen = i.ExternalOpen + 1);
                        }
                    }
                    else if (round.ToUpper() == "ROUND3" && student.AdmittedRound1 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.ExternalOpen = i.ExternalOpen + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound1).ToList().ForEach(i => i.ExternalOpen = i.ExternalOpen + 1);
                        }
                    }
                    #endregion

                    return true;
                }
                #endregion EXTERNALOPEN

                #region SVT RESERVED INTERNAL
                else if (svtReservedInternal && student.Percentage >= courseCutoffData.SVTReservedInternal && seatData.SVTReservedInternal > 0)
                {
                    seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.SVTReservedInternal = i.SVTReservedInternal - 1);

                    #region  Check Round 1 and 2 Preferences
                    if (round.ToUpper() == "ROUND2" && student.AdmittedRound1 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.SVTReservedInternal = i.SVTReservedInternal + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound1).ToList().ForEach(i => i.SVTReservedInternal = i.SVTReservedInternal + 1);
                        }
                    }
                    else if (round.ToUpper() == "ROUND3" && student.AdmittedRound2 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.SVTReservedInternal = i.SVTReservedInternal + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound2).ToList().ForEach(i => i.SVTReservedInternal = i.SVTReservedInternal + 1);
                        }
                    }
                    else if (round.ToUpper() == "ROUND3" && student.AdmittedRound1 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.SVTReservedInternal = i.SVTReservedInternal + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound1).ToList().ForEach(i => i.SVTReservedInternal = i.SVTReservedInternal + 1);
                        }
                    }
                    #endregion
                    return true;
                }
                #endregion SVTRESERVEDINTERNAL

                #region EXTERNALRESERVED
                else if (externalReserved && student.Percentage >= courseCutoffData.ExternalReserved && seatData.ExternalReserved > 0)
                {
                    seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.ExternalReserved = i.ExternalReserved - 1);

                    #region  Check Round 1 and 2 Preferences
                    if (round.ToUpper() == "ROUND2" && student.AdmittedRound1 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.ExternalReserved = i.ExternalReserved + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound1).ToList().ForEach(i => i.ExternalReserved = i.ExternalReserved + 1);
                        }
                    }
                    else if (round.ToUpper() == "ROUND3" && student.AdmittedRound2 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.ExternalReserved = i.ExternalReserved + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound2).ToList().ForEach(i => i.ExternalReserved = i.ExternalReserved + 1);
                        }
                    }
                    else if (round.ToUpper() == "ROUND3" && student.AdmittedRound1 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.ExternalReserved = i.ExternalReserved + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound1).ToList().ForEach(i => i.ExternalReserved = i.ExternalReserved + 1);
                        }
                    }
                    #endregion

                    return true;
                }
                #endregion EXTERNALRESERVED
            }

            return false;
        }

        public bool CheckRoundCutoffandAvailbilityByCourse(string coursePreference, List<RoundList> roundList, ref List<SeatsList> seatList, StudentDetail student, bool svtOpenInternal, bool svtReservedInternal, bool externalOpen, bool externalReserved, string round, string lastAdmittedCourse, ref bool leaveStudent, List<ReservationSeatList> reservedSeats)
        {
            if (!string.IsNullOrEmpty(lastAdmittedCourse) && lastAdmittedCourse == coursePreference)
            {
                leaveStudent = true;
                return false;
            }
            var courseCutoffData = roundList.Where(m => m.specialisation == coursePreference).FirstOrDefault();
            var seatData = seatList.Where(m => m.specialisation == coursePreference).FirstOrDefault();

            if (!string.IsNullOrEmpty(coursePreference) && courseCutoffData != null && seatData != null)
            {
                #region SVTOPENINTERNAL
                if (svtOpenInternal && student.Percentage >= courseCutoffData.SVTOpenInternal && seatData.SVTOpenInternal > 0)
                {
                    seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.SVTOpenInternal = i.SVTOpenInternal - 1);
                    #region  Check Round 1 and 2 Preferences
                    if (round.ToUpper() == "ROUND2" && student.AdmittedRound1 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.SVTOpenInternal = i.SVTOpenInternal + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound1).ToList().ForEach(i => i.SVTOpenInternal = i.SVTOpenInternal + 1);
                        }
                    }

                    else if (round.ToUpper() == "ROUND3" && student.AdmittedRound2 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.SVTOpenInternal = i.SVTOpenInternal + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound2).ToList().ForEach(i => i.SVTOpenInternal = i.SVTOpenInternal + 1);
                        }
                    }
                    else if (round.ToUpper() == "ROUND3" && student.AdmittedRound1 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.SVTOpenInternal = i.SVTOpenInternal + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound1).ToList().ForEach(i => i.SVTOpenInternal = i.SVTOpenInternal + 1);
                        }
                    }
                    #endregion

                    return true;
                }
                #endregion SVTOPENINTERNAL

                #region EXTERNALOPEN
                else if (externalOpen && student.Percentage >= courseCutoffData.ExternalOpen && seatData.ExternalOpen > 0)
                {
                    seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.ExternalOpen = i.ExternalOpen - 1);


                    #region  Check Round 1 and 2 Preferences
                    if (round.ToUpper() == "ROUND2" && student.AdmittedRound1 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.ExternalOpen = i.ExternalOpen + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound1).ToList().ForEach(i => i.ExternalOpen = i.ExternalOpen + 1);
                        }
                    }

                    else if (round.ToUpper() == "ROUND3" && student.AdmittedRound2 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.ExternalOpen = i.ExternalOpen + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound2).ToList().ForEach(i => i.ExternalOpen = i.ExternalOpen + 1);
                        }
                    }
                    else if (round.ToUpper() == "ROUND3" && student.AdmittedRound1 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.ExternalOpen = i.ExternalOpen + 1);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound1).ToList().ForEach(i => i.ExternalOpen = i.ExternalOpen + 1);
                        }
                    }
                    #endregion

                    return true;
                }
                #endregion EXTERNALOPEN

                #region SVT RESERVED INTERNAL
                else if (svtReservedInternal && student.Percentage >= courseCutoffData.SVTReservedInternal && seatData.SVTReservedInternal > 0)
                {
                    #region Check if reservation seats are available (cross castes)
                    var reservationCategory = reservedSeats.FirstOrDefault(s => s.specialisation == coursePreference);
                    if (GetReservedVacantSeats(reservationCategory.internalCategory, student.Caste) <= 0)
                    {
                        leaveStudent = true;
                        return false;
                    }
                    #endregion

                    seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.SVTReservedInternal = i.SVTReservedInternal - 1);
                    var obj = reservedSeats.FirstOrDefault(s => s.specialisation == coursePreference);
                    obj.internalCategory = SubtractReservedSeat(obj.internalCategory, student.Caste);

                    #region  Check Round 1 and 2 Preferences
                    if (round.ToUpper() == "ROUND2" && student.AdmittedRound1 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.SVTReservedInternal = i.SVTReservedInternal + 1);
                            obj.internalCategory = AddReservedSeat(obj.internalCategory, student.Caste);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound1).ToList().ForEach(i => i.SVTReservedInternal = i.SVTReservedInternal + 1);
                            //obj.internalCategory = AddReservedSeat(obj.internalCategory, student.Caste);
                            var obj1 = reservedSeats.FirstOrDefault(s => s.specialisation == student.CourseAdmittedRound1);
                            obj1.internalCategory = AddReservedSeat(obj1.internalCategory, student.Caste);
                        }
                    }
                    else if (round.ToUpper() == "ROUND3" && student.AdmittedRound2 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.SVTReservedInternal = i.SVTReservedInternal + 1);
                            obj.internalCategory = AddReservedSeat(obj.internalCategory, student.Caste);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound2).ToList().ForEach(i => i.SVTReservedInternal = i.SVTReservedInternal + 1);
                            //obj.internalCategory = AddReservedSeat(obj.internalCategory, student.Caste);
                            var obj1 = reservedSeats.FirstOrDefault(s => s.specialisation == student.CourseAdmittedRound2);

                            if (obj1 != null)
                                obj1.internalCategory = AddReservedSeat(obj1.internalCategory, student.Caste);
                        }
                    }
                    else if (round.ToUpper() == "ROUND3" && student.AdmittedRound1 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.SVTReservedInternal = i.SVTReservedInternal + 1);
                            obj.internalCategory = AddReservedSeat(obj.internalCategory, student.Caste);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound1).ToList().ForEach(i => i.SVTReservedInternal = i.SVTReservedInternal + 1);
                            //obj.internalCategory = AddReservedSeat(obj.internalCategory, student.Caste);
                            var obj1 = reservedSeats.FirstOrDefault(s => s.specialisation == student.CourseAdmittedRound1);
                            obj1.internalCategory = AddReservedSeat(obj1.internalCategory, student.Caste);
                        }
                    }
                    #endregion
                    return true;
                }
                #endregion SVTRESERVEDINTERNAL

                #region EXTERNALRESERVED
                else if (externalReserved && student.Percentage >= courseCutoffData.ExternalReserved && seatData.ExternalReserved > 0)
                {
                    #region Check if reservation seats are available (cross castes)
                    var reservationCategory = reservedSeats.FirstOrDefault(s => s.specialisation == coursePreference);
                    if (GetReservedVacantSeats(reservationCategory.externalCategory, student.Caste) <= 0)
                    {
                        leaveStudent = true;
                        return false;
                    }
                    #endregion

                    seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.ExternalReserved = i.ExternalReserved - 1);
                    var obj = reservedSeats.FirstOrDefault(s => s.specialisation == coursePreference);
                    obj.externalCategory = SubtractReservedSeat(obj.externalCategory, student.Caste);

                    #region  Check Round 1 and 2 Preferences
                    if (round.ToUpper() == "ROUND2" && student.AdmittedRound1 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.ExternalReserved = i.ExternalReserved + 1);
                            obj.externalCategory = AddReservedSeat(obj.externalCategory, student.Caste);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound1).ToList().ForEach(i => i.ExternalReserved = i.ExternalReserved + 1);
                            var obj1 = reservedSeats.FirstOrDefault(s => s.specialisation == student.CourseAdmittedRound1);
                            obj1.externalCategory = AddReservedSeat(obj1.externalCategory, student.Caste);
                        }
                    }
                    else if (round.ToUpper() == "ROUND3" && student.AdmittedRound2 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.ExternalReserved = i.ExternalReserved + 1);
                            obj.externalCategory = AddReservedSeat(obj.externalCategory, student.Caste);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound2).ToList().ForEach(i => i.ExternalReserved = i.ExternalReserved + 1);
                            var obj1 = reservedSeats.FirstOrDefault(s => s.specialisation == student.CourseAdmittedRound2);
                            obj1.externalCategory = AddReservedSeat(obj1.externalCategory, student.Caste);
                        }
                    }
                    else if (round.ToUpper() == "ROUND3" && student.AdmittedRound1 == true)
                    {
                        if (student.IsFeesPaid == null || student.IsFeesPaid.Value == false)
                        {
                            seatList.Where(w => w.specialisation == coursePreference).ToList().ForEach(i => i.ExternalReserved = i.ExternalReserved + 1);
                            obj.externalCategory = AddReservedSeat(obj.externalCategory, student.Caste);
                            leaveStudent = true;
                            return false;
                        }
                        else
                        {
                            seatList.Where(w => w.specialisation == student.CourseAdmittedRound1).ToList().ForEach(i => i.ExternalReserved = i.ExternalReserved + 1);
                            var obj1 = reservedSeats.FirstOrDefault(s => s.specialisation == student.CourseAdmittedRound1);
                            obj1.externalCategory = AddReservedSeat(obj1.externalCategory, student.Caste);
                        }
                    }
                    #endregion

                    return true;
                }
                #endregion EXTERNALRESERVED
            }

            return false;
        }

        public void UpdateReservedQuotaSeats(List<ReservationSeatList> seatList)
        {
            string outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(seatList, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(System.Web.HttpContext.Current.Server.MapPath("~/data/pdf/researvation.json"), outputJSON + Environment.NewLine);
        }

        private int GetReservedVacantSeat(ReservationCategory category, string caste)
        {
            int vacantSeats = 0;
            caste = caste.ToLower().Replace("(", "").Replace(")", "").Replace(" ", "");
            if (caste == nameof(category.sc))
            {
                vacantSeats = category.sc;
            }
            else if (caste == nameof(category.st))
            {
                vacantSeats = category.st;
            }
            else if (caste == nameof(category.vj))
            {
                vacantSeats = category.vj;
            }
            else if (caste == nameof(category.nt1))
            {
                vacantSeats = category.nt1;
            }
            else if (caste == nameof(category.nt2))
            {
                vacantSeats = category.nt2;
            }
            else if (caste == nameof(category.nt3))
            {
                vacantSeats = category.nt3;
            }
            else if (caste == nameof(category.obc))
            {
                vacantSeats = category.obc;
            }
            else if (caste == nameof(category.sebc))
            {
                vacantSeats = category.sebc;
            }
            else if (caste == nameof(category.ebc))
            {
                vacantSeats = category.ebc;
            }
            else if (caste == nameof(category.sbc))
            {
                vacantSeats = category.sbc;
            }


            return vacantSeats;
        }

        private ReservationCategory SubtractReservedSeat(ReservationCategory category, string caste)
        {
            caste = caste.ToLower().Replace("(", "").Replace(")", "").Replace(" ", "");
            if (caste == nameof(category.sc))
            {
                category.sc = category.sc - 1;
            }
            else if (caste == nameof(category.st))
            {
                category.st = category.st - 1;
            }
            else if (caste == nameof(category.vj))
            {
                category.vj = category.vj - 1;
            }
            else if (caste == nameof(category.nt1))
            {
                category.nt1 = category.nt1 - 1;
            }
            else if (caste == nameof(category.nt2))
            {
                category.nt2 = category.nt2 - 1;
            }
            else if (caste == nameof(category.nt3))
            {
                category.nt3 = category.nt3 - 1;
            }
            else if (caste == nameof(category.obc))
            {
                category.obc = category.obc - 1;
            }
            else if (caste == nameof(category.sebc))
            {
                category.sebc = category.sebc - 1;
            }
            else if (caste == nameof(category.ebc))
            {
                category.ebc = category.ebc - 1;
            }
            else if (caste == nameof(category.sbc))
            {
                category.sbc = category.sbc - 1;
            }

            return category;
        }

        private ReservationCategory AddReservedSeat(ReservationCategory category, string caste)
        {
            caste = caste.ToLower().Replace("(", "").Replace(")", "").Replace(" ", "");
            if (caste == nameof(category.sc))
            {
                category.sc = category.sc + 1;
            }
            else if (caste == nameof(category.st))
            {
                category.st = category.st + 1;
            }
            else if (caste == nameof(category.vj))
            {
                category.vj = category.vj + 1;
            }
            else if (caste == nameof(category.nt1))
            {
                category.nt1 = category.nt1 + 1;
            }
            else if (caste == nameof(category.nt2))
            {
                category.nt2 = category.nt2 + 1;
            }
            else if (caste == nameof(category.nt3))
            {
                category.nt3 = category.nt3 + 1;
            }
            else if (caste == nameof(category.obc))
            {
                category.obc = category.obc + 1;
            }
            else if (caste == nameof(category.sebc))
            {
                category.sebc = category.sebc + 1;
            }
            else if (caste == nameof(category.ebc))
            {
                category.ebc = category.ebc + 1;
            }
            else if (caste == nameof(category.sbc))
            {
                category.sbc = category.sbc + 1;
            }

            return category;
        }

        public void UpdateSeatData(List<SeatsList> seatsList)
        {
            string outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(seatsList, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(System.Web.HttpContext.Current.Server.MapPath("~/data/pdf/Seats.json"), outputJSON + Environment.NewLine);
        }

        [HttpGet]
        public List<RoundList> ReadRoundFile(string roundName)
        {
            List<RoundList> roundList = new List<RoundList>();
            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/data/pdf/" + roundName + ".json")))
            {
                String JSONtxt = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/data/pdf/" + roundName + ".json"));
                roundList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RoundList>>(JSONtxt);
            }
            return roundList;
        }

        public List<RoundList> ReadRoundPReviewFile(string specialisation)
        {
            List<RoundList> roundList = new List<RoundList>();

            int startOpenInternal = 75;
            int startOpenExternal = 80;
            int startReservedInternal = 65;
            int startReservedExternal = 75;

            switch (specialisation)
            {
                case "Food, Nutrition and Dietetics":
                    {
                        startOpenInternal = 75;
                        startOpenExternal = 89;
                        startReservedInternal = 63;
                        startReservedExternal = 65;
                        for (int i = 0; i < 11; i++)
                        {
                            RoundList round = new RoundList();
                            round.specialisation = "Food, Nutrition and Dietetics";
                            round.SVTOpenInternal = startOpenInternal - i;
                            round.SVTReservedInternal = startReservedInternal - i;
                            round.ExternalOpen = startOpenExternal - i;
                            round.ExternalReserved = startReservedExternal - i;
                            roundList.Add(round);
                        }
                    }
                    break;
                case "Textiles & Apparel Designing":
                    {
                        startOpenInternal = 65;
                        startOpenExternal = 84;
                        startReservedInternal = 60;
                        startReservedExternal = 63;

                        for (int i = 0; i < 11; i++)
                        {
                            RoundList round = new RoundList();
                            round.specialisation = "Textiles & Apparel Designing";
                            round.SVTOpenInternal = startOpenInternal - i;
                            round.SVTReservedInternal = startReservedInternal - i;
                            round.ExternalOpen = startOpenExternal - i;
                            round.ExternalReserved = startReservedExternal - i;
                            roundList.Add(round);
                        }
                    }
                    break;
                case "Developmental Counselling":
                    {
                        startOpenInternal = 72;
                        startOpenExternal = 75;
                        startReservedInternal = 55;
                        startReservedExternal = 65;

                        for (int i = 0; i < 11; i++)
                        {
                            RoundList round = new RoundList();
                            round.specialisation = "Developmental Counselling";
                            round.SVTOpenInternal = startOpenInternal - i;
                            round.SVTReservedInternal = startReservedInternal - i;
                            round.ExternalOpen = startOpenExternal - i;
                            round.ExternalReserved = startReservedExternal - i;
                            roundList.Add(round);
                        }
                    }
                    break;
                case "Hospitality & Tourism Management":
                    {
                        startOpenInternal = 64;
                        startOpenExternal = 80;
                        startReservedInternal = 60;
                        startReservedExternal = 62;

                        for (int i = 0; i < 11; i++)
                        {
                            RoundList round = new RoundList();
                            round.specialisation = "Hospitality & Tourism Management";
                            round.SVTOpenInternal = startOpenInternal - i;
                            round.SVTReservedInternal = startReservedInternal - i;
                            round.ExternalOpen = startOpenExternal - i;
                            round.ExternalReserved = startReservedExternal - i;
                            roundList.Add(round);
                        }
                    }
                    break;
                case "Early Childhood care & Education":
                    {
                        startOpenInternal = 65;
                        startOpenExternal = 75;
                        startReservedInternal = 55;
                        startReservedExternal = 65;

                        for (int i = 0; i < 11; i++)
                        {
                            RoundList round = new RoundList();
                            round.specialisation = "Early Childhood care & Education";
                            round.SVTOpenInternal = startOpenInternal - i;
                            round.SVTReservedInternal = startReservedInternal - i;
                            round.ExternalOpen = startOpenExternal - i;
                            round.ExternalReserved = startReservedExternal - i;
                            roundList.Add(round);
                        }
                    }
                    break;
                case "Mass Communication & Extension":
                    {
                        startOpenInternal = 65;
                        startOpenExternal = 80;
                        startReservedInternal = 60;
                        startReservedExternal = 60;

                        for (int i = 0; i < 11; i++)
                        {
                            RoundList round = new RoundList();
                            round.specialisation = "Mass Communication & Extension";
                            round.SVTOpenInternal = startOpenInternal - i;
                            round.SVTReservedInternal = startReservedInternal - i;
                            round.ExternalOpen = startOpenExternal - i;
                            round.ExternalReserved = startReservedExternal - i;
                            roundList.Add(round);
                        }
                    }
                    break;
                case "Interior Design & Resource Management":
                    {
                        startOpenInternal = 70;
                        startOpenExternal = 85;
                        startReservedInternal = 60;
                        startReservedExternal = 62;

                        for (int i = 0; i < 11; i++)
                        {
                            RoundList round = new RoundList();
                            round.specialisation = "Interior Design & Resource Management";
                            round.SVTOpenInternal = startOpenInternal - i;
                            round.SVTReservedInternal = startReservedInternal - i;
                            round.ExternalOpen = startOpenExternal - i;
                            round.ExternalReserved = startReservedExternal - i;
                            roundList.Add(round);
                        }
                    }
                    break;
            }

            return roundList;
        }

        public List<ReservationSeatList> GetReservationSeat()
        {
            List<ReservationSeatList> seatList = new List<ReservationSeatList>();
            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/data/pdf/researvation.json")))
            {
                String JSONtxt = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/data/pdf/researvation.json"));
                seatList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ReservationSeatList>>(JSONtxt);
            }
            return seatList;
        }

        [HttpPost]
        public HttpResponseMessage UpdateSeatList(List<SeatsList> seatsList)
        {
            if (seatsList == null || seatsList.Count == 0)
                Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Data invalid" });

            string outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(seatsList, Newtonsoft.Json.Formatting.Indented);
            string filePath = string.Empty;
            string path = System.Web.HttpContext.Current.Server.MapPath("~/data/pdf");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            filePath = path + "/" + Path.GetFileName("Seats.json");
            if (File.Exists(filePath))
            {
                string archivePath = path + "/" + Path.GetFileNameWithoutExtension(filePath) + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + Path.GetExtension(filePath);
                System.IO.File.Move(filePath, archivePath);
            }
            File.WriteAllText(filePath, outputJSON + Environment.NewLine);
            return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Seat updated successfully" });
        }

        [HttpPost]
        public HttpResponseMessage UpdateRoundList([FromBody] List<RoundList> roundList, [FromUri]int rNo)
        {
            if (roundList == null || roundList.Count == 0 || rNo == 0)
                Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Data invalid" });

            string outputJSON = Newtonsoft.Json.JsonConvert.SerializeObject(roundList, Newtonsoft.Json.Formatting.Indented);
            string filePath = string.Empty;
            string path = System.Web.HttpContext.Current.Server.MapPath("~/data/pdf");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            filePath = path + "/" + Path.GetFileName("Round" + rNo + ".json");
            if (File.Exists(filePath))
            {
                string archivePath = path + "/" + Path.GetFileNameWithoutExtension(filePath) + "_" + DateTime.Now.ToString("yyyy-MM-dd HH mm ss") + Path.GetExtension(filePath);
                System.IO.File.Move(filePath, archivePath);
            }
            File.WriteAllText(filePath, outputJSON + Environment.NewLine);
            return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "File updated successfully" });
        }


        [HttpGet]
        public List<SeatsList> ReadSeatsFile()
        {
            List<SeatsList> seatList = new List<SeatsList>();
            if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/data/pdf/Seats.json")))
            {
                String JSONtxt = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/data/pdf/Seats.json"));
                seatList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SeatsList>>(JSONtxt);
            }
            return seatList;
        }


        [HttpGet]
        public HttpResponseMessage GetAdmissionFormSummary()
        {
            try
            {
                AdmissionFormSummary summary = new AdmissionFormSummary();
                using (ServiceContext service = new ServiceContext())
                {
                    var studenList = service.GetStudentListForSummary();
                    if (studenList != null && studenList.Count > 0)
                    {
                        summary.TotalForms = studenList.Count;

                        summary.OpenInternal = new OpenInternal();
                        summary.OpenInternal.TotalForms = studenList.Where(s => s.IsSVTStudent == true && s.Category.ToUpper() == "OPEN").Count();
                        summary.OpenInternal.MaxPercent = studenList.Where(s => s.IsSVTStudent == true && s.Category.ToUpper() == "OPEN").Max(s => s.Percentage).ToDouble();
                        summary.OpenInternal.MinPercent = studenList.Where(s => s.IsSVTStudent == true && s.Category.ToUpper() == "OPEN").Min(s => s.Percentage).ToDouble();
                        summary.OpenInternal.AvgPercent = studenList.Where(s => s.IsSVTStudent == true && s.Category.ToUpper() == "OPEN").Average(s => s.Percentage).ToDouble();

                        summary.ReservedInternal = new ReservedInternal();
                        summary.ReservedInternal.TotalForms = studenList.Where(s => s.IsSVTStudent == true && s.Category.ToUpper() == "RESERVED").Count();
                        summary.ReservedInternal.MaxPercent = studenList.Where(s => s.IsSVTStudent == true && s.Category.ToUpper() == "RESERVED").Max(s => s.Percentage).ToDouble();
                        summary.ReservedInternal.MinPercent = studenList.Where(s => s.IsSVTStudent == true && s.Category.ToUpper() == "RESERVED").Min(s => s.Percentage).ToDouble();
                        summary.ReservedInternal.AvgPercent = studenList.Where(s => s.IsSVTStudent == true && s.Category.ToUpper() == "RESERVED").Average(s => s.Percentage).ToDouble();

                        summary.ReservedExternal = new ReservedExternal();
                        summary.ReservedExternal.TotalForms = studenList.Where(s => (s.IsSVTStudent == false || s.IsSVTStudent == null) && s.Category.ToUpper() == "RESERVED").Count();
                        summary.ReservedExternal.MaxPercent = studenList.Where(s => (s.IsSVTStudent == false || s.IsSVTStudent == null) && s.Category.ToUpper() == "RESERVED").Max(s => s.Percentage).ToDouble();
                        summary.ReservedExternal.MinPercent = studenList.Where(s => (s.IsSVTStudent == false || s.IsSVTStudent == null) && s.Category.ToUpper() == "RESERVED").Min(s => s.Percentage).ToDouble();
                        summary.ReservedExternal.AvgPercent = studenList.Where(s => (s.IsSVTStudent == false || s.IsSVTStudent == null) && s.Category.ToUpper() == "RESERVED").Average(s => s.Percentage).ToDouble();

                        summary.OpenExternal = new OpenExternal();
                        summary.OpenExternal.TotalForms = studenList.Where(s => (s.IsSVTStudent == false || s.IsSVTStudent == null) && s.Category.ToUpper() == "OPEN").Count();
                        summary.OpenExternal.MaxPercent = studenList.Where(s => (s.IsSVTStudent == false || s.IsSVTStudent == null) && s.Category.ToUpper() == "OPEN").Max(s => s.Percentage).ToDouble();
                        summary.OpenExternal.MinPercent = studenList.Where(s => (s.IsSVTStudent == false || s.IsSVTStudent == null) && s.Category.ToUpper() == "OPEN").Min(s => s.Percentage).ToDouble();
                        summary.OpenExternal.AvgPercent = studenList.Where(s => (s.IsSVTStudent == false || s.IsSVTStudent == null) && s.Category.ToUpper() == "OPEN").Average(s => s.Percentage).ToDouble();

                        summary.Specialisation = new List<Specialisation>();
                        List<SeatsList> seatList = new List<SeatsList>();
                        if (File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/data/pdf/Seats.json")))
                        {
                            String JSONtxt = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/data/pdf/Seats.json"));
                            seatList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SeatsList>>(JSONtxt);
                        }
                        foreach (SeatsList seat in seatList)
                        {
                            Specialisation sp = new Specialisation();
                            sp.Name = seat.specialisation;

                            sp.OpenInternal = new OpenInternal();
                            sp.OpenInternal.TotalForms = studenList.Where(s => s.IsSVTStudent == true && s.Category.ToUpper() == "OPEN" && s.CoursePreference1.Equals(seat.specialisation)).Count();
                            sp.OpenInternal.MaxPercent = studenList.Where(s => s.IsSVTStudent == true && s.Category.ToUpper() == "OPEN" && s.CoursePreference1.Equals(seat.specialisation)).Max(s => s.Percentage).ToDouble();
                            sp.OpenInternal.MinPercent = studenList.Where(s => s.IsSVTStudent == true && s.Category.ToUpper() == "OPEN" && s.CoursePreference1.Equals(seat.specialisation)).Min(s => s.Percentage).ToDouble();
                            sp.OpenInternal.AvgPercent = studenList.Where(s => s.IsSVTStudent == true && s.Category.ToUpper() == "OPEN" && s.CoursePreference1.Equals(seat.specialisation)).Average(s => s.Percentage).ToDouble();

                            sp.ReservedInternal = new ReservedInternal();
                            sp.ReservedInternal.TotalForms = studenList.Where(s => s.IsSVTStudent == true && s.Category.ToUpper() == "RESERVED" && s.CoursePreference1.Equals(seat.specialisation)).Count();
                            sp.ReservedInternal.MaxPercent = studenList.Where(s => s.IsSVTStudent == true && s.Category.ToUpper() == "RESERVED" && s.CoursePreference1.Equals(seat.specialisation)).Max(s => s.Percentage).ToDouble();
                            sp.ReservedInternal.MinPercent = studenList.Where(s => s.IsSVTStudent == true && s.Category.ToUpper() == "RESERVED" && s.CoursePreference1.Equals(seat.specialisation)).Min(s => s.Percentage).ToDouble();
                            sp.ReservedInternal.AvgPercent = studenList.Where(s => s.IsSVTStudent == true && s.Category.ToUpper() == "RESERVED" && s.CoursePreference1.Equals(seat.specialisation)).Average(s => s.Percentage).ToDouble();

                            sp.ReservedExternal = new ReservedExternal();
                            sp.ReservedExternal.TotalForms = studenList.Where(s => (s.IsSVTStudent == false || s.IsSVTStudent == null) && s.Category.ToUpper() == "RESERVED" && s.CoursePreference1.Equals(seat.specialisation)).Count();
                            sp.ReservedExternal.MaxPercent = studenList.Where(s => (s.IsSVTStudent == false || s.IsSVTStudent == null) && s.Category.ToUpper() == "RESERVED" && s.CoursePreference1.Equals(seat.specialisation)).Max(s => s.Percentage).ToDouble();
                            sp.ReservedExternal.MinPercent = studenList.Where(s => (s.IsSVTStudent == false || s.IsSVTStudent == null) && s.Category.ToUpper() == "RESERVED" && s.CoursePreference1.Equals(seat.specialisation)).Min(s => s.Percentage).ToDouble();
                            sp.ReservedExternal.AvgPercent = studenList.Where(s => (s.IsSVTStudent == false || s.IsSVTStudent == null) && s.Category.ToUpper() == "RESERVED" && s.CoursePreference1.Equals(seat.specialisation)).Average(s => s.Percentage).ToDouble();

                            sp.OpenExternal = new OpenExternal();
                            sp.OpenExternal.TotalForms = studenList.Where(s => (s.IsSVTStudent == false || s.IsSVTStudent == null) && s.Category.ToUpper() == "OPEN" && s.CoursePreference1.Equals(seat.specialisation)).Count();
                            sp.OpenExternal.MaxPercent = studenList.Where(s => (s.IsSVTStudent == false || s.IsSVTStudent == null) && s.Category.ToUpper() == "OPEN" && s.CoursePreference1.Equals(seat.specialisation)).Max(s => s.Percentage).ToDouble();
                            sp.OpenExternal.MinPercent = studenList.Where(s => (s.IsSVTStudent == false || s.IsSVTStudent == null) && s.Category.ToUpper() == "OPEN" && s.CoursePreference1.Equals(seat.specialisation)).Min(s => s.Percentage).ToDouble();
                            sp.OpenExternal.AvgPercent = studenList.Where(s => (s.IsSVTStudent == false || s.IsSVTStudent == null) && s.Category.ToUpper() == "OPEN" && s.CoursePreference1.Equals(seat.specialisation)).Average(s => s.Percentage).ToDouble();

                            summary.Specialisation.Add(sp);
                        }

                    }

                }
                summary.IsSuccess = true;
                summary.SuccessMessage = "Success";
                return Request.CreateResponse(HttpStatusCode.OK, summary);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetCuttOffPreview(string specialisation)
        {
            //string specialisation = "Food, Nutrition and Dietetics";
            DataTable dt = GetCutOffPercent(true, true, specialisation);
            HttpResponseMessage response = new HttpResponseMessage();
            string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/"));
            string fileName = "Preview_Cutt_Off_MeritList_Report_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
            string fullPath = folderPath + fileName;
            if (dt != null)
            {
                if (SVT.API.Helpers.xmlHeaderHelper.BuildWorkbook(fullPath, dt, "Preview CuttOff"))
                {
                    FileStream fileStream = File.Open(fullPath, FileMode.Open);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StreamContent(fileStream);
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = fileName;
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
                    response.Content.Headers.ContentLength = fileStream.Length;
                }

                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Datatable is empty");
            }
        }

        [HttpGet]
        public HttpResponseMessage GetPreviewMeritListReport(bool isSVT, string category, string round)
        {
            FileStream fileStream = null;
            try
            {
                string Reportname = "Preview Merit List Report";
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                DataTable dt = null;
                HttpResponseMessage response = new HttpResponseMessage();
                SVT.API.Helpers.xmlHeaderHelper xmlHeaderHelper = new SVT.API.Helpers.xmlHeaderHelper();
                bool isTrue = false;

                fileName = "PreviewMeritList_Report_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                fullPath = folderPath + fileName;
                //HttpResponseMessage res = PreviewMeritList(isSVT, category, round);
                dt = PreviewMeritListReport(isSVT, category, round);

                if (dt != null)
                {
                    isTrue = SVT.API.Helpers.xmlHeaderHelper.BuildWorkbook(fullPath, dt, Reportname);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Datatable is empty");
                }
                if (isTrue)
                {
                    //ValidateExcelDocument(fullPath);
                    fileStream = File.Open(fullPath, FileMode.Open);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Content = new StreamContent(fileStream);
                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                    response.Content.Headers.ContentDisposition.FileName = fileName;
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/ms-excel");
                    response.Content.Headers.ContentLength = fileStream.Length;
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Issue with the excel file generation");
                }
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable PreviewMeritListReport(bool isSVT, string category, string round)
        {
            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(category) || (category.ToUpper() != "OPEN" && category.ToUpper() != "RESERVED")
                || string.IsNullOrEmpty(round) || (round.ToUpper() != "ROUND1" && round.ToUpper() != "ROUND2" && round.ToUpper() != "ROUND3"))
            {
                return dt;
            }

            var previewList = GetPreviewList(isSVT, category, round);
            dt = ListtoDataTable.ToDataTable(previewList);
            return dt;
        }

        public DataTable PreviewMeritListReport1(bool isSVT, string category, string round)
        {
            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(category) || (category.ToUpper() != "OPEN" && category.ToUpper() != "RESERVED")
                || string.IsNullOrEmpty(round) || (round.ToUpper() != "ROUND1" && round.ToUpper() != "ROUND2" && round.ToUpper() != "ROUND3"))
            {
                return dt;
            }

            var roundlist = new List<RoundList>();
            //Get Seats json list item
            var seatsList = ReadSeatsFile();

            //Get Round json list item
            roundlist = ReadRoundFile(round);
            List<PreviewStudentDetails> listOfStudents = new List<PreviewStudentDetails>();
            int sr = 0;


            if (seatsList != null && seatsList.Count > 1 && roundlist != null && roundlist.Count > 1)
            {
                #region Internal Open
                try
                {
                    //Get Student List
                    isSVT = true;
                    category = "OPEN";
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);


                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"

                            bool isAdmittedinCurrentRound = false;
                            string lastAdmittedCourse = string.Empty;
                            bool leaveStudent = false;
                            if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"

                            bool SVTOpenInternal = false;
                            bool SVTReservedInternal = false;
                            bool ExternalOpen = false;
                            bool ExternalReserved = false;
                            //Get Categoy and SVT student wise but value
                            if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                            {
                                SVTOpenInternal = true;
                            }
                            else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                            {
                                ExternalOpen = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                            {
                                SVTReservedInternal = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                            {
                                ExternalReserved = true;
                            }


                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                            {
                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                //UpdateSeatData(seatsList);
                                if (listOfStudents.Count > 0)
                                    sr = listOfStudents.Last().SerialNumber;
                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                st.SerialNumber = sr + 1;
                                st.PossibleCourseAdmitted = student.CoursePreference1;
                                listOfStudents.Add(st);

                            }
                            else if (leaveStudent == false)
                            {

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                {
                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                    //UpdateSeatData(seatsList);
                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference2;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                    {
                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                        //UpdateSeatData(seatsList);
                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference3;
                                        listOfStudents.Add(st);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                        {
                                            //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                            //UpdateSeatData(seatsList);
                                            if (listOfStudents.Count > 0)
                                                sr = listOfStudents.Last().SerialNumber;
                                            PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                            st.SerialNumber = sr + 1;
                                            st.PossibleCourseAdmitted = student.CoursePreference4;
                                            listOfStudents.Add(st);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                            {
                                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                //UpdateSeatData(seatsList);
                                                if (listOfStudents.Count > 0)
                                                    sr = listOfStudents.Last().SerialNumber;
                                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                st.SerialNumber = sr + 1;
                                                st.PossibleCourseAdmitted = student.CoursePreference5;
                                                listOfStudents.Add(st);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                                {
                                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                    //UpdateSeatData(seatsList);
                                                    if (listOfStudents.Count > 0)
                                                        sr = listOfStudents.Last().SerialNumber;
                                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                    st.SerialNumber = sr + 1;
                                                    st.PossibleCourseAdmitted = student.CoursePreference6;
                                                    listOfStudents.Add(st);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                                    {
                                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                        //UpdateSeatData(seatsList);
                                                        if (listOfStudents.Count > 0)
                                                            sr = listOfStudents.Last().SerialNumber;
                                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                        st.SerialNumber = sr + 1;
                                                        st.PossibleCourseAdmitted = student.CoursePreference7;
                                                        listOfStudents.Add(st);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        string returnMessage = string.Empty;
                        #region "Transfer Seat"
                        seatsList.Where(w => w.SVTOpenInternal > 0).ToList().ForEach(i => { i.ExternalOpen = i.ExternalOpen + i.SVTOpenInternal; i.SVTOpenInternal = 0; });
                        //UpdateSeatData(seatsList);
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion

                #region External Open
                try
                {
                    //Get Student List
                    isSVT = false;
                    category = "OPEN";
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);


                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"

                            bool isAdmittedinCurrentRound = false;
                            string lastAdmittedCourse = string.Empty;
                            bool leaveStudent = false;
                            if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"

                            bool SVTOpenInternal = false;
                            bool SVTReservedInternal = false;
                            bool ExternalOpen = false;
                            bool ExternalReserved = false;
                            //Get Categoy and SVT student wise but value
                            if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                            {
                                SVTOpenInternal = true;
                            }
                            else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                            {
                                ExternalOpen = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                            {
                                SVTReservedInternal = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                            {
                                ExternalReserved = true;
                            }


                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                            {
                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                //UpdateSeatData(seatsList);
                                if (listOfStudents.Count > 0)
                                    sr = listOfStudents.Last().SerialNumber;
                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                st.SerialNumber = sr + 1;
                                st.PossibleCourseAdmitted = student.CoursePreference1;
                                listOfStudents.Add(st);

                            }
                            else if (leaveStudent == false)
                            {

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                {
                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                    //UpdateSeatData(seatsList);
                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference2;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                    {
                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                        //UpdateSeatData(seatsList);
                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference3;
                                        listOfStudents.Add(st);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                        {
                                            //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                            //UpdateSeatData(seatsList);
                                            if (listOfStudents.Count > 0)
                                                sr = listOfStudents.Last().SerialNumber;
                                            PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                            st.SerialNumber = sr + 1;
                                            st.PossibleCourseAdmitted = student.CoursePreference4;
                                            listOfStudents.Add(st);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                            {
                                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                //UpdateSeatData(seatsList);
                                                if (listOfStudents.Count > 0)
                                                    sr = listOfStudents.Last().SerialNumber;
                                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                st.SerialNumber = sr + 1;
                                                st.PossibleCourseAdmitted = student.CoursePreference5;
                                                listOfStudents.Add(st);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                                {
                                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                    //UpdateSeatData(seatsList);
                                                    if (listOfStudents.Count > 0)
                                                        sr = listOfStudents.Last().SerialNumber;
                                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                    st.SerialNumber = sr + 1;
                                                    st.PossibleCourseAdmitted = student.CoursePreference6;
                                                    listOfStudents.Add(st);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                                    {
                                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                        //UpdateSeatData(seatsList);
                                                        if (listOfStudents.Count > 0)
                                                            sr = listOfStudents.Last().SerialNumber;
                                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                        st.SerialNumber = sr + 1;
                                                        st.PossibleCourseAdmitted = student.CoursePreference7;
                                                        listOfStudents.Add(st);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        string returnMessage = string.Empty;
                        #region "Transfer Seat"
                        seatsList.Where(w => w.ExternalOpen > 0).ToList().ForEach(i => { i.SVTReservedInternal = i.ExternalOpen + i.SVTReservedInternal; i.ExternalOpen = 0; });
                        //UpdateSeatData(seatsList);
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion

                #region Internal Reserved
                try
                {
                    //Get Student List
                    isSVT = true;
                    category = "RESERVED";
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);


                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"

                            bool isAdmittedinCurrentRound = false;
                            string lastAdmittedCourse = string.Empty;
                            bool leaveStudent = false;
                            if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"

                            bool SVTOpenInternal = false;
                            bool SVTReservedInternal = false;
                            bool ExternalOpen = false;
                            bool ExternalReserved = false;
                            //Get Categoy and SVT student wise but value
                            if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                            {
                                SVTOpenInternal = true;
                            }
                            else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                            {
                                ExternalOpen = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                            {
                                SVTReservedInternal = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                            {
                                ExternalReserved = true;
                            }


                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                            {
                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                //UpdateSeatData(seatsList);
                                if (listOfStudents.Count > 0)
                                    sr = listOfStudents.Last().SerialNumber;
                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                st.SerialNumber = sr + 1;
                                st.PossibleCourseAdmitted = student.CoursePreference1;
                                listOfStudents.Add(st);

                            }
                            else if (leaveStudent == false)
                            {

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                {
                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                    //UpdateSeatData(seatsList);
                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference2;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                    {
                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                        //UpdateSeatData(seatsList);
                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference3;
                                        listOfStudents.Add(st);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                        {
                                            //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                            //UpdateSeatData(seatsList);
                                            if (listOfStudents.Count > 0)
                                                sr = listOfStudents.Last().SerialNumber;
                                            PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                            st.SerialNumber = sr + 1;
                                            st.PossibleCourseAdmitted = student.CoursePreference4;
                                            listOfStudents.Add(st);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                            {
                                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                //UpdateSeatData(seatsList);
                                                if (listOfStudents.Count > 0)
                                                    sr = listOfStudents.Last().SerialNumber;
                                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                st.SerialNumber = sr + 1;
                                                st.PossibleCourseAdmitted = student.CoursePreference5;
                                                listOfStudents.Add(st);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                                {
                                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                    //UpdateSeatData(seatsList);
                                                    if (listOfStudents.Count > 0)
                                                        sr = listOfStudents.Last().SerialNumber;
                                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                    st.SerialNumber = sr + 1;
                                                    st.PossibleCourseAdmitted = student.CoursePreference6;
                                                    listOfStudents.Add(st);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                                    {
                                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                        //UpdateSeatData(seatsList);
                                                        if (listOfStudents.Count > 0)
                                                            sr = listOfStudents.Last().SerialNumber;
                                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                        st.SerialNumber = sr + 1;
                                                        st.PossibleCourseAdmitted = student.CoursePreference7;
                                                        listOfStudents.Add(st);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        string returnMessage = string.Empty;
                        #region "Transfer Seat"
                        seatsList.Where(w => w.SVTReservedInternal > 0).ToList().ForEach(i => { i.ExternalReserved = i.ExternalReserved + i.SVTReservedInternal; i.SVTReservedInternal = 0; });
                        //UpdateSeatData(seatsList);
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion

                #region Internal Reserved
                try
                {
                    //Get Student List
                    isSVT = false;
                    category = "RESERVED";
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);


                        foreach (var student in studenList)
                        {
                            #region "Check Student already allocated anyhow then continue to other student"

                            bool isAdmittedinCurrentRound = false;
                            string lastAdmittedCourse = string.Empty;
                            bool leaveStudent = false;
                            if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                            {
                                isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                lastAdmittedCourse = student.CourseAdmittedRound3;
                            }
                            else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound2;
                            }
                            else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                            {
                                isAdmittedinCurrentRound = false;
                                lastAdmittedCourse = student.CourseAdmittedRound1;
                            }
                            if (isAdmittedinCurrentRound)
                            {
                                continue;
                            }

                            #endregion

                            #region "Check Seat, percentage and allocate seat"

                            bool SVTOpenInternal = false;
                            bool SVTReservedInternal = false;
                            bool ExternalOpen = false;
                            bool ExternalReserved = false;
                            //Get Categoy and SVT student wise but value
                            if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                            {
                                SVTOpenInternal = true;
                            }
                            else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                            {
                                ExternalOpen = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                            {
                                SVTReservedInternal = true;
                            }
                            else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                            {
                                ExternalReserved = true;
                            }


                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                            {
                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                //UpdateSeatData(seatsList);
                                if (listOfStudents.Count > 0)
                                    sr = listOfStudents.Last().SerialNumber;
                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                st.SerialNumber = sr + 1;
                                st.PossibleCourseAdmitted = student.CoursePreference1;
                                listOfStudents.Add(st);

                            }
                            else if (leaveStudent == false)
                            {

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                {
                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference2, round);
                                    //UpdateSeatData(seatsList);
                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference2;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference3, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                    {
                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference3, round);
                                        //UpdateSeatData(seatsList);
                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference3;
                                        listOfStudents.Add(st);
                                    }
                                    else if (leaveStudent == false)
                                    {
                                        if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference4, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                        {
                                            //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference4, round);
                                            //UpdateSeatData(seatsList);
                                            if (listOfStudents.Count > 0)
                                                sr = listOfStudents.Last().SerialNumber;
                                            PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                            st.SerialNumber = sr + 1;
                                            st.PossibleCourseAdmitted = student.CoursePreference4;
                                            listOfStudents.Add(st);
                                        }
                                        else if (leaveStudent == false)
                                        {
                                            if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference5, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                            {
                                                //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference5, round);
                                                //UpdateSeatData(seatsList);
                                                if (listOfStudents.Count > 0)
                                                    sr = listOfStudents.Last().SerialNumber;
                                                PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                st.SerialNumber = sr + 1;
                                                st.PossibleCourseAdmitted = student.CoursePreference5;
                                                listOfStudents.Add(st);
                                            }
                                            else if (leaveStudent == false)
                                            {
                                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference6, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                                {
                                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference6, round);
                                                    //UpdateSeatData(seatsList);
                                                    if (listOfStudents.Count > 0)
                                                        sr = listOfStudents.Last().SerialNumber;
                                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                    st.SerialNumber = sr + 1;
                                                    st.PossibleCourseAdmitted = student.CoursePreference6;
                                                    listOfStudents.Add(st);
                                                }
                                                else if (leaveStudent == false)
                                                {
                                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference7, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                                    {
                                                        //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference7, round);
                                                        //UpdateSeatData(seatsList);
                                                        if (listOfStudents.Count > 0)
                                                            sr = listOfStudents.Last().SerialNumber;
                                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                                        st.SerialNumber = sr + 1;
                                                        st.PossibleCourseAdmitted = student.CoursePreference7;
                                                        listOfStudents.Add(st);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            #endregion
                        }

                        string returnMessage = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion
                dt = ListtoDataTable.ToDataTable(listOfStudents);
                return dt;
            }
            else
            {
                return dt;
            }
        }

        public class ListtoDataTable
        {
            public static DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);

                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Defining type of data column gives proper data table 
                    var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name, type);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
        }

        public class RoundList
        {
            public string specialisation { get; set; }
            public decimal SVTOpenInternal { get; set; }
            public decimal SVTReservedInternal { get; set; }
            public decimal ExternalOpen { get; set; }
            public decimal ExternalReserved { get; set; }
        }

        public class SeatsList
        {
            public string specialisation { get; set; }
            public int SVTOpenInternal { get; set; }
            public int SVTReservedInternal { get; set; }
            public int ExternalOpen { get; set; }
            public int ExternalReserved { get; set; }
            public int Total { get; set; }
        }

        public class ReservationSeatList
        {
            public string specialisation { get; set; }
            public ReservationCategory internalCategory { get; set; }
            public ReservationCategory externalCategory { get; set; }
        }

        public class ReservationCategory
        {
            public int sc { get; set; }
            public int st { get; set; }
            public int vj { get; set; }
            public int nt1 { get; set; }
            public int nt2 { get; set; }
            public int nt3 { get; set; }
            public int obc { get; set; }
            public int sebc { get; set; }
            public int ebc { get; set; }
            public int sbc { get; set; }
        }

        public async Task<HttpResponseMessage> GetPDF(string id)
        {
            List<string> newPaths = new List<string>();
            ServiceContext service = new ServiceContext();
            var studentData = service.SelectObject<StudentDetail>(int.Parse(id));
            DownloadPDF2(studentData);
            string path = "data/PDF/" + id + "_" + studentData.FirstName + "_" + studentData.LastName + "_" + Convert.ToDateTime(studentData.DateRegistered).ToString("ddmmyyyy_hhmmss") + ".pdf" + ":~" + id;
            newPaths.Add(path);
            return Request.CreateResponse(HttpStatusCode.OK, newPaths);

        }

        public static void DownloadPDF2(StudentDetail studentDetailModel)
        {
            string HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/PDFHtml.html"));

            if (studentDetailModel != null)
            {
                #region Form Detail
                if (!string.IsNullOrEmpty(studentDetailModel.MKCLFormNumber) && !(studentDetailModel.MKCLFormNumber.Trim().Equals("0")))
                    HTMLContent = HTMLContent.Replace("{{ MKCLno }}", studentDetailModel.MKCLFormNumber);
                else
                    HTMLContent = HTMLContent.Replace("{{ MKCLno }}", "");
                HTMLContent = HTMLContent.Replace("{{ Fno }}", studentDetailModel.Id.ToString());
                HTMLContent = HTMLContent.Replace("{{ Surname }}", studentDetailModel.LastName);
                HTMLContent = HTMLContent.Replace("{{ Firstname }}", studentDetailModel.FirstName);
                HTMLContent = HTMLContent.Replace("{{ Middlename }}", studentDetailModel.FatherName);
                HTMLContent = HTMLContent.Replace("{{ Mothername }}", studentDetailModel.MotherName);

                // update it
                HTMLContent = HTMLContent.Replace("{{ Specialization }}", "");
                HTMLContent = HTMLContent.Replace("{{ Incharge }}", "");

                HTMLContent = HTMLContent.Replace("{{ Date }}", studentDetailModel.DateRegistered.ToString());
                if (studentDetailModel.IsSVTStudent != null && studentDetailModel.IsSVTStudent.Value)
                {
                    HTMLContent = HTMLContent.Replace("{{ IsCol }}", "YES");
                    HTMLContent = HTMLContent.Replace("{{ IsOtherCol }}", " ");
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ IsCol }}", " ");
                    HTMLContent = HTMLContent.Replace("{{ IsOtherCol }}", "YES");
                }

                #endregion

                #region Set Photo Path

                string ImagePath = string.Empty;
                string Image = string.Empty;

                if (studentDetailModel.Photo != null)
                {
                    ImagePath = ProjectConfiguration.PhotoPath + studentDetailModel.Photo;
                    using (System.Drawing.Image image = System.Drawing.Image.FromFile(ImagePath))
                    {
                        using (MemoryStream m = new MemoryStream())
                        {
                            image.Save(m, image.RawFormat);
                            byte[] imageBytes = m.ToArray();
                            Image = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(imageBytes));
                        }
                    }

                    HTMLContent = HTMLContent.Replace("{{ Image }}", ImagePath);
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ Image }}", "");
                }
                #endregion

                #region Set signature

                ImagePath = string.Empty;

                if (studentDetailModel.SignaturePath != null)
                {
                    ImagePath = ProjectConfiguration.SignaturePath + studentDetailModel.SignaturePath;
                    using (System.Drawing.Image image = System.Drawing.Image.FromFile(ImagePath))
                    {
                        using (MemoryStream m = new MemoryStream())
                        {
                            image.Save(m, image.RawFormat);
                            byte[] imageBytes = m.ToArray();
                            Image = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(imageBytes));
                        }
                    }
                    HTMLContent = HTMLContent.Replace("{{ Sign }}", ImagePath);
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ Sign }}", "");
                }
                #endregion

                #region Set preference

                string preference = string.Empty;

                preference += @"<tr><td style='width:95%;'>1." + studentDetailModel.CoursePreference1 + "</td></tr>";
                preference += @"<tr><td style='width:95%;'>2." + studentDetailModel.CoursePreference2 + "</td></tr>";
                preference += @"<tr><td style='width:95%;'>3." + studentDetailModel.CoursePreference3 + "</td></tr>";
                preference += @"<tr><td style='width:95%;'>4." + studentDetailModel.CoursePreference4 + "</td></tr>";
                preference += @"<tr><td style='width:95%;'>5." + studentDetailModel.CoursePreference5 + "</td></tr>";
                preference += @"<tr><td style='width:95%;'>6." + studentDetailModel.CoursePreference6 + "</td></tr>";

                if (studentDetailModel.HscStream.ToLower() == "science")
                {
                    preference += @"<tr><td style='width:95%;'>7." + studentDetailModel.CoursePreference7 + "</td></tr>";
                }

                HTMLContent = HTMLContent.Replace("{{ Preference }}", preference);

                #endregion

                HTMLContent = HTMLContent.Replace("{{ ExamPassMonthYear }}", studentDetailModel.YearLastExamPassed.ToString());
                HTMLContent = HTMLContent.Replace("{{ ExamBoard }}", studentDetailModel.NameofExaminationBoard);
                HTMLContent = HTMLContent.Replace("{{ Stream }}", studentDetailModel.HscStream);
                HTMLContent = HTMLContent.Replace("{{ Attempt }}", studentDetailModel.AttemptofHSC);
                HTMLContent = HTMLContent.Replace("{{ FullName }}", studentDetailModel.LastName + " " + studentDetailModel.FirstName + " " + studentDetailModel.FatherName + " " + studentDetailModel.MotherName);
                HTMLContent = HTMLContent.Replace("{{ PreviousCollegeName }}", studentDetailModel.LastSchoolAttend);
                HTMLContent = HTMLContent.Replace("{{ LastExaminationBoard }}", studentDetailModel.NameofExaminationBoard);

                #region Set Subject Detail

                int sub1Total, sub2Total, sub3Total, sub4Total, sub5Total, sub6Total, sub7Total = 0;
                int.TryParse(studentDetailModel.Subject1Total.ToString().Replace(".000", ""), out sub1Total);
                int.TryParse(studentDetailModel.Subject2Total.ToString().Replace(".000", ""), out sub2Total);
                int.TryParse(studentDetailModel.Subject3Total.ToString().Replace(".000", ""), out sub3Total);
                int.TryParse(studentDetailModel.Subject4Total.ToString().Replace(".000", ""), out sub4Total);
                int.TryParse(studentDetailModel.Subject5Total.ToString().Replace(".000", ""), out sub5Total);
                int.TryParse(studentDetailModel.Subject6Total.ToString().Replace(".000", ""), out sub6Total);
                int.TryParse(studentDetailModel.Subject7Total.ToString().Replace(".000", ""), out sub7Total);

                HTMLContent = HTMLContent.Replace("{{ Sub1 }}", studentDetailModel.Subject1Name);
                HTMLContent = HTMLContent.Replace("{{ Sub2 }}", studentDetailModel.Subject2Name);
                HTMLContent = HTMLContent.Replace("{{ Sub3 }}", studentDetailModel.Subject3Name);
                HTMLContent = HTMLContent.Replace("{{ Sub4 }}", studentDetailModel.Subject4Name);
                HTMLContent = HTMLContent.Replace("{{ Sub5 }}", studentDetailModel.Subject5Name);
                HTMLContent = HTMLContent.Replace("{{ Sub6 }}", studentDetailModel.Subject6Name);
                HTMLContent = HTMLContent.Replace("{{ Sub7 }}", studentDetailModel.Subject7Name);
                HTMLContent = HTMLContent.Replace("{{ Sub1From }}", sub1Total.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub2From }}", sub2Total.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub3From }}", sub3Total.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub4From }}", sub4Total.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub5From }}", sub5Total.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub6From }}", sub6Total.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub7From }}", sub7Total.ToString());

                int total = 0;

                total = int.Parse(studentDetailModel.Subject1Total.ToString().Replace(".000", "")) +
                        int.Parse(studentDetailModel.Subject2Total.ToString().Replace(".000", "")) +
                        int.Parse(studentDetailModel.Subject3Total.ToString().Replace(".000", "")) +
                        int.Parse(studentDetailModel.Subject4Total.ToString().Replace(".000", "")) +
                        int.Parse(studentDetailModel.Subject5Total.ToString().Replace(".000", "")) +
                        int.Parse(studentDetailModel.Subject6Total.ToString().Replace(".000", "")) +
                        int.Parse(studentDetailModel.Subject7Total.ToString().Replace(".000", ""));

                string t = (total == 0) ? studentDetailModel.TotalMarks.ToString() : total.ToString();
                HTMLContent = HTMLContent.Replace("{{ TotalFrom }}", t);

                int sub1Marks, sub2Marks, sub3Marks, sub4Marks, sub5Marks, sub6Marks, sub7Marks = 0;
                int.TryParse(studentDetailModel.Subject1MarksObtained.ToString().Replace(".000", ""), out sub1Marks);
                int.TryParse(studentDetailModel.Subject2MarksObtained.ToString().Replace(".000", ""), out sub2Marks);
                int.TryParse(studentDetailModel.Subject3MarksObtained.ToString().Replace(".000", ""), out sub3Marks);
                int.TryParse(studentDetailModel.Subject4MarksObtained.ToString().Replace(".000", ""), out sub4Marks);
                int.TryParse(studentDetailModel.Subject5MarksObtained.ToString().Replace(".000", ""), out sub5Marks);
                int.TryParse(studentDetailModel.Subject6MarksObtained.ToString().Replace(".000", ""), out sub6Marks);
                int.TryParse(studentDetailModel.Subject7MarksObtained.ToString().Replace(".000", ""), out sub7Marks);

                HTMLContent = HTMLContent.Replace("{{ TotalPerFrom }}", "100%");
                HTMLContent = HTMLContent.Replace("{{ Sub1Mark }}", sub1Marks.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub2Mark }}", sub2Marks.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub3Mark }}", sub3Marks.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub4Mark }}", sub4Marks.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub5Mark }}", sub5Marks.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub6Mark }}", sub6Marks.ToString());
                HTMLContent = HTMLContent.Replace("{{ Sub7Mark }}", sub7Marks.ToString());
                HTMLContent = HTMLContent.Replace("{{ TotalMark }}", studentDetailModel.MarksObtain.ToString());
                HTMLContent = HTMLContent.Replace("{{ TotalPerMark }}", studentDetailModel.Percentage.ToString());

                #endregion

                HTMLContent = HTMLContent.Replace("{{ AcademicHonors }}", "");
                HTMLContent = HTMLContent.Replace("{{ FLName }}", studentDetailModel.FatherLastName);
                HTMLContent = HTMLContent.Replace("{{ FFName }}", studentDetailModel.FatherFirstName);
                HTMLContent = HTMLContent.Replace("{{ FMName }}", studentDetailModel.FatherMiddleName);
                HTMLContent = HTMLContent.Replace("{{ MLName }}", studentDetailModel.MotherLastName);
                HTMLContent = HTMLContent.Replace("{{ MFName }}", studentDetailModel.MotherFirstName);
                HTMLContent = HTMLContent.Replace("{{ MMName }}", studentDetailModel.MotherMiddleName);

                #region Set Category

                HTMLContent = HTMLContent.Replace("{{ Religion }}", studentDetailModel.Religion);
                if (studentDetailModel.Category.ToLower().Equals("open"))
                {
                    HTMLContent = HTMLContent.Replace("{{ Category }}", "Open");
                    HTMLContent = HTMLContent.Replace("{{ Reservation }}", " ");
                    HTMLContent = HTMLContent.Replace("{{ SubCaste }}", " ");
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ Category }}", "Reserved");
                    HTMLContent = HTMLContent.Replace("{{ Caste }}", studentDetailModel.Caste);
                    HTMLContent = HTMLContent.Replace("{{ SubCaste }}", studentDetailModel.SubCaste);
                }
                HTMLContent = HTMLContent.Replace("{{ Caste }}", " ");

                #endregion

                #region Personal Detail

                HTMLContent = HTMLContent.Replace("{{ MaritalStatus }}", studentDetailModel.MaritalStatus);
                HTMLContent = HTMLContent.Replace("{{ BirthPlace }}", studentDetailModel.PlaceofBirth);
                HTMLContent = HTMLContent.Replace("{{ BloodGroup }}", studentDetailModel.BloodGroup);
                HTMLContent = HTMLContent.Replace("{{ CountryName }}", studentDetailModel.Nationality);
                HTMLContent = HTMLContent.Replace("{{ AadharCard }}", studentDetailModel.AadharNumber);


                if (studentDetailModel.EmploymentStatus == 1)
                {
                    HTMLContent = HTMLContent.Replace("{{ EmploymentStatus }}", "Employed");
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ EmploymentStatus }}", "Unemployed");
                }

                if (studentDetailModel.IsLearningDisability == true)
                {
                    HTMLContent = HTMLContent.Replace("{{ LearningDisability }}", "Yes");
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ LearningDisability }}", "No");
                }

                HTMLContent = HTMLContent.Replace("{{ Address1 }}", studentDetailModel.CurrentAddress);
                HTMLContent = HTMLContent.Replace("{{ Address2 }}", studentDetailModel.PermanentAddress);
                HTMLContent = HTMLContent.Replace("{{ Occupation }}", studentDetailModel.GuardianOccupation);
                HTMLContent = HTMLContent.Replace("{{ ETelNum }}", studentDetailModel.EmergencyTel.ToString());
                HTMLContent = HTMLContent.Replace("{{ MNum }}", studentDetailModel.MobileNumber);
                HTMLContent = HTMLContent.Replace("{{ Email }}", studentDetailModel.Email);
                HTMLContent = HTMLContent.Replace("{{ MTongue }}", studentDetailModel.MotherTongue);
                HTMLContent = HTMLContent.Replace("{{ WNCC }}", studentDetailModel.WishtojoinNCC.ToString());
                HTMLContent = HTMLContent.Replace("{{ GIncome }}", studentDetailModel.GuardianSalary);
                HTMLContent = HTMLContent.Replace("{{ GRelationship }}", studentDetailModel.RelwithGurdian);

                #endregion

                HTMLContent = HTMLContent.Replace("{{ AFirst }}", "");
                HTMLContent = HTMLContent.Replace("{{ ASecond }}", "");
                HTMLContent = HTMLContent.Replace("{{ AThird }}", "");
                HTMLContent = HTMLContent.Replace("{{ AFourth }}", "");
                HTMLContent = HTMLContent.Replace("{{ AFifth }}", "");
                HTMLContent = HTMLContent.Replace("{{ ASixth }}", "");
                HTMLContent = HTMLContent.Replace("{{ ASeventh }}", "");
                HTMLContent = HTMLContent.Replace("{{ AEighth }}", "");
                HTMLContent = HTMLContent.Replace("{{ ANinth }}", "");
                HTMLContent = HTMLContent.Replace("{{ ATenth }}", "");

                #region Set Course Preference Value

                HTMLContent = HTMLContent.Replace("{{ CoursePreference1 }}", studentDetailModel.CoursePreference1);
                HTMLContent = HTMLContent.Replace("{{ CoursePreference2 }}", studentDetailModel.CoursePreference2);
                HTMLContent = HTMLContent.Replace("{{ CoursePreference3 }}", studentDetailModel.CoursePreference3);
                HTMLContent = HTMLContent.Replace("{{ CoursePreference4 }}", studentDetailModel.CoursePreference4);
                HTMLContent = HTMLContent.Replace("{{ CoursePreference5 }}", studentDetailModel.CoursePreference5);
                HTMLContent = HTMLContent.Replace("{{ CoursePreference6 }}", studentDetailModel.CoursePreference6);
                HTMLContent = HTMLContent.Replace("{{ CoursePreference7 }}", studentDetailModel.CoursePreference7);

                HTMLContent = HTMLContent.Replace("{{ Elective1 }}", studentDetailModel.Preference1GE1);
                HTMLContent = HTMLContent.Replace("{{ Elective2 }}", studentDetailModel.Preference2GE1);
                HTMLContent = HTMLContent.Replace("{{ Elective3 }}", studentDetailModel.Preference3GE1);
                HTMLContent = HTMLContent.Replace("{{ Elective4 }}", studentDetailModel.Preference4GE1);
                HTMLContent = HTMLContent.Replace("{{ Elective5 }}", studentDetailModel.Preference5GE1);
                HTMLContent = HTMLContent.Replace("{{ Elective6 }}", studentDetailModel.Preference6GE1);
                HTMLContent = HTMLContent.Replace("{{ Elective7 }}", studentDetailModel.Preference7GE1);

                HTMLContent = HTMLContent.Replace("{{ Elective2_1 }}", studentDetailModel.Preference1GE2);
                HTMLContent = HTMLContent.Replace("{{ Elective2_2 }}", studentDetailModel.Preference2GE2);
                HTMLContent = HTMLContent.Replace("{{ Elective2_3 }}", studentDetailModel.Preference3GE2);
                HTMLContent = HTMLContent.Replace("{{ Elective2_4 }}", studentDetailModel.Preference4GE2);
                HTMLContent = HTMLContent.Replace("{{ Elective2_5 }}", studentDetailModel.Preference5GE2);
                HTMLContent = HTMLContent.Replace("{{ Elective2_6 }}", studentDetailModel.Preference6GE2);
                HTMLContent = HTMLContent.Replace("{{ Elective2_7 }}", studentDetailModel.Preference7GE2);


                HTMLContent = HTMLContent.Replace("{{ CoursePreference1 }}", "");
                HTMLContent = HTMLContent.Replace("{{ CoursePreference2 }}", "");
                HTMLContent = HTMLContent.Replace("{{ CoursePreference3 }}", "");
                HTMLContent = HTMLContent.Replace("{{ CoursePreference4 }}", "");
                HTMLContent = HTMLContent.Replace("{{ CoursePreference5 }}", "");
                HTMLContent = HTMLContent.Replace("{{ CoursePreference6 }}", "");
                HTMLContent = HTMLContent.Replace("{{ CoursePreference7 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective1 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective2 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective3 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective4 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective5 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective6 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective7 }}", "");

                HTMLContent = HTMLContent.Replace("{{ Elective2_1 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective2_2 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective2_3 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective2_4 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective2_5 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective2_6 }}", "");
                HTMLContent = HTMLContent.Replace("{{ Elective2_7 }}", "");

                #endregion
                HTMLContent = HTMLContent.Replace("{{ BirthDate }}", studentDetailModel.Dob.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
                HTMLContent = HTMLContent.Replace("{{ PinCode }}", studentDetailModel.PinCode.ToString());
                HTMLContent = HTMLContent.Replace("{{ DisabilityNumber }}", studentDetailModel.DisabilityNumber.ToString());
                HTMLContent = HTMLContent.Replace("{{ State }}", studentDetailModel.ResidenceState.ToString());
                HTMLContent = HTMLContent.Replace("{{ DisabilityType }}", studentDetailModel.DisabilityType.ToString());
                HTMLContent = HTMLContent.Replace("{{ DisabilityPercentage }}", studentDetailModel.DisabilityPercentage.ToString());

                if (studentDetailModel.IsNRI == true)
                {
                    HTMLContent = HTMLContent.Replace("{{ IsNRI }}", "Yes");
                }
                else
                    HTMLContent = HTMLContent.Replace("{{ IsNRI }}", "No");

                if (studentDetailModel.IsHostelRequired == true)
                {
                    HTMLContent = HTMLContent.Replace("{{ IsHostelRequired }}", "Yes");
                }
                else
                    HTMLContent = HTMLContent.Replace("{{ IsHostelRequired }}", "No");

                if (studentDetailModel.VoterId == true)
                {
                    HTMLContent = HTMLContent.Replace("{{ VoterId }}", "Yes");
                    HTMLContent = HTMLContent.Replace("{{ VoterNumber }}", studentDetailModel.VoterNumber);
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ VoterId }}", "No");
                    HTMLContent = HTMLContent.Replace("{{ VoterNumber }}", studentDetailModel.VoterNumber);
                }

                HTMLContent = HTMLContent.Replace("{{ AboutCollege }}", studentDetailModel.AboutCollege);

                // Populate values in hostel html
                if (studentDetailModel.IsHostelRequired == true)
                {
                    HTMLContent = HTMLContent.Replace("{{ HostelReason }}", studentDetailModel.HostelReason);
                }
            }
            GetPDF(HTMLContent, studentDetailModel.FirstName, studentDetailModel.LastName, studentDetailModel.Id.ToString(), studentDetailModel.DateRegistered);
        }

        [HttpGet]
        public HttpResponseMessage AdmitStudent(string formId, string courseId)
        {
            if (string.IsNullOrEmpty(formId) || string.IsNullOrEmpty(courseId))
            {
                return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Please pass proper parameter" });
            }
            else
            {
                string specialisation = string.Empty;
                switch (courseId)
                {
                    case "1":
                        specialisation = "Developmental Counselling";
                        break;
                    case "2":
                        specialisation = "Early Childhood care & Education";
                        break;
                    case "3":
                        specialisation = "Food, Nutrition and Dietetics";
                        break;
                    case "4":
                        specialisation = "Hospitality & Tourism Management";
                        break;
                    case "5":
                        specialisation = "Interior Design & Resource Management";
                        break;
                    case "6":
                        specialisation = "Mass Communication & Extension";
                        break;
                    case "7":
                        specialisation = "Textiles & Apparel Designing";
                        break;
                    default:
                        specialisation = string.Empty;
                        break;
                }
                if (!string.IsNullOrEmpty(specialisation))
                {

                    string connetionString = null;
                    SqlConnection connection = null;
                    SqlCommand command;
                    string sql = null;
                    try
                    {

                        connetionString = System.Configuration.ConfigurationManager.ConnectionStrings["RCTDBConnection"].ConnectionString;
                        connection = new SqlConnection(connetionString);
                        if (connection.State == ConnectionState.Open)
                            connection.Close();

                        connection.Open();

                        sql = string.Format(" Update StudentDetails set AdmittedRound3=1,CourseAdmittedRound3='{0}',DateAdmittedRound3='{1}',LastModified2='{2}' where Id='{3}'", specialisation, DateTime.Now, DateTime.Now, formId);
                        command = new SqlCommand(sql, connection);
                        command.ExecuteNonQuery();

                        return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = true, SuccessMessage = "Student admitted successfully." });
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new BaseClass() { IsSuccess = false, ErrorMessage = "Bad Request" });
                }

            }
        }

        #region Merit List Reporting
        [HttpGet]
        public HttpResponseMessage DownloadMeritListReports()
        {
            string specialisation = "Developmental Counselling";
            ServiceContext serviceRef = new ServiceContext();
            DataTable dt = null;
            string HTMLContent = string.Empty;

            string folderName = DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("hhmmss");
            Directory.CreateDirectory(ProjectConfiguration.PDFPath + "/Reports/" + folderName);

            #region Open Internal
            #region Development Counselling
            dt = serviceRef.GenerateReports(specialisation, true, true, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            string rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "DC-Internal-Open", folderName);
            #endregion

            #region Early Childhood care & Education
            specialisation = "Early Childhood care & Education";
            dt = serviceRef.GenerateReports(specialisation, true, true, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            //HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "ECCE-Internal-Open", folderName);
            #endregion

            #region Interior Design & Resource Management
            specialisation = "Interior Design & Resource Management";
            dt = serviceRef.GenerateReports(specialisation, true, true, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "IDRM-Internal-Open", folderName);
            #endregion

            #region Textiles & Apparel Designing
            specialisation = "Textiles & Apparel Designing";
            dt = serviceRef.GenerateReports(specialisation, true, true, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "TAD-Internal-Open", folderName);
            #endregion

            #region Hospitality & Tourism Management
            specialisation = "Hospitality & Tourism Management";
            dt = serviceRef.GenerateReports(specialisation, true, true, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "HTM-Internal-Open", folderName);
            #endregion

            #region Mass Communication & Extension
            specialisation = "Mass Communication & Extension";
            dt = serviceRef.GenerateReports(specialisation, true, true, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "MCE-Internal-Open", folderName);
            #endregion

            #region Food, Nutrition and Dietetics
            specialisation = "Food, Nutrition and Dietetics";
            dt = serviceRef.GenerateReports(specialisation, true, true, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "FND-Internal-Open", folderName);
            #endregion
            #endregion

            #region Open External
            #region Development Counselling
            specialisation = "Developmental Counselling";
            dt = serviceRef.GenerateReports(specialisation, false, true, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Open.html"));

            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "DC-External-Open", folderName);
            #endregion
            #region Early Childhood care & Education
            specialisation = "Early Childhood care & Education";
            dt = serviceRef.GenerateReports(specialisation, false, true, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            //HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "ECCE-External-Open", folderName);
            #endregion

            #region Interior Design & Resource Management
            specialisation = "Interior Design & Resource Management";
            dt = serviceRef.GenerateReports(specialisation, false, true, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Open.html"));
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "IDRM-External-Open", folderName);
            #endregion

            #region Textiles & Apparel Designing
            specialisation = "Textiles & Apparel Designing";
            dt = serviceRef.GenerateReports(specialisation, false, true, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Open.html"));
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "TAD-External-Open", folderName);
            #endregion

            #region Hospitality & Tourism Management
            specialisation = "Hospitality & Tourism Management";
            dt = serviceRef.GenerateReports(specialisation, false, true, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Open.html"));
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "HTM-External-Open", folderName);
            #endregion

            #region Mass Communication & Extension
            specialisation = "Mass Communication & Extension";
            dt = serviceRef.GenerateReports(specialisation, false, true, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Open.html"));

            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "MCE-External-Open", folderName);
            #endregion

            #region Food, Nutrition and Dietetics
            specialisation = "Food, Nutrition and Dietetics";
            dt = serviceRef.GenerateReports(specialisation, false, true, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Open.html"));

            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "FND-External-Open", folderName);
            #endregion
            #endregion

            #region Reserved Internal
            #region Development Counselling
            specialisation = "Developmental Counselling";
            dt = serviceRef.GenerateReports(specialisation, true, false, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "DC-Internal-Reserved", folderName);
            #endregion

            #region Early Childhood care & Education
            specialisation = "Early Childhood care & Education";
            dt = serviceRef.GenerateReports(specialisation, true, false, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            //HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "ECCE-Internal-Reserved", folderName);
            #endregion

            #region Interior Design & Resource Management
            specialisation = "Interior Design & Resource Management";
            dt = serviceRef.GenerateReports(specialisation, true, false, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "IDRM-Internal-Reserved", folderName);
            #endregion

            #region Textiles & Apparel Designing
            specialisation = "Textiles & Apparel Designing";
            dt = serviceRef.GenerateReports(specialisation, true, false, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "TAD-Internal-Reserved", folderName);
            #endregion

            #region Hospitality & Tourism Management
            specialisation = "Hospitality & Tourism Management";
            dt = serviceRef.GenerateReports(specialisation, true, false, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "HTM-Internal-Reserved", folderName);
            #endregion

            #region Mass Communication & Extension
            specialisation = "Mass Communication & Extension";
            dt = serviceRef.GenerateReports(specialisation, true, false, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "MCE-Internal-Reserved", folderName);
            #endregion

            #region Food, Nutrition and Dietetics
            specialisation = "Food, Nutrition and Dietetics";
            dt = serviceRef.GenerateReports(specialisation, true, false, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Reserved.html"));
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);

            GetMeritList(HTMLContent, "FND-Internal-Reserved", folderName);
            #endregion
            #endregion

            #region Reserved External
            #region Development Counselling
            specialisation = "Developmental Counselling";
            dt = serviceRef.GenerateReports(specialisation, false, false, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "DC-External-Reserved", folderName);
            #endregion

            #region Early Childhood care & Education
            specialisation = "Early Childhood care & Education";
            dt = serviceRef.GenerateReports(specialisation, false, false, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            //HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "ECCE-External-Reserved", folderName);
            #endregion

            #region Interior Design & Resource Management
            specialisation = "Interior Design & Resource Management";
            dt = serviceRef.GenerateReports(specialisation, false, false, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "IDRM-External-Reserved", folderName);
            #endregion

            #region Textiles & Apparel Designing
            specialisation = "Textiles & Apparel Designing";
            dt = serviceRef.GenerateReports(specialisation, false, false, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "TAD-External-Reserved", folderName);
            #endregion

            #region Hospitality & Tourism Management
            specialisation = "Hospitality & Tourism Management";
            dt = serviceRef.GenerateReports(specialisation, false, false, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "HTM-External-Reserved", folderName);
            #endregion

            #region Mass Communication & Extension
            specialisation = "Mass Communication & Extension";
            dt = serviceRef.GenerateReports(specialisation, false, false, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "MCE-External-Reserved", folderName);
            #endregion

            #region Food, Nutrition and Dietetics
            specialisation = "Food, Nutrition and Dietetics";
            dt = serviceRef.GenerateReports(specialisation, false, false, 1);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-1-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "FND-External-Reserved", folderName);
            #endregion
            #endregion
            ZipFile.CreateFromDirectory(ProjectConfiguration.PDFPath + "/Reports/" + folderName, ProjectConfiguration.PDFPath + "/Reports/" + folderName + ".zip", CompressionLevel.Fastest, true);
            HttpResponseMessage httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);

            var dataBytes = File.ReadAllBytes(ProjectConfiguration.PDFPath + "/Reports/" + folderName + ".zip");
            var dataStream = new MemoryStream(dataBytes);
            httpResponseMessage.Content = new StreamContent(dataStream);
            httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            httpResponseMessage.Content.Headers.ContentDisposition.FileName = "Merit_List_Report_" + folderName + ".zip";
            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            return httpResponseMessage;
        }

        [HttpGet]
        public HttpResponseMessage DownloadMeritListReportsRound2()
        {
            string specialisation = "Developmental Counselling";
            ServiceContext serviceRef = new ServiceContext();
            DataTable dt = null;
            string HTMLContent = string.Empty;

            string folderName = DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("hhmmss");
            Directory.CreateDirectory(ProjectConfiguration.PDFPath + "/Reports/" + folderName);

            #region Open Internal
            #region Development Counselling
            dt = serviceRef.GenerateReports(specialisation, true, true, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            string rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "DC-Internal-Open", folderName);
            #endregion

            #region Early Childhood care & Education
            specialisation = "Early Childhood care & Education";
            dt = serviceRef.GenerateReports(specialisation, true, true, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            //HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "ECCE-Internal-Open", folderName);
            #endregion

            #region Interior Design & Resource Management
            specialisation = "Interior Design & Resource Management";
            dt = serviceRef.GenerateReports(specialisation, true, true, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "IDRM-Internal-Open", folderName);
            #endregion

            #region Textiles & Apparel Designing
            specialisation = "Textiles & Apparel Designing";
            dt = serviceRef.GenerateReports(specialisation, true, true, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "TAD-Internal-Open", folderName);
            #endregion

            #region Hospitality & Tourism Management
            specialisation = "Hospitality & Tourism Management";
            dt = serviceRef.GenerateReports(specialisation, true, true, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "HTM-Internal-Open", folderName);
            #endregion

            #region Mass Communication & Extension
            specialisation = "Mass Communication & Extension";
            dt = serviceRef.GenerateReports(specialisation, true, true, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "MCE-Internal-Open", folderName);
            #endregion

            #region Food, Nutrition and Dietetics
            specialisation = "Food, Nutrition and Dietetics";
            dt = serviceRef.GenerateReports(specialisation, true, true, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "FND-Internal-Open", folderName);
            #endregion
            #endregion

            #region Open External
            #region Development Counselling
            specialisation = "Developmental Counselling";
            dt = serviceRef.GenerateReports(specialisation, false, true, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Open.html"));

            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "DC-External-Open", folderName);
            #endregion
            #region Early Childhood care & Education
            specialisation = "Early Childhood care & Education";
            dt = serviceRef.GenerateReports(specialisation, false, true, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            //HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "ECCE-External-Open", folderName);
            #endregion

            #region Interior Design & Resource Management
            specialisation = "Interior Design & Resource Management";
            dt = serviceRef.GenerateReports(specialisation, false, true, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Open.html"));
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "IDRM-External-Open", folderName);
            #endregion

            #region Textiles & Apparel Designing
            specialisation = "Textiles & Apparel Designing";
            dt = serviceRef.GenerateReports(specialisation, false, true, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Open.html"));
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "TAD-External-Open", folderName);
            #endregion

            #region Hospitality & Tourism Management
            specialisation = "Hospitality & Tourism Management";
            dt = serviceRef.GenerateReports(specialisation, false, true, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Open.html"));
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "HTM-External-Open", folderName);
            #endregion

            #region Mass Communication & Extension
            specialisation = "Mass Communication & Extension";
            dt = serviceRef.GenerateReports(specialisation, false, true, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Open.html"));

            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "MCE-External-Open", folderName);
            #endregion

            #region Food, Nutrition and Dietetics
            specialisation = "Food, Nutrition and Dietetics";
            dt = serviceRef.GenerateReports(specialisation, false, true, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Open.html"));

            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "FND-External-Open", folderName);
            #endregion
            #endregion

            #region Reserved Internal
            #region Development Counselling
            specialisation = "Developmental Counselling";
            dt = serviceRef.GenerateReports(specialisation, true, false, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "DC-Internal-Reserved", folderName);
            #endregion

            #region Early Childhood care & Education
            specialisation = "Early Childhood care & Education";
            dt = serviceRef.GenerateReports(specialisation, true, false, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            //HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "ECCE-Internal-Reserved", folderName);
            #endregion

            #region Interior Design & Resource Management
            specialisation = "Interior Design & Resource Management";
            dt = serviceRef.GenerateReports(specialisation, true, false, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "IDRM-Internal-Reserved", folderName);
            #endregion

            #region Textiles & Apparel Designing
            specialisation = "Textiles & Apparel Designing";
            dt = serviceRef.GenerateReports(specialisation, true, false, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "TAD-Internal-Reserved", folderName);
            #endregion

            #region Hospitality & Tourism Management
            specialisation = "Hospitality & Tourism Management";
            dt = serviceRef.GenerateReports(specialisation, true, false, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "HTM-Internal-Reserved", folderName);
            #endregion

            #region Mass Communication & Extension
            specialisation = "Mass Communication & Extension";
            dt = serviceRef.GenerateReports(specialisation, true, false, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "MCE-Internal-Reserved", folderName);
            #endregion

            #region Food, Nutrition and Dietetics
            specialisation = "Food, Nutrition and Dietetics";
            dt = serviceRef.GenerateReports(specialisation, true, false, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Reserved.html"));
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);

            GetMeritList(HTMLContent, "FND-Internal-Reserved", folderName);
            #endregion
            #endregion

            #region Reserved External
            #region Development Counselling
            specialisation = "Developmental Counselling";
            dt = serviceRef.GenerateReports(specialisation, false, false, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "DC-External-Reserved", folderName);
            #endregion

            #region Early Childhood care & Education
            specialisation = "Early Childhood care & Education";
            dt = serviceRef.GenerateReports(specialisation, false, false, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            //HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "ECCE-External-Reserved", folderName);
            #endregion

            #region Interior Design & Resource Management
            specialisation = "Interior Design & Resource Management";
            dt = serviceRef.GenerateReports(specialisation, false, false, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "IDRM-External-Reserved", folderName);
            #endregion

            #region Textiles & Apparel Designing
            specialisation = "Textiles & Apparel Designing";
            dt = serviceRef.GenerateReports(specialisation, false, false, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "TAD-External-Reserved", folderName);
            #endregion

            #region Hospitality & Tourism Management
            specialisation = "Hospitality & Tourism Management";
            dt = serviceRef.GenerateReports(specialisation, false, false, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "HTM-External-Reserved", folderName);
            #endregion

            #region Mass Communication & Extension
            specialisation = "Mass Communication & Extension";
            dt = serviceRef.GenerateReports(specialisation, false, false, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "MCE-External-Reserved", folderName);
            #endregion

            #region Food, Nutrition and Dietetics
            specialisation = "Food, Nutrition and Dietetics";
            dt = serviceRef.GenerateReports(specialisation, false, false, 2);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-2-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "FND-External-Reserved", folderName);
            #endregion
            #endregion
            ZipFile.CreateFromDirectory(ProjectConfiguration.PDFPath + "/Reports/" + folderName, ProjectConfiguration.PDFPath + "/Reports/" + folderName + ".zip", CompressionLevel.Fastest, true);
            HttpResponseMessage httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);

            var dataBytes = File.ReadAllBytes(ProjectConfiguration.PDFPath + "/Reports/" + folderName + ".zip");
            var dataStream = new MemoryStream(dataBytes);
            httpResponseMessage.Content = new StreamContent(dataStream);
            httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            httpResponseMessage.Content.Headers.ContentDisposition.FileName = "Merit_List_Report_" + folderName + ".zip";
            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            return httpResponseMessage;
        }

        [HttpGet]
        public HttpResponseMessage DownloadMeritListReportsRound3()
        {
            string specialisation = "Developmental Counselling";
            ServiceContext serviceRef = new ServiceContext();
            DataTable dt = null;
            string HTMLContent = string.Empty;

            string folderName = DateTime.Now.ToString("ddMMyyyy") + "_" + DateTime.Now.ToString("hhmmss");
            Directory.CreateDirectory(ProjectConfiguration.PDFPath + "/Reports/" + folderName);

            #region Open Internal
            #region Development Counselling
            dt = serviceRef.GenerateReports(specialisation, true, true,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            string rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "DC-Internal-Open", folderName);
            #endregion

            #region Early Childhood care & Education
            specialisation = "Early Childhood care & Education";
            dt = serviceRef.GenerateReports(specialisation, true, true,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            //HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "ECCE-Internal-Open", folderName);
            #endregion

            #region Interior Design & Resource Management
            specialisation = "Interior Design & Resource Management";
            dt = serviceRef.GenerateReports(specialisation, true, true,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "IDRM-Internal-Open", folderName);
            #endregion

            #region Textiles & Apparel Designing
            specialisation = "Textiles & Apparel Designing";
            dt = serviceRef.GenerateReports(specialisation, true, true,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "TAD-Internal-Open", folderName);
            #endregion

            #region Hospitality & Tourism Management
            specialisation = "Hospitality & Tourism Management";
            dt = serviceRef.GenerateReports(specialisation, true, true,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "HTM-Internal-Open", folderName);
            #endregion

            #region Mass Communication & Extension
            specialisation = "Mass Communication & Extension";
            dt = serviceRef.GenerateReports(specialisation, true, true,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "MCE-Internal-Open", folderName);
            #endregion

            #region Food, Nutrition and Dietetics
            specialisation = "Food, Nutrition and Dietetics";
            dt = serviceRef.GenerateReports(specialisation, true, true,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "FND-Internal-Open", folderName);
            #endregion
            #endregion

            #region Open External
            #region Development Counselling
            specialisation = "Developmental Counselling";
            dt = serviceRef.GenerateReports(specialisation, false, true,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Open.html"));

            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "DC-External-Open", folderName);
            #endregion
            #region Early Childhood care & Education
            specialisation = "Early Childhood care & Education";
            dt = serviceRef.GenerateReports(specialisation, false, true,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Open.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            //HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "ECCE-External-Open", folderName);
            #endregion

            #region Interior Design & Resource Management
            specialisation = "Interior Design & Resource Management";
            dt = serviceRef.GenerateReports(specialisation, false, true,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Open.html"));
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "IDRM-External-Open", folderName);
            #endregion

            #region Textiles & Apparel Designing
            specialisation = "Textiles & Apparel Designing";
            dt = serviceRef.GenerateReports(specialisation, false, true,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Open.html"));
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "TAD-External-Open", folderName);
            #endregion

            #region Hospitality & Tourism Management
            specialisation = "Hospitality & Tourism Management";
            dt = serviceRef.GenerateReports(specialisation, false, true,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Open.html"));
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "HTM-External-Open", folderName);
            #endregion

            #region Mass Communication & Extension
            specialisation = "Mass Communication & Extension";
            dt = serviceRef.GenerateReports(specialisation, false, true,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Open.html"));

            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "MCE-External-Open", folderName);
            #endregion

            #region Food, Nutrition and Dietetics
            specialisation = "Food, Nutrition and Dietetics";
            dt = serviceRef.GenerateReports(specialisation, false, true,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Open.html"));

            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "FND-External-Open", folderName);
            #endregion
            #endregion

            #region Reserved Internal
            #region Development Counselling
            specialisation = "Developmental Counselling";
            dt = serviceRef.GenerateReports(specialisation, true, false,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "DC-Internal-Reserved", folderName);
            #endregion

            #region Early Childhood care & Education
            specialisation = "Early Childhood care & Education";
            dt = serviceRef.GenerateReports(specialisation, true, false,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            //HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "ECCE-Internal-Reserved", folderName);
            #endregion

            #region Interior Design & Resource Management
            specialisation = "Interior Design & Resource Management";
            dt = serviceRef.GenerateReports(specialisation, true, false,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "IDRM-Internal-Reserved", folderName);
            #endregion

            #region Textiles & Apparel Designing
            specialisation = "Textiles & Apparel Designing";
            dt = serviceRef.GenerateReports(specialisation, true, false,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "TAD-Internal-Reserved", folderName);
            #endregion

            #region Hospitality & Tourism Management
            specialisation = "Hospitality & Tourism Management";
            dt = serviceRef.GenerateReports(specialisation, true, false,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "HTM-Internal-Reserved", folderName);
            #endregion

            #region Mass Communication & Extension
            specialisation = "Mass Communication & Extension";
            dt = serviceRef.GenerateReports(specialisation, true, false,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            GetMeritList(HTMLContent, "MCE-Internal-Reserved", folderName);
            #endregion

            #region Food, Nutrition and Dietetics
            specialisation = "Food, Nutrition and Dietetics";
            dt = serviceRef.GenerateReports(specialisation, true, false,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Reserved.html"));
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);

            GetMeritList(HTMLContent, "FND-Internal-Reserved", folderName);
            #endregion
            #endregion

            #region Reserved External
            #region Development Counselling
            specialisation = "Developmental Counselling";
            dt = serviceRef.GenerateReports(specialisation, false, false,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "DC-External-Reserved", folderName);
            #endregion

            #region Early Childhood care & Education
            specialisation = "Early Childhood care & Education";
            dt = serviceRef.GenerateReports(specialisation, false, false,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            //HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "SVT");
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "ECCE-External-Reserved", folderName);
            #endregion

            #region Interior Design & Resource Management
            specialisation = "Interior Design & Resource Management";
            dt = serviceRef.GenerateReports(specialisation, false, false,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "IDRM-External-Reserved", folderName);
            #endregion

            #region Textiles & Apparel Designing
            specialisation = "Textiles & Apparel Designing";
            dt = serviceRef.GenerateReports(specialisation, false, false,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "TAD-External-Reserved", folderName);
            #endregion

            #region Hospitality & Tourism Management
            specialisation = "Hospitality & Tourism Management";
            dt = serviceRef.GenerateReports(specialisation, false, false,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "HTM-External-Reserved", folderName);
            #endregion

            #region Mass Communication & Extension
            specialisation = "Mass Communication & Extension";
            dt = serviceRef.GenerateReports(specialisation, false, false,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "MCE-External-Reserved", folderName);
            #endregion

            #region Food, Nutrition and Dietetics
            specialisation = "Food, Nutrition and Dietetics";
            dt = serviceRef.GenerateReports(specialisation, false, false,3);

            HTMLContent = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/Templates/Merit-List-3-Reserved.html"));

            HTMLContent = HTMLContent.Replace("{{Specialisation}}", specialisation);
            rowHTMLItems = string.Empty;

            foreach (DataRow row in dt.Rows)
            {
                rowHTMLItems += "<tr><td>" + row["Serial Number"].ToString() + "</td><td>" + row["Form No"].ToString() + "</td><td style='text-align: left'>" + row["Full Name"].ToString() + "</td><td>" + row["Caste"].ToString() + "</td><td>" + row["Percentage"].ToString() + "</td><td>" + row["CourseAdmittedRound1"].ToString() + " </td><td>" + row["CourseAdmittedRound2"].ToString() + " </td><td>" + row["CourseAdmittedRound3"].ToString() + " </td></tr>";
            }

            HTMLContent = HTMLContent.Replace("{{ReportBody}}", rowHTMLItems);
            HTMLContent = HTMLContent.Replace("{{SVTStudent}}", "Outside");
            GetMeritList(HTMLContent, "FND-External-Reserved", folderName);
            #endregion
            #endregion
            ZipFile.CreateFromDirectory(ProjectConfiguration.PDFPath + "/Reports/" + folderName, ProjectConfiguration.PDFPath + "/Reports/" + folderName + ".zip", CompressionLevel.Fastest, true);
            HttpResponseMessage httpResponseMessage = Request.CreateResponse(HttpStatusCode.OK);

            var dataBytes = File.ReadAllBytes(ProjectConfiguration.PDFPath + "/Reports/" + folderName + ".zip");
            var dataStream = new MemoryStream(dataBytes);
            httpResponseMessage.Content = new StreamContent(dataStream);
            httpResponseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            httpResponseMessage.Content.Headers.ContentDisposition.FileName = "Merit_List_Report_" + folderName + ".zip";
            httpResponseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
            return httpResponseMessage;
        }
        #endregion

        #region Get Cut Of Percentage
        public DataTable GetCutOffPercent(bool firstPref, bool secondPref, string specialisation)
        {
            DataTable dt = new DataTable();
            var roundlist = new List<RoundList>();
            var seatsList = ReadSeatsFile();
            switch (specialisation)
            {
                case "DC":
                    specialisation = "Developmental Counselling";
                    break;
                case "ECCE":
                    specialisation = "Early Childhood care & Education";
                    break;
                case "FND":
                    specialisation = "Food, Nutrition and Dietetics";
                    break;
                case "HTM":
                    specialisation = "Hospitality & Tourism Management";
                    break;
                case "IDRM":
                    specialisation = "Interior Design & Resource Management";
                    break;
                case "MCE":
                    specialisation = "Mass Communication & Extension";
                    break;
                case "TAD":
                    specialisation = "Textiles & Apparel Designing";
                    break;
            }

            string round = "Round1";
            bool isSVT = false;
            string category = "OPEN";
            bool isAdmittedinCurrentRound = false;
            roundlist = ReadRoundPReviewFile(specialisation);// ReadRoundFile(round);
            List<PreviewStudentDetails> listOfStudents = new List<PreviewStudentDetails>();
            int sr = 0;
            List<CutOffPreview> list = new List<CutOffPreview>();



            if (seatsList != null && seatsList.Count > 1 && roundlist != null && roundlist.Count > 1)
            {
                #region Internal Open
                try
                {
                    //Get Student List
                    isSVT = true;
                    category = "OPEN";
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);

                        for (int i = 0; i < 10; i++)
                        {
                            foreach (var student in studenList)
                            {
                                // If first/second preference doesnt match then skip this student
                                if (student.CoursePreference1 != specialisation && student.CoursePreference2 != specialisation)
                                {
                                    continue;
                                }

                                #region "Check Student already allocated anyhow then continue to other student"

                                isAdmittedinCurrentRound = false;
                                string lastAdmittedCourse = string.Empty;
                                bool leaveStudent = false;
                                if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                                {
                                    isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                    lastAdmittedCourse = student.CourseAdmittedRound1;
                                }
                                else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                                {
                                    isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                    lastAdmittedCourse = student.CourseAdmittedRound2;
                                }
                                else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                                {
                                    isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                    lastAdmittedCourse = student.CourseAdmittedRound3;
                                }
                                else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                                {
                                    isAdmittedinCurrentRound = false;
                                    lastAdmittedCourse = student.CourseAdmittedRound1;
                                }
                                else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                                {
                                    isAdmittedinCurrentRound = false;
                                    lastAdmittedCourse = student.CourseAdmittedRound2;
                                }
                                else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                                {
                                    isAdmittedinCurrentRound = false;
                                    lastAdmittedCourse = student.CourseAdmittedRound1;
                                }
                                if (isAdmittedinCurrentRound)
                                {
                                    continue;
                                }

                                #endregion

                                #region "Check Seat, percentage and allocate seat"

                                bool SVTOpenInternal = false;
                                bool SVTReservedInternal = false;
                                bool ExternalOpen = false;
                                bool ExternalReserved = false;

                                if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                                {
                                    SVTOpenInternal = true;
                                }
                                else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                                {
                                    ExternalOpen = true;
                                }
                                else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                                {
                                    SVTReservedInternal = true;
                                }
                                else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                                {
                                    ExternalReserved = true;
                                }

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                {
                                    //service.UpdatePreferenceByCourse(student.Id, student.CoursePreference1, round);
                                    //UpdateSeatData(seatsList);
                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference1;
                                    listOfStudents.Add(st);

                                }
                                else if (leaveStudent == false && secondPref == true)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                    {
                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference2;
                                        listOfStudents.Add(st);
                                    }
                                }
                                #endregion
                            }

                            CutOffPreview cop = new CutOffPreview();
                            cop.Specialisation = specialisation;
                            cop.Category = "InternalOpen";
                            cop.CutOff = roundlist[0].SVTOpenInternal;
                            cop.Count = listOfStudents.Count;
                            list.Add(cop);
                            roundlist.RemoveAt(0);
                        }
                        seatsList.Where(w => w.SVTOpenInternal > 0).ToList().ForEach(i => { i.ExternalOpen = i.ExternalOpen + i.SVTOpenInternal; i.SVTOpenInternal = 0; });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion

                int previousCount = listOfStudents.Count;
                #region External Open
                try
                {
                    //Get Student List
                    isSVT = false;
                    category = "OPEN";
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);
                        roundlist = ReadRoundPReviewFile(specialisation);
                        for (int i = 0; i < 10; i++)
                        {
                            foreach (var student in studenList)
                            {
                                if (student.CoursePreference1 != specialisation && student.CoursePreference2 != specialisation)
                                    continue;

                                #region "Check Student already allocated anyhow then continue to other student"

                                isAdmittedinCurrentRound = false;
                                string lastAdmittedCourse = string.Empty;
                                bool leaveStudent = false;
                                if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                                {
                                    isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                    lastAdmittedCourse = student.CourseAdmittedRound1;
                                }
                                else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                                {
                                    isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                    lastAdmittedCourse = student.CourseAdmittedRound2;
                                }
                                else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                                {
                                    isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                    lastAdmittedCourse = student.CourseAdmittedRound3;
                                }
                                else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                                {
                                    isAdmittedinCurrentRound = false;
                                    lastAdmittedCourse = student.CourseAdmittedRound1;
                                }
                                else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                                {
                                    isAdmittedinCurrentRound = false;
                                    lastAdmittedCourse = student.CourseAdmittedRound2;
                                }
                                else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                                {
                                    isAdmittedinCurrentRound = false;
                                    lastAdmittedCourse = student.CourseAdmittedRound1;
                                }
                                if (isAdmittedinCurrentRound)
                                {
                                    continue;
                                }

                                #endregion

                                #region "Check Seat, percentage and allocate seat"

                                bool SVTOpenInternal = false;
                                bool SVTReservedInternal = false;
                                bool ExternalOpen = false;
                                bool ExternalReserved = false;
                                //Get Categoy and SVT student wise but value
                                if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                                {
                                    SVTOpenInternal = true;
                                }
                                else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                                {
                                    ExternalOpen = true;
                                }
                                else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                                {
                                    SVTReservedInternal = true;
                                }
                                else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                                {
                                    ExternalReserved = true;
                                }


                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                {
                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference1;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false && secondPref == true)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                    {
                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference2;
                                        listOfStudents.Add(st);
                                    }
                                }

                                #endregion
                            }

                            CutOffPreview cop = new CutOffPreview();
                            cop.Specialisation = specialisation;
                            cop.Category = "ExternalOpen";
                            cop.CutOff = roundlist[0].ExternalOpen;
                            cop.Count = listOfStudents.Count - previousCount;
                            list.Add(cop);
                            roundlist.RemoveAt(0);
                        }
                        seatsList.Where(w => w.ExternalOpen > 0).ToList().ForEach(i => { i.SVTReservedInternal = i.ExternalOpen + i.SVTReservedInternal; i.ExternalOpen = 0; });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion

                previousCount = listOfStudents.Count;
                #region Internal Reserved
                try
                {
                    //Get Student List
                    isSVT = true;
                    category = "RESERVED";
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);
                        roundlist = ReadRoundPReviewFile(specialisation);
                        for (int i = 0; i < 10; i++)
                        {
                            foreach (var student in studenList)
                            {
                                if (student.CoursePreference1 != specialisation && student.CoursePreference2 != specialisation)
                                    continue;

                                #region "Check Student already allocated anyhow then continue to other student"

                                isAdmittedinCurrentRound = false;
                                string lastAdmittedCourse = string.Empty;
                                bool leaveStudent = false;
                                if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                                {
                                    isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                    lastAdmittedCourse = student.CourseAdmittedRound1;
                                }
                                else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                                {
                                    isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                    lastAdmittedCourse = student.CourseAdmittedRound2;
                                }
                                else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                                {
                                    isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                    lastAdmittedCourse = student.CourseAdmittedRound3;
                                }
                                else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                                {
                                    isAdmittedinCurrentRound = false;
                                    lastAdmittedCourse = student.CourseAdmittedRound1;
                                }
                                else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                                {
                                    isAdmittedinCurrentRound = false;
                                    lastAdmittedCourse = student.CourseAdmittedRound2;
                                }
                                else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                                {
                                    isAdmittedinCurrentRound = false;
                                    lastAdmittedCourse = student.CourseAdmittedRound1;
                                }
                                if (isAdmittedinCurrentRound)
                                {
                                    continue;
                                }

                                #endregion

                                #region "Check Seat, percentage and allocate seat"

                                bool SVTOpenInternal = false;
                                bool SVTReservedInternal = false;
                                bool ExternalOpen = false;
                                bool ExternalReserved = false;
                                //Get Categoy and SVT student wise but value
                                if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                                {
                                    SVTOpenInternal = true;
                                }
                                else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                                {
                                    ExternalOpen = true;
                                }
                                else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                                {
                                    SVTReservedInternal = true;
                                }
                                else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                                {
                                    ExternalReserved = true;
                                }


                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                {
                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference1;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false && secondPref == true)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                    {
                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference2;
                                        listOfStudents.Add(st);
                                    }
                                }

                                #endregion
                            }

                            CutOffPreview cop = new CutOffPreview();
                            cop.Specialisation = specialisation;
                            cop.Category = "InternalReserved";
                            cop.CutOff = roundlist[0].SVTReservedInternal;
                            cop.Count = listOfStudents.Count - previousCount;
                            list.Add(cop);
                            roundlist.RemoveAt(0);
                        }
                        seatsList.Where(w => w.SVTReservedInternal > 0).ToList().ForEach(i => { i.ExternalReserved = i.ExternalReserved + i.SVTReservedInternal; i.SVTReservedInternal = 0; });
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion

                previousCount = listOfStudents.Count;
                #region External Reserved
                try
                {
                    //Get Student List
                    isSVT = false;
                    category = "RESERVED";
                    using (ServiceContext service = new ServiceContext())
                    {
                        var studenList = service.GetStudentList(isSVT, category);
                        roundlist = ReadRoundPReviewFile(specialisation);
                        for (int i = 0; i < 10; i++)
                        {
                            foreach (var student in studenList)
                            {
                                if (student.CoursePreference1 != specialisation && student.CoursePreference2 != specialisation)
                                {
                                    continue;
                                }

                                #region "Check Student already allocated anyhow then continue to other student"
                                isAdmittedinCurrentRound = false;
                                string lastAdmittedCourse = string.Empty;
                                bool leaveStudent = false;
                                if (round.ToUpper() == "ROUND1" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                                {
                                    isAdmittedinCurrentRound = student.AdmittedRound1.HasValue ? student.AdmittedRound1.Value : false;
                                    lastAdmittedCourse = student.CourseAdmittedRound1;
                                }
                                else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                                {
                                    isAdmittedinCurrentRound = student.AdmittedRound2.HasValue ? student.AdmittedRound2.Value : false;
                                    lastAdmittedCourse = student.CourseAdmittedRound2;
                                }
                                else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound3 != null && student.AdmittedRound3.Value == true))
                                {
                                    isAdmittedinCurrentRound = student.AdmittedRound3.HasValue ? student.AdmittedRound3.Value : false;
                                    lastAdmittedCourse = student.CourseAdmittedRound3;
                                }
                                else if (round.ToUpper() == "ROUND2" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                                {
                                    isAdmittedinCurrentRound = false;
                                    lastAdmittedCourse = student.CourseAdmittedRound1;
                                }
                                else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound2 != null && student.AdmittedRound2.Value == true))
                                {
                                    isAdmittedinCurrentRound = false;
                                    lastAdmittedCourse = student.CourseAdmittedRound2;
                                }
                                else if (round.ToUpper() == "ROUND3" && (student.AdmittedRound1 != null && student.AdmittedRound1.Value == true))
                                {
                                    isAdmittedinCurrentRound = false;
                                    lastAdmittedCourse = student.CourseAdmittedRound1;
                                }
                                if (isAdmittedinCurrentRound)
                                {
                                    continue;
                                }

                                #endregion

                                #region "Check Seat, percentage and allocate seat"

                                bool SVTOpenInternal = false;
                                bool SVTReservedInternal = false;
                                bool ExternalOpen = false;
                                bool ExternalReserved = false;
                                //Get Categoy and SVT student wise but value
                                if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == true)
                                {
                                    SVTOpenInternal = true;
                                }
                                else if (student.Category.ToUpper() == "OPEN" && student.IsSVTStudent == false)
                                {
                                    ExternalOpen = true;
                                }
                                else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == true)
                                {
                                    SVTReservedInternal = true;
                                }
                                else if (student.Category.ToUpper() == "RESERVED" && student.IsSVTStudent == false)
                                {
                                    ExternalReserved = true;
                                }

                                if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference1, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                {
                                    if (listOfStudents.Count > 0)
                                        sr = listOfStudents.Last().SerialNumber;
                                    PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                    st.SerialNumber = sr + 1;
                                    st.PossibleCourseAdmitted = student.CoursePreference1;
                                    listOfStudents.Add(st);
                                }
                                else if (leaveStudent == false && secondPref == true)
                                {
                                    if (CheckRoundCutoffandAvailbilityByCourse(student.CoursePreference2, roundlist, ref seatsList, student, SVTOpenInternal, SVTReservedInternal, ExternalOpen, ExternalReserved, round, lastAdmittedCourse, ref leaveStudent) == true)
                                    {
                                        if (listOfStudents.Count > 0)
                                            sr = listOfStudents.Last().SerialNumber;
                                        PreviewStudentDetails st = ConvertToPreviewStudentDetails(student);
                                        st.SerialNumber = sr + 1;
                                        st.PossibleCourseAdmitted = student.CoursePreference2;
                                        listOfStudents.Add(st);
                                    }
                                }
                                #endregion
                            }

                            CutOffPreview cop = new CutOffPreview();
                            cop.Specialisation = specialisation;
                            cop.Category = "ExternalReserved";
                            cop.CutOff = roundlist[0].ExternalReserved;
                            cop.Count = listOfStudents.Count - previousCount;
                            list.Add(cop);
                            roundlist.RemoveAt(0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion
                dt = ListtoDataTable.ToDataTable(list);
            }

            return dt;
            #endregion
        }
    }

    class CutOffPreview
    {
        public string Specialisation { get; set; }
        public decimal CutOff { get; set; }
        public int Count { get; set; }
        public string Category { get; set; }
    }
}