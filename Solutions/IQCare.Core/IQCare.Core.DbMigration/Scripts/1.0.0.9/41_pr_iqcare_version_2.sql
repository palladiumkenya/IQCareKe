/****** Object:  StoredProcedure [dbo].[Pr_Security_UserLogin_Constella]    Script Date: 11/12/2018 11:28:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER procedure [dbo].[Pr_Security_UserLogin_Constella]                                                              
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
			isnull(a.ModuleFlag,0) ModuleFlag,
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
				Inner Join lnk_FacilityModule b On a.FacilityID = b.FacilityID -- and b.ModuleID <> 203
	) Z
	Inner Join mst_module a On Z.ModuleID = a.ModuleID
	Where a.Status = 2
	And FacilityID = @LocationId     
	Union
	Select
			a.FacilityID,
			b.ModuleID,
			'Green Card (2016)' As DisplayName,
			1 CanEnroll,
			'CCC' As ModuleName,
			1 As ModuleFlag,
			M.PharmacyFlag,
			a.StrongPassFlag,
			a.ExpPwdFlag,
			a.ExpPwdDays
		From mst_Facility a
			Inner Join lnk_FacilityModule b On a.FacilityID = b.FacilityID  And a.FacilityID = @LocationId       and  b.ModuleID=203
		Inner Join mst_module M On M.ModuleID=b.ModuleID and M.ModuleID=203 and M.Status=2                           
              union
			  	Select
			a.FacilityID,
			b.ModuleID,
			'IQCare 2.0' As DisplayName,
			1 CanEnroll,
			'HTS' As ModuleName,
			1 As ModuleFlag,
			M.PharmacyFlag,
			a.StrongPassFlag,
			a.ExpPwdFlag,
			a.ExpPwdDays
		From mst_Facility a
			Inner Join lnk_FacilityModule b On a.FacilityID = b.FacilityID  And a.FacilityID = @LocationId       and  b.ModuleID=203
		Inner Join mst_module M On M.ModuleID=b.ModuleID and M.ModuleID=203 and M.Status=2  
--4                                
  Select GetDate()[CurrentDate]    
  
  Select * From mst_User Us                                       
end