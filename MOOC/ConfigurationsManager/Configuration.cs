using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Configuration;
using System.IO;
using System.Xml;

namespace ITM.Courses.ConfigurationsManager
{
    public class Configurations
    {
        public string file;

        // open existing file or create new file
        public XmlDocument OpenFile(string configFilePath)
        {
            try
            {
                //file = System.Configuration.ConfigurationManager.AppSettings["BASE_PATH"] + "/Release/Configuration.xml";

                if (string.IsNullOrEmpty(configFilePath))
                {
                    file = System.Configuration.ConfigurationManager.AppSettings["BASE_PATH"] + "/Release/Configuration.xml";
                }
                else
                {
                    file = configFilePath;
                }

                XmlDocument doc;
                //Verify whether a file is exists or not
                if (!File.Exists(file))
                {
                    doc = new XmlDocument();
                    XmlNode root = doc.CreateNode(XmlNodeType.Element, "Configuration", "");

                    XmlNode logger = doc.CreateNode(XmlNodeType.Element, "Logger", "");
                    XmlNode emailSetting = doc.CreateNode(XmlNodeType.Element, "EmailSettings", "");
                    XmlNode seatNumber = doc.CreateNode(XmlNodeType.Element, "SeatNumber", "");
                    XmlNode semester = doc.CreateNode(XmlNodeType.Element, "Semesters", "");
                    XmlNode pages = doc.CreateNode(XmlNodeType.Element, "Pages", "");
                    root.AppendChild(logger);
                    root.AppendChild(emailSetting);
                    root.AppendChild(seatNumber);
                    root.AppendChild(semester);
                    root.AppendChild(pages);
                    doc.AppendChild(root);
                }
                else
                {
                    doc = new XmlDocument();
                    doc.Load(file);
                }

                return doc;
            }
            catch (Exception ex)
            {
                throw new Exception("11576");
            }
        }

        public bool IsPhotoRequired(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                bool isRequired = false;
                XmlNode node = doc.SelectSingleNode("Configuration/Photograph");

                if (node != null)
                {
                    isRequired = Convert.ToBoolean(node.Attributes["IsRequired"].Value);
                }

                return isRequired;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddUpdateLogger(LoggerDetails objLogger, string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);

                XmlNode newlogger = doc.SelectSingleNode("Configuration/Logger");

                if (newlogger != null)
                {
                    //add level
                    if (newlogger.SelectSingleNode("LogLevel") != null)
                    {
                        newlogger.SelectSingleNode("LogLevel").InnerText = objLogger.LogLevel.ToString();
                    }
                    else
                    {
                        XmlNode logLevel = doc.CreateNode(XmlNodeType.Element, "LogLevel", "");
                        logLevel.InnerText = objLogger.LogLevel.ToString();
                        newlogger.AppendChild(logLevel);
                    }

                    //add logpath
                    if (newlogger.SelectSingleNode("LogPath") != null)
                    {
                        newlogger.SelectSingleNode("LogPath").InnerText = objLogger.LogFilePath;
                    }
                    else
                    {
                        XmlNode logPath = doc.CreateNode(XmlNodeType.Element, "LogPath", "");
                        logPath.InnerText = objLogger.LogFilePath;
                        newlogger.AppendChild(logPath);
                    }

                    //add logFileName
                    if (newlogger.SelectSingleNode("LogFileName") != null)
                    {
                        newlogger.SelectSingleNode("LogFileName").InnerText = objLogger.LogFileName;
                    }
                    else
                    {
                        XmlNode logFileName = doc.CreateNode(XmlNodeType.Element, "LogFileName", "");
                        logFileName.InnerText = objLogger.LogFileName;
                        newlogger.AppendChild(logFileName);
                    }

                    //add logFileSize
                    if (newlogger.SelectSingleNode("LogFileSize") != null)
                    {
                        newlogger.SelectSingleNode("LogFileSize").InnerText = objLogger.LogFileSizeMB.ToString();
                    }
                    else
                    {
                        XmlNode logFileSize = doc.CreateNode(XmlNodeType.Element, "LogFileSize", "");
                        logFileSize.InnerText = objLogger.LogFileSizeMB.ToString();
                        newlogger.AppendChild(logFileSize);
                    }

                    //add logType
                    if (newlogger.SelectSingleNode("LogType") != null)
                    {
                        newlogger.SelectSingleNode("LogType").InnerText = objLogger.LogType;
                    }
                    else
                    {
                        XmlNode logType = doc.CreateNode(XmlNodeType.Element, "LogType", "");
                        logType.InnerText = objLogger.LogType;
                        newlogger.AppendChild(logType);
                    }

                    //add logDBConnectionstring
                    if (newlogger.SelectSingleNode("LogDBConnectionString") != null)
                    {
                        newlogger.SelectSingleNode("LogDBConnectionString").InnerText = objLogger.LogDBConnectionString;
                    }
                    else
                    {
                        XmlNode logDBCon = doc.CreateNode(XmlNodeType.Element, "LogDBConnectionString", "");
                        logDBCon.InnerText = objLogger.LogDBConnectionString;
                        newlogger.AppendChild(logDBCon);
                    }
                }

                doc.Save(file);
            }
            catch (Exception ex)
            {
                throw new Exception("11577");
            }
        }

