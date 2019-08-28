/****** Object:  View [dbo].[vw_FamilyTestingRegister]    Script Date: 06/27/2019 11:33:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[vw_FamilyTestingRegister]
AS
SELECT distinct htc.Patientpk Ptn_pk,s.Personid,p.FacilityId FacilityCode,
	htc.PatientID,identify.IdentifierValue [HTS_Number], [ScreeningDate]  AS DateOfScreening, 
	htc.PatientName NameOfIndexClient, plink.CCCNumber CCC#, 
	htc.StrategyHTS AS SourceOfIndexClient, 
	NULL AS NamesOfFamilyMember,
	pat.Gender AS SexOfFamilyMember, 
	cast(month(pat.DateofBirth) as varchar) + '/' + cast(year(pat.DateofBirth) as varchar)  AS MonthAndYearOfBirth, 
	DATEDIFF(YEAR,pat.DateofBirth,[VisitDate]) AS AgeOfFamilyMember, 
	pat.RelationshipType RelationToIndexClient, 
	NULL AS HIVStatus, NULL AS FamilyMemberEligibleForTesting, 
	tracing.BookingDate AS DateContactBookedForTesting, NULL AS DateIndexClientRemindedOfContactTesting, [Trace1],[Trace2],[Trace3], tracingDate AS DateCOntacted, 
	PNSConsent FT_ConsentedTesting, plink.Facility TestingLocation, htc.finalResultHTS AS TestResults, NULL AS LinkedToCare
FROM [dbo].[HTS_LAB_Register] htc 
inner join (SELECT a.Id AS Patientid, a.ptn_pk, b.IdentifierValue
	FROM  dbo.PatientEnrollment AS d INNER JOIN dbo.ServiceArea AS c ON d.ServiceAreaId = c.Id INNER JOIN
	 dbo.Patient AS a INNER JOIN dbo.PatientIdentifier AS b ON a.Id = b.PatientId ON d.Id = b.PatientEnrollmentId
	WHERE (c.Name = 'HTS Module'))identify on identify.patientid=htc.patientid
inner join HTS_EncountersDetailView edv on htc.patientid=edv.patientid 
INNER JOIN HTS_PartnersView pat on pat.patientID=htc.patientID 
left join [dbo].[vw_HIVTesting_Services_Referral_Linkage_Register] link on link.Patientid=htc.patientid INNER JOIN
		patient p ON p.id = htc.patientid 
left join [PatientLinkage] plink on plink.personid=p.personid 
left join (SELECT distinct  a.[PersonId],[PatientId],[PatientMasterVisitId],b.[ScreeningDate],
	ScreeningCategory=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = b.[ScreeningCategoryId]),ScreeningValue=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = b.ScreeningValueId),[Occupation],[BookingDate] ,[Comment]
	FROM [dbo].[HtsScreening]a 
	inner join [dbo].[PatientScreening] b on b.Id=a.PatientScreeningId 
	left join [dbo].[HtsScreeningOptions] c on c.id=a.HtsScreeningOptionsId and a.personid=c.personid)y
	PIVOT (max(y.ScreeningValue) FOR ScreeningCategory IN (PnsRelationship,PnsPhysicallyHurt,PnsThreatenedHurt,
	PnsForcedSexual,IPVOutcome,LivingWithClient,HIVStatus,PNSApproach,EligibleTesting,ScreeningHivStatus) )s on s.PersonId=pat.PersonId
left join (SELECT  * FROM  (SELECT  PersonID,  cast(x.PNSMode AS varchar) + '    ' + CONVERT(varchar(11), x.tracingDate, 106) + '    ' + cast(x.FT_Outcome AS varchar) AS TracesTests, 
	CASE WHEN x.num = '1' THEN 'Trace1' WHEN x.num = '2' THEN 'Trace2' WHEN x.num = '3' THEN 'Trace3' END AS Trace,PNSConsent,BookingDate
	FROM (select cast(Row_Number() OVER (PARTITION BY a.PersonId
	ORDER BY a.tracingDate asc) AS Varchar) AS num, PersonId,tracingDate,FT_Outcome,PNSMode,PNSConsent,BookingDate from(
	SELECT  distinct a.PersonID,[DateTracingDone]tracingDate,FT_Outcome=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = a.Outcome),PNSMode=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = a.Mode),PNSConsent, BookingDate
	FROM [dbo].[Tracing] a inner join
	(select distinct PersonID, max(DateBookedTesting)BookingDate from [dbo].[Tracing]a
	group by PersonID)d on d.PersonID=a.PersonID left join (select distinct PersonID,  max(b.ItemName)PNSConsent from [dbo].[Tracing]a
	left join LookupItemView b on b.ItemId=a.Consent group by PersonID)e on e.PersonID=a.PersonID)a) x) y 
	PIVOT (Max(TracesTests) FOR Trace IN ([Trace1], [Trace2], [Trace3]))S) tracing on tracing.personid=s.personid

	where  pat.RelationshipType in ('Mother' ,'Father','Child','Sibling','Other') 
GO


