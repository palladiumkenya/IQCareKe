
CREATE TABLE [dbo].[BaselineAntenatalCare](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[PatientMasterVisitId] [int] NOT NULL,
	[PregnancyId] [int] NOT NULL,
	[HivStatusBeforeAnc] [int] NOT NULL,
	[TreatedForSyphilis] [int] NOT NULL,
	[BreastExamDone] [int] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[DeleteFlag] [bit] NOT NULL,
	[AudiData] [xml] NULL,
 CONSTRAINT [PK_BaselineAntenatalCare] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[BaselineAntenatalCare]  WITH CHECK ADD  CONSTRAINT [FK_BaselineAntenatalCare_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO

ALTER TABLE [dbo].[BaselineAntenatalCare] CHECK CONSTRAINT [FK_BaselineAntenatalCare_Patient]
GO

ALTER TABLE [dbo].[BaselineAntenatalCare]  WITH CHECK ADD  CONSTRAINT [FK_BaselineAntenatalCare_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitId])
REFERENCES [dbo].[PatientMasterVisit] ([Id])
GO

ALTER TABLE [dbo].[BaselineAntenatalCare] CHECK CONSTRAINT [FK_BaselineAntenatalCare_PatientMasterVisit]
GO


