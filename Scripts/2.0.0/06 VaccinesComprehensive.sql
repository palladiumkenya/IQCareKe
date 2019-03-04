------------PNCPostpartum haemorrhage						
-- master
If Not Exists(Select 1 From LookupMaster where Name='PostPartumHaemorrhage') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('PostPartumHaemorrhage','PostPartumHaemorrhage',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Absent') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Absent','Absent',0); End
If Not Exists(Select 1 From LookupItem where Name='Present') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Present','Present',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostPartumHaemorrhage') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Absent')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostPartumHaemorrhage'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Absent'),'Absent',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostPartumHaemorrhage') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Present')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostPartumHaemorrhage'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Present'),'Present',2); end 
-- If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostPartumHaemorrhage') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostPartumHaemorrhage'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other'),'Other',3); end 


------------VaccinesComprehensive
-- master
If Not Exists(Select 1 From LookupMaster where Name='VaccinesComprehensive') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('VaccinesComprehensive','VaccinesComprehensive',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='BCG') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('BCG','BCG',0); End

If Not Exists(Select 1 From LookupItem where Name='OPV 0') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('OPV 0','OPV 0',0); End

If Not Exists(Select 1 From LookupItem where Name='OPV 1') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('OPV 1','OPV 1',0); End
If Not Exists(Select 1 From LookupItem where Name='OPV 2') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('OPV 2','OPV 2',0); End
If Not Exists(Select 1 From LookupItem where Name='OPV 3') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('OPV 3','OPV 3',0); End

If Not Exists(Select 1 From LookupItem where Name='Pentavalent 1') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Pentavalent 1','Pentavalent 1',0); End
If Not Exists(Select 1 From LookupItem where Name='Pentavalent 2') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Pentavalent 2','Pentavalent 2',0); End
If Not Exists(Select 1 From LookupItem where Name='Pentavalent 3') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Pentavalent 3','Pentavalent 3',0); End

If Not Exists(Select 1 From LookupItem where Name='PCV-10 1') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('PCV-10 1','PCV-10 1',0); End
If Not Exists(Select 1 From LookupItem where Name='PCV-10 2') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('PCV-10 2','PCV-10 2',0); End
If Not Exists(Select 1 From LookupItem where Name='PCV-10 3') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('PCV-10 3','PCV-10 3',0); End

If Not Exists(Select 1 From LookupItem where Name='Rota virus 1') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Rota virus 1','Rota virus 1',0); End
If Not Exists(Select 1 From LookupItem where Name='Rota Virus 2') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Rota Virus 2','Rota Virus 2',0); End

If Not Exists(Select 1 From LookupItem where Name='IPV') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('IPV','IPV',0); End

If Not Exists(Select 1 From LookupItem where Name='Measles 6 Months') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Measles 6 Months','Measles 6 Months',0); End
If Not Exists(Select 1 From LookupItem where Name='Measles 9 Months') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Measles 9 Months','Measles 9 Months',0); End
If Not Exists(Select 1 From LookupItem where Name='Measles 18 Months') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Measles 18 Months','Measles 18 Months',0); End

If Not Exists(Select 1 From LookupItem where Name='Other') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Other','Other',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='BCG')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='BCG'),'BCG',1); end

If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='OPV 0')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='OPV 0'),'OPV 0',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='OPV 1')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='OPV 1'),'OPV 1',3); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='OPV 2')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='OPV 2'),'OPV 2',4); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='OPV 3')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='OPV 3'),'OPV 3',5); end 

If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Pentavalent 1')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Pentavalent 1'),'Pentavalent 1',6); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Pentavalent 2')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Pentavalent 2'),'Pentavalent 2',7); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Pentavalent 3')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Pentavalent 3'),'Pentavalent 3',8); end
 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PCV-10 1')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PCV-10 1'),'PCV-10 1',9); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PCV-10 2')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PCV-10 2'),'PCV-10 2',10); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PCV-10 3')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PCV-10 3'),'PCV-10 3',11); end 

If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Rota virus 1')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Rota virus 1'),'Rota virus 1',12); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Rota Virus 2')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Rota Virus 2'),'Rota Virus 2',13); end 

If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='IPV')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='IPV'),'IPV',14); end 

If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Measles 6 Months')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Measles 6 Months'),'Measles 6 Months',15); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Measles 9 Months')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Measles 9 Months'),'Measles 9 Months',16); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Measles 18 Months')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Measles 18 Months'),'Measles 18 Months',17); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='VaccinesComprehensive'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other'),'Other',18); end 
