IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[PersonTracingView]'))
	DROP VIEW [dbo].[PersonTracingView]
GO
/****** Object:  View [dbo].[PersonTracingView]    Script Date: 22/05/2018 12:13:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[PersonTracingView]
AS
SELECT        Id, PersonID, DateTracingDone AS TracingDate,
                             (SELECT        TOP (1) ItemName
                               FROM            dbo.LookupItemView
                               WHERE        (ItemId = dbo.Tracing.Mode)) AS TracingMode,
                             (SELECT        TOP (1) ItemName
                               FROM            dbo.LookupItemView AS LookupItemView_2
                               WHERE        (ItemId = dbo.Tracing.Outcome)) AS TracingOutcome, DateBookedTesting,
                             (SELECT        TOP (1) ItemName
                               FROM            dbo.LookupItemView AS LookupItemView_1
                               WHERE        (ItemId = dbo.Tracing.Consent)) AS Consent,
							   DeleteFlag
FROM            dbo.Tracing
GO


