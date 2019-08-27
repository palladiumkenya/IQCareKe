Update LookupItem set Name = 'NVP liquid OD for 12 weeks', DisplayName ='NVP liquid OD for 12 weeks' where Name = 'NVP for 12 weeks';
Update LookupItem set Name = 'AZT liquid BID + NVP liquid OD for 6 weeks then NVP liquid OD for 6 weeks', 
DisplayName ='AZT + NVP 6 weeks, then NVP 6 weeks after complete cessation of Breastfeeding' where Name = 'NVP for 6 weeks';

update LookupMasterItem set DisplayName='AZT + NVP 6 weeks, then NVP 6 weeks after complete cessation of Breastfeeding' where LookupItemId=(Select Id from LookupItem where Name ='AZT liquid BID + NVP liquid OD for 6 weeks then NVP liquid OD for 6 weeks')
update LookupMasterItem set DisplayName='NVP liquid OD for 12 weeks' where LookupItemId=(Select Id from LookupItem where Name ='NVP liquid OD for 12 weeks')


--------ARVProphylaxis
-- master
If Not Exists(Select 1 From LookupMaster where Name='ARVProphylaxis') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('ARVProphylaxis','ARVProphylaxis',0); End

-- lookupitem
If Not Exists(Select 1 From LookupItem where Name='AZT liquid BID for 12 weeks') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('AZT liquid BID for 12 weeks','AZT liquid BID for 12 weeks',0); End

-- LookupMasterItem
If Not Exists(Select 1 From LookupMasterItem where LookupMasterId=(SELECT TOP 1 Id FROM LookupMaster WHERE Name='ARVProphylaxis') and LookupItemId=(SELECT TOP 1 Id FROM LookupItem WHERE Name='AZT liquid BID for 12 weeks')) Begin  Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank)VALUES((SELECT TOP 1 Id FROM LookupMaster WHERE Name='ARVProphylaxis'),(SELECT TOP 1 Id FROM LookupItem WHERE Name='AZT liquid BID for 12 weeks'),'AZT liquid BID for 12 weeks',5) end;