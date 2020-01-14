-- master
If Not Exists(Select 1 From LookupMaster where Name='OTZ_CareEndedOptions') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('OTZ_CareEndedOptions','OTZ_CareEndedOptions',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Opt out of OTZ') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Opt out of OTZ','Opt out of OTZ',0); End
If Not Exists(Select 1 From LookupItem where Name='Transition to Adult care') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Transition to Adult care','Transition to Adult care',0); End


-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_CareEndedOptions') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Opt out of OTZ')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_CareEndedOptions'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Opt out of OTZ'),'Opt out of OTZ',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_CareEndedOptions') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Transition to Adult care')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_CareEndedOptions'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Transition to Adult care'),'Transition to Adult care',2); end 