--Dental
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Bad breath')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Bad breath','Bad breath')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Tender or bleeding gums')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Tender or bleeding gums','Tender or bleeding gums')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Painful chewing')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Painful chewing','Painful chewing')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Loose teeth')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Loose teeth','Loose teeth')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Sensitive teeth')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Sensitive teeth','Sensitive teeth')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Receding gums or longer appearing teeth')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Receding gums or longer appearing teeth','Receding gums or longer appearing teeth')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Longer appearing teeth')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Longer appearing teeth','Longer appearing teeth')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Tooth Cavities/ Decay')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Tooth Cavities/ Decay','Tooth Cavities/ Decay')  END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Dental' AND v.ItemName='Bad breath')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Dental'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Bad breath' ),'Bad breath',1)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Dental' AND v.ItemName='Tender or bleeding gums')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Dental'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Tender or bleeding gums' ),'Tender or bleeding gums',2)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Dental' AND v.ItemName='Painful chewing')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Dental'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Painful chewing' ),'Painful chewing',3)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Dental' AND v.ItemName='Loose teeth')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Dental'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Loose teeth' ),'Loose teeth',4)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Dental' AND v.ItemName='Sensitive teeth')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Dental'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Sensitive teeth' ),'Sensitive teeth',5)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Dental' AND v.ItemName='Receding gums or longer appearing teeth')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Dental'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Receding gums or longer appearing teeth' ),'Receding gums or longer appearing teeth',6)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Dental' AND v.ItemName='Longer appearing teeth')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Dental'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Longer appearing teeth' ),'Longer appearing teeth',7)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Dental' AND v.ItemName='Tooth Cavities/ Decay')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Dental'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Tooth Cavities/ Decay' ),'Tooth Cavities/ Decay',8)
END