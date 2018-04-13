-- NEW PSMART TABLES


IF NOT EXISTS
(
	SELECT *
	FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[Psmart_Store]')
		  AND type IN(N'U')
)
BEGIN
	/****** Object:  Table [dbo].[Psmart_Store]    Script Date: 4/13/2018 8:51:50 AM ******/
SET ANSI_NULLS ON


SET QUOTED_IDENTIFIER ON


CREATE TABLE [dbo].[Psmart_Store](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SHR] [text] NULL,
	[Status] [varchar](50) NULL,
	[uuid] [uniqueidentifier] NULL,
	[Status_date] [datetime] NULL,
	[Date_created] [datetime] NULL,
 CONSTRAINT [PK__PsmartSt__3214EC076DBB1F5D] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


ALTER TABLE [dbo].[Psmart_Store] ADD  CONSTRAINT [DF_PsmartStore_Status]  DEFAULT ('PENDING') FOR [Status]


ALTER TABLE [dbo].[Psmart_Store] ADD  CONSTRAINT [DF_Psmart_Store_Status_date]  DEFAULT (getdate()) FOR [Status_date]


ALTER TABLE [dbo].[Psmart_Store] ADD  CONSTRAINT [DF_Psmart_Store_Date_created]  DEFAULT (getdate()) FOR [Date_created]

END
GO

---------------- PPSMART TRANSACTIONLOG
IF NOT EXISTS
(
	SELECT *
	FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[PSMartTransactionLog]')
		  AND type IN(N'U')
)
BEGIN
	/****** Object:  Table [dbo].[PSmartTransactionLog]    Script Date: 4/13/2018 8:53:21 AM ******/
	SET ANSI_NULLS ON


	SET QUOTED_IDENTIFIER ON


	CREATE TABLE [dbo].[PSmartTransactionLog](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[TranLogId] [uniqueidentifier] NOT NULL,
		[TransactionType] [int] NOT NULL,
		[TransactionDate] [datetime] NOT NULL,
		[UserId] [int] NULL,
	 CONSTRAINT [PK_PSmartTransactionLog] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]


	ALTER TABLE [dbo].[PSmartTransactionLog] ADD  CONSTRAINT [DF_PSmartTranstionLog_UUID]  DEFAULT (newsequentialid()) FOR [TranLogId]
END



IF NOT EXISTS
(
	SELECT *
	FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[ImmunizationTracker]')
		  AND type IN(N'U')
)
BEGIN

	CREATE TABLE [dbo].[ImmunizationTracker](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[PersonId] [int] NULL,
		[PtnPk] [int] NULL,
		[AntigenAdministered] [varchar](50) NOT NULL,
		[DateAdministered] [datetime] NOT NULL,
		[FacilityMFLCode] [varchar](5) NULL,
		[LotNumber] [varchar](50) NULL,
		[ExpiryDate] [datetime] NULL,
		[ProviderName] [varchar](50) NULL,
		[ProviderId] [varchar](15) NULL,
		[CreateDate] [datetime] NOT NULL,
		[CreatedBy] [int] NOT NULL,
		[DeleteFlag] [bit] NOT NULL,
	 CONSTRAINT [PK_ImmunizationTracker] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]

	ALTER TABLE [dbo].[ImmunizationTracker] ADD  CONSTRAINT [DF_ImmunizationTracker_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]


	ALTER TABLE [dbo].[ImmunizationTracker] ADD  CONSTRAINT [DF_ImmunizationTracker_CreatedBy]  DEFAULT ((1)) FOR [CreatedBy]
	

	ALTER TABLE [dbo].[ImmunizationTracker] ADD  CONSTRAINT [DF_ImmunizationTracker_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
	

	ALTER TABLE [dbo].[ImmunizationTracker]  WITH CHECK ADD  CONSTRAINT [CK_ImmunizationTracker_Subject] CHECK  (([PersonId] IS NOT NULL AND [PtnPk] IS NOT NULL))
	

	ALTER TABLE [dbo].[ImmunizationTracker] CHECK CONSTRAINT [CK_ImmunizationTracker_Subject]

END
IF NOT EXISTS
(
	SELECT *
	FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[HivTestTracker]')
		  AND type IN(N'U')
)
BEGIN
		CREATE TABLE [dbo].[HIVTestTracker](
			[Id] [int] IDENTITY(1,1) NOT NULL,
			[Ptn_Pk] [int] NULL,
			[PersonId] [int] NULL,
			[DiagnosisMode] [varchar](50) NOT NULL,
			[TestEpisode] [varchar](50) NULL,
			[Result] [varchar](50) NOT NULL,
			[ResultDate] [datetime] NOT NULL,
			[ResultCategory] [varchar](50) NULL,
			[MFLCode] [varchar](50) NULL,
			[Strategy] [varchar](50) NULL,
			[ProviderName] [varchar](50) NULL,
			[ProviderId] [varchar](20) NULL,
			[DeleteFlag] [bit] NOT NULL,
			[CreatedBy] [int] NOT NULL,
			[CreateDate] [datetime] NOT NULL,
		 CONSTRAINT [PK_HIVTestTracker] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]
		

		ALTER TABLE [dbo].[HIVTestTracker] ADD  CONSTRAINT [DF_HIVTestTracker_DiagnosisMode]  DEFAULT ('Antibody') FOR [DiagnosisMode]
		

		ALTER TABLE [dbo].[HIVTestTracker] ADD  CONSTRAINT [DF_HIVTestTracker_ResultCategory]  DEFAULT ('Final') FOR [ResultCategory]
		

		ALTER TABLE [dbo].[HIVTestTracker] ADD  CONSTRAINT [DF_HIVTestTracker_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
		

		ALTER TABLE [dbo].[HIVTestTracker] ADD  CONSTRAINT [DF_HIVTestTracker_CreatedBy]  DEFAULT ((1)) FOR [CreatedBy]
		

		ALTER TABLE [dbo].[HIVTestTracker] ADD  CONSTRAINT [DF_HIVTestTracker_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
		

		ALTER TABLE [dbo].[HIVTestTracker]  WITH CHECK ADD  CONSTRAINT [CK_HIVTestTracker_Subject] CHECK  (([Ptn_Pk] IS NOT NULL AND [PersonId] IS NOT NULL))
		

		ALTER TABLE [dbo].[HIVTestTracker] CHECK CONSTRAINT [CK_HIVTestTracker_Subject]
		
END