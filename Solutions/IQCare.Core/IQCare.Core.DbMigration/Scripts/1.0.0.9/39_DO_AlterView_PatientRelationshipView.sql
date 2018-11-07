IF OBJECT_ID('[dbo].[PatientRelationshipView]', 'V') IS NOT NULL
    DROP VIEW [dbo].[PatientRelationshipView]
GO
CREATE VIEW [dbo].[PatientRelationshipView]
AS
SELECT 
     PR.Id
	,P.Id PatientId
	,P.PersonId PatientPersonId
	,PD.FirstName PatientFirstName
	,PD.MiddleName PatientMiddleName
	,PD.LastName PatientLastName
	,(SELECT TOP 1 Name	FROM LookupItem LI WHERE LI.Id = PD.Sex) PatientSex
	,isnull(PD.DateOfBirth, P.DateOfBirth) PatientDOB,
	 R.Id AS RelativePersonId
	,R.FirstName RelativeFirstName
	,R.[MiddleName] RelativeMiddleName
	,R.LastName RelativeLastName
	,(SELECT TOP 1	Name	From LookupItem LI	WHERE LI.Id = R.Sex) RelativeSex
	,R.DateOfBirth RelativeDateOfBirth
	,(SELECT TOP 1	Name From LookupItem LI	WHERE LI.Id = PR.RelationshipTypeId) Relationship

FROM Patient P
INNER JOIN PersonRelationship PR ON P.Id = PR.PatientId
INNER JOIN PersonView R ON R.Id = PR.PersonId
INNER JOIN PersonView PD ON PD.Id = P.PersonId
WHERE p.DeleteFlag = 0
AND PR.DeleteFlag = 0
AND R.DeleteFlag = 0 