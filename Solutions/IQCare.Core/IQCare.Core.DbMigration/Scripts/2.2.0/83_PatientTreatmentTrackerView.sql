ALTER VIEW [dbo].[PatientTreatmentTrackerView]
AS
SELECT a.Id, a.PatientId, p.ptn_pk, a.ServiceAreaId, a.PatientMasterVisitId, a.RegimenStartDate, a.RegimenId,
(SELECT  Name + '(' + DisplayName + ')' AS Expr1
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
                               WHERE        (Id = a.TreatmentStatusReasonId)) AS TreatmentReason, dbo.ord_PatientPharmacyOrder.DispensedByDate,
                             (SELECT        TOP 1 isnull(M.visitDate, M.Start)
                               FROM            PATIENTMASTERVISIT M
                               WHERE        M.PatientId = a.PatientId) AS VisitDate
FROM            dbo.ARVTreatmentTracker AS a INNER JOIN
                         dbo.Patient AS p ON p.Id = a.PatientId INNER JOIN
                         dbo.ord_PatientPharmacyOrder ON a.PatientMasterVisitId = dbo.ord_PatientPharmacyOrder.PatientMasterVisitId
UNION ALL
SELECT        R.RegimenMap_Pk, R.patientId, R.ptn_pk, 0 AS ServiceAreaId, 0 AS PatientmasterVisitId,
(SELECT TOP 1 p.DispensedByDate  FROM  ord_PatientPharmacyOrder p WHERE p.VisitID = R.Visit_Pk) AS RegimenStartDate, 

