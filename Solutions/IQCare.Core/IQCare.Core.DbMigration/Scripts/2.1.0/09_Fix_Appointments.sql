--MIN AND MAX DATE
DECLARE @minDate DATETIME, @maxDATE DATETIME, @facility int, @appDate DATETIME
BEGIN

SELECT @minDate = '2018-01-10';
SELECT @maxDATE = GETDATE();
SELECT @facility = FacilityID FROM mst_Facility WHERE DeleteFlag = 0
--SELECT @minDate, @maxDATE
--update all appointments to pending
UPDATE PatientAppointment SET StatusDate = NULL, StatusId = (SELECT TOP 1 ItemId FROM LookupItemView WHERE MasterName = 'AppointmentStatus' AND ItemName = 'Pending')

WHILE @minDate < @maxDATE
BEGIN
/*
LOOP AND UPDATE THE DATE
*/
EXEC	[dbo].[pr_Scheduler_UpdateAppointmentStatus]
@Currentdate = @minDate,
@locationid = @facility

SET @minDate = DATEADD(DAY,1,@minDate);

DECLARE @message varchar(80);
PRINT ' '  
SELECT @message = '----- Appointment Date: ' + CAST(@minDate as varchar(50));
PRINT @message;
END; 

END;