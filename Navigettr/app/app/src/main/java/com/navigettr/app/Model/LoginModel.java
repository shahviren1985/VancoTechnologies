package com.navigettr.app.Model;

import com.google.gson.annotations.SerializedName;

public class LoginModel {

    @SerializedName("UserId")
    private int UserId;

    public int getUserId() {
        return UserId;
    }

    public void setUserId(int userId) {
        UserId = userId;
    }
}
