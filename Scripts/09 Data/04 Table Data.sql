SET NOCOUNT ON;
Go
-- Update version
Update AppAdmin Set AppVer='Ver 1.0.0.3 Kenya HMIS', DBVer='Ver 1.0.0.3 Kenya HMIS', RelDate='20170801 00:00:00.000', VersionName = 'Kenya HMIS Ver 1.0.0.3'

Go

--update patientfacilityId

;With Patients as ( 
Select      Ptn_Pk,
    RegistrationDate,
    Row_number() Over(Partition By Datepart(Year,RegistrationDate) Order By Ptn_Pk) OrdValue
From mst_Patient 
)

Update P Set
PatientFacilityID =    Convert(varchar(4), Datepart(Year,P.RegistrationDate)) + '-' + Replicate('0', 5-Len(OrdValue)) + Convert(varchar, ordValue)
From Patients I   
Inner Join mst_Patient P On P.Ptn_Pk = I.Ptn_Pk And  P.PatientFacilityID Is Null;
Go
Update mst_Patient Set DeleteFlag = 0 Where DeleteFlag Is Null
Go
Delete from dtl_PatientTrackingCare Where Ptn_Pk = 0
Delete From dtl_PatientTrackingCare Where Ptn_Pk = 0
Go
update Mst_ItemMaster
set abbreviation = tbl.abbrv
from Mst_ItemMaster inner join 
 (
Select distinct ST2.drug_pk, 
    substring(
        (
            Select '/'+ST1.GenericAbbrevation  AS [text()]
            From (select c.drug_pk,d.GenericAbbrevation from mst_drug c inner join 
(select a.drug_pk,b.genericabbrevation from lnk_druggeneric a inner join mst_generic b on a.GenericID=b.GenericID 
where genericabbrevation is not null and genericabbrevation <>'') d
on c.Drug_pk = d.Drug_pk) ST1
            Where ST1.Drug_pk = ST2.Drug_pk
            ORDER BY ST1.Drug_pk
            For XML PATH ('')
        ), 2, 1000) [abbrv]
From (select c.drug_pk,d.GenericAbbrevation from mst_drug c inner join 
(select a.drug_pk,b.genericabbrevation from lnk_druggeneric a inner join mst_generic b on a.GenericID=b.GenericID 
where genericabbrevation is not null and genericabbrevation <>'') d
on c.Drug_pk = d.Drug_pk) ST2) tbl
on Mst_ItemMaster.item_pk = tbl.Drug_pk Where abbreviation Is Null
Go
update Mst_PreDefinedFields set controlid=4 where PDFName='CouncellingTopicId'
Go
/****** Object:  Index [IX_mst_Patient]    Script Date: 12/11/2014 16:22:36 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[mst_Patient]') AND name = N'IX_mst_Patient')
DROP INDEX [IX_mst_Patient] ON [dbo].[mst_Patient] WITH ( ONLINE = OFF )
GO


/****** Object:  Index [IX_mst_Patient]    Scrript Date: 12/11/2014 16:22:36 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_mst_Patient] ON [dbo].[mst_Patient] 
(
	[PatientFacilityID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

;
With md
As
(
Select	*
	,	row_number() Over (Partition By ModuleName Order By DeleteFlag Asc)	RI
From mst_module
)
Update M Set
		ModuleName = M.ModuleName + '_Deleted'
From mst_module M
Inner Join md On md.ModuleId = m.ModuleID
Where RI > 1
And M.DeleteFlag = 1
Go

;With SU As (Select	UserId
	,	StoreId
	,	StoreName = (Select Name From Mst_Store S Where S.Id = SU.StoreId)
	,	row_number() Over (Partition By convert(varchar, userid) + '-' + convert(varchar, StoreID) Order By StoreID)	rowindex
From Lnk_StoreUser SU)
Delete From SU Where RowIndex > 1;
Delete SU From Lnk_StoreUser SU Left Outer Join mst_store S On SU.StoreID = S.Id
Where S.Id Is Null;
Go
IF Not Exists (SELECT * FROM sys.key_constraints WHERE type = 'PK' AND parent_object_id = OBJECT_ID('dbo.Lnk_StoreUser') AND Name = 'PK_Lnk_StoreUser')
ALTER TABLE dbo.Lnk_StoreUser ADD CONSTRAINT
	PK_Lnk_StoreUser PRIMARY KEY CLUSTERED 
	(
	UserId,
	StoreId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
Go

If Not Exists (Select * From sys.columns Where Name = N'HEIIDNumber' And Object_ID = Object_id(N'mst_Patient'))    
Begin
  Alter table dbo.mst_Patient Add HEIIDNumber varchar(50) Null
End
Go

declare @Id int;
If Not Exists(Select 1 From mst_PatientIdentifier Where FieldName='HEIIDNumber') Begin
	Insert Into mst_PatientIdentifier(FieldName,FieldType,UserId,CreateDate,UpdateDate,UpdateFlag,Label, AutoPopulateNumber)
	Values('HEIIDNumber',1,1,getdate(),getdate(),0, 'HEINumber',0);
	Select @Id = scope_identity();
End
Else Begin
	Select @Id = ID From mst_PatientIdentifier Where FieldName='HEIIDNumber'
End

Update mst_PatientIdentifier Set Label='HEINumber' Where FieldName='HEIIDNumber'

If Not Exists(Select 1 From lnk_PatientModuleIdentifier Where ModuleID = 1 And FieldID=@Id) Begin
 Insert Into lnk_PatientModuleIdentifier(ModuleID,FieldID,UserID,CreateDate,UpdateDate,DeleteFlag,RequiredFlag)
 Values(1,@Id,1,getdate(),Null,0,0);
End
Go

--drop old tables
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mst_Generic_old]') AND type in (N'U'))
BEGIN
DROP TABLE [dbo].[mst_Generic_old]
End
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mst_DrugType_old]') AND type in (N'U'))
BEGIN
DROP TABLE [dbo].[mst_DrugType_old]
End
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lnk_DrugGeneric_old]') AND type in (N'U'))
BEGIN
DROP TABLE [dbo].[lnk_DrugGeneric_old]
End
GO 
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[lnk_DrugTypeGeneric_old]') AND type in (N'U'))
BEGIN
DROP TABLE [dbo].[lnk_DrugTypeGeneric_old]
End
GO 
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[mst_Drug_old]') AND type in (N'U'))
BEGIN
DROP TABLE [dbo].[mst_Drug_old]
End
GO
-- Fix missing regimen
If Not Exists(Select 1 From lnk_DrugGeneric Where Drug_pk =1147 and GenericID = 1) Begin
	Insert Into lnk_DrugGeneric(Drug_pk,GenericID,CreateDate,UserID,DeleteFlag)
	Values(1147,1,getdate(),1,0);
End
-- Import data to itemmaster
Update mst_module Set ModuleFlag = 1 Where ModuleName In('Billing','Pharmacy','Laboratory','Ward Admission');
Go
SET IDENTITY_INSERT mst_module ON
GO
If Not Exists(Select 1 From mst_module where ModuleName ='Billing')
Begin
	
	Insert into mst_module (ModuleID,ModuleName,DeleteFlag,UserID,CreateDate,UpdateDate,Status,UpdateFlag,Identifier,PharmacyFlag ,CanEnroll,DisplayName,ModuleFlag)
	values (210,'Billing',0,1,GETDATE(),null,2,0,1,0,0,'BILLING',1)
End
If Not Exists(Select 1 From mst_module where ModuleName ='Pharmacy')
Begin
	
	Insert into mst_module (ModuleID,ModuleName,DeleteFlag,UserID,CreateDate,UpdateDate,Status,UpdateFlag,Identifier,PharmacyFlag ,CanEnroll,DisplayName,ModuleFlag)
	values (204,'Pharmacy',0,1,GETDATE(),null,2,0,1,1,0,'Pharmacy',1)
End
Go
If Not Exists(Select 1 From mst_module where ModuleName ='Laboratory')
Begin
	
	Insert into mst_module (ModuleID,ModuleName,DeleteFlag,UserID,CreateDate,UpdateDate,Status,UpdateFlag,Identifier,PharmacyFlag ,CanEnroll,DisplayName,ModuleFlag)
	values (205,'Laboratory',0,1,GETDATE(),null,2,0,1,0,0,'Laboratory',1)
End
Go
If Not Exists(Select 1 From mst_module where ModuleName ='Ward Admission')
Begin
	
	Insert into mst_module (ModuleID,ModuleName,DeleteFlag,UserID,CreateDate,UpdateDate,Status,UpdateFlag,Identifier,PharmacyFlag ,CanEnroll,DisplayName,ModuleFlag)
	values (206,'Ward Admission',0,1,GETDATE(),null,2,0,1,0,0,'Ward Admission',1)
End
Go
SET IDENTITY_INSERT mst_module OFF
Go
/* Reports */
If Not Exists(Select 1 From [Mst_QueryBuilderCategory] where CategoryName ='Billing') Begin
	Insert Into [Mst_QueryBuilderCategory] (
		[CategoryName],
		[DeleteFlag],
		[UserId],
		[CreateDate])
	Values (
		'Billing',
		0,
		1,
		Getdate())
END
Go
If Not Exists(Select 1 From [Mst_QueryBuilderCategory] where CategoryName ='Patient Home Page') Begin
	Insert Into [Mst_QueryBuilderCategory] (
		[CategoryName],
		[DeleteFlag],
		[UserId],
		[CreateDate])
	Values (
		'Patient Home Page',
		0,
		1,
		Getdate())
