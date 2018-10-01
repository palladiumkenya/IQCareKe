IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Identifiers') AND name = 'IdentifierValueSeparator' )
ALTER TABLE Identifiers ADD IdentifierValueSeparator NVARCHAR(1) NOT NULL DEFAULT ''

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Identifiers') AND name = 'ValidatorRegex' )
ALTER TABLE Identifiers ADD ValidatorRegex NVARCHAR(100)  NOT NULL DEFAULT ''

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Identifiers') AND name = 'FailedValidationMessage' )
ALTER TABLE Identifiers ADD FailedValidationMessage NVARCHAR(250) NOT NULL DEFAULT '' 

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Identifiers') AND name = 'MinLength' )
ALTER TABLE Identifiers ADD MinLength INTEGER NOT NULL DEFAULT 10

IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Identifiers') AND name = 'MaxLength' )
ALTER TABLE Identifiers ADD MaxLength INTEGER NOT NULL DEFAULT 10
