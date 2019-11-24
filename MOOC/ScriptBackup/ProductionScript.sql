-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.0.96-log


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema adminappcourse
--

CREATE DATABASE IF NOT EXISTS adminappcourse;
USE adminappcourse;

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
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `chapterdetails`
--

/*!40000 ALTER TABLE `chapterdetails` DISABLE KEYS */;
INSERT INTO `chapterdetails` (`Id`,`CourseId`,`Language`,`ContentVersion`,`FileName`,`DateCreated`,`ChapterName`) VALUES 
 (1,1,'En','1','DomyFile_0.html','2013-11-02 12:00:00','Chapter 1'),
 (2,1,'En','1','DomyFile_2.html','2013-11-08 12:00:00','Chapter 2');
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
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `chapterquizmaster`
--

/*!40000 ALTER TABLE `chapterquizmaster` DISABLE KEYS */;
INSERT INTO `chapterquizmaster` (`Id`,`courseId`,`chapterId`,`sectionId`,`questionText`,`IsQuestionOptionPresent`,`QuestionOption`,`AnswerOption`,`ContentVersion`,`DateCreated`,`createdby`) VALUES 
 (1,1,1,2,'Computer components are divided into two major categories, namely :',0x00,'[]','[{\"AnswerOption\":\"Input and output\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware and Software\",\"IsCurrect\":true},{\"AnswerOption\":\"Processing and Storage\",\"IsCurrect\":false},{\"AnswerOption\":\"Control Unit and Arithmetic Logic Unit\",\"IsCurrect\":false}]','1','2013-11-02 08:29:46','vasim'),
 (2,1,1,2,'Computer is a device which :',0x00,'[]','[{\"AnswerOption\":\"It takes input from the user,processes these data and produces the output\",\"IsCurrect\":false},{\"AnswerOption\":\"It could process both numerical as well as non-numerical calculations.\",\"IsCurrect\":false},{\"AnswerOption\":\"It controls all operations inside a computer.\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above.\",\"IsCurrect\":true}]','1','2013-11-02 08:31:49','vasim'),
 (3,1,1,2,'Which of the following are Computer Functions :',0x00,'[]','[{\"AnswerOption\":\"Processing\",\"IsCurrect\":false},{\"AnswerOption\":\"Storage\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-02 08:32:52','vasim'),
 (4,1,1,2,'Which of the following are basic Computer components :',0x00,'[]','[{\"AnswerOption\":\"Control Unit (CU)\",\"IsCurrect\":false},{\"AnswerOption\":\"Arithmetic Logic Unit (ALU)\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":true},{\"AnswerOption\":\"None of these\",\"IsCurrect\":false}]','1','2013-11-02 08:33:42','vasim'),
 (5,1,1,2,'The process of input, output, processing and storage is performed under the supervision of a unit called _____',0x00,'[]','[{\"AnswerOption\":\"Memory Unit\",\"IsCurrect\":false},{\"AnswerOption\":\"Input\",\"IsCurrect\":false},{\"AnswerOption\":\"Control Unit(CU)\",\"IsCurrect\":true},{\"AnswerOption\":\"Output\",\"IsCurrect\":false}]','1','2013-11-02 08:34:32','vasim'),
 (6,1,1,2,'Which of the major operations performed are addition, subtraction, multiplication, division, logic and comparisons.',0x00,'[]','[{\"AnswerOption\":\"Output\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware\",\"IsCurrect\":false},{\"AnswerOption\":\"Arithmetic Logic Unit (ALU)\",\"IsCurrect\":true},{\"AnswerOption\":\"Control Unit(CU)\",\"IsCurrect\":false}]','1','2013-11-02 08:36:05','vasim'),
 (7,1,1,2,'Which of the computer system are jointly known as the central processing unit (CPU).',0x00,'[]','[{\"AnswerOption\":\"Input and Output.\",\"IsCurrect\":false},{\"AnswerOption\":\"Arithmetic Logic Unit and Control Unit\",\"IsCurrect\":true},{\"AnswerOption\":\"Hardware and Software\",\"IsCurrect\":false},{\"AnswerOption\":\"All of above\",\"IsCurrect\":false}]','1','2013-11-02 08:37:53','vasim'),
 (8,1,1,2,'Which of the following major characteristics of a computer :',0x00,'[]','[{\"AnswerOption\":\"high speed\",\"IsCurrect\":false},{\"AnswerOption\":\" accuracy\",\"IsCurrect\":false},{\"AnswerOption\":\"diligence\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-02 08:39:14','vasim'),
 (9,1,1,2,'Which of the Computer Components takes care of step -by-step processing of all operations inside the computer :',0x00,'[]','[{\"AnswerOption\":\"Memory Unit\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware\",\"IsCurrect\":false},{\"AnswerOption\":\"Control Unit(CU)\",\"IsCurrect\":true},{\"AnswerOption\":\"None of the above\",\"IsCurrect\":false}]','1','2013-11-02 08:40:10','vasim'),
 (10,1,1,3,'Which of the following refers to the physical parts or components of a computer :',0x00,'[]','[{\"AnswerOption\":\"Monitor\",\"IsCurrect\":false},{\"AnswerOption\":\"Hardware\",\"IsCurrect\":true},{\"AnswerOption\":\"Keyboard\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":false}]','1','2013-11-02 08:41:32','vasim'),
 (11,1,1,5,'Which of the following refers to sets of programs, responsible for running the computer, controlling various operations of computer systems and management of computer resources :',0x00,'[]','[{\"AnswerOption\":\"Control Unit(CU)\",\"IsCurrect\":false},{\"AnswerOption\":\"System Software\",\"IsCurrect\":true},{\"AnswerOption\":\"Utility Programs\",\"IsCurrect\":false},{\"AnswerOption\":\"Operating System\",\"IsCurrect\":false}]','1','2013-11-02 08:43:26','vasim'),
 (12,1,1,5,'An operating system is ______',0x00,'[]','[{\"AnswerOption\":\"The process of input, output, processing and storage is performed under the supervision of a unit.\",\"IsCurrect\":false},{\"AnswerOption\":\"The system software that provides an interface for a user to communicate with the computer.\",\"IsCurrect\":false},{\"AnswerOption\":\"It manages hardware devices and supports application programs.\",\"IsCurrect\":false},{\"AnswerOption\":\"Both B and C\",\"IsCurrect\":true}]','1','2013-11-02 08:44:33','vasim'),
 (13,1,1,5,'Which of the programs that bridge the gap between the functionality of operating systems :',0x00,'[]','[{\"AnswerOption\":\"Operating System\",\"IsCurrect\":false},{\"AnswerOption\":\"Utility\",\"IsCurrect\":true},{\"AnswerOption\":\"System Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Application Software\",\"IsCurrect\":false}]','1','2013-11-02 08:45:30','vasim'),
 (14,1,1,6,'Application software can be broadly classified into two types : ',0x00,'[]','[{\"AnswerOption\":\"Hardware and Software \",\"IsCurrect\":false},{\"AnswerOption\":\"Generalized packages and Customized packages\",\"IsCurrect\":true},{\"AnswerOption\":\"Input and Output\",\"IsCurrect\":false},{\"AnswerOption\":\"System Software and Application Software\",\"IsCurrect\":false}]','1','2013-11-02 08:46:39','vasim'),
 (15,1,1,7,'Which of the following Data Analysis?',0x00,'[]','[{\"AnswerOption\":\"Lotus Smart suites\",\"IsCurrect\":false},{\"AnswerOption\":\"Word Perfect\",\"IsCurrect\":false},{\"AnswerOption\":\"Apple Numbers\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and C\",\"IsCurrect\":true}]','1','2013-11-02 08:47:22','vasim'),
 (16,1,1,7,'Which of the generalized packages are listed : ',0x00,'[]','[{\"AnswerOption\":\"Word Processing Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Database Management System\",\"IsCurrect\":false},{\"AnswerOption\":\"Spreadsheets\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-02 08:48:15','vasim'),
 (17,1,1,5,'Some of the Popular Operating Systems are :',0x00,'[]','[{\"AnswerOption\":\" Spreadsheets\",\"IsCurrect\":false},{\"AnswerOption\":\"MS-Access\",\"IsCurrect\":false},{\"AnswerOption\":\"Central Processing Unit(CPU)\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":true}]','1','2013-11-02 08:48:50','vasim'),
 (18,1,1,8,'Which of the applications that are customized (or developed) to meet the specific requirements of an organization/institution :',0x00,'[]','[{\"AnswerOption\":\"generalized packages\",\"IsCurrect\":false},{\"AnswerOption\":\"Utility programs\",\"IsCurrect\":false},{\"AnswerOption\":\"Application Software\",\"IsCurrect\":false},{\"AnswerOption\":\"Customized packages\",\"IsCurrect\":true}]','1','2013-11-02 08:49:54','vasim'),
 (19,1,1,5,'Utility programs are a broad category of software such as__',0x00,'[]','[{\"AnswerOption\":\"Compress (zip)/uncompress (unzip) files software\",\"IsCurrect\":false},{\"AnswerOption\":\"anti-virus software\",\"IsCurrect\":false},{\"AnswerOption\":\"split and join files software\",\"IsCurrect\":false},{\"AnswerOption\":\"All the above\",\"IsCurrect\":true}]','1','2013-11-02 08:50:37','vasim'),
 (20,1,1,3,'Physical parts of components of a computer such as following  :',0x00,'[]','[{\"AnswerOption\":\"Monitor\",\"IsCurrect\":false},{\"AnswerOption\":\"Central Unit(CU)\",\"IsCurrect\":false},{\"AnswerOption\":\"Central Processing Unit(CPU)\",\"IsCurrect\":false},{\"AnswerOption\":\"Both A and C\",\"IsCurrect\":true}]','1','2013-11-02 08:51:22','vasim'),
 (21,1,1,8,'Which of the packages are developed using high-level computer language : ',0x00,'[]','[{\"AnswerOption\":\"Generalized packages\",\"IsCurrect\":false},{\"AnswerOption\":\"Customized packages\",\"IsCurrect\":true},{\"AnswerOption\":\"Both A and B\",\"IsCurrect\":false},{\"AnswerOption\":\"None of above\",\"IsCurrect\":false}]','1','2013-11-02 08:52:23','vasim'),
 (22,1,2,9,'What is You Name',0x00,'[]','[{\"AnswerOption\":\"Vasim\",\"IsCurrect\":false},{\"AnswerOption\":\"Ahmed\",\"IsCurrect\":false},{\"AnswerOption\":\"Nel\",\"IsCurrect\":true}]','1','2013-11-08 08:53:27','admin'),
 (23,1,2,9,'How are You',0x00,'[]','[{\"AnswerOption\":\"Fine\",\"IsCurrect\":true},{\"AnswerOption\":\"Not Fine\",\"IsCurrect\":false}]','1','2013-11-08 09:00:44','admin');
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
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `chapterquizresponse`
--

