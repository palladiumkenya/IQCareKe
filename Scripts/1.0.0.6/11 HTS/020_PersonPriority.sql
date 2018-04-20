SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[PersonPriority]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[PersonPriority](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[PersonId] [int] NOT NULL,
		[PriorityId] [int] NOT NULL,
		[DeleteFlag] [bit] NULL,
		[CreatedBy] [int] NULL,
		[CreateDate] [datetime] NULL,
		[AuditData] [xml] NULL,
	 CONSTRAINT [PK_PersonPriority] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
	END;
GO


