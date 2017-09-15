
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SCM_SavePurchaseOrderItem_Combined]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SCM_SavePurchaseOrderItem_Combined]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[SCM_SavePurchaseOrderItem_Combined]  
(    
	@POId  int = null,      
	@ItemId int = null,  
	@ItemTypeId int =null,    
	@OrderedQuantity int = null, 
	@IssuedQuantity int = null,  
	@IssuedQuantityDU int = null,    
	@PurchasePrice decimal(9,2)= 0.0,      	 
	@UserId int = null,  
	@BatchId int = null,  
	@AvaliableQty int = null,  
	@ExpiryDate datetime = null,
	@UnitQuantity int = null,
	@SupplierId int= null,
	@SourceStoreID int = null,
	@DestinationStoreID int = null,
	@BatchName varchar(50) = null,
	@IsPOorIST int = null  -- 1 for Purchase order and 2 for Inter store transfer 
)
as       
begin

	if(@BatchName IS NOT null)
	BEGIN
		declare @tempBatchId int, @tempbatchname varchar(100), @tempExpiryDate datetime, @Indexofdelimeter int,	@maxbatchSrno int;
                         
		Select @tempBatchId = ID From Mst_Batch	Where Name = @BatchName	And ItemID = @ItemId;
	
		If(@tempBatchId Is Null) 
		Begin
			Select @maxbatchSrno = Max(SRNO)	From Mst_Batch;
			Insert Into Mst_Batch (name, UserId, CreateDate, SRNO, ItemID, ExpiryDate)
			Values (@BatchName, @UserId, Getdate(), (@maxbatchSrno + 1), @ItemId, @ExpiryDate);
			Select @tempBatchId = SCOPE_IDENTITY();
		End

		SET @BatchId = @tempBatchId
	END




	declare @rowCount int;
	Insert Into Dtl_PurchaseItem (POId,ItemId,Quantity,IssuedQuantity,PurchasePrice,UserId,CreateDate,BatchID,AvaliableQty,ExpiryDate,UnitQuantity)
	Values (@POId,@ItemId,@OrderedQuantity,@IssuedQuantity,@PurchasePrice,@UserId,getdate(),@BatchID,@AvaliableQty,@ExpiryDate,@UnitQuantity);


	Declare @tempQtyPerPurchaseUnit int, @tempTotalRecievedQuantity int, @transactionType nvarchar(50)
	Select	@tempQtyPerPurchaseUnit = QtyPerPurchaseUnit From Mst_Drug Where Drug_pk = @ItemId;

	Select @transactionType =  Case 
			When @IsPOorIST = 2 Then 'Inter store transfer' 
			Else  'Purchase Order' End;

	--Select @tempTotalRecievedQuantity =  Case 
	--		When @IsPOorIST = 2 Then @IssuedQuantity 
	--		Else  @IssuedQuantity * @tempQtyPerPurchaseUnit End; 
	--Select @tempTotalRecievedQuantity =  @IssuedQuantity * @tempQtyPerPurchaseUnit; 

	Insert Into dtl_stocktransaction (ItemId,BatchId,POId,StoreId,TransactionDate,Quantity,ExpiryDate,UserId,CreateDate,transactionType)
	Values (@ItemId,@BatchID,@POId,@DestinationStoreID,Getdate(),@IssuedQuantityDU,@ExpiryDate,@UserId,Getdate(),@transactionType);

	If (@IsPOorIST = 2) 
	Begin
		Insert Into dtl_stocktransaction (ItemId,BatchId,POId,StoreId,TransactionDate,Quantity,ExpiryDate,UserId,CreateDate,transactionType)
		Values (@ItemId,@BatchID,@POId,@SourceStoreID,Getdate(),-@IssuedQuantityDU,@ExpiryDate,@UserId,Getdate(),@transactionType);
	end



	If (@SupplierId Is Not Null) 
	Begin
		Select @rowCount = Count(*) From  Lnk_SupplierItem Where SupplierId = @SupplierId And ItemId=@ItemId And ItemTypeId=@ItemTypeId;
		If(@rowCount = 0) 
		Begin
			Insert Into Lnk_SupplierItem(SupplierId,ItemId, ItemTypeId,UserId,CreateDate) Values(@SupplierId,@ItemId,@ItemTypeId,@UserId,getdate());
		End
	End

	declare @TotalRecievedQuantity int,
			@TotalQuantity int,
			@POstatus int;

	Select @TotalRecievedQuantity = Sum(a.IssuedQuantity)
	From Dtl_PurchaseItem a
	Inner Join
		ord_PurchaseOrder g On g.Poid = a.POId
	Where a.POId = @POId;

	Select @TotalQuantity = Sum(Quantity)	From dtl_PurchaseItem	Where POId = @POId;

	Update ord_PurchaseOrder Set
		Status = Case 
						When (@TotalRecievedQuantity = @TotalQuantity) Then 3 
						When (@TotalRecievedQuantity < @TotalQuantity) Then 4
						Else [Status] End --@POstatus
	Where POId = @POId;
