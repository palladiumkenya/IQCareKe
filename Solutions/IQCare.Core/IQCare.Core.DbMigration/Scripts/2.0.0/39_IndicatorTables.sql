IF NOT EXISTS
(
	SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[Form]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[Form](
		[Id][INT]NOT NULL IDENTITY(1,1),
		[Name] Varchar(300) NULL,
		[CreatedBy][INT] NOT NULL,
		[CreateDate][DateTime] NOT NULL DEFAULT(GETDATE()),
		[UpdateDate][DateTime] NULL,
		[DeleteFlag][Bit] NOT NULL DEFAULT(0),
		
	CONSTRAINT [PK_Form] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
END
GO



IF NOT EXISTS
(
	SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[Section]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[Section](
		[Id][INT]NOT NULL IDENTITY(1,1),
		[FormId] INT  NULL,
		[SectionName] varchar(400) NULL,
		[Description] varchar(Max) NULL,
		[Active] [BIT] NOT NULL DEFAULT(0),
		[CreateDate][DateTime] NOT NULL DEFAULT(GETDATE()),
		[UpdateDate][DateTime] NULL,
		[DeleteFlag][Bit] NOT NULL DEFAULT(0),
		[CreatedBy][INT] NOT NULL
		
	CONSTRAINT [PK_Section] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
END
GO




IF EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'dbo.FK_Section_FormId')
   AND parent_object_id = OBJECT_ID(N'dbo.Section')
)
BEGIN
ALTER TABLE [dbo].[Section]  DROP CONSTRAINT [FK_Section_FormId] 
END
ALTER TABLE [dbo].[Section]  WITH CHECK ADD  CONSTRAINT [FK_Section_FormId] FOREIGN KEY([FormId])
REFERENCES [dbo].[Form] ([Id])
GO


IF NOT EXISTS
(
	SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[SubSection]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[SubSection](
		[Id][INT]NOT NULL IDENTITY(1,1),
		[SectionId] INT  NULL,
		[SubSectionName] varchar(400) NULL,
		[Description] varchar(Max) NULL,
		[Active] [BIT] NOT NULL DEFAULT(0),
		[CreateDate][DateTime] NOT NULL DEFAULT(GETDATE()),
		[UpdateDate][DateTime] NULL,
		[DeleteFlag][Bit] NOT NULL DEFAULT(0),
		[CreatedBy][INT] NOT NULL
		
	CONSTRAINT [PK_SubSection] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
END
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_SubSection_SectionId') AND parent_object_id = OBJECT_ID(N'dbo.SubSection'))
BEGIN
ALTER TABLE [dbo].[SubSection]  DROP CONSTRAINT [FK_SubSection_SectionId] 
END
ALTER TABLE [dbo].[SubSection]  WITH CHECK ADD  CONSTRAINT [FK_SubSection_SectionId] FOREIGN KEY([SectionId])
REFERENCES [dbo].[Section] ([Id])
GO
go
IF NOT EXISTS
(
	SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[DataType]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[DataType](
		[Id][INT]NOT NULL IDENTITY(1,1),
		[Name] varchar(500)  NULL,
		[CreateDate][DateTime] NOT NULL DEFAULT(GETDATE()),
		[UpdateDate][DateTime] NULL,
		[DeleteFlag][Bit] NOT NULL DEFAULT(0),
		[CreatedBy][INT] NOT NULL
		
	CONSTRAINT [PK_DataType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
END
GO



IF NOT EXISTS
(
	SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[Indicator]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[Indicator](
		[Id][INT]NOT NULL IDENTITY(1,1),
		[SubSectionId] INT  NULL,
		[IndicatorName] varchar(400) NULL,
		[Code] varchar(Max) NULL,
		[DataTypeId] [int] NULL, 
		[CreateDate][DateTime] NOT NULL DEFAULT(GETDATE()),
		[UpdateDate][DateTime] NULL,
		[DeleteFlag][Bit] NOT NULL DEFAULT(0),
		[CreatedBy][INT] NOT NULL
		
	CONSTRAINT [PK_Indicator] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
END
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo.FK_Indicator_SubSectionId]') AND parent_object_id = OBJECT_ID(N'dbo.Indicator'))
BEGIN
ALTER TABLE [dbo].[Indicator]  DROP CONSTRAINT [dbo.FK_Indicator_SubSectionId] 
END
ALTER TABLE [dbo].[Indicator]  WITH CHECK ADD  CONSTRAINT [dbo.FK_Indicator_SubSectionId] FOREIGN KEY([SubSectionId])
REFERENCES [dbo].[SubSection] ([Id])
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo.FK_Indicator_DataType]') AND parent_object_id = OBJECT_ID(N'dbo.Indicator'))
BEGIN
ALTER TABLE [dbo].[Indicator]  DROP CONSTRAINT [dbo.FK_Indicator_DataType] 
END
ALTER TABLE [dbo].[Indicator]  WITH CHECK ADD  CONSTRAINT [dbo.FK_Indicator_DataType] FOREIGN KEY([DataTypeId])
REFERENCES [dbo].[DataType] ([Id])
GO


IF NOT EXISTS
(
	SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[FormReportingPeriod]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[FormReportingPeriod](
		[Id] [INT] NOT NULL IDENTITY(1,1),
		[FormId] INT  NULL,
		[FormReportingDate] DATETIME NULL,
		[CreateDate][DateTime] NOT NULL DEFAULT(GETDATE()),
		[UpdateDate][DateTime] NULL,
		[DeleteFlag][Bit] NOT NULL DEFAULT(0),
		[CreatedBy][INT] NOT NULL
		
		CONSTRAINT [PK_FormReportingPeriod] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
END
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo.FK_FormReportingPeriod_FormId]') AND parent_object_id = OBJECT_ID(N'dbo.FormReportingPeriod'))
BEGIN
ALTER TABLE [dbo].[FormReportingPeriod]  DROP CONSTRAINT [dbo.FK_FormReportingPeriod_FormId] 
END

ALTER TABLE [dbo].[FormReportingPeriod]  WITH CHECK ADD  CONSTRAINT [dbo.FK_FormReportingPeriod_FormId] FOREIGN KEY([FormId])
REFERENCES [dbo].[Form] ([Id])
GO





IF NOT EXISTS
(
	SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[IndicatorResults]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[IndicatorResults](
		[Id][INT]NOT NULL IDENTITY(1,1),
		[FormReportingPId] INT  NULL,
		[IndicatorId] INT NULL,
		[ResultText] varchar(100) NULL,
		[ResultNumeric]  decimal NULL,
		[CreateDate][DateTime] NOT NULL DEFAULT(GETDATE()),
		[UpdateDate][DateTime] NULL,
		[DeleteFlag][Bit] NOT NULL DEFAULT(0),
		[CreatedBy][INT] NOT NULL
		
	CONSTRAINT [PK_IndicatorResults] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
END
GO
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo.FK_IndicatorResults_IndicatorId]') AND parent_object_id = OBJECT_ID(N'dbo.IndicatorResults'))
BEGIN
ALTER TABLE [dbo].[IndicatorResults]  DROP CONSTRAINT [dbo.FK_IndicatorResults_IndicatorId] 
END
ALTER TABLE [dbo].[IndicatorResults]  WITH CHECK ADD  CONSTRAINT [dbo.FK_IndicatorResults_IndicatorId] FOREIGN KEY([IndicatorId])
REFERENCES [dbo].[Indicator] ([Id])
GO

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo.FK_IndicatorResults_FormReportingPId]') AND parent_object_id = OBJECT_ID(N'dbo.IndicatorResults'))
BEGIN
ALTER TABLE [dbo].[IndicatorResults]  DROP CONSTRAINT [dbo.FK_IndicatorResults_FormReportingPId] 
END
ALTER TABLE [dbo].[IndicatorResults]  WITH CHECK ADD  CONSTRAINT [dbo.FK_IndicatorResults_FormReportingPId] FOREIGN KEY([FormReportingPId])
REFERENCES [dbo].[FormReportingPeriod] ([Id])
GO
