package com.vanco.merlinapp.network;

import com.google.gson.JsonObject;

import java.net.URL;
import java.util.HashMap;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.Query;


/**
 */
public interface RestApiMethods {


    @POST(URLS.LOGIN)
    Call<String> login(@Body HashMap<String, Object> map);

    @POST(URLS.ADDTEACHERFEEDBACKS)
    Call<String> addTeacherFeedBack(@Body JsonObject jsonObject);

    @POST(URLS.ADDEXITFORM)
    Call<String> addExitForm(@Body JsonObject jsonObject);

    @GET(URLS.GETTEACHERDETAILS)
    Call<String> getTeacherDetails(@Query("id") String id);

    @GET(URLS.GETFEEDBACKDETAILS)
    Call<String> getFeedBackDetail();

    @GET(URLS.GETSECUREMENULIST)
    Call<String> getSecureMenuList(@Query("id") String id);

    @GET(URLS.GETANONYMOUSMENULIST)
    Call<String> getAnonymousMenuList(@Query("id") String id);

    @POST(URLS.NOTIFICATION)
    Call<String> notification(@Body HashMap<String, Object> map);


}
