IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientLabSample]') AND type in (N'U')) 
BEGIN
CREATE TABLE [dbo].[PatientLabSample](
	[Id] [int] NOT NULL,
	[PatientId] [int] NOT NULL,
	[PatientMasterVisitId] [int] NOT NULL,
	[SampleID] [int] NULL,
	[SampleValue] [int] NULL,
	[Description] [varchar](50) NULL,
	[DeleteFlag] [bit] NOT NULL,
	[Createdby] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[AuditData] [varchar](max) NULL,
 CONSTRAINT [PK_PatientLabSample] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [FK_PatientLabSample_Patient] FOREIGN KEY([PatientId]) REFERENCES [dbo].[Patient] ([Id]),
 CONSTRAINT [FK_PatientLabSample_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitId])  REFERENCES [dbo].[PatientMasterVisit] ([Id])
 )
 END
