package com.navigettr.app.Activity;

import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.google.gson.JsonObject;
import com.navigettr.app.Constant.Constant;
import com.navigettr.app.Model.RegisterModel;
import com.navigettr.app.ProgressDialogLoader;
import com.navigettr.app.R;
import com.navigettr.app.WebServices.ApiClient;
import com.navigettr.app.WebServices.ApiInterface;
import com.rilixtech.CountryCodePicker;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class RegisterActivity extends AppCompatActivity implements View.OnClickListener {

    private Button btn_register;
    private JsonObject json_obj;
    private ApiInterface apiInterface;
    private EditText et_firstname, et_lastname, et_email, et_password_reg, et_phone;
    private CountryCodePicker ccp;
    private TextView tv_sign_up, tv_forgrtPsw;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);
        setRequestedOrientation (ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);

        json_obj = new JsonObject();
        et_firstname = findViewById(R.id.et_firstname);
        et_lastname = findViewById(R.id.et_lastname);
        et_email = findViewById(R.id.et_email);
        et_password_reg = findViewById(R.id.et_password_reg);
        et_phone = findViewById(R.id.et_phone);
        btn_register = findViewById(R.id.btn_register);
        ccp = findViewById(R.id.ccp);
        tv_sign_up = findViewById(R.id.tv_sign_up);
        tv_forgrtPsw = findViewById(R.id.tv_forgrtPsw);

        ccp.registerPhoneNumberTextView(et_phone);
        btn_register.setOnClickListener(this);
        tv_sign_up.setOnClickListener(this);
        tv_forgrtPsw.setOnClickListener(this);
    }

    @Override
    public void onClick(View v) {
        Intent i;
        switch (v.getId()) {
            case R.id.btn_register:
                if (et_firstname.getText().toString().isEmpty())
                    et_firstname.setError(getResources().getString(R.string.error_fn));
                else if (et_lastname.getText().toString().isEmpty())
                    et_lastname.setError(getResources().getString(R.string.error_ln));
                else if (et_email.getText().toString().isEmpty())
                    et_email.setError(getResources().getString(R.string.error_email));
                else if (et_password_reg.getText().toString().isEmpty())
                    et_password_reg.setError(getResources().getString(R.string.error_pwd));
                else if (et_phone.getText().toString().isEmpty())
                    et_phone.setError(getResources().getString(R.string.error_phone));
                else
                    getRegister();
                break;
            case R.id.tv_sign_up:
                i = new Intent(RegisterActivity.this, LoginActivity.class);
                startActivity(i);
                finish();
                break;
            case R.id.tv_forgrtPsw:
                i = new Intent(RegisterActivity.this, ForgotpasswordActivity.class);
                startActivity(i);
                finish();
                break;
        }
    }

    public void createRegisterJsonObject() {
        json_obj.addProperty("firstName", et_firstname.getText().toString());
        json_obj.addProperty("lastName", et_lastname.getText().toString());
        json_obj.addProperty("countryCode", ccp.getSelectedCountryCodeWithPlus());
        json_obj.addProperty("mobileNumber", et_phone.getText().toString());
        json_obj.addProperty("emailAddress", et_email.getText().toString());
        json_obj.addProperty("password", et_password_reg.getText().toString());
    }

    public void getRegister() {
        createRegisterJsonObject();
        try {
            apiInterface = ApiClient.getClient().create(ApiInterface.class);
            ProgressDialogLoader.progressdialog_creation(RegisterActivity.this, "Registering user...");
            Call<RegisterModel> call = apiInterface.getRegister(json_obj);
            call.enqueue(new Callback<RegisterModel>() {

                @Override
                public void onResponse(Call<RegisterModel> call, Response<RegisterModel> response) {

                    if (response.body() != null) {
                        RegisterModel registerModel = response.body();
                        if (registerModel.getMessage().equals(getResources().getString(R.string.err_register_api)))
                            Constant.getToast(RegisterActivity.this, getResources().getString(R.string.toast_register_api));
                        else {
                            Constant.getToast(RegisterActivity.this, getResources().getString(R.string.success_register_api));
                            Intent i = new Intent(RegisterActivity.this, MainActivity.class);
                            startActivity(i);
                            finish();
                        }

                    }
                    ProgressDialogLoader.progressdialog_dismiss();
                }

                @Override
                public void onFailure(Call<RegisterModel> call, Throwable t) {
                    ProgressDialogLoader.progressdialog_dismiss();
                }
            });

        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
