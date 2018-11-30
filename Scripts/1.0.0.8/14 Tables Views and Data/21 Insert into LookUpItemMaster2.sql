
IF NOT EXISTS(SELECT * FROM LookupMaster WHERE Name='ANCVisitType')
BEGIN
INSERT INTO LookupMaster(Name,DisplayName,DeleteFlag)VALUES('ANCVisitType','ANCVisitType',0)
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Follow Up ANC visit')
BEGIN 
INSERT INTO LookupItem(Name,DeleteFlag,DisplayName)VALUES('Follow Up ANC visit',0,'Follow Up ANC visit')
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Initial ANC Visit')
BEGIN
INSERT INTO LookupItem(Name,DeleteFlag,DisplayName)VALUES('Initial ANC Visit',0,'Initial ANC Visit')
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='ANCVisitType' AND ItemName='Follow Up ANC visit')
BEGIN
INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank)VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='ANCVisitType'),(SELECT top 1 Id FROM LookupItem WHERE Name='Follow Up ANC visit'),'Follow Up ANC visit',2)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='ANCVisitType' AND ItemName='Initial ANC Visit')
BEGIN
INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank)VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='ANCVisitType'),(SELECT top 1 Id FROM LookupItem WHERE Name='Initial ANC Visit'),'Initial ANC Visit',1)
END

-------CacxMethod
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='CaCxMethod')
BEGIN
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES('CaCxMethod','CaCxMethod',0)
END
-- master
	-- lookupitem
	If Not Exists(Select 1 From LookupItem where Name='Pap Smear') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Pap Smear','Pap Smear',0); End
	If Not Exists(Select 1 From LookupItem where Name='VIA') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('VIA','VIA',0); End
	If Not Exists(Select 1 From LookupItem where Name='VILI') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('VILI','VILI',0); End
	If Not Exists(Select 1 From LookupItem where Name='Not Done') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Not Done','Not Done',0); End

	-- LookupMasterItem
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='CacxMethod'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Pap Smear'),'Pap Smear',1)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='CacxMethod'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='VIA'),'VIA',2)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='CacxMethod'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='VILI'),'VILI',3)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='CacxMethod'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Not Done'),'Not Done',4);
---------CacxResult
-- master
If Not Exists(Select 1 From LookupMaster where Name='CacxResult') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('CacxResult','Cacx Result',0); End
	-- lookupitem
	If Not Exists(Select 1 From LookupItem where Name='Normal') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Normal','Normal',0); End
	If Not Exists(Select 1 From LookupItem where Name='Suspected') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Suspected','Suspected',0); End
	If Not Exists(Select 1 From LookupItem where Name='Confirmed') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Confirmed','Confirmed',0); End
	If Not Exists(Select 1 From LookupItem where Name='N/A') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('N/A','N/A',0); End


	-- LookupMasterItem
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='CacxResult'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Normal'),'Normal',1)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='CacxResult'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Suspected'),'Suspected',2)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='CacxResult'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Confirmed'),'Confirmed',3)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='CacxResult'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/A'),'N/A',4)
--------Preventive Service
-- master
If Not Exists(Select 1 From LookupMaster where Name='PreventiveService') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('PreventiveService','Preventive Service',0); End
	-- lookupitem
	If Not Exists(Select 1 From LookupItem where Name='TT1') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('TT1','Tetanus Toxoid 1',0); End
	If Not Exists(Select 1 From LookupItem where Name='TT2') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('TT2','Tetanus Toxoid 2',0); End
	If Not Exists(Select 1 From LookupItem where Name='TT3') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('TT3','Tetanus Toxoid 3',0); End
	If Not Exists(Select 1 From LookupItem where Name='TT4') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('TT4','Tetanus Toxoid 4',0); End
	If Not Exists(Select 1 From LookupItem where Name='TT5') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('TT5','Tetanus Toxoid 5',0); End
	If Not Exists(Select 1 From LookupItem where Name='IPTp1') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('IPTp1','Malaria Prophylaxis (IPTp1)',0); End
	If Not Exists(Select 1 From LookupItem where Name='IPTp2') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('IPTp2','Malaria Prophylaxis (IPTp2)',0); End
	If Not Exists(Select 1 From LookupItem where Name='IPTp3') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('IPTp3','Malaria Prophylaxis (IPTp3)',0); End
	If Not Exists(Select 1 From LookupItem where Name='Dewormed') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Dewormed','Dewormed',0); End
	If Not Exists(Select 1 From LookupItem where Name='Vitamins') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Vitamins','Vitamins',0); End
	If Not Exists(Select 1 From LookupItem where Name='Calcium') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Calcium','Calcium',0); End
	If Not Exists(Select 1 From LookupItem where Name='Iron') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Iron','Iron',0); End
	If Not Exists(Select 1 From LookupItem where Name='Folate') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Folate','Folate',0); End

	If Not Exists(Select 1 From LookupItem where Name='TreatedNet') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('TreatedNet','Insecticide Treated Net',0); End
	If Not Exists(Select 1 From LookupItem where Name='AntenatalExercise') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('AntenatalExercise','Antenatal Exercise',0); End

	-- LookupMasterItem
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PreventiveService'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='TT1'),'Tetanus Toxoid 1',1)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PreventiveService'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='TT2'),'Tetanus Toxoid 2',2)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PreventiveService'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='TT3'),'Tetanus Toxoid 3',3)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PreventiveService'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='TT4'),'Tetanus Toxoid 4',4)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PreventiveService'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='TT5'),'Tetanus Toxoid 5',5)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PreventiveService'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='IPTp1'),'Malaria Prophylaxis (IPTp1)',6)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PreventiveService'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='IPTp2'),'Malaria Prophylaxis (IPTp2)',7)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PreventiveService'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='IPTp3'),'Malaria Prophylaxis (IPTp3)',8)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PreventiveService'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Dewormed'),'Dewormed',9)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PreventiveService'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Vitamins'),'Vitamins',10)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PreventiveService'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Calcium'),'Calcium',11)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PreventiveService'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Iron'),'Iron',12)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PreventiveService'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Folate'),'Folate',13)

	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PreventiveService'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='TreatedNet'),'Insecticide Treated Net',14)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PreventiveService'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='AntenatalExercise'),'Antenatal Exercise',15)


