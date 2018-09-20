/****** Object:  View [dbo].[PatientStabilitySummary]    Script Date: 5/26/2018 1:23:42 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PatientStabilitySummary]
AS
SELECT        ROW_NUMBER() OVER (ORDER BY Category) AS Id, count(*) AS Value, Category
FROM            (SELECT        CASE WHEN C.Id IS NULL OR
                                                    C.Categorization = 2 THEN 'Unstable' ELSE 'Stable' END AS Category
                          FROM            PatientEnrollment PE INNER JOIN
                                                    dbo.Patient PT ON PT.Id = pe.PatientId INNER JOIN
                                                    dbo.PatientIdentifier PI ON PE.Id = PI.PatientEnrollmentId INNER JOIN
                                                    dbo.Identifiers IE ON PI.IdentifierTypeId = IE.Id LEFT OUTER JOIN
                                                        (SELECT        PatientId, Id, Categorization, row_number() OVER (Partition BY PatientId
                                                          ORDER BY DateAssessed DESC) RowNum
                          FROM            PatientCategorization) C ON C.PatientId = Pe.PatientId AND C.RowNum = 1
WHERE        ServiceAreaId = 1 AND IE.Name = 'CCC Registration Number' AND PT.DeleteFlag = 0 AND DATEDIFF(MONTH, PE.EnrollmentDate, GETDATE()) > 12 AND PE.CareEnded = 0
UNION ALL
select case when (LUI.Name = 'Stage1' OR LUI.Name = 'Stage2') AND (PB.CD4Count > 200) Then 'Well'
when (LUI.Name = 'Stage3' OR LUI.Name = 'Stage4') OR (PB.CD4Count <= 200) Then 'Advanced'
ELSE 'Unknown (Missing Baseline WHO Stage or CD4)' END AS Category 
from PatientBaselineAssessment PB 
inner join LookUpItem LUI on PB.WHOStage = LUI.Id
inner join PatientEnrollment PE on PB.PatientId = PE.PatientID
where DATEDIFF(MONTH, PE.EnrollmentDate, GETDATE()) <= 12 AND PE.CareEnded = 0
) AS Categorization
GROUP BY Category

GO


