package com.navigettr.app.Adapter;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.google.gson.Gson;
import com.navigettr.app.Activity.LoginActivity;
import com.navigettr.app.Constant.PreferenceUtils;
import com.navigettr.app.Fragment.SearchresultMoneymapviewFragment;
import com.navigettr.app.Model.SearchResultMoneyModel;
import com.navigettr.app.R;

import java.text.DateFormat;
import java.text.DecimalFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.Locale;

public class BankItemsAdapter extends RecyclerView.Adapter<BankItemsAdapter.myviewHolder> {

    private Context context;
    private List<SearchResultMoneyModel.Providers> Bank_subList = new ArrayList<>();
    private List<SearchResultMoneyModel.Providers.WorkTime> workTime_List = new ArrayList<>();
    private LayoutInflater layoutInflater;
    private String contact_num, workday, current_day="",localTime="";
    private Calendar calendar;
    private DateFormat date;
    private Date date_currentTime, date_startTime, date_endTime;
    private Bundle bundle;
    private FragmentManager fragmentManager;
    private PreferenceUtils preferenceUtils;
    private View view;

    public BankItemsAdapter(Context context, List<SearchResultMoneyModel.Providers> bank_subList) {
        this.context = context;
        Bank_subList = bank_subList;
        layoutInflater = LayoutInflater.from(context);
        fragmentManager = ((FragmentActivity) context).getSupportFragmentManager();

        bundle = new Bundle();
        calendar= Calendar.getInstance();
        Date date1 = calendar.getTime();
        current_day= new SimpleDateFormat("EEEE", Locale.ENGLISH).format(date1.getTime());
        date= new SimpleDateFormat("HH:mm:ss");
        localTime = date.format(date1);
        try {
            date_currentTime = date.parse(localTime);
        } catch (ParseException e) {
            e.printStackTrace();
        }
        preferenceUtils = new PreferenceUtils(context);
    }

    @NonNull
    @Override
    public myviewHolder onCreateViewHolder(@NonNull ViewGroup viewGroup, int i) {
        View itemView = layoutInflater.inflate(R.layout.rv_list_bankitems, viewGroup, false);
        return new myviewHolder(itemView);
    }

