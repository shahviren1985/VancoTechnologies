package com.vanco.merlinapp;

import android.app.Application;
import android.content.Context;

import com.crashlytics.android.Crashlytics;
import com.google.android.gms.analytics.GoogleAnalytics;
import com.google.android.gms.analytics.Tracker;
import com.vanco.merlinapp.utility.PreferenceData;

import io.fabric.sdk.android.Fabric;

public class MerlinApp extends Application {
    public static Context CONTEXT;
    private static GoogleAnalytics sAnalytics;
    private static Tracker sTracker;

 public  static PreferenceData preferenceData;
    @Override
    public void onCreate() {
        super.onCreate();
        CONTEXT = this;
        preferenceData=new PreferenceData(this);
        sAnalytics = GoogleAnalytics.getInstance(this);
        Fabric.with(this, new Crashlytics());
    }

    /**
     * Gets the default {@link Tracker} for this {@link Application}.
     * @return tracker
     */
    synchronized public Tracker getDefaultTracker() {
        // To enable debug logging use: adb shell setprop log.tag.GAv4 DEBUG
        if (sTracker == null) {
            sTracker = sAnalytics.newTracker(R.xml.global_tracker);
        }

        return sTracker;
    }

}