END
Go
declare @reports Table (ReportName varchar(50), CategoryId int, ReportQuery varchar(8000));
Insert Into @reports(ReportName, CategoryId, ReportQuery)
SELECT 'Total Collections by Type' [ReportName],
	(Select Top 1 categoryID
		From Mst_QueryBuilderCategory
		Where CategoryName = 'Billing') [CategoryId],
	'Select T1.Item,    T1.transactionType,    
           Sum(T1.Amount) Amount  From (Select Case db.ItemType When 0 Then dc2.Name When 1 Then vt.VisitName        
           When 2 Then ''Laboratory'' When 3 Then ''Pharmacy'' End Item,
           (db.Quantity * db.SellingPrice) Amount,      ob.TransactionDate,      
           dc.Name transactionType    From dtl_Bill db      
           Join ord_bill ob On db.transactionID = ob.TransactionID      
           Join mst_Decode dc On dc.ID = ob.TransactionType      
           Left Join mst_Decode dc2 On dc2.ID = db.ItemId      
           Left Join lnk_TestParameter tp On tp.SubTestID = db.ItemId      
           Left Join mst_VisitType vt On vt.VisitTypeID = db.ItemId      
           Left Join Mst_Drug dg On dg.Drug_pk = db.ItemId      
           Join mst_Bill mb On mb.BillID = ob.BillID      
           Join mst_Patient p On p.Ptn_Pk = mb.ptn_pk   
           where ob.TransactionDate between CAST (@fromDate as Datetime) and CAST(@toDate as DateTime)   ) T1  
           Group By T1.Item,    T1.transactionType' [ReportQuery]
	
	UNION All
	SELECT 'Receipt Analysis' [ReportName],
	(Select Top 1 categoryID
		From Mst_QueryBuilderCategory
		Where CategoryName = 'Billing') [CategoryId],
	'Select	ReceiptNumber,
		convert(varchar(50), TransactionDate, 100) TransactionDate,
		(
			Select
				PaymentName
			From vw_BillPaymentType bt
			Where bt.ID = TransactionType
		)
		PaymentType,
		Amount,
		us.UserFirstName + '' '' + us.UserLastName CashierName,
		P.PatientFacilityID Patient#,
		Convert(varchar(50), Decryptbykey(P.FirstName))+ '' '' +Convert(varchar(50), Decryptbykey(P.LastName)) PatientName
From ord_bill ob
	Join mst_User us On us.UserID = ob.userID
Inner Join mst_Patient p on P.Ptn_Pk = ob.Ptn_PK
Where convert(datetime, floor(convert(float, ob.TransactionDate)))
	Between cast(@fromDate As datetime) And cast(@toDate As datetime) Order By ob.TransactionDate Asc;' [ReportQuery];
	
Insert Into [mst_QueryBuilderReports] (
	[ReportName],
	[CategoryId],
	[ReportQuery],
	[DeleteFlag],
	[UserId],
	[CreateDate])
SELECT [ReportName],
	[CategoryId],
	[ReportQuery],
	0 [DeleteFlag],
	1 [UserId],
	Getdate() [CreateDate] FROM @reports R  where not exists (Select * FROM [mst_QueryBuilderReports] qbr where qbr.ReportName=R.ReportName And qbr.CategoryId = R.CategoryId);
Update Q
	Set ReportQuery = R.ReportQuery
From mst_QueryBuilderReports Q Inner Join  @reports R On Q.ReportName = R.ReportName And Q.CategoryId = R.CategoryId

Go

Insert Into [MST_QueryBuilderParameters] (
	[ReportID],
	[ParameterName],
	[ParameterDataType])
	Select 
	[ReportID],
	[ParameterName],
	[ParameterDataType] FROM
	(
	Select Top 1	reportID [ReportID],
					'@FromDate'[ParameterName],
					'Date' [ParameterDataType]
	From mst_QueryBuilderReports
	Where ReportName = 'Total Collections by Type' 
	Union  
	Select Top 1	reportID,
				'@ToDate',
				'Date'
	From mst_QueryBuilderReports
	Where ReportName = 'Total Collections by Type'
	Union
		Select Top 1	reportID,
					'@FromDate',
					'Date'
	From mst_QueryBuilderReports
	Where ReportName = 'Receipt Analysis' 
	Union  
	Select Top 1	reportID,
				'@ToDate',
				'Date'
	From mst_QueryBuilderReports
	Where ReportName = 'Receipt Analysis') a where not exists (Select * FROM [MST_QueryBuilderParameters] qbp where qbp.ReportID=a.ReportID and qbp.ParameterName=a.ParameterName)


Go

If Not Exists(Select 1 From mst_decode where name ='Pharmacy Refill')
Begin
	
	Insert into mst_decode (Name,CodeId,SRNO,DeleteFlag,UserId,CreateDate,SystemId)
	values ('Pharmacy Refill',26,3,0,1,GETDATE(),1)
End
Go
IF NOT EXISTS(SELECT 1 FROM mst_decode WHERE Name = 'PREP')
	BEGIN
		INSERT INTO mst_decode(Name,CodeId,SRNO,DeleteFlag,UserId,CreateDate,SystemId)
		VALUES('PREP', 33, 5, 0, 1, GETDATE(), 1);
	END
GO
IF NOT EXISTS(SELECT 1 FROM mst_decode WHERE Name = 'HBV')
	BEGIN
		INSERT INTO mst_decode(Name,CodeId,SRNO,DeleteFlag,UserId,CreateDate,SystemId)
		VALUES('HBV', 33, 6, 0, 1, GETDATE(), 1);
	END
GO
 if not exists(select 1 from lnk_ControlBusinessRule where controlid=7 and businessruleid=1)
begin
insert into lnk_ControlBusinessRule(ControlId,BusinessRuleId,UserId,CreateDate)
values(7,1,1,GETDATE())
end
Go
Update D 
Set DeleteFlag =1 
From Mst_Decode D Inner Join Mst_Code C On C.CodeId=D.CodeID Where C.Name='PaymentType' 

Update Mst_Code Set DeleteFlag=1 Where Name='PaymentType' 
Go

If Not Exists(Select 1 From mst_Code Where Name = 'Billing Price Plans') Begin
    Insert Into Mst_Code(Name, DeleteFlag, UserId, CreateDate)
	Values('Billing Price Plans',0,1,getdate());
End
Go
declare @bppId int;
Select @bppId =	  CodeId From Mst_Code Where     Name = 'Billing Price Plans';
If Not Exists(Select 1 From Mst_Decode Where CodeId = @bppId and Name='Standard') Begin
Insert into mst_decode (Name,CodeId,SRNO,DeleteFlag,UserId,CreateDate,SystemId,Code)
values ('Standard',@bppId,1,0,1,GETDATE(),1,'STD')
End
Go
declare @stdPlan int;
Select @stdPlan = Id From Mst_Decode Where Name='Standard' And CodeId = (Select CodeId From Mst_Code Where Name = 'Billing Price Plans')

If  Exists(Select 1 From lnk_ItemCostConfiguration Where PricePlanId Is Null )Begin
	Update lnk_ItemCostConfiguration Set PricePlanId = @stdPlan Where PricePlanId Is Null
End
Go
If  Exists (Select * From sys.columns Where Name = N'PricePlanId' And Object_ID = Object_id(N'lnk_ItemCostConfiguration'))    
Begin
  Alter table dbo.lnk_ItemCostConfiguration Alter Column PricePlanId int Not Null
End
Go

If Not Exists(Select 1 From mst_Code Where Name = 'Form Category') Begin
    Insert Into Mst_Code(Name, DeleteFlag, UserId, CreateDate)
	Values('Form Category',0,1,getdate());
End
Go

Update Mst_Feature Set FeatureTypeId = Null Where FeatureName Like 'CareEnd_%'
Go
Insert Into mst_VisitType (
		VisitName
	,	DeleteFlag
	,	UserID
	,	CreateDate
	,	UpdateDate
	,	FeatureId
	,	Custom
	,	FormDescription)
Select	F.FeatureName
	,	F.DeleteFlag
	,	F.UserID
	,	F.CreateDate
	,	F.UpdateDate
	,	F.FeatureID
	,	1	Custom
	,	F.FeatureName
From mst_Feature F
Left Outer Join mst_VisitType V On F.FeatureID = V.FeatureId
Where FeatureName Like 'CareEnd_%'
	And V.FeatureId Is Null
Go
declare @formCategoryId int;
Select @formCategoryId =	  CodeId From Mst_Code Where     Name = 'Form Category';
Insert Into Mst_Decode(Name, CodeId, Code, DeleteFlag, UserId, SystemId, SrNo, CreateDate)
Select dx.Name
	,	dx.CodeId
	,	dx.Code
	,	dx.DeleteFlag
	,	dx.UserId
	,	dx.SystemId
	,	dx.SRNo
	,	getdate() CreateDate
From 
(Select 'Enrollment' Name,
		@formCategoryId CodeId,
		'ENROLLMENT' Code,
		0 DeleteFlag,	
		1 UserId,
		0 SystemId,
		1 SRNo
Union All
Select 'Consultation' Name,
		@formCategoryId CodeId,
		'CONSULTATION' Code,
		0 DeleteFlag,	
		1 UserId,
		0 SystemId,
		2 SRNo
Union All
Select 'Care Termination' Name,
		@formCategoryId CodeId,
		'CARE_END' Code,
		0 DeleteFlag,
		1 UserId,
		0 SystemId,
		3 SRNo
Union All
Select 'Registration' Name,
		@formCategoryId CodeId,
		'REGISTRATION' Code,
		0 DeleteFlag,	
		1 UserId,
		0 SystemId,
		4 SRNo	  
) dx  Where  Not Exists(Select 1 from mst_Decode where CodeId=@formCategoryId And Code= dx.Code);	  

Update  mst_VisitType Set CategoryId =
		Case
			When VisitName Like '%Enrollment%' Then (Select ID From mst_Decode Where Code = 'ENROLLMENT' And CodeID=@formCategoryId)
			When VisitName Like 'CareEnd_%' Then (Select ID From mst_Decode Where Code = 'CARE_END' And CodeID=@formCategoryId) 
			When VisitName Like '%Registration%' Then (Select ID From mst_Decode Where Code = 'REGISTRATION' And CodeID=@formCategoryId)  
			Else (Select ID From mst_Decode Where Code = 'CONSULTATION' And CodeID=@formCategoryId) 
		End	
Where CategoryId Is Null  
Go
update  mst_VisitType set VisitName='Pharmacy' where VisitName='Pharmacy Order'
Update V Set
		FeatureID = F.FeatureID
From mst_VisitType V
Inner Join mst_Feature F On F.FeatureName = V.VisitName
And VisitName In ('Initial and Follow up Visits', 'ART History', 'ART Therapy','Laboratory','Pharmacy')
And V.FeatureId Is Null	   And F.DeleteFlag = 0

Update V Set
		Custom = 1
From mst_VisitType V
Inner Join mst_Feature F On F.FeatureId = V.FeatureId
Where V.FeatureId > 1000 

