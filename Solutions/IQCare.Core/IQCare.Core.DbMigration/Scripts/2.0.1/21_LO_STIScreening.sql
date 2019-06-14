------------ScreenedForSTI
-- master
If Not Exists(Select 1 From LookupMaster where Name='ScreenedForSTI') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('ScreenedForSTI','ScreenedForSTI',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='STIScreeningDone') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('STIScreeningDone','STIScreeningDone',0); End
If Not Exists(Select 1 From LookupItem where Name='STISymptoms') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('STISymptoms','STISymptoms',0); End
If Not Exists(Select 1 From LookupItem where Name='STILabInvestigationDone') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('STILabInvestigationDone','STILabInvestigationDone',0); End
If Not Exists(Select 1 From LookupItem where Name='STITreatmentOffered') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('STITreatmentOffered','STITreatmentOffered',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ScreenedForSTI') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='STIScreeningDone')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ScreenedForSTI'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='STIScreeningDone'),'STIScreeningDone',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ScreenedForSTI') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='STISymptoms')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ScreenedForSTI'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='STISymptoms'),'STISymptoms',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ScreenedForSTI') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='STILabInvestigationDone')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ScreenedForSTI'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='STILabInvestigationDone'),'STILabInvestigationDone',3); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ScreenedForSTI') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='STITreatmentOffered')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ScreenedForSTI'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='STITreatmentOffered'),'STITreatmentOffered',4); end 
