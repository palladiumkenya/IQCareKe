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
  c.CreatedBy
FROM           
	 dbo.PatientCounselling c
GO


