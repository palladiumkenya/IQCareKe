------------HEIVisitType
-- master
If Not Exists(Select 1 From LookupMaster where Name='HEIVisitType') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('HEIVisitType','HEIVisitType',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Initial Visit') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Initial Visit','Initial Visit',0); End
If Not Exists(Select 1 From LookupItem where Name='Follow Up visit') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Follow Up visit','Follow Up visit',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIVisitType') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Initial Visit')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIVisitType'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Initial Visit'),'Initial Visit',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIVisitType') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Follow Up visit')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIVisitType'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Follow Up visit'),'Follow Up visit',2); end 


-------DeliveryPlace
-- master
If Not Exists(Select 1 From LookupMaster where Name='DeliveryPlace') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('DeliveryPlace','DeliveryPlace',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Health Facility') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Health Facility','Health Facility',0); End
If Not Exists(Select 1 From LookupItem where Name='Home') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Home','Home',0); End
If Not Exists(Select 1 From LookupItem where Name='Other') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Other','Other',0); End


-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='DeliveryPlace') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Health Facility')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='DeliveryPlace'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Health Facility'),'Health Facility',1) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='DeliveryPlace') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Home')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='DeliveryPlace'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Home'),'Home',2) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='DeliveryPlace') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='DeliveryPlace'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other'),'Other',3) end;

---------DeliveryMode
-- master
If Not Exists(Select 1 From LookupMaster where Name='DeliveryMode') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('DeliveryMode','DeliveryMode',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Normal Delivery') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Normal Delivery','Normal Delivery',0); End
If Not Exists(Select 1 From LookupItem where Name='Caesarean section') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Caesarean section','Caesarean section',0); End
If Not Exists(Select 1 From LookupItem where Name='Breech') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Breech','Breech',0); End
If Not Exists(Select 1 From LookupItem where Name='Assisted Vaginal Delivery') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Assisted Vaginal Delivery','Assisted Vaginal Delivery',0); End


-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='DeliveryMode') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Normal Delivery')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='DeliveryMode'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Normal Delivery'),'Normal Delivery',1) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='DeliveryMode') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Caesarean section')) Begin   Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='DeliveryMode'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Caesarean section'),'Caesarean section',2) end; 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='DeliveryMode') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Breech')) Begin   Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='DeliveryMode'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Breech'),'Breech',3) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='DeliveryMode') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Assisted Vaginal Delivery')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='DeliveryMode'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Assisted Vaginal Delivery'),'Assisted Vaginal Delivery',4) end;


--------ARVProphylaxis
-- master
If Not Exists(Select 1 From LookupMaster where Name='ARVProphylaxis') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('ARVProphylaxis','ARVProphylaxis',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='NVP for 6 weeks') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('NVP for 6 weeks','NVP for 6 weeks',0); End
If Not Exists(Select 1 From LookupItem where Name='NVP for 12 weeks') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('NVP for 12 weeks','NVP for 12 weeks',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ARVProphylaxis') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='NVP for 6 weeks')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ARVProphylaxis'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='NVP for 6 weeks'),'NVP for 6 weeks',1) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ARVProphylaxis') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='NVP for 12 weeks')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ARVProphylaxis'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='NVP for 12 weeks'),'NVP for 12 weeks',2) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ARVProphylaxis') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='None')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ARVProphylaxis'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='None'),'None',3) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ARVProphylaxis') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ARVProphylaxis'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other'),'Other',4) end;


------------MotherRegisteredAtClinic
-- master
If Not Exists(Select 1 From LookupMaster where Name='MotherRegisteredAtClinic') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('MotherRegisteredAtClinic','MotherRegisteredAtClinic',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='MotherRegisteredAtClinic') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Yes')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='MotherRegisteredAtClinic'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Yes'),'Yes',1) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='MotherRegisteredAtClinic') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='No')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='MotherRegisteredAtClinic'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='No'),'No',2) end;


-------MotherState
-- master
If Not Exists(Select 1 From LookupMaster where Name='MotherState') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('MotherState','MotherState',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Alive') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Alive','Alive',0); End
If Not Exists(Select 1 From LookupItem where Name='Dead') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Dead','Dead',0); End 


-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='MotherState') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Alive')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='MotherState'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Alive'),'Alive',1) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='MotherState') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Dead')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='MotherState'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Dead'),'Dead',2)  end;

-------PrimaryCareGiver
-- master
If Not Exists(Select 1 From LookupMaster where Name='PrimaryCareGiver') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('PrimaryCareGiver','PrimaryCareGiver',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Mother') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Mother','Mother',0); End
If Not Exists(Select 1 From LookupItem where Name='Guardian') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Guardian','Guardian',0); End 


