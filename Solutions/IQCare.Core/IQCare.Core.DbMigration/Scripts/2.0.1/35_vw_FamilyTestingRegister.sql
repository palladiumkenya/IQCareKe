/****** Object:  View [dbo].[vw_FamilyTestingRegister]    Script Date: 06/27/2019 11:33:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[vw_FamilyTestingRegister]
AS
SELECT distinct htc.Patientpk Ptn_pk,s.Personid,p.FacilityId FacilityCode,htc.PatientID,identify.IdentifierValue [HTS_Number], ScreeningDate  AS DateOfScreening, htc.PatientName NameOfIndexClient, plink.CCCNumber CCC#, 
htc.StrategyHTS AS SourceOfIndexClient, [Name(s)] AS NamesOfFamilyMember,[PNSSex] AS SexOfFamilyMember, 
cast(month(partnerdet.DateofBirth) as varchar) + '/' + cast(year(partnerdet.DateofBirth) as varchar)  AS MonthAndYearOfBirth, 
[AgeYears] AS AgeOfFamilyMember, [RelationsipToIndexClient]AS RelationToIndexClient, NULL AS HIVStatus, NULL AS FamilyMemberEligibleForTesting, 
tracing.BookingDate AS DateContactBookedForTesting, NULL AS DateIndexClientRemindedOfContactTesting, [Trace1],[Trace2],[Trace3], tracingDate AS DateCOntacted, 
PNSConsent FT_ConsentedTesting, plink.Facility TestingLocation, htc.finalResultHTS AS TestResults, NULL AS LinkedToCare
FROM [dbo].[HTS_LAB_Register] htc inner join 
	(SELECT a.Id AS Patientid, a.ptn_pk, b.IdentifierValue
	FROM  dbo.PatientEnrollment AS d INNER JOIN dbo.ServiceArea AS c ON d.ServiceAreaId = c.Id INNER JOIN
	 dbo.Patient AS a INNER JOIN dbo.PatientIdentifier AS b ON a.Id = b.PatientId ON d.Id = b.PatientEnrollmentId
	WHERE (c.Name = 'HTS Module'))identify on identify.patientid=htc.patientid
	 inner join
	HTS_EncountersDetailView edv on htc.patientid=edv.patientid left join 
	[dbo].[vw_HIVTesting_Services_Referral_Linkage_Register] link on link.Patientid=htc.patientid INNER JOIN
	patient p ON p.id = htc.patientid left join 
	PersonLocation ploc ON ploc.personid = p.personid LEFT JOIN
	PersonContact pcon ON pcon.personid = p.personid  left join 
	[PatientLinkage] plink on plink.personid=p.personid left join 
	(SELECT distinct  a.[PersonId],[PatientId],[PatientMasterVisitId],b.[ScreeningDate],
	s.Itemname ScreeningCategory,t.ItemName ScreeningValue,[Occupation],[BookingDate] ,[Comment]
	FROM [dbo].[HtsScreening]a left join 
	[dbo].[PatientScreening] b on b.Id=a.PatientScreeningId left join
	[dbo].[HtsScreeningOptions] c on c.id=a.HtsScreeningOptionsId and a.personid=c.personid
	inner join lookupitemview s on s.itemid=b.[ScreeningCategoryId]
	inner join lookupitemview t on t.itemid=b.ScreeningValueId )y
	PIVOT (max(y.ScreeningValue) FOR ScreeningCategory IN (PnsRelationship,PnsPhysicallyHurt,PnsThreatenedHurt,PnsForcedSexual,IPVOutcome,LivingWithClient,HIVStatus,PNSApproach,EligibleTesting,ScreeningHivStatus) )s on s.[PatientId]=htc.patientid
	left join (SELECT  * FROM  (SELECT  PersonID,  cast(x.PNSMode AS varchar) + '    ' + CONVERT(varchar(11), x.tracingDate, 106) + '    ' + cast(x.FT_Outcome AS varchar) AS TracesTests, 
	CASE WHEN x.num = '1' THEN 'Trace1' WHEN x.num = '2' THEN 'Trace2' WHEN x.num = '3' THEN 'Trace3' END AS Trace,PNSConsent,BookingDate
	FROM (select cast(Row_Number() OVER (PARTITION BY a.PersonId
	ORDER BY a.tracingDate asc) AS Varchar) AS num, PersonId,tracingDate,FT_Outcome,PNSMode,PNSConsent,BookingDate from(
	SELECT  distinct a.PersonID,[DateTracingDone]tracingDate, md.ItemName FT_Outcome, md1.ItemName PNSMode,PNSConsent, BookingDate
	FROM [dbo].[Tracing] a left JOIN LookupItemView md ON md.itemID = a.Outcome left JOIN
	LookupItemView md1 ON md1.itemID = a.Mode left join LookupItemView b on b.ItemId=a.Consent inner join
	(select distinct PersonID, max(DateBookedTesting)BookingDate from [dbo].[Tracing]a
	group by PersonID)d on d.PersonID=a.PersonID left join (select distinct PersonID,  max(b.ItemName)PNSConsent from [dbo].[Tracing]a
	left join LookupItemView b on b.ItemId=a.Consent group by PersonID)e on e.PersonID=a.PersonID)a) x) y 
	PIVOT (Max(TracesTests) FOR Trace IN ([Trace1], [Trace2], [Trace3]))S) tracing on tracing.personid=s.personid
	inner join 

	   (select distinct a.Patientid,PatientPk, b.PersonId,CONVERT(varchar(50), decryptbykey(c.firstname)) + ' ' + CONVERT(varchar(50), decryptbykey(c.midname)) + ' ' + CONVERT(varchar(50), decryptbykey(c.lastname)) AS [Name(s)],c.DateofBirth,
	DATEDIFF(mm,c.DateofBirth,getdate())/12 [AgeYears], s.ItemName [PNSSex],t.ItemName [RelationsipToIndexClient],CONVERT(varchar(50), decryptbykey([MobileNumber])) [CellPhoneNo] from [dbo].[HTS_LAB_Register] a inner join 
	[dbo].[PersonRelationship]b on b.patientid=a.patientid inner join 
	[dbo].person c on c.id =b.personid left join [dbo].[PersonContact]cont on c.id=cont.Personid
	left join LookupItemView s on s.ItemId=c.sex
	inner join LookupItemView t on t.itemid=b.[RelationshipTypeId] where t.itemname in ('Mother' ,'Father','Child','Sibling','Other') and t.mastername='Relationship') partnerdet on partnerdet.personid=s.personid
	
GO


