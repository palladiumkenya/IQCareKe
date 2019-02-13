IF OBJECT_ID('dbo.PNC Register', 'V') IS NOT NULL
    DROP VIEW [dbo].[PNC Register]
GO
CREATE VIEW [dbo].[PNC Register]
AS
SELECT        a.Ptn_Pk, b.Id, b.FacilityId, c.IdentifierValue AS [PNC Register Number], a.FirstName, a.MiddleName, a.LastName, a.VillageName, a.Phone, a.Landmark, a.DOB, a.Sex, g.VisitType, h.Name AS ServiceArea, g.VisitNumber, 
                         diag.Diagnosis, g.[DaysPostPartum],k.Temperature, k.Weight, k.Height, k.BPSystolic, k.BPDiastolic, k.Muac, delivery.DateOfDelivery, delivery.ModeOfDelivery, baby.Sex AS BabyGender, baby.BirthWeight, baby.DeliveryOutcome, 
                         baby.BreastFedWithinHr, baby.TeoGiven, baby.BirthDeformity,  g.TreatedForSyphilis, HIVTest.OneKitId, HIVTest.OneLotNumber, HIVTest.OneExpiryDate, 
                         HIVTest.FinalTestOneResult, HIVTest.twokitid, HIVTest.twolotnumber, HIVTest.twoexpirydate, HIVTest.FinalTestTwoResult, z.FinalResult, baby.BirthNotificationNumber, outc.[DateDischarged], outc.OutcomeStatus, 
                         ref.ReferredFrom, ref.ReferredTo, notes.ClinicalNotes
FROM            dbo.mst_Patient AS a INNER JOIN
                         dbo.Patient AS b ON a.Ptn_Pk = b.ptn_pk INNER JOIN
                         dbo.PatientIdentifier AS c ON b.Id = c.PatientId INNER JOIN
                         dbo.PatientMasterVisit AS d ON b.Id = d.PatientId INNER JOIN
                         dbo.PatientEncounter AS e ON b.Id = e.PatientId AND d.Id = e.PatientMasterVisitId INNER JOIN
                         dbo.PatientEnrollment AS f ON c.PatientEnrollmentId = f.Id INNER JOIN
                         dbo.PatientProfile AS g ON b.Id = g.PatientId AND d.Id = g.PatientMasterVisitId INNER JOIN
                         dbo.ServiceArea AS h ON f.ServiceAreaId = h.Id INNER JOIN
                         dbo.Pregnancy AS I ON b.Id = I.PatientId AND d.Id = I.PatientMasterVisitId AND g.PregnancyId = I.Id LEFT OUTER JOIN
                         dbo.PatientDrugAdministration AS j ON b.Id = j.PatientId AND d.Id = j.PatientMasterVisitId LEFT OUTER JOIN
                         dbo.PatientDiagnosis AS diag ON diag.PatientMasterVisitId = d.Id LEFT OUTER JOIN
                         dbo.LookupItemView AS lkup1 ON lkup1.ItemId = j.Value LEFT OUTER JOIN
                         dbo.PatientVitals AS k ON d.Id = k.PatientMasterVisitId AND b.Id = k.PatientId LEFT OUTER JOIN
                         dbo.LookupItemView AS lkup2 ON lkup2.ItemId = j.Value LEFT OUTER JOIN
                         dbo.PatientDelivery AS delivery ON delivery.PatientMasterVisitID = d.Id LEFT OUTER JOIN
                         dbo.PatientOutcome AS outc ON outc.PatientMasterVisitID = d.Id LEFT OUTER JOIN
                         dbo.LookupItemView AS lkup3 ON lkup3.ItemId = j.Value LEFT OUTER JOIN
                         dbo.DeliveredBabyBirthInformation AS baby ON baby.PatientMasterVisitId = d.Id LEFT OUTER JOIN
                         dbo.PMTCTReferral AS ref ON ref.PatientMasterVisitId = d.Id LEFT OUTER JOIN
                         dbo.PatientClinicalNotes AS notes ON notes.PatientMasterVisitId = d.Id LEFT OUTER JOIN
                             (SELECT DISTINCT 
                                                         e.PersonId, one.kitid AS OneKitId, one.KitLotNumber AS OneLotNumber, one.outcome AS FinalTestOneResult, two.outcome AS FinalTestTwoResult, one.ExpiryDate AS OneExpiryDate, two.kitid AS twokitid, 
                                                         two.KitLotNumber AS twolotnumber, two.ExpiryDate AS twoexpirydate
                               FROM            dbo.Testing AS t INNER JOIN
                                                         dbo.HtsEncounter AS e ON t.HtsEncounterId = e.Id FULL OUTER JOIN
                                                             (SELECT DISTINCT t.HtsEncounterId, b.ItemName AS kitid, t.KitLotNumber, t.ExpiryDate, e.PersonId, l.ItemName AS outcome
                                                               FROM            dbo.Testing AS t INNER JOIN
                                                                                         dbo.HtsEncounter AS e ON t.HtsEncounterId = e.Id INNER JOIN
                                                                                         dbo.LookupItemView AS l ON l.ItemId = t.Outcome INNER JOIN
                                                                                         dbo.LookupItemView AS b ON b.ItemId = t.KitId INNER JOIN
                                                                                         dbo.PatientEncounter AS pe ON pe.Id = e.PatientEncounterID LEFT OUTER JOIN
                                                                                         dbo.LookupItemView AS lk ON lk.ItemId = pe.EncounterTypeId
                                                               WHERE        (t.TestRound = 1) AND (lk.ItemName = 'pnc-encounter')) AS one ON one.PersonId = e.PersonId FULL OUTER JOIN
                                                             (SELECT DISTINCT t.HtsEncounterId, b.ItemName AS kitid, t.KitLotNumber, t.ExpiryDate, e.PersonId, l.ItemName AS outcome
                                                               FROM            dbo.Testing AS t INNER JOIN
                                                                                         dbo.HtsEncounter AS e ON t.HtsEncounterId = e.Id INNER JOIN
                                                                                         dbo.LookupItemView AS l ON l.ItemId = t.Outcome INNER JOIN
                                                                                         dbo.LookupItemView AS b ON b.ItemId = t.KitId INNER JOIN
                                                                                         dbo.PatientEncounter AS pe ON pe.Id = e.PatientEncounterID LEFT OUTER JOIN
                                                                                         dbo.LookupItemView AS lk ON lk.ItemId = pe.EncounterTypeId
                                                               WHERE        (t.TestRound = 2) AND (lk.ItemName = 'pnc-encounter')) AS two ON two.PersonId = e.PersonId) AS HIVTest ON HIVTest.PersonId = b.PersonId LEFT OUTER JOIN
                             (SELECT        he.PersonId, he.PatientEncounterID, lk.ItemName AS FinalResult
                               FROM            dbo.HtsEncounter AS he INNER JOIN
                                                         dbo.HtsEncounterResult AS her ON he.Id = her.HtsEncounterId INNER JOIN
                                                         dbo.PatientEncounter AS pe ON pe.Id = he.PatientEncounterID LEFT OUTER JOIN
                                                         dbo.LookupItemView AS lk1 ON lk1.ItemId = pe.EncounterTypeId LEFT OUTER JOIN
                                                         dbo.LookupItemView AS lk ON lk.ItemId = her.FinalResult
                               WHERE        (lk1.ItemName = 'pnc-encounter')) AS z ON z.PersonId = b.PersonId
WHERE        (h.Name = 'PNC')

GO
