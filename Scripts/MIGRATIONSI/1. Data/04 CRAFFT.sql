--- CRAFFT Screening Questions
IF not Exists(select * from LookupMaster where Name = 'CRAFFTScreeningQuestions')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('CRAFFTScreeningQuestions','CRAFFTScreeningQuestions','0')
GO

IF not exists(select * from LookupItem where Name = 'CRAFFTScreeningQuestion1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('CRAFFTScreeningQuestion1','Did you rink any alcohol (more than a few sips)?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'CRAFFTScreeningQuestion1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='CRAFFTScreeningQuestion1' AND lm.Name = 'CRAFFTScreeningQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CRAFFTScreeningQuestions' and lit.Name='CRAFFTScreeningQuestion1'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'CRAFFTScreeningQuestion2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('CRAFFTScreeningQuestion2','Did you smoke any marijuana?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'CRAFFTScreeningQuestion2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='CRAFFTScreeningQuestion2' AND lm.Name = 'CRAFFTScreeningQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CRAFFTScreeningQuestions' and lit.Name='CRAFFTScreeningQuestion2'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'CRAFFTScreeningQuestion3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('CRAFFTScreeningQuestion3','Did you use anything else to get high?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'CRAFFTScreeningQuestion3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='CRAFFTScreeningQuestion3' AND lm.Name = 'CRAFFTScreeningQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CRAFFTScreeningQuestions' and lit.Name='CRAFFTScreeningQuestion3'
		END
	END
GO

--- CRAFFT SCore Questions
IF not Exists(select * from LookupMaster where Name = 'CRAFFTScoreQuestions')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('CRAFFTScoreQuestions','CRAFFTScoreQuestions','0')
GO

IF not exists(select * from LookupItem where Name = 'CRAFFTScoreQuestion1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('CRAFFTScoreQuestion1','Have you ever ridden in a CAR driven by someone (including yourself) who was "high" or had been using alcohol or drugs?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'CRAFFTScoreQuestion1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='CRAFFTScoreQuestion1' AND lm.Name = 'CRAFFTScoreQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CRAFFTScoreQuestions' and lit.Name='CRAFFTScoreQuestion1'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'CRAFFTScoreQuestion2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('CRAFFTScoreQuestion2','Do you ever use alcohol or drugs to RELAX, feel better about yourself, or fit in?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'CRAFFTScoreQuestion2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='CRAFFTScoreQuestion2' AND lm.Name = 'CRAFFTScoreQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CRAFFTScoreQuestions' and lit.Name='CRAFFTScoreQuestion2'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'CRAFFTScoreQuestion3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('CRAFFTScoreQuestion3','Do you ever use alcohol or drugs while you are by yourself, or ALONE?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'CRAFFTScoreQuestion3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='CRAFFTScoreQuestion3' AND lm.Name = 'CRAFFTScoreQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CRAFFTScoreQuestions' and lit.Name='CRAFFTScoreQuestion3'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'CRAFFTScoreQuestion4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('CRAFFTScoreQuestion4','Do you ever FORGET things you did while using alcohol or drugs?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'CRAFFTScoreQuestion4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='CRAFFTScoreQuestion4' AND lm.Name = 'CRAFFTScoreQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CRAFFTScoreQuestions' and lit.Name='CRAFFTScoreQuestion4'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'CRAFFTScoreQuestion5')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('CRAFFTScoreQuestion5','Do your FAMILY or FRIENDS ever tell you that you should cut down on your drinking or drug use?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'CRAFFTScoreQuestion5')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='CRAFFTScoreQuestion5' AND lm.Name = 'CRAFFTScoreQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CRAFFTScoreQuestions' and lit.Name='CRAFFTScoreQuestion5'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'CRAFFTScoreQuestion6')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('CRAFFTScoreQuestion6','Have you ever gotten into TROUBLE while you were using alcohol or drugs?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'CRAFFTScoreQuestion6')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='CRAFFTScoreQuestion6' AND lm.Name = 'CRAFFTScoreQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CRAFFTScoreQuestions' and lit.Name='CRAFFTScoreQuestion6'
		END
	END
GO