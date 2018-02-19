/****** Object:  Table [dbo].[FacilityStatistics]    Script Date: 1/10/2018 8:42:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS
(
	SELECT *
	FROM sys.objects
	WHERE object_id = OBJECT_ID(N'[dbo].[FacilityStatistics]')
		  AND type IN(N'U')
)
BEGIN
	CREATE TABLE [dbo].[FacilityStatistics](
		[TotalCumulativePatients] [int] NOT NULL,
		[TotalActiveOnArt] [int] NOT NULL,
		[TotalTransferIn] [int] NOT NULL,
		[TotalPatientsTransferedOut] [int] NOT NULL,
		[TotalOnCtxDapson] [int] NOT NULL,
		[TotalPatientsDead] [int] NOT NULL,
		[TotalTransit] [int] NOT NULL,
		[LostToFollowUp] [int] NOT NULL,
		[TotalUndocumentedLTFU] [int] NOT NULL
	) ON [PRIMARY]
END
