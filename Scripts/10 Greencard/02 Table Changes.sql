IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'VisitDate'AND Object_ID = OBJECT_ID(N'PregnancyIndicator'))
    BEGIN
        ALTER TABLE PregnancyIndicator ADD VisitDate DATETIME;
    END;
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'VisitDate'AND Object_ID = OBJECT_ID(N'PatientFamilyPlanning'))
    BEGIN
        ALTER TABLE PatientFamilyPlanning ADD VisitDate DATETIME;
    END;
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'VisitDate'AND Object_ID = OBJECT_ID(N'PatientScreening'))
    BEGIN
        ALTER TABLE PatientScreening ADD VisitDate DATETIME;
    END;
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'PatientMasterVisitId'AND Object_ID = OBJECT_ID(N'ord_PatientPharmacyOrder'))
    BEGIN
        ALTER TABLE ord_PatientPharmacyOrder ADD PatientMasterVisitId INT NULL;
    END;
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'PatientId'AND Object_ID = OBJECT_ID(N'ord_PatientPharmacyOrder'))
    BEGIN
        ALTER TABLE ord_PatientPharmacyOrder ADD PatientId INT NULL;
    END;
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'abbreviation'AND Object_ID = OBJECT_ID(N'Mst_ItemMaster'))
    BEGIN
        ALTER TABLE Mst_ItemMaster ADD abbreviation NVARCHAR(50) NULL;
    END;