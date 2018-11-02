-- ICF DATA
IF not Exists(select * from LookupMaster where Name = 'TBFindings')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('TBFindings','TBFindings','0')
GO
--NoTB
IF not exists(select * from LookupItem where Name = 'NoTb')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('NoTb','Negative TB Screen - NoTB','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'NoTb')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='NoTb' AND lm.Name = 'TBFindings')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TBFindings' and lit.Name='NoTb'
		END
	END
GO
--PrTB
IF not exists(select * from LookupItem where Name = 'PrTB')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PrTB','Presumptive TB - PrTB','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PrTB')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PrTB' AND lm.Name = 'TBFindings')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TBFindings' and lit.Name='PrTB'
		END
	END
GO
--TBRx
IF not exists(select * from LookupItem where Name = 'TBRx')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('TBRx','Client on TB Treatment - TBRx','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'TBRx')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='TBRx' AND lm.Name = 'TBFindings')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TBFindings' and lit.Name='TBRx'
		END
	END
GO
--ND
IF not exists(select * from LookupItem where Name = 'ND')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ND','Not Done - ND','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ND')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ND' AND lm.Name = 'TBFindings')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TBFindings' and lit.Name='ND'
		END
	END
GO
--INH
IF not exists(select * from LookupItem where Name = 'INH')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('INH','Client on INH - INH','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'INH')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='INH' AND lm.Name = 'TBFindings')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TBFindings' and lit.Name='INH'
		END
	END
GO

--TB REGINEM
IF not Exists(select * from LookupMaster where Name = 'TBRegimen')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('TBRegimen','TBRegimen','0')
GO
--2RHZ|4rh
IF not exists(select * from LookupItem where Name = '2RHZ|4rh')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('2RHZ|4rh','2RHZ|4rh','0')
	END
GO

IF  exists(select * from LookupItem where Name = '2RHZ|4rh')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='2RHZ|4rh' AND lm.Name = 'TBRegimen')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TBRegimen' and lit.Name='2RHZ|4rh'
		END
	END
GO
--2RHZE|4RH
IF not exists(select * from LookupItem where Name = '2RHZE|4RH')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('2RHZE|4RH','2RHZE|4RH','0')
	END
GO

IF  exists(select * from LookupItem where Name = '2RHZE|4RH')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='2RHZE|4RH' AND lm.Name = 'TBRegimen')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TBRegimen' and lit.Name='2RHZE|4RH'
		END
	END
GO
--2SRHZE|4RHZE
IF not exists(select * from LookupItem where Name = '2SRHZE|4RHZE')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('2SRHZE|4RHZE','2SRHZE|4RHZE','0')
	END
GO

IF  exists(select * from LookupItem where Name = '2SRHZE|4RHZE')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='2SRHZE|4RHZE' AND lm.Name = 'TBRegimen')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TBRegimen' and lit.Name='2SRHZE|4RHZE'
		END
	END
GO
--2SRHZE|1RHZE|5RHE
IF not exists(select * from LookupItem where Name = '2SRHZE|1RHZE|5RHE')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('2SRHZE|1RHZE|5RHE','2SRHZE|1RHZE|5RHE','0')
	END
GO

IF  exists(select * from LookupItem where Name = '2SRHZE|1RHZE|5RHE')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='2SRHZE|1RHZE|5RHE' AND lm.Name = 'TBRegimen')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TBRegimen' and lit.Name='2SRHZE|1RHZE|5RHE'
		END
	END
GO
--2RHZE|10RH
IF not exists(select * from LookupItem where Name = '2RHZE|10RH')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('2RHZE|10RH','2RHZE|10RH','0')
	END
GO

IF  exists(select * from LookupItem where Name = '2RHZE|10RH')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='2RHZE|10RH' AND lm.Name = 'TBRegimen')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TBRegimen' and lit.Name='2RHZE|10RH'
		END
	END
