----PNCPostNatalExams
-- master
If Not Exists(Select 1 From LookupMaster where Name='Breast') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('Breast','Breast',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Cracked') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Cracked','Cracked',0); End
If Not Exists(Select 1 From LookupItem where Name='Engorged') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Engorged','Engorged',0); End
If Not Exists(Select 1 From LookupItem where Name='Mastitis') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Mastitis','Mastitis',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Breast') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Normal')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Breast'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Normal'),'Normal',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Breast') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Cracked')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Breast'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Cracked'),'Cracked',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Breast') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Engorged')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Breast'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Engorged'),'Engorged',3); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Breast') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Mastitis')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Breast'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Mastitis'),'Mastitis',4); end 


---Uterus
-- master
If Not Exists(Select 1 From LookupMaster where Name='Uterus') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('Uterus','Uterus',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Contracted') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Contracted','Contracted',0); End
If Not Exists(Select 1 From LookupItem where Name='Not Contracted') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Not Contracted','Not Contracted',0); End
If Not Exists(Select 1 From LookupItem where Name='Other (Specify)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Other (Specify)','Other (Specify)',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Uterus') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Contracted')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Uterus'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Contracted'),'Contracted',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Uterus') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Not Contracted')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Uterus'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Not Contracted'),'Not Contracted',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Uterus') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other (Specify)')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Uterus'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other (Specify)'),'Other (Specify)',3); end 

--Lochia
-- master
If Not Exists(Select 1 From LookupMaster where Name='Lochia') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('Lochia','Lochia',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Foul Smelling') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Foul Smelling','Foul Smelling',0); End
If Not Exists(Select 1 From LookupItem where Name='Excessive') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Excessive','Excessive',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Lochia') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Normal')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Lochia'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Normal'),'Normal',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Lochia') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Foul Smelling')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Lochia'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Foul Smelling'),'Foul Smelling',2); END 
IF NOT EXISTS(SELECT 1 FROM LookupMasterItem WHERE LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Lochia') AND LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Excessive')) BEGIN INSERT INTO LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Lochia'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Excessive'),'Excessive',3); END 


--Postpartum haemorrhage						
-- master
If Not Exists(Select 1 From LookupMaster where Name='PostpartumHaemorrhage') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('PostpartumHaemorrhage','PostpartumHaemorrhage',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Contracted') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Contracted','Contracted',0); End
If Not Exists(Select 1 From LookupItem where Name='Not Contracted') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Not Contracted','Not Contracted',0); End
If Not Exists(Select 1 From LookupItem where Name='Other (Specify)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Other (Specify)','Other (Specify)',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostpartumHaemorrhage') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Contracted')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostpartumHaemorrhage'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Contracted'),'Contracted',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostpartumHaemorrhage') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Not Contracted')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostpartumHaemorrhage'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Not Contracted'),'Not Contracted',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostpartumHaemorrhage') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other (Specify)')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostpartumHaemorrhage'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other (Specify)'),'Other (Specify)',3); end 

--Episiotomy						
-- master
If Not Exists(Select 1 From LookupMaster where Name='Episiotomy') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('Episiotomy','Episiotomy',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Repaired') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Repaired','Repaired',0); End
If Not Exists(Select 1 From LookupItem where Name='Gaping') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Gaping','Gaping',0); End
If Not Exists(Select 1 From LookupItem where Name='Infected') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Infected','Infected',0); End
If Not Exists(Select 1 From LookupItem where Name='Healed') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Healed','Healed',0); End
If Not Exists(Select 1 From LookupItem where Name='N/A') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('N/A','N/A',0); End


-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Episiotomy') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Repaired')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Episiotomy'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Repaired'),'Repaired',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Episiotomy') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Gaping')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Episiotomy'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Gaping'),'Gaping',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Episiotomy') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Infected')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Episiotomy'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Infected'),'Infected',3); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Episiotomy') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Healed')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Episiotomy'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Healed'),'Healed',4); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Episiotomy') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/A')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Episiotomy'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/A'),'N/A',5); end 

--C-Section Site						
-- master
If Not Exists(Select 1 From LookupMaster where Name='C-Section Site') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('C-Section Site','C-Section Site',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Repaired') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Repaired','Repaired',0); End
If Not Exists(Select 1 From LookupItem where Name='Gaping') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Gaping','Gaping',0); End
If Not Exists(Select 1 From LookupItem where Name='Infected') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Infected','Infected',0); End
If Not Exists(Select 1 From LookupItem where Name='Healed') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Healed','Healed',0); End
If Not Exists(Select 1 From LookupItem where Name='N/A') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('N/A','N/A',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='C-Section Site') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='normal')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='C-Section Site'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Normal'),'Normal',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='C-Section Site') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Bleeding')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='C-Section Site'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Bleeding'),'Bleedin',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='C-Section Site') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Infected')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='C-Section Site'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Infected'),'Infected',3); end 
IF Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='C-Section Site') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Gaping')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='C-Section Site'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Gaping'),'Gaping',4);end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='C-Section Site') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/A')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='C-Section Site'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/A'),'N/A',5); end 


--Fistula Screening									
-- master
If Not Exists(Select 1 From LookupMaster where Name='Fistula Screening') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('Fistula Screening','Fistula Screening',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Repaired') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Repaired','Repaired',0); End
If Not Exists(Select 1 From LookupItem where Name='Gaping') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Gaping','Gaping',0); End
If Not Exists(Select 1 From LookupItem where Name='Infected') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Infected','Infected',0); End
If Not Exists(Select 1 From LookupItem where Name='Healed') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Healed','Healed',0); End
If Not Exists(Select 1 From LookupItem where Name='N/A') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('N/A','N/A',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula Screening') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Repaired')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula Screening'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Repaired'),'Repaired',1);end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula Screening') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Gaping')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula Screening'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Gaping'),'Gaping',2);end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula Screening') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Infected')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula Screening'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Infected'),'Infected',3);end 
IF Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula Screening') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Healed')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula Screening'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Healed'),'Healed',4);end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula Screening') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/A')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula Screening'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/A'),'N/A',5);end 
