IF NOT EXISTS(SELECT TOP 1 Id FROM Identifiers WHERE Name = N'PNC Number')
BEGIN
	INSERT INTO [dbo].[Identifiers] ([Name], [Code], [DisplayName], [DataType], [PrefixType], [SuffixType], [DeleteFlag], [CreatedBy], [CreateDate] ,[AuditData], IdentifierType) 
	VALUES (N'PNC Number', N'PNCNumber', N'PNC Number', N'Numeric', NULL, NULL, 0, 1, GETDATE(), NULL, 1);
END;