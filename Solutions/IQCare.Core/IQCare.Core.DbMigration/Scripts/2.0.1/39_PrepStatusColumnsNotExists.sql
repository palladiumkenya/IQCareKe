IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PatientPrEPStatus' AND COLUMN_NAME = 'CondomsIssued')
 BEGIN
 BEGIN ALTER TABLE  PatientPrEPStatus ADD  [CondomsIssued] [int] NULL END; 
 END

 IF NOT EXISTS( SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PatientPrEPStatus' AND COLUMN_NAME = 'NoOfCondoms')
 BEGIN
 BEGIN ALTER TABLE PatientPrEPStatus ADD  	[NoOfCondoms] [int] NULL END; 
 END


 If Not Exists(Select 1 From LookupMaster where Name='EncounterType') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('EncounterType','EncounterType',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='prep-encounter') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('prep-encounter','prep-encounter',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='EncounterType') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='prep-encounter')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='EncounterType'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='prep-encounter'),'prep-encounter',1); end

 

