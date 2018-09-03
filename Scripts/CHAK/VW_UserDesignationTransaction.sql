/****** Object:  View [dbo].[VW_UserDesignationTransaction]    Script Date: 8/27/2018 12:44:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[VW_UserDesignationTransaction]
AS
SELECT        dbo.mst_User.UserID, LTRIM(RTRIM(dbo.mst_User.UserLastName)) + ' ' + LTRIM(RTRIM(dbo.mst_User.UserFirstName)) AS UserName, dbo.mst_User.Email, 
                         LTRIM(RTRIM(dbo.mst_Designation.Name)) AS Designation, dbo.mst_User.DeleteFlag
FROM            dbo.mst_Employee INNER JOIN
                         dbo.mst_User ON dbo.mst_Employee.EmployeeID = dbo.mst_User.EmployeeId INNER JOIN
                         dbo.mst_Designation ON dbo.mst_Employee.DesignationID = dbo.mst_Designation.Id

GO