End

;
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SCM_PurchasedReceivedItem_Save]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SCM_PurchasedReceivedItem_Save]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[SCM_PurchasedReceivedItem_Save]  
(    
 @POId  int = null,      
 @ItemId int = null,  
 @ItemTypeId int =null,    
 @OrderedQuantity int = null, 
 @IssuedQuantity int = null,  
 @IssuedQuantityDU int = null,    
 @PurchasePrice decimal(9,2)= 0.0,        
 @UserId int = null,  
 @BatchId int = null,  
 @AvaliableQty int = null,  
 @ExpiryDate datetime = null,
 @UnitQuantity int = null,
 @SupplierId int= null,
 @SourceStoreID int = null,
 @DestinationStoreID int = null,
 @BatchName varchar(50) = null,
 @IsPOorIST int = null  -- 1 for Purchase order and 2 for Inter store transfer 
)
as       
begin

 if(@BatchName IS NOT null)
 BEGIN
  declare @tempBatchId int, @tempbatchname varchar(100), @tempExpiryDate datetime, @Indexofdelimeter int, @maxbatchSrno int;
                         
  Select @tempBatchId = ID From Mst_Batch Where Name = @BatchName And ItemID = @ItemId;
 
  If(@tempBatchId Is Null) 
  Begin
   Select @maxbatchSrno = Max(SRNO) From Mst_Batch;
   Insert Into Mst_Batch (name, UserId, CreateDate, SRNO, ItemID, ExpiryDate)
   Values (@BatchName, @UserId, Getdate(), (@maxbatchSrno + 1), @ItemId, @ExpiryDate);
   Select @tempBatchId = SCOPE_IDENTITY();
  End

  SET @BatchId = @tempBatchId
 END




 declare @rowCount int;
 Insert Into Dtl_PurchaseItem (POId,ItemId,Quantity,IssuedQuantity,PurchasePrice,UserId,CreateDate,BatchID,AvaliableQty,ExpiryDate,UnitQuantity)
 Values (@POId,@ItemId,@OrderedQuantity,@IssuedQuantity,@PurchasePrice,@UserId,getdate(),@BatchID,@AvaliableQty,@ExpiryDate,@UnitQuantity);


 Declare @tempQtyPerPurchaseUnit int, @tempTotalRecievedQuantity int, @transactionType nvarchar(50)
 Select @tempQtyPerPurchaseUnit = QtyPerPurchaseUnit From Mst_Drug Where Drug_pk = @ItemId;

 Select @transactionType =  Case 
   When @IsPOorIST = 2 Then 'Inter store transfer' 
   Else  'Purchase Order' End;

 --Select @tempTotalRecievedQuantity =  Case 
 --  When @IsPOorIST = 2 Then @IssuedQuantity 
 --  Else  @IssuedQuantity * @tempQtyPerPurchaseUnit End; 
 --Select @tempTotalRecievedQuantity =  @IssuedQuantity * @tempQtyPerPurchaseUnit; 

 Insert Into dtl_stocktransaction (ItemId,BatchId,POId,StoreId,TransactionDate,Quantity,ExpiryDate,UserId,CreateDate,transactionType)
 Values (@ItemId,@BatchID,@POId,@DestinationStoreID,Getdate(),@IssuedQuantityDU,@ExpiryDate,@UserId,Getdate(),@transactionType);

 If (@IsPOorIST = 2) 
 Begin
  Insert Into dtl_stocktransaction (ItemId,BatchId,POId,StoreId,TransactionDate,Quantity,ExpiryDate,UserId,CreateDate,transactionType)
  Values (@ItemId,@BatchID,@POId,@SourceStoreID,Getdate(),-@IssuedQuantityDU,@ExpiryDate,@UserId,Getdate(),@transactionType);
 end



 If (@SupplierId Is Not Null) 
 Begin
  Select @rowCount = Count(*) From  Lnk_SupplierItem Where SupplierId = @SupplierId And ItemId=@ItemId And ItemTypeId=@ItemTypeId;
  If(@rowCount = 0) 
  Begin
   Insert Into Lnk_SupplierItem(SupplierId,ItemId, ItemTypeId,UserId,CreateDate) Values(@SupplierId,@ItemId,@ItemTypeId,@UserId,getdate());
  End
 End

 declare @TotalRecievedQuantity int,
   @TotalQuantity int,
   @POstatus int;

 Select @TotalRecievedQuantity = Sum(a.IssuedQuantity)
 From Dtl_PurchaseItem a
 Inner Join
  ord_PurchaseOrder g On g.Poid = a.POId
 Where a.POId = @POId;

 Select @TotalQuantity = Sum(Quantity) From dtl_PurchaseItem Where POId = @POId;

 Update ord_PurchaseOrder Set
  Status = Case 
      When (@TotalRecievedQuantity = @TotalQuantity) Then 3 
      When (@TotalRecievedQuantity < @TotalQuantity) Then 4
      Else [Status] End --@POstatus
 Where POId = @POId;
