
/****** Object:  View [dbo].[PatientPopulationView]    Script Date: 12/18/2018 09:02:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[PatientPopulationView]
AS
     SELECT DISTINCT
            b.ptn_pk AS PatientPK,
            CASE
                WHEN a.PopulationType = 'General Population'
                THEN 'General Population'
                WHEN a.PopulationType = 'Key Population'
                THEN case when c.ItemName in ('SW','PWID','FSW','MSM','Other')then c.ItemName  else 'Other' end 
            END AS PopulationCategory
     FROM dbo.PatientPopulation AS a
          INNER JOIN dbo.Patient AS b ON a.PersonId = b.PersonId
          LEFT OUTER JOIN dbo.LookupItemView AS c ON a.PopulationCategory = c.ItemId
     WHERE(a.DeleteFlag = 0);

GO

/****** Object:  View [dbo].[PatientPersonView]    Script Date: 12/18/2018 09:02:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[PatientPersonView]
AS

Select A.Id
	, A.PersonId
	, A.ptn_pk
	, A.PatientIndex
	,(Select Top 1 Name From Lookupitem Where Id = A.PatientType) PatientTypeName
	,A.PatientType
	,A.FacilityId
	  ,cast(decryptbykey(FirstName) As varchar(50)) As FirstName
	  ,cast(decryptbykey(MidName) As varchar(50)) As MiddleName
	  ,cast(decryptbykey(LastName) As varchar(50)) As LastName
	  ,(Select Top 1 Name From Lookupitem Where Id = B.Sex)  SexName
	  , B.Sex
	  ,A.Active
	  ,A.DeleteFlag
	  ,A.CreateDate
	  ,A.CreatedBy
	  ,cast (A.AuditData As varchar(max))AuditData
	  ,Isnull(A.DateOfBirth,B.DateOfBirth) DateOfBirth
	  ,Isnull(A.DobPrecision,B.DobPrecision) DobPrecision
	  ,cast(decryptbykey(A.NationalId) As varchar(50)) As NationalId
	  ,A.RegistrationDate
From Patient A inner join dbo.Person B On A.PersonId=B.Id

GO


/****** Object:  View [dbo].[HTS_LAB_Register]    Script Date: 11/13/2018 08:07:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Alter VIEW [dbo].[HTS_LAB_Register]
AS
SELECT DISTINCT ISNULL(ROW_NUMBER() OVER (ORDER BY PE.Id ASC), - 1) AS RowID, P.Id PatientID, p.Ptn_pk AS PatientPK, CONVERT(varchar(50), decryptbykey(Per.firstname)) + ' ' + CONVERT(varchar(50), 
decryptbykey(Per.middlename)) + ' ' + CONVERT(varchar(50), decryptbykey(Per.lastname)) AS PatientName,p.FacilityId FacilityCode, PE.EncounterStartTime VisitDate, p.dateofbirth AS DOB, DATEdiff(yy, p.dateofbirth, PE.EncounterStartTime) AS Age, 
Gender =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = per.sex), ISNULL(CAST((CASE HE.EncounterType WHEN 1 THEN 'Initial Test' WHEN 2 THEN 'Repeat Test' END) AS VARCHAR(50)), 'Initial') AS TestType, clientSelfTestesd =
    (SELECT        TOP 1 CASE ItemName WHEN 'Yes' THEN 'Y' WHEN 'NO' THEN 'N' ELSE NULL END
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = he.EverSelfTested), StrategyHTS =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = he.TestEntryPoint), ClientTestedAs =
    (SELECT        TOP 1 CASE ItemName WHEN 'C: Couple (includes polygamous)' THEN 'Couple' ELSE 'Individual' END
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = he.TestedAs), CoupleDiscordant =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = he.CoupleDiscordant), TestedBefore =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = he.evertested), MonthsSinceLastTest WhenLastTested, MaritalStatus =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = ms.maritalstatusid), kits.onekitid AS TestKitName1, kits.onelotnumber AS TestKitLotNumber1, kits.oneexpirydate AS TestKitExpiryDate1, ResultOne =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = her.RoundOneTestResult), kits.twokitid AS TestKitName_2, kits.twolotnumber AS TestKitLotNumber_2, kits.twoexpirydate AS TestKitExpiryDate_2, CASE WHEN dis.itemname IS NULL 
THEN 'NA' ELSE dis.itemname END AS Disability, kits.FinalTestOneResult, kits.FinalTestTwoResult AS FinalResultTestTwo, ResultTwo =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = her.RoundTwoTestResult), finalResultHTS =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = her.FinalResult), FinalResultsGiven =
    (SELECT        TOP 1 ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = he.FinalResultGiven), /*Disability =  (SELECT TOP 1 ItemName FROM [dbo].[LookupItemView] WHERE ItemId = dis.disabilityid),*/ Consent =
    (SELECT        TOP 1 CASE ItemName WHEN 'Yes' THEN 1 ELSE 0 END
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId =
                                    (SELECT        TOP 1 ConsentValue
                                      FROM            PatientConsent PC
                                      WHERE        PC.PatientMasterVisitId = PM.Id AND PC.ConsentType =
                                                                    (SELECT        TOP 1 ItemId
                                                                      FROM            LookupItemView
                                                                      WHERE        ItemName = 'ConsentToBeTested'))), he.EncounterRemarks AS Remarks, un.UserName AS TCAHTS, screen.TBScreening AS TBScreeningHTS, 
