Set Nocount On
Go
-- App versioning

If Not Exists (Select * From sys.columns Where Name = N'VersionName' And Object_ID = Object_id(N'AppAdmin'))    
Begin
  Alter table dbo.AppAdmin Add VersionName  varchar(50) Null
End
Go
Update AppAdmin Set VersionName= 'Kenya HMIS'
Go
Alter table dbo.AppAdmin Alter Column VersionName  varchar(50) Not Null
Go
Alter table dbo.AppAdmin Alter Column AppVer  varchar(50) Not Null
Go
Alter table dbo.AppAdmin Alter Column DBVer  varchar(50) Not Null
Go
Alter table dbo.AppAdmin Alter Column RelDate  datetime Not Null
Go
Alter table dtl_LabOrderTestResult drop column HasResult
Go
Alter table dtl_LabOrderTestResult alter column ResultValue [decimal](18,2)
Go
Alter table dtl_LabOrderTestResult alter column DetectionLimit [decimal](18,2)
Go
Alter table dtl_LabTestParameterConfig alter column [MinBoundary] [decimal](18, 2) NULL
Go
Alter table dtl_LabTestParameterConfig alter column [MaxBoundary] [decimal](18, 2) NULL
Go
Alter table dtl_LabTestParameterConfig alter column[MinNormalRange] [decimal](18, 2) NULL
Go
Alter table dtl_LabTestParameterConfig alter column [MaxNormalRange] [decimal](18, 2) NULL
Go	
Alter table dtl_LabTestParameterConfig alter column [DetectionLimit] [decimal](18, 2) NULL
Go
Alter table dtl_LabOrderTestResult add  [HasResult]  AS (CONVERT([bit],case when [resultvalue] IS NULL AND [resulttext] IS NULL AND [resultoption] IS NULL then (0) else (1) end,(0)))
Go
--Migrate the ART History filled in district from int to text
If not Exists (Select * From sys.columns Where Name = N'FromDistrict' And Object_ID = Object_id(N'dtl_PatientHivPrevCareIE') And system_type_id=TYPE_ID('varchar'))    
Begin
	ALTER TABLE [dbo].[dtl_PatientHivPrevCareIE] ADD [FromDistrict2] [varchar](200) NULL
End
GO
declare @d2 int, @d0 int;

If Exists (Select 1 From sys.columns Where Name = N'FromDistrict2' And Object_ID = Object_id(N'dtl_PatientHivPrevCareIE')  ) Begin
	If Exists (Select 1 From sys.columns Where Name = N'FromDistrict' And Object_ID = Object_id(N'dtl_PatientHivPrevCareIE')) Begin
	UPDATE [dbo].[dtl_PatientHivPrevCareIE] SET [FromDistrict2] = (SELECT [Name]  FROM [dbo].[mst_District]  where SystemId = 1 and ID = FromDistrict)
End 
End
Go
If not Exists (Select * From sys.columns Where Name = N'FromDistrict' And Object_ID = Object_id(N'dtl_PatientHivPrevCareIE')And system_type_id=TYPE_ID('varchar'))  Begin
	ALTER TABLE [dbo].[dtl_PatientHivPrevCareIE] DROP COLUMN [FromDistrict]
End
GO

If Exists (Select * From sys.columns Where Name = N'FromDistrict2' And Object_ID = Object_id(N'dtl_PatientHivPrevCareIE') And system_type_id=TYPE_ID('varchar'))    
Begin
	EXEC sp_RENAME '[dbo].[dtl_PatientHivPrevCareIE].[FromDistrict2]' , 'FromDistrict', 'COLUMN'
	End
	GO
--migrate the selected facility
If not Exists (Select * From sys.columns Where Name = N'ARTTransferInFrom' And Object_ID = Object_id(N'dtl_PatientHivPrevCareIE') And system_type_id=TYPE_ID('varchar'))    
Begin
	ALTER TABLE [dbo].[dtl_PatientHivPrevCareIE] ADD [ARTTransferInFrom2] [varchar](200) NULL
End
GO
If (not Exists (Select * From sys.columns Where Name = N'ARTTransferInFrom' 
											   And Object_ID = Object_id(N'dtl_PatientHivPrevCareIE')
											   And system_type_id=TYPE_ID('varchar'))
	AND Exists (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = N'mst_lptf'))					
Begin
UPDATE [dbo].[dtl_PatientHivPrevCareIE]
	   SET [ARTTransferInFrom2] = (SELECT [Name] FROM mst_lptf WHERE ID =  ARTTransferInFrom)
End
Go
If not Exists (Select * From sys.columns Where Name = N'ARTTransferInFrom' 
											   And Object_ID = Object_id(N'dtl_PatientHivPrevCareIE')
											   And system_type_id=TYPE_ID('varchar'))    
Begin
	ALTER TABLE [dbo].[dtl_PatientHivPrevCareIE] DROP COLUMN [ARTTransferInFrom]
	End
	GO

If Exists (Select * From sys.columns Where Name = N'ARTTransferInFrom2' 
											   And Object_ID = Object_id(N'dtl_PatientHivPrevCareIE')
											   And system_type_id=TYPE_ID('varchar'))    
Begin
	EXEC sp_RENAME '[dbo].[dtl_PatientHivPrevCareIE].[ARTTransferInFrom2]' , 'ARTTransferInFrom', 'COLUMN'
	End
	GO


If Not Exists (Select * From sys.columns Where Name = N'Id' And Object_ID = Object_id(N'dtl_PatientPharmacyOrder'))    
Begin
  Alter table dbo.dtl_PatientPharmacyOrder Add Id int Not Null Identity(1,1)
End
Go
/****** Object:  Index [IDX_dtl_PatientPharmacyOrder_CL1]    Script Date: 6/20/2016 1:22:38 PM ******/
If  Exists (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientPharmacyOrder]') AND name = N'IDX_dtl_PatientPharmacyOrder_CL1')   
DROP INDEX [IDX_dtl_PatientPharmacyOrder_CL1] ON [dbo].[dtl_PatientPharmacyOrder] WITH ( ONLINE = OFF )
GO

