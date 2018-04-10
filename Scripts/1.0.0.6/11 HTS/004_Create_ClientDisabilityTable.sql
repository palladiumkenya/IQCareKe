/****** Object:  Table [dbo].[ClientDisability]    Script Date: 21-Mar-18 9:57:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[ClientDisability]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[ClientDisability](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[PersonId] [int] NOT NULL,
		[PatientEncounterID] [int] NOT NULL,
		[DisabilityId] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY];

END;
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME='FK_ClientDisability_PatientEncounterID_PatientEncounter_Id' )
BEGIN
	ALTER TABLE [dbo].[ClientDisability]  WITH CHECK ADD  CONSTRAINT [FK_ClientDisability_PatientEncounterID_PatientEncounter_Id] FOREIGN KEY([PatientEncounterID])
	REFERENCES [dbo].[PatientEncounter] ([Id])	

	ALTER TABLE [dbo].[ClientDisability] CHECK CONSTRAINT [FK_ClientDisability_PatientEncounterID_PatientEncounter_Id]	

	ALTER TABLE [dbo].[ClientDisability]  WITH CHECK ADD  CONSTRAINT [FK_ClientDisability_PersonId_Person_Id] FOREIGN KEY([PersonId])
	REFERENCES [dbo].[Person] ([Id])	

	ALTER TABLE [dbo].[ClientDisability] CHECK CONSTRAINT [FK_ClientDisability_PersonId_Person_Id]
END;
GO