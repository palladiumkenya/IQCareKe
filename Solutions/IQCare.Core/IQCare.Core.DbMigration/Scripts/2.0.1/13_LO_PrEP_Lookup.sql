------------STI Screening and Treatment
-- master
If Not Exists(Select 1 From LookupMaster where Name='STIScreeningTreatment') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('STIScreeningTreatment','STIScreeningTreatment',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Genital Ulcer Disease (GUD)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Genital Ulcer Disease (GUD)','Genital Ulcer Disease (GUD)',0); End
If Not Exists(Select 1 From LookupItem where Name='Vaginitis or Vaginal discharge (VG)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Vaginitis or Vaginal discharge (VG)','Vaginitis or Vaginal discharge (VG)',0); End
If Not Exists(Select 1 From LookupItem where Name='Cervicitis and/or Cervical discharge') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Cervicitis and/or Cervical discharge','Cervicitis and/or Cervical discharge',0); End
If Not Exists(Select 1 From LookupItem where Name='Pelvic Inflammatory Disease (PID)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Pelvic Inflammatory Disease (PID)','Pelvic Inflammatory Disease (PID)',0); End
If Not Exists(Select 1 From LookupItem where Name='Urethral Discharge (UD)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Urethral Discharge (UD)','Urethral Discharge (UD)',0); End
If Not Exists(Select 1 From LookupItem where Name='Anal Discharge (AD)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Anal Discharge (AD)','Anal Discharge (AD)',0); End
If Not Exists(Select 1 From LookupItem where Name='Others (O)') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Others (O)','Others (O)',0); End


-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='STIScreeningTreatment') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Genital Ulcer Disease (GUD)')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='STIScreeningTreatment'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Genital Ulcer Disease (GUD)'),'Genital Ulcer Disease (GUD)',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='STIScreeningTreatment') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Vaginitis or Vaginal discharge (VG)')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='STIScreeningTreatment'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Vaginitis or Vaginal discharge (VG)'),'Vaginitis or Vaginal discharge (VG)',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='STIScreeningTreatment') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Cervicitis and/or Cervical discharge')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='STIScreeningTreatment'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Cervicitis and/or Cervical discharge'),'Cervicitis and/or Cervical discharge',3); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='STIScreeningTreatment') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Pelvic Inflammatory Disease (PID)')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='STIScreeningTreatment'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Pelvic Inflammatory Disease (PID)'),'Pelvic Inflammatory Disease (PID)',4); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='STIScreeningTreatment') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Urethral Discharge (UD)')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='STIScreeningTreatment'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Urethral Discharge (UD)'),'Urethral Discharge (UD)',5); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='STIScreeningTreatment') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Anal Discharge (AD)')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='STIScreeningTreatment'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Anal Discharge (AD)'),'Anal Discharge (AD)',6); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='STIScreeningTreatment') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Others (O)')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='STIScreeningTreatment'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Others (O)'),'Others (O)',7); end 


------------YesNoUnknown
-- master
If Not Exists(Select 1 From LookupMaster where Name='YesNoUnknown') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('YesNoUnknown','YesNoUnknown',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Yes') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Yes','Yes',0); End
If Not Exists(Select 1 From LookupItem where Name='No') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('No','No',0); End
If Not Exists(Select 1 From LookupItem where Name='Unknown') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Unknown','Unknown',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoUnknown') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Yes')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoUnknown'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Yes'),'Yes',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoUnknown') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='No')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoUnknown'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='No'),'No',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoUnknown') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Unknown')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoUnknown'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Unknown'),'Unknown',3); end 


------------Planning to get Pregnant
-- master
If Not Exists(Select 1 From LookupMaster where Name='PlanningPregnancy') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('PlanningPregnancy','PlanningPregnancy',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Trying to Conceive') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Trying to Conceive','Trying to Conceive',0); End
If Not Exists(Select 1 From LookupItem where Name='In Future') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('In Future','In Future',0); End
If Not Exists(Select 1 From LookupItem where Name='No') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('No','No',0); End
If Not Exists(Select 1 From LookupItem where Name='Dont Know') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Dont Know','Dont Know',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PlanningPregnancy') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Trying to Conceive')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PlanningPregnancy'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Trying to Conceive'),'Trying to Conceive',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PlanningPregnancy') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='In Future')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PlanningPregnancy'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='In Future'),'In Future',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PlanningPregnancy') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='No')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PlanningPregnancy'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='No'),'No',3); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PlanningPregnancy') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Dont Know')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PlanningPregnancy'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Dont Know'),'Dont Know',4); end 


------------YesNoDont_Know
-- master
If Not Exists(Select 1 From LookupMaster where Name='YesNoDont_Know') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('YesNoDont_Know','YesNoDont_Know',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Yes') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Yes','Yes',0); End
If Not Exists(Select 1 From LookupItem where Name='No') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('No','No',0); End
If Not Exists(Select 1 From LookupItem where Name='Dont Know') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Dont Know','Dont Know',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoDont_Know') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Yes')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoDont_Know'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Yes'),'Yes',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoDont_Know') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='No')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoDont_Know'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='No'),'No',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoDont_Know') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Dont Know')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoDont_Know'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Dont Know'),'Dont Know',3); end 

------------Pregnancy Outcome
-- master
If Not Exists(Select 1 From LookupMaster where Name='PregnancyOutcome') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('YesNoDont_Know','YesNoDont_Know',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Yes') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Yes','Yes',0); End
If Not Exists(Select 1 From LookupItem where Name='No') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('No','No',0); End
If Not Exists(Select 1 From LookupItem where Name='Dont Know') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Dont Know','Dont Know',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoDont_Know') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Yes')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoDont_Know'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Yes'),'Yes',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoDont_Know') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='No')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoDont_Know'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='No'),'No',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoDont_Know') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Dont Know')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='YesNoDont_Know'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Dont Know'),'Dont Know',3); end 
