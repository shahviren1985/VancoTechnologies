-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.0.84-community-nt


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema college
--

CREATE DATABASE IF NOT EXISTS college;
USE college;

--
-- Definition of table `applicationlogoheader`
--

DROP TABLE IF EXISTS `applicationlogoheader`;
CREATE TABLE `applicationlogoheader` (
  `Id` int(10) NOT NULL auto_increment,
  `collegeName` varchar(50) default NULL,
  `logoimage` varchar(50) default NULL,
  `logoimagepath` varchar(500) default NULL,
  `logotext` varchar(500) default NULL,
  `DateCreated` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `applicationlogoheader`
--

/*!40000 ALTER TABLE `applicationlogoheader` DISABLE KEYS */;
INSERT INTO `applicationlogoheader` (`Id`,`collegeName`,`logoimage`,`logoimagepath`,`logotext`,`DateCreated`) VALUES 
 (1,'mnwc','logo-mnwc-20131108130127.gif','~/LogoImage/mnwc/logo-mnwc-20131108130127.gif','Maniben Nanavati Women&#39;s College','2013-11-08 12:53:58');
/*!40000 ALTER TABLE `applicationlogoheader` ENABLE KEYS */;


--
-- Definition of table `chapterdetails`
--

DROP TABLE IF EXISTS `chapterdetails`;
CREATE TABLE `chapterdetails` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `CourseId` int(10) unsigned NOT NULL,
  `Language` varchar(100) character set utf8 NOT NULL default 'en',
  `ContentVersion` varchar(3) character set utf8 NOT NULL default '1',
  `FileName` varchar(100) character set utf8 NOT NULL,
  `DateCreated` timestamp NOT NULL default CURRENT_TIMESTAMP,
  `ChapterName` varchar(255) character set utf8 NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_chapterdetails_1` (`CourseId`),
  CONSTRAINT `FK_chapterdetails_1` FOREIGN KEY (`CourseId`) REFERENCES `coursedetails` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `chapterdetails`
--

/*!40000 ALTER TABLE `chapterdetails` DISABLE KEYS */;
INSERT INTO `chapterdetails` (`Id`,`CourseId`,`Language`,`ContentVersion`,`FileName`,`DateCreated`,`ChapterName`) VALUES 
 (1,1,'En','1','DomyFile_0.html','2013-11-02 12:00:00','Computer Basics'),
 (2,1,'En','1','DomyFile_2.html','2013-11-15 12:00:00','Computer Hardware'),
 (3,1,'En','1','DomyFile_0.html','2013-11-15 12:00:00','Storage Devices'),
 (4,1,'En','1','DomyFile_1.html','2013-11-15 12:00:00','Operating System'),
 (5,1,'En','1','DomyFile_3.html','2013-11-15 12:00:00','File management'),
 (6,1,'En','1','DomyFile_0.html','2013-11-16 12:00:00','MS Paint'),
 (7,1,'En','1','DomyFile_2.html','2013-11-16 12:00:00','Notepad'),
 (8,1,'En','1','DomyFile_2.html','2013-11-16 12:00:00','Word'),
 (9,1,'En','1','DomyFile_2.html','2013-11-16 12:00:00','Excel'),
 (10,1,'En','1','DomyFile_2.html','2013-11-16 12:00:00','Power point'),
 (11,1,'En','1','DomyFile_2.html','2013-11-16 12:00:00','Computer Networks'),
 (12,1,'En','1','DomyFile_2.html','2013-11-16 12:00:00','Security'),
 (13,1,'En','1','DomyFile_2.html','2013-11-16 12:00:00','Internet'),
 (14,1,'En','1','DomyFile_3.html','2013-11-16 12:00:00','Social media');
/*!40000 ALTER TABLE `chapterdetails` ENABLE KEYS */;


--
-- Definition of table `chapterquizmaster`
--

DROP TABLE IF EXISTS `chapterquizmaster`;
CREATE TABLE `chapterquizmaster` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `courseId` int(10) unsigned NOT NULL,
  `chapterId` int(10) unsigned NOT NULL,
  `sectionId` int(10) unsigned NOT NULL,
  `questionText` text NOT NULL,
  `IsQuestionOptionPresent` bit(1) NOT NULL default b'0',
  `QuestionOption` text,
  `AnswerOption` text NOT NULL,
  `ContentVersion` varchar(3) character set utf8 default '1',
  `DateCreated` timestamp NOT NULL default CURRENT_TIMESTAMP,
  `createdby` varchar(50) character set utf8 NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_chapterquizmaster_1` (`courseId`),
  KEY `FK_chapterquizmaster_2` (`chapterId`),
  CONSTRAINT `FK_chapterquizmaster_1` FOREIGN KEY (`courseId`) REFERENCES `coursedetails` (`Id`),
  CONSTRAINT `FK_chapterquizmaster_2` FOREIGN KEY (`chapterId`) REFERENCES `chapterdetails` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=127 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `chapterquizmaster`
--

/*!40000 ALTER TABLE `chapterquizmaster` DISABLE KEYS */;
INSERT INTO `chapterquizmaster` (`Id`,`courseId`,`chapterId`,`sectionId`,`questionText`,`IsQuestionOptionPresent`,`QuestionOption`,`AnswerOption`,`ContentVersion`,`DateCreated`,`createdby`) VALUES 
 (1,1,1,1,'Computer components are divided into which two major categories?',0x00,'[]','[{\"AnswerOption\":\"Input and output\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware and Software\",\"IsCurrect\":true},{\"AnswerOption\":\"Processing and Storage\",\"IsCurrect\":false},{\"AnswerOption\":\"Control Unit and Arithmetic Logic Unit\",\"IsCurrect\":false}]','1','2013-11-16 06:34:49','admin'),
 (2,1,1,2,'Which of the following describes Computer as a device which?',0x00,'[]','[{\"AnswerOption\":\"It takes input from the user, processes this data and produces the output\",\"IsCurrect\":false},{\"AnswerOption\":\"It could process both numerical as well as non-numerical calculations.\",\"IsCurrect\":false},{\"AnswerOption\":\"It controls all operations inside a computer\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-16 06:40:29','admin'),
 (3,1,1,2,'Which of the following are Computer functions?',0x00,'[]','[{\"AnswerOption\":\"Processing\",\"IsCurrect\":false},{\"AnswerOption\":\"Storage\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-16 06:42:25','admin'),
 (4,1,1,2,'Which of the following are basic Computer components :?',0x00,'[]','[{\"AnswerOption\":\"Control Unit (CU)\",\"IsCurrect\":false},{\"AnswerOption\":\"Arithmetic Logic Unit (ALU)\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-16 07:00:20','admin'),
 (5,1,1,2,'The process of input, output, processing and storage is performed under the supervision unit ?',0x00,'[]','[{\"AnswerOption\":\"Memory Unit\",\"IsCurrect\":false},{\"AnswerOption\":\"Input\",\"IsCurrect\":false},{\"AnswerOption\":\"Control Unit (CU)\",\"IsCurrect\":true},{\"AnswerOption\":\"Output\",\"IsCurrect\":false}]','1','2013-11-16 07:03:13','admin'),
 (6,1,1,2,'Which component perform are addition, subtraction, multiplication, division, logic and comparisons.?',0x00,'[]','[{\"AnswerOption\":\"Output\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware\",\"IsCurrect\":false},{\"AnswerOption\":\"Arithmetic Logic Unit (ALU)\",\"IsCurrect\":true},{\"AnswerOption\":\"Control Unit(CU)\",\"IsCurrect\":false}]','1','2013-11-16 07:05:49','admin'),
 (7,1,1,2,'Which of the computer system are jointly known as the central processing unit (CPU) ?',0x00,'[]','[{\"AnswerOption\":\"Input and Output.\",\"IsCurrect\":false},{\"AnswerOption\":\"Arithmetic Logic Unit and Control Unit\",\"IsCurrect\":true},{\"AnswerOption\":\"Hardware and Software\",\"IsCurrect\":false},{\"AnswerOption\":\"All of above\",\"IsCurrect\":false}]','1','2013-11-16 07:07:56','admin'),
 (8,1,1,2,'Which of the following are the major characteristics of a computer ?',0x00,'[]','[{\"AnswerOption\":\"high speed\",\"IsCurrect\":false},{\"AnswerOption\":\"accuracy\",\"IsCurrect\":false},{\"AnswerOption\":\"diligence\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-16 07:09:14','admin'),
 (9,1,1,2,'Which of the Computer Components takes care of step -by-step processing of all operations inside the computer ?',0x00,'[]','[{\"AnswerOption\":\"Memory Unit\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware\",\"IsCurrect\":false},{\"AnswerOption\":\"Control Unit(CU)\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-16 07:10:54','admin'),
 (10,1,1,3,'Which of the following refers to the physical parts or components of a computer :?',0x00,'[]','[{\"AnswerOption\":\"Monitor\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware\",\"IsCurrect\":true},{\"AnswerOption\":\"Keyboard\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-16 07:14:54','admin'),
 (11,1,1,4,'Which of the following refers to sets of programs, responsible for running the computer, controlling various operations of computer systems and management of computer resources.?',0x00,'[]','[{\"AnswerOption\":\"Control Unit(CU)\",\"IsCurrect\":false},{\"AnswerOption\":\"System Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Utility Programs\",\"IsCurrect\":true},{\"AnswerOption\":\"Operating System\",\"IsCurrect\":false}]','1','2013-11-16 07:17:27','admin'),
 (12,1,1,5,'Which of the following statements describes an operating system?',0x00,'[]','[{\"AnswerOption\":\"The process of input, output, processing and storage is performed under the supervision of a unit.\",\"IsCurrect\":false},{\"AnswerOption\":\"The system software that provides an interface for a user to communicate with the computer.\",\"IsCurrect\":false},{\"AnswerOption\":\"It manages hardware devices and supports application programs.\",\"IsCurrect\":false},{\"AnswerOption\":\"Both B and C\",\"IsCurrect\":true}]','1','2013-11-16 07:21:22','admin'),
 (13,1,1,5,'Which of the following programs bridge the gap between the functionality of operating systems.?',0x00,'[]','[{\"AnswerOption\":\"Operating System\",\"IsCurrect\":false},{\"AnswerOption\":\"Utility\",\"IsCurrect\":true},{\"AnswerOption\":\"System Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Application Software\",\"IsCurrect\":false}]','1','2013-11-16 07:22:52','admin'),
 (14,1,1,6,'Application software can be broadly classified into which two types ?',0x00,'[]','[{\"AnswerOption\":\"Hardware and Software \",\"IsCurrect\":false},{\"AnswerOption\":\"Generalized packages and Customized packages\",\"IsCurrect\":true},{\"AnswerOption\":\"Input and Output\",\"IsCurrect\":false},{\"AnswerOption\":\"d)	SystemSoftware and Application Software\",\"IsCurrect\":false}]','1','2013-11-16 07:24:44','admin'),
 (15,1,1,7,'Which of the following is used for Data Analysis?',0x00,'[]','[{\"AnswerOption\":\"Lotus Smart suites\",\"IsCurrect\":false},{\"AnswerOption\":\"Word Perfect\",\"IsCurrect\":false},{\"AnswerOption\":\"Apple Numbers\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and C\",\"IsCurrect\":true}]','1','2013-11-16 07:28:12','admin'),
 (16,1,1,7,'Which of the generalized software packages are listed below.?',0x00,'[]','[{\"AnswerOption\":\"Word Processing Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Database Management System\",\"IsCurrect\":false},{\"AnswerOption\":\"Spreadsheets\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-16 07:29:59','admin'),
 (17,1,1,7,'Which of the below Some of the are Popular Operating Systems ?',0x00,'[]','[{\"AnswerOption\":\"Spreadsheets\",\"IsCurrect\":false},{\"AnswerOption\":\"MS-Access\",\"IsCurrect\":false},{\"AnswerOption\":\"Central Processing Unit (CPU)\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":true}]','1','2013-11-16 07:32:55','admin'),
 (18,1,1,8,'Which of the applications that are customized (or developed) to meet the specific requirements of an organization/institution.?',0x00,'[]','[{\"AnswerOption\":\"generalized packages\",\"IsCurrect\":false},{\"AnswerOption\":\"Utility programs\",\"IsCurrect\":false},{\"AnswerOption\":\"Application Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Customized packages\",\"IsCurrect\":true}]','1','2013-11-16 07:35:55','admin'),
 (19,1,1,8,'How can you categorize Utility programs? ',0x00,'[]','[{\"AnswerOption\":\"compress (zip)/uncompress (unzip) files software\",\"IsCurrect\":false},{\"AnswerOption\":\"anti-virus software\",\"IsCurrect\":false},{\"AnswerOption\":\"split and join files software\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-16 07:37:42','admin'),
 (20,1,1,3,'Which of the following are the physical parts of the computer?  ',0x00,'[]','[{\"AnswerOption\":\"Monitor\",\"IsCurrect\":false},{\"AnswerOption\":\"Central Unit(CU)\",\"IsCurrect\":false},{\"AnswerOption\":\"Central Processing Unit(CPU)\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and C\",\"IsCurrect\":true}]','1','2013-11-16 07:39:31','admin'),
 (21,1,2,9,'Which of the following defines the collection of physical devices that constitutes a computer system?.',0x00,'[]','[{\"AnswerOption\":\"Computer Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Computer Hardware\",\"IsCurrect\":true},{\"AnswerOption\":\"Input Device\",\"IsCurrect\":false},{\"AnswerOption\":\"Output Device\",\"IsCurrect\":false}]','1','2013-11-16 07:44:28','admin'),
 (22,1,2,9,'What are of the types of Hardware devices? \n',0x00,'[]','[{\"AnswerOption\":\"Input Devices\",\"IsCurrect\":false},{\"AnswerOption\":\"Output Devices\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-16 07:46:15','admin'),
 (23,1,2,10,'Which of the following are the examples of heavily used input devices on daily basis :?\n',0x00,'[]','[{\"AnswerOption\":\"Microphone (Mic.) for voice as input\",\"IsCurrect\":false},{\"AnswerOption\":\"Optical/magnetic Scanner\",\"IsCurrect\":false},{\"AnswerOption\":\"Touch Screen\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-16 07:48:03','admin'),
 (24,1,2,11,'Which of the following Key board have alphabets A to Z ?',0x00,'[]','[{\"AnswerOption\":\"Function Keys\",\"IsCurrect\":false},{\"AnswerOption\":\"Alphanumeric characters\",\"IsCurrect\":true},{\"AnswerOption\":\"Special characters\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-16 07:49:44','admin'),
 (25,1,2,11,'Which of the following Special characters are present on numeric keys ? \n',0x00,'[]','[{\"AnswerOption\":\"comma(,)\",\"IsCurrect\":false},{\"AnswerOption\":\"semi colon(,)(;)\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":true}]','1','2013-11-16 07:52:16','admin'),
 (26,1,2,11,'Which of the following options does the user have when requires upper case letters :?',0x00,'[]','[{\"AnswerOption\":\"Press CAPS Lock key\",\"IsCurrect\":false},{\"AnswerOption\":\"Press Shift + alphabet key\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-16 07:55:50','admin'),
 (27,1,2,12,'Which of the following can perform functions such as selecting menu commands, moving icons, resize windows, starting programs, and choosing options?.\n\n',0x00,'[]','[{\"AnswerOption\":\"Keyboard\",\"IsCurrect\":false},{\"AnswerOption\":\"Mouse\",\"IsCurrect\":true},{\"AnswerOption\":\"Touch Screen\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-16 07:59:56','admin'),
 (28,1,2,12,'Which of the following has are the three basic functions of a Mouse ?\n\n',0x00,'[]','[{\"AnswerOption\":\"right click\",\"IsCurrect\":false},{\"AnswerOption\":\"left click\",\"IsCurrect\":false},{\"AnswerOption\":\"scroll\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-16 08:03:31','admin'),
 (29,1,2,12,'When does computer show context menu ?\n',0x00,'[]','[{\"AnswerOption\":\"Whenever user right clicks on screen\",\"IsCurrect\":true},{\"AnswerOption\":\"whenever user left clicks on screen\",\"IsCurrect\":false},{\"AnswerOption\":\"whenever user scroll up or scroll down\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-16 08:06:01','admin'),
 (30,1,2,12,'Which action could can perform additional operations on an item after it is highlighted ?\n',0x00,'[]','[{\"AnswerOption\":\"On Right click\",\"IsCurrect\":false},{\"AnswerOption\":\"On Left click\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-16 08:07:48','admin'),
 (31,1,2,13,'Which of the following are kind of devices to completely eliminate manual input of data.?',0x00,'[]','[{\"AnswerOption\":\"Input devices\",\"IsCurrect\":false},{\"AnswerOption\":\"output devices\",\"IsCurrect\":false},{\"AnswerOption\":\"Image scanner\",\"IsCurrect\":true},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-16 08:10:02','admin'),
 (32,1,2,13,'Which of the following translates printed images into an electronic format that can be stored in a computer&#39;s memory?.\n',0x00,'[]','[{\"AnswerOption\":\"Bar code reader\",\"IsCurrect\":false},{\"AnswerOption\":\"Optical character recognition (OCR)\",\"IsCurrect\":false},{\"AnswerOption\":\"Image scanner\",\"IsCurrect\":true},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-16 08:25:27','admin'),
 (33,1,2,13,'Which of the following used by banks to convert the scanned image of a typed or printed page into text that can be edited on the computer ?\n',0x00,'[]','[{\"AnswerOption\":\"Bar code reader\",\"IsCurrect\":false},{\"AnswerOption\":\"Optical character recognition (OCR)\",\"IsCurrect\":true},{\"AnswerOption\":\"Image scanner\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-16 08:28:46','admin'),
 (34,1,2,15,'In which of the following applications Microphone is used : ?\r\n',0x00,'[]','[{\"AnswerOption\":\"sound recording\",\"IsCurrect\":false},{\"AnswerOption\":\"video/audio chat applications\",\"IsCurrect\":false},{\"AnswerOption\":\"Either A or B\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false}]','1','2013-11-16 08:31:25','admin'),
 (35,1,2,15,'Which of the following types of Microphones are available :?\n\n',0x00,'[]','[{\"AnswerOption\":\"Desktop Microphone\",\"IsCurrect\":false},{\"AnswerOption\":\"Hand held Microphone\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-16 08:33:55','admin'),
 (36,1,2,16,'Which of the following  input device captures live video stream and sends it over computer network.?\n',0x00,'[]','[{\"AnswerOption\":\"Touch Screen\",\"IsCurrect\":false},{\"AnswerOption\":\"Optical/magnetic Scanner\",\"IsCurrect\":false},{\"AnswerOption\":\"Web Camera\",\"IsCurrect\":true},{\"AnswerOption\":\"Microphone\",\"IsCurrect\":false}]','1','2013-11-16 08:37:21','admin'),
 (37,1,2,17,'Which of the following are commonly used output devices ?\n',0x00,'[]','[{\"AnswerOption\":\"Monitor/Display unit\",\"IsCurrect\":false},{\"AnswerOption\":\"Speakers\",\"IsCurrect\":false},{\"AnswerOption\":\"Projector\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-16 08:38:59','admin'),
 (38,1,2,19,'Which of the following uses light-emitting diodes as a video display.?\n',0x00,'[]','[{\"AnswerOption\":\"Cathode Ray Tube (CRT)\",\"IsCurrect\":false},{\"AnswerOption\":\"Liquid Crystal Displays (LCD)\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"LED\",\"IsCurrect\":true}]','1','2013-11-16 08:41:35','admin'),
 (39,1,2,19,'Which of these monitors are considered outdated ?\n',0x00,'[]','[{\"AnswerOption\":\"LED\",\"IsCurrect\":false},{\"AnswerOption\":\"Liquid Crystal Displays (LCD)\",\"IsCurrect\":false},{\"AnswerOption\":\"Cathode Ray Tube (CRT)\",\"IsCurrect\":true},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-16 08:44:06','admin'),
 (40,1,2,19,'High-end monitors can have which of the following  resolutions ?\n\n',0x00,'[]','[{\"AnswerOption\":\"320 x 480\",\"IsCurrect\":false},{\"AnswerOption\":\"800 x 600\",\"IsCurrect\":false},{\"AnswerOption\":\"1600 x 900\",\"IsCurrect\":true},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-16 08:46:43','admin'),
 (41,1,2,20,'Which of the printer can print up to 200 pages per minute in black and white and up to 100 pages per minute in color ?',0x00,'[]','[{\"AnswerOption\":\"Laser Printer\",\"IsCurrect\":true},{\"AnswerOption\":\"Ink Jet Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"Dot Matrix Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-16 08:48:48','admin'),
 (42,1,2,20,'Which of the printer creates an image directly on paper by spraying ink through as many as 64 tiny nozzles ?\n',0x00,'[]','[{\"AnswerOption\":\"Laser Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"Dot Matrix Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"Ink Jet Printer\",\"IsCurrect\":true},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-16 08:50:02','admin'),
 (43,1,2,22,'Which of the following are optical output device that projects an image or video on a surface ?\n',0x00,'[]','[{\"AnswerOption\":\"Input\",\"IsCurrect\":false},{\"AnswerOption\":\"Output\",\"IsCurrect\":false},{\"AnswerOption\":\"Projector\",\"IsCurrect\":true},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-16 08:52:17','admin'),
 (44,1,3,27,'Which of the following is a Temporary memory ?\n',0x00,'[]','[{\"AnswerOption\":\"Secondary Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Primary Memory\",\"IsCurrect\":true},{\"AnswerOption\":\"Real time memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Flash memory\",\"IsCurrect\":false}]','1','2013-11-16 08:57:05','admin'),
 (45,1,3,26,'What is RAM ?\n',0x00,'[]','[{\"AnswerOption\":\"Random Access Memory\",\"IsCurrect\":true},{\"AnswerOption\":\"Recurring Access Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Flash Access Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-16 08:59:18','admin'),
 (46,1,3,27,'What component of a computer is referred to as Main board ?\n',0x00,'[]','[{\"AnswerOption\":\"Electric board \",\"IsCurrect\":false},{\"AnswerOption\":\"Black board\",\"IsCurrect\":false},{\"AnswerOption\":\"Mother board\",\"IsCurrect\":true},{\"AnswerOption\":\"White board\",\"IsCurrect\":false}]','1','2013-11-16 09:02:35','admin'),
 (47,1,3,25,'Which Memory is known as Volatile memory?\n\n',0x00,'[]','[{\"AnswerOption\":\"Secondary Storage\",\"IsCurrect\":false},{\"AnswerOption\":\"Hybrid Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Real time memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Primary Storage\",\"IsCurrect\":true}]','1','2013-11-16 09:04:00','admin'),
 (48,1,3,30,'Which of the below is secondary memory ?\n',0x00,'[]','[{\"AnswerOption\":\"Random Access Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Hybrid Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Compact Disk\",\"IsCurrect\":true},{\"AnswerOption\":\"Real time Memory\",\"IsCurrect\":false}]','1','2013-11-16 09:05:28','admin'),
 (49,1,3,32,'What is the storage capacity of a normal DVD ?\n',0x00,'[]','[{\"AnswerOption\":\"700 MB\",\"IsCurrect\":false},{\"AnswerOption\":\"4.7 GB\",\"IsCurrect\":true},{\"AnswerOption\":\"4 GB\",\"IsCurrect\":false},{\"AnswerOption\":\"16 GB\",\"IsCurrect\":false}]','1','2013-11-16 09:08:09','admin'),
 (50,1,3,32,'What is the storage capacity of a normal Compact Disk ?',0x00,'[]','[{\"AnswerOption\":\"4.7 GB\",\"IsCurrect\":false},{\"AnswerOption\":\"4 GB\",\"IsCurrect\":false},{\"AnswerOption\":\"700 MB\",\"IsCurrect\":true},{\"AnswerOption\":\"16 GB\",\"IsCurrect\":false}]','1','2013-11-16 09:09:31','admin'),
 (51,1,3,33,'USB is which type of memory?\n\n',0x00,'[]','[{\"AnswerOption\":\"Primary Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Flash Memory\",\"IsCurrect\":true},{\"AnswerOption\":\"Volatile Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Hybrid Memory\",\"IsCurrect\":false}]','1','2013-11-16 09:12:43','admin'),
 (52,1,4,33,'Which is a following software program that enables the computer hardware to communicate and operate with the computer software ?',0x00,'[]','[{\"AnswerOption\":\"Computer\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware\",\"IsCurrect\":false},{\"AnswerOption\":\"Operating System\",\"IsCurrect\":true},{\"AnswerOption\":\"Windows XP\",\"IsCurrect\":false}]','1','2013-11-16 09:29:44','admin'),
 (53,1,4,34,'Which of the following are not functional without an operating system ?\n',0x00,'[]','[{\"AnswerOption\":\"Input and Output\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware and Software\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-16 09:32:10','admin'),
 (54,1,4,34,'Which of the following are examples of operating system ?\n',0x00,'[]','[{\"AnswerOption\":\"Apple Mac OS\",\"IsCurrect\":false},{\"AnswerOption\":\"Google Android\",\"IsCurrect\":false},{\"AnswerOption\":\"Ubuntu Linux\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-16 09:34:14','admin'),
 (55,1,4,35,'Which of the following activities to Control access to shared resources like file, memory, I/O [Input/Output] and CPU ?\n',0x00,'[]','[{\"AnswerOption\":\"Memory management\",\"IsCurrect\":false},{\"AnswerOption\":\"Process management\",\"IsCurrect\":true},{\"AnswerOption\":\"Device management\",\"IsCurrect\":false},{\"AnswerOption\":\"File management\",\"IsCurrect\":false}]','1','2013-11-16 09:36:17','admin'),
 (56,1,4,35,'Which of the following system used to manage memory in a computer ? \n',0x00,'[]','[{\"AnswerOption\":\"Memory management\",\"IsCurrect\":true},{\"AnswerOption\":\"Process management\",\"IsCurrect\":false},{\"AnswerOption\":\"Device management\",\"IsCurrect\":false},{\"AnswerOption\":\"File management\",\"IsCurrect\":false}]','1','2013-11-16 09:39:22','admin'),
 (57,1,4,36,'Which of the following operating system vendors before MS-Windows?\n\n',0x00,'[]','[{\"AnswerOption\":\"Xenix\",\"IsCurrect\":false},{\"AnswerOption\":\"MS-DOS\",\"IsCurrect\":false},{\"AnswerOption\":\"MS-DOS OPSYST\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-16 09:42:04','admin'),
 (58,1,4,39,'Which of the following different available options on Windows start menu?\n\n',0x00,'[]','[{\"AnswerOption\":\"My Documents\",\"IsCurrect\":false},{\"AnswerOption\":\"My Recent Documents\",\"IsCurrect\":false},{\"AnswerOption\":\"My Music\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-16 09:43:14','admin'),
 (59,1,4,39,'Which of the following displays a list of utility to configure the computer system and install software and hardware ?\n',0x00,'[]','[{\"AnswerOption\":\"My Documents\",\"IsCurrect\":false},{\"AnswerOption\":\"My Recent Documents\",\"IsCurrect\":false},{\"AnswerOption\":\"My Music\",\"IsCurrect\":false},{\"AnswerOption\":\"Control panel\",\"IsCurrect\":true}]','1','2013-11-16 09:44:45','admin'),
 (60,1,4,45,'Windows look &amp; feel can be customized using ?\n',0x00,'[]','[{\"AnswerOption\":\"My Documents\",\"IsCurrect\":false},{\"AnswerOption\":\"My Recent Documents\",\"IsCurrect\":false},{\"AnswerOption\":\"My Music\",\"IsCurrect\":false},{\"AnswerOption\":\"Control panel\",\"IsCurrect\":true}]','1','2013-11-16 09:47:31','admin'),
 (61,1,4,46,'Which of the following displays the contents of hard disk, CD-ROM and network drives ?\n',0x00,'[]','[{\"AnswerOption\":\"Administrative Tools\",\"IsCurrect\":false},{\"AnswerOption\":\"My Computer\",\"IsCurrect\":true},{\"AnswerOption\":\"Control panel\",\"IsCurrect\":false},{\"AnswerOption\":\"My Document\",\"IsCurrect\":false}]','1','2013-11-16 09:49:33','admin'),
 (62,1,4,35,'Which are the main functions of the operating system ?\n',0x00,'[]','[{\"AnswerOption\":\"Memory management\",\"IsCurrect\":false},{\"AnswerOption\":\"Process management\",\"IsCurrect\":false},{\"AnswerOption\":\"Device management\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-16 09:50:55','admin'),
 (63,1,2,14,'User provide input to touch screen using?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Electronic buttons &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Light pen&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of these&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 17:06:58','admin'),
 (64,1,2,18,'What are the different types of monitors ?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Cathode Ray Tube (CRT)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Light Emitting Diode(LED)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All of the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 17:20:51','admin'),
 (65,1,2,21,'Speakers are use as?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Output device&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Input Device&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 17:24:37','admin'),
 (66,1,3,24,'Storage space is normally expressed in ?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Kilobyte (KB) &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Megabyte (MB)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Gigabyte (GB)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All of the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 17:32:40','admin'),
 (67,1,3,28,'Read Only Memory (ROM) is of which type memory?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Primary storage memory.&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Main Memory&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Non-volite memory&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All of the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 17:36:58','admin'),
 (68,1,3,29,'Secondary storage devices are classified as ?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Pen drives&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;ROM&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 17:42:51','admin'),
 (71,1,3,31,'CD stands for ?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Compact Disk&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Digital Versatile Disc&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 18:00:10','admin'),
 (72,1,3,31,'DVD stands for ?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Compact Disk&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Digital Versatile Disc&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 18:01:05','admin'),
 (73,1,4,37,'Is it possible to add new option to start menu?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;True&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;False&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 18:07:22','admin'),
 (74,1,4,38,'Which of the following option is not present in start menu?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;All Programs&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Control Panel&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Log Off &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 18:11:21','admin'),
 (75,1,4,40,'Is following steps are correct to start a program :\n1) Click on the All Programs\n2) start button\n3) Point to the desired folder say \"Accessories\" and select the desired program to run such as \"MS Paint\"',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;True&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;False&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 18:19:26','admin'),
 (77,1,4,41,'To quit a program, click on?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Close button&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;File menu and click on Close option&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 18:26:29','admin'),
 (78,1,4,42,'how user is able to get help on particular option ? ',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Click on a topic &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Click task to know more about.&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot; Type in a search &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All of the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 18:32:24','admin'),
 (79,1,4,43,'search option is use to search?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Files&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Folders&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 18:59:19','admin'),
 (80,1,4,44,'Windows XP&#39;s look &amp; feel could be customized from?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Control Panel&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;My Documents&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;My Computer&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 19:06:08','admin'),
 (82,1,4,47,'How to lock the windows ?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;press ctrl and alt and del&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;press alt and ctrl and del&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 19:13:33','admin'),
 (83,1,6,63,'Which of the following examples of application software.?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Accounting software&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Office  suites&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Media players &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 19:31:16','admin'),
 (84,1,6,63,'Which of the following used like a digital sketchpad to make simple pictures or add text and designs to other pictures, such as those taken with your digital camera :',0x00,'[]','[{&quot;AnswerOption&quot;:&quot; Toolbar&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Rectangle Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Paint&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 19:32:25','admin'),
 (85,1,6,63,'By default, which mode is active on Toolbar : ',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Airbrush mode&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Text mode&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Select mode&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Pencil mode&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 19:33:20','admin'),
 (86,1,6,64,'Which of the following icon on Toolbars : ',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Magnifier&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Menu Items&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Curve&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and C&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 19:34:32','admin'),
 (87,1,6,64,'Which of the following is present at bottom of paint program : ',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;The Curve Line Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Rectangle Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Color Palette&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;The Select Tool&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 19:36:02','admin'),
 (88,1,6,65,'In Color Palette , Left click on mouse to choose the?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Background color&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Foreground color&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of these&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 19:37:43','admin'),
 (89,1,6,65,'Which button Tool is used to draw multi-sided shapes :',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;The Rectangle Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Polygon Tool&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;The Free-Form Select Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 19:39:30','admin'),
 (90,1,6,66,'Which of the following to draw a perfect circle :',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Drag the mouse pointer and click at each corner&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Press and hold down Shift while dragging the mouse pointer&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Drag the cursor diagonally across the area.&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 19:48:30','admin'),
 (91,1,6,66,'Which of the following to draw the polygon',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Drag the mouse pointer and click at each corner and Double-click when done.&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Press and hold down Shift while dragging the mouse pointer&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Drag the cursor diagonally across the area.&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 19:50:03','admin'),
 (92,1,6,66,'Which button Tool is Used to select a color you have previously used in your drawing.',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;The Free-Form Select Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Magnifier Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Pick Color Tool&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;The Text Tool&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 19:51:28','admin'),
 (93,1,6,66,'Which button Tool is used to enlarge any part of a picture. ',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;The Free-Form Select Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Paint Brush Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Magnifier Tool&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;The Opaque Option Tool&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 19:52:39','admin'),
 (94,1,6,66,'Which button Tool is used to specify that the selection will cover the existing picture, using the foreground and background colors of the selected areas.',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;The Free-Form Select Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Paint Brush Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Magnifier Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Opaque Option Tool&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 19:54:01','admin'),
 (95,1,6,66,'Which button Tool is used to specify that the existing picture will show through your selection.',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;The Rectangle Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Transparent Option Tool&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;The Free-Form Select Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 20:02:28','admin'),
 (96,1,6,66,'Which of the Formats Image could be saved?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Bitmap (bmp)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;JPEG (JPG)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Portable Network Graphics (PNG)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 20:03:49','admin'),
 (97,1,5,48,'Which of the following displays the hierarchical list of files, folders, and storage drives on your computer?\n',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;File Management&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Windows Explorer&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 20:06:32','admin'),
 (98,1,5,54,'Where does windows operating system place the files or folders that are deleted from hard disk?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Recycle Bin&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Memory&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Desktop&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 20:08:14','admin'),
 (99,1,5,54,'Files or folders deleted from a which media are permanently deleted and not sent to the Recycle Bin?\n',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Removable storage&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Pen drive&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 20:09:49','admin'),
 (100,1,5,49,'Which of the following sequences are the set of correct steps to open Windows Explorer\n',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;1)  Click on Windows start button,2)  Point to All Programs,3)  Point to Accessories, and then click on Windows Explorer&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;1)  Click on Windows start button,2)  Click on Control panel,3)  Click on Programs&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;1)  Click on Windows start button,2)  Click on Devices and Printers ,3)  Click on Programs&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 20:18:01','admin'),
 (101,1,5,51,'Which of the following steps would you take to View file details :?\n',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;1)  Click on Windows start button,2)  Point to All Programs,3)  Point to Accessories, and then click on Windows Explorer&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;1) Click on Windows start button, and then click on My Documents,2)  Double-click the folder that contains the files to be viewed,3)  On the View menu, click Details&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;1)  Click on Windows start button, and then click on My Documents,2)  Click on Devices and Printers ,3)  and then click on Programs&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 20:23:52','admin'),
 (102,1,5,52,'Which of the following steps would you take to create a New Folder : ?\n',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;1)  Click on Windows start button,2)  Point to All Programs,3)  Point to Accessories, and then click on Windows Explorer&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;1) Click on Windows start button, and then click on My Documents,2)  Under File, click New and select Folder,3)  A new folder is displayed with the default name as New Folder&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;1)  Right-clicking a blank area in a Window or on the desktop,2)  Pointing to New,3)  and then Clicking Folder&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both B and C&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 20:28:39','admin'),
 (103,1,5,53,'Which of the following steps would you take to Rename a file or folder :? ',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;1)  Click on Windows start button, and then click on My Documents,2) Click on the file or folder  you want to rename,3)  Under file, click Rename, Type a new name and then press ENTER Key&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;1) Click on Windows start button, and then click on My Documents,2)  Under File, click New and select Folder,3)  A new folder is displayed with the default name as New Folder&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;1)  Right-clicking on the file or folder  you want to rename,2)  and then Clicking Rename&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and C&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 20:42:46','admin'),
 (104,1,5,54,'Which of the following steps would you take to Delete a file or folder : ?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;1)  Click on Windows start button, and then click on My Documents,2) Click on the file or folder you want to delete,3)  Under file, click Delete&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;1) Right-clicking on the file or folder  you want to delete,2)  and then Clicking Delete&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 20:46:38','admin'),
 (105,1,5,54,'Which of the following steps would you take to retrieve a deleted file?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Point to All Programs option from Widows Start button&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;1) Double-click the Recycle bin icon on desktop,2) Right-click on the file to retrieved,3) Clicks on Restore option&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;1) Double-click the Recycle bin icon on desktop,2) Right-click on the file to retrieved,3) Clicks on Properties option&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and C&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 20:50:27','admin'),
 (106,1,5,56,'Which of the following helps us to manage programs and components on the computer system?.',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Install Hardware&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Change or remove software&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Add or remove programs&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Install Software&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 20:52:26','admin'),
 (107,1,5,56,'Which of the following steps would you take to open add/remove dialog box ?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;1)  Click on Windows start button, and then click on Run option, 2) Type appwiz.cpl in run dialog box,3)  Press enter key or click on OK button&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;1) Click on Windows start button, and then click on Run option,2) Type appwiz.bmp in run dialog box ,3)  Press enter key or click on OK button&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 20:56:20','admin'),
 (108,1,5,59,'Which of the following steps would you take to add new features from Windows update?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;1) Click on Windows start button, and then click on Control Panel option, 2) Double-click on Add or Remove Programs,3) Click on Windows Update,4) Follow the instruction to locate and install new window features or updates&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;1) Click on Windows start button, and then click on Run option,2) Type appwiz.bmp in run dialog box,3)  Press enter key or click on OK button&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 21:00:32','admin'),
 (109,1,5,62,'Which of the following indicates that device driver is not installed :?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Green question mark&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Yellow question mark&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Red question mark&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 21:02:15','admin'),
 (110,1,7,69,'Which of the following examples of application software?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Office suites &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Graphics software&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Media players&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 21:06:14','admin'),
 (111,1,7,70,'Which is a handy program that a user can type in text quickly and easily.?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Printer&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Notepad&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;WordPad&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 21:09:25','admin'),
 (112,1,7,72,'Which of the following steps to Print a Document : ?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;1) Click on print menu option.  File &gt; Save... ,2) The three dots after the word Save means there will be another screen that pops up.This is a Windows standard,3) Select the printer you wish to print to then click the Save button.&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;1) Click on print menu option.  File &gt; Print...,2) The three dots after the word Print means there will be another screen that pops up.This is a Windows standard,       3) Select the printer you wish to print to then click the Print button.&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;1) Click on print menu option.  File &gt; Open...,2) The three dots after the word Open means there will be another screen that pops up. This is a Windows standard,3) Select the printer you wish to print to then click the Open button&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None &quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 21:14:49','admin'),
 (113,1,8,76,'Which of the following is an application program that allows you to create letters, reports, newsletters, tables, form letters, brochures, and Web pages?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Microsoft Excel&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Microsoft PowerPoint&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Word processing&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 21:24:15','admin'),
 (114,1,8,78,'MS Word 2007 have new user interface with new component called as ?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Tab&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Ribbon&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of these&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 21:25:43','admin'),
 (115,1,8,78,'Which of the following tab have most frequently used tools such as text formatting, text styling etc. ',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Insert&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Home&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Page Layout&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;View&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 21:26:58','admin'),
 (116,1,8,78,'Which of the following is Correct about WORD 2007?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Charts &amp; diagrams could be created in Word 2007 using 3D shapes, transparency, drop shadows and other effects.&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Document authenticity, integrity and origin can be ensured using digital signature&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Word 2007 document can be exported to PDF file format&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 21:30:38','admin'),
 (117,1,8,84,'Which of the following options to scroll the page ?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Click on arrow button present on the scroll bar (up arrow button or down arrow button)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Scroll up/down using scroll button present on mouse&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of these&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 21:32:15','admin'),
 (118,1,8,96,'Which of the following keyboard shortcuts are helpful when moving through the text of a document: ',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;HOME&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;CTRL PLUS HOME&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;CTRL PLUS END&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 21:34:31','admin'),
 (119,1,8,101,'Which of the following easiest way to change the look &amp; feel is using format toolbar?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Inserting Text&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Replacing Text&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Formatting Text&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Deleting Text&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 21:35:53','admin'),
 (120,1,8,101,'Which font size is best for paragraph of Text?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;15 or 16&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;10 or 12&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;9 or 10&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;8 or 10&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 21:37:04','admin'),
 (121,1,8,101,'Which of the following determines the emphasis or weight that the letters have when they are displayed?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Font face&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Font style&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Font size&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Alignment&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 21:38:15','admin'),
 (122,1,8,104,'Which of the following action pushes the text down to the next line, but does not create a new paragraph?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Press CTRL PLUS TAB keys&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Press SHIFT PLUS ENTER keys&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Press CTRL PLUS ALT PLUS DELETE&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Press SHIFT PLUS CTRL PLUS TAB&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 21:40:12','admin'),
 (123,1,8,105,'Which of the following determines the height of each line of text in the paragraph?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Paragraph spacing&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Line spacing&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Line markers&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-19 21:41:38','admin'),
 (124,1,8,108,'Which of the following are often used to bring main points to a reader&#39;s attention?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Paragraph spacing&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Line spacing&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Line markers&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Bulleted and Numbered List&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 21:43:43','admin'),
 (125,1,8,109,'When you copy or cut text, the text is stored in an area of memory called_____?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Document&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;RAM&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Hard disk&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Clipboard &quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 21:45:47','admin'),
 (126,1,8,112,'Which of the following predefined margins in Page margins?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Narrow&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Moderate&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Mirrored&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-19 21:47:15','admin');
/*!40000 ALTER TABLE `chapterquizmaster` ENABLE KEYS */;


--
-- Definition of table `chapterquizresponse`
--

DROP TABLE IF EXISTS `chapterquizresponse`;
CREATE TABLE `chapterquizresponse` (
  `userid` int(10) unsigned NOT NULL,
  `QuestionId` int(10) unsigned NOT NULL,
  `userReaponse` text,
  `IsCorrect` tinyint(3) unsigned default '0',
  `DateCreated` timestamp NOT NULL default CURRENT_TIMESTAMP,
  `Id` int(10) NOT NULL auto_increment,
  PRIMARY KEY  (`Id`),
  KEY `FK_chapterquizresponse_2` (`QuestionId`),
  KEY `FK_chapterquizresponse_3` (`userid`),
  CONSTRAINT `FK_chapterquizresponse_1` FOREIGN KEY (`userid`) REFERENCES `userdetails` (`Id`),
  CONSTRAINT `FK_chapterquizresponse_2` FOREIGN KEY (`QuestionId`) REFERENCES `chapterquizmaster` (`Id`),
  CONSTRAINT `FK_chapterquizresponse_3` FOREIGN KEY (`userid`) REFERENCES `userdetails` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=137 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `chapterquizresponse`
--

/*!40000 ALTER TABLE `chapterquizresponse` DISABLE KEYS */;
/*!40000 ALTER TABLE `chapterquizresponse` ENABLE KEYS */;


--
-- Definition of table `chaptersection`
--

DROP TABLE IF EXISTS `chaptersection`;
CREATE TABLE `chaptersection` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `chapterId` int(10) unsigned NOT NULL,
  `SectionName` varchar(255) character set utf8 NOT NULL,
  `SectionFileName` varchar(100) character set utf8 NOT NULL,
  `EstimateTime` int(255) unsigned NOT NULL default '0',
  `DateCreated` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`Id`),
  KEY `FK_chaptersection` (`chapterId`),
  CONSTRAINT `FK_chaptersection` FOREIGN KEY (`chapterId`) REFERENCES `chapterdetails` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=252 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `chaptersection`
--

/*!40000 ALTER TABLE `chaptersection` DISABLE KEYS */;
INSERT INTO `chaptersection` (`Id`,`chapterId`,`SectionName`,`SectionFileName`,`EstimateTime`,`DateCreated`) VALUES 
 (1,1,'Introduction','DomyFile_1.html',1800,'2013-11-02 06:26:41'),
 (2,1,'Computer Structure ','DomyFile_2.html',3600,'2013-11-02 06:26:41'),
 (3,1,'Hardware','DomyFile_3.html',2000,'2013-11-02 06:26:41'),
 (4,1,'Software','DomyFile_4.html',1800,'2013-11-02 06:26:41'),
 (5,1,'System Software','DomyFile_3.html',3600,'2013-11-02 06:26:41'),
 (6,1,'Application software','DomyFile_2.html',600,'2013-11-02 06:26:41'),
 (7,1,'Generalized Packages','DomyFile_1.html',1200,'2013-11-02 06:26:41'),
 (8,1,'Customized Packages','DomyFile_0.html',3000,'2013-11-02 06:26:41'),
 (9,2,'Introduction','DomyFile_0.html',0,'2013-11-15 09:36:14'),
 (10,2,'Input Devices','DomyFile_1.html',0,'2013-11-15 09:36:14'),
 (11,2,'keyboard','DomyFile_0.html',0,'2013-11-15 09:36:14'),
 (12,2,'Mouse','DomyFile_0.html',0,'2013-11-15 09:36:14'),
 (13,2,'Opticals-Canner','DomyFile_1.html',0,'2013-11-15 09:36:14'),
 (14,2,'Touch Screen','DomyFile_0.html',0,'2013-11-15 09:36:14'),
 (15,2,'Microphone','DomyFile_0.html',0,'2013-11-15 09:36:14'),
 (16,2,'Web Camera','DomyFile_0.html',0,'2013-11-15 09:36:14'),
 (17,2,'Output-Devices','DomyFile_1.html',0,'2013-11-15 09:36:14'),
 (18,2,'Monitor','DomyFile_0.html',0,'2013-11-15 09:36:14'),
 (19,2,'Display Resolution','DomyFile_1.html',0,'2013-11-15 09:36:14'),
 (20,2,'Printers','DomyFile_0.html',0,'2013-11-15 09:36:14'),
 (21,2,'Speaker','DomyFile_1.html',0,'2013-11-15 09:36:14'),
 (22,2,'Projector','DomyFile_0.html',0,'2013-11-15 09:36:14'),
 (23,2,'Fundamentals of CPU','DomyFile_1.html',0,'2013-11-15 09:36:15'),
 (24,3,'Introduction','DomyFile_1.html',0,'2013-11-15 09:41:59'),
 (25,3,'Storage Type','DomyFile_0.html',0,'2013-11-15 09:42:00'),
 (26,3,'Primary Storage','DomyFile_2.html',0,'2013-11-15 09:42:00'),
 (27,3,'RAM Memory','DomyFile_3.html',0,'2013-11-15 09:42:00'),
 (28,3,'ROM Memory','DomyFile_1.html',0,'2013-11-15 09:42:00'),
 (29,3,'Secondary Storage','DomyFile_3.html',0,'2013-11-15 09:42:00'),
 (30,3,'Hard Disk Drive','DomyFile_2.html',0,'2013-11-15 09:42:00'),
 (31,3,'CDs and- DVDs','DomyFile_3.html',0,'2013-11-15 09:42:00'),
 (32,3,'USB Flash Drive','DomyFile_4.html',0,'2013-11-15 09:42:00'),
 (33,4,'Introduction','DomyFile_2.html',0,'2013-11-15 09:50:29'),
 (34,4,'Functions of an Operating System','DomyFile_1.html',0,'2013-11-15 09:50:30'),
 (35,4,'Operating System Vendors','DomyFile_3.html',0,'2013-11-15 09:50:30'),
 (36,4,'Logging On','DomyFile_2.html',0,'2013-11-15 09:50:30'),
 (37,4,'Start Menu','DomyFile_1.html',0,'2013-11-15 09:50:30'),
 (38,4,'Overview of the Options','DomyFile_2.html',0,'2013-11-15 09:50:30'),
 (39,4,'Task Bar','DomyFile_4.html',0,'2013-11-15 09:50:30'),
 (40,4,'Start Program','DomyFile_2.html',0,'2013-11-15 09:50:30'),
 (41,4,'Quitting Program','DomyFile_3.html',0,'2013-11-15 09:50:30'),
 (42,4,'Getting Help','DomyFile_2.html',0,'2013-11-15 09:50:30'),
 (43,4,'Locating Files and Folders','DomyFile_4.html',0,'2013-11-15 09:50:30'),
 (44,4,'Changing System Settings','DomyFile_3.html',0,'2013-11-15 09:50:30'),
 (45,4,'Using My Computer','DomyFile_4.html',0,'2013-11-15 09:50:30'),
 (46,4,'Display the Storage Contents','DomyFile_3.html',0,'2013-11-15 09:50:30'),
 (47,4,'Start Lock and Shutdown Windows','DomyFile_3.html',0,'2013-11-15 09:50:30'),
 (48,5,'File Management in Windows','DomyFile_2.html',0,'2013-11-15 09:55:28'),
 (49,5,'Using Windows Explorer','DomyFile_3.html',0,'2013-11-15 09:55:28'),
 (50,5,'Copying or Moving a File or Folder','DomyFile_4.html',0,'2013-11-15 09:55:28'),
 (51,5,'View File Details','DomyFile_3.html',0,'2013-11-15 09:55:28'),
 (52,5,'Create New Folder','DomyFile_4.html',0,'2013-11-15 09:55:28'),
 (53,5,'Rename a File or Folder','DomyFile_3.html',0,'2013-11-15 09:55:28'),
 (54,5,'Delete a File or Folder','DomyFile_4.html',0,'2013-11-15 09:55:28'),
 (55,5,'Hidden Files and Folders','DomyFile_2.html',0,'2013-11-15 09:55:28'),
 (56,5,'Install Software Hardware','DomyFile_3.html',0,'2013-11-15 09:55:28'),
 (57,5,'Install Software','DomyFile_2.html',0,'2013-11-15 09:55:28'),
 (58,5,'Change or Remove Software','DomyFile_1.html',0,'2013-11-15 09:55:28'),
 (59,5,'Add New Features','DomyFile_3.html',0,'2013-11-15 09:55:28'),
 (60,5,'Install Hardware','DomyFile_2.html',0,'2013-11-15 09:55:28'),
 (61,5,'Search in Windows','DomyFile_3.html',0,'2013-11-15 09:55:28'),
 (62,5,'Device Manager','DomyFile_4.html',0,'2013-11-15 09:55:28'),
 (63,6,'Application Software','DomyFile_2.html',0,'2013-11-16 12:42:14'),
 (64,6,'The Toolbar','DomyFile_1.html',0,'2013-11-16 12:42:14'),
 (65,6,'Color Palette','DomyFile_4.html',0,'2013-11-16 12:42:14'),
 (66,6,'The Option Tool','DomyFile_2.html',0,'2013-11-16 12:42:14'),
 (67,6,'Save Image','DomyFile_3.html',0,'2013-11-16 12:42:14'),
 (68,6,'Introduction','DomyFile_4.html',0,'2013-11-16 12:42:14'),
 (69,7,'Introduction','DomyFile_3.html',0,'2013-11-16 12:44:25'),
 (70,7,'Open Notepad','DomyFile_2.html',0,'2013-11-16 12:44:25'),
 (71,7,'Save','DomyFile_3.html',0,'2013-11-16 12:44:25'),
 (72,7,'Print','DomyFile_4.html',0,'2013-11-16 12:44:25'),
 (73,7,'Open','DomyFile_3.html',0,'2013-11-16 12:44:25'),
 (74,7,'Font','DomyFile_4.html',0,'2013-11-16 12:44:25'),
 (75,7,'Word Wrap','DomyFile_3.html',0,'2013-11-16 12:44:26'),
 (76,8,'Introduction','DomyFile_1.html',0,'2013-11-16 01:02:33'),
 (77,8,'Features','DomyFile_3.html',0,'2013-11-16 01:02:33'),
 (78,8,'Word 2007','DomyFile_4.html',0,'2013-11-16 01:02:33'),
 (79,8,'Screen Layout','DomyFile_2.html',0,'2013-11-16 01:02:33'),
 (80,8,'Menus','DomyFile_1.html',0,'2013-11-16 01:02:33'),
 (81,8,'Toolbars','DomyFile_4.html',0,'2013-11-16 01:02:33'),
 (82,8,'Rulers','DomyFile_3.html',0,'2013-11-16 01:02:33'),
 (83,8,'Typing Screen Objects','DomyFile_2.html',0,'2013-11-16 01:02:33'),
 (84,8,'Scrollbars','DomyFile_3.html',0,'2013-11-16 01:02:33'),
 (85,8,'Managing Documents','DomyFile_2.html',0,'2013-11-16 01:02:33'),
 (86,8,'Create New Document','DomyFile_1.html',0,'2013-11-16 01:02:33'),
 (87,8,'Open an Existing Document','DomyFile_2.html',0,'2013-11-16 01:02:33'),
 (88,8,'Save a New or Existing Document','DomyFile_3.html',0,'2013-11-16 01:02:33'),
 (89,8,'Find a Document','DomyFile_4.html',0,'2013-11-16 01:02:34'),
 (90,8,'Close a Document','DomyFile_3.html',0,'2013-11-16 01:02:34'),
 (91,8,'Print a Document','DomyFile_2.html',0,'2013-11-16 01:02:34'),
 (92,8,'Exit Word Program','DomyFile_1.html',0,'2013-11-16 01:02:34'),
 (93,8,'Keyboard Shortcuts','DomyFile_2.html',0,'2013-11-16 01:02:34'),
 (94,8,'Working with Text','DomyFile_1.html',0,'2013-11-16 01:02:34'),
 (95,8,'Typing Text','DomyFile_3.html',0,'2013-11-16 01:02:34'),
 (96,8,'Inserting Text','DomyFile_4.html',0,'2013-11-16 01:02:34'),
 (97,8,'Spacebar and Tabs','DomyFile_2.html',0,'2013-11-16 01:02:34'),
 (98,8,'Selecting Text','DomyFile_3.html',0,'2013-11-16 01:02:34'),
 (99,8,'Deleting Text','DomyFile_1.html',0,'2013-11-16 01:02:34'),
 (100,8,'Replacing Text','DomyFile_3.html',0,'2013-11-16 01:02:34'),
 (101,8,'Formatting Text','DomyFile_2.html',0,'2013-11-16 01:02:34'),
 (102,8,'Format Painter','DomyFile_1.html',0,'2013-11-16 01:02:34'),
 (103,8,'Format Paragraphs','DomyFile_0.html',0,'2013-11-16 01:02:34'),
 (104,8,'Line Markers','DomyFile_2.html',0,'2013-11-16 01:02:34'),
 (105,8,'Line Spacing','DomyFile_3.html',0,'2013-11-16 01:02:34'),
 (106,8,'Paragraph Spacing','DomyFile_2.html',0,'2013-11-16 01:02:34'),
 (107,8,'Borders and Shading','DomyFile_3.html',0,'2013-11-16 01:02:34'),
 (108,8,'Bulleted and Numbered Lists','DomyFile_1.html',0,'2013-11-16 01:02:34'),
 (109,8,'Copying Text and Moveing Text','DomyFile_2.html',0,'2013-11-16 01:02:34'),
 (110,8,'Spelling and Grammar','DomyFile_3.html',0,'2013-11-16 01:02:34'),
 (111,8,'Page Formatting','DomyFile_4.html',0,'2013-11-16 01:02:34'),
 (112,8,'Page Margins','DomyFile_3.html',0,'2013-11-16 01:02:34'),
 (113,8,'Page Size and Orientation','DomyFile_4.html',0,'2013-11-16 01:02:34'),
 (114,8,'Zoom in to the Page','DomyFile_2.html',0,'2013-11-16 01:02:34'),
 (115,8,'Headers and Footers','DomyFile_3.html',0,'2013-11-16 01:02:34'),
 (116,8,'Page Numbers','DomyFile_4.html',0,'2013-11-16 01:02:34'),
 (117,8,'Inserting a Page Break','DomyFile_2.html',0,'2013-11-16 01:02:34'),
 (118,8,'Deleting a page break','DomyFile_3.html',0,'2013-11-16 01:02:34'),
 (119,9,'Introduction','DomyFile_3.html',0,'2013-11-16 01:17:26'),
 (120,9,'Features os Spreadsheets','DomyFile_1.html',0,'2013-11-16 01:17:26'),
 (121,9,'Features of MS-Excel 2007','DomyFile_3.html',0,'2013-11-16 01:17:26'),
 (122,9,'Office themes and Excel styles','DomyFile_4.html',0,'2013-11-16 01:17:26'),
 (123,9,'Formulas','DomyFile_3.html',0,'2013-11-16 01:17:26'),
 (124,9,'Function AutoComplete','DomyFile_2.html',0,'2013-11-16 01:17:26'),
 (125,9,'Sorting and Filtering','DomyFile_1.html',0,'2013-11-16 01:17:26'),
 (126,9,'Starting Excel','DomyFile_2.html',0,'2013-11-16 01:17:26'),
 (127,9,'Excel Worksheet','DomyFile_1.html',0,'2013-11-16 01:17:26'),
 (128,9,'Selecting, Adding and Renaming Worksheets','DomyFile_2.html',0,'2013-11-16 01:17:26'),
 (129,9,'Selecting Cells and Ranges','DomyFile_3.html',0,'2013-11-16 01:17:26'),
 (130,9,'Navigating the Worksheet','DomyFile_4.html',0,'2013-11-16 01:17:27'),
 (131,9,'Data Entry','DomyFile_2.html',0,'2013-11-16 01:17:27'),
 (132,9,'Editing Data','DomyFile_3.html',0,'2013-11-16 01:17:27'),
 (133,9,'Cell References','DomyFile_1.html',0,'2013-11-16 01:17:27'),
 (134,9,'Find and Replace','DomyFile_2.html',0,'2013-11-16 01:17:27'),
 (135,9,'Modifying a Worksheet','DomyFile_3.html',0,'2013-11-16 01:17:27'),
 (136,9,'Resizing Rows and Columns','DomyFile_2.html',0,'2013-11-16 01:17:27'),
 (137,9,'Insert moved or copied cells','DomyFile_3.html',0,'2013-11-16 01:17:27'),
 (138,9,'Move or copy the contents of a cell','DomyFile_2.html',0,'2013-11-16 01:17:27'),
 (139,9,'Copy cell values, cell formats, or formulas only','DomyFile_1.html',0,'2013-11-16 01:17:27'),
 (140,9,'Drag and Drop','DomyFile_2.html',0,'2013-11-16 01:17:27'),
 (141,9,'Freez Pangs','DomyFile_3.html',0,'2013-11-16 01:17:27'),
 (142,9,'Page Breaks','DomyFile_2.html',0,'2013-11-16 01:17:27'),
 (143,9,'Page Setup','DomyFile_1.html',0,'2013-11-16 01:17:27'),
 (144,9,'Print Preview','DomyFile_2.html',0,'2013-11-16 01:17:27'),
 (145,9,'Print','DomyFile_3.html',0,'2013-11-16 01:17:27'),
 (146,9,'File Open, Save and Close','DomyFile_4.html',0,'2013-11-16 01:17:27'),
 (147,9,'Format Cells','DomyFile_2.html',0,'2013-11-16 01:17:27'),
 (148,9,'Format Cell Dialog Box','DomyFile_1.html',0,'2013-11-16 01:17:27'),
 (149,9,'Date and Time','DomyFile_2.html',0,'2013-11-16 01:17:27'),
 (150,9,'Format Coulumns and Rows','DomyFile_1.html',0,'2013-11-16 01:17:27'),
 (151,9,'AutoFit Columns','DomyFile_3.html',0,'2013-11-16 01:17:27'),
 (152,9,'Hide Column or Row','DomyFile_2.html',0,'2013-11-16 01:17:27'),
 (153,9,'Unhide Column or Row','DomyFile_1.html',0,'2013-11-16 01:17:27'),
 (154,9,'Formulas and Functions','DomyFile_3.html',0,'2013-11-16 01:17:27'),
 (155,9,'Copy a Formula','DomyFile_2.html',0,'2013-11-16 01:17:27'),
 (156,9,'Auto sum feature','DomyFile_1.html',0,'2013-11-16 01:17:27'),
 (157,9,'Charts','DomyFile_2.html',0,'2013-11-16 01:17:27'),
 (158,9,'Types of Chart','DomyFile_3.html',0,'2013-11-16 01:17:27'),
 (159,9,'Components of a Chart','DomyFile_2.html',0,'2013-11-16 01:17:27'),
 (160,9,'Draw a Chart','DomyFile_3.html',0,'2013-11-16 01:17:27'),
 (161,9,'Editing of a Chart','DomyFile_2.html',0,'2013-11-16 01:17:27'),
 (162,9,'Resizing the Chart','DomyFile_4.html',0,'2013-11-16 01:17:27'),
 (163,9,'Moving the Chart','DomyFile_3.html',0,'2013-11-16 01:17:28'),
 (164,9,'Copying the Chart to Microsoft word','DomyFile_2.html',0,'2013-11-16 01:17:28'),
 (165,9,'Graphics - Autoshapes and Smart Art','DomyFile_3.html',0,'2013-11-16 01:17:28'),
 (166,9,'Smart Art Graphics','DomyFile_2.html',0,'2013-11-16 01:17:28'),
 (167,9,'Adding Clip Art','DomyFile_3.html',0,'2013-11-16 01:17:28'),
 (168,9,'Inserting and Editing a Picture from a File','DomyFile_4.html',0,'2013-11-16 01:17:28'),
 (169,10,'Microsoft Powerpoint','DomyFile_1.html',0,'2013-11-16 01:28:12'),
 (170,10,'Introduction','DomyFile_2.html',0,'2013-11-16 01:28:12'),
 (171,10,'Starting a Powerpoint','DomyFile_3.html',0,'2013-11-16 01:28:12'),
 (172,10,'Installed Templates','DomyFile_2.html',0,'2013-11-16 01:28:12'),
 (173,10,'Design Template','DomyFile_3.html',0,'2013-11-16 01:28:12'),
 (174,10,'Blank Presentations','DomyFile_2.html',0,'2013-11-16 01:28:12'),
 (175,10,'Slide Layouts','DomyFile_1.html',0,'2013-11-16 01:28:12'),
 (176,10,'Selecting Content','DomyFile_2.html',0,'2013-11-16 01:28:12'),
 (177,10,'Open and Existing Presentations','DomyFile_3.html',0,'2013-11-16 01:28:12'),
 (178,10,'Viewing slides','DomyFile_2.html',0,'2013-11-16 01:28:12'),
 (179,10,'Normal View','DomyFile_3.html',0,'2013-11-16 01:28:12'),
 (180,10,'Slide Sorter View','DomyFile_2.html',0,'2013-11-16 01:28:12'),
 (181,10,'Slide Show View','DomyFile_3.html',0,'2013-11-16 01:28:12'),
 (182,10,'Design Tips','DomyFile_1.html',0,'2013-11-16 01:28:12'),
 (183,10,'Working with Slides','DomyFile_2.html',0,'2013-11-16 01:28:12'),
 (184,10,'Applying a Design Templates','DomyFile_3.html',0,'2013-11-16 01:28:12'),
 (185,10,'Changing Slide Layouts','DomyFile_2.html',0,'2013-11-16 01:28:12'),
 (186,10,'Insert/Edit Existing Slides as Your New Slides','DomyFile_0.html',0,'2013-11-16 01:28:12'),
 (187,10,'Reordering Slides','DomyFile_1.html',0,'2013-11-16 01:28:12'),
 (188,10,'Hide Slides','DomyFile_2.html',0,'2013-11-16 01:28:12'),
 (189,10,'Moving Between Slides','DomyFile_3.html',0,'2013-11-16 01:28:12'),
 (190,10,'Working with Text','DomyFile_2.html',0,'2013-11-16 01:28:13'),
 (191,10,'Inserting Text','DomyFile_1.html',0,'2013-11-16 01:28:13'),
 (192,10,'Formatting Text','DomyFile_2.html',0,'2013-11-16 01:28:13'),
 (193,10,'Text Box Properties','DomyFile_3.html',0,'2013-11-16 01:28:13'),
 (194,10,'Adding Notes','DomyFile_2.html',0,'2013-11-16 01:28:13'),
 (195,10,'Spell Check','DomyFile_3.html',0,'2013-11-16 01:28:13'),
 (196,10,'Saving and Printing','DomyFile_2.html',0,'2013-11-16 01:28:13'),
 (197,10,'Page Setup','DomyFile_3.html',0,'2013-11-16 01:28:13'),
 (198,10,'Save as File','DomyFile_2.html',0,'2013-11-16 01:28:13'),
 (199,10,'Save as Web Page','DomyFile_1.html',0,'2013-11-16 01:28:13'),
 (200,10,'Print','DomyFile_3.html',0,'2013-11-16 01:28:13'),
 (201,10,'Close Document','DomyFile_4.html',0,'2013-11-16 01:28:13'),
 (202,10,'Exit Powerpoint Program','DomyFile_3.html',0,'2013-11-16 01:28:13'),
 (203,10,'Keyboard Shortcuts','DomyFile_4.html',0,'2013-11-16 01:28:13'),
 (204,11,'Computer Networks','DomyFile_1.html',0,'2013-11-16 01:33:21'),
 (205,11,'Local Area Network','DomyFile_2.html',0,'2013-11-16 01:33:21'),
 (206,11,'Metropolitan Area Network','DomyFile_1.html',0,'2013-11-16 01:33:21'),
 (207,11,'Wide Area Network','DomyFile_2.html',0,'2013-11-16 01:33:21'),
 (208,11,'Protocols','DomyFile_3.html',0,'2013-11-16 01:33:21'),
 (209,11,'Internet Protocol','DomyFile_2.html',0,'2013-11-16 01:33:21'),
 (210,11,'Postoffice Protocol','DomyFile_3.html',0,'2013-11-16 01:33:21'),
 (211,11,'Hyper Text Transfer Protocol','DomyFile_4.html',0,'2013-11-16 01:33:21'),
 (212,11,'File Transfer Protocol','DomyFile_2.html',0,'2013-11-16 01:33:21'),
 (213,11,'IP Address','DomyFile_1.html',0,'2013-11-16 01:33:21'),
 (214,11,'Share a Printer','DomyFile_2.html',0,'2013-11-16 01:33:21'),
 (215,11,'File and Printer Sharing','DomyFile_1.html',0,'2013-11-16 01:33:21'),
 (216,11,'Sharing Printers','DomyFile_2.html',0,'2013-11-16 01:33:21'),
 (217,11,'Stop Printer Sharing','DomyFile_1.html',0,'2013-11-16 01:33:21'),
 (218,11,'Connect to Printer','DomyFile_3.html',0,'2013-11-16 01:33:21'),
 (219,11,'Setting or Removing Permissions','DomyFile_2.html',0,'2013-11-16 01:33:21'),
 (220,12,'Introduction','DomyFile_1.html',0,'2013-11-16 04:35:39'),
 (221,12,'Antivirus','DomyFile_2.html',0,'2013-11-16 04:35:39'),
 (222,12,'Popular Antivirus Software','DomyFile_3.html',0,'2013-11-16 04:35:39'),
 (223,12,'Best Practices','DomyFile_2.html',0,'2013-11-16 04:35:40'),
 (224,12,'Firewall','DomyFile_3.html',0,'2013-11-16 04:35:40'),
 (225,12,'Configure Windows XP Firewall','DomyFile_4.html',0,'2013-11-16 04:35:40'),
 (226,12,'Best Practices','DomyFile_3.html',0,'2013-11-16 04:35:40'),
 (227,12,'Security Essentials','DomyFile_1.html',0,'2013-11-16 04:35:40'),
 (228,12,'Safe Mode','DomyFile_3.html',0,'2013-11-16 04:35:40'),
 (229,12,'Start the Computer in Safe Mode','DomyFile_2.html',0,'2013-11-16 04:35:40'),
 (230,12,'MSConfig Utility','DomyFile_3.html',0,'2013-11-16 04:35:40'),
 (231,13,'Introduction','DomyFile_3.html',0,'2013-11-16 04:41:00'),
 (232,13,'Browsers','DomyFile_2.html',0,'2013-11-16 04:41:00'),
 (233,13,'Popular Web Browsers','DomyFile_3.html',0,'2013-11-16 04:41:00'),
 (234,13,'Web Browser User Interface','DomyFile_2.html',0,'2013-11-16 04:41:00'),
 (235,13,'Internet Options','DomyFile_3.html',0,'2013-11-16 04:41:00'),
 (236,13,'Cleanup History','DomyFile_2.html',0,'2013-11-16 04:41:00'),
 (237,13,'Protocol and Security','DomyFile_1.html',0,'2013-11-16 04:41:00'),
 (238,13,'EMAIL System','DomyFile_3.html',0,'2013-11-16 04:41:00'),
 (239,13,'Popular Email Service Providers','DomyFile_2.html',0,'2013-11-16 04:41:00'),
 (240,13,'Password Strength','DomyFile_1.html',0,'2013-11-16 04:41:00'),
 (241,13,'SPAM Emails','DomyFile_3.html',0,'2013-11-16 04:41:00'),
 (242,13,'Social Engineering Emails','DomyFile_4.html',0,'2013-11-16 04:41:00'),
 (243,13,'Email Best Practices','DomyFile_2.html',0,'2013-11-16 04:41:00'),
 (244,13,'Search Engines','DomyFile_3.html',0,'2013-11-16 04:41:00'),
 (245,13,'Popular Search Engines','DomyFile_2.html',0,'2013-11-16 04:41:00'),
 (246,13,'Google Tricks','DomyFile_3.html',0,'2013-11-16 04:41:00'),
 (247,13,'Downloads and Installations','DomyFile_4.html',0,'2013-11-16 04:41:00'),
 (248,14,'Introduction','DomyFile_2.html',0,'2013-11-16 04:43:28'),
 (249,14,'Social Media Websites','DomyFile_1.html',0,'2013-11-16 04:43:28'),
 (250,14,'Popular Social Media','DomyFile_2.html',0,'2013-11-16 04:43:28'),
 (251,14,'Best Practices','DomyFile_3.html',0,'2013-11-16 04:43:29');
/*!40000 ALTER TABLE `chaptersection` ENABLE KEYS */;


--
-- Definition of table `contentversion`
--

DROP TABLE IF EXISTS `contentversion`;
CREATE TABLE `contentversion` (
  `version` varchar(3) NOT NULL default '1'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `contentversion`
--

/*!40000 ALTER TABLE `contentversion` DISABLE KEYS */;
INSERT INTO `contentversion` (`version`) VALUES 
 ('1');
/*!40000 ALTER TABLE `contentversion` ENABLE KEYS */;


--
-- Definition of table `coursedetails`
--

DROP TABLE IF EXISTS `coursedetails`;
CREATE TABLE `coursedetails` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `CourseName` varchar(512) character set utf8 NOT NULL,
  `DateCreated` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `coursedetails`
--

/*!40000 ALTER TABLE `coursedetails` DISABLE KEYS */;
INSERT INTO `coursedetails` (`Id`,`CourseName`,`DateCreated`) VALUES 
 (1,'Fundamental of Computers','2013-10-25 17:53:46');
/*!40000 ALTER TABLE `coursedetails` ENABLE KEYS */;


--
-- Definition of table `finalquizmaster`
--

DROP TABLE IF EXISTS `finalquizmaster`;
CREATE TABLE `finalquizmaster` (
  `Id` int(10) unsigned NOT NULL,
  `courseId` int(10) unsigned NOT NULL,
  `chapterId` int(10) unsigned NOT NULL,
  `sectionId` int(10) unsigned NOT NULL,
  `groupNo` int(10) unsigned NOT NULL,
  `complexity` int(10) unsigned NOT NULL,
  `questionText` text NOT NULL,
  `IsQuestionOptionPresent` tinyint(3) unsigned NOT NULL default '0',
  `QuestionOption` text,
  `AnswerOption` text NOT NULL,
  `ContentVersion` varchar(3) character set utf8 default '1',
  `DateCreated` timestamp NOT NULL default CURRENT_TIMESTAMP,
  `createdby` varchar(50) character set utf8 NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_finalquizmaster_1` (`courseId`),
  KEY `FK_finalquizmaster_2` (`chapterId`),
  CONSTRAINT `FK_finalquizmaster_1` FOREIGN KEY (`courseId`) REFERENCES `coursedetails` (`Id`),
  CONSTRAINT `FK_finalquizmaster_2` FOREIGN KEY (`chapterId`) REFERENCES `chapterdetails` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `finalquizmaster`
--

/*!40000 ALTER TABLE `finalquizmaster` DISABLE KEYS */;
INSERT INTO `finalquizmaster` (`Id`,`courseId`,`chapterId`,`sectionId`,`groupNo`,`complexity`,`questionText`,`IsQuestionOptionPresent`,`QuestionOption`,`AnswerOption`,`ContentVersion`,`DateCreated`,`createdby`) VALUES 
 (1,1,1,2,1,1,'Computer components are divided into two major categories, namely :',0,'[]','[{\"AnswerOption\":\"Input and output\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware and Software\",\"IsCurrect\":true},{\"AnswerOption\":\"Processing and Storage\",\"IsCurrect\":false},{\"AnswerOption\":\"Control Unit and Arithmetic Logic Unit\",\"IsCurrect\":false}]','1','2013-11-02 08:29:46','vasim'),
 (2,1,1,2,1,1,'Computer is a device which :',0,'[]','[{\"AnswerOption\":\"It takes input from the user,processes these data and produces the output\",\"IsCurrect\":false},{\"AnswerOption\":\"It could process both numerical as well as non-numerical calculations.\",\"IsCurrect\":false},{\"AnswerOption\":\"It controls all operations inside a computer.\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above.\",\"IsCurrect\":true}]','1','2013-11-02 08:31:49','vasim'),
 (3,1,1,2,1,1,'Which of the following are Computer Functions :',0,'[]','[{\"AnswerOption\":\"Processing\",\"IsCurrect\":false},{\"AnswerOption\":\"Storage\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-02 08:32:52','vasim'),
 (4,1,1,2,1,1,'Which of the following are basic Computer components :',0,'[]','[{\"AnswerOption\":\"Control Unit (CU)\",\"IsCurrect\":false},{\"AnswerOption\":\"Arithmetic Logic Unit (ALU)\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-02 08:33:42','vasim'),
 (5,1,1,2,2,1,'The process of input, output, processing and storage is performed under the supervision of a unit called _____',0,'[]','[{\"AnswerOption\":\"Memory Unit\",\"IsCurrect\":false},{\"AnswerOption\":\"Input\",\"IsCurrect\":false},{\"AnswerOption\":\"Control Unit(CU)\",\"IsCurrect\":true},{\"AnswerOption\":\"Output\",\"IsCurrect\":false}]','1','2013-11-02 08:34:32','vasim'),
 (6,1,1,2,2,1,'Which of the major operations performed are addition, subtraction, multiplication, division, logic and comparisons.',0,'[]','[{\"AnswerOption\":\"Output\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware\",\"IsCurrect\":false},{\"AnswerOption\":\"Arithmetic Logic Unit (ALU)\",\"IsCurrect\":true},{\"AnswerOption\":\"Control Unit(CU)\",\"IsCurrect\":false}]','1','2013-11-02 08:36:05','vasim'),
 (7,1,1,2,2,2,'Which of the computer system are jointly known as the central processing unit (CPU).',0,'[]','[{\"AnswerOption\":\"Input and Output.\",\"IsCurrect\":false},{\"AnswerOption\":\"Arithmetic Logic Unit and Control Unit\",\"IsCurrect\":true},{\"AnswerOption\":\"Hardware and Software\",\"IsCurrect\":false},{\"AnswerOption\":\"All of above\",\"IsCurrect\":false}]','1','2013-11-02 08:37:53','vasim'),
 (8,1,1,2,1,1,'Which of the following major characteristics of a computer :',0,'[]','[{\"AnswerOption\":\"high speed\",\"IsCurrect\":false},{\"AnswerOption\":\" accuracy\",\"IsCurrect\":false},{\"AnswerOption\":\"diligence\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-02 08:39:14','vasim'),
 (9,1,1,2,1,1,'Which of the Computer Components takes care of step -by-step processing of all operations inside the computer :',0,'[]','[{\"AnswerOption\":\"Memory Unit\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware\",\"IsCurrect\":false},{\"AnswerOption\":\"Control Unit(CU)\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-02 08:40:10','vasim'),
 (10,1,1,3,1,1,'Which of the following refers to the physical parts or components of a computer :',0,'[]','[{\"AnswerOption\":\"Monitor\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware\",\"IsCurrect\":true},{\"AnswerOption\":\"Keyboard\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-02 08:41:32','vasim'),
 (11,1,1,5,1,1,'Which of the following refers to sets of programs, responsible for running the computer, controlling various operations of computer systems and management of computer resources :',0,'[]','[{\"AnswerOption\":\"Control Unit(CU)\",\"IsCurrect\":false},{\"AnswerOption\":\"System Software\",\"IsCurrect\":true},{\"AnswerOption\":\"Utility Programs\",\"IsCurrect\":false},{\"AnswerOption\":\"Operating System\",\"IsCurrect\":false}]','1','2013-11-02 08:43:26','vasim'),
 (12,1,1,5,1,1,'An operating system is ______',0,'[]','[{\"AnswerOption\":\"The process of input, output, processing and storage is performed under the supervision of a unit.\",\"IsCurrect\":false},{\"AnswerOption\":\"The system software that provides an interface for a user to communicate with the computer.\",\"IsCurrect\":false},{\"AnswerOption\":\"It manages hardware devices and supports application programs.\",\"IsCurrect\":false},{\"AnswerOption\":\"Both B and C\",\"IsCurrect\":true}]','1','2013-11-02 08:44:33','vasim'),
 (13,1,1,5,1,1,'Which of the programs that bridge the gap between the functionality of operating systems :',0,'[]','[{\"AnswerOption\":\"Operating System\",\"IsCurrect\":false},{\"AnswerOption\":\"Utility\",\"IsCurrect\":true},{\"AnswerOption\":\"System Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Application Software\",\"IsCurrect\":false}]','1','2013-11-02 08:45:30','vasim'),
 (14,1,1,6,1,1,'Application software can be broadly classified into two types : ',0,'[]','[{\"AnswerOption\":\"Hardware and Software \",\"IsCurrect\":false},{\"AnswerOption\":\"Generalized packages and Customized packages\",\"IsCurrect\":true},{\"AnswerOption\":\"Input and Output\",\"IsCurrect\":false},{\"AnswerOption\":\"System Software and Application Software\",\"IsCurrect\":false}]','1','2013-11-02 08:46:39','vasim'),
 (15,1,1,7,1,1,'Which of the following Data Analysis?',0,'[]','[{\"AnswerOption\":\"Lotus Smart suites\",\"IsCurrect\":false},{\"AnswerOption\":\"Word Perfect\",\"IsCurrect\":false},{\"AnswerOption\":\"Apple Numbers\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and C\",\"IsCurrect\":true}]','1','2013-11-02 08:47:22','vasim'),
 (16,1,1,7,1,1,'Which of the generalized packages are listed : ',0,'[]','[{\"AnswerOption\":\"Word Processing Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Database Management System\",\"IsCurrect\":false},{\"AnswerOption\":\"Spreadsheets\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-02 08:48:15','vasim'),
 (17,1,1,5,1,1,'Some of the Popular Operating Systems are :',0,'[]','[{\"AnswerOption\":\" Spreadsheets\",\"IsCurrect\":false},{\"AnswerOption\":\"MS-Access\",\"IsCurrect\":false},{\"AnswerOption\":\"Central Processing Unit(CPU)\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":true}]','1','2013-11-02 08:48:50','vasim'),
 (18,1,1,8,1,1,'Which of the applications that are customized (or developed) to meet the specific requirements of an organization/institution :',0,'[]','[{\"AnswerOption\":\"generalized packages\",\"IsCurrect\":false},{\"AnswerOption\":\"Utility programs\",\"IsCurrect\":false},{\"AnswerOption\":\"Application Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Customized packages\",\"IsCurrect\":true}]','1','2013-11-02 08:49:54','vasim'),
 (19,1,1,5,1,1,'Utility programs are a broad category of software such as__',0,'[]','[{\"AnswerOption\":\"Compress (zip)/uncompress (unzip) files software\",\"IsCurrect\":false},{\"AnswerOption\":\"anti-virus software\",\"IsCurrect\":false},{\"AnswerOption\":\"split and join files software\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-02 08:50:37','vasim'),
 (20,1,1,3,1,1,'Physical parts of components of a computer such as following  :',0,'[]','[{\"AnswerOption\":\"Monitor\",\"IsCurrect\":false},{\"AnswerOption\":\"Central Unit(CU)\",\"IsCurrect\":false},{\"AnswerOption\":\"Central Processing Unit(CPU)\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and C\",\"IsCurrect\":true}]','1','2013-11-02 08:51:22','vasim'),
 (21,1,1,8,1,1,'Which of the packages are developed using high-level computer language : ',0,'[]','[{\"AnswerOption\":\"Generalized packages\",\"IsCurrect\":false},{\"AnswerOption\":\"Customized packages\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-02 08:52:23','vasim');
/*!40000 ALTER TABLE `finalquizmaster` ENABLE KEYS */;


--
-- Definition of table `finalquizresponse`
--

DROP TABLE IF EXISTS `finalquizresponse`;
CREATE TABLE `finalquizresponse` (
  `userid` int(10) unsigned NOT NULL,
  `QuestionId` int(10) unsigned NOT NULL,
  `userReaponse` text,
  `IsCorrect` tinyint(3) unsigned default '0',
  `DateCreated` timestamp NOT NULL default CURRENT_TIMESTAMP,
  `Id` int(10) NOT NULL auto_increment,
  PRIMARY KEY  (`Id`),
  KEY `FK_finalquizresponse_2` (`QuestionId`),
  KEY `FK_finalquizresponse_3` (`userid`),
  CONSTRAINT `FK_finalquizresponse_1` FOREIGN KEY (`userid`) REFERENCES `userdetails` (`Id`),
  CONSTRAINT `FK_finalquizresponse_2` FOREIGN KEY (`QuestionId`) REFERENCES `finalquizmaster` (`Id`),
  CONSTRAINT `FK_finalquizresponse_3` FOREIGN KEY (`userid`) REFERENCES `userdetails` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `finalquizresponse`
--

/*!40000 ALTER TABLE `finalquizresponse` DISABLE KEYS */;
INSERT INTO `finalquizresponse` (`userid`,`QuestionId`,`userReaponse`,`IsCorrect`,`DateCreated`,`Id`) VALUES 
 (1,1,'Hardware and Software',1,'2013-11-07 01:50:48',1),
 (1,2,'It takes input from the user,processes these data and produces the output',0,'2013-11-07 01:52:35',2),
 (1,3,'Processing',0,'2013-11-07 01:52:38',3),
 (1,4,'Arithmetic Logic Unit (ALU)',0,'2013-11-07 01:52:40',4),
 (1,5,'Control Unit(CU)',1,'2013-11-07 01:52:47',5),
 (1,6,'Arithmetic Logic Unit (ALU)',1,'2013-11-07 01:53:12',6),
 (1,7,'Input and Output.',0,'2013-11-07 01:53:21',7),
 (1,8,' accuracy',0,'2013-11-07 01:57:08',8),
 (1,9,'Memory Unit',0,'2013-11-07 01:57:10',9),
 (1,10,'All the above',0,'2013-11-07 01:58:59',10),
 (1,11,'Control Unit(CU)',0,'2013-11-07 02:01:26',11),
 (3,1,'Processing and Storage',0,'2013-11-07 02:52:47',12),
 (3,2,'It controls all operations inside a computer.',0,'2013-11-07 02:52:49',13),
 (3,3,'Storage',0,'2013-11-07 02:52:50',14),
 (3,4,'Control Unit (CU)',0,'2013-11-07 02:52:52',15),
 (3,5,'Memory Unit',0,'2013-11-07 02:52:54',16),
 (3,6,'Output',0,'2013-11-07 02:52:56',17),
 (3,7,'Input and Output.',0,'2013-11-07 02:52:57',18),
 (3,8,' accuracy',0,'2013-11-07 02:52:59',19),
 (3,9,'Hardware',0,'2013-11-07 02:53:01',20),
 (3,10,'Hardware',1,'2013-11-07 02:53:03',21),
 (3,11,'System Software',1,'2013-11-07 02:53:04',22),
 (3,12,'The system software that provides an interface for a user to communicate with the computer.',0,'2013-11-07 02:53:07',23),
 (3,13,'Operating System',0,'2013-11-07 02:53:34',24),
 (3,14,'Input and Output',0,'2013-11-07 02:53:35',25),
 (3,15,'Lotus Smart suites',0,'2013-11-07 02:53:37',26),
 (3,16,'Database Management System',0,'2013-11-07 02:53:38',27),
 (3,17,' Spreadsheets',0,'2013-11-07 02:53:40',28),
 (3,18,'Utility programs',0,'2013-11-07 02:53:42',29),
 (3,19,'split and join files software',0,'2013-11-07 02:53:44',30),
 (3,20,'Central Unit(CU)',0,'2013-11-07 02:53:46',31),
 (3,21,'Generalized packages',0,'2013-11-07 02:53:48',32);
/*!40000 ALTER TABLE `finalquizresponse` ENABLE KEYS */;


--
-- Definition of table `reportissue`
--

DROP TABLE IF EXISTS `reportissue`;
CREATE TABLE `reportissue` (
  `Id` int(10) NOT NULL auto_increment,
  `Title` varchar(500) default NULL,
  `description` varchar(500) default NULL,
  `email` varchar(100) default NULL,
  `Expectedconten` varchar(500) default NULL,
  `DatePosted` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `reportissue`
--

/*!40000 ALTER TABLE `reportissue` DISABLE KEYS */;
/*!40000 ALTER TABLE `reportissue` ENABLE KEYS */;


--
-- Definition of table `studentsqueries`
--

DROP TABLE IF EXISTS `studentsqueries`;
CREATE TABLE `studentsqueries` (
  `Id` int(10) NOT NULL auto_increment,
  `UserId` int(10) default NULL,
  `CollegeName` varchar(50) default NULL,
  `Name` varchar(100) default NULL,
  `MobileNo` varchar(50) default NULL,
  `Query` varchar(500) default NULL,
  `DatePosted` timestamp NOT NULL default CURRENT_TIMESTAMP,
  `QueryStatus` int(10) unsigned NOT NULL default '1',
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `studentsqueries`
--

/*!40000 ALTER TABLE `studentsqueries` DISABLE KEYS */;
/*!40000 ALTER TABLE `studentsqueries` ENABLE KEYS */;


--
-- Definition of table `studenttypingstats`
--

DROP TABLE IF EXISTS `studenttypingstats`;
CREATE TABLE `studenttypingstats` (
  `Id` int(10) NOT NULL auto_increment,
  `userId` int(10) default '0',
  `level` int(10) default '0',
  `timespaninsecods` int(10) default '0',
  `accuracy` int(10) default '0',
  `grossWPM` int(10) default '0',
  `netWPM` int(10) default '0',
  `DateCreated` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `studenttypingstats`
--

/*!40000 ALTER TABLE `studenttypingstats` DISABLE KEYS */;
/*!40000 ALTER TABLE `studenttypingstats` ENABLE KEYS */;


--
-- Definition of table `useractivitytracker`
--

DROP TABLE IF EXISTS `useractivitytracker`;
CREATE TABLE `useractivitytracker` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `userId` int(10) unsigned NOT NULL,
  `Activity` varchar(1000) character set utf8 NOT NULL,
  `DateCreated` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`Id`),
  KEY `FK_useractivitytracker_1` (`userId`),
  CONSTRAINT `FK_useractivitytracker_1` FOREIGN KEY (`userId`) REFERENCES `userdetails` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `useractivitytracker`
--

/*!40000 ALTER TABLE `useractivitytracker` DISABLE KEYS */;
/*!40000 ALTER TABLE `useractivitytracker` ENABLE KEYS */;


--
-- Definition of table `userchapterstatus`
--

DROP TABLE IF EXISTS `userchapterstatus`;
CREATE TABLE `userchapterstatus` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `userId` int(10) unsigned NOT NULL,
  `chapterId` int(10) unsigned NOT NULL,
  `sectionId` int(10) unsigned NOT NULL,
  `contentVersion` varchar(3) character set utf8 NOT NULL,
  `DateCreated` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`Id`),
  KEY `FK_userchapterstatus_1` (`userId`),
  KEY `FK_userchapterstatus_2` (`chapterId`),
  CONSTRAINT `FK_userchapterstatus_1` FOREIGN KEY (`userId`) REFERENCES `userdetails` (`Id`),
  CONSTRAINT `FK_userchapterstatus_2` FOREIGN KEY (`chapterId`) REFERENCES `chapterdetails` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=139 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `userchapterstatus`
--

/*!40000 ALTER TABLE `userchapterstatus` DISABLE KEYS */;
INSERT INTO `userchapterstatus` (`Id`,`userId`,`chapterId`,`sectionId`,`contentVersion`,`DateCreated`) VALUES 
 (32,2,1,1,'1','2013-11-19 16:52:59'),
 (33,2,1,2,'1','2013-11-19 16:55:07'),
 (34,2,1,3,'1','2013-11-19 16:58:38'),
 (35,2,1,4,'1','2013-11-19 16:58:52'),
 (36,2,1,5,'1','2013-11-19 16:59:01'),
 (37,2,1,6,'1','2013-11-19 16:59:21'),
 (38,2,1,7,'1','2013-11-19 16:59:42'),
 (39,2,1,8,'1','2013-11-19 16:59:53'),
 (40,2,2,9,'1','2013-11-19 17:00:01'),
 (41,2,2,10,'1','2013-11-19 17:00:14'),
 (42,2,2,11,'1','2013-11-19 17:00:45'),
 (43,2,2,12,'1','2013-11-19 17:00:58'),
 (44,2,2,13,'1','2013-11-19 17:01:16'),
 (45,2,2,14,'1','2013-11-19 17:01:40'),
 (46,1,1,1,'1','2013-11-19 17:07:05'),
 (47,1,1,2,'1','2013-11-19 17:07:27'),
 (48,1,1,3,'1','2013-11-19 17:08:03'),
 (49,1,1,4,'1','2013-11-19 17:08:12'),
 (50,1,1,5,'1','2013-11-19 17:08:16'),
 (51,1,1,6,'1','2013-11-19 17:08:21'),
 (52,1,1,7,'1','2013-11-19 17:08:26'),
 (53,1,1,8,'1','2013-11-19 17:08:40'),
 (54,1,2,9,'1','2013-11-19 17:08:47'),
 (55,1,2,10,'1','2013-11-19 17:08:55'),
 (56,1,2,11,'1','2013-11-19 17:09:00'),
 (57,1,2,12,'1','2013-11-19 17:09:14'),
 (58,1,2,13,'1','2013-11-19 17:09:29'),
 (59,1,2,14,'1','2013-11-19 17:09:47'),
 (60,1,2,15,'1','2013-11-19 17:09:52'),
 (61,1,2,16,'1','2013-11-19 17:10:01'),
 (62,1,2,17,'1','2013-11-19 17:10:06'),
 (63,1,2,18,'1','2013-11-19 17:10:19'),
 (64,2,2,15,'1','2013-11-19 17:11:50'),
 (65,2,2,16,'1','2013-11-19 17:12:14'),
 (66,2,2,17,'1','2013-11-19 17:12:19'),
 (67,2,2,18,'1','2013-11-19 17:12:22'),
 (68,2,2,19,'1','2013-11-19 17:21:38'),
 (69,2,2,20,'1','2013-11-19 17:22:11'),
 (70,2,2,21,'1','2013-11-19 17:22:24'),
 (71,2,2,22,'1','2013-11-19 17:24:51'),
 (72,2,2,23,'1','2013-11-19 17:25:00'),
 (73,2,3,24,'1','2013-11-19 17:28:58'),
 (74,2,3,25,'1','2013-11-19 17:33:17'),
 (75,2,3,26,'1','2013-11-19 17:33:25'),
 (76,2,3,27,'1','2013-11-19 17:33:34'),
 (77,2,3,28,'1','2013-11-19 17:33:48'),
 (78,2,3,29,'1','2013-11-19 17:40:29'),
 (79,2,3,30,'1','2013-11-19 17:43:05'),
 (80,2,3,31,'1','2013-11-19 17:43:24'),
 (81,2,3,32,'1','2013-11-19 17:48:52'),
 (82,2,4,33,'1','2013-11-19 18:01:37'),
 (83,2,4,34,'1','2013-11-19 18:02:18'),
 (84,2,4,35,'1','2013-11-19 18:04:18'),
 (85,2,4,36,'1','2013-11-19 18:04:30'),
 (86,2,4,37,'1','2013-11-19 18:04:34'),
 (87,2,4,38,'1','2013-11-19 18:08:22'),
 (88,2,4,39,'1','2013-11-19 18:11:38'),
 (89,2,4,40,'1','2013-11-19 18:11:45'),
 (90,2,4,41,'1','2013-11-19 18:19:57'),
 (91,2,4,42,'1','2013-11-19 18:26:49'),
 (92,2,4,43,'1','2013-11-19 18:32:41'),
 (93,2,4,44,'1','2013-11-19 18:32:45'),
 (94,2,4,45,'1','2013-11-19 18:32:49'),
 (95,2,4,46,'1','2013-11-19 19:10:58'),
 (96,2,4,47,'1','2013-11-19 19:11:02'),
 (122,2,5,48,'1','2013-11-19 20:20:03'),
 (123,2,5,49,'1','2013-11-19 21:48:10'),
 (124,2,5,50,'1','2013-11-19 21:49:13'),
 (125,2,5,51,'1','2013-11-19 21:49:18'),
 (126,2,5,52,'1','2013-11-19 21:49:30'),
 (127,2,5,53,'1','2013-11-19 21:49:36'),
 (128,2,5,54,'1','2013-11-19 21:49:41'),
 (129,2,5,55,'1','2013-11-19 21:49:53'),
 (130,2,5,56,'1','2013-11-19 21:49:58'),
 (131,2,5,57,'1','2013-11-19 21:50:08'),
 (132,2,5,58,'1','2013-11-19 21:50:13'),
 (133,2,5,59,'1','2013-11-19 21:50:15'),
 (134,2,5,60,'1','2013-11-19 21:50:22'),
 (135,2,5,61,'1','2013-11-19 21:50:25'),
 (136,2,5,62,'1','2013-11-19 21:50:29'),
 (137,2,6,63,'1','2013-11-19 21:50:34'),
 (138,2,6,64,'1','2013-11-19 21:50:44');
/*!40000 ALTER TABLE `userchapterstatus` ENABLE KEYS */;


--
-- Definition of table `usercontentfeedback`
--

DROP TABLE IF EXISTS `usercontentfeedback`;
CREATE TABLE `usercontentfeedback` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `userId` int(10) unsigned NOT NULL,
  `chapterDetailsId` int(10) unsigned NOT NULL,
  `Feedback` text NOT NULL,
  `DateCreated` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `usercontentfeedback`
--

/*!40000 ALTER TABLE `usercontentfeedback` DISABLE KEYS */;
/*!40000 ALTER TABLE `usercontentfeedback` ENABLE KEYS */;


--
-- Definition of table `userdetails`
--

DROP TABLE IF EXISTS `userdetails`;
CREATE TABLE `userdetails` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `DateCreated` datetime NOT NULL,
  `LastLogin` datetime NOT NULL,
  `firstName` varchar(500) character set utf8 NOT NULL,
  `LastName` varchar(500) character set utf8 NOT NULL,
  `FatherName` varchar(500) default NULL,
  `MotherName` varchar(500) default NULL,
  `EmailAddress` varchar(321) default NULL,
  `MobileNo` varchar(20) default '0',
  `RollNumber` varchar(45) NOT NULL,
  `userType` char(20) character set utf8 NOT NULL,
  `username` char(20) character set utf8 NOT NULL,
  `password` varchar(50) character set utf8 NOT NULL,
  `IsActive` tinyint(3) unsigned NOT NULL default '0',
  `IsEnabled` tinyint(3) unsigned NOT NULL default '0',
  `IsCompleted` tinyint(3) unsigned NOT NULL default '0',
  `IsCertified` tinyint(3) unsigned NOT NULL default '0',
  `VersionRegistered` varchar(3) NOT NULL default '1',
  `IsNewUser` tinyint(3) unsigned NOT NULL default '1',
  PRIMARY KEY  USING BTREE (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `userdetails`
--

/*!40000 ALTER TABLE `userdetails` DISABLE KEYS */;
INSERT INTO `userdetails` (`Id`,`DateCreated`,`LastLogin`,`firstName`,`LastName`,`FatherName`,`MotherName`,`EmailAddress`,`MobileNo`,`RollNumber`,`userType`,`username`,`password`,`IsActive`,`IsEnabled`,`IsCompleted`,`IsCertified`,`VersionRegistered`,`IsNewUser`) VALUES 
 (1,'2013-10-30 12:00:00','2013-10-30 12:00:00','Admin','Admin','F','M','admin@itmonarch.com','0','','Admin','admin','admin@123',1,1,1,1,'yes',1),
 (2,'2013-11-19 22:04:44','2013-11-19 22:04:44','Student 1','Ahmed','F','M','vasim.ahmed@itm.com','8080861670','123','User','student_1','123',1,0,0,0,'1',0),
 (3,'2013-11-19 22:05:58','2013-11-19 22:05:58','Student 2','Student 2','F','M','vasim.ahmed@itm.com','8080861670','123654','User','student_2','student@123',1,1,0,0,'1',1);
/*!40000 ALTER TABLE `userdetails` ENABLE KEYS */;


--
-- Definition of table `userlogger`
--

DROP TABLE IF EXISTS `userlogger`;
CREATE TABLE `userlogger` (
  `userId` int(10) unsigned NOT NULL,
  `LoginDate` datetime NOT NULL,
  `IpAddress` varchar(250) character set utf8 NOT NULL,
  `Id` int(10) unsigned NOT NULL auto_increment,
  PRIMARY KEY  (`Id`),
  KEY `FK_userlogger_1` (`userId`),
  CONSTRAINT `FK_userlogger_1` FOREIGN KEY (`userId`) REFERENCES `userdetails` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=117 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `userlogger`
--

/*!40000 ALTER TABLE `userlogger` DISABLE KEYS */;
INSERT INTO `userlogger` (`userId`,`LoginDate`,`IpAddress`,`Id`) VALUES 
 (1,'2013-11-08 01:56:05','::1',1),
 (1,'2013-11-08 03:29:27','::1',2),
 (1,'2013-11-08 04:01:51','::1',3),
 (1,'2013-11-08 05:08:36','::1',4),
 (1,'2013-11-08 05:09:30','::1',5),
 (1,'2013-11-08 05:37:24','::1',6),
 (1,'2013-11-08 05:48:53','::1',7),
 (1,'2013-11-08 06:24:05','::1',8),
 (2,'2013-11-08 08:06:24','::1',9),
 (2,'2013-11-08 08:15:20','::1',10),
 (2,'2013-11-08 08:28:06','::1',11),
 (3,'2013-11-08 08:52:20','::1',12),
 (2,'2013-11-08 08:58:07','::1',13),
 (3,'2013-11-08 09:00:11','::1',14),
 (2,'2013-11-09 03:24:52','::1',15),
 (2,'2013-11-09 03:52:22','::1',16),
 (2,'2013-11-09 04:10:26','::1',17),
 (2,'2013-11-09 05:42:12','::1',18),
 (2,'2013-11-09 09:01:25','::1',19),
 (2,'2013-11-12 03:17:45','120.138.105.180',20),
 (3,'2013-11-12 03:17:55','120.138.105.180',21),
 (2,'2013-11-12 03:20:28','120.138.105.180',22),
 (2,'2013-11-12 03:25:51','120.138.105.180',23),
 (2,'2013-11-12 03:27:23','120.138.105.180',24),
 (2,'2013-11-12 03:30:23','120.138.105.180',25),
 (6,'2013-11-12 03:32:59','120.138.105.180',26),
 (2,'2013-11-13 01:27:04','::1',27),
 (2,'2013-11-13 02:35:29','::1',28),
 (2,'2013-11-13 04:09:48','::1',29),
 (3,'2013-11-13 04:53:23','::1',30),
 (2,'2013-11-13 05:18:52','::1',31),
 (1,'2013-11-13 06:47:59','::1',32),
 (3,'2013-11-13 06:54:55','::1',33),
 (2,'2013-11-13 06:55:04','::1',34),
 (2,'2013-11-13 06:57:31','::1',35),
 (3,'2013-11-13 07:04:18','::1',36),
 (2,'2013-11-13 08:25:22','::1',37),
 (2,'2013-11-13 08:56:01','::1',38),
 (2,'2013-11-13 09:08:47','::1',39),
 (2,'2013-11-13 09:09:35','::1',40),
 (2,'2013-11-13 09:36:16','::1',41),
 (2,'2013-11-14 01:00:59','::1',42),
 (2,'2013-11-14 02:42:27','::1',43),
 (3,'2013-11-14 03:13:29','::1',44),
 (2,'2013-11-14 05:41:49','::1',45),
 (2,'2013-11-14 06:04:31','::1',46),
 (2,'2013-11-14 06:05:24','::1',47),
 (2,'2013-11-14 06:18:05','::1',48),
 (1,'2013-11-14 06:51:48','::1',49),
 (2,'2013-11-14 06:52:07','::1',50),
 (2,'2013-11-14 07:12:53','::1',51),
 (1,'2013-11-14 07:35:47','::1',52),
 (2,'2013-11-14 08:31:01','::1',53),
 (3,'2013-11-14 08:51:46','::1',54),
 (2,'2013-11-14 08:57:51','::1',55),
 (2,'2013-11-14 09:02:43','::1',56),
 (2,'2013-11-15 12:48:49','::1',57),
 (3,'2013-11-15 12:54:39','::1',58),
 (2,'2013-11-15 12:54:53','::1',59),
 (3,'2013-11-15 12:59:02','::1',60),
 (3,'2013-11-15 01:02:40','::1',61),
 (3,'2013-11-15 03:15:45','::1',62),
 (2,'2013-11-15 03:18:24','::1',63),
 (3,'2013-11-15 03:18:43','::1',64),
 (2,'2013-11-15 04:07:20','::1',65),
 (2,'2013-11-15 04:37:25','::1',66),
 (2,'2013-11-15 04:47:40','::1',67),
 (2,'2013-11-15 04:58:14','::1',68),
 (2,'2013-11-15 05:06:17','::1',69),
 (2,'2013-11-15 05:14:51','::1',70),
 (1,'2013-11-15 05:18:59','::1',71),
 (2,'2013-11-15 05:46:31','::1',72),
 (2,'2013-11-15 06:25:12','::1',73),
 (2,'2013-11-15 06:42:10','::1',74),
 (3,'2013-11-15 07:11:30','::1',75),
 (3,'2013-11-15 07:43:49','::1',76),
 (2,'2013-11-15 07:44:06','::1',77),
 (2,'2013-11-15 07:56:07','::1',78),
 (2,'2013-11-15 08:49:27','::1',79),
 (1,'2013-11-15 09:09:04','127.0.0.1',80),
 (1,'2013-11-15 09:15:19','127.0.0.1',81),
 (1,'2013-11-15 09:37:02','127.0.0.1',82),
 (2,'2013-11-16 12:39:19','127.0.0.1',83),
 (1,'2013-11-16 12:39:43','127.0.0.1',84),
 (2,'2013-11-16 01:40:44','127.0.0.1',85),
 (2,'2013-11-16 02:10:24','127.0.0.1',86),
 (2,'2013-11-16 02:27:16','127.0.0.1',87),
 (1,'2013-11-16 03:21:19','127.0.0.1',88),
 (2,'2013-11-16 03:24:03','127.0.0.1',89),
 (2,'2013-11-16 03:37:52','127.0.0.1',90),
 (1,'2013-11-16 03:43:54','127.0.0.1',91),
 (2,'2013-11-16 03:44:12','127.0.0.1',92),
 (1,'2013-11-16 04:31:22','127.0.0.1',93),
 (1,'2013-11-16 04:47:49','127.0.0.1',94),
 (2,'2013-11-16 04:48:15','127.0.0.1',95),
 (2,'2013-11-16 05:10:24','127.0.0.1',96),
 (2,'2013-11-16 05:12:01','127.0.0.1',97),
 (2,'2013-11-16 05:18:18','127.0.0.1',98),
 (1,'2013-11-16 05:57:33','127.0.0.1',99),
 (1,'2013-11-16 06:02:22','127.0.0.1',100),
 (1,'2013-11-16 06:03:31','127.0.0.1',101),
 (1,'2013-11-16 06:04:23','127.0.0.1',102),
 (1,'2013-11-16 06:14:16','127.0.0.1',103),
 (1,'2013-11-16 06:20:01','127.0.0.1',104),
 (1,'2013-11-16 08:16:08','127.0.0.1',105),
 (2,'2013-11-19 16:50:20','127.0.0.1',106),
 (1,'2013-11-19 17:04:24','127.0.0.1',107),
 (2,'2013-11-19 17:10:54','127.0.0.1',108),
 (1,'2013-11-19 17:16:58','127.0.0.1',109),
 (1,'2013-11-19 18:57:48','127.0.0.1',110),
 (2,'2013-11-19 21:22:11','127.0.0.1',111),
 (1,'2013-11-19 21:22:21','127.0.0.1',112),
 (2,'2013-11-19 21:47:53','127.0.0.1',113),
 (1,'2013-11-19 22:04:26','::1',114),
 (2,'2013-11-19 22:04:57','::1',115),
 (1,'2013-11-19 22:05:33','::1',116);
/*!40000 ALTER TABLE `userlogger` ENABLE KEYS */;


--
-- Definition of table `usertimetracker`
--

DROP TABLE IF EXISTS `usertimetracker`;
CREATE TABLE `usertimetracker` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `userId` int(10) unsigned NOT NULL,
  `chapterId` int(10) unsigned NOT NULL,
  `sectionId` int(10) unsigned NOT NULL,
  `timespent` int(10) unsigned NOT NULL default '0',
  PRIMARY KEY  (`Id`),
  KEY `FK_usertimetracker_1` (`userId`),
  KEY `FK_usertimetracker_2` (`chapterId`),
  CONSTRAINT `FK_usertimetracker_1` FOREIGN KEY (`userId`) REFERENCES `userdetails` (`Id`),
  CONSTRAINT `FK_usertimetracker_2` FOREIGN KEY (`chapterId`) REFERENCES `chapterdetails` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=61 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `usertimetracker`
--

/*!40000 ALTER TABLE `usertimetracker` DISABLE KEYS */;
INSERT INTO `usertimetracker` (`Id`,`userId`,`chapterId`,`sectionId`,`timespent`) VALUES 
 (1,3,1,3,660),
 (2,3,2,1,230),
 (3,2,1,1,1460),
 (4,2,1,2,7320),
 (5,2,1,4,30),
 (6,2,1,3,600),
 (7,2,1,5,3060),
 (8,2,1,6,80),
 (9,2,1,7,130),
 (10,2,1,8,7430),
 (11,3,1,2,23310),
 (12,2,2,1,2580),
 (13,2,2,7,30),
 (14,2,2,9,1080),
 (15,2,2,15,6730),
 (16,1,2,15,410),
 (17,2,2,2,90),
 (18,1,1,2,90),
 (19,2,2,14,190),
 (20,2,2,13,80),
 (21,2,2,11,10),
 (22,2,2,12,10),
 (23,1,2,14,160),
 (24,1,1,1,20),
 (25,1,1,7,10),
 (26,1,2,11,10),
 (27,1,2,12,10),
 (28,1,2,13,10),
 (29,1,2,17,10),
 (30,1,2,18,20),
 (31,2,2,18,490),
 (32,2,2,19,20),
 (33,2,2,20,10),
 (34,2,2,21,130),
 (35,2,2,23,230),
 (36,2,3,24,190),
 (37,2,3,27,10),
 (38,2,3,28,270),
 (39,2,3,29,120),
 (40,2,3,30,10),
 (41,2,3,31,720),
 (42,2,3,32,310),
 (43,2,4,33,50),
 (44,2,4,34,60),
 (45,2,4,35,10),
 (46,2,4,37,170),
 (47,2,4,38,180),
 (48,2,4,40,480),
 (49,2,4,41,310),
 (50,2,4,42,330),
 (51,2,4,43,1710),
 (52,2,4,44,430),
 (53,2,4,47,280),
 (54,2,5,48,5430),
 (55,2,5,62,110),
 (56,2,6,64,100),
 (57,2,6,63,1270),
 (58,2,5,49,60),
 (59,2,5,51,10),
 (60,2,5,54,10);
/*!40000 ALTER TABLE `usertimetracker` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
