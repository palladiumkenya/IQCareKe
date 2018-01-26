-- LookupMaster
INSERT INTO LookupMaster(Name,DisplayName,DeleteFlag) VALUES('EncounterType','EncounterType',0);

-- LookupItem
INSERT INTO LookupItem(Name,DisplayName,DeleteFlag) VALUES('Registration','Registration',0);
INSERT INTO LookupItem(Name,DisplayName,DeleteFlag) VALUES('Triage','Triage',0);
INSERT INTO LookupItem(Name,DisplayName,DeleteFlag) VALUES('ccc-encounter','ccc-encounter',0);
INSERT INTO LookupItem(Name,DisplayName,DeleteFlag) VALUES('pmtct-encounter','pmtct-encounter',0);
INSERT INTO LookupItem(Name,DisplayName,DeleteFlag) VALUES('tb-encounter','tb-encounter',0);
INSERT INTO LookupItem(Name,DisplayName,DeleteFlag) VALUES('Lab','Lab',0);
INSERT INTO LookupItem(Name,DisplayName,DeleteFlag) VALUES('Pharmacy','Pharmacy',0);

-- LookupMasterItem
INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 id FROM LookupMaster l WHERE l.Name='EncounterType'),(SELECT TOP 1 Id FROM LookupItem i WHERE i.Name='registration-encounter'),'registration-encounter',1);
INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 id FROM LookupMaster l WHERE l.Name='EncounterType'),(SELECT TOP 1 Id FROM LookupItem i WHERE i.Name='triage-encounter'),'triage-encounter',2);
INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 id FROM LookupMaster l WHERE l.Name='EncounterType'),(SELECT TOP 1 Id FROM LookupItem i WHERE i.Name='ccc-encounter'),'ccc-encounter',3);
INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 id FROM LookupMaster l WHERE l.Name='EncounterType'),(SELECT TOP 1 Id FROM LookupItem i WHERE i.Name='pmtct-encounter'),'pmtct-encounter',4);
INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 id FROM LookupMaster l WHERE l.Name='EncounterType'),(SELECT TOP 1 Id FROM LookupItem i WHERE i.Name='tb-encounter'),'tb-encounter',5);
INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 id FROM LookupMaster l WHERE l.Name='EncounterType'),(SELECT TOP 1 Id FROM LookupItem i WHERE i.Name='lab-encounter'),'lab-encounter',6);
INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 id FROM LookupMaster l WHERE l.Name='EncounterType'),(SELECT TOP 1 Id FROM LookupItem i WHERE i.Name='Pharmacy-encounter'),'Pharmacy-encounter',7);