package com.vanco.merlinapp.utility;

import android.content.Context;
import android.content.SharedPreferences;
import android.preference.PreferenceManager;

public class PreferenceData {
    private SharedPreferences preferences;
    private SharedPreferences.Editor editor;


    public PreferenceData(Context context) {
        preferences = PreferenceManager.getDefaultSharedPreferences(context);
    }

    public String getValueFromKey(String key) {
        return preferences.getString(key, "");
    }

    public int getIntValueFromKey(String key) {
        return preferences.getInt(key, -1);
    }

    public long getLongValue(String key) {
        return preferences.getLong(key, 0);
    }

    public boolean getBooleanValueFromKey(String key) {
        return preferences.getBoolean(key, false);
    }

    public boolean getBooleanValueFromKey(String key, boolean defaultValue) {
        return preferences.getBoolean(key, defaultValue);
    }

    public void setValue(String key, String value) {
        editor = preferences.edit();
        editor.putString(key, value);
        editor.commit();
    }

    public void setIntValue(String key, int value) {
        editor = preferences.edit();
        editor.putInt(key, value);
        editor.commit();
    }

    public void setBooleanValue(String key, boolean value) {
        editor = preferences.edit();
        editor.putBoolean(key, value);
        editor.commit();
    }

    public void setLongValue(String key, long value) {
        editor = preferences.edit();
        editor.putLong(key, value);
        editor.commit();
    }

    public void clearData() {
        editor = preferences.edit();
        editor.clear();
        editor.commit();
    }
}