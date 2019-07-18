IF EXISTS (SELECT *
           FROM   sys.objects
           WHERE  object_id = OBJECT_ID(N'[dbo].[fn_GetPatientVisitDate]')
                  AND type IN ( N'FN', N'IF', N'TF', N'FS', N'FT' ))
				  BEGIN
  DROP FUNCTION [dbo].[fn_GetPatientVisitDate]
  END
  GO

CREATE FUNCTION [dbo].[fn_GetPatientVisitDate]
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