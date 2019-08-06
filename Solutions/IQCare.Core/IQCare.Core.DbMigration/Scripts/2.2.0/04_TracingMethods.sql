


-- Client Called
IF not Exists(select * from LookupMaster where Name = 'TracingMethod')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('TracingMethod','TracingMethod','0')
GO

IF not exists(select * from LookupItem where Name = 'Client Called')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Client Called','Client Called','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Client Called')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Client Called' AND lm.Name = 'TracingMethod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TracingMethod' and lit.Name='Client Called'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Physical Tracing')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Physical Tracing','Physical Tracing','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Physical Tracing')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Physical Tracing' AND lm.Name = 'TracingMethod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TracingMethod' and lit.Name='Physical Tracing'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Buddy called and instructed')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Buddy called and instructed','Buddy called and instructed','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Buddy called and instructed')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Buddy called and instructed' AND lm.Name = 'TracingMethod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TracingMethod' and lit.Name='Buddy called and instructed'
		END
	END
GO
