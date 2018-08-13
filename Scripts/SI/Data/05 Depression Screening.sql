-- Depression Screening
IF not Exists(select * from LookupMaster where Name = 'DepressionScreeningQuestions')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('DepressionScreeningQuestions','DepressionScreeningQuestions','0')
GO

IF not exists(select * from LookupItem where Name = 'DSQuestion1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DSQuestion1','Feeling down, depressed or hopeless?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DSQuestion1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DSQuestion1' AND lm.Name = 'DepressionScreeningQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DepressionScreeningQuestions' and lit.Name='DSQuestion1'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'DSQuestion2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DSQuestion2','Little interest or no pleasure in doing things','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DSQuestion2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DSQuestion2' AND lm.Name = 'DepressionScreeningQuestions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DepressionScreeningQuestions' and lit.Name='DSQuestion2'
		END
	END
GO

--PHQ9Questions
IF not Exists(select * from LookupMaster where Name = 'PHQ9Questions')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('PHQ9Questions','PHQ9Questions','0')
GO

IF not exists(select * from LookupItem where Name = 'PHQ9Question1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PHQ9Question1','Little interest or no pleasure in doing things?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PHQ9Question1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PHQ9Question1' AND lm.Name = 'PHQ9Questions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PHQ9Questions' and lit.Name='PHQ9Question1'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'PHQ9Questions2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PHQ9Questions2','Feeling down, depressed or hopeless?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PHQ9Questions2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PHQ9Questions2' AND lm.Name = 'PHQ9Questions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PHQ9Questions' and lit.Name='PHQ9Questions2'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'PHQ9Questions3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PHQ9Questions3','Trouble falling or staying asleep, or sleeping too much?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PHQ9Questions3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PHQ9Questions3' AND lm.Name = 'PHQ9Questions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PHQ9Questions' and lit.Name='PHQ9Questions3'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'PHQ9Questions4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PHQ9Questions4','Feeling tired or having little energy?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PHQ9Questions4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PHQ9Questions4' AND lm.Name = 'PHQ9Questions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PHQ9Questions' and lit.Name='PHQ9Questions4'
		END
	END
GO
IF not exists(select * from LookupItem where Name = 'PHQ9Questions5')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PHQ9Questions5','Poor appetite or overeating','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PHQ9Questions5')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PHQ9Questions5' AND lm.Name = 'PHQ9Questions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PHQ9Questions' and lit.Name='PHQ9Questions5'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'PHQ9Questions6')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PHQ9Questions6','Feeling bad about yourself - or that you are a failure or have let yourself or your family down?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PHQ9Questions6')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PHQ9Questions6' AND lm.Name = 'PHQ9Questions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PHQ9Questions' and lit.Name='PHQ9Questions6'
		END
	END
GO
IF not exists(select * from LookupItem where Name = 'PHQ9Questions7')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PHQ9Questions7','Trouble concentrating on things, such as reading the newpaper or watching television?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PHQ9Questions7')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PHQ9Questions7' AND lm.Name = 'PHQ9Questions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PHQ9Questions' and lit.Name='PHQ9Questions7'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'PHQ9Questions8')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PHQ9Questions8','Moving or speaking so slowly that other people could have noticed?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PHQ9Questions8')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PHQ9Questions8' AND lm.Name = 'PHQ9Questions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PHQ9Questions' and lit.Name='PHQ9Questions8'
		END
	END
GO
IF not exists(select * from LookupItem where Name = 'PHQ9Questions9')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PHQ9Questions9','Or the opposite - being so fidgety or restless that you have been moving around a lot more than usual?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PHQ9Questions9')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PHQ9Questions9' AND lm.Name = 'PHQ9Questions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'9.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PHQ9Questions' and lit.Name='PHQ9Questions9'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'PHQ9Questions10')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PHQ9Questions10','Thoughts that you would be better off dead, or of hurting yourself in some way?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PHQ9Questions10')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PHQ9Questions10' AND lm.Name = 'PHQ9Questions')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'10.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PHQ9Questions' and lit.Name='PHQ9Questions10'
		END
	END
GO

--Depression Severity
IF not Exists(select * from LookupMaster where Name = 'DepressionFrequency')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('DepressionFrequency','DepressionFrequency','0')
GO

IF not exists(select * from LookupItem where Name = 'NotatAll')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('NotatAll','Not at All','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'NotatAll')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='NotatAll' AND lm.Name = 'DepressionFrequency')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DepressionFrequency' and lit.Name='NotatAll'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'SeveralDays')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SeveralDays','Several Days','0')
	END
GO
IF  exists(select * from LookupItem where Name = 'SeveralDays')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SeveralDays' AND lm.Name = 'DepressionFrequency')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DepressionFrequency' and lit.Name='SeveralDays'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Morethanhalfthedays')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Morethanhalfthedays','More than half the days','0')
	END
GO
IF  exists(select * from LookupItem where Name = 'Morethanhalfthedays')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Morethanhalfthedays' AND lm.Name = 'DepressionFrequency')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DepressionFrequency' and lit.Name='Morethanhalfthedays'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'NearlyEveryDay')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('NearlyEveryDay','Nearly every day','0')
	END
GO
IF  exists(select * from LookupItem where Name = 'NearlyEveryDay')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='NearlyEveryDay' AND lm.Name = 'DepressionFrequency')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DepressionFrequency' and lit.Name='NearlyEveryDay'
		END
	END
GO


-- DEPRESSION SCREENING NOTES
IF not Exists(select * from LookupMaster where Name = 'DepressionScreeningNotes')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('DepressionScreeningNotes','DepressionScreeningNotes','0')
GO

