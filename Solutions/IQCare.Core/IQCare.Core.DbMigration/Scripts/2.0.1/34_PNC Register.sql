SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('dbo.[PNC Register]', 'V') IS NOT NULL
    DROP VIEW [dbo].[PNC Register]
GO

Create VIEW [dbo].[PNC Register]
AS
SELECT  distinct      a.Ptn_Pk, b.Id,h.VisitDate, b.FacilityId, h.IdentifierValue AS [PNC Register Number], a.FirstName, a.MiddleName, a.LastName, a.VillageName, a.Phone, a.DOB, a.Sex,delivery.DateOfDelivery,''PlaceOfDelivery, lkdel.itemName ModeOfDelivery, 
						 h.VisitType, h.Name AS ServiceArea,k.Temperature, k.[HeartRate], k.BPSystolic, k.BPDiastolic, k.Muac,pallor.Pallor,breast.Breast,Uterus.Uterus,PPH.PPH,C_SectionSite.C_SectionSite,Lochia.Lochia,Episiotomy.Episiotomy,
						 '' as Infections,Breastfeeding.Breastfeeding,h.VisitNumber,case when h.[DaysPostPartum] between 0 and 2 then 1 when h.[DaysPostPartum] between 3 and 30 then 2 when h.[DaysPostPartum] between 31 and 44 then 3 end as [DaysPostPartum], 
						 '' [Prior Known Status], case when HIVTest.OneKitId is not null then 'Yes' else 'No' end as [HIV testing],HIVTest.OneKitId, HIVTest.OneLotNumber, HIVTest.OneExpiryDate, HIVTest.FinalTestOneResult, HIVTest.twokitid, HIVTest.twolotnumber,
						  HIVTest.twoexpirydate, HIVTest.FinalTestTwoResult, z.FinalResult,[Started HAART PNC],
						 ----Remember <=6wks and >6wks
						 z.FinalResult [FinalResult <=6wks],z.FinalResult [FinalResult >6wks], [AZT for Baby] [AZT for Baby <=6wks],[NVP for Baby] [NVP for Baby <=6wks],[AZT for Baby] [AZT for Baby >6wks],[NVP for Baby] [NVP for Baby >6wks],
						 partnerTesting.[PartnerTested],partnerTesting.[PartnerHIVResult],Fistula_Screening.Fistula_Screening,Cacx.ScreeningCategory [Cacx Method],Cacx.Results [Cacx Result],PNCExercise [PNC Exercises],FP.FP [Modern FP] ,diag.Diagnosis
