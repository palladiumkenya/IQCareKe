IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='PaedsSecondlineRegimen' AND ItemName='CS1B')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='PaedsSecondlineRegimen'),(SELECT top 1 id FROM LookupItem WHERE Name='CS1B'),(SELECT top 1 DisplayName FROM LookupItem WHERE Name='CS1B'),2);

END;

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='PaedsSecondlineRegimen' AND ItemName='CS2A')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='PaedsSecondlineRegimen'),(SELECT top 1 id FROM LookupItem WHERE Name='CS2A'),(SELECT top 1 DisplayName FROM LookupItem WHERE Name='CS2A'),2);

END;

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='PaedsSecondlineRegimen' AND ItemName='CS2C')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='PaedsSecondlineRegimen'),(SELECT top 1 id FROM LookupItem WHERE Name='CS2C'),(SELECT top 1 DisplayName FROM LookupItem WHERE Name='CS2C'),2);
END;

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='PaedsSecondlineRegimen' AND ItemName='CS4X')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='PaedsSecondlineRegimen'),(SELECT top 1 id FROM LookupItem WHERE Name='CS4X'),(SELECT top 1 DisplayName FROM LookupItem WHERE Name='CS4X'),2);
END;


