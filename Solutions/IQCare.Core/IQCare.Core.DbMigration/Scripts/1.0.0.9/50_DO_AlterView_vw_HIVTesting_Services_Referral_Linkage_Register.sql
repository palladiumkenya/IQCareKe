IF OBJECT_ID('dbo.vw_HIVTesting_Services_Referral_Linkage_Register', 'V') IS NOT NULL
    DROP VIEW dbo.vw_HIVTesting_Services_Referral_Linkage_Register
GO
CREATE VIEW [dbo].[vw_HIVTesting_Services_Referral_Linkage_Register]
AS
SELECT DISTINCT ISNULL(ROW_NUMBER() OVER (ORDER BY PE.Id ASC), - 1) AS RowID, he.id, p.FacilityID FacilityCode,firstname + ' ' + MiddleName + ' ' + lastname AS PatientName, PE.PatientId, p.ptn_pk AS Ptn_pk, p.dateofbirth, DATEdiff(yy, p.dateofbirth, 
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
