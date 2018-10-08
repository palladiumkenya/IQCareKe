
/****** Object:  View [dbo].[VW_UserDesignationTransaction]    Script Date: 9/18/2018 3:07:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/****** 
  Modified By - Bhupendra Singh
  Modified Dt - 8/23/2016 
  Reason	  - Bug ID 1317 Version 3.9.0
******/

ALTER VIEW [dbo].[VW_UserDesignationTransaction]
AS
SELECT dbo.mst_User.UserID
	,LTRIM(RTRIM(dbo.mst_User.UserLastName)) + ' ' + LTRIM(RTRIM(dbo.mst_User.UserFirstName)) AS UserName
	,dbo.mst_User.Email
	,LTRIM(RTRIM(dbo.mst_Designation.NAME)) AS Designation
	,dbo.mst_User.DeleteFlag 
FROM dbo.mst_Designation
INNER JOIN dbo.mst_User ON dbo.mst_Designation.Id = dbo.mst_User.Designation

GO


