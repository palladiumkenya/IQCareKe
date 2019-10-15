
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER VIEW [dbo].[ANC Register]
AS
SELECT  distinct a.Ptn_Pk,b.Id, a.FirstName, a.MiddleName, a.LastName, cast(a.DOB as date)DOB, a.Sex, a.MaritalStatus, a.Phone, 
        cast(d.VisitDate as date)VisitDate, h.VisitType, h.IdentifierValue AS ANC_Number, h.Name AS ServiceArea, b.FacilityId, h.VisitNumber, 
        i.Parity, i.Gravidae, a.Landmark, a.VillageName, cast(i.LMP as date)LMP, cast(i.EDD as date) EDC, k.Weight, k.Height, 
        k.BPSystolic, k.BPDiastolic, k.Muac,BAC.BreastExamDone BreastExam,counselledOn AS CounselledOn,HBLab.[TestResults] HB,BAC.TreatedForSyphillis AS [RPR/VDRL],RPRLab.TestResults1 AS [RPR/VDRL Results],
		BAC.TreatedForSyphillis AS [Syphilis Treated],BAC.HivStatusBeforeAnc [HIV status before 1st ANC],
		case when encounterone=1 then 'I' when encounterone=2 then 'R' end as [HIV testing] ,HIVTest.OneKitId,HIVTest.OneLotNumber,cast(HIVTest.OneExpiryDate as date)OneExpiryDate,HIVTest.FinalTestOneResult,
		HIVTest.TwoKitId,HIVTest.TwoLotNumber,cast(HIVTest.twoexpirydate as date)TwoExpiryDate,HIVTest.FinalTestTwoResult, z.FinalResult,WHO.[WHOStage],
		j.[On ARV Before 1st ANC Visit], [Started HAART in ANC],[CTX],[AZT for Baby],[NVP for Baby],
		TBScreening,CaCXScreening,OtherConditions,[Deworming],IPT [IPT 1-3], TTDose [TT Dose], Supplementation, TreatedNets AS [Received ITN],NULL [Additional Treatment],ANC_Exercises [ANC Exercises],
		partnerTesting.[PartnerTested],partnerTesting.[PartnerHIVResult],Refferals.ReferredFrom,Refferals.ReferredTo
		, cast(TCAs.AppointmentDate as date) TCA, TCAs.[Description] Remarks
