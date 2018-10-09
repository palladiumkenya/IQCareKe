

/****** Object:  Table [dbo].[HEIFeeding]    Script Date: 9/11/2018 11:08:17 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_HEIFeeding_PatientMasterVisit') AND parent_object_id = OBJECT_ID(N'dbo.HEIFeeding'))
ALTER TABLE [dbo].[HEIFeeding] DROP CONSTRAINT [FK_HEIFeeding_PatientMasterVisit]
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_HEIFeeding_Patient') AND parent_object_id = OBJECT_ID(N'dbo.HEIFeeding'))
ALTER TABLE [dbo].[HEIFeeding] DROP CONSTRAINT [FK_HEIFeeding_Patient]

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HEIFeeding]') AND type in (N'U'))

CREATE TABLE [dbo].[HEIFeeding](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PatientMasterVisitId] [int] NOT NULL,
	[PatientId] [int] not null,
	[FeedingModeId] [int] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[DeleteFlag] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_HEIFeeding] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[HEIFeeding]  WITH CHECK ADD  CONSTRAINT [FK_HEIFeeding_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitId])
REFERENCES [dbo].[PatientMasterVisit] ([Id])
GO

ALTER TABLE [dbo].[HEIFeeding]  WITH CHECK ADD  CONSTRAINT [FK_HEIFeeding_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO

ALTER TABLE [dbo].[HEIFeeding] CHECK CONSTRAINT [FK_HEIFeeding_PatientMasterVisit]
GO

ALTER TABLE [dbo].[HEIFeeding] CHECK CONSTRAINT [FK_HEIFeeding_Patient]
GO


