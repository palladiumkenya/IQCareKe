

/****** Object:  Table [dbo].[HEIEncounter]    Script Date: 9/11/2018 11:07:02 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_HEIEncounter_PatientMasterVisit') AND parent_object_id = OBJECT_ID(N'dbo.HEIEncounter'))
ALTER TABLE [dbo].[HEIEncounter] DROP CONSTRAINT [FK_HEIEncounter_PatientMasterVisit]
IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'dbo.FK_HEIEncounter_Patient') AND parent_object_id = OBJECT_ID(N'dbo.HEIEncounter'))
ALTER TABLE [dbo].[HEIEncounter] DROP CONSTRAINT [FK_HEIEncounter_Patient]

IF OBJECT_ID('dbo.HEIEncounter', 'U') IS NOT NULL DROP TABLE dbo.HEIEncounter

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HEIEncounter]') AND type in (N'U')) 

CREATE TABLE [dbo].[HEIEncounter](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[PatientMasterVisitId] [int] NOT NULL,
	[PatientId] [int] not null,
	[PlaceOfDeliveryId] [int] NULL,
	[PlaceOfDeliveryOther] [varchar](100) NULL,
	[ModeOfDeliveryId] [int] NULL,
	[BirthWeight] [float] NULL,
	[ArvProphylaxisId] [int] NULL,
	[ArvProphylaxisOther] [varchar](100) NULL,
	[MotherRegisteredId] [bit] NULL,
	[MotherPersonId] [int] NULL,
	[MotherStatusId] [int] NULL,
	[PrimaryCareGiverID] [int] NULL,
	[MotherName] [varchar](255) NULL,
	[MotherCCCNumber] [varchar](20) NULL,
	[MotherPMTCTDrugsId] [int] NULL,
	[MotherPMTCTRegimenId] [int] NULL,
	[MotherPMTCTRegimenOther] [varchar](100) NULL,
	[MotherArtInfantEnrolId] [bit] NULL,
	[MotherArtInfantEnrolRegimenId] [int] NULL,
	[MotherCurrentRegimenId] [int] NULL,
	[MotherLatestVL] [int] NULL,
	[Outcome24MonthId] [int] NULL,
	[DeleteFlag] [bit] NOT NULL DEFAULT ((0)),
	[CreateDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_HEIEncounter] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[HEIEncounter]  WITH CHECK ADD  CONSTRAINT [FK_HEIEncounter_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitId])
REFERENCES [dbo].[PatientMasterVisit] ([Id])
GO

ALTER TABLE [dbo].[HEIEncounter]  WITH CHECK ADD  CONSTRAINT [FK_HEIEncounter_Patient] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([Id])
GO

ALTER TABLE [dbo].[HEIEncounter] CHECK CONSTRAINT [FK_HEIEncounter_PatientMasterVisit]
GO

ALTER TABLE [dbo].[HEIEncounter] CHECK CONSTRAINT [FK_HEIEncounter_Patient]
GO

