using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Diagnostics;
using AdmissionForm.Business.Services;
using AdmissionForm.Business.Model;
using AdmissionForm.Infrastructure;
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
using AdmissionForm.API.Classes;

namespace AdmissionForm.API.Controllers
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

            AdmissionFormDetail admissionFormDetail = new AdmissionFormDetail();

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
                        admissionFormDetail.Id = provider.FormData.GetValues("Id")[0].ToInteger();
                    }
                    else
                    {
                        admissionFormDetail.DateRegistered = DateTime.Now;
                    }

                    #endregion

                    bool isSubmit = false;
                    if (provider.FormData.GetValues("isSubmit")[0].String() == "1")
                    {
                        isSubmit = true;
                        admissionFormDetail.IsSubmitted = true;
                    }
                    else
                    {
                        isSubmit = false;
                        admissionFormDetail.IsSubmitted = false;
                    }

                    #region Personal Detail

                    #region Personal Information

                    #region Validate Course Name and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("CourseName")[0].String()))
                    {
                        message = "Please enter course name.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.CourseName = provider.FormData.GetValues("CourseName")[0].String();
                    }

                    #endregion

                    #region  Validate Last Name and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("LastName")[0].String()))
                    {
                        message = "Please enter last name.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.LastName = provider.FormData.GetValues("LastName")[0].String();
                    }

                    #endregion

                    #region  Validate First Name and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("FirstName")[0].String()))
                    {
                        message = "Please enter first name.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.FirstName = provider.FormData.GetValues("FirstName")[0].String();
                    }

                    #endregion

                    #region  Validate Father Name and also Assign Value if proper

                    if (string.IsNullOrEmpty(provider.FormData.GetValues("FatherName")[0].String()))
                    {
                        message = "Please enter father name.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.FatherName = provider.FormData.GetValues("FatherName")[0].String();
                    }

                    #endregion

                    #region  AadharNumber
                    if (string.IsNullOrEmpty(provider.FormData.GetValues("AadharNumber")[0].String()))
                    {
                        message = "Please enter Aadhar Number.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        if (provider.FormData.GetValues("AadharNumber")[0].ToString().Length < 12)
                        {
                            message = "Please enter valid aadhar number.";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                        else
                        {
                            admissionFormDetail.AadharNumber = provider.FormData.GetValues("AadharNumber")[0].String();
                        }
                    }
                    #endregion

                    #region  Validate Father Last Name and also Assign Value if proper

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("FatherLastName")[0].String()))
                    {
                        message = "Please enter father last name.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.FatherLastName = provider.FormData.GetValues("FatherLastName")[0].String();
                    }

                    #endregion

                    #region  Validate Father First Name and also Assign Value if proper

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("FatherFirstName")[0].String()))
                    {
                        message = "Please enter father first name.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.FatherFirstName = provider.FormData.GetValues("FatherFirstName")[0].String();
                    }

                    #endregion

                    #region  Validate Father Middle Name and also Assign Value if proper

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("FatherMiddleName")[0].String()))
                    {
                        message = "Please enter father middle name.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.FatherMiddleName = provider.FormData.GetValues("FatherMiddleName")[0].String();
                    }

                    #endregion

                    #region  Validate Mother Last Name and also Assign Value if proper

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("MotherLastName")[0].String()))
                    {
                        message = "Please enter mother last name.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.MotherLastName = provider.FormData.GetValues("MotherLastName")[0].String();
                    }

                    #endregion

                    #region  Validate Mother First Name and also Assign Value if proper

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("MotherFirstName")[0].String()))
                    {
                        message = "Please enter mother first name.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.MotherFirstName = provider.FormData.GetValues("MotherFirstName")[0].String();
                    }

                    #endregion

                    #region  Validate Mother Middle Name and also Assign Value if proper

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("MotherMiddleName")[0].String()))
                    {
                        message = "Please enter mother middle name.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.MotherMiddleName = provider.FormData.GetValues("MotherMiddleName")[0].String();
                    }

                    #endregion

                    #endregion

                    #region IDENTITY DETAILS                      

                    if (!string.IsNullOrEmpty(provider.FormData.GetValues("PanNumber")[0].String()))
                    {
                        if (provider.FormData.GetValues("PanNumber")[0].ToString().Length < 10)
                        {
                            message = "Please enter valid pan card number.";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                        admissionFormDetail.PanNumber = provider.FormData.GetValues("PanNumber")[0].String().ToUpper();
                    }

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("txtBirthDate")[0].String()))
                    {
                        message = "Please enter birth date.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else if (!string.IsNullOrEmpty(provider.FormData.GetValues("txtBirthDate")[0].String()))
                    {
                        admissionFormDetail.Dob = Convert.ToDateTime(provider.FormData.GetValues("txtBirthDate")[0].String());
                        admissionFormDetail.CalculatedAge = provider.FormData.GetValues("CalculatedAge")[0].ToInteger();
                    }

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("MotherTongue")[0].String()))
                    {
                        message = "Please enter mother tongue.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.MotherTongue = provider.FormData.GetValues("MotherTongue")[0].String().ToUpper();
                    }

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("Nationality")[0].String()))
                    {
                        message = "Please enter nationality.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else { admissionFormDetail.Nationality = provider.FormData.GetValues("Nationality")[0].String().ToUpper(); }


                    #endregion

                    #region Religion/CASTE DETAILS

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("Religion")[0].String()))
                    {
                        message = "Please enter religion.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.Religion = provider.FormData.GetValues("Religion")[0].String();
                    }

                    if (provider.FormData.AllKeys.Contains("Category"))
                    {
                        if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("Category")[0].String()))
                        {
                            message = "Please select category.";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                        else
                        {
                            admissionFormDetail.Category = provider.FormData.GetValues("Category")[0].String();
                        }

                        if (provider.FormData.GetValues("Category")[0].String() == "RESERVED")
                        {
                            admissionFormDetail.Caste = provider.FormData.GetValues("Caste")[0].String();
                            admissionFormDetail.SubCaste = provider.FormData.GetValues("SubCaste")[0].String();
                        }
                        else
                        {
                            admissionFormDetail.Caste = "";
                        }
                    }

                    #endregion

                    #region  CONTACT DETAILS

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("CurrentAddress")[0].String()))
                    {
                        message = "Please enter current address.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.CurrentAddress = provider.FormData.GetValues("CurrentAddress")[0].String();
                    }

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("PermanentAddress")[0].String()))
                    {
                        message = "Please enter permanent address.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.PermanentAddress = provider.FormData.GetValues("PermanentAddress")[0].String();
                    }

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("ResContactNo")[0].String()))
                    {
                        message = "Please enter contact number.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.ResContactNo = provider.FormData.GetValues("ResContactNo")[0].String();
                    }

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("MobileNumber")[0].String()))
                    {
                        message = "Please enter mobile number.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.MobileNumber = provider.FormData.GetValues("MobileNumber")[0].String();
                    }

                    if (!string.IsNullOrEmpty(provider.FormData.GetValues("Email")[0].String()))
                    {
                        string emailRegex = @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$";
                        Regex re = new Regex(emailRegex);
                        if (!re.IsMatch(provider.FormData.GetValues("Email")[0].String()))
                        {
                            message = "Please enter correct email address";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                        else
                        {
                            admissionFormDetail.Email = provider.FormData.GetValues("Email")[0].String();
                        }
                    }
                    else if (isSubmit)
                    {
                        message = "Please enter email address";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }

                    #endregion

                    #region GUARDIAN DETAILS

                    if (admissionFormDetail.CalculatedAge < 18)
                    {
                        if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("GuardianMotherName")[0].String()))
                        {
                            message = "Please enter guardian mother name.";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                        if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("GuardianFatherName")[0].String()))
                        {
                            message = "Please enter guardian father name.";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                        if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("AnnualIncome")[0].String()))
                        {
                            message = "Please enter annual income.";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                        if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("GuardianEmergencyConactNo")[0].String()))
                        {
                            message = "Please enter guardian emergency conact no.";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                    }
                    admissionFormDetail.GuardianMotherName = provider.FormData.GetValues("GuardianMotherName")[0].String();
                    admissionFormDetail.GuardianFatherName = provider.FormData.GetValues("GuardianFatherName")[0].String();
                    admissionFormDetail.AnnualIncome = provider.FormData.GetValues("AnnualIncome")[0].ToString();
                    admissionFormDetail.GuardianEmergencyConactNo = provider.FormData.GetValues("GuardianEmergencyConactNo")[0].String();
                    admissionFormDetail.OccupationofMother = provider.FormData.GetValues("OccupationofMother")[0].String();
                    admissionFormDetail.OccupationofFather = provider.FormData.GetValues("OccupationofFather")[0].String();
                    admissionFormDetail.EducationofMother = provider.FormData.GetValues("EducationofMother")[0].String();
                    admissionFormDetail.EducationofFather = provider.FormData.GetValues("EducationofFather")[0].String();
                    admissionFormDetail.GuardianAddress = provider.FormData.GetValues("GuardianAddress")[0].String();


                    admissionFormDetail.GuardianTelephoneNo = provider.FormData.GetValues("GuardianTelephoneNo")[0].String();
                    admissionFormDetail.GuardianOffice = provider.FormData.GetValues("GuardianOffice")[0].String();
                    admissionFormDetail.GuardianMobile = provider.FormData.GetValues("GuardianMobile")[0].String();

                    if (!string.IsNullOrEmpty(provider.FormData.GetValues("GuardianEmail")[0].String()))
                    {
                        string emailRegex = @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$";
                        Regex re = new Regex(emailRegex);
                        if (!re.IsMatch(provider.FormData.GetValues("GuardianEmail")[0].String()))
                        {
                            message = "Please enter correct guardian email";
                            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                        }
                        else
                        {
                            admissionFormDetail.GuardianEmail = provider.FormData.GetValues("GuardianEmail")[0].String();
                        }
                    }

                    admissionFormDetail.NativePlaceAddress = provider.FormData.GetValues("NativePlaceAddress")[0].String();

                    #endregion

                    #region BANK DETAILS 
                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("BankName")[0].String()))
                    {
                        message = "Please enter bank name."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.BankName = provider.FormData.GetValues("BankName")[0].String();
                    }

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("BankAddress")[0].String()))
                    {
                        message = "Please enter bank address."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.BankAddress = provider.FormData.GetValues("BankAddress")[0].String();
                    }

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("BankAccountNumber")[0].String()))
                    {
                        message = "Please enter bank account number."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.BankAccountNumber = provider.FormData.GetValues("BankAccountNumber")[0].String();
                    }

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("AccountType")[0].String()))
                    {
                        message = "Please enter account type."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.AccountType = provider.FormData.GetValues("AccountType")[0].String();
                    }

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("IFSCCode")[0].String()))
                    {
                        message = "Please enter IFSC code."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.IFSCCode = provider.FormData.GetValues("IFSCCode")[0].String();
                    }
                    admissionFormDetail.MICRNumber = provider.FormData.GetValues("MICRNumber")[0].String();
                    #endregion

                    #region WORK EXPERIENCE

                    admissionFormDetail.OrganisationName = provider.FormData.GetValues("OrganisationName")[0].String();
                    admissionFormDetail.Designation = provider.FormData.GetValues("Designation")[0].String();
                    admissionFormDetail.TotalExperienceInMonth = provider.FormData.GetValues("TotalExperienceInMonth")[0].String();
                    admissionFormDetail.TotalExperienceInYear = provider.FormData.GetValues("TotalExperienceInYear")[0].String();

                    #endregion

                    #endregion

                    #region Documents Upload

                    if (provider.FileData[0] != null)
                    {
                        admissionFormDetail.Photo = "";
                    }
                    else
                    {
                        message = "Please upload the photo.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }

                    if (provider.FileData[1] != null)
                    {
                        admissionFormDetail.SignaturePath = "";
                    }
                    else
                    {
                        message = "Please upload the signature image.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }

                    if (provider.FileData[2] != null)
                    {
                        admissionFormDetail.ParentSignaturePath = "";
                    }
                    else
                    {
                        message = "Please upload the parent signature image.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }

                    #endregion

                    #region Education Details

                    #region 

                    if (provider.FormData.AllKeys.Contains("isSvt"))
                    {
                        if (provider.FormData.GetValues("isSvt")[0].String() == "YES")
                        {
                            if (provider.FormData.AllKeys.Contains("IsSvtKnowRefrence"))
                            {
                                admissionFormDetail.IsSvtKnowRefrence = provider.FormData.GetValues("IsSvtKnowRefrence")[0].String();
                            }
                            else
                            {
                                message = "Please select svt college.";
                                return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                            }
                            admissionFormDetail.IsSvtStudentFrom = true;
                        }
                        else
                        {
                            admissionFormDetail.IsSvtStudentFrom = false;
                            admissionFormDetail.IsSvtKnowRefrence = "";
                        }
                    }
                    else
                    {
                        admissionFormDetail.KnowAboutCourse = "";
                    }

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("KnowAboutCourse")[0].String()))
                    {
                        message = "Please enter Total Experience In Year.";
                        return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.KnowAboutCourse = provider.FormData.GetValues("KnowAboutCourse")[0].String();
                        if (admissionFormDetail.KnowAboutCourse == "ANY OTHER")
                        {
                            admissionFormDetail.OtherSpecifyHowYouknowCourses = provider.FormData.GetValues("OtherSpecifyHowYouknowCourses")[0].String();
                        }
                    }
                    #endregion

                    #region Set Education Table Data

                    if (!string.IsNullOrEmpty(provider.FormData.GetValues("CourseName")[0].String()))
                    {
                        var courseName = provider.FormData.GetValues("CourseName")[0].String();

                        #region  PG Diploma Detail 
                        admissionFormDetail.PGYearofPassing = provider.FormData.GetValues("PGYearofPassing")[0].String();
                        admissionFormDetail.PGSchoolName = provider.FormData.GetValues("PGSchoolName")[0].String();
                        admissionFormDetail.PGMedium = provider.FormData.GetValues("PGMedium")[0].String();
                        admissionFormDetail.PGBoardName = provider.FormData.GetValues("PGBoardName")[0].String();
                        admissionFormDetail.PGGrade = provider.FormData.GetValues("PGGrade")[0].String();
                        admissionFormDetail.PGTotalPercent = provider.FormData.GetValues("PGTotalPercent")[0].String();
                        #endregion

                        #region if Bachelor detail also puted then save here
                        if (!string.IsNullOrEmpty(provider.FormData.GetValues("ExaminationType")[0].String()))
                        {
                            if (provider.FormData.GetValues("ExaminationType")[0].String() == "OTHER")
                            {
                                admissionFormDetail.OtherExaminationType = provider.FormData.GetValues("OtherExaminationType")[0].String();
                            }
                            else
                            {
                                admissionFormDetail.OtherExaminationType = provider.FormData.GetValues("OtherExaminationType")[0].String();
                            }
                        }

                        admissionFormDetail.ExaminationType = provider.FormData.GetValues("ExaminationType")[0].String();
                        admissionFormDetail.BachelorYearofPassing = provider.FormData.GetValues("BachelorYearofPassing")[0].String();
                        admissionFormDetail.BachelorSchoolName = provider.FormData.GetValues("BachelorSchoolName")[0].String();
                        admissionFormDetail.BachelorMedium = provider.FormData.GetValues("BachelorMedium")[0].String();
                        admissionFormDetail.BachelorBoardName = provider.FormData.GetValues("BachelorBoardName")[0].String();
                        admissionFormDetail.BachelorGrade = provider.FormData.GetValues("BachelorGrade")[0].String();
                        admissionFormDetail.BachelorTotalPercent = provider.FormData.GetValues("BachelorTotalPercent")[0].String();
                        #endregion

                        #region set if HSC Detail Available

                        admissionFormDetail.HscYearofPassing = provider.FormData.GetValues("HscYearofPassing")[0].String();
                        admissionFormDetail.HscSchoolName = provider.FormData.GetValues("HscSchoolName")[0].String();
                        admissionFormDetail.HscMedium = provider.FormData.GetValues("HscMedium")[0].String();
                        admissionFormDetail.HscBoardName = provider.FormData.GetValues("HscBoardName")[0].String();
                        admissionFormDetail.HscTotalPercent = provider.FormData.GetValues("HscTotalPercent")[0].String();
                        admissionFormDetail.HscGrade = provider.FormData.GetValues("HscGrade")[0].String();

                        #endregion

                        #region  Ssc Detail 
                        admissionFormDetail.SscYearofPassing = provider.FormData.GetValues("SscYearofPassing")[0].String();
                        admissionFormDetail.SscSchoolName = provider.FormData.GetValues("SscSchoolName")[0].String();
                        admissionFormDetail.SscMedium = provider.FormData.GetValues("SscMedium")[0].String();
                        admissionFormDetail.SscBoardName = provider.FormData.GetValues("SscBoardName")[0].String();
                        admissionFormDetail.SscTotalPercent = provider.FormData.GetValues("SscTotalPercent")[0].String();
                        admissionFormDetail.SscGrade = provider.FormData.GetValues("SscGrade")[0].String();
                        #endregion

                        if (provider.FormData.GetValues("ExaminationType")[0].String() == "OTHER")
                        {
                            if (string.IsNullOrEmpty(provider.FormData.GetValues("OtherExaminationType")[0].String()))
                            {
                                message = "Please enter other examination type."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                            }
                        }

                        if (isSubmit)
                        {
                            if (courseName == "MASTERS IN SPECIALIZED DEITETICS" || courseName == "MASTERS IN FASHION DESIGN")
                            {
                                #region Check Validation for greater than 55

                                if (!string.IsNullOrEmpty(provider.FormData.GetValues("PGTotalPercent")[0].String()))
                                {
                                    int pgPer = provider.FormData.GetValues("PGTotalPercent")[0].ToInteger();
                                    if (pgPer < 55 || pgPer > 100)
                                    {
                                        message = "PG Diploma percentage must be greater than or equal to 55."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                                    }
                                }

                                #endregion

                                #region Validate Bachelors Detail 

                                //Check Other is Remain
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("ExaminationType")[0].String())) { message = "Please enter examination type."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("BachelorYearofPassing")[0].String())) { message = "Please enter bachelor passing year."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("BachelorSchoolName")[0].String())) { message = "Please enter bachelor school name."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("BachelorMedium")[0].String())) { message = "Please enter bachelor medium."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("BachelorBoardName")[0].String())) { message = "Please enter bachelor board name."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }


                                if (!string.IsNullOrEmpty(provider.FormData.GetValues("BachelorTotalPercent")[0].String()))
                                {
                                    int bcPer = provider.FormData.GetValues("BachelorTotalPercent")[0].ToInteger();
                                    if (bcPer < 60 || bcPer > 100)
                                    {
                                        message = "Bachelor percentage must be require greater than or eqal to 60."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                                    }
                                }

                                #endregion

                                #region Validate Hsc Detail 
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("HscYearofPassing")[0].String())) { message = "Please enter hsc passing year."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("HscSchoolName")[0].String())) { message = "Please enter hsc school name."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("HscMedium")[0].String())) { message = "Please enter hsc medium."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("HscBoardName")[0].String())) { message = "Please enter hsc board name."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("HscTotalPercent")[0].String())) { message = "Please enter hsc total percent."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                #endregion

                                #region Validate Ssc Detail 
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("SscYearofPassing")[0].String())) { message = "Please enter ssc  passing year."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("SscSchoolName")[0].String())) { message = "Please enter ssc school name."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("SscMedium")[0].String())) { message = "Please enter ssc medium."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("SscBoardName")[0].String())) { message = "Please enter ssc board name."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("SscTotalPercent")[0].String())) { message = "Please enter ssc total percent."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                #endregion
                            }
                            else if (courseName == "DIPLOMA IN FASHION DESIGN")
                            {
                                #region Validate Hsc Detail 
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("HscYearofPassing")[0].String())) { message = "Please enter hsc passing year."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("HscSchoolName")[0].String())) { message = "Please enter hsc school name."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("HscMedium")[0].String())) { message = "Please enter hsc medium."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("HscBoardName")[0].String())) { message = "Please enter hsc board name."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("HscTotalPercent")[0].String())) { message = "Please enter hsc total percent."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                #endregion

                                #region Validate Ssc Detail 
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("SscYearofPassing")[0].String())) { message = "Please enter ssc  passing year."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("SscSchoolName")[0].String())) { message = "Please enter ssc school name."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("SscMedium")[0].String())) { message = "Please enter ssc medium."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("SscBoardName")[0].String())) { message = "Please enter ssc board name."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("SscTotalPercent")[0].String())) { message = "Please enter ssc total percent."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                #endregion
                            }
                            else if (courseName == "CERTIFICATE AND DIPLOMA IN COMPUTER AIDED INTERIOR DESIGN MANAGEMENT")
                            {
                                #region Validate Ssc Detail 
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("SscYearofPassing")[0].String())) { message = "Please enter ssc  passing year."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("SscSchoolName")[0].String())) { message = "Please enter ssc school name."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("SscMedium")[0].String())) { message = "Please enter ssc medium."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("SscBoardName")[0].String())) { message = "Please enter ssc board name."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                if (string.IsNullOrEmpty(provider.FormData.GetValues("SscTotalPercent")[0].String())) { message = "Please enter ssc total percent."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message); }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    #endregion

                    #region Other Information

                    if (isSubmit && string.IsNullOrEmpty(provider.FormData.GetValues("HobbiesOrSpecailInterest")[0].String()))
                    {
                        message = "Please enter Hobbies Or Specail Interest."; return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
                    }
                    else
                    {
                        admissionFormDetail.HobbiesOrSpecailInterest = provider.FormData.GetValues("HobbiesOrSpecailInterest")[0].String();
                    }

                    admissionFormDetail.HonorPrizeName = provider.FormData.GetValues("HonorPrizeName")[0].String();
                    admissionFormDetail.Note = provider.FormData.GetValues("Note")[0].String();

                    #endregion 

                    #region Set some value 
                    admissionFormDetail.LastModified = DateTime.Now;
                    admissionFormDetail.IsDuplicateAadhar = false;

                    #endregion

                    #region Save Code

                    using (ServiceContext service = new ServiceContext())
                    {
                        var id = service.Save(admissionFormDetail);
                        if (id > 0)
                        {
                            var formData = service.SelectObject<AdmissionFormDetail>(id);

                            #region Upload File on Folder

                            #region  Photo Upload

                            if (provider.FileData[0] != null)
                            {
                                MultipartFileData photo = provider.FileData[0];
                                if (Regex.Replace(photo.Headers.ContentDisposition.FileName.Split('.')[0], @"[^\w\d]", "") != "")
                                {
                                    string fName = photo.Headers.ContentDisposition.FileName;
                                    string[] fPartData = fName.Split('.');

                                    if (fPartData.Length > 1)
                                    {
                                        string newpath = AdmissionForm.Infrastructure.ProjectConfiguration.PhotoPath;
                                        string extantion = Regex.Replace(fPartData[fPartData.Length - 1], @"[^\w\d]", "");
                                        string photonewpath = id + "_" + formData.FirstName + "_" + formData.LastName + "_Photo_" + Convert.ToDateTime(formData.DateRegistered).ToString("ddmmyyyy_hhmmss") + "." + extantion;
                                        string photonewtThumbpath = id + "_" + formData.FirstName + "_" + admissionFormDetail.LastName + "_Photo_Thumb_" + Convert.ToDateTime(formData.DateRegistered).ToString("ddmmyyyy_hhmmss") + "." + extantion;
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
                                        formData.Photo = photonewpath;
                                    }
                                }
                                else
                                {
                                    formData.Photo = provider.FormData.GetValues("hdnPhoto")[0].String();
                                }
                            }
                            else
                            {
                                formData.Photo = provider.FormData.GetValues("hdnPhoto")[0].String();
                            }

                            #endregion

                            #region Signature Photo

                            if (provider.FileData[1] != null)
                            {
                                MultipartFileData signature = provider.FileData[1];
                                if (Regex.Replace(signature.Headers.ContentDisposition.FileName.Split('.')[0], @"[^\w\d]", "") != "")
                                {
                                    string fsName = signature.Headers.ContentDisposition.FileName;
                                    string[] fsPartData = fsName.Split('.');

                                    if (fsPartData.Length > 1)
                                    {
                                        string newpath = AdmissionForm.Infrastructure.ProjectConfiguration.SignaturePath;
                                        string extantion = Regex.Replace(fsPartData[fsPartData.Length - 1], @"[^\w\d]", "");// Regex.Replace(signature.Headers.ContentDisposition.FileName.Split('.')[1], @"[^\w\d]", "");
                                        string signaturenewpath = id + "_" + formData.FirstName + "_" + formData.LastName + "_Sign_" + Convert.ToDateTime(formData.DateRegistered).ToString("ddmmyyyy_hhmmss") + "." + extantion;
                                        string signaturenewThumbpath = id + "_" + formData.FirstName + "_" + admissionFormDetail.LastName + "_Sign_Thumb_" + Convert.ToDateTime(formData.DateRegistered).ToString("ddmmyyyy_hhmmss") + "." + extantion;
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
                                        formData.SignaturePath = signaturenewpath;
                                    }
                                }
                                else
                                {
                                    formData.SignaturePath = provider.FormData.GetValues("hdnSignature")[0].String();
                                }
                            }
                            else
                            {
                                formData.SignaturePath = provider.FormData.GetValues("hdnSignature")[0].String();
                            }
                            #endregion

                            #region Parent Signature Photo

                            if (provider.FileData[2] != null)
                            {
                                MultipartFileData parentSignature = provider.FileData[2];
                                if (Regex.Replace(parentSignature.Headers.ContentDisposition.FileName.Split('.')[0], @"[^\w\d]", "") != "")
                                {
                                    string fsName = parentSignature.Headers.ContentDisposition.FileName;
                                    string[] fsPartData = fsName.Split('.');

                                    if (fsPartData.Length > 1)
                                    {
                                        string newpath = ProjectConfiguration.ParentSignaturePath;
                                        string extantion = Regex.Replace(fsPartData[fsPartData.Length - 1], @"[^\w\d]", "");
                                        string parentSignaturenewpath = id + "_" + formData.FirstName + "_" + formData.LastName + "_Parent_Sign_" + Convert.ToDateTime(formData.DateRegistered).ToString("ddmmyyyy_hhmmss") + "." + extantion;
                                        string parentSignaturenewThumbpath = id + "_" + formData.FirstName + "_" + admissionFormDetail.LastName + "_Parent_Sign_Thumb_" + Convert.ToDateTime(formData.DateRegistered).ToString("ddmmyyyy_hhmmss") + "." + extantion;
                                        FileInfo f1 = new FileInfo(parentSignature.LocalFileName);
                                        if (f1.Exists)
                                        {
                                            if (!Directory.Exists(newpath))
                                            {
                                                Directory.CreateDirectory(newpath);
                                            }
                                            ImageCompress imgCompress = ImageCompress.GetImageCompressObject;
                                            imgCompress.GetImage = new System.Drawing.Bitmap(parentSignature.LocalFileName);
                                            imgCompress.Height = 566;
                                            imgCompress.Width = 566;
                                            imgCompress.Save(parentSignaturenewpath, newpath);
                                            imgCompress.Save(parentSignaturenewThumbpath, newpath);
                                        }
                                        formData.ParentSignaturePath = parentSignaturenewpath;
                                    }
                                }
                                else
                                {
                                    formData.ParentSignaturePath = provider.FormData.GetValues("hdnParentSignature")[0].String();
                                }
                            }
                            else
                            {
                                formData.ParentSignaturePath = provider.FormData.GetValues("hdnParentSignature")[0].String();
                            }
                            #endregion

                            #endregion

                            var repatedId = service.Save(formData);
                            if (admissionFormDetail.IsSubmitted == false)
                            {
                                var encryptedId = AdmissionForm.Infrastructure.EncryptionDecryption.GetEncrypt(id.ToString());
                                return Request.CreateResponse(HttpStatusCode.OK, encryptedId + ":~" + id);
                            }
                            else
                            {
                                DownloadPDF(formData);
                                string path = "data/PDF/" + id + "_" + formData.FirstName + "_" + formData.LastName + "_" + Convert.ToDateTime(formData.DateRegistered).ToString("ddmmyyyy_hhmmss") + ".pdf";
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
                catch (Exception e)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
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
            return CheckFormExpireDate();
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
        public AdmissionFormDetail GetEditDetail(string id)
        {
            AdmissionFormDetail admissionFormDetail = new AdmissionFormDetail();

            if (!GetFormExpireDate())
            {
                using (ServiceContext service = new ServiceContext())
                {
                    if (id != null)
                    {
                        var newid = EncryptionDecryption.GetDecrypt(id);
                        admissionFormDetail.Id = newid.ToInteger();
                    }
                    var admission = service.Search(admissionFormDetail, 1, null, null).FirstOrDefault();

                    admission.StudentPdfURL = "data/PDF/" + admission.Id + "_" + admission.FirstName + "_" + admission.LastName + "_" + Convert.ToDateTime(admission.DateRegistered).ToString("ddmmyyyy_hhmmss") + ".pdf";
                    return admission;
                }
            }
            return admissionFormDetail;
        }

        #endregion

        #region Get Detail and PDF Part

        [HttpGet]
        public AdmissionFormDetail GetStudentDetail(string formNumber, string aadharNumber, string lastName, DateTime? dateofBirth)
        {
            AdmissionFormDetail admissionFormDetail = new AdmissionFormDetail();
            if (!GetFormExpireDate())
            {
                using (ServiceContext service = new ServiceContext())
                {
                    int fNumber = 0;
                    fNumber = formNumber.ToInteger();
                    if (fNumber > 0)
                    {
                        admissionFormDetail.Id = fNumber;
                    }
                    if (aadharNumber != null)
                    {
                        admissionFormDetail.AadharNumber = aadharNumber;
                    }
                    if (lastName != null)
                    {
                        admissionFormDetail.LastName = lastName;
                    }
                    if (dateofBirth != null)
                    {
                        admissionFormDetail.Dob = dateofBirth;
                    }
                    var admissionDetail = service.Search(admissionFormDetail, 1, null, null).FirstOrDefault();

                    if (admissionDetail != null)
                    {
                        AdmissionFormDetail selectedvalue = new AdmissionFormDetail();
                        selectedvalue.Id = admissionDetail.Id;
                        selectedvalue.IsSubmitted = admissionDetail.IsSubmitted;
                        selectedvalue.Photo = "data/Photos/" + admissionDetail.Photo;
                        selectedvalue.FirstName = admissionDetail.FirstName;
                        selectedvalue.LastName = admissionDetail.LastName;
                        selectedvalue.EncryptedId = AdmissionForm.Infrastructure.EncryptionDecryption.GetEncrypt(selectedvalue.Id.ToString());
                        selectedvalue.StudentPdfURL = "data/Pdf/" + admissionDetail.Id + "_" + admissionDetail.FirstName + "_" + admissionDetail.LastName + "_" + Convert.ToDateTime(admissionDetail.DateRegistered).ToString("ddmmyyyy_hhmmss") + ".pdf";
                        return selectedvalue;
                    }
                    return admissionDetail;
                }
            }
            return admissionFormDetail;
        }

        public static void DownloadPDF(AdmissionFormDetail admissionFormDetailModel)
        {
            string HTMLContent = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Templates/PDFHtml.html"));

            if (admissionFormDetailModel != null)
            {
                #region 

                HTMLContent = HTMLContent.Replace("{{ Fno }}", admissionFormDetailModel.Id.String());
                HTMLContent = HTMLContent.Replace("{{ Surname }}", admissionFormDetailModel.LastName.String());
                HTMLContent = HTMLContent.Replace("{{ Firstname }}", admissionFormDetailModel.FirstName.String());
                HTMLContent = HTMLContent.Replace("{{ Middlename }}", admissionFormDetailModel.FatherName.String());
                HTMLContent = HTMLContent.Replace("{{ Mothername }}", admissionFormDetailModel.MotherFirstName.String());
                HTMLContent = HTMLContent.Replace("{{ FullName }}", admissionFormDetailModel.LastName.String() + " " + admissionFormDetailModel.FirstName.String() + " " + admissionFormDetailModel.FatherName.String());
                HTMLContent = HTMLContent.Replace("{{ CourseName }}", admissionFormDetailModel.CourseName.String());

                if (admissionFormDetailModel.IsSvtStudentFrom == true)
                {
                    HTMLContent = HTMLContent.Replace("{{ IsCol }}", "YES");
                    HTMLContent = HTMLContent.Replace("{{ IsOtherCol }}", " ");
                    HTMLContent = HTMLContent.Replace("{{ IsSvtCollegeName }}", admissionFormDetailModel.IsSvtKnowRefrence.String());
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ IsCol }}", " ");
                    HTMLContent = HTMLContent.Replace("{{ IsOtherCol }}", "YES");
                    HTMLContent = HTMLContent.Replace("{{ IsSvtCollegeName }}", " ");
                }

                if (admissionFormDetailModel.DateRegistered.HasValue)
                {
                    HTMLContent = HTMLContent.Replace("{{ Date }}", admissionFormDetailModel.DateRegistered.Value.ToString(ProjectConfiguration.DateFormatString));
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ Date }}", "");
                }

                #region Set Photo Path

                string ImagePath = string.Empty;
                string Image = string.Empty;

                if (!string.IsNullOrEmpty(admissionFormDetailModel.Photo))
                {
                    ImagePath = ProjectConfiguration.PhotoPath + admissionFormDetailModel.Photo;
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

                if (admissionFormDetailModel.CalculatedAge < 18)
                {
                    if (!string.IsNullOrEmpty(admissionFormDetailModel.ParentSignaturePath))
                    {
                        ImagePath = ProjectConfiguration.ParentSignaturePath + admissionFormDetailModel.ParentSignaturePath;
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
                }
                else if (admissionFormDetailModel.CalculatedAge > 18)
                {
                    if (!string.IsNullOrEmpty(admissionFormDetailModel.SignaturePath))
                    {
                        ImagePath = ProjectConfiguration.SignaturePath + admissionFormDetailModel.SignaturePath;
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
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ Sign }}", "");
                }

                #endregion

                #region Bank Detail

                HTMLContent = HTMLContent.Replace("{{ BankName }}", admissionFormDetailModel.BankName.String());
                HTMLContent = HTMLContent.Replace("{{ BankAddress }}", admissionFormDetailModel.BankAddress.String());
                HTMLContent = HTMLContent.Replace("{{ BankAccountNumber }}", admissionFormDetailModel.BankAccountNumber.String());
                HTMLContent = HTMLContent.Replace("{{ AccountType }}", admissionFormDetailModel.AccountType.String());
                HTMLContent = HTMLContent.Replace("{{ IFSCCode }}", admissionFormDetailModel.AccountType.String());
                HTMLContent = HTMLContent.Replace("{{ MICRNumber }}", admissionFormDetailModel.MICRNumber.String());

                #endregion

                #region Set Category

                HTMLContent = HTMLContent.Replace("{{ Religion }}", admissionFormDetailModel.Religion.String());
                if (admissionFormDetailModel.Category == "OPEN")
                {
                    HTMLContent = HTMLContent.Replace("{{ Category }}", "OPEN");
                    HTMLContent = HTMLContent.Replace("{{ SubCaste }}", " ");
                    HTMLContent = HTMLContent.Replace("{{ Caste }}", " ");
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ Category }}", "RESERVED");
                    HTMLContent = HTMLContent.Replace("{{ Caste }}", admissionFormDetailModel.Caste.String());
                    HTMLContent = HTMLContent.Replace("{{ SubCaste }}", admissionFormDetailModel.SubCaste.String());
                }

                #endregion

                HTMLContent = HTMLContent.Replace("{{ FLName }}", admissionFormDetailModel.FatherLastName.String());
                HTMLContent = HTMLContent.Replace("{{ FFName }}", admissionFormDetailModel.FatherFirstName.String());
                HTMLContent = HTMLContent.Replace("{{ FMName }}", admissionFormDetailModel.FatherMiddleName.String());
                HTMLContent = HTMLContent.Replace("{{ MLName }}", admissionFormDetailModel.MotherLastName.String());
                HTMLContent = HTMLContent.Replace("{{ MFName }}", admissionFormDetailModel.MotherFirstName.String());
                HTMLContent = HTMLContent.Replace("{{ MMName }}", admissionFormDetailModel.MotherMiddleName.String());

                if (admissionFormDetailModel.Dob.HasValue)
                {
                    HTMLContent = HTMLContent.Replace("{{ Dob }}", admissionFormDetailModel.Dob.Value.ToString(ProjectConfiguration.DateFormatString));
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ Dob }}", "");
                }

                HTMLContent = HTMLContent.Replace("{{ Age }}", admissionFormDetailModel.CalculatedAge.String());
                HTMLContent = HTMLContent.Replace("{{ PanNo }}", admissionFormDetailModel.PanNumber.String());
                HTMLContent = HTMLContent.Replace("{{ Nationality }}", admissionFormDetailModel.Nationality.String());
                HTMLContent = HTMLContent.Replace("{{ MTongue }}", admissionFormDetailModel.MotherTongue.String());
                HTMLContent = HTMLContent.Replace("{{ AadharCard }}", admissionFormDetailModel.AadharNumber.String());
                HTMLContent = HTMLContent.Replace("{{ Address1 }}", admissionFormDetailModel.CurrentAddress.String());
                HTMLContent = HTMLContent.Replace("{{ Address2 }}", admissionFormDetailModel.PermanentAddress.String());
                HTMLContent = HTMLContent.Replace("{{ MNum }}", admissionFormDetailModel.MobileNumber.String());
                HTMLContent = HTMLContent.Replace("{{ Email }}", admissionFormDetailModel.Email.String());

                HTMLContent = HTMLContent.Replace("{{ OrganisationName }}", admissionFormDetailModel.OrganisationName.String());
                HTMLContent = HTMLContent.Replace("{{ Designation }}", admissionFormDetailModel.Designation.String());

                if (!string.IsNullOrEmpty(admissionFormDetailModel.TotalExperienceInYear.String()))
                {
                    HTMLContent = HTMLContent.Replace("{{ Year }}", admissionFormDetailModel.TotalExperienceInYear.String() + " Year ");
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ Year }}", " ");
                }

                if (!string.IsNullOrEmpty(admissionFormDetailModel.TotalExperienceInMonth.String()))
                {
                    HTMLContent = HTMLContent.Replace("{{ Month }}", admissionFormDetailModel.TotalExperienceInMonth.String() + " Month ");
                }
                else
                {
                    HTMLContent = HTMLContent.Replace("{{ Month }}", " ");
                }

                HTMLContent = HTMLContent.Replace("{{ GuardianMotherName }}", admissionFormDetailModel.GuardianMotherName.String());
                HTMLContent = HTMLContent.Replace("{{ GuardianFatherName }}", admissionFormDetailModel.GuardianFatherName.String());
                HTMLContent = HTMLContent.Replace("{{ OccupationofMother }}", admissionFormDetailModel.OccupationofMother.String());
                HTMLContent = HTMLContent.Replace("{{ EducationofMother }}", admissionFormDetailModel.EducationofMother.String());
                HTMLContent = HTMLContent.Replace("{{ EducationofFather }}", admissionFormDetailModel.EducationofFather.String());
                HTMLContent = HTMLContent.Replace("{{ GuardianAddress }}", admissionFormDetailModel.GuardianAddress.String());
                HTMLContent = HTMLContent.Replace("{{ AnnualIncome }}", admissionFormDetailModel.AnnualIncome.String());
                HTMLContent = HTMLContent.Replace("{{ TelNo }}", admissionFormDetailModel.GuardianTelephoneNo.String());
                HTMLContent = HTMLContent.Replace("{{ Office }}", admissionFormDetailModel.GuardianOffice.String());
                HTMLContent = HTMLContent.Replace("{{ Mobile }}", admissionFormDetailModel.GuardianMobile.String());
                HTMLContent = HTMLContent.Replace("{{ EmrContact }}", admissionFormDetailModel.GuardianEmergencyConactNo.String());
                HTMLContent = HTMLContent.Replace("{{ GuardianEmail }}", admissionFormDetailModel.GuardianEmail.String());
                HTMLContent = HTMLContent.Replace("{{ NativePlace }}", admissionFormDetailModel.NativePlaceAddress.String());

                HTMLContent = HTMLContent.Replace("{{ ResContactNo }}", admissionFormDetailModel.ResContactNo.String());
                HTMLContent = HTMLContent.Replace("{{ OccupationofFather }}", admissionFormDetailModel.OccupationofFather.String());

                #endregion

                #region Education Table Detail

                string EcucationDetailTR = string.Empty;

                if (!string.IsNullOrEmpty(admissionFormDetailModel.PGYearofPassing) && !string.IsNullOrEmpty(admissionFormDetailModel.PGTotalPercent))
                {
                    EcucationDetailTR += @"<tr><th class='borderTable'>PG DIPLOMA</th>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.PGYearofPassing.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.PGSchoolName.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.PGMedium.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.PGBoardName.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.PGTotalPercent.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.PGGrade.String() + "</td></tr>";
                }
                if (!string.IsNullOrEmpty(admissionFormDetailModel.BachelorYearofPassing) && !string.IsNullOrEmpty(admissionFormDetailModel.BachelorTotalPercent))
                {
                    EcucationDetailTR += @"<tr>";

                    if (admissionFormDetailModel.ExaminationType == "OTHER")
                    {
                        EcucationDetailTR += @"<th class='borderTable'>" + admissionFormDetailModel.OtherExaminationType.String() + "</th>";
                    }
                    else
                    {
                        EcucationDetailTR += @"<th class='borderTable'>" + admissionFormDetailModel.ExaminationType.String() + "</th>";
                    }
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.BachelorYearofPassing.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.BachelorSchoolName.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.BachelorMedium.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.BachelorBoardName.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.BachelorTotalPercent.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.BachelorGrade.String() + "</td></tr>";
                }
                if (!string.IsNullOrEmpty(admissionFormDetailModel.HscYearofPassing) && !string.IsNullOrEmpty(admissionFormDetailModel.HscTotalPercent))
                {
                    EcucationDetailTR += @"<tr><th class='borderTable'>H.S.C.</th>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.HscYearofPassing.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.HscSchoolName.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.HscMedium.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.HscBoardName.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.HscTotalPercent.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.HscGrade.String() + "</td></tr>";
                }
                if (!string.IsNullOrEmpty(admissionFormDetailModel.HscYearofPassing) && !string.IsNullOrEmpty(admissionFormDetailModel.HscTotalPercent))
                {
                    EcucationDetailTR += @"<tr><tH class='borderTable'>S.S.C.</th>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.SscYearofPassing.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.SscSchoolName.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.SscMedium.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.SscBoardName.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.SscTotalPercent.String() + "</td>";
                    EcucationDetailTR += @"<td class='borderTable'>" + admissionFormDetailModel.SscGrade.String() + "</td></tr>";
                }

                HTMLContent = HTMLContent.Replace("{{ EducationDetails }}", EcucationDetailTR);

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

            }
            GetPDF(HTMLContent, admissionFormDetailModel.FirstName, admissionFormDetailModel.LastName, admissionFormDetailModel.Id.ToString(), admissionFormDetailModel.DateRegistered);
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

        #endregion

        #region check Aadhar Already Exist

        [HttpGet]
        public bool ValidateAlreadyExistAadharCard(string aadharno, string id = "")
        {
            AdmissionFormDetail entityModel = new AdmissionFormDetail();

            string message = string.Empty;
            if (!string.IsNullOrEmpty(aadharno))
            {
                if (!GetFormExpireDate())
                {
                    using (ServiceContext service = new ServiceContext())
                    {
                        entityModel.AadharNumber = aadharno;

                        bool isExists = service.CheckDuplicate("[AdmissionFormDetail]", "AadharNumber", aadharno, "Id", id);
                        return isExists;
                    }
                }
            }
            return false;
        }

        #endregion
    }
}