package com.navigettr.app.Fragment;


import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.widget.DefaultItemAnimator;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.navigettr.app.Adapter.BankItemsAdapter;
import com.navigettr.app.Constant.PreferenceUtils;
import com.navigettr.app.Model.SearchResultMoneyModel;
import com.navigettr.app.R;
import com.nostra13.universalimageloader.core.DisplayImageOptions;
import com.nostra13.universalimageloader.core.ImageLoader;
import com.nostra13.universalimageloader.core.ImageLoaderConfiguration;
import com.nostra13.universalimageloader.core.display.FadeInBitmapDisplayer;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

public class BankitemsFragment extends Fragment {

    private LinearLayout LL_sort_filter, LL_location;
    private TextView tv_available_providers, tv_brandName, tv_convert_currency, tv_totalLocations;
    private ImageView ic_all_location, iv_brand_logo;
    private RecyclerView rv_searchdata;
    private LinearLayoutManager layoutManager;
    private BankItemsAdapter bankItemsAdapter;
    private List<SearchResultMoneyModel.Providers> Bank_subList = new ArrayList<>();
    private String selected_currency_from, selected_currency_to;
    private View View;
    private DisplayImageOptions options;
    private PreferenceUtils preferenceUtils;

    public BankitemsFragment() {
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_searchresultmoney, container, false);
        preferenceUtils = new PreferenceUtils(getActivity());
        ImageLoader.getInstance().init(ImageLoaderConfiguration.createDefault(getActivity()));
        options = new DisplayImageOptions.Builder()
                .cacheOnDisk(true)
                .showImageOnLoading(R.drawable.splash_icon)
                .resetViewBeforeLoading(true).considerExifParams(true)
                .displayer(new FadeInBitmapDisplayer(300)).build();

        Bundle bundle = getArguments();
        if (bundle != null) {
            Bank_subList = (List<SearchResultMoneyModel.Providers>) bundle.getSerializable("Bank_subList");
        }
        LL_sort_filter = view.findViewById(R.id.LL_sort_filter);
        LL_location = view.findViewById(R.id.LL_location);
        tv_available_providers = view.findViewById(R.id.tv_available_providers);
        ic_all_location = view.findViewById(R.id.ic_all_location);
        rv_searchdata = view.findViewById(R.id.rv_searchdata);
        View = view.findViewById(R.id.View);
        iv_brand_logo = view.findViewById(R.id.iv_brand_logo);
        tv_brandName = view.findViewById(R.id.tv_brandName);
        tv_convert_currency = view.findViewById(R.id.tv_convert_currency);
        tv_totalLocations = view.findViewById(R.id.tv_totalLocations);
        View.setVisibility(View.GONE);
        LL_sort_filter.setVisibility(View.GONE);
        LL_location.setVisibility(View.GONE);
        tv_available_providers.setVisibility(View.GONE);
        ic_all_location.setVisibility(View.GONE);

        Collections.sort(Bank_subList, new Comparator<SearchResultMoneyModel.Providers>() {

             @Override
            public int compare(SearchResultMoneyModel.Providers provider1, SearchResultMoneyModel.Providers provider2) {
                 return Double.compare(provider1.getDistance() , provider2.getDistance());
            }
        });
        ImageLoader.getInstance().displayImage(Bank_subList.get(0).getPartnerLogoPath(), iv_brand_logo, options);
        tv_convert_currency.setText("1 " + preferenceUtils.getFromCurrency() + " = " + Bank_subList.get(0).getIndicative() + " " + preferenceUtils.getToCurrency());
        tv_brandName.setText(Bank_subList.get(0).getPartnerName());
        tv_totalLocations.setText(Bank_subList.size() + " Locations found with in " + preferenceUtils.getSearchRadius()+" KM of your location.");

        layoutManager = new LinearLayoutManager(getActivity());
        rv_searchdata.setLayoutManager(layoutManager);
        bankItemsAdapter = new BankItemsAdapter(getActivity(), Bank_subList);
        rv_searchdata.setItemAnimator(new DefaultItemAnimator());
        rv_searchdata.setHasFixedSize(true);
        rv_searchdata.setAdapter(bankItemsAdapter);

        return view;
    }

}

