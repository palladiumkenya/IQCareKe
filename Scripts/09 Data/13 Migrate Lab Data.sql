Set Nocount On
Go
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vw_GenNewId]'))
DROP VIEW [dbo].[vw_GenNewId]
GO
create view vw_GenNewId as 
select 	Random_String =
	substring(x,(abs(checksum(newid()))%37)+1,1)+
	substring(x,(abs(checksum(newid()))%37)+1,1)+
	substring(x,(abs(checksum(newid()))%37)+1,1)+
	substring(x,(abs(checksum(newid()))%37)+1,1)+	
	substring(x,(abs(checksum(newid()))%37)+1,1)+
	substring(x,(abs(checksum(newid()))%37)+1,1)+
	substring(x,(abs(checksum(newid()))%37)+1,1)+
	substring(x,(abs(checksum(newid()))%37)+1,1)+
	substring(x,(abs(checksum(newid()))%37)+1,1)+
	substring(x,(abs(checksum(newid()))%37)+1,1)+
	substring(x,(abs(checksum(newid()))%37)+1,1)
from   (select x='49ABCFGHIJKDEL5678MNOPQRSTUVWXYZ0123') a
Go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RemoveNonAlphaCharacters]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[RemoveNonAlphaCharacters]
GO
Create Function [dbo].[RemoveNonAlphaCharacters](@Temp VarChar(1000))
Returns VarChar(36)
AS
Begin

    Declare @KeepValues as varchar(50)
    Set @KeepValues = '%[^a-z0-9#%_ ]%'
	set @temp = replace(@temp,' ','')
    While PatIndex(@KeepValues, @Temp) > 0
        Set @Temp = Stuff(@Temp, PatIndex(@KeepValues, @Temp), 1, '_')

    Return upper(left(replace(@Temp,' ','_'),36))
End
Go
If Not Exists (Select * From sys.columns Where Name = N'DataType' And Object_ID = Object_id(N'Mst_LabTest_Old'))  Begin
		Alter table dbo.Mst_LabTest_Old Add DataType  varchar(20) Null
	End
If Not exists (Select * From sys.columns Where Name = N'Migrated' And Object_ID = Object_id(N'Mst_LabTest_Old'))    
Begin
  Alter table dbo.Mst_LabTest_Old Add Migrated bit Default 0
end
Go
Update Mst_LabTest_Old Set Migrated = 0 Where Migrated  Is Null
Go
If Not exists (Select * From sys.columns Where Name = N'Migrated' And Object_ID = Object_id(N'lnk_TestParameter_Old'))    
Begin
  Alter table dbo.lnk_TestParameter_Old Add Migrated bit Default 0
end
Go
Update lnk_TestParameter_Old Set Migrated = 0 Where Migrated  Is Null
Go
If Not exists (Select * From sys.columns Where Name = N'Migrated' And Object_ID = Object_id(N'lnk_parameterresult_Old'))    
Begin
  Alter table dbo.lnk_parameterresult_Old Add Migrated bit Default 0
end
Go
Update lnk_parameterresult_Old Set Migrated = 0 Where Migrated  Is Null
Go
If Not exists (Select * From sys.columns Where Name = N'Migrated' And Object_ID = Object_id(N'lnk_LabValue_Old'))    
Begin
  Alter table dbo.lnk_LabValue_Old Add Migrated bit Default 0
end
Go
Update lnk_LabValue_Old Set Migrated = 0 Where Migrated  Is Null
Go
If Not exists (Select * From sys.columns Where Name = N'Migrated' And Object_ID = Object_id(N'ord_PatientLabOrder_Old'))    
Begin
  Alter table dbo.ord_PatientLabOrder_Old Add Migrated bit Default 0
