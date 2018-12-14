-- Immmune
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Frequent and recurrent pneumonia')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Frequent and recurrent pneumonia','Frequent and recurrent pneumonia')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Bronchitis')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Bronchitis','Bronchitis')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Sinus infection')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Sinus infection','Sinus infection')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Ear Infection')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Ear Infection','Ear Infection')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Skin infection')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Skin infection','Skin infection')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Meningitis')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Meningitis','Meningitis')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Anaemia')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Anaemia','Anaemia')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Inflammation and infection of internal organs')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Inflammation and infection of internal organs','Inflammation and infection of internal organs')  END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Low platelets count')BEGIN INSERT INTO LookupItem(Name,DisplayName) VALUES('Low platelets count','Low platelets count')  END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Immune' AND v.ItemName='Frequent and recurrent pneumonia')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Immune'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Frequent and recurrent pneumonia' ),'Frequent and recurrent pneumonia',1)
END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Immune' AND v.ItemName='Bronchitis')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Immune'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Bronchitis' ),'Bronchitis',2)
END

IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Immune' AND v.ItemName='Sinus infection')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Immune'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Sinus infection' ),'Sinus infection',3)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Immune' AND v.ItemName='Ear Infection')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Immune'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Ear Infection' ),'Ear Infection',4)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Immune' AND v.ItemName='Skin infection')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Immune'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Skin infection' ),'Skin infection',5)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Immune' AND v.ItemName='Meningitis')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Immune'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Meningitis' ),'Meningitis',6)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Immune' AND v.ItemName='Anaemia')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Immune'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Anaemia' ),'Anaemia',7)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Immune' AND v.ItemName='Inflammation and infection of internal organsInflammation and infection of internal organsInflammation and infection of internal organsInflammation and infection of internal organsInflammation and infection of internal organsInflammation and infection of internal organs')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Immune'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Inflammation and infection of internal organs' ),'Inflammation and infection of internal organs',8)
END
IF NOT EXISTS(SELECT * FROM LookupItemView v WHERE v.MasterName='Immune' AND v.ItemName='Low platelets count')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 l.Id FROM LookupMaster l WHERE l.Name='Immune'),(SELECT top 1 i.Id FROM LookupItem i WHERE i.Name='Low platelets count' ),'Low platelets count',9)
END
