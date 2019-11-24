package com.navigettr.app.WebServices;

import com.google.gson.JsonObject;
import com.navigettr.app.Model.CurrencyModel;
import com.navigettr.app.Model.ForgotPassowrdModel;
import com.navigettr.app.Model.LoginModel;
import com.navigettr.app.Model.RegisterModel;
import com.navigettr.app.Model.RewardsModel;
import com.navigettr.app.Model.ScanQRCodeModel;
import com.navigettr.app.Model.SearchResultMoneyModel;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.Field;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.GET;
import retrofit2.http.Headers;
import retrofit2.http.POST;
import retrofit2.http.Query;

public interface ApiInterface {

    @GET("getAllCurrencies")
    Call<CurrencyModel> getCurrency();

    @Headers("Content-Type: application/json")
    @POST("search")
    Call<SearchResultMoneyModel> getSearchResult(@Body JsonObject jsonObject);

    @POST("login")
    @FormUrlEncoded
    Call<List<LoginModel>> getLogin(@Field("username") String username, @Field("Password") String Password);

    @POST("forgotPassword")
   // @FormUrlEncoded
    Call<ForgotPassowrdModel> getForgotPassword(@Query("emailId") String emailId);

    @Headers("Content-Type: application/json")
    @POST("register")
    Call<RegisterModel> getRegister(@Body JsonObject jsonObject);

    @POST("scanPartnerQRCode")
    Call<ScanQRCodeModel> getScanQRCod(@Query("userId") String userId, @Query("partnerId") String partnerId,
                                       @Query("locationId") String locationId,
                                       @Query("amount") String amount, @Query("fromCurrency") String fromCurrency,
                                       @Query("toCurrency") String toCurrency,
                                       @Query("dateScanned") String dateScanned);

    @POST("getRewards")
    Call<List<RewardsModel>> getRewards(@Query("userId") int userId);
}
