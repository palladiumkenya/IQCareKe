
/****** Object:  StoredProcedure [dbo].[pr_Pharmacy_GetPatientPharmacyOrderList]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Pharmacy_GetPatientPharmacyOrderList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Pharmacy_GetPatientPharmacyOrderList]
GO
/****** Object:  StoredProcedure [dbo].[pr_Pharmacy_GetPatientRecordformStatus]    Script Date: 04/01/2015 11:33:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Pharmacy_GetPatientRecordformStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Pharmacy_GetPatientRecordformStatus]
GO
/****** Object:  StoredProcedure [dbo].[pr_Pharmacy_SaveUpdatePediatric_Constella]    Script Date: 04/01/2015 11:33:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Pharmacy_SaveUpdatePediatric_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Pharmacy_SaveUpdatePediatric_Constella]
GO
/****** Object:  StoredProcedure [dbo].[pr_Pharmacy_FindDrugByName]    Script Date: 09/04/2015 13:20:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Pharmacy_FindDrugByName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Pharmacy_FindDrugByName]
GO

/****** Object:  StoredProcedure [dbo].[pr_Pharmacy_SavePatientPediatric_Constella]    Script Date: 01/13/2016 10:14:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Pharmacy_SavePatientPediatric_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Pharmacy_SavePatientPediatric_Constella]
GO


/****** Object:  StoredProcedure [dbo].[pr_Pharmacy_GetPatientPharmacyOrderList]    Script Date: 12/11/2014 16:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Pharmacy_GetPatientPharmacyOrderList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Joseph Njung''e
-- Create date: Sep 02 2014
-- Description:	Get patient pharmacy orders List
--				@ShowMostRecent : If true, only the most recent pharmacy order is shown
--				@DrugTypeID		: If 0 show all orders , else show orders with the selected drugtype only
-- =============================================
CREATE PROCEDURE [dbo].[pr_Pharmacy_GetPatientPharmacyOrderList] 
	-- Add the parameters for the stored procedure here
	@PatientID int , 
	@CutOffDate datetime=Null,
	@ShowMostRecent bit = 1,
	@DrugTypeID int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	With PharmOrders As 
	( 
		Select	OPO.VisitID,
				V.VisitDate,
				OPO.ptn_pharmacy_pk,
				dbo.fn_GetDrugTypeId_futures(DPO.Drug_PK) DrugType,
				Row_number() Over (partition by dbo.fn_GetDrugTypeId_futures(DPO.Drug_PK)  Order By OPO.OrderedByDate Desc) OrderIndex
		From dbo.ord_PatientPharmacyOrder OPO
		Inner Join
			dbo.dtl_PatientPharmacyOrder DPO On OPO.ptn_pharmacy_pk = DPO.ptn_pharmacy_pk
		Inner Join
			ord_Visit V On V.Visit_Id = OPO.VisitID
		Where V.Ptn_Pk = @PatientID
		And
			Case
				When @DrugTypeID = 0 Then 1
				When dbo.fn_GetDrugTypeId_futures(DPO.Drug_Pk) = @DrugTypeID Then 1
				Else 0 End = 1
		And(OPO.OrderedByDate <= @CutOffDate Or @CutOffDate Is Null)
		And (OPO.DeleteFlag = 0	Or OPO.DeleteFlag Is Null)	
	)
	Select *
	From PharmOrders
	Where Case
		When @ShowMostRecent = 1	And OrderIndex = 1 Then 1
		When @ShowMostRecent = 1	And OrderIndex > 1 Then 0
		Else 1 End = 1;
END

' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Pharmacy_GetPatientRecordformStatus]    Script Date: 12/11/2014 16:16:40 ******/
/****** Object:  StoredProcedure [dbo].[pr_Pharmacy_GetPatientRecordformStatus]    Script Date: 04/01/2015 11:33:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Pharmacy_GetPatientRecordformStatus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE procedure [dbo].[pr_Pharmacy_GetPatientRecordformStatus]                        
(@Ptn_pk int 
)                       
AS
Begin
--Table 0                     
	Select	Visit_Id,
			Ptn_Pk,
			LocationID,
			VisitDate,
			VisitType,
			DataQuality,
			DeleteFlag,
			UserID,
			CreateDate,
			UpdateDate,
			TypeofVisit,
			OrderedBy,
			OrderedDate,
			ReportedBy,
			ReportedDate,
			Signature
	From dbo.ord_Visit
	Where (VisitType In (7, 8))
	And (DeleteFlag = 0)
	And (Ptn_Pk = @Ptn_pk)
	Or (VisitType In (7, 8))
	And (DeleteFlag Is Null)
	And (Ptn_Pk = @Ptn_pk);
--Table 1                         
	Select a.ARVStatus
	From dtl_PatientARVInfo a
	Inner Join
		ord_visit v On a.Visitid = v.Visit_id
	Where a.ptn_pk = @Ptn_pk
	And (v.deleteflag = 0
	Or v.deleteflag Is Null)
	Order By v.VisitDate Desc;
--Table 2                       
	Select Top 1	ARTended,
					ARTenddate
	From dtl_PatientCareEnded
	Where ptn_pk = @Ptn_pk
	Order By ARTenddate Desc
--Table 3                   
	Select dbo.fn_GetPatientProgramStatus_Constella(@Ptn_pk) [status];
        
--Table 4               
	Select	V.Visit_Id,
			V.Ptn_Pk,
			V.LocationID,
			V.VisitDate,
			V.VisitType,
			V.DataQuality,
			V.DeleteFlag,
			V.UserID,
			V.CreateDate,
			V.UpdateDate,
			V.TypeofVisit,
			V.OrderedBy,
			V.OrderedDate,
			V.ReportedBy,
			V.ReportedDate,
			V.Signature,
			R.RegimenMap_Pk,
		--	R.Ptn_Pk As Expr1,
			--R.LocationID As Expr2,
			--R.Visit_Pk,
		--	R.Drug_Pk,
			R.RegimenType,
			R.OrderID,
			--R.DeleteFlag As Expr3,
			--R.UserID As Expr4,
			--R.CreateDate As Expr5,
			--R.UpdateDate As Expr6,
			R.RegimenId
	From ord_Visit As V
	Inner Join
		dtl_RegimenMap As R On V.Visit_Id = R.Visit_Pk
	Where (V.VisitType In (4))
	And (V.DeleteFlag = 0 OR V.DeleteFlag Is Null)
	And (R.DeleteFlag = 0 OR R.DeleteFlag Is Null)
	And (V.Ptn_Pk = @Ptn_pk);
	
--Table 5               
	Select	t.therapyplan,
			v.visitdate
	From dtl_PatientArvTherapy t
	Inner Join
		ord_visit v On t.Visit_pk = v.Visit_id
	Where t.ptn_pk = @Ptn_pk
	And (v.DeleteFlag = 0	Or v.DeleteFlag Is Null)
	Order By v.VisitDate Desc;
--Table 6               
	Select Top 1 dbo.fn_formatregimen(RegimenType) [RegimenType]
	From dtl_RegimenMap
	Where ptn_pk = @Ptn_pk
	And (DeleteFlag = 0	Or DeleteFlag Is Null)
	And Nullif(ltrim(rtrim(RegimenType)),'''') Is Not Null
	Order By regimenmap_pk Desc;

	--Table 7       
	Select dbo.fn_GetPatientPMTCTProgramStatus_Futures(@Ptn_pk) [status];
End
' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Pharmacy_SaveUpdatePediatric_Constella]    Script Date: 04/01/2015 11:33:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_Pharmacy_SaveUpdatePediatric_Constella]                                                
(                                                
 @Ptn_pk int,                                                
 @LocationID int,                                                
 @OrderedBy int,                                                
 @OrderedByDate datetime,                                                
 @VisitType int,                                                
 @UserID int,                                                 
 @RegimenType varchar(50),                                                
 @DispensedBy int=null,                                                
 @DispensedByDate datetime=null,                      
 @ReportedByDate datetime=null,                                              
 @OrderType int,                                                
 @Signature int,                                                
 @EmployeeID int,                                                
 @Height decimal(8,1),                                                
 @Weight numeric(8,1),                                                 
 @FDC int,                                        
 @ProgID int,                                  
 @ProviderID int,                      
 @PeriodTaken int,                    
 @ptn_pharmacy_pk int,                    
 @flag int=null,                
 @RegimenLine int,                
 @PharmacyNotes varchar(200),              
 @AppntDate datetime=null,              
 @AppntReason int  ,
 @ModuleID int = null                   
)                                                
                                                
As       
Begin               
	Declare @ptn_pharmacy int,@Visit_Pk int ,@RegimenId int,@ARTStartDate datetime     ;

	Select @RegimenType = Nullif(Ltrim(Rtrim(@RegimenType)), '');

	If (@flag = 1) Begin                                                
		Insert Into ord_Visit (
			Ptn_pk,
			LocationID,
			VisitDate,
			VisitType,
			UserID,
			Createdate,
			DataQuality,
			ModuleID)
		Values (
			@Ptn_pk, 
			@LocationID, 
			@OrderedByDate, 
			@VisitType, 
			@UserID, 
			Getdate(), 
			1,
			@ModuleID);		
		Set @Visit_Pk =SCOPE_IDENTITY();

		Insert Into dbo.ord_PatientPharmacyOrder (
			Ptn_pk,
			VisitID,
			LocationID,
			OrderedBy,
			OrderedByDate,
			DispensedBy,
			DispensedByDate,
			OrderType,
			Height,
			Weight,
			FDC,
			ProgID,
			Signature,
			EmployeeID,
			UserID,
			CreateDate,
			ProviderID,
			PharmacyPeriodTaken,
			Regimenline,
			PharmacyNotes)
		Values (
			@Ptn_pk, 
			@Visit_Pk, 
			@LocationID, 
			@OrderedBy, 
			@OrderedByDate, 
			@DispensedBy, 
			@DispensedByDate, 
			@OrderType, 
			@Height, 
			@Weight, 
			@FDC, 
			@ProgID, 
			@Signature, 
			@EmployeeID, 
			@UserID, 
			Getdate(), 
			@ProviderID, 
			@PeriodTaken, 
			@RegimenLine, 
			@PharmacyNotes);
		Set @ptn_pharmacy =SCOPE_IDENTITY();
		If Not Exists (Select	1
			From [dtl_PatientVitals]
			Where Visit_pk = @Visit_Pk
			And ptn_pk = @Ptn_pk) Begin
			Insert Into [dtl_PatientVitals] (
				[Ptn_pk],
				[LocationID],
				[Visit_pk],
				[Height],
				[Weight],
				[UserID],
				[CreateDate])
			Values (
				@Ptn_pk, 
				@LocationID, 
				@Visit_Pk, 
				@Height, 
				@Weight, 
				@UserID, 
				Getdate());
		End
		Update ord_PatientPharmacyOrder Set
			ReportingID = (Select Right('000000' + Convert(varchar, @ptn_pharmacy), 6))
		Where ptn_pharmacy_pk = @ptn_pharmacy;

		If (@DispensedByDate Is Not Null And @DispensedBy > 0) Begin
			Update ord_PatientPharmacyOrder Set
				OrderStatus = 2
			Where ptn_pharmacy_pk = @ptn_pharmacy;
		End

		If (@AppntDate Is Not Null Or Year(@AppntDate) != '1900') Begin
			Insert Into dtl_patientappointment (
				Ptn_pk,
				Visit_pk,
				LocationID,
				AppDate,
				AppReason,
				AppStatus,
				EmployeeId,
				DeleteFlag,
				UserId,
				CreateDate)
			Values (
				@Ptn_pk, 
				@Visit_Pk, 
				@LocationID, 
				@AppntDate, 
				@AppntReason, 
				12, 
				@EmployeeID, 
				0, 
				@UserID, 
				Getdate());
		End

		
		If(@RegimenType Is Not Null) Begin	
			Insert Into dtl_RegimenMap (
				Ptn_Pk,
				LocationID,
				Visit_Pk,
				RegimenType,
				OrderId,
				UserID,
				CreateDate)
			Values (
				@Ptn_pk, 
				@LocationID, 
				@Visit_Pk, 
				@RegimenType, 
				@ptn_pharmacy, 
				@UserID, 
				Getdate());
		End
		Select @ARTStartDate = dbo.fn_GetPatientARTStartDate_constella(@Ptn_pk);
		Update mst_Patient Set
			ARTStartDate = @ARTStartDate
		Where ptn_pk = @Ptn_pk;

		Select @ptn_pharmacy;
	End
	
	If (@flag = 2) Begin
		Update [ord_PatientPharmacyOrder] Set
			[OrderedBy] = @OrderedBy,
			[DispensedBy] = @DispensedBy,
			[Signature] = @Signature,
			[EmployeeID] = @EmployeeID,
			[ProgID] = @ProgID,
			[Height] = @Height,
			[Weight] = @Weight,
			[UpdateDate] = Getdate(),
			[ProviderID] = @ProviderID,
			[OrderedByDate] = @OrderedByDate,
			[DispensedByDate] = @ReportedByDate,
			UserID = @UserID,
			PharmacyPeriodTaken = @PeriodTaken,
			Regimenline = @Regimenline,
			PharmacyNotes = @PharmacyNotes
		Where ([ptn_pharmacy_pk] = @ptn_pharmacy_pk);

		Declare @VID int;
		Select @VID = VisitID
		From ord_PatientPharmacyOrder
		Where ptn_pharmacy_pk = @ptn_pharmacy_pk;

		Update ord_Visit Set
			VisitDate = @OrderedByDate
		Where Visit_Id = @VID;

		If Not Exists (Select	1
			From [dtl_PatientVitals]
			Where Visit_pk = @VID
			And ptn_pk = @Ptn_pk) Begin
			Insert Into [dtl_PatientVitals] (
				[Ptn_pk],
				[LocationID],
				[Visit_pk],
				[Height],
				[Weight],
				[UserID],
				[CreateDate])
			Values (
				@Ptn_pk, 
				@LocationID,
				@Visit_Pk, 
				@Height, 
				@Weight, 
				@UserID, 
				Getdate());
		End 
		Else Begin
			Update dtl_PatientVitals Set
				[Height] = @Height,
				[Weight] = @Weight,
				UpdateDate = Getdate()
			Where ptn_pk = @Ptn_pk
			And Visit_pk = @VID
			And LocationId = @LocationID;
		End

		If (@AppntDate Is Not Null Or Year(@AppntDate) != '1900') Begin
			If Exists (Select *
				From dtl_PatientAppointment
				Where Ptn_pk = @Ptn_pk
				And Visit_pk = @VID) Begin
				Update dtl_PatientAppointment Set
					[AppDate] = @AppntDate,
					[AppReason] = @AppntReason,
					[EmployeeID] = @EmployeeID,
					[UserID] = @UserID,
					[UpdateDate] = Getdate()
				Where Ptn_pk = @Ptn_pk
				And Visit_pk = @VID;
			End 
			
			Else Begin
				Insert Into dtl_patientappointment (
					Ptn_pk,
					Visit_pk,
					LocationID,
					AppDate,
					AppReason,
					AppStatus,
					EmployeeId,
					DeleteFlag,
					UserId,
					CreateDate)
				Values (
					@Ptn_pk, 
					@VID, 
					@LocationID, 
					@AppntDate, 
					@AppntReason, 
					12,
					@EmployeeID, 
					0, 
					@UserID, 
					Getdate());
			End
			If (@ReportedByDate Is Not Null And @DispensedBy > 0) Begin
				Update ord_PatientPharmacyOrder Set
					OrderStatus = 2
				Where ptn_pharmacy_pk = @ptn_pharmacy_pk;
			End

		End
		
		If(@RegimenType Is Not Null) Begin
		
			Select @RegimenId = RegimenMap_Pk
			From dtl_regimenmap a, ord_patientpharmacyorder b
			Where a.ptn_pk = b.ptn_pk
			And b.ptn_pharmacy_pk = a.orderID
			And b.Ptn_Pharmacy_Pk = @Ptn_Pharmacy_Pk;

			Update [dtl_RegimenMap] Set
				[RegimenType] = @RegimenType,
				[UpdateDate] = Getdate()
			Where ([RegimenMap_Pk] = @RegimenId);
		End

		Select @Ptn_PK = ptn_pk
		From ord_PatientPharmacyOrder
		Where ptn_pharmacy_pk = @ptn_pharmacy_pk;
		
		Select @ARTStartDate = dbo.fn_GetPatientARTStartDate_constella(@Ptn_PK);
		
		Update mst_Patient Set
			ARTStartDate = @ARTStartDate
		Where ptn_pk = @Ptn_pk;

		--If Exists (Select *
		--	From ord_PatientPharmacyOrder
		--	Where orderstatus = 1
		--	And Ptn_Pk = @Ptn_Pk
		--	And ptn_pharmacy_pk = @ptn_pharmacy_pk) Begin
		--	Delete From dtl_PatientPharmacyOrder
		--	Where ptn_pharmacy_pk = @ptn_pharmacy_pk
		--End

		Select @ptn_pharmacy_pk;

	End
End

GO




/****** Object:  StoredProcedure [dbo].[pr_Pharmacy_FindDrugByName]    Script Date: 09/04/2015 13:20:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Pharmacy_FindDrugByName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Joseph Njung''e
-- Create date: July 21 2015
-- Description:	Search Drugs. If PM?SCM is on, show available quatity in the dispensing stores only
-- =============================================
CREATE PROCEDURE [dbo].[pr_Pharmacy_FindDrugByName] 
	-- Add the parameters for the stored procedure here
	@SearchText varchar(50) = null, 
	@CheckQuantity bit = 0,
	@ExcludeDrugType int = Null
AS
Begin
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	If Ltrim(Rtrim(@SearchText)) <> '''' Select @SearchText = ''%'' + @SearchText + ''%'';
	
	If(@ChecKQuantity = 1) Begin
		Select	D.Drug_pk,
			Convert(varchar(100), D.DrugName) As Drugname,
			Isnull(Convert(varchar, Sum(ST.Quantity)), 0) As QTY
		From Dtl_StockTransaction As ST
		Inner Join
			Mst_Store As S On S.Id = ST.StoreId		And S.DispensingStore = 1
		Right Outer Join
			Mst_Drug As D On D.Drug_pk = ST.ItemId 
		Where (D.DrugName Like @SearchText)
		And D.DeleteFlag = 0
		And Case When @ExcludeDrugType Is Null Then 1
				 When dbo.fn_GetDrugTypeId_futures (D.Drug_pk) = @ExcludeDrugType Then 0
				 Else 1
			End = 1
		Group By	D.Drug_pk,	D.DrugName;		
	End
	Else Begin
		Select Drug_pk,
			DrugName 
		From Mst_Drug D 
		Where DeleteFlag=0 And 
		DrugName LIKE @SearchText
		And Case When @ExcludeDrugType Is Null Then 1
				 When dbo.fn_GetDrugTypeId_futures (D.Drug_pk) = @ExcludeDrugType Then 0
				 Else 1
			End = 1
	End
End

' 
END
GO

/****** Object:  StoredProcedure [dbo].[pr_Pharmacy_SaveUpdatePediatric_Constella]    Script Date: 09/10/2015 13:07:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[pr_Pharmacy_SaveUpdatePediatric_Constella]                                                
(                                                
 @Ptn_pk int,                                                
 @LocationID int,                                                
 @OrderedBy int,                                                
 @OrderedByDate datetime,                                                
 @VisitType int,                                                
 @UserID int,                                                 
 @RegimenType varchar(50),                                                
 @DispensedBy int=null,                                                
 @DispensedByDate datetime=null,                      
 @ReportedByDate datetime=null,                                              
 @OrderType int,                                                
 @Signature int,                                                
 @EmployeeID int,                                                
 @Height decimal(8,1),                                                
 @Weight numeric(8,1),                                                 
 @FDC int,                                        
 @ProgID int,                                  
 @ProviderID int,                      
 @PeriodTaken int,                    
 @ptn_pharmacy_pk int,                    
 @flag int=null,                
 @RegimenLine int,                
 @PharmacyNotes varchar(200),              
 @AppntDate datetime=null,              
 @AppntReason int  ,
 @ModuleID int = null                   
)                                                
                                                
As       
Begin               
	Declare @ptn_pharmacy int,@Visit_Pk int ,@RegimenId int,@ARTStartDate datetime     ;

	Select @RegimenType = Nullif(Ltrim(Rtrim(@RegimenType)), '');

	If (@flag = 1) Begin                                                
		Insert Into ord_Visit (
			Ptn_pk,
			LocationID,
			VisitDate,
			VisitType,
			UserID,
			Createdate,
			DataQuality,
			ModuleID)
		Values (
			@Ptn_pk, 
			@LocationID, 
			@OrderedByDate, 
			@VisitType, 
			@UserID, 
			Getdate(), 
			1,
			@ModuleID);		
		Set @Visit_Pk =SCOPE_IDENTITY();

		Insert Into dbo.ord_PatientPharmacyOrder (
			Ptn_pk,
			VisitID,
			LocationID,
			OrderedBy,
			OrderedByDate,
			DispensedBy,
			DispensedByDate,
			OrderType,
			Height,
			Weight,
			FDC,
			ProgID,
			Signature,
			EmployeeID,
			UserID,
			CreateDate,
			ProviderID,
			PharmacyPeriodTaken,
			Regimenline,
			PharmacyNotes)
		Values (
			@Ptn_pk, 
			@Visit_Pk, 
			@LocationID, 
			@OrderedBy, 
			@OrderedByDate, 
			@DispensedBy, 
			@DispensedByDate, 
			@OrderType, 
			@Height, 
			@Weight, 
			@FDC, 
			@ProgID, 
			@Signature, 
			@EmployeeID, 
			@UserID, 
			Getdate(), 
			@ProviderID, 
			@PeriodTaken, 
			@RegimenLine, 
			@PharmacyNotes);
		Set @ptn_pharmacy =SCOPE_IDENTITY();
		If Not Exists (Select	1
			From [dtl_PatientVitals]
			Where Visit_pk = @Visit_Pk
			And ptn_pk = @Ptn_pk) Begin
			Insert Into [dtl_PatientVitals] (
				[Ptn_pk],
				[LocationID],
				[Visit_pk],
				[Height],
				[Weight],
				[UserID],
				[CreateDate])
			Values (
				@Ptn_pk, 
				@LocationID, 
				@Visit_Pk, 
				@Height, 
				@Weight, 
				@UserID, 
				Getdate());
		End
		Update ord_PatientPharmacyOrder Set
			ReportingID = (Select Right('000000' + Convert(varchar, @ptn_pharmacy), 6))
		Where ptn_pharmacy_pk = @ptn_pharmacy;

		If (@DispensedByDate Is Not Null And @DispensedBy > 0) Begin
			Update ord_PatientPharmacyOrder Set
				OrderStatus = 2
			Where ptn_pharmacy_pk = @ptn_pharmacy;
		End

		If (@AppntDate Is Not Null Or Year(@AppntDate) != '1900') Begin
			Insert Into dtl_patientappointment (
				Ptn_pk,
				Visit_pk,
				LocationID,
				AppDate,
				AppReason,
				AppStatus,
				EmployeeId,
				DeleteFlag,
				UserId,
				CreateDate)
			Values (
				@Ptn_pk, 
				@Visit_Pk, 
				@LocationID, 
				@AppntDate, 
				@AppntReason, 
				12, 
				@EmployeeID, 
				0, 
				@UserID, 
				Getdate());
		End

		
		If(@RegimenType Is Not Null) Begin	
			Insert Into dtl_RegimenMap (
				Ptn_Pk,
				LocationID,
				Visit_Pk,
				RegimenType,
				OrderId,
				UserID,
				CreateDate)
			Values (
				@Ptn_pk, 
				@LocationID, 
				@Visit_Pk, 
				@RegimenType, 
				@ptn_pharmacy, 
				@UserID, 
				Getdate());
		End
		Select @ARTStartDate = dbo.fn_GetPatientARTStartDate_constella(@Ptn_pk);
		Update mst_Patient Set
			ARTStartDate = @ARTStartDate
		Where ptn_pk = @Ptn_pk;

		Select @ptn_pharmacy;
	End
	
	If (@flag = 2) Begin
		Update [ord_PatientPharmacyOrder] Set
			[OrderedBy] = @OrderedBy,
			[DispensedBy] = @DispensedBy,
			[Signature] = @Signature,
			[EmployeeID] = @EmployeeID,
			[ProgID] = @ProgID,
			[Height] = @Height,
			[Weight] = @Weight,
			[UpdateDate] = Getdate(),
			[ProviderID] = @ProviderID,
			[OrderedByDate] = @OrderedByDate,
			[DispensedByDate] = @ReportedByDate,
			UserID = @UserID,
			PharmacyPeriodTaken = @PeriodTaken,
			Regimenline = @Regimenline,
			PharmacyNotes = @PharmacyNotes
		Where ([ptn_pharmacy_pk] = @ptn_pharmacy_pk);

		Declare @VID int;
		Select @VID = VisitID
		From ord_PatientPharmacyOrder
		Where ptn_pharmacy_pk = @ptn_pharmacy_pk;

		Update ord_Visit Set
			VisitDate = @OrderedByDate
		Where Visit_Id = @VID;

		If Not Exists (Select	1
			From [dtl_PatientVitals]
			Where Visit_pk = @VID
			And ptn_pk = @Ptn_pk) Begin
			Insert Into [dtl_PatientVitals] (
				[Ptn_pk],
				[LocationID],
				[Visit_pk],
				[Height],
				[Weight],
				[UserID],
				[CreateDate])
			Values (
				@Ptn_pk, 
				@LocationID,
				@Visit_Pk, 
				@Height, 
				@Weight, 
				@UserID, 
				Getdate());
		End 
		Else Begin
			Update dtl_PatientVitals Set
				[Height] = @Height,
				[Weight] = @Weight,
				UpdateDate = Getdate()
			Where ptn_pk = @Ptn_pk
			And Visit_pk = @VID
			And LocationId = @LocationID;
		End

		If (@AppntDate Is Not Null Or Year(@AppntDate) != '1900') Begin
			If Exists (Select *
				From dtl_PatientAppointment
				Where Ptn_pk = @Ptn_pk
				And Visit_pk = @VID) Begin
				Update dtl_PatientAppointment Set
					[AppDate] = @AppntDate,
					[AppReason] = @AppntReason,
					[EmployeeID] = @EmployeeID,
					[UserID] = @UserID,
					[UpdateDate] = Getdate()
				Where Ptn_pk = @Ptn_pk
				And Visit_pk = @VID;
			End 
			
			Else Begin
				Insert Into dtl_patientappointment (
					Ptn_pk,
					Visit_pk,
					LocationID,
					AppDate,
					AppReason,
					AppStatus,
					EmployeeId,
					DeleteFlag,
					UserId,
					CreateDate)
				Values (
					@Ptn_pk, 
					@VID, 
					@LocationID, 
					@AppntDate, 
					@AppntReason, 
					12,
					@EmployeeID, 
					0, 
					@UserID, 
					Getdate());
			End
			If (@ReportedByDate Is Not Null And @DispensedBy > 0) Begin
				Update ord_PatientPharmacyOrder Set
					OrderStatus = 2
				Where ptn_pharmacy_pk = @ptn_pharmacy_pk;
			End

		End
		
		If(@RegimenType Is Not Null) Begin
		
			Select @RegimenId = RegimenMap_Pk
			From dtl_regimenmap a, ord_patientpharmacyorder b
			Where a.ptn_pk = b.ptn_pk
			And b.ptn_pharmacy_pk = a.orderID
			And b.Ptn_Pharmacy_Pk = @Ptn_Pharmacy_Pk;

			Update [dtl_RegimenMap] Set
				[RegimenType] = @RegimenType,
				[UpdateDate] = Getdate()
			Where ([RegimenMap_Pk] = @RegimenId);
		End

		Select @Ptn_PK = ptn_pk
		From ord_PatientPharmacyOrder
		Where ptn_pharmacy_pk = @ptn_pharmacy_pk;
		
		Select @ARTStartDate = dbo.fn_GetPatientARTStartDate_constella(@Ptn_PK);
		
		Update mst_Patient Set
			ARTStartDate = @ARTStartDate
		Where ptn_pk = @Ptn_pk;

		If Exists (Select *
			From ord_PatientPharmacyOrder
			Where orderstatus = 1
			And Ptn_Pk = @Ptn_Pk
			And ptn_pharmacy_pk = @ptn_pharmacy_pk) Begin
			Delete From dtl_PatientPharmacyOrder
			Where ptn_pharmacy_pk = @ptn_pharmacy_pk
		End

		Select @ptn_pharmacy_pk;

	End
End
Go
/*
Correct the bug where ptn_pharmacy_pk was hardcoded to 89

*/

