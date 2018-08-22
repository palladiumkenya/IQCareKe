IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'OtherFacility'AND Object_ID = OBJECT_ID(N'Referral'))
BEGIN
	ALTER TABLE [dbo].[Referral] ADD OtherFacility varchar(50) NULL;
END;