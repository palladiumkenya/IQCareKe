IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_SCM_SavePharmacyDispenseOrderDetail_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_SCM_SavePharmacyDispenseOrderDetail_Futures]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[pr_SCM_SavePharmacyDispenseOrderDetail_Futures]                        
	@Ptn_Pk int,                        
	@StoreId int,                        
	@VisitId int,                        
	@Ptn_Pharmacy_Pk int,                        
	@Drug_Pk int,                        
	@StrengthId int,                        
	@FrequencyId int,                        
	@DispensedQuantity int,                        
	@Prophylaxis int,                        
	@BatchId int,                        
	@CostPrice decimal(18,2),                        
	@Margin decimal(18,2),                        
	@SellingPrice decimal(18,2),                        
	@BillAmount decimal(18,2),                        
	@ExpiryDate datetime,                        
	@DispensingUnit int,                        
	@DispensedByDate datetime,                        
	@LocationId int,                        
	@UserId int,          
	@DataStatus int ,  
	@Dose decimal(18,2),  
	@Duration decimal(18,2),  
	@PrescribeOrderedQuantity decimal(18,2),  
	@PrintPrescriptionStatus int,  
	@PatientInstructions varchar(500),
	@WhyPartial varchar(255) =null,
	@BatchNo varchar(50) = null,
	@pillCount int = 0  
