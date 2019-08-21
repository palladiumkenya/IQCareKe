
DELETE w
FROM LookupMasterItem w
INNER JOIN LookupItem e
  ON e.Id=w.LookupItemId
  INNER JOIN LookupMaster lm on
  lm.Id=w.LookupMasterId
WHERE lm.[Name]='PrEP_Status' and e.Name='Substitute'

go

DELETE w
FROM LookupMasterItem w
INNER JOIN LookupItem e
  ON e.Id=w.LookupItemId
  INNER JOIN LookupMaster lm on
  lm.Id=w.LookupMasterId
WHERE lm.[Name]='PrEP_Status' and e.Name='Status'


