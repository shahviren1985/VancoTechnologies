package com.sringara.sringara;

import android.app.ProgressDialog;
import android.content.DialogInterface;
import android.os.Bundle;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.text.Editable;
import android.text.TextUtils;
import android.text.TextWatcher;
import android.util.Patterns;
import android.view.MotionEvent;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RadioButton;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.squareup.picasso.Picasso;
import com.sringara.sringara.helper.Constants;
import com.sringara.sringara.helper.DialogAlert;
import com.sringara.sringara.helper.Utils;
import com.sringara.sringara.models.ProductList;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.File;
import java.io.UnsupportedEncodingException;

public class BuyNowActivity extends AppCompatActivity implements View.OnClickListener {

    private EditText mEtName;
    private EditText mEtPhone;
    private EditText mEtEmail;
    private EditText mEtPinCode;
    private EditText mEtAddress;
    private TextView mTvErrorName;
    private TextView mTvErrorPhone;
    private TextView mTvErrorEmail;
    private TextView mTvErrorPinCode;
    private TextView mTvErrorAddress;
    private TextView mTvErrorState;
    public ImageView mIvBack;
    private ProductList product;
    private Spinner mSpState;
    private RadioButton mRadIndia;
    private RadioButton mRadOther;
    private String state;
    private SqlliteDatabaseHelper databaseHelper;
    private String[] states;
    private LinearLayout mLlState;
    private ImageView mIvProductImage;
    private TextView mTvProductId;
    private TextView mTvProductName;
    private LinearLayout mllView;
    private Button mBtnOrder;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_buy_now);
        getFindViewById();
        setOnClickListener();
        setListener();
        init();

    }

    private void setOnClickListener() {
        mBtnOrder.setOnClickListener(this);
        mIvBack.setOnClickListener(this);
    }

    private void init() {
        databaseHelper = new SqlliteDatabaseHelper(BuyNowActivity.this);
        if(getIntent().getExtras() != null) {
            String id = getIntent().getStringExtra("id");
            product = databaseHelper.getProduct(id);
            mTvProductName.setText(product.getName());
            mTvProductId.setText(product.getId());
            if(!product.getProductFilter().equals("Earring")) {
                Picasso.with(this).load(new File(getFilesDir() + "/" + product.getImagePath())).fit().into(mIvProductImage);
            } else {
                Picasso.with(this).load(new File(getFilesDir() + "/" + product.getImagePath())).into(mIvProductImage);
            }

            if(product.getProductFilter().equals("Earring")) {
                mIvProductImage.setScaleType(ImageView.ScaleType.CENTER);
            } else {
                mIvProductImage.setScaleType(ImageView.ScaleType.FIT_CENTER);
            }

        }
        String statesStr = Utils.retriveState(BuyNowActivity.this, "states.json");
        if(!Utils.isStringNull(statesStr)) {
            try {
                JSONArray jsonArray = new JSONArray(statesStr);
                states = new String[jsonArray.length()];

                for (int i = 0; i < jsonArray.length(); i++) {
                    states[i] = jsonArray.getString(i);
                }

            } catch (Exception e) {
                e.printStackTrace();
            }

            ArrayAdapter<String> adapter = new ArrayAdapter<String>(this,
                    R.layout.row_state, states);
            adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            mSpState.setAdapter(adapter);
        }
    }

    private boolean isValid() {
        mTvErrorName.setVisibility(View.GONE);
        mTvErrorPhone.setVisibility(View.GONE);
        mTvErrorEmail.setVisibility(View.GONE);
        mTvErrorPinCode.setVisibility(View.GONE);
        mTvErrorAddress.setVisibility(View.GONE);
        mTvErrorState.setVisibility(View.GONE);

        boolean isError = false;
        if(Utils.isStringNull(mEtName.getText().toString())) {
            isError = true;
            mTvErrorName.setVisibility(View.VISIBLE);
            mEtName.requestFocus();
        }
        if(Utils.isStringNull(mEtPhone.getText().toString())) {
            mTvErrorPhone.setVisibility(View.VISIBLE);
            if(!isError)
            mEtPhone.requestFocus();
            isError = true;
        }
        if(Utils.isStringNull(mEtEmail.getText().toString().trim())) {
            mTvErrorEmail.setVisibility(View.VISIBLE);
            mTvErrorEmail.setText(getString(R.string.please_enter_email));
            if(!isError)
            mEtEmail.requestFocus();
            isError = true;
        } else if(!isValidEmail(mEtEmail.getText().toString().trim())) {
            mTvErrorEmail.setText(getString(R.string.please_enter_valid_email_address));
            mTvErrorEmail.setVisibility(View.VISIBLE);
            if(!isError)
                mEtEmail.requestFocus();
            isError = true;
        }
        if(Utils.isStringNull(mEtPinCode.getText().toString())) {
            mTvErrorPinCode.setVisibility(View.VISIBLE);
            if(!isError)
                mEtPinCode.requestFocus();
            isError = true;
        }
        if(Utils.isStringNull(mEtAddress.getText().toString())) {
            mTvErrorAddress.setVisibility(View.VISIBLE);
            if(!isError)
                mEtAddress.requestFocus();
            isError = true;
        }
        return isError;
    }

    private void setListener() {
        mEtName.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                if(!Utils.isStringNull(mEtName.getText().toString())) {
                    mTvErrorName.setVisibility(View.GONE);
                }
            }

            @Override
            public void afterTextChanged(Editable s) {
            }
        });

        mEtPhone.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                if(!Utils.isStringNull(mEtPhone.getText().toString())) {
                    mTvErrorPhone.setVisibility(View.GONE);
                }
            }

            @Override
            public void afterTextChanged(Editable s) {
            }
        });


        mSpState.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                mTvErrorState.setVisibility(View.GONE);
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });

        mEtAddress.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                if(!Utils.isStringNull(mEtAddress.getText().toString())) {
                    mTvErrorAddress.setVisibility(View.GONE);
                }
            }

            @Override
            public void afterTextChanged(Editable s) {
            }
        });

        mEtEmail.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                if(!Utils.isStringNull(mEtEmail.getText().toString())) {
                    mTvErrorEmail.setVisibility(View.GONE);
                }
            }

            @Override
            public void afterTextChanged(Editable s) {
            }
        });

        mEtPinCode.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {

            }

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                if(!Utils.isStringNull(mEtPinCode.getText().toString())) {
                    mTvErrorPinCode.setVisibility(View.GONE);
                }
            }

            @Override
            public void afterTextChanged(Editable s) {
            }
        });
        mRadIndia.setOnCheckedChangeListener(new CompoundButton.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(CompoundButton buttonView, boolean isChecked) {
                if(isChecked) {
                    mLlState.setVisibility(View.VISIBLE);
                } else {
                    mLlState.setVisibility(View.GONE);
                }
            }
        });
        mllView.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                Utils.hideKeyboard(v, BuyNowActivity.this);
                return false;
            }
        });
    }

    private void getFindViewById() {
        mEtName = (EditText) findViewById(R.id.etName);
        mEtPhone = (EditText) findViewById(R.id.etPhone);
        mEtEmail = (EditText) findViewById(R.id.etEmail);
        mEtPinCode = (EditText) findViewById(R.id.etPinCode);
        mEtAddress = (EditText) findViewById(R.id.etAddress);
        mSpState = (Spinner) findViewById(R.id.spState);
        mTvErrorName = (TextView) findViewById(R.id.tvErrorName);
        mTvErrorPhone = (TextView) findViewById(R.id.tvErrorPhone);
        mTvErrorEmail = (TextView) findViewById(R.id.tvErrorEmail);
        mTvErrorPinCode = (TextView) findViewById(R.id.tvErrorPinCode);
        mTvErrorAddress = (TextView) findViewById(R.id.tvErrorAddress);
        mBtnOrder = (Button) findViewById(R.id.btnOrder);
        mIvBack = (ImageView) findViewById(R.id.ivBack);
        mTvErrorState = (TextView) findViewById(R.id.tvErrorState);
        mLlState = (LinearLayout) findViewById(R.id.llState);
        mRadIndia = (RadioButton) findViewById(R.id.radIndia);
        mRadOther = (RadioButton) findViewById(R.id.radOther);
        mIvProductImage = (ImageView) findViewById(R.id.ivProduct);
        mTvProductId = (TextView) findViewById(R.id.tvProductId);
        mllView = (LinearLayout) findViewById(R.id.llView);
        mTvProductName = (TextView) findViewById(R.id.tvProductName);
    }

    public static boolean isValidEmail(CharSequence target) {
        return (!TextUtils.isEmpty(target) && Patterns.EMAIL_ADDRESS.matcher(target).matches());
    }

    private void buyProduct() {
        try {
            final ProgressDialog progressDialog = new ProgressDialog(BuyNowActivity.this);
            progressDialog.setMessage(getString(R.string.please_wait));
            progressDialog.show();
            RequestQueue requestQueue = Volley.newRequestQueue(this);
            String URL = Constants.SAVE_INQUIRY;
            JSONObject jsonBody = new JSONObject();
            jsonBody.put("ProductId", product.getId());
            jsonBody.put("ProductName", product.getName());
            jsonBody.put("PersonName", mEtName.getText().toString());
            jsonBody.put("MobileNumber", mEtPhone.getText().toString());
            jsonBody.put("EmailAddress", mEtEmail.getText().toString().trim());
            jsonBody.put("Address", mEtAddress.getText().toString());
            if(mRadIndia.isChecked()) {
                jsonBody.put("State", states[mSpState.getSelectedItemPosition()]);
            } else {
                jsonBody.put("State", "");
            }
            jsonBody.put("PinCode", mEtPinCode.getText().toString());
            final String requestBody = jsonBody.toString();

            StringRequest stringRequest = new StringRequest(Request.Method.POST, URL, new Response.Listener<String>() {
                @Override
                public void onResponse(String response) {
                    if(progressDialog != null && progressDialog.isShowing()) {
                        progressDialog.dismiss();
                    }
                    AlertDialog.Builder alerDialog = new AlertDialog.Builder(BuyNowActivity.this).setMessage(response.replaceAll("\"",""))
                            .setPositiveButton(android.R.string.ok, new DialogInterface.OnClickListener() {
                                @Override
                                public void onClick(DialogInterface dialog, int which) {
                                    dialog.dismiss();
                                    finish();
                                }
                            });
                    alerDialog.setCancelable(false);
                    alerDialog.setTitle(getString(R.string.product_inquiry));
                    if (!TextUtils.isEmpty(response)) {
                        alerDialog.setMessage(response.replaceAll("\"",""));
                    } else {
                        alerDialog.setMessage(getString(R.string.thank_you_for_your_inquiry));
                    }

                    alerDialog.show();
                }
            }, new Response.ErrorListener() {
                @Override
                public void onErrorResponse(VolleyError error) {
                    Toast.makeText(BuyNowActivity.this, getString(R.string.some_thing_went_wrong_please_try_again), Toast.LENGTH_LONG).show();
                }
            }) {
                @Override
                public String getBodyContentType() {
                    return "application/json; charset=utf-8";
                }

                @Override
                public byte[] getBody() throws AuthFailureError {
                    try {
                        return requestBody == null ? null : requestBody.getBytes("utf-8");
                    } catch (UnsupportedEncodingException e) {
                        return null;
                    }
                }
            };

            requestQueue.add(stringRequest);
        } catch (JSONException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void onClick(View v) {
        switch (v.getId()) {
            case R.id.ivBack:
                finish();
                break;
            case R.id.btnOrder :
                if(!isValid()) {
                    Utils.hideKeyboard(mBtnOrder, BuyNowActivity.this);
                    if(mSpState.getSelectedItemPosition() == 0 && mRadIndia.isChecked()) {
                        DialogAlert.show_dialog(BuyNowActivity.this, getString(R.string.please_enter_state));
                    } else if(Utils.isConnectingToInternet(BuyNowActivity.this, true)) {
                        buyProduct();
                    }
                }
                break;
        }
    }
}
