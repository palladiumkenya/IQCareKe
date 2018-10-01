-- DRINK ALCOHOL
IF not Exists(select * from LookupMaster where Name = 'DrinkAlcohol')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('DrinkAlcohol','DrinkAlcohol','0')
GO
--Never
IF not exists(select * from LookupItem where Name = 'Never')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Never','Never','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Never')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Never' AND lm.Name = 'DrinkAlcohol')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DrinkAlcohol' and lit.Name='Never'
		END
	END
GO
--Monthly or less
IF not exists(select * from LookupItem where Name = 'Monthly')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Monthly','Monthly or Less','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Monthly')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Monthly' AND lm.Name = 'DrinkAlcohol')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DrinkAlcohol' and lit.Name='Monthly'
		END
	END
GO
--2 - 4 times a month
IF not exists(select * from LookupItem where Name = 'TwoTimesaMonth')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('TwoTimesaMonth','2 - 4 Times a Month','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'TwoTimesaMonth')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='TwoTimesaMonth' AND lm.Name = 'DrinkAlcohol')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DrinkAlcohol' and lit.Name='TwoTimesaMonth'
		END
	END
GO
--2-3 times a week
IF not exists(select * from LookupItem where Name = 'TwoTimesaWeek')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('TwoTimesaWeek','2 - 3 times a week','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'TwoTimesaWeek')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='TwoTimesaWeek' AND lm.Name = 'DrinkAlcohol')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DrinkAlcohol' and lit.Name='TwoTimesaWeek'
		END
	END
GO
--4 or more times a week
IF not exists(select * from LookupItem where Name = 'FourorMore')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('FourorMore','4 or More Times a Week','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'FourorMore')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='FourorMore' AND lm.Name = 'DrinkAlcohol')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DrinkAlcohol' and lit.Name='FourorMore'
		END
	END
GO


-- SMOKE
IF not Exists(select * from LookupMaster where Name = 'Smoke')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Smoke','Smoke','0')
GO
--Never Smoked
IF not exists(select * from LookupItem where Name = 'NeverSmoked')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('NeverSmoked','Never Smoked','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'NeverSmoked')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='NeverSmoked' AND lm.Name = 'Smoke')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Smoke' and lit.Name='NeverSmoked'
		END
	END
GO
-- Former
IF not exists(select * from LookupItem where Name = 'FormerSmoker')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('FormerSmoker','Former Smoker','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'FormerSmoker')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='FormerSmoker' AND lm.Name = 'Smoke')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Smoke' and lit.Name='FormerSmoker'
		END
	END
GO
--current
IF not exists(select * from LookupItem where Name = 'CurrentSmoker')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('CurrentSmoker','Current some day smoker','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'CurrentSmoker')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='CurrentSmoker' AND lm.Name = 'Smoke')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Smoke' and lit.Name='CurrentSmoker'
		END
	END
GO
--Light
IF not exists(select * from LookupItem where Name = 'LightSmoker')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('LightSmoker','Light tobacco smoker (< 10 per day)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'LightSmoker')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='LightSmoker' AND lm.Name = 'Smoke')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Smoke' and lit.Name='LightSmoker'
		END
	END
GO
--Heavy
IF not exists(select * from LookupItem where Name = 'HeavySmoker')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('HeavySmoker','Heavy tobacco smoker (Over 10 per day)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'HeavySmoker')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='HeavySmoker' AND lm.Name = 'Smoke')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Smoke' and lit.Name='HeavySmoker'
		END
	END
GO
--Smoker
IF not exists(select * from LookupItem where Name = 'Smoker')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Smoker','Smoker, current status unknown','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Smoker')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Smoker' AND lm.Name = 'Smoke')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Smoke' and lit.Name='Smoker'
		END
	END
GO


-- USE DRUGS
IF not Exists(select * from LookupMaster where Name = 'UseDrugs')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('UseDrugs','UseDrugs','0')
GO
--Never
IF not exists(select * from LookupItem where Name = 'Never')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Never','Never','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Never')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Never' AND lm.Name = 'UseDrugs')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='UseDrugs' and lit.Name='Never'
		END
	END
GO
--Monthly or less
IF not exists(select * from LookupItem where Name = 'Monthly')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Monthly','Monthly or Less','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Monthly')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Monthly' AND lm.Name = 'UseDrugs')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='UseDrugs' and lit.Name='Monthly'
		END
	END
GO
--2 - 4 times a month
IF not exists(select * from LookupItem where Name = 'TwoTimesaMonth')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('TwoTimesaMonth','2 - 4 Times a Month','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'TwoTimesaMonth')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='TwoTimesaMonth' AND lm.Name = 'UseDrugs')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='UseDrugs' and lit.Name='TwoTimesaMonth'
		END
	END
GO
--2-3 times a week
IF not exists(select * from LookupItem where Name = 'TwoTimesaWeek')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('TwoTimesaWeek','2 - 3 times a week','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'TwoTimesaWeek')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='TwoTimesaWeek' AND lm.Name = 'UseDrugs')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='UseDrugs' and lit.Name='TwoTimesaWeek'
		END
	END
GO
--4 or more times a week
IF not exists(select * from LookupItem where Name = 'FourorMore')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('FourorMore','4 or More Times a Week','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'FourorMore')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='FourorMore' AND lm.Name = 'UseDrugs')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='UseDrugs' and lit.Name='FourorMore'
		END
	END
GO

--- QUESTIONS
--- QUESTIONS
IF not Exists(select * from LookupMaster where Name = 'SocialHistoryQuestions')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SocialHistoryQuestions','SocialHistoryQuestions','0')
GO

IF not exists(select * from LookupItem where Name = 'DrinkAlcohol')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DrinkAlcohol','1. How often do you have a drink containing alcohol?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DrinkAlcohol')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DrinkAlcohol' AND lm.Name = 'SocialHistoryQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocialHistoryQuestions' and lit.Name='DrinkAlcohol'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Smoke')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Smoke','2. How often do you smoke?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Smoke')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Smoke' AND lm.Name = 'SocialHistoryQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocialHistoryQuestions' and lit.Name='Smoke'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'UseDrugs')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('UseDrugs','3. How often do you use drugs?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'UseDrugs')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='UseDrugs' AND lm.Name = 'SocialHistoryQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocialHistoryQuestions' and lit.Name='UseDrugs'
		END
	END
GO

-- Social History Notes and Screening
IF not Exists(select * from LookupMaster where Name = 'SocialHistory')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SocialHistory','SocialHistory','0')
GO

IF not exists(select * from LookupItem where Name = 'SocialRecord')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SocialRecord','Record Social History','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SocialRecord')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SocialRecord' AND lm.Name = 'SocialHistory')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocialHistory' and lit.Name='SocialRecord'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'SocialNotes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SocialNotes','Social History Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SocialNotes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SocialNotes' AND lm.Name = 'SocialHistory')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocialHistory' and lit.Name='SocialNotes'
		END
	END
GO