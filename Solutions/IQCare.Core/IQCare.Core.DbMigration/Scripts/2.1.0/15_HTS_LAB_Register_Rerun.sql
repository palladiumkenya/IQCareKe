ALTER VIEW [dbo].[HTS_LAB_Register]
AS
SELECT distinct P.Id PatientID, p.Ptn_pk AS PatientPK, 
CONVERT(varchar(50), decryptbykey(Per.firstname)) + ' ' + CONVERT(varchar(50), 
decryptbykey(Per.middlename)) + ' ' + CONVERT(varchar(50), decryptbykey(Per.lastname)) AS PatientName,
p.FacilityId FacilityCode, cast(PE.EncounterStartTime as date) VisitDate, p.dateofbirth AS DOB, DATEdiff(yy, p.dateofbirth, 
PE.EncounterStartTime) AS Age,sex.Sex Gender,ms.MaritalStatus,e.PersonID,t.HTSEncounterId,ts.StrategyHTS,tep.TestEntryPoint,
case WHEN TA.ItemName='C: Couple (includes polygamous)' THEN 'Couple' WHEN TA.ItemName='I: Individual' THEN 'Individual'
ELSE NULL end as ClientTestedAs,est.clientSelfTestesd,cd.CoupleDiscordant,ET.TestedBefore,
[MonthsSinceLastTest] WhenLastTested,da.Disability,CASE pop.PopulationCategory WHEN 'General Population' THEN 'N/A' ELSE PopulationCategory END AS KeyPop,
[MonthSinceSelfTest],CASE EncounterType WHEN 1 THEN 'Initial Test' WHEN 2 THEN 'Repeat Test' END  AS TestType,
one.kitid AS [TestKitName1], one.kitlotnumber AS [TestKitLotNumber1],ROTR.FinalTestOneResult,
one.Outcome AS ResultOne, two.Outcome AS ResultTwo, 
one.expirydate AS [TestKitExpiryDate1], two.kitid AS [TestKitName_2],two.kitlotnumber AS [TestKitLotNumber_2], 
two.expirydate AS [TestKitExpiryDate_2],RTTR.FinalResultTestTwo,
FR.ItemName finalResultHTS,FRG.ItemName FinalResultsGiven,
s.UserName TCAHTS,EncounterRemarks Remarks,screen.TBScreening AS TBScreeningHTS,CASE consent.consent WHEN 'Yes' THEN 1 ELSE 0 END as Consent,
encounter.ItemName Module
FROM  [Testing] t INNER JOIN
[HtsEncounter] e ON t.htsencounterid = e.id 
inner join [dbo].[HtsEncounterResult] HER ON her.HtsEncounterId = E.Id
inner join [dbo].[LookupItemView]FR on  FR.ItemId = her.FinalResult
inner join [dbo].[LookupItemView]FRG on  FRG.ItemId = e.FinalResultGiven
inner join  [dbo].[PatientEncounter] pe on pe.id=e.PatientEncounterID 
inner join lookupitemview Encounter on Encounter.itemid=pe.EncounterTypeId
INNER JOIN patient p ON p.id = pe.patientid 
INNER JOIN personview per ON per.id = p.personid 
INNER JOIN [PatientMasterVisit] PM ON PM.Id = PE.PatientMasterVisitId 
inner join mst_User s on s.Userid=e.ProviderId
inner join lookupitemview c on c.itemid=pe.EncounterTypeId
LEFT JOIN (SELECT distinct SS.PatientPK,ss.PersonId, STUFF((SELECT '; ' + US.PopulationCategory 
    FROM PatientPopulationView US
		WHERE US.PatientPK = SS.PatientPK
		FOR XML PATH('')), 1, 1, '') PopulationCategory
		FROM PatientPopulationView SS
	GROUP BY SS.PatientPK,ss.Personid,  SS.PopulationCategory) pop ON pop.PatientPK = p.ptn_pk 
left join (SELECT distinct SS.Personid, STUFF((SELECT '; ' + US.Disability 
		FROM (select * from (SELECT distinct  personid, l.itemname Disability
		FROM  [dbo].[ClientDisability] d
		inner join  [dbo].[LookupItemView] l on l.itemid = d.disabilityid)a) US
		WHERE US.personid = SS.personid
		FOR XML PATH('')), 1, 1, '') Disability
	FROM ((select * from (SELECT distinct  personid, l.itemname Disability
		FROM  [dbo].[ClientDisability] d
		inner join  [dbo].[LookupItemView] l on l.itemid = d.disabilityid)a)) SS
	GROUP BY SS.personid, SS.Disability) DA on da.personId=e.personid 
left join (select distinct HtsEncounterID,ROTR.Itemname as [FinalTestOneResult] from  [dbo].[HtsEncounterResult] HER inner join
	[dbo].[LookupItemView]ROTR on ROTR.ItemId = her.RoundOneTestResult)ROTR on ROTR.HtsEncounterID = E.ID
left join (select distinct HtsEncounterID,RTTR.Itemname as FinalResultTestTwo from  [dbo].[HtsEncounterResult] HER inner join
	[dbo].[LookupItemView]RTTR on RTTR.ItemId = her.RoundTwoTestResult)RTTR on RTTR.HtsEncounterID =  E.ID