end
Go
Update ord_PatientLabOrder_Old Set Migrated = 0 Where Migrated  Is Null
Update ord_PatientLabOrder_Old set LabNumber = (Select Random_String From vw_GenNewId)  where nullif(labnumber,'') is null
update lnk_TestParameter_Old set DeleteFlag= 1 Where subtestname='ViralLoad Undetectable'
Go
If Not exists (Select * From sys.columns Where Name = N'Migrated' And Object_ID = Object_id(N'dtl_patientlabresults_Old'))    
Begin
  Alter table dbo.dtl_patientlabresults_Old Add Migrated bit Default 0
end
Go
Update dtl_patientlabresults_Old Set Migrated = 0 Where Migrated  Is Null 
Go
/****** Object:  StoredProcedure [dbo].[Laboratory_MigrateData]    Script Date: 6/9/2016 6:43:37 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Laboratory_MigrateData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Laboratory_MigrateData]
GO


/****** Object:  StoredProcedure [dbo].[Laboratory_MigrateData]    Script Date: 6/9/2016 6:43:37 AM ******/
SET ANSI_NULLS ON
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Joseph Njunge
-- Create date: 20160320
-- Description:	Migrate lab data to new tables
-- =============================================
Create PROCEDURE [dbo].[Laboratory_MigrateData]
AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
	Set Nocount On;
	If object_id('tempdb..#LabTest') Is Not Null Drop Table #LabTest
	If object_id('tempdb..#New_LabTest') Is Not Null Drop Table #New_LabTest
	If object_id('tempdb..#TestParameter') Is Not Null Drop Table #TestParameter
	If object_id('tempdb..#New_TestParameter') Is Not Null Drop Table #New_TestParameter
	If object_id('tempdb..#ParamConfig') Is Not Null Drop Table #ParamConfig
	If object_id('tempdb..#New_ParamConfig') Is Not Null Drop Table #New_ParamConfig
	If object_id('tempdb..#ResultOption') Is Not Null Drop Table #ResultOption
	If object_id('tempdb..#New_ResultOption') Is Not Null Drop Table #New_ResultOption
	If object_id('tempdb..#Group') Is Not Null Drop Table #Group
	If object_id('tempdb..#New_Group') Is Not Null Drop Table #New_Group
	If object_id('tempdb..#Order') Is Not Null Drop Table #Order
	If object_id('tempdb..#New_Order') Is Not Null Drop Table #New_Order
	If object_id('tempdb..#OrderTest') Is Not Null Drop Table #OrderTest
	If object_id('tempdb..#New_OrderTest') Is Not Null Drop Table #New_OrderTest
	If object_id('tempdb..#TestResult') Is Not Null Drop Table #TestResult
	If object_id('tempdb..#New_TestResult') Is Not Null Drop Table #New_TestResult

	Create Table #LabTest(Id int Primary Key, ReferenceId varchar(36), Name varchar(100), IsGroup bit, DepartmentId int, Active bit, DeleteFlag bit);

	Create Table #New_LabTest(New_Id int, Old_Id int);

	Create Table #TestParameter(Id int Primary Key, ReferenceId varchar(36), ParameterName varchar(100), LabTestId int, DataType varchar(15), OrdRank decimal(4, 2), DeleteFlag bit);

	Create Table #New_TestParameter(New_Id int, Old_Id int, Old_TestId int);

	Create Table #ParamConfig(Id int Primary Key, ParameterId int, MinBoundary decimal(10, 2), MaxBoundary decimal(10, 2), MinNormalRange decimal(10, 2),
	MaxNormalRange decimal(10, 2), UnitId int, DefaultUnit bit, DetectionLimit decimal(10, 2), DeleteFlag bit);

	Create Table #New_ParamConfig(New_Id int, Old_Id int, Old_ParameterId int);

	Create Table #ResultOption(Id int Primary Key, ParameterId int, Value varchar(50), DeleteFlag bit);

	Create Table #New_ResultOption(New_Id int, Old_Id int, Old_ParameterId int);

	Create Table #Group(LabGroupTestId int, LabTestId int);

	Create Table #New_Group(New_Id int, Old_Id int);

	Create Table #Order(Id int Primary Key, Ptn_Pk int, LocationId int, VisitId int, ModuleId int, OrderedBy bigint, OrderDate datetime, PreClinicLabDate datetime,
	ClinicalOrderNotes varchar(400), OrderNumber varchar(50), CreatedBy int, CreateDate datetime, OrderStatus varchar(50), UserId int,
	UpdateDate datetime, DeleteFlag bit);

	Create Table #New_Order(New_Id int, VisitId int);

	Create Table #OrderTest(OrderId int, LabTestId int, IsParent bit, ParentTestId int, DeleteFlag bit, ResultStatus varchar(50),
	ResultBy bigint, ResultDate datetime, UserId int, StatusDate datetime);

	Create Table #New_OrderTest(New_Id int, OrderId int, LabTestId int);

	Create Table #TestResult(OrderId int, LabTestId int, TestName varchar(100), ParameterId int, ParameterName varchar(100), ResultValue decimal(10, 2), ResultText varchar(400),
	ResultOptionId int, ResultOption varchar(50), ResultUnit varchar(50), ResultUnitId int, ResultConfigId int, Undetectable bit, DetectionLimit decimal(10, 2),
	UserId int, DeleteFlag bit, CreateDate datetime, CreatedBy int, StatusDate datetime);

	Create Table #New_TestResult(New_Id int, OrderId int, LabTestId int, ParameterId int);

