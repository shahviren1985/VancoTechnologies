package com.navigettr.app.Activity;

import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.design.widget.NavigationView;
import android.support.v4.app.FragmentManager;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.Gravity;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.FrameLayout;

import com.navigettr.app.Constant.PreferenceUtils;
import com.navigettr.app.Fragment.HomeFragment;
import com.navigettr.app.Fragment.RewardsFragment;
import com.navigettr.app.R;

public class MainActivity extends AppCompatActivity implements NavigationView.OnNavigationItemSelectedListener {

    private DrawerLayout drawerLayout;
    private Toolbar toolbar_home;
    private FrameLayout frame_content;
    private FragmentManager fragmentManager;
    private NavigationView navigationView;
    PreferenceUtils preferenceUtils;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        setRequestedOrientation (ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);

        fragmentManager = getSupportFragmentManager();
        drawerLayout = (DrawerLayout) findViewById(R.id.drawerLayout);
        frame_content = (FrameLayout) findViewById(R.id.frame_content);
        navigationView = (NavigationView)findViewById(R.id.navigationView);

        navigationView.setItemIconTintList(null);

        preferenceUtils = new PreferenceUtils(MainActivity.this);
        if (preferenceUtils.getIsLogin()){
            navigationView.getMenu().findItem(R.id.login).setVisible(false);
            navigationView.getMenu().findItem(R.id.register).setVisible(false);
        }
        else {
            navigationView.getMenu().findItem(R.id.rewards).setVisible(false);
            /*navigationView.getMenu().findItem(R.id.alerts).setVisible(false);
            navigationView.getMenu().findItem(R.id.notifications).setVisible(false);*/
            navigationView.getMenu().findItem(R.id.logout).setVisible(false);
        }
        setupToolbar();

        navigationView.setNavigationItemSelectedListener(this);

            HomeFragment homeFragment = new HomeFragment();
            fragmentManager.beginTransaction().add(R.id.frame_content, homeFragment).commit();
    }

    void setupToolbar() {
        toolbar_home = (Toolbar) findViewById(R.id.toolbar_home);
        setSupportActionBar(toolbar_home);
        Drawable drawable= getResources().getDrawable(R.drawable.navigettr_applogo);
        Bitmap bitmap = ((BitmapDrawable) drawable).getBitmap();
        Drawable newdrawable = new BitmapDrawable(getResources(), Bitmap.createScaledBitmap(bitmap, 70, 40, false));
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setHomeAsUpIndicator(newdrawable);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.toolbar_menu_home, menu);
        return true;
    }
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if (item != null && item.getItemId() == R.id.ic_navigationToggle) {
            if (drawerLayout.isDrawerOpen(Gravity.RIGHT)) {
                drawerLayout.closeDrawer(Gravity.RIGHT);
            }
            else {
                drawerLayout.openDrawer(Gravity.RIGHT);
            }
        }
        return false;
    }

    @Override
    public boolean onNavigationItemSelected(@NonNull MenuItem menuItem) {
        Intent i;
        int id = menuItem.getItemId();
        switch(id) {
            case R.id.home:
                for(int j = 0; j < fragmentManager.getBackStackEntryCount(); ++j) {
                    fragmentManager.popBackStack();
                }
                HomeFragment homeFragment = new HomeFragment();
                fragmentManager.beginTransaction().replace(R.id.frame_content, homeFragment).commit();
                drawerLayout.closeDrawer(Gravity.RIGHT);
                break;
            case R.id.login:
                i = new Intent(MainActivity.this, LoginActivity.class);
                startActivity(i);
                finish();
                drawerLayout.closeDrawer(Gravity.RIGHT);
                break;
            case R.id.register:
                i = new Intent(MainActivity.this, RegisterActivity.class);
                startActivity(i);
                finish();
                drawerLayout.closeDrawer(Gravity.RIGHT);
                break;
            case R.id.rewards:
                RewardsFragment rewardsFragment = new RewardsFragment();
                fragmentManager.beginTransaction().add(R.id.frame_content, rewardsFragment).addToBackStack(null).commit();
                drawerLayout.closeDrawer(Gravity.RIGHT);
                break;
            case R.id.logout:
                drawerLayout.closeDrawer(Gravity.RIGHT);
                preferenceUtils.setIsLogin(false);
                preferenceUtils.setLoginUserId(0);
                if (preferenceUtils.getIsLogin()){
                    navigationView.getMenu().findItem(R.id.login).setVisible(false);
                    navigationView.getMenu().findItem(R.id.register).setVisible(false);
                }
                else {
                    navigationView.getMenu().findItem(R.id.rewards).setVisible(false);
                    /*navigationView.getMenu().findItem(R.id.alerts).setVisible(false);
                    navigationView.getMenu().findItem(R.id.notifications).setVisible(false);*/
                    navigationView.getMenu().findItem(R.id.logout).setVisible(false);
                    navigationView.getMenu().findItem(R.id.login).setVisible(true);
                    navigationView.getMenu().findItem(R.id.register).setVisible(true);
                }
                break;
        }
        return false;
    }
}
