IF OBJECT_ID('dbo.PharmacyVisitDetailsView', 'V') IS NOT NULL
    DROP VIEW [dbo].[PharmacyVisitDetailsView]
GO


CREATE VIEW  [dbo].[PharmacyVisitDetailsView]
AS
SELECT t.*,CASE WHEN t.[Status] ='1' THEN 'New Order'
WHEN t.[Status] =2 THEN 'Complete' 
WHEN t.[Status]=3 THEN 'Partial' END AS OrderStatusText FROM (
SELECT DISTINCT a.patientmastervisitid VisitID,b.displayname VisitName,a.patientid,
(CASE b.displayname 
	WHEN 'Pharmacy' THEN CONVERT(VARCHAR(50),ISNULL((SELECT TOP 1 DATEADD(d,DATEDIFF(d,0,ISNULL(orderedbydate,dispensedbydate)),0) FROM ord_patientpharmacyorder WHERE patientmastervisitid = a.patientmastervisitid),'0'))
	WHEN 'Lab' THEN CONVERT(VARCHAR(50),ISNULL((SELECT TOP 1 DATEADD(d,DATEDIFF(d,0,OrderDate),0) FROM ord_laborder WHERE patientmastervisitid = a.patientmastervisitid),'0'))
	WHEN 'CCC' THEN CONVERT(VARCHAR(50),ISNULL((SELECT TOP 1 DATEADD(d,DATEDIFF(d,0,visitdate),0) FROM PatientMasterVisit WHERE id = a.patientmastervisitid),'0'))
	WHEN 'Triage' THEN CONVERT(VARCHAR(50),ISNULL((SELECT TOP 1 DATEADD(d,DATEDIFF(d,0,visitdate),0) FROM PatientVitals WHERE patientmastervisitid = a.patientmastervisitid),'0'))
	WHEN 'Care Ended'  THEN (SELECT [dbo].[fn_GetPatientVisitDate](a.PatientId,a.PatientMasterVisitId))
	ELSE DATEADD(d,DATEDIFF(d,0,a.encounterstarttime),0)
	END) VisitDate, 

c.username AS UserName, a.deleteflag AS DeleteFlag,
(CASE b.displayname 
	WHEN 'Pharmacy' THEN CONVERT(VARCHAR(50),ISNULL((SELECT TOP 1 orderstatus FROM ord_patientpharmacyorder WHERE patientmastervisitid = a.patientmastervisitid),'0'))
	WHEN 'Lab' THEN CONVERT(VARCHAR(50),ISNULL((SELECT TOP 1 orderstatus FROM ord_laborder WHERE patientmastervisitid = a.patientmastervisitid),'0'))
	ELSE '0'
	END) [Status]
	
FROM patientencounter a INNER JOIN lookupitem b ON a.encountertypeid = b.id
INNER JOIN mst_user c ON a.createdby = c.userid
INNER JOIN PatientMasterVisit d ON a.patientmastervisitid = d.id
WHERE -- a.patientid =@PatientID   
 b.Name='Pharmacy-encounter' AND 
 d.visitdate IS NOT NULL 
 and (a.DeleteFlag is null or a.DeleteFlag  = 0)
 )t 