SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('dbo.[Maternity Register]', 'V') IS NOT NULL
    DROP VIEW [dbo].[Maternity Register]
GO

Create VIEW [dbo].[Maternity Register]
AS
SELECT       distinct  a.Ptn_Pk, b.Id, b.FacilityId, h.IdentifierValue AS [Admission Number], d.VisitDate AS [Date of Admission], a.FirstName, a.MiddleName, a.LastName, a.VillageName, a.Phone, a.Landmark, a.DOB, a.Sex, a.MaritalStatus, 
                         h.VisitType, h.Name AS ServiceArea, h.VisitNumber, I.Parity, I.Gravidae, I.LMP, I.EDD AS EDC, diag.Diagnosis, delivery.DurationOfLabour, delivery.DateOfDelivery, delivery.TimeOfDelivery, delivery.ModeOfDelivery, 
                         delivery.PlacentaComplete,delivery.BloodLosscapacity, delivery.MotherCondition AS [Condition after Delivery], delivery. [DeliveryComplicationsExperienced], 
						 baby.Sex AS babyGender, baby.BirthWeight, baby.DeliveryOutcome, baby.BreastFedWithinHr, 
                         baby.TeoGiven, baby.BirthDeformity, ''APGAR1min, ''APGAR5min, ''APGAR10min, '' ANC,'' [HIV testing],
						 BAC.TreatedForSyphillis,HAARTMAT.[Started HAART MAT],
						 HIVTest.OneKitId,HIVTest.OneLotNumber,HIVTest.OneExpiryDate,HIVTest.FinalTestOneResult,
						 HIVTest.twokitid,HIVTest.twolotnumber,HIVTest.twoexpirydate,HIVTest.FinalTestTwoResult, '' FinalResult, 
						 k.Weight, k.Height, k.BPSystolic, k.BPDiastolic, k.Muac, baby.BirthNotificationNumber, 
						 null [DateDischarged], ''OutcomeStatus, 
						 partnerTesting.[PartnerTested],partnerTesting.[PartnerHIVResult],
						 Refferals.ReferredFrom, Refferals.ReferredTo, notes.ClinicalNotes
