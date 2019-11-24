package com.vanco.merlinapp.utility;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.res.TypedArray;
import android.database.Cursor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.graphics.PorterDuff;
import android.net.ConnectivityManager;
import android.net.Uri;
import android.os.Build;
import android.os.Environment;
import android.provider.MediaStore;
import android.support.design.widget.Snackbar;
import android.util.DisplayMetrics;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.PopupMenu;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import com.google.gson.Gson;
import com.google.gson.JsonSyntaxException;
import com.vanco.merlinapp.MerlinApp;
import com.vanco.merlinapp.R;
import com.vanco.merlinapp.keyinterface.Constants;
import com.vanco.merlinapp.keyinterface.OnPopupMenuClick;
import com.vanco.merlinapp.modal.ClsFeedback;
import com.vanco.merlinapp.modal.ClsLoginResponse;
import com.vanco.merlinapp.modal.ClsTeacher;
import com.vanco.merlinapp.modal.ClsTeacherFeedBackFill;
import com.vanco.merlinapp.network.RetrofitInterface;
import com.vanco.merlinapp.ui.activity.BaseActivity;

import org.json.JSONArray;
import org.json.JSONException;

import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.text.DecimalFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.TimeZone;
import java.util.UUID;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;


/**
 * Created by PCS77 on 01-03-2016.
 */
public class Utility implements Constants {

    private static ProgressDialog progressDialog;
    private static Toast mToast;
    private static AlertDialog alertDialog = null;
    private static Snackbar snackbar;
    private static final String TEMP_PHOTO_FILE = "temporary_holder.jpg";

    @SuppressLint("ShowToast")
    public static void toast(Context context, String message) {
        if (mToast != null)
            mToast.cancel();

        mToast = Toast.makeText(context, message, Toast.LENGTH_LONG);
        mToast.show();

        // if(!toast.getView().isShown())
        // Toast.makeText(context, message, Toast.LENGTH_LONG).show();
    }


    @SuppressLint("ShowToast")
    public static void toast(Context context, int message) {
        if (mToast != null)
            mToast.cancel();

        mToast = Toast.makeText(context, message, Toast.LENGTH_LONG);
        mToast.show();

        // if(!toast.getView().isShown())
        // Toast.makeText(context, message, Toast.LENGTH_LONG).show();
    }


    public static void setTintColor(ImageView imageView, int color) {
        if (imageView == null)
            return;

        imageView.setColorFilter(color, PorterDuff.Mode.SRC_ATOP);
    }

    public static void hideKeyboard(Context context, View view) {
        // Check if no view has focus:
        try {
            if (view != null) {
                InputMethodManager inputManager = (InputMethodManager) context.getSystemService(Context.INPUT_METHOD_SERVICE);
                inputManager.hideSoftInputFromWindow(view.getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);
            }
        } catch (NullPointerException e) {

        }

    }


    public static void setProgressStyle(Context mContext, ProgressBar progressBar, int color) {
        progressBar.setIndeterminate(true);
        progressBar.getIndeterminateDrawable().setColorFilter(color, PorterDuff.Mode.MULTIPLY);
    }

    public static String getIfNull(String checkStr, String returnStr) {
        if (isValueNull(checkStr))
            return returnStr;
        return checkStr;
    }


    public static int getScreenWidth() {
        DisplayMetrics metrics = MerlinApp.CONTEXT.getResources().getDisplayMetrics();
        return metrics.widthPixels;
        // int height = metrics.heightPixels;
    }

    public static int getScreenHeight() {
        DisplayMetrics metrics = MerlinApp.CONTEXT.getResources().getDisplayMetrics();
        return metrics.heightPixels;
        // int height = metrics.heightPixels;
    }