If Not Exists(Select 1 From mst_Code Where Name = 'Feature Type') Begin
    Insert Into Mst_Code(Name, DeleteFlag, UserId, CreateDate)
	Values('Feature Type',0,1,getdate());
End
declare @featureTypeId int;
Select @featureTypeId =	  CodeId From Mst_Code Where     Name = 'Feature Type';
Insert Into Mst_Decode(Name, CodeId, Code, DeleteFlag, UserId, SystemId, SrNo, CreateDate)
Select dx.Name
	,	dx.CodeId
	,	dx.Code
	,	dx.DeleteFlag
	,	dx.UserId
	,	dx.SystemId
	,	dx.SRNo
	,	getdate() CreateDate
From 
(Select 'Clinical Form' Name,
		@featureTypeId CodeId,
		'CLINICAL_FORM' Code,
		0 DeleteFlag,	
		1 UserId,
		0 SystemId,
		1 SRNo
Union All
Select 'Facility Report' Name,
		@featureTypeId CodeId,
		'FACILITY_REPORT' Code,
		0 DeleteFlag,	
		1 UserId,
		0 SystemId,
		2 SRNo
Union All
Select 'Patient Report' Name,
		@featureTypeId CodeId,
		'PATIENT_REPORT' Code,
		0 DeleteFlag,	
		1 UserId,
		0 SystemId,
		3 SRNo
Union All
Select 'List' Name,
		@featureTypeId CodeId,
		'LIST' Code,
		0 DeleteFlag,
		1 UserId,
		0 SystemId,
		4 SRNo
Union All
Select 'Admin Actions' Name,
		@featureTypeId CodeId,
		'ADMIN_ACTION' Code,
		0 DeleteFlag,
	
		1 UserId,
		0 SystemId,
		5 SRNo
Union All
Select 'Patient Actions' Name,
		@featureTypeId CodeId,
		'PATIENT_ACTION' Code,
		0 DeleteFlag, 	
		1 UserId,
		0 SystemId,
		6 SRNo
Union All
Select 'Module Actions' Name,
		@featureTypeId CodeId,
		'MODULE_ACTION' Code,
		0 DeleteFlag, 	
		1 UserId,
		0 SystemId,
		7 SRNo
Union All
Select 'Module Report' Name,
		@featureTypeId CodeId,
		'MODULE_REPORT' Code,
		0 DeleteFlag,	
		1 UserId,
		0 SystemId,
		8 SRNo

) dx  Where  Not Exists(Select 1 from mst_Decode where CodeId=@featureTypeId And Code= dx.Code);
 
Update F Set
	FeatureTypeId = Case When ReportFlag = 1 AND F.FeatureName Like 'Patient%' Then (Select Id From mst_Decode where Code='PATIENT_REPORT' And CodeID=@featureTypeId)
		 When ReportFlag = 1 AND F.FeatureName Not Like 'Patient%' Then (Select Id From mst_Decode where Code='FACILITY_REPORT' And CodeID=@featureTypeId)
		 When AdminFlag = 1 And (C.Name Is Not Null OR B.Name Is Not Null) Then (Select Id From mst_Decode where Code='LIST' And CodeID=@featureTypeId)
		 When AdminFlag = 1 And C.Name Is  Null Then (Select Id From mst_Decode where Code='ADMIN_ACTION' And CodeID=@featureTypeId)
		 When ReportFlag = 0 And AdminFlag = 0 And V.FeatureId Is Not Null Then (Select Id From mst_Decode where Code='CLINICAL_FORM' And CodeID=@featureTypeId) 
		 When ReportFlag = 0 And AdminFlag = 0 And F.FeatureName Like 'CareEnd_%' Then (Select Id From mst_Decode where Code='CLINICAL_FORM' And CodeID=@featureTypeId) 
		 When ReportFlag = 0 And AdminFlag = 0 And V.FeatureId Is  Null Then (Select Id From mst_Decode where Code='PATIENT_ACTION' And CodeID=@featureTypeId) 
	End 
 from mst_Feature F Left Outer Join mst_VisitType V on F.FeatureID= V.FeatureId 
 Left Outer Join Mst_Code C On C.Name = F.FeatureName
 Left Outer Join Mst_BlueCode B On B.Name = F.FeatureName
 Where F.FeatureTypeId Is Null

Go
If Exists(Select 1 From Mst_code Where Name='Pharmacy Order Close Reason') Begin
	  Insert Into Mst_Code(Name, DeleteFlag, UserId, CreateDate)	Values('Pharmacy Order Close Reason',0,1,getdate());
End

declare @featureTypeId int;
Select @featureTypeId =	  CodeId From Mst_Code Where     Name = 'Pharmacy Order Close Reason';
Insert Into Mst_Decode(Name, CodeId, Code, DeleteFlag, UserId, SystemId, SrNo, CreateDate)
Select dx.Name
	,	dx.CodeId
	,	dx.Code
	,	dx.DeleteFlag
	,	dx.UserId
	,	dx.SystemId
	,	dx.SRNo
	,	getdate() CreateDate
From 
(Select 'Stock not available' Name,
		@featureTypeId CodeId,
		'STOCK-OUT' Code,
		0 DeleteFlag,	
		1 UserId,
		0 SystemId,
		1 SRNo
Union All
Select 'Drug substitution' Name,
		@featureTypeId CodeId,
		'DRUG-SUBS' Code,
		0 DeleteFlag,	
		1 UserId,
		0 SystemId,
		2 SRNo
Union All
Select 'Client unable to pay' Name,
		@featureTypeId CodeId,
		'UNABLE-TO-PAY' Code,
		0 DeleteFlag,	
		1 UserId,
		0 SystemId,
		3 SRNo
) dx  Where  Not Exists(Select 1 from mst_Decode where CodeId=@featureTypeId And Code= dx.Code);
Go
declare @featureTypeId int,@moduleId int;
Select @moduleId=ModuleId From mst_Module Where ModuleName='Billing'  
update mst_Feature Set ModuleId = @ModuleId where FeatureName like '%Billing%' And (ModuleId Is Null Or ModuleId=0)

--select * from mst_Feature
SET IDENTITY_INSERT mst_feature ON
GO
If Not Exists(Select 1 From mst_Feature where FeatureName ='Service Request')
Begin
	declare @FeatureID int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
	declare @featureTypeId int;
	Select Top 1 @featureTypeId = Id From mst_Decode Where Code='CLINICAL_FORM' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0) 
	Insert into mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,ReferenceId,CanLink, FeatureTypeId)
	values( @FeatureID 	,'Service Request'	,0	,0	   	,0 ,1	 	,GETDATE()	,0 	,0	 ,'SERVICE_REQUEST', 1	,@featureTypeId	 )
End	
Go
Update mst_Feature Set CanLink= 1 Where FeatureName='Service Request' 
Go
Update mst_Feature Set DeleteFlag= 1 Where FeatureName='X-Ray Request' 
Go

If Not Exists(Select 1 From mst_Feature where FeatureName ='Billables') Begin
declare @featureTypeId int,@moduleId int;
Select @moduleId=ModuleId From mst_Module Where ModuleName='Billing'
declare @FeatureID int;
Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)
Insert into mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,FeatureTypeId)
values (@FeatureID,'Billables',0,0,1,1,GETDATE(),0,@moduleId,@featureTypeId)
End
GO
If Not Exists(Select 1 From mst_Feature where FeatureName ='BillingType')
Begin
declare @featureTypeId int,@moduleId int;
Select @moduleId=ModuleId From mst_Module Where ModuleName='Billing'
declare @FeatureID int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)	
Insert into mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,ReferenceId,FeatureTYpeId)
values (@FeatureID,'BillingType',0,0,1,1,GETDATE(),0,@moduleId,'BILLING_TYPE',@featureTypeId)
End
GO
declare @featureTypeId int,@moduleId int;
Select @moduleId=ModuleId From mst_Module Where ModuleName='Billing'
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)  
declare @FeatureID int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
If Not Exists(Select 1 From mst_Feature where FeatureName ='Client Billing')
Begin
Insert into mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,ReferenceID,FeatureTypeId)
values (@FeatureID,'Client Billing',0,0,1,1,GETDATE(),0,@moduleId,'CLIENT_BILLING',@featureTypeId);
End
If Exists(Select 1 From mst_Feature where FeatureName ='Client Billing')
Begin	
Update dbo.mst_Feature Set ReferenceID= 'CLIENT_BILLING',FeatureTypeId=@featureTypeId Where FeatureName ='Client Billing'
End
GO
  declare @featureTypeId int,@moduleId int;
  declare @FeatureID int;
  	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)
If Not Exists(Select 1 From mst_Feature where FeatureName ='Billing')
Begin

Select @moduleId=ModuleId From mst_Module Where ModuleName='Billing'

	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)	 
Insert into mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,FeatureTypeId)
values (@FeatureID,'Billing',0,0,1,1,GETDATE(),0,@moduleId,@FeatureTypeId);
End
Update	 dbo.mst_Feature Set AdminFlag =1,FeatureTypeId=@featureTypeId Where  FeatureName ='Billing'
GO

declare @featureTypeId int,@moduleId int;
 declare @FeatureID int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)  

If Not Exists(Select 1 From mst_Feature where FeatureName ='Billing Configuration')
Begin

Select @moduleId=ModuleId From mst_Module Where ModuleName='Billing'

	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0) 
Insert into mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,FeatureTypeId)
values (@FeatureID,'Billing Configuration',0,0,1,1,GETDATE(),0,@moduleId,@featureTypeId)
End
Update	 dbo.mst_Feature Set AdminFlag =1,FeatureTypeId=@featureTypeId Where  FeatureName ='Billing Configuration'
Go

 declare @featureTypeId int,@moduleId int;
 declare @FeatureID int;
 Select Top 1 @featureTypeId = Id
	From mst_Decode	Where Code = 'MODULE_ACTION'	And CodeID = (	Select Top 1 CodeID		From mst_Code		Where name = 'Feature Type' And DeleteFlag = 0		)  
If Not Exists(Select 1 From mst_Feature where FeatureName ='Billing Reports') Begin
Select @moduleId=ModuleId From mst_Module Where ModuleName='Billing'
Select @FeatureID = max(FeatureID) + 1	From mst_Feature	Where FeatureID < 1000	
Insert into mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,FeatureTypeId)
values (@FeatureID,'Billing Reports',1,0,0,1,GETDATE(),0,@moduleId,@featureTypeId)
End
Update	 dbo.mst_Feature Set ReportFlag =0,FeatureTypeId=@featureTypeId Where  FeatureName ='Billing Reports'
Go

