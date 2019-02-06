-- Treatment buddy
IF not Exists(select * from LookupMaster where Name = 'SupportSystem')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SupportSystem','SupportSystem','0')
GO

IF not exists(select * from LookupItem where Name = 'Treatment Buddy')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Treatment Buddy','Treatment Buddy','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Treatment Buddy')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Treatment Buddy' AND lm.Name = 'SupportSystem')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SupportSystem' and lit.Name='Treatment Buddy'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Support Group')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Support Group','Support Group','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Support Group')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Support Group' AND lm.Name = 'SupportSystem')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SupportSystem' and lit.Name='Support Group'
		END
	END
GO


--Encounter  Type
IF not Exists(select * from LookupMaster where Name = 'EncounterType')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('EncounterType','EncounterType','0')
GO

IF not exists(select * from LookupItem where Name = 'Adherence-Barriers')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Adherence-Barriers','Adherence Barriers','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Adherence-Barriers')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Adherence-Barriers' AND lm.Name = 'EncounterType')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EncounterType' and lit.Name='Adherence-Barriers'
		END
	END
GO


-- AWARENESS OF HIV STATUS
IF not Exists(select * from LookupMaster where Name = 'AwarenessofHIVStatus')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('AwarenessofHIVStatus','AwarenessofHIVStatus','0')
GO

IF not exists(select * from LookupItem where Name = 'AHSQuestion1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('AHSQuestion1','Has the patient/caregiver accepted HIV status?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'AHSQuestion1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='AHSQuestion1' AND lm.Name = 'AwarenessofHIVStatus')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AwarenessofHIVStatus' and lit.Name='AHSQuestion1'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'AHSQuestion2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('AHSQuestion2','For children/adolescents: is age - appropriate disclosure underway/complete?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'AHSQuestion2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='AHSQuestion2' AND lm.Name = 'AwarenessofHIVStatus')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AwarenessofHIVStatus' and lit.Name='AHSQuestion2'
		END
	END
GO

-- UNDERSTANDING OF HIV INFECTION AND ART
IF not Exists(select * from LookupMaster where Name = 'UnderstandingOfHIVInfection')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('UnderstandingOfHIVInfection','UnderstandingOfHIVInfection','0')
GO

IF not exists(select * from LookupItem where Name = 'UHIQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('UHIQ1','Understands how HIV affects the body and risk of transmission to sexual partners and children during pregnancy and breastfeeding','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'UHIQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='UHIQ1' AND lm.Name = 'UnderstandingOfHIVInfection')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='UnderstandingOfHIVInfection' and lit.Name='UHIQ1'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'UHIQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('UHIQ2','Understands ART and how it works','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'UHIQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='UHIQ2' AND lm.Name = 'UnderstandingOfHIVInfection')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='UnderstandingOfHIVInfection' and lit.Name='UHIQ2'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'UHIQ3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('UHIQ3','Understands side effects and what to do in case of side effects','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'UHIQ3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='UHIQ3' AND lm.Name = 'UnderstandingOfHIVInfection')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='UnderstandingOfHIVInfection' and lit.Name='UHIQ3'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'UHIQ4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('UHIQ4','Understands benefits of adherence','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'UHIQ4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='UHIQ4' AND lm.Name = 'UnderstandingOfHIVInfection')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='UnderstandingOfHIVInfection' and lit.Name='UHIQ4'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'UHIQ5')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('UHIQ5','Understands consequences of non-adherence including drug resistance and treatment failures','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'UHIQ5')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='UHIQ5' AND lm.Name = 'UnderstandingOfHIVInfection')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='UnderstandingOfHIVInfection' and lit.Name='UHIQ5'
		END
	END
GO




--- DAILY ROUTINE
IF not Exists(select * from LookupMaster where Name = 'DailyRoutine')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('DailyRoutine','DailyRoutine','0')
GO

IF not exists(select * from LookupItem where Name = 'DR1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DR1','Tell me about your typical day','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DR1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DR1' AND lm.Name = 'DailyRoutine')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DailyRoutine' and lit.Name='DR1'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'DR2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DR2','Please tell me how you take each of your medicines (Review how the patient takes the medicines or how the caregiver administers it)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DR2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DR2' AND lm.Name = 'DailyRoutine')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DailyRoutine' and lit.Name='DR2'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'DR3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DR3','What do you do in case of visits or travels?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DR3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DR3' AND lm.Name = 'DailyRoutine')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DailyRoutine' and lit.Name='DR3'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'DR4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DR4','For orphans/children: Who is the primary caregiver? Assess their level of commitment','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DR4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DR4' AND lm.Name = 'DailyRoutine')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DailyRoutine' and lit.Name='DR4'
		END
	END
GO


---PSYCHOSOCIAL CIRCUMSTANCES
IF not Exists(select * from LookupMaster where Name = 'PsychosocialCircumstances')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('PsychosocialCircumstances','PsychosocialCircumstances','0')
GO

IF not exists(select * from LookupItem where Name = 'PCQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PCQ1','Who do you live with?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PCQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PCQ1' AND lm.Name = 'PsychosocialCircumstances')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PsychosocialCircumstances' and lit.Name='PCQ1'
		END
	END
GO

-- Question 1 items
IF not Exists(select * from LookupMaster where Name = 'PCQ1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('PCQ1','PCQ1','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'PCQ1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PCQ1' and lit.Name='Notes'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'PCQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PCQ2','Who is aware of your HIV status? Are there people in your life with whom you''ve discussed your HIV status on ART use?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PCQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PCQ2' AND lm.Name = 'PsychosocialCircumstances')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PsychosocialCircumstances' and lit.Name='PCQ2'
		END
	END
GO
--Question 2 items
IF not Exists(select * from LookupMaster where Name = 'PCQ2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('PCQ2','PCQ2','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'PCQ2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PCQ2' and lit.Name='Notes'
		END
	END
GO



IF not exists(select * from LookupItem where Name = 'PCQ3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PCQ3','What is your support system?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PCQ3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PCQ3' AND lm.Name = 'PsychosocialCircumstances')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PsychosocialCircumstances' and lit.Name='PCQ3'
		END
	END
GO


---Question 3 Items
IF not Exists(select * from LookupMaster where Name = 'PCQ3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('PCQ3','PCQ3','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'PCQ3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PCQ3' and lit.Name='Notes'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'SupportSystem')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SupportSystem','SupportSystem','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SupportSystem')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SupportSystem' AND lm.Name = 'PCQ3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PCQ3' and lit.Name='SupportSystem'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'PCQ4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PCQ4','Are there changes in relationships with family members / friends?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PCQ4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PCQ4' AND lm.Name = 'PsychosocialCircumstances')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PsychosocialCircumstances' and lit.Name='PCQ4'
		END
	END
GO

--- Question 4 Items
IF not Exists(select * from LookupMaster where Name = 'PCQ4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('PCQ4','PCQ4','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'PCQ4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PCQ4' and lit.Name='Notes'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'PCQ4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PCQ4' and lit.Name='GeneralYesNo'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'PCQ5')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PCQ5','Does it bother you people might find out your HIV status?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PCQ5')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PCQ5' AND lm.Name = 'PsychosocialCircumstances')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PsychosocialCircumstances' and lit.Name='PCQ5'
		END
	END
