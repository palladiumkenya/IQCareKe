--- Tracing Type
IF not Exists(select * from LookupMaster where Name = 'TracingType')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('TracingType','TracingType','0')
GO

IF not exists(select * from LookupItem where Name = 'DefaulterTracing')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DefaulterTracing','DefaulterTracing','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DefaulterTracing')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DefaulterTracing' AND lm.Name = 'TracingType')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TracingType' and lit.Name='DefaulterTracing'
		END
	END
GO

--- Defaulter Tracing Method
IF not Exists(select * from LookupMaster where Name = 'DefaulterTracingMethod')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('DefaulterTracingMethod','DefaulterTracingMethod','0')
GO

IF not exists(select * from LookupItem where Name = 'Client Called')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Client Called','Client Called','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Client Called')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Client Called' AND lm.Name = 'DefaulterTracingMethod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DefaulterTracingMethod' and lit.Name='Client Called'
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
		lit.Name='Physical Tracing' AND lm.Name = 'DefaulterTracingMethod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DefaulterTracingMethod' and lit.Name='Physical Tracing'
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
		lit.Name='Buddy called and instructed' AND lm.Name = 'DefaulterTracingMethod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DefaulterTracingMethod' and lit.Name='Buddy called and instructed'
		END
	END
GO

--- Defaulter Tracing Outcome
-- Defaulter (Traced back to care)
IF not Exists(select * from LookupMaster where Name = 'DefaulterTracingOutcome')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('DefaulterTracingOutcome','DefaulterTracingOutcome','0')
GO

IF not exists(select * from LookupItem where Name = 'Defaulter (Traced back to care)')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Defaulter (Traced back to care)','Defaulter (Traced back to care)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Defaulter (Traced back to care)')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Defaulter (Traced back to care)' AND lm.Name = 'DefaulterTracingOutcome')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DefaulterTracingOutcome' and lit.Name='Defaulter (Traced back to care)'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'LTFU (Re-Initiated on ART)')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('LTFU (Re-Initiated on ART)','LTFU (Re-Initiated on ART)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'LTFU (Re-Initiated on ART)')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='LTFU (Re-Initiated on ART)' AND lm.Name = 'DefaulterTracingOutcome')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DefaulterTracingOutcome' and lit.Name='LTFU (Re-Initiated on ART)'
		END
	END
GO



IF not exists(select * from LookupItem where Name = 'Dead (Confirmed) ')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Dead (Confirmed) ','Dead (Confirmed) ','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Dead (Confirmed) ')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Dead (Confirmed) ' AND lm.Name = 'DefaulterTracingOutcome')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DefaulterTracingOutcome' and lit.Name='Dead (Confirmed) '
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'Previously undocumented  patient transfer (Confirmed)  ')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Previously undocumented  patient transfer (Confirmed)  ','Previously undocumented  patient transfer (Confirmed)  ','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Previously undocumented  patient transfer (Confirmed)  ')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Previously undocumented  patient transfer (Confirmed)  ' AND lm.Name = 'DefaulterTracingOutcome')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DefaulterTracingOutcome' and lit.Name='Previously undocumented  patient transfer (Confirmed)  '
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'Traced patient (Unable to locate)  ')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Traced patient (Unable to locate)  ','Traced patient (Unable to locate)  ','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Traced patient (Unable to locate)  ')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Traced patient (Unable to locate)  ' AND lm.Name = 'DefaulterTracingOutcome')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DefaulterTracingOutcome' and lit.Name='Traced patient (Unable to locate)  '
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'Traced (Patient declined) ')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Traced (Patient declined) ','Traced (Patient declined) ','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Traced (Patient declined) ')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Traced (Patient declined) ' AND lm.Name = 'DefaulterTracingOutcome')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DefaulterTracingOutcome' and lit.Name='Traced (Patient declined) '
		END
	END
GO



IF not exists(select * from LookupItem where Name = 'Traced (Promised to come back) ')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Traced (Promised to come back) ','Traced (Promised to come back) ','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Traced (Promised to come back) ')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Traced (Promised to come back) ' AND lm.Name = 'DefaulterTracingOutcome')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DefaulterTracingOutcome' and lit.Name='Traced (Promised to come back) '
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'Did not attempt to trace patient ')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Did not attempt to trace patient ','Did not attempt to trace patient ','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Did not attempt to trace patient ')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Did not attempt to trace patient ' AND lm.Name = 'DefaulterTracingOutcome')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DefaulterTracingOutcome' and lit.Name='Did not attempt to trace patient '
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Other tracing outcome')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Other tracing outcome','Other tracing outcome','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Other tracing outcome')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Other tracing outcome' AND lm.Name = 'DefaulterTracingOutcome')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'9.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DefaulterTracingOutcome' and lit.Name='Other tracing outcome'
		END
	END
GO