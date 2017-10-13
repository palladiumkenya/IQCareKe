ALTER VIEW [dbo].[PatientTreatmentTrackerView]
AS
SELECT  a.Id, a.PatientId, p.ptn_pk, a.ServiceAreaId, a.PatientMasterVisitId, a.RegimenStartDate, a.RegimenId,
    (SELECT        Name + '(' + DisplayName + ')' AS Expr1
    FROM            dbo.LookupItem
    WHERE        (Id = a.RegimenId)) AS Regimen, a.RegimenLineId,
    (SELECT        Name
    FROM            dbo.LookupItem AS LookupItem_3
    WHERE        (Id = a.RegimenLineId)) AS RegimenLine, a.DrugId, a.RegimenStatusDate, a.TreatmentStatusId,
    (SELECT        Name
    FROM            dbo.LookupItem AS LookupItem_2
    WHERE        (Id = a.TreatmentStatusId)) AS TreatmentStatus, a.ParentId, a.CreateBy AS CreatedBy, a.CreateDate, a.DeleteFlag, a.TreatmentStatusReasonId,
    (SELECT        Name
    FROM            dbo.LookupItem AS LookupItem_1
    WHERE        (Id = a.TreatmentStatusReasonId)) AS TreatmentReason, dbo.ord_PatientPharmacyOrder.DispensedByDate, (SELECT top 1 isnull(M.visitDate,M.Start) FROM PATIENTMASTERVISIT M WHERE M.PatientId=a.PatientId) as VisitDate
FROM            dbo.ARVTreatmentTracker AS a INNER JOIN
dbo.Patient AS p ON p.Id = a.PatientId INNER JOIN
dbo.ord_PatientPharmacyOrder ON a.PatientMasterVisitId = dbo.ord_PatientPharmacyOrder.PatientMasterVisitId


UNION ALL

