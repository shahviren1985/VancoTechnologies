// Generated code from Butter Knife. Do not modify!
package com.vanco.merlinapp.ui.fragment;

import android.support.annotation.CallSuper;
import android.support.annotation.UiThread;
import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.TextView;
import butterknife.Unbinder;
import butterknife.internal.Utils;
import com.vanco.merlinapp.R;
import java.lang.IllegalStateException;
import java.lang.Override;

public class TeacherFeedBackFragment_ViewBinding implements Unbinder {
  private TeacherFeedBackFragment target;

  @UiThread
  public TeacherFeedBackFragment_ViewBinding(TeacherFeedBackFragment target, View source) {
    this.target = target;

    target.txtTeacherName = Utils.findRequiredViewAsType(source, R.id.txtTeacherName, "field 'txtTeacherName'", TextView.class);
    target.txtSubject = Utils.findRequiredViewAsType(source, R.id.txtSubject, "field 'txtSubject'", TextView.class);
    target.txtCode = Utils.findRequiredViewAsType(source, R.id.txtCode, "field 'txtCode'", TextView.class);
    target.txtSemster = Utils.findRequiredViewAsType(source, R.id.txtSemster, "field 'txtSemster'", TextView.class);
    target.txtSubjectCode = Utils.findRequiredViewAsType(source, R.id.txtSubjectCode, "field 'txtSubjectCode'", TextView.class);
    target.txtCourse = Utils.findRequiredViewAsType(source, R.id.txtCourse, "field 'txtCourse'", TextView.class);
    target.recyclerView = Utils.findRequiredViewAsType(source, R.id.recyclerView, "field 'recyclerView'", RecyclerView.class);
  }

  @Override
  @CallSuper
  public void unbind() {
    TeacherFeedBackFragment target = this.target;
    if (target == null) throw new IllegalStateException("Bindings already cleared.");
    this.target = null;

    target.txtTeacherName = null;
    target.txtSubject = null;
    target.txtCode = null;
    target.txtSemster = null;
    target.txtSubjectCode = null;
    target.txtCourse = null;
    target.recyclerView = null;
  }
}
