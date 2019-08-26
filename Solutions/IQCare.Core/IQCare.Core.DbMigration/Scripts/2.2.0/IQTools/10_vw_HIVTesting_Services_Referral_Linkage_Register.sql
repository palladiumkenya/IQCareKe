

/****** Object:  View [dbo].[vw_HIVTesting_Services_Referral_Linkage_Register]    Script Date: 06/27/2019 11:29:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER VIEW [dbo].[vw_HIVTesting_Services_Referral_Linkage_Register]
AS
SELECT DISTINCT  ISNULL(ROW_NUMBER() OVER (ORDER BY p.ptn_pk ASC), - 1) AS RowID,he.id, p.FacilityID FacilityCode,firstname + ' ' + MiddleName + ' ' + lastname AS PatientName, 
	PE.PatientId, p.ptn_pk AS Ptn_pk, p.dateofbirth, DATEdiff(yy, p.dateofbirth, PE.EncounterStartTime) AS Age, Gender =
    (SELECT DISTINCT ItemName
      FROM  [dbo].[LookupItemView]
      WHERE ItemId = per.sex), PE.EncounterStartTime Date, ploc.LandMark AS landmark, NULL AS PhoneNumber, refer.ReferredToFacility AS FacilityName, 
	  NULL AS Occupation, NULL AS IndexClientType, CASE pop.PopulationCategory WHEN 'General Population' THEN 'NA' ELSE PopulationCategory END AS KeyPop, 
	  refer.referralDate AS ReferalDate, link.healthworker AS handedOverTo, link.cadre AS handedOverToCadre, tout.datetracingdone AS [TracingDate], 
	  tout.TraceType AS tracingtype, pcons.itemname AS ConsentValue, tout.Outcome, tout.Remarks AS Remarks, link.linkagedate AS dateEnrolled, 
	  link.Facility FacilityEnrolled, link.cccnumber AS CCCNumber,ArtStartDate
FROM  patient p
INNER JOIN [dbo].[PatientEncounter] PE  ON p.id = pe.patientid 
INNER JOIN PersonView per ON per.id = p.personid 
INNER JOIN [dbo].[PatientMasterVisit] PM ON PM.Id = PE.PatientMasterVisitId and pm.Patientid=pe.patientid
INNER JOIN [dbo].[HtsEncounter] HE ON PE.Id = HE.PatientEncounterID --and he.PatientMasterVisitId = PE.PatientMasterVisitId
INNER JOIN (SELECT DISTINCT he.personid, cast(max(referralDate) AS Date) referralDate,coalesce (max(b.[Name]),Max(OtherFacility))ReferredToFacility
			FROM  [dbo].[HtsEncounterResult] her 
			INNER JOIN [HtsEncounter] he on  her.HtsEncounterId = he.Id 
			INNER JOIN Referral ref on he.PersonId = ref.PersonId  
			LEFT JOIN [LookupItemView] look on her.FinalResult = look.ItemId 
			LEFT JOIN FacilityList b on ref.[ToFacility]=b.[MFLCode]
			WHERE ItemName = 'Positive'
			group by he.personid ) refer ON refer.personid = per.id
LEFT JOIN PersonLocation ploc ON ploc.personid = per.id  
LEFT JOIN (SELECT distinct SS.PatientPK,ss.PersonId, STUFF((SELECT '; ' + US.PopulationCategory 
    FROM (select distinct us.PopulationCategory, p.ptn_pk as PatientPK from PatientPopulationView US 
	INNER JOIN patient p on p.personid=us.personid) us 
		WHERE us.PatientPK = SS.PatientPK
		FOR XML PATH('')), 1, 1, '') PopulationCategory
		FROM (select distinct us. PopulationCategory,us.personid, p.ptn_pk as PatientPK from PatientPopulationView US 
	INNER JOIN patient p on p.personid=us.personid) SS
	GROUP BY SS.PatientPK,ss.Personid,  SS.PopulationCategory) pop ON pop.PatientPK = p.ptn_pk
inner JOIN(SELECT DISTINCT t.[PatientId],l.itemname, PatientMasterVisitId
    FROM  PatientConsent t inner join [dbo].[LookupItemView] l on  l.itemid = t .ConsentValue) pcons 
	ON pcons.PatientMasterVisitId = pm.id and pcons.[PatientId]=p.Id
LEFT JOIN (SELECT DISTINCT PersonId, PatientId, cast(LinkageDate AS date) LinkageDate, CCCNumber, Facility, Enrolled, HealthWorker, Cadre,ArtStartDate
			FROM dbo.[PatientLinkage]) link ON link.personid = per.id 
LEFT JOIN (SELECT DISTINCT personid, datetracingdone, TraceType=
				(SELECT DISTINCT ItemName
					FROM  [dbo].[LookupItemView]
					WHERE ItemId = t.mode),Outcome=
				(SELECT DISTINCT ItemName
				FROM  [dbo].[LookupItemView]
				WHERE ItemId = t.outcome), Remarks
		FROM  [dbo].[Tracing] t
		) tout ON tout.PersonID = per.id 


			



GO


