------------HEIVisitType
-- master
If Not Exists(Select 1 From LookupMaster where Name='OTZ_Modules') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('OTZ_Modules','OTZ_Modules',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Orientation - Operation Triple Zero') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Orientation - Operation Triple Zero','Orientation - Operation Triple Zero',0); End
If Not Exists(Select 1 From LookupItem where Name='OTZ Package') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('OTZ Package','OTZ Package',0); End
If Not Exists(Select 1 From LookupItem where Name='Leadership') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Leadership','Leadership',0); End
If Not Exists(Select 1 From LookupItem where Name='Decision Making') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Decision Making','Decision Making',0); End
If Not Exists(Select 1 From LookupItem where Name='OTZ Treatment Literacy') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('OTZ Treatment Literacy','OTZ Treatment Literacy',0); End
If Not Exists(Select 1 From LookupItem where Name='Transition to Adult care') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Transition to Adult care','Transition to Adult care',0); End


-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Orientation - Operation Triple Zero')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Orientation - Operation Triple Zero'),'Orientation - Operation Triple Zero',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='OTZ Package')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='OTZ Package'),'OTZ Package',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Leadership')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Leadership'),'Leadership',3); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Decision Making')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Decision Making'),'Decision Making',4); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='OTZ Treatment Literacy')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='OTZ Treatment Literacy'),'OTZ Treatment Literacy',5); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Transition to Adult care')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Transition to Adult care'),'Transition to Adult care',6); end 
