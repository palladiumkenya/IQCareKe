ALTER VIEW [dbo].[DeliveredBabyBirthInfoView] AS
SELECT dbi.Id, dbi.DeliveryId AS PatientDeliveryInformationId,dbi.PatientMasterVisitId
,dbi.BirthWeight,dbi.ResuscitationDone,dbi.BirthDeformity,dbi.TeoGiven, dbi.BirthNotificationNumber,dbi.BirthComments AS Comment, 
dbi.CreateDate AS DateCreated,dbi.BreastFedWithinHr AS BreastFedWithinHour, slki.DisplayName AS Sex, olki.DisplayName AS DeliveryOutcome, 
 dbi.CreatedBy, dbi.Deleteflag AS DeleteFlag
 FROM DeliveredBabyBirthInformation AS dbi
 INNER JOIN LookupItemView slki ON slki.ItemId = dbi.Sex
 INNER JOIN LookupItemView olki ON olki.ItemId = dbi.DeliveryOutcome