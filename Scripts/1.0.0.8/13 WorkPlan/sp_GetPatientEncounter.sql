

ALTER PROCEDURE [dbo].[sp_getPatientEncounter]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
	declare @TBScreeningTypeID int = (Select Id from LookupMaster where name = 'TBStatus')
	declare @NutritionScreeningTypeID int = (Select Id from LookupMaster where name = 'NutritionStatus')
	declare @GeneralExamMasterId int = (Select top 1 Id from LookupMaster where name = 'GeneralExamination')
	declare @ARVAdherenceType int = (Select Id from LookupMaster where name = 'ARVAdherence')
	declare @CTXAdherenceType int = (Select Id from LookupMaster where name = 'CTXAdherence')
	declare @StabilityAssessmentType int = (Select Id from LookupMaster where name = 'StabilityAssessment')

	--0
	--select * from PatientMasterVisit 
	--where id = @PatientMasterVisitID and patientId = @PatientID	and (DeleteFlag is null OR DeleteFlag = 0)
	Select id, PatientId, ServiceId, Start, [End], Active, REPLACE(CONVERT(VARCHAR(11), VisitDate, 106), ' ', '-') VisitDate, VisitScheduled, VisitBy, VisitType, [Status], CreateDate,DeleteFlag,	CreatedBy, AuditData FROM PatientMasterVisit
	where id = @PatientMasterVisitID and patientId = @PatientID	and (DeleteFlag is null OR DeleteFlag = 0)
	
	--1
	select * from ComplaintsHistory where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID
	and (DeleteFlag is null OR DeleteFlag = 0)
	
	--2
	select * from PhysicalExamination where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID
	and ExaminationTypeId = @GeneralExamMasterId and (DeleteFlag is null OR DeleteFlag = 0)
	
	--TB Screening 3
	select ScreeningValueId from PatientScreening 
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and ScreeningTypeId = @TBScreeningTypeID
	and (DeleteFlag is null OR DeleteFlag = 0)
	
	--Nutrition Screening 4
	select ScreeningValueId from PatientScreening 
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and ScreeningTypeId = @NutritionScreeningTypeID
	and (DeleteFlag is null OR DeleteFlag = 0)
	
	--5
	select * from PatientPHDP where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID 
	and (DeleteFlag is null OR DeleteFlag = 0)

	--6 ARV Adherence
	select Score from AdherenceOutcome 
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and AdherenceType = @ARVAdherenceType
	and (DeleteFlag is null OR DeleteFlag = 0)

	--7 CTX Adherence
	select Score from AdherenceOutcome 
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and AdherenceType = @CTXAdherenceType
	and (DeleteFlag is null OR DeleteFlag = 0)

	--8 workplan
	select * from PatientClinicalNotes
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID 
	and deleteflag <> 1 and NotesCategoryId is null

	--9 ICF
	select * from [dbo].[PatientIcf]
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and deleteflag <> 1

	--10 ICF Action
	select * from [dbo].[PatientIcfAction]
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and deleteflag <> 1

	--11 IPT
	select * from [dbo].[PatientIpt]
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and deleteflag <> 1

	--12 IPT Outcome
	select * from [dbo].[PatientIptOutcome]
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and deleteflag <> 1

	--13 IPT Workup
	select *, REPLACE(CONVERT(VARCHAR(11), iptstartdate, 106), ' ', '-') StartIPTDate from [dbo].[PatientIptWorkup]
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and deleteflag <> 1
	
	--14 WHO Stage
	select * from [dbo].[PatientWHOStage]
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID

	--15 appointment
	Select Id, PatientMasterVisitId, ServiceAreaId, PatientId, REPLACE(CONVERT(VARCHAR(11), AppointmentDate, 106), ' ', '-') AppointmentDate, ReasonId, Description, StatusId, StatusDate, DifferentiatedCareId, DeleteFlag, CreatedBy, CreateDate, AuditData from PatientAppointment
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID

	--16 Stability Categorization
	select Categorization from PatientCategorization 
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID
	and (DeleteFlag is null OR DeleteFlag = 0)

End



