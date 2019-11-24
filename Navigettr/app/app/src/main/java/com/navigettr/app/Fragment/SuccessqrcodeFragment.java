package com.navigettr.app.Fragment;


import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.navigettr.app.Constant.PreferenceUtils;
import com.navigettr.app.Model.SearchResultMoneyModel;
import com.navigettr.app.R;

import java.lang.reflect.Type;

public class SuccessqrcodeFragment extends Fragment implements View.OnClickListener {

    private TextView tv_get_rewards, tv_Branch_details, tv_From_Currency, tv_To_Currency, tv_Amount, tv_BankName;
    private PreferenceUtils preferenceUtils;
    private SearchResultMoneyModel.Providers providers;
    private FragmentManager fragmentManager;

    public SuccessqrcodeFragment() {
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_successqrcode, container, false);
        preferenceUtils = new PreferenceUtils(getActivity());
        fragmentManager = getActivity().getSupportFragmentManager();
        String json_modelData = preferenceUtils.getModelData();
        Gson gson = new Gson();
        if (!json_modelData.isEmpty()) {
            Type type = new TypeToken<SearchResultMoneyModel.Providers>() {
            }.getType();
            providers = gson.fromJson(json_modelData, type);
        }
        tv_get_rewards = view.findViewById(R.id.tv_get_rewards);
        tv_Branch_details = view.findViewById(R.id.tv_Branch_details);
        tv_From_Currency = view.findViewById(R.id.tv_From_Currency);
        tv_To_Currency = view.findViewById(R.id.tv_To_Currency);
        tv_Amount = view.findViewById(R.id.tv_Amount);
        tv_BankName = view.findViewById(R.id.tv_BankName);
        tv_Branch_details.setText(providers.getAddressLine1());
        tv_From_Currency.setText(preferenceUtils.getFromCurrency());
        tv_To_Currency.setText(preferenceUtils.getToCurrency());
        tv_Amount.setText(String.format("%d", preferenceUtils.getamount()));
        tv_BankName.setText(providers.getPartnerName());
        tv_get_rewards.setOnClickListener(this);
        return view;
    }

    @Override
    public void onClick(View v) {
        RewardsFragment rewardsFragment = new RewardsFragment();
        fragmentManager.beginTransaction().add(R.id.frame_content, rewardsFragment).addToBackStack(null).commit();
    }
}
