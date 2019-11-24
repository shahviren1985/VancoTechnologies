ALTER TABLE StudentDetails ADD [IsDuplicate] [bit] NULL

GO

-- =============================================
-- Author:	Kaushik
-- Create date:	13-Feb-2018
-- Description:	Save StudentDetails Data
-- =============================================
ALTER PROCEDURE [dbo].[UspStudentDetailsSearch] (
	@Id int= NULL,
	@LastName varchar(100)= NULL,
	@Dob date= NULL,
	@MKCLFormNumber varchar(100)= NULL,
	@StartRowIndex int = NULL,
	@EndRowIndex int = NULL,
	@SortExpression VARCHAR(50) = NULL,
	@SortDirection VARCHAR(10) = NULL
)
AS

SET NOCOUNT ON

-- Get Total Count
-- List the selected Page records from database
IF @SortExpression IS NULL
BEGIN
	SET @SortExpression = 'Id'
END

;WITH AllRecord AS
(
SELECT ROW_NUMBER() OVER (ORDER BY CASE 
WHEN @SortExpression = 'Id' and @SortDirection ='Descending'  THEN m.Id END DESC,
	CASE WHEN @SortExpression = 'Id' THEN m.Id END ASC,
m.Id) As RowIndex,
	 m.[Id],
	 m.[FirstName],
	 m.[LastName],
	 m.[FatherName],
	 m.[MotherName],
	 m.[CurrentAddress],
	 m.[PermanentAddress],
	 m.[FatherFirstName],
	 m.[FatherMiddleName],
	 m.[FatherLastName],
	 m.[MotherFirstName],
	 m.[MotherMiddleName],
	 m.[MotherLastName],
	 m.[Gender],
	 m.[Dob],
	 m.[MobileNumber],
	 m.[Email],
	 m.[MarksObtain],
	 m.[TotalMarks],
	 m.[Percentage],
	 m.[NumbrrAttends],
	 m.[Photo],
	 m.[SignaturePath],
	 m.[IsScience],
	 m.[Subject1Name],
	 m.[Subject1MarksObtained],
	 m.[Subject1Total],
	 m.[Subject2Name],
	 m.[Subject2MarksObtained],
	 m.[Subject2Total],
	 m.[Subject3Name],
	 m.[Subject3MarksObtained],
	 m.[Subject3Total],
	 m.[Subject4Name],
	 m.[Subject4MarksObtained],
	 m.[Subject4Total],
	 m.[Subject5Name],
	 m.[Subject5MarksObtained],
	 m.[Subject5Total],
	 m.[Subject6Name],
	 m.[Subject6MarksObtained],
	 m.[Subject6Total],
	 m.[Subject7Name],
	 m.[Subject7MarksObtained],
	 m.[Subject7Total],
	 m.[CoursePreference1],
	 m.[CoursePreference2],
	 m.[CoursePreference3],
	 m.[CoursePreference4],
	 m.[CoursePreference5],
	 m.[CoursePreference6],
	 m.[CoursePreference7],
	 m.[Preference1GE1],
	 m.[Preference1GE2],
	 m.[Preference2GE1],
	 m.[Preference2GE2],
	 m.[Preference3GE1],
	 m.[Preference3GE2],
	 m.[Preference4GE1],
	 m.[Preference4GE2],
	 m.[Preference5GE1],
	 m.[Preference5GE2],
	 m.[Preference6GE1],
	 m.[Preference6GE2],
	 m.[Preference7GE1],
	 m.[Preference7GE2],
	 m.[IsAdmitted],
	 m.[YearLastExamPassed],
	 m.[AadharNumber],
	 m.[Nationality],
	 m.[MotherTongue],
	 m.[Remarks],
	 m.[SubCourse],
	 m.[MKCLFormNumber],
	 m.[IsSVTStudent],
	 m.[StudentType],
	 m.[Category],
	 m.[HscStream],
	 m.[MonthLastExamPassed],
	 m.[AttemptofHSC],
	 m.[MaritalStatus],
	 m.[PlaceofBirth],
	 m.[BloodGroup],
	 m.[EmergencyTel],
	 m.[GuardianOccupation],
	 m.[GuardianSalary],
	 m.[RelwithGurdian],
	 m.[EmploymentStatus],
	 m.[WishtojoinNCC],
	 m.[CourseAdmittedRound1],
	 m.[AdmittedRound1],
	 m.[RejectedAdmissionRound1],
	 m.[TransferReasonRound1],
	 m.[DateAdmittedRound1],
	 m.[RejectedRoundnmbr],
	 m.[CourseAdmittedRound2],
	 m.[AdmittedRound2],
	 m.[RejectedAdmissionRound2],
	 m.[TransferReasonRound2],
	 m.[DateAdmittedRound2],
	 m.[CourseAdmittedRound3],
	 m.[AdmittedRound3],
	 m.[RejectedAdmissionRound3],
	 m.[TransferReasonRound3],
	 m.[DateAdmittedRound3],
	 m.[IsNRIStudent],
	 m.[IsLearningDisability],
	 m.[IsFeesPaid],
	 m.[IsCancelled],
	 m.[Caste],
	 m.[SubCaste],
	 m.[Religion],
	 m.[LastSchoolAttend],
	 m.[NameofExaminationBoard],
	 m.[DateRegistered],
	 m.[LastModified],
	m.[LastModified2],
	 m.[IsSubmitted],
	m.[DateSubmitted],
	m.[IsDuplicate]
FROM
	[StudentDetails] as m
WHERE 1=1 
	 AND (m.Id = @Id OR ISNULL(@Id,0)=0)
	 AND (m.LastName = @LastName OR @LastName IS NULL)	
	 AND (m.Dob = @Dob OR @Dob IS NULL)	
	 AND (m.MKCLFormNumber = @MKCLFormNumber OR @MKCLFormNumber IS NULL)	
),
AllRecordCount AS
(
SELECT COUNT(1) TotalRecords FROM AllRecord
)
SELECT * FROM AllRecord,AllRecordCount
WHERE RowIndex >= ISNULL(@StartRowIndex,0) and RowIndex<= ISNULL (@EndRowIndex,RowIndex)


GO

CREATE procedure [dbo].[UspGetStudentList]
@Category varchar(100),
@IsSVTStudent bit 
AS
BEGIN
	SELECT * FROM StudentDetails 
	WHERE IsSubmitted = 1 AND ISNULL(IsCancelled,0)=0 AND ISNULL(IsDuplicate,0)=0
		AND Category = @Category AND IsSVTStudent = @IsSVTStudent  
	ORDER BY [Percentage] DESC
END
GO

CREATE Procedure  [dbo].[UpdatePreferenceByCourse]
@Id int=null,
@CourseAdmitted varchar(100)=null,
@Round varchar(100) =null
as

BEGIN
		  IF @Round='Round1'
			  BEGIN
				Update StudentDetails set  CourseAdmittedRound1=@CourseAdmitted,AdmittedRound1=1,DateAdmittedRound1=GETUTCDATE() where id=@Id
			  END
		  ELSE IF @Round='Round2'
			  BEGIN
				Update StudentDetails set  CourseAdmittedRound2=@CourseAdmitted,AdmittedRound2=1,DateAdmittedRound2=GETUTCDATE() where id=@Id
			  END
		  ELSE IF @Round='Round3'
			  BEGIN
				Update StudentDetails set  CourseAdmittedRound3=@CourseAdmitted,AdmittedRound3=1,DateAdmittedRound3=GETUTCDATE() where id=@Id
			  END	
END

GO