GO
--3RHZE|5RHE
IF not exists(select * from LookupItem where Name = '3RHZE|5RHE')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('3RHZE|5RHE','3RHZE|5RHE','0')
	END
GO

IF  exists(select * from LookupItem where Name = '3RHZE|5RHE')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='3RHZE|5RHE' AND lm.Name = 'TBRegimen')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TBRegimen' and lit.Name='3RHZE|5RHE'
		END
	END
GO
--Other
IF not exists(select * from LookupItem where Name = 'Other')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Other','Other','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Other')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Other' AND lm.Name = 'TBRegimen')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='TBRegimen' and lit.Name='Other'
		END
	END
GO

--ICF Action Taken
--Sputum Smear
IF not Exists(select * from LookupMaster where Name = 'SputumSmear')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SputumSmear','SputumSmear','0')
GO
--Ordered
IF not exists(select * from LookupItem where Name = 'Ordered')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Ordered','Ordered','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Ordered')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Ordered' AND lm.Name = 'SputumSmear')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SputumSmear' and lit.Name='Ordered'
		END
	END
GO
--Positive
IF not exists(select * from LookupItem where Name = 'Positive')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Positive','Positive','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Positive')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Positive' AND lm.Name = 'SputumSmear')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SputumSmear' and lit.Name='Positive'
		END
	END
GO
--Negative
IF not exists(select * from LookupItem where Name = 'Negative')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Negative','Negative','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Negative')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Negative' AND lm.Name = 'SputumSmear')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SputumSmear' and lit.Name='Negative'
		END
	END
GO
--Not Done
IF not exists(select * from LookupItem where Name = 'NotDone')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('NotDone','Not Done','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'NotDone')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='NotDone' AND lm.Name = 'SputumSmear')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SputumSmear' and lit.Name='NotDone'
		END
	END
GO


--Gene Expert
IF not Exists(select * from LookupMaster where Name = 'GeneExpert')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('GeneExpert','GeneExpert','0')
GO
--Ordered
IF not exists(select * from LookupItem where Name = 'Ordered')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Ordered','Ordered','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Ordered')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Ordered' AND lm.Name = 'GeneExpert')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='GeneExpert' and lit.Name='Ordered'
		END
	END
GO
--Positive
IF not exists(select * from LookupItem where Name = 'Positive')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Positive','Positive','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Positive')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Positive' AND lm.Name = 'GeneExpert')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='GeneExpert' and lit.Name='Positive'
		END
	END
GO
--Negative
IF not exists(select * from LookupItem where Name = 'Negative')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Negative','Negative','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Negative')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Negative' AND lm.Name = 'GeneExpert')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='GeneExpert' and lit.Name='Negative'
		END
	END
GO
--Not Done
IF not exists(select * from LookupItem where Name = 'NotDone')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('NotDone','Not Done','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'NotDone')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='NotDone' AND lm.Name = 'GeneExpert')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='GeneExpert' and lit.Name='NotDone'
		END
	END
GO


--Chest X-Ray
IF not Exists(select * from LookupMaster where Name = 'ChestXray')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('ChestXray','ChestXray','0')
GO
--Ordered
IF not exists(select * from LookupItem where Name = 'Ordered')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Ordered','Ordered','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Ordered')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Ordered' AND lm.Name = 'ChestXray')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ChestXray' and lit.Name='Ordered'
		END
	END
GO
--Suggestive
IF not exists(select * from LookupItem where Name = 'Suggestive')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Suggestive','Suggestive','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Suggestive')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Suggestive' AND lm.Name = 'ChestXray')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ChestXray' and lit.Name='Suggestive'
		END
	END
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
		lit.Name='Normal' AND lm.Name = 'ChestXray')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ChestXray' and lit.Name='Normal'
		END
	END
GO
--Not Done
IF not exists(select * from LookupItem where Name = 'NotDone')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('NotDone','Not Done','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'NotDone')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='NotDone' AND lm.Name = 'ChestXray')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='ChestXray' and lit.Name='NotDone'
		END
	END
GO