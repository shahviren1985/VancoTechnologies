package com.vanco.merlinapp.network;

/**
 * Created by DELL on 02-08-2016.
 */
public interface URLS {


    String BASE_URL = "http://adminapps.in";
    String SERVER_URL = BASE_URL + "/NAAC/api/feedback/";
    String BASE_URL_2="http://vancotech.info/";
    String SERVER_URL_2 = BASE_URL_2;

    String LOGIN = "Login";
    String GETTEACHERDETAILS = "GetTeacherDetails";
    String GETFEEDBACKDETAILS = "GetFeedbackDetails";
    String ADDTEACHERFEEDBACKS = "AddTeacherFeedbacks";
    String ADDEXITFORM = "AddExitForm";
    String GETSECUREMENULIST = "StudentFeedback/prod/api/feedback/GetSecureMenuList";
    String GETANONYMOUSMENULIST="StudentFeedback/prod/api/feedback/GetAnonymousMenuList";
    String NOTIFICATION="notifications/dev/api/Notification/registerDevice";

}
