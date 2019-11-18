IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[OtzActivityForms]'))
DROP VIEW [dbo].[OtzActivityForms]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW 
 [dbo].[OtzActivityForms]
AS
SELECT 
 PM.VisitDate,
 AttendedSupportGroup = (SELECT ItemName FROM LookupItemView WHERE ItemId = OTZ.AttendedSupportGroup AND MasterName = 'YesNo'),
 OTZ.Remarks,
 Provider = (select UserFirstName + ' ' + UserLastName from mst_User where UserID = OTZ.UserId),
 ModulesDone = (select count(*) from [dbo].[OtzActivityTopics] where [ActivityFormId] = OTZ.Id)
FROM [dbo].[OtzActivityForm] OTZ
INNER JOIN PatientMasterVisit PM ON PM.Id = OTZ.PatientMasterVisitId
GO
