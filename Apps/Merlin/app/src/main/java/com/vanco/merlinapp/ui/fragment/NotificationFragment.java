package com.vanco.merlinapp.ui.fragment;

import android.arch.lifecycle.Observer;
import android.arch.lifecycle.ViewModelProviders;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.vanco.merlinapp.R;
import com.vanco.merlinapp.adapter.NotificationAdapter;
import com.vanco.merlinapp.database.Notification;
import com.vanco.merlinapp.viewmodal.NotificationViewModal;

import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class NotificationFragment extends BaseFragment {

    @BindView(R.id.recyclerView)
    RecyclerView recyclerView;

    @BindView(R.id.fab)
    FloatingActionButton fab;

    private View view;
    private NotificationAdapter adapter;
    private NotificationViewModal viewModal;

    public static NotificationFragment newInstance() {

        Bundle args = new Bundle();
        NotificationFragment fragment = new NotificationFragment();
        fragment.setArguments(args);
        return fragment;
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (getArguments() != null) {

        }

    }

    @Override
    public void onActivityCreated(@Nullable Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);


    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        view = inflater.inflate(R.layout.fragment_notification, null);
        ButterKnife.bind(this, view);
        init();
        return view;
    }

    private void init() {


        adapter = new NotificationAdapter(null);
        recyclerView.setLayoutManager(new LinearLayoutManager(getActivity()));
        recyclerView.setAdapter(adapter);

        viewModal = ViewModelProviders.of(this).get(NotificationViewModal.class);
        viewModal.getNotifications().observe(this, new Observer<List<Notification>>() {
            @Override
            public void onChanged(@Nullable List<Notification> notificationList) {

                adapter.setNotificationList(notificationList);
                adapter.notifyDataSetChanged();
            }
        });

    }

    @OnClick({R.id.fab})
    public void onClick(View view) {
        addRandomData();
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
    }

    public void addRandomData() {

        Notification notification = new Notification();
        notification.setTitle("Record #" + (++Notification.count));
        notification.setDescription("Description of record count :" + Notification.count);

        viewModal.insertNotification(notification);
    }
}
