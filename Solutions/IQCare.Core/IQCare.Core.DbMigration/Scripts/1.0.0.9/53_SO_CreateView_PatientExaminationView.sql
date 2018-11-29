IF OBJECT_ID('dbo.PhysicalExaminationView', 'V') IS NOT NULL
    DROP VIEW [dbo].[PhysicalExaminationView]
GO
CREATE VIEW [dbo].[PhysicalExaminationView]
AS
SELECT       
    Id,
    PatientMasterVisitId,
    PatientId,
    ExaminationTypeId,
	(SELECT top 1 l.Name FROM LookupMaster l WHERE l.Id=e.ExaminationTypeId) ExaminationType,
	ExamId, 
	(SELECT top 1 l.ItemName FROM LookupItemView l WHERE l.ItemId=e.ExamId) Exam,
	DeleteFlag,
	CreateBy,	  
	CreateDate,
	FindingId, 
	(SELECT top 1 l.ItemName FROM LookupItemView l WHERE l.ItemId=e.FindingId) Findings,
    FindingsNotes
FROM            dbo.PhysicalExamination e
WHERE e.ExaminationTypeId IS NOT NULL 
AND
e.ExamId IS NOT NULL
AND
e.FindingId IS NOT NULL
GO


