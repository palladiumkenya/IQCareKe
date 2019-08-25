IF NOT EXISTS(SELECT * FROM LookupItem l WHERE l.Name='VIA/VILI')
BEGIN 
  INSERT INTO LookupItem(Name,DisplayName,DeleteFlag) VALUES('VIA/VILI', 'VIA/VILI',0)
END

IF NOT EXISTS(SELECT * FROM LookupItemView l WHERE l.ItemName='VIA/VILI' AND l.MasterName='CacxMethod')
BEGIN 
   INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank)
   VALUES(
		(SELECT TOP 1 l.Id FROM LookupMaster l WHERE l.Name='CacxMethod'),
		(SELECT top 1 l.Id FROM LookupItem l WHERE l.Name='VIA/VILI'),
		'VIA/VILI',
		5
   )
END