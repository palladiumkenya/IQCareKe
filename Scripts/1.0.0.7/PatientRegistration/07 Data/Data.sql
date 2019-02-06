--lookupmater
If Not Exists(Select 1 From LookupMaster where Name='ContactCategory') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('ContactCategory','ContactCategory',0); End
If Not Exists(Select 1 From LookupMaster where Name='KinRelationship') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('KinRelationship','KinRelationship',0); End

--lookupitem
If Not Exists(Select 1 From LookupItem where Name='EmergencyContact') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('EmergencyContact','EmergencyContact',0); End
If Not Exists(Select 1 From LookupItem where Name='NextOfKin') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('NextOfKin','NextOfKin',0); End
If Not Exists(Select 1 From LookupItem where Name='TreatmentSupporter') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('TreatmentSupporter','TreatmentSupporter',0); End

If Not Exists(Select 1 From LookupItem where Name='Boyfriend') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Boyfriend','Boyfriend',0); End
If Not Exists(Select 1 From LookupItem where Name='Uncle') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Uncle','Uncle',0); End
If Not Exists(Select 1 From LookupItem where Name='Friend') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Friend','Friend',0); End
If Not Exists(Select 1 From LookupItem where Name='Girlfriend') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Girlfriend','Girlfriend',0); End
If Not Exists(Select 1 From LookupItem where Name='Aunt') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Aunt','Aunt',0); End
If Not Exists(Select 1 From LookupItem where Name='Partner') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Partner','Partner',0); End
If Not Exists(Select 1 From LookupItem where Name='Grand Mother') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Grand Mother','Grand Mother',0); End
If Not Exists(Select 1 From LookupItem where Name='Grand Father') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Grand Father','Grand Father',0); End
If Not Exists(Select 1 From LookupItem where Name='Niece') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Niece','Niece',0); End
If Not Exists(Select 1 From LookupItem where Name='Wife') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Wife','Wife',0); End
If Not Exists(Select 1 From LookupItem where Name='Nephew') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Nephew','Nephew',0); End
If Not Exists(Select 1 From LookupItem where Name='Brother') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Brother','Brother',0); End
If Not Exists(Select 1 From LookupItem where Name='Sister') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Sister','Sister',0); End
If Not Exists(Select 1 From LookupItem where Name='Sister-Inlaw') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Sister-Inlaw','Sister-Inlaw',0); End
If Not Exists(Select 1 From LookupItem where Name='Brother-Inlaw') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Brother-Inlaw','Brother-Inlaw',0); End
If Not Exists(Select 1 From LookupItem where Name='Father-Inlaw') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Father-Inlaw','Father-Inlaw',0); End
If Not Exists(Select 1 From LookupItem where Name='Mother-Inlaw') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Mother-Inlaw','Mother-Inlaw',0); End
If Not Exists(Select 1 From LookupItem where Name='Son') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Son','Son',0); End
If Not Exists(Select 1 From LookupItem where Name='Daughter') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Daughter','Daughter',0); End
If Not Exists(Select 1 From LookupItem where Name='Father ') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Father','Father',0); End
If Not Exists(Select 1 From LookupItem where Name='Mother') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Mother','Mother',0); End
If Not Exists(Select 1 From LookupItem where Name='Grand Father') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Grand Father','Grand Father',0); End

--lookupmasteritem
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'TreatmentSupporter',1 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='TreatmentSupporter') ItemId FROM LookupMaster  WHERE Name='ContactCategory') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'NextOfKin',2 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='NextOfKin') ItemId FROM LookupMaster  WHERE Name='ContactCategory') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'EmergencyContact',3 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='EmergencyContact') ItemId FROM LookupMaster  WHERE Name='ContactCategory') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;


Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Boyfriend',1 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Boyfriend') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Uncle',2 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Uncle') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Friend',3 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Friend') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Girlfriend',4 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Girlfriend') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Aunt',5 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Aunt') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Partner',6 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Partner') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Grand Mother',7 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Grand Mother') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Niece',8 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Niece') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Wife',9 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Wife') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Nephew',10 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Nephew') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Brother',11 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Brother') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Sister',12 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Sister') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Sister-Inlaw',13 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Sister-Inlaw') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Brother-Inlaw',14 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Brother-Inlaw') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Father-Inlaw',15 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Father-Inlaw') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Mother-Inlaw',16 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Mother-Inlaw') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Son',17 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Son') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Daughter',18 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Daughter') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Father',19 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Father') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Mother',20 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Mother') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'Grand Father',21 FROM ( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='Grand Father') ItemId FROM LookupMaster  WHERE Name='KinRelationship') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId)=0;

 IF EXISTS(SELECT * FROM LookupItem WHERE name='Aunty')
 BEGIN
   Update LookupItem SET Name='Aunt', DisplayName='Aunt' WHERE Name='Aunty'
 END
ELSE
  BEGIN
    INSERT INTO LookupItem(Name,DisplayName,DeleteFlag)VALUES('Aunt','Aunt',0)
  END

   IF EXISTS(SELECT * FROM LookupItem WHERE name='Grand Parent')
 BEGIN
   Update LookupItem SET Name='Grand Mother', DisplayName='Grand Mother' WHERE Name='Grand Parent'
 END
ELSE
  BEGIN
    INSERT INTO LookupItem(Name,DisplayName,DeleteFlag)VALUES('Grand Mother','Grand Mother',0)
  END