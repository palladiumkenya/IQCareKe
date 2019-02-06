IF NOT EXISTS (SELECT *
           FROM   sys.objects
           WHERE  object_id = OBJECT_ID(N'[dbo].[fn_GetAllergyDesc]')
                  AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))

BEGIN

EXEC ('CREATE function [dbo].[fn_GetAllergyDesc]    
(   
 @Id int ,  
 @Type int   
     
)    
returns varchar(20)    
as    
Begin    
 DECLARE @AllergyStatusdesc varchar(20)    
if (@Type = 1)  
 begin  
    select @AllergyStatusdesc=Name from mst_decode where id=@Id   
  end  
else  
begin  
    select @AllergyStatusdesc=Name from mst_code where codeid=@Id   
  end  
 return @AllergyStatusdesc   
end')

END







if  exists (select * from sys.procedures where name = 'pr_Clinical_GetAllergy')
BEGIN

EXEC('ALTER PROCEDURE [dbo].[pr_Clinical_GetAllergy] (
	@Ptn_pk INT = NULL
	
	)
AS
BEGIN
   
	select p.ptn_pk, b.displayname allergy, c.DisplayName reaction, 
	d.DisplayName severity, CONVERT(varchar(20),a.onsetdate,106) dateAllergy
	from patientallergy a inner join lookupitem b on a.allergen = b.Id
	left join lookupitem c on a.Reaction = c.id
	left join lookupitem d on a.severity = d.id
	left join  Patient p on  p.Id=a.PatientId
where p.ptn_pk = @Ptn_Pk and (a.DeleteFlag is null or a.DeleteFlag = 0)
	UNION  ALL

	SELECT 
			ptn_Pk
			
			,CASE 
				WHEN AllergyType IN (
						208
						,209
						)
					THEN dbo.fn_GetAllergyDesc(Allergen, 1)
				ELSE [Allergen]
				END AS [Allergy]
			      
            ,[TypeReaction] as Reactiom
			,dbo.fn_GetAllergyDesc(Severity, 1) [severity]
			,[DateAllergy] 
			
		FROM dtl_AllergiesDetails where ptn_pk=@Ptn_pk and (DeleteFlag=0 or DeleteFlag is null) 
END')

END

if NOT exists (select * from sys.procedures where name = 'pr_Clinical_GetAllergy')
BEGIN
EXEC('CREATE PROCEDURE [dbo].[pr_Clinical_GetAllergy] (
	@Ptn_pk INT = NULL
	
	)
AS
BEGIN
   
	select p.ptn_pk, b.displayname allergy, c.DisplayName reaction, 
	d.DisplayName severity, CONVERT(varchar(20),a.onsetdate,106) dateAllergy
	from patientallergy a inner join lookupitem b on a.allergen = b.Id
	left join lookupitem c on a.Reaction = c.id
	left join lookupitem d on a.severity = d.id
	left join  Patient p on  p.Id=a.PatientId
where p.ptn_pk = @Ptn_Pk and (a.DeleteFlag is null or a.DeleteFlag = 0)
	UNION  ALL

	SELECT 
			ptn_Pk
			
			,CASE 
				WHEN AllergyType IN (
						208
						,209
						)
					THEN dbo.fn_GetAllergyDesc(Allergen, 1)
				ELSE [Allergen]
				END AS [Allergy]
			      
            ,[TypeReaction] as Reactiom
			,dbo.fn_GetAllergyDesc(Severity, 1) [severity]
			,[DateAllergy] 
			
		FROM dtl_AllergiesDetails where ptn_pk=@Ptn_pk and (DeleteFlag=0 or DeleteFlag is null) 
END')
END

