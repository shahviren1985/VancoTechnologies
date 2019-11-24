// Generated code from Butter Knife. Do not modify!
package com.vanco.merlinapp.ui.fragment;

import android.support.annotation.CallSuper;
import android.support.annotation.UiThread;
import android.view.View;
import android.widget.EditText;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.RelativeLayout;
import android.widget.TextView;
import butterknife.Unbinder;
import butterknife.internal.DebouncingOnClickListener;
import butterknife.internal.Utils;
import com.vanco.merlinapp.R;
import java.lang.IllegalStateException;
import java.lang.Override;

public class ExitFormFragment_ViewBinding implements Unbinder {
  private ExitFormFragment target;

  private View view2131296344;

  private View view2131296364;

  private View view2131296341;

  private View view2131296452;

  private View view2131296447;

  private View view2131296445;

  private View view2131296357;

  private View view2131296356;

  private View view2131296365;

  private View view2131296359;

  private View view2131296339;

  private View view2131296327;

  private View view2131296361;

  private View view2131296326;

  private View view2131296342;

  private View view2131296337;

  private View view2131296329;

  private View view2131296350;

  private View view2131296362;

  private View view2131296352;

  private View view2131296338;

  private View view2131296333;

  private View view2131296334;

  private View view2131296363;

  private View view2131296336;

  private View view2131296335;

  private View view2131296360;

  private View view2131296358;

  private View view2131296332;

  private View view2131296351;

