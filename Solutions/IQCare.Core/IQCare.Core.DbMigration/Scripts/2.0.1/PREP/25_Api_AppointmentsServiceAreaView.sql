SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('dbo.[Api_AppointmentsServiceAreaView]', 'V') IS NOT NULL
    DROP VIEW [dbo].[Api_AppointmentsServiceAreaView]
GO



CREATE VIEW [dbo].[Api_AppointmentsServiceAreaView]
AS
SELECT
ISNULL(ROW_NUMBER() OVER(ORDER BY PT.Id ASC), -1) AS Id,
PT.Id AS PatientId,
PA.Id AS AppointmentId,
vt.ServiceId as ServiceAreaId,
AppointmentReason = (SELECT ItemName FROM LookupItemView LV WHERE MasterName = 'AppointmentReason' AND LV.ItemId = PA.ReasonId),
AppointmentDate = format(cast(PA.AppointmentDate as date),'yyyyMMdd'),
AppointmentStatus = (SELECT ItemName FROM LookupItemView LV WHERE MasterName = 'AppointmentStatus' AND LV.ItemId = PA.StatusId),
AppointmentType = (SELECT ItemName FROM LookupItemView LV WHERE MasterName = 'DifferentiatedCare' AND LV.ItemId = PA.DifferentiatedCareId),
Description = PA.Description
FROM            dbo.PatientAppointment AS PA
INNER JOIN Patient PT ON PT.Id = PA.PatientId
INNER JOIN PatientMasterVisit vt on vt.Id =pa.PatientMasterVisitId




GO

