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


IF  EXISTS
(
	SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[PatientMilestone]')
          AND type IN(N'U')
)
BEGIN
 IF  EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'TypeAssessedId'
          AND Object_ID = Object_ID(N'PatientMilestone'))
BEGIN
EXEC sp_rename '[dbo].[PatientMilestone].TypeAssessedId', 'MilestoneAssessedId','column';
ALTER TABLE dbo.PatientMilestone ALTER Column MilestoneAssessedId int not null
END

IF  EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'AchievedId'
          AND Object_ID = Object_ID(N'PatientMilestone'))
BEGIN
EXEC sp_rename '[dbo].[PatientMilestone].AchievedId', 'MilestoneAchievedId','column';


END
IF  EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'StatusId'
          AND Object_ID = Object_ID(N'PatientMilestone'))
BEGIN
EXEC sp_rename '[dbo].[PatientMilestone].StatusId', 'MilestoneStatusId','column';
END
IF  EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'Comment'
          AND Object_ID = Object_ID(N'PatientMilestone'))
BEGIN
EXEC sp_rename '[dbo].[PatientMilestone].Comment', 'MilestoneComments','column';
END
IF  EXISTS(SELECT * FROM sys.columns 
          WHERE Name = N'DateAssessed'
          AND Object_ID = Object_ID(N'PatientMilestone'))
BEGIN
EXEC sp_rename '[dbo].[PatientMilestone].DateAssessed', 'MilestoneDate','column';
END

ALTER TABLE dbo.PatientMilestone ALTER Column MilestoneAssessedId [INT] NOT NULL;
ALTER TABLE dbo.PatientMilestone ALTER Column	MilestoneDate [DateTime] NOT NULL;
ALTER TABLE dbo.PatientMilestone ALTER Column	MilestoneAchievedId [INT] NOT NULL; 
IF NOT EXISTS (  select *
      from sys.all_columns c
      join sys.tables t on t.object_id = c.object_id
    
      join sys.default_constraints d on c.default_object_id = d.object_id
    where t.name = 'PatientMilestone'
      and c.name = 'MilestoneAchievedId')
	BEGIN
ALTER TABLE dbo.PatientMilestone ADD CONSTRAINT DF_MilestoneAchieved DEFAULT(0) FOR MilestoneAchievedId;
END
ALTER TABLE dbo.PatientMilestone ALTER Column MilestoneStatusId [INT] NOT NULL;
ALTER TABLE dbo.PatientMilestone ALTER Column	MilestoneComments [Text];
  
END

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

