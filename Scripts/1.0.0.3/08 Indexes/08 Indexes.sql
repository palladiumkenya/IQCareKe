IF Not  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_LabOrderTestResult]') AND name = N'NCI_dtl_LabOrderTestResult_DeleteFlag_INC') Begin
CREATE NONCLUSTERED INDEX [NCI_dtl_LabOrderTestResult_DeleteFlag_INC] ON [dbo].[dtl_LabOrderTestResult]([DeleteFlag] ASC)
INCLUDE ( 	[LabOrderId],[ParameterId],	[ResultValue]) 
End
Go
If Exists(SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_LabOrderTestResult]') AND name = N'NCI_TestResult_OrderIdDeleteFlag_INC')
CREATE NONCLUSTERED INDEX [NCI_TestResult_OrderIdDeleteFlag_INC] ON [dbo].[dtl_LabOrderTestResult](	[LabOrderId] ASC,	[DeleteFlag] ASC) INCLUDE ( [ParameterId],	[ResultValue])
Go
IF Not  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Dtl_PurchaseItem]') AND name = N'NCI_Dtl_PurchaseItem_POIDItemId') Begin
CREATE NONCLUSTERED INDEX [NCI_Dtl_PurchaseItem_POIDItemId] ON [dbo].[Dtl_PurchaseItem]
(
	[POId] ASC,
	[ItemId] ASC
)

End
Go
IF Not  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Dtl_GRNote]') AND name = N'NCI_Dtl_GRNote_GrnId_ItemId_BatchId') Begin
CREATE NONCLUSTERED INDEX [NCI_Dtl_GRNote_GrnId_ItemId_BatchId] ON [dbo].[Dtl_GRNote]
(
	[GRNId] ASC,
	[BatchID] ASC,
	[ItemId] ASC
)
End
GO
IF Not  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ord_Visit]') AND name = N'NCI_Ord_Visit_VisitType_IX') Begin
CREATE NONCLUSTERED INDEX [NCI_Ord_Visit_VisitType_IX] ON [dbo].[ord_Visit] ([VisitType]) INCLUDE ([Visit_Id],[Ptn_Pk],[LocationID],[VisitDate])
End
Go
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ord_Visit]') AND name = N'NCI_Ord_Visit_VisitType_IXUser')
 DROP INDEX [NCI_Ord_Visit_VisitType_IXUser] ON [dbo].[ord_Visit]
GO

IF Not  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ord_Visit]') AND name = N'NCI_Ord_Visit_VisitType_IXUserId') Begin
CREATE NONCLUSTERED INDEX [NCI_Ord_Visit_VisitType_IXUserId] ON [dbo].[ord_Visit] ([VisitType]) INCLUDE ([Visit_Id],[Ptn_Pk],[VisitDate],UserId)
End
Go
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientClinicalStatus]') AND name = N'IX_PatientClinicalStatus_VisitPk_Inc')
 DROP INDEX [IX_PatientClinicalStatus_VisitPk_Inc] ON [dbo].[dtl_PatientClinicalStatus]
GO

CREATE  NONCLUSTERED INDEX [IX_PatientClinicalStatus_VisitPk_Inc] ON [dbo].[dtl_PatientClinicalStatus]([Visit_pk]) INCLUDE ([Pregnant])

Go

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[lnk_ItemCostConfiguration]') AND name = N'NCI_LNK_ItemCostConfig_PriceStatus_DeleteFlag')
 DROP INDEX [NCI_LNK_ItemCostConfig_PriceStatus_DeleteFlag] ON [dbo].[lnk_ItemCostConfiguration]
GO
CREATE NONCLUSTERED INDEX [NCI_LNK_ItemCostConfig_PriceStatus_DeleteFlag]
ON [dbo].[lnk_ItemCostConfiguration] ([PriceStatus],[DeleteFlag])
INCLUDE ([ItemId],[ItemType],[ItemSellingPrice],[EffectiveDate],[PharmacyPriceType])
GO


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientARTCare]') AND name = N'IX_PatientARTCare_VisitId_Inc')
 DROP INDEX [IX_PatientARTCare_VisitId_Inc] ON [dbo].[dtl_PatientARTCare]
GO

CREATE NONCLUSTERED INDEX [IX_PatientARTCare_VisitId_Inc]
ON [dbo].[dtl_PatientARTCare] ([visit_Id])
INCLUDE ([Pregnant])
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientStage]') AND name = N'IX_PatientStage_VisitPk_Inc')
 DROP INDEX [IX_PatientStage_VisitPk_Inc] ON [dbo].[dtl_PatientStage]
