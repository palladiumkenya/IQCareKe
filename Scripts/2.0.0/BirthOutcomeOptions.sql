If Not Exists(Select 1 From LookupMaster where Name='BirthOutcome') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('BirthOutcome','BirthOutcome',0); End

-- LOOKUPITEM
If Not Exists(Select 1 From LookupItem where Name='Live Birth') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Live Birth','Live Birth',0); End
If Not Exists(Select 1 From LookupItem where Name='Fresh Still Birth') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Fresh Still Birth','Fresh Still Birth',0); End
If Not Exists(Select 1 From LookupItem where Name='Macerated Still Birth') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Macerated Still Birth','Macerated Still Birth',0); End


--LOOKUPMASTERITEM
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='BirthOutcome') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Live Birth')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='BirthOutcome'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Live Birth'),'Live Birth',1) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='BirthOutcome') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Fresh Still Birth')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='BirthOutcome'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Fresh Still Birth'),'Fresh Still Birth',2) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='BirthOutcome') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Macerated Still Birth')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='BirthOutcome'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Macerated Still Birth'),'Macerated Still Birth',3) end;