Delete From mst_feature where FeatureName= 'Billing Reversal Approval' And FeatureID != 179
Go
Update mst_Feature Set FeatureName='Billing Reversal Approval', ReferenceID='BILL_REVERSAL_APPROVAL' Where FeatureName='Billing Reversal'
 Go
declare @featureTypeId int,@moduleId int;
Select Top 1 @featureTypeId = Id From mst_Decode 
Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)
If Not Exists(Select 1 From mst_Feature where FeatureName= 'Billing Reversal Approval')
Begin
Select @moduleId=ModuleId From mst_Module Where ModuleName='Billing'
declare @FeatureID int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)  ;
Insert into mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,ReferenceID,FeatureTypeId)
values (@FeatureID,'Billing Reversal Approval',0,0,1,1,GETDATE(),0,@moduleId,'BILL_REVERSAL_APPROVAL',@featureTypeId)
End
Update	 dbo.mst_Feature Set AdminFlag =1,FeatureTypeId=@featureTypeId Where  FeatureName ='Billing Reversal Approval'
Go


If Exists(Select 1 From mst_Feature where FeatureName ='Consumables')
Begin
Update Mst_Feature Set FeatureName = 'Billing Quick Panel', ReferenceID='BILL_QUICK_PANEL' Where FeatureName ='Consumables'
End
Go

declare @featureTypeId int,@moduleId int, @FeatureId int;
Select Top 1 @featureTypeId = Id From mst_Decode 
Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)
If Not Exists(Select 1 From mst_Feature where FeatureName ='Billing Quick Panel')
Begin
--declare @featureTypeId int,@moduleId int;
Select @moduleId=ModuleId From mst_Module Where ModuleName='Billing'

	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0);
Insert Into dbo.mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,ReferenceId, FeatureTypeId)
Values(	@FeatureID,	'Billing Quick Panel',	0,	0,	1,	1,	Getdate(),	0,	@moduleId,'BILL_QUICK_PANEL',@featureTypeId);
End
Update	 dbo.mst_Feature Set AdminFlag =1,FeatureTypeId=@featureTypeId Where  FeatureName ='Billing Quick Panel'
Go

 declare @featureTypeId int,@moduleId int, @FeatureId int;
Select Top 1 @featureTypeId = Id From mst_Decode 
Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)

If Not Exists(Select 1 From mst_Feature where FeatureName ='BillingChequePayment')
Begin
--declare @featureTypeId int,@moduleId int;
Select @moduleId=ModuleId From mst_Module Where ModuleName='Billing'
--declare @FeatureID int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0) 
Insert Into dbo.mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,FeatureTypeId)
Values(@FeatureID,	'BillingChequePayment',	0,	0,	1,	1,	Getdate(),	0,@moduleId,@featureTypeId);
End
Update	 dbo.mst_Feature Set AdminFlag =1,FeatureTypeId=@featureTypeId Where  FeatureName ='BillingChequePayment'
Go

declare @featureTypeId int,@moduleId int, @FeatureId int;
Select Top 1 @featureTypeId = Id From mst_Decode 
Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)

If Not Exists(Select 1 From mst_Feature where FeatureName ='BillingInsurancePayment')
Begin

Select @moduleId=ModuleId From mst_Module Where ModuleName='Billing'

	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0) 
Insert Into dbo.mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,FeatureTypeId)
Values(	@FeatureID,'BillingInsurancePayment',0,0,	1,	1,	Getdate(),		@moduleId,		0,@featureTypeId);
End
Update	 dbo.mst_Feature Set AdminFlag =1,FeatureTypeId=@featureTypeId Where  FeatureName ='BillingInsurancePayment'
Go

declare @featureTypeId int,@moduleId int, @FeatureId int;
Select Top 1 @featureTypeId = Id From mst_Decode 
Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)

If Not Exists(Select 1 From mst_Feature where FeatureName ='Bill Write Off')
Begin

Select @moduleId=ModuleId From mst_Module Where ModuleName='Billing'
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)	;

	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
Insert Into dbo.mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,FeatureTypeId)
Values(@FeatureID,	'Bill Write Off',	0,0,1,1,Getdate(),	0,	@moduleId,@featureTypeId);
End
Update	 dbo.mst_Feature Set AdminFlag =1,FeatureTypeId=@featureTypeId Where  FeatureName ='Bill Write Off'
GO

If Not Exists(Select 1 From mst_Feature where ReferenceId ='RETURN_DEPOSIT')
Begin
declare @featureTypeId int,@moduleId int, @FeatureId int;
Select @moduleId=ModuleId From mst_Module Where ModuleName='Billing'
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)	;

	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
Insert Into dbo.mst_Feature (FeatureID,FeatureName,ReferenceId,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,FeatureTypeId)
Values(@FeatureID,	'Billing - Return deposit','RETURN_DEPOSIT',	0,0,1,1,Getdate(),	0,	@moduleId,@featureTypeId);
End
Update	 dbo.mst_Feature Set AdminFlag =1,FeatureTypeId=@featureTypeId Where  ReferenceId ='RETURN_DEPOSIT'
GO


declare @featureTypeId int,@moduleId int, @FeatureId int;
Select Top 1 @featureTypeId = Id From mst_Decode 
Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)
	
If Exists(Select 1 From mst_Feature where FeatureName ='BillingPaymentMethod')
Begin	
	--declare @featureTypeId int,@moduleId int;
	Select @moduleId=ModuleId From mst_Module Where ModuleName='Billing'
	Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)	 
	Update dbo.mst_Feature Set FeatureName= 'BillingReceivePayment',FeatureTypeId=@featureTypeId Where FeatureName ='BillingPaymentMethod'
End
Update	 dbo.mst_Feature Set AdminFlag =1,FeatureTypeId=@featureTypeId Where  FeatureName ='BillingPaymentMethod'
Go

declare @featureTypeId int,@moduleId int, @FeatureId int;
Select Top 1 @featureTypeId = Id From mst_Decode 
Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)
If Not Exists(Select 1 From mst_Feature where FeatureName ='BillingReceivePayment')
Begin	 	
	Select @moduleId=ModuleId From mst_Module Where ModuleName='Billing'  
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
	Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)  
	Insert Into dbo.mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,FeatureTypeId)
	Values(@FeatureID,	'BillingReceivePayment',	0,0,1,1,Getdate(),	0,	@moduleId,@featureTypeId);
End
Update	 dbo.mst_Feature Set AdminFlag =1,FeatureTypeId=@featureTypeId Where  FeatureName ='BillingReceivePayment'
GO   

declare @featureTypeId int,@moduleId int, @FeatureId int;
Select Top 1 @featureTypeId = Id From mst_Decode 
Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)

If Not Exists(Select 1 From mst_Feature where FeatureName ='Billing Credit Knock Off')
Begin
	
	Select @moduleId=ModuleId From mst_Module Where ModuleName='Billing'
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000		
	

	Insert Into dbo.mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,ReferenceID,FeatureTypeId)
	Values(	@FeatureID ,'Billing Credit Knock Off',0,0,	1,	1,	Getdate(),		0,		@moduleId,'BILLING_KNOCKOFF',@featureTypeId);
End
Update	 dbo.mst_Feature Set AdminFlag =1,FeatureTypeId=@featureTypeId Where  FeatureName ='Billing Credit Knock Off'
Go

If Not Exists(Select 1 From mst_Feature where FeatureName ='Waiting List')
Begin
declare @featureTypeId int;
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='LIST' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)  
declare @FeatureID int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
Insert into mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,FeatureTypeId)
values (@FeatureID,'Waiting List',0,0,1,1,GETDATE(),0,0,@featureTypeId)
End
GO


If Not Exists(Select 1 From mst_Feature where FeatureName ='Consumables Issuance')
Begin
declare @featureTypeId int;
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='PATIENT_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0) 
declare @FeatureID int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
	Insert Into dbo.mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,ReferenceId,FeatureTypeId,CanLink)
	values(	@FeatureID ,'Consumables Issuance',0 ,0 ,0 ,1 ,Getdate(),1 ,0,'CONSUMABLES_ISSUANCE',@featureTypeId,1 );
End
Go
Update mst_Feature Set CanLink= 1 Where FeatureName='Consumables Issuance' 
Go
If Not Exists (Select 1	From dbo.mst_Feature	Where FeatureName = 'Patient Ward Admission')
Begin
declare @featureTypeId int,@moduleId int;
Select Top 1 @moduleId = ModuleId From mst_module Where ModuleName='Ward Admission'
declare @FeatureID int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)
	Insert Into dbo.mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,ReferenceId,FeatureTypeId)
	Values(@FeatureID,	'Patient Ward Admission',	0,0,1,1,Getdate(),	0,	@moduleId,'WARD_ADMIT',@featureTypeId);
End
Go
If Not Exists (Select 1	From dbo.mst_Feature	Where FeatureName = 'Update Patient Ward Admission')
Begin
declare @featureTypeId int,@moduleId int;
Select Top 1 @moduleId = ModuleId From mst_module Where ModuleName='Ward Admission'
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)
declare @FeatureID int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
	Insert Into dbo.mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,ReferenceId,FeatureTypeId)
	Values(@FeatureID,	'Update Patient Ward Admission',	0,0,1,1,Getdate(),	0,	@moduleId,'WARD_ADMISSION_MODIFY',@featureTypeId);
End
Go
If Not Exists (Select 1	From dbo.mst_Feature	Where FeatureName = 'Discharge from ward')
Begin
declare @featureTypeId int,@moduleId int;
Select Top 1 @moduleId = ModuleId From mst_module Where ModuleName='Ward Admission'
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0) 
declare @FeatureID int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
	Insert Into dbo.mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,ReferenceId,FeatureTypeId)
	Values(@FeatureID,	'Discharge from ward',	0,0,1,1,Getdate(),	0,	@moduleId,'WARD_DISCHARGE',@featureTypeId);
End
Go

If Not Exists(Select 1 From mst_Feature where FeatureName ='Manage Clinical Services')
Begin
declare @FeatureID int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
	declare @featureTypeId int;
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='ADMIN_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)