CASE pop.PopulationCategory WHEN 'General Population' THEN 'N/A' ELSE PopulationCategory END AS KeyPop
FROM            [dbo].[PatientEncounter] PE INNER JOIN
        patient p ON p.id = pe.patientid INNER JOIN
        personview per ON per.id = p.personid LEFT JOIN
        [dbo].[PatientPopulationView] pop ON pop.PatientPK = p.ptn_pk INNER JOIN
        [dbo].[PatientMasterVisit] PM ON PM.Id = PE.PatientMasterVisitId INNER JOIN
        [dbo].[HtsEncounter] HE ON PE.Id = HE.PatientEncounterID INNER JOIN
		(SELECT DISTINCT b.PatientId, d.UserName FROM  dbo.Patient AS a INNER JOIN dbo.PatientEncounter AS b ON a.Id = b.PatientId INNER JOIN
			dbo.HtsEncounter AS c ON a.PersonId = c.PersonId INNER JOIN dbo.mst_User AS d ON b.CreatedBy = d.UserID) UN on un.PatientId=pe.PatientId inner join
        [dbo].[HtsEncounterResult] HER ON HtsEncounterId = HE.Id LEFT JOIN
		(SELECT DISTINCT b.PatientId, lv.ItemName AS TBScreening FROM dbo.Patient AS a INNER JOIN
			dbo.PatientEncounter AS b ON a.Id = b.PatientId INNER JOIN dbo.HtsEncounter AS c ON a.PersonId = c.PersonId INNER JOIN
			dbo.PatientScreening AS ps ON a.Id = ps.PatientId INNER JOIN dbo.LookupItemView AS lv  ON ps.ScreeningValueId = lv.itemid
			WHERE lv.MasterName LIKE '%TbScreening%') screen on screen.patientid=pe.patientid left join
        [PatientMaritalStatus] ms ON ms.personid = p.personid LEFT JOIN
            (SELECT        TOP 1 personid, l.itemname
            FROM            [dbo].[ClientDisability] d, [dbo].[LookupItemView] l
            WHERE        l.itemid = d .disabilityid) dis ON dis.personid = p.personid LEFT JOIN
            (SELECT DISTINCT 
                                        e.personid, one.kitid AS onekitid, one.kitlotnumber AS onelotnumber, one.Outcome AS FinalTestOneResult, two.Outcome AS FinalTestTwoResult, one.expirydate AS oneexpirydate, two.kitid AS twokitid, 
                                        two.kitlotnumber AS twolotnumber, two.expirydate AS twoexpirydate
            FROM            [Testing] t INNER JOIN
                                        [HtsEncounter] e ON t .htsencounterid = e.id FULL OUTER JOIN
                                            (SELECT distinct  htsencounterid, b.ItemName kitid, kitlotnumber, expirydate, PersonId, l.ItemName AS outcome
											FROM [Testing] t inner join  [HtsEncounter] e on t.HtsEncounterId=e.id inner join  [LookupItemView] l on l.itemid=t.Outcome
											inner join lookupitemview b on b.itemid=t.KitId WHERE  e.encountertype = 1 and t.testround =1) one ON one.personid = e.PersonId FULL OUTER JOIN
                                            (SELECT distinct htsencounterid, b.ItemName kitid, kitlotnumber, expirydate, PersonId, l.ItemName AS outcome
											FROM  [Testing] t inner join  [HtsEncounter] e on t.HtsEncounterId=e.id inner join  [LookupItemView] l on l.itemid=t.Outcome
											inner join lookupitemview b on b.itemid=t.KitId WHERE   t.testround =2) two ON two.personid = e.PersonId) kits ON kits.personid = p.personid


