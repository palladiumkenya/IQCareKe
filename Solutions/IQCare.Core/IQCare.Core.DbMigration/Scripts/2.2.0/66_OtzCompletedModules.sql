IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[OtzCompletedModules]'))
DROP VIEW [dbo].[OtzCompletedModules]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW 
 [dbo].[OtzCompletedModules]
AS
SELECT
OTT.Id,
PM.PatientId,
Topic = (select ItemName from LookupItemView where ItemId = OTT.TopicId AND MasterName = 'OTZ_Modules'),
DateCompleted

FROM [dbo].[OtzActivityTopics] OTT
INNER JOIN [dbo].[OtzActivityForm] OAF ON OAF.Id = OTT.ActivityFormId
INNER JOIN PatientMasterVisit PM ON PM.Id = OAF.PatientMasterVisitId
GO
