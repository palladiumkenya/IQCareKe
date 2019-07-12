EXEC pr_OpenDecryptedSession;
IF OBJECT_ID('MigratedPersons') IS NOT NULL Drop Table MigratedPersons;
Go
Create Table MigratedPersons(PersonId int, Ptn_Pk int)
Go
IF OBJECT_ID('MigratedPatients') IS NOT NULL Drop Table MigratedPatients;
Go
Create Table MigratedPatients(PatientId int, Ptn_Pk int)
Go
If Not Exists (Select * From sys.columns Where Name = N'Ptn_Pk' And Object_ID = Object_id(N'Person'))    
Begin
  Alter table dbo.Person Add Ptn_Pk int 
End
Go
--INSERT INTO PERSON
INSERT INTO Person (
	[FirstName],
	[MidName],
	[LastName],
	[Sex],
	[Active],
	[DeleteFlag],
	[CreateDate],
	[CreatedBy],
	[AuditData],
	[DateOfBirth],
	[DobPrecision],
	[RegistrationDate],
	[FacilityId],
	[NickName],
	Ptn_Pk
)
Output inserted.Ptn_Pk, inserted.Id Into MigratedPersons(Ptn_Pk, PersonId)
SELECT 
FirstName,
MiddleName,
LastName,
Sex = (select TOP 1 ItemId from LookupItemView where ItemName = (select TOP 1 Name from mst_Decode where ID = M.Sex) AND MasterName = 'Gender'),
1,
M.DeleteFlag,
M.CreateDate,
M.UserID,
NULL,
M.DOB,
M.DobPrecision,
M.RegistrationDate,
NULL,
NULL,
M.Ptn_Pk

FROM mst_Patient M
left join Patient P on M.Ptn_Pk = P.ptn_pk
left join PatientIdentifier PI ON PI.PatientId = P.Id
where PI.Id IS NULL AND P.Id IS NULL

--insert into patient
INSERT INTO Patient (
	[ptn_pk]
      ,[PersonId]
      ,[PatientIndex]
      ,[PatientType]
      ,[FacilityId]
      ,[Active]
      ,[DateOfBirth]
      ,[DobPrecision]
      ,[NationalId]
      ,[DeleteFlag]
      ,[CreatedBy]
      ,[CreateDate]
      ,[AuditData]
      ,[RegistrationDate]
)
Output inserted.Ptn_Pk, inserted.Id Into MigratedPatients(Ptn_Pk, PatientId)
SELECT 
M.Ptn_Pk,
MP.PersonId,
M.PatientFacilityId,
PatientType = (CASE 
					WHEN FORMAT(dtl_PatientHivPrevCareIE.ARTTransferInDate, 'yyyy-MM-dd') IS NULL 
					THEN (SELECT top 1 Id FROM LookupItem WHERE Name='New')
					WHEN FORMAT(dtl_PatientHivPrevCareIE.ARTTransferInDate, 'yyyy-MM-dd') = '1900-01-01'
					THEN (SELECT top 1 Id FROM LookupItem WHERE Name='New')
					ELSE (SELECT top 1 Id FROM LookupItem WHERE Name='Transfer-In')
					END),
M.PosId,
0,
M.DOB,
M.DobPrecision,
NationalId=(Case When ENCRYPTBYKEY(KEY_GUID('Key_CTC'),M.[ID/PassportNo]) is null then ENCRYPTBYKEY(KEY_GUID('Key_CTC'),'99999999') ELSE ENCRYPTBYKEY(KEY_GUID('Key_CTC'),M.[ID/PassportNo]) END),
M.DeleteFlag,
M.UserID,
M.CreateDate,
NULL,
M.RegistrationDate

FROM mst_Patient M
inner join MigratedPersons MP on MP.Ptn_Pk = M.Ptn_Pk
LEFT JOIN dtl_PatientHivPrevCareIE on dtl_PatientHivPrevCareIE.Ptn_pk = M.Ptn_Pk
left join Patient P on M.Ptn_Pk = P.ptn_pk
left join PatientIdentifier PI ON PI.PatientId = P.Id
where PI.Id IS NULL AND P.Id IS NULL

-- insert into patientenrollment
INSERT INTO [dbo].[PatientEnrollment](
	[PatientId]
    ,[ServiceAreaId]
    ,[EnrollmentDate]
    ,[EnrollmentStatusId]
    ,[TransferIn]
    ,[CareEnded]
    ,[DeleteFlag]
    ,[CreatedBy]
    ,[CreateDate]
    ,[AuditData]
)
SELECT 
P.Id,
1,
LP.StartDate,
0,
EnrollmentStatusId = (CASE 
					WHEN FORMAT(dtl_PatientHivPrevCareIE.ARTTransferInDate, 'yyyy-MM-dd') IS NULL 
					THEN (SELECT top 1 Id FROM LookupItem WHERE Name='New')
					WHEN FORMAT(dtl_PatientHivPrevCareIE.ARTTransferInDate, 'yyyy-MM-dd') = '1900-01-01'
					THEN (SELECT top 1 Id FROM LookupItem WHERE Name='New')
					ELSE (SELECT top 1 Id FROM LookupItem WHERE Name='Transfer-In')
					END),
CareEnded = M.Status,
M.DeleteFlag,
M.UserID,
M.CreateDate,
NULL

FROM mst_Patient M
inner join Lnk_PatientProgramStart LP ON LP.Ptn_pk = M.Ptn_Pk
LEFT JOIN dtl_PatientHivPrevCareIE on dtl_PatientHivPrevCareIE.Ptn_pk = M.Ptn_Pk
left join Patient P on M.Ptn_Pk = P.ptn_pk
left join PatientIdentifier PI ON PI.PatientId = P.Id
where PI.Id IS NULL AND P.Id IS NOT NULL AND LP.ModuleId in (2,203)


-- INSERT INTO PATIENT IDENTIFIER
INSERT INTO [dbo].[PatientIdentifier](
	[PatientId]
    ,[PatientEnrollmentId]
    ,[IdentifierTypeId]
    ,[IdentifierValue]
    ,[DeleteFlag]
    ,[CreatedBy]
    ,[CreateDate]
    ,[Active]
    ,[AuditData]
    ,[AssigningFacility]
)
SELECT 
P.Id,
PE.Id,
1,
M.PatientEnrollmentID,
M.DeleteFlag,
M.UserID,
M.CreateDate,
0,
NULL,
NULL

FROM mst_Patient M
inner join Lnk_PatientProgramStart LP ON LP.Ptn_pk = M.Ptn_Pk
LEFT JOIN dtl_PatientHivPrevCareIE on dtl_PatientHivPrevCareIE.Ptn_pk = M.Ptn_Pk
left join Patient P on M.Ptn_Pk = P.ptn_pk
left join PatientIdentifier PI ON PI.PatientId = P.Id
left join PatientEnrollment PE ON PE.PatientId = P.Id
where PI.Id IS NULL AND P.Id IS NOT NULL AND LP.ModuleId in (2,203) AND PE.Id IS NOT NULL AND ISNULL(M.PatientEnrollmentID, '') != ''