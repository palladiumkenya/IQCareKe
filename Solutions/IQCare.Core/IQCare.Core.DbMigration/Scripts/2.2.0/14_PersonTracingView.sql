ALTER VIEW [dbo].[PersonTracingView]
AS
SELECT        Id, PersonID, 
DateTracingDone AS TracingDate,
(SELECT        TOP (1) ItemName
FROM            dbo.LookupItemView
WHERE        (ItemId = dbo.Tracing.Mode)) AS TracingMode,
(SELECT        TOP (1) ItemName
FROM            dbo.LookupItemView AS LookupItemView_2
WHERE        (ItemId = dbo.Tracing.Outcome)) AS TracingOutcome,
(SELECT        TOP (1) ItemName
FROM            dbo.LookupItemView AS LookupItemView_3
WHERE        (ItemId = dbo.Tracing.ReasonNotContacted)) AS ReasonNotContacted,
OtherReasonSpecify,
DateBookedTesting,
(SELECT        TOP (1) ItemName
FROM            dbo.LookupItemView AS LookupItemView_1
WHERE        (ItemId = dbo.Tracing.Consent)) AS Consent,
DeleteFlag
FROM            dbo.Tracing