IF not exists(select * from LookupItem where Name = 'DepressionTotal')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DepressionTotal','Total','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DepressionTotal')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DepressionTotal' AND lm.Name = 'DepressionScreeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DepressionScreeningNotes' and lit.Name='DepressionTotal'
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'DepressionSeverity')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DepressionSeverity','Depression Severity','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DepressionSeverity')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DepressionSeverity' AND lm.Name = 'DepressionScreeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DepressionScreeningNotes' and lit.Name='DepressionSeverity'
		END
	END
GO



IF not exists(select * from LookupItem where Name = 'ReccommendedManagement')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ReccommendedManagement','Recommended Management','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ReccommendedManagement')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ReccommendedManagement' AND lm.Name = 'DepressionScreeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DepressionScreeningNotes' and lit.Name='ReccommendedManagement'
		END
	END
GO


-- Depression Screening Severity
IF not Exists(select * from LookupMaster where Name = 'DepressionSeverity')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('DepressionSeverity','DepressionSeverity','0')
GO

IF not exists(select * from LookupItem where Name = 'None')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('None','None','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'None')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='None' AND lm.Name = 'DepressionSeverity')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'0.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DepressionSeverity' and lit.Name='None'
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'Mild')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Mild','Mild','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Mild')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Mild' AND lm.Name = 'DepressionSeverity')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DepressionSeverity' and lit.Name='Mild'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Moderate')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Moderate','Moderate','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Moderate')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Moderate' AND lm.Name = 'DepressionSeverity')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'10.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DepressionSeverity' and lit.Name='Moderate'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'ModerateSevere')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ModerateSevere','Moderate Severe','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ModerateSevere')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ModerateSevere' AND lm.Name = 'DepressionSeverity')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'15.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DepressionSeverity' and lit.Name='ModerateSevere'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Severe')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Severe','Severe','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Severe')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Severe' AND lm.Name = 'DepressionSeverity')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'20.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DepressionSeverity' and lit.Name='Severe'
		END
	END
GO


---RECOMMEMDED MANAGEMENT
IF not Exists(select * from LookupMaster where Name = 'RecommendedManagement')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('RecommendedManagement','RecommendedManagement','0')
GO

IF not exists(select * from LookupItem where Name = 'Severity0')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Severity0','None','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Severity0')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Severity0' AND lm.Name = 'RecommendedManagement')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'0.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='RecommendedManagement' and lit.Name='Severity0'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Severity5')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Severity5','Provide counselling support','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Severity5')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Severity5' AND lm.Name = 'RecommendedManagement')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='RecommendedManagement' and lit.Name='Severity5'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Severity10')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Severity10','Refer to psychistrist or mental health team.','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Severity10')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Severity10' AND lm.Name = 'RecommendedManagement')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'10.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='RecommendedManagement' and lit.Name='Severity10'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Severity15')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Severity15','Refer to psychistrist or mental health team.','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Severity15')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Severity15' AND lm.Name = 'RecommendedManagement')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'15.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='RecommendedManagement' and lit.Name='Severity15'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Severity20')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Severity20','Refer to psychistrist or mental health team.','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Severity20')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Severity20' AND lm.Name = 'RecommendedManagement')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'20.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='RecommendedManagement' and lit.Name='Severity20'
		END
	END
GO

-- DisclosedTo
IF not Exists(select * from LookupMaster where Name = 'DisclosedTo')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('DisclosedTo','DisclosedTo','0')
GO

IF not exists(select * from LookupItem where Name = 'Spouse')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Spouse','Spouse','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Spouse')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Spouse' AND lm.Name = 'DisclosedTo')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DisclosedTo' and lit.Name='Spouse'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Parent')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Parent','Parent','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Parent')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Parent' AND lm.Name = 'DisclosedTo')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DisclosedTo' and lit.Name='Parent'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Friends')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Friends','Friends','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Friends')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Friends' AND lm.Name = 'DisclosedTo')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DisclosedTo' and lit.Name='Friends'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Spiritual Leader')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Spiritual Leader','Spiritual Leader','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Spiritual Leader')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Spiritual Leader' AND lm.Name = 'DisclosedTo')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DisclosedTo' and lit.Name='Spiritual Leader'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Child')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Child','Child','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Child')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Child' AND lm.Name = 'DisclosedTo')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DisclosedTo' and lit.Name='Child'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Sibling')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Sibling','Sibling','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Sibling')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Sibling' AND lm.Name = 'DisclosedTo')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DisclosedTo' and lit.Name='Sibling'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Other')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Other','Other','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Other')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Other' AND lm.Name = 'DisclosedTo')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DisclosedTo' and lit.Name='Other'
		END
	END
GO

---Disclosure Questions
IF not Exists(select * from LookupMaster where Name = 'Disclosure')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Disclosure','Disclosure','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'DisclosureQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DisclosureQ1','Disclosed HIV status?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DisclosureQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DisclosureQ1' AND lm.Name = 'Disclosure')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Disclosure' and lit.Name='DisclosureQ1'
		END
	END
GO
--q1 items
IF not Exists(select * from LookupMaster where Name = 'DisclosureQ1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('DisclosureQ1','DisclosureQ1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'DisclosureQ1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DisclosureQ1' and lit.Name='GeneralYesNo'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'DisclosureQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DisclosureQ2','Disclosed To','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DisclosureQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DisclosureQ2' AND lm.Name = 'Disclosure')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Disclosure' and lit.Name='DisclosureQ2'
		END
	END
GO
--q2 items
IF not Exists(select * from LookupMaster where Name = 'DisclosureQ2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('DisclosureQ2','DisclosureQ2','0')
GO

IF not exists(select * from LookupItem where Name = 'DisclosedTo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DisclosedTo','DisclosedTo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DisclosedTo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DisclosedTo' AND lm.Name = 'DisclosureQ2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='DisclosureQ2' and lit.Name='DisclosedTo'
		END
	END
GO