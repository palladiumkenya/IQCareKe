If Not Exists(Select 1 From LookupMaster where Name='TracingReasonNotContacted') 
Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('TracingReasonNotContacted','TracingReasonNotContacted',0); End


-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='TracingReasonNotContacted') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='No locator information in record')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='TracingReasonNotContacted'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='No locator information in record'),'No locator information in record',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='TracingReasonNotContacted') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Incorrect locator information in record')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='TracingReasonNotContacted'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Incorrect locator information in record'),'Incorrect locator information in record',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='TracingReasonNotContacted') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Migrated from reported location')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='TracingReasonNotContacted'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Migrated from reported location'),'Migrated from reported location',3); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='TracingReasonNotContacted') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Not found at home')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='TracingReasonNotContacted'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Not found at home'),'Not found at home',4); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='TracingReasonNotContacted') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Died')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='TracingReasonNotContacted'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Died'),'Died',5); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='TracingReasonNotContacted') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='TracingReasonNotContacted'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other'),'Other',6); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='TracingReasonNotContacted') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Mteja, calls not going through, not picking calls')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='TracingReasonNotContacted'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Mteja, calls not going through, not picking calls'),'Mteja, calls not going through, not picking calls',7); end 