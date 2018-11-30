

ALTER PROCEDURE [dbo].[sp_savePatientEncounterPatientManagement]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null,
	@ARVAdherence int = null,
	@CTXAdherence int = null,
	@WorkPlan varchar(1000) = null,
	@userID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
-- Insert statements for procedure here
	declare @ARVAdherenceType int = (Select Id from LookupMaster where name = 'ARVAdherence')
	declare @CTXAdherenceType int = (Select Id from LookupMaster where name = 'CTXAdherence')

	if(@PatientMasterVisitID > 0)
	BEGIN
		if(@ARVAdherence > 0)
		BEGIN
			if exists(select 1 from AdherenceOutcome where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID and AdherenceType = @ARVAdherenceType)
			BEGIN
				update AdherenceOutcome set Score = @ARVAdherence
				where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID and AdherenceType = @ARVAdherenceType
			END
			ELSE
			BEGIN
				insert into [dbo].[AdherenceOutcome] (PatientId, PatientMasterVisitId,AdherenceType,Score,DeleteFlag,CreateBy,CreateDate)
				values(@PatientID,@PatientMasterVisitID,@ARVAdherenceType,@ARVAdherence,0,@userID,GETDATE())
			END
		END

		if(@CTXAdherence > 0)
		BEGIN
			if exists(select 1 from AdherenceOutcome where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID and AdherenceType = @CTXAdherenceType)
			BEGIN
				update AdherenceOutcome set Score = @CTXAdherence
				where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID and AdherenceType = @CTXAdherenceType
			END
			ELSE
			BEGIN
				insert into [dbo].[AdherenceOutcome] (PatientId, PatientMasterVisitId,AdherenceType,Score,DeleteFlag,CreateBy,CreateDate)
				values(@PatientID,@PatientMasterVisitID,@CTXAdherenceType,@CTXAdherence,0,@userID,GETDATE())
			END
		END

		if exists(select 1 from PatientClinicalNotes where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID and NotesCategoryId is null)
		BEGIN
			update PatientClinicalNotes set ClinicalNotes = @WorkPlan
			where PatientMasterVisitId = @PatientMasterVisitID and PatientId = @PatientID
		END
		ELSE
		BEGIN
			insert into PatientClinicalNotes(PatientId,PatientMasterVisitId,ServiceAreaId,ClinicalNotes,DeleteFlag,CreatedBy,CreateDate,Active) 
			values(@PatientID,@PatientMasterVisitID,203,@WorkPlan,0,@userID,GETDATE(),0)
		END
	END
	ELSE
	BEGIN
		if(@ARVAdherence > 0)
		BEGIN
			insert into [dbo].[AdherenceOutcome] (PatientId, PatientMasterVisitId,AdherenceType,Score,DeleteFlag,CreateBy,CreateDate)
			values(@PatientID,@PatientMasterVisitID,@ARVAdherenceType,@ARVAdherence,0,@userID,GETDATE())
		END

		if(@CTXAdherence > 0)
		BEGIN
			insert into [dbo].[AdherenceOutcome] (PatientId, PatientMasterVisitId,AdherenceType,Score,DeleteFlag,CreateBy,CreateDate)
			values(@PatientID,@PatientMasterVisitID,@CTXAdherenceType,@CTXAdherence,0,@userID,GETDATE())
		END

		insert into PatientClinicalNotes(PatientId,PatientMasterVisitId,ServiceAreaId,ClinicalNotes,DeleteFlag,CreatedBy,CreateDate,Active) 
		values(@PatientID,@PatientMasterVisitID,203,@WorkPlan,0,@userID,GETDATE(),0)
	END
End








GO


