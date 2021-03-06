UPDATE LookupItem SET Name = '1. OTZ Orientation', DisplayName = '1. OTZ Orientation' WHERE Name = 'Orientation - Operation Triple Zero';
UPDATE LookupItem SET Name = '2. OTZ Participation', DisplayName = '2. OTZ Participation' WHERE Name = 'OTZ Package';
UPDATE LookupItem SET Name = '3. OTZ Leadership', DisplayName = '3. OTZ Leadership' WHERE Name = 'Leadership';
UPDATE LookupItem SET Name = '4. OTZ Making Decisions for the future', DisplayName = '4. OTZ Making Decisions for the future' WHERE Name = 'Decision Making';
UPDATE LookupItem SET Name = '5. OTZ Transition to Adult Care', DisplayName = '5. OTZ Transition to Adult Care' WHERE Name = 'Transition to Adult care';
UPDATE LookupItem SET Name = '6. OTZ Treatment Literacy', DisplayName = '6. OTZ Treatment Literacy' WHERE Name = 'OTZ Treatment Literacy';

-- master
If Not Exists(Select 1 From LookupMaster where Name='OTZ_Modules') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('OTZ_Modules','OTZ_Modules',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='1. OTZ Orientation') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('1. OTZ Orientation','1. OTZ Orientation',0); End
If Not Exists(Select 1 From LookupItem where Name='2. OTZ Participation') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('2. OTZ Participation','2. OTZ Participation',0); End
If Not Exists(Select 1 From LookupItem where Name='3. OTZ Leadership') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('3. OTZ Leadership','3. OTZ Leadership',0); End
If Not Exists(Select 1 From LookupItem where Name='4. OTZ Making Decisions for the future') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('4. OTZ Making Decisions for the future','4. OTZ Making Decisions for the future',0); End
If Not Exists(Select 1 From LookupItem where Name='6. OTZ Treatment Literacy') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('6. OTZ Treatment Literacy','6. OTZ Treatment Literacy',0); End
If Not Exists(Select 1 From LookupItem where Name='5. OTZ Transition to Adult Care') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('5. OTZ Transition to Adult Care','5. OTZ Transition to Adult Care',0); End
If Not Exists(Select 1 From LookupItem where Name='7. OTZ SRH') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('7. OTZ SRH','7. OTZ SRH',0); End
If Not Exists(Select 1 From LookupItem where Name='8. OTZ Beyond the 3rd 90') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('8. OTZ Beyond the 3rd 90','8. OTZ Beyond the 3rd 90',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='1. OTZ Orientation')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='1. OTZ Orientation'),'1. OTZ Orientation',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='2. OTZ Participation')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='2. OTZ Participation'),'2. OTZ Participation',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='3. OTZ Leadership')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='3. OTZ Leadership'),'3. OTZ Leadership',3); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='4. OTZ Making Decisions for the future')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='4. OTZ Making Decisions for the future'),'4. OTZ Making Decisions for the future',4); end 

If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='6. OTZ Treatment Literacy'))
 Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES(
 (SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='6. OTZ Treatment Literacy'),'6. OTZ Treatment Literacy',5); end

If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='5. OTZ Transition to Adult Care')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='5. OTZ Transition to Adult Care'),'5. OTZ Transition to Adult Care',6); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='7. OTZ SRH')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='7. OTZ SRH'),'7. OTZ SRH',7); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='8. OTZ Beyond the 3rd 90')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OTZ_Modules'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='8. OTZ Beyond the 3rd 90'),'8. OTZ Beyond the 3rd 90',8); end 