------------Counselling Services
-- master
If Not Exists(Select 1 From LookupMaster where Name='Counselled On') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('Counselled On','Counselled On',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Birth Plans') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Birth Plans','Birth Plans',0); End
If Not Exists(Select 1 From LookupItem where Name='Danger Signs') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Danger Signs','Danger Signs',0); End
If Not Exists(Select 1 From LookupItem where Name='Family Planning') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Family Planning','Family Planning',0); End
If Not Exists(Select 1 From LookupItem where Name='HIV') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('HIV','HIV',0); End
If Not Exists(Select 1 From LookupItem where Name='Supplemental Feeding') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Supplemental Feeding','Supplemental Feeding',0); End
If Not Exists(Select 1 From LookupItem where Name='Breast Care') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Breast Care','Breast Care',0); End
If Not Exists(Select 1 From LookupItem where Name='Infant Feeding') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Infant Feeding','Infant Feeding',0); End
If Not Exists(Select 1 From LookupItem where Name='Insecticide Treated Nets') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Insecticide Treated Nets','Insecticide Treated Nets',0); End

-- LookupMasterItem
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Counselled On'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Birth Plans'),'Birth Plans',1)
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Counselled On'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Danger Signs'),'Danger Signs',2)
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Counselled On'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Family Planning'),'Family Planning',3)
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Counselled On'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='HIV'),'HIV',4)
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Counselled On'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Supplemental Feeding'),'Supplemental Feeding',5)
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Counselled On'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Breast Care'),'Breast Care',6)
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Counselled On'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Infant Feeding'),'Infant Feeding',7)
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Counselled On'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Insecticide Treated Nets'),'Insecticide Treated Nets',8)

------------On ARV before 1st ANC Visit
-- master
If Not Exists(Select 1 From LookupMaster where Name='ARV before 1st ANCVisit') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('ARV before 1st ANCVisit','ARV before 1st ANCVisit',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Yes') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Yes','Known Positive',0); End
If Not Exists(Select 1 From LookupItem where Name='No') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('No','No',0); End
If Not Exists(Select 1 From LookupItem where Name='N/A') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('N/A','Not Applicable',0); End

-- LookupMasterItem
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ARV before 1st ANCVisit'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Yes'),'Yes',1)
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ARV before 1st ANCVisit'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='No'),'No',2)
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ARV before 1st ANCVisit'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/A'),'NA',3)

------------Started HAART in ANC
-- master
If Not Exists(Select 1 From LookupMaster where Name='Started HAART at Service Point') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('Started HAART at Service Point','Started HAART at Service Point',0); End

-- LookupMasterItem
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Started HAART in ANC'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Yes'),'Yes',1)
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Started HAART in ANC'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='No'),'No',2)
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Started HAART in ANC'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/A'),'NA',3)

