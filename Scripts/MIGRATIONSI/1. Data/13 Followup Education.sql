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
--- Items 3
IF not exists(select * from LookupItem where Name = 'ARVPreparationInitiationSupportMonitor')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ARVPreparationInitiationSupportMonitor','ARV Preparation, Initiation, Support and Monitor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ARVPreparationInitiationSupportMonitor')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ARVPreparationInitiationSupportMonitor' AND lm.Name = 'CounsellingTypes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CounsellingTypes' and lit.Name='ARVPreparationInitiationSupportMonitor'
		END
	END
GO
--- Items 4
IF not exists(select * from LookupItem where Name = 'HomeBasedcareSupport')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('HomeBasedcareSupport','Home-based care, Support','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'HomeBasedcareSupport')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='HomeBasedcareSupport' AND lm.Name = 'CounsellingTypes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CounsellingTypes' and lit.Name='HomeBasedcareSupport'
		END
	END
GO
--- Items 5
IF not exists(select * from LookupItem where Name = 'Other')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Other','Other','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Other')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Other' AND lm.Name = 'CounsellingTypes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CounsellingTypes' and lit.Name='Other'
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
-- Items 2 : Follow-up appoinment, care and treatment
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
-- Items 3 : Progression of dieases
IF not exists(select * from LookupItem where Name = 'ProgressionofDieases')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ProgressionofDieases','Progression of disease','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ProgressionofDieases')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ProgressionofDieases' AND lm.Name = 'ProgressionRX')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ProgressionRX' and lit.Name='ProgressionofDieases'
		END
	END
GO

-- Dropdown [BasicPreventionDisclosureEducation]
IF not Exists(select * from LookupMaster where Name = 'BasicPreventionDisclosureEducation')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('BasicPreventionDisclosureEducation','Basic Prevention Disclosure Education','0')
GO

--- BasicPreventionDisclosureEducation Items

-- Items 1 : Basic Hiv Education Transmission
IF not exists(select * from LookupItem where Name = 'BasicHiveductaionTransmission')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('BasicHiveductaionTransmission','Basic Hiv education, transmission','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'BasicHiveductaionTransmission')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='BasicHiveductaionTransmission' AND lm.Name = 'BasicPreventionDisclosureEducation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BasicPreventionDisclosureEducation' and lit.Name='BasicHiveductaionTransmission'
		END
	END
GO
-- Items 2 : Child's blood test
IF not exists(select * from LookupItem where Name = 'ChildBloodTest')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ChildBloodTest','Child blood test','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ChildBloodTest')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ChildBloodTest' AND lm.Name = 'BasicPreventionDisclosureEducation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BasicPreventionDisclosureEducation' and lit.Name='ChildBloodTest'
		END
	END
GO
-- Items 3 : Disclosure
IF not exists(select * from LookupItem where Name = 'Disclosure')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Disclosure','Disclosure','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Disclosure')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Disclosure' AND lm.Name = 'BasicPreventionDisclosureEducation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BasicPreventionDisclosureEducation' and lit.Name='Disclosure'
		END
	END
GO
-- Items 4 : Family/Living situation
IF not exists(select * from LookupItem where Name = 'FamilyLivingsituation')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('FamilyLivingsituation','Family/Living situation','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'FamilyLivingsituation')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='FamilyLivingsituation' AND lm.Name = 'BasicPreventionDisclosureEducation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BasicPreventionDisclosureEducation' and lit.Name='FamilyLivingsituation'
		END
	END
GO
-- Items 5 : Malaria Prevention , IPT, ITN
IF not exists(select * from LookupItem where Name = 'MalariaPrevention')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MalariaPrevention','Malaria Prevention, IPT, ITN','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MalariaPrevention')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MalariaPrevention' AND lm.Name = 'BasicPreventionDisclosureEducation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BasicPreventionDisclosureEducation' and lit.Name='MalariaPrevention'
		END
	END
GO
-- Items 6 : Positive Living
IF not exists(select * from LookupItem where Name = 'PositiveLiving')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PositiveLiving','Positive Living','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PositiveLiving')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PositiveLiving' AND lm.Name = 'BasicPreventionDisclosureEducation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BasicPreventionDisclosureEducation' and lit.Name='PositiveLiving'
		END
	END
GO
-- Items 7 : Postest Counselling: implication of results
IF not exists(select * from LookupItem where Name = 'PostestCounselling')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PostestCounselling','Postest Counselling: implication of results','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PostestCounselling')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PostestCounselling' AND lm.Name = 'BasicPreventionDisclosureEducation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BasicPreventionDisclosureEducation' and lit.Name='PostestCounselling'
		END
	END
GO
-- Items 8 : Prevention: safe sex, Condoms
IF not exists(select * from LookupItem where Name = 'PreventionSafesex')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PreventionSafesex','Prevention: safe sex, Condoms','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PreventionSafesex')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PreventionSafesex' AND lm.Name = 'BasicPreventionDisclosureEducation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BasicPreventionDisclosureEducation' and lit.Name='PreventionSafesex'
		END
	END
GO
-- Items 9 : Prevention: Household precaution what is safe
IF not exists(select * from LookupItem where Name = 'PreventionHousehold')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PreventionHousehold','Prevention: Household precaution what is safe','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PreventionHousehold')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PreventionHousehold' AND lm.Name = 'BasicPreventionDisclosureEducation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'9.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BasicPreventionDisclosureEducation' and lit.Name='PreventionHousehold'
		END
	END
GO
-- Items 10 : Progression of dieases
IF not exists(select * from LookupItem where Name = 'ProgressionofDieases')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ProgressionofDieases','Progression of disease','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ProgressionofDieases')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ProgressionofDieases' AND lm.Name = 'BasicPreventionDisclosureEducation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'10.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BasicPreventionDisclosureEducation' and lit.Name='ProgressionofDieases'
		END
	END
GO
-- Items 11 : Reproductive Choices Prevention MTCT
IF not exists(select * from LookupItem where Name = 'ReproductiveChoices')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ReproductiveChoices','Reproductive Choices Prevention MTC T','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ReproductiveChoices')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ReproductiveChoices' AND lm.Name = 'BasicPreventionDisclosureEducation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'11.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BasicPreventionDisclosureEducation' and lit.Name='ReproductiveChoices'
		END
	END
GO
-- Items 12 : SharedConfidentiality
IF not exists(select * from LookupItem where Name = 'SharedConfidentiality')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SharedConfidentiality','Shared Confidentiality','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SharedConfidentiality')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SharedConfidentiality' AND lm.Name = 'BasicPreventionDisclosureEducation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'12.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BasicPreventionDisclosureEducation' and lit.Name='SharedConfidentiality'
		END
	END
GO
-- Items 13 : Testing Partners
IF not exists(select * from LookupItem where Name = 'TestingPartners')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('TestingPartners','Testing Partners','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'TestingPartners')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='TestingPartners' AND lm.Name = 'BasicPreventionDisclosureEducation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'13.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BasicPreventionDisclosureEducation' and lit.Name='TestingPartners'
		END
	END
GO
-- Items 14 : Disclosure to whom disclosed(list) 
IF not exists(select * from LookupItem where Name = 'Disclosure')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Disclosure','Disclosure to whom disclosed(list)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Disclosure')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Disclosure' AND lm.Name = 'BasicPreventionDisclosureEducation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'13.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BasicPreventionDisclosureEducation' and lit.Name='Disclosure'
		END
	END
GO