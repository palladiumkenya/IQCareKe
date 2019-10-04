IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_GetFacilityList_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_GetFacilityList_Constella]
GO

/****** Object:  StoredProcedure [dbo].[pr_Admin_GetFacilityList_Constella]    Script Date: 12/10/2018 7:41:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[pr_Admin_GetFacilityList_Constella]                                  
@SystemId int,                                  
@FeatureId int,          
@moduleid int = null                                  
as                                  
begin                                  
   select FacilityId,FacilityName,CountryId,PosId,SatelliteID,NationalId,ProvinceId,DistrictId,Currency,    
   AppGracePeriod,Image,PepFarStartDate,SystemId,(Case deleteflag when 0 then 'Active' when 1 then 'In-Active' end) [Status],                            
   isnull((Case Preferred when 0 then 'No' when 1 then 'Yes' end),'No') [Preferred],                      
   isnull((Case Paperless when 0 then 'No' when 1 then 'Yes' end),'No') [Paperless],  
 [FacilityAddress],[FacilityTel],[FacilityCell],[FacilityFax],[FacilityEmail],[FacilityURL],[FacilityFooter],[FacilityTemplate],[FacilityLogo],
 [StrongPassFlag],[ExpPwdFlag],[ExpPwdDays] ,Frequency,[SystemQueue]                 
   from mst_facility where (deleteflag is null or deleteflag = 0) order by FacilityName                  
--   select a.FacilityId, a.FacilityName,a.CountryId,a.PosId,a.SatelliteID,a.Currency, a.AppGracePeriod,a.Image,                                  
--   a.PepFarStartDate,a.SystemId,(Case a.deleteflag when 0 then 'Active' when 1 then 'In-Active' end) [Status],                            
--   isnull((Case a.Preferred when 0 then 'No' when 1 then 'Yes' end),'No') [Preferred],                      
--   isnull((Case a.Paperless when 0 then 'No' when 1 then 'Yes' end),'No') [Paperless], b.ModuleID                   
--   from mst_facility a left outer join lnk_facilitymodule b on a.facilityID=b.FacilityID                
--   where (a.deleteflag is null or a.deleteflag = 0) order by a.FacilityName                  
                
  exec dbo.pr_SystemAdmin_GetSystemBasedLabels_Constella @SystemId,@FeatureId, 0        
         select FacilityID, ModuleID from lnk_facilitymodule       
                       
                
end
GO


