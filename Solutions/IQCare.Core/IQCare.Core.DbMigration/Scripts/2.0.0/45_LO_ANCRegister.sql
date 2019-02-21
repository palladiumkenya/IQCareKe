
IF OBJECT_ID('dbo.[ANC Register]', 'V') IS NOT NULL
    DROP VIEW [dbo].[ANC Register]
GO

Create VIEW [dbo].[ANC Register]
AS
SELECT  distinct a.Ptn_Pk,b.Id, a.FirstName, a.MiddleName, a.LastName, a.DOB, a.Sex, a.MaritalStatus, a.Phone, 
        d.VisitDate, g.VisitType, c.IdentifierValue AS ANC_Number, h.Name AS ServiceArea, b.FacilityId, g.VisitNumber, 
        i.Parity, i.Gravidae, a.Landmark, a.VillageName, i.LMP, i.EDD EDC, k.Weight, k.Height, 
        k.BPSystolic, k.BPDiastolic, k.Muac,BAC.BreastExamDone BreastExam,'' AS CounselledOn,NULL HB,BAC.TreatedForSyphillis AS [RPR/VDRL],'' AS [RPR/VDRL Results],BAC.TreatedForSyphillis AS [Syphilis Treated],BAC.HivStatusBeforeAnc [HIV status before 1st ANC],
		case when encounterone=1 then 'I' when encounterone=2 then 'R' end as [HIV testing] ,HIVTest.OneKitId,HIVTest.OneLotNumber,HIVTest.OneExpiryDate,HIVTest.FinalTestOneResult,
		HIVTest.twokitid,HIVTest.twolotnumber,HIVTest.twoexpirydate,HIVTest.FinalTestTwoResult, z.FinalResult,WHO.[WHOStage],
		j.[On ARV Before 1st ANC Visit], [Started HAART in ANC],[CTX],[AZT for Baby],[NVP for Baby],
		TBScreening,CaCXScreening,OtherConditions,[Deworming],NULL [IPT 1-3],NULL [TT Dose],NULL Supplementation, '' AS [Received ITN],NULL [Additional Treatment],'' [ANC Exercises],
		partnerTesting.[PartnerTested],partnerTesting.[PartnerHIVResult],Refferals.ReferredFrom,Refferals.ReferredTo
		, NULL TCA, '' Remarks
FROM  dbo.mst_Patient a INNER JOIN
    dbo.Patient b ON a.Ptn_Pk = b.ptn_pk INNER JOIN
    dbo.PersonIdentifier c ON b.PersonId = c.PersonId INNER JOIN
    dbo.PatientMasterVisit d ON b.Id = d.PatientId INNER JOIN
    dbo.PatientEncounter e ON b.Id = e.PatientId AND d.Id = e.PatientMasterVisitId INNER JOIN
    dbo.PatientEnrollment f ON b.Id = f.PatientId inner JOIN
    dbo.VisitDetails g ON b.Id = g.PatientId AND d.Id = g.PatientMasterVisitId left JOIN
    dbo.ServiceArea h ON f.ServiceAreaId = h.Id left JOIN
    dbo.Pregnancy I ON b.Id = I.PatientId AND d.Id = i.PatientMasterVisitId --AND g.PregnancyId = i.Id 
	
	LEFT OUTER JOIN
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
    dbo.PatientVitals k ON d.Id = k.PatientMasterVisitId AND b.Id = k.PatientId Left outer join 
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
----------------TB Screening
	(SELECT DISTINCT ps.PatientId,ps.PatientMasterVisitId, lk.ItemName AS TBScreening
	FROM dbo.PatientScreening ps INNER JOIN
	dbo.LookupItemView AS lv ON ps.ScreeningTypeId = lv.masterid inner join
	dbo.LookupItemView AS lk ON ps.ScreeningCategoryId = lk.itemid
	WHERE lv.MasterName LIKE '%TBScreeningPMTCT%') TBScreen on TBScreen.PatientId=b.Id and TBScreen.PatientMasterVisitId=d.Id left outer join
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
	(SELECT DISTINCT d.Id PatientId,PatientMasterVisitId,[ItemName] [Deworming]
	FROM [dbo].[LookupItemView]a inner join [dbo].[PatientPreventiveServices] b on b.[PreventiveServiceid]=a.itemid
	inner join PatientMasterVisit c on c.Id=b.PatientMasterVisitId inner join Patient d on d.id=c.PatientId
	where MasterName ='Dewormed')Dewormed on Dewormed.PatientId=b.Id and Dewormed.PatientMasterVisitId=d.Id left join 
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


