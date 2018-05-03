SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[Tracing]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[Tracing](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[PersonID] [int] NOT NULL,
		[TracingType] [int] NOT NULL,
		[DateTracingDone] [datetime] NOT NULL,
		[Mode] [int] NOT NULL,
		[Outcome] [int] NOT NULL,
		[Remarks] [varchar](250) NULL,
		[DeleteFlag] [bit] NOT NULL CONSTRAINT [DF_Tracing_DeleteFlag]  DEFAULT ((0)),
		[CreatedBy] [int] NOT NULL,
		[CreateDate] [datetime] NOT NULL,
		[AuditData] [xml] NULL,
	 CONSTRAINT [PK_Tracing] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
END;
GO



