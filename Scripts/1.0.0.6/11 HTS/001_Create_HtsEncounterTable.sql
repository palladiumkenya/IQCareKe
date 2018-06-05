SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[HtsEncounter]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[HtsEncounter](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[PersonId] [int] NOT NULL,
		[ProviderId] [int] NOT NULL,
		[PatientEncounterID] [int] NOT NULL,
		[EverTested] [int] NOT NULL,
		[MonthsSinceLastTest] [int] NULL,
		[MonthSinceSelfTest] [int] NULL CONSTRAINT [DF__HtsEncoun__Month__0DD36488]  DEFAULT ((0)),
		[TestedAs] [int] NULL,
		[TestingStrategy] [int] NULL,
		[EncounterRemarks] [varchar](max) NULL,
		[FinalResultGiven] [int] NULL,
		[CoupleDiscordant] [int] NULL,
		[TestEntryPoint] [int] NOT NULL,
		[EverSelfTested] [int] NOT NULL,
		[GeoLocation] [varchar](200) NULL,
		[EncounterType] [int] NULL,
	 CONSTRAINT [PK__HtsEncou__3214EC073FDDE93A] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

END;
GO

ALTER TABLE [dbo].[HtsEncounter]  WITH CHECK ADD  CONSTRAINT [FK_HtsEncounter_PatientEncounterID_PatientEncounter_Id] FOREIGN KEY([PatientEncounterID])
REFERENCES [dbo].[PatientEncounter] ([Id])
GO

ALTER TABLE [dbo].[HtsEncounter] CHECK CONSTRAINT [FK_HtsEncounter_PatientEncounterID_PatientEncounter_Id]
GO

ALTER TABLE [dbo].[HtsEncounter]  WITH CHECK ADD  CONSTRAINT [FK_HtsEncounter_PersonId_Person_Id] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
GO

ALTER TABLE [dbo].[HtsEncounter] CHECK CONSTRAINT [FK_HtsEncounter_PersonId_Person_Id]
GO