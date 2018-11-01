BEGIN TRY

-- Insert Look up Items record
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name IN('Vitamin A Supplementation'))
BEGIN 
INSERT INTO LookupItem(Name,DeleteFlag,DisplayName)VALUES('Vitamin A Supplementation',0,'Vitamin A Supplementation')
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name IN('ARVs Started in Maternity'))
BEGIN 
INSERT INTO LookupItem(Name,DeleteFlag,DisplayName)VALUES('ARVs Started in Maternity',0,'ARVs Started in Maternity')
END


IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name IN('Cotrimoxaz'))
BEGIN 
INSERT INTO LookupItem(Name,DeleteFlag,DisplayName)VALUES('Cotrimoxaz',0,'Cotrimoxaz')
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

COMMIT
END TRY

BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRAN 
END CATCH