/****** Object:  Index [IDX_dtl_PatientPharmacyOrder_CL1]    Script Date: 6/20/2016 1:22:38 PM ******/
CREATE NonCLUSTERED INDEX [IDX_dtl_PatientPharmacyOrder_CL1] ON [dbo].[dtl_PatientPharmacyOrder]
(
	[ptn_pharmacy_pk] ASC,
	[Drug_Pk] ASC,
	[GenericID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = OFF, FILLFACTOR = 80)
GO

IF Not Exists (SELECT * FROM sys.key_constraints WHERE type = 'PK' AND parent_object_id = OBJECT_ID('dbo.dtl_PatientPharmacyOrder') AND Name = 'PK_dtl_PatientPharmacyOrder')
   ALTER TABLE [dbo].[dtl_PatientPharmacyOrder] ADD  CONSTRAINT [PK_dtl_PatientPharmacyOrder] PRIMARY KEY CLUSTERED 
	(
	[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO
IF  Exists (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ord_bill_Discount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ord_bill] DROP CONSTRAINT [DF_ord_bill_Discount]
End

GO
IF Exists (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LabOrderTestResult_LabTestParameterConfig]') AND parent_object_id = OBJECT_ID(N'[dbo].[dtl_LabOrderTestResult]'))
ALTER TABLE [dbo].[dtl_LabOrderTestResult] DROP CONSTRAINT [FK_LabOrderTestResult_LabTestParameterConfig]
GO
IF  Exists (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_LabOrderTestResult_LabTestParameterResultOption]') AND parent_object_id = OBJECT_ID(N'[dbo].[dtl_LabOrderTestResult]'))
ALTER TABLE [dbo].[dtl_LabOrderTestResult] DROP CONSTRAINT [FK_LabOrderTestResult_LabTestParameterResultOption]
GO

IF Exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Mst_LabTest]') AND type in (N'U')) Begin
	If Not Exists (Select * From sys.columns Where Name = N'Migrated' And Object_ID = Object_id(N'Mst_LabTest'))  Begin
	  Alter table dbo.Mst_LabTest Add Migrated bit Default 0
	End
	If Not Exists (Select * From sys.columns Where Name = N'DataType' And Object_ID = Object_id(N'mst_LabTest'))  Begin
		Alter table dbo.mst_LabTest Add DataType  varchar(20) Null
	End
	
End
Go
IF Exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lnk_TestParameter]') AND type in (N'U')) Begin
	If Not Exists (Select * From sys.columns Where Name = N'Migrated' And Object_ID = Object_id(N'lnk_TestParameter'))  Begin
	  Alter table dbo.lnk_TestParameter Add Migrated bit Default 0
	End
	
End
Go
IF Exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lnk_parameterresult]') AND type in (N'U')) Begin
	If Not Exists (Select * From sys.columns Where Name = N'Migrated' And Object_ID = Object_id(N'lnk_parameterresult'))  Begin
	  Alter table dbo.lnk_parameterresult Add Migrated bit Default 0
	End	
	If Not Exists (Select * From sys.columns Where Name = N'DeleteFlag' And Object_ID = Object_id(N'lnk_parameterresult')) Begin
		Alter table dbo.lnk_parameterresult Add DeleteFlag  int Null
	End
End
Go
IF Exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lnk_LabValue]') AND type in (N'U')) Begin
	If Not Exists (Select * From sys.columns Where Name = N'Migrated' And Object_ID = Object_id(N'lnk_LabValue'))  Begin
	  Alter table dbo.lnk_LabValue Add Migrated bit Default 0
	End	
End
Go
IF Exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ord_PatientLabOrder]') AND type in (N'U')) Begin
	If Not Exists (Select * From sys.columns Where Name = N'Migrated' And Object_ID = Object_id(N'ord_PatientLabOrder'))  Begin
	  Alter table dbo.ord_PatientLabOrder Add Migrated bit Default 0
	End	
	If Not Exists (Select * From sys.columns Where Name = N'ClinicalOrderNotes' And Object_ID = Object_id(N'ord_PatientLabOrder'))   Begin
		Alter table dbo.ord_PatientLabOrder Add ClinicalOrderNotes varchar(255) Null
	End
	If Not Exists (Select * From sys.columns Where Name = N'ModuleId' And Object_ID = Object_id(N'ord_PatientLabOrder')) Begin
		Alter table dbo.ord_PatientLabOrder Add ModuleId int Null
	End
End
Go
IF Exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dtl_patientlabresults]') AND type in (N'U')) Begin
	If Not Exists (Select * From sys.columns Where Name = N'Migrated' And Object_ID = Object_id(N'dtl_patientlabresults')) Begin
		Alter table dbo.dtl_PatientLabResults Add Migrated bit Default 0
	End
	If Not Exists (Select * From sys.columns Where Name = N'RequestNotes' And Object_ID = Object_id(N'dtl_PatientLabResults'))  Begin
		Alter table dbo.dtl_PatientLabResults Add RequestNotes varchar(255) Null
	End
	
End
Go
Alter table dbo.[mst_module] Alter Column [ModuleName] varchar(50) Not Null
Go
If Not Exists (Select * From sys.columns Where Name = N'HEIIDNumber' And Object_ID = Object_id(N'mst_Patient'))    
Begin
  Alter table dbo.mst_Patient Add HEIIDNumber varchar(50) Null
End
Go
IF Exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Mst_LabTest]') AND type in (N'U')) Begin
	update Mst_LabTest set DeleteFlag = 0 where DeleteFlag Is Null
	update lnk_LabValue set DeleteFlag = 0 where DeleteFlag Is Null
	update lnk_TestParameter set DeleteFlag = 0 where DeleteFlag Is Null
	update lnk_parameterresult set Result = nullif(result,'');
	Delete from lnk_parameterresult where Result Is Null
End
Go

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Mst_ItemMaster]') AND name = N'NCI_ItemMaster_ItemTypeID')
DROP INDEX [NCI_ItemMaster_ItemTypeID] ON [dbo].[Mst_ItemMaster] WITH ( ONLINE = OFF )
GO
If  Exists (Select * From sys.columns Where Name = N'DispensingMargin' And Object_ID = Object_id(N'Mst_ItemMaster'))    
Begin
  Alter table dbo.Mst_ItemMaster Alter Column DispensingMargin  decimal(18,2)
End
Go
If  Exists (Select * From sys.columns Where Name = N'DispensingUnitPrice' And Object_ID = Object_id(N'Mst_ItemMaster'))    
Begin
  Alter table dbo.Mst_ItemMaster Alter Column DispensingUnitPrice  decimal(18,2)
End
Go
If  Exists (Select * From sys.columns Where Name = N'PurchaseUnitPrice' And Object_ID = Object_id(N'Mst_ItemMaster'))    
Begin
  Alter table dbo.Mst_ItemMaster Alter Column PurchaseUnitPrice  decimal(18,2)
