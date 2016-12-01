
/****** Object:  StoredProcedure [dbo].[pr_Clinical_Get_ZScores]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_Get_ZScores]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_Get_ZScores]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_SaveInfantInfo_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_SaveInfantInfo_Futures]
GO
Set Ansi_nulls On
Go

Set Quoted_identifier On
Go

-- =============================================
-- Author:		<Archana Mahawar>
-- Create date: <06-11-2009>
-- Description:	<Used to Save the infant info in dtl_Infantsparent>
-- =============================================
CREATE PROCEDURE [dbo].[pr_Clinical_SaveInfantInfo_Futures]
@Ptnpk int,
@VisitPk int,
@LocationId int,
@ParentId int,
@UserId int
	
AS 
Begin
	Insert Into dtl_InfantParent (
			Ptn_pk
		,	Visit_pk
		,	LocationId
		,	ParentPtnPk
		,	UserId
		,	CreateDate)
	Values (
			@Ptnpk
		,	@VisitPk
		,	@LocationId
		,	@ParentID
		,	@UserID
		,	getdate())


	Select	Id
		,	Ptn_pk
		,	Visit_pk
		,	LocationId
		,	ParentPtnPk
		,	DeleteFlag
		,	UserId
		,	CreateDate
		,	UpdateDate
	From dtl_InfantParent
End

Go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_SaveEnrollmentFrmPMTCT_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_SaveEnrollmentFrmPMTCT_Constella]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[pr_Clinical_SaveEnrollmentFrmPMTCT_Constella]                                             
  @PatientId int = null,                                      
  @FirstName varchar(50), 
  @MiddleName varchar(50), 
  @LastName varchar(50), 
  @CountryID varchar(50)=null, 
  @POSID varchar(50)=null, 
  @SatelliteId varchar(50)=null,                         
  @LocationId int, 
  @RegDate datetime, 
  @Sex int, 
  @DOB datetime,
  @DOBPrecision int,
  @Status varchar(50)=null, 
  @MStatus varchar(50), 
  @TransferIn varchar(50),                         
  @DataQuality int, 
  @RefFrom varchar(50), 
  @ANCNumber varchar(50),
  @PMTCTNumber varchar(50), 
  @Admission varchar(50),
  @HEIIDNumber varchar(50),
  @OutpatientNumber varchar(50),                                         
  @Address varchar(50), 
  @Village varchar(50),
  @District varchar(50), 
  @Phone varchar(50),
  @userID int, 
  @password varchar(40) = null,                  
  @visittype int=11                                        
                                                                       
                                                                          
AS                                                                            
BEGIN                                                                            
 -- SET NOCOUNT ON added to prevent extra result sets from                                                                            
 -- interfering with SELECT statements.                                                                            
SET NOCOUNT ON;                                                                            
--Declare @SymKey varchar(400)                                                                        
--Set @SymKey = 'Open symmetric key Key_CTC decryption by password='+ @password + ''                                                                            
--exec(@SymKey)       
                                                                  
-- Insert statements for procedure here                                                      
--  DobPrecision, 1,                      
                    
	If (@PatientId Is Null Or @PatientId < 1) Begin --Add Mode                    
         
		Insert Into mst_patient (
				PatientEnrollmentID
			,	FirstName
			,	MiddleName
			,	LastName
			,	CountryID
			,	PosId
			,	SatelliteId
			,	LocationID
			,	RegistrationDate
			,	Sex
			,	DOB
			,	Status
			,	DobPrecision
			,	MaritalStatus
			,	ANCNumber
			,	PMTCTNumber
			,	AdmissionNumber
			,	HEIIDNumber
			,	OutpatientNumber
			,	TransferIn
			,	ReferredFrom
			,	Address
			,	VillageName
			,	DistrictName
			,	Phone
			,	UserID
			,	CreateDate)
		Values (
				Null
			,	encryptbykey(key_guid('Key_CTC'), @FirstName)
			,	encryptbykey(key_guid('Key_CTC'), @MiddleName)
			,	encryptbykey(key_guid('Key_CTC'), @LastName)
			,	@CountryID
			,	@POSID
			,	@SatelliteId
			,	@LocationId
			,	@RegDate
			,	@Sex
			,	@DOB
			,	@Status
			,	@DOBPrecision
			,	@MStatus
			,	nullif(@ANCNumber, '')
			,	nullif(@PMTCTNumber, '')
			,	nullif(@Admission, '')
			,	nullif(@HEIIDNumber,'')
			,	nullif(@OutpatientNumber, '')
			,	@TransferIn
			,	@RefFrom
			,	encryptbykey(key_guid('Key_CTC'), nullif(@Address, ''))
			,	@Village
			,	@District
			,	encryptbykey(key_guid('Key_CTC'), nullif(@Phone, ''))
			,	@UserId
			,	getdate())   ;
		                  
			Select @PatientId=scope_identity();        
		
			declare @IQNumber varchar(100);
		
			Select @IQNumber = upper(substring(@FirstName, 1, 1)) +	upper(substring(@LastName, 1, 1)) +	convert(varchar, @DOB, 112) + convert(varchar, @LocationId) + convert(varchar(10), @PatientId);

			Update mst_patient Set IQNumber = @IQNumber	Where ptn_pk = @PatientId
        
		--Update mst_patient Set
		--		IQNumber = 'IQ-' + convert(varchar, replicate('0', 20 - len(x.[ptnIdentifier]))) + x.[ptnIdentifier]
		--From (
		--Select upper(substring(@FirstName, 1, 1)) +	upper(substring(@LastName, 1, 1)) +	convert(varchar, DOB, 112) + convert(varchar, LocationID) + convert(varchar(10), Ptn_Pk) [ptnIdentifier]
		--From mst_patient
		--Where ptn_pk = @PatientId
		--) x
		--Where ptn_pk = @PatientID      
                                                                        
	 End         
	Else Begin --Update Mode            
           
		 --Declare @StartDate datetime               
		 --Select @StartDate=StartDate from Lnk_PatientProgramStart where ptn_pk=@PatientID and ModuleId=2                
		 --Select @StartDate =                
		 -- Case                
		 --  WHEN @RegDate < @StartDate  Then @RegDate                
		 --  WHEN @RegDate > @StartDate  Then @StartDate                
		 -- Else @RegDate                
		 -- End                 
		Update mst_patient Set
				FirstName = encryptbykey(key_guid('Key_CTC'), @FirstName)
			,	MiddleName = encryptbykey(key_guid('Key_CTC'), @MiddleName)
			,	LastName = encryptbykey(key_guid('Key_CTC'), @LastName)
			,	LocationID = @LocationId
			--,	RegistrationDate = @StartDate
			,	Sex = @Sex
			,	DOB = @DOB
			,	DobPrecision = @DOBPrecision
			,	MaritalStatus = @MStatus
			,	ANCNumber = @ANCNumber
			,	PMTCTNumber = @PMTCTNumber
			,	AdmissionNumber = @Admission
			,	HEIIDNumber =	 @HEIIDNumber
			,	OutpatientNumber = @OutpatientNumber
			,	TransferIn = @TransferIn
			,	ReferredFrom = @RefFrom
			,	Address = encryptbykey(key_guid('Key_CTC'), nullif(@Address, ''))
			,	VillageName = @Village
			,	DistrictName = @District
			,	Phone = encryptbykey(key_guid('Key_CTC'), nullif(@Phone, ''))
			,	UserID = @UserID
			,	status = 0
		Where ptn_pk = @PatientId ;                      
            
	End      
	
	If(Not exists (Select ptn_pk From ord_Visit Where ptn_pk = @PatientId	And VisitType = 12)) Begin
		Insert Into dbo.ord_Visit (
				Ptn_Pk
			,	LocationID
			,	VisitDate
			,	VisitType
			,	DataQuality
			,	UserID
			,	CreateDate)
		Values (
				@PatientId
			,	@locationId
			,	@RegDate
			,	12
			,	@DataQuality
			,	@UserId
			,	getdate())
	End               
          
	declare @VisitCount int,  @ProgCount int;
	      
	Update dbo.ord_Visit Set
			VisitDate = @RegDate
		,	DataQuality = @DataQuality
		,	UserID = @UserID
		,	updatedate = getdate()
	Where ptn_pk = @PatientId
	And Visittype = 11

	Select @VisitCount = @@rowcount;

	If(@VisitCount = 0) Begin
		Insert Into dbo.ord_Visit (
			Ptn_Pk
		,	LocationID
		,	VisitDate
		,	VisitType
		,	DataQuality
		,	UserID
		,	CreateDate)
		Values (
			@PatientID
		,	@locationid
		,	@RegDate
		,	@visittype
		,	@DataQuality
		,	@UserID
		,	getdate())    
	End
	
	Update Lnk_PatientProgramStart Set
			StartDate = @RegDate
	Where Ptn_pk = @PatientId
	And ModuleId = 1    ;
	
	Select @ProgCount = @@rowcount

	If(@ProgCount = 0 ) Begin
		Insert Into lnk_PatientProgramStart (
				Ptn_pk
			,	ModuleId
			,	StartDate
			,	UserID
			,	CreateDate)
		Values (
				@PatientID
			,	1
			,	@RegDate
			,	@UserID
			,	getdate())     
	End
	                                                          
 --Close symmetric key Key_CTC                                                                                   
          
	Select	@PatientID	As PatientID
		,	b.Visit_Id
	From mst_Patient As a
	Inner Join ord_Visit As b On a.Ptn_Pk = b.Ptn_Pk
	Where (a.Ptn_Pk = @PatientID)
		And (b.VisitType = 11)                             
                                                        
END

GO


IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_ValidateCustomForm_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_ValidateCustomForm_Futures]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_Clinical_ValidateCustomForm_Futures]                       
   @FormName varchar(50), 
   @VisitDate varchar(12), 
   @PatientId int  ,
   @ModuleId int        
AS                                                      
BEGIN                                                      
 -- SET NOCOUNT ON added to prevent extra result sets from                                                      
 -- interfering with SELECT statements.                                                      
SET NOCOUNT ON;                                                      
                                           
--01 Insert statements for procedure here                                
 Declare @VisitTypeId int            
 Select  @VisitTypeId=VisitTypeID from mst_visittype where VisitName=@FormName
if (@VisitDate = '01-01-1900')Begin
	--01
	Declare @Count int
		Select @Count = count(*)
		From ord_visit
		Where Ptn_pk = @PatientID
			And VisitType = @VisitTypeID
			And (DeleteFlag Is Null Or DeleteFlag = 0)       
	--04
	Select @Count [Count]
	if @Count = 1 Begin
		Select	Visit_Id
			,	LocationID
		From ord_visit
		Where Ptn_pk = @PatientID
			And VisitType = @VisitTypeID
			And (DeleteFlag Is Null Or DeleteFlag = 0)
	End
End                                       

	Else Begin
            
		Select count(*) [Visit]
		From ord_visit
		Where Ptn_pk = @PatientID
			And VisitDate = @VisitDate
			And VisitType = @VisitTypeID
			And (DeleteFlag Is Null Or DeleteFlag = 0)       
	--02  
	 --Declare @ModuleID varchar(5)  
	 --Select @ModuleID=ModuleID from mst_feature where FeatureName=@FormName
	 --Select ModuleId From mst_Feature Where FeatureName=@FormName
	
		Select StartDate From Lnk_PatientProgramStart Where Ptn_pk=@PatientId And ModuleId = @ModuleId
		--Select min(StartDate) StartDate
		--From Lnk_PatientProgramStart P
		--Inner Join (
		--	Select ModuleId
		--	From mst_Feature
		--	Where FeatureName = @FormName
		--	Union
		--	Select S.ModuleId
		--	From lnk_SplFormModule S
		--	Inner Join mst_Feature F On F.FeatureID = S.FeatureId
		--	And F.FeatureName = @FormName
		--) F On P.ModuleId = F.ModuleId
		--Where Ptn_pk = @PatientId

	End	
End

GO

/****** Object:  StoredProcedure [dbo].[pr_Clinical_GetPatientRecord_Futures]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_GetPatientRecord_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_GetPatientRecord_Futures]
GO
	  
/****** Object:  StoredProcedure [dbo].[pr_Clinical_IssueItemToPatient]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_IssueItemToPatient]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_IssueItemToPatient]
GO
/****** Object:  StoredProcedure [dbo].[pr_Clinical_FindItemByName]    Script Date: 02/02/2015 12:20:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_FindItemByName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_FindItemByName]
GO
/****** Object:  StoredProcedure [dbo].[pr_Clinical_DeleteItemIssuedToPatient]    Script Date: 02/03/2015 15:18:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_DeleteItemIssuedToPatient]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_DeleteItemIssuedToPatient]
GO
/****** Object:  StoredProcedure [dbo].[pr_Clinical_PatientConsumablesByDate]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_PatientConsumablesByDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_PatientConsumablesByDate]
GO
/****** Object:  StoredProcedure [dbo].[pr_Clinical_GetPatientItemsByUserID]    Script Date: 04/30/2015 11:53:56 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_GetPatientItemsByUserID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_GetPatientItemsByUserID]
GO
/****** Object:  StoredProcedure [dbo].[pr_Clinical_GetMaxAutopopulatIdentifier]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_GetMaxAutopopulatIdentifier]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_GetMaxAutopopulatIdentifier]
GO
/****** Object:  StoredProcedure [dbo].[pr_Security_facilitydetails1_constella]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Security_facilitydetails1_constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Security_facilitydetails1_constella]
GO
/****** Object:  StoredProcedure [dbo].[pr_Clinical_GetARVTherapyPatientData]    Script Date: 09/04/2015 12:18:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_GetARVTherapyPatientData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_GetARVTherapyPatientData]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_GetPatientRegistrationCustomFormFieldLabel_Constella]') AND type in (N'P', N'PC'))
 DROP PROCEDURE [dbo].[pr_Clinical_GetPatientRegistrationCustomFormFieldLabel_Constella]
GO

/****** Object:  StoredProcedure [dbo].[pr_Clinical_GetPatientRegistrationCustomFormFieldLabel_Constella]    Script Date: 8/11/2016 1:18:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[pr_Clinical_GetPatientRegistrationCustomFormFieldLabel_Constella]                                                                                                                                                             
@FeatureId int,                                                                                                                                                                  
@PatientId int,                                                                                                                                                                  
@Password varchar(40)    = null                                                                                                                                                                
as                                                                                                                                                                  
Begin                                                                                                                                                                  
Declare @SymKey varchar(400)                                                                                            
Declare @FormVisitType int                                                                                            
Declare @VisitTypeId int                                                                                              
Declare @FeatureName varchar(100)                                                                                                                                                                                               
                                                                                                                                                                                                
--Set @SymKey = 'Open symmetric key Key_CTC decryption by password='+ @password + ''                                                                                                                                                                             
  
    
      
        
          
--Exec(@SymKey)                                                                                                                                                                  
                                                                                                                                                                  
--Table 0                                                                                                                                                                  
Select	dbo.fn_PatientIdentificationNumber_Constella(a.Ptn_Pk, '', 1)	As PatientIdentification
	,	convert(varchar(50), decryptbykey(a.FirstName)) + ' ' + isnull(convert(varchar(50), decryptbykey(a.MiddleName)), '')
		+ ' ' + convert(varchar(50), decryptbykey(a.LastName))			As Name
	,	a.PatientClinicID
	,	a.DOB
	,	a.LocationID
From mst_Patient As a
Inner Join ord_Visit As b On a.Ptn_Pk = b.Ptn_Pk
Where (b.VisitType = 12)
	And (a.Ptn_Pk = @patientid)                                                                                                                                               
  

  
      
          
---Table 1                                                
Select * from (
	Select	tbl1.FeatureId
		,	tbl2.FeatureName
		,	tbl1.SectionId
		,	tbl3.SectionName
		,	tbl3.SectionInfo
		,	tbl1.FieldId
		,	tbl4.BindField						As FieldName
		,	replace(tbl1.FieldLabel, '''', '')	As FieldLabel
		,	tbl1.Predefined
		,	tbl4.PDFTableName
		,	tbl4.ControlId
		,	tbl4.ModuleId
		,	tbl4.BindTable						As BindSource
		,	tbl4.CategoryId						As CodeId
		,	tbl1.Seq
		,	tbl3.Seq							As SeqSection
		,	Convert(bit,Isnull(tbl3.IsGridView,0)) IsGridView
	From Lnk_Forms As tbl1
	Inner Join mst_Feature As tbl2 On tbl1.FeatureId = tbl2.FeatureID
	Inner Join mst_Section As tbl3 On tbl1.SectionId = tbl3.SectionID and tbl3.DeleteFlag =0 
	Inner Join Mst_PreDefinedFields As tbl4 On tbl1.FieldId = tbl4.Id
	Left Outer Join mst_pmtctCode As tbl5 On tbl4.CategoryId = tbl5.CodeID
	And tbl4.BindTable = 'Mst_PMTCTDecode'
	Left Outer Join mst_Code As tbl6 On tbl4.CategoryId = tbl6.CodeID
	And tbl4.BindTable = 'Mst_DeCode'
	Where (tbl1.Predefined = 1)
		And (tbl1.FeatureId = @FeatureId)                      
	union                                                                
	Select	tbl1.FeatureId
		,	tbl2.FeatureName
		,	tbl1.SectionId
		,	tbl3.SectionName
		,	tbl3.SectionInfo
		,	tbl1.FieldId
		,	'PlaceHolder'						As FieldName
		,	replace(tbl1.FieldLabel, '''', '')	As FieldLabel
		,	tbl1.Predefined
		,	tbl4.PDFTableName
		,	tbl4.ControlId
		,	tbl4.ModuleId
		,	tbl4.BindTable						As BindSource
		,	tbl4.CategoryId						As CodeId
		,	tbl1.Seq
		,	tbl3.Seq							As SeqSection
		,	Convert(bit,Isnull(tbl3.IsGridView,0)) IsGridView
	From Lnk_Forms As tbl1
	Inner Join mst_Feature As tbl2 On tbl1.FeatureId = tbl2.FeatureID
	Inner Join mst_Section As tbl3 On tbl1.SectionId = tbl3.SectionID and tbl3.DeleteFlag =0 
	Inner Join Mst_PreDefinedFields As tbl4 On 71 = tbl4.Id
	Left Outer Join mst_pmtctCode As tbl5 On tbl4.CategoryId = tbl5.CodeID
	And tbl4.BindTable = 'Mst_PMTCTDecode'
	Left Outer Join mst_Code As tbl6 On tbl4.CategoryId = tbl6.CodeID
	And tbl4.BindTable = 'Mst_DeCode'
	Where (tbl1.Predefined = 1)
		And (substring(convert(varchar, tbl1.FieldId), 3, 5) = '00000')
		And (tbl1.FeatureId = @FeatureId)                                                                 
	union                                                                                                                                                                
	Select	tbl1.FeatureId
		,	tbl2.FeatureName
		,	tbl1.SectionId
		,	tbl3.SectionName
		,	tbl3.SectionInfo
		,	tbl1.FieldId
		,	tbl4.FieldName
		,	replace(tbl1.FieldLabel, '''', '')	As FieldLabel
		,	tbl1.Predefined
		,	Case
				When ControlId = 11 Then Null
				When ControlId = 12 Then Null
				Else 'dtl_CustomField'
			End									As 'PDFTableName'
		,	tbl4.ControlId
		,	0									As ModuleId
		,	tbl4.BindTable						As BindSource
		,	tbl5.CodeID
		,	tbl1.Seq
		,	tbl3.Seq							As SeqSection
		,	Convert(bit,Isnull(tbl3.IsGridView,0)) IsGridView
	From Lnk_Forms As tbl1
	Inner Join mst_Feature As tbl2 On tbl1.FeatureId = tbl2.FeatureID
	Inner Join mst_Section As tbl3 On tbl1.SectionId = tbl3.SectionID and tbl3.DeleteFlag =0 
	Inner Join mst_CustomformField As tbl4 On tbl1.FieldId = tbl4.Id
	Left Outer Join mst_ModCode As tbl5 On tbl4.CategoryId = tbl5.CodeID
	And tbl4.BindTable = 'Mst_ModDecode'
	Where (tbl1.Predefined = 0)
		And (tbl1.FeatureId = @FeatureId)

 ) Z  order by Z.SeqSection, Z.Seq asc         
      
                      
---Table 02 [for Business Rule]                                                                                                                                       
Select distinct Y.FieldId, Y.FieldLabel, Y.Predefined, Y.BusRuleId, Y.FieldName, Mst_BusinessRule.Name, Y.ControlId, Y.Value,Y.Value1, Y.TableName           
from (
select Z.FieldId, Z.FieldLabel, Z.Predefined, Z.FieldName, lnk_fieldsBusinessRule.BusRuleId,lnk_fieldsBusinessRule.Value,lnk_fieldsBusinessRule.Value1,   
Z.ControlId, Z.TableName from (
Select	tbl1.FieldId
	,	tbl1.FieldLabel
	,	tbl1.Predefined
	,	tbl2.BindField		As FieldName
	,	tbl2.PDFTableName	As TableName
	,	tbl2.ControlId
From Lnk_Forms As tbl1
Inner Join Mst_PreDefinedFields As tbl2 On tbl1.FieldId = tbl2.Id
Where (tbl1.FeatureId = @FeatureId)
	And (tbl1.Predefined = 1)                                                                                                                                     
union                                                                                                                                      
Select	tbl1.FieldId
	,	tbl1.FieldLabel
	,	tbl1.Predefined
	,	tbl2.FieldName
	,	'dtl_customfield'	As TableName
	,	tbl2.ControlId
From Lnk_Forms As tbl1
Inner Join mst_CustomformField As tbl2 On tbl1.FieldId = tbl2.Id
Where (tbl1.FeatureId = @FeatureId)
	And (tbl1.Predefined = 0))Z                                                                             
inner join lnk_fieldsBusinessRule on Z.FieldId=lnk_fieldsBusinessRule.FieldId and                                                                                                                                       
Z.Predefined=lnk_fieldsBusinessRule.Predefined)Y, Mst_BusinessRule                                                                                                                             
where Y.BusRuleId=Mst_BusinessRule.ID                                                                                                          
union                                                                              
Select	a.ConditionalFieldId			As FieldId
	,	a.ConditionalFieldLabel			As FieldLabel
	,	a.ConditionalFieldPredefined	As Predefined
	,	c.Id							As BusRuleId
	,	a.ConditionalFieldName			As FieldName
	,	c.Name
	,	a.ConditionalFieldControlId		As ControlId
	,	b.Value
	,	b.Value1
	,	a.ConditionalFieldSavingTable	As TableName
From VW_FieldConditionalField As a
Inner Join lnk_fieldsBusinessRule As b On a.ConditionalFieldId = b.FieldId
And a.ConditionalFieldPredefined = b.Predefined
Inner Join Mst_BusinessRule As c On b.BusRuleId = c.Id
Where (a.FeatureId = @FeatureId)
Order By BusRuleId                            
                                                                            
---Table 03 for all Controls Except MultiSelect                                                 
Select * From (
	Select	tbl1.FeatureId
		,	tbl2.FeatureName
		,	tbl1.SectionId
		,	tbl3.SectionName
		,	tbl1.FieldId
		,	tbl4.BindField						As FieldName
		,	replace(tbl1.FieldLabel, '''', '')	As FieldLabel
		,	tbl1.Predefined
		,	lower(tbl4.PDFTableName)			
			As PDFTableName
		,	tbl4.ControlId
		,	tbl4.ModuleId
		,	tbl4.BindTable						As BindSource
		,	tbl4.CategoryId						As CodeId
		,	tbl1.Seq
	From Lnk_Forms As tbl1
	Inner Join mst_Feature As tbl2 On tbl1.FeatureId = tbl2.FeatureID
	Inner Join mst_Section As tbl3 On tbl1.SectionId = tbl3.SectionID
	Inner Join Mst_PreDefinedFields As tbl4 On tbl1.FieldId = tbl4.Id
	Left Outer Join mst_pmtctCode As tbl5 On tbl4.CategoryId = tbl5.CodeID
	And tbl4.BindTable = 'Mst_PMTCTDecode'
	Left Outer Join mst_Code As tbl6 On tbl4.CategoryId = tbl6.CodeID
	And tbl4.BindTable = 'Mst_DeCode'
	Where (tbl1.Predefined = 1)
		And (tbl1.FeatureId = @FeatureId)
		And (tbl4.ControlId Not In (9))                                                                                                                               
union                                                                        
Select	tbl1.FeatureId
	,	tbl2.FeatureName
	,	tbl1.SectionId
	,	tbl3.SectionName
	,	tbl1.FieldId
	,	tbl4.FieldName
	,	replace(tbl1.FieldLabel, '''', '')	As FieldLabel
	,	tbl1.Predefined
	,	Case
			When ControlId = 11 Then Null
			When ControlId = 12 Then Null
			Else 'dtl_customfield'
		End									As 'PDFTableName'
	,	tbl4.ControlId
	,	0									As 'ModuleId'
	,	tbl4.BindTable						As BindSource
	,	tbl5.CodeID
	,	tbl1.Seq
From Lnk_Forms As tbl1
Inner Join mst_Feature As tbl2 On tbl1.FeatureId = tbl2.FeatureID
Inner Join mst_Section As tbl3 On tbl1.SectionId = tbl3.SectionID
Inner Join mst_CustomformField As tbl4 On tbl1.FieldId = tbl4.Id
Left Outer Join mst_ModCode As tbl5 On tbl4.CategoryId = tbl5.CodeID
And tbl4.BindTable = 'Mst_ModDecode'
Where (tbl1.Predefined = 0)
	And (tbl1.FeatureId = @FeatureId)
	And (tbl4.ControlId Not In (9))          
union            
Select	a.FeatureId
	,	b.FeatureName
	,	a.FieldSectionId
	,	a.FieldSectionName	
	,	a.ConditionalFieldId					As FieldId
	,	a.ConditionalFieldName					As FieldName
	,	a.ConditionalFieldLabel					As FieldLabel
	,	a.ConditionalFieldPredefined			As Predefined
	,	lower(a.ConditionalFieldSavingTable)	As PDFTableName
	,	a.ConditionalFieldControlId				As ControlId
	,	a.ModuleId
	,	a.ConditionalFieldBindTable				As BindSource
	,	a.ConditionalFieldCategoryId			As CodeId
	,	a.ConditionalFieldSequence				As Seq
From VW_RegistrationConditionalField As a
Inner Join mst_Feature As b On a.FeatureId = b.FeatureID
And b.FeatureID = @FeatureId
And a.ConditionalFieldSavingTable Is Not Null      
--union      
--select tbl.FeatureId, 'Patient Registration'[FeatureName], tbl.FieldSectionId[SectionId], tbl.FieldSectionName[SectionName],            
--tbl1.Id, tbl1.BindField[FieldName], tbl.FieldLabel, tbl.FieldPredefined[Predefined],  lower(tbl1.PDFTableName)[PDFTableName],            
--tbl1.ControlId, tbl1.ModuleId, tbl1.BindTable[BindSource],             
--0[CodeId], 0[Seq]            
--from VW_RegistrationConditionalField tbl inner join Mst_PreDefinedFields tbl1 on tbl.FieldId=tbl1.Id            
--and tbl.FeatureId=@FeatureId             
) Z  order by Z.Seq asc             
                                                                                                                     
                                                                                                             
--Table 04                                                                                                  
Select * from mst_feature where FeatureId=@FeatureId                                            
--Table 05                                                                                           
Select @FormVisitType=MultiVisit,@FeatureName=FeatureName from mst_feature where FeatureId=@FeatureId                                                                                           
Select @VisitTypeId=VisitTypeId from mst_Visittype where (deleteflag =0 or deleteflag is null) and visitname=@FeatureName                                                       
 if(@FormVisitType=1)                                               
  Begin                                                                                            
   Select '0'[Visit_Id]                                                                                          
  End                                                                                            
 Else                                                                                            
  Begin                                                                                            
  Select Visit_Id from Ord_Visit where Ptn_pk=@PatientId and VisitType=@VisitTypeId and (DeleteFlag IS NULL or DeleteFlag=0)                                                                  
  End                                                                                            
---Table 06 Conditional Fields                            
Select	a.FeatureId
	,	b.FeatureName
	,	a.FieldSectionId
	,	a.FieldSectionName
	,	'8' + convert(varchar, a.ConditionalFieldId)	As FieldId
	,	a.ConditionalFieldBindField						As FieldName
	,	a.ConditionalFieldLabel							As FieldLabel
	,	a.ConditionalFieldPredefined					As Predefined
	,	a.ConditionalFieldSavingTable					As PDFTableName
	,	a.ConditionalFieldControlId						As ControlId
	,	a.ConditionalFieldBindTable						As BindSource
	,	a.ConditionalFieldCategoryId					As CodeId
	,	a.ConditionalFieldSequence						As Seq
	,	a.FieldSectionSequence							As SeqSection
	,	a.ConditionalFieldSectionId
	,	a.FieldId										As ConFieldId
	,	a.FieldPredefined								As ConFieldPredefined
	,	a.ModuleId
	,	a.ConditionalFieldId
From VW_RegistrationConditionalField As a
Inner Join mst_Feature As b On a.FeatureId = b.FeatureID
And a.ConditionalFieldPredefined = 1
And b.FeatureID = @FeatureId
And a.ConditionalFieldId Is Not Null
And a.ConditionalFieldName Is Not Null                                                                     
union                                             
Select	a.FeatureId
	,	b.FeatureName
	,	a.FieldSectionId
	,	a.FieldSectionName
	,	'9' + convert(varchar, a.ConditionalFieldId)	As FieldId
	,	a.ConditionalFieldName							As FieldName
	,	a.ConditionalFieldLabel							As FieldLabel
	,	a.ConditionalFieldPredefined					As Predefined
	,	a.ConditionalFieldSavingTable					As PDFTableName
	,	a.ConditionalFieldControlId						As ControlId
	,	a.ConditionalFieldBindTable						As BindSource
	,	a.ConditionalFieldCategoryId					As CodeId
	,	a.ConditionalFieldSequence						As Seq
	,	a.FieldSectionSequence							As SeqSection
	,	a.ConditionalFieldSectionId
	,	a.FieldId										As ConFieldId
	,	a.FieldPredefined								As ConFieldPredefined
	,	a.ModuleId
	,	a.ConditionalFieldId
From VW_RegistrationConditionalField As a
Inner Join mst_Feature As b On a.FeatureId = b.FeatureID
And a.ConditionalFieldPredefined = 0
And b.FeatureID = @FeatureId
And a.ConditionalFieldId Is Not Null
And a.ConditionalFieldName Is Not Null                                      
union                                                              
Select	a.FeatureId
	,	b.FeatureName
	,	a.FieldSectionId
	,	a.FieldSectionName
	,	'8' + convert(varchar, a.ConditionalFieldId)	As FieldId
	,	'PlaceHolder'									As FieldName
	,	a.ConditionalFieldLabel							As FieldLabel
	,	a.ConditionalFieldPredefined					As Predefined
	,	a.ConditionalFieldSavingTable					As PDFTableName
	,	'13'											As ControlId
	,	a.ConditionalFieldBindTable						As BindSource
	,	a.ConditionalFieldCategoryId					As CodeId
	,	a.ConditionalFieldSequence						As Seq
	,	a.FieldSectionSequence							As SeqSection
	,	a.ConditionalFieldSectionId
	,	a.FieldId										As ConFieldId
	,	a.FieldPredefined								As ConFieldPredefined
	,	a.ModuleId
	,	a.ConditionalFieldId
From VW_RegistrationConditionalField As a
Inner Join mst_Feature As b On a.FeatureId = b.FeatureID
And a.ConditionalFieldPredefined = 1
And b.FeatureID = @FeatureId
And a.ConditionalFieldId Is Not Null
And a.ConditionalFieldId Like '710000%'                                                                                 
--Table 07                                                                 
Select a.StartDate
From dbo.lnk_PatientProgramStart a
Inner Join Mst_Feature b On a.ModuleId = b.ModuleId
Where b.FeatureId = @FeatureId
	And a.Ptn_Pk = @PatientId                                                       
                      
--Table 08                                          
Declare @sql nvarchar(max)                                                
set @sql ='if exists(select * from sysobjects where name=''DTL_FBCUSTOMFIELD_'+REPLACE(@FeatureName,' ','_')+''')                                                   
Begin                                                  
select  * from [DTL_FBCUSTOMFIELD_'+REPLACE(@FeatureName,' ','_')+'] a inner join ord_visit b on a.visit_pk=b.Visit_Id                                                           
where b.ptn_pk='+ convert(varchar,@PatientId)+' order by b.visitdate desc                                                   
end                                              
else                                               
Begin                                              
Select 0                                              
End'                                                        
EXECUTE sp_executesql @sql                                                
--print  @sql                            
--Table 09                                   
select Z.Visit_ID[VisitID],z.VisitDate from(Select visit_id, VisitDate from ord_Visit where Visittype                                         
   =(select VisitTypeID from mst_visittype where (deleteflag = 0 or deleteflag is null) and  VisitTypeID <>0 and convert(binary(50),VisitName) =                                       
   convert(binary(50),(Select FeatureName from mst_feature where FeatureID=@FeatureId)))                                          
     and Ptn_Pk=@PatientId)Z                                        
     where Z.visitdate =(select X.Visitdate from(Select distinct  max(visitdate)[visitdate] from ord_Visit                                         
     where  (deleteflag = 0 or deleteflag is null) and Visittype = (select VisitTypeID from mst_visittype                                      
      where (deleteflag = 0 or deleteflag is null) and VisitTypeID <>0 and VisitTypeID <>0 and convert(binary(50),VisitName) =                                         
     convert(binary(50),(Select FeatureName from mst_feature where FeatureID=@FeatureId)))                                         
      and Ptn_Pk=@PatientId)X)                               
                                  
                                                 
--Close symmetric key Key_CTC                
            
select FeatureID,FeatureName,DeleteFlag ,Published from mst_feature              
where FeatureID=@FeatureId   and Published=2                                                                                                                  
End

GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_SaveUpdateARVTherapy_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_SaveUpdateARVTherapy_Futures]
GO

/****** Object:  StoredProcedure [dbo].[pr_Clinical_SaveUpdateARVTherapy_Futures]    Script Date: 7/29/2016 7:41:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[pr_Clinical_SaveUpdateARVTherapy_Futures]  (                                                                 
-- Add the parameters for the stored procedure here             
	@patientid int,
	@locationid int, 
	@Visit_ID int,
	@ARVDateEligible datetime,
	@ARVEligibleThrough int,
	@EligibleWHOStage int,          
	@EligibleCD4 varchar(50), 
	@EligibleCD4percent varchar(50),
	@ARVCohortMonth varchar(50),
	@ARVCohortYear int,          
	@AnotherRegimenStartDate datetime, 
	@AnotherRegimen varchar(200),
	@AnotherWeight varchar(50),
	@AnotherHeight varchar(50),          
	@AnotherWHOStage int, 
	@dataquality int, 
	@UserId int     ,
	@OtherEligibleCriteria varchar(100) = null  ,
	@Muac decimal (4,1) = null  ,
	@ModuleId int= null                                        
)                       
As                                                                  
Begin
-- SET NOCOUNT ON added to prevent extra result sets from                                                                  
-- interfering with SELECT statements.                                            

	Set Nocount On;
	Declare @Visit_Pk int;

	If (@Visit_ID > 0) Begin
		Update ord_visit Set
			DataQuality = @dataquality,
			UpdateDate = Getdate()
		Where visit_Id = @Visit_ID
		And Ptn_Pk = @patientid
		And LocationID = @locationid;

		Update dtl_PatientARVEligibility Set
			userID = @UserId,
			updateDate = Getdate(),
			dateEligible = @ARVDateEligible,
			eligibleThrough = @ARVEligibleThrough,
			WHOStage = @EligibleWHOStage,
			CD4 = Nullif(@EligibleCD4, ''),
			CD4percent = Nullif(@EligibleCD4percent, ''),
			OtherCriteria = Nullif(@OtherEligibleCriteria,'')
		Where visit_id = @Visit_ID
		And ptn_pk = @patientid
		And locationID = @locationid;

		Update dtl_patientARTCare Set
			FirstLineRegStDate = @AnotherRegimenStartDate,
			FirstLineReg = @AnotherRegimen,
			UserId = @UserId,
			UpdateDate = Getdate()
		Where visit_Id = @Visit_ID
		And ptn_pk = @patientid
		And locationId = @locationid;

		If Exists (Select 1	From dtl_PatientVitals	Where Visit_Pk = @Visit_ID	And Ptn_pk = @patientid	And LocationID = @locationid) Begin
			Update dtl_PatientVitals Set
				[Weight] = Nullif(@AnotherWeight, ''),
				[Height] = Nullif(@AnotherHeight, ''),
				Muac = @Muac,
				[UpdateDate] = Getdate()
			Where Visit_Pk = @Visit_ID
			And ptn_pk = @patientid
			And locationId = @locationid;
		End 
		Else Begin
			Insert Into dtl_PatientVitals (
				[Ptn_pk],
				[LocationID],
				[Visit_pk],
				[Weight],
				[Height],
				Muac,
				[UserID],
				[CreateDate])
			Values (
				@patientid,
				@locationid,
				@Visit_ID,
				Nullif(@AnotherWeight, ''),
				Nullif(@AnotherHeight, ''),
			    @Muac,
				@UserId,
				Getdate() );
		End

		If Exists (Select 1	From dtl_PatientStage	Where Visit_Pk = @Visit_ID	And Ptn_pk = @patientid	And LocationID = @locationid) Begin
			Update dtl_PatientStage Set
				[WHOStage] = @AnotherWHOStage,
				[UpdateDate] = Getdate()
			Where Visit_Pk = @Visit_ID
			And Ptn_pk = @patientid
			And LocationID = @locationid;
		End 
		Else Begin
			Insert Into dtl_PatientStage (
				[Ptn_pk],
				[LocationID],
				[Visit_Pk],
				[WHOStage],
				[UserID],
				[CreateDate])
			Values (
				@patientid,
				@locationid,
				@Visit_ID,
				@AnotherWHOStage,
				@UserId,
				Getdate() )
		End      
	End 
	Else Begin
		Declare @vdate datetime;
		Select @vdate = StartDate From dbo.Lnk_PatientProgramStart	Where Ptn_pk = @patientid	And ModuleId = 203;
		Insert Into ord_Visit (
			Ptn_Pk,
			LocationID,
			VisitDate,
			VisitType,
			DataQuality,
			DeleteFlag,
			UserID,
			CreateDate,
			ModuleId)
		Values (
			@patientid,
			@locationid,
			@vdate,
			19,
			@dataquality,
			0,
			@UserId,
			Getdate(),
			@ModuleId );
		Select @Visit_Pk = scope_identity();
		Insert Into dtl_PatientARVEligibility (
			ptn_pk,
			locationID,
			visit_id,
			userID,
			createDate,
			dateEligible,
			eligibleThrough,
			WHOStage,
			CD4,
			CD4percent,
			OtherCriteria)
		Values (
			@patientid,
			@locationid,
			@Visit_Pk,
			@UserId,
			Getdate(),
			@ARVDateEligible,
			@ARVEligibleThrough,
			@EligibleWHOStage,
			Nullif(@EligibleCD4, ''),
			Nullif(@EligibleCD4percent, ''),
			Nullif(@OtherEligibleCriteria,'') );

		Insert Into dtl_PatientARTCare (
			ptn_pk,
			visit_Id,
			locationId,
			FirstLineRegStDate,
			FirstLineReg,
			UserId,
			CreateDate)
		Values (
			@patientid,
			@Visit_Pk,
			@locationid,
			@AnotherRegimenStartDate,
			@AnotherRegimen,
			@UserId,
			Getdate() );

		Insert Into dtl_PatientVitals (
			[Ptn_pk],
			[LocationID],
			[Visit_pk],
			[Weight],
			[Height],
			Muac,
			[UserID],
			[CreateDate])
		Values (
			@patientid,
			@locationid,
			@Visit_Pk,
			Nullif(@AnotherWeight, ''),
			Nullif(@AnotherHeight, ''),
			@Muac,
			@UserId,
			Getdate() );

		Insert Into dtl_PatientStage (
			[Ptn_pk],
			[LocationID],
			[Visit_Pk],
			[WHOStage],
			[UserID],
			[CreateDate])
		Values (
			@patientid,
			@locationid,
			@Visit_Pk,
			@AnotherWHOStage,
			@UserId,
			Getdate() );
	End

	If (@Visit_ID > 0) Begin
		Select	Ptn_pk,	
				Visit_Id,
				VisitDate,	
				LocationID 
		From ord_Visit Where Visit_Id = @Visit_ID;
	End 
	Else Begin
		Select	Ptn_pk,	
				Visit_Id,	
				VisitDate,	
				LocationID 
		From ord_Visit Where Visit_Id = @Visit_Pk;
	End

End

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_Clinical_GetPatientDebitNoteDetails_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_Clinical_GetPatientDebitNoteDetails_Futures]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Pr_Clinical_GetPatientDebitNoteDetails_Futures]  
 @billid int,  
 @ptn_pk int,  
 @Password varchar(50)  =null    
AS      
BEGIN      
 SET NOCOUNT ON;    
--Declare @SymKey varchar(400)                                      
--Set @SymKey = 'Open symmetric key Key_CTC decryption by password='+ @password + ''                                          
--Exec(@SymKey)     
------------------------------------------------------------------------    
SELECT  dtl.ptn_pk,      
  dtl.TransactionDate,  
  ordbill.VisitDate[BillDate],  
 dtl.BillId,dtl.quantity,      
   Description =       
   CASE       
   WHEN pha.PharmacyId is not null      
   THEN pha.DrugName      
   WHEN pla.labid is not null      
   THEN pla.testname     
   WHEN dtl.ConsultancyFee is not null     
   THEN 'Consultation Fee'      
   END    
 ,dtl.SellingPrice+dtl.ConsultancyFee as Amount    
    ,dtl.AdminFee as Adminsitration      
    ,dtl.SellingPrice+dtl.ConsultancyFee+dtl.AdminFee as Cost      
    ,dtl.BillAmount as [ChargedPrice]      
   FROM Dtl_PatientBillTransaction dtl      
   left outer join VW_PatientPharmacyTransaction pha       
  on dtl.ptn_pk = pha.ptn_pk       
  and dtl.PharmacyId = pha.PharmacyId      
  and dtl.ItemId = pha.drug_pk      
  and dtl.BatchId = pha.BatchId      
   left outer join VW_PatientLaboratory pla      
   on dtl.ptn_pk = pla.ptn_pk       
  and dtl.labid = pla.labid      
    and dtl.itemid = pla.testid  
 left outer join ord_patientbilltransaction ordbill  
 on ordbill.BillId = dtl.billid      
   where dtl.billid = @billid      
   order by dtl.TransactionDate   
  
Select	convert(varchar(50), decryptbykey(a.FirstName))	As [FirstName]
	,	convert(varchar(50), decryptbykey(a.LastName))	As [LastName]
	,	a.LocationID
	,	a.CountryID
	,	a.PosID
	,	a.SatelliteID
	,	a.PatientFacilityId
	,	a.PatientEnrollmentID
	,	a.PatientClinicID
	,	a.RegistrationDate								'RegistrationDate'
	,	a.Sex
	,	a.DOB											'DOB'
	,	a.VillageName
	,	a.DistrictName
	,	a.Province
	,	convert(varchar(50), decryptbykey(a.Address))	As [Address]
	,	convert(varchar(50), decryptbykey(a.Phone))		As [Phone]
	,	a.MaritalStatus
	,	a.EducationLevel
	,	a.Literacy
	,	a.EmployeeID
	,	a.Status
	,	a.StatusChangedDate
	,	a.IQNumber
	,	a.Notes
From mst_patient a
Where ptn_pk = @ptn_pk  
--Close symmetric key Key_CTC   
END

GO
	  
/****** Object:  StoredProcedure [dbo].[pr_Clinical_GetPatientSummaryInfo_Futures]    Script Date: 5/12/2016 5:30:58 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_GetPatientSummaryInfo_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_GetPatientSummaryInfo_Futures]
GO
/****** Object:  StoredProcedure [dbo].[pr_Clinical_GetPatientSummaryInfo_Futures]    Script Date: 5/12/2016 5:30:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_Clinical_GetPatientSummaryInfo_Futures]  
      
@PatientId int,  
@ModuleId int,                                                                             
@DBKey varchar(50)    =null                                                                                                                                                                                                                                        
as                                                                                                                                                                                                                                              
                                                                                                                                        
begin                                                                                                                                                                                                                          
        
declare @CareEnded int                                                                                                                                                               
Declare @SymKey varchar(400)
  
Declare @ARTEndStatus varchar(50)
--Set @SymKey = 'Open symmetric key Key_CTC decryption by password=' + @DBKey + ''
--Exec (@SymKey)
-- Table 0 Patients Names                                                                                                                                                                       
Select Top 1	a.Ptn_Pk
				,convert(varchar(50), decryptbykey(a.lastName)) + ',' +
				convert(varchar(50), decryptbykey(a.firstname)) + ' ' +
				isnull(convert(varchar(50), decryptbykey(a.middlename)),'') [Names]
				,convert(varchar(50), decryptbykey(a.Address)) [Address]
				,convert(varchar(50), decryptbykey(a.Phone)) [phone]
				,a.CountryId + '-' + a.PosId + '-' + a.SatelliteId + '-' + a.PatientEnrollmentId [PatientEnrollmentId]
				,a.PatientFacilityID
				,a.PatientClinicId
				,a.RegistrationDate
				,a.Status
				,a.IQNumber [IQNumber]
				,datediff(yy, a.dob, getdate()) [AGE]
				,datediff(Month, a.dob, getdate()) [AgeInMonths]
				,dbo.fn_getpatientage(a.ptn_pk) [AGEINYEARMONTH]
				,dbo.fn_GetPatientAgeInYearsMonth(a.Ptn_Pk) AgeText
				,b.Name [SexNM]
From mst_patient a
Left Outer Join mst_decode b On a.sex = b.id
Where a.Ptn_Pk = @PatientId
--Table 1                                                                                                                                           
---Query to use for Weight, Height, BMI, BSA                                                 
Select Top 3 *
From (
	Select	a.Ptn_Pk
			,a.Height																											[Height]
			,a.Weight																											[Weight]
			,b.visit_Id																											[VisitID]
			,convert(decimal(18, 2), round((nullif(a.Weight, 0) / (nullif(a.height / 100, 0) * nullif(a.height / 100, 0))), 2))	As BMI
			,convert(decimal(18, 2), sqrt(nullif(a.Weight, 0) * nullif(a.height, 0) / 3600))									As BSA
			,b.visitType																										[VisitType]
			,b.VisitDate																										[Visit_OrderbyDate]
	From	dtl_patientvitals a
			,ord_visit b
	Where a.visit_pk = b.visit_Id
	And (b.DeleteFlag = 0 Or b.DeleteFlag Is Null)
	And a.ptn_pk = @PatientId
	And a.Height Is Not Null
	And a.Weight Is Not Null
	And (a.Height <> 0 Or a.Weight <> 0) 
) As inLineView
Order By Visit_OrderbyDate Desc
--Table 2                       
Select Top 3	a.Ptn_Pk
				,dbo.fn_GetPatientCurrentARTRegimen_Constella(a.ptn_pk) [Current ARV Regimen]
				,dbo.fn_GetPatientCurrentARTStartDate_Constella(a.ptn_pk) [Current ARV StartDate]
				,a.ARTStartDate [AidsRelief ARV StartDate]
				,b.currentartstartdate [Hist ARV StartDate]
				,c.ARTStartDate [Hist ARV StartDateCTC]
From mst_patient a
Left Outer Join dtl_patienthivprevcareie b On a.ptn_pk = b.ptn_pk
Left Outer Join dtl_PatientHivPrevCareEnrollment c On a.ptn_pk = c.ptn_pk
Where (a.deleteflag = 0 Or a.deleteflag Is Null)
And a.ptn_pk = @PatientId
--table 3 regimens  
Select Top 3 *
From (
		Select	b.CurrentART [RegimenType]
				,b.CurrentARTStartDate [VisitDate]
				,dbo.fn_GetSideEffects_Futures(@PatientId, b.CurrentARTStartDate) As SideEffects
		From	ord_visit a
				,dtl_PatientHivPrevCareIE b
		Where a.ptn_pk = b.ptn_pk
		And a.visit_id = b.visit_pk
		And b.CurrentART Is Not Null
		And b.CurrentART != ''
		And a.ptn_pk = @PatientId
	 Union 
		Select	a.RegimenName [RegimenType]
				,b.RegistrationDate [VisitDate]
				,dbo.fn_GetSideEffects_Futures(@PatientId, b.RegistrationDate) As SideEffects
		From	mst_Regimen a
				,mst_patient b
				,dtl_patienthivprevcareie c
		Where a.RegimenId = c.InitialRegimenCode
		And b.Ptn_Pk = c.Ptn_Pk
		And c.initialRegimenCode <> 0
		And c.Ptn_Pk = @PatientId
		And (a.RegimenName <> '' And a.RegimenName Is Not Null)
	Union 
	Select	a.CurrentART [RegimenType]
			,a.CurrentARTStartDate [VisitDate]
			,dbo.fn_GetSideEffects_Futures(@PatientId, a.CurrentARTStartDate) As SideEffects
	From	mst_patient b
			,dtl_patienthivprevcareie a
	Where a.Ptn_Pk = b.Ptn_Pk
	And (a.PrevARVRegimen <> '' Or a.PrevARVRegimen Is Not Null)
	And (a.CurrentART Is Not Null And a.CurrentART <> '')
	And a.Ptn_Pk = @PatientId
	Union 
	Select	b.RegimenType [RegimenType]
			,a.DispensedByDate [VisitDate]
			,dbo.fn_GetSideEffects_Futures(@PatientId, a.DispensedByDate) As SideEffects
	From	ord_PatientPharmacyOrder a
			,dtl_RegimenMap b
			,dtl_PatientPharmacyOrder c
	Where a.ptn_pk = b.Ptn_Pk
	And a.ptn_pharmacy_pk = b.orderid
	And a.ptn_pharmacy_pk = c.ptn_pharmacy_pk
	And (b.RegimenType <> '' And b.RegimenType Is Not Null)
	And (a.deleteflag = 0 Or a.deleteflag Is Null)
	And a.ptn_pk = @PatientId
	And a.DispensedByDate Is Not Null
) a
Order By a.visitdate Desc
----table 4 Allergies  
Select Distinct	a.Ptn_Pk
				,cast(convert(varchar, c.VisitDate, 111) As datetime) VisitDate
				,b.Name
From dtl_PatientAllergy a
Inner Join mst_Decode b On a.AllergyID = b.ID
Inner Join ord_visit c On a.visit_pk = c.visit_id
Where a.Ptn_Pk = @PatientId
Order By cast(convert(varchar, c.VisitDate, 111) As datetime) Desc
--table 5 OIs   
Select Distinct	cast(convert(varchar, c.VisitDate, 111) As datetime) VisitDate
				,b.Name
From dtl_PatientDisease a
Inner Join mst_bluedecode b On a.Disease_Pk = b.ID
Inner Join ord_visit c On a.visit_pk = c.visit_id
Where a.Ptn_Pk = @PatientId
And b.CodeID = 4
And (b.DeleteFlag = 0 Or b.DeleteFlag Is Null)
Order By cast(convert(varchar, c.VisitDate, 111) As datetime) Desc
--table 6 Care end  
Select Top 1 @CareEnded = CareEnded
From VW_PatientCareEnd
Where (CareEnded Is Not Null Or CareEnded <> 0)
And ptn_pk = @PatientId
And ModuleId = @ModuleId
Order By CareEndedId Desc

Select Case
		When @CareEnded > 0 Then 'Care Ended'
		Else 'Active'
	End As Status

--table7 Indentifier                                                  
Exec pr_GetModuleIdentifiers	@ModuleId
								,@PatientId

--table 8 Pharmacy
Select Top 5	D.Drug_Pk ItemId
				,G.DrugName ItemName
				,(
					Select Name
					From Mst_DispensingUnit DU
					Where DU.Id = G.DispensingUnit
				)
				DispensingUnitName
				,D.SingleDose Dose
				,O.OrderedByDate
				,D.OrderedQuantity
				,D.Duration
				,O.DispensedByDate DispensedDate
				,D.DispensedQuantity QtyDisp
				,(
					Select Name
					From Mst_Batch B
					Where B.ID = D.BatchNo
				)
				Batch
				,(Select Name From mst_Frequency F Where F.Id = D.FrequencyID) Frequency
				,D.ExpiryDate
				,D.PatientInstructions Instructions
				,O.VisitID
				,Case
					When DispensedQuantity Is Null Or DispensedQuantity = 0.0 Then 0
					Else 1
				End OrdRank
From dtl_PatientPharmacyOrder D
Inner Join ord_PatientPharmacyOrder O On O.ptn_pharmacy_pk = D.ptn_pharmacy_pk
Inner Join mst_drug G On G.Drug_pk = D.Drug_Pk
Where O.Ptn_pk = @PatientId
Order By O.OrderedByDate Desc, OrdRank Desc
--table 9 Lab	
Select	0 TestId
		,'' TestName
		,getdate() OrderDate
		,0 OrderBy
		,'' TestResult
		,getdate() resultDate
		,'' ResultNote
Where 1 = 0
--table 10 ClinicalNote
Select Top 5	ClinicalNotes NoteText
				,(
					Select VisitDate
					From Ord_visit O
					Where O.Visit_Id = C.Visit_Pk
				)
				NoteDate
				,CreateDate EntryDate
				,(
					Select U.UserFirstName + ', ' + U.UserLastName
					From Mst_user U
					Where U.UserId = C.UserId
				)
				NoteBy
				,(
					Select M.ModuleName
					From mst_module M
					Where M.ModuleID = C.ModuleId
				)
				ModuleName
				,Visit_pk VisitId
From dtl_PatientClinicalNotes As C
Where (Ptn_pk = @PatientId)
And DeleteFlag = 0;
--table 11 Appointment
Select Top 5	PA.AppDate
				,(
					Select M.ModuleName
					From mst_module M
					Where M.ModuleId = PA.ModuleId
				)
				ModuleName
				,(
					Select Name
					From mst_decode
					Where CodeID = 26
					And Id = PA.AppReason
				)
				Reason
				,(
					Select Name
					From mst_decode
					Where CodeID = 3
					And Id = PA.AppStatus
				)
				AppStatus
From dtl_PatientAppointment PA
Where PA.Ptn_pk = @PatientId And deleteflag=0
Order By appdate Desc;

--table 12 BP
Select Top 3	V.BPSystolic
				,V.BPDiastolic
				,O.VisitDate
				,O.Visit_Id
From dtl_PatientVitals V
Inner Join ord_Visit O On O.Visit_Id = V.Visit_pk
Where V.BPDiastolic Is Not Null
And v.BPDiastolic > 0
And o.DeleteFlag = 0
And O.Ptn_Pk = @PatientId
Order By O.VisitDate Desc

End

GO




/****** Object:  StoredProcedure [dbo].[pr_Clinical_FindItemByName]    Script Date: 12/03/2015 07:40:10 ******/
Set Ansi_nulls On
Go

