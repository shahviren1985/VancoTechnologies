package com.navigettr.app.Constant;

import android.content.Context;
import android.widget.Toast;

public class Constant {

    public static final String BASE_URL = "https://vancotech.com/navigettr/api/";

    public static void getToast(Context context, String msg){
        Toast.makeText(context, msg, Toast.LENGTH_SHORT).show();
    }
}
