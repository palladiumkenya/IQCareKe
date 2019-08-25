IF NOT EXISTS(SELECT 1 FROM [dbo].[Identifiers] WHERE Code = 'NHIFNO')
BEGIN
INSERT INTO [dbo].[Identifiers]
           ([Name]
           ,[Code]
           ,[DisplayName]
           ,[DataType]
           ,[PrefixType]
           ,[SuffixType]
           ,[DeleteFlag]
           ,[CreatedBy]
           ,[CreateDate]
           ,[AuditData]
           ,[IdentifierType]
           ,[AssigningAuthority]
           ,[IssuingAuthority]
           ,[IdentifierValueSeparator]
           ,[ValidatorRegex]
           ,[FailedValidationMessage]
           ,[MinLength]
           ,[MaxLength])
     VALUES
           ('NHIF No'
           ,'NHIFNO'
           ,'NHIF No'
           ,'Numeric'
           ,NULL
           ,NULL
           ,0
           ,1
           ,GETDATE()
           ,NULL
           ,2
           ,NULL
           ,NULL
           ,''
           ,''
           ,''
           ,0
           ,10)
END