End
Go
 If Not Exists (Select * From sys.columns Where Name = N'DeleteFlag' And Object_ID = object_id(N'Mst_PreDefinedFields')) Begin
	Alter Table dbo.Mst_PreDefinedFields Add DeleteFlag int 
End
Go
Update Mst_PreDefinedFields Set		DeleteFlag = 0 Where DeleteFlag Is Null

Go
  Alter table dbo.Mst_PreDefinedFields alter column DeleteFlag  int  not null
Go
 If Not Exists (Select 1      from sys.all_columns c join sys.tables t on t.object_id = c.object_id join sys.schemas s on s.schema_id = t.schema_id join sys.default_constraints d on c.default_object_id = d.object_id
		where t.name ='Mst_PreDefinedFields'    And c.name = 'DeleteFlag')
      Begin
		Alter table [Mst_PreDefinedFields] ADD CONSTRAINT DF_Mst_PreDefinedFields_DeleteFlag DEFAULT 0 FOR DeleteFlag
	 End
Go
If Not Exists (Select * From sys.columns Where Name = N'RequiredFlag' And Object_ID = Object_id(N'lnk_PatientModuleIdentifier'))    
Begin
  Alter table dbo.lnk_PatientModuleIdentifier Add RequiredFlag  bit Default 0
End
Go
If Not Exists (Select * From sys.columns Where Name = N'SP02' And Object_ID = Object_id(N'dtl_PatientVitals'))    
Begin
  Alter table dbo.dtl_PatientVitals Add SP02  decimal(7,2) Null
End
Go

If Not Exists (Select * From sys.columns Where Name = N'Active' And Object_ID = Object_id(N'Mst_ClinicalService'))    
Begin
  Alter table dbo.Mst_ClinicalService Add Active bit Default 1
End
Go
If Not Exists (Select * From sys.columns Where Name = N'DeletedBy' And Object_ID = Object_id(N'Mst_ClinicalService'))    
Begin
  Alter table dbo.Mst_ClinicalService Add DeletedBy int null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'DeleteDate' And Object_ID = Object_id(N'Mst_ClinicalService'))    
Begin
  Alter table dbo.Mst_ClinicalService Add DeleteDate datetime NULL
End
Go
If Not Exists (Select * From sys.columns Where Name = N'Active' And Object_ID = Object_id(N'mst_LabTestMaster'))    
Begin
  Alter table dbo.mst_LabTestMaster Add Active bit Default 1
End
Go
If Not Exists (Select * From sys.columns Where Name = N'LoincCode' And Object_ID = Object_id(N'mst_LabTestMaster'))    
Begin
  Alter table dbo.mst_LabTestMaster Add [LoincCode] [varchar](50) NULL
End
Go
If Not Exists (Select * From sys.columns Where Name = N'DeletedBy' And Object_ID = Object_id(N'mst_LabTestMaster'))    
Begin
  Alter table dbo.mst_LabTestMaster Add DeletedBy int null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'DeleteDate' And Object_ID = Object_id(N'mst_LabTestMaster'))    
Begin
  Alter table dbo.mst_LabTestMaster Add DeleteDate datetime NULL
End
Go
If Not Exists (Select * From sys.columns Where Name = N'DeletedBy' And Object_ID = Object_id(N'ord_labOrder'))    
Begin
  Alter table dbo.ord_labOrder Add DeletedBy int NULL
End
Go
If Not Exists (Select * From sys.columns Where Name = N'DeleteDate' And Object_ID = Object_id(N'ord_labOrder'))    
Begin
  Alter table dbo.ord_labOrder Add DeleteDate datetime NULL
End
Go
If Not Exists (Select * From sys.columns Where Name = N'DeleteReason' And Object_ID = Object_id(N'ord_labOrder'))    
Begin
  Alter table dbo.ord_labOrder Add DeleteReason varchar(250) NULL
End
Go
If Not Exists (Select * From sys.columns Where Name = N'SettledAmount' And Object_ID = Object_id(N'mst_bill'))    
Begin
  Alter table dbo.mst_bill Add SettledAmount Decimal(18,2) Default 0
End
Go
If Not Exists (Select * From sys.columns Where Name = N'Discount' And Object_ID = Object_id(N'mst_bill'))    
Begin
  Alter table dbo.mst_bill Add Discount Decimal(18,2) Default 0
End
Go
If Not Exists (Select * From sys.columns Where Name = N'PricePlanId' And Object_ID = Object_id(N'lnk_ItemCostConfiguration'))    
Begin
  Alter table dbo.lnk_ItemCostConfiguration Add PricePlanId int Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'CanEnroll' And Object_ID = Object_id(N'mst_module'))    
Begin
  Alter table dbo.mst_module Add CanEnroll bit Null
End
Go
Update mst_Module Set CanEnroll = Case When ModuleName In ('PM/SCM','Laboratory','Pharmacy','Records')  Then 0 Else 1 End Where CanEnroll Is Null
Go

If Not Exists (Select * From sys.columns Where Name = N'DisplayName' And Object_ID = Object_id(N'mst_module'))    
Begin
  Alter table dbo.mst_module Add DisplayName varchar(50) Null
End
Go
Update mst_Module Set DisplayName = ModuleName Where DisplayName Is Null
Go
If Not Exists (Select * From sys.columns Where Name = N'Discount' And Object_ID = Object_id(N'ord_bill'))    
Begin
  Alter table dbo.ord_bill Add Discount Decimal(3,2) Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'Settled' And Object_ID = Object_id(N'ord_bill'))    
Begin
  Alter table dbo.ord_bill Add Settled bit Default 0 not null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'AmountSettled' And Object_ID = Object_id(N'ord_bill'))    
Begin
  Alter table dbo.ord_bill Add AmountSettled decimal(18,2) Null
End
Go
Update B Set
		Settled = Case When P.TypeName In ('Cash', 'Waiver', 'Deposit', 'Cheque', 'Writeoff') Then 1 Else 0 End
	,	AmountSettled = Case When P.TypeName In ('Cash', 'Waiver', 'Deposit', 'Cheque', 'Writeoff') Then AmountPayable Else 0.0 End
From ord_bill B
Inner Join Mst_BillPaymentType P On B.TransactionType = P.TypeID
Where  B.Settled = 0;
Go
If Not Exists (Select * From sys.columns Where Name = N'Narrative' And Object_ID = Object_id(N'ord_bill'))    
Begin
  Alter table dbo.ord_bill Add Narrative varchar(50) Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'CostCenter' And Object_ID = Object_id(N'dtl_bill'))    
