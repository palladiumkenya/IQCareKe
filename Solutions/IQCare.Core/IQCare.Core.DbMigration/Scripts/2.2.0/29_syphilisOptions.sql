------------SyphilisTestType
-- master
If Not Exists(Select 1 From LookupMaster where Name='SyphilisTestType') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('SyphilisTestType','SyphilisTestType',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='VDRL') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('VDRL','VDRL',0); End
If Not Exists(Select 1 From LookupItem where Name='RPR') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('RPR','RPR',0); End
If Not Exists(Select 1 From LookupItem where Name='Duo Test') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Duo Test','Duo Test',0); End

If Not Exists(Select 1 From LookupItem where Name='Treated For Syphilis') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Treated For Syphilis','Treated For Syphilis',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SyphilisTestType') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='VDRL')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SyphilisTestType'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='VDRL'),'VDRL',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SyphilisTestType') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='RPR')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SyphilisTestType'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='RPR'),'RPR',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SyphilisTestType') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Duo Test')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SyphilisTestType'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Duo Test'),'Duo Test',3); end 
