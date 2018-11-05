IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PatientDelivery]') AND type in (N'U')) 
BEGIN
CREATE TABLE [dbo].[PatientDelivery](
	[DeliveryID] [int] IDENTITY(1,1) NOT NULL,
	[PatientMasterVisitID] [int] NOT NULL,
	[ProfileID] [int] NOT NULL,
	[DurationOfLabour] [varchar](50) NULL,
	[DateOfDelivery] [datetime] NULL,
	[TimeOfDelivery] [time](7) NULL,
	[ModeOfDelivery] [int] NULL,
	[PlacentaComplete] [int] NULL,
	[BloodLoss] [int] NULL,
	[MotherCondition] [int] NULL,
	[DeliveryComplications] [int] NULL,
	[DeliveryComplicationNotes] [varchar](max) NULL,
	[DeliveryConductedBy] [varchar](50) NULL,
	[DeleteFlag] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[AuditData] [varchar](max) NULL,
  CONSTRAINT [PK_PatientDelivery] PRIMARY KEY CLUSTERED ([DeliveryID] ASC),
  CONSTRAINT [FK_PatientDelivery_PatientMasterVisit] FOREIGN KEY([PatientMasterVisitID]) REFERENCES [dbo].[PatientMasterVisit] ([Id])
 )
END