As Begin                        
          
	declare @EntryStatus int;
	Select @BatchId = id	From mst_batch Where name = @BatchNo And itemid = @Drug_Pk

	Set @EntryStatus = 0

	If Exists (Select Drug_Pk From dbo.Dtl_PatientPharmacyOrder	Where Drug_Pk = @Drug_Pk And DispensedQuantity = 0	And Ptn_Pharmacy_Pk = @Ptn_Pharmacy_Pk) Begin
		Set @EntryStatus = 1

		Update dbo.Dtl_PatientPharmacyOrder Set
			DispensedQuantity = @DispensedQuantity
			,pillCount = @pillCount
			, BatchNo = @BatchId
			, ExpiryDate = @ExpiryDate
			, UserId = @UserId
			, UpdateDate = GETDATE()
			, SingleDose = @Dose
			, Duration = @Duration
			, OrderedQuantity = @PrescribeOrderedQuantity
			, PrintPrescriptionStatus = @PrintPrescriptionStatus
			, PatientInstructions = @PatientInstructions
			,WhyPartial = nullif(@WhyPartial,'')
		Where Ptn_Pharmacy_pk = @Ptn_Pharmacy_Pk
		And Drug_Pk = @Drug_Pk;

		Insert Into dbo.Dtl_PatientBillTransaction (
			Ptn_Pk
			,VisitId
			,LocationId
			,TransactionDate
			,LabId
			,PharmacyId
			,ItemId
			,BatchId
			,DispensingUnit
			,Quantity
			,SellingPrice
			,CostPrice
			,Margin
			,ConsultancyFee
			,AdminFee
			,BillAmount
			,DoctorId
			,UserId
			,CreateDate)
		Values (
			@Ptn_Pk
			,@VisitId
			,@LocationId
			,@DispensedByDate
			,0
			,@Ptn_Pharmacy_pk
			,@Drug_Pk
			,@BatchId
			,@DispensingUnit
			,@DispensedQuantity
			,@SellingPrice
			,@CostPrice
			,@Margin
			,0
			,0
			,@BillAmount
			,0
			,@UserId
			,GETDATE())

		Insert Into dbo.Dtl_StockTransaction (
			ItemId
			,BatchId
			,Ptn_Pharmacy_Pk
			,PtnPk
			,StoreId
			,TransactionDate
			,Quantity
			,ExpiryDate
			,PurchasePrice
			,SellingPrice
			,Margin
			,UserId
			,CreateDate
			,transactionType)
		Values (
			@Drug_Pk
			,@BatchId
			,@Ptn_Pharmacy_Pk
			,@Ptn_Pk
			,@StoreId
			,@DispensedByDate
			,'-' + CONVERT(varchar, @DispensedQuantity)
			,@ExpiryDate
			,@CostPrice
			,@SellingPrice
			,@Margin
			,@UserId
			,GETDATE()
			,'Dispense')
	End


	If Not Exists (Select	Drug_Pk	From dbo.Dtl_PatientPharmacyOrder Where Drug_Pk = @Drug_Pk	And BatchNo = @BatchId	And ExpiryDate = @ExpiryDate And Ptn_Pharmacy_Pk = @Ptn_Pharmacy_Pk) Begin
		Set @EntryStatus = 1
	
		Insert Into dbo.Dtl_PatientPharmacyOrder (
			Ptn_Pharmacy_Pk
			,Drug_Pk
			,GenericId
			,StrengthId
			,FrequencyId
			,DispensedQuantity
			,UserId
			,CreateDate
			,Prophylaxis
			,BatchNo
			,ExpiryDate
			,SingleDose
			,Duration
			,OrderedQuantity
			,PrintPrescriptionStatus
			,WhyPartial
			,PatientInstructions
			,pillCount)
		Values (
			@Ptn_Pharmacy_Pk
			,@Drug_Pk
			,0
			,@StrengthId
			,@FrequencyId
			,@DispensedQuantity
			,@UserId
			,GETDATE()
			,@Prophylaxis
			,@BatchId
			,@ExpiryDate
			,@Dose
			,@Duration
			,@PrescribeOrderedQuantity
			,@PrintPrescriptionStatus
			,nullif(@WhyPartial,'')
			,@PatientInstructions
			,@pillCount)

		Insert Into dbo.Dtl_PatientBillTransaction (
			Ptn_Pk
			,VisitId
			,LocationId
			,TransactionDate
			,LabId
			,PharmacyId
			,ItemId
			,BatchId
			,DispensingUnit
			,Quantity
			,SellingPrice
			,CostPrice
			,Margin
			,ConsultancyFee
			,AdminFee
			,BillAmount
			,DoctorId
			,UserId
			,CreateDate)
		Values (
			@Ptn_Pk
			,@VisitId
			,@LocationId
			,@DispensedByDate
			,0
			,@Ptn_Pharmacy_pk
			,@Drug_Pk
			,@BatchId
			,@DispensingUnit
			,@DispensedQuantity
			,@SellingPrice
			,@CostPrice
			,@Margin
			,0
			,0
			,@BillAmount
			,0
			,@UserId
			,GETDATE())
		Insert Into dbo.Dtl_StockTransaction (
			ItemId
			,BatchId
			,Ptn_Pharmacy_Pk
			,PtnPk
			,StoreId
			,TransactionDate
			,Quantity
			,ExpiryDate
			,PurchasePrice
			,SellingPrice
			,Margin
			,UserId
			,CreateDate
			,transactionType)
		Values (
			@Drug_Pk
			,@BatchId
			,@Ptn_Pharmacy_Pk
			,@Ptn_Pk
			,@StoreId
			,@DispensedByDate
			,'-' + CONVERT(varchar, @DispensedQuantity)
			,@ExpiryDate
			,@CostPrice
			,@SellingPrice
			,@Margin
			,@UserId
			,GETDATE()
			,'Dispense')
	End

	If (@EntryStatus = 0 And @DataStatus = 1) Begin
		Insert Into dbo.Dtl_PatientPharmacyOrder (
			Ptn_Pharmacy_Pk
			,Drug_Pk
			,GenericId
			,StrengthId
			,FrequencyId
			,DispensedQuantity
			,UserId
			,CreateDate
			,Prophylaxis
			,BatchNo
			,ExpiryDate
			,SingleDose
			,Duration
			,OrderedQuantity
			,PrintPrescriptionStatus
			,WhyPartial
			,PatientInstructions
			,pillCount)
		Values (
			@Ptn_Pharmacy_Pk
			,@Drug_Pk
			,0
			,@StrengthId
			,@FrequencyId
			,@DispensedQuantity
			,@UserId
			,GETDATE()
			,@Prophylaxis
			,@BatchId
			,@ExpiryDate
			,@Dose
			,@Duration
			,@PrescribeOrderedQuantity
			,@PrintPrescriptionStatus
			,nullif(@WhyPartial,'')
			,@PatientInstructions
			,@pillCount);

		Declare @duraction1 decimal(18, 2), @Qty1 decimal(18, 2);

		Select Top 1	@duraction1 = Duration
						,@Qty1 = OrderedQuantity
		From dbo.Dtl_PatientPharmacyOrder
		Where Ptn_Pharmacy_Pk = @Ptn_Pharmacy_Pk
		And Drug_Pk = @Drug_Pk
		And Duration Is Not Null
		And OrderedQuantity Is Not Null;

		Update dbo.Dtl_PatientPharmacyOrder Set
			Duration = @duraction1
			, OrderedQuantity = @Qty1
		Where Ptn_Pharmacy_Pk = @Ptn_Pharmacy_Pk
		And Drug_Pk = @Drug_Pk
		And Duration Is Null
		And OrderedQuantity Is Null;

		Insert Into dbo.Dtl_PatientBillTransaction (
			Ptn_Pk
			,VisitId
			,LocationId
			,TransactionDate
			,LabId
			,PharmacyId
			,ItemId
			,BatchId
			,DispensingUnit
			,Quantity
			,SellingPrice
			,CostPrice
			,Margin
			,ConsultancyFee
			,AdminFee
			,BillAmount
			,DoctorId
			,UserId
			,CreateDate)
		Values (
			@Ptn_Pk
			,@VisitId
			,@LocationId
			,@DispensedByDate
			,0
			,@Ptn_Pharmacy_pk
			,@Drug_Pk
			,@BatchId
			,@DispensingUnit
			,@DispensedQuantity
			,@SellingPrice
			,@CostPrice
			,@Margin
			,0
			,0
			,@BillAmount
			,0
			,@UserId
			,GETDATE())

		Insert Into dbo.Dtl_StockTransaction (
			ItemId
			,BatchId
			,Ptn_Pharmacy_Pk
			,PtnPk
			,StoreId
			,TransactionDate
			,Quantity
			,ExpiryDate
			,PurchasePrice
			,SellingPrice
			,Margin
			,UserId
			,CreateDate
			,transactionType)
		Values (
			@Drug_Pk
			,@BatchId
			,@Ptn_Pharmacy_Pk
			,@Ptn_Pk
			,@StoreId
			,@DispensedByDate
			,'-' + CONVERT(varchar, @DispensedQuantity)
			,@ExpiryDate
			,@CostPrice
			,@SellingPrice
			,@Margin
			,@UserId
			,GETDATE()
			,'Dispense')
	End
	
	Declare @OrderedQuantity decimal(18, 2), @TotalDispensedQuantity decimal(18, 2), @TotalPillCount decimal(18, 2);

	Select @OrderedQuantity = orderedquantity	
	From dtl_patientpharmacyorder Where ptn_pharmacy_pk = @ptn_Pharmacy_Pk And Drug_Pk = @Drug_Pk;


	Select @TotalDispensedQuantity = SUM(DispensedQuantity)
	From dtl_patientpharmacyorder Where ptn_pharmacy_pk = @ptn_Pharmacy_Pk And Drug_Pk = @Drug_Pk;

	Select @TotalPillCount = SUM(PillCount)
	From dtl_patientpharmacyorder Where ptn_pharmacy_pk = @ptn_Pharmacy_Pk And Drug_Pk = @Drug_Pk;


	Declare @OrderedQuantity1 decimal(18, 2),@TotalDispensedQuantity1 decimal(18, 2),@TotalPillCount1 decimal(18, 2);

	Select @OrderedQuantity1 = SUM(z.OrderedQuantity)
	From (
		Select	Drug_Pk		,ISNULL(OrderedQuantity, 0) 'OrderedQuantity'
		From dtl_patientpharmacyorder
		Where ptn_pharmacy_pk = @ptn_Pharmacy_Pk
		Group By	Drug_Pk,OrderedQuantity
	) z
	Select @TotalDispensedQuantity1 = SUM(DispensedQuantity)
	From dtl_patientpharmacyorder
	Where ptn_pharmacy_pk = @ptn_Pharmacy_Pk;

	Select @TotalPillCount1 = SUM(PillCount)
	From dtl_patientpharmacyorder
	Where ptn_pharmacy_pk = @ptn_Pharmacy_Pk;

	Select @OrderedQuantity1
	Select @TotalDispensedQuantity1
	select @TotalPillCount1

	--If (@OrderedQuantity1 <= (@TotalDispensedQuantity1 + @TotalPillCount1)) Begin
		select @Ptn_Pharmacy_Pk
		Update ord_PatientPharmacyOrder		Set 
			OrderStatus = case 
				when @OrderedQuantity1 > @TotalDispensedQuantity1 + isnull(@TotalPillCount1,0) Then 3
				When  @OrderedQuantity1 <= (@TotalDispensedQuantity1 + @TotalPillCount1) Then 2 
				Else 1 End,
			DispensedByDate = Isnull(DispensedByDate,@DispensedByDate)
		Where ptn_pharmacy_pk = @ptn_pharmacy_pk and @TotalDispensedQuantity1 + isnull(@TotalPillCount1,0) > 0
		
--	End
	
	--Update ord_PatientPharmacyOrder	Set 
	--	OrderStatus = 3,
	--		DispensedByDate = Isnull(DispensedByDate,@DispensedByDate)
	--Where  ptn_pharmacy_pk = @ptn_pharmacy_pk
	--And @OrderedQuantity1 = (@TotalDispensedQuantity + @TotalPillCount)

End


GO


