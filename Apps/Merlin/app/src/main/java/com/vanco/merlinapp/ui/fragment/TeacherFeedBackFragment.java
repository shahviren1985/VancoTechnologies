package com.vanco.merlinapp.ui.fragment;

import android.content.DialogInterface;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v7.app.AlertDialog;
import android.support.v7.widget.DividerItemDecoration;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;

import com.google.gson.JsonArray;
import com.google.gson.JsonObject;
import com.vanco.merlinapp.R;
import com.vanco.merlinapp.adapter.FeedBackFieldAdapter;
import com.vanco.merlinapp.keyinterface.OnClick;
import com.vanco.merlinapp.modal.ClsFeedback;
import com.vanco.merlinapp.modal.ClsTeacher;
import com.vanco.merlinapp.network.RetrofitInterface;
import com.vanco.merlinapp.ui.activity.FragmentContainerActivity;
import com.vanco.merlinapp.utility.Utility;

import org.json.JSONException;

import java.util.ArrayList;

import butterknife.BindView;
import butterknife.ButterKnife;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class TeacherFeedBackFragment extends BaseFragment implements OnClick {

    @BindView(R.id.txtTeacherName)
    TextView txtTeacherName;

    @BindView(R.id.txtSubject)
    TextView txtSubject;

    @BindView(R.id.txtCode)
    TextView txtCode;

    @BindView(R.id.txtSemster)
    TextView txtSemster;

    @BindView(R.id.txtSubjectCode)
    TextView txtSubjectCode;

    @BindView(R.id.txtCourse)
    TextView txtCourse;

    @BindView(R.id.recyclerView)
    RecyclerView recyclerView;

    private ClsTeacher clsTeacher;
    private ArrayList<ClsFeedback> feedbackArrayList;
    private FeedBackFieldAdapter adapter;


    public static TeacherFeedBackFragment newInstance(ClsTeacher clsTeacher) {

        Bundle args = new Bundle();
        args.putSerializable("param", clsTeacher);

        TeacherFeedBackFragment fragment = new TeacherFeedBackFragment();
        fragment.setArguments(args);
        return fragment;
    }


    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        if (getArguments() != null) {
            clsTeacher = (ClsTeacher) getArguments().getSerializable("param");
        }


    }

    @Override
    public void onActivityCreated(@Nullable Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);
        ((FragmentContainerActivity) getActivity()).setOnClick(this);
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view = LayoutInflater.from(container.getContext()).inflate(R.layout.fragment_teacher_feed_back, null);
        ButterKnife.bind(this, view);
        init();
        return view;
    }

    private void init() {


        getFeedBackForm();
        txtTeacherName.setText(clsTeacher.getTeacherName());
        txtCode.setText("(" + clsTeacher.getTeacherCode() + ")");
        txtSemster.setText(getString(R.string.semester) + " " + clsTeacher.getSemester());
        txtSubject.setText(getString(R.string.subject) + " " + clsTeacher.getSubjectName() + "  (" + clsTeacher.getSubjectCode() + ")");
        txtCourse.setText(getString(R.string.course) + " " + clsTeacher.getCourse() + "  (" + clsTeacher.getSubCourse() + ")");

        LinearLayoutManager layoutManager = new LinearLayoutManager(getContext());
        recyclerView.setLayoutManager(layoutManager);
        DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(recyclerView.getContext(),
                layoutManager.getOrientation());
        recyclerView.addItemDecoration(dividerItemDecoration);
        recyclerView.setNestedScrollingEnabled(false);
    }

    private void setAdapter() {
        if (feedbackArrayList == null)
            return;

        recyclerView.setItemViewCacheSize(feedbackArrayList.size());
        adapter = new FeedBackFieldAdapter(feedbackArrayList);
        recyclerView.setAdapter(adapter);
    }

    public void getFeedBackForm() {

        Utility.showProgress(getString(R.string.msg_feedback_form), getActivity());
        Call<String> call = RetrofitInterface.callToMethod().getFeedBackDetail();
        call.enqueue(new Callback<String>() {
            @Override
            public void onResponse(Call<String> call, Response<String> response) {
                if (getActivity() == null)
                    return;

                Utility.cancelProgress();
                if (response != null && response.isSuccessful()) {
                    String res = response.body();
                    if (res != null) {
                        try {
                            feedbackArrayList = Utility.getFeedBackData(res);
                            if (feedbackArrayList == null || feedbackArrayList.size() < 0) {
                                Utility.toast(getActivity(), getString(R.string.msg_no_data_found));
                            } else {
                                setAdapter();
                            }

                        } catch (JSONException e) {
                            Utility.toast(getActivity(), getString(R.string.msg_response_error));
                            e.printStackTrace();
                        }
                    } else {
                        Utility.toast(getActivity(), getString(R.string.msg_response_error));
                    }

                } else {
                    Utility.toast(getActivity(), getString(R.string.msg_response_error));
                }

                Utility.cancelProgress();
            }

            @Override
            public void onFailure(Call<String> call, Throwable t) {
                if (getActivity() == null)
                    return;

                Utility.cancelProgress();
                RetrofitInterface.handleFailerError(getActivity(), t);
            }
        });
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
    }

    @Override
    public void onClickView(View v) {
        boolean allDataValidate = true;
        for (int i = 0; i < feedbackArrayList.size(); i++) {

            ClsFeedback clsFeedback = feedbackArrayList.get(i);

            View view = recyclerView.getLayoutManager().findViewByPosition(i);
            ImageView imgValidation = view.findViewById(R.id.imgValidation);

            if (clsFeedback.getType().equals("radio")) {
                RadioGroup radioGroup = view.findViewById(R.id.radioGroup);
                int selectedRadioButtonID = radioGroup.getCheckedRadioButtonId();

                if (selectedRadioButtonID != -1) {
                    imgValidation.setImageResource(R.drawable.circle_green);

                } else {
                    allDataValidate = false;
                    imgValidation.setImageResource(R.drawable.circle_red);
                }
            } else {
                EditText editText = view.findViewById(R.id.edtDescription);
                EditText editText2 = view.findViewById(R.id.edtDescription2);
                if (Utility.isValueNull(editText)) {
                    allDataValidate = false;
                    imgValidation.setImageResource(R.drawable.circle_red);
                } else {
                    imgValidation.setImageResource(R.drawable.circle_green);
                }

                if (Utility.isValueNull(editText2)) {
                    allDataValidate = false;
                    imgValidation.setImageResource(R.drawable.circle_red);
                } else {
                    imgValidation.setImageResource(R.drawable.circle_green);
                }
            }
        }

        if (!allDataValidate) {
            Utility.toast(getContext(), getString(R.string.msg_feedback_validation));
        } else {
            saveFeedBack();
        }


    }


    private void saveFeedBack() {
        final AlertDialog.Builder dialog = new AlertDialog.Builder(getActivity());
        dialog.setTitle(R.string.teacher_feedback);
        dialog.setMessage(R.string.msg_submit_feedback);
        dialog.setPositiveButton(R.string.submit, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialogInterface, int i) {
                addTeacherFeedBack();
            }
        });

        dialog.setNegativeButton(R.string.cancel, new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialogInterface, int i) {
            }
        });
        dialog.show();
    }


    private void addTeacherFeedBack() {

        if (!Utility.isInternetConnectionAvailable(getActivity())) {
            Utility.openAlertDialog(getActivity(), getString(R.string.msg_internet_not_available));
            return;
        }


        Utility.showProgress("Submitting feedback form,Please wait...", getActivity());


        Call<String> call = RetrofitInterface.callToMethod().addTeacherFeedBack(getQueryMap());
        call.enqueue(new Callback<String>() {
            @Override
            public void onResponse(Call<String> call, Response<String> response) {
                if (getActivity() == null)
                    return;


                if (response != null && response.isSuccessful()) {
                    String res = response.body();
                    Utility.toast(getActivity(), getString(R.string.msg_submitted_succesfully));
                    getActivity().finish();
                } else if (response != null && response.code() == 401) {
                    Utility.toast(getActivity(), getString(R.string.msg_response_error));
                } else {
                    Utility.toast(getActivity(), getString(R.string.msg_response_error));
                }

                Utility.cancelProgress();
            }

            @Override
            public void onFailure(Call<String> call, Throwable t) {

                if (getActivity() == null)
                    return;

                Utility.cancelProgress();
                RetrofitInterface.handleFailerError(getActivity(), t);
            }
        });
    }

    public JsonObject getQueryMap() {

        JsonObject jsonObject = new JsonObject();

        jsonObject.addProperty("userId", Utility.getUserId());
        jsonObject.addProperty("teacherCode", clsTeacher.getTeacherCode());
        jsonObject.addProperty("subjectCode", clsTeacher.getSubjectCode());
        jsonObject.addProperty("collegeCode", Utility.getCollageCode());


        for (int i = 0; i < feedbackArrayList.size(); i++) {

            ClsFeedback clsFeedback = feedbackArrayList.get(i);

            View view = recyclerView.getLayoutManager().findViewByPosition(i);
            ImageView imgValidation = view.findViewById(R.id.imgValidation);

            if (clsFeedback.getType().equals("radio")) {
                RadioGroup radioGroup = view.findViewById(R.id.radioGroup);
                int selectedRadioButtonID = radioGroup.getCheckedRadioButtonId();

                if (selectedRadioButtonID != -1) {
                    RadioButton radioButton = view.findViewById(selectedRadioButtonID);
                    jsonObject.addProperty("a" + (i + 1), radioButton.getText().toString());
                }
            } else {
                EditText editText = view.findViewById(R.id.edtDescription);
                JsonArray jsonArray = new JsonArray();
                jsonArray.add(editText.getText().toString());

                EditText editText2 = view.findViewById(R.id.edtDescription2);
                jsonArray.add(editText2.getText().toString());

                jsonObject.add("a" + (i + 1), jsonArray);
            }
        }


        return jsonObject;
    }

}
