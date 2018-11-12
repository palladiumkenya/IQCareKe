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
@dbKey varchar(50)  = null
                
as                
                
declare @InstanceName varchar(1000)               
declare @dir varchar(500)        
declare @Ver varchar(20)  
declare @Loc varchar(200)             
declare @Tsql varchar(500)      
If @Deidentified = 1 Begin      
  
     --
     --set @Tsql = 'Open symmetric key Key_CTC decryption by password = '+@dbKey  
     --exec(@Tsql)  
           
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

	Create Table #MstEmployee(Id int, FName varchar(50), LName varchar(50));

	Insert Into #MstEmployee (
			Id
		  , FName
		  , Lname)
	Select EmployeeId
		 , FirstName
		 , LastName
	From mst_Employee

	Create Table #MstUser(Id int, FName varchar(50), LName varchar(50));
	Insert Into #MstUser (
			Id
		  , FName
		  , Lname)
	Select UserId
		 , UserFirstName
		 , UserLastName
	From Mst_User   

	--Greencard
	Create Table #PersonMaster(Id int,FirstName varchar(200),MidName varchar(200),LastName varchar(200))  
	Insert Into #PersonMaster (
			Id
		  , FirstName
		  , MidName
		  , LastName)
	Select Id
		 , decryptbykey(FirstName)
		 , decryptbykey(MidName)
		 , decryptbykey(LastName)
	From Person;

	Create Table #PersonContact(Id int,PhysicalAddress varchar(200),MobileNumber varchar(50),AlternativeNumber varchar(50), EmailAddress varchar(50))  
	Insert Into #PersonContact (
			Id
			, PhysicalAddress
			, MobileNumber
			, AlternativeNumber
			, EmailAddress)
	Select Id
			, decryptbykey(PhysicalAddress)
			, decryptbykey(MobileNumber)
			, decryptbykey(AlternativeNumber)
			, decryptbykey(EmailAddress)
	From PersonContact

	Create Table #PLocation(Id int, Village varchar(250), Location varchar(250), subLocation varchar(250), Landmark varchar(250), NearestHC varchar(250));
	Insert Into #PLocation (
			Id
		  , Village
		  , Location
		  , subLocation
		  , Landmark
		  , NearestHC)
	Select Id
		 , Village
		 , Location
		 , SubLocation
		 , LandMark
		 , NearestHealthCentre
	From PersonLocation

		Update mst_patient Set
				FirstName = encryptbykey(key_guid('Key_CTC'), (select Random_String from vw_GenNewId))
			,	MiddleName = encryptbykey(key_guid('Key_CTC'), (select Random_String from vw_GenNewId))
			,	LastName = encryptbykey(key_guid('Key_CTC'), (select Random_String from vw_GenNewId))
			,	Address = encryptbykey(key_guid('Key_CTC'), (select Random_String from vw_GenNewId))
			,	Phone = encryptbykey(key_guid('Key_CTC'), (select Random_String from vw_GenNewId))     
  
		Update dtl_patientcontacts Set
				GuardianName = encryptbykey(key_guid('Key_CTC'), (select Random_String from vw_GenNewId))
			,	GuardianInformation = encryptbykey(key_guid('Key_CTC'), (select Random_String from vw_GenNewId))
			,	EmergContactName = (select Random_String from vw_GenNewId)
			,	EmergContactPhone = (select Random_String from vw_GenNewId)
			,	EmergContactAddress = (select Random_String from vw_GenNewId)
			,	TenCellLeader = encryptbykey(key_guid('Key_CTC'), (select Random_String from vw_GenNewId))
			,	TenCellLeaderAddress = encryptbykey(key_guid('Key_CTC'), (select Random_String from vw_GenNewId))
			,	TreatmentSupportName = (select Random_String from vw_GenNewId)
			,	CommunitySupportGroup = (select Random_String from vw_GenNewId)
			,	TreatmentSupportAddress = (select Random_String from vw_GenNewId)       
  
		Update dtl_FamilyInfo Set
				RFirstName = encryptbykey(key_guid('Key_CTC'), (select Random_String from vw_GenNewId))
			,	RLastName = encryptbykey(key_guid('Key_CTC'), (select Random_String from vw_GenNewId))      
       
	Update mst_Employee Set
			LastName = (select Random_String from vw_GenNewId)
		,	FirstName = (select Random_String from vw_GenNewId)

	Update mst_User Set
			UserLastName = (select Random_String from vw_GenNewId)
		,	UserFirstName = (select Random_String from vw_GenNewId)

	Update Person Set
		FirstName =encryptbykey(key_guid('Key_CTC'),(select Random_String from vw_GenNewId)),
		MidName = encryptbykey(key_guid('Key_CTC'),(select Random_String from vw_GenNewId)),
		LastName= encryptbykey(key_guid('Key_CTC'),(select Random_String from vw_GenNewId))

	Update PersonContact Set
		PhysicalAddress = encryptbykey(key_guid('Key_CTC'), (select Random_String from vw_GenNewId)),
		MobileNumber= encryptbykey(key_guid('Key_CTC'), (select Random_String from vw_GenNewId)),
		AlternativeNumber =encryptbykey(key_guid('Key_CTC'), (select Random_String from vw_GenNewId)),
		EmailAddress=encryptbykey(key_guid('Key_CTC'), (select Random_String from vw_GenNewId))

	Update PersonLocation Set
		Location= (select Random_String from vw_GenNewId),
		Village= (select Random_String from vw_GenNewId),
		SubCounty= (select Random_String from vw_GenNewId),
		LandMark= (select Random_String from vw_GenNewId),
		NearestHealthCentre=(select Random_String from vw_GenNewId)

    
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
     set @InstanceName = 'iqcare_backup_Deidt' + @Loc + convert(varchar,getdate(),23)                
 end              
Else  begin      
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
		
		Update E Set
				FirstName = M.FName
			  , LastName = M.LName
		From mst_Employee E
		Inner Join #MstEmployee M On E.EmployeeID = M.Id 

		Update U Set
				UserFirstName = M.FName
			  , UserLastName = M.LName
		From mst_User U
		Inner Join #MstUser M On U.UserID = M.Id;

		Update P Set
				FirstName = encryptbykey(key_guid('Key_CTC'), M.FirstName)
			  , MidName = encryptbykey(key_guid('Key_CTC'), M.MidName)
			  , LastName = encryptbykey(key_guid('Key_CTC'), M.LastName)
		From Person P
		Inner Join #PersonMaster M On P.Id = M.Id

		Update L Set
				Village = M.Village
			  , SubLocation = M.subLocation
			  , Location = M.Location
			  , NearestHealthCentre = M.NearestHC
			  , LandMark = M.Landmark
		From PersonLocation L
		Inner Join #PLocation M On L.Id = M.Id

		Update PC Set
				PhysicalAddress = encryptbykey(key_guid('Key_CTC'), M.PhysicalAddress)
			  , MobileNumber = encryptbykey(key_guid('Key_CTC'), M.MobileNumber)
			  , AlternativeNumber = encryptbykey(key_guid('Key_CTC'), M.AlternativeNumber)
			  , EmailAddress = encryptbykey(key_guid('Key_CTC'), M.EmailAddress)
		From PersonContact PC
		Inner Join #PersonContact M On PC.Id = M.Id
      
     Drop Table #PtnMaster      
     Drop Table #PtnContacts      
     Drop Table #PtnRelations   
	 Drop Table #MstEmployee
	 Drop Table #MstUser
	 Drop Table #PersonContact
	 Drop Table #PLocation
	 Drop Table #PersonMaster   
     Close symmetric key Key_CTC  
  
  end


GO


