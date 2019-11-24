package com.vanco.merlinapp.database;

import android.arch.persistence.db.SupportSQLiteDatabase;
import android.arch.persistence.room.Database;
import android.arch.persistence.room.Room;
import android.arch.persistence.room.RoomDatabase;
import android.content.Context;
import android.os.AsyncTask;
import android.support.annotation.NonNull;

@Database(entities = {Notification.class}, version = 1, exportSchema = false)
public abstract class NotificationDatabase extends RoomDatabase {

    public abstract NotificationDao notificationDao();

    public static NotificationDatabase DATABASE_INSTANCE = null;

    static NotificationDatabase getDatabase(final Context context) {
        if (DATABASE_INSTANCE == null) {
            synchronized (NotificationDatabase.class) {
                if (DATABASE_INSTANCE == null) {
                    DATABASE_INSTANCE = Room.databaseBuilder(context.getApplicationContext(),
                            NotificationDatabase.class, "merlin_database")
                            .build();

                }
            }
        }
        return DATABASE_INSTANCE;
    }

    /**
     * Override the onOpen method to populate the database.
     * For this sample, we clear the database every time it is created or opened.
     */
    private static RoomDatabase.Callback sRoomDatabaseCallback = new RoomDatabase.Callback(){

        @Override
        public void onOpen (@NonNull SupportSQLiteDatabase db){
            super.onOpen(db);
            // If you want to keep the data through app restarts,
            // comment out the following line.
            new PopulateDbAsync(DATABASE_INSTANCE).execute();
        }
    };

    /**
     * Populate the database in the background.
     * If you want to start with more words, just add them.
     */
    private static class PopulateDbAsync extends AsyncTask<Void, Void, Void> {

        private final NotificationDao mDao;

        PopulateDbAsync(NotificationDatabase db) {
            mDao = db.notificationDao();
        }

        @Override
        protected Void doInBackground(final Void... params) {
            // Start the app with a clean database every time.
            // Not needed if you only populate on creation.

           Notification notification=new Notification();
           notification.setTitle("Test");
            mDao.insertNotification(notification);

            return null;
        }
    }

}