GO
CREATE NONCLUSTERED INDEX [IX_PatientStage_VisitPk_Inc]
ON [dbo].[dtl_PatientStage] ([Visit_Pk])
INCLUDE ([WHOStage])
Go

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientAppointment]') AND name = N'IX_PatientAppointment_PtnPk_OT')
 DROP INDEX [IX_PatientAppointment_PtnPk_OT] ON [dbo].[dtl_PatientAppointment]

GO
CREATE NONCLUSTERED INDEX [IX_PatientAppointment_PtnPk_OT]
ON [dbo].[dtl_PatientAppointment] ([Ptn_pk],[AppStatus],[DeleteFlag],[AppDate])

GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientArvTherapy]') AND name = N'IDX_dtl_PatientArvTherapy_NC1')
 DROP INDEX [IDX_dtl_PatientArvTherapy_NC1] ON [dbo].[dtl_PatientArvTherapy]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientStage]') AND name = N'IX_PatientStage_WhoStage_Inc')
 DROP INDEX [IX_PatientStage_WhoStage_Inc] ON [dbo].[dtl_PatientStage]
GO
CREATE NONCLUSTERED INDEX [IX_PatientStage_WhoStage_Inc]
ON [dbo].[dtl_PatientStage] ([WHOStage])
INCLUDE ([Visit_Pk])
Go

declare @sql NVARCHAR(1200)
SELECT @sql = 'ALTER TABLE dbo.dtl_PatientClinicalStatus DROP CONSTRAINT ' + name + ';'
    FROM sys.key_constraints    WHERE [type] = 'PK'    AND [parent_object_id] = OBJECT_ID('dbo.dtl_PatientClinicalStatus');

EXEC sp_executeSQL @sql;
Go
IF  Exists (SELECT * FROM sys.key_constraints WHERE type = 'PK' AND parent_object_id = OBJECT_ID('dbo.dtl_PatientClinicalStatus') AND Name = 'PK_dtl_PatientClinicalStatus')
   ALTER TABLE dbo.dtl_PatientClinicalStatus	DROP CONSTRAINT PK_dtl_PatientClinicalStatus
GO

IF Not Exists (SELECT * FROM sys.key_constraints WHERE type = 'PK' AND parent_object_id = OBJECT_ID('dbo.dtl_PatientClinicalStatus') AND Name = 'PK_dtl_PatientClinicalStatus')
   ALTER TABLE [dbo].[dtl_PatientClinicalStatus] ADD  CONSTRAINT [PK_dtl_PatientClinicalStatus] PRIMARY KEY CLUSTERED 
	(
	Ptn_pk,
	LocationID,
	Visit_pk
	)

GO

/****** Object:  Index [IX_mst_module_Name]    Script Date: 6/29/2016 11:16:46 AM ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[mst_module]') AND name = N'IX_mst_module_Name')
 DROP INDEX [IX_mst_module_Name] ON [dbo].[mst_module]
GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_mst_module_Name] ON [dbo].[mst_module]
(
	[ModuleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[lnk_GroupFeatures]') AND name = N'NCI_GrpFT_GrpID_FuncID')
DROP INDEX [NCI_GrpFT_GrpID_FuncID] ON [dbo].[lnk_GroupFeatures] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_GrpFT_GrpID_FuncID]
ON [dbo].[lnk_GroupFeatures] ([FeatureID])
INCLUDE ([GroupID],[FunctionID])
GO 
/*
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ord_PatientLabOrder]') AND name = N'NCI_LabOrd_ReportByDate')
DROP INDEX [NCI_LabOrd_ReportByDate] ON [dbo].[ord_PatientLabOrder] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_LabOrd_ReportByDate]
ON [dbo].[ord_PatientLabOrder] ([ReportedbyDate])
GO
*/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ord_PatientPharmacyOrder]') AND name = N'NCI_PharOrd_Dispensedate')
DROP INDEX [NCI_PharOrd_Dispensedate] ON [dbo].[ord_PatientPharmacyOrder] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_PharOrd_Dispensedate]
ON [dbo].[ord_PatientPharmacyOrder] ([DispensedByDate])
INCLUDE ([Ptn_pk],[UserID],[CreateDate])
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[mst_Bill]') AND name = N'NCI_MstBill_StatusDeleteFlag')
DROP INDEX [NCI_MstBill_StatusDeleteFlag] ON [dbo].[mst_Bill] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_MstBill_StatusDeleteFlag]
ON [dbo].[mst_Bill] ([BillStatus],[DeleteFlag])
INCLUDE ([BillID],[ptn_pk],[BillDate],[BillAmount])
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Mst_ItemMaster]') AND name = N'NCI_ItemMaster_ItemTypeID')
DROP INDEX [NCI_ItemMaster_ItemTypeID] ON [dbo].[Mst_ItemMaster] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_ItemMaster_ItemTypeID]
ON [dbo].[Mst_ItemMaster] ([ItemTypeID])
INCLUDE ([Item_PK],[ItemCode],[ItemName],[DispensingMargin],[DispensingUnitPrice],[FDACode],[Manufacturer],[MaxStock],[MinStock],[PurchaseUnitPrice],[QtyPerPurchaseUnit],[DispensingUnit],[PurchaseUnit],[DeleteFlag])
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_Bill]') AND name = N'NCI_dtl_Bill_ID_DeleteFlag')
DROP INDEX [NCI_dtl_Bill_ID_DeleteFlag] ON [dbo].[dtl_Bill] WITH ( ONLINE = OFF )
GO


