--add AF5X
If Not Exists(Select 1 From LookupItem where Name='AF5X') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('AF5X','Any other 1st line Adult regimens',0); End

-- link to masteritem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='AdultFirstLineRegimen') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='AF5X')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='AdultFirstLineRegimen'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='AF5X'),'Any other 1st line Adult regimens',14); end

--add AF5X
If Not Exists(Select 1 From LookupItem where Name='CF5X') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('CF5X','Any other 1st line Paediatric regimens',0); End
If Not Exists(Select 1 From LookupItem where Name='CF1E') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('CF1E','AZT + 3TC + RAL',0); End
If Not Exists(Select 1 From LookupItem where Name='CF2F') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('CF2F','ABC + 3TC + RAL',0); End
If Not Exists(Select 1 From LookupItem where Name='CF2G') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('CF2G','ABC + 3TC + DTG',0); End
If Not Exists(Select 1 From LookupItem where Name='CS1C') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('CS1C','AZT + 3TC + RAL',0); End

-- link to masteritem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PaedsFirstLineRegimen') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='CF5X')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PaedsFirstLineRegimen'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='CF5X'),'Any other 1st line Paediatric regimens',16); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PaedsFirstLineRegimen') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='CF1E')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PaedsFirstLineRegimen'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='CF1E'),'AZT + 3TC + RAL',17); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PaedsFirstLineRegimen') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='CF2F')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PaedsFirstLineRegimen'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='CF2F'),'ABC + 3TC + RAL',18); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PaedsFirstLineRegimen') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='CF2G')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PaedsFirstLineRegimen'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='CF2G'),'ABC + 3TC + DTG',19); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PaedsSecondlineRegimen') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='CS1C')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PaedsSecondlineRegimen'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='CS1C'),'AZT + 3TC + RAL',6); end

