-- Parent Dropdown (FollowupEducation) items [ProgressionRX,BasicPreventionDisclosureEducation,] 
-- CounsellingTypes
IF not Exists(select * from LookupMaster where Name = 'CounsellingTypes')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('CounsellingTypes','Counselling Types','0')
GO
--- Items 1
IF not exists(select * from LookupItem where Name = 'ProgressionRX')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ProgressionRX','Progression, RX','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ProgressionRX')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ProgressionRX' AND lm.Name = 'CounsellingTypes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CounsellingTypes' and lit.Name='ProgressionRX'
		END
	END
GO
--- Items 2
IF not exists(select * from LookupItem where Name = 'BasicPreventionDisclosureEducation')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('BasicPreventionDisclosureEducation','Basic Prevention Disclosure Education','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'BasicPreventionDisclosureEducation')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='BasicPreventionDisclosureEducation' AND lm.Name = 'CounsellingTypes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CounsellingTypes' and lit.Name='BasicPreventionDisclosureEducation'
		END
	END
GO

-- Dropdown [BasicPreventionDisclosureEducation]
IF not Exists(select * from LookupMaster where Name = 'BasicPreventionDisclosureEducation')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('BasicPreventionDisclosureEducation','Basic Prevention Disclosure Education','0')
GO

--- BasicPreventionDisclosureEducation Items

-- Items 1 : Available treatment/prophylaxis (CTX,INH)
IF not exists(select * from LookupItem where Name = 'AvailableTreatment')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('AvailableTreatment','Available treatment/prophylaxis (CTX,INH)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'AvailableTreatment')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='AvailableTreatment' AND lm.Name = 'BasicPreventionDisclosureEducation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BasicPreventionDisclosureEducation' and lit.Name='AvailableTreatment'
		END
	END
GO
-- Items 2 : Follow -up appoinment, care and treatment
IF not exists(select * from LookupItem where Name = 'FollowupAppointment')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('FollowupAppointment','Followup appointment, care and treatment','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'FollowupAppointment')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='FollowupAppointment' AND lm.Name = 'BasicPreventionDisclosureEducation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BasicPreventionDisclosureEducation' and lit.Name='FollowupAppointment'
		END
	END
GO
-- Dropdown [ProgressionRX]
IF not Exists(select * from LookupMaster where Name = 'ProgressionRX')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('ProgressionRX','Progression, RX','0')
GO

--- ProgressionRX Items

-- Items 1 : Available treatment/prophylaxis (CTX,INH)
IF not exists(select * from LookupItem where Name = 'AvailableTreatment')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('AvailableTreatment','Available treatment/prophylaxis (CTX,INH)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'AvailableTreatment')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='AvailableTreatment' AND lm.Name = 'ProgressionRX')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ProgressionRX' and lit.Name='AvailableTreatment'
		END
	END
GO
-- Items 2 : Follow -up appoinment, care and treatment
IF not exists(select * from LookupItem where Name = 'FollowupAppointment')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('FollowupAppointment','Followup appointment, care and treatment','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'FollowupAppointment')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='FollowupAppointment' AND lm.Name = 'ProgressionRX')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ProgressionRX' and lit.Name='FollowupAppointment'
		END
	END
GO
