
IF NOT EXISTS(select  * from LookupItemView where MasterName = 'PaedsFirstLineRegimen'
and ItemName='AF2E')
BEGIN
INSERT  LookupMasterItem (LookupMasterId,LookupItemId,DisplayName,OrdRank)
VALUES(
(select Id from LookupMaster where Name='PaedsFirstLineRegimen')
,(select Id  from LookupItem where  Name='AF2E'),(select  DisplayName from LookupItem where Name='AF2E') ,'15.00')
END