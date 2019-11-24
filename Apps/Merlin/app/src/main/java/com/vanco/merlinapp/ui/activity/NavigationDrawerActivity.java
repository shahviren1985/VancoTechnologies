package com.vanco.merlinapp.ui.activity;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentFilter;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.content.ContextCompat;
import android.support.v4.content.LocalBroadcastManager;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AlertDialog;
import android.support.v7.widget.Toolbar;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;

import com.google.android.gms.analytics.HitBuilders;
import com.google.android.gms.analytics.Tracker;
import com.google.gson.Gson;
import com.vanco.merlinapp.MerlinApp;
import com.vanco.merlinapp.R;
import com.vanco.merlinapp.keyinterface.Constants;
import com.vanco.merlinapp.modal.ClsLoginResponse;
import com.vanco.merlinapp.modal.securemenulist.ClsSecureMenuListResponse;
import com.vanco.merlinapp.network.RetrofitInterface;
import com.vanco.merlinapp.notification.Config;
import com.vanco.merlinapp.ui.fragment.ExitFormFragment;
import com.vanco.merlinapp.ui.fragment.TeacherListFragment;
import com.vanco.merlinapp.ui.fragment.WebviewFragment;
import com.vanco.merlinapp.utility.GlideApp;
import com.vanco.merlinapp.utility.Utility;

import java.util.HashMap;

