IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientOutcome]') AND type in (N'U')) 
BEGIN
CREATE TABLE [dbo].[PatientOutcome]
(
	[OutcomeId] [int] NOT NULL,
	[PatientEncounterId] [int] NULL,
	[PatientMasterVisitID] [int] NULL,
	[DateOfOutcome] [datetime] NULL,
	[OutcomeStatus] [int] NULL,
	[OutcomeDescription] [varchar](max) NULL,
	[DeleteFlag] [bit] NULL,

 CONSTRAINT [FK_PatientOutcome_PatientEncounter] FOREIGN KEY([PatientEncounterId]) REFERENCES [dbo].[PatientEncounter] ([Id]),
 CONSTRAINT [FK_PatientOutcome_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitID]) REFERENCES [dbo].[PatientMasterVisit] ([Id])
)
END
