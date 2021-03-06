-- =============================================
-- Author:		John Macharia
-- Create date: 23rd Jan 2018
-- Description:	Get a patients viral loads
-- =============================================
ALTER PROCEDURE [dbo].[sp_getAllViralLoads] 
	-- Add the parameters for the stored procedure here
	@PatientId int = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare @ptnpk int = (select top 1 ptn_pk from patient where id = @PatientId)

	select distinct * from (
	select a.ptn_pk,a.orderdate,orderstatus,parameterid,
	coalesce(
	case when resultvalue is not null then resultvalue else null end,
	case when undetectable = 1 then 50 else null end
	) resultvalue,
	b.deleteflag,Isnull(c.resultdate ,c.StatusDate) ResultDate
	from ord_laborder a inner join dtl_LabOrderTestResult b on a.id = b.laborderid
	inner join dtl_LabOrderTest c on a.id = c.laborderid
	where b.parameterid in (select Id from [dbo].[Mst_LabTestParameter] where ReferenceId = 'VIRAL_LOAD' OR ReferenceId = 'VIRALLOADUNDETECTABLE' OR ReferenceId = 'VIRAL_LOAD') and a.ptn_pk=@ptnpk ) VLs
	where VLs.resultvalue is not null
	ORDER BY OrderDate DESC
END
