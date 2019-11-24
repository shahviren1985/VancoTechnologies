package com.navigettr.app.Fragment;


import android.content.Context;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.SupportMapFragment;
import com.google.android.gms.maps.model.BitmapDescriptorFactory;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;
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

public class MapViewAllMoneyFragment extends Fragment implements OnMapReadyCallback, View.OnClickListener{

    private SupportMapFragment map_fragment;
    private GoogleMap mMap;
    private double latitude, longitude;
    private int total_providers;
    private String  current_day, localTime, workday;
    private TextView tv_available_providers;
    private ImageView ic_list;
    private FragmentManager fragmentManager;
    private List<SearchResultMoneyModel.Providers> latlong_List = new ArrayList<>();
    private List<SearchResultMoneyModel.Providers.WorkTime> workTime_List = new ArrayList<>();
    private MarkerInfoWindowAdapter markerInfoWindowAdapter;
    private boolean mapOpen = false;
    private Calendar calendar;
    private DateFormat date;
    private Date date_currentTime, date_startTime, date_endTime;
    private LatLng curr_location;
    private PreferenceUtils preferenceUtils;
    private LayoutInflater layoutInflater;

    public MapViewAllMoneyFragment() {
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_map_view_all_money, container, false);
        preferenceUtils = new PreferenceUtils(getActivity());
        layoutInflater = (LayoutInflater) getActivity().getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        calendar= Calendar.getInstance();
        Date date1 = calendar.getTime();
        current_day= new SimpleDateFormat("EEEE", Locale.ENGLISH).format(date1.getTime());
        date= new SimpleDateFormat("HH:mm:ss");
        localTime = date.format(date1);
        try {
            date_currentTime = date.parse(localTime);
        } catch (ParseException e) {
            e.printStackTrace();
        }

        Bundle bundle = getArguments();
        if (bundle != null) {
            total_providers = bundle.getInt("total_providers");
            latlong_List = (List<SearchResultMoneyModel.Providers>) bundle.getSerializable("LatLng_List");
        }
        ic_list = view.findViewById(R.id.ic_list);
        tv_available_providers = view.findViewById(R.id.tv_available_providers);
        if (preferenceUtils.getFragmentName().equals("MoneyTransfer")) {
            tv_available_providers.setText(total_providers + " " + getResources().getString(R.string.money_providers));
        }
        else if (preferenceUtils.getFragmentName().equals("Forex")) {
            tv_available_providers.setText(total_providers + " " + getResources().getString(R.string.forex_providers));
        }
        else if (preferenceUtils.getFragmentName().equals("TravelCard")) {
            tv_available_providers.setText(total_providers + " " + getResources().getString(R.string.travelcard_providers));
        }
        map_fragment = (SupportMapFragment) getChildFragmentManager().findFragmentById(R.id.map_fragment);
        map_fragment.getMapAsync(this);
        fragmentManager = getActivity().getSupportFragmentManager();

        ic_list.setOnClickListener(this);

        return view;
    }

    @Override
    public void onMapReady(GoogleMap googleMap) {
        mMap = googleMap;

        curr_location = new LatLng(Double.parseDouble(preferenceUtils.getLat()), Double.parseDouble(preferenceUtils.getLng()));
        mMap.addMarker(new MarkerOptions().position(curr_location).title("Current Location"));
        mMap.moveCamera(CameraUpdateFactory.newLatLngZoom(curr_location, 12.8f));

        for (int i = 0; i < latlong_List.size(); i++) {
            latitude = latlong_List.get(i).getLatitude();
            longitude = latlong_List.get(i).getLongitude();
            LatLng location = new LatLng(latitude, longitude);
            if (latlong_List.get(i).getDistance() >= 0.0 && latlong_List.get(i).getDistance() < 3.0)
                mMap.addMarker(new MarkerOptions().position(location).title("")
                        .icon(BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_VIOLET)));
            else if (latlong_List.get(i).getDistance() >= 3.0 && latlong_List.get(i).getDistance() < 6.0)
                mMap.addMarker(new MarkerOptions().position(location).title("")
                        .icon(BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_AZURE)));
            else if (latlong_List.get(i).getDistance() >= 6.0)
                mMap.addMarker(new MarkerOptions().position(location).title("")
                        .icon(BitmapDescriptorFactory.defaultMarker(BitmapDescriptorFactory.HUE_MAGENTA)));
        }

        mMap.setOnMarkerClickListener(new GoogleMap.OnMarkerClickListener() {
            @Override
            public boolean onMarkerClick(Marker marker) {
                if (!mapOpen) {
                    if (marker.getTitle().equals("Current Location")) {
                        markerInfoWindowAdapter = new MarkerInfoWindowAdapter();
                        mMap.setInfoWindowAdapter(markerInfoWindowAdapter);
                        marker.showInfoWindow();
                    }
                    else {
                        markerInfoWindowAdapter = new MarkerInfoWindowAdapter(latlong_List);
                        mMap.setInfoWindowAdapter(markerInfoWindowAdapter);
                        marker.showInfoWindow();
                    }
                    mapOpen = true;
                }
                else {
                    marker.hideInfoWindow();
                    mapOpen = false;
                }
                return true;
            }
        });
    }

    @Override
    public void onClick(View v) {
        fragmentManager.popBackStackImmediate();
    }

    public class MarkerInfoWindowAdapter implements GoogleMap.InfoWindowAdapter {
        private List<SearchResultMoneyModel.Providers> provider_List;
        private SearchResultMoneyModel.Providers providers;
        private View v;

        public MarkerInfoWindowAdapter(List<SearchResultMoneyModel.Providers> provider_List) {
            this.provider_List = provider_List;
        }
        public MarkerInfoWindowAdapter() {
        }
        @Override
        public View getInfoWindow(Marker arg0) {
            return null;
        }

        @Override
        public View getInfoContents(Marker arg) {
            TextView tv_Brandname, tv_Convert_Currency, tv_Status;
            ImageView iv_Brand_Logo;

            if (provider_List == null)
                v =  layoutInflater.inflate(R.layout.custom_infowindow_currentlocation, null);
            else {
                v = layoutInflater.inflate(R.layout.custom_infowindow, null);
                tv_Brandname = v.findViewById(R.id.tv_Brandname);
                tv_Convert_Currency = v.findViewById(R.id.tv_Convert_Currency);
                tv_Status = v.findViewById(R.id.tv_Status);
                iv_Brand_Logo = v.findViewById(R.id.iv_Brand_Logo);

                LatLng latLng = arg.getPosition();

                for (int i = 0; i < provider_List.size(); i++) {
                    providers = provider_List.get(i);
                    if (latLng.latitude == providers.getLatitude() && latLng.longitude == providers.getLongitude()) {
                        tv_Brandname.setText(providers.getPartnerName());
                        tv_Convert_Currency.setText("1 " + preferenceUtils.getFromCurrency() + " = " + providers.getIndicative() + " " + preferenceUtils.getToCurrency());
                        Picasso.get().load(providers.getPartnerLogoPath())
                                .placeholder(R.drawable.splash_icon)
                                .into(iv_Brand_Logo, new MarkerCallback(arg));

                        if (providers.getWorkTime() != null) {
                            workTime_List = providers.getWorkTime();

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
                                        }
                                    } catch (ParseException e) {
                                        e.printStackTrace();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return v;
        }
    }
}
