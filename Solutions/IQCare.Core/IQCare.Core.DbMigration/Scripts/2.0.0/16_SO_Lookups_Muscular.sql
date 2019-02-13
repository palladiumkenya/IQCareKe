-- Muscular
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Weakness')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Weakness','Weakness')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Abnormal Fatigue')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Abnormal Fatigue','Abnormal Fatigue')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Muscle Spasms')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Muscle Spasms','Muscle Spasms')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Cramps')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Cramps','Cramps')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Twitching')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Twitching','Twitching')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Difficulty Swallowing')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Difficulty Swallowing','Difficulty Swallowing')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Difficulty Breathing')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Difficulty Breathing','Difficulty Breathing')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Slurred Speech')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Slurred Speech','Slurred Speech')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Dropping eye lids')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Dropping eye lids','Dropping eye lids')  END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Muscular' AND v.ItemName='Weakness')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Muscular'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Weakness' ),'Weakness',1)
END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Muscular' AND v.ItemName='Abnormal Fatigue')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Muscular'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Abnormal Fatigue' ),'Abnormal Fatigue',2)
END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Muscular' AND v.ItemName='Muscle Spasms')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Muscular'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Muscle Spasms' ),'Muscle Spasms',3)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Muscular' AND v.ItemName='Cramps')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Muscular'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Cramps' ),'Cramps',4)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Muscular' AND v.ItemName='Twitching')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Muscular'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Twitching' ),'Twitching',5)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Muscular' AND v.ItemName='Difficulty Swallowing')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Muscular'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Difficulty Swallowing' ),'Difficulty Swallowing',6)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Muscular' AND v.ItemName='Difficulty Breathing')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Muscular'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Difficulty Breathing' ),'Difficulty Breathing',7)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Muscular' AND v.ItemName='Slurred Speech')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Muscular'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Slurred Speech' ),'Slurred Speech',8)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Muscular' AND v.ItemName='Dropping eye lids')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Muscular'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Dropping eye lids' ),'Dropping eye lids',9)
END
