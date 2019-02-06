--- Rugute ---
--============================================================================================================================================
-- CLINICAL SUMMARY SECTION
-- Created by: Rugute
-- Created on 17/07/2018
--============================================================================================================================================

IF not Exists(select * from LookupMaster where Name = 'CaseSummary')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('CaseSummary','CaseSummary','0')
GO
--PrimaryReason
IF not exists(select * from LookupItem where Name = 'PrimaryReason')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PrimaryReason','What is the primary reason for this consultation?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PrimaryReason')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PrimaryReason' AND lm.Name = 'CaseSummary')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CaseSummary' and lit.Name='PrimaryReason'
		END
	END
GO
--Case Clinical Evaluation
IF not exists(select * from LookupItem where Name = 'CaseClinicalEvaluation')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('CaseClinicalEvaluation','Clinical Evaluation: history, physical, diagnostics, working diagnosis (excluding the information in the table below)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'CaseClinicalEvaluation')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='CaseClinicalEvaluation' AND lm.Name = 'CaseSummary')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CaseSummary' and lit.Name='CaseClinicalEvaluation'
		END
	END
GO


-- Case Summary

IF not Exists(select * from LookupMaster where Name = 'ClinicalEvaluation')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('ClinicalEvaluation','ClinicalEvaluation','0')
GO
--Question 1
IF not exists(select * from LookupItem where Name = 'ClinicalEvaluationQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ClinicalEvaluationQ1','Number of adherence counselling/assessment sessions done in the last 3-6 months','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ClinicalEvaluationQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ClinicalEvaluationQ1' AND lm.Name = 'ClinicalEvaluation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='ClinicalEvaluation' and lit.Name='ClinicalEvaluationQ1'
		END
	END
GO

-- items
IF not Exists(select * from LookupMaster where Name = 'ClinicalEvaluationQ1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('ClinicalEvaluationQ1','ClinicalEvaluationQ1','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'ClinicalEvaluationQ1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='ClinicalEvaluationQ1' and lit.Name='Notes'
		END
	END
GO


-- Question 2 
IF not exists(select * from LookupItem where Name = 'ClinicalEvaluationQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ClinicalEvaluationQ2','Number of home visits conducted in last 3-6 months, and findings','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ClinicalEvaluationQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ClinicalEvaluationQ2' AND lm.Name = 'ClinicalEvaluation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='ClinicalEvaluation' and lit.Name='ClinicalEvaluationQ2'
		END
	END
GO
--Items
IF not Exists(select * from LookupMaster where Name = 'ClinicalEvaluationQ2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('ClinicalEvaluationQ2','ClinicalEvaluationQ2','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'ClinicalEvaluationQ2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='ClinicalEvaluationQ2' and lit.Name='Notes'
		END
	END
GO

-- Question 3
IF not exists(select * from LookupItem where Name = 'ClinicalEvaluationQ3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ClinicalEvaluationQ3','Support structures (e.g. treatment buddy, support group attendance, caregivers) in place for this patient?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ClinicalEvaluationQ3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ClinicalEvaluationQ3' AND lm.Name = 'ClinicalEvaluation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='ClinicalEvaluation' and lit.Name='ClinicalEvaluationQ3'
		END
	END
GO
--Items
IF not Exists(select * from LookupMaster where Name = 'ClinicalEvaluationQ3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('ClinicalEvaluationQ3','ClinicalEvaluationQ3','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'ClinicalEvaluationQ3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='ClinicalEvaluationQ3' and lit.Name='Notes'
		END
	END
GO

-- Question 4
IF not exists(select * from LookupItem where Name = 'ClinicalEvaluationQ4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ClinicalEvaluationQ4','Evidence of adherence concerns (e.g. missed appointments, pill counts)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ClinicalEvaluationQ4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ClinicalEvaluationQ4' AND lm.Name = 'ClinicalEvaluation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='ClinicalEvaluation' and lit.Name='ClinicalEvaluationQ4'
		END
	END
GO
--Items
IF not Exists(select * from LookupMaster where Name = 'ClinicalEvaluationQ4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('ClinicalEvaluationQ4','ClinicalEvaluationQ4','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'ClinicalEvaluationQ4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='ClinicalEvaluationQ4' and lit.Name='Notes'
		END
	END
GO
-- Question 5
IF not exists(select * from LookupItem where Name = 'ClinicalEvaluationQ5')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ClinicalEvaluationQ5','Number of DOTS done in last 3-6 months','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ClinicalEvaluationQ5')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ClinicalEvaluationQ5' AND lm.Name = 'ClinicalEvaluation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='ClinicalEvaluation' and lit.Name='ClinicalEvaluationQ5'
		END
	END
GO
--Items
IF not Exists(select * from LookupMaster where Name = 'ClinicalEvaluationQ5')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('ClinicalEvaluationQ5','ClinicalEvaluationQ5','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'ClinicalEvaluationQ5')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='ClinicalEvaluationQ5' and lit.Name='Notes'
		END
	END
GO
-- Question 6
IF not exists(select * from LookupItem where Name = 'ClinicalEvaluationQ6')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ClinicalEvaluationQ6','Number of DOTS done in last 3-6 months','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ClinicalEvaluationQ6')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ClinicalEvaluationQ6' AND lm.Name = 'ClinicalEvaluation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='ClinicalEvaluation' and lit.Name='ClinicalEvaluationQ6'
		END
	END
GO
--Items
IF not Exists(select * from LookupMaster where Name = 'ClinicalEvaluationQ6')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('ClinicalEvaluationQ6','ClinicalEvaluationQ6','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'ClinicalEvaluationQ6')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='ClinicalEvaluationQ6' and lit.Name='Notes'
		END
	END
GO

-- Question 7
IF not exists(select * from LookupItem where Name = 'ClinicalEvaluationQ7')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ClinicalEvaluationQ7','Likely root cause/s of poor adherence for this patient (e.g. stigma, disclosure, side effects, alcohol or ... etc.)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ClinicalEvaluationQ7')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ClinicalEvaluationQ7' AND lm.Name = 'ClinicalEvaluation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='ClinicalEvaluation' and lit.Name='ClinicalEvaluationQ7'
		END
	END
GO
--Items
IF not Exists(select * from LookupMaster where Name = 'ClinicalEvaluationQ7')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('ClinicalEvaluationQ7','ClinicalEvaluationQ7','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'ClinicalEvaluationQ7')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='ClinicalEvaluationQ7' and lit.Name='Notes'
		END
	END
GO

--Question 8 Radio items
-- Other causes of treatment failure
IF not Exists(select * from LookupMaster where Name = 'OtherCausesOfTreatmentFailure')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('OtherCausesOfTreatmentFailure','OtherCausesOfTreatmentFailure','0')
GO

IF not exists(select * from LookupItem where Name = 'InadequateDosing')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('InadequateDosing','Inadequate dosing/dose adjustments (particularly for children)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'InadequateDosing')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='InadequateDosing' AND lm.Name = 'OtherCausesOfTreatmentFailure')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='OtherCausesOfTreatmentFailure' and lit.Name='InadequateDosing'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'DrugInteractions')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DrugInteractions',' Drug-drug interactions','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DrugInteractions')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DrugInteractions' AND lm.Name = 'OtherCausesOfTreatmentFailure')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='OtherCausesOfTreatmentFailure' and lit.Name='DrugInteractions'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'DrugFoodInteractions')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DrugFoodInteractions','  Drug-food interactions','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DrugFoodInteractions')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DrugFoodInteractions' AND lm.Name = 'OtherCausesOfTreatmentFailure')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='OtherCausesOfTreatmentFailure' and lit.Name='DrugFoodInteractions'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Impairedabsorption')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Impairedabsorption','Impaired absorption (e.g. chronic severe diarrhoea)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Impairedabsorption')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Impairedabsorption' AND lm.Name = 'OtherCausesOfTreatmentFailure')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='OtherCausesOfTreatmentFailure' and lit.Name='Impairedabsorption'
		END
	END
GO



---Question 8
IF not exists(select * from LookupItem where Name = 'ClinicalEvaluationQ8')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ClinicalEvaluationQ8','Evaluation for other causes of treatment failure, e.g.','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ClinicalEvaluationQ8')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ClinicalEvaluationQ8' AND lm.Name = 'ClinicalEvaluation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='ClinicalEvaluation' and lit.Name='ClinicalEvaluationQ8'
		END
	END
GO
IF not Exists(select * from LookupMaster where Name = 'ClinicalEvaluationQ8')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('ClinicalEvaluationQ8','ClinicalEvaluationQ8','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'ClinicalEvaluationQ8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='ClinicalEvaluationQ8' and lit.Name='Notes'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'OtherCausesOfTreatmentFailure')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('OtherCausesOfTreatmentFailure','OtherCausesOfTreatmentFailure','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'OtherCausesOfTreatmentFailure')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='OtherCausesOfTreatmentFailure' AND lm.Name = 'ClinicalEvaluationQ8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrPCQank from LookupMaster lm,LookupItem lit where lm.Name='ClinicalEvaluationQ8' and lit.Name='OtherCausesOfTreatmentFailure'
		END
	END
GO
---  Other Relevant ART History

IF not Exists(select * from LookupMaster where Name = 'OtherRelevantARTHistory')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('OtherRelevantARTHistory',' Other Relevant ART History','0')
GO
--CommentOnTreatment
IF not exists(select * from LookupItem where Name = 'CommentOnTreatment')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('CommentOnTreatment',' Comment on treatment interruptions (if any)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'CommentOnTreatment')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='CommentOnTreatment' AND lm.Name = 'OtherRelevantARTHistory')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='OtherRelevantARTHistory' and lit.Name='CommentOnTreatment'
		END
	END
GO
 -- Has Drug Resistance/Sensitivity Testing been done
 IF not exists(select * from LookupItem where Name = 'DrugResistanceTestDone')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('DrugResistanceTestDone','Has Drug Resistance/Sensitivity Testing been done for this patient? If yes, state date done and attach the detailed results','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'DrugResistanceTestDone')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='DrugResistanceTestDone' AND lm.Name = 'OtherRelevantARTHistory')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='OtherRelevantARTHistory' and lit.Name='DrugResistanceTestDone'
		END
	END
GO
-- Multidisciplinary team discussed the patient’s case
 IF not exists(select * from LookupItem where Name = 'MultidisciplinaryTeamDiscussedPatientCase')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MultidisciplinaryTeamDiscussedPatientCase','Has facility multidisciplinary team discussed the patient’s case? If yes, comment on date, deliberations and recommendations','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MultidisciplinaryTeamDiscussedPatientCase')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MultidisciplinaryTeamDiscussedPatientCase' AND lm.Name = 'OtherRelevantARTHistory')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='OtherRelevantARTHistory' and lit.Name='MultidisciplinaryTeamDiscussedPatientCase'
		END
	END
GO
-- MDT members
IF not exists(select * from LookupItem where Name = 'MDTmembers')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MDTmembers','MDT members who participated in the case discussion (names and titles)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MDTmembers')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MDTmembers' AND lm.Name = 'OtherRelevantARTHistory')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='OtherRelevantARTHistory' and lit.Name='MDTmembers'
		END
	END
GO