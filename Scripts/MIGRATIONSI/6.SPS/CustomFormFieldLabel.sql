IF NOT  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Clinical_GetCustomFormFieldLabel_Constella]') AND type in (N'P', N'PC'))
BEGIN
EXEC('

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
	Select	dbo.fn_PatientIdentificationNumber_Constella(a.Ptn_Pk, '''', 1) As PatientIdentification
			,convert(varchar(50), decryptbykey(a.FirstName)) + '' '' + isnull(convert(varchar(50), decryptbykey(a.MiddleName)), '''')
			+ '' '' + convert(varchar(50), decryptbykey(a.LastName)) As Name
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
	, FieldName = Case When FieldID = 71 And FV.Predefined = 1 Then ''PlaceHolder'' + convert(varchar, FV.FieldOrder) + convert(varchar, FV.SectionId) Else FV.FieldName End 
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
					When 1 Then ''9999'' + convert(varchar, a.ConditionalFieldId)
					When 0 Then ''8888'' + convert(varchar, a.ConditionalFieldId)
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
		,''Additional Laboratory Test'' LabTypeName
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
--		,''Additional Laboratory Test'' LabTypeName
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
	Select Distinct	''0''	As Drug_pk
					,d.GenericID
					,b.StrengthId
					,b.StrengthName
	From lnk_DrugStrength As a
	Inner Join mst_Strength As b On a.StrengthId = b.StrengthId
	Inner Join mst_Generic As d On D.GenericID = A.GenericID
	Where (d.GenericID = a.GenericID)                                                                                                                 
	Union                                                                 
	Select Distinct	d.Drug_pk
					,''0''	As GenericId
					,c.StrengthId
					,c.StrengthName
	From lnk_DrugGeneric As b
	Inner Join Mst_Drug As d On b.Drug_pk = d.Drug_pk
	Inner Join lnk_DrugStrength As a On b.GenericID = a.GenericID
	Inner Join mst_Strength As c On a.StrengthId = c.StrengthId
	Order By GenericId, b.StrengthId, b.StrengthName                                                       
                                                                                                                              
--09                                                                                    
	Select Distinct	''0''		As Drug_pk
					,d.GenericID
					,a.FrequencyId
					,b.Name	As FrequencyName
	From lnk_DrugFrequency As a
	Inner Join mst_Frequency As b On a.FrequencyId = b.ID
	Inner Join mst_Generic As d On a.GenericID = d.GenericID                                                   
	Union                                      
	Select Distinct	d.Drug_pk
					,''0''	As GenericId
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
   Select ''0''[Visit_Id]                                                                                                          
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
case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId) end [FieldId],                            
a.ConditionalFieldBindField [FieldName],                                                                                              
a.ConditionalFieldLabel [FieldLabel], a.ConditionalFieldPredefined [Predefined],                                                                                              
Upper(a.ConditionalFieldSavingTable) [PDFTableName],a.ConditionalFieldControlId [ControlId],                                               
a.ConditionalFieldBindTable [BindSource],a.ConditionalFieldCategoryId [CodeId],                                                                                              
a.ConditionalFieldSequence [Seq],a.FieldSectionSequence [SeqSection],ConditionalFieldSectionId,                            
case a.FieldPredefined when 1 then ''9999''+convert(varchar,a.FieldId) when 0 then ''8888''+convert(varchar,a.FieldId) end [ConFieldId],                                                    
a.FieldPredefined [ConFieldPredefined], a.TabId, 
case a.ConditionalFieldControlId when 6 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''RADIO1-''+a.ConditionalFieldBindField+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                   
end [ConControlId],                     
a.TabName,
dbo.GetLookupName(a.ConditionalFieldCategoryId, a.ConditionalFieldBindTable)	BindCategory,
(Select Top 1 ReferenceId From mst_control Where ControlID =a.ConditionalFieldControlId ) ReferenceId
from Vw_FieldConditionalField a inner join Mst_Feature b on a.FeatureId = b.FeatureId                                                  
and a.ConditionalFieldPredefined = 1 and b.FeatureId = @FeatureId and a.ConditionalFieldId is not null                                                                                     
and a.ConditionalFieldName is not null and a.ConditionalFieldControlId = 6
union  
select Distinct a.FeatureId,b.FeatureName,a.FieldSectionId,a.FieldSectionName,                                                                                              
case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId) end [FieldId],                            
a.ConditionalFieldName [FieldName],                                                               
a.ConditionalFieldLabel [FieldLabel], a.ConditionalFieldPredefined [Predefined],                                                                                              
Upper(a.ConditionalFieldSavingTable) [PDFTableName],a.ConditionalFieldControlId [ControlId],                                                                                              
a.ConditionalFieldBindTable [BindSource],a.ConditionalFieldCategoryId [CodeId],                                                                                              
a.ConditionalFieldSequence [Seq],a.FieldSectionSequence [SeqSection],ConditionalFieldSectionId,                            
case a.FieldPredefined when 1 then ''9999''+convert(varchar,a.FieldId) when 0 then ''8888''+convert(varchar,a.FieldId) end [ConFieldId],                            
a.FieldPredefined [ConFieldPredefined], a.TabId,
case a.ConditionalFieldControlId when 6 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''RADIO1-''+a.ConditionalFieldName+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                     
end [ConControlId],                     
a.TabName   ,
dbo.GetLookupName(a.ConditionalFieldCategoryId, a.ConditionalFieldBindTable)	BindCategory,
(Select Top 1 ReferenceId From mst_control Where ControlID =a.ConditionalFieldControlId ) ReferenceId     
from Vw_FieldConditionalField a inner join Mst_Feature b on a.FeatureId = b.FeatureId                                                                                
and a.ConditionalFieldPredefined = 0 and b.FeatureId = @FeatureId and a.ConditionalFieldId is not null                                                                                     
and a.ConditionalFieldName is not null and a.ConditionalFieldControlId = 6      
union                   
select Distinct a.FeatureId,b.FeatureName,a.FieldSectionId,a.FieldSectionName,                                                                                              
case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId) end [FieldId],                            
a.ConditionalFieldBindField [FieldName],                                                                                              
a.ConditionalFieldLabel [FieldLabel], a.ConditionalFieldPredefined [Predefined],                                                                                              
Upper(a.ConditionalFieldSavingTable) [PDFTableName],a.ConditionalFieldControlId [ControlId],                                               
a.ConditionalFieldBindTable [BindSource],a.ConditionalFieldCategoryId [CodeId],                                                                                              
a.ConditionalFieldSequence [Seq],a.FieldSectionSequence [SeqSection],ConditionalFieldSectionId,                            
case a.FieldPredefined when 1 then ''9999''+convert(varchar,a.FieldId) when 0 then ''8888''+convert(varchar,a.FieldId) end [ConFieldId],                                                    
a.FieldPredefined [ConFieldPredefined], a.TabId, 
case a.ConditionalFieldControlId when 1 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''TXT-''+a.ConditionalFieldBindField+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                   
when 2 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''TXT-''+a.ConditionalFieldBindField+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                    
when 3 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''TXTNUM-''+a.ConditionalFieldBindField+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                   
when 4 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''SELECTLIST-''+a.ConditionalFieldBindField+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                  
when 5 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''TXTDT-''+a.ConditionalFieldBindField+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                     
when 6 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''RADIO2-''+a.ConditionalFieldBindField+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                   
when 7 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''Chk-''+a.ConditionalFieldBindField+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                   
when 8 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''TXTMulti-''+a.ConditionalFieldBindField+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                      
when 9 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''Pnl_-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end end [ConControlId],                     
a.TabName,
dbo.GetLookupName(a.ConditionalFieldCategoryId, a.ConditionalFieldBindTable)	BindCategory,
(Select Top 1 ReferenceId From mst_control Where ControlID =a.ConditionalFieldControlId ) ReferenceId
from Vw_FieldConditionalField a inner join Mst_Feature b on a.FeatureId = b.FeatureId                                                  
and a.ConditionalFieldPredefined = 1 and b.FeatureId = @FeatureId and a.ConditionalFieldId is not null                                                                                     
and a.ConditionalFieldName is not null                                   
union                                                                                    
select Distinct a.FeatureId,b.FeatureName,a.FieldSectionId,a.FieldSectionName,                                                                                              
case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId) end [FieldId],                            
a.ConditionalFieldName [FieldName],                                                               
a.ConditionalFieldLabel [FieldLabel], a.ConditionalFieldPredefined [Predefined],                                                                                              
Upper(a.ConditionalFieldSavingTable) [PDFTableName],a.ConditionalFieldControlId [ControlId],                                                                                              
a.ConditionalFieldBindTable [BindSource],a.ConditionalFieldCategoryId [CodeId],                                                                                              
a.ConditionalFieldSequence [Seq],a.FieldSectionSequence [SeqSection],ConditionalFieldSectionId,                            
case a.FieldPredefined when 1 then ''9999''+convert(varchar,a.FieldId) when 0 then ''8888''+convert(varchar,a.FieldId) end [ConFieldId],                            
a.FieldPredefined [ConFieldPredefined], a.TabId,
case a.ConditionalFieldControlId when 1 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''TXT-''+a.ConditionalFieldName+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                     
when 2 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''TXT-''+a.ConditionalFieldName+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                    
when 3 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''TXTNUM-''+a.ConditionalFieldName+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                    
when 4 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''SELECTLIST-''+a.ConditionalFieldName+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                    
when 5 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''TXTDT-''+a.ConditionalFieldName+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                    
when 6 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''RADIO2-''+a.ConditionalFieldName+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                     
when 7 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''Chk-''+a.ConditionalFieldName+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                     
when 8 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''TXTMulti-''+a.ConditionalFieldName+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId)                     
when 9 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''Pnl_-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end end [ConControlId],                     
a.TabName   ,
dbo.GetLookupName(a.ConditionalFieldCategoryId, a.ConditionalFieldBindTable)	BindCategory,
(Select Top 1 ReferenceId From mst_control Where ControlID =a.ConditionalFieldControlId ) ReferenceId      
from Vw_FieldConditionalField a inner join Mst_Feature b on a.FeatureId = b.FeatureId                                                                                
and a.ConditionalFieldPredefined = 0 and b.FeatureId = @FeatureId and a.ConditionalFieldId is not null                                                                                     
and a.ConditionalFieldName is not null                                                                         
union                                                                   
select Distinct a.FeatureId,b.FeatureName,a.FieldSectionId,a.FieldSectionName,                                      
a.ConditionalFieldId [FieldId],''PlaceHolder'' [FieldName],                                                                                              
a.ConditionalFieldLabel [FieldLabel], a.ConditionalFieldPredefined [Predefined],                                                                                              
Upper(a.ConditionalFieldSavingTable) [PDFTableName],''13'' [ControlId],                                                    
a.ConditionalFieldBindTable [BindSource],a.ConditionalFieldCategoryId [CodeId],                                                  
a.ConditionalFieldSequence [Seq],a.FieldSectionSequence [SeqSection],ConditionalFieldSectionId,a.FieldId [ConFieldId],                                                                                    
a.FieldPredefined [ConFieldPredefined], a.TabId, 
case a.ConditionalFieldControlId when 4 then ''ctl00_IQCareContentPlaceHolder_TAB_''+convert(varchar,a.TabId)+''_''+''SELECTLIST-''+''PlaceHolder''+''-''+Upper(a.ConditionalFieldSavingTable)+''-''+case a.ConditionalFieldPredefined when 1 then ''9999''+convert(varchar,a.ConditionalFieldId) when 0 then ''8888''+convert(varchar,a.ConditionalFieldId)end +''-''+convert(varchar,a.TabId) end [ConControlId],                     
a.TabName ,
dbo.GetLookupName(a.ConditionalFieldCategoryId, a.ConditionalFieldBindTable)	BindCategory,
(Select Top 1 ReferenceId From mst_control Where ControlID =a.ConditionalFieldControlId ) ReferenceId                                                                                            
from Vw_FieldConditionalField a inner join Mst_Feature b on a.FeatureId = b.FeatureId                                                                                              
and a.ConditionalFieldPredefined = 1 and b.FeatureId = @FeatureId and a.ConditionalFieldId is not null                                                              
and a.ConditionalFieldId like ''710000%''                                                                                                         
--18                                                                                   
select a.StartDate from dbo.lnk_PatientProgramStart a                                                                                     
Inner Join Mst_Feature b on a.ModuleId = b.ModuleId                                                                                    
where b.FeatureId = @FeatureId and a.Ptn_Pk = @PatientId                                                                       
                                                                          
