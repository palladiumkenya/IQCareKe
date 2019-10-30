ALTER VIEW [dbo].[VW_PatientCareEnding]
AS
SELECT 
P.ptn_pk,
PM.VisitDate,
PC.ExitReason,
[Patient CareEnd Reason] = (SELECT ItemName FROM LookupItemView WHERE ItemId = PC.ExitReason AND MasterName = 'CareEnded'),
PC.TransferOutfacility AS LPTFTransfer,
PC.DateOfDeath,
PC.ExitDate AS CareEndedDate,
PC.Id AS CareEndedID,
PC.CareEndingNotes,
PC.Active,
PC.DeleteFlag

FROM PatientCareending PC
INNER JOIN Patient P ON P.Id = PC.PatientId
left JOIN PatientMasterVisit PM ON PM.PatientId = P.Id AND PM.Id = pc.PatientMasterVisitId
WHERE PC.DeleteFlag=0