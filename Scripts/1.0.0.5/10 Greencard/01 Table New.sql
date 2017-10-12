/****** Object:  Table [dbo].[PatientAdverseEventOutcome]    Script Date: 10/10/2017 7:48:38 AM ******/
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientAdverseEventOutcome]') AND type in (N'U'))
BEGIN
	CREATE TABLE [dbo].[PatientAdverseEventOutcome](
		[Id] [int] IDENTITY(1,1) NOT NULL,
		[PatientId] [int] NOT NULL,
		[PatientMasterVisitId] [int] NOT NULL,
		[AdverseEventId] [int] NOT NULL,
		[OutComeId] [int] NOT NULL,
		[OutcomeDate] [datetime] NOT NULL,
		[DeleteFlag] [bit] NOT NULL CONSTRAINT [DF_AdverseEventOutcome_DeleteFlag]  DEFAULT ((0)),
		[CreateDate] [datetime] NOT NULL,
		[CreatedBy] [int] NOT NULL,
		[AuditData] [xml] NULL,
		[UserId] [int] NOT NULL,
	 CONSTRAINT [PK_AdverseEventOutcome] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END



