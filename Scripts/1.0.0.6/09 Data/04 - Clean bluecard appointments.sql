;with App as (SELECT
	AppointmentId
   ,Ptn_pk
   ,LocationID
   ,AppStatus
   ,AppReason
   ,AppDate
   , CreateDate
   ,row_number() over(partition by (CAST(ptn_pk AS VARCHAR(10)) + '|' + CONVERT(VARCHAR(10), appdate, 102)) order by AppointmentId) RowNum
FROM dtl_patientAppointment)
delete from app where RowNum > 1 