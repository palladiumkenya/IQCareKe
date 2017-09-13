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
	
If Not Exists (Select * From sys.columns Where Name = N'SpO2' And Object_ID = Object_id(N'PatientVitals'))    
Begin
  Alter table dbo.PatientVitals Add SpO2  decimal(7,2) Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'DifferentiatedCareId' And Object_ID = Object_id(N'PatientAppointment'))    
Begin
  Alter table dbo.PatientAppointment Add DifferentiatedCareId  int Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'DifferentiatedCareId' And Object_ID = Object_id(N'PatientAppointment'))    
Begin
  Alter table dbo.PatientAppointment Add DifferentiatedCareId  int Null
End
Go