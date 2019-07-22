
IF NOT EXISTS(select * from Identifiers where Code= 'PREPNumber')
BEGIN
INSERT INTO Identifiers([Name],Code,DisplayName,DataType,DeleteFlag,CreateDate,CreatedBy,IdentifierType)
VALUES('PREP Registration Number','PREPNumber','PREP Number','Text',0,GetDate(),1,(select Id from Identifiers where Name='Patient'))
END