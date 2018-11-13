IF OBJECT_ID('dbo.HEI Register', 'V') IS NOT NULL
    DROP VIEW [dbo].[HEI Register]
GO
CREATE VIEW [dbo].[HEI Register]
AS
SELECT        pe.EnrollmentDate, pid.IdentifierValue AS HEIID, p.Id AS PatientID, p.ptn_pk,p.FacilityId FacilityCode,
                             (SELECT        Name
                               FROM            dbo.LookupItem
                               WHERE        (Id = sep.EntryPointId)) AS EntryPoint,
                             (SELECT        Name
                               FROM            dbo.LookupItem AS LookupItem_2
                               WHERE        (Id = he.PrimaryCareGiverID)) AS PrimaryCareGiver, he.MotherPersonId, he.MotherName, NULL AS MotherPhone, NULL AS FacilityEnrolled, he.MotherCCCNumber,
                             (SELECT        Name
                               FROM            dbo.LookupItem AS LookupItem_1
                               WHERE        (Id = he.MotherPMTCTDrugsId)) AS PMTCTARVSMother, NULL AS PMTCTProphylaxisInfant, DATEDIFF(ww, mp.DOB, pl6.OrderedByDate) AS FirstPCR_AgeInWeeks, pl6.OrderedByDate AS FirstPCR_SampleTaken, 
                         pl6.ReportedByDate AS FirstPCR_ResultsReceived, pl6.ReportedByDate AS FirstPCR_ResultsCollected, 
                         CASE WHEN pl6.TestResults1 = '0.00' THEN 'Negative' WHEN pl6.TestResults1 = '1.00' THEN 'Positive' ELSE pl6.TestResults1 END AS FirstPCR_Results, pl6repeat.OrderedByDate AS RepeatFirstPCR_SampleTaken, 
                         pl6repeat.ReportedByDate AS RepeatFirstPCR_ResultsReceived, pl6repeat.ReportedByDate AS RepeatFirstPCR_ResultsCollected, 
                         CASE WHEN pl6repeat.TestResults1 = '0.00' THEN 'Negative' WHEN pl6repeat.TestResults1 = '1.00' THEN 'Positive' ELSE pl6repeat.TestResults1 END AS RepeatFirstPCR_Results, 
                         feedlook68Wks.ItemName AS Feeding_6_8Weeks, CASE WHEN pharm8nvp.ptn_pharmacy_pk IS NOT NULL THEN 'Yes' ELSE 'No' END AS GivenNVP_6_8Weeks, CASE WHEN pharm8ctx.ptn_pharmacy_pk IS NOT NULL 
                         THEN 'Yes' ELSE 'No' END AS GivenCTX_6_8Weeks, feedlook10Wks.ItemName AS Feeding_10Weeks, CASE WHEN pharm10nvp.ptn_pharmacy_pk IS NOT NULL THEN 'Yes' ELSE 'No' END AS GivenNVP_10Weeks, 
                         CASE WHEN pharm10ctx.ptn_pharmacy_pk IS NOT NULL THEN 'Yes' ELSE 'No' END AS GivenCTX_10Weeks, feedlook14Wks.ItemName AS Feeding_14Weeks, CASE WHEN pharm14nvp.ptn_pharmacy_pk IS NOT NULL 
                         THEN 'Yes' ELSE 'No' END AS GivenNVP_14Weeks, CASE WHEN pharm14ctx.ptn_pharmacy_pk IS NOT NULL THEN 'Yes' ELSE 'No' END AS GivenCTX_14Weeks, feedlook6Mons.ItemName AS Feeding_6_Months, 
                         CASE WHEN pharm6Monsnvp.ptn_pharmacy_pk IS NOT NULL THEN 'Yes' ELSE 'No' END AS GivenNVP_6Months, CASE WHEN pharm6Monsctx.ptn_pharmacy_pk IS NOT NULL 
                         THEN 'Yes' ELSE 'No' END AS GivenCTX_6Months, DATEDIFF(mm, p.DateOfBirth, pl6Mons.OrderedByDate) AS Months_6PCR_AgeInMonths, pl6Mons.OrderedByDate AS SecondPCR_SampleTaken, 
                         pl6Mons.ReportedByDate AS SecondPCR_ResultsReceived, pl6Mons.ReportedByDate AS SecondPCR_ResultsCollected, 
                         CASE WHEN pl6Mons.TestResults1 = '0.00' THEN 'Negative' WHEN pl6Mons.TestResults1 = '1.00' THEN 'Positive' ELSE pl6Mons.TestResults1 END AS SecondPCR_Results, 
                         pl6Monsrepeat.OrderedByDate AS RepeatSecondPCR_SampleTaken, pl6Monsrepeat.ReportedByDate AS RepeatSecondPCR_ResultsReceived, pl6Monsrepeat.ReportedByDate AS RepeatSecondPCR_ResultsCollected, 
                         CASE WHEN pl6Monsrepeat.TestResults1 = '0.00' THEN 'Negative' WHEN pl6Monsrepeat.TestResults1 = '1.00' THEN 'Positive' ELSE pl6Monsrepeat.TestResults1 END AS RepeatSecondPCR_Results, 
                         feedlook9Mons.ItemName AS Feeding_9_Months, CASE WHEN pharm9Monsnvp.ptn_pharmacy_pk IS NOT NULL THEN 'Yes' ELSE 'No' END AS GivenNVP_9Months, CASE WHEN pharm9Monsctx.ptn_pharmacy_pk IS NOT NULL 
                         THEN 'Yes' ELSE 'No' END AS GivenCTX_9Months, feedlook12Mons.ItemName AS Feeding_12_Months, CASE WHEN pharm12Monsnvp.ptn_pharmacy_pk IS NOT NULL THEN 'Yes' ELSE 'No' END AS GivenNVP_12Months, 
                         CASE WHEN pharm12Monsctx.ptn_pharmacy_pk IS NOT NULL THEN 'Yes' ELSE 'No' END AS GivenCTX_12Months, DATEDIFF(mm, p.DateOfBirth, pl12Mons.OrderedByDate) AS Months_12PCR_AgeInMonths, 
                         pl12Mons.OrderedByDate AS ThirdPCR_SampleTaken, pl12Mons.ReportedByDate AS ThirdPCR_ResultsReceived, pl12Mons.ReportedByDate AS ThirdPCR_ResultsCollected, 
                         CASE WHEN pl12Mons.TestResults1 = '0.00' THEN 'Negative' WHEN pl12Mons.TestResults1 = '1.00' THEN 'Positive' ELSE pl12Mons.TestResults1 END AS ThirdPCR_Results, 
                         pl12Monsrepeat.OrderedByDate AS RepeatThirdPCR_SampleTaken, pl12Monsrepeat.ReportedByDate AS RepeatThirdPCR_ResultsReceived, pl12Monsrepeat.ReportedByDate AS RepeatThirdPCR_ResultsCollected, 
                         CASE WHEN pl12Monsrepeat.TestResults1 = '0.00' THEN 'Negative' WHEN pl12Monsrepeat.TestResults1 = '1.00' THEN 'Positive' ELSE pl12Monsrepeat.TestResults1 END AS RepeatThirdPCR_Results, 
                         feedlook15Mons.ItemName AS Feeding_15_Months, CASE WHEN pharm15Monsnvp.ptn_pharmacy_pk IS NOT NULL THEN 'Yes' ELSE 'No' END AS GivenNVP_15Months, 
                         CASE WHEN pharm15Monsctx.ptn_pharmacy_pk IS NOT NULL THEN 'Yes' ELSE 'No' END AS GivenCTX_15Months, feedlookConfirm.ItemName AS Feeding_Confirm_Months, 
                         CASE WHEN pharmConfirmnvp.ptn_pharmacy_pk IS NOT NULL THEN 'Yes' ELSE 'No' END AS GivenNVP_Confirm, CASE WHEN pharmConfirmctx.ptn_pharmacy_pk IS NOT NULL 
                         THEN 'Yes' ELSE 'No' END AS GivenCTX_Confirm, DATEDIFF(mm, p.DateOfBirth, plConfirm.OrderedByDate) AS Months_ConfirmPCR_AgeInMonths, plConfirm.OrderedByDate AS ConfirmPCR_SampleTaken, 
                         plConfirm.ReportedByDate AS ConfirmPCR_ResultsReceived, plConfirm.ReportedByDate AS ConfirmPCR_ResultsCollected, 
                         CASE WHEN plConfirm.TestResults1 = '0.00' THEN 'Negative' WHEN plConfirm.TestResults1 = '1.00' THEN 'Positive' ELSE plConfirm.TestResults1 END AS ConfirmPCR_Results, 
                         plConfirmrepeat.OrderedByDate AS RepeatConfirmPCR_SampleTaken, plConfirmrepeat.ReportedByDate AS RepeatConfirmPCR_ResultsReceived, plConfirmrepeat.ReportedByDate AS RepeatConfirmPCR_ResultsCollected, 
                         CASE WHEN plConfirmrepeat.TestResults1 = '0.00' THEN 'Negative' WHEN plConfirmrepeat.TestResults1 = '1.00' THEN 'Positive' ELSE plConfirmrepeat.TestResults1 END AS RepeatConfirmPCR_Results, 
                         feedlook1824mnths.ItemName AS Feeding_1824_Months, CASE WHEN pharm1824mnthsnvp.ptn_pharmacy_pk IS NOT NULL THEN 'Yes' ELSE 'No' END AS GivenNVP_1824mnths, 
                         CASE WHEN pharm1824mnthsctx.ptn_pharmacy_pk IS NOT NULL THEN 'Yes' ELSE 'No' END AS GivenCTX_1824mnths, DATEDIFF(mm, p.DateOfBirth, plConfirm.OrderedByDate) AS Months_Antibody1824_AgeInMonths, 
                         CASE WHEN pl1824mnths.TestResults1 = '0.00' THEN 'Negative' WHEN pl1824mnths.TestResults1 = '1.00' THEN 'Positive' ELSE pl1824mnths.TestResults1 END AS Antibody1824mnths_Results, he.Outcome24MonthId, 
                         NULL StatusOfPair24Mnths, pm.Comment
