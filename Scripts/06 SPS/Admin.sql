IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_Admin_UpdateBackupSetup_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_Admin_UpdateBackupSetup_Constella]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Pr_Admin_UpdateBackupSetup_Constella]    
@BackupDrive varchar(10),    
@BackUpTime datetime    
    
as    
    
begin
   if (@BackUpTime <> '1900-01-01' and @BackupTime Is Not Null)begin
	Update mst_Facility Set
			BackupDrive = @BackupDrive
		,	BackupTime = @BackUpTime;
	Update ScheduledTask Set
			NextRunDate = @BackupTime
		,	Active = 1
	Where TaskName = 'Database.Backup'
	End
	Else Begin
		Update mst_Facility Set
				BackupDrive = @BackupDrive
			,	BackupTime = Null
		Update ScheduledTask Set
				NextRunDate = Null
			,	Active = 0
		Where TaskName = 'Database.Backup'
	End
End
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_SystemAdmin_GetBackupTime_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_SystemAdmin_GetBackupTime_Constella]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[pr_SystemAdmin_GetBackupTime_Constella]        
as      
select BackupTime,BackupDrive from  mst_Facility where backuptime is not null And DeleteFlag = 0

GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_FindItemByName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_FindItemByName]
GO

/****** Object:  StoredProcedure [dbo].[pr_Admin_InsertDrug_Constella]    Script Date: 12/11/2014 16:16:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_InsertDrug_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_InsertDrug_Constella]
GO
/****** Object:  StoredProcedure [dbo].[pr_Admin_GetItemsByType]    Script Date: 12/11/2014 16:16:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_GetItemsByType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_GetItemsByType]
GO
/****** Object:  StoredProcedure [dbo].[pr_Admin_SaveItemsForBillable]    Script Date: 02/02/2015 12:52:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_SaveItemsForBillable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_SaveItemsForBillable]
GO
/****** Object:  StoredProcedure [dbo].[pr_Admin_GetItemsForBillable]    Script Date: 02/02/2015 12:20:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_GetItemsForBillable]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_GetItemsForBillable]
GO
/****** Object:  StoredProcedure [dbo].[pr_Admin_GetSubItemTypes]    Script Date: 12/11/2014 16:16:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_GetSubItemTypes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_GetSubItemTypes]
GO
/****** Object:  StoredProcedure [dbo].[pr_Admin_GetItemTypeIDByName]    Script Date: 02/02/2015 12:20:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_GetItemTypeIDByName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_GetItemTypeIDByName]
GO

/****** Object:  StoredProcedure [dbo].[pr_Admin_InsertSubTypeForItem]    Script Date: 12/11/2014 16:16:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_InsertSubTypeForItem]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_InsertSubTypeForItem]
GO
/****** Object:  StoredProcedure [dbo].[pr_Admin_InsertUpdateItem]    Script Date: 12/11/2014 16:16:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_InsertUpdateItem]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_InsertUpdateItem]
GO
/****** Object:  StoredProcedure [dbo].[pr_Admin_InsertUpdateSubItemType]    Script Date: 12/11/2014 16:16:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_InsertUpdateSubItemType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_InsertUpdateSubItemType]
GO

/****** Object:  StoredProcedure [dbo].[pr_Admin_GetSubTypesForItem]    Script Date: 12/11/2014 16:16:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_GetSubTypesForItem]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_GetSubTypesForItem]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_SelectFeature_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_SelectFeature_Constella]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_Admin_SelectFeature_Constella]       
@SystemId int,        
@FacilityId int        
AS              
        
BEGIN           
        
declare @SQL varchar(2000)        
declare @SID varchar(100)       
declare @MID varchar(200)        
declare @sqlcode varchar(max)        
set @SID = convert(varchar,@SystemId) +',0'        
       
      
      
select * into #vwModuleId FROM      
(      
  SELECT ModuleId FROM mst_Facility a inner join lnk_FacilityModule b on  a.facilityid=b.facilityid and a.facilityid=@FacilityId)AS A      
INSERT INTO #vwModuleId(ModuleId) values('0')     


set @SQL = 'select FeatureID, FeatureName, UserID         
from mst_feature where ModuleId in (Select moduleid from #vwModuleId) and    
ReportFlag=1 and AdminFlag=0 and DeleteFlag = 0 and SystemId in ('+@SID+')        
order by FeatureName    
        
SELECT a.FeatureID, a.FeatureName, a.UserID              
FROM mst_Feature a where  a.ModuleId in (Select moduleid from #vwModuleId) and    
a.ReportFlag=0 and a.Adminflag = 0 and a.DeleteFlag = 0 and    
a.Published is null and a.SystemId in ('+@SID+')        
union    
SELECT a.FeatureID, a.FeatureName, a.UserID              
FROM mst_Feature a where  a.ModuleId in (Select moduleid from #vwModuleId) and    
a.Countryid in (select Currency from mst_facility where facilityId = '+convert(varchar,@FacilityId)+') and    
a.ReportFlag=0 and a.Adminflag = 0 and a.DeleteFlag = 0 and    
a.Published =2 and a.SystemId in ('+@SID+')    
union    
SELECT a.FeatureID, a.FeatureName, a.UserID              
FROM mst_Feature a where  a.ModuleId in (Select moduleid from #vwModuleId) and    
a.ReportFlag=0 and a.Adminflag = 0 and a.DeleteFlag = 0 and a.SystemId in ('+@SID+') and a.RegistrationFormFlag =1    
order by a.FeatureName             

        
Select FeatureID, FeatureName, UserID            
From mst_Feature where  ModuleId in (Select moduleid from #vwModuleId ) and    
AdminFlag=1 and DeleteFlag = 0 and SystemId in ('+@SID+')        
order by FeatureName'        
       
exec(@SQL)            
END

GO


/****** Object:  StoredProcedure [dbo].[pr_Admin_DeletePatient_Constella]    Script Date: 12/11/2014 16:16:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_DeletePatient_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_DeletePatient_Constella]
GO

/****** Object:  StoredProcedure [dbo].[pr_Admin_DeletePatient_Constella]    Script Date: 12/11/2014 16:16:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_DeletePatient_Constella]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'/****** 
	Updated: Njung''e J
	On	   : 05 May 2014
	Description:  Set all patient identifiers to Null to enable reuse.

 ******/
