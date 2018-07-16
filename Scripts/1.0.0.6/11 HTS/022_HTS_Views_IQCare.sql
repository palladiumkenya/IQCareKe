IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[HTS_LAB_Register]'))
	DROP VIEW [dbo].[HTS_LAB_Register]
GO
/****** Object:  View [dbo].[HTS_LAB_Register]    Script Date: 05/16/2018 11:52:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[HTS_LAB_Register]
AS
SELECT DISTINCT ISNULL(ROW_NUMBER() OVER (ORDER BY PE.Id ASC), - 1) AS RowID, P.Id PatientID, p.Ptn_pk AS PatientPK, CONVERT(varchar(50), decryptbykey(Per.firstname)) + ' ' + CONVERT(varchar(50), 
decryptbykey(Per.middlename)) + ' ' + CONVERT(varchar(50), decryptbykey(Per.lastname)) AS PatientName, PE.EncounterStartTime VisitDate, p.dateofbirth AS DOB, DATEdiff(yy, p.dateofbirth, PE.EncounterStartTime) AS Age, 
Gender =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = per.sex), ISNULL(CAST((CASE HE.EncounterType WHEN 1 THEN 'Initial Test' WHEN 2 THEN 'Repeat Test' END) AS VARCHAR(50)), 'Initial') AS TestType, clientSelfTestesd =
    (SELECT        TOP 1 CASE ItemName WHEN 'Yes' THEN 'Y' WHEN 'NO' THEN 'N' ELSE NULL END
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = he.EverSelfTested), StrategyHTS =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = he.TestEntryPoint), ClientTestedAs =
    (SELECT        TOP 1 CASE ItemName WHEN 'C: Couple (includes polygamous)' THEN 'Couple' ELSE 'Individual' END
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = he.TestedAs), CoupleDiscordant =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = he.CoupleDiscordant), TestedBefore =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = he.evertested), MonthsSinceLastTest WhenLastTested, MaritalStatus =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = ms.maritalstatusid), kits.onekitid AS TestKitName1, kits.onelotnumber AS TestKitLotNumber1, kits.oneexpirydate AS TestKitExpiryDate1, ResultOne =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = her.RoundOneTestResult), kits.twokitid AS TestKitName_2, kits.twolotnumber AS TestKitLotNumber_2, kits.twoexpirydate AS TestKitExpiryDate_2, CASE WHEN dis.itemname IS NULL 
THEN 'NA' ELSE dis.itemname END AS Disability, kits.FinalTestOneResult, kits.FinalTestTwoResult AS FinalResultTestTwo, ResultTwo =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = her.RoundTwoTestResult), finalResultHTS =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = her.FinalResult), FinalResultsGiven =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = he.FinalResultGiven), /*Disability =  (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = dis.disabilityid),*/ Consent =
    (SELECT        TOP 1 CASE ItemName WHEN 'Yes' THEN 1 ELSE 0 END
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId =
                                    (SELECT        TOP 1 ConsentValue
                                      FROM            PatientConsent PC
                                      WHERE        PC.PatientMasterVisitId = PM.Id AND PC.ConsentType =
                                                                    (SELECT        TOP 1 ItemId
                                                                      FROM            LookupItemView
                                                                      WHERE        ItemName = 'ConsentToBeTested'))), he.EncounterRemarks AS Remarks, NULL AS TCAHTS, NULL AS TBScreeningHTS, 
CASE pop.PopulationCategory WHEN 'General Population' THEN 'N/A' ELSE PopulationCategory END AS KeyPop
FROM            [dbo].[PatientEncounter] PE INNER JOIN
                         patient p ON p.id = pe.patientid INNER JOIN
                         personview per ON per.id = p.personid LEFT JOIN
                         [dbo].[PatientPopulationView] pop ON pop.PatientPK = p.ptn_pk INNER JOIN
                         [dbo].[PatientMasterVisit] PM ON PM.Id = PE.PatientMasterVisitId INNER JOIN
                         [dbo].[HtsEncounter] HE ON PE.Id = HE.PatientEncounterID INNER JOIN
                         [dbo].[HtsEncounterResult] HER ON HtsEncounterId = HE.Id LEFT JOIN
                         [PatientMaritalStatus] ms ON ms.personid = p.personid LEFT JOIN
                             (SELECT        TOP 1 personid, l.itemname
                               FROM            [dbo].[ClientDisability] d, [dbo].[LookupItemView] l
                               WHERE        l.itemid = d .disabilityid) dis ON dis.personid = p.personid LEFT JOIN
                             (SELECT DISTINCT 
                                                         e.personid, one.kitid AS onekitid, one.kitlotnumber AS onelotnumber, one.Outcome AS FinalTestOneResult, two.Outcome AS FinalTestTwoResult, one.expirydate AS oneexpirydate, two.kitid AS twokitid, 
                                                         two.kitlotnumber AS twolotnumber, two.expirydate AS twoexpirydate
                               FROM            [Testing] t INNER JOIN
                                                         [HtsEncounter] e ON t .htsencounterid = e.id FULL OUTER JOIN
                                                             (SELECT distinct  htsencounterid, b.ItemName kitid, kitlotnumber, expirydate, PersonId, l.ItemName AS outcome
																FROM [Testing] t inner join  [HtsEncounter] e on t.HtsEncounterId=e.id inner join  [LookupItemView] l on l.itemid=t.Outcome
																inner join lookupitemview b on b.itemid=t.KitId WHERE  e.encountertype = 1 and t.testround =1) one ON one.personid = e.PersonId FULL OUTER JOIN
                                                             (SELECT distinct htsencounterid, b.ItemName kitid, kitlotnumber, expirydate, PersonId, l.ItemName AS outcome
																FROM  [Testing] t inner join  [HtsEncounter] e on t.HtsEncounterId=e.id inner join  [LookupItemView] l on l.itemid=t.Outcome
																inner join lookupitemview b on b.itemid=t.KitId WHERE  e.encountertype = 2) two ON two.personid = e.PersonId) kits ON kits.personid = p.personid



