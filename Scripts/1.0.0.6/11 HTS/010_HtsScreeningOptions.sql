SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[HtsScreeningOptions]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[HtsScreeningOptions](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[PersonId] [int] NOT NULL,
		[Occupation] [varchar](250) NULL,
		[ScreeningDate] [datetime] NULL,
		[BookingDate] [datetime] NULL,
	 CONSTRAINT [PK_HtsScreeningOptions] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY];
END;
GO