ISNULL((SELECT TOP 1 id FROM LookupItem WHERE Name IN 
--(CASE R.RegimenType 
(CASE (isnull(ascii(SUBSTRING(R.regimentype, 1, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 2, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 3, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 4, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 5, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 6, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 7, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 8, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 9, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 10, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 11, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 12, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 13, 1)),0))
WHEN 779 /*'3TC/AZT/NVP'*/ THEN CASE WHEN R.age >= 15 THEN 'AF1A' ELSE 'CF1A' END /*'AZT + 3TC + NVP'*/ 
WHEN 760 /*'3TC/AZT/EFV'*/ THEN CASE WHEN R.age >= 15 THEN 'AF1B' ELSE 'CF1B' END /*'AZT + 3TC + EFV '*/
WHEN 758/*'3TC/AZT/DTG'*/ THEN CASE WHEN R.age >= 15 THEN 'AF1D' END /*'AZT + 3TC + DTG '*/
WHEN 762 /*'3TC/NVP/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2A' ELSE 'CF4A' END /*TDF + 3TC + NVP*/
WHEN 743 /*'3TC/EFV/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2B' ELSE 'CF4B' END /*'TDF + 3TC + EFV'*/ 
  --WHEN 914 /*'3TC/ATV/r/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2D' ELSE 'CF4D' END /*'TDF + 3TC + ATV/r'*/
  --WHEN 753 /*'3TC/ATV/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2D' ELSE 'CF4D' END /*'TDF + 3TC + ATV/r'*/
  WHEN 741 /*'3TC/DTG/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2E' END /*'TDF + 3TC + DTG'*/
  WHEN 867 /*'3TC/LOPr/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2F' ELSE 'CF4C' END /*'TDF + 3TC + LPV/r'*/ 
  WHEN 921 /*'3TC/LPV/r/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2F' ELSE 'CF4C' END /*'TDF + 3TC + LPV/r'*/
  --WHEN 741 /*'3TC/RAL/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2G' END /*'TDF + 3TC + RAL'*/
WHEN 933 /*'FTC/ATV/r/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2H' END /*'TDF + FTC + ATV/r'*/
WHEN 738 /*'3TC/ABC/NVP'*/ THEN CASE WHEN R.age >= 15 THEN 'AF4A' ELSE 'CF2A' END /*'ABC + 3TC + NVP'*/
WHEN 719 /*'3TC/ABC/EFV'*/ THEN CASE WHEN R.age >= 15 THEN 'AF4B' ELSE 'CF2B' END /*'ABC + 3TC + EFV'*/
  WHEN 717 /*'3TC/ABC/DTG'*/ THEN CASE WHEN R.age >= 15 THEN 'AF4C' WHEN R.age < 15 THEN 'CF2F' ELSE 'CF2G' END /*'ABC + 3TC + DTG'*/
WHEN 938 /*'3TC/AZT/LPV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS1A' ELSE 'CS1A' END /*'AZT + 3TC + LPV/r'*/ 
WHEN 884 /*'3TC/AZT/LOPr'*/ THEN CASE WHEN R.age >= 15 THEN 'AS1A' ELSE 'CS1A' END /*'AZT + 3TC + LPV/r'*/ 
WHEN 770 /*'3TC/AZT/ATV'*/ THEN CASE WHEN R.age >= 15 THEN 'AS1B' ELSE 'CS1B' END /*'AZT + 3TC + ATV/r'*/ 
WHEN 931 /*'3TC/AZT/ATV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS1B' ELSE 'CS1B' END /*'AZT + 3TC + ATV/r'*/
  WHEN 921 /*'3TC/TDF/LPV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS2A' END /*'TDF + 3TC + LPV/r'*/
  WHEN 867 /*'3TC/TDF/LOPr'*/ THEN CASE WHEN R.age >= 15 THEN 'AS2A' END /*'TDF + 3TC + LPV/r'*/
  WHEN 753 /*'3TC/TDF/ATV'*/ THEN CASE WHEN R.age >= 15 THEN 'AS2C' END /*'TDF + 3TC + ATV/r'*/ 
  WHEN 914 /*'3TC/TDF/ATV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS2C' END /*'TDF + 3TC + ATV/r'*/
WHEN 843 /*'3TC/ABC/LOPr'*/ THEN CASE WHEN R.age >= 15 THEN 'AS5A' ELSE 'CS2A' END /*'ABC + 3TC + LPV/r'*/ 
WHEN 897 /*'3TC/ABC/LPV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS5A' ELSE 'CS2A' END /*'ABC + 3TC + LPV/r'*/ 
WHEN 890 /*'3TC/ABC/ATV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS5B' ELSE 'CS2C' END /*'ABC + 3TC + ATV/r'*/
WHEN 729 /*'3TC/ABC/ATV'*/ THEN CASE WHEN R.age >= 15 THEN 'AS5B' ELSE 'CS2C' END /*'ABC + 3TC + ATV/r'*/
WHEN 758 THEN CASE WHEN R.age < 15 THEN 'CF1E' END
END)), 0) AS RegimenId, 

ISNULL(
--(CASE R.RegimenType 
(CASE (isnull(ascii(SUBSTRING(R.regimentype, 1, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 2, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 3, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 4, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 5, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 6, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 7, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 8, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 9, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 10, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 11, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 12, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 13, 1)),0))
WHEN 779/*'3TC/AZT/NVP'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AF1A') ELSE(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CF1A') END + '(AZT + 3TC + NVP)' 
WHEN 760/*'3TC/AZT/EFV'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AF1B') ELSE(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CF1B') END + '(AZT + 3TC + EFV)'
WHEN 758/*'3TC/AZT/DTG'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AF1D') END + '(AZT + 3TC + DTG)'
WHEN 762/*'3TC/NVP/TDF'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AF2A') ELSE(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CF4A') END + '(TDF + 3TC + NVP)' 
WHEN 743/*'3TC/EFV/TDF'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AF2B') ELSE(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CF4B') END + '(TDF + 3TC + EFV)'
WHEN 741/*'3TC/DTG/TDF'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AF2E') END + '(TDF + 3TC + DTG)'
WHEN 867/*'3TC/LOPr/TDF'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AF2F') ELSE(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CF4C') END + '(TDF + 3TC + LPV/r)' 
WHEN 921/*'3TC/LPV/r/TDF'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AF2F') ELSE(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CF4C') END + '(TDF + 3TC + LPV/r)'
WHEN 933/*'FTC/ATV/r/TDF'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AF2H') END + '(TDF + FTC + ATV/r)'
WHEN 738/*'3TC/ABC/NVP'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AF4A') ELSE(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CF2A') END + '(ABC + 3TC + NVP)'
WHEN 719/*'3TC/ABC/EFV'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AF4B') ELSE(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CF2B') END + '(ABC + 3TC + EFV)'
WHEN 717/*'3TC/ABC/DTG'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AF4C') WHEN R.age < 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CF2F') END + '(ABC + 3TC + DTG)'
WHEN 938/*'3TC/AZT/LPV/r'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AS1A') ELSE(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CS1A') END + '(AZT + 3TC + LPV/r)'
WHEN 884/*'3TC/AZT/LOPr'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AS1A') ELSE(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CS1A') END + '(AZT + 3TC + LPV/r)' 
WHEN 770/*'3TC/AZT/ATV'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AS1B') ELSE(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CS1B') END + '(AZT + 3TC + ATV/r)'
WHEN 931/*'3TC/AZT/ATV/r'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AS1B') ELSE(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CS1B') END + '(AZT + 3TC + ATV/r)'
WHEN 921/*'3TC/TDF/LPV/r'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AS2A') END + '(TDF + 3TC + LPV/r)'
WHEN 867/*'3TC/TDF/LOPr'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AS2A') END + '(TDF + 3TC + LPV/r)'
WHEN 753/*'3TC/TDF/ATV'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AS2C') END + '(TDF + 3TC + ATV/r)'
WHEN 914/*'3TC/TDF/ATV/r'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AS2C') END + '(TDF + 3TC + ATV/r)'
WHEN 843/*'3TC/ABC/LOPr'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AS5A') ELSE(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CS2A') END + '(ABC + 3TC + LPV/r)' 
WHEN 897/*'3TC/ABC/LPV/r'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AS5A') ELSE(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CS2A') END + '(ABC + 3TC + LPV/r)' 
WHEN 890/*'3TC/ABC/ATV/r'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AS5B') ELSE(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CS2C') END + '(ABC + 3TC + ATV/r)' 
WHEN 729/*'3TC/ABC/ATV'*/ THEN CASE WHEN R.age >= 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'AS5B') ELSE(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CS2C') END + '(ABC + 3TC + ATV/r)'
WHEN 758 THEN CASE WHEN R.age < 15 THEN (SELECT TOP 1 Name FROM lookupitem WHERE Name = 'CF1E') END

END), 
(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'Unknown')) AS Regimen, 

ISNULL((SELECT TOP 1 id FROM LookupItem WHERE Name IN (SELECT CASE MasterName 
WHEN 'AdultFirstLineRegimen' THEN 'AdultARTFirstLine' WHEN 'AdultSecondlineRegimen' THEN 'AdultARTSecondLine' WHEN 'AdultThirdlineRegimen' THEN 'AdultARTThirdLine' WHEN 'PaedsFirstLineRegimen' THEN 'PaedsARTFirstLine' WHEN 'PaedsSecondlineRegimen' THEN 'PaedsARTSecondLine' WHEN 'PaedsThirdlineRegimen' THEN 'PaedsARTThirdLine' END
FROM LookupItemView
WHERE ItemName IN 
--(CASE R.RegimenType 
(CASE (isnull(ascii(SUBSTRING(R.regimentype, 1, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 2, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 3, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 4, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 5, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 6, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 7, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 8, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 9, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 10, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 11, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 12, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 13, 1)),0)) 
WHEN 779 /*'3TC/AZT/NVP'*/ THEN CASE WHEN R.age >= 15 THEN 'AF1A' ELSE 'CF1A' END /*'AZT + 3TC + NVP'*/ 
WHEN 760 /*'3TC/AZT/EFV'*/ THEN CASE WHEN R.age >= 15 THEN 'AF1B' ELSE 'CF1B' END /*'AZT + 3TC + EFV '*/
WHEN 758/*'3TC/AZT/DTG'*/ THEN CASE WHEN R.age >= 15 THEN 'AF1D' END /*'AZT + 3TC + DTG '*/
WHEN 762 /*'3TC/NVP/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2A' ELSE 'CF4A' END /*TDF + 3TC + NVP*/
WHEN 743 /*'3TC/EFV/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2B' ELSE 'CF4B' END /*'TDF + 3TC + EFV'*/ 
  --WHEN 914 /*'3TC/ATV/r/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2D' ELSE 'CF4D' END /*'TDF + 3TC + ATV/r'*/
  --WHEN 753 /*'3TC/ATV/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2D' ELSE 'CF4D' END /*'TDF + 3TC + ATV/r'*/
  WHEN 741 /*'3TC/DTG/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2E' END /*'TDF + 3TC + DTG'*/
  WHEN 867 /*'3TC/LOPr/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2F' ELSE 'CF4C' END /*'TDF + 3TC + LPV/r'*/ 
  WHEN 921 /*'3TC/LPV/r/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2F' ELSE 'CF4C' END /*'TDF + 3TC + LPV/r'*/
  --WHEN 741 /*'3TC/RAL/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2G' END /*'TDF + 3TC + RAL'*/
WHEN 933 /*'FTC/ATV/r/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2H' END /*'TDF + FTC + ATV/r'*/
WHEN 738 /*'3TC/ABC/NVP'*/ THEN CASE WHEN R.age >= 15 THEN 'AF4A' ELSE 'CF2A' END /*'ABC + 3TC + NVP'*/
WHEN 719 /*'3TC/ABC/EFV'*/ THEN CASE WHEN R.age >= 15 THEN 'AF4B' ELSE 'CF2B' END /*'ABC + 3TC + EFV'*/
  WHEN 717 /*'3TC/ABC/DTG'*/ THEN CASE WHEN R.age >= 15 THEN 'AF4C' WHEN R.age < 15 THEN 'CF2F' ELSE 'CF2G' END /*'ABC + 3TC + DTG'*/
WHEN 938 /*'3TC/AZT/LPV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS1A' ELSE 'CS1A' END /*'AZT + 3TC + LPV/r'*/ 
WHEN 884 /*'3TC/AZT/LOPr'*/ THEN CASE WHEN R.age >= 15 THEN 'AS1A' ELSE 'CS1A' END /*'AZT + 3TC + LPV/r'*/ 
WHEN 770 /*'3TC/AZT/ATV'*/ THEN CASE WHEN R.age >= 15 THEN 'AS1B' ELSE 'CS1B' END /*'AZT + 3TC + ATV/r'*/ 
WHEN 931 /*'3TC/AZT/ATV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS1B' ELSE 'CS1B' END /*'AZT + 3TC + ATV/r'*/
  WHEN 921 /*'3TC/TDF/LPV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS2A' END /*'TDF + 3TC + LPV/r'*/
  WHEN 867 /*'3TC/TDF/LOPr'*/ THEN CASE WHEN R.age >= 15 THEN 'AS2A' END /*'TDF + 3TC + LPV/r'*/
  WHEN 753 /*'3TC/TDF/ATV'*/ THEN CASE WHEN R.age >= 15 THEN 'AS2C' END /*'TDF + 3TC + ATV/r'*/ 
  WHEN 914 /*'3TC/TDF/ATV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS2C' END /*'TDF + 3TC + ATV/r'*/
WHEN 843 /*'3TC/ABC/LOPr'*/ THEN CASE WHEN R.age >= 15 THEN 'AS5A' ELSE 'CS2A' END /*'ABC + 3TC + LPV/r'*/ 
WHEN 897 /*'3TC/ABC/LPV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS5A' ELSE 'CS2A' END /*'ABC + 3TC + LPV/r'*/ 
WHEN 890 /*'3TC/ABC/ATV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS5B' ELSE 'CS2C' END /*'ABC + 3TC + ATV/r'*/
WHEN 729 /*'3TC/ABC/ATV'*/ THEN CASE WHEN R.age >= 15 THEN 'AS5B' ELSE 'CS2C' END /*'ABC + 3TC + ATV/r'*/ 
WHEN 758 THEN CASE WHEN R.age < 15 THEN 'CF1E' END

END))),
(SELECT TOP 1 Id FROM lookupitem WHERE Name = 'Unknown')) AS RegimenLineId, 

ISNULL((SELECT  TOP 1 Name FROM LookupItem WHERE Name IN (SELECT CASE MasterName WHEN 'AdultFirstLineRegimen' THEN 'AdultARTFirstLine' WHEN 'AdultSecondlineRegimen' THEN 'AdultARTSecondLine' WHEN 'AdultThirdlineRegimen' THEN 'AdultARTThirdLine' WHEN 'PaedsFirstLineRegimen' THEN 'PaedsARTFirstLine' WHEN 'PaedsSecondlineRegimen' THEN 'PaedsARTSecondLine' WHEN 'PaedsThirdlineRegimen' THEN 'PaedsARTThirdLine' END FROM LookupItemView WHERE ItemName IN 
(CASE (isnull(ascii(SUBSTRING(R.regimentype, 1, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 2, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 3, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 4, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 5, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 6, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 7, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 8, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 9, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 10, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 11, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 12, 1)),0) +
	   isnull(ascii(SUBSTRING(R.regimentype, 13, 1)),0)) 
WHEN 779 /*'3TC/AZT/NVP'*/ THEN CASE WHEN R.age >= 15 THEN 'AF1A' ELSE 'CF1A' END /*'AZT + 3TC + NVP'*/ 
WHEN 760 /*'3TC/AZT/EFV'*/ THEN CASE WHEN R.age >= 15 THEN 'AF1B' ELSE 'CF1B' END /*'AZT + 3TC + EFV '*/
WHEN 758/*'3TC/AZT/DTG'*/ THEN CASE WHEN R.age >= 15 THEN 'AF1D' END /*'AZT + 3TC + DTG '*/
WHEN 762 /*'3TC/NVP/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2A' ELSE 'CF4A' END /*TDF + 3TC + NVP*/
WHEN 743 /*'3TC/EFV/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2B' ELSE 'CF4B' END /*'TDF + 3TC + EFV'*/ 
  --WHEN 914 /*'3TC/ATV/r/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2D' ELSE 'CF4D' END /*'TDF + 3TC + ATV/r'*/
  --WHEN 753 /*'3TC/ATV/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2D' ELSE 'CF4D' END /*'TDF + 3TC + ATV/r'*/
  WHEN 741 /*'3TC/DTG/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2E' END /*'TDF + 3TC + DTG'*/
  WHEN 867 /*'3TC/LOPr/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2F' ELSE 'CF4C' END /*'TDF + 3TC + LPV/r'*/ 
  WHEN 921 /*'3TC/LPV/r/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2F' ELSE 'CF4C' END /*'TDF + 3TC + LPV/r'*/
  --WHEN 741 /*'3TC/RAL/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2G' END /*'TDF + 3TC + RAL'*/
WHEN 933 /*'FTC/ATV/r/TDF'*/ THEN CASE WHEN R.age >= 15 THEN 'AF2H' END /*'TDF + FTC + ATV/r'*/
WHEN 738 /*'3TC/ABC/NVP'*/ THEN CASE WHEN R.age >= 15 THEN 'AF4A' ELSE 'CF2A' END /*'ABC + 3TC + NVP'*/
WHEN 719 /*'3TC/ABC/EFV'*/ THEN CASE WHEN R.age >= 15 THEN 'AF4B' ELSE 'CF2B' END /*'ABC + 3TC + EFV'*/
  WHEN 717 /*'3TC/ABC/DTG'*/ THEN CASE WHEN R.age >= 15 THEN 'AF4C' WHEN R.age < 15 THEN 'CF2F' ELSE 'CF2G' END /*'ABC + 3TC + DTG'*/
WHEN 938 /*'3TC/AZT/LPV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS1A' ELSE 'CS1A' END /*'AZT + 3TC + LPV/r'*/ 
WHEN 884 /*'3TC/AZT/LOPr'*/ THEN CASE WHEN R.age >= 15 THEN 'AS1A' ELSE 'CS1A' END /*'AZT + 3TC + LPV/r'*/ 
WHEN 770 /*'3TC/AZT/ATV'*/ THEN CASE WHEN R.age >= 15 THEN 'AS1B' ELSE 'CS1B' END /*'AZT + 3TC + ATV/r'*/ 
WHEN 931 /*'3TC/AZT/ATV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS1B' ELSE 'CS1B' END /*'AZT + 3TC + ATV/r'*/
  WHEN 921 /*'3TC/TDF/LPV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS2A' END /*'TDF + 3TC + LPV/r'*/
  WHEN 867 /*'3TC/TDF/LOPr'*/ THEN CASE WHEN R.age >= 15 THEN 'AS2A' END /*'TDF + 3TC + LPV/r'*/
  WHEN 753 /*'3TC/TDF/ATV'*/ THEN CASE WHEN R.age >= 15 THEN 'AS2C' END /*'TDF + 3TC + ATV/r'*/ 
  WHEN 914 /*'3TC/TDF/ATV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS2C' END /*'TDF + 3TC + ATV/r'*/
WHEN 843 /*'3TC/ABC/LOPr'*/ THEN CASE WHEN R.age >= 15 THEN 'AS5A' ELSE 'CS2A' END /*'ABC + 3TC + LPV/r'*/ 
WHEN 897 /*'3TC/ABC/LPV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS5A' ELSE 'CS2A' END /*'ABC + 3TC + LPV/r'*/ 
WHEN 890 /*'3TC/ABC/ATV/r'*/ THEN CASE WHEN R.age >= 15 THEN 'AS5B' ELSE 'CS2C' END /*'ABC + 3TC + ATV/r'*/
WHEN 729 /*'3TC/ABC/ATV'*/ THEN CASE WHEN R.age >= 15 THEN 'AS5B' ELSE 'CS2C' END /*'ABC + 3TC + ATV/r'*/ 
WHEN 758 THEN CASE WHEN R.age < 15 THEN 'CF1E' END

END))),
(SELECT TOP 1 Name FROM lookupitem WHERE Name = 'Unknown')) AS RegimenLine, 
NULL AS DrugId, NULL AS RegimenStatusDate, (CASE R.RowNumber WHEN 1 THEN
                             (SELECT        TOP 1 ItemId
                               FROM            LookupItemView
                               WHERE        ItemName = 'Start Treatment') ELSE
                             (SELECT        TOP 1 ItemId
                               FROM            LookupItemView
                               WHERE        ItemName = 'Continue current treatment') END) AS TreatmentStatusId, 
                         (CASE R.RowNumber WHEN 1 THEN 'Start Treatment' ELSE 'Continue Current Treatment' END) AS TreatmentStatus, 0 AS ParentId, R.UserID, R.VisitDate, 
                         R.DeleteFlag, 0 AS TreatmentStatusReasonid, NULL AS TreatmentReason,
                             (SELECT        TOP 1 p.DispensedByDate
                               FROM            ord_PatientPharmacyOrder p
                               WHERE        p.VisitID = R.Visit_Pk) AS DispensedByDate,
                             (SELECT        TOP 1 D .visitDate
                               FROM            ord_Visit D
                               WHERE        D .ptn_pk = R.ptn_pk)
FROM            RegimenMapView R INNER JOIN
                         ord_PatientPharmacyOrder o ON o.VisitID = R.Visit_Pk
WHERE        R.RegimenType <> '' AND R.RegimenType IS NOT NULL AND o.DispensedByDate IS NOT NULL


