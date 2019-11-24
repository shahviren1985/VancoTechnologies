package com.navigettr.app.Adapter;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.constraint.ConstraintLayout;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.navigettr.app.Constant.PreferenceUtils;
import com.navigettr.app.Fragment.BankitemsFragment;
import com.navigettr.app.Model.SearchResultMoneyModel;
import com.navigettr.app.R;
import com.nostra13.universalimageloader.core.DisplayImageOptions;
import com.nostra13.universalimageloader.core.ImageLoader;
import com.nostra13.universalimageloader.core.ImageLoaderConfiguration;
import com.nostra13.universalimageloader.core.display.FadeInBitmapDisplayer;

import java.io.Serializable;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.Locale;

public class SearchResultMoneyAdapter extends RecyclerView.Adapter<SearchResultMoneyAdapter.myviewHolder> {

    private Context mContext;
    private List<SearchResultMoneyModel.Providers> providers_List = new ArrayList<>();
    private List<SearchResultMoneyModel.Providers.WorkTime> workTime_List = new ArrayList<>();
    private List<SearchResultMoneyModel.Providers> Bank_subList = new ArrayList<>();
    private List<SearchResultMoneyModel.Providers> main_List = new ArrayList<>();
    private FragmentManager fragmentManager;
    private View view;
    private LayoutInflater layoutInflater;
    private String contact_num, workday, current_day = "", localTime = "";
    private double latitude, longitude;
    private DisplayImageOptions options;
    private Date date_currentTime, date_startTime, date_endTime;
    private int total_providers = 0;
    private Calendar calendar;
    private DateFormat date;
    private Bundle bundle;
    private PreferenceUtils preferenceUtils;

    public SearchResultMoneyAdapter(Context mContext) {
        this.mContext = mContext;
        bundle = new Bundle();
        ImageLoader.getInstance().init(ImageLoaderConfiguration.createDefault(mContext));
        options = new DisplayImageOptions.Builder()
                .cacheOnDisk(true)
                .showImageOnLoading(R.drawable.splash_icon)
                .resetViewBeforeLoading(true).considerExifParams(true)
                .displayer(new FadeInBitmapDisplayer(300)).build();

        preferenceUtils = new PreferenceUtils(mContext);
        calendar = Calendar.getInstance();
        Date date1 = calendar.getTime();
        current_day = new SimpleDateFormat("EEEE", Locale.ENGLISH).format(date1.getTime());
        date = new SimpleDateFormat("HH:mm:ss");
        localTime = date.format(date1);
        try {
            date_currentTime = date.parse(localTime);
        } catch (ParseException e) {
            e.printStackTrace();
        }
        fragmentManager = ((FragmentActivity) mContext).getSupportFragmentManager();
        layoutInflater = LayoutInflater.from(mContext);
    }

    @NonNull
    @Override
    public myviewHolder onCreateViewHolder(@NonNull ViewGroup viewGroup, int i) {
        View itemView = layoutInflater.inflate(R.layout.rv_list_searchresult, viewGroup, false);
        return new myviewHolder(itemView);
    }