    @Override
    public void onBindViewHolder(@NonNull myviewHolder myviewHolder, int i) {
        final SearchResultMoneyModel.Providers providers = Bank_subList.get(i);

       /* myviewHolder.VLL_offers.removeAllViews();
        myviewHolder.tv_offerTitle.setVisibility(View.GONE);*/

        myviewHolder.tv_brandname.setText(providers.getPartnerName());
        myviewHolder.tv_address_1.setText(providers.getAddressLine1());
        if (providers.getAddressLine2().equals(""))
            myviewHolder.tv_address_2.setVisibility(View.GONE);
        else
            myviewHolder.tv_address_2.setText(providers.getAddressLine2());

        myviewHolder.tv_city_state_zip.setText(providers.getCity()+", "+providers.getState()+" - "+providers.getZipCode());
        myviewHolder.tv_Distance.setText(new DecimalFormat("##.##").format(providers.getDistance()) + " KM away");
        /*contact_num = providers.getMobileNumber();
        if (preferenceUtils.getIsLogin()) {
            if (contact_num.equals("") || contact_num.equals(null))
                myviewHolder.tv_contact.setVisibility(View.GONE);
            else
                myviewHolder.tv_contact.setVisibility(View.VISIBLE);
        }*/
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

                        if (date_currentTime.after(oneHourBack_startTime) && date_currentTime.before(date_startTime)){
                            myviewHolder.tv_status.setText(context.getResources().getString(R.string.opening_soon));
                            myviewHolder.tv_status.setTextColor(context.getResources().getColor(R.color.color_green));
                        }
                        else if (date_currentTime.after(date_startTime) && date_currentTime.before(oneHourBack_endTime)) {
                            myviewHolder.tv_status.setText(context.getResources().getString(R.string.open_now));
                            myviewHolder.tv_status.setTextColor(context.getResources().getColor(R.color.color_green));
                        }
                        else if (date_currentTime.after(oneHourBack_endTime) && date_currentTime.before(date_endTime)){
                            myviewHolder.tv_status.setText(context.getResources().getString(R.string.closing_soon));
                            myviewHolder.tv_status.setTextColor(context.getResources().getColor(R.color.color_red));
                        }
                        else if (date_currentTime.before(oneHourBack_startTime) || date_currentTime.after(date_endTime)) {
                            myviewHolder.tv_status.setText(context.getResources().getString(R.string.close_now));
                            myviewHolder.tv_status.setTextColor(context.getResources().getColor(R.color.color_red));
                        }
                        else {
                            myviewHolder.tv_status.setText("N/A");
                            myviewHolder.tv_status.setTextColor(context.getResources().getColor(R.color.color_dark_grey));
                        }
                    } catch (ParseException e) {
                        e.printStackTrace();
                    }
                }
            }
        }

       /* if(preferenceUtils.getIsLogin()) {
            if (providers.getOffer() != null) {
                final SearchResultMoneyModel.Providers.Offer offer = providers.getOffer();
                myviewHolder.VLL_offers.setVisibility(View.VISIBLE);
                myviewHolder.tv_offerTitle.setVisibility(View.VISIBLE);
                view = layoutInflater.inflate(R.layout.custom_layout_offer, myviewHolder.VLL_offers, false);
                TextView tv_offerdata = (TextView) view.findViewById(R.id.tv_offerdata);
                tv_offerdata.setText(offer.getOfferText());
                myviewHolder.VLL_offers.addView(tv_offerdata);
            }
        }*/

        myviewHolder.iv_location.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (preferenceUtils.getIsLogin()) {
                    Gson gson = new Gson();
                    preferenceUtils.setModelData(gson.toJson(providers));

                    SearchresultMoneymapviewFragment searchresultMoneymapviewFragment = new SearchresultMoneymapviewFragment();
                    bundle.putInt("total_providers", preferenceUtils.getTotalProviders());
                    bundle.putSerializable("providers", providers);
                    searchresultMoneymapviewFragment.setArguments(bundle);
                    fragmentManager.beginTransaction().add(R.id.frame_content, searchresultMoneymapviewFragment).addToBackStack(null).commit();

                    /*String url = "http://maps.google.com/maps?q=" + providers.getAddressLine1() + preferenceUtils.getSearchedLocation();
                    Intent intent = new Intent(android.content.Intent.ACTION_VIEW,  Uri.parse(url));
                    v.getContext().startActivity(intent);*/
                }
                else {
                    Intent i = new Intent(context, LoginActivity.class);
                    v.getContext().startActivity(i);
                    ((Activity)context).finish();
                }
            }
        });
    }

    @Override
    public int getItemCount() {
        return Bank_subList.size();
    }

    public class myviewHolder extends RecyclerView.ViewHolder{

        private TextView tv_brandname, tv_offerTitle, tv_mapview, tv_Distance, tv_status,
                tv_address_1, tv_address_2, tv_city_state_zip;
        private LinearLayout VLL_offers;
        private ImageView iv_location;

        public myviewHolder(@NonNull View itemView) {
            super(itemView);

            tv_brandname = itemView.findViewById(R.id.tv_brandname);
            tv_mapview = itemView.findViewById(R.id.tv_mapview);
            tv_offerTitle = itemView.findViewById(R.id.tv_offer);
            tv_Distance = itemView.findViewById(R.id.tv_Distance);
            iv_location = itemView.findViewById(R.id.iv_location);
            tv_status = itemView.findViewById(R.id.tv_status);
            VLL_offers = itemView.findViewById(R.id.VLL_offers);
            tv_city_state_zip = itemView.findViewById(R.id.tv_city_state_zip);
            tv_address_1 = itemView.findViewById(R.id.tv_address_1);
            tv_address_2 = itemView.findViewById(R.id.tv_address_2);
        }
    }
}
