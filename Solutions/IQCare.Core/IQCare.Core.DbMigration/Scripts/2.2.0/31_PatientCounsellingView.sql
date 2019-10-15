IF OBJECT_ID('dbo.PatientCounsellingView', 'V') IS NOT NULL
    DROP VIEW [dbo].[PatientCounsellingView]
GO
CREATE VIEW [dbo].[PatientCounsellingView]
AS
SELECT
  c.Id,
  c.PatientMasterVisitId ,
  c.PatientId,

  c.CounsellingTopicId,
  (SELECT top 1 i.ItemName FROM LookupItemView i WHERE i.ItemId=c.CounsellingTopicId) CounsellingTopic,
  c.CounsellingDate,
  c.Description,
  c.CreatedBy,
  c.CreateDate
FROM           
	 dbo.PatientCounselling c
	  where (DeleteFlag is null or DeleteFlag = 0)
GO

