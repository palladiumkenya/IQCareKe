/****** Object:  View [dbo].[AppointmentSummaryView]    Script Date: 5/18/2018 2:21:43 PM ******/
IF EXISTS (
		SELECT *
		FROM sys.VIEWS
		WHERE object_id = OBJECT_ID(N'[dbo].[AppointmentSummaryView]')
		)
	DROP VIEW [AppointmentSummaryView]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[AppointmentSummaryView]
AS
SELECT CONVERT(INT, row_number() OVER (
			ORDER BY AppointmentDate
			)) Id
	,AppointmentDate
	,Count(*) Total
	,sum(CASE Name
			WHEN 'Missed'
				THEN 1
			ELSE 0
			END) Missed
	,sum(CASE Name
			WHEN 'Met'
				THEN 1
			ELSE 0
			END) Met
	,sum(CASE Name
			WHEN 'Pending'
				THEN 1
			ELSE 0
			END) Pending
	,sum(CASE Name
			WHEN 'Previously Missed'
				THEN 1
			ELSE 0
			END) PreviouslyMissed
FROM (
	SELECT PA.appointmentDate
		,L.name
	FROM PatientAppointment PA
	INNER JOIN LookupItem L ON L.Id = PA.StatusId
	WHERE PA.DeleteFlag = 0
	
	UNION ALL
	
	SELECT a.appdate
		,b.Name
	FROM dtl_patientappointment a
	INNER JOIN mst_decode b ON a.appstatus = b.id
	WHERE a.deleteflag = 0 AND a.ModuleId IN(SELECT m.ModuleID FROM mst_module m WHERE m.ModuleName='CCC Patient Card MoH 257')
	) Appointments
GROUP BY AppointmentDate
GO





