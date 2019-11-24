// Generated code from Butter Knife. Do not modify!
package com.vanco.merlinapp.ui.activity;

import android.support.annotation.CallSuper;
import android.support.annotation.UiThread;
import android.support.design.widget.NavigationView;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.widget.Toolbar;
import android.view.View;
import butterknife.Unbinder;
import butterknife.internal.Utils;
import com.vanco.merlinapp.R;
import java.lang.IllegalStateException;
import java.lang.Override;

public class NavigationDrawerActivity_ViewBinding implements Unbinder {
  private NavigationDrawerActivity target;

  @UiThread
  public NavigationDrawerActivity_ViewBinding(NavigationDrawerActivity target) {
    this(target, target.getWindow().getDecorView());
  }

  @UiThread
  public NavigationDrawerActivity_ViewBinding(NavigationDrawerActivity target, View source) {
    this.target = target;

    target.drawer = Utils.findRequiredViewAsType(source, R.id.drawer_layout, "field 'drawer'", DrawerLayout.class);
    target.toolbar = Utils.findRequiredViewAsType(source, R.id.toolbar, "field 'toolbar'", Toolbar.class);
    target.navigationView = Utils.findRequiredViewAsType(source, R.id.nav_view, "field 'navigationView'", NavigationView.class);
  }

  @Override
  @CallSuper
  public void unbind() {
    NavigationDrawerActivity target = this.target;
    if (target == null) throw new IllegalStateException("Bindings already cleared.");
    this.target = null;

    target.drawer = null;
    target.toolbar = null;
    target.navigationView = null;
  }
}
