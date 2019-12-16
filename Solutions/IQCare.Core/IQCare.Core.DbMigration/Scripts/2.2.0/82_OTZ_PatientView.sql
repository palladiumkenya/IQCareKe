
IF  OBJECT_ID('OTZ_PatientView', 'V') IS NOT NULL
    DROP VIEW [OTZ_PatientView]
GO


CREATE VIEW [dbo].[OTZ_PatientView]
AS
SELECT DISTINCT A.PatientId, P.ptn_pk, C.Sex, CAST(P.DateOfBirth AS DATE) AS DOB, DATEDIFF(year, P.DateOfBirth, A.EnrollmentDate) AS Age, 
CAST(A.EnrollmentDate AS DATE) AS EnrollmentDate
FROM            dbo.PatientEnrollment AS A INNER JOIN
                         dbo.ServiceArea AS B ON B.Id = A.ServiceAreaId INNER JOIN
                         dbo.Patient AS P ON P.Id = A.PatientId INNER JOIN
                         dbo.PatientIdentifier AS Pid ON Pid.PatientId = P.Id INNER JOIN
                         dbo.PatientView AS C ON C.Ptn_Pk = P.ptn_pk
WHERE        (B.Code = 'OTZ') AND (P.DeleteFlag = 0) AND (C.DeleteFlag = 0) AND (DATEDIFF(year, P.DateOfBirth, A.EnrollmentDate) < 20)


GO
