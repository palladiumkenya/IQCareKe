/****** Object:  Table [dbo].[AfyaMobileInbox]    Script Date: 6/19/2018 4:41:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[AfyaMobileInbox]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[AfyaMobileInbox](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[DateReceived] [datetime] NOT NULL,
		[AfyamobileId] [varchar](100) NULL,
		[Message] [varchar](max) NULL,
		[Processed] [bit] NULL,
		[DateProcessed] [datetime] NULL,
		[LogMessage] [varchar](max) NULL,
	 CONSTRAINT [PK_AfyaMobileInbox] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
	END;
GO

ALTER TABLE [dbo].[AfyaMobileInbox] ADD  CONSTRAINT [DF_AfyaMobileInbox_Processed]  DEFAULT ((0)) FOR [Processed]
GO


