package com.navigettr.app.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;
import java.util.List;

public class CurrencyModel implements Serializable {

    @SerializedName("currency")
    public List<CurrencyModel.currency> currency;

    public List<CurrencyModel.currency> getCurrency() {
        return currency;
    }

    public void setCurrency(List<CurrencyModel.currency> currency) {
        this.currency = currency;
    }

    public static class currency implements Serializable{

        public currency(String KEY) {
            this.KEY = KEY;
        }

        @SerializedName("KEY")
        private String KEY;

        public String getKEY() {
            return KEY;
        }

        public void setKEY(String KEY) {
            this.KEY = KEY;
        }

        @Override
        public String toString() {
            return KEY;
        }
    }
}
