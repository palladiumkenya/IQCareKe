

--  UPDATE LookupItems
	UPDATE LookupItem SET Name='Obese' WHERE Name='O' AND DisplayName='Overweight/Obese'
	UPDATE LookupItem SET Name='Normal' WHERE Name='N' AND DisplayName='Normal'

-- new master item
	IF NOT EXISTS(SELECT * FROM LookupMaster WHERE Name='AdverseEventOutcome')
	BEGIN
		INSERT INTO LookupMaster(Name,DisplayName) VALUES('AdverseEventOutcome','AEO');
	END


-- Died item already exists
	IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Got Well')
	BEGIN
		INSERT INTO LookupItem(name,DisplayName) VALUES('Got Well','Got Well');
	END

	IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Got Well')
	BEGIN
		INSERT INTO LookupItem(name,DisplayName) VALUES('No Change','No Change');
	END

	-- lookupmaster item
	IF NOT EXISTS(SELECT * FROM LookupMasterItem WHERE LookupMasterId IN(SELECT top 1 Id FROM LookupMaster WHERE NAME='AdverseEventOutcome') AND LookupItemId IN(SELECT top 1 Id FROM LookupItem WHERE Name='Died'))
	BEGIN
		INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank)VALUES((SELECT top 1 Id FROM LookupMaster WHERE NAME='AdverseEventOutcome'),(SELECT top 1 Id FROM LookupItem WHERE Name='Died'),'Died',1)
	END	
	
	IF NOT EXISTS(SELECT * FROM LookupMasterItem WHERE LookupMasterId IN(SELECT top 1 Id FROM LookupMaster WHERE NAME='AdverseEventOutcome') AND LookupItemId IN(SELECT top 1 Id FROM LookupItem WHERE Name='GotWell'))
	BEGIN
		INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank)VALUES((SELECT top 1 Id FROM LookupMaster WHERE NAME='AdverseEventOutcome'),(SELECT top 1 Id FROM LookupItem WHERE Name='GotWell'),'Got Well',2)
	END

	IF NOT EXISTS(SELECT * FROM LookupMasterItem WHERE LookupMasterId IN(SELECT top 1 Id FROM LookupMaster WHERE NAME='AdverseEventOutcome') AND LookupItemId IN(SELECT top 1 Id FROM LookupItem WHERE Name='NoChange'))
	BEGIN
		INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank)VALUES((SELECT top 1 Id FROM LookupMaster WHERE NAME='AdverseEventOutcome'),(SELECT top 1 Id FROM LookupItem WHERE Name='NoChange'),'No Change',3)
	END


	-- lookupmaster

IF NOT EXISTS(SELECT * FROM LookupMaster where Name='AdverseEvents')
BEGIN
	INSERT INTO LookupMaster(Name,DisplayName)VALUES('AdverseEvents','Adverse Events')
END

--lookupItem

IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Abnormal dreams or nightmares (Frightening or unpleasant dreams)')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Abnormal dreams or nightmares (Frightening or unpleasant dreams)','Abnormal dreams or nightmares (Frightening or unpleasant dreams)')
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Anxiety')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Anxiety','Anxiety')
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Confusion/abnormal thinking')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Confusion/abnormal thinking','Confusion/abnormal thinking')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Depression/mood changes (frequently feeling very low)')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Depression/mood changes (frequently feeling very low)','Depression/mood changes (frequently feeling very low)')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Dizziness/spinning sensation/vertigo')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Dizziness/spinning sensation/vertigo','Dizziness/spinning sensation/vertigo')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Fatigue/tiredness/weakness')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Fatigue/tiredness/weakness','Fatigue/tiredness/weakness')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Insomnia (lacking sleep at night)/sleep problems')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Insomnia (lacking sleep at night)/sleep problems','Insomnia (lacking sleep at night)/sleep problems')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Poor concentration/ memory problems')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Poor concentration/ memory problems','Poor concentration/ memory problems')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Burning and tingling in limbs/ Paresthesia/painful neuropathy')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Burning and tingling in limbs/ Paresthesia/painful neuropathy','Burning and tingling in limbs/ Paresthesia/painful neuropathy')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Suicide ideation (thoughts on ending the life)')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Suicide ideation (thoughts on ending the life)','Suicide ideation (thoughts on ending the life)')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Skin rash/hypersensitivity reaction')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Skin rash/hypersensitivity reaction','Skin rash/hypersensitivity reaction')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Abdominal discomfort/abdominal pain')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Abdominal discomfort/abdominal pain','Abdominal discomfort/abdominal pain')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Nausea/vomiting')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Nausea/vomiting','Nausea/vomiting')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Diarrhoea')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('','')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Jaundice')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Jaundice','Jaundice')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Fat changes/lipodystrophy/lipohypertrophy')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Fatchanges/lipodystrophy/lipohypertrophy','Fat changes/lipodystrophy/lipohypertrophy')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Gynaecomastia')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Gynaecomastia','Gynaecomastia')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Headache')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Headache','Headache')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Anaemia/pancytopenia')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Anaemia/pancytopenia','Anaemia/pancytopenia')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Renal failure/renal insufficiency')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Renal failure/renal insufficiency','Renal failure/renal insufficiency')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE name='Other Specify')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName) VALUES('Other Specify','Other Specify')
END


IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Abnormal dreams or nightmares (Frightening or unpleasant dreams)'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT top 1 id FROM LookupItem WHERE name='Abnormal dreams or nightmares (Frightening or unpleasant dreams)'),'Abnormal dreams or nightmares (Frightening or unpleasant dreams)',1);
END


IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Anxiety'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Anxiety'),'Anxiety',2);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Confusion/abnormal thinking'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Confusion/abnormal thinking'),'Depression/mood changes (frequently feeling very low)',3);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Depression/mood changes (frequently feeling very low)'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Depression/mood changes (frequently feeling very low)'),'Depression/mood changes (frequently feeling very low)',4);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Dizziness/spinning sensation/vertigo'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Dizziness/spinning sensation/vertigo'),'Dizziness/spinning sensation/vertigo',5);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Fatigue/tiredness/weakness'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Fatigue/tiredness/weakness'),'Fatigue/tiredness/weakness',6);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Insomnia (lacking sleep at night)/sleep problems'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Insomnia (lacking sleep at night)/sleep problems'),'Insomnia (lacking sleep at night)/sleep problems',7);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Poor concentration/ memory problems'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Poor concentration/ memory problems'),'Poor concentration/ memory problems',8);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Burning and tingling in limbs/ Paresthesia/painful neuropathy'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Burning and tingling in limbs/ Paresthesia/painful neuropathy'),'Burning and tingling in limbs/ Paresthesia/painful neuropathy',9);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Suicide ideation (thoughts on ending the life)'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Suicide ideation (thoughts on ending the life)'),'Suicide ideation (thoughts on ending the life)',10);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Skin rash/hypersensitivity reaction'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Skin rash/hypersensitivity reaction'),'Skin rash/hypersensitivity reaction',11);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Abdominal discomfort/abdominal pain'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Abdominal discomfort/abdominal pain'),'Abdominal discomfort/abdominal pain',12);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Nausea/vomiting'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Nausea/vomiting'),'Nausea/vomiting',13);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Diarrhoea'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Diarrhoea'),'Diarrhoea',14);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Jaundice'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Jaundice'),'Jaundice',15);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Fat changes/lipodystrophy/lipohypertrophy'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Fat changes/lipodystrophy/lipohypertrophy'),'Fat changes/lipodystrophy/lipohypertrophy',16);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Gynaecomastia'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Gynaecomastia'),'Gynaecomastia',17);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Headache'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Headache'),'Headache',18);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Anaemia/pancytopenia'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Anaemia/pancytopenia'),'Anaemia/pancytopenia',19);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Renal failure/renal insufficiency'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Renal failure/renal insufficiency'),'Renal failure/renal insufficiency',20);
END

IF NOT EXISTS(SELECT * FROM LookupMasterItem where LookupMasterId IN(SELECT id FROM LookupMaster WHERE name='AdverseEvents') AND LookupItemId IN(SELECT id FROM LookupItem WHERE name='Other Specify'))
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT id FROM LookupMaster WHERE name='AdverseEvents'),(SELECT id FROM LookupItem WHERE name='Other Specify'),'Other Specify',21);
END