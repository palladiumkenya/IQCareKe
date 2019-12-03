If Not Exists(Select *  From LookupMaster where Name ='OVC_CareEndedOptions') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('OVC_CareEndedOptions','OVC_CareEndedOptions',0); End



If Not Exists(Select 1 From LookupItem where Name='TransferOutPepfarFacility') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('TransferOutPepfarFacility','Transfer out to a PEPFAR supported facility',0); End
If Not Exists(Select 1 From LookupItem where Name='Aunt') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('Aunt','Aunt',0); End
If Not Exists(Select 1 From LookupItem where Name='TransferOutNonPepfarFacility') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('TransferOutNonPepfarFacility','Transfer out to a non PEPFAR supported facility',0); End
If Not Exists(Select 1 From LookupItem where Name='ExitWithoutGraduation') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('ExitWithoutGraduation','Exit Without Graduation',0); End
If Not Exists(Select  1 From LookupItem where Name like 'GraduatedOutOVC') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('GraduatedOutOVC','Graduated out of OVC' ,0); End







If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OVC_CareEndedOptions') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='TransferOutPepfarFacility')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OVC_CareEndedOptions'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='TransferOutPepfarFacility'),'Transfer out to a PEPFAR supported facility',1); end
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OVC_CareEndedOptions') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='TransferOutNonPepfarFacility')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OVC_CareEndedOptions'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='TransferOutNonPepfarFacility'),'Transfer out to a non PEPFAR supported facility',2); end 

If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OVC_CareEndedOptions') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='ExitWithoutGraduation')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OVC_CareEndedOptions'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='ExitWithoutGraduation'),'Exit Without Graduation',3); end 
 
 If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='OVC_CareEndedOptions') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='GraduatedOutOVC')) Begin Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='OVC_CareEndedOptions'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='GraduatedOutOVC'),'Graduated out of OVC',4); end 