  @UiThread
  public ExitFormFragment_ViewBinding(final ExitFormFragment target, View source) {
    this.target = target;

    View view;
    target.radioStudyFurther = Utils.findRequiredViewAsType(source, R.id.radioStudyFurther, "field 'radioStudyFurther'", RadioButton.class);
    target.radioWork = Utils.findRequiredViewAsType(source, R.id.radioWork, "field 'radioWork'", RadioButton.class);
    target.radioAnyOther = Utils.findRequiredViewAsType(source, R.id.radioAnyOther, "field 'radioAnyOther'", RadioButton.class);
    target.radioFurtherPlan = Utils.findRequiredViewAsType(source, R.id.radioFurtherPlan, "field 'radioFurtherPlan'", RadioGroup.class);
    view = Utils.findRequiredView(source, R.id.edtMostValuable, "field 'edtMostValuable' and method 'onClick'");
    target.edtMostValuable = Utils.castView(view, R.id.edtMostValuable, "field 'edtMostValuable'", EditText.class);
    view2131296344 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    view = Utils.findRequiredView(source, R.id.edtValuable, "field 'edtValuable' and method 'onClick'");
    target.edtValuable = Utils.castView(view, R.id.edtValuable, "field 'edtValuable'", EditText.class);
    view2131296364 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    view = Utils.findRequiredView(source, R.id.edtLessValuable, "field 'edtLessValuable' and method 'onClick'");
    target.edtLessValuable = Utils.castView(view, R.id.edtLessValuable, "field 'edtLessValuable'", EditText.class);
    view2131296341 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.edtPositiveEffect = Utils.findRequiredViewAsType(source, R.id.edtPositiveEffect, "field 'edtPositiveEffect'", EditText.class);
    target.edtNegativeEffect = Utils.findRequiredViewAsType(source, R.id.edtNegativeEffect, "field 'edtNegativeEffect'", EditText.class);
    target.radioYesKeepInTouch = Utils.findRequiredViewAsType(source, R.id.radioYesKeepInTouch, "field 'radioYesKeepInTouch'", RadioButton.class);
    target.radioNoKeepInTouch = Utils.findRequiredViewAsType(source, R.id.radioNoKeepInTouch, "field 'radioNoKeepInTouch'", RadioButton.class);
    target.radioGroupIKeepInTouch = Utils.findRequiredViewAsType(source, R.id.radioGroupIKeepInTouch, "field 'radioGroupIKeepInTouch'", RadioGroup.class);
    target.radioYesAdminExp = Utils.findRequiredViewAsType(source, R.id.radioYesAdminExp, "field 'radioYesAdminExp'", RadioButton.class);
    target.radioNoAdminExp = Utils.findRequiredViewAsType(source, R.id.radioNoAdminExp, "field 'radioNoAdminExp'", RadioButton.class);
    target.radioAdminExp = Utils.findRequiredViewAsType(source, R.id.radioAdminExp, "field 'radioAdminExp'", RadioGroup.class);
    target.edtNameOfDepartmentOne = Utils.findRequiredViewAsType(source, R.id.edtNameOfDepartmentOne, "field 'edtNameOfDepartmentOne'", EditText.class);
    target.edtPositiveCommentDeptOne = Utils.findRequiredViewAsType(source, R.id.edtPositiveCommentDeptOne, "field 'edtPositiveCommentDeptOne'", EditText.class);
    target.edtNegativeCommentsDepartmentOne = Utils.findRequiredViewAsType(source, R.id.edtNegativeCommentsDepartmentOne, "field 'edtNegativeCommentsDepartmentOne'", EditText.class);
    target.edtNameOfDepartmentTwo = Utils.findRequiredViewAsType(source, R.id.edtNameOfDepartmentTwo, "field 'edtNameOfDepartmentTwo'", EditText.class);
    target.edtPositiveCommentDeptTwo = Utils.findRequiredViewAsType(source, R.id.edtPositiveCommentDeptTwo, "field 'edtPositiveCommentDeptTwo'", EditText.class);
    target.edtNegativeCommentsDepartmentTwo = Utils.findRequiredViewAsType(source, R.id.edtNegativeCommentsDepartmentTwo, "field 'edtNegativeCommentsDepartmentTwo'", EditText.class);
    view = Utils.findRequiredView(source, R.id.radioYesExtraCar, "field 'radioYesExtraCar' and method 'onClick'");
    target.radioYesExtraCar = Utils.castView(view, R.id.radioYesExtraCar, "field 'radioYesExtraCar'", RadioButton.class);
    view2131296452 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    view = Utils.findRequiredView(source, R.id.radioNoExtraCar, "field 'radioNoExtraCar' and method 'onClick'");
    target.radioNoExtraCar = Utils.castView(view, R.id.radioNoExtraCar, "field 'radioNoExtraCar'", RadioButton.class);
    view2131296447 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    view = Utils.findRequiredView(source, R.id.radioNaCar, "field 'radioNaCar' and method 'onClick'");
    target.radioNaCar = Utils.castView(view, R.id.radioNaCar, "field 'radioNaCar'", RadioButton.class);
    view2131296445 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.raioExtraCarricular = Utils.findRequiredViewAsType(source, R.id.raioExtraCarricular, "field 'raioExtraCarricular'", RadioGroup.class);
    target.edtContributeToCollage = Utils.findRequiredViewAsType(source, R.id.edtContributeToCollage, "field 'edtContributeToCollage'", EditText.class);
    view = Utils.findRequiredView(source, R.id.edtQualityOfFood, "field 'edtQualityOfFood' and method 'onClick'");
    target.edtQualityOfFood = Utils.castView(view, R.id.edtQualityOfFood, "field 'edtQualityOfFood'", EditText.class);
    view2131296357 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relQualityOfFood = Utils.findRequiredViewAsType(source, R.id.relQualityOfFood, "field 'relQualityOfFood'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtPriceOfFood, "field 'edtPriceOfFood' and method 'onClick'");
    target.edtPriceOfFood = Utils.castView(view, R.id.edtPriceOfFood, "field 'edtPriceOfFood'", EditText.class);
    view2131296356 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relPriceOfFood = Utils.findRequiredViewAsType(source, R.id.relPriceOfFood, "field 'relPriceOfFood'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtVarityOfFood, "field 'edtVarityOfFood' and method 'onClick'");
    target.edtVarityOfFood = Utils.castView(view, R.id.edtVarityOfFood, "field 'edtVarityOfFood'", EditText.class);
    view2131296365 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relVarityOfFood = Utils.findRequiredViewAsType(source, R.id.relVarityOfFood, "field 'relVarityOfFood'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtService, "field 'edtService' and method 'onClick'");
    target.edtService = Utils.castView(view, R.id.edtService, "field 'edtService'", EditText.class);
    view2131296359 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relService = Utils.findRequiredViewAsType(source, R.id.relService, "field 'relService'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtHygiene, "field 'edtHygiene' and method 'onClick'");
    target.edtHygiene = Utils.castView(view, R.id.edtHygiene, "field 'edtHygiene'", EditText.class);
    view2131296339 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relHygiene = Utils.findRequiredViewAsType(source, R.id.relHygiene, "field 'relHygiene'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtBookAvailablity, "field 'edtBookAvailablity' and method 'onClick'");
    target.edtBookAvailablity = Utils.castView(view, R.id.edtBookAvailablity, "field 'edtBookAvailablity'", EditText.class);
    view2131296327 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relBookAvailablity = Utils.findRequiredViewAsType(source, R.id.relBookAvailablity, "field 'relBookAvailablity'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtStaffHelping, "field 'edtStaffHelping' and method 'onClick'");
    target.edtStaffHelping = Utils.castView(view, R.id.edtStaffHelping, "field 'edtStaffHelping'", EditText.class);
    view2131296361 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relStaffHelping = Utils.findRequiredViewAsType(source, R.id.relStaffHelping, "field 'relStaffHelping'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtAdequteSpaceSitting, "field 'edtAdequteSpaceSitting' and method 'onClick'");
    target.edtAdequteSpaceSitting = Utils.castView(view, R.id.edtAdequteSpaceSitting, "field 'edtAdequteSpaceSitting'", EditText.class);
    view2131296326 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relAdequteSpaceSetting = Utils.findRequiredViewAsType(source, R.id.relAdequteSpaceSetting, "field 'relAdequteSpaceSetting'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtLibraryTimming, "field 'edtLibraryTimming' and method 'onClick'");
    target.edtLibraryTimming = Utils.castView(view, R.id.edtLibraryTimming, "field 'edtLibraryTimming'", EditText.class);
    view2131296342 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relLibraryTimming = Utils.findRequiredViewAsType(source, R.id.relLibraryTimming, "field 'relLibraryTimming'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtFrequencyOfVisit, "field 'edtFrequencyOfVisit' and method 'onClick'");
    target.edtFrequencyOfVisit = Utils.castView(view, R.id.edtFrequencyOfVisit, "field 'edtFrequencyOfVisit'", EditText.class);
    view2131296337 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relFrequencyOfVisit = Utils.findRequiredViewAsType(source, R.id.relFrequencyOfVisit, "field 'relFrequencyOfVisit'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtCooperation, "field 'edtCooperation' and method 'onClick'");
    target.edtCooperation = Utils.castView(view, R.id.edtCooperation, "field 'edtCooperation'", EditText.class);
    view2131296329 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relCooperation = Utils.findRequiredViewAsType(source, R.id.relCooperation, "field 'relCooperation'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtOfficeInformation, "field 'edtOfficeInformation' and method 'onClick'");
    target.edtOfficeInformation = Utils.castView(view, R.id.edtOfficeInformation, "field 'edtOfficeInformation'", EditText.class);
    view2131296350 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relOfficeInformation = Utils.findRequiredViewAsType(source, R.id.relOfficeInformation, "field 'relOfficeInformation'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtTimeliness, "field 'edtTimeliness' and method 'onClick'");
    target.edtTimeliness = Utils.castView(view, R.id.edtTimeliness, "field 'edtTimeliness'", EditText.class);
    view2131296362 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relTimeLines = Utils.findRequiredViewAsType(source, R.id.relTimeLines, "field 'relTimeLines'", RelativeLayout.class);
    target.txtPoliteness = Utils.findRequiredViewAsType(source, R.id.txtPoliteness, "field 'txtPoliteness'", TextView.class);
    view = Utils.findRequiredView(source, R.id.edtPoliteness, "field 'edtPoliteness' and method 'onClick'");
    target.edtPoliteness = Utils.castView(view, R.id.edtPoliteness, "field 'edtPoliteness'", EditText.class);
    view2131296352 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relPoliteness = Utils.findRequiredViewAsType(source, R.id.relPoliteness, "field 'relPoliteness'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtGymTimming, "field 'edtGymTimming' and method 'onClick'");
    target.edtGymTimming = Utils.castView(view, R.id.edtGymTimming, "field 'edtGymTimming'", EditText.class);
    view2131296338 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relGymTimming = Utils.findRequiredViewAsType(source, R.id.relGymTimming, "field 'relGymTimming'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtEnviroment, "field 'edtEnviroment' and method 'onClick'");
    target.edtEnviroment = Utils.castView(view, R.id.edtEnviroment, "field 'edtEnviroment'", EditText.class);
    view2131296333 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relEnviroment = Utils.findRequiredViewAsType(source, R.id.relEnviroment, "field 'relEnviroment'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtEquipment, "field 'edtEquipment' and method 'onClick'");
    target.edtEquipment = Utils.castView(view, R.id.edtEquipment, "field 'edtEquipment'", EditText.class);
    view2131296334 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relEquipment = Utils.findRequiredViewAsType(source, R.id.relEquipment, "field 'relEquipment'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtTrainning, "field 'edtTrainning' and method 'onClick'");
    target.edtTrainning = Utils.castView(view, R.id.edtTrainning, "field 'edtTrainning'", EditText.class);
    view2131296363 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relTrainning = Utils.findRequiredViewAsType(source, R.id.relTrainning, "field 'relTrainning'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtFacalities, "field 'edtFacalities' and method 'onClick'");
    target.edtFacalities = Utils.castView(view, R.id.edtFacalities, "field 'edtFacalities'", EditText.class);
    view2131296336 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relFacalities = Utils.findRequiredViewAsType(source, R.id.relFacalities, "field 'relFacalities'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtEquipments, "field 'edtEquipments' and method 'onClick'");
    target.edtEquipments = Utils.castView(view, R.id.edtEquipments, "field 'edtEquipments'", EditText.class);
    view2131296335 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relEquipments = Utils.findRequiredViewAsType(source, R.id.relEquipments, "field 'relEquipments'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtSportsTrainning, "field 'edtSportsTrainning' and method 'onClick'");
    target.edtSportsTrainning = Utils.castView(view, R.id.edtSportsTrainning, "field 'edtSportsTrainning'", EditText.class);
    view2131296360 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relSportsTraining = Utils.findRequiredViewAsType(source, R.id.relSportsTraining, "field 'relSportsTraining'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtRegularHealthCheckUps, "field 'edtRegularHealthCheckUps' and method 'onClick'");
    target.edtRegularHealthCheckUps = Utils.castView(view, R.id.edtRegularHealthCheckUps, "field 'edtRegularHealthCheckUps'", EditText.class);
    view2131296358 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relRegularHealthCheckUps = Utils.findRequiredViewAsType(source, R.id.relRegularHealthCheckUps, "field 'relRegularHealthCheckUps'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtEmerganceMedicalFacality, "field 'edtEmerganceMedicalFacality' and method 'onClick'");
    target.edtEmerganceMedicalFacality = Utils.castView(view, R.id.edtEmerganceMedicalFacality, "field 'edtEmerganceMedicalFacality'", EditText.class);
    view2131296332 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relEmergencyMedicalFacality = Utils.findRequiredViewAsType(source, R.id.relEmergencyMedicalFacality, "field 'relEmergencyMedicalFacality'", RelativeLayout.class);
    view = Utils.findRequiredView(source, R.id.edtPhychologicalStudy, "field 'edtPhychologicalStudy' and method 'onClick'");
    target.edtPhychologicalStudy = Utils.castView(view, R.id.edtPhychologicalStudy, "field 'edtPhychologicalStudy'", EditText.class);
    view2131296351 = view;
    view.setOnClickListener(new DebouncingOnClickListener() {
      @Override
      public void doClick(View p0) {
        target.onClick(p0);
      }
    });
    target.relPhychologicalService = Utils.findRequiredViewAsType(source, R.id.relPhychologicalService, "field 'relPhychologicalService'", RelativeLayout.class);
    target.relMostValuable = Utils.findRequiredViewAsType(source, R.id.relMostValuable, "field 'relMostValuable'", RelativeLayout.class);
    target.relValuable = Utils.findRequiredViewAsType(source, R.id.relValuable, "field 'relValuable'", RelativeLayout.class);
    target.relLessValuable = Utils.findRequiredViewAsType(source, R.id.relLessValuable, "field 'relLessValuable'", RelativeLayout.class);
    target.txtValidation = Utils.findRequiredViewAsType(source, R.id.txtValidation, "field 'txtValidation'", TextView.class);
    target.txtOne = Utils.findRequiredViewAsType(source, R.id.txtOne, "field 'txtOne'", TextView.class);
    target.txtQuestionOne = Utils.findRequiredViewAsType(source, R.id.txtQuestionOne, "field 'txtQuestionOne'", TextView.class);
    target.txtTwo = Utils.findRequiredViewAsType(source, R.id.txtTwo, "field 'txtTwo'", TextView.class);
    target.txtQuestionTwo = Utils.findRequiredViewAsType(source, R.id.txtQuestionTwo, "field 'txtQuestionTwo'", TextView.class);
    target.txtMostValuable = Utils.findRequiredViewAsType(source, R.id.txtMostValuable, "field 'txtMostValuable'", TextView.class);
    target.txtValuable = Utils.findRequiredViewAsType(source, R.id.txtValuable, "field 'txtValuable'", TextView.class);
    target.txtLessValuable = Utils.findRequiredViewAsType(source, R.id.txtLessValuable, "field 'txtLessValuable'", TextView.class);
    target.txtThree = Utils.findRequiredViewAsType(source, R.id.txtThree, "field 'txtThree'", TextView.class);
    target.txtQuestionThree = Utils.findRequiredViewAsType(source, R.id.txtQuestionThree, "field 'txtQuestionThree'", TextView.class);
    target.txtPositiveEffect = Utils.findRequiredViewAsType(source, R.id.txtPositiveEffect, "field 'txtPositiveEffect'", TextView.class);
    target.txtNegativeEffect = Utils.findRequiredViewAsType(source, R.id.txtNegativeEffect, "field 'txtNegativeEffect'", TextView.class);
    target.txtFour = Utils.findRequiredViewAsType(source, R.id.txtFour, "field 'txtFour'", TextView.class);
    target.txtQuestionFour = Utils.findRequiredViewAsType(source, R.id.txtQuestionFour, "field 'txtQuestionFour'", TextView.class);
    target.txtFive = Utils.findRequiredViewAsType(source, R.id.txtFive, "field 'txtFive'", TextView.class);
    target.txtQuestionFive = Utils.findRequiredViewAsType(source, R.id.txtQuestionFive, "field 'txtQuestionFive'", TextView.class);
    target.txtSix = Utils.findRequiredViewAsType(source, R.id.txtSix, "field 'txtSix'", TextView.class);
    target.txtQuestionSix = Utils.findRequiredViewAsType(source, R.id.txtQuestionSix, "field 'txtQuestionSix'", TextView.class);
    target.txtNameOfDepartmentOne = Utils.findRequiredViewAsType(source, R.id.txtNameOfDepartmentOne, "field 'txtNameOfDepartmentOne'", TextView.class);
    target.txtDepartmentOnePositiveComments = Utils.findRequiredViewAsType(source, R.id.txtDepartmentOnePositiveComments, "field 'txtDepartmentOnePositiveComments'", TextView.class);
    target.txtDepartmentOneNegativeComments = Utils.findRequiredViewAsType(source, R.id.txtDepartmentOneNegativeComments, "field 'txtDepartmentOneNegativeComments'", TextView.class);
    target.txtNameOfDepartmentTwo = Utils.findRequiredViewAsType(source, R.id.txtNameOfDepartmentTwo, "field 'txtNameOfDepartmentTwo'", TextView.class);
    target.txtDepartmentTwoPositiveComments = Utils.findRequiredViewAsType(source, R.id.txtDepartmentTwoPositiveComments, "field 'txtDepartmentTwoPositiveComments'", TextView.class);
    target.txtDepartmentTwoNegativeComments = Utils.findRequiredViewAsType(source, R.id.txtDepartmentTwoNegativeComments, "field 'txtDepartmentTwoNegativeComments'", TextView.class);
    target.txtSeven = Utils.findRequiredViewAsType(source, R.id.txtSeven, "field 'txtSeven'", TextView.class);
    target.txtQuestionSeven = Utils.findRequiredViewAsType(source, R.id.txtQuestionSeven, "field 'txtQuestionSeven'", TextView.class);
    target.txtEight = Utils.findRequiredViewAsType(source, R.id.txtEight, "field 'txtEight'", TextView.class);
    target.txtQuestionEight = Utils.findRequiredViewAsType(source, R.id.txtQuestionEight, "field 'txtQuestionEight'", TextView.class);
    target.txtNine = Utils.findRequiredViewAsType(source, R.id.txtNine, "field 'txtNine'", TextView.class);
    target.txtQuestionNine = Utils.findRequiredViewAsType(source, R.id.txtQuestionNine, "field 'txtQuestionNine'", TextView.class);
    target.txtNineOne = Utils.findRequiredViewAsType(source, R.id.txtNineOne, "field 'txtNineOne'", TextView.class);
    target.txtQualityOfFood = Utils.findRequiredViewAsType(source, R.id.txtQualityOfFood, "field 'txtQualityOfFood'", TextView.class);
    target.txtPriceOfFood = Utils.findRequiredViewAsType(source, R.id.txtPriceOfFood, "field 'txtPriceOfFood'", TextView.class);
    target.txtVarityOfFood = Utils.findRequiredViewAsType(source, R.id.txtVarityOfFood, "field 'txtVarityOfFood'", TextView.class);
    target.txtService = Utils.findRequiredViewAsType(source, R.id.txtService, "field 'txtService'", TextView.class);
    target.txtHygine = Utils.findRequiredViewAsType(source, R.id.txtHygine, "field 'txtHygine'", TextView.class);
    target.txtBookAvailablity = Utils.findRequiredViewAsType(source, R.id.txtBookAvailablity, "field 'txtBookAvailablity'", TextView.class);
    target.txtStaffHelping = Utils.findRequiredViewAsType(source, R.id.txtStaffHelping, "field 'txtStaffHelping'", TextView.class);
    target.txtAdequiteSpacing = Utils.findRequiredViewAsType(source, R.id.txtAdequiteSpacing, "field 'txtAdequiteSpacing'", TextView.class);
    target.txtLibraryTimming = Utils.findRequiredViewAsType(source, R.id.txtLibraryTimming, "field 'txtLibraryTimming'", TextView.class);
    target.txtFrequencyOfVisit = Utils.findRequiredViewAsType(source, R.id.txtFrequencyOfVisit, "field 'txtFrequencyOfVisit'", TextView.class);
    target.txtCooperation = Utils.findRequiredViewAsType(source, R.id.txtCooperation, "field 'txtCooperation'", TextView.class);
    target.txtOfficeInformation = Utils.findRequiredViewAsType(source, R.id.txtOfficeInformation, "field 'txtOfficeInformation'", TextView.class);
    target.txtOfficeTimeliness = Utils.findRequiredViewAsType(source, R.id.txtOfficeTimeliness, "field 'txtOfficeTimeliness'", TextView.class);
    target.txtGymTiming = Utils.findRequiredViewAsType(source, R.id.txtGymTiming, "field 'txtGymTiming'", TextView.class);
    target.txtGymEnviroment = Utils.findRequiredViewAsType(source, R.id.txtGymEnviroment, "field 'txtGymEnviroment'", TextView.class);
    target.txtGymEquipment = Utils.findRequiredViewAsType(source, R.id.txtGymEquipment, "field 'txtGymEquipment'", TextView.class);
    target.txtGymTrainning = Utils.findRequiredViewAsType(source, R.id.txtGymTrainning, "field 'txtGymTrainning'", TextView.class);
    target.txtFacalities = Utils.findRequiredViewAsType(source, R.id.txtFacalities, "field 'txtFacalities'", TextView.class);
    target.txtSportsEquipment = Utils.findRequiredViewAsType(source, R.id.txtSportsEquipment, "field 'txtSportsEquipment'", TextView.class);
    target.txtSportTrainning = Utils.findRequiredViewAsType(source, R.id.txtSportTrainning, "field 'txtSportTrainning'", TextView.class);
    target.txtReqularHealthCheckUps = Utils.findRequiredViewAsType(source, R.id.txtReqularHealthCheckUps, "field 'txtReqularHealthCheckUps'", TextView.class);
    target.txtEmergencyMedical = Utils.findRequiredViewAsType(source, R.id.txtEmergencyMedical, "field 'txtEmergencyMedical'", TextView.class);
    target.txtPhyschologyConsuel = Utils.findRequiredViewAsType(source, R.id.txtPhyschologyConsuel, "field 'txtPhyschologyConsuel'", TextView.class);
    target.extExtraActivity = Utils.findRequiredViewAsType(source, R.id.extExtraActivity, "field 'extExtraActivity'", EditText.class);
  }

  @Override
  @CallSuper
  public void unbind() {
    ExitFormFragment target = this.target;
    if (target == null) throw new IllegalStateException("Bindings already cleared.");
    this.target = null;

    target.radioStudyFurther = null;
    target.radioWork = null;
    target.radioAnyOther = null;
    target.radioFurtherPlan = null;
    target.edtMostValuable = null;
    target.edtValuable = null;
    target.edtLessValuable = null;
    target.edtPositiveEffect = null;
    target.edtNegativeEffect = null;
    target.radioYesKeepInTouch = null;
    target.radioNoKeepInTouch = null;
    target.radioGroupIKeepInTouch = null;
    target.radioYesAdminExp = null;
    target.radioNoAdminExp = null;
    target.radioAdminExp = null;
    target.edtNameOfDepartmentOne = null;
    target.edtPositiveCommentDeptOne = null;
    target.edtNegativeCommentsDepartmentOne = null;
    target.edtNameOfDepartmentTwo = null;
    target.edtPositiveCommentDeptTwo = null;
    target.edtNegativeCommentsDepartmentTwo = null;
    target.radioYesExtraCar = null;
    target.radioNoExtraCar = null;
    target.radioNaCar = null;
    target.raioExtraCarricular = null;
    target.edtContributeToCollage = null;
    target.edtQualityOfFood = null;
    target.relQualityOfFood = null;
    target.edtPriceOfFood = null;
    target.relPriceOfFood = null;
    target.edtVarityOfFood = null;
    target.relVarityOfFood = null;
    target.edtService = null;
    target.relService = null;
    target.edtHygiene = null;
    target.relHygiene = null;
    target.edtBookAvailablity = null;
    target.relBookAvailablity = null;
    target.edtStaffHelping = null;
    target.relStaffHelping = null;
    target.edtAdequteSpaceSitting = null;
    target.relAdequteSpaceSetting = null;
    target.edtLibraryTimming = null;
    target.relLibraryTimming = null;
    target.edtFrequencyOfVisit = null;
    target.relFrequencyOfVisit = null;
    target.edtCooperation = null;
    target.relCooperation = null;
    target.edtOfficeInformation = null;
    target.relOfficeInformation = null;
    target.edtTimeliness = null;
    target.relTimeLines = null;
    target.txtPoliteness = null;
    target.edtPoliteness = null;
    target.relPoliteness = null;
    target.edtGymTimming = null;
    target.relGymTimming = null;
    target.edtEnviroment = null;
    target.relEnviroment = null;
    target.edtEquipment = null;
    target.relEquipment = null;
    target.edtTrainning = null;
    target.relTrainning = null;
    target.edtFacalities = null;
    target.relFacalities = null;
    target.edtEquipments = null;
    target.relEquipments = null;
    target.edtSportsTrainning = null;
    target.relSportsTraining = null;
    target.edtRegularHealthCheckUps = null;
    target.relRegularHealthCheckUps = null;
    target.edtEmerganceMedicalFacality = null;
    target.relEmergencyMedicalFacality = null;
    target.edtPhychologicalStudy = null;
    target.relPhychologicalService = null;
    target.relMostValuable = null;
    target.relValuable = null;
    target.relLessValuable = null;
    target.txtValidation = null;
    target.txtOne = null;
    target.txtQuestionOne = null;
    target.txtTwo = null;
    target.txtQuestionTwo = null;
    target.txtMostValuable = null;
    target.txtValuable = null;
    target.txtLessValuable = null;
    target.txtThree = null;
    target.txtQuestionThree = null;
    target.txtPositiveEffect = null;
    target.txtNegativeEffect = null;
    target.txtFour = null;
    target.txtQuestionFour = null;
    target.txtFive = null;
    target.txtQuestionFive = null;
    target.txtSix = null;
    target.txtQuestionSix = null;
    target.txtNameOfDepartmentOne = null;
    target.txtDepartmentOnePositiveComments = null;
    target.txtDepartmentOneNegativeComments = null;
    target.txtNameOfDepartmentTwo = null;
    target.txtDepartmentTwoPositiveComments = null;
    target.txtDepartmentTwoNegativeComments = null;
    target.txtSeven = null;
    target.txtQuestionSeven = null;
    target.txtEight = null;
    target.txtQuestionEight = null;
    target.txtNine = null;
    target.txtQuestionNine = null;
    target.txtNineOne = null;
    target.txtQualityOfFood = null;
    target.txtPriceOfFood = null;
    target.txtVarityOfFood = null;
    target.txtService = null;
    target.txtHygine = null;
    target.txtBookAvailablity = null;
    target.txtStaffHelping = null;
    target.txtAdequiteSpacing = null;
    target.txtLibraryTimming = null;
    target.txtFrequencyOfVisit = null;
    target.txtCooperation = null;
    target.txtOfficeInformation = null;
    target.txtOfficeTimeliness = null;
    target.txtGymTiming = null;
    target.txtGymEnviroment = null;
    target.txtGymEquipment = null;
    target.txtGymTrainning = null;
    target.txtFacalities = null;
    target.txtSportsEquipment = null;
    target.txtSportTrainning = null;
    target.txtReqularHealthCheckUps = null;
    target.txtEmergencyMedical = null;
    target.txtPhyschologyConsuel = null;
    target.extExtraActivity = null;

    view2131296344.setOnClickListener(null);
    view2131296344 = null;
    view2131296364.setOnClickListener(null);
    view2131296364 = null;
    view2131296341.setOnClickListener(null);
    view2131296341 = null;
    view2131296452.setOnClickListener(null);
    view2131296452 = null;
    view2131296447.setOnClickListener(null);
    view2131296447 = null;
    view2131296445.setOnClickListener(null);
    view2131296445 = null;
    view2131296357.setOnClickListener(null);
    view2131296357 = null;
    view2131296356.setOnClickListener(null);
    view2131296356 = null;
    view2131296365.setOnClickListener(null);
    view2131296365 = null;
    view2131296359.setOnClickListener(null);
    view2131296359 = null;
    view2131296339.setOnClickListener(null);
    view2131296339 = null;
    view2131296327.setOnClickListener(null);
    view2131296327 = null;
    view2131296361.setOnClickListener(null);
    view2131296361 = null;
    view2131296326.setOnClickListener(null);
    view2131296326 = null;
    view2131296342.setOnClickListener(null);
    view2131296342 = null;
    view2131296337.setOnClickListener(null);
    view2131296337 = null;
    view2131296329.setOnClickListener(null);
    view2131296329 = null;
    view2131296350.setOnClickListener(null);
    view2131296350 = null;
    view2131296362.setOnClickListener(null);
    view2131296362 = null;
    view2131296352.setOnClickListener(null);
    view2131296352 = null;
    view2131296338.setOnClickListener(null);
    view2131296338 = null;
    view2131296333.setOnClickListener(null);
    view2131296333 = null;
    view2131296334.setOnClickListener(null);
    view2131296334 = null;
    view2131296363.setOnClickListener(null);
    view2131296363 = null;
    view2131296336.setOnClickListener(null);
    view2131296336 = null;
    view2131296335.setOnClickListener(null);
    view2131296335 = null;
    view2131296360.setOnClickListener(null);
    view2131296360 = null;
    view2131296358.setOnClickListener(null);
    view2131296358 = null;
    view2131296332.setOnClickListener(null);
    view2131296332 = null;
    view2131296351.setOnClickListener(null);
    view2131296351 = null;
  }
}
