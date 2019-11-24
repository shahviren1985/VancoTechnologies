package com.vanco.merlinapp.ui.activity;

import android.support.v7.app.AppCompatActivity;

import com.vanco.merlinapp.keyinterface.Constants;

import butterknife.ButterKnife;


/**
 * Created by DELL on 01-02-2018.
 */

public class BaseActivity extends AppCompatActivity implements Constants {

    private boolean isBackPressedToExit;
    private boolean isDestory;

    public void init() {

        ButterKnife.bind(this);

    }



    @Override
    protected void onDestroy() {
        super.onDestroy();
        isDestory=true;
    }
}
