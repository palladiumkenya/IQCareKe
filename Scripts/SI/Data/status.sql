-- SESSIONS
IF not Exists(select * from LookupMaster where Name = 'CounsellingStatus')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('CounsellingStatus','CounsellingStatus','0')
GO

IF not exists(select * from LookupItem where Name = 'Ongoing')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Ongoing','Ongoing','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Ongoing')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Ongoing' AND lm.Name = 'CounsellingStatus')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CounsellingStatus' and lit.Name='Ongoing'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Complete')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Complete','Complete','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Complete')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Complete' AND lm.Name = 'CounsellingStatus')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CounsellingStatus' and lit.Name='Complete'
		END
	END
GO