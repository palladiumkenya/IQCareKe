EXECUTE sp_msforeachtable 'ALTER TABLE ? disable trigger ALL'
Go
If Not Exists(Select 1 From LookupItem where Name='N') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('NORMAL','Normal',0); End
If Not Exists(Select 1 From LookupItem where Name='O') Begin INSERT INTO LookupItem (Name, DisplayName, DeleteFlag) VALUES ('OBESE','Overweight/Obese',0); End


Exec LookupMasterItem_Insert @ItemName=N'FT' ,@MasterName=N'ARTRefillModel' ,@OrdRank=1.00, @DisplayName=N'FT = Fast Track'
Go
Exec LookupMasterItem_Insert @ItemName=N'CADH' ,@MasterName=N'ARTRefillModel' ,@OrdRank=1.01, @DisplayName=N'CADH = Community ART Distribution - HCW Led'
Go
Exec LookupMasterItem_Insert @ItemName=N'CADP' ,@MasterName=N'ARTRefillModel' ,@OrdRank=1.02, @DisplayName=N'CADP = Community ART Distribution - Peer Led'
Go
Exec LookupMasterItem_Insert @ItemName=N'FADG' ,@MasterName=N'ARTRefillModel' ,@OrdRank=1.02, @DisplayName=N'FADG = Facility ART Distribution Group'
Go



if not exists(select 1 from mst_code where name = 'ServiceRegisteredForAtPharmacy')
begin
insert into mst_code values('ServiceRegisteredForAtPharmacy',0,1,getdate(),null)
insert into mst_decode values('Hepatitis B',ident_current('mst_code'),1,0,0,1,getdate(),null,0,null,null)
insert into mst_decode values('PEP',ident_current('mst_code'),2,0,0,1,getdate(),null,0,null,null)
insert into mst_decode values('Goldstar',ident_current('mst_code'),3,0,0,1,getdate(),null,0,null,null)
end