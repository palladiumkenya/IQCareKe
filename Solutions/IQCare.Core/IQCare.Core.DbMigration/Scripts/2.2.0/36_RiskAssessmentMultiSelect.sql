------------STI Screening and Treatment
-- master
If Not Exists(Select 1 From LookupMaster where Name='SpecifyRiskReductionEducation ')
 Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('SpecifyRiskReductionEducation','SpecifyRiskReductionEducation',0); End
 go
 If Not Exists(Select 1 From LookupMaster where Name='SpecifyReferralPreventionServices')
 Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('SpecifyReferralPreventionServices','SpecifyReferralPreventionServices',0); End
 go
If Not Exists(Select 1 From LookupMaster where Name='SpecifyRiskEducation')
 Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('SpecifyRiskEducation','SpecifyRiskEducation',0); End
 go





-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='HIVInformationPrevention') 
Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('HIVInformationPrevention','HIV Information and Prevention',0); End
If Not Exists(Select 1 From LookupItem where Name='CondomDemonstration') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('CondomDemonstration','Condom use demonstration',0); End
If Not Exists(Select 1 From LookupItem where Name='PartnerRiskReduction') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('PartnerRiskReduction','Partner risk reduction',0); End
If Not Exists(Select 1 From LookupItem where Name='VMMC') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('VMMC','VMMC(applicable if  male)',0); End
If Not Exists(Select 1 From LookupItem where Name='OptimalAdherencePrep') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('OptimalAdherencePrep','Optimal adherence to Prep',0); End
If Not Exists(Select 1 From LookupItem where Name='AdherencePrepBasedHTSSchedule') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('AdherencePrepBasedHTSSchedule','Adherence to Prep-based HTS Schedule',0); End
If Not Exists(Select 1 From LookupItem where Name='STISymptomsTreatment') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('STISymptomsTreatment','Identification of signs and symptoms of STIs and seeking treatment',0); End
If Not Exists(Select 1 From LookupItem where Name='PartnerSTIScreening') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('PartnerSTIScreening','Partner STIS Screening and treatment',0); End
If Not Exists(Select 1 From LookupItem where Name='CommunicationSkillsSaferSex') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('CommunicationSkillsSaferSex','Communication Skills and Safer Sex negotiation',0); End
If Not Exists(Select 1 From LookupItem where Name='AvoidanceRiskyAreasRel') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('AvoidanceRiskyAreasRel','Identification and avoidance of risky areas and relationships',0); End



-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='HIVInformationPrevention')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='HIVInformationPrevention'),'HIV Information and Prevention',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='CondomDemonstration')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='CondomDemonstration'),'Condom use demonstration',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PartnerRiskReduction')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PartnerRiskReduction'),'Partner risk reduction',3); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='VMMC')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='VMMC'),'VMMC(applicable if  male)',4); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='OptimalAdherencePrep')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='OptimalAdherencePrep'),'Optimal adherence to Prep',5); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='AdherencePrepBasedHTSSchedule')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='AdherencePrepBasedHTSSchedule'),'Adherence to Prep-based HTS Schedule',6); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='STISymptomsTreatment')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='STISymptomsTreatment'),'Identification of signs and symptoms of STIs and seeking treatment',7); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PartnerSTIScreening')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PartnerSTIScreening'),'Partner STIS Screening and treatment',7); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='CommunicationSkillsSaferSex')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='CommunicationSkillsSaferSex'),'Communication Skills and Safer Sex negotiation',7); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='AvoidanceRiskyAreasRel')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskReductionEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='AvoidanceRiskyAreasRel'),'Identification and avoidance of risky areas and relationships',7); end 





If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='HIVInformationPrevention')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='HIVInformationPrevention'),'HIV Information and Prevention',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='CondomDemonstration')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='CondomDemonstration'),'Condom use demonstration',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PartnerRiskReduction')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PartnerRiskReduction'),'Partner risk reduction',3); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='VMMC')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='VMMC'),'VMMC(applicable if  male)',4); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='OptimalAdherencePrep')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='OptimalAdherencePrep'),'Optimal adherence to Prep',5); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='AdherencePrepBasedHTSSchedule')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='AdherencePrepBasedHTSSchedule'),'Adherence to Prep-based HTS Schedule',6); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='STISymptomsTreatment')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='STISymptomsTreatment'),'Identification of signs and symptoms of STIs and seeking treatment',7); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PartnerSTIScreening')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PartnerSTIScreening'),'Partner STIS Screening and treatment',7); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='CommunicationSkillsSaferSex')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='CommunicationSkillsSaferSex'),'Communication Skills and Safer Sex negotiation',7); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='AvoidanceRiskyAreasRel')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyReferralPreventionServices'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='AvoidanceRiskyAreasRel'),'Identification and avoidance of risky areas and relationships',7); end 




If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='HIVInformationPrevention')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='HIVInformationPrevention'),'HIV Information and Prevention',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='CondomDemonstration')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='CondomDemonstration'),'Condom use demonstration',2); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PartnerRiskReduction')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PartnerRiskReduction'),'Partner risk reduction',3); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='VMMC')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='VMMC'),'VMMC(applicable if  male)',4); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='OptimalAdherencePrep')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='OptimalAdherencePrep'),'Optimal adherence to Prep',5); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='AdherencePrepBasedHTSSchedule')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='AdherencePrepBasedHTSSchedule'),'Adherence to Prep-based HTS Schedule',6); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='STISymptomsTreatment')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='STISymptomsTreatment'),'Identification of signs and symptoms of STIs and seeking treatment',7); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PartnerSTIScreening')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PartnerSTIScreening'),'Partner STIS Screening and treatment',7); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='CommunicationSkillsSaferSex')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='CommunicationSkillsSaferSex'),'Communication Skills and Safer Sex negotiation',7); end 
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='AvoidanceRiskyAreasRel')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='SpecifyRiskEducation'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='AvoidanceRiskyAreasRel'),'Identification and avoidance of risky areas and relationships',7); end 




 If Not Exists(Select 1 From LookupMaster where Name='RiskAssessmentDone')
 Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('RiskAssessmentDone','RiskAssessmentDone',0); End





 If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='RiskAssessmentDone') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='Yes')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='RiskAssessmentDone'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='Yes'),'Yes',1); end 
  If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='RiskAssessmentDone') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='No')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='RiskAssessmentDone'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='No'),'No',1); end 
  
  
  
  
  
if  exists(select * from LookupItem where Name like 'Dis.Cop')
BEGIN
if not Exists (select * from LookupMasterItem  lmi inner join LookupMaster lm on lm.Id=lmi.LookupMasterId
inner join LookupItem lit on lit.Id=lmi.LookupItemId where 
lit.Name='Dis.Cop' and lm.Name='PopulationType')
BEGIN
insert into LookupMasterItem 
select lm.Id,lit.Id,lit.DisplayName,'3.00' as OrdRank from LookupMaster lm,LookupItem lit
where lm.Name='PopulationType' and lit.Name='Dis.Cop'
END
END




  