package com.sringara.sringara.helper;

import android.app.Activity;
import android.content.Context;
import android.content.DialogInterface;
import android.support.v7.app.AlertDialog;
import android.text.TextUtils;


public class DialogAlert {

	public static void show_dialog(final Context context, final String message) {
		((Activity) context).runOnUiThread(new Runnable() {
			public void run() {
				AlertDialog.Builder alerDialog = new AlertDialog.Builder(context).setMessage(message)
						.setPositiveButton(android.R.string.ok, new DialogInterface.OnClickListener() {
							@Override
							public void onClick(DialogInterface dialog, int which) {
								dialog.dismiss();
							}
						});
				if (!TextUtils.isEmpty(message)) {
					alerDialog.setMessage(message);
				}

				alerDialog.show();
			}
		});
	}

}