-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PrimaryCareGiver') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Mother')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PrimaryCareGiver'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Mother'),'Mother',1) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PrimaryCareGiver') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Guardian')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PrimaryCareGiver'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Guardian'),'Guardian',2)  end;

------------PMTCTHEIMotherReceiveDrugs
-- master
If Not Exists(Select 1 From LookupMaster where Name='PMTCTHEIMotherReceiveDrugs') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('PMTCTHEIMotherReceiveDrugs','PMTCTHEIMotherReceiveDrugs',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='HAART') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('HAART','HAART',0); End
If Not Exists(Select 1 From LookupItem where Name='None') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('None','None',0); End
If Not Exists(Select 1 From LookupItem where Name='Other') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Other','Other',0); End
If Not Exists(Select 1 From LookupItem where Name='Unknown') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Unknown','Unknown',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherReceiveDrugs') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='HAART')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherReceiveDrugs'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='HAART'),'HAART',1) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherReceiveDrugs') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='None')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherReceiveDrugs'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='None'),'None',2) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherReceiveDrugs') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherReceiveDrugs'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other'),'Other',3) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherReceiveDrugs') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Unknown')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherReceiveDrugs'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Unknown'),'Unknown',4) end;



------------PMTCTHEIMotherRegimen
-- master
If Not Exists(Select 1 From LookupMaster where Name='PMTCTHEIMotherRegimen') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('PMTCTHEIMotherRegimen','PMTCTHEIMotherRegimen',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='PM1 AZT from 14Wks') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('PM1 AZT from 14Wks','PM1 AZT from 14Wks',0); End
If Not Exists(Select 1 From LookupItem where Name='PM2 NVP stat + AZT stat') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('PM2 NVP stat + AZT stat','PM2 NVP stat + AZT stat',0); End
If Not Exists(Select 1 From LookupItem where Name='PM3 AZT + 3TC + NVP') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('PM3 AZT + 3TC + NVP','PM3 AZT + 3TC + NVP',0); End
If Not Exists(Select 1 From LookupItem where Name='PM4 AZT + 3TC + EFV') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('PM4 AZT + 3TC + EFV','PM4 AZT + 3TC + EFV',0); End
If Not Exists(Select 1 From LookupItem where Name='PM5 AZT + 3TC + LPV/r') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('PM5 AZT + 3TC + LPV/r','PM5 AZT + 3TC + LPV/r',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM1 AZT from 14Wks')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM1 AZT from 14Wks'),'PM1 AZT from 14Wks',1) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM2 NVP stat + AZT stat')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM2 NVP stat + AZT stat'),'PM2 NVP stat + AZT stat',2) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM3 AZT + 3TC + NVP')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM3 AZT + 3TC + NVP'),'PM3 AZT + 3TC + NVP',3) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM4 AZT + 3TC + EFV')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherRegimen'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM4 AZT + 3TC + EFV'),'PM4 AZT + 3TC + EFV',4) end;


------------PMTCTHEIMotherDrugsAtInfantEnrolYN
-- master
If Not Exists(Select 1 From LookupMaster where Name='PMTCTHEIMotherDrugsAtInfantEnrolYN') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('PMTCTHEIMotherDrugsAtInfantEnrolYN','PMTCTHEIMotherDrugsAtInfantEnrolYN',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherDrugsAtInfantEnrolYN') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Yes')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherDrugsAtInfantEnrolYN'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Yes'),'Yes',1) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherDrugsAtInfantEnrolYN') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='No')) Begin   Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherDrugsAtInfantEnrolYN'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='No'),'No',2) end;



------------PMTCTHEIMotherDrugsAtInfantEnrol
-- master
If Not Exists(Select 1 From LookupMaster where Name='PMTCTHEIMotherDrugsAtInfantEnrol') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('PMTCTHEIMotherDrugsAtInfantEnrol','PMTCTHEIMotherDrugsAtInfantEnrol',0); End


-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherDrugsAtInfantEnrol') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM1 AZT from 14Wks')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherDrugsAtInfantEnrol'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM1 AZT from 14Wks'),'PM1 AZT from 14Wks',1) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherDrugsAtInfantEnrol') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM2 NVP stat + AZT stat')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherDrugsAtInfantEnrol'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM2 NVP stat + AZT stat'),'PM2 NVP stat + AZT stat',2) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherDrugsAtInfantEnrol') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM3 AZT + 3TC + NVP')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherDrugsAtInfantEnrol'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM3 AZT + 3TC + NVP'),'PM3 AZT + 3TC + NVP',3) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherDrugsAtInfantEnrol') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM4 AZT + 3TC + EFV')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherDrugsAtInfantEnrol'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM4 AZT + 3TC + EFV'),'PM4 AZT + 3TC + EFV',4) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherDrugsAtInfantEnrol') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM5 AZT + 3TC + LPV/r')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTHEIMotherDrugsAtInfantEnrol'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PM5 AZT + 3TC + LPV/r'),'PM5 AZT + 3TC + LPV/r',5) end;


