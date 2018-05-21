/****** Object:  View [dbo].[AppointmentSummaryView]    Script Date: 5/18/2018 2:21:43 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[AppointmentSummaryView]
AS
select CONVERT(int, row_number() OVER (ORDER BY AppointmentDate)) Id, AppointmentDate, Count(*) Total, sum(CASE Name WHEN 'Missed' THEN 1 ELSE 0 END)
 Missed, sum(CASE Name WHEN 'Met' THEN 1 ELSE 0 END) Met, sum(CASE Name WHEN 'Pending' THEN 1 ELSE 0 END) Pending, 
sum(CASE Name WHEN 'Previously Missed' THEN 1 ELSE 0 END) PreviouslyMissed
from (
select PA.appointmentDate, L.name from PatientAppointment PA INNER JOIN LookupItem L ON L.Id = PA.StatusId WHERE PA.DeleteFlag = 0
UNION ALL
select a.appdate,b.Name from dtl_patientappointment a inner join mst_decode b on a.appstatus = b.id where a.deleteflag=0) Appointments
GROUP BY AppointmentDate

GO


