
If Not Exists(Select 1 From LookupMaster where Name='EMRImplementation') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('EMRImplementation','Mode of EMR Implementation',0); End


If Not Exists(Select 1 From LookupItem where Name='RDE') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('RDE','Retrospective Data Entry',0); End
If Not Exists(Select 1 From LookupItem where Name='POC') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('POC','Point of Care',0); End




If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='EMRImplementation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='RDE')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='EMRImplementation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='RDE'),'Retrospective Data Entry',1); end



If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='EMRImplementation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='POC')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='EMRImplementation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='POC'),'Point of Care',2); end