CREATE PROCEDURE [dbo].[pr_Admin_DeletePatient_Constella]   
@PatientId int, 
@UserID int  
 -- Add the parameters for the stored procedure here  
AS  
BEGIN
Update dbo.mst_Patient Set
	deleteflag = 1,
	UserID = @UserID,
	UpdateDate = Getdate()
Where Ptn_Pk = @PatientId;

Declare @TsqlUpdate varchar(max) ,@FieldName varchar(200) ,@TsqlSelect varchar(max) ;
       
Select @TsqlUpdate = ''''  ,@TsqlSelect='''';
Declare @SS varchar(4000), @UpdateStat varchar(4000),@SelectStat varchar(4000);
Select @ss=  Substring((Select '',['' + Convert(varchar(Max), FieldName) + '']''
			From dbo.mst_patientidentifier
			Order By Id
			For xml Path (''''))
		, 2, 4000);
		
Select @TsqlUpdate = ''Update dbo.mst_Patient Set ''+ Replace(@SS, '','', '' = Null,'')++ '' = Null'' +'' Where Ptn_PK = ''+ Convert(varchar, @PatientID);

Set @TsqlSelect = ''Declare @xml_var XML; Set @xml_var = (Select ''+@SS +'' From dbo.mst_patient  Where Ptn_pk = '' + convert(varchar,@PatientId) +'' FOR XML RAW (''''Identifiers''''), ELEMENTS );
Insert Into dbo.Dtl_PatientDeleteLog(Ptn_PK , Identifiers , DeleteDate , UserID)  Select ''+ convert(varchar,@PatientId) +'', @xml_var,getdate(),''+ Convert(varchar,@UserID)

exec (@TsqlSelect);

exec (@TsqlUpdate) ;

End 

       


' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Admin_GetSubTypesForItem]    Script Date: 12/11/2014 16:16:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_GetSubTypesForItem]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:  Joseph Njung''e
-- Create date: 11 June 2014
-- Description: Get Sub types for  an Item e.g a drug could be antiflu and and also anti painkiller
-- =============================================
Create PROCEDURE [dbo].[pr_Admin_GetSubTypesForItem] 
 -- Add the parameters for the stored procedure here
 @Item_PK int = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
Select S.SubTypeName,
  L.Item_PK,
  S.SubItemTypeID,
  S.ItemTypeID
From dbo.Lnk_ItemSubType L
Inner Join dbo.Mst_ItemSubType S
 On S.SubItemTypeID = L.ItemSubTypeID
Where L.Item_PK = @Item_PK;
    
END
' 
END
GO

/****** Object:  StoredProcedure [dbo].[pr_Admin_InsertUpdateSubItemType]    Script Date: 12/11/2014 16:16:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_InsertUpdateSubItemType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Joseph Njung''e
-- Create date: 11 June 2014
-- Description:	Add Item SubTypes
--	ItemSubTypeID formart :  less thabn 2000 = drugs
--							2000 -2999 = Consumbales--
--							3000 - 3999 	Equipment
--							4000 - 4999 Lab Tests
--							5000 - 5999 Non-Pharmaceuticals
--							6000>= Others

-- =============================================
CREATE PROCEDURE [dbo].[pr_Admin_InsertUpdateSubItemType] 
	-- Add the parameters for the stored procedure here
	
	@ItemSubTypeID int =Null,
	@SubTypeName varchar(150),
	@ItemTypeID int,
	@UserID int,
	@DeleteFlag bit = 0
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
--int ItemSubTypeID, string SubTypeName, int ItemTypeID, int UserID
    -- Insert statements for procedure here
	If  Exists (Select 1
	From dbo.Mst_ItemSubType
	Where SubItemTypeID = @ItemSubTypeID)
	Begin
		Update dbo.Mst_ItemSubType Set
			SubTypeName = @SubTypeName,
			ItemTypeID = @ItemTypeID,
			DeleteFlag = @DeleteFlag,
			UpdateDate = Getdate()
		Where SubItemTypeID = @ItemSubTypeID;
	End
	Else
	Begin
		Select @ItemSubTypeID = Max(SubItemTypeID) + 1
		From dbo.Mst_ItemSubType
		Where ItemTypeID = @ItemTypeID;
	
		If(@ItemSubTypeID Is Null)
		Begin
			Select @ItemSubTypeID =
					Case @ItemTypeID
						When 299 Then 2000
						When 300 Then 1
						When 301 Then 3000
						When 308 Then 4000
						When 331 Then 5000
						Else 6000 End
		End
		Insert Into dbo.Mst_ItemSubType (
			SubItemTypeID,
			SubTypeName,
			ItemTypeID,
			DeleteFlag,
			UserID,
			CreateDate)
		Values (
			@ItemSubTypeID,
			@SubTypeName,
			@ItemTypeID,
			@DeleteFlag,
			@UserID,
			Getdate());
	End
	Select @ItemSubTypeID;
END
--301	Equipment
--308	Lab Tests
--331	Non-Pharmaceuticals' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Admin_SaveItemsForBillable]    Script Date: 02/02/2015 12:52:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_SaveItemsForBillable]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

-- =============================================
-- Author:		Vincent Yahuma
-- Create date: 2014-Aug-08
-- Description:	Procedure for  saving billables items 
-- =============================================
CREATE PROCEDURE [dbo].[pr_Admin_SaveItemsForBillable](
@BillableID int,
@UserID int,
@ItemList xml)
AS
BEGIN
declare @Items Table(ItemTypeID int,ItemId int, RowStatus varchar(15));

Insert Into @Items
Select	bi.record.value(''itemtypeid[1]'', ''int'') ItemTypeID,
		bi.record.value(''itemid[1]'', ''int'') ItemID,
		bi.record.value(''rowstatus[1]'', ''varchar(15)'') RowStatus
From @ItemList.nodes(''/root/row'') As bi (record) ;
--Delete items marked for deletion

Delete DB 
From Dtl_Billables DB
Inner Join
	@Items I On I.ItemTypeID = DB.BillingTypeID
And I.ItemId = DB.ItemID
And DB.DeCodeID = @BillableID
Where  I.RowStatus=''Deleted'';


--Insert new records
Insert Into Dtl_Billables (
	DecodeID,
	BillingTypeID,
	ItemID,
	DeleteFlag,
	UserID,
	CreateDate)
	Select	@BillableID,
			I.ItemTypeID,
			I.ItemID,
			0,
			@UserID,
			Getdate()
	From @Items I
	Where I.RowStatus=''Added'';

End' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Admin_GetItemsForBillable]    Script Date: 02/02/2015 12:20:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_GetItemsForBillable]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Joseph Njung''e
-- Create date: 23 01 2015
-- Description:	Get items in a billable adopted from pr_SCM_GetBillablesItems
-- =============================================
CREATE PROCEDURE [dbo].[pr_Admin_GetItemsForBillable] 
	-- Add the parameters for the stored procedure here
	@BillableItemID int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @ItemTypeID int, 
		@Item_PK int, 
		@ItemTypeName varchar(50), 
		@TableName varchar(50), 
		@FieldName varchar(50), 
		@IDFieldName varchar(50),
		@Query varchar(max),
		@Part varchar(400);
			
	Select	@Query = '''',
			@Part = '''';
	Declare command Cursor For
		Select	D.ItemID Item_PK,
				D.BillingTypeID ItemTypeID,
				Name As ItemTypeName,
				BT.MasterTableName TableName,
				BT.MasterIDField IDField,
				BT.MasterFieldName FieldName
		From Dtl_Billables D
		Inner Join
			Mst_BillingType BT On BT.BillingTypeID = D.BillingTypeID
		Where D.DeCodeID = @BillableItemID
		And BT.DeleteFlag = 0
		And D.DeleteFlag = 0;
	Open command
	Fetch Next From command Into @Item_Pk,@ItemTypeID,@ItemTypeName,@TableName,@IDFieldName,@FieldName;
	While @@FETCH_STATUS=0
	Begin
		Select @Part = (Select ''Select ItemID='' + Convert(varchar,@Item_PK) +'' ,ItemName =''+ @FieldName + '' , ItemTypeName=''''''+ @ItemTypeName +'''''',ItemTypeID='' + Convert(varchar,@ItemTypeID) +'' ,DeleteFlag FROM '' +@TableName
			+'' WHERE ''+ @IDFieldName + '' = '' + Convert(varchar,@Item_PK)  +  '' AND DeleteFlag=0 '');
		Select @Query = @Query + @Part + '' Union ''
		Fetch Next From command Into @Item_Pk,@ItemTypeID,@ItemTypeName,@TableName,@IDFieldName,@FieldName;
	End

	Close command
	Deallocate command

	IF(LEN(@Query)>6)
	Begin
		Select @Query=LEFT(@Query,LEN(@Query)-6) + '' Order By ItemTypeName, ItemName'';
		
		Execute(@Query);
	End
	Else
	Begin
		Select 0  ItemID ,'''' ItemName, '''' As ItemTypeName, 0 ItemTypeID,0 DeleteFlag Where 1 > 2;
	End
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Admin_InsertUpdateItem]    Script Date: 12/11/2014 16:16:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_InsertUpdateItem]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Joseph Njung''e
-- Create date: 11 June 2014
-- Description:	Add Item into the Master List. Only the basic  details
-- =============================================
CREATE PROCEDURE [dbo].[pr_Admin_InsertUpdateItem] 
	-- Add the parameters for the stored procedure here
	@ItemName varchar(200),
	@ItemTypeID int ,	
	@UserID int,
	@Item_PK int = null,
	@DeleteFlag bit = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    
    If @Item_PK Is Not Null And Exists(Select 1 From dbo.Mst_ItemMaster Where Item_PK = @Item_PK)
	Begin
		If Exists(Select 1 From dbo.Mst_ItemMaster Where ItemName=@ItemName And Item_PK <> @Item_PK)
		Begin
			Raiserror(''Duplication::An item with similar name already exists'',16,1);
			Return (1);
		End
		Update dbo.Mst_ItemMaster Set
			ItemName = @ItemName,
			@ItemTypeID = @ItemTypeID,
			UpdateBy = @UserID,
			UpdateDate = Getdate(),
			DeleteFlag= @DeleteFlag
		Where Item_PK = @Item_PK;
		Select @Item_PK;			
	End
	Else
	Begin
		If Exists(Select 1 From dbo.Mst_ItemMaster Where ItemName=@ItemName )
		Begin
			Raiserror(''Duplication::An item with similar name already exists'',16,1);
			Return (1);
		End
		Insert Into dbo.Mst_ItemMaster(ItemName,ItemTypeID,CreatedBy,CreateDate,DeleteFlag)		
		Values(@ItemName,@ItemTypeId, @UserID, Getdate(),@DeleteFlag);		
		Select SCOPE_IDENTITY();
		
	End
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Admin_InsertSubTypeForItem]    Script Date: 12/11/2014 16:16:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_InsertSubTypeForItem]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Joseph Njung''e
-- Create date: 11 June 2014
-- Description:	Insert SubTypes for item
-- =============================================
CREATE PROCEDURE [dbo].[pr_Admin_InsertSubTypeForItem] 
	-- Add the parameters for the stored procedure here
	@Item_PK int  ,
	@SubTypes xml 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Delete From dbo.Lnk_ItemSubType Where Item_PK = @Item_PK;
	
	Select @SubTypes = Convert(xml,@SubTypes);	
	Insert Into dbo.Lnk_ItemSubType(Item_PK,ItemSubTypeID)	
	Select 
		@Item_PK, 
		T.N.value(''subtypeid[1]'', ''int'')
	From  @SubTypes.nodes(''/root/parameter'') as T(N);

END
' 
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_GetFacilityCmbList_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_GetFacilityCmbList_Constella]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[pr_Admin_GetFacilityCmbList_Constella]          
          
as          
          
Select	FacilityID
	,	CountryID + '-' + PosID + '-' + SatelliteID + ' - ' + FacilityName	As DescriptiveName
	,	Image
	,	SystemId
	,	Preferred
	,	CountryID
	,	AppGracePeriod
	,	Currency
	,	SatelliteID
	,	PosID
	,	FacilityName
	,	PaperLess
From mst_Facility
Where (DeleteFlag = 0)
Order By CountryID, PosID, SatelliteID
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_SelectFacility_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_SelectFacility_Constella]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_Admin_SelectFacility_Constella]                
        
AS                
      
begin        
      
	  Select	FacilityID
		,	CountryID + '-' + PosID + '-' + SatelliteID + ' - ' + FacilityName	As DescriptiveName
		,	Image
		,	SystemId
		,	Preferred
		,	CountryID
		,	Isnull(AppGracePeriod,0) AppGracePeriod
		,	Currency
		,	SatelliteID
		,	PosID
		,	FacilityName
		,	Isnull(PaperLess,0) PaperLess
		,	Isnull(BackupDrive,'C:') BackupDrive
		,	BackupTime
	From mst_Facility
	Where (DeleteFlag = 0)
	Order By CountryID, PosID, SatelliteID    
      
	Select	Id
		,	AppVer
		,	DBVer
		,	RelDate
		,	VersionName
	From AppAdmin     
      
end

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_SelectModulesByFacilityID_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_SelectModulesByFacilityID_Constella]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[pr_Admin_SelectModulesByFacilityID_Constella]                                  
@facilityId int          
 as                                  
begin
--0                                
	Select	L.FacilityID,
			M.ModuleID,
			M.ModuleName,
			M.DisplayName,
			M.CanEnroll
	From lnk_FacilityModule As L
	Inner Join
		mst_module As M On L.ModuleID = M.ModuleID
	Where (L.FacilityID = @FacilityID)
	And (M.[Status] = 2)
	And (M.DeleteFlag = 0 Or M.DeleteFlag Is Null)
	Order by M.ModuleName;


	Select	Id
		,	ModuleId
		,	BusRuleId
		,	Value
		,	UserId
		,	CreateDate
		,	UpdateDate
		,	Value1
		,	SetType
	From lnk_ServiceBusinessRule


	/*2*/
	Select	v.ModuleID
		,	v.FieldID
	From mst_module As m
	Inner Join lnk_PatientModuleIdentifier As v On v.ModuleID = m.ModuleId
	Inner Join lnk_FacilityModule As l On l.ModuleID = m.ModuleId
	Where (l.FacilityID = @FacilityID)
		And (m.Status = 2)
		And (m.DeleteFlag = 0 Or m.DeleteFlag Is Null)
	--3
	SELECT Z.FacilityID
		,a.ModuleId
		,a.ModuleName
		,a.PharmacyFlag
		,Z.StrongPassFlag
		,Z.ExpPwdFlag
		,Z.ExpPwdDays
	FROM (
		SELECT a.FacilityID
			,b.ModuleID
			,a.StrongPassFlag
			,a.ExpPwdFlag
			,a.ExpPwdDays
		FROM mst_Facility a
		INNER JOIN lnk_FacilityModule b ON a.FacilityID = b.FacilityID
		) Z
	INNER JOIN mst_module a ON Z.ModuleID = a.ModuleID
	WHERE a.STATUS = 2
		AND FacilityID = @FacilityID
	
End

GO



/****** Object:  StoredProcedure [dbo].[pr_Admin_GetItemTypeIDByName]    Script Date: 02/02/2015 12:20:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_GetItemTypeIDByName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Joseph Njung''e
-- Create date: Nov 3 2014
-- Description:	Get Item Type Billing ID
-- =============================================
CREATE PROCEDURE [dbo].[pr_Admin_GetItemTypeIDByName] 
	-- Add the parameters for the stored procedure here
	@ItemName varchar(50)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		Select Top 1 ItemTypeID From Mst_ItemType Where ItemName=@ItemName;
END
' 
END

GO
/****** Object:  StoredProcedure [dbo].[pr_Admin_GetSubItemTypes]    Script Date: 12/11/2014 16:16:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_GetSubItemTypes]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Joseph Njung''e
-- Create date: 11 June 2014
-- Description:	Get Items by subType
-- =============================================
CREATE  procedure [dbo].[pr_Admin_GetSubItemTypes] 
@ItemTypeId int = 300,  -- drugs
@ActiveOnly bit =1                                                                        
as
begin


Select	ST.SubItemTypeID ,
		ST.SubTypeName SubTypeName,
		ST.DeleteFlag,
		ST.ItemTypeID,
		[Status] =
			Case ST.DeleteFlag
				When 0 Then ''Active''
				When 1 Then ''InActive'' End,
		IT.ItemName ItemTypeName
From dbo.Mst_ItemSubType ST
Inner Join
	dbo.Mst_ItemType IT On IT.ItemTypeID = ST.ItemTypeID
Where (ST.ItemTypeID = @ItemTypeId OR @ItemTypeId Is Null)
and ST.DeleteFlag = ~@ActiveOnly
Order by ST.SubTypeName;
end' 
END
GO

/****** Object:  StoredProcedure [dbo].[pr_Admin_GetItemsByType]    Script Date: 12/11/2014 16:16:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_GetItemsByType]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Joseph Njung''e
-- Create date: 11 June 2014
-- Description:	Get Items by Type
-- =============================================
CREATE PROCEDURE [dbo].[pr_Admin_GetItemsByType] 
	-- Add the parameters for the stored procedure here
	@ItemTypeID int = 300
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

Select	IM.Item_PK,
		IM.ItemName,
		IT.ItemName ItemTypeName,
		IM.ItemTypeID,
		--ST.SubTypeName,
		--ST.SubItemTypeID,
		[Status] =
			Case IM.DeleteFlag
				When 0 Then ''Active''
				When 1 Then ''InActive'' End,
		[HasDetails] = Convert(bit,Case IT.ItemName When ''Billables'' Then 1 Else 0  End)
From dbo.Mst_ItemMaster IM
Inner Join dbo.Mst_ItemType IT
	On IT.ItemTypeID = IM.ItemTypeID
--Inner Join (Select Top 1	ST.SubTypeName,
--							ST.SubItemTypeID,
--							ST.ItemTypeID,
--							LST.Item_PK
--	From dbo.Mst_ItemSubType ST
--	Inner Join dbo.Lnk_ItemSubType LST
--		On LST.ItemSubTypeID = ST.SubItemTypeID) ST
--	On ST.ItemTypeID = IM.ItemTypeID
Where IM.ItemTypeID = @ItemTypeID;
	
End

' 
END
GO

/****** Object:  StoredProcedure [dbo].[pr_Admin_FindItemByName]    Script Date: 02/02/2015 12:20:13 ******/
/****** Object:  StoredProcedure [dbo].[pr_Admin_FindItemByName]    Script Date: 03/10/2015 11:44:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_FindItemByName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Joseph Njung''e
-- Create date: 13 June 2014
-- Description:	Search for items by name
-- =============================================
CREATE PROCEDURE [dbo].[pr_Admin_FindItemByName] 
	-- Add the parameters for the stored procedure here
	@SearchText varchar(15)  , 
	@ItemTypeID int = Null,
	@ExcludeItemTypeID int = Null,
	@BillingDate datetime = Null,
	@HasPrice bit = 1
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

-- Insert statements for procedure here
If Ltrim(Rtrim(@SearchText)) <> '''' Select @SearchText = ''%'' + @SearchText + ''%'';
If (@BillingDate Is Null) Select @BillingDate = Getdate();

Select @BillingDate = Dateadd(dd, 0, Datediff(dd, 0, @BillingDate));

	If(@HasPrice=1)
	Begin
		
		Select PL.ItemID,
				PL.ItemName,
				PL.ItemTypeID,
				PL.ItemTypeName,
				PL.PriceOnDate SellingPrice,
				PL.PriceDate,
				0 DeleteFlag
		From dbo.fn_Billing_PriceList(default,@ItemTypeID,@BillingDate) PL
		Where PL.ItemName Like @SearchText
		And Case
			When @ExcludeItemTypeID Is Null Or @ExcludeItemTypeID <> PL.ItemTypeID Then 1
			Else 0 End = 1
		And PriceIndex = 1	
		And PriceOnDate > 0.0	;		
	End
	Else
	Begin

		Select	I.ItemID,
				I.ItemName,
				I.ItemTypeID,
				I.ItemTypeName,
				Isnull(I.UnitSellingPrice, 0) SellingPrice,
				I.PriceDate,
				I.DeleteFlag
		From dbo.vw_Master_ItemList I
		Where Case
			When @HasPrice = 1 And (I.UnitSellingPrice !> 0 Or I.UnitSellingPrice Is Null) Then 0
			Else 1 End = 1
		And
			Case
				When @ItemTypeID Is Null Or ItemTypeID = @ItemTypeID Then 1
				Else 0 End = 1
		And I.ItemName Like @SearchText
		And Case
			When @ExcludeItemTypeID Is Null Or @ExcludeItemTypeID <> I.ItemTypeID Then 1
			Else 0 End = 1
		And
			Case
				When @HasPrice = 0 Then 1
				When (Dateadd(dd, 0, Datediff(dd, 0, PriceDate)) <= @BillingDate) Then 1
				Else 0 End = 1;

	End
End' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Admin_InsertDrug_Constella]    Script Date: 12/15/2015 10:51:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_Admin_InsertDrug_Constella]              
(              
 @DrugId int,          
 @DrugName varchar(150),              
 @DrugAbbreviation varchar(50),              
 @DrugTypeID int,              
 @UserID int,          
 @Status int,      
 @Update int             
             
)              
AS              
              
Begin             
    Declare  @ItemTypeID int;
    Select @ItemTypeID=ItemTypeID From Mst_ItemType Where ItemName='Pharmaceuticals'     ;
	     
	If (@Update > 0 ) Begin
		Update [mst_Drug] Set
			[DeleteFlag] = @Status,
			[UpdateDate] = getdate()
		Where [Drug_Pk] = @DrugId

		Select *
		From mst_Drug
		Where (Drug_pk = @DrugId)
	End 
	Else Begin
		If Exists(Select *	From mst_Drug Where DrugName = @DrugName) 
		Begin
			Update [mst_Drug] Set
				[DeleteFlag] = @Status,
				[UpdateDate] = getdate()
			Where [Drug_Pk] = @DrugId;
			Select 0
			Return
		End
 
		Declare @table Table(Id int); Declare @DrugPk int;
		Insert Into Mst_ItemMaster(
			ItemTypeID,
			ItemName, 
			CreatedBy,
			DeleteFlag,
			CreateDate
		)
		Values(
			@ItemTypeID,
			nullif(@DrugName,''),
			@UserId,
			0,
			getdate()
		)

		--Insert Into [mst_Drug] (
		--	[DrugName],
		--	[UserID],
		--	[DeleteFlag],
		--	CreateDate)
		--	--Output Inserted.Drug_Pk Into @table (Id)
		--	Values (
		--			nullIf(@DrugName, ''),
		--			@UserID,
		--			0,
		--			getdate() );
			Select @DrugPk= scope_identity();
			Select D.* From mst_Drug D Where (Drug_pk = @DrugPk) --	(Select	ID	From @table));
		
	End

End
GO
/****** Object:  StoredProcedure [dbo].[pr_Admin_GetModuleName_Futures]    Script Date: 03/17/2016 07:29:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_GetModuleName_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_GetModuleName_Futures]
GO

/****** Object:  StoredProcedure [dbo].[pr_Admin_GetModuleName_Futures]    Script Date: 03/17/2016 07:29:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_Admin_GetModuleName_Futures]        
         
         
AS        
BEGIN        
         
 SET NOCOUNT ON;        
        
    -- Insert statements for procedure here        
 SELECT moduleid, modulename,displayname,CanEnroll from mst_module where deleteflag = 0 and status=2   
  
SELECT moduleid, modulename,displayname,CanEnroll from mst_module where deleteflag = 0        
END

GO
/****** Object:  StoredProcedure [dbo].[Pr_Admin_SaveNewUser_Constella]    Script Date: 03/17/2016 07:28:02 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_Admin_SaveNewUser_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_Admin_SaveNewUser_Constella]
GO

/****** Object:  StoredProcedure [dbo].[Pr_Admin_SaveNewUser_Constella]    Script Date: 03/17/2016 07:28:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Pr_Admin_SaveNewUser_Constella]              
              
@fname varchar(50),              
@lname varchar(50),              
@username varchar(20),              
@password varchar(50),
@EmpId int,              
@userid int              
              
as              
              
begin              
    if exists(select userid from mst_user where username = @username) begin    
          Raiserror('Duplication error: The username is already taken',16,1);            
          return              
     end    
	If (@EmpId > 0 And Exists(Select 1 From Mst_User U Where U.EmployeeId = @EmpId  And U.DeleteFlag = 0 And (@EmpId Is Not Null Or @EmpId > 0))) Begin
	Raiserror('Duplication: The employee is already linked to another user',16,1);
	Return;
	End         
	Insert Into mst_user (
		userlastname,
		userfirstname,
		username,
		password,
		EmployeeId,
		deleteflag,
		operatorid,
		createdate)
	Values (
		@lname,
		@fname,
		@username,
		@password,
		@EmpId,
		0,
		@userid,
		getdate() )                    
              
	select scope_identity()        
end

GO



/****** Object:  StoredProcedure [dbo].[pr_Admin_UpdateUser_Constella]    Script Date: 03/17/2016 07:26:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_UpdateUser_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_UpdateUser_Constella]
GO

/****** Object:  StoredProcedure [dbo].[pr_Admin_UpdateUser_Constella]    Script Date: 03/17/2016 07:26:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_Admin_UpdateUser_Constella]  
(  
 @UserLastName varchar(50),  
 @UserFirstName varchar(50),  
 @username varchar(50),  
 @Password varchar(50), 
 @EmpId int, 
 @OperatorID int,  
 @UserID int  
)  
AS  
  
begin


If (@EmpId > 0 And Exists(Select 1 From Mst_User U Where U.EmployeeId = @EmpId And userId<> @UserId And U.DeleteFlag = 0 And (@EmpId Is Not Null Or @EmpId > 0))) Begin

	Raiserror('Duplication: The employee is already linked to another user',16,1);
	Return;

End

Update [mst_User] Set
	[UserLastName] = @UserLastName,
	[UserFirstName] = @UserFirstName,
	[UserName] = @username,
	[Password] = @Password,
	[EmployeeId] = @EmpId,
	[OperatorID] = @OperatorID,
	[UpdateDate] = getdate()
Where [UserID] = @UserID
And [DeleteFlag] = 0

End
GO
