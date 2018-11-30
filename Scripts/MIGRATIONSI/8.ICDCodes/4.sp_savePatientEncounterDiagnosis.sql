

/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterDiagnosis]    Script Date: 10/29/2018 9:50:57 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		John Macharia
-- Create date: 29th Jan 2017
-- Description:	save patient encounter - Adverse Events
-- =============================================
ALTER PROCEDURE [dbo].[sp_savePatientEncounterDiagnosis]
	-- Add the parameters for the stored procedure here
	@masterVisitID int = null,
	@PatientID int = null,
	@diagnosis varchar(250) = null,
	@treatment varchar(250) = null,
	@userID int = null,
	@LookupTableFlag bit=null,
	@DeleteFlag bit=0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	if exists(select 1 from PatientDiagnosis where Diagnosis = @diagnosis and PatientId = @PatientID and PatientMasterVisitID = @masterVisitID and LookupTableFlag=@LookupTableFlag)
	BEGIN
		update PatientDiagnosis set ManagementPlan = @treatment, DeleteFlag = @DeleteFlag,LookupTableFlag=@LookupTableFlag
		where Diagnosis = @diagnosis and PatientId = @PatientID and PatientMasterVisitID = @masterVisitID 
	END
	ELSE
	BEGIN
		insert into PatientDiagnosis(PatientId,PatientMasterVisitId,Diagnosis,ManagementPlan,DeleteFlag,CreateBy,CreateDate,LookupTableFlag) 
		values(@PatientID,@MasterVisitID,@diagnosis,@treatment,@DeleteFlag,@userID,GETDATE(),@LookupTableFlag)
	END
End








GO


