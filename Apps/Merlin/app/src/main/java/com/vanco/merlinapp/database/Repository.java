package com.vanco.merlinapp.database;

import android.app.Application;
import android.arch.lifecycle.LiveData;

import java.util.List;

import io.fabric.sdk.android.services.concurrency.AsyncTask;

public class Repository {
    private NotificationDao notificationDao;
    private LiveData<List<Notification>> notificationList;
    private Application application;

    public  Repository(Application application) {
        NotificationDatabase db = NotificationDatabase.getDatabase(application);
        notificationDao = db.notificationDao();
        notificationList = notificationDao.getNotification();
    }

    public  LiveData<List<Notification>> getNotificationList() {
        return notificationList;
    }

    public void insert(Notification notification) {
        new InsertAsyncTask(notificationDao).execute(notification);
    }

    private static class InsertAsyncTask extends AsyncTask<Notification, Void, Void> {
        NotificationDao notificationDao;

        public InsertAsyncTask(NotificationDao notificationDao) {
            this.notificationDao = notificationDao;
        }

        @Override
        protected Void doInBackground(Notification... notifications) {
            if (notifications == null)
                return null;

            notificationDao.insertNotification(notifications[0]);

            return null;
        }
    }
}
