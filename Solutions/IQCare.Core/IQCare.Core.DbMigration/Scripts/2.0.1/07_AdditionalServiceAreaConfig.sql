/****** Object:  Table [dbo].[AdditionalServiceAreaFields]    Script Date: 3/19/2019 9:54:03 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AdditionalServiceAreaConfig](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceAreaId] [int] NOT NULL,
	[AdditionalField] [varchar](50) NOT NULL,
	[TargetTable] [varchar](50) NULL,
	[DeleteFlag] [bit] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_AdditionalServiceAreaFields] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


