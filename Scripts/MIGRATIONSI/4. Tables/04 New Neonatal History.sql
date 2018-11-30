IF NOT EXISTS
(
	SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[PatientNeonatalHistory]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[PatientNeonatalHistory](
		[Id][INT]NOT NULL IDENTITY(1,1),
		[PatientId][INT] NOT NULL,
		[PatientMasterVisitId][INT] NOT NULL,
		[CreatedBy][INT] NOT NULL,
		[RecordNeonatalHistory][INT] NOT NULL,
		[NeonatalHistoryNotes][Text],
		[CreateDate][DateTime] NOT NULL DEFAULT(GETDATE()),
		[UpdateDate][DateTime] NULL,
		[DeleteFlag][Bit] NOT NULL DEFAULT(0),
		[AuditData][xml]
	)ON [PRIMARY]
END
GO


IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_PatientMilestone_PatientMasterVisit') AND parent_object_id = OBJECT_ID(N'dbo.PatientMilestone'))
ALTER TABLE [dbo].[PatientMilestone] DROP CONSTRAINT [FK_PatientMilestone_PatientMasterVisit]
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_PatientMilestone_Patient') AND parent_object_id = OBJECT_ID(N'dbo.PatientMilestone'))
ALTER TABLE [dbo].[PatientMilestone] DROP CONSTRAINT [FK_PatientMilestone_Patient]

IF OBJECT_ID('dbo.PatientMilestone', 'U') IS NOT NULL DROP TABLE dbo.PatientMilestone

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientMilestone]') AND type in (N'U'))


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
 CONSTRAINT [PK_PatientMilestone] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

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



IF NOT EXISTS
(
	SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TannersStaging]') AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[TannersStaging]
	(
		[Id][INT]NOT NULL IDENTITY(1,1),
		[PatientId][INT] NOT NULL,
		[PatientMasterVisitId][INT]NOT NULL,
		[CreatedBy][INT]NOT NULL,
		[RecordTannersStaging][INT] NOT NULL,
		[CreateDate][DateTime] NOT NULL DEFAULT(GETDATE()),
		[UpdateDate][DateTime] NULL,
		[DeleteFlag][Bit] NOT NULL DEFAULT(0),
		[AuditData][xml]
	)
END
GO


IF NOT EXISTS
(
	SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientTannersStaging]') AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[PatientTannersStaging]
	(
		[Id][INT]NOT NULL IDENTITY(1,1),
		[PatientId][INT] NOT NULL,
		[PatientMasterVisitId][INT]NOT NULL,
		[CreatedBy][INT]NOT NULL,
		[TannersStagingDate][DateTime] NOT NULL,
		[BreastsGenitalsId][INT],
		[PubicHairId][INT],
		[CreateDate][DateTime] NOT NULL DEFAULT(GETDATE()),
		[UpdateDate][DateTime] NULL,
		[DeleteFlag][Bit] NOT NULL DEFAULT(0),
		[AuditData][xml]
	) ON [PRIMARY]
END
GO

IF NOT EXISTS
(
	SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientSocialHistory]') AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[PatientSocialHistory]
	(
		[Id][INT]NOT NULL IDENTITY(1,1),
		[PatientId][INT] NOT NULL,
		[PatientMasterVisitId][INT]NOT NULL,
		[CreatedBy][INT]NOT NULL,
		[RecordSocialHistory][INT]NOT NULL,
		[DrinkAlcoholId][INT],
		[SmokeId][INT],
		[UseDrugsId][INT],
		[SocialHistoryNotes][Text],
		[CreateDate][DateTime] NOT NULL DEFAULT(GETDATE()),
		[UpdateDate][DateTime] NULL,
		[DeleteFlag][Bit] NOT NULL DEFAULT(0),
		[AuditData][xml]
	) ON [PRIMARY]
END
GO

