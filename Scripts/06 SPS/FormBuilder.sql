IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_ManageForm_GetAllFormDetail_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_ManageForm_GetAllFormDetail_Futures]
GO

/****** Object:  StoredProcedure [dbo].[Pr_ManageForm_GetAllFormDetail_Futures]    Script Date: 8/4/2016 10:56:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--Pr_ManageForm_GetAllFormDetail_Futures 1,0,161    
                
-- =============================================                  
-- Author:  Ajay Kumar                
-- Create date: 23-12-2010                  
-- Description: Used to fetch all the form details                  
------ Pr_Pmtct_GetFormDetail_Futures 2                  
---- Pr_ManageForm_GetFormDetail_Futures 3,0, 161                 
---- Pr_Pmtct_GetFormDetail_Futures 1                  
---- Pr_Pmtct_GetFormDetail_Futures 3                  
-- =============================================                  
CREATE PROCEDURE [dbo].[Pr_ManageForm_GetAllFormDetail_Futures]                  
@FormStatus varchar(50) ,               
@TechArea varchar(50) ,               
@CountryId int   
AS                  
                  
BEGIN                  
If( @FormStatus='3' or @FormStatus='' or @FormStatus is null) Begin                              
	Select	F.FeatureName																		As FormName
		,	U.UserName																			As UpdatedBy
		,	isnull(convert(varchar, F.UpdateDate, 103), convert(varchar, F.CreateDate, 103))	As LastUpdate
		,	F.Published
		,	F.FeatureID																			As FormId
	From mst_Feature As F
	Inner Join mst_User As U On F.UserID = U.UserID
	Where (F.DeleteFlag = 0)
		And (F.FeatureID > 1000)
		And (F.CountryId = @CountryId)
		And (F.ModuleId = Case (@TechArea)
								When 0 Then F.moduleid
								Else @TechArea
							End)
		And (F.ModuleId In (Select ModuleId		From mst_module		Where (Status = 2)		)
		)
	Order By F.Published, FormName               
 End                  
Else If(@FormStatus='0') Begin                  
	Select	F.FeatureName																		As FormName
		,	U.UserName																			As UpdatedBy
		,	isnull(convert(varchar, F.UpdateDate, 103), convert(varchar, F.CreateDate, 103))	As LastUpdate
		,	F.Published
		,	F.FeatureID																			As FormId
	From mst_Feature As F
	Inner Join mst_User As U On F.UserID = U.UserID
	Where (F.Published = 0 Or F.Published Is Null)
		And (F.DeleteFlag = 0)
		And (F.FeatureID > 1000)
		And (F.CountryId = @CountryId)
		And (F.ModuleId = Case (@TechArea)
								When 0 Then F.ModuleId
								Else @TechArea
							End)
		And (F.ModuleId In (Select ModuleId	From mst_module	Where (Status = 2)	)	)
	Order By FormName                
 End                  
Else If(@FormStatus='1')  Begin   
	SELECT z.FormName,z.UpdatedBy,z.LastUpdate ,z.Published,z.FormId FROM  
	(
		Select	F.FeatureName																		As FormName
			,	U.UserName																			As UpdatedBy
			,	isnull(convert(varchar, F.UpdateDate, 103), convert(varchar, F.CreateDate, 103))	As LastUpdate
			,	F.Published
			,	F.FeatureID																			As FormId
		From mst_Feature As F
		Inner Join mst_User As U On F.UserID = U.UserID
		Where (F.Published = 2)
			And (F.DeleteFlag = 0)
			And (F.FeatureID > 1000)
			And (F.CountryId = @CountryId)
			And (F.ModuleId = Case (@TechArea)
									When 0 Then F.ModuleId
									Else @TechArea
								End)
			And (F.ModuleId In (Select ModuleId	From mst_module	Where (Status = 2)	)	)    
		UNION   
		Select	F.FeatureName																		As FormName
			,	U.UserName																			As UpdatedBy
			,	isnull(convert(varchar, F.UpdateDate, 103), convert(varchar, F.CreateDate, 103))	As LastUpdate
			,	F.Published
			,	F.FeatureID																			As FormId
		From mst_Feature As F
		Inner Join mst_User As U On F.UserID = U.UserID
		Where (F.Published = 2)
			And (F.DeleteFlag = 0)
			And (F.FeatureID = 126)
			And (F.ModuleId = Case (@TechArea)
									When 0 Then F.ModuleId
									Else @TechArea
								End) 
	  )z               
           
	  Order by  z.FormName ,z.FormId               
 End                  
Else If(@FormStatus='2') Begin                  
	Select	F.FeatureName																		As FormName
		,	F_1.UserName																		As UpdatedBy
		,	isnull(convert(varchar, F.UpdateDate, 103), convert(varchar, F.CreateDate, 103))	As LastUpdate
		,	F.Published
		,	F.FeatureID																			As FormId
	From mst_Feature As F
	Inner Join mst_User As F_1 On F.UserID = F_1.UserID
	Where (F.Published = 1)
		And (F.DeleteFlag = 0)
		And (F.FeatureID > 1000)
		And (F.CountryId = @CountryId)
		And (F.ModuleId = Case (@TechArea)
								When 0 Then F.ModuleId
								Else @TechArea
							End)
		And (F.ModuleId In (Select ModuleId	From mst_module	Where (Status = 2)	)		)
	Order By FormName           
 End              
                  
End

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_ImportExportForms_ImportForm_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_ImportExportForms_ImportForm_Futures]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[Pr_ImportExportForms_ImportForm_Futures]                    
 @FeatureId int,                    
 @FeatureName varchar(50), 
 @FormDescription varchar(100)= null,           
 @ReportFlag int,            
 @DeleteFlag int,            
 @AdminFlag int,            
 @UserId int,            
 @OptionalFlag int=null,            
 @SystemId int,            
 @Published int,            
 @CountryId int,            
 @ModuleId int ,        
 @MultiVisit int=null,
 @ReferenceId varchar(50) = null                   
as                    
begin
              
 declare @iFeatureId as int
   
	IF(@FeatureId='126') Begin
		SET @iFeatureId = @FeatureId
		UPDATE mst_feature SET
		published = 2
		WHERE featureid = @iFeatureId
	END 
	ELSE BEGIN
		IF EXISTS (SELECT * FROM mst_feature WHERE featureName = '' + @FeatureName + ''	AND deleteflag = 0	AND ModuleId = '' + @ModuleId + '') BEGIN
			SELECT @iFeatureId = FeatureId
			FROM mst_feature
			WHERE featureName = '' + @FeatureName + ''
		END 
		ELSE BEGIN
			Select @ReferenceId = Isnull(@ReferenceId, Convert(varchar(36),newid()));
			EXEC  @iFeatureId = Pr_FormBuilder_SaveMstFeature_Futures	
						@FeatureId= 0
					,	@FeatureName= @FeatureName
					,	@FormDescription = @FormDescription
					,	@ReportFlag = @ReportFlag
					,	@DeleteFlag = @DeleteFlag
					,	@AdminFlag = @AdminFlag
					,	@OptionalFlag =@OptionalFlag
					,	@SystemId=@SystemId
					,	@UserId=@UserId
					,	@Published=@Published
					,	@CountryId=@CountryId
					,	@ModuleId=@ModuleId
					,	@MultiVisit=@MultiVisit
					,	@ReferenceId = @ReferenceId
			
			UPDATE mst_feature SET
				published = 2
			WHERE featureid = @iFeatureId
		END
	END
	SELECT @iFeatureId FeatureId

END

GO




IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_FormBuilder_SaveUpdateFormModuleLink_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_FormBuilder_SaveUpdateFormModuleLink_Futures]
GO

/****** Object:  StoredProcedure [dbo].[pr_FormBuilder_SaveUpdateFormModuleLink_Futures]    Script Date: 7/30/2016 4:55:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_FormBuilder_SaveUpdateFormModuleLink_Futures]                                            
(                                            
 @ModuleID int,                                            
 @FormID int,    
 @DeleteFlag int,                                                                         
 @UserID int    
    
                                              
)                                            
AS        
                                                              
Begin
      
	declare @count int;
	SELECT @Count = Count(FeatureId)	FROM mst_Feature	WHERE FeatureId = @FormID	AND ModuleID = @ModuleId;
	 If @DeleteFlag=1 Begin
		DELETE FROM dbo.lnk_SplFormModule
		WHERE ModuleId = @ModuleId
	END
	IF (@FormID <> -1 And @count = 0) BEGIN
		INSERT INTO dbo.lnk_SplFormModule
		(	FeatureId
		,	ModuleID
		,	UserID
		,	CreateDate
		)
		VALUES (
				@FormID
			,	@ModuleId
			,	@UserID
			,	getdate())
	END
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_FormBuilder_FormModuleLinking]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_FormBuilder_FormModuleLinking]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<John Macharia>
-- Create date: <18th Dec 2013>
-- Description:	<Form linking to service areas>
-- Update By : <Bhupendra Singh>
-- Ref : <Bug ID 612 [Redmine 3.8.0]>
-- =============================================
CREATE PROCEDURE [dbo].[Pr_FormBuilder_FormModuleLinking] (
 @ModuleID INT
 ,@CountryID INT
 )
AS
BEGIN
		SELECT	ModuleID
			,	ModuleName
			,   a.DisplayName
			, a.CanEnroll
			
		FROM mst_module AS a
		WHERE (DeleteFlag = 0 OR DeleteFlag IS NULL)
			AND (Status = 2)
			And CanEnroll = 1
			--AND (IsPlugIn = 0 OR IsPlugIn IS NULL)
			--AND (ModuleID NOT IN (207, 206))
		ORDER BY ModuleName

	IF @ModuleId = 0 BEGIN
		SELECT DISTINCT	FeatureID
					,	FeatureName
					,	'False'	AS Selected
					,	'False'	AS 'IsModule'
		FROM mst_Feature AS b
		WHERE (ReferenceID In ('PHARMACY','LABORATORY','CCC_INITIAL_FOLLOWUP','ART_HISTORY','ART_THERAPY','SERVICE_REQUEST','CONSUMABLES_ISSUANCE'))
			AND (DeleteFlag = 0)
			OR (FeatureID > 1000)
			AND (DeleteFlag = 0)
			AND (CountryId = @CountryID)
			AND (FeatureName NOT LIKE 'CareEnd%')
			AND (FeatureName NOT LIKE 'patient home%')
			AND (FeatureName NOT LIKE 'facility home%')
		ORDER BY FeatureName
	END 
	ELSE BEGIN
		WITH Existing_FeatureData AS (
			SELECT DISTINCT	tbl1.FeatureID
					,	tbl1.FeatureName
					, tbl1.ReferenceID FeatureReferenceId
					,	CASE isnull(tbl2.ModuleID, 0)
							WHEN 0 THEN 'False'
							ELSE 'True'
						END		AS 'Selected'
					,	'True'	AS 'IsModule'
		FROM mst_Feature AS tbl1
		INNER JOIN mst_Feature AS tbl2 ON tbl1.FeatureID = tbl2.FeatureID
		AND tbl2.ModuleId = @ModuleID
		WHERE (tbl1.DeleteFlag = 0 OR tbl1.DeleteFlag IS NULL)
			AND (tbl1.ReferenceID In ('PHARMACY','LABORATORY','CCC_INITIAL_FOLLOWUP','ART_HISTORY','ART_THERAPY','SERVICE_REQUEST','CONSUMABLES_ISSUANCE'))
			OR (tbl1.DeleteFlag = 0 OR tbl1.DeleteFlag IS NULL)
			AND (tbl1.FeatureID > 1000)
			AND (tbl1.CountryId = @CountryID)
			AND (tbl1.FeatureName NOT LIKE 'CareEnd%')
			AND (tbl1.FeatureName NOT LIKE 'patient home%')
			AND (tbl1.FeatureName NOT LIKE 'facility home%')
		),
		SplForm_FeatureData	AS (
			SELECT DISTINCT	tbl1.FeatureID
					,	tbl1.FeatureName
					,  tbl1.ReferenceID FeatureReferenceId
					,	CASE isnull(tbl2.ModuleID, 0)
							WHEN 0 THEN 'False'
							ELSE 'True'
						END		AS 'Selected'
					,	'False'	AS 'IsModule'
		FROM mst_Feature AS tbl1
		LEFT OUTER JOIN lnk_SplFormModule AS tbl2 ON tbl1.FeatureID = tbl2.FeatureId
		AND tbl2.ModuleId = @ModuleID
		WHERE (tbl1.DeleteFlag = 0 OR tbl1.DeleteFlag IS NULL)
			AND (tbl1.ReferenceID In ('PHARMACY','LABORATORY','CCC_INITIAL_FOLLOWUP','ART_HISTORY','ART_THERAPY','SERVICE_REQUEST','CONSUMABLES_ISSUANCE'))
			AND (tbl1.FeatureID NOT IN (
			SELECT FeatureID	FROM Existing_FeatureData
			)
			)
			OR (tbl1.DeleteFlag = 0 OR tbl1.DeleteFlag IS NULL)
			AND (tbl1.FeatureID > 1000)
			AND (tbl1.FeatureID NOT IN (
			SELECT FeatureID FROM Existing_FeatureData
			)
			)
			AND (tbl1.CountryId = @CountryID)
			AND (tbl1.FeatureName NOT LIKE 'CareEnd%')
			AND (tbl1.FeatureName NOT LIKE 'patient home%')
			AND (tbl1.FeatureName NOT LIKE 'facility home%')
		)
		SELECT *  FROM SplForm_FeatureData  
		UNION ALL
		SELECT *  FROM Existing_FeatureData
		ORDER BY FeatureName
END

--
SELECT   COUNT(*) AS FormCount FROM    lnk_SplFormModule WHERE   (ModuleId = @ModuleID)
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_FormBuilder_FetchMaxValue_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_FormBuilder_FetchMaxValue_Futures]
GO
/****** Object:  StoredProcedure [dbo].[pr_FormBuilder_GetModuleIdentifier_Constella]    Script Date: 03/17/2016 08:15:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_FormBuilder_GetModuleIdentifier_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_FormBuilder_GetModuleIdentifier_Constella]
GO
/****** Object:  StoredProcedure [dbo].[Pr_FormBuilder_FetchUpdateFormDetail_Futures]    Script Date: 6/27/2016 11:55:30 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_FormBuilder_FetchUpdateFormDetail_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_FormBuilder_FetchUpdateFormDetail_Futures]
GO

/****** Object:  StoredProcedure [dbo].[Pr_FormBuilder_FetchUpdateFormDetail_Futures]    Script Date: 6/27/2016 11:55:30 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Pr_FormBuilder_FetchUpdateFormDetail_Futures] (                         
	@iFeatureId int      
)                    
As                          
Begin    
	Declare @VisitType int  
	--0                        
	Select	F.FeatureID
		,	F.FeatureName
		,	V.VisitName FormName
		,	V.FormDescription
		,	V.VisitTypeID FormId
		,	V.Custom
		,	V.CategoryId
		,	F.ReportFlag
		,	F.DeleteFlag
		,	F.AdminFlag
		,	F.UserID
		,	F.CreateDate
		,	F.UpdateDate
		,	F.OptionalFlag
		,	F.SystemId
		,	F.Published
		,	F.CountryId
		,	F.ModuleId
		,	F.MultiVisit
		,	F.Seq
		,	F.RegistrationFormFlag
		,	F.ReferenceId
		,	F.CanLink
	From mst_Feature As F
	Inner Join mst_VisitType As V On V.FeatureId = F.FeatureID
	Where (F.FeatureID = @iFeatureId)
		And (F.DeleteFlag = 0)     
	--1                   
	Select	SectionID
			,SectionName
			,CustomFlag
			,DeleteFlag
			,UserID
			,CreateDate
			,UpdateDate
			,FeatureId
			,Seq
			,IsGridView
			,SectionInfo
	From mst_Section
	Where (FeatureId = @iFeatureId)
	And (DeleteFlag = 0)
	Order By Seq                      
	--select * from lnk_forms where featureid=@iFeatureId  order by sectionId,seq       
	--2                
	Select	Id
			,FeatureId
			,SectionId
			,Case
				When Predefined = 0 Then '8888' + convert(varchar, FieldId)
				When Predefined = 1 Then '9999' + convert(varchar, FieldId)
			End As FieldId
			,FieldLabel
			,UserId
			,CreateDate
			,UpdateDate
			,Predefined
			,Seq
	From Lnk_Forms
	Where (FeatureId = @iFeatureId)
	Order By SectionId, seq                 
	--3                
	Select Distinct	mst.TabID
					,mst.TabName
					,mst.FeatureID
					,mst.DeleteFlag
					,mst.UserID
					,mst.CreateDate
					,mst.UpdateDate
					,mst.seq
					,isnull(mst.Signature, 0) As Signature
	From Mst_FormBuilderTab As mst
	Inner Join lnk_FormTabSection As lnk On mst.TabID = lnk.TabID
	Where (mst.FeatureID = @iFeatureId)
	And (mst.DeleteFlag = 0)
	Order By mst.seq
	--select * from  Mst_FormBuilderTab where FeatureID=@iFeatureId   and DeleteFlag=0   order by seq                 
	--select * from  Lnk_FormTabSection where FeatureID=@iFeatureId          
	--4  
	Select Distinct	TabID
					,SectionID
					,FeatureID
					,UserID
	From lnk_FormTabSection
	Where (FeatureID = @iFeatureId) 
	--5   
Select @VisitType=VisitTypeID from mst_visittype where FeatureId=@iFeatureId  
if exists(select * from ord_Visit where VisitType=@VisitType)  
  Begin  
   Select '1'[Signature]         
        End    
else   Begin Select '0'[Signature] End       

--6
	Select	m.FeatureID As ServiceAreaName
			,l.BusRuleId
			,l.Value
			,l.Value1
			,l.SetType
	From mst_Feature As m
	Inner Join lnk_featureBusinessRule As l On l.FeatureID = m.FeatureID
	Where (l.FeatureID = @iFeatureId)    
end

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_ImportExportForms_ImportLnkForm_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_ImportExportForms_ImportLnkForm_Futures]
GO

/****** Object:  StoredProcedure [dbo].[Pr_ImportExportForms_ImportLnkForm_Futures]    Script Date: 8/1/2016 12:12:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Pr_ImportExportForms_ImportLnkForm_Futures]   (                       
	@Id int,                          
	@FeatureId int,                          
	@SectionId int,                          
	@FieldId int,                    
	@FieldName varchar(100),                        
	@FieldLabel varchar(100),                     
	@ControlId int=null,                       
	@SelectListVal xml=null,                  
	@BusRuleIdValAll varchar(max),                  
	@Seq int,                         
	@UserId int,                          
	@Predefined int,    
	@PatientRegistration int=null  
)                       
AS                          
BEGIN                    
	Declare @iNewFieldId int;
	declare @returntable table(id int);                   
	IF(@Predefined=0) BEGIN                  
		--no change in select list value in case custom field alread exist.                  
		IF Exists (SELECT * FROM mst_customFormField WHERE FieldName = '' + @FieldName + ''	AND deleteflag = 0) BEGIN
			IF (@PatientRegistration = '1') BEGIN
				IF NOT EXISTS (SELECT * FROM mst_customFormField WHERE FieldName = '' + @FieldName + '' 	AND deleteflag = 0	AND patientRegistration = 1) BEGIN
					EXEC  Pr_PMTCT_SaveUpdateCustomFields_Futures	'0'
												,	@FieldName
												,	@ControlID
												,	'0'
												,	@UserID
												,	'2'
												,	'2'
												,	@SelectListVal
												,	@Predefined
												,	'0'
					SELECT @iNewFieldId = Id FROM mst_customFormField WHERE fieldName = '' + @FieldName + ''	AND deleteflag = 0	AND patientRegistration = 1
				END 
				ELSE BEGIN
					SELECT @iNewFieldId = Id FROM mst_customFormField WHERE fieldName = '' + @FieldName + '' AND deleteflag = 0 AND patientRegistration = 1;
					EXEC  Pr_PMTCT_SaveUpdateCustomFields_Futures	@iNewFieldId
												,	@FieldName
												,	@ControlId
												,	0
												,	@UserId
												,	0
												,	5
												,	@SelectListVal
												,	@Predefined
												,	0
				END
			END	 
			ELSE BEGIN
				--use same fieldId
				IF NOT EXISTS (SELECT * FROM mst_customFormField WHERE FieldName = '' + @FieldName + ''	AND deleteflag = 0	AND (patientRegistration = 0 OR patientRegistration IS NULL)) BEGIN
					--INSERT INTO @returntable(id)
					EXEC  Pr_PMTCT_SaveUpdateCustomFields_Futures	NULL
											,	@FieldName
											,	@ControlId
											,	0
											,	@UserId
											,	0
											,	0
											,	@SelectListVal
											,	@Predefined
											,	0
					--set @iNewFieldId=ident_current('mst_CustomFormField')
					SELECT @iNewFieldId = Id FROM mst_customFormField WHERE FieldName = '' + @FieldName + ''	AND deleteflag = 0	AND (patientRegistration = 0 OR patientRegistration IS NULL)
				END 
				ELSE BEGIN
					SELECT @iNewFieldId = Id FROM mst_customFormField WHERE fieldName = '' + @FieldName + '' AND deleteflag = 0	AND (patientRegistration = 0 OR patientRegistration IS NULL)
					--INSERT INTO @returntable(id)
					EXEC  Pr_PMTCT_SaveUpdateCustomFields_Futures	@iNewFieldId
						,	@FieldName
						,	@ControlId
						,	0
						,	@UserId
						,	0
						,	5
						,	@SelectListVal
						,	@Predefined
						,	0
				END
			END
		END 
		ELSE BEGIN
			-- insert field and corresponding select list value       
			IF (@PatientRegistration = '1') BEGIN
				--INSERT INTO @returntable(id)
				EXEC  Pr_PMTCT_SaveUpdateCustomFields_Futures	'0'
														,	@FieldName
														,	@ControlID
														,	'0'
														,	@UserID
														,	'2'
														,	'2'
														,	@SelectListVal
														,	@Predefined
														,	'0'
				SELECT @iNewFieldId = Id FROM mst_customFormField WHERE fieldName = '' + @FieldName + '' AND deleteflag = 0	AND (patientRegistration = 1);
			END 
			ELSE BEGIN
			INSERT INTO @returntable(id)
				EXEC Pr_PMTCT_SaveUpdateCustomFields_Futures	NULL
											,	@FieldName
											,	@ControlId
											,	0
											,	@UserId
											,	0
											,	0
											,	@SelectListVal
											,	@Predefined
											,	0
				SELECT @iNewFieldId = Id FROM mst_customFormField WHERE fieldName = '' + @FieldName + '' AND deleteflag = 0	AND (patientRegistration = 0 OR patientRegistration IS NULL);
			END
			--SET @iNewFieldId = ident_current('mst_CustomFormField')
		END
	END 
	ELSE BEGIN
		--if predefined field then use the same id as previous or the passed one,no change in business rule and                   
		--select list values in case of predefined.                  
		SET @iNewFieldId = @FieldId
	END                
                  
	Declare @tempBusRuleCount int                  
	Declare @tempBusRuleValCount int                  
	Declare @tempBusRuleId int                  
	Declare @tempBusRulevalue Varchar(100)                 
	Declare @tempBusRuleSplitvalue Varchar(100)                   
	Declare @Index int                  
	Declare @Index2 int                  
                  
--business rules for the field                  
--re-eneter all business rule again, and remove previously set business rule, applies to both predefined and non                  
--predefined fields                  
	DELETE FROM lnk_fieldsBusinessRule WHERE FieldId=@iNewFieldId and predefined=@Predefined ;                
           
	SELECT @tempBusRuleCount = count(*) FROM dbo.fnParseDelimitedList(@BusRuleIdValAll,',') ;                                           
	SET @Index = 1;                                                          
	WHILE @Index <= @tempBusRuleCount   BEGIN                
		SET @tempBusRulevalue = dbo.fnGetParmTilte(@Index,@BusRuleIdValAll)                  
		--print '@tempBusRulevalue=' + @tempBusRulevalue                  
		SELECT @tempBusRuleValCount = count(*) FROM dbo.fnParseDelimitedList(@tempBusRulevalue,'-')                    
		--print '@tempBusRuleValCount='  + convert(varchar,@tempBusRuleValCount)                                       
		SET @Index2 = 1;                                                          
		WHILE @Index2 <= @tempBusRuleValCount BEGIN                   
			IF(@Index2=1) BEGIN                        
				 --print dbo.fnGetParmTilte(@Index2,@tempBusRulevalue)                              
				SET @tempBusRuleId = dbo.fnGetParmTilteForHighphen(@Index2,@tempBusRulevalue)                    
			End                
			ELSE  BEGIN                 
				--print  dbo.fnGetParmTilte(@Index2,@tempBusRulevalue)                 
				SET @tempBusRuleSplitvalue = dbo.fnGetParmTilteForHighphen(@Index2,@tempBusRulevalue)                    
			END                
			SET @Index2=@Index2+1                  
		END --end of Index2 Mob                  
		IF (@tempBusRuleSplitvalue='Null') BEGIN               
			--print 'split value null=' + @tempBusRuleSplitvalue                
			EXEC Pr_PMTCT_SaveBusinessRules_Futures @iNewFieldId,@tempBusRuleId,Null, @UserId,@Predefined                                 
		END                
		ELSE BEGIN                
			--print 'split value not null=' + @tempBusRuleSplitvalue                
			EXEC Pr_PMTCT_SaveBusinessRules_Futures @iNewFieldId,@tempBusRuleId,@tempBusRuleSplitvalue, @UserId,@Predefined                                 
		END                
		SET @Index = @Index+1;                      
	END                   
--if field already exists on form , irrespective of section. then dont insert in lnk_forms.                  
	IF Not Exists(SELECT * FROM lnk_forms WHERE featureid=@FeatureId AND FieldId=@iNewFieldId AND Predefined=@Predefined) BEGIN 
		--print 'lnk_forms'                    
		INSERT INTO lnk_forms
		(	FeatureId
			,	SectionId
			,	FieldId
			,	FieldLabel
			,	Seq
			,	UserId
			,	Predefined
			,	CreateDate
		)
		VALUES (
				@FeatureId
			,	@SectionId
			,	@iNewFieldId
			,	@FieldLabel
			,	@Seq
			,	@UserId
			,	@Predefined
			,	getdate())                       
	END                          
	SELECT @iNewFieldId                     
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_ImportExportForms_spLnkForms_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_ImportExportForms_spLnkForms_Futures]
GO

/****** Object:  StoredProcedure [dbo].[Pr_ImportExportForms_spLnkForms_Futures]    Script Date: 7/29/2016 8:01:19 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Pr_ImportExportForms_spLnkForms_Futures]                                                    
 @ModuleId int,
 @FeatureId int,
 @UserId varchar(50),                                                    
 @ModuleName varchar(50)                                                    
as                                                    
begin
declare @newModuleId as int   
if exists(select * from mst_module where modulename=@modulename and moduleid=@moduleid and deleteflag=0)
begin
	if not exists(select featureid, moduleid from lnk_splformmodule where featureid = @featureid and moduleid = @moduleid)                                                                              
	begin
		insert into lnk_splformmodule (featureid, moduleid, userid, createdate) VALUES(@featureid,@moduleid, @userid, getdate())
	end 
end
else
begin
	--set @newModuleId = ident_current('Mst_Module')
	if exists(select moduleid from mst_module where modulename=@modulename)
	begin
		set @newModuleId = (select top 1 moduleid from mst_module where modulename=@modulename)
		if not exists(select featureid, moduleid from lnk_splformmodule where featureid = @featureid and moduleid = @newModuleId)                                                                              
		begin
			insert into lnk_splformmodule (featureid, moduleid, userid, createdate) VALUES(@featureid,@newModuleId, @userid, getdate())
		end
	end
end
                                                                         
End

GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FormBuilder_GetFieldLookupValues]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FormBuilder_GetFieldLookupValues]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[FormBuilder_GetFieldLookupValues]                                                                                 
(                                                                                  
	@FieldId int,
	@Predefined bit = 0  ,
	@SystemId int = 0                            
)                                                                                                   
AS                                                                                  
Begin                                 
 
	If(@Predefined = 0) Begin
			Select	Id											FieldID
					,FieldName
					,ControlId
					,dbo.GetLookupValues(CategoryId, BindTable,@SystemId)	FieldValue
					,0											Predefined
					,CategoryId									CodeId
					,BindTable
					,0											ModuleId
			From mst_CustomformField
			Where Id = @FieldId;
	End                            
    Else Begin
		Select	Id											FieldID
					,PDFName FieldName
					,ControlId
					,dbo.GetLookupValues(CategoryId, BindTable,@SystemId)	FieldValue
					,0											Predefined
					,CategoryId									CodeId
					,BindTable
					,											ModuleId
			From Mst_PreDefinedFields
			Where Id = @FieldId;
	End             
End	   

GO
/****** Object:  StoredProcedure [dbo].[pr_FormBuilder_GetModuleIdentifier_Constella]    Script Date: 03/17/2016 08:15:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[pr_FormBuilder_GetModuleIdentifier_Constella]              
 as               
BEGIN
	Select	a.ModuleId,
			a.ModuleName,
			DisplayName,
			CanEnroll =Case a.CanEnroll When 1 Then 'Yes' Else 'No' End,
			dbo.fn_GetIdentifiers_Futures(a.ModuleId) [PatientIdentifier],			
			(Case a.Status
				When 2 Then 'Published'
				When 1 Then 'Un-Published'
			End) [Status],
			a.UpdateFlag,
			isnull(a.Identifier, 0) [Identifier],
			a.PharmacyFlag
	From mst_Module a
	Where a.DeleteFlag = 0
	Order By a.ModuleName

End
GO

/****** Object:  StoredProcedure [dbo].[pr_FormBuilder_GetFormModuleLinkIdentifier_Futures]    Script Date: 12/17/2014 06:59:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_FormBuilder_GetFormModuleLinkIdentifier_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_FormBuilder_GetFormModuleLinkIdentifier_Futures]
GO
/****** Object:  StoredProcedure [dbo].[pr_FormBuilder_GetFormModuleLinkIdentifier_Futures]    Script Date: 12/17/2014 06:59:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  StoredProcedure [dbo].[pr_FormBuilder_GetFormModuleLinkIdentifier_Futures]    Script Date: 03/17/2016 08:15:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[pr_FormBuilder_GetFormModuleLinkIdentifier_Futures]           
(                                                    
 @ModuleID int                                                                                               
)            
              
 as                 
BEGIN
	Select	ModuleID,
			ModuleName,
			a.DisplayName,
			a.CanEnroll
	From dbo.mst_module a
	Where (DeleteFlag = 0
	Or DeleteFlag Is Null)
	And Status = 2
	And a.ModuleId > 2
	Order By a.ModuleName;

	If (@ModuleId = 0)
	Begin
		Select	FeatureID,
				FeatureName,
				b.ReferenceID,
				'False' [Selected]
		From dbo.mst_feature b
		Where FeatureID In (3, 4, 5, 71)
		And DeleteFlag = 0
		Order By b.FeatureName
	End 
	Else Begin
		Select	tbl1.FeatureID,
			tbl1.FeatureName,
			Selected =
				Case Isnull(tbl2.ModuleID, 0)
					When 0 Then 'False'
					Else 'True' End
		From dbo.mst_feature tbl1
		Left Outer Join
			dbo.lnk_SplFormModule tbl2 On tbl1.FeatureID = tbl2.FeatureId
			And tbl2.ModuleID = @ModuleID
		Where (tbl1.DeleteFlag = 0
		Or tbl1.DeleteFlag Is Null)
		And tbl1.FeatureID In (3, 4, 5, 71)
		Order By tbl1.FeatureName;
	End

End
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_FormBuilder_SaveUpdateModuleIdentification_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_FormBuilder_SaveUpdateModuleIdentification_Constella]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_FormBuilder_SaveUpdateModuleIdentification_Constella]                                                
(                                                
@ModuleId int,                       
@FieldId int,                           
@FieldName varchar(200),                       
@FieldType int,            
@Identifierchecked varchar(10), 
@RequiredFlag bit=0,                         
@UserId int,        
@Label varchar(200)=null,
@autopopulatenumber int=null                   
                                          
)                                                
AS                       
Begin
                   
                  
declare @sql_query varchar(max), @LastInsertId int, @OldName varchar(500);

Set @OldName = @FieldName

If Exists (Select Id From mst_PatientIdentifier	Where ID = @FieldID) Begin
	Select @OldName = FieldName	From Mst_PatientIdentifier	Where Id = @FieldId
	Update [mst_PatientIdentifier] Set
		FieldName = @FieldName,
		Label = @Label,
		UserID = @UserID,
		UpdateDate = Getdate()
	Where ID = @FieldID

	If @Identifierchecked = 'True' Begin
		Insert Into [lnk_PatientModuleIdentifier] (
				ModuleID
			,	FieldID
			,	UserID
			,	CreateDate
			,	DeleteFlag
			,	RequiredFlag)
		Values (
				@ModuleID
			,	@FieldID
			,	@UserID
			,	getdate()
			,	0
			,	@RequiredFlag)
	End
End 
Else Begin
	Insert Into [mst_PatientIdentifier] (
			FieldName
		,	FieldType
		,	UserId
		,	CreateDate
		,	UpdateFlag
		,	Label
		,	autopopulatenumber)
	Values (
			@FieldName
		,	@FieldType
		,	@UserID
		,	getdate()
		,	0
		,	@Label
		,	@autopopulatenumber);
	Select @LastInsertId = scope_identity();

	Insert Into [lnk_PatientModuleIdentifier] (
			ModuleID
		,	FieldID
		,	UserID
		,	CreateDate
		,	DeleteFlag
		,	RequiredFlag)
	Values (
			@ModuleID
		,	@LastInsertId
		,	@UserID
		,	getdate()
		,	0
		,	@RequiredFlag)
End


	If Exists (Select name	From syscolumns	Where name = @OldName	And Object_name(id) = 'mst_patient') 
	Begin
		Set @OldName = 'Mst_Patient.' + @OldName
		Exec sp_rename	
			@objname = @OldName,
			@newname = @FieldName,
			@objtype = 'COLUMN'
	End 
	Else Begin                     
		Set @sql_query = ' ALTER TABLE mst_patient ADD [' + @FieldName + '] varchar(50)'
		Exec (@sql_query)                     
	End
End
GO




/****** Object:  StoredProcedure [dbo].[Pr_FormBuilder_FetchMaxValue_Futures]    Script Date: 12/11/2014 16:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_FormBuilder_FetchMaxValue_Futures]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- Pr_PMTCT_FetchMaxValue_Futures  
--To Fetch Maximum id from specified table    
--Pr_PMTCT_FetchMaxValue_Futures ''Mst_Feature''    
--select * from mst_feature    
CREATE procedure [dbo].[Pr_FormBuilder_FetchMaxValue_Futures]          
 @strTableName varchar(100)=null    
as           
Begin        
if(@strTableName=''mst_feature'')    
begin    
 --select max(featureId) from mst_feature    
 select ident_current(''mst_feature'')     
end    
else if (@strTableName=''mst_section'')    
begin    
 --select max(sectionId) from mst_section    
 select ident_current(''mst_section'')     
end   
else if (@strTableName=''Mst_FormBuilderTab'')    
begin    
 --select max(sectionId) from mst_section    
 select ident_current(''Mst_FormBuilderTab'')     
end   
End
' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_FormBuilder_GetPublishedModuleList_Constella]    Script Date: 03/17/2016 08:23:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_FormBuilder_GetPublishedModuleList_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_FormBuilder_GetPublishedModuleList_Constella]
GO

/****** Object:  StoredProcedure [dbo].[pr_FormBuilder_GetPublishedModuleList_Constella]    Script Date: 03/17/2016 08:23:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[pr_FormBuilder_GetPublishedModuleList_Constella]              
 as               
BEGIN              
select  a.ModuleId,a.ModuleName,a.CanEnroll, a.DisplayName from  mst_Module a            
--where a.DeleteFlag = 0 and a.status=2 and a.ModuleId!=2 order by a.ModuleName 
where a.DeleteFlag = 0 and a.status=2  order by a.ModuleName                   
                    
End

GO
/****** Object:  StoredProcedure [dbo].[pr_FormBuilder_SaveUpdateModule_Constella]    Script Date: 03/17/2016 08:21:03 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_FormBuilder_SaveUpdateModule_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_FormBuilder_SaveUpdateModule_Constella]
GO

/****** Object:  StoredProcedure [dbo].[pr_FormBuilder_SaveUpdateModule_Constella]    Script Date: 03/17/2016 08:21:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[pr_FormBuilder_SaveUpdateModule_Constella]                                                          
(                                                          
 @ModuleId int,                                                          
 @ModuleName varchar(50),   
 @DisplayName varchar(50) = null,  
 @CanEnroll bit =	1,                                
 @Status int,                                 
 @DeleteFlag int,                                                                            
 @UserID int ,  
 @PharmacyFlag int=0   
                                                         
)                                                          
AS                      
                                                                            
begin                            
declare @VisitType int                  
declare @VisitTypeID int                
declare @OldModuleName varchar(50)    
declare @ModId int                                           
if exists(select ModuleName from mst_module where ModuleID = @ModuleID and DeleteFlag=0 )  begin          
    Select 
		@OldModuleName = ModuleName 
	From mst_module Where ModuleID = @ModuleID and DeleteFlag=0  
	        
      If exists(select VisitTypeId from Mst_VisitType where VisitName = @OldModuleName+' - Enrollment')   Begin          
         select @VisitType = VisitTypeId from Mst_VisitType where VisitName = @OldModuleName+' - Enrollment'          
         update mst_VisitType set VisitName = @ModuleName +' - Enrollment' where visittypeid = @VisitType     
       End          
      Else  if(@ModuleId>2)Begin         
            
           Set Identity_Insert Mst_VisitType on    
			Select @VisitType = isnull(max(VisitTypeId), 100) + 1
			From mst_VisitType
			Where VisitTypeId > 100        
			Insert Into mst_VisitType (
				VisitTypeID,
				VisitName,
				DeleteFlag,
				UserID,
				CreateDate,
				SystemId)
			Values (
				@VisitType,
				@ModuleName + ' - Enrollment',
				0,
				@UserID,
				getdate(),
				0 )          
			Set Identity_Insert Mst_VisitType off    
          -- set @VisitType = ident_current('Mst_VisitType')           
                  
       End           
    If Exists(select ModuleId from mst_module where ModuleID = @ModuleID and DeleteFlag=0) Begin   		                               
       Delete from lnk_PatientModuleIdentifier where ModuleID = @ModuleID   ;                       
       Update [mst_module] Set 
			ModuleName=@ModuleName, 
			UserID=@UserID,
			DisplayName = Isnull(@DisplayName,@ModuleName),
			CanEnroll= Isnull(@CanEnroll,1),
			UpdateDate=getdate(),
			PharmacyFlag=@PharmacyFlag 
		where ModuleID = @ModuleID        
     end    
  end                                             
else begin    
  set identity_insert Mst_Module on                        
  select @ModId = isnull(max(ModuleId),0)+1 from mst_Module where ModuleId<200                   
	Insert Into mst_module (
		ModuleID,
		ModuleName,
		DisplayName,
		CanEnroll,
		DeleteFlag,
		UserId,
		CreateDate,
		Status,
		UpdateFlag,
		PharmacyFlag)
	Values (
		@ModId,
		@ModuleName,
		isnull(@DisplayName, @ModuleName),
		@CanEnroll,
		@DeleteFlag,
		@UserID,
		getdate(),
		@Status,
		0,
		@PharmacyFlag )          
		Set @ModuleId = @ModId
	set identity_insert Mst_Module off                        
  select @VisitType = isnull(max(VisitTypeId),100)+1 from mst_VisitType where VisitTypeId>100                  
  set identity_insert Mst_VisitType on                   
	Insert Into Mst_VisitType (
		VisitTypeId,
		VisitName,
		DeleteFlag,
		UserID,
		CreateDate,
		SystemId)
	Values (
		@VisitType,
		@ModuleName + ' - Enrollment',
		0,
		@UserId,
		getdate(),
		0 )                 
  set identity_insert Mst_VisitType off                   
  end    
                                
  if @ModuleId = 0                            
  begin                            
     select @ModId                               
  end                            
    else                            
  begin                            
     select @ModuleId                            
  end                      
    Return (@ModuleId)                                            
end


GO


/****** Object:  StoredProcedure [dbo].[Pr_ImportExportForms_ImportModules_Futures]    Script Date: 03/17/2016 08:25:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_ImportExportForms_ImportModules_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_ImportExportForms_ImportModules_Futures]
GO

/****** Object:  StoredProcedure [dbo].[Pr_ImportExportForms_ImportModules_Futures]    Script Date: 8/1/2016 12:20:49 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE proc [dbo].[Pr_ImportExportForms_ImportModules_Futures]  (          
 @ModuleId int, 
 @ModuleName varchar(50),                                                                                         
 @UserID int    )
As            
BEGIN      
	DECLARE @iModuleId as int, @CanEnroll bit, @DisplayName varchar(50),@VisitType int;
     
	 Set @CanEnroll = 1;
	        
	IF EXISTS (SELECT * FROM mst_module WHERE ModuleName = @ModuleName)  BEGIN  
		Update mst_module Set DeleteFlag = 0 Where ModuleName=@ModuleName;   
		Select	@iModuleId = ModuleId
			,	@CanEnroll = CanEnroll
			,	@DisplayName = DisplayName
		From mst_module
		Where ModuleName = @ModuleName 
	END                             
	ELSE BEGIN    
		SET IDENTITY_INSERT Mst_Module ON 
		IF Exists(Select 1 From mst_Module Where ModuleID = @ModuleId)   Begin                    
			SELECT @iModuleId = isnull(max(ModuleId),0)+1 FROM mst_Module WHERE ModuleId<200    
		END
		ELSE BEGIN
			SELECT @iModuleId =@ModuleId;
		END               
		Insert Into mst_module (
				ModuleId
			,	ModuleName
			,	DisplayName
			,	CanEnroll
			,	DeleteFlag
			,	UserId
			,	CreateDate
			,	Status
			,	UpdateFlag
			,	PharmacyFlag)
		Values (
				@iModuleId
			,	@ModuleName
			,	isnull(@DisplayName, @ModuleName)
			,	@CanEnroll
			,	0
			,	@UserID
			,	getdate()
			,	2
			,	0
			,	0)
		SET IDENTITY_INSERT Mst_Module OFF                        
		SELECT @VisitType = isnull(max(VisitTypeId),100)+1 FROM mst_VisitType WHERE VisitTypeId>100                  
		SET IDENTITY_INSERT Mst_VisitType on                   
		Insert Into Mst_VisitType (
				VisitTypeId
			,	VisitName
			,	DeleteFlag
			,	UserID
			,	CreateDate
			,	SystemId)
		Values (
				@VisitType
			,	@ModuleName + ' - Enrollment'
			,	0
			,	@UserId
			,	getdate()
			,	0)   
		SET IDENTITY_INSERT Mst_VisitType OFF                   
	END    
	Select @iModuleId ModuleId
    Return(@IModuleId);
End
  

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServiceArea_GetPatientReports]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ServiceArea_GetPatientReports]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Joseph
-- Create date: 2016 07 18
-- Description:	Get Forms for a service area
-- =============================================
Create PROCEDURE [dbo].[ServiceArea_GetPatientReports]
	-- Add the parameters for the stored procedure here
	@ModuleId int ,
	@LocationId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
Select	F.FeatureId
	,	F.FeatureName
	,	F.ReferenceId
	,	F.ModuleId
	,	F.FeatureTypeId
	,	D.Code		As FeatureTypeName
	,	D.CodeID	As FeatureCodeId
From mst_Feature As F
Inner Join mst_Decode As D On F.FeatureTypeId = D.ID
And D.Code = 'PATIENT_REPORT'
Where (F.ModuleId = @ModuleId Or F.ModuleId = 0)
	And (F.DeleteFlag = 0)
	And (F.ReportFlag = 1)
Union All
Select	F.FeatureId
	,	F.FeatureName
	,	F.ReferenceId
	,	F.ModuleId
	,	F.FeatureTypeId
	,	D.Code		As FeatureTypeName
	,	D.CodeID	As FeatureCodeId
From mst_Feature As F
Inner Join mst_Decode As D On F.FeatureTypeId = D.ID
And D.Code = 'MODULE_REPORT'
Where (F.ModuleId = @ModuleId  )
	And (F.DeleteFlag = 0)
	And (F.ReportFlag = 1)
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Form_GetBusinessRule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Form_GetBusinessRule]
GO

/****** Object:  StoredProcedure [dbo].[Form_GetBusinessRule]    Script Date: 8/2/2016 5:58:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 2016-07-15
-- Description:	Get the form business rule
-- =============================================
CREATE PROCEDURE [dbo].[Form_GetBusinessRule] 
	-- Add the parameters for the stored procedure here
	@FormId int = null,
	@FeatureId int = null,
	@ModuleId int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	If(@ModuleId Is Null) Begin
		Select	Id
			,	BusRuleId
			,	BusRuleName
			,	BusRuleDeleteFlag
			,	BusRuleReferenceId
			,	MinValue
			,	MaxValue
			,	SetType	As RuleSet
			,	FormId
			,	FormReferenceId
			,	FeatureId
			,	MultiVisit
			,	Custom
			,	FormTypeReferenceId
			,	FeatureTypeId	
			,	FormName	
		From FormBusinessRuleView
		Where (FormId = @FormId Or @FormId Is Null)
			And (FeatureId = @FeatureId Or @FeatureId Is Null);
	End
	Else Begin
		Select	Id
			,	BusRuleId
			,	BusRuleName
			,	BusRuleDeleteFlag
			,	BusRuleReferenceId
			,	MinValue
			,	MaxValue
			,	SetType	As RuleSet
			,	F.FormId
			,	FormReferenceId
			,	F.FeatureId
			,	F.MultiVisit
			,	F.Custom
			,	FormTypeReferenceId
			,	FeatureTypeId	
			,	F.FormName	
		From FormBusinessRuleView F
		--Inner Join ServiceAreaFormView S On S.FeatureId = F.FormId
		Where (F.FormId= @FormId Or @FormId Is Null)
		And  (F.FeatureId = @FeatureId Or @FeatureId Is Null)
		--And S.ModuleId = @ModuleId
	End
END


GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServiceArea_GetBusinessRule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ServiceArea_GetBusinessRule]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 2016-07-15
-- Description:	Get the service area business rule
-- =============================================
CREATE PROCEDURE [dbo].[ServiceArea_GetBusinessRule] 
	-- Add the parameters for the stored procedure here
	@ModuleId int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		Select	Id
			,	BusRuleId
			,	BusRuleName
			,	BusRuleDeleteFlag
			,	BusRuleReferenceId
			,	Value MinValue
			,	Value1 MaxValue
			,	SetType RuleSet
			,	ModuleId
			,	ModuleName
			,	DisplayName
			,	CanEnroll
		From ServiceAreaBusinessRuleView
		Where (ModuleId = @ModuleId Or @ModuleId Is Null)
END

GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FormBuilder_SaveModuleBusinessRules]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FormBuilder_SaveModuleBusinessRules]
GO

/****** Object:  StoredProcedure [dbo].[FormBuilder_SaveModuleBusinessRules]    Script Date: 8/1/2016 12:29:51 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[FormBuilder_SaveModuleBusinessRules]    
(        
@ModuleId int,        
@BusRuleId int,        
@value varchar(50) =null,        
@value1 varchar(50) =null,        
@setType int=null,        
@UserId int=null       
)        
As Begin 
	Insert Into lnk_ServiceBusinessRule (
			ModuleId
		,	BusRuleId
		,	Value
		,	Value1
		,	SetType
		,	CreateDate
		,	UserId)
	Values (
			@ModuleId
		,	@BusRuleid
		,	nullif(@value,'')
		,	nullif(@value1,'')
		,	@setType
		,	getdate()
		,	@userid)

End

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FormBuilder_DeleteModuleBusinessRules]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FormBuilder_DeleteModuleBusinessRules]
GO

/****** Object:  StoredProcedure [dbo].[FormBuilder_DeleteModuleBusinessRules]    Script Date: 8/1/2016 12:29:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create procedure [dbo].[FormBuilder_DeleteModuleBusinessRules]    
(        
	@ModuleId int       
)        
as        
begin

Delete From lnk_ServiceBusinessRule
Where moduleid = @ModuleId

End
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_ImportExportForms_ImportRegistrationConditionalField_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_ImportExportForms_ImportRegistrationConditionalField_Futures]
GO

/****** Object:  StoredProcedure [dbo].[Pr_ImportExportForms_ImportRegistrationConditionalField_Futures]    Script Date: 8/1/2016 12:26:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Pr_ImportExportForms_ImportRegistrationConditionalField_Futures]   (                       
	@FeatureId int,                                        
	@SectionId int,                        
	@FieldId int,                      
	@FieldName varchar(100),                                        
	@ConFieldId int,                                  
	@ConFieldName varchar(100),                                      
	@ConFieldLabel varchar(100),                      
	@ControlId int=null,                                   
	@ConControlId int=null,                                     
	@ConSelectListVal xml = null,                                 
	@ConBusRuleIdValAll varchar(max),                                
	@ConSeq int,                                       
	@UserId int,                                        
	@Predefined int,                        
	@ConPredefined int,                      
	@ConSectionId int,                      
	@ModdecodeName varchar(100),                  
	@SystemId int                       
 )                                      
as                                        
Begin---(-1)                                  
	 Declare @iNewFieldId int
                       
	 Declare @iConSectionId int
                       
	 Declare @iConCategoryId int
                      
	 Declare @iModcodeId int            
	Declare @iBindTable varchar(100), @returnValue int;
                       
                                   
	If(@Predefined=0) Begin----0                   
		If(@ControlId=6) Begin
			Select @iConSectionId = Id From mst_YesNo Where Name = @ModdecodeName 
		End 
		Else Begin
			Select @iModcodeId = CodeId	From mst_Modcode	Where name = @FieldName
			If (@iModcodeId != '') Begin
				Select @iConSectionId = Id From mst_Moddecode Where name = @ModdecodeName	And CodeId = @iModcodeId
			End 
			Else Begin
				Set @iConSectionId = @ConSectionId
			End
		End
		If (@ConPredefined = 0) Begin----01  --no change in select list value in case custom field already exist.
			If Exists (Select * From mst_CustomformField Where FieldName = '' + @ConFieldName + ''	And deleteflag = 0	And PatientRegistration = 1) Begin----1                       
				Select @iNewFieldId = Id
				From mst_customFormField
				Where fieldName = '' + @ConFieldName + ''
					And deleteflag = 0
					And PatientRegistration = 1
				If (@ControlId = 4 Or @ControlId = 9) Begin---3    
					--If field already exists on form , irrespective of section. then dont insert in lnk_forms.                                
					If Not Exists (Select * From lnk_PatientRegconditionalfields Where Sectionid = @iConSectionId	And ConFieldId = @FieldId	And FieldId = @iNewFieldId	And Predefined = @Predefined) Begin---4                                 
						Insert Into lnk_PatientRegconditionalfields (
								ConfieldId
							,	SectionId
							,	FieldId
							,	FieldLabel
							,	UserId
							,	CreateDate
							,	Predefined
							,	Seq
							,	Conpredefine)
						Values (
								@FieldId
							,	@iConSectionId
							,	@iNewFieldId
							,	@ConFieldLabel
							,	@UserId
							,	getdate()
							,	@ConPredefined
							,	@ConSeq
							,	@Predefined)

					End----4                      
				End---3                      
				Else If (@ControlId = 6) Begin----5   
					Set @iConSectionId = @ConSectionId
					--If field already exists on form , irrespective of section. then dont insert in lnk_forms.                 
					If Not Exists (Select * From lnk_PatientRegconditionalfields Where Sectionid = @iConSectionId	And ConFieldId = @FieldId	And FieldId = @iNewFieldId	And Predefined = @Predefined) Begin---6                               
						Insert Into lnk_PatientRegconditionalfields (
								ConfieldId
							,	SectionId
							,	FieldId
							,	FieldLabel
							,	UserId
							,	CreateDate
							,	Predefined
							,	Seq
							,	Conpredefine)
						Values (
								@FieldId
							,	@iConSectionId
							,	@iNewFieldId
							,	@ConFieldLabel
							,	@UserId
							,	getdate()
							,	@ConPredefined
							,	@ConSeq
							,	@Predefined)
					End---6                      
				End---5 
			End ---1                               
			Else Begin
				Exec @returnValue = Pr_PMTCT_SaveUpdateRegistrationConditionalCustomFields_Futures	
							@ConFieldId = @FieldId
						,	@FieldName = @ConFieldName
						,	@ControlId = @ConControlId
						,	@DeleteFlag = 0
						,	@UserId = @UserId
						,	@Flag = 0
						,	@SelectList = @ConSelectListVal
						,	@Predefined = @ConPredefined
						,	@ConFieldLabel = @ConFieldLabel
						,	@ConSeq = @ConSeq
						,	@ConPredefined = 0
						,	@SystemId = @SystemId
				Set @iNewFieldId = @returnValue;
				If Not Exists (Select * From lnk_PatientRegconditionalfields Where Sectionid = @iConSectionId	And ConFieldId = @FieldId	And FieldId = @iNewFieldId) Begin                                      
					Insert Into lnk_PatientRegconditionalfields (
							ConfieldId
						,	SectionId
						,	FieldId
						,	FieldLabel
						,	UserId
						,	CreateDate
						,	Predefined
						,	Seq
						,	Conpredefine)
					Values (
							@FieldId
						,	@iConSectionId
						,	@iNewFieldId
						,	@ConFieldLabel
						,	@UserId
						,	getdate()
						,	@ConPredefined
						,	@ConSeq
						,	@Predefined)
				End                    
			End
		End     
		Else Begin
			Set @iNewFieldId = @ConFieldId
			If (@ControlID = 9 Or @ControlId = 4) Begin
				--If field already exists on form , irrespective of section. then dont insert in lnk_forms.                                    
				If Not Exists (Select * From lnk_PatientRegconditionalfields	Where Sectionid = @iConSectionId And ConFieldId = @FieldId	And FieldId = @iNewFieldId) Begin
					Insert Into lnk_PatientRegconditionalfields (
							ConfieldId
						,	SectionId
						,	FieldId
						,	FieldLabel
						,	UserId
						,	CreateDate
						,	Predefined
						,	Seq
						,	Conpredefine)
					Values (
							@FieldId
						,	@iConSectionId
						,	@iNewFieldId
						,	@ConFieldLabel
						,	@UserId
						,	getdate()
						,	@ConPredefined
						,	@ConSeq
						,	@Predefined)
				End
			End 
			Else If (@ControlId = 6) Begin
				Select @iConSectionId = Id From mst_YesNo Where name = @ModdecodeName
				If Not Exists ( Select * from lnk_PatientRegconditionalfields Where Sectionid = @iConSectionId	And ConFieldId = @FieldId And FieldId = @ConFieldId) Begin
					Insert Into lnk_PatientRegconditionalfields (
							ConfieldId
						,	SectionId
						,	FieldId
						,	FieldLabel
						,	UserId
						,	CreateDate
						,	Predefined
						,	Seq
						,	Conpredefine)
					Values (
							@FieldId
						,	@iConSectionId
						,	@iNewFieldId
						,	@ConFieldLabel
						,	@UserId
						,	getdate()
						,	@ConPredefined
						,	@ConSeq
						,	@Predefined)
				End
		End
	End
		Declare @tempPreBusRuleCount int
		Declare @tempPreBusRuleValCount int
		Declare @tempPreBusRuleId int
		Declare @tempPreBusRulevalue varchar(100)
		Declare @tempPreBusRuleSplitvalue varchar(100)
		Declare @tempPreBusRuleSplitvalue2 varchar(100)
		Declare @PreIndex int
		Declare @PreIndex2 int

		--business rules for the field                                
		--re-eneter all business rule again, and remove previously set business rule, applies to both predefined and non                                
		--predefined fields                                
		Delete From lnk_fieldsBusinessRule Where FieldId = @iNewFieldId And predefined = @ConPredefined

		Select @tempPreBusRuleCount = count(*) From dbo.fnParseDelimitedList(@ConBusRuleIdValAll, ',')
		Set @PreIndex = 1;
		While @PreIndex <= @tempPreBusRuleCount Begin--1                                                       
			Set @tempPreBusRulevalue = dbo.fnGetParmTilte(@PreIndex, @ConBusRuleIdValAll)
			--print '@tempPreBusRulevalue=' + @tempPreBusRulevalue                                
			Select @tempPreBusRuleValCount = count(*) From dbo.fnParseDelimitedList(@tempPreBusRulevalue, '-')
			Set @tempPreBusRuleId = dbo.fnGetParmTilteForHighphen(1, @tempPreBusRulevalue)
			Set @tempPreBusRuleSplitvalue = dbo.fnGetParmTilteForHighphen(2, replace(@tempPreBusRulevalue, 'Null', ''))
			Set @tempPreBusRuleSplitvalue2 = dbo.fnGetParmTilteForHighphen(3, replace(@tempPreBusRulevalue, 'Null', ''))
			Exec Pr_PMTCT_SaveBusinessRules_Futures	
				@iNewFieldId
			,	@tempPreBusRuleId
			,	@tempPreBusRuleSplitvalue
			,	@UserId
			,	@Predefined
			,	@tempPreBusRuleSplitvalue2
			Set @PreIndex = @PreIndex + 1;
		End--1                                                   
	End--0  
	Else Begin--0                       
		If (@ConPredefined = 0) Begin---01     
			If (@ControlId = 6) Begin
				Select @iConSectionId = Id From mst_YesNo Where name = @ModdecodeName
			End 
			Else Begin
				If (@Predefined = '1') Begin
					Set @iConSectionId = @ConSectionId
				End 
				Else Begin
					Select @iModcodeId = CodeId From mst_Modcode Where name = @FieldName

					Select @iConSectionId = Id	From mst_Moddecode	Where name = @ModdecodeName	And CodeId = @iModcodeId
				End
			End
			If Exists (Select * From mst_CustomformField Where FieldName = '' + @ConFieldName + ''	And deleteflag = 0	And PatientRegistration = 1) Begin----1   --use same fieldId   
				Select @iNewFieldId = Id From mst_customFormField Where fieldName = '' + @ConFieldName + ''	And deleteflag = 0	And PatientRegistration = 1
				If (@ControlId = 4 Or @ControlId = 9) Begin---3 --If field already exists on form , irrespective of section. then dont insert in lnk_forms.  
					If Not Exists (Select * From lnk_PatientRegconditionalfields Where Sectionid = @iConSectionId And ConFieldId = @FieldId And FieldId = @iNewFieldId	And Predefined = @ConPredefined) Begin---4                                 
						Insert Into lnk_PatientRegconditionalfields (
								ConfieldId
							,	SectionId
							,	FieldId
							,	FieldLabel
							,	UserId
							,	CreateDate
							,	Predefined
							,	Seq
							,	Conpredefine)
						Values (
								@FieldId
							,	@iConSectionId
							,	@iNewFieldId
							,	@ConFieldLabel
							,	@UserId
							,	getdate()
							,	@ConPredefined
							,	@ConSeq
							,	@Predefined)

					End----4                      
				End---3                      
			Else If (@ControlId = 6) Begin----5                      
				Set @iConSectionId = @ConSectionId
				--If field already exists on form , irrespective of section. then dont insert in lnk_forms.                                
				If Not Exists (	Select * From lnk_PatientRegconditionalfields	Where Sectionid = @iConSectionId And ConFieldId = @FieldId	And FieldId = @iNewFieldId	And Predefined = @Predefined) Begin---6                                 
                   Insert Into lnk_PatientRegconditionalfields (
							ConfieldId
						,	SectionId
						,	FieldId
						,	FieldLabel
						,	UserId
						,	CreateDate
						,	Predefined
						,	Seq
						,	Conpredefine)
					Values (
							@FieldId
						,	@iConSectionId
						,	@iNewFieldId
						,	@ConFieldLabel
						,	@UserId
						,	getdate()
						,	@ConPredefined
						,	@ConSeq
						,	@Predefined)
				End---6                      
			End---5    
		End ---1                               
		Else Begin---1    -- insert field and corresponding select list value --                  
			Exec @returnValue = Pr_PMTCT_SaveUpdateRegistrationConditionalCustomFields_Futures	
					  @ConFieldId	=	@FieldId
					, @FieldName	=	@ConFieldName
					, @ControlId	=	@ConControlId
					, @DeleteFlag   =	0
					, @UserId       =	@UserId
					, @Flag			=	0
					, @SelectList   =	@ConSelectListVal
					, @Predefined   =	@ConPredefined
					, @ConFieldLabel=	@ConFieldLabel
					, @ConSeq 		=  @ConSeq
					, @ConPredefined=	0
					, @SystemId 	= @SystemId
			Set @iNewFieldId = @returnValue;
                 
			If Not Exists (Select * From lnk_PatientRegconditionalfields Where Sectionid = @iConSectionId	And ConFieldId = @FieldId	And FieldId = @iNewFieldId) Begin--2   
				Insert Into lnk_PatientRegconditionalfields (
						ConfieldId
					,	SectionId
					,	FieldId
					,	FieldLabel
					,	UserId
					,	CreateDate
					,	Predefined
					,	Seq
					,	Conpredefine)
				Values (
						@FieldId
					,	@iConSectionId
					,	@iNewFieldId
					,	@ConFieldLabel
					,	@UserId
					,	getdate()
					,	@ConPredefined
					,	@ConSeq
					,	@Predefined)
			End--2    
		End--1                      

	End----01                      
	Else Begin--01                      
		Set @iNewFieldId = @ConFieldId
		If (@ControlID = 9 Or @ControlId = 4) Begin---1       
			Set @iConSectionId = @ConSectionId
				--If field already exists on form , irrespective of section. then dont insert in lnk_forms.                                    
			If Not Exists (	Select * From lnk_PatientRegconditionalfields	Where Sectionid = @iConSectionId And ConFieldId = @FieldId And FieldId = @iNewFieldId	) Begin---2 
				Insert Into lnk_PatientRegconditionalfields (
						ConfieldId
					,	SectionId
					,	FieldId
					,	FieldLabel
					,	UserId
					,	CreateDate
					,	Predefined
					,	Seq
					,	Conpredefine)
				Values (
						@FieldId
					,	@iConSectionId
					,	@iNewFieldId
					,	@ConFieldLabel
					,	@UserId
					,	getdate()
					,	@ConPredefined
					,	@ConSeq
					,	@Predefined)
			End---2                      
		End---1                      
		Else If (@ControlId = 6) Begin----3                      
			Select @iConSectionId = Id From mst_YesNo Where name = @ModdecodeName
			If Not Exists (Select * From lnk_PatientRegconditionalfields Where Sectionid = @iConSectionId	And ConFieldId = @FieldId	And FieldId = @ConFieldId) Begin----4			
				Insert Into lnk_PatientRegconditionalfields (
						ConfieldId
					,	SectionId
					,	FieldId
					,	FieldLabel
					,	UserId
					,	CreateDate
					,	Predefined
					,	Seq
					,	Conpredefine)
				Values (
						@FieldId
					,	@iConSectionId
					,	@iNewFieldId
					,	@ConFieldLabel
					,	@UserId
					,	getdate()
					,	@ConPredefined
					,	@ConSeq
					,	@Predefined)
			End----4                      
		End---3                              
	End----01                              

                               
		Declare @tempBusRuleCount int
		Declare @tempBusRuleValCount int
		Declare @tempBusRuleId int
		Declare @tempBusRulevalue varchar(100)
		Declare @tempBusRuleSplitvalue varchar(100)
		Declare @tempBusRuleSplitvalue2 varchar(100)
		Declare @Index int
		Declare @Index2 int

		--business rules for the field                                
		--re-eneter all business rule again, and remove previously set business rule, applies to both predefined and non                                
		--predefined fields                                
		Delete From lnk_fieldsBusinessRule	Where FieldId = @iNewFieldId	And predefined = @ConPredefined

		Select @tempBusRuleCount = count(*) From dbo.fnParseDelimitedList(@ConBusRuleIdValAll, ',')
		Set @Index = 1;
		While @Index <= @tempBusRuleCount Begin--1                                             
			Set @tempBusRulevalue = dbo.fnGetParmTilte(@Index, @ConBusRuleIdValAll)	                               
			Select @tempBusRuleValCount = count(*)	From dbo.fnParseDelimitedList(@tempBusRulevalue, '-')
			Set @tempBusRuleId = dbo.fnGetParmTilteForHighphen(1, @tempBusRulevalue)
			Set @tempBusRuleSplitvalue = dbo.fnGetParmTilteForHighphen(2, replace(@tempBusRulevalue, 'Null', ''))
			Set @tempBusRuleSplitvalue2 = dbo.fnGetParmTilteForHighphen(3, replace(@tempBusRulevalue, 'Null', ''))
			Exec Pr_PMTCT_SaveBusinessRules_Futures	
					@iNewFieldId
				,	@tempPreBusRuleId
				,	@tempBusRuleSplitvalue
				,	@UserId
				,	@Predefined
				,	@tempBusRuleSplitvalue2
			Set @Index = @Index + 1;
		End--1      
	End--0                              
	Select @iNewFieldId;

	Return (@iNewFieldId);
End
GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_ImportExportForms_ImportTabs_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_ImportExportForms_ImportTabs_Futures]
GO

/****** Object:  StoredProcedure [dbo].[Pr_ImportExportForms_ImportTabs_Futures]    Script Date: 8/1/2016 12:25:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Pr_ImportExportForms_ImportTabs_Futures]        
@TabId int,        
@TabName varchar(50),        
@Seq int,
@DeleteFlag int,        
@UserId int,        
@FeatureId int    
as        
Begin        
	Declare @iTabId as int, @iDeleteFlag int        
	If Exists(Select * From Mst_FormBuilderTab Where TabName=@TabName And featureid=@FeatureId) Begin  
	--select 'a'        
		Select @iTabId = TabId
			, @iDeleteFlag =  DeleteFlag
		From Mst_FormBuilderTab
		Where TabName = @TabName
		And featureid = @FeatureId;
		Update Mst_FormBuilderTab Set DeleteFlag = @DeleteFlag Where TabName = @TabName	And featureid = @FeatureId
	End          
	Else Begin          
		--select 'B'        
		Insert Into Mst_FormBuilderTab (
				TabName
			,	Seq
			,	DeleteFlag
			,	UserId
			,	CreateDate
			,	FeatureId)
		Values (
				@TabName
			,	@Seq
			,	@DeleteFlag
			,	@UserId
			,	getdate()
			,	@FeatureId)      
         
		Set @iTabId = scope_identity();          
	End           
	Select @iTabId   ;
	Return(@iTabId); 
End

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_ImportExportForms_ImportSection_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_ImportExportForms_ImportSection_Futures]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[Pr_ImportExportForms_ImportSection_Futures]   (   
@SectionId int,      
@SectionName varchar(50),  
@SectionInfo varchar(255) = null,    
@Seq int,      
@CustomFlag int,      
@DeleteFlag int,      
@UserId int,      
@FeatureId int,
@IsGridView int=NULL   )   
as      
begin
      
Declare @iSectionId as int, @iDeleteFlag int;
      
	if exists (Select * From mst_Section Where SectionName = @SectionName	And FeatureId = @FeatureId	) Begin      
		Select	@iSectionId = SectionId
			,	@iDeleteFlag = DeleteFlag
		From mst_Section
		Where SectionName = @SectionName
			And FeatureId = @FeatureId		
		If(@iDeleteFlag != @DeleteFlag) Begin
			Update mst_Section Set DeleteFlag = @iDeleteFlag Where SectionID=@iSectionId;
		End
	End 
	Else Begin     
		Insert Into mst_Section (
				SectionName
			,	SectionInfo
			,	Seq
			,	CustomFlag
			,	DeleteFlag
			,	UserId
			,	CreateDate
			,	FeatureId
			,	IsGridView)
		Values (
				@SectionName
			,	@SectionInfo
			,	@Seq
			,	@CustomFlag
			,	@DeleteFlag
			,	@UserId
			,	getdate()
			,	@FeatureId
			,	@IsGridView);

		Set @iSectionId = scope_identity();
	End
	Select @iSectionId
	Return (@iSectionId);
End

GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_ImportExportForms_ImportModulesIdentifier_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_ImportExportForms_ImportModulesIdentifier_Futures]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Pr_ImportExportForms_ImportModulesIdentifier_Futures] (                     
	@ModuleID int,                           
	@FieldID int,                               
	@FieldName varchar(200),                           
	@FieldType int,                
	@UserId int,  
	@Label varchar(200)=null
)             
as                      
begin                
	declare @iFieldId as int                     
	if exists (Select * From mst_PatientIdentifier Where FieldName ='' + @FieldName + '') Begin  
		Select @iFieldId=id From mst_PatientIdentifier Where FieldName ='' + @FieldName + ''       
		If Not Exists (Select FieldId From lnk_PatientModuleIdentifier Where FieldId ='' + @FieldID + '' And ModuleId='' + @ModuleID + '' And (DeleteFlag=0 Or Deleteflag Is Null)) Begin   
			Insert Into [lnk_PatientModuleIdentifier] (
					ModuleID
				,	FieldID
				,	UserID
				,	CreateDate
				,	DeleteFlag)
			Values (
					@ModuleID
				,	@FieldID
				,	@UserID
				,	getdate()
				,	0)     
		End      
	End              
	Else Begin 
		Exec pr_FormBuilder_SaveUpdateModuleIdentification_Constella	
					@ModuleId = @ModuleId
				,	@FieldId = 0
				,	@FieldName = @FieldName
				,	@FieldType = @FieldType
				,	@Identifierchecked = 'false'
				,	@UserId = @UserID
				,	@Label = @Label  
	End              
  Select 0        
              
End

GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_ImportExportForms_ImportConditionalField_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_ImportExportForms_ImportConditionalField_Futures]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Pr_ImportExportForms_ImportConditionalField_Futures]  (                          
	@FeatureId int,                                          
	@SectionId int,                          
	@FieldId int,                        
	@FieldName varchar(100),                                          
	@ConFieldId int,                                    
	@ConFieldName varchar(100),                                        
	@ConFieldLabel varchar(100),                        
	@ControlId int=null,                                     
	@ConControlId int=null,                                       
	@ConSelectListVal xml = null,                                  
	@ConBusRuleIdValAll varchar(max),                                  
	@ConSeq int,                                         
	@UserId int,                                          
	@Predefined int,                          
	@ConPredefined int,                        
	@ConSectionId int,                        
	@ModdecodeName varchar(100),                    
	@SystemId int                         
)                                        
AS                                          
BEGIN---(-1)                                    
	Declare @iNewFieldId int
                         
	Declare @iConSectionId int
                         
	Declare @iConCategoryId int
                        
	Declare @iModcodeId int
              
	Declare @iBindTable varchar(100)
     declare @RC int;                     
                                     
	IF(@Predefined=0) BEGIN----0                     
		IF(@ControlId=6)  BEGIN
			SELECT @iConSectionId = Id	FROM mst_YesNo	WHERE name = @ModdecodeName
		END 
		ELSE BEGIN
			SELECT @iModcodeId = CodeId	FROM mst_ModCode WHERE name = @FieldName
			IF (@iModcodeId != '') BEGIN
				SELECT @iConSectionId = Id	FROM mst_Moddecode	WHERE name = @ModdecodeName	AND CodeId = @iModcodeId
				IF (@iConSectionId = '' OR @iConSectionId IS NULL) BEGIN
					DECLARE @iMaxSRNo int
					SELECT @iMaxSRNo = max(id) + 1 FROM mst_Moddecode WHERE CodeId = @iModcodeId;
					INSERT INTO mst_Moddecode (
							Name
						,	CodeId
						,	SRNo
						,	DeleteFlag
						,	UserId
						,	CreateDate
						,	SystemId)
					VALUES (
							@ModdecodeName
						,	@iModcodeId
						,	@iMaxSRNo
						,	0
						,	@UserId
						,	getdate()
						,	0)
					SELECT @iConSectionId = scope_identity()
				END
			END 
			ELSE BEGIN
				SET @iConSectionId = @ConSectionId
			END
		END
		IF (@ConPredefined = 0) BEGIN----01                               
			--no change in select list value in case custom field already exist.                         
			IF EXISTS (SELECT * FROM mst_CustomformField WHERE FieldName = '' + @ConFieldName + ''	AND deleteflag = 0) BEGIN----1                         
				SELECT @iNewFieldId = Id FROM mst_CustomformField WHERE fieldName = '' + @ConFieldName + ''	AND deleteflag = 0
				IF (@ControlId = 4 OR @ControlId = 9) BEGIN---3      
					--IF field already exists on form , irrespective of section. then dont insert in lnk_forms.                                  
					IF NOT EXISTS (SELECT * FROM lnk_ConditionalFields WHERE Sectionid = @iConSectionId	AND ConFieldId = @FieldId AND FieldId = @iNewFieldId AND Predefined = @ConPredefined) BEGIN---4                                   
						INSERT INTO lnk_ConditionalFields (
								ConfieldId
							,	SectionId
							,	FieldId
							,	FieldLabel
							,	UserId
							,	CreateDate
							,	Predefined
							,	Seq
							,	Conpredefine)
						VALUES (
								@FieldId
							,	@iConSectionId
							,	@iNewFieldId
							,	@ConFieldLabel
							,	@UserId
							,	getdate()
							,	@ConPredefined
							,	@ConSeq
							,	@Predefined)
					END----4                        
				END---3                        
				ELSE IF (@ControlId = 6) BEGIN----5    
					SET @iConSectionId = @ConSectionId
					--IF field already exists on form , irrespective of section. then dont insert in lnk_forms.                   
					IF NOT EXISTS (SELECT * FROM lnk_ConditionalFields WHERE Sectionid = @iConSectionId	AND ConFieldId = @FieldId AND FieldId = @iNewFieldId AND Predefined = @ConPredefined) BEGIN---6                                   
						INSERT INTO lnk_ConditionalFields (
								ConfieldId
							,	SectionId
							,	FieldId
							,	FieldLabel
							,	UserId
							,	CreateDate
							,	Predefined
							,	Seq
							,	Conpredefine)
						VALUES (
								@FieldId
							,	@iConSectionId
							,	@iNewFieldId
							,	@ConFieldLabel
							,	@UserId
							,	getdate()
							,	@ConPredefined
							,	@ConSeq
							,	@Predefined)
					END---6                        
				END---5 
			END ---1                                 
			ELSE BEGIN			   
				EXEC @RC = Pr_PMTCT_SaveUpdateConditionalCustomFields_Futures	
						@ConFieldId		= @FieldId
					,	@FieldName		= @ConFieldName
					,	@ControlId		= @ConControlId
					,	@DeleteFlag		= 0
					,	@UserId			= @UserId
					,	@Flag			= 0
					,	@SelectList		= @ConSelectListVal
					,	@Predefined		= @ConPredefined
					,	@ConFieldLabel	= @ConFieldLabel
					,	@ConSeq			= @ConSeq
					,	@ConPredefined	= 0
					,	@SystemId		= @SystemId
				SELECT  @iNewFieldId = @RC;
				--SET @iNewFieldId = ident_current('mst_CustomFormField')   
				IF NOT EXISTS (SELECT * FROM lnk_ConditionalFields WHERE Sectionid = @iConSectionId AND ConFieldId = @FieldId	AND FieldId = @iNewFieldId	AND Predefined = @ConPredefined) BEGIN                                   
					Insert Into lnk_ConditionalFields (
							ConfieldId
						,	SectionId
						,	FieldId
						,	FieldLabel
						,	UserId
						,	CreateDate
						,	Predefined
						,	Seq
						,	Conpredefine)
					Values (
							@FieldId
						,	@iConSectionId
						,	@iNewFieldId
						,	@ConFieldLabel
						,	@UserId
						,	getdate()
						,	@ConPredefined
						,	@ConSeq
						,	@Predefined)
				END                   
			END
		END-----01                        
		ELSE BEGIN -----IF(@Predefined=1)       
			SET @iNewFieldId = @ConFieldId
			IF (@ControlID = 9 OR @ControlId = 4) BEGIN
			--IF field already exists on form , irrespective of section. then dont insert in lnk_forms.                                      
				IF NOT EXISTS (SELECT * FROM lnk_ConditionalFields WHERE Sectionid = @iConSectionId	AND ConFieldId = @FieldId AND FieldId = @iNewFieldId AND Predefined = @ConPredefined) BEGIN
					Insert Into lnk_ConditionalFields (
							ConfieldId
						,	SectionId
						,	FieldId
						,	FieldLabel
						,	UserId
						,	CreateDate
						,	Predefined
						,	Seq
						,	Conpredefine)
					Values (
							@FieldId
						,	@iConSectionId
						,	@iNewFieldId
						,	@ConFieldLabel
						,	@UserId
						,	getdate()
						,	@ConPredefined
						,	@ConSeq
						,	@Predefined)
				END
			END 
			ELSE IF (@ControlId = 6) BEGIN
				SELECT @iConSectionId = Id FROM mst_YesNo WHERE Name = @ModdecodeName
				IF NOT EXISTS (SELECT * FROM lnk_ConditionalFields WHERE SectionId = @iConSectionId	AND ConfieldId = @FieldId AND FieldId = @ConFieldId	AND Predefined = @ConPredefined) BEGIN
               		Insert Into lnk_ConditionalFields (
							ConfieldId
						,	SectionId
						,	FieldId
						,	FieldLabel
						,	UserId
						,	CreateDate
						,	Predefined
						,	Seq
						,	Conpredefine)
					Values (
							@FieldId
						,	@iConSectionId
						,	@iNewFieldId
						,	@ConFieldLabel
						,	@UserId
						,	getdate()
						,	@ConPredefined
						,	@ConSeq
						,	@Predefined)
				END
			END
		END
                               
		DECLARE @tempPreBusRuleCount int
		DECLARE @tempPreBusRuleValCount int
		DECLARE @tempPreBusRuleId int
		DECLARE @tempPreBusRulevalue varchar(100)
		DECLARE @tempPreBusRuleSplitvalue varchar(100)
		DECLARE @tempPreBusRuleSplitvalue2 varchar(100)
		DECLARE @PreIndex int
		DECLARE @PreIndex2 int
		--business rules for the field                                  
		--re-eneter all business rule again, and remove previously set business rule, applies to both predefined and non                                  
		--predefined fields                                  
		DELETE FROM lnk_fieldsBusinessRule WHERE FieldId = @iNewFieldId	AND predefined = @ConPredefined
		SELECT @tempPreBusRuleCount = count(*)	FROM dbo.fnParseDelimitedList(@ConBusRuleIdValAll, ',')
		SET @PreIndex = 1;
		WHILE @PreIndex <= @tempPreBusRuleCount BEGIN--1                                                         
			SET @tempPreBusRulevalue = dbo.fnGetParmTilte(@PreIndex, @ConBusRuleIdValAll);                                  
			SELECT @tempPreBusRuleValCount = count(*) FROM dbo.fnParseDelimitedList(@tempPreBusRulevalue, '-');
			SET @tempPreBusRuleId = dbo.fnGetParmTilteForHighphen(1, @tempPreBusRulevalue)
			SET @tempPreBusRuleSplitvalue = dbo.fnGetParmTilteForHighphen(2, replace(@tempPreBusRulevalue, 'Null', ''))
			SET @tempPreBusRuleSplitvalue2 = dbo.fnGetParmTilteForHighphen(3, replace(@tempPreBusRulevalue, 'Null', ''))
			EXEC Pr_PMTCT_SaveBusinessRules_Futures	
					@iNewFieldId
				,	@tempPreBusRuleId
				,	@tempPreBusRuleSplitvalue
				,	@UserId
				,	@Predefined
				,	@tempPreBusRuleSplitvalue2
			SET @PreIndex = @PreIndex + 1;
		END--1                        
	END--0 
	ELSE BEGIN--0                         
		IF (@ConPredefined = 0) BEGIN---01                                          
			IF (@ControlId = 6) BEGIN
				SELECT @iConSectionId = Id FROM mst_YesNo WHERE Name = @ModdecodeName
			END 
			ELSE BEGIN
				IF (@Predefined = '1') BEGIN
					SET @iConSectionId = @ConSectionId
				END 
				ELSE BEGIN
					SELECT @iModcodeId = CodeId FROM mst_ModCode WHERE Name = @FieldName
					SELECT @iConSectionId = Id	FROM mst_ModDeCode	WHERE Name = @ModdecodeName	AND CodeId = @iModcodeId
				END
			END
			IF EXISTS (SELECT * FROM mst_CustomformField WHERE FieldName = '' + @ConFieldName + ''	AND deleteflag = 0 ) BEGIN----1                                  
				--use same fieldId           
				SELECT @iNewFieldId = Id	FROM mst_CustomformField WHERE fieldName = '' + @ConFieldName + '' AND deleteflag = 0
				IF (@ControlId = 4 OR @ControlId = 9) BEGIN---3   
					--IF field already exists on form , irrespective of section. then dont insert in lnk_forms.                                  
					IF NOT EXISTS (SELECT *	FROM lnk_ConditionalFields WHERE Sectionid = @iConSectionId AND ConFieldId = @FieldId AND FieldId = @iNewFieldId AND Predefined = @ConPredefined) BEGIN---4                                   
						Insert Into lnk_ConditionalFields (
								ConfieldId
							,	SectionId
							,	FieldId
							,	FieldLabel
							,	UserId
							,	CreateDate
							,	Predefined
							,	Seq
							,	Conpredefine)
						Values (
								@FieldId
							,	@iConSectionId
							,	@iNewFieldId
							,	@ConFieldLabel
							,	@UserId
							,	getdate()
							,	@ConPredefined
							,	@ConSeq
							,	@Predefined)
					END----4                        
				END---3                        
				ELSE IF (@ControlId = 6) BEGIN----5    
					SET @iConSectionId = @ConSectionId
					--IF field already exists on form , irrespective of section. then dont insert in lnk_forms.                                  
					IF NOT EXISTS (SELECT * FROM lnk_ConditionalFields WHERE Sectionid = @iConSectionId	AND ConFieldId = @FieldId AND FieldId = @iNewFieldId AND Predefined = @ConPredefined) BEGIN---6                                   
						Insert Into lnk_ConditionalFields (
								ConfieldId
							,	SectionId
							,	FieldId
							,	FieldLabel
							,	UserId
							,	CreateDate
							,	Predefined
							,	Seq
							,	Conpredefine)
						Values (
								@FieldId
							,	@iConSectionId
							,	@iNewFieldId
							,	@ConFieldLabel
							,	@UserId
							,	getdate()
							,	@ConPredefined
							,	@ConSeq
							,	@Predefined)
					END---6                        
				END---5
			END ---1                                 
			ELSE BEGIN---1                           
			-- insert field and corresponding select list value 
				
				EXEC @RC = Pr_PMTCT_SaveUpdateConditionalCustomFields_Futures	
						@FieldId
					,	@ConFieldName
					,	@ConControlId
					,	0
					,	@UserId
					,	0
					,	@ConSelectListVal
					,	@ConPredefined
					,	@ConFieldLabel
					,	@ConSeq
					,	0
					,	@SystemId
				SET @iNewFieldId = @RC;
				IF NOT EXISTS (SELECT * FROM lnk_ConditionalFields WHERE Sectionid = @iConSectionId AND ConFieldId = @FieldId	AND FieldId = @iNewFieldId	AND Predefined = @ConPredefined) BEGIN--2    
					Insert Into lnk_ConditionalFields (
							ConfieldId
						,	SectionId
						,	FieldId
						,	FieldLabel
						,	UserId
						,	CreateDate
						,	Predefined
						,	Seq
						,	Conpredefine)
					Values (
							@FieldId
						,	@iConSectionId
						,	@iNewFieldId
						,	@ConFieldLabel
						,	@UserId
						,	getdate()
						,	@ConPredefined
						,	@ConSeq
						,	@Predefined)
				END--2                               
			END--1 
		END----01                        
		ELSE BEGIN--01                        
			SET @iNewFieldId = @ConFieldId
			IF (@ControlID = 9 OR @ControlId = 4) BEGIN---1         
				SET @iConSectionId = @ConSectionId                                   
				IF NOT EXISTS (SELECT * FROM lnk_ConditionalFields WHERE Sectionid = @iConSectionId AND ConFieldId = @FieldId AND FieldId = @iNewFieldId AND Predefined = @ConPredefined) BEGIN---2                                       
					Insert Into lnk_ConditionalFields (
							ConfieldId
						,	SectionId
						,	FieldId
						,	FieldLabel
						,	UserId
						,	CreateDate
						,	Predefined
						,	Seq
						,	Conpredefine)
					Values (
							@FieldId
						,	@iConSectionId
						,	@iNewFieldId
						,	@ConFieldLabel
						,	@UserId
						,	getdate()
						,	@ConPredefined
						,	@ConSeq
						,	@Predefined)
				END---2                        
			END---1                        
			ELSE IF (@ControlId = 6) BEGIN----3                        
				SELECT @iConSectionId = Id FROM mst_YesNo WHERE name = @ModdecodeName
				IF NOT EXISTS ( SELECT * FROM lnk_ConditionalFields WHERE Sectionid = @iConSectionId AND ConFieldId = @FieldId AND FieldId = @ConFieldId AND Predefined = @ConPredefined) BEGIN----4                                       
					Insert Into lnk_ConditionalFields (
							ConfieldId
						,	SectionId
						,	FieldId
						,	FieldLabel
						,	UserId
						,	CreateDate
						,	Predefined
						,	Seq
						,	Conpredefine)
					Values (
							@FieldId
						,	@iConSectionId
						,	@iNewFieldId
						,	@ConFieldLabel
						,	@UserId
						,	getdate()
						,	@ConPredefined
						,	@ConSeq
						,	@Predefined)
				END----4                        
			END---3                                
		END----01                                                            
		DECLARE @tempBusRuleCount int
		DECLARE @tempBusRuleValCount int
		DECLARE @tempBusRuleId int
		DECLARE @tempBusRulevalue varchar(100)
		DECLARE @tempBusRuleSplitvalue varchar(100)
		DECLARE @tempBusRuleSplitvalue2 varchar(100)
		DECLARE @Index int
		DECLARE @Index2 int

		--business rules for the field                                  
		--re-eneter all business rule again, and remove previously set business rule, applies to both predefined and non                                  
		--predefined fields                                  
		Delete From lnk_fieldsBusinessRule
		Where FieldId = @iNewFieldId
			And predefined = @ConPredefined

		Select @tempBusRuleCount = count(*)
		From dbo.fnParseDelimitedList(@ConBusRuleIdValAll, ',')
		Set @Index = 1;

		While @Index <= @tempBusRuleCount Begin--1                                               
			Set @tempBusRulevalue = dbo.fnGetParmTilte(@Index, @ConBusRuleIdValAll);
			Select @tempBusRuleValCount = count(*) From dbo.fnParseDelimitedList(@tempBusRulevalue, '-')
			Set @tempBusRuleId = dbo.fnGetParmTilteForHighphen(1, @tempBusRulevalue)
			Set @tempBusRuleSplitvalue = dbo.fnGetParmTilteForHighphen(2, replace(@tempBusRulevalue, 'Null', ''))
			Set @tempBusRuleSplitvalue2 = dbo.fnGetParmTilteForHighphen(3, replace(@tempBusRulevalue, 'Null', ''))
			Exec Pr_PMTCT_SaveBusinessRules_Futures	
					@iNewFieldId
				,	@tempPreBusRuleId
				,	@tempBusRuleSplitvalue
				,	@UserId
				,	@Predefined
				,	@tempBusRuleSplitvalue2
			Set @Index = @Index + 1;
		End--1                                           
	END--0                                
	SELECT @iNewFieldId
END
GO

/****** Object:  StoredProcedure [dbo].[Pr_ImportExportForms_FetchFormsDetail_Futures]    Script Date: 03/17/2016 08:27:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_ImportExportForms_FetchFormsDetail_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_ImportExportForms_FetchFormsDetail_Futures]
GO

/****** Object:  StoredProcedure [dbo].[Pr_ImportExportForms_FetchFormsDetail_Futures]    Script Date: 03/17/2016 08:27:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Pr_ImportExportForms_FetchFormsDetail_Futures]                                                        
 @FeatureName varchar(50)                                                        
as                                                        
begin                                                  
 declare @iFeatureId as int                                            
 declare @iModuleId as int                                                     
 select @iFeatureID=Featureid,@iModuleId=ModuleId from mst_feature where featureName=@FeatureName and deleteFlag=0                                                        
                                                 
 --Fetch Form details                                                  
 select FeatureId,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserId,OptionalFlag ,SystemId,Published,CountryId,ModuleId,MultiVisit from mst_feature where FeatureId=@iFeatureID and deleteFlag=0                                                        
                                          
 --Fetch all Section details for particular form                                                
 select SectionId,SectionName,CustomFlag,DeleteFlag,UserId,CreateDate,UpdateDate,FeatureId,Seq,ISNULL(IsGridView,0)[IsGridView]          
 from mst_section where featureid=@iFeatureId  and deleteFlag=0   order by seq                                                      
                                                 
--lnk forms with fields                                                
 select * from                                                
 (select F.id lnkFormId,f.featureid,f.sectionId,f.fieldid,f.fieldLabel,f.predefined,f.seq,cf.id,cf.FieldName FieldName,                                                
 cf.FieldDesc FieldDesc,cf.ControlId,cf.deleteflag,cf.PatientRegistration                                                
 from lnk_forms f                                                 
 inner join mst_customFormField cf on f.FieldId=cf.id                                                
 where  featureid=@iFeatureId and cf.deleteflag=0 and f.predefined=0                                                      
 union                                                
 select F.id lnkFormId,f.featureid,f.sectionId,f.fieldid,f.fieldLabel,f.predefined,f.seq,                                                
 pf.id,pf.PDFName FieldName,Pf.PDFTableName FieldDesc,null ControlId,0 deleteflag,pf.PatientRegistration                                                
 from lnk_forms f                                                 
 inner join mst_predefinedFields pf on f.FieldId=pf.id                                                
 where  featureid=@iFeatureId and f.predefined=1 ) TblFieldDetails order by sectionId,seq                          
                                               
                                                 
--select list details for all predefined fields                                      
                                           
 if exists(                                                
    select lf.id lnkFormId,lf.featureid,lf.SectionId,lf.FieldId,cf.FieldName,cf.categoryid,dc.Id DecodeId,dc.Name ListVal,                                                
     dc.CodeId                                                
     from mst_customFormField cf                                                 
------     inner join  mst_PMTCTCode  c on cf.id=c.FieldId                                                
------     inner join  mst_PMTCTDecode dc on c.codeid=dc.CodeId                               
  inner join VW_MasterTableLinking dc on cf.categoryid=dc.CodeId and dc.Tablename= cf.BindTable                                               
     inner join lnk_forms lf on cf.id=lf.fieldId                                                 
     where lf.Predefined=0 and dc.predefined=0 and cf.deleteFlag=0 and lf.featureid=@iFeatureId                                        
      
   )                                            
 begin                   
    select lf.id lnkFormId,lf.featureid,lf.SectionId,lf.FieldId,cf.FieldName,cf.categoryid,dc.Id DecodeId,dc.Name ListVal,           
     dc.CodeId,cf.PatientRegistration                                                
     from mst_customFormField cf                 
  inner join VW_MasterTableLinking dc on cf.categoryid=dc.CodeId and dc.Tablename= cf.BindTable                                               
     inner join lnk_forms lf on cf.id=lf.fieldId                                                 
     where lf.Predefined=0 and dc.predefined=0 and cf.deleteFlag=0 and lf.featureid=@iFeatureId                                        
                                           
 end                           
 else --if no record found for select list then show single row and column with 0. at 3rd position there shud b select list                                            
 begin                                            
  select '0'[lnkFormId],'0'[featureid],'0'[SectionId],'0'[FieldId],'0'[FieldName],'0'[categoryid],'0'[DecodeId],'0'[ListVal],'0'[CodeId],'0'[PatientRegistration]                                            
 end                                       
                                          
                                    
                                                
--select * from mst_customFormField                                     
--select * from mst_PMTCTCode                                                 
--select * from mst_PMTCTDecode                                                
--select * from lnk_forms                                                
                                                
--Business Rule details for all the fields                                    
if(exists(                                  
   select cf.Id FieldId,br.BusRuleId,ISNULL(br.Value,'')[Value],ISNULL(br.Value1,'')[Value1],br.predefined from mst_customFormField cf                                                
   inner join lnk_fieldsBusinessRule br on cf.id=br.FieldId                                                
   inner join lnk_Forms lf on cf.id=lf.FieldId                                                
   where br.Predefined=0 and cf.deleteFlag=0 and lf.featureid=@iFeatureId                                                
   union                                              
   select pf.Id FieldId,br.BusRuleId,ISNULL(br.Value,'')[Value],ISNULL(br.Value1,'')[Value1],br.predefined from mst_PredefinedFields pf                                                
   inner join lnk_fieldsBusinessRule br on pf.id=br.FieldId                                                
   inner join lnk_Forms lf on pf.id=lf.FieldId                                                
   where br.Predefined=1 and  lf.featureid=@iFeatureId                                    
))                                  
Begin                                              
 select cf.Id FieldId,br.BusRuleId,ISNULL(br.Value,'')[Value],ISNULL(br.Value1,'')[Value1],br.predefined from mst_customFormField cf                                                
 inner join lnk_fieldsBusinessRule br on cf.id=br.FieldId                                                
 inner join lnk_Forms lf on cf.id=lf.FieldId                                                
 where br.Predefined=0 and cf.deleteFlag=0 and lf.featureid=@iFeatureId                                                
 union                                              
 select pf.Id FieldId,br.BusRuleId,ISNULL(br.Value,'')[Value],ISNULL(br.Value1,'')[Value1],br.predefined from mst_PredefinedFields pf                                                
 inner join lnk_fieldsBusinessRule br on pf.id=br.FieldId                                                
 inner join lnk_Forms lf on pf.id=lf.FieldId                                                
 where br.Predefined=1 and  lf.featureid=@iFeatureId                                     
end                             
else                                  
begin                                  
 select '0'[FieldId],'0'[BusRuleId],'0'[Value],'0'[Value1],'0'[predefined]                                  
end                                            
                                  
--Fetch Modules(Technical Area)                
if(@iModuleId>0)                
 Begin                                                 
	Select	ModuleID,
			ModuleName,
			DeleteFlag,
			UserId,
			CreateDate,
			UpdateDate,
			Status,
			UpdateFlag,
			Identifier,
			PharmacyFlag,
			DisplayName,
			CanEnroll
	From mst_module	Where (ModuleID = @iModuleId) And (DeleteFlag = 0) ;                                 
 End                
Else                 
 Begin                
   SELECT        '0' AS ModuleID, '0' AS ModuleName, '0' AS DeleteFlag, '0' AS UserId, 
   '1900-01-01 00:00:00.000' AS CreateDate, '1900-01-01 00:00:00.000' AS UpdateDate, '0' AS Status, '0' AS UpdateFlag, 
   '0' AS Identifier ,''DisplayName, 1 CanEnroll               
 End                
--fetch modules identifiers                            
if(@iModuleId>0)                
 Begin                                                 
  select id,fieldName,fieldType,pmi.ModuleId from                                        
  mst_patientIdentifier pid inner join lnk_patientModuleIdentifier pmi on pid.id=pmi.fieldid where pmi.moduleid=@iModuleId                                           
 End                
Else                 
 Begin                
   select '0'[id],'0'[fieldName],'0'[fieldType],'0'[ModuleId]                
 End                   
                
-------------------------------------------------------------------------------------------------------------------------------                           
if(@iFeatureId=126)                
Begin                
   --------------Get All Patient Registration Conditional Feilds ---Table 7                          
 if exists(select FieldId[id],f.featureid,f.fieldsectionId[sectionId],f.fieldid,f.fieldLabel,f.fieldpredefined,f.FieldSectionSequence[seq],                          
 f.FieldId[id],f.FieldName [FieldName],f.FieldControlId[ControlId],f.ConditionalFieldId,f.ConditionalFieldlabel,f.ConditionalFieldName,                          
 f.ConditionalFieldControlId,f.ConditionalFieldBindTable,f.ConditionalFieldCategoryId,f.ConditionalFieldBindField,f.ConditionalFieldSectionId,                     
 f.ConditionalFieldPredefined,f.ConditionalFieldSequence                          
 from VW_RegistrationConditionalField f join mst_ModdeCode mc on f.ConditionalFieldSectionId=mc.Id                    
 where f.ConditionalFieldId is not null and f.featureid=@iFeatureId)                      
 Begin                      
  select FieldId[id],f.featureid,f.fieldsectionId[sectionId],f.fieldid,f.fieldLabel,f.fieldpredefined,f.FieldSectionSequence[seq],                          
  f.FieldId[id],f.FieldName [FieldName],f.FieldControlId[ControlId],f.ConditionalFieldId,f.ConditionalFieldlabel,f.ConditionalFieldName,                          
  f.ConditionalFieldControlId,f.ConditionalFieldBindTable,f.ConditionalFieldCategoryId,f.ConditionalFieldBindField,f.ConditionalFieldSectionId,                          
  f.ConditionalFieldPredefined,f.ConditionalFieldSequence,CASE WHEN mc.Name<>'NA' Then mc.Name Else 'Null' end[Mod],f.ConditionalPatRegistration                          
  from VW_RegistrationConditionalField f join mst_ModdeCode mc on f.ConditionalFieldSectionId=mc.Id                    
  where f.ConditionalFieldId is not null and f.featureid=@iFeatureId order by sectionId,seq                      
 End                           
 else --if no record found for select list then show single row and column with 0. at 3rd position there shud b select list                                            
  begin                                            
   select '0'[id],'0'[featureid],'0'[sectionId],'0'[fieldid],'0'[fieldLabel],'0'[fieldpredefined],'0'[seq],'0'[id],'0'[FieldName],'0'[ControlId],'0'[ConditionalFieldId],        
 '0'[ConditionalFieldlabel],'0'[ConditionalFieldName],'0'[ConditionalFieldControlId],'0'[ConditionalFieldBindTable],'0'[ConditionalFieldCategoryId],        
 '0'[ConditionalFieldBindField],'0'[ConditionalFieldSectionId],'0'[ConditionalFieldPredefined],'0'[ConditionalFieldSequence],'0'[Mod],'0'[ConditionalPatRegistration]                                            
  end                         
 -------------------Get Select List for Patient Registration Condional Fields---------------------------------                          
 if exists(                            
  select f.featureid,f.ConditionalFieldSectionId[SectionId],f.ConditionalFieldId[FieldId],cf.FieldName,cf.categoryid,dc.Id DecodeId,dc.Name ListVal,                                                
   dc.CodeId                                                
   from mst_customFormField cf                           
   inner join VW_MasterTableLinking dc on cf.categoryid=dc.CodeId and dc.Tablename= cf.BindTable                          
   inner join VW_RegistrationConditionalField f on cf.id=f.ConditionalFieldId                          
   where f.ConditionalFieldId is not null and f.ConditionalFieldControlId in (4,9) and f.ConditionalFieldPredefined=0 and f.featureid=@iFeatureId                                         
    )                                      
  begin                            
  select f.featureid,f.ConditionalFieldSectionId[SectionId],f.ConditionalFieldId[FieldId],cf.FieldName,cf.categoryid,dc.Id DecodeId,dc.Name ListVal,                                                
   dc.CodeId                                                
   from mst_customFormField cf                           
   inner join VW_MasterTableLinking dc on cf.categoryid=dc.CodeId and dc.Tablename= cf.BindTable                          
  inner join VW_RegistrationConditionalField f on cf.id=f.ConditionalFieldId                          
   where f.ConditionalFieldId is not null and f.ConditionalFieldControlId in (4,9) and f.ConditionalFieldPredefined=0 and f.featureid=@iFeatureId                           
                                         
                                            
  end                                            
  else --if no record found for select list then show single row and column with 0. at 3rd position there shud b select list                                            
  begin                                            
   select '0'[featureid],'0'[SectionId],'0'[FieldId],'0'[FieldName],'0'[categoryid],'0'[DecodeId],'0'[ListVal],'0'[CodeId]                                            
  end                                                    
 -------------------Business Rule details for all the Patient Registration Conditional fields-----------------------------                        
 if(exists(                                  
    select br.FieldId,br.BusRuleId,ISNULL(br.Value,'')[Value],ISNULL(br.Value1,'')[Value1],br.predefined from lnk_fieldsBusinessRule br                         
    where br.FieldId in(select ConditionalFieldId from VW_RegistrationConditionalField f where featureid=@iFeatureId)                                                
                                       
 ))                                  
 Begin                                              
  select br.FieldId,br.BusRuleId,ISNULL(br.Value,'')[Value],ISNULL(br.Value1,'')[Value1],br.predefined from lnk_fieldsBusinessRule br                         
  where br.FieldId in(select ConditionalFieldId from VW_RegistrationConditionalField f where featureid=@iFeatureId)                                     
 end                     else                                  
 begin                                  
  select '0'[FieldId],'0'[BusRuleId],'0'[Value],'0'[Value1],'0'[predefined]                                  
 end                         
 ------------------------------------------------                   
                
End                
Else                
Begin                          
 --------------Get All Conditional Feilds ---Table 7                          
 if exists(select FieldId[id],f.featureid,f.fieldsectionId[sectionId],f.fieldid,f.fieldLabel,f.fieldpredefined,f.FieldSectionSequence[seq],                          
 f.FieldId[id],f.FieldName [FieldName],f.FieldControlId[ControlId],f.ConditionalFieldId,f.ConditionalFieldlabel,f.ConditionalFieldName,                          
 f.ConditionalFieldControlId,f.ConditionalFieldBindTable,f.ConditionalFieldCategoryId,f.ConditionalFieldBindField,f.ConditionalFieldSectionId,                          
 f.ConditionalFieldPredefined,f.ConditionalFieldSequence                          
 from Vw_FieldConditionalField f join mst_ModdeCode mc on f.ConditionalFieldSectionId=mc.Id                    
 where f.ConditionalFieldId is not null and f.featureid=@iFeatureId)                      
 Begin                      
  select FieldId[id],f.featureid,f.fieldsectionId[sectionId],f.fieldid,f.fieldLabel,f.fieldpredefined,f.FieldSectionSequence[seq],                          
  f.FieldId[id],f.FieldName [FieldName],f.FieldControlId[ControlId],f.ConditionalFieldId,f.ConditionalFieldlabel,f.ConditionalFieldName,                          
  f.ConditionalFieldControlId,f.ConditionalFieldBindTable,f.ConditionalFieldCategoryId,f.ConditionalFieldBindField,f.ConditionalFieldSectionId,                          
  f.ConditionalFieldPredefined,f.ConditionalFieldSequence,CASE WHEN mc.Name<>'NA' Then mc.Name Else 'Null' end[Mod]                          
  from Vw_FieldConditionalField f join mst_ModdeCode mc on f.ConditionalFieldSectionId=mc.Id                    
  where f.ConditionalFieldId is not null and f.featureid=@iFeatureId order by sectionId,seq                      
 End                           
 else --if no record found for select list then show single row and column with 0. at 3rd position there shud b select list                                            
  begin                                            
   select '0'[id],'0'[featureid],'0'[sectionId],'0'[fieldid],'0'[fieldLabel],'0'[fieldpredefined],'0'[seq],'0'[id],'0'[FieldName],'0'[ControlId],'0'[ConditionalFieldId],        
 '0'[ConditionalFieldlabel],'0'[ConditionalFieldName],'0'[ConditionalFieldControlId],'0'[ConditionalFieldBindTable],'0'[ConditionalFieldCategoryId],        
 '0'[ConditionalFieldBindField],'0'[ConditionalFieldSectionId],'0'[ConditionalFieldPredefined],'0'[ConditionalFieldSequence],'0'[Mod]                                            
  end                         
 -------------------Get Select List for Condional Fields---------------------------------                          
 if exists(                                                
  select f.featureid,f.ConditionalFieldSectionId[SectionId],f.ConditionalFieldId[FieldId],cf.FieldName,cf.categoryid,dc.Id DecodeId,dc.Name ListVal,                                                
   dc.CodeId                                                
   from mst_customFormField cf                           
   inner join VW_MasterTableLinking dc on cf.categoryid=dc.CodeId and dc.Tablename= cf.BindTable                          
   inner join Vw_FieldConditionalField f on cf.id=f.ConditionalFieldId                          
   where f.ConditionalFieldId is not null and f.ConditionalFieldControlId in (4,9) and f.ConditionalFieldPredefined=0 and f.featureid=@iFeatureId                                         
                                    
    )                                            
  begin                            
  select f.featureid,f.ConditionalFieldSectionId[SectionId],f.ConditionalFieldId[FieldId],cf.FieldName,cf.categoryid,dc.Id DecodeId,dc.Name ListVal,                                                
   dc.CodeId                                                
   from mst_customFormField cf                           
   inner join VW_MasterTableLinking dc on cf.categoryid=dc.CodeId and dc.Tablename= cf.BindTable                          
   inner join Vw_FieldConditionalField f on cf.id=f.ConditionalFieldId                          
   where f.ConditionalFieldId is not null and f.ConditionalFieldControlId in (4,9) and f.ConditionalFieldPredefined=0 and f.featureid=@iFeatureId                           
                                         
                                            
  end                                            
  else --if no record found for select list then show single row and column with 0. at 3rd position there shud b select list                                            
  begin                                            
   select '0'[featureid],'0'[SectionId],'0'[FieldId],'0'[FieldName],'0'[categoryid],'0'[DecodeId],'0'[ListVal],'0'[CodeId]                                            
  end                                     
 -------------------Business Rule details for all the Conditional fields-----------------------------                        
 if(exists(                                  
    select br.FieldId,br.BusRuleId,ISNULL(br.Value,'')[Value],ISNULL(br.Value1,'')[Value1],br.predefined from lnk_fieldsBusinessRule br                         
    where br.FieldId in(select ConditionalFieldId from Vw_FieldConditionalField f where featureid=@iFeatureId)                                 
                                       
 ))                                  
 Begin                                              
  select br.FieldId,br.BusRuleId,ISNULL(br.Value,'')[Value],ISNULL(br.Value1,'')[Value1],br.predefined from lnk_fieldsBusinessRule br                         
  where br.FieldId in(select ConditionalFieldId from Vw_FieldConditionalField f where featureid=@iFeatureId)                                     
 end                                  
 else                                  
 begin                                  
  select '0'[FieldId],'0'[BusRuleId],'0'[Value],'0'[Value1],'0'[predefined]                                  
 end                         
 ------------------------------------------------                 
End                  
                  
select ID,AppVer,DBVer,RelDate from AppAdmin             
            
 -------------------ICD Code Fields Linking Table-----------------------------                        
 if exists(select icd.FieldId,icd.Blockid,icd.SubBlockId,icd.CodeId,icd.Predefined,icd.UserId,icd.DeleteFlag from lnk_FieldICDCode icd             
   join lnk_forms frm on icd.Fieldid=frm.fieldid and icd.Predefined=frm.Predefined where featureid=@iFeatureId)             
                                   
 Begin                                              
  select icd.FieldId,icd.Blockid,icd.SubBlockId,icd.CodeId,icd.Predefined,icd.UserId,icd.DeleteFlag from lnk_FieldICDCode icd             
  join lnk_forms frm on icd.Fieldid=frm.fieldid and icd.Predefined=frm.Predefined where featureid=@iFeatureId                                     
 end                                  
 else                                  
 begin                                  
  select '0'[FieldId],'0'[Blockid],'0'[SubBlockId],'0'[CodeId],'0'[Predefined],'0'[UserId],'0'[DeleteFlag]                                  
 end                         
 ------------------------------------------------         
if exists (select * from Mst_FormBuilderTab where FeatureID = @iFeatureId)    
begin     
 select TabID ,TabName ,FeatureID,DeleteFlag,UserID ,seq from Mst_FormBuilderTab where FeatureID = @iFeatureId order by seq      
end     
else    
begin    
 select '0'[TabID] ,''[TabName] ,'0'[FeatureID],'0'[DeleteFlag],'0'[UserID],'0'[seq]     
    
end    
    
if exists (select * from lnk_FormTabSection where FeatureID = @iFeatureId)    
begin     
 select  ID ,TabID ,SectionID ,FeatureID,UserID from lnk_FormTabSection where FeatureID = @iFeatureId    
end     
else    
begin    
 select '0'[ID] ,'0'[TabID] ,'0'[SectionID] ,'0'[FeatureID],'0'[UserID]     
    
end    
       
-----Get Special Form Linking Details---------------------- 

if exists (select lnk.moduleid, mst.modulename, lnk.featureid from lnk_splformmodule lnk inner join mst_module mst    
			on lnk.moduleid = mst.moduleid where lnk.moduleid = @iModuleId)    
begin     
 select lnk.moduleid, mst.modulename, lnk.featureid from lnk_splformmodule lnk inner join mst_module mst    
on lnk.moduleid = mst.moduleid where lnk.moduleid = @iModuleId    
end     
else    
begin    
 select '0'[moduleid] ,''[modulename] ,'0'[FeatureID]     
    
end    
End

GO


/****** Object:  StoredProcedure [dbo].[Pr_FormBuilder_SaveMstTab_Futures]    Script Date: 6/9/2016 9:00:49 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_FormBuilder_SaveMstTab_Futures]') AND type in (N'P', N'PC'))
Drop Procedure [dbo].[Pr_FormBuilder_SaveMstTab_Futures]
Go

/****** Object:  StoredProcedure [dbo].[Pr_FormBuilder_SaveMstTab_Futures]    Script Date: 6/9/2016 9:00:49 PM ******/
Set Ansi_nulls On
Go

