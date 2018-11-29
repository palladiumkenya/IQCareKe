CREATE TABLE [dbo].[VisitDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[PatientMasterVisitId] [int] NOT NULL,
	[ServiceAreaId] [int] NOT NULL,
	[VisitDate] [Datetime]NOT NULL,
	[VisitNumber] [int] NOT NULL,
	[DaysPostPartum] [int] NULL,
	[VisitType] [int] NULL,
	[DeleteFlag] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[AuditData] [xml] NULL,
 CONSTRAINT [PK_VisitDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[VisitDetails] ADD  CONSTRAINT [DF_VisitDetails_DeleteFlag]  DEFAULT ((0)) FOR [DeleteFlag]
GO

ALTER TABLE [dbo].[VisitDetails] ADD  CONSTRAINT [DF_VisitDetails_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[VisitDetails]  WITH CHECK ADD  CONSTRAINT [FK_VisitDetails_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO

ALTER TABLE [dbo].[VisitDetails] CHECK CONSTRAINT [FK_VisitDetails_Patient]
GO

ALTER TABLE [dbo].[VisitDetails]  WITH CHECK ADD  CONSTRAINT [FK_VisitDetails_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitId])
REFERENCES [dbo].[PatientMasterVisit] ([Id])
GO

ALTER TABLE [dbo].[VisitDetails] CHECK CONSTRAINT [FK_VisitDetails_PatientMasterVisit]
GO


