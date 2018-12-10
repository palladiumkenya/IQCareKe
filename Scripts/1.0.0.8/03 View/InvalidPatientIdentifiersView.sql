IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[InvalidPatientIdentifiersView]'))
DROP VIEW [dbo].[InvalidPatientIdentifiersView]
GO
CREATE VIEW InvalidPatientIdentifiersView AS
	SELECT 
		P.ptn_pk,
		P.Id as PatientId,
		PID.IdentifierValue, 
		UPPER(CONCAT(ISNULL(CAST(DECRYPTBYKEY(MP.FirstName) AS VARCHAR(50)), ''),' ', ISNULL(CAST(DECRYPTBYKEY(MP.MiddleName) AS VARCHAR(50)), ''),' ', ISNULL(CAST(DECRYPTBYKEY(MP.LastName) AS VARCHAR(50)), ''))) as Name, 
		CASE WHEN MP.Sex = 17 THEN 'F' ELSE 'M' END as Sex, 
		I.Code as IdentifierType
	FROM PatientIdentifier PID 
		INNER JOIN Identifiers I ON PID.IdentifierTypeId = I.Id 
		INNER JOIN Patient P ON P.Id = PID.PatientId
		INNER JOIN mst_Patient MP ON MP.Ptn_Pk = P.Ptn_PK 
	WHERE 
		I.DeleteFlag = 0 AND PID.DeleteFlag = 0 AND
		(CASE WHEN LEN(I.IdentifierValueSeparator) > 0 THEN
		  CASE WHEN (PID.IdentifierValue LIKE '%' + IdentifierValueSeparator + '%' AND LEN(PID.IdentifierValue) BETWEEN MinLength AND MaxLength) THEN 1 ELSE 0 END  -- Match MInLenth,MaxLength and Existence of defined Separator
		ELSE
		  CASE WHEN (ISNUMERIC(PID.IdentifierValue) = 1  AND LEN(PID.IdentifierValue) BETWEEN MinLength AND MaxLength) THEN 1 ELSE 0 END  -- Match MInLenth,MaxLength and Absence of defined Separator
		END) = 0
GO