Begin
  Alter table dbo.dtl_bill Add CostCenter varchar(50) Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'OtherCriteria' And Object_ID = Object_id(N'dtl_PatientARVEligibility'))    
Begin
  Alter table dbo.dtl_PatientARVEligibility Add OtherCriteria varchar(100) Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'Muac' And Object_ID = Object_id(N'dtl_PatientVitals'))    
Begin
  Alter table dbo.dtl_PatientVitals Add Muac Decimal(4,1) Null
End
Go
ALTER TABLE [dbo].[ord_bill] ADD  CONSTRAINT [DF_ord_bill_Discount]  DEFAULT ((0.0)) FOR [Discount]
GO
If Not Exists (Select * From sys.columns Where Name = N'qryDescription' And Object_ID = Object_id(N'mst_QueryBuilderReports'))    
Begin
  Alter table dbo.mst_QueryBuilderReports Add qryDescription varchar(200)
End
Go
If Not Exists (Select * From sys.columns Where Name = N'PatientFacilityId' And Object_ID = Object_id(N'mst_Patient'))    
Begin
  Alter table dbo.mst_Patient Add PatientFacilityId  varchar(50) Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'Integrated' And Object_ID = Object_id(N'mst_Facility'))    
Begin
  Alter table dbo.mst_Facility Add Integrated  bit not null Constraint DF_mst_facility_Integrated Default 0
End
Go
If Not Exists (Select * From sys.columns Where Name = N'DateOfDeath' And Object_ID = Object_id(N'mst_Patient'))    
Begin
  Alter table dbo.mst_Patient Add DateOfDeath  datetime Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'LandMark' And Object_ID = Object_id(N'mst_Patient'))    
Begin
  Alter table dbo.mst_Patient Add LandMark  varchar(50) Null
End
Go

If Not Exists (Select * From sys.columns Where Name = N'VersionStamp' And Object_ID = Object_id(N'lnk_ItemCostConfiguration'))    
Begin
  Alter table dbo.lnk_ItemCostConfiguration Add VersionStamp  timestamp not null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'ItemCode' And Object_ID = Object_id(N'Mst_ItemMaster'))    
Begin
  Alter table dbo.Mst_ItemMaster Add ItemCode  varchar(50) Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'CanLink' And Object_ID = Object_id(N'Mst_Feature'))    
Begin
  Alter table dbo.Mst_Feature Add CanLink  bit Default 0
End
Go

If Not Exists (Select * From sys.columns Where Name = N'Custom' And Object_ID = Object_id(N'Mst_VisitType'))    
Begin
  Alter table dbo.Mst_VisitType Add Custom  bit  default 1
End
Go
Update Mst_VisitType Set Custom =0 Where Custom Is Null
Go
 Alter table dbo.Mst_VisitType Alter Column Custom  bit  not null
 Go
If Not Exists (Select * From sys.columns Where Name = N'CategoryId' And Object_ID = Object_id(N'Mst_VisitType'))    
Begin
  Alter table dbo.Mst_VisitType Add CategoryId  int  null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'FormDescription' And Object_ID = Object_id(N'Mst_VisitType'))    
Begin
  Alter table dbo.Mst_VisitType Add FormDescription varchar(100) Null
End
Go
Update Mst_VisitType Set FormDescription = VisitName Where FormDescription Is Null
Go
If Not Exists (Select * From sys.columns Where Name = N'FeatureTypeId' And Object_ID = Object_id(N'Mst_Feature'))    
Begin
  Alter table dbo.Mst_Feature Add FeatureTypeId  int Null
End
Go

If Not Exists (Select * From sys.columns Where Name = N'ReferenceId' And Object_ID = Object_id(N'Mst_Feature'))    
Begin
  Alter table dbo.Mst_Feature Add ReferenceId  varchar(50) Null
End
Go
Update mst_Feature Set CanLink = 1 Where ReferenceId In ('LABORATORY','PHARMACY','FOLLOWUP_EDUCATION','CONSUMABLES_ISSUANCE') And CanLink Is Null;
go
Alter table dbo.Mst_Feature Alter Column ReferenceId  varchar(50) Null
Go
If Not Exists (Select * From sys.columns Where Name = N'ReferenceId' And Object_ID = Object_id(N'Mst_Control'))    
Begin
  Alter table dbo.Mst_Control Add ReferenceId  varchar(36) Null 
End
Go
If Not Exists (Select * From sys.columns Where Name = N'LookupTable' And Object_ID = Object_id(N'Mst_Control'))    
Begin
  Alter table dbo.Mst_Control Add LookupTable  varchar(36) Null 
End
Go
If Not Exists (Select * From sys.columns Where Name = N'ReferenceId' And Object_ID = Object_id(N'Mst_BusinessRule'))    
Begin
  Alter table dbo.Mst_BusinessRule Add ReferenceId  varchar(50) Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'SectionInfo' And Object_ID = Object_id(N'Mst_Section'))    
Begin
  Alter table dbo.Mst_Section Add SectionInfo  varchar(255) Null 
End
Go
 Update mst_Control Set ReferenceId = Convert(varchar(36), newid()) Where ReferenceId Is Null
 GO
If Not Exists (Select * From sys.columns Where Name = N'ItemName' And Object_ID = Object_id(N'dtl_PatientItemsOrder'))    
Begin
  Alter table dbo.dtl_PatientItemsOrder Add ItemName  varchar(250) Null
End
Go

IF  Exists (SELECT * FROM sys.key_constraints WHERE type = 'PK' AND parent_object_id = OBJECT_ID('dbo.dtl_PatientBlueCardPriorART') AND Name = 'PK_dtl_PatientBlueCardPriorART')
	ALTER TABLE [dbo].[dtl_PatientBlueCardPriorART] DROP CONSTRAINT [PK_dtl_PatientBlueCardPriorART]
Go
If Not Exists (Select * From sys.columns Where Name = N'Id' And Object_ID = Object_id(N'dtl_PatientBlueCardPriorART'))    
Begin
  Alter table dbo.dtl_PatientBlueCardPriorART Add  Id  int Identity(1,1)
