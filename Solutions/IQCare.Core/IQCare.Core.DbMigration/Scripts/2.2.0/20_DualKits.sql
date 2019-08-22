------------SyphilisResults
-- master
If Not Exists(Select 1 From LookupMaster where Name='SyphilisResults') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('SyphilisResults','SyphilisResults',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Positive') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Positive','Positive',0); End
If Not Exists(Select 1 From LookupItem where Name='Negative') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Negative','Negative',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SyphilisResults') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Positive')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SyphilisResults'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Positive'),'Positive',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SyphilisResults') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Negative')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SyphilisResults'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Negative'),'Negative',2); end 


------------ScreeningHIVTestKits
-- master
If Not Exists(Select 1 From LookupMaster where Name='ScreeningHIVTestKits') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('ScreeningHIVTestKits','ScreeningHIVTestKits',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='HIV/Syphilis Duo') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('HIV/Syphilis Duo','HIV/Syphilis Duo',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ScreeningHIVTestKits') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='First Response')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ScreeningHIVTestKits'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='First Response'),'First Response',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ScreeningHIVTestKits') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Determine')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ScreeningHIVTestKits'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Determine'),'Determine',2);end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ScreeningHIVTestKits') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='HIV/Syphilis Duo')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ScreeningHIVTestKits'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='HIV/Syphilis Duo'),'HIV/Syphilis Duo',3);end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ScreeningHIVTestKits') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ScreeningHIVTestKits'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other'),'Other',4);end
