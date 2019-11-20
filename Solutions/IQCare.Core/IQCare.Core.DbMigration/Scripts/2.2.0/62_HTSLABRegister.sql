
ALTER VIEW [dbo].[HTS_LAB_Register]
AS
SELECT distinct P.Id PatientID, p.Ptn_pk AS PatientPK, NULL AS PatientName,
	p.FacilityId FacilityCode, cast(edv.EncounterDate as date) VisitDate, p.dateofbirth AS DOB, DATEdiff(yy, p.dateofbirth, 
	edv.EncounterDate) AS Age,Gender =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = per.sex),  MaritalStatus =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = ms.maritalstatusid and ms.deleteflag=0), e.PersonID,edv.EncounterId HTSEncounterId,edv.TestingStrategy StrategyHTS,
    edv.ServiceEntryPoint TestEntryPoint,edv.TestedAs ClientTestedAs,edv.EverSelfTested clientSelfTestesd ,[PartnerListingConsent],
	[PartnerListingConsentDeclineReason], edv.CoupleDiscordant CoupleDiscordant ,edv.EverTested TestedBefore , edv.MonthsSinceLastTest WhenLastTested,
	da.Disability,CASE pop.PopulationCategory WHEN 'General Population' THEN 'N/A' ELSE PopulationCategory END AS KeyPop,[MonthSinceSelfTest],edv.TestType, 
	one.kitid AS [TestKitName1], one.kitlotnumber AS [TestKitLotNumber1],edv.ResultOne FinalTestOneResult ,
	one.Outcome AS ResultOne, two.Outcome AS ResultTwo, one.expirydate AS [TestKitExpiryDate1], two.kitid AS [TestKitName_2],two.kitlotnumber AS [TestKitLotNumber_2], 
	two.expirydate AS [TestKitExpiryDate_2], edv.ResultTwo FinalResultTestTwo ,edv.FinalResult finalResultHTS , edv.FinalResultGiven FinalResultsGiven ,
	Provider TCAHTS, edv.EncounterRemarks Remarks,TBScreening TBScreeningHTS,edv.Consent Consent , Module=
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = pe.EncounterTypeId)
FROM [HtsEncounter] e 
INNER JOIN [dbo].[PatientEncounter] pe on pe.id=e.PatientEncounterID 
INNER JOIN patient p ON p.personid = e.personid 
INNER JOIN personview per ON per.id = p.personid 
INNER JOIN HTS_EncountersDetailView  EDV on e.PatientEncounterID=edv.PatientEncounterID and  p.id =edv.patientid
LEFT JOIN (SELECT distinct SS.PatientPK,ss.PersonId, STUFF((SELECT '; ' + US.PopulationCategory 
    FROM (select distinct us.PopulationCategory, p.ptn_pk as PatientPK from PatientPopulationView US 
	INNER JOIN patient p on p.personid=us.personid) us 
		WHERE us.PatientPK = SS.PatientPK
		FOR XML PATH('')), 1, 1, '') PopulationCategory
		FROM (select distinct us.PopulationCategory,us.personid, p.ptn_pk as PatientPK from PatientPopulationView US 
	INNER JOIN patient p on p.personid=us.personid) SS
	GROUP BY SS.PatientPK,ss.Personid,  SS.PopulationCategory) pop ON pop.PatientPK = p.ptn_pk 
left join (SELECT distinct SS.Personid, STUFF((SELECT '; ' + US.Disability 
		FROM (select * from (SELECT distinct  personid, l.name Disability
		FROM  [dbo].[ClientDisability] d
		INNER JOIN  [dbo].[LookupItem] l on l.id = d.disabilityid
		where d.deleteflag=0)a) US
		WHERE US.personid = SS.personid
		FOR XML PATH('')), 1, 1, '') Disability
	FROM ((select * from (SELECT distinct  personid, l.name Disability
		FROM  [dbo].[ClientDisability] d
		INNER JOIN  [dbo].[LookupItem] l on l.id = d.disabilityid
		where d.deleteflag=0)a)) SS
	GROUP BY SS.personid, SS.Disability) DA on da.personId=e.personid 
left join [PatientMaritalStatus] ms ON ms.personid = p.personid 
left OUTER JOIN (SELECT distinct  htsencounterid,t.Id as Test1ID, kitid=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = t.KitId), kitlotnumber, expirydate, PersonId, outcome=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = t.Outcome)
				FROM [Testing] t INNER JOIN  [HtsEncounter] e on t.HtsEncounterId=e.id --INNER JOIN  [LookupItem] l on l.id=t.Outcome
				INNER JOIN 	[dbo].[PatientEncounter] pe on pe.id=e.PatientEncounterID  
				INNER JOIN LookupItem c on c.id=pe.EncounterTypeId WHERE t.testround =1 ) one ON one.personid = e.PersonId  and edv.EncounterId=one.HtsEncounterId 
left JOIN (SELECT distinct htsencounterid,t.Id as Test2ID, kitid=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = t.KitId), kitlotnumber, expirydate, PersonId, outcome=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = t.Outcome)
			FROM  [Testing] t INNER JOIN  [HtsEncounter] e on t.HtsEncounterId=e.id 
			INNER JOIN  [dbo].[PatientEncounter] pe on pe.id=e.PatientEncounterID  
			INNER JOIN LookupItem c on c.id=pe.EncounterTypeId 
			WHERE   t.testround =2) two ON two.personid = e.PersonId  and edv.EncounterId=two.HtsEncounterId

where p.DeleteFlag=0 and per.[DeleteFlag]=0