--19                                                                     
Declare @sql nvarchar(max)                                                                
set @sql =''if exists(select * from sysobjects where name=''''DTL_FBCUSTOMFIELD_''+REPLACE(@FeatureName,'' '',''_'')+'''''')                                                                   
Begin                                                           
select  * from [DTL_FBCUSTOMFIELD_''+REPLACE(@FeatureName,'' '',''_'')+''] a inner join ord_visit b on a.visit_pk=b.Visit_Id                                                                           
where b.ptn_pk=''+ convert(varchar,@PatientId)+'' order by b.visitdate desc                                                                   
end                                                              
else                                                               
Begin                                                              
Select 0                                      End''                                                                        
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
select ''0''[Drug_pk],''0''[GenericId],c.ID[FrequencyId],c.Name[FrequencyName]                                               
from mst_Frequency c                                                                                                                                                                                     
where (c.deleteflag=0 or c.deleteflag IS NULL)                                            
order by c.Id                                                     
--22                              
select A.Ptn_pk, A.Visit_pk, A.LocationId, Case When A.Predefined=0 then Convert(int, ''8888''+Convert(varchar,A.FieldId)) when A.Predefined=1 then                               
Convert(int, ''9999''+Convert(varchar,A.FieldId))end[FieldId], A.BlockId, A.SubBlockId, A.ICDCodeId[Id],                               
+''%''+Convert(Varchar,ISNULL(A.BlockId,0)) +''%''+ Convert(Varchar,ISNULL(A.SubBlockId,0))+''%''+Convert(Varchar,ISNULL(A.ICDCodeId,0))+''%''+Convert(Varchar, A.Predefined)[CodeId],                               
Case When A.BlockId > 0 Then B.Code+'' ''+B.Name When A.SubBlockId>0 Then C.Code+'' ''+C.Name When A.ICDCodeId > 0                               
Then D.Code+'' ''+D.Name end [Name]                              
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
  



')
END