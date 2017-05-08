USE [IQCareDefault]
GO
/****** Object:  StoredProcedure [dbo].[SP_mst_PatientToGreencardRegistration]    Script Date: 5/4/2017 9:06:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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

DECLARE @ptn_pk int, @FirstName varbinary(max), @MiddleName varbinary(max), @LastName varbinary(max), @Sex int, @Status bit , @DeleteFlag bit, 
		@CreateDate datetime, @UserID int,  @message varchar(80), @Id int, @PatientFacilityId varchar(50), @PatientType int, 
		@FacilityId varchar(10), @DateOfBirth datetime, @DobPrecision int, @NationalId varbinary(max), @PatientId int,  
		@ARTStartDate date,@transferIn int,@CCCNumber varchar(20), @entryPoint int, @ReferredFrom int, @RegistrationDate datetime,
		@MaritalStatusId int, @MaritalStatus int, @DistrictName varchar(50), @CountyID int, @SubCountyID int, @WardID int,
		@Address varbinary(max), @Phone varbinary(max), @EnrollmentId int, @PatientIdentifierId int, @ServiceEntryPointId int,
		@PatientMaritalStatusID int, @PatientTreatmentSupporterID int, @PersonContactID int, @LocationID int;
  
PRINT '-------- Patients Report --------';  
  
DECLARE mstPatient_cursor CURSOR FOR   
SELECT mst_Patient.Ptn_Pk, FirstName, MiddleName ,LastName,Sex, [Status], DeleteFlag, mst_Patient.CreateDate, mst_Patient.UserID, PatientFacilityId, PosId, DOB, DobPrecision, [ID/PassportNo],PatientClinicID, [ReferredFrom], [RegistrationDate], MaritalStatus, DistrictName, Address, Phone, LocationID
FROM mst_Patient
INNER JOIN
 dbo.Lnk_PatientProgramStart ON dbo.mst_Patient.Ptn_Pk = dbo.Lnk_PatientProgramStart.Ptn_pk
WHERE        (dbo.Lnk_PatientProgramStart.ModuleId = 203) 
ORDER BY mst_Patient.Ptn_Pk;
  
OPEN mstPatient_cursor  
  
FETCH NEXT FROM mstPatient_cursor   
INTO @ptn_pk, @FirstName, @MiddleName, @LastName, @Sex, @Status, @DeleteFlag, @CreateDate, @UserID, @PatientFacilityId, @FacilityId, @DateOfBirth, @DobPrecision, @NationalId,@CCCNumber, @ReferredFrom, @RegistrationDate, @MaritalStatus , @DistrictName, @Address, @Phone, @LocationID
  
WHILE @@FETCH_STATUS = 0  
BEGIN  
    PRINT ' '  
    SELECT @message = '----- patients From mst_patient: ' + CAST(@ptn_pk as varchar(50))
  
    PRINT @message  

	--set null dates
	IF @CreateDate is null
		SELECT @CreateDate = getdate()
	--Due to the logic of green card
	IF @Status = 1
		SELECT @Status = 0
	ELSE
		SELECT @Status = 1

	IF @NationalId IS NULL
		SET @NationalId = 999999999;

	IF @Sex IS NOT NULL
		SET @Sex = (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName like '%gender%' and ItemName like (select Name from mst_Decode where id = @Sex) + '%');
	ELSE
		SET @Sex = 1341;

	--Default all persons to new
	SET @ARTStartDate=( SELECT ARTTransferInDate FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk=@ptn_pk);
	if(@ARTStartDate Is NULL) BEGIN SET @PatientType=(SELECT Id FROM LookupItem WHERE Name='New');SET @transferIn=0; END ELSE BEGIN SET @PatientType=(SELECT Id FROM LookupItem WHERE Name='TransferIn');SET @transferIn=1; END
	-- SELECT @PatientType = 1285

	--encrypt nationalid
	SELECT @NationalId=ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@NationalId)

	Insert into Person(FirstName, MidName, LastName, Sex, Active, DeleteFlag, CreateDate, CreatedBy)
	Values(@FirstName, @MiddleName, @LastName, @Sex, @Status, @DeleteFlag, @CreateDate, @UserID);

	SELECT @Id=@@IDENTITY;
	SELECT @message = 'Created Person Id: ' + CAST(@Id as varchar(50));
	PRINT @message;

	Insert into Patient(ptn_pk, PersonId, PatientIndex, PatientType, FacilityId, Active, DateOfBirth, DobPrecision, NationalId, DeleteFlag, CreatedBy, CreateDate, RegistrationDate)
	Values(@ptn_pk, @Id, @PatientFacilityId, @PatientType, @FacilityId, @Status, @DateOfBirth, @DobPrecision, @NationalId, @DeleteFlag, @UserID, @CreateDate, @RegistrationDate);

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
	ELSE
		SET @entryPoint = 1341;

	INSERT INTO ServiceEntryPoint([PatientId], [ServiceAreaId], [EntryPointId], [DeleteFlag], [CreatedBy], [CreateDate], [Active])
	VALUES(@PatientId, 1, @entryPoint, 0 , @UserID, @CreateDate, 0);

	SELECT @ServiceEntryPointId=@@IDENTITY;
	SELECT @message = 'Created ServiceEntryPoint Id: ' + CAST(@ServiceEntryPointId as varchar);
	PRINT @message;
	
	--Insert into MaritalStatus
	IF @MaritalStatus > 0
		BEGIN
			IF EXISTS (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (select Name from mst_Decode where ID = @MaritalStatus and CodeID = 12) + '%')
				SET @MaritalStatusId = (select TOP 1 ItemId from [dbo].[LookupItemView] where ItemName like '%' + (select Name from mst_Decode where ID = @MaritalStatus and CodeID = 12) + '%');
			ELSE
				SET @MaritalStatusId = 1341;
		END
	ELSE
		SET @MaritalStatusId = 1341;

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

	DECLARE @FirstNameT varchar(50), @LastNameT varchar(50), @TreatmentSupportTelNumber varbinary(50), 
	@CreateDateT datetime, @UserIDT int, @IDT int;


	OPEN Treatment_Supporter_cursor
	FETCH NEXT FROM Treatment_Supporter_cursor INTO @FirstNameT, @LastNameT, @TreatmentSupportTelNumber, @CreateDateT , @UserIDT

	IF @@FETCH_STATUS <> 0   
        PRINT '         <<None>>'       

    WHILE @@FETCH_STATUS = 0  
    BEGIN  

        --SELECT @message = '         ' + @product  
        --PRINT @message
		SET @TreatmentSupportTelNumber = ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@TreatmentSupportTelNumber);

		Insert into Person(FirstName, MidName, LastName, Sex, Active, DeleteFlag, CreateDate, CreatedBy)
		Values(ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@FirstNameT), NULL, ENCRYPTBYKEY(KEY_GUID('Key_CTC'),@LastNameT), 1341, 1, 0, @CreateDateT, @UserIDT);

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

	--Starting baseline

	DECLARE @HBVInfected bit, @Pregnant bit, @TBinfected bit, @WHOStage int, @BreastFeeding bit, 
			@CD4Count decimal , @MUAC decimal, @Weight decimal, @Height decimal, @artstart datetime,
			@ClosestARVDate datetime, @PatientMasterVisitId int, @HIVDiagnosisDate datetime, @EnrollmentDate datetime,
			@EnrollmentWHOStage int, @VisitDate datetime, @Cohort varchar(50);

	Select @artstart = ARTStartDate	From mst_Patient	Where Ptn_Pk = @patientID	And LocationID = @LocationId;
	SET @Pregnant = 0;

	IF @Sex = 62
		BEGIN
		
			SET @Pregnant =  (Select Top (1) Case Z.Pregnant
				When 0 Then 3
				Else 1 End pregnant
				From (
					Select	dtl.Pregnant,
							ord.VisitDate
					From ord_Visit As ord
					Inner Join
						dtl_PatientClinicalStatus As dtl On dtl.Visit_pk = ord.Visit_Id
					Where (ord.Ptn_Pk = @ptn_pk)
					And (ord.LocationID = @LocationId)
					Union 
					Select	dtl.Pregnant,
							ord.VisitDate
					From ord_Visit As ord
					Inner Join
						dtl_PatientARTCare As dtl On dtl.visit_Id = ord.Visit_Id
					Where (ord.Ptn_Pk = @ptn_pk)
					And (ord.LocationID = @LocationId)
					Union 
					Select	dtl.Pregnant,
							ord.VisitDate
					From ord_Visit As ord
					Inner Join
						dtl_PatientClinicalStatus As dtl On dtl.Visit_pk = ord.Visit_Id
					Where (ord.Ptn_Pk = @ptn_pk)
					And (ord.LocationID = @LocationId)
					) Z
				Where (Z.VisitDate > Dateadd(Month, -3, @artstart)
				And Z.VisitDate < Dateadd(wk, 2, @artstart)) 
				Order By z.VisitDate Desc);
		END

	Select Top 1 	@ClosestARVDate = Nullif(a.DispensedByDate,'')	From ord_PatientPharmacyOrder a,
	dtl_RegimenMap b
	Where a.Ptn_pk = b.Ptn_Pk
	And a.ptn_pharmacy_pk = b.OrderID
	And (a.DeleteFlag = 0	Or a.DeleteFlag Is Null)
	And a.RegimenLine In (1, 2)
	And (b.DeleteFlag = 0	Or b.DeleteFlag Is Null)
	And a.Ptn_pk = @patientID
	And a.DispensedByDate Is Not Null
	And b.RegimenType Is Not Null
	And b.RegimenType <> ''
	Order By a.dispensedbydate Desc;


	If (@ClosestARVDate Is Not Null) Begin
		SET @Weight = (Select Top (1) dtl.[Weight]
		From ord_Visit As ord
		Inner Join
			dtl_PatientVitals As dtl On dtl.Visit_pk = ord.Visit_Id
		Where (ord.Ptn_Pk = @patientID)
		And (dtl.[Weight] Is Not Null)
		Order By Abs(Datediff(Day, ord.VisitDate, @ClosestARVDate)));
	End 
	Else Begin
		SET @Weight = NULL;
	End

	If (@ClosestARVDate Is Not Null) Begin
		SET @Height = (Select Top 1 dtl.Height
		From Ord_visit ord
		Inner Join
			dtl_PatientVitals dtl On dtl.visit_pk = ord.Visit_Id
		Where ord.ptn_pk = @patientID
		And dtl.Height Is Not Null
		Order By Abs(Datediff(Day, Ord.VisitDate, @ClosestARVDate)));
	End 
	Else Begin
		SET @Height = NULL;
	End

	If (@ClosestARVDate Is Not Null) Begin
		SET @MUAC = (Select Top (1) dtl.Muac
		From ord_Visit As ord
		Inner Join
			dtl_PatientVitals As dtl On dtl.Visit_pk = ord.Visit_Id
		Where (ord.Ptn_Pk = @patientID)
		And (dtl.Muac Is Not Null)
		Order By Abs(Datediff(Day, ord.VisitDate, @ClosestARVDate)));
	End

	If (@artstart != '' Or @artstart Is Not Null) Begin
		SET @CD4Count =(Select Top (1) dtl.TestResults As CD4
		From dtl_PatientLabResults As dtl
		Inner Join
			ord_PatientLabOrder As ord On dtl.LabID = ord.LabID
		Where (ord.Ptn_pk = @patientID)
		And (dtl.ParameterID = 1)
		And (dtl.TestResults Is Not Null)
		And (ord.OrderedbyDate > Dateadd(Month, -3, @artstart))
		And (ord.OrderedbyDate < Dateadd(wk, 2, @artstart)));      
	End 
	Else Begin
		SET @CD4Count = NULL;
	End

	If (@ClosestARVDate != '' Or @ClosestARVDate Is Not Null) Begin
		SET @WHOStage = (Select Top (1) dtl.WHOStage
		From ord_Visit As ord
		Inner Join
			dtl_PatientStage As dtl On dtl.Visit_Pk = ord.Visit_Id
		Where (ord.Ptn_Pk = @patientID)
		And (dtl.WHOStage Is Not Null)
		And (dtl.WHOStage <> '0')
		Order By Abs(Datediff(Day, ord.VisitDate, @ClosestARVDate)));
	End 
	Else Begin
		SET @WHOStage = NULL;
	End

	SET @HIVDiagnosisDate = (select top 1 ConfirmHIVPosDate from dtl_PatientHivPrevCareEnrollment where ptn_pk = @ptn_pk);
	SET @EnrollmentDate = (select TOP 1 StartDate from Lnk_PatientProgramStart WHERE Ptn_pk = @ptn_pk);
	SET @EnrollmentWHOStage = (SELECT Name FROM mst_Decode WHERE ID = (SELECT WHOStage FROM dtl_PatientARVEligibility where WHOStage > 0 AND ptn_pk=@ptn_pk));
	SET @Cohort = (select convert(char(3),[FirstLineRegStDate] , 0) + ' ' + CONVERT(varchar(10), year([FirstLineRegStDate])) from [dbo].[dtl_PatientARTCare] WHERE ptn_pk = @ptn_pk);

	SELECT @EnrollmentWHOStage = CASE @EnrollmentWHOStage  
         WHEN 1 THEN (SELECT ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '1') 
         WHEN 2 THEN (SELECT ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '2')   
         WHEN 3 THEN (SELECT ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '3')   
         WHEN 4 THEN (SELECT ItemId FROM LookupItemView WHERE MasterName ='WHOStage' AND ItemName = 'Stage' + '4')  
         ELSE 1341
      END

	SET @VisitDate = (SELECT TOP 1 [VisitDate] FROM [dbo].[ord_Visit] where [Ptn_Pk] = @ptn_pk AND [VisitType] in(18, 19));

	INSERT INTO PatientMasterVisit(PatientId, ServiceId, Start, [End], Active, VisitDate, VisitScheduled, VisitBy, VisitType, [Status], CreateDate, DeleteFlag, CreatedBy)
	VALUES(@PatientId, 1, @EnrollmentDate, NULL, 0, @VisitDate, NULL, NULL, 1340, NULL, GETDATE(), 0 , @UserID);

	SELECT @PatientMasterVisitId=@@IDENTITY;
	
	INSERT INTO [dbo].[PatientBaselineAssessment]([PatientId], [PatientMasterVisitId], [HBVInfected], [Pregnant], [TBinfected], [WHOStage], [BreastFeeding], [CD4Count], [MUAC], [Weight], [Height], [DeleteFlag], [CreatedBy], [CreateDate] )
	VALUES(@PatientId, @PatientMasterVisitId, 0, @Pregnant, 0, @WHOStage, 0, @CD4Count, @MUAC, @Weight, @Height, 0 , @UserID, GETDATE());

	IF EXISTS(SELECT * FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk = @ptn_pk)
		BEGIN
			DECLARE @TransferInDate datetime, @TreatmentStartDate datetime, @CurrentART varchar(50), @FacilityFrom varchar(150), @CreateDateTransfer datetime;

			SET @TransferInDate = (SELECT TOP 1 ARTTransferInDate FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk = @ptn_pk);
			SET @TreatmentStartDate = (SELECT TOP 1 FirstLineRegStDate FROM dtl_PatientARTCare WHERE ptn_pk = @ptn_pk);
			SET @CurrentART = (SELECT TOP 1 CurrentART FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk = @ptn_pk);
			SET @FacilityFrom = (SELECT TOP 1 ARTTransferInFrom FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk = @ptn_pk);
			SET @CreateDateTransfer = (SELECT TOP 1 CreateDate FROM dtl_PatientHivPrevCareIE WHERE Ptn_pk = @ptn_pk)

			INSERT INTO [dbo].[PatientTransferIn]([PatientId], [PatientMasterVisitId], [ServiceAreaId], [TransferInDate], [TreatmentStartDate], [CurrentTreatment],  [FacilityFrom] , [MFLCode] ,[CountyFrom] , [TransferInNotes], [DeleteFlag] ,[CreatedBy] , [CreateDate])
			VALUES(@PatientId, @PatientMasterVisitId, 1, @TransferInDate, @TreatmentStartDate, @CurrentART, @FacilityFrom, NULL, NULL, NULL, 0 , @UserID, @CreateDateTransfer);
	END

	IF EXISTS (Select	ptn_pk,	locationID,	Visit_pk [VisitId], a.PurposeId, b.Name [Purpose], a.Regimen [Regimen],	a.DateLastUsed [RegLastUsed] From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk)
		BEGIN
			DECLARE @TreatmentType varchar(50), @Purpose varchar(50), @Regimen varchar(50), @DateLastUsed datetime;
			
			SET @TreatmentType = (select [Name] from mst_Decode where codeID=33 AND ID = (Select a.PurposeId From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk));
			SET @Purpose = (select b.Name [Purpose] From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk);
			SET @Regimen = (select a.Regimen [Regimen] From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk);
			SET @DateLastUsed = (select a.DateLastUsed [RegLastUsed] From dtl_PatientBlueCardPriorART a Inner Join Mst_Decode b On a.PurposeID = b.ID WHERE ptn_pk = @ptn_pk);

			INSERT INTO [dbo].[PatientARVHistory]([PatientId], [PatientMasterVisitId], [TreatmentType], [Purpose] , [Regimen], [DateLastUsed], [DeleteFlag] , [CreatedBy] , [CreateDate])
			VALUES(@PatientId, @PatientMasterVisitId, @TreatmentType, @Purpose, @Regimen, @DateLastUsed, 0, @UserID, @CreateDate);
		END



	INSERT INTO [dbo].[PatientTreatmentInitiation]([PatientMasterVisitId], [PatientId], [DateStartedOnFirstLine], [Cohort], [Regimen] , [BaselineViralload] , [BaselineViralloadDate] , [DeleteFlag] , [CreatedBy] , [CreateDate] )
	VALUES(@PatientMasterVisitId, @PatientId, (select TOP 1 FirstLineRegStDate from [dbo].[dtl_PatientARTCare] WHERE ptn_pk = @ptn_pk), @Cohort, (SELECT FirstLineReg FROM dtl_PatientARTCare where ptn_pk = @ptn_pk) , NULL, NULL, 0, @UserID, @CreateDate);

	INSERT INTO [dbo].[PatientHivDiagnosis]([PatientMasterVisitId] , [PatientId] , [HIVDiagnosisDate] , [EnrollmentDate] , [EnrollmentWHOStage] , [ARTInitiationDate] , [DeleteFlag] , [CreatedBy] , [CreateDate])
	VALUES(@PatientMasterVisitId, @PatientId, @HIVDiagnosisDate, @EnrollmentDate, @EnrollmentWHOStage, @artstart, 0 , @UserID, @CreateDate);

	--ending baseline

    -- Get the next mst_patient.
    FETCH NEXT FROM mstPatient_cursor   
    INTO @ptn_pk, @FirstName, @MiddleName, @LastName, @Sex, @Status, @DeleteFlag, @CreateDate, @UserID, @PatientFacilityId, @FacilityId, @DateOfBirth, @DobPrecision, @NationalId, @CCCNumber, @ReferredFrom, @RegistrationDate, @MaritalStatus , @DistrictName, @Address, @Phone, @LocationID
END   
CLOSE mstPatient_cursor;  
DEALLOCATE mstPatient_cursor;  
END


