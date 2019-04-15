


/****** Object:  Table [dbo].[Room]    Script Date: 26/03/2019 13:47:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS
(
	SELECT *
	FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[Rooms]')
		  AND type IN(N'U')
)

CREATE TABLE [dbo].[Rooms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoomName] [varchar](2500) NOT NULL,
	[DisplayName] [varchar] (5000) NOT NULL,
	[Description] [varchar](max) NULL,
	[DeleteFlag] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NULL,
	[UpdatedBy]  [int] null
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Rooms] ADD  CONSTRAINT [DF_Rooms_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO

ALTER TABLE [dbo].[Rooms] ADD  CONSTRAINT [DF_ROOMS_Active]  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [dbo].[Rooms] ADD  CONSTRAINT [DF_Rooms_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO


