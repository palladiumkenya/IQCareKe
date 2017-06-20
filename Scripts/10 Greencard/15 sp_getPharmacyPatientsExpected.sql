/****** Object:  StoredProcedure [dbo].[sp_getPharmacyPatientsExpected]    Script Date: 6/20/2017 10:35:14 PM ******/
DROP PROCEDURE [dbo].[sp_getPharmacyPatientsExpected]
GO
/****** Object:  StoredProcedure [dbo].[SP_Bluecard_ToGreenCard]    Script Date: 6/20/2017 10:35:14 PM ******/
DROP PROCEDURE [dbo].[SP_Bluecard_ToGreenCard]
GO
/****** Object:  StoredProcedure [dbo].[pr_SystemAdmin_Backup_Constella]    Script Date: 6/20/2017 10:35:14 PM ******/
DROP PROCEDURE [dbo].[pr_SystemAdmin_Backup_Constella]
GO
/****** Object:  StoredProcedure [dbo].[Pr_SCM_GetPurcaseOrderItem]    Script Date: 6/20/2017 10:35:14 PM ******/
DROP PROCEDURE [dbo].[Pr_SCM_GetPurcaseOrderItem]
GO
/****** Object:  StoredProcedure [dbo].[Pharmacy_GetPrescription]    Script Date: 6/20/2017 10:35:14 PM ******/
DROP PROCEDURE [dbo].[Pharmacy_GetPrescription]
GO
/****** Object:  StoredProcedure [dbo].[Pharmacy_GetPrescription]    Script Date: 6/20/2017 10:35:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Joseph
-- Create date: 
-- Description:	Get pending pharmacy prescriptions
-- =============================================
CREATE PROCEDURE [dbo].[Pharmacy_GetPrescription] 
	-- Add the parameters for the stored procedure here
	@PrescriptionDate datetime , 
	@LocationId int,
	@PrescriptionStatus int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @StartDate datetime, @EndDate datetime;

	Select @StartDate = dateadd(second, 0, dateadd(day, datediff(day, 0, @PrescriptionDate), 0)) ,	@EndDate = dateadd(second, -1, dateadd(day, datediff(day, 0, @PrescriptionDate)+1, 0))
    -- Insert statements for procedure here
	Select	PV.Ptn_pk
		,	PatientEnrollmentID PatientFacilityId
		,	ptn_pharmacy_pk	OrderId
		,	ReportingID		PrescriptionNumber
		,	FirstName
		,	MiddleName
		,	LastName
		,	DOB
		,	Sex
		,	convert(varchar(20),OrderedByDate,106) OrderedByDate
		,	Case
				When PO.OrderStatus = 1 Then 'New Order'
				When PO.OrderStatus = 3 Then 'Partial Dispense'
				Else 'Already Dispensed Order'
			End [Status]
		,  convert(varchar(20),PO.CreateDate,106) CreateDate
		--,	cast(datediff(Hour, PO.CreateDate, getdate()) As varchar) + ' hrs ' + cast(datediff(Minute, PO.CreateDate, getdate()) % 60 As varchar) + ' mins' Duration
		, datediff(Minute, PO.CreateDate, getdate()) Duration
		,	(
			Select UserFirstName + ' ' + UserLastName
			From mst_User U
			Where U.UserId = PO.OrderedBy
			)				
			PrescribedBy
	From ord_PatientPharmacyOrder PO
	Inner Join PatientView PV On PV.Ptn_Pk = PO.Ptn_pk 
	Where orderstatus = @PrescriptionStatus
	And PO.DeleteFlag = 0 And PV.DeleteFlag = 0 And OrderedByDate Between @StartDate And @EndDate
	And PO.LocationId = @LocationId
	order by Duration desc
END



GO
/****** Object:  StoredProcedure [dbo].[Pr_SCM_GetPurcaseOrderItem]    Script Date: 6/20/2017 10:35:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[Pr_SCM_GetPurcaseOrderItem] (                                        
@isPO int =0,                            
@UserID int,                        
@StoreID int =0  
)                                      
as                                          
                                          
begin
                                          
--0                                  
If(@isPO =1) begin
	Select	a.Drug_Pk	[ItemID]
		,	a.DrugName	[ItemName]
		,  a.ItemTypeID
		,	b.SupplierId
	From Mst_Drug a
	Inner Join lnk_supplierItem b On a.Drug_pk = b.ItemId and b.ItemTypeId=a.ItemTypeID
	Where a.DeleteFlag = 0
	Group By	a.Drug_Pk
			,	a.DrugName
			,	b.SupplierId
			,	a.ItemTypeID
	Order By a.DrugName Asc
End 
Else If (@isPO = 2) Begin
Select	convert(varchar(100), a.Drug_Pk) + '-' + convert(varchar(100), Stock.BatchId) + '-' + convert(varchar, Stock.ExpiryDate, 101) [ItemID],
		a.DrugName + ' - ' + Stock.BatchName + ' - ' + convert(varchar, Stock.ExpiryDate, 106) + ' - ' + Convert(varchar,Stock.AvailableQTY) ItemName,
		
		f.Name [DispensingUnit],
		f.Id [UnitId],
		isnull(Stock.BatchName, '') [Batch],
		 [BatchId],
		Stock.StoreID,
		a.Drug_Pk [StockItemID],
	
		Stock.[AvailableQTY] / a.QtyPerPurchaseUnit [AvailableQTY],
		replace(convert(varchar, Stock.ExpiryDate, 106), ' ', '-') [ExpiryDate]
From dbo.Mst_Drug a
	Inner Join
		(
			Select	sum(T.Quantity)	As AvailableQTY
				,	SI.StoreID
				,	T.ItemId		As Drug_pk
				,	T.BatchId
				,	T.ExpiryDate
				,	Store.Name		As StoreName
				,	Mst_Batch.Name	As BatchName
			From Dtl_StockTransaction As T
			Inner Join lnk_StoreItem As SI On T.ItemId = SI.ItemId
			Inner Join Mst_Store As Store On SI.StoreID = Store.Id
			Inner Join Mst_Batch On Mst_Batch.ID = T.BatchId
			Where (T.StoreId = @StoreId)
			Group By	SI.StoreID
					,	T.ItemId
					,	T.BatchId
					,	T.ExpiryDate
					,	Store.Name
					,	Mst_Batch.Name
			Having (sum(T.Quantity) > 0)
		) Stock On  Stock.Drug_pk = a.Drug_pk
	Left Outer Join Mst_DispensingUnit f On a.DispensingUnit = f.Id
	Where Stock.StoreId = @StoreID

Order By [ItemName]
End
--1                                    

Select	c.Drug_Pk,
		DrugId,
		c.ItemTypeID,
		c.DrugName [ItemName],
		dbo.fn_GetDrugGenericCommaSeprated(c.Drug_Pk) [GenericName],
		dbo.fn_Drug_Abbrev_Constella(c.Drug_Pk) [GenAbbr],
		c.FDACode,
		c.DispensingUnit,
		(
			Select
				name
			From Mst_DispensingUnit
			Where id = c.DispensingUnit
		)
		[DispensingunitName],
		c.MinStock,
		c.MaxStock,
		c.PurchaseUnit,
		(
			Select
				name
			From Mst_DispensingUnit
			Where id = c.PurchaseUnit
		)
		[PurchaseUnitName],
		c.QtyPerPurchaseUnit,
		isnull(c.PurchaseUnitPrice, 0) [PurchaseUnitPrice],
		c.Manufacturer,
		c.DispensingUnitPrice,
		c.DispensingMargin,
		isnull(c.SellingUnitPrice, 0) [SellingUnitPrice],
		c.EffectiveDate,
		c.DeleteFlag
From Mst_Drug c

---2           
If (@isPO = 1) Begin
Select	'' [ItemCode],
		'' [Units],
		'' [UnitQuantity],
		'' [OrderQuantity],
		'' [Price],
		'' [TotPrice],
		'' [Isfunded]
End Else If (@isPO = 2) Begin
Select	'' [ItemCode],
		'' [Units],
		'' [OrderQuantity],
		'' [Price],
		'' [TotPrice],
		'' [Isfunded],
		'' BatchID,
		'' BatchName,
		'' AvailableQTY,
		'' [ExpiryDate]
End
--3                            
If (@UserID = 1) Begin
Select	a.EmployeeID,
		rtrim(ltrim(a.FirstName)) + ' ' + rtrim(ltrim(a.LastName)) [EmpName]
From mst_employee a
--inner join mst_user b on a.EmployeeID =b.UserID                             
End Else Begin
Select	a.EmployeeID,
		rtrim(ltrim(a.FirstName)) + ' ' + rtrim(ltrim(a.LastName)) [EmpName]
From mst_employee a
	Inner Join mst_user b
		--on a.EmployeeID =b.UserID where b.userID=@UserID 
		On a.EmployeeID = b.EmployeeID
Where b.userID = @UserID;
End


--- 4                       
Select Distinct	c.Drug_Pk,
				c.DrugName [ItemName],
				c.ItemTypeID,
				Case
					When f.donorid > 0 Then 1
					Else 0
				End [Isfunded]
From Mst_Drug c
	Inner Join Lnk_ProgramItem e On e.ItemId = c.Drug_Pk
	Inner Join Lnk_DonorProgram f On f.ProgramId = e.ProgramId
		And convert(datetime, convert(varchar, getdate(), 106)) >= convert(datetime, convert(varchar, fundingstartdate, 106))
		And convert(datetime, convert(varchar, getdate(), 106)) <= convert(datetime, convert(varchar, FundingEndDate, 106))
Group By	c.Drug_Pk,
			c.DrugName,
			f.donorid,
			c.ItemTypeID

End



GO
/****** Object:  StoredProcedure [dbo].[pr_SystemAdmin_Backup_Constella]    Script Date: 6/20/2017 10:35:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[pr_SystemAdmin_Backup_Constella]                
@FileName varchar(500),      
@LocationId int,  
@Deidentified int,  
@dbKey varchar(50)  
                
as                
                
declare @InstanceName varchar(1000)               
declare @dir varchar(500)        
declare @Ver varchar(20)  
declare @Loc varchar(200)             
      
if @Deidentified = 1 Begin      
  
     declare @Tsql varchar(500)  
     set @Tsql = 'Open symmetric key Key_CTC decryption by password = '+@dbKey  
     exec(@Tsql)  
           
	Create Table #PtnMaster(Ptn_Pk int,FirstName varchar(200),MiddleName varchar(200),LastName varchar(200),Address varchar(200),Phone varchar(200))      
		Insert Into #PtnMaster (
				Ptn_Pk
			,	FirstName
			,	MiddleName
			,	LastName
			,	Address
			,	Phone)
		Select	Ptn_Pk
			,	decryptbykey(FirstName)
			,	decryptbykey(MiddleName)
			,	decryptbykey(LastName)
			,	decryptbykey(Address)
			,	decryptbykey(Phone)
		From mst_patient      
           
     Create Table #PtnContacts(Ptn_Pk int,GuardianName varchar(200),GuardianInformation varchar(200),EmergContactName varchar(200),EmergContactPhone varchar(200),      
     EmergContactAddress varchar(200),TenCellLeader varchar(200),TenCellLeaderAddress varchar(200),TreatmentSupportName varchar(100),CommunitySupportGroup varchar(200),  
     TreatmentSupportAddress varchar(200))       
  
 
		Insert Into #PtnContacts
		Select	Ptn_Pk
			,	decryptbykey(GuardianName)
			,	decryptbykey(GuardianInformation)
			,	EmergContactName
			,	EmergContactPhone
			,	EmergContactAddress
			,	decryptbykey(TenCellLeader)
			,	decryptbykey(TenCellLeaderAddress)
			,	TreatmentSupportName
			,	CommunitySupportGroup
			,	TreatmentSupportAddress
		From dtl_patientcontacts     
      
     Create Table #PtnRelations(Ptn_Pk int,RFirstName varchar(200),RLastName varchar(200), Id int)      
		Insert Into #PtnRelations
		Select	Ptn_Pk
			,	decryptbykey(RFirstName)
			,	decryptbykey(RLastName)
			,	id
		From dtl_FamilyInfo      
       
		Update mst_patient Set
				FirstName = encryptbykey(key_guid('Key_CTC'), 'FName')
			,	MiddleName = encryptbykey(key_guid('Key_CTC'), 'LName')
			,	LastName = encryptbykey(key_guid('Key_CTC'), 'LName')
			,	Address = encryptbykey(key_guid('Key_CTC'), 'Address')
			,	Phone = encryptbykey(key_guid('Key_CTC'), '')     
  
		Update dtl_patientcontacts Set
				GuardianName = encryptbykey(key_guid('Key_CTC'), 'GName')
			,	GuardianInformation = encryptbykey(key_guid('Key_CTC'), 'GInfo')
			,	EmergContactName = 'EContactName'
			,	EmergContactPhone = ''
			,	EmergContactAddress = 'EAddress'
			,	TenCellLeader = encryptbykey(key_guid('Key_CTC'), 'TCellLeader')
			,	TenCellLeaderAddress = encryptbykey(key_guid('Key_CTC'), 'TCellLeaderAdd')
			,	TreatmentSupportName = 'TSuppName'
			,	CommunitySupportGroup = 'ComSuppGroup'
			,	TreatmentSupportAddress = 'TSuppAddress'       
  
		Update dtl_FamilyInfo Set
				RFirstName = encryptbykey(key_guid('Key_CTC'), 'RFName')
			,	RLastName = encryptbykey(key_guid('Key_CTC'), 'RLName')      
       
	--Update mst_Employee Set
	--		LastName = 'LName'
	--	,	FirstName = 'FName'

	--Update mst_User Set
	--		UserLastName = 'LName'
	--	,	UserFirstName = 'FName'
    
  End      
  
set @Loc = ''  
        
select @Ver = isnull(appver,'') from appadmin where id = 1  
if @LocationId > 0  
    select @Loc = isnull(FacilityName,'') from mst_facility where facilityid = @LocationId  
          
set @dir = 'md ' + @filename              
exec xp_cmdshell @dir              
  
set @dir = 'EXECUTE master.dbo.xp_delete_file 0,N'''+ @FileName+''',N''*.bak'',N'''+convert(varchar,dateadd(dd,-14,getdate()),106)+''''  
exec(@dir)  
      
if @Deidentified = 1   Begin                
     set @FileName = @FileName + '\IQCare-Deidentified-'+ @Ver + ' ' +@Loc+ ' ' + convert(varchar,getdate(),23)+'.bak'               
     print(@FileName)    
     set @InstanceName = 'iqcare_backup_Deidt' + @Loc + convert(varchar,getdate(),23)                
 end              
else  begin      
     set @FileName = @FileName + '\IQCare-'+ @Ver + ' ' + @Loc + ' ' + convert(varchar,getdate(),23) +'.bak'                
     set @InstanceName = 'iqcare_backup' +@Loc+convert(varchar,getdate(),23)                
 end     
  
declare @dbname varchar(100)  
select @dbname= db_name()  
set @TSQL = 'BACKUP DATABASE ['+@dbname+'] TO  DISK = '''+@FileName+''' WITH NOFORMAT, NOINIT,  NAME = '''+@InstanceName+''', SKIP, REWIND, NOUNLOAD, STATS = 10'  
exec(@TSQL)  
      
if @Deidentified = 1   begin      
      
		Update mst_patient Set
				mst_patient.FirstName = encryptbykey(key_guid('Key_CTC'), b.FirstName)
			,	mst_patient.MiddleName = encryptbykey(key_guid('Key_CTC'), b.MiddleName)
			,	mst_patient.LastName = encryptbykey(key_guid('Key_CTC'), b.LastName)
			,	mst_patient.Address = encryptbykey(key_guid('Key_CTC'), b.Address)
			,	mst_patient.Phone = encryptbykey(key_guid('Key_CTC'), b.Phone)
		From #PtnMaster b
		Where mst_patient.Ptn_Pk = b.Ptn_Pk      
      
 

		Update dtl_patientcontacts Set
				dtl_patientcontacts.GuardianName = encryptbykey(key_guid('Key_CTC'), b.GuardianName)
			,	dtl_patientcontacts.GuardianInformation = encryptbykey(key_guid('Key_CTC'), b.GuardianInformation)
			,	dtl_patientcontacts.EmergContactName = b.EmergContactName
			,	dtl_patientcontacts.EmergContactPhone = b.EmergContactPhone
			,	dtl_patientcontacts.EmergContactAddress = b.EmergContactAddress
			,	dtl_patientcontacts.TenCellLeader = encryptbykey(key_guid('Key_CTC'), b.TenCellLeader)
			,	dtl_patientcontacts.TenCellLeaderAddress = encryptbykey(key_guid('Key_CTC'), b.TenCellLeaderAddress)
			,	dtl_patientcontacts.Treatmentsupportname = b.TreatmentSupportName
			,	dtl_patientcontacts.CommunitySupportGroup = b.CommunitySupportGroup
			,	dtl_patientcontacts.TreatmentSupportAddress = b.TreatmentSupportAddress
		From #PtnContacts b
		Where dtl_patientcontacts.ptn_pk = b.ptn_pk   
        
		Update dtl_FamilyInfo Set
				dtl_FamilyInfo.RFirstName = encryptbykey(key_guid('Key_CTC'), b.RFirstName)
			,	dtl_FamilyInfo.RLastName = encryptbykey(key_guid('Key_CTC'), b.RLastName)
		From #PtnRelations b
		Where dtl_FamilyInfo.ptn_pk = b.ptn_pk
		And dtl_familyinfo.Id = b.Id      
      
     Drop Table #PtnMaster      
     Drop Table #PtnContacts      
     Drop Table #PtnRelations      
     Close symmetric key Key_CTC  
  
  end


GO
/****** Object:  StoredProcedure [dbo].[SP_Bluecard_ToGreenCard]    Script Date: 6/20/2017 10:35:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<felix/stephen>
-- Create date: <03-22-2017>
-- Description:	<Patient registration migration from bluecard to greencard>
-- =============================================
CREATE PROCEDURE [dbo].[SP_Bluecard_ToGreenCard]
	-- Add the parameters for the stored procedure here
	@ptn_pk int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DECLARE @FirstName varbinary(max), @MiddleName varbinary(max), @LastName varbinary(max), @Sex int, @Status bit , @DeleteFlag bit, 
		@CreateDate datetime, @UserID int,  @message varchar(80), @Id int, @PatientFacilityId varchar(50), @PatientType int, 
		@FacilityId varchar(10), @DateOfBirth datetime, @DobPrecision int, @NationalId varbinary(max), @PatientId int,  
		@ARTStartDate date,@transferIn int,@CCCNumber varchar(20), @entryPoint int, @ReferredFrom int, @RegistrationDate datetime,
		@MaritalStatusId int, @MaritalStatus int, @DistrictName varchar(50), @CountyID int, @SubCountyID int, @WardID int,
		@Address varbinary(max), @Phone varbinary(max), @EnrollmentId int, @PatientIdentifierId int, @ServiceEntryPointId int,
		@PatientMaritalStatusID int, @PatientTreatmentSupporterID int, @PersonContactID int, @IDNational varbinary(max);

DECLARE @FirstNameT varchar(50), @LastNameT varchar(50), @TreatmentSupportTelNumber varbinary(max), 
			@CreateDateT datetime, @UserIDT int, @IDT int;
  
PRINT '-------- Patients Report --------'; 
SELECT @message = '----- ptn_pk ' + CAST(@ptn_pk as varchar(50));
PRINT @message;
  
DECLARE mstPatient_cursor CURSOR FOR   
SELECT FirstName, MiddleName ,LastName,Sex, [Status], DeleteFlag, dbo.mst_Patient.CreateDate, dbo.mst_Patient.UserID, PatientFacilityId, PosId, DOB, DobPrecision, [ID/PassportNo],PatientEnrollmentID, [ReferredFrom], [RegistrationDate], MaritalStatus, DistrictName, Address, Phone
FROM mst_Patient
INNER JOIN
 dbo.Lnk_PatientProgramStart ON dbo.mst_Patient.Ptn_Pk = dbo.Lnk_PatientProgramStart.Ptn_pk
WHERE        (dbo.Lnk_PatientProgramStart.ModuleId = 203) AND dbo.mst_Patient.Ptn_Pk = @ptn_pk
  
OPEN mstPatient_cursor  
  
FETCH NEXT FROM mstPatient_cursor   
INTO @FirstName, @MiddleName, @LastName, @Sex, @Status, @DeleteFlag, @CreateDate, @UserID, @PatientFacilityId, @FacilityId, @DateOfBirth, @DobPrecision, @NationalId,@CCCNumber, @ReferredFrom, @RegistrationDate, @MaritalStatus , @DistrictName, @Address, @Phone
  
WHILE @@FETCH_STATUS = 0  
BEGIN  
    PRINT ' '  
    SELECT @message = '----- patients From mst_patient: ' + CAST(@ptn_pk as varchar(50))
  
    PRINT @message  

	exec pr_OpenDecryptedSession;

	--set null dates
	IF @CreateDate IS NULL
		BEGIN
			SELECT @CreateDate = getdate();
		END
	--Due to the logic of green card
	IF @Status = 1
		BEGIN
			SELECT @Status = 0
		END
	ELSE
		BEGIN
			SELECT @Status = 1
		END

	IF @NationalId IS NULL
		SET @NationalId = 99999999;

	IF @Sex IS NOT NULL
		SET @Sex = (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName like '%gender%' and ItemName like (select Name from mst_Decode where id = @Sex) + '%');
	ELSE
		SET @Sex = (select top 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');

	--Default all persons to new
	SET @ARTStartDate=( SELECT top 1 ARTTransferInDate FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk=@ptn_pk);
	if(@ARTStartDate Is NULL) BEGIN SET @PatientType=(SELECT Id FROM LookupItem WHERE Name='New');SET @transferIn=0; END ELSE BEGIN SET @PatientType=(SELECT Id FROM LookupItem WHERE Name='TransferIn');SET @transferIn=1; END
	-- SELECT @PatientType = 1285

	--encrypt nationalid
	SET @IDNational=ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@NationalId);

	IF NOT EXISTS ( SELECT TOP 1 ptn_pk FROM Patient WHERE ptn_pk = @ptn_pk)
		BEGIN
			Insert into Person(FirstName, MidName, LastName, Sex, Active, DeleteFlag, CreateDate, CreatedBy)
			Values(@FirstName, @MiddleName, @LastName, @Sex, @Status, @DeleteFlag, @CreateDate, @UserID);

			SELECT @Id=@@IDENTITY;
			SELECT @message = 'Created Person Id: ' + CAST(@Id as varchar(50));
			PRINT @message;

			Insert into Patient(ptn_pk, PersonId, PatientIndex, PatientType, FacilityId, Active, DateOfBirth, DobPrecision, NationalId, DeleteFlag, CreatedBy, CreateDate, RegistrationDate)
			Values(@ptn_pk, @Id, @PatientFacilityId, @PatientType, @FacilityId, @Status, @DateOfBirth, @DobPrecision, @IDNational, @DeleteFlag, @UserID, @CreateDate, @RegistrationDate);

			SELECT @PatientId=@@IDENTITY;
			SELECT @message = 'Created Patient Id: ' + CAST(@PatientId as varchar);
			PRINT @message;

			-- Insert to PatientEnrollment
			INSERT INTO [dbo].[PatientEnrollment] ([PatientId] ,[ServiceAreaId] ,[EnrollmentDate] ,[EnrollmentStatusId] ,[TransferIn] ,[CareEnded] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[AuditData])
			VALUES (@PatientId,1,(SELECT top 1 StartDate FROM Lnk_PatientProgramStart WHERE Ptn_pk=@ptn_pk),0, @transferIn,0 ,0 ,@UserID ,@CreateDate ,NULL)
		
			SELECT @EnrollmentId=@@IDENTITY;
			SELECT @message = 'Created PatientEnrollment Id: ' + CAST(@EnrollmentId as varchar);
			PRINT @message;

			IF @CCCNumber IS NOT NULL
				BEGIN
					-- Patient Identifier
					INSERT INTO [dbo].[PatientIdentifier] ([PatientId], [PatientEnrollmentId], [IdentifierTypeId], [IdentifierValue] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[Active] ,[AuditData])
					VALUES (@PatientId , @EnrollmentId ,(SELECT Id FROM LookupItem WHERE Name='CCCNumber') ,@CCCNumber ,0 ,@UserID ,@CreateDate ,0 ,NULL);

					SELECT @PatientIdentifierId=@@IDENTITY;
					SELECT @message = 'Created PatientIdentifier Id: ' + CAST(@PatientIdentifierId as varchar);
					PRINT @message;
				END

			--Insert into ServiceEntryPoint
			IF @ReferredFrom > 0
				SET @entryPoint = (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (SELECT Name FROM mst_Decode WHERE ID=@ReferredFrom AND CodeID=17) + '%');
				IF @entryPoint IS NULL
					BEGIN
						SET @entryPoint = (select top 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
					END
			ELSE
				SET @entryPoint = (select top 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');

			INSERT INTO ServiceEntryPoint([PatientId], [ServiceAreaId], [EntryPointId], [DeleteFlag], [CreatedBy], [CreateDate], [Active])
			VALUES(@PatientId, 1, isnull(@entryPoint,0), 0 , @UserID, @CreateDate, 0);

			SELECT @ServiceEntryPointId=@@IDENTITY;
			SELECT @message = 'Created ServiceEntryPoint Id: ' + CAST(@ServiceEntryPointId as varchar);
			PRINT @message;
	
			--Insert into MaritalStatus
			IF @MaritalStatus > 0
				BEGIN
					IF EXISTS (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (select Name from mst_Decode where ID = @MaritalStatus and CodeID = 12) + '%')
						SET @MaritalStatusId = (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (select Name from mst_Decode where ID = @MaritalStatus and CodeID = 12) + '%');
					ELSE
						SET @MaritalStatusId = (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
				END
			ELSE
				SET @MaritalStatusId = (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');

			INSERT INTO PatientMaritalStatus(PersonId, MaritalStatusId, Active, DeleteFlag, CreatedBy, CreateDate)
			VALUES(@Id, @MaritalStatusId, 1, 0, @UserID, @CreateDate);

			SELECT @PatientMaritalStatusID=@@IDENTITY;
			SELECT @message = 'Created PatientMaritalStatus Id: ' + CAST(@PatientMaritalStatusID as varchar);
			PRINT @message;

			--Insert into PersonLocation
			--SET @CountyID = (SELECT TOP 1 CountyId from County where CountyName like '%' + @DistrictName  + '%');
			--SET @WardID = (SELECT TOP 1 WardId FROM County WHERE WardName LIKE '%' +  +'%')

			--INSERT INTO PersonLocation(PersonId, County, SubCounty, Ward, Village, Location, SubLocation, LandMark, NearestHealthCentre, Active, DeleteFlag, CreatedBy, CreateDate)
			--VALUES(@Id, @CountyID, @SubCountyID, @WardID, @Village, @Location, @SubLocation, @LandMark, @NearestHealthCentre, 1, @DeleteFlag, @UserID, @CreateDate);
    
			--Insert into Treatment Supporter
			DECLARE Treatment_Supporter_cursor CURSOR FOR
			SELECT SUBSTRING(TreatmentSupporterName,0,charindex(',',TreatmentSupporterName))as firstname ,
			SUBSTRING(TreatmentSupporterName,charindex(',',TreatmentSupporterName) + 1,len(TreatmentSupporterName)+1)as lastname,
			TreatmentSupportTelNumber, CreateDate, UserID
			from dtl_PatientContacts WHERE ptn_pk = @ptn_pk;


			OPEN Treatment_Supporter_cursor
			FETCH NEXT FROM Treatment_Supporter_cursor INTO @FirstNameT, @LastNameT, @TreatmentSupportTelNumber, @CreateDateT , @UserIDT

			IF @@FETCH_STATUS <> 0   
				PRINT '         <<None>>'       

			WHILE @@FETCH_STATUS = 0  
			BEGIN  

				--SELECT @message = '         ' + @product  
				--PRINT @message
				SET @TreatmentSupportTelNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber);
				IF @FirstNameT IS NOT NULL AND @LastNameT IS NOT NULL 
					BEGIN
						Insert into Person(FirstName, MidName, LastName, Sex, Active, DeleteFlag, CreateDate, CreatedBy)
						Values(ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstNameT), NULL, ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastNameT), (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown'), 1, 0, @CreateDateT, @UserIDT);

						SELECT @IDT=@@IDENTITY;
						SELECT @message = 'Created Person Treatment Supporter Id: ' + CAST(@IDT as varchar(50));
						PRINT @message;

						IF @TreatmentSupportTelNumber IS NOT NULL
						 SET @TreatmentSupportTelNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber)

						INSERT INTO PatientTreatmentSupporter(PersonId, [SupporterId], [MobileContact], [DeleteFlag], [CreatedBy], [CreateDate])
						VALUES(@Id, @IDT, @TreatmentSupportTelNumber, 0, @UserIDT, @CreateDateT);

						SELECT @PatientTreatmentSupporterID=@@IDENTITY;
						SELECT @message = 'Created PatientTreatmentSupporterID Id: ' + CAST(@PatientTreatmentSupporterID as varchar);
						PRINT @message;
					END

				FETCH NEXT FROM Treatment_Supporter_cursor INTO  @FirstNameT, @LastNameT, @TreatmentSupportTelNumber, @CreateDateT, @UserIDT
				END  

			CLOSE Treatment_Supporter_cursor  
			DEALLOCATE Treatment_Supporter_cursor

			--Insert into Person Contact
			IF @Address IS NOT NULL AND @Phone IS NOT NULL
				BEGIN
					INSERT INTO PersonContact(PersonId, [PhysicalAddress], [MobileNumber], [AlternativeNumber], [EmailAddress], [Active], [DeleteFlag], [CreatedBy], [CreateDate])
					VALUES(@Id, @Address, @Phone, null, null, @Status, 0, @UserID, @CreateDate);

					SELECT @PersonContactID=@@IDENTITY;
					SELECT @message = 'Created PersonContact Id: ' + CAST(@PersonContactID as varchar);
					PRINT @message;
				END
		END
	ELSE
		BEGIN
			SET @Id = (SELECT TOP 1 PersonId FROM Patient WHERE ptn_pk = @ptn_pk);
			UPDATE Person
			SET FirstName = @FirstName, MidName = @MiddleName, LastName = @LastName, Sex = @Sex, Active = @Status, DeleteFlag = @DeleteFlag, CreateDate = @CreateDate, CreatedBy = @UserID
			WHERE Id = @Id;

			SELECT @message = 'Update Person Id: ' + CAST(@Id as varchar(50));
			PRINT @message;

			PRINT @Status;

			UPDATE Patient
			SET PatientIndex = @PatientFacilityId, PatientType = @PatientType, FacilityId = @FacilityId, Active = @Status, DateOfBirth = @DateOfBirth, DobPrecision = @DobPrecision, NationalId = @IDNational, DeleteFlag = @DeleteFlag, CreatedBy = @UserID, CreateDate = @CreateDate, RegistrationDate = @RegistrationDate
			WHERE PersonId = @Id;

			SELECT @PatientId=(SELECT TOP 1 Id FROM Patient WHERE PersonId = @Id);
			SELECT @message = 'Updated Patient';
			PRINT @message;

			-- UPDATE to PatientEnrollment
			UPDATE PatientEnrollment
			SET EnrollmentDate = (SELECT top 1 StartDate FROM Lnk_PatientProgramStart WHERE Ptn_pk=@ptn_pk), EnrollmentStatusId = 0, TransferIn = @transferIn, CareEnded = 0, DeleteFlag = 0, CreatedBy = @UserID, CreateDate = @CreateDate
			WHERE PatientId = @PatientId;

			SELECT @EnrollmentId = (SELECT TOP 1 Id FROM PatientEnrollment WHERE PatientId = @PatientId);
			SELECT @message = 'Updated PatientEnrollment Id: ' + CAST(@EnrollmentId as varchar);
			PRINT @message;

			BEGIN
			IF @CCCNumber IS NOT NULL
				BEGIN
				IF NOT EXISTS ( SELECT PatientId FROM PatientIdentifier WHERE PatientId = @PatientId AND PatientEnrollmentId = @EnrollmentId AND IdentifierTypeId = (SELECT Id FROM LookupItem WHERE Name='CCCNumber'))
					BEGIN
						-- Patient Identifier
						INSERT INTO [dbo].[PatientIdentifier] ([PatientId], [PatientEnrollmentId], [IdentifierTypeId], [IdentifierValue] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[Active] ,[AuditData])
						VALUES (@PatientId , @EnrollmentId ,(SELECT Id FROM LookupItem WHERE Name='CCCNumber') ,@CCCNumber ,0 ,@UserID ,@CreateDate ,0 ,NULL);

						SELECT @PatientIdentifierId=@@IDENTITY;
						SELECT @message = 'Created PatientIdentifier Id: ' + CAST(@PatientIdentifierId as varchar);
						PRINT @message;
					END
				ELSE
					BEGIN
						UPDATE PatientIdentifier
						SET IdentifierTypeId = (SELECT Id FROM LookupItem WHERE Name='CCCNumber'), IdentifierValue = @CCCNumber, DeleteFlag = 0, CreatedBy = @UserID, CreateDate = @CreateDate, Active = 0
						WHERE PatientId = @PatientId AND PatientEnrollmentId = @EnrollmentId AND IdentifierTypeId = (SELECT Id FROM LookupItem WHERE Name='CCCNumber')
					END
				END
			END

			--Insert into ServiceEntryPoint
			IF @ReferredFrom > 0
				BEGIN
					SET @entryPoint = (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (SELECT Name FROM mst_Decode WHERE ID=@ReferredFrom AND CodeID=17) + '%');
				
					UPDATE ServiceEntryPoint
					SET EntryPointId = isnull(@entryPoint,0), CreatedBy = @UserID, CreateDate = @CreateDate
					WHERE PatientId = @PatientId;
					
					SELECT @ServiceEntryPointId=@@IDENTITY;
					SELECT @message = 'Updated ServiceEntryPoint Id: ' + CAST(@ServiceEntryPointId as varchar);
					PRINT @message;
				END
	
			--Updated into MaritalStatus
			IF @MaritalStatus > 0
				BEGIN
					BEGIN
						IF EXISTS (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (select Name from mst_Decode where ID = @MaritalStatus and CodeID = 12) + '%')
							SET @MaritalStatusId = (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (select Name from mst_Decode where ID = @MaritalStatus and CodeID = 12) + '%');
						ELSE
							SET @MaritalStatusId = (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
					END
					UPDATE PatientMaritalStatus
					SET MaritalStatusId = @MaritalStatusId, CreatedBy = @UserID, CreateDate = @CreateDate
					WHERE PersonId = @Id;

					SELECT @PatientMaritalStatusID=@@IDENTITY;
					SELECT @message = 'Updated PatientMaritalStatus Id: ' + CAST(@PatientMaritalStatusID as varchar);
					PRINT @message;
				END

			--Update into Treatment Supporter
			DECLARE Treatment_Supporter_cursor CURSOR FOR
			SELECT SUBSTRING(TreatmentSupporterName,0,charindex(',',TreatmentSupporterName))as firstname ,
			SUBSTRING(TreatmentSupporterName,charindex(',',TreatmentSupporterName) + 1,len(TreatmentSupporterName)+1)as lastname,
			TreatmentSupportTelNumber, CreateDate, UserID
			from dtl_PatientContacts WHERE ptn_pk = @ptn_pk;


			OPEN Treatment_Supporter_cursor
			FETCH NEXT FROM Treatment_Supporter_cursor INTO @FirstNameT, @LastNameT, @TreatmentSupportTelNumber, @CreateDateT , @UserIDT

			IF @@FETCH_STATUS <> 0   
				PRINT '         <<None>>'       

			WHILE @@FETCH_STATUS = 0  
			BEGIN

				SET @TreatmentSupportTelNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber);
				IF @FirstNameT IS NOT NULL AND @LastNameT IS NOT NULL
					BEGIN
						IF NOT EXISTS (SELECT PersonId FROM PatientTreatmentSupporter WHERE PersonId = @Id)
							BEGIN
								Insert into Person(FirstName, MidName, LastName, Sex, Active, DeleteFlag, CreateDate, CreatedBy)
								Values(ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstNameT), NULL, ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastNameT), (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown'), 1, 0, @CreateDateT, @UserIDT);

								SELECT @IDT=@@IDENTITY;
								SELECT @message = 'Created Person Treatment Supporter Id: ' + CAST(@IDT as varchar(50));
								PRINT @message;

								IF @TreatmentSupportTelNumber IS NOT NULL
								SET @TreatmentSupportTelNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber)

								INSERT INTO PatientTreatmentSupporter(PersonId, [SupporterId], [MobileContact], [DeleteFlag], [CreatedBy], [CreateDate])
								VALUES(@Id, @IDT, @TreatmentSupportTelNumber, 0, @UserIDT, @CreateDateT);

							END
						ELSE
							BEGIN
								SET @IDT = (SELECT SupporterId FROM PatientTreatmentSupporter WHERE PersonId = @Id);

								UPDATE Person
								SET FirstName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstNameT), LastName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastNameT)
								WHERE Id = @IDT;

								IF @TreatmentSupportTelNumber IS NOT NULL
								SET @TreatmentSupportTelNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber)

								UPDATE PatientTreatmentSupporter
								SET MobileContact = @TreatmentSupportTelNumber
								WHERE PersonId = @Id;

							END
						END

				FETCH NEXT FROM Treatment_Supporter_cursor INTO  @FirstNameT, @LastNameT, @TreatmentSupportTelNumber, @CreateDateT, @UserIDT
				END  

			CLOSE Treatment_Supporter_cursor  
			DEALLOCATE Treatment_Supporter_cursor

			--UPDATE into Person Contact
			IF @Address IS NOT NULL AND @Phone IS NOT NULL
				BEGIN
					UPDATE PersonContact
					SET PhysicalAddress = @Address, MobileNumber = @Phone
					WHERE PersonId = @Id;
				END

		END

    -- Get the next mst_patient.
    FETCH NEXT FROM mstPatient_cursor   
    INTO @FirstName, @MiddleName, @LastName, @Sex, @Status, @DeleteFlag, @CreateDate, @UserID, @PatientFacilityId, @FacilityId, @DateOfBirth, @DobPrecision, @NationalId, @CCCNumber, @ReferredFrom, @RegistrationDate, @MaritalStatus , @DistrictName, @Address, @Phone
END   
CLOSE mstPatient_cursor;  
DEALLOCATE mstPatient_cursor;  
END


GO
/****** Object:  StoredProcedure [dbo].[sp_getPharmacyPatientsExpected]    Script Date: 6/20/2017 10:35:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 14th Feb 2017
-- Description:	get number of patients expected to come for pharmacy
-- =============================================
CREATE PROCEDURE [dbo].[sp_getPharmacyPatientsExpected]
	-- Add the parameters for the stored procedure here
	@Date datetime

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
	declare @pharmacyReason int = (select top 1 id from mst_decode where name = 'Pharmacy Refill')
	--0 expected
	select count(*) expected from dtl_patientappointment 
	--where appreason = @pharmacyReason and (deleteflag = 0 or deleteflag is null) and cast(AppDate as date) = cast(@Date as date)
	where (deleteflag = 0 or deleteflag is null) and cast(AppDate as date) = cast(@Date as date)

	--1 Actual
	select count(*) actual  from ord_patientpharmacyorder 
	where (deleteflag=0 or deleteflag is null) and cast(dispensedbydate as date) = cast(@Date as date)
	
End









GO
