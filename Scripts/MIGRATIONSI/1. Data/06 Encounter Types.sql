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
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'9.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EncounterType' and lit.Name='Adherence-Barriers'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Screening')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Screening','Screening','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Screening')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Screening' AND lm.Name = 'EncounterType')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'10.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EncounterType' and lit.Name='Screening'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'EnhanceAdherence')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('EnhanceAdherence','Enhance Adherence Counselling','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'EnhanceAdherence')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='EnhanceAdherence' AND lm.Name = 'EncounterType')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'11.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EncounterType' and lit.Name='EnhanceAdherence'
		END
	END
GO





IF not exists(select * from LookupItem where Name = 'DepressionScreening')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DepressionScreening','Depression Screening','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DepressionScreening')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DepressionScreening' AND lm.Name = 'EncounterType')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'12.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EncounterType' and lit.Name='DepressionScreening'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'AlcoholandDrugAbuseScreening')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('AlcoholandDrugAbuseScreening','Alcohol and Drug Abuse Screening','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'AlcoholandDrugAbuseScreening')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='AlcoholandDrugAbuseScreening' AND lm.Name = 'EncounterType')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'13.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EncounterType' and lit.Name='AlcoholandDrugAbuseScreening'
		END
	END
GO




IF not exists(select * from LookupItem where Name = 'Disclosure')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Disclosure','Gender Based Violence Screening','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GBVScreening')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GBVScreening' AND lm.Name = 'EncounterType')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'13.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EncounterType' and lit.Name='GBVScreening'
		END
	END
GO









