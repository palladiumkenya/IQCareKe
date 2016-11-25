IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_SystemAdmin_Backup_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_SystemAdmin_Backup_Constella]
GO


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
       
	Update mst_Employee Set
			LastName = 'LName'
		,	FirstName = 'FName'

	Update mst_User Set
			UserLastName = 'LName'
		,	UserFirstName = 'FName'
    
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


