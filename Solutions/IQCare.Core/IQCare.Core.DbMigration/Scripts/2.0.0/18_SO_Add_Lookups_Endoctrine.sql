-- Endoctrine
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Skin Changes')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Skin Changes','Skin Changes')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Fatigue')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Fatigue','Fatigue')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Stomach upset')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Stomach upset','Stomach upset')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Dehydration')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Dehydration','Dehydration')  END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Endocrine' AND v.ItemName='Skin Changes')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Endocrine'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Skin Changes' ),'Skin Changes',1)
END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Endocrine' AND v.ItemName='Fatigue')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Endocrine'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Fatigue' ),'Fatigue',1)
END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Endocrine' AND v.ItemName='Stomach upset')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Endocrine'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Stomach upset' ),'Stomach upset',1)
END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Endocrine' AND v.ItemName='Dehydration')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Endocrine'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Dehydration' ),'Dehydration',1)
END