left join (select distinct e.ID HtsEncounterID,Ts.Itemname as StrategyHTS from  [dbo].[HtsEncounter] e inner join
	[dbo].[LookupItemView]Ts on  ts.ItemId = e.TestingStrategy)Ts on  ts.HtsEncounterID = e.ID
left join (select distinct e.ID HtsEncounterID,tep.Itemname as TestEntryPoint from  [dbo].[HtsEncounter] e inner join
	[dbo].[LookupItemView]TEP on  tep.ItemId = e.TestEntryPoint)TEP on  tep.HtsEncounterID = e.ID
left join (select distinct e.ID HtsEncounterID,TA.Itemname as ItemName from  [dbo].[HtsEncounter] e inner join
	[dbo].[LookupItemView]TA on  TA.ItemId = e.TestedAs)TA on  TA.HtsEncounterID = e.ID
left join (select distinct a.PersonId,Itemname Sex from 
(select a.ID PersonId,max([Sex])Sex, max(a.CreateDate)CreateDate 
from person a group by a.CreateDate,a.ID )a
	inner join [dbo].[LookupItemView] on   ItemId = a.sex) sex on sex.PersonId=e.PersonId
left join (SELECT  distinct PatientId,PatientmasterVisitID,cV.ItemName Consent
	 FROM PatientConsent PC inner join 
	 LookupItemView CV on pc.consentvalue= cv.ItemId
	 inner join  LookupItemView CT on pc.ConsentType= ct.ItemId
	 WHERE ct.ItemName = 'ConsentToBeTested')consent on consent.patientid=p.Id and PM.Id = consent.PatientMasterVisitId 
left Join (select distinct PersonId,ItemName MaritalStatus from (SELECT distinct [PersonId],max([MaritalStatusId])MaritalStatusId
	FROM [dbo].[PatientMaritalStatus] where active =1 group by personid )pms
inner join lookupitemview lkup on lkup.itemid=pms.MaritalStatusId)ms on ms.PersonId=e.personID
left join (select distinct e.ID HtsEncounterID,EST.Itemname as clientSelfTestesd from  [dbo].[HtsEncounter] e inner join
	[dbo].[LookupItemView]EST on  EST.ItemId = e.EverSelfTested)EST on  EST.HtsEncounterID = e.ID
left join (select distinct e.ID HtsEncounterID,ET.Itemname as TestedBefore from  [dbo].[HtsEncounter] e inner join
	[dbo].[LookupItemView]ET on  ET.ItemId = e.evertested)ET on  ET.HtsEncounterID = e.ID
left join (select distinct e.ID HtsEncounterID,CD.Itemname as CoupleDiscordant from  [dbo].[HtsEncounter] e inner join
	[dbo].[LookupItemView]CD on  CD.ItemId = e.CoupleDiscordant)CD on  CD.HtsEncounterID = e.ID
left OUTER JOIN (SELECT distinct  htsencounterid,t.Id as Test1ID, b.ItemName kitid, kitlotnumber, expirydate, PersonId, l.ItemName AS outcome
	FROM [Testing] t inner join  [HtsEncounter] e on t.HtsEncounterId=e.id inner join  [LookupItemView] l on l.itemid=t.Outcome
	inner join lookupitemview b on b.itemid=t.KitId inner join  [dbo].[PatientEncounter] pe on pe.id=e.PatientEncounterID  
	inner join lookupitemview c on c.itemid=pe.EncounterTypeId WHERE t.testround =1 ) one ON one.personid = e.PersonId  and t.HtsEncounterId=one.HtsEncounterId 
left JOIN (SELECT distinct htsencounterid,t.Id as Test2ID, b.ItemName kitid, kitlotnumber, expirydate, PersonId, l.ItemName AS outcome
	FROM  [Testing] t inner join  [HtsEncounter] e on t.HtsEncounterId=e.id inner join  [LookupItemView] l on l.itemid=t.Outcome
	inner join lookupitemview b on b.itemid=t.KitId inner join  [dbo].[PatientEncounter] pe on pe.id=e.PatientEncounterID  
	inner join lookupitemview c on c.itemid=pe.EncounterTypeId 
	WHERE   t.testround =2) two ON two.personid = e.PersonId  and t.HtsEncounterId=two.HtsEncounterId
left join (SELECT DISTINCT b.PatientId,a.Personid,c.Id HTSEncounterId, max(lv.ItemName) AS TBScreening 
FROM dbo.Patient AS a INNER JOIN
dbo.PatientEncounter AS b ON a.Id = b.PatientId INNER JOIN dbo.HtsEncounter AS c ON a.PersonId = c.PersonId INNER JOIN
dbo.PatientScreening AS ps ON a.Id = ps.PatientId INNER JOIN dbo.LookupItemView AS lv  ON ps.ScreeningValueId = lv.itemid
WHERE lv.MasterName LIKE '%TbScreening%' group by b.PatientId,a.Personid,c.Id)screen on screen.personid=e.PersonID and 
screen.HTSEncounterId=t.HTSEncounterId

