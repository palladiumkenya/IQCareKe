ALTER VIEW [dbo].[Api_PatientCareEndingView]
AS
SELECT        format(cast(dbo.PatientCareending.DateOfDeath as date),'yyyyMMdd') DateOfDeath, dbo.PatientCareending.PatientId, dbo.PatientCareending.CareEndingNotes as DeathIndicator
FROM            dbo.PatientCareending INNER JOIN
                         dbo.LookupItemView ON dbo.PatientCareending.ExitReason = dbo.LookupItemView.ItemId
WHERE        (dbo.PatientCareending.DeleteFlag = 0) AND (dbo.LookupItemView.MasterName = 'CareEnded') AND (dbo.LookupItemView.ItemName = 'Death')



