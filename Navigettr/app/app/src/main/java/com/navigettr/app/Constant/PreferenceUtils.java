package com.navigettr.app.Constant;

import android.content.Context;
import android.content.SharedPreferences;


public class PreferenceUtils {

    private final String LOG_TAG = PreferenceUtils.class.getSimpleName();

    protected Context mContext;

    protected SharedPreferences mSettings, mPrefLatLng, mPrefPopup;
    protected SharedPreferences.Editor mEditor, mEditorLatLng, mEditorPopup;


    public PreferenceUtils(Context context) {
        mContext = context;

        mSettings = mContext.getSharedPreferences("Login",
                Context.MODE_PRIVATE);
        mEditor = mSettings.edit();

        mPrefLatLng = mContext.getSharedPreferences("LatLng",
                Context.MODE_PRIVATE);
        mEditorLatLng = mPrefLatLng.edit();

        mPrefPopup = mContext.getSharedPreferences("Popup",
                Context.MODE_PRIVATE);
        mEditorPopup = mPrefPopup.edit();
    }

    public void clearEditor() {
        mEditorLatLng.clear().commit();
    }

    public void clearEditorPopup() {
        mEditorPopup.clear().commit();
    }

    public void removeValue(String key) {
        if (mEditor != null) {
            mEditor.remove(key).commit();
        }
    }

    /*------------    Destination Reached PopUp   -----------------------------------------*/
    public void setIsPopup(boolean isPopup) {

        mEditorPopup.putBoolean("isPopup", isPopup);
        mEditorPopup.commit();
    }
    public boolean getIsPopup() {
        return mPrefPopup.getBoolean("isPopup", false);
    }

    /*------------    Login   -----------------------------------------*/
    public void setIsLogin(boolean isLogin) {

        mEditor.putBoolean("isLogin", isLogin);
        mEditor.commit();
    }
    public boolean getIsLogin() {
        return mSettings.getBoolean("isLogin", false);
    }


    /*------------    Currency List  -----------------------------------------*/
    public void setCurencyList(String json_currency) {

        mEditor.putString("currency", json_currency);
        mEditor.commit();
    }
    public String getCurencyList() {
        return mSettings.getString("currency", "");
    }

    /*------------    Searched Loction  -----------------------------------------*/
    public void setSearchedLocation(String searchedLocation) {

        mEditor.putString("searchedLocation", searchedLocation);
        mEditor.commit();
    }
    public String getSearchedLocation() {
        return mSettings.getString("searchedLocation", "");
    }

    /*------------    Currency  -----------------------------------------*/
    public void setFromCurrency(String fromCurrency) {

        mEditor.putString("fromCurrency", fromCurrency);
        mEditor.commit();
    }
    public String getFromCurrency() {
        return mSettings.getString("fromCurrency", "");
    }

    public void setToCurrency(String toCurrency) {

        mEditor.putString("toCurrency", toCurrency);
        mEditor.commit();
    }
    public String getToCurrency() {
        return mSettings.getString("toCurrency", "");
    }

    /*------------    Amount --------------------------------------*/

    public void setamount(int amount) {

        mEditor.putInt("amount", amount);
        mEditor.commit();
    }
    public int getamount() {
        return mSettings.getInt("amount", 0);
    }

    /*------------    Partner Id --------------------------------------*/

    public void setPartnerId(int partnerId) {

        mEditor.putInt("partnerId", partnerId);
        mEditor.commit();
    }
    public int getPartnerId() {
        return mSettings.getInt("partnerId", 0);
    }

    /*------------    Location Id --------------------------------------*/

    public void setLocationId(int locationId) {

        mEditor.putInt("locationId", locationId);
        mEditor.commit();
    }
    public int getLocationId() {
        return mSettings.getInt("locationId", 0);
    }

    /*------------    Login User Id  --------------------------------------*/

    public void setLoginUserId(int UserId) {

        mEditor.putInt("UserId", UserId);
        mEditor.commit();
    }
    public int getLoginUserId() {
        return mSettings.getInt("UserId", 0);
    }

    /*------------    Total Providers   -----------------------------------------*/
    public void setTotalProviders(int total) {

        mEditor.putInt("total", total);
        mEditor.commit();
    }
    public int getTotalProviders() {
        return mSettings.getInt("total", 0);
    }

    /*------------    Location Data   -----------------------------------------*/
    public void setCity(String city) {

        mEditor.putString("city", city);
        mEditor.commit();
    }
    public String getCity() {
        return mSettings.getString("city", "");
    }

    public void setState(String state) {

        mEditor.putString("state", state);
        mEditor.commit();
    }
    public String getState() {
        return mSettings.getString("state", "");
    }

    public void setCountry(String country) {

        mEditor.putString("country", country);
        mEditor.commit();
    }
    public String getCountry() {
        return mSettings.getString("country", "");
    }

    public void setZipcode(String zipcode) {

        mEditor.putString("zipcode", zipcode);
        mEditor.commit();
    }
    public String getZipcode() {
        return mSettings.getString("zipcode", "");
    }

    /*------------    LatLng(Change Location)   -----------------------------------------*/
    public void setLat(String latitude) {

        mEditorLatLng.putString("latitude", latitude);
        mEditorLatLng.commit();
    }
    public String getLat() {
        return mPrefLatLng.getString("latitude", "");
    }

    public void setLng(String longitude) {

        mEditorLatLng.putString("longitude", longitude);
        mEditorLatLng.commit();
    }
    public String getLng() {
        return mPrefLatLng.getString("longitude", "");
    }

    /*------------    Search Radius  -----------------------------------------*/
    public void setSearchRadius(int searchRadius) {

        mEditor.putInt("searchRadius", searchRadius);
        mEditor.commit();
    }
    public int getSearchRadius() {
        return mSettings.getInt("searchRadius", 0);
    }

    /*------------    Model Class(BankItem)  -----------------------------------------*/
    public void setModelData(String modelData) {

        mEditor.putString("modelData", modelData);
        mEditor.commit();
    }
    public String getModelData() {
        return mSettings.getString("modelData", "");
    }

    /*------------    Fragment Name  -----------------------------------------*/
    public void setFragmentName(String fragmentName) {

        mEditor.putString("fragmentName", fragmentName);
        mEditor.commit();
    }
    public String getFragmentName() {
        return mSettings.getString("fragmentName", "");
    }
}