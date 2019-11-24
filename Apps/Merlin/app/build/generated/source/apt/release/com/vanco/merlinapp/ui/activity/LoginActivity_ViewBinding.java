// Generated code from Butter Knife. Do not modify!
package com.vanco.merlinapp.ui.activity;

import android.support.annotation.CallSuper;
import android.support.annotation.UiThread;
import android.support.design.widget.NavigationView;
import android.support.design.widget.TextInputLayout;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.widget.AppCompatButton;
import android.support.v7.widget.AppCompatEditText;
import android.support.v7.widget.Toolbar;
import android.view.View;
import butterknife.Unbinder;
import butterknife.internal.DebouncingOnClickListener;
import butterknife.internal.Utils;
import com.vanco.merlinapp.R;
import java.lang.IllegalStateException;
import java.lang.Override;

public class LoginActivity_ViewBinding implements Unbinder {
  private LoginActivity target;

  private View view2131296296;

  @UiThread
  public LoginActivity_ViewBinding(LoginActivity target) {
    this(target, target.getWindow().getDecorView());
  }

  @UiThread
  public LoginActivity_ViewBinding(final LoginActivity target, View source) {
    this.target = target;

    View view;
    target.drawer = Utils.findRequiredViewAsType(source, R.id.drawer_layout, "field 'drawer'", DrawerLayout.class);
    target.toolbar = Utils.findRequiredViewAsType(source, R.id.toolbar, "field 'toolbar'", Toolbar.class);
    target.navigationView = Utils.findRequiredViewAsType(source, R.id.nav_view, "field 'navigationView'", NavigationView.class);
    target.edtMobileNumber = Utils.findRequiredViewAsType(source, R.id.edtMobileNumber, "field 'edtMobileNumber'", AppCompatEditText.class);
    target.txtInputUsername = Utils.findRequiredViewAsType(source, R.id.txtInputMobileNumber, "field 'txtInputUsername'", TextInputLayout.class);
    target.txtInputPassword = Utils.findRequiredViewAsType(source, R.id.txtInputLastName, "field 'txtInputPassword'", TextInputLayout.class);
    target.edtLastName = Utils.findRequiredViewAsType(source, R.id.edtLastName, "field 'edtLastName'", AppCompatEditText.class);
    view = Utils.findRequiredView(source, R.id.btnLogin, "field 'btnLogin' and method 'onClick'");
    target.btnLogin = Utils.castView(view, R.id.btnLogin, "field 'btnLogin'", AppCompatButton.class);
    view2131296296 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
  }

  @Override
  @CallSuper
  public void unbind() {
    LoginActivity target = this.target;
    if (target == null) throw new IllegalStateException("Bindings already cleared.");
    this.target = null;

    target.drawer = null;
    target.toolbar = null;
    target.navigationView = null;
    target.edtMobileNumber = null;
    target.txtInputUsername = null;
    target.txtInputPassword = null;
    target.edtLastName = null;
    target.btnLogin = null;

    view2131296296.setOnClickListener(null);
    view2131296296 = null;
  }
}
