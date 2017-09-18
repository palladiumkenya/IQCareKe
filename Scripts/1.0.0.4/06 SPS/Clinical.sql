IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_Clinical_GetModuleFieldNames_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_Clinical_GetModuleFieldNames_Constella]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[Pr_Clinical_GetModuleFieldNames_Constella]                              
(                              
 @patientid int,                                 
 @moduleId int                            
)                              
as                              
Begin
                     
Declare @ModuleName varchar(200)
              
Declare @HIVStartDate datetime
              
Declare @InitialStartDate datetime
              
Declare @ModuleStartDate datetime
     
declare @AutoFieldName varchar(200)
    
declare @sql varchar(500)

Select @AutoFieldName = FieldName From VW_ModuleIdentifiers Where moduleid = @moduleId
Select @ModuleStartDate = StartDate From lnk_patientprogramstart Where ptn_pk = @patientid And ModuleId = @moduleId
Select @ModuleName = ModuleName From Mst_module Where ModuleId = @moduleId
--Select @HIVStartDate = StartDate From lnk_patientprogramstart Where ptn_pk = @patientid And ModuleId = 2
Select @InitialStartDate = StartDate From lnk_patientprogramstart Where ptn_pk = @patientid
And ModuleId In
(
	Select
		ModuleId
	From Mst_Module
	Where Modulename Like 'Initial Evaluation and Follow%'
)
--Table-0                      
Select	ModuleId [ModuleId],
		ModuleName [ModuleName],
		DisplayName,
		CanEnroll,
		FieldId [FieldId],
		FieldName [FieldName],
		FieldType [FieldType],
		FieldLabel,
		AutoPopulateNumber [AutoNumber]
From VW_ModuleIdentifiers
Where ModuleId = @moduleId
Order By AutoPopulateNumber

--table-1                    
Select *
From mst_patient
Where ptn_pk = @patientid
--table-2                  
	If (charindex(@ModuleName, 'CCC Patient Card MoH 257') > 0) Begin
	--select Ptn_pk,ModuleId,ISNULL(StartDate,ISNULL(@HIVStartDate,@InitialStartDate))[StartDate],CreateDate from lnk_patientprogramstart where ptn_pk=@patientid and ModuleId=@moduleId              
		Select	coalesce(@ModuleStartDate, @HIVStartDate, @InitialStartDate) [StartDate],
				'1' [Enrolchk],
				'0' [ReEnrollCount]
	End 
	Else Begin
		Select	Ptn_pk,
				ModuleId,
				@ModuleStartDate [StartDate],
				CreateDate,
				'1' [Enrolchk],
				isnull(
				(
					Select
						count(ReEnrollDate)
					From lnk_PatientReEnrollment
					Where ptn_Pk = @PatientId
						And ModuleId = @ModuleId
				), 0) [ReEnrollCount]
		From lnk_patientprogramstart
		Where ptn_pk = @patientid
		And ModuleId = @moduleId
	End
--Table3--        
Select	a.Ptn_Pk,
		a.LocationId,
		a.ModuleId,
		a.TrackingId,
		b.CareEnded,
		b.CareEndedDate
From dtl_PatientTrackingCare a
Inner Join dtl_PatientCareEnded b On a.Ptn_Pk = b.Ptn_Pk
		And a.TrackingId = b.TrackingId
		And a.LocationId = b.LocationId
Where a.Ptn_pk = @patientid
And a.ModuleId = @moduleId
And b.CareEnded = 1
---Table4  
If (@AutoFieldName Is Not Null)  Begin
	Set @sql = 'select ' + @AutoFieldName + '[AutoField] from mst_patient where ptn_pk=' + convert(varchar, @patientid) + ''
	Exec (@sql)
End
Else Select Null AutoField

End

;
GO


