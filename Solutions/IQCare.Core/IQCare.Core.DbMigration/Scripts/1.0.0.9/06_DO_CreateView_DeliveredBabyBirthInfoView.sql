CREATE VIEW DeliveredBabyBirthInfoView AS
SELECT dbi.BirthId AS Id, dbi.DeliveryId AS PatientDeliveryId,dbi.PatientMasterVisitId
,dbi.BirthWeight,dbi.ResuscitationDone,dbi.BirthDeformity,dbi.TeoGiven, dbi.BirthNotificationNumber,dbi.BirthComments AS Comment, 
dbi.CreateDate AS DateCreated,dbi.BreastFedWithinHr AS BreastFedWithinHour, slki.DisplayName AS Sex, olki.DisplayName AS DeliveryOutcome
 FROM DeliveredBabyBirthInformation AS dbi
 INNER JOIN LookupItemView slki ON slki.ItemId = dbi.Sex
 INNER JOIN LookupItemView olki ON olki.ItemId = dbi.DeliveryOutcome