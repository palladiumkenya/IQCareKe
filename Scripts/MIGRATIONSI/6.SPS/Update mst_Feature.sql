
update mst_Module set DeleteFlag=0 where ModuleName='PM/SCM'

update mst_Module set Status= 2 where ModuleName='PM/SCM'



update mst_feature set featuretypeid = (select top 1 id from mst_decode where name = 'Module Actions') where featurename='Drug Dispense'

 IF NOT EXISTS (
 select  * from lnk_FacilityModule fm inner join (select * from mst_Facility 
 where DeleteFlag=0)mf on mf.FacilityID=fm.FacilityID
 inner join mst_module md on md.ModuleID=fm.ModuleID
 where md.ModuleName='PM/SCM')
 BEGIN
 insert into lnk_FacilityModule(FacilityId,ModuleID,UserID,CreateDate) values((select top 1.FacilityId from mst_Facility 
 where DeleteFlag=0),(select  ModuleId from mst_module where ModuleName='PM/SCM'),1,GetDate())
 END
