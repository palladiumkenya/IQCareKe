-- PMTCT LOOKUPS

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='anc-encounter')
BEGIN
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES('anc-encounter','anc-encounter',0);
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE ItemName='anc-encounter' AND MasterName='EncounterType')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='EncounterType'),(SELECT top 1 Id FROM LookupItem WHERE Name='anc-encounter'),'anc-encounter',9)
END

-- counselled on Topic
IF NOT EXISTS(SELECT * FROM LookupMaster WHERE Name='counselledOn')
BEGIN
	INSERT INTO LookupMaster(Name,DisplayName,DeleteFlag) VALUES('counselledOn','counselledOn',0);
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Birth Plans')
BEGIN
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES('Birth Plans','Birth Plans',0);
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Danger Signs')
BEGIN
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES('Danger Signs','Danger Signs',0);
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='HIV')
BEGIN
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES('HIV','HIV',0);
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Supplemental Feeding')
BEGIN
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES('Supplemental Feeding','Supplemental Feeding',0);
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Breast Care')
BEGIN
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES('Breast Care','Breast Care',0);
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Infant Feeding')
BEGIN
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES('Infant Feeding','Infant Feeding',0);
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Insecticide Treated Nets')
BEGIN
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES('Insecticide Treated Nets','Insecticide Treated Nets',0);
END


IF NOT EXISTS(SELECT * FROM LookupItemView WHERE ItemName='Birth Plans' AND MasterName='Birth Plans')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='counselledOn'),(SELECT top 1 Id FROM LookupItem WHERE Name='Birth Plans'),'Birth Plans',1)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE ItemName='Danger Signs' AND MasterName='Danger Signs')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='counselledOn'),(SELECT top 1 Id FROM LookupItem WHERE Name='Danger Signs'),'Danger Signs',2)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE ItemName='Family Planning' AND MasterName='Family Planning')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='counselledOn'),(SELECT top 1 Id FROM LookupItem WHERE Name='Family Planning'),'Family Planning',3)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE ItemName='HIV' AND MasterName='HIV')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='counselledOn'),(SELECT top 1 Id FROM LookupItem WHERE Name='HIV'),'HIV',4)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE ItemName='Supplemental Feeding' AND MasterName='Supplemental Feeding')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='counselledOn'),(SELECT top 1 Id FROM LookupItem WHERE Name='Supplemental Feeding'),'Supplemental Feeding',5)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE ItemName='Breast Care' AND MasterName='Breast Care')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='counselledOn'),(SELECT top 1 Id FROM LookupItem WHERE Name='anc-encounter'),'anc-encounter',6)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE ItemName='Infant Feeding' AND MasterName='Infant Feeding')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='counselledOn'),(SELECT top 1 Id FROM LookupItem WHERE Name='Infant Feeding'),'Infant Feeding',7)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE ItemName='Insecticide Treated Nets' AND MasterName='Insecticide Treated Nets')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='counselledOn'),(SELECT top 1 Id FROM LookupItem WHERE Name='Insecticide Treated Nets'),'Insecticide Treated Nets',8)
END


IF NOT EXISTS(SELECT * FROM Lookupmaster WHERE Name='ancExamination')
BEGIN
	INSERT INTO Lookupmaster (Name,DisplayName,DeleteFlag) VALUES('ancExamination','ancExamination',0);
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='ancExamination')
BEGIN
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES('ancExamination','ancExamination',0);
END