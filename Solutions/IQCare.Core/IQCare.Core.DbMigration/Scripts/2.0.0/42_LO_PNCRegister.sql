IF OBJECT_ID('dbo.[PNC Register]', 'V') IS NOT NULL
    DROP VIEW [dbo].[PNC Register]
GO

Create VIEW [dbo].[PNC Register]
AS
SELECT  distinct      a.Ptn_Pk, b.Id,d.VisitDate, b.FacilityId, c.IdentifierValue AS [PNC Register Number], a.FirstName, a.MiddleName, a.LastName, a.VillageName, a.Phone, a.DOB, a.Sex,delivery.DateOfDelivery,''PlaceOfDelivery, delivery.ModeOfDelivery, 
						 g.VisitType, h.Name AS ServiceArea,k.Temperature, k.[HeartRate], k.BPSystolic, k.BPDiastolic, k.Muac,pallor.Pallor,breast.Breast,Uterus.Uterus,PPH.PPH,C_SectionSite.C_SectionSite,Lochia.Lochia,Episiotomy.Episiotomy,
						 '' as Infections,Breastfeeding.Breastfeeding,g.VisitNumber,case when g.[DaysPostPartum] between 0 and 2 then 1 when g.[DaysPostPartum] between 3 and 30 then 2 when g.[DaysPostPartum] between 31 and 44 then 3 end as [DaysPostPartum], 
						 '' [Prior Known Status], ''[HIV testing],HIVTest.OneKitId, HIVTest.OneLotNumber, HIVTest.OneExpiryDate, HIVTest.FinalTestOneResult, HIVTest.twokitid, HIVTest.twolotnumber, HIVTest.twoexpirydate, HIVTest.FinalTestTwoResult, FinalResult,[Started HAART PNC],
						 ----Remember <=6wks and >6wks
						 z.FinalResult [FinalResult <=6wks],z.FinalResult [FinalResult >6wks], [AZT for Baby] [AZT for Baby <=6wks],[NVP for Baby] [NVP for Baby <=6wks],[AZT for Baby] [AZT for Baby >6wks],[NVP for Baby] [NVP for Baby >6wks],
						 partnerTesting.[PartnerTested],partnerTesting.[PartnerHIVResult],Fistula_Screening.Fistula_Screening,Cacx.ScreeningCategory [Cacx Method],Cacx.Results [Cacx Result],'' [PNC Exercises],FP.FP [Modern FP] ,diag.Diagnosis
