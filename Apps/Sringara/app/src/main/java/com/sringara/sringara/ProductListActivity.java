package com.sringara.sringara;

import android.Manifest;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Build;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.GridLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.ImageRequest;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.sringara.sringara.adapter.ProductListAdapter;
import com.sringara.sringara.helper.Constants;
import com.sringara.sringara.helper.Utils;
import com.sringara.sringara.models.ProductList;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.FileOutputStream;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class ProductListActivity extends AppCompatActivity implements View.OnClickListener {

    ArrayList<ProductList> listsItem = new ArrayList<ProductList>();

    private Toolbar toolbar;
    private ProductListAdapter adapter;
    private RecyclerView mRvList;
    private ImageView mIvEarring;
    private TextView mTvSyncMessage;
    private ImageView mIvPendant;
    private ImageView mIvCall;
    String selectType="Earring";
    private ImageView mIvNecklace;
    private TextView mTvType;
    private LinearLayout mLlSyncTextView;
    private LinearLayout mLlEarring;
    private LinearLayout mLlPendant;
    private LinearLayout mLlNecklace;
    private TextView mTvNecklace;
    private TextView mTvEarring;
    private TextView mTvPendant;

    private SqlliteDatabaseHelper databaseHelper;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_product_list);
        getFindViewById();
        setOnClickListener();
        setSupportActionBar(toolbar);
        init();

    }

    private void setOnClickListener() {
        mLlNecklace.setOnClickListener(this);
        mLlEarring.setOnClickListener(this);
        mLlPendant.setOnClickListener(this);
        mIvCall.setOnClickListener(this);
    }

    private void init() {
        mRvList.setLayoutManager(new GridLayoutManager(ProductListActivity.this, 2));

        mRvList.setNestedScrollingEnabled(false);

        adapter = new ProductListAdapter(listsItem, ProductListActivity.this, new ProductListAdapter.OnItemClickListener() {
            @Override
            public void setOnItemClick(int position) {

            }
        });

        databaseHelper = new SqlliteDatabaseHelper(this);

        setNeckLace();
        mTvType.setText(getString(R.string.necklaces));
        mRvList.setAdapter(adapter);
        selectedBackground(mIvEarring);
        mTvType.setText(getString(R.string.necklaces));
        selectedBackground(mIvNecklace);
        mIvNecklace.setImageResource(R.drawable.ic_necklace_active);
        mTvNecklace.setTextColor(getResources().getColor(R.color.white));
        mLlNecklace.setBackgroundResource(R.drawable.bg_selected_ornament_category);
        ArrayList<ProductList> lists = databaseHelper.getListLastImageUpdate("");
        if(lists.size() != 0) {
            if(listsItem.size() == lists.size()) {
                getImage(this, 0, lists);
            } else {
                getImage(this, 0, lists);
            }
        }

        if(getIntent().getExtras() != null && getIntent().hasExtra("isFirstTime")) {
        } else{
            if (Utils.isConnectingToInternet(this, false)) {
                getProducts();
            } else {
                mTvSyncMessage.setText(getString(R.string.internet_msg_not_sync_data));
                mLlSyncTextView.setVisibility(View.VISIBLE);
            }
        }
    }

    private void getFindViewById() {
        toolbar = (Toolbar) findViewById(R.id.toolbar);
        mIvEarring = (ImageView) findViewById(R.id.ivImageEaring);
        mIvPendant = (ImageView) findViewById(R.id.ivImagePendant);
        mIvNecklace = (ImageView) findViewById(R.id.ivImageNecklace);
        mTvType = (TextView) findViewById(R.id.tvType);
        mRvList = (RecyclerView) findViewById(R.id.rvListItems);
        mLlSyncTextView = (LinearLayout) findViewById(R.id.llSyncTextView);
        mTvSyncMessage = (TextView) findViewById(R.id.tvSyncMessage);
        mIvCall = (ImageView) findViewById(R.id.ivDial);
        mLlNecklace = (LinearLayout) findViewById(R.id.llNecklace);
        mLlEarring = (LinearLayout) findViewById(R.id.llEarring);
        mLlPendant = (LinearLayout) findViewById(R.id.llPendant);
        mTvEarring = (TextView) findViewById(R.id.tvEarrings);
        mTvNecklace = (TextView) findViewById(R.id.tvNeckLace);
        mTvPendant = (TextView) findViewById(R.id.tvPendant);
    }

    private void setEarrings() {
        listsItem.clear();
        selectType = "Earring";
        listsItem.addAll(databaseHelper.getList(selectType));
        adapter.notifyDataSetChanged();
    }

    private void setNeckLace() {
        listsItem.clear();
        selectType = "Necklace";
        listsItem.addAll(databaseHelper.getList(selectType));
        adapter.notifyDataSetChanged();
    }

    private void setPendant() {
        listsItem.clear();
        selectType = "Pendant";
        listsItem.addAll(databaseHelper.getList(selectType));
        adapter.notifyDataSetChanged();
    }

    private void selectedBackground(ImageView ivOrnamentImage) {
        mIvEarring.setImageResource(R.drawable.ic_earring_inactive);
        mIvPendant.setImageResource(R.drawable.ic_pendent_inactive);
        mIvNecklace.setImageResource(R.drawable.ic_necklace_inactive);
        mLlNecklace.setBackgroundResource(R.drawable.bg_unselected_ornament_category);
        mLlEarring.setBackgroundResource(R.drawable.bg_unselected_ornament_category);
        mLlPendant.setBackgroundResource(R.drawable.bg_unselected_ornament_category);
        mTvEarring.setTextColor(getResources().getColor(R.color.purple_default_app_color));
        mTvNecklace.setTextColor(getResources().getColor(R.color.purple_default_app_color));
        mTvPendant.setTextColor(getResources().getColor(R.color.purple_default_app_color));
    }

    @Override
    public void onClick(View v) {

        switch (v.getId()) {
            case R.id.llPendant:
                setPendant();
                mTvType.setText(getString(R.string.pendants));
                selectedBackground(mIvPendant);
                mIvPendant.setImageResource(R.drawable.ic_pendent_active);
                mTvPendant.setTextColor(getResources().getColor(R.color.white));
                mLlPendant.setBackgroundResource(R.drawable.bg_selected_ornament_category);
                break;
            case R.id.ivDial:
                String t_number = getString(R.string.inquiry_number);
                String prefs_i_number = Utils.getPrefString(ProductListActivity.this, Constants.KEY_CONTACT);
                if(!Utils.isStringNull(prefs_i_number)) {
                    t_number = prefs_i_number;
                }
                if (Build.VERSION.SDK_INT > Build.VERSION_CODES.LOLLIPOP_MR1) {
                    int result = ContextCompat.checkSelfPermission(this, Manifest.permission.CALL_PHONE);
                    if (result != PackageManager.PERMISSION_GRANTED) {
                        ActivityCompat.requestPermissions(this, new String[]{Manifest.permission.CALL_PHONE}, 101);
                    } else {
                        Intent intent = new Intent(Intent.ACTION_CALL);
                        intent.setData(Uri.parse("tel:" + t_number));
                        startActivity(intent);
                    }
                } else {
                    Intent intent = new Intent(Intent.ACTION_CALL);
                    intent.setData(Uri.parse("tel:" + t_number));
                    startActivity(intent);
                }
                break;
            case R.id.llNecklace:
                setNeckLace();
                mTvType.setText(getString(R.string.necklaces));
                selectedBackground(mIvNecklace);
                mIvNecklace.setImageResource(R.drawable.ic_necklace_active);
                mTvNecklace.setTextColor(getResources().getColor(R.color.white));
                mLlNecklace.setBackgroundResource(R.drawable.bg_selected_ornament_category);
                break;
            case R.id.llEarring:
                setEarrings();
                mTvType.setText(getString(R.string.earrings));
                selectedBackground(mIvEarring);
                mIvEarring.setImageResource(R.drawable.ic_earring_active);
                mTvEarring.setTextColor(getResources().getColor(R.color.white));
                mLlEarring.setBackgroundResource(R.drawable.bg_selected_ornament_category);
                break;
        }
    }

    public void getImage(final Context context, final int position, final ArrayList<ProductList> list) {
        RequestQueue queue = Volley.newRequestQueue(context);
        ImageRequest request = new ImageRequest(list.get(position).getMainImage(),
                new Response.Listener<Bitmap>() {
                    @Override
                    public void onResponse(final Bitmap bitmap) {
                        ((Activity) context).runOnUiThread(new Runnable() {
                            public void run() {
                                try {
                                    new SaveImage(context, position, list).execute(bitmap);
                                } catch (Exception e) {
                                    e.printStackTrace();
                                }
                            }
                        });
                    }
                }, 0, 0, ImageView.ScaleType.CENTER_CROP,null,
                new Response.ErrorListener() {
                    public void onErrorResponse(VolleyError error) {
                    }
                });
        queue.add(request);
    }



    private void getProducts() {

        RequestQueue queue = Volley.newRequestQueue(this);
        String url = Constants.GET_PRODUCTS;
        mLlSyncTextView.setVisibility(View.VISIBLE);

        StringRequest stringRequest = new StringRequest(Request.Method.GET, url,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        JSONObject object;
                        try {
                            if(response != null) {
                                object = new JSONObject(response);
                                SqlliteDatabaseHelper databaseHelper = new SqlliteDatabaseHelper(ProductListActivity.this);
                                String previousLastModified = Utils.getPrefString(ProductListActivity.this, Constants.KEY_LAST_MODIFIED_DATE);
                                String lastModified = object.getString("lastModified");
                                long lastModifyMillis = Utils.getMillisFromDate(lastModified);
                                if(object.has("contact")) {
                                    String contact = object.getString("contact");
                                    Utils.setPrefString(ProductListActivity.this, Constants.KEY_CONTACT, contact);
                                }
                                long previousLastModifyMillis = Utils.getMillisFromDate(previousLastModified);
                                if(lastModifyMillis > previousLastModifyMillis) {
                                    {
                                        if (object.has("products") && !Utils.isStringNull(object.get("products").toString())) {
                                            Gson gson = new Gson();
                                            ArrayList<ProductList> list = gson.fromJson(object.getJSONArray("products").toString(), new TypeToken<List<ProductList>>() {
                                            }.getType());

                                            for (int i = 0; i < list.size(); i++) {
                                                if (!databaseHelper.rowIdExists(list.get(i).getId())) {
                                                    long id = databaseHelper.addProduct(list.get(i));
                                                } else {
                                                    databaseHelper.updateProduct(list.get(i).getId(), list.get(i));
                                                }
                                            }

                                        }
                                        Utils.setPrefString(ProductListActivity.this, Constants.KEY_LAST_MODIFIED_DATE, lastModified);
                                    }
                                    ArrayList<ProductList> lists = databaseHelper.getListLastImageUpdate("");

                                    if(lists.size() != 0) {
                                        getImage(ProductListActivity.this, 0, lists);
                                    }
                                }
                                new Handler().postDelayed(new Runnable() {
                                    @Override
                                    public void run() {
                                        mLlSyncTextView.setVisibility(View.GONE);
                                    }
                                }, 500);
                            }

                        } catch (JSONException e) {
                            e.printStackTrace();
                            mLlSyncTextView.setVisibility(View.GONE);
                        }
                    }
                }, new Response.ErrorListener() {
            @Override
            public void onErrorResponse(VolleyError error) {
                mLlSyncTextView.setVisibility(View.GONE);
            }
        });

        queue.add(stringRequest);
    }

    private void selectProductType(String typeFilter) {
        selectType = typeFilter;
        if(typeFilter.equals("Earring")) {
            setEarrings();
        } else if(typeFilter.equals("Necklace")) {
            setNeckLace();
        } else {
            setPendant();
        }
    }

    private class SaveImage extends AsyncTask<Bitmap, Void, Bitmap> {
        Context context;
        int position;
        ArrayList<ProductList> list;

        public SaveImage(Context mContext, int position, ArrayList<ProductList> list) {
            context = mContext;
            this.position = position;
            this.list = list;
        }

        @Override
        protected Bitmap doInBackground(Bitmap... params) {
            FileOutputStream out = null;
            try {
                if(params[0] != null) {
                    String fileName = list.get(position).getProductFilter()+"_"+list.get(position).getId() +"_"+list.get(position).getDb_id()+".png";
                    out = context.openFileOutput(fileName, Context.MODE_PRIVATE);
                    params[0].compress(Bitmap.CompressFormat.PNG, 85, out);
                    new SqlliteDatabaseHelper(context).updateImagePath(Integer.parseInt(list.get(position).getDb_id()), fileName);
                    list.get(position).setImagePath(fileName);
                }
            } catch (Exception e) {
                e.printStackTrace();
            } finally {
                try {
                    if (out != null) {
                        out.close();
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
            return null;
        }

        @Override
        protected void onPostExecute(Bitmap result) {
            if(context != null) {
                if(position<(list.size() - 1)) {
                    getImage(context, position+1, list);
                } else {
                    selectProductType(selectType);
                }
            }
            if(position>10) {
                if (adapter != null) {
                    selectProductType(selectType);
                    adapter.notifyDataSetChanged();
                }
            }
        }

        @Override
        protected void onPreExecute() {}

        @Override
        protected void onProgressUpdate(Void... values) {}
    }

}
