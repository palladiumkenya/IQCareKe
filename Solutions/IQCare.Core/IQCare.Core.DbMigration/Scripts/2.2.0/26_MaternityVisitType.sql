------------MaternityVisitType
-- master
If Not Exists(Select 1 From LookupMaster where Name='MaternityVisitType') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('MaternityVisitType','MaternityVisitType',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Initial Visit') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Initial Visit','Initial Visit',0); End
-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='MaternityVisitType') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Initial Visit')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='MaternityVisitType'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Initial Visit'),'Initial Visit',1); end

