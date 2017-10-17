/****** Object:  StoredProcedure [dbo].[PatientsNotSynced]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientsNotSynced]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PatientsNotSynced]
GO


/****** Object:  StoredProcedure [dbo].[PatientsNotSynced]    Script Date: 05/09/2017 17:08:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientsNotSynced]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PatientsNotSynced] AS' 
END
GO

-- =============================================
-- Author: Felix
-- Create date: 10-Oct-2017
-- Description:	Avoid duplication of patients
-- =============================================
ALTER PROCEDURE [dbo].[PatientsNotSynced]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Insert statements for procedure here
	DECLARE @j INT = 1;
	DECLARE @countj INT;
	--Create Temporary Tables for storing data 
	CREATE TABLE #Tmst_Patient (Id INT IDENTITY(1,1), ptn_pk int, personId int);

	INSERT INTO #Tmst_Patient(ptn_pk, personId)
	SELECT ptn_pk, PersonId FROM Patient

	DECLARE @ptn_pk int, @personId int, @patientId int, @message varchar(max);

	SELECT @countj = COUNT(Id) FROM #Tmst_Patient 

	BEGIN
		WHILE (@j <= @countj)
			BEGIN
				SELECT @ptn_pk = ptn_pk, @personId = personId FROM #Tmst_Patient WHERE Id = @j;

				BEGIN TRY
					BEGIN TRANSACTION
						UPDATE mst_Patient set MovedToPatientTable = 1 where Ptn_Pk = @ptn_pk;	

						IF NOT EXISTS(SELECT * FROM GreenCardBlueCard_Transactional WHERE Ptn_Pk = @ptn_pk)
							BEGIN
								PRINT ' ';
								SELECT @message = '----- Set Patient as moved ptn_pk: ' + CAST(@ptn_pk as varchar(50));
								PRINT @message;
								
								INSERT INTO [dbo].[GreenCardBlueCard_Transactional] ([PersonId],[Ptn_Pk]) VALUES (@personId , @ptn_pk);
							END
					COMMIT
				END TRY
				BEGIN CATCH
					ROLLBACK
				END CATCH

				SELECT @j = @j + 1;

			END
		END
	DROP TABLE #Tmst_Patient


	DECLARE @d int = 1;
	DECLARE @countd int;

	CREATE TABLE #TPatient(Id INT IDENTITY(1,1), ptn_pk int, personId int, patientId int)

	INSERT INTO #TPatient(ptn_pk, personId, patientId)
	 SELECT ptn_pk, PersonId, Id from (SELECT        Id, ptn_pk, PersonId, PatientIndex, PatientType, FacilityId, Active, DateOfBirth, DobPrecision, NationalId, DeleteFlag, CreatedBy, CreateDate, AuditData, RegistrationDate, row_number() Over (Partition By ptn_pk Order By Id Asc) RowNum
	FROM            dbo.Patient
	WHERE        (ptn_pk IN
		 (SELECT        ptn_pk
		   FROM            (SELECT        Patient_1.ptn_pk, COUNT(Patient_1.Id) AS Expr1
				FROM            dbo.Patient AS Patient_1 INNER JOIN
						dbo.mst_Patient ON Patient_1.ptn_pk = dbo.mst_Patient.Ptn_Pk
				GROUP BY Patient_1.ptn_pk
				HAVING         (COUNT(Patient_1.Id) > 1)) AS T))) B 

	WHERE B.RowNum>1

	SELECT @countd = COUNT(Id) FROM #TPatient
	
	BEGIN
		WHILE (@d <= @countd)
		BEGIN
			SELECT @ptn_pk = ptn_pk, @personId = personId, @patientId = patientId from #TPatient WHERE Id = @d;

			BEGIN TRY
				BEGIN TRANSACTION
					UPDATE Patient SET DeleteFlag = 1, ptn_pk = - FLOOR(RAND(CHECKSUM(NEWID()))*(9999-1000)+1000) WHERE Id = @patientId
					UPDATE Person SET DeleteFlag = 1 WHERE Id = @personId
				COMMIT
			END TRY
			BEGIN CATCH
				ROLLBACK
			END CATCH

			SELECT @d = @d + 1
		END
	END

	DROP TABLE #TPatient
END
GO

/****** Object:  StoredProcedure [dbo].[SP_Bluecard_ToGreenCard]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_Bluecard_ToGreenCard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_Bluecard_ToGreenCard]
GO


/****** Object:  StoredProcedure [dbo].[SP_Bluecard_ToGreenCard]    Script Date: 05/09/2017 17:08:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_Bluecard_ToGreenCard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_Bluecard_ToGreenCard] AS' 
END
GO

-- =============================================
-- Author:		<felix/stephen>
-- Create date: <03-22-2017>
-- Description:	<Patient registration migration from bluecard to greencard>
-- =============================================
ALTER PROCEDURE [dbo].[SP_Bluecard_ToGreenCard]
	-- Add the parameters for the stored procedure here
	@ptn_pk int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

DECLARE @FirstName varbinary(max), @MiddleName varbinary(max), @LastName varbinary(max), @Sex int, @Status bit , @DeleteFlag bit, 
		@CreateDate datetime, @UserID int,  @message varchar(80), @Id int, @PatientFacilityId varchar(50), @PatientType int, 
		@FacilityId varchar(10), @DateOfBirth datetime, @DobPrecision int, @NationalId varchar(100), @PatientId int,  
		@ARTStartDate date,@transferIn int,@CCCNumber varchar(20), @entryPoint int, @ReferredFrom int, @RegistrationDate datetime,
		@MaritalStatusId int, @MaritalStatus int, @DistrictName varchar(50), @CountyID int, @SubCountyID int, @WardID int,
		@Address varbinary(max), @Phone varbinary(max), @EnrollmentId int, @PatientIdentifierId int, @ServiceEntryPointId int,
		@PatientMaritalStatusID int, @PatientTreatmentSupporterID int, @PersonContactID int, @IDNational varbinary(max);

DECLARE @FirstNameT varchar(50), @LastNameT varchar(50), @TreatmentSupportTelNumber varbinary(max), 
			@CreateDateT datetime, @UserIDT int, @IDT int;
			
DECLARE @TreatmentSupportTelNumber_VARCHAR varchar(100);
  
PRINT '-------- Patients Report --------'; 
SELECT @message = '----- ptn_pk ' + CAST(@ptn_pk as varchar(50));
PRINT @message;
  
--DECLARE mstPatient_cursor CURSOR FOR   
Select Top 1 @FirstName = FirstName
		   , @MiddleName = MiddleName
		   , @LastName = LastName
		   , @Sex = Sex
		   , @Status = [Status]
		   , @DeleteFlag = DeleteFlag
		   , @CreateDate = dbo.mst_Patient.CreateDate
		   , @UserId = dbo.mst_Patient.UserID
		   , @PatientFacilityId = PatientFacilityId
		   , @FacilityId = PosId
		   , @DateOfBirth = DOB
		   , @DobPrecision = DobPrecision
		   , @NationalId = [ID/PassportNo]
		   , @CCCNumber = PatientEnrollmentID
		   , @ReferredFrom = [ReferredFrom]
		   , @RegistrationDate = [RegistrationDate]
		   , @MaritalStatus = MaritalStatus
		   , @DistrictName = DistrictName
		   , @Address = [Address]
		   , @Phone = Phone
From mst_Patient
Inner Join dbo.Lnk_PatientProgramStart On dbo.mst_Patient.Ptn_Pk = dbo.Lnk_PatientProgramStart.Ptn_pk
Where (dbo.Lnk_PatientProgramStart.ModuleId = 203)
	And dbo.mst_Patient.Ptn_Pk = @ptn_pk
  
--OPEN mstPatient_cursor  
  
--FETCH NEXT FROM mstPatient_cursor   
--INTO @FirstName, @MiddleName, @LastName, @Sex, @Status, @DeleteFlag, @CreateDate, @UserID, @PatientFacilityId, @FacilityId, @DateOfBirth, @DobPrecision, @NationalId,@CCCNumber, @ReferredFrom, @RegistrationDate, @MaritalStatus , @DistrictName, @Address, @Phone
  
IF @@rowcount = 1 BEGIN  
	PRINT ' '  
	SELECT @message = '----- patients From mst_patient: ' + CAST(@ptn_pk as varchar(50))
  
	PRINT @message  

	exec pr_OpenDecryptedSession;

	--set null dates
	Select @CreateDate = Isnull(@CreateDate, getdate()), 
		@Status = Case when @Status = 1 Then 0 Else 1 End,
		@IDNational = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),Isnull(@NationalId,'99999999')); 
	
	SET @Sex = (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName = 'Gender' and ItemName = (select top 1 Name from mst_Decode where id = @Sex));
	IF @Sex IS  NULL
		SET @Sex = (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');

		

	--Default all persons to new
	SET @ARTStartDate=( SELECT ARTTransferInDate FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk=@ptn_pk);
	if(@ARTStartDate Is NULL) BEGIN 
		SET @PatientType=(SELECT top 1 Id FROM LookupItem WHERE Name='New');
		SET @transferIn=0; 
	END 
	ELSE BEGIN 
		SET @PatientType=(SELECT Top 1 Id FROM LookupItem WHERE Name='TransferIn');
		SET @transferIn=1; 
	End
	-- SELECT @PatientType = 1285

	--encrypt nationalid
	--SET @IDNational=ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@IDNational);

	IF NOT EXISTS ( SELECT TOP 1 ptn_pk FROM Patient WHERE ptn_pk = @ptn_pk) BEGIN
			Insert into Person(FirstName, MidName, LastName, Sex, Active, DeleteFlag, CreateDate, CreatedBy)
			Values(@FirstName, @MiddleName, @LastName, @Sex, @Status, @DeleteFlag, @CreateDate, @UserID);

			SELECT @Id= scope_identity();
			SELECT @message = 'Created Person Id: ' + CAST(@Id as varchar(50));
			PRINT @message;

			Insert into Patient(ptn_pk, PersonId, PatientIndex, PatientType, FacilityId, Active, DateOfBirth, DobPrecision, NationalId, DeleteFlag, CreatedBy, CreateDate, RegistrationDate)
			Values(@ptn_pk, @Id, @PatientFacilityId, @PatientType, @FacilityId, @Status, @DateOfBirth, @DobPrecision, @IDNational, @DeleteFlag, @UserID, @CreateDate, @RegistrationDate);

			SELECT @PatientId=scope_identity();
			SELECT @message = 'Created Patient Id: ' + CAST(@PatientId as varchar);
			PRINT @message;

			Update mst_Patient Set MovedToPatientTable =1 Where Ptn_Pk=@ptn_pk;
			INSERT INTO [dbo].[GreenCardBlueCard_Transactional] ([PersonId] ,[Ptn_Pk]) VALUES (@Id , @ptn_pk);

			-- Insert to PatientEnrollment
			INSERT INTO [dbo].[PatientEnrollment] ([PatientId] ,[ServiceAreaId] ,[EnrollmentDate] ,[EnrollmentStatusId] ,[TransferIn] ,[CareEnded] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[AuditData])
			VALUES (@PatientId,1,(SELECT top 1 StartDate FROM Lnk_PatientProgramStart WHERE Ptn_pk=@ptn_pk),0, @transferIn,0 ,0 ,@UserID ,@CreateDate ,NULL)
		
			SELECT @EnrollmentId=scope_identity();
			SELECT @message = 'Created PatientEnrollment Id: ' + CAST(@EnrollmentId as varchar);
			PRINT @message;

			IF @CCCNumber IS NOT NULL BEGIN
					-- Patient Identifier
					INSERT INTO [dbo].[PatientIdentifier] ([PatientId], [PatientEnrollmentId], [IdentifierTypeId], [IdentifierValue] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[Active] ,[AuditData])
					VALUES (@PatientId , @EnrollmentId ,(select top 1 Id from Identifiers where Code='CCCNumber') ,@CCCNumber ,0 ,@UserID ,@CreateDate ,0 ,NULL);

					SELECT @PatientIdentifierId=@@IDENTITY;
					SELECT @message = 'Created PatientIdentifier Id: ' + CAST(@PatientIdentifierId as varchar);
					PRINT @message;
			END

			--Insert into ServiceEntryPoint
			IF @ReferredFrom > 0
				SET @entryPoint = (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (SELECT Name FROM mst_Decode WHERE ID=@ReferredFrom AND CodeID=17) + '%');
			IF @entryPoint IS NULL BEGIN
						SET @entryPoint = (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
			END
			ELSE
				SET @entryPoint = (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');

			INSERT INTO ServiceEntryPoint([PatientId], [ServiceAreaId], [EntryPointId], [DeleteFlag], [CreatedBy], [CreateDate], [Active])
			VALUES(@PatientId, 1, @entryPoint, 0 , @UserID, @CreateDate, 0);

			SELECT @ServiceEntryPointId=@@IDENTITY;
			SELECT @message = 'Created ServiceEntryPoint Id: ' + CAST(@ServiceEntryPointId as varchar);
			PRINT @message;
	
			--Insert into MaritalStatus
			IF @MaritalStatus > 0 BEGIN
					IF EXISTS (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (select Name from mst_Decode where ID = @MaritalStatus and CodeID = 12) + '%')
						SET @MaritalStatusId = (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (select Name from mst_Decode where ID = @MaritalStatus and CodeID = 12) + '%');
					ELSE
						SET @MaritalStatusId = (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
			END
			ELSE
				SET @MaritalStatusId = (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');

			INSERT INTO PatientMaritalStatus(PersonId, MaritalStatusId, Active, DeleteFlag, CreatedBy, CreateDate)
			VALUES(@Id, @MaritalStatusId, 1, 0, @UserID, @CreateDate);

			SELECT @PatientMaritalStatusID=scope_identity();
			SELECT @message = 'Created PatientMaritalStatus Id: ' + CAST(@PatientMaritalStatusID as varchar);
			PRINT @message;

			--Insert into PersonLocation
			----SET @CountyID = (SELECT TOP 1 CountyId from County where CountyName like '%' + @DistrictName  + '%');
			----SET @WardID = (SELECT TOP 1 WardId FROM County WHERE WardName LIKE '%' +  +'%')

			----INSERT INTO PersonLocation(PersonId, County, SubCounty, Ward, Village, Location, SubLocation, LandMark, NearestHealthCentre, Active, DeleteFlag, CreatedBy, CreateDate)
			----VALUES(@Id, @CountyID, @SubCountyID, @WardID, @Village, @Location, @SubLocation, @LandMark, @NearestHealthCentre, 1, @DeleteFlag, @UserID, @CreateDate);
	
			--Insert into Treatment Supporter
			--DECLARE Treatment_Supporter_cursor CURSOR FOR
			Select Top 1 @FirstNameT = substring(TreatmentSupporterName, 0, charindex(' ', TreatmentSupporterName))--									  As firstname
				 , @LastNameT = substring(TreatmentSupporterName, charindex(' ', TreatmentSupporterName) + 1, len(TreatmentSupporterName) + 1) --As lastname
				 , @TreatmentSupportTelNumber_VARCHAR =TreatmentSupportTelNumber
				 , @CreateDateT = CreateDate
				 , @UserIDT= UserID
			From dtl_PatientContacts
			Where ptn_pk = @ptn_pk And Nullif(TreatmentSupportName,'')	Is Not Null;

			--OPEN Treatment_Supporter_cursor
			--FETCH NEXT FROM Treatment_Supporter_cursor INTO @FirstNameT, @LastNameT, @TreatmentSupportTelNumber_VARCHAR, @CreateDateT , @UserIDT

			--IF @@FETCH_STATUS <> 0   
			--	PRINT '         <<None>>'       
			
			IF @@rowcount = 1			BEGIN  

				--SELECT @message = '         ' + @product  
				--PRINT @message
				--SET @TreatmentSupportTelNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber_VARCHAR);
				IF @FirstNameT IS NOT NULL AND @LastNameT IS NOT NULL BEGIN
						Insert into Person(FirstName, MidName, LastName, Sex, Active, DeleteFlag, CreateDate, CreatedBy)
						Values(ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstNameT), NULL, ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastNameT), (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown'), 1, 0, @CreateDateT, @UserIDT);

						SELECT @IDT=scope_identity();
						SELECT @message = 'Created Person Treatment Supporter Id: ' + CAST(@IDT as varchar(50));
						PRINT @message;

						IF @TreatmentSupportTelNumber_VARCHAR IS NOT NULL
						 SET @TreatmentSupportTelNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber_VARCHAR)

						INSERT INTO PatientTreatmentSupporter(PersonId, [SupporterId], [MobileContact], [DeleteFlag], [CreatedBy], [CreateDate])
						VALUES(@Id, @IDT, @TreatmentSupportTelNumber, 0, @UserIDT, @CreateDateT);

						SELECT @PatientTreatmentSupporterID=scope_identity();
						SELECT @message = 'Created PatientTreatmentSupporterID Id: ' + CAST(@PatientTreatmentSupporterID as varchar);
						PRINT @message;
				END

			--	FETCH NEXT FROM Treatment_Supporter_cursor INTO  @FirstNameT, @LastNameT, @TreatmentSupportTelNumber_VARCHAR, @CreateDateT, @UserIDT
			--	END  

			--CLOSE Treatment_Supporter_cursor  
			--DEALLOCATE Treatment_Supporter_cursor

			--Insert into Person Contact
				IF @Address IS NOT NULL OR @Phone IS NOT NULL	BEGIN
					INSERT INTO PersonContact(PersonId, [PhysicalAddress], [MobileNumber], [AlternativeNumber], [EmailAddress], [Active], [DeleteFlag], [CreatedBy], [CreateDate])
					VALUES(@Id, @Address, @Phone, null, null, @Status, 0, @UserID, @CreateDate);

					SELECT @PersonContactID=scope_identity();
					SELECT @message = 'Created PersonContact Id: ' + CAST(@PersonContactID as varchar);
					PRINT @message;
				END

			END
		End
		ELSE	BEGIN
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
			SELECT @message = 'Updated Patient ' +  cast(@PatientId as varchar);
			PRINT @message;

			

			UPDATE PatientEnrollment
			SET EnrollmentDate = (SELECT top 1 StartDate FROM Lnk_PatientProgramStart WHERE Ptn_pk=@ptn_pk), EnrollmentStatusId = 0, TransferIn = @transferIn, CareEnded = 0, DeleteFlag = 0, CreatedBy = @UserID, CreateDate = @CreateDate
			WHERE PatientId = @PatientId;
			If(@@rowcount = 0) Begin
			INSERT INTO [dbo].[PatientEnrollment] ([PatientId] ,[ServiceAreaId] ,[EnrollmentDate] ,[EnrollmentStatusId] ,[TransferIn] ,[CareEnded] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[AuditData])
			VALUES (@PatientId,1,(SELECT top 1 StartDate FROM Lnk_PatientProgramStart WHERE Ptn_pk=@ptn_pk),0, @transferIn,0 ,0 ,@UserID ,@CreateDate ,NULL)
			End

			SELECT @EnrollmentId = (SELECT TOP 1 Id FROM PatientEnrollment WHERE PatientId = @PatientId and ServiceAreaId=1);
			SELECT @message = 'Updated PatientEnrollment Id: ' + CAST(@EnrollmentId as varchar);
			PRINT @message;

			IF @CCCNumber IS NOT NULL		BEGIN
				IF NOT EXISTS ( SELECT PatientId FROM PatientIdentifier WHERE PatientId = @PatientId AND PatientEnrollmentId = @EnrollmentId AND IdentifierTypeId = (SELECT Id FROM LookupItem WHERE Name='CCCNumber'))
					BEGIN
						-- Patient Identifier
						INSERT INTO [dbo].[PatientIdentifier] ([PatientId], [PatientEnrollmentId], [IdentifierTypeId], [IdentifierValue] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[Active] ,[AuditData])
						VALUES (@PatientId , @EnrollmentId ,(select top 1 Id from Identifiers where Code='CCCNumber') ,@CCCNumber ,0 ,@UserID ,@CreateDate ,0 ,NULL);

						SELECT @PatientIdentifierId=scope_identity();
						SELECT @message = 'Created PatientIdentifier Id: ' + CAST(@PatientIdentifierId as varchar);
						PRINT @message;
					END
				ELSE					BEGIN
						UPDATE PatientIdentifier
						SET IdentifierTypeId = (select top 1 Id from Identifiers where Code='CCCNumber'), IdentifierValue = @CCCNumber, DeleteFlag = 0, CreatedBy = @UserID, CreateDate = @CreateDate, Active = 0
						WHERE PatientId = @PatientId AND PatientEnrollmentId = @EnrollmentId AND IdentifierTypeId = (SELECT Id FROM LookupItem WHERE Name='CCCNumber')
				END
			END
			

			--Insert into ServiceEntryPoint
			IF @ReferredFrom > 0
				BEGIN
					SET @entryPoint = (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (SELECT Name FROM mst_Decode WHERE ID=@ReferredFrom AND CodeID=17) + '%');
					
					IF @entryPoint IS NULL
						BEGIN
							SET @entryPoint = (select top 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown');
						END
						
					UPDATE ServiceEntryPoint
					SET EntryPointId = @entryPoint, CreatedBy = @UserID, CreateDate = @CreateDate
					WHERE PatientId = @PatientId;
					
					SELECT @ServiceEntryPointId=scope_identity();
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

					SELECT @PatientMaritalStatusID=scope_identity();
					SELECT @message = 'Updated PatientMaritalStatus Id: ' + CAST(@PatientMaritalStatusID as varchar);
					PRINT @message;
				END

			--Update into Treatment Supporter
			--DECLARE Treatment_Supporter_cursor CURSOR FOR
			Select top 1 @FirstNameT = substring(TreatmentSupporterName, 0, charindex(' ', TreatmentSupporterName))									--  As firstname
				 , @LastNameT = substring(TreatmentSupporterName, charindex(' ', TreatmentSupporterName) + 1, len(TreatmentSupporterName) + 1) --As lastname
				 , @TreatmentSupportTelNumber_VARCHAR= TreatmentSupportTelNumber
				 , @CreateDateT=CreateDate
				 , @UserIDT = UserID
			From dtl_PatientContacts
			Where ptn_pk = @ptn_pk and nullif(TreatmentSupporterName,'') Is Not Null;


			--OPEN Treatment_Supporter_cursor
			--FETCH NEXT FROM Treatment_Supporter_cursor INTO @FirstNameT, @LastNameT, @TreatmentSupportTelNumber_VARCHAR, @CreateDateT , @UserIDT

			--IF @@FETCH_STATUS <> 0   
			--	PRINT '         <<None>>'       

			--WHILE @@FETCH_STATUS = 0  
			If(@@rowcount =1 ) BEGIN

				--SET @TreatmentSupportTelNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber);
				IF @FirstNameT IS NOT NULL AND @LastNameT IS NOT NULL
					BEGIN
						IF NOT EXISTS (SELECT PersonId FROM PatientTreatmentSupporter WHERE PersonId = @Id)
							BEGIN
								Insert into Person(FirstName, MidName, LastName, Sex, Active, DeleteFlag, CreateDate, CreatedBy)
								Values(ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstNameT), NULL, ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastNameT), (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown'), 1, 0, getdate(), @UserIDT);

								SELECT @IDT=scope_identity();
								SELECT @message = 'Created Person Treatment Supporter Id: ' + CAST(@IDT as varchar(50));
								PRINT @message;

								IF @TreatmentSupportTelNumber_VARCHAR IS NOT NULL
								SET @TreatmentSupportTelNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber_VARCHAR)

								INSERT INTO PatientTreatmentSupporter(PersonId, [SupporterId], [MobileContact], [DeleteFlag], [CreatedBy], [CreateDate])
								VALUES(@Id, @IDT, @TreatmentSupportTelNumber, 0, @UserIDT, getdate());

							END
						ELSE
							BEGIN
								SET @IDT = (SELECT SupporterId FROM PatientTreatmentSupporter WHERE PersonId = @Id);

								UPDATE Person
								SET FirstName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstNameT), LastName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastNameT)
								WHERE Id = @IDT;

								IF @TreatmentSupportTelNumber_VARCHAR IS NOT NULL
								SET @TreatmentSupportTelNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber_VARCHAR)

								UPDATE PatientTreatmentSupporter
								SET MobileContact = @TreatmentSupportTelNumber
								WHERE PersonId = @Id;

							END
						END

				--FETCH NEXT FROM Treatment_Supporter_cursor INTO  @FirstNameT, @LastNameT, @TreatmentSupportTelNumber_VARCHAR, @CreateDateT, @UserIDT
				END 

			--CLOSE Treatment_Supporter_cursor  
			--DEALLOCATE Treatment_Supporter_cursor

			--UPDATE into Person Contact
			IF @Address IS NOT NULL OR @Phone IS NOT NULL
				BEGIN
					UPDATE PersonContact
					SET PhysicalAddress = Isnull(@Address,PhysicalAddress), MobileNumber = Isnull(@Phone,MobileNumber)
					WHERE PersonId = @Id;
					If @@rowcount = 0 Begin
					INSERT INTO PersonContact(PersonId, [PhysicalAddress], [MobileNumber], [AlternativeNumber], [EmailAddress], [Active], [DeleteFlag], [CreatedBy], [CreateDate])
					VALUES(@Id, @Address, @Phone, null, null, @Status, 0, @UserID, @CreateDate);
					end
				END

		END

	-- Get the next mst_patient.
  --  FETCH NEXT FROM mstPatient_cursor   
   -- INTO @FirstName, @MiddleName, @LastName, @Sex, @Status, @DeleteFlag, @CreateDate, @UserID, @PatientFacilityId, @FacilityId, @DateOfBirth, @DobPrecision, @NationalId, @CCCNumber, @ReferredFrom, @RegistrationDate, @MaritalStatus , @DistrictName, @Address, @Phone
END   
--CLOSE mstPatient_cursor;  
--DEALLOCATE mstPatient_cursor;  
END



/****** Object:  StoredProcedure [dbo].[BlueCardGreencard_sync]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlueCardGreencard_sync]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BlueCardGreencard_sync]
GO


/****** Object:  StoredProcedure [dbo].[BlueCardGreencard_sync]    Script Date: 05/09/2017 17:08:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BlueCardGreencard_sync]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[BlueCardGreencard_sync] AS' 
END
GO
-- =============================================
-- Author: Felix
-- Create date: 07-17-2017
-- Description:	Sync Blue Card demographics, baseline, careending, etc
-- =============================================
ALTER PROCEDURE [dbo].[BlueCardGreencard_sync]
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
						SELECT @message = '----- Syncing patient : ' + CAST(@ptn_pk as varchar(50));
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

						SET @transferIn=0;
						--Default all persons to new
						SET @ARTStartDate=( SELECT top 1  ARTTransferInDate FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk=@ptn_pk And ARTTransferInDate Is Not Null);
						if(@ARTStartDate Is NULL) BEGIN SET @PatientType=(SELECT top 1 Id FROM LookupItem WHERE Name='New');SET @transferIn=0; END ELSE BEGIN SET @PatientType=(SELECT top 1 Id FROM LookupItem WHERE Name='Transfer-In');SET @transferIn=1; END
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

								SELECT @message = 'SET CAREENDING FOR : ' + CAST(@ptn_pk as varchar(50));
								PRINT @message;
							END
						

						IF EXISTS(SELECT * FROM Patient WHERE ptn_pk = @ptn_pk)
							BEGIN
								SET @PersonId = (SELECT PersonId FROM Patient WHERE ptn_pk = @ptn_pk);
								SET @PatientId = (SELECT Id FROM Patient WHERE ptn_pk = @ptn_pk);

								UPDATE Person SET
								FirstName = @FirstName,
								MidName = @MiddleName,
								LastName = @LastName,
								Sex = @Sex,
								Active = @Status_Greencard
								WHERE Id = @PersonId;

								SELECT @message = 'Updated Person Id: ' + CAST(@PersonId as varchar(50));
								PRINT @message;

								UPDATE Patient SET
								PatientIndex = @PatientFacilityId,
								PatientType = @PatientType,
								FacilityId = @FacilityId,
								Active = @Status_Greencard,
								DateOfBirth = @DateOfBirth,
								DobPrecision = @DobPrecision,
								NationalId = @NationalId,
								RegistrationDate = @RegistrationDate
								WHERE ptn_pk = @ptn_pk;

								SELECT @message = 'Updated Patient ptn: ' + CAST(@ptn_pk as varchar(50));
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
																IF EXISTS(SELECT * FROM PatientEnrollment WHERE PatientId = @PatientId)
																	BEGIN
																		UPDATE PatientEnrollment SET
																		EnrollmentDate = @StartDate,
																		CareEnded = @Status
																		WHERE PatientId = @PatientId

																		SET @EnrollmentId = (SELECT TOP 1 Id FROM PatientEnrollment WHERE PatientId = @PatientId);
																	END;
																ELSE
																	BEGIN
																		INSERT INTO [dbo].[PatientEnrollment] ([PatientId] ,[ServiceAreaId] ,[EnrollmentDate] ,[EnrollmentStatusId] ,[TransferIn] ,[CareEnded] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[AuditData])
																		VALUES (@PatientId,1, @StartDate,0, @transferIn, @Status ,0 ,@UserID_Enrollment ,@CreateDate_Enrollment ,NULL);

																		SELECT @EnrollmentId = SCOPE_IDENTITY();
																		SELECT @message = 'Created PatientEnrollment Id: ' + CAST(@EnrollmentId as varchar);
																		PRINT @message;
																	END
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
								DROP TABLE #TLnk_PatientProgramStart;

								IF @CCCNumber IS NOT NULL AND @ModuleId = 203
									BEGIN
										IF EXISTS(SELECT * FROM PatientIdentifier WHERE PatientId = @PatientId)
											BEGIN
												UPDATE PatientIdentifier SET
												IdentifierValue = @CCCNumber
												WHERE PatientId = @PatientId AND [IdentifierTypeId] = (select top 1 Id from Identifiers where Code='CCCNumber');

												SELECT @message = 'Updated PatientIdentifier for : ' + CAST(@ptn_pk as varchar(50));
												PRINT @message;

												SET @PatientIdentifierId = (SELECT TOP 1 Id FROM PatientIdentifier WHERE PatientId = @PatientId AND [IdentifierTypeId] = (select top 1 Id from Identifiers where Code='CCCNumber'));
											END;
										ELSE
											BEGIN
												-- Patient Identifier
												INSERT INTO [dbo].[PatientIdentifier] ([PatientId], [PatientEnrollmentId], [IdentifierTypeId], [IdentifierValue] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[Active] ,[AuditData])
												VALUES (@PatientId , @EnrollmentId ,(select top 1 Id from Identifiers where Code='CCCNumber') ,@CCCNumber ,0 ,@UserID ,@CreateDate ,0 ,NULL);

												SELECT @PatientIdentifierId = SCOPE_IDENTITY();
												SELECT @message = 'Created PatientIdentifier Id: ' + CAST(@PatientIdentifierId as varchar);
												PRINT @message;
											END
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

								IF EXISTS(SELECT * FROM ServiceEntryPoint WHERE PatientId = @PatientId)
									BEGIN
										UPDATE ServiceEntryPoint SET
										EntryPointId = @entryPoint
										WHERE PatientId = @PatientId;

										SELECT @message = 'Updated ServiceEntryPoint for : ' + CAST(@ptn_pk as varchar(50));
										PRINT @message;

										SET @ServiceEntryPointId = (SELECT TOP 1 Id FROM ServiceEntryPoint WHERE PatientId = @PatientId);
									END;
								ELSE
									BEGIN
										INSERT INTO ServiceEntryPoint([PatientId], [ServiceAreaId], [EntryPointId], [DeleteFlag], [CreatedBy], [CreateDate], [Active])
										VALUES(@PatientId, 1, @entryPoint, 0 , @UserID, @CreateDate, 0);

										SELECT @ServiceEntryPointId = SCOPE_IDENTITY();
										SELECT @message = 'Created ServiceEntryPoint Id: ' + CAST(@ServiceEntryPointId as varchar);
										PRINT @message;
									END;

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

								IF EXISTS(SELECT * FROM PatientMaritalStatus WHERE PersonId=@PersonId)
									BEGIN
										UPDATE PatientMaritalStatus SET
										MaritalStatusId = @MaritalStatusId
										WHERE PersonId = @PersonId AND DeleteFlag = 0;

										SELECT @message = 'Updated PatientMaritalStatus for : ' + CAST(@ptn_pk as varchar(50));
										PRINT @message;

										SET @PatientMaritalStatusID = (SELECT TOP 1 Id FROM PatientMaritalStatus WHERE PersonId = @PersonId AND DeleteFlag = 0);
									END;
								ELSE
									BEGIN
										INSERT INTO PatientMaritalStatus(PersonId, MaritalStatusId, Active, DeleteFlag, CreatedBy, CreateDate)
										VALUES(@PersonId, @MaritalStatusId, 1, 0, @UserID, @CreateDate);

										SELECT @PatientMaritalStatusID = SCOPE_IDENTITY();
										SELECT @message = 'Created PatientMaritalStatus Id: ' + CAST(@PatientMaritalStatusID as varchar);
										PRINT @message;
									END;

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
														IF EXISTS(SELECT * FROM PatientTreatmentSupporter WHERE PersonId = @PersonId)
															BEGIN
																SELECT @IDT = (SELECT TOP 1 SupporterId FROM PatientTreatmentSupporter WHERE PersonId = @PersonId);
																UPDATE Person SET
																FirstName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstNameT),
																LastName = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastNameT)
																WHERE Id = @IDT;

																SELECT @message = 'Updated PatientTreatmentSupporter for : ' + CAST(@ptn_pk as varchar(50));
																PRINT @message;

																SET @PatientTreatmentSupporterID = (SELECT TOP 1 Id FROM PatientTreatmentSupporter WHERE PersonId = @PersonId);
															END;
														ELSE
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
															END;
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

								print 'after treatment supporter';
								--Insert into Person Contact
								IF @Address IS NOT NULL OR @Phone IS NOT NULL
									BEGIN
										print 'try address or phone for PersonId: ' + CAST(@PersonId as varchar(50));

										IF EXISTS(SELECT * FROM PersonContact WHERE PersonId = @PersonId)
											BEGIN
												UPDATE PersonContact SET
												PhysicalAddress = @Address,
												MobileNumber = @Phone
												WHERE PersonId = @PersonId;


												SELECT @message = 'Updated PersonContact for : ' + CAST(@ptn_pk as varchar(50));
												PRINT @message;

												SET @PersonContactID = (SELECT TOP 1 Id FROM PersonContact WHERE PersonId = @PersonId);
											END;
										ELSE
											BEGIN
												print 'try insert address or phone for PersonId: ' + CAST(@PersonId as varchar(50));
												--print 'Address: '+ CAST(@Address as varchar(50));
												--print 'Phone: '+ CAST(@Phone as varchar(50));

												INSERT INTO PersonContact(PersonId, [PhysicalAddress], [MobileNumber], [AlternativeNumber], [EmailAddress], [Active], [DeleteFlag], [CreatedBy], [CreateDate])
												VALUES(@PersonId, @Address, @Phone, null, null, @Status, 0, @UserID, @CreateDate);

												SELECT @PersonContactID = SCOPE_IDENTITY();
												SELECT @message = 'Created PersonContact Id: ' + CAST(@PersonContactID as varchar);
												PRINT @message;
											END;
									END

								--Starting baseline
								print 'starting baseline';

								DECLARE @HBVInfected bit, @Pregnant bit, @TBinfected bit, @WHOStage int, @WHOStageString varchar(50), @BreastFeeding bit, 
										@CD4Count decimal , @MUAC decimal, @Weight decimal, @Height decimal, @artstart datetime,
										@ClosestARVDate datetime, @PatientMasterVisitId int, @HIVDiagnosisDate datetime, @EnrollmentDate datetime,
										@EnrollmentWHOStage int, @EnrollmentWHOStageString varchar(50), @VisitDate datetime, @Cohort varchar(50), @visit_id int;

								IF @transferIn = 1
									BEGIN
										SET @artstart = @ARTStartDate
									END
								ELSE
									BEGIN
										Select TOP 1 @artstart = ARTStartDate	From mst_Patient	Where Ptn_Pk = @ptn_pk	And LocationID = @LocationId;
									END

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
								IF @EnrollmentDate IS NULL BEGIN SET @EnrollmentDate = @StartDate END;

								IF EXISTS(SELECT * FROM PatientMasterVisit WHERE PatientId = @PatientId AND VisitType = (SELECT top 1 ItemId FROM LookupItemView WHERE	MasterName like '%VisitType%' and ItemName like '%Enrollment%'))
								BEGIN SET @PatientMasterVisitId = (SELECT TOP 1 Id FROM  PatientMasterVisit WHERE PatientId = @PatientId AND VisitType = (SELECT top 1 ItemId FROM LookupItemView WHERE	MasterName like '%VisitType%' and ItemName like '%Enrollment%')); END;
								ELSE
									BEGIN
										INSERT INTO PatientMasterVisit(PatientId, ServiceId, Start, [End], Active, VisitDate, VisitScheduled, VisitBy, VisitType, [Status], CreateDate, DeleteFlag, CreatedBy)
										VALUES(@PatientId, 1, @EnrollmentDate, NULL, 0, @VisitDate, NULL, NULL, (SELECT top 1 ItemId FROM LookupItemView WHERE	MasterName like '%VisitType%' and ItemName like '%Enrollment%'), NULL, GETDATE(), 0 , @UserID);

										SET @PatientMasterVisitId = SCOPE_IDENTITY();

										SELECT @message = 'Created PatientMasterVisit Id: ' + CAST(@PatientMasterVisitId as varchar);
										PRINT @message;
									END


								IF @Status = 1
									BEGIN
										SELECT @message = 'Try to update PatientCareending for: ' + CAST(@ptn_pk as varchar(50));
										PRINT @message;

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

										IF EXISTS(SELECT * FROM PatientCareending WHERE PatientId = @PatientId AND PatientMasterVisitId = @PatientMasterVisitId AND PatientEnrollmentId = @EnrollmentId)
										BEGIN
											UPDATE PatientCareending SET
											ExitReason = @ExitReason,
											ExitDate = @ExitDate,
											DateOfDeath = @DateOfDeath
											WHERE PatientId = @PatientId AND PatientMasterVisitId = @PatientMasterVisitId AND PatientEnrollmentId = @EnrollmentId;

											SELECT @message = 'Updated PatientCareending Id: ' + CAST(@ptn_pk as varchar(50));
											PRINT @message;
										END;
										ELSE
											BEGIN
												print '@PatientId: '+ CAST(@PatientId as varchar(50));
												print '@PatientMasterVisitId: '+ CAST(@PatientMasterVisitId as varchar(50));
												print '@EnrollmentId: '+ CAST(@EnrollmentId as varchar(50));

												INSERT INTO [dbo].[PatientCareending] ([PatientId] ,[PatientMasterVisitId] ,[PatientEnrollmentId] ,[ExitReason] ,[ExitDate] ,[TransferOutfacility] ,[DateOfDeath] ,[CareEndingNotes] ,[Active] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[AuditData])
												VALUES(@PatientId ,@PatientMasterVisitId ,@EnrollmentId ,@ExitReason , @ExitDate ,NULL ,@DateOfDeath ,NULL ,0 ,0,@UserID_CareEnded ,@CreateDate_CareEnded ,NULL);

												SELECT @message = 'Created PatientCareending Id: ' + CAST(SCOPE_IDENTITY() as varchar);
												PRINT @message;
											END
									END

								IF (@Weight IS NOT NULL AND @Height IS NOT NULL AND @Weight > 0 AND @Height > 0)
								BEGIN
									IF EXISTS(SELECT * FROM PatientBaselineAssessment WHERE PatientId = @PatientId AND PatientMasterVisitId = @PatientMasterVisitId)
									BEGIN
										UPDATE PatientBaselineAssessment SET
										[Pregnant] = @Pregnant,
										[TBinfected] = @TBinfected, 
										[WHOStage] = @WHOStage, 
										[BreastFeeding] = @BreastFeeding, 
										[CD4Count] = @CD4Count, 
										[MUAC] = @MUAC, 
										[Weight] = @Weight, 
										[Height] = @Height
										WHERE PatientId = @PatientId AND PatientMasterVisitId = @PatientMasterVisitId;

										SELECT @message = 'Updated PatientBaselineAssessment: ' + CAST(@ptn_pk as varchar(50));
										PRINT @message;
									END
									ELSE
									BEGIN
										INSERT INTO [dbo].[PatientBaselineAssessment]([PatientId], [PatientMasterVisitId], [HBVInfected], [Pregnant], [TBinfected], [WHOStage], [BreastFeeding], [CD4Count], [MUAC], [Weight], [Height], [DeleteFlag], [CreatedBy], [CreateDate] )
										VALUES(@PatientId, @PatientMasterVisitId, 0, @Pregnant, @TBinfected, @WHOStage, @BreastFeeding, @CD4Count, @MUAC, @Weight, @Height, 0 , @UserID, GETDATE());

										SELECT @message = 'Created PatientBaselineAssessment Id: ' + CAST(SCOPE_IDENTITY() as varchar);
										PRINT @message;
									END
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

									IF @TransferInDate = CONVERT(datetime, '1900-01-01', 104)
									BEGIN
										SET @TransferInDate = NULL;
									END

									IF @transferIn = 1 AND @TreatmentStartDate IS NOT NULL AND @CurrentART IS NOT NULL AND @FacilityFrom IS NOT NULL AND @MFLCODE IS NOT NULL
									BEGIN
										IF EXISTS(SELECT * FROM PatientTransferIn WHERE PatientId = @PatientId AND PatientMasterVisitId = @PatientMasterVisitId)
										BEGIN
											UPDATE PatientTransferIn SET
											TransferInDate = @TransferInDate,
											TreatmentStartDate = @TreatmentStartDate,
											[CurrentTreatment] = @CurrentART,  
											[FacilityFrom] = @FacilityFrom, 
											[MFLCode] = @MFLCODE
											WHERE PatientId = @PatientId AND PatientMasterVisitId = @PatientMasterVisitId

											SELECT @message = 'Updated PatientTransferIn PatientId: ' + CAST(@PatientId as varchar(50));
											PRINT @message;
										END;
										ELSE
											BEGIN
												INSERT INTO [dbo].[PatientTransferIn]([PatientId], [PatientMasterVisitId], [ServiceAreaId], [TransferInDate], [TreatmentStartDate], [CurrentTreatment],  [FacilityFrom] , [MFLCode] ,[CountyFrom] , [TransferInNotes], [DeleteFlag] ,[CreatedBy] , [CreateDate])
												VALUES(@PatientId, @PatientMasterVisitId, 1, @TransferInDate, @TreatmentStartDate, @CurrentART, @FacilityFrom, @MFLCODE, (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown'), ' ', 0 , @UserID, @CreateDateTransfer);

												SELECT @message = 'Created PatientTransferIn Id: ' + CAST(SCOPE_IDENTITY() as varchar);
												PRINT @message;
											END;
									END;
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
									IF EXISTS(SELECT * FROM PatientARVHistory WHERE PatientId = @PatientId AND PatientMasterVisitId = @PatientMasterVisitId)
										BEGIN
											UPDATE PatientARVHistory SET
											TreatmentType = @TreatmentType,
											Purpose = @Purpose,
											Regimen = @Regimen,
											DateLastUsed = @DateLastUsed
											WHERE PatientId = @PatientId AND PatientMasterVisitId = @PatientMasterVisitId;

											SELECT @message = 'Updated PatientARVHistory PatientId: ' + CAST(@PatientId as varchar(50));
											PRINT @message;
										END;
									ELSE
										BEGIN
											INSERT INTO [dbo].[PatientARVHistory]([PatientId], [PatientMasterVisitId], [TreatmentType], [Purpose] , [Regimen], [DateLastUsed], [DeleteFlag] , [CreatedBy] , [CreateDate])
											VALUES(@PatientId, @PatientMasterVisitId, @TreatmentType, @Purpose, @Regimen, @DateLastUsed, 0, @UserID, @CreateDate);

											SELECT @message = 'Created PatientARVHistory Id: ' + CAST(SCOPE_IDENTITY() as varchar);
											PRINT @message;
										END
									END
								END

								IF EXISTS(select TOP 1 FirstLineRegStDate from [dbo].[dtl_PatientARTCare] WHERE ptn_pk = @ptn_pk) AND @transferIn = 1
								BEGIN
									DECLARE @DateStartedOnFirstLine datetime;
									SET @DateStartedOnFirstLine = (select TOP 1 FirstLineRegStDate from [dbo].[dtl_PatientARTCare] WHERE ptn_pk = @ptn_pk);

									--IF @DateStartedOnFirstLine IS NULL
									--BEGIN
									--	SET @DateStartedOnFirstLine = GETDATE();
									--	SET @Cohort = (select  convert(char(3),GETDATE() , 0) + ' ' + CONVERT(varchar(10), year(GETDATE())));
									--END

									IF EXISTS(SELECT * FROM PatientTreatmentInitiation WHERE PatientId = @PatientId AND PatientMasterVisitId = @PatientMasterVisitId)
									BEGIN
										UPDATE PatientTreatmentInitiation SET
										DateStartedOnFirstLine = @DateStartedOnFirstLine,
										Cohort = @Cohort,
										[RegimenCode] = (SELECT TOP 1 FirstLineReg FROM dtl_PatientARTCare where ptn_pk = @ptn_pk)
										WHERE PatientId = @PatientId AND PatientMasterVisitId = @PatientMasterVisitId;

										SELECT @message = 'Updated PatientTreatmentInitiation PatientId: ' + CAST(@PatientId as varchar(50));
										PRINT @message;
									END;
									ELSE
									BEGIN
										INSERT INTO [dbo].[PatientTreatmentInitiation]([PatientMasterVisitId], [PatientId], [DateStartedOnFirstLine], [Cohort], Regimen, [RegimenCode] , [BaselineViralload] , [BaselineViralloadDate] , [DeleteFlag] , [CreatedBy] , [CreateDate] )
										VALUES(@PatientMasterVisitId, @PatientId, @DateStartedOnFirstLine, @Cohort, Null,(SELECT TOP 1 FirstLineReg FROM dtl_PatientARTCare where ptn_pk = @ptn_pk) , NULL, NULL, 0, @UserID, @CreateDate);

										SELECT @message = 'Created PatientTreatmentInitiation Id: ' + CAST(SCOPE_IDENTITY() as varchar);
										PRINT @message;
									END;
								END

								IF @EnrollmentWHOStage IS NOT NULL
									BEGIN
										IF EXISTS(SELECT * FROM PatientHivDiagnosis WHERE PatientMasterVisitId = @PatientMasterVisitId AND PatientId = @PatientId)
										BEGIN
											UPDATE PatientHivDiagnosis SET
											HIVDiagnosisDate = @HIVDiagnosisDate,
											EnrollmentDate = @EnrollmentDate,
											EnrollmentWHOStage = @EnrollmentWHOStage,
											ARTInitiationDate = @artstart
											WHERE PatientMasterVisitId = @PatientMasterVisitId AND PatientId = @PatientId;

											SELECT @message = 'Updated PatientHivDiagnosis PatientId: ' + CAST(@PatientId as varchar(50));
											PRINT @message;
										END;
										ELSE
										BEGIN
											INSERT INTO [dbo].[PatientHivDiagnosis]([PatientMasterVisitId] , [PatientId] , [HIVDiagnosisDate] , [EnrollmentDate] , [EnrollmentWHOStage] , [ARTInitiationDate] , [DeleteFlag] , [CreatedBy] , [CreateDate])
											VALUES(@PatientMasterVisitId, @PatientId, @HIVDiagnosisDate, @EnrollmentDate, @EnrollmentWHOStage, @artstart, 0 , @UserID, @CreateDate);

											SELECT @message = 'Created PatientHivDiagnosis Id: ' + CAST(SCOPE_IDENTITY() as varchar);
											PRINT @message;
										END;
									END

								--ending baseline

							END;
					COMMIT;
				END TRY

				BEGIN CATCH
					ROLLBACK
				END CATCH

				SELECT @i = @i + 1
			END
		END
		--Now Drop Temporary Tables
		 DROP TABLE #Tmst_Patient

		 exec [dbo].[pr_CloseDecryptedSession];

		 SELECT @message = 'Finished Running';
		 PRINT @message;
END


/****** Object:  StoredProcedure [dbo].[PatientDemographics_To_Greencard]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientDemographics_To_Greencard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PatientDemographics_To_Greencard]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientDemographics_To_Greencard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PatientDemographics_To_Greencard] AS' 
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[PatientDemographics_To_Greencard]
	@ptn_pk int,
	@FirstName varbinary(max),
	@MiddleName varbinary(max),
	@LastName varbinary(max),
	@Sex int,
	@Status_Greencard bit,
	@DeleteFlag bit,
	@CreateDate datetime, 
	@UserID int,
	@PatientFacilityId varchar(50), 
	@PatientType int,
	@FacilityId varchar(10),
	@DateOfBirth datetime, 
	@DobPrecision int, 
	@NationalId varbinary(max),
	@RegistrationDate datetime,
	@PersonId int OUTPUT,
	@PatientId int OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @message varchar(max);

	Insert into Person(FirstName, MidName, LastName, Sex, Active, DeleteFlag, CreateDate, CreatedBy)
	Values(@FirstName, @MiddleName, @LastName, @Sex, @Status_Greencard, @DeleteFlag, @CreateDate, @UserID);

	SET @PersonId = SCOPE_IDENTITY();
	SET @message = 'Created Person Id: ' + CAST(@PersonId as varchar(50));
	PRINT @message;

	Insert into Patient(ptn_pk, PersonId, PatientIndex, PatientType, FacilityId, Active, DateOfBirth, DobPrecision, NationalId, DeleteFlag, CreatedBy, CreateDate, RegistrationDate)
	Values(@ptn_pk, @PersonId, @PatientFacilityId, @PatientType, @FacilityId, @Status_Greencard, @DateOfBirth, @DobPrecision, @NationalId, @DeleteFlag, @UserID, @CreateDate, @RegistrationDate);

	SET @PatientId = SCOPE_IDENTITY();
	SET @message = 'Created Patient Id: ' + CAST(@PatientId as varchar);
	PRINT @message;

	SELECT @PersonId, @PatientId;
END


/****** Object:  StoredProcedure [dbo].[PatientEnrollment_To_Greencard]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientEnrollment_To_Greencard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PatientEnrollment_To_Greencard]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientEnrollment_To_Greencard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PatientEnrollment_To_Greencard] AS' 
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[PatientEnrollment_To_Greencard]
	-- Add the parameters for the stored procedure here
	@ptn_pk int,
	@transferIn int,
	@PatientId int,
	@Status bit,
	@EnrollmentId int OUTPUT,
	@ModuleId int OUTPUT,
	@StartDate datetime OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Insert into Enrollment Table
	DECLARE @message varchar(max);
	DECLARE @j INT = 1;
	DECLARE @countj INT;
	--Create Temporary Tables for storing data 
	CREATE TABLE #TLnk_PatientProgramStart (Id INT IDENTITY(1,1), ModuleId int, [StartDate] datetime, [UserID] int, [CreateDate] datetime);

	INSERT INTO #TLnk_PatientProgramStart(ModuleId, [StartDate], [UserID], [CreateDate])
	SELECT ModuleId, [StartDate], [UserID], [CreateDate] FROM Lnk_PatientProgramStart WHERE Ptn_pk=@ptn_pk;

	DECLARE @UserID_Enrollment int, @CreateDate_Enrollment datetime;

	SELECT @countj = COUNT(Id) FROM #TLnk_PatientProgramStart 

	BEGIN
		WHILE (@j <= @countj)
			BEGIN
				SELECT @ModuleId = ModuleId, @StartDate = [StartDate], @UserID_Enrollment = [UserID], @CreateDate_Enrollment = [CreateDate] FROM #TLnk_PatientProgramStart WHERE Id = @j AND ModuleId = 203;

				BEGIN TRY
					BEGIN TRANSACTION

							PRINT ' ';
							SET @message = '----- Enrollment Start Date: ' + CAST(@StartDate as varchar(50));
							PRINT @message;

								IF @ModuleId = 203
								BEGIN
									PRINT ' ';
									SET @message = '----- Transfer In is (1), New (0) : ' + CAST(@transferIn as varchar(50));
									PRINT @message;

									DECLARE @DateEnrolledInCare DATETIME;
									IF @transferIn = 1
										BEGIN
											SET @DateEnrolledInCare = (SELECT TOP 1 dbo.dtl_PatientHivPrevCareEnrollment.DateEnrolledInCare
																		FROM dbo.dtl_PatientHivPrevCareEnrollment INNER JOIN
																			dbo.ord_Visit ON dbo.dtl_PatientHivPrevCareEnrollment.ptn_pk = dbo.ord_Visit.Ptn_Pk 
																			AND dbo.dtl_PatientHivPrevCareEnrollment.Visit_pk = dbo.ord_Visit.Visit_Id INNER JOIN
																			dbo.mst_VisitType ON dbo.ord_Visit.VisitType = dbo.mst_VisitType.VisitTypeID
																			WHERE (dbo.mst_VisitType.VisitName = 'ART History') AND dbo.dtl_PatientHivPrevCareEnrollment.ptn_pk = @ptn_pk);

											IF @DateEnrolledInCare IS NOT NULL
												BEGIN
													SET @StartDate = @DateEnrolledInCare 
												END;
										END;

									PRINT ' ';
									SET @message = '----- Start Date Patient enrollment: ' + CAST(@StartDate as varchar(50));
									PRINT @message;


									INSERT INTO [dbo].[PatientEnrollment] ([PatientId] ,[ServiceAreaId] ,[EnrollmentDate] ,[EnrollmentStatusId] ,[TransferIn] ,[CareEnded] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[AuditData])
									VALUES (@PatientId,1, @StartDate,0, @transferIn, @Status ,0 ,@UserID_Enrollment ,@CreateDate_Enrollment ,NULL);

									SET @EnrollmentId = SCOPE_IDENTITY();
									SET @message = 'Created PatientEnrollment Id: ' + CAST(@EnrollmentId as varchar);
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

	SELECT @EnrollmentId, @ModuleId, @StartDate;
END


/****** Object:  StoredProcedure [dbo].[PatientIdentifiers_To_Greencard]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientIdentifiers_To_Greencard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PatientIdentifiers_To_Greencard]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientIdentifiers_To_Greencard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PatientIdentifiers_To_Greencard] AS' 
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[PatientIdentifiers_To_Greencard]
	-- Add the parameters for the stored procedure here
	@CCCNumber varchar(20),
	@PersonId int,
	@ModuleId int,
	@StartDate datetime,
	@PatientId int,
	@EnrollmentId int,
	@UserID int,
	@CreateDate datetime,
	@ReferredFrom int,
	@entryPoint int,	
	@MaritalStatus int,
	@MaritalStatusId int,
	@ServiceEntryPointId int OUTPUT,
	@PatientIdentifierId int OUTPUT,
	@PatientMaritalStatusID int OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @message varchar(max);

	IF @CCCNumber IS NOT NULL AND @ModuleId = 203
		BEGIN
			-- Patient Identifier
			INSERT INTO [dbo].[PatientIdentifier] ([PatientId], [PatientEnrollmentId], [IdentifierTypeId], [IdentifierValue] ,[DeleteFlag] ,[CreatedBy] ,[CreateDate] ,[Active] ,[AuditData])
			VALUES (@PatientId , @EnrollmentId ,(select top 1 Id from Identifiers where Code='CCCNumber') ,@CCCNumber ,0 ,@UserID ,@CreateDate ,0 ,NULL);

			SET @PatientIdentifierId = SCOPE_IDENTITY();
			SET @message = 'Created PatientIdentifier Id: ' + CAST(@PatientIdentifierId as varchar);
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

	SET @ServiceEntryPointId = SCOPE_IDENTITY();
	SET @message = 'Created ServiceEntryPoint Id: ' + CAST(@ServiceEntryPointId as varchar);
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

	SET @PatientMaritalStatusID = SCOPE_IDENTITY();
	SET @message = 'Created PatientMaritalStatus Id: ' + CAST(@PatientMaritalStatusID as varchar);
	PRINT @message;

	SELECT @PatientIdentifierId, @ServiceEntryPointId, @PatientMaritalStatusID
END



/****** Object:  StoredProcedure [dbo].[PatientTreatmentSupporter_To_Greencard]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientTreatmentSupporter_To_Greencard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PatientTreatmentSupporter_To_Greencard]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientTreatmentSupporter_To_Greencard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PatientTreatmentSupporter_To_Greencard] AS' 
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[PatientTreatmentSupporter_To_Greencard]
	-- Add the parameters for the stored procedure here
	@ptn_pk int,
	@PersonId int,
	@PatientTreatmentSupporterID int OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	--Insert into Treatment Supporter
	DECLARE @k INT = 1;
	DECLARE @countk INT, @message varchar(max);

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

							SET @PatientTreatmentSupporterID = SCOPE_IDENTITY();
							SET @message = 'Created PatientTreatmentSupporterID Id: ' + CAST(@PatientTreatmentSupporterID as varchar);
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

	SELECT @PatientTreatmentSupporterID;
END


/****** Object:  StoredProcedure [dbo].[PatientContact_To_Greencard]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientContact_To_Greencard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PatientContact_To_Greencard]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientContact_To_Greencard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PatientContact_To_Greencard] AS' 
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[PatientContact_To_Greencard]
	-- Add the parameters for the stored procedure here
	@Address varbinary(max),
	@Phone varbinary(max),
	@PersonId int,
	@Status bit,
	@UserID int,
	@CreateDate datetime,
	@PersonContactID int OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @message varchar(max);

	--Insert into Person Contact
	IF @Address IS NOT NULL AND @Phone IS NOT NULL
		BEGIN
			INSERT INTO PersonContact(PersonId, [PhysicalAddress], [MobileNumber], [AlternativeNumber], [EmailAddress], [Active], [DeleteFlag], [CreatedBy], [CreateDate])
			VALUES(@PersonId, @Address, @Phone, null, null, @Status, 0, @UserID, @CreateDate);

			SET @PersonContactID = SCOPE_IDENTITY();
			SET @message = 'Created PersonContact Id: ' + CAST(@PersonContactID as varchar);
			PRINT @message;
		END

		SELECT @PersonContactID;
END



/****** Object:  StoredProcedure [dbo].[PatientBaselineVariables_To_Greencard]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientBaselineVariables_To_Greencard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PatientBaselineVariables_To_Greencard]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientBaselineVariables_To_Greencard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PatientBaselineVariables_To_Greencard] AS' 
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[PatientBaselineVariables_To_Greencard]
@ptn_pk int,
@transferIn int,
@ARTStartDate datetime,
@Sex int,
@LocationId int,
@StartDate datetime,
@EnrollmentDate datetime OUTPUT,
@VisitDate datetime OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		DECLARE @HBVInfected bit, @Pregnant bit, @TBinfected bit, @WHOStage int, @WHOStageString varchar(50), @BreastFeeding bit, 
			@CD4Count decimal , @MUAC decimal, @Weight decimal, @Height decimal, @artstart datetime,
			@ClosestARVDate datetime, @PatientMasterVisitId int, @HIVDiagnosisDate datetime, 
			@EnrollmentWHOStage int, @EnrollmentWHOStageString varchar(50),  @Cohort varchar(50), @visit_id int;

			IF @transferIn = 1
				BEGIN
					SET @artstart = @ARTStartDate
				END
			ELSE
				BEGIN
					Select TOP 1 @artstart = ARTStartDate	From mst_Patient	Where Ptn_Pk = @ptn_pk	And LocationID = @LocationId;
				END

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

			--okay remove 1900
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

		SELECT @EnrollmentDate;
END


/****** Object:  StoredProcedure [dbo].[PatientBaseline_To_Greencard]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientBaseline_To_Greencard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PatientBaseline_To_Greencard]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientBaseline_To_Greencard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[PatientBaseline_To_Greencard] AS' 
END
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[PatientBaseline_To_Greencard]
	-- Add the parameters for the stored procedure here
	@ptn_pk int,
	@PatientId int,
	@EnrollmentDate datetime,
	@VisitDate datetime,
	@UserID int,
	@PatientMasterVisitId int,
	@Status int,
	@ExitDate datetime,
	@CreateDate datetime,
	@UserID_CareEnded int,
	@CreateDate_CareEnded datetime,
	@EnrollmentId int,
	@ExitReason int,
	@DateOfDeath datetime,
	@Weight decimal,
	@Height decimal,
	@Pregnant bit,
	@TBinfected bit,
	@WHOStage int,
	@BreastFeeding bit,
	@CD4Count decimal,
	@MUAC decimal,
	@transferIn int,
	@EnrollmentWHOStage int,
	@HIVDiagnosisDate datetime,
	@artstart datetime,
	@Cohort varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @message varchar(max);

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
			--SET @TreatmentStartDate = ISNULL(@TreatmentStartDate, @artstart);
			--SET @TreatmentStartDate = ISNULL(@TreatmentStartDate, @DEFAULTDATE);

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

			IF @TransferInDate = CONVERT(datetime, '1900-01-01', 104)
			BEGIN
				SET @TransferInDate = NULL;
			END


			IF @transferIn = 1 AND @TransferInDate IS NOT NULL AND @TreatmentStartDate IS NOT NULL AND @CurrentART IS NOT NULL AND @FacilityFrom IS NOT NULL AND @MFLCODE IS NOT NULL
				BEGIN
					INSERT INTO [dbo].[PatientTransferIn]([PatientId], [PatientMasterVisitId], [ServiceAreaId], [TransferInDate], [TreatmentStartDate], [CurrentTreatment],  [FacilityFrom] , [MFLCode] ,[CountyFrom] , [TransferInNotes], [DeleteFlag] ,[CreatedBy] , [CreateDate])
					VALUES(@PatientId, @PatientMasterVisitId, 1, @TransferInDate, @TreatmentStartDate, @CurrentART, @FacilityFrom, @MFLCODE, (select ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown'), ' ', 0 , @UserID, @CreateDateTransfer);

					SELECT @message = 'Created PatientTransferIn Id: ' + CAST(SCOPE_IDENTITY() as varchar);
					PRINT @message;
				END
	END

	IF EXISTS (Select ptn_pk, locationID, Visit_pk [VisitId], a.PurposeId, b.Name [Purpose], a.Regimen [Regimen],	a.DateLastUsed [RegLastUsed] From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk)
		BEGIN
			DECLARE @TreatmentType varchar(50), @Purpose varchar(50), @Regimen varchar(50), @DateLastUsed datetime;
			
			SET @TreatmentType = (select TOP 1 [Name] from mst_Decode where codeID=33 AND ID = (Select top 1 a.PurposeId From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk));
			SET @Purpose = (select TOP 1 b.Name [Purpose] From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk);
			SET @Regimen = (select TOP 1 a.Regimen [Regimen] From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk);
			SET @DateLastUsed = (select TOP 1 a.DateLastUsed [RegLastUsed] From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk);

			IF @TreatmentType IS NOT NULL AND @Purpose IS NOT NULL AND @Regimen IS NOT NULL
				BEGIN
				INSERT INTO [dbo].[PatientARVHistory]([PatientId], [PatientMasterVisitId], [TreatmentType], [Purpose] , [Regimen], [DateLastUsed], [DeleteFlag] , [CreatedBy] , [CreateDate])
				VALUES(@PatientId, @PatientMasterVisitId, @TreatmentType, @Purpose, @Regimen, @DateLastUsed, 0, @UserID, @CreateDate);
				END

				SELECT @message = 'Created PatientARVHistory Id: ' + CAST(SCOPE_IDENTITY() as varchar);
				PRINT @message;
		END

	--Tranfer-ins//
	IF EXISTS(select TOP 1 FirstLineRegStDate from [dbo].[dtl_PatientARTCare] WHERE ptn_pk = @ptn_pk) AND @transferIn = 1
		BEGIN
			DECLARE @DateStartedOnFirstLine datetime;
			SET @DateStartedOnFirstLine = (select TOP 1 FirstLineRegStDate from [dbo].[dtl_PatientARTCare] WHERE ptn_pk = @ptn_pk);

			IF @DateStartedOnFirstLine IS NOT NULL AND @Cohort IS NOT NULL
			BEGIN
				INSERT INTO [dbo].[PatientTreatmentInitiation]([PatientMasterVisitId], [PatientId], [DateStartedOnFirstLine], [Cohort], Regimen, [RegimenCode] , [BaselineViralload] , [BaselineViralloadDate] , [DeleteFlag] , [CreatedBy] , [CreateDate] )
				VALUES(@PatientMasterVisitId, @PatientId, @DateStartedOnFirstLine, @Cohort, Null,(SELECT TOP 1 FirstLineReg FROM dtl_PatientARTCare where ptn_pk = @ptn_pk) , NULL, NULL, 0, @UserID, @CreateDate);

				SELECT @message = 'Created PatientTreatmentInitiation Id: ' + CAST(SCOPE_IDENTITY() as varchar);
				PRINT @message;
			END
		END

	--SET @HIVDiagnosisDate = ISNULL(@HIVDiagnosisDate, @DEFAULTDATE);
	--SET @EnrollmentDate = ISNULL(@EnrollmentDate, @DEFAULTDATE);
	--SET @artstart = ISNULL(@artstart, @DEFAULTDATE);

	IF @EnrollmentWHOStage IS NOT NULL
		BEGIN
			INSERT INTO [dbo].[PatientHivDiagnosis]([PatientMasterVisitId] , [PatientId] , [HIVDiagnosisDate] , [EnrollmentDate] , [EnrollmentWHOStage] , [ARTInitiationDate] , [DeleteFlag] , [CreatedBy] , [CreateDate])
			VALUES(@PatientMasterVisitId, @PatientId, @HIVDiagnosisDate, @EnrollmentDate, @EnrollmentWHOStage, @artstart, 0 , @UserID, @CreateDate);

				SELECT @message = 'Created PatientHivDiagnosis Id: ' + CAST(SCOPE_IDENTITY() as varchar);
				PRINT @message;
		END
END


/****** Object:  StoredProcedure [dbo].[SP_mst_PatientToGreencardRegistration]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_mst_PatientToGreencardRegistration]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_mst_PatientToGreencardRegistration]
GO


/****** Object:  StoredProcedure [dbo].[SP_mst_PatientToGreencardRegistration]    Script Date: 05/09/2017 17:08:22 ******/
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

						SET @transferIn=0;
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

						DECLARE @ModuleId int, @StartDate datetime;

						exec [dbo].[PatientDemographics_To_Greencard] @ptn_pk,	@FirstName,	@MiddleName, @LastName,	@Sex, @Status_Greencard, @DeleteFlag, @CreateDate, @UserID, @PatientFacilityId, @PatientType, @FacilityId, @DateOfBirth, @DobPrecision, @NationalId, @RegistrationDate,	@PersonId OUTPUT, @PatientId OUTPUT

						--Insert into Enrollment Table
						exec [dbo].[PatientEnrollment_To_Greencard] @ptn_pk, @transferIn, @PatientId, @Status, @EnrollmentId OUTPUT, @ModuleId OUTPUT, @StartDate OUTPUT

						--insert into PatientIdentifier, ServiceEntryPoint, MaritalStatus
						exec [dbo].[PatientIdentifiers_To_Greencard] @CCCNumber, @PersonId, @ModuleId, @StartDate, @PatientId, @EnrollmentId, @UserID, @CreateDate,	@ReferredFrom, @entryPoint,	@MaritalStatus,	@MaritalStatusId, @ServiceEntryPointId OUTPUT, @PatientIdentifierId OUTPUT,	@PatientMaritalStatusID OUTPUT

						--Insert into Treatment Supporter
						exec [dbo].[PatientTreatmentSupporter_To_Greencard] @ptn_pk, @PersonId, @PatientTreatmentSupporterID OUTPUT

						--Insert into Person Contact
						exec [dbo].[PatientContact_To_Greencard] @Address, @Phone, @PersonId, @Status, @UserID, @CreateDate, @PersonContactID OUTPUT

						--Starting baseline
						DECLARE @HBVInfected bit, @Pregnant bit, @TBinfected bit, @WHOStage int, @WHOStageString varchar(50), @BreastFeeding bit, @CD4Count decimal , @MUAC decimal, @Weight decimal, @Height decimal, @artstart datetime, @ClosestARVDate datetime, @PatientMasterVisitId int, @HIVDiagnosisDate datetime, @EnrollmentDate datetime, @EnrollmentWHOStage int, @EnrollmentWHOStageString varchar(50), @VisitDate datetime, @Cohort varchar(50), @visit_id int;

						exec [dbo].[PatientBaselineVariables_To_Greencard] @ptn_pk, @transferIn, @ARTStartDate, @Sex, @LocationId, @StartDate, @EnrollmentDate OUTPUT, @VisitDate OUTPUT, @artstart OUTPUT, @visit_id OUTPUT, @Pregnant OUTPUT, @HBVInfected OUTPUT, @TBinfected OUTPUT, @WHOStage OUTPUT, @WHOStageString OUTPUT, @BreastFeeding OUTPUT, @CD4Count OUTPUT, @MUAC OUTPUT, @Weight OUTPUT, @Height OUTPUT, @ClosestARVDate OUTPUT, @PatientMasterVisitId OUTPUT, @HIVDiagnosisDate OUTPUT, @EnrollmentWHOStage OUTPUT, @EnrollmentWHOStageString OUTPUT, @Cohort OUTPUT
						exec [dbo].[PatientBaseline_To_Greencard] @ptn_pk, @PatientId, @EnrollmentDate, @VisitDate, @UserID, @PatientMasterVisitId, @Status, @ExitDate, @CreateDate, @UserID_CareEnded, @CreateDate_CareEnded, @EnrollmentId, @ExitReason, @DateOfDeath, @Weight, @Height, @Pregnant, @TBinfected, @WHOStage, @BreastFeeding,	@CD4Count, @MUAC, @transferIn, @EnrollmentWHOStage,	@HIVDiagnosisDate, @artstart, @Cohort
						
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
		 
		 exec [dbo].[pr_CloseDecryptedSession];
