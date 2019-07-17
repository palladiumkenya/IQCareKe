/****** Object:  View [dbo].[PatientPopulationView]    Script Date: 07/12/2019 10:21:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[PatientPopulationView]
AS
     SELECT DISTINCT
            b.ptn_pk AS PatientPK,b.PersonId,
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