End
Go
IF Not Exists (SELECT * FROM sys.key_constraints WHERE type = 'PK' AND parent_object_id = OBJECT_ID('dbo.dtl_PatientBlueCardPriorART') AND Name = 'PK_dtl_PatientBlueCardPriorART')
   ALTER TABLE [dbo].[dtl_PatientBlueCardPriorART] ADD  CONSTRAINT [PK_dtl_PatientBlueCardPriorART] PRIMARY KEY CLUSTERED 
	(
	[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO
IF Not Exists (SELECT * FROM sys.key_constraints WHERE type = 'PK' AND parent_object_id = OBJECT_ID('dbo.mst_module') AND Name = 'PK_mst_module')
   ALTER TABLE [dbo].[mst_module] ADD  CONSTRAINT [PK_mst_module] PRIMARY KEY CLUSTERED 
	(
	[ModuleId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO

If Not Exists (Select * From sys.columns Where Name = N'Credit' And Object_ID = Object_id(N'Mst_BillPaymentType'))    
Begin
  Alter table dbo.Mst_BillPaymentType Add  Credit  bit Null
End
Go

Update Mst_BillPaymentType Set
Credit = Case When TypeName In('Cash','Deposit','Waiver', 'Exemption', 'Exempt') Then 0 Else 1 End
Where Credit is Null

IF Not Exists (select name from sys.default_constraints   where parent_object_id = object_id(N'Mst_BillPaymentType') 
and parent_column_id = columnproperty(object_id(N'Mst_BillPaymentType'), N'Credit', 'ColumnId'))
Begin

Alter Table dbo.Mst_BillPaymentType add Constraint DF_Mst_BillPaymentType_Credit Default 1 for Credit
End
Go

If Not Exists (Select * From sys.columns Where Name = N'StoreCategory' And Object_ID = Object_id(N'Mst_Store'))    
Begin
  Alter table dbo.Mst_Store Add StoreCategory  varchar(50) Null
End
Go

 If Not Exists (Select * From sys.columns Where Name = N'WhyPartial' And Object_ID = Object_id(N'Dtl_PatientPharmacyOrder'))    
Begin
  Alter table dbo.Dtl_PatientPharmacyOrder Add WhyPartial  varchar(250) Null
End
Go

If Not Exists (Select * From sys.columns Where Name = N'ItemInstructions' And Object_ID = Object_id(N'Mst_ItemMaster'))    
Begin
  Alter table dbo.Mst_ItemMaster Add ItemInstructions  varchar(250) Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'ItemInstructions' And Object_ID = Object_id(N'Mst_Drug'))    
Begin
  Alter table dbo.Mst_Drug Add ItemInstructions  varchar(250) Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'CreatedBy' And Object_ID = Object_id(N'Ord_Visit'))    
Begin
  Alter table dbo.Ord_Visit Add CreatedBy  int Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'old_signature_employee_id' And Object_ID = Object_id(N'Ord_Visit'))    
Begin
  Alter table dbo.Ord_Visit Add old_signature_employee_id  int Null
End
Go
Update dbo.ord_Visit Set old_signature_employee_id = Isnull(old_signature_employee_id,Signature) where old_signature_employee_id Is Null and Signature Is Not Null
Go
If Not Exists (Select * From sys.columns Where Name = N'ModuleId' And Object_ID = Object_id(N'dtl_PatientAppointment'))    
Begin
  Alter table dbo.dtl_PatientAppointment Add ModuleId  int Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'AppNote' And Object_ID = Object_id(N'dtl_PatientAppointment'))    
Begin
  Alter table dbo.dtl_PatientAppointment Add AppNote  varchar(250) Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'UpdateUserId' And Object_ID = Object_id(N'dtl_PatientAppointment'))    
Begin
  Alter table dbo.dtl_PatientAppointment Add UpdateUserId  int Null
End
Go
If Exists(SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientAppointment]') and name ='IX_PatientAppointment_PtnPk_OT')
DROP INDEX [IX_PatientAppointment_PtnPk_OT] ON [dbo].[dtl_PatientAppointment]
GO
If Exists(SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientAppointment]') and name ='NCI_DTL_PatientAppointment_VisitPK_INC')
DROP INDEX [NCI_DTL_PatientAppointment_VisitPK_INC] ON [dbo].[dtl_PatientAppointment]
GO

IF  Exists (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientAppointment]') AND name = N'NCI_DTL_PatientAppointment_DeleteFlag_INC')
DROP INDEX [NCI_DTL_PatientAppointment_DeleteFlag_INC] ON [dbo].[dtl_PatientAppointment] WITH ( ONLINE = OFF )
GO
IF  Exists (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientAppointment]') AND name = N'NCI_Appointment_LocStatus')
DROP INDEX [NCI_Appointment_LocStatus] ON [dbo].[dtl_PatientAppointment] WITH ( ONLINE = OFF )
GO
IF  Exists (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientAppointment]') AND name = N'NCI_dtlAppointment_status')
DROP INDEX [NCI_dtlAppointment_status] ON [dbo].[dtl_PatientAppointment] WITH ( ONLINE = OFF )
GO
IF  Exists (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientAppointment]') AND name = N'NCI_Appointment_Ptn_Stat_Date_DelFlag')
DROP INDEX [NCI_Appointment_Ptn_Stat_Date_DelFlag] ON [dbo].[dtl_PatientAppointment] WITH ( ONLINE = OFF )
GO
IF  Exists (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientAppointment]') AND name = N'NCI_DTL_PatientAppointment_PK_INC')
DROP INDEX [NCI_DTL_PatientAppointment_VisitPK_INC] ON [dbo].[dtl_PatientAppointment] WITH ( ONLINE = OFF )
GO
delete from dtl_PatientAppointment where AppDate is null or appstatus is null or userid is null
Go
If  Exists (Select * From sys.columns Where Name = N'AppDate' And Object_ID = Object_id(N'dtl_PatientAppointment'))    
Begin
  Alter table dbo.dtl_PatientAppointment Alter Column AppDate  datetime Not  Null
End
Go
 If  Exists (Select * From sys.columns Where Name = N'Code' And Object_ID = Object_id(N'Mst_Decode'))    
Begin
  Alter table dbo.Mst_Decode Alter Column Code  varchar(15)   Null
End
Go
If  Exists (Select * From sys.columns Where Name = N'AppStatus' And Object_ID = Object_id(N'dtl_PatientAppointment'))    
Begin
  Alter table dbo.dtl_PatientAppointment Alter Column AppStatus  int Not  Null
End
Go

If  Exists (Select * From sys.columns Where Name = N'UserId' And Object_ID = Object_id(N'dtl_PatientAppointment'))    
Begin
  Alter table dbo.dtl_PatientAppointment Alter Column UserId  int Not  Null
