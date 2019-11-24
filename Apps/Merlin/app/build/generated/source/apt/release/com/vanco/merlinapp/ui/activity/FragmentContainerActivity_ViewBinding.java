// Generated code from Butter Knife. Do not modify!
package com.vanco.merlinapp.ui.activity;

import android.support.annotation.CallSuper;
import android.support.annotation.UiThread;
import android.support.design.widget.AppBarLayout;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.widget.FrameLayout;
import android.widget.TextView;
import butterknife.Unbinder;
import butterknife.internal.DebouncingOnClickListener;
import butterknife.internal.Utils;
import com.vanco.merlinapp.R;
import java.lang.IllegalStateException;
import java.lang.Override;

public class FragmentContainerActivity_ViewBinding implements Unbinder {
  private FragmentContainerActivity target;

  private View view2131296601;

  @UiThread
  public FragmentContainerActivity_ViewBinding(FragmentContainerActivity target) {
    this(target, target.getWindow().getDecorView());
  }

  @UiThread
  public FragmentContainerActivity_ViewBinding(final FragmentContainerActivity target,
      View source) {
    this.target = target;

    View view;
    target.toolbar = Utils.findRequiredViewAsType(source, R.id.toolbar, "field 'toolbar'", Toolbar.class);
    target.txtTitle = Utils.findRequiredViewAsType(source, R.id.txtTitle, "field 'txtTitle'", TextView.class);
    view = Utils.findRequiredView(source, R.id.txtSave, "field 'txtSave' and method 'onClick'");
    target.txtSave = Utils.castView(view, R.id.txtSave, "field 'txtSave'", TextView.class);
    view2131296601 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.appBar = Utils.findRequiredViewAsType(source, R.id.appBar, "field 'appBar'", AppBarLayout.class);
    target.frmContainer = Utils.findRequiredViewAsType(source, R.id.frmContainer, "field 'frmContainer'", FrameLayout.class);
  }

  @Override
  @CallSuper
  public void unbind() {
    FragmentContainerActivity target = this.target;
    if (target == null) throw new IllegalStateException("Bindings already cleared.");
    this.target = null;

    target.toolbar = null;
    target.txtTitle = null;
    target.txtSave = null;
    target.appBar = null;
    target.frmContainer = null;

    view2131296601.setOnClickListener(null);
    view2131296601 = null;
  }
}
