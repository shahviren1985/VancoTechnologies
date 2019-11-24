DROP TABLE IF EXISTS `userdetails`;
CREATE TABLE userdetails(
userid int NOT NULL AUTO_INCREMENT,
mobilenumber VARCHAR(50) NOT NULL,
isvalidated int(1) NOT NULL,
dateofbirth date NULL,
usertype VARCHAR(50) NOT NULL,
emergency1 VARCHAR(50) NULL,
emergency2 VARCHAR(50) NULL,
emergency3 VARCHAR(50) NULL,
emergency4 VARCHAR(50) NULL,
emergency5 VARCHAR(50) NULL,
PRIMARY KEY (userId));

DROP TABLE IF EXISTS `otplogs`;
CREATE TABLE otplogs(
otplogsid int NOT NULL AUTO_INCREMENT,
userid int NOT NULL,
senttomobilenumber VARCHAR(50) NOT NULL,
otpmessage VARCHAR(1000) NOT NULL,
otpcode  VARCHAR(10) NOT NULL,
isvalidated int(1) NOT NULL,
sentat datetime NOT NULL,
validatedat datetime NULL,
PRIMARY KEY (otplogsid));

DROP TABLE IF EXISTS `soslogs`;
CREATE TABLE soslogs(
soslogsid int NOT NULL AUTO_INCREMENT,
userid int NOT NULL,
senttomobilenumber VARCHAR(50) NOT NULL,
sosmessage VARCHAR(1000) NOT NULL,
sentat datetime NOT NULL,
PRIMARY KEY (soslogsid));

DROP TABLE IF EXISTS `smsapiauditlogs`;
CREATE TABLE smsapiauditlogs(
smsapiauditlogsid int NOT NULL AUTO_INCREMENT,
userid int NOT NULL,
apirequestbody TEXT NULL,
apiresponsebody TEXT NULL, 
PRIMARY KEY (smsapiauditlogsid));