Set Quoted_identifier On
Go

CREATE procedure [dbo].[Pr_FormBuilder_SaveMstTab_Futures]       
@TabID int,       
@TabName varchar(200),                                            
@FeatureId int=null,                                                                                      
@DeleteFlag int=0,                                                                                    
@UserId int=null,      
@seq int =null,
@Signature int=null                                                                                  
as                                                     
Begin
                                              
      
	Declare @iTabId as int
	If Exists (	Select * From Mst_FormBuilderTab	Where TabID = @TabID) Begin
		Update Mst_FormBuilderTab	Set	
			TabName = @TabName
			,Seq = @Seq
			,DeleteFlag = @DeleteFlag
			,UserId = @UserId
			,FeatureId = @FeatureId
			,UpdateDate = getdate()
			,Signature = @Signature
		Where TabID = @TabID;

		Set @iTabID = @TabID
	End 
	Else Begin
		Insert Into Mst_FormBuilderTab (TabName, FeatureID, DeleteFlag, UserId, CreateDate, seq, Signature)
		Values (@TabName, @FeatureId, 0, @UserId, getdate(), @Seq, @Signature);
		Set @iTabID = scope_identity();
	End
	Select @iTabID
End

Go

/****** Object:  StoredProcedure [dbo].[Pr_FormBuilder_RemoveFieldInFormBuilder_Futures]    Script Date: 6/9/2016 8:56:05 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_FormBuilder_RemoveFieldInFormBuilder_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_FormBuilder_RemoveFieldInFormBuilder_Futures]
GO

/****** Object:  StoredProcedure [dbo].[Pr_FormBuilder_RemoveFieldInFormBuilder_Futures]    Script Date: 6/9/2016 8:56:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Pr_FormBuilder_RemoveFieldInFormBuilder_Futures]      
@Id int,      
@FieldName varchar(50),      
@TableName varchar(500)      
as       
begin      
 declare @strQuery as varchar(max)      
 delete from lnk_forms where id=@Id      
      
if Exists(select * from syscolumns where id=object_id(''+ @TableName +'') and [name]='' + @FieldName + '')        
 begin      
 set @strQuery='If Not Exists(Select * From [' + @TableName + '] Where [' + @FieldName + '] Is Not Null) Begin
	  Alter Table [' + @TableName + '] Drop Column [' + @FieldName  + ']  
	End '     
  exec(@strQuery)      
 end      
end

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_FormBuilder_CustomTableCreation_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_FormBuilder_CustomTableCreation_Futures]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Pr_FormBuilder_CustomTableCreation_Futures]                                                        
	@TableName varchar(500),                                                        
	@FieldName varchar(50),                                                        
	@DataType int,                                    
	@Predefined int,                                    
	@FieldId int                                                        
as                                                        
begin                                                       
                                               
 Declare @DataTypeValue varchar(100)              
 declare @PATIndex int              
 declare @NewFieldId varchar(50)      
	Select	@NewFieldId = F.FieldId
		,	@DataTypeValue = F.ControlDataType
	From FieldsView F
	Where F.FieldName = @FieldName And F.Predefined = @Predefined           
     
	If(@DataType Not In (9,11,12,13,16) and @Predefined = 0)  Begin 
	          
		If Exists (Select * From sysobjects Where [Name] = '' + @TableName + '' And xtype = 'U') Begin---Not for (@ControlID = 9) 			                                                            
			If Not Exists (Select * From syscolumns Where id = object_id('' + @TableName + '') 	And [name] = '' + @FieldName + '' ) Begin				
				Exec ('ALTER TABLE [' + @TableName + '] ADD  [' + @FieldName + ']  ' + @DataTypeValue + ' ')
			End
		End
		Else Begin
			EXEC('CREATE TABLE ['+@TableName +'] ( ID int IDENTITY(1,1), Ptn_pk int NOT NULL ,Visit_Pk int NOT NULL,                        
			LocationId int NOT NULL,UserId int ,CreateDate DateTime,UpdateDate DateTime, ['+ @FieldName +'] '+@DataTypeValue+', Primary Key(ID)) ')                                       
		End                        
	End           
      
     Select @DataTypeValue = Null                            
   --////----- Section Added for Conditional Fields by Sanjay ------//--                                    
	Declare ConditionalField Cursor For  
		Select	FieldName
			,	mst.ControlId
			,	C.DataType
		From dbo.Mst_CustomFormField Mst
		Inner Join dbo.lnk_ConditionalFields Lnk On Mst.Id = Lnk.FieldId
		Inner Join mst_control C On C.ControlID = mst.ControlId
		Where Lnk.Predefined = 0
			And Lnk.ConFieldId In (	Select fieldId	From Lnk_Conditionalfields	Where confieldId = @NewFieldId	)               
		union                  
		Select	FieldName
			,	mst.ControlId
			,	C.DataType
		From dbo.Mst_CustomFormField Mst
		Inner Join dbo.lnk_ConditionalFields Lnk On Mst.Id = Lnk.FieldId
		Inner Join mst_control C On C.ControlID = mst.ControlId
		Where Lnk.Predefined = 0
			And Lnk.ConFieldId = @NewFieldId                
		union              
		Select	FieldName
			,	mst.ControlId
			,	C.DataType
		From dbo.Mst_CustomFormField Mst
		Inner Join dbo.lnk_PatientRegconditionalfields Lnk On Mst.Id = Lnk.FieldId
		Inner Join mst_control C On C.ControlID = mst.ControlId
		Where Lnk.Predefined = 0
			And Lnk.ConFieldId = @NewFieldId  
			declare @FldName varchar(500)                                   
			declare @DType int                                  
		Open ConditionalField                                    
			Fetch Next From ConditionalField Into @FldName,@DType ,@DataTypeValue                                   
				While @@fetch_status = 0 Begin                                              
					If(@DType Not In (9,11,12,13,16)) Begin                                                                      
						If Exists(Select * from sysobjects  where [Name]='' + @TableName + '' and xtype='U') Begin ---Not for (@ControlID = 9) 
							If Not Exists(select * from syscolumns where id=object_id(''+ @TableName +'') and [name]='' + @FldName + '') Begin                          
								EXEC('ALTER TABLE ['+@TableName +'] ADD  ['+ @FldName +']  '+@DataTypeValue+' ')                                                                   
							End
						End                           
						Else Begin                        
							EXEC('CREATE TABLE ['+@TableName +'] ( ID int IDENTITY(1,1), Ptn_pk int NOT NULL ,Visit_Pk int NOT NULL,                        
							LocationId int NOT NULL,UserId int ,CreateDate DateTime,UpdateDate DateTime, ['+ @FldName +'] '+@DataTypeValue+', Primary Key(ID)) ')                       
						End 
						
					End                                     
				Fetch Next From ConditionalField Into @FldName,@DType ,@DataTypeValue                                   
				End  
			Close ConditionalField                                    
			Deallocate ConditionalField                                    
                                  
End
  
GO
/****** Object:  StoredProcedure [dbo].[Pr_FormBuilder_SaveLnkForm_Futures]    Script Date: 6/9/2016 8:51:43 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_FormBuilder_SaveLnkForm_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_FormBuilder_SaveLnkForm_Futures]
GO

/****** Object:  StoredProcedure [dbo].[Pr_FormBuilder_SaveLnkForm_Futures]    Script Date: 6/9/2016 8:51:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Pr_FormBuilder_SaveLnkForm_Futures]              
@Id int,              
@FeatureId int,              
@SectionId int,              
@FieldId int,              
@FieldLabel varchar(100),             
@Seq int,             
@UserId int,              
@Predefined int              
as              
begin        
--declare @tempfield int           
if(charindex('8888',@FieldId) >0)           
begin           
set @FieldId = replace(@FieldId,'8888','')          
end          
else if(charindex('9999',@FieldId) >0)           
begin           
set @FieldId = replace(@FieldId,'9999','')          
end 
              
if exists(select * from lnk_forms where Id=@Id)                
 begin              
  update lnk_forms set FeatureId=@FeatureId,SectionId=@SectionId,FieldId=@FieldId,FieldLabel=@FieldLabel,              
  Seq=@Seq,UserId=@UserId,Predefined=@Predefined,UpdateDate=getdate()              
  where id=@Id              
 end              
else              
 begin              
  insert into lnk_forms (FeatureId,SectionId,FieldId,FieldLabel,Seq,UserId,Predefined,CreateDate)              
  values(@FeatureId,@SectionId,@FieldId,@FieldLabel,@Seq,@UserId,@Predefined,getdate())              
 end              
end

GO


/****** Object:  StoredProcedure [dbo].[Pr_FormBuilder_CustomTableCreationGridView_Futures]    Script Date: 6/9/2016 8:51:21 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_FormBuilder_CustomTableCreationGridView_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_FormBuilder_CustomTableCreationGridView_Futures]
GO

/****** Object:  StoredProcedure [dbo].[Pr_FormBuilder_CustomTableCreationGridView_Futures]    Script Date: 6/9/2016 8:51:21 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Pr_FormBuilder_CustomTableCreationGridView_Futures]                                                  
@TableName varchar(500),                                                  
@FieldName varchar(50),                                                  
@DataType int,                              
--@Predefined int,                              
@FieldId int                                                  
as                                                  
begin                                                 
         
                                               
 Declare @DataTypeValue varchar(100)        
                                   
                                                
 if (@DataType=1)                                                
 Begin                                                
        set @DataTypeValue='varchar(1000)'                                                
 end                                                
 else if(@DataType=2)                                                
 begin                                                
        set @DataTypeValue='Decimal(18,2)';                                                
 end                                                
 else if(@DataType=3 or @DataType=4 or @DataType=9)                                                
 begin                                                
        set @DataTypeValue='int';                                                
 end                                                
 else if(@DataType=5)                                                
 begin                                                
        set @DataTypeValue='DateTime';                                                
 end                                                
 else if(@DataType=6 or @DataType=7)                                                
 begin                                                
        set @DataTypeValue='bit';                                                
 end                                                
 else if(@DataType=8)                                                
 begin                                                
        set @DataTypeValue='varchar(8000)';                                                
 end                                   
 else if(@DataType=10)                                                
 begin                                                
        set @DataTypeValue='varchar(300)';                                                
 end  
 else if (@DataType=14)    
 begin    
  set @DataTypeValue='varchar(10)';    
 end                                             
                                    
if((@DataType<11 or @DataType=14) and @DataType<>9)                               
begin                                                                
 If Exists(Select * from sysobjects  where [Name]='' + @TableName + '' and xtype='U') ---Not for (@ControlID = 9)                                                            
   Begin                                                            
    if not Exists(select * from syscolumns where id=object_id(''+ @TableName +'') and [name]='' + @FieldName + '')                                              
       begin    
   EXEC('ALTER TABLE ['+@TableName +'] ADD  ['+ @FieldName +']  '+@DataTypeValue+' ')      
       end                                                           
   End                     
 else                 
   Begin        
   EXEC('CREATE TABLE ['+@TableName +'] ( ID int IDENTITY(1,1), Ptn_pk int NOT NULL ,Visit_Pk int NOT NULL,                  
   LocationId int NOT NULL,UserId int,SectionId  int not null,FormID  int not null ,CreateDate DateTime,UpdateDate DateTime, ['+ @FieldName +'] '+@DataTypeValue+', Primary Key(ID)) ')                                 
   End                  
         
end                                                                             
End

GO


/****** Object:  StoredProcedure [dbo].[Pr_FormBuilder_SaveMstSection_Futures]    Script Date: 6/9/2016 8:48:56 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_FormBuilder_SaveMstSection_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_FormBuilder_SaveMstSection_Futures]
GO

/****** Object:  StoredProcedure [dbo].[Pr_FormBuilder_SaveMstSection_Futures]    Script Date: 6/9/2016 8:48:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[Pr_FormBuilder_SaveMstSection_Futures]        
@SectionId int,        
@SectionName varchar(50),        
@Seq int,        
@CustomFlag int,        
@DeleteFlag int,        
@UserId int,        
@FeatureId int,  
@IsGridView int=0    ,
@SectionInfo varchar(255) = null     
as        
begin        
Declare @iSectionId as int        
if exists(select * from mst_section where sectionId=@SectionId)          
 begin          
 --select 'a'        
	Update mst_section Set
		SectionName = @SectionName, SectionInfo = @SectionInfo, Seq = @Seq, CustomFlag = @CustomFlag, DeleteFlag = @DeleteFlag,
		UserId = @UserId, FeatureId = @FeatureId, UpdateDate = getdate(), IsGridView = @IsGridView
	Where SectionId = @SectionId      
        
  set @iSectionId=@SectionId        
 end          
else          
begin          
 --select 'B'        
Insert Into mst_Section (
	SectionName
	,SectionInfo
	,Seq
	,CustomFlag
	,DeleteFlag
	,UserId
	,CreateDate
	,FeatureId
	,IsGridView)
Values (
	@SectionName
	,@SectionInfo
	,@Seq
	,@CustomFlag
	,@DeleteFlag
	,@UserId
	,getdate()
	,@FeatureId
	,@IsGridView)         
         
  set @iSectionId= scope_identity();          
 end           
 select @iSectionId        
end

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_FormBuilder_SaveUpdateFormBusinessRules]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_FormBuilder_SaveUpdateFormBusinessRules]
GO

/****** Object:  StoredProcedure [dbo].[pr_FormBuilder_SaveUpdateFormBusinessRules]    Script Date: 6/9/2016 8:48:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[pr_FormBuilder_SaveUpdateFormBusinessRules]       
(          
@FeatureId int,          
@BusRuleid int,          
@value varchar(10) =null,          
@value1 varchar(10) =null,          
@setType int=null,          
@UserID int=null,          
@counter int=null          
)          
as          
begin
          
if @counter=0          
begin
Delete From lnk_featureBusinessRule
Where FeatureID = @FeatureId
End
Insert Into lnk_featureBusinessRule (
		FeatureID
	,	BusRuleId
	,	Value
	,	Value1
	,	SetType
	,	CreateDate
	,	UserId)
Values (
		@FeatureId
	,	@BusRuleid
	,	nullif(@value, '')
	,	nullif(@value1, '')
	,	@setType
	,	getdate()
	,	@userid)

End
GO

/****** Object:  StoredProcedure [dbo].[pr_FormBuilder_DeleteFormBusinessRules]    Script Date: 6/9/2016 8:48:08 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_FormBuilder_DeleteFormBusinessRules]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_FormBuilder_DeleteFormBusinessRules]
GO

/****** Object:  StoredProcedure [dbo].[pr_FormBuilder_DeleteFormBusinessRules]    Script Date: 6/9/2016 8:48:08 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[pr_FormBuilder_DeleteFormBusinessRules]       
(          
@FeatureId int,          
@BusRuleid int,          
@value int =null,          
@value1 int =null,          
@setType int=null,          
@UserID int=null,          
@counter int=null          
)          
as          
begin          
if @counter=0          
begin          
 delete from lnk_featureBusinessRule where FeatureID=@FeatureId          
end   
end


GO


/****** Object:  StoredProcedure [dbo].[Pr_FormBuilder_SaveMstFeature_Futures]    Script Date: 6/9/2016 8:47:46 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_FormBuilder_SaveMstFeature_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_FormBuilder_SaveMstFeature_Futures]
GO

/****** Object:  StoredProcedure [dbo].[Pr_FormBuilder_SaveMstFeature_Futures]    Script Date: 6/9/2016 8:47:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Pr_FormBuilder_SaveMstFeature_Futures]                                      
@FeatureId int=null,                                      
@FeatureName varchar(50),
@FormDescription varchar(100) = null,
@ReferenceId varchar(50) =null,                                      
@ReportFlag int=null,                                      
@DeleteFlag int=null,                                      
@AdminFlag int=null,                                      
@OptionalFlag int=null,                                      
@SystemId int=null,                                      
@UserId int=null,                                      
@Published int=null,                                      
@CountryId int=null,                                      
@ModuleId int=null,                  
@MultiVisit int=null                                   
as                                             
Begin                                      
  Declare @iFeatureId as int                                    
  Declare @strOldFeatureName as varchar(50)                                  
  Declare @MaxFeatureId as int                          
  Declare @MaxVisitTypeId as int , @featureTypeId int                        
  Select Top 1 @featureTypeId = Id From mst_Decode Where Code='CLINICAL_FORM' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)      
  Select @FormDescription = Isnull(@FormDescription, @FeatureName);                   
If(@FeatureId=0) Begin                             
  Select @MaxFeatureId=max(FeatureId) from mst_feature                           
                          
  If(@MaxFeatureId>1000)  Begin                          
		Insert Into mst_Feature (
				FeatureName
			,	ReferenceId
			,	ReportFlag
			,	DeleteFlag
			,	AdminFlag
			,	SystemId
			,	UserID
			,	Published
			,	ModuleId
			,	CountryId
			,	CreateDate
			,	MultiVisit
			,	FeatureTypeId)
		Values (
				@FeatureName
			,	@ReferenceId
			,	@ReportFlag
			,	@DeleteFlag
			,	@AdminFlag
			,	@SystemId
			,	@UserId
			,	0
			,	@ModuleId
			,	@CountryId
			,	getdate()
			,	@MultiVisit
			,	@featureTypeId)                 
		 set @iFeatureId= scope_identity()                                       
    End                          
	Else Begin                          
		SET IDENTITY_INSERT [dbo].[mst_feature] ON                          
		Insert Into mst_Feature (
				FeatureID
			,	FeatureName
			,	ReferenceId
			,	ReportFlag
			,	DeleteFlag
			,	AdminFlag
			,	SystemId
			,	UserID
			,	Published
			,	ModuleId
			,	CountryId
			,	CreateDate
			,	MultiVisit
			,	FeatureTypeId)
		Values (
				1001
			,	@FeatureName
			,	@ReferenceId
			,	@ReportFlag
			,	@DeleteFlag
			,	@AdminFlag
			,	@SystemId
			,	@UserId
			,	0
			,	@ModuleId
			,	@CountryId
			,	getdate()
			,	@MultiVisit
			,	@featureTypeId)                           
		SET IDENTITY_INSERT [dbo].[mst_feature] OFF                     
		Set @iFeatureId= 1001                            
	End                          
                          
                          
  --entry in visit type too                                  
	Select @MaxVisitTypeId=max(VisitTypeId) From mst_visitType                          
	If(@MaxVisitTypeId>100)  Begin                          
		Insert Into mst_VisitType (
				VisitName
			,	FormDescription
			,	DeleteFlag
			,	UserID
			,	CreateDate
			,	SystemId
			,	FeatureId)
		Values (
				@FeatureName
			,	@FormDescription
			,	@DeleteFlag
			,	@UserId
			,	getdate()
			,	@SystemId
			,	@iFeatureId)                              
	End                          
	Else Begin                          
		SET IDENTITY_INSERT [dbo].[mst_VisitType] ON                          
		Insert Into mst_VisitType (
				VisitTypeID
			,	VisitName
			,	FormDescription
			,	DeleteFlag
			,	UserID
			,	CreateDate
			,	SystemId)
		Values (
				101
			,	@FeatureName
			,	@FormDescription
			,	@DeleteFlag
			,	@UserId
			,	getdate()
			,	@SystemId)                        
		SET IDENTITY_INSERT [dbo].[mst_VisitType] OFF                          
	End                          
            
 insert into lnk_groupFeatures (GroupId,FeatureId,FunctionId,createDate)                            
 values(1,@iFeatureId,1,getdate())                               
 insert into lnk_groupFeatures (GroupId,FeatureId,FunctionId,createDate)                            
 values(1,@iFeatureId,2,getdate())                            
 insert into lnk_groupFeatures (GroupId,FeatureId,FunctionId,createDate)             
 values(1,@iFeatureId,3,getdate())                            
 insert into lnk_groupFeatures (GroupId,FeatureId,FunctionId,createDate)                            
 values(1,@iFeatureId,4,getdate())                            
 insert into lnk_groupFeatures (GroupId,FeatureId,FunctionId,createDate)                            
 values(1,@iFeatureId,5,getdate())                            
 end                
else                                      
 begin                                      
 --store old feature name in variable to modify visit type according to old feature name                                  
 select @strOldFeatureName=FeatureName from mst_feature where  featureId=@FeatureId                                   
  if (@FeatureId = 126) Begin
	Update mst_Feature Set
		--	FeatureName = @FeatureName
			ReportFlag = @ReportFlag
		,	ReferenceId = isnull(ReferenceId, @ReferenceId)
		,	DeleteFlag = @DeleteFlag
		,	AdminFlag = @AdminFlag
		,	OptionalFlag = @OptionalFlag
		,	SystemId = 0
		,	UserID = @UserId
		,	ModuleId = @ModuleId
		,	UpdateDate = getdate()
		,	MultiVisit = @MultiVisit
	Where (FeatureID = @FeatureId)
  End
  else  Begin
	Update mst_Feature Set
		--	FeatureName = @FeatureName
			ReferenceId = isnull(ReferenceId, @ReferenceId)
		,	ReportFlag = @ReportFlag
		,	DeleteFlag = @DeleteFlag
		,	AdminFlag = @AdminFlag
		,	OptionalFlag = @OptionalFlag
		,	SystemId = @SystemId
		,	UserID = @UserId
		,	ModuleId = @ModuleId
		,	UpdateDate = getdate()
		,	MultiVisit = @MultiVisit
		,	FeatureTypeId = isnull(FeatureTypeId, @featureTypeId)
	Where (FeatureID = @FeatureId)
  End                     
--entry in visit type                                  
		Update mst_VisitType Set
				FormDescription = Isnull(@FormDescription,VisitName)
			,	UserID = @UserId
			,	UpdateDate = getdate()
			,	SystemId = @SystemId
			,	FeatureId = @FeatureId
		Where --(VisitName = @strOldFeatureName)And
		 (VisitTypeID > 100)
		And (FeatureId = @FeatureId)                        
 set @iFeatureId=@FeatureId                                    
 end                                      
select @iFeatureId   FeatuerId     
Return (@iFeatureId)                            
end



GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_PMTCT_SaveUpdateConditionalCustomFields_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_PMTCT_SaveUpdateConditionalCustomFields_Futures]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Pr_PMTCT_SaveUpdateConditionalCustomFields_Futures]      (                                                                                
	@ConFieldId int				=null,                                                                                      
	@FieldName varchar(50)		=null,                                                                                      
	@ControlId int				=null,                                                                                      
	@DeleteFlag int				=null,                                                                                      
	@UserID int					=null,                                                                
	@flag int					=null,                                                                                      
	@SelectList xml				=null,                                                                                
	@Predefined int				=null,          
	@ConFieldLabel varchar(100),          
	@ConSeq int,                                                                    
	@ConPredefined int,    
	@SystemId int          
)                                                                        
As                                                                                       
Begin---0     
	declare @Modcodeid int                                                                          
	declare @tempSelectListCount int                                                                           
	declare @tempSelectlistvalue  varchar(1000)                                                                                         
	declare @Index int           
	declare @BindTable varchar(300)                                    
	declare @Tsql varchar(8000)                  
	declare @FormFieldId int
	Select @BindTable = Case
							When @ControlId = 6 Then 'Mst_YesNo'
							When @ControlId In (4, 9) Then 'Mst_ModDecode'
							Else ''
						End


	If @flag = 0 Begin    --insert customform field using manage field form.   ---1  
		Insert Into mst_ModCode (
				Name
			,	DeleteFlag
			,	Predefined
			,	UserID
			,	CreateDate)
		Values (
				@FieldName
			,	0
			,	@Predefined
			,	@UserID
			,	getdate())
		Set @Modcodeid = scope_identity();    
		Insert Into mst_customformfield (
				FieldName
			,	FieldDesc
			,	ControlID
			,	DeleteFlag
			,	UserID
			,	CreateDate
			,	CareEnd
			,	BindTable
			,	CategoryId)
		Values (
				@FieldName
			,	@FieldName
			,	@ControlID
			,	@DeleteFlag
			,	@UserID
			,	getdate()
			,	0
			,	@BindTable
			,	@Modcodeid);
		Set @FormFieldId = scope_identity();
		If (@SelectList Is Not Null) Begin---2 
				Insert Into mst_ModDeCode (
						Name
					,	CodeID
					,	SRNo
					,	UserID
					,	CreateDate
					,	SystemID
					,	DeleteFlag) 
				SELECT	L.G.value('.', 'varchar(250)')	Name
					,	@Modcodeid
					,	row_number() OVER (ORDER BY G)	SrNo
					,	@UserId
					,	getdate()
					,	@SystemId
					,	0
				FROM @SelectList.nodes('//option') AS L (G);				 
			
			If (@ControlId = 9 And @Predefined = 0) Begin----3                                        
				Set @Tsql = 'if not exists(select name from sysobjects where name = ''dtl_FB_' + @FieldName + ''' and type = ''U'')                       
				   CREATE TABLE [dtl_FB_' + @FieldName + '] ( ID int IDENTITY(1,1), Ptn_pk int NOT NULL ,' + @FieldName + ' varchar(50),                              
					 Visit_Pk int NOT NULL,LocationId int NOT NULL,UserId int ,CreateDate DateTime,UpdateDate DateTime )'
				Exec (@Tsql)
			End------3     
		End---2                                                            
	End---1                  
	If @flag = 1 Begin       --insert customform field using manage field form.   ---1   
		Declare @OldchkFieldName varchar(500)
		Declare @ChkCatId int
		Select	@OldchkFieldName = Fieldname,@ChkCatId = CategoryId From mst_customformfield Where Id = @ConFieldId And ControlId = @ControlId

		If (@SelectList Is Not Null) Begin---2  
			  
			If Exists (Select * From mst_ModCode Where CodeID = @ChkCatId And predefined = 0) Begin
				;
				WITH vals	AS (
					SELECT	L.G.value('.', 'varchar(250)')	Name
						,	@ChkCatId						CodeId
						,	row_number() OVER (ORDER BY G)	SrNo
						,	@UserId							UserId
					FROM @SelectList.nodes('//option') AS L (G)
				)
				INSERT INTO mst_ModDeCode (
					Name
				,	CodeId
				,	SRNo
				,	UserID
				,	CreateDate
				,	SystemId
				,	DeleteFlag)
				SELECT	Name
					,	CodeId
					,	SrNo
					,	UserId
					,	getdate()
					,	@SystemId
					,	0
				FROM vals V
				WHERE NOT EXISTS (	SELECT 1 FROM mst_ModDeCode I	WHERE I.Name = V.Name AND I.CodeId = V.CodeId );

			
			End
			If (@ControlID = 9 And @Predefined = 0) Begin----3                                        
				Set @Tsql = 'if not exists(select name from sysobjects where name = ''dtl_FB_' + @FieldName + ''' and type = ''U'')                       
				   CREATE TABLE [dtl_FB_' + @FieldName + '] ( ID int IDENTITY(1,1), Ptn_pk int NOT NULL ,' + @FieldName + ' varchar(50),                              
					 Visit_Pk int NOT NULL,LocationId int NOT NULL,UserId int ,CreateDate DateTime,UpdateDate DateTime )'
				Exec (@Tsql)
			End------3           
		End---2                                                            
	End---1  
	Return (@FormFieldId);
End----0
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_PMTCT_SaveUpdateCustomFields_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_PMTCT_SaveUpdateCustomFields_Futures]
Go

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Pr_PMTCT_SaveUpdateCustomFields_Futures] (                                                                                                                 
	@Id int					= null,                                                                                                                  
	@FieldName varchar(50)	= null,                                                                                                                  
	@ControlID int			= null,                                                                                                                  
	@DeleteFlag int			= null,                                                                                                                  
	@UserID int				= null,                                                                        
	@CareEnd int 			= null,                                                                                                                  
	@flag int				= null,                                                                                                                  
	@SelectList xml			= null,                                                                                                            
	@Predefined int			= null,                                                                                                
	@SystemID int			= null  )                                                                                                       
As       
Begin                                                                                                             
	declare @FormFieldID int                                                                                                        
	declare @pmtctcodeid int                                                                              
	declare @Modcodeid int                                                                                                     
	declare @tempSelectListCount int                                                                                                       
	declare @tempSelectlistvalue  varchar(1000)                                                                                                                     
	declare @Index int                                                                                                      
	declare @PMTCodeID int                                                                                            
	declare @maxpmtctcodeid int                                                      
	declare @BindTable varchar(300)
	declare @CategoryId int;                                                                
	declare @Tsql varchar(8000)    
	declare @delvalue varchar(max)  

	
		Select @ID = Case 
				When charindex('8888',@Id) > 0 Then replace(@Id,'8888','')                                                                                                                 
                When charindex('9999',@Id) > 0 Then replace(@Id,'9999','') 
				Else @Id End;


		Select @BindTable = nullif(LookupTable,'') From mst_control Where ControlID=  @ControlId;

		Select @BindTable = Case When @BindTable Is null And @ControlId  = 6 Then 'Mst_YesNo'
								When @BindTable Is null And @ControlId  In (4,9,22) Then 'Mst_ModDecode'
								Else @BindTable End
	
	  --delete                                 
		If(@flag=9) Begin                                                                                                
			declare @tmpCodeID int
			Delete From Lnk_FieldsBusinessRule	Where Fieldid = @ID		And Predefined = @Predefined
			If @Predefined = 1 
				Select @tmpCodeID = CategoryId,@CategoryId = CategoryId	From Mst_PreDefinedFields	Where ID = @ID 
			Else 
				Select @tmpCodeID = CategoryId,@CategoryId = CategoryId	From mst_CustomformField	Where ID = @ID
	
			Update mst_Modcode	Set DeleteFlag = 1	Where CodeId = @tmpCodeID	And Predefined = @Predefined;
		End                                                                                                      
		--Insert customform field using manage field form.                                                                                                                
		If @flag=0  Begin                                                                 
			If(@SelectList Is Not Null)  Begin  
				Insert Into mst_ModCode (
					Name
					,DeleteFlag
					,Predefined
					,UserID
					,CreateDate
				)
				Values (
					@FieldName
					,0
					,@Predefined
					,@UserID
					,getdate()
				);                     
			
				Set @Modcodeid =scope_identity()  ;
				INSERT INTO mst_ModDeCode(	
					Name
				,	CodeID
				,	SRNo
				,	UserID
				,	CreateDate
				,	SystemId
				,	DeleteFlag
				)
				SELECT	L.G.value('.', 'varchar(250)')	Name
					,	@Modcodeid
					,	row_number() OVER (ORDER BY G)	SrNo
					,	@UserId
					,	getdate()
					,	@SystemId
					,	0
				FROM @SelectList.nodes('//option') AS L (G);	           
			End                                                      
			If ( @ControlID = 15)  Begin                           
				INSERT INTO mst_CustomformField (
					FieldName
				,	FieldDesc
				,	ControlId
				,	DeleteFlag
				,	UserId
				,	CreateDate
				,	CareEnd
				,	BindTable)
				VALUES (
						@FieldName
					,	@FieldName
					,	@ControlID
					,	@DeleteFlag
					,	@UserID
					,	getdate()
					,	0
					,	'VWDiseaseSymptom');         
				Set @FormFieldID=scope_identity() ;                                 
			End                          
			Else Begin                          
				INSERT INTO mst_CustomformField (
					FieldName
				,	FieldDesc
				,	ControlId
				,	DeleteFlag
				,	UserId
				,	CreateDate
				,	CareEnd
				,	BindTable
				,	CategoryId)
				VALUES (
						@FieldName
					,	@FieldName
					,	@ControlID
					,	@DeleteFlag
					,	@UserID
					,	getdate()
					,	0
					,	@BindTable
					,	@Modcodeid)                                                           
				Set @FormFieldID=scope_identity()               
			End                          
			Delete From Lnk_FieldsBusinessRule where Fieldid=@FormFieldID and Predefined=0                                      
			If(@ControlID=9 and @Predefined=0) Begin                                                                    
			   Set @Tsql = 'If not Exists(Select name From sysobjects where name = ''dtl_FB_'+@FieldName+''' and type = ''U'')                                                   
				  CREATE TABLE [dtl_FB_'+@FieldName +'] ( ID int IDENTITY(1,1), Ptn_pk int NOT NULL ,['+@FieldName+'] varchar(50),                                                          
					Visit_Pk int NOT NULL,LocationId int NOT NULL,UserId int ,CreateDate DateTime,UpdateDate DateTime, DateField1 DATETIME NULL, DateField2 DATETIME NULL, NumericField INT NULL)'
			   Exec(@Tsql)  ; 
			End                            
			Else If (@ControlID=15 and @Predefined=0) Begin                                                                    
				Set @Tsql = 'If not Exists(Select name From sysobjects where name = ''dtl_FBDiseaseSymptom_'+@FieldName+''' and type = ''U'')                                                   
							CREATE TABLE [dtl_FB_'+@FieldName +'] ( ID int IDENTITY(1,1), Ptn_pk int NOT NULL ,'+@FieldName+' varchar(50),                                                          
							Visit_Pk int NOT NULL,LocationId int NOT NULL, ICDCodeId varchar(100), Other Varchar(100), UserId int ,CreateDate DateTime,UpdateDate DateTime )'                                                  
				Exec(@Tsql)                                                  
			End                                                                                                         
		End 
		--Insert customform field using ManagePatientRegistrationField form.                                  
		If @flag=2  Begin                                                                 
			If(@SelectList Is Not Null)  Begin                                                      
				INSERT INTO mst_ModCode (
					Name
				,	DeleteFlag
				,	Predefined
				,	UserID
				,	CreateDate)
				VALUES (
						@FieldName
					,	0
					,	@Predefined
					,	@UserID
					,	getdate())                                                                   
				Set @Modcodeid =scope_identity() ;
				INSERT INTO mst_ModDeCode (
					Name
				,	CodeID
				,	SRNo
				,	UserID
				,	CreateDate
				,	SystemId
				,	DeleteFlag)
				SELECT	L.G.value('.', 'varchar(250)')	Name
					,	@Modcodeid
					,	row_number() OVER (ORDER BY G)	SrNo
					,	@UserId
					,	getdate()
					,	@SystemId
					,	0
				FROM @SelectList.nodes('//option') AS L (G);                                                        
			End                                       
			INSERT INTO mst_CustomformField (
				FieldName
			,	FieldDesc
			,	ControlId
			,	DeleteFlag
			,	UserId
			,	CreateDate
			,	PatientRegistration
			,	BindTable
			,	CategoryId)
			VALUES (
					@FieldName
				,	@FieldName
				,	@ControlID
				,	@DeleteFlag
				,	@UserID
				,	getdate()
				,	1
				,	@BindTable
				,	@Modcodeid)  ;        
			Set @FormFieldID=scope_identity()                                                                                                                   
			Delete From Lnk_FieldsBusinessRule where Fieldid=@FormFieldID and Predefined=0 			
			If(@ControlID=9 and @Predefined=0) Begin                                            
				Set @Tsql = 'If not Exists(Select name From sysobjects where name = ''dtl_FB_'+@FieldName+''' and type = ''U'')                                                   
					CREATE TABLE [dtl_FB_'+@FieldName +'] ( ID int IDENTITY(1,1), Ptn_pk int NOT NULL ,['+@FieldName+'] varchar(50),                                                          
					Visit_Pk int NOT NULL,LocationId int NOT NULL,UserId int ,CreateDate DateTime,UpdateDate DateTime )'                                                  
				Exec(@Tsql)                                                  
			End                                                                                                         
		End 
			--Insert customform field using ManageCareEndField form. 
		Else If @flag=3 Begin                                                                             
			INSERT INTO mst_ModCode (
				Name
			,	DeleteFlag
			,	Predefined
			,	UserID
			,	CreateDate)
			VALUES (
					@FieldName
				,	0
				,	@Predefined
				,	@UserID
				,	getdate());
			Set @Modcodeid =scope_identity()                                                         
			If(@SelectList is Not Null) Begin                                                                                                      
				INSERT INTO mst_ModDeCode (
					Name
				,	CodeID
				,	SRNo
				,	UserID
				,	CreateDate
				,	SystemId
				,	DeleteFlag)
				SELECT	L.G.value('.', 'varchar(250)')	Name
					,	@Modcodeid
					,	row_number() OVER (ORDER BY G)	SrNo
					,	@UserId
					,	getdate()
					,	@SystemId
					,	0
				FROM @SelectList.nodes('//option') AS L (G);                                                                           
			End                                             
			INSERT INTO mst_customformfield (
				FieldName
			,	FieldDesc
			,	ControlID
			,	DeleteFlag
			,	UserID
			,	CreateDate
			,	CareEnd
			,	BindTable
			,	CategoryId)
			VALUES (
					@FieldName
				,	@FieldName
				,	@ControlID
				,	@DeleteFlag
				,	@UserID
				,	getdate()
				,	1
				,	@BindTable
				,	@Modcodeid);
			Set @FormFieldID=scope_identity() 
		
			Delete From Lnk_FieldsBusinessRule where Fieldid=@FormFieldID and Predefined=0             
			If(@ControlID=9 and @Predefined=0)   Begin                                                            
				  Set @Tsql = 'If not Exists(Select name From sysobjects where name = ''dtl_FB_'+@FieldName+''' and type = ''U'')                                                   
				  CREATE TABLE [dtl_FB_'+@FieldName +'] ( ID int IDENTITY(1,1), Ptn_pk int NOT NULL ,['+@FieldName+'] varchar(50),                                                          
					CareEndedId int NOT NULL,LocationId int NOT NULL,UserId int ,CreateDate DateTime,UpdateDate DateTime )'   
				  Exec(@Tsql) ;                                                                                                 
			End                                                                    
		End                                                                        
			--updated manageformfelds and careEnded form fields                                             
		If @flag=4  Begin                                                                                                    
			update mst_customformfield Set DeleteFlag=@DeleteFlag where id=@ID                                                                                                     
			Set @FormFieldID=@ID                                                                                                       
		End                  
		If (@flag=1 and @Predefined=0) Begin                                                                                                                  
			declare @OldFieldName varchar(500)                                                                                                        
			declare @CatId int                                                 
			Select @OldFieldName=Fieldname,@CatId = CategoryId, @Modcodeid= CategoryId From mst_customformfield where id=@ID;
			If(@SelectList Is Not Null)  Begin                                        
				If Not Exists(Select * From mst_ModCode where CodeID=@CatId and predefined=0)  Begin    
					INSERT INTO mst_ModCode (
							Name
						,	DeleteFlag
						,	Predefined
						,	UserID
						,	CreateDate)
					VALUES (
							@FieldName
						,	0
						,	@Predefined
						,	@UserID
						,	getdate()) ;                                                                
					Set @Modcodeid =scope_identity()
				End

				Update mst_ModDeCode Set DeleteFlag= 1 Where CodeID = @Modcodeid;
				
				; WITH vals	AS (
					SELECT	L.G.value('.', 'varchar(250)')	Name
						,	@Modcodeid						CodeId
						,	row_number() OVER (ORDER BY G)	SrNo
						,	@UserId							UserId
					FROM @SelectList.nodes('//option') AS L (G)
				)
				Update D Set	D.SRNo = V.SrNo
					, DeleteFlag = 0
				From mst_ModDeCode D
				Inner Join Vals V On V.CodeId = D.CodeId
				And V.Name = D.Name;
					
				;WITH vInsert	AS (
					SELECT	L.G.value('.', 'varchar(250)')	Name
						,	@Modcodeid						CodeId
						,	row_number() OVER (ORDER BY G)	SrNo
						,	@UserId							UserId
					FROM @SelectList.nodes('//option') AS L (G)
				)
				INSERT INTO mst_ModDeCode (
					Name
				,	CodeId
				,	SRNo
				,	UserID
				,	CreateDate
				,	SystemId
				,	DeleteFlag)
				SELECT	Name
					,	CodeId
					,	SrNo
					,	UserId
					,	getdate()
					,	@SystemId
					,	0
				FROM vInsert V
				WHERE NOT EXISTS (	SELECT 1 FROM mst_ModDeCode I	WHERE I.Name = V.Name AND I.CodeId = V.CodeId );			 
				                                                                                          
			End                                          
			Update mst_CustomformField SET
					FieldName = @FieldName
				,	FieldDesc = @FieldName
				,	ControlId = @ControlID
				,	DeleteFlag = @DeleteFlag
				,	BindTable = @BindTable
				,	CategoryId = @CatId
				,	UserId = @UserID
				,	UpdateDate = getdate()
			WHERE (Id = @ID)    ; 
		                          
			Update mst_Modcode SET
				DeleteFlag = @DeleteFlag
			WHERE CodeId = @CatId;

			Update Mst_ModDecode SET
				DeleteFlag = @DeleteFlag
			WHERE CodeId = @CatId And @DeleteFlag = 1;	                   
		End                                                   
                                                                                            
		                      
                                           
		If(@ControlID=9 and @Predefined=0 and @CareEnd=1 )  Begin                                          
			Set @Tsql = 'If not Exists(Select name From sysobjects where name = ''dtl_FB_'+@FieldName+''' and type = ''U'')                                       
				CREATE TABLE [dtl_FB_'+@FieldName +'] ( ID int IDENTITY(1,1), Ptn_pk int NOT NULL ,['+@FieldName+'] varchar(50),                                                          
				CareEndedId int NOT NULL,LocationId int NOT NULL,UserId int ,CreateDate DateTime,UpdateDate DateTime )' 
			exec(@Tsql)                                                                                               
		End                                                        
		If(@ControlID=9 and @Predefined=0 and @CareEnd=0) Begin                                                       
			Set @Tsql = 'If not Exists(Select name From sysobjects where name = ''dtl_FB_'+@FieldName+''' and type = ''U'')                                                   
				CREATE TABLE [dtl_FB_'+@FieldName +'] ( ID int IDENTITY(1,1), Ptn_pk int NOT NULL ,['+@FieldName+'] varchar(50),                                                          
				Visit_Pk int NOT NULL,LocationId int NOT NULL,UserId int ,CreateDate DateTime,UpdateDate DateTime )'  
			exec(@Tsql)                                                                                            
		End                                                                                                         
		Set @FormFieldID=@ID                                                                                                                  
		
 --@Select list End     
                                        
		delete From dbo.lnk_conditionalfields where ConFieldId = @FormFieldId                                                                                    
------------------------------------------------------------------------                                                                
		If (@flag=5 and @Predefined=0)  Begin                                                                                                                  
			declare @OldchkFieldName varchar(500)                                                                                             
			declare @ChkCatId int                                                 
			Select @OldchkFieldName=Fieldname,@ChkCatId = CategoryId, @Modcodeid= CategoryId From mst_customformfield where id=@ID    
			
			If(@SelectList is Not NULL)   Begin                                                                                                                  
				If Exists(Select * From mst_ModCode where CodeID=@ChkCatId and predefined=0) Begin  
					WITH vals	AS (
						SELECT	L.G.value('.', 'varchar(250)')	Name
							,	@ChkCatId						CodeId
							,	row_number() OVER (ORDER BY G)	SrNo
							,	@UserId							UserId
						FROM @SelectList.nodes('//option') AS L (G)
					)
					INSERT INTO mst_ModDeCode (
						Name
					,	CodeId
					,	SRNo
					,	UserID
					,	CreateDate
					,	SystemId
					,	DeleteFlag)
					SELECT	Name
						,	CodeId
						,	SrNo
						,	UserId
						,	getdate()
						,	@SystemId
						,	0
					FROM vals V
					WHERE NOT EXISTS (	SELECT 1 FROM mst_ModDeCode I	WHERE I.Name = V.Name AND I.CodeId = V.CodeId );
				End  
				Update mst_CustomformField Set BindTable = @BindTable Where Id=@Id;                                
			End                                            
			If(@ControlID=9 and @Predefined=0 and @CareEnd=0) Begin                            
			   Set @Tsql = 'If not Exists(Select name From sysobjects where name = ''dtl_FB_'+@FieldName+''' and type = ''U'')                                                   
			   CREATE TABLE [dtl_FB_'+@FieldName +'] ( ID int IDENTITY(1,1), Ptn_pk int NOT NULL ,['+@FieldName+'] varchar(50),                                                          
			   Visit_Pk int NOT NULL,LocationId int NOT NULL,UserId int ,CreateDate DateTime,UpdateDate DateTime )'  
				exec(@Tsql)                                                                                            
			End                                                                      
			Set @FormFieldID=@ID                                                                                                                  
		End                                                  
		If(@flag=1 and @Predefined=1)Begin      
			Set @FormFieldID=@ID       
		 End      
                                                                                
		Select @FormFieldID[FormFieldID]                                                                                  
        RETURN @FormFieldID                                   
                                                                
End


GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_PMTCT_GetCustomFields_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_PMTCT_GetCustomFields_Futures]
Go
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Pr_PMTCT_GetCustomFields_Futures]                                                                                                        
(                                                                                                        
 @FieldName varchar(50)=null ,                                                                                              
 @ModuleId int,                                                                                         
 @flag int,                                
 @isGridView int =null ,
 @SystemId int =0                                                                                                       
)                                                                                                        
as  
Begin
                                                                                                      
	declare @sql varchar(5000)                                                     
                                                                                        
	If @flag = 1  Begin
		Set @sql = 'select ''8888''+convert(varchar,c.ID)[ID],c.fieldname,c.fieldDesc,c.controlID, c.CategoryId CodeId, C.BindTable, dbo.GetLookupValues(CategoryId, BindTable,0)LookupValues,
				l.Name,L.referenceId,0[ModuleId],c.userID,u.UserName,0[Predefine],                                                                      
			c.DeleteFlag,CONVERT(VARCHAR, c.UpdateDate,103) AS [UpdateDate],                                                                      
			(select isnull(count(fieldid),0) from lnk_careendconditionalfields where confieldid = c.Id and ConPredefine =0)[ConditionalField]                                                                       
			from mst_customformfield c inner join mst_control l on l.controlid=c.controlid                                                                                                        
			inner join mst_user u on c.userid=u.userid and c.CareEnd = 1 and c.PatientRegistration IS NULL '
		If @FieldName <> '' Begin
			Set @sql = @sql + ' where c.FieldName like' + '''' + '%' + @FieldName + '%' + ''' and (c.Deleteflag IS NULL or c.Deleteflag=0)'
		End
		Set @sql = @sql + ' union   all                                                                                                      
				select ''9999''+convert(varchar,p.ID)[ID],p.PDFName[fieldname],''''[fieldDesc],p.controlID, p.CategoryId CodeId, p.BindTable,dbo.GetLookupValues(CategoryId, BindTable,0)LookupValues,l.Name,L.referenceId,p.ModuleId[ModuleId],p.userid,''''[UserName],1[Predefine],                                                                      
				0[DeleteFlag],CONVERT(VARCHAR, p.UpdateDate,103) AS [UpdateDate],                                                                      
				(select isnull(count(fieldid),0) from lnk_careendconditionalfields where confieldid = p.Id and ConPredefine =1)[ConditionalField]                                                                       
				from Mst_PreDefinedFields p  inner join mst_control l on l.controlid=p.controlid and p.PatientRegistration IS NULL'
		If @FieldName <> '' Begin
			Set @sql = @sql + ' where p.PDFName like' + '''' + '%' + @FieldName + '%' + ''' '
			--and (p.Deleteflag IS NULL or p.Deleteflag=0)                           
		End
		If @FieldName <> '' And @ModuleId = 0 Begin
			Set @sql = @sql + ' and p.Moduleid=' + convert(varchar, @ModuleId)
		End 
		Else If @ModuleId = 0 Begin
			Set @sql = @sql + ' and p.ModuleId =' + convert(varchar, @ModuleId)
		End
		Set @sql = @sql + ' order by [FieldName]'
	End 
	Else If @flag = 2 Begin
                                                                          
		Set @sql = 'select ''8888''+convert(varchar,c.ID)[ID],c.fieldname,c.fieldDesc,c.controlID, c.CategoryId CodeId, C.BindTable,dbo.GetLookupValues(CategoryId, BindTable,0)LookupValues,l.Name,L.referenceId,0[ModuleId],c.userID,u.UserName,0[Predefine],                                                                      
				c.DeleteFlag,CONVERT(VARCHAR, c.UpdateDate,103) AS [UpdateDate],                                                                      
				(select isnull(count(fieldid),0) from lnk_PatientRegconditionalfields where confieldid = c.Id and ConPredefine =0)[ConditionalField],                        
				c.fieldname[Bindfieldname],''dtl_CustomField''[PDFTableName]                                                                       
				from mst_customformfield c   inner join mst_control l on l.controlid=c.controlid                                                                                                        
				inner join mst_user u on c.userid=u.userid and c.PatientRegistration = 1 where (c.Deleteflag IS NULL or c.Deleteflag=0)  '
		If @FieldName <> '' Begin
			Set @sql = @sql + ' and c.FieldName like' + '''' + '%' + @FieldName + '%' + ''''
		End
		Set @sql = @sql + ' union    all                                                                                                   
				select ''9999''+convert(varchar,p.ID)[ID],p.PDFName[fieldname],''''[fieldDesc],p.controlID, p.CategoryId CodeId, p.BindTable,dbo.GetLookupValues(CategoryId, BindTable,0)LookupValues,l.Name,L.referenceId,p.ModuleId[ModuleId],p.userid,''''[UserName],1[Predefine],                                                                      
				0[DeleteFlag],CONVERT(VARCHAR, p.UpdateDate,103) AS [UpdateDate],                                                                      
				(select isnull(count(fieldid),0) from lnk_PatientRegconditionalfields where confieldid = p.Id and ConPredefine =1)[ConditionalField],                                                                      
				p.bindfield[Bindfieldname], p.PDFTableName[PDFTableName]from Mst_PreDefinedFields p                                                                                                        
				inner join mst_control l on l.controlid=p.controlid and p.PatientRegistration = 1'
		If @FieldName <> '' Begin
			Set @sql = @sql + ' where p.PDFName like' + '''' + '%' + @FieldName + '%' + ''' '
		End
		Set @sql = @sql + ' order by [FieldName]'
	End 
	Else Begin
		Set @sql = 'select ''8888''+convert(varchar,c.ID)[ID],c.fieldname,c.fieldDesc,c.controlID, c.CategoryId CodeId, C.BindTable,dbo.GetLookupValues(CategoryId, BindTable,0)LookupValues,l.Name,L.referenceId,0[ModuleId],c.userID,u.UserName,0[Predefine],                                                                      
				c.DeleteFlag,CONVERT(VARCHAR, c.UpdateDate,103) AS [UpdateDate],                                                                      
				(select isnull(count(fieldid),0) from lnk_conditionalfields where confieldid = c.Id and ConPredefine =0)[ConditionalField]                                                                       
				 from mst_customformfield c inner join mst_control l on l.controlid=c.controlid                                  
				inner join mst_user u on c.userid=u.userid and (c.CareEnd = 0 or c.CareEnd is null) and c.PatientRegistration IS NULL and (c.Deleteflag IS NULL or c.Deleteflag=0) '
		If (@isGridView Is Null) Begin
			If @FieldName <> '' Begin
				Set @sql = @sql + ' where c.FieldName like' + '''' + '%' + @FieldName + '%' + ''' and (c.Deleteflag IS NULL or c.Deleteflag=0 or c.Deleteflag=1)'
			End
			Set @sql = @sql + ' union  all                                                                                                       
							select ''9999''+convert(varchar,p.ID)[ID],p.PDFName[fieldname],''''[fieldDesc],p.controlID, p.CategoryId CodeId, p.BindTable,dbo.GetLookupValues(CategoryId, BindTable,0)LookupValues,l.Name,L.referenceId,p.ModuleId[ModuleId],p.userid,''''[UserName],1[Predefine],                                            
							0[DeleteFlag],CONVERT(VARCHAR, p.UpdateDate,103) AS [UpdateDate],                                                                      
							(select isnull(count(fieldid),0) from lnk_conditionalfields where confieldid = p.Id and ConPredefine =1)[ConditionalField]                                                                       
							from Mst_PreDefinedFields p inner join mst_control l on l.controlid=p.controlid  and p.PatientRegistration IS NULL and (p.Deleteflag IS NULL or p.Deleteflag=0)'
			If @FieldName <> '' Begin
				Set @sql = @sql + ' and p.PDFName like' + '''' + '%' + @FieldName + '%' + ''''
			End
			If @FieldName <> '' And @ModuleId <> 0 Begin
				Set @sql = @sql + ' and p.Moduleid=' + convert(varchar, @ModuleId)
			End 
			Else If @ModuleId <> 0 Begin
				Set @sql = @sql + ' and p.ModuleId =' + convert(varchar, @ModuleId)
			End
			End 
			Else Begin
				Set @sql = @sql + ' where  (select isnull(count(fieldid),0) from lnk_conditionalfields where confieldid = c.Id and ConPredefine =0) =0  and  l.controlid not in(9,13,15,16,12,11) '
			End
			Set @sql = @sql + ' order by [FieldName]'
	End

		--Table 0
	 --Print (@sql);
	 Exec (@sql)

		 --Table 1
	Select	Id
				,Case
					When Predefined = 0 Then '8888' + convert(varchar, FieldId)
					When Predefined = 1 Then '9999' + convert(varchar, FieldId)
				End [FieldId]
				,BusRuleId
				,Value [Value]
				,Predefined
				,UserId
				,CreateDate
				,UpdateDate
				,Value1
		From Lnk_FieldsBusinessRule

	--Tablw 2
	Select 0 FieldID , '' FieldName, '' FieldValue, 0 Predefined, 0 CodeId,'' BindTable, 0 ModuleId Where 1 > 1

	--Table 3
	Select	ControlID
				,Name
				,DataType
				,DeleteFlag
				,UserId
				,CreateDate
				,UpdateDate
				,ReferenceId
				,LookupTable
		From mst_control
		Where (Name <> 'System auto generated');


	--Table 4
		Select	PaddedFieldId	As FieldId
			,	FieldName
			,	BusRuleId
			,	BusRuleName
			,	BusRuleReferenceId
			,	Value
			,	Value1
			,	Predefined
			,	DeleteFlag
	From FieldsBusinessRuleView As BRV
	--Table 5

	Select	Case ConPredefine
					When '1' Then '9999' + convert(varchar, ConfieldId)
					When '0' Then '8888' + convert(varchar, ConfieldId)
				End [ConfieldId]
				,Case Predefined
					When '1' Then '9999' + convert(varchar, FieldId)
					When '0' Then '8888' + convert(varchar, FieldId)
				End [FieldId]
				,[SectionId]
				,[Predefined]
				,[FieldLabel]
				,[UserId]
				,[CreateDate]
				,[UpdateDate]
				,[Seq]
				,[Conpredefine]
		From Lnk_Conditionalfields

	--Table 6
	Select ''  As  LnkFrmFieldID Where 1 > 1  
End
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FormBuilder_GetFieldControlTypes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FormBuilder_GetFieldControlTypes]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 20150623
-- Description:	Get fieldControlTypes
-- =============================================
CREATE PROCEDURE [dbo].[FormBuilder_GetFieldControlTypes] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select	ControlID
			,Name
			,DataType
			,DeleteFlag
			,UserId
			,CreateDate
			,UpdateDate
			,ReferenceId
			,LookupTable
	From mst_control
	Where (Name <> 'System auto generated')
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_FormBuilder_GetModuleIdentificationDetails_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_FormBuilder_GetModuleIdentificationDetails_Constella]
GO

/****** Object:  StoredProcedure [dbo].[pr_FormBuilder_GetModuleIdentificationDetails_Constella]    Script Date: 7/30/2016 5:03:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[pr_FormBuilder_GetModuleIdentificationDetails_Constella]                                              
(                                                  
   @ModuleId int          
                                                
)                                                  
AS                         
                        
                                                                   
Begin

	Select	ControlID
		,	Name
	From mst_control
	Where ControlID In (1, 3, 17)


	If @ModuleId = 0 Begin
			Select	FieldName	[IdentifierName]
				,	Label
				,	0			[ModuleID]
				,	'False'		[Selected]
				,	ID
				,	FieldType 
				,	C.Name FieldTypeName
				,	UpdateFlag
				,	autopopulatenumber
			From Mst_PatientIdentifier P  Inner Join mst_control C on C.ControlID = P.FieldType
	End 
	Else Begin
		Select	tbl1.FieldName	[IdentifierName]
			,	tbl1.Label
			,	tbl2.ModuleID
			,	Selected =	Case isnull(tbl2.ModuleID, 0)
								When 0 Then 'False'
								Else 'True'
							End
			,	[Required] = Case Isnull(tbl2.RequiredFlag,0) When 0 Then 'False'  Else 'True' End
			,	tbl1.ID
			,	tbl1.FieldType 
			,	C.Name FieldTypeName
			,	tbl1.UpdateFlag
			,	tbl1.autopopulatenumber
		From Mst_PatientIdentifier tbl1
		Inner Join mst_control C on C.ControlID = tbl1.FieldType
		Left Outer Join lnk_PatientModuleIdentifier tbl2 On tbl1.Id = tbl2.FieldID
		And tbl2.ModuleID = @ModuleId
		And tbl2.DeleteFlag = 0
	End
	Select	m.ModuleName	As ServiceAreaName
		,	l.BusRuleId
		,	l.Value
		,	l.Value1
		,	l.SetType
	From mst_module As m
	Inner Join lnk_ServiceBusinessRule As l On l.ModuleId = m.ModuleId
	Where (l.ModuleId = @ModuleId)
End
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_FormBuilder_SaveCareEndFeature_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_FormBuilder_SaveCareEndFeature_Futures]
GO

/****** Object:  StoredProcedure [dbo].[Pr_FormBuilder_SaveCareEndFeature_Futures]    Script Date: 7/30/2016 5:13:44 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Pr_FormBuilder_SaveCareEndFeature_Futures]                                          
 @FeatureId int,                                          
 @FeatureName varchar(200),                                         
 @UserId int,                                          
 @ModuleId int,      
 @CountryId varchar(50)                      
                                           
 as                                                 
 Begin                                          
   Declare @iFeatureId as int                                        
   Declare @MaxFeatureId as int               
   Declare @MaxVisitTypeId as int              
   Declare @strFeatureName as varchar(50)                 
   declare @featureTypeId int;
	Select Top 1 @featureTypeId = Id From mst_Decode Where Code='CLINICAL_FORM' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0) 
	     
   if(@FeatureId=0)                  
   Begin                  
   if exists(select featureName from mst_feature where FeatureName = @FeatureName and ModuleId = @ModuleId   
    and (deleteflag =0 or deleteflag is null) and CountryId=@CountryId)                
           begin              
              RaisError('Care End Form for this technical area already exists.', 16, 1)                                          
              return          
           end           
          
  select @MaxFeatureId=max(FeatureId) from mst_feature                               
  if(@MaxFeatureId>1000)                              
     begin                      
          set @MaxFeatureId = @MaxFeatureId+1                      
     end                       
  else                      
     begin                      
        set @MaxFeatureId = 1000+1                      
     end                      
                      
     SET IDENTITY_INSERT [dbo].[mst_feature] ON                              
    Insert Into mst_Feature (
				FeatureID
			,	FeatureName
			,	ReportFlag
			,	DeleteFlag
			,	AdminFlag
			,	SystemId
			,	UserID
			,	Published
			,	ModuleId
			,	CountryId
			,	CreateDate
			,	FeatureTypeId
			,	ReferenceId)
		Values (
				@MaxFeatureId
			,	@FeatureName
			,	0
			,	0
			,	0
			,	0
			,	@UserId
			,	1
			,	@ModuleId
			,	@CountryId
			,	getdate()
			,	@featureTypeId
			,	convert(varchar(36), newid()))                            
     SET IDENTITY_INSERT [dbo].[mst_feature] Off                      
     set @iFeatureId= @MaxFeatureId;                                            
                     
    Insert Into lnk_groupFeatures (
				GroupId
			,	FeatureId
			,	FunctionId
			,	createDate)
		Values (
				1
			,	@iFeatureId
			,	1
			,	getdate())
		Insert Into lnk_groupFeatures (
				GroupId
			,	FeatureId
			,	FunctionId
			,	createDate)
		Values (
				1
			,	@iFeatureId
			,	2
			,	getdate())
		Insert Into lnk_groupFeatures (
				GroupId
			,	FeatureId
			,	FunctionId
			,	createDate)
		Values (
				1
			,	@iFeatureId
			,	3
			,	getdate())
		Insert Into lnk_groupFeatures (
				GroupId
			,	FeatureId
			,	FunctionId
			,	createDate)
		Values (
				1
			,	@iFeatureId
			,	4
			,	getdate())
		Insert Into lnk_groupFeatures (
				GroupId
			,	FeatureId
			,	FunctionId
			,	createDate)
		Values (
				1
			,	@iFeatureId
			,	5
			,	getdate())                      
    end                                         
 else                                          
    begin                
                                          
       Update mst_feature Set
				FeatureName = @FeatureName
			,	UserId = @UserId
			,	CountryId = @CountryId
			,	UpdateDate = getdate()
		Where featureId = @FeatureId
		And CountryId = @CountryId                                            
       set @iFeatureId=@FeatureId               
    end           
                                               
   select @iFeatureId                                              
   End

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServiceArea_GetForms]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ServiceArea_GetForms]
GO

/****** Object:  StoredProcedure [dbo].[ServiceArea_GetForms]    Script Date: 8/1/2016 12:32:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Joseph
-- Create date: 2016 07 18
-- Description:	Get Forms for a service area
-- =============================================
CREATE PROCEDURE [dbo].[ServiceArea_GetForms] 
	-- Add the parameters for the stored procedure here
	@ModuleId int ,
	@LocationId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select	FeatureId
		,	FeatureName
		,	Published
		,	ModuleId
		,	ReferenceId
		,	FormName
		,	FormId
		,	FormDescription
		,	Custom
		,	CategoryId
		,	Code
		,	PermCount
	From ServiceAreaFormView
	Where (ModuleId = @ModuleId)
		And (FeatureDeleteFlag = 0)
		And (Published = 2)
		And (FormDeleteFlag = 0)
END

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_FormBuilder_SaveFeature_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_FormBuilder_SaveFeature_Futures]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[Pr_FormBuilder_SaveFeature_Futures]                            
 @FeatureId int,                            
 @FeatureName varchar(max),                            
 @DeleteFlag int,                            
 @SystemId int,                            
 @UserId int,                            
 @Published int,                            
 @ModuleId int        
                             
 as                                   
 Begin                            
   Declare @iFeatureId as int                          
   Declare @MaxFeatureId as int    
   declare @featureTypeId int;
	Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)               
     
	if(@FeatureId=0)   begin    
		if exists(select featureName from mst_feature where FeatureName = @FeatureName)   Begin 
			RaisError('HomePageType and Technical Area already exists', 16, 1) ;
			Return
		End      
		Select @MaxFeatureId = Case
									When max(FeatureId) > 1000 Then max(FeatureId) + 1
									Else 10001
								End
		From mst_feature 
		SET IDENTITY_INSERT [dbo].[mst_feature] ON                
		Insert Into mst_Feature (
				FeatureID
			,	FeatureName
			,	ReportFlag
			,	DeleteFlag
			,	AdminFlag
			,	SystemId
			,	UserID
			,	Published
			,	ModuleId
			,	CountryId
			,	CreateDate
			,	FeatureTypeId
			,	ReferenceID)
		Values (
				@MaxFeatureId
			,	@FeatureName
			,	0
			,	@DeleteFlag
			,	0
			,	@SystemId
			,	@UserId
			,	1
			,	@ModuleId
			,	Null
			,	getdate()
			,	@featureTypeId
			,	convert(varchar(36), newid()))        
	   SET IDENTITY_INSERT [dbo].[mst_feature] Off        
	   set @iFeatureId= @MaxFeatureId                              
         
	   insert into lnk_groupFeatures (GroupId,FeatureId,FunctionId,createDate)                  
	   values(1,@iFeatureId,1,getdate())                     
	   insert into lnk_groupFeatures (GroupId,FeatureId,FunctionId,createDate)                  
	   values(1,@iFeatureId,2,getdate())                  
	   insert into lnk_groupFeatures (GroupId,FeatureId,FunctionId,createDate)                  
	   values(1,@iFeatureId,3,getdate())                  
	   insert into lnk_groupFeatures (GroupId,FeatureId,FunctionId,createDate)                  
	   values(1,@iFeatureId,4,getdate())                  
	   insert into lnk_groupFeatures (GroupId,FeatureId,FunctionId,createDate)                  
	   values(1,@iFeatureId,5,getdate())          
   End                           
   Else  Begin                            
	Update mst_feature Set
			FeatureName = @FeatureName
		,	DeleteFlag = @DeleteFlag
		,	SystemId = @SystemId
		,	UserId = @UserId
		,	ModuleId = @ModuleId
		,	UpdateDate = getdate()
	Where featureId = @FeatureId
	Set @iFeatureId = @FeatureId         
   End                                  
	 select @iFeatureId                                
 end

GO