
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_savePatientEncounterChronicIllness]') AND type in (N'P', N'PC'))
BEGIN
/****** Object:  StoredProcedure [dbo].[sp_savePatientEncounterChronicIllness]    Script Date: 11/27/2018 1:21:03 PM ******/

-- =============================================
-- Author:		John Macharia
-- Create date: 29th Jan 2017
-- Description:	save patient encounter - Chronic Illness
-- =============================================
EXEC('ALTER PROCEDURE [dbo].[sp_savePatientEncounterChronicIllness]
	-- Add the parameters for the stored procedure here
	@MasterVisitID int = null,
	@PatientID int = null,
	@chronicIllness varchar(50) = null,
	@treatment varchar(250) = null,
	@dose varchar(50) = null,
	@onsetDate varchar(20) = null,
	@active int = null,
	@userID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
IF @dose=N''''
BEGIN
SET @dose=null;
END
-- Insert statements for procedure here
	if exists(select 1 from PatientChronicIllness where ChronicIllness = @chronicIllness and PatientId = @PatientID)
	BEGIN
		update PatientChronicIllness set Treatment = @treatment,Dose = @dose,OnsetDate = @onsetDate, active = @active, DeleteFlag = 0
		where ChronicIllness = @chronicIllness and PatientId = @PatientID
	END
	ELSE
	BEGIN
		insert into PatientChronicIllness(PatientId,PatientMasterVisitId,ChronicIllness,Treatment,Dose,OnsetDate,active,DeleteFlag,CreateBy,CreateDate) 
		values(@PatientID,@MasterVisitID,@chronicIllness,@treatment,@dose,@onsetDate,@active,0,@userID,GETDATE())
	END
	
End ')
END







GO


