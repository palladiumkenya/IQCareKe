ALTER VIEW PatientDeliveryInformationView AS
SELECT pt.DeliveryID AS Id, pt.PatientMasterVisitID AS PatientMasterVisitId, pt.PregnancyId, 
pt.DurationOfLabour, pt.DateOfDelivery, pt.TimeOfDelivery,pt.BloodLossCapacity, blc.DisplayName AS BloodLossClassification,
  pt.CreateDate,pt.MaternalDeathAuditDate, pt.DeliveryConductedBy,mcon.DisplayName AS MotherCondition,
delivery.DisplayName  AS ModeOfDelivery, plc.DisplayName AS PlacentaComplete,dlc.DisplayName AS DeliveryComplicationsExperienced, 
mda.DisplayName AS MaternalDeathAudited, pt.CreatedBy, pt.DeliveryComplicationNotes
FROM PatientDelivery pt 
INNER JOIN LookupItem mcon ON mcon.Id = pt.MotherCondition
INNER JOIN LookupItem delivery ON delivery.Id = pt.ModeOfDelivery
INNER JOIN LookupItem plc ON plc.Id = pt.PlacentaComplete
INNER JOIN LookupItem dlc ON dlc.Id = pt.DeliveryComplicationsExperienced
INNER JOIN LookupItem blc ON blc.Id = pt.BloodLossClassification
LEFT JOIN LookupItem mda ON mda.Id = pt.MaternalDeathAudited