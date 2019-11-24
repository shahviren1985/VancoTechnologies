// Generated code from Butter Knife. Do not modify!
package com.vanco.merlinapp.ui.fragment;

import android.support.annotation.CallSuper;
import android.support.annotation.UiThread;
import android.view.View;
import android.webkit.WebView;
import android.widget.ProgressBar;
import butterknife.Unbinder;
import butterknife.internal.Utils;
import com.vanco.merlinapp.R;
import java.lang.IllegalStateException;
import java.lang.Override;

public class WebviewFragment_ViewBinding implements Unbinder {
  private WebviewFragment target;

  @UiThread
  public WebviewFragment_ViewBinding(WebviewFragment target, View source) {
    this.target = target;

    target.webView = Utils.findRequiredViewAsType(source, R.id.webView, "field 'webView'", WebView.class);
    target.progressBar = Utils.findRequiredViewAsType(source, R.id.progressbar, "field 'progressBar'", ProgressBar.class);
  }

  @Override
  @CallSuper
  public void unbind() {
    WebviewFragment target = this.target;
    if (target == null) throw new IllegalStateException("Bindings already cleared.");
    this.target = null;

    target.webView = null;
    target.progressBar = null;
  }
}