FROM            dbo.mst_Patient AS a INNER JOIN
                         dbo.Patient AS b ON a.Ptn_Pk = b.ptn_pk INNER JOIN
                          dbo.PersonIdentifier c ON b.PersonId = c.PersonId INNER JOIN
                         dbo.PatientMasterVisit AS d ON b.Id = d.PatientId INNER JOIN
                         dbo.PatientEncounter AS e ON b.Id = e.PatientId AND d.Id = e.PatientMasterVisitId INNER JOIN
						 dbo.PatientEnrollment f ON b.Id = f.PatientId inner JOIN
                         dbo.VisitDetails AS g ON b.Id = g.PatientId AND d.Id = g.PatientMasterVisitId left JOIN
                         dbo.ServiceArea AS h ON f.ServiceAreaId = h.Id left JOIN
                         dbo.Pregnancy AS I ON b.Id = I.PatientId AND d.Id = I.PatientMasterVisitId --AND g.PregnancyId = I.Id
						 LEFT OUTER JOIN
						(SELECT DISTINCT b.PatientId,b.PatientMasterVisitId,c.[ItemName] [Started HAART PNC]
							FROM dbo.PatientDrugAdministration b inner join  [dbo].[LookupItemView]a  on b.DrugAdministered=a.itemid
							inner join [dbo].[LookupItemView]c on c.itemid=b.value
							where b.[Description] ='Started HAART in PNC')HAARTPNC on HAARTPNC.PatientId=b.Id and HAARTPNC.PatientMasterVisitId=d.Id left outer join 
                         dbo.PatientDiagnosis AS diag ON diag.PatientMasterVisitId = d.Id LEFT OUTER JOIN
                         dbo.PatientVitals AS k ON d.Id = k.PatientMasterVisitId AND b.Id = k.PatientId LEFT OUTER JOIN
                         dbo.PatientDelivery AS delivery ON delivery.PatientMasterVisitID = d.Id LEFT OUTER JOIN
                         dbo.PatientOutcome AS outc ON outc.PatientMasterVisitID = d.Id LEFT OUTER JOIN
                         dbo.DeliveredBabyBirthInformation AS baby ON baby.PatientMasterVisitId = d.Id Left Outer join
						(SELECT DISTINCT b.PatientId,b.PatientMasterVisitId,[ItemName] [AZT for Baby]
							FROM [dbo].[LookupItemView]a inner join dbo.PatientDrugAdministration b on b.value=a.itemid
							where description ='AZT for the baby dispensed')AZTBaby on AZTBaby.PatientId=b.Id and AZTBaby.PatientMasterVisitId=d.Id left outer join 
							(SELECT DISTINCT b.PatientId,b.PatientMasterVisitId,[ItemName] [NVP for Baby]
							FROM [dbo].[LookupItemView]a inner join dbo.PatientDrugAdministration b on b.value=a.itemid
							where description ='NVP for the baby dispensed')NVPBaby on NVPBaby.PatientId=b.Id and NVPBaby.PatientMasterVisitId=d.Id left outer join 
						 (select distinct [PatientId],[PatientMasterVisitId], case when lkup.ItemName ='Normal' then 1 when lkup.ItemName ='Cracked' then 2 when lkup.ItemName ='Engorged' then 3 when lkup.ItemName ='Mastitis' then 4 end as Breast 
						  from [dbo].[PhysicalExamination] a inner join lookupitemview lkup on lkup.itemid=a.ExamId inner join lookupitemview lkup1 on lkup1.masterid=a.ExaminationTypeId
						  where lkup1.mastername='Breast') breast on d.id=breast.[PatientMasterVisitId] LEFT OUTER JOIN

						(select distinct [PatientId],[PatientMasterVisitId], case when lkup.ItemName ='Contracted' then 1 when lkup.ItemName ='Not Contracted' then 2 when lkup.ItemName ='Other' then 3 end as Uterus from [dbo].[PhysicalExamination] a 
						inner join lookupitemview lkup on lkup.itemid=a.ExamId inner join lookupitemview lkup1 on lkup1.masterid=a.ExaminationTypeId where lkup1.mastername='Uterus')Uterus on d.id=Uterus.[PatientMasterVisitId] LEFT OUTER JOIN

						(select distinct [PatientId],[PatientMasterVisitId],case when lkup.ItemName ='Contracted' then 1 when lkup.ItemName ='Not Contracted' then 2 when lkup.ItemName ='Other' then 3 end as PPH from [dbo].[PhysicalExamination] a 
						inner join lookupitemview lkup on lkup.itemid=a.ExamId inner join lookupitemview lkup1 on lkup1.masterid=a.ExaminationTypeId where lkup1.mastername='PostPartumHaemorrhage')PPH on d.id=PPH.[PatientMasterVisitId] LEFT OUTER JOIN

						(select distinct [PatientId],[PatientMasterVisitId],case when lkup1.ItemName ='Normal' then 1 when lkup1.ItemName ='Foul smelling' then 2 when lkup1.ItemName ='Excessive' then 3 end as Lochia from [dbo].[PhysicalExamination] a 
						inner join lookupitemview lkup on lkup.itemid=a.ExamId inner join lookupitemview lkup1 on lkup1.itemid=a.FindingId where lkup.itemname='Lochia')Lochia on d.id=Lochia.[PatientMasterVisitId] LEFT OUTER JOIN

						(select distinct [PatientId],[PatientMasterVisitId],case when lkup1.ItemName ='Yes' then 'Y'when lkup1.ItemName ='No' then 'N' when lkup1.ItemName ='N/A' then 'N/A' end as Pallor  from [dbo].[PhysicalExamination] a 
						inner join lookupitemview lkup on lkup.itemid=a.ExamId inner join lookupitemview lkup1 on lkup1.itemid=a.FindingId where lkup.itemname='Pallor')Pallor on d.id=Pallor.[PatientMasterVisitId] LEFT OUTER JOIN

						(select distinct [PatientId],[PatientMasterVisitId],case when lkup1.ItemName ='Repaired' then 1 when lkup1.ItemName ='Gaping' then 2 when lkup1.ItemName ='Infected' then 3 when lkup1.ItemName ='Healed' then 4 
									when lkup1.ItemName ='N/A' then 5 end as Episiotomy from [dbo].[PhysicalExamination] a inner join lookupitemview lkup on lkup.itemid=a.ExamId inner join lookupitemview lkup1 on lkup1.itemid=a.FindingId
						where lkup.itemname='Episiotomy')Episiotomy on d.id=Episiotomy.[PatientMasterVisitId] LEFT OUTER JOIN

						(select distinct [PatientId],[PatientMasterVisitId],case when lkup1.ItemName ='Normal' then 1 when lkup1.ItemName ='Bleeding' then 2 when lkup1.ItemName ='Infected' then 3 when lkup1.ItemName ='Gaping' then 4 
									when lkup1.ItemName ='N/A' then 5 end as C_SectionSite from [dbo].[PhysicalExamination] a inner join lookupitemview lkup on lkup.itemid=a.ExamId inner join lookupitemview lkup1 on lkup1.itemid=a.FindingId
						where lkup.itemname='C_SectionSite')C_SectionSite on d.id=C_SectionSite.[PatientMasterVisitId] LEFT OUTER JOIN

						(select distinct [PatientId],[PatientMasterVisitId],case when lkup1.ItemName ='None' then 1 when lkup1.ItemName ='Rectovaginal Fistula' then 2 when lkup1.ItemName ='Vesicovaginal Fistula' then 3 end as Fistula_Screening 
						from [dbo].[PhysicalExamination] a inner join lookupitemview lkup on lkup.itemid=a.ExamId inner join lookupitemview lkup1 on lkup1.itemid=a.FindingId
						where lkup.itemname='Fistula_Screening')Fistula_Screening on d.id=Fistula_Screening.[PatientMasterVisitId] LEFT OUTER JOIN

						(select distinct [PatientId],[PatientMasterVisitId],case when lkup1.ItemName ='Yes' then 'Y'when lkup1.ItemName ='No' then 'N' when lkup1.ItemName ='N/A' then 'N/A' end as Breastfeeding from [dbo].[PhysicalExamination] a 
						inner join lookupitemview lkup on lkup.itemid=a.ExamId inner join lookupitemview lkup1 on lkup1.itemid=a.FindingId where lkup.itemname='Breastfeeding')Breastfeeding on d.id=Breastfeeding.[PatientMasterVisitId] LEFT OUTER JOIN
						(SELECT    distinct    a.PatientId, a.PatientMasterVisitId,d.itemname [PartnerTested], e.itemname [PartnerHIVResult]
								FROM            [dbo].[PatientPartnerTesting] a INNER JOIN
														 dbo.PatientEncounter b  ON a.PatientMasterVisitId = b.PatientMasterVisitId LEFT OUTER JOIN
														 dbo.LookupItemView c ON c.ItemId = b.EncounterTypeId LEFT OUTER JOIN
														 dbo.LookupItemView d ON d.ItemId = a.[PartnerTested] LEFT OUTER JOIN
														 dbo.LookupItemView e ON e.ItemId = a.[PartnerHIVResult]
								WHERE        (c.ItemName = 'pnc-encounter'))partnerTesting on partnerTesting.PatientId=b.Id and partnerTesting.PatientMasterVisitId=d.id left outer join
								
						(SELECT a.[PatientId],b.ItemName FP,PatientMasterVisitId FROM [dbo].[PatientFamilyPlanningMethod] a inner join lookupitemview b on b.ItemId=a.[FPMethodId] inner join [PatientFamilyPlanning]c on c.[Id]=a.PatientFPId
								inner join [PatientMasterVisit] d on d.[Id]=c.[PatientMasterVisitId]  where b.ItemName not in ('UND') or [FPMethodId] is null)FP on FP.PatientId=b.Id and FP.PatientMasterVisitId=d.id	left outer join
						(SELECT distinct [PatientId],[PatientMasterVisitId],[ScreeningDate],c.Itemname ScreeningCategory,d.itemname Results,[VisitDate]
							FROM [dbo].[PatientScreening] a inner join lookupitemview b on b.masterid=a.[ScreeningTypeId]
							inner join lookupitemview c on c.itemid=a.[ScreeningCategoryId] inner join lookupitemview d on d.itemid=a.[ScreeningValueId]
							where b.mastername='CacxMethod')Cacx on Cacx.PatientId=b.Id and Cacx.PatientMasterVisitId=d.id	 left join

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
                             (SELECT he.PersonId, he.PatientEncounterID, lk.ItemName AS FinalResult
							   FROM  dbo.HtsEncounter AS he INNER JOIN
								dbo.HtsEncounterResult AS her ON he.Id = her.HtsEncounterId INNER JOIN
								dbo.PatientEncounter AS pe ON pe.Id = he.PatientEncounterID LEFT OUTER JOIN
								dbo.LookupItemView AS lk1 ON lk1.ItemId = pe.EncounterTypeId LEFT OUTER JOIN
								dbo.LookupItemView AS lk ON lk.ItemId = her.FinalResult
								WHERE  (lk1.ItemName = 'pnc-encounter')) AS z ON z.PersonId = b.PersonId
WHERE        (h.Name = 'PNC')











GO


