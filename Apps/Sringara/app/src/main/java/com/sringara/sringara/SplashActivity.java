package com.sringara.sringara;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.view.View;
import android.widget.LinearLayout;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.sringara.sringara.helper.Constants;
import com.sringara.sringara.helper.Utils;
import com.sringara.sringara.models.ProductList;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

public class SplashActivity extends Activity {

    LinearLayout mLlPrgBar;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_splash);
        mLlPrgBar = (LinearLayout) findViewById(R.id.llPrgBar);
        Handler handler = new Handler();

        if(Utils.isStringNull(Utils.getPrefString(this, Constants.KEY_LAST_MODIFIED_DATE))) {
            if(Utils.isConnectingToInternet(this, true)) {
                mLlPrgBar.setVisibility(View.VISIBLE);
                getProducts();
            }
        } else {
            handler.postDelayed(new Runnable() {

                @Override
                public void run() {
                    startActivity(new Intent(SplashActivity.this,ProductListActivity.class));
                    finish();
                }
            }, 3000);
        }

    }

    private void getProducts() {

        RequestQueue queue = Volley.newRequestQueue(this);
        String url = Constants.GET_PRODUCTS;

        StringRequest stringRequest = new StringRequest(Request.Method.GET, url,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        JSONObject object;
                        try {
                            if(response != null) {
                                object = new JSONObject(response);
                                SqlliteDatabaseHelper databaseHelper = new SqlliteDatabaseHelper(SplashActivity.this);
                                String lastModified = object.getString("lastModified");
                                if(object.has("contact")) {
                                    String contact = object.getString("contact");
                                    Utils.setPrefString(SplashActivity.this, Constants.KEY_CONTACT, contact);
                                }

                                Utils.setPrefString(SplashActivity.this, Constants.KEY_LAST_MODIFIED_DATE, lastModified);

                                if (object.has("products") && !Utils.isStringNull(object.get("products").toString())) {
                                    Gson gson = new Gson();
                                    ArrayList<ProductList> list = gson.fromJson(object.getJSONArray("products").toString(), new TypeToken<List<ProductList>>() {
                                    }.getType());

                                    for (int i = 0; i < list.size(); i++) {
                                        long id = databaseHelper.addProduct(list.get(i));
                                    }

                                    startActivity(new Intent(SplashActivity.this,ProductListActivity.class).putExtra("isFirstTime", true));
                                    finish();


                                }
                            }

                        } catch (JSONException e) {
                            e.printStackTrace();
                        }
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                Toast.makeText(SplashActivity.this, getString(R.string.some_thing_went_wrong_please_try_again), Toast.LENGTH_LONG).show();
            }
        });

        queue.add(stringRequest);
    }

}
