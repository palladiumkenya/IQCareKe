IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'Email'
          AND Object_ID = Object_ID(N'Mst_User'))
		  BEGIN
ALTER TABLE Mst_User
ADD Email varchar(50)
END

 IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'Designation'
          AND Object_ID = Object_ID(N'Mst_User'))
BEGIN
ALTER TABLE Mst_User
ADD	Designation int

END

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_UserDesignationTransaction]'))

BEGIN
EXEC ('
ALTER VIEW  [dbo].[VW_UserDesignationTransaction] as
SELECT dbo.mst_User.UserID
	,LTRIM(RTRIM(dbo.mst_User.UserLastName)) + '' '' + LTRIM(RTRIM(dbo.mst_User.UserFirstName)) AS UserName
	,dbo.mst_User.Email
	,CASE WHEN LTRIM(RTRIM(dbo.mst_Designation.NAME))=null then dt.Name else  LTRIM(RTRIM(dbo.mst_Designation.NAME)) end AS Designation
	,dbo.mst_User.DeleteFlag 
	from dbo.vw_UserList
	left join dbo.mst_User on dbo.mst_User.UserID=dbo.vw_UserList.UserId
LEFT JOIN dbo.mst_Designation ON dbo.mst_Designation.Id = dbo.vw_UserList.DesignationId or  dbo.mst_Designation.Id=dbo.mst_User.Designation
left join dbo.mst_Employee  on  dbo.mst_Employee.EmployeeID=dbo.vw_UserList.EmployeeId
left join dbo.mst_Designation dt on dt.Id=dbo.mst_Employee.DesignationID')
END


IF NOT  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_UserDesignationTransaction]'))
BEGIN
 EXEC ('
CREATE VIEW  [dbo].[VW_UserDesignationTransaction] as
SELECT dbo.mst_User.UserID
	,LTRIM(RTRIM(dbo.mst_User.UserLastName)) + '' '' + LTRIM(RTRIM(dbo.mst_User.UserFirstName)) AS UserName
	,dbo.mst_User.Email
	,CASE WHEN LTRIM(RTRIM(dbo.mst_Designation.NAME))=null then dt.Name else  LTRIM(RTRIM(dbo.mst_Designation.NAME)) end AS Designation
	,dbo.mst_User.DeleteFlag 
	from dbo.vw_UserList
	left join dbo.mst_User on dbo.mst_User.UserID=dbo.vw_UserList.UserId
LEFT JOIN dbo.mst_Designation ON dbo.mst_Designation.Id = dbo.vw_UserList.DesignationId or  dbo.mst_Designation.Id=dbo.mst_User.Designation
left join dbo.mst_Employee  on  dbo.mst_Employee.EmployeeID=dbo.vw_UserList.EmployeeId
left join dbo.mst_Designation dt on dt.Id=dbo.mst_Employee.DesignationID')

END

