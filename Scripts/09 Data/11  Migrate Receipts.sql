SET NOCOUNT ON;
Go
If (not Exists (Select * From sys.columns Where Name = N'ReceiptData'  And Object_ID = Object_id(N'dtl_BillingReceipt') And system_type_id=TYPE_ID('xml'))    
	) And not exists(Select 1 From sys.columns Where Name = N'XmlData'  And Object_ID = Object_id(N'dtl_BillingReceipt') And system_type_id=TYPE_ID('xml'))
Begin
	ALTER TABLE [dbo].[dtl_BillingReceipt] ADD [XmlData] xml NULL
End
GO
Declare @SymKey varchar(800) 
		Set @SymKey = 'Open symmetric key Key_CTC decryption by password='+  N'''ttwbvXWpqb5WOLfLrBgisw==''' + ''                                                              
		exec(@SymKey)

If Exists (Select * From sys.columns Where Name = N'XmlData'  And Object_ID = Object_id(N'dtl_BillingReceipt') And system_type_id=TYPE_ID('xml')) Begin
		Exec sys.sp_executesql N'update dtl_BillingReceipt Set XmlData = convert(xml, convert(varchar(max), decryptbykey(ReceiptData)));'	

End
Close symmetric key Key_CTC	
Go
If Exists (Select * From sys.columns Where Name = N'ReceiptData'  And Object_ID = Object_id(N'dtl_BillingReceipt') And system_type_id=TYPE_ID('varbinary')) Begin
	ALTER TABLE [dbo].[dtl_BillingReceipt] DROP COLUMN [ReceiptData]
End    
Go
If Not Exists (Select * From sys.columns Where Name = N'ReceiptData'  And Object_ID = Object_id(N'dtl_BillingReceipt') And system_type_id=TYPE_ID('xml')) Begin
	ALTER TABLE [dbo].[dtl_BillingReceipt] ADD [ReceiptData] xml NULL
End  
Go
If Exists (Select * From sys.columns Where Name = N'XmlData'  And Object_ID = Object_id(N'dtl_BillingReceipt') And system_type_id=TYPE_ID('xml')) Begin
	If Exists (Select * From sys.columns Where Name = N'ReceiptData'  And Object_ID = Object_id(N'dtl_BillingReceipt') And system_type_id=TYPE_ID('xml')) Begin
		Exec sys.sp_executesql N'update dtl_BillingReceipt Set ReceiptData = xmlData;'
	End 
End
Go

If Exists (Select * From sys.columns Where Name = N'XmlData'  And Object_ID = Object_id(N'dtl_BillingReceipt') And system_type_id=TYPE_ID('xml')) Begin
	ALTER TABLE [dbo].[dtl_BillingReceipt] DROP COLUMN [XmlData]
End 
Go
If Exists (Select * From sys.columns Where Name = N'ReceiptData'  And Object_ID = Object_id(N'dtl_BillingReceipt') And system_type_id=TYPE_ID('xml')) Begin
	ALTER TABLE [dbo].[dtl_BillingReceipt] alter COLUMN ReceiptData xml Not null
End 
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Billing_CreateReceipt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Billing_CreateReceipt]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[pr_Billing_CreateReceipt] (
	@patientId int,
	@TransactionId int,
	@ReceiptDate datetime,
	@ReceiptType int,
	@ReceiptNumber varchar(100),
	@ReceiptData xml,
	@UserId int,
	@PrintCount int)
As Begin



	Insert Into dtl_BillingReceipt(
			Ptn_PK,
			TransactionId,
			ReceiptDate,
			ReceiptType,
			ReceiptNumber,			
			ReceiptData,
			UserId,
			PrintCount
		)	
		Select
			@PatientId,
			@TransactionID,
			@ReceiptDate,
			@ReceiptType,
			@ReceiptNumber,	
			@ReceiptData,
			@UserId,
			@PrintCount

		

End

