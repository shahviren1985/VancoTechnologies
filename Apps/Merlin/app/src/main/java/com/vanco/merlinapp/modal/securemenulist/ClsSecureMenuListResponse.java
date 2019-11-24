
package com.vanco.merlinapp.modal.securemenulist;

import java.util.List;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class ClsSecureMenuListResponse {

    @SerializedName("sideMenu")
    @Expose
    private List<SideMenu> sideMenu = null;
    @SerializedName("logo")
    @Expose
    private String logo;
    @SerializedName("collegeName")
    @Expose
    private String collegeName;

    public List<SideMenu> getSideMenu() {
        return sideMenu;
    }

    public void setSideMenu(List<SideMenu> sideMenu) {
        this.sideMenu = sideMenu;
    }

    public String getLogo() {
        return logo;
    }

    public void setLogo(String logo) {
        this.logo = logo;
    }

    public String getCollegeName() {
        return collegeName;
    }

    public void setCollegeName(String collegeName) {
        this.collegeName = collegeName;
    }

}