FROM            dbo.mst_Patient AS a INNER JOIN
                         dbo.Patient AS b ON a.Ptn_Pk = b.ptn_pk INNER JOIN  
						 dbo.PatientMasterVisit d ON b.Id = d.PatientId INNER JOIN
                         (select a.patientID,EnrollmentDate,IdentifierValue,Name,Visitdate,PatientMasterVisitId,
							VisitType ,[VisitNumber] ,[DaysPostPartum]  from PatientEnrollment a 
							inner  join ServiceArea b on a.ServiceAreaId=b.id
								inner join PatientIdentifier c on c.PatientId=a.PatientId
							inner join ServiceAreaIdentifiers d on c.IdentifierTypeId=d.IdentifierId and b.id=d.ServiceAreaId
							inner join dbo.VisitDetails AS g ON a.PatientId = g.PatientId AND b.Id = g.ServiceAreaId
							where b.name='Maternity'  ) AS h ON b.ID = h.patientID and d.Id = h.PatientMasterVisitId left JOIN
                         dbo.Pregnancy AS I ON b.Id = I.PatientId LEFT OUTER JOIN
						(SELECT    distinct    a.PatientId, a.PatientMasterVisitId, case when d.itemname='Known Positive' then'KP'
							when d.itemname='Unknown' then'U' when d.itemname='Revisit' then'Revisit' end as HivStatusBeforeAnc, 
							e.itemname TreatedForSyphillis, f.itemname BreastExamDone
						FROM            [dbo].[BaselineAntenatalCare] a INNER JOIN
												 dbo.PatientEncounter b  ON a.PatientMasterVisitId = b.PatientMasterVisitId LEFT OUTER JOIN
												 dbo.LookupItemView c ON c.ItemId = b.EncounterTypeId LEFT OUTER JOIN
												 dbo.LookupItemView d ON d.ItemId = a.HivStatusBeforeAnc LEFT OUTER JOIN
												 dbo.LookupItemView e ON e.ItemId = a.TreatedForSyphilis LEFT OUTER JOIN
												 dbo.LookupItemView f ON f.ItemId = a.BreastExamDone
						WHERE        (c.ItemName = 'maternity-encounter') ) BAC on BAC.PatientId=b.Id and d.Id = BAC.PatientMasterVisitId LEFT OUTER JOIN
                         (SELECT distinct[PatientId],[PatientMasterVisitId],lkup1.itemName [On ARV Before 1st ANC Visit],[Description]
							FROM [dbo].[PatientDrugAdministration] j  Left outer join dbo.LookupItemView lkup1 on lkup1.ItemId=j.Value 
							where [description] ='On ARV before 1st ANC Visit') j ON b.Id = j.PatientId  LEFT OUTER JOIN
                         dbo.PatientDiagnosis AS diag ON diag.PatientMasterVisitId = d.Id LEFT OUTER JOIN
                         dbo.PatientVitals AS k ON d.Id = k.PatientMasterVisitId AND b.Id = k.PatientId and k.VisitDate=d.VisitDate  left JOIN
						 (SELECT DISTINCT b.PatientId,b.PatientMasterVisitId,c.[ItemName] [Started HAART MAT]
							FROM dbo.PatientDrugAdministration b inner join  [dbo].[LookupItemView]a  on b.DrugAdministered=a.itemid
							inner join [dbo].[LookupItemView]c on c.itemid=b.value
							where b.[Description] ='ARVs Started in Maternity')HAARTMAT on HAARTMAT.PatientId=b.Id and HAARTMAT.PatientMasterVisitId=d.Id left outer join 
													 ------------------HIV tests
						(SELECT DISTINCT e.PersonId, one.kitid AS OneKitId, one.kitlotNumber AS OneLotNumber, one.Outcome AS FinalTestOneResult, one.encountertype as encounterone,
							two.Outcome AS FinalTestTwoResult, one.expirydate AS OneExpiryDate, two.kitid AS twokitid, 
							two.kitlotnumber AS twolotnumber, two.expirydate AS twoexpirydate,one.encountertype as encountertwo
							FROM  Testing t INNER JOIN [HtsEncounter] e ON t .htsencounterid = e.id 
							left join  [dbo].[PatientEncounter] pe on pe.id=e.PatientEncounterID  inner join lookupitemview c on c.itemid=pe.EncounterTypeId
							left outer JOIN
							(SELECT distinct  htsencounterid, b.ItemName kitid, kitlotnumber, expirydate, PersonId, l.ItemName AS outcome,e.encountertype
							FROM [Testing] t inner join  [HtsEncounter] e on t.HtsEncounterId=e.id inner join  [LookupItemView] l on l.itemid=t.Outcome
							inner join lookupitemview b on b.itemid=t.KitId inner join  [dbo].[PatientEncounter] pe on pe.id=e.PatientEncounterID  
							inner join lookupitemview c on c.itemid=pe.EncounterTypeId
							WHERE  e.encountertype = 1 and t.testround =1 and c.ItemName='maternity-encounter') one ON one.personid = e.PersonId FULL OUTER JOIN
							(SELECT distinct htsencounterid, b.ItemName kitid, kitlotnumber, expirydate, PersonId, l.ItemName AS outcome,e.encountertype
							FROM  [Testing] t inner join  [HtsEncounter] e on t.HtsEncounterId=e.id inner join  [LookupItemView] l on l.itemid=t.Outcome
							inner join lookupitemview b on b.itemid=t.KitId inner join  [dbo].[PatientEncounter] pe on pe.id=e.PatientEncounterID  
							inner join lookupitemview c on c.itemid=pe.EncounterTypeId
							where t.testround =2 and c.ItemName='maternity-encounter' ) two ON two.personid = e.PersonId
							where c.ItemName='maternity-encounter')HIVTest on HIVTest.PersonId=b.PersonId left join
                        (select distinct [DeliveryID],[PatientMasterVisitID],[DurationOfLabour],[DateOfDelivery],[TimeOfDelivery], lkup2.itemName ModeOfDelivery,lkup3.itemName [PlacentaComplete] ,
						[BloodLossCapacity],lkup4.itemName[MotherCondition],
						lkup.itemName [DeliveryComplicationsExperienced],[DeliveryComplicationNotes],[DeliveryConductedBy],[MaternalDeathAuditDate]
						from dbo.PatientDelivery AS delivery 
						left JOIN dbo.LookupItemView AS lkup2 ON lkup2.ItemId = delivery.[ModeOfDelivery] 
						left JOIN dbo.LookupItemView AS lkup3 ON lkup3.ItemId = delivery.[PlacentaComplete]
						left JOIN dbo.LookupItemView AS lkup4 ON lkup4.ItemId = delivery.[MotherCondition]
						left JOIN dbo.LookupItemView AS lkup ON lkup.ItemId = delivery.[DeliveryComplicationsExperienced] ) delivery on delivery.PatientMasterVisitID=d.Id LEFT OUTER JOIN
                        dbo.PatientOutcome AS outc ON outc.PatientMasterVisitID = d.Id LEFT OUTER JOIN
                        dbo.DeliveredBabyBirthInformation AS baby ON baby.PatientMasterVisitId = d.Id LEFT OUTER JOIN
                        dbo.PatientClinicalNotes AS notes ON notes.PatientMasterVisitId = d.Id 
						--Left join 
						--(select a.Id,a.[DeliveredBabyBirthInformationId],APGAR1min,APGAR5min,APGAR10min from [dbo].[DeliveredBabyApgarScore] a inner join 
						--[dbo].[DeliveredBabyBirthInformation]b on a.[DeliveredBabyBirthInformationId]=b.[Id]
						--left join (select Id,Score APGAR1min from [dbo].[DeliveredBabyApgarScore] a left join
						--lookupitemview c on c.itemid=a.[ApgarScoreId] where itemname='Apgar Score 1 min')c on c.id=a.id
						--left join (select Id,Score APGAR5min from [dbo].[DeliveredBabyApgarScore] a left join
						--lookupitemview c on c.itemid=a.[ApgarScoreId] where itemname='Apgar Score 5 min')d on d.id=a.id
						--left join (select Id,Score APGAR10min from [dbo].[DeliveredBabyApgarScore] a left join
						--lookupitemview c on c.itemid=a.[ApgarScoreId] where itemname='Apgar Score 10 min')e on e.id=a.id)apgar on apgar.DeliveredBabyBirthInformationId=baby.id	 
						LEFT OUTER JOIN (SELECT    distinct    a.PatientId, a.PatientMasterVisitId,d.itemname [PartnerTested], e.itemname [PartnerHIVResult]
							FROM            [dbo].[PatientPartnerTesting] a INNER JOIN
														dbo.PatientEncounter b  ON a.PatientMasterVisitId = b.PatientMasterVisitId LEFT OUTER JOIN
														dbo.LookupItemView c ON c.ItemId = b.EncounterTypeId LEFT OUTER JOIN
														dbo.LookupItemView d ON d.ItemId = a.[PartnerTested] LEFT OUTER JOIN
														dbo.LookupItemView e ON e.ItemId = a.[PartnerHIVResult]
							WHERE        (c.ItemName = 'maternity-encounter'))partnerTesting on partnerTesting.PatientId=b.Id and partnerTesting.PatientMasterVisitId=d.id left outer join
								(SELECT distinct [PatientId] ,[PatientMasterVisitId] ,b.itemname [WHOStage]
								FROM [PatientWHOStage] a inner join lookupitemview b on b.itemid=a.[WHOStage])WHO on WHO.patientid=b.ID and WHO.PatientMasterVisitId=d.id
							Left join
							(SELECT    distinct    a.PatientId, a.PatientMasterVisitId,d.itemname ReferredFrom, e.itemname ReferredTo
							FROM            dbo.PMTCTReferral a INNER JOIN
														dbo.PatientEncounter b  ON a.PatientMasterVisitId = b.PatientMasterVisitId LEFT OUTER JOIN
														dbo.LookupItemView c ON c.ItemId = b.EncounterTypeId LEFT OUTER JOIN
														dbo.LookupItemView d ON d.ItemId = a.ReferredFrom LEFT OUTER JOIN
														dbo.LookupItemView e ON e.ItemId = a.ReferredTo
							WHERE        (c.ItemName = 'maternity-encounter'))Refferals on Refferals.PatientId=b.Id and Refferals.PatientMasterVisitId=d.id 
							--LEFT OUTER JOIN
--							 (SELECT        he.PersonId, he.PatientEncounterID, lk.ItemName AS FinalResult
--							   FROM            dbo.HtsEncounter AS he INNER JOIN
--														 dbo.HtsEncounterResult AS her ON he.Id = her.HtsEncounterId INNER JOIN
--														 dbo.PatientEncounter AS pe ON pe.Id = he.PatientEncounterID LEFT OUTER JOIN
--														 dbo.LookupItemView AS lk1 ON lk1.ItemId = pe.EncounterTypeId LEFT OUTER JOIN
--														 dbo.LookupItemView AS lk ON lk.ItemId = her.FinalResult
--							   WHERE        (lk1.ItemName = 'Maternity')) AS z ON z.PersonId = b.PersonId
WHERE        (h.Name = 'Maternity')

GO


