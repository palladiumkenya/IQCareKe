IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_Admin_UpdateBackupSetup_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_Admin_UpdateBackupSetup_Constella]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[Pr_Admin_UpdateBackupSetup_Constella]    
@BackupDrive varchar(10),    
@BackUpTime datetime    
    
as    
    
begin
   if (@BackUpTime <> '1900-01-01' and @BackupTime Is Not Null)begin
	Update mst_Facility Set
			BackupDrive = @BackupDrive
		,	BackupTime = @BackUpTime
	Where DeleteFlag = 0;
	Update ScheduledTask Set
			NextRunDate = @BackupTime
		,	Active = 1
	Where TaskName = 'Database.Backup'
	End
	Else Begin
		Update mst_Facility Set
				BackupDrive = @BackupDrive
			,	BackupTime = Null
		Where DeleteFlag = 0;
		Update ScheduledTask Set
				NextRunDate = Null
			,	Active = 0
		Where TaskName = 'Database.Backup'
	End
End

Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_Admin_GetBackupSetup_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_Admin_GetBackupSetup_Constella]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[Pr_Admin_GetBackupSetup_Constella]

as

begin
   select top 1 BackupDrive,BackupTime from mst_Facility where DeleteFlag =0 
end

Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_SystemAdmin_Backup_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_SystemAdmin_Backup_Constella]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[pr_SystemAdmin_Backup_Constella]     (           
	@FileName varchar(50),      
	@LocationId int,  
	@Deidentified int,  
	@dbKey varchar(50)  = null
)
                
