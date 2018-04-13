
--  Table Changes specific to PSMART

IF EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'CardSerialNumber'
          AND Object_ID = Object_ID(N'schemaName.mst_Patient'))
BEGIN
    -- Column Exists
	ALTER TABLE mst_Patient 
		ADD  CardSerialNumber varchar(60) null
END

IF EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'HTSID'
          AND Object_ID = Object_ID(N'schemaName.mst_Patient'))
BEGIN
    -- Column Exists
	ALTER TABLE mst_Patient 
		ADD  HTSID varchar(60) null
END

IF NOT EXISTS(SELECT * FROM Identifiers WHERE code='CARD_SERIALNUMBER')
BEGIN
  INSERT INTO Identifiers (Name,Code,DisplayName,DataType,PrefixType,SuffixType,IdentifierType) VALUES(
	'CardSerialNumber',
	'CARD_SERIAL_NUMBER',
	'CARD SERIAL NUMBER',
	'Text',
	'',
	'',
	2
  )
END