GO
/****** Object:  StoredProcedure [dbo].[pr_Billing_Save_PaymentReceipts]    Script Date: 11/30/2015 20:09:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Billing_Save_PaymentReceipts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Billing_Save_PaymentReceipts]
GO
/****** Object:  StoredProcedure [dbo].[pr_Billing_Save_PaymentReceipts]    Script Date: 11/30/2015 20:09:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Vincent Yahuma
-- Create date: 2014-May-16
-- Description:	Query to get Reciept
-- =============================================
CREATE PROCEDURE [dbo].[pr_Billing_Save_PaymentReceipts](
	@TransactionID int,
	@locationID int,
	@password varchar(50), 
	@xml xml= null Output 
)
	-- Add the parameters for the stored procedure here

AS
BEGIN

	Declare @SymKey varchar(400)
	Set @SymKey = 'Open symmetric key Key_CTC decryption by password=' + @password + ''
	Exec (@SymKey)

	Declare @Items xml, @Transaction xml;
	---Items
	If Exists (Select 1	From dbo.dtl_Bill Where TransactionId = @TransactionID) Begin
		Set @Items = 
		(
			Select	billItemID ID,
									Convert(varchar, D.BillItemDate, 112) BillItemDate,
									ItemName Item,
									Quantity,
									(SellingPrice - D.Discount) SellingPrice,
									(Quantity * (SellingPrice - D.Discount)) Amount,
									UR.UserFirstName,
									UR.UserLastName,
									UR.UserID
				From dbo.dtl_Bill D
				Inner Join
					dbo.ord_bill As T On D.TransactionId = T.TransactionID
				Inner Join
					dbo.mst_User As UR On UR.UserID = D.CreatedBy
				Where T.TransactionId = @TransactionID
				For xml Raw ('Items'), Elements
		);
	End 
	Else Begin
		Set @Items = 
		(
			Select	billItemID ID,
								Convert(varchar, D.BillItemDate, 112) BillItemDate,
								ItemName Item,
								Quantity,
								(SellingPrice - D.Discount) SellingPrice,
								(Quantity * (SellingPrice - D.Discount)) Amount,
								UR.UserFirstName,
								UR.UserLastName,
								UR.UserID
			From dbo.dtl_Bill D
			Inner Join
				dbo.ord_bill As T On D.BillID = T.BillID
			Inner Join
				dbo.mst_User As UR On UR.UserID = D.CreatedBy
			Where T.TransactionId = @TransactionID
			For xml Raw ('Items'), Elements
		);
	End

--Transaction Summary

	Set @Transaction = 
	(
		Select	P.PatientFacilityID As PatientID,
			Convert(varchar(50), Decryptbykey(P.LastName)) As PatientLastName,
			Convert(varchar(50), Decryptbykey(P.FirstName)) As PatientFirstName,
			(Select Name	From dbo.mst_Decode As dc		Where (P.Sex = ID))	As PatientSex,
			FacilityName,
			Nullif(Ltrim(Rtrim(FacilityTel)), '') FacilityTel,
			F.FacilityCell,
			F.FacilityFax,
			FacilityAddress,
			FacilityEmail,
			FacilityFooter,
			FacilityURL,
			FacilityLogo,
			Currency,
			B.BillNumber InvoiceNumber,
			B.BillDate InvoiceDate,
			B.BillAmount InvoiceAmount,
			T.ReceiptNumber ReceiptNumber,
			T.AmountPayable Amount,
			T.Discount,
			T.TenderedAmount,
			B.BillAmount - (Select Isnull(Sum(O.Amount),0.00)	From ord_bill O	Where O.BillID = T.BillID	And O.TransactionStatus = 'Paid')OutstandingAmount,
			0.00 AvailableDeposit,
			Case When T.TenderedAmount = 0.00 Then 0.00 Else T.TenderedAmount - T.AmountPayable End Change,
			T.TransactionDate,
			PT.PaymentName TransactionType,
			T.RefNumber,
			U.UserLastName CashierLastName,
			UserFirstName CashierFirstName,
			T.userID CashierUserID
		From dbo.mst_Patient P
		Inner Join
		dbo.mst_Bill B On B.ptn_pk = P.Ptn_Pk
		Inner Join
		dbo.ord_bill As T On B.BillID = T.BillID
		Inner Join
		dbo.mst_Facility F On F.FacilityID = B.LocationID
		Inner Join
		dbo.mst_User U On U.UserID = T.userID
		Inner Join
		dbo.vw_BillPaymentType PT On PT.ID = T.TransactionType
		Where T.TransactionID = @TransactionID
		For xml Raw ('Transaction'), Elements
	);

Close Symmetric Key Key_CTC
Set @xml = (Select	@Items,
					@Transaction
	For xml Path ('Receipt'))
--Select @xml;	



End
GO


/****** Object:  StoredProcedure [dbo].[pr_Billing_Report_SaveReversalReciept]    Script Date: 11/30/2015 20:09:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Billing_Report_SaveReversalReciept]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Billing_Report_SaveReversalReciept]
GO

/****** Object:  StoredProcedure [dbo].[pr_Billing_Report_SaveReversalReciept]    Script Date: 11/30/2015 20:09:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 2014-May-16
-- Description:	Query to get Reciept for reversals
-- =============================================
CREATE PROCEDURE [dbo].[pr_Billing_Report_SaveReversalReciept](
	@TransactionID int,
	@locationID int,
	@password varchar(50),
	@xml xml= null Output
)
	-- Add the parameters for the stored procedure here

AS
BEGIN
Declare @Items xml, @Transaction xml;
Declare @SymKey varchar(400)
Set @SymKey = 'Open symmetric key Key_CTC decryption by password=' + @password + ''
Exec (@SymKey)


---Items
Set @Items = 
(
	Select TransactionID ID,
			Convert(varchar, T.RefundDate, 112) BillItemDate,
			'Payment Refund' Item,
			1 Quantity,
			T.Amount SellingPrice,
			T.AmountPayable Amount,
			UR.UserFirstName,
			UR.UserLastName,
			UR.UserID
	From dbo.vw_Billing_BillTransaction T 
	Inner Join 
		dbo.mst_User As UR	On UR.UserID = T.RefundBy	
	Where T.TransactionReversalID=@TransactionID And T.Refunded = 1
	For XML RAW ('Items'), ELEMENTS
);

--Transaction Summary

Set @Transaction=
(	
	Select	Coalesce(P.PatientEnrollmentID, P.PatientClinicID,P.IQNumber) As PatientID,
		Convert(varchar(50), Decryptbykey(P.LastName)) As PatientLastName,
		Convert(varchar(50), Decryptbykey(P.FirstName)) As PatientFirstName,
		(Select Name	From dbo.mst_Decode As dc		Where (P.Sex = ID))	As PatientSex,
		FacilityName,
		Nullif(ltrim(rtrim(FacilityTel)),'') FacilityTel,
		F.FacilityCell,
		F.FacilityFax,
		FacilityAddress,
		FacilityEmail,
		FacilityFooter,
		FacilityURL,		
		FacilityLogo,
		Currency,
		T.BillNumber InvoiceNumber,
		T.BillDate InvoiceDate,
		T.BillAmount InvoiceAmount,
		T.ReversalReference ReceiptNumber,	
		T.AmountPayable Amount,		
		T.TenderedAmount,
		0.0 OutstandingAmount,
		0.00 AvailableDeposit,
		Case When T.TenderedAmount = 0.00 Then 0.00 Else T.TenderedAmount - T.AmountPayable End Change,
		T.RefundDate TransactionDate,		 
		T.PaymentName TransactionType ,
		T.ReceiptNumber RefNumber,			
		U.UserLastName CashierFirstName,
		U.UserFirstName CashierLastName,
		T.RefundBy CashierUserID,
		0 TenderedAmount
From dbo.mst_Patient P 
Inner Join
	dbo.vw_Billing_BillTransaction T On T.Ptn_PK = P.Ptn_Pk
Inner Join
	dbo.mst_Facility F On F.FacilityID = T.LocationID
Inner Join 
	dbo.mst_User U On U.UserID =T.RefundBy	
--Inner Join 
--	dbo.vw_BillPaymentType PT On PT.ID = T.TransactionType
Where T.TransactionReversalID=@TransactionID 
And Refunded =1
For XML RAW ('Transaction'), ELEMENTS
);

Close symmetric key Key_CTC
Set @xml = (
Select @Items, @Transaction  for xml path('Receipt')
)

	
END



GO


/****** Object:  StoredProcedure [dbo].[pr_Billing_Report_SaveDepositTransaction_Receipt]    Script Date: 11/30/2015 20:08:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Billing_Report_SaveDepositTransaction_Receipt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Billing_Report_SaveDepositTransaction_Receipt]
GO

/****** Object:  StoredProcedure [dbo].[pr_Billing_Report_SaveDepositTransaction_Receipt]    Script Date: 11/30/2015 20:08:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Vincent Yahuma
-- Create date: 2014-May-16
-- Description:	Query to get Reciept
-- =============================================
CREATE PROCEDURE [dbo].[pr_Billing_Report_SaveDepositTransaction_Receipt](
	@TransactionID int,
	@locationID int,
	@password varchar(50),
	@xml xml= null Output)
	-- Add the parameters for the stored procedure here

AS
BEGIN

	Declare @SymKey varchar(400)
	Set @SymKey = 'Open symmetric key Key_CTC decryption by password=' + @password + ''
	Exec (@SymKey)
	Set @xml = 
	(
		Select	
			T.TransactionID ID,
			Convert(varchar, T.TransactionDate, 112) TransactionDate,
			DepositType + ' ' + T.TransactionDescription TransactionDescription,
			T.DepositType TransactionType,
			T.ReferenceNumber ReceiptNumber,
			Amount TransactionAmount,
			dbo.fn_Billing_PatientAvailableDeposit(T.Ptn_PK, @LocationId) AvailableDeposit,
			UR.UserFirstName CashierFirstName,
			UR.UserLastName CashierLastName,
			UR.UserID CashierUserId,
			P.PatientFacilityID As PatientID,
			Convert(varchar(50), Decryptbykey(P.LastName)) As PatientLastName,
			Convert(varchar(50), Decryptbykey(P.FirstName)) As PatientFirstName,
			(Select Name	From dbo.mst_Decode As dc	Where (P.Sex = ID))As PatientSex,
			FacilityName,
			Nullif(Ltrim(Rtrim(FacilityTel)), '') FacilityTel,
			F.FacilityCell,
			F.FacilityFax,
			FacilityAddress,
			FacilityEmail,
			FacilityFooter,
			FacilityURL,
			FacilityLogo,
			Currency
		From dbo.dtl_BillDepositTransaction T
		Inner Join
			dbo.mst_User As UR On UR.UserID = T.UserID
		Inner Join
			dbo.mst_Patient P On P.Ptn_Pk = T.Ptn_PK
		Inner Join
			dbo.mst_Facility F On F.FacilityID = T.LocationID
		Where T.TransactionId = @TransactionID
		And T.LocationID = @LocationID
		And T.TransactionDescription In ('Refund', 'Deposit')
		For xml Raw ('Transaction'), Root ('Receipt'), Elements
	);


	Close Symmetric Key Key_CTC

End
GO


Declare @Password varchar(50)
Set @Password = N'''ttwbvXWpqb5WOLfLrBgisw=='''

IF OBJECT_ID('tempdb..#Receipt') IS NOT NULL Drop Table #Receipt 
IF OBJECT_ID('tempdb..#payment') IS NOT NULL Drop Table #payment 
IF OBJECT_ID('tempdb..#reversals') IS NOT NULL Drop Table #reversals 
IF OBJECT_ID('tempdb..#deposit') IS NOT NULL Drop Table #deposit 
declare @id int, @LocationId int, @patientid int, @date datetime, @number varchar(100), @userid int, @xml xml, @TranType int;

Create Table #Receipt  (patientId int, TransactionId int, ReceiptDate datetime, ReceiptType int, ReceiptNumber varchar(100), ReceiptData xml, userid int);

Create Table #payment (Id int, patientId int, userid int, locationid int,ReceiptDate datetime, ReceiptNumber varchar(100));

Create Table #reversals (Id int, patientId int, userid int, locationid int,ReceiptDate datetime, ReceiptNumber varchar(100));
create table #deposit  (Id int, patientId int, userid int, locationid int,ReceiptDate datetime, ReceiptNumber varchar(100), TranType int);
--declare @depositrefund table (Id int, patientId int, userid int, locationid int,ReceiptDate datetime, ReceiptNumber varchar(50));
	Declare @ErrorMessage nvarchar(4000), @ErrorSeverity int, @ErrorState int;
;
With Rec As ( Select
	transactionid,
	o.ptn_pk,
	o.userid,
	locationid,
	[TransactionDate],
	[ReceiptNumber],
	row_number() Over (Partition By ReceiptNumber Order By TransactionDate) RI
From ord_bill o
	Inner Join mst_bill b On b.billid = o.billid
)
Insert Into #payment (
	id,
	patientId,
	userid,
	locationid,
	ReceiptDate,
	ReceiptNumber)
Select	transactionid,
		ptn_pk,
		userid,
		locationid,
		transactiondate,
		Case RI
			When 1 Then receiptNumber
			Else receiptnumber +'#' + convert(varchar, RI)
		End ReceiptNumber
From rec O
Where o.ReceiptNumber Not In (Select ReceiptNumber From dtl_BillingReceipt);

Print Convert(varchar(10),@@ROWCOUNT) + ' Payments receipts to be processed'

declare @r int 
Set @r = 0;
While Exists(Select 1 From #payment) Begin
	Select Top 1 @ID= Id, @LocationId= locationid, @userid = userid, @patientid = patientId, @number = ReceiptNumber, @date= ReceiptDate from #payment;
	Set @xml =''
	exec dbo.pr_Billing_Save_PaymentReceipts 
			@TransactionID = @Id, 
			@LocationId= @LocationId, 
			@Password= @Password,
			@xml=@xml Output;
	Begin Try
		Exec dbo.pr_Billing_CreateReceipt	@PatientId = @patientId
									,	@TransactionId = @Id
									,	@ReceiptDate = @date
									,	@ReceiptType = 1
									,	@ReceiptNumber = @number
									,	@ReceiptData = @xml
									,	@UserId = @UserId
									,	@PrintCount = 1
									
	End Try
	Begin Catch
	
		Select	@ErrorMessage = error_message(),
				@ErrorSeverity = error_severity(),
				@ErrorState = error_state();
		Print @ErrorMessage + ' Severity =' + Convert(varchar,@ErrorSeverity)+ '  State = '+ Convert(varchar, @ErrorState)
		 + ' ReceiptNumber = '+ @number;
	End Catch

	Insert Into #Receipt (
		patientId,
		TransactionId,
		ReceiptDate,
		ReceiptType,
		ReceiptNumber,
		ReceiptData,		
		userid)
	Select	@patientid,
			@id,
			@date,
			1,
			@number,
			@xml,
			@userid;
	delete from #payment where id=@id
	set @r= @r+1;
End
Print Convert(varchar(10),@r) + ' Payments receipts processed'
set @r= 0;
-- reversals
Insert Into #reversals (
	id,
	patientId,
	userid,
	locationid,
	ReceiptDate,
	ReceiptNumber)
Select	o.TransactionReversalID,
		o.ptn_pk,
		o.refundby,
		locationid,
		RefundDate,
		ReversalReference
From vw_Billing_BillTransaction o
where o.Refunded=1 And o.ReceiptNumber Not In (Select ReceiptNumber From dtl_BillingReceipt);

Print Convert(varchar(10),@@ROWCOUNT) + ' reversals receipts to be processed'


While Exists(Select 1 From #reversals) Begin
	Select Top 1 @ID= Id, @LocationId= locationid, @userid = userid, @patientid = patientId, @number = ReceiptNumber, @date= ReceiptDate from #reversals;
	set @xml =''
	exec dbo.pr_Billing_Report_SaveReversalReciept 
			@TransactionID = @Id, 
			@LocationId= @LocationId, 
			@Password= @Password,
			@xml=@xml Output;


			Begin Try
		Exec dbo.pr_Billing_CreateReceipt	@PatientId = @patientId
									,	@TransactionId = @Id
									,	@ReceiptDate = @date
									,	@ReceiptType = 2
									,	@ReceiptNumber = @number
									,	@ReceiptData = @xml
									,	@UserId = @UserId
									,	@PrintCount = 1
									
	End Try
	Begin Catch
	
		Select	@ErrorMessage = error_message(),
				@ErrorSeverity = error_severity(),
				@ErrorState = error_state();
		Print @ErrorMessage + ' Severity =' + Convert(varchar,@ErrorSeverity)+ '  State = '+ Convert(varchar, @ErrorState)
		 + ' ReceiptNumber = '+ @number;
	End Catch

	Insert Into #Receipt (
		patientId,
		TransactionId,
		ReceiptDate,
		ReceiptType,
		ReceiptNumber,
		ReceiptData,
		userid)
	Select	@patientid,
			@id,
			@date,
			2,
			@number,
			@xml,
			@userid;
	delete from #reversals where id=@id
	set @r= @r+1;
End
Print Convert(varchar(10),@r) + ' reversals receipts processed'
set @r= 0;
--Deposits

Insert Into #deposit (
	id,
	patientId,
	userid,
	locationid,
	ReceiptDate,
	ReceiptNumber,
	TranType)
Select	transactionid,
		o.ptn_pk,
		o.UserID,
		locationid,
		o.TransactionDate,
		o.ReferenceNumber,
		case o.TransactionDescription when  'Deposit' Then 3 Else 4 End
From dtl_BillDepositTransaction o
where o.TransactionDescription In ('Deposit','Refund') And o.ReferenceNumber Not In (Select ReceiptNumber From dtl_BillingReceipt);

Print Convert(varchar(10),@@ROWCOUNT) + ' deposits receipts to be processed'

While Exists(Select 1 From #deposit) Begin
	Select Top 1 @ID= Id, @LocationId= locationid, @userid = userid, @patientid = patientId, @number = ReceiptNumber, @date= ReceiptDate, @TranType= TranType from #deposit;
	set @xml =''
	exec dbo.pr_Billing_Report_SaveDepositTransaction_Receipt 
			@TransactionID = @Id, 
			@LocationId= @LocationId, 
			@Password= @Password,
			@xml=@xml Output;

			Begin Try
		Exec dbo.pr_Billing_CreateReceipt	@PatientId = @patientId
									,	@TransactionId = @Id
									,	@ReceiptDate = @date
									,	@ReceiptType = @TranType
									,	@ReceiptNumber = @number
									,	@ReceiptData = @xml
									,	@UserId = @UserId
									,	@PrintCount = 1
									
	End Try
	Begin Catch
	
		Select	@ErrorMessage = error_message(),
				@ErrorSeverity = error_severity(),
				@ErrorState = error_state();
		Print @ErrorMessage + ' Severity =' + Convert(varchar,@ErrorSeverity)+ '  State = '+ Convert(varchar, @ErrorState)
		 + ' ReceiptNumber = '+ @number;
	End Catch

	Insert Into #Receipt (
		patientId,
		TransactionId,
		ReceiptDate,
		ReceiptType,
		ReceiptNumber,
		ReceiptData,
		userid)
	Select	@patientid,
			@Id,
			@date,
			@TranType,
			@number,
			@xml,
			@userid;
	delete from #deposit where id=@id
	set @r= @r+1;
End
Print Convert(varchar(10),@r) + ' deposits receipts processed'
set @r= 0;
Go

/*
Declare @SymKey varchar(800) 
Set @SymKey = 'Open symmetric key Key_CTC decryption by password='+  N'''ttwbvXWpqb5WOLfLrBgisw==''' + ''                                                              
exec(@SymKey)  

--Insert Into dtl_BillingReceipt(Ptn_pk, TransactionId, ReceiptDate,ReceiptType,ReceiptNumber, ReceiptData, UserId,PrintCount)
select patientId,
		TransactionId,
		ReceiptDate,
		ReceiptType,
		ReceiptNumber,
		convert(varbinary(max),encryptbykey(key_guid('Key_CTC'), Convert(varchar(max),ReceiptData))),
		userid,
		1
 from #Receipt 
 Close symmetric key Key_CTC   
 */   
	/*declare @patientId int,
	@TransactionId int,
	@ReceiptDate datetime,
	@ReceiptType int,
	@ReceiptNumber varchar(100),
	@ReceiptData xml,
	@UserId int,
	@PrintCount int,
	@Password varchar(100)                                                               
  While Exists(Select 1 From #Receipt) Begin
	Select Top 1	
				@patientId =	patientId
				,@TransactionId=	TransactionId
				,@ReceiptDate =	ReceiptDate
				,@ReceiptType =	ReceiptType
				,@ReceiptNumber =	ReceiptNumber
				,@ReceiptData =	 ReceiptData				
				,@UserId =	userid
				, @PrintCount=	1
	From #Receipt 
	Begin Try
	Exec dbo.pr_Billing_CreateReceipt	@PatientId = @patientId
									,	@TransactionId = @TransactionId
									,	@ReceiptDate = @ReceiptDate
									,	@ReceiptType = @ReceiptType
									,	@ReceiptNumber = @ReceiptNumber
									,	@ReceiptData = @ReceiptData
									,	@UserId = @UserId
									,	@PrintCount = @PrintCount
									,	@Password = N'''ttwbvXWpqb5WOLfLrBgisw=='''
	End Try
	Begin Catch
		Declare @ErrorMessage nvarchar(4000), @ErrorSeverity int, @ErrorState int;
		Select	@ErrorMessage = error_message(),
				@ErrorSeverity = error_severity(),
				@ErrorState = error_state();
		Print @ErrorMessage + ' Severity =' + Convert(varchar,@ErrorSeverity)+ '  State = '+ Convert(varchar, @ErrorState)
		 + ' ReceiptNumber = '+ @ReceiptNumber;
	End Catch

	delete from #Receipt where TransactionId=@TransactionId and ReceiptNumber=@ReceiptNumber
  End   
	*/                          
 Go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Billing_Report_SaveReversalReciept]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Billing_Report_SaveReversalReciept]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Billing_Report_SaveDepositTransaction_Receipt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Billing_Report_SaveDepositTransaction_Receipt]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Billing_Save_PaymentReceipts]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Billing_Save_PaymentReceipts]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dbo.pr_Billing_CreateReceipt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[dbo.pr_Billing_CreateReceipt]
GO