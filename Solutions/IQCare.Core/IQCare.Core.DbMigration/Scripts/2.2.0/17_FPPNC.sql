------------HTSEntryPoints
-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='FP') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('FP','FP',0); End
If Not Exists(Select 1 From LookupItem where Name='PNC') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('PNC','PNC',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='HTSEntryPoints') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='FP')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='HTSEntryPoints'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='FP'),'FP',18); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='HTSEntryPoints') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PNC')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='HTSEntryPoints'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PNC'),'PNC',19); end 
