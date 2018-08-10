-- PMTCT LOOKUPS

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='anc-encounter')
BEGIN
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES('anc-encounter','anc-encounter',0);
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE ItemName='anc-encounter' AND MasterName='EncounterType')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='EncounterType'),(SELECT top 1 Id FROM LookupItem WHERE Name='anc-encounter'),'anc-encounter',9)
END