
/****** Object:  Table [dbo].[HeiMilestone]    Script Date: 9/11/2018 11:09:15 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_HeiMilestone_PatientMasterVisit') AND parent_object_id = OBJECT_ID(N'dbo.HeiMilestone'))
ALTER TABLE [dbo].[HeiMilestone] DROP CONSTRAINT [FK_HeiMilestone_PatientMasterVisit]
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_HeiMilestone_Patient') AND parent_object_id = OBJECT_ID(N'dbo.HeiMilestone'))
ALTER TABLE [dbo].[HeiMilestone] DROP CONSTRAINT [FK_HeiMilestone_Patient]

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HeiMilestone]') AND type in (N'U'))


CREATE TABLE [dbo].[HeiMilestone](
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
 CONSTRAINT [PK_HeiMilestone] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[HeiMilestone]  WITH CHECK ADD  CONSTRAINT [FK_HeiMilestone_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitId])
REFERENCES [dbo].[PatientMasterVisit] ([Id])
GO

ALTER TABLE [dbo].[HeiMilestone]  WITH CHECK ADD  CONSTRAINT [FK_HeiMilestone_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO

ALTER TABLE [dbo].[HeiMilestone] CHECK CONSTRAINT [FK_HeiMilestone_PatientMasterVisit]
GO

ALTER TABLE [dbo].[HeiMilestone] CHECK CONSTRAINT [FK_HeiMilestone_Patient]
GO

