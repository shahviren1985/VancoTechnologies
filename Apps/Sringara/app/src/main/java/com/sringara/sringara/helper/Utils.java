package com.sringara.sringara.helper;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.view.View;
import android.view.inputmethod.InputMethodManager;

import com.sringara.sringara.R;

import java.io.IOException;
import java.io.InputStream;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

public class Utils {

    public static Boolean isStringNull(String nullableString) {
        Boolean result = true;
        if (nullableString != null && !nullableString.equals("null") && !nullableString.equals("n/a") && !nullableString.equals("N/A") && !nullableString.equals("") && !nullableString.equals("[ ]") && !nullableString.equals("[]") && !nullableString.equals("{}") && nullableString != null) { //NON-NLS
            result = false;
        }
        return result;
    }


    public static boolean isConnectingToInternet(Context context, boolean isShowAlertDialog) {
        ConnectivityManager cm = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
        NetworkInfo ni = cm.getActiveNetworkInfo();
        if (ni == null) {
            if(isShowAlertDialog) {
                DialogAlert.show_dialog(context, context.getString(R.string.internet_msg));
            }
            return false;
        } else
            return true;
    }

    public static void setPrefString(Context mContext,String key, String value) {
        SharedPreferences.Editor editor = mContext.getSharedPreferences(Constants.DB_NAME, mContext.MODE_PRIVATE).edit();
        editor.putString(key, value);
        editor.apply();
    }

    public static String getPrefString(Context mContext, String key) {
        SharedPreferences prefs = mContext.getSharedPreferences(Constants.DB_NAME, mContext.MODE_PRIVATE);
        String value = prefs.getString(key, null);
        return value;
    }

    public static String retriveState(Context context, String filePath) {
        String json = null;
        try {
            InputStream is = context.getAssets().open(filePath);
            int size = is.available();
            byte[] buffer = new byte[size];
            is.read(buffer);
            is.close();
            json = new String(buffer, "UTF-8");
        } catch (IOException ex) {
            ex.printStackTrace();
            return null;
        }
        return json;
    }

    public static void hideKeyboard(View view, Context mContext) {
        InputMethodManager inputMethodManager =(InputMethodManager) mContext.getSystemService(Activity.INPUT_METHOD_SERVICE);
        inputMethodManager.hideSoftInputFromWindow(view.getWindowToken(), 0);
    }

    public static long getMillisFromDate(String date) {
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        try {
            Date mDate = sdf.parse(date);
            long timeInMilliseconds = mDate.getTime();
            return timeInMilliseconds;
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return 0;
    }

}
