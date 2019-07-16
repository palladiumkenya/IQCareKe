/****** Object:  View [dbo].[PREP_EncountersView]    Script Date: 6/13/2019 12:06:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PREP_EncountersView]
AS
SELECT 
ISNULL(ROW_NUMBER() OVER(ORDER BY PE.Id ASC), -1) AS RowID,
PE.Id, 
PM.Id PatientMasterVisitId,
PE.PatientId, 
PE.ServiceAreaId, 
PE.EncounterTypeId, 
REPLACE(ltv.DisplayName,'-encounter','') as EncounterType,
PS.PrepStatusToday, 
PA.AppointmentDate,
PE.EncounterStartTime,
PreStatus = (SELECT TOP 1 ItemName FROM LookupItemView WHERE ItemId = PS.PrepStatusToday AND MasterName = 'PrEP_Status'),
ProviderName = (SELECT TOP 1 (UserFirstName + ' ' + UserLastName) FROM [dbo].[mst_User] WHERE UserID = PM.CreatedBy)

FROM PatientMasterVisit PM
INNER JOIN PatientEncounter PE ON PE.PatientMasterVisitId = PM.Id
LEFT JOIN PatientPrEPStatus PS ON PS.PatientEncounterId = PE.Id
LEFT JOIN PatientAppointment PA ON PA.PatientMasterVisitId = PM.Id
LEFT JOIN LookupItemView ltv  on ltv.ItemId=PE.EncounterTypeId
LEFT JOIN ServiceArea sa on sa.Id=PM.ServiceId
where sa.Code='PREP' and ltv.ItemName !='CareEnded'

GO
