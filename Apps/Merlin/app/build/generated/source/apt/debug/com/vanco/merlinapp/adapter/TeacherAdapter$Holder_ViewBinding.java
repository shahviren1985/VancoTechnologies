// Generated code from Butter Knife. Do not modify!
package com.vanco.merlinapp.adapter;

import android.support.annotation.CallSuper;
import android.support.annotation.UiThread;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;
import butterknife.Unbinder;
import butterknife.internal.Utils;
import com.vanco.merlinapp.R;
import java.lang.IllegalStateException;
import java.lang.Override;

public class TeacherAdapter$Holder_ViewBinding implements Unbinder {
  private TeacherAdapter.Holder target;

  @UiThread
  public TeacherAdapter$Holder_ViewBinding(TeacherAdapter.Holder target, View source) {
    this.target = target;

    target.txtTeacherName = Utils.findRequiredViewAsType(source, R.id.txtTeacherName, "field 'txtTeacherName'", TextView.class);
    target.txtSubject = Utils.findRequiredViewAsType(source, R.id.txtSubject, "field 'txtSubject'", TextView.class);
    target.imgStatus = Utils.findRequiredViewAsType(source, R.id.imgStatus, "field 'imgStatus'", ImageView.class);
    target.txtCode = Utils.findRequiredViewAsType(source, R.id.txtCode, "field 'txtCode'", TextView.class);
    target.txtSemster = Utils.findRequiredViewAsType(source, R.id.txtSemster, "field 'txtSemster'", TextView.class);
    target.imgArrow = Utils.findRequiredViewAsType(source, R.id.imgArrow, "field 'imgArrow'", ImageView.class);
  }

  @Override
  @CallSuper
  public void unbind() {
    TeacherAdapter.Holder target = this.target;
    if (target == null) throw new IllegalStateException("Bindings already cleared.");
    this.target = null;

    target.txtTeacherName = null;
    target.txtSubject = null;
    target.imgStatus = null;
    target.txtCode = null;
    target.txtSemster = null;
    target.imgArrow = null;
  }
}
