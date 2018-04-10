/****** Object:  Table [dbo].[Testing]    Script Date: 21-Mar-18 9:52:59 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[Testing]')
          AND type IN(N'U')
)
BEGIN

	CREATE TABLE [dbo].[Testing](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[HtsEncounterId] [int] NOT NULL,
		[ProviderId] [int] NOT NULL,
		[KitId] [int] NOT NULL,
		[KitLotNumber] [nvarchar](300) NULL,
		[ExpiryDate] [datetime] NULL,
		[Outcome] [int] NOT NULL,
		[TestRound] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY];

END;
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME='FK_Testing_HtsEncounterId_HtsEncounter_Id' )
BEGIN
	ALTER TABLE [dbo].[Testing]  WITH CHECK ADD  CONSTRAINT [FK_Testing_HtsEncounterId_HtsEncounter_Id] FOREIGN KEY([HtsEncounterId])
	REFERENCES [dbo].[HtsEncounter] ([Id])
	
	ALTER TABLE [dbo].[Testing] CHECK CONSTRAINT [FK_Testing_HtsEncounterId_HtsEncounter_Id]
END;
GO