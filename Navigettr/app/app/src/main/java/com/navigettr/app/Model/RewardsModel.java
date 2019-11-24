package com.navigettr.app.Model;

import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class RewardsModel implements Serializable {

    @SerializedName("Id")
    private int Id;

    @SerializedName("TransactionId")
    private String TransactionId;

    @SerializedName("RewardPoints")
    private int RewardPoints;

    @SerializedName("DateEarned")
    private String DateEarned;

    @SerializedName("Comments")
    private String Comments;

    public int getId() {
        return Id;
    }

    public void setId(int id) {
        Id = id;
    }

    public String getTransactionId() {
        return TransactionId;
    }

    public void setTransactionId(String transactionId) {
        TransactionId = transactionId;
    }

    public int getRewardPoints() {
        return RewardPoints;
    }

    public void setRewardPoints(int rewardPoints) {
        RewardPoints = rewardPoints;
    }

    public String getDateEarned() {
        return DateEarned;
    }

    public void setDateEarned(String dateEarned) {
        DateEarned = dateEarned;
    }

    public String getComments() {
        return Comments;
    }

    public void setComments(String comments) {
        Comments = comments;
    }
}