------------ImmunizationPeriod
-- master
If Not Exists(Select 1 From LookupMaster where Name='ImmunizationPeriod') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('ImmunizationPeriod','ImmunizationPeriod',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Birth') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Birth','Birth',0); End
If Not Exists(Select 1 From LookupItem where Name='6 Weeks') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('6 Weeks','6 Weeks',0); End
If Not Exists(Select 1 From LookupItem where Name='10 weeks') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('10 weeks','10 weeks',0); End
If Not Exists(Select 1 From LookupItem where Name='14 weeks') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('14 weeks','14 weeks',0); End
If Not Exists(Select 1 From LookupItem where Name='6 Months') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('6 Months','6 Months',0); End
If Not Exists(Select 1 From LookupItem where Name='7 Months') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('7 Months','7 Months',0); End
If Not Exists(Select 1 From LookupItem where Name='9 Months') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('9 Months','9 Months',0); End
If Not Exists(Select 1 From LookupItem where Name='11 Months') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('11 Months','11 Months',0); End
If Not Exists(Select 1 From LookupItem where Name='15 Months') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('15 Months','15 Months',0); End
If Not Exists(Select 1 From LookupItem where Name='2 Years') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('2 Years','2 Years',0); End

If Not Exists(Select 1 From LookupItem where Name='N/a') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('N/a','N/a',0); End
If Not Exists(Select 1 From LookupItem where Name='3 Months') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('3 Months','3 Months',0); End
If Not Exists(Select 1 From LookupItem where Name='12 Months') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('12 Months','12 Months',0); End
If Not Exists(Select 1 From LookupItem where Name='18 Months') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('18 Months','18 Months',0); End
If Not Exists(Select 1 From LookupItem where Name='36 Months') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('36 Months','36 Months',0); End


-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Birth')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Birth'),'Birth',1) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='6 Weeks')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='6 Weeks'),'6 Weeks',2) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='10 Weeks')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='10 weeks'),'10 weeks',3) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='14 Weeks')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='14 weeks'),'14 weeks',4) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='6 Months')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='6 Months'),'6 Months',5) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='7 Months')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='7 Months'),'7 Months',6) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='9 Months')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='9 Months'),'9 Months',7) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='11 Months')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='11 Months'),'11 Months',8) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='15 Months')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='15 Months'),'15 Months',9) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='2 Years')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='2 Years'),'2 Years',10) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/a')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ImmunizationPeriod'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/a'),'N/a',11) end;

------------HEIMilestones
-- master
If Not Exists(Select 1 From LookupMaster where Name='HEIMilestone') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('HEIMilestone','HEIMilestone',0); End
-- LookupMasterItem

If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIMilestone') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='3 Months')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIMilestone'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='3 Months'),'3 Months',1) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIMilestone') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='6 Months')) Begin   Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIMilestone'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='6 Months'),'6 Months',2) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIMilestone') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='9 Months')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIMilestone'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='9 Months'),'9 Months',3) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIMilestone') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='12 Months')) Begin   Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIMilestone'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='12 Months'),'12 Months',4) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIMilestone') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='15 Months')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIMilestone'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='15 Months'),'15 Months',5) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIMilestone') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='18 Months')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIMilestone'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='18 Months'),'18 Months',6) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIMilestone') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='36 Months')) Begin   Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='HEIMilestone'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='36 Months'),'36 Months',7) end;

------------MilestoneStatus
-- master
If Not Exists(Select 1 From LookupMaster where Name='MilestoneStatus') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('MilestoneStatus','MilestoneStatus',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Normal') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Normal','Normal',0); End
If Not Exists(Select 1 From LookupItem where Name='Delayed') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Delayed','Delayed',0); End
If Not Exists(Select 1 From LookupItem where Name='Regressed') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Regressed','Regressed',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='MilestoneStatus') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Normal')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='MilestoneStatus'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Normal'),'Normal',1) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='MilestoneStatus') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Delayed')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='MilestoneStatus'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Delayed'),'Delayed',2) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='MilestoneStatus') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Delayed')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='MilestoneStatus'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Regressed'),'Regressed',3) end;


