package com.vanco.merlinapp.ui.fragment;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.content.ContextCompat;
import android.support.v7.widget.AppCompatButton;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.RelativeLayout;
import android.widget.TextView;

import com.google.gson.JsonArray;
import com.google.gson.JsonObject;
import com.vanco.merlinapp.R;
import com.vanco.merlinapp.keyinterface.OnPopupMenuClick;
import com.vanco.merlinapp.network.RetrofitInterface;
import com.vanco.merlinapp.ui.activity.FragmentContainerActivity;
import com.vanco.merlinapp.utility.Utility;

import java.util.ArrayList;
import java.util.Arrays;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import butterknife.Unbinder;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class ExitFormFragment extends BaseFragment implements View.OnClickListener, com.vanco.merlinapp.keyinterface.OnClick {

    @BindView(R.id.radioStudyFurther)
    RadioButton radioStudyFurther;
    @BindView(R.id.radioWork)
    RadioButton radioWork;
    @BindView(R.id.radioAnyOther)
    RadioButton radioAnyOther;
    @BindView(R.id.radioFurtherPlan)
    RadioGroup radioFurtherPlan;
    @BindView(R.id.edtMostValuable)
    EditText edtMostValuable;
    @BindView(R.id.edtValuable)
    EditText edtValuable;
    @BindView(R.id.edtLessValuable)
    EditText edtLessValuable;
    @BindView(R.id.edtPositiveEffect)
    EditText edtPositiveEffect;
    @BindView(R.id.edtNegativeEffect)
    EditText edtNegativeEffect;
    @BindView(R.id.radioYesKeepInTouch)
    RadioButton radioYesKeepInTouch;
    @BindView(R.id.radioNoKeepInTouch)
    RadioButton radioNoKeepInTouch;
    @BindView(R.id.radioGroupIKeepInTouch)
    RadioGroup radioGroupIKeepInTouch;
    @BindView(R.id.radioYesAdminExp)
    RadioButton radioYesAdminExp;
    @BindView(R.id.radioNoAdminExp)
    RadioButton radioNoAdminExp;
    @BindView(R.id.radioAdminExp)
    RadioGroup radioAdminExp;
    @BindView(R.id.edtNameOfDepartmentOne)
    EditText edtNameOfDepartmentOne;
    @BindView(R.id.edtPositiveCommentDeptOne)
    EditText edtPositiveCommentDeptOne;
    @BindView(R.id.edtNegativeCommentsDepartmentOne)
    EditText edtNegativeCommentsDepartmentOne;
    @BindView(R.id.edtNameOfDepartmentTwo)
    EditText edtNameOfDepartmentTwo;
    @BindView(R.id.edtPositiveCommentDeptTwo)
    EditText edtPositiveCommentDeptTwo;
    @BindView(R.id.edtNegativeCommentsDepartmentTwo)
    EditText edtNegativeCommentsDepartmentTwo;
    @BindView(R.id.radioYesExtraCar)
    RadioButton radioYesExtraCar;
    @BindView(R.id.radioNoExtraCar)
    RadioButton radioNoExtraCar;
    @BindView(R.id.radioNaCar)
    RadioButton radioNaCar;
    @BindView(R.id.raioExtraCarricular)
    RadioGroup raioExtraCarricular;
    @BindView(R.id.edtContributeToCollage)
    EditText edtContributeToCollage;
    @BindView(R.id.edtQualityOfFood)
    EditText edtQualityOfFood;
    @BindView(R.id.relQualityOfFood)
    RelativeLayout relQualityOfFood;
    @BindView(R.id.edtPriceOfFood)
    EditText edtPriceOfFood;
    @BindView(R.id.relPriceOfFood)
    RelativeLayout relPriceOfFood;
    @BindView(R.id.edtVarityOfFood)
    EditText edtVarityOfFood;
    @BindView(R.id.relVarityOfFood)
    RelativeLayout relVarityOfFood;
    @BindView(R.id.edtService)
    EditText edtService;
    @BindView(R.id.relService)
    RelativeLayout relService;
    @BindView(R.id.edtHygiene)
    EditText edtHygiene;
    @BindView(R.id.relHygiene)
    RelativeLayout relHygiene;
    @BindView(R.id.edtBookAvailablity)
    EditText edtBookAvailablity;
    @BindView(R.id.relBookAvailablity)
    RelativeLayout relBookAvailablity;
    @BindView(R.id.edtStaffHelping)
    EditText edtStaffHelping;
    @BindView(R.id.relStaffHelping)
    RelativeLayout relStaffHelping;
    @BindView(R.id.edtAdequteSpaceSitting)
    EditText edtAdequteSpaceSitting;
    @BindView(R.id.relAdequteSpaceSetting)
    RelativeLayout relAdequteSpaceSetting;
    @BindView(R.id.edtLibraryTimming)
    EditText edtLibraryTimming;
    @BindView(R.id.relLibraryTimming)
    RelativeLayout relLibraryTimming;
    @BindView(R.id.edtFrequencyOfVisit)
    EditText edtFrequencyOfVisit;
    @BindView(R.id.relFrequencyOfVisit)
    RelativeLayout relFrequencyOfVisit;
    @BindView(R.id.edtCooperation)
    EditText edtCooperation;
    @BindView(R.id.relCooperation)
    RelativeLayout relCooperation;
    @BindView(R.id.edtOfficeInformation)
    EditText edtOfficeInformation;
    @BindView(R.id.relOfficeInformation)
    RelativeLayout relOfficeInformation;
    @BindView(R.id.edtTimeliness)
    EditText edtTimeliness;
    @BindView(R.id.relTimeLines)
    RelativeLayout relTimeLines;

    @BindView(R.id.txtPoliteness)
    TextView txtPoliteness;

    @BindView(R.id.edtPoliteness)
    EditText edtPoliteness;
    @BindView(R.id.relPoliteness)
    RelativeLayout relPoliteness;
    @BindView(R.id.edtGymTimming)
    EditText edtGymTimming;
    @BindView(R.id.relGymTimming)
    RelativeLayout relGymTimming;
    @BindView(R.id.edtEnviroment)
    EditText edtEnviroment;
    @BindView(R.id.relEnviroment)
    RelativeLayout relEnviroment;
    @BindView(R.id.edtEquipment)
    EditText edtEquipment;
    @BindView(R.id.relEquipment)
    RelativeLayout relEquipment;
    @BindView(R.id.edtTrainning)
    EditText edtTrainning;
    @BindView(R.id.relTrainning)
    RelativeLayout relTrainning;
    @BindView(R.id.edtFacalities)
    EditText edtFacalities;
    @BindView(R.id.relFacalities)
    RelativeLayout relFacalities;
    @BindView(R.id.edtEquipments)
    EditText edtEquipments;
    @BindView(R.id.relEquipments)
    RelativeLayout relEquipments;
    @BindView(R.id.edtSportsTrainning)
    EditText edtSportsTrainning;
    @BindView(R.id.relSportsTraining)
    RelativeLayout relSportsTraining;
    @BindView(R.id.edtRegularHealthCheckUps)
    EditText edtRegularHealthCheckUps;
    @BindView(R.id.relRegularHealthCheckUps)
    RelativeLayout relRegularHealthCheckUps;
    @BindView(R.id.edtEmerganceMedicalFacality)
    EditText edtEmerganceMedicalFacality;
    @BindView(R.id.relEmergencyMedicalFacality)
    RelativeLayout relEmergencyMedicalFacality;
    @BindView(R.id.edtPhychologicalStudy)
    EditText edtPhychologicalStudy;
    @BindView(R.id.relPhychologicalService)
    RelativeLayout relPhychologicalService;
    Unbinder unbinder;

    ArrayList<String> arrayOfValuable = null;
    ArrayList<String> arrayOfMeasure = null;
    @BindView(R.id.relMostValuable)
    RelativeLayout relMostValuable;
    @BindView(R.id.relValuable)
    RelativeLayout relValuable;
    @BindView(R.id.relLessValuable)
    RelativeLayout relLessValuable;


    @BindView(R.id.txtValidation)
    TextView txtValidation;
    @BindView(R.id.txtOne)
    TextView txtOne;
    @BindView(R.id.txtQuestionOne)
    TextView txtQuestionOne;
    @BindView(R.id.txtTwo)
    TextView txtTwo;
    @BindView(R.id.txtQuestionTwo)
    TextView txtQuestionTwo;
    @BindView(R.id.txtMostValuable)
    TextView txtMostValuable;
    @BindView(R.id.txtValuable)
    TextView txtValuable;
    @BindView(R.id.txtLessValuable)
    TextView txtLessValuable;
    @BindView(R.id.txtThree)
    TextView txtThree;
    @BindView(R.id.txtQuestionThree)
    TextView txtQuestionThree;
    @BindView(R.id.txtPositiveEffect)
    TextView txtPositiveEffect;
    @BindView(R.id.txtNegativeEffect)
    TextView txtNegativeEffect;
    @BindView(R.id.txtFour)
    TextView txtFour;
    @BindView(R.id.txtQuestionFour)
    TextView txtQuestionFour;
    @BindView(R.id.txtFive)
    TextView txtFive;
    @BindView(R.id.txtQuestionFive)
    TextView txtQuestionFive;
    @BindView(R.id.txtSix)
    TextView txtSix;
    @BindView(R.id.txtQuestionSix)
    TextView txtQuestionSix;
    @BindView(R.id.txtNameOfDepartmentOne)
    TextView txtNameOfDepartmentOne;
    @BindView(R.id.txtDepartmentOnePositiveComments)
    TextView txtDepartmentOnePositiveComments;
    @BindView(R.id.txtDepartmentOneNegativeComments)
    TextView txtDepartmentOneNegativeComments;
    @BindView(R.id.txtNameOfDepartmentTwo)
    TextView txtNameOfDepartmentTwo;
    @BindView(R.id.txtDepartmentTwoPositiveComments)
    TextView txtDepartmentTwoPositiveComments;
    @BindView(R.id.txtDepartmentTwoNegativeComments)
    TextView txtDepartmentTwoNegativeComments;
    @BindView(R.id.txtSeven)
    TextView txtSeven;
    @BindView(R.id.txtQuestionSeven)
    TextView txtQuestionSeven;
    @BindView(R.id.txtEight)
    TextView txtEight;
    @BindView(R.id.txtQuestionEight)
    TextView txtQuestionEight;
    @BindView(R.id.txtNine)
    TextView txtNine;
    @BindView(R.id.txtQuestionNine)
    TextView txtQuestionNine;
    @BindView(R.id.txtNineOne)
    TextView txtNineOne;
    @BindView(R.id.txtQualityOfFood)
    TextView txtQualityOfFood;
    @BindView(R.id.txtPriceOfFood)
    TextView txtPriceOfFood;
    @BindView(R.id.txtVarityOfFood)
    TextView txtVarityOfFood;
    @BindView(R.id.txtService)
    TextView txtService;
    @BindView(R.id.txtHygine)
    TextView txtHygine;
    @BindView(R.id.txtBookAvailablity)
    TextView txtBookAvailablity;
    @BindView(R.id.txtStaffHelping)
    TextView txtStaffHelping;
    @BindView(R.id.txtAdequiteSpacing)
    TextView txtAdequiteSpacing;
    @BindView(R.id.txtLibraryTimming)
    TextView txtLibraryTimming;
    @BindView(R.id.txtFrequencyOfVisit)
    TextView txtFrequencyOfVisit;
    @BindView(R.id.txtCooperation)
    TextView txtCooperation;
    @BindView(R.id.txtOfficeInformation)
    TextView txtOfficeInformation;
    @BindView(R.id.txtOfficeTimeliness)
    TextView txtOfficeTimeliness;
    @BindView(R.id.txtGymTiming)
    TextView txtGymTiming;
    @BindView(R.id.txtGymEnviroment)
    TextView txtGymEnviroment;
    @BindView(R.id.txtGymEquipment)
    TextView txtGymEquipment;
    @BindView(R.id.txtGymTrainning)
    TextView txtGymTrainning;
    @BindView(R.id.txtFacalities)
    TextView txtFacalities;
    @BindView(R.id.txtSportsEquipment)
    TextView txtSportsEquipment;
    @BindView(R.id.txtSportTrainning)
    TextView txtSportTrainning;
    @BindView(R.id.txtReqularHealthCheckUps)
    TextView txtReqularHealthCheckUps;
    @BindView(R.id.txtEmergencyMedical)
    TextView txtEmergencyMedical;
    @BindView(R.id.txtPhyschologyConsuel)
    TextView txtPhyschologyConsuel;
    @BindView(R.id.extExtraActivity)
    EditText extExtraActivity;

    public static ExitFormFragment newInstance() {

        Bundle args = new Bundle();

        ExitFormFragment fragment = new ExitFormFragment();
        fragment.setArguments(args);
        return fragment;
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_exit_form, null);
        unbinder = ButterKnife.bind(this, view);
        init();
        return view;
    }

    private void init() {
        arrayOfValuable = new ArrayList<>(Arrays.asList(getResources().getStringArray(R.array.array_valuable)));
        arrayOfMeasure = new ArrayList<>(Arrays.asList(getResources().getStringArray(R.array.array_measure)));


    }

    @Override
    public void onActivityCreated(@Nullable Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);
        ((FragmentContainerActivity) getActivity()).setOnClick(this);
    }

    @OnClick({R.id.edtMostValuable,
            R.id.edtLessValuable,
            R.id.edtValuable,
            R.id.edtQualityOfFood,
            R.id.edtPriceOfFood,
            R.id.edtVarityOfFood,
            R.id.edtService,
            R.id.edtHygiene,
            R.id.edtBookAvailablity,
            R.id.edtStaffHelping,
            R.id.edtAdequteSpaceSitting,
            R.id.edtLibraryTimming,
            R.id.edtFrequencyOfVisit,
            R.id.edtCooperation,
            R.id.edtOfficeInformation,
            R.id.edtTimeliness,
            R.id.edtPoliteness,
            R.id.edtGymTimming,
            R.id.edtEnviroment,
            R.id.edtEquipment,
            R.id.edtTrainning,
            R.id.edtFacalities,
            R.id.edtEquipments,
            R.id.edtSportsTrainning,
            R.id.edtRegularHealthCheckUps,
            R.id.edtEmerganceMedicalFacality,
            R.id.edtPhychologicalStudy,
            R.id.radioNoExtraCar,
            R.id.radioYesExtraCar,
            R.id.radioNaCar
    })
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.radioNaCar:
            case R.id.radioNoExtraCar:
                extExtraActivity.setVisibility(View.GONE);
                break;
            case R.id.radioYesExtraCar:
                extExtraActivity.setVisibility(View.VISIBLE);
                break;
            case R.id.edtMostValuable:
                showPopup(edtMostValuable, arrayOfValuable);
                break;

            case R.id.edtValuable:
                showPopup(edtValuable, arrayOfValuable);
                break;

            case R.id.edtLessValuable:
                showPopup(edtLessValuable, arrayOfValuable);
                break;
            case R.id.edtQualityOfFood:
                showPopup(edtQualityOfFood, arrayOfMeasure);
                break;
            case R.id.edtPriceOfFood:
                showPopup(edtPriceOfFood, arrayOfMeasure);
                break;
            case R.id.edtVarityOfFood:
                showPopup(edtVarityOfFood, arrayOfMeasure);
                break;
            case R.id.edtService:
                showPopup(edtService, arrayOfMeasure);
                break;
            case R.id.edtHygiene:
                showPopup(edtHygiene, arrayOfMeasure);
                break;
            case R.id.edtBookAvailablity:
                showPopup(edtBookAvailablity, arrayOfMeasure);
                break;
            case R.id.edtStaffHelping:
                showPopup(edtStaffHelping, arrayOfMeasure);
                break;
            case R.id.edtAdequteSpaceSitting:
                showPopup(edtAdequteSpaceSitting, arrayOfMeasure);
                break;
            case R.id.edtLibraryTimming:
                showPopup(edtLibraryTimming, arrayOfMeasure);
                break;
            case R.id.edtFrequencyOfVisit:
                showPopup(edtFrequencyOfVisit, arrayOfMeasure);
                break;
            case R.id.edtCooperation:
                showPopup(edtCooperation, arrayOfMeasure);
                break;

            case R.id.edtOfficeInformation:
                showPopup(edtOfficeInformation, arrayOfMeasure);
                break;

            case R.id.edtTimeliness:
                showPopup(edtTimeliness, arrayOfMeasure);
                break;

            case R.id.edtPoliteness:
                showPopup(edtPoliteness, arrayOfMeasure);
                break;

            case R.id.edtGymTimming:
                showPopup(edtGymTimming, arrayOfMeasure);
                break;

            case R.id.edtEnviroment:
                showPopup(edtEnviroment, arrayOfMeasure);
                break;

            case R.id.edtEquipment:
                showPopup(edtEquipment, arrayOfMeasure);
                break;
            case R.id.edtTrainning:
                showPopup(edtTrainning, arrayOfMeasure);
                break;
            case R.id.edtFacalities:
                showPopup(edtFacalities, arrayOfMeasure);
                break;
            case R.id.edtEquipments:
                showPopup(edtEquipments, arrayOfMeasure);
                break;
            case R.id.edtSportsTrainning:
                showPopup(edtSportsTrainning, arrayOfMeasure);
                break;
            case R.id.edtRegularHealthCheckUps:
                showPopup(edtRegularHealthCheckUps, arrayOfMeasure);
                break;
            case R.id.edtEmerganceMedicalFacality:
                showPopup(edtEmerganceMedicalFacality, arrayOfMeasure);
                break;

            case R.id.edtPhychologicalStudy:
                showPopup(edtPhychologicalStudy, arrayOfMeasure);
                break;


        }
    }

    private void save() {
        if (!Utility.isInternetConnectionAvailable(getActivity())) {
            Utility.openAlertDialog(getActivity(), getString(R.string.msg_internet_not_available));
            return;
        }
        boolean isValidate = true;

        if (radioFurtherPlan.getCheckedRadioButtonId() == -1) {
            isValidate = false;
            txtQuestionOne.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtOne.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));

        } else {
            txtQuestionOne.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtOne.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtMostValuable)) {
            isValidate = false;

            txtQuestionTwo.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtTwo.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));

            txtMostValuable.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtQuestionTwo.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtTwo.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));

            txtMostValuable.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtValuable)) {
            isValidate = false;

            txtQuestionTwo.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtTwo.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));

            txtValuable.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtQuestionTwo.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtTwo.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));

            txtValuable.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtLessValuable)) {
            isValidate = false;

            txtQuestionTwo.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtTwo.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));

            txtLessValuable.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtQuestionTwo.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtTwo.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));

            txtLessValuable.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNull(edtPositiveEffect)) {
            isValidate = false;
            txtPositiveEffect.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtQuestionThree.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtThree.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));

        } else {
            txtPositiveEffect.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtQuestionThree.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtThree.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNull(edtNegativeEffect)) {
            isValidate = false;
            txtNegativeEffect.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtQuestionThree.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtThree.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));

        } else {
            txtNegativeEffect.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtQuestionThree.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtThree.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (radioGroupIKeepInTouch.getCheckedRadioButtonId() == -1) {
            isValidate = false;
            txtQuestionFour.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtFour.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));

        } else {
            txtQuestionFour.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtFour.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (radioAdminExp.getCheckedRadioButtonId() == -1) {
            isValidate = false;
            txtQuestionFive.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtFive.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));

        } else {
            txtQuestionFive.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtFive.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }


        if (Utility.isValueNull(edtNameOfDepartmentOne)) {
            isValidate = false;
            txtQuestionSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtNameOfDepartmentOne.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtNameOfDepartmentOne.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtQuestionSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNull(edtPositiveCommentDeptOne)) {
            isValidate = false;
            txtQuestionSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtDepartmentOnePositiveComments.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtQuestionSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtDepartmentOnePositiveComments.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNull(edtNegativeCommentsDepartmentOne)) {
            isValidate = false;
            txtQuestionSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtDepartmentOneNegativeComments.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtQuestionSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtDepartmentOneNegativeComments.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }


        if (Utility.isValueNull(edtNameOfDepartmentTwo)) {
            isValidate = false;
            txtQuestionSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtNameOfDepartmentTwo.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtNameOfDepartmentTwo.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtQuestionSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNull(edtPositiveCommentDeptTwo)) {
            isValidate = false;
            txtQuestionSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtDepartmentTwoPositiveComments.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtQuestionSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtDepartmentTwoPositiveComments.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNull(edtNegativeCommentsDepartmentTwo)) {
            isValidate = false;
            txtQuestionSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtDepartmentTwoNegativeComments.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtQuestionSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtSix.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtDepartmentTwoNegativeComments.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (raioExtraCarricular.getCheckedRadioButtonId() == -1) {
            isValidate = false;
            txtQuestionSeven.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtSeven.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));

        } else {
            txtQuestionSeven.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtSeven.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (radioYesExtraCar.isChecked() && Utility.isValueNull(extExtraActivity)) {
            isValidate = false;
            radioYesExtraCar.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            radioYesExtraCar.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNull(edtContributeToCollage)) {
            isValidate = false;
            txtQuestionEight.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
            txtEight.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtQuestionEight.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
            txtEight.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtQualityOfFood)) {
            isValidate = false;
            txtQualityOfFood.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtQualityOfFood.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtPriceOfFood)) {
            isValidate = false;
            txtPriceOfFood.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtPriceOfFood.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtVarityOfFood)) {
            isValidate = false;
            txtVarityOfFood.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtVarityOfFood.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtService)) {
            isValidate = false;
            txtService.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtService.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtHygiene)) {
            isValidate = false;
            txtHygine.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtHygine.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtBookAvailablity)) {
            isValidate = false;
            txtBookAvailablity.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtBookAvailablity.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtStaffHelping)) {
            isValidate = false;
            txtStaffHelping.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtStaffHelping.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtAdequteSpaceSitting)) {
            isValidate = false;
            txtAdequiteSpacing.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtAdequiteSpacing.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtLibraryTimming)) {
            isValidate = false;
            txtLibraryTimming.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtLibraryTimming.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtFrequencyOfVisit)) {
            isValidate = false;
            txtFrequencyOfVisit.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtFrequencyOfVisit.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtCooperation)) {
            isValidate = false;
            txtCooperation.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtCooperation.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtOfficeInformation)) {
            isValidate = false;
            txtOfficeInformation.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtOfficeInformation.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtTimeliness)) {
            isValidate = false;
            txtOfficeTimeliness.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtOfficeTimeliness.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtPoliteness)) {
            isValidate = false;
            txtPoliteness.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtPoliteness.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtGymTimming)) {
            isValidate = false;
            txtGymTiming.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtGymTiming.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtEnviroment)) {
            isValidate = false;
            txtGymEnviroment.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtGymEnviroment.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtEquipment)) {
            isValidate = false;
            txtGymEquipment.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtGymEquipment.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtTrainning)) {
            isValidate = false;
            txtGymTrainning.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtGymTrainning.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtFacalities)) {
            isValidate = false;
            txtFacalities.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtFacalities.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtEquipments)) {
            isValidate = false;
            txtSportsEquipment.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtSportsEquipment.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtSportsTrainning)) {
            isValidate = false;
            txtSportTrainning.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtSportTrainning.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtRegularHealthCheckUps)) {
            isValidate = false;
            txtReqularHealthCheckUps.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtReqularHealthCheckUps.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtEmerganceMedicalFacality)) {
            isValidate = false;
            txtEmergencyMedical.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtEmergencyMedical.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (Utility.isValueNullExit(edtPhychologicalStudy)) {
            isValidate = false;
            txtPhyschologyConsuel.setTextColor(ContextCompat.getColor(getActivity(), R.color.red));
        } else {
            txtPhyschologyConsuel.setTextColor(ContextCompat.getColor(getActivity(), R.color.black));
        }

        if (!isValidate) {
            txtValidation.setVisibility(View.VISIBLE);
            return;
        } else {
            txtValidation.setVisibility(View.GONE);
        }

        addExitForm();
    }

    private void addExitForm() {

        Utility.showProgress("Submitting exit form,Please wait...", getActivity());


        Call<String> call = RetrofitInterface.callToMethod().addExitForm(getObject());
        call.enqueue(new Callback<String>() {
            @Override
            public void onResponse(Call<String> call, Response<String> response) {
                if (getActivity() == null)
                    return;


                if (response != null && response.isSuccessful()) {
                    String res = response.body();
                    Utility.toast(getActivity(), getString(R.string.msg_exit_form_submitted_succesfully));
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

    private JsonObject getObject() {
        JsonObject jsonObject = new JsonObject();

        jsonObject.addProperty("userId", Utility.getUserId());

        //Question one
        RadioButton radioButton = radioFurtherPlan.findViewById(radioFurtherPlan.getCheckedRadioButtonId());
        jsonObject.addProperty("a1", (String) radioButton.getTag());

        // Question two
        JsonObject jsonQuestionTwo = new JsonObject();
        jsonQuestionTwo.addProperty("mostValuable", edtMostValuable.getText().toString());
        jsonQuestionTwo.addProperty("valuable", edtValuable.getText().toString());
        jsonQuestionTwo.addProperty("lessValuable", edtLessValuable.getText().toString());
        jsonObject.add("a2", jsonQuestionTwo);

        //Question three
        JsonObject jsonQuestionThree = new JsonObject();
        jsonQuestionThree.addProperty("postiveEffect", edtPositiveEffect.getText().toString());
        jsonQuestionThree.addProperty("negativeEffect", edtNegativeEffect.getText().toString());
        jsonObject.add("a3", jsonQuestionThree);

        //Question four
        if (radioYesKeepInTouch.isChecked()) {
            jsonObject.addProperty("a4", "Yes");
        } else {
            jsonObject.addProperty("a4", "No");
        }

        //Question five
        if (radioYesAdminExp.isChecked()) {
            jsonObject.addProperty("a5", radioYesAdminExp.getText().toString());
        } else {
            jsonObject.addProperty("a5", radioNoAdminExp.getText().toString());
        }

        //Question six
        JsonArray jsonArray = new JsonArray();

        JsonObject jsonObjectDeptOne = new JsonObject();
        jsonObjectDeptOne.addProperty("deptName", edtNameOfDepartmentOne.getText().toString());
        jsonObjectDeptOne.addProperty("positiveComment", edtPositiveCommentDeptOne.getText().toString());
        jsonObjectDeptOne.addProperty("negativeComment", edtNegativeCommentsDepartmentOne.getText().toString());
        jsonArray.add(jsonObjectDeptOne);

        JsonObject jsonObjectDeptTwo = new JsonObject();
        jsonObjectDeptTwo.addProperty("deptName", edtNameOfDepartmentTwo.getText().toString());
        jsonObjectDeptTwo.addProperty("positiveComment", edtPositiveCommentDeptTwo.getText().toString());
        jsonObjectDeptTwo.addProperty("negativeComment", edtNegativeCommentsDepartmentTwo.getText().toString());
        jsonArray.add(jsonObjectDeptTwo);

        jsonObject.add("a6", jsonArray);

        //Question seven
        JsonObject jsonQuestionSeven = new JsonObject();
        if (radioYesExtraCar.isChecked()) {
            jsonQuestionSeven.addProperty("isParticipated", "Yes");
            jsonQuestionSeven.addProperty("eventName", extExtraActivity.getText().toString().trim());
        } else if (radioNoExtraCar.isChecked()) {
            jsonQuestionSeven.addProperty("isParticipated", "No");
            jsonQuestionSeven.addProperty("eventName", "");
        } else {
            jsonQuestionSeven.addProperty("isParticipated", "N.A.");
            jsonQuestionSeven.addProperty("eventName", "");
        }

        jsonObject.add("a7", jsonQuestionSeven);

        //Question eight
        jsonObject.addProperty("a8", edtContributeToCollage.getText().toString().trim());

        //Question nine
        JsonArray jsonArrayNine = new JsonArray();

        JsonObject jsonObjectCanteen = new JsonObject();
        jsonObjectCanteen.addProperty("deptName", "Canteen");
        jsonObjectCanteen.addProperty("a1", Utility.getRating(edtQualityOfFood.getText().toString().trim()));
        jsonObjectCanteen.addProperty("a2", Utility.getRating(edtPriceOfFood.getText().toString().trim()));
        jsonObjectCanteen.addProperty("a3", Utility.getRating(edtVarityOfFood.getText().toString().trim()));
        jsonObjectCanteen.addProperty("a4", Utility.getRating(edtService.getText().toString().trim()));
        jsonObjectCanteen.addProperty("a5", Utility.getRating(edtHygiene.getText().toString().trim()));
        jsonArrayNine.add(jsonObjectCanteen);

        JsonObject jsonObjectLibrary = new JsonObject();
        jsonObjectLibrary.addProperty("deptName", "Library");
        jsonObjectLibrary.addProperty("a1", Utility.getRating(edtBookAvailablity.getText().toString().trim()));
        jsonObjectLibrary.addProperty("a2", Utility.getRating(edtStaffHelping.getText().toString().trim()));
        jsonObjectLibrary.addProperty("a3", Utility.getRating(edtAdequteSpaceSitting.getText().toString().trim()));
        jsonObjectLibrary.addProperty("a4", Utility.getRating(edtLibraryTimming.getText().toString().trim()));
        jsonObjectLibrary.addProperty("a5", Utility.getRating(edtFrequencyOfVisit.getText().toString().trim()));
        jsonArrayNine.add(jsonObjectLibrary);

        JsonObject jsonObjectOffice = new JsonObject();
        jsonObjectOffice.addProperty("deptName", "Office");
        jsonObjectOffice.addProperty("a1", Utility.getRating(edtCooperation.getText().toString().trim()));
        jsonObjectOffice.addProperty("a2", Utility.getRating(edtOfficeInformation.getText().toString().trim()));
        jsonObjectOffice.addProperty("a3", Utility.getRating(edtTimeliness.getText().toString().trim()));
        jsonObjectOffice.addProperty("a4", Utility.getRating(edtPoliteness.getText().toString().trim()));
        jsonArrayNine.add(jsonObjectOffice);

        JsonObject jsonObjectGym = new JsonObject();
        jsonObjectGym.addProperty("deptName", "Gym/Fitness Center");
        jsonObjectGym.addProperty("a1", Utility.getRating(edtGymTimming.getText().toString().trim()));
        jsonObjectGym.addProperty("a2", Utility.getRating(edtEnviroment.getText().toString().trim()));
        jsonObjectGym.addProperty("a3", Utility.getRating(edtEquipment.getText().toString().trim()));
        jsonObjectGym.addProperty("a4", Utility.getRating(edtTrainning.getText().toString().trim()));
        jsonArrayNine.add(jsonObjectGym);

        JsonObject jsonObjectSport = new JsonObject();
        jsonObjectSport.addProperty("deptName", "Sport");
        jsonObjectSport.addProperty("a1", Utility.getRating(edtFacalities.getText().toString().trim()));
        jsonObjectSport.addProperty("a2", Utility.getRating(edtEquipments.getText().toString().trim()));
        jsonObjectSport.addProperty("a3", Utility.getRating(edtSportsTrainning.getText().toString().trim()));
        jsonArrayNine.add(jsonObjectSport);

        JsonObject jsonObjectHealthService = new JsonObject();
        jsonObjectHealthService.addProperty("deptName", "Health Services");
        jsonObjectHealthService.addProperty("a1", Utility.getRating(edtRegularHealthCheckUps.getText().toString().trim()));
        jsonObjectHealthService.addProperty("a2", Utility.getRating(edtEmerganceMedicalFacality.getText().toString().trim()));
        jsonObjectHealthService.addProperty("a3", Utility.getRating(edtPhychologicalStudy.getText().toString().trim()));
        jsonArrayNine.add(jsonObjectHealthService);

        jsonObject.add("a9", jsonArrayNine);


        return jsonObject;
    }


    public void showPopup(final EditText editText, ArrayList<String> items) {
        Utility.showPopup(items, editText, new OnPopupMenuClick() {
            @Override
            public void onPopupMenuItemClick(int pos, String title) {
                editText.setText(title);
            }
        });
    }

    @Override
    public void onDestroyView() {
        super.onDestroyView();
        unbinder.unbind();
    }

    @Override
    public void onClickView(View view) {
        save();
    }
}
