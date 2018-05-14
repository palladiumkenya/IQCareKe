SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 11 May 2018
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE sp_IsVisitScheduled 
	@PatientId int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @ptn_pk int = (select ptn_pk from patient where id = @PatientId)
	declare @GracePeriod int = (Select top 1 AppGracePeriod from mst_facility where deleteflag=0)
    
	select * from (
	select AppointmentDate from patientappointment where PatientId = @PatientId
	UNION
	select AppDate from dtl_patientappointment where ptn_pk=@ptn_pk) AppDates
	where AppointmentDate between dateadd(day, -@GracePeriod, getdate()) and dateadd(day, @GracePeriod, getdate())

END
GO