FROM  dbo.mst_Patient a INNER JOIN
    dbo.Patient b ON a.Ptn_Pk = b.ptn_pk INNER JOIN
    dbo.PatientMasterVisit d ON b.Id = d.PatientId INNER JOIN
    (select a.patientID,EnrollmentDate,IdentifierValue,Name,Visitdate,PatientMasterVisitId,
							VisitType ,[VisitNumber] ,[DaysPostPartum]  from PatientEnrollment a 
							inner  join ServiceArea b on a.ServiceAreaId=b.id
								inner join PatientIdentifier c on c.PatientId=a.PatientId
							inner join ServiceAreaIdentifiers d on c.IdentifierTypeId=d.IdentifierId and b.id=d.ServiceAreaId
							inner join dbo.VisitDetails AS g ON a.PatientId = g.PatientId AND b.Id = g.ServiceAreaId
							where b.name='ANC'  ) AS h ON b.ID = h.patientID and d.Id = h.PatientMasterVisitId left JOIN
    dbo.Pregnancy I ON b.Id = I.PatientId AND d.Id = i.PatientMasterVisitId LEFT OUTER JOIN
	(SELECT    distinct    a.PatientId, a.PatientMasterVisitId, case when d.itemname='Known Positive' then'KP'
		when d.itemname='Unknown' then'U' when d.itemname='Revisit' then'Revisit' end as HivStatusBeforeAnc, 
		e.itemname TreatedForSyphillis, f.itemname BreastExamDone
	FROM            [dbo].[BaselineAntenatalCare] a INNER JOIN
							 dbo.PatientEncounter b  ON a.PatientMasterVisitId = b.PatientMasterVisitId LEFT OUTER JOIN
							 dbo.LookupItemView c ON c.ItemId = b.EncounterTypeId LEFT OUTER JOIN
							 dbo.LookupItemView d ON d.ItemId = a.HivStatusBeforeAnc LEFT OUTER JOIN
							 dbo.LookupItemView e ON e.ItemId = a.TreatedForSyphilis LEFT OUTER JOIN
							 dbo.LookupItemView f ON f.ItemId = a.BreastExamDone
	WHERE        (c.ItemName = 'ANC-Encounter')
) BAC on BAC.PatientId=b.Id and d.Id = BAC.PatientMasterVisitId LEFT OUTER JOIN
    (SELECT distinct[PatientId],[PatientMasterVisitId],lkup1.itemName [On ARV Before 1st ANC Visit],[Description]
		FROM [dbo].[PatientDrugAdministration] j  Left outer join dbo.LookupItemView lkup1 on lkup1.ItemId=j.Value 
		where [description] ='On ARV before 1st ANC Visit') j ON b.Id = j.PatientId AND d.Id = j.PatientMasterVisitId  Left outer join 
    dbo.PatientVitals k ON b.Id = k.PatientId and k.VisitDate=d.VisitDate Left outer join 
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
		WHERE  e.encountertype = 1 and t.testround =1 and c.ItemName='anc-encounter') one ON one.personid = e.PersonId FULL OUTER JOIN
		(SELECT distinct htsencounterid, b.ItemName kitid, kitlotnumber, expirydate, PersonId, l.ItemName AS outcome,e.encountertype
		FROM  [Testing] t inner join  [HtsEncounter] e on t.HtsEncounterId=e.id inner join  [LookupItemView] l on l.itemid=t.Outcome
		inner join lookupitemview b on b.itemid=t.KitId inner join  [dbo].[PatientEncounter] pe on pe.id=e.PatientEncounterID  
		inner join lookupitemview c on c.itemid=pe.EncounterTypeId
		where t.testround =2 and c.ItemName='anc-encounter' ) two ON two.personid = e.PersonId
		where c.ItemName='anc-encounter' )HIVTest on HIVTest.PersonId=b.PersonId  left outer join 
	(SELECT DISTINCT b.PatientId,b.PatientMasterVisitId,c.[ItemName] [Started HAART in ANC]
	FROM dbo.PatientDrugAdministration b inner join  [dbo].[LookupItemView]a  on b.DrugAdministered=a.itemid
	inner join [dbo].[LookupItemView]c on c.itemid=b.value
	where b.[Description] ='Started HAART in ANC')HAARTANC on HAARTANC.PatientId=b.Id and HAARTANC.PatientMasterVisitId=d.Id left outer join 
	(SELECT DISTINCT b.PatientId,b.PatientMasterVisitId,c.[ItemName] [CTX]
	FROM dbo.PatientDrugAdministration b inner join  [dbo].[LookupItemView]a  on b.DrugAdministered=a.itemid
	inner join [dbo].[LookupItemView]c on c.itemid=b.value
	where a.itemname ='Cotrimoxazole')CTX on CTX.PatientId=b.Id and CTX.PatientMasterVisitId=d.Id 
	Left Outer join
	(SELECT DISTINCT b.PatientId,b.PatientMasterVisitId,[ItemName] [AZT for Baby]
	FROM [dbo].[LookupItemView]a inner join dbo.PatientDrugAdministration b on b.value=a.itemid
	where description ='AZT for the baby dispensed')AZTBaby on AZTBaby.PatientId=b.Id and AZTBaby.PatientMasterVisitId=d.Id left outer join 
	(SELECT DISTINCT b.PatientId,b.PatientMasterVisitId,[ItemName] [NVP for Baby]
	FROM [dbo].[LookupItemView]a inner join dbo.PatientDrugAdministration b on b.value=a.itemid
	where description ='NVP for the baby dispensed')NVPBaby on NVPBaby.PatientId=b.Id and NVPBaby.PatientMasterVisitId=d.Id left outer join 
	----------------Counselled On
	(SELECT distinct SS.PatientId,[PatientMasterVisitId], STUFF((SELECT ', ' + lk.ItemName 
		FROM PatientCounselling US inner join lookupitemview lk on lk.itemid=us.CounsellingTopicId
		WHERE US.PatientId = SS.PatientId and mastername ='counselledOn'
		FOR XML PATH('')), 1, 1, '') counselledOn
	FROM PatientCounselling SS
	GROUP BY SS.PatientId, SS.PatientMasterVisitID)counselledOn on counselledOn.PatientId=b.Id and counselledOn.PatientMasterVisitId=d.Id left Outer join
	------HB
	(SELECT b.ID PatientId
		  ,[OrderedByDate]
		  ,[ReportedByDate]
		  ,[TestResults]
		  ,[VisitDate]
	FROM [dbo].[VW_PatientLaboratory] a
	inner join patient b on b.Ptn_Pk =a.Ptn_PK
	where testname='HB' and hasresult=1)HBLab on HBLab.PatientId=b.Id and HBLab.OrderedByDate=d.[VisitDate] left Outer join
	------RPR
	(SELECT b.ID PatientId
			,[OrderedByDate]
			,[ReportedByDate]
			,[TestResults]
			,[TestResults1]
			,[VisitDate]
	FROM [dbo].[VW_PatientLaboratory] a
	inner join patient b on b.Ptn_Pk =a.Ptn_PK
	where testname like '%RPR%'and hasresult=1)RPRLab on RPRLab.PatientId=b.Id and RPRLab.OrderedByDate=d.[VisitDate] left Outer join
