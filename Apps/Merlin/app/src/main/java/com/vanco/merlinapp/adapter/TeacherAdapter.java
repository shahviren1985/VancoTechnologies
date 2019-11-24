package com.vanco.merlinapp.adapter;

import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.vanco.merlinapp.R;
import com.vanco.merlinapp.keyinterface.OnItemClick;
import com.vanco.merlinapp.modal.ClsTeacher;

import java.util.ArrayList;

import butterknife.BindView;
import butterknife.ButterKnife;

public class TeacherAdapter extends RecyclerView.Adapter<TeacherAdapter.Holder> {

    ArrayList<ClsTeacher> teachers;
    private OnItemClick onItemClick;


    public TeacherAdapter(ArrayList<ClsTeacher> teachers) {
        this.teachers = teachers;
    }

    public OnItemClick getOnItemClick() {
        return onItemClick;
    }

    public void setOnItemClick(OnItemClick onItemClick) {
        this.onItemClick = onItemClick;
    }

    @NonNull
    @Override
    public Holder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.list_item_teacher, null);
        return new Holder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull Holder holder, int position) {
        ClsTeacher clsTeacher = teachers.get(position);
        holder.txtTeacherName.setText(clsTeacher.getTeacherName());
        holder.txtSemster.setText(holder.txtSemster.getContext().getString(R.string.semester) + " " + clsTeacher.getSemester());
        holder.txtSubject.setText(holder.txtSemster.getContext().getString(R.string.subject) + " " + clsTeacher.getSubjectName());
        if (clsTeacher.isSendFeedBack()) {
            holder.imgStatus.setImageResource(R.drawable.circle_green);
            holder.imgArrow.setVisibility(View.GONE);
        } else {
            holder.imgStatus.setImageResource(R.drawable.circle_grey);
            holder.imgArrow.setVisibility(View.VISIBLE);
        }

    }

    @Override
    public int getItemCount() {
        if (teachers == null)
            return 0;

        return teachers.size();

    }

    public void setList(ArrayList<ClsTeacher> list) {
        this.teachers = list;
    }

    public class Holder extends RecyclerView.ViewHolder {
        @BindView(R.id.txtTeacherName)
        TextView txtTeacherName;

        @BindView(R.id.txtSubject)
        TextView txtSubject;

        @BindView(R.id.imgStatus)
        ImageView imgStatus;

        @BindView(R.id.txtCode)
        TextView txtCode;

        @BindView(R.id.txtSemster)
        TextView txtSemster;

        @BindView(R.id.imgArrow)
        ImageView imgArrow;

        public Holder(View itemView) {
            super(itemView);
            ButterKnife.bind(this, itemView);
            itemView.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    if (onItemClick != null && !teachers.get(getAdapterPosition()).isSendFeedBack())
                        onItemClick.onItemClick(getAdapterPosition(), teachers.get(getAdapterPosition()));
                }
            });
        }
    }
}
