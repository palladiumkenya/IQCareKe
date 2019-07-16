/****** Object:  View [dbo].[PatientView]    Script Date: 7/12/2019 11:01:54 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

ALTER VIEW [dbo].[PatientView]
AS
Select	MP.Ptn_Pk
	,	cast(decryptbykey(FirstName) As varchar(50))	As FirstName
	,	cast(decryptbykey(LastName) As varchar(50))		As LastName
	,	cast(decryptbykey(MiddleName) As varchar(50))	As MiddleName
	,	cast(decryptbykey(FirstName) As varchar(50)) + ' '+Isnull(cast(decryptbykey(MiddleName) As varchar(50)) ,'') + cast(decryptbykey(LastName) As varchar(50))		As PatientName
	,	LocationId
	,	IQNumber
	,	MP.RegistrationDate
	,	DOB
	,	Case Sex
			When 16 Then 'Male'
			Else 'Female'
		End												As Sex
	,	MP.DobPrecision
	,	DateOfDeath
	,	MaritalStatus
	,	Sex												As SexId
	,	Nullif(Convert(varchar(100), Decryptbykey([Address])),'') As [Address]
	,	Nullif(Convert(varchar(100), Decryptbykey(Phone)),'') As Phone
	,	PatientFacilityId
	,	UserId
	,	MP.CreateDate
	,	UpdateDate
	,	MP.DeleteFlag
	,	Status
	,	PatientEnrollmentID = (SELECT PI.IdentifierValue FROM PatientIdentifier PI WHERE IdentifierTypeId = 1 AND PI.PatientId = P.Id)
	,CardSerialNumber
From mst_Patient MP
LEFT JOIN Patient P ON P.ptn_pk = MP.Ptn_Pk
Where (MP.DeleteFlag = 0 Or MP.DeleteFlag Is Null)
GO


