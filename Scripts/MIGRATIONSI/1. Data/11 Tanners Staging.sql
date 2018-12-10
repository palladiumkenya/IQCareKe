-- Tanners Staging Notes and Screening
IF not Exists(select * from LookupMaster where Name = 'TannersStaging')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('TannersStaging','TannersStaging','0')
GO

IF not exists(select * from LookupItem where Name = 'TannersRecord')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('TannersRecord','Record Tanners Staging','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'TannersRecord')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='TannersRecord' AND lm.Name = 'TannersStaging')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TannersStaging' and lit.Name='TannersRecord'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'TannersNotes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('TannersNotes','Tanners Staging Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'TannersNotes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='TannersNotes' AND lm.Name = 'TannersStaging')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TannersStaging' and lit.Name='TannersNotes'
		END
	END
GO