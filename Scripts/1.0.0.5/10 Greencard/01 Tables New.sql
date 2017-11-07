/****** Object:  Table [dbo].[IdentifierType]    Script Date: 19/09/2017 21:19:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS
(
	SELECT *
	FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[IdentifierType]')
		  AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[IdentifierType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	 CONSTRAINT [PK_IdentifierType] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY];
END;

IF NOT EXISTS(SELECT *  FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ApiInbox]') AND type IN(N'U'))
BEGIN
	CREATE TABLE [dbo].[ApiInbox](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[uid] [char](50) NULL,
	[DateReceived] [datetime] NULL,
	[SenderId] [int] NULL,
	[Message] [varchar](max) NULL,
	[Processed] [bit] NULL,
	[DateProcessed] [datetime] NULL,
	[LogMessage] [nvarchar](max) NULL,
 CONSTRAINT [PK_ApiInbox] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[ApiInbox] ADD  CONSTRAINT [DF_ApiInbox_Processed]  DEFAULT ((0)) FOR [Processed]
END

IF NOT EXISTS(SELECT *  FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ApiInteropSystem]') AND type IN(N'U'))
BEGIN
CREATE TABLE [dbo].[ApiInteropSystem](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](225) NULL,
	[EndPoint] [varchar](50) NULL,
	[APIKey] [varchar](225) NULL,
	[Active] [bit] NULL,
	[DeleteFlag] [bit] NULL,
 CONSTRAINT [PK_ApiInteropSystem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

	ALTER TABLE [dbo].[ApiInteropSystem] ADD  CONSTRAINT [DF_ApiInteropSystem_Active]  DEFAULT ((0)) FOR [Active]

	ALTER TABLE [dbo].[ApiInteropSystem] ADD  CONSTRAINT [DF_ApiInteropSystem_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
END

IF NOT EXISTS(SELECT *  FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ApiOutbox]') AND type IN(N'U'))
BEGIN
CREATE TABLE [dbo].[ApiOutbox](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Uid] [char](36) NULL,
	[DateSent] [datetime] NULL CONSTRAINT [DF_ApiOutbox_DateSent]  DEFAULT (getdate()),
	[RecepientId] [int] NULL CONSTRAINT [DF_ApiOutbox_RecepientId]  DEFAULT ((1)),
	[Message] [varchar](max) NULL,
	[AttemptCount] [int] NULL CONSTRAINT [DF_ApiOutbox_AttemptCount]  DEFAULT ((0)),
	[LogMessage] [varchar](225) NULL,
 CONSTRAINT [PK_ApiOutbox] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


	ALTER TABLE [dbo].[ApiOutbox] ADD  CONSTRAINT [DF_ApiOutbox_AttemptCount]  DEFAULT ((0)) FOR [AttemptCount]

END


/****** Object:  Table [dbo].[PersonIdentifier]    Script Date: 19/09/2017 21:19:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS
(
	SELECT *
	FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[PersonIdentifier]')
		  AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[PersonIdentifier](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[IdentifierId] [int] NOT NULL,
	[IdentifierValue] [varchar](50) NOT NULL,
	[DeleteFlag] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[AuditData] [xml] NULL,
 CONSTRAINT [PK_PersonIdentifier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[PersonIdentifier] ADD  CONSTRAINT [DF_PersonIdentifier_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
END;


