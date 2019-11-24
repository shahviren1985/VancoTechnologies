
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using SVT.Business.Model;
using SVT.API.Helpers;
using SVT.Business.Services;
using DocumentFormat.OpenXml.Packaging;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace SVT.API.Controllers
{
    public class ReportController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GenerateReport(ReporIdEnum reportId)
        {
            FileStream fileStream = null;
            try
            {
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                DataTable dt = null;
                HttpResponseMessage response = new HttpResponseMessage();
                ServiceContext serviceRef = new ServiceContext();
                bool isTrue = false;
                switch (reportId)
                {
                    #region Case Round 1
                    case ReporIdEnum.R1:
                        fileName = "Developmental_Counselling_OpenInternal_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Developmental Counselling", true, true, 1);
                        break;
                    case ReporIdEnum.R2:
                        fileName = "Developmental_Counselling_ReservedInternal_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Developmental Counselling", true, false, 1);
                        break;
                    case ReporIdEnum.R3:
                        fileName = "Developmental Counselling_Open_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Developmental Counselling", false, true, 1);
                        break;
                    case ReporIdEnum.R4:
                        fileName = "Developmental_Counselling_ExternalReserved_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Developmental Counselling", false, false, 1);
                        break;

                    case ReporIdEnum.R5:
                        fileName = "Early_Childhood_care_&_Education_OpenInternal_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Early Childhood care & Education", true, true, 1);
                        break;
                    case ReporIdEnum.R6:
                        fileName = "Early_Childhood_care_&_Education_ReservedInternal_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Early Childhood care & Education", true, false, 1);
                        break;
                    case ReporIdEnum.R7:
                        fileName = "Early_Childhood_care_&_Education_ExternalOpen_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Early Childhood care & Education", false, true, 1);
                        break;
                    case ReporIdEnum.R8:
                        fileName = "Early_Childhood_care_&_Education_ExternalReserved_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Early Childhood care & Education", false, false, 1);
                        break;

                    case ReporIdEnum.R9:
                        fileName = "Interior_Design_&_Resource_Management_OpenInternal_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Interior Design & Resource Management", true, true, 1);
                        break;
                    case ReporIdEnum.R10:
                        fileName = "Interior_Design_&_Resource_Management_ReservedInternal_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Interior Design & Resource Management", true, false, 1);
                        break;
                    case ReporIdEnum.R11:
                        fileName = "Interior_Design_&_Resource_Management_ExternalOpen_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Interior Design & Resource Management", false, true, 1);
                        break;
                    case ReporIdEnum.R12:
                        fileName = "Interior_Design_&_Resource_Management_ExternalReserved_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Interior Design & Resource Management", false, false, 1);
                        break;

                    case ReporIdEnum.R13:
                        fileName = "Textiles_&_Apparel_Designing_OpenInternal_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Textiles & Apparel Designing", true, true, 1);
                        break;
                    case ReporIdEnum.R14:
                        fileName = "Textiles_&_Apparel_Designing_ReservedInternal_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Textiles & Apparel Designing", true, false, 1);
                        break;
                    case ReporIdEnum.R15:
                        fileName = "Textiles_&_Apparel_Designing_ExternalOpen_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Textiles & Apparel Designing", false, true, 1);
                        break;
                    case ReporIdEnum.R16:
                        fileName = "Textiles_&_Apparel_Designing_ExternalReserved_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Textiles & Apparel Designing", false, false, 1);
                        break;

                    case ReporIdEnum.R17:
                        fileName = "Hospitality_&_Tourism_Management_OpenInternal_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Hospitality & Tourism Management", true, true, 1);
                        break;
                    case ReporIdEnum.R18:
                        fileName = "Hospitality_&_Tourism_Management_ReservedInternal_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Hospitality & Tourism Management", true, false, 1);
                        break;
                    case ReporIdEnum.R19:
                        fileName = "Hospitality_&_Tourism_Management_ExternalOpen_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Hospitality & Tourism Management", false, true, 1);
                        break;
                    case ReporIdEnum.R20:
                        fileName = "Hospitality_&_Tourism_Management_ExternalReserved_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Hospitality & Tourism Management", false, false, 1);
                        break;

                    case ReporIdEnum.R21:
                        fileName = "Mass_Communication_&_Extension_OpenInternal_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Mass Communication & Extension", true, true, 1);
                        break;
                    case ReporIdEnum.R22:
                        fileName = "Mass_Communication_&_Extension_ReservedInternal_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Mass Communication & Extension", true, false, 1);
                        break;
                    case ReporIdEnum.R23:
                        fileName = "Mass_Communication_&_Extension_ExternalOpen_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Mass Communication & Extension", false, true, 1);
                        break;
                    case ReporIdEnum.R24:
                        fileName = "Mass_Communication_&_Extension_ExternalReserved_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Mass Communication & Extension", false, false, 1);
                        break;
                    case ReporIdEnum.R25:
                        fileName = "Food_Nutrition_and_Dietetics_OpenInternal_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Food, Nutrition and Dietetics", true, true, 1);
                        break;
                    case ReporIdEnum.R26:
                        fileName = "Food_Nutrition_and_Dietetics_ReservedInternal_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Food, Nutrition and Dietetics", true, false, 1);
                        break;
                    case ReporIdEnum.R27:
                        fileName = "Food_Nutrition_and_Dietetics_ExternalOpen_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Food, Nutrition and Dietetics", false, true, 1);
                        break;
                    case ReporIdEnum.R28:
                        fileName = "Food_Nutrition_and_Dietetics_ExternalReserved_Round1_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Food, Nutrition and Dietetics", false, false, 1);
                        break;
                    #endregion

                    #region Case Round 2
                    case ReporIdEnum.R29:
                        fileName = "Developmental_Counselling_OpenInternal_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Developmental Counselling", true, true, 2);
                        break;
                    case ReporIdEnum.R30:
                        fileName = "Developmental_Counselling_ReservedInternal_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Developmental Counselling", true, false, 2);
                        break;
                    case ReporIdEnum.R31:
                        fileName = "Developmental_Counselling_ExternalOpen_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Developmental Counselling", false, true, 2);
                        break;
                    case ReporIdEnum.R32:
                        fileName = "Developmental_Counselling_ExternalReserved_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Developmental Counselling", false, false, 2);
                        break;

                    case ReporIdEnum.R33:
                        fileName = "Early_Childhood_care_&_Education_OpenInternal_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Early Childhood care & Education", true, true, 2);
                        break;
                    case ReporIdEnum.R34:
                        fileName = "Early_Childhood_care_&_Education_ReservedInternal_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Early Childhood care & Education", true, false, 2);
                        break;
                    case ReporIdEnum.R35:
                        fileName = "Early_Childhood_care_&_Education_ExternalOpen_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Early Childhood care & Education", false, true, 2);
                        break;
                    case ReporIdEnum.R36:
                        fileName = "Early_Childhood_care_&_Education_ExternalReserved_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Early Childhood care & Education", false, false, 2);
                        break;

                    case ReporIdEnum.R37:
                        fileName = "Interior_Design_&_Resource_Management_OpenInternal_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Interior Design & Resource Management", true, true, 2);
                        break;
                    case ReporIdEnum.R38:
                        fileName = "Interior_Design_&_Resource_Management_ReservedInternal_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Interior Design & Resource Management", true, false, 2);
                        break;
                    case ReporIdEnum.R39:
                        fileName = "Interior_Design_&_Resource_Management_ExternalOpen_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Interior Design & Resource Management", false, true, 2);
                        break;
                    case ReporIdEnum.R40:
                        fileName = "Interior_Design_&_Resource_Management_ExternalReserved_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Interior Design & Resource Management", false, false, 2);
                        break;

                    case ReporIdEnum.R41:
                        fileName = "Textiles_&_Apparel_Designing_OpenInternal_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Textiles & Apparel Designing", true, true, 2);
                        break;
                    case ReporIdEnum.R42:
                        fileName = "Textiles_&_Apparel_Designing_ReservedInternal_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Textiles & Apparel Designing", true, false, 2);
                        break;
                    case ReporIdEnum.R43:
                        fileName = "Textiles_&_Apparel_Designing_ExternalOpen_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Textiles & Apparel Designing", false, true, 2);
                        break;
                    case ReporIdEnum.R44:
                        fileName = "Textiles_&_Apparel_Designing_ExternalReserved_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Textiles & Apparel Designing", false, false, 2);
                        break;

                    case ReporIdEnum.R45:
                        fileName = "Hospitality_&_Tourism_Management_OpenInternal_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Hospitality & Tourism Management", true, true, 2);
                        break;
                    case ReporIdEnum.R46:
                        fileName = "Hospitality_&_Tourism_Management_ReservedInternal_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Hospitality & Tourism Management", true, false, 2);
                        break;
                    case ReporIdEnum.R47:
                        fileName = "Hospitality_&_Tourism_Management_ExternalOpen_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Hospitality & Tourism Management", false, true, 2);
                        break;
                    case ReporIdEnum.R48:
                        fileName = "Hospitality_&_Tourism_Management_ExternalReserved_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Hospitality & Tourism Management", false, false, 2);
                        break;

                    case ReporIdEnum.R49:
                        fileName = "Mass_Communication_&_Extension_OpenInternal_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Mass Communication & Extension", true, true, 2);
                        break;
                    case ReporIdEnum.R50:
                        fileName = "Mass_Communication_&_Extension_ReservedInternal_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Mass Communication & Extension", true, false, 2);
                        break;
                    case ReporIdEnum.R51:
                        fileName = "Mass_Communication_&_Extension_ExternalOpen_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Mass Communication & Extension", false, true, 2);
                        break;
                    case ReporIdEnum.R52:
                        fileName = "Mass_Communication_&_Extension_ExternalReserved_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Mass Communication & Extension", false, false, 2);
                        break;
                    case ReporIdEnum.R53:
                        fileName = "Food_Nutrition_and_Dietetics_OpenInternal_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Food, Nutrition and Dietetics", true, true, 2);
                        break;
                    case ReporIdEnum.R54:
                        fileName = "Food_Nutrition_and_Dietetics_ReservedInternal_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Food, Nutrition and Dietetics", true, false, 2);
                        break;
                    case ReporIdEnum.R55:
                        fileName = "Food_Nutrition_and_Dietetics_ExternalOpen_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Food, Nutrition and Dietetics", false, true, 2);
                        break;
                    case ReporIdEnum.R56:
                        fileName = "Food_Nutrition_and_Dietetics_ExternalReserved_Round2_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Food, Nutrition and Dietetics", false, false, 2);
                        break;
                    #endregion

                    #region Case Round 3
                    case ReporIdEnum.R57:
                        fileName = "Developmental_Counselling_OpenInternal_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Developmental Counselling", true, true, 3);
                        break;
                    case ReporIdEnum.R58:
                        fileName = "Developmental_Counselling_ReservedInternal_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Developmental Counselling", true, false, 3);
                        break;
                    case ReporIdEnum.R59:
                        fileName = "Developmental_Counselling_ExternalOpen_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Developmental Counselling", false, true, 3);
                        break;
                    case ReporIdEnum.R60:
                        fileName = "Developmental_Counselling_ExternalReserved_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Developmental Counselling", false, false, 3);
                        break;

                    case ReporIdEnum.R61:
                        fileName = "Early_Childhood_care_&_Education_OpenInternal_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Early Childhood care & Education", true, true, 3);
                        break;
                    case ReporIdEnum.R62:
                        fileName = "Early_Childhood_care_&_Education_ReservedInternal_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Early Childhood care & Education", true, false, 3);
                        break;
                    case ReporIdEnum.R63:
                        fileName = "Early_Childhood_care_&_Education_ExternalOpen_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Early Childhood care & Education", false, true, 3);
                        break;
                    case ReporIdEnum.R64:
                        fileName = "Early_Childhood_care_&_Education_ExternalReserved_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Early Childhood care & Education", false, false, 3);
                        break;

                    case ReporIdEnum.R65:
                        fileName = "Interior_Design_&_Resource_Management_OpenInternal_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Interior Design & Resource Management", true, true, 3);
                        break;
                    case ReporIdEnum.R66:
                        fileName = "Interior_Design_&_Resource_Management_ReservedInternal_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Interior Design & Resource Management", true, false, 3);
                        break;
                    case ReporIdEnum.R67:
                        fileName = "Interior_Design_&_Resource_Management_ExternalOpen_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Interior Design & Resource Management", false, true, 3);
                        break;
                    case ReporIdEnum.R68:
                        fileName = "Interior_Design_&_Resource_Management_ExternalReserved_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Interior Design & Resource Management", false, false, 3);
                        break;

                    case ReporIdEnum.R69:
                        fileName = "Textiles_&_Apparel_Designing_OpenInternal_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Textiles & Apparel Designing", true, true, 3);
                        break;
                    case ReporIdEnum.R70:
                        fileName = "Textiles_&_Apparel_Designing_ReservedInternal_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Textiles & Apparel Designing", true, false, 3);
                        break;
                    case ReporIdEnum.R71:
                        fileName = "Textiles_&_Apparel_Designing_ExternalOpen_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Textiles & Apparel Designing", false, true, 3);
                        break;
                    case ReporIdEnum.R72:
                        fileName = "Textiles_&_Apparel_Designing_ExternalReserved_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Textiles & Apparel Designing", false, false, 3);
                        break;

                    case ReporIdEnum.R73:
                        fileName = "Hospitality_&_Tourism_Management_OpenInternal_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Hospitality & Tourism Management", true, true, 3);
                        break;
                    case ReporIdEnum.R74:
                        fileName = "Hospitality_&_Tourism_Management_ReservedInternal_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Hospitality & Tourism Management", true, false, 3);
                        break;
                    case ReporIdEnum.R75:
                        fileName = "Hospitality_&_Tourism_Management_ExternalOpen_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Hospitality & Tourism Management", false, true, 3);
                        break;
                    case ReporIdEnum.R76:
                        fileName = "Hospitality_&_Tourism_Management_ExternalReserved_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Hospitality & Tourism Management", false, false, 3);
                        break;

                    case ReporIdEnum.R77:
                        fileName = "Mass_Communication_&_Extension_OpenInternal_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Mass Communication & Extension", true, true, 3);
                        break;
                    case ReporIdEnum.R78:
                        fileName = "Mass_Communication_&_Extension_ReservedInternal_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Mass Communication & Extension", true, false, 3);
                        break;
                    case ReporIdEnum.R79:
                        fileName = "Mass_Communication_&_Extension_ExternalOpen_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Mass Communication & Extension", false, true, 3);
                        break;
                    case ReporIdEnum.R80:
                        fileName = "Mass_Communication_&_Extension_ExternalReserved_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Mass Communication & Extension", false, false, 3);
                        break;
                    case ReporIdEnum.R81:
                        fileName = "Food_Nutrition_and_Dietetics_OpenInternal_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Food, Nutrition and Dietetics", true, true, 3);
                        break;
                    case ReporIdEnum.R82:
                        fileName = "Food_Nutrition_and_Dietetics_ReservedInternal_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Food, Nutrition and Dietetics", true, false, 3);
                        break;
                    case ReporIdEnum.R83:
                        fileName = "Food_Nutrition_and_Dietetics_ExternalOpen_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Food, Nutrition and Dietetics", false, true, 3);
                        break;
                    case ReporIdEnum.R84:
                        fileName = "Food_Nutrition_and_Dietetics_ExternalReserved_Round3_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                        fullPath = folderPath + fileName;
                        dt = serviceRef.GenerateReports("Food, Nutrition and Dietetics", false, false, 3);
                        break;
                    #endregion
                    default:
                        break;
                }
                if (dt != null)
                {
                    OpenXmlHelper helper = new OpenXmlHelper();
                    isTrue = xmlHeaderHelper.BuildWorkbook(fullPath, dt, "Merit List report");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Datatable is empty");
                }
                if (isTrue)
                {
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

        [HttpGet]
        public HttpResponseMessage GenerateGeneralReport()
        {
            FileStream fileStream = null;
            try
            {
                string Reportname = "General Report";
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                DataTable dt = null;
                HttpResponseMessage response = new HttpResponseMessage();
                ServiceContext serviceRef = new ServiceContext();
                bool isTrue = false;

                fileName = "General_Report_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                fullPath = folderPath + fileName;
                dt = serviceRef.GenerateGeneralReport();
                if (dt != null)
                {
                    isTrue = xmlHeaderHelper.BuildWorkbook(fullPath, dt, Reportname);
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


        [HttpGet]
        public HttpResponseMessage GenerateGeneralReportWithParamters(bool isSVT,bool category)
        {
            FileStream fileStream = null;
            string ctgy = "Open";
            string SVT = "External";
            try
            {
             
                if (category)
                {
                    ctgy = "Reserved";
                }
                if (isSVT)
                {
                    SVT = "Internal";
                }

                string Reportname = "General Report";
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                DataTable dt = null;
                HttpResponseMessage response = new HttpResponseMessage();
                ServiceContext serviceRef = new ServiceContext();
                bool isTrue = false;

                fileName = SVT+"_"+ctgy+"_"+ DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                fullPath = folderPath + fileName;
                dt = serviceRef.GenerateGeneralReport2(isSVT, ctgy);
                if (dt != null)
                {
                    isTrue = xmlHeaderHelper.BuildWorkbook(fullPath, dt, Reportname);
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

        [HttpGet]
        public HttpResponseMessage GetStudentWithPagination(string firstName="",string lastName="",DateTime? dob=null,int formId=0,int pageNo=0, int pageSize=0)
        {
            ServiceContext serviceRef = new ServiceContext();
            JArrayObject response = serviceRef.GetStudentWithPagination(firstName, lastName, dob,formId,pageNo,pageSize);
            return Request.CreateResponse(HttpStatusCode.OK,response);
        }

        [HttpGet]
        public HttpResponseMessage GetStudentsByRollNumber(string course)
        {
            ServiceContext serviceRef = new ServiceContext();
            JArray jArray = serviceRef.ToJson(serviceRef.GetStudentByRollNumber(course));
            return Request.CreateResponse(HttpStatusCode.OK, jArray);
        }

        [HttpGet]
        public HttpResponseMessage GenerateStudentIdCardReport(string Specialization)
        {
            FileStream fileStream = null;
            try
            {
                string folderPath =System.Web.HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                DataTable dt = null;
                HttpResponseMessage response = new HttpResponseMessage();
                ServiceContext serviceRef = new ServiceContext();
                bool isTrue = false;

                fileName = "IdCard_Report_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                fullPath = folderPath + fileName;
                dt = serviceRef.GetStudentIdCardReport(Specialization);
                if (dt != null)
                {
                    isTrue = xmlHeaderHelper.BuildWorkbook(fullPath, dt, "Id Card Report");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Datatable is empty");
                }
                if (isTrue)
                {
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


        [HttpGet]
        public HttpResponseMessage GetGeneralElectiveReport(string Specialization)
        {
            FileStream fileStream = null;
            try
            {
                string folderPath = System.Web.HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                DataTable dt = null;
                HttpResponseMessage response = new HttpResponseMessage();
                ServiceContext serviceRef = new ServiceContext();
                bool isTrue = false;

                fileName = "GE_Report_" + Specialization + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                fullPath = folderPath + fileName;
                dt = serviceRef.GetGeneralElectiveReport(Specialization);
                if (dt != null)
                {
                    isTrue = xmlHeaderHelper.BuildWorkbook(fullPath, dt, "GE Report");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Datatable is empty");
                }
                if (isTrue)
                {
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

        [HttpGet]
        public HttpResponseMessage AdmittedEnrollmentStudentDetail()
        {
            FileStream fileStream = null;
            try
            {
                string Reportname = "Enrollment Report";
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                DataTable dt = null;
                HttpResponseMessage response = new HttpResponseMessage();
                ServiceContext serviceRef = new ServiceContext();
                bool isTrue = false;

                fileName = "Enrollment_Report_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                fullPath = folderPath + fileName;
                dt = serviceRef.GenerateEnrollmentReport();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        #region Logic For Total Row & Column data table
                        int OutSiderOpen = 0; int SVTOpen = 0;
                        int NRI = 0; int OutReserveSC = 0;
                        int OutOBC = 0; int OutNT1 = 0;
                        int OutNT2 = 0; int OutNT3 = 0;
                        int OutSBC = 0; int OutLD = 0;
                        int OutST = 0; int OutVT = 0;
                        int SVTReserveSC = 0; int SVTReserveOBC = 0;
                        int SVTReserveNT1 = 0; int SVTReserveNT2 = 0;
                        int SVTReserveNT3 = 0; int SVTReserveSBC = 0;
                        int SVTReserveLD = 0; int SVTST = 0;
                        int SVTVT = 0;
                        foreach (DataRow row in dt.Rows)
                        {
                            int LineTotal = 0;
                            LineTotal = (int)row["Out Sider Open"] + (int)row["SVT Open"] + (int)row["NRI"] + (int)row["Out Reserve SC"] +
                                (int)row["Out OBC"] + (int)row["Out NT 1"] + (int)row["Out NT 2"] + (int)row["Out NT 3"] + (int)row["Out SBC"] +
                                (int)row["Out LD"] + (int)row["Out ST"] + (int)row["Out VJ"] + (int)row["SVT Reserve SC"] + (int)row["SVT Reserve OBC"] + (int)row["SVT Reserve NT 1"] +
                                (int)row["SVT Reserve NT 2"] + (int)row["SVT Reserve NT 3"] + (int)row["SVT Reserve SBC"] + (int)row["SVT Reserve LD"] + (int)row["SVT ST"] + (int)row["SVT VJ"];
                            row["total"] = LineTotal;
                            OutSiderOpen += (int)row["Out Sider Open"];
                            SVTOpen += (int)row["SVT Open"]; NRI += (int)row["NRI"];
                            OutReserveSC += (int)row["Out Reserve SC"];
                            OutOBC += (int)row["Out OBC"]; OutNT1 += (int)row["Out NT 1"];
                            OutNT2 += (int)row["Out NT 2"]; OutNT3 += (int)row["Out NT 3"];
                            OutSBC += (int)row["Out SBC"]; OutLD += (int)row["Out LD"];
                            OutST += (int)row["Out ST"]; OutVT += (int)row["Out VJ"];
                            SVTReserveSC += (int)row["SVT Reserve SC"]; SVTReserveOBC += (int)row["SVT Reserve OBC"];
                            SVTReserveNT1 += (int)row["SVT Reserve NT 1"]; SVTReserveNT2 += (int)row["SVT Reserve NT 2"];
                            SVTReserveNT3 += (int)row["SVT Reserve NT 3"]; SVTReserveSBC += (int)row["SVT Reserve SBC"];
                            SVTReserveLD += (int)row["SVT Reserve LD"];
                            SVTST += (int)row["SVT St"]; SVTVT += (int)row["SVT VJ"];
                        }
                        DataRow drtotal = dt.NewRow();
                        drtotal[1] = "Total"; drtotal[2] = OutSiderOpen; drtotal[3] = SVTOpen;
                        drtotal[4] = NRI; drtotal[5] = OutReserveSC; drtotal[6] = OutOBC;
                        drtotal[7] = OutNT1; drtotal[8] = OutNT2; drtotal[9] = OutNT3;
                        drtotal[10] = OutSBC; drtotal[11] = OutLD; drtotal[12] = OutST;
                        drtotal[13] = OutVT; drtotal[14] = SVTReserveSC; drtotal[15] = SVTReserveOBC;
                        drtotal[16] = SVTReserveNT1; drtotal[17] = SVTReserveNT2; drtotal[18] = SVTReserveNT3;
                        drtotal[19] = SVTReserveSBC; drtotal[20] = SVTReserveLD; drtotal[21] = SVTST;
                        drtotal[22] = SVTVT;
                        drtotal[23] = OutSiderOpen + SVTOpen + NRI + OutReserveSC + OutOBC + OutNT1 + OutNT2 + OutNT3 + OutSBC + OutLD + OutST + OutVT + SVTReserveSC
                            + SVTReserveOBC + SVTReserveNT1 + SVTReserveNT2 + SVTReserveNT3 + SVTReserveSBC + SVTReserveLD + SVTST + SVTVT;
                        dt.Rows.Add(drtotal);
                        #endregion
                    }

                    isTrue = xmlHeaderHelper.BuildWorkbook(fullPath, dt, Reportname);
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

        public static void ValidateExcelDocument(string fileName)
        {
            using (var xlsx = SpreadsheetDocument.Open(fileName, true))
            {
                ValidateOpenXmlDocument(xlsx);
            }
        }

        public static string ValidateOpenXmlDocument(OpenXmlPackage pXmlDoc, bool throwExceptionOnValidationFail = false)
        {
            using (var docToValidate = pXmlDoc)
            {
                var validator = new DocumentFormat.OpenXml.Validation.OpenXmlValidator();
                var validationErrors = validator.Validate(docToValidate).ToList();
                var errors = new System.Text.StringBuilder();
                if (validationErrors.Any())
                {
                    var errorMessage = string.Format("ValidateOpenXmlDocument: {0} validation error(s) with document", validationErrors.Count);
                    errors.AppendLine(errorMessage);
                    errors.AppendLine();
                }

                foreach (var error in validationErrors)
                {
                    errors.AppendLine("Description: " + error.Description);
                    errors.AppendLine("ErrorType: " + error.ErrorType);
                    errors.AppendLine("Node: " + error.Node);
                    errors.AppendLine("Path: " + error.Path.XPath);
                    errors.AppendLine("Part: " + error.Part.Uri);
                    if (error.RelatedNode != null)
                    {
                        errors.AppendLine("Related Node: " + error.RelatedNode);
                        errors.AppendLine("Related Node Inner Text: " + error.RelatedNode.InnerText);
                    }
                    errors.AppendLine();
                    errors.AppendLine("==============================");
                    errors.AppendLine();
                }

                if (validationErrors.Any() && throwExceptionOnValidationFail)
                {
                    throw new Exception(errors.ToString());
                }
                if (errors.Length > 0)
                {
                    System.Diagnostics.Debug.WriteLine(errors.ToString());
                }
                return errors.ToString();
            }
        }

        [HttpGet]
        public HttpResponseMessage GetHostelReport()
        {
            FileStream fileStream = null;
            try
            {
                string Reportname = "Hostel-Applications";
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                DataTable dt = null;
                HttpResponseMessage response = new HttpResponseMessage();
                ServiceContext serviceRef = new ServiceContext();
                bool isTrue = false;
                fileName = "Hostel_Applications_Report" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                fullPath = folderPath + fileName;
                dt = serviceRef.GetHostelApplicationReport();
                if (dt != null)
                {
                    isTrue = xmlHeaderHelper.BuildWorkbook(fullPath, dt, Reportname);
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

        [HttpGet]
        public HttpResponseMessage GetFeesPaidReport(string startDate, string endDate)
        {
            FileStream fileStream = null;
            try
            {
                DateTime myStartDate;
                DateTime myEndDate;
                if (!DateTime.TryParseExact(startDate, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out myStartDate))
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid date format");
                }

                if (!DateTime.TryParseExact(endDate, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out myEndDate))
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid date format");
                }

                string Reportname = "Fees Paid Report on " + startDate;
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                DataTable dt = null;
                HttpResponseMessage response = new HttpResponseMessage();
                ServiceContext serviceRef = new ServiceContext();
                bool isTrue = false;
                fileName = "FeesPaid_Report" + myStartDate.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                fullPath = folderPath + fileName;
                dt = serviceRef.GetOperationalReport("GetFessPaidReportByDate", myStartDate, myEndDate);
                if (dt != null)
                {
                    isTrue = xmlHeaderHelper.BuildWorkbook(fullPath, dt, Reportname);
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

        [HttpGet]
        public HttpResponseMessage GetFormSubmittedReport(string startDate, string endDate)
        {
            FileStream fileStream = null;
            try
            {
                DateTime myStartDate;
                DateTime myEndDate;
                if (!DateTime.TryParseExact(startDate, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out myStartDate))
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid date formate");
                }

                if (!DateTime.TryParseExact(endDate, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out myEndDate))
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid date formate");
                }


                string Reportname = "Form Submitted Report on " + startDate;
                string folderPath = HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                DataTable dt = null;
                HttpResponseMessage response = new HttpResponseMessage();
                ServiceContext serviceRef = new ServiceContext();
                bool isTrue = false;
                fileName = "Form_Submitted_Report" + myStartDate.ToString("yyyy-MM-dd_HH_mm_ss") + ".xlsx";
                fullPath = folderPath + fileName;
                dt = serviceRef.GetOperationalReport("GetFormSubmittedReportByDate", myStartDate, myEndDate);
                if (dt != null)
                {
                    isTrue = xmlHeaderHelper.BuildWorkbook(fullPath, dt, Reportname);
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

        [HttpGet]
        public HttpResponseMessage GetMasterDataForExam()
        {
            try
            {
                string folderPath = System.Web.HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                DataTable dt = null;
                bool isTrue = false;
                FileStream fileStream = null;
                HttpResponseMessage response = new HttpResponseMessage();
                ServiceContext serviceRef = new ServiceContext();
                fileName = "Student.xlsx";
                fullPath = folderPath + fileName;
               
                dt = serviceRef.GetMasterDataForExam();
                if (dt != null)
                {
                    isTrue = xmlHeaderHelper.BuildWorkbook(fullPath, dt, "Student");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Datatable is empty");
                }
                if (isTrue)
                {
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

        [HttpGet]
        public HttpResponseMessage GetRegistrationFormData()
        {
            try
            {
                string folderPath = System.Web.HttpContext.Current.Server.MapPath(String.Format("~/data/PDF/"));
                string fileName = string.Empty;
                string fullPath = string.Empty;
                DataTable dt = null;
                bool isTrue = false;
                FileStream fileStream = null;
                HttpResponseMessage response = new HttpResponseMessage();
                ServiceContext serviceRef = new ServiceContext();
                fileName = "RegistrationFormData.xlsx";
                fullPath = folderPath + fileName;

                dt = serviceRef.GetStudentRegisterFormExcelData();
                if (dt != null)
                {
                    isTrue = xmlHeaderHelper.BuildWorkbook(fullPath, dt, "RegistrationFormData");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, "Datatable is empty");
                }
                if (isTrue)
                {
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

        public static void ExportDataTableToCSV(DataTable dt, string fileName)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb.Append(dt.Columns[i].ColumnName + ',');
                }
                sb.Append(Environment.NewLine);

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    for (int k = 0; k < dt.Columns.Count; k++)
                    {
                        sb.Append(dt.Rows[j][k].ToString() + ',');
                    }
                    sb.Append(Environment.NewLine);
                }
                System.IO.File.WriteAllText(fileName, sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



     
    }
}