End
Go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SCM_GetPurchaseOrderItems]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SCM_GetPurchaseOrderItems]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 2017-Mar-20
-- Description:	Get purchase order items
-- =============================================
Create PROCEDURE [dbo].[SCM_GetPurchaseOrderItems] 
	-- Add the parameters for the stored procedure here
	@PoId int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select	a.POId
		,	a.LocationID
		,	a.SupplierID
		,	sup.SupplierName
		,	a.OrderDate
		,	a.AuthorizedBy
		,	a.PreparedBy
		,	a.Status
		,	a.SourceStoreID
		,	str1.Name								As SourceStoreName
		,	a.DestinStoreID
		,	str2.Name								As DestinationStoreName
		,	a.UserID
		,	Isnull(nullif(a.PONumber,''), a.OrderNo)			As OrderNo
		,	b.RecievedDate
		,	b.Freight
		,	b.Tax
		,	b.GRNId
		,	emp1.FirstName + ' ' + emp1.LastName	As AuthorizeName
		,	emp2.FirstName + ' ' + emp2.LastName	As PreparedName
		,	(Select sum(D.Quantity * D.PurchasePrice) From Dtl_PurchaseItem D Where (D.POId =  A.POId)) TotalAmount
	From ord_PurchaseOrder As a
	Left Outer Join Ord_GRNote As b On b.POId = a.POId
	Left Outer Join Mst_Supplier As sup On a.SupplierID = sup.Id
	Left Outer Join Mst_Store As str1 On str1.Id = a.SourceStoreID
	Left Outer Join Mst_Store As str2 On str2.Id = a.DestinStoreID
	Left Outer Join mst_Employee As emp1 On emp1.EmployeeID = a.AuthorizedBy
	Left Outer Join mst_Employee As emp2 On emp2.EmployeeID = a.PreparedBy
	Where (a.POId = @PoId)  ;

	;With Rec as (Select	O.POId
		,	D.GRNId
		,	D.ItemId
		,	D.RecievedQuantity
		,	D.FreeRecievedQuantity
		,	D.PurchasePrice
		,	D.TotPurchasePrice
		,	B.Name	BatchNumber
		,	B.ExpiryDate
		,	R.UserFirstName + ' ' + R.UserLastName ReceivedBy
		,	D.CreateDate ReceivedDate
	From Dtl_GRNote D
	Inner Join Ord_GRNote O On D.GRNId = O.GRNId
	Inner Join Mst_Batch B On B.ID = D.BatchID
	Left Outer Join mst_User As R On D.UserId = R.UserID
	Where B.DeleteFlag = 0	And O.POId = @PoId
	)
	Select	I.POId
		,	D.DrugID								As ItemCode
		,	I.ItemId
		,	D.DrugName								As ItemName
		,	I.PurchasePrice							As Price
		,	I.Quantity								As OrderQuantity
		,	I.Quantity * I.PurchasePrice			As TotPrice
		,	I.CreateDate	PurchaseDate
		,	D.QtyPerPurchaseUnit
		,	U.Name									As Units
		,	Rec.RecievedQuantity
		,	Rec.BatchNumber
		,	Rec.ExpiryDate
		,	I.UserId
		,	R.UserFirstName + ' ' + R.UserLastName	As IssuedBy
		,	Rec.ReceivedBy
		,	Rec.ReceivedDate
	From Dtl_PurchaseItem I
	Left Outer Join Mst_Drug D On I.ItemId = D.Drug_pk
	Left Outer Join Mst_DispensingUnit As U On D.PurchaseUnit = U.Id
	Left Outer Join mst_User As R On I.UserId = R.UserID
	Left Outer Join Rec On rec.POId = I.POId And rec.ItemId = I.ItemId
	Where I.POId = @PoId

