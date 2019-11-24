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
-- Create schema adminapps
--

CREATE DATABASE IF NOT EXISTS adminapps;
USE adminapps;

--
-- Definition of table `logindetails`
--

DROP TABLE IF EXISTS `logindetails`;
CREATE TABLE `logindetails` (
  `CollegeId` int(10) unsigned NOT NULL auto_increment,
  `CollegeName` varchar(500) character set utf8 NOT NULL,
  `UserName` varchar(500) character set utf8 NOT NULL,
  `Password` varchar(250) character set utf8 NOT NULL,
  `LastLogin` datetime NOT NULL,
  `ConnectionString` varchar(1024) character set utf8 NOT NULL,
  `PDFFolderPath` varchar(1024) character set utf8 NOT NULL,
  `ReleaseFilePath` varchar(1024) character set utf8 NOT NULL,
  `LogFilePath` varchar(1024) NOT NULL,
  `RoleType` varchar(45) NOT NULL,
  `ApplicationType` varchar(45) NOT NULL,
  `RedirectUrl` varchar(500) NOT NULL,
  PRIMARY KEY  (`CollegeId`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `logindetails`
--

/*!40000 ALTER TABLE `logindetails` DISABLE KEYS */;
INSERT INTO `logindetails` (`CollegeId`,`CollegeName`,`UserName`,`Password`,`LastLogin`,`ConnectionString`,`PDFFolderPath`,`ReleaseFilePath`,`LogFilePath`,`RoleType`,`ApplicationType`,`RedirectUrl`) VALUES 
 (1,'mnwc','mnwcba','admin@ba','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=mnwcexambabcom1_clean;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MNWC/BABCOM/','~/Release/MNWC/Configuration.xml','~/Logs/MNWC/','Student','Exam','~/StudentExamRegistration.aspx'),
 (2,'mnwc','mnwcbafi','admin@bafi','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=mnwcbafi;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MNWC/BAFI/','~/Release/MNWC/Configuration.xml','~/Logs/MNWC/','Student','Exam','~/StudentExamRegistration.aspx'),
 (3,'mnwc','mnwcbms','admin@bms','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=mnwcbms;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MNWC/BMS/','~/Release/MNWC/Configuration.xml','~/Logs/MNWC/','Student','Exam','~/StudentExamRegistration.aspx'),
 (4,'mnwc','mnwcma','admin@ma','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=mnwc_cms_registration;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MNWC/MA/','~/Release/MNWC/Configuration.xml','~/Logs/MNWC/','Student','Exam','~/StudentExamRegistration.aspx'),
 (5,'mmpshah','mmpshahba','admin@ba','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=mmpshahcms_ba;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MMPSHAH/BA/','~/Release/MMPSHAH/Configuration.xml','~/Logs/MMPSHAH/','Student','Exam','~/StudentExamRegistration.aspx'),
 (6,'mmpshah','mmpshahbcom','admin@bcom','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=mmpshahcms_bcom;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MMPSHAH/BCOM/','~/Release/MMPSHAH/Configuration.xml','~/Logs/MMPSHAH/','Student','Exam','~/StudentExamRegistration.aspx'),
 (7,'mmpshah','mmpshahbms','admin@bms','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=mmpshah_bms;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MMPSHAH/BMS/','~/Release/MMPSHAH/Configuration.xml','~/Logs/MMPSHAH/','Student','Exam','~/StudentExamRegistration.aspx'),
 (8,'mmpshah','mmpshahbmm','admin@bmm','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=mmpshahcms_bmm;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MMPSHAH/BMM/','~/Release/MMPSHAH/Configuration-bmm.xml','~/Logs/MMPSHAH/','Student','Exam','~/StudentExamRegistration.aspx'),
 (9,'mmpshah','mmpadminba','admin@mmp','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=mmp_ba_2;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MMPSHAH/BA/','~/Release/MMPSHAH/Configuration.xml','~/Logs/MMPSHAH/','Admin','Exam','~/Admin/ImportStudents.aspx'),
 (10,'mmpshah','mmpadminbcom','admin@mmp','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=mmpshahbcom;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MMPSHAH/BCOM/','~/Release/MMPSHAH/Configuration.xml','~/Logs/MMPSHAH/','Admin','Exam','~/Admin/ImportStudents.aspx'),
 (11,'mmpshah','mmpadminbms','admin@mmp','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=mmpshah_bms;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MMPSHAH/BMS/','~/Release/MMPSHAH/Configuration.xml','~/Logs/MMPSHAH/','Admin','Exam','~/Admin/ImportStudents.aspx'),
 (12,'mmpshah','mmpadminbmm','admin@mmp','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=mmpshahcms_bmm;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MMPSHAH/BMM/','~/Release/MMPSHAH/Configuration-bmm.xml','~/Logs/MMPSHAH/','Admin','Exam','~/Admin/ImportStudents.aspx'),
 (13,'mnwc','mnwcadminba','admin@mnwc','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=mnwcexambabcom1_clean;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MNWC/BABCOM/','~/Release/MNWC/Configuration.xml','~/Logs/MNWC/','Admin','Exam','~/Admin/ImportStudents.aspx'),
 (14,'mnwc','mnwcadminbafi','admin@mnwc','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=mnwcbafi;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MNWC/BAFI/','~/Release/MNWC/Configuration.xml','~/Logs/MNWC/','Admin','Exam','~/Admin/ImportStudents.aspx'),
 (15,'mnwc','mnwcadminbms','admin@mnwc','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=mnwcbms;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MNWC/BMS/','~/Release/MNWC/Configuration.xml','~/Logs/MNWC/','Admin','Exam','~/Admin/ImportStudents.aspx'),
 (16,'mnwc','mnwcadminma','admin@mnwc','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=mnwc_cms_registration;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MNWC/MA/','~/Release/MNWC/Configuration.xml','~/Logs/MNWC/','Admin','Exam','~/Admin/ImportStudents.aspx');
/*!40000 ALTER TABLE `logindetails` ENABLE KEYS */;


--
-- Definition of table `userlogins`
--

DROP TABLE IF EXISTS `userlogins`;
CREATE TABLE `userlogins` (
  `CollegeId` int(10) unsigned NOT NULL auto_increment,
  `CollegeName` varchar(500) character set utf8 NOT NULL,
  `UserName` varchar(500) character set utf8 NOT NULL,
  `Password` varchar(250) character set utf8 NOT NULL,
  `LastLogin` datetime NOT NULL,
  `ConnectionString` varchar(1024) character set utf8 NOT NULL,
  `PDFFolderPath` varchar(1024) character set utf8 NOT NULL,
  `ReleaseFilePath` varchar(1024) character set utf8 NOT NULL,
  `LogFilePath` varchar(1024) NOT NULL,
  `userType` varchar(45) NOT NULL,
  `ApplicationType` varchar(45) NOT NULL,
  `RedirectUrl` varchar(500) NOT NULL,
  PRIMARY KEY  (`CollegeId`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;

--
-- Dumping data for table `userlogins`
--

/*!40000 ALTER TABLE `userlogins` DISABLE KEYS */;
INSERT INTO `userlogins` (`CollegeId`,`CollegeName`,`UserName`,`Password`,`LastLogin`,`ConnectionString`,`PDFFolderPath`,`ReleaseFilePath`,`LogFilePath`,`userType`,`ApplicationType`,`RedirectUrl`) VALUES 
 (1,'mnwc','admin','admin@123','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=college;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MNWC/','~/Release/Configuration.xml','~/Logs/MNWC/','Admin','Course','~/Admin/Dashboard.aspx'),
 (2,'mnwc','student_1','student@123','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=college;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MNWC/','~/Release/Configuration.xml','~/Logs/MNWC/','User','Course','~/Dashboard.aspx'),
 (3,'mnwc','student_2','student@123','2013-08-23 00:00:00','Server=localhost;Port=3306;Database=college;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MNWC/','~/Release/Configuration.xml','~/Logs/MNWC/','User','Course','~/Dashboard.aspx'),
 (4,'mnwc','student_3','student@123','2013-11-06 04:58:30','Server=localhost;Port=3306;Database=college;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MNWC/','~/Release/Configuration.xml','~/Logs/MNWC/','User','Course','~/Dashboard.aspx'),
 (5,'mnwc','student_4','student@123','2013-11-06 05:09:05','Server=localhost;Port=3306;Database=college;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MNWC/','~/Release/Configuration.xml','~/Logs/MNWC/','User','Course','~/Dashboard.aspx'),
 (6,'mnwc','student_5','student@123','2013-11-13 06:49:02','Server=localhost;Port=3306;Database=college;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MNWC/','~/Release/Configuration.xml','~/Logs/MNWC/','Course','User','~/Dashboard.aspx'),
 (7,'mnwc','student_6','student@123','2013-11-13 06:50:50','Server=localhost;Port=3306;Database=college;Uid=root;Pwd=sndt;Pooling=false;Connect timeout=900;','~/PDF/MNWC/','~/Release/Configuration.xml','~/Logs/MNWC/','Course','User','~/Dashboard.aspx');
/*!40000 ALTER TABLE `userlogins` ENABLE KEYS */;




/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;