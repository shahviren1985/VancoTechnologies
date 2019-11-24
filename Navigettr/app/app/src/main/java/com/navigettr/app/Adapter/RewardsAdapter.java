package com.navigettr.app.Adapter;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.navigettr.app.Model.RewardsModel;
import com.navigettr.app.R;

import java.util.ArrayList;
import java.util.List;

public class RewardsAdapter extends RecyclerView.Adapter<RewardsAdapter.myViewHolder> {

    private Context mContext;
    private List<RewardsModel> rewardsModelList = new ArrayList<>();
    private LayoutInflater layoutInflater;

    public RewardsAdapter(Context mContext, List<RewardsModel> rewardsModelList) {
        this.mContext = mContext;
        this.rewardsModelList = rewardsModelList;
        layoutInflater = LayoutInflater.from(mContext);
    }

    @NonNull
    @Override
    public myViewHolder onCreateViewHolder(@NonNull ViewGroup viewGroup, int i) {
        View itemView = layoutInflater.inflate(R.layout.rv_list_rewards, viewGroup, false);
        return new myViewHolder(itemView);
    }

    @Override
    public void onBindViewHolder(@NonNull myViewHolder myViewHolder, int i) {
        final RewardsModel rewardsModel = rewardsModelList.get(i);
        myViewHolder.tv_TransactionId.setText(rewardsModel.getTransactionId());
        myViewHolder.tv_DateEarned.setText(rewardsModel.getDateEarned());
        myViewHolder.tv_Comments.setText(rewardsModel.getComments());
        myViewHolder.tv_RewardPoints.setText(String.format("%d", rewardsModel.getRewardPoints()));
    }

    @Override
    public int getItemCount() {
        return rewardsModelList.size();
    }

    public class myViewHolder extends RecyclerView.ViewHolder{

        private TextView tv_TransactionId, tv_DateEarned, tv_Comments, tv_RewardPoints;

        public myViewHolder(@NonNull View itemView) {
            super(itemView);

            tv_TransactionId = itemView.findViewById(R.id.tv_TransactionId);
            tv_DateEarned = itemView.findViewById(R.id.tv_DateEarned);
            tv_Comments = itemView.findViewById(R.id.tv_Comments);
            tv_RewardPoints = itemView.findViewById(R.id.tv_RewardPoints);
        }
    }
}