END

;
Go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_SCM_BINCard_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_SCM_BINCard_Futures]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[pr_SCM_BINCard_Futures]                                                        
@StoreId int,                                                    
@ItemId int,                                                    
@FromDate datetime,                                                    
@ToDate datetime,
@LocationId int                                                    
                                                        
AS                                                                  
BEGIN                                                               
--0    
select dr.drugid,dr.drugname,du.name[disUnit] from mst_drug dr inner join mst_dispensingunit du
on dr.dispensingunit = du.id where dr.drug_pk=@ItemId
--1
select name[storeName],@FromDate[dateFrom],@ToDate[dateTo]  from mst_store where id = @StoreId
--2

DECLARE @tempTbl TABLE 
([Date] datetime,VoucherNo varchar(50),username varchar(50),Receipts int,Issues int,BatchNo varchar(50),ExpiryDate datetime, Balance int)
 
DECLARE @transactiodate datetime,@voucherNo varchar(50),@username varchar(50),@receipts int,@issues int,@batch varchar(50),@expirydate datetime,
@balance int, @balanceBF int
 
SET @balance = 0
 
DECLARE rt_cursor CURSOR
FOR
--------------------------balance b/f----------------------
select @FromDate transactiondate,'Balance b/f' OrderNo,'' username,
0 [Receipts], 0 [Issues], '' name, null expirydate, isnull(sum(quantity),0) balanceBF 
from dtl_stocktransaction dtl
where dtl.itemid = @ItemId and dtl.storeid = @StoreId and dtl.TransactionDate < @FromDate
--order by dtl.transactiondate
UNION
-------------------------------------------------
select dtl.transactiondate,isnull(nullif(ord.ponumber,''),ord.OrderNo) OrderNo,usr.username,
case when dtl.quantity >=0 then dtl.quantity else 0 end [Receipts],
case when dtl.quantity <0 then dtl.quantity else 0 end [Issues],
bch.name,dtl.expirydate, 0 balanceBF 
from dtl_stocktransaction dtl inner join mst_user usr on dtl.userid = usr.userid 
inner join mst_store store on dtl.storeid = store.id
inner join mst_batch bch on dtl.batchid = bch.id
left join ord_purchaseorder ord on dtl.POId = ord.POId
where dtl.itemid = @ItemId and dtl.storeid = @StoreId 
and CONVERT(datetime,CONVERT(VARCHAR(10),dtl.transactiondate,10)) >= CONVERT(datetime,CONVERT(VARCHAR(10),@FromDate,10)) 
and CONVERT(datetime,CONVERT(VARCHAR(10),dtl.transactiondate,10)) <= CONVERT(datetime,CONVERT(VARCHAR(10),@ToDate,10))
--order by dtl.transactiondate asc
 
