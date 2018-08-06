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

IF NOT EXISTS
(
	SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[PatientMilestone]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[PatientMilestone](
		[Id][INT]NOT NULL IDENTITY(1,1),
		[PatientId][INT] NOT NULL,
		[PatientMasterVisitId][INT] NOT NULL,
		[CreatedBy][INT] NOT NULL,
		[MilestoneAssessedId][INT] NOT NULL,
		[MilestoneDate][DateTime] NOT NULL,
		[MilestoneAchievedId][INT] NOT NULL DEFAULT(0),
		[MilestoneStatusId][INT] NOT NULL,
		[MilestoneComments][Text],
		[CreateDate][DateTime] NOT NULL DEFAULT(GETDATE()),
		[UpdateDate][DateTime] NULL,
		[DeleteFlag][Bit] NOT NULL DEFAULT(0),
		[AuditData][xml]
	)ON [PRIMARY]
END
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

