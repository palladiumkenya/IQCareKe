IF OBJECT_ID('dbo.IndicatorResultsView', 'V') IS NOT NULL
    DROP VIEW [dbo].[IndicatorResultsView]
GO
CREATE VIEW IndicatorResultsView
AS
SELECT frm.Id AS FormId, frm.Name as FormName,frmp.FormReportingDate, se.SectionName, sb.SubSectionName, i.Code, i.IndicatorName,
ir.ResultNumeric,ir.ResultText FROM IndicatorResults ir 
INNER JOIN FormReportingPeriod frmp ON frmp.Id = IR.FormReportingPId
INNER JOIN Indicator i ON ir.IndicatorId = i.Id
INNER JOIN SubSection sb ON sb.Id = i.SubSectionId
INNER JOIN Section se ON se.Id = sb.SectionId
INNER JOIN Form frm ON frm.Id = se.FormId


