/****** Object:  Trigger [TRG_VW_Drugs]    Script Date: 01/09/2015 11:04:49 ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[TRG_VW_Drugs]'))
DROP TRIGGER [dbo].[TRG_VW_Drugs]
GO

/****** Object:  Trigger [dbo].[TRG_VW_Drugs]    Script Date: 01/09/2015 11:04:49 ******/
/****** Object:  Trigger [dbo].[TRG_VW_Drugs]    Script Date: 04/01/2015 16:54:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Joseph
-- Create date: 10 June 2014
-- Description:	Trigger for inserting drugs into Item Master
-- =============================================
CREATE TRIGGER [dbo].[TRG_VW_Drugs]    ON  [dbo].[Mst_Drug] 
   Instead Of INSERT,UPDATE
AS 
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
    -- Insert statements for trigger here
    Declare  @ItemTypeID int;
    Select @ItemTypeID=ItemTypeID From Mst_ItemType Where ItemName='Pharmaceuticals'
  
    If Exists (Select 1	From Deleted)
    Begin
		Declare @NewPrice decimal(18,2),@ItemID int,@CurrentUserId int;
		
		
		Update M Set
			ItemName = I.DrugName,
			ItemTypeID = @ItemTypeID,
			DeleteFlag = I.DeleteFlag,
			M.ItemCode = I.DrugID,
			ItemInstructions = I.ItemInstructions,
			--CreatedBy = I.UserID,
			--CreateDate = I.CreateDate,
			UpdateDate = I.UpdateDate,
			UpdateBy = I.UserID,
			DispensingMargin = I.DispensingMargin,
			DispensingUnitPrice = I.DispensingUnitPrice,
			FDACode = I.FDACode,
			Manufacturer = I.Manufacturer,
			MaxStock = I.MaxStock,
			MinStock = I.MinStock,
			PurchaseUnitPrice = I.PurchaseUnitPrice,
			QtyPerPurchaseUnit = I.QtyPerPurchaseUnit,
			DispensingUnit = I.DispensingUnit,
			PurchaseUnit = I.PurchaseUnit
		From dbo.Mst_ItemMaster M
		Inner Join
			INSERTED I On I.Drug_pk = M.Item_PK;
		
    End
    Else
    Begin
		Insert Into dbo.Mst_ItemMaster (			
			ItemName,
			ItemCode,
			ItemInstructions,
			ItemTypeID,
			DeleteFlag,
			CreatedBy,
			CreateDate,
			UpdateDate,
			UpdateBy,
			DispensingMargin,
			DispensingUnitPrice,
			FDACode,
			Manufacturer,
			MaxStock,
			MinStock,
			PurchaseUnitPrice,
			QtyPerPurchaseUnit,
			DispensingUnit,
			PurchaseUnit)
			Select	
					I.DrugName,
					I.DrugID,					
					I.ItemInstructions,
					@ItemTypeID ItemTypeID,
					0 DeleteFlag,
					I.UserID,
					I.CreateDate,
					I.UpdateDate,
					Null UpdateBy,
					I.DispensingMargin,
					I.DispensingUnitPrice,
					I.FDACode,
					I.Manufacturer,
					I.MaxStock,
					I.MinStock,
					I.PurchaseUnitPrice,
					I.QtyPerPurchaseUnit,
					I.DispensingUnit,
					I.PurchaseUnit					
			  From INSERTED I;
			  
		If ((Select	Isnull(I.SellingUnitPrice, 0) From INSERTED I) > 0)Begin		
			Insert Into dbo.lnk_ItemCostConfiguration (
				ItemId,
				ItemSellingPrice,
				UserID,
				CreateDate,
				PriceStatus,
				ItemType,
				EffectiveDate)
			Select	I.Drug_pk ItemID,
					Isnull(I.SellingUnitPrice, 0),
					I.UserID,
					I.CreateDate,
					1 As PriceStatus,
					@ItemTypeID As ItemType,
					I.EffectiveDate
			From INSERTED I;
		End
    End

END


GO

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tr_autobillPharmacy]'))
DROP TRIGGER [dbo].[tr_autobillPharmacy]
GO

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tr_autobillPharmacy_update]'))
DROP TRIGGER [dbo].[tr_autobillPharmacy_update]
GO
/****** Object:  Trigger [dbo].[tr_autobillPharmacy_update]    Script Date: 6/21/2016 4:36:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 2016-06-13
-- Description:	Trigger for updating billing automatically based on drugs that have been ordered/given
-- =============================================
CREATE Trigger [dbo].[tr_autobillPharmacy_update] ON [dbo].[dtl_PatientPharmacyOrder]  After UPDATE 
AS 
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
	Set Nocount On;	 	

	declare @patientId int,
			@OrderedbyDate datetime,
			@DispensedbyDate datetime, 
			@itemType int,
			@ItemId int,
			@SellingPrice decimal(9,2),
			@UserId int,
			@PharmacyPK int,
			@DtlPK int,
			@billItemID int, 
			@OrderedQuantity int,
			@dispensedQuantity int, 
			@itemName varchar(250),
			@paymentStatus int,
			@LocationId int,
			@VisitId int,
			@ServiceStatus int,
			@PriceType int;
			
	Select Top 1 @itemType = BillingTypeID	From dbo.Mst_BillingType Where Name = 'Pharmaceuticals';
	declare @table Table(Id int, OrderId int, OrderDate datetime, DispensedDate datetime,LocationId int, VisitId int,PatientId int,OrderedQty int, DispensedQty int,
						 ItemId int, ItemName varchar(250), UserId int, Dispensed bit);
	Insert Into @table( Id
		, OrderId
		, OrderDate
		, DispensedDate
		, LocationId
		, VisitId
		, PatientId
		, OrderedQty
		, DispensedQty
		, ItemId
		, ItemName
		, UserId
		, Dispensed)
	Select	I.Id
		,	I.ptn_pharmacy_pk
		,	O.OrderedByDate
		,	O.DispensedByDate
		,	O.LocationID
		,	O.VisitID
		,	O.Ptn_pk
		,	I.OrderedQuantity
		,	I.DispensedQuantity
		,	I.Drug_Pk
		,	M.DrugName
		,	O.OrderedBy
		,	Case
				When I.DispensedQuantity Is Null Or I.DispensedQuantity = 0 Then 0
				Else 1
			End
	From Inserted I
	Inner Join ord_PatientPharmacyOrder O On O.ptn_pharmacy_pk = I.ptn_pharmacy_pk
	Inner Join Mst_Drug M On I.Drug_Pk = M.Drug_pk
	Inner Join (Select F.FacilityID From lnk_FacilityModule F Inner Join Mst_module M On M.ModuleId = F.ModuleID Where M.ModuleName = 'Billing') FM
	On FM.FacilityId = O.LocationId
	Where O.Ptn_pk > 0
		And(Select Paperless From mst_Facility F Where F.FacilityID = O.LocationId) = 1;

	While Exists (Select 1 From @table) Begin

		Select Top 1 @DtlPK = Id
			, @PharmacyPK =  OrderId
			, @OrderedbyDate = OrderDate
			, @DispensedbyDate = DispensedDate
			, @LocationId = LocationId
			, @VisitId = VisitId
			, @PatientId = PatientId
			, @OrderedQuantity = OrderedQty
			, @dispensedQuantity= DispensedQty
			, @ItemId = ItemId
			, @ItemName = ItemName
			, @UserId = UserId
			, @ServiceStatus = Dispensed
		From @table

		Select Top 1 @billItemID = db.billItemID,
					@paymentStatus = Isnull(db.PaymentStatus,0),
					@OrderedQuantity = Quantity
		From dtl_Bill db
		Where db.ItemId = @itemID	
		And db.ItemType = @itemType
		And db.DeleteFlag = 0
		And db.ptn_pk = @patientID
		And ItemSourceReferenceID = @PharmacyPK
		And db.ServiceStatus = 0;	

		If (@billItemID Is Not Null and  @billItemID > 0 ) Begin	
			
			Select @paymentStatus = Isnull(@PaymentStatus,0);			

			Exec @billItemID = pr_Billing_SaveBillItem						
					@PatientId		= @patientID,				
					@BillItemID		= @billItemID,
					@BillItemDate	= @OrderedbyDate,
					@PaymentStatus	= @paymentStatus,
					@LocationId		= @LocationID,
					@ItemId			= @itemID,
					@ItemName		= @itemName,
					@ItemType		= @itemType,
					@Quantity		= @OrderedQuantity,	
					@UserID			= @userID,
					@ServiceStatus	= @ServiceStatus,
					@ItemSourceReferenceID = @PharmacyPK; 
		End
		Delete From @table Where Id = @DtlPK;
	End

END

GO




IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tr_autobillPharmacy_insert]'))
DROP TRIGGER [dbo].[tr_autobillPharmacy_insert]
GO
 /****** Object:  Trigger [dbo].[tr_autobillPharmacy]    Script Date: 6/20/2016 11:07:15 AM ******/
