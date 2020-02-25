IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_InsertFacility_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_InsertFacility_Constella]  
GO

/****** Object:  StoredProcedure [dbo].[pr_Admin_InsertFacility_Constella]    Script Date: 12/10/2018 8:35:42 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[pr_Admin_InsertFacility_Constella]                              
(                              
 @FacilityName varchar(50),                              
 @CountryID varchar(10),                              
 @PosID varchar(10),                              
 @SatelliteID varchar(10),    
 @NationalID varchar(10),    
 @ProvinceId int,            
 @DistrictId int,                              
 @Image varchar(50),                              
 @Currency varchar(50),                              
 @AppGracePeriod int,                              
 @DateFormat varchar(20),                              
 @PepFarStartDate datetime,                
 @SystemId int,            
 @Preferred int,           
 @Paperless int,        
 @UserID int,  
 @FacilityLogo varchar(50),                              
 @FacilityAddress varchar(200),    
 @FacilityTel varchar(50),    
 @FacilityCell varchar(50),            
 @FacilityFax varchar(50),                              
 @FacilityEmail varchar(50),                              
 @FacilityURL varchar(50),                              
 @FacilityFooter varchar(500),                              
 @FacilityTemplate int,
 @StrongPassword int,
 @ExpirePaswordFlag int,
 @DosageFrequency int,
 @SystemQueue int,
 @ExpirePaswordDays varchar(50),
 @PartialDispense int   
                        
)                              
AS                              
                      
begin                      
                              
if exists(select * from mst_Facility where FacilityName = @FacilityName)                              
     return 0                    
                              
if exists(select * from mst_facility where convert(int,CountryId) = convert(int,@CountryId) and convert(int,DistrictId) = convert(int,@DistrictId) and     
          convert(int,NationalId) = convert(int,@NationalID) and convert(int,ProvinceId) = convert(int,@ProvinceId) and              
          convert(int,PosID) = convert(int,@PosID) and convert(int,SatelliteId) = convert(int,@SatelliteId))                    
     return 0                    
                         
INSERT INTO [mst_Facility] ([FacilityName], [CountryID], [PosID], [SatelliteID], [NationalId], [ProvinceId], [DistrictId], [Image],     
[Currency], [AppGracePeriod], [DateFormat],[PepFarStartDate],[DeleteFlag],[SystemId],[Preferred], [Paperless], [UserID],[CreateDate],  
[FacilityAddress],[FacilityTel],[FacilityCell],[FacilityFax],[FacilityEmail],[FacilityURL],[FacilityFooter],[FacilityTemplate],[FacilityLogo],
[StrongPassFlag],[ExpPwdFlag],[ExpPwdDays],[Frequency],[SystemQueue],[PartialDispense])                           
VALUES (@FacilityName, @CountryID, @PosID, @SatelliteID,@NationalID,@ProvinceId,@DistrictId, @Image, @Currency, @AppGracePeriod,     
@DateFormat,@PepFarStartDate,0,@SystemId,@Preferred,@Paperless, @UserID,getdate(),@FacilityAddress,@FacilityTel,@FacilityCell,@FacilityFax,@FacilityEmail,                              
 @FacilityURL,@FacilityFooter,@FacilityTemplate,@FacilityLogo,@StrongPassword,@ExpirePaswordFlag
 ,@ExpirePaswordDays,@DosageFrequency,@SystemQueue,@PartialDispense)                          
                      
if @Image != ''                      
   update mst_facility set image = @Image                      
                      
end
GO


