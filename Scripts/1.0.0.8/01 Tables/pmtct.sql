IF NOT EXISTS(SELECT * FROM sys.objects WHERE Object_ID = OBJECT_ID(N'PatientProfile'))
BEGIN
	CREATE TABLE [dbo].[PatientProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[PatientMasterVisitId] [int] NOT NULL,
	[AgeMenarche] [decimal](4, 1) NOT NULL,
	[PregnancyId] [int] NOT NULL,
	[VisitNumber] [int] NULL,
	[VisitType] [int] NULL,
	[TreatedForSyphilis] [int] NULL,
	[DeleteFlag] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[AuditData] [varchar](max) NULL,
	[DaysPostPartum] [int] NULL,
	 CONSTRAINT [PK_PatientProfile] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

	ALTER TABLE [dbo].[PatientProfile] ADD  CONSTRAINT [DF_PatientProfile_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
	ALTER TABLE [dbo].[PatientProfile] ADD  CONSTRAINT [DF_PatientProfile_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
	ALTER TABLE [dbo].[PatientProfile]  WITH CHECK ADD  CONSTRAINT [FK_PatientProfile_Patient] FOREIGN KEY([PatientId])
	REFERENCES [dbo].[Patient] ([Id])
	ALTER TABLE [dbo].[PatientProfile] CHECK CONSTRAINT [FK_PatientProfile_Patient]
	ALTER TABLE [dbo].[PatientProfile]  WITH CHECK ADD  CONSTRAINT [FK_PatientProfile_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitId])
	REFERENCES [dbo].[PatientMasterVisit] ([Id])
	ALTER TABLE [dbo].[PatientProfile] CHECK CONSTRAINT [FK_PatientProfile_PatientMasterVisit]
	ALTER TABLE [dbo].[PatientProfile]  WITH CHECK ADD  CONSTRAINT [FK_PatientProfile_Pregnancy] FOREIGN KEY([PregnancyId])
	REFERENCES [dbo].[Pregnancy] ([Id])
	ALTER TABLE [dbo].[PatientProfile] CHECK CONSTRAINT [FK_PatientProfile_Pregnancy]
END