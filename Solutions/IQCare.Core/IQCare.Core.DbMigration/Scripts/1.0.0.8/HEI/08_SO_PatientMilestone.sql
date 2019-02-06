
/****** Object:  Table [dbo].[PatientMilestone]    Script Date: 9/11/2018 11:09:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_PatientMilestone_PatientMasterVisit') AND parent_object_id = OBJECT_ID(N'dbo.PatientMilestone'))
ALTER TABLE [dbo].[PatientMilestone] DROP CONSTRAINT [FK_PatientMilestone_PatientMasterVisit]
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_PatientMilestone_Patient') AND parent_object_id = OBJECT_ID(N'dbo.PatientMilestone'))
ALTER TABLE [dbo].[PatientMilestone] DROP CONSTRAINT [FK_PatientMilestone_Patient]



IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientMilestone]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PatientMilestone](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PatientMasterVisitId] [int] NOT NULL,
	[PatientId] int not null,
	[TypeAssessedId] [int] NULL,
	[AchievedId] [bit] NULL,
	[StatusId] [int] NULL,
	[Comment] [text] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[DeleteFlag] [bit] NOT NULL DEFAULT ((0)),
	[DateAssessed] [datetime] NULL,
	[AuditData] [xml] NULL
 CONSTRAINT [PK_PatientMilestone] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

ALTER TABLE [dbo].[PatientMilestone]  WITH CHECK ADD  CONSTRAINT [FK_PatientMilestone_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitId])
REFERENCES [dbo].[PatientMasterVisit] ([Id])
GO

ALTER TABLE [dbo].[PatientMilestone]  WITH CHECK ADD  CONSTRAINT [FK_PatientMilestone_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO

ALTER TABLE [dbo].[PatientMilestone] CHECK CONSTRAINT [FK_PatientMilestone_PatientMasterVisit]
GO

ALTER TABLE [dbo].[PatientMilestone] CHECK CONSTRAINT [FK_PatientMilestone_Patient]
GO

