package com.sringara.sringara.adapter;

import android.content.Context;
import android.content.Intent;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.squareup.picasso.Picasso;
import com.sringara.sringara.BuyNowActivity;
import com.sringara.sringara.MainActivity;
import com.sringara.sringara.R;
import com.sringara.sringara.SqlliteDatabaseHelper;
import com.sringara.sringara.helper.DialogAlert;
import com.sringara.sringara.helper.Utils;
import com.sringara.sringara.models.ProductList;

import java.io.File;
import java.util.List;

public class ProductListAdapter extends RecyclerView.Adapter<RecyclerView.ViewHolder> {

    Context context;
    List<ProductList> lists;
    int selectedPosition = -1;
    OnItemClickListener onItemClickListener;

    public ProductListAdapter(List<ProductList> lists, Context context, OnItemClickListener onItemClickListener) {
        this.lists = lists;
        this.context = context;
        this.onItemClickListener = onItemClickListener;
    }

    @Override
    public RecyclerView.ViewHolder onCreateViewHolder(ViewGroup viewGroup, int position) {
        RecyclerView.ViewHolder viewHolder = null;
        if (position == 0) {
            View v = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.row_product_list, viewGroup, false);
            viewHolder = new ViewHolder(v);
        }
        return viewHolder;
    }

    @Override
    public void onBindViewHolder(final RecyclerView.ViewHolder holder, final int position) {
        if (holder instanceof ViewHolder) {

            final ProductList productData = lists.get(position);

            if(!Utils.isStringNull(productData.getImagePath())) {
                if(!productData.getProductFilter().equals("Earring")) {
                    Picasso.with(context).load(new File(context.getFilesDir() + "/" + productData.getImagePath())).fit().into(((ViewHolder) holder).mIvImage);
                } else {
                    Picasso.with(context).load(new File(context.getFilesDir() + "/" + productData.getImagePath())).into(((ViewHolder) holder).mIvImage);
                }
            } else {
                ((ViewHolder) holder).mIvImage.setImageResource(R.drawable.avtar_image);
            }

            if(productData.getIsFavourite() == 0) {
                ((ViewHolder) holder).mIvFavourite.setImageResource(R.drawable.baseline_favorite_border_white);
            } else {
                ((ViewHolder) holder).mIvFavourite.setImageResource(R.drawable.baseline_favorite_white);
            }

            if(lists.get(position).getProductFilter().equals("Earring")) {
                ((ViewHolder) holder).mIvImage.setScaleType(ImageView.ScaleType.FIT_CENTER);
            } else {
                ((ViewHolder) holder).mIvImage.setScaleType(ImageView.ScaleType.CENTER);
            }

            ((ViewHolder) holder).mTvName.setText(lists.get(position).getName());

            ((ViewHolder) holder).mTvPrice.setText(context.getString(R.string.rupee_currency_symbol) + lists.get(position).getPrice());

            holder.itemView.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {

                    if(!Utils.isStringNull(productData.getImagePath())) {
                        Intent intent = new Intent(context, MainActivity.class);
                        intent.putExtra("id", productData.getDb_id());
                        intent.putExtra("selectType", productData.getProductFilter());
                        context.startActivity(intent);
                    } else {
                        DialogAlert.show_dialog(context, context.getString(R.string.please_wait));
                    }

                }
            });

            ((ViewHolder) holder).mIvFavourite.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    if(!Utils.isStringNull(productData.getImagePath())) {
                        SqlliteDatabaseHelper databaseHelper = new SqlliteDatabaseHelper(context);
                        databaseHelper.addToFavourite(Integer.parseInt(productData.getDb_id()));
                        ((ViewHolder) holder).mIvFavourite.setImageResource(R.drawable.baseline_favorite_white);
                        productData.setIsFavourite(1);
                        Intent intent = new Intent(context, BuyNowActivity.class);
                        intent.putExtra("id", productData.getDb_id());
                        context.startActivity(intent);
                    } else {
                        DialogAlert.show_dialog(context, context.getString(R.string.please_wait));
                    }
                }
            });

            ((ViewHolder) holder).mTvTryNow.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    if(!Utils.isStringNull(productData.getImagePath())) {
                        Intent intent = new Intent(context, MainActivity.class);
                        intent.putExtra("id", productData.getDb_id());
                        intent.putExtra("selectType", productData.getProductFilter());
                        context.startActivity(intent);
                    } else {
                        DialogAlert.show_dialog(context, context.getString(R.string.please_wait));
                    }
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
        public TextView mTvName;
        public TextView mTvPrice;
        public TextView mTvTryNow;
        public ImageView mIvFavourite;

        ViewHolder(View itemView) {
            super(itemView);
            mIvImage = (ImageView) itemView.findViewById(R.id.ivImage);
            mTvName = (TextView) itemView.findViewById(R.id.tvName);
            mTvPrice = (TextView) itemView.findViewById(R.id.tvPrice);
            mTvTryNow = (TextView) itemView.findViewById(R.id.tvTryNow);
            mIvFavourite = (ImageView) itemView.findViewById(R.id.ivFavourite);
        }
    }

    public interface OnItemClickListener {
        public void setOnItemClick(int position);
    }

}