IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Vaccination]') 
         AND (name = 'AppointmentId' or name = 'periodid' )
)
begin
ALTER TABLE [dbo].[Vaccination]
ADD [AppointmentId] [int] NULL,
	[PeriodId] [int] NULL
end