    @Override
    public void onBindViewHolder(@NonNull myviewHolder myviewHolder, int i) {
        final SearchResultMoneyModel.Providers providers = providers_List.get(i);
        myviewHolder.VLL_offers.removeAllViews();
        myviewHolder.tv_offerTitle.setVisibility(View.GONE);

        myviewHolder.tv_brandname.setText(providers.getPartnerName());
        myviewHolder.tv_convert_currency.setText("1 " + preferenceUtils.getFromCurrency() + " = " + providers.getIndicative() + " " + preferenceUtils.getToCurrency());
               /* contact_num = providers.getMobileNumber();
                if (contact_num.equals("") || contact_num.equals(null))
                    holder.tv_contact.setVisibility(View.GONE);
                else
                    holder.tv_contact.setVisibility(View.VISIBLE);*/
        latitude = providers.getLatitude();
        longitude = providers.getLongitude();

        if (providers.getWorkTime() != null) {
            workTime_List = providers.getWorkTime();

            for (int k = 0; k < workTime_List.size(); k++) {
                final SearchResultMoneyModel.Providers.WorkTime workTime = workTime_List.get(k);
                workday = workTime.getWorkDay();
                if (current_day.equals(workday)) {
                    try {
                        date_startTime = date.parse(workTime.getWorkStartTime());
                        date_endTime = date.parse(workTime.getWorkEndTime());

                        calendar.setTime(date_startTime);
                        calendar.add(Calendar.HOUR, -1);
                        Date oneHourBack_startTime = calendar.getTime();

                        calendar.setTime(date_endTime);
                        calendar.add(Calendar.HOUR, -1);
                        Date oneHourBack_endTime = calendar.getTime();

                        if (date_currentTime.after(oneHourBack_startTime) && date_currentTime.before(date_startTime)) {
                            myviewHolder.tv_status.setText(mContext.getResources().getString(R.string.opening_soon));
                            myviewHolder.tv_status.setTextColor(mContext.getResources().getColor(R.color.color_green));
                        } else if (date_currentTime.after(date_startTime) && date_currentTime.before(oneHourBack_endTime)) {
                            myviewHolder.tv_status.setText(mContext.getResources().getString(R.string.open_now));
                            myviewHolder.tv_status.setTextColor(mContext.getResources().getColor(R.color.color_green));
                        } else if (date_currentTime.after(oneHourBack_endTime) && date_currentTime.before(date_endTime)) {
                            myviewHolder.tv_status.setText(mContext.getResources().getString(R.string.closing_soon));
                            myviewHolder.tv_status.setTextColor(mContext.getResources().getColor(R.color.color_red));
                        } else if (date_currentTime.before(oneHourBack_startTime) || date_currentTime.after(date_endTime)) {
                            myviewHolder.tv_status.setText(mContext.getResources().getString(R.string.close_now));
                            myviewHolder.tv_status.setTextColor(mContext.getResources().getColor(R.color.color_red));
                        } else {
                            myviewHolder.tv_status.setText("N/A");
                            myviewHolder.tv_status.setTextColor(mContext.getResources().getColor(R.color.color_dark_grey));
                        }
                    } catch (ParseException e) {
                        e.printStackTrace();
                    }
                }
            }
        }
        ImageLoader.getInstance().displayImage(providers.getPartnerLogoPath(), myviewHolder.iv_brand_logo, options);

                /*holder.tv_contact.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View v) {
                        Intent intent = new Intent(Intent.ACTION_DIAL);
                        intent.setData(Uri.parse("tel:" + contact_num));
                        v.getContext().startActivity(intent);
                    }
                });*/
        if(preferenceUtils.getIsLogin()) {
            if (providers.getOffer() != null) {
                final SearchResultMoneyModel.Providers.Offer offer = providers.getOffer();
                myviewHolder.VLL_offers.setVisibility(View.VISIBLE);
                myviewHolder.tv_offerTitle.setVisibility(View.VISIBLE);
                view = layoutInflater.inflate(R.layout.custom_layout_offer, myviewHolder.VLL_offers, false);
                TextView tv_offerdata = (TextView) view.findViewById(R.id.tv_offerdata);
                tv_offerdata.setText(offer.getOfferText());
                myviewHolder.VLL_offers.addView(tv_offerdata);
            }
        }
        myviewHolder.CL_item.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Bank_subList.clear();
                for (int j = 0; j < main_List.size(); j++) {
                    if (providers.getPartnerName().equals(main_List.get(j).getPartnerName()))
                        Bank_subList.add(main_List.get(j));
                }
                BankitemsFragment bankitemsFragment = new BankitemsFragment();
                bundle.putSerializable("Bank_subList", (Serializable) Bank_subList);
                bankitemsFragment.setArguments(bundle);
                fragmentManager.beginTransaction().add(R.id.frame_content, bankitemsFragment).addToBackStack(null).commit();
            }
        });
    }

    @Override
    public int getItemCount() {
        return providers_List.size();
    }

    public void getTotalProviders(int total_providers) {
        this.total_providers = total_providers;
    }

    public void clearList() {
        providers_List.clear();
    }

    public void add(SearchResultMoneyModel.Providers dataModel) {
        providers_List.add(dataModel);
        notifyItemInserted(providers_List.size() - 1);
    }

    public void addMain(SearchResultMoneyModel.Providers dataModel) {
        main_List.add(dataModel);
        notifyItemInserted(main_List.size() - 1);
    }

    public void addAll(List<SearchResultMoneyModel.Providers> moveResults, List<SearchResultMoneyModel.Providers> main_List) {
        for (SearchResultMoneyModel.Providers result : moveResults) {
            add(result);
        }
        for (SearchResultMoneyModel.Providers providers : main_List) {
            addMain(providers);
        }
    }

    public class myviewHolder extends RecyclerView.ViewHolder {

        private TextView tv_brandname, tv_convert_currency, tv_status, tv_offerTitle;
        private ImageView iv_brand_logo;
        private ConstraintLayout CL_item;
        private LinearLayout VLL_offers;

        private myviewHolder(@NonNull View itemView) {
            super(itemView);

            tv_brandname = itemView.findViewById(R.id.tv_brandname);
            tv_convert_currency = itemView.findViewById(R.id.tv_convert_currency);
            iv_brand_logo = itemView.findViewById(R.id.iv_brand_logo);
            tv_status = itemView.findViewById(R.id.tv_status);
            CL_item = itemView.findViewById(R.id.CL_item);
            tv_offerTitle = itemView.findViewById(R.id.tv_offer);
            VLL_offers = itemView.findViewById(R.id.VLL_offers);
        }
    }
}
