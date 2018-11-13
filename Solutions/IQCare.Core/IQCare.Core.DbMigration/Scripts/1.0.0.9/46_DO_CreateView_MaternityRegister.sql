IF OBJECT_ID('dbo.Maternity Register', 'V') IS NOT NULL
    DROP VIEW [dbo].[Maternity Register]
GO
CREATE VIEW [dbo].[Maternity Register]
AS
SELECT        a.Ptn_Pk, b.Id, b.FacilityId, c.IdentifierValue AS [Admission Number], d.VisitDate AS [Date of Admission], a.FirstName, a.MiddleName, a.LastName, a.VillageName, a.Phone, a.Landmark, a.DOB, a.Sex, a.MaritalStatus, 
                         g.VisitType, h.Name AS ServiceArea, g.VisitNumber, I.Parity, I.Gravidae, I.LMP, I.EDD AS EDC, diag.Diagnosis, delivery.DurationOfLabour, delivery.DateOfDelivery, delivery.TimeOfDelivery, delivery.ModeOfDelivery, 
                         delivery.PlacentaComplete, delivery.BloodLosscapacity, delivery.MotherCondition AS [Condition after Delivery], delivery. [DeliveryComplicationsExperienced], baby.Sex AS babyGender, baby.BirthWeight, baby.DeliveryOutcome, baby.BreastFedWithinHr, 
                         baby.TeoGiven, baby.BirthDeformity, apgar.APGAR1min, apgar.APGAR5min, apgar.APGAR10min, g.TreatedForSyphilis, HIVTest.OneKitId, HIVTest.OneLotNumber, HIVTest.OneExpiryDate, HIVTest.FinalTestOneResult, 
                         HIVTest.twokitid, HIVTest.twolotnumber, HIVTest.twoexpirydate, HIVTest.FinalTestTwoResult, z.FinalResult, k.Weight, k.Height, k.BPSystolic, k.BPDiastolic, k.Muac, baby.BirthNotificationNumber, outc.[DateDischarged], 
                         outc.OutcomeStatus, ref.ReferredFrom, ref.ReferredTo, notes.ClinicalNotes
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
                         dbo.PatientClinicalNotes AS notes ON notes.PatientMasterVisitId = d.Id Left join 
						 (select a.Id,a.[DeliveredBabyBirthInformationId],APGAR1min,APGAR5min,APGAR10min from [dbo].[DeliveredBabyApgarScore] a inner join 
							[dbo].[DeliveredBabyBirthInformation]b on a.[DeliveredBabyBirthInformationId]=b.[Id]
							left join (select Id,Score APGAR1min from [dbo].[DeliveredBabyApgarScore] a left join
							lookupitemview c on c.itemid=a.[ApgarScoreId] where itemname='Apgar Score 1 min')c on c.id=a.id
							left join (select Id,Score APGAR5min from [dbo].[DeliveredBabyApgarScore] a left join
							lookupitemview c on c.itemid=a.[ApgarScoreId] where itemname='Apgar Score 5 min')d on d.id=a.id
							left join (select Id,Score APGAR10min from [dbo].[DeliveredBabyApgarScore] a left join
							lookupitemview c on c.itemid=a.[ApgarScoreId] where itemname='Apgar Score 10 min')e on e.id=a.id)apgar on apgar.DeliveredBabyBirthInformationId=baby.id	 LEFT OUTER JOIN
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
                                                               WHERE        (t.TestRound = 1) AND (lk.ItemName = 'maternity-encounter')) AS one ON one.PersonId = e.PersonId FULL OUTER JOIN
                                                             (SELECT DISTINCT t.HtsEncounterId, b.ItemName AS kitid, t.KitLotNumber, t.ExpiryDate, e.PersonId, l.ItemName AS outcome
                                                               FROM            dbo.Testing AS t INNER JOIN
                                                                                         dbo.HtsEncounter AS e ON t.HtsEncounterId = e.Id INNER JOIN
                                                                                         dbo.LookupItemView AS l ON l.ItemId = t.Outcome INNER JOIN
                                                                                         dbo.LookupItemView AS b ON b.ItemId = t.KitId INNER JOIN
                                                                                         dbo.PatientEncounter AS pe ON pe.Id = e.PatientEncounterID LEFT OUTER JOIN
                                                                                         dbo.LookupItemView AS lk ON lk.ItemId = pe.EncounterTypeId
                                                               WHERE        (t.TestRound = 2) AND (lk.ItemName = 'maternity-encounter')) AS two ON two.PersonId = e.PersonId) AS HIVTest ON HIVTest.PersonId = b.PersonId LEFT OUTER JOIN
                             (SELECT        he.PersonId, he.PatientEncounterID, lk.ItemName AS FinalResult
                               FROM            dbo.HtsEncounter AS he INNER JOIN
                                                         dbo.HtsEncounterResult AS her ON he.Id = her.HtsEncounterId INNER JOIN
                                                         dbo.PatientEncounter AS pe ON pe.Id = he.PatientEncounterID LEFT OUTER JOIN
                                                         dbo.LookupItemView AS lk1 ON lk1.ItemId = pe.EncounterTypeId LEFT OUTER JOIN
                                                         dbo.LookupItemView AS lk ON lk.ItemId = her.FinalResult
                               WHERE        (lk1.ItemName = 'maternity-encounter')) AS z ON z.PersonId = b.PersonId
WHERE        (h.Name = 'Maternity')

GO