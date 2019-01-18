IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterVaccines]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPatientEncounterVaccines]
GO

/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterVaccines]    Script Date: 1/18/2019 11:51:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		John Macharia
-- Create date: 14th Feb 2017
-- Description:	get patient encounter Vaccines
-- =============================================
CREATE PROCEDURE [dbo].[sp_getPatientEncounterVaccines]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	select vaccine vaccineID, VaccineStageId as  VaccineStageID, b.DisplayName VaccineName, c.DisplayName VaccineStageName, Convert(varchar(12),VaccineDate,106)VaccineDate
	from Vaccination a 
	inner join LookupItem b on a.Vaccine = b.Id
	left join LookupItem c on a.VaccineStageId = c.Id
	where patientId = @PatientID and (a.DeleteFlag is null or a.DeleteFlag = 0)
	
End








GO


