
IF OBJECT_ID('dbo.[patientEncounterView]', 'V') IS NOT NULL
    DROP VIEW [dbo].[patientEncounterView]
GO
CREATE VIEW [dbo].[patientEncounterView]
AS
SELECT vd.Id,pe.Id PatientEncounterId,pe.PatientMasterVisitId,pe.EncounterTypeId,pe.EncounterStartTime,pe.EncounterEndTime,
vd.VisitNumber,lk.ItemName AS encounter,CAST(DECRYPTBYKEY(u.UserFirstName) AS VARCHAR(50))  + ' '+ CAST(DECRYPTBYKEY(u.UserLastName) AS VARCHAR(50)) AS username,
pa.PersonId,vd.PatientId,pe.DeleteFlag
FROM PatientEncounter pe 
INNER JOIN VisitDetails vd 
ON pe.PatientMasterVisitId = vd.PatientMasterVisitId
INNER JOIN Patient pa ON pa.Id = vd.PatientId
INNER JOIN LookupItemView lk ON lk.ItemId = pe.EncounterTypeId
LEFT JOIN mst_User u ON u.UserID = pe.CreatedBy
WHERE pe.DeleteFlag=0
GO
GO


