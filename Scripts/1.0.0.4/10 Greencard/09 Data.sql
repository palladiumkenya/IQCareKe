﻿-- version changes
UPDATE AppAdmin
SET
	AppVer='Ver 1.0.0.4 Kenya HMIS',
	DBVer='Ver 1.0.0.4 Kenya HMIS',
	RelDate='01-Sep-2017',
	VersionName='Kenya HMIS Ver 1.0.0.4'

If Not Exists(Select 1 From LookupItem where Name='N') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('NORMAL','Normal',0); End
If Not Exists(Select 1 From LookupItem where Name='O') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('OBESE','Overweight/Obese',0); End

if not exists(select 1 from mst_code where name = 'ServiceRegisteredForAtPharmacy')
begin
insert into mst_code values('ServiceRegisteredForAtPharmacy',0,1,getdate(),null)
insert into mst_decode values('Hepatitis B',ident_current('mst_code'),1,0,0,1,getdate(),null,0,null,null)
insert into mst_decode values('PEP',ident_current('mst_code'),2,0,0,1,getdate(),null,0,null,null)
insert into mst_decode values('Goldstar',ident_current('mst_code'),3,0,0,1,getdate(),null,0,null,null)
end