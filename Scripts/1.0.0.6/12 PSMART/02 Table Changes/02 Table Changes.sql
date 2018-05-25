
--  Table Changes specific to PSMART

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'CardSerialNumber'
          AND Object_ID = Object_ID(N'schemaName.mst_Patient'))
BEGIN
    -- Column Exists
	ALTER TABLE mst_Patient 
		ADD  CardSerialNumber varchar(60) null
END

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'HTSID'
          AND Object_ID = Object_ID(N'schemaName.mst_Patient'))
BEGIN
    -- Column Exists
	ALTER TABLE mst_Patient 
		ADD  HTSID varchar(60) null
END

IF NOT EXISTS(SELECT * FROM Identifiers WHERE code='CARD_SERIALNUMBER')
BEGIN
  INSERT INTO Identifiers ([Name],Code,DisplayName,DataType,PrefixType,SuffixType,IdentifierType,CreatedBy,AssigningAuthority) VALUES(
	'CardSerialNumber',
	'CARD_SERIAL_NUMBER',
	'CARD SERIAL NUMBER',
	'Text',
	'',
	'',
	2,
	1,
	'PSMART_REGISTRY'
  )
END
Go
If Not Exists(Select * from sys.columns where Name = N'Request' AND Object_ID = Object_ID(N'PSmartTransactionLog'))
BEGIN
	ALTER TABLE PSmartTransactionLog ADD Request varchar(max)
END
GO

If Not Exists(Select * from sys.columns where Name = N'LogMessage' AND Object_ID = Object_ID(N'PSmartTransactionLog'))
BEGIN
	ALTER TABLE PSmartTransactionLog ADD LogMessage varchar(800)
END
GO

If Not Exists(Select * from sys.columns where Name = N'IssuingAuthority' AND Object_ID = Object_ID(N'Identifiers'))
BEGIN
	ALTER TABLE Identifiers ADD IssuingAuthority varchar(15)
END
GO
If Not Exists(Select * from sys.columns where Name = N'AssigningFacility' AND Object_ID = Object_ID(N'PatientIdentifier'))
BEGIN
	ALTER TABLE PatientIdentifier ADD AssigningFacility varchar(15)
END
GO
If Not Exists(Select * from sys.columns where Name = N'AssigningFacility' AND Object_ID = Object_ID(N'PersonIdentifier'))
BEGIN
	ALTER TABLE PersonIdentifier ADD AssigningFacility varchar(15)
END
GO