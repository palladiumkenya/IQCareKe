/****** Object:  StoredProcedure [dbo].[Pr_Clinical_SaveFollowupEducation_Constella]    Script Date: 02/09/2016 10:49:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_Clinical_SaveFollowupEducation_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_Clinical_SaveFollowupEducation_Constella]
GO

/****** Object:  StoredProcedure [dbo].[Pr_Clinical_SaveFollowupEducation_Constella]    Script Date: 02/09/2016 10:49:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Pr_Clinical_SaveFollowupEducation_Constella]                                    
(               
                           
	@Id int=null,                      
	@Ptn_Pk int,        
	@VisitPk int,     
	@LocationID int,                                  
	@CouncellingTypeId int=null,                                    
	@CouncellingTopicId int=null,                              
	@VisitDate datetime=null,              
	@Comments varchar(250)=null,                
	@OtherDetail varchar(250) =null,            
	@UserId int=null,                      
	@DeleteFlag int=null    ,
	@ModuleId int =null          
              
)                   
                                 
as                                    
Begin

	Begin Transaction CTC
	Begin Try
                               
	 If (@Id=-1)  Begin
		declare @VisitId int;

		--If Exists(Select 1	From ord_Visit	Where Ptn_Pk = @Ptn_Pk	And VisitType = 10	And VisitDate = @VisitDate And LocationID = @LocationId and DeleteFlag = 0) Begin
		Select @VisitId = Visit_Id		From ord_Visit	Where Ptn_Pk = @Ptn_Pk	And VisitType = 10	And VisitDate = @VisitDate	And LocationID = @LocationId	And @DeleteFlag = 0;
		If(@VisitId Is Null) Begin
			Insert Into ord_visit (
				Ptn_Pk,
				VisitDate,
				LocationID,
				VisitType,
				DeleteFlag,
				UserID,
				CreateDate,
				ModuleID)
			Values (
				@Ptn_Pk,
				@VisitDate,
				@LocationID,
				10,
				@DeleteFlag,
				@UserId,
				getdate(),
				@ModuleId);
			Select @VisitId = scope_identity();
		End
		Insert Into dtl_FollowupEducation (
			Ptn_Pk,
			CouncellingTypeId,
			CouncellingTopicId,
			VisitDate,
			Visit_pk,
			Comments,
			OtherDetail,
			Createdate,
			UserId,
			LocationId)
		Values (
			@Ptn_Pk,
			@CouncellingTypeId,
			@CouncellingTopicId,
			@VisitDate,
			@VisitId,
			@Comments,
			@OtherDetail,
			getdate(),
			@UserId,
			@LocationID )
	End 
	Else Begin
		Update dtl_FollowupEducation Set
			CouncellingTypeId = @CouncellingTypeId,
			CouncellingTopicId = @CouncellingTopicId,
			VisitDate = @VisitDate,
			Comments = @Comments,
			OtherDetail = @OtherDetail,
			Updatedate = getdate()
		Where Id = @Id
		And Ptn_pk = @Ptn_pk;
	End
	If @@TRANCOUNT > 0 Commit Transaction CTC;
	End Try 
	Begin Catch

		Declare @ErrorMessage nvarchar(4000), @ErrorSeverity int, @ErrorState int;
		Select	@ErrorMessage = error_message(),
				@ErrorSeverity = error_severity(),
				@ErrorState = error_state();
		Raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState);
		If @@TRANCOUNT > 0 Rollback Transaction CTC;
	End Catch;

End
GO


