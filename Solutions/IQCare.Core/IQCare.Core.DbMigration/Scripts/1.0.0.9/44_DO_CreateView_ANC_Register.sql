IF OBJECT_ID('dbo.ANC Register', 'V') IS NOT NULL
    DROP VIEW [dbo].[ANC Register]
GO
CREATE VIEW [dbo].[ANC Register]
AS
SELECT  a.Ptn_Pk,b.Id, a.FirstName, a.MiddleName, a.LastName, a.DOB, a.Sex, a.MaritalStatus, a.Phone, 
        d.VisitDate, g.VisitType, c.IdentifierValue AS ANC_Number, h.Name AS ServiceArea, b.FacilityId, g.VisitNumber, 
        i.Parity, i.Gravidae, a.Landmark, a.VillageName, i.LMP, i.EDD EDC, k.Weight, k.Height, 
        k.BPSystolic, k.BPDiastolic, k.Muac,HIVTest.OneKitId,HIVTest.OneLotNumber,HIVTest.OneExpiryDate,HIVTest.FinalTestOneResult,
		HIVTest.twokitid,HIVTest.twolotnumber,HIVTest.twoexpirydate,HIVTest.FinalTestTwoResult,
		lkup1.itemName [On ARV Before 1st ANC Visit], [Started HAART in ANC],[CTX],[AZT for Baby],[NVP for Baby],
		TBScreening,CaCXScreening,OtherConditions,[Deworming],Refferals.ReferredFrom,Refferals.ReferredTo
FROM  dbo.mst_Patient a INNER JOIN
    dbo.Patient b ON a.Ptn_Pk = b.ptn_pk INNER JOIN
    dbo.PatientIdentifier c ON b.Id = c.PatientId INNER JOIN
    dbo.PatientMasterVisit d ON b.Id = d.PatientId INNER JOIN
    dbo.PatientEncounter e ON b.Id = e.PatientId AND d.Id = e.PatientMasterVisitId INNER JOIN
    dbo.PatientEnrollment f ON c.PatientEnrollmentId = f.Id INNER JOIN
    dbo.PatientProfile g ON b.Id = g.PatientId AND d.Id = g.PatientMasterVisitId INNER JOIN
    dbo.ServiceArea h ON f.ServiceAreaId = h.Id INNER JOIN
    dbo.Pregnancy I ON b.Id = I.PatientId AND d.Id = i.PatientMasterVisitId AND g.PregnancyId = i.Id LEFT OUTER JOIN
    dbo.PatientDrugAdministration j ON b.Id = j.PatientId AND d.Id = j.PatientMasterVisitId  Left outer join 
	dbo.LookupItemView lkup1 on lkup1.ItemId=j.Value LEFT OUTER JOIN
    dbo.PatientVitals k ON d.Id = k.PatientMasterVisitId AND b.Id = k.PatientId Left outer join 
	dbo.LookupItemView lkup2 on lkup2.ItemId=j.Value left outer join 
	------------------HIV tests
	(SELECT DISTINCT e.PersonId, one.kitid AS OneKitId, one.kitlotNumber AS OneLotNumber, one.Outcome AS FinalTestOneResult, 
	two.Outcome AS FinalTestTwoResult, one.expirydate AS OneExpiryDate, two.kitid AS twokitid, 
	two.kitlotnumber AS twolotnumber, two.expirydate AS twoexpirydate
	FROM  Testing t INNER JOIN [HtsEncounter] e ON t .htsencounterid = e.id FULL OUTER JOIN
	(SELECT distinct  htsencounterid, b.ItemName kitid, kitlotnumber, expirydate, PersonId, l.ItemName AS outcome
	FROM [Testing] t inner join  [HtsEncounter] e on t.HtsEncounterId=e.id inner join  [LookupItemView] l on l.itemid=t.Outcome
	inner join lookupitemview b on b.itemid=t.KitId WHERE  e.encountertype = 1 and t.testround =1) one ON one.personid = e.PersonId FULL OUTER JOIN
	(SELECT distinct htsencounterid, b.ItemName kitid, kitlotnumber, expirydate, PersonId, l.ItemName AS outcome
	FROM  [Testing] t inner join  [HtsEncounter] e on t.HtsEncounterId=e.id inner join  [LookupItemView] l on l.itemid=t.Outcome
	inner join lookupitemview b on b.itemid=t.KitId WHERE  t.testround =2) two ON two.personid = e.PersonId )HIVTest on HIVTest.PersonId=b.PersonId  left outer join 
	(SELECT DISTINCT b.PatientId,b.PatientMasterVisitId,[ItemName] [Started HAART in ANC]
	FROM [dbo].[LookupItemView]a inner join dbo.PatientDrugAdministration b on b.value=a.itemid
	where MasterName ='Started HAART at Service Point')HAARTANC on HAARTANC.PatientId=b.Id and HAARTANC.PatientMasterVisitId=d.Id left outer join 
	(SELECT DISTINCT b.PatientId,b.PatientMasterVisitId,[ItemName] [CTX]
	FROM [dbo].[LookupItemView]a inner join dbo.PatientDrugAdministration b on b.value=a.itemid
	where MasterName ='CTX at Service Point')CTX on CTX.PatientId=b.Id and CTX.PatientMasterVisitId=d.Id Left Outer join
	(SELECT DISTINCT b.PatientId,b.PatientMasterVisitId,[ItemName] [AZT for Baby]
	FROM [dbo].[LookupItemView]a inner join dbo.PatientDrugAdministration b on b.value=a.itemid
	where MasterName ='AZT for Baby')AZTBaby on AZTBaby.PatientId=b.Id and AZTBaby.PatientMasterVisitId=d.Id left outer join 
	(SELECT DISTINCT b.PatientId,b.PatientMasterVisitId,[ItemName] [NVP for Baby]
	FROM [dbo].[LookupItemView]a inner join dbo.PatientDrugAdministration b on b.value=a.itemid
	where MasterName ='NVP for Baby')NVPBaby on NVPBaby.PatientId=b.Id and NVPBaby.PatientMasterVisitId=d.Id left outer join 
