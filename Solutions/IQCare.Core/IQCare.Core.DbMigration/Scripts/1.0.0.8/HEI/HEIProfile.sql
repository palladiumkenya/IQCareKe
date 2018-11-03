

/****** Object:  Table [dbo].[HEIProfile]    Script Date: 9/11/2018 11:08:40 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_HEIProfile_PatientMasterVisit') AND parent_object_id = OBJECT_ID(N'dbo.HEIProfile'))
ALTER TABLE [dbo].[HEIProfile] DROP CONSTRAINT [FK_HEIProfile_PatientMasterVisit]
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_HEIProfile_Patient') AND parent_object_id = OBJECT_ID(N'dbo.HEIProfile'))
ALTER TABLE [dbo].[HEIProfile] DROP CONSTRAINT [FK_HEIProfile_Patient]

IF OBJECT_ID('dbo.HEIProfile', 'U') IS NOT NULL DROP TABLE dbo.HEIProfile

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HEIProfile]') AND type in (N'U'))

CREATE TABLE [dbo].[HEIProfile](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PatientMasterVisitID] [int] NOT NULL,
	[PatientId] int not null,
	[VisitDate] [datetime] NULL,
	[VisitTypeId] [int] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[DeleteFlag] [bit] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_HEIProfile] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[HEIProfile]  WITH CHECK ADD  CONSTRAINT [FK_HEIProfile_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitID])
REFERENCES [dbo].[PatientMasterVisit] ([Id])
GO

ALTER TABLE [dbo].[HEIProfile]  WITH CHECK ADD  CONSTRAINT [FK_HEIProfile_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO

ALTER TABLE [dbo].[HEIProfile] CHECK CONSTRAINT [FK_HEIProfile_PatientMasterVisit]
GO

ALTER TABLE [dbo].[HEIProfile] CHECK CONSTRAINT [FK_HEIProfile_Patient]
GO


