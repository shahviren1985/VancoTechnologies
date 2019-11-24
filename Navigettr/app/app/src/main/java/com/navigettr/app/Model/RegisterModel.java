package com.navigettr.app.Model;

import com.google.gson.annotations.SerializedName;

public class RegisterModel {

    @SerializedName("message")
    private String message;

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }
}