----------------TB Screening
	(SELECT DISTINCT ps.PatientId,ps.PatientMasterVisitId, lk.ItemName AS TBScreening
	FROM dbo.PatientScreening ps left JOIN
	dbo.LookupItemView AS lv ON ps.ScreeningTypeId = lv.masterid left join
	dbo.LookupItemView AS lk ON ps.ScreeningValueId = lk.itemid
	WHERE lv.MasterName like'%TBScreeningPMTCT%') TBScreen on TBScreen.PatientId=b.Id and TBScreen.PatientMasterVisitId=d.Id left outer join
----------------CaCX Screening
	(SELECT DISTINCT ps.PatientId,ps.PatientMasterVisitId, lk.ItemName AS CaCxScreening
	FROM dbo.PatientScreening ps INNER JOIN
	dbo.LookupItemView AS lv ON ps.ScreeningTypeId = lv.masterid inner join
	dbo.LookupItemView AS lk ON ps.ScreeningValueId = lk.itemid
	WHERE lv.MasterName LIKE '%CaCxScreening%')CaCX on CaCX.PatientId=b.Id and CaCX.PatientMasterVisitId=d.Id left Outer join
----------------Other Conditions-- Chronic Illnesses
	(SELECT distinct SS.PatientId,[PatientMasterVisitId], STUFF((SELECT ', ' + lk.ItemName 
		FROM PatientChronicIllness US inner join lookupitemview lk on lk.itemid=us.ChronicIllness
		WHERE US.PatientId = SS.PatientId and mastername ='ChronicIllness'
		FOR XML PATH('')), 1, 1, '') OtherConditions
	FROM PatientChronicIllness SS
	GROUP BY SS.PatientId, SS.PatientMasterVisitID)OtherConditions on OtherConditions.PatientId=b.Id and OtherConditions.PatientMasterVisitId=d.Id left Outer join
