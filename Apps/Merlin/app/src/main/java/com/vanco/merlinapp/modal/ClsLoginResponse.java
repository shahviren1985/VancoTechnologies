
package com.vanco.merlinapp.modal;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.List;

public class ClsLoginResponse {

    @SerializedName("Success")
    @Expose
    private Boolean success;
    @SerializedName("userId")
    @Expose
    private String userId;
    @SerializedName("firstName")
    @Expose
    private String firstName;
    @SerializedName("lastName")
    @Expose
    private String lastName;
    @SerializedName("mobileNumber")
    @Expose
    private String mobileNumber;
    @SerializedName("collegeCode")
    @Expose
    private String collegeCode;
    @SerializedName("course")
    @Expose
    private String course;
    @SerializedName("subCourse")
    @Expose
    private String subCourse;
    @SerializedName("isActive")
    @Expose
    private String isActive;
    @SerializedName("isFinalYearStudent")
    @Expose
    private String isFinalYearStudent;
    @SerializedName("currentSemester")
    @Expose
    private String currentSemester;
    @SerializedName("feedbackStatus")
    @Expose
    private List<ClsTeacherFeedBackFill> feedbackStatus;
    @SerializedName("roleType")
    @Expose
    private String roleType;
    @SerializedName("isExistFormSubmitted")
    @Expose
    private String isExistFormSubmitted;
    @SerializedName("dateCreated")
    @Expose
    private String dateCreated;

    public Boolean getSuccess() {
        return success;
    }

    public void setSuccess(Boolean success) {
        this.success = success;
    }

    public String getUserId() {
        return userId;
    }

    public void setUserId(String userId) {
        this.userId = userId;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public String getMobileNumber() {
        return mobileNumber;
    }

    public void setMobileNumber(String mobileNumber) {
        this.mobileNumber = mobileNumber;
    }

    public String getCollegeCode() {
        return collegeCode;
    }

    public void setCollegeCode(String collegeCode) {
        this.collegeCode = collegeCode;
    }

    public String getCourse() {
        return course;
    }

    public void setCourse(String course) {
        this.course = course;
    }

    public String getSubCourse() {
        return subCourse;
    }

    public void setSubCourse(String subCourse) {
        this.subCourse = subCourse;
    }

    public String getIsActive() {
        return isActive;
    }

    public void setIsActive(String isActive) {
        this.isActive = isActive;
    }

    public String getIsFinalYearStudent() {
        return isFinalYearStudent;
    }

    public void setIsFinalYearStudent(String isFinalYearStudent) {
        this.isFinalYearStudent = isFinalYearStudent;
    }

    public String getCurrentSemester() {
        return currentSemester;
    }

    public void setCurrentSemester(String currentSemester) {
        this.currentSemester = currentSemester;
    }

    public List<ClsTeacherFeedBackFill> getFeedbackStatus() {
        return feedbackStatus;
    }

    public void setFeedbackStatus(List<ClsTeacherFeedBackFill> feedbackStatus) {
        this.feedbackStatus = feedbackStatus;
    }

    public String getRoleType() {
        return roleType;
    }

    public void setRoleType(String roleType) {
        this.roleType = roleType;
    }

    public String getIsExistFormSubmitted() {
        return isExistFormSubmitted;
    }

    public void setIsExistFormSubmitted(String isExistFormSubmitted) {
        this.isExistFormSubmitted = isExistFormSubmitted;
    }

    public String getDateCreated() {
        return dateCreated;
    }

    public void setDateCreated(String dateCreated) {
        this.dateCreated = dateCreated;
    }
}
