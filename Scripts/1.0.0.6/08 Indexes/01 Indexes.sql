IF Not  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[LookupItem]') AND name = N'NCI_LookupItem_Name') Begin
	CREATE NONCLUSTERED INDEX [NCI_LookupItem_Name] ON [dbo].[LookupItem]([Name])
End
Go
IF Not  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[LookupMaster]') AND name = N'NCI_LookupMaster_Name') Begin
	CREATE NONCLUSTERED INDEX [NCI_LookupMaster_Name] ON [dbo].[LookupMaster]([Name])
End
Go

IF Not  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Patient]') AND name = N'NCI_Patient_DeleteFlag') Begin
	CREATE NONCLUSTERED INDEX [NCI_Patient_DeleteFlag]	ON [dbo].[Patient] ([DeleteFlag])
	INCLUDE ([Id],[ptn_pk],[PersonId])
End
GO

IF Not  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[PatientIdentifier]') AND name = N'NCI_PatientEnrollment_IdentifierTypeId_INC') Begin
CREATE NONCLUSTERED INDEX [NCI_PatientEnrollment_IdentifierTypeId_INC]	ON [dbo].[PatientIdentifier] ([IdentifierTypeId])
INCLUDE ([PatientId],[PatientEnrollmentId])
End
GO
IF Not  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientAppointment]') AND name = N'NCI_DTLPatientAppointment_DeleteFlag_UserId_Inc') Begin
CREATE NONCLUSTERED INDEX [NCI_DTLPatientAppointment_DeleteFlag_UserId_Inc] ON [dbo].[dtl_PatientAppointment] ([DeleteFlag],[UserID])
INCLUDE ([Ptn_pk],[LocationID],[Visit_pk],[AppDate],[AppReason],[AppStatus],[EmployeeID],[CreateDate],[UpdateDate],[ModuleId],[AppNote],[AppointmentId])
End
GO

