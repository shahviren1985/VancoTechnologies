
package com.vanco.merlinapp.modal.loginslidemenu;

import java.util.List;
import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class ClsAnonymousMenuListResponse {

    @SerializedName("sideMenu")
    @Expose
    private List<SideMenu> sideMenu = null;

    public List<SideMenu> getSideMenu() {
        return sideMenu;
    }

    public void setSideMenu(List<SideMenu> sideMenu) {
        this.sideMenu = sideMenu;
    }

}