------------CTX in ANC
-- master
If Not Exists(Select 1 From LookupMaster where Name='CTX at Service Point') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('CTX at Service Point','CTX at Service Point',0); End
-- LookupMasterItem
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='CTX in ANC'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Yes'),'Yes',1)
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='CTX in ANC'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='No'),'No',2)
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='CTX in ANC'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/A'),'NA',3)

------------AZT for Baby
-- master
If Not Exists(Select 1 From LookupMaster where Name='AZT for Baby') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('AZT for Baby','AZT for Baby',0); End
-- LookupMasterItem
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='AZT for Baby'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Yes'),'Yes',1)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='AZT for Baby'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='No'),'No',2)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='AZT for Baby'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/A'),'NA',3)
------------NVP for Baby
-- master
If Not Exists(Select 1 From LookupMaster where Name='NVP for Baby') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('NVP for Baby','NVP for Baby',0); End
-- LookupMasterItem
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='NVP for Baby'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Yes'),'Yes',1)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='NVP for Baby'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='No'),'No',2)
	Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='NVP for Baby'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/A'),'NA',3)
-----TB Screening for PMTCT
-- lookupMaster
IF NOT EXISTS(SELECT * FROM lookupMaster WHERE Name='TBScreeningPMTCT')
BEGIN
	INSERT INTO LookupMaster(Name,DisplayName,DeleteFlag) VALUES('TBScreeningPMTCT','TBScreeningPMTCT',0)	
END

-- lookupItem
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='No TB')
BEGIN
  INSERT INTO LookupItem(Name,DisplayName,DeleteFlag) VALUES('No TB','Negative TB screen',0)
END


IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='INH')
BEGIN
  INSERT INTO LookupItem(Name,DisplayName,DeleteFlag) VALUES('INH','Client was screened negative & started INH',0)
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Not Done')
BEGIN
  INSERT INTO LookupItem(Name,DisplayName,DeleteFlag) VALUES('Not Done','Not Done',0)
END

-- lookupItemMaster
IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='TBScreeningPMTCT' AND ItemName='PrTB')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank)VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='TBScreeningPMTCT'),(SELECT top 1 Id FROM LookupItem WHERE Name='PrTB'),'PrTB: Presumed TB',1);
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='TBScreeningPMTCT' AND ItemName='No TB')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank)VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='TBScreeningPMTCT'),(SELECT top 1 Id FROM LookupItem WHERE Name='No TB'),'NoTB:Negative TB screen',2);
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='TBScreeningPMTCT' AND ItemName='INH')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank)VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='TBScreeningPMTCT'),(SELECT top 1 Id FROM LookupItem WHERE Name='INH'),'INH:Client was screened negative & started INH',3);

END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='TBScreeningPMTCT' AND ItemName='TBRx')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank)VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='TBScreeningPMTCT'),(SELECT top 1 Id FROM LookupItem WHERE Name='TBRx'),'TBRx: Client On TB treatment',4);

END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='TBScreeningPMTCT' AND ItemName='Not Done')
BEGIN
 INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank)VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='TBScreeningPMTCT'),(SELECT top 1 Id FROM LookupItem WHERE Name='Not Done'),'Not Done',5);
END
-----PMTCT Referrals
IF NOT EXISTS(SELECT * FROM LookupMaster WHERE Name='pmtctReferrals')
BEGIN 
  INSERT INTO LookupMaster(name,DisplayName,DeleteFlag) VALUES('pmtctReferrals','pmtctReferrals',0)
END

IF NOT EXISTS(SELECT * FROM LookupItem l WHERE Name ='Another Health Facility')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName,DeleteFlag) VALUES('Another Health Facility','Another Health Facility',0)
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Community Unit')
BEGIN
	INSERT INTO LookupItem(Name,DisplayName,DeleteFlag) VALUES('Community Unit','Community Unit',0)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='pmtctReferrals' AND ItemName='Another Health Facility')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='pmtctReferrals'),(SELECT top 1 Id FROM LookupItem WHERE Name='Another Health Facility'),'Another Health Facility',1)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='pmtctReferrals' AND ItemName='Community Unit')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='pmtctReferrals'),(SELECT top 1 Id FROM LookupItem WHERE Name='Community Unit'),'Community Unit',2)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='pmtctReferrals' AND ItemName='N/A')
BEGIN
  INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='pmtctReferrals'),(SELECT top 1 Id FROM LookupItem WHERE Name='N/A'),'N/A',3)
END

---Breast Exam

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Breast Exam')
BEGIN 
  INSERT INTO LookupItem(Name,DisplayName,DeleteFlag)VALUES('Breast Exam','Breast Exam',0)
