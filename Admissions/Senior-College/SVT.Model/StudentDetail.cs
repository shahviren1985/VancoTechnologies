//-----------------------------------------------------------------------
// <copyright file="StudentDetail.cs" company="KarmSoft">
//     Copyright (c) KarmSoft. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace SVT.Business.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    /// <summary>
    /// This class is used to Define Model for Table - StudentDetails
    /// </summary>
    /// <CreatedBy>Kaushik</CreatedBy>
    /// <CreatedDate>26-June-2020</CreatedDate>
    /// <ModifiedBy></ModifiedBy>
    /// <ModifiedDate></ModifiedDate>
    /// <ReviewBy></ReviewBy>
    /// <ReviewDate></ReviewDate>
    [Table("StudentPdfDetails")]
    public sealed class StudentPdfDetail : BaseModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Id value.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the MKCLFormNumber value.
        /// </summary>
        [Required(ErrorMessage = "*")]        
        public int StudentId { get; set; }

        /// <summary>
        /// Gets or sets the FirstName value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "*")]
        public string PDFPath { get; set; }

        
        #endregion
    }


    /// <summary>
    /// This class is used to Define Model for Table - StudentDetails
    /// </summary>
    /// <CreatedBy>Kaushik</CreatedBy>
    /// <CreatedDate>13-Feb-2018</CreatedDate>
    /// <ModifiedBy></ModifiedBy>
    /// <ModifiedDate></ModifiedDate>
    /// <ReviewBy></ReviewBy>
    /// <ReviewDate></ReviewDate>
    [Table("StudentDetails")]
    public sealed class StudentDetail : BaseModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Id value.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the MKCLFormNumber value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "*")]
        public string MKCLFormNumber { get; set; }

        /// <summary>
        /// Gets or sets the FirstName value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "*")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the LastName value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "*")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the FatherName value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "*")]
        public string FatherName { get; set; }

        /// <summary>
        /// Gets or sets the MotherName value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "*")]
        public string MotherName { get; set; }

        /// <summary>
        /// Gets or sets the FatherFirstName value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "*")]
        public string FatherFirstName { get; set; }

        /// <summary>
        /// Gets or sets the FatherMiddleName value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "*")]
        public string FatherMiddleName { get; set; }

        /// <summary>
        /// Gets or sets the FatheLastName value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "*")]
        public string FatherLastName { get; set; }

        /// <summary>
        /// Gets or sets the MotherFirstName value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "*")]
        public string MotherFirstName { get; set; }

        /// <summary>
        /// Gets or sets the MotherMiddleName value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "*")]
        public string MotherMiddleName { get; set; }

        /// <summary>
        /// Gets or sets the MotherLastName value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "*")]
        public string MotherLastName { get; set; }

        /// <summary>
        /// Gets or sets the AadharNumber value.
        /// </summary>
        [StringLength(20, ErrorMessage = "*")]
        public string AadharNumber { get; set; }

        /// <summary>
        /// Gets or sets the Dob value.
        /// </summary>
        public DateTime? Dob { get; set; }

        /// <summary>
        /// Gets or sets the Gender value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        public short? Gender { get; set; }

        /// <summary>
        /// Gets or sets the BloodGroup value.
        /// </summary>
        [StringLength(20, ErrorMessage = "*")]
        public string BloodGroup { get; set; }

        /// <summary>
        /// Gets or sets the PlaceofBirth value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string PlaceofBirth { get; set; }

        /// <summary>
        /// Gets or sets the Nationality value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Nationality { get; set; }

        /// <summary>
        /// Gets or sets the MaritalStatus value.
        /// </summary>
        [StringLength(50, ErrorMessage = "*")]
        public string MaritalStatus { get; set; }

        /// <summary>
        /// Gets or sets the Religion value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "*")]
        public string Religion { get; set; }

        /// <summary>
        /// Gets or sets the Category value.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the Caste value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "*")]
        public string Caste { get; set; }

        /// <summary>
        /// Gets or sets the SubCaste value.
        /// </summary>
        [StringLength(50, ErrorMessage = "*")]
        public string SubCaste { get; set; }

        /// <summary>
        /// Gets or sets the MotherTongue value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string MotherTongue { get; set; }


        /// <summary>
        /// Gets or sets the CurrentAddress value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(500, ErrorMessage = "*")]
        public string CurrentAddress { get; set; }

        /// <summary>
        /// Gets or sets the PermanentAddress value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(500, ErrorMessage = "*")]
        public string PermanentAddress { get; set; }

        /// <summary>
        /// Gets or sets the EmergencyTel value.
        /// </summary>
        [StringLength(20, ErrorMessage = "*")]
        public string EmergencyTel { get; set; }


        /// <summary>
        /// Gets or sets the MobileNumber value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "*")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the Email value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(150, ErrorMessage = "*")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the GuardianOccupation value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string GuardianOccupation { get; set; }

        /// <summary>
        /// Gets or sets the GuardianSalary value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string GuardianSalary { get; set; }

        /// <summary>
        /// Gets or sets the RelwithGurdian value.
        /// </summary>
        [StringLength(50, ErrorMessage = "*")]
        public string RelwithGurdian { get; set; }

        /// <summary>
        /// Gets or sets the EmploymentStatus value.
        /// </summary>
        public short? EmploymentStatus { get; set; }

        /// <summary>
        /// Gets or sets the Photo value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(255, ErrorMessage = "*")]
        public string Photo { get; set; }

        /// <summary>
        /// Gets or sets the SignaturePath value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(255, ErrorMessage = "*")]
        public string SignaturePath { get; set; }

        /// <summary>
        /// Gets or sets the HscStream value.
        /// </summary>
        [StringLength(50, ErrorMessage = "*")]
        public string HscStream { get; set; }

        /// <summary>
        /// Gets or sets the CoursePreference1 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string CoursePreference1 { get; set; }

        /// <summary>
        /// Gets or sets the CoursePreference2 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string CoursePreference2 { get; set; }

        /// <summary>
        /// Gets or sets the CoursePreference3 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string CoursePreference3 { get; set; }

        /// <summary>
        /// Gets or sets the CoursePreference4 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string CoursePreference4 { get; set; }

        /// <summary>
        /// Gets or sets the CoursePreference5 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string CoursePreference5 { get; set; }

        /// <summary>
        /// Gets or sets the CoursePreference6 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string CoursePreference6 { get; set; }

        /// <summary>
        /// Gets or sets the CoursePreference7 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string CoursePreference7 { get; set; }

        /// <summary>
        /// Gets or sets the Preference1GE1 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Preference1GE1 { get; set; }

        /// <summary>
        /// Gets or sets the Preference1GE2 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Preference1GE2 { get; set; }

        /// <summary>
        /// Gets or sets the Preference2GE1 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Preference2GE1 { get; set; }

        /// <summary>
        /// Gets or sets the Preference2GE2 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Preference2GE2 { get; set; }

        /// <summary>
        /// Gets or sets the Preference3GE1 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Preference3GE1 { get; set; }

        /// <summary>
        /// Gets or sets the Preference3GE2 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Preference3GE2 { get; set; }

        /// <summary>
        /// Gets or sets the Preference4GE1 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Preference4GE1 { get; set; }

        /// <summary>
        /// Gets or sets the Preference4GE2 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Preference4GE2 { get; set; }

        /// <summary>
        /// Gets or sets the Preference5GE1 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Preference5GE1 { get; set; }

        /// <summary>
        /// Gets or sets the Preference5GE2 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Preference5GE2 { get; set; }

        /// <summary>
        /// Gets or sets the Preference6GE1 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Preference6GE1 { get; set; }

        /// <summary>
        /// Gets or sets the Preference6GE2 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Preference6GE2 { get; set; }

        /// <summary>
        /// Gets or sets the Preference7GE1 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Preference7GE1 { get; set; }

        /// <summary>
        /// Gets or sets the Preference7GE2 value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Preference7GE2 { get; set; }

        /// <summary>
        /// Gets or sets the LastSchoolAttend value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(255, ErrorMessage = "*")]
        public string LastSchoolAttend { get; set; }

        /// <summary>
        /// Gets or sets the NameofExaminationBoard value.
        /// </summary>
        [StringLength(250, ErrorMessage = "*")]
        public string NameofExaminationBoard { get; set; }


        /// <summary>
        /// Gets or sets the MonthLastExamPassed value.
        /// </summary>
        public short? MonthLastExamPassed { get; set; }


        /// <summary>
        /// Gets or sets the YearLastExamPassed value.
        /// </summary>
        public short? YearLastExamPassed { get; set; }



        /// <summary>
        /// Gets or sets the AttemptofHSC value.
        /// </summary>
        [StringLength(50, ErrorMessage = "*")]
        public string AttemptofHSC { get; set; }

        /// <summary>
        /// Gets or sets the IsSVTStudent value.
        /// </summary>
        public bool? IsSVTStudent { get; set; }

        /// <summary>
        /// Gets or sets the WishtojoinNCC value.
        /// </summary>
        public bool? WishtojoinNCC { get; set; }


        /// <summary>
        /// Gets or sets the Subject1Name value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Subject1Name { get; set; }

        /// <summary>
        /// Gets or sets the Subject1MarksObtained value.
        /// </summary>
        public decimal? Subject1MarksObtained { get; set; }

        /// <summary>
        /// Gets or sets the Subject1Total value.
        /// </summary>
        public decimal? Subject1Total { get; set; }

        /// <summary>
        /// Gets or sets the Subject2Name value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Subject2Name { get; set; }

        /// <summary>
        /// Gets or sets the Subject2MarksObtained value.
        /// </summary>
        public decimal? Subject2MarksObtained { get; set; }

        /// <summary>
        /// Gets or sets the Subject2Total value.
        /// </summary>
        public decimal? Subject2Total { get; set; }

        /// <summary>
        /// Gets or sets the Subject3Name value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Subject3Name { get; set; }

        /// <summary>
        /// Gets or sets the Subject3MarksObtained value.
        /// </summary>
        public decimal? Subject3MarksObtained { get; set; }

        /// <summary>
        /// Gets or sets the Subject3Total value.
        /// </summary>
        public decimal? Subject3Total { get; set; }

        /// <summary>
        /// Gets or sets the Subject4Name value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Subject4Name { get; set; }

        /// <summary>
        /// Gets or sets the Subject4MarksObtained value.
        /// </summary>
        public decimal? Subject4MarksObtained { get; set; }

        /// <summary>
        /// Gets or sets the Subject4Total value.
        /// </summary>
        public decimal? Subject4Total { get; set; }

        /// <summary>
        /// Gets or sets the Subject5Name value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Subject5Name { get; set; }

        /// <summary>
        /// Gets or sets the Subject5MarksObtained value.
        /// </summary>
        public decimal? Subject5MarksObtained { get; set; }

        /// <summary>
        /// Gets or sets the Subject5Total value.
        /// </summary>
        public decimal? Subject5Total { get; set; }

        /// <summary>
        /// Gets or sets the Subject6Name value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Subject6Name { get; set; }

        /// <summary>
        /// Gets or sets the Subject6MarksObtained value.
        /// </summary>
        public decimal? Subject6MarksObtained { get; set; }

        /// <summary>
        /// Gets or sets the Subject6Total value.
        /// </summary>
        public decimal? Subject6Total { get; set; }

        /// <summary>
        /// Gets or sets the Subject7Name value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Subject7Name { get; set; }

        /// <summary>
        /// Gets or sets the Subject7MarksObtained value.
        /// </summary>
        public decimal? Subject7MarksObtained { get; set; }

        /// <summary>
        /// Gets or sets the Subject7Total value.
        /// </summary>
        public decimal? Subject7Total { get; set; }


        /// <summary>
        /// Gets or sets the TotalMarks value.
        /// </summary>
        public short? TotalMarks { get; set; }

        /// <summary>
        /// Gets or sets the Percentage value.
        /// </summary>
        public decimal? Percentage { get; set; }


        /// <summary>
        /// Gets or sets the NumbrrAttends value.
        /// </summary>
        public short? NumbrrAttends { get; set; }

        /// <summary>
        /// Gets or sets the MarksObtain value.
        /// </summary>
        public short? MarksObtain { get; set; }


        /// <summary>
        /// Gets or sets the IsScience value.
        /// </summary>
        public bool? IsScience { get; set; }

        /// <summary>
        /// Gets or sets the IsAdmitted value.
        /// </summary>
        public bool? IsAdmitted { get; set; }


        /// <summary>
        /// Gets or sets the Remarks value.
        /// </summary>
        [StringLength(500, ErrorMessage = "*")]
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets the SubCourse value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string SubCourse { get; set; }


        /// <summary>
        /// Gets or sets the StudentType value.
        /// </summary>
        public short? StudentType { get; set; }


        /// <summary>
        /// Gets or sets the CourseAdmittedRound1 value.
        /// </summary>
        [StringLength(250, ErrorMessage = "*")]
        public string CourseAdmittedRound1 { get; set; }

        /// <summary>
        /// Gets or sets the AdmittedRound1 value.
        /// </summary>
        public bool? AdmittedRound1 { get; set; }

        /// <summary>
        /// Gets or sets the RejectedAdmissionRound1 value.
        /// </summary>
        public bool? RejectedAdmissionRound1 { get; set; }

        /// <summary>
        /// Gets or sets the TransferReasonRound1 value.
        /// </summary>
        [StringLength(250, ErrorMessage = "*")]
        public string TransferReasonRound1 { get; set; }

        /// <summary>
        /// Gets or sets the DateAdmittedRound1 value.
        /// </summary>
        public DateTime? DateAdmittedRound1 { get; set; }

        /// <summary>
        /// Gets or sets the RejectedRoundnmbr value.
        /// </summary>
        public short? RejectedRoundnmbr { get; set; }

        /// <summary>
        /// Gets or sets the CourseAdmittedRound2 value.
        /// </summary>
        [StringLength(250, ErrorMessage = "*")]
        public string CourseAdmittedRound2 { get; set; }

        /// <summary>
        /// Gets or sets the AdmittedRound2 value.
        /// </summary>
        public bool? AdmittedRound2 { get; set; }

        /// <summary>
        /// Gets or sets the RejectedAdmissionRound2 value.
        /// </summary>
        public bool? RejectedAdmissionRound2 { get; set; }

        /// <summary>
        /// Gets or sets the TransferReasonRound2 value.
        /// </summary>
        [StringLength(250, ErrorMessage = "*")]
        public string TransferReasonRound2 { get; set; }

        /// <summary>
        /// Gets or sets the DateAdmittedRound2 value.
        /// </summary>
        public DateTime? DateAdmittedRound2 { get; set; }

        /// <summary>
        /// Gets or sets the CourseAdmittedRound3 value.
        /// </summary>
        [StringLength(250, ErrorMessage = "*")]
        public string CourseAdmittedRound3 { get; set; }

        /// <summary>
        /// Gets or sets the AdmittedRound3 value.
        /// </summary>
        public bool? AdmittedRound3 { get; set; }

        /// <summary>
        /// Gets or sets the RejectedAdmissionRound3 value.
        /// </summary>
        public bool? RejectedAdmissionRound3 { get; set; }

        /// <summary>
        /// Gets or sets the TransferReasonRound3 value.
        /// </summary>
        [StringLength(250, ErrorMessage = "*")]
        public string TransferReasonRound3 { get; set; }

        /// <summary>
        /// Gets or sets the DateAdmittedRound3 value.
        /// </summary>
        public DateTime? DateAdmittedRound3 { get; set; }

        /// <summary>
        /// Gets or sets the IsNRIStudent value.
        /// </summary>
        public bool? IsNRIStudent { get; set; }

        /// <summary>
        /// Gets or sets the IsLearningDisability value.
        /// </summary>
        public bool? IsLearningDisability { get; set; }

        /// <summary>
        /// Gets or sets the IsFeesPaid value.
        /// </summary>
        public bool? IsFeesPaid { get; set; }

        /// <summary>
        /// Gets or sets the IsCancelled value.
        /// </summary>
        public bool? IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the DateRegistered value.
        /// </summary>
        //[Required(ErrorMessage = "*")]
        public DateTime? DateRegistered { get; set; }

        /// <summary>
        /// Gets or sets the LastModified value.
        /// </summary>
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Gets or sets the LastModified2 value.
        /// </summary>
        public DateTime? LastModified2 { get; set; }


        public bool? IsSubmitted { get; set; }

        public DateTime? DateSubmitted { get; set; }

        [NotMapped]
        public string EncryptedId { get; set; }


        [NotMapped]
        public string StudentPdfURL { get; set; }

        [NotMapped]
        public string AadharCardURL { get; set; }

        [NotMapped]
        public string SSCMarksheetURL { get; set; }

        [NotMapped]
        public string HSCMarksheetURL { get; set; }

        [NotMapped]
        public string CasteCertificateURL { get; set; }

        [NotMapped]
        public string DisabilityCertificateURL { get; set; }

        [NotMapped]
        public string GapCertificateURL { get; set; }

        [NotMapped]
        public string MigrationCertificateURL { get; set; }

        [NotMapped]
        public string LeavingCertificateURL { get; set; }
        [NotMapped]
        public string ParentSignaturePath { get; set; }

        [NotMapped]
        public string RationCardUrl { get; set; }
        [NotMapped]
        public string UnderTakingUrl { get; set; }
        [NotMapped]
        public string SNDTUrl { get; set; }

        [NotMapped]
        public string EligibilityUrl { get; set; }

        [NotMapped]
        public bool? IsDuplicate { get; set; }

        [StringLength(100, ErrorMessage = "*")]
        public string StudentState { get; set; }

        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string Branch { get; set; }
        public string IFSCCode { get; set; }
        public string AccountHolderName { get; set; }
        public string PanNumber { get; set; }

        public bool? VoterId { get; set; }
        public string VoterNumber { get; set; }
        public string HscSeatNumber { get; set; }

        public string ResidenceState { get; set; }
        public string PinCode { get; set; }
        public string DisabilityNumber { get; set; }
        public string DisabilityType { get; set; }
        public string DisabilityPercentage { get; set; }

        public bool? IsHostelRequired { get; set; }

        [StringLength(500, ErrorMessage = "*")]
        public string HostelReason { get; set; }

        public bool? IsNRI { get; set; }
        public string AboutCollege { get; set; }
        #endregion
    }
    public class MailProp 
    {
    public string sub { get; set; }
        public string mailbody { get; set; }
        public string docs { get; set; }
        public string Id { get; set; }
    }

}
