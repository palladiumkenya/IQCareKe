-- GBV
IF not Exists(select * from LookupMaster where Name = 'GBVQuestions')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('GBVQuestions','GBVQuestions','0')
GO

IF not exists(select * from LookupItem where Name = 'GBVQuestion1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GBVQuestion1','Within the past year, have you been hit, slapped, kicked or physically hurt by someone in any way?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GBVQuestion1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GBVQuestion1' AND lm.Name = 'GBVQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='GBVQuestions' and lit.Name='GBVQuestion1'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'GBVQuestion2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GBVQuestion2','Are you in a relationship with a person who physically hit you?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GBVQuestion2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GBVQuestion2' AND lm.Name = 'GBVQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='GBVQuestions' and lit.Name='GBVQuestion2'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'GBVQuestion3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GBVQuestion3','Are you in a relationship with a person who threatens, frightens or insults you or treats you badly?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GBVQuestion3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GBVQuestion3' AND lm.Name = 'GBVQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='GBVQuestions' and lit.Name='GBVQuestion3'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'GBVQuestion4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GBVQuestion4','Are you in relationship with a person who forces you to participate in sexual activities that make you feel uncormfortable?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GBVQuestion4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GBVQuestion4' AND lm.Name = 'GBVQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='GBVQuestions' and lit.Name='GBVQuestion4'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'GBVQuestion5')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GBVQuestion5','Have you ever experienced any of the above with someone you do not have a relationship with?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GBVQuestion5')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GBVQuestion5' AND lm.Name = 'GBVQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='GBVQuestions' and lit.Name='GBVQuestion5'
		END
	END
GO