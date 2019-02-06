IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'RegistrationDate'AND Object_ID = OBJECT_ID(N'Person'))
BEGIN
	ALTER TABLE [dbo].[Person] ADD RegistrationDate datetime null;
END;