
/****** Object:  View [dbo].[VW_UserDesignationTransaction]    Script Date: 9/18/2018 3:07:52 PM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_UserDesignationTransaction]'))
DROP VIEW [dbo].[VW_UserDesignationTransaction]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/****** 
  Modified By - Bhupendra Singh
  Modified Dt - 8/23/2016 
  Reason	  - Bug ID 1317 Version 3.9.0
******/


IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_UserDesignationTransaction]'))

BEGIN
DROP VIEW [dbo].[VW_UserDesignationTransaction]
END


if  exists (select
                     column_name
               from
                     INFORMATION_SCHEMA.columns
               where
                     table_name = 'mst_User'
                     and column_name = 'Designation')
BEGIN
EXEC('CREATE VIEW [dbo].[VW_UserDesignationTransaction]
AS


SELECT dbo.mst_User.UserID
	,LTRIM(RTRIM(dbo.mst_User.UserLastName)) + '''' + LTRIM(RTRIM(dbo.mst_User.UserFirstName)) AS UserName
	,mst_User.Email as Email
	,LTRIM(RTRIM(dbo.mst_Designation.NAME)) AS Designation
	,dbo.mst_User.DeleteFlag 
FROM dbo.mst_Designation
INNER JOIN dbo.mst_User ON dbo.mst_Designation.Id = dbo.mst_User.Designation


')
END


if NOT exists (select
                     column_name
               from
                     INFORMATION_SCHEMA.columns
               where
                     table_name = 'mst_User'
                     and column_name = 'Designation')
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