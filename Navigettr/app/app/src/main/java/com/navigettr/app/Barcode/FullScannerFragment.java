package com.navigettr.app.Barcode;

import android.content.Context;
import android.media.Ringtone;
import android.media.RingtoneManager;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.DialogFragment;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v7.app.AlertDialog;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import com.google.zxing.BarcodeFormat;
import com.google.zxing.Result;
import com.navigettr.app.Constant.PreferenceUtils;
import com.navigettr.app.Fragment.SuccessqrcodeFragment;
import com.navigettr.app.Model.ScanQRCodeModel;
import com.navigettr.app.ProgressDialogLoader;
import com.navigettr.app.R;
import com.navigettr.app.WebServices.ApiClient;
import com.navigettr.app.WebServices.ApiInterface;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import me.dm7.barcodescanner.zxing.ZXingScannerView;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class FullScannerFragment extends Fragment implements MessageDialogFragment.MessageDialogListener,
        ZXingScannerView.ResultHandler, FormatSelectorDialogFragment.FormatSelectorDialogListener {
    private static final String SELECTED_FORMATS = "SELECTED_FORMATS";
    private static final String CAMERA_ID = "CAMERA_ID";
    private ZXingScannerView mScannerView;
    private ArrayList<Integer> mSelectedIndices;
    private int mCameraId = -1;
    private ApiInterface apiInterface;
    private PreferenceUtils preferenceUtils;
    private Calendar calendar;
    private DateFormat date;
    private String localTime;
    private FragmentManager fragmentManager;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle state) {
        preferenceUtils = new PreferenceUtils(getActivity());
        fragmentManager = getActivity().getSupportFragmentManager();

        calendar = Calendar.getInstance();
        Date date1 = calendar.getTime();
        date = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        localTime = date.format(date1);

        mScannerView = new ZXingScannerView(getActivity());
        preferenceUtils.setIsPopup(true);
        if(state != null) {
            mSelectedIndices = state.getIntegerArrayList(SELECTED_FORMATS);
            mCameraId = state.getInt(CAMERA_ID, -1);
        } else {
            mSelectedIndices = null;
            mCameraId = -1;
        }
        setupFormats();
        return mScannerView;
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        getView().setFocusableInTouchMode(true);
        getView().requestFocus();
        getView().setOnKeyListener(new View.OnKeyListener() {
            @Override
            public boolean onKey(View v, int keyCode, KeyEvent event) {
                if (event.getAction() == KeyEvent.ACTION_DOWN) {
                    if (keyCode == KeyEvent.KEYCODE_BACK) {
                        LocationAlertdialog(getActivity());
                        return true;
                    }
                }
                return false;
            }
        });
    }

    @Override
    public void onCreate(Bundle state) {
        super.onCreate(state);
        setHasOptionsMenu(true);
    }

    @Override
    public void onResume() {
        super.onResume();
        mScannerView.setResultHandler(this);
        mScannerView.startCamera(mCameraId);
    }

    @Override
    public void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putIntegerArrayList(SELECTED_FORMATS, mSelectedIndices);
        outState.putInt(CAMERA_ID, mCameraId);
    }

    @Override
    public void handleResult(Result rawResult) {
        try {
            Uri notification = RingtoneManager.getDefaultUri(RingtoneManager.TYPE_NOTIFICATION);
            Ringtone r = RingtoneManager.getRingtone(getActivity().getApplicationContext(), notification);
            r.play();
        } catch (Exception e) {}
        getScanQRCode();
        //showMessageDialog("Contents = " + rawResult.getText() + ", Format = " + rawResult.getBarcodeFormat().toString());
    }

    public void showMessageDialog(String message) {
        DialogFragment fragment = MessageDialogFragment.newInstance("Scan Results", message, this);
        fragment.show(getActivity().getSupportFragmentManager(), "scan_results");
    }

    public void closeMessageDialog() {
        closeDialog("scan_results");
    }

    public void closeFormatsDialog() {
        closeDialog("format_selector");
    }

    public void closeDialog(String dialogName) {
        FragmentManager fragmentManager = getActivity().getSupportFragmentManager();
        DialogFragment fragment = (DialogFragment) fragmentManager.findFragmentByTag(dialogName);
        if(fragment != null) {
            fragment.dismiss();
        }
    }

    @Override
    public void onDialogPositiveClick(DialogFragment dialog) {
        mScannerView.resumeCameraPreview(this);
        /*Intent i = new Intent(getActivity(), RewardsActivity.class);
        startActivity(i);*/
    }

    @Override
    public void onFormatsSaved(ArrayList<Integer> selectedIndices) {
        mSelectedIndices = selectedIndices;
        setupFormats();
    }

    public void setupFormats() {
        List<BarcodeFormat> formats = new ArrayList<BarcodeFormat>();
        if(mSelectedIndices == null || mSelectedIndices.isEmpty()) {
            mSelectedIndices = new ArrayList<Integer>();
            for(int i = 0; i < ZXingScannerView.ALL_FORMATS.size(); i++) {
                mSelectedIndices.add(i);
            }
        }
        for(int index : mSelectedIndices) {
            formats.add(ZXingScannerView.ALL_FORMATS.get(index));
        }
        if(mScannerView != null) {
            mScannerView.setFormats(formats);
        }
    }

    @Override
    public void onPause() {
        super.onPause();
        mScannerView.stopCamera();
        closeMessageDialog();
        closeFormatsDialog();
    }

    public void getScanQRCode() {
        try {
            apiInterface = ApiClient.getClient().create(ApiInterface.class);
            ProgressDialogLoader.progressdialog_creation(getActivity(), "Scanned...");
            Call<ScanQRCodeModel> call = apiInterface.getScanQRCod(String.valueOf(preferenceUtils.getLoginUserId()),
                    String.valueOf(preferenceUtils.getPartnerId()),
                    String.valueOf(preferenceUtils.getLocationId()), String.valueOf(preferenceUtils.getamount()),
                    preferenceUtils.getFromCurrency(), preferenceUtils.getToCurrency(), localTime);
            call.enqueue(new Callback<ScanQRCodeModel>() {

                @Override
                public void onResponse(Call<ScanQRCodeModel> call, Response<ScanQRCodeModel> response) {

                    if (response.body() != null) {
                        //showMessageDialog(getResources().getString(R.string.msg_scanQRCode));
                        /*fragmentManager.popBackStackImmediate();
                        Intent i = new Intent(getActivity(), SuccessqrcodeActivity.class);
                        startActivity(i);*/
                        SuccessqrcodeFragment successqrcodeFragment = new SuccessqrcodeFragment();
                        fragmentManager.beginTransaction().add(R.id.frame_content, successqrcodeFragment).addToBackStack(null).commit();
                    }
                    ProgressDialogLoader.progressdialog_dismiss();
                }

                @Override
                public void onFailure(Call<ScanQRCodeModel> call, Throwable t) {
                    ProgressDialogLoader.progressdialog_dismiss();
                }
            });

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void LocationAlertdialog(Context context) {
        final AlertDialog.Builder builder = new AlertDialog.Builder(context);
        LayoutInflater layoutInflater = LayoutInflater.from(context);
        View view = layoutInflater.inflate(R.layout.alert_location, null);
        builder.setView(view);
        final AlertDialog alertDialog = builder.create();
        alertDialog.setCancelable(false);
        alertDialog.show();
        final TextView tv_text = view.findViewById(R.id.tv_msg);
        tv_text.setText(getResources().getString(R.string.reach_des_noQRCode));
        final Button btnok =  view.findViewById(R.id.btn_ok);
        btnok.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                alertDialog.dismiss();
                fragmentManager.popBackStackImmediate();
            }
        });
    }
}
