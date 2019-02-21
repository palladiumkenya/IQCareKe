IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[PatientIptOutcome]') 
         AND (name = 'IPTOutComeDate')
)
begin
ALTER TABLE [dbo].[PatientIptOutcome]
ADD [IPTOutComeDate] [DATETIME] NULL
	
end


