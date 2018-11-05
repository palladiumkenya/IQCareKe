IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientPartnerTesting]') AND type in (N'U')) 
BEGIN
CREATE TABLE [dbo].[PatientPartnerTesting](
	[Id] [int] NOT NULL IDENTITY(1,1),
	[PatientId] [int] NOT NULL,
	[PatientMasterVisitId] [int] NOT NULL,
	[PartnerTested] [int] NULL,
	[PartnerHIVResult] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[DeleteFlag] [bit] NULL,
	[AuditData] [varchar](max) NULL,

 CONSTRAINT [PK_PatientPartnerTesting] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [FK_PatientPartnerTesting_Patient] FOREIGN KEY([PatientId]) REFERENCES [dbo].[Patient] ([Id]),
 CONSTRAINT [FK_PatientPartnerTesting_PatientEncounter] FOREIGN KEY([PatientEncounterId]) REFERENCES [dbo].[PatientEncounter] ([Id]),
 CONSTRAINT [FK_PatientPartnerTesting_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitId]) REFERENCES [dbo].[PatientMasterVisit] ([Id])
)
END 