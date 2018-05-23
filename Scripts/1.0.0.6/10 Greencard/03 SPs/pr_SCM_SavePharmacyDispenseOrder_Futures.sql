IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_SCM_SavePharmacyDispenseOrder_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_SCM_SavePharmacyDispenseOrder_Futures]
GO
/****** Object:  StoredProcedure [dbo].[pr_SCM_SavePharmacyDispenseOrder_Futures]    Script Date: 5/23/2018 10:18:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[pr_SCM_SavePharmacyDispenseOrder_Futures]                
	@Ptn_Pk int,                
	@LocationId int,                
	@DispensedBy int,                
	@DispensedByDate datetime,                
	@OrderType int,                
	@ProgramId int=225,                
	@StoreId int,            
	@Regimen varchar(50),                
	@UserId int,        
	@OrderId int = 0   ,
	@ModuleId int = null,
	@PharmacyRefillAppDate datetime   = Null,
	@PeriodTaken int = null,
	@RegimenLine int = null,
	@ProviderId int =null,
	@Height numeric(8,2)= null,
	@Weight numeric(8,2)= null,
	@PharmacyNotes varchar(250) =''
As             
Begin
	Declare @VisitId int,@ARTStartDate datetime;
	Select @Regimen = Nullif(Ltrim(Rtrim(@Regimen)), '');
	If (@OrderId > 0) Begin
		
		Select @VisitId = VisitId
		From dbo.Ord_PatientPharmacyOrder
		Where Ptn_Pharmacy_Pk = @OrderId;

		Update dbo.Ord_Visit Set
			VisitDate = @DispensedByDate,
			DataQuality = 1,
			UserId = @UserId
		Where Ptn_Pk = @VisitId;

		Update dbo.Ord_PatientPharmacyOrder Set
			DispensedBy = @DispensedBy,
			DispensedByDate = @DispensedByDate,
			StoreId = @StoreId,
			UserId = @UserId,
			UpdateDate = Getdate()
		Where Ptn_Pharmacy_Pk = @OrderId;
		
		If (@Regimen Is Not Null) Begin
			Update dbo.Dtl_RegimenMap Set
				RegimenType = @Regimen
			Where ptn_pk = @Ptn_pk
			And Visit_Pk = @VisitId
			And OrderId = @OrderId;			
		End
	
		--exec pr_SCM_SetPharmacyRefillAppointment @Ptn_Pk,@LocationId,@VisitId,@PharmacyRefillAppDate,@UserId,@UserId   
		--  Delete from Dtl_PatientPharmacyOrder where Ptn_Pharmacy_Pk = @OrderId            
		Select	@VisitId [VisitId],
				@OrderId [Ptn_Pharmacy_Pk];
	End 
	Else Begin

		Select @ModuleId = Isnull(@ModuleId,ModuleId) From mst_module Where ModuleName='Pharmacy';

		declare @PatientId int = (select top 1 Id from patient where ptn_pk = @Ptn_Pk);
		declare @PatientMasterVisitId int
		declare @PharmacyEncounter int = (select top 1 Id from lookupitem where Name = 'Pharmacy-encounter')
		declare @ServiceArea int = (select top 1 ModuleId from mst_module where ModuleName = 'Pharmacy')

		if exists(select 1 from PatientMasterVisit where VisitDate between dateadd(day, -2, cast(@DispensedByDate as date)) and dateadd(day, 2, cast(@DispensedByDate as date)) and PatientId = @PatientId)
		begin
			set @PatientMasterVisitId = (select top 1 Id from PatientMasterVisit where VisitDate between dateadd(day, -2, cast(@DispensedByDate as date)) and dateadd(day, 2, cast(@DispensedByDate as date)) and PatientId = @PatientId)
			Insert into PatientEncounter (PatientId,EncounterTypeId,PatientMasterVisitId,EncounterStartTime,EncounterEndTime,ServiceAreaId,DeleteFlag,CreatedBy,CreateDate,AuditData,[Status])
			Values(@PatientId,@PharmacyEncounter,@PatientMasterVisitId,getdate(),getdate(),isnull(@ServiceArea,0),0,@UserId,getdate(),NULL,0)
		end
		ELSE
		begin
			if exists(select 1 from mst_patient where RegisteredAtPharmacy = 0 and Ptn_Pk = @Ptn_Pk)
			begin
				insert into PatientMasterVisit (PatientId, ServiceId,Start,[End],Active,VisitDate,VisitScheduled,VisitBy,VisitType,[Status],CreateDate,DeleteFlag,CreatedBy,AuditData)
				values(@PatientId,1,getdate(),NULL,0,cast(getdate() as date),NULL,NULL,NULL,NULL,getdate(),0,@UserId,NULL) 
				set @PatientMasterVisitId = SCOPE_IDENTITY()
				Insert into PatientEncounter (PatientId,EncounterTypeId,PatientMasterVisitId,EncounterStartTime,EncounterEndTime,ServiceAreaId,DeleteFlag,CreatedBy,CreateDate,AuditData,[Status])
				Values(@PatientId,@PharmacyEncounter,@PatientMasterVisitId,getdate(),getdate(),isnull(@ServiceArea,0),0,@UserId,getdate(),NULL,0)
			end
			
		end
		
		Insert Into dbo.Ord_Visit (
			Ptn_Pk,
			LocationId,
			VisitDate,
			VisitType,
			DataQuality,
			DeleteFlag,
			UserId,
			ModuleID,
			CreateDate)
		Values (
			@Ptn_Pk, 
			@LocationId, 
			@DispensedByDate, 
			4, 
			0, 
			0, 
			@UserId,
			@ModuleId, 
			Getdate());
		Select @VisitId = SCOPE_IDENTITY();	

		If(@Height Is Not Null OR @Weight Is Not Null) Begin
			Insert Into dtl_PatientVitals (
				Ptn_pk
				,LocationID
				,Visit_pk
				,Height
				,Weight)
			Values (
				@Ptn_Pk
				,@LocationId
				,@VisitId
				,@Height
				,@Weight);
		End
		Insert Into dbo.Ord_PatientPharmacyOrder (
			Ptn_Pk,
			VisitId,
			LocationId,
			OrderedBy,
			OrderedByDate,
			DispensedBy,
			DispensedByDate,
			OrderType,
			ProgId,
			StoreId,
			DeleteFlag,			
			UserId,
			CreateDate,
			PharmacyPeriodTaken,
			ProviderID,
			RegimenLine,
			Signature,
			Height,
			Weight,
			PharmacyNotes,
			PatientMasterVisitId,
			PatientId,
			OrderStatus
		)
		Values (
			@Ptn_Pk, 
			@VisitId, 
			@LocationId, 
			@DispensedBy, 
			@DispensedByDate, 
			@DispensedBy, 
			@DispensedByDate, 
			@OrderType, 
			@ProgramId, 
			@StoreId, 
			0, 
			@UserId, 
			Getdate(),
			@PeriodTaken,
			@ProviderId,
			@RegimenLine,
			@UserId,
			@Height,
			@Weight,
			@PharmacyNotes,
			@PatientMasterVisitId,
			@PatientId,
			2);

		Select @OrderId = SCOPE_IDENTITY();
		Update ord_PatientPharmacyOrder Set
			ReportingID = (Select Right('000000' + Convert(varchar, @OrderId), 6))
		Where ptn_pharmacy_pk = @OrderId;


		If (@Regimen Is Not Null) Begin
			Insert Into dbo.Dtl_RegimenMap (
				Ptn_Pk,
				LocationId,
				Visit_Pk,
				RegimenType,
				OrderId,
				DeleteFlag,
				UserId,
				CreateDate)
			Values (
				@Ptn_Pk, 
				@LocationId, 
				@VisitId, 
				@Regimen, 
				@OrderId, 
				0, 
				@UserId, 
				Getdate());
		End
		--exec pr_SCM_SetPharmacyRefillAppointment @Ptn_Pk,@LocationId,@VisitId,@PharmacyRefillAppDate,@UserId,@UserId   
		Select	@VisitId [VisitId],
				@OrderId [Ptn_Pharmacy_Pk];
	End
	Select @ARTStartDate = dbo.fn_GetPatientARTStartDate_constella(@Ptn_pk);
	Update mst_Patient Set
		ARTStartDate = @ARTStartDate
	Where ptn_pk = @Ptn_pk;
End