----------------TB Screening
	(SELECT DISTINCT b.PatientId,b.PatientMasterVisitId, lv.ItemName AS TBScreening
	FROM  dbo.Patient AS a INNER JOIN
	dbo.PatientEncounter AS b ON a.Id = b.PatientId INNER JOIN
	dbo.Patientprofile AS c ON a.Id = c.PatientId INNER JOIN
	dbo.PatientScreening AS ps ON a.Id = ps.PatientId INNER JOIN
	dbo.LookupItemView AS lv ON ps.ScreeningValueId = lv.itemid
	WHERE lv.MasterName LIKE '%TBScreeningPMTCT%') TBScreen on TBScreen.PatientId=b.Id and TBScreen.PatientMasterVisitId=d.Id left outer join
----------------CaCX Screening
	(SELECT DISTINCT b.PatientId,b.PatientMasterVisitId, lv.ItemName AS CaCXScreening
	FROM  dbo.Patient AS a INNER JOIN
	dbo.PatientEncounter AS b ON a.Id = b.PatientId INNER JOIN
	dbo.Patientprofile AS c ON a.Id = c.PatientId INNER JOIN
	dbo.PatientScreening AS ps ON a.Id = ps.PatientId INNER JOIN
	dbo.LookupItemView AS lv ON ps.ScreeningValueId = lv.itemid
	WHERE lv.MasterName = 'CaCxScreening')CaCX on CaCX.PatientId=b.Id and CaCX.PatientMasterVisitId=d.Id left Outer join
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
	where MasterName ='Dewormed')Dewormed on Dewormed.PatientId=b.Id and Dewormed.PatientMasterVisitId=d.Id left outer join
	(SELECT        dbo.PMTCTReferral.PatientId, dbo.PMTCTReferral.PatientMasterVisitId, dbo.PatientMasterVisit.VisitDate, dbo.PMTCTReferral.ReferredFrom, dbo.PMTCTReferral.ReferredTo
	FROM            dbo.PMTCTReferral INNER JOIN
							 dbo.PatientMasterVisit ON dbo.PMTCTReferral.PatientMasterVisitId = dbo.PatientMasterVisit.Id INNER JOIN
							 dbo.PatientEncounter ON dbo.PatientMasterVisit.Id = dbo.PatientEncounter.PatientMasterVisitId LEFT OUTER JOIN
							 dbo.LookupItemView ON dbo.LookupItemView.ItemId = dbo.PatientEncounter.EncounterTypeId
	WHERE        (dbo.LookupItemView.ItemName = 'ANC-Encounter'))Refferals on Refferals.PatientId=b.Id and Refferals.PatientMasterVisitId=d.id
WHERE        (h.Name = 'ANC')


GO
