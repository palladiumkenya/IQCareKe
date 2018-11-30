IF OBJECT_ID('dbo.patientEncounterView', 'V') IS NOT NULL
    DROP VIEW [dbo].[patientEncounterView]
GO

CREATE VIEW [dbo].[patientEncounterView]
AS
/*SELECT 
p.Id,
e.Id PatientEncounterId,
e.PatientMasterVisitId,
e.EncounterTypeId,
e.EncounterStartTime,
e.EncounterEndTime,
 p.PatientId,
 (SELECT i.PersonId FROM Patient i WHERE i.Id=p.PatientId) PersonId,
  p.VisitNumber,
  (SELECT top 1 ItemName FROM LookupItemView WHERE ItemId=e.EncounterTypeId) [encounter],
  (SELECT CAST(DECRYPTBYKEY(u.UserFirstName) AS VARCHAR(50))  + ' '+ CAST(DECRYPTBYKEY(u.UserLastName) AS VARCHAR(50))  FROM mst_User u WHERE u.UserID=e.PatientMasterVisitId ) [userName],
  e.DeleteFlag
FROM  
          dbo.PatientProfile p
INNER JOIN 
PatientEncounter e
ON
e.PatientMasterVisitId= p.PatientMasterVisitId
WHERE 
e.DeleteFlag=0 AND p.DeleteFlag=0*/

SELECT 
p.Id,
e.Id PatientEncounterId,
e.PatientMasterVisitId,
e.EncounterTypeId,
e.EncounterStartTime,
e.EncounterEndTime,
p.PatientId,
(SELECT i.PersonId FROM Patient i WHERE i.Id=p.PatientId) PersonId,
p.VisitNumber,
(SELECT top 1 ItemName FROM LookupItemView WHERE ItemId=e.EncounterTypeId) [encounter],
(SELECT CAST(DECRYPTBYKEY(u.UserFirstName) AS VARCHAR(50)) + ' '+ CAST(DECRYPTBYKEY(u.UserLastName) AS VARCHAR(50)) FROM mst_User u WHERE u.UserID=e.PatientMasterVisitId ) [userName],
e.DeleteFlag
FROM 
dbo.VisitDetails p
INNER JOIN 
PatientEncounter e
ON
e.PatientMasterVisitId= p.PatientMasterVisitId
WHERE 
e.DeleteFlag=0 AND p.DeleteFlag=0
GO




