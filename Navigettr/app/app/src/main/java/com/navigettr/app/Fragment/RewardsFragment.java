package com.navigettr.app.Fragment;


import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.navigettr.app.Adapter.RewardsAdapter;
import com.navigettr.app.Constant.PreferenceUtils;
import com.navigettr.app.Model.RewardsModel;
import com.navigettr.app.ProgressDialogLoader;
import com.navigettr.app.R;
import com.navigettr.app.WebServices.ApiClient;
import com.navigettr.app.WebServices.ApiInterface;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class RewardsFragment extends Fragment {

    private RecyclerView rv_rewards;
    private RewardsAdapter rewardsAdapter;
    private LinearLayoutManager layoutManager;
    private ApiInterface apiInterface;
    private PreferenceUtils preferenceUtils;

    public RewardsFragment() {
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_rewards, container, false);
        preferenceUtils = new PreferenceUtils(getActivity());
        rv_rewards = view.findViewById(R.id.rv_rewards);
        layoutManager = new LinearLayoutManager(getActivity());
        rv_rewards.setLayoutManager(layoutManager);
        getRewards();
        return view;
    }

    public void getRewards() {
        try {
            apiInterface = ApiClient.getClient().create(ApiInterface.class);
            ProgressDialogLoader.progressdialog_creation(getActivity(), getResources().getString(R.string.apiCall_rewards));
            Call<List<RewardsModel>> call = apiInterface.getRewards(preferenceUtils.getLoginUserId());
            call.enqueue(new Callback<List<RewardsModel>>() {

                @Override
                public void onResponse(Call<List<RewardsModel>> call, Response<List<RewardsModel>> response) {
                    if (response.body() != null) {
                        List<RewardsModel> rewardsModelList = new ArrayList<>();
                        rewardsModelList = (List<RewardsModel>) response.body();
                        rewardsAdapter = new RewardsAdapter(getActivity(), rewardsModelList);
                        rv_rewards.setHasFixedSize(true);
                        rv_rewards.setAdapter(rewardsAdapter);
                    }
                    ProgressDialogLoader.progressdialog_dismiss();
                }

                @Override
                public void onFailure(Call<List<RewardsModel>> call, Throwable t) {
                    ProgressDialogLoader.progressdialog_dismiss();
                }
            });

        } catch (Exception e) {
            e.printStackTrace();
        }
    }
}
