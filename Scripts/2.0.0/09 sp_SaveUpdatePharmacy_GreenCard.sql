SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_SaveUpdatePharmacy_GreenCard]                                                
( 
 @PatientMasterVisitID int = 0,                                               
 @PatientId int = null,                                                
 @LocationID int = null,                                                
 @OrderedBy int = null,                                                                                                                                          
 @UserID int = null,                                                 
 @RegimenType varchar(50) = null,                                                
 @DispensedBy int=null,                                                    
 @RegimenLine int = null,                
 @PharmacyNotes varchar(200) = null,
 @ModuleID int = null,

 @TreatmentProgram int = null,
 @PeriodTaken int = null,

 @TreatmentPlan int = null, 
 @TreatmentPlanReason int = null,
 @Regimen int = null,
 @PrescribedDate varchar(50) = null,
 @DispensedDate varchar(50) = null                 
)                                                
                                                
As       
Begin               
	Declare @ptn_pharmacy int,@RegimenMap_Pk int,@ARTStartDate datetime,@Ptn_Pharmacy_Pk int=null , @ptn_pk int, @visitPK int

	Select @RegimenType = Nullif(Ltrim(Rtrim(@RegimenType)), '')

	set @ptn_pk = (select ptn_pk from patient where id = @PatientId)

	if(@DispensedDate = '')
	begin
		set @DispensedDate = null
		set @DispensedBy = null
	end

	IF EXISTS(select 1 from ord_PatientPharmacyOrder where PatientMasterVisitId = @PatientMasterVisitID) 
	--IF EXISTS(select 1 from ord_PatientPharmacyOrder where ptn_pharmacy_pk = @Ptn_Pharmacy_Pk) 
	BEGIN
		set @Ptn_Pharmacy_Pk = (select top 1 ptn_pharmacy_pk from ord_PatientPharmacyOrder where patientmasterVisitID = @PatientMasterVisitID);
		IF @TreatmentPlan = 0 BEGIN (select TOP 1 @Regimenline = RegimenLine, @TreatmentProgram = [ProgID] from ord_PatientPharmacyOrder where patientmasterVisitID = @PatientMasterVisitID); END;
		Update [ord_PatientPharmacyOrder] Set
			[OrderedBy] = @OrderedBy, [DispensedBy] = @DispensedBy,
			[ProgID] = @TreatmentProgram, [UpdateDate] = Getdate(),
			[ProviderID] = 1, OrderedByDate = @PrescribedDate, [DispensedByDate] = @DispensedDate,
			UserID = @UserID,	Regimenline = @Regimenline,
			PharmacyNotes = @PharmacyNotes, pharmacyperiodtaken = @PeriodTaken
		Where patientmasterVisitID = @PatientMasterVisitID;

		IF @TreatmentPlan = 0 BEGIN (SELECT TOP 1 @Regimen = RegimenId, @RegimenLine = RegimenLineId, @TreatmentPlan = TreatmentStatusId, @TreatmentPlanReason = TreatmentStatusReasonId FROM ARVTreatmentTracker WHERE PatientMasterVisitId = @PatientMasterVisitID); END;
		Update ARVTreatmentTracker set regimenid = @Regimen, regimenLineId = @RegimenLine, TreatmentStatusId = @TreatmentPlan,
		TreatmentStatusReasonId = @TreatmentPlanReason
        WHERE 
        PatientId=@PatientId AND
        PatientMasterVisitId=@PatientMasterVisitID

		If(@RegimenType Is Not Null) 
		Begin
		
			Select @RegimenMap_Pk = RegimenMap_Pk
			From dtl_regimenmap a, ord_patientpharmacyorder b
			Where a.ptn_pk = b.ptn_pk
			And b.ptn_pharmacy_pk = a.orderID
			And b.Ptn_Pharmacy_Pk = @Ptn_Pharmacy_Pk;

			Update [dtl_RegimenMap] Set
				[RegimenType] = @RegimenType,
				[UpdateDate] = Getdate()
			Where ([RegimenMap_Pk] = @RegimenMap_Pk)
		End
		
		Select @ARTStartDate = dbo.fn_GetPatientARTStartDate_constella(@ptn_pk)
		
		Update mst_Patient Set
			ARTStartDate = @ARTStartDate
		Where ptn_pk = @ptn_pk;

		Select @ptn_pharmacy_pk;
	END
	ELSE
	BEGIN
		insert into ord_Visit (ptn_pk,locationid,VisitDate,VisitType,DataQuality,DeleteFlag,UserID,CreateDate,CreatedBy)
		values(@ptn_pk, @locationID,GETDATE(),4,1,0,@UserID,GETDATE(),@UserID)

		set @visitPK = SCOPE_IDENTITY();

		Insert Into dbo.ord_PatientPharmacyOrder (
			Ptn_pk, PatientID, patientmasterVisitID, LocationID, OrderedBy, OrderedByDate, DispensedBy, DispensedByDate, ProgID,
			UserID, CreateDate, ProviderID, Regimenline, PharmacyNotes, VisitID, pharmacyPeriodTaken)
		Values (
			@ptn_pk,@PatientId, @PatientMasterVisitID, @LocationID, @OrderedBy, @PrescribedDate, @DispensedBy, @DispensedDate, @TreatmentProgram, 
			@UserID, Getdate(), 1, @RegimenLine, @PharmacyNotes, @visitPK, @PeriodTaken);

		Set @ptn_pharmacy =SCOPE_IDENTITY();

		Insert into ARVTreatmentTracker (PatientId,ServiceAreaId,PatientMasterVisitId,RegimenId,RegimenLineId,
		TreatmentStatusId,TreatmentStatusReasonId, DeleteFlag, CreateBy, createdate)
		values(@patientid,@moduleid,@PatientMasterVisitID,@Regimen,@RegimenLine,@TreatmentPlan,@TreatmentPlanReason,
		0,@UserID,GETDATE())

		Update ord_PatientPharmacyOrder Set
			ReportingID = (Select Right('000000' + Convert(varchar, @ptn_pharmacy), 6))
		Where ptn_pharmacy_pk = @ptn_pharmacy;

		--If (@DispensedByDate Is Not Null And @DispensedBy > 0) Begin
		--	Update ord_PatientPharmacyOrder Set
		--		OrderStatus = 2
		--	Where ptn_pharmacy_pk = @ptn_pharmacy;
		--End

		
		If(@RegimenType Is Not Null) 
		Begin	
			Insert Into dtl_RegimenMap (Ptn_Pk,	LocationID,	Visit_Pk, RegimenType, OrderId,	UserID,	CreateDate)
			Values (@ptn_pk, @LocationID, @visitPK, @RegimenType, @ptn_pharmacy, @UserID, Getdate());
		End

		Select @ARTStartDate = dbo.fn_GetPatientARTStartDate_constella(@ptn_pk)
		Update mst_Patient Set
			ARTStartDate = @ARTStartDate
		Where ptn_pk = @ptn_pk;

		Select @ptn_pharmacy;

	END
End

-- get prescription
--IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_getPatientPharmacyPrescription]') AND type in (N'P', N'PC'))
/****** Object:  StoredProcedure [dbo].[sp_getPatientPharmacyPrescription]    Script Date: 11/21/2017 8:38:04 AM ******/
--SET ANSI_NULLS ON

GO