-- LabTests
	Insert Into #LabTest (Id, ReferenceId, Name, IsGroup, DepartmentId, Active, DeleteFlag)
	Select	LabTestID					Id
			,Case
				When LabName = 'CD4' Then 'CD4'
				When LabName = 'CD4 Percent' Then 'CD4_PERCENT'
				When LabName = 'Viral Load' Then 'VIRAL_LOAD'
				Else dbo.RemoveNonAlphaCharacters(LabName) + 
				Case row_number() Over(Partition by Labname Order by LabTestID) When 1 Then '' Else '-'+Convert(varchar,(row_number() Over(Partition by Labname Order by LabTestID))) End				 
			End							As ReferenceId
			,LabName					Name	
			,IsGroup =					
						Case
							When Exists (
									Select 1
									From Dtl_LabGroupItems G
									Where G.LabGroupTestId = O.LabTestId) Then 1
							Else 0
						End
			,LabDepartmentID			DepartmentId
			,~convert(bit, DeleteFlag)	Active
			,DeleteFlag
	From Mst_LabTest_Old O
	Where LabDepartmentID <> 8
		And Migrated = 0

--matching Parameters only
	Insert Into #TestParameter (Id, ReferenceId, ParameterName, LabTestId, DataType, OrdRank, DeleteFlag)
	Select	SubTestID																Id
			,dbo.RemoveNonAlphaCharacters(SubTestName)+ 
				Case row_number() Over(Partition by SubTestName Order by LabTestID) When 1 Then '' Else '-'+Convert(varchar,(row_number() Over(Partition by SubTestName Order by SubTestID))) End								ReferenceId
			,SubTestName															ParameterName
			,TestID																	LabTestId
			,DataType =																
						Case
							When T.DataType Is Not Null And
								T.DataType <> 'Group' Then T.DataType
							When Exists (
									Select 1
									From lnk_LabValue_Old V
									Where V.SubTestID = P.SubTestID) Then 'NUMERIC'
							When Exists (
									Select 1
									From lnk_parameterresult_Old R
									Where R.ParameterID = P.SubTestID) Then 'SELECTLIST'
							Else 'TEXT'
						End
			,row_number() Over (Partition By T.LabTestID Order By P.SubTestName)	OrdRank
			,P.DeleteFlag
	From lnk_TestParameter_Old P
	Inner Join Mst_LabTest_Old T On P.TestID = T.LabTestID
	Where LabDepartmentID <> 8
		And P.Migrated = 0 --And P.DeleteFlag = 0 And T.DeleteFlag = 0