End
Go
If  Exists (Select * From sys.columns Where Name = N'CreateDate' And Object_ID = Object_id(N'dtl_PatientAppointment'))    
Begin
  Alter table dbo.dtl_PatientAppointment Alter Column CreateDate  datetime Not  Null
End
Go
If  Exists (Select * From sys.columns Where Name = N'DeleteFlag' And Object_ID = Object_id(N'dtl_PatientAppointment'))    
Begin
 Update dbo.dtl_PatientAppointment Set DeleteFlag = 0 Where DeleteFlag Is null  
End
Go

If  Exists (Select 1      from sys.all_columns c join sys.tables t on t.object_id = c.object_id join sys.schemas s on s.schema_id = t.schema_id join sys.default_constraints d on c.default_object_id = d.object_id
		where t.name ='dtl_PatientAppointment'    And c.name = 'deleteflag')
      Begin
		Alter table [dtl_PatientAppointment] Drop CONSTRAINT DF_dtl_PatientAppointment_DeleteFlag 
	 End
Go

If  Exists (Select * From sys.columns Where Name = N'DeleteFlag' And Object_ID = Object_id(N'dtl_PatientAppointment') And  system_type_id=TYPE_ID('int'))    
Begin
  Alter table dbo.dtl_PatientAppointment Alter Column DeleteFlag  bit Not  Null
End
Go	 
	If Not Exists (Select 1      from sys.all_columns c join sys.tables t on t.object_id = c.object_id join sys.schemas s on s.schema_id = t.schema_id join sys.default_constraints d on c.default_object_id = d.object_id
		where t.name ='dtl_PatientAppointment'    And c.name = 'deleteflag')
      Begin
		Alter table [dtl_PatientAppointment] ADD CONSTRAINT DF_dtl_PatientAppointment_DeleteFlag DEFAULT 0 FOR DeleteFlag
	 End
Go
CREATE NONCLUSTERED INDEX [NCI_DTL_PatientAppointment_VisitPK_INC] ON [dbo].[dtl_PatientAppointment]
(
	[Visit_pk] ASC,
	[DeleteFlag] ASC
)
INCLUDE ( 	[AppDate],
	[AppReason]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_PatientAppointment_PtnPk_OT] ON [dbo].[dtl_PatientAppointment]
(
	[Ptn_pk] ASC,
	[AppStatus] ASC,
	[DeleteFlag] ASC,
	[AppDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
If Not Exists (Select * From sys.columns Where Name = N'ModuleId' And Object_ID = Object_id(N'ord_Visit'))    
Begin
  Alter table dbo.ord_Visit Add ModuleId  int Null
End
Go

If Not Exists (Select * From sys.columns Where Name = N'DateEnrolledInCare' And Object_ID = Object_id(N'dtl_PatientHivPrevCareEnrollment'))    
Begin
  Alter table dbo.dtl_PatientHivPrevCareEnrollment Add DateEnrolledInCare  datetime Null
End
Go

/****** Object:  Table [dbo].[Mst_ItemMaster]    Script Date: 06/13/2014 14:48:45 ******/
IF  Exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Mst_Drug]') AND type in (N'U'))
Exec dbo.sp_rename 'Mst_Drug', 'Mst_Drug_Bill'
GO

-- Drop column Zero
If  Exists (Select * From sys.columns Where Name = N'0' And Object_ID = Object_id(N'mst_patient'))    
Begin
  Alter table dbo.mst_patient drop Column [0]  
End
Go
-- Change all identifier columns to varchar(50) 
declare @T Table(fieldname varchar(50)); 
declare @F varchar(50), @query varchar (700);

Insert Into @T
Select I.FieldName
From mst_PatientIdentifier I;

While ((Select Count(*) From @T) > 0)
Begin
	Select Top 1 @f=  fieldname From @T;
	
	If Exists (Select name	From syscolumns	Where name = @f	And Object_name(id) = 'mst_patient') 
	Begin
		Select @query ='ALTER TABLE mst_patient Alter Column [' + @f + '] varchar(50)';
		Exec (@query);
	End
	Delete From @T where fieldname=@f;
End

Go
/*
If Not Exists (Select * From sys.columns Where Name = N'DeleteFlag' And Object_ID = Object_id(N'lnk_parameterresult'))    
Begin
	Alter Table dbo.lnk_parameterresult Add	DeleteFlag int Not Null Constraint DF_lnk_parameterresult_DeleteFlag Default 0
End
GO*/
--add Versionstamp column in lnk_ItemCostConfiguration

If Not Exists (Select * From sys.columns Where Name = N'VersionStamp' And Object_ID = Object_id(N'lnk_ItemCostConfiguration'))    
Begin
  Alter table dbo.lnk_ItemCostConfiguration Add VersionStamp timestamp
End
Go

If Not Exists (Select * From sys.columns Where Name = N'CreateDate' And Object_ID = Object_id(N'Ord_AdjustStock'))    
Begin
  Alter table dbo.Ord_AdjustStock Add CreateDate  datetime Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'UserId' And Object_ID = Object_id(N'Ord_AdjustStock'))    
Begin
  Alter table dbo.Ord_AdjustStock Add UserId  int Null
End
Go
Update dbo.Ord_AdjustStock Set 
	Createdate = Isnull(CreateDate,AdjustmentDate), 
	UserId = Isnull(UserId,AdjustmentPreparedBy) 
Where (CreateDate Is Null Or UserId Is Null);
Go
 Alter table dbo.Ord_AdjustStock Alter Column CreateDate  datetime Not Null 
Go
 Alter table dbo.Ord_AdjustStock Alter Column LocationId  int Not Null 
 Go
  Alter table dbo.Ord_AdjustStock Alter Column StoreId  int Not Null 
 Go
  Alter table dbo.Ord_AdjustStock Alter Column AdjustmentDate  datetime Not Null 
 Go
  Alter table dbo.Ord_AdjustStock Alter Column UserId  int Not Null 
 Go
 
 IF Not Exists (SELECT * FROM sys.columns WHERE Name = 'Id' AND object_id = OBJECT_ID('dbo.Dtl_AdjustStock'))
    ALTER TABLE dbo.Dtl_AdjustStock Add Id int Identity(1,1);
GO