/****** Object:  Trigger [dbo].[tr_autobillPharmacy_insert]    Script Date: 6/21/2016 4:34:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
  
-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 2016-06-13
-- Description:	Trigger for updating billing automatically based on drugs that have been ordered/given
-- =============================================
CREATE Trigger [dbo].[tr_autobillPharmacy_insert] ON [dbo].[dtl_PatientPharmacyOrder]  After INSERT 
AS 
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from interfering with SELECT statements.
	Set Nocount On;	 	

	declare @patientId int,
			@OrderedbyDate datetime,
			@DispensedbyDate datetime, 
			@itemType int,
			@ItemId int,
			@SellingPrice decimal(9,2),
			@UserId int,
			@PharmacyPK int,
			@DtlPK int,
			@billItemID int, 
			@OrderedQuantity int,
			@dispensedQuantity int, 
			@itemName varchar(250),
			@paymentStatus int,
			@LocationId int,
			@VisitId int,
			@ServiceStatus int,
			@PriceType int;
			
	Select Top 1 @itemType = BillingTypeID	From dbo.Mst_BillingType Where Name = 'Pharmaceuticals';
	declare @table Table(Id int, OrderId int, OrderDate datetime, DispensedDate datetime,LocationId int, VisitId int,PatientId int,OrderedQty int, DispensedQty int,
						 ItemId int, ItemName varchar(250), UserId int, Dispensed bit);
	Insert Into @table( Id
		, OrderId
		, OrderDate
		, DispensedDate
		, LocationId
		, VisitId
		, PatientId
		, OrderedQty
		, DispensedQty
		, ItemId
		, ItemName
		, UserId
		, Dispensed)
	Select	I.Id
		,	I.ptn_pharmacy_pk
		,	O.OrderedByDate
		,	O.DispensedByDate
		,	O.LocationID
		,	O.VisitID
		,	O.Ptn_pk
		,	I.OrderedQuantity
		,	I.DispensedQuantity
		,	I.Drug_Pk
		,	M.DrugName
		,	O.OrderedBy
		,	Case
				When I.DispensedQuantity Is Null Or I.DispensedQuantity = 0 Then 0
				Else 1
			End
	From Inserted I
	Inner Join ord_PatientPharmacyOrder O On O.ptn_pharmacy_pk = I.ptn_pharmacy_pk
	Inner Join Mst_Drug M On I.Drug_Pk = M.Drug_pk
	Inner Join (Select F.FacilityID From lnk_FacilityModule F Inner Join Mst_module M On M.ModuleId = F.ModuleID Where M.ModuleName = 'Billing') FM
	On FM.FacilityId = O.LocationId
	Where O.Ptn_pk > 0
		And(Select Paperless From mst_Facility F Where F.FacilityID = O.LocationId) = 1;

	While Exists (Select 1 From @table) Begin

		Select Top 1 @DtlPK = Id
			, @PharmacyPK =  OrderId
			, @OrderedbyDate = OrderDate
			, @DispensedbyDate = DispensedDate
			, @LocationId = LocationId
			, @VisitId = VisitId
			, @PatientId = PatientId
			, @OrderedQuantity = OrderedQty
			, @dispensedQuantity= DispensedQty
			, @ItemId = ItemId
			, @ItemName = ItemName
			, @UserId = UserId
			, @ServiceStatus = Dispensed
		From @table

		Select Top 1 @SellingPrice =ItemSellingPrice 
				, @PriceType = Isnull(PharmacyPriceType,0)
		From dbo.lnk_ItemCostConfiguration 
		Where ItemId=@ItemId  
		And ItemType = @itemType
		And (DATEADD(dd, 0, DATEDIFF(dd, 0, EffectiveDate)) <=@OrderedbyDate) 
		And DeleteFlag =0
		Order By EffectiveDate Desc, statusDate Desc; 

		If (@SellingPrice Is Not Null And @SellingPrice !=0) Begin	 		

			Select @OrderedQuantity= Case when @PriceType=1 Then 1 Else @OrderedQuantity End;	 

			Select @paymentStatus = 0;--Isnull(@PaymentStatus,0);

			Exec @billItemID = pr_Billing_SaveBillItem	
					@BillId			= 0,
					@PatientId		= @patientID,
					@ModuleId		= 0,
					@LocationId		= @LocationID,
					--@BillItemID		= @billItemID,
					@BillItemDate	= @OrderedbyDate,
					@PaymentStatus	= @paymentStatus,
					@ItemId			= @itemID,
					@ItemName		= @itemName,
					@ItemType		= @itemType,
					@Quantity		= @OrderedQuantity,
					@SellingPrice	= @sellingprice,
					@Discount		= 0,
					@UserID			= @userID,
					@ServiceStatus	= @ServiceStatus,
					@ItemSourceReferenceID = @PharmacyPK; 					
			
		End
		Delete From @table Where Id = @DtlPK;
	End

END

GO
 
/****** Object:  Trigger [dbo].[tr_autobillLab]    Script Date: 12/11/2014 15:27:23 ******/

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tr_autobillLab]'))
DROP TRIGGER [dbo].[tr_autobillLab]
GO
/****** Object:  Trigger [dbo].[tr_autobillLab]    Script Date: 6/21/2016 3:28:06 PM ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tr_autobillLab_update]'))
DROP TRIGGER [dbo].[tr_autobillLab_update]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 2016-02-23
-- Description:	Trigger for updating billing automatically based on labs that have been ordered/done
-- =============================================
CREATE Trigger [dbo].[tr_autobillLab_update] ON [dbo].[dtl_LabOrderTest] FOR UPDATE 
AS 	BEGIN
	Set Nocount on;
	declare @PatientId int,@BillItemDate datetime,@itemType int, @itemId int,@SellingPrice decimal(18,2),@UserId int,@orderId int,@BillItemId int,
			@PaymentStatus int,@ItemName varchar(250), @LocationId int, @VisitId int,@ServiceStatus int, @ParentTestId int,@OrderTestId int, @ResultStatus varchar(50);

	Select Top 1 @itemType = BillingTypeID	From Mst_BillingType	Where Name = 'Lab Tests';
	declare @table Table(OrderTestId int, OrderId int, OrderDate datetime,LocationId int, VisitId int,PatientId int,
							ItemId int, ItemName varchar(250), UserId int, ParentTestId int, ResultStatus varchar(50));
		Insert Into @table (
			OrderTestId,
			OrderId,
			OrderDate,
			LocationId,
			VisitId,
			PatientId,
			ItemId,
			ItemName,
			UserId,
			ParentTestId,
			ResultStatus)
			Select	I.Id OrderTestId,
					O.Id OrderId,
					O.OrderDate,
					O.LocationId,
					O.VisitId,
					O.Ptn_Pk,
					I.LabTestId,
					TM.Name,
					I.UserId,
					I.ParentTestId,
					I.ResultStatus
			From Inserted I
			Inner Join mst_LabTestMaster TM	On TM.Id = I.LabTestId
			Inner Join ord_LabOrder O	On O.Id = I.LabOrderId
			Inner Join (Select F.FacilityID From lnk_FacilityModule F Inner Join Mst_module M On M.ModuleId = F.ModuleID Where M.ModuleName = 'Billing') FM
			On FM.FacilityId = O.LocationId
			Where I.ParentTestId Is Null And O.Ptn_Pk > 0
			And(Select Paperless From mst_Facility F Where F.FacilityID = O.LocationId) = 1;

		While Exists(Select 1 From @table) Begin
			Select Top 1 @orderId = OrderId
					, @itemId = ItemId
					, @ItemName = ItemName 
					, @UserId = UserId
					, @ResultStatus = ResultStatus
					, @PatientId = PatientId
					, @BillItemDate = OrderDate
					, @LocationId = LocationId
					, @VisitId = VisitId
					, @OrderTestId = OrderTestId
			From @table
			

		Select Top 1	@BillItemId = db.billItemID,
						@PaymentStatus = db.PaymentStatus
		From dtl_Bill db
		Where db.ItemId = @itemId
		And db.ItemType = @itemType
		And db.DeleteFlag = 0
		And ItemSourceReferenceID = @VisitId
		And db.ptn_pk = @PatientId
		And db.ServiceStatus = 0;

		If (@BillItemId Is Not Null) Begin			

			Select @paymentStatus = Isnull(@PaymentStatus,0);
			Select @ServiceStatus = Case When (@ResultStatus = 'Pending') Then 0 Else 1 End;
			Exec pr_Billing_SaveBillItem					
					@PatientId = @PatientId,					
					@LocationId = @LocationId,
					@BillItemId= @BillItemId,
					@BillItemDate = @billitemDate,				
					@PaymentStatus = @paymentStatus,
					@ItemId = @itemID,
					@ItemName = @itemName,
					@ItemType = @itemType,
					@Quantity = 1,	
					@UserId = @UserId,
					@serviceStatus = @ServiceStatus ,
					@ItemSourceReferenceID = @VisitId;
		End
		
		Delete From @table Where OrderTestId = @OrderTestId;

	End
End

Go

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tr_autobillLab_insert]'))
DROP TRIGGER [dbo].[tr_autobillLab_insert]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 2016-02-23
-- Description:	Trigger for updating billing automatically based on labs that have been ordered/done
-- =============================================
CREATE Trigger [dbo].[tr_autobillLab_insert] ON [dbo].[dtl_LabOrderTest] FOR INSERT
AS 	BEGIN
	Set Nocount on;
	declare @PatientId int,@BillItemDate datetime,@itemType int, @itemId int,@SellingPrice decimal(18,2),@UserId int,@orderId int,@BillItemId int,
			@PaymentStatus int,@ItemName varchar(250), @LocationId int, @VisitId int,@ServiceStatus int, @ParentTestId int,@OrderTestId int, @ResultStatus varchar(50);

	Select Top 1 @itemType = BillingTypeID	From Mst_BillingType	Where Name = 'Lab Tests';
	declare @table Table(OrderTestId int, OrderId int, OrderDate datetime,LocationId int, VisitId int,PatientId int,
							ItemId int, ItemName varchar(250), UserId int, ParentTestId int, ResultStatus varchar(50));
		Insert Into @table (
			OrderTestId,
			OrderId,
			OrderDate,
			LocationId,
			VisitId,
			PatientId,
			ItemId,
			ItemName,
			UserId,
			ParentTestId,
			ResultStatus)
			Select	I.Id OrderTestId,
					O.Id OrderId,
					O.OrderDate,
					O.LocationId,
					O.VisitId,
					O.Ptn_Pk,
					I.LabTestId,
					TM.Name,
					I.UserId,
					I.ParentTestId,
					I.ResultStatus
			From Inserted I
			Inner Join mst_LabTestMaster TM
				On TM.Id = I.LabTestId
			Inner Join ord_LabOrder O
				On O.Id = I.LabOrderId
			Inner Join (Select F.FacilityID From lnk_FacilityModule F Inner Join Mst_module M On M.ModuleId = F.ModuleID Where M.ModuleName = 'Billing') FM
			On FM.FacilityId = O.LocationId
			Where I.ParentTestId Is Null And O.Ptn_Pk > 0
			And(Select Paperless From mst_Facility F Where F.FacilityID = O.LocationId) = 1;

		While Exists(Select 1 From @table) Begin
			Select Top 1 @orderId = OrderId
					, @itemId = ItemId
					, @ItemName = ItemName 
					, @UserId = UserId
					, @ResultStatus = ResultStatus
					, @PatientId = PatientId
					, @BillItemDate = OrderDate
					, @LocationId = LocationId
					, @VisitId = VisitId
					, @OrderTestId = OrderTestId
			From @table
		Select Top 1 
			@SellingPrice =ItemSellingPrice 
		From dbo.lnk_ItemCostConfiguration 
		Where ItemId=@itemId  
		And ItemType = @itemType
		And (DATEADD(dd, 0, DATEDIFF(dd, 0, EffectiveDate)) <=@BillItemDate) 
		And DeleteFlag =0
		Order By EffectiveDate Desc, statusDate Desc; 

		If (@SellingPrice Is Not Null And @SellingPrice !=0) Begin			

			Select @paymentStatus = 0;-- Isnull(@PaymentStatus,0);

			Select @ServiceStatus = Case When (@ResultStatus = 'Pending') Then 0 Else 1 End;

			Exec pr_Billing_SaveBillItem	
					@BillID = 0,
					@PatientId = @PatientId,
					@ModuleID = 0,
					@LocationId = @LocationId,
					@BillItemDate = @billitemDate,					
					@PaymentStatus = @paymentStatus,
					@ItemId = @itemID,
					@ItemName = @itemName,
					@ItemType = @itemType,
					@Quantity = 1,
					@SellingPrice = @SellingPrice,
					@Discount = 0 ,
					@UserId = @UserId,
					@serviceStatus = @ServiceStatus ,
					@ItemSourceReferenceID = @VisitId;
		End
		
		Delete From @table Where OrderTestId = @OrderTestId;

	End
	End
		
Go


/****** Object:  Trigger [dbo].[tr_autobillVisit]    Script Date: 12/11/2014 16:03:58 ******/

IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tr_autobillVisit]'))
DROP TRIGGER [dbo].[tr_autobillVisit]

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Vincent Yahuma
-- Create date: 2014-04-09
-- Description:	Trigger for updating billing automatically based on form filled
-- =============================================
Create Trigger [dbo].[tr_autobillVisit] ON [dbo].[ord_Visit] FOR INSERT 
AS 
BEGIN
-- Updated by VY 2014-06-26 to include moduleid,itemname and payment status
	Set Nocount On;
	
	
	declare @patientID int,
			@billitemDate DateTime, 
			@itemType int,
			@itemID int,
			@sellingprice decimal(18,2),
			@userID int,
			@billItemID int,
			@moduleID int,
			@paymentStatus int,
			@itemName varchar(250),
			@LocationID int,
			@VisitID int;

		Select Top 1 @itemType  = BillingTypeID	From Mst_BillingType	Where Name = 'VisitType'

		Select	@patientID = Ptn_Pk,
				@billitemDate = VisitDate,
				@itemID = VisitType,
				@userID = inserted.UserID,
				@itemName = vt.VisitName,
				@LocationID = INSERTED.LocationID,
				@VisitID = Visit_Id
		From inserted
		Inner Join
			mst_VisitType vt On vt.VisitTypeID = inserted.VisitType And vt.DeleteFlag = 0 Or vt.DeleteFlag Is Null;

		If Not Exists(Select 1 From lnk_FacilityModule F Inner Join Mst_module M On M.ModuleId = F.ModuleID
		Where M.ModuleName='Billing' And F.FacilityID = @LocationId) Begin
			Return;
		End
		If Not Exists(Select 1 From mst_Facility F Where F.FacilityID = @LocationId And F.PaperLess = 1) Begin
			Return;
		End
		If(@patientID > 0) Begin
		Select Top 1 @moduleID = ins.ModuleId
		From inserted ins
		Inner Join
			mst_VisitType vt On vt.VisitTypeID = ins.VisitType;		
		
		Select Top 1 
			@SellingPrice =ItemSellingPrice 
		From dbo.lnk_ItemCostConfiguration 
		Where ItemId=@ItemID  
		And ItemType = @ItemType
		And (DATEADD(dd, 0, DATEDIFF(dd, 0, EffectiveDate)) <=@billItemDate) 
		And DeleteFlag =0
		Order By EffectiveDate Desc, statusDate Desc; 
  
	  If (@sellingprice Is Not Null And @sellingprice > 0.0)  Begin
			Select Top 1	@billItemID = db.billItemID,
					@paymentStatus = db.PaymentStatus
			From dtl_Bill db
			Where db.ItemId = @itemID
			And db.ItemType = @itemType
			And db.DeleteFlag = 0
			And db.ptn_pk = @patientID
			And db.ServiceStatus = 0;
			 If @paymentStatus Is Null
					Set @paymentStatus = 0;

			Exec pr_Billing_SaveBillItem
				@BillID = 0,
				@PatientID = @patientID,
				@ModuleID = @moduleID,
				@LocationID = @LocationID,
				@BillItemID = @billItemID,
				@BillItemDate = @billitemDate,
				--@PaymentType = Null,
				@PaymentStatus = @paymentStatus,
				@ItemID = @itemID,
				@ItemName = @itemName,
				@ItemType = @itemType,
				@Quantity = 1,
				@SellingPrice = @sellingprice,
				@Discount = 0,
				@UserID = @userID,
				@ServiceStatus = 1,
				@ItemSourceReferenceID = @VisitID;
		End   
	End

