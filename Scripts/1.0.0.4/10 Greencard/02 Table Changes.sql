IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'FamilyInfoId' AND Object_ID = OBJECT_ID(N'PersonRelationship'))
BEGIN
	ALTER TABLE [dbo].[PersonRelationship] ADD FamilyInfoId int NULL;
END;

IF EXISTS (SELECT * FROM sys.columns WHERE Name = N'FamilyInfoId' AND Object_ID = OBJECT_ID(N'PersonRelationship'))
BEGIN
	ALTER TABLE [dbo].[PersonRelationship] ALTER COLUMN FamilyInfoId int NULL;
END



IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'RegisteredAtPharmacy'AND Object_ID = OBJECT_ID(N'mst_patient'))
    BEGIN
        ALTER TABLE mst_patient ADD RegisteredAtPharmacy int;
    END;
	
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ServiceRegisteredForAtPharmacy'AND Object_ID = OBJECT_ID(N'mst_patient'))
    BEGIN
        ALTER TABLE mst_patient ADD ServiceRegisteredForAtPharmacy int;
    END;