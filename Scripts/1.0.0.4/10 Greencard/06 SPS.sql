/****** Object:  StoredProcedure [dbo].[CCC_GetPatientCount]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CCC_GetPatientCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CCC_GetPatientCount]
GO


/****** Object:  StoredProcedure [dbo].[CCC_GetPatientCount]    Script Date: 05/09/2017 17:08:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CCC_GetPatientCount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CCC_GetPatientCount] AS' 
END
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[CCC_GetPatientCount]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT        COUNT(PT.Id) AS PatientCount
	FROM            dbo.Patient AS PT INNER JOIN
							 dbo.PatientEnrollment AS PE ON PT.Id = PE.PatientId INNER JOIN
							 dbo.PatientIdentifier AS PI ON PE.Id = PI.PatientEnrollmentId AND PT.Id = PI.PatientId INNER JOIN
							 dbo.Identifiers AS IDE ON PI.IdentifierTypeId = IDE.Id
	WHERE        (PT.DeleteFlag = 0) AND (IDE.Name = 'CCC Registration Number')
END

GO


/****** Object:  StoredProcedure [dbo].[SP_mst_PatientToGreencardRegistration]    Script Date: 5/9/2017 3:16:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_mst_PatientToGreencardRegistration]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_mst_PatientToGreencardRegistration] AS' 
END
GO
-- =============================================
-- Author:		<felix/stephen>
-- Create date: <03-22-2017>
-- Description:	<Patient registration migration from bluecard to greencard>
-- =============================================
ALTER PROCEDURE [dbo].[SP_mst_PatientToGreencardRegistration]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	DECLARE @ptn_pk int, @FirstName varbinary(max), @MiddleName varbinary(max), @LastName varbinary(max), @Sex int, @Status bit, @Status_Greencard bit , @DeleteFlag bit, @CreateDate datetime, @UserID int,  @message varchar(80), @PersonId int, @PatientFacilityId varchar(50), @PatientType int, @FacilityId varchar(10), @DateOfBirth datetime, @DobPrecision int, @NationalId varbinary(max), @IDNumber varchar(100), @PatientId int, @ARTStartDate date,@transferIn int,@CCCNumber varchar(20), @entryPoint int, @ReferredFrom int, @RegistrationDate datetime, @MaritalStatusId int, @MaritalStatus int, @DistrictName varchar(50), @CountyID int, @SubCountyID int, @WardID int, @Address varbinary(max), @Phone varbinary(max), @EnrollmentId int, @PatientIdentifierId int, @ServiceEntryPointId int, @PatientMaritalStatusID int, @PatientTreatmentSupporterID int, @PersonContactID int, @LocationID int; 
		
	DECLARE @ExitReason int, @ExitDate datetime, @DateOfDeath datetime, @UserID_CareEnded int, @CreateDate_CareEnded datetime;
	
	DECLARE @i INT = 1;
	DECLARE @count INT;
  
	PRINT '-------- Patients Report --------';  
	exec pr_OpenDecryptedSession;
	
	--Create Temporary Tables for storing data 
	CREATE TABLE #Tmst_Patient(Id INT IDENTITY(1,1), ptn_pk int, FirstName varbinary(max), MiddleName varbinary(max), LastName varbinary(max), Sex int, Status int, DeleteFlag bit, CreateDate datetime, UserID int, PatientFacilityId varchar(50), FacilityId varchar(10), DateOfBirth datetime, DobPrecision int, IDNumber varchar(100), CCCNumber varchar(50), ReferredFrom int, RegistrationDate datetime, MaritalStatus int, DistrictName int, Address varbinary(max), Phone varbinary(max), LocationID int); 

	 --Insert data to temporary table #Tdtl_FamilyInfo 
	INSERT INTO #Tmst_Patient(
		ptn_pk, FirstName, MiddleName, LastName, Sex, Status, DeleteFlag, CreateDate, UserID, PatientFacilityId, FacilityId, DateOfBirth, DobPrecision, IDNumber, CCCNumber, ReferredFrom,
		RegistrationDate, MaritalStatus, DistrictName, Address, Phone, LocationID
	)
	
	SELECT DISTINCT mst_Patient.Ptn_Pk, FirstName, MiddleName ,LastName,Sex, [Status], DeleteFlag, mst_Patient.CreateDate, mst_Patient.UserID, PatientFacilityId, PosId, DOB, DobPrecision, [ID/PassportNo], PatientEnrollmentID, [ReferredFrom], [RegistrationDate], MaritalStatus, DistrictName, Address, Phone, LocationID
	FROM mst_Patient INNER JOIN  dbo.Lnk_PatientProgramStart ON dbo.mst_Patient.Ptn_Pk = dbo.Lnk_PatientProgramStart.Ptn_pk
	WHERE MovedToPatientTable = 0
	ORDER BY mst_Patient.Ptn_Pk;

	SELECT @count = COUNT(Id) FROM #Tmst_Patient
	BEGIN
		WHILE (@i <= @count)
			BEGIN
				SELECT @ptn_pk = Ptn_pk, @FirstName = FirstName, @MiddleName = MiddleName, @LastName = LastName, @Sex = Sex, @Status = [Status], @DeleteFlag = DeleteFlag, 
					   @CreateDate = CreateDate, @UserID = UserID, @PatientFacilityId = PatientFacilityId, @FacilityId = FacilityId, @DateOfBirth = DateOfBirth, 
					   @DobPrecision = DobPrecision, @IDNumber = IDNumber, @CCCNumber = CCCNumber, @ReferredFrom = [ReferredFrom], @RegistrationDate = [RegistrationDate], 
					   @MaritalStatus = MaritalStatus, @DistrictName = DistrictName, @Address = Address, @Phone = Phone, @LocationID = LocationID FROM #Tmst_Patient WHERE Id = @i;

				BEGIN TRY
					BEGIN TRANSACTION
						PRINT ' '  
						SELECT @message = '----- patients From mst_patient: ' + CAST(@ptn_pk as varchar(50));
						PRINT @message;

						--set null dates
						IF @CreateDate is null
							SET @CreateDate = getdate()
						--Due to the logic of green card
						IF @Status = 1
							SET @Status_Greencard = 0
						ELSE
							SET @Status_Greencard = 1

						IF @IDNumber IS NULL
							SET @IDNumber = 99999999;

						IF @Sex IS NOT NULL
							BEGIN
								IF ((select top 1  Name from mst_Decode where id = @Sex) = 'Male' OR (select top 1 Name from mst_Decode where id = @Sex) = 'Female')
									BEGIN
										SET @Sex = (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName like '%gender%' and ItemName like + (select top 1  Name from mst_Decode where id = @Sex) + '%');
									END
								ELSE
									SET @Sex = (select top 1  ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
							END
						ELSE
							SET @Sex = (select top 1  ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');

						--Default all persons to new
						SET @ARTStartDate=( SELECT top 1 FORMAT(ARTTransferInDate, 'yyyy-MM-dd') FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk=@ptn_pk And ARTTransferInDate Is Not Null);
						if(@ARTStartDate Is NULL OR @ARTStartDate = '1900-01-01') BEGIN SET @PatientType=(SELECT top 1 Id FROM LookupItem WHERE Name='New');SET @transferIn=0; END ELSE BEGIN SET @PatientType=(SELECT top 1 Id FROM LookupItem WHERE Name='Transfer-In');SET @transferIn=1; END
						-- SELECT @PatientType = 1285

						--encrypt nationalid
						SET @NationalId=ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@IDNumber);
		
						IF @Status = 1
							BEGIN
								DECLARE @PatientExitReason varchar(50);
				
								SET @PatientExitReason = (SELECT TOP 1 Name FROM mst_Decode WHERE CodeID=23 AND ID = (SELECT TOP 1 PatientExitReason FROM dtl_PatientCareEnded WHERE Ptn_Pk = @ptn_pk AND CareEnded=1));
								IF @PatientExitReason = 'Lost to follow-up'
									BEGIN
										SET @PatientExitReason = 'LostToFollowUp';
									END
								ELSE IF @PatientExitReason = 'Transfer to another LPTF' OR @PatientExitReason = 'Transfer to ART'
									BEGIN
										SET @PatientExitReason = 'Transfer Out';
									END
								ELSE IF NOT EXISTS(select top 1 ItemId from LookupItemView where MasterName = 'CareEnded' AND ItemName like '%' + @PatientExitReason + '%')
									BEGIN
										SET @PatientExitReason = 'Transfer Out';
									END
								SET @ExitReason = (select top 1 ItemId from LookupItemView where MasterName = 'CareEnded' AND ItemName like '%' + @PatientExitReason + '%');
								SET @ExitDate = (SELECT top 1 CareEndedDate FROM dtl_PatientCareEnded WHERE Ptn_Pk=@ptn_pk);
								SET @DateOfDeath = (SELECT top 1 DeathDate FROM dtl_PatientCareEnded WHERE Ptn_Pk=@ptn_pk);
								SET @UserID_CareEnded = (SELECT top 1 UserID FROM dtl_PatientCareEnded WHERE Ptn_Pk=@ptn_pk);
								SET @CreateDate_CareEnded = (SELECT top 1 CreateDate FROM dtl_PatientCareEnded WHERE Ptn_Pk=@ptn_pk);
							END
			

						Insert into Person(FirstName, MidName, LastName, Sex, Active, DeleteFlag, CreateDate, CreatedBy)
						Values(@FirstName, @MiddleName, @LastName, @Sex, @Status_Greencard, @DeleteFlag, @CreateDate, @UserID);



						SELECT @PersonId = SCOPE_IDENTITY();
						SELECT @message = 'Created Person Id: ' + CAST(@PersonId as varchar(50));
						PRINT @message;

						Insert into Patient(ptn_pk, PersonId, PatientIndex, PatientType, FacilityId, Active, DateOfBirth, DobPrecision, NationalId, DeleteFlag, CreatedBy, CreateDate, RegistrationDate)
						Values(@ptn_pk, @PersonId, @PatientFacilityId, @PatientType, @FacilityId, @Status_Greencard, @DateOfBirth, @DobPrecision, @NationalId, @DeleteFlag, @UserID, @CreateDate, @RegistrationDate);

						SELECT @PatientId = SCOPE_IDENTITY();
						SELECT @message = 'Created Patient Id: ' + CAST(@PatientId as varchar);
						PRINT @message;

						--Insert into Enrollment Table
						DECLARE @j INT = 1;
						DECLARE @countj INT;
						--Create Temporary Tables for storing data 
						CREATE TABLE #TLnk_PatientProgramStart (Id INT IDENTITY(1,1), ModuleId int, [StartDate] datetime, [UserID] int, [CreateDate] datetime);

						INSERT INTO #TLnk_PatientProgramStart(ModuleId, [StartDate], [UserID], [CreateDate])
						SELECT ModuleId, [StartDate], [UserID], [CreateDate] FROM Lnk_PatientProgramStart WHERE Ptn_pk=@ptn_pk;

						DECLARE @ModuleId int, @StartDate datetime, @UserID_Enrollment int, @CreateDate_Enrollment datetime;

						SELECT @countj = COUNT(Id) FROM #TLnk_PatientProgramStart 

						BEGIN
							WHILE (@j <= @countj)
								BEGIN
									SELECT @ModuleId = ModuleId, @StartDate = [StartDate], @UserID_Enrollment = [UserID], @CreateDate_Enrollment = [CreateDate] FROM #TLnk_PatientProgramStart WHERE Id = @j;

									BEGIN TRY
										BEGIN TRANSACTION
												PRINT ' ';
												SELECT @message = '----- Enrollment for: ' + CAST(@ptn_pk as varchar(50));
												PRINT @message;

												 IF @ModuleId = 203
													BEGIN
														DECLARE @DateEnrolledInCare DATETIME;
														IF @transferIn = 1
															BEGIN
																SET @DateEnrolledInCare = (select top 1 [DateEnrolledInCare] from dtl_PatientHivPrevCareEnrollment where ptn_pk = @ptn_pk);
															END;

														IF @DateEnrolledInCare IS NOT NULL
														BEGIN SET @StartDate = @DateEnrolledInCare END;

														INSERT INTO [dbo].[PatientEnrollment] ([PatientId] ,[ServiceAreaId] ,[EnrollmentDate] ,[EnrollmentStatusId] ,[TransferIn] ,[CareEnded] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[AuditData])
														VALUES (@PatientId,1, @StartDate,0, @transferIn, @Status ,0 ,@UserID_Enrollment ,@CreateDate_Enrollment ,NULL);

														SELECT @EnrollmentId = SCOPE_IDENTITY();
														SELECT @message = 'Created PatientEnrollment Id: ' + CAST(@EnrollmentId as varchar);
														PRINT @message;
													END
										COMMIT
									END TRY
									BEGIN CATCH
										ROLLBACK
									END CATCH

									SELECT @j = @j + 1;

								END
							END
						--Now Drop Temporary Tables
						DROP TABLE #TLnk_PatientProgramStart

						IF @CCCNumber IS NOT NULL AND @ModuleId = 203
							BEGIN
								-- Patient Identifier
								INSERT INTO [dbo].[PatientIdentifier] ([PatientId], [PatientEnrollmentId], [IdentifierTypeId], [IdentifierValue] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[Active] ,[AuditData])
								VALUES (@PatientId , @EnrollmentId ,(select top 1 Id from Identifiers where Code='CCCNumber') ,@CCCNumber ,0 ,@UserID ,@CreateDate ,0 ,NULL);

								SELECT @PatientIdentifierId = SCOPE_IDENTITY();
								SELECT @message = 'Created PatientIdentifier Id: ' + CAST(@PatientIdentifierId as varchar);
								PRINT @message;
							END

						--Insert into ServiceEntryPoint
						IF @ReferredFrom > 0 bEGIN
							SET @entryPoint = (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (SELECT top 1 Name FROM mst_Decode WHERE ID=@ReferredFrom AND CodeID=17) + '%');
							IF @entryPoint IS NULL
								BEGIN
									SET @entryPoint = (select top 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
								END
						END
						ELSE
							SET @entryPoint = (select top 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');

						INSERT INTO ServiceEntryPoint([PatientId], [ServiceAreaId], [EntryPointId], [DeleteFlag], [CreatedBy], [CreateDate], [Active])
						VALUES(@PatientId, 1, @entryPoint, 0 , @UserID, @CreateDate, 0);

						SELECT @ServiceEntryPointId = SCOPE_IDENTITY();
						SELECT @message = 'Created ServiceEntryPoint Id: ' + CAST(@ServiceEntryPointId as varchar);
						PRINT @message;
	
						--Insert into MaritalStatus
						IF @MaritalStatus > 0
							BEGIN
								IF EXISTS (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (select Name from mst_Decode where ID = @MaritalStatus and CodeID = 12) + '%')
									SET @MaritalStatusId = (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (select TOP 1 Name from mst_Decode where ID = @MaritalStatus and CodeID = 12) + '%');
								ELSE
									SET @MaritalStatusId = (select TOP 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
							END
						ELSE
							SET @MaritalStatusId = (select TOP 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');

						INSERT INTO PatientMaritalStatus(PersonId, MaritalStatusId, Active, DeleteFlag, CreatedBy, CreateDate)
						VALUES(@PersonId, @MaritalStatusId, 1, 0, @UserID, @CreateDate);

						SELECT @PatientMaritalStatusID = SCOPE_IDENTITY();
						SELECT @message = 'Created PatientMaritalStatus Id: ' + CAST(@PatientMaritalStatusID as varchar);
						PRINT @message;
    
						--Insert into Treatment Supporter
						DECLARE @k INT = 1;
						DECLARE @countk INT;

						DECLARE @FirstNameT varchar(50), @LastNameT varchar(50), @TreatmentSupportTelNumber varchar(50), 
						@CreateDateT datetime, @UserIDT int, @IDT int;

						--Create Temporary Tables for storing data 
						CREATE TABLE #Tdtl_PatientContacts(Id INT IDENTITY(1,1), FirstNameT varchar(50), LastNameT varchar(50), TreatmentSupportTelNumber varchar(50), CreateDateT datetime, UserIDT int);
						 --Insert data to temporary table #Tdtl_PatientContacts 
						INSERT INTO #Tdtl_PatientContacts(FirstNameT, LastNameT, TreatmentSupportTelNumber, CreateDateT, UserIDT)
						SELECT SUBSTRING(TreatmentSupporterName,0,charindex(' ',TreatmentSupporterName))as firstname, SUBSTRING(TreatmentSupporterName,charindex(' ',TreatmentSupporterName) + 1,len(TreatmentSupporterName)+1)as lastname,	TreatmentSupportTelNumber, CreateDate, UserID 
						from dtl_PatientContacts WHERE ptn_pk = @ptn_pk;

						SELECT @countk = COUNT(Id) FROM #Tdtl_PatientContacts 
						BEGIN
						WHILE (@k <= @countk)
							BEGIN
								SELECT @FirstNameT = FirstNameT, @LastNameT = LastNameT, @TreatmentSupportTelNumber = TreatmentSupportTelNumber, @CreateDateT = CreateDateT, @UserIDT = UserIDT FROM #Tdtl_PatientContacts WHERE Id = @k;

								BEGIN TRY
									BEGIN TRANSACTION
										PRINT ' '  
										SELECT @message = '----- Treatment Supporter: ' + CAST(@ptn_pk as varchar(50));
										PRINT @message;

										IF @FirstNameT IS NOT NULL AND @LastNameT IS NOT NULL 
											BEGIN
												Insert into Person(FirstName, MidName, LastName, Sex, Active, DeleteFlag, CreateDate, CreatedBy)
												Values(ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstNameT), NULL, ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastNameT), (select TOP 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown'), 1, 0, getdate(), @UserIDT);

												SELECT @IDT = SCOPE_IDENTITY();
												SELECT @message = 'Created Person Treatment Supporter Id: ' + CAST(@IDT as varchar(50));
												PRINT @message;

												INSERT INTO PatientTreatmentSupporter(PersonId, [SupporterId], [MobileContact], [DeleteFlag], [CreatedBy], [CreateDate])
												VALUES(@PersonId, @IDT, ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber), 0, @UserIDT, getdate());

												SELECT @PatientTreatmentSupporterID = SCOPE_IDENTITY();
												SELECT @message = 'Created PatientTreatmentSupporterID Id: ' + CAST(@PatientTreatmentSupporterID as varchar);
												PRINT @message;
											END
									COMMIT
									END TRY
									BEGIN CATCH
										ROLLBACK
									END CATCH

									SELECT @k = @k + 1;

									END
								END
							--Now Drop Temporary Tables
							DROP TABLE #Tdtl_PatientContacts

						--Insert into Person Contact
						IF @Address IS NOT NULL AND @Phone IS NOT NULL
							BEGIN
								INSERT INTO PersonContact(PersonId, [PhysicalAddress], [MobileNumber], [AlternativeNumber], [EmailAddress], [Active], [DeleteFlag], [CreatedBy], [CreateDate])
								VALUES(@PersonId, @Address, @Phone, null, null, @Status, 0, @UserID, @CreateDate);

								SELECT @PersonContactID = SCOPE_IDENTITY();
								SELECT @message = 'Created PersonContact Id: ' + CAST(@PersonContactID as varchar);
								PRINT @message;
							END

						--Starting baseline
						print 'starting baseline';

						DECLARE @DEFAULTDATE DATETIME;
						SET @DEFAULTDATE = ISNULL(@DEFAULTDATE, CONVERT(datetime, '1900-15-06', 104));
						PRINT @DEFAULTDATE;

						DECLARE @HBVInfected bit, @Pregnant bit, @TBinfected bit, @WHOStage int, @WHOStageString varchar(50), @BreastFeeding bit, 
								@CD4Count decimal , @MUAC decimal, @Weight decimal, @Height decimal, @artstart datetime,
								@ClosestARVDate datetime, @PatientMasterVisitId int, @HIVDiagnosisDate datetime, @EnrollmentDate datetime,
								@EnrollmentWHOStage int, @EnrollmentWHOStageString varchar(50), @VisitDate datetime, @Cohort varchar(50), @visit_id int;

						Select TOP 1 @artstart = ARTStartDate	From mst_Patient	Where Ptn_Pk = @ptn_pk	And LocationID = @LocationId;
						select TOP 1 @visit_id = visit_id from dtl_PatientARVEligibility where ptn_pk = @ptn_pk And LocationID = @LocationId;
		
						print 'set @artstart and @visit_id';

						SET @Pregnant = 0;

						IF @Sex = (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName like '%gender%' and ItemName like 'Female%')
							BEGIN
								--SET @Pregnant = 0;
								IF EXISTS(select TOP 1 Name from mst_Decode where id=(select TOP 1 eligibleThrough from dtl_PatientARVEligibility where ptn_pk = @ptn_pk And LocationID = @LocationId) and name like 'Pregnancy')
									BEGIN
										SET @Pregnant = 1;
									END
							END
			
						print 'set @Sex';

						If EXISTS(SELECT * FROM dtl_PatientVitals dtl WHERE dtl.Visit_pk = @visit_id ) Begin
							SET @Weight = (Select Top (1) dtl.[Weight]
							From ord_Visit As ord
							Inner Join
								dtl_PatientVitals As dtl On dtl.Visit_pk = ord.Visit_Id
							Where (ord.Ptn_Pk = @ptn_pk)
							And (dtl.[Weight] Is Not Null)
							And (ord.Visit_Id = @visit_id));
						End 
						Else Begin
							SET @Weight = NULL;
						End
		
						print 'set @Weight';

						If exists (SELECT * FROM dtl_PatientVitals dtl WHERE dtl.Visit_pk = @visit_id) Begin
							SET @Height = (Select Top 1 dtl.Height
							From Ord_visit ord
							Inner Join
								dtl_PatientVitals dtl On dtl.visit_pk = ord.Visit_Id
							Where ord.ptn_pk = @ptn_pk
							And dtl.Height Is Not Null
							And (ord.Visit_Id = @visit_id));
						End 
						Else Begin
							SET @Height = NULL;
						End
		
						print 'set @Height';

						If EXISTS(SELECT * FROM dtl_PatientVitals dtl WHERE dtl.Visit_pk = @visit_id) Begin
							SET @MUAC = (Select Top (1) dtl.Muac
							From ord_Visit As ord
							Inner Join
								dtl_PatientVitals As dtl On dtl.Visit_pk = ord.Visit_Id
							Where (ord.Ptn_Pk = @ptn_pk)
							And (dtl.Muac Is Not Null)
							And (ord.Visit_Id = @visit_id));
						End
		
						print 'set @MUAC';

						SET @TBinfected = 0;
						IF EXISTS(select TOP 1 Name from mst_Decode where id=(select TOP 1 eligibleThrough from dtl_PatientARVEligibility where ptn_pk = @ptn_pk And LocationID = @LocationId) and name like 'TB/HIV')
							BEGIN
								SET @TBinfected = 1;
							END
			
						print 'set @TBinfected';

						SET @BreastFeeding = 0;
						IF EXISTS(select TOP 1 Name from mst_Decode where id=(select TOP 1 eligibleThrough from dtl_PatientARVEligibility where ptn_pk = @ptn_pk And LocationID = @LocationId) and name like 'BreastFeeding')
							BEGIN
								SET @TBinfected = 1;
							END
			
						print 'set @BreastFeeding';

						SET @HIVDiagnosisDate = (SELECT TOP 1 dbo.dtl_PatientHivPrevCareEnrollment.ConfirmHIVPosDate
						FROM dbo.dtl_PatientHivPrevCareEnrollment INNER JOIN
						 dbo.ord_Visit ON dbo.dtl_PatientHivPrevCareEnrollment.ptn_pk = dbo.ord_Visit.Ptn_Pk 
						 AND dbo.dtl_PatientHivPrevCareEnrollment.Visit_pk = dbo.ord_Visit.Visit_Id INNER JOIN
                         dbo.mst_VisitType ON dbo.ord_Visit.VisitType = dbo.mst_VisitType.VisitTypeID
						 WHERE (dbo.mst_VisitType.VisitName = 'ART History') AND dbo.dtl_PatientHivPrevCareEnrollment.ptn_pk = @ptn_pk);

						print 'set @HIVDiagnosisDate';
						SET @EnrollmentDate = (select TOP 1 DateEnrolledInCare from dtl_PatientHivPrevCareEnrollment where ptn_pk=@ptn_pk);
						print 'set @EnrollmentDate';
						SET @EnrollmentWHOStageString = (SELECT TOP 1 Name FROM mst_Decode WHERE ID = (SELECT TOP 1 WHOStage FROM dtl_PatientARVEligibility where WHOStage > 0 AND ptn_pk=@ptn_pk) and codeid=22 AND Name <> 'N/A');
						print 'set @EnrollmentWHOStage';
						SET @Cohort = (select  TOP 1 convert(char(3),[FirstLineRegStDate] , 0) + ' ' + CONVERT(varchar(10), year([FirstLineRegStDate])) from [dbo].[dtl_PatientARTCare] WHERE ptn_pk = @ptn_pk);
						print 'set @Cohort';
						SET @CD4Count = (SELECT top 1 CD4 FROM dtl_PatientARVEligibility WHERE ptn_pk = @ptn_pk)
						print 'set @CD4Count';
						SET @WHOStageString = (SELECT TOP 1 WHOStage FROM dtl_PatientARVEligibility where ptn_pk = @ptn_pk);

						print 'set @HIVDiagnosisDate, @EnrollmentDate, @EnrollmentWHOStage, @Cohort, @CD4Count, @WHOStage';
		
						SET @EnrollmentWHOStage = CASE @EnrollmentWHOStageString  
							 WHEN '1' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '1') 
							 WHEN '2' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '2')   
							 WHEN '3' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '3')   
							 WHEN '4' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '4')
							 WHEN 'T1' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '1') 
							 WHEN 'T2' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '2')   
							 WHEN 'T3' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '3')   
							 WHEN 'T4' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '4')
							 ELSE (select TOP 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown')
						  END
		  
						SET @WHOStage = CASE @WHOStageString  
							 WHEN '1' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '1') 
							 WHEN '2' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '2')   
							 WHEN '3' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '3')   
							 WHEN '4' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '4')
							 WHEN 'T1' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '1') 
							 WHEN 'T2' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '2')   
							 WHEN 'T3' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '3')   
							 WHEN 'T4' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '4')
							 ELSE (select TOP 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown')
						  END
		  
						SET @VisitDate = (SELECT TOP 1 [VisitDate] FROM [dbo].[ord_Visit] where [Ptn_Pk] = @ptn_pk AND [VisitType] in(18, 19));
						IF @EnrollmentDate IS NULL BEGIN SET @EnrollmentDate =@StartDate; END;

						INSERT INTO PatientMasterVisit(PatientId, ServiceId, Start, [End], Active, VisitDate, VisitScheduled, VisitBy, VisitType, [Status], CreateDate, DeleteFlag, CreatedBy)
						VALUES(@PatientId, 1, @EnrollmentDate, NULL, 0, @VisitDate, NULL, NULL, (SELECT top 1 ItemId FROM LookupItemView WHERE	MasterName like '%VisitType%' and ItemName like '%Enrollment%'), NULL, GETDATE(), 0 , @UserID);

						SET @PatientMasterVisitId = SCOPE_IDENTITY();
		
						SELECT @message = 'Created PatientMasterVisit Id: ' + CAST(@PatientMasterVisitId as varchar);
						PRINT @message;
			
						IF @Status = 1
							BEGIN
								IF @ExitDate IS NULL
								BEGIN
									SET @ExitDate = @CreateDate;
								END;
				
								IF @UserID_CareEnded IS NULL
								BEGIN
									SET @UserID_CareEnded = @UserID;
								END;
				
								IF @CreateDate_CareEnded IS NULL
								BEGIN
									SET @CreateDate_CareEnded = @CreateDate;
								END;
				
								INSERT INTO [dbo].[PatientCareending] ([PatientId] ,[PatientMasterVisitId] ,[PatientEnrollmentId] ,[ExitReason] ,[ExitDate] ,[TransferOutfacility] ,[DateOfDeath] ,[CareEndingNotes] ,[Active] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[AuditData])
								VALUES(@PatientId ,@PatientMasterVisitId ,@EnrollmentId ,@ExitReason , @ExitDate ,NULL ,@DateOfDeath ,NULL ,0 ,0,@UserID_CareEnded ,@CreateDate_CareEnded ,NULL);
							END
			
						SELECT @message = 'Created PatientCareending Id: ' + CAST(SCOPE_IDENTITY() as varchar);
						PRINT @message;

						IF (@Weight IS NOT NULL AND @Height IS NOT NULL AND @Weight > 0 AND @Height > 0)
						BEGIN
							INSERT INTO [dbo].[PatientBaselineAssessment]([PatientId], [PatientMasterVisitId], [HBVInfected], [Pregnant], [TBinfected], [WHOStage], [BreastFeeding], [CD4Count], [MUAC], [Weight], [Height], [DeleteFlag], [CreatedBy], [CreateDate] )
							VALUES(@PatientId, @PatientMasterVisitId, 0, @Pregnant, @TBinfected, @WHOStage, @BreastFeeding, @CD4Count, @MUAC, @Weight, @Height, 0 , @UserID, GETDATE());

							SELECT @message = 'Created PatientBaselineAssessment Id: ' + CAST(SCOPE_IDENTITY() as varchar);
							PRINT @message;
						END

						IF EXISTS(SELECT * FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk = @ptn_pk)
							BEGIN
								DECLARE @TransferInDate datetime, @TreatmentStartDate datetime, @CurrentART varchar(50), @FacilityFrom varchar(150), @CreateDateTransfer datetime, @MFLCODE int;

								SET @TransferInDate = (SELECT TOP 1 ARTTransferInDate FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk = @ptn_pk);
								SET @TreatmentStartDate = (SELECT TOP 1 FirstLineRegStDate FROM dtl_PatientARTCare WHERE ptn_pk = @ptn_pk);
								SET @CurrentART = (SELECT TOP 1 CurrentART FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk = @ptn_pk);
								SET @FacilityFrom = (SELECT TOP 1 ARTTransferInFrom FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk = @ptn_pk);
								SET @CreateDateTransfer = (SELECT TOP 1 CreateDate FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk = @ptn_pk);

								SET @MFLCODE = (select TOP 1 PosId from mst_Patient WHERE Ptn_pk = @ptn_pk);
								SET @TreatmentStartDate = ISNULL(@TreatmentStartDate, @artstart);
								SET @TreatmentStartDate = ISNULL(@TreatmentStartDate, @DEFAULTDATE);

								SET @MFLCODE = (select TOP 1 PosId from mst_Patient WHERE Ptn_pk = @ptn_pk);
								SET @FacilityFrom = ISNULL(@FacilityFrom, 'Unknown');
								SET @CurrentART = ISNULL(@CurrentART, (select top 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown'));

								SELECT @message = '@TransferInDate: ' + CAST(@TransferInDate as varchar);
								PRINT @message;

								SELECT @message = '@TreatmentStartDate: ' + CAST(@TreatmentStartDate as varchar);
								PRINT @message;

								SELECT @message = '@CurrentART: ' + CAST(@CurrentART as varchar);
								PRINT @message;

								SELECT @message = '@FacilityFrom: ' + CAST(@FacilityFrom as varchar);
								PRINT @message;

								SELECT @message = '@MFLCODE: ' + CAST(@MFLCODE as varchar);
								PRINT @message;


								IF @TransferInDate IS NOT NULL AND @TreatmentStartDate IS NOT NULL AND @CurrentART IS NOT NULL AND @FacilityFrom IS NOT NULL AND @MFLCODE IS NOT NULL
									BEGIN
										INSERT INTO [dbo].[PatientTransferIn]([PatientId], [PatientMasterVisitId], [ServiceAreaId], [TransferInDate], [TreatmentStartDate], [CurrentTreatment],  [FacilityFrom] , [MFLCode] ,[CountyFrom] , [TransferInNotes], [DeleteFlag] ,[CreatedBy] , [CreateDate])
										VALUES(@PatientId, @PatientMasterVisitId, 1, @TransferInDate, @TreatmentStartDate, @CurrentART, @FacilityFrom, @MFLCODE, (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown'), ' ', 0 , @UserID, @CreateDateTransfer);

										SELECT @message = 'Created PatientTransferIn Id: ' + CAST(SCOPE_IDENTITY() as varchar);
										PRINT @message;
									END
						END

						IF EXISTS (Select	ptn_pk,	locationID,	Visit_pk [VisitId], a.PurposeId, b.Name [Purpose], a.Regimen [Regimen],	a.DateLastUsed [RegLastUsed] From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk)
							BEGIN
								DECLARE @TreatmentType varchar(50), @Purpose varchar(50), @Regimen varchar(50), @DateLastUsed datetime;
			
								SET @TreatmentType = (select TOP 1 [Name] from mst_Decode where codeID=33 AND ID = (Select top 1 a.PurposeId From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk));
								SET @Purpose = (select TOP 1 b.Name [Purpose] From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk);
								SET @Regimen = (select TOP 1 a.Regimen [Regimen] From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk);
								SET @DateLastUsed = (select TOP 1 a.DateLastUsed [RegLastUsed] From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk);

								IF @TreatmentType IS NOT NULL AND @Purpose IS NOT NULL AND @Regimen IS NOT NULL
									BEGIN
									INSERT INTO [dbo].[PatientARVHistory]([PatientId], [PatientMasterVisitId], [TreatmentType], [Purpose] , [Regimen], [DateLastUsed], [DeleteFlag] , [CreatedBy] , [CreateDate])
									VALUES(@PatientId, @PatientMasterVisitId, @TreatmentType, @Purpose, @Regimen, ISNULL(@DateLastUsed, @DEFAULTDATE), 0, @UserID, @CreateDate);
									END

									SELECT @message = 'Created PatientARVHistory Id: ' + CAST(SCOPE_IDENTITY() as varchar);
									PRINT @message;
							END

						IF EXISTS(select TOP 1 FirstLineRegStDate from [dbo].[dtl_PatientARTCare] WHERE ptn_pk = @ptn_pk)
							BEGIN
								DECLARE @DateStartedOnFirstLine datetime;
								SET @DateStartedOnFirstLine = (select TOP 1 FirstLineRegStDate from [dbo].[dtl_PatientARTCare] WHERE ptn_pk = @ptn_pk);

								IF @DateStartedOnFirstLine IS NULL
									BEGIN
										SET @DateStartedOnFirstLine = @DEFAULTDATE;
										SET @Cohort = (select  convert(char(3),@DateStartedOnFirstLine , 0) + ' ' + CONVERT(varchar(10), year(@DateStartedOnFirstLine)));
									END
					
								INSERT INTO [dbo].[PatientTreatmentInitiation]([PatientMasterVisitId], [PatientId], [DateStartedOnFirstLine], [Cohort], Regimen, [RegimenCode] , [BaselineViralload] , [BaselineViralloadDate] , [DeleteFlag] , [CreatedBy] , [CreateDate] )
								VALUES(@PatientMasterVisitId, @PatientId, @DateStartedOnFirstLine, @Cohort, Null,(SELECT TOP 1 FirstLineReg FROM dtl_PatientARTCare where ptn_pk = @ptn_pk) , NULL, NULL, 0, @UserID, @CreateDate);

									SELECT @message = 'Created PatientTreatmentInitiation Id: ' + CAST(SCOPE_IDENTITY() as varchar);
									PRINT @message;
							END

						SET @HIVDiagnosisDate = ISNULL(@HIVDiagnosisDate, @DEFAULTDATE);
						SET @EnrollmentDate = ISNULL(@EnrollmentDate, @DEFAULTDATE);
						SET @artstart = ISNULL(@artstart, @DEFAULTDATE);

						IF @HIVDiagnosisDate IS NOT NULL AND @EnrollmentDate IS NOT NULL AND @EnrollmentWHOStage IS NOT NULL AND @artstart IS NOT NULL
							BEGIN
								INSERT INTO [dbo].[PatientHivDiagnosis]([PatientMasterVisitId] , [PatientId] , [HIVDiagnosisDate] , [EnrollmentDate] , [EnrollmentWHOStage] , [ARTInitiationDate] , [DeleteFlag] , [CreatedBy] , [CreateDate])
								VALUES(@PatientMasterVisitId, @PatientId, @HIVDiagnosisDate, @EnrollmentDate, @EnrollmentWHOStage, @artstart, 0 , @UserID, @CreateDate);

									SELECT @message = 'Created PatientHivDiagnosis Id: ' + CAST(SCOPE_IDENTITY() as varchar);
									PRINT @message;
							END

						--ending baseline
						Update mst_Patient Set MovedToPatientTable =1 Where Ptn_Pk=@ptn_pk;
						INSERT INTO [dbo].[GreenCardBlueCard_Transactional] ([PersonId] ,[Ptn_Pk]) VALUES (@PersonId , @ptn_pk);
						COMMIT;

						SELECT @message = 'Completed Inserting Patient: ' + CAST(@ptn_pk as varchar);
						PRINT @message;

					END TRY

					BEGIN CATCH
						ROLLBACK
					END CATCH

				SELECT @i = @i + 1
			END
		END
		--Now Drop Temporary Tables
		 DROP TABLE #Tmst_Patient
END

GO