

/****** Object:  Table [dbo].[QueueWaitingList]    Script Date: 25/03/2019 08:49:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS
(
	SELECT *
	FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[QueueWaitingList]')
		  AND type IN(N'U')
)
BEGIN

CREATE TABLE [dbo].[QueueWaitingList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[ServiceRoomId] [int] NOT NULL,
	[Priority] [int] NOT NULL,
	[DeleteFlag] [bit] NOT NULL,
	[Status] [bit] NULL,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdateDate] [datetime] NULL,
 CONSTRAINT [PK_QueueWaitingList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


END