/****** Object:  Index [NCI_Vitals_Hgt]   Script Date: 12/11/2014 16:22:43 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientVitals]') AND name = N'NCI_Vitals_Hgt')
DROP INDEX [NCI_Vitals_Hgt] ON [dbo].[dtl_PatientVitals] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_Vitals_Hgt]
ON [dbo].[dtl_PatientVitals] ([Visit_pk],[Height])
GO
/****** Object:  Index [IX_Mst_BillPaymentType]    Script Date: 09/11/2015 06:55:55 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Mst_BillPaymentType]') AND name = N'IX_Mst_BillPaymentType')
DROP INDEX [IX_Mst_BillPaymentType] ON [dbo].[Mst_BillPaymentType]
GO
/****** Object:  Index [IX_Mst_BillPaymentType]    Script Date: 09/11/2015 06:55:55 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Mst_BillPaymentType] ON [dbo].[Mst_BillPaymentType]
(
	[TypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

/****** Object:  Index [NCI_MSTPatient_DeleteFlag]    Script Date: 12/11/2014 16:22:43 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[mst_Patient]') AND name = N'NCI_MSTPatient_DeleteFlag')
DROP INDEX [NCI_MSTPatient_DeleteFlag] ON [dbo].[mst_Patient] WITH ( ONLINE = OFF )
GO

/****** Object:  Index [NCI_MSTPatient_DeleteFlag]    Script Date: 12/11/2014 16:22:43 ******/
CREATE NONCLUSTERED INDEX [NCI_MSTPatient_DeleteFlag] ON [dbo].[mst_Patient] 
(
	[DeleteFlag] ASC
)
INCLUDE ( [Ptn_Pk],
[FirstName],
[LastName],
[LocationID],
[PatientEnrollmentID],
[PatientClinicID],
[RegistrationDate],
[Sex],
[DOB],
[DobPrecision],
[Status],
[MiddleName],
[IQNumber],
[PatientFacilityID]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


/****** Object:  Index [NCI_DTLBill_BillIDDeleteFlag]    Script Date: 07/13/2015 18:24:51 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_Bill]') AND name = N'NCI_DTLBill_BillIDDeleteFlag')
DROP INDEX [NCI_DTLBill_BillIDDeleteFlag] ON [dbo].[dtl_Bill] WITH ( ONLINE = OFF )
GO

/****** Object:  Index [NCI_DTLBill_BillIDDeleteFlag]    Script Date: 07/13/2015 18:24:51 ******/
CREATE NONCLUSTERED INDEX [NCI_DTLBill_BillIDDeleteFlag] ON [dbo].[dtl_Bill] 
(
	[BillID] ASC,
	[DeleteFlag] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


/****** Object:  Index [NCI_OrdBill_BillID]    Script Date: 07/13/2015 18:25:00 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ord_bill]') AND name = N'NCI_OrdBill_BillID')
DROP INDEX [NCI_OrdBill_BillID] ON [dbo].[ord_bill] WITH ( ONLINE = OFF )
GO

/****** Object:  Index [NCI_OrdBill_BillID]    Script Date: 07/13/2015 18:25:00 ******/
CREATE NONCLUSTERED INDEX [NCI_OrdBill_BillID] ON [dbo].[ord_bill] 
(
	[BillID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientOtherTreatment]') AND name = N'NCI_POT_PtnPK_TbStatus_DeleteFlag')
DROP INDEX [NCI_POT_PtnPK_TbStatus_DeleteFlag] ON [dbo].[dtl_PatientOtherTreatment] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_POT_PtnPK_TbStatus_DeleteFlag]
ON [dbo].[dtl_PatientOtherTreatment] ([Ptn_pk],[TBStatus],[DeleteFlag])
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Mst_ItemMaster]') AND name = N'NCI_ItemMaster_DeleteFlag_Name')
DROP INDEX [NCI_ItemMaster_DeleteFlag_Name] ON [dbo].[Mst_ItemMaster] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_ItemMaster_DeleteFlag_Name]
ON [dbo].[Mst_ItemMaster] ([DeleteFlag],[ItemName])
INCLUDE ([Item_PK],[ItemTypeID])
GO


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_RegimenMap]') AND name = N'NCI_RM_OrderID_RegimenType')
DROP INDEX [NCI_RM_OrderID_RegimenType] ON [dbo].[dtl_RegimenMap] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_RM_OrderID_RegimenType]
ON [dbo].[dtl_RegimenMap] ([OrderID])
INCLUDE ([RegimenType])
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_Bill]') AND name = N'NCI_DTL_Bill_TransId')
DROP INDEX [NCI_DTL_Bill_TransId] ON [dbo].[dtl_Bill] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_DTL_Bill_TransId]
ON [dbo].[dtl_Bill] ([TransactionId])


IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_Bill]') AND name = N'NCI_Dtl_Bill_LocPaymentDeleteDate')
DROP INDEX [NCI_Dtl_Bill_LocPaymentDeleteDate] ON [dbo].[dtl_Bill] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_Dtl_Bill_LocPaymentDeleteDate]
ON [dbo].[dtl_Bill] ([LocationID],[PaymentStatus],[DeleteFlag],[BillItemDate])
INCLUDE ([ItemId],[ItemName],[ItemType],[Quantity],[SellingPrice],[Discount],[ModuleID])
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ord_Visit]') AND name = N'NCI_ORDVisit_PtnPK_VisitType_DeleteFlag')
DROP INDEX [NCI_ORDVisit_PtnPK_VisitType_DeleteFlag] ON [dbo].[ord_Visit] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_ORDVisit_PtnPK_VisitType_DeleteFlag]
ON [dbo].[ord_Visit] ([Ptn_Pk],[VisitType],[DeleteFlag])

GO
/*
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientLabResults]') AND name = N'NCI_PtnLabResults_ParamID')
DROP INDEX [NCI_PtnLabResults_ParamID] ON [dbo].[dtl_PatientLabResults] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_PtnLabResults_ParamID]
ON [dbo].[dtl_PatientLabResults] ([ParameterID])
INCLUDE ([LabID],[TestResults])
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientLabResults]') AND name = N'NCI_PtnLabResults_LabID_ParamID')
DROP INDEX [NCI_PtnLabResults_LabID_ParamID] ON [dbo].[dtl_PatientLabResults] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_PtnLabResults_LabID_ParamID]
ON [dbo].[dtl_PatientLabResults] ([LabID],[ParameterID])
INCLUDE ([TestResults])
Go*/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ord_Visit]') AND name = N'NCI_OrdVisit_LocVstType')
DROP INDEX [NCI_OrdVisit_LocVstType] ON [dbo].[ord_Visit] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_OrdVisit_LocVstType]
ON [dbo].[ord_Visit] ([LocationID],[VisitType])
INCLUDE ([Visit_Id],[Ptn_Pk],[VisitDate])
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientAppointment]') AND name = N'NCI_Appointment_LocStatus')
DROP INDEX [NCI_Appointment_LocStatus] ON [dbo].[dtl_PatientAppointment] WITH ( ONLINE = OFF )
GO
GO
CREATE NONCLUSTERED INDEX [NCI_Appointment_LocStatus]
ON [dbo].[dtl_PatientAppointment] ([LocationID],[AppStatus])
INCLUDE ([Ptn_pk],[Visit_pk],[AppDate],[DeleteFlag])
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientAppointment]') AND name = N'NCI_dtlAppointment_status')
DROP INDEX [NCI_dtlAppointment_status] ON [dbo].[dtl_PatientAppointment] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_dtlAppointment_status]
ON [dbo].[dtl_PatientAppointment] ([AppStatus])
INCLUDE ([AppDate],[DeleteFlag],[AppointmentId],[Ptn_pk],[LocationID])
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientAppointment]') AND name = N'NCI_Appointment_Ptn_Stat_Date_DelFlag')
DROP INDEX [NCI_Appointment_Ptn_Stat_Date_DelFlag] ON [dbo].[dtl_PatientAppointment] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_Appointment_Ptn_Stat_Date_DelFlag]
ON [dbo].[dtl_PatientAppointment] ([Ptn_pk],[AppStatus],[AppDate],[DeleteFlag])

GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[mst_Village]') AND name = N'NCI_Village_DelFlag_Sys')
DROP INDEX [NCI_Village_DelFlag_Sys] ON [dbo].[mst_Village] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_Village_DelFlag_Sys]
ON [dbo].[mst_Village] ([DeleteFlag],[SystemId])
INCLUDE ([ID],[Name])
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Dtl_StockTransaction]') AND name = N'NCI_StockTran_StoreId_ExpiryDate')
DROP INDEX [NCI_StockTran_StoreId_ExpiryDate] ON [dbo].[Dtl_StockTransaction] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_StockTran_StoreId_ExpiryDate]
ON [dbo].[Dtl_StockTransaction] ([StoreId],[ExpiryDate])
INCLUDE ([ItemId])
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Lnk_SupplierItem]') AND name = N'NCI_Lnk_Supplier_ItemId')
DROP INDEX [NCI_Lnk_Supplier_ItemId] ON [dbo].[Lnk_SupplierItem] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_Lnk_Supplier_ItemId]
ON [dbo].[Lnk_SupplierItem] ([ItemId])
INCLUDE ([SupplierId])
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[lnk_StoreItem]') AND name = N'NCI_LNK_StoreItem_StoreId_ItemId')
DROP INDEX [NCI_LNK_StoreItem_StoreId_ItemId] ON [dbo].[lnk_StoreItem] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_LNK_StoreItem_StoreId_ItemId]
ON [dbo].[lnk_StoreItem] ([StoreID],[ItemId])
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Dtl_StockTransaction]') AND name = N'NCI_DTL_StockTran_StoreId_INC')
DROP INDEX [NCI_DTL_StockTran_StoreId_INC] ON [dbo].[Dtl_StockTransaction] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_DTL_StockTran_StoreId_INC]
ON [dbo].[Dtl_StockTransaction] ([StoreId])
INCLUDE ([ItemId],[BatchId],[Quantity],[ExpiryDate])
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Dtl_StockTransaction]') AND name = N'NCI_DTL_StockTran_ItemId_INC')
DROP INDEX [NCI_DTL_StockTran_ItemId_INC] ON [dbo].[Dtl_StockTransaction] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_DTL_StockTran_ItemId_INC]
ON [dbo].[Dtl_StockTransaction] ([ItemId])
INCLUDE ([Quantity])
Go

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientAppointment]') AND name = N'NCI_DTL_PatientAppointment_DeleteFlag_INC')
DROP INDEX [NCI_DTL_PatientAppointment_DeleteFlag_INC] ON [dbo].[dtl_PatientAppointment] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_DTL_PatientAppointment_DeleteFlag_INC]
ON [dbo].[dtl_PatientAppointment] ([DeleteFlag])
INCLUDE ([Visit_pk],[AppDate],[AppReason])
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientAppointment]') AND name = N'NCI_DTL_PatientAppointment_VisitPK_INC')
DROP INDEX [NCI_DTL_PatientAppointment_VisitPK_INC] ON [dbo].[dtl_PatientAppointment] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_DTL_PatientAppointment_VisitPK_INC]
ON [dbo].[dtl_PatientAppointment] ([Visit_pk],[DeleteFlag])
INCLUDE ([AppDate],[AppReason])
Go
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_Bill]') AND name = N'NCI_DTL_Bill_PTNPK_createdby')
DROP INDEX [NCI_DTL_Bill_PTNPK_createdby] ON [dbo].[dtl_Bill] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_DTL_Bill_PTNPK_createdby]
ON [dbo].[dtl_Bill] ([Ptn_PK],[CreatedBy])

GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientItemsOrder]') AND name = N'NCI_PatientItemsOrder_PtnPk_LocationId')
DROP INDEX [NCI_PatientItemsOrder_PtnPk_LocationId] ON [dbo].[dtl_PatientItemsOrder] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_PatientItemsOrder_PtnPk_LocationId]
ON [dbo].[dtl_PatientItemsOrder] ([ptn_Pk],[LocationID],[UserID],[DeleteFlag])

GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Dtl_AdjustStock]') AND name = N'NCI_IX_Dtl_AdjustStock_DET')
DROP INDEX [NCI_IX_Dtl_AdjustStock_DET] ON [dbo].[Dtl_AdjustStock] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_IX_Dtl_AdjustStock_DET] ON [dbo].[Dtl_AdjustStock]
(
	[BatchId] ASC,
	[AdjustId] ASC,
	[ItemId] ASC,
	[StoreId] ASC,
	[ExpiryDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_Bill]') AND name = N'NCI_DTL_Bill_PyStatus_Delete_Ptn_INC')
DROP INDEX [NCI_DTL_Bill_PyStatus_Delete_Ptn_INC] ON [dbo].[dtl_Bill] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_DTL_Bill_PyStatus_Delete_Ptn_INC]
ON [dbo].[dtl_Bill] ([PaymentStatus],[DeleteFlag],[Ptn_PK])
INCLUDE ([BillID],[ItemId],[ItemName],[ItemType],[Quantity],[SellingPrice],[Discount],[ModuleID],[CostCenter])
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_PatientBlueCardPriorART]') AND name = N'NCI_dtl_PatientBlueCardPriorART')
DROP INDEX [NCI_dtl_PatientBlueCardPriorART] ON [dbo].[dtl_PatientBlueCardPriorART] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_dtl_PatientBlueCardPriorART] ON [dbo].[dtl_PatientBlueCardPriorART]
(
	[Ptn_pk] ASC,
	[LocationID] ASC,
	[Visit_pk] ASC,
	[PurposeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/*IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[ord_PatientLabOrder]') AND name = N'NCI_PatientLabOrder_Loc_Inc')
DROP INDEX [NCI_PatientLabOrder_Loc_Inc] ON [dbo].[ord_PatientLabOrder] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_PatientLabOrder_Loc_Inc]
ON [dbo].[ord_PatientLabOrder] ([LocationID])
INCLUDE ([Ptn_pk],[CreateDate])
GO*/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[mst_Patient]') AND name = N'NCI_MstPatient_Status_INC_PtnPk_Loc')
DROP INDEX [NCI_MstPatient_Status_INC_PtnPk_Loc] ON [dbo].[mst_Patient] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_MstPatient_Status_INC_PtnPk_Loc]
ON [dbo].[mst_Patient] ([Status])
INCLUDE ([Ptn_Pk],[LocationID])
GO

IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[lnk_StoreItem]') AND name = N'NCI_StoreItem_Id_INC')
DROP INDEX [NCI_StoreItem_Id_INC] ON [dbo].[lnk_StoreItem] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_StoreItem_Id_INC]
ON [dbo].[lnk_StoreItem] ([ItemId])
INCLUDE ([StoreID])
GO
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[mst_ModDeCode]') AND name = N'NCI_ModDecode_CodeId_DeleteFlag')
DROP INDEX [NCI_ModDecode_CodeId_DeleteFlag] ON [dbo].[mst_ModDeCode] WITH ( ONLINE = OFF )
GO
CREATE NONCLUSTERED INDEX [NCI_ModDecode_CodeId_DeleteFlag]
ON [dbo].[mst_ModDeCode] ([CodeID],[DeleteFlag])

GO
IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_LabOrderTestResult]') AND name = N'NCI_dtl_LabOrderTestResult_DeleteFlag_INC')
CREATE NONCLUSTERED INDEX [NCI_dtl_LabOrderTestResult_DeleteFlag_INC] ON [dbo].[dtl_LabOrderTestResult]
(
	[DeleteFlag] ASC
)
INCLUDE ( 	[LabOrderId],
	[ParameterId],
	[ResultValue]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF Not EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[dtl_LabOrderTestResult]') AND name = N'NCI_TestResult_OrderIdDeleteFlag_INC')
CREATE NONCLUSTERED INDEX [NCI_TestResult_OrderIdDeleteFlag_INC] ON [dbo].[dtl_LabOrderTestResult]
([LabOrderId],[DeleteFlag])
INCLUDE ( 	
	[ParameterId],
	[ResultValue]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO