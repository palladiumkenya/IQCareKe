/****** Object:  StoredProcedure [dbo].[pr_Report_PatientBluecart]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Report_PatientBluecart]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Report_PatientBluecart]
GO
/****** Object:  StoredProcedure [dbo].[pr_Reports_GetKenyaMOHCard_Futures]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Reports_GetKenyaMOHCard_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_Reports_GetKenyaMOHCard_Futures]
GO
/****** Object:  StoredProcedure [dbo].[pr_QueryBuilderReportSaveUpdate_Futures]    Script Date: 12/11/2014 16:16:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_QueryBuilderReportSaveUpdate_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_QueryBuilderReportSaveUpdate_Futures]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_SaveCustomReports_Futures]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[pr_SaveCustomReports_Futures]
GO
/****** Object:  StoredProcedure [dbo].[pr_Report_PatientBluecart]    Script Date: 12/11/2014 16:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Report_PatientBluecart]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'--VW_PatientClinicalEncounter
    
 --exec [pr_Report_PatientBluecart]   37
    
--exec [pr_Report_PatientBluecart] 17   
--,''2011-03-11''    
    
CREATE procedure [dbo].[pr_Report_PatientBluecart]    
@ptn_pk int    
--@visitdate datetime     
as begin    
    
--declare @ptn_pk int    
--declare @visitdate datetime     
--set @visitdate =''2011-03-11''-- 00:00:00.000'' 2011-03-11 00:00:00.000--Initial Evaluation  --ART Follow-Up    
--declare @VisitType varchar --int    
--set  @ptn_pk =624-- 257  --253    
--declare @qry varchar(max)    
declare @Visit_id int    
    
----select  @Visit_id=Visit_Id from   ord_visit where Ptn_Pk=@ptn_pk     
------and  visitdate =@visitdate     
----and VisitType in (1,2,3)and DeleteFlag is  null    
    
--select @Visit_id= Visit_id from dbo.VW_PatientClinicalEncounter where patienthivdisease is not null and  ptn_pk =@ptn_pk    
-- and visitdate =@visitdate    
--group by visitdate,[Visit Type],ptn_pk,Visit_id    
    
    
select Visit_id,convert(varchar, visitdate,106)as visitdate,[Visit Type],ptn_pk     
,DateHIVDiagnosis,HIVDiagnosisVerified,Discloused,[Pregnancy Status],LMP,convert(varchar,[Pregnancy EDD],106)as [Pregnancy EDD],Temp,RR,HR,
convert(varchar ,BPSystolic) + ''/''  + convert(varchar,BPDiastolic) as [BP]
,Height,Weight,Pain    
,WABStage,WHOStage,ClinicalNotes,TherapyPlan,TherapyChangeReason    
,[Adherence_Missed Last_month],Adherence_Missed_other_reason    
,Adherence_Dot_per_week,Adherence_home_visit_per_weeks,Adherence_support_Group,Adherence_Interrupted_date,Adherence_Interrupted_Num_days    
,Adherence_stopped_date,Adherence_stopped_num_days,Adherence_HerbalsMeds    
,Adherence_Reason,[Patient ART Restart],    
convert(varchar,AppDate,106) as AppDate,[Appointment Reason],[Appointment Status],HistoricalART ,convert(varchar ,HistoricalARTStDate,106) as HistoricalARTStDate    
,PrevARVExposure,PrevNVPExposure,PrevNVPDate1,PrevNVPDate2,PrevARVRegimen1Name,PrevARVRegimen1Months,PrevARVRegimen2Name,PrevARVRegimen2Months    
,PrevARVRegimen3Name,PrevARVRegimen3Months,PrevARVRegimen4Name,PrevARVRegimen4Months,PreviousARVRegimen,PrevLowestCD4,PrevLowestCD4Percent,PrevLowestCD4Date    
,PrevMostRecentCD4,PrevMostRecentCD4Percent,PrevMostRecentCD4Date,CD4PriorStARV,CD4PriorStARVPercent,CD4PriorStARVDate,PreTherapyVL,PreTherapyVLDate,    
longTermMedsSulfa,longTermMedsSulfaDesc    
,longTermMedsOther1,longTermMedsOther2,longTermTBMed,longTermTBMedDesc    
,longTermTBStartDate,longTermMedsOther1Desc,longTermMedsOther2Desc,longTermMedsOther3desc    
 from dbo.VW_PatientClinicalEncounter where patienthivdisease is not null and  ptn_pk =@ptn_pk    
-- and visitdate =@visitdate    
group by visitdate,[Visit Type],ptn_pk,Visit_id    
,PrevARVExposure,PrevNVPExposure,PrevNVPDate1,PrevNVPDate2,PrevARVRegimen1Name,PrevARVRegimen1Months,PrevARVRegimen2Name,PrevARVRegimen2Months    
,PrevARVRegimen3Name,PrevARVRegimen3Months,PrevARVRegimen4Name,PrevARVRegimen4Months,PreviousARVRegimen,PrevLowestCD4,PrevLowestCD4Percent,PrevLowestCD4Date    
,PrevMostRecentCD4,PrevMostRecentCD4Percent,PrevMostRecentCD4Date,CD4PriorStARV,CD4PriorStARVPercent,CD4PriorStARVDate,PreTherapyVL,PreTherapyVLDate    
,HistoricalART,HistoricalARTStDate    
,longTermMedsSulfa,longTermMedsSulfaDesc    
,longTermMedsOther1,longTermMedsOther2,longTermTBMed,longTermTBMedDesc    
,longTermTBStartDate,longTermMedsOther1Desc,longTermMedsOther2Desc,longTermMedsOther3desc    
,DateHIVDiagnosis,HIVDiagnosisVerified,Discloused,[Pregnancy Status],LMP,[Pregnancy EDD],Temp,RR,HR
--BPDiastolic,BPSystolic
,convert(varchar ,BPSystolic) + ''/''  + convert(varchar,BPDiastolic),Height,Weight,Pain    
,WABStage,WHOStage,ClinicalNotes,TherapyPlan,TherapyChangeReason    
,[Adherence_Missed Last_month],Adherence_Missed_other_reason    
,Adherence_Dot_per_week,Adherence_home_visit_per_weeks,Adherence_support_Group,Adherence_Interrupted_date,Adherence_Interrupted_Num_days    
,Adherence_stopped_date,Adherence_stopped_num_days,Adherence_HerbalsMeds    
,Adherence_Reason,[Patient ART Restart]    
,AppDate,[Appointment Reason],[Appointment Status]    
order by visitdate asc    
    
    
    
---------------Disclosure-----------    
select a.Ptn_pk ,a.Visit_pk ,a.LocationID,a.DisclosureID , b.Name as [DisclosureTo]  from dtl_patientdisclosure a      
inner join mst_HIVDisclosure b on a.DisclosureID=b.id     
where a.ptn_pk =@ptn_pk    
--and a.Visit_pk =@Visit_id    
group by a.Ptn_pk ,a.Visit_pk ,a.LocationID,a.DisclosureID,b.Name    
    
---------------disease-------------------    
select a.Ptn_pk ,a.Visit_pk ,a.LocationID, a.Disease_Pk, b.Name as [patientdisease] ,a.DateOfDisease,a.DiseaseDesc    
 from  dtl_patientdisease a inner join  mst_HivDisease b on a.Disease_Pk =b.ID    
where a.ptn_pk =@ptn_pk     
--and a.Visit_pk =@Visit_id    
group by a.Ptn_pk ,a.Visit_pk ,a.LocationID,a.Disease_Pk,b.Name,a.DateOfDisease,a.DiseaseDesc    
    
---------------Assessment---------------------    
select a.Ptn_pk ,a.Visit_pk ,a.LocationID,a.AssessmentID ,b.AssessmentName,a.Description1,a.Description2       
from  dtl_patientassessment a  inner join mst_assessment b on a.AssessmentID =b.AssessmentID    
where a.ptn_pk =@ptn_pk    
--and a.Visit_pk =@Visit_id    
group by a.Ptn_pk ,a.Visit_pk ,a.LocationID,a.AssessmentID ,b.AssessmentName ,a.Description1,a.Description2       
    
----------------[Adherence]------------------    
select a.Ptn_pk ,a.Visit_pk ,a.LocationID,b.Name  [Adherence_Missed_Reason] ,a.Other_Desc  from  dtl_Adherence_Missed_Reason  a inner join mst_reason b    
on a.missedReasonid= b.id  where a.ptn_pk =@ptn_pk    
--and a.Visit_pk =@Visit_id    
group by a.Ptn_pk ,a.Visit_pk ,a.LocationID,b.Name,a.Other_Desc    
    
    
----------------[Allergy]---------------    
select  a.Ptn_pk ,a.Visit_pk ,a.LocationID,b.Name as [Allergy],a.AllergyID,a.AllergyNameOther  from dtl_patientallergy a inner join mst_decode b on a.AllergyID =b.ID    
where a.ptn_pk =@ptn_pk    
--and a.Visit_pk =@Visit_id    
 group by a.Ptn_pk ,a.Visit_pk ,a.LocationID,b.Name,a.AllergyID,a.AllergyNameOther    
    
--------[symptom]-----------    
select  a.Ptn_pk ,a.Visit_pk ,a.LocationID,b.Name as [symptom],a.SymptomDescription from dtl_patientsymptoms a inner join mst_symptom b on     
a.SymptomID =b.ID where a.ptn_pk =@ptn_pk    
--and a.Visit_pk =@Visit_id    
 group by a.Ptn_pk ,a.Visit_pk ,a.LocationID,b.Name,a.SymptomDescription   

--------Patient info-----
select * from mst_patient where Ptn_Pk =@ptn_pk 
    
    
end
' 
END
GO
/****** Object:  StoredProcedure [dbo].[pr_Reports_GetKenyaMOHCard_Futures]    Script Date: 12/11/2014 16:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Reports_GetKenyaMOHCard_Futures]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[pr_Reports_GetKenyaMOHCard_Futures]                        
@Ptn_Pk int,                        
@Key varchar(100)                        
as                        
Begin                        
   Declare @SymKey varchar(400)                          
   Set @SymKey = ''Open symmetric key Key_CTC decryption by password=''+ @Key + ''''                                                                    
   Exec(@SymKey)                         
                        
 ---0 patientinfo                       
 select a.[Patient Location][FacilityName],a.PatientClinicID [PatientClinicNumber], b.PatientEnrollmentID [UniquePatNumber],                        
 convert(varchar(100),decryptbykey(a.FirstName))[FirstName],convert(varchar(100),decryptbyKey(a.LastName)) [LastName],                        
 a.DOB [DOB], a.Age,a.Gender,convert(varchar(500),decryptbykey(a.Address)) [Address],                        
 convert(varchar(500),decryptbykey(a.Phone)) [TelContact],a.District,  a.Division [SubLocation],a.Village [Location],                        
 a.landmark [LandMark],c.Name [MaritalStatus],a.ptn_pk                         
 from dbo.vw_patientDetail a inner join mst_Patient b on a.Ptn_Pk = b.Ptn_pk                         
 left Outer join mst_decode c on b.MaritalStatus = c.Id                        
 where a.ptn_pk = @Ptn_Pk and a.ModuleId = 203                       
                        
 ---1  EmergContact                      
 Select convert(varchar(150),decryptbykey(a.EmergContactName)) [TSName],b.Name [TSRelation],convert(varchar(150),decryptbykey(EmergContactPhone)) [TSPhone],                  
 convert(varchar(150),decryptbykey(EmergContactAddress)) [TSAddress]                        
 from dtl_PatientContacts a Left Outer Join mst_decode b on a.EmergContactRelation = b.Id                        
 Inner Join Ord_Visit c on a.Ptn_pk = c.Ptn_Pk and a.VisitId = c.Visit_Id                        
 where c.visittype = 12 and c.ptn_pk = @Ptn_Pk                        
                        
 ---2  Referred         
 select b.Name [ReferredFrom], d.Name [ReferredFacilityName], c.Transfered, c.[Transfered From Facility],                        
 case c.Transfered when ''1'' then c.[Registration Date] else null end [TransferInDate], c.[ARTStartDate]  as ARTStartDate                      
 from mst_Patient a left Outer Join Mst_Decode b on a.ReferredFrom = b.Id                        
 Inner Join vw_PatientDetail c on a.ptn_pk = c.ptn_pk                         
 Left Outer Join mst_Lptf d on d.Id = a.referredFromSpecify                        
 where a.Ptn_pk = @Ptn_Pk and c.ModuleId = 203                     
                        
 ----3  PrevARV                      
 Select PrevARVExposure, CurrentART [PrevARV1], CurrentArtStartDate [PrevARV1dtUsed],                        
 PrevARVRegimen1Name [PrevARV2],CurrentARTStartDate -1 [PrevARV2dtUsed],                        
 PrevARVRegimen2Name [PrevARV3],(CurrentARTStartDate - (30*PrevARVRegimen1Months+1)) [PrevARV3dtUsed]                        
 from dtl_PatientHivPrevCareIE where ptn_pk = @Ptn_Pk       
 ---4  HIVDiagnosis                      
 select a.ConfirmHIVPosDate [DtConfirmHIVPositive],b.[Registration Date] [dtEnrolledHIVCare],                        
 d.Name [WHOStage]                        
 from dbo.[dtl_PatientHivPrevCareEnrollment] a Left Outer join dbo.vw_patientdetail b on a.Ptn_Pk = b.Ptn_Pk                        
 Inner Join Ord_Visit Ord on b.Ptn_Pk = Ord.Ptn_Pk and a.Visit_Pk = Ord.Visit_Id                                                 
 Left Outer Join dtl_PatientStage e on b.Ptn_pk = e.Ptn_pk and e.Visit_Pk = Ord.Visit_Id                        
 Left Outer Join Mst_Decode d on e.whoStage = d.Id                        
 where Ord.VisitType = 18 and b.ModuleId = 203 and b.ptn_pk = @Ptn_Pk                        
                      
 ---5 patientAllergy                      
                       
select SUBSTRING((SELECT '','' + Case replace((Convert(Varchar(100),c.Name)),''Allergy'','''') when ''Other'' then ''Other-''+AllergyNameOther else                   
 replace((Convert(Varchar(100),c.Name)),''Allergy'','''') end                   
 from dtl_patientAllergy a inner join ord_visit b                        
 on a.ptn_pk = b.ptn_pk and a.visit_pk = b.visit_id                        
 Left Outer Join mst_Decode c on a.allergyid = c.id                        
 where b.visittype = 18 and b.ptn_pk = @Ptn_Pk and c.Name is not null FOR XML PATH('''')),2,200000)                        
 [Allergy]                        
                        
 ----6  FamilyInfo                      
 select  top 5 convert(varchar(100),decryptbykey(a.RFirstName))+'' ''+convert(varchar(100),decryptbykey(a.RLastName)) [RName],                        
 a.AgeYear,c.Name [Relation],d.Name [HIVStatus],Case isnull(a.ReferenceId,0) when 0 then ''No'' else ''Yes'' end [InCare],                        
 (select IQNumber from Mst_Patient where Ptn_pk = a.ReferenceId) [CCCNumber]                        
 from dbo.dtl_FamilyInfo a Inner Join Mst_Patient b on a.Ptn_Pk = b.Ptn_Pk                        
 Inner Join Mst_RelationshipType c on a.RelationshipType = c.Id                       
 Left Outer Join Mst_Decode d on a.HivStatus = d.Id                        
 where b.Ptn_Pk = @Ptn_Pk                          
                        
 -----7  patientlabresults                      
 Select                       
 (select TestResults [CD4Count] from dbo.dtl_patientlabresults a inner join                         
 dbo.ord_PatientLabOrder b on a.LabId = b.LabId                        
 where a.LabTestId = 1 and a.ParameterId = 1 and b.Ptn_pk = @Ptn_Pk and b.ReportedByDate <=                        
 (select  top 1 VisitDate from dbo.ord_visit where ptn_pk = @Ptn_Pk and visittype in (18,19) order by visitdate asc))[CD4Count],                        
 (select a.Name from dbo.Mst_Decode a Left Outer Join dbo.dtl_PatientStage b on a.Id = b.WHOStage                        
 where b.Ptn_Pk = @Ptn_Pk and b.Visit_Pk in                         
 (select  top 1 Visit_Id from dbo.ord_visit where ptn_pk = @Ptn_Pk and visittype in (18,19) order by visitdate asc))[WHOStage],                        
 a.Weight,a.Height, convert(decimal(18,2),Round((Nullif(a.Weight,0)/(Nullif(a.height/100,0)*Nullif(a.height/100,0))),2)) [BMI]                        
 from dtl_patientvitals a inner join ord_visit b on a.visit_pk = b.visit_Id and a.Ptn_Pk = b.Ptn_Pk                        
 where b.visit_Id in                         
 (select  top 1 Visit_Id from dbo.ord_visit where ptn_pk = @Ptn_Pk and visittype in (18,19) and               
  visitdate <= (select artstartdate from mst_patient where ptn_pk = @Ptn_Pk) order by visitdate desc)                        
                        
 -----8  firstgegimen                      
select   top 3 case when [DispensedByDate]  IS NULL then min(a.OrderedByDate) else min(a.DispensedByDate) end as [RegDate],b.RegimenType from dbo.Ord_PatientPharmacyOrder a                         
 Inner Join dtl_regimenmap b on a.Ptn_Pharmacy_Pk = b.OrderId and a.Ptn_Pk = b.Ptn_Pk                        
where a.Ptn_Pk = @Ptn_Pk              
and (len(rtrim(ltrim(b.RegimenType))) >0)              
 group by b.RegimenType ,a.DispensedByDate order by a.DispensedByDate desc     
 -----9  IEFuinfopervisit                      
declare @Visit_id int                              
                             
select Visit_id, visitdate as visitdate,[Visit Type],ptn_pk                               
,DateHIVDiagnosis,HIVDiagnosisVerified,Discloused,[Pregnancy Status],LMP,[Pregnancy EDD] as [Pregnancy EDD],Temp,RR,HR,                          
convert(varchar ,BPSystolic) + ''/''  + convert(varchar,BPDiastolic) as [BP]                          
,Height,Weight,Pain,   convert(decimal(18,2),Round((Nullif(Weight,0)/(Nullif(height/100,0)*Nullif(height/100,0))),2)) [BMI]                            
,WABStage,WHOStage,ClinicalNotes,TherapyPlan,TherapyChangeReason  ,[Adherence_Missed Last Week]                            
,[Adherence_Missed Last_month],Adherence_Missed_other_reason                              
,Adherence_Dot_per_week,Adherence_home_visit_per_weeks,Adherence_support_Group,Adherence_Interrupted_date,Adherence_Interrupted_Num_days                              
,Adherence_stopped_date,Adherence_stopped_num_days,Adherence_HerbalsMeds                              
,Adherence_Reason,[Patient ART Restart],                              
AppDate as AppDate,[Appointment Reason],[Appointment Status],HistoricalART ,HistoricalARTStDate as HistoricalARTStDate                              
,PrevARVExposure,PrevNVPExposure,PrevNVPDate1,PrevNVPDate2,PrevARVRegimen1Name,PrevARVRegimen1Months,PrevARVRegimen2Name,PrevARVRegimen2Months                              
,PrevARVRegimen3Name,PrevARVRegimen3Months,PrevARVRegimen4Name,PrevARVRegimen4Months,PreviousARVRegimen,PrevLowestCD4,PrevLowestCD4Percent,PrevLowestCD4Date                              
,PrevMostRecentCD4,PrevMostRecentCD4Percent,PrevMostRecentCD4Date,CD4PriorStARV,CD4PriorStARVPercent,CD4PriorStARVDate,PreTherapyVL,PreTherapyVLDate,                              
longTermMedsSulfa,longTermMedsSulfaDesc                              
,longTermMedsOther1,longTermMedsOther2,longTermTBMed,longTermTBMedDesc                              
,longTermTBStartDate,longTermMedsOther1Desc,longTermMedsOther2Desc,longTermMedsOther3desc                              
 from dbo.VW_PatientClinicalEncounter where      
-- patienthivdisease is not null and      
  ptn_pk =@ptn_pk                              
-- and visitdate =@visitdate                              
group by visitdate,[Visit Type] ,ptn_pk,Visit_id                              
,PrevARVExposure,PrevNVPExposure,PrevNVPDate1,PrevNVPDate2,PrevARVRegimen1Name,PrevARVRegimen1Months,PrevARVRegimen2Name,PrevARVRegimen2Months                              
,PrevARVRegimen3Name,PrevARVRegimen3Months,PrevARVRegimen4Name,PrevARVRegimen4Months,PreviousARVRegimen,PrevLowestCD4,PrevLowestCD4Percent,PrevLowestCD4Date                              
,PrevMostRecentCD4,PrevMostRecentCD4Percent,PrevMostRecentCD4Date,CD4PriorStARV,CD4PriorStARVPercent,CD4PriorStARVDate,PreTherapyVL,PreTherapyVLDate                              
,HistoricalART,HistoricalARTStDate                              
,longTermMedsSulfa,longTermMedsSulfaDesc                              
,longTermMedsOther1,longTermMedsOther2,longTermTBMed,longTermTBMedDesc                              
,longTermTBStartDate,longTermMedsOther1Desc,longTermMedsOther2Desc,longTermMedsOther3desc                              
,DateHIVDiagnosis,HIVDiagnosisVerified,Discloused,[Pregnancy Status],LMP,[Pregnancy EDD],Temp,RR,HR                          
--BPDiastolic,BPSystolic                          
,convert(varchar ,BPSystolic) + ''/''  + convert(varchar,BPDiastolic),Height,Weight,Pain                              
,WABStage,WHOStage,ClinicalNotes,TherapyPlan,TherapyChangeReason,[Adherence_Missed Last Week]                              
,[Adherence_Missed Last_month],Adherence_Missed_other_reason                              
,Adherence_Dot_per_week,Adherence_home_visit_per_weeks,Adherence_support_Group,Adherence_Interrupted_date,Adherence_Interrupted_Num_days                              
,Adherence_stopped_date,Adherence_stopped_num_days,Adherence_HerbalsMeds                              
,Adherence_Reason,[Patient ART Restart]                              
,AppDate,[Appointment Reason],[Appointment Status]                              
order by visitdate asc  ,[Visit Type] desc 
---------------Disclosure-----------                              
                                  
select                 
(select SUBSTRING((SELECT '','' +replace((Convert(Varchar(100),b.Name)),''DisclosureTo '','''')                   
 from dtl_patientdisclosure  a inner join  mst_HIVDisclosure b on a.DisclosureID=b.id              
 Left Outer Join Ord_Visit c on a.Visit_PK = c.Visit_Id                              
where a.ptn_pk =@ptn_pk and c.visittype in (12,18)                
group by b.Name FOR XML PATH('''')),2,200000)) as DisclosureTo              
                          
---------------disease  NewOisOtherProblems-------------------                                          
                  
--select Visit_Pk ,                  
--(select SUBSTRING((SELECT '','' +replace((Convert(Varchar(100),b.Name)),''patientdisease'','''')                   
-- from dtl_patientdisease  a inner join  mst_HivDisease b on a.Disease_Pk =b.ID                              
--where a.ptn_pk =@ptn_pk   and Disease_Pk  not in (99,94,97) and Visit_Pk = d.Visit_Pk                                     
--group by b.Name FOR XML PATH('''')),2,200000)    ) as patientdisease from dtl_patientdisease  d                   
--group by Visit_Pk                  
--                            
 select Visit_Pk ,                  
(select SUBSTRING((SELECT '','' +replace((Convert(Varchar(100),b.Name)),''patientdisease'','''')                   
 from dtl_patientdisease  a inner join  mst_HivDisease b on a.Disease_Pk =b.ID                              
where a.ptn_pk =@ptn_pk   and Disease_Pk  not in (99,94,97) and Visit_Pk = d.Visit_Pk                                     
group by b.Name FOR XML PATH('''')),2,200000)    ) as patientdisease from dtl_patientdisease  d  
where  d.Ptn_Pk = @ptn_pk              
group by Visit_Pk           
                  
----------------[CotrimoxazoleAdherence]]---------------   
select d.Visit_Id,d.visitdate, a.GenericName [genericname]                
from MST_GENERIC  a Left Outer Join dbo.dtl_PatientPharmacyOrder b on a.GenericID = b.GenericID                
inner join dbo.Ord_PatientPharmacyOrder c  on b.Ptn_Pharmacy_Pk = c.Ptn_Pharmacy_Pk                
Left Outer Join dbo.Ord_Visit d on c.Ptn_pk = d.Ptn_pk and c.DispensedByDate = d.VisitDate                
Left Outer Join dbo.Vw_PatientDetail e on d.Ptn_pk = e.ptn_pk                 
where e.ModuleId = 203 and e.ptn_pk =@ptn_pk   and b.GenericID in (277,123)  and d.visittype in (17,18,19) and (c.deleteflag is null or c.deleteflag =0)               
                  
-----[INHdrug]                          
                  
  select d.Visit_Id,d.visitdate, a.DrugName [DrugName]                
from mst_drug  a Left Outer Join dbo.dtl_PatientPharmacyOrder b on a.Drug_pk = b.Drug_Pk                
inner join dbo.Ord_PatientPharmacyOrder c  on b.Ptn_Pharmacy_Pk = c.Ptn_Pharmacy_Pk                
Left Outer Join dbo.Ord_Visit d on c.Ptn_pk = d.Ptn_pk and c.DispensedByDate = d.VisitDate                
Left Outer Join dbo.Vw_PatientDetail e on d.Ptn_pk = e.ptn_pk                 
where e.ModuleId = 203 and e.ptn_pk =@ptn_pk  and  a.Drug_pk in (150)   and d.visittype in (17,18,19)and (c.deleteflag is null or c.deleteflag =0)   
union  
select  d.Visit_Id,d.visitdate, a.DrugName [DrugName]                
from mst_drug  a Left Outer Join dbo.dtl_PatientPharmacyOrderNonARV b on a.Drug_pk = b.Drug_Pk                
inner join dbo.Ord_PatientPharmacyOrder c  on b.Ptn_Pharmacy_Pk = c.Ptn_Pharmacy_Pk                
Left Outer Join dbo.Ord_Visit d on c.Ptn_pk = d.Ptn_pk and c.DispensedByDate = d.VisitDate                
Left Outer Join dbo.Vw_PatientDetail e on d.Ptn_pk = e.ptn_pk                 
where e.ModuleId = 203 and e.ptn_pk= @ptn_pk  and  a.Drug_pk in (150)    
 and d.visittype in (17,18,19)and (c.deleteflag is null or c.deleteflag =0)                
                       
                              
--------  [sideeffect]-----------                                          
                  
--select Visit_Pk ,                  
--(select SUBSTRING((SELECT '','' +replace((Convert(Varchar(150),b.Name)),''symptom'','''')                   
-- from dtl_patientsymptoms a inner join mst_symptom b on                               
--a.SymptomID =b.ID where a.ptn_pk= @ptn_pk    and Visit_Pk = d.Visit_Pk                                       
--group by b.Name FOR XML PATH('''')),2,200000)    ) as symptom from dtl_patientsymptoms  d                   
--group by Visit_Pk  

select Visit_Pk ,                  
(select SUBSTRING((SELECT '','' +replace((Convert(Varchar(150),b.Name)),''symptom'','''')                   
 from dtl_patientsymptoms a inner join mst_symptom b on                               
a.SymptomID =b.ID where a.ptn_pk= @ptn_pk    and Visit_Pk = d.Visit_Pk                                       
group by b.Name FOR XML PATH('''')),2,200000)    ) as symptom from dtl_patientsymptoms  d  
where d.Visit_Pk=@ptn_pk           
group by Visit_Pk                       
                  
--------Patientvisitinfo-----                   
               
            
select e.FirstName +'' '' +e.LastName as signature, Visit_Id,PatientEnrollmentID,IQNumber,b.VisitType ,case b.VisitType when 1 then c.CurrentARTstartdate else  a.ARTStartDate end [ARTStartDate]                      
from mst_patient a inner join ord_Visit b  on a.ptn_pk=b.ptn_pk                    
left outer join dtl_PatientHivPrevCareIE  c on a.ptn_pk= c.ptn_pk and b.Visit_Id=c.Visit_pk                    
left outer join dtl_patientappointment d on a.ptn_pk= d.ptn_pk and b.Visit_Id=d.Visit_pk                   
left outer join mst_employee e on d.EmployeeID=e.EmployeeID                  
where a.Ptn_Pk =@ptn_pk and b.VisitType in (17,18,19)                  
                  
--Tuberclulosis------                
select d.Visit_Id,d.visitdate,''Yes'' [TBStatus], a.Name [TBRegimen]                
from dbo.mst_tbregimen a Left Outer Join dbo.dtl_PatientPharmacyOrdernonarv b on a.Id = b.TB_RegimenId                
inner join dbo.Ord_PatientPharmacyOrder c  on b.Ptn_Pharmacy_Pk = c.Ptn_Pharmacy_Pk                
Left Outer Join dbo.Ord_Visit d on c.Ptn_pk = d.Ptn_pk and c.DispensedByDate = d.VisitDate                
Left Outer Join dbo.Vw_PatientDetail e on d.Ptn_pk = e.ptn_pk                 
where e.ModuleId = 203 and e.ptn_pk =@ptn_pk and d.visittype in (17,18,19) and (c.deleteflag is null or c.deleteflag =0)                
                
---otherMedication---                
                
select d.visit_Id,d.visitdate ,a.RegimenType [OtherMeds]         
from dbo.dtl_regimenMap a Left Outer Join dbo.Ord_PatientPharmacyOrder c on a.OrderId = c.Ptn_Pharmacy_Pk                
Left Outer Join dbo.Ord_Visit d on c.Ptn_pk = d.Ptn_pk and c.DispensedByDate = d.VisitDate                
Left Outer Join dbo.Vw_PatientDetail e on d.Ptn_pk = e.ptn_pk                 
where e.ModuleId = 203 and e.ptn_pk = @ptn_pk and d.visittype in (17,18,19) and (c.deleteflag is null or c.deleteflag =0)                
                
-----labinvestigation                
--select d.visit_Id,d.visitdate,a.LabTestId,a.ParameterId,convert(varchar,a.TestResults)[TestResults]                
--from dbo.dtl_patientlabresults a  Inner Join dbo.Ord_PatientLabOrder c on a.LabId = c.LabId                
--Left Outer Join dbo.Ord_Visit d on c.Ptn_pk = d.Ptn_pk and c.ReportedByDate = d.VisitDate                
--Left Outer Join dbo.Vw_PatientDetail e on d.Ptn_pk = e.ptn_pk                 
--where e.ModuleId = 203 and e.ptn_pk = @ptn_pk and d.visittype in (1,2,3) and (c.deleteflag is null or c.deleteflag =0)                
--and ParameterId in (1,6)                
--union                
--select d.visit_Id,d.visitdate,a.LabTestId,a.ParameterId,case a.TestResults when 1 then ''Positive'' else ''Negative'' end [TestResults]                
--from dbo.dtl_patientlabresults a  Inner Join dbo.Ord_PatientLabOrder c on a.LabId = c.LabId                
--Left Outer Join dbo.Ord_Visit d on c.Ptn_pk = d.Ptn_pk and c.ReportedByDate = d.VisitDate                
--Left Outer Join dbo.Vw_PatientDetail e on d.Ptn_pk = e.ptn_pk                 
--where e.ModuleId = 203 and e.ptn_pk =@ptn_pk and d.visittype in (1,2,3) and (c.deleteflag is null or c.deleteflag =0)                
--and ParameterId in (75,17,18,19)      
select d.visit_Id,d.visitdate,a.LabTestId,a.ParameterId,convert(varchar,a.TestResults)[TestResults]     
,f.SubTestName               
from dbo.dtl_patientlabresults a  Inner Join dbo.Ord_PatientLabOrder c on a.LabId = c.LabId                
Left Outer Join dbo.Ord_Visit d on c.Ptn_pk = d.Ptn_pk and c.ReportedByDate = d.VisitDate                
Left Outer Join dbo.Vw_PatientDetail e on d.Ptn_pk = e.ptn_pk       
left outer join lnk_testparameter f on a.parameterid = f.subtestid            
where e.ModuleId = 203 and e.ptn_pk =@ptn_pk and d.visittype in (17,18,19) and (c.deleteflag is null or c.deleteflag =0)                
and a.ParameterId in (1,6)                
union                
select d.visit_Id,d.visitdate,a.LabTestId,a.ParameterId,case a.TestResults when 1 then ''Positive'' else ''Negative'' end [TestResults]    
 ,f.SubTestName                
from dbo.dtl_patientlabresults a  Inner Join dbo.Ord_PatientLabOrder c on a.LabId = c.LabId                
Left Outer Join dbo.Ord_Visit d on c.Ptn_pk = d.Ptn_pk and c.ReportedByDate = d.VisitDate                
Left Outer Join dbo.Vw_PatientDetail e on d.Ptn_pk = e.ptn_pk      
left outer join lnk_testparameter f on a.parameterid = f.subtestid      
                  
where e.ModuleId = 203 and e.ptn_pk =@ptn_pk and d.visittype in (17,18,19) and (c.deleteflag is null or c.deleteflag =0)                
and a.ParameterId in (75,17,18,19)    
                
----------------[ArvdrugAdherence]------------------    
--select Visit_Pk ,                  
--(select SUBSTRING((SELECT '','' +replace((Convert(Varchar(150),b.Name)),''Adherence_Missed_Reason'','''')                   
-- from  dtl_Adherence_Missed_Reason  a inner join mst_reason b                              
--on a.missedReasonid= b.id  where a.ptn_pk =@ptn_pk and a.Visit_Pk = d.Visit_Pk and  b.Name is not null                                          
--group by b.Name FOR XML PATH('''')),2,200000)    ) as [Adherence_Missed_Reason] from dtl_Adherence_Missed_Reason  d                   
--group by Visit_Pk  

select Visit_Pk ,                  
(select SUBSTRING((SELECT '','' +replace((Convert(Varchar(150),b.Name)),''Adherence_Missed_Reason'','''')                   
 from  dtl_Adherence_Missed_Reason  a inner join mst_reason b                              
on a.missedReasonid= b.id  where a.ptn_pk =@ptn_pk and a.Visit_Pk = d.Visit_Pk and  b.Name is not null                                          
group by b.Name FOR XML PATH('''')),2,200000)    ) as [Adherence_Missed_Reason] from dtl_Adherence_Missed_Reason  d   
where    d.Visit_Pk=@ptn_pk             
group by Visit_Pk               
              
--[HIVTest]              
select case when a.NumHouseHoldHIVTest> 0 then ''Yes'' else ''No'' end [PartnerTested] from dtl_patienthivother a Left Outer Join               
Ord_Visit b on a.visit_pk = b.visit_id  and b.ptn_pk =a.Ptn_pk             
where    a.numhouseholdhivtest is not null and b.Visittype = 0 and a.Ptn_pk  =  @ptn_pk            
              
                  
 close symmetric key Key_CTC                        
                         
end
' 
END
GO

/****** Object:  StoredProcedure [dbo].[pr_QueryBuilderReportSaveUpdate_Futures]    Script Date: 12/11/2014 16:16:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_QueryBuilderReportSaveUpdate_Futures]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE Proc [dbo].[pr_QueryBuilderReportSaveUpdate_Futures]                
(      
           
@CategoryName varchar(200),                
@ReportName varchar(300),               
@ReportQuery varchar(8000),         
@UserID int                    
)                
AS                
BEGIN                
         
declare @CategoryId int        
 IF not exists(select CategoryName from Mst_QueryBuilderCategory  where CategoryName = @CategoryName and DeleteFlag=0)           
   BEGIN                
     INSERT INTO Mst_QueryBuilderCategory(CategoryName,DeleteFlag,UserId,CreateDate)                
     VALUES(@CategoryName,0,@UserID,getdate())                
            
   END 
  else                                     
    begin
    raiserror(''Duplicate Category Name.Try Again.'', 16, 1)                    
        
    end
     
   SELECT @CategoryID=ident_current(''Mst_QueryBuilderCategory'')    
 IF exists(select ReportId from mst_querybuilderreports a,mst_querybuildercategory b where a.CategoryId = b.CategoryId       
        and a.ReportName = @ReportName and b.categoryname = @CategoryName and a.DeleteFlag=0)            
    begin        
       Update mst_QueryBuilderReports Set ReportQuery=@ReportQuery,UserID=@UserID,UpdateDate=getdate() where ReportName = @ReportName        
    end        
 else        
    begin        
       insert into mst_QueryBuilderReports(ReportName,CategoryId,ReportQuery,DeleteFlag,UserID,CreateDate)      
       Values(@ReportName,@CategoryId,@ReportQuery,0,@UserID,getdate())        
    end         
End
' 
END
GO

/****** Object:  StoredProcedure [dbo].[pr_SaveCustomReports_Futures]    Script Date: 12/08/2015 12:49:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[pr_SaveCustomReports_Futures]              
@ReportId int = 0,              
@CategoryId int = 0,              
@CategoryName varchar(200),              
@ReportName varchar(300),              
@ReportQuery varchar(8000),              
@UserId int   ,
@ParamXML xml    = null       
As              
Begin              

declare @RepId int  , @CatId int;    
If (@ReportId = 0 and @CategoryId > 0)    
Begin    
	 If Exists (Select *	From dbo.mst_querybuildercategory a, dbo.Mst_QueryBuilderReports b
		Where a.CategoryName = @CategoryName
		And b.ReportName = @ReportName And a.CategoryId = b.CategoryId)    
	 Begin    
	  Raiserror('Category Name and Report Name Exists',16,1);    
	  Return    
	 End    
	 Else If Exists (Select *	From dbo.Mst_QueryBuilderReports	Where ReportQuery = @ReportQuery)    
	 Begin    
	  Raiserror('Query already Exists',16,1);    
	  Return;    
	 End
	Begin Transaction I
	Begin Try
					
		Insert Into dbo.mst_QueryBuilderReports (
			CategoryId,
			ReportName,
			ReportQuery,
			DeleteFlag,
			UserId,
			CreateDate)		
		Values (
			@CategoryId,
			@ReportName,
			@ReportQuery,
			0,
			@UserId,
			Getdate())
		Set @RepId = scope_identity();
		
		If(@ParamXML Is Not Null)
		Begin
			Insert Into dbo.MST_QueryBuilderParameters(ReportID,ParameterName,ParameterDataType)
			Select
				@RepId,
				P.V.value('name[1]','varchar(50)') [Name],
				P.V.value('basetype[1]','varchar(50)') [DataType]
			From @ParamXML.nodes('/parameters/parameter') As P(V);
		End
		
		
	End Try
	Begin Catch
		If @@TRANCOUNT > 0         Rollback Transaction I;
	End Catch;
	If @@TRANCOUNT > 0		Commit Transaction I;
		
	
              
              
End     
       
Else If (@ReportId > 0 and @CategoryId > 0)              
Begin    
    If Exists (Select *	From dbo.mst_querybuildercategory Where CategoryName = @CategoryName)
    Begin
		Begin Transaction O
		Begin Try
			
			Delete From dbo.MST_QueryBuilderParameters Where ReportID = @ReportId;
			
			Select @CatId = CategoryId	From dbo.mst_querybuildercategory		Where CategoryName = @CategoryName;
			
			Update dbo.mst_QueryBuilderReports Set
				ReportName	= @ReportName,
				CategoryId	= @CatId,
				ReportQuery = @ReportQuery,
				UserId		= @UserId,
				UpdateDate	= Getdate()
			Where ReportId	= @ReportId;
			
			Set @RepId = @ReportId;
			
			If(@ParamXML Is Not Null)
			Begin
				Insert Into dbo.MST_QueryBuilderParameters(ReportID,ParameterName,ParameterDataType)
				Select
					@RepId,
					P.V.value('name[1]','varchar(50)') [Name],
					P.V.value('basetype[1]','varchar(50)') [DataType]
				From @ParamXML.nodes('/parameters/parameter') As P(V);
			End
		End Try
		Begin Catch
			If @@TRANCOUNT > 0         Rollback Transaction O;
		End Catch;
		If @@TRANCOUNT > 0			Commit Transaction O;          
	End
  
End              
Else If(@CategoryId > 0 or @CategoryName <>'')              
Begin
	Begin Transaction U
		Begin Try
			Insert Into dbo.Mst_QueryBuilderCategory (
				CategoryName,
				DeleteFlag,
				UserId,
				CreateDate)
			Values (
				@CategoryName,
				0,
				@UserId,
				Getdate());
				
			Set @CatId = scope_identity();

			Insert Into dbo.Mst_QueryBuilderReports (
				CategoryId,
				ReportName,
				ReportQuery,
				DeleteFlag,
				UserId,
				CreateDate)
			Values (
				@CatId,
				@ReportName,
				@ReportQuery,
				0,
				@UserId,
				Getdate());
				
			Set @RepId = scope_identity();

			If(@ParamXML Is Not Null)
			Begin
				Insert Into dbo.MST_QueryBuilderParameters(ReportID,ParameterName,ParameterDataType)
				Select
					@RepId,
					P.V.value('name[1]','varchar(50)') [Name],
					P.V.value('basetype[1]','varchar(50)') [DataType]
				From @ParamXML.nodes('/parameters/parameter') As P(V);
			End
		End Try
		Begin Catch
			If @@TRANCOUNT > 0         Rollback Transaction U;
		End Catch;
		If @@TRANCOUNT > 0			Commit Transaction U; 
          
End

Set @CatId = @CategoryId;

Select	@RepId,
		@CatId,
		@ReportName,
		@CategoryName;
              
End

GO


