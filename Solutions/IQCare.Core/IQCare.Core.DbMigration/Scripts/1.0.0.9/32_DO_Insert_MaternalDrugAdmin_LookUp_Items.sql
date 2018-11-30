BEGIN TRY

BEGIN TRANSACTION
--Insert look up Master record
IF NOT EXISTS(SELECT * FROM LookupMaster WHERE Name='MaternalDrugAdministration')
BEGIN
INSERT INTO LookupMaster(Name,DisplayName,DeleteFlag)VALUES('MaternalDrugAdministration','Maternal Drug Administration',0)
END

-- Insert Look up Items record
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name IN('Vitamin A Supplementation'))
BEGIN 
INSERT INTO LookupItem(Name,DeleteFlag,DisplayName)VALUES('Vitamin A Supplementation',0,'Vitamin A Supplementation')
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name IN('ARVs Started in Maternity'))
BEGIN 
INSERT INTO LookupItem(Name,DeleteFlag,DisplayName)VALUES('ARVs Started in Maternity',0,'ARVs Started in Maternity')
END


IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name IN('Cotrimoxazole'))
BEGIN 
INSERT INTO LookupItem(Name,DeleteFlag,DisplayName)VALUES('Cotrimoxazole',0,'Cotrimoxazole')
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name IN('Started HAART in ANC'))
BEGIN 
INSERT INTO LookupItem(Name,DeleteFlag,DisplayName)VALUES('Started HAART in ANC',0,'Started HAART in ANC')
END


IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name IN('Infant Provided With ARV prophylaxis'))
BEGIN 
INSERT INTO LookupItem(Name,DeleteFlag,DisplayName)VALUES('Infant Provided With ARV prophylaxis',0,'Infant Provided With ARV prophylaxis')
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name IN('ARVs Started in Maternity'))
BEGIN 
INSERT INTO LookupItem(Name,DeleteFlag,DisplayName)VALUES('ARVs Started in Maternity',0,'ARVs Started in Maternity')
END


IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name IN('Haemanistics Given'))
BEGIN 
INSERT INTO LookupItem(Name,DeleteFlag,DisplayName)VALUES('Haemanistics Given',0,'Haemanistics Given')
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name IN('Started HAART in PNC Visit'))
BEGIN 
INSERT INTO LookupItem(Name,DeleteFlag,DisplayName)VALUES('Started HAART in PNC Visit',0,'Started HAART in PNC Visit')
END

--Insert LookUpMasterItem
DECLARE @LookUpMasterId INT 
SELECT TOP 1 @LookUpMasterId = Id FROM LookupMaster WHERE Name='MaternalDrugAdministration'

INSERT INTO LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES(@LookUpMasterId,(SELECT TOP 1 Id FROM LookupItem WHERE Name='Vitamin A Supplementation'),'Vitamin A Supplementation',1)

INSERT INTO LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES(@LookUpMasterId,(SELECT TOP 1 Id FROM LookupItem WHERE Name='ARVs Started in Maternity'),'ARVs Started in Maternity',2)

INSERT INTO LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES(@LookUpMasterId,(SELECT TOP 1 Id FROM LookupItem WHERE Name='Cotrimoxazole'),'Cotrimoxazole',3)

INSERT INTO LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES(@LookUpMasterId,(SELECT TOP 1 Id FROM LookupItem WHERE Name='Infant Provided With ARV prophylaxis'),'Infant Provided With ARV prophylaxis',4)

INSERT INTO LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES(@LookUpMasterId,(SELECT TOP 1 Id FROM LookupItem WHERE Name='Started HAART in ANC'),'Started HAART in ANC',5)


COMMIT
END TRY

BEGIN CATCH
IF @@TRANCOUNT > 0
ROLLBACK TRAN 
END CATCH