END

---Treated Syphilis
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Treated Syphilis')
BEGIN 
  INSERT INTO LookupItem(Name,DisplayName,DeleteFlag)VALUES('Treated Syphilis','Treated Syphilis',0)
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='On ARV before 1st ANC Visit')
BEGIN
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES('On ARV before 1st ANC Visit','On ARV before 1st ANC Visit',0)
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Started HAART in Service Point')
BEGIN
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES('Started HAART in Service Point','Started HAART in Service Point',0)
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Cotrimoxazole')
BEGIN
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES('Cotrimoxazole','Cotrimoxazole',0)
END
IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='AZT for Baby')
BEGIN
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES('AZT for Baby','AZT for Baby',0)
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='NVP for Baby')
BEGIN
	INSERT INTO LookupItem (Name,DisplayName,DeleteFlag) VALUES('NVP for Baby','NVP for Baby',0)
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Known Positive')
BEGIN
INSERT INTO LookupItem(name,DisplayName,DeleteFlag) VALUES('Known Positive','Known Positive',0)
END

IF NOT EXISTS(SELECT * FROM LookupMaster WHERE Name='HIVFinalResultsPMTCT')
BEGIN
INSERT INTO LookupMaster(Name,DisplayName,DeleteFlag) VALUES('HIVFinalResultsPMTCT','HIVFinalResultsPMTCT',0)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='HIVFinalResultsPMTCT' AND ItemName='Positive')
BEGIN
INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='HIVFinalResultsPMTCT'),(SELECT top 1 Id FROM LookupItem WHERE Name='Positive'),'Positive',1)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='HIVFinalResultsPMTCT' AND ItemName='Negative')
BEGIN
INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='HIVFinalResultsPMTCT'),(SELECT top 1 Id FROM LookupItem WHERE Name='Negative'),'Negative',2)
END


IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='HIVFinalResultsPMTCT' AND ItemName='Known Positive')
BEGIN
INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='HIVFinalResultsPMTCT'),(SELECT top 1 Id FROM LookupItem WHERE Name='Known Positive'),'Known Positive',3)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='HIVFinalResultsPMTCT' AND ItemName='N/A')
BEGIN
INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT top 1 Id FROM LookupMaster WHERE Name='HIVFinalResultsPMTCT'),(SELECT top 1 Id FROM LookupItem WHERE Name='N/A'),'Positive',4)
END

IF NOT EXISTS(SELECT * FROM LookupMaster WHERE Name='PMTCTHIVTests')
BEGIN 
  INSERT INTO LookupMaster(Name,DisplayName,DeleteFlag)VALUES('PMTCTHIVTests','PMTCTHIVTests',0)
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='HIV Test-1')
BEGIN
	INSERT INTO LookupItem(name,DisplayName,DeleteFlag) VALUES('HIV Test-1','HIV Test-1',0)
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='HIV Test-2')
BEGIN
	INSERT INTO LookupItem(name,DisplayName,DeleteFlag) VALUES('HIV Test-2','HIV Test-2',0)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='PMTCTHIVTests' AND ItemName='HIV Test-1')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHIVTests'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='HIV Test-1'),'HIV Test-1',1)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='PMTCTHIVTests' AND ItemName='HIV Test-2')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHIVTests'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='HIV Test-2'),'HIV Test-2',2)
END

IF NOT EXISTS(SELECT * FROM LookupMaster WHERE Name='PMTCTHIVTestVisit')
BEGIN 
  INSERT INTO LookupMaster(Name,DisplayName,DeleteFlag)VALUES('PMTCTHIVTestVisit','PMTCTHIVTestVisit',0)
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Initial')
BEGIN
	INSERT INTO LookupItem(name,DisplayName,DeleteFlag) VALUES('Initial','Initial',0)
END

IF NOT EXISTS(SELECT * FROM LookupItem WHERE Name='Retest')
BEGIN
	INSERT INTO LookupItem(name,DisplayName,DeleteFlag) VALUES('Retest','Retest',0)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='PMTCTHIVTestVisit' AND ItemName='Initial')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHIVTestVisit'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Initial'),'Initial',1)
END

IF NOT EXISTS(SELECT * FROM LookupItemView WHERE MasterName='PMTCTHIVTestVisit' AND ItemName='Retest')
BEGIN
	INSERT INTO LookupMasterItem(LookupMasterId,LookupItemId,DisplayName,OrdRank) VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHIVTestVisit'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Retest'),'Retest',2)
END


