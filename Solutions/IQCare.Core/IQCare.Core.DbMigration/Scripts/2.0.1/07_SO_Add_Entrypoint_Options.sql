-- Add LookupItem
IF NOT EXISTS(SELECT * FROM LookupItem l WHERE l.Name='FP')
BEGIN
    INSERT INTO LookupItem (Name,DisplayName)VALUES('FP','FP')
END

IF NOT EXISTS(SELECT * FROM LookupItem l WHERE l.Name='PNC')
BEGIN
    INSERT INTO LookupItem (Name,DisplayName)VALUES('PNC','PNC')
END

-- INSERT INTO LookupMasterItem
IF NOT EXISTS(SELECT * FROM LookupItemView l WHERE l.MasterName='HTSEntryPoints' AND l.ItemName='FP')
BEGIN
    INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank)VALUES((SELECT top 1 m.Id FROM LookupMaster m WHERE m.Name='HTSEntryPoints'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='FP'),'FP',(SELECT (MAX(v.OrdRank)+1) FROM LookupItemView v WHERE v.MasterName='HTSEntryPoints'))
END

IF NOT EXISTS(SELECT * FROM LookupItemView l WHERE l.MasterName='HTSEntryPoints' AND l.ItemName='PNC')
BEGIN
    INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank)VALUES((SELECT top 1 m.Id FROM LookupMaster m WHERE m.Name='HTSEntryPoints'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='PNC'),'PNC',(SELECT (MAX(v.OrdRank)+1) FROM LookupItemView v WHERE v.MasterName='HTSEntryPoints'))
END