Insert Into dbo.mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,ReferenceID,FeatureTypeId)
Values(	@FeatureID ,'Manage Clinical Services',0,0,	0,	1,	Getdate(),		0,		0,'MANAGE_CLINICALSERIVICES',@featureTypeId);
End
Go


If Not Exists(Select 1 From mst_Feature where FeatureName ='Laboratory Test Results')
Begin
declare @FeatureID int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
	declare @featureTypeId int, @moduleId int;
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)
  Select Top 1 @moduleId = ModuleId From mst_module Where ModuleName='Laboratory'
Insert Into dbo.mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,ReferenceID,FeatureTypeId)
Values(	@FeatureID ,'Laboratory Test Results',0,0,	0,	1,	Getdate(),		0,		@moduleId,'LABORATORY_RESULT',@featureTypeId);
End
Go
If Not Exists(Select 1 From mst_Feature where FeatureName ='Laboratory Module')
Begin
declare @FeatureID int,@moduleId int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
	declare @featureTypeId int;
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)
 Select Top 1 @moduleId = ModuleId From mst_module Where ModuleName='Laboratory'
Insert Into dbo.mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,ReferenceID,FeatureTypeId)
Values(	@FeatureID ,'Laboratory Module',0,0,	0,	1,	Getdate(),		0,		@moduleId,'LABORATORY_MODULE',@featureTypeId);
End
Go
If Not Exists (Select 1	From dbo.mst_Feature	Where FeatureName = 'Admission Wards')
Begin
declare @featureTypeId int,@moduleId int;
 Select Top 1 @moduleId = ModuleId From mst_module Where ModuleName='Ward Admission'
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)
	 declare @FeatureID int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
	Insert Into dbo.mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,FeatureTypeId)
	Values(@FeatureID,	'Admission Wards',	0,0,1,1,Getdate(),	0,	@moduleId,@featureTypeId);
End

Go
If Not Exists(Select 1 From mst_Feature where FeatureName ='Setup Database Backup')
Begin
declare @featureTypeId int;
declare @FeatureID int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='ADMIN_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)  
Insert into mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,ReferenceID,FeatureTypeId)
values (@FeatureID,'Setup Database Backup',0,0,1,1,GETDATE(),0,0,'SETUP_DATABASE_BACKUP',@featureTypeId);
End
GO
If Not Exists(Select 1 From mst_Feature where FeatureName ='Restore Database')
Begin
declare @featureTypeId int;
declare @FeatureID int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='ADMIN_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)  
Insert into mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,ReferenceID,FeatureTypeId)
values (@FeatureID,'Restore Database',0,0,1,1,GETDATE(),0,0,'RESTORE_DATABASE',@featureTypeId);
End
GO
 Update mst_feature Set ReferenceId=Null,DeleteFlag= 1 Where FeatureName = 'Pharmacy' And DeleteFlag = 0 And SystemId=2	
 Go
If Not Exists(Select 1 From mst_Feature where FeatureName ='Configure Custom Lists')
Begin
declare @featureTypeId int;
declare @FeatureID int;
	Select   @FeatureID = max(FeatureID)+1 From mst_Feature Where FeatureID < 1000
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='ADMIN_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)  
Insert into mst_Feature (FeatureID,FeatureName,ReportFlag,DeleteFlag,AdminFlag,UserID,CreateDate,SystemId,ModuleId,ReferenceID,FeatureTypeId)
values (@FeatureID,'Configure Custom Lists',0,0,1,1,GETDATE(),0,0,'CONFIG_CUSTOM_LIST',@featureTypeId);
End
GO
If Exists(Select 1 From mst_Feature where FeatureName ='Configure Custom Lists')
Begin
declare @featureTypeId int;
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='ADMIN_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)
Update dbo.mst_Feature Set ReferenceID= 'CONFIG_CUSTOM_LIST', FeatureTypeId=@featureTypeId Where FeatureName ='Configure Custom Lists'
End
SET IDENTITY_INSERT mst_feature OFF
GO
Update mst_feature Set ReferenceId='MANAGE_ADMISSION_WARDS' Where FeatureName = 'Admission Wards'		And ReferenceId Is Null
Update mst_feature Set ReferenceId='WARD_DISCHARGE' Where FeatureName = 'Discharge from ward'		  And ReferenceId Is Null
Update mst_feature Set ReferenceId='WARD_ADMIT' Where FeatureName = 'Patient Ward Admission'			 And ReferenceId Is Null
Update mst_feature Set ReferenceId='WARD_ADMISSION_MODIFY' Where FeatureName = 'Update Patient Ward Admission'			And ReferenceId Is Null
Update mst_feature Set ReferenceId='MANAGE_LABORATORY' Where FeatureName = 'Laboratory Configuration'	   And ReferenceId Is Null
Update mst_feature Set ReferenceId='BILLABLES_MANAGE' Where FeatureName = 'Billables'		And ReferenceId Is Null
Update mst_feature Set ReferenceId='BILLING_TYPE' Where FeatureName = 'BillingType'		  And ReferenceId Is Null
Update mst_feature Set ReferenceId='BILLING_MODULE' Where FeatureName = 'Billing'	   And ReferenceId Is Null
Update mst_feature Set ReferenceId='BILLING_CONFIGURATION' Where FeatureName = 'Billing Configuration'	 And ReferenceId Is Null
Update mst_feature Set ReferenceId='BILLING_REPORTS' Where FeatureName = 'Billing Reports'		   And ReferenceId Is Null
Update mst_feature Set ReferenceId='BILL_REVERSAL_APPROVAL' Where FeatureName = 'Billing Reversal Approval'	 And ReferenceId Is Null
Update mst_feature Set ReferenceId='BILL_QUICK_PANEL' Where FeatureName = 'Billing Quick Panel'		  And ReferenceId Is Null
Update mst_feature Set ReferenceId='BILL_CHEQUE_PAYMENT' Where FeatureName = 'BillingChequePayment'		   And ReferenceId Is Null
Update mst_feature Set ReferenceId='BILL_INSURANCE_PAYMENT' Where FeatureName = 'BillingInsurancePayment'	 And ReferenceId Is Null
Update mst_feature Set ReferenceId='BILL_RECEIVE_PAYMENT' Where FeatureName = 'BillingReceivePayment'			 And ReferenceId Is Null
Update mst_feature Set ReferenceId='BILL_WRITE_OFF' Where FeatureName = 'Bill Write Off'			   And ReferenceId Is Null
Update mst_feature Set ReferenceId='WAITING_LIST_MANAGE' Where FeatureName = 'Waiting List'						And ReferenceId Is Null
Update mst_feature Set ReferenceId='CLIENT_BILLING' Where FeatureName = 'Client Billing'					 And ReferenceId Is Null
Update mst_feature Set ReferenceId='CONSUMABLES_ISSUANCE' Where FeatureName = 'Consumables Issuance'			And ReferenceId Is Null
Update mst_feature Set ReferenceId='LABORATORY' Where FeatureName = 'Laboratory' And DeleteFlag = 0				 And ReferenceId Is Null
Update mst_feature Set ReferenceId=Null Where FeatureName = 'Laboratory' And DeleteFlag = 1					   
Update mst_feature Set ReferenceId='PHARMACY' Where FeatureName = 'Pharmacy'			   And ReferenceId Is Null
Update mst_feature Set ReferenceId='ART_HISTORY' Where FeatureName = 'ART History'			And ReferenceId Is Null
Update mst_feature Set ReferenceId='ART_THERAPY' Where FeatureName = 'ART Therapy'		  And ReferenceId Is Null
Update mst_feature Set ReferenceId='CCC_INITIAL_FOLLOWUP' Where FeatureName = 'Initial and Follow up Visits' 	   And ReferenceId Is Null
Update mst_feature Set ReferenceId='CCC_CAREEND' Where FeatureName = 'CareEnd_CCC Patient Card MoH 257' 	   And ReferenceId Is Null
Update mst_feature Set ReferenceId='PMTCT_CAREEND' Where FeatureName = 'CareEnd_PMTCT' 	   And ReferenceId Is Null
Update mst_feature Set ReferenceId='ICF' Where FeatureName = 'Intensive Case Finding'  			  And ReferenceId Is Null
Update mst_feature Set ReferenceId='PATIENT_REGISTRATION' Where FeatureId = 126		  And ReferenceId Is Null
Update mst_feature Set ReferenceId='CONFIG_CUSTOM_FIELD' Where FeatureName = 'Configure Custom Fields'		  And ReferenceId Is Null
Go
If Not Exists(Select 1 From mst_Decode where CodeID=24 And Name='To be filled in by the clinician')
Begin
	Insert Into mst_Decode(Name,CodeID,DeleteFlag,SystemId ,CreateDate)
	Values('To be filled in by the clinician',24,0,1,Getdate())
