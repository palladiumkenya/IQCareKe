------------PNCBreast
-- master
If Not Exists(Select 1 From LookupMaster where Name='Breast') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('Breast','Breast',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Normal') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Normal','Normal',0); End
If Not Exists(Select 1 From LookupItem where Name='Cracked') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Cracked','Cracked',0); End
If Not Exists(Select 1 From LookupItem where Name='Engorged') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Engorged','Engorged',0); End
If Not Exists(Select 1 From LookupItem where Name='Mastitis') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Mastitis','Mastitis',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Breast') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Normal')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Breast'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Normal'),'Normal',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Breast') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Cracked')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Breast'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Cracked'),'Cracked',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Breast') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Engorged')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Breast'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Engorged'),'Engorged',3); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Breast') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Mastitis')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Breast'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Mastitis'),'Mastitis',4); end 


------------PNCUterus
-- master
If Not Exists(Select 1 From LookupMaster where Name='Uterus') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('Uterus','Uterus',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Contracted') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Contracted','Contracted',0); End
If Not Exists(Select 1 From LookupItem where Name='Not Contracted') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Not Contracted','Not Contracted',0); End
If Not Exists(Select 1 From LookupItem where Name='Other') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Other','Other',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Uterus') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Contracted')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Uterus'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Contracted'),'Contracted',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Uterus') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Not Contracted')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Uterus'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Not Contracted'),'Not Contracted',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Uterus') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Uterus'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other'),'Other',3); end 


------------PNCLochia
-- master
If Not Exists(Select 1 From LookupMaster where Name='Lochia') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('Lochia','Lochia',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Foul smelling') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Foul smelling','Foul smelling',0); End
If Not Exists(Select 1 From LookupItem where Name='Excessive') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Excessive','Excessive',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Lochia') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Normal')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Lochia'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Normal'),'Normal',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Lochia') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Foul smelling')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Lochia'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Foul smelling'),'Foul smelling',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Lochia') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Excessive')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Lochia'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Excessive'),'Excessive',3); end 


------------PNCPostpartum haemorrhage						
-- master
If Not Exists(Select 1 From LookupMaster where Name='PostPartumHaemorrhage') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('PostPartumHaemorrhage','PostPartumHaemorrhage',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostPartumHaemorrhage') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Contracted')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostPartumHaemorrhage'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Contracted'),'Contracted',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostPartumHaemorrhage') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Not Contracted')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostPartumHaemorrhage'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Not Contracted'),'Not Contracted',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostPartumHaemorrhage') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PostPartumHaemorrhage'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Other'),'Other',3); end 


------------PNCEpisiotomy
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



------------PNCC-Section Site
-- master
If Not Exists(Select 1 From LookupMaster where Name='C_SectionSite') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('C_SectionSite','C_SectionSite',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Bleeding') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Bleeding','Bleeding',0); End
-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='C_SectionSite') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Normal')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='C_SectionSite'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Normal'),'Normal',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='C_SectionSite') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Bleeding')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='C_SectionSite'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Bleeding'),'Bleeding',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='C_SectionSite') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Infected')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='C_SectionSite'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Infected'),'Infected',3); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='C_SectionSite') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Gaping')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='C_SectionSite'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Gaping'),'Gaping',4); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='C_SectionSite') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/A')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='C_SectionSite'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/A'),'N/A',5); end 



------------PNC Fistula Screening				
-- master
If Not Exists(Select 1 From LookupMaster where Name='Fistula_Screening') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('Fistula_Screening','Fistula_Screening',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='None') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('None','None',0); End
If Not Exists(Select 1 From LookupItem where Name='Rectovaginal Fistula') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Rectovaginal Fistula','Rectovaginal Fistula',0); End
If Not Exists(Select 1 From LookupItem where Name='Vesicovaginal Fistula') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Vesicovaginal Fistula','Vesicovaginal Fistula',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula_Screening') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='None')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula_Screening'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='None'),'None',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula_Screening') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Rectovaginal Fistula')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula_Screening'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Rectovaginal Fistula'),'Rectovaginal Fistula',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula_Screening') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Vesicovaginal Fistula')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula_Screening'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Vesicovaginal Fistula'),'Vesicovaginal Fistula',3); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula_Screening') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/A')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Fistula_Screening'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='N/A'),'N/A',4); end 


------------PNC Baby Condition			
-- master
If Not Exists(Select 1 From LookupMaster where Name='Baby_Condition') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('Baby_Condition','Baby_Condition',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='Well') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Well','Well',0); End
If Not Exists(Select 1 From LookupItem where Name='Unwell') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Unwell','Unwell',0); End
-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Baby_Condition') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Well')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Baby_Condition'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Well'),'Well',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Baby_Condition') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Unwell')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Baby_Condition'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Unwell'),'Unwell',2); end 


------------PNC Infant Drug		
-- master
If Not Exists(Select 1 From LookupMaster where Name='Infant_Drug') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('Infant_Drug','Infant_Drug',0); End


-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Infant_Drug') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Zidovudine')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Infant_Drug'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Zidovudine'),'Zidovudine (AZT)',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='Infant_Drug') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Nevirapine')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='Infant_Drug'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Nevirapine'),'Nevirapine (NVP)',2); end 

