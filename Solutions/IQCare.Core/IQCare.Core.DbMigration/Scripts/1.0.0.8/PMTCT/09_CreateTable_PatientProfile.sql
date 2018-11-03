IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientProfile]') AND type in (N'U')) 
BEGIN
CREATE TABLE [dbo].[PatientProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[PatientMasterVisitId] [int] NOT NULL,
	[AgeMenarche] [decimal](4, 1) NULL,
	[PregnancyId] [int] NULL,
	[VisitNumber] [int] NULL,
	[VisitType] [int] NULL,
	[TreatedForSyphilis] [int] NULL,
	[DeleteFlag] [bit] NOT NULL DEFAULT(0),
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL DEFAULT(GetDate()),
	[AuditData] [varchar](max) NULL,
	[DaysPostPartum] [int] NULL,
 CONSTRAINT [PK_PatientProfile] PRIMARY KEY CLUSTERED (	[Id] ASC),
 CONSTRAINT [FK_PatientProfile_Patient] FOREIGN KEY([PatientId]) REFERENCES [dbo].[Patient] ([Id]),
 CONSTRAINT [FK_PatientProfile_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitId]) REFERENCES [dbo].[PatientMasterVisit] ([Id]),
 CONSTRAINT [FK_PatientProfile_Pregnancy] FOREIGN KEY([PregnancyId]) REFERENCES [dbo].[Pregnancy] ([Id])
 )
END


