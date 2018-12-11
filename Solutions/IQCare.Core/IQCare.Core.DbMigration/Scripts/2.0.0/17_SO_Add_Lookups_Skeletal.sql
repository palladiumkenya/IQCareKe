-- Skeletal
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Pain in your bones and joints')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Pain in your bones and joints','Pain in your bones and joints')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Tingling and weakness')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Tingling and weakness','Tingling and weakness')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Bone deformities')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Bone deformities','Bone deformities')  END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Skeletal' AND v.ItemName='Pain in your bones and joints')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Skeletal'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Pain in your bones and joints' ),'Pain in your bones and joints',1)
END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Skeletal' AND v.ItemName='Tingling and weakness')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Skeletal'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Tingling and weakness' ),'Tingling and weakness',2)
END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Skeletal' AND v.ItemName='Bone deformities')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Skeletal'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Bone deformities' ),'Bone deformities',3)
END