End
Go
Update mst_Control Set ReferenceId='TXT_SINGLE_LINE' Where ControlId=1
Update mst_Control Set ReferenceId='DECIMAL_TXT' Where ControlId=2
Update mst_Control Set ReferenceId='NUMERIC_TXT' Where ControlId=3
Update mst_Control Set ReferenceId='SELECT_LIST' ,LookupTable = 'Mst_ModDecode' Where ControlId=4
Update mst_Control Set ReferenceId='DATE_TXT' Where ControlId=5
Update mst_Control Set ReferenceId='YES_NO', LookupTable = 'Mst_YesNo' Where ControlId=6
Update mst_Control Set ReferenceId='CHECK_BOX' Where ControlId=7
Update mst_Control Set ReferenceId='TXT_MULTILINE' Where ControlId=8
Update mst_Control Set ReferenceId='SELECT_LIST_MULTI' ,LookupTable = 'Mst_ModDecode' Where ControlId=9
Update mst_Control Set ReferenceId='REGIMEN' Where ControlId=10
Update mst_Control Set ReferenceId='DRUG_FIELD' Where ControlId=11
Update mst_Control Set ReferenceId='LAB_FIELD', DeleteFlag =1 Where ControlId=12
Update mst_Control Set ReferenceId='PLACE_HOLDER' Where ControlId=13
Update mst_Control Set ReferenceId='TIME_FIELD' Where ControlId=14
Update mst_Control Set ReferenceId='DISEASE_SYMPTOM' Where ControlId=15
Update mst_Control Set ReferenceId='ICD10_FIELD' Where ControlId=16
Update mst_Control Set ReferenceId='SYS_AUTOINCREMENT_FIELD' Where ControlId=17
Update mst_Control Set ReferenceId='BMI_FIELD' Where ControlId=18
Update mst_Control Set LookupTable = 'Mst_ModDecode' Where ReferenceId='SELECTLIST_TEXTBOX'
Go
Update mst_BusinessRule set ReferenceId ='REQUIRED_FIELD' Where Id=1
Update mst_BusinessRule set ReferenceId ='MAX_VALUE' Where Id=2
Update mst_BusinessRule set ReferenceId ='MIN_VALUE' Where Id=3
Update mst_BusinessRule set ReferenceId ='NUMBER_DECIMAL' Where Id=4
Update mst_BusinessRule set ReferenceId ='MULTI_LINE' Where Id=5
Update mst_BusinessRule set ReferenceId ='SINGLE_LINE' Where Id=6
Update mst_BusinessRule set ReferenceId ='DATE_GT_TODAY' Where Id=7
Update mst_BusinessRule set ReferenceId ='DATE_LT_TODAY' Where Id=8
Update mst_BusinessRule set ReferenceId ='DATE_GT_DOB' Where Id=9
Update mst_BusinessRule set ReferenceId ='REGIMEN' Where Id=10
Update mst_BusinessRule set ReferenceId ='FILTERED_DRUG' Where Id=11
Update mst_BusinessRule set ReferenceId ='LABTEST' Where Id=12
Update mst_BusinessRule set ReferenceId ='DATA_QC' Where Id=13
Update mst_BusinessRule set ReferenceId ='ACTIVE_MALE' Where Id=14
Update mst_BusinessRule set ReferenceId ='ACTIVE_FEMALE' Where Id=15
Update mst_BusinessRule set ReferenceId ='ACTIVE_AGE_RANGE_YEARS' Where Id=16
Update mst_BusinessRule set ReferenceId ='AUTO_POPULATE' Where Id=17
Update mst_BusinessRule set ReferenceId ='MULTISELECT_1_DATE_FIELD' Where Id=18
Update mst_BusinessRule set ReferenceId ='MULTISELECT_2_DATE_FIELD' Where Id=19
Update mst_BusinessRule set ReferenceId ='MULTISELECT_1_NUMERIC_FIELD' Where Id=20
Update mst_BusinessRule set ReferenceId ='MMMYYYY_FORMAT' Where Id=21
Update mst_BusinessRule set ReferenceId ='24 HOUR_FORMAT' Where Id=22
Update mst_BusinessRule set ReferenceId ='12 HOUR_FORMAT' Where Id=23
Update mst_BusinessRule set ReferenceId ='SYSTEM_TIME' Where Id=24
Update mst_BusinessRule set ReferenceId ='DISPLAY_WIDTH_FULL' Where Id=25
Update mst_BusinessRule set ReferenceId ='MAX_NORMAL' Where Id=26
Update mst_BusinessRule set ReferenceId ='MIN_NORMAL' Where Id=27
Go

If Not Exists(Select 1 From mst_control where ReferenceId='TXT_MULTILINE_LARGE' Or Name ='Text MultiLine Large') Begin

	Insert Into mst_control(Name,DataType,DeleteFlag,UserId,CreateDate,UpdateDate,ReferenceId)
	Values('Text MultiLine Large','varchar(4000)',0,1,getdate(),null,'TXT_MULTILINE_LARGE')
End
Go
Update mst_control set DataType = 'varchar(4000)' Where ReferenceId= 'TXT_MULTILINE'
Update mst_control set DataType = 'varchar(250)' Where ReferenceId= 'TXT_SINGLE_LINE'
Go
If Not Exists(Select 1 From mst_control where ReferenceId='INSTRUCTIONS' Or Name='Instruction Panel') Begin

	Insert Into mst_control(Name,DataType,DeleteFlag,UserId,CreateDate,UpdateDate,ReferenceId)
	Values('Instruction Panel','varchar(4000)',0,1,getdate(),null,'INSTRUCTIONS')
End
Go
If Not Exists(Select 1 From mst_control where ReferenceId='NEXT_APPOINTMENT' Or Name='Next Appointment') Begin

	Insert Into mst_control(Name,DataType,DeleteFlag,UserId,CreateDate,UpdateDate,ReferenceId)
	Values('Next Appointment','datetime',0,1,getdate(),null,'NEXT_APPOINTMENT')
End
Else Begin
	  Update mst_control set DataType='datetime' where ReferenceId ='NEXT_APPOINTMENT'
End
Go
If Not Exists(Select 1 From mst_control where ReferenceId='SELECTLIST_TEXTBOX' Or Name='Select List TextBox') Begin

	Insert Into mst_control(Name,DataType,DeleteFlag,UserId,CreateDate,UpdateDate,ReferenceId, LookupTable  )
	Values('Select List TextBox','int',0,1,getdate(),null,'SELECTLIST_TEXTBOX','Mst_ModDecode')
End
Go
If Not Exists(Select 1 From mst_control where ReferenceId='ENCRYPT_FIELD' Or Name='Encrypted Field') Begin

	Insert Into mst_control(Name,DataType,DeleteFlag,UserId,CreateDate,UpdateDate,ReferenceId, LookupTable  )
	Values('Encrypted Field','varbinary(max)',0,1,getdate(),null,'ENCRYPT_FIELD',null)
End
Go
SET IDENTITY_INSERT mst_control OFF
GO
If Not Exists(Select 1 From Mst_PreDefinedFields where PDFName='SYS_ClinicalNotes') Begin
	DECLARE @txtLargeId int;
	Select @txtLargeId= ControlId From mst_control where ReferenceId='TXT_MULTILINE_LARGE'
	Insert Into Mst_PreDefinedFields(PDFName,PDFTableName,ControlId,BindTable,UserId,CreateDate,UpdateDate,ModuleId,CategoryId,BindField,PatientRegistration)
	Values('SYS_ClinicalNotes','dtl_PatientClinicalNotes',@txtLargeId,Null,1,getdate(),Null,0,Null,'ClinicalNotes',Null);
End
Go
If Not Exists(Select 1 From Mst_PreDefinedFields where PDFName='SYS_SP02') Begin
	DECLARE @decimalId int;
	Select @decimalId= ControlId From mst_control where ReferenceId='DECIMAL_TXT'
	Insert Into Mst_PreDefinedFields(PDFName,PDFTableName,ControlId,BindTable,UserId,CreateDate,UpdateDate,ModuleId,CategoryId,BindField,PatientRegistration)
	Values('SYS_SP02','dtl_PatientVitals',@decimalId,Null,1,getdate(),Null,0,Null,'SP02',Null);
End
Go
--create billing types im mst_code and mst_decode

SET IDENTITY_INSERT mst_Code ON
GO

If Not Exists (Select 1	From mst_Code	Where Name = 'Waiting List') 
Begin
Insert Into mst_Code (
	CodeID,
	Name,
	DeleteFlag,
	UserID,
	CreateDate)
Values (
	214,
	'Waiting List',
	0,
	1,
	Getdate())
End

GO

SET IDENTITY_INSERT mst_Code OFF
GO
If Not Exists(Select 1 From Mst_DeCode Where CodeId=214 And Name='Cashier') Begin Insert Into Mst_Decode(Name,code,CodeId,DeleteFlag,CreateDate,UserId,SystemId,SRNO) values('Cashier',6,214,0,getdate(),1,0,1) End
If Not Exists(Select 1 From Mst_DeCode Where CodeId=214 And Name='Consultation') Begin Insert Into Mst_Decode(Name,code,CodeId,DeleteFlag,CreateDate,UserId,SystemId,SRNO) values('Consultation',1,214,0,getdate(),1,0,4) End
If Not Exists(Select 1 From Mst_DeCode Where CodeId=214 And Name='Laboratory') Begin Insert Into Mst_Decode(Name,code,CodeId,DeleteFlag,CreateDate,UserId,SystemId,SRNO) values('Laboratory',3,214,0,getdate(),1,0,14) End
If Not Exists(Select 1 From Mst_DeCode Where CodeId=214 And Name='Pharmacy') Begin Insert Into Mst_Decode(Name,code,CodeId,DeleteFlag,CreateDate,UserId,SystemId,SRNO) values('Pharmacy',4,214,0,getdate(),1,0,19) End
If Not Exists(Select 1 From Mst_DeCode Where CodeId=214 And Name='Triage') Begin Insert Into Mst_Decode(Name,code,CodeId,DeleteFlag,CreateDate,UserId,SystemId,SRNO) values('Triage',5,214,0,getdate(),1,0,26) End
If Not Exists(Select 1 From Mst_DeCode Where CodeId=214 And Name='Clinical Service') Begin Insert Into Mst_Decode(Name,code,CodeId,DeleteFlag,CreateDate,UserId,SystemId,SRNO) values('Clinical Service',5,214,0,getdate(),1,0,26) End
Go
Update L Set
	ListID =D.ID
From mst_Decode D 
Inner Join dtl_WaitingList L On D.Code = L.ListID
Where D.CodeID =214;
Go


If Not Exists (Select 1	From mst_Code	Where Name = 'Payment Exemption') 
Begin
	Insert Into mst_Code (		
		Name,
		DeleteFlag,
		UserID,
		CreateDate)
	Values (		
		'Payment Exemption',
		0,
		1,
		Getdate());
End
declare @ID int;
Select @ID = CodeId From Mst_Code Where Name = 'Payment Exemption'

Delete D From Mst_Decode D Inner Join
(Select D.ID ReasonID, D.Name ReasonText, row_number() Over (Partition By D.Name Order By D.ID) R From Mst_Decode D
	Inner Join Mst_Code C On C.CodeID=D.CodeID
	Where C.Name = 'Payment Exemption'
	)Dups On Dups.ReasonID = D.ID And Dups.R > 1

If Not Exists(Select 1 From mst_Decode Where Name='Payment Exemption' And CodeID = @ID)
Begin
	Insert Into mst_Decode (Name,CodeID,SRNo,DeleteFlag,UserID,CreateDate,SystemId)
	Values ('Under 5',@ID,1,0,1,GETDATE(),0)
