package com.sringara.sringara.adapter;

import android.content.Context;
import android.graphics.Color;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;

import com.squareup.picasso.Picasso;
import com.sringara.sringara.R;
import com.sringara.sringara.helper.Utils;
import com.sringara.sringara.models.ProductList;

import java.io.File;
import java.util.List;

public class ListAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {

    Context context;
    List<ProductList> lists;
    int selectedPosition = -1;
    OnItemClickListener onItemClickListener;

    public ListAdapter(List<ProductList> lists, Context context, OnItemClickListener onItemClickListener, int selectedPosition) {
        this.lists = lists;
        this.context = context;
        this.onItemClickListener = onItemClickListener;
        this.selectedPosition = selectedPosition;
    }

    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup viewGroup, int position) {
        RecyclerView.ViewHolder viewHolder = null;
        if (position == 0) {
            View v = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.row_list, viewGroup, false);
            viewHolder = new ViewHolder(v);
        }
        return viewHolder;
    }

    @Override
    public void onBindViewHolder(RecyclerView.ViewHolder holder, final int position) {
        if (holder instanceof ViewHolder) {

            if(!Utils.isStringNull(lists.get(position).getImagePath())) {
                if(!lists.get(position).getProductFilter().equals("Earring")) {
                    Picasso.with(context).load(new File(context.getFilesDir() + "/" + lists.get(position).getImagePath())).fit().into(((ViewHolder) holder).mIvImage);
                } else {
                    Picasso.with(context).load(new File(context.getFilesDir() + "/" + lists.get(position).getImagePath())).into(((ViewHolder) holder).mIvImage);
                }
            } else {
                Picasso.with(context).load(R.drawable.avtar_image).fit().centerCrop().into(((ViewHolder) holder).mIvImage);
            }

            if(position == selectedPosition) {
                ((ViewHolder) holder).mIvImage.setBackgroundColor(Color.parseColor("#ef6c00"));
            } else {
                ((ViewHolder) holder).mIvImage.setBackgroundColor(Color.parseColor("#00000000"));
            }

            holder.itemView.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {

                    selectedPosition = position;
                    onItemClickListener.setOnItemClick(position);
                    notifyDataSetChanged();

                }
            });

        }
    }

    @Override
    public int getItemCount() {
        return lists.size();
    }

    public static class ViewHolder extends RecyclerView.ViewHolder {
        public ImageView mIvImage;

        ViewHolder(View itemView) {
            super(itemView);
            mIvImage = (ImageView) itemView.findViewById(R.id.ivImage);
        }
    }

    public interface OnItemClickListener {
        public void setOnItemClick(int position);
    }

}