/****** Object:  Table [dbo].[PatientPrEPStatus]    Script Date: 6/6/2019 9:25:06 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PatientPrEPStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[PatientEncounterId] [int] NOT NULL,
	[SignsOrSymptomsHIV] [int] NOT NULL,
	[AdherenceCounsellingDone] [int] NOT NULL,
	[ContraindicationsPrepPresent] [int] NOT NULL,
	[PrepStatusToday] [int] NOT NULL,
	[CondomsIssued] [int] NULL,
	[NoOfCondoms] [int] NULL,
	[DeleteFlag] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[AuditData] [xml] NULL,
 CONSTRAINT [PK_PatientPrEPStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[PatientPrEPStatus] ADD  CONSTRAINT [DF_PatientPrEPStatus_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO


