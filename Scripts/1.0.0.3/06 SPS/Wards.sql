/****** Object:  StoredProcedure [dbo].[pr_Wards_GetAdmission]    Script Date: 02/11/2015 15:32:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Wards_GetAdmission]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Wards_GetAdmission]
GO
/****** Object:  StoredProcedure [dbo].[pr_Wards_Get]    Script Date: 02/11/2015 15:32:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Wards_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Wards_Get]
GO
/****** Object:  StoredProcedure [dbo].[pr_Wards_Save]    Script Date: 02/11/2015 15:32:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Wards_Save]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Wards_Save]
GO
/****** Object:  StoredProcedure [dbo].[pr_Wards_DischargePatient]    Script Date: 02/11/2015 15:32:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Wards_DischargePatient]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Wards_DischargePatient]
GO
/****** Object:  StoredProcedure [dbo].[pr_Wards_SaveAdmission]    Script Date: 02/11/2015 15:32:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Wards_SaveAdmission]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Wards_SaveAdmission]
GO
/****** Object:  StoredProcedure [dbo].[pr_Wards_SaveAdmission]    Script Date: 02/11/2015 15:32:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Wards_SaveAdmission]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Joseph Njung''e
-- Create date: 06 Feb 2015
-- Description:	Admit a patient
-- =============================================
Create PROCEDURE [dbo].[pr_Wards_SaveAdmission] 
	-- Add the parameters for the stored procedure here
	@WardID int , 
	@UserID int ,
	@AdmittedBy int,
	@PatientID int ,
	@AdmissionDate datetime , 
	@AdmissionID int =Null, 
	---@AdmissionNumber  varchar(50)=Null ,		
	@ReferredFrom  varchar(50)=Null ,
	@BedNumber varchar(10) = Null , 
	@ExpectedDischargeDate datetime= Null,
	@Active bit =1 	
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	
	Begin Try
		If (@AdmissionID Is Null) 
		Begin
		-- we are adding a new admission
			If Exists (Select 1	From dtl_PatientWardAdmission	Where ptn_pk = @PatientID	And DisCharged = 0 And DeleteFlag=0) 
			Begin
				Raiserror (''This Patient is already admitted in another ward'', 16, 0);
				Return (1);
			End
			Insert Into dtl_PatientWardAdmission (
				Ptn_PK,
				WardID,
				ReferredFrom,
				AdmissionDate,
				--AdmissionNumber,
				AdmittedBy,
				BedNumber,
				ExpectedDischargeDate,
				CreateDate,
				UserID)				
			Values (
				@PatientID,
				@WardID,
				@ReferredFrom,
				@AdmissionDate,
				--Nullif(Ltrim(Rtrim(@AdmissionNumber)), ''''),
				@AdmittedBy,
				@BedNumber,
				@ExpectedDischargeDate,
				Getdate(),
				@UserID
			);
			Select  Scope_identity();
		End
		Else
		Begin
			-- update. only bednumber can be updated
			Update dtl_PatientWardAdmission Set				
				--AdmissionNumber = Isnull(AdmissionNumber,@AdmissionNumber),
				BedNumber =@BedNumber,
				ExpectedDischargeDate = @ExpectedDischargeDate,
				AdmissionDate = @AdmissionDate,
				UserID=@UserID	,
				DeleteFlag = ~@Active
			Where AdmissionID=@AdmissionID 
			And Ptn_PK =@PatientID;
			
			Select @AdmissionID
		End
	
	End Try 
		
	Begin Catch
		Declare @ErrorMessage nvarchar(4000), @ErrorSeverity int, @ErrorState int;
		Select	@ErrorMessage = Error_message(),
				@ErrorSeverity = Error_severity(),
				@ErrorState = Error_state();
		Raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState);
	End Catch
	
End

' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Wards_DischargePatient]    Script Date: 02/11/2015 15:32:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Wards_DischargePatient]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Joseph Njung''e
-- Create date: 09 Feb 2015
-- Description:	Discharge Patient From ward
-- =============================================
CREATE PROCEDURE [dbo].[pr_Wards_DischargePatient] 
	-- Add the parameters for the stored procedure here
	@AdmissionID int , 
	@DischargedBy int ,
	@UserID int,
	@DischargeDate datetime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update dtl_PatientWardAdmission Set	
		ActualDischargeDate = Isnull(ActualDischargeDate,@DischargeDate),
		UserID=@UserID,
		DischargedBy=@DischargedBy
	Where AdmissionID = @AdmissionID 
	And Discharged = 0 And @DischargeDate > AdmissionDate; 
	
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Wards_Save]    Script Date: 02/11/2015 15:32:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Wards_Save]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Joseph Njung''e
-- Create date: 05 Feb 2015
-- Description:	Create and update Patients wards
-- =============================================
CREATE PROCEDURE [dbo].[pr_Wards_Save] 
	-- Add the parameters for the stored procedure here
	@WardID int = Null, 
	@LocationID int ,
	@WardName varchar(50),
	@PatientCategory varchar(50),
	@Capacity int,
	@Active bit=1,
	@UserID int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

-- Insert statements for procedure here
Begin Try

	Declare @ExistingID int;
	
	Select Top 1 @ExistingID = WardID
	From dbo.Mst_PatientWard
	Where WardName = @WardName
	And PatientCategory = @PatientCategory
	And LocationID = @LocationID;

	If (@WardID Is Null) 
	Begin
		-- we are adding a new ward;
		If (@ExistingID Is Not Null) 
		Begin
			Raiserror (''Duplicate found: Such a ward already exists;'', 16, 1);
			Return (1);
		End
		Insert Into dbo.Mst_PatientWard (
			LocationID,
			WardName,
			PatientCategory,			
			Capacity,
			DeleteFlag,
			CreatedBy,
			CreatedDate)
		Values (
			@LocationID,
			@WardName,
			@PatientCategory,
			@Capacity,
			~@Active,
			@UserID,
			Getdate());		
	End
	Else
	Begin
		If (@ExistingID <> @WardID) 
		Begin
			Raiserror (''Unknown Error occured'', 16, 1);
			Return (1);
		End
		Update dbo.Mst_PatientWard Set
			WardName = @WardName,
			Capacity=@Capacity,
			PatientCategory = @PatientCategory,
			DeleteFlag=~@Active,
			UpdatedBy=@UserID,
			UpdatedDate=Getdate()
		Where WardID=@WardID;
	End
End Try 
Begin Catch
	Declare @ErrorMessage nvarchar(4000),@ErrorSeverity int,@ErrorState int;
	Select	@ErrorMessage = Error_message(),@ErrorSeverity = Error_severity(),@ErrorState = Error_state();  
	Raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState  );
End Catch
End

' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Wards_Get]    Script Date: 02/11/2015 15:32:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Wards_Get]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Joseph Njung''e
-- Create date: 05 Feb 2015
-- Description:	Get the patient wards
-- =============================================
CREATE PROCEDURE [dbo].[pr_Wards_Get] 
	-- Add the parameters for the stored procedure here
	@LocationID int , 
	@WardID int = Null
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
	Set Nocount On;

-- Insert statements for procedure here
	Select 
		WardID,
		LocationID,
		WardName,
		PatientCategory,
		Capacity,
		Occupancy,
		~DeleteFlag Active
	From dbo.Mst_PatientWard
	Where LocationID = @LocationID
	And
		Case
			When @WardID Is Null Or WardID = @WardID Then 1
			Else 0 
		End = 1
	Order By WardName;
End

' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Wards_GetAdmission]    Script Date: 02/11/2015 15:32:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Wards_GetAdmission]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Joseph Njung''e
-- Create date: 09 Feb 2015
-- Description:	Get Wards admission
-- =============================================
CREATE PROCEDURE [dbo].[pr_Wards_GetAdmission] 
	-- Add the parameters for the stored procedure here
	@LocationID int,
	@WardID int = Null, 
	@AdmissionID int = null,
	@PatientID int = null,
	@Password varchar(50),
	@ExcludeDischarged bit=0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
Declare @SymKey varchar(400)
Set @SymKey = ''Open symmetric key Key_CTC decryption by password='' + @password + ''''
Exec (@SymKey);

Select @WardID = Nullif(@WardID,''''), @AdmissionID = Nullif(@AdmissionID,''''), @PatientID = Nullif(@PatientID,'''');
If(@AdmissionID Is Not Null) Select @ExcludeDischarged = 0;
Select 
	A.AdmissionID,
	W.WardID,
	W.WardName,
	P.Ptn_Pk PatientID,
	Convert(varchar(50), Decryptbykey(P.LastName)) + '', ''+ Convert(varchar(50), Decryptbykey(P.FirstName)) PatientName,
	P.PatientFacilityID PatientNumber,
	A.AdmissionDate,
	A.BedNumber,
	Null AdmissionNumber,
	A.AdmittedBy,
	A.ExpectedDischargeDate ExpectedDOD,
	A.Discharged,
	A.DischargedBy,
	A.ReferredFrom,
	A.ActualDischargeDate ActualDOD	
From dtl_PatientWardAdmission A
Inner Join
	dbo.mst_Patient P On P.Ptn_Pk = A.Ptn_PK
Inner Join	
	dbo.Mst_PatientWard  W on W.WardID = A.WardID
Where W.LocationID = @LocationID
And Case 
		When (@WardID Is Not Null And @WardId = A.WardID) Or @WardID Is Null Then 1 
		Else 0 End = 1
And Case 
		When (@AdmissionID Is Not Null And @AdmissionID = A.AdmissionID) Or @AdmissionID Is  Null Then 1 
		Else 0 End = 1
And Case 
		When (@ExcludeDischarged = 1 And A.Discharged = 1) Then 0 
		Else 1 End = 1
And Case 
		When @PatientID  Is Not Null  And A.Ptn_PK = @PatientID Then 1 
		When @PatientID Is Null Then 1
		Else 0 End = 1
Order By A.AdmissionDate;

End


Close symmetric key Key_CTC

' 
END
GO
