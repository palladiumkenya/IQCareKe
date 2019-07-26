-- Patient-Tracing
IF not Exists(select * from LookupMaster where Name = 'EncounterType')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('EncounterType','EncounterType','0')
GO

IF not exists(select * from LookupItem where Name = 'Patient-Tracing')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Patient-Tracing','Patient-Tracing','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Patient-Tracing')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Patient-Tracing' AND lm.Name = 'EncounterType')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EncounterType' and lit.Name='Patient-Tracing'
		END
	END
GO