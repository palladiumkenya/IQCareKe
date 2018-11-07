CREATE PROCEDURE GetPatientRelationships
@PatientId INT
AS
exec pr_OpenDecryptedSession
SELECT pr.*  FROM PatientRelationshipView pr WHERE pr.PatientId = @PatientId
exec [dbo].[pr_CloseDecryptedSession]
GO

