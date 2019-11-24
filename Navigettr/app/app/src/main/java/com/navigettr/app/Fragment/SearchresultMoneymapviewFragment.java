package com.navigettr.app.Fragment;


import android.Manifest;
import android.annotation.SuppressLint;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.location.Location;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AlertDialog;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.location.LocationListener;
import com.google.android.gms.location.LocationRequest;
import com.google.android.gms.location.LocationServices;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;
import com.navigettr.app.Barcode.FullScannerFragment;
import com.navigettr.app.Constant.Constant;
import com.navigettr.app.Constant.PreferenceUtils;
import com.navigettr.app.MarkerCallback;
import com.navigettr.app.Model.SearchResultMoneyModel;
import com.navigettr.app.R;
import com.squareup.picasso.Picasso;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.Locale;

public class SearchresultMoneymapviewFragment extends Fragment implements OnMapReadyCallback, View.OnClickListener, LocationListener, GoogleApiClient.ConnectionCallbacks, GoogleApiClient.OnConnectionFailedListener {

    private SupportMapFragment map_fragment;
    private GoogleMap mMap;
    private int total_providers;
    private ImageView ic_list, ic_googlemap;
    private FragmentManager fragmentManager;
    private TextView tv_available_providers;
    private GoogleApiClient mGoogleApiClient;
    private LocationRequest mLocationRequest;
    private Location loc1, loc2;
    private String current_day, localTime, workday;
    private List<SearchResultMoneyModel.Providers.WorkTime> workTime_List = new ArrayList<>();
    private SearchresultMoneymapviewFragment.MarkerInfoWindowAdapter markerInfoWindowAdapter;
    private boolean mapOpen = false;
    private Calendar calendar;
    private DateFormat date;
    private Date date_currentTime, date_startTime, date_endTime;
    private SearchResultMoneyModel.Providers providers;
    private PreferenceUtils preferenceUtils;

    public SearchresultMoneymapviewFragment() {
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        View view = inflater.inflate(R.layout.fragment_searchresultmoneymapview, container, false);

        preferenceUtils = new PreferenceUtils(getActivity());
        calendar = Calendar.getInstance();
        Date date1 = calendar.getTime();
        current_day = new SimpleDateFormat("EEEE", Locale.ENGLISH).format(date1.getTime());
        date = new SimpleDateFormat("HH:mm:ss");
        localTime = date.format(date1);
        try {
            date_currentTime = date.parse(localTime);
        } catch (ParseException e) {
            e.printStackTrace();
        }
        Bundle bundle = getArguments();
        if (bundle != null) {
            total_providers = bundle.getInt("total_providers");
            providers = (SearchResultMoneyModel.Providers) bundle.getSerializable("providers");
        }
        preferenceUtils.setPartnerId(providers.getPartnerId());
        for (int k = 0; k < providers.getWorkTime().size(); k++) {
            final SearchResultMoneyModel.Providers.WorkTime workTime = providers.getWorkTime().get(k);
            workday = workTime.getWorkDay();
            if (current_day.equals(workday)) {
                preferenceUtils.setLocationId(workTime.getLocationId());
            }
        }
        loc1 = new Location("");
        loc2 = new Location("");
        map_fragment = (SupportMapFragment) getChildFragmentManager().findFragmentById(R.id.map_fragment);
        map_fragment.getMapAsync(this);
        ic_list = view.findViewById(R.id.ic_list);
        tv_available_providers = view.findViewById(R.id.tv_available_providers);
        ic_googlemap = view.findViewById(R.id.ic_googlemap);
        //tv_available_providers.setText(total_providers + " " + getResources().getString(R.string.providers));
        fragmentManager = getActivity().getSupportFragmentManager();

        ic_list.setOnClickListener(this);
        ic_googlemap.setOnClickListener(this);
        return view;
    }

