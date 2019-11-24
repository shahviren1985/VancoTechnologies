package com.navigettr.app.Activity;

import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;

import com.navigettr.app.Model.ForgotPassowrdModel;
import com.navigettr.app.ProgressDialogLoader;
import com.navigettr.app.R;
import com.navigettr.app.WebServices.ApiClient;
import com.navigettr.app.WebServices.ApiInterface;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ForgotpasswordActivity extends AppCompatActivity implements View.OnClickListener {

    private Button btn_send;
    private EditText et_email;
    private ApiInterface apiInterface;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_forgotpassword);

        setRequestedOrientation (ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);

        btn_send = findViewById(R.id.btn_send);
        et_email = findViewById(R.id.et_email);
        btn_send.setOnClickListener(this);
    }

    public void getForgotPassword() {

        try {
            apiInterface = ApiClient.getClient().create(ApiInterface.class);
            ProgressDialogLoader.progressdialog_creation(ForgotpasswordActivity.this, "Loading...");
            Call<ForgotPassowrdModel> call = apiInterface.getForgotPassword(et_email.getText().toString());
            call.enqueue(new Callback<ForgotPassowrdModel>() {

                @Override
                public void onResponse(Call<ForgotPassowrdModel> call, Response<ForgotPassowrdModel> response) {

                    if (response.body() != null) {
                        final AlertDialog.Builder builder = new AlertDialog.Builder(ForgotpasswordActivity.this);
                        LayoutInflater layoutInflater = LayoutInflater.from(ForgotpasswordActivity.this);
                        View popupInputDialogView = layoutInflater.inflate(R.layout.alertdialog, null);
                        builder.setView(popupInputDialogView);
                        final AlertDialog alertDialog = builder.create();
                        ImageView ic_gps=(ImageView)popupInputDialogView.findViewById(R.id.ic_gps);
                        ic_gps.setVisibility(View.GONE);
                        TextView tv_msg=(TextView)popupInputDialogView.findViewById(R.id.tv_msg);
                        tv_msg.setText(response.body().getMessage());
                        alertDialog.show();
                        final Button btnok=(Button)popupInputDialogView.findViewById(R.id.btn_ok);
                        btnok.setOnClickListener(new View.OnClickListener() {
                            @Override
                            public void onClick(View view) {
                                alertDialog.cancel();
                                Intent i = new Intent(ForgotpasswordActivity.this, LoginActivity.class);
                                startActivity(i);
                                finish();
                            }
                        });
                    }
                    ProgressDialogLoader.progressdialog_dismiss();
                }

                @Override
                public void onFailure(Call<ForgotPassowrdModel> call, Throwable t) {
                    ProgressDialogLoader.progressdialog_dismiss();
                }
            });

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onClick(View v) {
        if (et_email.getText().toString().isEmpty())
            et_email.setError(getResources().getString(R.string.error_email));
        else
            getForgotPassword();
    }
}
