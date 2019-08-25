delete from lnk_FacilityModule where ModuleID NOT IN(
select lf.ModuleID from mst_module M
inner join lnk_FacilityModule lf on lf.ModuleID = M.ModuleID
inner join mst_Facility mf on mf.FacilityID = lf.FacilityID
where ModuleName in ('CCC Patient Card MoH 257','PM/SCM With Same point dispense','PM/SCM'));