-- Result Config
Insert Into #ParamConfig (Id, ParameterId, MinBoundary, MaxBoundary, MinNormalRange, MaxNormalRange, UnitId, DefaultUnit, DetectionLimit, DeleteFlag)
	Select	V.Id
			,P.SubTestID		ParameterId
			,V.MinBoundaryValue	MinBoundary
			,V.MaxBoundaryValue	MaxBoundary
			,V.MinNormalRange
			,V.MaxNormalRange
			,V.UnitID			UnitId
			,V.DefaultUnit
			,0.0				DetectionLimit
			,V.DeleteFlag
	From lnk_LabValue_Old V
	Inner Join lnk_TestParameter_Old P On P.SubTestID = V.SubTestID
	Where V.Migrated = 0
--Where V.DeleteFlag = 0 And P.DeleteFlag = 0



--Result Options
Insert Into #ResultOption (Id, ParameterId, Value, DeleteFlag)
	Select	R.ResultID		Id
			,R.ParameterID	ParameterId
			,R.Result		Value
			,0				DeleteFlag
	From lnk_parameterresult_Old R
	Inner Join lnk_TestParameter_Old P On P.SubTestID = R.ParameterID
	Where Result Is Not Null
		And R.Migrated = 0
--And P.DeleteFlag = 0

-- Group Labs[dbo].[lnk_LabValue_Old]
Insert Into #Group (LabGroupTestId, LabTestId)
	Select Distinct	G.LabGroupTestID	LabGroupTestId
					,G.LabTestID		LabTestId
	From Dtl_LabGroupItems G
	Inner Join Mst_LabTest_Old T On T.LabTestID = g.LabGroupTestID
	Inner Join Mst_LabTest_Old T1 On T1.LabTestID = G.LabTestID
	Where G.DeleteFlag = 0
		And T.DeleteFlag = 0


