IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Admin_UpdateFacility_Constella]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Admin_UpdateFacility_Constella]
GO

/****** Object:  StoredProcedure [dbo].[pr_Admin_UpdateFacility_Constella]    Script Date: 12/10/2018 8:24:22 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[pr_Admin_UpdateFacility_Constella]                                              
(                                              
 @FacilityName varchar(50),                                              
 @CountryID varchar(10),                                              
 @PosID Varchar(10),                                              
 @SatelliteID Varchar(10),    
 @NationalID varchar(10),    
 @ProvinceId int,            
 @DistrictId int,                                              
 @Image varchar(50),                                              
 @Currency varchar(50),                                              
 @AppGracePeriod int,                                              
 @DateFormat varchar(20),                                              
 @PepFarStartDate datetime,                              
 @Status int,                          
 @SystemId int,                 
 @Preferred int,            
 @Paperless int,          
 @UserID int,                                              
 @FacilityID int,  
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
 @ExpirePaswordDays varchar(50)                                            
)                                              
AS                                          
                                
begin                              
                                
if exists(select * from mst_Facility where FacilityName = @FacilityName and FacilityId <> @FacilityId)                                      
     return 0                            
                                      
if @Status = 1                    
   begin                    
     declare @PCount int                    
     select @PCount=count(ptn_pk) from mst_patient where (deleteflag = 0 or deleteflag is null) and                
     locationid = @FacilityId                    
     if @PCount > 0                   
        begin                   
   RAISERROR('Facility containing Patient records cannot be Inactivated.',11,1)                  
            return                  
        end                   
   end                            
                    
UPDATE [mst_Facility] SET [FacilityName] = @FacilityName, [CountryID] = @CountryID, [PosID] = @PosID,                                             
[SatelliteID] = @SatelliteID, [Currency] = @Currency, [AppGracePeriod] = @AppGracePeriod,    
[NationalID] = @NationalID, [ProvinceId] = @ProvinceId, [DistrictId] = @DistrictId,                                            
[DateFormat] = @DateFormat, [PepFarStartDate] = Nullif(@PepFarStartDate, '01-01-1900'), [DeleteFlag] = @Status,                        
[SystemId] = @SystemId,[Preferred] = @Preferred, [Paperless]=@Paperless, [UserID] = @UserID,[UpdateDate] = getdate()   
,[FacilityAddress] = @FacilityAddress,[FacilityTel] = @FacilityTel,[FacilityCell] = @FacilityCell,[FacilityFax] = @FacilityFax  
,[FacilityEmail] = @FacilityEmail,[FacilityURL] = @FacilityURL,[FacilityFooter] = @FacilityFooter,[FacilityTemplate] = @FacilityTemplate,[Frequency]=@DosageFrequency
,[FacilityLogo] = @FacilityLogo,[StrongPassFlag]=@StrongPassword,[ExpPwdFlag]=@ExpirePaswordFlag,[ExpPwdDays]=@ExpirePaswordDays  
WHERE ([FacilityID] = @FacilityID)                               
                              
if @Image != ''                              
   update mst_facility set image = @Image                              
                              
end
GO


