



IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[PatientDiagnosis]') 
         AND name = 'LookupTableFlag'
)
BEGIN
ALTER TABLE PatientDiagnosis ADD LookupTableFlag   bit  null
END
go



IF  EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[PatientDiagnosis]') 
         AND name = 'LookupTableFlag'
)
BEGIN
update PatientDiagnosis set LookupTableFlag='1'  
where LookupTableFlag is null
END