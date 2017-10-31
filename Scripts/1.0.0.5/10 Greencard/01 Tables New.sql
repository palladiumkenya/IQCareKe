/****** Object:  Table [dbo].[IdentifierTypes]    Script Date: 19/09/2017 21:19:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[IdentifierTypes]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[IdentifierTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	 CONSTRAINT [PK_IdentifierTypes] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY];
END;

IF NOT EXISTS(SELECT *  FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[API_Inbox]') AND type IN(N'U'))
BEGIN
	CREATE TABLE [dbo].[API_Inbox](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[uid] [char](50) NOT NULL,
	[DateReceived] [datetime] NOT NULL,
	[SenderId] [int] NOT NULL,
	[Message] [varchar](max) NOT NULL,
	[Processed] [bit] NULL,
	[DateProcessed] [datetime] NOT NULL,
	[LogMessage] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_API_Inbox] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[API_Inbox] ADD  CONSTRAINT [DF_API_Inbox_Processed]  DEFAULT ((0)) FOR [Processed]
END

IF NOT EXISTS(SELECT *  FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[API_InteropSystem]') AND type IN(N'U'))
BEGIN
		CREATE TABLE [dbo].[API_InteropSystem](
		[Id] [int] NOT NULL,
		[Name] [nvarchar](225) NOT NULL,
		[EndPoint] [varchar](50) NOT NULL,
		[APIKey] [varchar](225) NOT NULL,
		[Active] [bit] NOT NULL,
		[DeleteFlag] [bit] NOT NULL,
	 CONSTRAINT [PK_API_InteropSystem] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[API_InteropSystem] ADD  CONSTRAINT [DF_API_InteropSystem_Active]  DEFAULT ((0)) FOR [Active]

	ALTER TABLE [dbo].[API_InteropSystem] ADD  CONSTRAINT [DF_API_InteropSystem_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
END

IF NOT EXISTS(SELECT *  FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[API_Outbox]') AND type IN(N'U'))
BEGIN
	
	CREATE TABLE [dbo].[API_Outbox](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[Uid] [char](36) NOT NULL,
		[DateRead] [datetime] NULL,
		[DateSent] [datetime] NULL,
		[RecepientId] [int] NOT NULL,
		[Message] [varchar](max) NOT NULL,
		[AttemptCount] [int] NOT NULL,
		[LogMessage] [varchar](225) NOT NULL,
	 CONSTRAINT [PK_API_Outbox] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	SET ANSI_PADDING OFF

	ALTER TABLE [dbo].[API_Outbox] ADD  CONSTRAINT [DF_API_Outbox_AttemptCount]  DEFAULT ((0)) FOR [AttemptCount]

END