--Orders
Insert Into #Order(Id,Ptn_Pk,LocationId,VisitId,ModuleId,OrderedBy,OrderDate,PreClinicLabDate,ClinicalOrderNotes, OrderNumber,CreatedBy,CreateDate,
OrderStatus,UserId,UpdateDate,DeleteFlag)
select 
	LabId Id,
	Ptn_Pk,
	LocationId,
	VisitId,
	-1 ModuleId,
	OrderedByName OrderedBy,
	OrderedByDate OrderDate,
	PreClinicLabDate,
	Null ClinicalOrderNotes,
	LabNumber OrderNumber,
	UserID CreatedBy,
	CreateDate,
	OrderStatus = Case When ReportedByDate Is Null Then 'Pending' Else 'Complete' End,
	UserId,
	UpdateDate,
	DeleteFlag
 From ord_PatientLabOrder_Old O where  Migrated=0
 And Exists(Select 1 From dtl_PatientLabResults_Old R  inner join #LabTest T On T.Id=R.LabTestID Where R.LabID = O.LabID )

 -- OrderedTest

;With tests As(Select  Distinct	labid,
				labtestid
From dtl_PatientLabResults_Old Where Migrated=0
)
Insert Into #OrderTest(OrderId,LabTestId,IsParent,ParentTestId,DeleteFlag,ResultBy,ResultDate,UserId,ResultStatus,StatusDate)
Select T.LabId OrderId, 
LabTestId,
0 IsParent,
Null ParentTestId,
0 DeleteFlag,
nullif(ReportedByName,0) ResultBy,
ReportedByDate ResultDate,
O.UserId,
ResultStatus =Case When ReportedByDate Is Null Then 'Pending' Else 'Received' End,
O.CreateDate StatusDate
 From tests T Inner Join ord_PatientLabOrder_Old O On T.LabId = O.LabId  Inner JOin #Order LO on LO.Id = O.LabID
 inner  join #LabTest L On L.Id = T.LabTestId


 -- Test Results
		Insert Into #TestResult (
			OrderId,
			LabTestId,
			TestName,
			ParameterId,
			ParameterName,
			ResultValue,
			ResultText,
			ResultOptionId,
			ResultOption,
			ResultUnitId,
			ResultUnit,
			Undetectable,
			DetectionLimit,
			UserId,
			DeleteFlag,
			CreateDate,
			CreatedBy,
			StatusDate)
			Select	d.LabID OrderId,
					LabTestID LabTestId,
					TestName =(	Select	LabName	From Mst_LabTest_Old p		Where P.LabTestID = d.LabTestID),
					ParameterId,
					ParameterName =	(Select	SubTestName	From lnk_TestParameter_Old p Where P.SubTestID = d.ParameterID),
					TestResults As ResultValue,
					nullif(replace(replace(d.TestResults1, char(10), ''), char(13), ''), '') As ResultText,
					TestResultId ResultOptionId,
					ResultOption =(Select Result From lnk_parameterresult_Old lp Where lp.ResultId = TestResultId	And lp.ParameterId = D.ParameterId	),
					Units ResultUnitId,
					ResultUnit =(Select	Name From mst_decode U Where  CodeId = 30 And U.Id = Units	),
	
					0 As Undetectable,
					0.0 As DetectableLimit,
					d.UserID,
					0 As DeleteFlag,
					d.CreateDate,
					d.UserID CreatedBy,
					isnull(o.ReportedbyDate, O.OrderedbyDate) StatusDate
			From [dbo].[dtl_PatientLabResults_Old] d, [ord_PatientLabOrder_Old] o
			Where d.LabID = o.LabID
			And d.Migrated=0
			And d.parameterid <> 107
		And LabTestID In
		(
			Select	LabTestId	From Mst_LabTest_Old T	Where LabDepartmentId != 8
		) 
		Union All
		Select 		d.LabID OrderId,
					 3  LabTestId,	
						TestName = (Select LabName From Mst_LabTest_Old p Where P.LabTestID = d.LabTestID)		,
						d.ParameterId,
						ParameterName = (Select SubTestName From lnk_TestParameter_Old p Where P.SubTestID = 3),
						Null As ResultValue,
						Nullif(replace(replace (d.TestResults1, char(10), ''), char(13), ''),'')As ResultText,		
						TestResultId ResultOptionId,			
						ResultOption = (Select Result From lnk_parameterresult_Old lp Where lp.ResultId = TestResultId And lp.ParameterId = D.ParameterId) ,			
						Units ResultUnitId,
						ResultUnit= (Select	Name From mst_decode U Where  CodeId = 30 And U.Id = Units),				
						1 As Undetectable,
						TestResults As DetectableLimit,
						d.UserID,
						0 As DeleteFlag,
						d.CreateDate,
						d.UserID CreatedBy,
						Isnull(o.ReportedbyDate,o.OrderedbyDate) StatusDate
		From [dbo].[dtl_PatientLabResults_Old] d, [ord_PatientLabOrder_Old] o
		Where d.LabID = o.LabID   and d.parameterid = 107 
		And d.Migrated=0

	

	SET IDENTITY_INSERT [dbo].[mst_LabTestMaster] ON


	Insert Into mst_LabTestMaster (
		Id,
		ReferenceId,
		Name,
		IsGroup,
		DepartmentId,
		CreateDate,
		Active,
		DeleteFlag)
	Output INSERTED.Id,INSERTED.Id Into #New_LabTest(New_Id,Old_Id)
	Select	T.Id,
			T.ReferenceId,
			T.Name,
			T.IsGroup,
			T.DepartmentId,
			getdate(),
			T.Active,
			T.DeleteFlag
	From #LabTest T;

Print Convert(varchar(10),@@ROWCOUNT) + ' Lab Tests Migrated'

SET IDENTITY_INSERT [dbo].[mst_LabTestMaster] Off


Update O Set Migrated = 1 From Mst_LabTest_Old  O Inner Join #New_LabTest N On N.New_Id=O.LabTestID;

-- parameters



SET IDENTITY_INSERT [dbo].[Mst_LabTestParameter] ON


Insert Into Mst_LabTestParameter (
	Id,
	ReferenceId,
	ParameterName,
	LabTestId,
	DataType,
	OrdRank,
	UserId,
	CreateDate,
	DeleteFlag)
