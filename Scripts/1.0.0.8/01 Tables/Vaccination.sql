
/*ALTER TABLE [dbo].[Vaccination]
ADD [AppointmentId] [int] NULL,
	[PeriodId] [int] NULL
	[VaccineStageId] [int] NULL
GO
*/



IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'VaccineStageId'
          AND Object_ID = Object_ID(N'Vaccination'))
BEGIN
ALTER  TABLE [dbo].[Vaccination] ADD VaccineStageId INT NULL
END

GO

IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'PeriodId'
          AND Object_ID = Object_ID(N'Vaccination'))
BEGIN
ALTER  TABLE [dbo].[Vaccination] ADD PeriodId INT NULL
END


GO


IF NOT EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'AppointmentId'
          AND Object_ID = Object_ID(N'Vaccination'))
BEGIN
ALTER  TABLE [dbo].[Vaccination] ADD AppointmentId INT NULL
END

