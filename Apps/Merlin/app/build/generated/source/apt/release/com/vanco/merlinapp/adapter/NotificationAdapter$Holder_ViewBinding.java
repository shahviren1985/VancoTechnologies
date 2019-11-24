// Generated code from Butter Knife. Do not modify!
package com.vanco.merlinapp.adapter;

import android.support.annotation.CallSuper;
import android.support.annotation.UiThread;
import android.view.View;
import android.widget.TextView;
import butterknife.Unbinder;
import butterknife.internal.Utils;
import com.vanco.merlinapp.R;
import java.lang.IllegalStateException;
import java.lang.Override;

public class NotificationAdapter$Holder_ViewBinding implements Unbinder {
  private NotificationAdapter.Holder target;

  @UiThread
  public NotificationAdapter$Holder_ViewBinding(NotificationAdapter.Holder target, View source) {
    this.target = target;

    target.txtTitle = Utils.findRequiredViewAsType(source, R.id.txtTitle, "field 'txtTitle'", TextView.class);
    target.txtDescription = Utils.findRequiredViewAsType(source, R.id.txtDescription, "field 'txtDescription'", TextView.class);
  }

  @Override
  @CallSuper
  public void unbind() {
    NotificationAdapter.Holder target = this.target;
    if (target == null) throw new IllegalStateException("Bindings already cleared.");
    this.target = null;

    target.txtTitle = null;
    target.txtDescription = null;
  }
}