END




/****** Object:  StoredProcedure [dbo].[sp_getPharmacyDrugList]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPharmacyDrugList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPharmacyDrugList]
GO


/****** Object:  StoredProcedure [dbo].[sp_getPharmacyDrugList]    Script Date: 05/09/2017 17:08:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPharmacyDrugList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPharmacyDrugList] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 14th Feb 2017
-- Description:	get pharmacy drug list
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPharmacyDrugList]
	-- Add the parameters for the stored procedure here
	@pmscm int = null,@tp varchar(10)=null
	

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
	DECLARE @drugTypeId int =0;

	--select Drug_pk, DrugName,CONCAT(Drug_pk, '~',abbreviation, '~', DrugName)val 
	--from mst_drug
	-- ////////////////////////////////////////////////////////////////////////////////////////
	IF(@tp IN('ART','PMTCT','PEP','PREP','HBV','Hepatitis B'))
		BEGIN
		  SET @drugTypeId=37
		END


	-- ///////////////////////////////////////////////////////////////////////////////////////

	
	IF(@pmscm = 1)
	BEGIN

		IF(@drugTypeId=37)
		  BEGIN
				SELECT
					D.Drug_pk
				   ,D.DrugName
				   , (D.Drug_pk + '~' + isnull(D.abbreviation,D.DrugName) + '~' + D.DrugName) val
				FROM Dtl_StockTransaction AS ST
				INNER JOIN Mst_Store AS S
					ON S.Id = ST.StoreId
						AND S.DispensingStore = 1
				RIGHT OUTER JOIN Mst_Drug AS D
					ON D.Drug_pk = ST.ItemId
				INNER JOIN lnk_DrugGeneric l
					ON D.Drug_pk = l.Drug_pk
				INNER JOIN lnk_DrugTypeGeneric g
					ON l.GenericID = g.GenericId
				WHERE D.DeleteFlag = 0
				AND g.DrugTypeId =37
				GROUP BY D.Drug_pk
						,D.DrugName
						,D.abbreviation
				HAVING SUM(ST.Quantity) > 0

				--select D.*, G.DrugTypeId From Mst_Drug D Inner join lnk_DrugGeneric DG on DG.Drug_pk=D.drug_pk
				-- inner join lnk_DrugTypeGeneric G on G.GenericId= DG.GenericID and g.DrugTypeId=37

			END
		ELSE
		 BEGIN 
			SELECT
					D.Drug_pk
				   ,D.DrugName
				   ,(D.Drug_pk + '~' + isnull(D.abbreviation,D.DrugName) +  '~' + D.DrugName) val
				FROM Dtl_StockTransaction AS ST
				INNER JOIN Mst_Store AS S
					ON S.Id = ST.StoreId
						AND S.DispensingStore = 1
				RIGHT OUTER JOIN Mst_Drug AS D
					ON D.Drug_pk = ST.ItemId
				INNER JOIN lnk_DrugGeneric l
					ON D.Drug_pk = l.Drug_pk
				INNER JOIN lnk_DrugTypeGeneric g
					ON l.GenericID = g.GenericId
				WHERE D.DeleteFlag = 0
				AND g.DrugTypeId <>37
				GROUP BY D.Drug_pk
						,D.DrugName
						,D.abbreviation
				HAVING SUM(ST.Quantity) > 0   
		 END

	END
	ELSE
		BEGIN

			IF(@drugTypeId=37)
			BEGIN
				Select	D.Drug_pk, D.DrugName,
				(D.Drug_pk +  '~' + isnull(D.abbreviation,D.DrugName) +  '~' + D.DrugName) val 
				From Dtl_StockTransaction As ST	Inner Join Mst_Store As S On S.Id = ST.StoreId And S.DispensingStore = 1
				Right Outer Join Mst_Drug As D On D.Drug_pk = ST.ItemId 
								INNER JOIN lnk_DrugGeneric l
						ON D.Drug_pk = l.Drug_pk
					INNER JOIN lnk_DrugTypeGeneric g
						ON l.GenericID = g.GenericId
					WHERE D.DeleteFlag = 0
					AND g.DrugTypeId = @drugTypeId
				Group By D.Drug_pk,	D.DrugName, D.abbreviation
			END
			ELSE
			BEGIN
						Select	D.Drug_pk, D.DrugName,
				(D.Drug_pk + '~' + isnull(D.abbreviation,D.DrugName) + '~' + D.DrugName) val 
				From Dtl_StockTransaction As ST	Inner Join Mst_Store As S On S.Id = ST.StoreId And S.DispensingStore = 1
				Right Outer Join Mst_Drug As D On D.Drug_pk = ST.ItemId 
								INNER JOIN lnk_DrugGeneric l
						ON D.Drug_pk = l.Drug_pk
					INNER JOIN lnk_DrugTypeGeneric g
						ON l.GenericID = g.GenericId
					WHERE D.DeleteFlag = 0
					AND g.DrugTypeId <> 37
				Group By D.Drug_pk,	D.DrugName, D.abbreviation
			END
		END

End


/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounter]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPatientEncounter]
GO


/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounter]    Script Date: 05/09/2017 17:08:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounter]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPatientEncounter] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 10th Feb 2017
-- Description:	get patient encounter
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPatientEncounter]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;
	declare @TBScreeningTypeID int = (Select Id from LookupMaster where name = 'TBStatus')
	declare @NutritionScreeningTypeID int = (Select Id from LookupMaster where name = 'NutritionStatus')
	declare @GeneralExamMasterId int = (Select top 1 Id from LookupMaster where name = 'GeneralExamination')
	declare @ARVAdherenceType int = (Select Id from LookupMaster where name = 'ARVAdherence')
	declare @CTXAdherenceType int = (Select Id from LookupMaster where name = 'CTXAdherence')

	--0
	select * from PatientMasterVisit where id = @PatientMasterVisitID and patientId = @PatientID
	and (DeleteFlag is null OR DeleteFlag = 0)
	
	--1
	select * from ComplaintsHistory where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID
	and (DeleteFlag is null OR DeleteFlag = 0)
	
	--2
	select * from PhysicalExamination where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID
	and ExaminationTypeId = @GeneralExamMasterId and (DeleteFlag is null OR DeleteFlag = 0)
	
	--TB Screening 3
	select ScreeningValueId from PatientScreening 
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and ScreeningTypeId = @TBScreeningTypeID
	and (DeleteFlag is null OR DeleteFlag = 0)
	
	--Nutrition Screening 4
	select ScreeningValueId from PatientScreening 
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and ScreeningTypeId = @NutritionScreeningTypeID
	and (DeleteFlag is null OR DeleteFlag = 0)
	
	--5
	select * from PatientPHDP where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID 
	and (DeleteFlag is null OR DeleteFlag = 0)

	--6 ARV Adherence
	select Score from AdherenceOutcome 
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and AdherenceType = @ARVAdherenceType
	and (DeleteFlag is null OR DeleteFlag = 0)

	--7 CTX Adherence
	select Score from AdherenceOutcome 
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and AdherenceType = @CTXAdherenceType
	and (DeleteFlag is null OR DeleteFlag = 0)

	--8 workplan
	select * from PatientClinicalNotes
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and deleteflag <> 1

	--9 ICF
	select * from [dbo].[PatientIcf]
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and deleteflag <> 1

	--10 ICF Action
	select * from [dbo].[PatientIcfAction]
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and deleteflag <> 1

	--11 IPT
	select * from [dbo].[PatientIpt]
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and deleteflag <> 1

	--12 IPT Outcome
	select * from [dbo].[PatientIptOutcome]
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and deleteflag <> 1

	--13 IPT Workup
	select * from [dbo].[PatientIptWorkup]
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID and deleteflag <> 1
	
	--14 WHO Stage
	select * from [dbo].[PatientWHOStage]
	where PatientMasterVisitId = @PatientMasterVisitID and patientId = @PatientID

End
END



/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterAdverseEvents]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterAdverseEvents]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_getPatientEncounterAdverseEvents]
GO


/****** Object:  StoredProcedure [dbo].[sp_getPatientEncounterAdverseEvents]    Script Date: 05/09/2017 17:08:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientEncounterAdverseEvents]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[sp_getPatientEncounterAdverseEvents] AS' 
END
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 14th Feb 2017
-- Description:	get patient encounter Adverse Events
-- =============================================
ALTER PROCEDURE [dbo].[sp_getPatientEncounterAdverseEvents]
	-- Add the parameters for the stored procedure here
	@PatientMasterVisitID int = null,
	@PatientID int = null

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

	select Severity SeverityID,EventName,EventCause,b.DisplayName Severity,[Action] 
	from AdverseEvent a left join LookupItem b on a.Severity = b.Id
	where patientId = @PatientID and (a.DeleteFlag is null or a.DeleteFlag = 0)

	-- PatientMasterVisitId = @PatientMasterVisitID and  //commented to show all adverseEvents patient has had.
	
End




/****** Object:  StoredProcedure [dbo].[FamilyTesting_To_Greencard]    Script Date: 5/9/2017 3:16:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FamilyTesting_To_Greencard]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FamilyTesting_To_Greencard]
GO


/****** Object:  StoredProcedure [dbo].[FamilyTesting_To_Greencard]    Script Date: 05/09/2017 17:08:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FamilyTesting_To_Greencard]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[FamilyTesting_To_Greencard] AS' 
END
GO
-- =============================================
-- Author: Felix
-- Create date: 12-Jul-2017
-- Description:	move family testing to greencard
-- =============================================
ALTER PROCEDURE [dbo].[FamilyTesting_To_Greencard]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SET XACT_ABORT ON;

	exec pr_OpenDecryptedSession;
    -- Insert statements for procedure here
	DECLARE @Ptn_pk int, @RFirstName varbinary(max), @RLastName varbinary(max), @Sex int, @AgeYear int, @AgeMonth int, @RelationshipDate datetime, @RelationshipType int, @HivStatus int, @HivCareStatus int, @RegistrationNo int, @FileNo int, @ReferenceId int, @UserId int, @DeleteFlag int, @CreateDate datetime, @UpdateDate datetime, @message varchar(100), @DOB datetime, @StartDate datetime, @PersonId int, @PatientId int, @RelationshipTypeId int, @BaselineResult int, @HivStatusString varchar(20), @PatientMasterVisitId int, @VisitType int, @MovedToFamilyTestingTable bit, @FamilyInfoId int;

	DECLARE @i INT = 1;
	DECLARE @count INT;
	--Create Temporary Tables for storing data 
	CREATE TABLE #Tdtl_FamilyInfo 
	(
		Id INT IDENTITY(1,1), Ptn_pk int, RFirstName varbinary(max), RLastName varbinary(max), Sex int, AgeYear int, AgeMonth int, 
		RelationshipDate datetime, RelationshipType int, HivStatus int, HivCareStatus int, RegistrationNo varchar, FileNo varchar, 
		ReferenceId int, UserId int, DeleteFlag int, CreateDate datetime, UpdateDate datetime, StartDate datetime, MovedToFamilyTestingTable bit, FamilyInfoId int
	 ); 
	 --Insert data to temporary table #Tdtl_FamilyInfo 
	INSERT INTO #Tdtl_FamilyInfo(Ptn_pk, RFirstName, RLastName, Sex, AgeYear, AgeMonth, 
		RelationshipDate, RelationshipType, HivStatus, HivCareStatus, RegistrationNo, FileNo, 
		ReferenceId, UserId, DeleteFlag, CreateDate, UpdateDate, StartDate, MovedToFamilyTestingTable, FamilyInfoId) 
	
		SELECT dbo.dtl_FamilyInfo.Ptn_pk, RFirstName, RLastName, Sex, AgeYear, AgeMonth, RelationshipDate, RelationshipType, HivStatus, HivCareStatus, RegistrationNo, FileNo, ReferenceId, dbo.dtl_FamilyInfo.UserId, DeleteFlag, dbo.dtl_FamilyInfo.CreateDate, dbo.dtl_FamilyInfo.UpdateDate, dbo.Lnk_PatientProgramStart.StartDate, [dbo].[dtl_FamilyInfo].[MovedToFamilyTestingTable], dbo.dtl_FamilyInfo.Id
		FROM    dbo.dtl_FamilyInfo INNER JOIN dbo.Lnk_PatientProgramStart ON dbo.dtl_FamilyInfo.Ptn_pk = dbo.Lnk_PatientProgramStart.Ptn_pk
		WHERE dbo.Lnk_PatientProgramStart.ModuleId = 203;

	SELECT @count = COUNT(Id) FROM #Tdtl_FamilyInfo 
	WHILE (@i <= @count)
		BEGIN
			SELECT @ptn_pk = Ptn_pk, @RFirstName = RFirstName, @RLastName = RLastName, @Sex = Sex, @AgeYear = AgeYear, @AgeMonth = AgeMonth, 
					@RelationshipDate = RelationshipDate, @RelationshipType = RelationshipType, @HivStatus = HivStatus, @HivCareStatus = HivCareStatus, 
					@RegistrationNo = RegistrationNo, @FileNo = FileNo, @ReferenceId = ReferenceId, @UserId = UserId, @DeleteFlag = DeleteFlag, @CreateDate = CreateDate, 
					@UpdateDate = UpdateDate, @StartDate = StartDate, @MovedToFamilyTestingTable = MovedToFamilyTestingTable, @FamilyInfoId = FamilyInfoId FROM #Tdtl_FamilyInfo WHERE Id = @i;

			BEGIN TRY
				BEGIN TRANSACTION
					PRINT ' '  
					SELECT @message = '----- Family Testing: ' + CAST(@ptn_pk as varchar(50));
					PRINT @message;

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

					DECLARE @SexName varchar(50);

					SET @SexName = (select top 1  ItemName from LookupItemView where ItemId = @Sex);

					BEGIN
						SET @DOB = DATEADD(year, -@AgeYear, @StartDate);
						SET @DOB = DATEADD(month, -@AgeMonth, @DOB);
					END;

					IF @CreateDate IS NULL
						BEGIN
							SET @CreateDate = @StartDate;
						END

					SET @PatientId = (select Id from Patient where ptn_pk = @Ptn_pk);

					IF @RelationshipType IS NOT NULL
						BEGIN
							DECLARE @RelationTypeName varchar(20);

							SET @RelationTypeName = (select top 1  Name from [dbo].[mst_RelationshipType] where id = @RelationshipType);
							SET @RelationshipTypeId = CASE
								WHEN @RelationTypeName = 'Parent' and @SexName = 'Male' THEN (select top 1  ItemId from LookupItemView where MasterName = 'Relationship' and ItemName = 'Father')
								WHEN @RelationTypeName = 'Parent' and @SexName = 'Female' THEN (select top 1  ItemId from LookupItemView where MasterName = 'Relationship' and ItemName = 'Mother')
								WHEN @RelationTypeName = 'Aunt/Uncle' THEN (select top 1  ItemId from LookupItemView where MasterName = 'Relationship' and ItemName = 'Other')
								WHEN @RelationTypeName = 'Brother/Sister' THEN (select top 1  ItemId from LookupItemView where MasterName = 'Relationship' and ItemName = 'Sibling')
								WHEN @RelationTypeName = 'Child' THEN (select top 1  ItemId from LookupItemView where MasterName = 'Relationship' and ItemName = 'Child')
								WHEN @RelationTypeName = 'Cousin' THEN (select top 1  ItemId from LookupItemView where MasterName = 'Relationship' and ItemName = 'Other')
								WHEN @RelationTypeName = 'GrandChild' THEN (select top 1  ItemId from LookupItemView where MasterName = 'Relationship' and ItemName = 'Other')
								WHEN @RelationTypeName = 'GrandParent' THEN (select top 1  ItemId from LookupItemView where MasterName = 'Relationship' and ItemName = 'Other')
								WHEN @RelationTypeName = 'Niece/Nephew' THEN (select top 1  ItemId from LookupItemView where MasterName = 'Relationship' and ItemName = 'Other')
								WHEN @RelationTypeName = 'Not Family' THEN (select top 1  ItemId from LookupItemView where MasterName = 'Relationship' and ItemName = 'Other')
								WHEN @RelationTypeName = 'Other' THEN (select top 1  ItemId from LookupItemView where MasterName = 'Relationship' and ItemName = 'Other')
								WHEN @RelationTypeName = 'Spouse/Partner' THEN (select top 1  ItemId from LookupItemView where MasterName = 'Relationship' and ItemName = 'Partner')
								ELSE (select top 1  ItemId from LookupItemView where MasterName = 'Relationship' and ItemName = 'Other')
							END
						END
					ELSE
						SET @RelationshipTypeId = (select top 1  ItemId from LookupItemView where MasterName = 'Relationship' and ItemName = 'Other');

					SET @HivStatusString = (SELECT TOP 1 Name FROM mst_Decode WHERE CodeID=10 AND ID=@HivStatus);

					SET @BaselineResult = CASE @HivStatusString  
							WHEN 'Positive' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='BaseLineHivStatus' AND ItemName like '%Tested Positive%') 
							WHEN 'Negative' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='BaseLineHivStatus' AND ItemName like '%Tested Negative%')   
							WHEN 'Unknown' THEN (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='BaseLineHivStatus' AND ItemName = 'Never Tested')
							ELSE (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='BaseLineHivStatus' AND ItemName = 'Never Tested')
						END

					SET @BaselineResult = CASE WHEN @BaselineResult IS NULL THEN (select TOP 1 ItemId from LookupItemView where MasterName = 'Unknown' and ItemName = 'Unknown') ELSE @BaselineResult END;

					SET @VisitType = (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName ='VisitType' AND ItemName='Enrollment');

					SET @PatientMasterVisitId = (SELECT TOP 1 Id FROM PatientMasterVisit where VisitType = @VisitType and PatientId = @PatientId);


					IF @MovedToFamilyTestingTable = 1
						BEGIN
							SET @PersonId = (SELECT TOP 1 PersonId FROM PersonRelationship WHERE FamilyInfoId = @FamilyInfoId);

							SELECT @message = '----- Update Family Testing: ' + CAST(@PersonId as varchar(50));
							PRINT @message;

							SELECT @message = '----- Relationship Name: ' + CAST(@RelationTypeName as varchar(50));
							PRINT @message;

							SELECT @message = '----- Relationship Type: ' + CAST(@RelationshipTypeId as varchar(50));
							PRINT @message;

							SELECT @message = '----- PersonId: ' + CAST(@PersonId as varchar(50));
							PRINT @message;

							SELECT @message = '----- PatientId: ' + CAST(@PatientId as varchar(50));
							PRINT @message;

							SELECT @message = '----- BaselineResult: ' + CAST(@BaselineResult as varchar(50));
							PRINT @message;

							UPDATE Person SET FirstName = @RFirstName, LastName = @RLastName, Sex = @Sex, DateOfBirth = @DOB WHERE Id = @PersonId;

							UPDATE PersonRelationship SET RelationshipTypeId = @RelationshipTypeId, BaselineResult = @BaselineResult WHERE PersonId = @PersonId AND PatientId = @PatientId;
						END
					ELSE
						BEGIN
							SELECT @message = '----- adding new relationship';
							PRINT @message;

							DECLARE @r INT = 1;
							DECLARE @countr INT;

							CREATE TABLE #Tperson(
								Id INT IDENTITY(1,1), PersonId int
							);
							INSERT INTO #Tperson(PersonId)
							select T.PersonId from (SELECT TOP (100) PERCENT dbo.PersonRelationship.PersonId, dbo.PersonRelationship.RelationshipTypeId, dbo.PersonRelationship.PatientId, dbo.PersonRelationship.ID, dbo.dtl_FamilyInfo.Id AS FamilyInfoId, dbo.Patient.ptn_pk
							FROM  dbo.dtl_FamilyInfo INNER JOIN dbo.Patient ON dbo.dtl_FamilyInfo.Ptn_pk = dbo.Patient.ptn_pk RIGHT OUTER JOIN dbo.PersonRelationship ON dbo.Patient.Id = dbo.PersonRelationship.PatientId
							GROUP BY dbo.PersonRelationship.PersonId, dbo.PersonRelationship.RelationshipTypeId, dbo.Patient.Id, dbo.PersonRelationship.PatientId, dbo.PersonRelationship.ID, dbo.dtl_FamilyInfo.Id, dbo.Patient.ptn_pk
							HAVING        (dbo.PersonRelationship.PatientId IS NOT NULL)
							ORDER BY dbo.PersonRelationship.PatientId) as T
							where T.ptn_pk=@Ptn_pk and T.FamilyInfoId=@FamilyInfoId and T.PatientId=@PatientId;

							SELECT @countr = COUNT(Id) FROM #Tperson

							SELECT @message = '----- new guy: ' + CAST(@countr as varchar(50));
							PRINT @message;

							WHILE (@r <= @countr)
							BEGIN
								SELECT @message = '----- am in: ' + CAST(@r as varchar(50));
								PRINT @message;

								SELECT @PersonId = PersonId FROM #Tperson WHERE Id = @r;

								DECLARE @FamilyInfoId_ISNULL int;
								SELECT @FamilyInfoId_ISNULL = FamilyInfoId FROM PersonRelationship WHERE PersonId = @PersonId;

								IF EXISTS(SELECT ID FROM PersonRelationship WHERE PersonId = @PersonId) AND  @FamilyInfoId_ISNULL IS NULL
								BEGIN
									SELECT @message = '----- old personrelationship updated: ';
									PRINT @message;

									UPDATE PersonRelationship SET RelationshipTypeId = @RelationshipTypeId, BaselineResult = @BaselineResult, FamilyInfoId = @FamilyInfoId WHERE PersonId = @PersonId AND PatientId = @PatientId;								
								END
								SELECT @r = @r + 1
							END
							DROP TABLE #Tperson
							IF @r = 1
								BEGIN
									INSERT INTO Person(FirstName, MidName, LastName, Sex, DateOfBirth, DobPrecision, Active, DeleteFlag, CreateDate, CreatedBy)
									VALUES(@RFirstName, NULL, @RLastName, @Sex, @DOB, 1, 1, 0, @CreateDate, @UserId);

									SELECT @PersonId = SCOPE_IDENTITY();
									SELECT @message = 'Created Person Id: ' + CAST(@PersonId as varchar(50));
									PRINT @message;

									INSERT INTO [dbo].[PersonRelationship](PersonId, PatientId, RelationshipTypeId, BaselineResult, BaselineDate, DeleteFlag, CreatedBy, CreateDate, FamilyInfoId)
									VALUES(@PersonId, @PatientId, @RelationshipTypeId, @BaselineResult, @CreateDate, 0, @UserId, @CreateDate, @FamilyInfoId);

									SELECT @message = 'Created PersonRelationship Id: ' + CAST(SCOPE_IDENTITY() as varchar(50));
									PRINT @message;
								END
							UPDATE dtl_FamilyInfo SET MovedToFamilyTestingTable = 1 WHERE Ptn_pk = @Ptn_pk AND Id = @FamilyInfoId;
						END
					--INSERT INTO HIVTesting(PersonId, PatientMasterVisitId, TestingDate, TestingResult, ReferredToCare, CCCNumber, EnrollmentId, DeleteFlag, CreatedBy, CreateDate, AuditData)
					--VALUES(@PersonId, @PatientMasterVisitId, NULL, 0, 0, NULL, 0, 0, @UserId, @CreateDate, NULL);

					--SELECT @message = 'Created HIVTesting Id: ' + CAST(SCOPE_IDENTITY() as varchar(50));
					--PRINT @message;

				COMMIT TRANSACTION

			END TRY
			BEGIN CATCH
			  ROLLBACK TRANSACTION
			END CATCH

			SELECT @i = @i + 1
		END
	--Now Drop Temporary Tables
	 DROP TABLE #Tdtl_FamilyInfo
	 UPDATE PersonRelationship SET FamilyInfoId = 0 WHERE FamilyInfoId IS NULL;
END