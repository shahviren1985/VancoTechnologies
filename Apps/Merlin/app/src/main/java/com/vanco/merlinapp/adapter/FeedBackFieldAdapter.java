package com.vanco.merlinapp.adapter;

import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;

import com.vanco.merlinapp.R;
import com.vanco.merlinapp.modal.ClsFeedback;

import java.util.ArrayList;

import butterknife.BindView;
import butterknife.ButterKnife;

public class FeedBackFieldAdapter extends RecyclerView.Adapter<FeedBackFieldAdapter.Holder> {

    ArrayList<ClsFeedback> clsFeedFieldList;


    public FeedBackFieldAdapter(ArrayList<ClsFeedback> teachers) {
        this.clsFeedFieldList = teachers;
    }


    @NonNull
    @Override
    public Holder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.list_item_feedback_form, null);
        return new Holder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull Holder holder, int position) {
        ClsFeedback clsFeedback = clsFeedFieldList.get(position);
        holder.txtQuestion.setText(clsFeedback.getQuestion());
        holder.txtPosition.setText((position + 1) + ".");

        if (clsFeedback.getType().equals("radio")) {
            holder.radioGroup.setVisibility(View.VISIBLE);

            holder.radioButton1.setText(clsFeedback.getOptionValues().get(0));
            holder.radioButton2.setText(clsFeedback.getOptionValues().get(1));
            holder.radioButton3.setText(clsFeedback.getOptionValues().get(2));
            holder.radioButton4.setText(clsFeedback.getOptionValues().get(3));
            holder.radioButton5.setText(clsFeedback.getOptionValues().get(4));
            holder.radioButton6.setText(clsFeedback.getOptionValues().get(5));
            holder.radioButton7.setText(clsFeedback.getOptionValues().get(6));

            holder.edtDescription.setVisibility(View.GONE);
            holder.edtDescription2.setVisibility(View.GONE);
        } else {
            holder.edtDescription.setVisibility(View.VISIBLE);
            holder.edtDescription.setHint(clsFeedback.getPlaceHolder());

            holder.edtDescription2.setVisibility(View.VISIBLE);
            holder.edtDescription2.setHint(clsFeedback.getPlaceHolder());

            holder.radioGroup.setVisibility(View.GONE);
        }

    }

    @Override
    public int getItemCount() {
        if (clsFeedFieldList == null)
            return 0;

        return clsFeedFieldList.size();

    }

    public void setList(ArrayList<ClsFeedback> list) {
        this.clsFeedFieldList = list;
    }

    public class Holder extends RecyclerView.ViewHolder {

        @BindView(R.id.txtPosition)
        TextView txtPosition;

        @BindView(R.id.imgValidation)
        ImageView imgValidation;

        @BindView(R.id.txtQuestion)
        TextView txtQuestion;

        @BindView(R.id.radioButton1)
        RadioButton radioButton1;

        @BindView(R.id.radioButton2)
        RadioButton radioButton2;

        @BindView(R.id.radioButton3)
        RadioButton radioButton3;

        @BindView(R.id.radioButton4)
        RadioButton radioButton4;

        @BindView(R.id.radioButton5)
        RadioButton radioButton5;

        @BindView(R.id.radioButton6)
        RadioButton radioButton6;

        @BindView(R.id.radioButton7)
        RadioButton radioButton7;

        @BindView(R.id.radioGroup)
        RadioGroup radioGroup;

        @BindView(R.id.edtDescription)
        EditText edtDescription;

        @BindView(R.id.edtDescription2)
        EditText edtDescription2;

        public Holder(View itemView) {
            super(itemView);
            ButterKnife.bind(this, itemView);
        }
    }
}
