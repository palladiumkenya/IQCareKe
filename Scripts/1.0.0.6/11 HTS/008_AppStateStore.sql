SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[AppStateStore]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[AppStateStore](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[PersonId] [int] NULL,
		[PatientId] [int] NULL,
		[PatientMasterVisitId] [int] NULL,
		[EncounterId] [int] NULL,
		[AppStateId] [int] NULL,
		[StatusDate] [datetime] NULL,
		[DeleteFlag] [bit] NULL CONSTRAINT [DF_AppStateStore_DeleteFlag]  DEFAULT ((0)),
	 CONSTRAINT [PK_AppStateStore] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY];
END;
GO