Set Quoted_identifier On
Go

-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 13 June 2014
-- Description:	Search for items by name
-- =============================================
Create PROCEDURE [dbo].[pr_Clinical_FindItemByName] 
	-- Add the parameters for the stored procedure here
	@SearchText varchar(15)  
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

-- Insert statements for procedure here
If ltrim(rtrim(@SearchText)) <> '' Select @SearchText = '%' + @SearchText + '%';

Select	I.Item_PK ItemID,
		I.ItemName,
		I.ItemTypeID,
		0 SellingPrice,
		'Pharmaceuticals' ItemTypeName	
From dbo.Mst_Consumables I
Where I.ItemName Like @SearchText
	And I.DeleteFlag = 0 
	Union All 
Select	I.Item_PK ItemId,
		I.ItemName,
		I.ItemTypeID,
		0 SellingPrice,
		'Billables' ItemTypeName
From Mst_ItemMaster I
	Inner Join Mst_ItemType T On I.ItemTypeID = T.ItemTypeID
Where I.ItemName Like @SearchText
	And T.ItemName = 'Billables'
	And I.DeleteFlag = 0


End
Go
/****** Object:  StoredProcedure [dbo].[pr_Clinical_GetMaxAutopopulatIdentifier]    Script Date: 12/11/2014 16:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_GetMaxAutopopulatIdentifier]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pr_Clinical_GetMaxAutopopulatIdentifier] 
(
@columnname varchar(200)=null
)
as
begin
declare @sql varchar(500)
set @sql=''select max(convert(int,''+@columnname+'')) from mst_patient''
exec(@sql)
end' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Clinical_GetPatientItemsByUserID]    Script Date: 04/30/2015 11:53:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_GetPatientItemsByUserID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Joseph Njung''e
-- Create date: 23 Apr 2015
-- Description:	Get items issued to patient by a particular user
-- =============================================
CREATE PROCEDURE [dbo].[pr_Clinical_GetPatientItemsByUserID] 
	-- Add the parameters for the stored procedure here
	@PatientID int , 
	@LocationID int ,
	@UserID int ,
	@IssueDate datetime = null,
	@ItemTypeID int =null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select	
		O.PatientItemID,
		O.ptn_pk As PatientID,
		O.DateIssued IssueDate,
		O.ItemName,
		O.ItemID ,
		O.ItemTypeID,
		O.Quantity IssuedQuantity,
		O.SellingPrice ,	
		D.Quantity * D.SellingPrice BilledAmount,	
		O.CreateDate,
		O.LocationID,
		O.UserID,
		U.UserFirstName +'', ''+U.UserLastName As IssuedByName,
		D.billItemID,
		D.BillID,		
		Convert(bit,Isnull(D.ServiceStatus,1)) ServiceStatus,
		Convert(bit,D.PaymentStatus) PaymentStatus	,
		~Convert(bit,Isnull(D.DeleteFlag,0)) As Active	,
		D.TransactionId,
		O.ModuleID,
		Case 
			When O.ModuleID = 0  And I.ItemName =''Pharmaceuticals'' Then ''Pharmacy''
			When O.ModuleID = 0  And I.ItemName =''Ward Admission'' Then ''Inpatient''
			When O.ModuleID = 0  And I.ItemName =''Lab Tests'' Then ''Laboratory''
			Else M.ModuleName
		End As CostCenterName 
	From 	dbo.dtl_PatientItemsOrder O 
	--Inner Join dbo.vw_Master_ItemList As I
	--	On I.ItemID = O.ItemID And I.ItemTypeID = O.ItemTypeID
	Inner Join dbo.Mst_ItemType As I
	On I.ItemTypeID = O.ItemTypeID
	Inner Join
		dbo.mst_User U On U.UserID = O.UserID
	Left Outer Join	
		mst_Module M ON M.ModuleID = O.ModuleID
	Left Outer Join		dbo.dtl_Bill As D
		On O.ptn_Pk= D.Ptn_PK 
		And O.ItemID = D.ItemId 
		And O.ItemTypeID =  D.ItemType
		And O.UserID = D.CreatedBy
		And O.PatientItemID = D.ItemSourceReferenceID	
	Where (O.ptn_pk = @PatientID) 
	And O.LocationID = @LocationID
	And (O.DateIssued = @IssueDate OR @IssueDate Is Null) 
	And (O.DeleteFlag = 0)
	And O.UserID = @UserID;
END
' 
END
GO

/****** Object:  StoredProcedure [dbo].[pr_Clinical_PatientConsumablesByDate]    Script Date: 12/11/2014 16:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_PatientConsumablesByDate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Joseph Njung''e
-- Create date: 16 June 2014
-- Description:	Get Patients consumables for the date
-- =============================================
CREATE PROCEDURE [dbo].[pr_Clinical_PatientConsumablesByDate] 
	-- Add the parameters for the stored procedure here
	@Ptn_PK int , 
	@IssueDate datetime 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select	--billItemID BillID,
		D.PatientItemID,
		D.ptn_pk As PatientID,
		D.DateIssued IssueDate,
		I.ItemName,
		I.Item_PK,
		D.ItemTypeID ItemType,
		D.Quantity,
		D.SellingPrice UnitSellingPrice,
		D.SellingPrice * D.Quantity As Amount,	
		M.ModuleName ,
		D.CreateDate,
		D. UserId CreatedBy,
		U.UserFirstName +'', ''+U.UserLastName As IssuedBy
	
	From 
	dbo.dtl_PatientItemsOrder As D
		--On D.BillID = B.BillID
	Inner Join dbo.Mst_ItemMaster As I
		On I.Item_PK = D.ItemID
	Inner Join
		dbo.mst_User U On U.UserID = D.UserID
	Left Outer Join
		dbo.mst_module M On M.ModuleID = D.ModuleID
	Where (D.ptn_pk = @Ptn_PK) And (D.DateIssued  = @IssueDate) And (D.DeleteFlag = 0);
END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Clinical_IssueItemToPatient]    Script Date: 12/11/2014 16:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_IssueItemToPatient]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Joseph Njung''e
-- Create date: 25 Jun 2014
-- Description:	Issue Items to a patient
-- =============================================
CREATE PROCEDURE [dbo].[pr_Clinical_IssueItemToPatient]
	-- Add the parameters for the stored procedure here
	@ItemID int,
	@ItemTypeID int,
	@ItemName varchar(250),
	@PatientID int ,
	@LocationID int,
	@DateIssued datetime,
	@ModuleID int,
	@Quantity int ,
	@SellingPrice decimal(18,2)=0,
	@UserID int
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

-- Insert statements for procedure here

--	declare @SellingPrice decimal(18,2), @PatientItemID int;

--Select @SellingPrice = C.ItemSellingPrice


--From dbo.lnk_ItemCostConfiguration C
--Where C.ItemId = @ItemID And C.ItemType = @ItemTypeID And C.PriceStatus = 1;

--if(@SellingPrice Is Null)
--	Select @SellingPrice = 0;

Insert Into [dbo].[dtl_PatientItemsOrder] (
	ItemID,
	ItemTypeID,
	ItemName,
	ptn_Pk,
	LocationID,
	DateIssued,
	ModuleID,
	Quantity,
	SellingPrice,
	UserID,
	CreateDate,
	DeleteFlag)
Values (
	@ItemID,
	@ItemTypeID,
	@ItemName,
	@PatientID,
	@LocationID,
	@DateIssued,
	@ModuleID,
	@Quantity,
	@SellingPrice,
	@UserID,
	Getdate(),
	0);

Select  Scope_identity() as PatientItemID;

END
' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Clinical_DeleteItemIssuedToPatient]    Script Date: 02/03/2015 15:18:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_DeleteItemIssuedToPatient]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Joseph Njung''e
-- Create date: 2014-Apr-02
-- Description:	Procedure for  dleleting an item from patients items issue 
-- =============================================
Create PROCEDURE [dbo].[pr_Clinical_DeleteItemIssuedToPatient](@ItemIssueID int, @UserID int)

AS
BEGIN
	UPDATE dbo.dtl_PatientItemsOrder set DeleteFlag=1,UserID=@userID where PatientItemID =@ItemIssueID
END

' 
END
GO
 /****** Object:  StoredProcedure [dbo].[Pr_Clinical_GetPatientSearchresults]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_Clinical_GetPatientSearchresults]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_Clinical_GetPatientSearchresults]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Joseph Njung'e>
-- Create date: <08-08-2014,>
-- Description:	<Modified Find Patient>
-- =============================================
CREATE PROCEDURE [dbo].[Pr_Clinical_GetPatientSearchresults]
	-- Add the parameters for the stored procedure here
	@Sex int = Null, 
	@Firstname varchar(50) = Null, 
	@LastName varchar(50) = Null, 
	@MiddleName varchar(50) = Null, 
	@DOB datetime = Null, 
	@RegistrationDate datetime = Null,
	@EnrollmentType int = null,
	@EnrollmentId varchar(50) = Null, 
	@FacilityId int = Null,  
	@Status int = Null,
	@Password varchar(50) = Null,    
	@ModuleId int = 999,
	@FilterByModuleId bit= 0,
	@top int = 100,
	@RuleFilter varchar(400)=''
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Declare @Query nvarchar(max), 
			@ParamDefinition nvarchar(2000),
			@Identifiers varchar(4000),
			@ByModule varchar(1000), @ByStatus varchar(120);
			
	Declare @SymKey nvarchar(400)	;	 
	Select	@Identifiers = '',@ByModule='',@ByStatus=' Status =  Null ,';
			
	 --                                                                                    
	 --Set @SymKey = 'Open symmetric key Key_CTC decryption by password='+ @password + ''                                                                                        
	 --Exec(@SymKey) 
	 
	
Select @Firstname = Nullif(Ltrim(Rtrim(@Firstname)), '');
If  (@Firstname Is Not Null) Select @Firstname = '%' + @Firstname + '%';

Select @MiddleName = Nullif(Ltrim(Rtrim(@MiddleName)), '');
If  (@MiddleName Is Not Null) Select @MiddleName = '%' + @MiddleName + '%';

Select @LastName = Nullif(Ltrim(Rtrim(@LastName)), '')
If  (@LastName Is Not Null) Select @LastName ='%' + @LastName + '%';

Select @EnrollmentId = Convert(varchar,Nullif(Ltrim(Rtrim(@EnrollmentID)), ''));


If (@EnrollmentId Is Not Null) 
Begin
	declare @SS varchar(1000)
	Select @ss = Substring((Select ',P.[' + Convert(varchar(Max), FieldName) + ']'
			From dbo.mst_patientidentifier
			Order By Id
			For xml Path (''))
		, 2, 1000);
	Select @Identifiers = ' AND(' + Replace(@SS, ',', ' = ''' + Convert(varchar,@enrollmentid) + ''' or ') + ' = ''' 
	+ Convert(varchar,@enrollmentid) + ''' or P.IQNumber=''' + Convert(varchar,@enrollmentid) 
	+ ''' or P.PatientFacilityID = ''' + Convert(varchar,@enrollmentid) + ''')';


End
If(@ModuleID <> 999)
Begin
	Select @ByModule= ' Left Outer Join (Select	P.Ptn_pk,P.ModuleId,P.StartDate EnrollmentDate,	Case CT.CareEnded When 1 Then ''Care Ended'' When 0 Then ''Restarted''  Else ''Active'' End CareStatus,		
		CT.PatientExitReasonName CareEndReason,	Isnull(CT.EnrollmentIndex,1)EnrollmentIndex From dbo.Lnk_PatientProgramStart As P
Left Outer Join (Select	CE.Ptn_Pk,	CE.CareEnded,	CE.PatientExitReason,	D.Name As PatientExitReasonName,CE.CareEndedDate,TC.TrackingID,
TC.ModuleId	,Row_number() Over(Partition By TC.Ptn_Pk Order By TC.TrackingId Desc) EnrollmentIndex From dbo.dtl_PatientCareEnded As CE Inner Join	dbo.dtl_PatientTrackingCare As TC On TC.TrackingID = CE.TrackingId
Inner Join	dbo.mst_Decode As D On D.ID = CE.PatientExitReason Where TC.ModuleId = @ModuleID ) As CT On CT.Ptn_Pk = P.Ptn_pk And CT.ModuleId = P.ModuleId Where P.ModuleID=@ModuleID ) CT On CT.Ptn_Pk=P.Ptn_Pk And CT.ModuleID=@ModuleID And EnrollmentIndex=1'

Select @ByStatus = ' [Status] = Case When CT.ModuleID Is Null Then ''Not Enrolled'' Else IsNull(CT.CareEndReason,CT.CareStatus) End , '
End


Set @Query=N'Select Top (@top) * From (Select  P.Ptn_Pk PatientId,Convert(varchar(50), Decryptbykey(FirstName)) As FirstName, Convert(varchar(50), Decryptbykey(MiddleName)) As Middlename,
		Convert(varchar(50), Decryptbykey(LastName)) As LastName,IQNumber, PatientFacilityId, LocationId,	F.FacilityName,
		Case DOBPrecision	When 0 Then ''No'' When 1 Then ''Yes'' End As [Precision],Dob ,	Convert(varchar(100), Decryptbykey([Address])) [Address],	Convert(varchar(100), Decryptbykey(Phone)) [Phone],
		convert(decimal(5,2),round(cast(datediff(dd,P.DOB,isnull(P.DateofDeath,getdate()))/365.25 as decimal(5,2)),2)) Age,	P.RegistrationDate,'+@ByStatus +' Sex = Case P.Sex When 16 Then ''Male'' Else ''Female'' End
From dbo.mst_Patient As P Inner Join dbo.mst_Facility F	On F.FacilityID = P.LocationID'+ @ByModule +' Where  (P.DeleteFlag = 0 OR P.DeleteFlag Is Null)
And Case When @FirstName Is  Null Or Convert(varchar(50), decryptbykey(P.FirstName)) Like  @Firstname Then 1 Else 0 End = 1
And Case When @LastName Is  Null Or Convert(varchar(50), decryptbykey(P.LastName)) Like  @LastName Then 1	Else 0 End = 1
And Case When @MiddleName Is  Null Or Convert(varchar(50), decryptbykey(P.MiddleName)) Like  @MiddleName Then 1	Else 0 End = 1
And (@DOB Is Null Or P.DOB = @DOB) And (@RegistrationDate Is Null Or P.RegistrationDate= @RegistrationDate) And (@Sex Is Null Or P.Sex = @Sex) And (@Status Is Null Or P.[Status] = @status)
And (@FacilityID Is Null Or P.LocationID=@FacilityID)' +@Identifiers + ') P '+ @RuleFilter  ;

Set @Query = @Query + ' Order By [Status],P.RegistrationDate';
	  
Set @ParamDefinition= N'@Sex int = Null, 
	@Firstname varchar(50) = Null, 
	@LastName varchar(50) = Null, 
	@MiddleName varchar(50) = Null, 
	@DOB datetime = Null, 
	@RegistrationDate datetime = Null,
	@EnrollmentID varchar(50) = Null,  
	@FacilityID int = Null,  
	@Status int = Null,
	@Password varchar(50) = Null,    
	@ModuleID int = 999,
	@top int=100 ';
							 
--Set @SymKey = 'Open symmetric key Key_CTC decryption by password=' + @password ;
--Exec sp_executesql @SymKey ;
Execute sp_Executesql @Query, @ParamDefinition, @Sex, @Firstname,@LastName,@MiddleName,@DOB,@RegistrationDate,@EnrollmentID,@FacilityID,@status,@password,@moduleId,@top;
	 
End	   
	  
GO

	
/****** Object:  StoredProcedure [dbo].[Pr_Clinical_GetDuplicatePatientSearchresults_COnstella]    Script Date: 01/07/2015 16:24:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_Clinical_GetDuplicatePatientSearchresults_COnstella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_Clinical_GetDuplicatePatientSearchresults_COnstella]
GO	  
/****** Object:  StoredProcedure [dbo].[Pr_Clinical_GetDuplicatePatientSearchresults_COnstella]    Script Date: 01/07/2015 16:24:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Joseph Njung''e>
-- Create date: <08-Dec-2014,>
-- Description:	<Modified Find Patient Duplicate to allow name with special characters like
---------------- apostrophes >
-- =============================================
CREATE PROCEDURE [dbo].[Pr_Clinical_GetDuplicatePatientSearchresults_COnstella]                                                     
(                                                    
	@Lastname varchar(50)=Null,                        
	@Middlename varchar(50)=Null,                                                    
	@Firstname varchar(50)=Null,                                                    
	@Address varchar(100)=Null,        
	@Phone   varchar(100)=Null,        
	@Password varchar(50)=Null                        
                                                    
)                                                    
AS                                                    
BEGIN  

Declare @Query nvarchar(4000), @ParamDefinition nvarchar(1000), @SymKey nvarchar(400),@Proceed int	;	
Set @Proceed = @Proceed + 1;

Select @Firstname = Nullif(Ltrim(Rtrim(@Firstname)), '');
If  (@Firstname Is Not Null) Begin Select @Firstname =  @Firstname + '%'; Set @Proceed = @Proceed + 1; End

Select @MiddleName = Nullif(Ltrim(Rtrim(@MiddleName)), '');
If  (@MiddleName Is Not Null) Begin Select @MiddleName =  @MiddleName + '%'; Set @Proceed = @Proceed + 1; End

Select @LastName = Nullif(Ltrim(Rtrim(@LastName)), '')
If  (@LastName Is Not Null) Begin Select @LastName = @LastName + '%'; Set @Proceed = @Proceed + 1; End

Select @Phone = Nullif(Ltrim(Rtrim(@Phone)), '')
If  (@Phone Is Not Null) Begin Select @Phone = @Phone + '%'; Set @Proceed = @Proceed + 1; End

Select @Address = Nullif(Ltrim(Rtrim(@Address)), '')
If  (@Address Is Not Null) Begin Select @Address = @Address + '%'; Set @Proceed = @Proceed + 1; End

--Proceed only when atleast 2 fields are specified

--Select 
--	@Proceed =  Case When Sum(Case When P Is Null Then 0 Else 1 End)< 2 Then 0 Else 1 End -- Case When Count(p) < 2 Then 0 Else 1 End
--From(
--	Values (Convert(sql_variant, @Firstname)),
--		(@LastName),
--		(@MiddleName),
--		(@Phone),
--		(@Address)
--	) T (P);

	Set Rowcount  10;
	Select	FirstName
		,	LastName
		,	MiddleName
		,	RegistrationDate
		,	DOB
		,	Sex
		,	DobPrecision
		,	DateOfDeath
		,	MaritalStatus		
		,	[Address]
		,	Phone
		,	P.IQNumber
		,	F.FacilityName
		,	PatientFacilityId		
	From PatientView As P Inner Join mst_Facility F On P.LocationId = F.FacilityID
	Where (P.DeleteFlag = 0)
		And (Case
			When @FirstName Is Null Or
				convert(varchar(50), p.FirstName) Like @Firstname Then 1
			Else 0
		End = 1)
		And (Case
			When @LastName Is Null Or
				convert(varchar(50), (P.LastName)) Like @LastName Then 1
			Else 0
		End = 1)
		And (Case
			When @MiddleName Is Null Or
				convert(varchar(50), (P.MiddleName)) Like @MiddleName Then 1
			Else 0
		End = 1)
		And (Case
			When @Address Is Null Or
				convert(varchar(100), (P.[Address])) Like @Address Then 1
			Else 0
		End = 1)
		And (Case
			When @Phone Is Null Or
				convert(varchar(100), (P.Phone)) Like @Phone Then 1
			Else 0
		End = 1)
		And (@Proceed > 1)
	Order By PatientFacilityId

	Set Rowcount 0

                 
                                          
END

GO

/****** Object:  StoredProcedure [dbo].[pr_Clinical_GetPatientRecord_Futures]    Script Date: 12/11/2014 16:16:40 ******/
/****** Object:  StoredProcedure [dbo].[pr_Clinical_GetPatientRecord_Futures]    Script Date: 6/9/2016 9:19:58 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[pr_Clinical_GetPatientRecord_Futures]  
@PatientId Int,  
@Password varchar(50)  
as  
Begin
  

--                               
Select	Convert(varchar(50), Decryptbykey(a.FirstName)) As [FirstName],
		Convert(varchar(50), Decryptbykey(a.LastName)) As [LastName],
		Convert(varchar(50), Decryptbykey(a.Middlename)) As [Middlename],
		a.Ptn_Pk PatientId,
		a.LocationID,
		a.CountryID,
		a.PosID,
		a.SatelliteID,
		a.PatientEnrollmentID,
		a.PatientClinicID,
		a.IQNumber,
		a.PatientFacilityID,
		a.ReferredFrom,
		Isnull(a.ReferredFromSpecify, 0) [ReferredFromSpecify],
		a.RegistrationDate 'RegistrationDate',
		a.Sex,
		PatientSex = Case a.Sex When 16 Then 'Male' When 17 Then 'Female' Else '' End,
		a.DOB 'DOB',
		Convert(varchar, Datediff(Month, a.DOB, Getdate()) / 12) [Age],
		Convert(varchar, Datediff(Month, a.DOB, Getdate()) % 12) [Age1],
		a.DateOfDeath,
		Datediff(Month, a.dob, a.RegistrationDate) / 12 'EnrolAge',
		Datediff(Month, a.dob, a.RegistrationDate) % 12 'EnrolAge1',
		a.DobPrecision,
		TransferIn,
		LPTFTransferId,
		a.LocalCouncil,
		a.VillageName,
		a.DistrictName,
		a.Province,
		Convert(varchar(50), Decryptbykey(a.Address)) As [Address],
		Convert(varchar(50), Decryptbykey(a.Phone)) As [Phone],
		a.MaritalStatus,
		a.EducationLevel,
		a.Literacy,
		a.EmployeeID,
		a.Status,
		a.StatusChangedDate,
		a.Notes,
		a.DeleteFlag
From mst_patient a
Where ptn_pk = @patientid



End
GO
/****** Object:  StoredProcedure [dbo].[Pr_Clinical_GetPatient_EnrolledServiceLines]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_Clinical_GetPatient_EnrolledServiceLines]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_Clinical_GetPatient_EnrolledServiceLines]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Joseph Njung'e
-- Create date: 11 Aug 2014
-- Description:	Get servicelines where a patient is enrolled and the identifiers
---				need to move to their own tables
-- =============================================
CREATE PROCEDURE [dbo].[Pr_Clinical_GetPatient_EnrolledServiceLines] 
	-- Add the parameters for the stored procedure here
	@PatientId int, 
	@LocationId int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare @FieldList varchar(2000)
	Declare @ColumnList xml;
	-- Get all the identifiers from mst_patient:: need to move to their own tables
	Select @FieldList = Substring((Select ',' + Convert(varchar(Max), FieldName) + ''
			From dbo.mst_patientidentifier
			Order By Id
			For xml Path (''))
		, 2, 2000);	

	Declare @Columns Table(name varchar(50));
	Declare @PatientValues Table(Columnname varchar(50), ColumnValue varchar(50));
	Insert Into @Columns
		Select C.f.value('.', 'varchar(50)') ColumnName
		From (Select Convert(xml, '<Fields><field>' +
				Replace(@FieldList, ',', '</field><field>') + '</field></Fields>') ColumnList) F
		Cross Apply
			F.ColumnList.nodes('/Fields/field') As C (f);
	
	-- Get the patient dataset
	Set @ColumnList = (Select *
		From dbo.mst_Patient
		Where Ptn_Pk = @PatientID
		For xml Path ('patient'))
	
	-- Retrieve the patient identifiers
		
	Insert Into @PatientValues (
		Columnname,
		ColumnValue)
	Select	Columname = P.c.value('local-name(.)', 'varchar(50)'),
			ColumnValue = Nullif(P.c.value('(.)', 'varchar(50)'),'')
	From @ColumnList.nodes('/patient/*') As P (c);

	-- match column names with service lines identifiers

	Select 
		M.ModuleId, 
		P.PatientId,
		P.LocationId,
		P.ModuleName, 
		DisplayName,
		P.StartDate As EnrollmentDate,
		P.CareStatus,
		ExitReason,
		M.FieldID,
		M.FieldName, 
		M.FieldLabel IdentifierName, 
		PV.ColumnValue IdentifierValue 
	From 
	(
		Select	PS.ModuleId
			,	PS.Ptn_pk		As PatientId
			,	F.FacilityID	As LocationId
			,	M.ModuleName
			,	M. DisplayName
			,	PS.StartDate
			--, (Select 1 From dtl_PatientCareEnded C Where C.Ptn_Pk=@PatientId And C.TrackingId = TC.TrackingId And C.CareEnded
			,	Case When TC.CareEnded = 1 Then 'Care Ended'
					 Else 'Active'
				End				As CareStatus
			, TC.ExitReasonName ExitReason
		From mst_module As M
		Inner Join Lnk_PatientProgramStart As PS On PS.ModuleId = M.ModuleId
		Inner Join lnk_FacilityModule As F On F.ModuleID = PS.ModuleId
		Left Outer Join(
			Select Tc.TrackingID
				, TC.Ptn_Pk
				, TC.LocationId
				, TC.ModuleId
				, TC.DateLastContact
				, C.CareEnded
				, C.PatientExitReason ExitReasonId
				, (Select Name From mst_Decode D Where CodeId=23  And D.Id = C.PatientExitReason) ExitReasonName
				, C.CareEndedDate
				,CareEndedId
			From dtl_PatientCareEnded C 
				Inner Join	dtl_PatientTrackingCare As TC On TC.TrackingID = C.TrackingId 
			Where C.Ptn_Pk = TC.Ptn_Pk 

		 ) TC On TC.Ptn_Pk = PS.Ptn_pk		And TC.LocationId = F.FacilityID		And TC.ModuleId = PS.ModuleId
		Where (PS.Ptn_pk = @PatientID)
			And (F.FacilityID = @LocationID)
	)P
	Inner Join
	(Select	M.ModuleID ModuleId,		
			PMI.FieldID FieldId,
			MPI.FieldName,
			MPI.Label As FieldLabel			
	From
		dbo.mst_module As M 
	Inner Join
		dbo.lnk_PatientModuleIdentifier As PMI On M.ModuleId = PMI.ModuleID
	Inner Join
		dbo.mst_PatientIdentifier As MPI On MPI.ID = PMI.FieldID		
		) M On M.ModuleID = P.ModuleId 
	Inner Join
	 @PatientValues PV On PV.Columnname = M.FieldName Where PV.ColumnValue Is Not Null;

END


GO


/****** Object:  StoredProcedure [dbo].[pr_Clinical_Get_ZScores]    Script Date: 12/11/2014 16:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_Get_ZScores]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================      
-- Author:  John Macharia      
-- Create date:       
-- Modify date: 31 July 2014    
-- Description: Paediatric  scores    
-- =============================================  
Create procedure [dbo].[pr_Clinical_Get_ZScores]
(
	@Ptn_pk int=null,
	@sex varchar(10)=null,
	@height float = null
)

As

begin
Declare @ageInDays int, @ageInMonths int;

Select @ageInMonths = Datediff(Month, DOB, Getdate()),
	@ageInDays = Datediff(Day, DOB, Getdate())
From mst_Patient
Where Ptn_Pk = @Ptn_pk;


--0 weight for Age
If (@ageInDays <= 1856) 
Begin
	Select *
	From [dbo].[z_waz_young]
	Where Case  When @Sex= ''Female'' And Sex= 2 Then 1
				When @Sex = ''Male'' And Sex = 1 Then 1
				Else 0 End = 1
	And agedays = @ageInDays;
End
Else If(@ageInMonths >=61)
Begin
	Select *
	From [dbo].[z_waz_old]
	Where Case  When @Sex= ''Female'' And Sex= 2 Then 1
				When @Sex = ''Male'' And Sex = 1 Then 1
				Else 0 End = 1
	And ageMos = @ageInMonths;
End
Else
Begin	
	Select 1
End
--0 weight for Age
--If (@sex = ''Female'' And @ageInDays <= 1856) Begin
--Select * From [dbo].[z_waz_young] Where Sex = 2 And agedays = @ageInDays
--End 
--Else If (@sex = ''Male'' And @ageInDays <= 1856) Begin
--Select * From [dbo].[z_waz_young] Where Sex = 1 And agedays = @ageInDays
--End 

--Else If (@sex = ''Female'' And @ageInMonths >= 61) Begin
--Select *
--From [dbo].[z_waz_old]
--Where Sex = 2
--And Agemos = @ageInMonths
--End Else If (@sex = ''Male'' And @ageInMonths >= 61) Begin
--Select *
--From [dbo].[z_waz_old]
--Where Sex = 1
--And Agemos = @ageInMonths
--End Else Begin
--Select 1
--End

--1 weight for Height

If (@height Between 45 And 110) 
Begin
	Select *
	From [dbo].[z_whz_young]
	Where Case  When @Sex= ''Female'' And Sex= 2 Then 1
				When @Sex = ''Male'' And Sex = 1 Then 1
				Else 0 End = 1
	And LengthCm = @height;
End
Else If (@height Between 65 And 120) 
Begin
	Select *
	From [dbo].[z_whz_old]
	Where Case  When @Sex= ''Female'' And Sex= 2 Then 1
				When @Sex = ''Male'' And Sex = 1 Then 1
				Else 0 End = 1
	And HeightCm = @height;
End
Else
Begin	
	Select 1
End

--If (@sex = ''Female'' And @height >= 45 And @height <= 110) Begin
--Select *
--From [dbo].[z_whz_young]
--Where Sex = 2
--And LengthCm = @height
--End 
--Else If (@sex = ''Male'' And @height >= 45 And @height <= 110) Begin
--Select *
--From [dbo].[z_whz_young]
--Where Sex = 1
--And LengthCm = @height
--End 
--Else If (@sex = ''Female'' And @height >= 65 And @height <= 120) Begin
--Select *
--From [dbo].[z_whz_old]
--Where Sex = 2
--And HeightCm = @height
--End Else If (@sex = ''Male'' And @height >= 65 And @height <= 120) Begin
--Select *
--From [dbo].[z_whz_old]
--Where Sex = 1
--And HeightCm = @height
--End Else Begin
--Select 1
--End

/* 2 BMIz */
If (@ageInDays Between 0 And 1856) 
Begin
	Select *
	From [dbo].z_bmiz_young
	Where Case  When @Sex= ''Female'' And Sex= 2 Then 1
				When @Sex = ''Male'' And Sex = 1 Then 1
				Else 0 End = 1
	And agedays = @ageInDays;
