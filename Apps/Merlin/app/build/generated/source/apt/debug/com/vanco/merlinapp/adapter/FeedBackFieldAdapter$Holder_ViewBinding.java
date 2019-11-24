// Generated code from Butter Knife. Do not modify!
package com.vanco.merlinapp.adapter;

import android.support.annotation.CallSuper;
import android.support.annotation.UiThread;
import android.view.View;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;
import butterknife.Unbinder;
import butterknife.internal.Utils;
import com.vanco.merlinapp.R;
import java.lang.IllegalStateException;
import java.lang.Override;

public class FeedBackFieldAdapter$Holder_ViewBinding implements Unbinder {
  private FeedBackFieldAdapter.Holder target;

  @UiThread
  public FeedBackFieldAdapter$Holder_ViewBinding(FeedBackFieldAdapter.Holder target, View source) {
    this.target = target;

    target.txtPosition = Utils.findRequiredViewAsType(source, R.id.txtPosition, "field 'txtPosition'", TextView.class);
    target.imgValidation = Utils.findRequiredViewAsType(source, R.id.imgValidation, "field 'imgValidation'", ImageView.class);
    target.txtQuestion = Utils.findRequiredViewAsType(source, R.id.txtQuestion, "field 'txtQuestion'", TextView.class);
    target.radioButton1 = Utils.findRequiredViewAsType(source, R.id.radioButton1, "field 'radioButton1'", RadioButton.class);
    target.radioButton2 = Utils.findRequiredViewAsType(source, R.id.radioButton2, "field 'radioButton2'", RadioButton.class);
    target.radioButton3 = Utils.findRequiredViewAsType(source, R.id.radioButton3, "field 'radioButton3'", RadioButton.class);
    target.radioButton4 = Utils.findRequiredViewAsType(source, R.id.radioButton4, "field 'radioButton4'", RadioButton.class);
    target.radioButton5 = Utils.findRequiredViewAsType(source, R.id.radioButton5, "field 'radioButton5'", RadioButton.class);
    target.radioButton6 = Utils.findRequiredViewAsType(source, R.id.radioButton6, "field 'radioButton6'", RadioButton.class);
    target.radioButton7 = Utils.findRequiredViewAsType(source, R.id.radioButton7, "field 'radioButton7'", RadioButton.class);
    target.radioGroup = Utils.findRequiredViewAsType(source, R.id.radioGroup, "field 'radioGroup'", RadioGroup.class);
    target.edtDescription = Utils.findRequiredViewAsType(source, R.id.edtDescription, "field 'edtDescription'", EditText.class);
    target.edtDescription2 = Utils.findRequiredViewAsType(source, R.id.edtDescription2, "field 'edtDescription2'", EditText.class);
  }

  @Override
  @CallSuper
  public void unbind() {
    FeedBackFieldAdapter.Holder target = this.target;
    if (target == null) throw new IllegalStateException("Bindings already cleared.");
    this.target = null;

    target.txtPosition = null;
    target.imgValidation = null;
    target.txtQuestion = null;
    target.radioButton1 = null;
    target.radioButton2 = null;
    target.radioButton3 = null;
    target.radioButton4 = null;
    target.radioButton5 = null;
    target.radioButton6 = null;
    target.radioButton7 = null;
    target.radioGroup = null;
    target.edtDescription = null;
    target.edtDescription2 = null;
  }
}
