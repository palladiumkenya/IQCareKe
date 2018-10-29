
ALTER PROCEDURE [dbo].[sp_getPatientWorkPlan]
	-- Add the parameters for the stored procedure here
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	select convert(varchar(20),b.visitdate,106) visitDate, a.clinicalNotes
	from patientclinicalnotes a inner join PatientMasterVisit b on a.PatientMasterVisitId = b.Id
	where b.patientid = @PatientID and b.DeleteFlag <> 1 and NotesCategoryId is null
	and LEN(a.ClinicalNotes) > 0 
	order by b.visitdate desc
	
End










GO


