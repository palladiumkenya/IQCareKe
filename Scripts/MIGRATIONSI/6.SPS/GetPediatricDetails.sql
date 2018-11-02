IF   EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Pharmacy_GetPediatricDetails_Constella]') AND type in (N'P', N'PC'))
BEGIN
EXEC('
ALTER PROCEDURE [dbo].[pr_Pharmacy_GetPediatricDetails_Constella]                                                                                                    
@PatientID int,                                                              
@password varchar(50)                                                                                                      
As                                                                                                    
Begin                                                                                                    
Declare @SymKey varchar(400)                                                                          
Set @SymKey = ''Open symmetric key Key_CTC decryption by password=''+ @password + ''''                                                                              
exec(@SymKey)    

Declare @Drug Table(Drug_Pk int, DrugName varchar(max), GenericAbbreviation varchar(max), GenericCommaSeparated varchar(max),DrugTypeId int);
Insert Into @Drug(Drug_Pk,DrugName,GenericAbbreviation,GenericCommaSeparated,DrugTypeId)
Select	a.drug_pk,
		a.drugname [FixedDrug],
		dbo.fn_Drug_Abbrev_Constella(a.drug_pk) [GenericAbbrevation],
		dbo.fn_GetDrugGenericCommaSeprated(a.drug_pk) GenericCommaSeparated,
		dbo.fn_GetDrugTypeId_futures(a.drug_pk) [drugtypeid]
From mst_drug a
Where a.deleteflag = 0
Order By a.drugname   ;                                                                     
                                                              
--0                                                                                                   
Select	m.drug_pk,
		m.drugname,
		drugtypeid,
		m.GenericAbbreviation [GenericAbbrevation],
		''0'' [genericid],
		m.GenericCommaSeparated [genericname]
From @Drug m      where 1 > 1                                                                                           
--1                                                                                                   
               
select distinct d.Drug_pk[GenericId],b.StrengthId,b.StrengthName                                                                                                                       
from lnk_drugstrength a,mst_strength b,@Drug d                                                                                                                                              
where a.strengthid = b.strengthid and d.Drug_pk=a.DrugID                                                                                                                      
order by d.Drug_pk,b.StrengthId,b.StrengthName                                                                                                     
--2                                   
select distinct d.Drug_pk[GenericId],a.FrequencyId,b.Name [FrequencyName]                                     
from lnk_DrugFrequency a,mst_frequency b,@Drug d                                       
where a.frequencyid = b.id and d.Drug_pk=a.DrugID                                       
order by d.Drug_pk,a.FrequencyId                                                                                                        
--3                                                                                                     
select  '''' Name,''''EmployeeId  where 1 > 1--from  mst_Employee  where designationid in (1,2)order by FirstName                                                                                                        
--4   
select a.GenericId,a.GenericName,a.GenericAbbrevation,b.DrugTypeId,a.DeleteFlag                                                
from mst_generic a,lnk_drugtypegeneric b                                                                                                        
where a.genericid = b.genericid                                                                    
order by GenericName;                                                                                                       
--5   
Select '''' Name, ''''   EmployeeId  where 1 > 1                                                                                                       
--6                                                                                                     
Select                                                               
(convert(varchar(50), decryptbykey(firstname))                                       
+ '' ''+                          
convert(varchar(50), decryptbykey(lastname)))[Name],                                                                          
PatientEnrollmentID,PatientClinicID,DOB, Convert(varchar, datediff(month,DOB,getdate())/12)[Age],                                                               
Convert(varchar, datediff(month,DOB,getdate())%12)[Age1],                                                                
CountryId +''-''+PosId+''-''+SatelliteId+''-''+PatientEnrollmentId [PatientID]                                                
from mst_patient where ptn_pk=@PatientID                                                                        
--7                                                        
select Id [UnitId],Name [UnitName] from mst_decode where codeid = 32 and deleteflag = 0                                                                                                    
--8                                                                                               
select Id [FrequencyId],Name [FrequencyName],multiplier  from mst_frequency where deleteflag = 0                                                       
--9                                                                                                     
select DrugId,GenericId,MaxDose,MinDose from lnk_nonarvdruggeneric where deleteflag = 0                                                              
--10                                                                                               
select Ptn_Pk,Moduleid,StartDate from dbo.lnk_patientprogramstart where ptn_pk = @PatientId                                        
--11                                                                                                     
exec dbo.pr_Admin_SelectTreatmentProgram_Constella                                                                                             
--12  
Select	m.Drug_Pk,
		m.DrugName,
		m.DrugTypeId,
		m.GenericAbbreviation [GenericAbbrevation],
		''0'' [genericid],
		m.GenericCommaSeparated [genericname]
From @Drug m;
                                                                                  
--13                                                                                      
select Id [FrequencyId],Name [FrequencyName]  from mst_FrequencyUnits where deleteflag = 0                                                                                      
--14                                                                                      
SELECT ID,name,DeleteFlag from mst_Provider where DeleteFlag=0 order by SRNO asc                                                                                    
--15                                                                             
select Id [UnitId],Name [UnitName] from mst_decode where codeid = 32                                                                                                    
--16                                                                                
SELECT  ID, Name, DeleteFlag from mst_Provider order by SRNO asc                                                                                
--17                                                                            
 
Select Distinct	m.drug_pk,
				m.drugname,
				m.DrugTypeId,
				''0'' [genericid],
				max(m.GenericAbbreviation) [GenericAbbrevation],
				lnkstr.StrengthId,
				isnull(convert(varchar, sum(st.Quantity)), 0) [Stock]
From @Drug m
	Join lnk_DrugStrength lnkstr On lnkstr.DrugId = m.Drug_pk
	Left Outer Join dtl_stocktransaction st On m.Drug_pk = st.ItemId
Group By	m.Drug_pk,
			m.Drugname,
			drugtypeid,
			lnkstr.StrengthId
Order By m.drugname                               
                                
--18      
select  distinct d.Drug_pk,c.StrengthId,c.StrengthName                                  
from lnk_DrugStrength a,mst_Strength c,@Drug d                            
where a.DrugId=d.Drug_pk                        
and a.StrengthId=c.StrengthId                                                                  
                                                              
--19 
select  distinct d.Drug_pk,c.ID as FrequencyId,c.Name as FrequencyName                                                                                            
from lnk_Drugfrequency a,mst_Frequency c,@Drug d                                                                                            
where a.DrugId=d.Drug_pk                                                                                            
and a.FrequencyId=c.ID    
--And d.DeleteFlag =0                                                                   
                                                                      
--20                                                                        
------for inactive generics 
select  null GenericId,null GenericName,null GenericAbbrevation,null DrugTypeId    where 1 > 1                                            
                                                                   
                                                                        
--21                                                                  
select  VisitDate from ord_Visit where VisitType=3 and Ptn_pk=@PatientId                                                   
Close symmetric key Key_CTC                                                              
                                                          
--22 period taken                               
select ID,Name from mst_decode where codeID=(select CodeID from mst_code where Name=''Pharmacy Period Taken'') and (DeleteFlag=0 or Deleteflag is null)                                                           
 --23 TB Regimen                                                        
select distinct r.ID,r.SRNo[TBRegimenID],r.Name,r.TreatmentTime,r.userID,l.GenericID,g.GenericName,(Case r.deleteflag when 0 then ''Active'' when 1 then ''In-Active'' end) [Status] from mst_TBregimen r                                 
 inner join lnk_TBRegimenGeneric l on l.TBRegimenID=r.ID                                          
 inner join mst_Generic g on l.GenericID=g.GenericID                                                                          
 where r.deleteflag=0                                                     
                                                  
--24                                                                                      
Select Top 1 VisitType, VisitDate,Visit_Id from ord_Visit where ptn_pk=@PatientID and VisitType = 11 order by VisitDate desc     
--Select null VisitType, null VisitDate, null Visit_Id --from ord_Visit where ptn_pk=@PatientID and VisitType = 11 order by VisitDate desc      
--25                                      
select a.ID, a.Name, b.DrugId from mst_drugschedule a inner join lnk_drugschedule b on a.ID=b.ScheduleId                  
                                    
--26                                    
select ID,Name, DeleteFlag, SRNO from dbo.Mst_RegimenLine order by SRNO                                      
--27
	declare @RegimeLine int ;
	Select Top (1) @RegimeLine = @RegimeLine
	From ord_patientpharmacyorder
	Where ptn_pk = @PatientID
		And DeleteFlag = 0
	Order By OrderedByDate Desc;
	Select @RegimeLine [RegimenLine]                                     
--28        
	Select	m.drug_pk,
			''0'' [genericid],
			m.DrugName,
			m.DrugTypeId,
			m.GenericAbbreviation [GenericAbbrevation]
	From @Drug m
	Order By m.drugname                              
--29    
	Select	Drug_Pk,
			drugname [FixedDrug],
			GenericAbbreviation [GenericAbbrevation],
			DrugTypeId [drugtypeid]
	From @Drug;  
--30  
Select Top 1 PV.Height, OV.VisitDate from dtl_patientvitals PV inner join ord_visit OV on PV.Visit_pk=OV.Visit_Id  
where PV.ptn_pk=@PatientID and PV.Height IS NOT NULL order by  OV.VisitDate desc                
    
--31  
Select Top 1 PV.Weight,PV.Height, OV.VisitDate from dtl_patientvitals PV inner join ord_visit OV on PV.Visit_pk=OV.Visit_Id  
where PV.ptn_pk=@PatientID and PV.Weight IS NOT NULL and OV.VisitDate between dateadd(day, -7, getdate()) and getdate() order by OV.VisitDate desc      
                                                 
end



')

END

IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[pr_Pharmacy_GetPediatricDetails_Constella]') AND type in (N'P', N'PC'))
BEGIN
EXEC('
CREATE PROCEDURE [dbo].[pr_Pharmacy_GetPediatricDetails_Constella]                                                                                                    
@PatientID int,                                                              
@password varchar(50)                                                                                                      
As                                                                                                    
Begin                                                                                                    
Declare @SymKey varchar(400)                                                                          
Set @SymKey = ''Open symmetric key Key_CTC decryption by password=''+ @password + ''''                                                                              
exec(@SymKey)    

Declare @Drug Table(Drug_Pk int, DrugName varchar(max), GenericAbbreviation varchar(max), GenericCommaSeparated varchar(max),DrugTypeId int);
Insert Into @Drug(Drug_Pk,DrugName,GenericAbbreviation,GenericCommaSeparated,DrugTypeId)
Select	a.drug_pk,
		a.drugname [FixedDrug],
		dbo.fn_Drug_Abbrev_Constella(a.drug_pk) [GenericAbbrevation],
		dbo.fn_GetDrugGenericCommaSeprated(a.drug_pk) GenericCommaSeparated,
		dbo.fn_GetDrugTypeId_futures(a.drug_pk) [drugtypeid]
From mst_drug a
Where a.deleteflag = 0
Order By a.drugname   ;                                                                     
                                                              
--0                                                                                                   
Select	m.drug_pk,
		m.drugname,
		drugtypeid,
		m.GenericAbbreviation [GenericAbbrevation],
		''0'' [genericid],
		m.GenericCommaSeparated [genericname]
From @Drug m      where 1 > 1                                                                                           
--1                                                                                                   
               
select distinct d.Drug_pk[GenericId],b.StrengthId,b.StrengthName                                                                                                                       
from lnk_drugstrength a,mst_strength b,@Drug d                                                                                                                                              
where a.strengthid = b.strengthid and d.Drug_pk=a.DrugID                                                                                                                      
order by d.Drug_pk,b.StrengthId,b.StrengthName                                                                                                     
--2                                   
select distinct d.Drug_pk[GenericId],a.FrequencyId,b.Name [FrequencyName]                                     
from lnk_DrugFrequency a,mst_frequency b,@Drug d                                       
where a.frequencyid = b.id and d.Drug_pk=a.DrugID                                       
order by d.Drug_pk,a.FrequencyId                                                                                                        
--3                                                                                                     
select  '''' Name,''''EmployeeId  where 1 > 1--from  mst_Employee  where designationid in (1,2)order by FirstName                                                                                                        
--4   
select a.GenericId,a.GenericName,a.GenericAbbrevation,b.DrugTypeId,a.DeleteFlag                                                
from mst_generic a,lnk_drugtypegeneric b                                                                                                        
where a.genericid = b.genericid                                                                    
order by GenericName;                                                                                                       
--5   
Select '''' Name, ''''   EmployeeId  where 1 > 1                                                                                                       
--6                                                                                                     
Select                                                               
(convert(varchar(50), decryptbykey(firstname))                                       
+ '' ''+                          
convert(varchar(50), decryptbykey(lastname)))[Name],                                                                          
PatientEnrollmentID,PatientClinicID,DOB, Convert(varchar, datediff(month,DOB,getdate())/12)[Age],                                                               
Convert(varchar, datediff(month,DOB,getdate())%12)[Age1],                                                                
CountryId +''-''+PosId+''-''+SatelliteId+''-''+PatientEnrollmentId [PatientID]                                                
from mst_patient where ptn_pk=@PatientID                                                                        
--7                                                        
select Id [UnitId],Name [UnitName] from mst_decode where codeid = 32 and deleteflag = 0                                                                                                    
--8                                                                                               
select Id [FrequencyId],Name [FrequencyName],multiplier  from mst_frequency where deleteflag = 0                                                       
--9                                                                                                     
select DrugId,GenericId,MaxDose,MinDose from lnk_nonarvdruggeneric where deleteflag = 0                                                              
--10                                                                                               
select Ptn_Pk,Moduleid,StartDate from dbo.lnk_patientprogramstart where ptn_pk = @PatientId                                        
--11                                                                                                     
exec dbo.pr_Admin_SelectTreatmentProgram_Constella                                                                                             
--12  
Select	m.Drug_Pk,
		m.DrugName,
		m.DrugTypeId,
		m.GenericAbbreviation [GenericAbbrevation],
		''0'' [genericid],
		m.GenericCommaSeparated [genericname]
From @Drug m;
                                                                                  
--13                                                                                      
select Id [FrequencyId],Name [FrequencyName]  from mst_FrequencyUnits where deleteflag = 0                                                                                      
--14                                                                                      
SELECT ID,name,DeleteFlag from mst_Provider where DeleteFlag=0 order by SRNO asc                                                                                    
--15                                                                             
select Id [UnitId],Name [UnitName] from mst_decode where codeid = 32                                                                                                    
--16                                                                                
SELECT  ID, Name, DeleteFlag from mst_Provider order by SRNO asc                                                                                
--17                                                                            
 
Select Distinct	m.drug_pk,
				m.drugname,
				m.DrugTypeId,
				''0'' [genericid],
				max(m.GenericAbbreviation) [GenericAbbrevation],
				lnkstr.StrengthId,
				isnull(convert(varchar, sum(st.Quantity)), 0) [Stock]
From @Drug m
	Join lnk_DrugStrength lnkstr On lnkstr.DrugId = m.Drug_pk
	Left Outer Join dtl_stocktransaction st On m.Drug_pk = st.ItemId
Group By	m.Drug_pk,
			m.Drugname,
			drugtypeid,
			lnkstr.StrengthId
Order By m.drugname                               
                                
--18      
select  distinct d.Drug_pk,c.StrengthId,c.StrengthName                                  
from lnk_DrugStrength a,mst_Strength c,@Drug d                            
where a.DrugId=d.Drug_pk                        
and a.StrengthId=c.StrengthId                                                                  
                                                              
--19 
select  distinct d.Drug_pk,c.ID as FrequencyId,c.Name as FrequencyName                                                                                            
from lnk_Drugfrequency a,mst_Frequency c,@Drug d                                                                                            
where a.DrugId=d.Drug_pk                                                                                            
and a.FrequencyId=c.ID    
--And d.DeleteFlag =0                                                                   
                                                                      
--20                                                                        
------for inactive generics 
select  null GenericId,null GenericName,null GenericAbbrevation,null DrugTypeId    where 1 > 1                                            
                                                                   
                                                                        
--21                                                                  
select  VisitDate from ord_Visit where VisitType=3 and Ptn_pk=@PatientId                                                   
Close symmetric key Key_CTC                                                              
                                                          
--22 period taken                               
select ID,Name from mst_decode where codeID=(select CodeID from mst_code where Name=''Pharmacy Period Taken'') and (DeleteFlag=0 or Deleteflag is null)                                                           
 --23 TB Regimen                                                        
select distinct r.ID,r.SRNo[TBRegimenID],r.Name,r.TreatmentTime,r.userID,l.GenericID,g.GenericName,(Case r.deleteflag when 0 then ''Active'' when 1 then ''In-Active'' end) [Status] from mst_TBregimen r                                 
 inner join lnk_TBRegimenGeneric l on l.TBRegimenID=r.ID                                          
 inner join mst_Generic g on l.GenericID=g.GenericID                                                                          
 where r.deleteflag=0                                                     
                                                  
--24                                                                                      
Select Top 1 VisitType, VisitDate,Visit_Id from ord_Visit where ptn_pk=@PatientID and VisitType = 11 order by VisitDate desc     
--Select null VisitType, null VisitDate, null Visit_Id --from ord_Visit where ptn_pk=@PatientID and VisitType = 11 order by VisitDate desc      
--25                                      
select a.ID, a.Name, b.DrugId from mst_drugschedule a inner join lnk_drugschedule b on a.ID=b.ScheduleId                  
                                    
--26                                    
select ID,Name, DeleteFlag, SRNO from dbo.Mst_RegimenLine order by SRNO                                      
--27
	declare @RegimeLine int ;
	Select Top (1) @RegimeLine = @RegimeLine
	From ord_patientpharmacyorder
	Where ptn_pk = @PatientID
		And DeleteFlag = 0
	Order By OrderedByDate Desc;
	Select @RegimeLine [RegimenLine]                                     
--28        
	Select	m.drug_pk,
			''0'' [genericid],
			m.DrugName,
			m.DrugTypeId,
			m.GenericAbbreviation [GenericAbbrevation]
	From @Drug m
	Order By m.drugname                              
--29    
	Select	Drug_Pk,
			drugname [FixedDrug],
			GenericAbbreviation [GenericAbbrevation],
			DrugTypeId [drugtypeid]
	From @Drug;  
--30  
Select Top 1 PV.Height, OV.VisitDate from dtl_patientvitals PV inner join ord_visit OV on PV.Visit_pk=OV.Visit_Id  
where PV.ptn_pk=@PatientID and PV.Height IS NOT NULL order by  OV.VisitDate desc                
    
--31  
Select Top 1 PV.Weight,PV.Height, OV.VisitDate from dtl_patientvitals PV inner join ord_visit OV on PV.Visit_pk=OV.Visit_Id  
where PV.ptn_pk=@PatientID and PV.Weight IS NOT NULL and OV.VisitDate between dateadd(day, -7, getdate()) and getdate() order by OV.VisitDate desc      
                                                 
end



')

END