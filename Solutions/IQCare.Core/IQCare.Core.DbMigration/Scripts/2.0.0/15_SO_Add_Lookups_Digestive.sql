
-- Digestive
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Bleeding')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Bleeding','Bleeding')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Bleeding')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Bloating','Bloating')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Constipation')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Constipation','Constipation')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Diarrhea')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Diarrhea','Diarrhea')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Heartburn')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Heartburn','Heartburn')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Incontinence')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Incontinence','Incontinence')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Nausea and vomiting')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Nausea and vomiting','Nausea and vomiting')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Pain in the belly')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Pain in the belly','Pain in the belly')  END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Digestive' AND v.ItemName='Bleeding')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Digestive'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Bleeding' ),'Bleeding',1)
END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Digestive' AND v.ItemName='Bleeding')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Digestive'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Bloating' ),'Bloating',2)
END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Digestive' AND v.ItemName='Constipation')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Digestive'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Constipation' ),'Constipation',3)
END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Digestive' AND v.ItemName='Diarrhea')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Digestive'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Diarrhea' ),'Diarrhea',4)
END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Digestive' AND v.ItemName='Heartburn')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Digestive'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Heartburn' ),'Heartburn',5)
END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Digestive' AND v.ItemName='Incontinence')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Digestive'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Incontinence' ),'Incontinence',6)
END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Digestive' AND v.ItemName='Nausea and vomiting')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Digestive'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Nausea and vomiting' ),'Nausea and vomiting',7)
END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Digestive' AND v.ItemName='Pain in the belly')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Digestive'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Pain in the belly' ),'Pain in the belly',8)
END