/*!40000 ALTER TABLE `chapterquizresponse` DISABLE KEYS */;
INSERT INTO `chapterquizresponse` (`userid`,`QuestionId`,`userReaponse`,`IsCorrect`,`DateCreated`,`Id`) VALUES 
 (1,10,'Monitor',0,'2013-11-04 09:05:10',1),
 (1,20,'Both A and C',1,'2013-11-04 09:06:18',2),
 (1,1,'Input and output',0,'2013-11-04 09:23:22',3),
 (2,9,'Control Unit(CU)',1,'2013-11-05 12:38:42',4),
 (2,8,'high speed',0,'2013-11-05 12:38:50',5),
 (2,6,'Output',0,'2013-11-05 12:38:59',6),
 (2,3,'None of these',0,'2013-11-05 12:39:23',7),
 (2,10,'Hardware',1,'2013-11-05 12:52:10',8),
 (2,20,'Monitor',0,'2013-11-05 12:53:16',9),
 (2,22,'Nel',1,'2013-11-05 12:53:16',10),
 (2,23,'Not Fine',0,'2013-11-05 12:53:16',11);
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
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

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
 (9,2,'Introduction','DomyFile_3.html',500,'2013-11-08 08:52:52'),
 (10,2,'Computer Networks','DomyFile_3.html',1800,'2013-11-08 08:52:52');
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
  CONSTRAINT `FK_userchapterstatus_2` FOREIGN KEY (`chapterId`) REFERENCES `chapterdetails` (`Id`),
  CONSTRAINT `FK_userchapterstatus_1` FOREIGN KEY (`userId`) REFERENCES `userdetails` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `userchapterstatus`