GO

/****** Object:  View [dbo].[vw_FamilyTestingRegister]    Script Date: 11/13/2018 08:08:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP VIEW [dbo].[vw_FamilyTestingRegister]
GO


CREATE VIEW [dbo].[vw_FamilyTestingRegister]
AS
SELECT         distinct htc.Patientpk Ptn_pk,p.FacilityId FacilityCode,htc.patientID,identify.IdentifierValue [HTS_Number], htc.VisitDate  AS DateOfScreening, htc.PatientName NameOfIndexClient, plink.CCCNumber CCC#, 
htc.StrategyHTS AS SourceOfIndexClient, [Name(s)] AS NamesOfFamilyMember,[PNSSex] AS SexOfFamilyMember, 
cast(month(partnerdet.DateofBirth) as varchar) + '/' + cast(year(partnerdet.DateofBirth) as varchar)  AS MonthAndYearOfBirth, 
[AgeYears] AS AgeOfFamilyMember, [RelationsipToIndexClient]AS RelationToIndexClient, NULL AS HIVStatus, NULL AS FamilyMemberEligibleForTesting, 
tracing.BookingDate AS DateContactBookedForTesting, NULL AS DateIndexClientRemindedOfContactTesting, [Trace1],[Trace2],[Trace3], tracingDate AS DateCOntacted, 
PNSConsent FT_ConsentedTesting, plink.Facility TestingLocation, htc.finalResultHTS AS TestResults, plink.CCCNumber AS LinkedToCare
FROM [dbo].[HTS_LAB_Register] htc inner join 
(SELECT a.Id AS Patientid, a.ptn_pk, b.IdentifierValue
FROM  dbo.PatientEnrollment AS d INNER JOIN dbo.ServiceArea AS c ON d.ServiceAreaId = c.Id INNER JOIN
 dbo.Patient AS a INNER JOIN dbo.PatientIdentifier AS b ON a.Id = b.PatientId ON d.Id = b.PatientEnrollmentId
WHERE (c.Name = 'HTS Module'))identify on identify.patientid=htc.patientid inner join
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
left join 

   (select distinct a.Patientid,PatientPk, b.PersonId,CONVERT(varchar(50), decryptbykey(c.firstname)) + ' ' + CONVERT(varchar(50), decryptbykey(c.midname)) + ' ' + CONVERT(varchar(50), decryptbykey(c.lastname)) AS [Name(s)],c.DateofBirth,
DATEDIFF(mm,c.DateofBirth,getdate())/12 [AgeYears], s.ItemName [PNSSex],t.ItemName [RelationsipToIndexClient],CONVERT(varchar(50), decryptbykey([MobileNumber])) [CellPhoneNo] from [dbo].[HTS_LAB_Register] a inner join 
[dbo].[PersonRelationship]b on b.patientid=a.patientid inner join 
[dbo].person c on c.id =b.personid left join [dbo].[PersonContact]cont on c.id=cont.Personid
left join LookupItemView s on s.ItemId=c.sex
inner join LookupItemView t on t.itemid=b.[RelationshipTypeId]) partnerdet on partnerdet.personid=s.personid


GO


/****** Object:  View [dbo].[vw_HIVTesting_Services_Referral_Linkage_Register]    Script Date: 11/13/2018 08:09:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[vw_HIVTesting_Services_Referral_Linkage_Register]
AS
SELECT DISTINCT ISNULL(ROW_NUMBER() OVER (ORDER BY PE.Id ASC), - 1) AS RowID, he.id, p.FacilityID FacilityCode,firstname + ' ' + MiddleName + ' ' + lastname AS PatientName, PE.PatientId, p.ptn_pk AS Ptn_pk, p.dateofbirth, DATEdiff(yy, p.dateofbirth, 
PE.EncounterStartTime) AS Age, Gender =
    (SELECT DISTINCT ItemName
      FROM            [dbo].[LookupItemView]
      WHERE        ItemId = per.sex), PE.EncounterStartTime Date, ploc.LandMark AS landmark, pcon.MobileNumber AS PhoneNumber, link.Facility AS FacilityName, NULL AS Occupation, NULL AS IndexClientType, 
CASE pop.PopulationCategory WHEN 'General Population' THEN 'NA' ELSE PopulationCategory END AS KeyPop, refer.referralDate AS ReferalDate, link.healthworker AS handedOverTo, link.cadre AS handedOverToCadre, 
tout.datetracingdone AS [TracingDate], tout.TraceType AS tracingtype, pcons.itemname AS ConsentValue, tout.Outcome, tout.Remarks AS Remarks, link.linkagedate AS dateEnrolled, link.cccnumber AS CCCNumber
FROM            [dbo].[PatientEncounter] PE INNER JOIN
                         patient p ON p.id = pe.patientid INNER JOIN
                         PersonView per ON per.id = p.personid LEFT JOIN
                         PersonLocation ploc ON ploc.personid = per.id LEFT JOIN
                         PersonContact pcon ON pcon.personid = per.id LEFT JOIN
                         PatientPopulationView pop ON pop.PatientPK = p.ptn_pk INNER JOIN
                         [dbo].[PatientMasterVisit] PM ON PM.Id = PE.PatientMasterVisitId LEFT JOIN
                             (SELECT DISTINCT l.itemname, PatientMasterVisitId
                               FROM            PatientConsent t, [dbo].[LookupItemView] l
                               WHERE        l.itemid = t .ConsentValue) pcons ON pcons.PatientMasterVisitId = pm.id INNER JOIN
                         [dbo].[HtsEncounter] HE ON PE.Id = HE.PatientEncounterID inner JOIN
                             (SELECT DISTINCT PersonId, PatientId, cast(LinkageDate AS date) LinkageDate, CCCNumber, Facility, Enrolled, HealthWorker, Cadre
                               FROM            [dbo].[PatientLinkage]) link ON link.personid = per.id INNER JOIN
                         [dbo].[HtsEncounterResult] HER ON HtsEncounterId = HE.Id LEFT JOIN
                             (SELECT DISTINCT personid, datetracingdone, l.itemname TraceType, j.itemname Outcome, Remarks
                               FROM            [dbo].[Tracing] t LEFT JOIN
                                                         [dbo].[LookupItemView] l ON l.itemid = t .mode LEFT JOIN
                                                         [dbo].[LookupItemView] j ON j.itemid = t .outcome) tout ON tout.PersonID = per.id LEFT JOIN
                             (SELECT DISTINCT he.personid, cast(referralDate AS Date) referralDate
                               FROM            [dbo].[HtsEncounterResult] her, [HtsEncounter] he, [LookupItemView] look, Referral ref
                               WHERE        her.HtsEncounterId = he.Id AND he.PersonId = ref.PersonId AND her.FinalResult = look.ItemId AND ItemName = 'Positive') refer ON refer.personid = per.id

GO



/****** Object:  View [dbo].[vw_PartnerNotificationRegister]    Script Date: 11/13/2018 08:09:30 ******/
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
s.Personid,p.FacilityId FacilityCode,
		NULL as ID,
		identify.IdentifierValue [HTS_Number],
		htc.VisitDate [Date],
		htc.PatientName [Name],
		DATEDIFF(year,htc.DOB,getdate()) Age,
		htc.Gender [Sex],
		htc.MaritalStatus [MaritalStatus],
		htc.KeyPop [KeyPopulation],
		edv.TestingStrategy [Modality],
		ploc.[landmark] [PhysicalLocation],
		CONVERT(varchar(100), decryptbykey(pcon.MobileNumber)) [CellPhone],
		case when plink.CCCNumber is not null then 'Yes' else 'No' end as [EnrolledIntoCare],
		plink.CCCNumber ,
		plink.Facility [FacilityEnrolledIn],
		edv.PartnerListingConsent [PNSAccepted],
		edv.[PartnerListingConsentDeclineReason] [PNSReasonNotAccepted],
		[Name(s)],--partner
		[AgeYears] [AgeYears],--partner
		[PNSSex]  [PNSSex],--partner
		[RelationsipToIndexClient]  [RelationsipToIndexClient],--partner
		s.[Occupation],--partner
		case when IPVOutcome is null --and [RelationsipToIndexClient]='Child'  
		then 'NA' when IPVOutcome is not null then 'Yes' else 'No' end as ScreenedforIPV,
		PnsPhysicallyHurt  [IPVQuestionOne],
		PnsThreatenedHurt  [IPVQuestionTwo],
		PnsForcedSexual  [PNSQuestionThree],
		IPVOutcome  [IPVScreeningOutcome],
		 [CellPhoneNo],--partner
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
		
		
FROM [dbo].[HTS_LAB_Register] htc inner join 
(SELECT a.Id AS Patientid, a.ptn_pk, b.IdentifierValue
FROM  dbo.PatientEnrollment AS d INNER JOIN dbo.ServiceArea AS c ON d.ServiceAreaId = c.Id INNER JOIN
 dbo.Patient AS a INNER JOIN dbo.PatientIdentifier AS b ON a.Id = b.PatientId ON d.Id = b.PatientEnrollmentId
WHERE (c.Name = 'HTS Module'))identify on identify.patientid=htc.patientid inner join
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
left join 

   (select distinct a.Patientid,PatientPk, b.PersonId,CONVERT(varchar(50), decryptbykey(c.firstname)) + ' ' + CONVERT(varchar(50), decryptbykey(c.midname)) + ' ' + CONVERT(varchar(50), decryptbykey(c.lastname)) AS [Name(s)],
DATEDIFF(mm,c.DateofBirth,getdate())/12 [AgeYears], s.ItemName [PNSSex],t.ItemName [RelationsipToIndexClient],CONVERT(varchar(50), decryptbykey([MobileNumber])) [CellPhoneNo] from [dbo].[HTS_LAB_Register] a inner join 
[dbo].[PersonRelationship]b on b.patientid=a.patientid inner join 
[dbo].person c on c.id =b.personid left join [dbo].[PersonContact]cont on c.id=cont.Personid
left join LookupItemView s on s.ItemId=c.sex
inner join LookupItemView t on t.itemid=b.[RelationshipTypeId]) partnerdet on partnerdet.personid=s.personid



GO

