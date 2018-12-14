-- Add to Master
IF NOT EXISTS(SELECT * FROM LookupMaster WHERE Name='Digestive')BEGIN INSERT INTO LookupMaster(Name,DisplayName) VALUES('Digestive','Digestive')  END
IF NOT EXISTS(SELECT * FROM LookupMaster WHERE Name='Muscular')BEGIN INSERT INTO LookupMaster(Name,DisplayName) VALUES('Muscular','Muscular')  END
IF NOT EXISTS(SELECT * FROM LookupMaster WHERE Name='Skeletal')BEGIN INSERT INTO LookupMaster(Name,DisplayName) VALUES('Skeletal','Skeletal')  END
IF NOT EXISTS(SELECT * FROM LookupMaster WHERE Name='Endocrine')BEGIN INSERT INTO LookupMaster(Name,DisplayName) VALUES('Endocrine','Endocrine')  END
IF NOT EXISTS(SELECT * FROM LookupMaster WHERE Name='Immune')BEGIN INSERT INTO LookupMaster(Name,DisplayName) VALUES('Immune','Immune')  END
IF NOT EXISTS(SELECT * FROM LookupMaster WHERE Name='Lymphatic')BEGIN INSERT INTO LookupMaster(Name,DisplayName) VALUES('Lymphatic','Lymphatic')  END
IF NOT EXISTS(SELECT * FROM LookupMaster WHERE Name='Dental')BEGIN INSERT INTO LookupMaster(Name,DisplayName) VALUES('Dental','Dental')  END

-- LookupItem
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Digestive')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Digestive','Digestive')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Muscular')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Muscular','Muscular')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Skeletal')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Skeletal','Skeletal')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Endocrine')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Endocrine','Endocrine')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Immune')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Immune','Immune')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Lymphatic')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Lymphatic','Lymphatic')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Dental')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Dental','Dental')  END


-- LookUpMasterItem
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='ReviewOfSystems' AND v.ItemName='Digestive')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='ReviewOfSystems'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Digestive' ),'Digestive',9)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='ReviewOfSystems' AND v.ItemName='Muscular')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='ReviewOfSystems'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Muscular' ),'Muscular',10)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='ReviewOfSystems' AND v.ItemName='Skeletal')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='ReviewOfSystems'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Skeletal' ),'Skeletal',11)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='ReviewOfSystems' AND v.ItemName='Endocrine')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='ReviewOfSystems'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Endocrine' ),'Endocrine',12)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='ReviewOfSystems' AND v.ItemName='Immune')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='ReviewOfSystems'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Immune' ),'Immune',13)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='ReviewOfSystems' AND v.ItemName='Lymphatic')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='ReviewOfSystems'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Lymphatic' ),'Lymphatic',14)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='ReviewOfSystems' AND v.ItemName='Dental')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='ReviewOfSystems'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Dental' ),'Dental',15)
END