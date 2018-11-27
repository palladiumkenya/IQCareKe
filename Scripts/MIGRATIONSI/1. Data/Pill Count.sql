--Pill Adherence
IF not Exists(select * from LookupMaster where Name = 'PillAdherence')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('PillAdherence','PillAdherence','0')
GO

--Q3
IF not exists(select * from LookupItem where Name = 'PillAdherenceQ3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PillAdherenceQ3','a. Doses issued in the last visit + doses from the last visit','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PillAdherenceQ3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PillAdherenceQ3' AND lm.Name = 'PillAdherence')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PillAdherence' and lit.Name='PillAdherenceQ3'
		END
	END
GO


--Q4
IF not exists(select * from LookupItem where Name = 'PillAdherenceQ4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PillAdherenceQ4','b. Number of doses expected to have been taken','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PillAdherenceQ4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PillAdherenceQ4' AND lm.Name = 'PillAdherence')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PillAdherence' and lit.Name='PillAdherenceQ4'
		END
	END
GO

--Q5
IF not exists(select * from LookupItem where Name = 'PillAdherenceQ5')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PillAdherenceQ5','c. Doses missed from b.','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PillAdherenceQ5')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PillAdherenceQ5' AND lm.Name = 'PillAdherence')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PillAdherence' and lit.Name='PillAdherenceQ5'
		END
	END
GO