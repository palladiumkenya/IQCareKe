IF not exists(select * from LookupItem where Name = 'CaseSummary')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('CaseSummary','Case Summary','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'CaseSummary')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='CareSummary' AND lm.Name = 'EncounterType')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'20.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EncounterType' and lit.Name='CaseSummary'
		END
	END
GO

