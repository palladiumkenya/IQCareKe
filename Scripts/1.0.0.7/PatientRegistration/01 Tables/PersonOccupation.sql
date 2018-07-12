
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO



IF NOT EXISTS
(
	SELECT *
	FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[PersonOccupation]')
		  AND type IN(N'U')
)
BEGIN
CREATE TABLE [dbo].[PersonOccupation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PersonId] [int] NOT NULL,
	[Occupation] [int] NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_PersonOccupation_IsActive]  DEFAULT ((1)),
	[DeleteFlag] [bit] NOT NULL CONSTRAINT [DF_PersonOccupation_Void]  DEFAULT ((0)),
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[AuditData] [xml] NULL,
 CONSTRAINT [PK_PersonOccupation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
END
GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[PersonOccupation]  WITH CHECK ADD  CONSTRAINT [FK_PersonOccupation_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
GO

ALTER TABLE [dbo].[PersonOccupation] CHECK CONSTRAINT [FK_PersonOccupation_Person]
GO


