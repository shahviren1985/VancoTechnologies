//-----------------------------------------------------------------------
// <copyright file="AdmissionFormDetail.cs" company="TatvaSoft">
//     Copyright (c) TatvaSoft. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AdmissionForm.Business.Model
{
    using Model;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    /// <summary>
    /// This class is used to Define Model for Table - AdmissionFormDetail
    /// </summary>
    /// <CreatedBy>Kaushik</CreatedBy>
    /// <CreatedDate>23-May-2018</CreatedDate>
    /// <ModifiedBy></ModifiedBy>
    /// <ModifiedDate></ModifiedDate>
    /// <ReviewBy></ReviewBy>
    /// <ReviewDate></ReviewDate>
    [Table("AdmissionFormDetail")]
    public sealed class AdmissionFormDetail : BaseModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Id value.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the CourseName value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        [StringLength(200, ErrorMessage = "*")]
        public string CourseName { get; set; }

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
        /// Gets or sets the FatherFirstName value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string FatherFirstName { get; set; }

        /// <summary>
        /// Gets or sets the FatherMiddleName value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string FatherMiddleName { get; set; }

        /// <summary>
        /// Gets or sets the FatherLastName value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string FatherLastName { get; set; }

        /// <summary>
        /// Gets or sets the MotherFirstName value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string MotherFirstName { get; set; }

        /// <summary>
        /// Gets or sets the MotherMiddleName value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string MotherMiddleName { get; set; }

        /// <summary>
        /// Gets or sets the MotherLastName value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string MotherLastName { get; set; }

        /// <summary>
        /// Gets or sets the AadharNumber value.
        /// </summary>
        [StringLength(20, ErrorMessage = "*")]
        public string AadharNumber { get; set; }

        /// <summary>
        /// Gets or sets the PanNumber value.
        /// </summary>
        [StringLength(20, ErrorMessage = "*")]
        public string PanNumber { get; set; }

        /// <summary>
        /// Gets or sets the Dob value.
        /// </summary>
        public DateTime? Dob { get; set; }

        /// <summary>
        /// Gets or sets the CalculatedAge value.
        /// </summary>
        public int? CalculatedAge { get; set; }

        /// <summary>
        /// Gets or sets the Nationality value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Nationality { get; set; }

        /// <summary>
        /// Gets or sets the MotherTongue value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string MotherTongue { get; set; }

        /// <summary>
        /// Gets or sets the Religion value.
        /// </summary>
        [StringLength(50, ErrorMessage = "*")]
        public string Religion { get; set; }

        /// <summary>
        /// Gets or sets the Category value.
        /// </summary>
        [StringLength(50, ErrorMessage = "*")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the Caste value.
        /// </summary>
        [StringLength(50, ErrorMessage = "*")]
        public string Caste { get; set; }

        /// <summary>
        /// Gets or sets the SubCaste value.
        /// </summary>
        [StringLength(50, ErrorMessage = "*")]
        public string SubCaste { get; set; }

        /// <summary>
        /// Gets or sets the CurrentAddress value.
        /// </summary>
        [StringLength(500, ErrorMessage = "*")]
        public string CurrentAddress { get; set; }

        /// <summary>
        /// Gets or sets the PermanentAddress value.
        /// </summary>
        [StringLength(500, ErrorMessage = "*")]
        public string PermanentAddress { get; set; }

        /// <summary>
        /// Gets or sets the ResContactNo value.
        /// </summary>
        [StringLength(20, ErrorMessage = "*")]
        public string ResContactNo { get; set; }

        /// <summary>
        /// Gets or sets the MobileNumber value.
        /// </summary>
        [StringLength(20, ErrorMessage = "*")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets the Email value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the GuardianMotherName value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string GuardianMotherName { get; set; }

        /// <summary>
        /// Gets or sets the GuardianFatherName value.
        /// </summary>
        [StringLength(150, ErrorMessage = "*")]
        public string GuardianFatherName { get; set; }

        /// <summary>
        /// Gets or sets the OccupationofMother value.
        /// </summary>
        [StringLength(200, ErrorMessage = "*")]
        public string OccupationofMother { get; set; }

        /// <summary>
        /// Gets or sets the OccupationofFather value.
        /// </summary>
        [StringLength(200, ErrorMessage = "*")]
        public string OccupationofFather { get; set; }

        /// <summary>
        /// Gets or sets the EducationofMother value.
        /// </summary>
        [StringLength(200, ErrorMessage = "*")]
        public string EducationofMother { get; set; }

        /// <summary>
        /// Gets or sets the EducationofFather value.
        /// </summary>
        [StringLength(200, ErrorMessage = "*")]
        public string EducationofFather { get; set; }

        /// <summary>
        /// Gets or sets the GuardianAddress value.
        /// </summary>
        [StringLength(-1, ErrorMessage = "*")]
        public string GuardianAddress { get; set; }

        /// <summary>
        /// Gets or sets the AnnualIncome value.
        /// </summary>
        public string AnnualIncome { get; set; }

        /// <summary>
        /// Gets or sets the GuardianTelephoneNo value.
        /// </summary>
        [StringLength(20, ErrorMessage = "*")]
        public string GuardianTelephoneNo { get; set; }

        /// <summary>
        /// Gets or sets the GuardianOffice value.
        /// </summary>
        [StringLength(20, ErrorMessage = "*")]
        public string GuardianOffice { get; set; }

        /// <summary>
        /// Gets or sets the GuardianMobile value.
        /// </summary>
        [StringLength(20, ErrorMessage = "*")]
        public string GuardianMobile { get; set; }

        /// <summary>
        /// Gets or sets the GuardianEmergencyConactNo value.
        /// </summary>
        [StringLength(20, ErrorMessage = "*")]
        public string GuardianEmergencyConactNo { get; set; }

        /// <summary>
        /// Gets or sets the GuardianEmail value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string GuardianEmail { get; set; }

        /// <summary>
        /// Gets or sets the NativePlaceAddress value.
        /// </summary>
        [StringLength(500, ErrorMessage = "*")]
        public string NativePlaceAddress { get; set; }

        /// <summary>
        /// Gets or sets the BankName value.
        /// </summary>
        [StringLength(200, ErrorMessage = "*")]
        public string BankName { get; set; }

        /// <summary>
        /// Gets or sets the BankAddress value.
        /// </summary>
        [StringLength(500, ErrorMessage = "*")]
        public string BankAddress { get; set; }

        /// <summary>
        /// Gets or sets the BankAccountNumber value.
        /// </summary>
        [StringLength(20, ErrorMessage = "*")]
        public string BankAccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the AccountType value.
        /// </summary>
        [StringLength(200, ErrorMessage = "*")]
        public string AccountType { get; set; }

        /// <summary>
        /// Gets or sets the IFSCCode value.
        /// </summary>
        [StringLength(20, ErrorMessage = "*")]
        public string IFSCCode { get; set; }

        /// <summary>
        /// Gets or sets the MICRNumber value.
        /// </summary>
        [StringLength(30, ErrorMessage = "*")]
        public string MICRNumber { get; set; }

        /// <summary>
        /// Gets or sets the OrganisationName value.
        /// </summary>
        [StringLength(200, ErrorMessage = "*")]
        public string OrganisationName { get; set; }

        /// <summary>
        /// Gets or sets the Designation value.
        /// </summary>
        [StringLength(300, ErrorMessage = "*")]
        public string Designation { get; set; }

        /// <summary>
        /// Gets or sets the TotalExperienceInMonth value.
        /// </summary>
        [StringLength(5, ErrorMessage = "*")]
        public string TotalExperienceInMonth { get; set; }

        /// <summary>
        /// Gets or sets the TotalExperienceInYear value.
        /// </summary>
        [StringLength(10, ErrorMessage = "*")]
        public string TotalExperienceInYear { get; set; }

        /// <summary>
        /// Gets or sets the Photo value.
        /// </summary>
        [StringLength(255, ErrorMessage = "*")]
        public string Photo { get; set; }

        /// <summary>
        /// Gets or sets the SignaturePath value.
        /// </summary>
        [StringLength(255, ErrorMessage = "*")]
        public string SignaturePath { get; set; }

        /// <summary>
        /// Gets or sets the ParentSignaturePath value.
        /// </summary>
        [StringLength(255, ErrorMessage = "*")]
        public string ParentSignaturePath { get; set; }

        /// <summary>
        /// Gets or sets the IsSvtStudentFrom value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public bool? IsSvtStudentFrom { get; set; }

        public string IsSvtKnowRefrence { get; set; }

        /// <summary>
        /// Gets or sets the KnowAboutCourse value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string KnowAboutCourse { get; set; }

        /// <summary>
        /// Gets or sets the OtherSpecifyHowYouknowCourses value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string OtherSpecifyHowYouknowCourses { get; set; }

        /// <summary>
        /// Gets or sets the PGYearofPassing value.
        /// </summary>
        [StringLength(20, ErrorMessage = "*")]
        public string PGYearofPassing { get; set; }

        /// <summary>
        /// Gets or sets the PGSchoolName value.
        /// </summary>
        [StringLength(200, ErrorMessage = "*")]
        public string PGSchoolName { get; set; }

        /// <summary>
        /// Gets or sets the PGMedium value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string PGMedium { get; set; }

        /// <summary>
        /// Gets or sets the PGBoardName value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string PGBoardName { get; set; }

        /// <summary>
        /// Gets or sets the PGTotalPercent value.
        /// </summary>
        [StringLength(10, ErrorMessage = "*")]
        public string PGTotalPercent { get; set; }

        /// <summary>
        /// Gets or sets the PGGrade value.
        /// </summary>
        [StringLength(10, ErrorMessage = "*")]
        public string PGGrade { get; set; }

        /// <summary>
        /// Gets or sets the BachelorYearofPassing value.
        /// </summary>
        [StringLength(20, ErrorMessage = "*")]
        public string BachelorYearofPassing { get; set; }

        /// <summary>
        /// Gets or sets the BachelorSchoolName value.
        /// </summary>
        [StringLength(200, ErrorMessage = "*")]
        public string BachelorSchoolName { get; set; }

        /// <summary>
        /// Gets or sets the BachelorMedium value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string BachelorMedium { get; set; }

        /// <summary>
        /// Gets or sets the BachelorBoardName value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string BachelorBoardName { get; set; }

        /// <summary>
        /// Gets or sets the BachelorTotalPercent value.
        /// </summary>
        [StringLength(10, ErrorMessage = "*")]
        public string BachelorTotalPercent { get; set; }

        /// <summary>
        /// Gets or sets the BachelorGrade value.
        /// </summary>
        [StringLength(10, ErrorMessage = "*")]
        public string BachelorGrade { get; set; }

        /// <summary>
        /// Gets or sets the HscYearofPassing value.
        /// </summary>
        [StringLength(20, ErrorMessage = "*")]
        public string HscYearofPassing { get; set; }

        /// <summary>
        /// Gets or sets the HscSchoolName value.
        /// </summary>
        [StringLength(200, ErrorMessage = "*")]
        public string HscSchoolName { get; set; }

        /// <summary>
        /// Gets or sets the HscMedium value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string HscMedium { get; set; }

        /// <summary>
        /// Gets or sets the HscBoardName value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string HscBoardName { get; set; }

        /// <summary>
        /// Gets or sets the HscTotalPercent value.
        /// </summary>
        [StringLength(10, ErrorMessage = "*")]
        public string HscTotalPercent { get; set; }

        /// <summary>
        /// Gets or sets the HscGrade value.
        /// </summary>
        [StringLength(10, ErrorMessage = "*")]
        public string HscGrade { get; set; }

        /// <summary>
        /// Gets or sets the SscYearofPassing value.
        /// </summary>
        [StringLength(20, ErrorMessage = "*")]
        public string SscYearofPassing { get; set; }

        /// <summary>
        /// Gets or sets the SscSchoolName value.
        /// </summary>
        [StringLength(200, ErrorMessage = "*")]
        public string SscSchoolName { get; set; }

        /// <summary>
        /// Gets or sets the SscMedium value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string SscMedium { get; set; }

        /// <summary>
        /// Gets or sets the SscBoardName value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string SscBoardName { get; set; }

        /// <summary>
        /// Gets or sets the SscTotalPercent value.
        /// </summary>
        [StringLength(10, ErrorMessage = "*")]
        public string SscTotalPercent { get; set; }

        /// <summary>
        /// Gets or sets the SscGrade value.
        /// </summary>
        [StringLength(10, ErrorMessage = "*")]
        public string SscGrade { get; set; }

        /// <summary>
        /// Gets or sets the ExaminationType value.
        /// </summary>
        [StringLength(100, ErrorMessage = "*")]
        public string ExaminationType { get; set; }

        /// <summary>
        /// Gets or sets the OtherExaminationType value.
        /// </summary>
        [StringLength(200, ErrorMessage = "*")]
        public string OtherExaminationType { get; set; }

        /// <summary>
        /// Gets or sets the HobbiesOrSpecailInterest value.
        /// </summary>
        [StringLength(500, ErrorMessage = "*")]
        public string HobbiesOrSpecailInterest { get; set; }

        /// <summary>
        /// Gets or sets the HonorPrizeName value.
        /// </summary>
        [StringLength(500, ErrorMessage = "*")]
        public string HonorPrizeName { get; set; }

        /// <summary>
        /// Gets or sets the Note value.
        /// </summary>
        [StringLength(-1, ErrorMessage = "*")]
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the DateRegistered value.
        /// </summary>
        [Required(ErrorMessage = "*")]
        public DateTime? DateRegistered { get; set; }

        /// <summary>
        /// Gets or sets the IsSubmitted value.
        /// </summary>
        public bool? IsSubmitted { get; set; }

        /// <summary>
        /// Gets or sets the DateSubmitted value.
        /// </summary>
        public DateTime? DateSubmitted { get; set; }

        /// <summary>
        /// Gets or sets the LastModified value.
        /// </summary>
        public DateTime? LastModified { get; set; }

        /// <summary>
        /// Gets or sets the LastModified2 value.
        /// </summary>
        public DateTime? LastModified2 { get; set; }

        /// <summary>
        /// Gets or sets the IsDuplicateAadhar value.
        /// </summary>
        public bool? IsDuplicateAadhar { get; set; }

        [NotMapped]
        public string EncryptedId { get; set; }


        [NotMapped]
        public string StudentPdfURL { get; set; }
        #endregion
    }
}
