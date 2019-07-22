

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_RiskAssessment_PatientMasterVisit') AND parent_object_id = OBJECT_ID(N'dbo.RiskAssessment'))
ALTER TABLE [dbo].[RiskAssessment] DROP CONSTRAINT [FK_RiskAssessment_PatientMasterVisit]
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_RiskAssessment_Patient') AND parent_object_id = OBJECT_ID(N'dbo.RiskAssessment'))
ALTER TABLE [dbo].[RiskAssessment] DROP CONSTRAINT [FK_RiskAssessment_Patient]

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RiskAssessment]') AND type in (N'U'))


CREATE TABLE [dbo].[RiskAssessment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PatientMasterVisitId] [int] NOT NULL,
	[PatientId] int not null,
	[RiskAssessmentId] [int] not NULL,
	[RiskAssessmentValue] [int] NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_RiskAssessment_IsActive]  DEFAULT ((1)),
	[Comment] [text] NULL,
	[AssessmentDate] [DateTime] Null,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[DeleteFlag] [bit] NOT NULL DEFAULT ((0)),
	[AuditData] [xml] NULL,

 CONSTRAINT [PK_RiskAssessment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[RiskAssessment]  WITH CHECK ADD  CONSTRAINT [FK_RiskAssessment_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitId])
REFERENCES [dbo].[PatientMasterVisit] ([Id])
GO

ALTER TABLE [dbo].[RiskAssessment]  WITH CHECK ADD  CONSTRAINT [FK_RiskAssessment_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO

ALTER TABLE [dbo].[RiskAssessment] CHECK CONSTRAINT [FK_RiskAssessment_PatientMasterVisit]
GO

ALTER TABLE [dbo].[RiskAssessment] CHECK CONSTRAINT [FK_RiskAssessment_Patient]
GO