/****** Object:  StoredProcedure [dbo].[pr_Pharmacy_SavePatientPediatric_Constella]    Script Date: 03/11/2015 12:54:56 ******/
Set Ansi_nulls On
Go

Set Quoted_identifier On
Go
Create PROCEDURE [dbo].[pr_Pharmacy_SavePatientPediatric_Constella]                                    
	@ptn_pharmacy_pk int,
	@Drug_Pk int,
	@GenericId int,
	@Dose decimal(18,2)=0,
	@SingleDose decimal(18,2)=0,
	@UnitId int,
	@StrengthID int,                                    
	@FrequencyID int,
	@Duration decimal(18,2),
	@OrderedQuantity decimal(18,2),
	@DispensedQuantity decimal(18,2),
	@Finance int,
	@UserID int,
	@TotDailyDose decimal(18,2)=0,                  
	@TBRegimenID int=null,
	@TreatmentPhase varchar(50)=null,
	@TrMonth int=null,
	@Prophylaxis int=null, 
	@DrugSchedule int=null,
	@PrintPrescriptionStatus int,
	@PatientInstructions varchar(500),
	@flag int=null,
	@SCMflag int                                        
                                    
AS                                        
Begin
	declare @DrugType int, @DrugCount int, @TotalOrderedQuantity int,@TotalDispensedQuantity int; 
	Select @TotalOrderedQuantity = 0, @TotalDispensedQuantity = 0, @DrugCount = 0;
	If @GenericId = 0 Begin       
		Select @DrugType = dbo.fn_GetDrugTypeId_futures(@Drug_Pk);
	End 
	Else Begin
		Select @DrugType = DrugTypeId
		From lnk_drugtypegeneric
		Where genericid = @GenericId
		And deleteflag = 0;
	End               
	If (@DrugType != '' Or @DrugType != Null) Begin
		Select 
			@DrugCount = Count(Drug_Pk)	
		From dtl_PatientPharmacyOrder	
		Where ptn_pharmacy_pk = @ptn_pharmacy_pk	
		And Drug_Pk = @Drug_Pk;
		If (@DrugCount > 0 And @flag = 2) Begin
			If (@SCMflag = 1) Begin
				Update dtl_PatientPharmacyOrder Set
					FrequencyID = @FrequencyID,
					Duration = @Duration,
					OrderedQuantity = @OrderedQuantity,
					SingleDose = @SingleDose,
					Financed = @Finance,
					UserID = @UserID,
					UpdateDate = Getdate(),
					TreatmentPhase = @TreatmentPhase,
					Month = @TrMonth,
					Prophylaxis = @Prophylaxis,
					PrintPrescriptionStatus = @PrintPrescriptionStatus,
					PatientInstructions = @PatientInstructions
				Where ptn_pharmacy_pk = @ptn_pharmacy_pk
				And Drug_Pk = @Drug_Pk;
			End 
			Else Begin
				Update dtl_PatientPharmacyOrder Set
					FrequencyID = @FrequencyID,
					Duration = @Duration,
					OrderedQuantity = @OrderedQuantity,
					DispensedQuantity = @DispensedQuantity,
					Financed = @Finance,
					UserID = @UserID,
					UpdateDate = Getdate(),
					TreatmentPhase = @TreatmentPhase,
					Month = @TrMonth,
					Prophylaxis = @Prophylaxis,
					SingleDose = @SingleDose,
					PrintPrescriptionStatus = @PrintPrescriptionStatus,
					PatientInstructions = @PatientInstructions
				Where ptn_pharmacy_pk = @ptn_pharmacy_pk
				And Drug_Pk = @Drug_Pk;
			End
		End 
		Else Begin
			Insert Into dtl_PatientPharmacyOrder (
				ptn_pharmacy_pk,
				Drug_Pk,
				GenericID,
				StrengthID,
				FrequencyID,
				Duration,
				OrderedQuantity,
				DispensedQuantity,
				Financed,
				UserID,
				CreateDate,
				TreatmentPhase,
				Month,
				Prophylaxis,
				SingleDose,
				PrintPrescriptionStatus,
				PatientInstructions,
				ScheduleId)
			Values (
				@ptn_pharmacy_pk,
				@Drug_Pk,
				@GenericId,
				@StrengthID,
				@FrequencyID,
				@Duration,
				@OrderedQuantity,
				@DispensedQuantity,
				@Finance,
				@UserID,
				Getdate(),
				@TreatmentPhase,
				@TrMonth,
				@Prophylaxis,
				@SingleDose,
				@PrintPrescriptionStatus,
				@PatientInstructions,
				@DrugSchedule);
		End
	End

	--Update ord_PatientPharmacyOrder Set
	--	OrderStatus = 1
	--Where DispensedByDate Is Null
	--And ptn_pharmacy_pk = @ptn_pharmacy_pk;

   
	Select	@TotalOrderedQuantity = Sum(OrderedQuantity),
			@TotalDispensedQuantity = Sum(DispensedQuantity)
	From dtl_PatientPharmacyOrder
	Where ptn_pharmacy_pk =@ptn_pharmacy_pk;-- 89;

	Update ord_PatientPharmacyOrder Set
		OrderStatus = Case 
						When DispensedByDate Is Null Then 1
						When DispensedByDate Is Not Null  And @TotalDispensedQuantity = @TotalOrderedQuantity Then 2
						When DispensedByDate Is Not Null  And @TotalDispensedQuantity < @TotalOrderedQuantity Then 3
						Else orderstatus End
	Where ptn_pharmacy_pk = @ptn_pharmacy_pk	;

                                     