End
If Not Exists(Select 1 From mst_Decode Where Name='Payment Exemption' And CodeID = @ID)
Begin
	Insert Into mst_Decode (Name,CodeID,SRNo,DeleteFlag,UserID,CreateDate,SystemId)
	Values ('Maternity',@ID,1,0,1,GETDATE(),0)
End
If Not Exists(Select 1 From mst_Decode Where Name='Payment Exemption' And CodeID = @ID)
Begin
	Insert Into mst_Decode (Name,CodeID,SRNo,DeleteFlag,UserID,CreateDate,SystemId)
	Values ('Prisoner',@ID,1,0,1,GETDATE(),0)
End
GO
Declare @moduleId int;
declare @featureTypeId int;
Select Top 1 @featureTypeId = Id From mst_Decode Where Code='MODULE_ACTION' And CodeID=(Select Top 1 CodeID From mst_Code where name='Feature Type' And DeleteFlag=0)
 Select Top 1 @moduleId = ModuleId From mst_module Where ModuleName='Ward Admission'
update mst_Feature Set ModuleId = @ModuleId, FeatureTypeId=@featureTypeId where FeatureName  In('Patient Ward Admission','Update Patient Ward Admission','Discharge from ward','Admission Wards') 
 Go
--Add admin to be a user for billing and billing configuration
INSERT INTO [lnk_GroupFeatures]([GroupID],[FeatureID],[FunctionID],[CreateDate])
    SELECT [GroupID],[FeatureID],[FunctionID],[CreateDate] FROM
    (
     select 1[GroupID],[FeatureID],1[FunctionID],GETDATE()[CreateDate]   from mst_Feature where FeatureName in (
'Billables'
,'BillingType'
,'Billing'
,'Billing Configuration'
,'Billing Reports'
,'Billing Reversal'
--,'Consumables'
,'BillingChequePayment'
,'BillingInsurancePayment'
,'BillingReceivePayment'
,'Consumables Issuance'
,'Billing Quick Panel'
,'Billing Credit Knock Off'
, 'Billing - Return deposit'
,'Items Master'
,'Bill Write Off'
,'Patient Ward Admission'
,'Update Patient Ward Admission'
,'Discharge from ward'
,'Admission Wards'
,'Client Billing'
,'X-Ray Request'
,'Waiting List'
,'Laboratory Configuration'
,'Manage Clinical Services'
,'Laboratory Test Results'
,'Laboratory Module'
,'Configure Custom Lists'
,'Restore Database'
,'Setup Database Backup'))a	 
	   where not exists(Select gf.* from lnk_GroupFeatures gf where a.FeatureID=gf.FeatureID and a.FunctionID=gf.FunctionID and a.GroupID=gf.GroupID)
	 GO


Update Dtl_HomePage Set Query=Replace(Query,'[IQcare].','');
Go
SET IDENTITY_INSERT dbo.[mst_VisitType] off
Go
SET IDENTITY_INSERT dbo.[mst_VisitType] on
Go
If Not Exists(Select 1 From dbo.mst_VisitType V Inner Join Mst_Feature F On V.FeatureId = F.FeatureId Where F.ReferenceId = 'CONSUMABLES_ISSUANCE')
Begin
	Insert Into dbo.mst_VisitType(VisitTypeID,VisitName,DeleteFlag,UserID,CreateDate,SystemId,FeatureId,Custom)
	Select Top 1 60,'Issue Consumables',0,1,Getdate(),0,FeatureId,0 From Mst_Feature	F   Where F.ReferenceId = 'CONSUMABLES_ISSUANCE'	 And DeleteFlag = 0
	
End
Update 		mst_VisitType Set Custom=0 Where   VisitName = 'Issue Consumables'
Go
If Not Exists(Select 1 From dbo.mst_VisitType Where VisitName = 'Revisit')
Begin
	Insert Into dbo.mst_VisitType(VisitTypeID,VisitName,DeleteFlag,UserID,CreateDate,SystemId,FeatureId)
	Values(20,'Revisit',0,1,Getdate(),0,Null);
End 
--If Not Exists(Select 1 From dbo.mst_VisitType Where VisitName = 'XRay Order Form')
--Begin
--	Insert Into dbo.mst_VisitType(VisitTypeID,VisitName,DeleteFlag,UserID,CreateDate,SystemId,FeatureId)
--	Values(21,'XRay Order Form',0,1,Getdate(),0,Null);
--End
If Not Exists(Select 1 From dbo.mst_VisitType Where VisitName = 'Service Order Form')
Begin
	declare @VisitTypeId int;
	declare @featureId int;
	Select @FeatureId = FeatureId From mst_Feature where ReferenceId='SERVICE_REQUEST'
	Select   @VisitTypeId = max(VisitTypeId)+1 From mst_VisitType Where VisitTypeID < 59
	Insert Into dbo.mst_VisitType(VisitTypeID,VisitName,DeleteFlag,UserID,CreateDate,SystemId,FeatureId,Custom)
	Values(@VisitTypeId,'Service Order Form',0,1,Getdate(),0,@FeatureId,0);
End
Go
Update 		mst_VisitType Set Custom=0 Where   VisitName = 'Service Order Form'

GO
SET IDENTITY_INSERT dbo.[mst_VisitType] off
Go

SET IDENTITY_INSERT dbo.[mst_Feature] Off
Go
SET IDENTITY_INSERT dbo.[mst_VisitType] Off
Go
Update mst_VisitType set DeleteFlag=1 Where VisitName='XRay Order Form'
Go
If Not Exists(Select 1 From dbo.mst_Decode Where CodeID=202 And Name in ('VisitType','Visit Type'))
Begin
	Insert Into mst_Decode(Name,CodeID,SRNo,DeleteFlag,UserID,CreateDate)
	Values('VisitType',202,4,0,1,Getdate());
End
else
BEGIN
UPDATE mst_decode set name='VisitType' where name ='Visit Type' and codeid=202
END
Go
Update Mst_Decode Set DeleteFlag = 1 Where CodeID=202 And Name= 'Consumables';

Update mst_Decode set DeleteFlag= 1, Name='Old Lab Tests' Where CodeId = 202 And Id=308 And Name='Lab Tests' And DeleteFlag=0
Go
If Not Exists(Select 1 From dbo.mst_Decode Where CodeID=202 And Name='Billables')
Begin
	Insert Into mst_Decode(Name,CodeID,SRNo,DeleteFlag,UserID,CreateDate)
	Values('Billables',202,6,0,1,Getdate());
End
Go
If Not Exists(Select 1 From dbo.mst_Decode Where CodeID=202 And Name='Clinical Services')
Begin
	Insert Into mst_Decode(Name,CodeID,SRNo,DeleteFlag,UserID,CreateDate)
	Values('Clinical Services',202,7,0,1,Getdate());
End
Go
If Not Exists(Select 1 From dbo.mst_Decode Where CodeID=202 And Name='Lab Tests')
Begin
	Insert Into mst_Decode(Name,CodeID,SRNo,DeleteFlag,UserID,CreateDate)
	Values('Lab Tests',202,4,0,1,Getdate());
End
Go
Update mst_Decode Set DeleteFlag= 1 Where CodeID=202 And Name='Radiology';
Go
If Not Exists(Select 1 From dbo.mst_Decode Where CodeID=202 And Name='Ward Admission')
Begin
	Insert Into mst_Decode(Name,CodeID,SRNo,DeleteFlag,UserID,CreateDate)
	Values('Ward Admission',202,8,0,1,Getdate());
End
Go
update mst_Decode set DeleteFlag = 1 where id=332 and name='clinical' and codeid=1007
update mst_Decode set DeleteFlag = 1 where id=333 and name='CD4 count/percent' and codeid=1007
If Not Exists(Select 1 From mst_Decode where CodeID=1007 And Name='Pregnancy') Begin
	Insert Into mst_Decode (Name,CodeID,SRNo,DeleteFlag,UserID,CreateDate,SystemId) Values('Pregnancy',1007,3,0,1,getdate(),1);
End
If Not Exists(Select 1 From mst_Decode where CodeID=1007 And Name='Lactating') Begin
	Insert Into mst_Decode (Name,CodeID,SRNo,DeleteFlag,UserID,CreateDate,SystemId) Values('Lactating',1007,4,0,1,getdate(),1);
End
If Not Exists(Select 1 From mst_Decode where CodeID=1007 And Name='Under 10 Years') Begin
	Insert Into mst_Decode (Name,CodeID,SRNo,DeleteFlag,UserID,CreateDate,SystemId) Values('Under 10 Years',1007,5,0,1,getdate(),1);
End
If Not Exists(Select 1 From mst_Decode where CodeID=1007 And Name='Discordant Couple') Begin
	Insert Into mst_Decode (Name,CodeID,SRNo,DeleteFlag,UserID,CreateDate,SystemId) Values('Discordant Couple',1007,6,0,1,getdate(),1);
End
If Not Exists(Select 1 From mst_Decode where CodeID=1007 And Name='HEI (Positive PCR)') Begin
	Insert Into mst_Decode (Name,CodeID,SRNo,DeleteFlag,UserID,CreateDate,SystemId) Values('HEI (Positive PCR)',1007,7,0,1,getdate(),1);
End
If Not Exists(Select 1 From mst_Decode where CodeID=1007 And Name='Hepatitis B+') Begin
	Insert Into mst_Decode (Name,CodeID,SRNo,DeleteFlag,UserID,CreateDate,SystemId) Values('Hepatitis B+',1007,8,0,1,getdate(),1);
End
-- Update item type Drugs to Pharmaceuticals
Update Mst_Decode Set Name = 'Pharmaceuticals' Where CodeID=202 And Name= 'Drugs';
Go

Truncate Table Mst_BillingType
Go

Insert Into Mst_BillingType(BillingTypeID,Name,MasterTableName,MasterFieldName,MasterIDField,DeleteFlag,UserID,CreateDate)

