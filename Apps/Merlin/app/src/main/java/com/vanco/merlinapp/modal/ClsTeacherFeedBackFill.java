package com.vanco.merlinapp.modal;

import com.google.gson.annotations.SerializedName;

public class ClsTeacherFeedBackFill {
    @SerializedName("TeacherCode")
    private  String teacherCode;

    @SerializedName("SubjectCode")
    private String subjectCode;

    public String getTeacherCode() {
        return teacherCode;
    }

    public void setTeacherCode(String teacherCode) {
        this.teacherCode = teacherCode;
    }

    public String getSubjectCode() {
        return subjectCode;
    }

    public void setSubjectCode(String subjectCode) {
        this.subjectCode = subjectCode;
    }
}