GO

--- Question 5 Items
IF not Exists(select * from LookupMaster where Name = 'PCQ5')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('PCQ5','PCQ5','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'PCQ5')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PCQ5' and lit.Name='Notes'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'PCQ5')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PCQ5' and lit.Name='GeneralYesNo'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'PCQ6')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PCQ6','Do you feel that people treat you deifferently when they know your HIV status?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PCQ6')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PCQ6' AND lm.Name = 'PsychosocialCircumstances')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PsychosocialCircumstances' and lit.Name='PCQ6'
		END
	END
GO

--- Question 6 Items
IF not Exists(select * from LookupMaster where Name = 'PCQ6')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('PCQ6','PCQ6','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'PCQ6')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PCQ6' and lit.Name='Notes'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'PCQ6')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PCQ6' and lit.Name='GeneralYesNo'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'PCQ7')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PCQ7','Is stigma interfering with taking medication on time or with keeping clinical appointments?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PCQ7')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PCQ7' AND lm.Name = 'PsychosocialCircumstances')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PsychosocialCircumstances' and lit.Name='PCQ7'
		END
	END
GO

--- Question 7 Items
IF not Exists(select * from LookupMaster where Name = 'PCQ7')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('PCQ7','PCQ7','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'PCQ7')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PCQ7' and lit.Name='Notes'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'PCQ7')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PCQ7' and lit.Name='GeneralYesNo'
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'PCQ8')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PCQ8','Have you ever stopped using medication because of religious beliefs?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PCQ8')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PCQ8' AND lm.Name = 'PsychosocialCircumstances')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PsychosocialCircumstances' and lit.Name='PCQ8'
		END
	END
GO

--- Question 8 Items
IF not Exists(select * from LookupMaster where Name = 'PCQ8')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('PCQ8','PCQ8','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'PCQ8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PCQ8' and lit.Name='Notes'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'PCQ8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='PCQ8' and lit.Name='GeneralYesNo'
		END
	END
GO


--- REFERRALS AND NETWORKS
IF not Exists(select * from LookupMaster where Name = 'ReferralsNetworks')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('ReferralsNetworks','ReferralsNetworks','0')
GO


IF not exists(select * from LookupItem where Name = 'RNQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('RNQ1','Has the patient been referred to other other services? [Nutrition, psychosocial support services, other medical clinics, substance us treatment, etc]','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'RNQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='RNQ1' AND lm.Name = 'ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrRNQank from LookupMaster lm,LookupItem lit where lm.Name='ReferralsNetworks' and lit.Name='RNQ1'
		END
	END
GO


---Question 1 Items
IF not Exists(select * from LookupMaster where Name = 'RNQ1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('RNQ1','RNQ1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'RNQ1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrRNQank from LookupMaster lm,LookupItem lit where lm.Name='RNQ1' and lit.Name='GeneralYesNo'
		END
	END
GO



IF not exists(select * from LookupItem where Name = 'RNQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('RNQ2','Did he/she attend the appointments?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'RNQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='RNQ2' AND lm.Name = 'ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrRNQank from LookupMaster lm,LookupItem lit where lm.Name='ReferralsNetworks' and lit.Name='RNQ2'
		END
	END
GO


---Question 2 Items
IF not Exists(select * from LookupMaster where Name = 'RNQ2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('RNQ2','RNQ2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'RNQ2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrRNQank from LookupMaster lm,LookupItem lit where lm.Name='RNQ2' and lit.Name='GeneralYesNo'
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'RNQ3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('RNQ3','What was the experience? Do the referrals need to be re-organized?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'RNQ3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='RNQ3' AND lm.Name = 'ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrRNQank from LookupMaster lm,LookupItem lit where lm.Name='ReferralsNetworks' and lit.Name='RNQ3'
		END
	END
GO


---Question 3 Items
IF not Exists(select * from LookupMaster where Name = 'RNQ3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('RNQ3','RNQ3','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'RNQ3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrRNQank from LookupMaster lm,LookupItem lit where lm.Name='RNQ3' and lit.Name='Notes'
		END
	END
GO