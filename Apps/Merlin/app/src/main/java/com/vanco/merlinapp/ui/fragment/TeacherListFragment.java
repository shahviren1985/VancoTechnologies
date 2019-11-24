package com.vanco.merlinapp.ui.fragment;

import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v7.widget.DividerItemDecoration;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.google.gson.Gson;
import com.vanco.merlinapp.R;
import com.vanco.merlinapp.adapter.TeacherAdapter;
import com.vanco.merlinapp.keyinterface.Constants;
import com.vanco.merlinapp.keyinterface.OnItemClick;
import com.vanco.merlinapp.modal.ClsLoginResponse;
import com.vanco.merlinapp.modal.ClsTeacher;
import com.vanco.merlinapp.modal.ClsTeacherFeedBackFill;
import com.vanco.merlinapp.network.RetrofitInterface;
import com.vanco.merlinapp.ui.activity.FragmentContainerActivity;
import com.vanco.merlinapp.utility.Utility;

import org.json.JSONException;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class TeacherListFragment extends BaseFragment implements OnItemClick {


    @BindView(R.id.recyclerView)
    RecyclerView recyclerView;

    private TeacherAdapter adapter;
    private ArrayList<ClsTeacher> teachers;

    public static TeacherListFragment newInstance() {

        Bundle args = new Bundle();

        TeacherListFragment fragment = new TeacherListFragment();
        fragment.setArguments(args);

        return fragment;

    }

    private void setFeedBack() {
        List<ClsTeacherFeedBackFill> clsTeacherFeedBackFills = Utility.getTeacherFeedBackList();
        if (clsTeacherFeedBackFills == null || clsTeacherFeedBackFills.size() < 1)
            return;
        if (teachers == null)
            return;

        for (int i = 0; i < teachers.size(); i++) {
            for (int j = 0; j < clsTeacherFeedBackFills.size(); j++) {
                if (clsTeacherFeedBackFills.get(j).getTeacherCode().equalsIgnoreCase(teachers.get(i).getTeacherCode())
                        &&
                        clsTeacherFeedBackFills.get(j).getSubjectCode().equalsIgnoreCase(teachers.get(i).getSubjectCode())) {
                    teachers.get(i).setSendFeedBack(true);
                }
            }
        }
    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_teacher_list, null);
        ButterKnife.bind(this, view);
        init();
        return view;
    }

    private void init() {

        getTeacherList();
        LinearLayoutManager layoutManager = new LinearLayoutManager(getContext());
        recyclerView.setLayoutManager(layoutManager);
        DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(recyclerView.getContext(),
                layoutManager.getOrientation());
        recyclerView.addItemDecoration(dividerItemDecoration);


    }

    private void setAdapter(boolean msg) {
        teachers = filterList(teachers);

        if (msg && (teachers == null || teachers.size() < 1)) {
            Utility.toast(getActivity(), getString(R.string.msg_no_data_found));
        }
        setFeedBack();

        if (adapter == null) {
            adapter = new TeacherAdapter(teachers);
            adapter.setOnItemClick(this);
            recyclerView.setAdapter(adapter);
        } else {
            adapter.setList(teachers);
            adapter.notifyDataSetChanged();
        }
    }

    private ArrayList<ClsTeacher> filterList(ArrayList<ClsTeacher> teachers) {

        if (teachers == null)
            return null;

        String res = Utility.getStringValue(LOGIN_DATA);
        ClsLoginResponse clsLoginResponse = new Gson().fromJson(res, ClsLoginResponse.class);
        if (clsLoginResponse == null)
            return null;

        String courceName = clsLoginResponse.getCourse();
        String subCource = clsLoginResponse.getSubCourse();
        String currentSemster = clsLoginResponse.getCurrentSemester();
        ArrayList<ClsTeacher> filterdArraylist = new ArrayList<>();
        for (int i = 0; i < teachers.size(); i++) {
            ClsTeacher clsTeacher = teachers.get(i);
            if (clsTeacher.getCourse().equalsIgnoreCase(courceName) &&
                    clsTeacher.getSubCourse().equalsIgnoreCase(subCource)
                    && clsTeacher.getSemester().equalsIgnoreCase(currentSemster)) {
                filterdArraylist.add(clsTeacher);
            }
        }
        return filterdArraylist;
    }

    public void getTeacherList() {

        Utility.showProgress(getString(R.string.msg_fetch_teacher), getActivity());
        Call<String> call = RetrofitInterface.callToMethod().getTeacherDetails(Utility.getCollageCode());
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
                            teachers = Utility.getTeacherList(res);
                            if (teachers == null || teachers.size() < 0) {
                                Utility.toast(getActivity(), getString(R.string.msg_no_data_found));
                            } else {
                                setAdapter(true);
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
    public void onItemClick(int position, ClsTeacher clsTeacher) {
        Intent intent = new Intent(getActivity(), FragmentContainerActivity.class);
        intent.putExtra("param1", clsTeacher);
        intent.putExtra("param", TeacherFeedBackFragment.class.getSimpleName());
        startActivity(intent);
    }

    @Override
    public void onResume() {
        super.onResume();
        loginCall();

    }

    private void loginCall() {

        if (!Utility.isInternetConnectionAvailable(getActivity())) {
            return;
        }


        Call<String> call = RetrofitInterface.callToMethod().login(getQueryMap());
        call.enqueue(new Callback<String>() {
            @Override
            public void onResponse(Call<String> call, Response<String> response) {
                if (getActivity() == null)
                    return;


                if (response != null && response.isSuccessful()) {
                    String res = response.body();
                    ClsLoginResponse clsLoginResponse = new Gson().fromJson(res, ClsLoginResponse.class);
                    if (clsLoginResponse != null && clsLoginResponse.getSuccess()) {
                        Utility.setValue(Constants.LOGIN_DATA, res);
                        setAdapter(false);
                    } else {
                        if (clsLoginResponse == null) {
                        } else {
                        }
                    }
                } else if (response != null && response.code() == 401) {
                } else {
                }

            }

            @Override
            public void onFailure(Call<String> call, Throwable t) {


            }
        });
    }

    public HashMap<String, Object> getQueryMap() {
        HashMap<String, Object> map = new HashMap<>();
        map.put("mobileNumber", Utility.getStringValue(MOBILE_NUMBER));
        map.put("lastName", Utility.getStringValue(LAST_NAME));
        return map;
    }

}
