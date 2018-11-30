
IF OBJECT_ID('dbo.PhysicalExaminationView', 'V') IS NOT NULL
    DROP VIEW [dbo].[PhysicalExaminationView]
GO

CREATE VIEW [dbo].[PhysicalExaminationView]
AS
SELECT pe.Id, pe.PatientId,pe.CreateBy, pe.CreateDate, pe.ExamId, pe.ExaminationTypeId, pe.FindingId, pe.FindingsNotes, 
pe.PatientMasterVisitId,lki.DisplayName AS ExamDisplayName, lki.Name as ExamName

from PhysicalExamination pe 
INNER JOIN LookupItem lki 
ON lki.Id = pe.ExamId
GO