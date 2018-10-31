



IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[PatientDiagnosis]') 
         AND name = 'LookupTableFlag'
)
BEGIN
ALTER TABLE PatientDiagnosis ADD LookupTableFlag   bit  null
END

--select  * from PatientDiagnosis

update PatientDiagnosis set LookupTableFlag='0'  
where LookupTableFlag is null