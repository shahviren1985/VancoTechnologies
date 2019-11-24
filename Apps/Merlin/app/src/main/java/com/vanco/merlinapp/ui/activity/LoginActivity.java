package com.vanco.merlinapp.ui.activity;

import android.content.Intent;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.design.widget.TextInputLayout;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.widget.AppCompatButton;
import android.support.v7.widget.AppCompatEditText;
import android.support.v7.widget.Toolbar;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;

import com.google.android.gms.analytics.HitBuilders;
import com.google.android.gms.analytics.Tracker;
import com.google.gson.Gson;
import com.vanco.merlinapp.MerlinApp;
import com.vanco.merlinapp.R;
import com.vanco.merlinapp.keyinterface.Constants;
import com.vanco.merlinapp.modal.ClsLoginResponse;
import com.vanco.merlinapp.modal.loginslidemenu.ClsAnonymousMenuListResponse;
import com.vanco.merlinapp.network.RetrofitInterface;
import com.vanco.merlinapp.ui.fragment.WebviewFragment;
import com.vanco.merlinapp.utility.Utility;

import java.util.HashMap;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class LoginActivity extends BaseActivity
        implements NavigationView.OnNavigationItemSelectedListener {

    @BindView(R.id.drawer_layout)
    DrawerLayout drawer;

    @BindView(R.id.toolbar)
    Toolbar toolbar;

    @BindView(R.id.nav_view)
    NavigationView navigationView;

    @BindView(R.id.edtMobileNumber)
    AppCompatEditText edtMobileNumber;

    @BindView(R.id.txtInputMobileNumber)
    TextInputLayout txtInputUsername;

    @BindView(R.id.txtInputLastName)
    TextInputLayout txtInputPassword;

    @BindView(R.id.edtLastName)
    AppCompatEditText edtLastName;

    @BindView(R.id.btnLogin)
    AppCompatButton btnLogin;

    HashMap<String, String> map;

    private ClsAnonymousMenuListResponse clsAnonymousMenuListResponse;
    private Tracker mTracker;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
        ButterKnife.bind(this);
        init();
    }

    @Override
    public void init() {
        super.init();

        MerlinApp application = (MerlinApp) getApplication();
        mTracker = application.getDefaultTracker();
        mTracker.setScreenName("Activity~ Login screen");
        mTracker.send(new HitBuilders.ScreenViewBuilder().build());

        map = new HashMap<>();

        setSupportActionBar(toolbar);

        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();


        navigationView.setNavigationItemSelectedListener(this);
        getMenuList();

        edtMobileNumber.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void afterTextChanged(Editable editable) {
                txtInputUsername.setError(null);
            }
        });

        edtLastName.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void afterTextChanged(Editable editable) {
                txtInputPassword.setError(null);
            }
        });


        TextView txtVersion = findViewById(R.id.txtVersion);
        try {
            PackageInfo pInfo = this.getPackageManager().getPackageInfo(getPackageName(), 0);
            String version = pInfo.versionName;
            txtVersion.setText("Version: " + version);
        } catch (PackageManager.NameNotFoundException e) {
            e.printStackTrace();
        }

    }


    @OnClick({R.id.btnLogin})
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.btnLogin:
                loginCall();
                break;
        }
    }

    private void loginCall() {

        if (!Utility.isInternetConnectionAvailable(this)) {
            Utility.openAlertDialog(this, getString(R.string.msg_internet_not_available));
            return;
        }

        if (Utility.isValueNull(edtMobileNumber)) {
            txtInputUsername.setError(getString(R.string.msg_enter_user_name));
            return;
        }

        if (Utility.isValueNull(edtLastName)) {
            txtInputPassword.setError(getString(R.string.msg_enter_password));
            return;
        }

        Utility.showProgress(getString(R.string.msg_logging), this);


        Call<String> call = RetrofitInterface.callToMethod().login(getQueryMap());
        call.enqueue(new Callback<String>() {
            @Override
            public void onResponse(Call<String> call, Response<String> response) {
                if (isDestroyed())
                    return;


                if (response != null && response.isSuccessful()) {
                    String res = response.body();
                    ClsLoginResponse clsLoginResponse = new Gson().fromJson(res, ClsLoginResponse.class);
                    if (clsLoginResponse != null && clsLoginResponse.getSuccess()) {
                        Utility.toast(LoginActivity.this, getString(R.string.msg_login_successfully));
                        Utility.setValue(Constants.IS_LOGIN, true);
                        Utility.setValue(Constants.LOGIN_DATA, res);
                        Utility.setValue(Constants.MOBILE_NUMBER, edtMobileNumber.getText().toString().trim());
                        Utility.setValue(Constants.LAST_NAME, edtLastName.getText().toString().trim());
                        openActivity();

                    } else {
                        if (clsLoginResponse == null) {
                            Utility.toast(LoginActivity.this, getString(R.string.msg_response_error));
                        } else {
                            Utility.toast(LoginActivity.this, getString(R.string.msg_valid_user_detail));
                        }
                    }
                } else if (response != null && response.code() == 401) {
                    Utility.toast(LoginActivity.this, getString(R.string.msg_valid_user_detail));
                } else {
                    Utility.toast(LoginActivity.this, getString(R.string.msg_response_error));
                }

                Utility.cancelProgress();
            }

            @Override
            public void onFailure(Call<String> call, Throwable t) {

                if (isDestroyed())
                    return;

                Utility.cancelProgress();
                RetrofitInterface.handleFailerError(LoginActivity.this, t);
            }
        });
    }

    public HashMap<String, Object> getQueryMap() {
        HashMap<String, Object> map = new HashMap<>();
        map.put("mobileNumber", edtMobileNumber.getText().toString().trim());
        map.put("lastName", edtLastName.getText().toString().trim());
        return map;
    }

    private void openActivity() {
        Intent intent = new Intent(LoginActivity.this, NavigationDrawerActivity.class);
        startActivity(intent);
        finish();
    }

    @Override
    public void onBackPressed() {
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            super.onBackPressed();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }

        return super.onOptionsItemSelected(item);
    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        Intent intent = new Intent(LoginActivity.this, FragmentContainerActivity.class);
        intent.putExtra("url", map.get(item.getTitle()));
        intent.putExtra("title", item.getTitle());
        intent.putExtra("param", WebviewFragment.class.getSimpleName());
        startActivity(intent);
        closeDrawer();
        return false;
    }

    public void getMenuList() {
        if (!Utility.isInternetConnectionAvailable(LoginActivity.this)) {
            Utility.openAlertDialog(LoginActivity.this, getString(R.string.msg_internet_not_available));
            return;
        }

        Call<String> call = RetrofitInterface.callToMethodSecondServer().getAnonymousMenuList(String.valueOf(101));
        call.enqueue(new Callback<String>() {
            @Override
            public void onResponse(Call<String> call, Response<String> response) {
                if (isDestroyed())
                    return;

                findViewById(R.id.progressbarDrawer).setVisibility(View.GONE);
                if (response != null && response.isSuccessful()) {
                    String res = response.body();
                    clsAnonymousMenuListResponse = new Gson().fromJson(res, ClsAnonymousMenuListResponse.class);
                    if (clsAnonymousMenuListResponse != null) {
                        addMenu();
                    }

                } else {
                    Utility.toast(LoginActivity.this, getString(R.string.msg_response_error));
                }

                Utility.cancelProgress();
            }

            @Override
            public void onFailure(Call<String> call, Throwable t) {

                if (isDestroyed())
                    return;
                findViewById(R.id.progressbarDrawer).setVisibility(View.GONE);
                RetrofitInterface.handleFailerError(LoginActivity.this, t);
            }
        });
    }

    private void addMenu() {
        if (clsAnonymousMenuListResponse == null)
            return;

        Menu menu = navigationView.getMenu();
        for (int i = 0; i < clsAnonymousMenuListResponse.getSideMenu().size(); i++) {
            MenuItem item = menu.add(clsAnonymousMenuListResponse.getSideMenu().get(i).getTitle());
            map.put(clsAnonymousMenuListResponse.getSideMenu().get(i).getTitle(), clsAnonymousMenuListResponse.getSideMenu().get(i).getLink());

            if (clsAnonymousMenuListResponse.getSideMenu().get(i).getTitle().equalsIgnoreCase("contact us")) {
                item.setIcon(R.drawable.ic_contact_us);
            } else if (clsAnonymousMenuListResponse.getSideMenu().get(i).getTitle().equalsIgnoreCase("website")) {
                item.setIcon(R.drawable.ic_website);
            } else if (clsAnonymousMenuListResponse.getSideMenu().get(i).getTitle().equalsIgnoreCase("trending topics")) {
                item.setIcon(R.drawable.ic_trending_topics);
            } else {
                item.setIcon(R.drawable.ic_website);
            }

        }
        navigationView.invalidate();
    }

    private void closeDrawer() {
        drawer.closeDrawer(GravityCompat.START);
    }
}
