package com.vanco.merlinapp.ui.activity;

import android.os.Bundle;
import android.support.design.widget.AppBarLayout;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.widget.FrameLayout;
import android.widget.TextView;

import com.google.android.gms.analytics.HitBuilders;
import com.google.android.gms.analytics.Tracker;
import com.vanco.merlinapp.MerlinApp;
import com.vanco.merlinapp.R;
import com.vanco.merlinapp.modal.ClsTeacher;
import com.vanco.merlinapp.ui.fragment.ExitFormFragment;
import com.vanco.merlinapp.ui.fragment.NotificationFragment;
import com.vanco.merlinapp.ui.fragment.TeacherFeedBackFragment;
import com.vanco.merlinapp.ui.fragment.WebviewFragment;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class FragmentContainerActivity extends BaseActivity {
    @BindView(R.id.toolbar)
    Toolbar toolbar;

    @BindView(R.id.txtTitle)
    TextView txtTitle;

    @BindView(R.id.txtSave)
    TextView txtSave;

    @BindView(R.id.appBar)
    AppBarLayout appBar;

    @BindView(R.id.frmContainer)
    FrameLayout frmContainer;

    private com.vanco.merlinapp.keyinterface.OnClick onClick;
    private Fragment fragment;
    private Tracker mTracker;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_fragment_container);
        ButterKnife.bind(this);

        MerlinApp application = (MerlinApp) getApplication();
        mTracker = application.getDefaultTracker();

        init();

    }

    public com.vanco.merlinapp.keyinterface.OnClick getOnClick() {
        return onClick;
    }

    public void setOnClick(com.vanco.merlinapp.keyinterface.OnClick onClick) {
        this.onClick = onClick;
    }

    @Override
    public void init() {
        super.init();

        setSupportActionBar(toolbar);

        toolbar.setNavigationIcon(R.drawable.ic_back); // Set the icon

        // Icon click listener
        toolbar.setNavigationOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                onBackPressed();
            }
        });

        setFragment();
    }

    @Override
    public void onBackPressed() {
        super.onBackPressed();

    }

    @Override
    public void setTitle(int titleId) {
        txtTitle.setText(titleId);
    }

    public void setTitle(String title) {
        txtTitle.setText(title);
    }

    private void setFragment() {
        String param = getIntent().getStringExtra("param");
        if (param == null || param.equals(WebviewFragment.class.getSimpleName())) {
            String title = getIntent().getStringExtra("title");

            mTracker.setScreenName("Fragment~ Open web page " + title);
            mTracker.send(new HitBuilders.ScreenViewBuilder().build());

            String url = getIntent().getStringExtra("url");
            fragment = WebviewFragment.newInstance(url);

            addFragment(fragment, WebviewFragment.class.getSimpleName(), false);
            setTitle(title);

        } else if (param.equals(TeacherFeedBackFragment.class.getSimpleName())) {
            mTracker.setScreenName("Fragment~ Teacher feedback screen ");
            mTracker.send(new HitBuilders.ScreenViewBuilder().build());


            ClsTeacher clsTeacher = (ClsTeacher) getIntent().getSerializableExtra("param1");
            fragment = TeacherFeedBackFragment.newInstance(clsTeacher);
            addFragment(fragment, WebviewFragment.class.getSimpleName(), false);
            setTitle(R.string.label_teacher_feed_back);
            txtSave.setVisibility(View.VISIBLE);

        } else if (param.equals(ExitFormFragment.class.getSimpleName())) {

            mTracker.setScreenName("Fragment~ Exit form screen ");
            mTracker.send(new HitBuilders.ScreenViewBuilder().build());


            fragment = ExitFormFragment.newInstance();
            addFragment(fragment, ExitFormFragment.class.getSimpleName(), false);
            setTitle(R.string.exit_form);
            txtSave.setVisibility(View.VISIBLE);
        } else if (param.equals(NotificationFragment.class.getSimpleName())) {
            mTracker.setScreenName("Fragment~ Notification screen ");
            mTracker.send(new HitBuilders.ScreenViewBuilder().build());

            fragment = NotificationFragment.newInstance();
            addFragment(fragment, NotificationFragment.class.getSimpleName(), false);
            setTitle(R.string.notification);
        }
    }

    @OnClick({R.id.txtSave})
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.txtSave:
                if (onClick != null) {
                    onClick.onClickView(view);
                }

                break;
        }

    }

    private void addFragment(Fragment fragment, String tag, boolean isAddBackStack) {

        FragmentTransaction fragmentTransaction = getSupportFragmentManager().beginTransaction();
        fragmentTransaction.replace(R.id.frmContainer, fragment, tag);

        if (isAddBackStack)
            fragmentTransaction.addToBackStack(null);

        fragmentTransaction.commit();
    }
}
