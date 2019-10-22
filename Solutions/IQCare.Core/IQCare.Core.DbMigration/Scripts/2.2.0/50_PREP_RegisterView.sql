
/****** Object:  View [dbo].[PREP_RegisterView]    Script Date: 10/16/2019 13:58:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[PREP_RegisterView]
AS
SELECT distinct a.[PatientId]
	  ,p.Ptn_Pk
      ,a.[PatientEncounterId]
	  ,Pv.Sex Gender
	  ,pop.PopulationCategory  PopType
	  ,cast(PV.RegistrationDate as date)RegistrationDate
	  ,cast(pm.VisitDate as date)VisitDate
	  ,pe.PatientMasterVisitId
      ,[SignsSymptomsHIV]=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = a.SignsOrSymptomsHIV)
      ,[AdherenceCounselling]=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = a.AdherenceCounsellingDone)
     -- ,[ContraindicationsPrep]=
					--(SELECT        TOP 1 ItemName
					--  FROM            [dbo].[LookupItemView]
					--  WHERE        ItemId = a.ContraindicationsPrepPresent)
      ,[PrepStatus]=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = a.PrepStatusToday)
      ,[IssuedCondoms]=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = a.CondomsIssued)
      ,[NoOfCondoms],[AppointmentDate],[Reason]=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = pa.ReasonId),[Description],STILabInvestigationDone, STIScreeningDone, STISymptoms,STITreatmentOffered,[FinalResult],
	  CASE WHEN STIScreeningDone is Null then NULL else  STIScreeningDone end as  STIScreening,
	  ProviderName = (SELECT TOP 1 (UserFirstName + ' ' + UserLastName) FROM [dbo].[mst_User] WHERE UserID = Pe.CreatedBy),[ChronicIllness],illness.[Treatment],
	  AllergenName,ReactionName,SeverityName,EventName AdverseEventName,[EventCause]AdverseEventCause,AdverseEventSeverity=
					(SELECT        TOP 1 ItemName
					  FROM            [dbo].[LookupItemView]
					  WHERE        ItemId = AE.[Severity]),AE.[Action]AdverseEventAction
  FROM [dbo].[PatientPrEPStatus]a
  INNER JOIN Patient p on P.ID=a.[PatientId]
  INNER JOIN PatientView PV on P.[Ptn_Pk]=PV.[Ptn_Pk]
  INNER JOIN PatientMasterVisit PM on PM.[PatientId] = P.ID
  INNER JOIN PatientEncounter PE ON PE.PatientMasterVisitId = pm.ID
  --INNER JOIN PatientMasterVisit PM on PM.Id = pe.PatientMasterVisitId
  left join (SELECT distinct  * FROM  (SELECT  distinct patientid, [PatientMasterVisitId], ScreeningValue,
					CASE WHEN x.num = '1' THEN 'STILabInvestigationDone' WHEN x.num = '2' THEN 'STIScreeningDone' 
					WHEN x.num = '3' THEN 'STISymptoms' 
					WHEN x.num = '4' THEN 'STITreatmentOffered'END AS Trace
				FROM (select distinct cast(Row_Number() OVER (PARTITION BY a.patientid ORDER BY PatientMasterVisitId asc) AS Varchar) AS num, 
					patientid,[PatientMasterVisitId],[ScreeningCategory],ScreeningValue from
						(select distinct  ps.patientid,pe.[PatientMasterVisitId],[ScreeningCategory]=
							(SELECT        TOP 1 ItemName
								FROM            [dbo].[LookupItemView]
								WHERE        ItemId = ps.[ScreeningCategoryId])
								,[ScreeningValue]=
							(SELECT        TOP 1 ItemName
								FROM            [dbo].[LookupItemView]
								WHERE        ItemId = ps.[ScreeningValueId])
								from [dbo].[PatientPrEPStatus]a
			INNER JOIN PatientEncounter PE ON PE.ID = a.PatientEncounterId
			inner join [dbo].[PatientScreening]ps on ps.[PatientId]=a.[PatientId] and ps.[PatientMasterVisitId]=pe.[PatientMasterVisitId] )a)x) y 
			PIVOT (Max(ScreeningValue) FOR Trace IN (STILabInvestigationDone, STIScreeningDone, STISymptoms,STITreatmentOffered))S)ps 
			on ps.[PatientId]=a.[PatientId] and ps.[PatientMasterVisitId]=pe.[PatientMasterVisitId]
  LEFT JOIN [dbo].[HTS_EncountersDetailView] edv on edv.[PatientId]=a.[PatientId] and datediff(day,edv.[EncounterDate],pm.VisitDate)<=2
  LEFT JOIN PatientAppointment PA ON PA.PatientMasterVisitId = pe.PatientMasterVisitId
  left join PatientChronicIllnessView illness on illness.PatientId=a.[PatientId] and pm.Id=illness.PatientMasterVisitId
  left join PatientAllergyView PAV  on PAV.PatientId=a.[PatientId] and pm.Id=PAV.PatientMasterVisitId
  left join AdverseEvent AE on AE.PatientId=a.[PatientId] and pm.Id=AE.PatientMasterVisitId
  LEFT JOIN (SELECT distinct SS.PatientPK,ss.PersonId, STUFF((SELECT ';' + US.PopulationCategory 
    FROM (select distinct us.PopulationCategory, p.ptn_pk as PatientPK from PatientPopulationView US 
	INNER JOIN patient p on p.personid=us.personid) us 
		WHERE us.PatientPK = SS.PatientPK
		FOR XML PATH('')), 1, 1, '') PopulationCategory
		FROM (select distinct us.PopulationCategory,us.personid, p.ptn_pk as PatientPK from PatientPopulationView US 
	INNER JOIN patient p on p.personid=us.personid) SS
	GROUP BY SS.PatientPK,ss.Personid,  SS.PopulationCategory) pop ON pop.PatientPK = p.ptn_pk 
  LEFT JOIN ServiceArea sa on sa.Id=Pe.[ServiceAreaId]
  where a.DeleteFlag=0 and sa.Code='PREP'

GO


