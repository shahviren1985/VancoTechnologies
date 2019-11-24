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
-- Create schema today_course
--

CREATE DATABASE IF NOT EXISTS today_course;
USE today_course;

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
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `applicationlogoheader`
--

/*!40000 ALTER TABLE `applicationlogoheader` DISABLE KEYS */;
/*!40000 ALTER TABLE `applicationlogoheader` ENABLE KEYS */;


--
-- Definition of table `archivefinalquizresponse`
--

DROP TABLE IF EXISTS `archivefinalquizresponse`;
CREATE TABLE `archivefinalquizresponse` (
  `userid` int(10) unsigned NOT NULL,
  `QuestionId` int(10) unsigned NOT NULL,
  `userReaponse` text,
  `IsCorrect` tinyint(3) unsigned default '0',
  `DateCreated` timestamp NOT NULL default CURRENT_TIMESTAMP,
  `Id` int(10) NOT NULL auto_increment,
  PRIMARY KEY  (`Id`),
  KEY `FK_archivefinalquizresponse_2` (`QuestionId`),
  KEY `FK_archivefinalquizresponse_3` (`userid`),
  CONSTRAINT `FK_archivefinalquizresponse_1` FOREIGN KEY (`userid`) REFERENCES `userdetails` (`Id`),
  CONSTRAINT `FK_archivefinalquizresponse_2` FOREIGN KEY (`QuestionId`) REFERENCES `finalquizmaster` (`Id`),
  CONSTRAINT `FK_archivefinalquizresponse_3` FOREIGN KEY (`userid`) REFERENCES `userdetails` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `archivefinalquizresponse`
--

/*!40000 ALTER TABLE `archivefinalquizresponse` DISABLE KEYS */;
/*!40000 ALTER TABLE `archivefinalquizresponse` ENABLE KEYS */;


--
-- Definition of table `chapterdetails`
--

DROP TABLE IF EXISTS `chapterdetails`;
CREATE TABLE `chapterdetails` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `CourseId` int(10) unsigned NOT NULL,
  `Language` varchar(100) character set utf8 NOT NULL default 'en',
  `ContentVersion` varchar(3) character set utf8 NOT NULL default '1',
  `PageName` varchar(100) character set utf8 NOT NULL,
  `DateCreated` timestamp NOT NULL default CURRENT_TIMESTAMP,
  `Title` varchar(255) character set utf8 NOT NULL,
  `Link` varchar(255) default NULL,
  `Time` int(10) unsigned NOT NULL,
  `IsDB` tinyint(3) unsigned NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_chapterdetails_1` (`CourseId`),
  CONSTRAINT `FK_chapterdetails_1` FOREIGN KEY (`CourseId`) REFERENCES `coursedetails` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `chapterdetails`
--

/*!40000 ALTER TABLE `chapterdetails` DISABLE KEYS */;
INSERT INTO `chapterdetails` (`Id`,`CourseId`,`Language`,`ContentVersion`,`PageName`,`DateCreated`,`Title`,`Link`,`Time`,`IsDB`) VALUES 
 (1,1,'En','1','DomyFile_0.html','2013-11-03 13:00:00','Computer Basics',NULL,0,0),
 (2,1,'En','1','DomyFile_2.html','2013-11-16 13:00:00','Computer Hardware',NULL,0,0),
 (3,1,'En','1','DomyFile_0.html','2013-11-16 13:00:00','Storage Devices',NULL,0,0),
 (4,1,'En','1','DomyFile_1.html','2013-11-16 13:00:00','Operating System',NULL,0,0),
 (5,1,'En','1','DomyFile_3.html','2013-11-16 13:00:00','File management',NULL,0,0),
 (6,1,'En','1','DomyFile_0.html','2013-11-17 13:00:00','MS Paint',NULL,0,0),
 (7,1,'En','1','DomyFile_2.html','2013-11-17 13:00:00','Notepad',NULL,0,0),
 (8,1,'En','1','DomyFile_2.html','2013-11-17 13:00:00','Word',NULL,0,0),
 (9,1,'En','1','DomyFile_2.html','2013-11-17 13:00:00','Excel',NULL,0,0),
 (10,1,'En','1','DomyFile_2.html','2013-11-17 13:00:00','Power point',NULL,0,0),
 (11,1,'En','1','DomyFile_2.html','2013-11-17 13:00:00','Computer Networks',NULL,0,0),
 (12,1,'En','1','DomyFile_2.html','2013-11-17 13:00:00','Security',NULL,0,0),
 (13,1,'En','1','DomyFile_2.html','2013-11-17 13:00:00','Internet',NULL,0,0),
 (14,1,'En','1','DomyFile_3.html','2013-11-17 13:00:00','Social media',NULL,0,0);
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
) ENGINE=InnoDB AUTO_INCREMENT=354 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `chapterquizmaster`
--

/*!40000 ALTER TABLE `chapterquizmaster` DISABLE KEYS */;
INSERT INTO `chapterquizmaster` (`Id`,`courseId`,`chapterId`,`sectionId`,`questionText`,`IsQuestionOptionPresent`,`QuestionOption`,`AnswerOption`,`ContentVersion`,`DateCreated`,`createdby`) VALUES 
 (1,1,1,1,'Computer components are divided into which two major categories?',0x00,'[]','[{\"AnswerOption\":\"Input and output\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware and Software\",\"IsCurrect\":true},{\"AnswerOption\":\"Processing and Storage\",\"IsCurrect\":false},{\"AnswerOption\":\"Control Unit and Arithmetic Logic Unit\",\"IsCurrect\":false}]','1','2013-11-17 07:34:49','admin'),
 (4,1,1,2,'Which of the following are basic Computer components?',0x00,'[]','[{\"AnswerOption\":\"Control Unit (CU)\",\"IsCurrect\":false},{\"AnswerOption\":\"Arithmetic Logic Unit (ALU)\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-17 08:00:20','admin'),
 (6,1,1,2,'Which component performs arithmetic operations?',0x00,'[]','[{\"AnswerOption\":\"Output\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware\",\"IsCurrect\":false},{\"AnswerOption\":\"Arithmetic Logic Unit (ALU)\",\"IsCurrect\":true},{\"AnswerOption\":\"Control Unit(CU)\",\"IsCurrect\":false}]','1','2013-11-17 08:05:49','admin'),
 (10,1,1,3,'Which are the physical components of a computer system?',0x00,'[]','[{\"AnswerOption\":\"Monitor\",\"IsCurrect\":false},{\"AnswerOption\":\"CPU\",\"IsCurrect\":false},{\"AnswerOption\":\"Keyboard\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-17 08:14:54','admin'),
 (11,1,1,4,'Which is type of software?',0x00,'[]','[{\"AnswerOption\":\"Application software\",\"IsCurrect\":false},{\"AnswerOption\":\"System Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Operating system\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-17 08:17:27','admin'),
 (12,1,1,5,'An interface for a user to communicate with the computer is known as:',0x00,'[]','[{\"AnswerOption\":\"Operating systems\",\"IsCurrect\":true},{\"AnswerOption\":\"utility program\",\"IsCurrect\":false},{\"AnswerOption\":\"Compression software\",\"IsCurrect\":false},{\"AnswerOption\":\"Anti virus\",\"IsCurrect\":false}]','1','2013-11-17 08:21:22','admin'),
 (13,1,1,5,'_______________ programs bridge the gap between the functionality of operating systems and the needs of the users.',0x00,'[]','[{\"AnswerOption\":\"Operating System\",\"IsCurrect\":false},{\"AnswerOption\":\"Utility\",\"IsCurrect\":true},{\"AnswerOption\":\"System Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Application Software\",\"IsCurrect\":false}]','1','2013-11-17 08:22:52','admin'),
 (14,1,1,6,'Application software classification includes:',0x00,'[]','[{\"AnswerOption\":\"Hardware and Software \",\"IsCurrect\":false},{\"AnswerOption\":\"Generalized packages and Customized packages\",\"IsCurrect\":true},{\"AnswerOption\":\"Input and Output\",\"IsCurrect\":false},{\"AnswerOption\":\"System Software and Application Software\",\"IsCurrect\":false}]','1','2013-11-17 08:24:44','admin'),
 (15,1,1,7,'Which of the following is used for Data Analysis?',0x00,'[]','[{\"AnswerOption\":\"Lotus Smart suites\",\"IsCurrect\":false},{\"AnswerOption\":\"Word Perfect\",\"IsCurrect\":false},{\"AnswerOption\":\"Apple Numbers\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and C\",\"IsCurrect\":true}]','1','2013-11-17 08:28:12','admin'),
 (16,1,1,7,'Which is generalized software package?',0x00,'[]','[{\"AnswerOption\":\"Word Processing Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Database Management System\",\"IsCurrect\":false},{\"AnswerOption\":\"Spreadsheets\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-17 08:29:59','admin'),
 (17,1,1,7,'Which is popular operating system?',0x00,'[]','[{\"AnswerOption\":\"Spreadsheets\",\"IsCurrect\":false},{\"AnswerOption\":\"MS-Access\",\"IsCurrect\":false},{\"AnswerOption\":\"Central Processing Unit (CPU)\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":true}]','1','2013-11-17 08:32:55','admin'),
 (18,1,1,8,'Customized software application which meets the specific requirements is known as:',0x00,'[]','[{\"AnswerOption\":\"generalized packages\",\"IsCurrect\":false},{\"AnswerOption\":\"Utility programs\",\"IsCurrect\":false},{\"AnswerOption\":\"Application Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Customized packages\",\"IsCurrect\":true}]','1','2013-11-17 08:35:55','admin'),
 (21,1,2,9,'Which of the following is hardware device?',0x00,'[]','[{\"AnswerOption\":\"Input Device\",\"IsCurrect\":false},{\"AnswerOption\":\"Output Device\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-17 08:44:28','admin'),
 (23,1,2,10,'Which is an input device?',0x00,'[]','[{\"AnswerOption\":\"Microphone (Mic.) for voice as input\",\"IsCurrect\":false},{\"AnswerOption\":\"Optical/magnetic Scanner\",\"IsCurrect\":false},{\"AnswerOption\":\"Touch Screen\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-17 08:48:03','admin'),
 (26,1,2,11,'Which options could be used to create Upper case/Capital characters?',0x00,'[]','[{\"AnswerOption\":\"Press CAPS Lock key\",\"IsCurrect\":false},{\"AnswerOption\":\"Press Shift + alphabet key\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-17 08:55:50','admin'),
 (27,1,2,12,'Which device is used to resize windows?',0x00,'[]','[{\"AnswerOption\":\"Keyboard\",\"IsCurrect\":false},{\"AnswerOption\":\"Mouse\",\"IsCurrect\":true},{\"AnswerOption\":\"Touch Screen\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-17 08:59:56','admin'),
 (29,1,2,12,'When computer shows context menu?',0x00,'[]','[{\"AnswerOption\":\"Whenever user right clicks on screen\",\"IsCurrect\":true},{\"AnswerOption\":\"whenever user left clicks on screen\",\"IsCurrect\":false},{\"AnswerOption\":\"whenever user scroll up or scroll down\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-17 09:06:01','admin'),
 (31,1,2,13,' Which device translates printed images into an electronic format?',0x00,'[]','[{\"AnswerOption\":\"Input devices\",\"IsCurrect\":false},{\"AnswerOption\":\"output devices\",\"IsCurrect\":false},{\"AnswerOption\":\"Image scanner\",\"IsCurrect\":true},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-17 09:10:02','admin'),
 (33,1,2,13,'Which device is used to extract the text from the scanned image?',0x00,'[]','[{\"AnswerOption\":\"Bar code reader\",\"IsCurrect\":false},{\"AnswerOption\":\"Optical character recognition (OCR)\",\"IsCurrect\":true},{\"AnswerOption\":\"Image scanner\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-17 09:28:46','admin'),
 (34,1,2,15,'Microphone is used in:',0x00,'[]','[{\"AnswerOption\":\"Sound recording\",\"IsCurrect\":false},{\"AnswerOption\":\"Video chat\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A or B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-17 09:31:25','admin'),
 (36,1,2,16,'Which device is used to capture the video stream?',0x00,'[]','[{\"AnswerOption\":\"Touch Screen\",\"IsCurrect\":false},{\"AnswerOption\":\"Optical/magnetic Scanner\",\"IsCurrect\":false},{\"AnswerOption\":\"Web Camera\",\"IsCurrect\":true},{\"AnswerOption\":\"Microphone\",\"IsCurrect\":false}]','1','2013-11-17 09:37:21','admin'),
 (37,1,2,17,'Which is an output device?',0x00,'[]','[{\"AnswerOption\":\"Monitor/Display unit\",\"IsCurrect\":false},{\"AnswerOption\":\"Speakers\",\"IsCurrect\":false},{\"AnswerOption\":\"Projector\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-17 09:38:59','admin'),
 (38,1,2,19,'Which monitor uses light emitting diodes for a video display?',0x00,'[]','[{\"AnswerOption\":\"Cathode Ray Tube (CRT)\",\"IsCurrect\":false},{\"AnswerOption\":\"Liquid Crystal Displays (LCD)\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"LED\",\"IsCurrect\":true}]','1','2013-11-17 09:41:35','admin'),
 (40,1,2,19,'High-end monitors can have which of the following  resolutions ?\n\n',0x00,'[]','[{\"AnswerOption\":\"320 x 480\",\"IsCurrect\":false},{\"AnswerOption\":\"800 x 600\",\"IsCurrect\":false},{\"AnswerOption\":\"1600 x 900\",\"IsCurrect\":true},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-17 09:46:43','admin'),
 (41,1,2,20,'Which is type of printer?',0x00,'[]','[{\"AnswerOption\":\"Laser Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"Ink Jet Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"Dot Matrix Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-17 09:48:48','admin'),
 (43,1,2,22,'Projector is:',0x00,'[]','[{\"AnswerOption\":\"Input device\",\"IsCurrect\":false},{\"AnswerOption\":\"Output device\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-17 09:52:17','admin'),
 (44,1,3,27,'Which is temporary memory?',0x00,'[]','[{\"AnswerOption\":\"Secondary Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Primary Memory\",\"IsCurrect\":true},{\"AnswerOption\":\"Real time memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Flash memory\",\"IsCurrect\":false}]','1','2013-11-17 09:57:05','admin'),
 (45,1,3,26,'What is RAM ?\n',0x00,'[]','[{\"AnswerOption\":\"Random Access Memory\",\"IsCurrect\":true},{\"AnswerOption\":\"Recurring Access Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Flash Access Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-17 09:59:18','admin'),
 (46,1,3,27,'The main board of a computer is known as:',0x00,'[]','[{\"AnswerOption\":\"Electric board \",\"IsCurrect\":false},{\"AnswerOption\":\"Black board\",\"IsCurrect\":false},{\"AnswerOption\":\"Mother board\",\"IsCurrect\":true},{\"AnswerOption\":\"White board\",\"IsCurrect\":false}]','1','2013-11-17 10:02:35','admin'),
 (47,1,3,25,'Which Memory is known as Volatile memory?\n\n',0x00,'[]','[{\"AnswerOption\":\"Secondary Storage\",\"IsCurrect\":false},{\"AnswerOption\":\"Hybrid Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Real time memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Primary Storage\",\"IsCurrect\":true}]','1','2013-11-17 10:04:00','admin'),
 (48,1,3,30,'______________ is secondary memory.',0x00,'[]','[{\"AnswerOption\":\"Random Access Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Hybrid Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Compact Disk\",\"IsCurrect\":true},{\"AnswerOption\":\"Real time Memory\",\"IsCurrect\":false}]','1','2013-11-17 10:05:28','admin'),
 (51,1,3,33,'USB is which type of memory?\n\n',0x00,'[]','[{\"AnswerOption\":\"Primary Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Flash Memory\",\"IsCurrect\":true},{\"AnswerOption\":\"Volatile Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Hybrid Memory\",\"IsCurrect\":false}]','1','2013-11-17 10:12:43','admin'),
 (52,1,4,33,'Which software program helps computer hardware to operate with software?',0x00,'[]','[{\"AnswerOption\":\"Word\",\"IsCurrect\":false},{\"AnswerOption\":\"Operating system\",\"IsCurrect\":true},{\"AnswerOption\":\"Games\",\"IsCurrect\":false},{\"AnswerOption\":\"Google\",\"IsCurrect\":false}]','1','2013-11-17 10:29:44','admin'),
 (54,1,4,34,'Which of the following is an operating system?',0x00,'[]','[{\"AnswerOption\":\"Apple Mac OS\",\"IsCurrect\":false},{\"AnswerOption\":\"Google Android\",\"IsCurrect\":false},{\"AnswerOption\":\"Ubuntu Linux\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-17 10:34:14','admin'),
 (55,1,4,35,'Which is not a Microsoft operating system?',0x00,'[]','[{\"AnswerOption\":\"Windows 7\",\"IsCurrect\":false},{\"AnswerOption\":\"Windows XP\",\"IsCurrect\":false},{\"AnswerOption\":\"Solaris\",\"IsCurrect\":true},{\"AnswerOption\":\"Windows 8\",\"IsCurrect\":false}]','1','2013-11-17 10:36:17','admin'),
 (56,1,4,34,'Which of the following system used to manage memory in a computer ? \n',0x00,'[]','[{\"AnswerOption\":\"Memory management\",\"IsCurrect\":true},{\"AnswerOption\":\"Process management\",\"IsCurrect\":false},{\"AnswerOption\":\"Device management\",\"IsCurrect\":false},{\"AnswerOption\":\"File management\",\"IsCurrect\":false}]','1','2013-11-17 10:39:22','admin'),
 (58,1,4,39,'Which option is present in windows start menu?',0x00,'[]','[{\"AnswerOption\":\"My Documents\",\"IsCurrect\":false},{\"AnswerOption\":\"My Recent Documents\",\"IsCurrect\":false},{\"AnswerOption\":\"My Music\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-17 10:43:14','admin'),
 (59,1,4,39,'Which section contain uninstalled option to remove installed software?',0x00,'[]','[{\"AnswerOption\":\"My Documents\",\"IsCurrect\":false},{\"AnswerOption\":\"My Recent Documents\",\"IsCurrect\":false},{\"AnswerOption\":\"My Music\",\"IsCurrect\":false},{\"AnswerOption\":\"Control panel\",\"IsCurrect\":true}]','1','2013-11-17 10:44:45','admin'),
 (61,1,4,46,'Content of hard disk, CD, DVD are displayed in:',0x00,'[]','[{\"AnswerOption\":\"Administrative Tools\",\"IsCurrect\":false},{\"AnswerOption\":\"My Computer\",\"IsCurrect\":true},{\"AnswerOption\":\"Control panel\",\"IsCurrect\":false},{\"AnswerOption\":\"My Document\",\"IsCurrect\":false}]','1','2013-11-17 10:49:33','admin'),
 (62,1,4,34,'Which are the main functions of the operating system ?\n',0x00,'[]','[{\"AnswerOption\":\"Memory management\",\"IsCurrect\":false},{\"AnswerOption\":\"Process management\",\"IsCurrect\":false},{\"AnswerOption\":\"Device management\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-17 10:50:55','admin'),
 (63,1,2,14,'How user provides input through touch screen devices?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Electronic buttons &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Light pen&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of these&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 18:06:58','admin'),
 (64,1,2,18,'Which is type of monitor?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Cathode Ray Tube (CRT)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Light Emitting Diode(LED)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All of the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 18:20:51','admin'),
 (65,1,2,21,'Speakers are:',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Output device&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Input Device&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 18:24:37','admin'),
 (66,1,3,24,'Storage space is expressed in:',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Kilobyte (KB) &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Megabyte (MB)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Gigabyte (GB)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All of the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 18:32:40','admin'),
 (67,1,3,28,'Read Only Memory (ROM) is:',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Primary storage memory.&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Main Memory&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Non-volite memory&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All of the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 18:36:58','admin'),
 (68,1,3,29,' ______________ is not a secondary storage device.',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Pen drives&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;ROM&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 18:42:51','admin'),
 (71,1,3,31,'CD stands for ?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Compact Disk&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Digital Versatile Disc&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 19:00:10','admin'),
 (72,1,3,31,'DVD stands for ?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Compact Disk&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Digital Versatile Disc&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 19:01:05','admin'),
 (73,1,4,37,'Windows start menu appears after pressing windows key.',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;True&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;False&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 19:07:22','admin'),
 (74,1,4,38,'Which option is not present in start menu?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;All Programs&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Control Panel&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Log Off &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 19:11:21','admin'),
 (77,1,4,41,'To quit a program, click on?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Close button&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;File menu and click on Close option&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 19:26:29','admin'),
 (78,1,4,42,'How user gets help on particular topic?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Click on a topic &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Click task to know more about.&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot; Type in a search &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All of the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 19:32:24','admin'),
 (79,1,4,43,'Search option helps in finding:',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Files&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Folders&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 19:59:19','admin'),
 (80,1,4,44,'Operating systems theme could be customized using:',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Control Panel&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;My Documents&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;My Computer&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 20:06:08','admin'),
 (82,1,4,47,'How to lock the windows operating system?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;press ctrl %2b alt %2b del&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;press alt %2b tab %2b del&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 20:13:33','admin'),
 (83,1,6,64,'Which is an application software?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Accounting software&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Office  suites&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Media players &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 20:31:16','admin'),
 (84,1,6,64,'What is used as a digital sketchpad to make pictures? ',0x00,'[]','[{&quot;AnswerOption&quot;:&quot; Toolbar&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Rectangle Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Paint&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 20:32:25','admin'),
 (85,1,6,64,'Which is default active mode on toolbar?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Airbrush mode&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Text mode&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Select mode&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Pencil mode&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 20:33:20','admin'),
 (86,1,6,65,'Which option is available on toolbar?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Magnifier&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Menu Items&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Curve&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and C&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 20:34:32','admin'),
 (87,1,6,65,'What is present at bottom of MS paint program?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;The Curve Line Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Rectangle Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Color Palette&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;The Select Tool&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 20:36:02','admin'),
 (88,1,6,66,'Mouse left click on color palette is used to select:',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Background color&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Foreground color&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of these&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 20:37:43','admin'),
 (89,1,6,66,'Which tool is used to draw multi-sided shapes?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;The Rectangle Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Polygon Tool&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;The Free-Form Select Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 20:39:30','admin'),
 (93,1,6,67,'Which tool is used to enlarge picture?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;The Free-Form Select Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Paint Brush Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Magnifier Tool&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;The Opaque Option Tool&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 20:52:39','admin'),
 (95,1,6,67,' Which tool is used to specify that the existing picture will show through your selection?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;The Rectangle Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Transparent Option Tool&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;The Free-Form Select Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 21:02:28','admin'),
 (96,1,6,68,'MS Paint could save image in _______________ format.',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Bitmap (bmp)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;JPEG (JPG)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Portable Network Graphics (PNG)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 21:03:49','admin'),
 (97,1,5,48,'Hierarchical list of files/folders are displayed in:',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;File Management&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Windows Explorer&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 21:06:32','admin'),
 (98,1,5,54,'Deleted files/folders are placed in _____________.',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Recycle Bin&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Memory&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Desktop&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 21:08:14','admin'),
 (99,1,5,54,'Files/folders deleted from _________ are permanently deleted. ',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Removable storage&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Pen drive&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 21:09:49','admin'),
 (106,1,5,56,'Programs and components are managed using ___________________',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Install Hardware&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Change or remove software&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Add or remove programs&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Install Software&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 21:52:26','admin'),
 (109,1,5,62,' Which mark indicates that device driver is not installed?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Green question mark&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Yellow question mark&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Red question mark&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 22:02:15','admin'),
 (110,1,7,69,'Which is application software?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Office suites &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Graphics software&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Media players&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 22:06:14','admin'),
 (111,1,7,70,'Which program is used to type in the text quickly and easily?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Printer&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Notepad&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;MS Paint&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 22:09:25','admin'),
 (113,1,8,76,'Which application programs allows user to create letter, report etc.?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Microsoft Excel&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Microsoft PowerPoint&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Word processing&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 22:24:15','admin'),
 (114,1,8,78,'New component in Word 2007 which groups similar tasks is known as:',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Tab&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Ribbon&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of these&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 22:25:43','admin'),
 (115,1,8,78,'Which tab have most frequently used tools such as text formatting?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Insert&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Home&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Page Layout&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;View&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 22:26:58','admin'),
 (117,1,8,84,'Which option could be used to scroll the document?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Click on arrow button present on the scroll bar (up arrow button or down arrow button)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Scroll up/down using scroll button present on mouse&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of these&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 22:32:15','admin'),
 (118,1,8,96,'Which short cut keys could be used to move through the document?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;HOME&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;CTRL %2B HOME&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;CTRL %2B END&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All of the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 22:34:31','admin'),
 (119,1,8,101,'How user can change the look and feel of the text in Word 2007?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Inserting Text&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Replacing Text&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Formatting Text&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Deleting Text&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 22:35:53','admin'),
 (121,1,8,101,'Which option determines the emphasis or weight applied to the text?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Font face&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Font style&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Font size&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Alignment&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 22:38:15','admin'),
 (122,1,8,104,'Which of the following action pushes the text down to the next line, but does not create a new paragraph?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Press CTRL %2B TAB keys&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Press SHIFT %2B ENTER keys&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Press CTRL %2B ALT %2B DELETE&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Press SHIFT %2B CTRL %2B TAB&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 22:40:12','admin'),
 (123,1,8,105,'Which setting determines the height of each line of text in the paragraph?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Paragraph spacing&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Line spacing&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Line markers&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 22:41:38','admin'),
 (124,1,8,108,'______________ is used to get readers attention to main points.',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Paragraph spacing&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Line spacing&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Line markers&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Bulleted and Numbered List&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 22:43:43','admin'),
 (125,1,8,109,'____________ stores a copy of text, when user cut or copy a text.',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Document&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;RAM&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Hard disk&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Clipboard &quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 22:45:47','admin'),
 (126,1,8,112,'Which are predefined margins?',0x00,'[]','[{&quot;AnswerOption&quot;:&quot;Narrow&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Moderate&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Mirrored&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 22:47:15','admin'),
 (127,1,3,32,'USB is ______________',0x00,'[]','[{\"AnswerOption\":\"Primary Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Flash Memory\",\"IsCurrect\":true},{\"AnswerOption\":\"Volatile Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Hybrid Memory\",\"IsCurrect\":false}]','1','2013-11-21 05:16:08','admin'),
 (130,1,5,49,'Which shortcut keys are used to open Windows Explorer?',0x00,'[]','[{\"AnswerOption\":\"Window Key + F\",\"IsCurrect\":false},{\"AnswerOption\":\"Window key + E\",\"IsCurrect\":true},{\"AnswerOption\":\"Window Key + C\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-21 05:54:38','admin'),
 (131,1,5,50,'More than one files/folders could be copied from My Documents.',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-21 06:07:43','admin'),
 (132,1,9,119,'Which is widely used spreadsheet applications?',0x00,'[]','[{\"AnswerOption\":\"Microsoft word\",\"IsCurrect\":false},{\"AnswerOption\":\"Microsoft excel\",\"IsCurrect\":true},{\"AnswerOption\":\"Microsoft  PowerPoint\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-21 06:15:35','admin'),
 (133,1,9,120,' ___________ is feature of spreadsheet.',0x00,'[]','[{\"AnswerOption\":\"List AutoFill\",\"IsCurrect\":false},{\"AnswerOption\":\"Pivot Table\",\"IsCurrect\":false},{\"AnswerOption\":\"Drag and Drop\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 06:17:03','admin'),
 (134,1,5,51,'Which file details are associated about the file? ',0x00,'[]','[{\"AnswerOption\":\"Name\",\"IsCurrect\":false},{\"AnswerOption\":\"Type\",\"IsCurrect\":false},{\"AnswerOption\":\"Size \",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 06:17:53','admin'),
 (135,1,9,121,'Which is feature of MS EXCEL 2007?',0x00,'[]','[{\"AnswerOption\":\"Office themes and Excel sheets\",\"IsCurrect\":false},{\"AnswerOption\":\"Sorting and Filtering\",\"IsCurrect\":false},{\"AnswerOption\":\"Formulas\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 06:18:37','admin'),
 (136,1,9,122,'Predefined set of colors, lines etc. is known as _____________.',0x00,'[]','[{\"AnswerOption\":\"Styles\",\"IsCurrect\":false},{\"AnswerOption\":\"Sorting\",\"IsCurrect\":false},{\"AnswerOption\":\"Themes\",\"IsCurrect\":true},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-21 06:21:07','admin'),
 (137,1,9,124,'______________ feature helps to write the formula syntax quickly.',0x00,'[]','[{\"AnswerOption\":\"Office themes and Excel sheets\",\"IsCurrect\":false},{\"AnswerOption\":\"Sorting and Filtering\",\"IsCurrect\":false},{\"AnswerOption\":\"Formulas\",\"IsCurrect\":false},{\"AnswerOption\":\"Function AutoComplete\",\"IsCurrect\":true}]','1','2013-11-21 06:22:23','admin'),
 (138,1,9,131,'Which different type of values could be entered in cell?',0x00,'[]','[{\"AnswerOption\":\"Numbers\",\"IsCurrect\":false},{\"AnswerOption\":\"Text\",\"IsCurrect\":false},{\"AnswerOption\":\"Date and Time\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 06:23:52','admin'),
 (139,1,5,52,' A new folder is displayed with the default name as_____.',0x00,'[]','[{\"AnswerOption\":\"My Computer\",\"IsCurrect\":false},{\"AnswerOption\":\"New Folder\",\"IsCurrect\":true},{\"AnswerOption\":\"My Picture\",\"IsCurrect\":false},{\"AnswerOption\":\"Folder\",\"IsCurrect\":false}]','1','2013-11-21 06:24:13','admin'),
 (141,1,9,144,'Which option is used to view the worksheet before printout is taken?',0x00,'[]','[{\"AnswerOption\":\"Modifying a worksheet\",\"IsCurrect\":false},{\"AnswerOption\":\"Print preview\",\"IsCurrect\":true},{\"AnswerOption\":\"Format Cells\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 06:28:00','admin'),
 (142,1,9,147,'Which dialog box allows formatting style & numbers?',0x00,'[]','[{\"AnswerOption\":\"Modifying a worksheet\",\"IsCurrect\":false},{\"AnswerOption\":\"Save\",\"IsCurrect\":false},{\"AnswerOption\":\"Print\",\"IsCurrect\":false},{\"AnswerOption\":\"Format Cells\",\"IsCurrect\":true},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-21 06:30:20','admin'),
 (143,1,5,53,'Which shortcut key is used to rename a file/folder?',0x00,'[]','[{\"AnswerOption\":\"F2\",\"IsCurrect\":true},{\"AnswerOption\":\"F5\",\"IsCurrect\":false},{\"AnswerOption\":\"F11\",\"IsCurrect\":false},{\"AnswerOption\":\"F12\",\"IsCurrect\":false}]','1','2013-11-21 06:30:40','admin'),
 (144,1,9,148,'Which tabs are present under Format Cells dialog box?',0x00,'[]','[{\"AnswerOption\":\"Number\",\"IsCurrect\":false},{\"AnswerOption\":\"Protection\",\"IsCurrect\":false},{\"AnswerOption\":\"Border and Patterns\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 06:32:08','admin'),
 (145,1,9,154,'Which feature is used to manipulate data in Excel worksheets?',0x00,'[]','[{\"AnswerOption\":\"Formulas and worksheet\",\"IsCurrect\":true},{\"AnswerOption\":\"Border and Patterns\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 06:33:46','admin'),
 (147,1,9,157,'___________ are used to present data in visual format.',0x00,'[]','[{\"AnswerOption\":\"Format Cells\",\"IsCurrect\":false},{\"AnswerOption\":\"Charts\",\"IsCurrect\":true},{\"AnswerOption\":\"Formulas and Functions\",\"IsCurrect\":false},{\"AnswerOption\":\"Modifying a Worksheet\",\"IsCurrect\":false}]','1','2013-11-21 06:40:56','admin'),
 (148,1,9,158,'Which is one of the chart type?',0x00,'[]','[{\"AnswerOption\":\"Bubble chart\",\"IsCurrect\":false},{\"AnswerOption\":\"Doughnut chart\",\"IsCurrect\":false},{\"AnswerOption\":\"Line chart\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 06:43:32','admin'),
 (151,1,10,170,'Which of the following are main advantages of PowerPoint:\r\n',0x00,'[]','[{\"AnswerOption\":\"PowerPoint gives you several ways to create a presentation.\",\"IsCurrect\":false},{\"AnswerOption\":\"Adding text will help you put your ideas into words.\",\"IsCurrect\":false},{\"AnswerOption\":\"The multimedia features makes your slides sparkle. You can add clip art, sound effects, music, video clips\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 06:50:42','admin'),
 (152,1,10,172,'________ provides ideas/templates for variety of presentation types.',0x00,'[]','[{\"AnswerOption\":\"Installed templates\",\"IsCurrect\":true},{\"AnswerOption\":\"Design templates\",\"IsCurrect\":false},{\"AnswerOption\":\"Blank presentations\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide Layout\",\"IsCurrect\":false}]','1','2013-11-21 06:52:26','admin'),
 (153,1,10,175,'Which of the following are Slide Layouts?',0x00,'[]','[{\"AnswerOption\":\"Title Slide\",\"IsCurrect\":false},{\"AnswerOption\":\"Section Header\",\"IsCurrect\":false},{\"AnswerOption\":\"Text Content\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 07:11:01','admin'),
 (154,1,10,176,'What are different types of content?',0x00,'[]','[{\"AnswerOption\":\"Media Clip\",\"IsCurrect\":false},{\"AnswerOption\":\"Chart\",\"IsCurrect\":false},{\"AnswerOption\":\"SmartArt Graphic\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 07:12:27','admin'),
 (155,1,5,55,'Hidden files/folders could be made visible using folder options.',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-21 07:12:35','admin'),
 (156,1,10,178,'Which option helps in creation of presentation slides?',0x00,'[]','[{\"AnswerOption\":\"Normal\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide Sorter\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide Show\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 07:24:03','admin'),
 (157,1,10,181,'Slide show is used to _____.',0x00,'[]','[{\"AnswerOption\":\"Normal\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide Sorter\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide Show\",\"IsCurrect\":true},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-21 07:29:25','admin'),
 (158,1,10,189,'Which tool is moved from one slide to another slide?',0x00,'[]','[{\"AnswerOption\":\"Scroll Bars\",\"IsCurrect\":false},{\"AnswerOption\":\"Next Slide and Previous Slide Buttons\",\"IsCurrect\":false},{\"AnswerOption\":\"Using Outline Pane\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 07:32:14','admin'),
 (160,1,10,200,'Which option is present in Print dialog box?',0x00,'[]','[{\"AnswerOption\":\"Print range\",\"IsCurrect\":false},{\"AnswerOption\":\"Copies\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 07:35:29','admin'),
 (161,1,12,220,'Which security tools make computer/information more secure?',0x00,'[]','[{\"AnswerOption\":\"Antivirus\",\"IsCurrect\":false},{\"AnswerOption\":\"Firewall\",\"IsCurrect\":false},{\"AnswerOption\":\"Security Essentials\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 07:42:04','admin'),
 (162,1,5,57,'User gets an option to choose location while installing a software.',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-21 07:42:36','admin'),
 (163,1,12,220,'_________ is the practice of defending recording or desctruction.',0x00,'[]','[{\"AnswerOption\":\"Antivirus Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Firewall\",\"IsCurrect\":false},{\"AnswerOption\":\"Computer security\",\"IsCurrect\":true},{\"AnswerOption\":\"Security essentials\",\"IsCurrect\":false}]','1','2013-11-21 07:43:44','admin'),
 (164,1,12,221,'_______ is computer program which interfere with computer operation.',0x00,'[]','[{\"AnswerOption\":\"Antivirus Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Computer viruses\",\"IsCurrect\":true},{\"AnswerOption\":\"Security essentials\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 07:45:04','admin'),
 (165,1,5,58,'Which buttons are present in add remove program window?',0x00,'[]','[{\"AnswerOption\":\"Change\",\"IsCurrect\":false},{\"AnswerOption\":\"Remove\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 07:46:16','admin'),
 (166,1,12,221,'_______ is computer program ......... viruses and worms.',0x00,'[]','[{\"AnswerOption\":\"Antivirus Softwares\",\"IsCurrect\":true},{\"AnswerOption\":\"Computer viruses\",\"IsCurrect\":false},{\"AnswerOption\":\"Security essentials\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 07:46:36','admin'),
 (167,1,12,222,'____ is popular freely available antivirus program.',0x00,'[]','[{\"AnswerOption\":\"Microsoft Security Essentials\",\"IsCurrect\":false},{\"AnswerOption\":\"AVG\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 07:47:55','admin'),
 (169,1,12,223,'This is best practice:',0x00,'[]','[{\"AnswerOption\":\"Avoid visiting software key generator related websites.\",\"IsCurrect\":false},{\"AnswerOption\":\"Use antivirus programs \",\"IsCurrect\":false},{\"AnswerOption\":\"Do not install more than one antivirus software\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-21 07:53:37','admin'),
 (170,1,5,59,'Windows features could be added/removed from Add remove programs.',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-21 07:53:38','admin'),
 (171,1,12,224,'Which program provides defense against people or programs?',0x00,'[]','[{\"AnswerOption\":\"Antivirus Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Firewall\",\"IsCurrect\":true},{\"AnswerOption\":\"Computer security\",\"IsCurrect\":false},{\"AnswerOption\":\"Security essentials\",\"IsCurrect\":false}]','1','2013-11-21 07:56:18','admin'),
 (173,1,5,60,'Which hardware devices could be installed using Windows operating system?',0x00,'[]','[{\"AnswerOption\":\"sound card\",\"IsCurrect\":false},{\"AnswerOption\":\"network card\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 07:58:20','admin'),
 (174,1,12,227,'_______ is free anti-malware software for your computer.',0x00,'[]','[{\"AnswerOption\":\"Antivirus Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Firewall\",\"IsCurrect\":false},{\"AnswerOption\":\"Computer security\",\"IsCurrect\":false},{\"AnswerOption\":\"Security essentials\",\"IsCurrect\":true}]','1','2013-11-21 07:59:29','admin'),
 (175,1,12,228,'____________ is a diagnostic mode of an operating system.',0x00,'[]','[{\"AnswerOption\":\"Antivirus Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Firewall\",\"IsCurrect\":false},{\"AnswerOption\":\"Computer security\",\"IsCurrect\":false},{\"AnswerOption\":\"Safe mode\",\"IsCurrect\":true}]','1','2013-11-21 08:00:53','admin'),
 (176,1,5,61,'Which shortcut keys are used to search files/folders in computer?',0x00,'[]','[{\"AnswerOption\":\"Window Key + F\",\"IsCurrect\":true},{\"AnswerOption\":\"Window Key + D\",\"IsCurrect\":false},{\"AnswerOption\":\"Window Key + E\",\"IsCurrect\":false},{\"AnswerOption\":\"Window Key + V\",\"IsCurrect\":false}]','1','2013-11-21 08:10:25','admin'),
 (177,1,11,204,'_____________ consists of two or more computers that are linked in order to share resources.',0x00,'[]','[{\"AnswerOption\":\"Servers\",\"IsCurrect\":false},{\"AnswerOption\":\"Computer Networks\",\"IsCurrect\":true},{\"AnswerOption\":\"Protocols\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 08:15:05','admin'),
 (178,1,11,204,'Which options are used to link the computers on a network?',0x00,'[]','[{\"AnswerOption\":\"Satellites\",\"IsCurrect\":false},{\"AnswerOption\":\"Bluetooth\",\"IsCurrect\":false},{\"AnswerOption\":\"Cables\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 08:16:28','admin'),
 (179,1,11,205,'__________ is limited to a geographic area such as a computer lb, school etc.',0x00,'[]','[{\"AnswerOption\":\"Servers\",\"IsCurrect\":false},{\"AnswerOption\":\"Workstations\",\"IsCurrect\":false},{\"AnswerOption\":\"Local Area Network\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 08:18:23','admin'),
 (180,1,11,205,'Which of these statements is true ?',0x00,'[]','[{\"AnswerOption\":\"Servers tend to be more powerful than workstations.\",\"IsCurrect\":false},{\"AnswerOption\":\"Workstations tend to be more powerful than servers.\",\"IsCurrect\":false},{\"AnswerOption\":\"Workstations are any device through which human user can interact to utilize the network resources.\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and C\",\"IsCurrect\":true}]','1','2013-11-21 08:20:02','admin'),
 (181,1,11,205,'Which of the following is used to connect networks in larger geographic areas, such as India or the world.?',0x00,'[]','[{\"AnswerOption\":\"Local Area Network (LAN)\",\"IsCurrect\":false},{\"AnswerOption\":\"Wide Area Network (WAN) \",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 08:21:37','admin'),
 (182,1,4,45,'Option to modify my computer settings is present in : ',0x00,'[]','[{\"AnswerOption\":\"Administrative Tools\",\"IsCurrect\":false},{\"AnswerOption\":\"Control Panel\",\"IsCurrect\":true},{\"AnswerOption\":\"Desktop\",\"IsCurrect\":false},{\"AnswerOption\":\"My Document\",\"IsCurrect\":false}]','1','2013-11-21 08:23:16','admin'),
 (183,1,11,206,'__________ provides communication between two or more devices located in same city.',0x00,'[]','[{\"AnswerOption\":\"Servers\",\"IsCurrect\":false},{\"AnswerOption\":\"Metropolitan area network (MAN) \",\"IsCurrect\":true},{\"AnswerOption\":\"Protocols\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 08:26:56','admin'),
 (184,1,11,210,'What is the purpose of protocol?',0x00,'[]','[{\"AnswerOption\":\"To Compress the Data\",\"IsCurrect\":false},{\"AnswerOption\":\"To indicate that sender has finished sending a message\",\"IsCurrect\":false},{\"AnswerOption\":\"To indicate that receiver has received a message\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 08:28:34','admin'),
 (185,1,11,210,'___________ is type of protocol.',0x00,'[]','[{\"AnswerOption\":\"Simple mail transport Protocol (SMTP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Transmission control Protocol (TCP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 08:30:04','admin'),
 (186,1,11,209,'______ is an addressing protocol.',0x00,'[]','[{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Hyper Text Transfer Protocol (HTTP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Internet Protocol (IP)\",\"IsCurrect\":true},{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":false}]','1','2013-11-21 08:32:09','admin'),
 (187,1,11,210,'_________ is used to receive incoming e-mail.',0x00,'[]','[{\"AnswerOption\":\"Simple mail transport Protocol (SMTP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Transmission control Protocol (TCP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":true},{\"AnswerOption\":\"Internet Protocol (IP)\",\"IsCurrect\":false}]','1','2013-11-21 08:34:43','admin'),
 (189,1,11,211,'____________ is based on the client/server principles.',0x00,'[]','[{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Hyper Text Transfer Protocol (HTTP)\",\"IsCurrect\":true},{\"AnswerOption\":\"Internet Protocol (IP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":false}]','1','2013-11-21 08:38:58','admin'),
 (190,1,11,213,'_______ contains a series of four numbers unique to the computer in a network.',0x00,'[]','[{\"AnswerOption\":\"Protocols\",\"IsCurrect\":false},{\"AnswerOption\":\"Workstations\",\"IsCurrect\":false},{\"AnswerOption\":\"IP address\",\"IsCurrect\":true},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-21 08:40:26','admin'),
 (191,1,6,64,'Paint is like a digital sketchpad to make simple pictures or add text.',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-21 08:40:28','admin'),
 (192,1,11,214,'Which of the following is a way to connect to a printer on a network?',0x00,'[]','[{\"AnswerOption\":\"Find a printer in the directory\",\"IsCurrect\":false},{\"AnswerOption\":\"Connect to this printer(Or to browse for a printer)\",\"IsCurrect\":false},{\"AnswerOption\":\"Connect to a printer on the Internet or on a home or office network.\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 08:42:04','admin'),
 (193,1,11,219,'Which permission offers an option to change the permission settings for a printer?',0x00,'[]','[{\"AnswerOption\":\"Manage Printers\",\"IsCurrect\":true},{\"AnswerOption\":\"Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 08:44:33','admin'),
 (195,1,11,211,' _______ is used to transfer a hypertext between two or more computers.',0x00,'[]','[{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Hyper Text Transfer Protocol (HTTP)\",\"IsCurrect\":true},{\"AnswerOption\":\"Internet Protocol (IP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":false}]','1','2013-11-21 08:48:52','admin'),
 (196,1,13,232,'Which software is used to retrieve, present information from the world wide web (WWW)?',0x00,'[]','[{\"AnswerOption\":\"Downloads & installations\",\"IsCurrect\":false},{\"AnswerOption\":\"Protocol & security\",\"IsCurrect\":false},{\"AnswerOption\":\"Web Browsers\",\"IsCurrect\":true},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-21 08:54:01','admin'),
 (197,1,13,232,'What is  the primary purpose of a web browser?',0x00,'[]','[{\"AnswerOption\":\"To bring information resources to the user\",\"IsCurrect\":false},{\"AnswerOption\":\"To allow them users to view the information\",\"IsCurrect\":false},{\"AnswerOption\":\"To access other information.\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 08:55:55','admin'),
 (198,1,13,232,'______ identifies a resource to be retrieved over the Secure HTTP protocol.',0x00,'[]','[{\"AnswerOption\":\"FILE\",\"IsCurrect\":false},{\"AnswerOption\":\"HTTP\",\"IsCurrect\":false},{\"AnswerOption\":\"HTTPS\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 08:58:09','admin'),
 (199,1,13,232,'________ identifies and attempts to open local resource in web browser.',0x00,'[]','[{\"AnswerOption\":\"FILE\",\"IsCurrect\":true},{\"AnswerOption\":\"HTTP\",\"IsCurrect\":false},{\"AnswerOption\":\"HTTPS\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 08:59:32','admin'),
 (200,1,7,71,'Which shortcut key is used to save the content of notepad?',0x00,'[]','[{\"AnswerOption\":\"Ctrl + D\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl + S\",\"IsCurrect\":true},{\"AnswerOption\":\"Ctrl + E\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl + R\",\"IsCurrect\":false}]','1','2013-11-21 08:59:47','admin'),
 (201,1,13,233,'Which one is popular web browser?',0x00,'[]','[{\"AnswerOption\":\"Google chrome\",\"IsCurrect\":false},{\"AnswerOption\":\"Microsoft Internet Explorer\",\"IsCurrect\":false},{\"AnswerOption\":\"Apple Safari\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 09:01:00','admin'),
 (202,1,13,233,'_______ is web browser provided by Microsoft.',0x00,'[]','[{\"AnswerOption\":\"Google chrome\",\"IsCurrect\":false},{\"AnswerOption\":\"Internet Explorer\",\"IsCurrect\":true},{\"AnswerOption\":\"Apple Safari\",\"IsCurrect\":false},{\"AnswerOption\":\"Mozilla Firefox\",\"IsCurrect\":false}]','1','2013-11-21 09:02:34','admin'),
 (203,1,13,234,'Which is user interface element in the web browsers?',0x00,'[]','[{\"AnswerOption\":\"Status bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Search bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Home button\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 09:04:16','admin'),
 (204,1,13,235,'Which of the following browsers provides additional options to make browsing experience safe, secure, private and efficient?\n',0x00,'[]','[{\"AnswerOption\":\"Google chrome\",\"IsCurrect\":false},{\"AnswerOption\":\"Microsoft Internet Explorer\",\"IsCurrect\":false},{\"AnswerOption\":\"Mozilla Firefox\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 09:05:25','admin'),
 (205,1,7,72,'Which shortcut key is used to print the notepad contents?',0x00,'[]','[{\"AnswerOption\":\"Ctrl + V\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl + S\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl + P\",\"IsCurrect\":true},{\"AnswerOption\":\"Ctrl + R\",\"IsCurrect\":false}]','1','2013-11-21 09:05:38','admin'),
 (206,1,13,235,'Which task is performed using internet options?',0x00,'[]','[{\"AnswerOption\":\"Advance configuration options \",\"IsCurrect\":false},{\"AnswerOption\":\"Change the security settings\",\"IsCurrect\":false},{\"AnswerOption\":\"Set the content privacy options\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 09:06:34','admin'),
 (207,1,13,237,'HTTP is based on __________.',0x00,'[]','[{\"AnswerOption\":\"Input/output devices\",\"IsCurrect\":false},{\"AnswerOption\":\"TCP/IP Protocols\",\"IsCurrect\":false},{\"AnswerOption\":\"Client/server principles\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 09:08:48','admin'),
 (208,1,7,73,'Which option allows user to browse to existing document?',0x00,'[]','[{\"AnswerOption\":\"Open Window\",\"IsCurrect\":true},{\"AnswerOption\":\"Save Window\",\"IsCurrect\":false},{\"AnswerOption\":\"Print Window\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 09:11:26','admin'),
 (209,1,13,237,'Which icon is used in web browsers to indicate invalid/expired certificates?',0x00,'[]','[{\"AnswerOption\":\"Green lock icons\",\"IsCurrect\":false},{\"AnswerOption\":\"Red lock icons\",\"IsCurrect\":true},{\"AnswerOption\":\"Blue lock icons\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 09:11:47','admin'),
 (210,1,13,238,'What are the two section of any email message?\n',0x00,'[]','[{\"AnswerOption\":\"Input and Output\",\"IsCurrect\":false},{\"AnswerOption\":\"Header and Body\",\"IsCurrect\":true},{\"AnswerOption\":\"Protocol & security\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-21 09:13:07','admin'),
 (211,1,7,74,'Which is one of the font styles?',0x00,'[]','[{\"AnswerOption\":\"Oblique\",\"IsCurrect\":false},{\"AnswerOption\":\"Bold\",\"IsCurrect\":false},{\"AnswerOption\":\"Regular\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 09:13:11','admin'),
 (212,1,7,75,' _______ does not affect the printing.',0x00,'[]','[{\"AnswerOption\":\"Font\",\"IsCurrect\":false},{\"AnswerOption\":\"Word Wrap\",\"IsCurrect\":true},{\"AnswerOption\":\"Print\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 09:13:59','admin'),
 (213,1,13,241,'User unwanted mails are called as _______.',0x00,'[]','[{\"AnswerOption\":\"Electronic emails\",\"IsCurrect\":false},{\"AnswerOption\":\"Spam emails\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 09:16:30','admin'),
 (214,1,13,242,'Criminal gets access to your computer using __________.',0x00,'[]','[{\"AnswerOption\":\"SPAM Emails\",\"IsCurrect\":false},{\"AnswerOption\":\"Social Engineering Emails\",\"IsCurrect\":true},{\"AnswerOption\":\"Search Engines\",\"IsCurrect\":false},{\"AnswerOption\":\"Protocol  and Security\",\"IsCurrect\":false}]','1','2013-11-21 09:17:54','admin'),
 (215,1,13,244,'_________ is designed to search an information on the World Wide Web.',0x00,'[]','[{\"AnswerOption\":\"Web search engine \",\"IsCurrect\":true},{\"AnswerOption\":\"Web browsers\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 09:20:13','admin'),
 (216,1,13,247,'User shall avoid downloading/installing _____________ applications.',0x00,'[]','[{\"AnswerOption\":\"Toolbars\",\"IsCurrect\":false},{\"AnswerOption\":\"Smiley\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 09:22:51','admin'),
 (217,1,14,251,'How to keep your passwords secret?\n',0x00,'[]','[{\"AnswerOption\":\"Stop accessing websites\",\"IsCurrect\":false},{\"AnswerOption\":\"Never provide your password over email or in response to an email request\",\"IsCurrect\":true},{\"AnswerOption\":\"Never meet any person\",\"IsCurrect\":false},{\"AnswerOption\":\"Allow your account to be create anyone of your friend\",\"IsCurrect\":false}]','1','2013-11-21 09:25:32','admin'),
 (218,1,14,251,'How to avoid online fraud ?\n',0x00,'[]','[{\"AnswerOption\":\"Don’t secure your password\",\"IsCurrect\":false},{\"AnswerOption\":\"Use only secure sites. \",\"IsCurrect\":true},{\"AnswerOption\":\"Give your personal information\",\"IsCurrect\":false},{\"AnswerOption\":\"None any of the above\",\"IsCurrect\":false}]','1','2013-11-21 09:27:05','admin'),
 (219,1,4,40,'To view content in start menu, click on?',0x00,'[]','[{\"AnswerOption\":\"All Programs\",\"IsCurrect\":false},{\"AnswerOption\":\"Windows start button\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 09:47:29','admin'),
 (220,1,8,77,'MS Word allows user to:',0x00,'[]','[{\"AnswerOption\":\"Creating a word document\",\"IsCurrect\":false},{\"AnswerOption\":\"Create table of contents\",\"IsCurrect\":false},{\"AnswerOption\":\"Print out multiple pages on a single sheet of paper\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-21 10:03:21','admin'),
 (221,1,8,79,'Word 2007 screen could have _______________',0x00,'[]','[{\"AnswerOption\":\"Objects\",\"IsCurrect\":false},{\"AnswerOption\":\"Tabs\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:08:17','admin'),
 (222,1,8,80,'In word 2007, the main options are represented as:',0x00,'[]','[{\"AnswerOption\":\"Tabs\",\"IsCurrect\":true},{\"AnswerOption\":\"Table\",\"IsCurrect\":false},{\"AnswerOption\":\"Rows\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:09:47','admin'),
 (223,1,8,81,'In Word 2007, Is it possible to customize quick access toolbar?',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-21 10:11:19','admin'),
 (224,1,8,82,'In Word 2007, ruler represents:',0x00,'[]','[{\"AnswerOption\":\"Width\",\"IsCurrect\":false},{\"AnswerOption\":\"Height\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:14:04','admin'),
 (225,1,8,83,'What is permanent part of the typing area?',0x00,'[]','[{\"AnswerOption\":\"Insertion Point \",\"IsCurrect\":false},{\"AnswerOption\":\"Mouse Pointer \",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None the above\",\"IsCurrect\":false}]','1','2013-11-21 10:16:40','admin');
INSERT INTO `chapterquizmaster` (`Id`,`courseId`,`chapterId`,`sectionId`,`questionText`,`IsQuestionOptionPresent`,`QuestionOption`,`AnswerOption`,`ContentVersion`,`DateCreated`,`createdby`) VALUES 
 (226,1,8,85,'Which option is present under Office button?',0x00,'[]','[{\"AnswerOption\":\"Create new documents\",\"IsCurrect\":false},{\"AnswerOption\":\"Open existing documents\",\"IsCurrect\":false},{\"AnswerOption\":\"Save documents in Word\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-21 10:19:41','admin'),
 (227,1,8,86,'Shortcut key to create new documents in MS-Word ?',0x00,'[]','[{\"AnswerOption\":\"Ctrl %2B Z\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B U\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B N\",\"IsCurrect\":true}]','1','2013-11-21 10:21:48','admin'),
 (228,1,8,87,'Which short cut keys are used to open an existing word document?',0x00,'[]','[{\"AnswerOption\":\"Ctrl %2B Z\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B X\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B O\",\"IsCurrect\":true}]','1','2013-11-21 10:23:09','admin'),
 (229,1,8,88,'Which short cut keys are used to save a word document?',0x00,'[]','[{\"AnswerOption\":\"Ctrl %2B V\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B S\",\"IsCurrect\":true},{\"AnswerOption\":\"Ctrl %2B Z\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B X\",\"IsCurrect\":false}]','1','2013-11-21 10:24:21','admin'),
 (230,1,8,89,'Look in option is present under:',0x00,'[]','[{\"AnswerOption\":\"In the Open dialog box\",\"IsCurrect\":true},{\"AnswerOption\":\"In the Close dialog box\",\"IsCurrect\":false},{\"AnswerOption\":\"In the footer\",\"IsCurrect\":false}]','1','2013-11-21 10:28:49','admin'),
 (231,1,8,90,'MS Word 2007 has close option  _____________',0x00,'[]','[{\"AnswerOption\":\"In the Open dialog box\",\"IsCurrect\":false},{\"AnswerOption\":\"Inside Office button\",\"IsCurrect\":true},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:30:30','admin'),
 (232,1,8,91,'Word 2007 could print documents on:',0x00,'[]','[{\"AnswerOption\":\"Single page \",\"IsCurrect\":false},{\"AnswerOption\":\"Multiple page\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B \",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:32:48','admin'),
 (233,1,8,92,'Exit command is present _________ in MS-Word.',0x00,'[]','[{\"AnswerOption\":\"Inside Office button\",\"IsCurrect\":true},{\"AnswerOption\":\"Inside header\",\"IsCurrect\":false},{\"AnswerOption\":\"Inside footer\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:34:50','admin'),
 (234,1,8,93,'Shortcut key for print preview:',0x00,'[]','[{\"AnswerOption\":\"Ctrl  %2B O\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B F10\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B F2\",\"IsCurrect\":true},{\"AnswerOption\":\"Ctrl %2B Z\",\"IsCurrect\":false}]','1','2013-11-21 10:36:58','admin'),
 (235,1,8,94,'Text section contain ?',0x00,'[]','[{\"AnswerOption\":\"Common word concepts\",\"IsCurrect\":false},{\"AnswerOption\":\"Tips\",\"IsCurrect\":false},{\"AnswerOption\":\"Commands\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-21 10:39:40','admin'),
 (236,1,8,94,'User could type text in:',0x00,'[]','[{\"AnswerOption\":\"Text area\",\"IsCurrect\":true},{\"AnswerOption\":\"Footer area\",\"IsCurrect\":false},{\"AnswerOption\":\"Header area\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:41:28','admin'),
 (237,1,8,97,'Spacebar is used as __________.',0x00,'[]','[{\"AnswerOption\":\"Creator\",\"IsCurrect\":false},{\"AnswerOption\":\"Separator\",\"IsCurrect\":true},{\"AnswerOption\":\" All of the above\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:43:53','admin'),
 (238,1,8,98,'How to select the whole word in Word 2007?',0x00,'[]','[{\"AnswerOption\":\"Single-click within the word\",\"IsCurrect\":false},{\"AnswerOption\":\"Double-click within the word\",\"IsCurrect\":true},{\"AnswerOption\":\"Remove word\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:46:03','admin'),
 (239,1,8,99,'How to delete a word in Word 2007?',0x00,'[]','[{\"AnswerOption\":\"Using backspace\",\"IsCurrect\":false},{\"AnswerOption\":\"Pressing delete button\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:47:46','admin'),
 (240,1,8,100,'Is it possible to replace a word without selecting it in Word 2007?',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":false},{\"AnswerOption\":\"False\",\"IsCurrect\":true}]','1','2013-11-21 10:50:26','admin'),
 (241,1,8,102,'Which section of Word 2007 has \"Format painter\"?',0x00,'[]','[{\"AnswerOption\":\"Clipboard\",\"IsCurrect\":false},{\"AnswerOption\":\"Home tab\",\"IsCurrect\":true},{\"AnswerOption\":\"Footer\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 01:37:02','admin'),
 (242,1,8,103,'Which section of Word 2007 has \"Format paragraph\" option?',0x00,'[]','[{\"AnswerOption\":\"Footer\",\"IsCurrect\":false},{\"AnswerOption\":\"Header\",\"IsCurrect\":false},{\"AnswerOption\":\"Context menu\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 01:39:15','admin'),
 (243,1,8,106,'Paragraph spacing enables user to give a space ______________.',0x00,'[]','[{\"AnswerOption\":\"Before Paragraph\",\"IsCurrect\":false},{\"AnswerOption\":\"After Paragraph\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the \",\"IsCurrect\":false}]','1','2013-11-22 01:43:26','admin'),
 (244,1,8,107,'User could give borders to __________',0x00,'[]','[{\"AnswerOption\":\"Paragraph\",\"IsCurrect\":false},{\"AnswerOption\":\"Text\",\"IsCurrect\":false},{\"AnswerOption\":\"Table\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 01:47:51','admin'),
 (245,1,8,110,'The red wavy lines will appear underneath _____________',0x00,'[]','[{\"AnswerOption\":\"Paragraph\",\"IsCurrect\":false},{\"AnswerOption\":\"Grammatical errors\",\"IsCurrect\":false},{\"AnswerOption\":\"Misspelled words\",\"IsCurrect\":true},{\"AnswerOption\":\"Both B and C\",\"IsCurrect\":false}]','1','2013-11-22 01:50:04','admin'),
 (246,1,8,111,'Page formatting used to ____________',0x00,'[]','[{\"AnswerOption\":\"Change default paragraph setting\",\"IsCurrect\":false},{\"AnswerOption\":\"Change default text setting\",\"IsCurrect\":false},{\"AnswerOption\":\"Change default page setting\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 01:58:29','admin'),
 (247,1,8,113,'This is type of page orientation:',0x00,'[]','[{\"AnswerOption\":\" Portrait\",\"IsCurrect\":false},{\"AnswerOption\":\"Landscape\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 02:02:49','admin'),
 (248,1,8,114,'Zoom option is present under ____________.',0x00,'[]','[{\"AnswerOption\":\"File\",\"IsCurrect\":false},{\"AnswerOption\":\"Insert\",\"IsCurrect\":false},{\"AnswerOption\":\"View\",\"IsCurrect\":true},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 02:05:52','admin'),
 (249,1,8,115,'Header and footer option are present under _____________.',0x00,'[]','[{\"AnswerOption\":\"File\",\"IsCurrect\":false},{\"AnswerOption\":\"View\",\"IsCurrect\":false},{\"AnswerOption\":\"Insert\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 02:07:06','admin'),
 (250,1,8,116,'By default page number starts with ___.',0x00,'[]','[{\"AnswerOption\":\"15\",\"IsCurrect\":false},{\"AnswerOption\":\"1\",\"IsCurrect\":true},{\"AnswerOption\":\"2\",\"IsCurrect\":false},{\"AnswerOption\":\"10\",\"IsCurrect\":false}]','1','2013-11-22 02:10:02','admin'),
 (251,1,8,117,'Page break option present under ____________',0x00,'[]','[{\"AnswerOption\":\"View option\",\"IsCurrect\":false},{\"AnswerOption\":\"Insert option\",\"IsCurrect\":true},{\"AnswerOption\":\"Page layout option\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 02:12:29','admin'),
 (252,1,8,118,'Page break can be deleted by deleting ________',0x00,'[]','[{\"AnswerOption\":\"Extra page break indicator in the document\",\"IsCurrect\":true},{\"AnswerOption\":\"Extra pages in the document\",\"IsCurrect\":false},{\"AnswerOption\":\"Extra paragraph in the document\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 02:15:35','admin'),
 (253,1,9,123,'Formulas can be explain as:',0x00,'[]','[{\"AnswerOption\":\"Equation that calculates the count\",\"IsCurrect\":false},{\"AnswerOption\":\"Equation that calculates the missing values\",\"IsCurrect\":false},{\"AnswerOption\":\"Equation that calculates the value\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 02:22:31','admin'),
 (254,1,9,125,'In excel, data could be sorted by:',0x00,'[]','[{\"AnswerOption\":\"Date\",\"IsCurrect\":false},{\"AnswerOption\":\"Font Color\",\"IsCurrect\":false},{\"AnswerOption\":\"Cell Color\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 02:27:31','admin'),
 (255,1,9,126,'Excel application is present under ____________.',0x00,'[]','[{\"AnswerOption\":\"Start menu\",\"IsCurrect\":false},{\"AnswerOption\":\"Microsoft office\",\"IsCurrect\":true},{\"AnswerOption\":\"Paint\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 02:29:36','admin'),
 (256,1,9,127,'Excel files are also known as _________.',0x00,'[]','[{\"AnswerOption\":\"Workbook or spreadsheet\",\"IsCurrect\":false},{\"AnswerOption\":\"Workbook\",\"IsCurrect\":true},{\"AnswerOption\":\"Column\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 02:31:29','admin'),
 (257,1,9,128,'By default, workbook have ____ worksheets. ',0x00,'[]','[{\"AnswerOption\":\"5\",\"IsCurrect\":false},{\"AnswerOption\":\"6\",\"IsCurrect\":false},{\"AnswerOption\":\"3\",\"IsCurrect\":true},{\"AnswerOption\":\"1\",\"IsCurrect\":false}]','1','2013-11-22 02:34:15','admin'),
 (258,1,9,129,'Default active cell in an open excel worksheet is ____.',0x00,'[]','[{\"AnswerOption\":\"A5\",\"IsCurrect\":false},{\"AnswerOption\":\"B1\",\"IsCurrect\":false},{\"AnswerOption\":\"A1\",\"IsCurrect\":true},{\"AnswerOption\":\"Z0\",\"IsCurrect\":false}]','1','2013-11-22 02:36:05','admin'),
 (259,1,9,130,'Which is valid way to move between cells?',0x00,'[]','[{\"AnswerOption\":\"Mouse\",\"IsCurrect\":false},{\"AnswerOption\":\"Scroll bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Navigation keys\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 03:34:59','admin'),
 (260,1,9,133,'Each cell of a worksheet has ________.',0x00,'[]','[{\"AnswerOption\":\"Multiple referance \",\"IsCurrect\":false},{\"AnswerOption\":\"Unique referance\",\"IsCurrect\":true},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 03:38:13','admin'),
 (261,1,9,134,'Find and Replace option is present under ____________.',0x00,'[]','[{\"AnswerOption\":\"Insert option\",\"IsCurrect\":false},{\"AnswerOption\":\"Page layout option\",\"IsCurrect\":false},{\"AnswerOption\":\"View option\",\"IsCurrect\":false},{\"AnswerOption\":\"Home option\",\"IsCurrect\":true}]','1','2013-11-22 03:41:03','admin'),
 (262,1,9,135,'Is it possible to insert multiple rows simultaneously?',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-22 03:43:24','admin'),
 (263,1,9,136,'Is it possible to re-size multiple rows simultaneously?',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-22 03:44:53','admin'),
 (264,1,9,137,'Shortcut key to copy and cut the content of cell is _________.',0x00,'[]','[{\"AnswerOption\":\"Ctrl %2B V and Ctrl %2B C \",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B C and Ctrl  %2B X\",\"IsCurrect\":true},{\"AnswerOption\":\"Ctrl %2B Z and Ctrl %2B C\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl  %2B V and Ctrl  %2B X\",\"IsCurrect\":false}]','1','2013-11-22 03:48:50','admin'),
 (265,1,9,138,'Is it possible to move cell without moving it&#39;s data?',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":false},{\"AnswerOption\":\"False\",\"IsCurrect\":true}]','1','2013-11-22 03:50:48','admin'),
 (266,1,9,139,'User could paste: ',0x00,'[]','[{\"AnswerOption\":\"Only the cell formatting\",\"IsCurrect\":false},{\"AnswerOption\":\"Only the formulas\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 03:54:17','admin'),
 (267,1,9,140,'Cells could be moved using:',0x00,'[]','[{\"AnswerOption\":\"Drag and drop\",\"IsCurrect\":true},{\"AnswerOption\":\"Mouse right click\",\"IsCurrect\":false},{\"AnswerOption\":\"F2 key\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 03:55:48','admin'),
 (268,1,9,141,'Freeze Panes are use to ___________',0x00,'[]','[{\"AnswerOption\":\"Fix the cell\",\"IsCurrect\":false},{\"AnswerOption\":\"Fix the row\",\"IsCurrect\":false},{\"AnswerOption\":\"Fix the heading\",\"IsCurrect\":true},{\"AnswerOption\":\"Fix the column\",\"IsCurrect\":false}]','1','2013-11-22 03:59:06','admin'),
 (269,1,9,142,'Page break option is present under _____________.',0x00,'[]','[{\"AnswerOption\":\"Home\",\"IsCurrect\":false},{\"AnswerOption\":\"View\",\"IsCurrect\":false},{\"AnswerOption\":\"Insert\",\"IsCurrect\":false},{\"AnswerOption\":\"Page Layout\",\"IsCurrect\":true}]','1','2013-11-22 04:00:49','admin'),
 (270,1,9,143,'Page setup option contain _________.',0x00,'[]','[{\"AnswerOption\":\"Page option\",\"IsCurrect\":false},{\"AnswerOption\":\"Margin\",\"IsCurrect\":false},{\"AnswerOption\":\"Header and Footer\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 04:03:41','admin'),
 (271,1,9,145,'Can user specify the range of page numbers to print?',0x00,'[]','[{\"AnswerOption\":\"Yes\",\"IsCurrect\":true},{\"AnswerOption\":\"No\",\"IsCurrect\":false}]','1','2013-11-22 04:05:56','admin'),
 (272,1,9,146,'Can user save excel file with .doc extension?',0x00,'[]','[{\"AnswerOption\":\"Yes\",\"IsCurrect\":true},{\"AnswerOption\":\"No\",\"IsCurrect\":false}]','1','2013-11-22 04:12:47','admin'),
 (273,1,9,146,'What will happen if user saves the excel file with .doc extension?',0x00,'[]','[{\"AnswerOption\":\"Content of file may loss\",\"IsCurrect\":true},{\"AnswerOption\":\"Content of file remain same\",\"IsCurrect\":false},{\"AnswerOption\":\"Not possible to excel file with .doc extension\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above possible\",\"IsCurrect\":false}]','1','2013-11-22 04:19:57','admin'),
 (274,1,9,149,'Excels default date format is ____________.',0x00,'[]','[{\"AnswerOption\":\"January 1, 2001\",\"IsCurrect\":false},{\"AnswerOption\":\"1 January 2001\",\"IsCurrect\":false},{\"AnswerOption\":\"1-Jan-01\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 04:24:28','admin'),
 (275,1,9,151,'AutoFit feature is use to adjust the _________.',0x00,'[]','[{\"AnswerOption\":\"Height\",\"IsCurrect\":false},{\"AnswerOption\":\"Width\",\"IsCurrect\":true},{\"AnswerOption\":\"Margin\",\"IsCurrect\":false},{\"AnswerOption\":\"Spacing\",\"IsCurrect\":false}]','1','2013-11-22 04:27:05','admin'),
 (276,1,9,151,'AutoFit Row height feature is use to adjust _____________.',0x00,'[]','[{\"AnswerOption\":\"Row height\",\"IsCurrect\":true},{\"AnswerOption\":\"Row width\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 04:29:36','admin'),
 (277,1,9,152,'Hide feature is use to hide _______.',0x00,'[]','[{\"AnswerOption\":\"Column\",\"IsCurrect\":false},{\"AnswerOption\":\"Row\",\"IsCurrect\":false},{\"AnswerOption\":\"Cell\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 04:31:34','admin'),
 (278,1,9,153,'Unhide feature is use to unhide _______.',0x00,'[]','[{\"AnswerOption\":\"Column\",\"IsCurrect\":false},{\"AnswerOption\":\"Row\",\"IsCurrect\":false},{\"AnswerOption\":\"Hide rows and columns\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 04:33:41','admin'),
 (279,1,9,155,'Which feature is use to copy a formula ?',0x00,'[]','[{\"AnswerOption\":\"Copy and pest\",\"IsCurrect\":false},{\"AnswerOption\":\"Drag and drop\",\"IsCurrect\":true},{\"AnswerOption\":\"Move column\",\"IsCurrect\":false},{\"AnswerOption\":\"Move Rows\",\"IsCurrect\":false}]','1','2013-11-22 04:36:05','admin'),
 (280,1,9,156,'Max function is use to calculate __________.',0x00,'[]','[{\"AnswerOption\":\"Minimum of numbers\",\"IsCurrect\":false},{\"AnswerOption\":\"Maximum of the numbers\",\"IsCurrect\":true},{\"AnswerOption\":\"Top value of number in cell\",\"IsCurrect\":false},{\"AnswerOption\":\"Lowest value of number in cell\",\"IsCurrect\":false}]','1','2013-11-22 04:41:20','admin'),
 (281,1,9,159,'Which is not a component of chart?',0x00,'[]','[{\"AnswerOption\":\"X-Axis\",\"IsCurrect\":false},{\"AnswerOption\":\"Y-Axis\",\"IsCurrect\":false},{\"AnswerOption\":\"Data Labels \",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":true}]','1','2013-11-22 04:46:19','admin'),
 (282,1,9,160,'Is it necessary to select all the row and column present in workbook to draw a chart?',0x00,'[]','[{\"AnswerOption\":\"No\",\"IsCurrect\":true},{\"AnswerOption\":\"Yes\",\"IsCurrect\":false}]','1','2013-11-22 04:50:21','admin'),
 (283,1,9,161,'Edit chart feature is available under __________ tab.',0x00,'[]','[{\"AnswerOption\":\"Home\",\"IsCurrect\":false},{\"AnswerOption\":\"Layout\",\"IsCurrect\":true},{\"AnswerOption\":\"Insert\",\"IsCurrect\":false},{\"AnswerOption\":\"View\",\"IsCurrect\":false}]','1','2013-11-22 05:03:11','admin'),
 (284,1,9,162,'Which feature is use to resize a chart?',0x00,'[]','[{\"AnswerOption\":\"Copy and Pest\",\"IsCurrect\":false},{\"AnswerOption\":\"Drag and Drop\",\"IsCurrect\":true},{\"AnswerOption\":\"Move chat\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 05:05:34','admin'),
 (285,1,9,163,'Elements of chart are also moved when we move chart.',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-22 05:07:02','admin'),
 (286,1,9,164,'Which option is used to copy chart to Word?',0x00,'[]','[{\"AnswerOption\":\"Copy and paste\",\"IsCurrect\":true},{\"AnswerOption\":\"Cut and paste\",\"IsCurrect\":false},{\"AnswerOption\":\"Delete a chart \",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 05:09:28','admin'),
 (287,1,9,166,'What is Smart Art ?',0x00,'[]','[{\"AnswerOption\":\"Visual representation of cells\",\"IsCurrect\":false},{\"AnswerOption\":\"Visual representation of paragraph\",\"IsCurrect\":false},{\"AnswerOption\":\"Visual representation of text\",\"IsCurrect\":false},{\"AnswerOption\":\"Visual representation of information and ideas\",\"IsCurrect\":true}]','1','2013-11-22 05:11:41','admin'),
 (288,1,9,167,'Clip art includes ____________.',0x00,'[]','[{\"AnswerOption\":\"Sound\",\"IsCurrect\":false},{\"AnswerOption\":\"Animation\",\"IsCurrect\":false},{\"AnswerOption\":\"Movies\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 05:13:54','admin'),
 (289,1,9,168,'Edit picture feature option is present under _______ tab.',0x00,'[]','[{\"AnswerOption\":\"Home\",\"IsCurrect\":false},{\"AnswerOption\":\"Insert\",\"IsCurrect\":false},{\"AnswerOption\":\"View\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 05:15:33','admin'),
 (290,1,10,169,'Microsoft powerpoint helps you in __________.',0x00,'[]','[{\"AnswerOption\":\"Creation of text files\",\"IsCurrect\":false},{\"AnswerOption\":\"Creation of video files\",\"IsCurrect\":false},{\"AnswerOption\":\"Creation of slid shows\",\"IsCurrect\":true},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 05:24:44','admin'),
 (291,1,10,171,'Microsoft powerpoint is part of ______________.',0x00,'[]','[{\"AnswerOption\":\"Microsoft Office Suite\",\"IsCurrect\":true},{\"AnswerOption\":\"Google plus\",\"IsCurrect\":false},{\"AnswerOption\":\"Apple \",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 05:27:30','admin'),
 (292,1,10,173,'Preview design template by __________.',0x00,'[]','[{\"AnswerOption\":\"Clicking the template name on the list\",\"IsCurrect\":false},{\"AnswerOption\":\"Highlighting the template name on the list\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 05:31:01','admin'),
 (293,1,10,174,'It is necessary to select blank presentation to create slide show? ',0x00,'[]','[{\"AnswerOption\":\"Yes\",\"IsCurrect\":false},{\"AnswerOption\":\"No\",\"IsCurrect\":true}]','1','2013-11-22 05:33:24','admin'),
 (294,1,10,175,'Different type of slide layouts are ?',0x00,'[]','[{\"AnswerOption\":\"Title and Content\",\"IsCurrect\":false},{\"AnswerOption\":\"Section Header\",\"IsCurrect\":false},{\"AnswerOption\":\"Text Content\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 05:35:14','admin'),
 (296,1,10,177,'How user could open the Power Point presentation?',0x00,'[]','[{\"AnswerOption\":\"Copy it\",\"IsCurrect\":false},{\"AnswerOption\":\"Delete it\",\"IsCurrect\":false},{\"AnswerOption\":\"Double Click on it\",\"IsCurrect\":true},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 05:39:38','admin'),
 (298,1,10,179,'Normal View divides the screen into _______.',0x00,'[]','[{\"AnswerOption\":\"5 section\",\"IsCurrect\":false},{\"AnswerOption\":\"10 section\",\"IsCurrect\":false},{\"AnswerOption\":\"3 section\",\"IsCurrect\":true},{\"AnswerOption\":\"7 section\",\"IsCurrect\":false}]','1','2013-11-22 05:42:09','admin'),
 (299,1,10,180,'Slide sorter view is use to _____.',0x00,'[]','[{\"AnswerOption\":\"Set order of the slide\",\"IsCurrect\":false},{\"AnswerOption\":\"Sort the slide\",\"IsCurrect\":false},{\"AnswerOption\":\"Add special effects\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 05:45:45','admin'),
 (300,1,10,181,'Which view is used to preview the presentation?',0x00,'[]','[{\"AnswerOption\":\"To preview of slide\",\"IsCurrect\":true},{\"AnswerOption\":\"To add new slide to slide show\",\"IsCurrect\":false},{\"AnswerOption\":\"To delete slide from slide show\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 05:48:53','admin'),
 (301,1,10,182,'Slide transition effect in presentation slide show is necessary?',0x00,'[]','[{\"AnswerOption\":\"Yes\",\"IsCurrect\":false},{\"AnswerOption\":\"No\",\"IsCurrect\":true}]','1','2013-11-22 05:52:31','admin'),
 (302,1,10,183,'_____ layouts are available while creating new slide show.',0x00,'[]','[{\"AnswerOption\":\"3\",\"IsCurrect\":false},{\"AnswerOption\":\"5\",\"IsCurrect\":false},{\"AnswerOption\":\"9\",\"IsCurrect\":true},{\"AnswerOption\":\"8\",\"IsCurrect\":false}]','1','2013-11-22 05:54:12','admin'),
 (303,1,10,184,'Design template option is available under _______.',0x00,'[]','[{\"AnswerOption\":\"Ribbon tab\",\"IsCurrect\":true},{\"AnswerOption\":\"Footer tab\",\"IsCurrect\":false},{\"AnswerOption\":\"Header tab\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 05:58:33','admin'),
 (304,1,10,185,'Layout command is present in ________.',0x00,'[]','[{\"AnswerOption\":\"Header bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Menu bar\",\"IsCurrect\":true},{\"AnswerOption\":\"Side bar\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 05:59:51','admin'),
 (305,1,10,186,'Which of the following command is useful while editing existing slide?',0x00,'[]','[{\"AnswerOption\":\"Copy\",\"IsCurrect\":false},{\"AnswerOption\":\"Cut\",\"IsCurrect\":false},{\"AnswerOption\":\"Paste\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 06:02:02','admin'),
 (306,1,10,187,'Which feature is used to reorder the slides?',0x00,'[]','[{\"AnswerOption\":\"Copy\",\"IsCurrect\":false},{\"AnswerOption\":\"Cut\",\"IsCurrect\":false},{\"AnswerOption\":\"Drag and Drop\",\"IsCurrect\":true},{\"AnswerOption\":\"Delete\",\"IsCurrect\":false}]','1','2013-11-22 06:03:20','admin'),
 (307,1,10,188,'Hide feature could be used to hide the slide in ________.',0x00,'[]','[{\"AnswerOption\":\"Slide show\",\"IsCurrect\":true},{\"AnswerOption\":\"Normal view\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide sorter view\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 06:06:46','admin'),
 (308,1,10,190,'New - Text are use to _______.',0x00,'[]','[{\"AnswerOption\":\"Communicate your ideas to your audience\",\"IsCurrect\":true},{\"AnswerOption\":\" communicate your ideas to power point\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 06:12:12','admin'),
 (309,1,10,191,'Text could be inserted using ___________.',0x00,'[]','[{\"AnswerOption\":\"Outline Text\",\"IsCurrect\":false},{\"AnswerOption\":\"Text Boxes\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 06:13:43','admin'),
 (310,1,10,192,'Which feature is used to change the existing fonts?',0x00,'[]','[{\"AnswerOption\":\"Format Fonts\",\"IsCurrect\":false},{\"AnswerOption\":\"Change Case\",\"IsCurrect\":false},{\"AnswerOption\":\"Replace Fonts\",\"IsCurrect\":true},{\"AnswerOption\":\"Toggle case\",\"IsCurrect\":false}]','1','2013-11-22 06:17:48','admin'),
 (311,1,10,193,'How to activate the textbox?',0x00,'[]','[{\"AnswerOption\":\"By moving it\",\"IsCurrect\":false},{\"AnswerOption\":\"By clicking on it\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 06:20:04','admin'),
 (312,1,10,194,'Notes are not visible in _________________.',0x00,'[]','[{\"AnswerOption\":\"Normal View\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide sorter View\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide show\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 06:22:03','admin'),
 (313,1,10,195,'Shortcut key for spell check is ___.',0x00,'[]','[{\"AnswerOption\":\"F1\",\"IsCurrect\":false},{\"AnswerOption\":\"F10\",\"IsCurrect\":false},{\"AnswerOption\":\"F11\",\"IsCurrect\":false},{\"AnswerOption\":\"F7\",\"IsCurrect\":true}]','1','2013-11-22 06:23:53','admin'),
 (314,1,10,196,'Power point slide could be saved as ___________.',0x00,'[]','[{\"AnswerOption\":\"Folder\",\"IsCurrect\":false},{\"AnswerOption\":\"Web pages\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 06:26:00','admin'),
 (315,1,10,197,'Page set up option is present under ____________.',0x00,'[]','[{\"AnswerOption\":\"Menu bar\",\"IsCurrect\":true},{\"AnswerOption\":\"Header bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Footer bar\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 06:27:53','admin'),
 (316,1,10,198,'Save as option is present under _____.',0x00,'[]','[{\"AnswerOption\":\"Menu bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Header  bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Office button\",\"IsCurrect\":true},{\"AnswerOption\":\"Footer bar\",\"IsCurrect\":false}]','1','2013-11-22 06:29:42','admin'),
 (317,1,10,199,'When it is necessary to save power point pages as Web pages ?',0x00,'[]','[{\"AnswerOption\":\"When user want to run it as a slide show\",\"IsCurrect\":false},{\"AnswerOption\":\"When user want to run it on the internet\",\"IsCurrect\":true},{\"AnswerOption\":\"When user want to run it in slide sorter view\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 06:33:02','admin'),
 (318,1,10,200,'Which option is available to print a slide show?',0x00,'[]','[{\"AnswerOption\":\"Print slide\",\"IsCurrect\":false},{\"AnswerOption\":\"Print hangout\",\"IsCurrect\":false},{\"AnswerOption\":\"Print Notes Page\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 06:35:30','admin'),
 (319,1,10,201,'What happens after executing the close command?',0x00,'[]','[{\"AnswerOption\":\"Close the current presentation slides file\",\"IsCurrect\":false},{\"AnswerOption\":\"Prompted to save the file before closing\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 06:38:19','admin'),
 (320,1,10,202,'Exit option is present in __________.',0x00,'[]','[{\"AnswerOption\":\"Office button\",\"IsCurrect\":true},{\"AnswerOption\":\"Header bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Footer bar\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 06:50:30','admin'),
 (321,1,10,203,'Short cut key for save as is ___________.',0x00,'[]','[{\"AnswerOption\":\"F1\",\"IsCurrect\":false},{\"AnswerOption\":\"F10\",\"IsCurrect\":false},{\"AnswerOption\":\"F11\",\"IsCurrect\":false},{\"AnswerOption\":\"F12\",\"IsCurrect\":true}]','1','2013-11-22 06:51:39','admin'),
 (322,1,4,36,'Password is not required always for access.',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-22 06:56:43','admin'),
 (323,1,11,207,'______ is used to connect networks in larger geographic areas.',0x00,'[]','[{\"AnswerOption\":\"Local Area Network (LAN)\",\"IsCurrect\":false},{\"AnswerOption\":\"Wide Area Network (WAN)\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-22 07:15:54','admin'),
 (324,1,11,208,'______is a set of rules to govern the data transfer between the devices.',0x00,'[]','[{\"AnswerOption\":\"Local Area Network\",\"IsCurrect\":false},{\"AnswerOption\":\"Workstations\",\"IsCurrect\":false},{\"AnswerOption\":\"Protocol\",\"IsCurrect\":true},{\"AnswerOption\":\"Local Area Network (LAN)\",\"IsCurrect\":false}]','1','2013-11-22 07:18:14','admin'),
 (325,1,11,212,'File transfer protocol allows user to transfer files from one computer to another.',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-22 07:23:59','admin'),
 (326,1,11,215,'The file and printer sharing allows other computers on a network to access resources on your computer by using a _______.',0x00,'[]','[{\"AnswerOption\":\"Workstations\",\"IsCurrect\":false},{\"AnswerOption\":\"Microsoft network\",\"IsCurrect\":true},{\"AnswerOption\":\"Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"Post office Protocol \",\"IsCurrect\":false}]','1','2013-11-22 07:30:40','admin'),
 (327,1,11,216,'Sharing Printers describes how to use Windows to share a printer with others on network.',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":false},{\"AnswerOption\":\"False\",\"IsCurrect\":true}]','1','2013-11-22 07:35:43','admin'),
 (328,1,11,218,'Printers could be connected to network using: ',0x00,'[]','[{\"AnswerOption\":\"Find a printer in the directory\",\"IsCurrect\":false},{\"AnswerOption\":\"To connect to an Internet or intranet printer\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-22 08:06:17','admin'),
 (329,1,11,217,'Stop Printer Sharing is present in ______ tab.',0x00,'[]','[{\"AnswerOption\":\"Ports\",\"IsCurrect\":false},{\"AnswerOption\":\"General\",\"IsCurrect\":false},{\"AnswerOption\":\"Advanced \",\"IsCurrect\":false},{\"AnswerOption\":\"Sharing\",\"IsCurrect\":true}]','1','2013-11-22 08:09:41','admin'),
 (330,1,2,11,'Computer keyboard have:',0x00,'[]','[{\"AnswerOption\":\"Function keys\",\"IsCurrect\":false},{\"AnswerOption\":\"Special characters\",\"IsCurrect\":false},{\"AnswerOption\":\"Alphanumeric characters\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 08:12:31','admin'),
 (331,1,12,225,'Windows firewall settings have _________ tab.',0x00,'[]','[{\"AnswerOption\":\"General\",\"IsCurrect\":false},{\"AnswerOption\":\"Exceptions\",\"IsCurrect\":false},{\"AnswerOption\":\"Advanced\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-22 08:15:21','admin'),
 (332,1,12,225,'The exception tab lets user to add exceptions to permit inbound traffic.',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-22 08:16:09','admin'),
 (333,1,12,226,'You shall use more than one firewall is one of the best practices',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":false},{\"AnswerOption\":\"False\",\"IsCurrect\":true}]','1','2013-11-22 08:18:24','admin'),
 (334,1,12,229,'Press F1 to get the list of operating systems.',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":false},{\"AnswerOption\":\"False\",\"IsCurrect\":true}]','1','2013-11-22 08:20:26','admin'),
 (335,1,12,230,'In Windows, some background processes can prevent applications from working correctly.',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-22 08:22:59','admin'),
 (336,1,13,231,'The Internet is a global system of interconnected computer networks that consists of _______.',0x00,'[]','[{\"AnswerOption\":\"government networks\",\"IsCurrect\":false},{\"AnswerOption\":\"academic\",\"IsCurrect\":false},{\"AnswerOption\":\"business\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-22 08:26:27','admin'),
 (337,1,13,236,'Website access is tracked in web browser to maintain it as a history.',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-22 08:31:23','admin'),
 (338,1,13,239,'Which is very popular email service?',0x00,'[]','[{\"AnswerOption\":\"Gmail\",\"IsCurrect\":true},{\"AnswerOption\":\"Yahoo\",\"IsCurrect\":false},{\"AnswerOption\":\"Outlook \",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 08:33:50','admin'),
 (339,1,13,240,'Password shall have only upper case letters.',0x00,'[]','[{\"AnswerOption\":\"Yes\",\"IsCurrect\":false},{\"AnswerOption\":\"No\",\"IsCurrect\":true}]','1','2013-11-22 08:35:41','admin'),
 (340,1,13,243,'User shall attach files of up to ___ to an email. This is one of the best practices.',0x00,'[]','[{\"AnswerOption\":\"1MB\",\"IsCurrect\":true},{\"AnswerOption\":\"5MB\",\"IsCurrect\":false},{\"AnswerOption\":\"10MB\",\"IsCurrect\":false},{\"AnswerOption\":\"20MB\",\"IsCurrect\":false}]','1','2013-11-22 08:38:07','admin'),
 (342,1,13,245,'_______ is one of the most popular search engines.',0x00,'[]','[{\"AnswerOption\":\"Windows Live\",\"IsCurrect\":false},{\"AnswerOption\":\"Google\",\"IsCurrect\":true},{\"AnswerOption\":\"Windows Live\",\"IsCurrect\":false},{\"AnswerOption\":\"Yahoo\",\"IsCurrect\":false}]','1','2013-11-22 08:43:29','admin'),
 (344,1,14,248,'_________ is an internet based applications/websites which help people to interact with other people.',0x00,'[]','[{\"AnswerOption\":\"Social networking\",\"IsCurrect\":false},{\"AnswerOption\":\"Content communities\",\"IsCurrect\":false},{\"AnswerOption\":\"Wikipedia\",\"IsCurrect\":false},{\"AnswerOption\":\"Social media\",\"IsCurrect\":true}]','1','2013-11-22 08:49:07','admin'),
 (345,1,14,249,'_______ is one kind of social media website.',0x00,'[]','[{\"AnswerOption\":\"News\",\"IsCurrect\":false},{\"AnswerOption\":\"Networking\",\"IsCurrect\":false},{\"AnswerOption\":\"Photo and Video Sharing\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-22 08:51:45','admin'),
 (346,1,14,250,'WhatsApp Messenger is a cross-platform mobile messaging app which allows you to exchange messages without having to pay for SMS.',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-22 08:53:50','admin'),
 (347,1,14,250,'Which is popular social media website?',0x00,'[]','[{\"AnswerOption\":\"MySpace\",\"IsCurrect\":false},{\"AnswerOption\":\"Twitter\",\"IsCurrect\":false},{\"AnswerOption\":\"YouTube\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-22 08:54:59','admin'),
 (349,1,8,91,'User could change the paper size before printing the document.',0x00,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-23 05:08:17','admin'),
 (350,1,8,93,'Shortcut key for cut:',0x00,'[]','[{\"AnswerOption\":\"CTRL %2B X\",\"IsCurrect\":true},{\"AnswerOption\":\"CTRL %2B V\",\"IsCurrect\":false},{\"AnswerOption\":\"CTRL %2B P\",\"IsCurrect\":false},{\"AnswerOption\":\"CTRL %2B O\",\"IsCurrect\":false}]','1','2013-11-23 05:12:40','admin'),
 (351,1,9,132,'What is the short cut key for undo?',0x00,'[]','[{\"AnswerOption\":\"Ctrl %2B X\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B Z\",\"IsCurrect\":true},{\"AnswerOption\":\"Ctrl %2B A\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B P\",\"IsCurrect\":false}]','1','2013-11-23 07:44:47','admin'),
 (352,1,9,150,'The content of the column is adjusted to column width using ________ feature.',0x00,'[]','[{\"AnswerOption\":\"Autofit\",\"IsCurrect\":true},{\"AnswerOption\":\"Autopilot\",\"IsCurrect\":false},{\"AnswerOption\":\"AutoCopy\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-23 08:01:31','admin'),
 (353,1,9,165,'Which is auto shape category?',0x00,'[]','[{\"AnswerOption\":\"Lines\",\"IsCurrect\":false},{\"AnswerOption\":\"Flow charts\",\"IsCurrect\":false},{\"AnswerOption\":\"Call outs\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-23 08:31:25','admin');
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
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

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
  `Title` varchar(255) character set utf8 NOT NULL,
  `PageName` varchar(100) character set utf8 NOT NULL,
  `Time` int(255) unsigned NOT NULL default '0',
  `DateCreated` timestamp NOT NULL default CURRENT_TIMESTAMP,
  `Description` varchar(500) character set latin1 collate latin1_bin NOT NULL,
  `Link` varchar(100) NOT NULL,
  `IsDB` tinyint(3) NOT NULL,
  `PageContent` mediumtext,
  PRIMARY KEY  (`Id`),
  KEY `FK_chaptersection` (`chapterId`),
  CONSTRAINT `FK_chaptersection` FOREIGN KEY (`chapterId`) REFERENCES `chapterdetails` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=252 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `chaptersection`
--

/*!40000 ALTER TABLE `chaptersection` DISABLE KEYS */;
INSERT INTO `chaptersection` (`Id`,`chapterId`,`Title`,`PageName`,`Time`,`DateCreated`,`Description`,`Link`,`IsDB`,`PageContent`) VALUES 
 (1,1,'Introduction','Introduction.htm',1800,'2013-11-03 07:26:41','','Course/FOC/1/Introduction.htm',0,NULL),
 (2,1,'Computer Structure ','computer-structure.htm',3600,'2013-11-03 07:26:41','','Course/FOC/1/computer-structure.htm',0,NULL),
 (3,1,'Hardware','Hardware.htm',2000,'2013-11-03 07:26:41','','Course/FOC/1/Hardware.htm',0,NULL),
 (4,1,'Software','software.htm',1800,'2013-11-03 07:26:41','','Course/FOC/1/software.htm',0,NULL),
 (5,1,'System Software','system-software.htm',3600,'2013-11-03 07:26:41','','Course/FOC/1/system-software.htm',0,NULL),
 (6,1,'Application software','application-software.htm',600,'2013-11-03 07:26:41','','Course/FOC/1/application-software.htm',0,NULL),
 (7,1,'Generalized Packages','generalized-packages.htm',1200,'2013-11-03 07:26:41','','Course/FOC/1/generalized-packages.htm',0,NULL),
 (8,1,'Customized Packages','customized-packeges.htm',3000,'2013-11-03 07:26:41','','Course/FOC/1/customized-packeges.htm',0,NULL),
 (9,2,'Introduction','introduction.htm',600,'2013-11-16 10:36:14','','Course/FOC/2/introduction.htm',0,NULL),
 (10,2,'Input Devices','input-devices.htm',600,'2013-11-16 10:36:14','','Course/FOC/2/input-devices.htm',0,NULL),
 (11,2,'keyboard','keyboard.htm',1800,'2013-11-16 10:36:14','','Course/FOC/2/keyboard.htm',0,NULL),
 (12,2,'Mouse','mouse.htm',1800,'2013-11-16 10:36:14','','Course/FOC/2/mouse.htm',0,NULL),
 (13,2,'Opticals-Canner','opticals-scanner.htm',1800,'2013-11-16 10:36:14','','Course/FOC/2/opticals-scanner.htm',0,NULL),
 (14,2,'Touch Screen','touch-screen.htm',300,'2013-11-16 10:36:14','','Course/FOC/2/touch-screen.htm',0,NULL),
 (15,2,'Microphone','microphone.htm',900,'2013-11-16 10:36:14','','Course/FOC/2/microphone.htm',0,NULL),
 (16,2,'Web Camera','web-camera.htm',300,'2013-11-16 10:36:14','','Course/FOC/2/web-camera.htm',0,NULL),
 (17,2,'Output-Devices','output-devices.htm',600,'2013-11-16 10:36:14','','Course/FOC/2/output-devices.htm',0,NULL),
 (18,2,'Monitor','monitor.htm',1800,'2013-11-16 10:36:14','','Course/FOC/2/monitor.htm',0,NULL),
 (19,2,'Display Resolution','display-resolution.htm',300,'2013-11-16 10:36:14','','Course/FOC/2/display-resolution.htm',0,NULL),
 (20,2,'Printers','printers.htm',2700,'2013-11-16 10:36:14','','Course/FOC/2/printers.htm',0,NULL),
 (21,2,'Speaker','speaker.htm',300,'2013-11-16 10:36:14','','Course/FOC/2/speaker.htm',0,NULL),
 (22,2,'Projector','projector.htm',600,'2013-11-16 10:36:14','','Course/FOC/2/projector.htm',0,NULL),
 (23,2,'Fundamentals of CPU','fundamentals-of-cpu.htm',300,'2013-11-16 10:36:15','','Course/FOC/2/fundamentals-of-cpu.htm',0,NULL),
 (24,3,'Introduction','Introduction.htm',900,'2013-11-16 10:41:59','','Course/FOC/3/Introduction.htm',0,NULL),
 (25,3,'Storage Type','storage-type.htm',1200,'2013-11-16 10:42:00','','Course/FOC/3/storage-type.htm',0,NULL),
 (26,3,'Primary Storage','primary-storage.htm',600,'2013-11-16 10:42:00','','Course/FOC/3/primary-storage.htm',0,NULL),
 (27,3,'RAM Memory','ram-memory.htm',1200,'2013-11-16 10:42:00','','Course/FOC/3/ram-memory.htm',0,NULL),
 (28,3,'ROM Memory','rom-memory.htm',1200,'2013-11-16 10:42:00','','Course/FOC/3/rom-memory.htm',0,NULL),
 (29,3,'Secondary Storage','secondary-storage.htm',600,'2013-11-16 10:42:00','','Course/FOC/3/secondary-storage.htm',0,NULL),
 (30,3,'Hard Disk Drive','hard-disk-drive.htm',900,'2013-11-16 10:42:00','','Course/FOC/3/hard-disk-drive.htm',0,NULL),
 (31,3,'CDs and- DVDs','cds-and-dvds.htm',900,'2013-11-16 10:42:00','','Course/FOC/3/cds-and-dvds.htm',0,NULL),
 (32,3,'USB Flash Drive','usb-flash-drive.htm',1200,'2013-11-16 10:42:00','','Course/FOC/3/usb-flash-drive.htm',0,NULL),
 (33,4,'Introduction','Introduction.htm',1200,'2013-11-16 10:50:29','','Course/FOC/4/Introduction.htm',0,NULL),
 (34,4,'Functions of an Operating System','functions-of-an-operating-system.htm',2700,'2013-11-16 10:50:30','','Course/FOC/4/functions-of-an-operating-system.htm',0,NULL),
 (35,4,'Operating System Vendors','operating-system-vendors.htm',1200,'2013-11-16 10:50:30','','Course/FOC/4/operating-system-vendors.htm',0,NULL),
 (36,4,'Logging On','logging-on.htm',900,'2013-11-16 10:50:30','','Course/FOC/4/logging-on.htm',0,NULL),
 (37,4,'Start Menu','start-menu.htm',600,'2013-11-16 10:50:30','','Course/FOC/4/start-menu.htm',0,NULL),
 (38,4,'Overview of the Options','overview-of-the-options.htm',1800,'2013-11-16 10:50:30','','Course/FOC/4/overview-of-the-options.htm',0,NULL),
 (39,4,'Task Bar','task-bar.htm',900,'2013-11-16 10:50:30','','Course/FOC/4/task-bar.htm',0,NULL),
 (40,4,'Start Program','start-program.htm',600,'2013-11-16 10:50:30','','Course/FOC/4/start-program.htm',0,NULL),
 (41,4,'Quitting Program','quitting-program.htm',600,'2013-11-16 10:50:30','','Course/FOC/4/quitting-program.htm',0,NULL),
 (42,4,'Getting Help','getting-help.htm',1800,'2013-11-16 10:50:30','','Course/FOC/4/getting-help.htm',0,NULL),
 (43,4,'Locating Files and Folders','locating-files-and-folders.htm',1200,'2013-11-16 10:50:30','','Course/FOC/4/locating-files-and-folders.htm',0,NULL),
 (44,4,'Changing System Settings','changing-system-settings.htm',1200,'2013-11-16 10:50:30','','Course/FOC/4/changing-system-settings.htm',0,NULL),
 (45,4,'Using My Computer','using-my-computer.htm',900,'2013-11-16 10:50:30','','Course/FOC/4/using-my-computer.htm',0,NULL),
 (46,4,'Display the Storage Contents','display-the-storage-contents.htm',900,'2013-11-16 10:50:30','','Course/FOC/4/display-the-storage-contents.htm',0,NULL),
 (47,4,'Start Lock and Shutdown Windows','start-lock-and-shutdown-windows.htm',1200,'2013-11-16 10:50:30','','Course/FOC/4/start-lock-and-shutdown-windows.htm',0,NULL),
 (48,5,'File Management in Windows','file-management-in-windows.htm',1200,'2013-11-16 10:55:28','','Course/FOC/5/file-management-in-windows.htm',0,NULL),
 (49,5,'Using Windows Explorer','using-windows-explorer.htm',900,'2013-11-16 10:55:28','','Course/FOC/5/using-windows-explorer.htm',0,NULL),
 (50,5,'Copying or Moving a File or Folder','copying-or-moving-a-file-or-folder.htm',1800,'2013-11-16 10:55:28','','Course/FOC/5/copying-or-moving-a-file-or-folder.htm',0,NULL),
 (51,5,'View File Details','view-file-details.htm',900,'2013-11-16 10:55:28','','Course/FOC/5/view-file-details.htm',0,NULL),
 (52,5,'Create New Folder','create-new-folder.htm',900,'2013-11-16 10:55:28','','Course/FOC/5/create-new-folder.htm',0,NULL),
 (53,5,'Rename a File or Folder','rename-a-file-or-folder.htm',600,'2013-11-16 10:55:28','','Course/FOC/5/rename-a-file-or-folder.htm',0,NULL),
 (54,5,'Delete a File or Folder','delete-a-file-or-folder.htm',900,'2013-11-16 10:55:28','','Course/FOC/5/delete-a-file-or-folder.htm',0,NULL),
 (55,5,'Hidden Files and Folders','hidden-files-and-folders.htm',900,'2013-11-16 10:55:28','','Course/FOC/5/hidden-files-and-folders.htm',0,NULL),
 (56,5,'Install Software Hardware','install-software-hardware.htm',900,'2013-11-16 10:55:28','','Course/FOC/5/install-software-hardware.htm',0,NULL),
 (57,5,'Install Software','install-software.htm',900,'2013-11-16 10:55:28','','Course/FOC/5/install-software.htm',0,NULL),
 (58,5,'Change or Remove Software','change-or-remove-software.htm',900,'2013-11-16 10:55:28','','Course/FOC/5/change-or-remove-software.htm',0,NULL),
 (59,5,'Add New Features','add-new-features.htm',900,'2013-11-16 10:55:28','','Course/FOC/5/add-new-features.htm',0,NULL),
 (60,5,'Install Hardware','install-hardware.htm',900,'2013-11-16 10:55:28','','Course/FOC/5/install-hardware.htm',0,NULL),
 (61,5,'Search in Windows','search-in-windows.htm',900,'2013-11-16 10:55:28','','Course/FOC/5/search-in-windows.htm',0,NULL),
 (62,5,'Device Manager','device-manager.htm',2700,'2013-11-16 10:55:28','','Course/FOC/5/device-manager.htm',0,NULL),
 (63,6,'Introduction','introduction.htm',1200,'2013-11-17 13:42:14','','Course/FOC/6/introduction.htm',0,NULL),
 (64,6,'Application Software','application-software.htm',900,'2013-11-17 13:42:14','','Course/FOC/6/application-software.htm',0,NULL),
 (65,6,'The Toolbar','toolbar.htm',1800,'2013-11-17 13:42:14','','Course/FOC/6/toolbar.htm',0,NULL),
 (66,6,'Color Palette','color-palette.htm',900,'2013-11-17 13:42:14','','Course/FOC/6/color-palette.htm',0,NULL),
 (67,6,'The Option Tool','the-option-tool.htm',7200,'2013-11-17 13:42:14','','Course/FOC/6/the-option-tool.htm',0,NULL),
 (68,6,'Save Image','save-image.htm',900,'2013-11-17 13:42:14','','Course/FOC/6/save-image.htm',0,NULL),
 (69,7,'Introduction','introduction.htm',900,'2013-11-17 13:44:25','','Course/FOC/7/introduction.htm',0,NULL),
 (70,7,'Open Notepad','open-notepad.htm',900,'2013-11-17 13:44:25','','Course/FOC/7/open-notepad.htm',0,NULL),
 (71,7,'Save','save.htm',900,'2013-11-17 13:44:25','','Course/FOC/7/save.htm',0,NULL),
 (72,7,'Print','print.htm',900,'2013-11-17 13:44:25','','Course/FOC/7/print.htm',0,NULL),
 (73,7,'Open','open.htm',600,'2013-11-17 13:44:25','','Course/FOC/7/open.htm',0,NULL),
 (74,7,'Font','font.htm',900,'2013-11-17 13:44:25','','Course/FOC/7/font.htm',0,NULL),
 (75,7,'Word Wrap','word-wrap.htm',1200,'2013-11-17 13:44:26','','Course/FOC/7/word-wrap.htm',0,NULL),
 (76,8,'Introduction','introduction.htm',600,'2013-11-17 02:02:33','','Course/FOC/8/introduction.htm',0,NULL),
 (77,8,'Features','features.htm',1200,'2013-11-17 02:02:33','','Course/FOC/8/features.htm',0,NULL),
 (78,8,'Word 2007','word-2007.htm',1800,'2013-11-17 02:02:33','','Course/FOC/8/word-2007.htm',0,NULL),
 (79,8,'Screen Layout','screen-layout.htm',600,'2013-11-17 02:02:33','','Course/FOC/8/screen-layout.htm',0,NULL),
 (80,8,'Menus','menus.htm',1800,'2013-11-17 02:02:33','','Course/FOC/8/menus.htm',0,NULL),
 (81,8,'Toolbars','toolbars.htm',3600,'2013-11-17 02:02:33','','Course/FOC/8/toolbars.htm',0,NULL),
 (82,8,'Rulers','rulers.htm',900,'2013-11-17 02:02:33','','Course/FOC/8/rulers.htm',0,NULL),
 (83,8,'Typing Screen Objects','typing-screen-objects.htm',1800,'2013-11-17 02:02:33','','Course/FOC/8/typing-screen-objects.htm',0,NULL),
 (84,8,'Scrollbars','scrollbars.htm',1200,'2013-11-17 02:02:33','','Course/FOC/8/scrollbars.htm',0,NULL),
 (85,8,'Managing Documents','managing-documents.htm',1800,'2013-11-17 02:02:33','','Course/FOC/8/managing-documents.htm',0,NULL),
 (86,8,'Create New Document','create-new-document.htm',600,'2013-11-17 02:02:33','','Course/FOC/8/create-new-document.htm',0,NULL),
 (87,8,'Open an Existing Document','open-an-existing-document.htm',600,'2013-11-17 02:02:33','','Course/FOC/8/open-an-existing-document.htm',0,NULL),
 (88,8,'Save a New or Existing Document','save-new-or-existing-document.htm',900,'2013-11-17 02:02:33','','Course/FOC/8/save-new-or-existing-document.htm',0,NULL),
 (89,8,'Find a Document','find-a-document.htm',1200,'2013-11-17 02:02:34','','Course/FOC/8/find-a-document.htm',0,NULL),
 (90,8,'Close a Document','close-a-document.htm',600,'2013-11-17 02:02:34','','Course/FOC/8/close-a-document.htm',0,NULL),
 (91,8,'Print a Document','print-a-document.htm',1800,'2013-11-17 02:02:34','','Course/FOC/8/print-a-document.htm',0,NULL),
 (92,8,'Exit Word Program','exit-word-program.htm',600,'2013-11-17 02:02:34','','Course/FOC/8/exit-word-program.htm',0,NULL),
 (93,8,'Keyboard Shortcuts','keyboard-shortcuts.htm',4800,'2013-11-17 02:02:34','','Course/FOC/8/keyboard-shortcuts.htm',0,NULL),
 (94,8,'Working with Text','working-with-text.htm',600,'2013-11-17 02:02:34','','Course/FOC/8/working-with-text.htm',0,NULL),
 (95,8,'Typing Text','typing-text.htm',900,'2013-11-17 02:02:34','','Course/FOC/8/typing-text.htm',0,NULL),
 (96,8,'Inserting Text','inserting-text.htm',900,'2013-11-17 02:02:34','','Course/FOC/8/inserting-text.htm',0,NULL),
 (97,8,'Spacebar and Tabs','spacebar-and-tabs.htm',600,'2013-11-17 02:02:34','','Course/FOC/8/spacebar-and-tabs.htm',0,NULL),
 (98,8,'Selecting Text','selecting-text.htm',1800,'2013-11-17 02:02:34','','Course/FOC/8/selecting-text.htm',0,NULL),
 (99,8,'Deleting Text','deleting-text.htm',900,'2013-11-17 02:02:34','','Course/FOC/8/deleting-text.htm',0,NULL),
 (100,8,'Replacing Text','replacing-text.htm',900,'2013-11-17 02:02:34','','Course/FOC/8/replacing-text.htm',0,NULL),
 (101,8,'Formatting Text','formatting-text.htm',7200,'2013-11-17 02:02:34','','Course/FOC/8/formatting-text.htm',0,NULL),
 (102,8,'Format Painter','format-painter.htm',3600,'2013-11-17 02:02:34','','Course/FOC/8/format-painter.htm',0,NULL),
 (103,8,'Format Paragraphs','format-paragraphs.htm',7200,'2013-11-17 02:02:34','','Course/FOC/8/format-paragraphs.htm',0,NULL),
 (104,8,'Line Markers','line-markers.htm',1800,'2013-11-17 02:02:34','','Course/FOC/8/line-markers.htm',0,NULL),
 (105,8,'Line Spacing','line-spacing.htm',1800,'2013-11-17 02:02:34','','Course/FOC/8/line-spacing.htm',0,NULL),
 (106,8,'Paragraph Spacing','paragraph-spacing.htm',1200,'2013-11-17 02:02:34','','Course/FOC/8/paragraph-spacing.htm',0,NULL),
 (107,8,'Borders and Shading','borders-and-shading.htm',3600,'2013-11-17 02:02:34','','Course/FOC/8/borders-and-shading.htm',0,NULL),
 (108,8,'Bulleted and Numbered Lists','bulleted-and-numberd-lists.htm',1800,'2013-11-17 02:02:34','','Course/FOC/8/bulleted-and-numberd-lists.htm',0,NULL),
 (109,8,'Copying Text and Moveing Text','copying-text-and-moving-text.htm',1800,'2013-11-17 02:02:34','','Course/FOC/8/copying-text-and-moving-text.htm',0,NULL),
 (110,8,'Spelling and Grammar','spelling-and-grammer.htm',3600,'2013-11-17 02:02:34','','Course/FOC/8/spelling-and-grammer.htm',0,NULL),
 (111,8,'Page Formatting','page-formatting.htm',5400,'2013-11-17 02:02:34','','Course/FOC/8/page-formatting.htm',0,NULL),
 (112,8,'Page Margins','page-margins.htm',1800,'2013-11-17 02:02:34','','Course/FOC/8/page-margins.htm',0,NULL),
 (113,8,'Page Size and Orientation','page-size-and-orientation.htm',1200,'2013-11-17 02:02:34','','Course/FOC/8/page-size-and-orientation.htm',0,NULL),
 (114,8,'Zoom in to the Page','zoom-in-to-the-page.htm',1800,'2013-11-17 02:02:34','','Course/FOC/8/zoom-in-to-the-page.htm',0,NULL),
 (115,8,'Headers and Footers','headers-and-footers.htm',3600,'2013-11-17 02:02:34','','Course/FOC/8/headers-and-footers.htm',0,NULL),
 (116,8,'Page Numbers','page-numbers.htm',1800,'2013-11-17 02:02:34','','Course/FOC/8/page-numbers.htm',0,NULL),
 (117,8,'Inserting a Page Break','inserting-a-page-break.htm',1200,'2013-11-17 02:02:34','','Course/FOC/8/inserting-a-page-break.htm',0,NULL),
 (118,8,'Deleting a page break','deleting-a-page-break.htm',600,'2013-11-17 02:02:34','','Course/FOC/8/deleting-a-page-break.htm',0,NULL),
 (119,9,'Introduction','introduction.htm',600,'2013-11-17 02:17:26','','Course/FOC/9/introduction.htm',0,NULL),
 (120,9,'Features os Spreadsheets','features-of-spreadsheets.htm',1200,'2013-11-17 02:17:26','','Course/FOC/9/features-of-spreadsheets.htm',0,NULL),
 (121,9,'Features of MS-Excel 2007','features-of-ms-excel-2007.htm',600,'2013-11-17 02:17:26','','Course/FOC/9/features-of-ms-excel-2007.htm',0,NULL),
 (122,9,'Office themes and Excel styles','office-themes-and-excel-styles.htm',900,'2013-11-17 02:17:26','','Course/FOC/9/office-themes-and-excel-styles.htm',0,NULL),
 (123,9,'Formulas','formulas.htm',300,'2013-11-17 02:17:26','','Course/FOC/9/formulas.htm',0,NULL),
 (124,9,'Function AutoComplete','function-autocomplete.htm',600,'2013-11-17 02:17:26','','Course/FOC/9/function-autocomplete.htm',0,NULL),
 (125,9,'Sorting and Filtering','sorting-and-filtering.htm',1200,'2013-11-17 02:17:26','','Course/FOC/9/sorting-and-filtering.htm',0,NULL),
 (126,9,'Starting Excel','starting-excel.htm',600,'2013-11-17 02:17:26','','Course/FOC/9/starting-excel.htm',0,NULL),
 (127,9,'Excel Worksheet','excel-worksheet.htm',1200,'2013-11-17 02:17:26','','Course/FOC/9/excel-worksheet.htm',0,NULL),
 (128,9,'Selecting, Adding and Renaming Worksheets','selecting-adding-and-renaming-worksheets.htm',900,'2013-11-17 02:17:26','','Course/FOC/9/selecting-adding-and-renaming-worksheets.htm',0,NULL),
 (129,9,'Selecting Cells and Ranges','selecting-cells-and-ranges.htm',900,'2013-11-17 02:17:26','','Course/FOC/9/selecting-cells-and-ranges.htm',0,NULL),
 (130,9,'Navigating the Worksheet','navigating-the-worksheet.htm',600,'2013-11-17 02:17:27','','Course/FOC/9/navigating-the-worksheet.htm',0,NULL),
 (131,9,'Data Entry','data-entry.htm',1200,'2013-11-17 02:17:27','','Course/FOC/9/data-entry.htm',0,NULL),
 (132,9,'Editing Data','editing-data.htm',1200,'2013-11-17 02:17:27','','Course/FOC/9/editing-data.htm',0,NULL),
 (133,9,'Cell References','cell-references.htm',600,'2013-11-17 02:17:27','','Course/FOC/9/cell-references.htm',0,NULL),
 (134,9,'Find and Replace','find-and-replace.htm',900,'2013-11-17 02:17:27','','Course/FOC/9/find-and-replace.htm',0,NULL),
 (135,9,'Modifying a Worksheet','modifying-a-worksheet.htm',2400,'2013-11-17 02:17:27','','Course/FOC/9/modifying-a-worksheet.htm',0,NULL),
 (136,9,'Resizing Rows and Columns','modifying-a-worksheet.htm',1200,'2013-11-17 02:17:27','','Course/FOC/9/resizing-rows-and-columns.htm',0,NULL),
 (137,9,'Insert moved or copied cells','resizing-rows-and-columns.htm',900,'2013-11-17 02:17:27','','Course/FOC/9/insert-moved-or-copied-cells.htm',0,NULL),
 (138,9,'Move or copy the contents of a cell','insert-moved-or-copied-cells.htm',900,'2013-11-17 02:17:27','','Course/FOC/9/move-or-copy-the-contents-of-cell.htm',0,NULL),
 (139,9,'Copy cell values, cell formats, or formulas only','move-or-copy-the-contents-of-cell.htm',900,'2013-11-17 02:17:27','','Course/FOC/9/copy-cell-values-formats-formulas.htm',0,NULL),
 (140,9,'Drag and Drop','drag-and-drop.htm',600,'2013-11-17 02:17:27','','Course/FOC/9/drag-and-drop.htm',0,NULL),
 (141,9,'Freez Pangs','freeze-pans.htm',1200,'2013-11-17 02:17:27','','Course/FOC/9/freeze-pans.htm',0,NULL),
 (142,9,'Page Breaks','page-breaks.htm',600,'2013-11-17 02:17:27','','Course/FOC/9/page-breaks.htm',0,NULL),
 (143,9,'Page Setup','page-setup.htm',1800,'2013-11-17 02:17:27','','Course/FOC/9/page-setup.htm',0,NULL),
 (144,9,'Print Preview','print-preview.htm',600,'2013-11-17 02:17:27','','Course/FOC/9/print-preview.htm',0,NULL),
 (145,9,'Print','print.htm',600,'2013-11-17 02:17:27','','Course/FOC/9/print.htm',0,NULL),
 (146,9,'File Open, Save and Close','file-open-save-and-close.htm',1800,'2013-11-17 02:17:27','','Course/FOC/9/file-open-save-and-close.htm',0,NULL),
 (147,9,'Format Cells','format-cells.htm',2400,'2013-11-17 02:17:27','','Course/FOC/9/format-cells.htm',0,NULL),
 (148,9,'Format Cell Dialog Box','format-cell-dialog-box.htm',1800,'2013-11-17 02:17:27','','Course/FOC/9/format-cell-dialog-box.htm',0,NULL),
 (149,9,'Date and Time','date-and-time.htm',1200,'2013-11-17 02:17:27','','Course/FOC/9/date-and-time.htm',0,NULL),
 (150,9,'Format Coulumns and Rows','format-columns-and-rows.htm',600,'2013-11-17 02:17:27','','Course/FOC/9/format-columns-and-rows.htm',0,NULL),
 (151,9,'AutoFit Columns','autofit-columns.htm',1200,'2013-11-17 02:17:27','','Course/FOC/9/autofit-columns.htm',0,NULL),
 (152,9,'Hide Column or Row','hide-column-or-row.htm',900,'2013-11-17 02:17:27','','Course/FOC/9/hide-column-or-row.htm',0,NULL),
 (153,9,'Unhide Column or Row','unhide-column-or-row.htm',600,'2013-11-17 02:17:27','','Course/FOC/9/unhide-column-or-row.htm',0,NULL),
 (154,9,'Formulas and Functions','formulas-and-functions.htm',1800,'2013-11-17 02:17:27','','Course/FOC/9/formulas-and-functions.htm',0,NULL),
 (155,9,'Copy a Formula','copy-a-formula.htm',900,'2013-11-17 02:17:27','','Course/FOC/9/copy-a-formula.htm',0,NULL),
 (156,9,'Auto sum feature','auto-sum-features.htm',3600,'2013-11-17 02:17:27','','Course/FOC/9/auto-sum-features.htm',0,NULL),
 (157,9,'Charts','charts.htm',300,'2013-11-17 02:17:27','','Course/FOC/9/charts.htm',0,NULL),
 (158,9,'Types of Chart','types-of-charts.htm',2400,'2013-11-17 02:17:27','','Course/FOC/9/types-of-charts.htm',0,NULL),
 (159,9,'Components of a Chart','components-of-a-chart.htm',1200,'2013-11-17 02:17:27','','Course/FOC/9/components-of-a-chart.htm',0,NULL),
 (160,9,'Draw a Chart','draw-a-chart.htm',5400,'2013-11-17 02:17:27','','Course/FOC/9/draw-a-chart.htm',0,NULL),
 (161,9,'Editing of a Chart','editing-of-a-chart.htm',2400,'2013-11-17 02:17:27','','Course/FOC/9/editing-of-a-chart.htm',0,NULL),
 (162,9,'Resizing the Chart','resizing-the-chart.htm',600,'2013-11-17 02:17:27','','Course/FOC/9/resizing-the-chart.htm',0,NULL),
 (163,9,'Moving the Chart','moving-the-chart.htm',300,'2013-11-17 02:17:28','','Course/FOC/9/moving-the-chart.htm',0,NULL),
 (164,9,'Copying the Chart to Microsoft word','copying-the-chart.htm',600,'2013-11-17 02:17:28','','Course/FOC/9/copying-the-chart.htm',0,NULL),
 (165,9,'Graphics - Autoshapes and Smart Art','graphics-autoshapes-and-smart-art.htm',2400,'2013-11-17 02:17:28','','Course/FOC/9/graphics-autoshapes-and-smart-art.htm',0,NULL),
 (166,9,'Smart Art Graphics','smart-art-graphics.htm',900,'2013-11-17 02:17:28','','Course/FOC/9/smart-art-graphics.htm',0,NULL),
 (167,9,'Adding Clip Art','adding-clip-art.htm',1800,'2013-11-17 02:17:28','','Course/FOC/9/adding-clip-art.htm',0,NULL),
 (168,9,'Inserting and Editing a Picture from a File','inserting-and-editing-picture.htm',1200,'2013-11-17 02:17:28','','Course/FOC/9/inserting-and-editing-picture.htm',0,NULL),
 (169,10,'Microsoft Powerpoint','microsoft-powerpoint.htm',900,'2013-11-17 02:28:12','','Course/FOC/10/microsoft-powerpoint.htm',0,NULL),
 (170,10,'Introduction','introduction.htm',1200,'2013-11-17 02:28:12','','Course/FOC/10/introduction.htm',0,NULL),
 (171,10,'Starting a Powerpoint','starting-a-powerpoint.htm',3600,'2013-11-17 02:28:12','','Course/FOC/10/starting-a-powerpoint.htm',0,NULL),
 (172,10,'Installed Templates','installed-templates.htm',1800,'2013-11-17 02:28:12','','Course/FOC/10/installed-templates.htm',0,NULL),
 (173,10,'Design Template','design-template.htm',1200,'2013-11-17 02:28:12','','Course/FOC/10/design-template.htm',0,NULL),
 (174,10,'Blank Presentations','blank-presentations.htm',1800,'2013-11-17 02:28:12','','Course/FOC/10/blank-presentations.htm',0,NULL),
 (175,10,'Slide Layouts','slide-layouts.htm',3600,'2013-11-17 02:28:12','','Course/FOC/10/slide-layouts.htm',0,NULL),
 (176,10,'Selecting Content','selecting-content.htm',1800,'2013-11-17 02:28:12','','Course/FOC/10/selecting-content.htm',0,NULL),
 (177,10,'Open and Existing Presentations','open-an-existing-presentation.htm',1800,'2013-11-17 02:28:12','','Course/FOC/10/open-an-existing-presentation.htm',0,NULL),
 (178,10,'Viewing slides','viewing-slides.htm',1800,'2013-11-17 02:28:12','','Course/FOC/10/viewing-slides.htm',0,NULL),
 (179,10,'Normal View','normal-view.htm',1800,'2013-11-17 02:28:12','','Course/FOC/10/normal-view.htm',0,NULL),
 (180,10,'Slide Sorter View','slide-sorter-view.htm',1800,'2013-11-17 02:28:12','','Course/FOC/10/slide-sorter-view.htm',0,NULL),
 (181,10,'Slide Show View','slide-show-view.htm',1200,'2013-11-17 02:28:12','','Course/FOC/10/slide-show-view.htm',0,NULL),
 (182,10,'Design Tips','design-tips.htm',1800,'2013-11-17 02:28:12','','Course/FOC/10/design-tips.htm',0,NULL),
 (183,10,'Working with Slides','working-with-slides.htm',1800,'2013-11-17 02:28:12','','Course/FOC/10/working-with-slides.htm',0,NULL),
 (184,10,'Applying a Design Templates','applying-design-template.htm',1800,'2013-11-17 02:28:12','','Course/FOC/10/applying-design-template.htm',0,NULL),
 (185,10,'Changing Slide Layouts','changing-slide-layouts.htm',1800,'2013-11-17 02:28:12','','Course/FOC/10/changing-slide-layouts.htm',0,NULL),
 (186,10,'Insert/Edit Existing Slides as Your New Slides','insert-or-edit-existing-slides.htm',3600,'2013-11-17 02:28:12','','Course/FOC/10/insert-or-edit-existing-slides.htm',0,NULL),
 (187,10,'Reordering Slides','reordering-slides.htm',900,'2013-11-17 02:28:12','','Course/FOC/10/reordering-slides.htm',0,NULL),
 (188,10,'Hide Slides','hide-slides.htm',600,'2013-11-17 02:28:12','','Course/FOC/10/hide-slides.htm',0,NULL),
 (189,10,'Moving Between Slides','moving-between-slides.htm',1800,'2013-11-17 02:28:12','','Course/FOC/10/moving-between-slides.htm',0,NULL),
 (190,10,'Working with Text','working-with-text.htm',1800,'2013-11-17 02:28:13','','Course/FOC/10/working-with-text.htm',0,NULL),
 (191,10,'Inserting Text','inserting-text.htm',1800,'2013-11-17 02:28:13','','Course/FOC/10/inserting-text.htm',0,NULL),
 (192,10,'Formatting Text','formatting-text.htm',7200,'2013-11-17 02:28:13','','Course/FOC/10/formatting-text.htm',0,NULL),
 (193,10,'Text Box Properties','text-box-properties.htm',2400,'2013-11-17 02:28:13','','Course/FOC/10/text-box-properties.htm',0,NULL),
 (194,10,'Adding Notes','adding-notes.htm',1200,'2013-11-17 02:28:13','','Course/FOC/10/adding-notes.htm',0,NULL),
 (195,10,'Spell Check','spell-check.htm',3600,'2013-11-17 02:28:13','','Course/FOC/10/spell-check.htm',0,NULL),
 (196,10,'Saving and Printing','saving-and-printing.htm',900,'2013-11-17 02:28:13','','Course/FOC/10/saving-and-printing.htm',0,NULL),
 (197,10,'Page Setup','page-setup.htm',2400,'2013-11-17 02:28:13','','Course/FOC/10/page-setup.htm',0,NULL),
 (198,10,'Save as File','save-as-file.htm',900,'2013-11-17 02:28:13','','Course/FOC/10/save-as-file.htm',0,NULL),
 (199,10,'Save as Web Page','save-as-web-page.htm',1200,'2013-11-17 02:28:13','','Course/FOC/10/save-as-web-page.htm',0,NULL),
 (200,10,'Print','print.htm',1800,'2013-11-17 02:28:13','','Course/FOC/10/print.htm',0,NULL),
 (201,10,'Close Document','close-document.htm',600,'2013-11-17 02:28:13','','Course/FOC/10/close-document.htm',0,NULL),
 (202,10,'Exit Powerpoint Program','exit-powerpoint-program.htm',600,'2013-11-17 02:28:13','','Course/FOC/10/exit-powerpoint-program.htm',0,NULL),
 (203,10,'Keyboard Shortcuts','keyboard-shortcuts.htm',3600,'2013-11-17 02:28:13','','Course/FOC/10/keyboard-shortcuts.htm',0,NULL),
 (204,11,'Computer Networks','computer-networks.htm',900,'2013-11-17 02:33:21','','Course/FOC/11/computer-networks.htm',0,NULL),
 (205,11,'Local Area Network','local-area-network.htm',1200,'2013-11-17 02:33:21','','Course/FOC/11/local-area-network.htm',0,NULL),
 (206,11,'Metropolitan Area Network','metropolitan-area-network.htm',900,'2013-11-17 02:33:21','','Course/FOC/11/metropolitan-area-network.htm',0,NULL),
 (207,11,'Wide Area Network','wide-area-network.htm',900,'2013-11-17 02:33:21','','Course/FOC/11/wide-area-network.htm',0,NULL),
 (208,11,'Protocols','protocols.htm',1200,'2013-11-17 02:33:21','','Course/FOC/11/protocols.htm',0,NULL),
 (209,11,'Internet Protocol','nternet-protocol.htm',600,'2013-11-17 02:33:21','','Course/FOC/11/internet-protocol.htm',0,NULL),
 (210,11,'Post Office Protocol','postoffice-protocol.htm',2100,'2013-11-17 02:33:21','','Course/FOC/11/postoffice-protocol.htm',0,NULL),
 (211,11,'Hyper Text Transfer Protocol','hyper-text-transfer-protocol.htm',600,'2013-11-17 02:33:21','','Course/FOC/11/hyper-text-transfer-protocol.htm',0,NULL),
 (212,11,'File Transfer Protocol','file-transfer-protocol.htm',600,'2013-11-17 02:33:21','','Course/FOC/11/file-transfer-protocol.htm',0,NULL),
 (213,11,'IP Address','ip-address.htm',900,'2013-11-17 02:33:21','','Course/FOC/11/ip-address.htm',0,NULL),
 (214,11,'Share a Printer','share-a-printer.htm',300,'2013-11-17 02:33:21','','Course/FOC/11/share-a-printer.htm',0,NULL),
 (215,11,'File and Printer Sharing','file-and-printer-sharing.htm',600,'2013-11-17 02:33:21','','Course/FOC/11/file-and-printer-sharing.htm',0,NULL),
 (216,11,'Sharing Printers','sharing-printers.htm',1200,'2013-11-17 02:33:21','','Course/FOC/11/sharing-printers.htm',0,NULL),
 (217,11,'Stop Printer Sharing','stop-printer-sharing.htm',600,'2013-11-17 02:33:21','','Course/FOC/11/stop-printer-sharing.htm',0,NULL),
 (218,11,'Connect to Printer','connect-to-printer.htm',3600,'2013-11-17 02:33:21','','Course/FOC/11/connect-to-printer.htm',0,NULL),
 (219,11,'Setting or Removing Permissions','setting-or-removing-permissions.htm',1800,'2013-11-17 02:33:21','','Course/FOC/11/setting-or-removing-permissions.htm',0,NULL),
 (220,12,'Introduction','introduction.htm',900,'2013-11-17 05:35:39','','Course/FOC/12/introduction.htm',0,NULL),
 (221,12,'Antivirus','antivirus.htm',900,'2013-11-17 05:35:39','','Course/FOC/12/antivirus.htm',0,NULL),
 (222,12,'Popular Antivirus Software','popular-antivirus-software.htm',1800,'2013-11-17 05:35:39','','Course/FOC/12/popular-antivirus-software.htm',0,NULL),
 (223,12,'Best Practices','best-practices.htm',1200,'2013-11-17 05:35:40','','Course/FOC/12/best-practices.htm',0,NULL),
 (224,12,'Firewall','firewall.htm',900,'2013-11-17 05:35:40','','Course/FOC/12/firewall.htm',0,NULL),
 (225,12,'Configure Windows XP Firewall','configure-windows-xp-firewall.htm',1800,'2013-11-17 05:35:40','','Course/FOC/12/configure-windows-xp-firewall.htm',0,NULL),
 (226,12,'Best Practices','best-practices-firewall.htm',900,'2013-11-17 05:35:40','','Course/FOC/12/best-practices-firewall.htm',0,NULL),
 (227,12,'Security Essentials','security-essentials.htm',1200,'2013-11-17 05:35:40','','Course/FOC/12/security-essentials.htm',0,NULL),
 (228,12,'Safe Mode','safe-mode.htm',900,'2013-11-17 05:35:40','','Course/FOC/12/safe-mode.htm',0,NULL),
 (229,12,'Start the Computer in Safe Mode','start-the-computer-in-safe-mode.htm',900,'2013-11-17 05:35:40','','Course/FOC/12/start-the-computer-in-safe-mode.htm',0,NULL),
 (230,12,'MSConfig Utility','msconfig-utility.htm',2700,'2013-11-17 05:35:40','','Course/FOC/12/msconfig-utility.htm',0,NULL),
 (231,13,'Introduction','introduction.htm',600,'2013-11-17 05:41:00','','Course/FOC/13/introduction.htm',0,NULL),
 (232,13,'Browsers','browsers.htm',1200,'2013-11-17 05:41:00','','Course/FOC/13/browsers.htm',0,NULL),
 (233,13,'Popular Web Browsers','popular-web-browsers.htm',1200,'2013-11-17 05:41:00','','Course/FOC/13/popular-web-browsers.htm',0,NULL),
 (234,13,'Web Browser User Interface','web-browser-user-interface.htm',900,'2013-11-17 05:41:00','','Course/FOC/13/web-browser-user-interface.htm',0,NULL),
 (235,13,'Internet Options','internet-options.htm',900,'2013-11-17 05:41:00','','Course/FOC/13/internet-options.htm',0,NULL),
 (236,13,'Cleanup History','cleanup-history.htm',3600,'2013-11-17 05:41:00','','Course/FOC/13/cleanup-history.htm',0,NULL),
 (237,13,'Protocol and Security','protocol-and-security.htm',1200,'2013-11-17 05:41:00','','Course/FOC/13/protocol-and-security.htm',0,NULL),
 (238,13,'EMAIL System','email-system.htm',900,'2013-11-17 05:41:00','','Course/FOC/13/email-system.htm',0,NULL),
 (239,13,'Popular Email Service Providers','popular-email-service-providers.htm',600,'2013-11-17 05:41:00','','Course/FOC/13/popular-email-service-providers.htm',0,NULL),
 (240,13,'Password Strength','password-strength.htm',600,'2013-11-17 05:41:00','','Course/FOC/13/password-strength.htm',0,NULL),
 (241,13,'SPAM Emails','spam-emails.htm',900,'2013-11-17 05:41:00','','Course/FOC/13/spam-emails.htm',0,NULL),
 (242,13,'Social Engineering Emails','social-engineering-emails.htm',1200,'2013-11-17 05:41:00','','Course/FOC/13/social-engineering-emails.htm',0,NULL),
 (243,13,'Email Best Practices','email-best-practices.htm',1200,'2013-11-17 05:41:00','','Course/FOC/13/email-best-practices.htm',0,NULL),
 (244,13,'Search Engines','search-engines.htm',600,'2013-11-17 05:41:00','','Course/FOC/13/search-engines.htm',0,NULL),
 (245,13,'Popular Search Engines','popular-search-engines.htm',600,'2013-11-17 05:41:00','','Course/FOC/13/popular-search-engines.htm',0,NULL),
 (246,13,'Google Tricks','google-tricks.htm',1200,'2013-11-17 05:41:00','','Course/FOC/13/google-tricks.htm',0,NULL),
 (247,13,'Downloads and Installations','downloads-and-installations.htm',900,'2013-11-17 05:41:00','','Course/FOC/13/downloads-and-installations.htm',0,NULL),
 (248,14,'Introduction','introduction.htm',900,'2013-11-17 05:43:28','','Course/FOC/14/introduction.htm',0,NULL),
 (249,14,'Social Media Websites','social-media-websites.htm',900,'2013-11-17 05:43:28','','Course/FOC/14/social-media-websites.htm',0,NULL),
 (250,14,'Popular Social Media','popular-social-media.htm',1800,'2013-11-17 05:43:28','','Course/FOC/14/popular-social-media.htm',0,NULL),
 (251,14,'Best Practices','best-practices.htm',2700,'2013-11-17 05:43:29','','Course/FOC/14/best-practices.htm',0,NULL);
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
 (1,'Fundamental of Computers','2013-10-26 18:53:46');
/*!40000 ALTER TABLE `coursedetails` ENABLE KEYS */;


--
-- Definition of table `coursefiles`
--

DROP TABLE IF EXISTS `coursefiles`;
CREATE TABLE `coursefiles` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `name` varchar(45) NOT NULL,
  `displayname` varchar(45) NOT NULL,
  `caption` varchar(45) NOT NULL,
  `contenttype` varchar(45) character set utf8 NOT NULL,
  `contentsize` int(10) unsigned NOT NULL default '0',
  `thumbnail` varchar(255) character set utf8 default NULL,
  `attachlink` varchar(1024) character set utf8 default NULL,
  `userId` int(10) unsigned NOT NULL,
  `courseId` int(10) unsigned NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_coursefiles_1` (`userId`),
  KEY `FK_coursefiles_2` (`courseId`),
  CONSTRAINT `FK_coursefiles_1` FOREIGN KEY (`userId`) REFERENCES `userdetails` (`Id`),
  CONSTRAINT `FK_coursefiles_2` FOREIGN KEY (`courseId`) REFERENCES `coursedetails` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `coursefiles`
--

/*!40000 ALTER TABLE `coursefiles` DISABLE KEYS */;
/*!40000 ALTER TABLE `coursefiles` ENABLE KEYS */;


--
-- Definition of table `coursetests`
--

DROP TABLE IF EXISTS `coursetests`;
CREATE TABLE `coursetests` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `testname` varchar(255) character set utf8 NOT NULL,
  `courseId` int(10) unsigned NOT NULL,
  `chapters` varchar(255) character set utf8 default NULL,
  `IsTimebound` tinyint(3) unsigned default '0',
  `timelimit` int(10) unsigned NOT NULL default '0',
  `totalquestions` int(10) unsigned NOT NULL default '10',
  `teststatus` tinyint(3) unsigned default '1',
  `startdate` datetime NOT NULL,
  `enddate` datetime NOT NULL,
  `datecreated` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  `lastupdated` datetime NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_coursetests_1` (`courseId`),
  CONSTRAINT `FK_coursetests_1` FOREIGN KEY (`courseId`) REFERENCES `coursedetails` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `coursetests`
--

/*!40000 ALTER TABLE `coursetests` DISABLE KEYS */;
/*!40000 ALTER TABLE `coursetests` ENABLE KEYS */;


--
-- Definition of table `finalquizmaster`
--

DROP TABLE IF EXISTS `finalquizmaster`;
CREATE TABLE `finalquizmaster` (
  `Id` int(10) unsigned NOT NULL auto_increment,
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
) ENGINE=InnoDB AUTO_INCREMENT=295 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `finalquizmaster`
--

/*!40000 ALTER TABLE `finalquizmaster` DISABLE KEYS */;
INSERT INTO `finalquizmaster` (`Id`,`courseId`,`chapterId`,`sectionId`,`groupNo`,`complexity`,`questionText`,`IsQuestionOptionPresent`,`QuestionOption`,`AnswerOption`,`ContentVersion`,`DateCreated`,`createdby`) VALUES 
 (1,1,2,19,1,1,'High-end monitors can have which of the following  resolutions ?\n\n',0,'[]','[{\"AnswerOption\":\"320 x 480\",\"IsCurrect\":false},{\"AnswerOption\":\"800 x 600\",\"IsCurrect\":false},{\"AnswerOption\":\"1600 x 900\",\"IsCurrect\":true},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-17 02:46:43','admin'),
 (2,1,1,1,1,1,'Computer components are divided into which two major categories?',0,'[]','[{\"AnswerOption\":\"Input and output\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware and Software\",\"IsCurrect\":true},{\"AnswerOption\":\"Processing and Storage\",\"IsCurrect\":false},{\"AnswerOption\":\"Control Unit and Arithmetic Logic Unit\",\"IsCurrect\":false}]','1','2013-11-17 00:34:49','admin'),
 (3,1,9,144,1,1,'Which option is used to view the worksheet before printout is taken?',0,'[]','[{\"AnswerOption\":\"Modifying a worksheet\",\"IsCurrect\":false},{\"AnswerOption\":\"Print preview\",\"IsCurrect\":true},{\"AnswerOption\":\"Format Cells\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-20 23:28:00','admin'),
 (4,1,4,47,1,1,'How to lock the windows operating system?',0,'[]','[{&quot;AnswerOption&quot;:&quot;press ctrl %2b alt %2b del&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;press alt %2b tab %2b del&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 13:13:33','admin'),
 (5,1,1,7,1,1,'Which is generalized software package?',0,'[]','[{\"AnswerOption\":\"Word Processing Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Database Management System\",\"IsCurrect\":false},{\"AnswerOption\":\"Spreadsheets\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-17 01:29:59','admin'),
 (6,1,1,2,1,1,'Which component performs arithmetic operations?',0,'[]','[{\"AnswerOption\":\"Output\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware\",\"IsCurrect\":false},{\"AnswerOption\":\"Arithmetic Logic Unit (ALU)\",\"IsCurrect\":true},{\"AnswerOption\":\"Control Unit(CU)\",\"IsCurrect\":false}]','1','2013-11-17 01:05:49','admin'),
 (7,1,7,69,1,1,'Which is application software?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Office suites &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Graphics software&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Media players&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 15:06:14','admin'),
 (8,1,5,51,1,1,'Which file details are associated about the file? ',0,'[]','[{\"AnswerOption\":\"Name\",\"IsCurrect\":false},{\"AnswerOption\":\"Type\",\"IsCurrect\":false},{\"AnswerOption\":\"Size \",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-20 23:17:53','admin'),
 (9,1,4,35,1,1,'Which is not a Microsoft operating system?',0,'[]','[{\"AnswerOption\":\"Windows 7\",\"IsCurrect\":false},{\"AnswerOption\":\"Windows XP\",\"IsCurrect\":false},{\"AnswerOption\":\"Solaris\",\"IsCurrect\":true},{\"AnswerOption\":\"Windows 8\",\"IsCurrect\":false}]','1','2013-11-17 03:36:17','admin'),
 (10,1,8,101,1,1,'How user can change the look feel of the text in Word 2007?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Inserting Text&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Replacing Text&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Formatting Text&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Deleting Text&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 15:35:53','admin'),
 (11,1,1,5,1,1,'_______________ programs bridge the gap between the functionality of operating systems and the needs of the users.',0,'[]','[{\"AnswerOption\":\"Operating System\",\"IsCurrect\":false},{\"AnswerOption\":\"Utility\",\"IsCurrect\":true},{\"AnswerOption\":\"System Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Application Software\",\"IsCurrect\":false}]','1','2013-11-17 01:22:52','admin'),
 (12,1,2,11,1,1,'Which options could be used to create Upper case/Capital characters?',0,'[]','[{\"AnswerOption\":\"Press CAPS Lock key\",\"IsCurrect\":false},{\"AnswerOption\":\"Press Shift + alphabet key\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-17 01:55:50','admin'),
 (13,1,10,181,1,1,'Which view is used to preview the presentation?',0,'[]','[{\"AnswerOption\":\"To preview of slide\",\"IsCurrect\":true},{\"AnswerOption\":\"To add new slide to slide show\",\"IsCurrect\":false},{\"AnswerOption\":\"To delete slide from slide show\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-21 22:48:53','admin'),
 (14,1,10,201,1,1,'What happens after executing the close command?',0,'[]','[{\"AnswerOption\":\"Close the current presentation slides file\",\"IsCurrect\":false},{\"AnswerOption\":\"Prompted to save the file before closing\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 23:38:19','admin'),
 (15,1,9,138,1,1,'Is it possible to move cell without moving it&#39;s data?',0,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":false},{\"AnswerOption\":\"False\",\"IsCurrect\":true}]','1','2013-11-21 20:50:48','admin'),
 (16,1,6,67,1,1,'Which tool is used to enlarge picture?',0,'[]','[{&quot;AnswerOption&quot;:&quot;The Free-Form Select Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Paint Brush Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Magnifier Tool&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;The Opaque Option Tool&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 13:52:39','admin'),
 (17,1,3,24,1,1,'Storage space is expressed in:',0,'[]','[{&quot;AnswerOption&quot;:&quot;Kilobyte (KB) &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Megabyte (MB)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Gigabyte (GB)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All of the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 11:32:40','admin'),
 (18,1,1,6,1,1,'Application software classification includes:',0,'[]','[{\"AnswerOption\":\"Hardware and Software \",\"IsCurrect\":false},{\"AnswerOption\":\"Generalized packages and Customized packages\",\"IsCurrect\":true},{\"AnswerOption\":\"Input and Output\",\"IsCurrect\":false},{\"AnswerOption\":\"System Software and Application Software\",\"IsCurrect\":false}]','1','2013-11-17 01:24:44','admin'),
 (19,1,2,13,1,1,'Which device translates printed images into an electronic format?',0,'[]','[{\"AnswerOption\":\"Input devices\",\"IsCurrect\":false},{\"AnswerOption\":\"output devices\",\"IsCurrect\":false},{\"AnswerOption\":\"Image scanner\",\"IsCurrect\":true},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-17 02:10:02','admin'),
 (20,1,2,13,1,1,'Which device is used to extract the text from the scanned image?',0,'[]','[{\"AnswerOption\":\"Bar code reader\",\"IsCurrect\":false},{\"AnswerOption\":\"Optical character recognition (OCR)\",\"IsCurrect\":true},{\"AnswerOption\":\"Image scanner\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-17 02:28:46','admin'),
 (21,1,11,205,1,1,'Which of the following is used to connect networks in larger geographic areas, such as India or the world.?',0,'[]','[{\"AnswerOption\":\"Local Area Network (LAN)\",\"IsCurrect\":false},{\"AnswerOption\":\"Wide Area Network (WAN) \",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 01:21:37','admin'),
 (22,1,10,180,1,1,'Slide sorter view is use to _____.',0,'[]','[{\"AnswerOption\":\"Set order of the slide\",\"IsCurrect\":false},{\"AnswerOption\":\"Sort the slide\",\"IsCurrect\":false},{\"AnswerOption\":\"Add special effects\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-21 22:45:45','admin'),
 (23,1,10,185,1,1,'Layout command is present in ________.',0,'[]','[{\"AnswerOption\":\"Header bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Menu bar\",\"IsCurrect\":true},{\"AnswerOption\":\"Side bar\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-21 22:59:51','admin'),
 (24,1,8,104,1,1,'Which of the following action pushes the text down to the next line, but does not create a new paragraph?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Press CTRL %2B TAB keys&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Press SHIFT %2B ENTER keys&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Press CTRL PLUS ALT %2B DELETE&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Press SHIFT PLUS CTRL %2B TAB&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 15:40:12','admin'),
 (25,1,8,76,1,1,'Which application programs allows user to create letter, report etc.?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Microsoft Excel&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Microsoft PowerPoint&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Word processing&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 15:24:15','admin'),
 (26,1,4,41,1,1,'To quit a program, click on?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Close button&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;File menu and click on Close option&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 12:26:29','admin'),
 (27,1,3,27,1,1,'Which is temporary memory?',0,'[]','[{\"AnswerOption\":\"Secondary Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Primary Memory\",\"IsCurrect\":true},{\"AnswerOption\":\"Real time memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Flash memory\",\"IsCurrect\":false}]','1','2013-11-17 02:57:05','admin'),
 (28,1,3,27,1,1,'The main board of a computer is known as:',0,'[]','[{\"AnswerOption\":\"Electric board \",\"IsCurrect\":false},{\"AnswerOption\":\"Black board\",\"IsCurrect\":false},{\"AnswerOption\":\"Mother board\",\"IsCurrect\":true},{\"AnswerOption\":\"White board\",\"IsCurrect\":false}]','1','2013-11-17 03:02:35','admin'),
 (29,1,4,33,1,1,'Which software program helps computer hardware to operate with software?',0,'[]','[{\"AnswerOption\":\"Word\",\"IsCurrect\":false},{\"AnswerOption\":\"Operating system\",\"IsCurrect\":true},{\"AnswerOption\":\"Games\",\"IsCurrect\":false},{\"AnswerOption\":\"Google\",\"IsCurrect\":false}]','1','2013-11-17 03:29:44','admin'),
 (30,1,4,34,1,1,'Which are the main functions of the operating system ?\n',0,'[]','[{\"AnswerOption\":\"Memory management\",\"IsCurrect\":false},{\"AnswerOption\":\"Process management\",\"IsCurrect\":false},{\"AnswerOption\":\"Device management\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-17 03:50:55','admin'),
 (31,1,5,60,1,1,'Which hardware devices could be installed using Windows operating system?',0,'[]','[{\"AnswerOption\":\"sound card\",\"IsCurrect\":false},{\"AnswerOption\":\"network card\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 00:58:20','admin'),
 (32,1,14,249,1,1,'_______ is one kind of social media website.',0,'[]','[{\"AnswerOption\":\"News\",\"IsCurrect\":false},{\"AnswerOption\":\"Networking\",\"IsCurrect\":false},{\"AnswerOption\":\"Photo and Video Sharing\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-22 01:51:45','admin'),
 (33,1,13,236,1,1,'Website access is tracked in web browser to maintain it as a history.',0,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-22 01:31:23','admin'),
 (34,1,8,108,1,1,'______________ is used to get reader attention to main points.',0,'[]','[{&quot;AnswerOption&quot;:&quot;Paragraph spacing&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Line spacing&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Line markers&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Bulleted and Numbered List&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 15:43:43','admin'),
 (35,1,8,78,1,1,'New component in Word 2007 which groups similar tasks is known as:',0,'[]','[{&quot;AnswerOption&quot;:&quot;Tab&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Ribbon&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of these&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 15:25:43','admin'),
 (36,1,6,66,1,1,'Mouse left click on color palette is used to select:',0,'[]','[{&quot;AnswerOption&quot;:&quot;Background color&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Foreground color&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of these&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 13:37:43','admin'),
 (37,1,3,31,1,1,'DVD stands for ?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Compact Disk&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Digital Versatile Disc&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 12:01:05','admin'),
 (38,1,6,64,1,1,'What is used as a digital sketchpad to make pictures? ',0,'[]','[{&quot;AnswerOption&quot;:&quot; Toolbar&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Rectangle Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Paint&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 13:32:25','admin'),
 (39,1,6,65,1,1,'Which option is available on toolbar?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Magnifier&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Menu Items&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Curve&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and C&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 13:34:32','admin'),
 (40,1,13,240,1,1,'Password shall have only upper case letters.',0,'[]','[{\"AnswerOption\":\"Yes\",\"IsCurrect\":false},{\"AnswerOption\":\"No\",\"IsCurrect\":true}]','1','2013-11-22 01:35:41','admin'),
 (41,1,12,225,1,1,'The exception tab lets user to add exceptions to permit inbound traffic.',0,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-22 01:16:09','admin'),
 (42,1,14,248,1,1,'_________ is an internet based applications/websites which help people to interact with other people.',0,'[]','[{\"AnswerOption\":\"Social networking\",\"IsCurrect\":false},{\"AnswerOption\":\"Content communities\",\"IsCurrect\":false},{\"AnswerOption\":\"Wikipedia\",\"IsCurrect\":false},{\"AnswerOption\":\"Social media\",\"IsCurrect\":true}]','1','2013-11-22 01:49:07','admin'),
 (43,1,5,57,1,1,'User gets an option to choose location while installing a software.',0,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-21 00:42:36','admin'),
 (44,1,9,151,1,1,'AutoFit feature is use to adjust the _________.',0,'[]','[{\"AnswerOption\":\"Height\",\"IsCurrect\":false},{\"AnswerOption\":\"Width\",\"IsCurrect\":true},{\"AnswerOption\":\"Margin\",\"IsCurrect\":false},{\"AnswerOption\":\"Spacing\",\"IsCurrect\":false}]','1','2013-11-21 21:27:05','admin'),
 (45,1,11,204,1,1,'Which options are used to link the computers on a network?',0,'[]','[{\"AnswerOption\":\"Satellites\",\"IsCurrect\":false},{\"AnswerOption\":\"Bluetooth\",\"IsCurrect\":false},{\"AnswerOption\":\"Cables\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 01:16:28','admin'),
 (46,1,9,148,1,1,'Which tabs are present under Format Cells dialog box?',0,'[]','[{\"AnswerOption\":\"Number\",\"IsCurrect\":false},{\"AnswerOption\":\"Protection\",\"IsCurrect\":false},{\"AnswerOption\":\"Border and Patterns\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-20 23:32:08','admin'),
 (47,1,1,7,1,1,'Which of the following is used for Data Analysis?',0,'[]','[{\"AnswerOption\":\"Lotus Smart suites\",\"IsCurrect\":false},{\"AnswerOption\":\"Word Perfect\",\"IsCurrect\":false},{\"AnswerOption\":\"Apple Numbers\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and C\",\"IsCurrect\":true}]','1','2013-11-17 01:28:12','admin'),
 (48,1,6,68,1,1,'MS Paint could save image in _______________ format.',0,'[]','[{&quot;AnswerOption&quot;:&quot;Bitmap (bmp)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;JPEG (JPG)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Portable Network Graphics (PNG)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 14:03:49','admin'),
 (49,1,5,54,1,1,'Deleted files/folders are placed in _____________.',0,'[]','[{&quot;AnswerOption&quot;:&quot;Recycle Bin&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Memory&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Desktop&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 14:08:14','admin'),
 (50,1,8,84,1,1,'Which option could be used to scroll the document?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Click on arrow button present on the scroll bar (up arrow button or down arrow button)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Scroll up/down using scroll button present on mouse&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of these&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 15:32:15','admin'),
 (51,1,12,227,1,1,'_______ is free anti-malware software for your computer.',0,'[]','[{\"AnswerOption\":\"Antivirus Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Firewall\",\"IsCurrect\":false},{\"AnswerOption\":\"Computer security\",\"IsCurrect\":false},{\"AnswerOption\":\"Security essentials\",\"IsCurrect\":true}]','1','2013-11-21 00:59:29','admin'),
 (52,1,10,200,1,1,'Which option is present in Print dialog box?',0,'[]','[{\"AnswerOption\":\"Print range\",\"IsCurrect\":false},{\"AnswerOption\":\"Copies\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 00:35:29','admin'),
 (53,1,10,200,1,1,'Which option is available to print a slide show?',0,'[]','[{\"AnswerOption\":\"Print slide\",\"IsCurrect\":false},{\"AnswerOption\":\"Print hangout\",\"IsCurrect\":false},{\"AnswerOption\":\"Print Notes Page\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-21 23:35:30','admin'),
 (54,1,7,74,1,1,'Which is one of the font styles?',0,'[]','[{\"AnswerOption\":\"Oblique\",\"IsCurrect\":false},{\"AnswerOption\":\"Bold\",\"IsCurrect\":false},{\"AnswerOption\":\"Regular\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 02:13:11','admin'),
 (55,1,10,177,1,1,'How user could open the Power Point presentation?',0,'[]','[{\"AnswerOption\":\"Copy it\",\"IsCurrect\":false},{\"AnswerOption\":\"Delete it\",\"IsCurrect\":false},{\"AnswerOption\":\"Double Click on it\",\"IsCurrect\":true},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-21 22:39:38','admin'),
 (56,1,9,164,1,1,'Which option is used to copy chart to Word?',0,'[]','[{\"AnswerOption\":\"Copy and paste\",\"IsCurrect\":true},{\"AnswerOption\":\"Cut and paste\",\"IsCurrect\":false},{\"AnswerOption\":\"Delete a chart \",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-21 22:09:28','admin'),
 (57,1,10,203,1,1,'Short cut key for save as is ___________.',0,'[]','[{\"AnswerOption\":\"F1\",\"IsCurrect\":false},{\"AnswerOption\":\"F10\",\"IsCurrect\":false},{\"AnswerOption\":\"F11\",\"IsCurrect\":false},{\"AnswerOption\":\"F12\",\"IsCurrect\":true}]','1','2013-11-21 23:51:39','admin'),
 (58,1,13,237,1,1,'HTTP is based on __________.',0,'[]','[{\"AnswerOption\":\"Input/output devices\",\"IsCurrect\":false},{\"AnswerOption\":\"TCP/IP Protocols\",\"IsCurrect\":false},{\"AnswerOption\":\"Client/server principles\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 02:08:48','admin'),
 (59,1,12,222,1,1,'____ is popular freely available antivirus program.',0,'[]','[{\"AnswerOption\":\"Microsoft Security Essentials\",\"IsCurrect\":false},{\"AnswerOption\":\"AVG\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 00:47:55','admin'),
 (60,1,11,210,1,1,'___________ is type of protocol.',0,'[]','[{\"AnswerOption\":\"Simple mail transport Protocol (SMTP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Transmission control Protocol (TCP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 01:30:04','admin'),
 (61,1,9,135,1,1,'Is it possible to insert multiple rows simultaneously?',0,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-21 20:43:24','admin'),
 (62,1,4,36,1,1,'Password is required to login into the system.',0,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-21 23:56:43','admin'),
 (63,1,13,245,1,1,'_______ is one of the most popular search engines.',0,'[]','[{\"AnswerOption\":\"Windows Live\",\"IsCurrect\":false},{\"AnswerOption\":\"Google\",\"IsCurrect\":true},{\"AnswerOption\":\"Windows Live\",\"IsCurrect\":false},{\"AnswerOption\":\"Yahoo\",\"IsCurrect\":false}]','1','2013-11-22 01:43:29','admin'),
 (64,1,10,181,1,1,'Slide show is used to _____.',0,'[]','[{\"AnswerOption\":\"Normal\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide Sorter\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide Show\",\"IsCurrect\":true},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-21 00:29:25','admin'),
 (65,1,7,72,1,1,'Which shortcut key is used to print the notepad contents?',0,'[]','[{\"AnswerOption\":\"Ctrl + V\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl + S\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl + P\",\"IsCurrect\":true},{\"AnswerOption\":\"Ctrl + R\",\"IsCurrect\":false}]','1','2013-11-21 02:05:38','admin'),
 (66,1,5,56,1,1,'Programs and components are managed using ___________________',0,'[]','[{&quot;AnswerOption&quot;:&quot;Install Hardware&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Change or remove software&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Add or remove programs&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Install Software&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 14:52:26','admin'),
 (67,1,8,112,1,1,'Which are predefined margins?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Narrow&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Moderate&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Mirrored&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 15:47:15','admin'),
 (68,1,9,119,1,1,'Which is widely used spreadsheet applications?',0,'[]','[{\"AnswerOption\":\"Microsoft word\",\"IsCurrect\":false},{\"AnswerOption\":\"Microsoft excel\",\"IsCurrect\":true},{\"AnswerOption\":\"Microsoft  PowerPoint\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-20 23:15:35','admin'),
 (69,1,9,153,1,1,'Unhide feature is use to unhide _______.',0,'[]','[{\"AnswerOption\":\"Column\",\"IsCurrect\":false},{\"AnswerOption\":\"Row\",\"IsCurrect\":false},{\"AnswerOption\":\"Hide rows and columns\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 21:33:41','admin'),
 (70,1,11,217,1,1,'Stop Printer Sharing is present in ______ tab.',0,'[]','[{\"AnswerOption\":\"Ports\",\"IsCurrect\":false},{\"AnswerOption\":\"General\",\"IsCurrect\":false},{\"AnswerOption\":\"Advanced \",\"IsCurrect\":false},{\"AnswerOption\":\"Sharing\",\"IsCurrect\":true}]','1','2013-11-22 01:09:41','admin'),
 (71,1,5,49,1,1,'Which shortcut keys are used to open Windows Explorer?',0,'[]','[{\"AnswerOption\":\"Window Key + F\",\"IsCurrect\":false},{\"AnswerOption\":\"Window key + E\",\"IsCurrect\":true},{\"AnswerOption\":\"Window Key + C\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-20 22:54:38','admin'),
 (72,1,9,146,1,1,'What will happen if user saves the excel file with .doc extension?',0,'[]','[{\"AnswerOption\":\"Content of file may loss\",\"IsCurrect\":true},{\"AnswerOption\":\"Content of file remain same\",\"IsCurrect\":false},{\"AnswerOption\":\"Not possible to excel file with .doc extension\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above possible\",\"IsCurrect\":false}]','1','2013-11-21 21:19:57','admin'),
 (73,1,7,71,1,1,'Which shortcut key is used to save the content of notepad?',0,'[]','[{\"AnswerOption\":\"Ctrl + D\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl + S\",\"IsCurrect\":true},{\"AnswerOption\":\"Ctrl + E\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl + R\",\"IsCurrect\":false}]','1','2013-11-21 01:59:47','admin'),
 (74,1,9,143,1,1,'Page setup option contain _________.',0,'[]','[{\"AnswerOption\":\"Page option\",\"IsCurrect\":false},{\"AnswerOption\":\"Margin\",\"IsCurrect\":false},{\"AnswerOption\":\"Header and Footer\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-21 21:03:41','admin'),
 (75,1,11,210,1,1,'_________ is used to receive incoming e-mail.',0,'[]','[{\"AnswerOption\":\"Simple mail transport Protocol (SMTP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Transmission control Protocol (TCP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":true},{\"AnswerOption\":\"Internet Protocol (IP)\",\"IsCurrect\":false}]','1','2013-11-21 01:34:43','admin'),
 (76,1,10,169,1,1,'Microsoft powerpoint helps you in __________.',0,'[]','[{\"AnswerOption\":\"Creation of text files\",\"IsCurrect\":false},{\"AnswerOption\":\"Creation of video files\",\"IsCurrect\":false},{\"AnswerOption\":\"Creation of slid shows\",\"IsCurrect\":true},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-21 22:24:44','admin'),
 (77,1,5,59,1,1,'Windows features could be added/removed from Add remove programs.',0,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-21 00:53:38','admin'),
 (78,1,9,131,1,1,'Which different type of values could be entered in cell?',0,'[]','[{\"AnswerOption\":\"Numbers\",\"IsCurrect\":false},{\"AnswerOption\":\"Text\",\"IsCurrect\":false},{\"AnswerOption\":\"Date and Time\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-20 23:23:52','admin'),
 (79,1,10,175,1,1,'Which of the following are Slide Layouts?',0,'[]','[{\"AnswerOption\":\"Title Slide\",\"IsCurrect\":false},{\"AnswerOption\":\"Section Header\",\"IsCurrect\":false},{\"AnswerOption\":\"Text Content\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 00:11:01','admin'),
 (80,1,8,93,1,1,'Shortcut key for cut:',0,'[]','[{\"AnswerOption\":\"CTRL %2B X\",\"IsCurrect\":true},{\"AnswerOption\":\"CTRL %2B V\",\"IsCurrect\":false},{\"AnswerOption\":\"CTRL %2B P\",\"IsCurrect\":false},{\"AnswerOption\":\"CTRL %2B O\",\"IsCurrect\":false}]','1','2013-11-22 22:12:40','admin'),
 (81,1,9,147,1,1,'Which dialog box allows formatting style & numbers?',0,'[]','[{\"AnswerOption\":\"Modifying a worksheet\",\"IsCurrect\":false},{\"AnswerOption\":\"Save\",\"IsCurrect\":false},{\"AnswerOption\":\"Print\",\"IsCurrect\":false},{\"AnswerOption\":\"Format Cells\",\"IsCurrect\":true},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-20 23:30:20','admin'),
 (82,1,5,55,1,1,'Hidden files/folders could be made visible using folder options.',0,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-21 00:12:35','admin'),
 (83,1,10,189,1,1,'Which tool is moved from one slide to another slide?',0,'[]','[{\"AnswerOption\":\"Scroll Bars\",\"IsCurrect\":false},{\"AnswerOption\":\"Next Slide and Previous Slide Buttons\",\"IsCurrect\":false},{\"AnswerOption\":\"Using Outline Pane\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 00:32:14','admin'),
 (84,1,5,52,1,1,' A new folder is displayed with the default name as_____.',0,'[]','[{\"AnswerOption\":\"My Computer\",\"IsCurrect\":false},{\"AnswerOption\":\"New Folder\",\"IsCurrect\":true},{\"AnswerOption\":\"My Picture\",\"IsCurrect\":false},{\"AnswerOption\":\"Folder\",\"IsCurrect\":false}]','1','2013-11-20 23:24:13','admin'),
 (85,1,11,206,1,1,'__________ provides communication between two or more devices located in same city.',0,'[]','[{\"AnswerOption\":\"Servers\",\"IsCurrect\":false},{\"AnswerOption\":\"Metropolitan area network (MAN) \",\"IsCurrect\":true},{\"AnswerOption\":\"Protocols\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 01:26:56','admin'),
 (86,1,10,188,1,1,'Hide feature could be used to hide the slide in ________.',0,'[]','[{\"AnswerOption\":\"Slide show\",\"IsCurrect\":true},{\"AnswerOption\":\"Normal view\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide sorter view\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-21 23:06:46','admin'),
 (87,1,11,214,1,1,'Which of the following is a way to connect to a printer on a network?',0,'[]','[{\"AnswerOption\":\"Find a printer in the directory\",\"IsCurrect\":false},{\"AnswerOption\":\"Connect to this printer(Or to browse for a printer)\",\"IsCurrect\":false},{\"AnswerOption\":\"Connect to a printer on the Internet or on a home or office network.\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 01:42:04','admin'),
 (88,1,12,220,1,1,'Which security tools make computer/information more secure?',0,'[]','[{\"AnswerOption\":\"Antivirus\",\"IsCurrect\":false},{\"AnswerOption\":\"Firewall\",\"IsCurrect\":false},{\"AnswerOption\":\"Security Essentials\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 00:42:04','admin'),
 (89,1,9,167,1,1,'Clip art includes ____________.',0,'[]','[{\"AnswerOption\":\"Sound\",\"IsCurrect\":false},{\"AnswerOption\":\"Animation\",\"IsCurrect\":false},{\"AnswerOption\":\"Movies\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-21 22:13:54','admin'),
 (90,1,11,219,1,1,'Which permission offers an option to change the permission settings for a printer?',0,'[]','[{\"AnswerOption\":\"Manage Printers\",\"IsCurrect\":true},{\"AnswerOption\":\"Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 01:44:33','admin'),
 (91,1,13,237,1,1,'Which icon is used in web browsers to indicate invalid/expired certificates?',0,'[]','[{\"AnswerOption\":\"Green lock icons\",\"IsCurrect\":false},{\"AnswerOption\":\"Red lock icons\",\"IsCurrect\":true},{\"AnswerOption\":\"Blue lock icons\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 02:11:47','admin'),
 (92,1,4,43,1,1,'Search option helps in finding:',0,'[]','[{&quot;AnswerOption&quot;:&quot;Files&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Folders&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 12:59:19','admin'),
 (93,1,7,70,1,1,'Which program is used to type in the text quickly and easily?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Printer&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Notepad&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;MS Paint&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 15:09:25','admin'),
 (94,1,10,183,1,1,'_____ layouts are available while creating new slide show.',0,'[]','[{\"AnswerOption\":\"3\",\"IsCurrect\":false},{\"AnswerOption\":\"5\",\"IsCurrect\":false},{\"AnswerOption\":\"9\",\"IsCurrect\":true},{\"AnswerOption\":\"8\",\"IsCurrect\":false}]','1','2013-11-21 22:54:12','admin'),
 (95,1,10,191,1,1,'Text could be inserted using ___________.',0,'[]','[{\"AnswerOption\":\"Outline Text\",\"IsCurrect\":false},{\"AnswerOption\":\"Text Boxes\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 23:13:43','admin'),
 (96,1,9,165,1,1,'Which is auto shape category?',0,'[]','[{\"AnswerOption\":\"Lines\",\"IsCurrect\":false},{\"AnswerOption\":\"Flow charts\",\"IsCurrect\":false},{\"AnswerOption\":\"Call outs\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-23 01:31:25','admin'),
 (97,1,8,91,1,1,'User could change the paper size before printing the document.',0,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-22 22:08:17','admin'),
 (98,1,10,187,1,1,'Which feature is used to reorder the slides?',0,'[]','[{\"AnswerOption\":\"Copy\",\"IsCurrect\":false},{\"AnswerOption\":\"Cut\",\"IsCurrect\":false},{\"AnswerOption\":\"Drag and Drop\",\"IsCurrect\":true},{\"AnswerOption\":\"Delete\",\"IsCurrect\":false}]','1','2013-11-21 23:03:20','admin'),
 (99,1,13,234,1,1,'Which is user interface element in the web browsers?',0,'[]','[{\"AnswerOption\":\"Status bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Search bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Home button\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 02:04:16','admin'),
 (100,1,8,105,1,1,'Which setting determines the height of each line of text in the paragraph?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Paragraph spacing&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Line spacing&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Line markers&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 15:41:38','admin'),
 (101,1,1,2,1,1,'Which of the following are basic Computer components?',0,'[]','[{\"AnswerOption\":\"Control Unit (CU)\",\"IsCurrect\":false},{\"AnswerOption\":\"Arithmetic Logic Unit (ALU)\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-17 08:00:20','admin'),
 (102,1,1,3,1,1,'Which are the physical components of a computer system?',0,'[]','[{\"AnswerOption\":\"Monitor\",\"IsCurrect\":false},{\"AnswerOption\":\"CPU\",\"IsCurrect\":false},{\"AnswerOption\":\"Keyboard\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-17 08:14:54','admin'),
 (103,1,1,4,1,1,'Which is type of software?',0,'[]','[{\"AnswerOption\":\"Application software\",\"IsCurrect\":false},{\"AnswerOption\":\"System Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Operating system\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-17 08:17:27','admin'),
 (104,1,1,5,1,1,'An interface for a user to communicate with the computer is known as:',0,'[]','[{\"AnswerOption\":\"Operating systems\",\"IsCurrect\":true},{\"AnswerOption\":\"utility program\",\"IsCurrect\":false},{\"AnswerOption\":\"Compression software\",\"IsCurrect\":false},{\"AnswerOption\":\"Anti virus\",\"IsCurrect\":false}]','1','2013-11-17 08:21:22','admin'),
 (105,1,1,7,1,1,'Which is popular operating system?',0,'[]','[{\"AnswerOption\":\"Spreadsheets\",\"IsCurrect\":false},{\"AnswerOption\":\"MS-Access\",\"IsCurrect\":false},{\"AnswerOption\":\"Central Processing Unit (CPU)\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":true}]','1','2013-11-17 08:32:55','admin'),
 (106,1,1,8,1,1,'Customized software application which meets the specific requirements is known as:',0,'[]','[{\"AnswerOption\":\"generalized packages\",\"IsCurrect\":false},{\"AnswerOption\":\"Utility programs\",\"IsCurrect\":false},{\"AnswerOption\":\"Application Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Customized packages\",\"IsCurrect\":true}]','1','2013-11-17 08:35:55','admin'),
 (107,1,2,9,1,1,'Which of the following is hardware device?',0,'[]','[{\"AnswerOption\":\"Input Device\",\"IsCurrect\":false},{\"AnswerOption\":\"Output Device\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-17 08:44:28','admin'),
 (108,1,2,10,1,1,'Which is an input device?',0,'[]','[{\"AnswerOption\":\"Microphone (Mic.) for voice as input\",\"IsCurrect\":false},{\"AnswerOption\":\"Optical/magnetic Scanner\",\"IsCurrect\":false},{\"AnswerOption\":\"Touch Screen\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-17 08:48:03','admin'),
 (109,1,2,12,1,1,'Which device is used to resize windows?',0,'[]','[{\"AnswerOption\":\"Keyboard\",\"IsCurrect\":false},{\"AnswerOption\":\"Mouse\",\"IsCurrect\":true},{\"AnswerOption\":\"Touch Screen\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-17 08:59:56','admin'),
 (110,1,2,12,1,1,'When computer shows context menu?',0,'[]','[{\"AnswerOption\":\"Whenever user right clicks on screen\",\"IsCurrect\":true},{\"AnswerOption\":\"whenever user left clicks on screen\",\"IsCurrect\":false},{\"AnswerOption\":\"whenever user scroll up or scroll down\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-17 09:06:01','admin'),
 (111,1,2,15,1,1,'Microphone is used in:',0,'[]','[{\"AnswerOption\":\"Sound recording\",\"IsCurrect\":false},{\"AnswerOption\":\"Video chat\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A or B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-17 09:31:25','admin'),
 (112,1,2,16,1,1,'Which device is used to capture the video stream?',0,'[]','[{\"AnswerOption\":\"Touch Screen\",\"IsCurrect\":false},{\"AnswerOption\":\"Optical/magnetic Scanner\",\"IsCurrect\":false},{\"AnswerOption\":\"Web Camera\",\"IsCurrect\":true},{\"AnswerOption\":\"Microphone\",\"IsCurrect\":false}]','1','2013-11-17 09:37:21','admin'),
 (113,1,2,17,1,1,'Which is an output device?',0,'[]','[{\"AnswerOption\":\"Monitor/Display unit\",\"IsCurrect\":false},{\"AnswerOption\":\"Speakers\",\"IsCurrect\":false},{\"AnswerOption\":\"Projector\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-17 09:38:59','admin'),
 (114,1,2,19,1,1,'Which monitor uses light emitting diodes for a video display?',0,'[]','[{\"AnswerOption\":\"Cathode Ray Tube (CRT)\",\"IsCurrect\":false},{\"AnswerOption\":\"Liquid Crystal Displays (LCD)\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"LED\",\"IsCurrect\":true}]','1','2013-11-17 09:41:35','admin'),
 (115,1,2,20,1,1,'Which is type of printer?',0,'[]','[{\"AnswerOption\":\"Laser Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"Ink Jet Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"Dot Matrix Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-17 09:48:48','admin'),
 (116,1,2,22,1,1,'Projector is:',0,'[]','[{\"AnswerOption\":\"Input device\",\"IsCurrect\":false},{\"AnswerOption\":\"Output device\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-17 09:52:17','admin'),
 (117,1,3,26,1,1,'What is RAM ?\n',0,'[]','[{\"AnswerOption\":\"Random Access Memory\",\"IsCurrect\":true},{\"AnswerOption\":\"Recurring Access Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Flash Access Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-17 09:59:18','admin'),
 (118,1,3,25,1,1,'Which Memory is known as Volatile memory?\n\n',0,'[]','[{\"AnswerOption\":\"Secondary Storage\",\"IsCurrect\":false},{\"AnswerOption\":\"Hybrid Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Real time memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Primary Storage\",\"IsCurrect\":true}]','1','2013-11-17 10:04:00','admin'),
 (119,1,3,30,1,1,'______________ is secondary memory.',0,'[]','[{\"AnswerOption\":\"Random Access Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Hybrid Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Compact Disk\",\"IsCurrect\":true},{\"AnswerOption\":\"Real time Memory\",\"IsCurrect\":false}]','1','2013-11-17 10:05:28','admin'),
 (120,1,3,33,1,1,'USB is which type of memory?\n\n',0,'[]','[{\"AnswerOption\":\"Primary Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Flash Memory\",\"IsCurrect\":true},{\"AnswerOption\":\"Volatile Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Hybrid Memory\",\"IsCurrect\":false}]','1','2013-11-17 10:12:43','admin'),
 (121,1,4,34,1,1,'Which of the following is an operating system?',0,'[]','[{\"AnswerOption\":\"Apple Mac OS\",\"IsCurrect\":false},{\"AnswerOption\":\"Google Android\",\"IsCurrect\":false},{\"AnswerOption\":\"Ubuntu Linux\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-17 10:34:14','admin'),
 (122,1,4,34,1,1,'Which of the following system used to manage memory in a computer ? \n',0,'[]','[{\"AnswerOption\":\"Memory management\",\"IsCurrect\":true},{\"AnswerOption\":\"Process management\",\"IsCurrect\":false},{\"AnswerOption\":\"Device management\",\"IsCurrect\":false},{\"AnswerOption\":\"File management\",\"IsCurrect\":false}]','1','2013-11-17 10:39:22','admin'),
 (123,1,4,39,1,1,'Which option is present in windows start menu?',0,'[]','[{\"AnswerOption\":\"My Documents\",\"IsCurrect\":false},{\"AnswerOption\":\"My Recent Documents\",\"IsCurrect\":false},{\"AnswerOption\":\"My Music\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-17 10:43:14','admin'),
 (124,1,4,39,1,1,'Which section helps to install hardware or software?',0,'[]','[{\"AnswerOption\":\"My Documents\",\"IsCurrect\":false},{\"AnswerOption\":\"My Recent Documents\",\"IsCurrect\":false},{\"AnswerOption\":\"My Music\",\"IsCurrect\":false},{\"AnswerOption\":\"Control panel\",\"IsCurrect\":true}]','1','2013-11-17 10:44:45','admin'),
 (125,1,4,46,1,1,'Content of hard disk, CD, DVD are displayed in:',0,'[]','[{\"AnswerOption\":\"Administrative Tools\",\"IsCurrect\":false},{\"AnswerOption\":\"My Computer\",\"IsCurrect\":true},{\"AnswerOption\":\"Control panel\",\"IsCurrect\":false},{\"AnswerOption\":\"My Document\",\"IsCurrect\":false}]','1','2013-11-17 10:49:33','admin'),
 (126,1,2,14,1,1,'How user provides input through touch screen devices?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Electronic buttons &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Light pen&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of these&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 18:06:58','admin'),
 (127,1,2,18,1,1,'Which is type of monitor?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Cathode Ray Tube (CRT)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Light Emitting Diode(LED)&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All of the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 18:20:51','admin'),
 (128,1,2,21,1,1,'Speakers are:',0,'[]','[{&quot;AnswerOption&quot;:&quot;Output device&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Input Device&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 18:24:37','admin'),
 (129,1,3,28,1,1,'Read Only Memory (ROM) is:',0,'[]','[{&quot;AnswerOption&quot;:&quot;Primary storage memory.&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Main Memory&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Non-volite memory&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All of the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 18:36:58','admin'),
 (130,1,3,29,1,1,' ______________ is not a secondary storage device.',0,'[]','[{&quot;AnswerOption&quot;:&quot;Pen drives&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;ROM&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 18:42:51','admin'),
 (131,1,3,31,1,1,'CD stands for ?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Compact Disk&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Digital Versatile Disc&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 19:00:10','admin'),
 (132,1,4,37,1,1,'Windows start menu appears after pressing windows key.',0,'[]','[{&quot;AnswerOption&quot;:&quot;true&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;false&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 19:07:22','admin'),
 (133,1,4,38,1,1,'Which option is not present in start menu?',0,'[]','[{&quot;AnswerOption&quot;:&quot;All Programs&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Control Panel&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Log Off &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 19:11:21','admin'),
 (134,1,4,42,1,1,'How user gets help on particular topic?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Click on a topic &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Click task to know more about.&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot; Type in a search &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All of the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 19:32:24','admin'),
 (135,1,4,44,1,1,'Operating systems theme could be customized using:',0,'[]','[{&quot;AnswerOption&quot;:&quot;Control Panel&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;My Documents&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;My Computer&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of the above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 20:06:08','admin'),
 (136,1,6,64,1,1,'Which is an application software?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Accounting software&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Office  suites&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Media players &quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 20:31:16','admin'),
 (137,1,6,64,1,1,'What is used as a digital sketchpad to make pictures? ',0,'[]','[{&quot;AnswerOption&quot;:&quot; Toolbar&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Rectangle Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Paint&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 20:32:25','admin'),
 (138,1,6,64,1,1,'Which is default active mode on toolbar?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Airbrush mode&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Text mode&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Select mode&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Pencil mode&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 20:33:20','admin'),
 (139,1,6,65,1,1,'What is present at bottom of MS paint program?',0,'[]','[{&quot;AnswerOption&quot;:&quot;The Curve Line Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Rectangle Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Color Palette&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;The Select Tool&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 20:36:02','admin'),
 (140,1,6,66,1,1,'Which tool is used to draw multi-sided shapes?',0,'[]','[{&quot;AnswerOption&quot;:&quot;The Rectangle Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Polygon Tool&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;The Free-Form Select Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 20:39:30','admin'),
 (141,1,6,67,1,1,' Which tool is used to specify that the existing picture will show through your selection?',0,'[]','[{&quot;AnswerOption&quot;:&quot;The Rectangle Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;The Transparent Option Tool&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;The Free-Form Select Tool&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 21:02:28','admin'),
 (142,1,5,48,1,1,'Hierarchical list of files/folders are displayed in:',0,'[]','[{&quot;AnswerOption&quot;:&quot;File Management&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Windows Explorer&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 21:06:32','admin'),
 (143,1,5,54,1,1,'Files/folders deleted from _________ are permanently deleted. ',0,'[]','[{&quot;AnswerOption&quot;:&quot;Removable storage&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Pen drive&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Both A and B&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 21:09:49','admin'),
 (144,1,5,62,1,1,' Which mark indicates that device driver is not installed?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Green question mark&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Yellow question mark&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Red question mark&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;None of above&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 22:02:15','admin'),
 (145,1,8,78,1,1,'Which tab have most frequently used tools such as text formatting?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Insert&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Home&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Page Layout&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;View&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 22:26:58','admin'),
 (146,1,8,96,1,1,'Which short cut keys could be used to move through the document?',0,'[]','[{&quot;AnswerOption&quot;:&quot;HOME&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;CTRL %2B HOME&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;CTRL %2B END&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;All of the above&quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 22:34:31','admin'),
 (147,1,8,101,1,1,'Which option determines the emphasis or weight applied to the text?',0,'[]','[{&quot;AnswerOption&quot;:&quot;Font face&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Font style&quot;,&quot;IsCurrect&quot;:true},{&quot;AnswerOption&quot;:&quot;Font size&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Alignment&quot;,&quot;IsCurrect&quot;:false}]','1','2013-11-20 22:38:15','admin'),
 (148,1,8,109,1,1,'____________ stores a copy of text, when user cut or copy a text.',0,'[]','[{&quot;AnswerOption&quot;:&quot;Document&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;RAM&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Hard disk&quot;,&quot;IsCurrect&quot;:false},{&quot;AnswerOption&quot;:&quot;Clipboard &quot;,&quot;IsCurrect&quot;:true}]','1','2013-11-20 22:45:47','admin'),
 (149,1,3,32,1,1,'USB is ______________',0,'[]','[{\"AnswerOption\":\"Primary Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Flash Memory\",\"IsCurrect\":true},{\"AnswerOption\":\"Volatile Memory\",\"IsCurrect\":false},{\"AnswerOption\":\"Hybrid Memory\",\"IsCurrect\":false}]','1','2013-11-21 05:16:08','admin'),
 (150,1,5,50,1,1,'More than one files/folders could be copied from My Documents.',0,'[]','[{\"AnswerOption\":\"true\",\"IsCurrect\":true},{\"AnswerOption\":\"false\",\"IsCurrect\":false}]','1','2013-11-21 06:07:43','admin'),
 (151,1,9,119,1,1,'Which is widely used spreadsheet applications?',0,'[]','[{\"AnswerOption\":\"Microsoft word\",\"IsCurrect\":false},{\"AnswerOption\":\"Microsoft excel\",\"IsCurrect\":true},{\"AnswerOption\":\"Microsoft  PowerPoint\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-21 06:15:35','admin'),
 (152,1,9,120,1,1,' ___________ is feature of spreadsheet.',0,'[]','[{\"AnswerOption\":\"List AutoFill\",\"IsCurrect\":false},{\"AnswerOption\":\"Pivot Table\",\"IsCurrect\":false},{\"AnswerOption\":\"Drag and Drop\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 06:17:03','admin'),
 (153,1,9,121,1,1,'Which is feature of MS EXCEL 2007?',0,'[]','[{\"AnswerOption\":\"Office themes and Excel sheets\",\"IsCurrect\":false},{\"AnswerOption\":\"Sorting and Filtering\",\"IsCurrect\":false},{\"AnswerOption\":\"Formulas\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 06:18:37','admin'),
 (154,1,9,122,1,1,'Predefined set of colors, lines etc. is known as _____________.',0,'[]','[{\"AnswerOption\":\"Styles\",\"IsCurrect\":false},{\"AnswerOption\":\"Sorting\",\"IsCurrect\":false},{\"AnswerOption\":\"Themes\",\"IsCurrect\":true},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-21 06:21:07','admin'),
 (155,1,9,124,1,1,'______________ feature helps to write the formula syntax quickly.',0,'[]','[{\"AnswerOption\":\"Office themes and Excel sheets\",\"IsCurrect\":false},{\"AnswerOption\":\"Sorting and Filtering\",\"IsCurrect\":false},{\"AnswerOption\":\"Formulas\",\"IsCurrect\":false},{\"AnswerOption\":\"Function AutoComplete\",\"IsCurrect\":true}]','1','2013-11-21 06:22:23','admin'),
 (156,1,5,53,1,1,'Which shortcut key is used to rename a file/folder?',0,'[]','[{\"AnswerOption\":\"F2\",\"IsCurrect\":true},{\"AnswerOption\":\"F5\",\"IsCurrect\":false},{\"AnswerOption\":\"F11\",\"IsCurrect\":false},{\"AnswerOption\":\"F12\",\"IsCurrect\":false}]','1','2013-11-21 06:30:40','admin'),
 (157,1,9,154,1,1,'Which feature is used to manipulate data in Excel worksheets?',0,'[]','[{\"AnswerOption\":\"Formulas and worksheet\",\"IsCurrect\":true},{\"AnswerOption\":\"Border and Patterns\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 06:33:46','admin'),
 (158,1,9,157,1,1,'___________ are used to present data in visual format.',0,'[]','[{\"AnswerOption\":\"Format Cells\",\"IsCurrect\":false},{\"AnswerOption\":\"Charts\",\"IsCurrect\":true},{\"AnswerOption\":\"Formulas and Functions\",\"IsCurrect\":false},{\"AnswerOption\":\"Modifying a Worksheet\",\"IsCurrect\":false}]','1','2013-11-21 06:40:56','admin'),
 (159,1,9,158,1,1,'Which is one of the chart type?',0,'[]','[{\"AnswerOption\":\"Bubble chart\",\"IsCurrect\":false},{\"AnswerOption\":\"Doughnut chart\",\"IsCurrect\":false},{\"AnswerOption\":\"Line chart\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 06:43:32','admin'),
 (160,1,10,170,1,1,'Which of the following are main advantages of PowerPoint:\r\n',0,'[]','[{\"AnswerOption\":\"PowerPoint gives you several ways to create a presentation.\",\"IsCurrect\":false},{\"AnswerOption\":\"Adding text will help you put your ideas into words.\",\"IsCurrect\":false},{\"AnswerOption\":\"The multimedia features makes your slides sparkle. You can add clip art, sound effects, music, video clips\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 06:50:42','admin'),
 (161,1,10,172,1,1,'________ provides ideas/templates for variety of presentation types.',0,'[]','[{\"AnswerOption\":\"Installed templates\",\"IsCurrect\":true},{\"AnswerOption\":\"Design templates\",\"IsCurrect\":false},{\"AnswerOption\":\"Blank presentations\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide Layout\",\"IsCurrect\":false}]','1','2013-11-21 06:52:26','admin'),
 (162,1,10,176,1,1,'What are different types of content?',0,'[]','[{\"AnswerOption\":\"Media Clip\",\"IsCurrect\":false},{\"AnswerOption\":\"Chart\",\"IsCurrect\":false},{\"AnswerOption\":\"SmartArt Graphic\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 07:12:27','admin'),
 (163,1,10,178,1,1,'Which option helps in creation of presentation slides?',0,'[]','[{\"AnswerOption\":\"Normal\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide Sorter\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide Show\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 07:24:03','admin'),
 (164,1,12,220,1,1,'_________ is the practice of defending recording or desctruction.',0,'[]','[{\"AnswerOption\":\"Antivirus Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Firewall\",\"IsCurrect\":false},{\"AnswerOption\":\"Computer security\",\"IsCurrect\":true},{\"AnswerOption\":\"Security essentials\",\"IsCurrect\":false}]','1','2013-11-21 07:43:44','admin'),
 (165,1,12,221,1,1,'_______ is computer program which interfere with computer operation.',0,'[]','[{\"AnswerOption\":\"Antivirus Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Computer viruses\",\"IsCurrect\":true},{\"AnswerOption\":\"Security essentials\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 07:45:04','admin'),
 (166,1,5,58,1,1,'Which buttons are present in add remove program window?',0,'[]','[{\"AnswerOption\":\"Change\",\"IsCurrect\":false},{\"AnswerOption\":\"Remove\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 07:46:16','admin'),
 (167,1,12,221,1,1,'_______ is computer program ......... viruses and worms.',0,'[]','[{\"AnswerOption\":\"Antivirus Softwares\",\"IsCurrect\":true},{\"AnswerOption\":\"Computer viruses\",\"IsCurrect\":false},{\"AnswerOption\":\"Security essentials\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 07:46:36','admin'),
 (168,1,12,223,1,1,'This is best practice:',0,'[]','[{\"AnswerOption\":\"Avoid visiting software key generator related websites.\",\"IsCurrect\":false},{\"AnswerOption\":\"Use antivirus programs \",\"IsCurrect\":false},{\"AnswerOption\":\"Do not install more than one antivirus software\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-21 07:53:37','admin'),
 (169,1,12,224,1,1,'Which program provides defense against people or programs?',0,'[]','[{\"AnswerOption\":\"Antivirus Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Firewall\",\"IsCurrect\":true},{\"AnswerOption\":\"Computer security\",\"IsCurrect\":false},{\"AnswerOption\":\"Security essentials\",\"IsCurrect\":false}]','1','2013-11-21 07:56:18','admin'),
 (170,1,12,228,1,1,'____________ is a diagnostic mode of an operating system.',0,'[]','[{\"AnswerOption\":\"Antivirus Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Firewall\",\"IsCurrect\":false},{\"AnswerOption\":\"Computer security\",\"IsCurrect\":false},{\"AnswerOption\":\"Safe mode\",\"IsCurrect\":true}]','1','2013-11-21 08:00:53','admin'),
 (171,1,5,61,1,1,'Which shortcut keys are used to search files/folders in computer?',0,'[]','[{\"AnswerOption\":\"Window Key + F\",\"IsCurrect\":true},{\"AnswerOption\":\"Window Key + D\",\"IsCurrect\":false},{\"AnswerOption\":\"Window Key + E\",\"IsCurrect\":false},{\"AnswerOption\":\"Window Key + V\",\"IsCurrect\":false}]','1','2013-11-21 08:10:25','admin'),
 (172,1,11,204,1,1,'_____________ consists of two or more computers that are linked in order to share resources.',0,'[]','[{\"AnswerOption\":\"Servers\",\"IsCurrect\":false},{\"AnswerOption\":\"Computer Networks\",\"IsCurrect\":true},{\"AnswerOption\":\"Protocols\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 08:15:05','admin'),
 (173,1,11,205,1,1,'__________ is limited to a geographic area such as a computer lb, school etc.',0,'[]','[{\"AnswerOption\":\"Servers\",\"IsCurrect\":false},{\"AnswerOption\":\"Workstations\",\"IsCurrect\":false},{\"AnswerOption\":\"Local Area Network\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 08:18:23','admin'),
 (174,1,11,205,1,1,'Which of these statements is true ?',0,'[]','[{\"AnswerOption\":\"Servers tend to be more powerful than workstations.\",\"IsCurrect\":false},{\"AnswerOption\":\"Workstations tend to be more powerful than servers.\",\"IsCurrect\":false},{\"AnswerOption\":\"Workstations are any device through which human user can interact to utilize the network resources.\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and C\",\"IsCurrect\":true}]','1','2013-11-21 08:20:02','admin'),
 (175,1,4,45,1,1,'Option to modify my computer settings is present in : ',0,'[]','[{\"AnswerOption\":\"Administrative Tools\",\"IsCurrect\":false},{\"AnswerOption\":\"Control Panel\",\"IsCurrect\":true},{\"AnswerOption\":\"Desktop\",\"IsCurrect\":false},{\"AnswerOption\":\"My Document\",\"IsCurrect\":false}]','1','2013-11-21 08:23:16','admin');
INSERT INTO `finalquizmaster` (`Id`,`courseId`,`chapterId`,`sectionId`,`groupNo`,`complexity`,`questionText`,`IsQuestionOptionPresent`,`QuestionOption`,`AnswerOption`,`ContentVersion`,`DateCreated`,`createdby`) VALUES 
 (176,1,11,209,1,1,'______ is an addressing protocol.',0,'[]','[{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Hyper Text Transfer Protocol (HTTP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Internet Protocol (IP)\",\"IsCurrect\":true},{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":false}]','1','2013-11-21 08:32:09','admin'),
 (177,1,11,211,1,1,'____________ is based on the client/server principles.',0,'[]','[{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Hyper Text Transfer Protocol (HTTP)\",\"IsCurrect\":true},{\"AnswerOption\":\"Internet Protocol (IP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":false}]','1','2013-11-21 08:38:58','admin'),
 (178,1,11,213,1,1,'_______ contains a series of four numbers unique to the computer in a network.',0,'[]','[{\"AnswerOption\":\"Protocols\",\"IsCurrect\":false},{\"AnswerOption\":\"Workstations\",\"IsCurrect\":false},{\"AnswerOption\":\"IP address\",\"IsCurrect\":true},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-21 08:40:26','admin'),
 (179,1,11,211,1,1,' _______ is used to transfer a hypertext between two or more computers.',0,'[]','[{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Hyper Text Transfer Protocol (HTTP)\",\"IsCurrect\":true},{\"AnswerOption\":\"Internet Protocol (IP)\",\"IsCurrect\":false},{\"AnswerOption\":\"Post office Protocol (POP)\",\"IsCurrect\":false}]','1','2013-11-21 08:48:52','admin'),
 (180,1,13,232,1,1,'Which software is used to retrieve, present information from the world wide web (WWW)?',0,'[]','[{\"AnswerOption\":\"Downloads & installations\",\"IsCurrect\":false},{\"AnswerOption\":\"Protocol & security\",\"IsCurrect\":false},{\"AnswerOption\":\"Web Browsers\",\"IsCurrect\":true},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-21 08:54:01','admin'),
 (181,1,13,232,1,1,'What is  the primary purpose of a web browser?',0,'[]','[{\"AnswerOption\":\"To bring information resources to the user\",\"IsCurrect\":false},{\"AnswerOption\":\"To allow them users to view the information\",\"IsCurrect\":false},{\"AnswerOption\":\"To access other information.\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 08:55:55','admin'),
 (182,1,13,232,1,1,'______ identifies a resource to be retrieved over the Secure HTTP protocol.',0,'[]','[{\"AnswerOption\":\"FILE\",\"IsCurrect\":false},{\"AnswerOption\":\"HTTP\",\"IsCurrect\":false},{\"AnswerOption\":\"HTTPS\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 08:58:09','admin'),
 (183,1,13,232,1,1,'________ identifies and attempts to open local resource in web browser.',0,'[]','[{\"AnswerOption\":\"FILE\",\"IsCurrect\":true},{\"AnswerOption\":\"HTTP\",\"IsCurrect\":false},{\"AnswerOption\":\"HTTPS\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 08:59:32','admin'),
 (184,1,13,233,1,1,'Which one is popular web browser?',0,'[]','[{\"AnswerOption\":\"Google chrome\",\"IsCurrect\":false},{\"AnswerOption\":\"Microsoft Internet Explorer\",\"IsCurrect\":false},{\"AnswerOption\":\"Apple Safari\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 09:01:00','admin'),
 (185,1,13,233,1,1,'_______ is web browser provided by Microsoft.',0,'[]','[{\"AnswerOption\":\"Google chrome\",\"IsCurrect\":false},{\"AnswerOption\":\"Internet Explorer\",\"IsCurrect\":true},{\"AnswerOption\":\"Apple Safari\",\"IsCurrect\":false},{\"AnswerOption\":\"Mozilla Firefox\",\"IsCurrect\":false}]','1','2013-11-21 09:02:34','admin'),
 (186,1,13,235,1,1,'Which of the following browsers provides additional options to make browsing experience safe, secure, private and efficient?\n',0,'[]','[{\"AnswerOption\":\"Google chrome\",\"IsCurrect\":false},{\"AnswerOption\":\"Microsoft Internet Explorer\",\"IsCurrect\":false},{\"AnswerOption\":\"Mozilla Firefox\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 09:05:25','admin'),
 (187,1,13,235,1,1,'Which task is performed using internet options?',0,'[]','[{\"AnswerOption\":\"Advance configuration options \",\"IsCurrect\":false},{\"AnswerOption\":\"Change the security settings\",\"IsCurrect\":false},{\"AnswerOption\":\"Set the content privacy options\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-21 09:06:34','admin'),
 (188,1,7,73,1,1,'Which option allows user to browse an existing document?',0,'[]','[{\"AnswerOption\":\"Open Window\",\"IsCurrect\":true},{\"AnswerOption\":\"Save Window\",\"IsCurrect\":false},{\"AnswerOption\":\"Print Window\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 09:11:26','admin'),
 (189,1,13,238,1,1,'What are the two section of any email message?\n',0,'[]','[{\"AnswerOption\":\"Input and Output\",\"IsCurrect\":false},{\"AnswerOption\":\"Header and Body\",\"IsCurrect\":true},{\"AnswerOption\":\"Protocol & security\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-21 09:13:07','admin'),
 (190,1,7,75,1,1,' _______ does not affect the printing.',0,'[]','[{\"AnswerOption\":\"Font\",\"IsCurrect\":false},{\"AnswerOption\":\"Word Wrap\",\"IsCurrect\":true},{\"AnswerOption\":\"Print\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 09:13:59','admin'),
 (191,1,13,241,1,1,'User unwanted mails are called as _______.',0,'[]','[{\"AnswerOption\":\"Electronic emails\",\"IsCurrect\":false},{\"AnswerOption\":\"Spam emails\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 09:16:30','admin'),
 (192,1,13,242,1,1,'Criminal gets access to your computer using __________.',0,'[]','[{\"AnswerOption\":\"SPAM Emails\",\"IsCurrect\":false},{\"AnswerOption\":\"Social Engineering Emails\",\"IsCurrect\":true},{\"AnswerOption\":\"Search Engines\",\"IsCurrect\":false},{\"AnswerOption\":\"Protocol  and Security\",\"IsCurrect\":false}]','1','2013-11-21 09:17:54','admin'),
 (193,1,13,244,1,1,'_________ is designed to search an information on the World Wide Web.',0,'[]','[{\"AnswerOption\":\"Web search engine \",\"IsCurrect\":true},{\"AnswerOption\":\"Web browsers\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 09:20:13','admin'),
 (194,1,13,247,1,1,'User shall avoid downloading/installing _____________ applications.',0,'[]','[{\"AnswerOption\":\"Toolbars\",\"IsCurrect\":false},{\"AnswerOption\":\"Smiley\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 09:22:51','admin'),
 (195,1,14,251,1,1,'How to keep your passwords secret?\n',0,'[]','[{\"AnswerOption\":\"Stop accessing websites\",\"IsCurrect\":false},{\"AnswerOption\":\"Never provide your password over email or in response to an email request\",\"IsCurrect\":true},{\"AnswerOption\":\"Never meet any person\",\"IsCurrect\":false},{\"AnswerOption\":\"Allow your account to be create anyone of your friend\",\"IsCurrect\":false}]','1','2013-11-21 09:25:32','admin'),
 (196,1,14,251,1,1,'How to avoid online fraud ?\n',0,'[]','[{\"AnswerOption\":\"Donâ€™t secure your password\",\"IsCurrect\":false},{\"AnswerOption\":\"Use only secure sites. \",\"IsCurrect\":true},{\"AnswerOption\":\"Give your personal information\",\"IsCurrect\":false},{\"AnswerOption\":\"None any of the above\",\"IsCurrect\":false}]','1','2013-11-21 09:27:05','admin'),
 (197,1,4,40,1,1,'To view content in start menu, click on?',0,'[]','[{\"AnswerOption\":\"All Programs\",\"IsCurrect\":false},{\"AnswerOption\":\"Windows start button\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-21 09:47:29','admin'),
 (198,1,8,77,1,1,'MS Word allows user to:',0,'[]','[{\"AnswerOption\":\"Creating a word document\",\"IsCurrect\":false},{\"AnswerOption\":\"Create table of contents\",\"IsCurrect\":false},{\"AnswerOption\":\"Print out multiple pages on a single sheet of paper\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-21 10:03:21','admin'),
 (199,1,8,79,1,1,'Word 2007 screen could have _______________',0,'[]','[{\"AnswerOption\":\"Objects\",\"IsCurrect\":false},{\"AnswerOption\":\"Tabs\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:08:17','admin'),
 (200,1,8,80,1,1,'In word 2007, the main options are represented as:',0,'[]','[{\"AnswerOption\":\"Tabs\",\"IsCurrect\":true},{\"AnswerOption\":\"Table\",\"IsCurrect\":false},{\"AnswerOption\":\"Rows\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:09:47','admin'),
 (201,1,8,81,1,1,'In Word 2007, Is it possible to customize quick access toolbar?',0,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-21 10:11:19','admin'),
 (202,1,8,82,1,1,'In Word 2007, ruler represents:',0,'[]','[{\"AnswerOption\":\"Width\",\"IsCurrect\":false},{\"AnswerOption\":\"Height\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:14:04','admin'),
 (203,1,8,83,1,1,'Which is permanent part of the typing area?',0,'[]','[{\"AnswerOption\":\"Insertion Point \",\"IsCurrect\":false},{\"AnswerOption\":\"Mouse Pointer \",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None the above\",\"IsCurrect\":false}]','1','2013-11-21 10:16:40','admin'),
 (204,1,8,85,1,1,'Which option is present under Office button?',0,'[]','[{\"AnswerOption\":\"Create new documents\",\"IsCurrect\":false},{\"AnswerOption\":\"Open existing documents\",\"IsCurrect\":false},{\"AnswerOption\":\"Save documents in Word\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-21 10:19:41','admin'),
 (205,1,8,86,1,1,'Shortcut key to create new documents in MS-Word ?',0,'[]','[{\"AnswerOption\":\"Ctrl %2B Z\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B U\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B N\",\"IsCurrect\":true}]','1','2013-11-21 10:21:48','admin'),
 (206,1,8,87,1,1,'Which short cut keys are used to open an existing word document?',0,'[]','[{\"AnswerOption\":\"Ctrl %2B Z\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B X\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B O\",\"IsCurrect\":true}]','1','2013-11-21 10:23:09','admin'),
 (207,1,8,88,1,1,'Which short cut keys are used to save a word document?',0,'[]','[{\"AnswerOption\":\"Ctrl %2B V\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B S\",\"IsCurrect\":true},{\"AnswerOption\":\"Ctrl %2B Z\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B X\",\"IsCurrect\":false}]','1','2013-11-21 10:24:21','admin'),
 (208,1,8,89,1,1,'Look in option is present under:',0,'[]','[{\"AnswerOption\":\"In the Open dialog box\",\"IsCurrect\":true},{\"AnswerOption\":\"In the Close dialog box\",\"IsCurrect\":false},{\"AnswerOption\":\"In the footer\",\"IsCurrect\":false}]','1','2013-11-21 10:28:49','admin'),
 (209,1,8,90,1,1,'MS Word 2007 has close option  _____________',0,'[]','[{\"AnswerOption\":\"In the Open dialog box\",\"IsCurrect\":false},{\"AnswerOption\":\"Inside Office button\",\"IsCurrect\":true},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:30:30','admin'),
 (210,1,8,92,1,1,'Exit command is present _________ in MS-Word.',0,'[]','[{\"AnswerOption\":\"Inside Office button\",\"IsCurrect\":true},{\"AnswerOption\":\"Inside header\",\"IsCurrect\":false},{\"AnswerOption\":\"Inside footer\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:34:50','admin'),
 (211,1,8,94,1,1,'Text section contain ?',0,'[]','[{\"AnswerOption\":\"Common word concepts\",\"IsCurrect\":false},{\"AnswerOption\":\"Tips\",\"IsCurrect\":false},{\"AnswerOption\":\"Commands\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-21 10:39:40','admin'),
 (212,1,8,94,1,1,'User could type text in:',0,'[]','[{\"AnswerOption\":\"Text area\",\"IsCurrect\":true},{\"AnswerOption\":\"Footer area\",\"IsCurrect\":false},{\"AnswerOption\":\"Header area\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:41:28','admin'),
 (213,1,8,97,1,1,'Spacebar is used as __________.',0,'[]','[{\"AnswerOption\":\"Creator\",\"IsCurrect\":false},{\"AnswerOption\":\"Separator\",\"IsCurrect\":true},{\"AnswerOption\":\" All of the above\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:43:53','admin'),
 (214,1,8,98,1,1,'How to select the whole word in Word 2007?',0,'[]','[{\"AnswerOption\":\"Single-click within the word\",\"IsCurrect\":false},{\"AnswerOption\":\"Double-click within the word\",\"IsCurrect\":true},{\"AnswerOption\":\"Remove word\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:46:03','admin'),
 (215,1,8,99,1,1,'How to delete a word in Word 2007?',0,'[]','[{\"AnswerOption\":\"Using backspace\",\"IsCurrect\":false},{\"AnswerOption\":\"Pressing delete button\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-21 10:47:46','admin'),
 (216,1,8,100,1,1,'Is it possible to replace a word without selecting it in Word 2007?',0,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":false},{\"AnswerOption\":\"False\",\"IsCurrect\":true}]','1','2013-11-21 10:50:26','admin'),
 (217,1,8,102,1,1,'Which section of Word 2007 has \"Format painter\"?',0,'[]','[{\"AnswerOption\":\"Clipboard\",\"IsCurrect\":false},{\"AnswerOption\":\"Home tab\",\"IsCurrect\":true},{\"AnswerOption\":\"Footer\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 01:37:02','admin'),
 (218,1,8,103,1,1,'Which section of Word 2007 has \"Format paragraph\" option?',0,'[]','[{\"AnswerOption\":\"Footer\",\"IsCurrect\":false},{\"AnswerOption\":\"Header\",\"IsCurrect\":false},{\"AnswerOption\":\"Context menu\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 01:39:15','admin'),
 (219,1,8,106,1,1,'Paragraph spacing enables user to give a space ______________.',0,'[]','[{\"AnswerOption\":\"Before Paragraph\",\"IsCurrect\":false},{\"AnswerOption\":\"After Paragraph\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the \",\"IsCurrect\":false}]','1','2013-11-22 01:43:26','admin'),
 (220,1,8,107,1,1,'User could give borders to __________',0,'[]','[{\"AnswerOption\":\"Paragraph\",\"IsCurrect\":false},{\"AnswerOption\":\"Text\",\"IsCurrect\":false},{\"AnswerOption\":\"Table\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 01:47:51','admin'),
 (221,1,8,110,1,1,'The red wavy lines will appear underneath _____________',0,'[]','[{\"AnswerOption\":\"Paragraph\",\"IsCurrect\":false},{\"AnswerOption\":\"Grammatical errors\",\"IsCurrect\":false},{\"AnswerOption\":\"Misspelled words\",\"IsCurrect\":true},{\"AnswerOption\":\"Both B and C\",\"IsCurrect\":false}]','1','2013-11-22 01:50:04','admin'),
 (222,1,8,111,1,1,'Page formatting used to ____________',0,'[]','[{\"AnswerOption\":\"Change default paragraph setting\",\"IsCurrect\":false},{\"AnswerOption\":\"Change default text setting\",\"IsCurrect\":false},{\"AnswerOption\":\"Change default page setting\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 01:58:29','admin'),
 (223,1,8,113,1,1,'This is type of page orientation:',0,'[]','[{\"AnswerOption\":\" Portrait\",\"IsCurrect\":false},{\"AnswerOption\":\"Landscape\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 02:02:49','admin'),
 (224,1,8,114,1,1,'Zoom option is present under ____________.',0,'[]','[{\"AnswerOption\":\"File\",\"IsCurrect\":false},{\"AnswerOption\":\"Insert\",\"IsCurrect\":false},{\"AnswerOption\":\"View\",\"IsCurrect\":true},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 02:05:52','admin'),
 (225,1,8,115,1,1,'Header and footer option are present under _____________.',0,'[]','[{\"AnswerOption\":\"File\",\"IsCurrect\":false},{\"AnswerOption\":\"View\",\"IsCurrect\":false},{\"AnswerOption\":\"Insert\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 02:07:06','admin'),
 (226,1,8,116,1,1,'By default page number starts with ___.',0,'[]','[{\"AnswerOption\":\"15\",\"IsCurrect\":false},{\"AnswerOption\":\"1\",\"IsCurrect\":true},{\"AnswerOption\":\"2\",\"IsCurrect\":false},{\"AnswerOption\":\"10\",\"IsCurrect\":false}]','1','2013-11-22 02:10:02','admin'),
 (227,1,8,117,1,1,'Page break option present under ____________',0,'[]','[{\"AnswerOption\":\"View option\",\"IsCurrect\":false},{\"AnswerOption\":\"Insert option\",\"IsCurrect\":true},{\"AnswerOption\":\"Page layout option\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 02:12:29','admin'),
 (228,1,8,118,1,1,'Page break can be deleted by deleting ________',0,'[]','[{\"AnswerOption\":\"Extra page break indicator in the document\",\"IsCurrect\":true},{\"AnswerOption\":\"Extra pages in the document\",\"IsCurrect\":false},{\"AnswerOption\":\"Extra paragraph in the document\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 02:15:35','admin'),
 (229,1,9,123,1,1,'Formulas can be explain as:',0,'[]','[{\"AnswerOption\":\"Equation that calculates the count\",\"IsCurrect\":false},{\"AnswerOption\":\"Equation that calculates the missing values\",\"IsCurrect\":false},{\"AnswerOption\":\"Equation that calculates the value\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 02:22:31','admin'),
 (230,1,9,125,1,1,'In excel, data could be sorted by:',0,'[]','[{\"AnswerOption\":\"Date\",\"IsCurrect\":false},{\"AnswerOption\":\"Font Color\",\"IsCurrect\":false},{\"AnswerOption\":\"Cell Color\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 02:27:31','admin'),
 (231,1,9,126,1,1,'Excel application is present under ____________.',0,'[]','[{\"AnswerOption\":\"Start menu\",\"IsCurrect\":false},{\"AnswerOption\":\"Microsoft office\",\"IsCurrect\":true},{\"AnswerOption\":\"Paint\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 02:29:36','admin'),
 (232,1,9,127,1,1,'Excel files are also known as _________.',0,'[]','[{\"AnswerOption\":\"Workbook or spreadsheet\",\"IsCurrect\":false},{\"AnswerOption\":\"Workbook\",\"IsCurrect\":true},{\"AnswerOption\":\"Column\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 02:31:29','admin'),
 (233,1,9,128,1,1,'By default, workbook have ____ worksheets. ',0,'[]','[{\"AnswerOption\":\"5\",\"IsCurrect\":false},{\"AnswerOption\":\"6\",\"IsCurrect\":false},{\"AnswerOption\":\"3\",\"IsCurrect\":true},{\"AnswerOption\":\"1\",\"IsCurrect\":false}]','1','2013-11-22 02:34:15','admin'),
 (234,1,9,129,1,1,'Default active cell in an open excel worksheet is ____.',0,'[]','[{\"AnswerOption\":\"A5\",\"IsCurrect\":false},{\"AnswerOption\":\"B1\",\"IsCurrect\":false},{\"AnswerOption\":\"A1\",\"IsCurrect\":true},{\"AnswerOption\":\"Z0\",\"IsCurrect\":false}]','1','2013-11-22 02:36:05','admin'),
 (235,1,9,130,1,1,'Which is valid way to move between cells?',0,'[]','[{\"AnswerOption\":\"Mouse\",\"IsCurrect\":false},{\"AnswerOption\":\"Scroll bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Navigation keys\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 03:34:59','admin'),
 (236,1,9,133,1,1,'Each cell of a worksheet has ________.',0,'[]','[{\"AnswerOption\":\"Multiple referance \",\"IsCurrect\":false},{\"AnswerOption\":\"Unique referance\",\"IsCurrect\":true},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 03:38:13','admin'),
 (237,1,9,134,1,1,'Find and Replace option is present under ____________.',0,'[]','[{\"AnswerOption\":\"Insert option\",\"IsCurrect\":false},{\"AnswerOption\":\"Page layout option\",\"IsCurrect\":false},{\"AnswerOption\":\"View option\",\"IsCurrect\":false},{\"AnswerOption\":\"Home option\",\"IsCurrect\":true}]','1','2013-11-22 03:41:03','admin'),
 (238,1,9,136,1,1,'Is it possible to re-size multiple rows simultaneously?',0,'[]','[{\"AnswerOption\":\"true\",\"IsCurrect\":true},{\"AnswerOption\":\"false\",\"IsCurrect\":false}]','1','2013-11-22 03:44:53','admin'),
 (239,1,9,137,1,1,'Shortcut key to copy and cut the content of cell is _________.',0,'[]','[{\"AnswerOption\":\"Ctrl %2B V and Ctrl %2B C \",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B C and Ctrl  %2B X\",\"IsCurrect\":true},{\"AnswerOption\":\"Ctrl %2B Z and Ctrl %2B C\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl  %2B V and Ctrl  %2B X\",\"IsCurrect\":false}]','1','2013-11-22 03:48:50','admin'),
 (240,1,9,139,1,1,'User could paste: ',0,'[]','[{\"AnswerOption\":\"Only the cell formatting\",\"IsCurrect\":false},{\"AnswerOption\":\"Only the formulas\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 03:54:17','admin'),
 (241,1,9,140,1,1,'Cells could be moved using:',0,'[]','[{\"AnswerOption\":\"Drag and drop\",\"IsCurrect\":true},{\"AnswerOption\":\"Mouse right click\",\"IsCurrect\":false},{\"AnswerOption\":\"F2 key\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 03:55:48','admin'),
 (242,1,9,141,1,1,'Freeze Panes are use to ___________',0,'[]','[{\"AnswerOption\":\"Fix the cell\",\"IsCurrect\":false},{\"AnswerOption\":\"Fix the row\",\"IsCurrect\":false},{\"AnswerOption\":\"Fix the heading\",\"IsCurrect\":true},{\"AnswerOption\":\"Fix the column\",\"IsCurrect\":false}]','1','2013-11-22 03:59:06','admin'),
 (243,1,9,142,1,1,'Page break option is present under _____________.',0,'[]','[{\"AnswerOption\":\"Home\",\"IsCurrect\":false},{\"AnswerOption\":\"View\",\"IsCurrect\":false},{\"AnswerOption\":\"Insert\",\"IsCurrect\":false},{\"AnswerOption\":\"Page Layout\",\"IsCurrect\":true}]','1','2013-11-22 04:00:49','admin'),
 (244,1,9,145,1,1,'Can user specify the range of page numbers to print?',0,'[]','[{\"AnswerOption\":\"Yes\",\"IsCurrect\":true},{\"AnswerOption\":\"No\",\"IsCurrect\":false}]','1','2013-11-22 04:05:56','admin'),
 (245,1,9,146,1,1,'Can user save excel file with .doc extension?',0,'[]','[{\"AnswerOption\":\"Yes\",\"IsCurrect\":true},{\"AnswerOption\":\"No\",\"IsCurrect\":false}]','1','2013-11-22 04:12:47','admin'),
 (246,1,9,149,1,1,'Excels default date format is ____________.',0,'[]','[{\"AnswerOption\":\"January 1, 2001\",\"IsCurrect\":false},{\"AnswerOption\":\"1 January 2001\",\"IsCurrect\":false},{\"AnswerOption\":\"1-Jan-01\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 04:24:28','admin'),
 (247,1,9,151,1,1,'AutoFit Row height feature is use to adjust _____________.',0,'[]','[{\"AnswerOption\":\"Row height\",\"IsCurrect\":true},{\"AnswerOption\":\"Row width\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 04:29:36','admin'),
 (248,1,9,152,1,1,'Hide feature is use to hide _______.',0,'[]','[{\"AnswerOption\":\"Column\",\"IsCurrect\":false},{\"AnswerOption\":\"Row\",\"IsCurrect\":false},{\"AnswerOption\":\"Cell\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 04:31:34','admin'),
 (249,1,9,155,1,1,'Which feature is use to copy a formula ?',0,'[]','[{\"AnswerOption\":\"Copy and pest\",\"IsCurrect\":false},{\"AnswerOption\":\"Drag and drop\",\"IsCurrect\":true},{\"AnswerOption\":\"Move column\",\"IsCurrect\":false},{\"AnswerOption\":\"Move Rows\",\"IsCurrect\":false}]','1','2013-11-22 04:36:05','admin'),
 (250,1,9,156,1,1,'Max function is use to calculate __________.',0,'[]','[{\"AnswerOption\":\"Minimum of numbers\",\"IsCurrect\":false},{\"AnswerOption\":\"Maximum of the numbers\",\"IsCurrect\":true},{\"AnswerOption\":\"Top value of number in cell\",\"IsCurrect\":false},{\"AnswerOption\":\"Lowest value of number in cell\",\"IsCurrect\":false}]','1','2013-11-22 04:41:20','admin'),
 (251,1,9,159,1,1,'Which is not a component of chart?',0,'[]','[{\"AnswerOption\":\"X-Axis\",\"IsCurrect\":false},{\"AnswerOption\":\"Y-Axis\",\"IsCurrect\":false},{\"AnswerOption\":\"Data Labels \",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":true}]','1','2013-11-22 04:46:19','admin'),
 (252,1,9,160,1,1,'Is it necessary to select all the row and column present in workbook to draw a chart?',0,'[]','[{\"AnswerOption\":\"No\",\"IsCurrect\":true},{\"AnswerOption\":\"Yes\",\"IsCurrect\":false}]','1','2013-11-22 04:50:21','admin'),
 (253,1,9,161,1,1,'Edit chart feature is available under __________ tab.',0,'[]','[{\"AnswerOption\":\"Home\",\"IsCurrect\":false},{\"AnswerOption\":\"Layout\",\"IsCurrect\":true},{\"AnswerOption\":\"Insert\",\"IsCurrect\":false},{\"AnswerOption\":\"View\",\"IsCurrect\":false}]','1','2013-11-22 05:03:11','admin'),
 (254,1,9,162,1,1,'Which feature is use to resize a chart?',0,'[]','[{\"AnswerOption\":\"Copy and Pest\",\"IsCurrect\":false},{\"AnswerOption\":\"Drag and Drop\",\"IsCurrect\":true},{\"AnswerOption\":\"Move chat\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 05:05:34','admin'),
 (255,1,9,163,1,1,'Elements of chart are also moved when we move chart.',0,'[]','[{\"AnswerOption\":\"true\",\"IsCurrect\":true},{\"AnswerOption\":\"false\",\"IsCurrect\":false}]','1','2013-11-22 05:07:02','admin'),
 (256,1,9,166,1,1,'What is Smart Art ?',0,'[]','[{\"AnswerOption\":\"Visual representation of cells\",\"IsCurrect\":false},{\"AnswerOption\":\"Visual representation of paragraph\",\"IsCurrect\":false},{\"AnswerOption\":\"Visual representation of text\",\"IsCurrect\":false},{\"AnswerOption\":\"Visual representation of information and ideas\",\"IsCurrect\":true}]','1','2013-11-22 05:11:41','admin'),
 (257,1,9,168,1,1,'Edit picture feature option is present under _______ tab.',0,'[]','[{\"AnswerOption\":\"Home\",\"IsCurrect\":false},{\"AnswerOption\":\"Insert\",\"IsCurrect\":false},{\"AnswerOption\":\"View\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 05:15:33','admin'),
 (258,1,10,171,1,1,'Microsoft powerpoint is part of ______________.',0,'[]','[{\"AnswerOption\":\"Microsoft Office Suite\",\"IsCurrect\":true},{\"AnswerOption\":\"Google plus\",\"IsCurrect\":false},{\"AnswerOption\":\"Apple \",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 05:27:30','admin'),
 (259,1,10,173,1,1,'Preview design template by __________.',0,'[]','[{\"AnswerOption\":\"Clicking the template name on the list\",\"IsCurrect\":false},{\"AnswerOption\":\"Highlighting the template name on the list\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 05:31:01','admin'),
 (260,1,10,174,1,1,'It is necessary to select blank presentation to create slide show? ',0,'[]','[{\"AnswerOption\":\"Yes\",\"IsCurrect\":false},{\"AnswerOption\":\"No\",\"IsCurrect\":true}]','1','2013-11-22 05:33:24','admin'),
 (261,1,10,175,1,1,'Different type of slide layouts are ?',0,'[]','[{\"AnswerOption\":\"Title and Content\",\"IsCurrect\":false},{\"AnswerOption\":\"Section Header\",\"IsCurrect\":false},{\"AnswerOption\":\"Text Content\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 05:35:14','admin'),
 (262,1,10,179,1,1,'Normal View divides the screen into _______.',0,'[]','[{\"AnswerOption\":\"5 section\",\"IsCurrect\":false},{\"AnswerOption\":\"10 section\",\"IsCurrect\":false},{\"AnswerOption\":\"3 section\",\"IsCurrect\":true},{\"AnswerOption\":\"7 section\",\"IsCurrect\":false}]','1','2013-11-22 05:42:09','admin'),
 (263,1,10,182,1,1,'Slide transition effect in presentation slide show is necessary?',0,'[]','[{\"AnswerOption\":\"Yes\",\"IsCurrect\":false},{\"AnswerOption\":\"No\",\"IsCurrect\":true}]','1','2013-11-22 05:52:31','admin'),
 (264,1,10,184,1,1,'Design template option is available under _______.',0,'[]','[{\"AnswerOption\":\"Ribbon tab\",\"IsCurrect\":true},{\"AnswerOption\":\"Footer tab\",\"IsCurrect\":false},{\"AnswerOption\":\"Header tab\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 05:58:33','admin'),
 (265,1,10,186,1,1,'Which of the following command is useful while editing existing slide?',0,'[]','[{\"AnswerOption\":\"Copy\",\"IsCurrect\":false},{\"AnswerOption\":\"Cut\",\"IsCurrect\":false},{\"AnswerOption\":\"Paste\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 06:02:02','admin'),
 (266,1,10,190,1,1,'New - Text are use to _______.',0,'[]','[{\"AnswerOption\":\"Communicate your ideas to your audience\",\"IsCurrect\":true},{\"AnswerOption\":\" communicate your ideas to power point\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 06:12:12','admin'),
 (267,1,10,192,1,1,'Which feature is used to change the existing fonts?',0,'[]','[{\"AnswerOption\":\"Format Fonts\",\"IsCurrect\":false},{\"AnswerOption\":\"Change Case\",\"IsCurrect\":false},{\"AnswerOption\":\"Replace Fonts\",\"IsCurrect\":true},{\"AnswerOption\":\"Toggle case\",\"IsCurrect\":false}]','1','2013-11-22 06:17:48','admin'),
 (268,1,10,193,1,1,'How to activate the textbox?',0,'[]','[{\"AnswerOption\":\"By moving it\",\"IsCurrect\":false},{\"AnswerOption\":\"By clicking on it\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 06:20:04','admin'),
 (269,1,10,194,1,1,'Notes are not visible in _________________.',0,'[]','[{\"AnswerOption\":\"Normal View\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide sorter View\",\"IsCurrect\":false},{\"AnswerOption\":\"Slide show\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 06:22:03','admin'),
 (270,1,10,195,1,1,'Shortcut key for spell check is ___.',0,'[]','[{\"AnswerOption\":\"F1\",\"IsCurrect\":false},{\"AnswerOption\":\"F10\",\"IsCurrect\":false},{\"AnswerOption\":\"F11\",\"IsCurrect\":false},{\"AnswerOption\":\"F7\",\"IsCurrect\":true}]','1','2013-11-22 06:23:53','admin'),
 (271,1,10,196,1,1,'Power point slide could be saved as ___________.',0,'[]','[{\"AnswerOption\":\"Folder\",\"IsCurrect\":false},{\"AnswerOption\":\"Web pages\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 06:26:00','admin'),
 (272,1,10,197,1,1,'Page set up option is present under ____________.',0,'[]','[{\"AnswerOption\":\"Menu bar\",\"IsCurrect\":true},{\"AnswerOption\":\"Header bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Footer bar\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 06:27:53','admin'),
 (273,1,10,198,1,1,'Save as option is present under _____.',0,'[]','[{\"AnswerOption\":\"Menu bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Header  bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Office button\",\"IsCurrect\":true},{\"AnswerOption\":\"Footer bar\",\"IsCurrect\":false}]','1','2013-11-22 06:29:42','admin'),
 (274,1,10,199,1,1,'When it is necessary to save power point pages as Web pages ?',0,'[]','[{\"AnswerOption\":\"When user want to run it as a slide show\",\"IsCurrect\":false},{\"AnswerOption\":\"When user want to run it on the internet\",\"IsCurrect\":true},{\"AnswerOption\":\"When user want to run it in slide sorter view\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 06:33:02','admin'),
 (275,1,10,202,1,1,'Exit option is present in __________.',0,'[]','[{\"AnswerOption\":\"Office button\",\"IsCurrect\":true},{\"AnswerOption\":\"Header bar\",\"IsCurrect\":false},{\"AnswerOption\":\"Footer bar\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-22 06:50:30','admin'),
 (276,1,11,207,1,1,'______ is used to connect networks in larger geographic areas.',0,'[]','[{\"AnswerOption\":\"Local Area Network (LAN)\",\"IsCurrect\":false},{\"AnswerOption\":\"Wide Area Network (WAN)\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-22 07:15:54','admin'),
 (277,1,11,208,1,1,'______is a set of rules to govern the data transfer between the devices.',0,'[]','[{\"AnswerOption\":\"Local Area Network\",\"IsCurrect\":false},{\"AnswerOption\":\"Workstations\",\"IsCurrect\":false},{\"AnswerOption\":\"Protocol\",\"IsCurrect\":true},{\"AnswerOption\":\"Local Area Network (LAN)\",\"IsCurrect\":false}]','1','2013-11-22 07:18:14','admin'),
 (278,1,11,212,1,1,'File transfer protocol allows user to transfer files from one computer to another.',0,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-22 07:23:59','admin'),
 (279,1,11,215,1,1,'The file and printer sharing allows other computers on a network to access resources on your computer by using a _______.',0,'[]','[{\"AnswerOption\":\"Workstations\",\"IsCurrect\":false},{\"AnswerOption\":\"Microsoft network\",\"IsCurrect\":true},{\"AnswerOption\":\"Printer\",\"IsCurrect\":false},{\"AnswerOption\":\"Post office Protocol \",\"IsCurrect\":false}]','1','2013-11-22 07:30:40','admin'),
 (280,1,11,216,1,1,'Sharing Printers describes how to use Windows to share a printer with others on network.',0,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":false},{\"AnswerOption\":\"False\",\"IsCurrect\":true}]','1','2013-11-22 07:35:43','admin'),
 (281,1,11,218,1,1,'Printers could be connected to network using: ',0,'[]','[{\"AnswerOption\":\"Find a printer in the directory\",\"IsCurrect\":false},{\"AnswerOption\":\"To connect to an Internet or intranet printer\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-22 08:06:17','admin'),
 (282,1,2,11,1,1,'Computer keyboard have:',0,'[]','[{\"AnswerOption\":\"Function keys\",\"IsCurrect\":false},{\"AnswerOption\":\"Special characters\",\"IsCurrect\":false},{\"AnswerOption\":\"Alphanumeric characters\",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":true}]','1','2013-11-22 08:12:31','admin'),
 (283,1,12,225,1,1,'Windows firewall settings have _________ tab.',0,'[]','[{\"AnswerOption\":\"General\",\"IsCurrect\":false},{\"AnswerOption\":\"Exceptions\",\"IsCurrect\":false},{\"AnswerOption\":\"Advanced\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-22 08:15:21','admin'),
 (284,1,12,226,1,1,'You shall use more than one firewall is one of the best practices',0,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":false},{\"AnswerOption\":\"False\",\"IsCurrect\":true}]','1','2013-11-22 08:18:24','admin'),
 (285,1,12,229,1,1,'Press F1 to get the list of operating systems.',0,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":false},{\"AnswerOption\":\"False\",\"IsCurrect\":true}]','1','2013-11-22 08:20:26','admin'),
 (286,1,12,230,1,1,'In Windows, some background processes can prevent applications from working correctly.',0,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-22 08:22:59','admin'),
 (287,1,13,231,1,1,'The Internet is a global system of interconnected computer networks that consists of _______.',0,'[]','[{\"AnswerOption\":\"government networks\",\"IsCurrect\":false},{\"AnswerOption\":\"academic\",\"IsCurrect\":false},{\"AnswerOption\":\"business\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-22 08:26:27','admin'),
 (288,1,13,239,1,1,'Which is very popular email service?',0,'[]','[{\"AnswerOption\":\"Gmail\",\"IsCurrect\":true},{\"AnswerOption\":\"Yahoo\",\"IsCurrect\":false},{\"AnswerOption\":\"Outlook \",\"IsCurrect\":false},{\"AnswerOption\":\"All of the above\",\"IsCurrect\":false}]','1','2013-11-22 08:33:50','admin'),
 (289,1,13,243,1,1,'User shall attach files of up to ___ to an email. This is one of the best practices.',0,'[]','[{\"AnswerOption\":\"1MB\",\"IsCurrect\":true},{\"AnswerOption\":\"5MB\",\"IsCurrect\":false},{\"AnswerOption\":\"10MB\",\"IsCurrect\":false},{\"AnswerOption\":\"20MB\",\"IsCurrect\":false}]','1','2013-11-22 08:38:07','admin'),
 (290,1,14,250,1,1,'Which is popular social media website?',0,'[]','[{\"AnswerOption\":\"MySpace\",\"IsCurrect\":false},{\"AnswerOption\":\"Twitter\",\"IsCurrect\":false},{\"AnswerOption\":\"YouTube\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-22 08:54:59','admin'),
 (291,1,8,91,1,1,'User could change the paper size before printing the document.',0,'[]','[{\"AnswerOption\":\"True\",\"IsCurrect\":true},{\"AnswerOption\":\"False\",\"IsCurrect\":false}]','1','2013-11-23 05:08:17','admin'),
 (292,1,8,93,1,1,'Shortcut key for cut:',0,'[]','[{\"AnswerOption\":\"CTRL %2B X\",\"IsCurrect\":true},{\"AnswerOption\":\"CTRL %2B V\",\"IsCurrect\":false},{\"AnswerOption\":\"CTRL %2B P\",\"IsCurrect\":false},{\"AnswerOption\":\"CTRL %2B O\",\"IsCurrect\":false}]','1','2013-11-23 05:12:40','admin'),
 (293,1,9,132,1,1,'What is the short cut key for undo?',0,'[]','[{\"AnswerOption\":\"Ctrl %2B X\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B Z\",\"IsCurrect\":true},{\"AnswerOption\":\"Ctrl %2B A\",\"IsCurrect\":false},{\"AnswerOption\":\"Ctrl %2B P\",\"IsCurrect\":false}]','1','2013-11-23 07:44:47','admin'),
 (294,1,9,150,1,1,'The content of the column is adjusted to column width using ________ feature.',0,'[]','[{\"AnswerOption\":\"Autofit\",\"IsCurrect\":true},{\"AnswerOption\":\"Autopilot\",\"IsCurrect\":false},{\"AnswerOption\":\"AutoCopy\",\"IsCurrect\":false},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-23 08:01:31','admin');
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
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `finalquizresponse`
--

/*!40000 ALTER TABLE `finalquizresponse` DISABLE KEYS */;
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
  `Expectedcontent` varchar(500) default NULL,
  `DatePosted` timestamp NOT NULL default CURRENT_TIMESTAMP,
  `userid` int(10) unsigned NOT NULL,
  `chapterId` int(10) unsigned NOT NULL,
  `sectionId` int(10) unsigned NOT NULL,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `reportissue`
--

/*!40000 ALTER TABLE `reportissue` DISABLE KEYS */;
/*!40000 ALTER TABLE `reportissue` ENABLE KEYS */;


--
-- Definition of table `siteanalytics`
--

DROP TABLE IF EXISTS `siteanalytics`;
CREATE TABLE `siteanalytics` (
  `Id` int(10) NOT NULL auto_increment,
  `time` int(10) default '2',
  `ipaddress` varchar(500) default NULL,
  `browser` varchar(500) default NULL,
  `language` varchar(500) default NULL,
  `os` varchar(500) default NULL,
  `pagename` varchar(500) default NULL,
  `pagetitle` varchar(500) default NULL,
  `refferpage` varchar(500) default NULL,
  `useragent` varchar(500) default NULL,
  `DateCreated` timestamp NOT NULL default CURRENT_TIMESTAMP,
  PRIMARY KEY  (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `siteanalytics`
--

/*!40000 ALTER TABLE `siteanalytics` DISABLE KEYS */;
/*!40000 ALTER TABLE `siteanalytics` ENABLE KEYS */;


--
-- Definition of table `studentcoursemapper`
--

DROP TABLE IF EXISTS `studentcoursemapper`;
CREATE TABLE `studentcoursemapper` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `userId` int(10) unsigned NOT NULL,
  `courseId` int(10) unsigned NOT NULL,
  `IsEnrolled` tinyint(3) unsigned default '0',
  `mappedOn` datetime NOT NULL,
  `updatedOn` timestamp NOT NULL default CURRENT_TIMESTAMP on update CURRENT_TIMESTAMP,
  PRIMARY KEY  (`Id`),
  KEY `FK_studentcoursemapper_1` (`userId`),
  KEY `FK_studentcoursemapper_2` (`courseId`),
  CONSTRAINT `FK_studentcoursemapper_1` FOREIGN KEY (`userId`) REFERENCES `userdetails` (`Id`),
  CONSTRAINT `FK_studentcoursemapper_2` FOREIGN KEY (`courseId`) REFERENCES `coursedetails` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `studentcoursemapper`
--

/*!40000 ALTER TABLE `studentcoursemapper` DISABLE KEYS */;
/*!40000 ALTER TABLE `studentcoursemapper` ENABLE KEYS */;


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
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `studentsqueries`
--

/*!40000 ALTER TABLE `studentsqueries` DISABLE KEYS */;
/*!40000 ALTER TABLE `studentsqueries` ENABLE KEYS */;


--
-- Definition of table `studentsqueryresponse`
--

DROP TABLE IF EXISTS `studentsqueryresponse`;
CREATE TABLE `studentsqueryresponse` (
  `Id` int(10) unsigned NOT NULL auto_increment,
  `QueryId` int(10) NOT NULL,
  `ResponsedBy` int(10) NOT NULL,
  `ResponsedDate` datetime NOT NULL,
  `Comments` varchar(250) character set utf8 NOT NULL,
  PRIMARY KEY  (`Id`),
  KEY `FK_studentsqueryresponse_1` (`QueryId`),
  CONSTRAINT `FK_studentsqueryresponse_1` FOREIGN KEY (`QueryId`) REFERENCES `studentsqueries` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `studentsqueryresponse`
--

/*!40000 ALTER TABLE `studentsqueryresponse` DISABLE KEYS */;
/*!40000 ALTER TABLE `studentsqueryresponse` ENABLE KEYS */;


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
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

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
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `userchapterstatus`
--

/*!40000 ALTER TABLE `userchapterstatus` DISABLE KEYS */;
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
  `RollNumber` int(10) unsigned NOT NULL,
  `userType` char(20) character set utf8 NOT NULL,
  `username` varchar(50) character set utf8 NOT NULL,
  `password` varchar(50) character set utf8 NOT NULL,
  `IsActive` tinyint(3) unsigned NOT NULL default '0',
  `IsEnabled` tinyint(3) unsigned NOT NULL default '0',
  `IsCompleted` tinyint(3) unsigned NOT NULL default '0',
  `IsCertified` tinyint(3) unsigned NOT NULL default '0',
  `VersionRegistered` varchar(3) NOT NULL default '1',
  `IsNewUser` tinyint(3) unsigned NOT NULL default '1',
  `isPrint` tinyint(3) unsigned NOT NULL default '0',
  `BatchYear` int(10) unsigned NOT NULL default '2013',
  `Course` varchar(50) NOT NULL default '0',
  `SubCourse` varchar(50) NOT NULL default '0',
  PRIMARY KEY  USING BTREE (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `userdetails`
--

/*!40000 ALTER TABLE `userdetails` DISABLE KEYS */;
INSERT INTO `userdetails` (`Id`,`DateCreated`,`LastLogin`,`firstName`,`LastName`,`FatherName`,`MotherName`,`EmailAddress`,`MobileNo`,`RollNumber`,`userType`,`username`,`password`,`IsActive`,`IsEnabled`,`IsCompleted`,`IsCertified`,`VersionRegistered`,`IsNewUser`,`isPrint`,`BatchYear`,`Course`,`SubCourse`) VALUES 
 (1,'2013-10-30 12:00:00','2013-10-30 12:00:00','Admin','Admin','F','M','admin@itmonarch.com','0',0,'Admin','admin','admin@123',1,1,1,1,'yes',1,0,2013,'0','0'),
 (2,'2013-11-19 22:13:49','2013-11-19 22:13:49','Temp','User','F','M','vasim.ahmed@itm.com','8080861670',123456,'User','temp_user','123',1,1,1,0,'1',0,0,2013,'0','0');
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
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `userlogger`
--

/*!40000 ALTER TABLE `userlogger` DISABLE KEYS */;
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
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `usertimetracker`
--

/*!40000 ALTER TABLE `usertimetracker` DISABLE KEYS */;
/*!40000 ALTER TABLE `usertimetracker` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
