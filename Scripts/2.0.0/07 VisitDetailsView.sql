IF OBJECT_ID('dbo.VisitDetailsView', 'V') IS NOT NULL
    DROP VIEW [dbo].[VisitDetailsView]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW  [dbo].[VisitDetailsView]
AS

select 
  v.Id,
  v.PatientId,
  v.PatientMasterVisitId,
  v.pregnancyId,
  v.serviceAreaId,
  (SELECT top 1 s.Name FROM ServiceArea s WHERE s.Id=v.serviceAreaId) ServiceAreaName,
  v.VisitDate,
  v.VisitNumber,
  v.DaysPostPartum,
  v.VisitType,
  (SELECT top 1 l.Name FROM LookupItem l WHERE l.Id=v.VisitType) VisitTypeName,
  v.DeleteFlag,
  v.CreatedBy
  FROM 
VisitDetails v

GO
