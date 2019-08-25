ALTER VIEW [dbo].[VW_PatientCareEnding]
AS
     SELECT dbo.Patient.ptn_pk,
            dbo.PatientMasterVisit.VisitDate,
            dbo.PatientCareending.ExitReason,
            dbo.LookupItemView.ItemName AS [Patient CareEnd Reason],
            dbo.PatientCareending.TransferOutfacility AS LPTFTransfer,
            dbo.PatientCareending.DateOfDeath,
            dbo.PatientCareending.ExitDate AS CareEndedDate,
            dbo.PatientCareending.Id AS CareEndedID,
            dbo.PatientCareending.CareEndingNotes,
            dbo.PatientCareending.Active,
            dbo.PatientCareending.DeleteFlag
     FROM dbo.Patient
		  inner join [dbo].[PatientEnrollment] on PatientEnrollment.PatientId=dbo.Patient.Id
          INNER JOIN dbo.PatientCareending ON dbo.Patient.Id = dbo.PatientCareending.PatientId
          INNER JOIN dbo.PatientMasterVisit ON dbo.PatientCareending.PatientMasterVisitId = dbo.PatientMasterVisit.Id
          INNER JOIN dbo.LookupItemView ON dbo.PatientCareending.ExitReason = dbo.LookupItemView.ItemId
		  where PatientEnrollment.CareEnded=1



