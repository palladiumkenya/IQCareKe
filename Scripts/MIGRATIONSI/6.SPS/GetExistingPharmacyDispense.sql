IF   EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_SCM_GetExistingPharmacyDispense_Futures]') AND type in (N'P', N'PC'))
BEGIN

EXEC('
ALTER procedure [dbo].[pr_SCM_GetExistingPharmacyDispense_Futures]          
	@Ptn_Pk int,        
	@StoreId int 
As          
Begin          
          
	Select	convert(varchar(11), isnull(a.DispensedByDate, a.OrderedByDate), 113) [TransactionDate],
			Case
				When a.OrderStatus = 1 Then ''New Order''
				When a.OrderStatus = 3 Then ''Partial Dispense''
				Else ''Already Dispensed Order''
			End [Status],
			a.Ptn_Pharmacy_Pk,
			cast(convert(VARCHAR(11), isnull(a.DispensedByDate, a.OrderedByDate), 113) AS DATETIME) AS TransactionDate_Formatted,
			a.VisitID as visitID
	From dbo.ord_PatientPharmacyOrder a
		Inner Join dbo.ord_visit b On a.visitid = b.visit_id
	Where a.Ptn_Pk = @Ptn_Pk
		And (StoreId = @StoreId Or a.StoreId Is Null)
		And (b.deleteflag Is Null Or b.deleteflag = 0)
		And (a.deleteflag Is Null Or a.deleteflag = 0)
		And (a.DispensedByDate Is Not Null Or a.OrderedByDate Is Not Null) 
	Order By isnull(a.DispensedByDate, a.OrderedByDate)     desc 
	
	END')
	END


	IF NOT  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_SCM_GetExistingPharmacyDispense_Futures]') AND type in (N'P', N'PC'))
BEGIN

EXEC('
CREATE procedure [dbo].[pr_SCM_GetExistingPharmacyDispense_Futures]          
	@Ptn_Pk int,        
	@StoreId int 
As          
Begin          
          
	Select	convert(varchar(11), isnull(a.DispensedByDate, a.OrderedByDate), 113) [TransactionDate],
			Case
				When a.OrderStatus = 1 Then ''New Order''
				When a.OrderStatus = 3 Then ''Partial Dispense''
				Else ''Already Dispensed Order''
			End [Status],
			a.Ptn_Pharmacy_Pk,
			cast(convert(VARCHAR(11), isnull(a.DispensedByDate, a.OrderedByDate), 113) AS DATETIME) AS TransactionDate_Formatted,
			a.VisitID as visitID
	From dbo.ord_PatientPharmacyOrder a
		Inner Join dbo.ord_visit b On a.visitid = b.visit_id
	Where a.Ptn_Pk = @Ptn_Pk
		And (StoreId = @StoreId Or a.StoreId Is Null)
		And (b.deleteflag Is Null Or b.deleteflag = 0)
		And (a.deleteflag Is Null Or a.deleteflag = 0)
		And (a.DispensedByDate Is Not Null Or a.OrderedByDate Is Not Null) 
	Order By isnull(a.DispensedByDate, a.OrderedByDate)     desc 
	
	END')
	END

 
   
            



