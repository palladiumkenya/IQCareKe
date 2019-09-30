
-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='PreferredPREPRegimen') 
Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('PreferredPREPRegimen','Preferred PREP Regimen',0); End
If Not Exists(Select 1 From LookupItem where Name='OtherPREPRegimen') 
Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('OtherPREPRegimen','Other PREP Regimen',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='RegimenClassificationAdult') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PreferredPREPRegimen')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='RegimenClassificationAdult'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='PreferredPrepRegimen'),'Preferred PREP Regimen',1); end

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='RegimenClassificationAdult') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='OtherPREPRegimen')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='RegimenClassificationAdult'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='OtherPREPRegimen'),'Other PREP Regimen',1); end




If Not Exists(Select 1 From LookupMaster where Name='PreferredPREPRegimen') 
Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('PreferredPREPRegimen','Preferred PREP Regimen',0); End
If Not Exists(Select 1 From LookupMaster where Name='OtherPREPRegimen') 
Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('OtherPREPRegimen','Other PREP Regimen',0); End



If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PreferredPREPRegimen') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PRP1A')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PreferredPREPRegimen'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='PRP1A'),'TDF + FTC (PrEP)',1); end






If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OtherPREPRegimen') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PRP1B')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OtherPREPRegimen'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='PRP1B'),'TDF + FTC (PrEP)',1); end




If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OtherPREPRegimen') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PRP1C')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OtherPREPRegimen'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='PRP1C'),'TDF (PrEP)',1); end







If Not Exists(Select 1 From LookupMaster where Name='ART') 
Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('ART','ART',0); End
If Not Exists(Select 1 From LookupMaster where Name='PMTCT') 
Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('PMTCT','PMTCT',0); End
If Not Exists(Select 1 From LookupMaster where Name='PEP') 
Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('PEP','PEP',0); End
If Not Exists(Select 1 From LookupMaster where Name='Non-ART') 
Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('Non-ART','Non-ART',0); End
If Not Exists(Select 1 From LookupMaster where Name='PREP') 
Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('PREP','PREP',0); End

If Not Exists(Select 1 From LookupMaster where Name='HBV') 
Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('HBV','HBV',0); End


If Not Exists(Select 1 From LookupMaster where Name='Hepatitis B') 
Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('Hepatitis B','Hepatitis B',0); End







If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ART') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='AdultARTFirstLine')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ART'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='AdultARTFirstLine'),'Adult FirstLine Regimen',1); end


go




If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ART') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='AdultARTSecondLine')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ART'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='AdultARTSecondLine'),'Adult Secondline Regimen',1); end


go




If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ART') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='AdultARTThirdLine')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ART'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='AdultARTThirdLine'),'Adult Thirdline Regimen',1); end

go


If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ART') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PaedsARTThirdLine')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ART'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='PaedsARTThirdLine'),'Paeds Thirdline Regimen',1); end


go



If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ART') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PaedsARTSecondLine')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ART'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='PaedsARTSecondLine'),'Paeds Secondline Regimen',1); end

go


If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ART') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PaedsARTFirstLine')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ART'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='PaedsARTFirstLine'),'Paeds FirstLine Regimen',1); end



go



If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PEP') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PEP Regimens')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PEP'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='PEP Regimens'),'PEP Regimens',1); end

go



If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCT') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PMTCTRegimens')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCT'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='PMTCTRegimens'),'PMTCT Regimens',1); end


If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PREP') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='OtherPREPRegimen')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PREP'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='OtherPREPRegimen'),'Other PREP Regimen',2); end



If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PREP') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PreferredPREPRegimen')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PREP'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='PreferredPREPRegimen'),'Preferred PREP Regimen',1); end

go





If Not Exists(Select 1 From LookupMaster where Name='PEPRegimens') 
Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('PEPRegimens','PEP Regimens',0); End

go



If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PEPRegimens') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='CS4X')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PEPRegimens'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='CS4X'),'Any other 2nd line Paediatric regimens',1); end



If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PEPRegimens') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PA1C')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PEPRegimens'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='PA1C'),'AZT + 3TC + ATV/r (Adult PEP)',1); end



If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PEPRegimens') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PA3C')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PEPRegimens'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='PA3C'),'TDF + 3TC + ATV/r (Adult PEP)',1); end



If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PEPRegimens') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PA4X')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PEPRegimens'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='PA4X'),'Any other PEP regimens for Adults',1); end

If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PEPRegimens') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PC1A')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PEPRegimens'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='PC1A'),'AZT + 3TC + ATV/r (Adult PEP)',1); end



If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PEPRegimens') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PC3A')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PEPRegimens'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='PC3A'),'ABC + 3TC + LPV/r (Paed PEP)',1); end



If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PEPRegimens') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PC4X')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PEPRegimens'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='PC4X'),'Any other PEP regimens for Children',1); end



If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='RegimenClassificationAdult') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PEP Regimens')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='RegimenClassificationAdult'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='PEP Regimens'),'PEP Regimens',1); end



If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='RegimenClassificationPaeds') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PEP Regimens')) 
Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='RegimenClassificationPaeds'),
(SELECT TOP 1 Id FROM LookupItem WHERE Name='PEP Regimens'),'PEP Regimens',1); end




