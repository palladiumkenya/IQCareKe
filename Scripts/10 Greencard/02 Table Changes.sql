ALTER TABLE ord_LabOrder ALTER COLUMN OrderNumber VARCHAR(50) NULL
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
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'AuditData'AND Object_ID = OBJECT_ID(N'dtl_LabOrderTest'))
    BEGIN
        ALTER TABLE dtl_LabOrderTest ADD AuditData XML NULL;
    END;
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ResultUnits'AND Object_ID = OBJECT_ID(N'PatientLabTracker'))
    BEGIN
        ALTER TABLE PatientLabTracker ADD ResultUnits varchar(50) NULL;
    END;
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ResultDate'AND Object_ID = OBJECT_ID(N'PatientLabTracker'))
    BEGIN
        ALTER TABLE PatientLabTracker ADD ResultDate DATETIME;
    END;	
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ResultOptions'AND Object_ID = OBJECT_ID(N'PatientLabTracker'))
    BEGIN
        ALTER TABLE PatientLabTracker ADD ResultOptions varchar(50) NULL;
    END;	
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'AuditData'AND Object_ID = OBJECT_ID(N'ord_Visit'))
    BEGIN
        ALTER TABLE ord_Visit ADD AuditData xml NULL;
    END;
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'PatientId'AND Object_ID = OBJECT_ID(N'ord_LabOrder'))
    BEGIN
        ALTER TABLE ord_LabOrder ADD PatientId INT NULL;
    END;
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'PatientMasterVisitId'AND Object_ID = OBJECT_ID(N'ord_LabOrder'))
    BEGIN
        ALTER TABLE ord_LabOrder ADD	PatientMasterVisitId INT NULL;
    END;
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'AuditData'AND Object_ID = OBJECT_ID(N'ord_LabOrder'))
    BEGIN
        ALTER TABLE ord_LabOrder ADD AuditData xml NULL;
    END;
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'AuditData'AND Object_ID = OBJECT_ID(N'dtl_LabOrderTestResult'))
    BEGIN
        ALTER TABLE dtl_LabOrderTestResult ADD AuditData xml NULL;
    END;
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'CreateDate'AND Object_ID = OBJECT_ID(N'dtl_LabOrderTest'))
    BEGIN
        ALTER TABLE dtl_LabOrderTest ADD CreateDate datetime NULL;
    END;
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'CreatedBy'AND Object_ID = OBJECT_ID(N'dtl_LabOrderTest'))
    BEGIN
        ALTER TABLE dtl_LabOrderTest ADD CreatedBy int NULL;
    END;
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'RegimenCode'AND Object_ID = OBJECT_ID(N'PatientTreatmentInitiation'))
    BEGIN
        ALTER TABLE PatientTreatmentInitiation ADD RegimenCode int NULL;
    END;
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'IssuedQuantity'AND Object_ID = OBJECT_ID(N'Dtl_PurchaseItem'))
    BEGIN
        ALTER TABLE Dtl_PurchaseItem ADD IssuedQuantity int;
    END;
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'transactionType'AND Object_ID = OBJECT_ID(N'dtl_stocktransaction'))
    BEGIN
        ALTER TABLE dtl_stocktransaction ADD transactionType nvarchar(50);
    END;