OPEN rt_cursor
 
FETCH NEXT FROM rt_cursor INTO @transactiodate,@voucherNo,@username,@receipts,@issues,@batch,@expirydate,@balanceBF
 
WHILE @@FETCH_STATUS = 0
 BEGIN
 SET @balance = @balance + @receipts + @issues + @balanceBF
 INSERT @tempTbl VALUES (@transactiodate,@voucherNo,@username,@receipts,@issues,@batch,@expirydate,@balance)
 FETCH NEXT FROM rt_cursor INTO @transactiodate,@voucherNo,@username,@receipts,@issues,@batch,@expirydate,@balanceBF
 END
 
CLOSE rt_cursor
DEALLOCATE rt_cursor
 
SELECT * FROM @tempTbl order by [Date] asc

-----3
select facilitylogo from mst_facility where facilityid = @LocationId                                           
                        
                                     
END

;
Go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_SCM_SavePurchaseOrderMaster_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_SCM_SavePurchaseOrderMaster_Futures]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[pr_SCM_SavePurchaseOrderMaster_Futures]       ( 
	@LocationID  int,        
	@SupplierID int ,        
	@OrderDate datetime ,        
	@AuthorizedBy int,        
	@PreparedBy int=null,        
	@Status int,        
	@SourceStoreID int,        
	@DestinStoreID int,        
	@UserID int,  
	--@IsRejectedStatus bit =0,  
	@IsUpdate bit =0,  
	@Poid int =0  ,
	@PONumber varchar(36)= null
) 
as         
Begin
         
	declare @temPoId int;
        
	if(@IsUpdate =0 )  begin
		Insert Into ord_PurchaseOrder (
			LocationID,
			SupplierID,
			OrderDate,
			AuthorizedBy,
			PreparedBy,
			Status,
			SourceStoreID,
			DestinStoreID,
			UserID,
			CreateDate,
			PONumber)
		Values (
			@LocationID,
			@SupplierID,
			@OrderDate,
			@AuthorizedBy,
			@PreparedBy,
			@Status,
			@SourceStoreID,
			@DestinStoreID,
			@UserID,
			Getdate(),
			nullif(@PONumber,'') );
		Set @temPoId = scope_identity();
		Update ord_PurchaseOrder Set
			OrderNo = 'PO-' + Convert(varchar(100), @temPoId)
		Where POID = @temPoId;
	End 
	Else Begin
		Update ord_PurchaseOrder Set
			AuthorizedBy = @AuthorizedBy,
			PreparedBy = @PreparedBy,
			Status = @Status,
			DestinStoreID = @DestinStoreID,
			UpdateDate = Getdate()
		Where POId = @Poid;
		Set @temPoId = @Poid;
	End
	Select @temPoId
End;
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_SCM_SavePurchaseOrderItem_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_SCM_SavePurchaseOrderItem_Futures]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[pr_SCM_SavePurchaseOrderItem_Futures]  
(    
	@POId  int,      
	@ItemId int ,  
	@ItemTypeId int =null,    
	@Quantity int ,      
	@PurchasePrice decimal(9,2)= 0.0,      	 
	@UserId int ,  
	@BatchId int ,  
	@AvaliableQty  int ,  
	@ExpiryDate datetime   ,
	@UnitQuantity int ,
	@SupplierId int= null
)
as       
begin

	declare @rowCount int;
	Insert Into Dtl_PurchaseItem (
			POId
		,	ItemId
		,	Quantity
		,	PurchasePrice
		,	UserId
		,	CreateDate
		,	BatchID
		,	AvaliableQty
		,	ExpiryDate
		,	UnitQuantity)
	Values (
			@POId
		,	@ItemId
		,	@Quantity
		,	@PurchasePrice
		,	@UserId
		,	getdate()
		,	@BatchID
		,	@AvaliableQty
		,	@ExpiryDate
		,	@UnitQuantity);
		If (@SupplierId Is Not Null) Begin
			Select @rowCount = Count(*) From  Lnk_SupplierItem Where SupplierId = @SupplierId And ItemId=@ItemId And ItemTypeId=@ItemTypeId;
			If(@rowCount = 0) Begin
				Insert Into Lnk_SupplierItem(SupplierId,ItemId, ItemTypeId,UserId,CreateDate) Values(@SupplierId,@ItemId,@ItemTypeId,@UserId,getdate());
			End
		End

