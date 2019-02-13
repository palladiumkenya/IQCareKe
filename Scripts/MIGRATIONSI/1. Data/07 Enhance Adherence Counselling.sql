-- SESSIONS
IF not Exists(select * from LookupMaster where Name = 'EnhanceAdherenceCounselling')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('EnhanceAdherenceCounselling','EnhanceAdherenceCounselling','0')
GO

IF not exists(select * from LookupItem where Name = 'Session1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session1','Session 1','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session1' AND lm.Name = 'EnhanceAdherenceCounselling')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EnhanceAdherenceCounselling' and lit.Name='Session1'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Session2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2','Session 2','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2' AND lm.Name = 'EnhanceAdherenceCounselling')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EnhanceAdherenceCounselling' and lit.Name='Session2'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Session3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3','Session 3','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3' AND lm.Name = 'EnhanceAdherenceCounselling')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EnhanceAdherenceCounselling' and lit.Name='Session3'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Session4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4','Session 4','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4' AND lm.Name = 'EnhanceAdherenceCounselling')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EnhanceAdherenceCounselling' and lit.Name='Session4'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'ViralLoad')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('ViralLoad','Viral Load','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'ViralLoad')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='ViralLoad' AND lm.Name = 'EnhanceAdherenceCounselling')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EnhanceAdherenceCounselling' and lit.Name='ViralLoad'
		END
	END
GO


---Remembering Frequency
IF not Exists(select * from LookupMaster where Name = 'RememberingFrequency')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('RememberingFrequency','RememberingFrequency','0')
GO
--1
IF not exists(select * from LookupItem where Name = 'NeverRarely')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('NeverRarely','Never / Rarely','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'NeverRarely')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='NeverRarely' AND lm.Name = 'RememberingFrequency')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='RememberingFrequency' and lit.Name='NeverRarely'
		END
	END
GO
--2
IF not exists(select * from LookupItem where Name = 'Onceinawhile')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Onceinawhile','Once in a while','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Onceinawhile')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Onceinawhile' AND lm.Name = 'RememberingFrequency')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='RememberingFrequency' and lit.Name='Onceinawhile'
		END
	END
GO
--3
IF not exists(select * from LookupItem where Name = 'Sometimes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Sometimes','Sometimes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Sometimes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Sometimes' AND lm.Name = 'RememberingFrequency')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='RememberingFrequency' and lit.Name='Sometimes'
		END
	END
GO
--4
IF not exists(select * from LookupItem where Name = 'Usually')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Usually','Usually','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Usually')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Usually' AND lm.Name = 'RememberingFrequency')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='RememberingFrequency' and lit.Name='Usually'
		END
	END
GO
--5
IF not exists(select * from LookupItem where Name = 'Allthetime')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Allthetime','All the time','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Allthetime')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Allthetime' AND lm.Name = 'RememberingFrequency')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='RememberingFrequency' and lit.Name='Allthetime'
		END
	END
GO


