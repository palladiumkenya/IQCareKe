
/****** Object:  View [dbo].[VW_UserDesignationTransaction]    Script Date: 9/18/2018 3:07:52 PM ******/



IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_UserDesignationTransaction]'))

BEGIN
EXEC ('
ALTER VIEW  [dbo].[VW_UserDesignationTransaction] as

SELECT dbo.mst_Employee.UserID
	,LTRIM(RTRIM(dbo.mst_Employee.LastName)) + '''' + LTRIM(RTRIM(dbo.mst_Employee.FirstName)) AS UserName
	,'''' as Email
	,LTRIM(RTRIM(dbo.mst_Designation.NAME)) AS Designation
	,dbo.mst_Employee.DeleteFlag 
FROM dbo.mst_Designation
INNER JOIN dbo.mst_Employee ON dbo.mst_Designation.Id = dbo.mst_Employee.DesignationID')
END


IF NOT  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_UserDesignationTransaction]'))
BEGIN
 EXEC ('
CREATE VIEW  [dbo].[VW_UserDesignationTransaction] as

SELECT dbo.mst_Employee.UserID
	,LTRIM(RTRIM(dbo.mst_Employee.LastName)) + '''' + LTRIM(RTRIM(dbo.mst_Employee.FirstName)) AS UserName
	,'''' as Email
	,LTRIM(RTRIM(dbo.mst_Designation.NAME)) AS Designation
	,dbo.mst_Employee.DeleteFlag 
FROM dbo.mst_Designation
INNER JOIN dbo.mst_Employee ON dbo.mst_Designation.Id = dbo.mst_Employee.DesignationID')

END