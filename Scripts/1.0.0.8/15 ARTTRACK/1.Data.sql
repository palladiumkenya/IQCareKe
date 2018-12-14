IF not exists(select * from LookupItem where Name = 'ARTFastTrack')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ARTFastTrack','ART Fast Track','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ARTFastTrack')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ARTFastTrack' AND lm.Name = 'EncounterType')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'10.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EncounterType' and lit.Name='ARTFastTrack'
		END
	END
GO



IF not exists(select * from LookupItem where Name = 'ARTFastTrackReferral')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ARTFastTrackReferral','ART Fast Track Referral','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ARTFastTrackReferral')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ARTFastTrackReferral' AND lm.Name = 'AppointmentReason')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AppointmentReason' and lit.Name='ARTFastTrackReferral'
		END
	END
GO


