
IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[gcPatientView]'))
DROP VIEW [gcPatientView]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  View [dbo].[gcPatientView]    Script Date: 5/18/2018 11:32:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[gcPatientView]
AS
SELECT DISTINCT pt.Id
	,pt.PersonId
	,pt.ptn_pk
	,isnull(pni.IdentifierValue, (
			SELECT TOP 1 isnull(PosID, '00000')
			FROM mst_Facility
			WHERE DeleteFlag = 0
			) + '-00000') AS EnrollmentNumber
	,pt.PatientIndex
	,CAST(DECRYPTBYKEY(pn.FirstName) AS VARCHAR(50)) AS FirstName
	,CAST(DECRYPTBYKEY(pn.MidName) AS VARCHAR(50)) AS MiddleName
	,CAST(DECRYPTBYKEY(pn.LastName) AS VARCHAR(50)) AS LastName
	,pn.Sex
	,pn.Active
	,pt.RegistrationDate
	,ISNULL(pe.EnrollmentDate, '01-01-1900') AS [EnrollmentDate ]
	,ISNULL(CAST((
				CASE pe.CareEnded
					WHEN 0
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
	,CAST(DECRYPTBYKEY(pc.MobileNumber) AS VARCHAR(20)) AS MobileNumber
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
LEFT JOIN  dbo.Person AS pn  ON pn.Id = pt.PersonId
LEFT JOIN dbo.PatientEnrollment AS pe ON pt.Id = pe.PatientId
LEFT JOIN dbo.PatientIdentifier AS pni ON pni.PatientId = pt.Id
LEFT JOIN dbo.Identifiers ON pni.IdentifierTypeId = dbo.Identifiers.Id
LEFT OUTER JOIN (
	SELECT PatientId
		,ExitReason
	FROM dbo.PatientCareending
	
	UNION
	
	SELECT dbo.Patient.Id AS PatientId
		,CASE PatientExitReason
			WHEN 91
				THEN 526
			WHEN 93
				THEN 259
			WHEN 115
				THEN 260
			WHEN 118
				THEN 260
			WHEN 414
				THEN 526
			END AS ExitReason
	FROM dbo.dtl_PatientCareEnded
	INNER JOIN dbo.Patient ON dbo.dtl_PatientCareEnded.Ptn_Pk = dbo.Patient.ptn_pk
	) AS ptC ON pt.Id = ptC.PatientId
LEFT OUTER JOIN dbo.PersonContact AS pc ON pc.PersonId = pt.PersonId
WHERE --   (dbo.Identifiers.Name = 'CCC Registration Number') AND 
	(pn.DeleteFlag = 0) AND pt.PersonId IS NOT NULL

GO

--CREATE VIEW [gcPatientView]
--AS
--SELECT DISTINCT 
--                         pt.Id, pt.PersonId, pt.ptn_pk, pni.IdentifierValue AS EnrollmentNumber, pt.PatientIndex, CAST(DECRYPTBYKEY(pn.FirstName) AS VARCHAR(50)) AS FirstName, CAST(DECRYPTBYKEY(pn.MidName) AS VARCHAR(50)) 
--                         AS MiddleName, CAST(DECRYPTBYKEY(pn.LastName) AS VARCHAR(50)) AS LastName, pn.Sex, pn.Active, pt.RegistrationDate, pe.EnrollmentDate AS [EnrollmentDate ], 
--                         ISNULL(CAST((CASE pe.CareEnded WHEN 0 THEN 'Active' WHEN 1 THEN
--                             (SELECT        TOP 1 ItemName
--                               FROM            LookupItemView
--                               WHERE        MasterName = 'CareEnded' AND ItemId = ptC.ExitReason) END) AS VARCHAR(50)), 'Active') AS PatientStatus, ptC.ExitReason, pt.DateOfBirth, CAST(DECRYPTBYKEY(pt.NationalId) AS VARCHAR(50)) AS NationalId, 
--                         pt.FacilityId, pt.PatientType, pe.TransferIn, CAST(DECRYPTBYKEY(pc.MobileNumber) AS VARCHAR(20)) AS MobileNumber, ISNULL
--                             ((SELECT        TOP (1) ScreeningValueId
--                                 FROM            dbo.PatientScreening
--                                 WHERE        (PatientId = pt.Id) AND (ScreeningTypeId IN
--                                                              (SELECT        Id
--                                                                FROM            dbo.LookupMaster
--                                                                WHERE        (Name = 'TBStatus')))
--                                 ORDER BY Id DESC), 0) AS TBStatus, ISNULL
--                             ((SELECT        TOP (1) ScreeningValueId
--                                 FROM            dbo.PatientScreening AS PatientScreening_1
--                                 WHERE        (PatientId = pt.Id) AND (ScreeningTypeId IN
--                                                              (SELECT        Id
--                                                                FROM            dbo.LookupMaster AS LookupMaster_1
--                                                                WHERE        (Name = 'NutritionStatus')))
--                                 ORDER BY Id DESC), 0) AS NutritionStatus, ISNULL
--                             ((SELECT        TOP (1) Categorization
--                                 FROM            dbo.PatientCategorization
--                                 WHERE        (PatientId = pt.Id)
--                                 ORDER BY id DESC), 0) AS Categorization, pt.DobPrecision
--FROM            dbo.Patient AS pt INNER JOIN
--                         dbo.Person AS pn ON pn.Id = pt.PersonId INNER JOIN
--                         dbo.PatientEnrollment AS pe ON pt.Id = pe.PatientId INNER JOIN
--                         dbo.PatientIdentifier AS pni ON pni.PatientId = pt.Id INNER JOIN
--                         dbo.Identifiers ON pni.IdentifierTypeId = dbo.Identifiers.Id LEFT OUTER JOIN
--                             (SELECT        PatientId, ExitReason
--                               FROM            dbo.PatientCareending
--                               UNION
--                               SELECT        dbo.Patient.Id AS PatientId, CASE PatientExitReason WHEN 91 THEN 526 WHEN 93 THEN 259 WHEN 115 THEN 260 WHEN 118 THEN 260 WHEN 414 THEN 526 END AS ExitReason
--                               FROM            dbo.dtl_PatientCareEnded INNER JOIN
--                                                        dbo.Patient ON dbo.dtl_PatientCareEnded.Ptn_Pk = dbo.Patient.ptn_pk) AS ptC ON pt.Id = ptC.PatientId LEFT OUTER JOIN
--                         dbo.PersonContact AS pc ON pc.PersonId = pt.PersonId
--WHERE        (dbo.Identifiers.Name = 'CCC Registration Number') AND (pn.DeleteFlag = 0)
--GO