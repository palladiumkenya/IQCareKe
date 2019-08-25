IF OBJECT_ID('dbo.patientEncounterView', 'V') IS NOT NULL
    DROP VIEW [dbo].[VisitDetailsView]
GO

ALTER VIEW [dbo].[patientEncounterView]
AS
SELECT DISTINCT vd.Id,pe.Id PatientEncounterId,pe.PatientMasterVisitId,pe.EncounterTypeId,pe.EncounterStartTime,pe.EncounterEndTime,
vd.VisitNumber,lk.ItemName AS encounter,CAST(DECRYPTBYKEY(u.UserFirstName) AS VARCHAR(50))  + ' '+ CAST(DECRYPTBYKEY(u.UserLastName) AS VARCHAR(50)) AS username,
pa.PersonId,vd.PatientId,pe.DeleteFlag
FROM VisitDetails vd  
INNER JOIN PatientEncounter pe 
ON pe.PatientMasterVisitId = vd.PatientMasterVisitId
INNER JOIN Patient pa ON pa.Id = pe.PatientId
INNER JOIN LookupItemView lk ON lk.ItemId = pe.EncounterTypeId
LEFT JOIN mst_User u ON u.UserID = pe.CreatedBy
WHERE pe.DeleteFlag=0 
GO