SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[HtsEncounterResult]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[HtsEncounterResult](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[HtsEncounterId] [int] NOT NULL,
		[RoundOneTestResult] [int] NOT NULL,
		[RoundTwoTestResult] [int] NULL,
		[FinalResult] [int] NULL,
	PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY];

END;
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME='FK_HtsEncounterResult_HtsEncounterId_HtsEncounter_Id' )
BEGIN
	ALTER TABLE [dbo].[HtsEncounterResult]  WITH CHECK ADD  CONSTRAINT [FK_HtsEncounterResult_HtsEncounterId_HtsEncounter_Id] FOREIGN KEY([HtsEncounterId])
	REFERENCES [dbo].[HtsEncounter] ([Id])

	ALTER TABLE [dbo].[HtsEncounterResult] CHECK CONSTRAINT [FK_HtsEncounterResult_HtsEncounterId_HtsEncounter_Id]
END;
GO