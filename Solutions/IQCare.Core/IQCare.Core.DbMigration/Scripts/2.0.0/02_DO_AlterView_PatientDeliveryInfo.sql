IF OBJECT_ID('dbo.PatientDeliveryInformationView', 'V') IS NOT NULL
    DROP VIEW [dbo].[PatientDeliveryInformationView]
GO

ALTER VIEW PatientDeliveryInformationView AS
SELECT pt.DeliveryID AS Id, pt.PatientMasterVisitID AS PatientMasterVisitId, pt.ProfileID AS ProfileId, 
pt.DurationOfLabour, pt.DateOfDelivery, pt.TimeOfDelivery,pt.BloodLossCapacity, blc.DisplayName AS BloodLossClassification,
pt.BloodLossClassification AS BloodLossClassificationId,
pt.CreateDate,pt.MaternalDeathAuditDate, pt.DeliveryConductedBy,mcon.DisplayName AS MotherCondition,pt.MotherCondition AS MotherConditionId,
delivery.DisplayName  AS ModeOfDelivery,pt.ModeOfDelivery AS ModeOfDeliveryId, plc.DisplayName AS PlacentaComplete,pt.PlacentaComplete AS PlacentaCompleteId,
dlc.DisplayName AS DeliveryComplicationsExperienced,
pt.DeliveryComplicationsExperienced AS DeliveryComplicationsExperiencedId,pt.MaternalDeathAudited AS MaternalDeathAuditedId ,
mda.DisplayName AS MaternalDeathAudited, pt.CreatedBy, pt.DeliveryComplicationNotes
FROM PatientDelivery pt 
INNER JOIN LookupItem mcon ON mcon.Id = pt.MotherCondition
INNER JOIN LookupItem delivery ON delivery.Id = pt.ModeOfDelivery
INNER JOIN LookupItem plc ON plc.Id = pt.PlacentaComplete
INNER JOIN LookupItem dlc ON dlc.Id = pt.DeliveryComplicationsExperienced
INNER JOIN LookupItem blc ON blc.Id = pt.BloodLossClassification
LEFT JOIN LookupItem mda ON mda.Id = pt.MaternalDeathAudited