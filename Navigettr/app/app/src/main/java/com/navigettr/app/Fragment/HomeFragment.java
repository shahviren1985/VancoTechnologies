package com.navigettr.app.Fragment;


import android.content.Intent;
import android.location.Address;
import android.location.Geocoder;
import android.os.Bundle;
import android.support.design.widget.TabLayout;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;
import android.support.v4.view.ViewPager;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.google.android.gms.common.GooglePlayServicesNotAvailableException;
import com.google.android.gms.common.GooglePlayServicesRepairableException;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.location.places.Place;
import com.google.android.gms.location.places.ui.PlaceAutocomplete;
import com.google.android.gms.maps.model.LatLng;
import com.navigettr.app.Constant.PreferenceUtils;
import com.navigettr.app.R;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;
import java.util.Objects;

import static android.app.Activity.RESULT_CANCELED;
import static android.app.Activity.RESULT_OK;

public class HomeFragment extends Fragment implements View.OnClickListener{

    private TabLayout tl_tabview;
    private ViewPager viewpager;
    int[] tabicons = {R.drawable.ic_moneytransfer_blue, R.drawable.ic_foreignexchange_black, R.drawable.ic_travelcard_black};
    private TextView tv_locationdata;
    private ImageView ic_changelocation;
    Geocoder geocoder;
    List<Address> addresses;
    private PreferenceUtils preferenceUtils;
    private FragmentManager fragmentManager;
    private GoogleApiClient mGoogleApiClient;
    private int PLACE_PICKER_REQUEST = 1;

    public HomeFragment() {
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_home, container, false);

        fragmentManager = getActivity().getSupportFragmentManager();
        preferenceUtils = new PreferenceUtils(getActivity());
        tl_tabview = (TabLayout)view.findViewById(R.id.tl_tabview);
        viewpager = (ViewPager)view.findViewById(R.id.viewpager);
        geocoder = new Geocoder(getActivity(), Locale.getDefault());
        tv_locationdata = (TextView) view.findViewById(R.id.tv_locationdata);
        ic_changelocation =  view.findViewById(R.id.ic_changelocation);
        ic_changelocation.setOnClickListener(this);
        //buildGoogleApiClient();

        setUpViewpager(viewpager);
        tl_tabview.setupWithViewPager(viewpager);

        setUpIcon();
        getLocationData();

