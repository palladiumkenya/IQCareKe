SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PREP_EncountersView]
AS
SELECT 
ISNULL(ROW_NUMBER() OVER(ORDER BY PE.Id ASC), -1) AS RowID,
PE.Id, 
PE.PatientId, 
PE.ServiceAreaId, 
PE.EncounterTypeId, 
PS.PrepStatusToday, 
PA.AppointmentDate 

FROM PatientMasterVisit PM
INNER JOIN PatientEncounter PE ON PE.PatientMasterVisitId = PM.Id
LEFT JOIN PatientPrEPStatus PS ON PS.PatientEncounterId = PE.Id
LEFT JOIN PatientAppointment PA ON PA.PatientMasterVisitId = PM.Id

GO