FROM            dbo.PatientMasterVisit AS v INNER JOIN
                         dbo.Patient AS p ON v.PatientId = p.Id INNER JOIN
                         dbo.mst_Patient AS mp ON mp.Ptn_Pk = p.ptn_pk INNER JOIN
                         dbo.ServiceEntryPoint AS sep ON sep.PatientId = p.Id AND sep.EntryPointId IN
                             (SELECT        ItemId
                               FROM            dbo.LookupItemView
                               WHERE        (ItemId = sep.EntryPointId) AND (MasterId =
                                                             (SELECT        TOP (1) Id
                                                               FROM            dbo.LookupMaster
                                                               WHERE        (Name LIKE 'heie%')))) INNER JOIN
                         dbo.PatientEnrollment AS pe ON p.Id = pe.PatientId INNER JOIN
                         dbo.PatientIdentifier AS pid ON p.Id = pid.PatientId INNER JOIN
                         dbo.HEIEncounter AS he ON v.Id = he.PatientMasterVisitId left outer join 
						 [dbo].[PatientMilestone] pm on pm.[PatientMasterVisitId] =v.id and pm.[PatientId]=p.Id  LEFT OUTER JOIN
                         dbo.HEIFeeding AS hf68Wks ON v.Id = hf68Wks.PatientMasterVisitId AND DATEDIFF(ww, p.DateOfBirth, v.VisitDate) BETWEEN 6 AND 8 LEFT OUTER JOIN
                         dbo.LookupItemView AS feedlook68Wks ON hf68Wks.FeedingModeId = feedlook68Wks.ItemId LEFT OUTER JOIN
                         dbo.HEIFeeding AS hf10Wks ON v.Id = hf10Wks.PatientMasterVisitId AND DATEDIFF(ww, p.DateOfBirth, v.VisitDate) = 10 LEFT OUTER JOIN
                         dbo.LookupItemView AS feedlook10Wks ON hf10Wks.FeedingModeId = feedlook10Wks.ItemId LEFT OUTER JOIN
                         dbo.HEIFeeding AS hf14Wks ON v.Id = hf14Wks.PatientMasterVisitId AND DATEDIFF(ww, p.DateOfBirth, v.VisitDate) = 14 LEFT OUTER JOIN
                         dbo.LookupItemView AS feedlook14Wks ON hf14Wks.FeedingModeId = feedlook14Wks.ItemId LEFT OUTER JOIN
                         dbo.HEIFeeding AS hf6Mons ON v.Id = hf6Mons.PatientMasterVisitId AND DATEDIFF(mm, p.DateOfBirth, v.VisitDate) = 6 LEFT OUTER JOIN
                         dbo.LookupItemView AS feedlook6Mons ON hf6Mons.FeedingModeId = feedlook6Mons.ItemId LEFT OUTER JOIN
                         dbo.HEIFeeding AS hf9Mons ON v.Id = hf9Mons.PatientMasterVisitId AND DATEDIFF(mm, p.DateOfBirth, v.VisitDate) = 9 LEFT OUTER JOIN
                         dbo.LookupItemView AS feedlook9Mons ON hf9Mons.FeedingModeId = feedlook9Mons.ItemId LEFT OUTER JOIN
                         dbo.HEIFeeding AS hf12Mons ON v.Id = hf12Mons.PatientMasterVisitId AND DATEDIFF(mm, p.DateOfBirth, v.VisitDate) = 12 LEFT OUTER JOIN
                         dbo.LookupItemView AS feedlook12Mons ON hf12Mons.FeedingModeId = feedlook12Mons.ItemId LEFT OUTER JOIN
                         dbo.HEIFeeding AS hf15Mons ON v.Id = hf15Mons.PatientMasterVisitId AND DATEDIFF(mm, p.DateOfBirth, v.VisitDate) = 15 LEFT OUTER JOIN
                         dbo.LookupItemView AS feedlook15Mons ON hf15Mons.FeedingModeId = feedlook15Mons.ItemId LEFT OUTER JOIN
                         dbo.HEIFeeding AS hfConfirm ON v.Id = hfConfirm.PatientMasterVisitId AND DATEDIFF(mm, p.DateOfBirth, v.VisitDate) > 15 LEFT OUTER JOIN
                         dbo.LookupItemView AS feedlookConfirm ON hfConfirm.FeedingModeId = feedlookConfirm.ItemId LEFT OUTER JOIN
                         dbo.HEIFeeding AS hf1824mnths ON v.Id = hf1824mnths.PatientMasterVisitId AND DATEDIFF(mm, p.DateOfBirth, v.VisitDate) BETWEEN 18 AND 24 LEFT OUTER JOIN
                         dbo.LookupItemView AS feedlook1824mnths ON hf1824mnths.FeedingModeId = feedlook1824mnths.ItemId LEFT OUTER JOIN
                         dbo.VW_PatientLaboratory AS pl6 ON pl6.Ptn_Pk = p.ptn_pk AND pl6.TestName LIKE '%PCR%' AND DATEDIFF(ww, p.DateOfBirth, pl6.OrderedByDate) < 7 LEFT OUTER JOIN
                         dbo.VW_PatientLaboratory AS pl6repeat ON pl6repeat.Ptn_Pk = p.ptn_pk AND pl6repeat.TestName LIKE '%PCR%' AND DATEDIFF(ww, p.DateOfBirth, pl6repeat.OrderedByDate) < 7 AND pl6repeat.OrderedByDate >
                             (SELECT        TOP (1) y.OrderedByDate
                               FROM            dbo.VW_PatientLaboratory AS y INNER JOIN
                                                         dbo.mst_Patient AS a ON a.Ptn_Pk = y.Ptn_Pk
                               WHERE        (y.TestName LIKE '%PCR%') AND (DATEDIFF(ww, a.DOB, y.OrderedByDate) < 7)) LEFT OUTER JOIN
                         dbo.VW_PatientLaboratory AS pl6Mons ON pl6Mons.Ptn_Pk = p.ptn_pk AND pl6Mons.TestName LIKE '%PCR%' AND DATEDIFF(mm, p.DateOfBirth, pl6Mons.OrderedByDate) < 7 LEFT OUTER JOIN
                         dbo.VW_PatientLaboratory AS pl6Monsrepeat ON pl6Monsrepeat.Ptn_Pk = p.ptn_pk AND pl6Monsrepeat.TestName LIKE '%PCR%' AND DATEDIFF(ww, p.DateOfBirth, pl6Monsrepeat.OrderedByDate) < 7 AND 
                         pl6Monsrepeat.OrderedByDate >
                             (SELECT        TOP (1) y.OrderedByDate
                               FROM            dbo.VW_PatientLaboratory AS y INNER JOIN
                                                         dbo.mst_Patient AS a ON a.Ptn_Pk = y.Ptn_Pk
                               WHERE        (y.TestName LIKE '%PCR%') AND (DATEDIFF(ww, a.DOB, y.OrderedByDate) < 7)) LEFT OUTER JOIN
                         dbo.VW_PatientLaboratory AS pl12Mons ON pl12Mons.Ptn_Pk = p.ptn_pk AND pl12Mons.TestName LIKE '%PCR%' AND DATEDIFF(mm, p.DateOfBirth, pl12Mons.OrderedByDate) BETWEEN 7 AND 12 LEFT OUTER JOIN
                         dbo.VW_PatientLaboratory AS pl12Monsrepeat ON pl12Monsrepeat.Ptn_Pk = p.ptn_pk AND pl12Monsrepeat.TestName LIKE '%PCR%' AND DATEDIFF(ww, p.DateOfBirth, pl12Monsrepeat.OrderedByDate) BETWEEN 7 AND 12 AND
                          pl12Monsrepeat.OrderedByDate >
                             (SELECT        TOP (1) y.OrderedByDate
                               FROM            dbo.VW_PatientLaboratory AS y INNER JOIN
                                                         dbo.mst_Patient AS a ON a.Ptn_Pk = y.Ptn_Pk
                               WHERE        (y.TestName LIKE '%PCR%') AND (DATEDIFF(ww, a.DOB, y.OrderedByDate) BETWEEN 7 AND 12)) LEFT OUTER JOIN
                         dbo.VW_PatientLaboratory AS plConfirm ON plConfirm.Ptn_Pk = p.ptn_pk AND plConfirm.TestName LIKE '%PCR%' AND DATEDIFF(mm, p.DateOfBirth, plConfirm.OrderedByDate) > 15 LEFT OUTER JOIN
                         dbo.VW_PatientLaboratory AS plConfirmrepeat ON plConfirmrepeat.Ptn_Pk = p.ptn_pk AND plConfirmrepeat.TestName LIKE '%PCR%' AND DATEDIFF(ww, p.DateOfBirth, plConfirmrepeat.OrderedByDate) > 15 AND 
                         plConfirmrepeat.OrderedByDate >
                             (SELECT        TOP (1) y.OrderedByDate
                               FROM            dbo.VW_PatientLaboratory AS y INNER JOIN
                                                         dbo.mst_Patient AS a ON a.Ptn_Pk = y.Ptn_Pk
                               WHERE        (y.TestName LIKE '%PCR%') AND (DATEDIFF(ww, a.DOB, y.OrderedByDate) > 15)) LEFT OUTER JOIN
                         dbo.VW_PatientLaboratory AS pl1824mnths ON pl1824mnths.Ptn_Pk = p.ptn_pk AND pl1824mnths.TestName LIKE '%ANT%' AND DATEDIFF(mm, p.DateOfBirth, pl1824mnths.OrderedByDate) > 15 LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharm8nvp ON pharm8nvp.Ptn_pk = p.ptn_pk AND pharm8nvp.DrugName LIKE '%nvp%' AND DATEADD(ww, 8, p.DateOfBirth) BETWEEN pharm8nvp.DispensedByDate AND DATEADD(dd, 
                         pharm8nvp.Duration, pharm8nvp.DispensedByDate) LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharm8ctx ON pharm8ctx.Ptn_pk = p.ptn_pk AND pharm8ctx.DrugName LIKE '%trimoxazole%' AND DATEADD(ww, 8, p.DateOfBirth) BETWEEN pharm8ctx.DispensedByDate AND DATEADD(dd, 
                         pharm8ctx.Duration, pharm8ctx.DispensedByDate) LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharm10nvp ON pharm10nvp.Ptn_pk = p.ptn_pk AND pharm10nvp.DrugName LIKE '%nvp%' AND DATEADD(ww, 10, p.DateOfBirth) BETWEEN pharm10nvp.DispensedByDate AND DATEADD(dd, 
                         pharm10nvp.Duration, pharm10nvp.DispensedByDate) LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharm10ctx ON pharm10ctx.Ptn_pk = p.ptn_pk AND pharm10ctx.DrugName LIKE '%trimoxazole%' AND DATEADD(ww, 10, p.DateOfBirth) BETWEEN pharm10ctx.DispensedByDate AND DATEADD(dd, 
                         pharm10ctx.Duration, pharm10ctx.DispensedByDate) LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharm14nvp ON pharm14nvp.Ptn_pk = p.ptn_pk AND pharm14nvp.DrugName LIKE '%nvp%' AND DATEADD(ww, 14, p.DateOfBirth) BETWEEN pharm14nvp.DispensedByDate AND DATEADD(dd, 
                         pharm14nvp.Duration, pharm14nvp.DispensedByDate) LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharm14ctx ON pharm14ctx.Ptn_pk = p.ptn_pk AND pharm14ctx.DrugName LIKE '%trimoxazole%' AND DATEADD(ww, 14, p.DateOfBirth) BETWEEN pharm14ctx.DispensedByDate AND DATEADD(dd, 
                         pharm14ctx.Duration, pharm14ctx.DispensedByDate) LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharm6Monsnvp ON pharm6Monsnvp.Ptn_pk = p.ptn_pk AND pharm6Monsnvp.DrugName LIKE '%nvp%' AND DATEADD(mm, 6, p.DateOfBirth) BETWEEN pharm6Monsnvp.DispensedByDate AND 
                         DATEADD(dd, pharm6Monsnvp.Duration, pharm6Monsnvp.DispensedByDate) LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharm6Monsctx ON pharm6Monsctx.Ptn_pk = p.ptn_pk AND pharm6Monsctx.DrugName LIKE '%trimoxazole%' AND DATEADD(mm, 6, p.DateOfBirth) BETWEEN 
                         pharm6Monsctx.DispensedByDate AND DATEADD(dd, pharm6Monsctx.Duration, pharm6Monsctx.DispensedByDate) LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharm9Monsnvp ON pharm9Monsnvp.Ptn_pk = p.ptn_pk AND pharm9Monsnvp.DrugName LIKE '%nvp%' AND DATEADD(mm, 9, p.DateOfBirth) BETWEEN pharm9Monsnvp.DispensedByDate AND 
                         DATEADD(dd, pharm9Monsnvp.Duration, pharm9Monsnvp.DispensedByDate) LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharm9Monsctx ON pharm9Monsctx.Ptn_pk = p.ptn_pk AND pharm9Monsctx.DrugName LIKE '%trimoxazole%' AND DATEADD(mm, 9, p.DateOfBirth) BETWEEN 
                         pharm9Monsctx.DispensedByDate AND DATEADD(dd, pharm9Monsctx.Duration, pharm9Monsctx.DispensedByDate) LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharm12Monsnvp ON pharm12Monsnvp.Ptn_pk = p.ptn_pk AND pharm12Monsnvp.DrugName LIKE '%nvp%' AND DATEADD(mm, 12, p.DateOfBirth) BETWEEN 
                         pharm12Monsnvp.DispensedByDate AND DATEADD(dd, pharm12Monsnvp.Duration, pharm12Monsnvp.DispensedByDate) LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharm12Monsctx ON pharm12Monsctx.Ptn_pk = p.ptn_pk AND pharm12Monsctx.DrugName LIKE '%trimoxazole%' AND DATEADD(mm, 12, p.DateOfBirth) BETWEEN 
                         pharm12Monsctx.DispensedByDate AND DATEADD(dd, pharm12Monsctx.Duration, pharm12Monsctx.DispensedByDate) LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharm15Monsnvp ON pharm15Monsnvp.Ptn_pk = p.ptn_pk AND pharm15Monsnvp.DrugName LIKE '%nvp%' AND DATEADD(mm, 15, p.DateOfBirth) BETWEEN 
                         pharm15Monsnvp.DispensedByDate AND DATEADD(dd, pharm15Monsnvp.Duration, pharm15Monsnvp.DispensedByDate) LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharm15Monsctx ON pharm15Monsctx.Ptn_pk = p.ptn_pk AND pharm15Monsctx.DrugName LIKE '%trimoxazole%' AND DATEADD(mm, 15, p.DateOfBirth) BETWEEN 
                         pharm15Monsctx.DispensedByDate AND DATEADD(dd, pharm15Monsctx.Duration, pharm15Monsctx.DispensedByDate) LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharmConfirmnvp ON pharmConfirmnvp.Ptn_pk = p.ptn_pk AND pharmConfirmnvp.DrugName LIKE '%nvp%' AND DATEADD(mm, 12, p.DateOfBirth) BETWEEN 
                         pharmConfirmnvp.DispensedByDate AND DATEADD(dd, pharmConfirmnvp.Duration, pharmConfirmnvp.DispensedByDate) LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharmConfirmctx ON pharmConfirmctx.Ptn_pk = p.ptn_pk AND pharmConfirmctx.DrugName LIKE '%trimoxazole%' AND DATEADD(mm, 12, p.DateOfBirth) BETWEEN 
                         pharmConfirmctx.DispensedByDate AND DATEADD(dd, pharmConfirmctx.Duration, pharmConfirmctx.DispensedByDate) LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharm1824mnthsnvp ON pharm1824mnthsnvp.Ptn_pk = p.ptn_pk AND pharmConfirmnvp.DrugName LIKE '%nvp%' AND DATEADD(mm, 12, p.DateOfBirth) BETWEEN 
                         pharm1824mnthsnvp.DispensedByDate AND DATEADD(dd, pharm1824mnthsnvp.Duration, pharm1824mnthsnvp.DispensedByDate) LEFT OUTER JOIN
                         dbo.VW_PatientPharmacy AS pharm1824mnthsctx ON pharm1824mnthsctx.Ptn_pk = p.ptn_pk AND pharmConfirmctx.DrugName LIKE '%trimoxazole%' AND DATEADD(mm, 12, p.DateOfBirth) BETWEEN 
                         pharm1824mnthsctx.DispensedByDate AND DATEADD(dd, pharm1824mnthsctx.Duration, pharm1824mnthsctx.DispensedByDate)



GO