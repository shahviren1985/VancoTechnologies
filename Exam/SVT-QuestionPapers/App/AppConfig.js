'use strict';
var Examapp = angular.module("ExamSystemApp", ["ngRoute"]);

Examapp.config(function ($routeProvider) {
    $routeProvider
        .when("/home", {
            templateUrl: "View/Dashboard.html",
            controller: "DashboardCtrl",
            activetab: 'Dashboard'
        })
        .when("/MenuDashboard", {
            templateUrl: "View/MenuDashboard.html",
            controller: "MenuDashboardCtrl",
            activetab: 'MenuDashboard'
        })
        .when("/importStudent", {
            templateUrl: "View/ImportStudent.html",
            controller: "ImportStudentCtrl",
            activetab: 'ImportStudents'
        })
        .when("/ExamForms", {
            templateUrl: "View/ExamForms.html",
            controller: "ExamFormsCtrl",
            activetab: "ExamForms"
        })
        .when("/SeatNumbers", {
            templateUrl: "View/SeatNumbers.html",
            controller: "SeatNumbersCtrl",
            activetab: "SeatNumbers"
        })
        .when("/internalMarkEntry", {
            templateUrl: "View/internalMarkEntry.html",
            controller: "internalMarkEntryCtrl",
            activetab: "internalMarkEntry"
        })
        .when("/HallTickets", {
            templateUrl: "View/HallTickets.html",
            controller: "HallTicketsCtrl",
            activetab: "HallTickets"
        })
        .when("/ExternalMarkEntry", {
            templateUrl: "View/ExternalMarkEntry.html",
            controller: "ExternalMarkEntryCtrl",
            activetab: "ExternalMarkEntry"
        })
        .when("/PracticalMarkEntry", {
            templateUrl: "View/PractcalMarkEntr.html",
            controller: "PracticalMarkEntryCtrl",
            activetab: "PracticalMarkEntry"
        })
        .when("/GraceMarkEntry", {
            templateUrl: "View/GraceMarkEntry.html",
            controller: "GraceMarkEntryCtrl",
            activetab: "GraceMarkEntry"
        })
        .when("/marksheets", {
            templateUrl: "View/marksheets.html",
            controller: "marksheetsCtrl",
            activetab: "marksheets"
        })
        .when("/ledger-report", {
            templateUrl: "View/ledger-report.html",
            controller: "ledgerreportCtrl",
            activetab: "ledger-report"
        })
        .when("/OtherReports", {
            templateUrl: "View/OtherReports.html",
            controller: "OtherReportsCtrl",
            activetab: "OtherReports"
        })
        .when("/UpdateInfo", {
            templateUrl: "View/UpdateInfo.html",
            controller: "UpdateInfoCtrl",
            activetab: "UpdateInfo"
        })
        .when("/Login", {
            templateUrl: "View/Login.html",
            controller: "LoginCtrl",
        })
        .when("/ExternalExamSheet", {
            templateUrl: "View/ExternalExamSheet.html",
            controller: "ExternalExamSheetCtrl",
            activetab: "ExternalExamSheet"
        })
        .when("/UploadPaper", {
            templateUrl: "View/UploadPaper.html",
            controller: "UploadPaperCtrl",
            activetab: "UploadPaper"
        })
        .when("/ViewPreviousPapers", {
            templateUrl: "View/ViewPreviousPapers.html",
            controller: "ViewPreviousPapersCtrl",
            activetab: "ViewPreviousPapers"
        })
        .when("/GenerateATKTStudent", {
            templateUrl: "View/GenerateATKTStudent.html",
            controller: "GenerateATKTStudentCtrl",
            activetab: "GenerateATKTStudent"
        })
        .when("/AddExtraSubject", {
            templateUrl: "View/AddExtraSubject.html",
            controller: "AddExtraSubjectCtrl",
            activetab: "AddExtraSubject"
        })
        .when("/AddPracticalSubject", {
            templateUrl: "View/AddPracticalSubject.html",
            controller: "AddPracticalSubjectCtrl",
            activetab: "AddPracticalSubject"
        })
        .when("/StudentResult", {
            templateUrl: "View/AddPracticalSubject.html",
            controller: "AddPracticalSubjectCtrl",
            activetab: "AddPracticalSubject"
        })
        .when("/ProcessStudentToDB", {
            templateUrl: "View/ProcessStudentToDB.html",
            controller: "ProcessStudentToDBCtrl",
            activetab: "ProcessStudentToDB"
        })
        .when("/GenerateAggregateMarksheet", {
            templateUrl: "View/GenerateAggregateMarksheet.html",
            controller: "GenerateAggregateMarksheetCtrl",
            activetab: "GenerateAggregateMarksheet"
        })
        .when("/Summaryreport", {
            templateUrl: "View/DynamicView.html",
            controller: "SummaryreportCtrl",
            activetab: "Summaryreport"
        })
        .when("/TopperReport", {
            templateUrl: "View/TopperReport.html",
            controller: "TopperReportCtrl",
            activetab: "TopperReport"
        })
        .when("/AggregateTopperReport", {
            templateUrl: "View/AggregateTopperReport.html",
            controller: "AggregateTopperReportCtrl",
            activetab: "AggregateTopperReport"
        })
        .when("/YearSetting", {
            templateUrl: "View/YearSetting.html",
            controller: "YearSettingCtrl",
            activetab: "YearSetting"
        })
        .when("/SeatNumberAdj", {
            templateUrl: "View/Setting/SeatNumberAdj.html",
            controller: "SeatNumberAdjCtrl",
            activetab: "SeatNumberAdj"
        })
        .when("/AdvancePaper", {
            templateUrl: "App/PaperMapping/AdvancePaper/AdvancePaper.html",
            controller: "AdvancePaperCtrl",
            activetab: "SeatNumberAdj"
        })
        .when("/TranscriptRequest", {
            templateUrl: "App/OnlineQueries/Transcript.html",
            controller: "TranscriptCtrl",
            activetab: "Transcript"
        })
        .when("/UpdateAggregateStudentDetails", {
            templateUrl: "App/Student/UpdateAggregateStudentDetails.html",
            controller: "AggregateStudentForm",
            activetab: "Transcript"
        })
        .otherwise({
            templateUrl: "View/Dashboard.html",
            controller: "DashboardCtrl",
            activetab: 'Dashboard'
        });
});