SELECT 
	
	
	
	R.RegimenMap_Pk,R.patientId,R.ptn_pk,0 as ServiceAreaId,0 as PatientmasterVisitId,
	(SELECT TOP 1  p.DispensedByDate FROM ord_PatientPharmacyOrder p WHERE p.VisitID=R.Visit_Pk) as RegimenStartDate,
	ISNULL((SELECT top 1 id FROM LookupItem WHERE Name IN(CASE R.RegimenType
			WHEN '3TC/NVP/TDF'	THEN CASE  WHEN R.age>=15 THEN 'AF2A' ELSE 'CF2A' END
			WHEN '3TC/AZT/NVP'	THEN CASE  WHEN R.age>=15 THEN 'AF1A' ELSE 'CF1A' END --'AZT + 3TC + NVP'
			WHEN '3TC/AZT/EFV'	THEN CASE  WHEN R.age>=15 THEN 'AF1B' ELSE 'CF1B' END--'AZT + 3TC + EFV '
			WHEN '3TC/AZT/LOPr' THEN CASE  WHEN R.age>=15 THEN 'AS1A' ELSE 'CS1A' END--'AZT + 3TC + LPV/r'
			WHEN '3TC/LOPr/TDF' THEN CASE  WHEN R.age>=15 THEN 'AF2F' ELSE 'CF4C' END--'TDF + 3TC + LPV/r'
			WHEN '3TC/ABC/EFV'	THEN CASE  WHEN R.age>=15 THEN 'AF2B' ELSE 'CF4B' END --'TDF + 3TC + EFV'
			WHEN '3TC/ABC/LOPr' THEN CASE  WHEN R.age>=15 THEN 'AS5A' ELSE 'CS2A' END --'ABC + 3TC + LPV/r'
			WHEN '3TC/ABC/NVP'	THEN CASE  WHEN R.age>=15 THEN 'AF4A' ELSE 'CF2A' END --'ABC + 3TC + NVP'
			WHEN '3TC/EFV/TDF'	THEN CASE  WHEN R.age>=15 THEN 'AF2B' ELSE 'CF4B' END --'TDF + 3TC + NVP'
			WHEN '3TC/NVP/AZT'  THEN CASE WHEN R.age>=15 THEN 'AF1A' ELSE 'CF1A' END --'AZT + 3TC + NVP'
		END)),0) as RegimenId,
	ISNULL((
		CASE R.RegimenType
			WHEN '3TC/NVP/TDF'	THEN CASE WHEN R.age>= 15 THEN (SELECT top 1 Name FROM lookupitem WHERE Name='AF2A')ELSE (SELECT top 1 Name FROM lookupitem WHERE Name='AF2A') END +'(TDF + 3TC + NVP)'
			WHEN '3TC/AZT/NVP'	THEN CASE WHEN R.age>= 15 THEN (SELECT top 1 Name FROM lookupitem WHERE Name='AF1A')ELSE (SELECT top 1 Name FROM lookupitem WHERE Name='CF1A')  END+'(AZT + 3TC + NVP)' 
			WHEN '3TC/AZT/EFV'	THEN CASE WHEN R.age>= 15 THEN (SELECT top 1 Name FROM lookupitem WHERE Name='AF1B')ELSE (SELECT top 1 Name FROM lookupitem WHERE Name='CF1B')  END+'(AZT + 3TC + EFV)'
			WHEN '3TC/AZT/LOPr' THEN CASE WHEN R.age>= 15 THEN (SELECT top 1 Name FROM lookupitem WHERE Name='AS1A')ELSE (SELECT top 1 Name FROM lookupitem WHERE Name='CS1A')  END+'(AZT + 3TC + LPV/r)'
			WHEN '3TC/LOPr/TDF' THEN CASE WHEN R.age>= 15 THEN (SELECT top 1 Name FROM lookupitem WHERE Name='AF2F')ELSE (SELECT top 1 Name FROM lookupitem WHERE Name='CF4C')  END+'(TDF + 3TC + LPV/r)'
			WHEN '3TC/ABC/EFV'	THEN CASE WHEN R.age>= 15 THEN (SELECT top 1 Name FROM lookupitem WHERE Name='AF2B')ELSE (SELECT top 1 Name FROM lookupitem WHERE Name='CF4B')  END+'(TDF + 3TC + EFV)'
			WHEN '3TC/ABC/LOPr' THEN CASE WHEN R.age>= 15 THEN (SELECT top 1 Name FROM lookupitem WHERE Name='AS5A')ELSE (SELECT top 1 Name FROM lookupitem WHERE Name='CS2A')  END+'(ABC + 3TC + LPV/r)'
			WHEN '3TC/ABC/NVP'	THEN CASE WHEN R.age>= 15 THEN (SELECT top 1 Name FROM lookupitem WHERE Name='AF4A')ELSE (SELECT top 1 Name FROM lookupitem WHERE Name='CF2A')  END+'(ABC + 3TC + NVP)'
			WHEN '3TC/EFV/TDF'	THEN CASE WHEN R.age>= 15 THEN (SELECT top 1 Name FROM lookupitem WHERE Name='AF2B')ELSE (SELECT top 1 Name FROM lookupitem WHERE Name='AF2B')  END+'(TDF + 3TC + EFV)'
			WHEN '3TC/NVP/AZT'  THEN CASE WHEN R.age>= 15 THEN (SELECT top 1 Name FROM lookupitem WHERE Name='AF1A')ELSE (SELECT top 1 Name FROM lookupitem WHERE Name='CF2A')  END+ '(AZT + 3TC + NVP)'
		END
	), (select TOP 1 Name from lookupitem where Name='Unknown')) as Regimen,
	ISNULL((SELECT top 1 id FROM LookupItem WHERE Name IN(SELECT 
	CASE MasterName 
	WHEN 'AdultFirstLineRegimen' THEN 'AdultARTFirstLine'
	WHEN 'AdultSecondlineRegimen' THEN 'AdultARTSecondLine'
	WHEN 'AdultThirdlineRegimen' THEN 'AdultARTThirdLine'
	WHEN 'PaedsFirstLineRegimen' THEN 'PaedsARTFirstLine'
	WHEN 'PaedsSecondlineRegimen' THEN 'PaedsARTSecondLine'
	WHEN 'PaedsThirdlineRegimen' THEN 'PaedsARTThirdLine' 
	END 
	
	FROM LookupItemView WHERE ItemName IN(CASE R.RegimenType
			WHEN '3TC/NVP/TDF'	THEN CASE  WHEN R.age>=15 THEN 'AF2A' ELSE 'CF2A' END
			WHEN '3TC/AZT/NVP'	THEN CASE  WHEN R.age>=15 THEN 'AF1A' ELSE 'CF1A' END --'AZT + 3TC + NVP'
			WHEN '3TC/AZT/EFV'	THEN CASE  WHEN R.age>=15 THEN 'AF1B' ELSE 'CF1B' END--'AZT + 3TC + EFV '
			WHEN '3TC/AZT/LOPr' THEN CASE  WHEN R.age>=15 THEN 'AS1A' ELSE 'CS1A' END--'AZT + 3TC + LPV/r'
			WHEN '3TC/LOPr/TDF' THEN CASE  WHEN R.age>=15 THEN 'AF2F' ELSE 'CF4C' END--'TDF + 3TC + LPV/r'
			WHEN '3TC/ABC/EFV'	THEN CASE  WHEN R.age>=15 THEN 'AF2B' ELSE 'CF4B' END --'TDF + 3TC + EFV'
			WHEN '3TC/ABC/LOPr' THEN CASE  WHEN R.age>=15 THEN 'AS5A' ELSE 'CS2A' END --'ABC + 3TC + LPV/r'
			WHEN '3TC/ABC/NVP'	THEN CASE  WHEN R.age>=15 THEN 'AF4A' ELSE 'CF2A' END --'ABC + 3TC + NVP'
			WHEN '3TC/EFV/TDF'	THEN CASE  WHEN R.age>=15 THEN 'AF2B' ELSE 'CF2B' END --'TDF + 3TC + NVP'
			WHEN '3TC/NVP/AZT'  THEN CASE WHEN R.age>=15 THEN 'AF1A' ELSE 'CF1A' END --'AZT + 3TC + NVP'
			--WHEN '3TC/NVP/TDF'	THEN 'TDF + 3TC + NVP'
			--WHEN '3TC/AZT/NVP'	THEN 'AZT + 3TC + NVP'
			--WHEN '3TC/AZT/EFV'	THEN 'AZT + 3TC + EFV '
			--WHEN '3TC/AZT/LOPr' THEN 'AZT + 3TC + LPV/r'
			--WHEN '3TC/LOPr/TDF' THEN 'TDF + 3TC + LPV/r'
			--WHEN '3TC/ABC/EFV'	THEN 'TDF + 3TC + EFV'
			--WHEN '3TC/ABC/LOPr' THEN 'ABC + 3TC + LPV/r'
			--WHEN '3TC/ABC/NVP'	THEN 'ABC + 3TC + NVP'
			--WHEN '3TC/EFV/TDF'	THEN 'TDF + 3TC + NVP'
			--WHEN '3TC/NVP/AZT' THEN  'AZT + 3TC + NVP'
		END))),(select TOP 1 Id from lookupitem where Name='Unknown')) as RegimenLineId,
	ISNULL((SELECT top 1 Name FROM LookupItem WHERE Name IN(SELECT 
	CASE MasterName 
	WHEN 'AdultFirstLineRegimen' THEN 'AdultARTFirstLine'
	WHEN 'AdultSecondlineRegimen' THEN 'AdultARTSecondLine'
	WHEN 'AdultThirdlineRegimen' THEN 'AdultARTThirdLine'
	WHEN 'PaedsFirstLineRegimen' THEN 'PaedsARTFirstLine'
	WHEN 'PaedsSecondlineRegimen' THEN 'PaedsARTSecondLine'
	WHEN 'PaedsThirdlineRegimen' THEN 'PaedsARTThirdLine' 
	END  

	FROM LookupItemView WHERE ItemName IN(CASE R.RegimenType
			WHEN '3TC/NVP/TDF'	THEN CASE  WHEN R.age>=15 THEN 'AF2A' ELSE 'CF2A' END
			WHEN '3TC/AZT/NVP'	THEN CASE  WHEN R.age>=15 THEN 'AF1A' ELSE 'CF1A' END --'AZT + 3TC + NVP'
			WHEN '3TC/AZT/EFV'	THEN CASE  WHEN R.age>=15 THEN 'AF1B' ELSE 'CF1B' END--'AZT + 3TC + EFV '
			WHEN '3TC/AZT/LOPr' THEN CASE  WHEN R.age>=15 THEN 'AS1A' ELSE 'CS1A' END--'AZT + 3TC + LPV/r'
			WHEN '3TC/LOPr/TDF' THEN CASE  WHEN R.age>=15 THEN 'AF2F' ELSE 'CF4C' END--'TDF + 3TC + LPV/r'
			WHEN '3TC/ABC/EFV'	THEN CASE  WHEN R.age>=15 THEN 'AF2B' ELSE 'CF4B' END --'TDF + 3TC + EFV'
			WHEN '3TC/ABC/LOPr' THEN CASE  WHEN R.age>=15 THEN 'AS5A' ELSE 'CS2A' END --'ABC + 3TC + LPV/r'
			WHEN '3TC/ABC/NVP'	THEN CASE  WHEN R.age>=15 THEN 'AF4A' ELSE 'CF2A' END --'ABC + 3TC + NVP'
			WHEN '3TC/EFV/TDF'	THEN CASE  WHEN R.age>=15 THEN 'AF2B' ELSE 'CF2B' END --'TDF + 3TC + NVP'
			WHEN '3TC/NVP/AZT'  THEN CASE WHEN R.age>=15 THEN 'AF1A' ELSE 'CF1A' END --'AZT + 3TC + NVP'
			--WHEN '3TC/NVP/TDF'	THEN 'TDF + 3TC + NVP'
			--WHEN '3TC/AZT/NVP'	THEN 'AZT + 3TC + NVP'
			--WHEN '3TC/AZT/EFV'	THEN 'AZT + 3TC + EFV '
			--WHEN '3TC/AZT/LOPr' THEN 'AZT + 3TC + LPV/r'
			--WHEN '3TC/LOPr/TDF' THEN 'TDF + 3TC + LPV/r'
			--WHEN '3TC/ABC/EFV'	THEN 'TDF + 3TC + EFV'
			--WHEN '3TC/ABC/LOPr' THEN 'ABC + 3TC + LPV/r'
			--WHEN '3TC/ABC/NVP'	THEN 'ABC + 3TC + NVP'
			--WHEN '3TC/EFV/TDF'	THEN 'TDF + 3TC + NVP'
			--WHEN '3TC/NVP/AZT' THEN  'AZT + 3TC + NVP'
		END))),(select TOP 1 Name from lookupitem where Name='Unknown')) as RegimenLine,
	NULL as DrugId,
	NULL as RegimenStatusDate,
	(
	 case R.RowNumber
		  When 1 then (SELECT top 1 ItemId FROM LookupItemView WHERE ItemName='Start Treatment') 
		  ELSE (SELECT top 1 ItemId FROM LookupItemView WHERE ItemName='Continue current treatment') 
	 end
	) as TreatmentStatusId,
	(
	  case R.RowNumber

		when 1 then 'Start Treatment'
		ELSE 'Continue Current Treatment'

	  end
	) as TreatmentStatus,
	0 as ParentId,R.UserID,R.VisitDate,R.DeleteFlag,0 as TreatmentStatusReasonid,NULL as TreatmentReason,
	(SELECT TOP 1 p.DispensedByDate FROM ord_PatientPharmacyOrder p WHERE p.VisitID=R.Visit_Pk ) as DispensedByDate, (SELECT top 1 D.visitDate FROM ord_Visit D Where D.ptn_pk=R.ptn_pk)


	FROM RegimenMapView R
	INNER JOIN ord_PatientPharmacyOrder o 
	ON
	o.VisitID=R.Visit_Pk
	 WHERE R.RegimenType<>'' AND R.RegimenType IS NOT NULL AND o.DispensedByDate IS NOT NULL