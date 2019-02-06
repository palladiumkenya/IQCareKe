-- General Yes No
IF not Exists(select * from LookupMaster where Name = 'GeneralYesNo')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
GO

IF not exists(select * from LookupItem where Name = 'Yes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Yes','Yes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Yes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Yes' AND lm.Name = 'GeneralYesNo')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='GeneralYesNo' and lit.Name='Yes'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'No')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('No','No','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'No')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='No' AND lm.Name = 'GeneralYesNo')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='GeneralYesNo' and lit.Name='No'
		END
	END
GO

-- Milestone Assessed
IF not Exists(select * from LookupMaster where Name = 'MilestoneAssessed')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('MilestoneAssessed','Milestone Assessed','0')
GO
--<2 months
IF not exists(select * from LookupItem where Name = '<2 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('<2 Months','<2 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '<2 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='<2 Months' AND lm.Name = 'MilestoneAssessed')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MilestoneAssessed' and lit.Name='<2 Months'
		END
	END
GO
-- 2 months
IF not exists(select * from LookupItem where Name = '2 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('2 Months','2 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '2 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='2 Months' AND lm.Name = 'MilestoneAssessed')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MilestoneAssessed' and lit.Name='2 Months'
		END
	END
GO
--3
IF not exists(select * from LookupItem where Name = '3 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('3 Months','3 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '3 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='3 Months' AND lm.Name = 'MilestoneAssessed')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MilestoneAssessed' and lit.Name='3 Months'
		END
	END
GO
--4
IF not exists(select * from LookupItem where Name = '4 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('4 Months','4 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '4 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='4 Months' AND lm.Name = 'MilestoneAssessed')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MilestoneAssessed' and lit.Name='4 Months'
		END
	END
GO
--6
IF not exists(select * from LookupItem where Name = '6 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('6 Months','6 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '6 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='6 Months' AND lm.Name = 'MilestoneAssessed')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MilestoneAssessed' and lit.Name='6 Months'
		END
	END
GO
--9
IF not exists(select * from LookupItem where Name = '9 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('9 Months','9 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '9 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='9 Months' AND lm.Name = 'MilestoneAssessed')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MilestoneAssessed' and lit.Name='9 Months'
		END
	END
GO
--12
IF not exists(select * from LookupItem where Name = '12 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('12 Months','12 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '12 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='12 Months' AND lm.Name = 'MilestoneAssessed')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MilestoneAssessed' and lit.Name='12 Months'
		END
	END
GO
--15
IF not exists(select * from LookupItem where Name = '15 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('15 Months','15 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '15 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='15 Months' AND lm.Name = 'MilestoneAssessed')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MilestoneAssessed' and lit.Name='15 Months'
		END
	END
GO
--18
IF not exists(select * from LookupItem where Name = '18 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('18 Months','18 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '18 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='18 Months' AND lm.Name = 'MilestoneAssessed')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'9.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MilestoneAssessed' and lit.Name='18 Months'
		END
	END
GO
--36
IF not exists(select * from LookupItem where Name = '36 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('36 Months','36 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '36 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='36 Months' AND lm.Name = 'MilestoneAssessed')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'10.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MilestoneAssessed' and lit.Name='36 Months'
		END
	END
GO

--Milestone status
IF not Exists(select * from LookupMaster where Name = 'MilestoneStatus')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('MilestoneStatus','MilestoneStatus','0')
GO

--Normal
IF not exists(select * from LookupItem where Name = 'Normal')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Normal','Normal','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Normal')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Normal' AND lm.Name = 'MilestoneStatus')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MilestoneStatus' and lit.Name='Normal'
		END
	END
GO
--Delayed
IF not exists(select * from LookupItem where Name = 'Delayed')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Delayed','Delayed','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Delayed')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Delayed' AND lm.Name = 'MilestoneStatus')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MilestoneStatus' and lit.Name='Delayed'
		END
	END
GO
--Regressed
IF not exists(select * from LookupItem where Name = 'Regressed')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Regressed','Regressed','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Regressed')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Regressed' AND lm.Name = 'MilestoneStatus')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MilestoneStatus' and lit.Name='Regressed'
		END
	END
GO

--Immunization period
IF not Exists(select * from LookupMaster where Name = 'ImmunizationPeriod')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('ImmunizationPeriod','ImmunizationPeriod','0')
GO

--birth
IF not exists(select * from LookupItem where Name = 'Birth')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Birth','Birth','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Birth')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Birth' AND lm.Name = 'ImmunizationPeriod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationPeriod' and lit.Name='Birth'
		END
	END
GO
--6 weeks
IF not exists(select * from LookupItem where Name = '6 Weeks')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('6 Weeks','6 Weeks','0')
	END
GO

IF  exists(select * from LookupItem where Name = '6 Weeks')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='6 Weeks' AND lm.Name = 'ImmunizationPeriod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationPeriod' and lit.Name='6 Weeks'
		END
	END
GO
--10 weeks
IF not exists(select * from LookupItem where Name = '10 Weeks')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('10 Weeks','10 Weeks','0')
	END
GO

IF  exists(select * from LookupItem where Name = '10 Weeks')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='10 Weeks' AND lm.Name = 'ImmunizationPeriod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationPeriod' and lit.Name='10 Weeks'
		END
	END
GO
--14 weeks
IF not exists(select * from LookupItem where Name = '14 Weeks')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('14 Weeks','14 Weeks','0')
	END
GO

IF  exists(select * from LookupItem where Name = '14 Weeks')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='14 Weeks' AND lm.Name = 'ImmunizationPeriod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationPeriod' and lit.Name='14 Weeks'
		END
	END
GO
--<2 months
IF not exists(select * from LookupItem where Name = '<2 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('<2 Months','<2 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '<2 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='<2 Months' AND lm.Name = 'ImmunizationPeriod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationPeriod' and lit.Name='<2 Months'
		END
	END
GO
--2 months
IF not exists(select * from LookupItem where Name = '2 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('2 Months','2 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '2 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='2 Months' AND lm.Name = 'ImmunizationPeriod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationPeriod' and lit.Name='2 Months'
		END
	END
GO
--3 months
IF not exists(select * from LookupItem where Name = '3 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('3 Months','3 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '3 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='3 Months' AND lm.Name = 'ImmunizationPeriod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationPeriod' and lit.Name='3 Months'
		END
	END
GO
--4 months
IF not exists(select * from LookupItem where Name = '4 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('4 Months','4 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '4 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='4 Months' AND lm.Name = 'ImmunizationPeriod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationPeriod' and lit.Name='4 Months'
		END
	END
GO
--6 months
IF not exists(select * from LookupItem where Name = '6 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('6 Months','6 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '6 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='6 Months' AND lm.Name = 'ImmunizationPeriod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'9.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationPeriod' and lit.Name='6 Months'
		END
	END
GO
--9 months
IF not exists(select * from LookupItem where Name = '9 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('9 Months','9 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '9 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='9 Months' AND lm.Name = 'ImmunizationPeriod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'10.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationPeriod' and lit.Name='9 Months'
		END
	END
GO
--12 months
IF not exists(select * from LookupItem where Name = '12 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('12 Months','12 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '12 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='12 Months' AND lm.Name = 'ImmunizationPeriod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'11.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationPeriod' and lit.Name='12 Months'
		END
	END
GO
--15 months
IF not exists(select * from LookupItem where Name = '15 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('15 Months','15 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '15 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='15 Months' AND lm.Name = 'ImmunizationPeriod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'12.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationPeriod' and lit.Name='15 Months'
		END
	END
GO
--18 months
IF not exists(select * from LookupItem where Name = '18 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('18 Months','18 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '18 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='18 Months' AND lm.Name = 'ImmunizationPeriod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'13.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationPeriod' and lit.Name='18 Months'
		END
	END
GO
--36 months
IF not exists(select * from LookupItem where Name = '36 Months')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('36 Months','36 Months','0')
	END
GO

IF  exists(select * from LookupItem where Name = '36 Months')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='36 Months' AND lm.Name = 'ImmunizationPeriod')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'14.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationPeriod' and lit.Name='36 Months'
		END
	END
GO

--Immunization given
IF not Exists(select * from LookupMaster where Name = 'ImmunizationGiven')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('ImmunizationGiven','ImmunizationGiven','0')
GO
--measles
IF not exists(select * from LookupItem where Name = 'Measles')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Measles','Measles','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Measles')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Measles' AND lm.Name = 'ImmunizationGiven')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationGiven' and lit.Name='Measles'
		END
	END
GO
--Rota
IF not exists(select * from LookupItem where Name = 'Rota')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Rota','Rota','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Rota')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Rota' AND lm.Name = 'ImmunizationGiven')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationGiven' and lit.Name='Rota'
		END
	END
GO
--OPV
IF not exists(select * from LookupItem where Name = 'OPV')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('OPV','OPV','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'OPV')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='OPV' AND lm.Name = 'ImmunizationGiven')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationGiven' and lit.Name='OPV'
		END
	END
GO
--BCG
IF not exists(select * from LookupItem where Name = 'BCG')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('BCG','BCG','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'BCG')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='BCG' AND lm.Name = 'ImmunizationGiven')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationGiven' and lit.Name='BCG'
		END
	END
GO
--DPT
IF not exists(select * from LookupItem where Name = 'DPT')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DPT','DPT','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DPT')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DPT' AND lm.Name = 'ImmunizationGiven')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationGiven' and lit.Name='DPT'
		END
	END
GO
--HepB
IF not exists(select * from LookupItem where Name = 'HepB')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('HepB','HepB','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'HepB')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='HepB' AND lm.Name = 'ImmunizationGiven')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationGiven' and lit.Name='HepB'
		END
	END
GO
--Influenza B
IF not exists(select * from LookupItem where Name = 'Influenza B')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Influenza B','Influenza B','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Influenza B')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Influenza B' AND lm.Name = 'ImmunizationGiven')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationGiven' and lit.Name='Influenza B'
		END
	END
GO
--Pneumococcal
IF not exists(select * from LookupItem where Name = 'Pneumococcal')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Pneumococcal','Pneumococcal','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Pneumococcal')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Pneumococcal' AND lm.Name = 'ImmunizationGiven')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationGiven' and lit.Name='Pneumococcal'
		END
	END
GO
--Yellow Fever
IF not exists(select * from LookupItem where Name = 'Yellow Fever')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Yellow Fever','Yellow Fever','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Yellow Fever')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Yellow Fever' AND lm.Name = 'ImmunizationGiven')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'9.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ImmunizationGiven' and lit.Name='Yellow Fever'
		END
	END
GO

-- Drink alcohol
IF not Exists(select * from LookupMaster where Name = 'DrinkAlcohol')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('DrinkAlcohol','DrinkAlcohol','0')
GO
--Never
IF not exists(select * from LookupItem where Name = 'Never')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Never','Never','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Never')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Never' AND lm.Name = 'DrinkAlcohol')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DrinkAlcohol' and lit.Name='Never'
		END
	END
GO
--Monthly or less
IF not exists(select * from LookupItem where Name = 'Monthly')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Monthly','Monthly or Less','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Monthly')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Never' AND lm.Name = 'DrinkAlcohol')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DrinkAlcohol' and lit.Name='Monthly'
		END
	END
GO
--2 - 4 times a month
IF not exists(select * from LookupItem where Name = 'TwoTimesaMonth')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('TwoTimesaMonth','2 - 4 Times a Month','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'TwoTimesaMonth')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='TwoTimesaMonth' AND lm.Name = 'DrinkAlcohol')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DrinkAlcohol' and lit.Name='FourTimes'
		END
	END
GO
--2-3 times a week
IF not exists(select * from LookupItem where Name = 'TwoTimesaWeek')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('TwoTimesaWeek','2 - 3 times a week','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'TwoTimesaWeek')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='TwoTimesaWeek' AND lm.Name = 'DrinkAlcohol')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DrinkAlcohol' and lit.Name='TwoTimesaWeek'
		END
	END
GO
--4 or more times a week
IF not exists(select * from LookupItem where Name = 'FourorMore')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('FourorMore','4 or More Times a Week','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'FourorMore')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='FourorMore' AND lm.Name = 'DrinkAlcohol')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DrinkAlcohol' and lit.Name='FourorMore'
		END
	END
GO
-- Smoke
IF not Exists(select * from LookupMaster where Name = 'Smoke')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Smoke','Smoke','0')
GO
--Never Smoked
IF not exists(select * from LookupItem where Name = 'NeverSmoked')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('NeverSmoked','Never Smoked','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'NeverSmoked')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='NeverSmoked' AND lm.Name = 'Smoke')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Smoke' and lit.Name='Never Smoked'
		END
	END
GO
-- Former
IF not exists(select * from LookupItem where Name = 'FormerSmoker')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('FormerSmoker','Former Smoker','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'FormerSmoker')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='FormerSmoker' AND lm.Name = 'Smoke')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Smoke' and lit.Name='FormerSmoker'
		END
	END
GO
--current
IF not exists(select * from LookupItem where Name = 'CurrentSmoker')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('CurrentSmoker','Current some day smoker','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'CurrentSmoker')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='CurrentSmoker' AND lm.Name = 'Smoke')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Smoke' and lit.Name='CurrentSmoker'
		END
	END
GO
--Light
IF not exists(select * from LookupItem where Name = 'LightSmoker')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('LightSmoker','Light tobacco smoker (< 10 per day)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'LightSmoker')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='LightSmoker' AND lm.Name = 'Smoke')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Smoke' and lit.Name='LightSmoker'
		END
	END
GO
--Heavy
IF not exists(select * from LookupItem where Name = 'HeavySmoker')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('HeavySmoker','Heavy tobacco smoker (Over 10 per day)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'HeavySmoker')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='HeavySmoker' AND lm.Name = 'Smoke')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Smoke' and lit.Name='HeavySmoker'
		END
	END
GO
--Smoker
IF not exists(select * from LookupItem where Name = 'Smoker')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Smoker','Smoker, current status unknown','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Smoker')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Smoker' AND lm.Name = 'Smoke')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Smoke' and lit.Name='Smoker'
		END
	END
GO
-- Use drugs
IF not Exists(select * from LookupMaster where Name = 'UseDrugs')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('UseDrugs','UseDrugs','0')
GO
--Never
IF not exists(select * from LookupItem where Name = 'Never')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Never','Never','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Never')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Never' AND lm.Name = 'UseDrugs')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='UseDrugs' and lit.Name='Never'
		END
	END
GO
--Monthly or less
IF not exists(select * from LookupItem where Name = 'Monthly')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Monthly','Monthly or Less','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Monthly')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Never' AND lm.Name = 'UseDrugs')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='UseDrugs' and lit.Name='Monthly'
		END
	END
GO
--2 - 4 times a month
IF not exists(select * from LookupItem where Name = 'TwoTimesaMonth')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('TwoTimesaMonth','2 - 4 Times a Month','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'TwoTimesaMonth')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='TwoTimesaMonth' AND lm.Name = 'UseDrugs')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='UseDrugs' and lit.Name='FourTimes'
		END
	END
GO
--2-3 times a week
IF not exists(select * from LookupItem where Name = 'TwoTimesaWeek')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('TwoTimesaWeek','2 - 3 times a week','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'TwoTimesaWeek')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='TwoTimesaWeek' AND lm.Name = 'UseDrugs')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='UseDrugs' and lit.Name='TwoTimesaWeek'
		END
	END
GO
--4 or more times a week
IF not exists(select * from LookupItem where Name = 'FourorMore')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('FourorMore','4 or More Times a Week','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'FourorMore')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='FourorMore' AND lm.Name = 'UseDrugs')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='UseDrugs' and lit.Name='FourorMore'
		END
	END
GO

--Breasts Genitals
IF not Exists(select * from LookupMaster where Name = 'BreastsGenitals')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('BreastsGenitals','BreastsGenitals','0')
GO
--1
IF not exists(select * from LookupItem where Name = '1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('1','1','0')
	END
GO

IF  exists(select * from LookupItem where Name = '1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='1' AND lm.Name = 'BreastsGenitals')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BreastsGenitals' and lit.Name='1'
		END
	END
GO
--2
IF not exists(select * from LookupItem where Name = '2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('2','2','0')
	END
GO

IF  exists(select * from LookupItem where Name = '2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='2' AND lm.Name = 'BreastsGenitals')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BreastsGenitals' and lit.Name='2'
		END
	END
GO
--3
IF not exists(select * from LookupItem where Name = '3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('3','3','0')
	END
GO

IF  exists(select * from LookupItem where Name = '3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='3' AND lm.Name = 'BreastsGenitals')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BreastsGenitals' and lit.Name='3'
		END
	END
GO
--4
IF not exists(select * from LookupItem where Name = '4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('4','4','0')
	END
GO

IF  exists(select * from LookupItem where Name = '4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='4' AND lm.Name = 'BreastsGenitals')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BreastsGenitals' and lit.Name='4'
		END
	END
GO

-- Pubic Hair
IF not Exists(select * from LookupMaster where Name = 'PubicHair')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('PubicHair','PubicHair','0')
GO
--1
IF not exists(select * from LookupItem where Name = '1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('1','1','0')
	END
GO

IF  exists(select * from LookupItem where Name = '1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='1' AND lm.Name = 'PubicHair')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PubicHair' and lit.Name='1'
		END
	END
GO
--2
IF not exists(select * from LookupItem where Name = '2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('2','2','0')
	END
GO

IF  exists(select * from LookupItem where Name = '2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='2' AND lm.Name = 'PubicHair')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PubicHair' and lit.Name='2'
		END
	END
GO
--3
IF not exists(select * from LookupItem where Name = '3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('3','3','0')
	END
GO

IF  exists(select * from LookupItem where Name = '3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='3' AND lm.Name = 'PubicHair')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PubicHair' and lit.Name='3'
		END
	END
GO
--4
IF not exists(select * from LookupItem where Name = '4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('4','4','0')
	END
GO

IF  exists(select * from LookupItem where Name = '4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='4' AND lm.Name = 'PubicHair')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PubicHair' and lit.Name='4'
		END
	END
GO

-- Neonatal History Notes and Screening
IF not Exists(select * from LookupMaster where Name = 'NeonatalHistory')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('NeonatalHistory','NeonatalHistory','0')
GO

IF not exists(select * from LookupItem where Name = 'NeonatalRecord')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('NeonatalRecord','Record Neonatal History','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'NeonatalRecord')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='NeonatalRecord' AND lm.Name = 'NeonatalHistory')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='NeonatalHistory' and lit.Name='NeonatalRecord'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'NeonatalNotes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('NeonatalNotes','Neonatal History Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'NeonatalNotes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='NeonatalNotes' AND lm.Name = 'NeonatalHistory')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='NeonatalHistory' and lit.Name='NeonatalNotes'
		END
	END
GO