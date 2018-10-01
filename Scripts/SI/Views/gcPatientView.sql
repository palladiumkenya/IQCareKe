/****** Object:  View [dbo].[gcPatientView]    Script Date: 28-Sept-2018 18:39:51 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[gcPatientView]'))
DROP VIEW [dbo].[gcPatientView]
GO

/****** Object:  View [dbo].[gcPatientView]    Script Date: 9/28/2018 11:11:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[gcPatientView]
AS
SELECT [Id],
[PersonId],
[ptn_pk],
[PatientClinicId],
[CCCNumber] AS EnrollmentNumber,
[PatientIndex],
[FirstName],
[MiddleName],
[LastName],
[Sex],
[Active],
[RegistrationDate],
[EnrollmentDate],
[CareEnded],
[PatientStatus],
[ExitReason],
[DateOfBirth],
[NationalId],
[FacilityId],
[PatientType],
[TransferIn],
[MobileNumber],
[TBStatus],
[NutritionStatus],
[Categorization],
[DobPrecision]

FROM
(
	SELECT pt.Id
		,pt.PersonId
		,pt.ptn_pk
		,pni.IdentifierValue
		,I.Code
		,pt.PatientIndex
		,CAST(DECRYPTBYKEY(pn.FirstName) AS VARCHAR(50)) AS FirstName
		,CAST(DECRYPTBYKEY(pn.MidName) AS VARCHAR(50)) AS MiddleName
		,CAST(DECRYPTBYKEY(pn.LastName) AS VARCHAR(50)) AS LastName
		,pn.Sex
		,pn.Active
		,pt.RegistrationDate
		,ISNULL(pe.EnrollmentDate
		, convert(datetime,'1900-06-15') ) AS [EnrollmentDate]
		,pe.CareEnded
		,ISNULL(CAST((
					CASE pe.CareEnded
						WHEN 0--(SELECT top 1 i.IdentifierValue FROM patientIdentifier i WHERE i.PatientId=pt.Id AND pt.Id NOT IN(SELECT top 1 d.PatientId FROM PatientCareending d WHERE d.PatientId=pt.Id)) 
							THEN 'Active'
						WHEN 1
							THEN (
									SELECT TOP 1 ItemName
									FROM LookupItemView
									WHERE MasterName = 'CareEnded'
										AND ItemId = ptC.ExitReason
									)
						ELSE 'Not Enrolled'
						END
					) AS VARCHAR(50)), 'Active') AS PatientStatus
		,ptC.ExitReason
		,pt.DateOfBirth
		,CAST(DECRYPTBYKEY(pt.NationalId) AS VARCHAR(50)) AS NationalId
		,pt.FacilityId
		,pt.PatientType
		,pe.TransferIn
		,(CAST(DECRYPTBYKEY((SELECT top 1 MobileNumber FROM PersonContact WHERE PersonId=pn.Id)) AS VARCHAR(20))) AS MobileNumber
		,ISNULL((
				SELECT TOP (1) ScreeningValueId
				FROM dbo.PatientScreening
				WHERE (PatientId = pt.Id)
					AND (
						ScreeningTypeId IN (
							SELECT Id
							FROM dbo.LookupMaster
							WHERE (Name = 'TBStatus')
							)
						)
				ORDER BY Id DESC
				), 0) AS TBStatus
		,ISNULL((
				SELECT TOP (1) ScreeningValueId
				FROM dbo.PatientScreening AS PatientScreening_1
				WHERE (PatientId = pt.Id)
					AND (
						ScreeningTypeId IN (
							SELECT Id
							FROM dbo.LookupMaster AS LookupMaster_1
							WHERE (Name = 'NutritionStatus')
							)
						)
				ORDER BY Id DESC
				), 0) AS NutritionStatus
		,ISNULL((
				SELECT TOP (1) Categorization
				FROM dbo.PatientCategorization
				WHERE (PatientId = pt.Id)
				ORDER BY id DESC
				), 0) AS Categorization
		,ISNULL(pt.DobPrecision, 0) AS DobPrecision
	FROM dbo.Patient AS pt
	INNER JOIN dbo.Person AS pn ON pn.Id = pt.PersonId
	INNER JOIN dbo.PatientEnrollment AS pe ON pt.Id = pe.PatientId
	INNER JOIN dbo.PatientIdentifier AS pni ON pni.PatientId = pt.Id
	LEFT JOIN dbo.Identifiers I ON pni.IdentifierTypeId = I.Id
	INNER JOIN dbo.Identifiers ON pni.IdentifierTypeId = dbo.Identifiers.Id
	LEFT OUTER JOIN (
		SELECT PatientId
			,ExitReason
		FROM dbo.PatientCareending where deleteflag=0
	
		UNION
	
		SELECT dbo.Patient.Id AS PatientId
			,CASE (SELECT TOP 1 Name FROM mst_Decode WHERE CodeID=23 AND ID = (SELECT TOP 1 PatientExitReason FROM dtl_PatientCareEnded WHERE Ptn_Pk = dbo.Patient.ptn_pk AND CareEnded=1))
				WHEN 'Lost to follow-up'
					THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName = 'CareEnded' AND ItemName = 'LostToFollowUp')
				WHEN 'HIV Negative'
					THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName = 'CareEnded' AND ItemName = 'HIV Negative')
				WHEN 'Death'
					THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName = 'CareEnded' AND ItemName = 'Death')
				WHEN 'Confirmed HIV Negative'
					THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName = 'CareEnded' AND ItemName = 'Confirmed HIV Negative')
				WHEN 'Transfer to ART'
					THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName = 'CareEnded' AND ItemName = 'Transfer Out')
				WHEN 'Transfer to another LPTF'
					THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName = 'CareEnded' AND ItemName = 'Transfer Out')
				WHEN 'Discharged at 18 months'
					THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName = 'CareEnded' AND ItemName = 'Confirmed HIV Negative')
				WHEN 'Transfered out'
					THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName = 'CareEnded' AND ItemName = 'Transfer Out')
				END AS ExitReason
		FROM dbo.dtl_PatientCareEnded
		INNER JOIN dbo.Patient ON dbo.dtl_PatientCareEnded.Ptn_Pk = dbo.Patient.ptn_pk
		WHERE dbo.Patient.Id NOT IN (SELECT PatientId FROM dbo.PatientCareending where deleteflag=0)
		) AS ptC ON pt.Id = ptC.PatientId
	WHERE (pn.DeleteFlag = 0) And Pt.DeleteFlag=0
)AS SOURCE PIVOT
(
	MAX(IdentifierValue) FOR Code IN([PatientClinicId],[CCCNumber])
)
	AS TARGET
GO