    @Override
    public void onMapReady(GoogleMap googleMap) {
        mMap = googleMap;

        LatLng location = new LatLng(providers.getLatitude(), providers.getLongitude());
        mMap.addMarker(new MarkerOptions().position(location));
        //mMap.getUiSettings().setMapToolbarEnabled(true);
        mMap.moveCamera(CameraUpdateFactory.newLatLngZoom(location, 13.0f));
        markerInfoWindowAdapter = new MarkerInfoWindowAdapter(providers);
        mMap.setInfoWindowAdapter(markerInfoWindowAdapter);

        mMap.setOnMarkerClickListener(new GoogleMap.OnMarkerClickListener() {
            @Override
            public boolean onMarkerClick(Marker marker) {
                if (!mapOpen) {
                    marker.showInfoWindow();
                    mMap.getUiSettings().setMapToolbarEnabled(true);
                    //mMap.getUiSettings().setZoomControlsEnabled(true);
                    mapOpen = true;
                } else {
                    marker.hideInfoWindow();
                    mapOpen = false;
                }
                return true;
            }
        });
    }

    public class MarkerInfoWindowAdapter implements GoogleMap.InfoWindowAdapter {
        private SearchResultMoneyModel.Providers providersModel;

        public MarkerInfoWindowAdapter(SearchResultMoneyModel.Providers providers) {
            this.providersModel = providers;
        }

        @Override
        public View getInfoWindow(Marker arg0) {
            return null;
        }

        @Override
        public View getInfoContents(Marker arg) {
            TextView tv_Brandname, tv_Convert_Currency, tv_Status;
            ImageView iv_Brand_Logo;

            LayoutInflater inflater = (LayoutInflater) getActivity().getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            View v = inflater.inflate(R.layout.custom_infowindow, null);
            tv_Brandname = v.findViewById(R.id.tv_Brandname);
            tv_Convert_Currency = v.findViewById(R.id.tv_Convert_Currency);
            tv_Status = v.findViewById(R.id.tv_Status);
            iv_Brand_Logo = v.findViewById(R.id.iv_Brand_Logo);

            tv_Brandname.setText(providersModel.getPartnerName());
            tv_Convert_Currency.setText("1 " + preferenceUtils.getFromCurrency() + " = " + providersModel.getIndicative() + " " + preferenceUtils.getToCurrency());
            Picasso.get().load(providersModel.getPartnerLogoPath())
                    .placeholder(R.drawable.splash_icon)
                    .into(iv_Brand_Logo, new MarkerCallback(arg));

            if (providersModel.getWorkTime() != null) {
                workTime_List = providersModel.getWorkTime();

                for (int k = 0; k < workTime_List.size(); k++) {
                    final SearchResultMoneyModel.Providers.WorkTime workTime = workTime_List.get(k);
                    workday = workTime.getWorkDay();
                    if (current_day.equals(workday)) {
                        try {
                            date_startTime = date.parse(workTime.getWorkStartTime());
                            date_endTime = date.parse(workTime.getWorkEndTime());

                            calendar.setTime(date_startTime);
                            calendar.add(Calendar.HOUR, -1);
                            Date oneHourBack_startTime = calendar.getTime();

                            calendar.setTime(date_endTime);
                            calendar.add(Calendar.HOUR, -1);
                            Date oneHourBack_endTime = calendar.getTime();

                            if (date_currentTime.after(oneHourBack_startTime) && date_currentTime.before(date_startTime)) {
                                tv_Status.setText(getResources().getString(R.string.opening_soon));
                                tv_Status.setTextColor(getResources().getColor(R.color.color_green));
                            } else if (date_currentTime.after(date_startTime) && date_currentTime.before(oneHourBack_endTime)) {
                                tv_Status.setText(getResources().getString(R.string.open_now));
                                tv_Status.setTextColor(getResources().getColor(R.color.color_green));
                            } else if (date_currentTime.after(oneHourBack_endTime) && date_currentTime.before(date_endTime)) {
                                tv_Status.setText(getResources().getString(R.string.closing_soon));
                                tv_Status.setTextColor(getResources().getColor(R.color.color_red));
                            } else if (date_currentTime.before(oneHourBack_startTime) || date_currentTime.after(date_endTime)) {
                                tv_Status.setText(getResources().getString(R.string.close_now));
                                tv_Status.setTextColor(getResources().getColor(R.color.color_red));
                            } else {
                                tv_Status.setText("N/A");
                                tv_Status.setTextColor(getResources().getColor(R.color.color_dark_grey));
                            }
                        } catch (ParseException e) {
                            e.printStackTrace();
                        }
                    }
                }
            }
            return v;
        }
    }

