


CREATE TABLE Transcript (
    RequestId int NOT NULL AUTO_INCREMENT,
    FirstName varchar(40) NOT NULL,
    LastName varchar(40),
    PNR varchar(20),
    AdmissionYear varchar(20),
    ReqRecvdOn datetime,
    RequestStatus int,
    PRIMARY KEY (RequestId)
);


ALTER TABLE Transcript AUTO_INCREMENT = 1000;