        return view;
    }

    private void setUpIcon(){
        tl_tabview.getTabAt(0).setIcon(tabicons[0]);
        tl_tabview.getTabAt(1).setIcon(tabicons[1]);
        tl_tabview.getTabAt(2).setIcon(tabicons[2]);
        tl_tabview.addOnTabSelectedListener(new TabLayout.OnTabSelectedListener() {
            @Override
            public void onTabSelected(TabLayout.Tab tab) {
              switch (tab.getPosition()){
                  case 0:
                      tab.setIcon(R.drawable.ic_moneytransfer_blue);
                      break;
                  case 1:
                      tab.setIcon(R.drawable.ic_foreignexchange_blue);
                      break;
                  case 2:
                      tab.setIcon(R.drawable.ic_travelcard_blue);
                      break;
              }
            }

            @Override
            public void onTabUnselected(TabLayout.Tab tab) {
                switch (tab.getPosition()){
                    case 0:
                        tab.setIcon(R.drawable.ic_moneytransfer_black);
                        break;
                    case 1:
                        tab.setIcon(R.drawable.ic_foreignexchange_black);
                        break;
                    case 2:
                        tab.setIcon(R.drawable.ic_travelcard_black);
                        break;
                }
            }

            @Override
            public void onTabReselected(TabLayout.Tab tab) {
            }
        });

    }

    private void setUpViewpager(ViewPager viewpager){
        ViewPageAdapter viewPageAdapter = new ViewPageAdapter(getChildFragmentManager());
        viewPageAdapter.addFragment(new MoneytransferFragment(), getResources().getString(R.string.frg_money));
        viewPageAdapter.addFragment(new ForeignexchangeFragment(),  getResources().getString(R.string.frg_foreign));
        viewPageAdapter.addFragment(new TravelcardFragment(),  getResources().getString(R.string.frg_travel));
        viewpager.setAdapter(viewPageAdapter);

    }

    private void getLocationData(){
        String city = "", state = "", country = "", postalCode = "", address = "";
        try {
            addresses = geocoder.getFromLocation(Double.parseDouble(preferenceUtils.getLat()), Double.parseDouble(preferenceUtils.getLng()), 1);
            city = addresses.get(0).getLocality();
            state = addresses.get(0).getAdminArea();
            country = addresses.get(0).getCountryName();
            postalCode = addresses.get(0).getPostalCode();
            address = addresses.get(0).getAddressLine(0);
            preferenceUtils.setCity(city);
            preferenceUtils.setState(state);
            preferenceUtils.setCountry(country);
            preferenceUtils.setZipcode(postalCode);
            tv_locationdata.setText(address);
            preferenceUtils.setSearchedLocation(tv_locationdata.getText().toString());
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onClick(View v) {
        //displayPlacePicker();
        Intent intent = null;
        try {
            intent = new PlaceAutocomplete
                    .IntentBuilder(PlaceAutocomplete.MODE_FULLSCREEN)
                    .build(Objects.requireNonNull(getActivity()));
        } catch (GooglePlayServicesRepairableException e) {
            e.printStackTrace();
        } catch (GooglePlayServicesNotAvailableException e) {
            e.printStackTrace();
        }
        startActivityForResult(intent, 1);
    }
    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (requestCode == 1) {
            if (resultCode == RESULT_OK) {
                Place place = PlaceAutocomplete.getPlace(getActivity(), data);
                LatLng latLng = place.getLatLng();
                preferenceUtils.clearEditor();
                preferenceUtils.setLat(String.valueOf(latLng.latitude));
                preferenceUtils.setLng(String.valueOf(latLng.longitude));
                getLocationData();

            } else if (resultCode == PlaceAutocomplete.RESULT_ERROR) {
                Status status = PlaceAutocomplete.getStatus(getActivity(), data);
                Log.e("Tag", status.getStatusMessage());

            } else if (resultCode == RESULT_CANCELED) {
            }
        }
    }

    /*private void buildGoogleApiClient(){
        mGoogleApiClient = new GoogleApiClient
                .Builder( getActivity() )
                .enableAutoManage( getActivity(), 0, this )
                .addApi( Places.GEO_DATA_API )
                .addApi( Places.PLACE_DETECTION_API )
                .addConnectionCallbacks( this )
                .addOnConnectionFailedListener( this )
                .build();
        mGoogleApiClient.connect();
    }*/

  /*  @Override
    public void onConnectionFailed(@NonNull ConnectionResult connectionResult) {

    }

    @Override
    public void onConnected(@Nullable Bundle bundle) {

    }

    @Override
    public void onConnectionSuspended(int i) {

    }*/

    /*private void displayPlacePicker() {
        if( mGoogleApiClient == null || !mGoogleApiClient.isConnected() )
            return;

        PlacePicker.IntentBuilder builder = new PlacePicker.IntentBuilder();

        try {
            startActivityForResult( builder.build(Objects.requireNonNull(getActivity())), PLACE_PICKER_REQUEST );
        } catch ( GooglePlayServicesRepairableException e ) {
            Log.d( "PlacesAPI Demo", "GooglePlayServicesRepairableException thrown" );
        } catch ( GooglePlayServicesNotAvailableException e ) {
            Log.d( "PlacesAPI Demo", "GooglePlayServicesNotAvailableException thrown" );
        }
    }*/

   /* @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        if( requestCode == PLACE_PICKER_REQUEST && resultCode == RESULT_OK ) {
            Place place = PlacePicker.getPlace(getActivity(), data);
            LatLng latLng = place.getLatLng();
            preferenceUtils.clearEditor();
            preferenceUtils.setLat(String.valueOf(latLng.latitude));
            preferenceUtils.setLng(String.valueOf(latLng.longitude));
            getLocationData();
        }
        else if (resultCode == PlacePicker.RESULT_ERROR){
            Status status = PlacePicker.getStatus(getActivity(), data);
            Log.e("Tag", status.getStatusMessage());
        }
    }*/

    class ViewPageAdapter extends FragmentPagerAdapter {

        private final List<Fragment> mfragmentList = new ArrayList<>();
        private final List<String> mFragmentTitleList = new ArrayList<>();

        public ViewPageAdapter(FragmentManager fm) {
            super(fm);
        }

        @Override
        public Fragment getItem(int position) {
            return mfragmentList.get(position);
        }

        @Override
        public int getCount() {
            return mfragmentList.size();
        }

        public void addFragment(Fragment fragment, String title){
            mfragmentList.add(fragment);
            mFragmentTitleList.add(title);
        }
        public CharSequence getPageTitle(int position) {
            return mFragmentTitleList.get(position);
        }
    }

    /*@Override
    public void onStop() {
        super.onStop();
        if( mGoogleApiClient != null && mGoogleApiClient.isConnected() ) {
            mGoogleApiClient.disconnect();
        }
    }*/
}