End
;
Go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_SCM_GetPurchaseOrderGRNByPoid_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_SCM_GetPurchaseOrderGRNByPoid_Futures]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[pr_SCM_GetPurchaseOrderGRNByPoid_Futures]                                                                
@POid  int                                                                     
as                                                                 
begin                                                                 
                                                  
--0                                                      
Select a.POId
	 , a.LocationID
	 , a.SupplierID
	 , sup.SupplierName
	 , convert(varchar(15), a.OrderDate, 106) [OrderDate]
	 , a.AuthorizedBy
	 , a.PreparedBy
	 , a.Status
	 , a.SourceStoreID
	 , str1.Name							   [SourceStoreName]
	 , a.DestinStoreID
	 , str2.Name							   [DestinationStoreName]
	 , a.UserID
	 , isnull(a.ponumber, a.OrderNo)		   OrderNo
	 , b.RecievedDate
	 , b.Freight
	 , b.Tax
	 , b.GRNId
	 , emp1.FirstName + ' ' + emp1.LastName	   [AuthorizeName]
	 , emp2.FirstName + ' ' + emp2.LastName	   [PreparedName]
From ord_PurchaseOrder a
Left Outer Join Ord_Grnote b On b.POId = a.POId
Left Outer Join Mst_Supplier sup On a.SupplierID = sup.Id
Left Outer Join Mst_Store str1 On str1.Id = a.SourceStoreID
Left Outer Join Mst_Store str2 On str2.Id = a.DestinStoreID
Left Outer Join dbo.mst_Employee emp1 On emp1.EmployeeID = a.AuthorizedBy
Left Outer Join dbo.mst_Employee emp2 On emp2.EmployeeID = a.PreparedBy
Where a.POId = @POid                                                          
                                                
                              
                                  
declare @ISTstoreID int                                   
set @ISTstoreID= 0                                  
select @ISTstoreID =SourceStoreID from ord_PurchaseOrder where  supplierid = 0 and SourceStoreID >0 and poid =@POid                                  
if(@ISTstoreID >0)                                  
begin                            
            
print 'a'            
--1                          
            
Select isnull(d.GRNId, 0)																									  [GRNId]
	 , a.POId
	 , b.DrugID																												  [ItemCode]
	 , a.ItemId
	 , (convert(varchar(100), a.ItemId) + '-' + convert(varchar(100), a.BatchId) + '-' + convert(varchar, a.ExpiryDate, 101)) [ISTItemID]
	 , b.DrugName																											  [ItemName]
	 , a.Quantity																											  [OrderQuantity]
	 , a.Quantity * a.PurchasePrice																							  [TotPrice]
	 , a.PurchasePrice																										  [Price]
	 , a.Unit																												  [UnitID]
	 , c.Name																												  [Units]
	 , a.UserId
	 , z.[TotalAmount]
	 , e.Name																												  [BatchName]
	 , a.BatchId
	 , a.ExpiryDate
	 , muser.UserFirstName + ' ' + muser.UserLastName																		  [IssuedBy]
From dtl_PurchaseItem a
Left Outer Join mst_drug b On a.ItemId = b.Drug_pk
Left Outer Join Ord_Grnote d On d.POId = a.POId
Left Outer Join mst_dispensingUnit c On b.PurchaseUnit = c.ID
Left Outer Join mst_batch e On a.BatchId = e.ID
Left Outer Join mst_user muser On a.UserId = muser.userId
Cross Apply (
Select sum(a.Quantity * a.PurchasePrice) [TotalAmount]
From dtl_PurchaseItem a
Left Outer Join mst_drug b On a.ItemId = b.Drug_pk
Where a.POId = @POid
) z
Where a.POId = @POid                             
                          
                          
--2                          
select 0[AutoID], e.GRNId, a.POId,b.DrugName [ItemName],a.ItemId,a.BatchID,f.Name[BatchName],isnull(e.RecievedQuantity,0)[RecievedQuantity],e.FreeRecievedQuantity,                                              
--e.PurchasePrice                                  
e.PurchasePrice[ItemPurchasePrice],b.PurchaseUnitPrice[MasterPurchaseprice] ,e.SellingPrice                           
,Convert(varchar(100),a.ExpiryDate,101)ExpiryDate                                
,b.DispensingMargin[Margin],e.SellingPricePerDispense,b.QtyPerPurchaseUnit,e.UserId ,g.SourceStoreID,                      
--e.TotPurchasePrice[TotPurchasePrice],                        
 e.TotPurchasePrice [TotPurchasePrice],                              
