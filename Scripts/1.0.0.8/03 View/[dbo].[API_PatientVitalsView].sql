IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[API_PatientVitalsView]'))
DROP VIEW [dbo].[API_PatientVitalsView]
GO

/****** Object:  View [dbo].[gcPatientView]    Script Date: 7/23/2018 11:11:14 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[API_PatientVitalsView]
AS
select Id,PatientId,PatientMasterVisitId,Height,[Weight],
'kgs' as WeightUnits ,'cms' as HeightUnits,convert(varchar(20),VisitDate)  as VisitDate
 from PatientVitals



