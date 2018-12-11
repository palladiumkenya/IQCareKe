
IF not exists(select * from LookupItem where Name = 'CareEnded')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('CareEnded','Care Ended','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'CareEnded')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='CareEnded' AND lm.Name = 'EncounterType')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'19.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EncounterType' and lit.Name='CareEnded'
		END
	END
GO

