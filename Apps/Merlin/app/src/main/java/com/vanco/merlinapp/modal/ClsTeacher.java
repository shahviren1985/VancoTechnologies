package com.vanco.merlinapp.modal;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.io.Serializable;

public class ClsTeacher implements Serializable {

    @SerializedName("teacherCode")
    @Expose
    private String teacherCode;
    @SerializedName("teacherName")
    @Expose
    private String teacherName;
    @SerializedName("subjectCode")
    @Expose
    private String subjectCode;
    @SerializedName("subjectName")
    @Expose
    private String subjectName;
    @SerializedName("course")
    @Expose
    private String course;
    @SerializedName("subCourse")
    @Expose
    private String subCourse;
    @SerializedName("semester")
    @Expose
    private String semester;

    private boolean isSendFeedBack;

    public boolean isSendFeedBack() {
        return isSendFeedBack;
    }

    public void setSendFeedBack(boolean sendFeedBack) {
        isSendFeedBack = sendFeedBack;
    }

    public String getTeacherCode() {
        return teacherCode;
    }

    public void setTeacherCode(String teacherCode) {
        this.teacherCode = teacherCode;
    }

    public String getTeacherName() {
        return teacherName;
    }

    public void setTeacherName(String teacherName) {
        this.teacherName = teacherName;
    }

    public String getSubjectCode() {
        return subjectCode;
    }

    public void setSubjectCode(String subjectCode) {
        this.subjectCode = subjectCode;
    }

    public String getSubjectName() {
        return subjectName;
    }

    public void setSubjectName(String subjectName) {
        this.subjectName = subjectName;
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

    public String getSemester() {
        return semester;
    }

    public void setSemester(String semester) {
        this.semester = semester;
    }

}