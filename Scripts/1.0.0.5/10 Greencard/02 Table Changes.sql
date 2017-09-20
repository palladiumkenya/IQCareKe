IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'IdentifierType' AND Object_ID = OBJECT_ID(N'Identifiers'))
BEGIN
	ALTER TABLE [dbo].[Identifiers] ADD IdentifierType int NULL DEFAULT(1);
END;

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS WHERE CONSTRAINT_NAME='Identifiers')
BEGIN
	ALTER TABLE [dbo].[Identifiers]  WITH CHECK ADD  CONSTRAINT [FK_Identifiers_IdentifierTypes] FOREIGN KEY([IdentifierType]) REFERENCES [dbo].[IdentifierTypes] ([Id])
	GO
	ALTER TABLE [dbo].[Identifiers] CHECK CONSTRAINT [FK_Identifiers_IdentifierTypes];
	GO
END;