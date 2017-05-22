/****** Object:  View [dbo].[Mst_Drug]    Script Date: 5/11/2017 4:23:25 PM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Mst_Drug]'))
DROP VIEW [dbo].[Mst_Drug]
GO
/****** Object:  View [dbo].[Mst_Drug]    Script Date: 5/11/2017 4:23:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Object:  View [dbo].[view_patientVisit]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[view_patientVisit]'))
DROP VIEW [dbo].[view_patientVisit]
GO
/****** Object:  View [dbo].[ServiceAreaFormView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ServiceAreaFormView]'))
DROP VIEW [dbo].[ServiceAreaFormView]
GO
/****** Object:  View [dbo].[ServiceAreaBusinessRuleView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ServiceAreaBusinessRuleView]'))
DROP VIEW [dbo].[ServiceAreaBusinessRuleView]
GO
/****** Object:  View [dbo].[PersonView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PersonView]'))
DROP VIEW [dbo].[PersonView]
GO
/****** Object:  View [dbo].[PersonContactView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PersonContactView]'))
DROP VIEW [dbo].[PersonContactView]
GO
/****** Object:  View [dbo].[PatientVisitView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientVisitView]'))
DROP VIEW [dbo].[PatientVisitView]
GO
/****** Object:  View [dbo].[PatientView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientView]'))
DROP VIEW [dbo].[PatientView]
GO
/****** Object:  View [dbo].[PatientTreatmentTrackerView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientTreatmentTrackerView]'))
DROP VIEW [dbo].[PatientTreatmentTrackerView]
GO
/****** Object:  View [dbo].[PatientTreatmentSupporterView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientTreatmentSupporterView]'))
DROP VIEW [dbo].[PatientTreatmentSupporterView]
GO
/****** Object:  View [dbo].[PatientPopulationView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientPopulationView]'))
DROP VIEW [dbo].[PatientPopulationView]
GO
/****** Object:  View [dbo].[PatientBaselineView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientBaselineView]'))
DROP VIEW [dbo].[PatientBaselineView]
GO
/****** Object:  View [dbo].[ord_PatientLabOrder]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ord_PatientLabOrder]'))
DROP VIEW [dbo].[ord_PatientLabOrder]
GO
/****** Object:  View [dbo].[Laboratory_ViralLoad]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Laboratory_ViralLoad]'))
DROP VIEW [dbo].[Laboratory_ViralLoad]
GO
/****** Object:  View [dbo].[LookupView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[LookupView]'))
DROP VIEW [dbo].[LookupView]
GO
/****** Object:  View [dbo].[gcPatientView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[gcPatientView]'))
DROP VIEW [dbo].[gcPatientView]
GO
/****** Object:  View [dbo].[facilityStatisticsView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[facilityStatisticsView]'))
DROP VIEW [dbo].[facilityStatisticsView]
GO
/****** Object:  View [dbo].[PatientServiceEnrollmentView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PatientServiceEnrollmentView]'))
DROP VIEW [dbo].[PatientServiceEnrollmentView]
GO
/****** Object:  View [dbo].[LookupItemView]    Script Date: 5/9/2017 10:27:05 AM ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[LookupItemView]'))
DROP VIEW [dbo].[LookupItemView]
GO
/****** Object:  View [dbo].[LookupItemView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[LookupItemView]
AS
SELECT        MasterId, ItemId, MasterName, ItemName, DisplayName, ItemDisplayName, OrdRank, ISNULL(ROW_NUMBER() OVER(ORDER BY ItemId DESC), -1) AS RowID
FROM            (SELECT        M.Id AS MasterId, I.Id AS ItemId, M.Name AS MasterName, I.Name AS ItemName, L.DisplayName, L.DisplayName AS ItemDisplayName, L.OrdRank
                          FROM            dbo.LookupMaster AS M INNER JOIN
                                                    dbo.LookupMasterItem AS L ON M.Id = L.LookupMasterId INNER JOIN
                                                    dbo.LookupItem AS I ON L.LookupItemId = I.Id) AS X


GO
/****** Object:  View [dbo].[PatientServiceEnrollmentView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PatientServiceEnrollmentView]
AS
SELECT        dbo.PatientIdentifier.IdentifierValue AS EnrollmentNumber, dbo.LookupItemView.DisplayName AS ServiceArea, dbo.PatientEnrollment.EnrollmentDate, 
                         CAST((CASE dbo.PatientEnrollment.CareEnded WHEN 0 THEN 'Active' WHEN 1 THEN 'In-Active' END) AS varchar(50)) AS PatientStatus, dbo.PatientIdentifier.PatientId, dbo.Person.Id AS PersonId,ISNULL(ROW_NUMBER() OVER (ORDER BY PersonId DESC), - 1) AS Id
FROM            dbo.PatientIdentifier INNER JOIN
                         dbo.LookupItemView ON dbo.PatientIdentifier.IdentifierTypeId = dbo.LookupItemView.ItemId INNER JOIN
                         dbo.PatientEnrollment ON dbo.PatientIdentifier.PatientEnrollmentId = dbo.PatientEnrollment.Id INNER JOIN
                         dbo.Patient ON dbo.PatientEnrollment.PatientId = dbo.Patient.Id RIGHT OUTER JOIN
                         dbo.Person ON dbo.Patient.PersonId = dbo.Person.Id


GO
/****** Object:  View [dbo].[facilityStatisticsView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW 
	[dbo].[facilityStatisticsView]
AS
	SELECT 
		(SELECT 1) Id,
		(SELECT ISNULL(count(*),0) FROM patient WHERE Id IN(SELECT PatientId FROM PatientEnrollment) AND PatientType NOT IN(SELECT Id FROM LookupItemView WHERE ItemName='Transit' or ItemName='Transfer-In') AND Id IN(SELECT PatientId FROM PatientIdentifier WHERE IdentifierTypeId IN(SELECT Id FROM LookupItem WHERE Name='CCCNumber'))) [TotalCumulativePatients],
		((SELECT COUNT(DISTINCT PatientId) FROM PatientTreatmentTrackerView WHERE DATEDIFF(DAY,CreateDate,GETDATE())<=90 AND RegimenLine IS NOT NULL)+(SELECT COUNT(DISTINCT ptn_pk) FROM dtl_RegimenMap r WHERE r.Ptn_Pk NOT IN(SELECT Ptn_Pk FROM PatientTreatmentTrackerView) AND DATEDIFF(DAY,r.CreateDate,GETDATE())<=90)) [TotalActiveOnArt],
		((SELECT ISNULL(count(*),0) FROM Patient WHERE PatientType IN(SELECT Id from LookupItem WHERE Name='Transfer-In') AND Id IN(SELECT PatientId FROM PatientEnrollment) AND Id IN(SELECT PatientId FROM PatientIdentifier WHERE IdentifierTypeId IN(SELECT Id FROM LookupItem WHERE Name='CCCNumber')))+(SELECT COUNT(DISTINCT ptn_pk) FROM mst_Patient WHERE TransferIn IS NOT NULL AND Ptn_Pk NOT IN(SELECT Ptn_Pk FROM Patient))) [TotalTransferIn],
		((SELECT isnull(count(*),0) FROM PatientCareending WHERE ExitReason IN(SELECT Id FROM LookupItem WHERE Name='Transfer Out'))+(SELECT COUNT(DISTINCT ptn_pk) FROM dtl_PatientCareEnded WHERE PatientExitReason IN(118,349))) [TotalPatientsTransferedOut],
		(SELECT ISNULL(count(*),0) FROM dtl_PatientPharmacyOrder WHERE Drug_pk IN(460,1095,1015968,969,970,971,972,973,974,975,976,977,978,979,1005,1006,1010,1011,1012,1013,1014,1015) AND DispensedQuantity>0 AND DATEDIFF(Day,CreateDate,GETDATE())<=90) [TotalOnCtxDapson],
		((SELECT isnull(count(*),0) FROM PatientCareending WHERE ExitReason IN(SELECT Id FROM LookupItem WHERE Name='Death'))+(SELECT COUNT(DISTINCT Ptn_Pk) FROM dtl_PatientCareEnded WHERE PatientExitReason=93 ))[TotalPatientsDead],
		((SELECT ISNULL(count(*),0) FROM Patient WHERE PatientType IN(SELECT Id from LookupItem WHERE Name='Transit') AND Id IN(SELECT PatientId FROM PatientEnrollment) AND Id IN(SELECT PatientId FROM PatientIdentifier WHERE IdentifierTypeId IN(SELECT Id FROM LookupItem WHERE Name='CCCNumber')))+(SELECT COUNT(DISTINCT ptn_pk) FROM mst_Patient WHERE Ptn_Pk IN(SELECT Ptn_Pk FROM Lnk_PatientProgramStart WHERE ModuleId=20)))  [TotalTransit],
		(SELECT ISNULL(COUNT(DISTINCT Ptn_Pk),0) FROM dtl_PatientCareEnded p WHERE p.PatientExitReason=91)  [LostToFollowUp];

GO
/****** Object:  View [dbo].[gcPatientView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[gcPatientView]
AS
SELECT DISTINCT 
                         pt.Id, pt.PersonId, pt.ptn_pk, pni.IdentifierValue AS EnrollmentNumber, pt.PatientIndex, CAST(DECRYPTBYKEY(pn.FirstName) AS VARCHAR(50)) AS FirstName, CAST(DECRYPTBYKEY(pn.MidName) 
                         AS VARCHAR(50)) AS MiddleName, CAST(DECRYPTBYKEY(pn.LastName) AS VARCHAR(50)) AS LastName, pn.Sex, pn.Active, pt.RegistrationDate AS RegistrationDate, pe.EnrollmentDate AS [EnrollmentDate ], 
                         CAST((CASE pe.CareEnded WHEN 0 THEN 'Active' WHEN 1 THEN 'In-Active' END) AS varchar(50)) AS PatientStatus, pt.DateOfBirth, CAST(DECRYPTBYKEY(pt.NationalId) AS VARCHAR(50)) AS NationalId, 
                         pt.FacilityId, pt.PatientType, pe.TransferIn, CAST(DECRYPTBYKEY(pc.MobileNumber) AS varchar(20)) AS MobileNumber
FROM            dbo.Patient AS pt INNER JOIN
                         dbo.Person AS pn ON pn.Id = pt.PersonId INNER JOIN
                         dbo.PatientEnrollment AS pe ON pt.Id = pe.PatientId INNER JOIN
                         dbo.PatientIdentifier AS pni ON pni.PatientId = pt.Id LEFT OUTER JOIN
                         dbo.PersonContact AS pc ON pc.PersonId = pt.PersonId
WHERE        (pni.IdentifierTypeId IN
                             (SELECT        Id
                               FROM            dbo.LookupItem
                               WHERE        (Name = 'CCCNumber')))


GO
/****** Object:  View [dbo].[LookupView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[LookupView]
AS
	Select	Id
		,	D.Name
		,	D.CodeID					LookupId
		,	C.Name						LookupName
		,	convert(bit, D.DeleteFlag)	Deleted
		,	'MST_DECODE'				Category
	From mst_Decode D
	Inner Join Mst_Code C On C.CodeID = D.CodeID
	Union All
	Select	Id
		,	D.Name
		,	D.CodeID					LookupId
		,	C.Name						LookupName
		,	convert(bit, D.DeleteFlag)	Deleted
		,	'MST_MODDECODE'				Category
	From mst_ModDecode D
	Inner Join Mst_ModCode C On C.CodeID = D.CodeID
	Union All
	Select	Id
		,	D.Name
		,	D.CodeID					LookupId
		,	C.Name						LookupName
		,	convert(bit, D.DeleteFlag)	Deleted
		,	'MST_BLUEDECODE'			Category
	From mst_BlueDecode D
	Inner Join Mst_BlueCode C On C.CodeID = D.CodeID
	Union All
	Select	Id
		,	D.Name
		,	D.CodeID					LookupId
		,	C.Name						LookupName
		,	convert(bit, D.DeleteFlag)	Deleted
		,	'PMTCTDECODE'				Category
	From mst_pmtctDeCode D
	Inner Join mst_pmtctCode C On C.CodeID = D.CodeID



GO
/****** Object:  View [dbo].[ord_PatientLabOrder]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[ord_PatientLabOrder]
AS
Select	Id As LabID,
		Null As OldLabID,
		Ptn_Pk,
		LocationId,
		OrderedBy As OrderedbyName,
		OrderDate As OrderedbyDate,
		(
			Select Top (1)
				ResultBy
			From dbo.dtl_LabOrderTest
			Where (LabOrderId = lo.Id) And ResultBy Is Not Null
		)
		As ReportedbyName,
		(
			Select Top (1)
				ResultDate
			From dbo.dtl_LabOrderTest As R
			Where (LabOrderId = lo.Id) And R.ResultDate Is Not Null
		)
		As ReportedbyDate,
		Null As CheckedbyName,
		Null As CheckedbyDate,
		PreClinicLabDate,
		DeleteFlag,
		UserId,
		CreateDate,
		UpdateDate,
		VisitId,
		Null As LabPeriod,
		OrderNumber As LabNumber
From dbo.ord_LabOrder As lo

GO
/****** Object:  View [dbo].[Laboratory_ViralLoad]    Script Date: 5/17/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Laboratory_ViralLoad]
AS
SELECT        labTrac.Id, labTrac.patientId, PatientLabTracker_1.ResultValues, PatientLabTracker_1.FacilityId
FROM            (SELECT        MAX(Id) AS Id, patientId
                          FROM            dbo.PatientLabTracker
                          GROUP BY patientId, LabTestId
                          HAVING         (LabTestId = 3)) AS labTrac INNER JOIN
                         dbo.PatientLabTracker AS PatientLabTracker_1 ON labTrac.Id = PatientLabTracker_1.Id
                        WHERE        (PatientLabTracker_1.Results = 'Complete')
GO
/****** Object:  View [dbo].[PatientBaselineView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[PatientBaselineView]
AS
SELECT        dbo.PatientHivDiagnosis.PatientId, dbo.PatientHivDiagnosis.Id, dbo.PatientHivDiagnosis.PatientMasterVisitId, dbo.PatientTransferIn.ServiceAreaId, dbo.PatientTransferIn.TransferInDate, 
                         dbo.PatientTransferIn.TreatmentStartDate, isnull(dbo.PatientTransferIn.CurrentTreatment,0) 'CurrentTreatment',
                             (SELECT        Name + ' (' + DisplayName + ')' AS Expr1
                               FROM            dbo.LookupItem
                               WHERE        (Id = dbo.PatientTransferIn.CurrentTreatment)) AS CurrentTreatmentName, dbo.PatientTransferIn.FacilityFrom, dbo.PatientTransferIn.MFLCode, ISNULL(dbo.PatientTransferIn.CountyFrom,0) 'CountyFrom', 
                         dbo.PatientTransferIn.TransferInNotes, dbo.PatientHivDiagnosis.HIVDiagnosisDate, dbo.PatientHivDiagnosis.EnrollmentDate, ISNULL(dbo.PatientHivDiagnosis.EnrollmentWHOStage,0) 'EnrollmentWHOStage',
                             (SELECT        Name
                               FROM            dbo.LookupItem
                               WHERE        (Id = dbo.PatientHivDiagnosis.EnrollmentWHOStage)) AS EnrollmentWHOStageName, dbo.PatientHivDiagnosis.ARTInitiationDate, dbo.PatientTreatmentInitiation.DateStartedOnFirstLine, 
                         dbo.PatientTreatmentInitiation.Cohort, ISNULL(dbo.PatientTreatmentInitiation.Regimen,0)'Regimen',
                             (SELECT        Name + ' (' + DisplayName + ')' AS Expr1
                               FROM            dbo.LookupItem
                               WHERE        (Id = dbo.PatientTreatmentInitiation.Regimen)) AS RegimenName, dbo.PatientTreatmentInitiation.BaselineViralload, dbo.PatientTreatmentInitiation.BaselineViralloadDate, 
                         dbo.PatientBaselineAssessment.HBVInfected, dbo.PatientBaselineAssessment.Pregnant, dbo.PatientBaselineAssessment.TBinfected, dbo.PatientBaselineAssessment.WHOStage,
                             (SELECT        Name
                               FROM            dbo.LookupItem
                               WHERE        (Id = dbo.PatientBaselineAssessment.WHOStage)) AS WhoStageName, dbo.PatientBaselineAssessment.BreastFeeding, dbo.PatientBaselineAssessment.CD4Count, 
                         dbo.PatientBaselineAssessment.MUAC, dbo.PatientBaselineAssessment.Weight, dbo.PatientBaselineAssessment.Height, dbo.PatientBaselineAssessment.BMI
FROM           dbo.PatientHivDiagnosis LEFT OUTER JOIN
                          dbo.PatientTransferIn ON dbo.PatientHivDiagnosis.PatientId = dbo.PatientTransferIn.PatientId LEFT OUTER JOIN
                         dbo.PatientTreatmentInitiation ON dbo.PatientHivDiagnosis.PatientId = dbo.PatientTreatmentInitiation.PatientId LEFT OUTER JOIN
                         dbo.PatientBaselineAssessment ON dbo.PatientHivDiagnosis.PatientId = dbo.PatientBaselineAssessment.PatientId



GO
/****** Object:  View [dbo].[PatientPopulationView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PatientPopulationView]
AS
SELECT b.ptn_pk AS PatientPK, CASE WHEN a.PopulationType = 'General Population' THEN 'General Population' ELSE c.name END AS PopulationCategory
FROM     dbo.PatientPopulation AS a INNER JOIN
                  dbo.Patient AS b ON a.PersonId = b.PersonId LEFT OUTER JOIN
                  dbo.LookupItem AS c ON a.PopulationCategory = b.Id


GO
/****** Object:  View [dbo].[PatientTreatmentSupporterView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PatientTreatmentSupporterView]
AS
SELECT        Id, PersonId, SupporterId, CAST(DECRYPTBYKEY(MobileContact) AS VARCHAR(50)) AS MobileContact, DeleteFlag, CreatedBy, CreateDate, AuditData
FROM            dbo.PatientTreatmentSupporter


GO
/****** Object:  View [dbo].[PatientTreatmentTrackerView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[PatientTreatmentTrackerView]
AS
SELECT        a.Id,
     PatientId,
	 p.ptn_pk,
     ServiceAreaId,
     PatientMasterVisitId,
        RegimenStartDate,
           RegimenId,
           (SELECT Name+'('+DisplayName+')' FROM LookupItem WHERE Id=RegimenId) as Regimen,
           RegimenLineId,
           (SELECT Name FROM LookupItem WHERE Id=RegimenLineId) as RegimenLine, 
        DrugId, 
        RegimenStatusDate,
        TreatmentStatusId,
     (SELECT Name FROM LookupItem WHERE Id=TreatmentStatusId) TreatmentStatus,
        ParentId, 
        CreateBy as CreatedBy,
       a. CreateDate, 
            a.  DeleteFlag,
     TreatmentStatusReasonId,
     (SELECT Name FROM LookupItem WHERE Id=TreatmentStatusReasonId) as TreatmentReason
FROM          dbo.ARVTreatmentTracker a
INNER JOIN patient p
ON
p.Id=a.PatientId         dbo.ARVTreatmentTracker




GO
/****** Object:  View [dbo].[PatientView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PatientView]
AS
Select	Ptn_Pk
	,	cast(decryptbykey(FirstName) As varchar(50))	As FirstName
	,	cast(decryptbykey(LastName) As varchar(50))		As LastName
	,	cast(decryptbykey(MiddleName) As varchar(50))	As MiddleName
	,	cast(decryptbykey(FirstName) As varchar(50)) + ' '+Isnull(cast(decryptbykey(MiddleName) As varchar(50)) ,'') + cast(decryptbykey(LastName) As varchar(50))		As PatientName
	,	LocationId
	,	IQNumber
	,	RegistrationDate
	,	DOB
	,	Case Sex
			When 16 Then 'Male'
			Else 'Female'
		End												As Sex
	,	DobPrecision
	,	DateOfDeath
	,	MaritalStatus
	,	Sex												As SexId
	,	Nullif(Convert(varchar(100), Decryptbykey([Address])),'') As [Address]
	,	Nullif(Convert(varchar(100), Decryptbykey(Phone)),'') As Phone
	,	PatientFacilityId
	,	UserId
	,	CreateDate
	,	UpdateDate
	,	DeleteFlag
	,	Status
From mst_Patient
Where (DeleteFlag = 0 Or DeleteFlag Is Null)

GO
/****** Object:  View [dbo].[PatientVisitView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create VIEW [dbo].[PatientVisitView]
AS
Select	V.Visit_Id		VisitId
	,	VisitName
	,	V.Ptn_Pk		PatientId
	,	V.LocationID	LocationId
	,	V.VisitDate
	,	Isnull(V.DataQuality,0) DataQuality
	,	Isnull(V.Signature,0) [Signature]
	,	V.CreateDate
	,	V.UserID		UserId
	,	Cast(Isnull(V.DeleteFlag,0) as bit) DeleteFlag
From Ord_visit V
Inner Join mst_VisitType T On T.VisitTypeID = V.VisitType
Where V.Ptn_Pk > 0


GO
/****** Object:  View [dbo].[PersonContactView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PersonContactView]
AS
SELECT        Id, PersonId, CAST(DECRYPTBYKEY(PhysicalAddress) AS VARCHAR(50)) AS PhysicalAddress, CAST(DECRYPTBYKEY(MobileNumber) AS VARCHAR(50)) AS MobileNumber, 
                         CAST(DECRYPTBYKEY(AlternativeNumber) AS VARCHAR(50)) AS AlternativeNumber, CAST(DECRYPTBYKEY(EmailAddress) AS VARCHAR(50)) AS EmailAddress, Active, DeleteFlag, CreatedBy, CreateDate
FROM            dbo.PersonContact


GO
/****** Object:  View [dbo].[PersonView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PersonView]
AS
SELECT        Id, CAST(DECRYPTBYKEY(FirstName) AS VARCHAR(50)) AS FirstName, CAST(DECRYPTBYKEY(MidName) AS VARCHAR(50)) AS MiddleName, CAST(DECRYPTBYKEY(LastName) AS VARCHAR(50)) AS LastName, 
                         Sex, Active, DeleteFlag, CreateDate, CreatedBy, AuditData
FROM            dbo.Person


GO
/****** Object:  View [dbo].[ServiceAreaBusinessRuleView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[ServiceAreaBusinessRuleView]
AS
Select	SR.Id
	,	SR.BusRuleId
	,	R.Name			As BusRuleName
	,	R.DeleteFlag	As BusRuleDeleteFlag
	,	R.ReferenceId	As BusRuleReferenceId
	,	SR.Value
	,	SR.Value1
	,	SR.SetType
	,	M.ModuleId		As ModuleId
	,	M.ModuleName	
	,	M.DisplayName	
	,	M.CanEnroll	
From lnk_ServiceBusinessRule As SR
Inner Join mst_module As M On M.ModuleId = SR.ModuleId
Inner Join Mst_BusinessRule As R On R.Id = SR.BusRuleId



GO
/****** Object:  View [dbo].[ServiceAreaFormView]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[ServiceAreaFormView]
AS
Select Distinct	F.FeatureID		As FeatureId
	,	F.FeatureName
	,	F.ReportFlag
	,	F.FeatureDeleteFlag	As FeatureDeleteFlag
	,	F.AdminFlag
	,	F.Published
	,	F.ModuleId
	,	F.MultiVisit
	,	F.RegistrationFormFlag
	,	F.ReferenceID	As ReferenceId
	,	F.CanLink
	,	F.FormId
	,	F.FormName
	,	F.FormDescription
	,	F.Custom
	,	F.CategoryId
	,	F.CategoryName Code
	,	F.FormDeleteFlag
	,	PermissionCount PermCount
From FormView As F
--Inner Join mst_VisitType As V On V.FeatureId = F.FeatureID
--Left Outer Join( Select D.ID, D.Name, C.CodeID, D.Code From mst_Decode D Inner Join mst_Code C On C.CodeID=D.CodeID And C.Name='Form Category') D On D.ID= V.CategoryId
Where (F.FeatureDeleteFlag = 0)
	And (F.AdminFlag = 0)
	And (F.FormDeleteFlag = 0)
	Or (F.FeatureDeleteFlag Is Null)
	And (F.AdminFlag Is Null)
	And (F.FormDeleteFlag Is Null)
Union
Select	Distinct F.FeatureID		As FeatureId
	,	F.FeatureName
	,	F.ReportFlag
	,	F.FeatureDeleteFlag
	,	F.AdminFlag
	,	F. Published
	,	L.ModuleId
	,	F.MultiVisit
	,	F.RegistrationFormFlag
	,	F.ReferenceId
	,	F.CanLink
	,	F.FormId
	,	F.FormName
	,	F.FormDescription
	,	F.Custom
	,	F.CategoryId
	,	F.CategoryName Code
	,	F.FormDeleteFlag
	,	PermissionCount PermCount
From FormView As F
--Inner Join mst_VisitType As V On V.FeatureId = F.FeatureID
Inner Join lnk_SplFormModule L On L.FeatureId = F.FeatureID
--Left Outer Join( Select D.ID, D.Name, C.CodeID, D.Code From mst_Decode D Inner Join mst_Code C On C.CodeID=D.CodeID And C.Name='Form Category') D On D.ID= V.CategoryId
Where (F.FeatureDeleteFlag = 0)
	And (F.AdminFlag = 0)
	And (F.FormDeleteFlag = 0)
	Or (F.FeatureDeleteFlag Is Null)
	And (F.AdminFlag Is Null)
	And (F.FormDeleteFlag Is Null)





GO
/****** Object:  View [dbo].[view_patientVisit]    Script Date: 5/9/2017 10:27:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[view_patientVisit]
AS
SELECT        TOP (100) PERCENT dbo.PatientMasterVisit.id AS visitID, 'Green Card' AS VisitName, dbo.PatientMasterVisit.patientId, dbo.PatientMasterVisit.visitDate, 
                         dbo.mst_User.UserName, dbo.PatientMasterVisit.DeleteFlag
FROM            dbo.PatientMasterVisit INNER JOIN
                         dbo.mst_User ON dbo.PatientMasterVisit.createdBy = dbo.mst_User.UserID
WHERE        (dbo.PatientMasterVisit.visitDate IS NOT NULL) AND (dbo.PatientMasterVisit.DeleteFlag IS NULL OR
                         dbo.PatientMasterVisit.DeleteFlag = 0 AND dbo.PatientMasterVisit.VisitType NOT IN(SELECT Id FROM LookupItem WHERE Name='Enrollment'))



GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "X"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 221
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'LookupItemView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'LookupItemView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PatientHivDiagnosis"
            Begin Extent = 
               Top = 12
               Left = 275
               Bottom = 142
               Right = 480
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "PatientTransferIn"
            Begin Extent = 
               Top = 18
               Left = 17
               Bottom = 148
               Right = 211
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PatientTreatmentInitiation"
            Begin Extent = 
               Top = 6
               Left = 513
               Bottom = 136
               Right = 723
            End
            DisplayFlags = 280
            TopColumn = 7
         End
         Begin Table = "PatientBaselineAssessment"
            Begin Extent = 
               Top = 6
               Left = 761
               Bottom = 136
               Right = 955
            End
            DisplayFlags = 280
            TopColumn = 12
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 31
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 2175
         Width = 1500
         Width = 1500
         Width = 2385
         Width = 2595
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Wi' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientBaselineView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'dth = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientBaselineView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientBaselineView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 275
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "b"
            Begin Extent = 
               Top = 7
               Left = 323
               Bottom = 170
               Right = 517
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 7
               Left = 565
               Bottom = 170
               Right = 759
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1200
         Width = 1980
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
         Width = 1200
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientPopulationView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientPopulationView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[22] 4[24] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[50] 4[13] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1[58] 4) )"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 1
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PatientIdentifier"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 232
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "LookupItemView"
            Begin Extent = 
               Top = 170
               Left = 276
               Bottom = 300
               Right = 459
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PatientEnrollment"
            Begin Extent = 
               Top = 25
               Left = 381
               Bottom = 155
               Right = 570
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Patient"
            Begin Extent = 
               Top = 81
               Left = 717
               Bottom = 211
               Right = 887
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Person"
            Begin Extent = 
               Top = 169
               Left = 482
               Bottom = 299
               Right = 652
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
      PaneHidden = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 2175
         Width = 1980
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin Criteria' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientServiceEnrollmentView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'Pane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientServiceEnrollmentView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientServiceEnrollmentView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PatientTreatmentSupporter"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 4
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientTreatmentSupporterView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PatientTreatmentSupporterView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PersonContact"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 228
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 11
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PersonContactView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PersonContactView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Person"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 8
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 13
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PersonView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'PersonView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[43] 4[12] 2[14] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PatientMasterVisit"
            Begin Extent = 
               Top = 12
               Left = 263
               Bottom = 204
               Right = 433
            End
            DisplayFlags = 280
            TopColumn = 5
         End
         Begin Table = "mst_User"
            Begin Extent = 
               Top = 20
               Left = 21
               Bottom = 149
               Right = 191
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 2190
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_patientVisit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'view_patientVisit'
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Mst_Drug]'))
EXEC dbo.sp_executesql @statement = N'
/*
 Created By Joseph Njung''e
 Return DrugList from mst_itemmaster
 ItemTypeID 300	 = Drugs
*/
CREATE VIEW [dbo].[Mst_Drug]
AS
SELECT        D.Item_PK AS Drug_pk, D.ItemCode AS DrugID, D.ItemTypeID, D.ItemName AS DrugName, D.DeleteFlag, D.CreatedBy AS UserID, D.CreateDate, D.UpdateDate, D.DispensingMargin, D.DispensingUnitPrice, 
                         D.FDACode, D.Manufacturer, D.MaxStock, D.MinStock, D.PurchaseUnitPrice, D.QtyPerPurchaseUnit, ISNULL(CC.ItemSellingPrice, 0) AS SellingUnitPrice, D.DispensingUnit, D.PurchaseUnit, CC.EffectiveDate, 
                         1 AS Sequence, D.ItemInstructions, D.abbreviation
FROM            dbo.Mst_ItemMaster AS D INNER JOIN
                         dbo.Mst_ItemType AS I ON I.ItemTypeID = D.ItemTypeID LEFT OUTER JOIN
                             (SELECT DISTINCT ItemId, ItemType, PriceStatus, ItemSellingPrice, EffectiveDate
                               FROM            dbo.lnk_ItemCostConfiguration) AS CC ON CC.ItemId = D.Item_PK AND CC.ItemType = D.ItemTypeID AND CC.PriceStatus = 1
WHERE        (I.ItemName = ''Pharmaceuticals'')

' 
GO
