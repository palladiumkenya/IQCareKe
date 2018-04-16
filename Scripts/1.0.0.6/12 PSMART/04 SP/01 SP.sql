-- STORED PROCEDUE FOR PSMART

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Psmart_ProcessMotherDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Psmart_ProcessMotherDetails]
GO

/****** Object:  StoredProcedure [dbo].[Psmart_ProcessMotherDetails]    Script Date: 4/13/2018 8:57:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<sosewe>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Psmart_ProcessMotherDetails]
	-- Add the parameters for the stored procedure here
	@firstName varchar(100),
	@middleName varchar(100),
	@lastName varchar(100),
	@cccNumber varchar(20),
	@ptnpk int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @hivStatus int,@hivCareStatus int,@PersonId int,@RelationshipTypeId int,@PatientId int;


    -- Insert statements for procedure here
	if(@cccNumber IS NULL)BEGIN SET @hivStatus=37 END ELSE BEGIN SET @hivStatus=37 END
	if(@cccNumber IS NULL)BEGIN SET @hivCareStatus=1 END ELSE BEGIN SET @hivStatus=null END

	--SET @PersonId=(SELECT PersonId FROM Patient WHERE ptn_pk=@ptnpk);
	SET @RelationshipTypeId=(SELECT Id FROM LookupItem WHERE Name='mother');
	SET @PatientId=(SELECT Id FROM Patient WHERE ptn_pk=@ptnpk)

	INSERT INTO [dbo].[dtl_FamilyInfo]
           ([Ptn_pk],[RFirstName],[RLastName],[Sex],[AgeYear],[AgeMonth],[RelationshipDate],[RelationshipType],[HivStatus],[HivCareStatus],[RegistrationNo],[UserId] ,[DeleteFlag],[CreateDate]	)
     VALUES
           (@ptnpk
           , encryptbykey(key_guid('Key_CTC'), @firstName)--<RFirstName, varbinary(max),>
           , encryptbykey(key_guid('Key_CTC'), @lastName) --<RLastName, varbinary(max),>
           ,17
           ,0 --<AgeYear, int,>
           ,0 --<AgeMonth, int,>
           ,GETDATE() --<RelationshipDate, datetime,>
           ,10 -- <RelationshipType, int,>
           ,@hivStatus
           ,@hivCareStatus
           ,@cccNumber
           ,1 -- <UserId, int,>
           ,0 --<DeleteFlag, int,>
           ,GETDATE()
           )
	
	 -- INSERT TO PERSON
    INSERT INTO Person (FirstName,LastName,Sex,CreatedBy,CreateDate) VALUES(
		ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstName),
		ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastName),
		(SELECT Id FROM LookupItem WHERE Name='female'),
		1,
		GETDATE()
	)
	SELECT @PersonId=SCOPE_IDENTITY();

	INSERT INTO PersonRelationship (PersonId,RelationshipTypeId,CreatedBy,CreateDate,PatientId) VALUES(
		@PersonId,@RelationshipTypeId,1,GETDATE(),@PatientId
	)

END



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Psmart_ProcessNewClientRegistration]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Psmart_ProcessNewClientRegistration]
GO

/****** Object:  StoredProcedure [dbo].[Psmart_ProcessNewClientRegistration]    Script Date: 4/13/2018 8:58:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<stephen Osewe>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Psmart_ProcessNewClientRegistration]
	-- Add the parameters for the stored procedure here
	@firstName varchar(100), --1
	@middleName varchar(100)=null, --2
	@lastName varchar(100), --3
	@registrationDate datetime, --4
	@dob varchar(50), --5
	@dobPrecision varchar(15), --6
	@phone varchar(50), --7
	@gender varchar(15), --8
	@landmark varchar(50), --9
	@maritalStatus varchar(50)=0, --10
	@htsId varchar(50), --11
	@serialNumber varchar(50), --12
	@facilityId int=null, --13
	@moduleId varchar(5), --14
	@village varchar(50) null, --15
	@ward varchar(50) null, --16
	@subcounty varchar(50) null, --17
	@heiNumber varchar(15), --18
	@Address varchar(250) --19
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	declare @ptnpk int=0,@visitId int=0, @locationId int,@PosId int,@CountryId int
	DECLARE @PatientMastrVisitId int;
	DECLARE @PersonId int;
	Declare @PatientId int,@PatientEnrollmentId int;

	Select top 1 @locationId = FacilityID, @PosId = PosId, @CountryId = CountryID From mst_Facility where DeleteFlag=0
		Begin Transaction InsertPat
	Begin Try 
	INSERT INTO
                mst_Patient(
                    [Status], FirstName, MiddleName, LastName,
                    LocationID, RegistrationDate, Sex, DOB, DobPrecision,
                    CountryId, PosId, SatelliteId, UserID, CreateDate,
                    Phone, 
					Landmark, HTSID,MaritalStatus,CardSerialNumber,
				--	VillageName,
					--Ward,
					--DistrictName,
					[Address],
					HEIIDNumber)
                VALUES(
                    '0',
					 encryptbykey(key_guid('Key_CTC'), @firstName),
					  encryptbykey(key_guid('Key_CTC'), @middleName),
					  encryptbykey(key_guid('Key_CTC'), @lastName),
                   @locationId,
					 CONVERT(datetime,  @registrationDate , 104),
					(SELECT top 1 Id FROM mst_Decode WHERE Name=''+@gender+''),
					 CONVERT(datetime, @dob , 104),
					ISNULL(convert(int,@dobPrecision),0),
                    @CountryId,
					@PosId,
					0,
					1,
					GETDATE(),
                    encryptbykey(key_guid('Key_CTC'), @phone), 
					@landmark,
					@htsId,
					ISNULL((SELECT top 1 Id FROM mst_decode WHERE Name=''+@maritalStatus+''),0),-- SELECT SCOPE_IDENTITY(); ");,
					@serialNumber,
				--	(SELECT ID FROM mst_Village WHERE Name=''+@village+''),
				--	@ward,
					--(SELECT ID FROM mst_District WHERE Name=''+@subcounty+''),
					encryptbykey(key_guid('Key_CTC'), @Address),
					@heiNumber
					);
					SET @ptnpk=SCOPE_IDENTITY();

			-- insert into ord_visit
			
	INSERT INTO [dbo].[ord_Visit](
			[Ptn_Pk]
		  ,[LocationID]
		  ,[VisitDate]
		 ,[VisitType]
		 ,[DataQuality]
		 ,[DeleteFlag]
		 ,[UserID]
		 ,[CreateDate]
		 ,[UpdateDate]
           )
     VALUES
           (
		     @ptnpk
           ,@locationId
		   ,GETDATE() -- visitdate
           ,12 -- for registration
           ,1
           ,0
           ,1 -- default psmart userId
           ,GETDATE()
           ,GETDATE()
           )

		   -- insert into lnk_patientprogramstart
		   INSERT INTO [dbo].[Lnk_PatientProgramStart]
           ([Ptn_pk]
           ,[ModuleId]
           ,[StartDate]
           ,[UserID]
           ,[CreateDate]
           ,[UpdateDate])
     VALUES
           (@ptnpk
           ,@moduleId
           ,GETDATE()
           ,1
           ,GETDATE()
           ,GETDATE()
		   );

		   
 -- INSERT TO PERSON
    INSERT INTO Person (FirstName,MidName,LastName,Sex,DateOfBirth,DobPrecision,CreatedBy,CreateDate) VALUES(
		ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstName),
		ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastName),
		ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@MiddleName),
		(SELECT Id FROM LookupItem WHERE Name=''+@gender+''),
		 CONVERT(datetime,  @dob , 104),
		(SELECT Isnull(Id,0) FROM LookupItem WHERE Name=''+@DobPrecision+''),
		1,
		 CONVERT(datetime,  @registrationDate , 104)
	)
	SELECT @PersonId=SCOPE_IDENTITY();


 -- INSERT TO PATIENT
	INSERT INTO Patient(ptn_pk,PersonId,PatientType,FacilityId,DateOfBirth,DobPrecision,NationalId,RegistrationDate,CreateDate,CreatedBy) VALUES(
		@ptnpk,
		@PersonId,
		(SELECT Id FROM LookupItem WHERE Name='New'),
		'',-- facilityId,
		 CONVERT(datetime,  @dob , 104),
		(SELECT ISNULL(Id,0) FROM LookupItem WHERE Name=''+@DobPrecision+''),
		999999,
		 CONVERT(datetime,  @registrationDate , 104),
		GETDATE(),
		1
	)
	SELECT @PatientId=SCOPE_IDENTITY();

 -- PatientMasterVisit
	INSERT INTO PatientMasterVisit(PatientId,ServiceId,Start,VisitDate,VisitScheduled,VisitBy,VisitType,CreatedBy,CreateDate) VALUES(
		@PatientId,
		1,
		GETDATE()
		,CONVERT(datetime,  @registrationDate , 104),
		0,
		(SELECT top 1 Id FROM LookupItem WHERE Name='self'),
		0,
		1,
		GETDATE()
	 )
	 SET @PatientMastrVisitId=SCOPE_IDENTITY();
 -- INsert Patient Enrollment
    INSERT INTO PatientEnrollment (PatientId,ServiceAreaId,EnrollmentDate,EnrollmentStatusId,TransferIn,CareEnded,CreatedBy,CreateDate) VALUES(
		@PatientId
		,2
		, CONVERT(datetime,  @registrationDate , 104)
		,0
		,0
		,0
		,1
		,CONVERT(datetime,  @registrationDate , 104)
	)
	SELECT @PatientEnrollmentId=SCOPE_IDENTITY();

 -- INSER TO PatientIdentifier
	INSERT INTO PatientIdentifier (PatientId,PatientEnrollmentId,IdentifierTypeId,IdentifierValue,CreatedBy,CreateDate)VALUES(
	  @PatientId,
	  @PatientEnrollmentId,
	  (SELECT Id FROM Identifiers WHERE Code='HTSNumber'),
	  @htsId,
	  1,
	  GETDATE()
	);

	-- PersonContact
		INSERT INTO PatientContact (PersonId,PhysicalAddress,mobileNo,CreateDate,CreatedBy) VALUES(
			@PersonId,
			encryptbykey(key_guid('Key_CTC'), @Address),
			encryptbykey(key_guid('Key_CTC'), @phone),
			GETDATE(),
			1
		)
	-- PersonLocation
	   
	   IF(@village IS NOT NULL AND @ward IS NOT NULL AND @subcounty IS NOT NULL)
	   BEGIN
		INSERT INTO PersonLocation(PersonId,County,SubCounty,Village,Ward,LandMark,CreatedBy,CreateDate) VALUES(
			@PersonId,
			(SELECT top 1 CountyId  FROM County WHERE Subcountyname=''+@subcounty+''),
			(SELECT top 1 SubcountyId  FROM County WHERE Subcountyname=''+@subcounty+''),
			@village,
			(SELECT top 1 CountyId  FROM County WHERE WardName=''+@ward+''),
			@landmark,
			1,
			GETDATE()
		)
	   END
	-- PatientENcounter
	INSERT INTO PatientEncounter(PatientId,EncounterTypeId,EncounterStartTime,ServiceAreaId,CreateDate,createdby,Status) VALUES(
		@PatientId,
		(SELECT top 1 Id FROM LookupItem WHERE Name='Hts-encounter'),
		CONVERT(datetime,  @registrationDate , 104),
		1,
		CONVERT(datetime,  @registrationDate , 104),
		1,
		0
	)

		  SELECT @ptnpk Id;

		  If @@TRANCOUNT > 0 Commit Transaction InsertPat;
	End Try 
	Begin Catch
		Declare @ErrorMessage nvarchar(4000), @ErrorSeverity int, @ErrorState int;
		Select	@ErrorMessage = error_message(),
				@ErrorSeverity = error_severity(),
				@ErrorState = error_state();
		Raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState);
		If @@TRANCOUNT > 0 Rollback Transaction InsertPat;
	End Catch;
END


-- 