End
Else If (@ageInMonths Between 61 And 229) 
Begin
	Select *
	From [dbo].[z_bmiz_old]
	Where Case  When @Sex= ''Female'' And Sex= 2 Then 1
				When @Sex = ''Male'' And Sex = 1 Then 1
				Else 0 End = 1
	And Agemos = @ageInMonths;
End
Else
Begin	
	Select 1
End

--If (@sex = ''Female'' And @ageInDays >= 0 And @ageInDays <= 1856) Begin
--Select *
--From z_bmiz_young
--Where Sex = 2
--And agedays = @ageInDays
--End Else If (@sex = ''Male'' And @ageInDays >= 0 And @ageInDays <= 1856) Begin
--Select *
--From z_bmiz_young
--Where Sex = 1
--And agedays = @ageInDays
--End Else If (@sex = ''Female'' And @ageInMonths >= 61 And @ageInMonths <= 229) Begin
--Select *
--From z_bmiz_old
--Where Sex = 2
--And Agemos = @ageInMonths
--End Else If (@sex = ''Male'' And @ageInMonths >= 61 And @ageInMonths <= 229) Begin
--Select *
--From z_bmiz_old
--Where Sex = 1
--And Agemos = @ageInMonths
--End Else Begin
--Select 1
--End
End' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Security_facilitydetails1_constella]    Script Date: 12/11/2014 16:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Security_facilitydetails1_constella]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'




CREATE procedure  [dbo].[pr_Security_facilitydetails1_constella]                                           
(                                                                                                                                                        
@LocationId int,                                                          
@DBKey varchar(50)                                                                                                                                                                                                                                
)                                                                                                                                          
                                                                                                                                                                
as                                                                                                                                                                
                                                                                                                                                             
begin                                                                                                                                            
Set NoCount on                                                                                                                                                             
Declare @Active int                                              
declare @Indicator varchar(200)                                    
declare @Query varchar(8000)                                    
declare @Tsql varchar(MAX)        
declare @TsqlTable varchar(MAX)                            
declare @ModuleId varchar(100)                                                           
Declare @SymKey varchar(400)        
                                                                                                                                                                                        
Set @SymKey = ''Open symmetric key Key_CTC decryption by password=''+ @DBKey + ''''                                                                                                                                                                                
  
    
      
        
         
            
Exec(@SymKey)                                                          
                                                                                                                                               
if @LocationId < 999                                                                                                                                            
begin                                          
                                        
--Table[0]----Active Patients----------------------------------------------------------------------------------------                                                          
select Distinct                                                          
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                                                                               
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                                         
[Patient Location][Facility], Gender[Sex], a.DOB,                                                           
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                                                                                
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                          
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                      
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                     
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                           
isnull(a.OutPatientNumber,'''')[OutPatient Number], Ptn_Pk                                           
from  VW_PatientDetail a                                         
where a.LocationId = @LocationId and a.ModuleId in (203,12,2)
and [Patient Status]=''Active'' and (dbo.fn_GetPatientProgramStatus_Constella(a.ptn_pk) = ''Non-ART'' or dbo.fn_GetPatientProgramStatus_Constella(a.ptn_pk) = ''Stopped ART'' or dbo.fn_GetPatientProgramStatus_Constella(a.ptn_pk) = ''ART'')
                                              
--Table[1]----Active Non-ART Patients----------------------------------------------------------------------------------------                            
select Distinct                                                          
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                                                                               
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                                         
[Patient Location][Facility], Gender[Sex], a.DOB,                                                           
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                        
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                    
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                        
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                                          
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                      
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                                                  
from  VW_PatientDetail a                                                
where a.LocationId = @LocationId and a.ModuleID in (203,12,2)                                                  
and [Patient Status]=''Active''                                  
and (dbo.fn_GetPatientProgramStatus_Constella(a.ptn_pk) = ''Non-ART'' or dbo.fn_GetPatientProgramStatus_Constella(a.ptn_pk) = ''Stopped ART'')                                              
                                
                                                          
--Table[2]----Active ART Patients----------------------------------------------------------------------------------------                                                          
select Distinct                                                          
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                                                                               
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                                         
[Patient Location][Facility], Gender[Sex], a.DOB,                                                           
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                                                                                
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                          
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                          
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                                          
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                           
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                                   
from  VW_PatientDetail a                                                
where a.LocationId = @LocationId and a.ModuleID in (203,12,2)                                                 
and [Patient Status]=''Active''                       
and dbo.fn_GetPatientProgramStatus_Constella(a.ptn_pk)  = ''ART''                                           
                                        
--Table[3]--Current Mothers in PMTCT -------------------------------------------------------------------------------------------------                                                            
select Distinct                                                      
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                      
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                                     
[Patient Location][Facility], Gender[Sex], a.DOB,                                                       
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                                                        
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                      
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                      
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                 
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                       
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                                              
from  VW_PatientDetail a                                            
where  a.ModuleId=1 and a.LocationId = @LocationId                                                                         
and a.Sex=17 and datediff(dd,a.dob,getdate())/365.25 >= 10                                            
and dbo.fn_GetPatientPMTCTProgramStatus_Futures(a.ptn_pk)= ''PMTCT''                                        
                                        
 --Table[4]--Current Number Women on ARV Prophylaxis--ANC-----------------------------------------------------------------------------------------------                                                                          
                                        
select Distinct                                                      
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                                                                           
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                                     
[Patient Location][Facility], Gender[Sex], a.DOB,                                                       
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                                                                            
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                      
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                      
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                                      
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                       
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                                              
from  VW_PatientDetail a                                            
where a.LocationId = @LocationId and a.ModuleId=1                                                                 
and a.Sex=17 and datediff(dd,a.dob,getdate())/365.25 >= 10                                            
and dbo.fn_GetPatientPMTCTProgramStatus_Futures(a.ptn_pk)= ''PMTCT''                                            
and a.Ptn_Pk in                                                             
 (select q.ptn_pk from mst_patient p                                                      
  inner join ord_visit q on p.ptn_pk = q.ptn_pk                                                                 
     Left Outer Join ord_patientpharmacyorder s on q.visit_id = s.visitid                                        
     Left Outer Join dtl_patientpharmacyorder t on s.ptn_pharmacy_pk = t.ptn_pharmacy_pk                                                             
     where q.visittype in (4,6) and s.progid = 223 and s.pharmacyperiodtaken = 140                                                             
     and s.Ptn_Pharmacy_Pk in(select top 1 Ptn_Pharmacy_pk from                                        
           Ord_PatientPharmacyOrder where Ptn_Pk = p.Ptn_pk and (PharmacyPeriodTaken is not null or PharmacyPeriodTaken <> 0)                                               
           and (DeleteFlag = 0 or DeleteFlag is null) order by Dispensedbydate desc)                                                            
           and t.prophylaxis = 1 and dateadd(dd,90,q.VisitDate) >=getdate() and p.Ptn_Pk = a.Ptn_Pk)                                                                
                                                                     
--Table[5]--Current Number Women on ARV Prophylaxis--L&D-----------------------------------------------------------------------------------------------                                                                          
                                        
select Distinct                                                      
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                                                                           
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                                     
[Patient Location][Facility], Gender[Sex], a.DOB,                                                       
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                                                                     
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                      
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                      
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                                      
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                       
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                           
from  VW_PatientDetail a                                            
where a.LocationId = @LocationId and a.ModuleId=1                                                                          
and a.Sex=17 and datediff(dd,a.dob,getdate())/365.25 >= 10                                            
and dbo.fn_GetPatientPMTCTProgramStatus_Futures(a.ptn_pk)= ''PMTCT''                                                            
and a.Ptn_Pk in                                                             
 (select q.ptn_pk from mst_patient p                                                             
  inner join ord_visit q on p.ptn_pk = q.ptn_pk                                             
     Left Outer Join ord_patientpharmacyorder s on q.visit_id = s.visitid                                                             
     Left Outer Join dtl_patientpharmacyorder t on s.ptn_pharmacy_pk = t.ptn_pharmacy_pk                                                             
     where q.visittype in (4,6) and s.progid = 223 and s.pharmacyperiodtaken = 141                                                             
     and s.Ptn_Pharmacy_Pk in(select top 1 Ptn_Pharmacy_pk from                                                             
            Ord_PatientPharmacyOrder where Ptn_Pk = p.Ptn_pk and (PharmacyPeriodTaken is not null or PharmacyPeriodTaken <> 0)                                           
   and (DeleteFlag = 0 or DeleteFlag is null) order by Dispensedbydate desc)                                                            
            and t.prophylaxis = 1 and dateadd(dd,90,q.VisitDate) >=getdate() and p.Ptn_Pk = a.Ptn_Pk)                                    
                                                                      
--Table[6]--Current Number Women on ARV Prophylaxis--PN-----------------------------------------------------                         
select Distinct                                                      
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                            
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                                     
[Patient Location][Facility], Gender[Sex], a.DOB,                                                       
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                                                                            
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                      
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                      
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                                      
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                       
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                                              
from  VW_PatientDetail a                                            
where a.LocationId = @LocationId and a.ModuleId=1                                                 
and a.Sex=17 and datediff(dd,a.dob,getdate())/365.25 >= 10                                            
and dbo.fn_GetPatientPMTCTProgramStatus_Futures(a.ptn_pk)= ''PMTCT''                                            
and a.Ptn_Pk in                                                             
 (select q.ptn_pk from mst_patient p                                                             
  inner join ord_visit q on p.ptn_pk = q.ptn_pk                                                                 
     Left Outer Join ord_patientpharmacyorder s on q.visit_id = s.visitid                                                             
     Left Outer Join dtl_patientpharmacyorder t on s.ptn_pharmacy_pk = t.ptn_pharmacy_pk                                                             
     where q.visittype in (4,6) and s.progid = 223 and s.pharmacyperiodtaken = 142                                                             
   and s.Ptn_Pharmacy_Pk in(select top 1 Ptn_Pharmacy_pk from                                                             
           Ord_PatientPharmacyOrder where Ptn_Pk = p.Ptn_pk and (PharmacyPeriodTaken is not null or PharmacyPeriodTaken <> 0)                                                           
     and (DeleteFlag = 0 or DeleteFlag is null) order by Dispensedbydate desc)                                                            
           and t.prophylaxis = 1 and dateadd(dd,90,q.VisitDate) >=getdate() and p.Ptn_Pk = a.Ptn_Pk)                                        
                                        
--Table[7]--Current Exposed Infants--less than 24 months -----------------------------------------------------------------------------------------------                                                                 
select distinct                                                         
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                                   
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                                         
[Patient Location][Facility], Gender[Sex], a.DOB,          
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                       
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                          
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                          
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                                          
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                      
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                                                  
from  VW_PatientDetail a                                                
where a.LocationId = @LocationId and (a.ModuleId=1 or a.ModuleID in (203,12,2))                                                 
and a.[Patient Status]=''Active'' and datediff(dd,a.dob,getdate())< 730.50                                                                                                                                                         
                                                
--Table[8]--Current PMTCT Infants--less than 24 months -----------------------------------------------------------------------------------------------                                                                 
                                        
select Distinct                                                          
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                                                                               
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                                         
[Patient Location][Facility], Gender[Sex], a.DOB,                                                           
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                                                                                
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                          
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                          
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                                          
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                           
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                                                  
from  VW_PatientDetail a                                                
where a.LocationId = @LocationId and a.ModuleId=1                                    
and a.[Patient Status]=''Active'' and datediff(dd,a.dob,getdate())< 730.50                                                
                                                
--Table[9]--Current HIV Care Infants--less than 24 months -----------------------------------------------------------------------------------------------                                      
select Distinct                                                          
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                                                                               
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                                
[Patient Location][Facility], Gender[Sex], a.DOB,                                  
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                              
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                          
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                         
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number], 
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],  
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                                                  
from  VW_PatientDetail a                                                
where a.LocationId = @LocationId and a.ModuleID in (203,12,2)                                                 
and a.[Patient Status]=''Active'' and datediff(dd,a.dob,getdate())< 730.50                                        
------------------Added on 26-11-2010------------------------------------------------------------------------------                                        
                          
--Table 10                                        
select mm.ModuleId,mm.ModuleName,fm.FacilityID from mst_module mm                               
inner join dbo.lnk_FacilityModule fm                               
on mm.ModuleId=fm.ModuleId                              
Where mm.status=2 and (DeleteFlag=0 or DeleteFlag is null)                              
and fm.FacilityID=@LocationId and mm.ModuleId > 2                             
                
--Table 11                                
select Module.ModuleName[Module],dtl.IndicatorName[IndicatorName],dtl.Query[Query],Module.ModuleId                                      
from mst_Feature Feature inner join Mst_HomePage Home on Home.FeatureId = Feature.FeatureId                                
inner join Mst_Module Module on Module.ModuleId = Feature.ModuleId                                 
inner join dtl_HomePage dtl on Home.Id = dtl.HomePageId                                       
where Feature.FeatureName like ''Facility Home%'' and dtl.DeleteFlag=0 and Feature.Published=2                              
--Table[12]----For Preferred Location----                                    
select ISNULL(Preferred,0)[Preferred], FacilityName from mst_facility where FacilityID= @LocationId                                                          
                                                                              
--Table[13]----------------------------------------------------------------------------------------------------------                                                              


	--select * from lnk_facilitymodule where FacilityID= @LocationId    
select * from lnk_facilitymodule Where FacilityID= @LocationId  And ModuleID Not in (2,1)
Union
Select FacilityID,2,UserID,CreateDate,UpdateDate From dbo.lnk_FacilityModule Where FacilityID= @LocationId  And ModuleID=203      
Union
Select FacilityID,1,UserID,CreateDate,UpdateDate From dbo.lnk_FacilityModule Where FacilityID= @LocationId  And ModuleID=203;           
                             
--Table[14]----                            
                      
Declare Moduleidres Cursor                                  
for                          
 select mm.ModuleId from mst_module mm      
 inner join dbo.lnk_FacilityModule fm                             
 on mm.ModuleId=fm.ModuleId                            
 Where mm.status=2 and (DeleteFlag=0 or DeleteFlag is null)                            
 and mm.ModuleId>2                          
 open Moduleidres                          
 set @Tsql = ''''         
 set @TsqlTable=''''                                 
 fetch next from Moduleidres into @ModuleId                                  
 while @@fetch_status=0                                  
   begin                                  
    --set @Tsql = ''''                                  
     Declare IndicatorResults Cursor                                  
     for                                  
     select dtl.IndicatorName,dtl.Query                                  
     from mst_Feature Feature inner join Mst_HomePage Home on Home.FeatureID = Feature.FeatureID                                    
     inner join dtl_HomePage dtl on Home.Id = dtl.HomePageId                                   
     where Feature.ModuleId=@ModuleId and Feature.Featurename like ''Facility Home%'' and Home.DeleteFlag=0                                   
                                        
     open IndicatorResults                                  
     fetch next from IndicatorResults into @Indicator,@Query         
     while @@fetch_status=0      
       begin       
    Select @Indicator [IndicatorName]      
        exec(@Query)                                          
          fetch next from IndicatorResults into @Indicator,@Query         
       end        
     close IndicatorResults                                  
     deallocate IndicatorResults                            
     fetch next from Moduleidres into @ModuleId                                  
  end                         
                       
close Moduleidres                                  
deallocate Moduleidres                                 
------------Declare Moduleidres Cursor                                  
------------for                          
------------select mm.ModuleId from mst_module mm                             
------------inner join dbo.lnk_FacilityModule fm                             
------------on mm.ModuleId=fm.ModuleId                            
------------Where mm.status=2 and (DeleteFlag=0 or DeleteFlag is null)                            
------------and fm.FacilityID=@LocationId and mm.ModuleId > 2                          
------------open Moduleidres                          
------------set @Tsql = ''''                                  
------------fetch next from Moduleidres into @ModuleId                                  
------------while @@fetch_status=0                                  
------------  begin                                  
------------    --set @Tsql = ''''                                  
------------ Declare IndicatorResults Cursor                                  
------------ for                                  
------------  select dtl.IndicatorName,dtl.Query                                  
------------  from mst_Feature Feature inner join Mst_HomePage Home on Home.FeatureID = Feature.FeatureID                                    
------------  inner join dtl_HomePage dtl on Home.Id = dtl.HomePageId                                   
------------  where Feature.ModuleId=@ModuleId and Feature.Featurename like ''Facility Home%'' and Home.DeleteFlag=0                        
------------                                   
------------                                      
------------  open IndicatorResults                                  
------------   fetch next from IndicatorResults into @Indicator,@Query                                  
------------   while @@fetch_status=0                                  
------------     begin                                  
------------    if @Tsql!=''''                                  
------------       begin                                  
------------     set @Tsql = @Tsql +'',''                            
------------       end                         
------------if @Query !=''''                          
------------     begin                          
------------      set @Tsql = @Tsql + ''(''+@Query+'') as [''+@Indicator+''_''+@ModuleId+'']''                                          
------------     end                      else                          
------------     begin                          
------------        set @Tsql = @Tsql +'''''''''' as [''+@Indicator+''_''+@ModuleId+'']''                                           
------------     end                            
------------   --set @Tsql = @Tsql + ''(''+@Query+'') as [''+@Indicator+''_''+@ModuleId+'']''                                  
------------    fetch next from IndicatorResults into @Indicator,@Query                                  
------------     end                                  
------------   close IndicatorResults                                  
------------   deallocate IndicatorResults                            
------------                                 
------------    fetch next from Moduleidres into @ModuleId                                  
------------  end                        
------------if @Tsql!=''''                        
------------Begin                           
------------set @Tsql = ''Select ''+ @Tsql                           
------------                         
------------End                          
------------exec(@Tsql)                             
------------close Moduleidres                                  
------------deallocate Moduleidres                                        
                                       
-------------------------------------------------------------------------------------------------------------------    
print ''firstloop''                                      
end                                                                                                       
                                                                                     
else                                                                                                       
                                                                                                                                           
  begin                                                                   
                                           
--Table[0]----Active Patients----------------------------------------------------------------------------------------                                                          
select Distinct                                                          
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                                                                               
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                             
[Patient Location][Facility], Gender[Sex], a.DOB,                                                           
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                                                                                
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                          
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                          
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                                          
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                           
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                                                  
from  VW_PatientDetail a                  
where a.ModuleID in (203,12,2)                                                  
and [Patient Status]=''Active''                                                
                                                
--Table[1]----Active Non-ART Patients----------------------------------------------------------------------------------------                                                          
select Distinct                                                          
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+             
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                                         
[Patient Location][Facility], Gender[Sex], a.DOB,                                                           
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                            
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                          
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                          
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                                          
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                           
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                                                  
from  VW_PatientDetail a                                                
where a.ModuleID in (203,12,2)                                                  
and [Patient Status]=''Active''                                                
and (dbo.fn_GetPatientProgramStatus_Constella(a.ptn_pk) = ''Non-ART''  or dbo.fn_GetPatientProgramStatus_Constella(a.ptn_pk) = ''Stopped ART'')                                                      
                                              
--Table[2]----Active ART Patients----------------------------------------------------------------------------------------                                                          
select Distinct                                                          
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                     
[Patient Location][Facility], Gender[Sex], a.DOB,                            
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                                                                                
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                          
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                          
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                                          
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                           
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                           
from  VW_PatientDetail a                                                
where a.ModuleID in (203,12,2)                                                 
and [Patient Status]=''Active''                                                
and dbo.fn_GetPatientProgramStatus_Constella(a.ptn_pk) = ''ART''                                         
                                        
--Table[3]--Current Mothers in PMTCT -------------------------------------------------------------------------------------------------                                                                          
select Distinct                                                      
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                                        
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                                 
[Patient Location][Facility], Gender[Sex], a.DOB,                                                       
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                                                                     
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                      
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                      
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                            
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                       
isnull(a.OutPatientNumber,'''')[OutPatient Number]            
from  VW_PatientDetail a                                            
where  a.ModuleId=1                                                                          
and a.Sex=17 and datediff(dd,a.dob,getdate())/365.25 >= 10                                            
and dbo.fn_GetPatientPMTCTProgramStatus_Futures(a.ptn_pk)= ''PMTCT''                
                                                                     
                                                                      
--Table[4]--Current Number Women on ARV Prophylaxis--ANC-----------------------------------------------------------------------------------------------                                                                          
select Distinct                                                      
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                 
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                  
[Patient Location][Facility], Gender[Sex], a.DOB,                                                       
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                                                                            
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                      
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                   
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                                      
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                       
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                                       
from  VW_PatientDetail a                                            
where  a.ModuleId=1                                                                          
and a.Sex=17 and datediff(dd,a.dob,getdate())/365.25 >= 10                                            
and dbo.fn_GetPatientPMTCTProgramStatus_Futures(a.ptn_pk)= ''PMTCT''                                            
and a.Ptn_Pk in                                                             
 (select q.ptn_pk from mst_patient p                                                             
  inner join ord_visit q on p.ptn_pk = q.ptn_pk   
     Left Outer Join ord_patientpharmacyorder s on q.visit_id = s.visitid                                                             
     Left Outer Join dtl_patientpharmacyorder t on s.ptn_pharmacy_pk = t.ptn_pharmacy_pk                                                             
     where q.visittype in (4,6) and s.progid = 223 and s.pharmacyperiodtaken = 140                                                             
     and s.Ptn_Pharmacy_Pk in(select top 1 Ptn_Pharmacy_pk from                                                             
           Ord_PatientPharmacyOrder where Ptn_Pk = p.Ptn_pk and (PharmacyPeriodTaken is not null or PharmacyPeriodTaken <> 0)                    
           and (DeleteFlag = 0 or DeleteFlag is null) order by Dispensedbydate desc)                                                            
     and t.prophylaxis = 1 and dateadd(dd,90,q.VisitDate) >=getdate() and p.Ptn_Pk = a.Ptn_Pk)                                                                
                                                                     
--Table[5]--Current Number Women on ARV Prophylaxis--L&D-----------------------------------------------------------------------------------------------                                                                          
select Distinct                               
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                                                                  
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                                     
[Patient Location][Facility], Gender[Sex], a.DOB,                                                       
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                                                                            
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                      
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                      
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                                      
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                       
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                                              
from  VW_PatientDetail a                                            
where  a.ModuleId=1                                                                
and a.Sex=17 and datediff(dd,a.dob,getdate())/365.25 >= 10                                            
and dbo.fn_GetPatientPMTCTProgramStatus_Futures(a.ptn_pk)= ''PMTCT''                                                            
and a.Ptn_Pk in                                                             
 (select q.ptn_pk from mst_patient p                                                             
  inner join ord_visit q on p.ptn_pk = q.ptn_pk                                                   
     Left Outer Join ord_patientpharmacyorder s on q.visit_id = s.visitid                                                             
     Left Outer Join dtl_patientpharmacyorder t on s.ptn_pharmacy_pk = t.ptn_pharmacy_pk                                                             
     where q.visittype in (4,6) and s.progid = 223 and s.pharmacyperiodtaken = 141                                                             
     and s.Ptn_Pharmacy_Pk in(select top 1 Ptn_Pharmacy_pk from                                                             
            Ord_PatientPharmacyOrder where Ptn_Pk = p.Ptn_pk and (PharmacyPeriodTaken is not null or PharmacyPeriodTaken <> 0)                                   
   and (DeleteFlag = 0 or DeleteFlag is null) order by Dispensedbydate desc)                                                            
            and t.prophylaxis = 1 and dateadd(dd,90,q.VisitDate) >=getdate() and p.Ptn_Pk = a.Ptn_Pk)                                                             
                                                                      
--Table[6]--Current Number Women on ARV Prophylaxis--PN-----------------------------------------------------                                                                          
select Distinct                                                       
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                                           
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                                     
[Patient Location][Facility], Gender[Sex], a.DOB,                                                       
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                                                                            
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                      
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                      
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                           
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                       
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                                              
from  VW_PatientDetail a                                            
where  a.ModuleId=1                                                                          
and a.Sex=17 and datediff(dd,a.dob,getdate())/365.25 >= 10                                            
and dbo.fn_GetPatientPMTCTProgramStatus_Futures(a.ptn_pk)= ''PMTCT''                                            
and a.Ptn_Pk in                                              
 (select q.ptn_pk from mst_patient p                                                             
  inner join ord_visit q on p.ptn_pk = q.ptn_pk                                                                 
     Left Outer Join ord_patientpharmacyorder s on q.visit_id = s.visitid                                                             
     Left Outer Join dtl_patientpharmacyorder t on s.ptn_pharmacy_pk = t.ptn_pharmacy_pk                                                             
     where q.visittype in (4,6) and s.progid = 223 and s.pharmacyperiodtaken = 142                                                             
   and s.Ptn_Pharmacy_Pk in(select top 1 Ptn_Pharmacy_pk from                                                             
           Ord_PatientPharmacyOrder where Ptn_Pk = p.Ptn_pk and (PharmacyPeriodTaken is not null or PharmacyPeriodTaken <> 0)                                                           
     and (DeleteFlag = 0 or DeleteFlag is null) order by Dispensedbydate desc)                                                            
           and t.prophylaxis = 1 and dateadd(dd,90,q.VisitDate) >=getdate() and p.Ptn_Pk = a.Ptn_Pk)                                                              
                                      
                                                            
                                        
