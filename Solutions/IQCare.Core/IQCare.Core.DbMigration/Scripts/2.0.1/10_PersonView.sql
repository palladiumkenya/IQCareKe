/****** Object:  View [dbo].[PersonView]    Script Date: 5/16/2019 11:31:47 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER VIEW [dbo].[PersonView]
AS
SELECT
[Id],
CAST(DECRYPTBYKEY([FirstName]) AS VARCHAR(50)) AS [FirstName],
CAST(DECRYPTBYKEY([MidName]) AS VARCHAR(50)) AS [MiddleName],
CAST(DECRYPTBYKEY([LastName]) AS VARCHAR(50)) AS [LastName],
[Sex],
[DateOfBirth],
[DobPrecision],
[RegistrationDate],
[FacilityId],
DeleteFlag,
CAST(DECRYPTBYKEY([NickName]) AS VARCHAR(50)) AS [NickName]
FROM Person
GO


