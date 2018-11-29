IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientMilestone]') AND type in (N'U')) 
BEGIN
CREATE TABLE [dbo].[PatientMilestone](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PatientMasterVisitId] [int] NOT NULL,
	[PatientId] [int] NOT NULL,
	[TypeAssessedId] [int] NULL,
	[AchievedId] [bit] NULL,
	[StatusId] [int] NULL,
	[Comment] [text] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[DeleteFlag] [bit] NOT NULL,
	[DateAssessed] [datetime] NULL,
 CONSTRAINT [PK_PatientMilestone] PRIMARY KEY CLUSTERED ([id] ASC),
CONSTRAINT [FK_PatientMilestone_Patient] FOREIGN KEY([PatientId]) REFERENCES [dbo].[Patient] ([Id]),
CONSTRAINT [FK_PatientMilestone_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitId]) REFERENCES [dbo].[PatientMasterVisit] ([Id])
)
END