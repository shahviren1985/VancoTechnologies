package com.vanco.merlinapp.ui.activity;

import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.support.annotation.Nullable;

import com.google.gson.Gson;
import com.vanco.merlinapp.R;
import com.vanco.merlinapp.keyinterface.Constants;
import com.vanco.merlinapp.modal.ClsLoginResponse;
import com.vanco.merlinapp.network.RetrofitInterface;
import com.vanco.merlinapp.utility.Utility;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SplashActivity extends BaseActivity {

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_splash);
        new Handler().postDelayed(new Runnable() {
            @Override
            public void run() {
                if(isDestroyed())
                    return;

                Intent intent;
                if (Utility.getBooleanValue(Constants.IS_LOGIN)) {
                    intent = new Intent(SplashActivity.this, NavigationDrawerActivity.class);
                } else {
                    intent = new Intent(SplashActivity.this, LoginActivity.class);
                }
                startActivity(intent);
                finish();
            }
        }, 2000);
    }

}