--MMAS4
---MMAS4 Master
IF not Exists(select * from LookupMaster where Name = 'MMAS4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('MMAS4','MMAS4','0')
GO
---MMAS4 Questions
----MMAS4Q1
IF not exists(select * from LookupItem where Name = 'MMAS4Q1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MMAS4Q1','Do you forget to take your medicine since the last visit?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MMAS4Q1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MMAS4Q1' AND lm.Name = 'MMAS4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MMAS4' and lit.Name='MMAS4Q1'
		END
	END
GO
-----MMAS4Q1 Items
IF not Exists(select * from LookupMaster where Name = 'MMAS4Q1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('MMAS4Q1','MMAS4Q1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'MMAS4Q1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MMAS4Q1' and lit.Name='GeneralYesNo'
		END
	END
GO

----MMAS4Q2
IF not exists(select * from LookupItem where Name = 'MMAS4Q2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MMAS4Q2','Are you careless at times about taking your medicine?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MMAS4Q2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MMAS4Q2' AND lm.Name = 'MMAS4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MMAS4' and lit.Name='MMAS4Q2'
		END
	END
GO
-----MMAS4Q2 Items
IF not Exists(select * from LookupMaster where Name = 'MMAS4Q2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('MMAS4Q2','MMAS4Q2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'MMAS4Q2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MMAS4Q2' and lit.Name='GeneralYesNo'
		END
	END
GO


----MMAS4Q3
IF not exists(select * from LookupItem where Name = 'MMAS4Q3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MMAS4Q3','Sometimes if you feel worse when you take the medicine, do you stop taking it?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MMAS4Q3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MMAS4Q3' AND lm.Name = 'MMAS4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MMAS4' and lit.Name='MMAS4Q3'
		END
	END
GO
-----MMAS4Q3 Items
IF not Exists(select * from LookupMaster where Name = 'MMAS4Q3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('MMAS4Q3','MMAS4Q3','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'MMAS4Q3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MMAS4Q3' and lit.Name='GeneralYesNo'
		END
	END
GO


----MMAS4Q4
IF not exists(select * from LookupItem where Name = 'MMAS4Q4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MMAS4Q4','When you feel better do you sometimes stop taking your medicine?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MMAS4Q4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MMAS4Q4' AND lm.Name = 'MMAS4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MMAS4' and lit.Name='MMAS4Q4'
		END
	END
GO
-----MMAS4Q4 Items
IF not Exists(select * from LookupMaster where Name = 'MMAS4Q4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('MMAS4Q4','MMAS4Q4','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'MMAS4Q4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MMAS4Q4' and lit.Name='GeneralYesNo'
		END
	END
GO

--MMAS8
---MMAS8 Master
IF not Exists(select * from LookupMaster where Name = 'MMAS8')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('MMAS8','MMAS8','0')
GO
---MMAS8 Questions
----MMAS8Q1
IF not exists(select * from LookupItem where Name = 'MMAS8Q1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MMAS8Q1','Did you take the medicine yesterday?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MMAS8Q1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MMAS8Q1' AND lm.Name = 'MMAS8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MMAS8' and lit.Name='MMAS8Q1'
		END
	END
GO
-----MMAS8Q1 Items
IF not Exists(select * from LookupMaster where Name = 'MMAS8Q1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('MMAS8Q1','MMAS8Q1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'MMAS8Q1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MMAS8Q1' and lit.Name='GeneralYesNo'
		END
	END
GO

----MMAS8Q2
IF not exists(select * from LookupItem where Name = 'MMAS8Q2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MMAS8Q2','When you feel like your symptoms are under control, do you sometimes stop taking your medicine?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MMAS8Q2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MMAS8Q2' AND lm.Name = 'MMAS8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MMAS8' and lit.Name='MMAS8Q2'
		END
	END
GO
-----MMAS8Q2 Items
IF not Exists(select * from LookupMaster where Name = 'MMAS8Q2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('MMAS8Q2','MMAS8Q2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'MMAS8Q2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MMAS8Q2' and lit.Name='GeneralYesNo'
		END
	END
GO


----MMAS8Q3
IF not exists(select * from LookupItem where Name = 'MMAS8Q3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MMAS8Q3','Do you ever feel under pressure about sticking to your treatment plan?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MMAS8Q3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MMAS8Q3' AND lm.Name = 'MMAS8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MMAS8' and lit.Name='MMAS8Q3'
		END
	END
GO
-----MMAS8Q3 Items
IF not Exists(select * from LookupMaster where Name = 'MMAS8Q3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('MMAS8Q3','MMAS8Q3','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'MMAS8Q3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MMAS8Q3' and lit.Name='GeneralYesNo'
		END
	END
GO


----MMAS8Q4
IF not exists(select * from LookupItem where Name = 'MMAS8Q4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MMAS8Q4','How often do you have difficulty remembering to take all your medications?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MMAS8Q4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MMAS8Q4' AND lm.Name = 'MMAS8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MMAS8' and lit.Name='MMAS8Q4'
		END
	END
GO
-----MMAS8Q4 Items
IF not Exists(select * from LookupMaster where Name = 'MMAS8Q4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('MMAS8Q4','MMAS8Q4','0')
GO

IF not exists(select * from LookupItem where Name = 'RememberingFrequency')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('RememberingFrequency','RememberingFrequency','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'RememberingFrequency')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='RememberingFrequency' AND lm.Name = 'MMAS8Q4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MMAS8Q4' and lit.Name='RememberingFrequency'
		END
	END
GO

--Session2MMAS4
---Session2MMAS4 Master
IF not Exists(select * from LookupMaster where Name = 'Session2MMAS4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2MMAS4','Session2MMAS4','0')
GO
---Session2MMAS4 Questions
----Session2MMAS4Q1
IF not exists(select * from LookupItem where Name = 'Session2MMAS4Q1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2MMAS4Q1','Do you forget to take your medicine since the last visit?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2MMAS4Q1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2MMAS4Q1' AND lm.Name = 'Session2MMAS4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2MMAS4' and lit.Name='Session2MMAS4Q1'
		END
	END
GO
-----Session2MMAS4Q1 Items
IF not Exists(select * from LookupMaster where Name = 'Session2MMAS4Q1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2MMAS4Q1','Session2MMAS4Q1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session2MMAS4Q1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2MMAS4Q1' and lit.Name='GeneralYesNo'
		END
	END
GO

----Session2MMAS4Q2
IF not exists(select * from LookupItem where Name = 'Session2MMAS4Q2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2MMAS4Q2','Are you careless at times about taking your medicine?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2MMAS4Q2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2MMAS4Q2' AND lm.Name = 'Session2MMAS4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2MMAS4' and lit.Name='Session2MMAS4Q2'
		END
	END
GO
-----Session2MMAS4Q2 Items
IF not Exists(select * from LookupMaster where Name = 'Session2MMAS4Q2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2MMAS4Q2','Session2MMAS4Q2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session2MMAS4Q2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2MMAS4Q2' and lit.Name='GeneralYesNo'
		END
	END
GO


----Session2MMAS4Q3
IF not exists(select * from LookupItem where Name = 'Session2MMAS4Q3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2MMAS4Q3','Sometimes if you feel worse when you take the medicine, do you stop taking it?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2MMAS4Q3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2MMAS4Q3' AND lm.Name = 'Session2MMAS4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2MMAS4' and lit.Name='Session2MMAS4Q3'
		END
	END
GO
-----Session2MMAS4Q3 Items
IF not Exists(select * from LookupMaster where Name = 'Session2MMAS4Q3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2MMAS4Q3','Session2MMAS4Q3','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session2MMAS4Q3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2MMAS4Q3' and lit.Name='GeneralYesNo'
		END
	END
GO


----Session2MMAS4Q4
IF not exists(select * from LookupItem where Name = 'Session2MMAS4Q4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2MMAS4Q4','When you feel better do you sometimes stop taking your medicine?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2MMAS4Q4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2MMAS4Q4' AND lm.Name = 'Session2MMAS4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2MMAS4' and lit.Name='Session2MMAS4Q4'
		END
	END
GO
-----Session2MMAS4Q4 Items
IF not Exists(select * from LookupMaster where Name = 'Session2MMAS4Q4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2MMAS4Q4','Session2MMAS4Q4','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session2MMAS4Q4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2MMAS4Q4' and lit.Name='GeneralYesNo'
		END
	END
GO

--Session2MMAS8
---Session2MMAS8 Master
IF not Exists(select * from LookupMaster where Name = 'Session2MMAS8')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2MMAS8','Session2MMAS8','0')
GO
---Session2MMAS8 Questions
----Session2MMAS8Q1
IF not exists(select * from LookupItem where Name = 'Session2MMAS8Q1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2MMAS8Q1','Did you take the medicine yesterday?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2MMAS8Q1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2MMAS8Q1' AND lm.Name = 'Session2MMAS8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2MMAS8' and lit.Name='Session2MMAS8Q1'
		END
	END
GO
-----Session2MMAS8Q1 Items
IF not Exists(select * from LookupMaster where Name = 'Session2MMAS8Q1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2MMAS8Q1','Session2MMAS8Q1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session2MMAS8Q1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2MMAS8Q1' and lit.Name='GeneralYesNo'
		END
	END
GO

----Session2MMAS8Q2
IF not exists(select * from LookupItem where Name = 'Session2MMAS8Q2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2MMAS8Q2','When you feel like your symptoms are under control, do you sometimes stop taking your medicine?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2MMAS8Q2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2MMAS8Q2' AND lm.Name = 'Session2MMAS8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2MMAS8' and lit.Name='Session2MMAS8Q2'
		END
	END
GO
-----Session2MMAS8Q2 Items
IF not Exists(select * from LookupMaster where Name = 'Session2MMAS8Q2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2MMAS8Q2','Session2MMAS8Q2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session2MMAS8Q2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2MMAS8Q2' and lit.Name='GeneralYesNo'
		END
	END
GO


----Session2MMAS8Q3
IF not exists(select * from LookupItem where Name = 'Session2MMAS8Q3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2MMAS8Q3','Do you ever feel under pressure about sticking to your treatment plan?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2MMAS8Q3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2MMAS8Q3' AND lm.Name = 'Session2MMAS8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2MMAS8' and lit.Name='Session2MMAS8Q3'
		END
	END
GO
-----Session2MMAS8Q3 Items
IF not Exists(select * from LookupMaster where Name = 'Session2MMAS8Q3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2MMAS8Q3','Session2MMAS8Q3','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session2MMAS8Q3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2MMAS8Q3' and lit.Name='GeneralYesNo'
		END
	END
GO


----Session2MMAS8Q4
IF not exists(select * from LookupItem where Name = 'Session2MMAS8Q4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2MMAS8Q4','How often do you have difficulty remembering to take all your medications?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2MMAS8Q4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2MMAS8Q4' AND lm.Name = 'Session2MMAS8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2MMAS8' and lit.Name='Session2MMAS8Q4'
		END
	END
GO
-----Session2MMAS8Q4 Items
IF not Exists(select * from LookupMaster where Name = 'Session2MMAS8Q4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2MMAS8Q4','Session2MMAS8Q4','0')
GO

IF not exists(select * from LookupItem where Name = 'RememberingFrequency')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('RememberingFrequency','RememberingFrequency','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'RememberingFrequency')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='RememberingFrequency' AND lm.Name = 'Session2MMAS8Q4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2MMAS8Q4' and lit.Name='RememberingFrequency'
		END
	END
GO

--Session3MMAS4
---Session3MMAS4 Master
IF not Exists(select * from LookupMaster where Name = 'Session3MMAS4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3MMAS4','Session3MMAS4','0')
GO
---Session3MMAS4 Questions
----Session3MMAS4Q1
IF not exists(select * from LookupItem where Name = 'Session3MMAS4Q1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3MMAS4Q1','Do you forget to take your medicine since the last visit?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3MMAS4Q1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3MMAS4Q1' AND lm.Name = 'Session3MMAS4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3MMAS4' and lit.Name='Session3MMAS4Q1'
		END
	END
GO
-----Session3MMAS4Q1 Items
IF not Exists(select * from LookupMaster where Name = 'Session3MMAS4Q1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3MMAS4Q1','Session3MMAS4Q1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session3MMAS4Q1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3MMAS4Q1' and lit.Name='GeneralYesNo'
		END
	END
GO

----Session3MMAS4Q2
IF not exists(select * from LookupItem where Name = 'Session3MMAS4Q2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3MMAS4Q2','Are you careless at times about taking your medicine?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3MMAS4Q2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3MMAS4Q2' AND lm.Name = 'Session3MMAS4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3MMAS4' and lit.Name='Session3MMAS4Q2'
		END
	END
GO
-----Session3MMAS4Q2 Items
IF not Exists(select * from LookupMaster where Name = 'Session3MMAS4Q2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3MMAS4Q2','Session3MMAS4Q2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session3MMAS4Q2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3MMAS4Q2' and lit.Name='GeneralYesNo'
		END
	END
GO


----Session3MMAS4Q3
IF not exists(select * from LookupItem where Name = 'Session3MMAS4Q3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3MMAS4Q3','Sometimes if you feel worse when you take the medicine, do you stop taking it?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3MMAS4Q3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3MMAS4Q3' AND lm.Name = 'Session3MMAS4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3MMAS4' and lit.Name='Session3MMAS4Q3'
		END
	END
GO
-----Session3MMAS4Q3 Items
IF not Exists(select * from LookupMaster where Name = 'Session3MMAS4Q3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3MMAS4Q3','Session3MMAS4Q3','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session3MMAS4Q3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3MMAS4Q3' and lit.Name='GeneralYesNo'
		END
	END
GO


----Session3MMAS4Q4
IF not exists(select * from LookupItem where Name = 'Session3MMAS4Q4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3MMAS4Q4','When you feel better do you sometimes stop taking your medicine?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3MMAS4Q4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3MMAS4Q4' AND lm.Name = 'Session3MMAS4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3MMAS4' and lit.Name='Session3MMAS4Q4'
		END
	END
GO
-----Session3MMAS4Q4 Items
IF not Exists(select * from LookupMaster where Name = 'Session3MMAS4Q4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3MMAS4Q4','Session3MMAS4Q4','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session3MMAS4Q4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3MMAS4Q4' and lit.Name='GeneralYesNo'
		END
	END
GO

--Session3MMAS8
---Session3MMAS8 Master
IF not Exists(select * from LookupMaster where Name = 'Session3MMAS8')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3MMAS8','Session3MMAS8','0')
GO
---Session3MMAS8 Questions
----Session3MMAS8Q1
IF not exists(select * from LookupItem where Name = 'Session3MMAS8Q1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3MMAS8Q1','Did you take the medicine yesterday?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3MMAS8Q1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3MMAS8Q1' AND lm.Name = 'Session3MMAS8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3MMAS8' and lit.Name='Session3MMAS8Q1'
		END
	END
GO
-----Session3MMAS8Q1 Items
IF not Exists(select * from LookupMaster where Name = 'Session3MMAS8Q1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3MMAS8Q1','Session3MMAS8Q1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session3MMAS8Q1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3MMAS8Q1' and lit.Name='GeneralYesNo'
		END
	END
GO

----Session3MMAS8Q2
IF not exists(select * from LookupItem where Name = 'Session3MMAS8Q2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3MMAS8Q2','When you feel like your symptoms are under control, do you sometimes stop taking your medicine?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3MMAS8Q2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3MMAS8Q2' AND lm.Name = 'Session3MMAS8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3MMAS8' and lit.Name='Session3MMAS8Q2'
		END
	END
GO
-----Session3MMAS8Q2 Items
IF not Exists(select * from LookupMaster where Name = 'Session3MMAS8Q2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3MMAS8Q2','Session3MMAS8Q2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session3MMAS8Q2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3MMAS8Q2' and lit.Name='GeneralYesNo'
		END
	END
GO


----Session3MMAS8Q3
IF not exists(select * from LookupItem where Name = 'Session3MMAS8Q3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3MMAS8Q3','Do you ever feel under pressure about sticking to your treatment plan?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3MMAS8Q3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3MMAS8Q3' AND lm.Name = 'Session3MMAS8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3MMAS8' and lit.Name='Session3MMAS8Q3'
		END
	END
GO
-----Session3MMAS8Q3 Items
IF not Exists(select * from LookupMaster where Name = 'Session3MMAS8Q3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3MMAS8Q3','Session3MMAS8Q3','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session3MMAS8Q3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3MMAS8Q3' and lit.Name='GeneralYesNo'
		END
	END
GO


----Session3MMAS8Q4
IF not exists(select * from LookupItem where Name = 'Session3MMAS8Q4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3MMAS8Q4','How often do you have difficulty remembering to take all your medications?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3MMAS8Q4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3MMAS8Q4' AND lm.Name = 'Session3MMAS8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3MMAS8' and lit.Name='Session3MMAS8Q4'
		END
	END
GO
-----Session3MMAS8Q4 Items
IF not Exists(select * from LookupMaster where Name = 'Session3MMAS8Q4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3MMAS8Q4','Session3MMAS8Q4','0')
GO

IF not exists(select * from LookupItem where Name = 'RememberingFrequency')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('RememberingFrequency','RememberingFrequency','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'RememberingFrequency')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='RememberingFrequency' AND lm.Name = 'Session3MMAS8Q4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3MMAS8Q4' and lit.Name='RememberingFrequency'
		END
	END
GO

--Session4MMAS4
---Session4MMAS4 Master
IF not Exists(select * from LookupMaster where Name = 'Session4MMAS4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4MMAS4','Session4MMAS4','0')
GO
---Session4MMAS4 Questions
----Session4MMAS4Q1
IF not exists(select * from LookupItem where Name = 'Session4MMAS4Q1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4MMAS4Q1','Do you forget to take your medicine since the last visit?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4MMAS4Q1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4MMAS4Q1' AND lm.Name = 'Session4MMAS4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4MMAS4' and lit.Name='Session4MMAS4Q1'
		END
	END
GO
-----Session4MMAS4Q1 Items
IF not Exists(select * from LookupMaster where Name = 'Session4MMAS4Q1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4MMAS4Q1','Session4MMAS4Q1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session4MMAS4Q1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4MMAS4Q1' and lit.Name='GeneralYesNo'
		END
	END
GO

----Session4MMAS4Q2
IF not exists(select * from LookupItem where Name = 'Session4MMAS4Q2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4MMAS4Q2','Are you careless at times about taking your medicine?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4MMAS4Q2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4MMAS4Q2' AND lm.Name = 'Session4MMAS4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4MMAS4' and lit.Name='Session4MMAS4Q2'
		END
	END
GO
-----Session4MMAS4Q2 Items
IF not Exists(select * from LookupMaster where Name = 'Session4MMAS4Q2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4MMAS4Q2','Session4MMAS4Q2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session4MMAS4Q2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4MMAS4Q2' and lit.Name='GeneralYesNo'
		END
	END
GO


----Session4MMAS4Q3
IF not exists(select * from LookupItem where Name = 'Session4MMAS4Q3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4MMAS4Q3','Sometimes if you feel worse when you take the medicine, do you stop taking it?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4MMAS4Q3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4MMAS4Q3' AND lm.Name = 'Session4MMAS4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4MMAS4' and lit.Name='Session4MMAS4Q3'
		END
	END
GO
-----Session4MMAS4Q3 Items
IF not Exists(select * from LookupMaster where Name = 'Session4MMAS4Q3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4MMAS4Q3','Session4MMAS4Q3','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session4MMAS4Q3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4MMAS4Q3' and lit.Name='GeneralYesNo'
		END
	END
GO


----Session4MMAS4Q4
IF not exists(select * from LookupItem where Name = 'Session4MMAS4Q4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4MMAS4Q4','When you feel better do you sometimes stop taking your medicine?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4MMAS4Q4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4MMAS4Q4' AND lm.Name = 'Session4MMAS4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4MMAS4' and lit.Name='Session4MMAS4Q4'
		END
	END
GO
-----Session4MMAS4Q4 Items
IF not Exists(select * from LookupMaster where Name = 'Session4MMAS4Q4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4MMAS4Q4','Session4MMAS4Q4','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session4MMAS4Q4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4MMAS4Q4' and lit.Name='GeneralYesNo'
		END
	END
GO

--Session4MMAS8
---Session4MMAS8 Master
IF not Exists(select * from LookupMaster where Name = 'Session4MMAS8')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4MMAS8','Session4MMAS8','0')
GO
---Session4MMAS8 Questions
----Session4MMAS8Q1
IF not exists(select * from LookupItem where Name = 'Session4MMAS8Q1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4MMAS8Q1','Did you take the medicine yesterday?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4MMAS8Q1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4MMAS8Q1' AND lm.Name = 'Session4MMAS8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4MMAS8' and lit.Name='Session4MMAS8Q1'
		END
	END
GO
-----Session4MMAS8Q1 Items
IF not Exists(select * from LookupMaster where Name = 'Session4MMAS8Q1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4MMAS8Q1','Session4MMAS8Q1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session4MMAS8Q1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4MMAS8Q1' and lit.Name='GeneralYesNo'
		END
	END
GO

----Session4MMAS8Q2
IF not exists(select * from LookupItem where Name = 'Session4MMAS8Q2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4MMAS8Q2','When you feel like your symptoms are under control, do you sometimes stop taking your medicine?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4MMAS8Q2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4MMAS8Q2' AND lm.Name = 'Session4MMAS8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4MMAS8' and lit.Name='Session4MMAS8Q2'
		END
	END
GO
-----Session4MMAS8Q2 Items
IF not Exists(select * from LookupMaster where Name = 'Session4MMAS8Q2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4MMAS8Q2','Session4MMAS8Q2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session4MMAS8Q2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4MMAS8Q2' and lit.Name='GeneralYesNo'
		END
	END
GO


----Session4MMAS8Q3
IF not exists(select * from LookupItem where Name = 'Session4MMAS8Q3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4MMAS8Q3','Do you ever feel under pressure about sticking to your treatment plan?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4MMAS8Q3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4MMAS8Q3' AND lm.Name = 'Session4MMAS8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4MMAS8' and lit.Name='Session4MMAS8Q3'
		END
	END
GO
-----Session4MMAS8Q3 Items
IF not Exists(select * from LookupMaster where Name = 'Session4MMAS8Q3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4MMAS8Q3','Session4MMAS8Q3','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session4MMAS8Q3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4MMAS8Q3' and lit.Name='GeneralYesNo'
		END
	END
GO


----Session4MMAS8Q4
IF not exists(select * from LookupItem where Name = 'Session4MMAS8Q4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4MMAS8Q4','How often do you have difficulty remembering to take all your medications?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4MMAS8Q4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4MMAS8Q4' AND lm.Name = 'Session4MMAS8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4MMAS8' and lit.Name='Session4MMAS8Q4'
		END
	END
GO
-----Session4MMAS8Q4 Items
IF not Exists(select * from LookupMaster where Name = 'Session4MMAS8Q4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4MMAS8Q4','Session4MMAS8Q4','0')
GO

IF not exists(select * from LookupItem where Name = 'RememberingFrequency')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('RememberingFrequency','RememberingFrequency','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'RememberingFrequency')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='RememberingFrequency' AND lm.Name = 'Session4MMAS8Q4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4MMAS8Q4' and lit.Name='RememberingFrequency'
		END
	END
GO

--Understanding Viral Load
IF not Exists(select * from LookupMaster where Name = 'UnderstandingViralLoad')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('UnderstandingViralLoad','UnderstandingViralLoad','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'UVLQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('UVLQ1','How does the patient feel concerning the result?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'UVLQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='UVLQ1' AND lm.Name = 'UnderstandingViralLoad')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='UnderstandingViralLoad' and lit.Name='UVLQ1'
		END
	END
GO
--Q2
IF not exists(select * from LookupItem where Name = 'UVLQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('UVLQ2','What does the patient think caused the high viral load?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'UVLQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='UVLQ2' AND lm.Name = 'UnderstandingViralLoad')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='UnderstandingViralLoad' and lit.Name='UVLQ2'
		END
	END
GO

--Cognitive Barriers
IF not Exists(select * from LookupMaster where Name = 'CognitiveBarriers')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('CognitiveBarriers','CognitiveBarriers','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'CognitiveBarriersQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('CognitiveBarriersQ1','(HIV and ART Knowledge) Assess patient''s knowledge about HIV and ART; correct any misconceptions','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'CognitiveBarriersQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='CognitiveBarriersQ1' AND lm.Name = 'CognitiveBarriers')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='CognitiveBarriers' and lit.Name='CognitiveBarriersQ1'
		END
	END
GO



--Behavioural Barriers
IF not Exists(select * from LookupMaster where Name = 'BehaviouralBarriers')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('BehaviouralBarriers','BehaviouralBarriers','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'BehaviouralBarriersQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('BehaviouralBarriersQ1','Let the patient explan how they take their drugs, and at what time and how they store them','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'BehaviouralBarriersQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='BehaviouralBarriersQ1' AND lm.Name = 'BehaviouralBarriers')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BehaviouralBarriers' and lit.Name='BehaviouralBarriersQ1'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'BehaviouralBarriersQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('BehaviouralBarriersQ2','How does treatment fit the patient daily routines? What reminder tools are used?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'BehaviouralBarriersQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='BehaviouralBarriersQ2' AND lm.Name = 'BehaviouralBarriers')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BehaviouralBarriers' and lit.Name='BehaviouralBarriersQ2'
		END
	END
GO

--Q3
IF not exists(select * from LookupItem where Name = 'BehaviouralBarriersQ3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('BehaviouralBarriersQ3','What does the patient do in case of visits and travels?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'BehaviouralBarriersQ3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='BehaviouralBarriersQ3' AND lm.Name = 'BehaviouralBarriers')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BehaviouralBarriers' and lit.Name='BehaviouralBarriersQ3'
		END
	END
GO

--Q4
IF not exists(select * from LookupItem where Name = 'BehaviouralBarriersQ4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('BehaviouralBarriersQ4','What does the patient do incase of Side Effects?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'BehaviouralBarriersQ4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='BehaviouralBarriersQ4' AND lm.Name = 'BehaviouralBarriers')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BehaviouralBarriers' and lit.Name='BehaviouralBarriersQ4'
		END
	END
GO

--Q5
IF not exists(select * from LookupItem where Name = 'BehaviouralBarriersQ5')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('BehaviouralBarriersQ5','What are the most difficult situations for the patient to take drugs?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'BehaviouralBarriersQ5')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='BehaviouralBarriersQ5' AND lm.Name = 'BehaviouralBarriers')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='BehaviouralBarriers' and lit.Name='BehaviouralBarriersQ5'
		END
	END
GO

--Emotional Barriers
IF not Exists(select * from LookupMaster where Name = 'EmotionalBarriers')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('EmotionalBarriers','EmotionalBarriers','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'EmotionalBarriersQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('EmotionalBarriersQ1','How does the patient feel about taking drugs everyday?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'EmotionalBarriersQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='EmotionalBarriersQ1' AND lm.Name = 'EmotionalBarriers')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EmotionalBarriers' and lit.Name='EmotionalBarriersQ1'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'EmotionalBarriersQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('EmotionalBarriersQ2','Motivation. What are the patient ambitions in life? What are the 3 most important things they still want to achieve?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'EmotionalBarriersQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='EmotionalBarriersQ2' AND lm.Name = 'EmotionalBarriers')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EmotionalBarriers' and lit.Name='EmotionalBarriersQ2'
		END
	END
GO


--SocioEconomic Barriers
IF not Exists(select * from LookupMaster where Name = 'SocioEconomicBarriers')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SocioEconomicBarriers','SocioEconomicBarriers','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'SocioEconomicBarriersQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SocioEconomicBarriersQ1','Does the patient have any people in their life who they can talk to about HIV status and ART?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SocioEconomicBarriersQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SocioEconomicBarriersQ1' AND lm.Name = 'SocioEconomicBarriers')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriers' and lit.Name='SocioEconomicBarriersQ1'
		END
	END
GO
--q1 items
IF not Exists(select * from LookupMaster where Name = 'SocioEconomicBarriersQ1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SocioEconomicBarriersQ1','SocioEconomicBarriersQ1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'SocioEconomicBarriersQ1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriersQ1' and lit.Name='GeneralYesNo'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'SocioEconomicBarriersQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SocioEconomicBarriersQ2','Discuss how the patient can enlist the support of their family, friends and/or co-workers, a treatment buddy, community or support group?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SocioEconomicBarriersQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SocioEconomicBarriersQ2' AND lm.Name = 'SocioEconomicBarriers')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriers' and lit.Name='SocioEconomicBarriersQ2'
		END
	END
GO
--q2 items
IF not Exists(select * from LookupMaster where Name = 'SocioEconomicBarriersQ2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SocioEconomicBarriersQ2','SocioEconomicBarriersQ2','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'SocioEconomicBarriersQ2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriersQ2' and lit.Name='Notes'
		END
	END
GO



--Q3
IF not exists(select * from LookupItem where Name = 'SocioEconomicBarriersQ3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SocioEconomicBarriersQ3','Review the patient''s and family''s sources of income and how well they cover their needs.','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SocioEconomicBarriersQ3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SocioEconomicBarriersQ3' AND lm.Name = 'SocioEconomicBarriers')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriers' and lit.Name='SocioEconomicBarriersQ3'
		END
	END
GO
---q3 items
IF not Exists(select * from LookupMaster where Name = 'SocioEconomicBarriersQ3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SocioEconomicBarriersQ3','SocioEconomicBarriersQ3','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'SocioEconomicBarriersQ3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriersQ3' and lit.Name='Notes'
		END
	END
GO

--Q4
IF not exists(select * from LookupItem where Name = 'SocioEconomicBarriersQ4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SocioEconomicBarriersQ4','Does the patient have any challenges getting the clinic on regular basis?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SocioEconomicBarriersQ4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SocioEconomicBarriersQ4' AND lm.Name = 'SocioEconomicBarriers')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriers' and lit.Name='SocioEconomicBarriersQ4'
		END
	END
GO
--q4 items
IF not Exists(select * from LookupMaster where Name = 'SocioEconomicBarriersQ4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SocioEconomicBarriersQ4','SocioEconomicBarriersQ4','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'SocioEconomicBarriersQ4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriersQ4' and lit.Name='GeneralYesNo'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'SocioEconomicBarriersQ4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriersQ4' and lit.Name='Notes'
		END
	END
GO

--Q5
IF not exists(select * from LookupItem where Name = 'SocioEconomicBarriersQ5')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SocioEconomicBarriersQ5','Is the patient worried about people finding out about their HIV status accidentally?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SocioEconomicBarriersQ5')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SocioEconomicBarriersQ5' AND lm.Name = 'SocioEconomicBarriers')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriers' and lit.Name='SocioEconomicBarriersQ5'
		END
	END
GO
--q5 items
IF not Exists(select * from LookupMaster where Name = 'SocioEconomicBarriersQ5')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SocioEconomicBarriersQ5','SocioEconomicBarriersQ5','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'SocioEconomicBarriersQ5')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriersQ5' and lit.Name='GeneralYesNo'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'SocioEconomicBarriersQ5')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriersQ5' and lit.Name='Notes'
		END
	END
GO


---Q6
IF not exists(select * from LookupItem where Name = 'SocioEconomicBarriersQ6')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SocioEconomicBarriersQ6','Does the patient feel like people treat them differently when they know their HIV status?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SocioEconomicBarriersQ6')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SocioEconomicBarriersQ6' AND lm.Name = 'SocioEconomicBarriers')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriers' and lit.Name='SocioEconomicBarriersQ6'
		END
	END
GO
--q6 items
IF not Exists(select * from LookupMaster where Name = 'SocioEconomicBarriersQ6')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SocioEconomicBarriersQ6','SocioEconomicBarriersQ6','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'SocioEconomicBarriersQ6')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriersQ6' and lit.Name='GeneralYesNo'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'SocioEconomicBarriersQ6')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriersQ6' and lit.Name='Notes'
		END
	END
GO

--Q7
IF not exists(select * from LookupItem where Name = 'SocioEconomicBarriersQ7')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SocioEconomicBarriersQ7','Does stigma making it difficult for them to take their medications on time, or for them to attend clinical appointments?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SocioEconomicBarriersQ7')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SocioEconomicBarriersQ7' AND lm.Name = 'SocioEconomicBarriers')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriers' and lit.Name='SocioEconomicBarriersQ7'
		END
	END
GO
--q7 items
IF not Exists(select * from LookupMaster where Name = 'SocioEconomicBarriersQ7')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SocioEconomicBarriersQ7','SocioEconomicBarriersQ7','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'SocioEconomicBarriersQ7')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriersQ7' and lit.Name='GeneralYesNo'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'SocioEconomicBarriersQ7')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriersQ7' and lit.Name='Notes'
		END
	END
GO

--Q8
IF not exists(select * from LookupItem where Name = 'SocioEconomicBarriersQ8')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SocioEconomicBarriersQ8','Find out if the patient has tried faith healing, or if they have ever stopped taking their medicine because of religious belief','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SocioEconomicBarriersQ8')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SocioEconomicBarriersQ8' AND lm.Name = 'SocioEconomicBarriers')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriers' and lit.Name='SocioEconomicBarriersQ8'
		END
	END
GO
--q8 items
IF not Exists(select * from LookupMaster where Name = 'SocioEconomicBarriersQ8')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SocioEconomicBarriersQ8','SocioEconomicBarriersQ8','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'SocioEconomicBarriersQ8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriersQ8' and lit.Name='GeneralYesNo'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'SocioEconomicBarriersQ8')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SocioEconomicBarriersQ8' and lit.Name='Notes'
		END
	END
GO


---Referrals and Networks
IF not Exists(select * from LookupMaster where Name = 'SessionReferralsNetworks')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SessionReferralsNetworks','SessionReferralsNetworks','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'SessionReferralsNetworksQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SessionReferralsNetworksQ1','Has the patient been referred to other services? (Nutrition, psychosocial support services, substance use treatment, etc)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SessionReferralsNetworksQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SessionReferralsNetworksQ1' AND lm.Name = 'SessionReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SessionReferralsNetworks' and lit.Name='SessionReferralsNetworksQ1'
		END
	END
GO
--q1 items
IF not Exists(select * from LookupMaster where Name = 'SessionReferralsNetworksQ1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SessionReferralsNetworksQ1','SessionReferralsNetworksQ1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'SessionReferralsNetworksQ1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SessionReferralsNetworksQ1' and lit.Name='GeneralYesNo'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'SessionReferralsNetworksQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SessionReferralsNetworksQ2','Did he/she attend the appointments?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SessionReferralsNetworksQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SessionReferralsNetworksQ2' AND lm.Name = 'SessionReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SessionReferralsNetworks' and lit.Name='SessionReferralsNetworksQ2'
		END
	END
GO
--q2 items
IF not Exists(select * from LookupMaster where Name = 'SessionReferralsNetworksQ2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SessionReferralsNetworksQ2','SessionReferralsNetworksQ2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'SessionReferralsNetworksQ2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SessionReferralsNetworksQ2' and lit.Name='GeneralYesNo'
		END
	END
GO


--Q3
IF not exists(select * from LookupItem where Name = 'SessionReferralsNetworksQ3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SessionReferralsNetworksQ3','What was the experience? Do the referrals need to be re-organized?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SessionReferralsNetworksQ3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SessionReferralsNetworksQ3' AND lm.Name = 'SessionReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SessionReferralsNetworks' and lit.Name='SessionReferralsNetworksQ3'
		END
	END
GO
--q3 items
IF not Exists(select * from LookupMaster where Name = 'SessionReferralsNetworksQ3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SessionReferralsNetworksQ3','SessionReferralsNetworksQ3','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'SessionReferralsNetworksQ3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SessionReferralsNetworksQ3' and lit.Name='Notes'
		END
	END
GO



--Q4
IF not exists(select * from LookupItem where Name = 'SessionReferralsNetworksQ4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SessionReferralsNetworksQ4','Will the patient benefit from a home visit?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SessionReferralsNetworksQ4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SessionReferralsNetworksQ4' AND lm.Name = 'SessionReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SessionReferralsNetworks' and lit.Name='SessionReferralsNetworksQ4'
		END
	END
GO
--q4 items
IF not Exists(select * from LookupMaster where Name = 'SessionReferralsNetworksQ4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SessionReferralsNetworksQ4','SessionReferralsNetworksQ4','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'SessionReferralsNetworksQ4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SessionReferralsNetworksQ4' and lit.Name='GeneralYesNo'
		END
	END
GO


--Q5
IF not exists(select * from LookupItem where Name = 'SessionReferralsNetworksQ5')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('SessionReferralsNetworksQ5','Adherence plan','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'SessionReferralsNetworksQ5')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='SessionReferralsNetworksQ5' AND lm.Name = 'SessionReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SessionReferralsNetworks' and lit.Name='SessionReferralsNetworksQ5'
		END
	END
GO
--q5 items
IF not Exists(select * from LookupMaster where Name = 'SessionReferralsNetworksQ5')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('SessionReferralsNetworksQ5','SessionReferralsNetworksQ5','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'SessionReferralsNetworksQ5')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='SessionReferralsNetworksQ5' and lit.Name='Notes'
		END
	END
GO

---Referrals and Networks
IF not Exists(select * from LookupMaster where Name = 'Session2ReferralsNetworks')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2ReferralsNetworks','Session2ReferralsNetworks','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'Session2ReferralsNetworksQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2ReferralsNetworksQ1','Has the patient been referred to other services? (Nutrition, psychosocial support services, substance use treatment, etc)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2ReferralsNetworksQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2ReferralsNetworksQ1' AND lm.Name = 'Session2ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2ReferralsNetworks' and lit.Name='Session2ReferralsNetworksQ1'
		END
	END
GO
--q1 items
IF not Exists(select * from LookupMaster where Name = 'Session2ReferralsNetworksQ1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2ReferralsNetworksQ1','Session2ReferralsNetworksQ1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session2ReferralsNetworksQ1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2ReferralsNetworksQ1' and lit.Name='GeneralYesNo'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'Session2ReferralsNetworksQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2ReferralsNetworksQ2','Did he/she attend the appointments?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2ReferralsNetworksQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2ReferralsNetworksQ2' AND lm.Name = 'Session2ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2ReferralsNetworks' and lit.Name='Session2ReferralsNetworksQ2'
		END
	END
GO
--q2 items
IF not Exists(select * from LookupMaster where Name = 'Session2ReferralsNetworksQ2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2ReferralsNetworksQ2','Session2ReferralsNetworksQ2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session2ReferralsNetworksQ2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2ReferralsNetworksQ2' and lit.Name='GeneralYesNo'
		END
	END
GO


--Q3
IF not exists(select * from LookupItem where Name = 'Session2ReferralsNetworksQ3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2ReferralsNetworksQ3','What was the experience? Do the referrals need to be re-organized?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2ReferralsNetworksQ3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2ReferralsNetworksQ3' AND lm.Name = 'Session2ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2ReferralsNetworks' and lit.Name='Session2ReferralsNetworksQ3'
		END
	END
GO
--q3 items
IF not Exists(select * from LookupMaster where Name = 'Session2ReferralsNetworksQ3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2ReferralsNetworksQ3','Session2ReferralsNetworksQ3','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'Session2ReferralsNetworksQ3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2ReferralsNetworksQ3' and lit.Name='Notes'
		END
	END
GO



--Q4
IF not exists(select * from LookupItem where Name = 'Session2ReferralsNetworksQ4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2ReferralsNetworksQ4','Will the patient benefit from a home visit?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2ReferralsNetworksQ4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2ReferralsNetworksQ4' AND lm.Name = 'Session2ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2ReferralsNetworks' and lit.Name='Session2ReferralsNetworksQ4'
		END
	END
GO
--q4 items
IF not Exists(select * from LookupMaster where Name = 'Session2ReferralsNetworksQ4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2ReferralsNetworksQ4','Session2ReferralsNetworksQ4','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session2ReferralsNetworksQ4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2ReferralsNetworksQ4' and lit.Name='GeneralYesNo'
		END
	END
GO


--Q5
IF not exists(select * from LookupItem where Name = 'Session2ReferralsNetworksQ5')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2ReferralsNetworksQ5','Adherence plan','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2ReferralsNetworksQ5')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2ReferralsNetworksQ5' AND lm.Name = 'Session2ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2ReferralsNetworks' and lit.Name='Session2ReferralsNetworksQ5'
		END
	END
GO
--q5 items
IF not Exists(select * from LookupMaster where Name = 'Session2ReferralsNetworksQ5')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2ReferralsNetworksQ5','Session2ReferralsNetworksQ5','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'Session2ReferralsNetworksQ5')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2ReferralsNetworksQ5' and lit.Name='Notes'
		END
	END
GO

---Referrals and Networks
IF not Exists(select * from LookupMaster where Name = 'Session3ReferralsNetworks')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3ReferralsNetworks','Session3ReferralsNetworks','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'Session3ReferralsNetworksQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3ReferralsNetworksQ1','Has the patient been referred to other services? (Nutrition, psychosocial support services, substance use treatment, etc)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3ReferralsNetworksQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3ReferralsNetworksQ1' AND lm.Name = 'Session3ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3ReferralsNetworks' and lit.Name='Session3ReferralsNetworksQ1'
		END
	END
GO
--q1 items
IF not Exists(select * from LookupMaster where Name = 'Session3ReferralsNetworksQ1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3ReferralsNetworksQ1','Session3ReferralsNetworksQ1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session3ReferralsNetworksQ1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3ReferralsNetworksQ1' and lit.Name='GeneralYesNo'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'Session3ReferralsNetworksQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3ReferralsNetworksQ2','Did he/she attend the appointments?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3ReferralsNetworksQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3ReferralsNetworksQ2' AND lm.Name = 'Session3ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3ReferralsNetworks' and lit.Name='Session3ReferralsNetworksQ2'
		END
	END
GO
--q2 items
IF not Exists(select * from LookupMaster where Name = 'Session3ReferralsNetworksQ2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3ReferralsNetworksQ2','Session3ReferralsNetworksQ2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session3ReferralsNetworksQ2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3ReferralsNetworksQ2' and lit.Name='GeneralYesNo'
		END
	END
GO


--Q3
IF not exists(select * from LookupItem where Name = 'Session3ReferralsNetworksQ3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3ReferralsNetworksQ3','What was the experience? Do the referrals need to be re-organized?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3ReferralsNetworksQ3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3ReferralsNetworksQ3' AND lm.Name = 'Session3ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3ReferralsNetworks' and lit.Name='Session3ReferralsNetworksQ3'
		END
	END
GO
--q3 items
IF not Exists(select * from LookupMaster where Name = 'Session3ReferralsNetworksQ3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3ReferralsNetworksQ3','Session3ReferralsNetworksQ3','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'Session3ReferralsNetworksQ3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3ReferralsNetworksQ3' and lit.Name='Notes'
		END
	END
GO



--Q4
IF not exists(select * from LookupItem where Name = 'Session3ReferralsNetworksQ4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3ReferralsNetworksQ4','Will the patient benefit from a home visit?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3ReferralsNetworksQ4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3ReferralsNetworksQ4' AND lm.Name = 'Session3ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3ReferralsNetworks' and lit.Name='Session3ReferralsNetworksQ4'
		END
	END
GO
--q4 items
IF not Exists(select * from LookupMaster where Name = 'Session3ReferralsNetworksQ4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3ReferralsNetworksQ4','Session3ReferralsNetworksQ4','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session3ReferralsNetworksQ4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3ReferralsNetworksQ4' and lit.Name='GeneralYesNo'
		END
	END
GO


--Q5
IF not exists(select * from LookupItem where Name = 'Session3ReferralsNetworksQ5')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3ReferralsNetworksQ5','Adherence plan','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3ReferralsNetworksQ5')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3ReferralsNetworksQ5' AND lm.Name = 'Session3ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3ReferralsNetworks' and lit.Name='Session3ReferralsNetworksQ5'
		END
	END
GO
--q5 items
IF not Exists(select * from LookupMaster where Name = 'Session3ReferralsNetworksQ5')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3ReferralsNetworksQ5','Session3ReferralsNetworksQ5','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'Session3ReferralsNetworksQ5')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3ReferralsNetworksQ5' and lit.Name='Notes'
		END
	END
GO


---Referrals and Networks
IF not Exists(select * from LookupMaster where Name = 'Session4ReferralsNetworks')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4ReferralsNetworks','Session4ReferralsNetworks','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'Session4ReferralsNetworksQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4ReferralsNetworksQ1','Has the patient been referred to other services? (Nutrition, psychosocial support services, substance use treatment, etc)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4ReferralsNetworksQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4ReferralsNetworksQ1' AND lm.Name = 'Session4ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4ReferralsNetworks' and lit.Name='Session4ReferralsNetworksQ1'
		END
	END
GO
--q1 items
IF not Exists(select * from LookupMaster where Name = 'Session4ReferralsNetworksQ1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4ReferralsNetworksQ1','Session4ReferralsNetworksQ1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session4ReferralsNetworksQ1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4ReferralsNetworksQ1' and lit.Name='GeneralYesNo'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'Session4ReferralsNetworksQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4ReferralsNetworksQ2','Did he/she attend the appointments?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4ReferralsNetworksQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4ReferralsNetworksQ2' AND lm.Name = 'Session4ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4ReferralsNetworks' and lit.Name='Session4ReferralsNetworksQ2'
		END
	END
GO
--q2 items
IF not Exists(select * from LookupMaster where Name = 'Session4ReferralsNetworksQ2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4ReferralsNetworksQ2','Session4ReferralsNetworksQ2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session4ReferralsNetworksQ2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4ReferralsNetworksQ2' and lit.Name='GeneralYesNo'
		END
	END
GO


--Q3
IF not exists(select * from LookupItem where Name = 'Session4ReferralsNetworksQ3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4ReferralsNetworksQ3','What was the experience? Do the referrals need to be re-organized?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4ReferralsNetworksQ3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4ReferralsNetworksQ3' AND lm.Name = 'Session4ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4ReferralsNetworks' and lit.Name='Session4ReferralsNetworksQ3'
		END
	END
GO
--q3 items
IF not Exists(select * from LookupMaster where Name = 'Session4ReferralsNetworksQ3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4ReferralsNetworksQ3','Session4ReferralsNetworksQ3','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'Session4ReferralsNetworksQ3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4ReferralsNetworksQ3' and lit.Name='Notes'
		END
	END
GO



--Q4
IF not exists(select * from LookupItem where Name = 'Session4ReferralsNetworksQ4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4ReferralsNetworksQ4','Will the patient benefit from a home visit?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4ReferralsNetworksQ4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4ReferralsNetworksQ4' AND lm.Name = 'Session4ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4ReferralsNetworks' and lit.Name='Session4ReferralsNetworksQ4'
		END
	END
GO
--q4 items
IF not Exists(select * from LookupMaster where Name = 'Session4ReferralsNetworksQ4')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4ReferralsNetworksQ4','Session4ReferralsNetworksQ4','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session4ReferralsNetworksQ4')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4ReferralsNetworksQ4' and lit.Name='GeneralYesNo'
		END
	END
GO


--Q5
IF not exists(select * from LookupItem where Name = 'Session4ReferralsNetworksQ5')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4ReferralsNetworksQ5','Adherence plan','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4ReferralsNetworksQ5')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4ReferralsNetworksQ5' AND lm.Name = 'Session4ReferralsNetworks')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4ReferralsNetworks' and lit.Name='Session4ReferralsNetworksQ5'
		END
	END
GO
--q5 items
IF not Exists(select * from LookupMaster where Name = 'Session4ReferralsNetworksQ5')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4ReferralsNetworksQ5','Session4ReferralsNetworksQ5','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'Session4ReferralsNetworksQ5')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4ReferralsNetworksQ5' and lit.Name='Notes'
		END
	END
GO

-- Follow up Date
IF not Exists(select * from LookupMaster where Name = 'FollowupDate')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('FollowupDate','FollowupDate','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'FollowupDateQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('FollowupDateQ1','Follow-up Date','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'FollowupDateQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='FollowupDateQ1' AND lm.Name = 'FollowupDate')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='FollowupDate' and lit.Name='FollowupDateQ1'
		END
	END
GO
--q1 items
IF not Exists(select * from LookupMaster where Name = 'FollowupDateQ1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('FollowupDateQ1','FollowupDateQ1','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'FollowupDateQ1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='FollowupDateQ1' and lit.Name='Notes'
		END
	END
GO

-- END SESSION VIRAL LOAD
IF not Exists(select * from LookupMaster where Name = 'EndSessionViralLoad')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('EndSessionViralLoad','EndSessionViralLoad','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'EndSessionViralLoadQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('EndSessionViralLoadQ1','Way forward (VL<1000 continue current regiment, VL>1000 Prep for regimen change)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'EndSessionViralLoadQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='EndSessionViralLoadQ1' AND lm.Name = 'EndSessionViralLoad')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EndSessionViralLoad' and lit.Name='EndSessionViralLoadQ1'
		END
	END
GO
--q1 items
IF not Exists(select * from LookupMaster where Name = 'EndSessionViralLoadQ1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('EndSessionViralLoadQ1','EndSessionViralLoadQ1','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'EndSessionViralLoadQ1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EndSessionViralLoadQ1' and lit.Name='Notes'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'EndSessionViralLoadQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('EndSessionViralLoadQ2','Check this box if all required sessions (1,2,3/4) have been completed','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'EndSessionViralLoadQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='EndSessionViralLoadQ2' AND lm.Name = 'EndSessionViralLoad')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EndSessionViralLoad' and lit.Name='EndSessionViralLoadQ2'
		END
	END
GO

IF not Exists(select * from LookupMaster where Name = 'EndSessionViralLoadQ2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('EndSessionViralLoadQ2','EndSessionViralLoadQ2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'EndSessionViralLoadQ2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='EndSessionViralLoadQ2' and lit.Name='GeneralYesNo'
		END
	END
GO


--Pill Adherence
IF not Exists(select * from LookupMaster where Name = 'PillAdherence')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('PillAdherence','PillAdherence','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'PillAdherenceQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PillAdherenceQ1','Adherence % (from pill count)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PillAdherenceQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PillAdherenceQ1' AND lm.Name = 'PillAdherence')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PillAdherence' and lit.Name='PillAdherenceQ1'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'PillAdherenceQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('PillAdherenceQ2','Date filled in','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'PillAdherenceQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='PillAdherenceQ2' AND lm.Name = 'PillAdherence')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='PillAdherence' and lit.Name='PillAdherenceQ2'
		END
	END
GO

--Session4Pill Adherence
IF not Exists(select * from LookupMaster where Name = 'Session4PillAdherence')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4PillAdherence','Session4PillAdherence','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'Session4PillAdherenceQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4PillAdherenceQ1','Adherence % (from Session4Pill count)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4PillAdherenceQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4PillAdherenceQ1' AND lm.Name = 'Session4PillAdherence')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4PillAdherence' and lit.Name='Session4PillAdherenceQ1'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'Session4PillAdherenceQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4PillAdherenceQ2','Date filled in','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4PillAdherenceQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4PillAdherenceQ2' AND lm.Name = 'Session4PillAdherence')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4PillAdherence' and lit.Name='Session4PillAdherenceQ2'
		END
	END
GO
--Session3Pill Adherence
IF not Exists(select * from LookupMaster where Name = 'Session3PillAdherence')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3PillAdherence','Session3PillAdherence','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'Session3PillAdherenceQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3PillAdherenceQ1','Adherence % (from Session3Pill count)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3PillAdherenceQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3PillAdherenceQ1' AND lm.Name = 'Session3PillAdherence')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3PillAdherence' and lit.Name='Session3PillAdherenceQ1'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'Session3PillAdherenceQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3PillAdherenceQ2','Date filled in','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3PillAdherenceQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3PillAdherenceQ2' AND lm.Name = 'Session3PillAdherence')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3PillAdherence' and lit.Name='Session3PillAdherenceQ2'
		END
	END
GO

--Session2Pill Adherence
IF not Exists(select * from LookupMaster where Name = 'Session2PillAdherence')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2PillAdherence','Session2PillAdherence','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'Session2PillAdherenceQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2PillAdherenceQ1','Adherence % (from Session2Pill count)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2PillAdherenceQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2PillAdherenceQ1' AND lm.Name = 'Session2PillAdherence')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2PillAdherence' and lit.Name='Session2PillAdherenceQ1'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'Session2PillAdherenceQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2PillAdherenceQ2','Date filled in','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2PillAdherenceQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2PillAdherenceQ2' AND lm.Name = 'Session2PillAdherence')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2PillAdherence' and lit.Name='Session2PillAdherenceQ2'
		END
	END
GO

--Session1Pill Adherence
IF not Exists(select * from LookupMaster where Name = 'Session1PillAdherence')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session1PillAdherence','Session1PillAdherence','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'Session1PillAdherenceQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session1PillAdherenceQ1','Adherence % (from Session1Pill count)','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session1PillAdherenceQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session1PillAdherenceQ1' AND lm.Name = 'Session1PillAdherence')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session1PillAdherence' and lit.Name='Session1PillAdherenceQ1'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'Session1PillAdherenceQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session1PillAdherenceQ2','Date filled in','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session1PillAdherenceQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session1PillAdherenceQ2' AND lm.Name = 'Session1PillAdherence')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session1PillAdherence' and lit.Name='Session1PillAdherenceQ2'
		END
	END
GO


---Adherence Review
IF not Exists(select * from LookupMaster where Name = 'AdherenceReviews')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('AdherenceReviews','AdherenceReviews','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'AdherenceReviewsQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('AdherenceReviewsQ1','Does patient think adherence has improved since last visit?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'AdherenceReviewsQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='AdherenceReviewsQ1' AND lm.Name = 'AdherenceReviews')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AdherenceReviews' and lit.Name='AdherenceReviewsQ1'
		END
	END
GO
--q1 items
IF not Exists(select * from LookupMaster where Name = 'AdherenceReviewsQ1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('AdherenceReviewsQ1','AdherenceReviewsQ1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'AdherenceReviewsQ1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AdherenceReviewsQ1' and lit.Name='GeneralYesNo'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'AdherenceReviewsQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('AdherenceReviewsQ2','Have any dosses been missed?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'AdherenceReviewsQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='AdherenceReviewsQ2' AND lm.Name = 'AdherenceReviews')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AdherenceReviews' and lit.Name='AdherenceReviewsQ2'
		END
	END
GO
--q2 items
IF not Exists(select * from LookupMaster where Name = 'AdherenceReviewsQ2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('AdherenceReviewsQ2','AdherenceReviewsQ2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'AdherenceReviewsQ2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AdherenceReviewsQ2' and lit.Name='GeneralYesNo'
		END
	END
GO


--Q3
IF not exists(select * from LookupItem where Name = 'AdherenceReviewsQ3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('AdherenceReviewsQ3','Review barriers to adherence from previous session and if strategies identified have been taken up identified have been taken up, identify other gaps and issue emerging','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'AdherenceReviewsQ3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='AdherenceReviewsQ3' AND lm.Name = 'AdherenceReviews')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AdherenceReviews' and lit.Name='AdherenceReviewsQ3'
		END
	END
GO
--q3 items
IF not Exists(select * from LookupMaster where Name = 'AdherenceReviewQ3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('AdherenceReviewQ3','AdherenceReviewQ3','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'AdherenceReviewQ3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='AdherenceReviewQ3' and lit.Name='Notes'
		END
	END
GO


---Session2Adherence Review
IF not Exists(select * from LookupMaster where Name = 'Session2AdherenceReviews')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2AdherenceReviews','Session2AdherenceReviews','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'Session2AdherenceReviewsQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2AdherenceReviewsQ1','Does patient think Session2Adherence has improved since last visit?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2AdherenceReviewsQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2AdherenceReviewsQ1' AND lm.Name = 'Session2AdherenceReviews')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2AdherenceReviews' and lit.Name='Session2AdherenceReviewsQ1'
		END
	END
GO
--q1 items
IF not Exists(select * from LookupMaster where Name = 'Session2AdherenceReviewsQ1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2AdherenceReviewsQ1','Session2AdherenceReviewsQ1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session2AdherenceReviewsQ1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2AdherenceReviewsQ1' and lit.Name='GeneralYesNo'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'Session2AdherenceReviewsQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2AdherenceReviewsQ2','Have any dosses been missed?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2AdherenceReviewsQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2AdherenceReviewsQ2' AND lm.Name = 'Session2AdherenceReviews')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2AdherenceReviews' and lit.Name='Session2AdherenceReviewsQ2'
		END
	END
GO
--q2 items
IF not Exists(select * from LookupMaster where Name = 'Session2AdherenceReviewsQ2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2AdherenceReviewsQ2','Session2AdherenceReviewsQ2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session2AdherenceReviewsQ2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2AdherenceReviewsQ2' and lit.Name='GeneralYesNo'
		END
	END
GO


--Q3
IF not exists(select * from LookupItem where Name = 'Session2AdherenceReviewsQ3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2AdherenceReviewsQ3','Review barriers to Session2Adherence from previous session and if strategies identified have been taken up identified have been taken up, identify other gaps and issue emerging','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2AdherenceReviewsQ3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2AdherenceReviewsQ3' AND lm.Name = 'Session2AdherenceReviews')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2AdherenceReviews' and lit.Name='Session2AdherenceReviewsQ3'
		END
	END
GO
--q3 items
IF not Exists(select * from LookupMaster where Name = 'Session2AdherenceReviewsQ3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2AdherenceReviewsQ3','Session2AdherenceReviewsQ3','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'Session2AdherenceReviewsQ3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2AdherenceReviewsQ3' and lit.Name='Notes'
		END
	END
GO


---Session3Adherence Review
IF not Exists(select * from LookupMaster where Name = 'Session3AdherenceReviews')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3AdherenceReviews','Session3AdherenceReviews','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'Session3AdherenceReviewsQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3AdherenceReviewsQ1','Does patient think Session3Adherence has improved since last visit?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3AdherenceReviewsQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3AdherenceReviewsQ1' AND lm.Name = 'Session3AdherenceReviews')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3AdherenceReviews' and lit.Name='Session3AdherenceReviewsQ1'
		END
	END
GO
--q1 items
IF not Exists(select * from LookupMaster where Name = 'Session3AdherenceReviewsQ1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3AdherenceReviewsQ1','Session3AdherenceReviewsQ1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session3AdherenceReviewsQ1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3AdherenceReviewsQ1' and lit.Name='GeneralYesNo'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'Session3AdherenceReviewsQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3AdherenceReviewsQ2','Have any dosses been missed?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3AdherenceReviewsQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3AdherenceReviewsQ2' AND lm.Name = 'Session3AdherenceReviews')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3AdherenceReviews' and lit.Name='Session3AdherenceReviewsQ2'
		END
	END
GO
--q2 items
IF not Exists(select * from LookupMaster where Name = 'Session3AdherenceReviewsQ2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3AdherenceReviewsQ2','Session3AdherenceReviewsQ2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session3AdherenceReviewsQ2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3AdherenceReviewsQ2' and lit.Name='GeneralYesNo'
		END
	END
GO


--Q3
IF not exists(select * from LookupItem where Name = 'Session3AdherenceReviewsQ3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3AdherenceReviewsQ3','Review barriers to Session3Adherence from previous session and if strategies identified have been taken up identified have been taken up, identify other gaps and issue emerging','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3AdherenceReviewsQ3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3AdherenceReviewsQ3' AND lm.Name = 'Session3AdherenceReviews')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3AdherenceReviews' and lit.Name='Session3AdherenceReviewsQ3'
		END
	END
GO
--q3 items
IF not Exists(select * from LookupMaster where Name = 'Session3AdherenceReviewsQ3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3AdherenceReviewsQ3','Session3AdherenceReviewsQ3','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'Session3AdherenceReviewsQ3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3AdherenceReviewsQ3' and lit.Name='Notes'
		END
	END
GO


---Session4Adherence Review
IF not Exists(select * from LookupMaster where Name = 'Session4AdherenceReviews')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4AdherenceReviews','Session4AdherenceReviews','0')
GO

--Q1
IF not exists(select * from LookupItem where Name = 'Session4AdherenceReviewsQ1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4AdherenceReviewsQ1','Does patient think Session4Adherence has improved since last visit?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4AdherenceReviewsQ1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4AdherenceReviewsQ1' AND lm.Name = 'Session4AdherenceReviews')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4AdherenceReviews' and lit.Name='Session4AdherenceReviewsQ1'
		END
	END
GO
--q1 items
IF not Exists(select * from LookupMaster where Name = 'Session4AdherenceReviewsQ1')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4AdherenceReviewsQ1','Session4AdherenceReviewsQ1','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session4AdherenceReviewsQ1')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4AdherenceReviewsQ1' and lit.Name='GeneralYesNo'
		END
	END
GO

--Q2
IF not exists(select * from LookupItem where Name = 'Session4AdherenceReviewsQ2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4AdherenceReviewsQ2','Have any dosses been missed?','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4AdherenceReviewsQ2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4AdherenceReviewsQ2' AND lm.Name = 'Session4AdherenceReviews')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4AdherenceReviews' and lit.Name='Session4AdherenceReviewsQ2'
		END
	END
GO
--q2 items
IF not Exists(select * from LookupMaster where Name = 'Session4AdherenceReviewsQ2')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4AdherenceReviewsQ2','Session4AdherenceReviewsQ2','0')
GO

IF not exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('GeneralYesNo','GeneralYesNo','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'GeneralYesNo')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='GeneralYesNo' AND lm.Name = 'Session4AdherenceReviewsQ2')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4AdherenceReviewsQ2' and lit.Name='GeneralYesNo'
		END
	END
GO


--Q3
IF not exists(select * from LookupItem where Name = 'Session4AdherenceReviewsQ3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4AdherenceReviewsQ3','Review barriers to Session4Adherence from previous session and if strategies identified have been taken up identified have been taken up, identify other gaps and issue emerging','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4AdherenceReviewsQ3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4AdherenceReviewsQ3' AND lm.Name = 'Session4AdherenceReviews')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4AdherenceReviews' and lit.Name='Session4AdherenceReviewsQ3'
		END
	END
GO
--q3 items
IF not Exists(select * from LookupMaster where Name = 'Session4AdherenceReviewsQ3')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4AdherenceReviewsQ3','Session4AdherenceReviewsQ3','0')
GO

IF not exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Notes','Notes','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Notes')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Notes' AND lm.Name = 'Session4AdherenceReviewsQ3')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4AdherenceReviewsQ3' and lit.Name='Notes'
		END
	END
GO



---MMAS4 Notes
IF not Exists(select * from LookupMaster where Name = 'mmas4screeningNotes')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('mmas4screeningNotes','mmas4screeningNotes','0')
GO

IF not exists(select * from LookupItem where Name = 'mmas4Score')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('mmas4Score','Score','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'mmas4Score')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='mmas4Score' AND lm.Name = 'mmas4screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='mmas4screeningNotes' and lit.Name='mmas4Score'
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'mmas4Adherence')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('mmas4Adherence','Adherence Rating','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'mmas4Adherence')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='mmas4Adherence' AND lm.Name = 'mmas4screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='mmas4screeningNotes' and lit.Name='mmas4Adherence'
		END
	END
GO

--MMAS8 Notes
IF not Exists(select * from LookupMaster where Name = 'mmas8screeningNotes')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('mmas8screeningNotes','mmas8screeningNotes','0')
GO

IF not exists(select * from LookupItem where Name = 'mmas8Score')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('mmas8Score','Score','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'mmas8Score')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='mmas8Score' AND lm.Name = 'mmas8screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='mmas8screeningNotes' and lit.Name='mmas8Score'
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'mmas8Adherence')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('mmas8Adherence','Adherence Rating','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'mmas8Adherence')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='mmas8Adherence' AND lm.Name = 'mmas8screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='mmas8screeningNotes' and lit.Name='mmas8Adherence'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'mmas8Recommendation')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('mmas8Recommendation','Recommendation Rating','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'mmas8Recommendation')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='mmas8Recommendation' AND lm.Name = 'mmas8screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='mmas8screeningNotes' and lit.Name='mmas8Recommendation'
		END
	END
GO

---Session4mmas4 Notes
IF not Exists(select * from LookupMaster where Name = 'Session4mmas4screeningNotes')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4mmas4screeningNotes','Session4mmas4screeningNotes','0')
GO

IF not exists(select * from LookupItem where Name = 'Session4mmas4Score')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4mmas4Score','Score','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4mmas4Score')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4mmas4Score' AND lm.Name = 'Session4mmas4screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4mmas4screeningNotes' and lit.Name='Session4mmas4Score'
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'Session4mmas4Adherence')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4mmas4Adherence','Adherence Rating','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4mmas4Adherence')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4mmas4Adherence' AND lm.Name = 'Session4mmas4screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4mmas4screeningNotes' and lit.Name='Session4mmas4Adherence'
		END
	END
GO

--Session4mmas8 Notes
IF not Exists(select * from LookupMaster where Name = 'Session4mmas8screeningNotes')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session4mmas8screeningNotes','Session4mmas8screeningNotes','0')
GO

IF not exists(select * from LookupItem where Name = 'Session4mmas8Score')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4mmas8Score','Score','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4mmas8Score')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4mmas8Score' AND lm.Name = 'Session4mmas8screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4mmas8screeningNotes' and lit.Name='Session4mmas8Score'
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'Session4mmas8Adherence')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4mmas8Adherence','Adherence Rating','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4mmas8Adherence')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4mmas8Adherence' AND lm.Name = 'Session4mmas8screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4mmas8screeningNotes' and lit.Name='Session4mmas8Adherence'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Session4mmas8Recommendation')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4mmas8Recommendation','Recommendation Rating','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4mmas8Recommendation')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4mmas8Recommendation' AND lm.Name = 'Session4mmas8screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4mmas8screeningNotes' and lit.Name='Session4mmas8Recommendation'
		END
	END
GO


---Session3mmas4 Notes
IF not Exists(select * from LookupMaster where Name = 'Session3mmas4screeningNotes')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3mmas4screeningNotes','Session3mmas4screeningNotes','0')
GO

IF not exists(select * from LookupItem where Name = 'Session3mmas4Score')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3mmas4Score','Score','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3mmas4Score')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3mmas4Score' AND lm.Name = 'Session3mmas4screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3mmas4screeningNotes' and lit.Name='Session3mmas4Score'
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'Session3mmas4Adherence')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3mmas4Adherence','Adherence Rating','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3mmas4Adherence')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3mmas4Adherence' AND lm.Name = 'Session3mmas4screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3mmas4screeningNotes' and lit.Name='Session3mmas4Adherence'
		END
	END
GO

--Session3mmas8 Notes
IF not Exists(select * from LookupMaster where Name = 'Session3mmas8screeningNotes')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session3mmas8screeningNotes','Session3mmas8screeningNotes','0')
GO

IF not exists(select * from LookupItem where Name = 'Session3mmas8Score')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3mmas8Score','Score','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3mmas8Score')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3mmas8Score' AND lm.Name = 'Session3mmas8screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3mmas8screeningNotes' and lit.Name='Session3mmas8Score'
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'Session3mmas8Adherence')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3mmas8Adherence','Adherence Rating','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3mmas8Adherence')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3mmas8Adherence' AND lm.Name = 'Session3mmas8screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3mmas8screeningNotes' and lit.Name='Session3mmas8Adherence'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Session3mmas8Recommendation')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3mmas8Recommendation','Recommendation Rating','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3mmas8Recommendation')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3mmas8Recommendation' AND lm.Name = 'Session3mmas8screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3mmas8screeningNotes' and lit.Name='Session3mmas8Recommendation'
		END
	END
GO


---Session2mmas4 Notes
IF not Exists(select * from LookupMaster where Name = 'Session2mmas4screeningNotes')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2mmas4screeningNotes','Session2mmas4screeningNotes','0')
GO

IF not exists(select * from LookupItem where Name = 'Session2mmas4Score')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2mmas4Score','Score','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2mmas4Score')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2mmas4Score' AND lm.Name = 'Session2mmas4screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2mmas4screeningNotes' and lit.Name='Session2mmas4Score'
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'Session2mmas4Adherence')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2mmas4Adherence','Adherence Rating','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2mmas4Adherence')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2mmas4Adherence' AND lm.Name = 'Session2mmas4screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2mmas4screeningNotes' and lit.Name='Session2mmas4Adherence'
		END
	END
GO

--Session2mmas8 Notes
IF not Exists(select * from LookupMaster where Name = 'Session2mmas8screeningNotes')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session2mmas8screeningNotes','Session2mmas8screeningNotes','0')
GO

IF not exists(select * from LookupItem where Name = 'Session2mmas8Score')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2mmas8Score','Score','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2mmas8Score')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2mmas8Score' AND lm.Name = 'Session2mmas8screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2mmas8screeningNotes' and lit.Name='Session2mmas8Score'
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'Session2mmas8Adherence')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2mmas8Adherence','Adherence Rating','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2mmas8Adherence')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2mmas8Adherence' AND lm.Name = 'Session2mmas8screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2mmas8screeningNotes' and lit.Name='Session2mmas8Adherence'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Session2mmas8Recommendation')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2mmas8Recommendation','Recommendation Rating','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2mmas8Recommendation')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2mmas8Recommendation' AND lm.Name = 'Session2mmas8screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2mmas8screeningNotes' and lit.Name='Session2mmas8Recommendation'
		END
	END
GO


---Session1mmas4 Notes
IF not Exists(select * from LookupMaster where Name = 'Session1mmas4screeningNotes')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session1mmas4screeningNotes','Session1mmas4screeningNotes','0')
GO

IF not exists(select * from LookupItem where Name = 'Session1mmas4Score')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session1mmas4Score','Score','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session1mmas4Score')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session1mmas4Score' AND lm.Name = 'Session1mmas4screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session1mmas4screeningNotes' and lit.Name='Session1mmas4Score'
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'Session1mmas4Adherence')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session1mmas4Adherence','Adherence Rating','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session1mmas4Adherence')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session1mmas4Adherence' AND lm.Name = 'Session1mmas4screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session1mmas4screeningNotes' and lit.Name='Session1mmas4Adherence'
		END
	END
GO

--Session1mmas8 Notes
IF not Exists(select * from LookupMaster where Name = 'Session1mmas8screeningNotes')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('Session1mmas8screeningNotes','Session1mmas8screeningNotes','0')
GO

IF not exists(select * from LookupItem where Name = 'Session1mmas8Score')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session1mmas8Score','Score','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session1mmas8Score')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session1mmas8Score' AND lm.Name = 'Session1mmas8screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session1mmas8screeningNotes' and lit.Name='Session1mmas8Score'
		END
	END
GO


IF not exists(select * from LookupItem where Name = 'Session1mmas8Adherence')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session1mmas8Adherence','Adherence Rating','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session1mmas8Adherence')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session1mmas8Adherence' AND lm.Name = 'Session1mmas8screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session1mmas8screeningNotes' and lit.Name='Session1mmas8Adherence'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'Session1mmas8Recommendation')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session1mmas8Recommendation','Recommendation Rating','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session1mmas8Recommendation')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session1mmas8Recommendation' AND lm.Name = 'Session1mmas8screeningNotes')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session1mmas8screeningNotes' and lit.Name='Session1mmas8Recommendation'
		END
	END
GO


--- MMAS RATING AND RECOMMENDATIONS
--Rating
IF not Exists(select * from LookupMaster where Name = 'MmasRating')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('MmasRating','MmasRating','0')
GO

IF not exists(select * from LookupItem where Name = 'MmasRating0')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRating0','Good','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRating0')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRating0' AND lm.Name = 'MmasRating')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'0.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRating' and lit.Name='MmasRating0'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRating1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRating1','Inadequate','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRating1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRating1' AND lm.Name = 'MmasRating')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRating' and lit.Name='MmasRating1'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRating2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRating2','Inadequate','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRating2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRating2' AND lm.Name = 'MmasRating')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRating' and lit.Name='MmasRating2'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRating3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRating3','Poor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRating3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRating3' AND lm.Name = 'MmasRating')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRating' and lit.Name='MmasRating3'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRating4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRating4','Poor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRating4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRating4' AND lm.Name = 'MmasRating')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRating' and lit.Name='MmasRating4'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRating5')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRating5','Poor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRating5')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRating5' AND lm.Name = 'MmasRating')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRating' and lit.Name='MmasRating5'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRating6')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRating6','Poor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRating6')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRating6' AND lm.Name = 'MmasRating')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRating' and lit.Name='MmasRating6'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRating7.00')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRating7.00','Poor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRating7.00')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRating7.00' AND lm.Name = 'MmasRating')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRating' and lit.Name='MmasRating7.00'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRating7.25')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRating7.25','Poor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRating7.25')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRating7.25' AND lm.Name = 'MmasRating')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.25' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRating' and lit.Name='MmasRating7.25'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRating7.50')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRating7.50','Poor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRating7.50')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRating7.50' AND lm.Name = 'MmasRating')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.50' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRating' and lit.Name='MmasRating7.50'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRating7.75')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRating7.75','Poor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRating7.75')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRating7.75' AND lm.Name = 'MmasRating')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.75' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRating' and lit.Name='MmasRating7.75'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRating8.00')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRating8.00','Poor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRating8.00')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRating8.00' AND lm.Name = 'MmasRating')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRating' and lit.Name='MmasRating8.00'
		END
	END
GO


--Recommendation
IF not Exists(select * from LookupMaster where Name = 'MmasRecommendation')
	insert into LookupMaster (Name,DisplayName,DeleteFlag)values('MmasRecommendation','MmasRecommendation','0')
GO

IF not exists(select * from LookupItem where Name = 'MmasRecommendation0')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRecommendation0','','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRecommendation0')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRecommendation0' AND lm.Name = 'MmasRecommendation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'0.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRecommendation' and lit.Name='MmasRecommendation0'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRecommendation1')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRecommendation1','','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRecommendation1')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRecommendation1' AND lm.Name = 'MmasRecommendation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRecommendation' and lit.Name='MmasRecommendation1'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRecommendation2')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRecommendation2','','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRecommendation2')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRecommendation2' AND lm.Name = 'MmasRecommendation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'2.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRecommendation' and lit.Name='MmasRecommendation2'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRecommendation3')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRecommendation3','Refer to Counselor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRecommendation3')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRecommendation3' AND lm.Name = 'MmasRecommendation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRecommendation' and lit.Name='MmasRecommendation3'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRecommendation4')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRecommendation4','Refer to Counselor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRecommendation4')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRecommendation4' AND lm.Name = 'MmasRecommendation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'4.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRecommendation' and lit.Name='MmasRecommendation4'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRecommendation5')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRecommendation5','Refer to Counselor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRecommendation5')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRecommendation5' AND lm.Name = 'MmasRecommendation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'5.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRecommendation' and lit.Name='MmasRecommendation5'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRecommendation6')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRecommendation6','Refer to Counselor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRecommendation6')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRecommendation6' AND lm.Name = 'MmasRecommendation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'6.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRecommendation' and lit.Name='MmasRecommendation6'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRecommendation7.00')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRecommendation7.00','Refer to Counselor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRecommendation7.00')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRecommendation7.00' AND lm.Name = 'MmasRecommendation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRecommendation' and lit.Name='MmasRecommendation7.00'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRecommendation7.25')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRecommendation7.25','Refer to Counselor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRecommendation7.25')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRecommendation7.25' AND lm.Name = 'MmasRecommendation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.25' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRecommendation' and lit.Name='MmasRecommendation7.25'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRecommendation7.50')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRecommendation7.50','Refer to Counselor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRecommendation7.50')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRecommendation7.50' AND lm.Name = 'MmasRecommendation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.50' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRecommendation' and lit.Name='MmasRecommendation7.50'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRecommendation7.75')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRecommendation7.75','Refer to Counselor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRecommendation7.75')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRecommendation7.75' AND lm.Name = 'MmasRecommendation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'7.75' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRecommendation' and lit.Name='MmasRecommendation7.75'
		END
	END
GO

IF not exists(select * from LookupItem where Name = 'MmasRecommendation8.00')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('MmasRecommendation8.00','Refer to Counselor','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'MmasRecommendation8.00')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='MmasRecommendation8.00' AND lm.Name = 'MmasRecommendation')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'8.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='MmasRecommendation' and lit.Name='MmasRecommendation8.00'
		END
	END
GO


-- Follow up date
---Session 1
IF not exists(select * from LookupItem where Name = 'Session1FollowupDate')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session1FollowupDate','Date filled in','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session1FollowupDate')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session1FollowupDate' AND lm.Name = 'Session1PillAdherence')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session1PillAdherence' and lit.Name='Session1FollowupDate'
		END
	END
GO
---Session 2
IF not exists(select * from LookupItem where Name = 'Session2FollowupDate')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session2FollowupDate','Date filled in','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session2FollowupDate')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session2FollowupDate' AND lm.Name = 'Session2PillAdherence')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session2PillAdherence' and lit.Name='Session2FollowupDate'
		END
	END
GO
---Session 3
IF not exists(select * from LookupItem where Name = 'Session3FollowupDate')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session3FollowupDate','Date filled in','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session3FollowupDate')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session3FollowupDate' AND lm.Name = 'Session3PillAdherence')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session3PillAdherence' and lit.Name='Session3FollowupDate'
		END
	END
GO
---Session 4
IF not exists(select * from LookupItem where Name = 'Session4FollowupDate')
	BEGIN
		insert into LookupItem(Name,DisplayName,DeleteFlag)values('Session4FollowupDate','Date filled in','0')
	END
GO

IF  exists(select * from LookupItem where Name = 'Session4FollowupDate')
	BEGIN
		if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
		lit.Name='Session4FollowupDate' AND lm.Name = 'Session4PillAdherence')
		BEGIN
			insert into LookupMasterItem select lm.Id,lit.Id,lit.DisplayName,'1.00' as OrdRank from LookupMaster lm,LookupItem lit where lm.Name='Session4PillAdherence' and lit.Name='Session4FollowupDate'
		END
	END
GO

---Disclosed To
