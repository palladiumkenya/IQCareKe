IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientCounselling]') AND type in (N'U')) 
BEGIN
CREATE TABLE [dbo].[PatientCounselling](
	[ConsellingId] [int] NOT NULL,
	[PatientEncounterId] [int] NOT NULL,
	[PatientMasterVisitId] [int] NOT NULL,
	[CounsellingTopicId] [int] NULL,
	[CounsellingDate] [datetime] NULL,
	[Description] [varchar](max) NULL,
	[DeleteFlag] [bit] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[AuditData] [varchar](max) NULL,
	CONSTRAINT [FK_PatientCounselling_PatientEncounter] FOREIGN KEY([PatientEncounterId]) REFERENCES [dbo].[PatientEncounter] ([Id]),
    CONSTRAINT [FK_PatientCounselling_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitId]) REFERENCES [dbo].[PatientMasterVisit] ([Id])
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END