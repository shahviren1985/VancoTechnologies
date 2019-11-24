package com.navigettr.app.Fragment;


import android.content.Context;
import android.location.LocationManager;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.navigettr.app.Constant.Constant;
import com.navigettr.app.Constant.PreferenceUtils;
import com.navigettr.app.Model.CurrencyModel;
import com.navigettr.app.R;

import java.lang.reflect.Type;
import java.util.ArrayList;
import java.util.List;

public class ForeignexchangeFragment extends Fragment implements View.OnClickListener {

    private TextView tv_btn_search, tv_text, tv_title;
    private EditText et_amount;
    private FragmentManager fragmentManager;
    private Spinner sp_from, sp_to, sp_searchradius;
    private List<CurrencyModel.currency> currency_List = new ArrayList<>();
    private String selected_currency_from, selected_currency_to, selected_buysell;
    private int selected_searchradius = 0;
    private ArrayAdapter<CurrencyModel.currency> adapter_to, adapter_from;
    private LocationManager locationManager ;
    private CurrencyModel.currency currencyModel;
    private PreferenceUtils preferenceUtils;

    public ForeignexchangeFragment() {
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_moneytransfer, container, false);
        preferenceUtils = new PreferenceUtils(getActivity());
        String json_currency = preferenceUtils.getCurencyList();
        Gson gson = new Gson();
        if (json_currency.isEmpty()) {
            // Constant.getToast(getActivity(),  getResources().getString(R.string.err_currency_list));
        } else {
            Type type = new TypeToken<List<CurrencyModel.currency>>() {
            }.getType();
            currency_List = gson.fromJson(json_currency, type);
        }
        tv_btn_search = view.findViewById(R.id.tv_btn_search);
        sp_from = view.findViewById(R.id.sp_from);
        sp_to = view.findViewById(R.id.sp_to);
        sp_searchradius = view.findViewById(R.id.sp_searchradius);
        et_amount = view.findViewById(R.id.et_amount);
        tv_text = view.findViewById(R.id.tv_text);
        tv_text.setText(getResources().getString(R.string.tv_forex_text));
        tv_title = view.findViewById(R.id.tv_title);
        tv_title.setText(getResources().getString(R.string.tv_forex_title));

        fragmentManager = getActivity().getSupportFragmentManager();

        setFromSpinner();
        setToSpinner();
        setSearchRadiusSpinner();

        tv_btn_search.setOnClickListener(this);
        return view;
    }

    @Override
    public void onClick(View v) {
        selected_currency_from = sp_from.getSelectedItem().toString();
        if (selected_currency_from.equals("INR")){
            selected_buysell = "Buy";
        }
        selected_currency_to = sp_to.getSelectedItem().toString();
        if (selected_currency_to.equals("INR")){
            selected_buysell = "Sell";
        }
        selected_searchradius = Integer.parseInt(sp_searchradius.getSelectedItem().toString());

        if (isConnected()) {
            if (selected_currency_from.equals(selected_currency_to)) {
                Constant.getToast(getActivity(), getResources().getString(R.string.error_currency));
            }
            else if (et_amount.getText().toString().isEmpty()) {
                et_amount.setError(getResources().getString(R.string.error_amount));
            }
            else {
                preferenceUtils.setamount(Integer.parseInt(et_amount.getText().toString()));
                preferenceUtils.setFromCurrency(selected_currency_from);
                preferenceUtils.setToCurrency(selected_currency_to);
                preferenceUtils.setFragmentName("Forex");

                SearchresultMoneyFragment searchresultMoneyFragment = new SearchresultMoneyFragment();
                Bundle bundle = new Bundle();
                bundle.putString("selected_BuySell", selected_buysell);
                bundle.putInt("selected_searchradius", selected_searchradius);
                searchresultMoneyFragment.setArguments(bundle);
                fragmentManager.beginTransaction().add(R.id.frame_content, searchresultMoneyFragment).addToBackStack(null).commit();
            }
        }
        else
            Constant.getToast(getActivity(), getResources().getString(R.string.error_internet));
    }

    private void setFromSpinner() {
        adapter_from = new ArrayAdapter<CurrencyModel.currency>(getActivity(),
                R.layout.spinner_item, currency_List);
        adapter_from.setDropDownViewResource(R.layout.layout_dropdownmenu);
        sp_from.setAdapter(adapter_from);
    }
    private void setToSpinner() {
        int index = 0;
        adapter_to = new ArrayAdapter<CurrencyModel.currency>(getActivity(),
                R.layout.spinner_item, currency_List);
        adapter_to.setDropDownViewResource(R.layout.layout_dropdownmenu);
        sp_to.setAdapter(adapter_to);
        for (int i = 0;i<currency_List.size();i++){
            if (currency_List.get(i).getKEY().equals("INR")) {
                index =  i;
            }
        }
        sp_to.setSelection(adapter_to.getPosition(currency_List.get(index)));
    }
    private void setSearchRadiusSpinner() {
        ArrayAdapter adapter = ArrayAdapter.createFromResource(getActivity(), R.array.filter_distance, R.layout.spinner_item);
        adapter.setDropDownViewResource(R.layout.layout_dropdownmenu);
        sp_searchradius.setAdapter(adapter);
    }

    public boolean isConnected() {
        boolean connected = false;
        try {
            ConnectivityManager cm = (ConnectivityManager)getActivity().getSystemService(Context.CONNECTIVITY_SERVICE);
            NetworkInfo nInfo = cm.getActiveNetworkInfo();
            connected = nInfo != null && nInfo.isAvailable() && nInfo.isConnected();
            return connected;
        } catch (Exception e) {
            Log.e("Connectivity Exception", e.getMessage());
        }
        return connected;
    }
}
