
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_PatientPartnerProfile_Patient') AND parent_object_id = OBJECT_ID(N'dbo.PatientPartnerProfile'))
ALTER TABLE [dbo].[PatientPartnerProfile] DROP CONSTRAINT [FK_PatientPartnerProfile_Patient]

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientPartnerProfile]') AND type in (N'U'))



CREATE TABLE [dbo].[PatientPartnerProfile](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] int not null,
	[HivPositiveStatusDate] DATETIME NULL,
	[CCCEnrollment] INT NULL,
	[CCCNumber] varchar(max) NULL,
	[PartnerARTStartDate] DATETIME NULL,
	[HIVSeroDiscordantDuration] varchar(max)  NULL,
	[SexWithOutCondoms] [int] NULL,
	[NumberofChildren] varchar(max) NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_PatientPartnerProfile_IsActive]  DEFAULT ((1)),
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[DeleteFlag] [bit] NOT NULL DEFAULT ((0)),
	[AuditData] [xml] NULL,

 CONSTRAINT [PK_PatientPartnerProfile] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



ALTER TABLE [dbo].[PatientPartnerProfile]  WITH CHECK ADD  CONSTRAINT [FK_PatientPartnerProfile_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO



ALTER TABLE [dbo].[PatientPartnerProfile] CHECK CONSTRAINT [FK_PatientPartnerProfile_Patient]
GO

