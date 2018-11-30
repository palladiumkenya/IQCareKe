

-- =============================================
-- Author:		John Macharia
-- Create date: 14th Feb 2017
-- Description:	get patient encounter Diagnosis
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPatientEncounterDiagnosis]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	select CASE WHEN a.LookupTableFlag='0' then a.Diagnosis + '~'+'mstICD' WHEN a.LookupTableFlag='1' then a.Diagnosis +'~' + 'lookupitem' else a.Diagnosis +'~' + 'lookupitem' end as Diagnosis ,
	CASE WHEN a.LookupTableFlag='0' then icd.[Name] when a.LookupTableFlag='1' then  
	 b.DisplayName end as DisplayName, ManagementPlan,a.DeleteFlag
	from PatientDiagnosis a inner join lookupitem b on a.diagnosis = b.id
	left join mst_ICDCodes icd on icd.Id=a.Diagnosis
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and (a.DeleteFlag is null or a.DeleteFlag = 0)
	
End









GO