g.DestinStoreID ,(convert(varchar(100),a.ItemId)+'-'+convert(varchar(100),a.BatchId) +'-'+convert(varchar,a.ExpiryDate,101))[ISTItemID],e.createdate                    
 from                          
dtl_PurchaseItem a left outer join mst_drug b   on a.ItemId =b.Drug_pk                                               
left outer join Ord_Grnote d on d.POId=a.POId right  outer join                        
 dtl_Grnote e on (e.GRNId =d.GRNId  and e.ItemId = a.ItemId and e.batchId=a.BatchID and e.expiryDate= a.expiryDate) left outer join  mst_batch f                                                
on f.ID =a.BatchID   left  outer join  ord_PurchaseOrder g   on   g.Poid=  a.POId                          
--left outer  join  dtl_StockTransaction stktan on g.SourceStoreID = stktan.StoreId and e.ItemId = stktan.ItemId                           
--and f.ID= e.BatchID and   e.ExpiryDate = a.ExpiryDate                                        
where a.POId =@POid and  g.SourceStoreID =@ISTstoreID  order by   e.createdate asc                      
                        
                       
                         
                      
                         
-----3                               
SELECT  a.Id,a.Name+'~'+ convert(varchar(50),b.ExpiryDate,101)[Name],a.DeleteFlag,b.ExpiryDate,c.ItemID,b.StoreId,                          
(convert(varchar(100),c.ItemId)+'-'+convert(varchar(100),c.BatchId) +'-'+convert(varchar,c.ExpiryDate,101))[ISTItemID]                           
FROM [dbo].[mst_batch] a inner  join dtl_stocktransaction b                                  
on a.ID= b.BatchId    inner   join ord_PurchaseOrder d on                                  
d.SourceStoreID = b.StoreId   inner   join dtl_PurchaseItem  c on c.ItemId =b.ItemId and c.BatchID=b.BatchId and b.ExpiryDate =c.ExpiryDate                                
where c.poid =@POid and  d.SourceStoreID =@ISTstoreID group by a.Id,a.Name,a.DeleteFlag,b.ExpiryDate,c.ItemID,b.StoreId,(convert(varchar(100),c.ItemId)+'-'+convert(varchar(100),c.BatchId) +'-'+convert(varchar,c.ExpiryDate,101))                            
 
     
     
                   
---4                  
Select 0																													  [AutoID]
	 , ''																													  GRNId
	 , a.POId
	 , b.DrugName																											  [ItemName]
	 , a.ItemId
	 , a.BatchID
	 , f.Name																												  [BatchName]
	 , isnull(e.RecievedQuantity, 0)																						  [RecievedQuantity]
	 , e.FreeRecievedQuantity
	 ,
	   --e.PurchasePrice                                  
	   b.PurchaseUnitPrice																									  [ItemPurchasePrice]
	 , b.PurchaseUnitPrice																									  [MasterPurchaseprice]
	 , isnull(e.SellingPrice, 0)																							  SellingPrice
	 , a.ExpiryDate
	-- , convert(varchar(100), a.ExpiryDate, 101)																				  ExpiryDate
	 , b.DispensingMargin																									  [Margin]
	 , isnull(e.SellingPricePerDispense, 0)																					  SellingPricePerDispense
	 , b.QtyPerPurchaseUnit
	 , a.UserId
	 , g.SourceStoreID
	 , '0'																													  [TotPurchasePrice]
	 , g.DestinStoreID
	 , (convert(varchar(100), a.ItemId) + '-' + convert(varchar(100), a.BatchId) + '-' + convert(varchar, a.ExpiryDate, 101)) [ISTItemID]
	 , e.createdate
