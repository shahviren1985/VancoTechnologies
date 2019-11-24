package com.vanco.merlinapp.network;


import android.content.Context;

import com.vanco.merlinapp.R;
import com.vanco.merlinapp.utility.Utility;

import java.util.concurrent.TimeUnit;

import okhttp3.OkHttpClient;
import okhttp3.logging.HttpLoggingInterceptor;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.converter.scalars.ScalarsConverterFactory;

/**
 * Created by PCS77 on 05-10-2015.
 */
final public class RetrofitInterface {

    static Retrofit retrofitForServer = null;
    static Retrofit retrofitForSecondServer = null;


    public static RestApiMethods callToMethod() {
        if (retrofitForServer == null) {
            HttpLoggingInterceptor logging = new HttpLoggingInterceptor();
            // set your desired log level
            logging.setLevel(HttpLoggingInterceptor.Level.BODY);
            OkHttpClient.Builder httpClient = new OkHttpClient.Builder();
            // add logging as last interceptor
            httpClient.addInterceptor(logging);  // <-- this is the important line!

            // Retrofit setup
            retrofitForServer = new Retrofit.Builder()
                    .baseUrl(URLS.SERVER_URL)
                    .addConverterFactory(ScalarsConverterFactory.create())
                    .addConverterFactory(GsonConverterFactory.create())
                    .client(httpClient.build())
                    .build();
            // Service setup
        }
        return retrofitForServer.create(RestApiMethods.class);
    }

    public static RestApiMethods callToMethodSecondServer() {
        if (retrofitForSecondServer == null) {
            HttpLoggingInterceptor logging = new HttpLoggingInterceptor();
            // set your desired log level
            logging.setLevel(HttpLoggingInterceptor.Level.BODY);
            OkHttpClient.Builder httpClient = new OkHttpClient.Builder();
            httpClient.readTimeout(60, TimeUnit.SECONDS);
            httpClient.connectTimeout(60, TimeUnit.SECONDS);
            // add logging as last interceptor
            httpClient.addInterceptor(logging);  // <-- this is the important line!

            // Retrofit setup
            retrofitForSecondServer = new Retrofit.Builder()
                    .baseUrl(URLS.SERVER_URL_2)
                    .addConverterFactory(ScalarsConverterFactory.create())
                    .addConverterFactory(GsonConverterFactory.create())
                    .client(httpClient.build())
                    .build();
            // Service setup
        }
        return retrofitForSecondServer.create(RestApiMethods.class);
    }

    public static void handleResponseError(Context context, Response response) {


        if (context == null)
            return;
        if (response != null)
            Utility.openAlertDialog(context, response.message());
        else
            Utility.openAlertDialog(context, context.getString(R.string.msg_response_error));
    }

    public static void handleFailerError(Context context, Throwable t) {
        if (context == null)
            return;

        if (!Utility.isInternetConnectionAvailable(context)) {
            Utility.openAlertDialog(context, context.getString(R.string.msg_internet_not_available));
        }

        if (t == null) {
            Utility.openAlertDialog(context, context.getString(R.string.msg_response_error));
            return;
        }
        Utility.openAlertDialog(context, t.getMessage());
    }

    /*public static Object handleResponse(Context context, Response response, View view) {
        if (response != null && response.isSuccessful()) {
            Object object = response.body();
            ClsCommonResponse clsCommonResponse = (ClsCommonResponse) object;
            if (clsCommonResponse != null && clsCommonResponse.getStatus() == -1) {
                Utility.logout(context);
            }
            if (clsCommonResponse != null && clsCommonResponse.getStatus() == 1) {
                return object;
            } else {
                if (clsCommonResponse == null) {
                    if (view != null) {
                        Utility.showSnackBar(view, R.string.alert_error_in_response);
                    } else {
                        Utility.openAlertDialog(context, R.string.alert_error_in_response);
                    }

                } else {
                    if (view != null) {
                        Utility.showSnackBar(view, clsCommonResponse.getMessage());
                    } else {
                        Utility.openAlertDialog(context, clsCommonResponse.getMessage());
                    }
                }
                return null;
            }
        } else {
            if (response != null) {
                try {
                    if (view != null) {
                        Utility.showSnackBar(view, response.errorBody().string());
                    } else {
                        Utility.openAlertDialog(context, response.errorBody().string());
                    }

                } catch (IOException e) {
                    e.printStackTrace();
                }
            } else {
                if (view != null) {
                    Utility.showSnackBar(view, R.string.alert_error_in_response);
                } else {
                    Utility.openAlertDialog(context, R.string.alert_error_in_response);
                }
            }

            return null;
        }

    }*/
}