as Begin
	Set Nocount On
		declare @InstanceName varchar(100) , @dbname varchar(100)  ,@dirSQL varchar(500)   , @Ver varchar(20) , @Loc varchar(200) , @Tsql varchar(max)  ;

	declare  @db_path varchar(200), @deidentified_filename varchar(200), @deidentified_instance varchar(200),@facility_name varchar(50) ,@backup_path varchar(50) ,@normal_filename varchar(150) ;

	Select @db_path = physical_name	From sys.master_files	Where database_id = db_id(db_name()) And type_desc = 'ROWS';

	Set @db_path = reverse(right(reverse(@db_path), (len(@db_path) - charindex('\', reverse(@db_path), 1)) + 1));

	Select @facility_name = ''	 , @backup_path = @FileName + '\';

	Select @Ver = isnull(appver, '') From AppAdmin Where Id = 1;

	Select @dbname = db_name();

	If @LocationId > 0 Begin
		Select @facility_name = isnull(FacilityName, '') From mst_facility	Where facilityid = @LocationId
	End
	Else Begin
		Select Top 1 @facility_name = isnull(FacilityName, '')	From mst_facility	Where DeleteFlag = 0
	End

	Set @dirSQL = 'md ' + @backup_path;
	Exec xp_cmdshell @dirSQL

	Set @dirSQL = 'EXECUTE master.dbo.xp_delete_file 0,N''' + @backup_path + ''',N''*.bak'',N''' + convert(varchar, dateadd(dd, -14, getdate()), 106) + '''' ;
	Exec (@dirSQL)

	If @Deidentified = 1 Begin
		Select @InstanceName = (Select @dbname + '_' + Random_String	From vw_GenNewId	);

		Select @deidentified_filename = @backup_path + 'IQCare-Deidentified-' + @Ver + ' ' + @facility_name + ' ' + convert(varchar, getdate(), 23) + '.bak'
		Set @deidentified_instance = 'iqcare_backup_Deidt' + @facility_name + convert(varchar, getdate(), 23)
		Select @normal_filename = @backup_path + @InstanceName + '.bak'

		Set @TSQL = 'BACKUP DATABASE [' + @dbname + '] TO  DISK = ''' + @normal_filename + ''' WITH NOFORMAT, NOINIT,  NAME = ''' + @InstanceName + ''', SKIP, REWIND, NOUNLOAD, STATS = 10'
		Exec (@TSQL)

		Set @TSQL = 'Restore database ' + @InstanceName + ' from disk = ''' + @normal_filename + ''' With  nounload,replace,stats = 10, recovery
			   ,MOVE ''TestDataBase_IQCare'' TO ''' + @db_path + '\' + @InstanceName + '.mdf'',  MOVE ''TestDataBase_IQCare_log'' TO  ''' + @db_path + '\' + @InstanceName + '.ldf'''
		Exec (@TSQL)

		Set @dirSQL = 'del ' + @normal_filename + ' /F /Q'
		Exec xp_cmdshell @dirSQL

		Set @Tsql = ('exec( '' Update ' + @InstanceName + '.dbo.mst_patient Set
						FirstName = encryptbykey(key_guid(''''Key_CTC''''), (select Random_String from vw_GenNewId))
					,	MiddleName = encryptbykey(key_guid(''''Key_CTC''''), (select Random_String from vw_GenNewId))
					,	LastName = encryptbykey(key_guid(''''Key_CTC''''), (select Random_String from vw_GenNewId))
					,	Address = encryptbykey(key_guid(''''Key_CTC''''), (select Random_String from vw_GenNewId))
					,	Phone = encryptbykey(key_guid(''''Key_CTC''''), (select Random_String from vw_GenNewId))  ; 
				 Update ' + @InstanceName + '.dbo.dtl_patientcontacts Set
						GuardianName = encryptbykey(key_guid(''''Key_CTC''''), (select Random_String from vw_GenNewId))
					,	GuardianInformation = encryptbykey(key_guid(''''Key_CTC''''), (select Random_String from vw_GenNewId))
					,	EmergContactName = (select Random_String from vw_GenNewId)
					,	EmergContactPhone = (select Random_String from vw_GenNewId)
					,	EmergContactAddress = (select Random_String from vw_GenNewId)
					,	TenCellLeader = encryptbykey(key_guid(''''Key_CTC''''), (select Random_String from vw_GenNewId))
					,	TenCellLeaderAddress = encryptbykey(key_guid(''''Key_CTC''''), (select Random_String from vw_GenNewId))
					,	TreatmentSupportName = (select Random_String from vw_GenNewId)
					,	CommunitySupportGroup = (select Random_String from vw_GenNewId)
					,	TreatmentSupportAddress = (select Random_String from vw_GenNewId)       
  
				Update ' + @InstanceName + '.dbo.dtl_FamilyInfo Set
						RFirstName = encryptbykey(key_guid(''''Key_CTC''''), (select Random_String from vw_GenNewId))
					,	RLastName = encryptbykey(key_guid(''''Key_CTC''''), (select Random_String from vw_GenNewId)) ;       
				Update ' + @InstanceName + '.dbo. mst_Employee Set
					LastName = (select Random_String from vw_GenNewId)
				,	FirstName = (select Random_String from vw_GenNewId);
			Update ' + @InstanceName + '.dbo.mst_User Set
					UserLastName = (select Random_String from vw_GenNewId)
				,	UserFirstName = (select Random_String from vw_GenNewId);
			Update ' + @InstanceName + '.dbo.Person Set
				FirstName =encryptbykey(key_guid(''''Key_CTC''''),(select Random_String from vw_GenNewId)),
				MidName = encryptbykey(key_guid(''''Key_CTC''''),(select Random_String from vw_GenNewId)),
				LastName= encryptbykey(key_guid(''''Key_CTC''''),(select Random_String from vw_GenNewId));
			Update ' + @InstanceName + '.dbo. PersonContact Set
				PhysicalAddress = encryptbykey(key_guid(''''Key_CTC''''), (select Random_String from vw_GenNewId)),
				MobileNumber= encryptbykey(key_guid(''''Key_CTC''''), (select Random_String from vw_GenNewId)),
				AlternativeNumber =encryptbykey(key_guid(''''Key_CTC''''), (select Random_String from vw_GenNewId)),
				EmailAddress=encryptbykey(key_guid(''''Key_CTC''''), (select Random_String from vw_GenNewId));
			Update ' + @InstanceName + '.dbo.PersonLocation Set
				Location= (select Random_String from vw_GenNewId),
				Village= (select Random_String from vw_GenNewId),
				SubCounty= (select Random_String from vw_GenNewId),
				LandMark= (select Random_String from vw_GenNewId),
				NearestHealthCentre=(select Random_String from vw_GenNewId)'')');
		Exec (@Tsql)

		Set @TSQL = 'BACKUP DATABASE [' + @InstanceName + '] TO  DISK = ''' + @deidentified_filename + ''' WITH NOFORMAT, NOINIT, 
						NAME = ''' + @deidentified_instance + ''', SKIP, REWIND, NOUNLOAD, STATS = 10'
		Exec (@TSQL)

		Exec (' drop database [' + @InstanceName + '] ')

	End
	Else Begin
		select @backup_path
		Set @normal_filename = @backup_path + 'IQCare-' + @Ver + ' ' + @facility_name + ' ' + convert(varchar, getdate(), 23) + '.bak';
		Set @InstanceName = 'iqcare_backup' + @facility_name + convert(varchar, getdate(), 23) ;
		Set @TSQL = 'BACKUP DATABASE [' + @dbname + '] TO  DISK = ''' + @normal_filename + ''' WITH NOFORMAT, NOINIT,  NAME = ''' + @InstanceName + ''', SKIP, REWIND, NOUNLOAD, STATS = 10'
		Exec (@TSQL)
	End

End

GO