FROM            dbo.mst_Patient AS a INNER JOIN
                         dbo.Patient AS b ON a.Ptn_Pk = b.ptn_pk INNER JOIN
                         dbo.PatientMasterVisit AS d ON b.Id = d.PatientId INNER JOIN
                        (select a.patientID,EnrollmentDate,IdentifierValue,Name,Visitdate,PatientMasterVisitId,
							VisitType ,[VisitNumber] ,[DaysPostPartum]  from PatientEnrollment a 
							inner  join ServiceArea b on a.ServiceAreaId=b.id
								inner join PatientIdentifier c on c.PatientId=a.PatientId
							inner join ServiceAreaIdentifiers d on c.IdentifierTypeId=d.IdentifierId and b.id=d.ServiceAreaId
							inner join dbo.VisitDetails AS g ON a.PatientId = g.PatientId AND b.Id = g.ServiceAreaId
							where b.name='PNC') AS h ON b.ID = h.patientID and d.Id = h.PatientMasterVisitId
						 LEFT OUTER JOIN
						(SELECT DISTINCT b.PatientId,b.PatientMasterVisitId,c.[ItemName] [Started HAART PNC]
							FROM dbo.PatientDrugAdministration b inner join  [dbo].[LookupItemView]a  on b.DrugAdministered=a.itemid
							inner join [dbo].[LookupItemView]c on c.itemid=b.value
							where b.[Description] ='Started HAART in PNC')HAARTPNC on HAARTPNC.PatientId=b.Id and HAARTPNC.PatientMasterVisitId=d.Id left outer join 
                         dbo.PatientDiagnosis AS diag ON diag.PatientMasterVisitId = d.Id LEFT OUTER JOIN
                         dbo.PatientVitals AS k ON d.VisitDate = k.VisitDate AND b.Id = k.PatientId LEFT OUTER JOIN
                         dbo.PatientDelivery AS delivery ON delivery.PatientMasterVisitID = d.Id left JOIN 
						 dbo.LookupItemView AS lkdel ON lkdel.ItemId = delivery.[ModeOfDelivery] LEFT OUTER JOIN
                         dbo.PatientOutcome AS outc ON outc.PatientMasterVisitID = d.Id LEFT OUTER JOIN
                         dbo.DeliveredBabyBirthInformation AS baby ON baby.PatientMasterVisitId = d.Id Left Outer join
						(SELECT DISTINCT b.PatientId,b.PatientMasterVisitId,case when [ItemName] in ('Start','Continue') then 'Yes' else [ItemName] end as [AZT for Baby]
							FROM [dbo].[LookupItemView]a inner join dbo.PatientDrugAdministration b on b.value=a.itemid
							where description like'%AZT%')AZTBaby on AZTBaby.PatientId=b.Id and AZTBaby.PatientMasterVisitId=d.Id left outer join 
						(SELECT DISTINCT b.PatientId,b.PatientMasterVisitId,case when [ItemName] in ('Start','Continue') then 'Yes' else [ItemName] end as [NVP for Baby]
							FROM [dbo].[LookupItemView]a inner join dbo.PatientDrugAdministration b on b.value=a.itemid
								where description like'%NVP%')NVPBaby on NVPBaby.PatientId=b.Id and NVPBaby.PatientMasterVisitId=d.Id 
							left join (select distinct patientid,PatientMasterVisitId,b.ItemName as PNCExercise  from PatientPncExercises a inner join  lookupitemview b on b.itemid=PncExercisesDone)PncExer on d.ID=pncexer.PatientMasterVisitId and b.id=pncexer.PatientID
							left outer join 
						 (select distinct [PatientId],[PatientMasterVisitId], case when lkup.ItemName ='Normal' then 1 when lkup.ItemName ='Cracked' then 2 when lkup.ItemName ='Engorged' then 3 when lkup.ItemName ='Mastitis' then 4 end as Breast 
						  from [dbo].[PhysicalExamination] a inner join lookupitemview lkup on lkup.itemid=a.FindingId  inner join lookupitemview lkup1 on lkup1.itemid=a.ExamID where lkup1.itemname='Breast') breast on d.id=breast.[PatientMasterVisitId] LEFT OUTER JOIN

						(select distinct [PatientId],[PatientMasterVisitId], case when lkup.ItemName ='Contracted' then 1 when lkup.ItemName ='Not Contracted' then 2 when lkup.ItemName like'Other%' then 3 end as Uterus from [dbo].[PhysicalExamination] a 
						inner join lookupitemview lkup on lkup.itemid=a.FindingId  inner join lookupitemview lkup1 on lkup1.itemid=a.ExamID where lkup1.itemname='Uterus')Uterus on d.id=Uterus.[PatientMasterVisitId] LEFT OUTER JOIN

						(select distinct [PatientId],[PatientMasterVisitId],case when lkup.ItemName ='Absent' then 1 when lkup.ItemName ='Present' then 2 when lkup.ItemName like 'Other%' then 3 end as PPH 
						from [dbo].[PhysicalExamination] a inner join lookupitemview lkup on lkup.itemid=a.FindingId  inner join lookupitemview lkup1 on lkup1.itemid=a.ExamID where lkup1.itemname='PostPartumHaemorrhage')PPH on d.id=PPH.[PatientMasterVisitId] LEFT OUTER JOIN

						(select distinct [PatientId],[PatientMasterVisitId],case when lkup.ItemName ='Normal' then 1 when lkup.ItemName ='Foul smelling' then 2 when lkup.ItemName ='Excessive' then 3 end as Lochia from [dbo].[PhysicalExamination] a 
						inner join lookupitemview lkup on lkup.itemid=a.FindingId  inner join lookupitemview lkup1 on lkup1.itemid=a.ExamID where lkup1.itemname='Lochia')Lochia on d.id=Lochia.[PatientMasterVisitId] LEFT OUTER JOIN

						(select distinct [PatientId],[PatientMasterVisitId],case when lkup.ItemName ='Yes' then 'Y'when lkup.ItemName ='No' then 'N' when lkup.ItemName ='N/A' then 'N/A' end as Pallor  from [dbo].[PhysicalExamination] a 
						inner join lookupitemview lkup on lkup.itemid=a.FindingId  inner join lookupitemview lkup1 on lkup1.itemid=a.ExamID where lkup1.itemname='Pallor')Pallor on d.id=Pallor.[PatientMasterVisitId] LEFT OUTER JOIN

						(select distinct [PatientId],[PatientMasterVisitId],case when lkup.ItemName ='Repaired' then 1 when lkup.ItemName ='Gaping' then 2 when lkup.ItemName ='Infected' then 3 when lkup.ItemName ='Healed' then 4 
									when lkup.ItemName ='N/A' then 5 end as Episiotomy from [dbo].[PhysicalExamination] a inner join lookupitemview lkup on lkup.itemid=a.FindingId  inner join lookupitemview lkup1 on lkup1.itemid=a.ExamID where lkup1.itemname='Episiotomy')Episiotomy on d.id=Episiotomy.[PatientMasterVisitId] LEFT OUTER JOIN

						(select distinct [PatientId],[PatientMasterVisitId],case when lkup.ItemName ='Normal' then 1 when lkup.ItemName ='Bleeding' then 2 when lkup.ItemName ='Infected' then 3 when lkup.ItemName ='Gaping' then 4 
									when lkup.ItemName ='N/A' then 5 end as C_SectionSite from [dbo].[PhysicalExamination] a inner join lookupitemview lkup on lkup.itemid=a.FindingId  inner join lookupitemview lkup1 on lkup1.itemid=a.ExamID where lkup1.itemname='C_SectionSite')C_SectionSite on d.id=C_SectionSite.[PatientMasterVisitId] LEFT OUTER JOIN

						(select distinct [PatientId],[PatientMasterVisitId],case when lkup.ItemName ='None' then 'None' when lkup.ItemName ='Rectovaginal Fistula' then 'RVF' when lkup.ItemName ='Vesicovaginal Fistula' then 'VVF' when lkup.ItemName ='N/A' then 'N/A' end as Fistula_Screening 
						from [dbo].[PhysicalExamination] a inner join lookupitemview lkup on lkup.itemid=a.FindingId  inner join lookupitemview lkup1 on lkup1.itemid=a.ExamID where lkup1.itemname='Fistula_Screening')Fistula_Screening on d.id=Fistula_Screening.[PatientMasterVisitId] LEFT OUTER JOIN

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