IF Not Exists (SELECT * FROM sys.key_constraints WHERE type = 'PK' AND parent_object_id = OBJECT_ID('dbo.Dtl_AdjustStock') AND Name = 'PK_Dtl_AdjustStock')
   ALTER TABLE [dbo].[Dtl_AdjustStock] ADD  CONSTRAINT [PK_Dtl_AdjustStock] PRIMARY KEY CLUSTERED 
	(
	[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO
Alter table dbo.Dtl_AdjustStock Alter Column AdjustId	int	Not Null
Go
Alter table dbo.Dtl_AdjustStock Alter Column ItemId	int	Not Null
Go
Alter table dbo.Dtl_AdjustStock Alter Column StoreId	int	Not Null
Go
Alter table dbo.Dtl_AdjustStock Alter Column BatchId	int	Not Null 
Go
Alter table dbo.Dtl_AdjustStock Alter Column ExpiryDate	datetime	Not Null
Go
Alter table dbo.Dtl_AdjustStock Alter Column AdjustReasonId	int	Not Null
Go
Alter table dbo.Dtl_AdjustStock Alter Column AdjustmentQuantity	int	Not Null
Go


--remove Records,billing, and Wards to mst_facility for configuration	 use modules
If Exists(SELECT * FROM sys.default_constraints WHERE  parent_object_id = OBJECT_ID('dbo.mst_facility') And name='DF_mst_facility_Billing')
ALTER TABLE [dbo].[mst_Facility] DROP CONSTRAINT [DF_mst_facility_Billing]
GO
If  Exists (Select * From sys.columns Where Name = N'Billing' And Object_ID = Object_id(N'mst_Facility'))
ALTER TABLE [dbo].[mst_Facility] DROP COLUMN [Billing]
GO
If  Exists (Select * From sys.columns Where Name = N'Wards' And Object_ID = Object_id(N'mst_Facility'))
ALTER TABLE [dbo].[mst_Facility] DROP COLUMN [Wards]
GO
If  Exists (Select * From sys.columns Where Name = N'Records' And Object_ID = Object_id(N'mst_Facility'))
ALTER TABLE [dbo].[mst_Facility] DROP COLUMN [Records]
GO
If Exists (Select * From sys.columns Where Name = N'DeleteFlag'  And Object_ID = Object_id(N'mst_Patient') And system_type_id=TYPE_ID('int'))    Begin
	If Exists(SELECT * FROM sys.default_constraints WHERE  parent_object_id = OBJECT_ID('dbo.mst_Patient') And name='DF_mst_Patient_DeleteFlag') Begin 
		Alter table [mst_Patient] Drop CONSTRAINT DF_mst_Patient_DeleteFlag 

	End
End
Go
If Exists (Select * From sys.columns Where Name = N'DeleteFlag'  And Object_ID = Object_id(N'mst_Patient') And system_type_id=TYPE_ID('int'))    Begin
	IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[mst_Patient]') AND name = N'NCI_MSTPatient_DeleteFlag')
	DROP INDEX [NCI_MSTPatient_DeleteFlag] ON [dbo].[mst_Patient] WITH ( ONLINE = OFF )
End
GO

If Exists (Select * From sys.columns Where Name = N'DeleteFlag'  And Object_ID = Object_id(N'mst_Patient') And system_type_id=TYPE_ID('int'))    Begin
	Alter table dbo.[mst_Patient] Alter Column [DeleteFlag] bit
End
Go
If Exists (Select * From sys.columns Where Name = N'DeleteFlag'  And Object_ID = Object_id(N'mst_module') And system_type_id=TYPE_ID('int'))    Begin
	If Exists(SELECT * FROM sys.default_constraints WHERE  parent_object_id = OBJECT_ID('dbo.mst_module') And name='DF_mst_module_DeleteFlag') Begin 
		Alter table [mst_module] Drop CONSTRAINT DF_mst_module_DeleteFlag 

	End
End
Go
If Exists (Select * From sys.columns Where Name = N'DeleteFlag'  And Object_ID = Object_id(N'mst_module') And system_type_id=TYPE_ID('int'))    Begin
	Alter table dbo.[mst_module] Alter Column [DeleteFlag] bit
End
Go
If Exists (Select * From sys.columns Where Name = N'DeleteFlag'  And Object_ID = Object_id(N'Mst_QueryBuilderCategory') And system_type_id=TYPE_ID('int'))    Begin
	If Exists(SELECT * FROM sys.default_constraints WHERE  parent_object_id = OBJECT_ID('dbo.Mst_QueryBuilderCategory') And name='DF_Mst_QueryBuilderCategory_DeleteFlag') Begin 
		Alter table [Mst_QueryBuilderCategory] Drop CONSTRAINT DF_Mst_QueryBuilderCategory_DeleteFlag 

	End
End
Go
If Exists (Select * From sys.columns Where Name = N'DeleteFlag'  And Object_ID = Object_id(N'Mst_QueryBuilderCategory') And system_type_id=TYPE_ID('int'))    Begin
	Alter table dbo.[Mst_QueryBuilderCategory] Alter Column [DeleteFlag] bit
End
Go


If Exists (Select * From sys.columns Where Name = N'DeleteFlag'  And Object_ID = Object_id(N'mst_QueryBuilderReports') And system_type_id=TYPE_ID('int'))    Begin
	If Exists(SELECT * FROM sys.default_constraints WHERE  parent_object_id = OBJECT_ID('dbo.mst_QueryBuilderReports') And name='DF_Mst_QueryBuilderReports_DeleteFlag') Begin 
		Alter table [mst_QueryBuilderReports] Drop CONSTRAINT DF_Mst_QueryBuilderReports_DeleteFlag 

	End
End
Go
If Exists (Select * From sys.columns Where Name = N'DeleteFlag'  And Object_ID = Object_id(N'mst_QueryBuilderReports') And system_type_id=TYPE_ID('int'))    Begin
	Alter table dbo.[mst_QueryBuilderReports] Alter Column [DeleteFlag] bit
End
Go
/*If Not Exists (Select * From sys.columns Where Name = N'Records' And Object_ID = Object_id(N'mst_Facility'))    
Begin
  Alter table dbo.mst_facility Add Records  int Null
End
Go
If Not Exists (Select * From sys.columns Where Name = N'Wards' And Object_ID = Object_id(N'mst_Facility'))    
Begin
  Alter table dbo.mst_facility Add Wards  int Null
End
Go
	Delete From mst_facility Where DeleteFlag = 1;
GO
If  Exists (Select * From sys.columns Where Name = N'Billing' And Object_ID = Object_id(N'mst_Facility'))  Begin
  Alter table dbo.mst_facility Add Billing  bit Not Null Constraint DF_mst_facility_Billing Default 0
End
Go	 */
If Not Exists (Select * From sys.columns Where Name = N'LocationId' And Object_ID = Object_id(N'mst_Store')) Begin
  Alter table dbo.mst_Store Add LocationId  int Null
End
GO
If Not Exists (Select * From sys.columns Where Name = N'Active' And Object_ID = Object_id(N'mst_Store')) Begin
  Alter table dbo.mst_Store Add Active  bit  Not Null Constraint DF_mst_Store_ActiveFlag Default 1
End
GO
Update Mst_Store Set LocationId = (Select Top 1 F.FacilityId From mst_Facility F Where F.DeleteFlag = 0)
Go
Alter table dbo.mst_Store Alter Column LocationId  int Not Null
Go
If not Exists (Select * From sys.columns Where Name = N'Comments' And Object_ID = Object_id(N'dtl_FollowupEducation')) Begin
  Alter table dbo.dtl_FollowupEducation Add Comments varchar(255) Null
End
If Not  Exists (Select * From sys.columns Where Name = N'OtherDetail' And Object_ID = Object_id(N'dtl_FollowupEducation'))  Begin
  Alter table dbo.dtl_FollowupEducation Add OtherDetail varchar(255) Null
End

IF Not Exists (SELECT * FROM sys.key_constraints WHERE type = 'PK' AND parent_object_id = OBJECT_ID('dbo.dtl_FollowupEducation') )
   ALTER TABLE [dbo].[dtl_FollowupEducation] ADD  CONSTRAINT [PK_dtl_FollowupEducation] PRIMARY KEY CLUSTERED 
	(
	[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO
-- drop column if it already Exists
IF Not Exists (SELECT * FROM sys.columns WHERE Name = 'AppointmentId' AND object_id = OBJECT_ID('dbo.dtl_PatientAppointment'))
    ALTER TABLE dbo.dtl_PatientAppointment Add AppointmentId int Identity(1,1);
GO

IF Not Exists (SELECT * FROM sys.key_constraints WHERE type = 'PK' AND parent_object_id = OBJECT_ID('dbo.dtl_PatientAppointment') AND Name = 'PK_dtl_PatientAppointment')
   ALTER TABLE [dbo].[dtl_PatientAppointment] ADD  CONSTRAINT [PK_dtl_PatientAppointment] PRIMARY KEY CLUSTERED 
	(
	[AppointmentID] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)

GO
--Update mst_facility Set Billing = 1, Wards=1 Where DeleteFlag = 0;
--Go

IF Exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientLabResults]') AND type in (N'U'))
Begin		
	  Exec dbo.sp_rename 'dtl_PatientLabResults', 'dtl_PatientLabResults_Old'	
End
Go
IF Exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Mst_LabTest]') AND type in (N'U'))
Begin		
	  Exec dbo.sp_rename 'Mst_LabTest', 'Mst_LabTest_Old'	
End
Go
IF Exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lnk_TestParameter]') AND type in (N'U'))
Begin		
	  Exec dbo.sp_rename 'lnk_TestParameter', 'lnk_TestParameter_Old'	
End
Go
IF Exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ord_PatientLabOrder]') AND type in (N'U'))
Begin		
	  Exec dbo.sp_rename 'ord_PatientLabOrder', 'ord_PatientLabOrder_Old'	
