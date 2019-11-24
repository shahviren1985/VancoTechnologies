using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Xml;

namespace AA.Util
{
    public class ConfigurationXmlParser
    {
        public ConfigurationXmlParser()
        {
        }

        public string filePath;

        // open existing file or create new file
        public XmlDocument OpenFile(string logPath, string configurationPath)
        {
            try
            {
                if (string.IsNullOrEmpty(configurationPath))
                {
                    filePath = Path.Combine(HttpContext.Current.Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["BASE_PATH"]), "/Release/Default/Configuration.xml");
                }
                else
                {
                    filePath = configurationPath;
                }


                XmlDocument doc = null;

                //Verify whether a file exists
                if (File.Exists(filePath))
                {
                    doc = new XmlDocument();
                    doc.Load(filePath);
                }

                return doc;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "OpenFile", "Error Occured while Open Existing File", ex, logPath);
                throw ex;
            }
        }

        public XmlConfigurations GetCommonConfigurations(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                XmlConfigurations config = new XmlConfigurations();
                XmlNode common = doc.SelectSingleNode("Configuration/Print/Common");


                if (common != null)
                {
                    XmlNode header = common.SelectSingleNode("HeaderDetails");
                    XmlNode footer = common.SelectSingleNode("FooterDetails");
                    XmlNode acadYear = common.SelectSingleNode("AcademicYear");
                    XmlNode loginUrl = doc.SelectSingleNode("Configuration/LoginPageURL");
                    if (header != null)
                    {
                        config.Header = new ConfigurationHeaderDetails();
                        config.Header.IsRequired = bool.Parse(header.SelectSingleNode("Header").Attributes["required"].Value);
                        XmlNodeList collegeNames = header.SelectNodes("CollegeName/Line");
                        config.Header.CollegeName = new List<ConfigHeaderCollegeName>();
                        config.Header.CollegeFontSize = new List<int>();
                        foreach (XmlNode college in collegeNames)
                        {
                            ConfigHeaderCollegeName collegeConfig = new ConfigHeaderCollegeName();
                            collegeConfig.FontSize = college.Attributes["fontsize"] != null ? int.Parse(college.Attributes["fontsize"].Value) : 9;
                            collegeConfig.Bold = college.Attributes["bold"] != null ? bool.Parse(college.Attributes["bold"].Value) : true;
                            collegeConfig.CollegeName = college.InnerText;
                            collegeConfig.Display = college.Attributes["show"] != null ? bool.Parse(college.Attributes["show"].Value) : true;
                            collegeConfig.LLX = college.Attributes["llx"] != null ? int.Parse(college.Attributes["llx"].Value) : 0;
                            collegeConfig.LLY = college.Attributes["lly"] != null ? int.Parse(college.Attributes["lly"].Value) : 0;
                            collegeConfig.URX = college.Attributes["urx"] != null ? int.Parse(college.Attributes["urx"].Value) : 0;
                            collegeConfig.URY = college.Attributes["ury"] != null ? int.Parse(college.Attributes["ury"].Value) : 0;

                            config.Header.CollegeName.Add(collegeConfig);

                            //config.Header.CollegeName.Add(college.InnerText);

                            #region Commented Code
                            /*int fontsize = 0;
                            if (college.Attributes["fontsize"] == null)
                            {
                                fontsize = 9;
                            }
                            else
                            {
                                int.TryParse(college.Attributes["fontsize"].Value, out fontsize);
                                if (fontsize < 2)
                                {
                                    fontsize = 9;
                                }
                            }

                            config.Header.CollegeFontSize.Add(fontsize);*/
                            #endregion
                        }

                        config.Header.LogoPath = header.SelectSingleNode("Logo").InnerText;
                        config.Header.LogoWidth = header.SelectSingleNode("LogoSize").Attributes["width"].Value;
                        config.Header.LogoHeight = header.SelectSingleNode("LogoSize").Attributes["height"].Value;
                        config.Header.LogoScale = header.SelectSingleNode("Logo").Attributes["scale"] != null ? int.Parse(header.SelectSingleNode("Logo").Attributes["scale"].Value) : 50;
                        config.Header.LogoX = header.SelectSingleNode("Logo").Attributes["x"] != null ? int.Parse(header.SelectSingleNode("Logo").Attributes["x"].Value) : 0;
                        config.Header.LogoY = header.SelectSingleNode("Logo").Attributes["y"] != null ? int.Parse(header.SelectSingleNode("Logo").Attributes["y"].Value) : 0;

                        /* Tag Line Properties */
                        config.Header.TagLine = header.SelectSingleNode("TagLine").InnerText;
                        config.Header.ShowTagLine = header.SelectSingleNode("TagLine").Attributes["show"] != null ? bool.Parse(header.SelectSingleNode("TagLine").Attributes["show"].Value) : false;
                        config.Header.BoldTagLine = header.SelectSingleNode("TagLine").Attributes["bold"] != null ? bool.Parse(header.SelectSingleNode("TagLine").Attributes["bold"].Value) : false;
                        config.Header.FontSizeTagLine = header.SelectSingleNode("TagLine").Attributes["fontsize"] != null ? int.Parse(header.SelectSingleNode("TagLine").Attributes["fontsize"].Value) : 7;
                        config.Header.LLXTagLine = header.SelectSingleNode("TagLine").Attributes["llx"] != null ? int.Parse(header.SelectSingleNode("TagLine").Attributes["llx"].Value) : 7;
                        config.Header.LLYTagLine = header.SelectSingleNode("TagLine").Attributes["lly"] != null ? int.Parse(header.SelectSingleNode("TagLine").Attributes["lly"].Value) : 7;
                        config.Header.URXTagLine = header.SelectSingleNode("TagLine").Attributes["urx"] != null ? int.Parse(header.SelectSingleNode("TagLine").Attributes["urx"].Value) : 7;
                        config.Header.URYTagLine = header.SelectSingleNode("TagLine").Attributes["ury"] != null ? int.Parse(header.SelectSingleNode("TagLine").Attributes["ury"].Value) : 7;


                        /* Small Tag Line Properties */
                        config.Header.SmallTagLine = header.SelectSingleNode("SmallTagLine").InnerText;
                        config.Header.ShowSmallTagLine = header.SelectSingleNode("SmallTagLine").Attributes["show"] != null ? bool.Parse(header.SelectSingleNode("SmallTagLine").Attributes["show"].Value) : false;
                        config.Header.BoldSmallTagLine = header.SelectSingleNode("SmallTagLine").Attributes["bold"] != null ? bool.Parse(header.SelectSingleNode("SmallTagLine").Attributes["bold"].Value) : false;
                        config.Header.FontSizeSmallTagLine = header.SelectSingleNode("SmallTagLine").Attributes["fontsize"] != null ? int.Parse(header.SelectSingleNode("SmallTagLine").Attributes["fontsize"].Value) : 7;
                        config.Header.LLXSmallTagLine = header.SelectSingleNode("SmallTagLine").Attributes["llx"] != null ? int.Parse(header.SelectSingleNode("SmallTagLine").Attributes["llx"].Value) : 7;
                        config.Header.LLYSmallTagLine = header.SelectSingleNode("SmallTagLine").Attributes["lly"] != null ? int.Parse(header.SelectSingleNode("SmallTagLine").Attributes["lly"].Value) : 7;
                        config.Header.URXSmallTagLine = header.SelectSingleNode("SmallTagLine").Attributes["urx"] != null ? int.Parse(header.SelectSingleNode("SmallTagLine").Attributes["urx"].Value) : 7;
                        config.Header.URYSmallTagLine = header.SelectSingleNode("SmallTagLine").Attributes["ury"] != null ? int.Parse(header.SelectSingleNode("SmallTagLine").Attributes["ury"].Value) : 7;


                        XmlNodeList address = header.SelectNodes("Address/Line");
                        config.Header.Address = new List<ConfigHeaderCollegeName>();
                        foreach (XmlNode addressLine in address)
                        {
                            ConfigHeaderCollegeName addressConfig = new ConfigHeaderCollegeName();
                            addressConfig.FontSize = addressLine.Attributes["fontsize"] != null ? int.Parse(addressLine.Attributes["fontsize"].Value) : 9;
                            addressConfig.Bold = addressLine.Attributes["bold"] != null ? bool.Parse(addressLine.Attributes["bold"].Value) : true;
                            addressConfig.CollegeName = addressLine.InnerText;
                            addressConfig.Display = addressLine.Attributes["show"] != null ? bool.Parse(addressLine.Attributes["show"].Value) : true;
                            addressConfig.LLX = addressLine.Attributes["llx"] != null ? int.Parse(addressLine.Attributes["llx"].Value) : 0;
                            addressConfig.LLY = addressLine.Attributes["lly"] != null ? int.Parse(addressLine.Attributes["lly"].Value) : 0;
                            addressConfig.URX = addressLine.Attributes["urx"] != null ? int.Parse(addressLine.Attributes["urx"].Value) : 0;
                            addressConfig.URY = addressLine.Attributes["ury"] != null ? int.Parse(addressLine.Attributes["ury"].Value) : 0;

                            config.Header.Address.Add(addressConfig);
                            //config.Header.Address.Add(addressLine.InnerText);
                        }
                    }

                    if (footer != null)
                    {
                        config.Footer = new ConfigurationFooterDetails();
                        config.Footer.FooterLine = footer.SelectSingleNode("FooterLine").InnerText;
                        XmlNodeList collegeNames = footer.SelectNodes("CollegeName/Line");
                        config.Footer.CollegeName = new List<string>();
                        foreach (XmlNode college in collegeNames)
                        {
                            config.Footer.CollegeName.Add(college.InnerText);
                        }
                    }

                    if (acadYear != null)
                    {
                        config.Header.AcademicYear = acadYear.Attributes["format"].Value;
                        config.Header.AcademicYear = string.IsNullOrEmpty(config.Header.AcademicYear) ? "1" : config.Header.AcademicYear;
                    }
                    else
                    {
                        config.Header.AcademicYear = "1";
                    }

                    if (loginUrl != null)
                    {
                        config.LoginUrl = loginUrl.Attributes["value"].Value;
                    }

                    config.LeavingAdmitDetails = GetLCAdmissionDate(logPath, configurationPath);

                    return config;
                }
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "GetCommonConfigurations", "Error Occured while getting Common Configurations", ex, logPath);
                throw ex;
            }

            return null;
        }

        public void GetBonafideContent(ref XmlConfigurations config, string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                XmlNode bonafide = doc.SelectSingleNode("Configuration/Print/Bonafide");

                if (bonafide != null)
                {
                    XmlNodeList bonafideContent = bonafide.SelectNodes("Line");
                    Bonafide bonafideCertificate = new Bonafide();
                    bonafideCertificate.BonafideLine = new List<string>();
                    config.BonafideText = bonafideCertificate;
                    foreach (XmlNode line in bonafideContent)
                    {
                        bonafideCertificate.BonafideLine.Add(line.InnerText);
                    }
                }
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "GetBonafideContent", "Error Occured while getting Bonafide Content", ex, logPath);
                throw ex;
            }
        }

        public string GetCourseSpecilization(ref XmlConfigurations config, string subCourse, string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                XmlNode leaving = doc.SelectSingleNode("Configuration/Print/Specialization");
                string subcourse = string.Empty;
                string subCourseText = subCourse;

                if (leaving != null)
                {
                    XmlNodeList specialization = leaving.SelectNodes("Special");
                    if (specialization != null)
                    {
                        foreach (XmlNode node in specialization)
                        {
                            subcourse = node.Attributes["subcourse"].Value;
                            if (subcourse.Trim().ToUpper() == subCourse.Trim().ToUpper())
                            {
                                subCourseText = node.Attributes["text"].Value.Trim().ToUpper();
                            }
                        }
                    }
                }

                return subCourseText;
            }
            catch (Exception ex)
            {
                return subCourse;
            }
        }

        public void GetLeavingContent(ref XmlConfigurations config, string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                XmlNode leaving = doc.SelectSingleNode("Configuration/Print/LeavingCertificate");

                if (leaving != null)
                {
                    XmlNodeList leavingContent = leaving.SelectNodes("Line");
                    XmlNode sinceWhenNode = leaving.SelectSingleNode("Admission");
                    Leaving leavingCertificate = new Leaving();

                    if (sinceWhenNode != null)
                    {
                        leavingCertificate.UseConfiguredSinceWhenValue = true;
                        leavingCertificate.ShowYear = sinceWhenNode.Attributes["showyear"] != null ? bool.Parse(sinceWhenNode.Attributes["showyear"].Value) : false;
                        leavingCertificate.LCMonth = sinceWhenNode.Attributes["month"] != null ? sinceWhenNode.Attributes["month"].Value : string.Empty;
                        leavingCertificate.YearIncValue = sinceWhenNode.Attributes["year"] != null ? int.Parse(sinceWhenNode.Attributes["year"].Value) : 0;
                    }


                    leavingCertificate.LeavingCertificates = new List<LeavingCertificate>();
                    leavingCertificate.LCType = leaving.Attributes["type"] != null ? leaving.Attributes["type"].Value : string.Empty;
                    config.LeavingCertificates = leavingCertificate.LeavingCertificates;

                    leavingCertificate.IsHeaderRequired = leaving.Attributes["header"] == null ? true : bool.Parse(leaving.Attributes["header"].Value);

                    foreach (XmlNode line in leavingContent)
                    {
                        LeavingCertificate lc = new LeavingCertificate();
                        lc.Display = line.Attributes["show"] == null ? false : bool.Parse(line.Attributes["show"].Value);
                        lc.Align = line.Attributes["align"] == null ? string.Empty : line.Attributes["align"].Value;
                        lc.FontSize = line.Attributes["fontsize"] == null ? 9 : int.Parse(line.Attributes["fontsize"].Value);
                        lc.id = line.Attributes["id"] == null ? 0 : int.Parse(line.Attributes["id"].Value);
                        lc.IsBold = line.Attributes["bold"] == null ? false : bool.Parse(line.Attributes["bold"].Value);
                        lc.LeavingLine = line.InnerText;
                        lc.LineType = line.Attributes["type"] == null ? "text" : line.Attributes["type"].Value;
                        lc.LowerLeftXCoOrd = line.Attributes["llx"] == null ? 0 : int.Parse(line.Attributes["llx"].Value);
                        lc.LowerLeftYCoOrd = line.Attributes["lly"] == null ? 0 : int.Parse(line.Attributes["lly"].Value);
                        lc.UpperRightXCoOrd = line.Attributes["urx"] == null ? 0 : int.Parse(line.Attributes["urx"].Value);
                        lc.UpperRightYCoOrd = line.Attributes["ury"] == null ? 0 : int.Parse(line.Attributes["ury"].Value);
                        lc.ShowColonSeparator = line.Attributes["showcolon"] == null ? false : bool.Parse(line.Attributes["showcolon"].Value);
                        leavingCertificate.LeavingCertificates.Add(lc);
                    }
                }
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "GetLeavingContent", "Error Occured while getting Leaving Content", ex, logPath);
                throw ex;
            }
        }

        public Leaving GetLCAdmissionDate(string logPath, string configurationPath)
        {
            Leaving leave = new Leaving();
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                XmlNode sinceWhenNode = doc.SelectSingleNode("Configuration/Print/LeavingCertificate/Admission");

                if (sinceWhenNode != null)
                {
                    leave.UseConfiguredSinceWhenValue = true;
                    leave.ShowYear = sinceWhenNode.Attributes["showyear"] != null ? bool.Parse(sinceWhenNode.Attributes["showyear"].Value) : false;
                    leave.LCMonth = sinceWhenNode.Attributes["month"] != null ? sinceWhenNode.Attributes["month"].Value : string.Empty;
                    leave.YearIncValue = sinceWhenNode.Attributes["year"] != null ? int.Parse(sinceWhenNode.Attributes["year"].Value) : 0;
                }
            }
            catch (Exception ex)
            {

            }

            return leave;
        }

        public string GetLCType(ref XmlConfigurations config, string logPath, string configurationPath)
        {
            string lcType = string.Empty;

            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                XmlNode leaving = doc.SelectSingleNode("Configuration/Print/LeavingCertificate");

                if (leaving != null)
                {
                    lcType = leaving.Attributes["type"].Value;
                }
            }
            catch (Exception ex)
            {

            }

            return lcType;
        }

        public bool IsLCHeaderRequired(ref XmlConfigurations config, string logPath, string configurationPath)
        {
            bool lcHeader = false;

            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                XmlNode leaving = doc.SelectSingleNode("Configuration/Print/LeavingCertificate");

                if (leaving != null)
                {
                    lcHeader = leaving.Attributes["header"] != null ? bool.Parse(leaving.Attributes["header"].Value) : true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lcHeader;

        }

        public bool IsHeaderRequired(ref XmlConfigurations config, string logPath, string configurationPath)
        {
            bool lcHeader = false;

            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                XmlNode leaving = doc.SelectSingleNode("Configuration/Print/Common/HeaderDetails/Header");

                if (leaving != null)
                {
                    lcHeader = leaving.Attributes["header"] != null ? bool.Parse(leaving.Attributes["header"].Value) : true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lcHeader;

        }

        public DataTable GetLCExams(ref XmlConfigurations config, string logPath, string configurationPath)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("ExamName");
            DataRow row;

            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                XmlNode exams = doc.SelectSingleNode("Configuration/Print/LeavingCertificate/ExamSettings");

                if (exams != null)
                {
                    XmlNodeList examContent = exams.SelectNodes("Exam");
                    int counter = 1;
                    foreach (XmlNode exam in examContent)
                    {
                        if (exam.Attributes["text"] != null && !string.IsNullOrEmpty(exam.Attributes["text"].Value))
                        {
                            List<string> examList = new List<string>();
                            examList.Add(counter.ToString());
                            examList.Add(exam.Attributes["text"].Value.Trim());
                            row = dt.NewRow();
                            row.ItemArray = examList.ToArray();
                            dt.Rows.Add(row);
                            counter++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        //Added by Rajesh
        public string PrintTranseferClgName(string logPath, string configurationPath)
        {

            ConfigHeaderCollegeName cHCN = new ConfigHeaderCollegeName();
            XmlDocument xdoc = OpenFile(logPath, configurationPath);
            XmlNode clgName = xdoc.SelectSingleNode("Configuration/Print/TransferCertificate/CollegeName/Line");
            if (clgName != null)
            {
                cHCN.CollegeName = clgName.InnerText;
            }

            return cHCN.CollegeName;
        }


        public string PrintAdmissionDate(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument xdoc = OpenFile(logPath, configurationPath);
                XmlNode adminDate = xdoc.SelectSingleNode("Configuration/AdmissionCounter");

                if (adminDate != null)
                {
                    return adminDate.Attributes["startdate"].Value;
                }
            }
            catch (Exception)
            {
                //logger.Error("ConfigurationXmlParser.cs", "PrintAdmissionDate", "Error Occured while getting value from xml", ex, logPath);
            }
            //return new Util(loggedInUser).GetCurrentIndianDate().ToShortDateString("yyyy-mm-dd");
            return string.Empty;
        }
        //End
        public string GetPrintMode(ref XmlConfigurations config, string type, string logPath, string configurationPath)
        {
            try
            {
                //logger.Debug("ConfigurationXmlParser.cs", "GetPrintMode", "Getting print mode", logPath);
                XmlDocument doc = OpenFile(logPath, configurationPath);
                XmlNode certificates = doc.SelectSingleNode("Configuration/Certificates");

                if (certificates != null)
                {
                    XmlNodeList certificateContent = certificates.SelectNodes("Certificate");
                    //Leaving leavingCertificate = new Leaving();
                    //leavingCertificate.LeavingLine = new List<string>();
                    //config.LeavingText = leavingCertificate;
                    foreach (XmlNode line in certificateContent)
                    {
                        if (line.Attributes["type"].Value == type && line.Attributes["mode"].Value == "pdf")
                        {
                            //logger.Debug("ConfigurationXmlParser.cs", "GetPrintMode", "Retuning pdf mode", logPath);
                            return "pdf";
                        }
                        if (line.Attributes["type"].Value == type && line.Attributes["mode"].Value == "regularpdf")
                        {
                            //logger.Debug("ConfigurationXmlParser.cs", "GetPrintMode", "Retuning regularpdf mode", logPath);
                            return "regularpdf";
                        }
                        if (line.Attributes["type"].Value == type && line.Attributes["mode"].Value == "regular")
                        {
                            //logger.Debug("ConfigurationXmlParser.cs", "GetPrintMode", "Retuning regular mode", logPath);
                            return "regular";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "GetPrintMode", "Error Occured while getting Print Mode", ex, logPath);
                throw ex;
            }
            return null;
        }
        public string GetScholrshipAmt(string name, string logPath, string configurationPath)
        {
            try
            {
                //logger.Debug("ConfigurationXmlParser.cs", "GetScholrshipAmt", "Reading Scholarship amount from configuration file", logPath);
                XmlDocument doc = OpenFile(logPath, configurationPath);
                string item = string.Empty;
                XmlNode scholarship = doc.SelectSingleNode("Configuration/Scholarship");
                if (scholarship != null)
                {
                    XmlNodeList type = scholarship.SelectNodes("Type");
                    foreach (XmlNode node in type)
                    {
                        if (node.InnerText == name)
                        {
                            if (node.Attributes.Count == 0)
                            {
                                break;
                            }
                            item = node.Attributes["amount"].Value;
                            break;
                        }
                    }
                }
                //logger.Debug("ConfigurationXmlParser.cs", "GetScholrshipAmt", "Returning Scholarship amount from configuration file", logPath);
                return item;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "GetScholarshipAmt", "Error Occured while gettign Scholrship Amount", ex, logPath);
                throw ex;
            }
        }

        public string GetStationToName(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                string item = string.Empty;
                XmlNode stationTo = doc.SelectSingleNode("Configuration/StationTo");
                if (stationTo != null)
                {
                    XmlNodeList type = stationTo.SelectNodes("Type");
                    item = type.Item(0).InnerText.ToString();


                }
                return item;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "GetStationToName", "Error Occured while getting Station Name", ex, logPath);
                throw ex;
            }
        }


        public List<Scholarship> GetScholarship(string logPath, string configurationPath)
        {
            try
            {
                //logger.Debug("ConfigurationXmlParser.cs", "GetScholarship", "Reading Scholarship types from configuration file", logPath);
                XmlDocument doc = OpenFile(logPath, configurationPath);

                XmlNode scholarship = doc.SelectSingleNode("Configuration/Scholarship");

                List<Scholarship> scholarshipList = new List<Scholarship>();

                if (scholarship != null)
                {
                    XmlNodeList type = scholarship.SelectNodes("Type");
                    foreach (XmlNode node in type)
                    {
                        Scholarship item = new Scholarship();
                        item.ScholarshipName = node.InnerText;
                        //  item.amount = node.Attributes["amount"].Name;
                        scholarshipList.Add(item);
                    }
                }
                //logger.Debug("ConfigurationXmlParser.cs", "GetScholarship", "returning Scholarship types from configuration file", logPath);
                return scholarshipList;

            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "GetScholarship", "Error Occured while getting Scholarship", ex, logPath);
                throw ex;
            }
        }

        public string GetRollNumberConfiguration(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                string required = string.Empty;
                XmlNode node = doc.SelectSingleNode("Configuration/RollNumber");
                if (node != null)
                {
                    required = node.SelectSingleNode("CustomRollNumber").Attributes["required"].Value;
                }
                return required;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "GetRollNumberConfiguration", "Error Occured while getting Roll Number Configuration", ex, logPath);
                throw ex;
            }
        }

        public bool CallDMSHandlerForSavingFiles(string logPath, string configurationPath)
        {
            try
            {
                //logger.Debug("ConfigurationXmlParser.cs", "CallDMSHandlerForSavingFiles", "Getting Flag for call DMS API", logPath);
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isCall = false;
                XmlNode node = doc.SelectSingleNode("Configuration/IsCallDMSApi");

                if (node != null)
                {
                    isCall = Convert.ToBoolean(node.Attributes["iscall"].Value);
                }
                //logger.Debug("ConfigurationXmlParser.cs", "CallDMSHandlerForSavingFiles", "Reutrning Flag for call DMS API", logPath);
                return isCall;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "CallDMSHandlerForSavingFiles", "Error Occured while Calling DMS API for Saving Files", ex, logPath);
                throw ex;
            }
        }

        public bool IsChallanDateEditable(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isEditable = false;
                XmlNode node = doc.SelectSingleNode("Configuration/IsChallanDateEditable");

                if (node != null)
                {
                    isEditable = Convert.ToBoolean(node.Attributes["isEditable"].Value);
                }

                return isEditable;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "IsChallanDateEditabel", "Error Occured while Check Challan Date Editable", ex, logPath);
                throw ex;
            }
        }

        public bool IsOldStudent(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isOldStudent = false;
                XmlNode node = doc.SelectSingleNode("Configuration/IsOldStudent");

                if (node != null)
                {
                    isOldStudent = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isOldStudent;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsGenderVisible(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isGenderVisible = false;
                XmlNode node = doc.SelectSingleNode("Configuration/IsGenderVisible");

                if (node != null)
                {
                    isGenderVisible = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isGenderVisible;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsSchollarshipApplicable(string logPath, string configurationPath)
        {
            try
            {
                //logger.Debug("ConfigurationXmlParser.cs", "IsSchollarshipApplicable", "Reading scholarship applicable flag from configuration file", logPath);
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isEditable = false;
                XmlNode node = doc.SelectSingleNode("Configuration/ISSchollarshipApplicable");

                if (node != null)
                {
                    isEditable = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                //logger.Debug("ConfigurationXmlParser.cs", "IsSchollarshipApplicable", "returning scholarship applicable flag from configuration file", logPath);
                return isEditable;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "IsSchollarshipApplicable", "Error Occured while Check Schollarship Applicable", ex, logPath);
                throw;
            }
        }

        public bool IsEligible(string logPath, string configurationPath)
        {
            try
            {
                //logger.Debug("ConfigurationXmlParser.cs", "IsEligible", "Reading eligible applicable flag from configuration file", logPath);
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isEditable = false;
                XmlNode node = doc.SelectSingleNode("Configuration/ISEligibleApplicable");

                if (node != null)
                {
                    isEditable = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                //logger.Debug("ConfigurationXmlParser.cs", "IsEligible", "Returning eligible applicable flag from configuration file", logPath);
                return isEditable;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "IsEligible", "Error Occured while check Eligible", ex, logPath);
                throw ex;
            }
        }

        public bool IsNewInCollege(string logPath, string configurationPath)
        {
            try
            {
                //logger.Debug("ConfigurationXmlParser.cs", "IsNewInCollege", "Reading New in college flag from configuration file", logPath);
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isEditable = false;
                XmlNode node = doc.SelectSingleNode("Configuration/ISNewInCollegeApplicable");

                if (node != null)
                {
                    isEditable = Convert.ToBoolean(node.Attributes["value"].Value);
                }
                //logger.Debug("ConfigurationXmlParser.cs", "IsNewInCollege", "Returning New in college flag from configuration file", logPath);
                return isEditable;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "IsNewInCollege", "Occured ", ex, logPath);
                throw ex;
            }
        }

        public bool SaveFeeReceipt(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isEditable = false;
                XmlNode node = doc.SelectSingleNode("Configuration/SaveFeeReceipt");

                if (node != null)
                {
                    isEditable = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isEditable;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "SaveFeeReceipt", "Occured ", ex, logPath);
                throw ex;
            }
        }

        public bool StudentMarksDetails(string logPath, string configurationPath)
        {
            try
            {
                //logger.Debug("ConfigurationXmlParser.cs", "StudentMarksDetails", "Reading student mark details flag from configuration file", logPath);
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isEditable = false;
                XmlNode node = doc.SelectSingleNode("Configuration/StudentMarksDetails");

                if (node != null)
                {
                    isEditable = Convert.ToBoolean(node.Attributes["value"].Value);
                }
                //logger.Debug("ConfigurationXmlParser.cs", "StudentMarksDetails", "Returning student mark details flag from configuration file", logPath);
                return isEditable;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "StudentMarksDetails", "Error Occured while Reading Student mark details", ex, logPath);
                throw ex;
            }
        }

        public bool schollarshipwisereport(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isEditable = false;
                XmlNode node = doc.SelectSingleNode("Configuration/schollarshipwisereport");

                if (node != null)
                {
                    isEditable = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isEditable;
            }
            catch (Exception ex)
            {
                // logger.Error("ConfigurationXmlParser.cs", "schollarshipwiseReport", "Error Occured on Scholarship Wise Report", ex, logPath);
                throw ex;
            }
        }
        public bool account8010report(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isEditable = false;
                XmlNode node = doc.SelectSingleNode("Configuration/account8010report");

                if (node != null)
                {
                    isEditable = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isEditable;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "account8010report", "Error Occured on Account 8010 Report", ex, logPath);
                throw ex;
            }
        }
        public bool account8009report(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isEditable = false;
                XmlNode node = doc.SelectSingleNode("Configuration/account8009report");

                if (node != null)
                {
                    isEditable = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isEditable;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "account8009report", "Error Occured on Account 8009 Report", ex, logPath);
                throw ex;
            }
        }
        public bool account2701report(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isEditable = false;
                XmlNode node = doc.SelectSingleNode("Configuration/account2701report");

                if (node != null)
                {
                    isEditable = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isEditable;
            }
            catch (Exception ex)
            {
                // logger.Error("ConfigurationXmlParser.cs", "account2701report", "Error Occured on Account 2701 Report ", ex, logPath);
                throw ex;
            }
        }

        public string ReceiptAccount_1(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                string isEditable = string.Empty;
                XmlNode node = doc.SelectSingleNode("Configuration/ReceiptAccount_1");

                if (node != null)
                {
                    isEditable = node.Attributes["value"].Value;
                }

                return isEditable;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "ReceiptAccount_1", "Error Occured on first Receipt Account", ex, logPath);
                throw ex;
            }
        }

        public string ReceiptAccount_2(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                string isEditable = string.Empty;
                XmlNode node = doc.SelectSingleNode("Configuration/ReceiptAccount_2");

                if (node != null)
                {
                    isEditable = node.Attributes["value"].Value;
                }

                return isEditable;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "ReceiptAccount_2", "Error Occured on Second Receipt Account", ex, logPath);
                throw ex;
            }
        }
        public string ReceiptAccount_3(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                string isEditable = string.Empty;
                XmlNode node = doc.SelectSingleNode("Configuration/ReceiptAccount_3");

                if (node != null)
                {
                    isEditable = node.Attributes["value"].Value;
                }

                return isEditable;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "ReceiptAccount_3", "Error Occured on Third Receipt Account", ex, logPath);
                throw ex;
            }
        }

        public int GetReceiptFeeHeadColumnCount(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                int columnCount = 3;
                XmlNode node = doc.SelectSingleNode("Configuration/ReceiptColumnCount");

                if (node != null)
                {
                    columnCount = int.Parse(node.Attributes["value"].Value);
                }

                return columnCount;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "ReceiptAccount_1", "Error Occured on first Receipt Account", ex, logPath);
                throw ex;
            }
        }


        public Hashtable ShowReportsPages(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);

                XmlNode reportPages = doc.SelectSingleNode("Configuration/ShowReportPages");

                Hashtable pages = new Hashtable();

                if (reportPages != null)
                {
                    XmlNodeList type = reportPages.SelectNodes("page");
                    int count = 1;
                    foreach (XmlNode node in type)
                    {
                        string pagename = node.InnerText;

                        pages.Add(count.ToString(), pagename);
                        count++;
                    }
                }

                return pages;
            }
            catch (Exception ex)
            {
                // logger.Error("ConfigurationXmlParser.cs", "ShowReportPages", "Error Occured on show Reports Pages", ex, logPath);
                throw ex;
            }
        }


        public string GetReportName(string nm, string logPath, string configurationPath)
        {
            try
            {
                string name = string.Empty;
                XmlDocument doc = OpenFile(logPath, configurationPath);

                XmlNode reportPages = doc.SelectSingleNode("Configuration/ShowReportPageName");
                if (reportPages != null)
                {
                    XmlNodeList page = reportPages.SelectNodes("ReportName");
                    int count = page.Count;

                    int c = 1;
                    foreach (XmlNode node in page)
                    {
                        if (nm == node.Attributes["name"].Value)
                        {
                            name = node.InnerText;
                        }

                    }
                }
                return name;

            }
            catch (Exception ex)
            {
                // logger.Error("ConfigurationXmlParser.cs", "GetReportName", "Error Occured while getting Report Name", ex, logPath);
                throw ex;
            }
        }

        public bool GetPdfValue(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isPdf = false;
                XmlNode print = doc.SelectSingleNode("Configuration/PrintPDF");
                if (print != null)
                {
                    isPdf = Convert.ToBoolean(print.Attributes["value"].Value);
                }
                return isPdf;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "GetPdfValue", "Error Occured while getting PDF Value", ex, logPath);
                throw ex;
            }
        }

        public Hashtable DisplayOnlineFormFields(string logPath, string configurationPath)
        {
            Hashtable table = new Hashtable();
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                XmlNodeList nodes = doc.SelectNodes("Configuration/OnlineForm/Fields/Field");

                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        table.Add(node.Attributes["name"].Value, bool.Parse(node.Attributes["value"].Value));
                    }
                }

                return table;
            }
            catch (Exception ex)
            {
                // logger.Error("ConfigurationXmlParser.cs", "IsOnline_TotalMarks", "Error Occured while check Online Total Marks", ex, logPath);
                throw ex;
            }
        }

        public bool IsOnline_TotalMarks(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isTotalMarks = false;
                XmlNode node = doc.SelectSingleNode("Configuration/online_TotalMarks");

                if (node != null)
                {
                    isTotalMarks = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isTotalMarks;
            }
            catch (Exception ex)
            {
                // logger.Error("ConfigurationXmlParser.cs", "IsOnline_TotalMarks", "Error Occured while check Online Total Marks", ex, logPath);
                throw ex;
            }
        }

        public bool IsOnline_MarksObtained(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isMarksObtained = false;
                XmlNode node = doc.SelectSingleNode("Configuration/online_MarksObtained");

                if (node != null)
                {
                    isMarksObtained = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isMarksObtained;
            }
            catch (Exception ex)
            {
                // logger.Error("ConfigurationXmlParser.cs", "IsOnline_MarksObtained", "Error Occured while check Online Marks Obtained", ex, logPath);
                throw ex;
            }
        }

        public bool IsOnline_Board(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isBoard = false;
                XmlNode node = doc.SelectSingleNode("Configuration/online_Board");

                if (node != null)
                {
                    isBoard = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isBoard;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "IsOnline_Board", "Error Occured while check Online Board", ex, logPath);
                throw ex;
            }
        }

        public bool IsOnline_FormFee(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isFormFee = false;
                XmlNode node = doc.SelectSingleNode("Configuration/online_FormFee");

                if (node != null)
                {
                    isFormFee = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isFormFee;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "IsOnline_FormFee", "Error Occured while check Online Form Fee", ex, logPath);
                throw ex;
            }
        }
        public bool IsOnline_ChallanNumber(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isChallanNumber = false;
                XmlNode node = doc.SelectSingleNode("Configuration/online_ChallanNumber");

                if (node != null)
                {
                    isChallanNumber = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isChallanNumber;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "IsOnline_ChallanNumber", "Error Occured while check Online Challan Number", ex, logPath);
                throw ex;
            }
        }
        public bool IsOnline_PreviousATKT(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isPreviousATKT = false;
                XmlNode node = doc.SelectSingleNode("Configuration/online_PreviousATKT");

                if (node != null)
                {
                    isPreviousATKT = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isPreviousATKT;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "IsOnline_PreviousATKT", "Error Occured while check Online Previous ATKT", ex, logPath);
                throw ex;
            }
        }

        public string OnlineFeeReceipt_FeeHeadText(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                string text = string.Empty;
                XmlNode node = doc.SelectSingleNode("Configuration/online_FeeHeadName");

                if (node != null)
                {
                    text = node.Attributes["value"].Value;
                }

                return text;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "OnlineFeeReceipt_FeeHeadText", "Error Occured while getting Online Fee Receipt", ex, logPath);
                throw ex;
            }
        }

        public int Online_FeeHeadAmount(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                int amt = 0;
                XmlNode node = doc.SelectSingleNode("Configuration/online_FormAmount");

                if (node != null)
                {
                    amt = int.Parse(node.Attributes["value"].Value);
                }

                return amt;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "Online_FeeHeadAmount", "Error Occured while getting Online Fee Head Amount", ex, logPath);
                throw ex;
            }
        }

        public string GetLableName(string nm, string logPath, string configurationPath)
        {
            try
            {
                string name = string.Empty;
                XmlDocument doc = OpenFile(logPath, configurationPath);

                XmlNode formLables = doc.SelectSingleNode("Configuration/FormLables");
                if (formLables != null)
                {
                    XmlNodeList page = formLables.SelectNodes("LableName");
                    int count = page.Count;

                    foreach (XmlNode node in page)
                    {
                        if (nm == node.Attributes["name"].Value)
                        {
                            name = node.InnerText;
                        }
                    }
                }
                return name;

            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "GetLableName", "Error Occured while getting Lable Name", ex, logPath);
                throw ex;
            }
        }

        public bool IsEli_And_New_OnReceipt(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isOnReceipt = false;
                XmlNode node = doc.SelectSingleNode("Configuration/EliAndNewStudent_OnReceipt");

                if (node != null)
                {
                    isOnReceipt = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isOnReceipt;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "IsEli_And_New_OnReceipt", "Error Occured while Check Receipt", ex, logPath);
                throw ex;
            }
        }

        public string FirstReceipt_TopMargin(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                string margin = string.Empty;
                XmlNode node = doc.SelectSingleNode("Configuration/TopMarginFor_FirstReceipt");

                if (node != null)
                {
                    margin = node.Attributes["value"].Value;
                }

                return margin;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "FirstReceipt_TopMargin", "Error Occured while getting Top Marging of First Receipt", ex, logPath);
                throw ex;
            }
        }

        public string SecondReceipt_TopMargin(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                string margin = string.Empty;
                XmlNode node = doc.SelectSingleNode("Configuration/TopMarginFor_SecondReceipt");

                if (node != null)
                {
                    margin = node.Attributes["value"].Value;
                }

                return margin;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "SecondReceipt_TopMargin", "Error Occured while getting Top Marging of Second Receipt", ex, logPath);
                throw ex;
            }
        }

        public string ThirdReceipt_TopMargin(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                string margin = string.Empty;
                XmlNode node = doc.SelectSingleNode("Configuration/TopMarginFor_ThirdReceipt");

                if (node != null)
                {
                    margin = node.Attributes["value"].Value;
                }

                return margin;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "ThirdReceipt_TopMargin", "Error Occured while getting Top Marging of Third Receipt", ex, logPath);
                throw ex;
            }
        }

        public bool IsGenerateExcel_WithManuallyCreation(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                bool isExcel = false;
                XmlNode node = doc.SelectSingleNode("Configuration/GenerateExcelWithManuallyCreation");

                if (node != null)
                {
                    isExcel = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isExcel;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "IsGenerateExcel_WithManuallyCreation", "Error Occured while generating Excel whie Manually Creation", ex, logPath);
                throw ex;
            }
        }

        public string GetLoginPageURL(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                string url = string.Empty;
                XmlNode node = doc.SelectSingleNode("Configuration/LoginPageURL");

                if (node != null)
                {
                    url = node.Attributes["value"].Value;
                }

                return url;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "IsGenerateExcel_WithManuallyCreation", "Error Occured while generating Excel whie Manually Creation", ex, logPath);
                throw ex;
            }
        }

        public LoggerDetails GetLoggerDetails(string configFilePath)
        {
            try
            {
                LoggerDetails logDetails = new LoggerDetails();

                XmlDocument doc = OpenFile("", configFilePath);

                if (doc == null)
                {
                    return logDetails;
                }

                XmlNode newlogger = doc.SelectSingleNode("Configuration/Logger");

                if (newlogger != null)
                {
                    if (newlogger.SelectSingleNode("LogLevel") != null)
                    {
                        logDetails.LogLevel = newlogger.SelectSingleNode("LogLevel").InnerText;
                    }

                    if (newlogger.SelectSingleNode("LogPath") != null)
                    {
                        logDetails.LogFilePath = newlogger.SelectSingleNode("LogPath").InnerText;
                    }

                    if (newlogger.SelectSingleNode("LogFileName") != null)
                    {
                        logDetails.LogFileName = newlogger.SelectSingleNode("LogFileName").InnerText;
                    }

                    if (newlogger.SelectSingleNode("LogFileSize") != null)
                    {
                        logDetails.LogFileSizeMB = newlogger.SelectSingleNode("LogFileSize").InnerText;
                    }

                    if (newlogger.SelectSingleNode("LogType") != null)
                    {
                        logDetails.LogType = newlogger.SelectSingleNode("LogType").InnerText;
                    }

                    if (newlogger.SelectSingleNode("LogDBConnectionString") != null)
                    {
                        logDetails.LogDBConnectionString = newlogger.SelectSingleNode("LogDBConnectionString").InnerText;
                    }

                    return logDetails;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public int GetSinceYearValue(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                int Value = 0;
                XmlNode node = doc.SelectSingleNode("Configuration/SinceYear");

                if (node != null)
                {
                    Value = int.Parse(node.Attributes["value"].Value);
                }

                return Value;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "Online_FeeHeadAmount", "Error Occured while getting Online Fee Head Amount", ex, logPath);
                throw ex;
            }
        }

        public bool IsReceiptHeaderRequired(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                XmlNode node = doc.SelectSingleNode("Configuration/ReceiptLabels/FeeReceipt");
                if (node.Attributes["header"] != null)
                {
                    return bool.Parse(node.Attributes["header"].Value);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ReceiptConfig> GetReceiptConfig(string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                XmlNodeList nodes = doc.SelectNodes("Configuration/ReceiptLabels/FeeReceipt/Line");
                List<ReceiptConfig> configList = new List<ReceiptConfig>();
                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        ReceiptConfig config = new ReceiptConfig();
                        config.Bold = node.Attributes["bold"] == null ? false : bool.Parse(node.Attributes["bold"].Value);
                        config.Display = node.Attributes["show"] == null ? false : bool.Parse(node.Attributes["show"].Value);
                        config.FontSize = node.Attributes["fontsize"] == null ? 8 : int.Parse(node.Attributes["fontsize"].Value);
                        config.Id = node.Attributes["id"] == null ? 0 : int.Parse(node.Attributes["id"].Value);
                        config.LineType = node.Attributes["type"] == null ? "text" : node.Attributes["type"].Value;
                        config.LLX = node.Attributes["llx"] == null ? 0 : int.Parse(node.Attributes["llx"].Value);
                        config.LLY = node.Attributes["lly"] == null ? 0 : int.Parse(node.Attributes["lly"].Value);
                        config.Text = node.InnerText;
                        config.URX = node.Attributes["urx"] == null ? 0 : int.Parse(node.Attributes["urx"].Value);
                        config.URY = node.Attributes["ury"] == null ? 0 : int.Parse(node.Attributes["ury"].Value);
                        config.ImagePath = node.Attributes["logopath"] != null ? node.Attributes["logopath"].Value : string.Empty;
                        config.Width = node.Attributes["width"] == null ? 0 : int.Parse(node.Attributes["width"].Value);
                        config.Height = node.Attributes["height"] == null ? 0 : int.Parse(node.Attributes["height"].Value);
                        config.Scale = node.Attributes["scale"] == null ? 0 : int.Parse(node.Attributes["scale"].Value);
                        config.Section = node.Attributes["section"] == null ? string.Empty : node.Attributes["section"].Value;
                        config.OffSet = node.Attributes["offset"] == null ? 0 : int.Parse(node.Attributes["offset"].Value);
                        configList.Add(config);
                    }
                }

                return configList;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "Online_FeeHeadAmount", "Error Occured while getting Online Fee Head Amount", ex, logPath);
                throw ex;
            }
        }

        public RedirectConfig GetRedirectConfig(string pageName, string logPath, string configPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configPath);
                XmlNodeList nodes = doc.SelectNodes("Configuration/RedirectUrl/Page[@name='" + pageName + "']/Button");
                RedirectConfig config = new RedirectConfig();
                List<RedirectButtonConfig> configList = new List<RedirectButtonConfig>();
                config.ButtonConfig = configList;

                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        RedirectButtonConfig button = new RedirectButtonConfig();
                        button.Id = node.Attributes["id"] == null ? 0 : int.Parse(node.Attributes["id"].Value);
                        button.Title = node.Attributes["title"] == null ? string.Empty : node.Attributes["title"].Value;
                        button.Url = node.Attributes["url"] == null ? string.Empty : node.Attributes["url"].Value;
                        configList.Add(button);
                    }
                }

                return config;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "Online_FeeHeadAmount", "Error Occured while getting Online Fee Head Amount", ex, logPath);
                throw ex;
            }
        }

        public void GetOnlineRegistrationContent(ref XmlConfigurations config, string type, string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                XmlNodeList onlineForms = doc.SelectNodes("Configuration/OnlineForm/Form");

                if (onlineForms != null)
                {
                    foreach (XmlNode onlineForm in onlineForms)
                    {
                        if (onlineForm.Attributes["type"].Value.ToLower() == type.ToLower())
                        {
                            XmlNodeList leavingContent = onlineForm.SelectNodes("Line");
                            Leaving leavingCertificate = new Leaving();
                            leavingCertificate.LeavingCertificates = new List<LeavingCertificate>();
                            leavingCertificate.LCType = onlineForm.Attributes["type"] != null ? onlineForm.Attributes["type"].Value : string.Empty;
                            config.LeavingCertificates = leavingCertificate.LeavingCertificates;

                            leavingCertificate.IsHeaderRequired = onlineForm.Attributes["header"] == null ? true : bool.Parse(onlineForm.Attributes["header"].Value);

                            foreach (XmlNode line in leavingContent)
                            {
                                LeavingCertificate lc = new LeavingCertificate();
                                lc.Display = line.Attributes["show"] == null ? false : bool.Parse(line.Attributes["show"].Value);
                                lc.Align = line.Attributes["align"] == null ? string.Empty : line.Attributes["align"].Value;
                                lc.FontSize = line.Attributes["fontsize"] == null ? 9 : int.Parse(line.Attributes["fontsize"].Value);
                                lc.id = line.Attributes["id"] == null ? 0 : int.Parse(line.Attributes["id"].Value);
                                lc.IsBold = line.Attributes["bold"] == null ? false : bool.Parse(line.Attributes["bold"].Value);
                                lc.LeavingLine = line.InnerText;
                                lc.LineType = line.Attributes["type"] == null ? "text" : line.Attributes["type"].Value;
                                lc.LowerLeftXCoOrd = line.Attributes["llx"] == null ? 0 : int.Parse(line.Attributes["llx"].Value);
                                lc.LowerLeftYCoOrd = line.Attributes["lly"] == null ? 0 : int.Parse(line.Attributes["lly"].Value);
                                lc.UpperRightXCoOrd = line.Attributes["urx"] == null ? 0 : int.Parse(line.Attributes["urx"].Value);
                                lc.UpperRightYCoOrd = line.Attributes["ury"] == null ? 0 : int.Parse(line.Attributes["ury"].Value);
                                lc.ShowColonSeparator = line.Attributes["showcolon"] == null ? false : bool.Parse(line.Attributes["showcolon"].Value);
                                leavingCertificate.LeavingCertificates.Add(lc);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "GetLeavingContent", "Error Occured while getting Leaving Content", ex, logPath);
                throw ex;
            }
        }

        //function added by vasim on 15-apr-14 for getting admission from configruration
        public void GetAdmissionFormContent(ref XmlConfigurations config, string type, string logPath, string configurationPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configurationPath);
                XmlNodeList onlineForms = doc.SelectNodes("Configuration/AdmissionFrom/Form");

                if (onlineForms != null)
                {
                    foreach (XmlNode onlineForm in onlineForms)
                    {
                        if (onlineForm.Attributes["type"].Value.ToLower() == type.ToLower())
                        {
                            XmlNodeList leavingContent = onlineForm.SelectNodes("Line");
                            Leaving leavingCertificate = new Leaving();
                            leavingCertificate.LeavingCertificates = new List<LeavingCertificate>();
                            leavingCertificate.LCType = onlineForm.Attributes["type"] != null ? onlineForm.Attributes["type"].Value : string.Empty;
                            config.LeavingCertificates = leavingCertificate.LeavingCertificates;

                            leavingCertificate.IsHeaderRequired = onlineForm.Attributes["header"] == null ? true : bool.Parse(onlineForm.Attributes["header"].Value);

                            foreach (XmlNode line in leavingContent)
                            {
                                LeavingCertificate lc = new LeavingCertificate();
                                lc.Display = line.Attributes["show"] == null ? false : bool.Parse(line.Attributes["show"].Value);
                                lc.Align = line.Attributes["align"] == null ? string.Empty : line.Attributes["align"].Value;
                                lc.FontSize = line.Attributes["fontsize"] == null ? 9 : int.Parse(line.Attributes["fontsize"].Value);
                                lc.id = line.Attributes["id"] == null ? 0 : int.Parse(line.Attributes["id"].Value);
                                lc.IsBold = line.Attributes["bold"] == null ? false : bool.Parse(line.Attributes["bold"].Value);
                                lc.LeavingLine = line.InnerText;
                                lc.LineType = line.Attributes["type"] == null ? "text" : line.Attributes["type"].Value;
                                lc.LowerLeftXCoOrd = line.Attributes["llx"] == null ? 0 : int.Parse(line.Attributes["llx"].Value);
                                lc.LowerLeftYCoOrd = line.Attributes["lly"] == null ? 0 : int.Parse(line.Attributes["lly"].Value);
                                lc.UpperRightXCoOrd = line.Attributes["urx"] == null ? 0 : int.Parse(line.Attributes["urx"].Value);
                                lc.UpperRightYCoOrd = line.Attributes["ury"] == null ? 0 : int.Parse(line.Attributes["ury"].Value);
                                lc.ShowColonSeparator = line.Attributes["showcolon"] == null ? false : bool.Parse(line.Attributes["showcolon"].Value);
                                leavingCertificate.LeavingCertificates.Add(lc);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "GetLeavingContent", "Error Occured while getting Leaving Content", ex, logPath);
                throw ex;
            }
        }

        public bool GetExamFeesType(string logPath, string configPath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configPath);
                XmlNode node = doc.SelectSingleNode("Configuration/ExamFees");
                bool isExamFeesEqual = true;
                if (node != null)
                {
                    isExamFeesEqual = node.Attributes["equal"] == null ? true : bool.Parse(node.Attributes["equal"].Value);
                }

                return isExamFeesEqual;
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "Online_FeeHeadAmount", "Error Occured while getting Online Fee Head Amount", ex, logPath);
                throw ex;
            }
        }

        // added by rajesh on 19-5-2014
        public bool GetPayemntDetail(string logpath, string configFilePath)
        {
            bool paymentType = true;
            try
            {
                XmlDocument doc = OpenFile(logpath, configFilePath);

                XmlNode node = doc.SelectSingleNode("Configuration/MakePayment");

                if (node != null)
                {
                    paymentType = node.Attributes["display"].Value.ToLower() == "true" ? true : false;
                    return paymentType;
                }
                else
                {
                    return paymentType;
                }
            }
            catch (Exception ex)
            {
                return paymentType;
            }
        }


        public string GetCollegeType(string logPath, string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(logPath, configFilePath); ;

                XmlNode node = doc.SelectSingleNode("Configuration/CollegeType");

                if (node != null)
                {
                    return node.Attributes["type"].Value;
                }

                return "junior";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetClassDetails(string logFilePath, string configFilePath)
        {
            List<string> str = new List<string>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(configFilePath);

                XmlDocument xdoc = OpenFile(logFilePath, configFilePath);
                XmlNodeList nodes = doc.SelectNodes("Configuration/Print/TransferCertificate/ClassName/Test");
                XmlNode nod = xdoc.SelectSingleNode("ClassName");

                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        if (node != null)
                        {
                            string clgName = node["Class"].InnerText;
                            str.Add(clgName);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return str;
        }

        public List<string> GetMediumDetails(string logPath, string configFilePath)
        {
            List<string> str = new List<string>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(configFilePath);
                XmlDocument xdoc = OpenFile(logPath, configFilePath);

                XmlNodeList nodes = doc.SelectNodes("Configuration/Print/TransferCertificate/Language/Mediums");
                foreach (XmlNode node in nodes)
                {
                    if (node != null)
                    {
                        string med = node["Medium"].InnerText;
                        str.Add(med);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return str;
        }

        public List<string> GetStatusDetails(string logPath, string configFilePath)
        {
            List<string> str = new List<string>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(configFilePath);
                XmlDocument xdoc = OpenFile(logPath, configFilePath);

                XmlNodeList nodes = doc.SelectNodes("Configuration/Print/TransferCertificate/PassingStatus/stats");
                foreach (XmlNode node in nodes)
                {
                    string med = node["status"].InnerText;
                    str.Add(med);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return str;
        }

        public List<string> GetSemesterDetails(string logPath, string configFilePath)
        {
            List<string> str = new List<string>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(configFilePath);
                XmlDocument xdoc = OpenFile(logPath, configFilePath);

                XmlNodeList nodes = doc.SelectNodes("Configuration/Print/TransferCertificate/Semesters/Semester");
                foreach (XmlNode node in nodes)
                {
                    if (node != null)
                    {
                        string med = node["Sem"].InnerText;
                        str.Add(med);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return str;
        }

        //end
    }

    public class XmlConfigurations
    {
        public ConfigurationHeaderDetails Header { get; set; }
        public ConfigurationFooterDetails Footer { get; set; }
        public Bonafide BonafideText { get; set; }
        public List<LeavingCertificate> LeavingCertificates { get; set; }
        public Leaving LeavingAdmitDetails { get; set; }
        public string LoginUrl { get; set; }

    }

    public class ReportPage
    {
        public int Id { get; set; }
        public string PageName { get; set; }
    }

    public class RedirectConfig
    {
        public string PageName { get; set; }
        public List<RedirectButtonConfig> ButtonConfig { get; set; }
    }

    public class RedirectButtonConfig
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }

    public class ReceiptConfig
    {
        public int Id { get; set; }
        public string LineType { get; set; }
        public bool Display { get; set; }
        public bool Bold { get; set; }
        public int FontSize { get; set; }
        public int LLX { get; set; }
        public int LLY { get; set; }
        public int URX { get; set; }
        public int URY { get; set; }
        public string Text { get; set; }
        public string ImagePath { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Scale { get; set; }
        public string Section { get; set; }
        public int OffSet { get; set; }
    }


    public class ConfigurationHeaderDetails
    {
        public bool IsRequired { get; set; }
        public string LogoPath { get; set; }
        public string LogoWidth { get; set; }
        public string LogoHeight { get; set; }
        public int LogoX { get; set; }
        public int LogoY { get; set; }
        public int LogoScale { get; set; }
        public List<ConfigHeaderCollegeName> CollegeName { get; set; }
        public List<ConfigHeaderCollegeName> Address { get; set; }
        public List<int> CollegeFontSize { get; set; }
        /* Tag Line Properties */
        public string TagLine { get; set; }
        public bool ShowTagLine { get; set; }
        public bool BoldTagLine { get; set; }
        public int FontSizeTagLine { get; set; }
        public int LLXTagLine { get; set; }
        public int LLYTagLine { get; set; }
        public int URXTagLine { get; set; }
        public int URYTagLine { get; set; }
        /* Small TagLine Properties */
        public string SmallTagLine { get; set; }
        public bool ShowSmallTagLine { get; set; }
        public bool BoldSmallTagLine { get; set; }
        public int FontSizeSmallTagLine { get; set; }
        public int LLXSmallTagLine { get; set; }
        public int LLYSmallTagLine { get; set; }
        public int URXSmallTagLine { get; set; }
        public int URYSmallTagLine { get; set; }

        public string AcademicYear { get; set; }
    }

    public class ConfigurationFooterDetails
    {
        public string FooterLine { get; set; }
        public List<string> CollegeName { get; set; }
    }

    public class ConfigHeaderCollegeName
    {
        public string CollegeName { get; set; }
        public int FontSize { get; set; }
        public bool Display { get; set; }
        public bool Bold { get; set; }
        public int LLX { get; set; }
        public int LLY { get; set; }
        public int URX { get; set; }
        public int URY { get; set; }

        public string Date { get; set; }

    }

    public class ConfigFooterCollegeDetails
    {
        public string CollegeName { get; set; }
        public int FontSize { get; set; }
        public bool Display { get; set; }
        public bool Bold { get; set; }
        public int LLX { get; set; }
        public int LLY { get; set; }
        public int URX { get; set; }
        public int URY { get; set; }
    }

    public class Bonafide
    {
        public List<string> BonafideLine { get; set; }
    }
    public class Leaving
    {
        public List<LeavingCertificate> LeavingCertificates { get; set; }
        public string LCType { get; set; }
        public bool IsHeaderRequired { get; set; }
        public bool UseConfiguredSinceWhenValue { get; set; }
        public bool ShowYear { get; set; }
        public int YearIncValue { get; set; }
        public string LCMonth { get; set; }
    }

    public class LeavingCertificate
    {
        public int id { get; set; }
        public bool Display { get; set; }
        public int FontSize { get; set; }
        public bool IsBold { get; set; }
        public int LowerLeftXCoOrd { get; set; }
        public int LowerLeftYCoOrd { get; set; }
        public int UpperRightXCoOrd { get; set; }
        public int UpperRightYCoOrd { get; set; }
        public string Align { get; set; }
        public string LineType { get; set; }
        public string LeavingLine { get; set; }
        public bool ShowColonSeparator { get; set; }
    }

    public class Scholarship
    {
        public string ScholarshipName { get; set; }
        public string amount { get; set; }
    }


    public class LoggerDetails
    {
        public string LogLevel { get; set; }
        public string LogFilePath { get; set; }
        public string LogFileName { get; set; }
        public string LogFileSizeMB { get; set; }
        public string LogType { get; set; }
        public string LogDBConnectionString { get; set; }
    }
}