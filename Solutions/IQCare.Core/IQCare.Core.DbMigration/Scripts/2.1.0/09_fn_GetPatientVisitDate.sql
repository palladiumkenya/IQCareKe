ALTER FUNCTION [dbo].[fn_GetPatientVisitDate]
(
@PatientId as int,
@PatientMasterVisitId  as int
)
RETURNS Datetime
AS

BEGIN
DECLARE @ExitDate as datetime;
Declare @VisitDate as datetime;
declare @date as datetime;
SET @ExitDate= (select ExitDate from PatientCareending where PatientMasterVisitId =@PatientMasterVisitId)
SET @VisitDate= (select VisitDate from PatientMasterVisit where Id=@PatientMasterVisitId)

SET @date=@VisitDate;
return @date;
END