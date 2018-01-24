-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		John Macharia
-- Create date: 23rd Jan 2018
-- Description:	Get a patients viral loads
-- =============================================
create PROCEDURE sp_getAllViralLoads 
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
	b.deleteflag,c.resultdate 
	from ord_laborder a inner join dtl_LabOrderTestResult b on a.id = b.laborderid
	inner join dtl_LabOrderTest c on a.id = c.laborderid
	where b.parameterid in (3,107) and a.ptn_pk=@ptnpk ) VLs
	where VLs.resultvalue is not null
END
GO