    /**
     * Check weather the Internet connection is available
     *
     * @param context
     * @return
     */
    public static boolean isInternetConnectionAvailable(Context context) {
        try {
            ConnectivityManager connec = (ConnectivityManager) context.getSystemService(Context.CONNECTIVITY_SERVICE);
            android.net.NetworkInfo wifi = connec.getNetworkInfo(ConnectivityManager.TYPE_WIFI);
            android.net.NetworkInfo mobile = connec.getNetworkInfo(ConnectivityManager.TYPE_MOBILE);

            if ((wifi != null && wifi.isConnected()) || (mobile != null && mobile.isConnected())) {
                return true;
            }
            return false;
        } catch (NullPointerException e) {
            //Logger.e(TAG, Log.getStackTraceString(e));

            return false;
        }
    }


    public static void openAlertDialog(Context context, String message) {
        try {


            if (!checkNullValue(message)) {
                message = "Message NULL Check this.";
                return;
            }

            if (alertDialog != null && alertDialog.isShowing())
                return;

            AlertDialog.Builder builder = new AlertDialog.Builder(context);
            builder.setTitle(R.string.app_name);
            builder.setMessage(message);
            builder.setCancelable(false).setPositiveButton(android.R.string.ok, new DialogInterface.OnClickListener() {
                public void onClick(DialogInterface dialog, int which) {
                    dialog.dismiss();
                    alertDialog = null;
                }
            });
            alertDialog = builder.create();
            alertDialog.show();

        } catch (NullPointerException e) {

        }

    }

    public static boolean isValueNull(String value) {
        if (value != null && !value.trim().equalsIgnoreCase("null") && !value.trim().equals("")) {
            return false;
        } else {
            return true;
        }
    }

    public static boolean isValueNull(EditText editText) {
        if (editText == null)
            return true;
        return isValueNull(editText.getText().toString());
    }

    public static boolean isValueNullExit(EditText editText) {
        if (editText == null)
            return true;
        if (editText.getText().toString().trim().equals(editText.getContext().getString(R.string.select)))
            return true;
        return isValueNull(editText.getText().toString());
    }

    public static boolean isValueNull(TextView textView) {
        if (textView == null)
            return true;
        return isValueNull(textView.getText().toString());
    }

    public static void openAlertDialog(Context context, int message) {
        try {
            if (!checkNullValue(context.getString(message))) {
                return;
            }

            if (alertDialog != null && alertDialog.isShowing())
                return;

            AlertDialog.Builder builder = new AlertDialog.Builder(context);
            builder.setTitle(R.string.app_name);
            builder.setMessage(context.getString(message));
            builder.setCancelable(false).setPositiveButton(R.string.label_ok, new DialogInterface.OnClickListener() {
                public void onClick(DialogInterface dialog, int which) {
                    dialog.dismiss();
                    alertDialog = null;
                }
            });
            alertDialog = builder.create();
            alertDialog.show();


            //For Changing the color of dialog title
           /* int textViewId = alertDialog.getContext().getResources().getIdentifier("android:id/alertTitle", null, null);
            TextView tv = (TextView) alertDialog.findViewById(textViewId);
            tv.setTextColor(alertDialog.getContext().getResources().getColor(R.color.colorAccent));
*/
        } catch (NullPointerException e) {

        }

    }

    public static void openAlertDialog(Context context, String title, String message) {
        try {
            if (!checkNullValue(message)) {
                return;
            }

            if (alertDialog != null && alertDialog.isShowing())
                return;

            AlertDialog.Builder builder = new AlertDialog.Builder(context);
            builder.setTitle(title);
            builder.setMessage(message);
            builder.setCancelable(false).setPositiveButton(R.string.label_ok, new DialogInterface.OnClickListener() {
                public void onClick(DialogInterface dialog, int which) {
                    dialog.dismiss();
                    alertDialog = null;
                }
            });
            alertDialog = builder.create();
            alertDialog.show();


            //For Changing the color of dialog title
           /* int textViewId = alertDialog.getContext().getResources().getIdentifier("android:id/alertTitle", null, null);
            TextView tv = (TextView) alertDialog.findViewById(textViewId);
            tv.setTextColor(alertDialog.getContext().getResources().getColor(R.color.colorAccent));
*/
        } catch (NullPointerException e) {

        }

    }

