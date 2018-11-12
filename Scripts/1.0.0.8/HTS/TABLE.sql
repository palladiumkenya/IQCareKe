IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Person]') 
         AND name = 'NickName'
)
BEGIN
ALTER TABLE Person ADD NickName   varbinary(800) null
END



IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[PersonEducation]') 
         AND name = 'EducationOutcome'
)
BEGIN
ALTER TABLE PersonEducation ADD  EducationOutcome  int null
END

