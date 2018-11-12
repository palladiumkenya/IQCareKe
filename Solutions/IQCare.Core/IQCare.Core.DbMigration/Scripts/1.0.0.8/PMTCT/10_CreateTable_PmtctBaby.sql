IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PMTCTBaby]') OR  object_id = OBJECT_ID(N'[dbo].[DeliveredBabyBirthInformation]')) 
BEGIN
CREATE TABLE [dbo].[PMTCTBaby](
	[BirthId] [int] NOT NULL,
	[DeliveryId] [int] NOT NULL,
	[PatientMasterVisitId] [int] NOT NULL,
	[Sex] [int] NULL,
	[BirthWeight] [decimal](18, 2) NULL,
	[DeliveryOutcome] [int] NULL,
	[ResuscitationDone] [int] NULL,
	[BirthDeformity] [int] NULL,
	[TeoGiven] [int] NULL,
	[BreastFedWithinHr] [int] NULL,
	[APGAR1min] [int] NULL,
	[APGAR5min] [int] NULL,
	[APGAR10min] [int] NULL,
	[BirthNotificationNumber] [varchar](50) NULL,
	[BirthComments] [varchar](max) NULL,
	[Deleteflag] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[AuditData] [varchar](max) NULL,
 CONSTRAINT [PK_PMTCTBaby] PRIMARY KEY CLUSTERED (	[BirthId] ASC),
 CONSTRAINT [FK_PMTCTBaby_PatientDelivery] FOREIGN KEY([DeliveryId]) REFERENCES [dbo].[PatientDelivery] ([DeliveryID]),
 CONSTRAINT [FK_PMTCTBaby_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitId]) REFERENCES [dbo].[PatientMasterVisit] ([Id])
 )
END