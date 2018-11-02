--Nutrition Assessment Questions
IF not Exists(select * from LookupMaster where Name = 'NutritionAssessment')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('NutritionAssessment','NutritionAssessmentQ','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'NutritionAssessmentQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('NutritionAssessmentQ1','Nutrition Assessment Comments','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'NutritionAssessmentQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='NutritionAssessmentQ1' AND lm.Name = 'NutritionAssessment')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='NutritionAssessment' and lit.Name='NutritionAssessmentQ1'
		END
	END
GO
---Q1 items
IF not Exists(select * from LookupMaster where Name = 'NutritionAssessmentQ1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('NutritionAssessmentQ1','NutritionAssessmentQ1','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'NutritionAssessmentQ1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='NutritionAssessmentQ1' and lit.Name='Notes'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'NutritionAssessmentQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('NutritionAssessmentQ2','Nutrition counselling/Referral provided?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'NutritionAssessmentQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='NutritionAssessmentQ2' AND lm.Name = 'NutritionAssessment')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='NutritionAssessment' and lit.Name='NutritionAssessmentQ2'
		END
	END
GO
--q2 items
IF not Exists(select * from LookupMaster where Name = 'NutritionAssessmentQ2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('NutritionAssessmentQ2','NutritionAssessmentQ2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'NutritionAssessmentQ2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='NutritionAssessmentQ2' and lit.Name='GeneralYesNo'
		END
	END
GO