    public static boolean checkNullValue(String value) {
        if (value != null && !value.equalsIgnoreCase("null") && !value.equals(""))
            return true;
        else
            return false;
    }


    public static void setEditTextEditable(EditText editText, boolean isEditable) {

        if (isEditable) {
            editText.setFocusable(true);
            editText.setFocusableInTouchMode(true); // user touches widget on phone with touch screen
            editText.setClickable(true); // user navigates with wheel and selects widget
        } else {
            editText.setFocusable(false);
            editText.setFocusableInTouchMode(false); // user touches widget on phone with touch screen
            editText.setClickable(false); // user navigates with wheel and selects widget

        }

    }

    public static String readStream(InputStream entityResponse) throws IOException {
        ByteArrayOutputStream baos = new ByteArrayOutputStream();
        byte[] buffer = new byte[1024];
        int length = 0;
        while ((length = entityResponse.read(buffer)) != -1) {
            baos.write(buffer, 0, length);
        }
        return baos.toString();
    }


    public static String getDate() {

        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");

        Date currenTimeZone = new Date(System.currentTimeMillis());
        return sdf.format(currenTimeZone);
    }

    public static String getDate(int year, int month, int day) {

        StringBuffer date = new StringBuffer();
        if (String.valueOf(day).length() < 2) {
            date.append("0" + day + "-");
        } else {
            date.append(String.valueOf(day) + "-");
        }

        if (String.valueOf(month).length() < 2) {
            date.append("0" + month + "-");
        } else {
            date.append(String.valueOf(month) + "-");
        }

        date.append(year);

        return date.toString();
    }

    public static String getDate(long interval) {
        SimpleDateFormat sdf = new SimpleDateFormat("h:mm aa");
        Date currenTimeZone = new Date(interval);
        return sdf.format(currenTimeZone);
    }

    public static String getDate(String strDate) {
        SimpleDateFormat fromSimpleDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm");
        SimpleDateFormat sdf = new SimpleDateFormat("h:mm aa");

        Date currentTimeZone = null;
        try {
            currentTimeZone = fromSimpleDateFormat.parse(strDate);
            return sdf.format(currentTimeZone);

        } catch (ParseException e) {
            e.printStackTrace();
        }
        return strDate;
    }

    public static String getStrDate() {
        SimpleDateFormat fromSimpleDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm");
        Date currentTimeZone = new Date();
        return fromSimpleDateFormat.format(currentTimeZone);
    }