End
Go

/****** Object:  StoredProcedure [dbo].[pr_Pharmacy_GetExistPaediatricDetails_Constella]    Script Date: 12/02/2015 13:50:55 ******/
Set Ansi_nulls On
Go

Set Quoted_identifier On
Go
ALTER PROCEDURE [dbo].[pr_Pharmacy_GetExistPaediatricDetails_Constella]                                    
@PharmacyID int                                      
as                                      
begin


Select	a.ptn_pharmacy_pk,
		a.RegimenLine,
		b.drug_pk,
		0 [GenericID],
		0 [Dose],
		0 [UnitId],
		b.StrengthId,
		b.FrequencyID,
		b.Duration,
		'' [DrugSchedule],
		[OrderedQuantity],--sum(b.OrderedQuantity) Over (Partition By b.drug_pk) [OrderedQuantity],
		[DispensedQuantity],--sum(b.DispensedQuantity) Over (Partition By b.drug_pk) [DispensedQuantity],
		b.Prophylaxis,
		isnull(b.PrintPrescriptionStatus, 0) [PrintPrescriptionStatus],
		b.PatientInstructions,
		0 [TB_RegimenID],
		b.TreatmentPhase,
		b.Month,
		'' [Financed],
		a.OrderType,
		a.CreateDate,
		a.Ptn_pk,
		a.LocationID,
		a.OrderedBy,
		a.OrderedByDate,
		a.DispensedBy,
		a.DispensedByDate,
		a.Signature,
		a.EmployeeID,
		0 [HoldMedicine],
		a.ProgID,
		a.ProviderID,
		a.pharmacyperiodtaken,
		a.PharmacyNotes,
		a.Height,
		a.Weight,
		b.SingleDose,
		b.ScheduleId
From ord_patientpharmacyorder a
	Join dtl_patientpharmacyorder b On a.ptn_pharmacy_pk = b.ptn_pharmacy_pk
Where a.ptn_pharmacy_pk = @PharmacyID

Select	AppDate,
		AppReason
From dtl_PatientAppointment a
	Join ord_visit b On a.Visit_pk = b.Visit_id
	Join ord_patientPharmacyorder c On b.Visit_Id = c.VisitId
Where c.ptn_pharmacy_pk = @PharmacyID
	And (a.DeleteFlag = 0 Or a.DeleteFlag Is Null)

Select	convert(varchar(11), isnull(a.DispensedByDate, a.OrderedByDate), 113) [TransactionDate],
		Case
			When a.OrderStatus = 1 Then 'New Order'
			When a.OrderStatus = 3 Then 'Partial Dispense'
			Else 'Already Dispensed Order'
		End [Status],
		a.Ptn_Pharmacy_Pk,
		a.OrderStatus
From dbo.ord_PatientPharmacyOrder a
Where a.ptn_pharmacy_pk = @PharmacyID

End
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Pharmacy_GetPediatricDetails_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Pharmacy_GetPediatricDetails_Constella]
GO


/****** Object:  StoredProcedure [dbo].[pr_Pharmacy_GetPediatricDetails_Constella]    Script Date: 12/02/2015 15:50:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[pr_Pharmacy_GetPediatricDetails_Constella]                                                                                                    
@PatientID int,                                                              
@password varchar(50)                                                                                                      
As                                                                                                    
Begin                                                                                                    
Declare @SymKey varchar(400)                                                                          
Set @SymKey = 'Open symmetric key Key_CTC decryption by password='+ @password + ''                                                                              
exec(@SymKey)    

Declare @Drug Table(Drug_Pk int, DrugName varchar(250), GenericAbbreviation varchar(100), GenericCommaSeparated varchar(100),DrugTypeId int);
Insert Into @Drug(Drug_Pk,DrugName,GenericAbbreviation,GenericCommaSeparated,DrugTypeId)
Select	a.drug_pk,
		a.drugname [FixedDrug],
		dbo.fn_Drug_Abbrev_Constella(a.drug_pk) [GenericAbbrevation],
		dbo.fn_GetDrugGenericCommaSeprated(a.drug_pk) GenericCommaSeparated,
		dbo.fn_GetDrugTypeId_futures(a.drug_pk) [drugtypeid]
From mst_drug a
Where a.deleteflag = 0
Order By a.drugname   ;                                                                     
                                                              
--0                                                                                                   
Select	m.drug_pk,
		m.drugname,
		drugtypeid,
		m.GenericAbbreviation [GenericAbbrevation],
		'0' [genericid],
		m.GenericCommaSeparated [genericname]
From @Drug m      where 1 > 1                                                                                           
--1                                                                                                   
               
select distinct d.Drug_pk[GenericId],b.StrengthId,b.StrengthName                                                                                                                       
from lnk_drugstrength a,mst_strength b,@Drug d                                                                                                                                              
where a.strengthid = b.strengthid and d.Drug_pk=a.DrugID                                                                                                                      
order by d.Drug_pk,b.StrengthId,b.StrengthName                                                                                                     
--2                                   
select distinct d.Drug_pk[GenericId],a.FrequencyId,b.Name [FrequencyName]                                     
from lnk_DrugFrequency a,mst_frequency b,@Drug d                                       
where a.frequencyid = b.id and d.Drug_pk=a.DrugID                                       
order by d.Drug_pk,a.FrequencyId                                                                                                        
--3                                                                                                     
select  '' Name,''EmployeeId  where 1 > 1--from  mst_Employee  where designationid in (1,2)order by FirstName                                                                                                        
--4   
select a.GenericId,a.GenericName,a.GenericAbbrevation,b.DrugTypeId,a.DeleteFlag                                                
from mst_generic a,lnk_drugtypegeneric b                                                                                                        
where a.genericid = b.genericid                                                                    
order by GenericName;                                                                                                       
--5   
Select '' Name, ''   EmployeeId  where 1 > 1                                                                                                       
--6                                                                                                     
Select                                                               
(convert(varchar(50), decryptbykey(firstname))                                       
+ ' '+                          
convert(varchar(50), decryptbykey(lastname)))[Name],                                                                          
PatientEnrollmentID,PatientClinicID,DOB, Convert(varchar, datediff(month,DOB,getdate())/12)[Age],                                                               
Convert(varchar, datediff(month,DOB,getdate())%12)[Age1],                                                                
CountryId +'-'+PosId+'-'+SatelliteId+'-'+PatientEnrollmentId [PatientID]                                                
from mst_patient where ptn_pk=@PatientID                                                                        
--7                                                        
select Id [UnitId],Name [UnitName] from mst_decode where codeid = 32 and deleteflag = 0                                                                                                    
--8                                                                                               
select Id [FrequencyId],Name [FrequencyName],multiplier  from mst_frequency where deleteflag = 0                                                       
--9                                                                                                     
select DrugId,GenericId,MaxDose,MinDose from lnk_nonarvdruggeneric where deleteflag = 0                                                              
--10                                                                                               
select Ptn_Pk,Moduleid,StartDate from dbo.lnk_patientprogramstart where ptn_pk = @PatientId                                        
--11                                                                                                     
exec dbo.pr_Admin_SelectTreatmentProgram_Constella                                                                                             
--12  
Select	m.Drug_Pk,
		m.DrugName,
		m.DrugTypeId,
		m.GenericAbbreviation [GenericAbbrevation],
		'0' [genericid],
		m.GenericCommaSeparated [genericname]
From @Drug m;
                                                                                  
--13                                                                                      
select Id [FrequencyId],Name [FrequencyName]  from mst_FrequencyUnits where deleteflag = 0                                                                                      
--14                                                                                      
SELECT ID,name,DeleteFlag from mst_Provider where DeleteFlag=0 order by SRNO asc                                                                                    
--15                                                                             
select Id [UnitId],Name [UnitName] from mst_decode where codeid = 32                                                                                                    
--16                                                                                
SELECT  ID, Name, DeleteFlag from mst_Provider order by SRNO asc                                                                                
--17                                                                            
 
Select Distinct	m.drug_pk,
				m.drugname,
				m.DrugTypeId,
				'0' [genericid],
				max(m.GenericAbbreviation) [GenericAbbrevation],
				lnkstr.StrengthId,
				isnull(convert(varchar, sum(st.Quantity)), 0) [Stock]
From @Drug m
	Join lnk_DrugStrength lnkstr On lnkstr.DrugId = m.Drug_pk
	Left Outer Join dtl_stocktransaction st On m.Drug_pk = st.ItemId
Group By	m.Drug_pk,
			m.Drugname,
			drugtypeid,
			lnkstr.StrengthId
Order By m.drugname                               
                                
--18      
select  distinct d.Drug_pk,c.StrengthId,c.StrengthName                                  
from lnk_DrugStrength a,mst_Strength c,@Drug d                            
where a.DrugId=d.Drug_pk                        
and a.StrengthId=c.StrengthId                                                                  
                                                              
--19 
select  distinct d.Drug_pk,c.ID as FrequencyId,c.Name as FrequencyName                                                                                            
from lnk_Drugfrequency a,mst_Frequency c,@Drug d                                                                                            
where a.DrugId=d.Drug_pk                                                                                            
and a.FrequencyId=c.ID    
--And d.DeleteFlag =0                                                                   
                                                                      
--20                                                                        
------for inactive generics 
select  null GenericId,null GenericName,null GenericAbbrevation,null DrugTypeId    where 1 > 1                                            
                                                                   
                                                                        
--21                                                                  
select  VisitDate from ord_Visit where VisitType=3 and Ptn_pk=@PatientId                                                   
Close symmetric key Key_CTC                                                              
                                                          
--22 period taken                               
select ID,Name from mst_decode where codeID=(select CodeID from mst_code where Name='Pharmacy Period Taken') and (DeleteFlag=0 or Deleteflag is null)                                                           
 --23 TB Regimen                                                        
select distinct r.ID,r.SRNo[TBRegimenID],r.Name,r.TreatmentTime,r.userID,l.GenericID,g.GenericName,(Case r.deleteflag when 0 then 'Active' when 1 then 'In-Active' end) [Status] from mst_TBregimen r                                 
 inner join lnk_TBRegimenGeneric l on l.TBRegimenID=r.ID                                          
 inner join mst_Generic g on l.GenericID=g.GenericID                                                                          
 where r.deleteflag=0                                                     
                                                  
--24                                                                                      
Select Top 1 VisitType, VisitDate,Visit_Id from ord_Visit where ptn_pk=@PatientID and VisitType = 11 order by VisitDate desc     
--Select null VisitType, null VisitDate, null Visit_Id --from ord_Visit where ptn_pk=@PatientID and VisitType = 11 order by VisitDate desc                                         
                                    
     
--25                                      
select a.ID, a.Name, b.DrugId from mst_drugschedule a inner join lnk_drugschedule b on a.ID=b.ScheduleId                  
                                    
--26                                    
select ID,Name, DeleteFlag, SRNO from dbo.Mst_RegimenLine order by SRNO                                      
--27
	declare @RegimeLine int ;
	Select Top (1) @RegimeLine = @RegimeLine
	From ord_patientpharmacyorder
	Where ptn_pk = @PatientID
		And DeleteFlag = 0
	Order By OrderedByDate Desc;
	Select @RegimeLine [RegimenLine]                                     
--28        
	Select	m.drug_pk,
			'0' [genericid],
			m.DrugName,
			m.DrugTypeId,
			m.GenericAbbreviation [GenericAbbrevation]
	From @Drug m
	Order By m.drugname                              
--29    
	Select	Drug_Pk,
			drugname [FixedDrug],
			GenericAbbreviation [GenericAbbrevation],
			DrugTypeId [drugtypeid]
	From @Drug;
  
--30  
Select Top 1 PV.Height, OV.VisitDate from dtl_patientvitals PV inner join ord_visit OV on PV.Visit_pk=OV.Visit_Id  
where PV.ptn_pk=@PatientID and PV.Height IS NOT NULL order by  OV.VisitDate desc                
    
--31  
Select Top 1 PV.Weight,PV.Height, OV.VisitDate from dtl_patientvitals PV inner join ord_visit OV on PV.Visit_pk=OV.Visit_Id  
where PV.ptn_pk=@PatientID and PV.Weight IS NOT NULL and OV.VisitDate between dateadd(day, -7, getdate()) and getdate() order by OV.VisitDate desc      
                                                 
end


Go