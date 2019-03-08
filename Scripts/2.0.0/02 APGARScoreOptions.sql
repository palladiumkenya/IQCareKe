BEGIN TRY
BEGIN TRANSACTION
--Insert look up Master record
IF NOT EXISTS(SELECT * FROM LookupMaster WHERE Name='ApgarScore')
BEGIN
INSERT INTO LookupMaster(Name,DisplayName,DeleteFlag)VALUES('ApgarScore','Apgar Score',0)
END

-- Insert Look up Items record
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name IN('Apgar Score 1 min','Apgar Score 5 min','Apgar Score 10 min'))
BEGIN 
INSERT INTO LookupItem(Name,DeleteFlag,DisplayName)VALUES('Apgar Score 1 min',0,'Apgar Score 1 min')
INSERT INTO LookupItem(Name,DeleteFlag,DisplayName)VALUES('Apgar Score 5 min',0,'Apgar Score 5 min')
INSERT INTO LookupItem(Name,DeleteFlag,DisplayName)VALUES('Apgar Score 10 min',0,'Apgar Score 10 min')

END

--Insert LookUpMasterItem
DECLARE @LookUpMasterId INT 
SELECT TOP 1 @LookUpMasterId = Id FROM LookupMaster WHERE Name='ApgarScore'

INSERT INTO LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES(@LookUpMasterId,(SELECT TOP 1 Id FROM LookupItem WHERE Name='Apgar Score 1 min'),'Apgar Score 1 min',1)

INSERT INTO LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES(@LookUpMasterId,(SELECT TOP 1 Id FROM LookupItem WHERE Name='Apgar Score 5 min'),'Apgar Score 5 min',2)

INSERT INTO LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES(@LookUpMasterId,(SELECT TOP 1 Id FROM LookupItem WHERE Name='Apgar Score 10 min'),'Apgar Score 10 min',3)

COMMIT
END TRY

BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN 
END CATCH