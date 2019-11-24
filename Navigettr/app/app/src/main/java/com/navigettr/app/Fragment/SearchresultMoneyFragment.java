package com.navigettr.app.Fragment;


import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v7.app.AlertDialog;
import android.support.v7.widget.DefaultItemAnimator;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.ProgressBar;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.google.gson.JsonObject;
import com.navigettr.app.Adapter.SearchResultMoneyAdapter;
import com.navigettr.app.Constant.Constant;
import com.navigettr.app.Constant.PreferenceUtils;
import com.navigettr.app.Model.SearchResultMoneyModel;
import com.navigettr.app.ProgressDialogLoader;
import com.navigettr.app.R;
import com.navigettr.app.WebServices.ApiClient;
import com.navigettr.app.WebServices.ApiInterface;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class SearchresultMoneyFragment extends Fragment implements View.OnClickListener {

    private RecyclerView rv_searchdata;
    private ImageView ic_all_location, ic_sort;
    private LinearLayout LL_filter, LL_sort, LL_bankData;
    private SearchResultMoneyAdapter searchResultMoneyAdapter;
    private List<SearchResultMoneyModel.Providers> providers_List = new ArrayList<>();
    private List<SearchResultMoneyModel.Providers> latlong_List = new ArrayList<>();
    private List<SearchResultMoneyModel.Providers> Bank_List = new ArrayList<>();
    private List<SearchResultMoneyModel.Providers> Temp_List = new ArrayList<>();
    private SearchResultMoneyModel searchResultMoneyModel;
    private String selected_buysell = "", selected_order = "";
    private TextView tv_available_providers, tv_msg_noData, tv_loadmore, tv_locationdata;
    private LinearLayoutManager layoutManager;
    private static final int PAGE_START = 1;
    private int TOTAL_PAGES, select_radius = 0, currentPage = PAGE_START;
    private ApiInterface apiInterface;
    private JsonObject obj, ServiceParams;
    private FragmentManager fragmentManager;
    private boolean up;
    private PreferenceUtils preferenceUtils;
    private ProgressBar loadmore_progressBar;

    public SearchresultMoneyFragment() {
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {

        View view = inflater.inflate(R.layout.fragment_searchresultmoney, container, false);
        ServiceParams = new JsonObject();
        obj = new JsonObject();
        preferenceUtils = new PreferenceUtils(getActivity());

        Bundle bundle = getArguments();
        if (bundle != null) {
            selected_buysell = bundle.getString("selected_BuySell");
            select_radius = bundle.getInt("selected_searchradius");
        }
        preferenceUtils.setSearchRadius(select_radius);
        rv_searchdata = view.findViewById(R.id.rv_searchdata);
        tv_available_providers = view.findViewById(R.id.tv_available_providers);
        ic_all_location = view.findViewById(R.id.ic_all_location);
        ic_sort = view.findViewById(R.id.ic_sort);
        LL_filter = view.findViewById(R.id.LL_filter);
        LL_sort = view.findViewById(R.id.LL_sort);
        LL_bankData = view.findViewById(R.id.LL_bankData);
        tv_msg_noData = view.findViewById(R.id.tv_msg_noData);
        tv_loadmore = view.findViewById(R.id.tv_loadmore);
        loadmore_progressBar = view.findViewById(R.id.loadmore_progressBar);
        tv_locationdata = view.findViewById(R.id.tv_locationdata);
        tv_locationdata.setText(preferenceUtils.getSearchedLocation());
        LL_bankData.setVisibility(View.GONE);
        fragmentManager = getActivity().getSupportFragmentManager();

        if (selected_buysell.equals("Buy")) {
            selected_order = getResources().getString(R.string.str_ASC);
            ic_sort.setImageResource(R.drawable.ic_arrow_upward);
            up = true;
        } else {
            selected_order = getResources().getString(R.string.str_DESC);
            ic_sort.setImageResource(R.drawable.ic_arrow_downward);
            up = false;
        }

        ic_all_location.setOnClickListener(this);
        LL_filter.setOnClickListener(this);
        LL_sort.setOnClickListener(this);
        tv_loadmore.setOnClickListener(this);

        getSearchResultFirstPage();
        layoutManager = new LinearLayoutManager(getActivity());
        rv_searchdata.setLayoutManager(layoutManager);
        searchResultMoneyAdapter = new SearchResultMoneyAdapter(getActivity());
        rv_searchdata.setItemAnimator(new DefaultItemAnimator());
        rv_searchdata.setHasFixedSize(true);
        rv_searchdata.setAdapter(searchResultMoneyAdapter);

        return view;
    }

    public void getSearchResultFirstPage() {
        createJsonObject();
        try {
            apiInterface = ApiClient.getClient().create(ApiInterface.class);
            ProgressDialogLoader.progressdialog_creation(getActivity(), getResources().getString(R.string.apiCall_text));
            Call<SearchResultMoneyModel> call = apiInterface.getSearchResult(obj);
            call.enqueue(new Callback<SearchResultMoneyModel>() {

                @Override
                public void onResponse(Call<SearchResultMoneyModel> call, Response<SearchResultMoneyModel> response) {

                    if (response.body() != null) {
                        searchResultMoneyModel = response.body();
                        providers_List = searchResultMoneyModel.getProviders();
                        latlong_List.addAll(providers_List);
                        searchResultMoneyAdapter.getTotalProviders(searchResultMoneyModel.getTotalCount());
                        int total_count = searchResultMoneyModel.getTotalCount();
                        double res = total_count / 50.0;
                        double total;
                        if (res > 1.0) {
                            total = (double) (res + 1.0);
                            TOTAL_PAGES = (int) total;
                        }
                        PreferenceUtils preferenceUtils = new PreferenceUtils(getActivity());
                        preferenceUtils.setTotalProviders(total_count);
                        for (int i = 0; i < providers_List.size(); i++) {
                            if (Bank_List.size() == 0)
                                Bank_List.add(providers_List.get(i));
                            else {
                                if (!(containBankName(providers_List.get(i).getPartnerName(), Bank_List))) {
                                    Bank_List.add(providers_List.get(i));
                                }
                            }
                        }
                        Collections.sort(Bank_List, new Comparator<SearchResultMoneyModel.Providers>() {
                            @Override
                            public int compare(SearchResultMoneyModel.Providers provider1, SearchResultMoneyModel.Providers provider2) {
                                if (selected_order.equals(getResources().getString(R.string.str_DESC)))
                                    return Double.compare(provider2.getIndicative(), provider1.getIndicative());
                                else
                                    return Double.compare(provider1.getIndicative(), provider2.getIndicative());
                            }
                        });

                        if (preferenceUtils.getFragmentName().equals("MoneyTransfer")) {
                            tv_available_providers.setText(Bank_List.size() + " " + getResources().getString(R.string.money_providers));
                        }
                        else if (preferenceUtils.getFragmentName().equals("Forex")) {
                            tv_available_providers.setText(Bank_List.size() + " " + getResources().getString(R.string.forex_providers));
                        }
                        else if (preferenceUtils.getFragmentName().equals("TravelCard")) {
                            tv_available_providers.setText(Bank_List.size() + " " + getResources().getString(R.string.travelcard_providers));
                        }

                        searchResultMoneyAdapter.addAll(Bank_List, providers_List);
                        if (Bank_List.size() == 0) {
                            tv_available_providers.setVisibility(View.GONE);
                            ic_all_location.setVisibility(View.GONE);
                            tv_msg_noData.setVisibility(View.VISIBLE);
                            tv_msg_noData.setText(getResources().getString(R.string.msg_noData));
                        }

                        if (currentPage < TOTAL_PAGES)
                            tv_loadmore.setVisibility(View.VISIBLE);
                        else
                            tv_loadmore.setVisibility(View.GONE);
                    } else {
                        Toast.makeText(getActivity(), "No Data Fetch", Toast.LENGTH_LONG).show();
                    }
                    ProgressDialogLoader.progressdialog_dismiss();
                }

                @Override
                public void onFailure(Call<SearchResultMoneyModel> call, Throwable t) {
                    ProgressDialogLoader.progressdialog_dismiss();
                }
            });

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public boolean containBankName(
            String name, List<SearchResultMoneyModel.Providers> list) {

        for (SearchResultMoneyModel.Providers providers : list) {
            if (providers.getPartnerName().equals(name)) {
                return true;
            }
        }
        return false;
    }

    public void getSearchResultNextPage() {
        currentPage += 1;
        createJsonObject();
        try {
            apiInterface = ApiClient.getClient().create(ApiInterface.class);
            Call<SearchResultMoneyModel> call = apiInterface.getSearchResult(obj);
            call.enqueue(new Callback<SearchResultMoneyModel>() {

                @Override
                public void onResponse(Call<SearchResultMoneyModel> call, Response<SearchResultMoneyModel> response) {

                    if (response.body() != null) {
                        providers_List = response.body().getProviders();
                        loadmore_progressBar.setVisibility(View.GONE);
                        if (providers_List.size() > 0) {
                            latlong_List.addAll(providers_List);

                            for (int i = 0; i < providers_List.size(); i++) {
                                if (Bank_List.size() == 0)
                                    Bank_List.add(providers_List.get(i));
                                else {
                                    if (!(containBankName(providers_List.get(i).getPartnerName(), Bank_List))) {
                                        Bank_List.add(providers_List.get(i));
                                        Temp_List.add(providers_List.get(i));
                                    }
                                }
                            }
                            searchResultMoneyAdapter.clearList();
                            searchResultMoneyAdapter.notifyDataSetChanged();
                            Collections.sort(Bank_List, new Comparator<SearchResultMoneyModel.Providers>() {

                                @Override
                                public int compare(SearchResultMoneyModel.Providers provider1, SearchResultMoneyModel.Providers provider2) {
                                    if (selected_order.equals(getResources().getString(R.string.str_DESC)))
                                        return Double.compare(provider2.getIndicative(), provider1.getIndicative());
                                    else
                                        return Double.compare(provider1.getIndicative(), provider2.getIndicative());
                                }
                            });
                            if (preferenceUtils.getFragmentName().equals("MoneyTransfer")) {
                                tv_available_providers.setText(Bank_List.size() + " " + getResources().getString(R.string.money_providers));
                            }
                            else if (preferenceUtils.getFragmentName().equals("Forex")) {
                                tv_available_providers.setText(Bank_List.size() + " " + getResources().getString(R.string.forex_providers));
                            }
                            else if (preferenceUtils.getFragmentName().equals("TravelCard")) {
                                tv_available_providers.setText(Bank_List.size() + " " + getResources().getString(R.string.travelcard_providers));
                            }
                            searchResultMoneyAdapter.addAll(Bank_List, providers_List);
                            Temp_List.clear();
                            if (currentPage != TOTAL_PAGES)
                                tv_loadmore.setVisibility(View.VISIBLE);
                            else
                                tv_loadmore.setVisibility(View.GONE);
                        }
                    } else
                        Constant.getToast(getActivity(), "No Data");
                }

                @Override
                public void onFailure(Call<SearchResultMoneyModel> call, Throwable t) {

                }
            });

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    public void createJsonObject() {

        ServiceParams.addProperty("FromCurrency", preferenceUtils.getFromCurrency());
        ServiceParams.addProperty("ToCurrency", preferenceUtils.getToCurrency());
        ServiceParams.addProperty("Amount", preferenceUtils.getamount());

        obj.addProperty("UserId", preferenceUtils.getLoginUserId());
        obj.addProperty("city", preferenceUtils.getCity());
        obj.addProperty("state", preferenceUtils.getState());
        obj.addProperty("country", preferenceUtils.getCountry());
        obj.addProperty("zipCode", preferenceUtils.getZipcode());
        obj.addProperty("Latitude", preferenceUtils.getLat());
        obj.addProperty("Longitude", preferenceUtils.getLng());
        obj.addProperty("OrderByColumn", "Rate");
        obj.addProperty("OrderByDirection", selected_order);
        obj.addProperty("PageSize", 50);
        obj.addProperty("PageNumber", currentPage);
        if (preferenceUtils.getFragmentName().equals("MoneyTransfer")) {
            obj.addProperty("ServiceType", "money-transfer");
        }
        else if (preferenceUtils.getFragmentName().equals("Forex")) {
            obj.addProperty("ServiceType", "Forex ");
        }
        else if (preferenceUtils.getFragmentName().equals("TravelCard")) {
            obj.addProperty("ServiceType", "TravelCard ");
        }
        obj.addProperty("SearchRadius", select_radius);
        obj.addProperty("Operation", selected_buysell);
        //obj.addProperty("Mode", "TT");
        obj.add("ServiceParams", ServiceParams);
        /*Gson gsonObj = new Gson();
        str_jsonObj = gsonObj.toJson(obj);*/
    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.ic_all_location:
                MapViewAllMoneyFragment mapViewAllMoneyFragment = new MapViewAllMoneyFragment();
                Bundle bundle = new Bundle();
                bundle.putSerializable("LatLng_List", (Serializable) latlong_List);
                bundle.putInt("total_providers", searchResultMoneyModel.getTotalCount());
                mapViewAllMoneyFragment.setArguments(bundle);
                fragmentManager.beginTransaction().add(R.id.frame_content, mapViewAllMoneyFragment).addToBackStack(null).commit();
                break;
            case R.id.tv_loadmore:
                tv_loadmore.setVisibility(View.GONE);
                loadmore_progressBar.setVisibility(View.VISIBLE);
                getSearchResultNextPage();
                break;
            case R.id.LL_filter:
                AlertdialogFilterDistance();
                break;
            case R.id.LL_sort:
                tv_loadmore.setVisibility(View.GONE);
                Bank_List.clear();
                if (up) {
                    selected_order = getResources().getString(R.string.str_DESC);
                    ic_sort.setImageResource(R.drawable.ic_arrow_downward);
                    latlong_List.clear();
                    searchResultMoneyAdapter.clearList();
                    rv_searchdata.setAdapter(searchResultMoneyAdapter);
                    currentPage = 1;
                    getSearchResultFirstPage();
                    up = false;
                } else {
                    selected_order = getResources().getString(R.string.str_ASC);
                    ic_sort.setImageResource(R.drawable.ic_arrow_upward);
                    latlong_List.clear();
                    searchResultMoneyAdapter.clearList();
                    rv_searchdata.setAdapter(searchResultMoneyAdapter);
                    currentPage = 1;
                    getSearchResultFirstPage();
                    up = true;
                }
                break;
        }
    }

    public void AlertdialogFilterDistance() {
        final AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
        LayoutInflater layoutInflater = LayoutInflater.from(getActivity());
        View popupInputDialogView = layoutInflater.inflate(R.layout.alert_filterdistance, null);
        builder.setView(popupInputDialogView);
        final AlertDialog alertDialog = builder.create();
        alertDialog.show();
        final Button btn_ok = (Button) popupInputDialogView.findViewById(R.id.btn_ok);
        final Spinner sp_filterdistance = (Spinner) popupInputDialogView.findViewById(R.id.sp_filterdistance);
        ArrayAdapter adapter = ArrayAdapter.createFromResource(getActivity(), R.array.filter_distance, R.layout.spinner_sort_item);
        adapter.setDropDownViewResource(R.layout.layout_dropdownmenu);
        sp_filterdistance.setAdapter(adapter);
        btn_ok.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                tv_loadmore.setVisibility(View.GONE);
                select_radius = Integer.parseInt(sp_filterdistance.getSelectedItem().toString());
                preferenceUtils.setSearchRadius(select_radius);
                alertDialog.cancel();
                latlong_List.clear();
                Bank_List.clear();
                searchResultMoneyAdapter.clearList();
                rv_searchdata.setAdapter(searchResultMoneyAdapter);
                currentPage = 1;
                getSearchResultFirstPage();
            }
        });
    }
}
