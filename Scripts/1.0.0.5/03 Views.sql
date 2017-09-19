/****** Object:  View [dbo].[Api_MaritalStatusView]    Script Date: 19/09/2017 13:06:30 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientRegistrationView]'))
DROP VIEW [dbo].[Api_MaritalStatusView]
GO

/****** Object:  View [dbo].[Api_MaritalStatusView]    Script Date: 19/09/2017 13:06:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[Api_MaritalStatusView]
AS
SELECT        PM.PersonId, CASE (L.ItemName) 
                         WHEN 'Widowed' THEN 'W' WHEN 'Divorced' THEN 'D' WHEN 'Separated' THEN 'SP' WHEN 'Married Polygamous' THEN 'MP' WHEN 'Cohabiting' THEN 'C' WHEN 'Single' THEN 'S' WHEN 'Married Monogamous' THEN 'MM' ELSE
                          'S' END AS ApiValue, L.ItemName, PM.MaritalStatusId ValueId
FROM            PatientMaritalStatus AS PM INNER JOIN
                         LookupItemView AS L ON PM.MaritalStatusId = L.ItemId
WHERE        (PM.DeleteFlag = 0) AND (L.MasterName = 'MaritalStatus')



GO


IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Api_PatientCareEndingView]'))
DROP VIEW [dbo].[Api_PatientCareEndingView]
GO

/****** Object:  View [dbo].[Api_PatientCareEndingView]    Script Date: 19/09/2017 13:10:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[Api_PatientCareEndingView]
AS
SELECT        dbo.PatientCareending.DateOfDeath, dbo.PatientCareending.PatientId
FROM            dbo.PatientCareending INNER JOIN
                         dbo.LookupItemView ON dbo.PatientCareending.ExitReason = dbo.LookupItemView.ItemId
WHERE        (dbo.PatientCareending.DeleteFlag = 0) AND (dbo.LookupItemView.MasterName = 'CareEnded')


GO


IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Api_PatientContactsView]'))
DROP VIEW [dbo].[Api_PatientContactsView]
GO


/****** Object:  View [dbo].[Api_PatientContactsView]    Script Date: 19/09/2017 13:11:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[Api_PatientContactsView]
AS
SELECT        PersonId, CAST(DECRYPTBYKEY(PhysicalAddress) AS varchar(100)) AS PhysicalAddress, CAST(DECRYPTBYKEY(MobileNumber) AS varchar(20)) AS MobileNumber
FROM            dbo.PersonContact
WHERE        (DeleteFlag = 0)


GO


IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Api_PatientDemographicsView]'))
DROP VIEW [dbo].[Api_PatientDemographicsView]
GO

/****** Object:  View [dbo].[Api_PatientDemographicsView]    Script Date: 19/09/2017 13:12:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[Api_PatientDemographicsView]
AS
SELECT PR.Id, PT.Id as PatientId, PIE.IdentifierValue, CAST(DECRYPTBYKEY(PR.FirstName) AS VARCHAR(50)) AS FIRST_NAME, CAST(DECRYPTBYKEY(PR.MidName) AS VARCHAR(50)) AS MIDDLE_NAME, CAST(DECRYPTBYKEY(PR.LastName) AS VARCHAR(50)) 
                         AS LAST_NAME, CAST(DECRYPTBYKEY(PT.NationalId) AS VARCHAR(50)) AS NATIONAL_ID, NULL AS MOTHER_MAIDEN_NAME, format(cast(PT.DateOfBirth as date),'yyyyMMdd') AS DATE_OF_BIRTH,
SEX = CASE(SELECT ItemName FROM LookupItemView WHERE ItemId = PR.Sex AND MasterName = 'Gender') WHEN 'Male' THEN 'M' WHEN 'Female' THEN 'F' ELSE '' END, PE.ServiceAreaId
FROM            dbo.Patient AS PT INNER JOIN
                         dbo.Person AS PR ON PT.PersonId = PR.Id INNER JOIN
                         dbo.PatientEnrollment AS PE ON PT.Id = PE.PatientId INNER JOIN
                         dbo.PatientIdentifier AS PIE ON PE.Id = PIE.PatientEnrollmentId AND PT.Id = PIE.PatientId


GO


IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Api_PatientLocationView]'))
DROP VIEW [dbo].[Api_PatientLocationView]
GO

/****** Object:  View [dbo].[Api_PatientLocationView]    Script Date: 19/09/2017 13:13:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Api_PatientLocationView]
AS
SELECT        dbo.PersonLocation.PersonId, dbo.County.CountyName, dbo.County.Subcountyname, dbo.County.WardName, dbo.PersonLocation.Village
FROM            dbo.PersonLocation INNER JOIN
                         dbo.County ON dbo.PersonLocation.Ward = dbo.County.WardId
WHERE        (dbo.PersonLocation.DeleteFlag = 0)

GO


IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Api_TreatmentSupporterView]'))
DROP VIEW [dbo].[Api_TreatmentSupporterView]
GO


/****** Object:  View [dbo].[Api_TreatmentSupporterView]    Script Date: 19/09/2017 13:14:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Api_TreatmentSupporterView]
AS
SELECT        dbo.Person.Id AS PersonId, CAST(DECRYPTBYKEY(dbo.Person.FirstName) AS VARCHAR(50)) AS FIRST_NAME, CAST(DECRYPTBYKEY(dbo.Person.MidName) AS VARCHAR(50)) AS MIDDLE_NAME, 
                         CAST(DECRYPTBYKEY(dbo.Person.LastName) AS VARCHAR(50)) AS LAST_NAME, NULL AS RELATIONSHIP, NULL AS ADDRESS, CAST(DECRYPTBYKEY(dbo.PatientTreatmentSupporter.MobileContact) AS VARCHAR(50)) 
                         AS PHONE_NUMBER, SEX = CASE(SELECT ItemName FROM LookupItemView WHERE ItemId = Person.Sex AND MasterName = 'Gender') WHEN 'Male' THEN 'M' WHEN 'Female' THEN 'F' ELSE '' END,NULL AS DATE_OF_BIRTH, 'T' AS CONTACT_ROLE
FROM            dbo.Person INNER JOIN
                         dbo.PatientTreatmentSupporter ON dbo.Person.Id = dbo.PatientTreatmentSupporter.PersonId

GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Api_PatientMessage]'))
DROP VIEW [dbo].[Api_PatientMessage]
GO

/****** Object:  View [dbo].[Api_PatientMessage]    Script Date: 19/09/2017 13:14:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[Api_PatientMessage]
AS
SELECT        PDG.Id, PDG.PatientId, PDG.IdentifierValue, PDG.FIRST_NAME, PDG.MIDDLE_NAME, 
                         PDG.LAST_NAME, PDG.NATIONAL_ID, PDG.MOTHER_MAIDEN_NAME, PDG.DATE_OF_BIRTH, 
                         PDG.SEX, PCV.PhysicalAddress, PCV.MobileNumber, PLV.CountyName, PLV.Subcountyname, 
                         PLV.WardName, PLV.Village, PDG.ServiceAreaId, MS.ApiValue AS MARITAL_STATUS, PTS.FIRST_NAME AS TFIRST_NAME, 
                         PTS.MIDDLE_NAME AS TMIDDLE_NAME, PTS.LAST_NAME AS TLAST_NAME, PTS.RELATIONSHIP AS TRELATIONSHIP, 
                         PTS.ADDRESS AS TADDRESS, PTS.PHONE_NUMBER AS TPHONE_NUMBER, PTS.SEX AS TSEX, 
                         PTS.DATE_OF_BIRTH AS TDATE_OF_BIRTH, PTS.CONTACT_ROLE AS TCONTACT_ROLE
FROM           [dbo].[Api_PatientDemographicsView] PDG  INNER JOIN
                         [dbo].[Api_MaritalStatusView] MS ON PDG.Id = MS.PersonId LEFT OUTER JOIN
                         [dbo].[Api_TreatmentSupporterView] PTS ON PDG.Id = PTS.PersonId LEFT OUTER JOIN
                         [dbo].[Api_PatientContactsView] PCV ON PDG.Id = PCV.PersonId LEFT OUTER JOIN
                         [dbo].[Api_PatientLocationView] PLV ON PDG.Id = PLV.PersonId


GO