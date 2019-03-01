
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='maternity-encounter')
BEGIN
  INSERT INTO LookupItem(Name,DisplayName,DeleteFlag) VALUES('maternity-encounter','maternity-encounter',0)
END
IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='EncounterType' AND ItemName='maternity-encounter')
BEGIN
 INSERT INTO LookupMasterItem (LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES(
 (SELECT TOP 1 id FROM LookupMaster WHERE [name]='EncounterType'),
 (SELECT top 1 id FROM LookupItem WHERE [name]='maternity-encounter'),
 'maternity-encounter',
 14)
END