Output INSERTED.Id,INSERTED.LabTestId Into #New_TestParameter(New_Id,Old_TestId)
Select	P.Id,
		P.ReferenceId,
		ParameterName,
		LabTestId,
		DataType,
		OrdRank,
		1,
		getdate(),
		P.DeleteFlag
From #TestParameter P
Inner Join mst_LabTestMaster T On T.Id = P.LabTestId;

Print Convert(varchar(10),@@ROWCOUNT) + ' Lab Tests Parameters Migrated'

SET IDENTITY_INSERT [dbo].[Mst_LabTestParameter] Off

Update O Set Migrated = 1 From lnk_TestParameter_Old  O Inner Join #New_TestParameter N On N.New_Id=O.SubTestID;

update Mst_LabTestParameter set DeleteFlag= 1 Where ParameterName='ViralLoad Undetectable'
update mst_labtestparameter set LabTestId=(select Id from mst_labtestmaster where referenceid='CD4_PERCENT') Where parametername='CD4 Percent'
update mst_labtestparameter set ParameterName='CD4 Count', ReferenceId='CD4COUNT' Where parametername='CD4'

-- Config

SET IDENTITY_INSERT [dbo].dtl_LabTestParameterConfig On

Insert Into dtl_LabTestParameterConfig (
	Id,
	ParameterId,
	MinBoundary,
	MaxBoundary,
	MinNormalRange,
	MaxNormalRange,
	UnitId,
	DefaultUnit,
	DetectionLimit,
	DeleteFlag)
Output INSERTED.Id,INSERTED.ParameterId Into #New_ParamConfig(New_Id,Old_ParameterId)
Select	C.Id,
		C.ParameterId,
		C.MinBoundary,
		C.MaxBoundary,
		C.MinNormalRange,
		C.MaxNormalRange,
		C.UnitId,
		C.DefaultUnit,
		C.DetectionLimit,
		C.DeleteFlag
From #ParamConfig C
Inner Join Mst_LabTestParameter P On P.Id = C.ParameterId;

Print Convert(varchar(10),@@ROWCOUNT) + ' Parameters Configuration Migrated'


SET IDENTITY_INSERT [dbo].[dtl_LabTestParameterConfig] Off

Update O Set Migrated = 1 From lnk_LabValue_Old  O Inner Join #New_ParamConfig N On N.New_Id=O.ID and O.SubTestID = N.Old_ParameterId;

--options

SET IDENTITY_INSERT [dbo].[dtl_LabTestParameterResultOption] ON


Insert Into dtl_LabTestParameterResultOption (
	Id,
	ParameterId,
	Value,
	DeleteFlag)
Output INSERTED.Id, INSERTED.ParameterId Into #New_ResultOption(New_Id,Old_ParameterId)
Select	O.Id,
		o.ParameterId,
		o.Value,
		o.DeleteFlag
From #ResultOption O
Inner Join Mst_LabTestParameter P On O.ParameterId = P.Id;

Print Convert(varchar(10),@@ROWCOUNT) + ' Result Options'

SET IDENTITY_INSERT [dbo].[dtl_LabTestParameterResultOption] Off

Update O Set Migrated = 1 From lnk_parameterresult_Old  O Inner Join #New_ResultOption N On N.New_Id= O.ResultID and O.ParameterID = N.Old_ParameterId;

-- orders

SET IDENTITY_INSERT [dbo].ord_LabOrder ON


Insert Into ord_LabOrder (
	Id,
	Ptn_Pk,
	LocationId,
	VisitId,
	ModuleId,
	OrderedBy,
	OrderDate,
	PreClinicLabDate,
	ClinicalOrderNotes,
	OrderNumber,
	CreatedBy,
	CreateDate,
	OrderStatus,
	UserId,
	DeleteFlag,
	UpdateDate)