End
Go
IF Exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lnk_parameterresult]') AND type in (N'U'))
Begin		
	  Exec dbo.sp_rename 'lnk_parameterresult', 'lnk_parameterresult_Old'	
End
Go
IF Exists (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lnk_LabValue]') AND type in (N'U'))
Begin		
	  Exec dbo.sp_rename 'lnk_LabValue', 'lnk_LabValue_Old'	
End
Go
;
With md
As
(
Select	*
	,	row_number() Over (Partition By ModuleName Order By DeleteFlag Asc)	RI
From mst_module
)
Update M Set
		ModuleName = M.ModuleName + '_Deleted'
From mst_module M
Inner Join md On md.ModuleId = m.ModuleID
Where RI > 1
And M.DeleteFlag = 1
Go

Set Nocount On;
Go
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
IF OBJECT_ID('tempdb..#SrNo') IS NOT NULL Drop Table #SrNo
Go
Create Table #SrNo(	
	tablename varchar(50),
	ColumnName varchar(10)
);
Go
Insert Into #SrNo
SELECT  t.name AS table_name,
--SCHEMA_NAME(schema_id) AS schema_name,
c.name AS column_name
FROM sys.tables AS t
INNER JOIN sys.columns c ON t.OBJECT_ID = c.OBJECT_ID
WHERE c.name LIKE 'SRNo' And system_type_id=TYPE_ID('int');
Go
Select * From #SrNo

Declare @Query varchar(250)
		, @command varchar(400)
		,@tablename varchar(50)
		,@update varchar(250);
While Exists(Select 1 From #SrNo) Begin
	Select top 1 @tablename= tableName from #SrNo
	If Exists(Select 1      from sys.all_columns c
      join sys.tables t on t.object_id = c.object_id
      join sys.schemas s on s.schema_id = t.schema_id
      join sys.default_constraints d on c.default_object_id = d.object_id
		where t.name = @tablename      And c.name = 'SrNo')  Begin
	select @Command = 'ALTER TABLE dbo.' + @tablename + ' drop constraint ' + d.name
	 from sys.tables t	  join    sys.default_constraints d	   on d.parent_object_id = t.object_id
	  join    sys.columns c	   on c.object_id = t.object_id		and c.column_id = d.parent_column_id
	 where t.name = @tablename
	  and t.schema_id = schema_id('dbo')
	  and c.name = 'SRNo'
	  exec (@command)
	  End
		Select @Query = 'Alter table ['+@tablename + '] Alter Column SrNo decimal(5,2) ';	
		Exec (@Query);
	Select @Query = 'Alter table ['+@tablename + '] ADD CONSTRAINT DF_'+replace(@tablename,' ','')+'_SRNo'+' DEFAULT 0.00 FOR SRNo; ';
	
	If Not Exists (Select 1      from sys.all_columns c
      join sys.tables t on t.object_id = c.object_id
      join sys.schemas s on s.schema_id = t.schema_id
      join sys.default_constraints d on c.default_object_id = d.object_id
		where t.name = @tablename      And c.name = 'SrNo')
      Begin
		--Print @Query		
		Exec(@Query)
	 End
	
	
	Delete From #SrNo where tablename = @tablename
	
End
GO
SET ANSI_PADDING OFF
GO