        public void AddUpdateSeatNumbers(SeatDetails seatDetails, string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                XmlNode newSeat = doc.SelectSingleNode("Configuration/SeatNumber");

                bool isRecordNotExsit = true;

                if (newSeat != null)
                {
                    XmlNodeList nodeList = newSeat.SelectNodes("Course");

                    if (nodeList.Count > 0)
                    {
                        foreach (XmlNode node in nodeList)
                        {
                            if (node.Attributes["id"].Value == seatDetails.CourseId)
                            {
                                isRecordNotExsit = false;

                                node.Attributes["name"].Value = seatDetails.CourseName;
                                node.SelectSingleNode("Initial").InnerText = seatDetails.Initial;
                                node.SelectSingleNode("Year").InnerText = seatDetails.Year;
                                node.SelectSingleNode("Semester").InnerText = seatDetails.Semester;
                                node.SelectSingleNode("StartNo").InnerText = seatDetails.StartNo;
                                node.SelectSingleNode("EndNo").InnerText = seatDetails.EndNo;
                            }
                        }

                        if (isRecordNotExsit)
                        {
                            // if current record not exist the create new node and add
                            newSeat.AppendChild(CreateNewNode(doc, seatDetails));
                        }
                    }
                    else
                    {
                        // if nodelist count is 0 then add new course node
                        newSeat.AppendChild(CreateNewNode(doc, seatDetails));
                    }
                }

                doc.Save(file);
            }
            catch (Exception ex)
            {
                throw new Exception("11578");
            }
        }

        public XmlNode CreateNewNode(XmlDocument doc, SeatDetails seatDetails)
        {
            try
            {
                XmlNode course = doc.CreateNode(XmlNodeType.Element, "Course", "");
                XmlAttribute id = doc.CreateAttribute("id");
                id.Value = seatDetails.CourseId;
                course.Attributes.Append(id);

                XmlAttribute name = doc.CreateAttribute("name");
                name.Value = seatDetails.CourseName;
                course.Attributes.Append(name);

                XmlNode initial = doc.CreateNode(XmlNodeType.Element, "Initial", "");
                initial.InnerText = seatDetails.Initial;
                course.AppendChild(initial);

                XmlNode year = doc.CreateNode(XmlNodeType.Element, "Year", "");
                year.InnerText = seatDetails.Year;
                course.AppendChild(year);

                XmlNode semester = doc.CreateNode(XmlNodeType.Element, "Semester", "");
                semester.InnerText = seatDetails.Semester;
                course.AppendChild(semester);

                XmlNode startNo = doc.CreateNode(XmlNodeType.Element, "StartNo", "");
                startNo.InnerText = seatDetails.StartNo;
                course.AppendChild(startNo);

                XmlNode endNo = doc.CreateNode(XmlNodeType.Element, "EndNo", "");
                endNo.InnerText = seatDetails.EndNo;
                course.AppendChild(endNo);

                return course;
            }
            catch (Exception ex)
            {
                throw new Exception("11579");
            }
        }

        public SeatDetails GetSeatDetailsByStreamName(string streamName, string semesterName, string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                XmlNode newSeat = doc.SelectSingleNode("Configuration/SeatNumber");

                SeatDetails seatDetails = new SeatDetails();

                if (newSeat != null)
                {
                    XmlNodeList nodeList = newSeat.SelectNodes("Course");

                    if (nodeList.Count > 0)
                    {
                        foreach (XmlNode node in nodeList)
                        {
                            if (node.Attributes["name"].Value == streamName)
                            {
                                if (node.SelectSingleNode("Semester").InnerText == semesterName)
                                {
                                    seatDetails.CourseId = node.Attributes["id"].Value;
                                    seatDetails.CourseName = node.Attributes["name"].Value;
                                    seatDetails.Initial = node.SelectSingleNode("Initial").InnerText;
                                    seatDetails.Year = node.SelectSingleNode("Year").InnerText;
                                    seatDetails.Semester = node.SelectSingleNode("Semester").InnerText;
                                    seatDetails.StartNo = node.SelectSingleNode("StartNo").InnerText;
                                    seatDetails.EndNo = node.SelectSingleNode("EndNo").InnerText;
                                }
                            }
                        }
                    }
                }

                return seatDetails;
            }
            catch (Exception ex)
            {
                throw new Exception("11580");
            }
        }

        //public static SeatDetails GetSeatDetailsByStreamName(string streamName, string month)
        //{
        //    try
        //    {
        //        XmlDocument doc = OpenFile();
        //        XmlNode newSeat = doc.SelectSingleNode("Configuration/SeatNumber");

        //        SeatDetails seatDetails = new SeatDetails();

        //        if (newSeat != null)
        //        {
        //            XmlNodeList nodeList = newSeat.SelectNodes("Course");

        //            if (nodeList.Count > 0)
        //            {
        //                foreach (XmlNode node in nodeList)
        //                {
        //                    if (node.Attributes["name"].Value == streamName && node.SelectSingleNode("Semester").InnerText == month)
        //                    {
        //                        seatDetails.CourseId = node.Attributes["id"].Value;
        //                        seatDetails.CourseName = node.Attributes["name"].Value;
        //                        seatDetails.Initial = node.SelectSingleNode("Initial").InnerText;
        //                        seatDetails.Year = node.SelectSingleNode("Year").InnerText;
        //                        seatDetails.Semester = node.SelectSingleNode("Semester").InnerText; ;
        //                        seatDetails.StartNo = node.SelectSingleNode("StartNo").InnerText;
        //                        seatDetails.EndNo = node.SelectSingleNode("EndNo").InnerText;
        //                    }
        //                }
        //            }
        //        }

        //        return seatDetails;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("11580");
        //    }
        //}

        public List<SeatDetails> GetAllSeatDetails(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                XmlNode newSeat = doc.SelectSingleNode("Configuration/SeatNumber");

                List<SeatDetails> seatDetailsList = new List<SeatDetails>();

                if (newSeat != null)
                {
                    XmlNodeList nodeList = newSeat.SelectNodes("Course");

                    if (nodeList.Count > 0)
                    {
                        foreach (XmlNode node in nodeList)
                        {
                            SeatDetails seatDetails = new SeatDetails();
                            seatDetails.CourseId = node.Attributes["id"].Value;
                            seatDetails.CourseName = node.Attributes["name"].Value;

                            seatDetails.Initial = node.SelectSingleNode("Initial").InnerText;
                            seatDetails.Year = node.SelectSingleNode("Year").InnerText;
                            seatDetails.Semester = node.SelectSingleNode("Semester").InnerText; ;
                            seatDetails.StartNo = node.SelectSingleNode("StartNo").InnerText;
                            seatDetails.EndNo = node.SelectSingleNode("EndNo").InnerText;

                            seatDetailsList.Add(seatDetails);
                        }
                    }
                }

                return seatDetailsList;
            }
            catch (Exception ex)
            {
                throw new Exception("11581");
            }
        }

        public void AddUpdateEmailSettings(EmailDetails objEmail, string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);

                XmlNode newEmailSetting = doc.SelectSingleNode("Configuration/EmailSettings");

                if (newEmailSetting != null)
                {
                    //add level
                    if (newEmailSetting.SelectSingleNode("EmailAddress") != null)
                    {
                        newEmailSetting.SelectSingleNode("EmailAddress").InnerText = objEmail.EmailAddress;
                    }
                    else
                    {
                        XmlNode node = doc.CreateNode(XmlNodeType.Element, "EmailAddress", "");
                        node.InnerText = objEmail.EmailAddress;
                        newEmailSetting.AppendChild(node);
                    }

                    //add logpath
                    if (newEmailSetting.SelectSingleNode("UserName") != null)
                    {
                        newEmailSetting.SelectSingleNode("UserName").InnerText = objEmail.UserName;
                    }
                    else
                    {
                        XmlNode node = doc.CreateNode(XmlNodeType.Element, "UserName", "");
                        node.InnerText = objEmail.UserName;
                        newEmailSetting.AppendChild(node);
                    }

                    //add logFileName
                    if (newEmailSetting.SelectSingleNode("Password") != null)
                    {
                        newEmailSetting.SelectSingleNode("Password").InnerText = objEmail.Password;
                    }
                    else
                    {
                        XmlNode node = doc.CreateNode(XmlNodeType.Element, "Password", "");
                        node.InnerText = objEmail.Password;
                        newEmailSetting.AppendChild(node);
                    }

                    //add logFileSize
                    if (newEmailSetting.SelectSingleNode("Port") != null)
                    {
                        newEmailSetting.SelectSingleNode("Port").InnerText = objEmail.Port.ToString();
                    }
                    else
                    {
                        XmlNode node = doc.CreateNode(XmlNodeType.Element, "Port", "");
                        node.InnerText = objEmail.Port.ToString();
                        newEmailSetting.AppendChild(node);
                    }

                    //add logType
                    if (newEmailSetting.SelectSingleNode("Server") != null)
                    {
                        newEmailSetting.SelectSingleNode("Server").InnerText = objEmail.Server;
                    }
                    else
                    {
                        XmlNode node = doc.CreateNode(XmlNodeType.Element, "Server", "");
                        node.InnerText = objEmail.Server;
                        newEmailSetting.AppendChild(node);
                    }

                    //add logDBConnectionstring
                    if (newEmailSetting.SelectSingleNode("IsSSLEnabled") != null)
                    {
                        newEmailSetting.SelectSingleNode("IsSSLEnabled").InnerText = objEmail.IsSSLEnable.ToString();
                    }
                    else
                    {
                        XmlNode node = doc.CreateNode(XmlNodeType.Element, "IsSSLEnabled", "");
                        node.InnerText = objEmail.IsSSLEnable.ToString();
                        newEmailSetting.AppendChild(node);
                    }

                    if (newEmailSetting.SelectSingleNode("IsHTMLResponse") != null)
                    {
                        newEmailSetting.SelectSingleNode("IsHTMLResponse").InnerText = objEmail.IsHTMLResponse.ToString();
                    }
                    else
                    {
                        XmlNode node = doc.CreateNode(XmlNodeType.Element, "IsHTMLResponse", "");
                        node.InnerText = objEmail.IsHTMLResponse.ToString();
                        newEmailSetting.AppendChild(node);
                    }
                }

                doc.Save(file);
            }
            catch (Exception ex)
            {
                throw new Exception("11582");
            }
        }

        public LoggerDetails GetLoggerDetails(string configFilePath)
        {
            try
            {
                LoggerDetails logDetails = new LoggerDetails();

                XmlDocument doc = OpenFile(configFilePath);

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
                throw new Exception("11583");
            }
            return null;
        }

        public EmailDetails GetEmailSettings(string configFilePath)
        {
            try
            {
                EmailDetails emailDetails = new EmailDetails();

                XmlDocument doc = OpenFile(configFilePath);

                XmlNode newEmail = doc.SelectSingleNode("Configuration/EmailSettings");

                if (newEmail != null)
                {
                    if (newEmail.SelectSingleNode("EmailAddress") != null)
                    {
                        emailDetails.EmailAddress = newEmail.SelectSingleNode("EmailAddress").InnerText;
                    }

                    if (newEmail.SelectSingleNode("UserName") != null)
                    {
                        emailDetails.UserName = newEmail.SelectSingleNode("UserName").InnerText;
                    }

                    if (newEmail.SelectSingleNode("Password") != null)
                    {
                        emailDetails.Password = newEmail.SelectSingleNode("Password").InnerText;
                    }

                    if (newEmail.SelectSingleNode("Port") != null)
                    {
                        emailDetails.Port = newEmail.SelectSingleNode("Port").InnerText;
                    }

                    if (newEmail.SelectSingleNode("Server") != null)
                    {
                        emailDetails.Server = newEmail.SelectSingleNode("Server").InnerText;
                    }

                    if (newEmail.SelectSingleNode("IsSSLEnabled") != null)
                    {
                        emailDetails.IsSSLEnable = bool.Parse(newEmail.SelectSingleNode("IsSSLEnabled").InnerText);
                    }

                    if (newEmail.SelectSingleNode("IsHTMLResponse") != null)
                    {
                        emailDetails.IsHTMLResponse = bool.Parse(newEmail.SelectSingleNode("IsHTMLResponse").InnerText);
                    }

                    return emailDetails;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("11584");
            }
            return null;
        }

        public HallTicketConfig GetHallTicketConfiguration(string configFilePath)
        {
            try
            {
                HallTicketConfig config = new HallTicketConfig();
                XmlDocument doc = OpenFile(configFilePath);
                XmlNode hallticket = doc.SelectSingleNode("Configuration/HallTicket");

                if (hallticket != null)
                {
                    XmlNode header = hallticket.SelectSingleNode("HeaderDetails");
                    if (header != null)
                    {
                        config.IsHeaderRequired = bool.Parse(header.SelectSingleNode("Header").Attributes["required"].Value);
                        config.Address = header.SelectSingleNode("Address").InnerText;
                        config.CollegeName = header.SelectSingleNode("CollegeName").InnerText;
                        config.TagLine = header.SelectSingleNode("TagLine").InnerText;
                        config.TagLine1 = header.SelectSingleNode("TagLine1").InnerText;
                        config.TagLine2 = header.SelectSingleNode("TagLine2").InnerText;
                        config.LogoPath = header.SelectSingleNode("Logo").InnerText;
                        config.LogoWidth = int.Parse(header.SelectSingleNode("LogoSize").Attributes["width"].Value);
                        config.LogoHeight = int.Parse(header.SelectSingleNode("LogoSize").Attributes["height"].Value);
                        config.PostHeader = int.Parse(hallticket.SelectSingleNode("LineBreaks/PostHeader").InnerText);
                        config.PostHallTicket = int.Parse(hallticket.SelectSingleNode("LineBreaks/PostHallTicket").InnerText);
                        config.LineBreakHeight = int.Parse(hallticket.SelectSingleNode("LineBreaks/LineBreakHeight").InnerText);
                        config.PrintHallTicketsPerPage = int.Parse(hallticket.SelectSingleNode("PrintPerPage").InnerText);
                    }

                    return config;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("11583");
            }
            return null;
        }

        public MarksheetConfig GetMarkSheetConfiguration(string configFilePath)
        {
            try
            {
                MarksheetConfig config = new MarksheetConfig();
                XmlDocument doc = OpenFile(configFilePath);
                XmlNode markSheet = doc.SelectSingleNode("Configuration/MarkSheet");

                if (markSheet != null)
                {
                    XmlNode header = markSheet.SelectSingleNode("HeaderDetails");
                    if (header != null)
                    {
                        config.IsHeaderRequired = bool.Parse(header.SelectSingleNode("Header").Attributes["required"].Value);
                        config.Address = header.SelectSingleNode("Address").InnerText;
                        config.CollegeName = header.SelectSingleNode("CollegeName").InnerText;
                        config.TagLine = header.SelectSingleNode("TagLine").InnerText;
                        config.LogoPath = header.SelectSingleNode("Logo").InnerText;
                        config.LogoWidth = int.Parse(header.SelectSingleNode("LogoSize").Attributes["width"].Value);
                        config.LogoHeight = int.Parse(header.SelectSingleNode("LogoSize").Attributes["height"].Value);
                        config.PostHeader = int.Parse(markSheet.SelectSingleNode("LineBreaks/PostHeader").InnerText);
                        config.PostMarkSheet = int.Parse(markSheet.SelectSingleNode("LineBreaks/PostMarkSheet").InnerText);
                        config.LineBreakHeight = int.Parse(markSheet.SelectSingleNode("LineBreaks/LineBreakHeight").InnerText);
                        config.PrintMarkSheetsPerPage = int.Parse(markSheet.SelectSingleNode("PrintPerPage").InnerText);
                        config.DisplayURoNumber = bool.Parse(markSheet.SelectSingleNode("Print").Attributes["DisplayURoNumber"].Value);
                        config.AllowedAtkt = int.Parse(markSheet.SelectSingleNode("AllowedAtkt").Attributes["count"].Value);
                        config.ATKTYPosition = int.Parse(markSheet.SelectSingleNode("Position/ATKTYPosition").Attributes["y"].Value);
                        config.ATKTCounterYPosition = int.Parse(markSheet.SelectSingleNode("Position/ATKTCounterYPosition").Attributes["y"].Value);
                        config.ResultYPosition = int.Parse(markSheet.SelectSingleNode("Position/ResultYPosition").Attributes["y"].Value);
                        config.DateYPosition = int.Parse(markSheet.SelectSingleNode("Position/DateYPosition").Attributes["y"].Value);
                    }

                    return config;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("11585");
            }

            return null;
        }

        public int GetAllowedAtktNo(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                XmlNode allowedAtkt = doc.SelectSingleNode("Configuration/AllowedAtkt");

                if (allowedAtkt != null)
                {
                    int allowedAtktNo = Convert.ToInt32(allowedAtkt.SelectSingleNode("Number").InnerText);
                    return allowedAtktNo;
                }
            }
            catch (Exception)
            {
                //return 0;    
                //throw;
            }
            return 0;
        }

        public int GetReserveSeatNo(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                XmlNode reserveSeatNo = doc.SelectSingleNode("Configuration/ReserveSeatNo");

                if (reserveSeatNo != null)
                {
                    int no = Convert.ToInt32(reserveSeatNo.SelectSingleNode("Number").InnerText);
                    return no;
                }
            }
            catch (Exception)
            {
                //return 0;
                //throw;
            }
            return 0;
        }

        public int GetReportCount(string type, string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                XmlNode report = doc.SelectSingleNode("Configuration/Reports");

                if (report != null)
                {
                    XmlNodeList reporttype = report.SelectNodes(type);

                    if (reporttype.Count > 0)
                    {
                        foreach (XmlNode node in reporttype)
                        {

                            int count = int.Parse(node.SelectSingleNode("RecordCount").Attributes["count"].Value);
                            return count;


                        }
                    }
                }
            }
            catch (Exception)
            {
                //return 0;
                //throw;
            }
            return 0;
        }

        public List<MappingPagesConfig> GetPageList(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);

                XmlNode pages = doc.SelectSingleNode("Configuration/Pages");

                List<MappingPagesConfig> mappingPage = new List<MappingPagesConfig>();

                if (pages != null)
                {
                    foreach (XmlNode node in pages.ChildNodes)
                    {
                        MappingPagesConfig mapping = new MappingPagesConfig();
                        mapping.Name = node.Attributes["name"].Value;
                        mapping.Staff = node.Attributes["Staff"].Name;
                        mapping.ExamHead = node.Attributes["ExamHead"].Name;

                        string staff = node.Attributes["Staff"].Name;
                        string examHead = node.Attributes["ExamHead"].Name;

                        List<string> name = new List<string>();
                        name.Add(staff);
                        name.Add(examHead);
                        mapping.RoleName = name;
                        mappingPage.Add(mapping);
                    }

                }
                return mappingPage;
            }
            catch (Exception ex)
            {

                throw;
            }
            // return null;
        }

        public string GetSemesterName(string courseName, string semesterMonth, string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                XmlNode course = doc.SelectSingleNode("Configuration/SemesterName/Course[@name='" + courseName + "']");

                if (course != null)
                {
                    if (course.Attributes[semesterMonth] != null)
                    {
                        return course.Attributes[semesterMonth].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("11588");
            }

            return string.Empty;
        }

        public bool ShowAtktSubjectsInMarksheet(string configFilePath)
        {
            XmlDocument doc = OpenFile(configFilePath);
            try
            {
                XmlNode markSheet = doc.SelectSingleNode("Configuration/MarkSheet");

                if (markSheet != null)
                {
                    XmlNode header = markSheet.SelectSingleNode("AtktSubjects");
                    if (header != null)
                    {
                        return bool.Parse(header.Attributes["display"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("11589");
            }

            return false;
        }

        public bool ShowURONumberInMarksSheet(string configFilePath)
        {
            XmlDocument doc = OpenFile(configFilePath);
            try
            {
                XmlNode markSheet = doc.SelectSingleNode("Configuration/MarkSheet");

                if (markSheet != null)
                {
                    //XmlNode header = markSheet.SelectSingleNode("URONumber");Print
                    XmlNode header = markSheet.SelectSingleNode("Print");
                    if (header != null)
                    {
                        //return bool.Parse(header.Attributes["display"].Value);
                        return bool.Parse(header.Attributes["DisplayURoNumber"].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return false;
        }

        public ApplicationLogoTitleConfig GetApplicationLogoAndTitle(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);

                XmlNode application = doc.SelectSingleNode("Configuration/ApplicationLogoTitle");

                if (application != null)
                {
                    ApplicationLogoTitleConfig applicationConfig = new ApplicationLogoTitleConfig();
                    if (application.SelectSingleNode("Logo") != null && application.SelectSingleNode("Logo").InnerText != null && application.SelectSingleNode("Title") != null && application.SelectSingleNode("Title").InnerText != null)
                    {
                        applicationConfig.ApplicationLogo = application.SelectSingleNode("Logo").InnerText;
                        applicationConfig.ApplicationTitle = application.SelectSingleNode("Title").InnerText;

                        return applicationConfig;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("11590");
            }
        }

        public bool IsOrderBySubCourseID(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);

                XmlNode orderByCourseid = doc.SelectSingleNode("Configuration/OrderbySubCourseId");

                if (orderByCourseid != null)
                {
                    if (orderByCourseid.SelectSingleNode("Value") != null && orderByCourseid.SelectSingleNode("Value").InnerText != null)
                    {
                        return Convert.ToBoolean(orderByCourseid.SelectSingleNode("Value").InnerText);
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("11590");
            }
        }

        public int GetMarkSheetReportPageSize(string configFilePath)
        {
            XmlDocument doc = OpenFile(configFilePath);
            try
            {
                XmlNode markSheetPazeSize = doc.SelectSingleNode("Configuration/PrintMarksSheetReport");

                if (markSheetPazeSize != null)
                {
                    return int.Parse(markSheetPazeSize.Attributes["pagesize"].Value);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return 25;
        }

        public void IncreaseChallanNumber(string configFilePath)
        {
            XmlDocument doc = OpenFile(configFilePath);
            try
            {
                XmlNode challanNumber = doc.SelectSingleNode("Configuration/ChallanNumber");

                if (challanNumber != null)
                {
                    int chalNo = int.Parse(challanNumber.InnerText);
                    chalNo = chalNo + 1;
                    challanNumber.InnerText = chalNo.ToString();
                }

                doc.Save(file);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GetChallanNumber(string configFilePath)
        {
            XmlDocument doc = OpenFile(configFilePath);
            try
            {
                XmlNode challanNumber = doc.SelectSingleNode("Configuration/ChallanNumber");

                if (challanNumber != null)
                {
                    int chalNo = int.Parse(challanNumber.InnerText);

                    return chalNo;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return 0;
        }

        public ExamFeeConfig GetExamConfig(string configFilePath)
        {
            XmlDocument doc = OpenFile(configFilePath);
            try
            {
                XmlNode examReceipt = doc.SelectSingleNode("Configuration/ExamFeeReceipt");
                ExamFeeConfig config = new ExamFeeConfig();
                if (examReceipt != null)
                {
                    #region Populate Header Details
                    XmlNode header = examReceipt.SelectSingleNode("HeaderDetails");
                    config.IsHeaderRequired = bool.Parse(header.SelectSingleNode("Header").Attributes["required"].Value);
                    config.LogoPath = header.SelectSingleNode("Logo").InnerText;
                    config.LogoWidth = header.SelectSingleNode("LogoSize").Attributes["width"].Value;
                    config.LogoHeight = header.SelectSingleNode("LogoSize").Attributes["height"].Value;
                    config.CollegeName = header.SelectSingleNode("CollegeName").InnerText;
                    config.TagLine = header.SelectSingleNode("TagLine").InnerText;
                    config.Address = header.SelectSingleNode("Address").InnerText;
                    #endregion

                    config.ReceiptTitle = examReceipt.SelectSingleNode("Title").InnerText;
                    config.ReceiptContent = new List<string>();
                    XmlNodeList nodes = examReceipt.SelectNodes("ReceiptContent/Line");
                    foreach (XmlNode node in nodes)
                    {
                        config.ReceiptContent.Add(node.InnerText);
                    }
                }

                return config;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsShowExamFeePaidStudents(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);

                try
                {
                    XmlNode markSheetPazeSize = doc.SelectSingleNode("Configuration/ShowExamFeePaidStudents");

                    if (markSheetPazeSize != null)
                    {
                        return bool.Parse(markSheetPazeSize.Attributes["display"].Value);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("11590");
            }
        }

        /// <summary>
        /// Show Challan Number form MIS through API if it is true
        /// </summary>
        /// <returns></returns>
        public bool IsShowChallanNoFromMIS_API(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);

                try
                {
                    XmlNode markSheetPazeSize = doc.SelectSingleNode("Configuration/ShowChallanNoFromAPI");

                    if (markSheetPazeSize != null)
                    {
                        return bool.Parse(markSheetPazeSize.Attributes["display"].Value);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("11590");
            }
        }

        /// <summary>
        /// Get Exam time from configuration.xml file 
        /// </summary>
        /// <returns></returns>
        public List<ExamTimeConfig> GetExamTiming(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                XmlNode examTime = doc.SelectSingleNode("Configuration/ExamTime");

                List<ExamTimeConfig> examTimeList = new List<ExamTimeConfig>();

                if (examTime != null)
                {
                    XmlNodeList nodeList = examTime.SelectNodes("Time");

                    if (nodeList.Count > 0)
                    {
                        foreach (XmlNode node in nodeList)
                        {
                            ExamTimeConfig objExamTime = new ExamTimeConfig();
                            objExamTime.ExamTime = node.Attributes["start"].Value + " to " + node.Attributes["end"].Value;
                            objExamTime.StartTime = node.Attributes["start"].Value;
                            objExamTime.EndTime = node.Attributes["end"].Value;

                            examTimeList.Add(objExamTime);
                        }
                    }
                }

                return examTimeList;
            }
            catch (Exception ex)
            {
                throw new Exception("11581");
            }
        }


        public bool CallDMSHandlerForSavingFiles(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                bool isCall = false;
                XmlNode node = doc.SelectSingleNode("Configuration/IsCallDMSApi");

                if (node != null)
                {
                    isCall = Convert.ToBoolean(node.Attributes["iscall"].Value);
                }

                return isCall;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Previous AkttNull flag from configuration xml file
        /// </summary>
        /// <returns>return value as boolean</returns>
        public bool IsPreviousAtktNull(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                bool previousAtkt = false;
                XmlNode node = doc.SelectSingleNode("Configuration/PreviousAtktNull");

                if (node != null)
                {
                    previousAtkt = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return previousAtkt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Principal Name from Configuration.xml file
        /// </summary>
        /// <returns>Principal Name as string</returns>
        public string GetPrincipalName(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                string principalName = string.Empty;
                XmlNode node = doc.SelectSingleNode("Configuration/PrincipalName");

                if (node != null)
                {
                    principalName = node.Attributes["name"].Value;
                }

                return principalName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Ledger Footer index value from configuration.xml file
        /// </summary>
        /// <returns>ledger footer number as integer</returns>
        public int GetLedgerFooter(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                int ledgerFooter = 0;
                XmlNode node = doc.SelectSingleNode("Configuration/LedgerFooter");

                if (node != null)
                {
                    ledgerFooter = Convert.ToInt32(node.Attributes["value"].Value);
                }

                return ledgerFooter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Load Staff Course flag from Configuration.xml file
        /// </summary>
        /// <returns>flag value as bool</returns>
        public bool IsLoadStaffCourse(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                bool loadStaffCourse = false;
                XmlNode node = doc.SelectSingleNode("Configuration/LoadStaffCourse");

                if (node != null)
                {
                    loadStaffCourse = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return loadStaffCourse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get passing marks from configuration.xml file
        /// </summary>
        /// <returns></returns>
        public bool IsPassing40(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                bool passing40 = false;
                XmlNode node = doc.SelectSingleNode("Configuration/Passing40");

                if (node != null)
                {
                    passing40 = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return passing40;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// get passing marks from configuration.xml file
        /// </summary>
        /// <returns></returns>
        public bool IsSortBySubCourse(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                bool isSortBySubcourse = false;
                XmlNode node = doc.SelectSingleNode("Configuration/IsLegderSortbySubCourse");

                if (node != null)
                {
                    isSortBySubcourse = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isSortBySubcourse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsShowSubcourseInMarksheet(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                bool isShowSubcourse = false;
                XmlNode node = doc.SelectSingleNode("Configuration/IsShowSubcourseInMarksheet");

                if (node != null)
                {
                    isShowSubcourse = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isShowSubcourse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetSubcourseSynonym(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                string synonym = string.Empty;
                XmlNode node = doc.SelectSingleNode("Configuration/Subcoursename");

                if (node != null)
                {
                    synonym = node.Attributes["value"].Value;
                }

                return synonym;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsShowFotterInMarksheet(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                bool isShowFotter = false;
                XmlNode node = doc.SelectSingleNode("Configuration/IsShowFotterInMarksheet");

                if (node != null)
                {
                    isShowFotter = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isShowFotter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public XmlConfigurations GetCommonConfigurations(string filePath)
        {
            try
            {
                XmlDocument doc = OpenFile(filePath);
                XmlConfigurations config = new XmlConfigurations();
                XmlNode common = doc.SelectSingleNode("Configuration/Print/Common");

                if (common != null)
                {
                    XmlNode header = common.SelectSingleNode("HeaderDetails");
                    XmlNode footer = common.SelectSingleNode("FooterDetails");
                    if (header != null)
                    {
                        config.Header = new ConfigurationHeaderDetails();
                        config.Header.IsRequired = bool.Parse(header.SelectSingleNode("Header").Attributes["required"].Value);
                        XmlNodeList collegeNames = header.SelectNodes("CollegeName/Line");
                        config.Header.CollegeName = new List<string>();
                        foreach (XmlNode college in collegeNames)
                        {
                            config.Header.CollegeName.Add(college.InnerText);
                        }

                        config.Header.LogoPath = header.SelectSingleNode("Logo").InnerText;
                        config.Header.LogoWidth = header.SelectSingleNode("LogoSize").Attributes["width"].Value;
                        config.Header.LogoHeight = header.SelectSingleNode("LogoSize").Attributes["height"].Value;

                        config.Header.TagLine = header.SelectSingleNode("TagLine").InnerText;
                        config.Header.SmallTagLine = header.SelectSingleNode("SmallTagLine").InnerText;

                        XmlNodeList address = header.SelectNodes("Address/Line");
                        config.Header.Address = new List<string>();
                        foreach (XmlNode addressLine in address)
                        {
                            config.Header.Address.Add(addressLine.InnerText);
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

                    return config;
                }
            }
            catch (Exception ex)
            {
                //logger.Error("ConfigurationXmlParser.cs", "GetCommonConfigurations", "Occured by " + ex.Message);
                throw ex;
            }

            return null;
        }

        public bool IsSubcoursePrintOnExamForm(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                bool isPrintSubcourse = true;
                XmlNode node = doc.SelectSingleNode("Configuration/IsSubcoursePrintOnExamForm");

                if (node != null)
                {
                    isPrintSubcourse = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isPrintSubcourse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // return true or false for displaying Semester dropdown or marksdetails page
        public bool IsDisplaySemesterDropDown(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);
                bool isDisplay = false;
                XmlNode node = doc.SelectSingleNode("Configuration/IsShowSemesterDropDown");

                if (node != null)
                {
                    isDisplay = Convert.ToBoolean(node.Attributes["value"].Value);
                }

                return isDisplay;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get Auto Send Students reports configuration and send
        /// </summary>
        /// <param name="configFilePath"></param>
        /// <returns>AutoSendConfig Object</returns>
        public AutoSendReportConfig GetAutoSendReportConfig(string configFilePath)
        {
            try
            {
                XmlDocument doc = OpenFile(configFilePath);                
                XmlNode autoSendReportNode = doc.SelectSingleNode("Configuration/AutoSendReports");

                if (autoSendReportNode != null)
                {
                    AutoSendReportConfig autoSendReportConfig = new AutoSendReportConfig();

                    autoSendReportConfig.IsSendChapterWiseProgressReport = Convert.ToBoolean(autoSendReportNode.SelectSingleNode("ChapterWiseProgress").Attributes["autosend"].Value);
                    autoSendReportConfig.IsSendCourseCompletedReport= Convert.ToBoolean(autoSendReportNode.SelectSingleNode("CourseCompleted").Attributes["autosend"].Value);
                    autoSendReportConfig.IsSendStepWiseProgressReport = Convert.ToBoolean(autoSendReportNode.SelectSingleNode("StepWiseProgress").Attributes["autosend"].Value);
                    autoSendReportConfig.IsSendTypingStatsReport = Convert.ToBoolean(autoSendReportNode.SelectSingleNode("TypingStats").Attributes["autosend"].Value);
                    autoSendReportConfig.IsSendNotStartedCourseReport = Convert.ToBoolean(autoSendReportNode.SelectSingleNode("NotStartedCourse").Attributes["autosend"].Value);
                    autoSendReportConfig.IsSendNotStartedTypingReport = Convert.ToBoolean(autoSendReportNode.SelectSingleNode("NotStartedTyping").Attributes["autosend"].Value);
                    autoSendReportConfig.IsSendCompletedCourseFinalQuizTypingReport = Convert.ToBoolean(autoSendReportNode.SelectSingleNode("FullCompleted").Attributes["autosend"].Value);
                    
                    return autoSendReportConfig;
                }
            }
            catch (Exception)
            {
                
            }

            return null;
        }

        public Dictionary<string, List<string>> GetAutoSendOverAllReportsEmailSettings(string configFilePath, out List<string> ccAddress)
        {
            ccAddress = new List<string>();

            try
            {
                XmlDocument doc = OpenFile(configFilePath); 
               
                XmlNode autoSendOverAllReportNode = doc.SelectSingleNode("Configuration/AutoSendOverAllReport");

                //get cc address
                XmlNodeList ccAddressNodes = autoSendOverAllReportNode.SelectNodes("ccaddress/add");

                foreach (XmlNode ccAddressNode in ccAddressNodes)
                {
                    if (!string.IsNullOrEmpty(ccAddressNode.Attributes["cc"].Value))
                    {
                        ccAddress.Add(ccAddressNode.Attributes["cc"].Value);
                    }
                }

                XmlNodeList toAddressNodes = autoSendOverAllReportNode.SelectNodes("toaddress/add");

                Dictionary<string, List<string>> dToAddress = new Dictionary<string, List<string>>();

                List<string> lToAddress = new List<string>();

                foreach (XmlNode toAddressNode in toAddressNodes)
                {
                    string course = toAddressNode.Attributes["course"].Value;
                    string toAddress = toAddressNode.Attributes["to"].Value;

                    if (!string.IsNullOrEmpty(toAddress) && !string.IsNullOrEmpty(course))
                    {
                        if (!dToAddress.Keys.Contains(course))
                        {
                            dToAddress.Add(course, new List<string>());

                            lToAddress = null;
                            lToAddress = new List<string>();
                        }

                        lToAddress.Add(toAddressNode.Attributes["to"].Value);

                        dToAddress[course] = lToAddress;
                    }
                }

                return dToAddress;
            }
            catch (Exception ex)
            {                
                
            }

            return null;
        }
    }

    public class XmlConfigurations
    {
        public ConfigurationHeaderDetails Header { get; set; }
        public ConfigurationFooterDetails Footer { get; set; }
    }

    public class ConfigurationHeaderDetails
    {
        public bool IsRequired { get; set; }
        public string LogoPath { get; set; }
        public string LogoWidth { get; set; }
        public string LogoHeight { get; set; }
        public List<string> CollegeName { get; set; }
        public List<string> Address { get; set; }
        public string TagLine { get; set; }
        public string SmallTagLine { get; set; }
    }

    public class ConfigurationFooterDetails
    {
        public string FooterLine { get; set; }
        public List<string> CollegeName { get; set; }
    }
}
