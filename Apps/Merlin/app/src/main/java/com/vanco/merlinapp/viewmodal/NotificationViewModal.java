package com.vanco.merlinapp.viewmodal;

import android.app.Application;
import android.arch.lifecycle.AndroidViewModel;
import android.arch.lifecycle.LiveData;
import android.support.annotation.NonNull;

import com.vanco.merlinapp.database.Notification;
import com.vanco.merlinapp.database.Repository;

import java.util.List;

public class NotificationViewModal extends AndroidViewModel {
    Repository repository;
    private LiveData<List<Notification>> notifications;

    public NotificationViewModal(@NonNull Application application) {
        super(application);
        repository=new Repository(application);
        notifications=repository.getNotificationList();
    }

    public LiveData<List<Notification>> getNotifications() {
        return notifications;
    }

    public void setNotifications(LiveData<List<Notification>> notifications) {
        this.notifications = notifications;
    }

    public void insertNotification(Notification notification){
        repository.insert(notification);
    }
}