SELECT BillingTypeID,Name,MasterTableName,MasterFieldName,MasterIDField,DeleteFlag,UserID,CreateDate
FROM(
Select	BillingTypeID=IT.ItemTypeID,
		Name='Pharmaceuticals',
		MasterTableName='Mst_Drug',
		MasterFieldName= 'DrugName',
		MasterIDField='Drug_PK',
		DeleteFlag=0,
		UserID=1,
		CreateDate= Getdate()
From Mst_ItemType IT
Where (IT.DeleteFlag = 0 Or IT.DeleteFlag Is Null) And IT.ItemName='Pharmaceuticals'
Union
Select	BillingTypeID=IT.ItemTypeID,
		Name='Billables',
		MasterTableName='Mst_ItemMaster',
		MasterFieldName= 'ItemName',
		MasterIDField='Item_PK',
		DeleteFlag=0,
		UserID=1,
		CreateDate= Getdate()
From Mst_ItemType IT
Where (IT.DeleteFlag = 0 Or IT.DeleteFlag Is Null) And IT.ItemName='Billables'
Union
Select	IT.ItemTypeID,
		ItemName = 'Lab Tests',
		MasterTableName='Mst_LabTestMaster',
		MasterFieldName='Name',
		MasterIDField='Id',
		0,
		1,
		Getdate()
From Mst_ItemType IT
Where (IT.DeleteFlag = 0 Or IT.DeleteFlag Is Null) And IT.ItemName='Lab Tests'
Union
Select	IT.ItemTypeID,
		ItemName = 'Clinical Services',
		MasterTableName='Mst_ClinicalService',
		MasterFieldName='Name',
		MasterIDField='Id',
		0,
		1,
		Getdate()
From Mst_ItemType IT
Where (IT.DeleteFlag = 0 Or IT.DeleteFlag Is Null) And IT.ItemName='Clinical Services'
Union
Select	IT.ItemTypeID,
		ItemName = 'VisitType',
		MasterTableName= 'Mst_VisitType',
		MasterFieldName= 'VisitName',
		MasterIDField= 'VisitTypeID',
		0,
		1,
		Getdate()
From Mst_ItemType IT
Where (IT.DeleteFlag = 0 Or IT.DeleteFlag Is Null) And IT.ItemName='VisitType'
Union
Select	IT.ItemTypeID,
		ItemName = 'Ward Admission',
		MasterTableName= 'Mst_PatientWard',
		MasterFieldName= 'WardName',
		MasterIDField= 'WardID',
		0,
		1,
		Getdate()
From Mst_ItemType IT
Where (IT.DeleteFlag = 0 Or IT.DeleteFlag Is Null) And IT.ItemName='Ward Admission')a
where not exists(select * from Mst_BillingType bt where bt.BillingTypeID=a.BillingTypeID)
;
GO
Declare @DrugItemTypeID int;

Select @DrugItemTypeID=ItemTypeID From Mst_ItemType Where ItemName='Pharmaceuticals'
/*Insert Into lnk_itemCostConfiguration 
(
	ItemId,
	ItemType,
	ItemSellingPrice,
	EffectiveDate,
	PriceStatus,
	statusDate,
	PharmacyPriceType,
	UserID,
	CreateDate,
	DeleteFlag
)
Select 
	D.Drug_PK ItemID,
	@DrugItemTypeID ItemType,
	D.SellingUnitPrice,
	D.EffectiveDate,
	PriceStatus=1,
	StatusDate=D.EffectiveDate,
	PharmacyPriceType = 0,
	D.UserID,
	CreateDate= Isnull(Isnull(D.UpdateDate,D.CreateDate),Getdate()),
	DeleteFlag = 0
From dbo.mst_Drug D 
Where (D.DeleteFlag = 0  Or D.DeleteFlag Is Null)
And D.SellingUnitPrice > 0
AND not exists (Select * from lnk_itemCostConfiguration icc where icc.ItemId=d.Drug_pk and icc.ItemType=@DrugItemTypeID);*/

Insert Into dbo.Mst_ItemSubType(SubItemTypeID,SubTypeName,ItemTypeID,DeleteFlag,userid,CreateDate,UpdateDate,SRNo)
Select
	DT.DrugTypeID,DT.DrugTypeName,@DrugItemTypeID,DT.DeleteFlag,DT.UserID,Getdate(),DT.UpdateDate,DT.SRNo
From dbo.mst_DrugType DT where not Exists (Select * from Mst_ItemSubType ist where ist.ItemTypeID=@DrugItemTypeID and ist.SubItemTypeID=dt.DrugTypeID );

Delete From  lnk_ItemCostConfiguration where ItemId Is Null

Update C Set PriceStatus= 0 From lnk_ItemCostConfiguration C Inner Join (
Select	CostId
	,	ItemId
	,	ItemType
	,	PriceStatus
	,	ItemSellingPrice
	,	EffectiveDate
	,	StatusDate
	, row_number() Over(partition by ItemId order by statusDate desc) RI
From dbo.lnk_ItemCostConfiguration
Where ItemType=@DrugItemTypeID and PriceStatus =1
)W  On W.CostId=C.CostId
Where RI > 1


Go
SET IDENTITY_INSERT dbo.[mst_Feature] Off
Go
SET IDENTITY_INSERT dbo.[mst_VisitType] Off
Go
SET IDENTITY_INSERT dbo.Mst_ItemMaster On

Go
Declare @ConsumableitemTypeID int;

select @ConsumableitemTypeID=ID from mst_decode where CodeID=202 and Name='Consumables'

Update dbo.Mst_ItemMaster Set DeleteFlag = 0 Where ItemTypeId = @ConsumableitemTypeID

Go

Declare @DrugItemTypeID int;

select @DrugItemTypeID=ID from mst_decode where CodeID=202 and Name='Pharmaceuticals'

Insert Into dbo.Mst_ItemMaster (
	Item_PK,
	ItemName,
	ItemCode,
	ItemInstructions,
	ItemTypeID,
	DeleteFlag,
	CreatedBy,
	CreateDate,
	UpdateDate,
	UpdateBy,
	DispensingMargin,
	DispensingUnitPrice,
	FDACode,
	Manufacturer,
	MaxStock,
	MinStock,
	PurchaseUnitPrice,
	QtyPerPurchaseUnit,
	DispensingUnit,
	PurchaseUnit)
Select	D.Drug_pk Item_PK,
		D.DrugName ItemName,
		D.DrugID,
		D.ItemInstructions,
		@DrugItemTypeID As ItemTypeID,
		D.DeleteFlag,
		D.UserID CreatedBy,
		D.CreateDate,
		D.UpdateDate,
		Null As UpdateBy,
		D.DispensingMargin,
		D.DispensingUnitPrice,
		D.FDACode,
		D.Manufacturer,
		D.MaxStock,
		D.MinStock,
		D.PurchaseUnitPrice,
		D.QtyPerPurchaseUnit,
		--D.SellingUnitPrice,
		D.DispensingUnit,
		D.PurchaseUnit
From dbo.mst_Drug_Bill D where not exists(select item_pk from Mst_ItemMaster im where im.Item_PK=D.Drug_pk );

Delete From  lnk_ItemCostConfiguration where ItemId Is Null

Update C Set PriceStatus= 0 From lnk_ItemCostConfiguration C Inner Join (
Select	CostId
	,	ItemId
	,	ItemType
	,	PriceStatus
	,	ItemSellingPrice
	,	EffectiveDate
	,	StatusDate
	, row_number() Over(partition by ItemId order by statusDate desc) RI
From dbo.lnk_ItemCostConfiguration
Where ItemType=@DrugItemTypeID and PriceStatus =1
)W  On W.CostId=C.CostId
Where RI > 1


-- get the drugs with prices

SET IDENTITY_INSERT dbo.Mst_ItemMaster Off 

Go
If Not Exists(Select 1 From ScheduledTask Where TaskName='Appointment.Update') Begin
	Insert Into ScheduledTask(TaskName,LastRunDate,NextRunDate,Active)
	Values('Appointment.Update',null,getdate(),1);
End
Go
If Not Exists(Select 1 From ScheduledTask Where TaskName='Database.Backup') Begin
	Insert Into ScheduledTask(TaskName,LastRunDate,NextRunDate,Active)
	Values('Database.Backup',null,getdate(),1);;
End
Go
If Not Exists(Select 1 From ScheduledTask Where TaskName='IQTools.Update') Begin
	Insert Into ScheduledTask(TaskName,LastRunDate,NextRunDate,Active)
	Values('IQTools.Update',null,getdate(),1);
End
Go
If Not Exists(Select 1 From ScheduledTask Where TaskName='Bluecard.Sync') Begin
	Insert Into ScheduledTask(TaskName,LastRunDate,NextRunDate,Active)
	Values('Bluecard.Sync',null,getdate(),1);
End
Go
Update Mst_Store Set StoreCategory =
Case
			When centralstore = 1 Then 'Purchasing'
			When dispensingstore = 1 Then 'Dispensing' End 
Where StoreCategory Is Null
Go
Update mst_Frequency Set
		DeleteFlag = 1
Where Name Not In ('OD', 'BD', 'TID','QID','PRN', '5 Times daily', 'As directed', 'Now')
And DeleteFlag = 0

Update mst_Frequency Set
		multiplier = 3
Where Name = 'TID'
And DeleteFlag = 0
And multiplier = 0
Update mst_Frequency Set
		multiplier = 4
Where Name = 'QID'
And DeleteFlag = 0
And multiplier = 0
Update mst_Frequency Set
		multiplier = 5
Where Name = '5 Times daily'
And DeleteFlag = 0
And multiplier = 0

Go
 Alter table dbo.Mst_Store Alter Column StoreCategory  varchar(250) Not Null
 Go
Update O Set
	ItemName = MI.ItemName
From dtl_PatientItemsOrder O
Inner Join
	vw_Master_ItemList MI On   MI.ItemTypeID = O.ItemTypeID
Where MI.ItemID = O.ItemID
And O.ItemName Is Null;
Go
 Alter table dbo.dtl_PatientItemsOrder Alter Column ItemName  varchar(250) Not Null
 Go
UPDATE mst_generic
  SET
      GenericAbbrevation = 'LOP/r'
WHERE GenericAbbrevation = 'LOPr';
GO
UPDATE mst_generic
  SET
      GenericAbbrevation = 'ATV'
WHERE GenericAbbrevation = 'ATR';
GO
IF NOT EXISTS
(
    SELECT *
    FROM mst_generic
    WHERE GenericName = N'Atazanavir/Ritonavir'
          AND GenericAbbrevation = N'ATV/r'
)
    BEGIN
        INSERT INTO [dbo].[mst_Generic]
        ([GenericName],
         [GenericAbbrevation]
        )
        VALUES
        ('Atazanavir/Ritonavir',
         'ATV/r'
        );
    END;