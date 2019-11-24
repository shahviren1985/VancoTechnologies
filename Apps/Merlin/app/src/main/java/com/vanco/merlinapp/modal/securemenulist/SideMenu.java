
package com.vanco.merlinapp.modal.securemenulist;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class SideMenu {

    @SerializedName("title")
    @Expose
    private String title;
    @SerializedName("isActive")
    @Expose
    private Boolean isActive;
    @SerializedName("link")
    @Expose
    private String link;

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public Boolean getIsActive() {
        return isActive;
    }

    public void setIsActive(Boolean isActive) {
        this.isActive = isActive;
    }

    public String getLink() {
        return link;
    }

    public void setLink(String link) {
        this.link = link;
    }

}