Output Inserted.Id, Inserted.VisitId Into #New_Order(New_Id,VisitId)
Select	Id,
		O.Ptn_Pk,
		O.LocationId,
		O.VisitId,
		O.ModuleId,
		O.OrderedBy,
		O.OrderDate,
		O.PreClinicLabDate,
		O.ClinicalOrderNotes,
		O.OrderNumber,
		O.CreatedBy,
		O.CreateDate,
		O.OrderStatus,
		O.UserId,
		Isnull(O.DeleteFlag,0),
		O.UpdateDate
From #Order O;

Print Convert(varchar(10),@@ROWCOUNT) + ' Lab Orders'

SET IDENTITY_INSERT [dbo].ord_LabOrder Off

Update O Set Migrated = 1 From ord_PatientLabOrder_Old  O Inner Join #New_Order N On N.New_Id= O.LabID and O.VisitId = N.VisitId;




--SET IDENTITY_INSERT [dbo].dtl_LabOrderTest On

Insert Into dtl_LabOrderTest (
	LabOrderId,
	LabTestId,
	TestNotes,
	IsParent,
	ParentTestId,
	DeleteFlag,
	ResultNotes,
	ResultBy,
	ResultDate,
	ResultStatus,
	UserId,
	StatusDate)
	Output INSERTED.Id, INSERTED.LabOrderId, INSERTED.LabTestId Into #New_OrderTest(new_id,OrderId,LabTestId)
	Select	T.OrderId,
			T.LabTestId,
			Null,
			T.IsParent,
			T.ParentTestId,
			T.DeleteFlag,
			Null,
			T.ResultBy,
			T.ResultDate,
			T.ResultStatus,
			T.UserId,
			T.StatusDate
	From #OrderTest T
	Inner Join ord_LabOrder O On O.Id = T.OrderId;

	Print Convert(varchar(10),@@ROWCOUNT) + ' Orders Tests Migrated'

--SET IDENTITY_INSERT [dbo].dtl_LabOrderTest Off

--SET IDENTITY_INSERT [dbo].dtl_LabOrderTestResult on

Insert Into dtl_LabOrderTestResult (
	LabOrderId,
	LabTestId,
	LabOrderTestId,
	ParameterId,
	ResultValue,
	ResultText,
	ResultOption,
	ResultOptionId,
	ResultUnit,
	ResultUnitId,
	ResultConfigId,
	Undetectable,
	DetectionLimit,
	UserId,
	DeleteFlag,
	CreateDate,
	CreatedBy,
	StatusDate)
Output Inserted.Id, Inserted.LabOrderId, Inserted.LabTestId,Inserted.ParameterId Into #New_TestResult(New_Id  ,OrderId , LabTestId , ParameterId )
Select	R.OrderId,
		R.LabTestId,
		T.Id,
		R.ParameterId,
		R.ResultValue,
		R.ResultText,
		R.ResultOption,
		R.ResultOptionId,
		r.ResultUnit,
		R.ResultUnitId,
		R.ResultConfigId,
		R.Undetectable,
		R.DetectionLimit,
		R.UserId,
		Isnull(R.DeleteFlag,0),
		Isnull(R.CreateDate,getdate()),
		R.CreatedBy,
		R.StatusDate
From #TestResult R
Inner Join dtl_LabOrderTest T On R.LabTestId = T.LabTestId
		And T.LabOrderId = R.OrderId
Inner Join #New_OrderTest OT On OT.OrderId = T.LabOrderId
		And OT.LabTestId = T.LabTestId
		And OT.New_Id = T.Id;

Print Convert(varchar(10),@@ROWCOUNT) + ' Test Results Migrated'


--SET IDENTITY_INSERT [dbo].dtl_LabOrderTestResult Off

Update O Set Migrated = 1 From dtl_PatientLabResults_Old  O Inner Join #New_TestResult N On O.LabId = N.OrderId And O.LabTestId = N.LabTestId And O.ParameterId = N.ParameterId



End


Go

If Not Exists(Select 1 From mst_LabTestMaster) Begin
	DECLARE @RC int
-- TODO: Set parameter values here.
	EXECUTE @RC = [dbo].[Laboratory_MigrateData] 
End
Go