    public static long getTimeStampOfHistory(String date) {
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.SS");
        try {
            Date d = simpleDateFormat.parse(date);
            return d.getTime();
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return System.currentTimeMillis();
    }

    public static long getTimeForHoursAndMin(String strDate) {

        strDate = getDateDDMMYYYY(System.currentTimeMillis()) + " " + strDate;

        SimpleDateFormat sdf1 = new SimpleDateFormat("yyyy-MM-dd kk:mm");
        try {
            Date date = sdf1.parse(strDate);
            return date.getTime();

        } catch (ParseException e) {
            e.printStackTrace();
        }

        return System.currentTimeMillis();

    }

    public static long getTimeStamp(String strDate) {

        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        try {
            Date date = sdf.parse(strDate);
            return date.getTime();

        } catch (ParseException e) {
            e.printStackTrace();
        }

        return System.currentTimeMillis();

    }


    public static String[] parseDate(String strDate) throws ParseException {
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        Date date = simpleDateFormat.parse(strDate);

        String[] string = new String[2];
        string[0] = (String) android.text.format.DateFormat.format("MMM", date);
        string[1] = (String) android.text.format.DateFormat.format("dd", date);

        return string;
    }

    public static String getDateDDMMYYYY(long interval) {
        SimpleDateFormat sdf1 = new SimpleDateFormat("dd/MM/yyyy");
        return sdf1.format(new Date(interval));

    }

    public static String getDateDDMMYYYY(String date) {
        SimpleDateFormat sdf1 = new SimpleDateFormat("yyyy-MM-dd");

        try {
            return sdf1.format(sdf1.parse(date));
        } catch (ParseException e) {
            e.printStackTrace();
        }

        return null;
    }

    public static Date getDateObjectYYYYMMDD(String date) {
        SimpleDateFormat sdf1 = new SimpleDateFormat("yyyy-MM-dd");

        try {
            return sdf1.parse(date);
        } catch (ParseException e) {
            e.printStackTrace();
        }

        return null;
    }

    public static String getDateYYYYMMDDHHMMSS(long interval) {

        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        return simpleDateFormat.format(new Date(interval));
    }

    public static String getDateHHMMSS(long interval) {
        SimpleDateFormat sdf1 = new SimpleDateFormat("HH:mm");
        return sdf1.format(new Date(interval));

    }

    public static String getDateHHMMSS24(long interval) {
        interval += getTimeOfDay();
        SimpleDateFormat sdf1 = new SimpleDateFormat("kk:mm");
        return sdf1.format(new Date(interval));

    }


    public static long getOneDayInterval() {
        return 24 * 60 * 60 * 1000;
    }

    public static long getTimeOfDay() {
        SimpleDateFormat sdf1 = new SimpleDateFormat("yyyy-MM-dd");
        Date date = new Date();
        try {
            Log.d("date Format", sdf1.format(date));
            date = sdf1.parse(sdf1.format(date));
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return date.getTime();
    }


    public static void showProgress(String message, Activity activity) {
        progressDialog = new ProgressDialog(activity);
        progressDialog.setMessage(message == null
                ? "Loading..."
                : message);
        progressDialog.setCancelable(false);
        progressDialog.setProgressStyle(ProgressDialog.STYLE_SPINNER);
        progressDialog.show();
    }

    public static boolean checkProgressOpen() {
        if (progressDialog != null && progressDialog.isShowing())
            return true;
        else
            return false;
    }

    public static void cancelProgress() {
        if (progressDialog != null && progressDialog.isShowing()) {
            progressDialog.cancel();
            progressDialog = null;
        }
    }


    public static void giveTintEffect(ImageView imageView, int color) {

        if (imageView != null) {
            imageView.getDrawable().setColorFilter(color, PorterDuff.Mode.SRC_ATOP);
        }
    }


    public static String getTime(long time) {
        Date date = new Date();
        date.setTime(time);
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("HH:mm:ss");
        return simpleDateFormat.format(date);
    }


    public static int getDayOfWeek(String date) throws ParseException {
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        Date currenTimeZone = sdf.parse(date);

        Calendar calendar = Calendar.getInstance();
        calendar.setTimeZone(TimeZone.getDefault());
        // calendar.setTimeZone(TimeZone.getTimeZone("GMT+2:00"));
        calendar.setTimeInMillis(currenTimeZone.getTime());
        return calendar.get(Calendar.WEEK_OF_YEAR);
    }


    public static String getHours(double time) {
        double originalTime = time;
        time = time / 1000;
        double hour = (time / 60) / 60;
        DecimalFormat df = new DecimalFormat();
        if (originalTime < 60000)
            df.setMaximumFractionDigits(5);
        else
            df.setMaximumFractionDigits(2);

        return df.format(hour);
    }


    public static int getActionBarHeight(Context context) {

        final TypedArray styledAttributes = context.getTheme().obtainStyledAttributes(
                new int[]{android.R.attr.actionBarSize});
        int mActionBarSize = (int) styledAttributes.getDimension(0, 0);
        styledAttributes.recycle();
        return mActionBarSize;
    }

    public static int getStatusBarHeight(Context context) {
        int result = 0;
        int resourceId = context.getResources().getIdentifier("status_bar_height", "dimen", "android");
        if (resourceId > 0) {
            result = context.getResources().getDimensionPixelSize(resourceId);
        }
        return result;
    }


    /*public static void call(Context context, String number) {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M && !VideochatApp.preferenceData.getBooleanValueFromKey(Manifest.permission.CALL_PHONE)) {
            return;
        }
        String uri = "tel:" + number.trim();
        Intent intent = new Intent(Intent.ACTION_CALL);
        intent.setData(Uri.parse(uri));

        PackageManager packageManager = context.getPackageManager();
        if (intent.resolveActivity(packageManager) != null) {
            context.startActivity(intent);
        } else {
            openAlertDialog(context, "Du kan inte ringa från den här enheten.");
        }
    }
*/

    public static void sentEmail(Context context, String email) {
        Intent emailIntent = new Intent(Intent.ACTION_SENDTO, Uri.fromParts(
                "mailto", email, null));
        emailIntent.putExtra(Intent.EXTRA_SUBJECT, "Supportmail via " + context.getString(R.string.app_name));
        emailIntent.putExtra(Intent.EXTRA_TEXT, "");
        context.startActivity(Intent.createChooser(emailIntent, "Send email..."));
    }


    public static String makeDir(String path) {
        File gallery = new File(Environment.getExternalStorageDirectory().getPath()
                + path);
        if (!gallery.exists()) {
            gallery.mkdirs();
        }

        return gallery.getAbsolutePath();
    }

    public static void mediaScan(String file) {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.KITKAT) {
            Intent mediaScanIntent = new Intent(Intent.ACTION_MEDIA_SCANNER_SCAN_FILE);
            File f = new File(file);
            Uri contentUri = Uri.fromFile(f);
            mediaScanIntent.setData(contentUri);
            MerlinApp.CONTEXT.sendBroadcast(mediaScanIntent);
        } else {
            MerlinApp.CONTEXT.sendBroadcast(new Intent(Intent.ACTION_MEDIA_MOUNTED, Uri.parse(file)));
        }
    }

    public static void showSnackBar(View layout, String message) {

        if (layout == null || layout.getContext() == null || ((BaseActivity) layout.getContext()).isDestroyed())
            return;

        if (snackbar != null && snackbar.isShown())
            snackbar.dismiss();
        snackbar = Snackbar
                .make(layout, message, Snackbar.LENGTH_LONG);

        View view = snackbar.getView();
        if (view != null) {
            view.setBackgroundColor(Color.parseColor("#D50000"));
        }
        snackbar.show();
    }

    public static void showSnackBar(View layout, int message) {
        if (layout == null || layout.getContext() == null || ((BaseActivity) layout.getContext()).isDestroyed())
            return;
        showSnackBar(layout, layout.getContext().getString(message));

    }

    public final static boolean isValidEmail(CharSequence target) {
        if (target == null) {
            return false;
        } else {
            return android.util.Patterns.EMAIL_ADDRESS.matcher(target).matches();
        }
    }

    public final static boolean isValidMobileNo(CharSequence target) {
        if (target == null) {
            return false;
        } else {
            if (target.length() < 13)
                return false;

            return true;
        }
    }


    public final static boolean isMatchingString(String str1, String str2) {
        if (str1 == null || str2 == null)
            return false;

        if (str1.equals(str2))
            return true;
        return false;
    }

    public final static boolean isPasswordLengthValid(CharSequence target) {
        if (target == null) {
            return false;
        } else {
            if (target.toString().trim().length() < 6)
                return false;
        }
        return true;
    }

    public static void openEmail(Context context) {
        if (context == null)
            return;

        Intent intent = new Intent(Intent.ACTION_VIEW);
        Uri data = Uri.parse("mailto:?subject=" + "Report Issue" + "&body=" + "Found Bug into Application");
        intent.setData(data);
        context.startActivity(intent);
    }

    public static String convertDate(String strDate, String format) {
        if (isValueNull(strDate))
            return null;

        SimpleDateFormat fromDateFormat = new SimpleDateFormat(format);
        SimpleDateFormat toDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss.FFFFFFFzzz");

        try {
            return toDateFormat.format(fromDateFormat.parse(strDate));
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return strDate;
    }

    public static <T> T fromJson(String json, Class<T> classOfT) throws JsonSyntaxException {
        Gson gson = new Gson();
        return gson.fromJson(json, classOfT);
    }


    public final static boolean isValidPhoneNumber(CharSequence target) {
        if (target == null || target.length() < 6 || target.length() > 13) {
            return false;
        } else {
            return android.util.Patterns.PHONE.matcher(target).matches();
        }
    }


    public static boolean isMe(String role) {
        if (role.equalsIgnoreCase("Sender")) {
            return true;
        } else {
            return false;
        }
    }

    public static Uri getTempUri(boolean isCreatingNewFile) {
        return Uri.fromFile(getTempFile(isCreatingNewFile));
    }

    private static File getTempFile(boolean isCreatingNewFile) {
        if (Environment.getExternalStorageState().equals(
                Environment.MEDIA_MOUNTED)) {

            File file = new File(Environment.getExternalStorageDirectory(), TEMP_PHOTO_FILE);
            try {
                if (!file.exists()) {
                    file.createNewFile();
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
            return file;
        } else {
            return null;
        }
    }

    public static boolean createDirIfNotExists(String path) {
        boolean ret = true;

        File file = new File(Environment.getExternalStorageDirectory(), path);
        if (!file.exists()) {
            if (!file.mkdirs()) {
                Log.e("TravellerLog :: ", "Problem creating  folder");
                ret = false;
            }
        }
        return ret;
    }


    public static void deletePhoto(String path) {
        File file = new File(path);
        if (file.exists()) {
            file.delete();
        }
    }

    public static int getImageWidth() {
        return (int) (getScreenWidth() * 0.52);
    }

    public static int[] getBitmap(Bitmap bitmap) {
        Bitmap scaledBitmap = null;

        int maxWidth = getImageWidth();
        int maxHeight = getImageWidth();

        BitmapFactory.Options options = new BitmapFactory.Options();
        options.inJustDecodeBounds = true;
        options.outHeight = bitmap.getHeight();
        options.outWidth = bitmap.getWidth();


        int actualHeight = options.outHeight;
        int actualWidth = options.outWidth;

        float imgRatio = (float) actualWidth / (float) actualHeight;
        float maxRatio = maxWidth / maxHeight;

        if (actualHeight > maxHeight || actualWidth > maxWidth) {
            if (imgRatio < maxRatio) {
                imgRatio = maxHeight / actualHeight;
                actualWidth = (int) (imgRatio * actualWidth);
                actualHeight = (int) maxHeight;
            } else if (imgRatio > maxRatio) {
                imgRatio = maxWidth / actualWidth;
                actualHeight = (int) (imgRatio * actualHeight);
                actualWidth = (int) maxWidth;
            } else {
                actualHeight = (int) maxHeight;
                actualWidth = (int) maxWidth;

            }
        }

        return new int[]{actualWidth, actualHeight};
    }

    public static int calculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight) {
        final int height = options.outHeight;
        final int width = options.outWidth;
        int inSampleSize = 1;

        if (height > reqHeight || width > reqWidth) {
            final int heightRatio = Math.round((float) height / (float) reqHeight);
            final int widthRatio = Math.round((float) width / (float) reqWidth);
            inSampleSize = heightRatio < widthRatio ? heightRatio : widthRatio;
        }
        final float totalPixels = width * height;
        final float totalReqPixelsCap = reqWidth * reqHeight * 2;

        while (totalPixels / (inSampleSize * inSampleSize) > totalReqPixelsCap) {
            inSampleSize++;
        }

        return inSampleSize;
    }

    public static long getFileSize(File file) {
        try {
            if (file == null || !file.exists())
                return 0;

            long length = file.length();
            length = length / 1024;
            System.out.println("File Path : " + file.getPath() + ", File size : " + length + " KB");
            return length;
        } catch (Exception e) {
            System.out.println("File not found : " + e.getMessage() + e);
        }
        return 0;
    }

    public static String getRealPathFromURI(Context context, Uri contentUri) {
        String path = null;
        String[] proj = {MediaStore.MediaColumns.DATA};

        if ("content".equalsIgnoreCase(contentUri.getScheme())) {
            Cursor cursor = context.getContentResolver().query(contentUri, proj, null, null, null);

            if (cursor == null)
                return null;

            if (cursor.moveToFirst()) {

                int column_index = cursor.getColumnIndexOrThrow(MediaStore.MediaColumns.DATA);
                path = cursor.getString(column_index);
            }
            cursor.close();
            return path;
        } else if ("file".equalsIgnoreCase(contentUri.getScheme())) {
            return contentUri.getPath();
        }
        return null;
    }

    public static String getFileNameByUri(Context context, Uri uri) {
        String fileName = "unknown";//default fileName
        Uri filePathUri = uri;
        if (uri.getScheme().toString().compareTo("content") == 0) {
            Cursor cursor = context.getContentResolver().query(uri, null, null, null, null);
            if (cursor.moveToFirst()) {
                int column_index = cursor.getColumnIndexOrThrow(MediaStore.Images.Media.DATA);//Instead of "MediaStore.Images.Media.DATA" can be used "_data"
                filePathUri = Uri.parse(cursor.getString(column_index));
                fileName = filePathUri.getPath().toString();
            }
        } else if (uri.getScheme().compareTo("file") == 0) {
            fileName = filePathUri.getPath().toString();
        } else {
            fileName = fileName + "_" + filePathUri.getLastPathSegment();
        }
        return fileName;
    }

    public static void downloadFile(Context context, String uri) {
        try {
            Intent i = new Intent(Intent.ACTION_VIEW);
            if (uri.contains("https://"))
                i.setData(Uri.parse(uri));
            else
                i.setData(Uri.fromFile(new File(uri)));

            context.startActivity(i);

        } catch (Exception e) {
            e.printStackTrace();
        }
    }


    public static void setValue(String key, String value) {
        MerlinApp.preferenceData.setValue(key, value);
    }

    public static void setValue(String key, int value) {
        MerlinApp.preferenceData.setIntValue(key, value);
    }

    public static void setValue(String key, boolean value) {
        MerlinApp.preferenceData.setBooleanValue(key, value);
    }

    public static void setValue(String key, long value) {
        MerlinApp.preferenceData.setLongValue(key, value);
    }

    public static int getIntValue(String key) {
        return MerlinApp.preferenceData.getIntValueFromKey(key);
    }

    public static String getStringValue(String key) {
        return MerlinApp.preferenceData.getValueFromKey(key);
    }

    public static boolean getBooleanValue(String key) {
        return MerlinApp.preferenceData.getBooleanValueFromKey(key);
    }

    public static long getLongValue(String key) {
        return MerlinApp.preferenceData.getLongValue(key);
    }

    public static String getCollageCode() {
        String res = Utility.getStringValue(LOGIN_DATA);
        ClsLoginResponse clsLoginResponse = new Gson().fromJson(res, ClsLoginResponse.class);
        if (clsLoginResponse == null)
            return null;
        return clsLoginResponse.getCollegeCode();
    }

    public static String getUserId() {
        String res = Utility.getStringValue(LOGIN_DATA);
        ClsLoginResponse clsLoginResponse = new Gson().fromJson(res, ClsLoginResponse.class);
        if (clsLoginResponse == null)
            return null;
        return clsLoginResponse.getUserId();
    }

    public static String getUserMobileNo() {
        String res = Utility.getStringValue(LOGIN_DATA);
        ClsLoginResponse clsLoginResponse = new Gson().fromJson(res, ClsLoginResponse.class);
        if (clsLoginResponse == null)
            return null;
        return clsLoginResponse.getMobileNumber();
    }

    public static ClsLoginResponse getLoginResponse() {
        String res = Utility.getStringValue(LOGIN_DATA);
        ClsLoginResponse clsLoginResponse = new Gson().fromJson(res, ClsLoginResponse.class);
        if (clsLoginResponse == null)
            return null;
        return clsLoginResponse;
    }

    public static List<ClsTeacherFeedBackFill> getTeacherFeedBackList() {
        String res = Utility.getStringValue(LOGIN_DATA);
        ClsLoginResponse clsLoginResponse = new Gson().fromJson(res, ClsLoginResponse.class);
        if (clsLoginResponse == null)
            return null;
        return clsLoginResponse.getFeedbackStatus();
    }


    public static ArrayList<ClsTeacher> getTeacherList(String json) throws JSONException {
        if (Utility.isValueNull(json))
            return null;
        ArrayList<ClsTeacher> clsTeachers = new ArrayList<>();

        JSONArray jsonArray = new JSONArray(json);
        for (int i = 0; i < jsonArray.length(); i++) {
            clsTeachers.add(new Gson().fromJson(jsonArray.get(i).toString(), ClsTeacher.class));
        }


        return clsTeachers;
    }

    public static ArrayList<ClsFeedback> getFeedBackData(String json) throws JSONException {
        if (Utility.isValueNull(json))
            return null;

        ArrayList<ClsFeedback> clsFeedbackArrayList = new ArrayList<>();

        JSONArray jsonArray = new JSONArray(json);
        for (int i = 0; i < jsonArray.length(); i++) {
            clsFeedbackArrayList.add(new Gson().fromJson(jsonArray.get(i).toString(), ClsFeedback.class));
        }
        return clsFeedbackArrayList;
    }


    public static void showPopup(ArrayList<String> items, View view, final OnPopupMenuClick onPopupMenuClick) {
        if (view == null || items == null || items.size() == 0)
            return;

        PopupMenu popupMenu = new PopupMenu(view.getContext(), view);
        for (int i = 0; i < items.size(); i++) {
            Menu menu = popupMenu.getMenu();
            menu.add(Menu.NONE, i, i, items.get(i));
        }

        popupMenu.setOnMenuItemClickListener(new PopupMenu.OnMenuItemClickListener() {
            @Override
            public boolean onMenuItemClick(MenuItem menuItem) {
                if (onPopupMenuClick != null)
                    onPopupMenuClick.onPopupMenuItemClick(menuItem.getItemId(), menuItem.getTitle().toString());
                return false;
            }
        });
        popupMenu.show();
    }

    public static int getRating(String txt) {
        if (txt.equalsIgnoreCase("Excellent"))
            return 5;

        if (txt.equalsIgnoreCase("Good"))
            return 4;

        if (txt.equalsIgnoreCase("Average"))
            return 3;

        if (txt.equalsIgnoreCase("Poor"))
            return 2;

        if (txt.equalsIgnoreCase("Very Poor"))
            return 1;

        return 1;
    }

    public static void openLink(Context context, String url) {
        Intent browserIntent = new Intent(Intent.ACTION_VIEW, Uri.parse(url));
        context.startActivity(browserIntent);
    }

    public static String getUniqueId() {
        return UUID.randomUUID().toString();
    }

    public static void setNotificationDeviceId() {
        if (!Utility.isInternetConnectionAvailable(MerlinApp.CONTEXT)) {
            return;
        }

        Call<String> call = RetrofitInterface.callToMethodSecondServer().notification(getNotificationMap());
        call.enqueue(new Callback<String>() {
            @Override
            public void onResponse(Call<String> call, Response<String> response) {

                if (response != null && response.isSuccessful()) {
                    String res = response.body();

                } else {
                }

            }

            @Override
            public void onFailure(Call<String> call, Throwable t) {


            }
        });
    }

    private static HashMap<String, Object> getNotificationMap() {
        HashMap<String, Object> map = new HashMap<>();
        map.put("notificationToken", Utility.getStringValue(DEVICE_TOKEN));
        map.put("deviceNumber", getUserMobileNo() + "||" + getUniqueId());
        map.put("userId", getUserId());

        return map;
    }
}


