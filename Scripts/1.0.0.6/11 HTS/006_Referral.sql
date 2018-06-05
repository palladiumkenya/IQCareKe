SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[Referrals]')
          AND type IN(N'U')
)
BEGIN
	alter table Referrals drop constraint [PK_Referrals]
	alter table Referrals drop constraint [FK_Referrals_PatientMasterVisit]
	DROP TABLE Referrals;
END;
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS
(
    SELECT *
    FROM sys.objects
    WHERE object_id = OBJECT_ID(N'[dbo].[Referral]')
          AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[Referral](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[PersonId] [int] NOT NULL,
		[FromServicePoint] [int] NULL,
		[FromFacility] [int] NULL,
		[ToServicePoint] [int] NULL,
		[ToFacility] [int] NULL,
		[ReferralReason] [int] NOT NULL,
		[ReferralDate] [datetime] NOT NULL,
		[ReferredBy] [int] NOT NULL,
		[ExpectedDate] [datetime] NOT NULL,
		[CreatedBy] [int] NOT NULL,
		[CreateDate] [datetime] NOT NULL,
		[DeleteFlag] [bit] NOT NULL,
		[AuditData] [xml] NULL,
	 CONSTRAINT [PK_Referrals] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];
END;
GO