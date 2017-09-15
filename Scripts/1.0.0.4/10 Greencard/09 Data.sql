-- version changes
UPDATE AppAdmin
SET
	AppVer='Ver 1.0.0.4 Kenya HMIS',
	DBVer='Ver 1.0.0.4 Kenya HMIS',
	RelDate='01-Sep-2017',
	VersionName='Kenya HMIS Ver 1.0.0.4';

--Lookup master
If Not Exists(Select 1 From LookupMaster where Name='ARTRefillModel') Begin INSERT INTO LookupMaster (Name, DisplayName, DeleteFlag) VALUES ('ARTRefillModel','ART Refill Model',0); End

--Lookup items
If Not Exists(Select 1 From LookupItem where Name='FT') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('FT','FT = Fast Track',0); End
If Not Exists(Select 1 From LookupItem where Name='CADH') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('CADH','CADH = Community ART Distribution - HCW Led',0); End
If Not Exists(Select 1 From LookupItem where Name='CADP') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('CADP','CADP = Community ART Distribution - Peer Led',0); End
If Not Exists(Select 1 From LookupItem where Name='FADG') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('FADG','FADG = Facility ART Distribution Group',0); End

If Not Exists(Select 1 From LookupItem where Name='N') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('NORMAL','Normal',0); End
If Not Exists(Select 1 From LookupItem where Name='O') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('OBESE','Overweight/Obese',0); End

Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'FT = Fast Track',1 FROM( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='FT'  ) ItemId  FROM LookupMaster  WHERE Name='ARTRefillModel') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId )=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'CADH = Community ART Distribution - HCW Led',2 FROM( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='CADH'  ) ItemId  FROM LookupMaster  WHERE Name='ARTRefillModel') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId )=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'CADP = Community ART Distribution - Peer Led',3 FROM( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='CADP'  ) ItemId  FROM LookupMaster  WHERE Name='ARTRefillModel') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId )=0;
Insert Into LookupMasterItem(LookupMasterId ,LookupItemId,DisplayName, OrdRank) SELECT MasterId, ItemId,'FADG = Facility ART Distribution Group',4 FROM( SELECT Id MasterId, ( SELECT TOP 1 Id  FROM LookupItem   WHERE Name='FADG'  ) ItemId  FROM LookupMaster  WHERE Name='ARTRefillModel') X where (select count(*) from LookupMasterItem where lookupMasterId=x.MasterId and LookupItemId=x.ItemId )=0;


if not exists(select 1 from mst_code where name = 'ServiceRegisteredForAtPharmacy')
begin
insert into mst_code values('ServiceRegisteredForAtPharmacy',0,1,getdate(),null)
insert into mst_decode values('Hepatitis B',ident_current('mst_code'),1,0,0,1,getdate(),null,0,null,null)
insert into mst_decode values('PEP',ident_current('mst_code'),2,0,0,1,getdate(),null,0,null,null)
insert into mst_decode values('Goldstar',ident_current('mst_code'),3,0,0,1,getdate(),null,0,null,null)
end