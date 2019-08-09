IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'ReasonNotContacted' AND Object_ID = OBJECT_ID(N'Tracing'))
BEGIN
	ALTER TABLE [dbo].[Tracing] ADD ReasonNotContacted int NULL;
END

IF NOT EXISTS (SELECT * FROM sys.columns WHERE Name = N'OtherReasonSpecify' AND Object_ID = OBJECT_ID(N'Tracing'))
BEGIN
	ALTER TABLE [dbo].[Tracing] ADD OtherReasonSpecify varchar(50) NULL;
END