-- Alxcohol Screening Questions
IF not Exists(select * from LookupMaster where Name = 'AlcoholScreeningQuestions')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('AlcoholScreeningQuestions','AlcoholScreeningQuestions','0')
GO

IF not exists(select * from LookupItem where Name = 'AlcoholScreeningQuestion1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('AlcoholScreeningQuestion1','Have you felt you should cut down on your drinking or drug use?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'AlcoholScreeningQuestion1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='AlcoholScreeningQuestion1' AND lm.Name = 'AlcoholScreeningQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AlcoholScreeningQuestions' and lit.Name='AlcoholScreeningQuestion1'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'AlcoholScreeningQuestion2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('AlcoholScreeningQuestion2','Have people ever annoyed you by criticizing your drinking or drug use?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'AlcoholScreeningQuestion2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='AlcoholScreeningQuestion2' AND lm.Name = 'AlcoholScreeningQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AlcoholScreeningQuestions' and lit.Name='AlcoholScreeningQuestion2'
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'AlcoholScreeningQuestion3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('AlcoholScreeningQuestion3','Have you ever felt bad or guilty about your drinking or drug use?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'AlcoholScreeningQuestion3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='AlcoholScreeningQuestion3' AND lm.Name = 'AlcoholScreeningQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AlcoholScreeningQuestions' and lit.Name='AlcoholScreeningQuestion3'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'AlcoholScreeningQuestion4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('AlcoholScreeningQuestion4','Have you ever had a drink or used drugs first thing in the morning to steady your nerves or to get rid of a hangover','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'AlcoholScreeningQuestion4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='AlcoholScreeningQuestion4' AND lm.Name = 'AlcoholScreeningQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AlcoholScreeningQuestions' and lit.Name='AlcoholScreeningQuestion4'
		END
	END
GO


-- Smoking Screening Questions
IF not Exists(select * from LookupMaster where Name = 'SmokingScreeningQuestions')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SmokingScreeningQuestions','SmokingScreeningQuestions','0')
GO

IF not exists(select * from LookupItem where Name = 'SmokingScreeningQuestion1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SmokingScreeningQuestion1','Have you tried to stop smoking?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SmokingScreeningQuestion1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SmokingScreeningQuestion1' AND lm.Name = 'SmokingScreeningQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SmokingScreeningQuestions' and lit.Name='SmokingScreeningQuestion1'
		END
	END
GO

--Alcohol Risk Levels
IF not Exists(select * from LookupMaster where Name = 'AlcoholRiskLevel')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('AlcoholRiskLevel','AlcoholRiskLevel','0')
GO

IF not exists(select * from LookupItem where Name = 'RiskLevel1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('RiskLevel1','Low Risk','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'RiskLevel1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='RiskLevel1' AND lm.Name = 'AlcoholRiskLevel')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AlcoholRiskLevel' and lit.Name='RiskLevel1'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'RiskLevel2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('RiskLevel2','Low Risk','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'RiskLevel2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='RiskLevel2' AND lm.Name = 'AlcoholRiskLevel')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AlcoholRiskLevel' and lit.Name='RiskLevel2'
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'RiskLevel3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('RiskLevel3','High Risk','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'RiskLevel3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='RiskLevel3' AND lm.Name = 'AlcoholRiskLevel')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AlcoholRiskLevel' and lit.Name='RiskLevel3'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'RiskLevel4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('RiskLevel4','High Risk','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'RiskLevel4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='RiskLevel4' AND lm.Name = 'AlcoholRiskLevel')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AlcoholRiskLevel' and lit.Name='RiskLevel4'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'RiskLevel5')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('RiskLevel5','High Risk','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'RiskLevel5')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='RiskLevel5' AND lm.Name = 'AlcoholRiskLevel')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AlcoholRiskLevel' and lit.Name='RiskLevel5'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'RiskLevel6')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('RiskLevel6','High Risk','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'RiskLevel6')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='RiskLevel6' AND lm.Name = 'AlcoholRiskLevel')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AlcoholRiskLevel' and lit.Name='RiskLevel6'
		END
	END
GO


---ALCOHOL SCREENING NOTES
IF not Exists(select * from LookupMaster where Name = 'AlcoholScreeningNotes')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('AlcoholScreeningNotes','AlcoholScreeningNotes','0')
GO

IF not exists(select * from LookupItem where Name = 'AlcoholScore')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('AlcoholScore','Score','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'AlcoholScore')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='AlcoholScore' AND lm.Name = 'AlcoholScreeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AlcoholScreeningNotes' and lit.Name='AlcoholScore'
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'AlcoholRiskLevel')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('AlcoholRiskLevel','Alcohol Risk','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'AlcoholRiskLevel')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='AlcoholRiskLevel' AND lm.Name = 'AlcoholScreeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AlcoholScreeningNotes' and lit.Name='AlcoholRiskLevel'
		END
	END
GO