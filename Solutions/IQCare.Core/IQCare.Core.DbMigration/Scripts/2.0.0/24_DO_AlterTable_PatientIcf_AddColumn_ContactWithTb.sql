IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ContactWithTb' AND Object_ID = OBJECT_ID(N'PatientIcf'))
BEGIN
	ALTER TABLE [dbo].[PatientIcf] ADD ContactWithTb bit NULL;
END;