END





GO


/****** Object:  Trigger [GeneratePatientFacilityID]    Script Date: 01/09/2015 10:14:58 ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[GeneratePatientFacilityID]'))
DROP TRIGGER [dbo].[GeneratePatientFacilityID]
GO

/****** Object:  Trigger [dbo].[GeneratePatientFacilityID]    Script Date: 01/09/2015 10:14:58 ******/
/****** Object:  Trigger [GeneratePatientFacilityID]    Script Date: 04/28/2015 13:12:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 2014 Oct 15
-- Description:	Update patient facility ID on insert
-- =============================================
-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 2014 Oct 15
-- Description:	Update patient facility ID on insert
-- =============================================
CREATE TRIGGER [dbo].[GeneratePatientFacilityID]    ON  [dbo].[mst_Patient]   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
    Declare @RegYear int, @NewPtn_Pk int, @MaxFacilityID varchar(10);

	Declare @table table(RegYear int, PatientId int);
	Insert Into @table(RegYear, PatientId)
	Select  datepart(Year,I.RegistrationDate), I.Ptn_Pk From Inserted I  ;
	While Exists(Select 1 From @table) Begin
	   Select Top 1 @RegYear = t.RegYear, @NewPtn_Pk=t.PatientId From @table t  ;
	   Select @MaxFacilityID= max(Convert(varchar,Replace(PatientFacilityID,'-',''))) From 
		mst_Patient Where  PatientFacilityID Like Convert(varchar,@RegYear)+'-%'
		If(@MaxFacilityID Is Null)
			Select @MaxFacilityID = Convert(varchar(4), @RegYear)  + Replicate('0', 5) + Convert(varchar, 1);
		Else
			Select @MaxFacilityID = Convert(int,@MaxFacilityID)+1;
	
		Select @MaxFacilityID = Stuff(@MaxFacilityID,5,0,'-')	;
	
		Update mst_Patient Set PatientFacilityID = @MaxFacilityID Where Ptn_Pk =@NewPtn_Pk And PatientFacilityID Is Null;

		Delete From @table Where  PatientId = @NewPtn_Pk;

	End
    

    
END

Go
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tr_AutoBillAdmission]'))
DROP TRIGGER [dbo].[tr_AutoBillAdmission]
GO


/****** Object:  Trigger [dbo].[tr_AutoBillAdmission]    Script Date: 5/12/2016 5:06:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 11 Feb 2015
-- Description:	Trigger for updating billing automatically based on date of discharge from a ward
-- =============================================
CREATE TRIGGER [dbo].[tr_AutoBillAdmission] 
   ON  [dbo].[dtl_PatientWardAdmission] 
   AFTER UPDATE
AS 
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
	Set Nocount On;

	Declare @patientID int,
		@billitemDate datetime,
		@PriceDate datetime,
		@itemType int,
		@itemID int,
		@sellingprice decimal(18, 2),
		@userID int,
		@billItemID int,
		@moduleID int,
		@paymentStatus int,
		@itemName varchar(250),
		@CostCenter varchar(50),
		@LocationID int,
		@AdmissionID int,
		@Days int;

	Select Top 1 @itemType = BillingTypeID
	From Mst_BillingType
	Where Name = 'Ward Admission';

	
	If (Update(ActualDischargeDate)) 
	Begin
		Select	@patientID = PA.Ptn_PK,
				@billitemDate = I.ActualDischargeDate,
				@PriceDate = I.AdmissionDate,
				@itemID = PA.WardID,
				@userID = I.DischargedBy,
				@itemName = W.WardName +' Bed Charges('  + convert(varchar(11),I.AdmissionDate,106) + '-'+convert(varchar(11),I.ActualDischargeDate,106)+')',
				@CostCenter = W.WardName,			
				@LocationID = W.LocationID,
				@AdmissionID = PA.AdmissionID,
				@Days = Datediff(Day, PA.AdmissionDate, I.ActualDischargeDate)
		From inserted I
		Inner Join
			dtl_PatientWardAdmission PA On PA.AdmissionID = I.AdmissionID
		Inner Join
			Mst_PatientWard W On W.WardID = PA.WardID
		Inner Join (Select F.FacilityID From lnk_FacilityModule F Inner Join Mst_module M On M.ModuleId = F.ModuleID Where M.ModuleName = 'Billing') FM
				On FM.FacilityId = W.LocationId;
		If(@patientID > 0) Begin
			Select Top 1 @SellingPrice = ItemSellingPrice
			From dbo.lnk_ItemCostConfiguration
			Where ItemId = @ItemID
			And ItemType = @ItemType
			And (Dateadd(dd, 0, Datediff(dd, 0, EffectiveDate)) <= @PriceDate)
			And DeleteFlag = 0
			Order By EffectiveDate Desc, statusDate Desc;
			
			If (@sellingprice Is Not Null) Begin
				Exec pr_Billing_SaveBillItem	
						@BillID = 0,
						@PatientID = @patientID,
						@ModuleID = @moduleID,
						@LocationID = @LocationID,
						@BillItemID = @billItemID,
						@BillItemDate = @billitemDate,
						
						@CostCenter = @CostCenter,
						@PaymentStatus = 0,
						@ItemID = @itemID,
						@ItemName = @itemName,
						@ItemType = @itemType,
						@Quantity = @Days,
						@SellingPrice = @sellingprice,
						@Discount = 0,
						@UserID = @userID,
						@ServiceStatus = 1,
						@ItemSourceReferenceID = @AdmissionID;
			End
		End
	End

End


GO


/****** Object:  Trigger [GenerateAdmissionNumber]    Script Date: 02/11/2015 15:38:33 ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[GenerateAdmissionNumber]'))
DROP TRIGGER [dbo].[GenerateAdmissionNumber]
GO


/****** Object:  Trigger [TR_SaveFormCreatedBy]    Script Date: 04/01/2015 16:52:35 ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[TR_SaveFormCreatedBy]'))
DROP TRIGGER [dbo].[TR_SaveFormCreatedBy]
GO


GO

/****** Object:  Trigger [dbo].[TR_SaveFormCreatedBy]    Script Date: 04/01/2015 16:52:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Joseph Njung'e
-- Create date: Mar 30 2015
-- Description:	Save the user id for the creator in the createdby column
-- =============================================
Create TRIGGER [dbo].[TR_SaveFormCreatedBy] 
   ON  [dbo].[ord_Visit] 
   After INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    
		Update V Set
			V.CreatedBy = I.UserID,
			DeleteFlag = Isnull(I.DeleteFlag,0)
		From dbo.ord_Visit V 
		Inner Join INSERTED I On I.Visit_Id = V.Visit_Id;

END

GO
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tr_autoBillWaitingList]'))
DROP TRIGGER [dbo].[tr_autoBillWaitingList]
GO

/****** Object:  Trigger [dbo].[tr_autobillVisit]    Script Date: 4/24/2015 12:45:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
   
-- =============================================
-- Author:		Vincent Yahuma
-- Create date: 2015-04-24
-- Description:	Trigger for updating billing automatically based on form filled
-- =============================================
CREATE Trigger [dbo].[tr_autoBillWaitingList] ON [dbo].[dtl_WaitingList] FOR INSERT 
AS 
BEGIN

Set Nocount On;
Return;
/*

	declare @patientID int,
			@billitemDate DateTime, 
			@itemType int,
			@itemID int,
			@sellingprice decimal(18,2),
			@userID int,
			@billItemID int,
			@moduleID int,
			@itemName varchar(250),
			@VisitID int,
			@regDate datetime,
			@LocationID int;


		Select Top 1 @itemType  = BillingTypeID	From Mst_BillingType	Where Name = 'VisitType'

		Select	@patientID = Ptn_Pk,
				@billitemDate = getDate(),
				--@itemID = VisitType,
				@userID = inserted.CreatedBy,
				@itemName = 'Revisit',
				@moduleID=inserted.ModuleID
				--@LocationID = INSERTED.LocationID,
				--@VisitID = Visit_Id
				
		From inserted
		
 --check whether revisist or enrollment
 select @regDate=min(CreateDate) from Lnk_PatientProgramStart where Ptn_pk=@patientID and ModuleId=@moduleID
 -- select @Registered=1 from Lnk_PatientProgramStart where Ptn_pk=@patientID and ModuleId=@moduleID

 select @LocationID=locationID from mst_Patient where Ptn_Pk=@patientID

IF(dateadd(second, 0, dateadd(day, datediff(day, 0, @regDate), 0)) !=
 dateadd(second, 0, dateadd(day, datediff(day, 0, getDate()), 0)))
 --IF(@Registered=1)
BEGIN		


--GET revisit ID
 select @itemID=VisitTypeID from mst_VisitType where VisitName='revisit'

		--Select @sellingprice = P.SellingPrice From dbo.fn_Billing_GetItemPriceOnDate(@itemID,@billItemDate,@itemType) P;
		Select Top 1 
			@SellingPrice =ItemSellingPrice 
		From dbo.lnk_ItemCostConfiguration 
		Where ItemId=@ItemID  
		And ItemType = @ItemType
		And (DATEADD(dd, 0, DATEDIFF(dd, 0, EffectiveDate)) <=@billItemDate) 
		And DeleteFlag =0
		Order By EffectiveDate Desc, statusDate Desc; 
  
	  If (@sellingprice Is Not Null And @sellingprice > 0.0)
  	 -- If (@sellingprice Is Not Null)
	  Begin
				 

			Exec pr_Billing_SaveBillItem
				@BillID = 0,
				@PatientID = @patientID,
				@ModuleID = @moduleID,
				@LocationID = @LocationID,
				@BillItemID = @billItemID,
				@BillItemDate = @billitemDate,
				--@PaymentType = Null,
				@PaymentStatus = 0,
				@ItemID = @itemID,
				@ItemName = @itemName,
				@ItemType = @itemType,
				@Quantity = 1,
				@SellingPrice = @sellingprice,
				@Discount = 0,
				@UserID = @userID,
				@ServiceStatus = 1,
				@ItemSourceReferenceID = @VisitID;
		End
	   
   END
   */
