using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util
{
    public class ConstantMessage
    {
        //Common varibles
        public static string MSG_ColumnNameExistExcel = "please upload file which contains different colums.";
        public static string MSG_INVALID_ColumnName = "Invalid Excel File Format.Please Upload Correct File format";
        public static string MSG_INVALID_Data = "Following data is not imported due to error in data or data Already Exists."; 

        //ErrorPage
        public static string ErrorMessage = "Error occurred while performing your operation. <br />Administrator has been notified about this error message. <br />Please try again later.";

        //CourseMaster
        public static string MSG_CreateCourse = "Course Created Successfully.";
        public static string MSG_CourseExist = "Course name already exist. Please enter different course name.";
        public static string MSG_CourseUpdate = "Course Updated Successfully.";
        public static string MSG_ExcelCourseExist = "Following courses are already added to course master.";
        public static string MSG_CourseDelete = "Course successfully deleted.";

        //Sub-CourseMaster
        public static string MSG_CreateSUbCourse = "Sub Course created successfully.";
        public static string MSG_SubCourseExist = "Sub Course name is aleady exist. Please enter different name.";
        public static string MSG_UpdateSubCourse = "Sub Course Updated Successfully.";
        public static string MSG_ExcelSubCourseExist = "following sub-courses are already exist.";
        public static string MSG_ExcelSubCourseCreate = "Sub-courses imported successfully.";

        //SubectMaster
        public static string MSG_CreateSubject = "Subject created successfully.";
        public static string MSG_SubNameExist = "Subject Name Does Not Exists. Please Enter Different Name.";
        public static string MSG_SubNameAlredyExist = "Subject Name Already Exists. Please Enter Different Name.";
        public static string MSG_SubCodeExist = "Subject Code does not Exists. Please Enter Different Code.";
        public static string MSG_SubCodeAlredyExist = "Subject Code Altesdy Exists. Please Enter Different Code.";
        public static string MSG_UpdateSubject = "Subject Updated Successfully.";
        public static string MSG_ExcelSubjectExist = "Following subjects already exist.";
        public static string MSG_ExcelSubjectCreate = "Subjects imported successfully.";

        //Subject Mapper
        public static string MSG_MappedSubject = "Selected subjects mapped successfully.";
        public static string MSG_UnMappedSubject = "Subject unmapped successfully.";
        public static string MSG_ExcelSubjectMapped = "Course Subject imported and mapped successfully.";

        //Department
        public static string MSG_DepartmentAdd = "Department Details Added Successfully.";

        //Exam-Master
        public static string MSG_ExamMasterCreate = "Exam master created successfully.";
        public static string MSG_ExamMasterDelete = "Exam Master deleted successfully.";
        public static string MSG_ExamMasterUpdate = "Exam Master Updated successfully.";

        //Exam Type
        public static string MSG_ExamTypeAdd = "Exam Type Added Successfully.";
        public static string MSG_ExamTypeUpdate = "Exam Type Updated Successfully.";
        public static string MSG_ExamTypeDelete = "Exam Type Deleted  Successfully.";

        //Marks Details
        public static string MSG_MarkDetailsAdd = "Marks Details created successfully.";

        //Role Details
        public static string MSG_RoleDelete = "Role deleted successfully.";
        public static string MSG_RoleAdd = "Role created successfully.";
        public static string MSG_RoleExist = "Role Already present.";
        public static string MSG_RoleUpdate = "User Details Updated successfully.";
        public static string MSG_StaffUpdate = "Staff Details Updated Successfully.";

        //Staff Details
        public static string MSG_StaffAdd = "New staff created successfully.";
        //public static string MSG_StaffUpdate = "Staff Details Updated Successfully.";

        //Student Details
        public static string MSG_StudentAdd = "Student created successfully.";
        public static string MSG_StudentSelectDDL = "Please select course and sub-course.";
        public static string MSG_ExcelStudentAdd = "Student list imported successfully.";
        public static string MSG_StudentUpdate = "Student Detail Updated Successfully";
        public static string MSG_StudentDelete = "Student Detail Deleted Successfully";

        //Exam status
        public static string MSG_ExamStatusAdd = "Exam status created successfully.";
        public static string MSG_ExamStatusUpdate = "Exam status updated successfully.";
        public static string MSG_ExamStatusExists = "Exam status Already Exists.";
        public static string MSG_ExamStatusDelete = "Exam status deleted successfully.";
        //Subject Exam details
        public static string MSG_SubjectExamAdd = "Subject Exam Detail Added Successfully.";
        public static string MSG_SubjectExamDel = "Subject Exam Detail Deleted Successfully.";
        public static string MSG_SubjectExamUpdate = "Subject Exam Detail Updated Successfully.";

        //SuperVisior
        public static string MSG_SuperVisioAdd = "Super-Visior Detail Added Successfully.";

        //UserDetails
        public static string MSG_UserAdd = "User created successfully.";
        public static string MSG_UserUpdate = "User Details Updated successfully.";
        public static string MSG_UserRemove = "User deleted successfully.";
        public static string MSG_UserExists = "User Id Already Exists.";

        public static string MSG_PASSWORD_NOT_MATCH = "Password and retype password shall match";

        public const string CREDIT = "4";


        //add from following variables from Contast class
        public const string COURSE_NAME = "SELECT Id, CourseName FROM sr_CourseDetails";
        public const string CASTE_NAME = "SELECT Id, Caste FROM CasteDetails";
        public const string RELEGION_NAME = "SELECT Id, RelegionName FROM RelegionDetails";
        public const string SEMESTER_MASTER = "SELECT Id, SemesterName FROM semestermaster";
        public const string SUBJECT_MASTER = "SELECT Subject_UID, SubjectCode, SubjectName FROM subjectmaster";
        public const string EXAM_NAMES = "SELECT Id, ExamName FROM examdetails";

        public const string MINIMUM_MARKS = "35";
        public const string MAXIMUM_MARKS = "100";

        public const string INTERNAL_MINIMUM_MARKS = "9";
        public const string EXTERNAL_MINIMUM_MARKS = "26";

        public const string INTERNAL_MAXIMUM_MARKS = "25";
        public const string EXTERNAL_MAXIMUM_MARKS = "75";

        public const string SEAT_NO_STARTING = "3401";

        // public const string CREDIT = "4";
        //end

    }
}
