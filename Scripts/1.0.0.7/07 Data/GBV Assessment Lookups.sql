-- Gender Based Violence Assessment Form Lookup Options

IF NOT EXISTS (SELECT * FROM LookupMaster WHERE [Name] = 'YesNo')
	INSERT INTO LookupMaster (Name,DisplayName,DeleteFlag) VALUES ('YesNo','YesNo',0)
IF NOT EXISTS (SELECT * FROM LookupMaster WHERE [Name] = 'Yes')
	INSERT INTO LookupMaster (Name,DisplayName,DeleteFlag) VALUES ('Yes','Yes',0)
IF NOT EXISTS (SELECT * FROM LookupItem WHERE Name = 'No')
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES ('No','No',0)

DECLARE @YesNo INT 
SELECT @YesNo = id from LookupMaster WHERE Name = 'YesNo'

IF @YesNo IS NOT NULL AND NOT EXISTS (SELECT * FROM LookupMasterItem WHERE LookupMasterId = @YesNo AND LookupItemId = (SELECT top 1 Id FROM LookupItem WHERE [Name] = 'Yes'))
	INSERT INTO LookupMasterItem (LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES (@YesNo,(SELECT top 1 Id FROM LookupItem WHERE [Name] = 'Yes'),(SELECT top 1 DisplayName FROM LookupItem WHERE [Name] = 'Yes'),1)
IF @YesNo IS NOT NULL AND NOT EXISTS (SELECT * FROM LookupMasterItem WHERE LookupMasterId = @YesNo AND LookupItemId = (SELECT top 1 Id FROM LookupItem WHERE [Name] = 'No'))
	INSERT INTO LookupMasterItem (LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES (@YesNo,(SELECT top 1 Id FROM LookupItem WHERE [Name] = 'No'),(SELECT top 1 DisplayName FROM LookupItem WHERE [Name] = 'No'),2)

IF NOT EXISTS (SELECT * FROM LookupMaster WHERE [Name] = 'GBVAssessment')
	INSERT INTO LookupMaster (Name,DisplayName,DeleteFlag) VALUES ('GBVAssessment','Gender Based Violence Assessment',0)
IF NOT EXISTS (SELECT * FROM LookupItem WHERE Name = 'GbvPhysicallyHurt')
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES ('GbvPhysicallyHurt','Within the past one year have you been hit, slapped, kicked or physically hurt, by someone in any way',0)
IF NOT EXISTS (SELECT * FROM LookupItem WHERE [Name] = 'GbvRelationshipPhysicalAssault')
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES ('GbvRelationshipPhysicalAssault','Are you in a relationship with someone who physically assaults you?',0)
IF NOT EXISTS (SELECT * FROM LookupItem WHERE [Name] = 'GbvRelationshipVerbalAssault')
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES ('GbvRelationshipVerbalAssault','Are you in a relationship with a person who threatens, frightens, insults you or treats you badly?',0)
IF NOT EXISTS (SELECT * FROM LookupItem WHERE [Name] = 'GbvRelationshipUncomfSexAct')
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES ('GbvRelationshipUncomfSexAct','Are you in a relationship with a person who forces you to participate in sexual activities that make you feel uncomfortable?',0)
IF NOT EXISTS (SELECT * FROM LookupItem WHERE [Name] = 'GbvNonRelationshipViolence')
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES ('GbvNonRelationshipViolence','Have you ever experienced the above with someone whom you don''t have a relationship with',0)
		
DECLARE @GBVAssessment INT 
SELECT @GBVAssessment = id from LookupMaster WHERE Name = 'GBVAssessment'

IF @GBVAssessment IS NOT NULL AND NOT EXISTS (SELECT * FROM LookupMasterItem WHERE LookupMasterId = @GBVAssessment AND LookupItemId = (SELECT top 1 Id FROM LookupItem WHERE [Name] = 'GbvPhysicallyHurt'))
	INSERT INTO LookupMasterItem (LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES (@GBVAssessment,(SELECT top 1 Id FROM LookupItem WHERE [Name] = 'GbvPhysicallyHurt'),(SELECT top 1 DisplayName FROM LookupItem WHERE [Name] = 'GbvPhysicallyHurt'),1)
IF @GBVAssessment IS NOT NULL AND NOT EXISTS (SELECT * FROM LookupMasterItem WHERE LookupMasterId = @GBVAssessment AND LookupItemId = (SELECT top 1 Id FROM LookupItem WHERE [Name] = 'GbvRelationshipPhysicalAssault'))
	INSERT INTO LookupMasterItem (LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES (@GBVAssessment,(SELECT top 1 Id FROM LookupItem WHERE [Name] = 'GbvRelationshipPhysicalAssault'),(SELECT top 1 DisplayName FROM LookupItem WHERE [Name] = 'GbvRelationshipPhysicalAssault'),2)
IF @GBVAssessment IS NOT NULL AND NOT EXISTS (SELECT * FROM LookupMasterItem WHERE LookupMasterId = @GBVAssessment AND LookupItemId = (SELECT top 1 Id FROM LookupItem WHERE [Name] = 'GbvRelationshipVerbalAssault'))
	INSERT INTO LookupMasterItem (LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES (@GBVAssessment,(SELECT top 1 Id FROM LookupItem WHERE [Name] = 'GbvRelationshipVerbalAssault'),(SELECT top 1 DisplayName FROM LookupItem WHERE [Name] = 'GbvRelationshipVerbalAssault'),3)
IF @GBVAssessment IS NOT NULL AND NOT EXISTS (SELECT * FROM LookupMasterItem WHERE LookupMasterId = @GBVAssessment AND LookupItemId = (SELECT top 1 Id FROM LookupItem WHERE [Name] = 'GbvRelationshipUncomfSexAct'))
	INSERT INTO LookupMasterItem (LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES (@GBVAssessment,(SELECT top 1 Id FROM LookupItem WHERE [Name] = 'GbvRelationshipUncomfSexAct'),(SELECT top 1 DisplayName FROM LookupItem WHERE [Name] = 'GbvRelationshipUncomfSexAct'),4)
IF @GBVAssessment IS NOT NULL AND NOT EXISTS (SELECT * FROM LookupMasterItem WHERE LookupMasterId = @GBVAssessment AND LookupItemId = (SELECT top 1 Id FROM LookupItem WHERE [Name] = 'GbvNonRelationshipViolence'))
	INSERT INTO LookupMasterItem (LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES (@GBVAssessment,(SELECT top 1 Id FROM LookupItem WHERE [Name] = 'GbvNonRelationshipViolence'),(SELECT top 1 DisplayName FROM LookupItem WHERE [Name] = 'GbvNonRelationshipViolence'),5)

IF NOT EXISTS (SELECT * FROM LookupMaster WHERE [Name] = 'ScreeningCategory')
	INSERT INTO LookupMaster (Name,DisplayName,DeleteFlag) VALUES ('ScreeningCategory','Groups together all the Screening Category',0)
IF NOT EXISTS (SELECT * FROM LookupItem WHERE [Name] = 'GBVAssessment')
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES ('GBVAssessment','Gender Based Violence Assessment',0)

DECLARE @ScreeningCategory INT 
SELECT @ScreeningCategory = id from LookupMaster WHERE Name = 'ScreeningCategory'

IF @ScreeningCategory IS NOT NULL AND NOT EXISTS (SELECT * FROM LookupMasterItem WHERE LookupMasterId = @ScreeningCategory AND LookupItemId = (SELECT top 1 Id FROM LookupItem WHERE [Name] = 'GBVAssessment'))
	INSERT INTO LookupMasterItem (LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES (@ScreeningCategory,(SELECT top 1 Id FROM LookupItem WHERE [Name] = 'GBVAssessment'),(SELECT top 1 DisplayName FROM LookupItem WHERE [Name] = 'GBVAssessment'),1)

