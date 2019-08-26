

/****** Object:  View [dbo].[vw_PartnerNotificationRegister]    Script Date: 08/19/2019 01:09:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*WHERE
	 info.RelationshipType IN(SELECT Id FROM mst_RelationshipType WHERE Name='Spouse/Partner')*/
ALTER VIEW [dbo].[vw_PartnerNotificationRegister]
AS
SELECT  distinct htc.Patientpk Ptn_pk,
		htc.patientID,
		s.Personid,
		NULL as ID,
		NULL [HTS_Number],
		ScreeningDate [Date],
		htc.PatientName [Name],
		DATEDIFF(year,htc.DOB,getdate()) Age,
		htc.Gender [Sex],
		htc.MaritalStatus [MaritalStatus],
		htc.KeyPop [KeyPopulation],
		htc.StrategyHTS [Modality],
		ploc.[landmark] [PhysicalLocation],
		NULL [CellPhone],
		case when plink.CCCNumber is not null then 'Yes' else 'No' end as [EnrolledIntoCare],
		plink.CCCNumber ,
		plink.Facility [FacilityEnrolledIn],
		htc.PartnerListingConsent [PNSAccepted],
		htc.[PartnerListingConsentDeclineReason] [PNSReasonNotAccepted],
		NULL [Name(s)],--partner
		DATEDIFF(YEAR,pat.DateofBirth,[VisitDate])[AgeYears],--partner
		pat.Gender [PNSSex],--partner
		pat.RelationshipType  [RelationsipToIndexClient],--partner
		NULL [Occupation],--partner
		case when IPVOutcome is null --and [RelationsipToIndexClient]='Child'  
		then 'NA' when IPVOutcome is not null then 'Yes' else 'No' end as ScreenedforIPV,
		PnsPhysicallyHurt  [IPVQuestionOne],
		PnsThreatenedHurt  [IPVQuestionTwo],
		PnsForcedSexual  [PNSQuestionThree],
		IPVOutcome  [IPVScreeningOutcome],
		NULL [CellPhoneNo],--partner
		LivingWithClient  [CurrentlyLivingWithIndexClient],
		HIVStatus  [KnowledgeOfHIVStatus],--partner
		PNSApproach  [PNSApproach],
		tracingDate  [TracingDate],--partner
		PNSConsent  [ConsentedTesting],--partner
		tracing.BookingDate  [DateBookedForTesting],--partner
		[Trace1],
		[Trace2],
		[Trace3],
		NULL  [DateHIVTesting],--partner
		NULL  [Tested],--partner
		NULL  [HIVTestOutCome],--partner
		NULL  [Linked],--partner
		NULL  [FacilityLinkedTo],--partner
		NULL  [DateLinkedToCare],--partner
		NULL [CCCNo]--partner	
FROM [dbo].[HTS_LAB_Register] htc 
INNER JOIN patient p ON p.id = htc.patientid  
inner join HTS_PartnersView pat on pat.patientID=htc.patientID
LEFT JOIN [dbo].[vw_HIVTesting_Services_Referral_Linkage_Register] link on link.Patientid=htc.patientid 
LEFT JOIN PersonLocation ploc ON ploc.personid = p.personid  
LEFT JOIN [PatientLinkage] plink on plink.personid=p.personid 
LEFT JOIN (SELECT distinct  a.[PersonId],[PatientId],[PatientMasterVisitId],b.[ScreeningDate],ScreeningCategory=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = b.[ScreeningCategoryId]),ScreeningValue=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = b.ScreeningValueId),[Occupation],[BookingDate] ,[Comment]
			FROM [dbo].[HtsScreening]a 
			inner join [dbo].[PatientScreening] b on b.Id=a.PatientScreeningId 
			left join [dbo].[HtsScreeningOptions] c on c.id=a.HtsScreeningOptionsId and a.personid=c.personid)y
			PIVOT (max(y.ScreeningValue) FOR ScreeningCategory IN (PnsRelationship,PnsPhysicallyHurt,PnsThreatenedHurt,PnsForcedSexual,
			IPVOutcome,LivingWithClient,HIVStatus,PNSApproach,EligibleTesting,ScreeningHivStatus) )s on s.PersonId=pat.PersonId
LEFT JOIN (SELECT  * FROM  (SELECT  PersonID,  cast(x.PNSMode AS varchar) + '    ' + CONVERT(varchar(11), x.tracingDate, 106) + '    ' + cast(x.FT_Outcome AS varchar) AS TracesTests, 
				CASE WHEN x.num = '1' THEN 'Trace1' WHEN x.num = '2' THEN 'Trace2' WHEN x.num = '3' THEN 'Trace3' END AS Trace,PNSConsent,BookingDate
			FROM (select cast(Row_Number() OVER (PARTITION BY a.PersonId
				ORDER BY a.tracingDate asc) AS Varchar) AS num, PersonId,tracingDate,FT_Outcome,PNSMode,PNSConsent,BookingDate from(
				SELECT  distinct a.PersonID,[DateTracingDone]tracingDate, FT_Outcome=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = a.Outcome),PNSMode=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = a.Mode),PNSConsent, BookingDate
				FROM [dbo].[Tracing] a 
				inner join(select distinct PersonID, max(DateBookedTesting)BookingDate from [dbo].[Tracing]a
				group by PersonID)d on d.PersonID=a.PersonID left join (select distinct PersonID,  max(b.ItemName)PNSConsent from [dbo].[Tracing]a
				left join LookupItemView b on b.ItemId=a.Consent group by PersonID)e on e.PersonID=a.PersonID)a) x) y 
				PIVOT (Max(TracesTests) FOR Trace IN ([Trace1], [Trace2], [Trace3]))S) tracing on tracing.personid=s.personid
where htc.PartnerListingConsent='Yes' and pat.RelationshipType in ('Co-Wife' ,'Partner','Spouse')
--GO