    @Override
    public void onClick(View v) {
        String url = "http://maps.google.com/maps?q=" + providers.getAddressLine1() + preferenceUtils.getSearchedLocation();
        Intent intent = new Intent(android.content.Intent.ACTION_VIEW,  Uri.parse(url));
        startActivity(intent);
    }

    protected synchronized void buildGoogleApiClient() {

        mGoogleApiClient = new GoogleApiClient.Builder(getActivity())
                .addConnectionCallbacks(this)
                .addOnConnectionFailedListener(this)
                .addApi(LocationServices.API)
                .build();
        mGoogleApiClient.connect();
    }

    @Override
    public void onLocationChanged(Location location) {
        loc1.setLatitude(providers.getLatitude());
        loc1.setLongitude(providers.getLongitude());
        /*loc1.setLatitude(21.1880);
        loc1.setLongitude(72.8150);*/
        loc2.setLatitude(location.getLatitude());
        loc2.setLongitude(location.getLongitude());
        float distanceInMeters = loc1.distanceTo(loc2);
        if (distanceInMeters < 4000.0) {
            if (!preferenceUtils.getIsPopup())
                LocationAlertdialog(getActivity());
        }
        Constant.getToast(getActivity(), location.getLatitude() + ", " + location.getLongitude() + ", " + String.valueOf(distanceInMeters));
    }

    @SuppressWarnings("deprecation")
    @SuppressLint("RestrictedApi")
    @Override
    public void onConnected(@Nullable Bundle bundle) {
        mLocationRequest = new LocationRequest();
        mLocationRequest.setPriority(LocationRequest.PRIORITY_HIGH_ACCURACY);
        mLocationRequest.setInterval(30000);
        if (ContextCompat.checkSelfPermission(getActivity(), Manifest.permission.ACCESS_FINE_LOCATION) == PackageManager.PERMISSION_GRANTED) {
            LocationServices.FusedLocationApi.requestLocationUpdates(mGoogleApiClient, mLocationRequest, this);
        }
    }

    @Override
    public void onConnectionSuspended(int i) {

    }

    @Override
    public void onConnectionFailed(@NonNull ConnectionResult connectionResult) {

    }

    public void LocationAlertdialog(Context context) {
        final AlertDialog.Builder builder = new AlertDialog.Builder(context);
        LayoutInflater layoutInflater = LayoutInflater.from(context);
        View view = layoutInflater.inflate(R.layout.alert_location, null);
        builder.setView(view);
        final AlertDialog alertDialog = builder.create();
        alertDialog.setCancelable(false);
        alertDialog.show();
        final Button btnok = (Button) view.findViewById(R.id.btn_ok);
        btnok.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                alertDialog.dismiss();
                /*Intent i = new Intent(getActivity(), FullScannerFragmentActivity.class);
                startActivity(i);*/
                FullScannerFragment fullScannerFragment = new FullScannerFragment();
                fragmentManager.beginTransaction().add(R.id.frame_content, fullScannerFragment).addToBackStack(null).commit();
            }
        });
    }

    @Override
    public void onResume() {
        super.onResume();
        buildGoogleApiClient();
    }

    @Override
    public void onPause() {
        super.onPause();
        if (mGoogleApiClient != null)
            mGoogleApiClient.disconnect();
    }

    @Override
    public void onStop() {
        super.onStop();
        if (mGoogleApiClient != null)
            mGoogleApiClient.disconnect();
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        preferenceUtils.clearEditorPopup();
    }
}