From dtl_PurchaseItem a
Left Outer Join mst_drug b On a.ItemId = b.Drug_pk
Left Outer Join Ord_Grnote d On d.POId = a.POId
Left Outer Join dtl_Grnote e On (e.GRNId = d.GRNId And e.ItemId = a.ItemId And e.batchId = a.BatchID And e.expiryDate = a.expiryDate)
Left Outer Join mst_batch f On f.ID = a.BatchID
Left Outer Join ord_PurchaseOrder g On g.Poid = a.POId
Where a.POId = @POid
	And g.SourceStoreID = @ISTstoreID
Order By e.createdate Asc                     
                          
                                  
end                                   
else              
begin                            
                          
--1                          
select  isnull(d.GRNId,0)[GRNId], a.POId,b.DrugID [ItemCode],a.ItemId,b.DrugName [ItemName],a.Quantity [OrderQuantity],a.Quantity * a.PurchasePrice [TotPrice] ,a.PurchasePrice [Price],a.Unit[UnitID],c.Name[Units] ,a.UserId                        
,z.[TotalAmount],a.ItemId,(convert(varchar(100),a.ItemId)+'-'+convert(varchar(100),a.BatchId) +'-'+convert(varchar,a.ExpiryDate,101))[ISTItemID] from                                                        
dtl_PurchaseItem a left outer join mst_drug b  on a.ItemId =b.Drug_pk  left outer join  Ord_Grnote d on                                              
d.POId=a.POId    left outer join mst_dispensingUnit c on b.PurchaseUnit= c.ID                                                     
cross Apply                                                       
(select sum(a.Quantity * a.PurchasePrice)[TotalAmount]                                                         
from dtl_PurchaseItem a left outer join mst_drug b                                                        
on a.ItemId =b.Drug_pk  where a.POId =@POid)z                                                       
where a.POId =@POid                             
                          
                          
                          
 --2                                      
--isnull(e.GRNId,-1)[GRNId]                                              
select 0[AutoID], e.GRNId, a.POId,b.DrugName [ItemName],a.ItemId,e.BatchID,f.Name[BatchName],e.RecievedQuantity[RecievedQuantity],e.FreeRecievedQuantity,                                              
--e.PurchasePrice                                  
e.PurchasePrice[ItemPurchasePrice],b.PurchaseUnitPrice[MasterPurchaseprice] ,e.SellingPrice ,Convert(varchar(100),e.ExpiryDate,101)ExpiryDate,                                  
b.DispensingMargin[Margin],e.SellingPricePerDispense,b.QtyPerPurchaseUnit,e.UserId ,g.SourceStoreID,e.TotPurchasePrice[TotPurchasePrice],                                  
g.DestinStoreID,a.ItemId,(convert(varchar(100),a.ItemId)+'-'+convert(varchar(100),a.BatchId) +'-'+convert(varchar,a.ExpiryDate,101))[ISTItemID] from                                                        
dtl_PurchaseItem a left outer join mst_drug b   on a.ItemId =b.Drug_pk                                               
left outer join Ord_Grnote d on d.POId=a.POId left outer join  dtl_Grnote e on (e.GRNId =d.GRNId  and e.ItemId = a.ItemId) left outer join  mst_batch f                                                
on f.ID =e.BatchID   inner join   ord_PurchaseOrder g   on   g.Poid=  a.POId                                                     
where a.POId =@POid    order by   e.createdate asc                                
                          
                          
  --3                               
SELECT  Id,Name,DeleteFlag,ItemID,ExpiryDate FROM [dbo].[mst_batch]  WHERE DeleteFlag = 0  or   DeleteFlag is   null                                   
end                                           
--SELECT  Id,Name,DeleteFlag FROM [dbo].[mst_batch]  WHERE DeleteFlag = 0  or   DeleteFlag is   null                                                  
                                                              
end

GO