-----------------Treatment 
	(SELECT DISTINCT [PatientId] PatientId,PatientMasterVisitId,'Yes' [Deworming]
	FROM [dbo].[LookupItemView]a inner join [dbo].[PatientPreventiveServices] b on b.[PreventiveServiceid]=a.itemid
	where ItemName ='Dewormed')Dewormed on Dewormed.PatientId=b.Id and Dewormed.PatientMasterVisitId=d.Id left join 
	(SELECT DISTINCT [PatientId] PatientId,PatientMasterVisitId,[ItemName] TTDose
	FROM [dbo].[LookupItemView]a inner join [dbo].[PatientPreventiveServices] b on b.[PreventiveServiceid]=a.itemid
	where Itemname in('TT1','TT2','TT3','TT4','TT5'))TTDose on TTDose.PatientId=b.Id and TTDose.PatientMasterVisitId=d.Id left join 
	(SELECT DISTINCT [PatientId] PatientId,PatientMasterVisitId,[ItemName] IPT
	FROM [dbo].[LookupItemView]a inner join [dbo].[PatientPreventiveServices] b on b.[PreventiveServiceid]=a.itemid
	where Itemname in('IPTp1','IPTp2','IPTp3'))IPTDose on IPTDose.PatientId=b.Id and IPTDose.PatientMasterVisitId=d.Id left join 
	(SELECT DISTINCT [PatientId] PatientId,PatientMasterVisitId,'Yes' Supplementation
	FROM [dbo].[LookupItemView]a inner join [dbo].[PatientPreventiveServices] b on b.[PreventiveServiceid]=a.itemid
	where ItemName ='Folate'or ItemName='Calcium' or ItemName ='Iron'or ItemName ='Vitamins')Vitamins on Vitamins.PatientId=b.Id and Vitamins.PatientMasterVisitId=d.Id left join 
	(SELECT DISTINCT [PatientId]PatientId,PatientMasterVisitId,itemname ANC_Exercises
	FROM [dbo].[PatientPreventiveServices] b  inner join lookupitemview l on l.itemid=b.PreventiveServiceId
	where Description ='Antenatal exercise')ANC_Exercises on ANC_Exercises.PatientId=b.Id and ANC_Exercises.PatientMasterVisitId=d.Id left join 
	(SELECT DISTINCT [PatientId]PatientId,PatientMasterVisitId,itemname TreatedNets
	FROM [dbo].[PatientPreventiveServices] b inner join lookupitemview l on l.itemid=b.PreventiveServiceId 
	where Description ='Insecticide treated nets given')TreatedNets on TreatedNets.PatientId=b.Id and TreatedNets.PatientMasterVisitId=d.Id left join 
	----TCA
	(SELECT  [PatientMasterVisitId]
		  ,[PatientId]
		  ,[AppointmentDate]
		  ,[AppointmentReason]=(SELECT        TOP 1 ItemName
		  FROM            [dbo].[LookupItemView]
		  WHERE        ItemId = [ReasonId])
		  ,[Description]
	  FROM [dbo].[PatientAppointment]
	  where deleteflag = 0 and serviceareaid=3)TCAs on TCAs.PatientId=b.Id and TCAs.PatientMasterVisitId=d.Id left join

	---Partner Testing
	(SELECT    distinct    a.PatientId, a.PatientMasterVisitId,d.itemname [PartnerTested], e.itemname [PartnerHIVResult]
	FROM            [dbo].[PatientPartnerTesting] a INNER JOIN
							 dbo.PatientEncounter b  ON a.PatientMasterVisitId = b.PatientMasterVisitId LEFT OUTER JOIN
							 dbo.LookupItemView c ON c.ItemId = b.EncounterTypeId LEFT OUTER JOIN
							 dbo.LookupItemView d ON d.ItemId = a.[PartnerTested] LEFT OUTER JOIN
							 dbo.LookupItemView e ON e.ItemId = a.[PartnerHIVResult]
	WHERE        (c.ItemName = 'ANC-Encounter'))partnerTesting on partnerTesting.PatientId=b.Id and partnerTesting.PatientMasterVisitId=d.id left outer join
		(SELECT distinct [PatientId] ,[PatientMasterVisitId] ,b.itemname [WHOStage]
		FROM [PatientWHOStage] a inner join lookupitemview b on b.itemid=a.[WHOStage])WHO on WHO.patientid=b.ID and WHO.PatientMasterVisitId=d.id Left join
	(SELECT    distinct    a.PatientId, a.PatientMasterVisitId,d.itemname ReferredFrom, e.itemname ReferredTo
	FROM            dbo.PMTCTReferral a INNER JOIN
							 dbo.PatientEncounter b  ON a.PatientMasterVisitId = b.PatientMasterVisitId LEFT OUTER JOIN
							 dbo.LookupItemView c ON c.ItemId = b.EncounterTypeId LEFT OUTER JOIN
							 dbo.LookupItemView d ON d.ItemId = a.ReferredFrom LEFT OUTER JOIN
							 dbo.LookupItemView e ON e.ItemId = a.ReferredTo
	WHERE        (c.ItemName = 'ANC-Encounter'))Refferals on Refferals.PatientId=b.Id and Refferals.PatientMasterVisitId=d.id LEFT OUTER JOIN
                             (SELECT        he.PersonId, he.PatientEncounterID, lk.ItemName AS FinalResult
                               FROM            dbo.HtsEncounter AS he INNER JOIN
                                                         dbo.HtsEncounterResult AS her ON he.Id = her.HtsEncounterId INNER JOIN
                                                         dbo.PatientEncounter AS pe ON pe.Id = he.PatientEncounterID LEFT OUTER JOIN
                                                         dbo.LookupItemView AS lk1 ON lk1.ItemId = pe.EncounterTypeId LEFT OUTER JOIN
                                                         dbo.LookupItemView AS lk ON lk.ItemId = her.FinalResult
                               WHERE        (lk1.ItemName = 'ANC-Encounter')) AS z ON z.PersonId = b.PersonId
WHERE        (h.Name = 'ANC')





GO


