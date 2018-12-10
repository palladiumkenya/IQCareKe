-- Lymphatic
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Lymphedema')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Lymphedema','Lymphedema')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Fingers Swelling')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Fingers Swelling','Fingers Swelling')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Toes Swelling')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Toes Swelling','Toes Swelling')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Leg Swelling')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Leg Swelling','Leg Swelling')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Arm Swelling')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Arm Swelling','Arm Swelling')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='A feeling of heaviness or tightness')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('A feeling of heaviness or tightness','A feeling of heaviness or tightness')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Restricted range of motion')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Restricted range of motion','Restricted range of motion')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Aching or discomfort')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Aching or discomfort','Aching or discomfort')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Recurring infections')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Recurring infections','Recurring infections')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Skin Fibrosis')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Skin Fibrosis','Skin Fibrosis')  END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Lymphatic' AND v.ItemName='Lymphedema')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Lymphatic'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Lymphedema' ),'Lymphedema',1)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Lymphatic' AND v.ItemName='Fingers Swelling')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Lymphatic'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Fingers Swelling' ),'Fingers Swelling',2)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Lymphatic' AND v.ItemName='Toes Swelling')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Lymphatic'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Toes Swelling' ),'Toes Swelling',3)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Lymphatic' AND v.ItemName='Leg Swelling')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Lymphatic'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Arm Swelling' ),'Arm Swelling',5)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Lymphatic' AND v.ItemName='Arm Swelling')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Lymphatic'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='A feeling of heaviness or tightness' ),'A feeling of heaviness or tightness',6)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Lymphatic' AND v.ItemName='A feeling of heaviness or tightness')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Lymphatic'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Restricted range of motion' ),'Restricted range of motion',7)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Lymphatic' AND v.ItemName='Restricted range of motion')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Lymphatic'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Aching or discomfort' ),'Aching or discomfort',8)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Lymphatic' AND v.ItemName='Aching or discomfort')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Lymphatic'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Recurring infections' ),'Recurring infections',9)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Lymphatic' AND v.ItemName='Recurring infections')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Lymphatic'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Leg Swelling' ),'Leg Swelling',4)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Lymphatic' AND v.ItemName='Skin Fibrosis')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Lymphatic'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Skin Fibrosis' ),'Skin Fibrosis',10)
END