import butterknife.BindView;
import okhttp3.internal.Util;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class NavigationDrawerActivity extends BaseActivity
        implements NavigationView.OnNavigationItemSelectedListener {

    @BindView(R.id.drawer_layout)
    DrawerLayout drawer;

    @BindView(R.id.toolbar)
    Toolbar toolbar;

    @BindView(R.id.nav_view)
    NavigationView navigationView;
    private String currentFragment;
    private ClsSecureMenuListResponse clsSecureMenuList;
    private HashMap<String, String> map;
    private int initCount, lastCount;
    private Tracker mTracker;
    private BroadcastReceiver broadcastReceiver;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_navigation_drawer);
        init();
        setDeviceToken();
    }

    private void setDeviceToken() {
        if (Utility.isValueNull(Utility.getStringValue(Constants.DEVICE_TOKEN))) {
            broadcastReceiver = new BroadcastReceiver() {
                @Override
                public void onReceive(Context context, Intent intent) {

                }
            };
        } else if (!Utility.getBooleanValue(Constants.IS_TOKEN_SET)) {
            Utility.setNotificationDeviceId();
        }

    }

    @Override
    public void init() {
        super.init();

        Utility.setNotificationDeviceId();

        MerlinApp application = (MerlinApp) getApplication();
        mTracker = application.getDefaultTracker();

        mTracker.setScreenName(getString(R.string.track_teacher_list));
        mTracker.send(new HitBuilders.ScreenViewBuilder().build());

        setSupportActionBar(toolbar);

        map = new HashMap<>();
        getMenuList();



        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, toolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.addDrawerListener(toggle);
        toggle.syncState();

        navigationView.setNavigationItemSelectedListener(this);
        setExitFormInMenu();

        openTeacherList();
        setHeader();
    }


    public void setMenu() {
        String res = Utility.getStringValue(SECURE_DRAWER);
        clsSecureMenuList = new Gson().fromJson(res, ClsSecureMenuListResponse.class);
        addMenu();
    }

    private void setExitFormInMenu() {

        if (Utility.getLoginResponse() == null || Utility.getLoginResponse().getIsFinalYearStudent() == null)
            return;

        if (Utility.getLoginResponse().getIsFinalYearStudent().equalsIgnoreCase("yes") || Utility.getLoginResponse().getIsFinalYearStudent().equalsIgnoreCase("true")) {
            Menu menu = navigationView.getMenu();
            MenuItem item = menu.add(getString(R.string.exit_form));
            item.setIcon(R.drawable.ic_exit_forms);
        }
    }

    private void setHeader() {

        ClsLoginResponse clsLoginResponse = Utility.getLoginResponse();

        if (clsLoginResponse == null)
            return;

        View navHeaderView = navigationView.getHeaderView(0);
        TextView txtCollageName = navHeaderView.findViewById(R.id.txtCollageName);
        txtCollageName.setText(clsLoginResponse.getCollegeCode());

        TextView txtVersion = findViewById(R.id.txtVersion);
        try {
            PackageInfo pInfo = this.getPackageManager().getPackageInfo(getPackageName(), 0);
            String version = pInfo.versionName;
            txtVersion.setText("Version: " + version);
        } catch (PackageManager.NameNotFoundException e) {
            e.printStackTrace();
        }

        TextView txtUserName = navHeaderView.findViewById(R.id.txtUserName);
        txtUserName.setText(clsLoginResponse.getFirstName() + " " + clsLoginResponse.getLastName());

        TextView txtCourse = navHeaderView.findViewById(R.id.txtCourse);
        txtCourse.setText(clsLoginResponse.getCourse() + " (" + clsLoginResponse.getSubCourse() + ")");


    }


    private void addFragment(Fragment fragment, String tag, boolean isAddBackStack) {

        currentFragment = tag;
        FragmentTransaction fragmentTransaction = getSupportFragmentManager().beginTransaction();
        fragmentTransaction.replace(R.id.frmContainer, fragment, tag);

        if (isAddBackStack)
            fragmentTransaction.addToBackStack(null);

        fragmentTransaction.commit();
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
    protected void onResume() {
        super.onResume();
        if (broadcastReceiver != null)
            LocalBroadcastManager.getInstance(this).registerReceiver(broadcastReceiver,
                    new IntentFilter(Config.REGISTRATION_COMPLETE));

    }

    @Override
    protected void onPause() {
        if (broadcastReceiver != null)
            LocalBroadcastManager.getInstance(this).unregisterReceiver(broadcastReceiver);
        super.onPause();
    }

    public void openTeacherList() {
        Fragment fragment = TeacherListFragment.newInstance();
        addFragment(fragment, TeacherListFragment.class.getSimpleName(), false);
        getSupportActionBar().setTitle(R.string.teacher_list);

    }

    @SuppressWarnings("StatementWithEmptyBody")
    @Override
    public boolean onNavigationItemSelected(MenuItem item) {
        // Handle navigation view item clicks here.
        int id = item.getItemId();

       /* if (id == R.id.nav_notification) {
            Intent intent = new Intent(NavigationDrawerActivity.this, FragmentContainerActivity.class);
            intent.putExtra("param", NotificationFragment.class.getSimpleName());
            startActivity(intent);
            drawer.closeDrawer(GravityCompat.START);
            return false;

        } else */
        if (id == R.id.nav_teacher_feedback) {
            if (!currentFragment.equals(TeacherListFragment.class.getSimpleName())) {
                Fragment fragment = TeacherListFragment.newInstance();
                addFragment(fragment, TeacherListFragment.class.getSimpleName(), false);
                getSupportActionBar().setTitle(R.string.teacher_list);
            }
        } else if (item.getTitle().equals(getString(R.string.exit_form))) {
            if (Utility.getLoginResponse().getIsExistFormSubmitted().equalsIgnoreCase("true")) {
                Utility.toast(NavigationDrawerActivity.this, getString(R.string.form_already_submited));
                drawer.closeDrawer(GravityCompat.START);
                return false;
            }


            Intent intent = new Intent(NavigationDrawerActivity.this, FragmentContainerActivity.class);
            intent.putExtra("param", ExitFormFragment.class.getSimpleName());
            startActivity(intent);
            drawer.closeDrawer(GravityCompat.START);
            return false;

        } else if (item.getTitle().equals(getString(R.string.logout))) {
            logout();
            drawer.closeDrawer(GravityCompat.START);
            return false;
        } else {
            Intent intent = new Intent(NavigationDrawerActivity.this, FragmentContainerActivity.class);
            intent.putExtra("url", map.get(item.getTitle()));
            intent.putExtra("title", item.getTitle());
            intent.putExtra("param", WebviewFragment.class.getSimpleName());
            startActivity(intent);
            drawer.closeDrawer(GravityCompat.START);
            return false;
        }

        drawer.closeDrawer(GravityCompat.START);
        return true;
    }

    private void logout() {
        final AlertDialog.Builder dialog = new AlertDialog.Builder(this);
        dialog.setTitle(R.string.logout);
        dialog.setMessage(R.string.msg_logut);
        dialog.setPositiveButton(R.string.label_yes, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialogInterface, int i) {
                Utility.setValue(Constants.LOGIN_DATA, "");
                Utility.setValue(Constants.IS_LOGIN, false);
                Intent intent = new Intent(NavigationDrawerActivity.this, LoginActivity.class);
                startActivity(intent);
                finish();
            }
        });

        dialog.setNegativeButton(R.string.label_no, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialogInterface, int i) {
            }
        });
        dialog.show();
    }


    public void getMenuList() {
        if (!Utility.isInternetConnectionAvailable(NavigationDrawerActivity.this)) {
            Utility.openAlertDialog(NavigationDrawerActivity.this, getString(R.string.msg_internet_not_available));
            return;
        }

        Call<String> call = RetrofitInterface.callToMethodSecondServer().getSecureMenuList(String.valueOf(101));
        call.enqueue(new Callback<String>() {
            @Override
            public void onResponse(Call<String> call, Response<String> response) {
                if (isDestroyed())
                    return;

                findViewById(R.id.progressbarDrawer).setVisibility(View.GONE);
                if (response != null && response.isSuccessful()) {
                    String res = response.body();
                    clsSecureMenuList = new Gson().fromJson(res, ClsSecureMenuListResponse.class);
                    if (clsSecureMenuList != null) {
                        Utility.setValue(SECURE_DRAWER, res);
                        addMenu();
                    }

                } else {
                    Utility.toast(NavigationDrawerActivity.this, getString(R.string.msg_response_error));
                }

            }

            @Override
            public void onFailure(Call<String> call, Throwable t) {

                if (isDestroyed())
                    return;

                findViewById(R.id.progressbarDrawer).setVisibility(View.GONE);
                RetrofitInterface.handleFailerError(NavigationDrawerActivity.this, t);
            }
        });
    }

    private void addMenu() {
        if (clsSecureMenuList == null)
            return;

        Menu menu = navigationView.getMenu();
        for (int i = 0; i < clsSecureMenuList.getSideMenu().size(); i++) {
            MenuItem item = menu.add(clsSecureMenuList.getSideMenu().get(i).getTitle());
            map.put(clsSecureMenuList.getSideMenu().get(i).getTitle(), clsSecureMenuList.getSideMenu().get(i).getLink());

            if (clsSecureMenuList.getSideMenu().get(i).getTitle().equalsIgnoreCase("contact us")) {
                item.setIcon(R.drawable.ic_contact_us);
            } else if (clsSecureMenuList.getSideMenu().get(i).getTitle().equalsIgnoreCase("website")) {
                item.setIcon(R.drawable.ic_website);
            } else if (clsSecureMenuList.getSideMenu().get(i).getTitle().equalsIgnoreCase("trending topics")) {
                item.setIcon(R.drawable.ic_trending_topics);
            } else if (clsSecureMenuList.getSideMenu().get(i).getTitle().equalsIgnoreCase("about us")) {
                item.setIcon(R.drawable.ic_about_us);
            } else {
                item.setIcon(R.drawable.ic_website);
            }

        }
        MenuItem item = menu.add(getString(R.string.logout));
        item.setIcon(R.drawable.ic_logout);

        View navHeaderView = navigationView.getHeaderView(0);
        TextView txtCollageName = navHeaderView.findViewById(R.id.txtCollageName);
        txtCollageName.setText(clsSecureMenuList.getCollegeName());

        ImageView imageView = navigationView.findViewById(R.id.imageView);
        GlideApp.with(navigationView.getContext()).load(clsSecureMenuList.getLogo())
                .dontAnimate()
                .placeholder(ContextCompat.getDrawable(imageView.getContext(), R.mipmap.ic_launcher))
                .into(imageView);

        navigationView.invalidate();
    }

}
