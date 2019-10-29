update LookupItem set Name='PMTCTMaternalRegimens' ,DisplayName='PMTCT Maternal Regimens' where Name='PMTCTRegimens'
update LookupMasterItem set DisplayName='PMTCT Maternal Regimens' where DisplayName='PMTCT Regimens'
update LookupMaster  set Name='PMTCTMaternalRegimens' ,DisplayName='PMTCT Maternal Regimens' where Name='PMTCTRegimens'
If Not Exists(Select 1 From LookupItem where Name='PMTCTInfantRegimens') 
Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag)
 VALUES ('PMTCTInfantRegimens','PMTCT Infant Regimens',0); End

 If Not Exists(Select 1 From LookupMaster where Name='PMTCTInfantRegimens') 
Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag)
 VALUES ('PMTCTInfantRegimens','PMTCT Infant Regimens',0); End


 If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=
(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCT') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PMTCTInfantRegimens
')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCT')
,(SELECT TOP 1 Id FROM LookupItem WHERE Name='PMTCTInfantRegimens'),
(SELECT TOP 1 DisplayName FROM LookupItem WHERE Name='PMTCTInfantRegimens'),1); end






 If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=
(SELECT TOP 1 Id FROM LookupMaster WHERE Name='RegimenClassification') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PMTCTInfantRegimens
')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='RegimenClassification')
,(SELECT TOP 1 Id FROM LookupItem WHERE Name='PMTCTInfantRegimens'),
(SELECT TOP 1 DisplayName FROM LookupItem WHERE Name='PMTCTInfantRegimens'),1); end



 If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=
(SELECT TOP 1 Id FROM LookupMaster WHERE Name='RegimenClassificationPaeds') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PMTCTInfantRegimens
')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='RegimenClassificationPaeds')
,(SELECT TOP 1 Id FROM LookupItem WHERE Name='PMTCTInfantRegimens'),
(SELECT TOP 1 DisplayName FROM LookupItem WHERE Name='PMTCTInfantRegimens'),1); end



 If Not Exists(Select 1 From LookupItem where Name='PC6') 
 Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag)
  VALUES ('PC6','NVP liquid OD for 12 Weeks',0); End

   If Not Exists(Select 1 From LookupItem where Name='PC7') 
 Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag)
  VALUES ('PC7','AZT liquid BID + NVP liquid OD for 6 weeks then NVP liquid OD for 6 weeks',0); End



    If Not Exists(Select 1 From LookupItem where Name='PC8') 
 Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag)
  VALUES ('PC8',
  'AZT liquid BID + NVP liquid OD for 6 weeks then NVP liquid OD 
  until 6 weeks after complete cessation of Breastfeeding (mother NOT on ART)
',0); End



   If Not Exists(Select 1 From LookupItem where Name='PC9') 
 Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag)
  VALUES ('PC9',
  'AZT liquid BID for 12 weeks',0); End




    If Not Exists(Select 1 From LookupItem where Name='PC1X') 
 Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag)
  VALUES ('PC1X','Any other PMTCT regimens for Infants',0); End






If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=
(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTInfantRegimens') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PC6
')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTInfantRegimens')
,(SELECT TOP 1 Id FROM LookupItem WHERE Name='PC6'),(SELECT TOP 1 Name FROM LookupItem WHERE Name='PC6'),1); end



If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=
(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTInfantRegimens') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PC7
')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTInfantRegimens')
,(SELECT TOP 1 Id FROM LookupItem WHERE Name='PC7'),(SELECT TOP 1 Name FROM LookupItem WHERE Name='PC7'),1); end




If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=
(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTInfantRegimens') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PC8
')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTInfantRegimens')
,(SELECT TOP 1 Id FROM LookupItem WHERE Name='PC8'),(SELECT TOP 1 Name FROM LookupItem WHERE Name='PC8'),1); end




If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=
(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTInfantRegimens') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PC9'))
 Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTInfantRegimens')
,(SELECT TOP 1 Id FROM LookupItem WHERE Name='PC9'),(SELECT TOP 1 Name FROM LookupItem WHERE Name='PC9'),1); end





If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=
(SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTInfantRegimens') 
and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='PC1X'))
 Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)
VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='PMTCTInfantRegimens')
,(SELECT TOP 1 Id FROM LookupItem WHERE Name='PC1X'),
(SELECT TOP 1 Name FROM LookupItem WHERE Name='PC1X'),1); END





update  lt  set  DisplayName='Low Risk' 
from LookupMasterItem lt 
inner join LookupMaster m on m.Id=lt.LookupMasterId
inner join LookupItem l on l.Id=lt.LookupItemId
where l.[Name]='NoRisk'
and m.Name='AssessmentOutCome'


update LookupItem set Name ='LowRisk' , DisplayName='Low Risk' where
Name ='NoRisk'
