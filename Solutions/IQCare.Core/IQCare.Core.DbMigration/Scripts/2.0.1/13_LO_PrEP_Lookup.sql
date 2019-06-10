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


------------Contraindications for PrEP Present
-- master
If Not Exists(Select 1 From LookupMaster where Name='ContraindicationsPrEP') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('ContraindicationsPrEP','ContraindicationsPrEP',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='None') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('None','None',0); End
If Not Exists(Select 1 From LookupItem where Name='Confirmed HIV+') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Confirmed HIV+','Confirmed HIV+',0); End
If Not Exists(Select 1 From LookupItem where Name='Renal impairment') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Renal impairment','Renal impairment',0); End
If Not Exists(Select 1 From LookupItem where Name='Not willing') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Not willing','Not willing',0); End
If Not Exists(Select 1 From LookupItem where Name='Less than 35kgs') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Less than 35kgs','Less than 35kgs',0); End
If Not Exists(Select 1 From LookupItem where Name='Under 15 yrs of age') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Under 15 yrs of age','Under 15 yrs of age',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ContraindicationsPrEP') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='None')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ContraindicationsPrEP'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='None'),'None',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ContraindicationsPrEP') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Confirmed HIV+')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ContraindicationsPrEP'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Confirmed HIV+'),'Confirmed HIV+',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ContraindicationsPrEP') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Renal impairment')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ContraindicationsPrEP'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Renal impairment'),'Renal impairment',3); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ContraindicationsPrEP') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Not willing')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ContraindicationsPrEP'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Not willing'),'Not willing',4); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ContraindicationsPrEP') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Less than 35kgs')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ContraindicationsPrEP'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Less than 35kgs'),'Less than 35kgs',5); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ContraindicationsPrEP') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Under 15 yrs of age')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ContraindicationsPrEP'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Under 15 yrs of age'),'Under 15 yrs of age',6); end 


------------PrEP Status today
-- master
If Not Exists(Select 1 From LookupMaster where Name='PrEP_Status') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('PrEP_Status','PrEP_Status',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Status') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Status','Status',0); End
If Not Exists(Select 1 From LookupItem where Name='Continue') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Continue','Continue',0); End
If Not Exists(Select 1 From LookupItem where Name='Restart') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Restart','Restart',0); End
If Not Exists(Select 1 From LookupItem where Name='Substitute') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Substitute','Substitute',0); End
If Not Exists(Select 1 From LookupItem where Name='Defer') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Defer','Defer',0); End
-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PrEP_Status') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Status')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PrEP_Status'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Status'),'Status',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PrEP_Status') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Continue')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PrEP_Status'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Continue'),'Continue',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PrEP_Status') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Restart')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PrEP_Status'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Restart'),'Restart',3); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PrEP_Status') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Substitute')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PrEP_Status'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Substitute'),'Substitute',4); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PrEP_Status') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Defer')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PrEP_Status'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Defer'),'Defer',5); end  


------------Reasons not to give PrEP Appointment 
-- master
If Not Exists(Select 1 From LookupMaster where Name='ReasonsPrEPAppointmentNotGiven') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('ReasonsPrEPAppointmentNotGiven','ReasonsPrEPAppointmentNotGiven',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Risk will no longer exist') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Risk will no longer exist','Risk will no longer exist',0); End
If Not Exists(Select 1 From LookupItem where Name='Intention to transfer out') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Intention to transfer out','Intention to transfer out',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ReasonsPrEPAppointmentNotGiven') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Risk will no longer exist')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ReasonsPrEPAppointmentNotGiven'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Risk will no longer exist'),'Risk will no longer exist',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ReasonsPrEPAppointmentNotGiven') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Intention to transfer out')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ReasonsPrEPAppointmentNotGiven'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Intention to transfer out'),'Intention to transfer out',2); end 