GO


IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_HIVTesting_Services_Referral_Linkage_Register]'))
	DROP VIEW [dbo].[vw_HIVTesting_Services_Referral_Linkage_Register]
GO

/****** Object:  View [dbo].[vw_HIVTesting_Services_Referral_Linkage_Register]    Script Date: 05/16/2018 12:10:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_HIVTesting_Services_Referral_Linkage_Register]
AS
SELECT DISTINCT ISNULL(ROW_NUMBER() OVER (ORDER BY PE.Id ASC), - 1) AS RowID, he.id, firstname + ' ' + MiddleName + ' ' + lastname AS PatientName, PE.PatientId, p.ptn_pk AS Ptn_pk, p.dateofbirth, DATEdiff(yy, p.dateofbirth, 
PE.EncounterStartTime) AS Age, Gender =
    (SELECT DISTINCT ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = per.sex), PE.EncounterStartTime Date, ploc.LandMark AS landmark, pcon.MobileNumber AS PhoneNumber, link.Facility AS FacilityName, NULL AS Occupation, NULL AS IndexClientType, 
CASE pop.PopulationCategory WHEN 'General Population' THEN 'NA' ELSE PopulationCategory END AS KeyPop, refer.referralDate AS ReferalDate, link.healthworker AS handedOverTo, link.cadre AS handedOverToCadre, 
tout.datetracingdone AS [TracingDate], tout.TraceType AS tracingtype, pcons.itemname AS ConsentValue, tout.Outcome, tout.Remarks AS Remarks, link.linkagedate AS dateEnrolled, link.cccnumber AS CCCNumber
FROM            [dbo].[PatientEncounter] PE INNER JOIN
                         patient p ON p.id = pe.patientid INNER JOIN
                         PersonView per ON per.id = p.personid LEFT JOIN
                         PersonLocation ploc ON ploc.personid = per.id LEFT JOIN
                         PersonContact pcon ON pcon.personid = per.id LEFT JOIN
                         PatientPopulationView pop ON pop.PatientPK = p.ptn_pk INNER JOIN
                         [dbo].[PatientMasterVisit] PM ON PM.Id = PE.PatientMasterVisitId LEFT JOIN
                             (SELECT DISTINCT l.itemname, PatientMasterVisitId
                               FROM            PatientConsent t, [dbo].[LookupItemView] l
                               WHERE        l.itemid = t .ConsentValue) pcons ON pcons.PatientMasterVisitId = pm.id INNER JOIN
                         [dbo].[HtsEncounter] HE ON PE.Id = HE.PatientEncounterID inner JOIN
                             (SELECT DISTINCT PersonId, PatientId, cast(LinkageDate AS date) LinkageDate, CCCNumber, Facility, Enrolled, HealthWorker, Cadre
                               FROM            [dbo].[PatientLinkage]) link ON link.personid = per.id INNER JOIN
                         [dbo].[HtsEncounterResult] HER ON HtsEncounterId = HE.Id LEFT JOIN
                             (SELECT DISTINCT personid, datetracingdone, l.itemname TraceType, j.itemname Outcome, Remarks
                               FROM            [dbo].[Tracing] t LEFT JOIN
                                                         [dbo].[LookupItemView] l ON l.itemid = t .mode LEFT JOIN
                                                         [dbo].[LookupItemView] j ON j.itemid = t .outcome) tout ON tout.PersonID = per.id LEFT JOIN
                             (SELECT DISTINCT he.personid, cast(referralDate AS Date) referralDate
                               FROM            [dbo].[HtsEncounterResult] her, [HtsEncounter] he, [LookupItemView] look, Referral ref
                               WHERE        her.HtsEncounterId = he.Id AND he.PersonId = ref.PersonId AND her.FinalResult = look.ItemId AND ItemName = 'Positive') refer ON refer.personid = per.id

GO