------------InfantFeeding
-- master
If Not Exists(Select 1 From LookupMaster where Name='InfantFeeding') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('InfantFeeding','InfantFeeding',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Exclusive Breast Feeding (EBF)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Exclusive Breast Feeding (EBF)','Exclusive Breast Feeding (EBF)',0); End
If Not Exists(Select 1 From LookupItem where Name='Exclusive Replacement Feeding (ERF)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Exclusive Replacement Feeding (ERF)','Exclusive Replacement Feeding (ERF)',0); End
If Not Exists(Select 1 From LookupItem where Name='Mixed Feeding (MF)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Mixed Feeding (MF)','Mixed Feeding (MF)',0); End
If Not Exists(Select 1 From LookupItem where Name='Breastfeeding') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Breastfeeding','Breastfeeding',0); End
If Not Exists(Select 1 From LookupItem where Name='Not Breastfeeding') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Not Breastfeeding','Not Breastfeeding',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='InfantFeeding') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Exclusive Breast Feeding (EBF)')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='InfantFeeding'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Exclusive Breast Feeding (EBF)'),'Exclusive Breast Feeding (EBF)',1) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='InfantFeeding') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Exclusive Replacement Feeding (ERF)')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='InfantFeeding'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Exclusive Replacement Feeding (ERF)'),'Exclusive Replacement Feeding (ERF)',2) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='InfantFeeding') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Mixed Feeding (MF)')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='InfantFeeding'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Mixed Feeding (MF)'),'Mixed Feeding (MF)',3) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='InfantFeeding') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Breastfeeding')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='InfantFeeding'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Breastfeeding'),'Breastfeeding',4) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='InfantFeeding') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Not Breastfeeding')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='InfantFeeding'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Not Breastfeeding'),'Not Breastfeeding',5) end;

------------OutcomeAt24Months
-- master
If Not Exists(Select 1 From LookupMaster where Name='OutcomeAt24Months') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('OutcomeAt24Months','OutcomeAt24Months',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Infected Breastfed (IBF)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Infected Breastfed (IBF)','Infected Breastfed (IBF)',0); End
If Not Exists(Select 1 From LookupItem where Name='Infected not Breastfed (IBFn)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Infected not Breastfed (IBFn)','Infected not Breastfed (IBFn)',0); End
If Not Exists(Select 1 From LookupItem where Name='Infected BF Unknown (IBFu)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Infected BF Unknown (IBFu)','Infected BF Unknown (IBFu)',0); End
If Not Exists(Select 1 From LookupItem where Name='Status unknown because test was not done due to LTFU (TnD)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Status unknown because test was not done due to LTFU (TnD)','Status unknown because test was not done due to LTFU (TnD)',0); End
If Not Exists(Select 1 From LookupItem where Name='Child transferred out to another facility before confirming HIV status (TO)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Child transferred out to another facility before confirming HIV status (TO)','Child transferred out to another facility before confirming HIV status (TO)',0); End
If Not Exists(Select 1 From LookupItem where Name='Uninfected Breastfed (UBF)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Uninfected Breastfed (UBF)','Uninfected Breastfed (UBF)',0); End
If Not Exists(Select 1 From LookupItem where Name='Uninfected not Breastfed (UBFn)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Uninfected not Breastfed (UBFn)','Uninfected not Breastfed (UBFn)',0); End
If Not Exists(Select 1 From LookupItem where Name='Uninfected BF Unknown (UBFu)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Uninfected BF Unknown (UBFu)','Uninfected BF Unknown (UBFu)',0); End
If Not Exists(Select 1 From LookupItem where Name='Died Child died before confirming the HIV status ') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Died Child died before confirming the HIV status','Died Child died before confirming the HIV status ',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Infected Breastfed (IBF)')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Infected Breastfed (IBF)'),'Infected Breastfed (IBF)',1) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Infected not Breastfed (IBFn)')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Infected not Breastfed (IBFn)'),'Infected not Breastfed (IBFn)',2) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Infected BF Unknown (IBFu)')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Infected BF Unknown (IBFu)'),'Infected BF Unknown (IBFu)',3) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Status unknown because test was not done due to LTFU (TnD)')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Status unknown because test was not done due to LTFU (TnD)'),'Status unknown because test was not done due to LTFU (TnD)',4) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Child transferred out to another facility before confirming HIV status (TO)')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Child transferred out to another facility before confirming HIV status (TO)'),'Child transferred out to another facility before confirming HIV status (TO)',5) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Uninfected Breastfed (UBF)')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Uninfected Breastfed (UBF)'),'Uninfected Breastfed (UBF)',6) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Uninfected not Breastfed (UBFn)')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Uninfected not Breastfed (UBFn)'),'Uninfected not Breastfed (UBFn)',7) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Died Child died before confirming the HIV status')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Died Child died before confirming the HIV status'),'Died Child died before confirming the HIV status',8) end;
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Died Child died before confirming the HIV status')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OutcomeAt24Months'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Died Child died before confirming the HIV status'),'Died Child died before confirming the HIV status',9) end;