END
GO

/****** Object:  Trigger [TRV_PatientCareEnd]    Script Date: 11/30/2015 18:38:22 ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[TRV_PatientCareEnd]'))
DROP TRIGGER [dbo].[TRV_PatientCareEnd]
GO

/****** Object:  Trigger [dbo].[TRV_PatientCareEnd]    Script Date: 11/30/2015 18:38:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF  EXISTS (Select * From sys.triggers Where object_id = object_id(N'[dbo].[tr_autoBillClinicalService]'))
	 Drop Trigger [dbo].[tr_autoBillClinicalService]
Go
/****** Object:  Trigger [tr_autoBillClinicalService_insert]    Script Date: 6/21/2016 4:47:55 PM ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tr_autoBillClinicalService_insert]'))
DROP TRIGGER [dbo].[tr_autoBillClinicalService_insert]
GO

/****** Object:  Trigger [dbo].[tr_autoBillClinicalService_insert]    Script Date: 6/21/2016 4:47:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 2016-02-23
-- Description:	Trigger for updating billing automatically based on labs that have been ordered/done
-- =============================================
CREATE Trigger [dbo].[tr_autoBillClinicalService_insert] ON [dbo].[dtl_ClinicalServiceOrder] FOR INSERT
AS 
BEGIN
Set Nocount On;

Declare @PatientId int, @BillItemDate datetime, @itemType int, @itemId int, @SellingPrice decimal(18, 2), @UserId int, @orderId int, @BillItemId int,
@PaymentStatus int, @ItemName varchar(250), @LocationId int, @VisitId int, @ServiceStatus int, @TargetModuleId int,
@Quantity int, @ServiceOrderId int;

Select Top 1 @itemType = BillingTypeID
From Mst_BillingType
Where Name = 'Clinical Services';

Declare @table Table(Id int, OrderId int, OrderDate datetime, LocationId int, VisitId int, PatientId int, ItemId int, ItemName varchar(250), UserId int, ModuleId int, Quantity int, ServiceStatus bit);

Insert Into @table (
		Id
	,	OrderId
	,	OrderDate
	,	LocationId
	,	VisitId
	,	PatientId
	,	ItemId
	,	ItemName
	,	UserId
	,	ModuleId
	,	Quantity
	,	ServiceStatus
)
	Select	I.Id
		,	I.OrderId
		,	O.OrderDate
		,	O.LocationId
		,	O.VisitId
		,	O.Ptn_Pk
		,	I.ClinicalServiceId
		,	M.Name
		,	O.OrderedBy
		,	o.TargetModuleId
		,	I.Quantity
		,	Case
				When (I.ResultDate Is Null) Then 0
				Else 1
			End
	From Inserted I
	Inner Join Mst_ClinicalService M On M.Id = I.ClinicalServiceId
	Inner Join ord_ClinicalServiceOrder O On I.OrderId = O.Id 
	Inner Join (Select F.FacilityID From lnk_FacilityModule F Inner Join Mst_module M On M.ModuleId = F.ModuleID Where M.ModuleName = 'Billing') FM
	On FM.FacilityId = O.LocationId
	Where O.Ptn_Pk > 0
	And(Select Paperless From mst_Facility F Where F.FacilityID = O.LocationId) = 1;

	While Exists(Select 1 From @table) Begin
			Select Top 1
				 @orderId = OrderId
				, @ServiceOrderId  = Id
				, @PatientId = PatientId
				, @BillItemDate = OrderDate
				, @LocationId = LocationId
				, @VisitId = VisitId
				, @UserId = UserId
				, @TargetModuleId = ModuleId
				, @itemId = ItemId
				, @ItemName = ItemName
				, @Quantity = Quantity
				, @ServiceStatus = ServiceStatus
			From @table;

			Select Top 1 @SellingPrice = ItemSellingPrice
			From dbo.lnk_ItemCostConfiguration
			Where ItemId = @itemId
				And ItemType = @itemType
				And (dateadd(dd, 0, datediff(dd, 0, EffectiveDate)) <= @BillItemDate)
				And DeleteFlag = 0
			Order By EffectiveDate Desc, statusDate Desc;

			If (@SellingPrice Is Not Null And @SellingPrice != 0) Begin			

				Select @paymentStatus = 0;

				Exec pr_Billing_SaveBillItem	@BillID = 0
					,	@PatientId = @PatientId
					,	@ModuleID = @TargetModuleId
					,	@LocationId = @LocationId
					--,	@BillItemId = @BillItemId
					,	@BillItemDate = @billitemDate
					,   @PaymentStatus = @paymentStatus
					,	@ItemId = @itemId
					,	@ItemName = @itemName
					,	@ItemType = @itemType
					,	@Quantity = @Quantity
					,	@SellingPrice = @SellingPrice
					,	@Discount = 0
					,	@UserId = @UserId
					,	@serviceStatus = @ServiceStatus
					,	@ItemSourceReferenceID = @VisitId;
			End
			Delete From @table Where Id = @ServiceOrderId;
	End	 	

End
	  
	  
GO
 
/****** Object:  Trigger [tr_autoBillClinicalService_update]    Script Date: 6/21/2016 4:48:11 PM ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tr_autoBillClinicalService_update]'))
	DROP TRIGGER [dbo].[tr_autoBillClinicalService_update]
GO

/****** Object:  Trigger [dbo].[tr_autoBillClinicalService_update]    Script Date: 6/21/2016 4:48:11 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE Trigger [dbo].[tr_autoBillClinicalService_update] ON [dbo].[dtl_ClinicalServiceOrder] FOR Update
AS 
BEGIN
Set Nocount On;


Declare @PatientId int, @BillItemDate datetime, @itemType int, @itemId int, @SellingPrice decimal(18, 2), @UserId int, @orderId int, @BillItemId int,
@PaymentStatus int, @ItemName varchar(250), @LocationId int, @VisitId int, @ServiceStatus int, @TargetModuleId int,
@Quantity int, @ServiceOrderId int;

Select Top 1 @itemType = BillingTypeID
From Mst_BillingType
Where Name = 'Clinical Services';

Declare @table Table(Id int, OrderId int, OrderDate datetime, LocationId int, VisitId int, PatientId int, ItemId int, ItemName varchar(250), UserId int, ModuleId int, Quantity int, ServiceStatus bit);

Insert Into @table (
		Id
	,	OrderId
	,	OrderDate
	,	LocationId
	,	VisitId
	,	PatientId
	,	ItemId
	,	ItemName
	,	UserId
	,	ModuleId
	,	Quantity
	,	ServiceStatus
)
	Select	I.Id
		,	I.OrderId
		,	O.OrderDate
		,	O.LocationId
		,	O.VisitId
		,	O.Ptn_Pk
		,	I.ClinicalServiceId
		,	M.Name
		,	O.OrderedBy
		,	o.TargetModuleId
		,	I.Quantity
		,	Case
				When (I.ResultDate Is Null) Then 0
				Else 1
			End
	From Inserted I
	Inner Join Mst_ClinicalService M On M.Id = I.ClinicalServiceId
	Inner Join ord_ClinicalServiceOrder O On I.OrderId = O.Id 
	Inner Join (Select F.FacilityID From lnk_FacilityModule F Inner Join Mst_module M On M.ModuleId = F.ModuleID Where M.ModuleName = 'Billing') FM
	On FM.FacilityId = O.LocationId
	Where O.Ptn_Pk > 0
	And(Select Paperless From mst_Facility F Where F.FacilityID = O.LocationId) = 1;

	While Exists(Select 1 From @table) Begin
			Select Top 1
				 @orderId = OrderId
				, @ServiceOrderId  = Id
				, @PatientId = PatientId
				, @BillItemDate = OrderDate
				, @LocationId = LocationId
				, @VisitId = VisitId
				, @UserId = UserId
				, @TargetModuleId = ModuleId
				, @itemId = ItemId
				, @ItemName = ItemName
				, @Quantity = Quantity
				, @ServiceStatus = ServiceStatus
			From @table;

			Select Top 1	@BillItemId = db.billItemID
						,	@PaymentStatus = db.PaymentStatus
						,  @Quantity = Quantity
				From dtl_Bill db
				Where db.ItemId = @itemId
					And db.ItemType = @itemType
					And db.DeleteFlag = 0
					And db.ptn_pk = @PatientId
					And ItemSourceReferenceID = @VisitId
					And db.ServiceStatus = 0;

			If (@BillItemId Is Not Null) Begin	
				Select @paymentStatus = isnull(@PaymentStatus, 0);

				Exec pr_Billing_SaveBillItem	
					   @PatientId = @PatientId					
					,	@BillItemId = @BillItemId
					,	@BillItemDate = @billitemDate
					,   @PaymentStatus = @paymentStatus
					,	@ItemId = @itemId
					,	@ItemName = @itemName
					,	@ItemType = @itemType
					,	@Quantity = @Quantity					
					,	@UserId = @UserId
					,	@serviceStatus = @ServiceStatus
					,	@ItemSourceReferenceID = @VisitId;
			End
			Delete From @table Where Id = @ServiceOrderId;
	End	 	

End		 		
		
GO

-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 20151130
-- Description:	Update patient date of death on care end
-- =============================================
CREATE TRIGGER [dbo].[TRV_PatientCareEnd]    ON  [dbo].[dtl_PatientCareEnded] 	 AFTER INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for trigger here   
	Update  P 
		Set DateOfDeath = I.DeathDate
	From dbo.mst_Patient P
	Inner Join INSERTED I On I.Ptn_Pk = P.Ptn_Pk
	Where P.DateOfDeath Is Null
	

END

GO

/****** Object:  Trigger [dbo].[TRG_Create_Note]    Script Date: 5/12/2016 5:33:48 PM ******/
/****** Object:  Trigger [TRG_Create_Note]    Script Date: 6/20/2016 10:41:50 AM ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[TRG_Create_Note]'))
DROP TRIGGER [dbo].[TRG_Create_Note]
GO

/****** Object:  Trigger [dbo].[TRG_Create_Note]    Script Date: 6/20/2016 10:41:50 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TRIGGER [dbo].[TRG_Create_Note]    ON  [dbo].[dtl_PatientClinicalNotes] 
   Instead Of INSERT
AS 
BEGIN
Set Nocount On;


Insert Into dtl_PatientClinicalNotes
(	Ptn_Pk
	,	LocationId
	,	Visit_Pk
	,	ClinicalNotes
	,	DeleteFlag
	,	UserId
	,	ModuleId
	,	CreateDate
	,	ModifiedFlag
)
	Select	I.Ptn_Pk
		,	I.LocationId
		,	I.Visit_Pk
		,	I.ClinicalNotes
		,	0
		,	I.UserId
		,	V.ModuleId
		,	getdate()
		,	0
	From Inserted I
	Inner Join ord_Visit V On I.Visit_pk = V.Visit_Id;

End

GO		   

/****** Object:  Trigger [dbo].[TRG_Create_Note]    Script Date: 5/12/2016 6:39:51 PM ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[TRG_Modify_Note]'))
DROP TRIGGER [dbo].[TRG_Modify_Note]
GO
/****** Object:  Trigger [dbo].[TRG_Modify_Note]    Script Date: 6/20/2016 10:42:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Joseph
-- Create date: 10 May 2016
-- Description:	Trigger for Updating and auditing trailing clinical notes
-- =============================================

CREATE TRIGGER [dbo].[TRG_Modify_Note]    ON  [dbo].[dtl_PatientClinicalNotes] 
   Instead Of Update
AS 
BEGIN
Set Nocount On;

	Update N Set
		DeleteFlag= 1,
		DeletedBy = I.UserId,
		DeleteDate= Getdate()
	From dtl_PatientClinicalNotes N 
	Inner Join Inserted I On I.Id= N.Id	 Where I.Deleteflag = 1;
	
	If @@rowcount > 0 Return;

	Update N Set
		ModifiedFlag= 1,
		ModifiedBy = I.UserId,
		UpdateDate= Getdate()
	From dtl_PatientClinicalNotes N 
	Inner Join Inserted I On I.Id= N.Id
	Where N.ClinicalNotes != I.ClinicalNotes;

	If (@@rowcount > 0) Begin 
		Insert Into dtl_PatientClinicalNotes
		(
			Ptn_Pk,
			LocationId,Visit_Pk,
			ClinicalNotes,
			DeleteFlag,
			UserId,
			ModuleId,
			CreateDate,
			ModifiedFlag
		)
		Select	I.Ptn_Pk
				,I.LocationId
				,I.Visit_Pk
				,I.ClinicalNotes
				,0
				,I.UserId
				,I.ModuleId
				,getdate()
				,0
		From Inserted I;
	End
	   
End	   

GO		

/****** Object:  Trigger [TRG_VW_LabResults]    Script Date: 08/25/2016 08:29:53 ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[TRG_VW_LabResults]'))
DROP TRIGGER [dbo].[TRG_VW_LabResults]
GO

/****** Object:  Trigger [dbo].[TRG_VW_LabResults]    Script Date: 08/25/2016 08:29:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Joseph
-- Create date: 10 June 2016
-- Description:	Trigger for preventing inputs to old lab tables
-- =============================================
Create TRIGGER [dbo].[TRG_VW_LabResults]    ON  [dbo].[dtl_PatientLabResults] 
   Instead Of INSERT,UPDATE,Delete
AS 
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
  
  Return;

END

GO
