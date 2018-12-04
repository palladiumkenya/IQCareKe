IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='hei-encounter')
BEGIN
  INSERT INTO LookupItem(Name,DisplayName,DeleteFlag) VALUES('hei-encounter','hei-encounter',0)
END
IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='EncounterType' AND ItemName='hei-encounter')
BEGIN
 INSERT INTO LookupMasterItem (LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES(
 (SELECT TOP 1 id FROM LookupMaster WHERE [name]='EncounterType'),
 (SELECT top 1 id FROM LookupItem WHERE [name]='hei-encounter'),
 'hei-encounter',
 14)
END