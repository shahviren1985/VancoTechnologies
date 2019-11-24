package com.navigettr.app.Activity;

import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

import com.navigettr.app.Constant.Constant;
import com.navigettr.app.Constant.PreferenceUtils;
import com.navigettr.app.Model.LoginModel;
import com.navigettr.app.ProgressDialogLoader;
import com.navigettr.app.R;
import com.navigettr.app.WebServices.ApiClient;
import com.navigettr.app.WebServices.ApiInterface;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class LoginActivity extends AppCompatActivity implements View.OnClickListener {

    private TextView tv_login, tv_forgrtPsw, tv_register_here;
    private EditText et_username, et_password;
    private ApiInterface apiInterface;
    private List<LoginModel> login_List = new ArrayList<>();
    private PreferenceUtils preferenceUtils;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        setRequestedOrientation (ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);

        preferenceUtils = new PreferenceUtils(LoginActivity.this);

        tv_login = findViewById(R.id.tv_login);
        et_username = findViewById(R.id.et_username);
        et_password = findViewById(R.id.et_password);
        tv_forgrtPsw = findViewById(R.id.tv_forgrtPsw);
        tv_register_here = findViewById(R.id.tv_register_here);
        tv_login.setOnClickListener(this);
        tv_forgrtPsw.setOnClickListener(this);
        tv_register_here.setOnClickListener(this);
    }

    @Override
    public void onClick(View v) {
        Intent i;
        switch (v.getId()){
            case R.id.tv_login:
                if (et_username.getText().toString().isEmpty())
                    et_username.setError(getResources().getString(R.string.error_un));
                else if (et_password.getText().toString().isEmpty())
                    et_password.setError(getResources().getString(R.string.error_pwd));
                else
                    getLogin();
                break;
            case R.id.tv_forgrtPsw:
                i = new Intent(LoginActivity.this, ForgotpasswordActivity.class);
                startActivity(i);
                finish();
                break;
            case R.id.tv_register_here:
                i = new Intent(LoginActivity.this, RegisterActivity.class);
                startActivity(i);
                finish();
                break;
        }
    }

    public void getLogin() {

        try {
            apiInterface = ApiClient.getClient().create(ApiInterface.class);
            ProgressDialogLoader.progressdialog_creation(LoginActivity.this, "Loading...");
            Call<List<LoginModel>> call = apiInterface.getLogin(et_username.getText().toString(),et_password.getText().toString());
            call.enqueue(new Callback<List<LoginModel>>() {

                @Override
                public void onResponse(Call<List<LoginModel>> call, Response<List<LoginModel>> response) {

                    if (response.body() != null) {
                        int userId = 0;
                        login_List = response.body();
                        for (int i =0; i < login_List.size(); i++) {
                            userId = login_List.get(i).getUserId();
                        }
                        preferenceUtils.setLoginUserId(userId);
                        preferenceUtils.setIsLogin(true);
                            Intent i = new Intent(LoginActivity.this, MainActivity.class);
                            startActivity(i);
                            finish();
                    }
                    else
                        Constant.getToast(LoginActivity.this, getResources().getString(R.string.error_un_pwd));
                    ProgressDialogLoader.progressdialog_dismiss();
                }

                @Override
                public void onFailure(Call<List<LoginModel>> call, Throwable t) {
                    ProgressDialogLoader.progressdialog_dismiss();
                }
            });

        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
