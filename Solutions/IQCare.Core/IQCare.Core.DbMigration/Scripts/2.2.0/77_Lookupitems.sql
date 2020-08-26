If Not Exists(Select *  From LookupMaster where Name = 'CaregiverRelationship') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('CaregiverRelationship','CaregiverRelationship',0); End



If Not Exists(Select 1 From LookupItem where Name='Parent') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Parent','Parent',0); End
If Not Exists(Select 1 From LookupItem where Name='Aunt') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Aunt','Aunt',0); End
If Not Exists(Select 1 From LookupItem where Name='Uncle') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Uncle','Uncle',0); End
If Not Exists(Select 1 From LookupItem where Name='Sibling') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Sibling','Sibling',0); End
If Not Exists(Select  1 From LookupItem where Name like 'Childrenshome') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Childrenshome','Childrens Home' ,0); End



If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='CaregiverRelationship') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Parent')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='CaregiverRelationship'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Parent'),'Parent',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='CaregiverRelationship') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Aunt')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='CaregiverRelationship'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Aunt'),'Aunt',2); end 

If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='CaregiverRelationship') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Uncle')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='CaregiverRelationship'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Uncle'),'Uncle',3); end 
 
 If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='CaregiverRelationship') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Sibling')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='CaregiverRelationship'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Sibling'),'Sibling',4); end 

  If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='CaregiverRelationship') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Childrenshome')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='CaregiverRelationship'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Childrenshome'),'Childrens home',5); end 
 
 

 