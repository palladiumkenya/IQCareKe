CREATE VIEW PatientDischargeInformationView
AS
SELECT po.Id,po.PatientMasterVisitID, po.OutcomeStatus as OutcomeStatusId,lki.DisplayName as OutcomeStatus,
po.OutcomeDescription,po.DateDischarged,po.DateCreated,po.CreatedBy,po.DeleteFlag from PatientOutcome po 
INNER JOIN LookupItem lki 
ON lki.Id = po.OutcomeStatus