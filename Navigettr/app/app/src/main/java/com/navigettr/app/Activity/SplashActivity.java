package com.navigettr.app.Activity;

import android.Manifest;
import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.content.pm.PackageManager;
import android.location.Location;
import android.location.LocationManager;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.provider.Settings;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.Toast;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.location.LocationListener;
import com.google.android.gms.location.LocationRequest;
import com.google.android.gms.location.LocationServices;
import com.google.gson.Gson;
import com.navigettr.app.Constant.PreferenceUtils;
import com.navigettr.app.Model.CurrencyModel;
import com.navigettr.app.R;
import com.navigettr.app.WebServices.ApiClient;
import com.navigettr.app.WebServices.ApiInterface;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SplashActivity extends AppCompatActivity implements LocationListener,GoogleApiClient.ConnectionCallbacks, GoogleApiClient.OnConnectionFailedListener{

    private static final int REQUEST_PERMISSION = 100;
    private Animation animBounce;
    private ImageView ic_location;
    private ApiInterface apiInterface;
    private List<CurrencyModel.currency> currency_List = new ArrayList<>();
    private CurrencyModel currencyModel;
    private LocationManager locationManager ;
    private boolean GpsStatus ;
    private GoogleApiClient mGoogleApiClient;
    private LocationRequest mLocationRequest;
    private PreferenceUtils preferenceUtils;
    private AlertDialog.Builder builder;
    private LayoutInflater layoutInflater;
    private View view;
    private  AlertDialog alertDialog;
    Handler handler;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_splash);

        setRequestedOrientation (ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
        preferenceUtils = new PreferenceUtils(SplashActivity.this);
        handler = new Handler();
        CheckGpsStatus();

        ic_location = findViewById(R.id.ic_location);
        animBounce = AnimationUtils.loadAnimation(getApplicationContext(),
                R.anim.bounce);

        builder = new AlertDialog.Builder(SplashActivity.this);
        layoutInflater = LayoutInflater.from(SplashActivity.this);
        view = layoutInflater.inflate(R.layout.alertdialog, null);
        builder.setView(view);
        alertDialog = builder.create();
        alertDialog.setCancelable(false);

        if (isConnected()) {
            requestPermission();
            new Handler().postDelayed(new Runnable(){
                @Override
                public void run() {
                    ic_location.startAnimation(animBounce);
                }
            }, 1000);

        }
        else
            Toast.makeText(SplashActivity.this,"No Internet Connection",Toast.LENGTH_SHORT).show();

    }

    //    .............................PERMISSION......................

    private void requestPermission() {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M)
        {
            requestPermissions(new String[]{Manifest.permission.ACCESS_FINE_LOCATION, Manifest.permission.ACCESS_NETWORK_STATE,
                            Manifest.permission.ACCESS_COARSE_LOCATION, Manifest.permission.CAMERA},
                    REQUEST_PERMISSION);
        }
        else
        {
            if(!GpsStatus) {
                Alertdialog();
            }
            else
                buildGoogleApiClient();
        }
    }
    @Override public void onRequestPermissionsResult(int requestCode, String[] permissions, int[] grantResults)
    {
        if (requestCode == REQUEST_PERMISSION && grantResults[0] == PackageManager.PERMISSION_GRANTED)
        {
            if(!GpsStatus) {
                Alertdialog();
            }
            else
                buildGoogleApiClient();

        }
        else
        {
            Toast.makeText(this, "You must accept permission.", Toast.LENGTH_SHORT).show();
        }
    }
    private void splashHandler(){
        handler.postDelayed(new Runnable(){
            @Override
            public void run() {
                    Intent i = new Intent(SplashActivity.this,MainActivity.class);
                    startActivity(i);
                    finish();
            }
        }, 2000);
    }

    public boolean isConnected() {
        boolean connected = false;
        try {
            ConnectivityManager cm = (ConnectivityManager)getApplicationContext().getSystemService(Context.CONNECTIVITY_SERVICE);
            NetworkInfo nInfo = cm.getActiveNetworkInfo();
            connected = nInfo != null && nInfo.isAvailable() && nInfo.isConnected();
            return connected;
        } catch (Exception e) {
            Log.e("Connectivity Exception", e.getMessage());
        }
        return connected;
    }

    public void getCurrency() {
        try {
            apiInterface = ApiClient.getClient().create(ApiInterface.class);
            Call<CurrencyModel> call = apiInterface.getCurrency();
            call.enqueue(new Callback<CurrencyModel>() {

                @Override
                public void onResponse(Call<CurrencyModel> call, Response<CurrencyModel> response) {

                    if (response.body() != null) {
                        currencyModel = response.body();
                        currency_List = currencyModel.getCurrency();

                        Gson gson = new Gson();
                        String json_currency = gson.toJson(currency_List);
                        preferenceUtils.setCurencyList(json_currency);
                        splashHandler();
                    }
                }

                @Override
                public void onFailure(Call<CurrencyModel> call, Throwable t) {
                }
            });

        } catch (Exception e) {
            e.printStackTrace();
        }
    }
    public void CheckGpsStatus(){
        locationManager = (LocationManager)getSystemService(Context.LOCATION_SERVICE);
        GpsStatus = locationManager.isProviderEnabled(LocationManager.NETWORK_PROVIDER);
    }

    public void Alertdialog()
    {
        alertDialog.show();
        final Button btnok=(Button)view.findViewById(R.id.btn_ok);
        btnok.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                alertDialog.cancel();
                Intent intent1 = new Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS);
                startActivity(intent1);
            }
        });
    }

    protected synchronized void buildGoogleApiClient() {

        mGoogleApiClient = new GoogleApiClient.Builder(SplashActivity.this)
                .addConnectionCallbacks(this)
                .addOnConnectionFailedListener(this)
                .addApi(LocationServices.API)
                .build();
        mGoogleApiClient.connect();
    }

    @Override
    public void onLocationChanged(Location location) {
        if (location == null)
            buildGoogleApiClient();
        else {
            preferenceUtils.setLat(String.valueOf(location.getLatitude()));
            preferenceUtils.setLng(String.valueOf(location.getLongitude()));
            getCurrency();
        }
    }

    @SuppressWarnings("deprecation")
    @SuppressLint("RestrictedApi")
    @Override
    public void onConnected(@Nullable Bundle bundle) {
        mLocationRequest = new LocationRequest();
        mLocationRequest.setPriority(LocationRequest.PRIORITY_HIGH_ACCURACY);
        if (ContextCompat.checkSelfPermission(SplashActivity.this, Manifest.permission.ACCESS_FINE_LOCATION) == PackageManager.PERMISSION_GRANTED) {
            LocationServices.FusedLocationApi.requestLocationUpdates(mGoogleApiClient, mLocationRequest, this);

        }
    }

    @Override
    public void onConnectionSuspended(int i) {

    }

    @Override
    public void onConnectionFailed(@NonNull ConnectionResult connectionResult) {

    }

    @Override
    protected void onRestart() {
        super.onRestart();
        CheckGpsStatus();
        if (GpsStatus) {
            new Handler().postDelayed(new Runnable() {
                @Override
                public void run() {
                    ic_location.startAnimation(animBounce);
                }
            }, 1000);
            buildGoogleApiClient();
        }
        else
            Alertdialog();
    }

    @Override
    protected void onPause() {
        super.onPause();
        if (mGoogleApiClient != null)
            mGoogleApiClient.disconnect();
        if (handler != null)
            handler.removeCallbacksAndMessages(null);
    }

    @Override
    protected void onStop() {
        super.onStop();
        if (mGoogleApiClient != null)
            mGoogleApiClient.disconnect();
        if (handler != null)
            handler.removeCallbacksAndMessages(null);
    }
}