--

/*!40000 ALTER TABLE `userchapterstatus` DISABLE KEYS */;
INSERT INTO `userchapterstatus` (`Id`,`userId`,`chapterId`,`sectionId`,`contentVersion`,`DateCreated`) VALUES 
 (1,1,1,1,'en','2013-10-30 12:00:00'),
 (2,1,1,3,'en','2013-10-30 12:00:00');
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
  `userType` char(20) character set utf8 NOT NULL,
  `username` char(20) character set utf8 NOT NULL,
  `password` varchar(50) character set utf8 NOT NULL,
  `IsActive` tinyint(3) unsigned NOT NULL default '0',
  `IsEnabled` tinyint(3) unsigned NOT NULL default '0',
  `IsCompleted` tinyint(3) unsigned NOT NULL default '0',
  `IsCertified` tinyint(3) unsigned NOT NULL default '0',
  `VersionRegistered` varchar(3) NOT NULL default '1',
  PRIMARY KEY  USING BTREE (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `userdetails`
--

/*!40000 ALTER TABLE `userdetails` DISABLE KEYS */;
INSERT INTO `userdetails` (`Id`,`DateCreated`,`LastLogin`,`firstName`,`LastName`,`FatherName`,`MotherName`,`EmailAddress`,`MobileNo`,`userType`,`username`,`password`,`IsActive`,`IsEnabled`,`IsCompleted`,`IsCertified`,`VersionRegistered`) VALUES 
 (1,'2013-10-30 12:00:00','2013-10-30 12:00:00','Admin','Admin','F','M','admin@itmonarch.com','0','Admin','admin','admin@123',1,1,1,1,'yes'),
 (2,'2013-10-30 12:00:00','2013-10-30 12:00:00','Student 1','Student 1','F','M','vasim.ahmed@itmonarch.com','0','User','student_1','student@123',1,1,0,0,'yes'),
 (3,'2013-10-30 12:00:00','2013-10-30 12:00:00','Student 2','Student 2',NULL,NULL,'admin@itm.com','8080861670','User','student_2','student@123',1,1,1,1,'Yes'),
 (5,'2013-11-06 04:58:30','2013-11-06 04:58:30','Student 3','Student 3',NULL,NULL,'akram.ahmed@itm.com','9981791085','User','student_3','student@123',1,1,0,0,'Yes'),
 (6,'2013-11-06 05:08:59','2013-11-06 05:08:59','Student 4','Student 4',NULL,NULL,'akram.ahmed@itm.com','9981791085','User','student_4','student@123',1,1,0,0,'Yes');
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
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=latin1;

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
 (6,'2013-11-12 03:32:59','120.138.105.180',26);
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
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `usertimetracker`
--

/*!40000 ALTER TABLE `usertimetracker` DISABLE KEYS */;
INSERT INTO `usertimetracker` (`Id`,`userId`,`chapterId`,`sectionId`,`timespent`) VALUES 
 (1,3,1,3,660),
 (2,3,2,1,230),
 (3,2,1,1,1420),
 (4,2,1,2,5130),
 (5,2,1,4,30),
 (6,2,1,3,30),
 (7,2,1,5,10),
 (8,2,1,6,10),
 (9,2,1,7,20),
 (10,2,1,8,3410);
/*!40000 ALTER TABLE `usertimetracker` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
