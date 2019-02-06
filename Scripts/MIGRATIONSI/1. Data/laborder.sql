-- Routine VL
IF  exists(select * from LookupItem where Name = 'routine')
	BEGIN
		UPDATE LookupItem SET Name = 'Routine VL',DisplayName='Routine VL' WHERE Name = 'routine'
	END
GO 
IF  exists(select * from LookupMasterItem where DisplayName = 'routine' and LookupMasterId = (SELECT Id FROM LookupMaster WHERE Name = 'LabOrderReason'))
	BEGIN
		UPDATE LookupMasterItem SET DisplayName = 'Routine VL',OrdRank = '1.00' WHERE DisplayName = 'Routine' and LookupMasterId = (SELECT Id FROM LookupMaster WHERE Name = 'LabOrderReason')
	END
ELSE
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Routine VL' AND lm.Name = 'LabOrderReason')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='LabOrderReason' and lit.Name='Routine VL'
		END
	END
GO

-- Baseline VL
IF  exists(select * from LookupItem where Name = 'Baseline')
	BEGIN 
		UPDATE LookupItem SET Name = 'Baseline VL (Infants diagnosed through IED)' WHERE Name = 'Baseline'
		
	END
GO
IF  exists(select * from LookupMasterItem where DisplayName = 'Baseline' and LookupMasterId = (SELECT Id FROM LookupMaster WHERE Name = 'LabOrderReason'))
	BEGIN 
		UPDATE LookupMasterItem SET DisplayName = 'Baseline VL (Infants diagnosed through IED)',OrdRank = '5.00' WHERE DisplayName = 'Baseline' and LookupMasterId = (SELECT Id FROM LookupMaster WHERE Name = 'LabOrderReason')
	END
ELSE
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Baseline VL (Infants diagnosed through IED)' AND lm.Name = 'LabOrderReason')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='LabOrderReason' and lit.Name='Baseline VL (Infants diagnosed through IED)'
		END
	END
GO


-- Confirmation of Treatment Failure
IF  exists(select * from LookupItem where Name = 'Suspected Drug Resistance')
	BEGIN 
		UPDATE LookupItem SET Name = 'Confirmation of Treatment Failure (Repeat VL)',DisplayName = 'Confirmation of Treatment Failure (Repeat VL)' WHERE Name = 'Suspected Drug Resistance'	
	END
GO
IF  exists(select * from LookupMasterItem where DisplayName = 'Suspected Drug Resistance')
	BEGIN
		UPDATE LookupMasterItem SET DisplayName = 'Confirmation of Treatment Failure (Repeat VL)',OrdRank = '2.00' WHERE DisplayName = 'Suspected Drug Resistance'	
	END
ELSE
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Confirmation of Treatment Failure (Repeat VL)' AND lm.Name = 'LabOrderReason')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='LabOrderReason' and lit.Name='Confirmation of Treatment Failure (Repeat VL)'
		END
	END
GO

-- Clinical Failure
IF not exists(select * from LookupItem where Name = 'Clinical Failure')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Clinical Failure','Clinical Failure','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Clinical Failure')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Clinical Failure' AND lm.Name = 'LabOrderReason')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='LabOrderReason' and lit.Name='Clinical Failure'
		END
	END
GO

-- Single Drug Duration
IF not exists(select * from LookupItem where Name = 'Single Drug Substitution')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Single Drug Substitution','Single Drug Substitution','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Single Drug Substitution')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Single Drug Substitution' AND lm.Name = 'LabOrderReason')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='LabOrderReason' and lit.Name='Single Drug Substitution'
		END
	END
GO

-- Other
IF  exists(select * from LookupMasterItem where DisplayName = 'Other' and LookupMasterId = (SELECT Id FROM LookupMaster WHERE Name = 'LabOrderReason'))
	BEGIN 
		UPDATE LookupMasterItem SET DisplayName = 'Other',OrdRank = '6.00' WHERE DisplayName = 'Other' and LookupMasterId = (SELECT Id FROM LookupMaster WHERE Name = 'LabOrderReason')
	END
ELSE
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Other' AND lm.Name = 'LabOrderReason')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='LabOrderReason' and lit.Name='Other'
		END
	END
GO