--Table[7]--Current Exposed Infants--less than 24 months -----------------------------------------------------------------------------------------------                                                                 
select distinct                                                         
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                                                                               
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                                         
[Patient Location][Facility], Gender[Sex], a.DOB,                                                           
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                                                                                
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                          
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                          
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                                          
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                      
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                                                  
from  VW_PatientDetail a                                                
where (a.ModuleId=1 or a.ModuleID in (203,12,2))                                                 
and a.[Patient Status]=''Active'' and datediff(dd,a.dob,getdate())< 730.50                                             
                                        
--Table[8]--Current PMTCT Infants--less than 24 months -----------------------------------------------------------------------------------------------                                                                 
select Distinct                                                          
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                                                                               
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                                         
[Patient Location][Facility], Gender[Sex], a.DOB,                                                           
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                                                                                
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                            
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                          
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                                          
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                           
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                                                  
from  VW_PatientDetail a                                                
where  a.ModuleId=1                                                  
and a.[Patient Status]=''Active'' and datediff(dd,a.dob,getdate())< 730.50                                                
                                                
--Table[9]--Current HIV Care Infants--less than 24 months -----------------------------------------------------------------------------------------------                                   
select Distinct                                                          
convert(varchar(50), decryptbykey(a.FirstName))+ '' ''+ isnull(convert(varchar(50), decryptbykey(a.MiddleName)),'''') +'' ''+                                                                                               
convert(varchar(50), decryptbykey(a.LastName))[Patient Name],                                                         
[Patient Location][Facility], Gender[Sex], a.DOB,                                                           
isnull(convert(varchar(50), decryptbykey(a.Address)),'''')[Address],                                                                                                                            
isnull(convert(varchar(50), decryptbykey(a.Phone)),'''')[Phone],                                                          
a.CountryId+''-''+a.PosId+''-''+a.SatelliteId+''-''+a.PatientEnrollmentID[Patient ID],                                                       
isnull(a.PatientClinicID,'''') [Patient Clinic Id], isnull(a.ANCNumber,'''')[ANC Number],                                                          
isnull(a.PMTCTNumber,'''')[PMTCT Number], isnull(a.AdmissionNumber,'''')[Admission Number],                                                           
isnull(a.OutPatientNumber,'''')[OutPatient Number]                                                                  
from  VW_PatientDetail a           
where  a.ModuleID in (203,12,2)                                                  
and a.[Patient Status]=''Active'' and datediff(dd,a.dob,getdate())< 730.50                           
                          
------------------Added on 26-11-2010------------------------------------------------------------------------------                                        
                          
--Table 10                                        
select mm.ModuleId,mm.ModuleName,fm.FacilityID from mst_module mm                               
inner join dbo.lnk_FacilityModule fm                               
on mm.ModuleId=fm.ModuleId                              
Where mm.status=2 and (DeleteFlag=0 or DeleteFlag is null)                              
and mm.ModuleId > 2                             
                            
--Table 11                                
select Module.ModuleName[Module],dtl.IndicatorName[IndicatorName],dtl.Query[Query],Module.ModuleId                                      
from mst_Feature Feature inner join Mst_HomePage Home on Home.FeatureId = Feature.FeatureId                                
inner join Mst_Module Module on Module.ModuleId = Feature.ModuleId                                 
inner join dtl_HomePage dtl on Home.Id = dtl.HomePageId                                       
where Feature.FeatureName like ''Facility Home%'' and dtl.DeleteFlag=0 and Feature.Published=2                                
--Table[12]----For Preferred Location----                                    
select ISNULL(Preferred,0)[Preferred], FacilityName from mst_facility where FacilityID= @LocationId                                     
                                                                              
--Table[13]----------------------------------------------------------------------------------------------------------                                                              
 -- select * from lnk_facilitymodule                                
 --  DECLARE @FacilityID1 varchar(max)
	--SELECT @FacilityID1 =  COALESCE(@FacilityID1 + '', '', '''')+                   
	--	 CAST(FacilityId AS varchar(50)) FROM mst_Facility 
	
	--declare @sql varchar(max)
	--set @sql=''select * from lnk_facilitymodule where FacilityID in (''+@FacilityID1+'')''
	--exec (@sql)         
select * from lnk_facilitymodule Where  ModuleID Not in (2,1)
Union
Select FacilityID,2,UserID,CreateDate,UpdateDate From dbo.lnk_FacilityModule Where  ModuleID=203      
Union
Select FacilityID,1,UserID,CreateDate,UpdateDate From dbo.lnk_FacilityModule Where  ModuleID=203;
                 
--Table[14]----                            
                          
Declare Moduleidres Cursor                                  
for                          
 select mm.ModuleId from mst_module mm                             
 inner join dbo.lnk_FacilityModule fm                             
 on mm.ModuleId=fm.ModuleId                            
 Where mm.status=2 and (DeleteFlag=0 or DeleteFlag is null)                            
 and mm.ModuleId>2                          
 open Moduleidres                          
 set @Tsql = ''''         
 set @TsqlTable=''''                                 
 fetch next from Moduleidres into @ModuleId                                  
 while @@fetch_status=0                                  
   begin                                  
    --set @Tsql = ''''                                  
     Declare IndicatorResults Cursor                                  
     for                                  
     select dtl.IndicatorName,dtl.Query                                  
     from mst_Feature Feature inner join Mst_HomePage Home on Home.FeatureID = Feature.FeatureID                                    
     inner join dtl_HomePage dtl on Home.Id = dtl.HomePageId                                   
     where Feature.ModuleId=@ModuleId and Feature.Featurename like ''Facility Home%'' and Home.DeleteFlag=0                                   
                                        
     open IndicatorResults                                  
     fetch next from IndicatorResults into @Indicator,@Query         
     while @@fetch_status=0      
       begin       
    Select @Indicator       
--          print @Indicator        
        exec(@Query)                                          
          fetch next from IndicatorResults into @Indicator,@Query         
       end        
     close IndicatorResults                                  
     deallocate IndicatorResults                     
     fetch next from Moduleidres into @ModuleId                                  
  end                         
        
        
        
                                 
--  while @@fetch_status=0                                  
--   begin                                  
--  if @Indicator!=''''                                  
--    begin                                  
--     set @Tsql = ''Select ''+ @Indicator         
--     print(@Tsql)                                 
--    end                         
--    if @Query !=''''                          
--    begin                          
--   set @TsqlTable =  @Query         
--   print(@TsqlTable)                                          
--    end                          
--   else                          
--  if @Indicator!=''''                                  
--    begin                                  
--     set @Tsql = ''Select ''+ @Indicator         
--     print(@Tsql)                                 
--    end                         
--    if @Query !=''''                          
--    begin                          
--   set @TsqlTable =  @Query         
--   print(@TsqlTable)        
--  end                                
   ----fetch next from IndicatorResults into @Indicator,@Query                                 
-- end                                  
--   close IndicatorResults                                  
--   deallocate IndicatorResults                            
                                 
--if @Tsql!=''''                        
-- Begin                          
--  set @Tsql = ''Select ''+ @Tsql                           
-- End                          
--exec(@Tsql)                           
close Moduleidres                                  
deallocate Moduleidres                                        
                                       
-------------------------------------------------------------------------------------------------------------------                                        
end                                                           
                                                                                                                                                                        
                                              
--Close symmetric key Key_CTC                                               
                                                                
--exec pr_Security_facilitydetailsPMTCT_constella @LocationId,@DBKey  with RECOMPILE;                                           
--exec pr_Security_facilitydetailsExposedInfants_constella @LocationId,@DBKey with RECOMPILE;                                              
                                        
Set NoCount off                                                                                                                                                             
                                                  
end 
' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Clinical_PatientDetails_Constella]    Script Date: 12/15/2014 19:09:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_PatientDetails_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_PatientDetails_Constella]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_Clinical_PatientDetails_Constella]   ( 
	@PatientId int,                                                                                                                                                                                
	@SystemId int,                                  
	@ModuleId int,                                                                                                                                                                                
	@DBKey varchar(50) = null                                                                                                                                                                                                                                            
  
  )  
     
As                                                                                                                                                                                                                                                    
                                                                                                                                              
begin
                                                                                                                                                                                                                                 
                                                                                                                                                                          
--Declare @SymKey varchar(400)
--Set @SymKey = 'Open symmetric key Key_CTC decryption by password=' + @DBKey + ''
--Exec (@SymKey)
Declare @ARTEndStatus varchar(50)
-- Table 0                                                                                                                                                                              
Select	convert(varchar(50), decryptbykey(a.firstname)) [firstname],
		convert(varchar(50), decryptbykey(a.middlename)) [middlename],
		convert(varchar(50), decryptbykey(a.lastName)) [lastname],
		convert(varchar(50), decryptbykey(a.Address)) [Address],
		convert(varchar(50), decryptbykey(a.Phone)) [phone],
		a.CountryId + '-' + a.PosId + '-' + a.SatelliteId + '-' + a.PatientEnrollmentId PatientId,
		PatientEnrollmentID,
		a.PatientClinicId,
		a.RegistrationDate,
		a.Status,
		a.IQNumber [IQNumber],
		DOB,
		(datediff(Month, DOB, getdate()) / 12) [Age],
		convert(varchar, datediff(Month, DOB, getdate()) % 12) [Age1],
		--datediff(yy,a.dob,getdate()) [AGE],                                                                        
		datediff(Month, a.dob, getdate()) [AgeInMonths],
		dbo.fn_getpatientage(a.ptn_pk) [AGEINYEARMONTH],
		b.Name [SexNM],
		e.Name [Program],
		isnull(f.Name, '') [VillageNM],
		isnull(g.Name, '') [District],
		isnull(h.Name, '') [ProvinceNM],
		c.EmergContactName [EmergContactName],
		c.EmergContactPhone [EmergContactPhone],
		d.HIVStatus_Child,
		convert(varchar(50), decryptbykey(c.TenCellLeader)) [TenCellLeader],
		convert(varchar(50), decryptbykey(c.TenCellLeaderAddress)) [TenCellLeaderAddress],
		a.CountryId + '-' + a.PosId + '-' + a.SatelliteId + '-' + a.PatientEnrollmentId [EnrollmentID],
		isnull(a.ANCNumber, '') [ANCNumber],
		isnull(a.PMTCTNumber, '') [PMTCTNumber],
		isnull(a.AdmissionNumber, '') [AdmissionNumber],
		isnull(a.OutpatientNumber, '') [OutpatientNumber],
		isnull(a.HEIIDNumber,'') HEIIDNumber,
		a.PatientFacilityID,
		isnull(f.ID, '') [VillageId],
		isnull(g.ID, '') [DistrictId],
		IP.ParentPtnPk
From mst_patient a
Left Outer Join mst_decode b On a.sex = b.id
Left Outer Join dtl_patientcontacts c On a.ptn_pk = c.ptn_pk
Left Outer Join dtl_PatientHivOther d On a.Ptn_Pk = d.Ptn_pk
Left Outer Join mst_decode e On a.ProgramId = e.Id
Left Outer Join mst_village f On a.VillageName = f.Id
Left Outer Join mst_district g On a.DistrictName = g.Id
Left Outer Join mst_province h On a.Province = h.Id
Left Outer Join dtl_InfantParent IP On IP.Ptn_pk = a.Ptn_Pk And IP.DeleteFlag = 0 OR IP.DeleteFlag Is Null
Where a.Ptn_Pk = @PatientId


--Table 1 --ART --Last Visit Date                                                   
Select Top 1 a.VisitDate
From ord_Visit a, mst_patient b
Where a.ptn_pk = b.ptn_pk
And a.Ptn_Pk = @PatientId
And (a.DeleteFlag = 0 Or a.DeleteFlag Is Null)
And (a.visittype Not In (5, 10, 11, 12) Or a.visittype < 100)
And (b.DeleteFlag = 0 Or b.DeleteFlag Is Null)
And nullif(b.PatientEnrollmentId, '') Is Not Null
Order By a.Visitdate Desc

--Table 2                                                                             
Select Top 1 AppDate
From dtl_patientappointment
Where ptn_pk = @PatientId
And Appstatus In (12)
And AppDate <> '1900-01-01'
And (DeleteFlag = 0 Or DeleteFlag Is Null)
Order By AppDate Asc

--Table 3                                                                                                                                                                        
-----HB,HCT/HB,AST                                                                                                                                               


Select	R.ResultValue	TestResults
		,O.OrderDate	OrderedByDate
		,R.ParameterId
From dtl_LabOrderTestResult R
Inner Join ord_LabOrder O On O.Id = R.LabOrderId
Inner Join Mst_LabTestParameter P On R.ParameterId = P.Id
And r.DeleteFlag = 0
And O.DeleteFlag = 0
And O.Ptn_Pk = @PatientId
And P.ReferenceId In ('HCT', 'HB', 'ASTSGOT', 'CREATININE', 'SYPHILIS_RPR', 'CREATININEMM')

--Table 4                             
Select	dbo.fn_GetPatientCurrentARTRegimen_Constella(a.ptn_pk) [Current ARV Regimen],
		dbo.fn_GetPatientCurrentARTStartDate_Constella(a.ptn_pk) [Current ARV StartDate],
		a.ARTStartDate [AidsRelief ARV StartDate],
		b.currentartstartdate [Hist ARV StartDate],
		c.ARTStartDate [Hist ARV StartDateCTC]
From mst_patient a
Left Outer Join dtl_patienthivprevcareie b On a.ptn_pk = b.ptn_pk
Left Outer Join dtl_PatientHivPrevCareEnrollment c On a.ptn_pk = c.ptn_pk
Where (a.deleteflag = 0 Or a.deleteflag Is Null)
And a.ptn_pk = @PatientId

--Table 5                                                                                                                                                  
---Query to use for Weight and BMI Graph                                                       
Select *
From
(
	Select
		a.Height [Height],
		a.Weight [Weight],
		0 VisitID,--b.visit_Id [VisitID],
		convert(decimal(18, 2), round((nullif(a.Weight, 0) / (nullif(a.height / 100, 0) * nullif(a.height / 100, 0))), 2)) As BMI,
	null	[VisitType],--b.visitType [VisitType],
		b.VisitDate [Visit_OrderbyDate]
	From	dtl_patientvitals a,
			ord_visit b
	Where a.visit_pk = b.visit_Id
		And (b.DeleteFlag = 0 Or b.DeleteFlag Is Null)
		And a.ptn_pk = @PatientId
		And a.Height Is Not Null
		And a.Weight Is Not Null 
	Union 
	Select
		a.Height [Height],
		a.Weight [Weight],
		0 VisitID, --b.visit_Id [VisitID],
		convert(decimal(18, 2), round((nullif(a.Weight, 0) / (nullif(a.height / 100, 0) * nullif(a.height / 100, 0))), 2)) As BMI,
		null	[VisitType],--b.visitType [VisitType],
		a.OrderedByDate [Visit_OrderbyDate]
	From	ord_PatientPharmacyOrder a,
			ord_visit b
	Where a.visitId = b.visit_Id
		And (b.DeleteFlag = 0 Or b.DeleteFlag Is Null)
		And a.ptn_pk = @PatientId
		And a.ordertype = 117
		And a.Height Is Not Null
		And a.Weight Is Not Null
) As inLineView
Order By Visit_OrderbyDate Desc

--Table 6                                                                                     

			declare @CD4Values Table( 
				Ptn_Pk int, 
				LabTestId int, 
				TestName varchar(100),
				TestReferenceId varchar(50),
				ParameterId int,
				ParameterReferenceId varchar(50),
				ParameterName varchar(100),
				TestResult decimal(10,2),
				ResultUnit varchar(50),
				DetectionLimit decimal(10,2), 
				Undetectable bit, 
				OrderDate datetime,
				ResultDate datetime,
				HasResult bit
				)
				Insert Into @CD4Values
				(
					Ptn_Pk,
					LabTestId,
					TestName,
					TestReferenceId,
					ParameterId,
					ParameterReferenceId,
					ParameterName,
					TestResult,
					ResultUnit,
					DetectionLimit,
					Undetectable,
					OrderDate,
					ResultDate,
					HasResult
				)
				Select	O.Ptn_Pk
						,R.LabTestId
						,T.Name TestName
						,T.ReferenceId TestReferenceId
						,R.ParameterId
						,P.ReferenceId ParameterReferenceId
						,P.ParameterName
						,R.ResultValue	As TestResult
						,R.ResultUnit
						,R.DetectionLimit
						,R.Undetectable
						,O.OrderDate
						,ResultDate
						,R.HasResult
				From dtl_LabOrderTestResult As R
				Inner Join dtl_LabOrderTest OT On R.LabOrderTestId=OT.Id
				Inner Join Mst_LabTestParameter As P On P.Id = R.ParameterId
				Inner Join mst_LabTestMaster As T On T.Id = R.LabTestId
				Inner Join ord_LabOrder As O On O.Id = R.LabOrderId And O.Id = OT.LabOrderId
				Where (P.ReferenceId = 'CD4')
					And (R.DeleteFlag = 0)
					And (O.DeleteFlag = 0)
					And (O.Ptn_Pk = @PatientId)
				Order By OrderDate

				Select Ptn_Pk,LabTestId,TestResult, OrderDate [Date], ResultDate,ResultUnit From @CD4Values

--Table 7                                                                                                           

Select	O.Ptn_Pk
		,R.LabTestId
		,R.ParameterId
		,R.ResultValue TestResult
		,R.DetectionLimit
		,R.Undetectable
		,O.OrderDate	[Date]
		,ResultDate
From dtl_LabOrderTestResult R
Inner Join Mst_LabTestParameter P On P.Id = R.ParameterId
Inner Join dtl_LabOrderTest OT On R.LabOrderTestId=OT.Id
Inner Join ord_LabOrder O On O.Id = R.LabOrderId
Where P.ReferenceId = 'VIRAL_LOAD'
	And R.DeleteFlag = 0
	And O.Ptn_Pk =@PatientId
Order By O.OrderDate Asc

--Table 8  

	Select		O.OrderDate	[Date]
	From dtl_LabOrderTestResult R
	Inner Join Mst_LabTestParameter P On P.Id = R.ParameterId
	Inner Join ord_LabOrder O On O.Id = R.LabOrderId
	Where P.ReferenceId In ('CD4','VIRAL_LOAD')
		And R.DeleteFlag = 0
		And O.DeleteFlag = 0
		And O.Ptn_Pk = @PatientId
	Order By [Date] Asc
--Table 9                                                                            
--Pregnant Record   
If (@ModuleId = '203') Begin
Select Top 1	a.*,
				b.Name [PregnantValue]
From dtl_PatientClinicalStatus a
Inner Join VW_AllMasters b On a.Pregnant = b.Id
Join Ord_visit c On a.Visit_pk = c.Visit_Id
Where a.ptn_pk = @PatientId
And b.ModuleId = @ModuleId
And (c.deleteflag = 0 Or c.deleteflag Is Null)
Order By a.Visit_pk Desc
End 
Else Begin
Select Top 1	*,
				[PregnantValue] =
					Case
						When Pregnant = '0' Then 'Negative'
						When Pregnant = '1' Then 'Positive'
					End
From dtl_PatientClinicalStatus a
Join Ord_visit b On a.Visit_pk = b.Visit_Id
Where a.ptn_pk = @PatientId
And (b.deleteflag = 0 Or b.deleteflag Is Null)
Order By a.Visit_pk Desc
End

--Table 10 WHOStage Data                                                                    

Select WHOStageFlag = 1
--Table 11                                                                                        
Select Top 1	a.ptn_pk,
				a.visit_pk,
				c.Name [WHOStage],
				d.Name [WAB Stage]
From dtl_patientstage a
Inner Join ord_visit b On a.visit_pk = b.Visit_ID
Left Outer Join mst_decode c On a.whostage = c.id
Left Outer Join mst_decode d On a.wabstage = d.id
Where a.ptn_pk = @PatientId
And (WHOStage Is Not Null And WHOStage <> 0)
And (b.DeleteFlag Is Null Or b.DeleteFlag = 0)
Order By a.WABStageID Desc

--End Else Begin
--Select WHOStageFlag = 0
--Select 19
--End

--Table 12--- Lowest CD4                                                                                                                                                                     

Select LowestCD4Flag = 1

--Table 13                                                                                                                                                              
Select *
From
(
	Select
		convert(numeric, PrevLowestCD4) [TestResults],
		PrevMostRecentCD4 [TestResultsCTC],
		isnull(PrevLowestCD4Date, PrevMostRecentCD4Date) [OrderedByDate]
	From	dtl_PatientHivPrevCareIE a,
			ord_Visit b
	Where a.Visit_pk = b.Visit_Id
		And a.Ptn_pk = @PatientId
		And (b.deleteflag Is Null Or b.deleteflag = 0)
		And (PrevLowestCD4 Is Not Null Or PrevMostRecentCD4 Is Not Null) 
	Union 
	Select TestResult TestResults
		   ,TestResult TestResultCTC
		   ,OrderDate OrderByDate
	From @CD4Values Where HasResult = 1
	
) As InlineView

--Table 14 Most Recent CD4                                                            

Select RecentCD4Flag = 1

-- Table 15                                                                             
Select	max(TestResults) [TestResults],
		OrderedByDate
From
(
	Select
		convert(numeric, PrevMostRecentCD4) [TestResults],
		PrevMostRecentCD4Date [OrderedByDate]
	From	dtl_PatientHivPrevCareIE a,
			ord_Visit b
	Where a.Visit_pk = b.Visit_Id
		And a.Ptn_pk = @PatientId
		And (b.deleteflag Is Null Or b.deleteflag = 0)
		And PrevMostRecentCD4 Is Not Null
		And PrevMostRecentCD4Date Is Not Null 
	Union 
	Select TestResult TestResults		  
		   ,OrderDate OrderByDate
	From @CD4Values Where HasResult = 1
) As InlineView
Group By OrderedByDate
Order By OrderedByDate Desc

--Table 16                                                                                                                                                                                                                             

Declare @checkdate datetime
Declare @finaldate datetime
Select RecentCD4Flag = 1

-- Table 17                                                                                                                       
Set @checkdate =
(
	Select Top 1
		OrderedByDate
	From
		(
			Select
				convert(numeric, PrevMostRecentCD4) [TestResults],
				PrevMostRecentCD4Date [OrderedByDate]
			From	dtl_PatientHivPrevCareIE a,
					ord_Visit b
			Where a.Visit_pk = b.Visit_Id
				And a.Ptn_pk = @PatientId
				And (b.deleteflag Is Null Or b.deleteflag = 0)
				And PrevMostRecentCD4 Is Not Null
				And PrevMostRecentCD4Date Is Not Null 
			Union

			Select TestResult TestResults
					,OrderDate OrderByDate
			From @CD4Values Where HasResult = 1
		) As InlineView
	Order By OrderedByDate Desc
)
Set @finaldate = dateadd(Month, 6, @checkdate)
Select @finaldate


--Table 18 WAB stage                                                                                                                          
Select Top 1	a.ptn_pk,
				a.Visit_Pk,
				a.WABStageID,
				d.name [WABStage]
From dtl_patientstage a, ord_visit b, mst_decode d
Where a.wabstage = d.id
And a.visit_pk = b.visit_Id
And a.ptn_pk = @PatientId
And (WABStage Is Not Null And WABStage <> 0)
And (b.DeleteFlag Is Null Or b.DeleteFlag = 0)
Order By a.WABStageID Desc

---Table 19 Program Status for ART(ART, Non-ART, Unknown, CareEnd) and PMTCT (PMTCT, PMTCT Care End)                                    
Select	nullif(dbo.fn_GetPatientProgramStatus_Naveen(@PatientId, @ModuleId), '') [ART/PalliativeCare],
		---- --dbo.fn_GetPatientPMTCTProgramStatus_Futures(@PatientId, 0) [PMTCTStatus]                                                                               
		nullif(dbo.fn_GetPatientPMTCTProgramStatus_Futures(@PatientId), '') [PMTCTStatus]


-- Table 20 for family info                                      
Select count(*) [FamilyCount]
From dtl_familyInfo
Where Ptn_pk = @PatientId
And Referenceid Is Not Null
And (DeleteFlag Is Null Or DeleteFlag = 0)

-- Table 21---for family ART Count                                                                            
Select count(*) [FamilyARTCount]
From dtl_FamilyInfo f
Left Outer Join mst_RelationshipType r On r.ID = f.RelationshipType
Left Outer Join mst_decode s On s.ID = f.Sex
Where f.Ptn_pk = @PatientId
And dbo.fn_GetHivCareStatusID(f.ptn_pk, f.ReferenceId, f.Id) = 2
And f.Referenceid Is Not Null
And (f.DeleteFlag Is Null Or f.DeleteFlag = 0)

--table 22                                                                                                                                    
Select count(*) [FamilyAllCount]
From dtl_familyInfo
Where Ptn_pk = @PatientId
And (DeleteFlag Is Null Or DeleteFlag = 0)

--Table 23 -- Dynamic Labels                                                                                                                                
Exec dbo.pr_SystemAdmin_GetSystemBasedLabels_Constella	@SystemId,
														1000,
														''
--Table 24                                                                                                                                                 
Select Top 1	Z.TestResults,
				Z.OrderedByDate,
				dateadd(mm, 6, Z.OrderedByDate) [OrderedByDueDate],
				Z.Dis_Date
From
(
	Select
		convert(numeric, b.PrevARVsCD4) [TestResults],
		a.RegistrationDate [OrderedByDate],
		'0' [Dis_Date]
	From mst_patient a
		Inner Join dtl_PatientHivPrevCareIE b On a.ptn_pk = b.ptn_pk
		Inner Join ord_Visit c On c.Visit_Id = b.Visit_pk
			And c.Ptn_Pk = a.Ptn_pk
			And c.visittype = 0
	Where (a.DeleteFlag = 0 Or a.DeleteFlag Is Null)
		And b.ptn_pk = @PatientId
		And a.RegistrationDate Is Not Null
		And b.PrevARVsCD4 Is Not Null 
	Union 
	Select TestResult TestResults	
		   ,OrderDate OrderByDate
		   ,'1' Dis_Date
	From @CD4Values Where HasResult = 1

) Z
Order By Z.OrderedByDate Desc

--Table 25-- Most Recent Weight                                                                          
Select Top 1 Weight, Height
From dtl_patientvitals
Where ptn_pk = @PatientId
And Weight Is Not Null
Order By visit_pk Desc

--Table 26 ARV runs out                                                                                   
Select Top 1	max(po.Duration),
				opo.dispensedbydate,
				datediff(dd, getdate(), (dateadd(dd, max(po.duration), opo.dispensedbydate))) [CurrARTStock]
From ord_PatientPharmacyOrder opo
Inner Join dtl_PatientPharmacyOrder po On opo.Ptn_Pharmacy_Pk = po.Ptn_Pharmacy_Pk
Where opo.ptn_pk = @PatientId
And --locationid = @LocationId and                                
opo.Ptn_Pharmacy_Pk In
(
	Select
		a.ptn_pharmacy_pk
	From	ord_patientpharmacyorder a,
			dtl_patientpharmacyorder b,
			lnk_drugtypegeneric c
	Where a.ptn_pharmacy_pk = b.ptn_pharmacy_pk
		And b.genericid = c.genericid
		And c.drugtypeid = 37
		And (a.deleteflag = 0 Or a.deleteflag Is Null)
		And a.dispensedbydate Is Not Null Union Select
		a.ptn_pharmacy_pk
	From	ord_patientpharmacyorder a,
			dtl_patientpharmacyorder b,
			lnk_drugtypegeneric c,
			lnk_druggeneric d
	Where a.ptn_pharmacy_pk = b.ptn_pharmacy_pk
		And b.drug_pk = d.drug_pk
		And d.genericid = c.genericid
		And c.drugtypeid = 37
		And a.dispensedbydate Is Not Null
		And (a.deleteflag = 0 Or a.deleteflag Is Null)
)
Group By opo.dispensedbydate
Order By dispensedbydate Desc

--Table 27                                                                                    
Select dbo.fn_GetAgeConstella(DOB, RegistrationDate) [PatientAge]
From mst_patient
Where ptn_pk = @PatientId
--Table 28 -- PMTCT -- Current ARV Prophylaxis Regimen and Current ARV Prophylaxis Regimen Start Date    
If Exists
(
	Select
		*
	From mst_patient
	Where datediff(dd, dob, getdate()) / 365 >= 15
		And ptn_pk = @PatientId
) Begin

Select	dbo.fn_GetPatientCurrentProphylaxisRegimen_Constella(@PatientId) [CurrentARVProphylaxisRegimen],
		dbo.fn_GetPatientCurrentProphylaxisStartDate_Constella(@PatientId) [CurrentProphylaxisRegimenStartDate]

End Else Select	[CurrentARVProphylaxisRegimen] = Null,
				[CurrentProphylaxisRegimenStartDate] = Null


--Table 29 -- PMTCT -- Delivery Date                                                                                                                                   
Select max(DateOfDelivery) [DeliveryDateTime]
From dtl_patientclinicalstatus a, mst_patient b
Where a.Ptn_pk = @PatientId
And a.ptn_pk = b.ptn_pk
And (b.deleteflag = 0 Or b.deleteflag Is Null)


--Table 30 -- PMTCT -- Feeding Option                                                                                                              
Select Top 1 (a.Name) [FeedingOption]
From dtl_InfantInfo b
Inner Join mst_pmtctdecode a On b.FeedingOption = a.Id
Inner Join ord_Visit c On b.visit_pk = c.visit_Id
Where b.Ptn_pk = @PatientId
And b.FeedingOption Is Not Null
And (a.deleteflag = 0 Or a.deleteflag Is Null)
And (b.deleteflag = 0 Or b.deleteflag Is Null)
Order By c.visitDate Desc

--Table 31 -- PMTCT -- Last Visit Date                                                                               
Select Top 1 a.VisitDate [PMTCTVisitDate]
From ord_Visit a, mst_patient b
Where a.ptn_pk = b.ptn_pk
And a.Ptn_Pk = @PatientId
And (a.DeleteFlag = 0 Or a.DeleteFlag Is Null)
And (a.visittype In (4, 6, 11, 12) Or a.visittype > 100)
And (b.DeleteFlag = 0 Or b.DeleteFlag Is Null)
And datediff(dd, b.dob, getdate()) / 365 >= 15
And b.sex = 17
And (ANCNumber Is Not Null Or PMTCTNumber Is Not Null Or AdmissionNumber Is Not Null Or OutPatientNumber Is Not Null)
Order By Visitdate Desc

--Table 32 -- Child HIV Status                                                                              
Select Top 1 (b.Name) [ChildHIVStatus]
From dtl_patienthivother a
Inner Join mst_pmtctDecode b On a.HIVStatus_CHILD = b.Id
Inner Join ord_Visit c On a.visit_pk = c.visit_Id
Inner Join mst_patient d On a.ptn_pk = d.ptn_pk
Where a.ptn_pk = @PatientId
And (d.ANCNumber Is Not Null Or d.PMTCTNumber Is Not Null Or d.AdmissionNumber Is Not Null Or d.OutPatientNumber Is Not Null)


And (d.DeleteFlag = 0 Or d.DeleteFlag Is Null)
Order By c.visitDate Desc

---Table 33 ---- LMP from ANC-----                                                                      

Select Top 1 a.LMP [LMP]
From dtl_PatientClinicalStatus a, ord_visit b, mst_visittype c, mst_patient d
Where a.visit_pk = b.visit_id
And b.visittype = c.visittypeid
And (b.DeleteFlag = 0 Or b.DeleteFlag Is Null)
And datediff(dd, d.dob, getdate()) / 365 >= 15
And a.ptn_pk = d.ptn_pk
And d.sex = 17
And c.visitname Like '%ANC%'
And a.Ptn_pk = @PatientId
And a.LMP Is Not Null
Order By b.visitdate Asc

--Table 34 ---- EDD from ANC-----                                                                      

Select Top 1 a.EDD [EDD]
From dtl_PatientClinicalStatus a, ord_visit b, mst_visittype c, mst_patient d
Where a.visit_pk = b.visit_id
And b.visittype = c.visittypeid
And (b.DeleteFlag = 0 Or b.DeleteFlag Is Null)
And datediff(dd, d.dob, getdate()) / 365 >= 15
And a.ptn_pk = d.ptn_pk
And d.sex = 17
And c.visitname Like '%ANC%'
And a.Ptn_pk = @PatientId
And a.EDD Is Not Null
Order By b.visitdate Asc

--Table 35 ---- TBStatus from ANC -----                                                  
Select Top 1 (d.Name) [TBStatus]
From dtl_PatientOtherTreatment a, ord_visit b, mst_visittype c, mst_pmtctdecode d, mst_patient e
Where a.visit_pk = b.visit_id
And b.visittype = c.visittypeid
And (b.DeleteFlag = 0 Or b.DeleteFlag Is Null)
And (a.DeleteFlag = 0 Or a.DeleteFlag Is Null)
And a.TBStatus = d.Id
And datediff(dd, e.dob, getdate()) / 365 >= 15
And b.ptn_pk = e.ptn_pk
And e.sex = 17
And c.visitname Like '%ANC%'
And a.Ptn_pk = @PatientId
And a.TBStatus Is Not Null
Order By b.visitdate Desc

--Table 36 ---- Partner HIV Status -----                                                                      
Select Top 1 (b.Name) [Partner HIV Status]
From dtl_PatientCounseling a, mst_pmtctdecode b, ord_Visit c, mst_patient d
Where a.visit_pk = c.visit_Id
And datediff(dd, d.dob, getdate()) / 365 >= 15
And a.ptn_pk = d.ptn_pk
And (a.DeleteFlag = 0 Or a.DeleteFlag Is Null)
And (c.DeleteFlag = 0 Or c.DeleteFlag Is Null)
And a.PartnerHIVTestResult = b.Id
And a.PartnerHIVTestResult Is Not Null
And d.sex = 17
And a.Ptn_pk = @PatientId
Order By c.visitdate Desc

--Table 37 ---- Infant Prophylaxis Regimen -----                                                            
Select Top 1 (z.RegimenType) [Prophylaxis Regimen]
From ord_patientpharmacyorder x, dtl_patientpharmacyorder y,
dtl_RegimenMap z, mst_patient c
Where (x.DeleteFlag Is Null Or x.DeleteFlag = 0)
And x.progid = 223
And y.prophylaxis = 1
And x.ptn_pharmacy_pk = y.ptn_pharmacy_pk
And x.ptn_pk = c.ptn_pk
And datediff(dd, c.dob, getdate()) / 365 <= 2
And x.ptn_pk = @PatientId
And y.ptn_pharmacy_pk = z.orderid
Order By x.dispensedbydate Desc
--Table 38 ---- Lab Results -----        
Select	TP.ParameterName															As Test
		,convert(varchar, max(O.OrderDate), 103)									As Date
		,max(cast(datediff(Month, P.DOB, O.OrderDate) / 12.0 As decimal(10, 1)))	As [Age(Mnt)]
		,R.ResultValue																As Result
From Mst_LabTestParameter As TP
Inner Join dtl_LabOrderTestResult As R On R.ParameterId = TP.Id
Inner Join ord_LabOrder As O On O.Id = R.LabOrderId
Inner Join mst_Patient As P On P.Ptn_Pk = O.Ptn_Pk
Where (R.DeleteFlag = 0)
	And (O.DeleteFlag = 0)
	And (R.HasResult = 1)
	And (TP.ReferenceId In ('HIV_RAPID_TEST', 'HIV_CONFIRM', 'PCR'))
	And (O.Ptn_Pk = @PatientId)
Group By	TP.ParameterName
			,R.ResultValue

--Table 39 ---- Lab Results -----                                                       

Select Top 1 a.GestAge [Gestational Age]
From dtl_patientdelivery a, mst_patient b
Where a.ptn_pk = @PatientId
And a.ptn_pk = b.ptn_pk
And datediff(dd, b.dob, getdate()) / 365 >= 15
Order By a.Visit_pk Desc

--Table 40 ---- Care Ended Status -----                                            
Select Top 1	PatientExitReason,
				CareEnded,
				PMTCTCareEnded,
				Ptn_Pk
From VW_PatientCareEnd
Where (CareEnded Is Not Null Or CareEnded <> 0)
And ptn_pk = @PatientId
And ModuleId = @ModuleId
Order By CareEndedId Desc

--Table 41 ---- Techenical AreaName according Patient Selection -----                                    
Select	ptn_pk,
		ModuleID,
		StartDate
From lnk_patientprogramstart
Where ptn_pk = @PatientId
Order By ModuleID

--Table 42 ---- Techenical AreaName according Patient Selection -----                          
--select status from mst_patient where ptn_pk = @PatientId and deleteflag=0              

Select	PatientExitReason,
		CareEnded,
		PMTCTCareEnded,
		Ptn_Pk
From VW_PatientCareEnd
Where ptn_pk = @PatientId
And CareEnded = 1
And PatientExitReason = 93

---Table 43               
Select @ARTEndStatus = dbo.fn_GetPatientARTStatus_Futures(@PatientId)

If (@ARTEndStatus != '' Or @ARTEndStatus Is Not Null) Begin
Select @ARTEndStatus [ARTEndStatus]
End Else Begin
Select '' [ARTEndStatus]
End

-- Table 44 --3 Viral Load due date
declare @ParameterId int;

Select Top (1) @ParameterId = Id from Mst_LabTestParameter As P Where (P.ReferenceId = 'VIRAL_LOAD');

declare @LatestViralLoad float ;
Set @LatestViralLoad = (
Select Top (1) dtl.ResultValue
From dtl_LabOrderTest As ord
Inner Join dtl_LabOrderTestResult As dtl On ord.LabOrderId = dtl.LabOrderId
Inner Join ord_LabOrder As L On L.Id = ord.LabOrderId
--Inner Join Mst_LabTestParameter As P On P.Id = dtl.ParameterId
Where (dtl.ParameterId = @ParameterId)
	And (L.Ptn_Pk = @PatientId)
	And (ord.DeleteFlag Is Null Or ord.DeleteFlag = 0)
	And (L.OrderDate <> '')
	And (L.OrderDate Is Not Null)
Order By L.OrderDate Desc
)

IF(@LatestViralLoad is null)
begin
       select ''[ViralLoadDueDate] from [dbo].[ord_LabOrder] where Ptn_pk=-1
end
else IF(@LatestViralLoad > 1000)
begin
       select TOP 1 DATEADD(MONTH, 3, ord.ResultDate) [ViralLoadDueDate],dtl.ResultValue from dtl_LabOrderTest ord inner JOIN dtl_LabOrderTestResult dtl on ord.LabOrderId = dtl.LabOrderId 
       inner JOIN [dbo].[ord_LabOrder] L ON L.Id=ord.LabOrderId
      -- inner JOIN ord_Visit ordV ON L.VisitId = ordV.Visit_Id
       where dtl.ParameterId = @ParameterId and L.Ptn_pk = @PatientId and (ord.DeleteFlag IS NULL OR ord.DeleteFlag=0) and ord.ResultDate <> '' 
       and ord.ResultDate is NOT NULL
       order BY ord.ResultDate desc
end
else
begin
       select TOP 1 DATEADD(MONTH, 12, ord.ResultDate) [ViralLoadDueDate],dtl.ResultValue from dtl_LabOrderTest ord inner JOIN dtl_LabOrderTestResult dtl on ord.LabOrderId = dtl.LabOrderId 
       inner JOIN [dbo].[ord_LabOrder] L ON L.Id=ord.LabOrderId
     --  inner JOIN ord_Visit ordV ON L.VisitId = ordV.Visit_Id
       where dtl.ParameterId =@ParameterId and L.Ptn_pk = @PatientId and (ord.DeleteFlag IS NULL OR ord.DeleteFlag=0) and ord.ResultDate <> '' 
       and ord.ResultDate is NOT NULL
       order BY ord.ResultDate desc
end
--Close Symmetric Key Key_CTC
End

GO


/****** Object:  StoredProcedure [dbo].[pr_Clinical_GetARTHistoryData_Futures]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_GetARTHistoryData_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_GetARTHistoryData_Futures]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_Clinical_GetARTHistoryData_Futures]                         
@Ptn_Pk int,           
@VisitId int,           
@LocationId int          
AS                        
BEGIN
Set Nocount On;

declare @Visitdate datetime
--0        
If exists (Select Visit_Id	From ord_visit	Where Ptn_Pk = @Ptn_Pk And Visittype = 18 And LocationID = @LocationId)
Begin
	Select	@VisitId = Visit_Id,
			@Visitdate = VisitDate
	From dbo.ord_Visit
	Where Ptn_Pk = @Ptn_Pk And Visittype = 18 And LocationID = @LocationId
	--0    
	Select	@VisitId [VisitId],
			@Visitdate [VisitDate]
	--1      
	Select	Ptn_pk,
			LocationID,
			Visit_pk,
			Nullif(ARTTransferInDate, '1900-01-01') [ARTTransferInDate],
			ARTTransferInFrom,
			FromDistrict
	From dbo.dtl_PatientHivPrevCareIE
	Where Ptn_Pk = @Ptn_Pk And Visit_pk = @VisitId And LocationID = @LocationId
	--2        
	Select	Ptn_pk,
			LocationID,
			Visit_pk,
			PriorART,
			HIVCareWhere
	From dtl_PriorArvAndHivCare
	Where Ptn_Pk = @Ptn_Pk And Visit_pk = @VisitId And LocationID = @LocationId
	--3        
	Select	ptn_pk,
			locationID,
			Visit_pk,
			Nullif(ARTStartDate, '1900-01-01') [ARTStartDate],
			Nullif(ConfirmHIVPosDate, '1900-01-01') [ConfirmHIVPosDate],
			DateEnrolledInCare
	From dtl_PatientHivPrevCareEnrollment
	Where Ptn_Pk = @Ptn_Pk And Visit_pk = @VisitId And LocationID = @LocationId
	--4        
	Select	ptn_pk,
			locationID,
			Visit_pk,
			WHOStage
	From dtl_PatientStage
	Where Ptn_Pk = @Ptn_Pk And Visit_pk = @VisitId And LocationID = @LocationId
	--5        
	Select	ptn_pk,
			locationID,
			Visit_pk,
			DrugMedicalID,
			DrugAllergies
	From dtl_DrugAllegiesAndMedicalCondition
	Where Ptn_Pk = @Ptn_Pk And Visit_pk = @VisitId And LocationID = @LocationId
	--6        
	--Select RegistrationDate
	--From mst_patient
	--Where Ptn_pk = @Ptn_Pk And LocationId = @LocationId
	
	Select Top 1 StartDate EnrollmentDate From Lnk_PatientProgramStart Where Ptn_pk = @Ptn_Pk And ModuleId=203 Order by StartDate Asc
	
	--7        
	Select	ptn_pk,
			locationID,
			Visit_pk [VisitId],
			a.PurposeId,
			b.Name [Purpose],
			a.Regimen [Regimen],
			a.DateLastUsed [RegLastUsed]
	From dtl_PatientBlueCardPriorART a
	Inner Join Mst_Decode b
		On a.PurposeID = b.ID
	Where a.Ptn_Pk = @Ptn_Pk And a.Visit_pk = @VisitId And a.LocationID = @LocationId
        
 End    
else     
   Begin
--0    
Select '0' [VisitId]
--    
--Select RegistrationDate
--From mst_patient
--Where Ptn_pk = @Ptn_Pk And LocationId = @LocationId

Select Top 1 StartDate EnrollmentDate From Lnk_PatientProgramStart Where Ptn_pk = @Ptn_Pk And ModuleId=203 Order by StartDate Asc
End
    
End


GO



/****** Object:  StoredProcedure [dbo].[pr_Clinical_UpdateInitialFollowupVisit]    Script Date: 09/04/2015 13:36:26 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_UpdateInitialFollowupVisit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_UpdateInitialFollowupVisit]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[pr_Clinical_UpdateInitialFollowupVisit]                            
	 @patientID int,                  
	 @UserId int,                              
	 @locationID int,          
	 @visitID int,                                  
	 @dataQuality int,   
	 @ModuleId int =null,                
	 @visitDate datetime=null,                
	 @TypeofVisit int=null,               
	 @Scheduled int=null ,                               
	 @treatmentSupporterName varchar(50)=null,                                  
	 @treatmentSupporterContact varchar(50)=null,
	 @Temp varchar(5)=null,                                  
	 @height varchar(5)=null,                                  
	 @weight varchar(5)=null,  
	 @Muac varchar(5) = null,                                
	 @BPSystolic int=null,                
	 @BPDiastolic int=null,                
	 @pregnant int=null,                                  
	 @EDD datetime=null,                                  
	 @ANCNo int=null,                     
	 @ReferredtoPMTCT varchar(100)=null,                                  
	 @DateofInducedAbortion varchar(30)=null,                                    
	 @DateofMiscarriage varchar(30)=null,                             
	 @familyPlanningStatus int=null,              
	 @NoFamilyPlanning int=null,                                
	 @TBStatus int=null,                                  
	 @TBRxStart datetime=null,                                  
	 @TBRegNumber varchar(50)=null,                              
	 @nutritionalProblem   int=null,               
	 @WHOStage int,               
	 @CotrimoxazoleAdhere int=null,                                  
	 @ARVDrugsAdhere int=null,                                  
	 @WhyPooFair int=null,                                  
	 @reasonARVDrugsPoorFairOther varchar(50)=null,                
	 @TherapyPlan int=null,                            
	 @TherapyReasonCode int=null,                            
	 @TherapyOther varchar(100)=null,                            
	 @PrescribedARVStartDate datetime =null,                            
	 @numOfDaysHospitalized varchar(5)=null,                                  
	 @nutritionalSupport int=null,                                  
	 @infantFeedingOption int=null,                                  
	 @attendingClinician int=null,                
	 @Datenextappointment datetime=null,          
	 @createDate datetime=null                             
AS
Set Nocount On;

Begin
	Declare @updateDate datetime, 
		@SafeHeight decimal(7,1), 
		@SafeWeight decimal(7,1),
		@SafeTemp decimal(7,1),
		@SafeBPSystolic decimal(7,0),
		@SafeBPDiastolic decimal(7,0),
		@SafeMuac decimal(4,1);;

	Set @updateDate = Getdate()

		Select	@SafeHeight =
					Case
						When Isnumeric(nullif(@Height,'')) = 1 Then Convert(decimal(7, 2), @Height)
						Else Null End,
				@SafeWeight =
					Case
						When Isnumeric(nullif(@Weight,'')) = 1 Then Convert(decimal(7, 2), @Weight)
						Else Null End,
				@SafeTemp =
					Case
						When Isnumeric(nullif(@Temp,'')) = 1 Then Convert(decimal(7, 1), @Temp)
						Else Null End,
				@SafeBPSystolic =
					Case
						When Isnumeric(nullif(@BPSystolic,'')) = 1 Then Convert(decimal(7, 0), @BPSystolic)
						Else Null End,
				@SafeBPDiastolic =
					Case
						When Isnumeric(nullif(@BPDiastolic,'')) = 1 Then Convert(decimal(7, 0), @BPDiastolic)
						Else Null End,
				@SafeMuac =
					Case
						When Isnumeric(nullif(@Muac,'')) = 1 Then Convert(decimal(7, 0), @Muac)
						Else Null End;
	

	--Visit Date & Attending Clinician                            
	Update ord_visit Set
		DataQuality = @dataQuality,
		UpdateDate = @updateDate,
		UserID = @UserID,
		VisitDate = @visitDate,
		TypeofVisit = @TypeofVisit
	Where Ptn_Pk = @patientID
	And LocationID = @locationID
	And Visit_Id = @visitID;

-----------SCM Section--------------------------------------------------          
	Update Dtl_PatientBillTransaction Set
		TransactionDate = @visitDate,
		ConsultancyFee = dbo.fn_GetConsultationPerVisit_Futures(@visitDate),
		AdminFee = dbo.fn_GetOverHeadPerVisit_Futures(@visitDate),
		BillAmount = dbo.fn_GetConsultationPerVisit_BillAmount_Futures(@visitDate) + dbo.fn_GetOverHeadPerVisit_BillAmount_Futures(@visitDate),
		DoctorId = @attendingClinician,
		UserId = @userID,
		UpdateDate = Getdate()
	Where VisitId = @visitID;

	--TreatmentSupporter Name & Contact                            
	If Exists (Select 1	From dtl_PatientARTEncounter	Where Ptn_Pk = @patientID	
		And LocationID = @locationID	
		And Visit_Id = @visitID) 
	Begin
		Update dtl_PatientARTEncounter Set
			SupporterName = Nullif(@treatmentSupporterName, ''),
			TreatmentSupporterContact = Nullif(@treatmentSupporterContact, ''),
			TBRegistration = Nullif(@TBRegNumber, ''),
			NutritionalProblem = Nullif(@nutritionalProblem, ''),
			AttendingClinician = Nullif(@attendingClinician, ''),
			Scheduled = Nullif(@Scheduled, 0),
			UpdateDate = @updateDate,
			UserID = @UserID
		Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_Id = @visitID;
	End 
	Else Begin

		Insert Into dtl_PatientARTEncounter (
			Ptn_Pk,
			Visit_Id,
			LocationId,
			Scheduled,
			SupporterName,
			TreatmentSupporterContact,
			TBRegistration,
			NutritionalProblem,
			AttendingClinician,
			UserId,
			CreateDate)
		Values (
			@patientID, @visitID, @locationID, 
			Nullif(@Scheduled, 0), 
			Nullif(@treatmentSupporterName, ''), 
			Nullif(@treatmentSupporterContact, ''),
			Nullif(@TBRegNumber, ''), 
			@nutritionalProblem, 
			@attendingClinician, 
			@UserId, 
			Getdate());

	End

------------------------------------------------------------------------                                 

	--Date of next appointment                            
	If Exists (Select 1	From dtl_PatientAppointment	Where Ptn_Pk = @patientID	
		And LocationID = @locationID 
		And Visit_pk = @visitID 
		And AppReason = 110 
		And AppStatus = 12) 
	Begin
		Update dtl_PatientAppointment Set
			AppDate = Isnull(@Datenextappointment,Appdate),
			UpdateDate = @updateDate,
			UserID = @UserID,
			ModuleID = Isnull(ModuleId,203)
		Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID
		And AppReason = 110
		And AppStatus = 12;
	End 
	Else If (nullif(@Datenextappointment,'') Is Not null) 
	Begin
		Insert Into dtl_PatientAppointment (
			Ptn_pk,
			LocationID,
			Visit_pk,
			AppDate,
			CreateDate,
			AppReason,
			AppStatus,
			EmployeeID,
			UserID,
			ModuleID)
		Values (
			@patientID, 
			@locationID, 
			@visitID, 
			Nullif(@Datenextappointment, ''), 
			@updateDate, 
			110, 
			12, 
			@UserID, 
			@UserID,
			203
		);	
	End

--Clinical Status                                 
--Height & Weight & Oedema                            
	If Exists (Select 1	From dtl_PatientVitals	Where Ptn_Pk = @patientID	
		And LocationID = @locationID 
		And Visit_pk = @visitID) 
	Begin
		Update dtl_PatientVitals Set
			Height = @SafeHeight,
			Weight = @SafeWeight,
			Temp = @SafeTemp,--Nullif(@Temp, ''),
			BPSystolic = @SafeBPSystolic,-- Nullif(@BPSystolic, ''),
			BPDiastolic = @SafeBPDiastolic ,-- Nullif(@BPDiastolic, ''),
			Muac=@SafeMuac,
			UpdateDate = @updateDate,
			UserID = @UserID
		Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID;
	End 
	Else If (@SafeHeight Is Not Null 
		Or @SafeWeight Is Not Null 
		Or @SafeBPSystolic <> 0 
		Or @SafeBPDiastolic  Is Not Null 
		Or @SafeTemp Is Not Null) 
	Begin
		Insert Into dtl_PatientVitals (
			Ptn_pk,
			LocationID,
			Visit_pk,
			Height,
			Weight,
			Muac,
			BPDiastolic,
			BPSystolic,
			CreateDate,
			UserID,
			Temp)
		Values (
			@patientID, 
			@locationID, 
			@visitID, 
			@SafeHeight,--Nullif(@height, ''), 
			@SafeWeight,--Nullif(@weight, ''), 
			@Muac,
			@SafeBPDiastolic , --Nullif(@BPDiastolic, 0), 
			@SafeBPSystolic  , --Nullif(@BPSystolic, 0), 
			Getdate(), 
			@UserID, 
			@SafeTemp -- @Temp
			);
	End


--Pregnant & EDD & Delivery Date & PMTCT & MUAC                           
	If Exists (Select 1	From dtl_PatientClinicalStatus	Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID) 
	Begin		
		Update dtl_PatientClinicalStatus Set
			Pregnant = Nullif(@pregnant, -1),
			EDD = Nullif(@EDD, ''),
			DateofMiscarriage = Nullif(@DateofMiscarriage, ''),
			DateofInducedAbortion = Nullif(@DateofInducedAbortion, ''),
			PMTCT = @ReferredtoPMTCT,
			UpdateDate = @updateDate,
			UserID = @UserID
		Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID;
	End 
	Else If (@pregnant <> -1) 
	Begin
		Insert Into dtl_PatientClinicalStatus (
			Ptn_pk,
			LocationID,
			Visit_pk,
			Pregnant,
			EDD,
			DateofMiscarriage,
			DateofInducedAbortion,
			PMTCT,
			CreateDate,
			UserID)
		Values (
			@patientID, 
			@locationID, 
			@visitID, 
			Nullif(@pregnant, -1), 
			Nullif(@EDD, ''), 
			Nullif(@DateofMiscarriage, ''),
			Nullif(@DateofInducedAbortion, ''), 
			@ReferredtoPMTCT, Getdate(), 
			@UserID);
	End
	If (@ReferredtoPMTCT = 1 And @ANCNo <> '' And @pregnant = 89) 
	Begin	
		Update mst_patient Set
			ANCNumber = Isnull(Nullif(AncNumber,''), @ANCNo)
		Where Ptn_pk = @patientID ;	
	End	
--If (@ReferredtoPMTCT = 1 And @ANCNo <> '' And @pregnant = 89) Begin
--If (Exists (Select ANCNumber
--	From mst_patient
--	Where ptn_pk = @patientID
--	And (ANCNumber Is Null
--	Or ANCNumber = ''))
--) Begin
--Update mst_patient Set
--	ANCNumber =  @ANCNo
--Where Ptn_pk = @patientID 
--End
--End

	--TB Status                                
	If Exists (Select 1	From dtl_PatientOtherTreatment	Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID) 
	Begin
	--If (@TBRxStart <> '') Begin
	--Set @TBRxStart = @TBRxStart;
	--End

		Update dtl_PatientOtherTreatment Set
			TBStatus = Nullif(@TBStatus, 0),
			TBRxStartDate = Nullif(@TBRxStart, ''),
			UpdateDate = @updateDate,
			UserID = @UserID
		Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID;
	End 
	Else Begin
	--If (@TBStatus <> 0 And @TBRxStart <> '') Begin
	--Set @TBRxStart = @TBRxStart;
	--End

	Insert Into dtl_PatientOtherTreatment (
		Ptn_pk,
		Visit_pk,
		LocationID,
		TBStatus,
		TBRxStartDate,
		CreateDate,
		UserID)
	Values (
		@patientID, 
		@visitID, 
		@locationID, 
		@TBStatus, 
		Nullif(@TBRxStart, ''), 
		@updateDate, 
		@UserID);
	End
--End


	Update dtl_PatientARVTherapy Set
		TherapyPlan = Nullif(@TherapyPlan, 0),
		TherapyReasonCode = Nullif(@TherapyReasonCode, ''),
		TherapyOther = Nullif(@TherapyOther, ''),
		PrescribedARVStartDate = Nullif(@PrescribedARVStartDate, ''),
		UpdateDate = Getdate(),
		UserID = @UserID
	Where Ptn_Pk = @patientID
	And LocationID = @locationID
	And Visit_pk = @visitID;


	If Exists (Select Top 1 *	From dtl_patientcareended	Where Ptn_Pk = @patientID
		And LocationId = @locationID) 
	Begin
	If (@TherapyPlan = 99) Begin
		Update dtl_PatientTrackingCare Set
			DateLastContact = @PrescribedARVStartDate,
			EmployeeID = @attendingClinician,
			UserID = @UserID,
			UpdateDate = @updateDate
		Where Ptn_Pk = @patientID
		And LocationId = @locationID;

		Update dtl_patientcareended Set
			ARTenddate = @PrescribedARVStartDate,
			ARTendreason = @TherapyReasonCode
		Where Ptn_Pk = @patientID
		And LocationId = @locationID;
	End
	End 
	Else If (@TherapyPlan = 99) 
	Begin
		Declare @TrackingID int;
		Insert dtl_PatientTrackingCare (
			Ptn_Pk,
			DateLastContact,
			EmployeeID,
			UserID,
			CreateDate,
			LocationId,
			ModuleId)
		Values (
			@patientID, 
			@PrescribedARVStartDate, 
			@attendingClinician, 
			@UserID, 
			@updateDate, 
			@locationID, 
			203
		);
		Select @TrackingID = SCOPE_IDENTITY();
		Insert dtl_patientcareended (
			ptn_pk,
			ARTended,
			ARTenddate,
			ARTendreason,
			CreateDate,
			LocationId,
			TrackingId)
		Values (
			@patientID, 
			1, 
			@PrescribedARVStartDate, 
			@TherapyReasonCode, 
			@updateDate, 
			@locationID, 
			@TrackingID
		);
	End

	If (@TherapyPlan = 96) 	Begin
		If Exists (Select *	From dtl_patientARTRestart	Where Ptn_pk = @patientID
			And locationId = @locationID
			And Visit_Pk = @visitID
			And (Deleteflag = 0
			Or deleteflag Is Null)) 
		Begin
			Update dtl_PatientARTRestart Set
				RestartDate = @visitDate,
				UpdateDate = Getdate()
			Where Ptn_pk = @patientID
			And locationId = @locationID
			And Visit_Pk = @visitID
			And (Deleteflag = 0
			Or deleteflag Is Null);
		End 
		Else Begin
			Insert Into dtl_PatientARTRestart (
				Ptn_Pk,
				LocationId,
				Visit_Pk,
				RestartDate,
				UserID,
				CreateDate)
			Values (
				@patientID, 
				@locationID, 
				@visitID, 
				@visitDate, 
				@userID, 
				Getdate()
			);
		End
	End 
	Else If Exists (Select *	From dtl_patientARTRestart	Where Ptn_pk = @patientid
		And locationId = @locationID
		And Visit_Pk = @visitID
		And (Deleteflag = 0
		Or deleteflag Is Null)) 
	Begin
		Update dtl_PatientARTRestart Set
			UpdateDate = Getdate(),
			DeleteFlag = 1
		Where Ptn_pk = @patientID
		And locationId = @locationID
		And Visit_Pk = @visitID
		And (Deleteflag = 0
		Or deleteflag Is Not Null);
	End


	--If Exists (Select *	From Dtl_PatientFamilyPlanning	Where Ptn_Pk = @patientID
	--	And LocationID = @locationID
	--	And Visit_Id = @visitID) 
	--Begin
		Delete From Dtl_PatientFamilyPlanning
		Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_Id = @visitID;
	--End

	--If Exists (Select *	From dtl_PatientDisease	Where Ptn_Pk = @patientID
	--	And LocationID = @locationID
	--	And Visit_pk = @visitID) 
	--Begin
		Delete From dtl_PatientDisease
		Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID;
	--End

	--If Exists (Select *	From dtl_PatientSymptoms	Where Ptn_Pk = @patientID
	--	And LocationID = @locationID
	--	And Visit_pk = @visitID) 
	--Begin
		Delete From dtl_PatientSymptoms
		Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID;
	--End
	--If Exists (Select *	From dtl_PatientAtRiskPopulation	Where Ptn_Pk = @patientID
	--	And LocationID = @locationID
	--	And Visit_pk = @visitID) 
	--Begin
		Delete From dtl_PatientAtRiskPopulation
		Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID;
	--End
	--If Exists (Select *	From dtl_PatientAtRiskPopulationServices Where Ptn_Pk = @patientID
	--	And LocationID = @locationID
	--	And Visit_pk = @visitID) 
	--Begin
		Delete From dtl_PatientAtRiskPopulationServices
		Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID;
	--End
	--If Exists (Select *	From dtl_PatientPreventionwithpositives	Where Ptn_Pk = @patientID
	--	And LocationID = @locationID
	--	And Visit_pk = @visitID) 
	--Begin
		Delete From dtl_PatientPreventionwithpositives
		Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID;
	--End


	--WAB Stage & WHO Stage                              
	If Exists (Select *		From dtl_PatientStage	Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID) 
	Begin
		Update dtl_PatientStage Set
			WHOStage = Nullif(@WHOStage, 0),
			UpdateDate = @updateDate,
			UserID = @UserID
		Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID;
	End 
	Else If (@WHOStage <> 0) Begin
		Insert Into dtl_PatientStage (
			Ptn_pk,
			Visit_Pk,
			LocationID,
			WHOStage,
			CreateDate)
		Values (
			@patientID, 
			@visitID, 
			@locationID, 
			Nullif(@WHOStage, 0), 
			@updateDate);
	End
--CPT Adhere                            
	If Exists (Select *	From dtl_Adherence_Reason	Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID) 
	Begin
		Update dtl_Adherence_Reason Set
			CotrimoxazoleAdhere = Nullif(@CotrimoxazoleAdhere, 0),
			UpdateDate = @updateDate,
			UserID = @UserID
		Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID;
	End 
	Else If (@CotrimoxazoleAdhere <> 0) Begin
		Insert Into dtl_Adherence_Reason (
			Ptn_pk,
			Visit_pk,
			LocationID,
			CotrimoxazoleAdhere,
			CreateDate)
		Values (
			@patientID, 
			@visitID, 
			@locationID, 
			@CotrimoxazoleAdhere, 
			@updateDate);
	End


	--ARV Drugs Adhere + Reason                            
	If Exists (Select *		From dtl_PatientAdherence	Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID) 
	Begin
	Update dtl_PatientAdherence Set
		ARVAdhere = Nullif(@ARVDrugsAdhere, 0),
		AdherenceReason = Nullif(@WhyPooFair, 0),
		AdherenceReasonOther = Nullif(@reasonARVDrugsPoorFairOther, ''),
		UpdateDate = @updateDate,
		UserID = @UserID
	Where Ptn_Pk = @patientID
	And LocationID = @locationID
	And Visit_pk = @visitID
	End 
	Else If (@ARVDrugsAdhere <> 0) Begin
		Insert Into dtl_PatientAdherence (
			Ptn_pk,
			Visit_pk,
			LocationID,
			ARVAdhere,
			AdherenceReason,
			AdherenceReasonOther,
			CreateDate)
		Values (
			@patientID, 
			@visitID, 
			@locationID, 
			Nullif(@ARVDrugsAdhere, 0),
			 Nullif(@WhyPooFair, 0), 
			Nullif(@reasonARVDrugsPoorFairOther, ''), 
			@updateDate);
	End
	--ReferredTo + Other                              
	If Exists (Select *	From dtl_PatientReferredTo	Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID) 
	Begin
		Delete From dtl_PatientReferredTo
		Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID;
	End



--Number of Days Hospitalized & Nutritional Support                            
	If Exists (Select *	From dtl_patientCounseling	Where Ptn_Pk = @patientID
	And LocationID = @locationID
	And Visit_pk = @visitID) 
	Begin
		Update dtl_patientCounseling Set
			HospitalizedNumberofDays = Nullif(@numOfDaysHospitalized, ''),
			NutritionalSupport = Nullif(@nutritionalSupport, 0),
			FamilyPlanningStatus = Nullif(@familyPlanningStatus, ''),
			NoFamilyPlanning = Nullif(@NoFamilyPlanning, 0),
			UpdateDate = @updateDate,
			UserID = @UserID
		Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID;
	End 
	Else If (@numOfDaysHospitalized <> '' Or @nutritionalSupport <> 0 Or @familyPlanningStatus <> '' Or @NoFamilyPlanning = '') Begin
		Insert Into dtl_patientCounseling (
			Ptn_pk,
			LocationID,
			Visit_pk,
			HospitalizedNumberofDays,
			NutritionalSupport,
			CreateDate,
			FamilyPlanningStatus,
			UserID,
			NoFamilyPlanning)
		Values (
			@patientID, 
			@locationID, 
			@visitID, 
			Nullif(@numOfDaysHospitalized, ''), 
			Nullif(@nutritionalSupport, 0), 
			@updateDate, 
			Nullif(@familyPlanningStatus, ''), 
			@UserID, Nullif(@NoFamilyPlanning, 0));
	End


--Infant Feeding Option                              
	If Exists (Select *	From dtl_InfantInfo	Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID) 
	Begin
		Update dtl_InfantInfo Set
			FeedingOption = Nullif(@infantFeedingOption, 0),
			UserID = @UserID
		Where Ptn_Pk = @patientID
		And LocationID = @locationID
		And Visit_pk = @visitID;
	End 
	Else If (@infantFeedingOption <> 0) Begin
		Insert Into dtl_InfantInfo (
			Ptn_pk,
			LocationID,
			Visit_pk,
			FeedingOption,
			CreateDate)
		Values (
			@patientID, 
			@locationID, 
			@visitID, 
			@infantFeedingOption, 
			@updateDate);
	End
	Select	Ptn_Pk,
			VisitDate,
			Visit_Id As 'visitID',
			LocationID
	From ord_visit
	Where Visit_Id = @visitID;

End

GO







Go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_SaveARTHistory_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_SaveARTHistory_Futures]
GO

/****** Object:  StoredProcedure [dbo].[pr_Clinical_SaveARTHistory_Futures]    Script Date: 8/5/2016 7:57:25 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_Clinical_SaveARTHistory_Futures]                           
 @Ptn_Pk int,                          
 @VisitId int=0,                      
 @LocationId int,                      
 @TransferInDate datetime,                      
 @TransferInFrom varchar(50),                      
 @dddistrict varchar(50),                      
 @DateARTStarted datetime,                      
 @PriorART int,                      
 @ConfirmHIVPosDate datetime,                      
 @Where Varchar(100),                      
 @EnrolledinHIVCare datetime,                      
 @WHOStage int,                      
 @DrugAllergy varchar(100),                      
 @UserId int,          
 @DataQlty int ,
 @ModuleId int                     
                       
AS                            
SET NOCOUNT ON;                             
Begin                      
--0        
	declare @vdate datetime  
    
	Select @vdate = startdate
	From dbo.Lnk_PatientProgramStart
	Where ptn_pk = @Ptn_Pk
		And ModuleId = 203

	Insert Into Ord_Visit (
			Ptn_Pk
		,	LocationID
		,	VisitDate
		,	VisitType
		,	DataQuality
		,	UserId
		,	CreateDate
		,	ModuleID)
	Values (
			@Ptn_Pk
		,	@LocationId
		,	@vdate
		,	18
		,	@DataQlty
		,	@UserId
		,	getdate()
		,	@ModuleId);
	Select @VisitId = scope_identity();
	--1                      
	Insert Into dtl_PatientHivPrevCareIE (
			Ptn_pk
		,	LocationID
		,	Visit_pk
		,	ARTTransferInDate
		,	ARTTransferInFrom
		,	FromDistrict
		,	UserId
		,	CreateDate)
	Values (
			@Ptn_Pk
		,	@LocationId
		,	@VisitId
		,	@TransferInDate
		,	@TransferInFrom
		,	@dddistrict
		,	@UserId
		,	getdate())
	--2                      
	Insert Into dtl_PriorArvAndHivCare (
			Ptn_pk
		,	LocationID
		,	Visit_pk
		,	PriorART
		,	HIVCareWhere
		,	UserId
		,	CreateDate)
	Values (
			@Ptn_Pk
		,	@LocationId
		,	@VisitId
		,	@PriorART
		,	@Where
		,	@UserId
		,	getdate())
	--3                      
	Insert Into dtl_PatientHivPrevCareEnrollment (
			ptn_pk
		,	locationID
		,	Visit_pk
		,	ARTStartDate
		,	ConfirmHIVPosDate
		,	UserId
		,	CreateDate)
	Values (
			@Ptn_Pk
		,	@LocationId
		,	@VisitId
		,	@DateARTStarted
		,	@ConfirmHIVPosDate
		,	@UserId
		,	getdate())
	--4                      
	--select * from dtl_PatientStage                  
	Insert Into dtl_PatientStage (
			ptn_pk
		,	locationID
		,	Visit_pk
		,	WHOStage
		,	UserId
		,	CreateDate)
	Values (
			@Ptn_Pk
		,	@LocationId
		,	@VisitId
		,	@WHOStage
		,	@UserId
		,	getdate())
	--5                      
	--select * from dtl_DrugAllegiesAndMedicalCondition                  
	Insert Into dtl_DrugAllegiesAndMedicalCondition (
			ptn_pk
		,	locationID
		,	Visit_pk
		,	DrugMedicalID
		,	DrugAllergies
		,	UserId
		,	CreateDate)
	Values (
			@Ptn_Pk
		,	@LocationId
		,	@VisitId
		,	0
		,	@DrugAllergy
		,	@UserId
		,	getdate())
	--0 Get                      
	Select @VisitId [VisitId]                      
End

GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_UpdateARTHistory_Futures]') AND type in (N'P', N'PC')) 
DROP PROCEDURE [dbo].[pr_Clinical_UpdateARTHistory_Futures]
GO

/****** Object:  StoredProcedure [dbo].[pr_Clinical_UpdateARTHistory_Futures]    Script Date: 8/5/2016 7:52:43 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_Clinical_UpdateARTHistory_Futures]                       
 @Ptn_Pk int,                      
 @VisitId int=0,                  
 @LocationId int,                  
 @TransferInDate datetime,                  
 @TransferInFrom varchar(50),                      
 @dddistrict varchar(50),                   
 @DateARTStarted datetime,                  
 @PriorART int,                  
 @ConfirmHIVPosDate datetime,                  
 @Where Varchar(100),                  
 @EnrolledinHIVCare datetime,                  
 @WHOStage int,                  
 @DrugAllergy varchar(100),                  
 @UserId int,  
 @DataQlty int,
 @ModuleId int = null                 
                   
AS
Set Nocount On;
Begin

Declare @VisitDate datetime;
	Select 
		@VisitDate = VisitDate 
	From ord_visit 
	Where Ptn_Pk = @Ptn_Pk
	And LocationID = @LocationId
	And Visit_Id = @VisitId
--0  
	Update Ord_Visit Set
		VisitDate = @VisitDate,
		UserId = @UserId,
		UpdateDate = Getdate(),
		DataQuality = @DataQlty
	Where Ptn_Pk = @Ptn_Pk
	And LocationID = @LocationId
	And Visit_Id = @VisitId

--1             
	Update dtl_PatientHivPrevCareIE Set
		ARTTransferInDate = @TransferInDate,
		ARTTransferInFrom = @TransferInFrom,
		FromDistrict = @dddistrict,
		UserId = @UserId,
		UpdateDate = Getdate()
	Where Ptn_Pk = @Ptn_Pk
	And LocationID = @LocationId
	And Visit_pk = @VisitId
--2                  
	Update dtl_PriorArvAndHivCare Set
		PriorART = @PriorART,
		HIVCareWhere = @Where,
		UserId = @UserId,
		UpdateDate = Getdate()
	Where Ptn_Pk = @Ptn_Pk
	And LocationID = @LocationId
	And Visit_pk = @VisitId
--3          
	Update dtl_PatientHivPrevCareEnrollment Set
		ARTStartDate = @DateARTStarted,
		ConfirmHIVPosDate = @ConfirmHIVPosDate,
		UserId = @UserId,
		UpdateDate = Getdate(),
		DateEnrolledInCare = @EnrolledinHIVCare
	Where Ptn_Pk = @Ptn_Pk
	And LocationID = @LocationId
	And Visit_pk = @VisitId
--4                  

	Update dtl_PatientStage Set
		WHOStage = @WHOStage,
		UserId = @UserId,
		UpdateDate = Getdate()
	Where Ptn_Pk = @Ptn_Pk
	And LocationID = @LocationId
	And Visit_pk = @VisitId
--5                  
	Update dtl_DrugAllegiesAndMedicalCondition Set
		DrugAllergies = @DrugAllergy,
		UserId = @UserId,
		UpdateDate = Getdate()
	Where Ptn_Pk = @Ptn_Pk
	And LocationID = @LocationId
	And Visit_pk = @VisitId

	Delete From dbo.dtl_PatientBlueCardPriorART
	Where Ptn_pk = @Ptn_Pk
		And Visit_pk = @VisitId
		And LocationID = @LocationId
--0 Get                  
Select @VisitId [VisitId]
End

GO




/****** Object:  StoredProcedure [dbo].[pr_Clinical_GetARVTherapyPatientData]    Script Date: 01/13/2016 09:29:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_Clinical_GetARVTherapyPatientData] 
(                                                   
 @patientID int,                                      
 @LocationId int    
 )                                               
AS                                                    
 Begin
                                                    
  --0 ART Start Date and Regimen Start Date                                                     
  declare @artstart datetime,
		  @DOB datetime,
		  @ClosestARVDate datetime,
		  @FirstLineRegimen varchar(200), 
		  @Countchngregimen int,
		  @Visit_Pk int,
		  @Muac decimal(4,1);
	Select @Muac = null;

	Select 
		@Visit_Pk = ord.Visit_Id
	From ord_Visit ord
	Where ord.VisitType = 19
	And ord.Ptn_pk = @patientID
	And ord.LocationId = @LocationId;

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
             
	Select @artstart = ARTStartDate, @DOB = DOB	From mst_Patient	Where Ptn_Pk = @patientID	And LocationID = @LocationId;
	Select @artstart As 'ARTStartDate';  
	---1 ARV Regimen list                                        
	Select	A.GenericID As DrugId,
			A.GenericName As DrugName,
			'0' As Generic,
			B.DrugTypeId,
			A.GenericAbbrevation As Abbr
	From mst_Generic As A
	Inner Join
		lnk_DrugTypeGeneric As B On A.GenericID = B.GenericId
	Where (A.DeleteFlag = 0)
	And (B.DrugTypeId = 37)
	Order By DrugName;

--2 Height 
	If (@ClosestARVDate Is Not Null) Begin
		Select Top 1 dtl.Height
		From Ord_visit ord
		Inner Join
			dtl_PatientVitals dtl On dtl.visit_pk = ord.Visit_Id
		Where ord.ptn_pk = @patientID
		And dtl.Height Is Not Null
		Order By Abs(Datediff(Day, Ord.VisitDate, @ClosestARVDate));
	End 
	Else Begin
		Select '' [Height];
	End

--3 Weight                             
	If (@ClosestARVDate Is Not Null) Begin
		Select Top (1) dtl.[Weight]
		From ord_Visit As ord
		Inner Join
			dtl_PatientVitals As dtl On dtl.Visit_pk = ord.Visit_Id
		Where (ord.Ptn_Pk = @patientID)
		And (dtl.[Weight] Is Not Null)
		Order By Abs(Datediff(Day, ord.VisitDate, @ClosestARVDate));
	End 
	Else Begin
		Select '' [Weight]
	End
--4 Previous                 

	Select	dtl.ptn_pk,
			dtl.visit_Id,
			dtl.locationId,
			dtl.FirstLineRegStDate,
			dtl.FirstLineReg,
			dtl.CD4,
			dtl.CD4Percent,
			dtl.Pregnant,
			dtl.UserId,
			dtl.CreateDate,
			dtl.UpdateDate,
			ord.VisitDate,
			ord.VisitType,
			ord.DataQuality,
			ord.DeleteFlag,
			ord.TypeofVisit,
			ord.OrderedBy,
			ord.OrderedDate,
			ord.ReportedBy,
			ord.ReportedDate,
			ord.Signature,
			ord.CreatedBy,			
			V.Visit_pk,
			V.Temp,
			V.RR,
			V.HR,
			V.BPDiastolic,
			V.BPSystolic,
			V.Height,
			V.Weight,
			V.Pain,
			V.TLC,
			V.TLCPercent,
			V.Oedema,
			V.Muac
	From dtl_PatientARTCare As dtl
	Inner Join
		ord_Visit As ord On dtl.ptn_pk = ord.Ptn_Pk
		And dtl.locationId = ord.LocationID
		And dtl.visit_Id = ord.Visit_Id
	Left Outer Join
		dtl_PatientVitals As V On V.Visit_pk = dtl.visit_Id
		And V.Ptn_pk = dtl.ptn_pk
	Where (ord.VisitType = 19)
	And (dtl.ptn_pk = @patientID)
	And (dtl.locationId = @LocationId);

--5 Pregnant Status                                      

	Select Top (1) Case Z.Pregnant
			When 0 Then 3
			Else 1 End pregnant
	From (
		Select	dtl.Pregnant,
				ord.VisitDate
		From ord_Visit As ord
		Inner Join
			dtl_PatientClinicalStatus As dtl On dtl.Visit_pk = ord.Visit_Id
		Where (ord.Ptn_Pk = @patientID)
		And (ord.LocationID = @LocationId)
		Union 
		Select	dtl.Pregnant,
				ord.VisitDate
		From ord_Visit As ord
		Inner Join
			dtl_PatientARTCare As dtl On dtl.visit_Id = ord.Visit_Id
		Where (ord.Ptn_Pk = @patientID)
		And (ord.LocationID = @LocationId)
		Union 
		Select	dtl.Pregnant,
				ord.VisitDate
		From ord_Visit As ord
		Inner Join
			dtl_PatientClinicalStatus As dtl On dtl.Visit_pk = ord.Visit_Id
		Where (ord.Ptn_Pk = @patientID)
		And (ord.LocationID = @LocationId)
		) Z
	Where (Z.VisitDate > Dateadd(Month, -3, @artstart)
	And Z.VisitDate < Dateadd(wk, 2, @artstart)) 
	Order By z.VisitDate Desc;

---6 CD4                  

	If (@artstart != '' Or @artstart Is Not Null) Begin
		Select Top (1) dtl.TestResults As CD4
		From dtl_PatientLabResults As dtl
		Inner Join
			ord_PatientLabOrder As ord On dtl.LabID = ord.LabID
		Where (ord.Ptn_pk = @patientID)
		And (dtl.ParameterID = 1)
		And (dtl.TestResults Is Not Null)
		And (ord.OrderedbyDate > Dateadd(Month, -3, @artstart))
		And (ord.OrderedbyDate < Dateadd(wk, 2, @artstart));
	--order by abs(DATEDIFF(DAY,Ord.OrderedbyDate,@ClosestARVDate))        
	End 
	Else Begin
		Select '' [CD4]
	End
--7 CD4 Percent                            
	If (@artstart != '' Or @artstart Is Not Null) Begin
		Select Top (1) dtl.TestResults As CD4Percent
		From dtl_PatientLabResults As dtl
		Inner Join
			ord_PatientLabOrder As ord On dtl.LabID = ord.LabID
		Where (ord.Ptn_pk = @patientID)
		And (dtl.ParameterID = 2)
		And (dtl.TestResults Is Not Null)
		And (ord.OrderedbyDate > Dateadd(Month, -3, @artstart))
		And (ord.OrderedbyDate < Dateadd(wk, 2, @artstart));
	--order by abs(DATEDIFF(DAY,Ord.OrderedbyDate,@ClosestARVDate))                            
	End 
	Else Begin
		Select '' [CD4Percent]
	End

---8 First Line Art Regimen                                      
	Exec @FirstLineRegimen = fn_GetPatientFirstLineARTRegimen_Futures @patientID

	If (@artstart != '' Or @artstart Is Not Null) Begin
		Select @FirstLineRegimen [FirstLineRegimen]
	End 
	Else Begin
		Select '' [FirstLineRegimen]
	End
--9 First Line ART WHO Stage                             
	If (@ClosestARVDate != '' Or @ClosestARVDate Is Not Null) Begin
		Select Top (1) dtl.WHOStage
		From ord_Visit As ord
		Inner Join
			dtl_PatientStage As dtl On dtl.Visit_Pk = ord.Visit_Id
		Where (ord.Ptn_Pk = @patientID)
		And (dtl.WHOStage Is Not Null)
		And (dtl.WHOStage <> '0')
		Order By Abs(Datediff(Day, ord.VisitDate, @ClosestARVDate));
	End 
	Else Begin
		Select '' [WHOStage]
	End

--10 Transfer in on ART Section                                      
	Select	prev.ARTTransferInDate,
			prev.PrevARVRegimen,
			mst.LPTFTransferId
	From dtl_PatientHivPrevCareIE prev
	Join
		mst_patient mst On prev.Ptn_pk = mst.Ptn_pk
		And prev.LocationId = mst.LocationId
	Where prev.Visit_pk = @Visit_Pk
	And prev.Ptn_pk = @patientID
	And prev.LocationId = @LocationId

--11 ART Start in Another Facility                                      
	Select	art.ptn_pk,
			art.Visit_Id,
			art.LocationId,
			art.FirstLineRegStDate,
			art.Firstlinereg,
			art.cd4,
			art.cd4percent,
			art.pregnant,
			vit.weight,
			vit.Height,
			vit.Muac,
			stg.whostage
	From dtl_patientArtCare art
	Left Outer Join
		dtl_patientvitals vit On art.visit_id = vit.Visit_pk
	Left Outer Join
		dtl_PatientStage stg On art.visit_id = stg.Visit_pk
	Where art.Ptn_pk = @patientID
	And art.locationId = @LocationId;

--12 Subsitution and switches                                       
	Create Table #TemptblRegChng(
		[ID] [int] Identity (1, 1) Not Null,
		[regimentype] [varchar](200), 
		[ChangeDate] [datetime], 
		[RegimenLine] [varchar](50), 
		[ChangeReason] [varchar](50));
		
	Declare @Previousregimen varchar(200);
                  
	Select @Previousregimen = PrevARVRegimen
	From dtl_PatientHivPrevCareIE
	Where Visit_pk = @Visit_Pk
	And Ptn_pk = @patientID
	And LocationID = @LocationId;

	If (Isnull(@Previousregimen, '') = '') Begin
		--In (Select artstartdate	From mst_patient Where ptn_pk = @patientID)
		Select @Previousregimen = a.regimentype
		From dtl_regimenmap a
		Inner Join
			ord_PatientPharmacyOrder b On a.OrderID = b.ptn_pharmacy_pk
		Left Outer Join
			mst_RegimenLine c On c.ID = b.RegimenLine
		Where b.Ptn_pk = @patientID
		And b.DispensedByDate = @artstart  
		And b.ProgID In (222,223)
		Group By a.RegimenType;
	End

-----------------------------------------------------                        
	Declare @regimentype varchar(200),@ChangeDate datetime, @RegimenLine varchar(50),@ChangeReason varchar(50);
	
	Declare curRegChng Cursor For 
	Select	a.regimentype,
			a.ChangeDate,
			a.RegimenLine,
			a.ChangeReason
	From (
		Select	b.dispensedbydate [ChangeDate],
				a.RegimenType,
				dbo.fn_GetPatientRegimenLine_futures(b.dispensedbydate, @patientID) [RegimenLine],
				dbo.fn_GetPatientregChangReason_futures(@patientID, @LocationId, b.DispensedByDate) [ChangeReason]
		From dtl_RegimenMap a
		Inner Join
			ord_PatientPharmacyOrder b On a.OrderID = b.ptn_pharmacy_pk
		Left Outer Join
			mst_RegimenLine c On c.ID = b.RegimenLine
		Where b.ptn_pk = @patientID
		And (b.DeleteFlag Is Null	Or b.DeleteFlag = 0)                
		And b.ProgID in (222,223)
		And a.RegimenType <> '') a
		Order By a.ChangeDate Asc
	Open curRegChng
		Fetch Next From curRegChng 
		Into @regimentype, 
			@ChangeDate, 
			@RegimenLine, 
			@ChangeReason;
		While @@fetch_status = 0 Begin
			If ((@Previousregimen Is Not Null Or @Previousregimen <> '') And (@Previousregimen <> @regimentype)) Begin
				Insert Into #TemptblRegChng (
					[regimentype],
					[ChangeDate],
					[RegimenLine],
					[ChangeReason])
				Values (
					@regimentype,
					@ChangeDate,
					@RegimenLine,
					@ChangeReason )
			End
			Set @Previousregimen = @regimentype
			Fetch Next From curRegChng Into @regimentype, @ChangeDate, @RegimenLine, @ChangeReason
		End
	Close curRegChng
	Deallocate curRegChng
	Select	dbo.fnFormatDate(ChangeDate) [ChangeDate],
			[regimentype],
			[RegimenLine],
			[ChangeReason]
	From #TemptblRegChng;

--13 Interuptions                                       

	Declare @StrCareTerm varchar(50);

	Select @StrCareTerm = dbo.fn_GetPatientDuefortermination_futures(@patientID);

	If (@StrCareTerm = 'Due for Termination') Begin
		Select	dec.Name As StopLost,
				dbo.fnFormatDate(care.ARTenddate) As ARTEndDate,
				Case Charindex('Other', res.name)
					When 0 Then res.name
					Else 'Others: ' + dtl.TherapyOther End As Reason
		From dtl_PatientArvTherapy As dtl
		Inner Join
			ord_Visit As ord On dtl.Visit_pk = ord.Visit_Id
		Inner Join
			mst_BlueDecode As res On res.ID = dtl.TherapyReasonCode
		Inner Join
			mst_Decode As dec On dec.ID = dtl.TherapyPlan
		Inner Join
			dtl_PatientCareEnded As care On care.Ptn_Pk = dtl.ptn_pk
			And care.LocationId = ord.LocationID
			And care.ARTendreason = dtl.TherapyReasonCode
		Where (dtl.ptn_pk = @patientID)
		And (ord.LocationID = @LocationId)
		And (dtl.TherapyPlan = 99)
		And (care.ARTended = 1)
		Union 
		Select	'Lost' [StopLost],
				dbo.fnFormatDate(Getdate()) [ARTEndDate],
				@StrCareTerm [Reason]
	End 
	Else Begin
		Select	D.Name As StopLost,
				dbo.fnFormatDate(care.ARTenddate) As ARTEndDate,
				Case Charindex('Other', res.name)
					When 0 Then res.name
					Else 'Others: ' + dtl.TherapyOther End As Reason
		From dtl_PatientArvTherapy As dtl
		Inner Join
			ord_Visit As ord On dtl.Visit_pk = ord.Visit_Id
		Inner Join
			mst_BlueDecode As res On res.ID = dtl.TherapyReasonCode
		Inner Join
			mst_Decode As D On D.ID = dtl.TherapyPlan
		Inner Join
			dtl_PatientCareEnded As care On care.Ptn_Pk = dtl.ptn_pk
			And care.LocationId = ord.LocationID
			And care.ARTendreason = dtl.TherapyReasonCode
		Where (dtl.ptn_pk = @patientID)
		And (ord.LocationID = @LocationId)
		And (dtl.TherapyPlan = 99)
		And (care.ARTended = 1);
	End

--14 Get ARV Medically Eligible Data
	Select	ptn_pk,
			locationID,
			visit_id,
			userID,
			createDate,
			updateDate,
			dateEligible,
			eligibleThrough,
			WHOStage,
			CD4,
			CD4percent,
			OtherCriteria
	From dtl_PatientARVEligibility
	Where (ptn_pk = @patientID)
	And (locationID = @LocationId);

--15 get DOB of patient
	Select @DOB DOB;

	Drop Table #TemptblRegChng;

--16 Muac                             
	If (@ClosestARVDate Is Not Null) Begin
		Select Top (1) @Muac = dtl.Muac
		From ord_Visit As ord
		Inner Join
			dtl_PatientVitals As dtl On dtl.Visit_pk = ord.Visit_Id
		Where (ord.Ptn_Pk = @patientID)
		And (dtl.Muac Is Not Null)
		Order By Abs(Datediff(Day, ord.VisitDate, @ClosestARVDate));
	End 

	Select @Muac Muac;

End
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_Security_UserLogin_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_Security_UserLogin_Constella]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE procedure [dbo].[Pr_Security_UserLogin_Constella]                                                              
@LoginName varchar(50),                                                              
@LocationId int,                                                              
@SystemId int                                                  
                                                     
as                                                              
                                                            
declare @UsrId int                                                              
declare @Tsql varchar(3000)                                
begin                                           
--0                                                       
	Select	U.UserID
		,	U.UserLastName
		,	U.UserFirstName
		,	U.UserName
		,	U.Password
		,	isnull(U.EmployeeId, 0)				As EmployeeId
		,	E.DesignationID
		,	(
			Select Top (1) Name	From mst_Designation As D	Where (Id = E.DesignationID)
			)									
			As Designation
		,	isnull(E.DeleteFlag,0)						As EmployeeDeleteFlag
		,	isnull(U.UpdateDate, U.CreateDate)	As PwdDate
	From mst_User As U
	Left Outer Join mst_Employee As E On U.EmployeeId = E.EmployeeID
	And U.EmployeeId Is Not Null
	And U.EmployeeId > 0
	Where (U.UserName = @loginname)
		And (U.DeleteFlag = 0)                                                         
                                                              
  select @UsrId = UserId from mst_user where username = @loginname and deleteflag = 0                                                            
 --1                                                       
  
	Select	U.UserID
		,	G.GroupID
		,	G.GroupName
		,	isnull(G.EnrollmentFlag, 0)	As EnrollmentFlag
		,	isnull(G.CareEndFlag, 0)	As CareEndFlag
		,	isnull(G.IdentifierFlag, 0)	
			As IdentifierFlag
		,	F.ModuleId
		,	F.FeatureID
		,	F.FeatureName
		,	FX.FunctionID
		,	FX.FunctionName
		,	F.SystemId
		,	F.ReferenceID
		,	F.FeatureTypeId
		,	(Select Code From mst_Decode D Where D.Id= F.FeatureTypeId)FeatureTypeName
	From mst_User As U
	Inner Join lnk_UserGroup As UG On U.UserID = UG.UserID
	Inner Join mst_Groups As G On UG.GroupID = G.GroupID
	Inner Join lnk_GroupFeatures As GF On G.GroupID = GF.GroupID
	And UG.GroupID = GF.GroupID
	Inner Join mst_Feature As F On GF.FeatureID = F.FeatureID
	Inner Join mst_Function As FX On GF.FunctionID = FX.FunctionID
	Where (U.UserID = @UsrId)
		And (F.SystemId In (@SystemID, 0))
		And F.DeleteFlag = 0
		And F.ModuleId In (Select F.ModuleId	From lnk_FacilityModule	Where FacilityID = @LocationID  Union Select 0) --Order By G.GroupID,F.FeatureID,FX.FunctionID
	UNION
	Select	U.UserID
		,	G.GroupID
		,	G.GroupName
		,	isnull(G.EnrollmentFlag, 0)	As EnrollmentFlag
		,	isnull(G.CareEndFlag, 0)	As CareEndFlag
		,	isnull(G.IdentifierFlag, 0)	
			As IdentifierFlag
		,	F.ModuleId
		,	F.FeatureID
		,	F.FeatureName
		,	FX.FunctionID
		,	FX.FunctionName
		,	F.SystemId
		,	F.ReferenceID
		,	F.FeatureTypeId
		,	(Select Code From mst_Decode D Where D.Id= F.FeatureTypeId)FeatureTypeName
	From mst_User As U
	Inner Join lnk_UserGroup As UG On U.UserID = UG.UserID
	Inner Join mst_Groups As G On UG.GroupID = G.GroupID
	Inner Join lnk_GroupFeatures As GF On G.GroupID = GF.GroupID
	And UG.GroupID = GF.GroupID
	Inner Join (
	Select	FM.FeatureId
		,	FM.ModuleId
		,	F.FeatureName
		,	F.SystemId
		,	F.ReferenceID
		,	F.FeatureTypeId
	From lnk_SplFormModule FM
	Inner Join mst_Feature F On F.FeatureID = FM.FeatureId And F.DeleteFlag= 0
	) As F On GF.FeatureID = F.FeatureID
	Inner Join mst_Function As FX On GF.FunctionID = FX.FunctionID
	Where (U.UserID = @UsrId)
		And (F.SystemId In (@SystemID, 0))
		And F.ModuleId In (Select F.ModuleId	From lnk_facilitymodule	Where FacilityID = @LocationId Union Select 0)  
	Order By G.GroupID,F.FeatureID,FX.FunctionID                                                              
 --2                                                      
	Select	FacilityId
		,	isnull(Paperless, 0)				[Paperless]
		,	FacilityName
		,	CountryId
		,	PosId
		,	SatelliteId
		,	Currency
		,	AppGracePeriod
		,	[DateFormat]
		,	PepFarStartDate
		,	BackupDrive
		,	convert(varchar, BackupTime, 108)	[BackupTime]
		,	SystemId
		,	[Integrated]
	From mst_Facility
	Where deleteflag = 0
		And FacilityId = @LocationId                                         
--3                                          
	Select	Z.FacilityID,
			a.ModuleId,
			a.DisplayName,
			a.CanEnroll,
			a.ModuleName,
			a.PharmacyFlag,
			Z.StrongPassFlag,
			Z.ExpPwdFlag,
			Z.ExpPwdDays
	From
	(
		Select
			a.FacilityID,
			b.ModuleID,
			a.StrongPassFlag,
			a.ExpPwdFlag,
			a.ExpPwdDays
		From mst_Facility a
			Inner Join lnk_FacilityModule b On a.FacilityID = b.FacilityID
	) Z
	Inner Join mst_module a On Z.ModuleID = a.ModuleID
	Where a.Status = 2
	And FacilityID = @LocationId                            
              
--4                                
  Select GetDate()[CurrentDate]    
  
  Select * From mst_User Us                                       
end
  
GO   
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_GetPatientHistory_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_GetPatientHistory_Constella]
GO

CREATE procedure [dbo].[pr_Clinical_GetPatientHistory_Constella]                                                                                                                  
@PatientId int,                                                                                                                  
@Password varchar(40)   = null                                                                                                               
as                                                                                                                  
                                                                                                                  
begin                                                                                             
Declare @SymKey varchar(400)                                                                                  
                                                                                                            
                                                                                                                  
Select Distinct	dbo.fn_PatientIdentificationNumber_Constella(a.ptn_pk, '', 1) [PatientId],
				(CountryId + '-' + PosId + '-' + SatelliteId + '-' + PatientEnrollmentId) [PatientID],
				(convert(varchar(50), decryptbykey(a.firstname)) + ' ' +
				isnull(convert(varchar(50), decryptbykey(a.MiddleName)), '') + ' ' +
				convert(varchar(50), decryptbykey(a.lastName))) Name,
				b.LocationID,
				a.Sex,
				a.PatientClinicID
From mst_patient a, ord_visit b
Where a.ptn_pk = b.ptn_pk
And a.ptn_pk = @patientid
And b.visittype = 12                                                                                                              
                                                                                  
                                                                                                               
Select	'HIV-Enrollment' [FormName],
		a.ptn_pk,
		(convert(varchar(50), decryptbykey(a.FirstName)) + ' ' +
		convert(varchar(50), decryptbykey(a.MiddleName)) + ' ' +
		convert(varchar(50), decryptbykey(a.LastName))) Name,
		isnull(b.VisitDate, '1900-01-01') [TranDate],
		b.DataQuality [DataQuality],
		b.Visit_Id [OrderNo],
		b.LocationID [LocationID],
		'0' [PharmacyNo],
		'1' [Priority],
		'2' [Module],
		'0' [ID],
		'0' [ART],
		'0' [CAUTION]
From mst_patient a, ord_visit b
Where a.ptn_pk = b.ptn_pk
And b.visittype = 0
And a.Ptn_Pk = @PatientId
And a.PatientEnrollmentID <> ''
And (b.deleteflag Is Null Or b.deleteflag = 0)      
                                                                                 
Union All                                             
                                    
Select	'Initial Evaluation' As FormName,
		a.Ptn_Pk,
		convert(varchar(50), decryptbykey(a.FirstName)) + ' ' + convert(varchar(50), decryptbykey(a.MiddleName)) + ' ' + convert(varchar(50), decryptbykey(a.LastName))
		As Name,
		isnull(b.VisitDate, '1900-01-01') As TranDate,
		b.DataQuality,
		b.Visit_Id As OrderNo,
		b.LocationID,
		'0' As PharmacyNo,
		'2' As Priority,
		'2' As Module,
		'0' As ID,
		'0' As ART,
		'0' As CAUTION
From mst_Patient As a
Inner Join ord_Visit As b On a.Ptn_Pk = b.Ptn_Pk
Where (b.VisitType = 1)
And (a.Ptn_Pk = @PatientId)
And (b.DeleteFlag Is Null Or b.DeleteFlag = 0)                                                                                 
                                                                                                        
Union All                
                
Select	'Prior ART/HIV Care' As FormName,
		a.Ptn_Pk,
		convert(varchar(50), decryptbykey(a.FirstName)) + ' ' + convert(varchar(50), decryptbykey(a.MiddleName)) + ' ' + convert(varchar(50),
		decryptbykey(a.LastName)) As Name,
		isnull(b.VisitDate, '1900-01-01') As TranDate,
		b.DataQuality,
		b.Visit_Id As OrderNo,
		b.LocationID,
		'0' As PharmacyNo,
		'2' As Priority,
		'202' As Module,
		'0' As ID,
		'0' As ART,
		'0' As CAUTION
From mst_Patient As a
Inner Join ord_Visit As b On a.Ptn_Pk = b.Ptn_Pk
Where (b.VisitType = 16)
And (a.Ptn_Pk = @PatientId)
And (b.DeleteFlag Is Null Or b.DeleteFlag = 0) 
          
Union All                    
                
Select	'ART Care' As FormName,
		a.Ptn_Pk,
		convert(varchar(50), decryptbykey(a.FirstName)) + ' ' + convert(varchar(50), decryptbykey(a.MiddleName)) + ' ' + convert(varchar(50), decryptbykey(a.LastName))
		As Name,
		isnull(b.VisitDate, '1900-01-01') As TranDate,
		b.DataQuality,
		b.Visit_Id As OrderNo,
		b.LocationID,
		'0' As PharmacyNo,
		'4' As Priority,
		'202' As Module,
		'0' As ID,
		'0' As ART,
		'0' As CAUTION
From mst_Patient As a
Inner Join ord_Visit As b On a.Ptn_Pk = b.Ptn_Pk
Where (b.VisitType = 14)
And (a.Ptn_Pk = @PatientId)
And (b.DeleteFlag Is Null Or b.DeleteFlag = 0)   
    
---john start    
Union All       
Select	'ART Therapy' As FormName,
		a.Ptn_Pk,
		convert(varchar(50), decryptbykey(a.FirstName)) + ' ' + convert(varchar(50), decryptbykey(a.MiddleName)) + ' ' + convert(varchar(50), decryptbykey(a.LastName))
		As Name,
		isnull(b.VisitDate, '1900-01-01') As TranDate,
		b.DataQuality,
		b.Visit_Id As OrderNo,
		b.LocationID,
		'0' As PharmacyNo,
		'4' As Priority,
		'203' As Module,
		'0' As ID,
		'0' As ART,
		'0' As CAUTION
From mst_Patient As a
Inner Join ord_Visit As b On a.Ptn_Pk = b.Ptn_Pk
Where (b.VisitType = 19)
And (a.Ptn_Pk = @PatientId)
And (b.DeleteFlag Is Null Or b.DeleteFlag = 0)                                                                         
--john end                                                                                 
                                                                                                        
Union All           
               
Select	'ART History' As FormName,
		a.Ptn_Pk,
		convert(varchar(50), decryptbykey(a.FirstName)) + ' ' + convert(varchar(50), decryptbykey(a.MiddleName)) + ' ' + convert(varchar(50), decryptbykey(a.LastName))
		As Name,
		isnull(b.VisitDate, '1900-01-01') As TranDate,
		b.DataQuality,
		b.Visit_Id As OrderNo,
		b.LocationID,
		'0' As PharmacyNo,
		'4' As Priority,
		'203' As Module,
		'0' As ID,
		'0' As ART,
		'0' As CAUTION
From mst_Patient As a
Inner Join ord_Visit As b On a.Ptn_Pk = b.Ptn_Pk
Where (b.VisitType = 18)
And (a.Ptn_Pk = @PatientId)
And (b.DeleteFlag Is Null Or b.DeleteFlag = 0)    

Union All       
                                                                                         
                                                                                                         
Select	'Pharmacy' As FormName,
		mst_Patient.Ptn_Pk,
		convert(varchar(50), decryptbykey(mst_Patient.FirstName)) + ' ' + convert(varchar(50), decryptbykey(mst_Patient.MiddleName)) + ' ' + convert(varchar(50),
		decryptbykey(mst_Patient.LastName)) As Name,
		coalesce(DispensedByDate,OrderedByDate,VisitDate) TranDate,
		--Case
		--	When dbo.ord_PatientPharmacyOrder.DispensedByDate Is Null Then dbo.ord_PatientPharmacyOrder.OrderedByDate
		--	Else dbo.ord_PatientPharmacyOrder.DispensedByDate
		--End As TranDate,
		ord_Visit.DataQuality,
		ord_PatientPharmacyOrder.ptn_pharmacy_pk As OrderNo,
		ord_Visit.LocationID,
		'0' As PharmacyNo,
		'5' As Priority,
		'0' As Module,
		mst_Decode.ID,
		mst_Decode.Name As ART,
		Case
			When dbo.ord_PatientPharmacyOrder.DispensedByDate Is Null Then '1'
			Else '0'
		End As CAUTION
From mst_Patient
Inner Join ord_PatientPharmacyOrder On mst_Patient.Ptn_Pk = ord_PatientPharmacyOrder.Ptn_pk
Inner Join ord_Visit On mst_Patient.Ptn_Pk = ord_Visit.Ptn_Pk
		And ord_PatientPharmacyOrder.VisitID = ord_Visit.Visit_Id
Left Outer Join mst_Decode On mst_Decode.ID = ord_PatientPharmacyOrder.ProgID
Where (ord_Visit.VisitType = 4)
And (mst_Patient.Ptn_Pk = @PatientId)
And (ord_Visit.DeleteFlag Is Null Or ord_Visit.DeleteFlag = 0)
And (ord_Visit.VisitDate Is Not Null)
And (ord_PatientPharmacyOrder.OrderType = 116)                                                                                        
                       
Union All  
Select	'Laboratory' [FormName],
		a.ptn_pk,
		(convert(varchar(50), decryptbykey(a.firstname)) + ' ' +
		convert(varchar(50), decryptbykey(a.MiddleName)) + ' ' +
		convert(varchar(50), decryptbykey(a.lastName))) Name,
		isnull(b.OrderDate, '1900-01-01') [TranDate],
		c.DataQuality [DataQuality],
		b.Id [OrderNo],
		c.LocationID [LocationId],
		'0' [PharmacyNo],
		'7' [Priority],
		'0' As Module,
		'0' [ID],
		'0' [ART],
		CAUTION = ( Case b.OrderStatus When 'Pending' Then 1 Else 0 End)
		
From mst_patient a, ord_LabOrder b, ord_Visit c
Where a.ptn_pk = b.ptn_pk
And b.VisitId = c.Visit_Id
And a.ptn_pk = @PatientId
And c.visittype = 6
And (b.deleteflag Is Null Or b.deleteflag = 0)                                                                                         
 
--union                                                                                                              
                                                                                        
--Select	'Laboratory' [FormName],
--		a.ptn_pk,
--		(convert(varchar(50), decryptbykey(a.firstname)) + ' ' +
--		convert(varchar(50), decryptbykey(a.MiddleName)) + ' ' +
--		convert(varchar(50), decryptbykey(a.lastName))) Name,
--		isnull(b.OrderedbyDate, '1900-01-01') [TranDate],
--		c.DataQuality [DataQuality],
--		LabId [OrderNo],
--		c.LocationID [LocationID],
--		'0' [PharmacyNo],
--		'7' [Priority],
--		'0' [Module],
--		'0' [ID],
--		'0' [ART],
--		CAUTION =
--			Case
--				When b.ReportedByDate Is Null Or b.ReportedByDate = '1900-01-01' Then '1'
--				Else '0'
--			End
--From mst_patient a, ord_PatientLabOrder b, ord_Visit c
--Where a.ptn_pk = b.ptn_pk
--And b.VisitId = c.Visit_Id
--And a.ptn_pk = @PatientId
--And c.visittype = 6
--And (b.deleteflag Is Null Or b.deleteflag = 0)                                                                                     
 
union  All                                                                                                               
  Select	'Service Request ' + M.DisplayName As FormName,
		P.Ptn_Pk,
		convert(varchar(50), decryptbykey(P.FirstName)) + ' ' + convert(varchar(50), decryptbykey(P.MiddleName)) + ' ' + convert(varchar(50),
		decryptbykey(P.LastName)) As Name,
		O.OrderDate,
		V.DataQuality,
		O.Id As [OrderNo],
		V.LocationID,
		'0' As PharmacyNo,
		'7' As Priority,
		 0 As Module,
		'0' As ID,
		'0' As ART,
		Case
			When 	O.OrderStatus <> 'Complete' Then '1'
			Else '0'
		End As CAUTION
From mst_Patient As P
Inner Join ord_ClinicalServiceOrder As O On P.Ptn_Pk = O.Ptn_Pk
Inner Join ord_Visit As V On O.VisitId = V.Visit_Id
Inner Join mst_module As M On O.TargetModuleId = M.ModuleID
Where (P.Ptn_Pk = @PatientId)
And O.DeleteFlag = 0                                                                                      
                                                              
                                                                                                              
Union All                                                             
                                                                                                      
Select	'Paediatric Pharmacy' As FormName,
		mst_Patient.Ptn_Pk,
		convert(varchar(50), decryptbykey(mst_Patient.FirstName)) + ' ' + convert(varchar(50), decryptbykey(mst_Patient.MiddleName))
		+ ' ' + convert(varchar(50), decryptbykey(mst_Patient.LastName)) As Name,
		Case
			When dbo.ord_PatientPharmacyOrder.DispensedByDate Is Null Then dbo.ord_PatientPharmacyOrder.OrderedByDate
			Else dbo.ord_PatientPharmacyOrder.DispensedByDate
		End As TranDate,
		ord_Visit.DataQuality,
		ord_PatientPharmacyOrder.ptn_pharmacy_pk As OrderNo,
		ord_Visit.LocationID,
		'0' As PharmacyNo,
		'6' As Priority,
		'0' As Module,
		'0' As ID,
		'0' As ART,
		Case
			When dbo.ord_PatientPharmacyOrder.DispensedByDate Is Null Then '1'
			Else '0'
		End As CAUTION
From mst_Patient
Inner Join ord_PatientPharmacyOrder On mst_Patient.Ptn_Pk = ord_PatientPharmacyOrder.Ptn_pk
Inner Join ord_Visit On mst_Patient.Ptn_Pk = ord_Visit.Ptn_Pk
		And ord_PatientPharmacyOrder.VisitID = ord_Visit.Visit_Id
Where (ord_Visit.VisitType = 4)
And (mst_Patient.Ptn_Pk = @PatientId)
And (ord_Visit.DeleteFlag Is Null Or ord_Visit.DeleteFlag = 0)
And (ord_Visit.VisitDate Is Not Null)
And (ord_PatientPharmacyOrder.OrderType = 117)                                                                                                       
                                                                                                      
Union All                                   
                                                                                             
Select	'ART Follow-Up' As FormName,
		a.Ptn_Pk,
		convert(varchar(50), decryptbykey(a.FirstName, a.Ptn_Pk, convert(varchar(50), a.Ptn_Pk))) + ' ' + convert(varchar(50), decryptbykey(a.MiddleName, a.Ptn_Pk,
		convert(varchar(50), a.Ptn_Pk))) + ' ' + convert(varchar(50), decryptbykey(a.LastName, a.Ptn_Pk, convert(varchar(50), a.Ptn_Pk))) As Name,
		isnull(b.VisitDate, '1900-01-01') As TranDate,
		b.DataQuality,
		b.Visit_Id As OrderNo,
		b.LocationID,
		'0' As PharmacyNo,
		'3' As Priority,
		'2' As Module,
		'0' As ID,
		'0' As ART,
		'0' As CAUTION
From mst_Patient As a
Inner Join ord_Visit As b On a.Ptn_Pk = b.Ptn_Pk
Where (b.VisitType = 2)
And (a.Ptn_Pk = @PatientId)
And (b.DeleteFlag Is Null Or b.DeleteFlag = 0)                                                                                                             
                  
Union All 
                                                                                   
Select	'HIV Care/ART Encounter' As FormName,
		a.Ptn_Pk,
		convert(varchar(50), decryptbykey(a.FirstName, a.Ptn_Pk, convert(varchar(50), a.Ptn_Pk))) + ' ' + convert(varchar(50), decryptbykey(a.MiddleName,
		a.Ptn_Pk, convert(varchar(50), a.Ptn_Pk))) + ' ' + convert(varchar(50), decryptbykey(a.LastName, a.Ptn_Pk, convert(varchar(50), a.Ptn_Pk))) As Name,
		isnull(b.VisitDate, '1900-01-01') As TranDate,
		b.DataQuality,
		b.Visit_Id As OrderNo,
		b.LocationID,
		'0' As PharmacyNo,
		'3' As Priority,
		'202' As Module,
		'0' As ID,
		'0' As ART,
		'0' As CAUTION
From mst_Patient As a
Inner Join ord_Visit As b On a.Ptn_Pk = b.Ptn_Pk
Where (b.VisitType = 15)
And (a.Ptn_Pk = @PatientId)
And (b.DeleteFlag Is Null Or b.DeleteFlag = 0)                                                                                                         
    
Union All        

Select	'Initial and Follow up Visits' As FormName,
		a.Ptn_Pk,
		convert(varchar(50), decryptbykey(a.FirstName, a.Ptn_Pk, convert(varchar(50), a.Ptn_Pk))) + ' ' + convert(varchar(50), decryptbykey(a.MiddleName,
		a.Ptn_Pk, convert(varchar(50), a.Ptn_Pk))) + ' ' + convert(varchar(50), decryptbykey(a.LastName, a.Ptn_Pk, convert(varchar(50), a.Ptn_Pk))) As Name,
		isnull(b.VisitDate, '1900-01-01') As TranDate,
		b.DataQuality,
		b.Visit_Id As OrderNo,
		b.LocationID,
		'0' As PharmacyNo,
		'3' As Priority,
		'203' As Module,
		'0' As ID,
		'0' As ART,
		'0' As CAUTION
From mst_Patient As a
Inner Join ord_Visit As b On a.Ptn_Pk = b.Ptn_Pk
Where (b.VisitType = 17)
And (a.Ptn_Pk = @PatientId)
And (b.DeleteFlag Is Null Or b.DeleteFlag = 0)     
    
Union All                                                                                                           
                                                                                                
Select Distinct	'Non-ART Follow-Up' As FormName,
				a.Ptn_Pk,
				convert(varchar(50), decryptbykey(a.FirstName, a.Ptn_Pk, convert(varchar(50), a.Ptn_Pk))) + ' ' + convert(varchar(50), decryptbykey(a.MiddleName, a.Ptn_Pk,
				convert(varchar(50), a.Ptn_Pk))) + ' ' + convert(varchar(50), decryptbykey(a.LastName, a.Ptn_Pk, convert(varchar(50), a.Ptn_Pk))) As Name,
				isnull(b.VisitDate, '1900-01-01') As TranDate,
				b.DataQuality,
				b.Visit_Id As OrderNo,
				b.LocationID,
				'0' As PharmacyNo,
				'4' As Priority,
				'2' As Module,
				'0' As ID,
				'0' As ART,
				'0' As CAUTION
From mst_Patient As a
Inner Join ord_Visit As b On a.Ptn_Pk = b.Ptn_Pk
Where (b.VisitType = 3)
And (b.Ptn_Pk = @PatientId)
And (b.DeleteFlag Is Null Or b.DeleteFlag = 0)                               
                                                                                                          
Union All                                         
                                                                       
Select	'Patient Record - Initial Visit' As FormName,
		mst_Patient.Ptn_Pk,
		convert(varchar(50), decryptbykey(mst_Patient.FirstName, mst_Patient.Ptn_Pk, convert(varchar(50), mst_Patient.Ptn_Pk)))
		+ ' ' + convert(varchar(50), decryptbykey(mst_Patient.MiddleName, mst_Patient.Ptn_Pk, convert(varchar(50), mst_Patient.Ptn_Pk))) + ' ' + convert(varchar(50), decryptbykey(mst_Patient.LastName,
		mst_Patient.Ptn_Pk, convert(varchar(50), mst_Patient.Ptn_Pk))) As Name,
		isnull(ord_Visit.VisitDate, '1900-01-01') As TranDate,
		ord_Visit.DataQuality,
		ord_Visit.Visit_Id As OrderNo,
		ord_Visit.LocationID,
		'0' As PatientRecordNo,
		'0' As Priority,
		'' As Module,
		'0' As ID,
		'0' As ART,
		'0' As CAUTION
From mst_Patient
Inner Join ord_Visit On mst_Patient.Ptn_Pk = ord_Visit.Ptn_Pk
Where (ord_Visit.VisitType = 7)
And (mst_Patient.Ptn_Pk = @PatientId)                                                                                                        
                                      
Union All        
                                             
Select	'Patient Record - Follow Up' As FormName,
		mst_Patient.Ptn_Pk,
		convert(varchar(50), decryptbykey(mst_Patient.FirstName, mst_Patient.Ptn_Pk, convert(varchar(50), mst_Patient.Ptn_Pk)))
		+ ' ' + convert(varchar(50), decryptbykey(mst_Patient.MiddleName, mst_Patient.Ptn_Pk, convert(varchar(50), mst_Patient.Ptn_Pk))) + ' ' + convert(varchar(50), decryptbykey(mst_Patient.LastName,
		mst_Patient.Ptn_Pk, convert(varchar(50), mst_Patient.Ptn_Pk))) As Name,
		isnull(ord_Visit.VisitDate, '1900-01-01') As TranDate,
		ord_Visit.DataQuality,
		ord_Visit.Visit_Id As OrderNo,
		ord_Visit.LocationID,
		'0' As PatientRecordNo,
		'0' As Priority,
		'' As Module,
		'0' As ID,
		'0' As ART,
		'0' As CAUTION
From mst_Patient
Inner Join ord_Visit On mst_Patient.Ptn_Pk = ord_Visit.Ptn_Pk
Where (ord_Visit.VisitType = 8)
And (mst_Patient.Ptn_Pk = @PatientId)
And (ord_Visit.DeleteFlag Is Null)                                                                                        
                                      
Union All                                                                                           
                                                                          
Select	'Care Tracking' As FormName,
		a.Ptn_Pk,
		convert(varchar(50), decryptbykey(a.FirstName, a.Ptn_Pk, convert(varchar(50), a.Ptn_Pk))) + ' ' + convert(varchar(50), decryptbykey(a.MiddleName, a.Ptn_Pk,
		convert(varchar(50), a.Ptn_Pk))) + ' ' + convert(varchar(50), decryptbykey(a.LastName, a.Ptn_Pk, convert(varchar(50), a.Ptn_Pk))) As Name,
		Case
			When c.Careended = 1 Then isnull(c.CareEndedDate, '')
			When c.ARTended = 1 Then isnull(c.ARTenddate, '')
			Else isnull(b.DateLastContact, '')
		End As TranDate,
		b.DataQuality,
		b.TrackingID As OrderNo,
		c.LocationId As LocationID,
		c.CareEndedID As PharmacyNo,
		'9' As Priority,
		b.ModuleId As Module,
		'0' As ID,
		'0' As ART,
		'0' As CAUTION
From mst_Patient As a
Inner Join dtl_PatientTrackingCare As b On a.Ptn_Pk = b.Ptn_Pk
Inner Join dtl_PatientCareEnded As c On a.Ptn_Pk = c.Ptn_Pk
		And b.TrackingID = c.TrackingId
Where (c.ARTended Is Null Or c.ARTended = 0)
And (a.Ptn_Pk = @PatientId)

Union All                                                                                                            
                                                                                        
Select	'Home Visit' As FormName,
		a.Ptn_Pk,
		convert(varchar(50), decryptbykey(a.FirstName, a.Ptn_Pk, convert(varchar(50), a.Ptn_Pk))) + ' ' + convert(varchar(50), decryptbykey(a.MiddleName, a.Ptn_Pk,
		convert(varchar(50), a.Ptn_Pk))) + ' ' + convert(varchar(50), decryptbykey(a.LastName, a.Ptn_Pk, convert(varchar(50), a.Ptn_Pk))) As Name,
		isnull(b.hvBeginDate, '1900-01-01') As TranDate,
		b.DataQuality,
		b.HomeVisitID As OrderNo,
		b.LocationId As LocationID,
		'0' As PharmacyNo,
		'8' As Priority,
		'2' As Module,
		'0' As ID,
		'0' As ART,
		'0' As CAUTION
From mst_Patient As a
Inner Join dtl_PatientHomeVisit As b On a.Ptn_Pk = b.ptn_pk
Where (a.Ptn_Pk = @PatientId)
And (b.DeleteFlag Is Null)
Or (a.Ptn_Pk = @PatientId)
And (b.DeleteFlag = 0)                                                                          
                                       
Union All                                                  
                                                  
Select	'Patient Registration' As FormName,
		a.Ptn_Pk,
		convert(varchar(50), decryptbykey(a.FirstName)) + ' ' + convert(varchar(50), decryptbykey(a.MiddleName)) + ' ' + convert(varchar(50),
		decryptbykey(a.LastName)) As Name,
		isnull(b.VisitDate, '1900-01-01') As TranDate,
		b.DataQuality,
		b.Visit_Id As OrderNo,
		b.LocationID,
		'0' As PharmacyNo,
		'1' As Priority,
		'0' As Module,
		'0' As ID,
		'0' As ART,
		'0' As CAUTION
From mst_Patient As a
Inner Join ord_Visit As b On a.Ptn_Pk = b.Ptn_Pk
Where (b.VisitType = 12)
And (a.Ptn_Pk = @PatientId)
And (b.DeleteFlag Is Null Or b.DeleteFlag = 0)                                                       
                                          
Union All                                              
                     
Select Distinct	c.VisitName As FormName,
				a.Ptn_Pk,
				convert(varchar(50), decryptbykey(a.FirstName)) + ' ' + convert(varchar(50), decryptbykey(a.MiddleName)) + ' ' + convert(varchar(50), decryptbykey(a.LastName))
				As Name,
				isnull(b.VisitDate, '1900-01-01') As TranDate,
				b.DataQuality,
				b.Visit_Id As OrderNo,
				b.LocationID,
				'0' As PharmacyNo,
				c.VisitTypeID As Priority,
				d.ModuleId As Module,
				'0' As ID,
				'0' As ART,
				'0' As CAUTION
From mst_Patient As a
Inner Join ord_Visit As b On a.Ptn_Pk = b.Ptn_Pk
Inner Join mst_VisitType As c On b.VisitType = c.VisitTypeID
Inner Join mst_Feature As d
Left Outer Join mst_module As e On d.ModuleId = e.ModuleID On c.SystemId = d.SystemId
	And c.VisitName = d.FeatureName
Where (e.DeleteFlag = 0)
And (a.Ptn_Pk = @PatientId)
And (b.DeleteFlag Is Null Or b.DeleteFlag = 0)
And (b.VisitType Not In (0, 1, 2, 3, 4, 5, 6, 7, 8, 11, 12))
And (d.ModuleId Not In (0))
And (d.Published = 2)
And (d.DeleteFlag Is Null Or d.DeleteFlag = 0)
Order By TranDate Desc, FormName Desc                                       
                 
--02                                                                                
select Visit_Id, LocationID from ord_visit where ptn_pk = @patientid and visittype=0                          
                                                                          
--03                                                                                 
--Select FeatureID, FeatureName from mst_feature where Published IN(2) and ModuleId not IN(2)                          
Select FeatureID, FeatureName from mst_feature a left outer join mst_module b on a.ModuleID=b.ModuleId  where Published IN(2) and  b.deleteflag=0 and a.deleteflag=0                                    
                                   
                                                                                                                        
                                                                           
End


GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_CreateLocalCache_AllMasters_constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_CreateLocalCache_AllMasters_constella]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Vincent Yahuma
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[pr_CreateLocalCache_AllMasters_constella] 
	-- Add the parameters for the stored procedure here

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
Set Nocount On;

Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemId
From mst_Provider
Order By SRNO --0                                                                                                                                                       
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemId
From mst_District
Order By SRNO --1                                                                                                                                                    
Select	Id
	,	Name
	,	CategoryId
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemId
	,	Code + ' = ' + Name	[DisplayName]
From mst_Reason
Order By SRNO
--2      
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemID
From mst_Education
Order By SRNO
--3                                                                                                                                                       
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemID
From mst_Designation
Order By SRNO
--4                                                                                                                      
--select Id,Name,SRNO,UpdateFlag,Deleteflag from Mst_Village                                                                                       
Select	EmployeeId
	,	EmployeeId						[Id]
	,	FirstName
	,	LastName
	,	(FirstName + ' ' + LastName)	As [Name]
	,	(FirstName + ' ' + LastName)	As [EmployeeName]
	,	DesignationId
	,	SRNO
	,	UpdateFlag
	,	DeleteFlag
From mst_Employee
Order By SRNO --5                                                                                                                                                      
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemID
From mst_Occupation
Order By SRNO   --6                                                                                                                                                     
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemID
From mst_Province
Order By SRNO
--7                                                                                                                 
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemID
From Mst_Village
Order By SRNO  --8                                                                                                                                                 
Select	CodeId
	,	Name
	,	Deleteflag
From mst_Code --9                                                                                                                                                       
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemID
From mst_HIVAIDSCareTypes
Order By SRNO
--10                                      
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemID
From mst_ARTSponsor
Order By SRNO
--11                                 
Select	Id
	,	Name
	,	SectionId
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemId
From mst_HivDisease
Order By SRNO
--12                                       
Select	AssessmentId
	,	AssessmentCategoryId
	,	AssessmentName
	,	UpdateFlag
	,	DeleteFlag
From mst_Assessment
--13                                                    
Select	Id
	,	Name
	,	CategoryId
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemId
From mst_Symptom
Order By SRNO
--14                                                               
Select	D.ID
	,	D.Name
	,	D.CodeID
	,	D.SRNo
	,	D.UpdateFlag
	,	D.DeleteFlag
	,	D.SystemId
	,	D.Code
	,	D.Code + ' = ' + D.Name	As DisplayName
	,	D.ModuleId
	,	C.Name					ListName
From mst_Decode As D
Inner Join mst_Code C On C.CodeID = D.CodeID
Order By SRNo
--15                                                                
Select	FeatureId
	,	FeatureName
	,	ReportFlag
	,	DeleteFlag
	,	AdminFlag
	,	SystemID
From mst_feature --16                                                                                                               
Select	FunctionId
	,	FunctionName
	,	DeleteFlag
From mst_Function    --17                                                                                                                                                    
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemID
From mst_hivdisclosure
Order By SRNO
--18                                                                                                       
Select	Id
	,	Name
	,	SRNO
	,	Deleteflag
	,	isnull(multiplier, '0')	[multiplier]
From mst_Frequency
Order By SRNO --19                                                                                                                                               
Select	StrengthId
	,	StrengthName
	,	DeleteFlag
From mst_Strength   --20                                                                                                                               
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
From mst_FrequencyUnits
Order By SRNO
--21                                                                                   
Select	Drug_PK
	,	DrugId
	,	DrugName
	,	DeleteFlag
From mst_drug
--22                                                                                            
Select	GenericId
	,	GenericName
	,	GenericAbbrevation
From mst_generic  --23                                                                                                                                                     
Select	DrugTypeId
	,	DrugTypeName
	,	DeleteFlag
From mst_drugtype
--24    
Select	Id									LabTestId
	,	Name								LabName
	,	DepartmentId						LabDepartmentId
	,	0									LabTypeId
	,	row_number() Over (Order By Name)	[Sequence]
	,	DeleteFlag
From mst_LabTestMaster
--select LabTestId,LabName,LabDepartmentId,LabTypeId,Sequence,DeleteFlag from mst_Labtest   --25                                                                                                                                   
--select SubTestId,SubTestName,TestId,DeleteFlag from lnk_testParameter --26  
Select	Id				SubTestId
	,	ParameterName	SubTestName
	,	LabTestId		TestId
	,	DeleteFlag
From Mst_LabTestParameter
--select SubTestID,MinBoundaryValue,MaxBoundaryValue,MinNormalRange,MaxNormalRange,TextNormalRange,  
--DefaultUnit,UnitID,DeleteFlag from lnk_labvalue  --27  

Select	ParameterId	SubTestId
	,	MinBoundary	MinBoundaryValue
	,	MaxBoundary	MaxBoundaryValue
	,	MinNormalRange
	,	MaxNormalRange
	,	''			TextNormalRange
	,	DefaultUnit
	,	UnitId
	,	DeleteFlag
From dtl_LabTestParameterConfig

--select ResultId,ParameterId,Result from lnk_parameterresult
Select	Id		ResultId
	,	ParameterId
	,	Value	Result
From dtl_LabTestParameterResultOption

--28                                                                                                                                      
---Select ID, SatelliteID, Name, SRNO, DeleteFlag FROM mst_Satellite                                            
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemID
From mst_LPTF
Order By SRNO
--29                                                                                            
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemID
From mst_StoppedReason
Order By SRNO
--30                                                                            
Select	FacilityName
	,	FacilityID
	,	PosID
	,	DeleteFlag
	,	SatelliteID
	,	SystemID
	,	[FacilityAddress]
	,	[FacilityTel]
	,	[FacilityCell]
	,	[FacilityFax]
	,	[FacilityEmail]
	,	[FacilityURL]
	,	[FacilityFooter]
	,	isnull(FacilityTemplate, 0)	[FacilityTemplate]
	,	[FacilityLogo]
	,	[StrongPassFlag]
From mst_facility
--31                                                                               
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemID
From mst_HivCareStatus
Order By SRNO
--32                                         
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemID
From mst_RelationshipType
Order By SRNO
--33                                                                                        
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemID
From mst_Ward
Order By SRNO
--34                                                                
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemID
From mst_Division
Order By SRNO
--35                                                    
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	Code + ' = ' + Name	[DisplayName]
From mst_TBStatus
Order By SRNO
--36                                                                                                                 
Select	Id
	,	Name
	,	Code
	,	UpdateFlag
	,	Deleteflag
	,	Code + ' = ' + Name	[DisplayName]
From mst_ARVStatus
--37                                                                            
Select	Id
	,	Name
	,	SRNo
	,	UpdateFlag
	,	Deleteflag
	,	SystemID
From mst_CouncellingType
Order By SRNO --38                                                                                                        
Select	Id
	,	Name
	,	SRNo
	,	UpdateFlag
	,	Deleteflag
	,	SystemID
From mst_CouncellingTopic
Order By SRNO  --39                                                                                                   
Select	Id
	,	Name
	,	SRNO
	,	UpdateFlag
	,	Deleteflag
	,	SystemID
From mst_LostFollowreason
Order By SRNO
--40                                                                                           
Select	regimenID							[Id]
	,	RegimenCode + ' - ' + RegimenName	[Name]
	,	RegimenCode
	,	Stage
	,	SRNo
	,	DeleteFlag
From mst_regimen
Where regimenCode Is Not Null --41                                                                                   

--select a.SubTestId,a.SubTestName,a.TestId,a.DeleteFlag from lnk_TestParameter a,mst_Labtest b                                                                                
--where b.labTestId = a.TestId and a.DeleteFlag=0 --- 42   

Select	P.Id			SubTestId
	,	P.ParameterName	SubTestName
	,	P.LabTestId		TestId
	,	m.DeleteFlag
From Mst_LabTestParameter P
Inner Join mst_LabTestMaster M On M.Id = P.LabTestId
Where M.DeleteFlag = 0

Select	Id
	,	Name
	,	SRNo
	,	UpdateFlag
	,	DeleteFlag
	,	SystemID
From mst_ReferredFrom
Order By SRNO--- 43                                                                        

Select	Id
	,	Name
	,	SRNo
	,	UpdateFlag
	,	DeleteFlag
	,	SystemID
From mst_PatientLabPeriod
Order By SRNO --44                                                    

Select	Id
	,	Name
	,	CodeID
	,	SRNo
	,	UpdateFlag
	,	DeleteFlag
	,	SystemID
	,	Code
From Mst_pmtctDeCode
Where (deleteFlag = 0 Or deleteFlag Is Null)
Order By SRNO--45                                

Select	ModuleID
	,	ModuleName
	,	DeleteFlag
From mst_module
Where DeleteFlag = 0;--46                                                          

--47                                                          
Select	b.ID
	,	b.Name
	,	a.Predefined
	,	b.CodeID
	,	b.SRNo
	,	b.UpdateFlag
	,	b.DeleteFlag
	,	b.SystemID
	,	b.Code
From mst_modcode a
Inner Join mst_moddecode b On a.CodeId = b.CodeId
Where (b.deleteFlag = 0 Or b.deleteFlag Is Null)
	And (a.deleteFlag = 0 Or a.deleteFlag Is Null)

Select *
From Mst_ARVSideEffects
Where DeleteFlag = 0;--48                                                    

Select *
From Mst_ModCode
Where DeleteFlag = 0;--49                  

Select *
From Mst_DrugSchedule
Where DeleteFlag = 0;--50                                                    
Select *
From Mst_Store---51                                          
Select *
From Mst_Supplier -- 52                                         
Select *
From mst_Donor -- 53                                          
Select *
From Mst_Program -- 54                                       
Select *
From Mst_Batch --55                                    
Select *
From Mst_Country --56                                    
Select *
From Mst_Town--57                                
--58                             
Select	row_number() Over (Order By ID Asc)	As ID
	,	ID									[DiseaseID]
	,	Name
	,	Name1
	,	SRNO
	,	DeleteFlag
	,	SystemId
	,	DiseaseFlag
	,	ModuleId
	,	Name1
From dbo.VWDiseaseSymptom
Order By Name Asc

--59                          
Select	Case
			When Predefined = 0 Then convert(int, '8888' + convert(varchar, FieldId))
			When Predefined = 1 Then convert(int, '9999' + convert(varchar, FieldId))
		End																																											[FieldId]
	,	BlockId
	,	SubBlockId
	,	Id
	,	+'%' + convert(varchar, isnull(BlockId, 0)) + '%' + convert(varchar, isnull(SubBlockId, 0)) + '%' + convert(varchar, isnull(Id, 0)) + '%' + convert(varchar, Predefined)	[CodeId]
	,	BindField																																									[Name]
	,	isnull(DeleteFlag, 0)																																						[DeleteFlag]
From dbo.VW_ICDList

Select *
From mst_RegimenLine --60     
--Query builder reports - jnjung''e
Select	C.CategoryId
	,	C.CategoryName
	,	R.ReportId
	,	R.ReportName
	,	isnull(R.qryDescription, R.ReportName)	ReportDescription
From dbo.Mst_QueryBuilderCategory C
Inner Join dbo.mst_QueryBuilderReports R On R.CategoryId = C.CategoryId
Where isnull(R.DeleteFlag, 0) = 0
	And isnull(C.DeleteFlag, 0) = 0; --61    
--Select U.UserId,U.UserLastName, U.UserFirstName, (U.UserFirstName + ' ' +U.UserLastName) As Names, U.EmployeeID,U.DeleteFlag  from mst_User U -- 62
Select	UserId
	,	SystemUserName
	,	Name
	,	EmployeeName
	,	UserDeleteFlag
	,	EmployeeId
	,	DesignationId
	,	Designation
	,	EmployeeDeleteFlag
From vw_UserList
Order By Name -- 62
End

GO


/****** Object:  StoredProcedure [dbo].[pr_Clinical_GetInitialFollowupVisitInfo]    Script Date: 09/28/2015 16:08:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[pr_Clinical_GetInitialFollowupVisitInfo]    (            
 @patientID int,                
 @visitID int,                
 @locationID int                
 )
AS                
 Begin
--0 Visit Date &  & CreateDate & DataQuality                
Select	VisitDate As 'visitDate',
		CreateDate As 'createDate',
		DataQuality As 'dataQuality',
		typeofvisit
From ord_visit
Where Visit_Id = @visitID
And Ptn_Pk = @patientID
And LocationID = @locationID


Select	Scheduled,
		SupporterName As 'treatmentSupporterName',
		TreatmentSupporterContact As 'treatmentSupporterContact',
		TBRegistration As 'TBRegNumber',
		NutritionalProblem As 'nutritionalProblem',
		AttendingClinician As 'attendingClinician',
		NutritionalProblem As 'nutritionalProblem'
From dtl_PatientARTEncounter
Where Ptn_pk = @patientID
And Visit_Id = @visitID
And LocationID = @locationID


--2 Follow Up Date                
Select AppDate As 'Dateofnextappointment'
From dtl_PatientAppointment
Where Visit_pk = @visitID
And Ptn_Pk = @patientID
And LocationID = @locationID
And AppReason = 110
And AppStatus = 12

--3 Height Weight Oedema                
Select	Height As 'height',
		Weight As 'weight',
		Temp,
		BPDiastolic,
		BPSystolic,
		Muac
From dtl_PatientVitals
Where Visit_pk = @visitID
And Ptn_Pk = @patientID
And LocationID = @locationID

--4 Pregnancy & EDD & DateOfDelivery & PMTCT & MUAC                 
Select	Pregnant As 'pregnant',
		EDD As 'EDD',
		DateofDelivery As 'deliveryDate',
		PMTCT As 'ReferredtoPMTCT',
		DateofMiscarriage,
		DateofInducedAbortion
From dtl_PatientClinicalStatus
Where Visit_pk = @visitID
And Ptn_Pk = @patientID
And LocationID = @locationID

--5 Gestation                
Select GestAge As 'gestation'
From dtl_PatientDelivery
Where Visit_pk = @visitID
And Ptn_Pk = @patientID
And LocationID = @locationID

--6 PMTCTANCNumber                
Select	ANCNumber As 'PMTCTANCNumber',
		DOB
From mst_patient
Where Ptn_pk = @patientID

--7 FamilyPlanning & Number of Days Hospitalized & Nutritional Support                
Select	FamilyPlanningStatus As 'familyPlanningStatus',
		HospitalizedNumberofDays As 'numOfDaysHospitalized',
		NutritionalSupport As 'nutritionalSupport',
		NoFamilyPlanning
From dtl_patientCounseling
Where Ptn_pk = @patientID
And Visit_pk = @visitID
And LocationID = @locationID

--8 TB Status                    
Select	TBStatus As 'TBStatus',
		TBRxStartDate As 'TBRxStart',
		TBRxEndDate As 'TBRxStop'
From dtl_PatientOtherTreatment
Where Ptn_pk = @patientID
And Visit_pk = @visitID
And LocationID = @locationID


--9 TB Reg Number & New OIs Problem & Nutritional Problem                
Select	Disease_Pk As 'newOIsProblemID',
		DiseaseDesc As 'newOIsProblemOther'
From dtl_PatientDisease
Where Ptn_pk = @patientID
And Visit_pk = @visitID
And LocationID = @locationID

--10 Potential Side Effects                
Select	SymptomID As 'potentialSideEffectID',
		SymptomDescription As 'potentialSideEffectOther'
From dtl_PatientSymptoms
Where Ptn_pk = @patientID
And Visit_pk = @visitID
And LocationID = @locationID

--11 WAB Stage & WHO Stage                
Select	WABStage As 'WABStage',
		WHOStage As 'WHOStage'
From dtl_PatientStage
Where Ptn_pk = @patientID
And Visit_pk = @visitID
And LocationID = @locationID

--12 CPTAdhere                
Select CotrimoxazoleAdhere As 'CPTAdhere'
From dtl_Adherence_Reason
Where Ptn_pk = @patientID
And Visit_pk = @visitID
And LocationID = @locationID

--13 ARV Drugs Adhere + Reason                
Select	ARVAdhere As 'ARVDrugsAdhere',
		AdherenceReason As 'reasonARVDrugsPoorFair',
		AdherenceReasonOther As 'reasonARVDrugsPoorFairOther'
From dtl_PatientAdherence
Where Ptn_pk = @patientID
And Visit_pk = @visitID
And LocationID = @locationID

--14 ReferredTo + Other                  
Select	PatientRefID As 'referredTo',
		PatientRefDesc As 'referredToOther'
From dtl_PatientReferredTo
Where Ptn_pk = @patientID
And Visit_pk = @visitID
And LocationID = @locationID
--15 Infant Feeding Option                
Select FeedingOption As 'infantFeedingOption'
From dtl_InfantInfo
Where Ptn_pk = @patientID
And Visit_pk = @visitID
And LocationID = @locationID

--16             
Select *
From dtl_PatientARVTherapy
Where Ptn_pk = @patientID
And Visit_pk = @visitID
And LocationID = @locationID


--17 Family Planning Method          
Select FamilyPlanningMethod As 'familyPlanningMethodID'
From Dtl_PatientFamilyPlanning
Where Ptn_pk = @patientID
And Visit_Id = @visitID
And LocationID = @locationID

--18 --  at risk population        
Select *
From dbo.dtl_PatientAtRiskPopulation
Where Ptn_pk = @patientID
And Visit_pk = @visitID
And LocationID = @locationID
--19  at risk population services    

Select *
From dbo.dtl_PatientAtRiskPopulationServices
Where Ptn_pk = @patientID
And Visit_pk = @visitID
And LocationID = @locationID
--20  pwp    
Select *
From dbo.dtl_PatientPreventionwithpositives
Where Ptn_pk = @patientID
And Visit_pk = @visitID
And LocationID = @locationID
End

Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_SaveInitialFollowupVisit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_SaveInitialFollowupVisit]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

							   
CREATE PROCEDURE [dbo].[pr_Clinical_SaveInitialFollowupVisit]    
(                             
 @patientID int,                
 @UserId int,                            
 @locationID int,                                
 @dataQuality int,   
 @ModuleId int =null,            
 @visitDate datetime=null,              
 @TypeofVisit int=null,             
 @Scheduled int=null ,                             
 @treatmentSupporterName varchar(50)=null,                                
 @treatmentSupporterContact varchar(50)=null, 
 @Temp varchar(5)=null,                              
 @height varchar(5)=null,                                
 @weight varchar(5)=null,  
 @Muac varchar(5) = null,                              
 @BPSystolic varchar(6) =null,              
 @BPDiastolic varchar(6) =null,              
 @pregnant int=null,                                
 @EDD datetime=null,                                
 @ANCNo int=null,                   
 @ReferredtoPMTCT varchar(100)=null,                                
 @DateofInducedAbortion varchar(15)=null,                                  
 @DateofMiscarriage varchar(15)=null,                           
 @familyPlanningStatus int=null,            
 @NoFamilyPlanning int=null,                              
 @TBStatus int=null,                                
 @TBRxStart datetime=null,                                
 @TBRegNumber varchar(50)=null,                            
 @nutritionalProblem   int=null,             
 @WHOStage int,             
 @CotrimoxazoleAdhere int=null,                                
 @ARVDrugsAdhere int=null,                                
 @WhyPooFair int=null,                                
 @reasonARVDrugsPoorFairOther varchar(50)=null,              
 @TherapyPlan int=null,                          
 @TherapyReasonCode int=null,                          
 @TherapyOther varchar(100)=null,                          
 @PrescribedARVStartDate datetime =null,                          
 @numOfDaysHospitalized varchar(5)=null,                                
 @nutritionalSupport int=null,                                
 @infantFeedingOption int=null,                                
 @attendingClinician int=null,              
 @Datenextappointment datetime=null  
 )                    
AS

Begin
Set Nocount On;

	Declare @visitID int, @currentDate datetime;
	Set @currentDate = Getdate()
	Declare @updateDate datetime,
	@SafeHeight decimal(7, 1),
	@SafeWeight decimal(7, 1),
	@SafeTemp decimal(7, 1),
	@SafeBPSystolic decimal(7, 0),
	@SafeBPDiastolic decimal(7, 0),
	@SafeMuac decimal(7, 0);
	Set @updateDate = Getdate();
	--Select Isnumeric(nullif(@Height,''));
	
	Begin Transaction InitSave
	Begin Try
	
		Select	@SafeHeight =
					Case
						When Isnumeric(nullif(@Height,'')) = 1 Then Convert(decimal(7, 2), @Height)
						Else Null End,
				@SafeWeight =
					Case
						When Isnumeric(nullif(@Weight,'')) = 1 Then Convert(decimal(7, 2), @Weight)
						Else Null End,
				@SafeTemp =
					Case
						When Isnumeric(nullif(@Temp,'')) = 1 Then Convert(decimal(7, 1), @Temp)
						Else Null End,
				@SafeBPSystolic =
					Case
						When Isnumeric(nullif(@BPSystolic,'')) = 1 Then Convert(decimal(7, 0), @BPSystolic)
						Else Null End,
				@SafeBPDiastolic =
					Case
						When Isnumeric(nullif(@BPDiastolic,'')) = 1 Then Convert(decimal(7, 0), @BPDiastolic)
						Else Null End,
				@SafeMuac =
					Case
						When Isnumeric(nullif(@Muac,'')) = 1 Then Convert(decimal(7, 1), @Muac)
						Else Null End;

		Select @ModuleId = Isnull(@ModuleId, 203);
		--Select @SafeBPDiastolic BPD, @SafeBPSystolic BPSys, @SafeHeight Hgt, @SafeWeight Wgt, @SafeMuac Muac, @SafeTemp Tmp
		--Appointment Schduling Visit Date & Attending Clinician & DataQuality                              
		Insert Into ord_visit (
			Ptn_Pk,
			LocationID,
			VisitDate,
			VisitType,
			DataQuality,
			CreateDate,
			UserID,
			TypeofVisit,
			ModuleID)
		Values (
			@patientID,
			@locationID,
			@visitDate,
			17,
			@dataQuality,
			@currentDate,
			@UserId,
			@TypeofVisit,
			@ModuleId );

		Select @visitID = Cast(Scope_identity() As int);

		Insert Into dtl_PatientARTEncounter (
				Ptn_Pk,
				Visit_Id,
				LocationId,
				Scheduled,
				SupporterName,
				TreatmentSupporterContact,
				TBRegistration,
				NutritionalProblem,
				AttendingClinician,
				UserId,
				CreateDate)
			Values (
				@patientID,
				@visitID,
				@locationID,
				Nullif(@Scheduled, 0),
				Nullif(@treatmentSupporterName, ''),
				Nullif(@treatmentSupporterContact, ''),
				Nullif(@TBRegNumber, ''),
				Nullif(@nutritionalProblem, ''),
				@attendingClinician,
				@UserId,
				@currentDate );
				
			                         
		If (@Datenextappointment Is Not Null) Begin

				Insert Into dtl_PatientAppointment (
					Ptn_pk,
					LocationID,
					Visit_pk,
					AppDate,
					CreateDate,
					AppReason,
					AppStatus,
					EmployeeID,
					UserID,
					ModuleID)
				Values (
					@patientID,
					@locationID,
					@visitID,
					@Datenextappointment,
					@currentDate,
					110,
					12,
					@UserID,
					@UserID,
					@ModuleId )
						
		End
--Clinical Status                                   
--If (@height <> '' Or @weight <> '' Or @BPSystolic <> 0 Or @BPDiastolic <> '' Or @Temp <> '') Begin
			Insert Into dtl_PatientVitals (
				Ptn_pk,
				LocationID,
				Visit_pk,
				Height,
				Weight,
				Muac,
				BPDiastolic,
				BPSystolic,
				CreateDate,
				UserID,
				Temp)
			Values (
				@patientID,
				@locationID,
				@visitID,
				@SafeHeight,
				@SafeWeight,
				@SafeMuac,
				Nullif(@SafeBPDiastolic, 0),
				Nullif(@SafeBPSystolic, 0),
				@currentDate,
				@UserID,
				@SafeTemp
				)
		
--End
	
	If (@pregnant <> -1) Begin
		Insert Into dtl_PatientClinicalStatus (
			Ptn_pk,
			LocationID,
			Visit_pk,
			Pregnant,
			EDD,
			DateofMiscarriage,
			DateofInducedAbortion,
			PMTCT,
			CreateDate,
			UserID)
		Values (
			@patientID,
			@locationID,
			@visitID,
			Nullif(@pregnant, -1),
			Nullif(@EDD, ''),
			Nullif(@DateofMiscarriage, ''),
			Nullif(@DateofInducedAbortion, ''),
			@ReferredtoPMTCT,
			@currentDate,
			@UserID )
	End

--familyPlanningStatus,   Number of Days Hospitalized & Nutritional Support                       

	If (@familyPlanningStatus <> '' Or @numOfDaysHospitalized <> '' Or @nutritionalSupport = '' Or @NoFamilyPlanning = '') Begin
		Insert Into dtl_patientCounseling (
			Ptn_pk,
			LocationID,
			Visit_pk,
			FamilyPlanningStatus,
			HospitalizedNumberofDays,
			NutritionalSupport,
			CreateDate,
			NoFamilyPlanning)
		Values (
			@patientID,
			@locationID,
			@visitID,
			Nullif(@familyPlanningStatus, ''),
			Nullif(@numOfDaysHospitalized, ''),
			Nullif(@nutritionalSupport, 0),
			Getdate(),
			Nullif(@NoFamilyPlanning, 0) );
	End


If (@ReferredtoPMTCT = 1 And @ANCNo <> '' And @pregnant = 89) Begin
If (Exists (Select ANCNumber
	From mst_patient
	Where ptn_pk = @patientID
	And (ANCNumber Is Null
	Or ANCNumber = ''))
) Begin
Update mst_patient Set
	ANCNumber = @ANCNo
Where Ptn_pk = @patientID
End
End

--                        
If (@TBStatus <> 0) Begin
If (@TBRxStart <> '') Begin
Set @TBRxStart = @TBRxStart;
End

Insert Into dtl_PatientOtherTreatment (
	Ptn_pk,
	Visit_pk,
	LocationID,
	TBStatus,
	TBRxStartDate,
	CreateDate,
	UserID)
Values (
	@patientID,
	@visitID,
	@locationID,
	@TBStatus,
	Nullif(@TBRxStart, ''),
	@currentDate,
	@UserID )
End

--Subsitutions/Interruption                          


Insert Into dtl_PatientARVTherapy (
	ptn_pk,
	Visit_pk,
	LocationID,
	TherapyPlan,
	TherapyReasonCode,
	TherapyOther,
	CreateDate,
	UserID,
	PrescribedARVStartDate)
Values (
	@patientID,
	@visitID,
	@locationID,
	@TherapyPlan,
	@TherapyReasonCode,
	@TherapyOther,
	Getdate(),
	@UserID,
	Nullif(@PrescribedARVStartDate, '') )
-- PrescribedARVStartDate nullif(@PrescribedARVStartDate,'')                
If (@TherapyPlan = 99) Begin
Declare @TrackingID int
Insert dtl_PatientTrackingCare (
	Ptn_Pk,
	DateLastContact,
	EmployeeID,
	UserID,
	CreateDate,
	LocationId,
	ModuleId)
Values (
	@patientID,
	@PrescribedARVStartDate,
	@attendingClinician,
	@UserID,
	@currentDate,
	@locationID,
	203 )
Select @TrackingID = SCOPE_IDENTITY();

Insert dtl_patientcareended (
	ptn_pk,
	ARTended,
	ARTenddate,
	ARTendreason,
	CreateDate,
	LocationId,
	TrackingId)
Values (
	@patientID,
	1,
	@PrescribedARVStartDate,
	@TherapyReasonCode,
	@currentDate,
	@locationID,
	@TrackingID )
End
------------------------------Naveen-22-Sep-2010 Update ART Restart Begin-----------------------------------------------------                      


If (@TherapyPlan = 96) Begin
Insert Into dtl_PatientARTRestart (
	Ptn_Pk,
	LocationId,
	Visit_Pk,
	RestartDate,
	UserID,
	CreateDate)
Values (
	@patientID,
	@locationID,
	@visitID,
	@visitDate,
	@UserID,
	Getdate() )
End

-------------------------------                                 

--WAB Stage & WHO Stage                                
If (@WHOStage <> 0) Begin
Insert Into dtl_PatientStage (
	Ptn_pk,
	Visit_Pk,
	LocationID,
	WHOStage,
	CreateDate,
	UserID)
Values (
	@patientID,
	@visitID,
	@locationID,
	Nullif(@WHOStage, 0),
	@currentDate,
	@UserID )
End

--CPT Adhere                                
If (@CotrimoxazoleAdhere <> 0) Begin
Insert Into dtl_Adherence_Reason (
	Ptn_pk,
	Visit_pk,
	LocationID,
	CotrimoxazoleAdhere,
	CreateDate,
	UserID)
Values (
	@patientID,
	@visitID,
	@locationID,
	@CotrimoxazoleAdhere,
	@currentDate,
	@UserID )
End

--ARV Drugs Adhere + Reason                              
If (@ARVDrugsAdhere <> 0) Begin
Insert Into dtl_PatientAdherence (
	Ptn_pk,
	Visit_pk,
	LocationID,
	ARVAdhere,
	AdherenceReason,
	AdherenceReasonOther,
	CreateDate,
	UserID)
Values (
	@patientID,
	@visitID,
	@locationID,
	Nullif(@ARVDrugsAdhere, 0),
	Nullif(@WhyPooFair, 0),
	Nullif(@reasonARVDrugsPoorFairOther, ''),
	@currentDate,
	@UserID )
End


--Infant Feeding Option                                
If (@infantFeedingOption <> 0) Begin
Insert Into dtl_InfantInfo (
	Ptn_pk,
	LocationID,
	Visit_pk,
	FeedingOption,
	CreateDate,
	UserID)
Values (
	@patientID,
	@locationID,
	@visitID,
	@infantFeedingOption,
	@currentDate,
	@UserID )
End

Select	Ptn_Pk,
		VisitDate,
		Visit_Id As 'visitID',
		LocationID
From ord_visit
Where Visit_Id = @visitID;

	IF @@TRANCOUNT > 0
			Commit Transaction InitSave;
		End Try
		Begin Catch

		Declare @ErrorMessage NVARCHAR(4000),@ErrorSeverity Int,@ErrorState Int;

		Select	@ErrorMessage = Error_message(),@ErrorSeverity = Error_severity(),@ErrorState = Error_state();

  
		Raiserror (@ErrorMessage, @ErrorSeverity, @ErrorState  );

		IF @@TRANCOUNT > 0 Rollback Transaction InitSave;
	End Catch;
End
				 

GO
					  

/****** Object:  StoredProcedure [dbo].[Pr_Clinical_SaveFollowupEducation_Constella]    Script Date: 01/20/2016 11:13:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_Clinical_SaveFollowupEducation_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_Clinical_SaveFollowupEducation_Constella]
GO

/****** Object:  StoredProcedure [dbo].[Pr_Clinical_SaveFollowupEducation_Constella]    Script Date: 01/20/2016 11:13:32 ******/
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
	@DeleteFlag int=null              
              
)                   
                                 
as                                    
Begin
                               
 If (@Id=-1)  Begin
	declare @VisitId int;

	Insert Into ord_visit (
		Ptn_Pk,
		VisitDate,
		LocationID,
		VisitType,
		DeleteFlag,
		UserID,
		CreateDate)
	Values (
		@Ptn_Pk,
		@VisitDate,
		@LocationID,
		10,
		@DeleteFlag,
		@UserId,
		getdate() );

	Select @VisitId = scope_identity();

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

End
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pr_Clinical_GetModuleFieldNames_COnstella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Pr_Clinical_GetModuleFieldNames_COnstella]
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
Select @HIVStartDate = StartDate From lnk_patientprogramstart Where ptn_pk = @patientid And ModuleId = 2
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

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_GetTechnicalAreaandFormName_Future]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_GetTechnicalAreaandFormName_Future]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[pr_Clinical_GetTechnicalAreaandFormName_Future]                              
(                                                                
   @ModuleId int                                                             
)                                                                
AS                                     
BEGIN                                    
		Select	 ModuleId
				,ModuleName
				,DisplayName
				,CanEnroll
		From mst_Module
		Where ModuleId = @ModuleId
		And DeleteFlag = 0
		Order By ModuleName                         
                          
	SELECT	FeatureID
		,	(CASE
				WHEN substring(tbl1.FeatureName, 1, 7) = 'CareEnd' THEN 'Care Termination'
				ELSE tbl1.FeatureName
			END)	AS FeatureName
		,	Seq
		,	ReferenceID
	FROM mst_Feature AS tbl1
	WHERE (ModuleId = @ModuleId)
		AND (FeatureID > 1000)
		AND (DeleteFlag = 0)
		AND (Published = 2)
		AND (FeatureName NOT LIKE '%Home%')         
	union        
	SELECT	tbl1.FeatureID
		,	tbl1.FeatureName
		,	tbl1.Seq
		,	tbl1.ReferenceId
	FROM mst_Feature AS tbl1
	INNER JOIN lnk_SplFormModule AS SplForm ON tbl1.FeatureID = SplForm.FeatureId
	WHERE (SplForm.ModuleId = @ModuleId)
		AND (tbl1.DeleteFlag = 0 OR tbl1.DeleteFlag IS NOT NULL AND tbl1.DeleteFlag <> 1)
	ORDER BY Seq, FeatureName                     

	SELECT	module.ModuleID
		,	module.ModuleName
		,	module.DisplayName
		,	module.CanEnroll
	FROM mst_module AS module
	INNER JOIN lnk_FacilityModule AS lnk ON module.ModuleID = lnk.ModuleID
	WHERE (module.DeleteFlag = 0)
		AND (module.Status = 1)
	ORDER BY module.ModuleName;

	Select	Id
			,FeatureID
			,BusRuleId
			,Value
			,UserId
			,CreateDate
			,UpdateDate
			,Value1
			,SetType
	From lnk_featureBusinessRule
                                      
End
	
GO	

/****** Object:  StoredProcedure [dbo].[pr_Clinical_GetChildDetail_Futures]    Script Date: 5/12/2016 5:04:00 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_GetChildDetail_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_GetChildDetail_Futures]
GO

/****** Object:  StoredProcedure [dbo].[pr_Clinical_GetChildDetail_Futures]    Script Date: 5/12/2016 5:04:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[pr_Clinical_GetChildDetail_Futures]
	@PatientId int,
	@LocationId int,
	@password varchar(40) = null
AS
BEGIN
SET NOCOUNT ON;

Select	convert(varchar(50), decryptbykey(FirstName))	As FirstName
	,	convert(varchar(50), decryptbykey(MiddleName))	As MiddleName
	,	convert(varchar(50), decryptbykey(LastName))	As LastName
	,	(
		Select Name
		From mst_Decode
		Where (ID In (mst_Patient.Sex))
		)												
		As Sex
	,	convert(nvarchar(20), DOB, 106)					As DOB
	,	convert(nvarchar(20), RegistrationDate, 106)	As RegistrationDate
	,	AdmissionNumber	
	,	Ptn_Pk											As Id
	,	HEIIDNumber
From mst_Patient
Where (Ptn_Pk In (
	Select Ptn_pk	From dtl_InfantParent	Where (ParentPtnPk = @PatientId)
		And (LocationId = @LocationId)
		And (DeleteFlag = 0 Or DeleteFlag Is Null)
	)
	)
	And (DeleteFlag = 0 Or DeleteFlag Is Null)

--	select Nullif(VillageName,'')[VillageName],Nullif(DistrictName,'')[DistrictName],Nullif(Address,'')[Address],Nullif(Phone,'')[Phone] 
--	from mst_patient where ptn_pk=@PatientId  and (deleteflag=0 or deleteflag is NULL)
Select count(Ptn_pk) As Total_Child
From dtl_InfantParent
Where (ParentPtnPk = @PatientId)
	And (LocationId = @LocationId)
	And (DeleteFlag = 0 Or DeleteFlag Is Null)

END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_GetCustomFormFieldLabel_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Clinical_GetCustomFormFieldLabel_Constella]
Go
CREATE Procedure [dbo].[pr_Clinical_GetCustomFormFieldLabel_Constella]                                                                                                                                                                             
	@FeatureId int,                                                                                                                                                                                  
	@PatientId int,                                                                                                                                                                                  
	@Password varchar(40) = null                                                                                                                                                                                   
as                                                                                                                                                                                  
Begin                                                                                                                                                                                  
Declare @SymKey varchar(400)                                                                                                            
Declare @FormVisitType int                                                                                                            
Declare @VisitTypeId int                                                                                                              
Declare @FeatureName varchar(100)                                                                                                                                             
--Table 0                                                                                                                                                                                  
	Select	dbo.fn_PatientIdentificationNumber_Constella(a.Ptn_Pk, '', 1) As PatientIdentification
			,convert(varchar(50), decryptbykey(a.FirstName)) + ' ' + isnull(convert(varchar(50), decryptbykey(a.MiddleName)), '')
			+ ' ' + convert(varchar(50), decryptbykey(a.LastName)) As Name
			,a.PatientClinicID
			,a.DOB
			,a.LocationID
	From mst_Patient As a
	Inner Join ord_Visit As b On a.Ptn_Pk = b.Ptn_Pk
	Where (b.VisitType = 12)
	And (a.Ptn_Pk = @patientid)           
              
----Table 1                                               
  Select Distinct FV.FeatureId
	, FV.FeatureName
	, FV.FormDescription
	, FV.FormId
	, FV.SectionId
	, FV.SectionName
	, FV.SectionInfo
	, FV.PaddedFieldId FieldId
	, FieldName = Case When FieldID = 71 And FV.Predefined = 1 Then 'PlaceHolder' + convert(varchar, FV.FieldOrder) + convert(varchar, FV.SectionId) Else FV.FieldName End 
	, FieldLabel
	, FV.Predefined
	, FV.PDFTableName
	, FV.ControlId
	, FV.ControlReferenceId ReferenceId
	, FV.BindTable As BindSource
	, FV.BindCategory
	, FV.CategoryId CodeId
	, FV.FieldOrder Seq
	, FV.SectionOrder SeqSection
	, FV.IsGridView
	, TS.TabId
	, MT.TabName
	, MT.seq TabSeq 
 From FormFieldsView FV  
 Inner Join lnk_FormTabSection TS On TS.FeatureID = FV.FeatureId And TS.SectionID = FV.SectionId
 Inner Join Mst_FormBuilderTab MT On TS.TabID = MT.TabID
 Where FV.FeatureId= @FeatureId
 And (FV.PatientRegistration Is Null Or FV.PatientRegistration = 0)    
 And (FV.FeatureDeleteFlag = 0 Or FV.FeatureDeleteFlag Is Null)  
 Order By SeqSection, Seq, TabSeq    Asc                                        
                                                                                     
---Table 02 [for Business Rule]                                                                                                                                                       
Select Distinct Y.FieldId
	,Y.FieldLabel
	,Y.Predefined
	,Y.BusRuleId
	,Y.FieldName
	,Y.BusRuleName Name
	,Y.ControlId
	,Y.CtrlReferenceId
	,Y.BusRuleReferenceId
	,ISNULL(Y.Value,0)[Value] 
	,ISNULL(Y.Value1,0)[Value1]    
	,Upper(Y.TableName)[TableName]
	, Y.TabId 
	, Y.SectionId
	From (    	
		Select Distinct PaddedFieldId  FieldId
			  ,V.FieldLabel
			  ,V.Predefined
			  ,V.BusRuleId
			  ,V.FieldName
			  ,V.TableName
			  ,V.ControlId
			  ,V.CtrlReferenceId
			  ,V.TabId	
			  ,V.SectionID SectionId	 
			  ,V.BusRuleReferenceId
			  ,V.BusRuleName
			  ,Isnull(V.Value,0) Value
			  ,Isnull(V.Value1,0) Value1
		From FormFieldsBusinessRuleView V Where V.FeatureId = @FeatureId                                                                                                                   
		Union                                                                                  
		Select Distinct	Case b.Predefined
					When 1 Then '9999' + convert(varchar, a.ConditionalFieldId)
					When 0 Then '8888' + convert(varchar, a.ConditionalFieldId)
				End								As FieldId
				,a.ConditionalFieldLabel		As FieldLabel
				,a.ConditionalFieldPredefined	As Predefined
				,c.Id							As BusRuleId
				,a.ConditionalFieldName			As FieldName
				,upper(a.ConditionalFieldSavingTable) TableName
				,a.ConditionalFieldControlId	As ControlId
				,Ctrl.ReferenceId CtrlReferenceId
				,a.TabId
				,a. FieldSectionId SectionId		
				,C.ReferenceId BusRuleReferenceId
				,C.Name BusRuleName
				,Isnull(b.Value,0) Value
				,Isnull(b.Value1,0) Value1
		From VW_FieldConditionalField As a
		Inner Join lnk_fieldsBusinessRule As b On a.ConditionalFieldId = b.FieldId
		Inner Join mst_control Ctrl On Ctrl.ControlId = a.ConditionalFieldControlId
		And a.ConditionalFieldPredefined = b.Predefined
		Inner Join Mst_BusinessRule As c On b.BusRuleId = c.Id
		Where (a.FeatureId = @FeatureId)		                                                
  ) Y    Order By BusRuleId                          
---Table 03 for all Controls Except MultiSelect   
Select Distinct	FV.FeatureId
	,	FV.FeatureName
	,	FV.SectionId
	,	FV.SectionName
	,	FV.SectionInfo
	,	FV.PaddedFieldId	As FieldId
	,	FV.FieldName
	,	FV.FieldLabel
	,	FV.Predefined
	,	FV.PDFTableName
	,	FV.ControlId
	,   FV.ControlReferenceId ReferenceId
	,	FV.BindTable		As BindSource
	,	FV.CategoryId		As CodeId
	,   FV.BindCategory
	,	FV.FieldOrder		As Seq
	,	FV.IsGridView
	,	TS.TabID
	,	TB.TabName
From FormFieldsView As FV
Inner Join lnk_FormTabSection As TS On FV.FeatureId = TS.FeatureID
And TS.SectionID = FV.SectionId
Inner Join Mst_FormBuilderTab As TB On TS.TabID = TS.TabID
And TB.FeatureID = TS.FeatureID
Where (FV.ControlId Not In (9, 16))
	And (FV.FieldId <> 71)
	And (FV.FeatureDeleteFlag = 0 Or FV.FeatureDeleteFlag Is Null)
Order By Seq                                                                          
                         
--- 04                                                                                                                                    
 Select drug_pk[DrugId],DrugName,0 [Generic],DrugTypeId ,[Generic Abbrevation][Abbr]                                                                                                                                                       
 from vw_Drug                                                   
 union                                                                                                                                                  
 Select GenericId [DrugId],GenericName [DrugName],GenericId [Generic],drugTypeId,GenericAbbrevation[Abbr]                                                                                                           
 from vw_Generic where GenericId is not null  order by [DrugName]                                           
                                                                                              
                                                                               
--- 05                                                                                           
 Select a.drug_pk[DrugId],a.DrugName, b.genericid,b.GenericAbbrevation[Abbr]                                                                                                                                                        
 from mst_drug a, mst_Generic b,lnk_drugGeneric c where a.deleteflag = 0 and dbo.fn_GetDrugTypeId_futures(a.drug_pk) = 37                     
 and a.drug_pk=c.drug_pk  and b.GenericID=c.GenericID                                               
 group by a.drug_pk,a.DrugName,b.GenericAbbrevation,b.genericid                                                                                                            
 union                                                                 
 Select GenericId[DrugId],GenericName[DrugName],GenericId[GenericId],GenericAbbrevation[Abbr]                                                                                                                                                    
 from vw_Generic where GenericId is not null  order by [DrugName]                                                                                        
                                              
-- 06    
Select	 T.Id LabTestId
		,T.Name LabName
		,P.Id SubTestID
		,P.ParameterName SubTestName
		,1 LabTypeID
		,'Additional Laboratory Test' LabTypeName
		,T.DepartmentId LabDepartmentID
		,(Select LabDepartmentName From mst_LabDepartment D Where D.LabDepartmentID = T.DepartmentId) LabDepartmentName
		,T.DeleteFlag
		,T.ReferenceId LabReferenceId
		,P.ParameterName ParameterRefereceId
		,P.DataType
From mst_LabTestMaster As T
Inner Join Mst_LabTestParameter As P On T.Id = P.LabTestId
Order By T.Id                   
--Select	T.LabTestID
--		,T.LabName
--		,P.SubTestID
--		,P.SubTestName
--		,1 LabTypeID
--		,'Additional Laboratory Test' LabTypeName
--		,T.LabDepartmentID
--		,(Select LabDepartmentName From mst_LabDepartment D Where D.LabDepartmentID = T.LabDepartmentId) LabDepartmentName
--		,T.DeleteFlag
--From Mst_LabTest As T
--Inner Join lnk_TestParameter As P On T.LabTestID = P.TestID
--Order By T.LabTestID                                                                                               
                                                                                       
--07                                                                                                                                  
                                                                                                      
Select	Id		[UnitId]
	,	Name	[UnitName]
From mst_decode
Where codeid = 32
	And deleteflag = 0                                                                 
                                                                                                                                                                               
--08                                                                                              
	Select Distinct	'0'	As Drug_pk
					,d.GenericID
					,b.StrengthId
					,b.StrengthName
	From lnk_DrugStrength As a
	Inner Join mst_Strength As b On a.StrengthId = b.StrengthId
	Inner Join mst_Generic As d On D.GenericID = A.GenericID
	Where (d.GenericID = a.GenericID)                                                                                                                 
	Union                                                                 
	Select Distinct	d.Drug_pk
					,'0'	As GenericId
					,c.StrengthId
					,c.StrengthName
	From lnk_DrugGeneric As b
	Inner Join Mst_Drug As d On b.Drug_pk = d.Drug_pk
	Inner Join lnk_DrugStrength As a On b.GenericID = a.GenericID
	Inner Join mst_Strength As c On a.StrengthId = c.StrengthId
	Order By GenericId, b.StrengthId, b.StrengthName                                                       
                                                                                                                              
--09                                                                                    
	Select Distinct	'0'		As Drug_pk
					,d.GenericID
					,a.FrequencyId
					,b.Name	As FrequencyName
	From lnk_DrugFrequency As a
	Inner Join mst_Frequency As b On a.FrequencyId = b.ID
	Inner Join mst_Generic As d On a.GenericID = d.GenericID                                                   
	Union                                      
	Select Distinct	d.Drug_pk
					,'0'	As GenericId
					,c.ID	As FrequencyId
					,c.Name	As FrequencyName
	From lnk_DrugGeneric As b
	Inner Join Mst_Drug As d On b.Drug_pk = d.Drug_pk
	Inner Join lnk_DrugFrequency As a On b.GenericID = a.GenericID
	Inner Join mst_Frequency As c On a.FrequencyId = c.ID                                                                       
                                                                                                                             
----11                                                    
--select a.GenericId,a.GenericName,a.GenericAbbrevation,b.DrugTypeId                                             
--from mst_generic a,lnk_drugtypegeneric b                                                                                                                                                          
--where a.genericid = b.genericid and a.deleteflag = 0                                                     
                                                                                                                              
--10                                                                                                                                
Select a.Drug_Pk,a.DrugName,0 [Generic],dbo.fn_GetDrugTypeId_futures(a.Drug_Pk)[DrugTypeId],                                               
dbo.fn_GetFixedDoseDrugAbbrevation(a.Drug_Pk)[Abbr]                                                                                                                                                       
from mst_drug a where  a.deleteflag = 0                                                                
--select * from mst_DrugType                                              
--select * from mst_Drug                                              
--select * from dbo.Lnk_DrugTypeGeneric                                              
                                                                                                                         
--11                                                                                                                                
Select a.GenericId,a.GenericName [DrugName],a.GenericId [Generic],b.drugTypeId,a.GenericAbbrevation[Abbr]                                                              
from mst_generic a,lnk_drugtypegeneric b where a.genericid = b.genericid and                                                                 
a.deleteflag = 0                                                                                                         
                                                                                                      
                                                                   
--12                                   
--for OI Treatment Other Medications  - Frq to be displayed from custorm list                                                                                                                                                              
select Id [FrequencyId],Name [FrequencyName]  from mst_FrequencyUnits where deleteflag = 0                                                                                                                                  
--13                                                                                     
Select DrugTypeID, DrugTypeName from mst_drugtype where deleteflag = 0                                                                                                                                
--14                                                                                                                  
Select * from mst_feature where FeatureId=@FeatureId                                              
--15                                                                                                            
Select @FormVisitType=MultiVisit,@FeatureName=FeatureName from mst_feature where FeatureId=@FeatureId                                                                                                    
                                                                                                         
Select @VisitTypeId=VisitTypeId from mst_Visittype where (deleteflag =0 or deleteflag is null) and visitname=@FeatureName                                            
                                       
 if(@FormVisitType=1)                                                               
  Begin                                                                                                            
   Select '0'[Visit_Id]                                                                                                          
  End                                                          
 Else                                       
  Begin                                                      
  Select Visit_Id,VisitDate from Ord_Visit where Ptn_pk=@PatientId and VisitType=@VisitTypeId and (DeleteFlag IS NULL or DeleteFlag=0)                                                                                  
                                                                                                  
  End                                                                
--16                                                                                                        
                                                                    
select a.drug_pk,a.drugname,d.drugtypeid,b.GenericAbbrevation,b.genericid,b.genericname                                                                                                                                         
from mst_drug a,mst_generic b,lnk_druggeneric c,lnk_DrugTypeGeneric d  where                                           
c.genericid = b.genericid and c.drug_pk = a.drug_pk and                                                                                                                                                 
a.deleteflag = 0                                                                                                                      
and a.Drug_pk=c.Drug_pk                                                                                                                              
and c.GenericID=d.GenericID                                                                                                                                        
order by a.drugname                                      
                                                                    
---17 Conditional Fields                                                                                                    

select Distinct a.FeatureId,b.FeatureName,a.FieldSectionId,a.FieldSectionName,                                                                                              
case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId) end [FieldId],                            
a.ConditionalFieldBindField [FieldName],                                                                                              
a.ConditionalFieldLabel [FieldLabel], a.ConditionalFieldPredefined [Predefined],                                                                                              
Upper(a.ConditionalFieldSavingTable) [PDFTableName],a.ConditionalFieldControlId [ControlId],                                               
a.ConditionalFieldBindTable [BindSource],a.ConditionalFieldCategoryId [CodeId],                                                                                              
a.ConditionalFieldSequence [Seq],a.FieldSectionSequence [SeqSection],ConditionalFieldSectionId,                            
case a.FieldPredefined when 1 then '9999'+convert(varchar,a.FieldId) when 0 then '8888'+convert(varchar,a.FieldId) end [ConFieldId],                                                    
a.FieldPredefined [ConFieldPredefined], a.TabId, 
case a.ConditionalFieldControlId when 6 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'RADIO1-'+a.ConditionalFieldBindField+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                   
end [ConControlId],                     
a.TabName,
dbo.GetLookupName(a.ConditionalFieldCategoryId, a.ConditionalFieldBindTable)	BindCategory,
(Select Top 1 ReferenceId From mst_control Where ControlID =a.ConditionalFieldControlId ) ReferenceId
from Vw_FieldConditionalField a inner join Mst_Feature b on a.FeatureId = b.FeatureId                                                  
and a.ConditionalFieldPredefined = 1 and b.FeatureId = @FeatureId and a.ConditionalFieldId is not null                                                                                     
and a.ConditionalFieldName is not null and a.ConditionalFieldControlId = 6
union  
select Distinct a.FeatureId,b.FeatureName,a.FieldSectionId,a.FieldSectionName,                                                                                              
case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId) end [FieldId],                            
a.ConditionalFieldName [FieldName],                                                               
a.ConditionalFieldLabel [FieldLabel], a.ConditionalFieldPredefined [Predefined],                                                                                              
Upper(a.ConditionalFieldSavingTable) [PDFTableName],a.ConditionalFieldControlId [ControlId],                                                                                              
a.ConditionalFieldBindTable [BindSource],a.ConditionalFieldCategoryId [CodeId],                                                                                              
a.ConditionalFieldSequence [Seq],a.FieldSectionSequence [SeqSection],ConditionalFieldSectionId,                            
case a.FieldPredefined when 1 then '9999'+convert(varchar,a.FieldId) when 0 then '8888'+convert(varchar,a.FieldId) end [ConFieldId],                            
a.FieldPredefined [ConFieldPredefined], a.TabId,
case a.ConditionalFieldControlId when 6 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'RADIO1-'+a.ConditionalFieldName+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                     
end [ConControlId],                     
a.TabName   ,
dbo.GetLookupName(a.ConditionalFieldCategoryId, a.ConditionalFieldBindTable)	BindCategory,
(Select Top 1 ReferenceId From mst_control Where ControlID =a.ConditionalFieldControlId ) ReferenceId     
from Vw_FieldConditionalField a inner join Mst_Feature b on a.FeatureId = b.FeatureId                                                                                
and a.ConditionalFieldPredefined = 0 and b.FeatureId = @FeatureId and a.ConditionalFieldId is not null                                                                                     
and a.ConditionalFieldName is not null and a.ConditionalFieldControlId = 6      
union                   
select Distinct a.FeatureId,b.FeatureName,a.FieldSectionId,a.FieldSectionName,                                                                                              
case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId) end [FieldId],                            
a.ConditionalFieldBindField [FieldName],                                                                                              
a.ConditionalFieldLabel [FieldLabel], a.ConditionalFieldPredefined [Predefined],                                                                                              
Upper(a.ConditionalFieldSavingTable) [PDFTableName],a.ConditionalFieldControlId [ControlId],                                               
a.ConditionalFieldBindTable [BindSource],a.ConditionalFieldCategoryId [CodeId],                                                                                              
a.ConditionalFieldSequence [Seq],a.FieldSectionSequence [SeqSection],ConditionalFieldSectionId,                            
case a.FieldPredefined when 1 then '9999'+convert(varchar,a.FieldId) when 0 then '8888'+convert(varchar,a.FieldId) end [ConFieldId],                                                    
a.FieldPredefined [ConFieldPredefined], a.TabId, 
case a.ConditionalFieldControlId when 1 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'TXT-'+a.ConditionalFieldBindField+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                   
when 2 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'TXT-'+a.ConditionalFieldBindField+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                    
when 3 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'TXTNUM-'+a.ConditionalFieldBindField+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                   
when 4 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'SELECTLIST-'+a.ConditionalFieldBindField+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                  
when 5 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'TXTDT-'+a.ConditionalFieldBindField+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                     
when 6 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'RADIO2-'+a.ConditionalFieldBindField+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                   
when 7 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'Chk-'+a.ConditionalFieldBindField+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                   
when 8 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'TXTMulti-'+a.ConditionalFieldBindField+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                      
when 9 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'Pnl_-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end end [ConControlId],                     
a.TabName,
dbo.GetLookupName(a.ConditionalFieldCategoryId, a.ConditionalFieldBindTable)	BindCategory,
(Select Top 1 ReferenceId From mst_control Where ControlID =a.ConditionalFieldControlId ) ReferenceId
from Vw_FieldConditionalField a inner join Mst_Feature b on a.FeatureId = b.FeatureId                                                  
and a.ConditionalFieldPredefined = 1 and b.FeatureId = @FeatureId and a.ConditionalFieldId is not null                                                                                     
and a.ConditionalFieldName is not null                                   
union                                                                                    
select Distinct a.FeatureId,b.FeatureName,a.FieldSectionId,a.FieldSectionName,                                                                                              
case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId) end [FieldId],                            
a.ConditionalFieldName [FieldName],                                                               
a.ConditionalFieldLabel [FieldLabel], a.ConditionalFieldPredefined [Predefined],                                                                                              
Upper(a.ConditionalFieldSavingTable) [PDFTableName],a.ConditionalFieldControlId [ControlId],                                                                                              
a.ConditionalFieldBindTable [BindSource],a.ConditionalFieldCategoryId [CodeId],                                                                                              
a.ConditionalFieldSequence [Seq],a.FieldSectionSequence [SeqSection],ConditionalFieldSectionId,                            
case a.FieldPredefined when 1 then '9999'+convert(varchar,a.FieldId) when 0 then '8888'+convert(varchar,a.FieldId) end [ConFieldId],                            
a.FieldPredefined [ConFieldPredefined], a.TabId,
case a.ConditionalFieldControlId when 1 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'TXT-'+a.ConditionalFieldName+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                     
when 2 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'TXT-'+a.ConditionalFieldName+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                    
when 3 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'TXTNUM-'+a.ConditionalFieldName+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                    
when 4 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'SELECTLIST-'+a.ConditionalFieldName+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                    
when 5 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'TXTDT-'+a.ConditionalFieldName+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                    
when 6 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'RADIO2-'+a.ConditionalFieldName+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                     
when 7 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'Chk-'+a.ConditionalFieldName+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                     
when 8 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'TXTMulti-'+a.ConditionalFieldName+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId)                     
when 9 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'Pnl_-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end end [ConControlId],                     
a.TabName   ,
dbo.GetLookupName(a.ConditionalFieldCategoryId, a.ConditionalFieldBindTable)	BindCategory,
(Select Top 1 ReferenceId From mst_control Where ControlID =a.ConditionalFieldControlId ) ReferenceId      
from Vw_FieldConditionalField a inner join Mst_Feature b on a.FeatureId = b.FeatureId                                                                                
and a.ConditionalFieldPredefined = 0 and b.FeatureId = @FeatureId and a.ConditionalFieldId is not null                                                                                     
and a.ConditionalFieldName is not null                                                                         
union                                                                   
select Distinct a.FeatureId,b.FeatureName,a.FieldSectionId,a.FieldSectionName,                                      
a.ConditionalFieldId [FieldId],'PlaceHolder' [FieldName],                                                                                              
a.ConditionalFieldLabel [FieldLabel], a.ConditionalFieldPredefined [Predefined],                                                                                              
Upper(a.ConditionalFieldSavingTable) [PDFTableName],'13' [ControlId],                                                    
a.ConditionalFieldBindTable [BindSource],a.ConditionalFieldCategoryId [CodeId],                                                  
a.ConditionalFieldSequence [Seq],a.FieldSectionSequence [SeqSection],ConditionalFieldSectionId,a.FieldId [ConFieldId],                                                                                    
a.FieldPredefined [ConFieldPredefined], a.TabId, 
case a.ConditionalFieldControlId when 4 then 'ctl00_IQCareContentPlaceHolder_TAB_'+convert(varchar,a.TabId)+'_'+'SELECTLIST-'+'PlaceHolder'+'-'+Upper(a.ConditionalFieldSavingTable)+'-'+case a.ConditionalFieldPredefined when 1 then '9999'+convert(varchar,a.ConditionalFieldId) when 0 then '8888'+convert(varchar,a.ConditionalFieldId)end +'-'+convert(varchar,a.TabId) end [ConControlId],                     
a.TabName ,
dbo.GetLookupName(a.ConditionalFieldCategoryId, a.ConditionalFieldBindTable)	BindCategory,
(Select Top 1 ReferenceId From mst_control Where ControlID =a.ConditionalFieldControlId ) ReferenceId                                                                                            
from Vw_FieldConditionalField a inner join Mst_Feature b on a.FeatureId = b.FeatureId                                                                                              
and a.ConditionalFieldPredefined = 1 and b.FeatureId = @FeatureId and a.ConditionalFieldId is not null                                                              
and a.ConditionalFieldId like '710000%'                                                                                                         
--18                                                                                   
select a.StartDate from dbo.lnk_PatientProgramStart a                                                                                     
Inner Join Mst_Feature b on a.ModuleId = b.ModuleId                                                                                    
where b.FeatureId = @FeatureId and a.Ptn_Pk = @PatientId                                                                       
                                                                          
--19                                                                     
Declare @sql nvarchar(max)                                                                
set @sql ='if exists(select * from sysobjects where name=''DTL_FBCUSTOMFIELD_'+REPLACE(@FeatureName,' ','_')+''')                                                                   
Begin                                                           
select  * from [DTL_FBCUSTOMFIELD_'+REPLACE(@FeatureName,' ','_')+'] a inner join ord_visit b on a.visit_pk=b.Visit_Id                                                                           
where b.ptn_pk='+ convert(varchar,@PatientId)+' order by b.visitdate desc                                                                   
end                                                              
else                                                               
Begin                                                              
Select 0                                      End'                                                                        
EXECUTE sp_executesql @sql                                                                
--print  @sql                              
--20    
                                                      
select Z.Visit_ID[VisitID],z.VisitDate                                             
    from(Select visit_id, VisitDate from ord_Visit where Visittype                                                         
   =(select VisitTypeID from mst_visittype where (deleteflag = 0 or deleteflag is null) and  VisitTypeID <>0 and convert(binary(50),VisitName) =                  
   convert(binary(50),(Select FeatureName from mst_feature where FeatureID=@FeatureId))and FeatureID=@FeatureId  )                                 
     and Ptn_Pk=@PatientId)Z                                                        
     where Z.visitdate =(select X.Visitdate from(Select distinct  max(visitdate)[visitdate] from ord_Visit                                                     
     where  (deleteflag = 0 or deleteflag is null) and Visittype = (select VisitTypeID from mst_visittype                                                      
      where (deleteflag = 0 or deleteflag is null) and VisitTypeID <>0 and VisitTypeID <>0 and convert(binary(50),VisitName) =                                   
     convert(binary(50),(Select FeatureName from mst_feature where FeatureID=@FeatureId)) and FeatureID=@FeatureId )                                                         
      and Ptn_Pk=@PatientId)X)                                 
--21                                          
select '0'[Drug_pk],'0'[GenericId],c.ID[FrequencyId],c.Name[FrequencyName]                                               
from mst_Frequency c                                                                                                                                                                                     
where (c.deleteflag=0 or c.deleteflag IS NULL)                                            
order by c.Id                                                     
--22                              
select A.Ptn_pk, A.Visit_pk, A.LocationId, Case When A.Predefined=0 then Convert(int, '8888'+Convert(varchar,A.FieldId)) when A.Predefined=1 then                               
Convert(int, '9999'+Convert(varchar,A.FieldId))end[FieldId], A.BlockId, A.SubBlockId, A.ICDCodeId[Id],                               
+'%'+Convert(Varchar,ISNULL(A.BlockId,0)) +'%'+ Convert(Varchar,ISNULL(A.SubBlockId,0))+'%'+Convert(Varchar,ISNULL(A.ICDCodeId,0))+'%'+Convert(Varchar, A.Predefined)[CodeId],                               
Case When A.BlockId > 0 Then B.Code+' '+B.Name When A.SubBlockId>0 Then C.Code+' '+C.Name When A.ICDCodeId > 0                               
Then D.Code+' '+D.Name end [Name]                              
from dtl_ICD10Field A left outer join dbo.Mst_ICDCodeBlocks B on A.BlockId=B.BlockId                              
left outer join dbo.Mst_ICDCodeSubBlock C on A.SubBlockId=C.SubBlockId                              
left outer join dbo.mst_ICDCodes D on A.ICDCodeId=D.Id where A.Ptn_pk=@PatientId                              
                  
--23            
Select	tbl1.TabID
		,tbl2.TabName
		,tbl2.seq
		,isnull(tbl2.Signature, 0)	As signature		
From lnk_FormTabSection As tbl1
Inner Join Mst_FormBuilderTab As tbl2 On tbl1.TabID = tbl2.TabID
And tbl1.FeatureID = @FeatureId
Group By	tbl1.TabID
			,tbl2.TabName
			,tbl2.seq
			,tbl2.Signature
Order By tbl2.seq                             
                                                                                                                                         
End
  
GO




